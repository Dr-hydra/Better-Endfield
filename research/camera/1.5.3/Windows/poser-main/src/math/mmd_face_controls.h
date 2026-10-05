#pragma once
#include "math/character_face.h"

namespace mmd_face_controls {
struct Native {std::string name;int channel=-1,panel=4;};
struct Control {std::string name;int morph=-1,native=-1,panel=4;};
inline std::string Label(const std::string &name,int panel,int index) {
  auto n=character_face::MorphSpelling(name);std::string side;
  for(auto p:{std::pair<const char *,const char *>{u8"左",u8"左侧"},{u8"右",u8"右侧"}}) {
    std::string suffix=p.first;
    if(n.size()>=suffix.size()&&n.compare(n.size()-suffix.size(),suffix.size(),suffix)==0){side=p.second;n.resize(n.size()-suffix.size());break;}
  }
  const std::pair<const char *,const char *> names[]={
    {u8"あ",u8"啊口型"},{u8"あ2",u8"啊口型 2"},{u8"い",u8"咿口型"},{u8"う",u8"呜口型"},{u8"え",u8"诶口型"},{u8"お",u8"哦口型"},{u8"ん",u8"闭嘴"},
    {u8"まばたき",u8"眨眼"},{u8"笑い",u8"笑眼"},{u8"なごみ",u8"温和眼"},{u8"びっくり",u8"惊讶眼"},{u8"じと目",u8"半睁眼"},{u8"キリッ",u8"锐利眼神"},
    {u8"悲しい",u8"悲伤眼"},{u8"悲しい目",u8"悲伤眼"},{u8"怒り目",u8"生气眼"},{u8"はぅ",u8"紧闭双眼"},
    {u8"真面目",u8"认真眉"},{u8"困る",u8"困扰眉"},{u8"怒り",u8"生气眉"},{u8"にこり",u8"微笑眉"},{u8"上",u8"眉毛上提"},{u8"下",u8"眉毛下压"},{u8"前",u8"眉毛前移"},
    {u8"にっこり",u8"微笑嘴"},{u8"にやり",u8"嘴角微扬"},{u8"にやり2",u8"嘴角微扬 2"},{u8"口角上げ",u8"嘴角上扬"},{u8"口角下げ",u8"嘴角下垂"},
    {u8"口横広げ",u8"嘴部加宽"},{u8"口横狭め",u8"嘴部收窄"},{u8"ω",u8"猫咪嘴"},{u8"ω□",u8"张口猫咪嘴"},{u8"∧",u8"抿嘴"},{u8"▲",u8"三角嘴"},
    {u8"ワ",u8"大笑嘴"},{u8"ぺろっ",u8"吐舌"},{u8"てへぺろ",u8"俏皮吐舌"}
  };
  if(n==u8"ウィンク"||n==u8"ウィンク2") {
    if(side.empty())side=u8"左侧";
    return side+(n==u8"ウィンク"?u8"笑眼眨眼":u8"普通眨眼");
  }
  for(auto &p:names)if(n==p.first)return side+p.second;
  const char *groups[]={u8"其他",u8"眉部",u8"眼部",u8"嘴部",u8"其他"};
  return std::string(groups[panel>=1&&panel<=4?panel:4])+u8"表情 "+std::to_string(index+1);
}
inline int Panel(const character_face::Profile &profile,const character_face::Morph &morph) {
  if(morph.panel>=1&&morph.panel<=4)return morph.panel;
  // Legacy calibrations predate PMX panel metadata. Prefer clear MMD names,
  // then infer the dominant affected region without inventing new shapes.
  auto n=character_face::MorphSpelling(morph.name);
  if(n.find(u8"口")!=n.npos||n==u8"にっこり")return 3;
  if(n.find(u8"目")!=n.npos||n.find(u8"ウィンク")!=n.npos||n==u8"まばたき"||n==u8"笑い"||n==u8"なごみ"||n==u8"びっくり"||n==u8"じと目")return 2;
  if(n.find(u8"眉")!=n.npos||n.find(u8"まゆ")!=n.npos||n==u8"真面目"||n==u8"困る"||n==u8"怒り"||n==u8"にこり"||n==u8"上"||n==u8"下"||n==u8"前")return 1;
  float energy[4]={};
  for(const auto &d:morph.deltas)if(d.bone>=0&&d.bone<int(profile.bones.size())) {
    int r=face_mixing::BoneRegion(profile.bones[d.bone].name);
    if(r>=0)energy[r]+=Len(d.position)+Len(d.rotation);
  }
  int best=-1;for(int i=0;i<4;++i)if(energy[i]>0&&(best<0||energy[i]>energy[best]))best=i;
  return best>=0?best+1:4;
}
inline std::vector<Control> Build(const character_face::Profile *profile,const std::vector<Native> &fixed) {
  std::vector<Control> controls;std::set<std::string> spellings;
  if(profile)for(int i=0;i<int(profile->morphs.size());++i) {
    const auto &m=profile->morphs[i];Control c{m.name,i,-1,Panel(*profile,m)};
    auto spelling=character_face::MorphSpelling(m.name);
    for(const auto &f:fixed)if(character_face::MorphSpelling(f.name)==spelling){c.native=f.channel;break;}
    controls.push_back(c);spellings.insert(spelling);
  }
  for(const auto &f:fixed)if(spellings.insert(character_face::MorphSpelling(f.name)).second)
    controls.push_back({f.name,-1,f.channel,f.panel});
  return controls;
}
struct State {
  std::shared_ptr<const character_face::Profile> profile;
  std::vector<Control> controls;
  std::vector<float> weights;
  void *owner=nullptr;uint64_t generation=0;
  bool applied=false,fallback=true;
  float strength=1;
  void bind(void *actor,uint64_t revision,std::shared_ptr<const character_face::Profile> selected,const std::vector<Native> &fixed) {
    if(owner==actor&&generation==revision&&profile==selected)return;
    owner=actor;generation=revision;profile=std::move(selected);
    controls=Build(profile.get(),fixed);weights.assign(controls.size(),0);applied=false;
  }
  bool set(int index,float value) {
    if(index<0||index>=int(weights.size())||!std::isfinite(value))return false;
    weights[index]=face_geometry::Clamp(value,0,1);applied=true;return true;
  }
  void clear(int panel=0) {
    for(int i=0;i<int(weights.size());++i)if(panel==0||controls[i].panel==panel)weights[i]=0;
    applied=true;
  }
};
}
