// Included inside the slot namespace, after its host/pose state declarations.
// Metadata enumeration is only needed for already-instantiated generic lists
// and NativeArrays; all invocation, allocation, fields and pins use the host.
struct MetadataApi {
  void* (*objectClass)(void*) = nullptr;
  void* (*classMethods)(void*, void**) = nullptr;
  const char* (*methodName)(void*) = nullptr;
  uint32_t (*paramCount)(void*) = nullptr;
  void* (*paramType)(void*, uint32_t) = nullptr;
  void* (*returnType)(void*) = nullptr;
  const char* (*typeName)(void*) = nullptr;
  bool (*valueType)(void*) = nullptr;
  void (*freeMemory)(void*) = nullptr;
  void (*setField)(void*, void*, void*) = nullptr;
  bool Ready() const { return objectClass && classMethods && methodName && paramCount && paramType && returnType && typeName && valueType && freeMemory; }
  bool Load() {
#if !defined(BE_EIEM_ANDROID_OFFLINE_TEST)
    void* image = betterendfield::OpenLoadedIl2Cpp();
    if (!image) return false;
#define EIEM_META(member, name) member = reinterpret_cast<decltype(member)>(dlsym(image, name))
#else
#define EIEM_META(member, name) member = reinterpret_cast<decltype(member)>(BeEiemOfflineMetadataExport(name))
#endif
    EIEM_META(objectClass, "il2cpp_object_get_class");
    EIEM_META(classMethods, "il2cpp_class_get_methods");
    EIEM_META(methodName, "il2cpp_method_get_name");
    EIEM_META(paramCount, "il2cpp_method_get_param_count");
    EIEM_META(paramType, "il2cpp_method_get_param");
    EIEM_META(returnType, "il2cpp_method_get_return_type");
    EIEM_META(typeName, "il2cpp_type_get_name");
    EIEM_META(valueType, "il2cpp_class_is_valuetype");
    EIEM_META(freeMemory, "il2cpp_free");
    EIEM_META(setField, "il2cpp_field_set_value");
#undef EIEM_META
#if !defined(BE_EIEM_ANDROID_OFFLINE_TEST)
    dlclose(image);
#endif
    return Ready();
  }
} metadata;
bool MetadataTypeEquals(void* type, const char* expected) {
  const char* name = metadata.typeName(type);
  const bool equal = name && !strcmp(name, expected);
  if (name) metadata.freeMemory(const_cast<char*>(name));
  return equal;
}
bool ManagedCall(const void* method, const Pin& instance, void** args, void** result = nullptr) {
  if (!method || !OnMainThread()) return false;
  void* object = instance.object;
  if (object && metadata.Ready() && metadata.valueType(metadata.objectClass(object))) {
    object = host->object_unbox(host->context, object);
    if (!object) return false;
  }
  void* exception = nullptr;
  void* value = host->runtime_invoke(host->context, method, object, args, &exception);
  if (exception) return false;
  if (result) *result = value;
  return true;
}
template <class T> bool ManagedRead(const void* method, const Pin& instance, T& out, void** args = nullptr) {
  void* value = nullptr;
  if (!ManagedCall(method, instance, args, &value)) return false;
  Pin box(value);
  if (!box) return false;
  const void* data = host->object_unbox(host->context, box.object);
  if (!data) return false;
  memcpy(&out, data, sizeof(T)); return true;
}
const void* RuntimeMethod(const Pin& instance, const char* name, const char* result,
                          const std::vector<const char*>& parameters) {
  if (!instance || !metadata.Ready()) return nullptr;
  void* klass = metadata.objectClass(instance.object);
  void* it = nullptr;
  const void* found = nullptr;
  while (void* method = metadata.classMethods(klass, &it)) {
    const char* actualName = metadata.methodName(method);
    if (!actualName || strcmp(name, actualName) || metadata.paramCount(method) != parameters.size()) continue;
    if (!MetadataTypeEquals(metadata.returnType(method), result)) continue;
    bool match = true;
    for (size_t i = 0; i < parameters.size(); ++i) {
      match &= MetadataTypeEquals(metadata.paramType(method, uint32_t(i)), parameters[i]);
    }
    if (!match) continue;
    if (found) return nullptr; // Ambiguous metadata is unavailable.
    found = method;
  }
  return found;
}
const void* ResolveMethod(const char* assembly, const char* ns, const char* cls,
    const char* name, const char* params, const char* result, uint32_t count) {
  const BE_MethodDescriptorV1 spec{assembly, ns, cls, name, params, result, count};
  BE_ResolvedMethodV1 method{};
  if (host->resolve_method(host->context, &spec, &method) != BE_Result_Ok) return nullptr;
  return method.method_info;
}
const void* ResolveField(const char* assembly, const char* ns, const char* cls, const char* name, const char* type) {
  if (!host->resolve_field || !host->field_get_value_object) return nullptr;
  const BE_FieldDescriptorV1 spec{assembly, ns, cls, name, type};
  BE_ResolvedFieldV1 field{};
  return host->resolve_field(host->context, &spec, &field) == BE_Result_Ok ? field.field_info : nullptr;
}
Pin Field(const void* field, const Pin& instance) {
  return Pin(field && host->field_get_value_object ? host->field_get_value_object(host->context, field, instance.object) : nullptr);
}
template <class T> bool FieldValue(const void* field, const Pin& instance, T& out) {
  Pin box = Field(field, instance);
  if (!box) return false;
  void* payload = host->object_unbox(host->context, box.object);
  if (!payload) return false;
  memcpy(&out, payload, sizeof(T)); return true;
}
Pin ArrayItem(const Pin& array, int index) {
  void* value = nullptr;
  void* args[] = {&index};
  return array && Call(ArrayValue, array.object, args, &value) ? Pin(value) : Pin();
}

// Bounded traversal uses managed child/name APIs, never object/array offsets.
std::vector<Pin> Tree() {
  std::vector<Pin> nodes;
  if (!ownerRoot) return nodes;
  nodes.emplace_back(ownerRoot.object);
  for (size_t n = 0; n < nodes.size() && n < 1024; ++n) {
    int count = 0;
    if (!Read(ChildCount, nodes[n].object, count) || count < 0 || count > 1024) return {};
    for (int i = 0; i < count; ++i) {
      if (nodes.size() == 1024) { Log("hierarchy capacity exceeded; optional channels disabled"); return {}; }
      void* child = nullptr; void* args[] = {&i};
      if (!Call(GetChild, nodes[n].object, args, &child)) return {};
      Pin pin(child);
      if (!pin) return {};
      nodes.emplace_back(std::move(pin));
    }
  }
  return nodes;
}
std::string Name(const Pin& object) {
  if (!host->copy_managed_string) return {};
  void* text = nullptr;
  if (!Call(ObjectName, object.object, nullptr, &text)) return {};
  Pin pin(text);
  char buffer[256]{};
  if (!pin || !host->copy_managed_string(host->context, pin.object, buffer, sizeof(buffer))) return {};
  return buffer;
}
bool CaptureNaturalBind(const std::vector<Pin>& nodes) {
  avatarLocalPoses.clear();
  if (!host->copy_managed_string || nodes.empty()) return false;
  const char* assembly="UnityEngine.AnimationModule.dll";
  const auto avatarGetter=ResolveMethod(assembly,"UnityEngine","Animator","get_avatar","","UnityEngine.Avatar",0);
  const auto descriptionGetter=ResolveMethod(assembly,"UnityEngine","Avatar","get_humanDescription","","UnityEngine.HumanDescription",0);
  void* value=nullptr;
  if (!ManagedCall(avatarGetter,animator,nullptr,&value)) return false;
  Pin avatar(value);
  if (!avatar || !ManagedCall(descriptionGetter,avatar,nullptr,&value)) return false;
  Pin description(value);
  Pin skeleton=Field(ResolveField(assembly,"UnityEngine","HumanDescription","skeleton","UnityEngine.SkeletonBone[]"),description);
  int count=0;
  if (!skeleton || !Read(ArrayLength,skeleton.object,count) || count<1 || count>4096) return false;
  const auto nameField=ResolveField(assembly,"UnityEngine","SkeletonBone","name","System.String");
  const auto positionField=ResolveField(assembly,"UnityEngine","SkeletonBone","position","UnityEngine.Vector3");
  const auto rotationField=ResolveField(assembly,"UnityEngine","SkeletonBone","rotation","UnityEngine.Quaternion");
  const auto scaleField=ResolveField(assembly,"UnityEngine","SkeletonBone","scale","UnityEngine.Vector3");
  if (!nameField || !positionField || !rotationField || !scaleField) return false;
  struct Record { DirectVmdLocalPosePod local; VmdVec3 scale; };
  std::map<std::string,Record> local;
  for(int i=0;i<count;++i) {
    Pin bone=ArrayItem(skeleton,i); Pin name=Field(nameField,bone); char text[256]{};
    Record record{};
    if (!bone || !name || host->copy_managed_string(host->context,name.object,text,sizeof(text))<=0 ||
        !FieldValue(positionField,bone,record.local.position) || !FieldValue(rotationField,bone,record.local.rotation) ||
        !FieldValue(scaleField,bone,record.scale) || !eiem_source::Finite(record.local.position) ||
        !Finite(record.local.rotation) || !eiem_source::Finite(record.scale)) return false;
    if (!local.emplace(text,record).second) return false;
  }
  const auto scaleGetter=ResolveMethod("UnityEngine.CoreModule.dll","UnityEngine","Transform","get_lossyScale","","UnityEngine.Vector3",0);
  VmdVec3 rootScale{};
  if (!ManagedRead(scaleGetter,ownerRoot,rootScale) || !eiem_source::Finite(rootScale) || rootScale.x<=0 || rootScale.y<=0 || rootScale.z<=0) return false;
  struct Natural { DirectVmdWorldPosePod pose; VmdVec3 scale; };
  std::map<void*,Natural> world;
  world.emplace(ownerRoot.object,Natural{entryRoot,rootScale});
  for(const auto& node:nodes) {
    if (node.object==ownerRoot.object) continue;
    auto record=local.find(Name(node));
    if (record==local.end()) continue;
    void* parent=nullptr;
    if (!Call(GetParent,node.object,nullptr,&parent)) return false;
    const auto naturalParent=world.find(parent);
    if (naturalParent==world.end()) continue;
    auto pose=record->second.local;
    const auto& parentScale=naturalParent->second.scale;
    pose.position={pose.position.x*parentScale.x,pose.position.y*parentScale.y,pose.position.z*parentScale.z};
    const auto& scale=record->second.scale;
    avatarLocalPoses.emplace(node.object,record->second.local);
    world.emplace(node.object,Natural{DirectVmdComposeWorldPose(naturalParent->second.pose,pose),
        {parentScale.x*scale.x,parentScale.y*scale.y,parentScale.z*scale.z}});
  }
  for(auto& bone:bones) if(bone.transform) {
    auto value=world.find(bone.transform.object);
    if(value==world.end()) return false;
    bone.natural=value->second.pose;
  }
  return true;
}

struct TwistTarget {
  Pin transform;
  VmdQuaternion entry{0,0,0,1};
  VmdQuaternion bind{0,0,0,1};
  VmdVec3 axis{};
};
std::array<std::array<TwistTarget, 2>, 4> twists;
void CaptureTwists(const std::vector<Pin>& nodes) {
  static const char* names[4][2] = {{"Bip001_LUpArmTwist", "Bip001_LUpArmTwist1"},
      {"Bip001_L_ForeTwist", "Bip001_L_ForeTwist1"}, {"Bip001_RUpArmTwist", "Bip001_RUpArmTwist1"},
      {"Bip001_R_ForeTwist", "Bip001_R_ForeTwist1"}};
  for (size_t c = 0; c < twists.size(); ++c) {
    const auto& spec = kDirectVmdPhase6TwistChannels[c];
    VmdVec3 sourceAxis{};
    if (!DirectVmdGetCanonicalSourceChildDirection(spec.sourceLimb, &sourceAxis)) continue;
    sourceAxis = SourceToGameBasis::ConvertPosition(sourceAxis);
    for (size_t t = 0; t < 2; ++t) {
      auto& target = twists[c][t];
      for (const auto& node : nodes) if (Name(node) == names[c][t]) {
        if (target.transform) { target.transform.Reset(); break; } // Ambiguous name.
        target.transform = Pin(node.object);
      }
      if (!target.transform || !Read(GetLocalRotation, target.transform.object, target.entry)) {
        target.transform.Reset(); continue;
      }
      const auto natural=avatarLocalPoses.find(target.transform.object);
      if (natural==avatarLocalPoses.end()) { target.transform.Reset(); continue; }
      target.bind=natural->second.rotation;
      void* parent = nullptr;
      if (!Call(GetParent, target.transform.object, nullptr, &parent)) { target.transform.Reset(); continue; }
      Pin parentPin(parent);
      DirectVmdWorldPosePod parentPose{};
      if (!parentPin || !World(parentPin.object, parentPose)) { target.transform.Reset(); continue; }
      const auto axisWorld = DirectVmdRotateVector(entryRoot.rotation,
          DirectVmdRotateVector(bones[size_t(spec.sourceLimb)].alignment, sourceAxis));
      target.axis = DirectVmdRotateVector(DirectVmdQuaternionInverse(parentPose.rotation), axisWorld);
    }
  }
}
void ApplyTwists(const DirectVmdSampleFrame& frame) {
  for (size_t c = 0; c < twists.size(); ++c) {
    const auto& spec = kDirectVmdPhase6TwistChannels[c];
    if (!twists[c][0].transform || !twists[c][1].transform) continue;
    VmdVec3 axis{}; float angle = 0;
    if (!DirectVmdGetCanonicalSourceChildDirection(spec.sourceLimb, &axis) ||
        !DirectVmdExtractSignedTwistAngle(SourceToGameBasis::ConvertRotation(frame.bones[size_t(spec.control)].rotation),
            SourceToGameBasis::ConvertPosition(axis), &angle)) continue;
    for (auto& target : twists[c]) {
      auto local = DirectVmdQuaternionMultiply(DirectVmdQuaternionFromAxisAngle(target.axis, angle * .5f), target.bind);
      if (!Alive(target.transform.object) || !Set(SetLocalRotation, target.transform.object, &local))
        Log("twist managed write failed");
    }
  }
}

constexpr const char* kGame = "Gameplay.Beyond.dll";
constexpr const char* kSmcNs = "Beyond.Gameplay.Core";
constexpr const char* kPoseNs = "Beyond.Gameplay.View.SkeletalMorph";
constexpr const char* kTracker = "Beyond.Gameplay.Core.ESkeletalMorphTrackerType";
constexpr const char* kPoseType = "Beyond.Gameplay.View.SkeletalMorph.SkeletalMorphPose";
constexpr const char* kCtrlType = "Beyond.Gameplay.View.SkeletalMorph.MorphCtrlValue";
struct FaceBuffer {
  Pin array, original;
  const void* setter = nullptr;
  const void* field = nullptr;
  const void* equals = nullptr;
};
struct FaceState {
  Pin owner, component, core, pose;
  BE_ResolvedClassV1 ctrlClass{};
  const void* ctorCtrl = nullptr;
  const void* setPose = nullptr;
  const void* update = nullptr;
  const void* complete = nullptr;
  const void* inited = nullptr;
  const void* setOverride = nullptr;
  const void* setPause = nullptr;
  const void* setEmotionPause = nullptr;
  bool originalOverride = false, originalPause = false, originalEmotionPause = false;
  std::set<std::string> controllers;
  int tracker = 0;
  std::array<FaceBuffer, 7> snapshots;
  bool changed = false;
  bool ready = false;
} face;
bool FaceRestore() {
  if (!face.changed) return true;
  if (!Alive(face.owner.object)) { face = {}; return true; }
  if (!ManagedCall(face.complete, face.core, nullptr)) return false;
  bool inited = false;
  if (!ManagedRead(face.inited, face.core, inited) || !inited) {
    // Release/rebuild disposed the native buffers; never access saved pointers.
    face = {}; return true;
  }
  bool ok = true;
  for (auto& snapshot : face.snapshots) {
    Pin current = Field(snapshot.field, face.core);
    void* currentPayload = current ? host->object_unbox(host->context, current.object) : nullptr;
    void* compareArgs[] = {currentPayload};
    bool sameBuffer = false;
    if (!currentPayload || !ManagedRead(snapshot.equals, snapshot.array, sameBuffer, compareArgs) || !sameBuffer) {
      Log("SMC rebuilt native buffers; old tracker snapshot discarded"); face = {}; return true;
    }
  }
  for (auto& snapshot : face.snapshots) {
    void* payload = host->object_unbox(host->context, snapshot.original.object);
    void* args[] = {&face.tracker, payload};
    ok &= payload && ManagedCall(snapshot.setter, snapshot.array, args);
  }
  bool reset = false;
  void* overrideArgs[] = {&face.tracker,&face.originalOverride};
  void* pauseArgs[] = {&face.tracker,&face.originalPause,&reset};
  void* emotionArgs[] = {&face.originalEmotionPause};
  ok &= ManagedCall(face.setOverride,face.core,overrideArgs) && ManagedCall(face.setPause,face.core,pauseArgs) &&
      ManagedCall(face.setEmotionPause,face.core,emotionArgs);
  if (ok) face.changed = false;
  else Log("SMC restoration pending; retaining owned snapshots");
  return ok;
}
bool CaptureFace() {
  if (!options.face || !metadata.Ready() || !host->resolve_class || !host->object_new || !host->string_new) return false;
  face = {};
  face.owner=Pin(entity.object);
  if (!face.owner) return false;
  const auto getComponent = ResolveMethod(kGame, kSmcNs, "Entity", "get_skMorphCom", "", "Beyond.Gameplay.View.SkeletalMorphComponent", 0);
  void* value = nullptr;
  if (!ManagedCall(getComponent, entity, nullptr, &value) || !(face.component = Pin(value))) return false;
  const auto coreField = ResolveField(kGame, "Beyond.Gameplay.View", "SkeletalMorphComponent", "m_core", "Beyond.Gameplay.Core.SkeletalMorphCore");
  face.core = Field(coreField, face.component);
  if (!face.core) return false;
  face.complete = ResolveMethod(kGame, kSmcNs, "SkeletalMorphCore", "CompleteJob", "", "System.Void", 0);
  face.inited = ResolveMethod(kGame, kSmcNs, "SkeletalMorphCore", "get_IsInited", "", "System.Boolean", 0);
  face.update = ResolveMethod(kGame, kSmcNs, "SkeletalMorphCore", "Update", "System.Single", "System.Void", 1);
  const std::string setParams = std::string(kPoseType) + "|" + kTracker + "|System.Single";
  face.setPose = ResolveMethod(kGame, kSmcNs, "SkeletalMorphCore", "SetPose", setParams.c_str(), "System.Void", 3);
  const auto trackerField = ResolveField(kGame, kSmcNs, "ESkeletalMorphTrackerType", "DefaultEx2", kTracker);
  Pin none;
  bool inited = false;
  if (!face.complete || !face.update || !face.setPose || !ManagedRead(face.inited, face.core, inited) || !inited ||
      !FieldValue(trackerField, none, face.tracker) || face.tracker < 0 || face.tracker > 64) return false;
  if (!ManagedCall(face.complete, face.core, nullptr)) return false;
  const auto getOverride = ResolveMethod(kGame,kSmcNs,"SkeletalMorphCore","GetIsOverridedTracker",kTracker,"System.Boolean",1);
  const auto getPause = ResolveMethod(kGame,kSmcNs,"SkeletalMorphCore","GetTrackerAnimationPause",kTracker,"System.Boolean",1);
  const auto emotionPause = ResolveField(kGame,kSmcNs,"SkeletalMorphCore","m_pauseEmotion","System.Boolean");
  const std::string overrideParams=std::string(kTracker)+"|System.Boolean";
  const std::string pauseParams=overrideParams+"|System.Boolean";
  face.setOverride=ResolveMethod(kGame,kSmcNs,"SkeletalMorphCore","SetIsOverridedTracker",overrideParams.c_str(),"System.Void",2);
  face.setPause=ResolveMethod(kGame,kSmcNs,"SkeletalMorphCore","SetTrackerAnimationPause",pauseParams.c_str(),"System.Void",3);
  face.setEmotionPause=ResolveMethod(kGame,kSmcNs,"SkeletalMorphCore","SetMorphEmotionPause","System.Boolean","System.Void",1);
  void* trackerArgs[]={&face.tracker};
  if (!face.setOverride || !face.setPause || !face.setEmotionPause ||
      !ManagedRead(getOverride,face.core,face.originalOverride,trackerArgs) ||
      !ManagedRead(getPause,face.core,face.originalPause,trackerArgs) ||
      !FieldValue(emotionPause,face.core,face.originalEmotionPause)) return false;
  static const char* fields[] = {"m_tracks", "m_tracksDoubleBuffer", "m_transitionTracks", "m_transitionTracksDoubleBuffer",
      "m_transitionCurrentTrackAnim", "m_transitionCurrent", "m_transitionNext"};
  static const char* types[] = {"Beyond.Gameplay.Core.FSkeletalMorphTracker", "Beyond.Gameplay.Core.FSkeletalMorphTracker",
      "Beyond.Gameplay.Core.FSkeletalMorphTracker", "Beyond.Gameplay.Core.FSkeletalMorphTracker",
      "Beyond.Gameplay.Core.FMorphAnimTransitionRuntime", "Beyond.Gameplay.Core.FMorphCurrentEmotionTransitionRuntime",
      "Beyond.Gameplay.Core.FMorphEmotionTransitionRuntime"};
  for (size_t i = 0; i < face.snapshots.size(); ++i) {
    auto& snapshot = face.snapshots[i];
    const std::string arrayType = std::string("Unity.Collections.NativeArray<") + types[i] + ">";
    snapshot.field = ResolveField(kGame, kSmcNs, "SkeletalMorphCore", fields[i], arrayType.c_str());
    snapshot.array = Field(snapshot.field, face.core);
    snapshot.equals = RuntimeMethod(snapshot.array, "Equals", "System.Boolean", {arrayType.c_str()});
    int length = 0;
    const auto getLength = RuntimeMethod(snapshot.array, "get_Length", "System.Int32", {});
    if (!ManagedRead(getLength, snapshot.array, length) || face.tracker >= length || length > 128) return false;
    const auto getter = RuntimeMethod(snapshot.array, "get_Item", types[i], {"System.Int32"});
    snapshot.setter = RuntimeMethod(snapshot.array, "set_Item", "System.Void", {"System.Int32", types[i]});
    void* args[] = {&face.tracker};
    if (!getter || !snapshot.setter || !snapshot.equals || !ManagedCall(getter, snapshot.array, args, &value) || !(snapshot.original = Pin(value))) return false;
  }
  Pin avatar = Field(ResolveField(kGame,kSmcNs,"SkeletalMorphCore","morphData","Beyond.Gameplay.Core.SkeletalMorphAvatarData"),face.core);
  Pin names = Field(ResolveField(kGame,kSmcNs,"SkeletalMorphAvatarData","morphMappingNames","System.String[]"),avatar);
  int nameCount = 0;
  if (!names || !host->copy_managed_string || !Read(ArrayLength,names.object,nameCount) || nameCount < 1 || nameCount > 4096) return false;
  for (int i = 0; i < nameCount; ++i) {
    Pin name = ArrayItem(names,i); char text[256]{};
    if (name && host->copy_managed_string(host->context,name.object,text,sizeof(text)) > 0) face.controllers.insert(text);
  }
  BE_ResolvedClassV1 poseClass{};
  if (host->resolve_class(host->context, kGame, kPoseNs, "SkeletalMorphPose", &poseClass) != BE_Result_Ok ||
      host->resolve_class(host->context, kGame, kPoseNs, "MorphCtrlValue", &face.ctrlClass) != BE_Result_Ok) return false;
  face.pose = Pin(host->object_new(host->context, poseClass.class_info));
  const auto poseCtor = ResolveMethod(kGame, kPoseNs, "SkeletalMorphPose", ".ctor", "", "System.Void", 0);
  face.ctorCtrl = ResolveMethod(kGame, kPoseNs, "MorphCtrlValue", ".ctor", "System.String|System.Single", "System.Void", 2);
  if (!face.pose || !face.ctorCtrl || !ManagedCall(poseCtor, face.pose, nullptr)) return false;
  face.ready = true;
  return true;
}

// The mapping is generated from the PC g_extraMorphs table without offsets.
#include "android_morph_map.h"
bool AddControl(const Pin& list, const char* controller, float weight) {
  Pin text(host->string_new(host->context, controller));
  Pin control(host->object_new(host->context, face.ctrlClass.class_info));
  void* ctorArgs[] = {text.object, &weight};
  if (!text || !control || !ManagedCall(face.ctorCtrl, control, ctorArgs)) return false;
  const auto add = RuntimeMethod(list, "Add", "System.Void", {kCtrlType});
  void* payload = host->object_unbox(host->context, control.object);
  void* addArgs[] = {payload};
  return payload && ManagedCall(add, list, addArgs);
}
void ApplyFace(const DirectVmdSampleFrame& frame) {
  if (!face.ready || !options.face) return;
  if (!ManagedCall(face.complete, face.core, nullptr)) return;
  std::map<std::string, float> weights;
  float mouth[5]{};
  static const char* mouthNames[] = {u8"あ",u8"い",u8"う",u8"え",u8"お"};
  for (size_t i = 0; i < 5; ++i) DirectVmdFindMorphWeight(frame, mouthNames[i], &mouth[i]);
  for (const auto& alias : kDirectVmdMouthAliases) {
    float value = 0;
    if (DirectVmdFindMorphWeight(frame, alias.name, &value)) mouth[alias.shape] = std::max(mouth[alias.shape], value);
  }
  float total = 0;
  for (float& value : mouth) { value = std::isfinite(value) ? std::clamp(value,0.0f,1.0f) : 0; total += value; }
  const char* vowels[] = {"A", "I", "U", "E", "O"};
  for (size_t i = 0; i < 5; ++i) weights[vowels[i]] = mouth[i] / std::max(total, 1.0f);
  for (const auto& rule : kAndroidMorphMap) {
    float value = 0;
    if (!DirectVmdFindMorphWeight(frame, rule.source, &value)) continue;
    value = std::isfinite(value) ? std::clamp(value, 0.0f, 1.0f) : 0;
    for (const char* target : rule.targets) if (target) weights[target] = std::max(weights[target], value);
  }
  std::array<Pin, 6> lists;
  const char* properties[] = {"get_browValueL", "get_browValueR", "get_eyeValueL", "get_eyeValueR", "get_mouthValue", "get_otherValue"};
  const char* listType = "System.Collections.Generic.List<Beyond.Gameplay.View.SkeletalMorph.MorphCtrlValue>";
  for (size_t i = 0; i < lists.size(); ++i) {
    const auto getter = ResolveMethod(kGame, kPoseNs, "SkeletalMorphPose", properties[i], "", listType, 0);
    void* value = nullptr;
    if (!ManagedCall(getter, face.pose, nullptr, &value) || !(lists[i] = Pin(value)) ||
        !ManagedCall(RuntimeMethod(lists[i], "Clear", "System.Void", {}), lists[i], nullptr)) {
      Log("SMC pose list contract unavailable"); FaceRestore(); face.ready = false; return;
    }
  }
  for (const auto& [name, weight] : weights) {
    if (!face.controllers.count(name)) continue; // Explicitly absent on this avatar.
    size_t list = 4;
    if (name.rfind("brow_",0) == 0) list = name.find("_L_") != std::string::npos ? 0 : 1;
    else if (name.rfind("eye_",0) == 0) list = name.find("_L_") != std::string::npos ? 2 : 3;
    if (!AddControl(lists[list], name.c_str(), weight)) { FaceRestore(); face.ready = false; return; }
  }
  float weight = 1, dt = 0;
  void* args[] = {face.pose.object, &face.tracker, &weight};
  face.changed = true;
  // Pause the original emotion progression and reserve this exact tracker.
  // Subsequent game Update ticks evaluate the authored pose instead of advancing
  // its animation/transition. Stop restores both opaque data and control flags.
  bool enabled=true, reset=false;
  void* overrideArgs[]={&face.tracker,&enabled};
  void* pauseArgs[]={&face.tracker,&enabled,&reset};
  void* emotionArgs[]={&enabled};
  if (!ManagedCall(face.setOverride,face.core,overrideArgs) || !ManagedCall(face.setPause,face.core,pauseArgs) ||
      !ManagedCall(face.setEmotionPause,face.core,emotionArgs)) { FaceRestore(); face.ready=false; return; }
  if (!ManagedCall(face.setPose, face.core, args)) { FaceRestore(); face.ready = false; return; }
  void* updateArgs[] = {&dt};
  if (!ManagedCall(face.update, face.core, updateArgs) || !ManagedCall(face.complete, face.core, nullptr)) {
    Log("SMC evaluation failed"); FaceRestore(); face.ready = false;
  }
}

struct ClothState {
  Pin object, serialize;
  bool enabled = false, changed = false, changedNumeric = false;
  float weight = 0, ratio = 0;
  float propertyWeight = 0, propertyRatio = 0;
};
struct ClothAnchor {
  Pin transform;
  DirectVmdLocalPosePod original, natural;
  bool changed=false;
};
std::vector<ClothAnchor> clothAnchors;
std::vector<ClothState> cloth;
const void* clothWeight = nullptr;
const void* clothRatio = nullptr;
const void* serializeWeight = nullptr;
const void* serializeRatio = nullptr;
const void* clothPropertyWeight = nullptr;
const void* clothPropertyRatio = nullptr;
bool ClothRestore() {
  bool complete = true;
  for (auto& anchor:clothAnchors) {
    if (!anchor.changed || !Alive(anchor.transform.object)) { anchor.changed=false; continue; }
    const bool ok=Set(SetLocalPosition,anchor.transform.object,&anchor.original.position) &&
        Set(SetLocalRotation,anchor.transform.object,&anchor.original.rotation);
    if (ok) anchor.changed=false;
    complete &= ok;
  }
  for (auto& item : cloth) {
    if (!item.changed || !Alive(item.object.object)) { item.changed = false; continue; }
    void* weightArgs[] = {&item.weight}; void* ratioArgs[] = {&item.ratio};
    bool ok = true;
    if (item.changedNumeric) {
      ok = ManagedCall(clothWeight, item.object, weightArgs) && ManagedCall(clothRatio, item.object, ratioArgs);
      if (metadata.setField) {
        metadata.setField(item.serialize.object, const_cast<void*>(serializeWeight), &item.weight);
        metadata.setField(item.serialize.object, const_cast<void*>(serializeRatio), &item.ratio);
        metadata.setField(item.object.object, const_cast<void*>(clothPropertyWeight), &item.propertyWeight);
        metadata.setField(item.object.object, const_cast<void*>(clothPropertyRatio), &item.propertyRatio);
      }
    }
    ok &= Set(SetEnabled, item.object.object, &item.enabled);
    float actualWeight = NAN, actualRatio = NAN; bool actualEnabled = false;
    ok &= Read(GetEnabled, item.object.object, actualEnabled) && actualEnabled == item.enabled;
    float propertyWeight = NAN, propertyRatio = NAN;
    if (item.changedNumeric) ok &= FieldValue(serializeWeight, item.serialize, actualWeight) && FieldValue(serializeRatio, item.serialize, actualRatio) &&
        actualWeight == item.weight && actualRatio == item.ratio &&
        FieldValue(clothPropertyWeight,item.object,propertyWeight) && FieldValue(clothPropertyRatio,item.object,propertyRatio) &&
        propertyWeight == item.propertyWeight && propertyRatio == item.propertyRatio;
    if (ok) item.changed = false;
    complete &= ok;
  }
  if (complete) { cloth.clear(); clothAnchors.clear(); }
  else Log("cloth restoration pending; retaining originals");
  return complete;
}
void CaptureCloth(const std::vector<Pin>& nodes) {
  if (options.cloth == BetterEndfield::EiemBody::ClothMode::Game || !host->resolve_class) return;
  BE_ResolvedClassV1 klass{};
  if (host->resolve_class(host->context, "BeyondDynamicBone.dll", "BeyondDynamicBone", "BeyondBoneCloth", &klass) != BE_Result_Ok || !klass.type_object) {
    Log("cloth class unavailable"); return;
  }
  Pin type(klass.type_object);
  clothWeight = ResolveMethod("BeyondDynamicBone.dll", "BeyondDynamicBone", "BeyondBoneCloth", "SetClothSimulateWeight", "System.Single", "System.Void", 1);
  clothRatio = ResolveMethod("BeyondDynamicBone.dll", "BeyondDynamicBone", "BeyondBoneCloth", "SetAnimationPoseRatio", "System.Single", "System.Void", 1);
  const auto getter = ResolveMethod("BeyondDynamicBone.dll", "BeyondDynamicBone", "BeyondBoneCloth", "get_SerializeData", "", "BeyondDynamicBone.ClothSerializeData", 0);
  serializeWeight = ResolveField("BeyondDynamicBone.dll", "BeyondDynamicBone", "ClothSerializeData", "clothSimulateWeight", "System.Single");
  serializeRatio = ResolveField("BeyondDynamicBone.dll", "BeyondDynamicBone", "ClothSerializeData", "animationPoseRatio", "System.Single");
  clothPropertyWeight = ResolveField("BeyondDynamicBone.dll", "BeyondDynamicBone", "BeyondBoneCloth", "clothSimulateWeightProperty", "System.Single");
  clothPropertyRatio = ResolveField("BeyondDynamicBone.dll", "BeyondDynamicBone", "BeyondBoneCloth", "animationPoseRatioProperty", "System.Single");
  if (!type || !clothWeight || !clothRatio || !getter || !serializeWeight || !serializeRatio || !clothPropertyWeight || !clothPropertyRatio) { Log("cloth restoration contract unavailable"); return; }
  for (const auto& node : nodes) {
    void* go = nullptr; void* array = nullptr;
    if (!Call(ComponentGO, node.object, nullptr, &go)) continue;
    Pin goPin(go); void* args[] = {type.object};
    if (!goPin || !Call(GetComponents, goPin.object, args, &array)) continue;
    Pin arrayPin(array); int count = 0;
    if (!arrayPin || !Read(ArrayLength, arrayPin.object, count) || count < 0 || count > 64) continue;
    for (int i = 0; i < count; ++i) {
      if (cloth.size() == 64) { Log("cloth capacity exceeded"); return; }
      ClothState item; item.object = ArrayItem(arrayPin, i);
      if (!item.object || !Alive(item.object.object)) continue;
      void* data = nullptr;
      if (!ManagedCall(getter, item.object, nullptr, &data) || !(item.serialize = Pin(data)) ||
          !Read(GetEnabled, item.object.object, item.enabled) ||
          !FieldValue(serializeWeight, item.serialize, item.weight) || !FieldValue(serializeRatio, item.serialize, item.ratio) ||
          !FieldValue(clothPropertyWeight,item.object,item.propertyWeight) || !FieldValue(clothPropertyRatio,item.object,item.propertyRatio) ||
          !std::isfinite(item.weight) || !std::isfinite(item.ratio) || !std::isfinite(item.propertyWeight) || !std::isfinite(item.propertyRatio)) {
        Log("cloth original state unreadable; component left under game control"); continue;
      }
      if (!metadata.setField && (item.propertyWeight != item.weight || item.propertyRatio != item.ratio) &&
          options.cloth == BetterEndfield::EiemBody::ClothMode::Stable) {
        Log("cloth stable unavailable: distinct property/serialize originals require field_set_value export"); continue;
      }
      // The PC service parks non-humanoid cloth roots at their Avatar pose.
      // A managed List<Transform> accessor replaces its raw list layout here.
      const auto rootsField=ResolveField("BeyondDynamicBone.dll","BeyondDynamicBone","ClothSerializeData","rootBones",
          "System.Collections.Generic.List<UnityEngine.Transform>");
      Pin roots=Field(rootsField,item.serialize);
      const auto getCount=RuntimeMethod(roots,"get_Count","System.Int32",{});
      const auto getItem=RuntimeMethod(roots,"get_Item","UnityEngine.Transform",{"System.Int32"});
      int rootsCount=0;
      if (getItem && ManagedRead(getCount,roots,rootsCount) && rootsCount>=0 && rootsCount<=128) {
        for (int index=0;index<rootsCount;++index) {
          void* transform=nullptr;void* rootArgs[]={&index};
          if (!ManagedCall(getItem,roots,rootArgs,&transform)) continue;
          bool mapped=false,seen=false;
          for (const auto& bone:bones) mapped |= bone.transform.object==transform;
          for (const auto& anchor:clothAnchors) seen |= anchor.transform.object==transform;
          const auto natural=avatarLocalPoses.find(transform);
          if (!transform || mapped || seen || natural==avatarLocalPoses.end() || clothAnchors.size()==128) continue;
          ClothAnchor anchor;anchor.transform=Pin(transform);anchor.natural=natural->second;
          if (!anchor.transform || !Read(GetLocalPosition,transform,anchor.original.position) ||
              !Read(GetLocalRotation,transform,anchor.original.rotation)) continue;
          clothAnchors.emplace_back(std::move(anchor));
        }
      } else Log("cloth root anchor metadata unavailable; only verified weight/ratio/enabled mode is available");
      cloth.emplace_back(std::move(item));
    }
  }
}
void ApplyCloth() {
  for (auto& anchor:clothAnchors) if (Alive(anchor.transform.object)) {
    anchor.changed=true;
    if (!Set(SetLocalPosition,anchor.transform.object,&anchor.natural.position) ||
        !Set(SetLocalRotation,anchor.transform.object,&anchor.natural.rotation)) Log("cloth anchor managed write failed");
  }
  for (auto& item : cloth) {
    if (!Alive(item.object.object)) continue;
    item.changed = true;
    if (options.cloth == BetterEndfield::EiemBody::ClothMode::Freeze) {
      bool value = false;
      if (!Set(SetEnabled, item.object.object, &value)) Log("cloth freeze failed");
    } else if (options.cloth == BetterEndfield::EiemBody::ClothMode::Stable) {
      item.changedNumeric = true;
      float weight = 1, ratio = 0;
      void* weightArgs[] = {&weight}; void* ratioArgs[] = {&ratio};
      if (!ManagedCall(clothWeight, item.object, weightArgs) || !ManagedCall(clothRatio, item.object, ratioArgs)) Log("cloth stable write failed");
    }
  }
}
