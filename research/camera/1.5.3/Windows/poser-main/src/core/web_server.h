#pragma once
// 独立 UI 方案：插件内置 localhost HTTP 服务器，浏览器访问 http://127.0.0.1:18923
// 获得独立的摆姿控制窗口（画布骨骼小人 + 滑条）。不受游戏内 HUD 的
// 焦点与合成问题。

#include <winsock2.h>
#include <ws2tcpip.h>
#pragma comment(lib, "ws2_32.lib")

#include <cstdio>
#include <cstring>
#include <string>

#include "nlohmann/json.hpp"
#include "config.h"
#include "user_agreement.h"
#include "game/skeleton.h"
#include "game/freeze.h"
#include "math/pose_file.h"
#include "editor/selection.h"
#include "editor/panel_library.h"

static int g_webPort = 18923;
static std::atomic<bool> g_webRunning = false;

// ---- 简易 HTTP 响应 ----
static void HttpReply(SOCKET c, const char *ctype, const std::string &body) {
  char head[512];
  int n = snprintf(head, sizeof(head),
                   "HTTP/1.1 200 OK\r\nContent-Type: %s\r\n"
                   "Access-Control-Allow-Origin: *\r\n"
                   "Content-Length: %d\r\nConnection: close\r\n\r\n",
                   ctype, (int)body.size());
  send(c, head, n, 0);
  send(c, body.data(), (int)body.size(), 0);
}

static void HttpJson(SOCKET c, const nlohmann::json &j) {
  HttpReply(c, "application/json; charset=utf-8", j.dump());
}

// ---- API：骨骼列表 ----
static nlohmann::json ApiBones() {
  nlohmann::json arr = nlohmann::json::array();
  for (int i = 0; i < s_humanBoneCount; i++) {
    nlohmann::json o;
    o["i"] = i;
    o["name"] = s_humanBones[i].name;
    o["locked"] = s_humanBones[i].locked;
    if (s_humanBones[i].transform) {
      Vec3 p = GetBoneWorldPos(s_humanBones[i].transform);
      o["x"] = p.x; o["y"] = p.y; o["z"] = p.z;
      Quat lr = GetBoneLocalRot(s_humanBones[i].transform);
      Vec3 lp = GetBoneLocalPos(s_humanBones[i].transform);
      o["lrx"] = lr.x; o["lry"] = lr.y; o["lrz"] = lr.z; o["lrw"] = lr.w;
      o["lpx"] = lp.x; o["lpy"] = lp.y; o["lpz"] = lp.z;
      int pi = FindTransformIndex(
          g_transform_get_parent
              ? Invoke(g_transform_get_parent, s_humanBones[i].transform)
              : nullptr);
      o["parent"] = pi;
    } else {
      o["parent"] = -1;
    }
    arr.push_back(o);
  }
  return arr;
}

// ---- 处理一个请求 ----
static void HandleRequestBody(SOCKET c, const std::string &path,
                          const std::string &body) {
  std::lock_guard<std::recursive_mutex> lock(g_poseMutex);
  if (!poser_agreement::Allowed() && path != "/api/status") {
    const std::string payload = nlohmann::json({{"ok", false}, {"err", "agreement_required"},
      {"message", u8"请在游戏内打开 Poser 面板，阅读并确认用户协议后使用插件。"}}).dump();
    char head[256];
    int n = snprintf(head, sizeof(head), "HTTP/1.1 403 Forbidden\r\nContent-Type: application/json; charset=utf-8\r\nAccess-Control-Allow-Origin: *\r\nContent-Length: %zu\r\nConnection: close\r\n\r\n", payload.size());
    send(c, head, n, 0); send(c, payload.data(), (int)payload.size(), 0); return;
  }
  const bool readOnly = path=="/" || path=="/index.html" || path=="/api/status" || path=="/api/mmd/status" || path=="/api/mmd/squad/status" || path=="/api/bones" || path=="/api/allbones" || path=="/api/face" || (path=="/api/pose" && body.empty());
  if(MmdOwnsPose() && !readOnly) {
    const char* payload="{\"ok\":false,\"err\":\"MMD playback owns the pose; stop playback before editing\"}";
    char head[256];int n=snprintf(head,sizeof(head),"HTTP/1.1 409 Conflict\r\nContent-Type: application/json\r\nAccess-Control-Allow-Origin: *\r\nContent-Length: %zu\r\nConnection: close\r\n\r\n",strlen(payload));
    send(c,head,n,0);send(c,payload,(int)strlen(payload),0);return;
  }

  if (path == "/" || path == "/index.html") {
    // 内嵌网页：画布骨骼小人 + 滑条 + 按钮
    extern const char *g_poserHtml;
    HttpReply(c, "text/html; charset=utf-8", g_poserHtml);
    return;
  }
  if (path == "/api/status") {
    HttpJson(c, {{"ok", true}, {"frozen", g_frozen},
                 {"agreement_required", !poser_agreement::Allowed()},
                 {"agreement_revision", poser_agreement::kRevision},
                 {"bones", s_humanBoneCount},
                 {"bones_rev", s_bonesRev},
                 {"selected", g_selectedBone},
                 {"freeze_accessories", g_freezeAccessories}});
    return;
  }
  if (path == "/api/bones") {
    HttpJson(c, {{"ok", true}, {"bones", ApiBones()}});
    return;
  }
  if (path == "/api/mmd/squad/status") {
    auto &s=g_squad;nlohmann::json slots=nlohmann::json::array();
    for(int i=0;i<4;++i) {
      auto &slot=s.slots[i];auto *a=s.actors[i].get();
      ClothActorScope clothScope(unsigned(i)+1);
      s_collisionInspect.store(true);
      const auto cloth=CollisionGetUi();
      slots.push_back({{"slot",i+1},{"enabled",slot.enabled},{"member",slot.member},
        {"ready",s.roster.members[i].animator!=nullptr},{"file",slot.file},{"status",slot.status},
        {"calibrated",slot.calibrated},{"calibration",slot.calibration},
        {"active",a!=nullptr},{"bones",a?a->bones.size():0},
        {"terrain_status",a?a->saved.terrain.status:""},{"terrain_root",a?a->saved.terrain.rootOffset:0},
        {"terrain_contacts",a?a->saved.terrain.contacts:0},
        {"cloth_active",s_cloth.active},{"cloth_failed",s_cloth.failed},
        {"cloth_preparing",cloth.autoPreparing},{"cloth_restoring",s_cloth.releasing||cloth.boneRestoring},
        {"cloth_authored",cloth.authoredApplied},{"cloth_connections",cloth.autoConnectionsApplied},
        {"cloth_skin",cloth.autoSkinApplied},{"cloth_partial",cloth.autoPartialApplied},
        {"cloth_preserved",cloth.autoPreserved},{"cloth_issue",cloth.boneIssue},
        {"face_ready",a&&a->face&&a->face->smcOwnershipVerified},
        {"offset",{slot.offset.x,slot.offset.y,slot.offset.z}}});
    }
    HttpJson(c,{{"active",s.active},{"pending",s.pending.active},{"loading",s.loading},
      {"state",int(s.timeline.state)},{"frame",s.timeline.seconds*30},{"last_frame",s.timeline.duration*30},
      {"status",s.status},{"roster_status",poser_squad::status},{"roster_valid",s.roster.valid},
      {"terrain_enabled",s.terrain.enabled},{"auto_scale",s.autoScale},{"gaze_camera",poser_gaze::motionLock},
      {"cloth_enhancement_enabled",s_clothSquadAutoEnabled.load()},
      {"origin",{s.anchor.origin.x,s.anchor.origin.y,s.anchor.origin.z}},{"slots",slots}});
    return;
  }
  if (path == "/api/mmd/status") {
    auto &m=g_mmd;
    s_collisionInspect.store(true);
    const auto enhancement=CollisionGetUi();
    nlohmann::json clothDetails=nlohmann::json::array();
    for(int n=0;n<s_cloth.count;++n) {
      const auto &i=s_cloth.instances[n];
      clothDetails.push_back({{"name",i.name},{"phase",int(i.startup.phase)},
        {"status",i.startup.reason},{"weight",i.last.weight},{"animation_pose_ratio",i.last.ratio},
        {"running",i.last.state.running},{"active",i.last.state.active},
        {"culled",i.last.state.culled},{"skirt",i.skirt}});
    }
    HttpJson(c, {{"active",MmdOwnsPose()},{"loading",m.loading},{"preview",m.preview},
      {"state",int(m.timeline.state)},{"frame",m.timeline.seconds*30.},
      {"last_frame",m.timeline.duration*30},{"speed",m.timeline.speed},{"loop",m.timeline.loop},
      {"camera_keys",MmdCameraKeys().size()},{"camera_file",m.cameraFile},
      {"camera_enabled",m.cameraSettings.enabled},{"camera_origin",int(m.cameraSettings.origin)},
      {"camera_offset",{m.cameraSettings.offset.x,m.cameraSettings.offset.y,m.cameraSettings.offset.z}},
      {"camera_ready",mmd_camera::ready},{"camera_status",std::string(mmd_camera::status.load())},
      {"camera_callback_age",mmd_camera::lastCallback < 0 ? -1.0 : MmdNow()-mmd_camera::lastCallback},
      {"camera_applied",mmd_camera::applied.load()},{"camera_restore_pending",!mmd_camera::desiredActive.load()&&mmd_camera::restorePending.load()},
      {"camera_sequence",mmd_camera::lastSequence.load()},{"camera_source_frame",mmd_camera::sourceFrame.load()},
      {"camera_repeated_samples",mmd_camera::repeatedFrames.load()},{"camera_driver_paused",mmd_camera::driverPaused.load()},
      {"camera_settings",mmd::WriteCameraSettings(m.cameraSettings)},
      {"hotkey_target",g_mmdSquadBridge.hotkeyTarget&&g_mmdSquadBridge.hotkeyTarget()?"squad":"single"},
      {"in_place",m.inPlace},{"scale",m.scale},{"status",m.status},
      {"auto_scale",m.autoScale},{"terrain_enabled",m.terrain.enabled},
      {"terrain_status",m.session.terrain.status},{"terrain_root",m.session.terrain.rootOffset},
      {"terrain_contacts",m.session.terrain.contacts},{"terrain_queries",m.session.terrain.queries},
      {"gaze_camera",poser_gaze::motionLock},{"gaze_strength",poser_gaze::motionStrength},
      {"cloth_enhancement",{{"enabled",s_clothAutoEnabled.load()},
        {"preparing",enhancement.autoPreparing},{"restoring",enhancement.boneRestoring},
        {"authored",enhancement.authoredApplied},{"connections",enhancement.autoConnectionsApplied},
        {"skin",enhancement.autoSkinApplied},{"partial",enhancement.autoPartialApplied},
        {"preserved",enhancement.autoPreserved},{"catalog",enhancement.catalogCount},
        {"source",enhancement.catalogSource},{"issue",enhancement.boneIssue},
        {"geometry",s_collisionGeometry.load()},{"ribbon_damping",s_clothRibbonDamping.load()},
        {"verbose_diagnostics",s_clothVerboseDiagnostics.load()}}},
      {"cloth_mode","native"},{"cloth_requested",s_clothRequested},
      {"cloth_active",s_cloth.active},{"cloth_failed",s_cloth.failed},
      {"cloth_restore_pending",s_cloth.releasing || (!s_clothRequested && s_cloth.active)},
      {"cloth_hip_radius",s_skirtHipRadiusDelta.load()},{"cloth_details",clothDetails},
      {"freeze_cloth",m.freezeCloth},
      {"calibration",m.calibrationStatus},
      {"character_ready",MmdCharacterReady()},
      {"source_rig",m.rig.name},{"source_preset",m.sourcePreset},
        {"ik_mode",int(m.ikMode)},{"pmx_reference",m.reference},
        {"motion_amplitude",mmd::AmplitudeJson(m.amplitude)},
      {"leg_ik_left",m.session.active && !m.preview && m.mapper.output.legIkActive[0]},
      {"leg_ik_right",m.session.active && !m.preview && m.mapper.output.legIkActive[1]},
      {"rig_bones",m.rig.bones.size()},
      {"adaptation",mmd::AdaptationJson(m.adaptation,m.sourcePreset,m.ikMode)},
      {"adaptation_file",m.adaptationFile},
      {"music_file",m.musicFile},{"music_enabled",m.musicEnabled},
      {"music_playing",m.audio.running()},{"music_offset",m.musicOffset},
      {"music_volume",m.musicVolume},{"music_error",m.musicError},
      {"music_duration",m.audio.clip() ? m.audio.clip()->duration() : 0},
      {"game_frame_sync",g_frameDiagnostics.gameDriven},
      {"frame_source",g_frameDiagnostics.source},
      {"game_thread",g_frameGameThreadId.load()},{"play_pending",s_mmdStartRequest.active},
      {"update_hz",g_frameDiagnostics.hz},{"max_gap_ms",g_frameDiagnostics.maxGapMs},
      {"max_update_ms",g_frameDiagnostics.maxCostMs},{"busy_skips",g_frameDiagnostics.busy},
      {"calibrated",m.profileAnimator == g_charAnimator &&
                       m.profileRevision == s_bonesRev && m.profile.valid()},
      {"model",CurrentCharModelKey()},{"file",m.file},{"report",m.report},
      {"hidden_props",m.session.active ? m.session.props.size() : 0},
      {"smc_ready",SMCSectionReady()},{"bone_tracks",m.clip.bones.size()},
      {"smc_owner_verified",s_smcOwnershipVerified},
      {"native_face_paused",s_smcAutomation.confirmed},
      {"face_mode","character"},{"face_fallback",m.faceSettings.fallback},
      {"face_strength",m.faceSettings.strength},{"face_regions",face_mixing::Write(m.faceSettings)["regions"]},
      {"face_profile",m.characterFace?m.characterFace->key:""},
      {"face_profile_ready",s_characterBinding.ready},{"face_profile_matched",s_characterBinding.matched},
      {"face_profile_usable",s_characterBinding.usableCount},{"face_profile_error",s_characterBinding.error},
      {"face_library_count",m.faceLibrary.size()},{"face_library_loading",m.faceLibraryLoading},
      {"face_library_error",m.faceLibraryError},
      {"morph_tracks",m.clip.morphs.size()}});
    return;
  }
  if(path=="/api/face") {
    nlohmann::json bones=nlohmann::json::array(),missing=nlohmann::json::array();
    nlohmann::json expressions=nlohmann::json::array(),native=nlohmann::json::array();
    const auto &face=s_motionFaceCurrent;
    if(face.profile)for(int i=0;i<int(face.profile->morphs.size());++i)
      if(face.expressions[i]!=0)expressions.push_back({{"name",face.profile->morphs[i].name},{"weight",face.expressions[i]}});
    for(int i=0;i<SMC_NUM_MOUTH+s_extraMorphCount;++i)
      if(face.weights[i]!=0||face.fallbackWeights[i]!=0)
        native.push_back({{"name",SMCSliderLabel(i)},{"weight",face.weights[i]},{"fallback",face.fallbackWeights[i]}});
    for(int i=0;i<int(s_faceNodes.size());++i) {
      const auto &b=s_faceNodes[i];auto v=b.neutral.position();
      bones.push_back({{"name",b.name},{"parent",b.parent},{"position",{v.x,v.y,v.z}},
        {"region",s_faceRegions[i]},{"matrix",b.neutral.m}});
      if(i<s_faceBoneCount) {
        const auto &rest=s_faceRestPose[i],&evaluated=s_faceBones[i];
        bones.back()["neutral_local"]={rest.px,rest.py,rest.pz};
        bones.back()["neutral_rotation"]={rest.rx,rest.ry,rest.rz,rest.rw};
        bones.back()["evaluated_local"]={evaluated.px,evaluated.py,evaluated.pz};
        bones.back()["evaluated_rotation"]={evaluated.rx,evaluated.ry,evaluated.rz,evaluated.rw};
      }
    }
    if(s_characterProfile)for(int i=0;i<int(s_characterBinding.slots.size());++i)
      if(s_characterBinding.slots[i]<0)missing.push_back(s_characterProfile->bones[i].name);
    HttpJson(c,{{"model",CurrentCharModelKey()},{"generation",s_faceGeneration},{"ready",s_characterBinding.ready},
      {"status",s_characterBinding.status},{"bones",bones},{"missing",missing},
      {"motion",{{"active",SMCMotionActive()},{"evaluated",s_faceBoneEvalOk},
        {"profile",face.profile?face.profile->key:""},{"settings",face_mixing::Write(face.settings)},
        {"expressions",expressions},{"native",native}}},
      {"gaze",{{"model",poser_gaze::editor.modelKey},{"pmx_reference",poser_gaze::binding.pmxReference},
        {"estimated_limits",poser_gaze::binding.estimatedLimits},{"settings",eye_gaze::WriteProfile(poser_gaze::ProfileFor())}}}});return;
  }
  if (path == "/api/allbones") {
    // 全骨骼（含手指/配饰等），供 Blender 桥接构建完整 Armature
    nlohmann::json arr = nlohmann::json::array();
    for (size_t i = 0; i < s_allBones.size(); i++) {
      nlohmann::json o;
      o["i"] = (int)i;
      o["name"] = s_allBones[i].name;
      o["parent"] = s_allBones[i].parentIdx;
      if (s_allBones[i].transform) {
        Vec3 p = GetBoneWorldPos(s_allBones[i].transform);
        o["x"] = p.x; o["y"] = p.y; o["z"] = p.z;
        Quat lr = GetBoneLocalRot(s_allBones[i].transform);
        Vec3 lp = GetBoneLocalPos(s_allBones[i].transform);
        o["lrx"] = lr.x; o["lry"] = lr.y; o["lrz"] = lr.z; o["lrw"] = lr.w;
        o["lpx"] = lp.x; o["lpy"] = lp.y; o["lpz"] = lp.z;
      }
      arr.push_back(o);
    }
    HttpJson(c, {{"ok", true}, {"rev", s_bonesRev}, {"bones", arr}});
    return;
  }
  if (path == "/api/select") {
    auto j = nlohmann::json::parse(body, nullptr, false);
    int i = j.value("i", -1);
    if (i >= 0 && i < s_humanBoneCount)
      SelectTransform(s_humanBones[i].transform, s_humanBones[i].name);
    HttpJson(c, {{"ok", true}});
    return;
  }
  if (path == "/api/freeze") {
    auto j = nlohmann::json::parse(body, nullptr, false);
    bool acc = j.value("accessories", g_freezeAccessories);
    if (acc != g_freezeAccessories) {
      g_freezeAccessories = acc;
      if (g_frozen) {
        if (acc) {
          if (s_accessoryChains.empty())
            RebuildAccessories();
          SetAllPhysicsEnabled(false);
        } else {
          SetAllPhysicsEnabled(true);
        }
      }
    }
    bool on = j.value("on", !g_frozen);
    if (on) {
      if (!g_frozen)
        FreezeCharacter();
    } else {
      if (g_frozen)
        UnfreezeCharacter();
    }
    HttpJson(c, {{"ok", true}, {"frozen", g_frozen},
                 {"freeze_accessories", g_freezeAccessories}});
    return;
  }
  if (path == "/api/tpose") {
    ApplyTPose();
    HttpJson(c, {{"ok", true}});
    return;
  }
  if (path == "/api/refresh") {
    RefreshCharacterBones();
    HttpJson(c, {{"ok", true}, {"bones", s_humanBoneCount}});
    return;
  }
  if (path == "/api/reset") {
    ApplyPoseSnapshot();
    HttpJson(c, {{"ok", true}});
    return;
  }
  if (path == "/api/setrot") {
    // 增量旋转：绕骨局部轴 {axis} 转 d 度
    auto j = nlohmann::json::parse(body, nullptr, false);
    int i = j.value("i", -1);
    int axis = j.value("axis", 0);
    float d = j.value("d", 0.0f);
    if (i >= 0 && i < s_humanBoneCount && !s_humanBones[i].locked) {
      const float kD2R = 3.14159265f / 180.0f;
      Vec3 axs[3] = {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}};
      Quat cur = GetBoneLocalRot(s_humanBones[i].transform);
      SetBoneLocalRot(s_humanBones[i].transform,
                      NormQ(Quat::AxisAngle(axs[axis], d * kD2R) * cur));
    }
    HttpJson(c, {{"ok", true}});
    return;
  }
  if (path == "/api/setpos") {
    auto j = nlohmann::json::parse(body, nullptr, false);
    int i = j.value("i", -1);
    if (i >= 0 && i < s_humanBoneCount && !s_humanBones[i].locked) {
      Vec3 p = GetBoneLocalPos(s_humanBones[i].transform);
      if (j.contains("x")) p.x = j["x"];
      if (j.contains("y")) p.y = j["y"];
      if (j.contains("z")) p.z = j["z"];
      SetBoneLocalPos(s_humanBones[i].transform, p);
    }
    HttpJson(c, {{"ok", true}});
    return;
  }
  if (path == "/api/setbonerot") {
    auto j = nlohmann::json::parse(body, nullptr, false);
    int i = j.value("i", -1);
    if (i >= 0 && i < s_humanBoneCount && !s_humanBones[i].locked &&
        j.contains("q")) {
      Quat q{0, 0, 0, 1};
      const auto &qq = j["q"];
      if (qq.is_array() && qq.size() >= 4) {
        q.x = qq[0].get<float>();
        q.y = qq[1].get<float>();
        q.z = qq[2].get<float>();
        q.w = qq[3].get<float>();
      }
      SetBoneLocalRot(s_humanBones[i].transform, NormQ(q));
    }
    HttpJson(c, {{"ok", true}});
    return;
  }
  if (path == "/api/pose") {
    auto j = nlohmann::json::parse(body, nullptr, false);
    if (j.contains("pose")) {
      int applied = 0;
      for (auto &e : j["pose"]) {
        if (!e.is_object())
          continue;
        void *tr = nullptr;
        bool locked = false;
        if (e.contains("n")) {
          // 按名字匹配全骨骼（Blender 桥接主路径，含手指/配饰）
          std::string nm = e["n"].get<std::string>();
          for (size_t k = 0; k < s_allBones.size(); k++) {
            if (strcmp(s_allBones[k].name, nm.c_str()) == 0) {
              tr = s_allBones[k].transform;
              for (int h = 0; h < s_humanBoneCount; h++) {
                if (strcmp(s_humanBones[h].name, nm.c_str()) == 0) {
                  locked = s_humanBones[h].locked;
                  break;
                }
              }
              break;
            }
          }
        } else if (e.contains("i")) {
          int i = e.value("i", -1);
          if (i >= 0 && i < s_humanBoneCount) {
            tr = s_humanBones[i].transform;
            locked = s_humanBones[i].locked;
          }
        }
        if (!tr || locked)
          continue;
        if (e.contains("q")) {
          Quat q{0, 0, 0, 1};
          const auto &qq = e["q"];
          if (qq.is_array() && qq.size() >= 4) {
            q.x = qq[0].get<float>();
            q.y = qq[1].get<float>();
            q.z = qq[2].get<float>();
            q.w = qq[3].get<float>();
          }
          SetBoneLocalRot(tr, NormQ(q));
        }
        if (e.contains("p")) {
          Vec3 p{0, 0, 0};
          const auto &pp = e["p"];
          if (pp.is_array() && pp.size() >= 3) {
            p.x = pp[0].get<float>();
            p.y = pp[1].get<float>();
            p.z = pp[2].get<float>();
          }
          SetBoneLocalPos(tr, p);
        }
        applied++;
      }
      HttpJson(c, {{"ok", true}, {"applied", applied}});
      return;
    }
    // 返回当前姿势（整姿，供 Blender 同步）：全骨骼（含手指/配饰），按名匹配
    nlohmann::json arr = nlohmann::json::array();
    for (size_t i = 0; i < s_allBones.size(); i++) {
      if (!s_allBones[i].transform)
        continue;
      Quat q = GetBoneLocalRot(s_allBones[i].transform);
      Vec3 p = GetBoneLocalPos(s_allBones[i].transform);
      arr.push_back({{"n", s_allBones[i].name},
                     {"q", {q.x, q.y, q.z, q.w}},
                     {"p", {p.x, p.y, p.z}}});
    }
    HttpJson(c, {{"ok", true}, {"pose", arr}});
    return;
  }
  if (path == "/api/drag") {
    // 拖拽关节点：旋转其父骨，让 骨i 在面板平面(世界XY)对准 (wx,wy)
    auto j = nlohmann::json::parse(body, nullptr, false);
    int i = j.value("i", -1);
    float wx = j.value("wx", 0.0f), wy = j.value("wy", 0.0f);
    if (i >= 0 && i < s_humanBoneCount && !s_humanBones[i].locked) {
      void *t = s_humanBones[i].transform;
      void *parent =
          g_transform_get_parent ? Invoke(g_transform_get_parent, t) : nullptr;
      if (parent) {
        Vec3 pj = GetBoneWorldPos(t);
        Vec3 pp = GetBoneWorldPos(parent);
        float curA = atan2f(pj.y - pp.y, pj.x - pp.x);
        float tgtA = atan2f(wy - pp.y, wx - pp.x);
        float da = tgtA - curA;
        if (fabsf(da) > 1e-4f)
          ApplyWorldRotDelta(parent, Quat::AxisAngle(Vec3{0, 0, 1}, da));
      }
    }
    HttpJson(c, {{"ok", true}});
    return;
  }
  // ---- 姿态预设库（WebUI 文字输入保存/载入，不依赖游戏内输入）----
  if (path == "/api/poses") {
    nlohmann::json arr = nlohmann::json::array();
    CreateDirectoryA(g_defaultPoseDir, nullptr);
    std::string pat = std::string(g_defaultPoseDir) + "\\*.poser.json";
    WIN32_FIND_DATAA fd;
    HANDLE h = FindFirstFileA(pat.c_str(), &fd);
    if (h != INVALID_HANDLE_VALUE) {
      do {
        if (!(fd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)) {
          std::string fn = fd.cFileName;
          size_t dot = fn.rfind(".poser.json");
          if (dot != std::string::npos)
            fn = fn.substr(0, dot);
          arr.push_back(fn);
        }
      } while (FindNextFileA(h, &fd));
      FindClose(h);
    }
    HttpJson(c, {{"ok", true}, {"poses", arr}});
    return;
  }
  if (path == "/api/poses/save") {
    auto j = nlohmann::json::parse(body, nullptr, false);
    std::string name = j.value("name", "");
    if (name.empty()) {
      char buf[64];
      RefreshPoseList();
      MakeNextPoseName(buf, sizeof(buf));
      name = buf;
    }
    bool bad = name.size() > 64;
    for (char ch : name)
      if (strchr("\\/:*?\"<>|", ch)) { bad = true; break; }
    if (!bad && s_humanBoneCount > 0) {
      CreateDirectoryA(g_defaultPoseDir, nullptr);
      PoseDoc doc = CapturePoseDoc(name.c_str());
      std::string json = PoseToJson(doc);
      std::string path = std::string(g_defaultPoseDir) + "\\" + name + ".poser.json";
      FILE *f = nullptr;
      if (fopen_s(&f, path.c_str(), "wb") == 0 && f) {
        fwrite(json.data(), 1, json.size(), f);
        fclose(f);
        HttpJson(c, {{"ok", true}});
        return;
      }
    }
    HttpJson(c, {{"ok", false}, {"err", "save failed"}});
    return;
  }
  if (path == "/api/poses/load") {
    auto j = nlohmann::json::parse(body, nullptr, false);
    std::string name = j.value("name", "");
    bool bad = name.empty();
    for (char ch : name)
      if (strchr("\\/:*?\"<>|", ch)) { bad = true; break; }
    if (!bad) {
      std::string path = std::string(g_defaultPoseDir) + "\\" + name + ".poser.json";
      FILE *f = nullptr;
      if (fopen_s(&f, path.c_str(), "rb") == 0 && f) {
        fseek(f, 0, SEEK_END);
        long sz = ftell(f);
        fseek(f, 0, SEEK_SET);
        std::string text(sz > 0 ? sz : 0, '\0');
        if (sz > 0)
          fread(&text[0], 1, (size_t)sz, f);
        fclose(f);
        try {
          PoseDoc doc = PoseFromJson(text);
          ApplyPoseDoc(doc);
          HttpJson(c, {{"ok", true}});
          return;
        } catch (...) {
        }
      }
    }
    HttpJson(c, {{"ok", false}, {"err", "load failed"}});
    return;
  }
  if (path == "/api/poses/delete") {
    auto j = nlohmann::json::parse(body, nullptr, false);
    std::string name = j.value("name", "");
    bool bad = name.empty();
    for (char ch : name)
      if (strchr("\\/:*?\"<>|", ch)) { bad = true; break; }
    if (!bad) {
      std::string path = std::string(g_defaultPoseDir) + "\\" + name + ".poser.json";
      DeleteFileA(path.c_str());
      HttpJson(c, {{"ok", true}});
      return;
    }
    HttpJson(c, {{"ok", false}});
    return;
  }
  HttpJson(c, {{"ok", false}, {"err", "unknown api"}});
}

static void HandleAttachedRequest(SOCKET c, const std::string &path, const std::string &body) {
  // Do not remain registered with IL2CPP while waiting on a socket or the pose
  // lock. Runtime abort APCs previously escaped from Winsock select on this thread.
  std::lock_guard<std::recursive_mutex> lock(g_poseMutex);
  RuntimeThreadScope runtime;
  if (!runtime.ready) {
    HttpJson(c, {{"ok", false}, {"err", "Game runtime is unavailable"}});
    return;
  }
  HandleRequestBody(c, path, body);
}
static void HandleClient(SOCKET c) {
  try {
    char buf[8192];
    int n = recv(c, buf, sizeof(buf) - 1, 0);
    if (n <= 0) {
      closesocket(c);
      return;
    }
    buf[n] = 0;
    std::string req(buf);
    std::string method, path, body;
    size_t sp = req.find(' ');
    if (sp != std::string::npos) {
      method = req.substr(0, sp);
      size_t sp2 = req.find(' ', sp + 1);
      if (sp2 != std::string::npos)
        path = req.substr(sp + 1, sp2 - sp - 1);
    }
    size_t hb = req.find("\r\n\r\n");
    if (hb != std::string::npos)
      body = req.substr(hb + 4);
    if (path.empty())
      path = "/";
    HandleAttachedRequest(c, path, body);
  } catch (...) {
    Log("[WEB] C++ exception in handler");
  }
  closesocket(c);
}

static DWORD WINAPI WebServerThread(LPVOID) {
  WSADATA wsa;
  WSAStartup(MAKEWORD(2, 2), &wsa);
  // Attach only for HandleAttachedRequest, never during network waits.
  SOCKET s = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
  if (s == INVALID_SOCKET) {
    Log("[WEB] socket failed");
    return 0;
  }
  int opt = 1;
  setsockopt(s, SOL_SOCKET, SO_REUSEADDR, (const char *)&opt, sizeof(opt));
  sockaddr_in addr = {};
  addr.sin_family = AF_INET;
  addr.sin_addr.s_addr = htonl(INADDR_LOOPBACK);
  addr.sin_port = htons((u_short)g_webPort);
  if (bind(s, (sockaddr *)&addr, sizeof(addr)) != 0) {
    Log("[WEB] bind 127.0.0.1:%d failed (port in use?)", g_webPort);
    closesocket(s);
    return 0;
  }
  listen(s, 8);
  Log("[WEB] UI server: http://127.0.0.1:%d", g_webPort);
  g_webRunning = !RuntimeClosing();
  while (g_webRunning && !RuntimeClosing()) {
    // 非阻塞 accept：500ms 超时轮询，g_webRunning 置假后可干净退出
    fd_set rfds;
    FD_ZERO(&rfds);
    FD_SET(s, &rfds);
    timeval tv = {0, 500000};
    int sel = select(0, &rfds, nullptr, nullptr, &tv);
    if (sel <= 0)
      continue;
    SOCKET c = accept(s, nullptr, nullptr);
    if (c == INVALID_SOCKET)
      break;
    DWORD timeoutMs = 3000;
    setsockopt(c, SOL_SOCKET, SO_RCVTIMEO, reinterpret_cast<const char *>(&timeoutMs), sizeof(timeoutMs));
    setsockopt(c, SOL_SOCKET, SO_SNDTIMEO, reinterpret_cast<const char *>(&timeoutMs), sizeof(timeoutMs));
    // 简单串行处理（够用）
    HandleClient(c);
  }
  closesocket(s);
  WSACleanup();
  return 0;
}

static void StartWebServer() {
  if (RuntimeClosing() || g_webRunning)
    return;
  CreateThread(nullptr, 0, WebServerThread, nullptr, 0, nullptr);
}

// 内嵌网页（画布骨骼小人 + 滑条 + 按钮）
const char *g_poserHtml = R"HTML(<!DOCTYPE html>
<html lang="zh"><head><meta charset="utf-8"><title>Endfield Poser</title>
<style>
body{background:#15171e;color:#e8e8e8;font-family:system-ui;margin:0;display:flex;height:100vh}
#canvasBox{flex:1;position:relative;background:#1b1e29}
canvas{position:absolute;inset:0;width:100%;height:100%}
#side{width:300px;padding:12px;overflow-y:auto;background:#1e2230}
h1{font-size:16px;margin:0 0 8px}
button{margin:2px;padding:6px 10px;background:#3a4260;color:#fff;border:none;border-radius:4px;cursor:pointer}
button:hover{background:#4a5478}
button.on{background:#e8a020}
label{display:block;margin-top:8px;font-size:12px;color:#aaa}
.sl{display:flex;align-items:center;gap:6px}
.sl input{flex:1}
.sl span{width:52px;text-align:right;font-size:12px}
</style></head><body>
<div id="canvasBox"><canvas id="cv"></canvas><div id="tip" style="position:absolute;top:8px;left:8px;font-size:12px;color:#888">点击关节选中，拖动摆姿势</div></div>
<div id="side">
  <h1>Endfield Poser</h1>
  <div><button id="btnFreeze">冻结</button><button id="btnTpose">T-Pose</button><button id="btnReset">复位</button></div>
  <label style="display:flex;align-items:center;gap:6px;margin-top:6px;font-size:12px;color:#aaa"><input type="checkbox" id="chkAcc"> 冻结飘带/裙子/头发（默认不冻结）</label>
  <div style="margin-top:12px"><button id="btnSavePose">保存预设（自动编号）</button></div>
  <div id="poseList" style="font-size:12px;margin-top:6px"></div>
  <div style="font-size:12px;margin-top:6px" id="status">连接中...</div>
  <div id="boneInfo" style="font-size:12px;color:#bbb;margin-top:6px">未选中骨骼</div>
  <label>旋转 X</label><div class="sl"><input type="range" id="rx" min="-180" max="180" step="0.5" value="0"><span id="rxv">0</span></div>
  <label>旋转 Y</label><div class="sl"><input type="range" id="ry" min="-180" max="180" step="0.5" value="0"><span id="ryv">0</span></div>
  <label>旋转 Z</label><div class="sl"><input type="range" id="rz" min="-180" max="180" step="0.5" value="0"><span id="rzv">0</span></div>
  <label>位置 X</label><div class="sl"><input type="range" id="px" min="-3" max="3" step="0.01" value="0"><span id="pxv">0</span></div>
  <label>位置 Y</label><div class="sl"><input type="range" id="py" min="-3" max="3" step="0.01" value="0"><span id="pyv">0</span></div>
  <label>位置 Z</label><div class="sl"><input type="range" id="pz" min="-3" max="3" step="0.01" value="0"><span id="pzv">0</span></div>
</div>
<script>
const API='http://127.0.0.1:18923';
const cv=document.getElementById('cv'),ctx=cv.getContext('2d');
let bones=[],selected=-1,frozen=false;
function fit(){cv.width=cv.clientWidth;cv.height=cv.clientHeight;}
window.addEventListener('resize',fit);fit();
async function post(path,data){
  try{const r=await fetch(API+path,{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify(data||{})});return await r.json();}
  catch(e){return {ok:false};}
}
async function refresh(){
  try{
    const r=await fetch(API+'/api/bones');const d=await r.json();
    bones=d.bones||[];
    const s=await (await fetch(API+'/api/status')).json();
    frozen=s.frozen;selected=s.selected;
    document.getElementById('chkAcc').checked=!!s.freeze_accessories;
    document.getElementById('status').textContent=(frozen?'已冻结':'未冻结')+' | 骨骼 '+s.bones+' | 选中 '+(selected>=0?bones[selected]?.name:'无');
    draw();
  }catch(e){document.getElementById('status').textContent='未连接';}
}
function toPanel(p){ // 世界XY -> 画布坐标（正视图，Y翻转）
  let minX=1e9,maxX=-1e9,minY=1e9,maxY=-1e9;
  for(const b of bones){if(b.x<minX)minX=b.x;if(b.x>maxX)maxX=b.x;if(b.y<minY)minY=b.y;if(b.y>maxY)maxY=b.y;}
  const spanX=(maxX-minX)||1,spanY=(maxY-minY)||1;
  const sc=Math.min((cv.width-40)/spanX,(cv.height-40)/spanY)||1;
  const mx=(minX+maxX)/2,my=(minY+maxY)/2;
  return [cv.width/2+(p.x-mx)*sc, cv.height/2-(p.y-my)*sc, sc, mx, my];
}
function toWorld(px,py){
  let minX=1e9,maxX=-1e9,minY=1e9,maxY=-1e9;
  for(const b of bones){if(b.x<minX)minX=b.x;if(b.x>maxX)maxX=b.x;if(b.y<minY)minY=b.y;if(b.y>maxY)maxY=b.y;}
  const spanX=(maxX-minX)||1,spanY=(maxY-minY)||1;
  const sc=Math.min((cv.width-40)/spanX,(cv.height-40)/spanY)||1;
  const mx=(minX+maxX)/2,my=(minY+maxY)/2;
  return [mx+(px-cv.width/2)/sc, my-(py-cv.height/2)/sc];
}
function draw(){
  ctx.clearRect(0,0,cv.width,cv.height);
  if(!bones.length)return;
  const base=toPanel(bones[0]);
  // 连线
  ctx.strokeStyle='#c0c8dc';ctx.lineWidth=1.5;
  for(const b of bones){
    if(b.parent<0)continue;
    const p=bones[b.parent];if(!p)continue;
    const a=toPanel(p),c=toPanel(b);
    ctx.beginPath();ctx.moveTo(a[0],a[1]);ctx.lineTo(c[0],c[1]);ctx.stroke();
  }
  // 关节点
  for(const b of bones){
    const c=toPanel(b);
    ctx.beginPath();
    ctx.fillStyle=b.i===selected?'#ffc83c':(b.locked?'#ff5a5a':'#78c8ff');
    ctx.arc(c[0],c[1],b.i===selected?6:4,0,Math.PI*2);ctx.fill();
  }
}
// 鼠标
let drag=-1;
function evPos(e){const r=cv.getBoundingClientRect();return [e.clientX-r.left,e.clientY-r.top];}
cv.addEventListener('mousedown',e=>{
  const [mx,my]=evPos(e);let best=-1,bd=20;
  for(const b of bones){const c=toPanel(b);const d=Math.hypot(mx-c[0],my-c[1]);if(d<bd){bd=d;best=b.i;}}
  if(best>=0){
    if(!frozen) post('/api/freeze',{on:true}).then(r=>{frozen=r.frozen;document.getElementById('btnFreeze').textContent=frozen?'解冻':'冻结';});
    selected=best;drag=best;post('/api/select',{i:best});draw();
  }
});
cv.addEventListener('mousemove',e=>{
  if(drag>=0){const [mx,my]=evPos(e);const [wx,wy]=toWorld(mx,my);post('/api/drag',{i:drag,wx,wy});}
});
cv.addEventListener('mouseup',()=>{drag=-1;});
// 滑条：按下拖动时发送增量
function slider(id,axis){
  const el=document.getElementById(id),lab=document.getElementById(id+'v');
  let base=0;
  el.addEventListener('pointerdown',()=>{base=parseFloat(el.value);});
  el.addEventListener('input',()=>{
    const d=parseFloat(el.value)-base;
    lab.textContent=el.value;
    if(selected>=0)post('/api/setrot',{i:selected,axis,d});
  });
}
slider('rx',0);slider('ry',1);slider('rz',2);
document.getElementById('chkAcc').addEventListener('change',e=>{post('/api/freeze',{on:frozen,accessories:e.target.checked});});
function posSlider(id,key){
  const el=document.getElementById(id),lab=document.getElementById(id+'v');
  let base=0;
  el.addEventListener('pointerdown',()=>{base=parseFloat(el.value);});
  el.addEventListener('input',()=>{
    const d=parseFloat(el.value)-base;
    lab.textContent=el.value;
    if(selected>=0)post('/api/setpos',{i:selected,[key]:d});
  });
}
posSlider('px','x');posSlider('py','y');posSlider('pz','z');
async function refreshPoses(){
  try{
    const r=await fetch(API+'/api/poses');const d=await r.json();
    const box=document.getElementById('poseList');
    box.innerHTML='';
    (d.poses||[]).forEach(n=>{
      const row=document.createElement('div');
      row.style.cssText='display:flex;gap:4px;align-items:center;margin-top:2px';
      const span=document.createElement('span');
      span.style.cssText='flex:1;color:#ccc';span.textContent=n;
      const lb=document.createElement('button');
      lb.textContent='载入';lb.style.cssText='padding:2px 6px';
      lb.onclick=()=>post('/api/poses/load',{name:n}).then(()=>refresh());
      const db=document.createElement('button');
      db.textContent='删';db.style.cssText='padding:2px 6px';
      db.onclick=()=>post('/api/poses/delete',{name:n}).then(()=>refreshPoses());
      row.appendChild(span);row.appendChild(lb);row.appendChild(db);
      box.appendChild(row);
    });
  }catch(e){}
}
document.getElementById('btnSavePose').addEventListener('click',()=>{
  post('/api/poses/save',{name:''}).then(()=>refreshPoses());
});
refreshPoses();
document.getElementById('btnFreeze').onclick=async()=>{
  const r=await post('/api/freeze',{on:!frozen});
  frozen=r.frozen;
  document.getElementById('btnFreeze').textContent=frozen?'解冻':'冻结';
};
document.getElementById('btnTpose').onclick=()=>post('/api/tpose',{});
document.getElementById('btnReset').onclick=()=>post('/api/reset',{});
setInterval(refresh,120);
refresh();
</script></body></html>)HTML";
