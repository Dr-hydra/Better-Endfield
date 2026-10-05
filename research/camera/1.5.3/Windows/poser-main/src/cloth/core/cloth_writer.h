#pragma once
#include <intrin.h>
using ClothWeightWriterFn = void(__fastcall *)(void *, float, void *);
static ClothWeightWriterFn s_clothOriginalWeightWriter = nullptr;
static void (*s_clothWeightHookInstaller)(void *) = nullptr;
static thread_local unsigned s_clothWeightCommandDepth = 0;
static thread_local eiem_cloth::DeferredWeightWrites s_clothDeferredWeights;
static bool ClothInvokeWeightCommand(void *method, void *obj, void **args, void *&result) {
  bool ok = false;
  ++s_clothWeightCommandDepth;
  __try {
    ok = ClothInvoke(method, obj, args, result);
  } __finally {
    --s_clothWeightCommandDepth;
  }
  return ok;
}
static bool ClothWriterIdentity(ClothInstance &i, void *object) {
  if (ClothTarget(i.ref) != object || !i.processHandle || !i.weightSerializeHandle)
    return false;
  void *process = nullptr, *serialize = nullptr;
  return ClothInvoke(i.api.process, object, nullptr, process) && process &&
         process == il2cpp_gchandle_get_target(i.processHandle) &&
         ClothInvoke(i.api.serialize, object, nullptr, serialize) && serialize &&
         serialize == il2cpp_gchandle_get_target(i.weightSerializeHandle);
}
static bool ClothWriterActive(ClothInstance &i, void *object) {
  void *process = il2cpp_gchandle_get_target(i.processHandle);
  void *cls = il2cpp_object_get_class(process), *go = nullptr;
  if (cls != i.writerProcessClass) {
    static const char *names[] = {"IsValid",
                                  "IsRunning",
                                  "get_IsEnable",
                                  "IsCameraCullingInvisible",
                                  "IsDistanceCullingInvisible",
                                  "IsLodCulled",
                                  "IsSkipWriting"};
    for (int n = 0; n < 7; ++n)
      i.writerStateMethods[n] = ClothMethod(cls, names[n], "System.Boolean");
    i.writerProcessClass = cls;
  }
  bool enabled = false, active = false;
  float time = NAN;
  if (!ClothValue(s_clothUnity.getEnabled, object, enabled) || !enabled ||
      !ClothInvoke(s_clothUnity.getGO, object, nullptr, go) || !go ||
      !ClothValue(s_clothUnity.active, go, active) || !active ||
      !ClothValue(s_clothUnity.globalTime, nullptr, time) || !std::isfinite(time) || time <= 0)
    return false;
  for (int n = 0; n < 7; ++n) {
    bool value = false;
    if (!ClothValue(i.writerStateMethods[n], process, value) || (n < 3 ? !value : value))
      return false;
  }
  return true;
}
static int ClothWriterFind(void *object) {
  if (s_clothWeightCommandDepth || !ClothOnMainThread() || !s_cloth.active ||
      !ClothOwns(s_cloth.owner))
    return -1;
  for (int n = 0; n < s_cloth.count; ++n) {
    auto &i = s_cloth.instances[n];
    if (i.ref.id.managed != reinterpret_cast<uintptr_t>(object))
      continue;
    void *animator = ClothTarget(s_cloth.animator);
    int scene = 0;
    if (!animator || animator != ClothHostAnimator() || !ClothScene(animator, scene) ||
        scene != s_cloth.scene)
      return -1;
    if (!i.changedWeight || !i.startup.weightSent || !i.weightWriter.ownWriteConfirmed ||
        !ClothWriterIdentity(i, object) || !ClothWriterActive(i, object))
      return -1;
    return n;
  }
  return -1;
}
static int ClothWriterPrepare(void *obj, uintptr_t caller, float requested, float &forwarded,
                              eiem_cloth::Owner &owner) {
  __try {
    const int index = ClothWriterFind(obj);
    if (index < 0)
      return -1;
    owner = s_cloth.owner;
    auto &i = s_cloth.instances[index];
    if (i.weightWriter.ShouldOverride(caller, requested))
      forwarded = eiem_cloth::PlaybackWeight;
    return index;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return -1;
  }
}
static void ClothWriterReadback(int index, eiem_cloth::Owner owner, void *obj, uintptr_t caller,
                                float requested, float forwarded) {
  __try {
    if (index < 0 || !ClothOnMainThread() || !ClothOwns(owner) || index >= s_cloth.count)
      return;
    auto &i = s_cloth.instances[index];
    if (!ClothWriterIdentity(i, obj))
      return;
    void *sd = il2cpp_gchandle_get_target(i.weightSerializeHandle);
    float actual = NAN, property = NAN;
    const bool read = ClothField(sd, "clothSimulateWeight", "System.Single", actual) &&
                      ClothField(obj, "clothSimulateWeightProperty", "System.Single", property);
    const int frame = ClothFrame();
    auto &e = i.weightWriter;
    if (forwarded != requested) {
      if (!read || !eiem_cloth::WeightAtTarget(actual) || !eiem_cloth::WeightAtTarget(property)) {
        e.armed = false;
        e.ownWriteConfirmed = false;
        Log("[CLOTH-WRITER-FAILED] session=%llu instance=%d frame=%d caller=%p requested=%g "
            "forwarded=%g read=%d actual=%g baseReadbackProtection=retained",
            (unsigned long long)owner.session, i.ref.id.instance, frame,
            reinterpret_cast<void *>(caller), requested, forwarded, read, actual);
      } else if (++e.intercepted == 1 || e.intercepted % 300 == 0) {
        Log("[CLOTH-WRITER-GUARD] backend=%s generation=%llu session=%llu instance=%d frame=%d "
            "caller=%p incoming=%g forwarded=%g actual=%g property=%g writes=%u "
            "scope=current-bbc-same-producer jobWeight=unknown",
            "MMD",
            (unsigned long long)owner.generation, (unsigned long long)owner.session,
            i.ref.id.instance, frame, reinterpret_cast<void *>(caller), requested, forwarded,
            actual, property, e.intercepted);
      }
    } else if (read && std::isfinite(property) && fabsf(property - actual) <= .000001f && !e.armed) {
      const unsigned previous = e.observations;
      const bool armed = e.Observe(frame, caller, requested, actual);
      if (e.observations != previous || armed) {
        Log("[CLOTH-WRITER-OBSERVED] backend=%s generation=%llu session=%llu instance=%d "
            "process=%p serialize=%p frame=%d caller=%p requested=%g actual=%g property=%g "
            "observations=%u armed=%d",
            "MMD",
            (unsigned long long)owner.generation, (unsigned long long)owner.session,
            i.ref.id.instance, sd ? il2cpp_gchandle_get_target(i.processHandle) : nullptr, sd,
            frame, reinterpret_cast<void *>(caller), requested, actual, property, e.observations,
            armed);
      }
    }
  } __except (EXCEPTION_EXECUTE_HANDLER) {
  }
}
static __declspec(noinline) void __fastcall ClothNativeWeightWriter(void *obj, float requested,
                                                                    void *method) {
  const auto original = s_clothOriginalWeightWriter;
  if (!original)
    return;
  std::unique_lock<std::recursive_mutex> poseLock(g_poseMutex, std::try_to_lock);
  const uintptr_t caller = reinterpret_cast<uintptr_t>(_ReturnAddress());
  if (!poseLock.owns_lock() || !ClothOnMainThread()) {
    const bool observe=ClothOnMainThread()&&!s_clothWeightCommandDepth;
    original(obj,requested,method);
    if(observe)s_clothDeferredWeights.Record(reinterpret_cast<uintptr_t>(obj),caller,requested,GetTickCount64());
    return;
  }
  for(unsigned actor=0;actor<ClothActorCount;++actor) {
    if(!ClothActorEngaged(actor))continue;
    ClothActorScope scope(actor);float forwarded=requested;eiem_cloth::Owner owner{};
    const int index=ClothWriterPrepare(obj,caller,requested,forwarded,owner);
    if(index<0)continue;
    original(obj,forwarded,method);
    ClothWriterReadback(index,owner,obj,caller,requested,forwarded);
    return;
  }
  original(obj,requested,method);
}
