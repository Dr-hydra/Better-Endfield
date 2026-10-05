#pragma once
#include "core/build_features.h"
#include "game/char_state.h"
#include "game/mmd_io.h"
#include "game/mmd_audio.h"
#include "game/smc_morph.h"
#include "math/mmd_props.h"
#include "math/mmd_retarget.h"
#include "math/mmd_calibration.h"
#include "game/mmd_avatar.h"
#include "game/mmd_terrain.h"
#include "math/mmd_adaptation.h"
#include "math/mmd_countdown.h"
#include "core/mmd_countdown_hud.h"
#include "game/mmd_camera.h"
#if POSER_ENABLE_XXMI_BRIDGE
#include "game/mod_bridge.h"
#endif
#include "game/mmd_camera_settings.h"
#include "nlohmann/json.hpp"
#include <atomic>
#include <chrono>
#include <commdlg.h>
#include <future>
#include <iomanip>
#include <sstream>
#pragma comment(lib, "comdlg32.lib")

static double MmdNow() {
  return std::chrono::duration<double>(
             std::chrono::steady_clock::now().time_since_epoch())
      .count();
}
// SEH leaves: keep C++ containers/destructors out of SEH functions (MSVC
// C2712).
static bool MmdReadMatrix(void *method, void *object, mmd::Matrix *out) {
  __try {
    if (!method || !UnityObjAlive(object))
      return false;
    void *boxed = Invoke(method, object);
    if (!boxed)
      return false;
    memcpy(out, static_cast<char *>(boxed) + 16, 64);
    for (float f : out->m)
      if (!std::isfinite(f))
        return false;
    return true;
  } __except (1) {
    return false;
  }
}
static void *MmdComponent(void *transform, void *klass) {
  __try {
    if (!UnityObjAlive(transform) || !klass)
      return nullptr;
    void *go = Invoke(g_component_get_gameObject, transform);
    void *type = il2cpp_class_get_type(klass);
    void *obj = type ? il2cpp_type_get_object(type) : nullptr;
    void *p[] = {obj};
    return go && obj ? Invoke(g_gameObject_GetComponent, go, p) : nullptr;
  } __except (1) {
    return nullptr;
  }
}
static int MmdArrayLength(void *a) {
  __try {
    if (!a)
      return 0;
    int n = *(int *)(static_cast<char *>(a) + IL2CPP_ARRAY_LEN);
    return n >= 0 && n <= 16384 ? n : 0;
  } __except (1) {
    return 0;
  }
}
static void *MmdArrayObject(void *a, int i) {
  __try {
    return *((void **)(static_cast<char *>(a) + IL2CPP_ARRAY_DATA) + i);
  } __except (1) {
    return nullptr;
  }
}
static bool MmdArrayMatrix(void *a, int i, mmd::Matrix *out) {
  __try {
    memcpy(out, static_cast<char *>(a) + IL2CPP_ARRAY_DATA + 64 * i, 64);
    for (float f : out->m)
      if (!std::isfinite(f))
        return false;
    return true;
  } __except (1) {
    return false;
  }
}
static void MmdRawPose(void *t, Vec3 p, Quat q) {
  __try {
    if (!UnityObjAlive(t))
      return;
    void *pp[] = {&p};
    void *qp[] = {&q};
    Invoke(g_transform_set_localPosition, t, pp);
    Invoke(g_transform_set_localRotation, t, qp);
  } __except (1) {
  }
}
static bool MmdEnabled(void *c) {
  bool enabled = false;
  ReadBehaviourEnabled(c, enabled);
  return enabled;
}
static void MmdEnable(void *c, bool value) {
  WriteBehaviourEnabled(c, value);
}
static Vec3 MmdLocalScale(void *transform) {
  __try {
    if (g_transform_get_localScale && UnityObjAlive(transform)) {
      void *v = Invoke(g_transform_get_localScale, transform);
      if (v)
        return *(Vec3 *)(static_cast<char *>(v) + 16);
    }
  } __except (1) {
  }
  return {1, 1, 1};
}
static std::string MmdFingerprint() {
  uint64_t h = 14695981039346656037ull;
  auto hash = [&](const void *data, size_t length) {
    auto p = static_cast<const unsigned char *>(data);
    for (size_t j = 0; j < length; j++) {
      h ^= p[j];
      h *= 1099511628211ull;
    }
  };
  for (size_t i = 0; i < s_allBones.size(); i++) {
    std::string n = i ? s_allBones[i].name : CurrentCharModelKey();
    n += "/" + std::to_string(s_allBones[i].parentIdx);
    hash(n.data(), n.size());

  }
  std::ostringstream s;
  s << "hierarchy1-" << std::hex << h;
  return s.str();
}
static std::filesystem::path MmdConfigDirectory() {
  wchar_t p[32768] = {};
  GetModuleFileNameW(GetModuleHandleW(L"poser.dll"), p, 32768);
  return std::filesystem::path(p).parent_path() / L"mmd";
}
static mmd::RetargetProfile MmdCurrentProfile() {
  mmd::RetargetProfile p;
  p.model = CurrentCharModelKey();
  p.fingerprint = MmdFingerprint();
  p.bones.reserve(s_allBones.size());
  for (const auto &b : s_allBones) {
    mmd::TargetBone n;
    n.name = b.parentIdx < 0 ? p.model : b.name;
    n.parent = b.parentIdx;
    n.localPos = b.parentIdx < 0 ? Vec3{} : GetBoneLocalPos(b.transform);
    n.localRot = b.parentIdx < 0 ? Quat{} : GetBoneLocalRot(b.transform);
    n.localScale = b.parentIdx < 0 ? Vec3{1, 1, 1} : MmdLocalScale(b.transform);
    for (int j = 0; j < s_humanBoneCount; j++)
      if (s_humanBones[j].transform == b.transform)
        n.role = int(s_humanBones[j].humanBone);
    p.bones.push_back(n);
  }
  p.globals();
  return p;
}
static std::string s_mmdCalibrationDetail;
static bool MmdBindCalibration(mmd::RetargetProfile &profile) {
  return mmd_avatar::Calibrate(g_charAnimator,profile,s_mmdCalibrationDetail);
}
static uint64_t s_mmdCalibrationSerial=0;
static void MmdSaveCalibration(const mmd::RetargetProfile &p) {
  using nlohmann::json;
  json j = {{"version", 3},
            {"model", p.model},
            {"fingerprint", p.fingerprint},
            {"bones", json::array()}};
  for (const auto &b : p.bones)
    j["bones"].push_back(
        {{"name", b.name},
         {"parent", b.parent},
         {"role", b.role},
         {"pos", {b.localPos.x, b.localPos.y, b.localPos.z}},
         {"scale", {b.localScale.x, b.localScale.y, b.localScale.z}},
         {"rot", {b.localRot.x, b.localRot.y, b.localRot.z, b.localRot.w}},
         {"calibrated", b.calibrated}});
  auto dir = MmdConfigDirectory();
  std::filesystem::create_directories(dir);
  auto dest = dir / (p.fingerprint + ".rig.json"), temp = dest;
  // Version 2 manual previews could save a distorted pose. Retain the file for
  // recovery, but never silently reuse it as a valid calibration.
  if (std::filesystem::exists(dest)) {
    std::ifstream old(dest);
    auto previous = json::parse(old, nullptr, false);
    if (previous.is_object() && previous.value("version", 0) < 3) {
      auto backup = dest;
      backup += L".before-tpose-fix.bak";
      std::filesystem::copy_file(dest, backup,
                                 std::filesystem::copy_options::skip_existing);
    }
  }
  temp += L".tmp";
  {
    std::ofstream f(temp);
    if (!f || !(f << j.dump(2)))
      throw std::runtime_error("Cannot save calibration");
  }
  if (!MoveFileExW(temp.c_str(), dest.c_str(),
                   MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH))
    throw std::runtime_error("Cannot replace calibration");
  ++s_mmdCalibrationSerial;
}
static bool MmdLoadCalibration(mmd::RetargetProfile &p) {
  auto load=[&](const std::filesystem::path &path) {
    try {
      if(std::filesystem::file_size(path)>4*1024*1024)return false;
      std::ifstream f(path);nlohmann::json j;f>>j;
      if(j.value("model","")!=p.model)return false;
      return mmd::RestoreBodyCalibration(mmd::ReadCalibration(j),p);
    }catch(...){return false;}
  };
  const auto directory=MmdConfigDirectory(),exact=directory/(p.fingerprint+".rig.json");
  if(load(exact))return true;
  // Preserve version-3 user files. A weapon or effect change can alter the old
  // whole-hierarchy fingerprint while the actual calibrated body is unchanged.
  try {
    std::vector<std::pair<std::filesystem::file_time_type,std::filesystem::path>> candidates;
    for(const auto &entry:std::filesystem::directory_iterator(directory)) {
      const auto name=entry.path().filename().wstring();
      if(entry.is_regular_file()&&entry.path()!=exact&&name.size()>9&&name.substr(name.size()-9)==L".rig.json")
        candidates.emplace_back(entry.last_write_time(),entry.path());
      if(candidates.size()>256)return false;
    }
    std::sort(candidates.rbegin(),candidates.rend());
    for(const auto &candidate:candidates)if(load(candidate.second))return true;
  }catch(...){}
  return false;
}
struct MmdSavedTransform {
  void *transform;
  Vec3 pos;
  Quat rot;
};
struct MmdSavedComponent {
  void *component;
  bool enabled;
};
struct MmdSavedProp {
  void *object = nullptr;
  bool active = false;
};
static bool MmdActiveSelf(void *object, bool *active) {
  __try {
    if (!g_gameObject_get_activeSelf || !UnityObjAlive(object))
      return false;
    void *boxed = Invoke(g_gameObject_get_activeSelf, object);
    if (!boxed)
      return false;
    *active = *reinterpret_cast<bool *>(static_cast<char *>(boxed) + 16);
    return true;
  } __except (1) {
    return false;
  }
}
static void MmdSetActive(void *object, bool active) {
  __try {
    if (!g_gameObject_setActive || !UnityObjAlive(object))
      return;
    int value = active ? 1 : 0;
    void *args[] = {&value};
    Invoke(g_gameObject_setActive, object, args);
  } __except (1) {
  }
}
static int MmdChildCount(void *transform) {
  __try {
    if (!UnityObjAlive(transform))
      return 0;
    void *boxed = Invoke(g_transform_get_childCount, transform);
    int n =
        boxed ? *reinterpret_cast<int *>(static_cast<char *>(boxed) + 16) : 0;
    return n >= 0 && n < 8192 ? n : 0;
  } __except (1) {
    return 0;
  }
}
struct MmdSession {
  bool bodyOwned = false;
  uint64_t cameraSession = 0;
  Quat cameraBasis;
  float cameraHeight=0;
  bool active = false, wasFrozen = false, freezeAccessories = false,
       animatorWasEnabled = false;
  void *animator = nullptr;
  void *root = nullptr;
  int revision = 0;
  Vec3 rootPos;
  Quat rootRot;
  mmd::Matrix anchorWorld;
  mmd_terrain::Runtime terrain;
  bool anchorWorldValid=false;
  std::vector<MmdSavedTransform> transforms;
  std::shared_ptr<GripReferences> references;
  std::vector<MmdSavedComponent> components;
  std::vector<MmdSavedProp> props;
  double nextPropScan = 0;
  std::vector<AccessoryBone> accessories;
  std::vector<AccessoryChain> chains;
};
struct MmdMorphMapping {
  int slider = -1;
  float gain = 1;
  int nativeSlider = -1;
  float nativeGain = 1;
};
struct MmdLoadResult {
  int kind = 0;
  bool cancelled = false;
  std::string file, error;
  mmd::MotionClip clip;
  mmd::RigDefinition rig;
  nlohmann::json adaptation;
  std::shared_ptr<const mmd::AudioClip> music;
};
struct MmdFaceLibraryResult {
  std::vector<std::shared_ptr<const character_face::Profile>> profiles;
  std::string error;
};
struct MmdPlayer {
  bool faceSettingsLoaded=false,faceLibraryStarted=false,faceLibraryLoading=false;
  std::vector<std::shared_ptr<const character_face::Profile>> faceLibrary;
  std::shared_ptr<const character_face::Profile> characterFace;
  std::future<MmdFaceLibraryResult> faceLoader;
  std::string faceLibraryError,faceModel;
  uint64_t faceSelectionGeneration=0;
  bool faceSelectionReady=false;
  face_mixing::Settings faceSettings;
  nlohmann::json faceSavedMappings=nlohmann::json::object();
  nlohmann::json faceSavedNativeMappings=nlohmann::json::object();
  mmd::MotionClip clip;
  std::vector<mmd::CameraKey> cameraTrack;
  std::string cameraFile;
  mmd::CameraSettings cameraSettings;
  mmd::CameraPresetStore cameraPresets;
  std::string cameraTrackId;
  mmd::RigDefinition rig = mmd::AdaptRig(mmd::StandardRig(),mmd::DefaultRigAdaptation());
  mmd::RigDefinition baseRig = mmd::StandardRig();
  mmd::RigAdaptation adaptation=mmd::DefaultRigAdaptation();
  int adaptationRevision = 0;
  std::string adaptationFile;
  int sourcePreset = 0; // 0: A-pose, 1: extracted T-pose; manual selection
  mmd::IkMode ikMode = mmd::IkMode::FollowMotion;
  mmd::MotionAmplitude amplitude;
  mmd::RetargetProfile profile;
  mmd::Retargeter mapper;
  mmd::Timeline timeline;
  mmd::PlaybackCountdown countdown;
  bool countdownEnabled=false;
  int countdownSeconds=3;
  mmd::AudioPlayer audio;
  bool musicEnabled = true;
  float musicVolume = .7f, musicOffset = 0;
  std::string musicFile, musicError;
  MmdSession session;
  bool reference = false, inPlace = false, freezeCloth = false, preview = false,
       show = true, autoScale = true;
  float scale = .08f, height = 0;
  mmd_terrain::Settings terrain;
  bool calibrateRequested=false;
  std::string file, referenceFile, status = u8"选择 VMD 动作文件",
                                   calibrationStatus;
  std::map<std::string, MmdMorphMapping> morphMap;
  std::vector<std::string> report;
  std::future<MmdLoadResult> loader;
  bool loading = false;
  int profileRevision = -1;
  void *profileAnimator = nullptr;
};
// Process-lifetime owner: its futures and XAudio voices must not be destroyed
// by the CRT under the DLL loader lock after Windows has stopped other threads.
// Normal plugin disable still performs MmdStop and joins imports explicitly.
static MmdPlayer &g_mmd = *new MmdPlayer;
// Optional squad controller. Callbacks are installed by mmd_squad.h; keeping
// this boundary independent also preserves the standalone single-player tests.
struct MmdSquadBridge {
  bool (*active)() = nullptr;
  void (*stop)() = nullptr;
  bool (*tick)() = nullptr;
  bool (*command)(int) = nullptr;
  bool (*busy)() = nullptr;
  void (*selectSingle)() = nullptr;
  bool (*hotkeyTarget)() = nullptr;
  void (*characterChanging)(void *) = nullptr;
};
static MmdSquadBridge g_mmdSquadBridge;
static bool MmdSquadOwnsPose() {return g_mmdSquadBridge.active && g_mmdSquadBridge.active();}
static bool MmdSquadBusy() {return g_mmdSquadBridge.busy && g_mmdSquadBridge.busy();}
static mmd::DeferredStart s_mmdStartRequest;
static void *s_mmdStartEntity=nullptr;
static double s_mmdStartDeadline=0;
static void *MmdSelectedEntity() {
  AcquireSRWLockShared(&g_pendingCharacterLock);
  void *entity=g_pendingSelection?g_pendingEntity:g_captureEntity;
  ReleaseSRWLockShared(&g_pendingCharacterLock);
  return entity;
}
static void MmdQueueStart(bool replace=false) {
  if(replace||!s_mmdStartRequest.active) {
    s_mmdStartRequest.play();s_mmdStartEntity=MmdSelectedEntity();s_mmdStartDeadline=MmdNow()+15.;
  } else if(!s_mmdStartDeadline) {
    s_mmdStartEntity=MmdSelectedEntity();s_mmdStartDeadline=MmdNow()+15.;
  }
}
static void MmdCancelStart() {
  s_mmdStartRequest.cancel();s_mmdStartEntity=nullptr;s_mmdStartDeadline=0;
}
static const std::vector<mmd::CameraKey> &MmdCameraKeys() {
  return g_mmd.cameraFile.empty() ? g_mmd.clip.cameras : g_mmd.cameraTrack;
}
static bool MmdHasContent() { return !g_mmd.clip.empty() || !MmdCameraKeys().empty(); }
static void MmdUpdateDuration() {
  auto &m=g_mmd;const auto &keys=MmdCameraKeys();
  uint32_t last=0;
  for(const auto &kv:m.clip.bones)if(!kv.second.empty())last=(std::max)(last,kv.second.back().frame);
  for(const auto &kv:m.clip.morphs)if(!kv.second.empty())last=(std::max)(last,kv.second.back().frame);
  m.timeline.duration=(std::max)(last/30.0,mmd::CameraDuration(keys,m.cameraSettings));
}
static void MmdSelectCameraSettings() {
  auto &m=g_mmd;
  static void *actor=nullptr;static int revision=-1;static std::string model;
  if(actor!=g_charAnimator||revision!=s_bonesRev) {
    actor=g_charAnimator;revision=s_bonesRev;model=character_face::ModelKey(CurrentCharModelKey());
  }
  m.cameraPresets.load(MmdConfigDirectory()/L"camera-settings.json");
  auto previous=m.cameraPresets.selected;
  m.cameraPresets.select(model,m.cameraTrackId,m.cameraSettings);
  if(previous!=m.cameraPresets.selected)MmdUpdateDuration();
}
static void MmdCameraTrackChanged() {
  g_mmd.cameraTrackId=mmd::CameraTrackId(MmdCameraKeys());
  MmdSelectCameraSettings();
}
static void MmdPublishCamera() {
  auto &m=g_mmd;auto &s=m.session;const auto &keys=MmdCameraKeys();
  if (!s.active || m.preview || !m.cameraSettings.enabled || keys.empty() || !mmd_camera::ready) {
    mmd_camera::Stop();return;
  }
  // Track actual model-root motion; camera-only playback can follow locomotion.
  Vec3 delta=GetBoneWorldPos(s.root)-s.anchorWorld.position();
  Vec3 correction=s.bodyOwned?mmd::Rotation(s.anchorWorld)*Vec3{0,m.height,0}:Vec3{};
  correction.y+=s.terrain.rootOffset;
  mmd_camera::Publish({true,s.cameraSession,s.animator,
    mmd::PlaceCamera(mmd::SampleCamera(keys,mmd::CameraFrame(m.timeline.seconds,m.cameraSettings),m.cameraSettings),
      m.cameraSettings,s.anchorWorld.position(),s.cameraBasis,delta,m.scale,
      s.cameraHeight,mmd::CameraSourceHeight(m.rig),correction),nullptr,0,m.timeline.seconds*30});
}

static void MmdSyncAudio() {
  auto &m = g_mmd;
  try {
    m.audio.sync(m.timeline, m.session.active && !m.preview,
                 m.musicEnabled, m.musicOffset, m.musicVolume);
  } catch (const std::exception &e) {
    m.audio.close();
    m.musicEnabled = false;
    m.musicError = e.what();
    Log("[MMD] music disabled, body continues: %s", e.what());
  }
}
static void MmdSeek(double seconds) {
  g_mmd.countdown.cancel();g_mmdCountdownDisplay.store(0);
  ++g_mmd.session.terrain.epoch;
  g_mmd.timeline.seek(seconds, MmdNow());
  MmdSyncAudio();
}
static const char *MmdSourceRigLabel() {
  return g_mmd.rig.name == "Extracted T-pose" ? u8"提取动作 T 姿（中心骨在原点）"
                                              : u8"标准 MMD（A 姿）";
}
static void MmdHideSessionProps(MmdSession &s, bool force = false) {
  if (!s.active || !s.bodyOwned || !UnityObjAlive(s.root) || !g_gameObject_setActive)
    return;
  double now = MmdNow();
  if (!force && now < s.nextPropScan)
    return;
  s.nextPropScan = now + .2;
  // Reassert hidden state if an independent idle/weapon controller enables it.
  for (const auto &prop : s.props) {
    bool active = false;
    if (MmdActiveSelf(prop.object, &active) && active)
      MmdSetActive(prop.object, false);
  }
  std::vector<void *> pending{s.root};
  size_t visited = 0;
  while (!pending.empty() && ++visited <= 8192) {
    void *t = pending.back();
    pending.pop_back();
    if (!UnityObjAlive(t))
      continue;
    char name[256] = {};
    GetBoneName(t, name, sizeof(name));
    if (t != s.root && mmd::IsPlaybackPropNode(name)) {
      void *object = Invoke(g_component_get_gameObject, t);
      bool active = false;
      if (MmdActiveSelf(object, &active)) {
        auto found = std::find_if(
            s.props.begin(), s.props.end(),
            [&](const MmdSavedProp &p) { return p.object == object; });
        if (found == s.props.end()) {
          s.props.push_back({object, active});
          Log("[MMD] prop hidden: %s (was active=%d)", name, int(active));
        }
        if (active)
          MmdSetActive(object, false);
      }
      continue;
    }
    int count = MmdChildCount(t);
    for (int child = 0; child < count; ++child) {
      void *args[] = {&child};
      if (void *next = Invoke(g_transform_GetChild, t, args))
        pending.push_back(next);
    }
  }
}
static void MmdHideProps(bool force = false) {MmdHideSessionProps(g_mmd.session,force);}
static std::atomic<HWND> s_mmdDialog{nullptr};
static std::atomic<bool> s_mmdClosing{false};
static UINT_PTR CALLBACK MmdDialogHook(HWND window, UINT message, WPARAM,
                                       LPARAM) {
  if (message == WM_INITDIALOG) {
    HWND dialog = GetParent(window);
    s_mmdDialog.store(dialog);
    if (s_mmdClosing.load())
      PostMessageW(dialog, WM_CLOSE, 0, 0);
  }
  return 0;
}
static void MmdStop(void *nextEntity=nullptr);
static void MmdReport();
static void MmdSaveFaceSettings();
static void MmdLoadFaceSettings() {
  auto &m=g_mmd;if(m.faceSettingsLoaded)return;m.faceSettingsLoaded=true;
  try {
    auto path=MmdConfigDirectory()/L"character-face-settings.json";
    bool legacy=!std::filesystem::exists(path);
    std::ifstream f(legacy?MmdConfigDirectory()/L"face-templates.json":path);
    if(!f)return;
    nlohmann::json j;f>>j;m.faceSettings=face_mixing::Read(j);
    if(!legacy) {
      auto mappings=j.value("mappings",nlohmann::json::object());
      if(!mappings.is_object())throw std::runtime_error("Invalid character face mappings");
      m.faceSavedMappings=std::move(mappings);
    }
    if(legacy)MmdSaveFaceSettings();
  } catch(const std::exception &e){m.faceLibraryError=e.what();Log("[FACE] settings load failed: %s",e.what());}
}
static void MmdSaveFaceSettings() {
  try {
    auto dir=MmdConfigDirectory();std::filesystem::create_directories(dir);
    auto path=dir/L"character-face-settings.json",temp=dir/L"character-face-settings.json.tmp";
    auto j=face_mixing::Write(g_mmd.faceSettings);j["mappings"]=g_mmd.faceSavedMappings;
    {std::ofstream f(temp,std::ios::binary|std::ios::trunc);f<<j.dump(2);f.flush();
      if(!f)throw std::runtime_error("Could not write character face settings");}
    if(!MoveFileExW(temp.c_str(),path.c_str(),MOVEFILE_REPLACE_EXISTING|MOVEFILE_WRITE_THROUGH))
      throw std::runtime_error("Could not replace character face settings");
  } catch(const std::exception &e){g_mmd.faceLibraryError=e.what();}
}
static void MmdSaveMappings(bool native) {
  if(!native) {
    if(!g_mmd.characterFace)return;
    auto &saved=g_mmd.faceSavedMappings[g_mmd.characterFace->key];
    for(auto &kv:g_mmd.morphMap)saved[kv.first]={{"morph",kv.second.slider>=0?
      g_mmd.characterFace->morphs[kv.second.slider].name:""},{"gain",kv.second.gain}};
    MmdSaveFaceSettings();return;
  }
  try {
    nlohmann::json j=g_mmd.faceSavedNativeMappings;
    for(auto &kv:g_mmd.morphMap)j[kv.first]={{"slider",kv.second.nativeSlider},{"gain",kv.second.nativeGain}};
    auto dir=MmdConfigDirectory();std::filesystem::create_directories(dir);
    auto path=dir/L"morph-mapping.json",temp=dir/L"morph-mapping.json.tmp";
    {std::ofstream f(temp,std::ios::binary|std::ios::trunc);f<<j.dump(2);f.flush();
      if(!f)throw std::runtime_error("Could not write fixed expression mappings");}
    if(!MoveFileExW(temp.c_str(),path.c_str(),MOVEFILE_REPLACE_EXISTING|MOVEFILE_WRITE_THROUGH))
      throw std::runtime_error("Could not replace fixed expression mappings");
    g_mmd.faceSavedNativeMappings=std::move(j);
  } catch(const std::exception &e){g_mmd.faceLibraryError=e.what();}
}
static void MmdMapMorphs() {
  MmdLoadFaceSettings();auto &m=g_mmd;m.morphMap.clear();
  nlohmann::json saved;
  try {std::ifstream f(MmdConfigDirectory()/L"morph-mapping.json");if(f)f>>saved;}catch(...){}
  m.faceSavedNativeMappings=saved.is_object()?saved:nlohmann::json::object();
  const char *vowels[]={u8"あ",u8"い",u8"う",u8"え",u8"お"};
  for(auto &kv:m.clip.morphs) {
    MmdMorphMapping map;
    if(m.characterFace) {
      auto target=kv.first;
      try {
        const auto &all=m.faceSavedMappings;
        if(all.contains(m.characterFace->key)&&all[m.characterFace->key].contains(kv.first)) {
          const auto &v=all[m.characterFace->key][kv.first];target=v.value("morph",target);
          map.gain=face_geometry::Clamp(v.value("gain",1.f),0,2);
        }
      }catch(...){}
      map.slider=character_face::FindMorph(*m.characterFace,target);
    }
    for(int i=0;i<5;++i)if(kv.first==vowels[i])map.nativeSlider=i;
    for(int i=0;i<s_extraMorphCount;++i)if(kv.first==mmd::Name(s_extraMorphs[i].vmdNameUtf8))map.nativeSlider=5+i;
    try {if(saved.contains(kv.first)){map.nativeSlider=saved[kv.first].value("slider",map.nativeSlider);
      map.nativeGain=face_geometry::Clamp(saved[kv.first].value("gain",1.f),0,2);}}catch(...){}
    if(map.nativeSlider < -1||map.nativeSlider>=SMCSliderCount())map.nativeSlider=-1;
    m.morphMap[kv.first]=map;
  }
}
static void MmdReloadCharacterFaces() {
  auto &m=g_mmd;if(m.faceLibraryLoading||MmdOwnsPose())return;
  m.faceLibraryStarted=true;m.faceLibraryLoading=true;m.faceLibraryError.clear();
  auto directory=MmdConfigDirectory()/L"character-faces";
  m.faceLoader=std::async(std::launch::async,[directory] {
    MmdFaceLibraryResult result;
    try {
      if(!std::filesystem::exists(directory))return result;
      std::vector<std::filesystem::path> paths;
      for(const auto &entry:std::filesystem::directory_iterator(directory)) {
        auto name=entry.path().filename().u8string();
        if(entry.is_regular_file()&&name.size()>=10&&name.compare(name.size()-10,10,".face.json")==0)paths.push_back(entry.path());
        if(paths.size()>256)throw std::runtime_error("Too many character face profiles");
      }
      std::sort(paths.begin(),paths.end());uintmax_t total=0;
      for(const auto &path:paths) {
        auto bytes=std::filesystem::file_size(path);total+=bytes;
        if(bytes>16*1024*1024||total>128*1024*1024)throw std::runtime_error("Character face library too large");
        std::ifstream f(path,std::ios::binary);nlohmann::json j;f>>j;
        result.profiles.push_back(std::make_shared<const character_face::Profile>(character_face::Read(j)));
      }
      std::stable_sort(result.profiles.begin(),result.profiles.end(),[](const auto &a,const auto &b) {
        if(a->key!=b->key)return a->key<b->key;
        return a->label.size()<b->label.size();
      });
    }catch(const std::exception &e){result.profiles.clear();result.error=e.what();}
    return result;
  });
}
static void MmdPollCharacterFaces() {
  auto &m=g_mmd;bool changed=false,selectionChanged=false;
  if(!m.faceLibraryStarted)MmdReloadCharacterFaces();
  if(m.faceLibraryLoading&&m.faceLoader.wait_for(std::chrono::seconds(0))==std::future_status::ready) {
    auto result=m.faceLoader.get();m.faceLibraryLoading=false;m.faceLibraryError=result.error;
    if(result.error.empty()){m.faceLibrary=std::move(result.profiles);changed=true;}
  }
  auto key=character_face::ModelKey(CurrentCharModelKey());
  if(changed||key!=m.faceModel||m.faceSelectionGeneration!=s_faceGeneration||m.faceSelectionReady!=s_faceHierarchy.ready) {
    selectionChanged=true;
    auto previous=m.characterFace;m.faceModel=key;m.characterFace.reset();float best=1e30f;
    for(auto &profile:m.faceLibrary)if(profile->key==key) {
      if(!m.characterFace)m.characterFace=profile;
      if(s_faceHierarchy.ready) {
        auto binding=character_face::Bind(*profile,key,s_faceNodes,s_faceHierarchy);
        if(binding.ready&&binding.error<best){best=binding.error;m.characterFace=profile;}
      }
    }
    m.faceSelectionGeneration=s_faceGeneration;m.faceSelectionReady=s_faceHierarchy.ready;
    if(changed||previous!=m.characterFace)MmdMapMorphs();
  }
  SMCFaceSelectProfile(m.characterFace,CurrentCharModelKey());
  if(selectionChanged)MmdReport();
}
static void MmdReport() {
  auto &m = g_mmd;
  m.report = m.clip.warnings;
  m.report.insert(m.report.end(), m.rig.warnings.begin(), m.rig.warnings.end());
  mmd::RigEvaluator evaluation;
  auto roles = mmd::AdaptedRoles(m.adaptation);
  std::vector<int> outputs;
  for (int i = 0; i < 55; ++i) {
    auto it = roles.find(i);
    outputs.push_back(m.rig.find(it == roles.end() ? mmd::RoleNames()[i] : it->second));
  }
  evaluation.bind(m.rig, m.clip, m.adaptation.tracks, outputs);
  for (const auto &kv : m.adaptation.tracks)
    if (!kv.second.empty() && !m.clip.bones.count(kv.second))
      m.report.push_back(u8"映射的动作轨道不存在：" + kv.second + u8" → " + kv.first);
  for (const auto &name : evaluation.unmapped)
    m.report.push_back(u8"未映射或不影响人体的骨骼: " + name);
  if(!m.characterFace)m.report.push_back(m.faceSettings.fallback?
    u8"当前角色没有专属表情校准，将使用固定映射":u8"当前角色没有专属表情校准，固定映射兜底已关闭");
  else if(!s_characterBinding.ready)m.report.push_back(m.faceSettings.fallback?
    u8"专属校准尚未匹配当前骨架，将使用固定映射":u8"专属校准尚未匹配当前骨架，固定映射兜底已关闭");
  for(auto &kv:m.morphMap) {
#if POSER_ENABLE_XXMI_BRIDGE
    if(ModBridgeUsesMorph(kv.first)){m.report.push_back(u8"用于 mod 联动: "+kv.first);continue;}
#endif
    if(kv.second.slider<0) {
      if(kv.second.nativeSlider<0||!m.faceSettings.fallback)m.report.push_back(u8"未映射表情: "+kv.first);
      else m.report.push_back(u8"专属校准未覆盖，使用固定映射: "+kv.first);
    } else {
      const auto &morph=m.characterFace->morphs[kv.second.slider];
      if(!morph.supported)m.report.push_back(kv.first+u8"："+morph.reason+
        (kv.second.nativeSlider>=0?u8"；可使用固定映射":u8"；无对应的固定映射"));
      else if(s_characterBinding.ready&&kv.second.slider<int(s_characterBinding.usable.size())&&!s_characterBinding.usable[kv.second.slider])
        m.report.push_back(kv.first+u8"：游戏面部缺少校准所需控制点");
      else if(morph.residual>.1f)m.report.push_back(kv.first+u8"：骨骼近似，部分源形变无法完整还原");
    }
  }
}
static bool MmdApplyAdaptation(const mmd::RigAdaptation &next, int sourcePreset) {
  auto &m = g_mmd;
  if (m.session.active || m.loading) return false;
  try {
    auto base = m.reference ? m.baseRig : mmd::StandardRig(
        sourcePreset == 1 ? mmd::BuiltinRigPreset::ExtractedTPose : mmd::BuiltinRigPreset::StandardMmd);
    auto rig = mmd::AdaptRig(base, next);
    m.baseRig = std::move(base); m.rig = std::move(rig);
    m.adaptation = next; m.sourcePreset = sourcePreset;
    ++m.adaptationRevision;
    MmdReport();
    m.status = u8"骨架适配已应用；下次播放使用新设置";
    return true;
  } catch (const std::exception &e) {
    m.status = std::string(u8"未应用，保留原适配：") + e.what();
    return false;
  }
}
static void MmdBeginLoad(int kind, std::filesystem::path path = {}) {
  auto &m = g_mmd;
  if (m.loading || m.session.active || MmdSquadBusy())
    return;
  s_mmdClosing.store(false);
  HWND owner = g_gameHwnd;
  nlohmann::json saved;
  if (kind == 4) {
    saved = mmd::AdaptationJson(m.adaptation, m.sourcePreset, m.ikMode);
    saved["motion_amplitude"] = mmd::AmplitudeJson(m.amplitude);
    saved["native_cloth"] = mmd::NativeClothJson({s_skirtHipRadiusDelta.load(),s_clothAutoEnabled.load(),s_collisionGeometry.load(),s_clothRibbonDamping.load()});
    saved["requires_pmx"] = m.reference;
    // Portable source structure check, never store a required local PMX path.
    if (m.reference) {
      saved["pmx_bones"] = nlohmann::json::array();
      for (const auto &b : m.baseRig.bones) saved["pmx_bones"].push_back(b.name);
    }
  }
  auto presetDir = MmdConfigDirectory() / L"rig-presets";
  m.loader = std::async(std::launch::async, [kind, owner, path, saved, presetDir]() {
    MmdLoadResult result;
    result.kind = kind;
    try {
      auto selected = path;
      if (selected.empty()) {
        wchar_t name[32768] = {};
        OPENFILENAMEW ofn = {};
        ofn.lStructSize = sizeof(ofn);
        ofn.hwndOwner = owner;
        ofn.lpstrFile = name;
        ofn.nMaxFile = 32768;
        ofn.lpstrFilter =
            kind == 5 ? L"Music (WAV/MP3/M4A/AAC/WMA/FLAC)\0*.wav;*.mp3;*.m4a;*.aac;*.wma;*.flac\0All files\0*.*\0\0" :
            kind == 6 ? L"VMD camera\0*.vmd\0\0" :
            kind >= 3 ? L"MMD rig preset\0*.mmdrig.json;*.json\0\0" :
            kind == 2 ? L"PMX skeleton\0*.pmx\0\0" : L"VMD motion\0*.vmd\0\0";
        ofn.Flags = (kind == 4 ? OFN_OVERWRITEPROMPT : OFN_FILEMUSTEXIST) | OFN_PATHMUSTEXIST | OFN_NOCHANGEDIR |
                    OFN_EXPLORER | OFN_ENABLEHOOK;
        if (kind == 3 || kind == 4) {
          std::filesystem::create_directories(presetDir);
          ofn.lpstrInitialDir = presetDir.c_str();
          ofn.lpstrDefExt = L"mmdrig.json";
        }
        ofn.lpfnHook = MmdDialogHook;
        if (!(kind == 4 ? GetSaveFileNameW(&ofn) : GetOpenFileNameW(&ofn))) {
          s_mmdDialog.store(nullptr);
          DWORD error = CommDlgExtendedError();
          if (error)
            throw std::runtime_error("File dialog failed: " +
                                     std::to_string(error));
          result.cancelled = true;
          return result;
        }
        s_mmdDialog.store(nullptr);
        selected = name;
      }
      result.file = mmd::Utf8(selected.wstring());
      if (kind == 5) {
        result.music = mmd::DecodeAudio(selected, s_mmdClosing);
        return result;
      }
      if (kind == 4) {
        auto temporary = selected; temporary += L".tmp";
        auto text = saved.dump(2);
        if (text.size() > 1024 * 1024) throw std::runtime_error(u8"适配预设超过 1 MiB");
        {
          std::ofstream f(temporary, std::ios::binary | std::ios::trunc);
          f.write(text.data(), std::streamsize(text.size()));
          f.flush();
          if (!f) throw std::runtime_error(u8"无法写入适配预设");
        }
        if (!MoveFileExW(temporary.c_str(), selected.c_str(), MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH))
          throw std::runtime_error(u8"无法替换适配预设文件");
        return result;
      }
      if (kind == 3 && std::filesystem::file_size(selected) > 1024 * 1024)
        throw std::runtime_error(u8"适配预设超过 1 MiB");
      if (kind == 3) {
        auto bytes = mmd::ReadFile(selected);
        result.adaptation = nlohmann::json::parse(bytes.begin(), bytes.end());
        int pose; mmd::IkMode ik;
        mmd::ReadAdaptation(result.adaptation, pose, ik);
        mmd::ReadAmplitude(result.adaptation);
        mmd::ReadNativeCloth(result.adaptation);
      } else if (kind == 2)
        result.rig = mmd::ReadPmx(mmd::ReadFile(selected), mmd::Decode);
      else {
        result.clip = mmd::ReadVmdFile(selected);
        if (result.clip.empty())
          throw std::runtime_error("VMD contains no bone, face or camera motion");
        if (kind == 6 && result.clip.cameras.empty())
          throw std::runtime_error("VMD contains no camera keyframes; previous camera retained");
        if (kind == 1) {
          bool eyes = false;
          for (auto &kv : result.clip.bones)
            eyes = eyes || mmd::EyeBone(kv.first);
          if (result.clip.morphs.empty() && !eyes)
            throw std::runtime_error("No facial/eye tracks to append");
        }
      }
    } catch (const std::exception &e) {
      result.error = e.what();
    }
    return result;
  });
  m.loading = true;
}
static void MmdPollLoad() {
  auto &m = g_mmd;
  if (!m.loading ||
      m.loader.wait_for(std::chrono::seconds(0)) != std::future_status::ready)
    return;
  auto r = m.loader.get();
  m.loading = false;
  if (r.cancelled)
    return;
  if (!r.error.empty()) {
    m.status = r.error;
    Log("[MMD] load error file=%s: %s", r.file.c_str(), r.error.c_str());
    return;
  }
  if (r.kind == 5) {
    m.audio.setClip(std::move(r.music));
    m.musicFile = r.file;
    m.musicError.clear();
    m.musicEnabled = true;
    m.status = u8"音乐已就绪，随动作播放";
    return;
  }
  if (r.kind == 6) {
    m.cameraTrack=std::move(r.clip.cameras);m.cameraFile=r.file;
    MmdCameraTrackChanged();MmdUpdateDuration();
    m.status=u8"镜头已导入，随动作时间轴播放";
    Log("[MMD-CAMERA] imported %s: keys=%zu",r.file.c_str(),m.cameraTrack.size());
    return;
  }
  if (r.kind == 4) {
    m.adaptationFile = r.file;
    m.status = u8"适配预设已保存：" + r.file;
    return;
  }
  if (r.kind == 3) {
    try {
      const auto &j = r.adaptation;
      if (j.value("requires_pmx", false)) {
        std::vector<std::string> names;
        for (const auto &b : m.baseRig.bones) names.push_back(b.name);
        if (!m.reference || names != j.at("pmx_bones").get<std::vector<std::string>>())
          throw std::runtime_error(u8"此预设需要先选择对应 PMX 骨架参考");
      } else if (m.reference) {
        throw std::runtime_error(u8"此预设用于内置骨架，请先点击使用内置骨架");
      }
      int pose; mmd::IkMode ik;
      auto a = mmd::ReadAdaptation(j, pose, ik);
      auto amplitude = mmd::ReadAmplitude(j);
      auto cloth = mmd::ReadNativeCloth(j);
      if (MmdApplyAdaptation(a, pose)) {
        m.amplitude = amplitude;
        s_skirtHipRadiusDelta.store(cloth.hipRadius);
        s_collisionGeometry.store(cloth.geometry);
        s_clothRibbonDamping.store(cloth.ribbonDamping);
        ClothBoneQueueCommand(0,!cloth.enhancement);
        s_skirtDirty.store(true);
        m.ikMode = ik; m.adaptationFile = r.file;
        m.status = u8"适配预设已载入：" + r.file;
      }
    } catch (const std::exception &e) {
      m.status = std::string(u8"保留原适配：") + e.what();
    }
    return;
  }
  if (r.kind == 2) {
    try {
      auto adapted = mmd::AdaptRig(r.rig, m.adaptation);
      m.baseRig = std::move(r.rig); m.rig = std::move(adapted);
    } catch (const std::exception &e) {
      m.status = std::string(u8"PMX 与当前适配不兼容，保留原骨架：") + e.what();
      return;
    }
    ++m.adaptationRevision;
    m.reference = true;
    m.autoScale = true;
    m.referenceFile = r.file;
  } else if (r.kind == 1)
    mmd::AppendFace(m.clip, r.clip);
  else {
    if (!r.clip.cameras.empty()) {
      m.cameraTrack.clear();m.cameraFile.clear();m.cameraSettings.enabled=true;
    }
    m.clip = std::move(r.clip);
    m.file = r.file;
    m.timeline.stop();
    MmdCameraTrackChanged();
    if(g_mmdSquadBridge.selectSingle)g_mmdSquadBridge.selectSingle();
  }
  MmdUpdateDuration();
  MmdMapMorphs();
  MmdReport();
  m.profileRevision = -1;
  m.status = u8"导入完成";
  Log("[MMD] imported %s: bone_keys=%zu morph_keys=%zu frames=%u",
      r.file.c_str(), m.clip.boneKeys, m.clip.morphKeys, m.clip.lastFrame);
}
static bool MmdCharacterReady() {
  return g_charAnimator && g_mainCharEntity == g_captureEntity &&
         !g_charChanged && CharAnimatorAlive() && s_humanBoneCount > 0;
}
static bool MmdPrepareProfile() {
  auto &m = g_mmd;
  if(!ClothOnMainThread()) {
    m.calibrateRequested=true;m.calibrationStatus=u8"等待游戏线程读取 Avatar 骨架";return false;
  }
  if (!MmdCharacterReady()) {
    m.status = u8"正在等待当前角色骨架，请稍后重试或点击刷新骨骼";
    return false;
  }
  if (m.profileAnimator == g_charAnimator &&
      m.profileRevision == s_bonesRev && m.profile.valid())
    return true;
  m.profile = MmdCurrentProfile();
  if (MmdBindCalibration(m.profile)) {
    m.calibrationStatus = u8"Avatar 自动适配完成，无需手动 T 姿";
    try {
      MmdSaveCalibration(m.profile);
    } catch (const std::exception &e) {
      Log("[MMD] calibration cache: %s", e.what());
    }
  } else if (MmdLoadCalibration(m.profile)) {
    m.calibrationStatus=u8"已读取保存的备用校准";
  } else {
    m.calibrationStatus =
        u8"无法取得完整 Avatar 骨架，请使用备用 T 姿校准。" + s_mmdCalibrationDetail;
    m.status = m.calibrationStatus;
    return false;
  }
  m.profileRevision = s_bonesRev;
  m.profileAnimator = g_charAnimator;
  return true;
}
static void MmdCaptureSession() {
  auto &m = g_mmd;
  auto &s = m.session;
  s = MmdSession{};
  s.active = true;
  s.bodyOwned = m.preview || !m.clip.bones.empty() || !m.clip.morphs.empty();
  s.cameraSession = ++mmd_camera::nextSession;
  s.animator = g_charAnimator;
  s.root = GetCharRootTransform();
  s.revision = s_bonesRev;
  s.wasFrozen = g_frozen;
  s.freezeAccessories = g_freezeAccessories;
  s.animatorWasEnabled = g_animatorWasEnabled;
  s.references = std::make_shared<GripReferences>();
  auto retain = [&](void *object) {
    if (object && il2cpp_gchandle_new && il2cpp_gchandle_free) {
      uint32_t handle = il2cpp_gchandle_new(object, false);
      if (handle) s.references->handles.push_back(handle);
    }
  };
  retain(s.animator);
  retain(s.root);
  s.rootPos = GetBoneLocalPos(s.root);
  s.rootRot = GetBoneLocalRot(s.root);
  s.anchorWorldValid=MmdReadMatrix(g_transform_get_localToWorldMatrix,s.root,&s.anchorWorld);
  if (!s.anchorWorldValid) {
    s.anchorWorld=mmd::TRS(GetBoneWorldPos(s.root),GetBoneWorldRot(s.root));
    s.anchorWorldValid=true;
  }
  s.cameraBasis=NormQ(mmd::Rotation(s.anchorWorld)*m.mapper.sourceBasis());
  s.cameraHeight=mmd::CameraTargetHeight(m.profile);
  if (m.clip.bones.empty()) {
    // Camera-only/face-only clips do not require a calibrated body rig.
    auto at=[&](int role) {for(int i=0;i<s_humanBoneCount;++i)
      if(s_humanBones[i].humanBone==role && UnityObjAlive(s_humanBones[i].transform))
        return GetBoneWorldPos(s_humanBones[i].transform);return Vec3{};};
    Vec3 across=at(13)-at(14);across.y=0;
    if (Len(across)>1e-4f) {
      Vec3 x=Norm(across),up{0,1,0};s.cameraBasis=mmd::Basis(x,up,Norm(Cross(x,up)));
    } else s.cameraBasis=mmd::Rotation(s.anchorWorld);
  }
  if (!s.bodyOwned) return;
  for (auto &b : s_allBones) {
    retain(b.transform);
    s.transforms.push_back({b.transform, GetBoneLocalPos(b.transform),
                            GetBoneLocalRot(b.transform)});
  }
  s.accessories = s_accessoryBones;
  s.chains = s_accessoryChains;
  CollectIKComponents();
  std::set<void *> components;
  components.insert(g_charAnimator);
  components.insert(g_charAnimComp);
  components.insert(s_animatorMono);
  for (int i = 0; i < s_ikBipedCount; i++)
    components.insert(s_ikBiped[i]);
  for (int i = 0; i < s_ikGrounderCount; i++)
    components.insert(s_ikGrounder[i]);
  for (int i = 0; i < s_ikLookAtCount; i++)
    components.insert(s_ikLookAt[i]);
  for (int i = 0; i < s_ikDamperCount; i++)
    components.insert(s_ikDamper[i]);
  for (auto &b : s_accessoryBones)
    for (void *c : b.physicsComps)
      components.insert(c);
  for (void *c : components)
    if (LiveBehaviour(c)) {
      retain(c);
      s.components.push_back({c, MmdEnabled(c)});
    }
  for(const auto &c:s.components) {
    auto name=il2cpp_class_get_name(il2cpp_object_get_class(c.component));
    if(name&&!strcmp(name,"GrounderBipedIK")){mmd_terrain::Configure(s.terrain,c.component);break;}
  }
  g_freezeAccessories = m.freezeCloth;
  if (!m.preview && !m.freezeCloth) {
    for (int i=0; i<s_humanBoneCount; ++i) if (s_humanBones[i].humanBone==Head) {
      float h=GetBoneWorldPos(s_humanBones[i].transform).y-GetBoneWorldPos(s.root).y;
      s_clothCharacterHeight=std::isfinite(h)&&h>.1f&&h<5.f?h:1.245f;
      break;
    }
    ClothRequestPlayback(true, !g_frozen);
  }
  if (g_frozen && !m.freezeCloth)
    SetAllPhysicsEnabled(true, true);
  if (!g_frozen)
    FreezeCharacter();
  if (m.freezeCloth) {
    CaptureAccessorySnapshot();
    SetAllPhysicsEnabled(false, true);
  }
  MmdHideProps(true);
}
static void MmdStop(void *nextEntity) {
  std::lock_guard<std::recursive_mutex> lock(g_poseMutex);
  if(nextEntity&&g_mmdSquadBridge.characterChanging)g_mmdSquadBridge.characterChanging(nextEntity);
  else if(g_mmdSquadBridge.stop)g_mmdSquadBridge.stop();
  auto &m = g_mmd;
  auto &s = m.session;
  m.timeline.stop();
  m.countdown.cancel();g_mmdCountdownDisplay.store(0);
  mmd_camera::Stop();
  ClothRequestPlayback(false);
  MmdCancelStart();
  m.audio.close();
#if POSER_ENABLE_XXMI_BRIDGE
  ModBridgeRelease();
#endif
  SMCMotionPublish({});
  poser_gaze::Release(false);
  InterlockedExchange(&g_mmdOwnsPose, 0);
  m.preview = false;
  if (!s.active)
    return;
  if (!s.bodyOwned) {
    s.active=false;s.references.reset();m.status=u8"镜头已停止，等待游戏回调恢复相机";
    return;
  }
  bool ownerAlive = UnityObjAlive(s.animator) && UnityObjAlive(s.root);
  // Handles belong to the recorded actor, never implicitly to the new actor.
  if (ownerAlive)
    for (auto &b : s.transforms)
      MmdRawPose(b.transform, b.pos, b.rot);
  if (!s.wasFrozen) {
    for (size_t i = 0; i < g_frozenGrips.size(); i++)
      if (g_frozenGrips[i].animator == s.animator) {
        g_frozenGrips.erase(g_frozenGrips.begin() + i);
        break;
      }
  }
  if (ownerAlive) {
    for (auto &c : s.components) MmdEnable(c.component, c.enabled);
    for (const auto &prop : s.props) MmdSetActive(prop.object, prop.active);
  }
  if (g_charAnimator == s.animator) {
    g_frozen = s.wasFrozen;
    g_freezeAccessories = s.freezeAccessories;
    g_animatorWasEnabled = s.animatorWasEnabled;
    s_accessoryBones = s.accessories;
    s_accessoryChains = s.chains;
    if (ownerAlive) CapturePoseSnapshot();
    else {
      // Preserve the pre-playback body pose in memory even if Unity has
      // already destroyed the original actor during the switch.
      for (int i = 0; i < s_humanBoneCount; ++i)
        for (const auto &saved : s.transforms)
          if (s_humanBones[i].transform == saved.transform) {
            s_humanBones[i].localPos = saved.pos;
            s_humanBones[i].localRot = saved.rot;
            break;
          }
    }
  }
  s.active = false;
  s.terrain={};
  s.references.reset();
  m.preview = false;
  m.status = u8"已停止并恢复播放前状态";
  Log("[MMD] stopped/restored actor=%p", s.animator);
}
static void MmdCharacterChanging(void *nextEntity=nullptr) {
  // A click made for the incoming selection must survive its delayed Animator
  // capture. Requests belonging to the outgoing actor never carry over.
  const auto request=s_mmdStartRequest;const auto target=s_mmdStartEntity;
  const auto deadline=s_mmdStartDeadline;
  const bool pending=request.active&&target&&target==nextEntity&&MmdNow()<deadline&&
      (target!=g_mainCharEntity||!g_mmd.session.active);
  MmdStop(nextEntity);
  if(pending) {s_mmdStartRequest=request;s_mmdStartEntity=target;s_mmdStartDeadline=deadline;}
  auto &m = g_mmd;
  m.profileRevision = -1;
  m.profileAnimator = nullptr;
  m.profile = mmd::RetargetProfile{};
  m.calibrationStatus = u8"角色已切换，等待新角色骨架；原角色校准仍保存在文件中";
  m.status = u8"切换角色已停止动作，等待新角色骨架";
}
static bool MmdClothMayAdjustAnchor(void *transform);
static void MmdCountdownUpdate(bool ready) {
  auto &m=g_mmd;const auto now=MmdNow();
  const bool cloth=g_clothPlaybackGate.Holding(1u,s_clothRequestGeneration);
  m.countdown.update(now,ready&&!cloth,m.timeline.state==mmd::PlayState::Playing);
  g_mmdCountdownDisplay.store(m.countdown.display(),std::memory_order_release);
  m.timeline.holdClock(cloth||m.countdown.active,now);
}
static void MmdApplyFrame() {
  if(!ClothOnMainThread())return;
  auto &m = g_mmd;
  auto &s = m.session;
  if (!s.active || m.preview)
    return;
  if (s.animator != g_charAnimator || s.revision != s_bonesRev ||
      !UnityObjAlive(s.animator) || !UnityObjAlive(s.root)) {
    MmdStop();
    return;
  }
  double frame = m.timeline.seconds * 30.;
  if (!s.bodyOwned) {MmdPublishCamera();MmdCountdownUpdate(true);return;}
  if(ClothBlockFirstBodyPose("single-preparation",MmdClothMayAdjustAnchor)) {MmdCountdownUpdate(false);return;}
  m.mapper.sample(frame, m.scale, m.inPlace, m.height, m.ikMode, m.amplitude);
  auto &p = m.mapper.output;
  auto world=s.anchorWorld*mmd::TRS(p.rootOffset,{});
  float ground=m.clip.bones.empty()?0:mmd_terrain::Apply(s.terrain,m.terrain,m.profile,p,world,s.anchorWorld,MmdNow(),m.timeline.seconds);
  mmd::Matrix anchorInverse;Vec3 groundLocal{};
  if(mmd::Inverse(s.anchorWorld,anchorInverse))groundLocal=mmd::terrain::Vector(anchorInverse,{0,ground,0});
  MmdRawPose(s.root, s.rootPos + s.rootRot * (p.rootOffset+groundLocal), s.rootRot);
  for (size_t i = 0; i < p.write.size(); i++) {
    if (!p.write[i] || i >= s_allBones.size())
      continue;
    int role = m.profile.bones[i].role;
    bool locked = false;
    for (int h = 0; h < s_humanBoneCount; h++)
      if (s_humanBones[h].humanBone == role)
        locked = s_humanBones[h].locked;
    if (locked)
      continue;
    void *t = s_allBones[i].transform;
    MmdRawPose(t, m.profile.bones[i].localPos, p.localRot[i]);
    for (int h = 0; h < s_humanBoneCount; h++)
      if (s_humanBones[h].transform == t) {
        s_humanBones[h].localPos = m.profile.bones[i].localPos;
        s_humanBones[h].localRot = p.localRot[i];
      }
  }
  SMCMotionFrame face;
  face.gazeCamera=poser_gaze::motionLock;face.gazeStrength=poser_gaze::motionStrength;
  face.active = !m.clip.morphs.empty()||face.gazeCamera;
  face.animator = s.animator;
  face.generation=s_faceGeneration;
  face.settings=m.faceSettings;
  face.profile=m.characterFace;
  for(auto &kv:m.morphMap) {
    float sample=mmd::SampleMorph(m.clip.morphs.at(kv.first),frame);
    int id=kv.second.slider;
    bool calibrated=m.characterFace&&s_characterProfile==m.characterFace&&s_characterBinding.ready&&
      id>=0&&id<int(s_characterBinding.usable.size())&&s_characterBinding.usable[id];
    if(calibrated)face.expressions[id]=(std::max)(face.expressions[id],face_geometry::Clamp(sample*kv.second.gain,0,1));
    int native=kv.second.nativeSlider;
    if(native>=0) {
      float value=sample*kv.second.nativeGain;
      face.weights[native]=face_geometry::Clamp(face.weights[native]+value,0,1);
      if(!calibrated)face.fallbackWeights[native]=face_geometry::Clamp(face.fallbackWeights[native]+value,0,1);
    }
  }
  for (int e = 0; e < 2; e++) {
    int idx = m.profile.roles[21 + e];
    if (idx >= 0 && p.write[idx]) {
      face.active = true;
      face.eyeDriven[e] = true;
      face.eyes[e] = s_allBones[idx].transform;
      face.eyeRotation[e] = p.localRot[idx];
    }
  }
  SMCMotionPublish(face);
#if POSER_ENABLE_XXMI_BRIDGE
  ModBridgeFrame(m.clip, frame);
#endif
  ClothService(true,MmdClothMayAdjustAnchor,frame);
  m.timeline.holdClock(g_clothPlaybackGate.Holding(1u,s_clothRequestGeneration)||m.countdown.active,MmdNow());
  MmdPublishCamera();
  MmdCountdownUpdate(true);
}
static bool MmdClothMayAdjustAnchor(void *transform) {
  if (!transform || transform==g_mmd.session.root) return false;
  for (int i=0;i<s_humanBoneCount;++i) if (s_humanBones[i].transform==transform) return false;
  for (const auto &b:s_accessoryBones) if (b.transform==transform && b.locked) return false;
  for (size_t i=0;i<s_allBones.size() && i<g_mmd.mapper.output.write.size();++i)
    if (s_allBones[i].transform==transform && g_mmd.mapper.output.write[i]) return false;
  return true;
}
static bool MmdStart() {
  auto &m = g_mmd;
  if(MmdSquadBusy()) {m.status=u8"请先完成多人导入或停止多人播放";return false;}
  if (m.loading || m.preview || !MmdHasContent()) return false;
  // Capture cloth originals before suppressing animation, on the Unity thread.
  if (!ClothOnMainThread()) {
    MmdQueueStart(true);m.status=u8"等待游戏线程开始播放";return false;
  }
  if(s_mmdStartRequest.active&&s_mmdStartDeadline&&
      (MmdNow()>=s_mmdStartDeadline||(s_mmdStartEntity&&s_mmdStartEntity!=MmdSelectedEntity()))) {
    MmdCancelStart();m.status=u8"角色选择已改变或等待超时，请重新播放";return false;
  }
  if(ClothSquadRestoring()) {
    MmdQueueStart();
    m.status=u8"等待多人衣物增强恢复";return false;
  }
  if (!MmdCharacterReady()) {
    if(m.session.active)MmdStop();
    MmdQueueStart();
    m.status = u8"正在等待所选角色骨架，就绪后自动开始播放";
    return false;
  }
  MmdCancelStart();
  if (m.session.active) {
    m.timeline.play(MmdNow());
    return true;
  }
  if (m.clip.bones.empty()) {
    if (!g_charAnimator || !UnityObjAlive(g_charAnimator))
      return false;
    m.profile = MmdCurrentProfile();
    m.profileRevision = -1;
  } else if (!MmdPrepareProfile())
    return false;
  if (!m.clip.bones.empty() || !m.clip.morphs.empty())
    m.mapper.bind(m.rig, m.clip, m.profile, mmd::AdaptedRoles(m.adaptation), m.adaptation.tracks);
  if (!m.clip.bones.empty() && m.autoScale)
    m.scale = m.mapper.suggestedScale;
  MmdCaptureSession();
  m.countdown.arm(m.countdownEnabled,m.countdownSeconds);
  if(m.countdown.active)m.timeline.seconds=0;
  m.timeline.holdClock(g_clothPlaybackGate.Holding(1u,s_clothRequestGeneration)||m.countdown.active,MmdNow());
  InterlockedExchange(&g_mmdOwnsPose, 1);
  MmdUpdateDuration();
  m.timeline.play(MmdNow());
  m.status = u8"播放中";
  MmdApplyFrame();
  Log("[MMD] playing %s, actor=%p scale=%.5f", m.file.c_str(), g_charAnimator,
      m.scale);
  return true;
}
static void MmdSeekOrStart(double seconds) {
  if (!g_mmd.session.active && !MmdStart()) {
    if (s_mmdStartRequest.active) s_mmdStartRequest.seek(seconds);
    return;
  }
  MmdSeek(seconds);MmdApplyFrame();
}
static void MmdTick() {
  try {
    auto &m = g_mmd;
    MmdLoadFaceSettings();
    MmdPollCharacterFaces();
    MmdPollLoad();
#if POSER_ENABLE_XXMI_BRIDGE
    ModBridgeService();
#endif
    MmdSelectCameraSettings();
    if(m.calibrateRequested&&ClothOnMainThread()&&!m.session.active&&!MmdSquadBusy()) {
      m.calibrateRequested=false;MmdPrepareProfile();
    }
    if(g_mmdSquadBridge.tick && g_mmdSquadBridge.tick())return;
    if (s_mmdStartRequest.active && ClothOnMainThread()) {
      const auto request=s_mmdStartRequest;
      if (MmdStart()) {
        if(std::isfinite(request.seconds)) {m.countdown.cancel();g_mmdCountdownDisplay.store(0);}
        if(request.paused)m.countdown.pause();
        request.apply(m.timeline,MmdNow());
      }
    }
    ClothRequestPlayback(m.session.active && m.session.bodyOwned && !m.preview && !m.freezeCloth);
    if (m.session.active && (!MmdCharacterReady() ||
                             m.session.animator != g_charAnimator ||
                             m.session.revision != s_bonesRev ||
                             !UnityObjAlive(m.session.animator))) {
      MmdStop();
      return;
    }
    m.timeline.holdClock(g_clothPlaybackGate.Holding(1u,s_clothRequestGeneration)||m.countdown.active,MmdNow());
    if (MmdOwnsPose()) {
      m.timeline.tick(MmdNow());
      MmdApplyFrame();
    }
    ClothService();
    MmdSyncAudio();
    if (m.session.active)
      MmdHideProps();
  } catch (const std::exception &e) {
    MmdStop();
    g_mmd.status = e.what();
    Log("[MMD] playback failed: %s", e.what());
  }
}
// Reset keeps the playback anchor and holds frame zero; Stop restores the pose
// captured before playback. Pausing never captures or replaces that session.
static void MmdPlaybackCommand(int command, bool allowSquad = true) {
  // A live single session/calibration always owns transport. An idle squad
  // selection must never swallow pause/stop for the character on screen.
  if(allowSquad && !g_mmd.session.active && !g_mmd.preview && !s_mmdStartRequest.active &&
     g_mmdSquadBridge.command && g_mmdSquadBridge.command(command))return;
  if(!allowSquad && g_mmdSquadBridge.selectSingle)g_mmdSquadBridge.selectSingle();
  try {
    auto &m = g_mmd;
    Log("[MMD] command begin=%d state=%d frame=%.2f actor=%p", command,
        int(m.timeline.state), m.timeline.seconds * 30, m.session.animator);
    if (command == 2) {
      MmdStop();
      return;
    }
    if(m.preview){m.status=u8"正在 T 姿校准：请先确认保存或取消校准，再播放动作";return;}
    if(m.loading){m.status=u8"动作仍在读取，请完成后再使用播放快捷键";return;}
    if (command == 0) {
      MmdStart();
    } else if (command == 1 && s_mmdStartRequest.active) {
      s_mmdStartRequest.pause();
    } else if (command == 1 && MmdOwnsPose()) {
      m.countdown.pause();
      m.timeline.pause(MmdNow());
      m.status = u8"已暂停，保持当前姿态";
    } else if (command == 3) {
      MmdSeekOrStart(0);
      m.status = u8"已回到动作首帧，暂停中";
    }
    MmdSyncAudio();
    Log("[MMD] command=%d state=%d frame=%.2f actor=%p", command,
        int(m.timeline.state), m.timeline.seconds * 30, m.session.animator);
  } catch (const std::exception &e) {
    MmdStop();
    g_mmd.status = e.what();
    Log("[MMD] command failed: %s", e.what());
  }
}
static void MmdBeginCalibration() {
  auto &m = g_mmd;
  if(MmdSquadBusy()) {m.status=u8"请先停止多人播放或完成导入";return;}
  if (m.session.active || m.loading)
    return;
  if (!MmdCharacterReady()) {
    m.status = u8"正在等待当前角色骨架，请稍后重试或点击刷新骨骼";
    return;
  }
  m.preview = true;
  MmdCaptureSession();
  auto p = MmdCurrentProfile();
  Vec3 up = Conj(GetBoneWorldRot(m.session.root)) * Vec3{0, 1, 0};
  if (!mmd::MakeCalibrationTPose(p, up)) {
    MmdStop();
    m.calibrationStatus =
        u8"必要骨骼缺失或方向退化，无法生成 T 姿；已恢复原姿态";
    return;
  }
  for (size_t i = 0; i < p.bones.size(); ++i)
    if (p.bones[i].role >= 0 && p.bones[i].role != LeftEye &&
        p.bones[i].role != RightEye && p.bones[i].role != Jaw)
      MmdRawPose(s_allBones[i].transform, p.bones[i].localPos,
                 p.bones[i].localRot);
  CapturePoseSnapshot();
  if (m.freezeCloth)
    CaptureAccessorySnapshot();
  m.status = u8"正在预览 T 姿";
  m.calibrationStatus = u8"校准预览：可用骨骼面板调整 T 姿，再确认保存或取消";
}
static void MmdConfirmCalibration() {
  auto &m = g_mmd;
  if (!m.preview)
    return;
  if (!MmdCharacterReady() || m.session.animator != g_charAnimator ||
      m.session.revision != s_bonesRev) {
    MmdCharacterChanging();
    return;
  }
  auto p = MmdCurrentProfile();
  for (auto &b : p.bones)
    b.calibrated = true;
  p.globals();
  Vec3 up = Conj(GetBoneWorldRot(m.session.root)) * Vec3{0, 1, 0};
  if (!mmd::CalibrationTPoseValid(p, up)) {
    m.status = u8"校准姿态无效：请保持躯干直立、双臂水平、双腿伸直，再确认保存";
    return;
  }
  try {
    MmdSaveCalibration(p);
    MmdStop();
    m.profile = std::move(p);
    m.profileRevision = s_bonesRev;
    m.profileAnimator = g_charAnimator;
    m.calibrationStatus = u8"手动校准已保存";
  } catch (const std::exception &e) {
    m.status = e.what();
  }
}
