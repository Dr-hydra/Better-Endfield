#pragma once
#include "config.h"
#include "game/mmd_player.h"
#include "editor/panel_mmd_adaptation.h"
#if POSER_ENABLE_XXMI_BRIDGE
#include "editor/panel_mod_bridge.h"
#endif
#include "imgui.h"

static void DrawMmdFile(const std::string &path) {
  if (path.empty())
    return;
  auto slash = path.find_last_of("/\\");
  ImGui::TextWrapped("%s", slash == std::string::npos ? path.c_str() : path.c_str() + slash + 1);
  if (ImGui::IsItemHovered())
    ImGui::SetTooltip("%s", path.c_str());
}

static void DrawMmdHotkeyHints() {
  char keys[4][48] = {};
  for (int i = 0; i < 4; ++i)
    HotkeyDisplay(g_mmdHotkeyVK[i], g_mmdHotkeyCtrl[i], keys[i], sizeof(keys[i]));
  ImGui::TextWrapped(u8"播放/继续：%s    暂停：%s", keys[0], keys[1]);
  ImGui::TextWrapped(u8"停止并恢复：%s    重置动作：%s", keys[2], keys[3]);
  ImGui::TextDisabled(u8"快捷键当前控制：%s", g_mmdSquadBridge.hotkeyTarget && g_mmdSquadBridge.hotkeyTarget()
                                                  ? u8"多人播放器"
                                                  : u8"单人播放器");
}

static void DrawMmdAmplitude() {
  if (!ImGui::CollapsingHeader(u8"动作幅度（全身 / 分部位）"))
    return;
  auto &a = g_mmd.amplitude;
  bool changed = false;
  auto slider = [&](const char *label, float &value) {
    float percent = value * 100.f;
    bool edit = ImGui::SliderFloat(label, &percent, 0, 200, "%.0f%%");
    if (edit)
      value = mmd::MotionAmplitude::safe(percent / 100.f);
    return edit;
  };
  changed |= slider(u8"全身幅度", a.master);
  if (ImGui::Button(u8"全部幅度复位 100%")) {
    a = {};
    changed = true;
  }
  if (ImGui::TreeNode(u8"分部位调整")) {
    static bool linked = false;
    if (ImGui::Checkbox(u8"左右联动（开启时以左侧为准）", &linked) && linked) {
      for (int i = 2; i < int(mmd::MotionPart::Count); i += 2)
        a.parts[i + 1] = a.parts[i];
      changed = true;
    }
    const char *labels[] = {u8"躯干",        u8"头颈", u8"左臂", u8"右臂",        u8"左手 / 手指",
                            u8"右手 / 手指", u8"左腿", u8"右腿", u8"左脚 / 脚尖", u8"右脚 / 脚尖"};
    for (int i = 0; i < int(mmd::MotionPart::Count); ++i) {
      ImGui::PushID(i);
      bool edited = slider(labels[i], a.parts[i]);
      ImGui::SameLine();
      if (ImGui::SmallButton(u8"复位")) {
        a.parts[i] = 1;
        edited = true;
      }
      if (edited && linked && i >= 2)
        a.parts[i ^ 1] = a.parts[i];
      changed |= edited;
      ImGui::PopID();
    }
    ImGui::TreePop();
  }
  ImGui::TextWrapped(u8"全身与部位强度相乘；100% 保留原动作。");

  if (changed)
    MmdApplyFrame();
}

static void DrawMmdCloth() {
  if (!ImGui::CollapsingHeader(u8"衣物物理"))
    return;
  auto &m = g_mmd;
  if (ImGui::Checkbox(u8"冻结头发 / 衣物", &m.freezeCloth) && m.session.active && m.session.bodyOwned) {
    g_freezeAccessories = m.freezeCloth;
    if (m.freezeCloth)
      CaptureAccessorySnapshot();
    SetAllPhysicsEnabled(!m.freezeCloth, true);
  }
  s_collisionInspect.store(true);
  const auto ui = CollisionGetUi();
  if(g_mmd.timeline.clockHeld) {
    if(g_clothPlaybackGate.State()==eiem_playback::Preparation::Failed)
      ImGui::TextWrapped(u8"衣物准备失败，动作与音乐已保持。可停止重试，或关闭服装增强。%s",s_ClothActorRequest.Get().preparationReason);
    else ImGui::TextWrapped(u8"正在准备衣物，动作与音乐将在就绪后同步开始。");
  }
  bool enabled = s_clothAutoEnabled.load();
  if (ImGui::Checkbox(u8"服装碰撞增强", &enabled))
    ClothBoneQueueCommand(ui.session, !enabled);

  if (g_mmd.freezeCloth)
    ImGui::TextWrapped(u8"衣物已冻结：取消“冻结头发 / 衣物”后启用动态模拟。");
  else if (ui.boneRestoring || s_cloth.releasing)
    ImGui::TextWrapped(u8"正在恢复原有衣物，请稍候。");
  else if (ui.autoPreparing)
    ImGui::TextWrapped(u8"正在准备当前服装，期间保留原有物理。");
  else if (s_cloth.failed)
    ImGui::TextWrapped(u8"衣物校验未通过，已停止调整。详细原因见日志。");
  else if (s_cloth.active) {
    const int applied =
        ui.authoredApplied + ui.autoConnectionsApplied + ui.autoSkinApplied + ui.autoPartialApplied;
    ImGui::Text(u8"已增强 %d 个部位；保留原有物理 %d 个部位", applied, ui.autoPreserved);
    if (enabled && !applied && !ui.boneBusy)
      ImGui::TextWrapped(u8"暂无可用增强，使用角色原有物理。");
  } else
    ImGui::TextDisabled(u8"开始播放后匹配服装");
  const bool ribbonBusy = g_mmd.session.active || ui.boneBusy || s_cloth.active || s_cloth.releasing;
  ImGui::BeginDisabled(ribbonBusy);
  float damping = s_clothRibbonDamping.load() * 100.f;
  if (ImGui::SliderFloat(u8"飘带减振", &damping, 0, 100, "%.0f%%", ImGuiSliderFlags_AlwaysClamp))
    s_clothRibbonDamping.store(damping * .01f);
  ImGui::EndDisabled();
  ImGui::TextWrapped(u8"停止后调整减振，下次播放生效；默认 30%。");
  if (ImGui::TreeNode(u8"高级：碰撞体尺寸")) {
    int geometry = s_collisionGeometry.load();
    if (ImGui::Combo(u8"尺寸模式", &geometry, u8"腿根半径补偿\0游戏原始尺寸（默认）\0")) {
      s_collisionGeometry.store(geometry);
      s_skirtDirty.store(true);
    }
    if (geometry == 0) {
      float hip = s_skirtHipRadiusDelta.load();
      if (ImGui::SliderFloat(u8"腿根半径补偿", &hip, 0, .25f, "%.3f", ImGuiSliderFlags_AlwaysClamp)) {
        s_skirtHipRadiusDelta.store(hip);
        s_skirtDirty.store(true);
      }
    }
    ImGui::TreePop();
  }
  ImGui::TextWrapped(u8"服装变形异常时关闭增强；不能保证消除全部穿模。");
}
static bool DrawMmdCameraSettings(double seconds, float targetHeight) {
  auto &m = g_mmd;
  auto &s = m.cameraSettings;
  const auto &keys = MmdCameraKeys();
  bool changed = false;
  int origin = int(s.origin);
  if (ImGui::Combo(u8"镜头原点", &origin, u8"固定播放起点\0跟随角色位移\0")) {
    s.origin = mmd::CameraOrigin(origin);
    changed = true;
  }
  ImGui::TextWrapped(u8"动作自带跟拍时通常使用固定起点；跟随模式会额外叠加角色位移。");
  if (s.origin == mmd::CameraOrigin::Follow)
    changed |= ImGui::Checkbox(u8"跟随上下起伏", &s.followVertical);
  changed |= ImGui::SliderFloat3(u8"偏移（左右／上下／前后）", &s.offset.x, -3, 3, "%.3f");
  changed |= ImGui::Checkbox(u8"按角色身高适配构图", &s.autoHeight);
  changed |= ImGui::SliderFloat(u8"构图整体倍率", &s.heightScale, .25f, 3, "%.2f");
  changed |= ImGui::SliderFloat(u8"镜头距离倍率", &s.distanceScale, .1f, 3, "%.2f");
  if (ImGui::TreeNode(u8"高级构图与切镜")) {
    changed |= ImGui::Checkbox(u8"跟随角色高度修正", &s.followCorrection);
    if (s.autoHeight) {
      if (targetHeight > 0)
        ImGui::TextDisabled(u8"校准头脚高度 %.3f m", targetHeight);
      else
        ImGui::TextWrapped(u8"缺少可用身高校准，暂用下方单位比例。完成身体校准后自动生效。");
      changed |= ImGui::SliderFloat(u8"参考头脚高度（0 = 源骨架）", &s.referenceHeight, 0, 40, "%.2f");
    }
    ImGui::BeginDisabled(s.autoHeight && targetHeight > 0);
    changed |= ImGui::Checkbox(u8"单位比例跟随动作", &s.linkScale);
    if (!s.linkScale)
      changed |= ImGui::SliderFloat(u8"镜头单位比例", &s.scale, .001f, .3f, "%.4f");
    ImGui::EndDisabled();
    changed |= ImGui::SliderFloat(u8"镜头整体朝向", &s.yaw, -180, 180, "%.1f deg");
    changed |= ImGui::SliderFloat(u8"视角偏移", &s.fovOffset, -60, 60, "%.1f deg");
    changed |= ImGui::DragFloat(u8"镜头时间偏移（秒）", &s.timeOffset, .01f, -600, 600, "%.2f",
                                ImGuiSliderFlags_AlwaysClamp);
    ImGui::TextDisabled(u8"正值延后镜头，负值提前；动作和音乐时间不变。");
    int cuts = int(s.cutMode);
    if (ImGui::Combo(u8"切镜方式", &cuts, u8"相邻关键帧硬切（兼容）\0连续插值（逐帧镜头）\0手动指定切镜\0")) {
      s.cutMode = mmd::CameraCutMode(cuts);
      changed = true;
    }
    if (s.cutMode == mmd::CameraCutMode::Manual && !keys.empty()) {
      auto n = mmd::Upper(keys, mmd::CameraFrame(seconds, s));
      const auto frame = keys[(std::min)(n, keys.size() - 1)].frame;
      ImGui::Text(u8"当前段末尾：镜头帧 %u", frame);
      if (ImGui::Button(u8"此处切镜") && s.cutFrames.size() < 4096) {
        s.cutFrames.push_back(frame);
        std::sort(s.cutFrames.begin(), s.cutFrames.end());
        s.cutFrames.erase(std::unique(s.cutFrames.begin(), s.cutFrames.end()), s.cutFrames.end());
        changed = true;
      }
      ImGui::SameLine();
      if (ImGui::Button(u8"取消此处切镜")) {
        s.cutFrames.erase(std::remove(s.cutFrames.begin(), s.cutFrames.end(), frame), s.cutFrames.end());
        changed = true;
      }
      if (ImGui::Button(u8"清空切镜点")) {
        s.cutFrames.clear();
        changed = true;
      }
      ImGui::SameLine();
      ImGui::Text(u8"已标记 %zu 处", s.cutFrames.size());
    }
    ImGui::TreePop();
  }
  if (ImGui::Button(u8"复位镜头调整")) {
    s = {};
    changed = true;
  }
  ImGui::SameLine();
  if (ImGui::Button(u8"保存此角色＋镜头设置"))
    m.cameraPresets.save(MmdConfigDirectory() / L"camera-settings.json", s);
  ImGui::TextWrapped("%s", m.cameraPresets.status.c_str());
  ImGui::TextWrapped("%s", mmd_camera::status.load());
  if (changed)
    MmdUpdateDuration();
  return changed;
}
static void DrawMmdCamera() {
  if (!ImGui::CollapsingHeader(u8"MMD 镜头"))
    return;
  auto &m = g_mmd;
  auto &s = m.cameraSettings;
  const auto &keys = MmdCameraKeys();
  ImGui::BeginDisabled(m.loading || m.session.active);
  if (ImGui::Button(u8"选择镜头 VMD"))
    MmdBeginLoad(6);
  ImGui::SameLine();
  if (ImGui::Button(u8"移除镜头")) {
    m.cameraFile.clear();
    m.cameraTrack.clear();
    m.clip.cameras.clear();
    mmd::Recount(m.clip);
    MmdCameraTrackChanged();
    MmdUpdateDuration();
    mmd_camera::Stop();
  }
  ImGui::EndDisabled();
  if (m.session.active)
    ImGui::TextDisabled(u8"停止并恢复后可更换镜头文件");
  if (keys.empty()) {
    ImGui::TextWrapped(u8"选择独立镜头 VMD，或打开包含镜头轨道的动作 VMD。也支持只播放镜头。");
    return;
  }
  DrawMmdFile(m.cameraFile.empty() ? m.file : m.cameraFile);
  ImGui::Text(u8"镜头关键帧 %zu / %.2f 秒", keys.size(), keys.back().frame / 30.0);
  bool changed = ImGui::Checkbox(u8"随动作播放镜头", &s.enabled);
  changed |= DrawMmdCameraSettings(m.timeline.seconds, m.session.active ? m.session.cameraHeight
                                                                        : mmd::CameraTargetHeight(m.profile));
  if (changed)
    MmdPublishCamera();
  if (!mmd_camera::ready)
    ImGui::TextWrapped(u8"相机接口尚未就绪，身体动作仍可播放。");
  else if (mmd_camera::request.active && MmdNow() - mmd_camera::lastCallback > 2)
    ImGui::TextWrapped(u8"等待游戏相机更新；尚未确认镜头实际生效。");
}
static void DrawMmdCountdownOptions(bool busy) {
  ImGui::BeginDisabled(busy);
  ImGui::Checkbox(u8"播放倒计时",&g_mmd.countdownEnabled);
  if(g_mmd.countdownEnabled) {
    ImGui::SameLine();ImGui::SetNextItemWidth(120);
    ImGui::SliderInt(u8"秒##countdown",&g_mmd.countdownSeconds,1,10);
  }
  ImGui::EndDisabled();
}
static void DrawMmdPanel() {
  auto &m = g_mmd;
  if (!m.show)
    return;
  const bool wide = ImGui::GetIO().DisplaySize.x >= 1630;
  ImGui::SetNextWindowPos(ImVec2(wide ? 1130.f : 380.f, wide ? 10.f : 370.f), PanelPositionCondition());
  ImGui::SetNextWindowSize(ImVec2(470, 520), ImGuiCond_FirstUseEver);
  if (!ImGui::Begin(u8"MMD 播放器", &m.show, g_pinPanels ? ImGuiWindowFlags_NoMove : 0)) {
    ImGui::End();
    return;
  }
  if (MmdSquadBusy()) {
    ImGui::TextWrapped(u8"多人播放器正在控制小队，请在多人面板暂停或停止。");
    ImGui::End();
    return;
  }
  try {
    DrawMmdHotkeyHints();
    ImGui::Separator();
    ImGui::BeginDisabled(m.loading || m.session.active);
    if (ImGui::Button(u8"打开 VMD"))
      MmdBeginLoad(0);
    ImGui::SameLine();
    if (ImGui::Button(u8"追加口型 / 表情 / 眼神"))
      MmdBeginLoad(1);
    ImGui::EndDisabled();
    if (m.loading)
      ImGui::TextDisabled(u8"正在读取文件…");
    DrawMmdFile(m.file);
    ImGui::BeginDisabled(!MmdHasContent() || m.loading || m.preview);
    if (ImGui::Button(m.timeline.state == mmd::PlayState::Playing ? u8"暂停" : u8"播放")) {
      MmdPlaybackCommand(m.timeline.state == mmd::PlayState::Playing ? 1 : 0, false);
    }
    ImGui::SameLine();
    if (ImGui::Button(u8"停止并恢复"))
      MmdPlaybackCommand(2, false);
    ImGui::SameLine();
    if (ImGui::Button(u8"重置动作"))
      MmdPlaybackCommand(3, false);
    ImGui::SameLine();
    if (ImGui::SmallButton("<")) {
      MmdSeekOrStart(m.timeline.seconds - 1. / 30);
    }
    ImGui::SameLine();
    if (ImGui::SmallButton(">")) {
      MmdSeekOrStart(m.timeline.seconds + 1. / 30);
    }
    float seconds = float(m.timeline.seconds);
    ImGui::SetNextItemWidth(-1);
    if (ImGui::SliderFloat(u8"##mmdtime", &seconds, 0, float(m.timeline.duration), "%.2f s")) {
      MmdSeekOrStart(seconds);
    }
    ImGui::Text(u8"帧 %.1f / %.0f", m.timeline.seconds * 30, m.timeline.duration * 30);
    if (m.session.active && !m.preview)
      ImGui::TextDisabled(m.timeline.state == mmd::PlayState::Playing ? u8"正在播放"
                          : m.timeline.seconds >= m.timeline.duration ? u8"已到末帧，保持姿态"
                                                                      : u8"已暂停，保持姿态");
    float speed = float(m.timeline.speed);
    if (ImGui::SliderFloat(u8"播放速度", &speed, .25f, 2.f, "%.2fx")) {
      m.timeline.tick(MmdNow());
      m.timeline.speed = speed;
      MmdSyncAudio();
    }
    DrawMmdCountdownOptions(m.session.active||s_mmdStartRequest.active);
    ImGui::Checkbox(u8"循环播放", &m.timeline.loop);
    ImGui::SameLine();
    ImGui::Checkbox(u8"原地播放", &m.inPlace);
    ImGui::EndDisabled();
    if (MmdOwnsPose()) {
      ImGui::TextDisabled(u8"动作控制中；暂停后可在姿态库保存当前身体姿态");
      if (!m.clip.morphs.empty())
        ImGui::TextDisabled(((m.faceSettings.uniform() || s_faceHierarchy.ready) &&
                             (!m.faceSettings.uses(face_mixing::Driver::Character) ||
                              s_characterBinding.ready || m.faceSettings.fallback) &&
                             SMCSectionReady())
                                ? u8"表情系统已就绪"
                                : u8"表情尚未就绪或骨骼不匹配，身体动作继续播放");
    }
    ImGui::TextWrapped("%s", m.status.c_str());
    if (m.preview) {
      ImGui::TextWrapped(u8"手动校准中：用骨骼面板修正 T 姿，再保存或取消。");
      if (ImGui::Button(u8"确认保存校准"))
        MmdConfirmCalibration();
      ImGui::SameLine();
      if (ImGui::Button(u8"取消校准"))
        MmdStop();
    }
    ImGui::Separator();
    ImGui::BeginChild("##mmd-options-scroll", ImVec2(0, 0), false);
    if (ImGui::BeginTabBar("##mmd-options")) {
      if (ImGui::BeginTabItem(u8"动作")) {
        ImGui::BeginDisabled(m.loading || m.preview);
        int ikMode = int(m.ikMode);
        if (ImGui::Combo(u8"动作 IK", &ikMode, u8"跟随动作\0强制开启\0强制关闭\0")) {
          m.ikMode = static_cast<mmd::IkMode>(ikMode);
          MmdApplyFrame();
        }
        if (ImGui::IsItemHovered())
          ImGui::SetTooltip(
              u8"手动覆盖脚部、脚尖及 PMX 的 "
              u8"IK；暂停时也会立即更新姿态。\n关闭后按骨骼旋转播放；开启后使用所选骨架的 IK 目标。");
        if (ImGui::Checkbox(u8"按角色腿长自动适配位移", &m.autoScale) && m.autoScale && m.session.active)
          m.scale = m.mapper.suggestedScale;
        if (!m.autoScale)
          ImGui::SliderFloat(u8"位移比例", &m.scale, .001f, .3f, "%.4f");
        ImGui::SliderFloat(u8"高度修正", &m.height, -1, 1, "%.3f");
        ImGui::Checkbox(u8"地形跟随（坡面 / 台阶）", &m.terrain.enabled);
        if (m.terrain.enabled) {
          ImGui::SliderFloat(u8"贴地强度", &m.terrain.strength, 0, 1, "%.2f", ImGuiSliderFlags_AlwaysClamp);
          ImGui::TextWrapped("%s", m.session.terrain.status);
        }
        DrawMmdAmplitude();
        DrawMmdCloth();
        ImGui::EndDisabled();
        ImGui::EndTabItem();
      }
      if (ImGui::BeginTabItem(u8"镜头与音乐")) {
        DrawMmdCamera();
        if (ImGui::CollapsingHeader(u8"音乐同步")) {
          ImGui::BeginDisabled(m.loading || m.session.active);
          if (ImGui::Button(u8"选择音乐"))
            MmdBeginLoad(5);
          ImGui::SameLine();
          if (ImGui::Button(u8"移除音乐")) {
            m.audio.setClip({});
            m.musicFile.clear();
            m.musicError.clear();
          }
          ImGui::EndDisabled();
          if (m.session.active)
            ImGui::TextDisabled(u8"停止并恢复后可更换音乐");
          if (m.audio.clip()) {
            DrawMmdFile(m.musicFile);
            ImGui::Text(u8"音乐长度：%.2f 秒", m.audio.clip()->duration());
            if (ImGui::Checkbox(u8"随动作播放音乐", &m.musicEnabled)) {
              m.musicError.clear();
              MmdSyncAudio();
            }
            if (ImGui::SliderFloat(u8"音乐音量", &m.musicVolume, 0, 1, "%.2f"))
              MmdSyncAudio();
            if (ImGui::InputFloat(u8"音乐偏移（秒）", &m.musicOffset, .01f, .1f, "%.3f")) {
              if (!std::isfinite(m.musicOffset))
                m.musicOffset = 0;
              m.musicOffset = mmd::Clamp(m.musicOffset, -600.f, 600.f);
              m.audio.close();
              MmdSyncAudio();
            }
            ImGui::TextWrapped(u8"正值延后音乐，负值提前；变速会同时改变音高。");
          } else
            ImGui::TextWrapped(u8"手动选择 WAV / MP3 / M4A 等音频；无需音乐也可播放动作。");
          if (!m.musicError.empty())
            ImGui::TextWrapped("%s", m.musicError.c_str());
        }
        ImGui::EndTabItem();
      }
      if (ImGui::BeginTabItem(u8"表情")) {
        if (ImGui::CollapsingHeader(u8"角色表情与强度", ImGuiTreeNodeFlags_DefaultOpen)) {
          auto &settings = m.faceSettings;
          if (m.characterFace) {
            ImGui::Text(u8"当前角色校准：%s", m.characterFace->label.c_str());
            if (s_characterBinding.ready)
              ImGui::Text(u8"可用表情 %d / %d", s_characterBinding.usableCount,
                          int(m.characterFace->morphs.size()));
            else
              ImGui::TextWrapped("%s", s_characterBinding.status.empty() ? u8"等待当前角色中性脸"
                                                                         : s_characterBinding.status.c_str());
          } else
            ImGui::TextWrapped(u8"当前角色没有 MMD 表情校准。");
          if (m.faceLibraryLoading)
            ImGui::TextDisabled(u8"正在读取角色校准…");
          if (!m.faceLibraryError.empty())
            ImGui::TextWrapped(u8"校准读取失败，已保留原数据：%s", m.faceLibraryError.c_str());
          ImGui::BeginDisabled(MmdOwnsPose() || m.faceLibraryLoading);
          if (ImGui::SmallButton(u8"重新读取校准"))
            MmdReloadCharacterFaces();
          ImGui::EndDisabled();
          if (ImGui::Checkbox(u8"专属校准缺失时使用固定映射", &settings.fallback)) {
            MmdSaveFaceSettings();
            MmdReport();
          }

          float percent = settings.strength * 100;
          if (ImGui::SliderFloat(u8"整体表情强度", &percent, 0, 200, "%.0f%%"))
            settings.strength = percent * .01f;
          if (ImGui::IsItemDeactivatedAfterEdit())
            MmdSaveFaceSettings();
          ImGui::SameLine();
          if (ImGui::SmallButton(u8"复位全部强度")) {
            settings.strength = 1;
            settings.gain.fill(1);
            MmdSaveFaceSettings();
          }
          if (ImGui::BeginTable("##faceregions", 3, ImGuiTableFlags_SizingStretchProp)) {
            ImGui::TableSetupColumn(u8"部位", 0, .65f);
            ImGui::TableSetupColumn(u8"映射方式", 0, 1.7f);
            ImGui::TableSetupColumn(u8"独立强度", 0, 1.f);
            ImGui::TableHeadersRow();
            for (int r = 0; r < face_mixing::RegionCount; ++r) {
              ImGui::PushID(r);
              ImGui::TableNextRow();
              ImGui::TableNextColumn();
              ImGui::TextUnformatted(face_mixing::Label(r));
              ImGui::TableNextColumn();
              ImGui::SetNextItemWidth(-1);
              int mode = int(settings.driver[r]);
              if (ImGui::Combo("##source", &mode, u8"角色专属映射\0固定表情映射\0关闭\0")) {
                settings.driver[r] = static_cast<face_mixing::Driver>(mode);
                MmdSaveFaceSettings();
                MmdReport();
              }
              ImGui::TableNextColumn();
              ImGui::SetNextItemWidth(-1);
              float regionPercent = settings.gain[r] * 100;
              if (ImGui::SliderFloat("##strength", &regionPercent, 0, 200, "%.0f%%"))
                settings.gain[r] = regionPercent * .01f;
              if (ImGui::IsItemDeactivatedAfterEdit())
                MmdSaveFaceSettings();
              ImGui::PopID();
            }
            ImGui::EndTable();
          }
          if (ImGui::SmallButton(u8"全部使用角色专属映射")) {
            settings.driver.fill(face_mixing::Driver::Character);
            MmdSaveFaceSettings();
            MmdReport();
          }
          ImGui::TextWrapped(
              u8"整体和部位强度可以在播放或暂停时调整；可在表情面板的“眼睛朝向”覆盖动作眼神。");
        }
        if (ImGui::CollapsingHeader(u8"手动绑定（高级）")) {
          static int editSource = 0;
          ImGui::Combo(u8"编辑映射表", &editSource, u8"角色专属映射\0固定表情映射\0");
          bool native = editSource == 1;
          ImGui::TextWrapped(u8"角色专属映射按角色分别保存。可将动作中的自定义名称绑定到该角色已有表情。");
          ImGui::BeginDisabled(MmdOwnsPose() || (!native && !m.characterFace));
          bool changed = false;
          for (auto &kv : m.morphMap) {
            int &slider = native ? kv.second.nativeSlider : kv.second.slider;
            float &gain = native ? kv.second.nativeGain : kv.second.gain;
            ImGui::PushID(kv.first.c_str());
            ImGui::TextUnformatted(kv.first.c_str());
            ImGui::SetNextItemWidth(185);
            bool valid =
                slider >= 0 && (native ? slider < SMCSliderCount()
                                       : m.characterFace && slider < int(m.characterFace->morphs.size()));
            const char *label = !valid   ? u8"未指定 / 不支持"
                                : native ? SMCSliderLabel(slider)
                                         : m.characterFace->morphs[slider].name.c_str();
            if (ImGui::BeginCombo("##target", label)) {
              if (ImGui::Selectable(u8"未指定 / 不支持", slider < 0)) {
                slider = -1;
                changed = true;
              }
              int count = native            ? SMCSliderCount()
                          : m.characterFace ? int(m.characterFace->morphs.size())
                                            : 0;
              for (int i = 0; i < count; ++i) {
                if (!native && !m.characterFace->morphs[i].supported)
                  continue;
                const char *name = native ? SMCSliderLabel(i) : m.characterFace->morphs[i].name.c_str();
                if (ImGui::Selectable(name, slider == i)) {
                  slider = i;
                  changed = true;
                }
              }
              ImGui::EndCombo();
            }
            ImGui::SameLine();
            ImGui::SetNextItemWidth(130);
            changed |= ImGui::SliderFloat("##gain", &gain, 0, 2, "%.2f");
            if (!native && valid && m.characterFace->morphs[slider].residual > .1f)
              ImGui::TextDisabled(u8"此表情部分形状为近似");
            ImGui::PopID();
          }
          if (changed) {
            MmdSaveMappings(native);
            MmdReport();
          }
          ImGui::EndDisabled();
        }
        ImGui::EndTabItem();
      }
      if (ImGui::BeginTabItem(u8"高级")) {
        ImGui::TextDisabled(u8"播放时自动适配；动作异常时再调整以下设置。");
        if (ImGui::CollapsingHeader(u8"源骨架与参考姿态")) {
          ImGui::BeginDisabled(m.loading || m.session.active);
          if (ImGui::Button(u8"选择 PMX 骨架参考"))
            MmdBeginLoad(2);
          ImGui::SameLine();
          if (m.reference && ImGui::SmallButton(u8"使用内置骨架")) {
            m.reference = false;
            if (MmdApplyAdaptation(m.adaptation, m.sourcePreset)) {
              m.referenceFile.clear();
              m.scale = .08f;
            } else
              m.reference = true;
          }
          if (!m.reference) {
            int sourcePreset = m.sourcePreset;
            if (ImGui::Combo(u8"动作原始姿态", &sourcePreset, u8"标准 MMD / A 姿\0提取动作 / T 姿\0")) {
              MmdApplyAdaptation(m.adaptation, sourcePreset);
            }
            if (m.rig.name == "Extracted T-pose")
              ImGui::TextWrapped(u8"T 姿基准：手臂和手指平行展开，中心骨在原点。");
          }
          ImGui::EndDisabled();
          ImGui::TextWrapped(u8"骨架：%s", m.reference ? u8"PMX 参考" : MmdSourceRigLabel());
          if (m.reference)
            DrawMmdFile(m.referenceFile);
        }
        if (ImGui::CollapsingHeader(u8"角色校准")) {
          ImGui::TextWrapped("%s", m.calibrationStatus.c_str());
          ImGui::BeginDisabled(m.session.active || m.loading);
          if (ImGui::Button(u8"重新自动适配")) {
            m.profileRevision = -1;
            MmdPrepareProfile();
          }
          ImGui::SameLine();
          if (ImGui::Button(u8"备用手动 T 姿"))
            MmdBeginCalibration();
          ImGui::EndDisabled();
        }
        DrawMmdAdaptationPanel();
#if POSER_ENABLE_XXMI_BRIDGE
        DrawModBridgePanel();
#endif
        if (ImGui::CollapsingHeader(u8"诊断与导入报告")) {
          ImGui::TextDisabled(g_frameDiagnostics.gameDriven ? u8"动作更新：跟随游戏帧"
                                                            : u8"动作更新：独立计时（游戏帧回调未触发）");
          if (g_frameDiagnostics.source == 3)
            ImGui::TextDisabled(u8"帧来源：角色更新");
          if (g_frameDiagnostics.source == 4)
            ImGui::TextDisabled(u8"帧来源：游戏镜头更新");
          if (g_frameDiagnostics.source == 2)
            ImGui::TextDisabled(u8"帧来源：游戏渲染管线 SRP");
          ImGui::TextDisabled(u8"实际 %.1f Hz / 最长间隔 %.1f ms / 最大耗时 %.1f ms", g_frameDiagnostics.hz,
                              g_frameDiagnostics.maxGapMs, g_frameDiagnostics.maxCostMs);
          ImGui::Separator();
          if (m.session.active && !m.preview)
            ImGui::Text(u8"腿部 IK：左 %s / 右 %s", m.mapper.output.legIkActive[0] ? u8"开启" : u8"关闭",
                        m.mapper.output.legIkActive[1] ? u8"开启" : u8"关闭");
          ImGui::Text(u8"骨骼轨道 %zu / 表情轨道 %zu", m.clip.bones.size(), m.clip.morphs.size());
          ImGui::Text(u8"地形支撑脚 %d / 高度补偿 %.3f m", m.session.terrain.contacts,
                      m.session.terrain.rootOffset);
          ImGui::Text(u8"相机更新 %llu / 实际写入 %llu", (unsigned long long)mmd_camera::callbacks,
                      (unsigned long long)mmd_camera::applied);
          {
            if (m.report.empty())
              ImGui::TextDisabled(u8"没有发现未映射轨道");
            ImGui::BeginChild("##mmdreport", ImVec2(0, 130), true);
            for (auto &line : m.report)
              ImGui::TextWrapped("%s", line.c_str());
            ImGui::EndChild();
          }
        }
        ImGui::EndTabItem();
      }
      ImGui::EndTabBar();
    }
    ImGui::EndChild();
  } catch (const std::exception &e) {
    MmdStop();
    m.status = e.what();
  }
  ImGui::End();
}
