// Android managed adapter for the shared EIEM DirectVmd parser, sampler and
// pose math. Never includes the desktop raw-layout/SEH/hook implementation.
// Called by the existing director's main-thread PublishClock contract.
#ifndef BE_EIEM_SLOT
#error Define BE_EIEM_SLOT before including android_slot.inc
#endif
#include <algorithm>
#include <array>
#include <atomic>
#include <chrono>
#include <cmath>
#include <condition_variable>
#include <cstdio>
#include <cstring>
#include <functional>
#include <iomanip>
#include <limits>
#include <locale>
#include <map>
#include <memory>
#include <mutex>
#include <new>
#include <set>
#include <sstream>
#include <stdexcept>
#include <string>
#include <thread>
#include <type_traits>
#include <utility>
#include <vector>
#include "android_encoding.h"
#include "eiem_slot.h"
#if !defined(BE_EIEM_ANDROID_OFFLINE_TEST)
#include "../../../../shared/android_compat/loaded_il2cpp.h"
#else
void* BeEiemOfflineMetadataExport(const char* name);
#endif

#define BE_EIEM_CAT2(a, b) a##b
#define BE_EIEM_CAT(a, b) BE_EIEM_CAT2(a, b)
#define BE_EIEM_NS BE_EIEM_CAT(eiem_slot_, BE_EIEM_SLOT)
#define BE_EIEM_API BE_EIEM_CAT(Slot, BE_EIEM_SLOT)
namespace BE_EIEM_NS {
#include "vmd_parser.h"
#include "direct_vmd_pose.h"
#include "direct_vmd_source_sample.h"

namespace {
using BetterEndfield::EiemSlot::Api;
using BetterEndfield::EiemBody::LoadState;
using BetterEndfield::EiemBody::Options;
using Id = DirectVmdBoneId;
constexpr size_t N = DIRECT_VMD_BONE_COUNT;
const BE_HostApiV1* host = nullptr;
std::thread::id mainThread;
bool initialized = false;
bool available = false;
bool active = false;
bool animatorWasEnabled = false;
Options options;
void Log(const std::string& message) {
  if (host && host->log) host->log(host->context, "betterendfield.camera",
      ("EIEM Android[" + std::to_string(BE_EIEM_SLOT + 1) + "]: " + message).c_str());
}
bool OnMainThread() { return std::this_thread::get_id() == mainThread; }

// GC pins own every retained managed pointer. Temporary boxes are pinned before
// unboxing/copying; the only dereference is the payload returned by host.unbox.
struct Pin {
  void* object = nullptr;
  uint32_t handle = 0;
  Pin() = default;
  explicit Pin(void* value) {
    if (value && host && host->gchandle_new) {
      handle = host->gchandle_new(host->context, value, 1);
      if (handle) object = value;
    }
  }
  Pin(const Pin&) = delete;
  Pin& operator=(const Pin&) = delete;
  Pin(Pin&& other) noexcept : object(std::exchange(other.object, nullptr)),
      handle(std::exchange(other.handle, 0)) {}
  Pin& operator=(Pin&& other) noexcept {
    Reset(); object = std::exchange(other.object, nullptr);
    handle = std::exchange(other.handle, 0); return *this;
  }
  void Reset() {
    if (handle && host && host->gchandle_free) host->gchandle_free(host->context, handle);
    object = nullptr; handle = 0;
  }
  ~Pin() { Reset(); }
  explicit operator bool() const { return object && handle; }
};
Pin entity, animator, ownerRoot, movement;

struct Method {
  BE_MethodDescriptorV1 descriptor;
  BE_ResolvedMethodV1 resolved{};
};
enum MethodId { GetBone, GetEnabled, SetEnabled, ComponentTransform,
  GetLocalPosition, GetLocalRotation, GetPosition, GetRotation,
  SetPosition, SetRotation, ObjectAlive, ChildCount, GetChild, ObjectName,
  ComponentGO, GetComponents, ArrayLength, ArrayValue, SetLocalRotation, GetParent, SetLocalPosition, Count };
Method methods[] = {
  {{"UnityEngine.AnimationModule.dll", "UnityEngine", "Animator", "GetBoneTransform", "UnityEngine.HumanBodyBones", "UnityEngine.Transform", 1}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Behaviour", "get_enabled", "", "System.Boolean", 0}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Behaviour", "set_enabled", "System.Boolean", "System.Void", 1}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Component", "get_transform", "", "UnityEngine.Transform", 0}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_localPosition", "", "UnityEngine.Vector3", 0}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_localRotation", "", "UnityEngine.Quaternion", 0}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_position", "", "UnityEngine.Vector3", 0}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_rotation", "", "UnityEngine.Quaternion", 0}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "set_position", "UnityEngine.Vector3", "System.Void", 1}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "set_rotation", "UnityEngine.Quaternion", "System.Void", 1}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "op_Implicit", "UnityEngine.Object", "System.Boolean", 1}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_childCount", "", "System.Int32", 0}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "GetChild", "System.Int32", "UnityEngine.Transform", 1}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "get_name", "", "System.String", 0}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Component", "get_gameObject", "", "UnityEngine.GameObject", 0}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "GetComponents", "System.Type", "UnityEngine.Component[]", 1}},
  {{"mscorlib.dll", "System", "Array", "get_Length", "", "System.Int32", 0}},
  {{"mscorlib.dll", "System", "Array", "GetValue", "System.Int32", "System.Object", 1}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "set_localRotation", "UnityEngine.Quaternion", "System.Void", 1}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_parent", "", "UnityEngine.Transform", 0}},
  {{"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "set_localPosition", "UnityEngine.Vector3", "System.Void", 1}},
};
static_assert(sizeof(methods) / sizeof(*methods) == Count);
bool Call(MethodId id, void* object, void** args, void** result = nullptr) {
  if (!OnMainThread() || !host || !methods[id].resolved.method_info) return false;
  void* exception = nullptr;
  void* value = host->runtime_invoke(host->context, methods[id].resolved.method_info,
                                    object, args, &exception);
  if (exception) return false;
  if (result) *result = value;
  return true;
}
template <class T> bool Read(MethodId id, void* object, T& value, void** args = nullptr) {
  void* boxed = nullptr;
  if (!Call(id, object, args, &boxed)) return false;
  Pin pin(boxed);
  if (!pin) return false;
  void* payload = host->object_unbox(host->context, pin.object);
  if (!payload) return false;
  std::memcpy(&value, payload, sizeof(T));
  return true;
}
bool Alive(void* object) {
  if (!object) return false;
  bool alive = false;
  void* args[] = {object};
  return Read(ObjectAlive, nullptr, alive, args) && alive;
}
bool Set(MethodId id, void* object, void* value) {
  void* args[] = {value};
  return Call(id, object, args);
}
bool Finite(VmdQuaternion q) {
  return DirectVmdFinite(q.x) && DirectVmdFinite(q.y) &&
      DirectVmdFinite(q.z) && DirectVmdFinite(q.w);
}
bool World(void* transform, DirectVmdWorldPosePod& pose) {
  return Alive(transform) && Read(GetPosition, transform, pose.position) &&
      Read(GetRotation, transform, pose.rotation) &&
      eiem_source::Finite(pose.position) && Finite(pose.rotation);
}

struct Clip {
  std::unique_ptr<VmdFile> motion;
  std::unique_ptr<VmdFile> face;
  eiem_source::Prepared source = eiem_source::Prepare(eiem_source::Config());
  eiem_source::ClipBinding binding;
  uint64_t generation = 0;
};
std::mutex workerMutex;
std::condition_variable wake;
std::thread worker;
bool running = false;
uint64_t requested = 0;
std::string motionPath, facePath;
std::shared_ptr<Clip> readyClip;
DirectVmdSampleFrame latest{};
std::atomic<LoadState> loadState{LoadState::Idle};
std::atomic<double> duration{0}, clockSeconds{0};
std::atomic<bool> clockPlaying{false};
std::atomic<int64_t> clockStamp{0};
int64_t Now() {
  return std::chrono::duration_cast<std::chrono::nanoseconds>(
      std::chrono::steady_clock::now().time_since_epoch()).count();
}
void Worker() {
  uint64_t handled = 0, sequence = 0;
  std::shared_ptr<Clip> clip;
  while (true) {
    uint64_t revision;
    std::string motion, face;
    {
      std::unique_lock lock(workerMutex);
      if (!running) break;
      revision = requested;
      if (revision != handled) { motion = motionPath; face = facePath; }
    }
    if (revision != handled) {
      std::shared_ptr<Clip> loaded;
      // This catch handles parser/allocation errors only, never native faults.
      try {
        loaded = std::make_shared<Clip>();
        loaded->motion.reset(LoadVmd(motion.c_str()));
        if (!loaded->motion || !loaded->motion->loaded)
          throw std::runtime_error(loaded->motion ? loaded->motion->error : "VMD allocation failed");
        loaded->binding = eiem_source::BindClip(loaded->source, *loaded->motion);
        if (!face.empty()) {
          loaded->face.reset(LoadVmd(face.c_str()));
          if (!loaded->face || !loaded->face->loaded) throw std::runtime_error("Face VMD load failed");
        }
        loaded->generation = revision;
      } catch (const std::exception& e) {
        Log(std::string("load rejected: ") + e.what()); loaded.reset();
      }
      handled = revision;
      std::lock_guard lock(workerMutex);
      if (requested == revision) {
        readyClip = clip = std::move(loaded);
        latest = {};
        duration.store(clip ? double(clip->motion->totalFrames) / kVmdFramesPerSecond : 0);
        loadState.store(clip ? LoadState::Ready : LoadState::Failed, std::memory_order_release);
      }
    }
    if (clip) {
      double seconds = clockSeconds.load(std::memory_order_acquire);
      const bool playing = clockPlaying.load(std::memory_order_acquire);
      if (playing) seconds += std::clamp(double(Now() - clockStamp.load()) / 1e9, 0.0, 0.1);
      const double frame = std::clamp(seconds * kVmdFramesPerSecond, 0.0, double(clip->motion->totalFrames));
      DirectVmdSampleFrame sample{};
      DirectVmdSampleClip(*clip->motion, frame, ++sequence, 0, clip->generation, 0,
          playing ? DirectVmdPlaybackState::Playing : DirectVmdPlaybackState::Paused, &sample, false);
      try {
        eiem_source::Sample(clip->source, clip->binding, *clip->motion, frame, &sample);
        if (clip->face) DirectVmdSampleMorphChannels(*clip->face, frame, &sample);
        std::lock_guard lock(workerMutex);
        if (clip->generation == requested) latest = sample;
      } catch (const std::exception& e) {
        Log(std::string("sample rejected: ") + e.what());
        std::lock_guard lock(workerMutex);
        if (clip->generation == requested) {
          readyClip.reset(); latest = {}; loadState.store(LoadState::Failed);
        }
        clip.reset();
      }
    }
    std::unique_lock lock(workerMutex);
    wake.wait_for(lock, std::chrono::milliseconds(clockPlaying.load() ? 1 : 20),
                  [&] { return !running || requested != handled; });
  }
}

struct Bone {
  Pin transform;
  DirectVmdWorldPosePod entry{};
  DirectVmdWorldPosePod natural{};
  DirectVmdBindNodePod bind{};
  VmdQuaternion alignment{0, 0, 0, 1};
};
std::array<Bone, N> bones;
std::map<void*,DirectVmdLocalPosePod> avatarLocalPoses;
std::array<DirectVmdWorldPosePod, N> poses;
DirectVmdWorldPosePod anchor{}, entryRoot{}, sharedAnchor{};
bool sharedAnchorValid = false;
float baseScale = 0;
float naturalHeight = 0;
VmdVec3 placement{};
bool placementReady = false;
uint64_t activeClipGeneration = 0;
#include "android_managed_features.inc"
#include "android_terrain.inc"

int HumanBone(size_t i) {
  static constexpr int body[] = {-1, -1, -1, 0, 7, 8, 9, 10, 11, 13, 15, 17,
    12, 14, 16, 18, 1, 3, 5, 19, 2, 4, 6, 20, -1, -1, -1, -1, -1, -1};
  if (i < sizeof(body) / sizeof(*body)) return body[i];
  if (i <= size_t(Id::LeftLittle3)) return 24 + int(i - size_t(Id::LeftThumb0));
  if (i <= size_t(Id::RightLittle3)) return 39 + int(i - size_t(Id::RightThumb0));
  if (i == size_t(Id::LeftEye)) return 21;
  if (i == size_t(Id::RightEye)) return 22;
  return -1;
}
void ReleaseBones() {
  for (auto& bone : bones) bone.transform.Reset();
  avatarLocalPoses.clear();
  baseScale = 0; placementReady = false;
}
void Stop(const char*) {
  if (!OnMainThread()) return;
  const bool wasActive = active;
  active = false;
  ResetTerrain();
  const bool faceRestored = FaceRestore();
  ClothRestore();
  if (faceRestored) face = {};
  for (auto& channel : twists) for (auto& target : channel) {
    if (target.transform && Alive(target.transform.object)) Set(SetLocalRotation, target.transform.object, &target.entry);
    target.transform.Reset();
  }
  if (wasActive && Alive(entity.object)) {
    for (auto& bone : bones) if (bone.transform && Alive(bone.transform.object))
      Set(SetRotation, bone.transform.object, &bone.entry.rotation);
    auto& pelvis = bones[size_t(Id::LowerBody)];
    if (pelvis.transform && Alive(pelvis.transform.object)) Set(SetPosition, pelvis.transform.object, &pelvis.entry.position);
  }
  if (wasActive && Alive(animator.object)) {
    bool value = animatorWasEnabled;
    if (!Set(SetEnabled, animator.object, &value)) Log("Animator restore failed");
  }
  ReleaseBones();
}

bool Capture() {
  if (!Alive(animator.object) || !Alive(entity.object)) return false;
  void* root = nullptr;
  if (!Call(ComponentTransform, animator.object, nullptr, &root)) return false;
  ownerRoot = Pin(root);
  if (!ownerRoot || !World(ownerRoot.object, entryRoot)) return false;
  for (size_t i = 0; i < N; ++i) {
    const int human = HumanBone(i);
    if (human < 0) continue;
    int index = human;
    void* args[] = {&index};
    void* transform = nullptr;
    if (!Call(GetBone, animator.object, args, &transform)) return false;
    bones[i].transform = Pin(transform);
    if (!bones[i].transform) continue; // Optional bones absent on this avatar.
    if (!World(bones[i].transform.object, bones[i].entry)) return false;
  }
  for (Id id : {Id::LowerBody, Id::UpperBody, Id::Head, Id::LeftLeg,
                Id::LeftKnee, Id::LeftAnkle, Id::RightLeg, Id::RightKnee, Id::RightAnkle})
    if (!bones[size_t(id)].transform) { Log("required humanoid bone unavailable"); return false; }
  auto nodes = Tree();
  if (!CaptureNaturalBind(nodes)) { Log("Avatar natural bind contract unavailable; refusing animated-pose fallback"); return false; }
  float legLength = 0;
  for (int side = 0; side < 2; ++side) {
    const size_t thigh = size_t(side ? Id::RightLeg : Id::LeftLeg);
    legLength += DirectVmdLength(DirectVmdSub(bones[thigh + 1].natural.position, bones[thigh].natural.position)) +
        DirectVmdLength(DirectVmdSub(bones[thigh + 2].natural.position, bones[thigh + 1].natural.position));
  }
  baseScale = (legLength * 0.5f) / eiem_source::Reference().legLength;
  if (!DirectVmdFinite(baseScale) || baseScale < 1e-5f || baseScale > 100) return false;
  naturalHeight = bones[size_t(Id::LowerBody)].natural.position.y - entryRoot.position.y;
  const auto inv = DirectVmdQuaternionInverse(entryRoot.rotation);
  for (size_t i = 0; i < N; ++i) {
    auto& bone = bones[i];
    bone.bind.worldRotation = {0, 0, 0, 1};
    bone.bind.localRotation = {0, 0, 0, 1};
    if (bone.transform) bone.bind.worldPosition = DirectVmdRotateVector(inv,
        DirectVmdSub(bone.natural.position, entryRoot.position));
    else {
      const int parent = kDirectVmdBoneSpecs[i].parent;
      bone.bind.worldPosition = parent < 0 ? VmdVec3{} : bones[size_t(parent)].bind.worldPosition;
    }
  }
  // Same source control geometry used by PC DirectVmd, expressed in game basis.
  const eiem_source::Reference reference;
  bones[size_t(Id::Center)].bind.worldPosition = DirectVmdSub(bones[size_t(Id::LowerBody)].bind.worldPosition,
      DirectVmdScale(SourceToGameBasis::ConvertPosition(reference.lowerFromCenter), baseScale));
  bones[size_t(Id::Groove)].bind.worldPosition = DirectVmdAdd(bones[size_t(Id::Center)].bind.worldPosition,
      DirectVmdScale(SourceToGameBasis::ConvertPosition(reference.grooveFromCenter), baseScale));
  for (int side = 0; side < 2; ++side) {
    const size_t foot = size_t(side ? Id::RightFootIk : Id::LeftFootIk);
    const size_t ankle = size_t(side ? Id::RightAnkle : Id::LeftAnkle);
    bones[foot].bind.worldPosition = bones[ankle].bind.worldPosition;
    bones[foot + 1].bind.worldPosition = bones[ankle + 1].transform
        ? bones[ankle + 1].bind.worldPosition : bones[ankle].bind.worldPosition;
  }
  for (size_t i = 0; i < N; ++i) {
    auto& bone = bones[i];
    const int parent = kDirectVmdBoneSpecs[i].parent;
    bone.bind.localPosition = parent < 0 ? bone.bind.worldPosition :
        DirectVmdSub(bone.bind.worldPosition, bones[size_t(parent)].bind.worldPosition);
    Id child;
    VmdVec3 sourceDirection{};
    if (bone.transform && DirectVmdGetSemanticDirectionChild(Id(i), &child) &&
        bones[size_t(child)].transform && DirectVmdGetCanonicalSourceChildDirection(Id(i), &sourceDirection)) {
      const auto targetDirection = DirectVmdSub(bones[size_t(child)].bind.worldPosition, bone.bind.worldPosition);
      bone.alignment = DirectVmdQuaternionFromTo(SourceToGameBasis::ConvertPosition(sourceDirection), targetDirection);
    }
  }
  anchor = sharedAnchorValid ? sharedAnchor : entryRoot;
  if (!Read(GetEnabled, animator.object, animatorWasEnabled)) return false;
  CaptureTwists(nodes);
  CaptureCloth(nodes);
  if (options.face && !CaptureFace()) Log("SMC disabled for this actor: named managed pose/snapshot contract unavailable");
  return true;
}

// All source/ghost data below is owned C++ POD. Native game layouts are never
// read or written. Optional PC grounder, SMC jobs and cloth paths remain gated.
void SolveLeg(int side, const DirectVmdSampleFrame& frame) {
  const size_t thigh = size_t(side ? Id::RightLeg : Id::LeftLeg);
  const size_t knee = thigh + 1, ankle = thigh + 2;
  const size_t foot = size_t(side ? Id::RightFootIk : Id::LeftFootIk);
  if (!(side ? frame.rightFootIkEnabled : frame.leftFootIkEnabled) ||
      !frame.bones[foot].hasTrack) return;
  const VmdVec3 hip = poses[thigh].position;
  const float upper = DirectVmdLength(DirectVmdSub(poses[knee].position, hip));
  const float lower = DirectVmdLength(DirectVmdSub(poses[ankle].position, poses[knee].position));
  if (upper < 1e-5f || lower < 1e-5f) return;
  const auto reach = DirectVmdProjectLegReach(hip, poses[foot].position, upper + lower, 1e-5f);
  VmdVec3 axis{};
  if (!reach.valid || !DirectVmdTryNormalizeVector(DirectVmdSub(reach.solverTarget, hip), &axis)) return;
  VmdVec3 normal = DirectVmdCross(DirectVmdSub(poses[knee].position, hip),
                                DirectVmdSub(poses[ankle].position, poses[knee].position));
  if (DirectVmdLength(normal) < 1e-6f) normal = {1, 0, 0};
  if (options.knee_mix) {
    auto sourceKnee = frame.knees[side];
    sourceKnee.direction = SourceToGameBasis::ConvertPosition(sourceKnee.direction);
    const auto mix = DirectVmdMixKneeDirection(DirectVmdSub(reach.solverTarget, hip), normal,
        sourceKnee, upper + lower, options.knee_weight >= 0 ? options.knee_weight : DIRECT_VMD_KNEE_DEFAULT_WEIGHT);
    if (mix.applied) normal = mix.normal;
  }
  VmdVec3 bend{};
  if (!DirectVmdTryNormalizeVector(DirectVmdCross(axis, normal), &bend)) return;
  const float distance = std::clamp(reach.reachableDistance, std::fabs(upper - lower) + 1e-5f,
                                   upper + lower - 1e-5f);
  const float along = (upper * upper - lower * lower + distance * distance) / (2 * distance);
  const float height = std::sqrt(std::max(0.0f, upper * upper - along * along));
  const auto newKnee = DirectVmdAdd(hip, DirectVmdAdd(DirectVmdScale(axis, along), DirectVmdScale(bend, height)));
  poses[thigh].rotation = DirectVmdQuaternionMultiply(
      DirectVmdQuaternionFromTo(DirectVmdSub(poses[knee].position, hip), DirectVmdSub(newKnee, hip)), poses[thigh].rotation);
  poses[knee].rotation = DirectVmdQuaternionMultiply(
      DirectVmdQuaternionFromTo(DirectVmdSub(poses[ankle].position, poses[knee].position),
          DirectVmdSub(reach.solverTarget, newKnee)), poses[knee].rotation);
  poses[knee].position = newKnee;
  poses[ankle].position = reach.solverTarget;
  if (frame.bones[foot].hasTrack) poses[ankle].rotation = poses[foot].rotation;
  if (frame.bones[foot + 1].hasTrack && bones[ankle + 1].transform) {
    const auto toeOffset = DirectVmdRotateVector(poses[ankle].rotation, bones[ankle + 1].bind.localPosition);
    const auto toe = DirectVmdAdd(poses[ankle].position, toeOffset);
    const auto result = DirectVmdSolveToeAim(poses[ankle].position, toe, poses[foot + 1].position, poses[ankle].rotation);
    if (result.valid) {
      poses[ankle].rotation = result.solverAnkleWorldRotation;
      poses[ankle + 1].position = result.solverToe;
      poses[ankle + 1].rotation = DirectVmdQuaternionMultiply(result.correction, poses[ankle + 1].rotation);
    }
  }
}
void Apply() {
  if (!active || !OnMainThread()) return;
  if (loadState.load(std::memory_order_acquire) == LoadState::Failed) { Stop("sample-failed"); return; }
  if (!Alive(animator.object) || !Alive(entity.object) || !Alive(ownerRoot.object)) {
    Stop("managed-owner-destroyed"); return;
  }
  DirectVmdSampleFrame frame{};
  {
    std::lock_guard lock(workerMutex);
    if (!readyClip || !latest.valid || latest.clipGeneration != activeClipGeneration) return;
    frame = latest;
  }
  const float multiplier = std::isfinite(options.motion_scale) ? std::clamp(options.motion_scale, 0.05f, 5.0f) : 1.0f;
  const float scale = baseScale * multiplier;
  for (size_t i = 0; i < N; ++i) {
    const int parent = kDirectVmdBoneSpecs[i].parent;
    const auto local = DirectVmdEvaluateLocalPose(bones[i].bind, {0, 0, 0, 1}, &frame.bones[i], scale);
    poses[i] = parent < 0 ? DirectVmdWorldPosePod{local.position, local.rotation} :
        DirectVmdComposeWorldPose(poses[size_t(parent)], local);
  }
  auto pelvis = DirectVmdComposeWorldPose(anchor, {poses[size_t(Id::LowerBody)].position, poses[size_t(Id::LowerBody)].rotation});
  if (!placementReady) {
    placement = sharedAnchorValid ? VmdVec3{} :
        DirectVmdBuildWorldTranslationPlacementOffset(bones[size_t(Id::LowerBody)].entry, pelvis);
    placementReady = true;
  }
  pelvis = DirectVmdApplyWorldTranslationPlacement(pelvis, placement);
  ApplyTerrain(frame,pelvis);
  SolveLeg(0, frame); SolveLeg(1, frame);
  if (!eiem_source::Finite(pelvis.position)) { Stop("non-finite-pose"); return; }
  for (size_t i = 0; i < N; ++i) {
    auto& bone = bones[i];
    if (!bone.transform) continue;
    if (!Alive(bone.transform.object)) { Stop("managed-bone-destroyed"); return; }
    // Align the source delta to the avatar's captured stance using shared PC math.
    const auto alignedDelta = DirectVmdQuaternionMultiply(
        DirectVmdQuaternionMultiply(bone.alignment, poses[i].rotation), DirectVmdQuaternionInverse(bone.alignment));
    auto rotation = DirectVmdRetargetWorldRotation(
        DirectVmdQuaternionMultiply(anchor.rotation, alignedDelta), entryRoot.rotation, bone.natural.rotation);
    if (!Finite(rotation) || !Set(SetRotation, bone.transform.object, &rotation)) {
      Stop("managed-pose-write-failed"); return;
    }
  }
  if (!Set(SetPosition, bones[size_t(Id::LowerBody)].transform.object, &pelvis.position)) { Stop("managed-root-write-failed"); return; }
  ApplyTwists(frame);
  ApplyCloth();
  ApplyFace(frame);
}

bool Initialize(const BE_HostApiV1* value, const std::wstring&, BetterEndfield::EiemSlot::HookTargets* targets) {
  if (initialized) return available;
  initialized = true; host = value; mainThread = std::this_thread::get_id();
  if (targets) *targets = {};
  if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 || !host->resolve_method ||
      !host->runtime_invoke || !host->object_unbox || !host->gchandle_new || !host->gchandle_free) {
    Log("required managed host contract unavailable"); return false;
  }
  for (auto& method : methods) {
    if (host->resolve_method(host->context, &method.descriptor, &method.resolved) != BE_Result_Ok || !method.resolved.method_info) {
      Log(std::string("required metadata unavailable: ") + method.descriptor.class_name + "." + method.descriptor.method_name);
      if (&method <= &methods[ObjectAlive]) return false;
    }
  }
  if (!metadata.Load()) Log("generic managed metadata exports absent; SMC feature disabled");
  if (targets) {
    const BE_MethodDescriptorV1 spec{"RootMotion.dll", "RootMotion.FinalIK", "BipedIK", "UpdateSolver", "", "System.Void", 0};
    BE_ResolvedMethodV1 result{};
    if (host->resolve_method(host->context,&spec,&result) != BE_Result_Ok || !result.method_info || !result.method_pointer) {
      Log("required named BipedIK.UpdateSolver callback unavailable"); return false;
    }
    targets->biped_update_solver = result.method_pointer;
  }
  try {
    { std::lock_guard lock(workerMutex); running = true; }
    worker = std::thread(Worker);
  } catch (const std::system_error& e) {
    std::lock_guard lock(workerMutex); running = false;
    Log(std::string("worker unavailable: ") + e.what()); return false;
  }
  available = true;
  Log("DirectVmd managed adapter ready; FK/IK/knee/twist/SMC/cloth/terrain use named managed contracts");
  return true;
}
bool PrepareFace(BetterEndfield::EiemSlot::FaceTargets* targets) {
  if (targets) *targets = {};
  // This adapter feeds SMC.SetPose/Update rather than intercepting native jobs.
  if (!available || !metadata.Ready() || !host->resolve_class || !host->object_new || !host->string_new) return false;
  const std::string params = std::string(kPoseType)+"|"+kTracker+"|System.Single";
  BE_ResolvedClassV1 poseClass{},ctrlClass{};
  return ResolveMethod(kGame,kSmcNs,"SkeletalMorphCore","SetPose",params.c_str(),"System.Void",3) &&
      ResolveMethod(kGame,kSmcNs,"SkeletalMorphCore","CompleteJob","","System.Void",0) &&
      ResolveMethod(kGame,kSmcNs,"SkeletalMorphCore","Update","System.Single","System.Void",1) &&
      host->resolve_class(host->context,kGame,kPoseNs,"SkeletalMorphPose",&poseClass)==BE_Result_Ok && poseClass.class_info &&
      host->resolve_class(host->context,kGame,kPoseNs,"MorphCtrlValue",&ctrlClass)==BE_Result_Ok && ctrlClass.class_info;
}
void SetOriginals(const BetterEndfield::EiemSlot::Originals&) {}
void SetOptions(const Options& value) {
  if (!OnMainThread()) return;
  if (value.terrain != options.terrain) ResetTerrain();
  if (value.terrain && !options.terrain) PrepareTerrain();
  if (active && options.cloth != value.cloth) {
    if (!ClothRestore()) { Log("cloth mode change rejected: restoration pending"); return; }
    options = value; auto nodes = Tree(); CaptureCloth(nodes);
  } else options = value;
  if (active && !options.face) { if (FaceRestore()) face = {}; }
  else if (active && options.face && !face.ready) {
    if (FaceRestore()) CaptureFace();
    else Log("SMC re-enable rejected: restoration pending");
  }
}
void SetSharedAnchor(bool valid, const float position[3], const float rotation[4]) {
  if (!OnMainThread()) return;
  sharedAnchorValid = valid && position && rotation;
  if (sharedAnchorValid) {
    sharedAnchor = {{position[0], position[1], position[2]}, {rotation[0], rotation[1], rotation[2], rotation[3]}};
    sharedAnchorValid = eiem_source::Finite(sharedAnchor.position) && Finite(sharedAnchor.rotation);
    sharedAnchor.rotation = DirectVmdNormalizeQuaternion(sharedAnchor.rotation);
  }
  if (active) { anchor = sharedAnchorValid ? sharedAnchor : entryRoot; placementReady = false; }
}
void Load(const std::string& motion, const std::string& face) {
  if (!available || !OnMainThread()) return;
  Stop("load");
  std::lock_guard lock(workerMutex);
  motionPath = motion; facePath = face; ++requested; readyClip.reset(); latest = {};
  duration.store(0); loadState.store(LoadState::Loading, std::memory_order_release); wake.notify_one();
}
LoadState LoadStatus() { return loadState.load(std::memory_order_acquire); }
double Duration() { return duration.load(std::memory_order_acquire); }
void SetTarget(void* owner, void* anim, void* move) {
  if (!OnMainThread()) return;
  if (entity.object == owner && animator.object == anim && movement.object == move) return;
  Stop("target-change"); ownerRoot.Reset();
  entity = Pin(owner); animator = Pin(anim); movement=Pin(move);
  terrain.result.Reset();terrain.resolved=false;terrain.available=false;
  terrain.reason="terrain metadata not prepared";
}
bool Start(const char*) {
  if (!available || !OnMainThread() || LoadStatus() != LoadState::Ready) return false;
  if (active) return true;
  if (!FaceRestore() || !ClothRestore()) { Log("start rejected: managed restoration pending"); return false; }
  {
    std::lock_guard lock(workerMutex);
    if (!readyClip) return false;
    activeClipGeneration = readyClip->generation;
  }
  if (!Capture()) { ReleaseBones(); Log("DirectVmd start rejected: avatar/managed lifetime contract"); return false; }
  bool enabled = false;
  if (!Set(SetEnabled, animator.object, &enabled)) { ReleaseBones(); return false; }
  active = true; placementReady = false; wake.notify_one();
  ResetTerrain();
  if (options.terrain && (!movement || !PrepareTerrain())) Log("terrain unavailable for actor: "+
      (movement?terrain.reason:std::string("MovementComponent target missing")));
  return true;
}
bool Active() { return active; }
bool Busy() { return active; }
void PublishClock(double seconds, bool playing) {
  if (!OnMainThread()) return;
  clockSeconds.store(std::isfinite(seconds) ? std::max(0.0, seconds) : 0, std::memory_order_release);
  clockStamp.store(Now(), std::memory_order_release); clockPlaying.store(playing, std::memory_order_release);
  wake.notify_one(); Apply();
}
void Shutdown() {
  // Public contract: process shutdown may be off the Unity thread. Only join
  // the file/sampling worker there; managed teardown belongs to Stop on main.
  {
    std::lock_guard lock(workerMutex); running = false;
  }
  wake.notify_one();
  if (worker.joinable()) worker.join();
  if (OnMainThread()) terrain.result.Reset();
}
void SolverManagerLateUpdate(void*) {}
bool OwnsBiped(void* self) {
  if (!active || !OnMainThread() || !self || !ownerRoot) return false;
  void* transform = nullptr;
  if (!Call(ComponentTransform,self,nullptr,&transform)) return false;
  Pin current(transform);
  for (int depth=0; current && depth<128; ++depth) {
    if (current.object == ownerRoot.object) return true;
    void* parent=nullptr;
    if (!Call(GetParent,current.object,nullptr,&parent)) return false;
    current = Pin(parent);
  }
  return false;
}
void BeforeFinalIk(void* self) { if (OwnsBiped(self)) Apply(); }
bool SuppressFinalIk(void* self) { return OwnsBiped(self); }
void AfterFinalIk(void*) {}
void BeforeLeg(void*, void*) {}
void AfterLeg(void*) {}
bool GrounderActive(void*) { return false; }
void GrounderUpdate(void*, void*) {}
bool OwnsSmc(void*) { return false; }
void SmcUpdate(void*, float, void*) {}
void Job(void*, void*, void*, void*) {}
bool CameraReference(BetterEndfield::EiemBody::CameraReference* out) {
  if (!out || !active || !placementReady) return false;
  auto reference = DirectVmdApplyWorldTranslationPlacement(anchor, placement);
  out->position[0] = reference.position.x; out->position[1] = reference.position.y; out->position[2] = reference.position.z;
  out->rotation[0] = reference.rotation.x; out->rotation[1] = reference.rotation.y;
  out->rotation[2] = reference.rotation.z; out->rotation[3] = reference.rotation.w;
  out->motion_scale = baseScale * (std::isfinite(options.motion_scale) ? std::clamp(options.motion_scale, 0.05f, 5.0f) : 1.0f);
  out->natural_height = naturalHeight; out->offset[0] = out->offset[2] = 0; out->offset[1]=terrain.rootOffset;
  return true;
}
bool ClothPrepare(void*, uintptr_t, float, float*) { return false; }
void ClothReadback(void*, uintptr_t, float, float) {}
bool TerrainAvailable() { return OnMainThread() && PrepareTerrain() && bool(movement); }
const char* TerrainReason() {
  if (OnMainThread()) PrepareTerrain();
  return terrain.available && !movement?"MovementComponent target missing":terrain.reason.c_str();
}
const Api kApi{Initialize, PrepareFace, SetOriginals, SetOptions, SetSharedAnchor,
  Load, LoadStatus, Duration, SetTarget, Start, Stop, Active, Busy, PublishClock, Shutdown,
  SolverManagerLateUpdate, BeforeFinalIk, SuppressFinalIk, AfterFinalIk, BeforeLeg, AfterLeg,
  GrounderActive, GrounderUpdate, GrounderUpdate, OwnsSmc, OwnsSmc, SmcUpdate, Job, Job,
  CameraReference, ClothPrepare, ClothReadback,TerrainAvailable,TerrainReason};
} // namespace
} // namespace BE_EIEM_NS
const BetterEndfield::EiemSlot::Api* BetterEndfield::EiemSlot::BE_EIEM_API() {
  return &BE_EIEM_NS::kApi;
}
#undef BE_EIEM_API
#undef BE_EIEM_NS
#undef BE_EIEM_CAT
#undef BE_EIEM_CAT2
