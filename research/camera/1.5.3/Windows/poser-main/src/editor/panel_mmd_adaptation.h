#pragma once
#include "game/mmd_player.h"
#include "imgui.h"

static bool MmdBoneChoice(const char *label, std::string &value,
                          const char *emptyLabel) {
  bool changed = false;
  if (ImGui::BeginCombo(label, value.empty() ? emptyLabel : value.c_str())) {
    if (ImGui::Selectable(emptyLabel, value.empty())) {
      value.clear();
      changed = true;
    }
    for (const auto &b : g_mmd.rig.bones)
      if (ImGui::Selectable(b.name.c_str(), value == b.name)) {
        value = b.name;
        changed = true;
      }
    ImGui::EndCombo();
  }
  return changed;
}
static void DrawMmdAdaptationPanel() {
  if (!ImGui::CollapsingHeader(u8"骨架适配 / 追加骨骼"))
    return;
  auto &m = g_mmd;
  static mmd::RigAdaptation draft;
  static int revision = -1;
  static int editing = -1;
  static bool dirty = false;
  if (revision != m.adaptationRevision) {
    draft = m.adaptation;
    revision = m.adaptationRevision;
    dirty = false;
    editing = -1;
  }
  ImGui::TextWrapped(u8"先停止动作，再追加骨骼或修改映射。应用后可播放或逐帧检"
                     u8"查，角色校准保持不变。");
  ImGui::BeginDisabled(m.loading || m.session.active);
  if (ImGui::Button(u8"应用适配")) {
    if (MmdApplyAdaptation(draft, m.sourcePreset)) {
      revision = m.adaptationRevision;
      dirty = false;
    }
  }
  ImGui::SameLine();
  if (ImGui::Button(u8"撤销未应用修改")) {
    draft = m.adaptation;
    dirty = false;
  }
  ImGui::SameLine();
  if (ImGui::Button(u8"清空适配")) {
    draft = {};
    dirty = true;
  }
  ImGui::BeginDisabled(dirty);
  if (ImGui::Button(u8"保存适配预设"))
    MmdBeginLoad(4);
  ImGui::SameLine();
  if (ImGui::Button(u8"载入适配预设"))
    MmdBeginLoad(3);
  ImGui::EndDisabled();
  if (dirty)
    ImGui::TextWrapped(u8"有未应用修改；点击“应用适配”后生效并可保存。");
  if (!m.adaptationFile.empty())
    ImGui::TextWrapped(u8"预设文件：%s", m.adaptationFile.c_str());

  if (ImGui::TreeNode(u8"准标准骨补全")) {
    if(ImGui::SmallButton(u8"全部开启")) {
      auto next=mmd::DefaultRigAdaptation();next.extra=draft.extra;next.tracks=draft.tracks;next.roles=draft.roles;
      draft=std::move(next);dirty=true;
    }
    ImGui::TextDisabled(u8"默认全部开启，可单独关闭。载入预设时使用预设设置。");
    ImGui::TextWrapped(u8"已有同名骨骼保留原定义。内置骨架已含部分标准骨；此处"
                       u8"也可补全 PMX 参考缺少的骨骼。");
    dirty |= ImGui::Checkbox(u8"全ての親（总父级）", &draft.parentRoot);
    dirty |= ImGui::Checkbox(u8"グルーブ（上下位移）", &draft.groove);
    dirty |= ImGui::Checkbox(u8"上半身2（胸部）", &draft.upperBody2);
    dirty |=
        ImGui::Checkbox(u8"腰 / 腰キャンセル（腰部取消）", &draft.waistCancel);
    dirty |= ImGui::Checkbox(u8"肩P / 肩C（肩部取消）", &draft.shoulderCancel);
    dirty |= ImGui::Checkbox(u8"腕捩 / 手捩及分段附加骨", &draft.twists);
    dirty |= ImGui::Checkbox(u8"足IK親（脚部 IK 父级）", &draft.ikParents);
    dirty |= ImGui::Checkbox(u8"足D / ひざD / 足首D / 足先EX", &draft.legD);
    if (draft.legD)
      ImGui::TextWrapped(
          u8"腿、膝、脚踝、脚尖默认改用 D / EX 链；可在角色部位映射中覆盖。");
    dirty |= ImGui::Checkbox(u8"操作中心（独立视图骨）", &draft.controlRoot);
    dirty |=
        ImGui::Checkbox(u8"额外上半身1（插入上半身2之前）", &draft.upperBody1);
    ImGui::TextWrapped(u8"操作中心默认不影响身体。分段捩骨不会重刷游戏模型权重"
                       u8"；需要对应角色部位映射才能直接输出。");
    ImGui::TreePop();
  }
  if (ImGui::TreeNode(u8"自定义追加 / 插入骨骼")) {
    static char name[241] = {};
    static std::string parent, anchor, before, grant;
    static float offset[3] = {}, weight = 1;
    static bool rotation = false, position = false;
    int remove = -1;
    for (size_t i = 0; i < draft.extra.size(); ++i) {
      ImGui::PushID(int(i));
      const auto &b = draft.extra[i];
      if (ImGui::Selectable(b.name.c_str(), editing == int(i))) {
        editing = int(i);
        snprintf(name, sizeof(name), "%s", b.name.c_str());
        parent = b.parent;
        anchor = b.anchor;
        before = b.before;
        grant = b.grant;
        offset[0] = b.offset.x;
        offset[1] = b.offset.y;
        offset[2] = b.offset.z;
        weight = b.grantWeight;
        rotation = b.grantRotation;
        position = b.grantPosition;
      }
      ImGui::SameLine();
      if (ImGui::SmallButton(u8"移除"))
        remove = int(i);
      ImGui::PopID();
    }
    if (remove >= 0) {
      draft.extra.erase(draft.extra.begin() + remove);
      editing = -1;
      dirty = true;
    }
    ImGui::InputText(u8"新增骨名", name, sizeof(name));
    MmdBoneChoice(u8"父级", parent, u8"无 / 插入时继承原父级");
    MmdBoneChoice(u8"静止位置参考", anchor, u8"源模型原点");
    ImGui::InputFloat3(u8"位置偏移（MMD 单位）", offset);
    MmdBoneChoice(u8"插入到此骨骼之前", before, u8"无（独立子骨）");
    MmdBoneChoice(u8"附加变换来源", grant, u8"无");
    ImGui::Checkbox(u8"继承旋转", &rotation);
    ImGui::SameLine();
    ImGui::Checkbox(u8"继承位移", &position);
    ImGui::SliderFloat(u8"附加比例", &weight, -1, 1, "%.2f");
    if (ImGui::Button(editing >= 0 ? u8"更新此追加骨" : u8"加入追加列表")) {
      mmd::ExtraBone b;
      b.name = mmd::Name(name);
      b.parent = parent;
      b.anchor = anchor;
      b.before = before;
      b.grant = grant;
      b.offset = {offset[0], offset[1], offset[2]};
      b.grantWeight = weight;
      b.grantRotation = rotation;
      b.grantPosition = position;
      if (editing >= 0 && size_t(editing) < draft.extra.size())
        draft.extra[editing] = b;
      else
        draft.extra.push_back(b);
      dirty = true;
      editing = -1;
    }
    ImGui::SameLine();
    if (ImGui::Button(u8"新建")) {
      name[0] = 0;
      editing = -1;
      parent.clear();
      anchor.clear();
      before.clear();
      grant.clear();
      offset[0] = offset[1] = offset[2] = 0;
      weight = 1;
      rotation = position = false;
    }
    ImGui::TextWrapped(u8"独立骨需映射到角色部位；插入父级会带动后代。追加后先"
                       u8"应用，再继续添加子骨或绑定轨道。");
    ImGui::TreePop();
  }
  if (ImGui::TreeNode(u8"动作轨道 → 源骨骼")) {
    static std::string bone;
    static char filter[241] = {};
    MmdBoneChoice(u8"目标源骨骼", bone, u8"选择要驱动的源骨骼");
    if (!bone.empty()) {
      auto it = draft.tracks.find(bone);
      const char *label = it == draft.tracks.end() ? u8"默认：同名轨道"
                          : it->second.empty()     ? u8"忽略此骨骼的动作轨道"
                                                   : it->second.c_str();
      ImGui::InputText(u8"筛选动作轨道", filter, sizeof(filter));
      if (ImGui::BeginCombo(u8"使用动作轨道", label)) {
        if (ImGui::Selectable(u8"默认：同名轨道", it == draft.tracks.end())) {
          draft.tracks.erase(bone);
          dirty = true;
        }
        if (ImGui::Selectable(u8"忽略此骨骼的动作轨道")) {
          draft.tracks[bone] = "";
          dirty = true;
        }
        for (const auto &kv : m.clip.bones) {
          if (filter[0] && kv.first.find(filter) == std::string::npos)
            continue;
          if (ImGui::Selectable(kv.first.c_str())) {
            draft.tracks[bone] = kv.first;
            dirty = true;
          }
        }
        ImGui::EndCombo();
      }
    }
    ImGui::TextWrapped(
        u8"把特殊命名的轨道指定给源骨骼；不会按文件名或说明文件自动识别。");
    for (const auto &kv : draft.tracks)
      ImGui::TextWrapped("%s -> %s",
                         kv.second.empty() ? u8"忽略" : kv.second.c_str(),
                         kv.first.c_str());
    ImGui::TreePop();
  }
  if (ImGui::TreeNode(u8"源骨骼 → 角色部位")) {
    auto defaults = draft;
    defaults.roles.clear();
    auto resolved = mmd::AdaptedRoles(defaults);
    ImGui::BeginChild("##mmdroles", ImVec2(0, 240), true);
    for (int role = 0; role < 55; ++role) {
      ImGui::PushID(role);
      std::string fallback =
          resolved.count(role) ? resolved.at(role) : mmd::RoleNames()[role];
      auto it = draft.roles.find(role);
      std::string label = it == draft.roles.end()
                              ? std::string(u8"默认：") + fallback
                          : it->second.empty() ? u8"不控制此部位"
                                               : it->second;
      ImGui::TextUnformatted(HumanBoneName(role));
      ImGui::SetNextItemWidth(-1);
      if (ImGui::BeginCombo("##mappedrole", label.c_str())) {
        if (ImGui::Selectable((std::string(u8"默认：") + fallback).c_str())) {
          draft.roles.erase(role);
          dirty = true;
        }
        if (ImGui::Selectable(u8"不控制此部位")) {
          draft.roles[role] = "";
          dirty = true;
        }
        for (const auto &b : m.rig.bones)
          if (ImGui::Selectable(b.name.c_str())) {
            draft.roles[role] = b.name;
            dirty = true;
          }
        ImGui::EndCombo();
      }
      ImGui::PopID();
    }
    ImGui::EndChild();
    ImGui::TreePop();
  }
  ImGui::EndDisabled();
}
