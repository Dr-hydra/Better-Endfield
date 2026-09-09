#include "BetterEndfield/ModuleApi.h"
#include "dash_policy.h"
#include "pose_policy.h"
#include <fstream>

#include <Windows.h>
#include <algorithm>
#include <atomic>
#include <cstdint>
#include <cstdio>
#include <cstring>
#include <memory>
#include <filesystem>
#include <string>
#include <string_view>

namespace BetterEndfield::Actions {
namespace {
constexpr char kId[] = "betterendfield.actions";
constexpr char kCharacter[] = "chr_0013_aglina";
constexpr char kPerform[] = "CharIntPerform_Aglina_Spdash";
constexpr char kGame[] = "Gameplay.Beyond.dll";
constexpr char kUnity[] = "UnityEngine.AnimationModule.dll";
constexpr char kUnityCore[] = "UnityEngine.CoreModule.dll";
constexpr char kParticles[] = "UnityEngine.ParticleSystemModule.dll";
constexpr char kView[] = "Beyond.Gameplay.View";
constexpr char kCore[] = "Beyond.Gameplay.Core";
constexpr char kAnimation[] = "Beyond.Gameplay.View.Animation";
constexpr char kCharacterComp[] = "CharacterAnimationComponent";
constexpr char kComplex[] = "ComplexAnimatorComponent";
constexpr char kBrain[] = "CharacterSpecialDashBrain";
constexpr char kPerformNs[] = "Beyond.Gameplay.Core.CharInteractPerform";
constexpr char kHandle[] = "CharPerformHandleBase";

struct Configuration {
    bool enabled = false;
    bool diagnostics = true;
    bool external_loop = false; // v12: external bone-pose data, not AnimationClip bundles.
    bool operator==(const Configuration&) const = default;
};
std::atomic<std::shared_ptr<const Configuration>> g_config{std::make_shared<Configuration>()};
const BE_HostApiV1* g_host = nullptr;
std::atomic_bool g_stopping{false};
std::atomic<DWORD> g_game_thread{0};

enum MethodId {
    Start, PreLate, ShouldInterrupt, Interrupt, ForceStop, ForceCurrent, Release,
    GetEntity, EntityValid, GetTemplate, GetCharCtrl, IsMain, GetMovement,
    GetGait, GaitValue, GetBlackboard, IsDead, GetAnimator,
    CurrentState, NextState, InTransition, StateHash, StateTime, StateLength,
    IsPlayingPerform, PerformId, SetDashing, MoveMode, Moving, Airborne,
    FlowTick, PerformClear, PerformState, HandleAlive, StateLoop, AddCommand,
    TickStateInterrupt, CrossFade, TryExit, CheckTrackEnd, ShowObject,
    AudioMonoPost, AudioMonoComponent, AudioEntityPost, AudioHash,
    EffectName, EffectObject, EffectDuration, EffectFinish, EffectStop,
    ComponentsInChildren, ArrayLength, ArrayItem, ParticleMain,
    ParticleLoop, ParticleSetLoop, ParticleDuration, ParticleSetDuration,
    ParticlePlay, ParticleStop, UnityAlive,
    BundleLoad, BundleAsset, BundleUnload, ClipLength, ClipHuman, ClipName,
    ControllerGet, ControllerSet, ControllerClips, OverrideCtor, OverrideClip,
    CurrentClipInfos, InfoClip, DestroyOwned,
    PoseTail, PoseComponentTransform, PoseFind, PoseGetLocal, PoseSetLocal, PoseFrameCount, PoseIsChild, MethodCount
};
struct Method { BE_MethodDescriptorV1 desc; BE_ResolvedMethodV1 resolved{}; };
Method g_methods[] = {
    {{kGame,kView,kCharacterComp,"StartSpDash","System.Int32","System.Void",1}},
    {{kGame,kView,kCharacterComp,"PreLateTick","System.Single","System.Void",1}},
    {{kGame,kAnimation,kBrain,"ShouldInterruptSpDash","Beyond.Gameplay.View.CharacterAnimationComponent","System.Boolean",1}},
    {{kGame,kView,kCharacterComp,"InterruptSpDashPerform",nullptr,"System.Void",0}},
    {{kGame,kView,kCharacterComp,"ForceStopSpDashPerform",nullptr,"System.Void",0}},
    {{kGame,kView,kCharacterComp,"ForceStopCurrentStatePerform","System.Boolean","System.Void",1}},
    {{kGame,kView,kCharacterComp,"ReleaseInternal","System.Boolean","System.Void",1}},
    {{kGame,kCore,"BaseComponent","get_entity",nullptr,"Beyond.Gameplay.Core.Entity",0}},
    {{kGame,kCore,"Entity","IsValid",nullptr,"System.Boolean",0}},
    {{kGame,kCore,"Entity","get_templateData",nullptr,"Beyond.Gameplay.EntityTemplateData",0}},
    {{kGame,kCore,"Entity","get_charCtrl",nullptr,"Beyond.Gameplay.Core.CharacterController",0}},
    {{kGame,kCore,"CharacterController","get_isMainCharacter",nullptr,"System.Boolean",0}},
    {{kGame,kCore,"Entity","get_movementComponent",nullptr,"Beyond.Gameplay.Core.MovementComponent",0}},
    {{kGame,kCore,"MovementComponent","get_moveGait",nullptr,"Beyond.Gameplay.Core.MoveGait",0}},
    {{kGame,kCore,"MoveGait","get_gait",nullptr,"Beyond.Gameplay.Core.MovementComponent.GroundedMoveGait",0}},
    {{kGame,kView,kComplex,"get_blackboard",nullptr,"Beyond.Gameplay.View.Animation.AnimBlackboard",0}},
    {{kGame,kView,"CharacterAnimationBlackboard","get_isDead",nullptr,"System.Boolean",0}},
    {{kGame,kView,kComplex,"get_animator",nullptr,"UnityEngine.Animator",0}},
    {{kUnity,"UnityEngine","Animator","GetCurrentAnimatorStateInfo","System.Int32","UnityEngine.AnimatorStateInfo",1}},
    {{kUnity,"UnityEngine","Animator","GetNextAnimatorStateInfo","System.Int32","UnityEngine.AnimatorStateInfo",1}},
    {{kUnity,"UnityEngine","Animator","IsInTransition","System.Int32","System.Boolean",1}},
    {{kUnity,"UnityEngine","AnimatorStateInfo","get_shortNameHash",nullptr,"System.Int32",0}},
    {{kUnity,"UnityEngine","AnimatorStateInfo","get_normalizedTime",nullptr,"System.Single",0}},
    {{kUnity,"UnityEngine","AnimatorStateInfo","get_length",nullptr,"System.Single",0}},
    {{kGame,kView,kCharacterComp,"IsPlayingSpDashPerform",nullptr,"System.Boolean",0}},
    {{kGame,kView,"CharacterAnimationComponent.PerformRequest","GetPlayingPerformId",nullptr,"System.String",0}},
    {{kGame,kView,"CharacterAnimationBlackboard","set_isDashing","System.Boolean","System.Void",1}},
    {{kGame,kCore,"MovementComponent","get_moveMode",nullptr,"Beyond.Gameplay.Core.MovementComponent.MoveMode",0}},
    {{kGame,kCore,"MovementComponent","get_isMovingOnGround",nullptr,"System.Boolean",0}},
    {{kGame,kCore,"MovementComponent","get_isInAir",nullptr,"System.Boolean",0}},
    {{kGame,kPerformNs,kHandle,"_TickMainFlow","System.Single","System.Void",1}},
    {{kGame,kPerformNs,kHandle,"Clear","System.Boolean","System.Void",1}},
    {{kGame,kPerformNs,kHandle,"get_curState",nullptr,"Beyond.Gameplay.CharInteractPerformEnums.CharInteractPerformState",0}},
    {{kGame,kPerformNs,kHandle,"_CheckEntityAlive",nullptr,"System.Boolean",0}},
    {{kUnity,"UnityEngine","AnimatorStateInfo","get_loop",nullptr,"System.Boolean",0}},
    {{kGame,kPerformNs,kHandle,"AddInterruptCommand","Beyond.Gameplay.CharInteractPerformEnums.InterruptReason|Beyond.Gameplay.CharInteractPerformEnums.InterruptType|System.Boolean","System.Void",3}},
    {{kGame,kView,kCharacterComp,"_TickStatePerformInterrupt",nullptr,"System.Void",0}},
    {{kUnity,"UnityEngine","Animator","CrossFade","System.Int32|System.Single|System.Int32|System.Single|System.Single|UnityEngine.AnimationBlendStyle|UnityEngine.AnimatorTransitionInterruptionSource|System.Boolean","System.Void",8}},
    {{kGame,kPerformNs,kHandle,"_TryExit","Beyond.Gameplay.Core.CharInteractPerform.InterruptCommandData","System.Boolean",1}},
    {{kGame,kPerformNs,kHandle,"_CheckTrackEnd",nullptr,"System.Boolean",0}},
    {{kGame,kPerformNs,kHandle,"ShowObject","Beyond.Gameplay.Core.CharInteractPerform.ObjectShowActData","System.Void",1}},
    {{kGame,kAnimation,"AnimatorMono","PostAudioEvent","System.UInt32|System.Single","System.Void",2}},
    {{kGame,kAnimation,"AnimatorMono","get_innerAnimatorComponent",nullptr,"Beyond.Gameplay.View.ComplexAnimatorComponent",0}},
    {{kGame,"Beyond.Gameplay.Audio","AudioManager","PostEvent","Beyond.Gameplay.Core.Entity|System.UInt32|Beyond.Audio.AudioCallbackType|Beyond.Audio.AudioEventCallback|System.Object","System.UInt32",5}},
    {{"Audio.Beyond.dll","Beyond.Audio","AudioHashGenerator","Compute","System.String","System.UInt32",1}},
    {{kGame,"Beyond.Gameplay","EffectInstance","get_effectName",nullptr,"System.String",0}},
    {{kGame,"Beyond.Gameplay","EffectInstance","get_effectObj",nullptr,"UnityEngine.GameObject",0}},
    {{kGame,"Beyond.Gameplay","EffectInstance","DurationTick","System.Single","System.Void",1}},
    {{kGame,"Beyond.Gameplay","EffectInstance","Finish","System.Boolean|System.Boolean","System.Void",2}},
    {{kGame,"Beyond.Gameplay","EffectInstance","OnStop",nullptr,"System.Void",0}},
    {{kUnityCore,"UnityEngine","GameObject","GetComponentsInChildren","System.Type|System.Boolean","UnityEngine.Component[]",2}},
    {{"mscorlib.dll","System","Array","get_Length",nullptr,"System.Int32",0}},
    {{"mscorlib.dll","System","Array","GetValue","System.Int32","System.Object",1}},
    {{kParticles,"UnityEngine","ParticleSystem","get_main",nullptr,"UnityEngine.ParticleSystem.MainModule",0}},
    {{kParticles,"UnityEngine","ParticleSystem.MainModule","get_loop",nullptr,"System.Boolean",0}},
    {{kParticles,"UnityEngine","ParticleSystem.MainModule","set_loop","System.Boolean","System.Void",1}},
    {{kParticles,"UnityEngine","ParticleSystem.MainModule","get_duration",nullptr,"System.Single",0}},
    {{kParticles,"UnityEngine","ParticleSystem.MainModule","set_duration","System.Single","System.Void",1}},
    {{kParticles,"UnityEngine","ParticleSystem","Play","System.Boolean","System.Void",1}},
    {{kParticles,"UnityEngine","ParticleSystem","Stop","System.Boolean|UnityEngine.ParticleSystemStopBehavior","System.Void",2}},
    {{kUnityCore,"UnityEngine","Object","op_Implicit","UnityEngine.Object","System.Boolean",1}},
    {{"UnityEngine.AssetBundleModule.dll","UnityEngine","AssetBundle","LoadFromFile","System.String","UnityEngine.AssetBundle",1}},
    {{"UnityEngine.AssetBundleModule.dll","UnityEngine","AssetBundle","LoadAsset","System.Int64|System.Type","UnityEngine.Object",2}},
    {{"UnityEngine.AssetBundleModule.dll","UnityEngine","AssetBundle","Unload","System.Boolean","System.Void",1}},
    {{kUnity,"UnityEngine","AnimationClip","get_length",nullptr,"System.Single",0}},
    {{kUnity,"UnityEngine","AnimationClip","get_humanMotion",nullptr,"System.Boolean",0}},
    {{kUnityCore,"UnityEngine","Object","get_name",nullptr,"System.String",0}},
    {{kUnity,"UnityEngine","Animator","get_runtimeAnimatorController",nullptr,"UnityEngine.RuntimeAnimatorController",0}},
    {{kUnity,"UnityEngine","Animator","set_runtimeAnimatorController","UnityEngine.RuntimeAnimatorController","System.Void",1}},
    {{kUnity,"UnityEngine","RuntimeAnimatorController","get_animationClips",nullptr,"UnityEngine.AnimationClip[]",0}},
    {{kUnity,"UnityEngine","AnimatorOverrideController",".ctor","UnityEngine.RuntimeAnimatorController","System.Void",1}},
    {{kUnity,"UnityEngine","AnimatorOverrideController","SetClip","UnityEngine.AnimationClip|UnityEngine.AnimationClip|System.Boolean","System.Void",3}},
    {{kUnity,"UnityEngine","Animator","GetCurrentAnimatorClipInfo","System.Int32","UnityEngine.AnimatorClipInfo[]",1}},
    {{kUnity,"UnityEngine","AnimatorClipInfo","get_clip",nullptr,"UnityEngine.AnimationClip",0}},
    {{kUnityCore,"UnityEngine","Object","Destroy","UnityEngine.Object","System.Void",1}},
    {{kGame,kView,kCharacterComp,"TailLateTick","System.Single","System.Void",1}},
    {{kUnityCore,"UnityEngine","Component","get_transform",nullptr,"UnityEngine.Transform",0}},
    {{kUnityCore,"UnityEngine","Transform","Find","System.String","UnityEngine.Transform",1}},
    {{kUnityCore,"UnityEngine","Transform","GetLocalPositionAndRotation","UnityEngine.Vector3&|UnityEngine.Quaternion&","System.Void",2}},
    {{kUnityCore,"UnityEngine","Transform","SetLocalPositionAndRotation_Injected","UnityEngine.Vector3&|UnityEngine.Quaternion&","System.Void",2}},
    {{kUnityCore,"UnityEngine","Time","get_frameCount",nullptr,"System.Int32",0}},
    {{kUnityCore,"UnityEngine","Transform","IsChildOf","UnityEngine.Transform","System.Boolean",1}},
};
static_assert(std::size(g_methods) == MethodCount);
struct Field { BE_FieldDescriptorV1 desc; BE_ResolvedFieldV1 resolved{}; };
enum FieldId { TemplateId, Request, RequestHandle, HasCommand,
    CommandType, CommandReason, ActionLogic, ActionIndex, ActionShow, ActionDelay, Effects, FieldCount };
Field g_fields[] = {
    {{kGame,"Beyond.Gameplay","GameDataWithId","id","System.String"}},
    {{kGame,kView,kCharacterComp,"m_currentRequest","Beyond.Gameplay.View.CharacterAnimationComponent.PerformRequest"}},
    {{kGame,kView,"CharacterAnimationComponent.PerformRequest","handle","Beyond.Gameplay.Core.CharInteractPerform.CharPerformHandleBase"}},
    {{kGame,kPerformNs,kHandle,"m_hasCommand","System.Boolean"}},
    {{kGame,kPerformNs,"InterruptCommandData","interruptType","Beyond.Gameplay.CharInteractPerformEnums.InterruptType"}},
    {{kGame,kPerformNs,"InterruptCommandData","reason","Beyond.Gameplay.CharInteractPerformEnums.InterruptReason"}},
    {{kGame,kPerformNs,"CharInteractPerformActionData","logicId","System.UInt32"}},
    {{kGame,kPerformNs,"ActorActData","actorIndex","System.Int32"}},
    {{kGame,kPerformNs,"ObjectShowActData","show","System.Boolean"}},
    {{kGame,kPerformNs,"CharInteractPerformActionData","delay","System.Single"}},
    {{kGame,kPerformNs,kHandle,"m_effects","System.Collections.Generic.Dictionary<System.Int32,Beyond.ObjectPtr<Beyond.Gameplay.EffectInstance>>"}},
};
static_assert(std::size(g_fields) == FieldCount);

using StartFn = void(__fastcall*)(void*, int, const void*);
using TickFn = void(__fastcall*)(void*, float, const void*);
// This method is static: RCX is the animation component, not a Brain instance.
using InterruptCheckFn = bool(__fastcall*)(void*, const void*);
using VoidFn = void(__fastcall*)(void*, const void*);
using BoolArgFn = void(__fastcall*)(void*, bool, const void*);
StartFn g_start = nullptr;
TickFn g_pre_late = nullptr;
InterruptCheckFn g_check = nullptr;
VoidFn g_interrupt = nullptr, g_force = nullptr;
BoolArgFn g_force_current = nullptr, g_release = nullptr, g_set_dashing = nullptr;
TickFn g_flow_tick = nullptr;
BoolArgFn g_perform_clear = nullptr;
using CommandFn = void(__fastcall*)(void*, int, int, bool, const void*);
CommandFn g_add_command = nullptr;
VoidFn g_tick_state_interrupt = nullptr;
using TryExitFn = bool(__fastcall*)(void*, void*, const void*);
TryExitFn g_try_exit = nullptr;
using CheckTrackFn = bool(__fastcall*)(void*, const void*);
using ShowFn = void(__fastcall*)(void*, void*, const void*);
using AudioMonoFn = void(__fastcall*)(void*, uint32_t, float, const void*);
using AudioEntityFn = uint32_t(__fastcall*)(void*, uint32_t, int, void*, void*, const void*);
CheckTrackFn g_check_track = nullptr;
ShowFn g_show = nullptr;
AudioMonoFn g_audio_mono = nullptr;
AudioEntityFn g_audio_entity = nullptr;
TickFn g_effect_duration = nullptr;
using EffectFinishFn = void(__fastcall*)(void*, bool, bool, const void*);
EffectFinishFn g_effect_finish = nullptr;
VoidFn g_effect_stop = nullptr;
using ObjectClassFn = void*(*)(void*);
using ClassMethodFn = const void*(*)(void*, const char*, int);
using ClassParentFn = void*(*)(void*);
ObjectClassFn g_object_class = nullptr;
ClassMethodFn g_class_method = nullptr;
ClassParentFn g_class_parent = nullptr;
void* g_particle_type = nullptr;
uint32_t g_particle_type_root = 0;
int g_particle_stop_clear = -1;
thread_local void* g_main_flow_handle = nullptr;

struct ParticleHold { void* object = nullptr; uint32_t root = 0; bool loop = false; float duration = 0; };
struct EffectHold {
    void* object = nullptr;
    uint32_t root = 0;
    bool prepared = false;
    bool disabled = false;
    uint32_t ticks = 0;
    ParticleHold particles[8]{};
    int count = 0;
};
struct Session {
    void* component = nullptr;
    void* handle = nullptr;
    uint32_t root = 0;
    uint32_t handle_root = 0;
    std::shared_ptr<const Configuration> config;
    DashPolicy policy;
    LoopSchedule loop;
    ImportedLoopSchedule imported_loop;
    bool external_expected = false, external_confirmed = false;
    float external_wait = 0;
    void* entity = nullptr;
    bool deferred_flying_stop = false;
    uint32_t hidden_deferrals = 0;
    EffectHold effects[2]{};
    bool effect_error_logged = false;
    void* blackboard = nullptr;
    bool last_requested_dashing = false;
    bool overridden_dashing = false;
    int last_special_hash = 0;
    uint32_t side_changes = 0;
    uint32_t natural_end_deferrals = 0;
    int last_progress_bucket = -1;
    uint32_t animation_replays = 0;
    bool held_state_interrupt = false;
    uint32_t try_exit_calls = 0;
    uint32_t spdash_interrupts_suppressed = 0;
    int last_interrupt_reason = -1;
    int last_interrupt_type = -1;
};
// Only game-thread hooks access the session. Configuration callbacks publish an
// immutable snapshot; no Unity/managed calls are made on the settings thread.
Session g_session;
const void* g_hash_fields[2]{};
int g_hashes[2]{};
const void* g_locomotion_fields[2]{};
int g_locomotion_hashes[2]{};
int g_sprint = -1, g_dash_mode = -1, g_grounded_mode = -1;
int g_perform_end = -1;
int g_sp_dash_end = -1, g_force_exit = -1;
int g_blend_style = -1, g_blend_interrupt = -1, g_audio_none = -1;
uint32_t g_flying_stop_hash = 0;

void Log(const char* message) {
    if (g_host && g_host->log) g_host->log(g_host->context, kId, message);
}
void* Invoke(MethodId id, void* instance, void** args, bool& ok) {
    if (!ok) return nullptr;
    void* exception = nullptr;
    void* result = g_host->runtime_invoke(g_host->context,
        g_methods[id].resolved.method_info, instance, args, &exception);
    if (exception) ok = false;
    return result;
}
void* Object(MethodId id, void* instance, bool& ok) {
    if (!instance) { ok = false; return nullptr; }
    void* result = Invoke(id, instance, nullptr, ok);
    if (!result) ok = false;
    return result;
}
template<class T> T Unbox(void* object, bool& ok) {
    T result{};
    if (!ok || !object) { ok = false; return result; }
    void* value = g_host->object_unbox(g_host->context, object);
    if (!value) { ok = false; return result; }
    std::memcpy(&result, value, sizeof(T));
    return result;
}
template<class T> T Value(MethodId id, void* instance, bool& ok, void** args = nullptr) {
    if (!instance) { ok = false; return {}; }
    return Unbox<T>(Invoke(id, instance, args, ok), ok);
}
void* FieldObject(FieldId id, void* instance) {
    return g_host->field_get_value_object(g_host->context,
        g_fields[id].resolved.field_info, instance);
}
bool StringEquals(void* managed, const char* expected) {
    char text[160]{};
    return managed && g_host->copy_managed_string(g_host->context,
        managed, text, sizeof(text)) > 0 && std::strcmp(text, expected) == 0;
}
void* OwnedPerform(void* component) {
    bool ok = true;
    if (!Value<bool>(IsPlayingPerform, component, ok) || !ok) return nullptr;
    void* request = FieldObject(Request, component);
    if (!request) return nullptr;
    void* unboxed = g_host->object_unbox(g_host->context, request);
    if (!unboxed) return nullptr;
    void* id = Invoke(PerformId, unboxed, nullptr, ok);
    return ok && StringEquals(id, kPerform) ? FieldObject(RequestHandle, request) : nullptr;
}
void* InvokeNamed(void* object, const char* name, int count, void** args, bool& ok, bool value_type = false) {
    if (!ok || !object) { ok = false; return nullptr; }
    void* cls = g_object_class(object);
    const void* method = nullptr;
    for (void* current = cls; current && !method; current = g_class_parent(current))
        method = g_class_method(current, name, count);
    if (!method) { ok = false; return nullptr; }
    void* instance = value_type ? g_host->object_unbox(g_host->context, object) : object;
    if (!instance) { ok = false; return nullptr; }
    void* exception = nullptr;
    void* result = g_host->runtime_invoke(g_host->context, method, instance, args, &exception);
    if (exception) ok = false;
    return result;
}
bool UnityObjectAlive(void* object) {
    if (!object) return false;
    bool ok = true;
    void* args[]{object};
    return Unbox<bool>(Invoke(UnityAlive, nullptr, args, ok), ok) && ok;
}
void* ParticleMainValue(void* particle, bool& ok) {
    void* boxed = Invoke(ParticleMain, particle, nullptr, ok);
    void* result = boxed ? g_host->object_unbox(g_host->context, boxed) : nullptr;
    if (!result) ok = false;
    return result;
}
void StopParticle(void* particle, bool& ok) {
    bool children = false;
    void* args[]{&children, &g_particle_stop_clear};
    Invoke(ParticleStop, particle, args, ok);
}
void RestoreEffect(EffectHold& effect, bool restore_unity) {
    EffectHold old = effect;
    effect = {};
    effect.disabled = true; // Do not reacquire an instance that native code has finished.
    for (int i = 0; i < old.count; ++i) {
        auto& particle = old.particles[i];
        if (restore_unity && UnityObjectAlive(particle.object)) {
            bool ok = true;
            StopParticle(particle.object, ok);
            void* main = ParticleMainValue(particle.object, ok);
            void* loop_args[]{&particle.loop};
            Invoke(ParticleSetLoop, main, loop_args, ok);
            void* duration_args[]{&particle.duration};
            Invoke(ParticleSetDuration, main, duration_args, ok);
        }
        if (particle.root) g_host->gchandle_free(g_host->context, particle.root);
    }
    if (old.root) g_host->gchandle_free(g_host->context, old.root);
}
bool TargetEffectName(void* effect, int index, bool& ok) {
    char name[384]{};
    void* text = Invoke(EffectName, effect, nullptr, ok);
    if (!ok || !text || g_host->copy_managed_string(g_host->context, text, name, sizeof(name)) <= 0) return false;
    char* base = name;
    for (char* c = name; *c; ++c) if (*c == '/' || *c == '\\') base = c + 1;
    if (auto* suffix = std::strrchr(base, '.'); suffix && _stricmp(suffix, ".prefab") == 0) *suffix = 0;
    return _stricmp(base, index == 0 ? "P_fxbat_aglina_sprint_dash_sp_01" : "P_fxbat_aglina_sprint_dash_sp_02") == 0;
}
void MaintainEffects() {
    if (!g_session.component || !g_session.policy.active) return;
    for (int slot = 0; slot < 2; ++slot) {
        auto& effect = g_session.effects[slot];
        if (effect.disabled || effect.prepared) continue;
        bool ok = true;
        if (!effect.object) {
            void* dictionary = FieldObject(Effects, g_session.handle);
            if (!dictionary) continue;
            void* args[]{&slot};
            bool contains = Unbox<bool>(InvokeNamed(dictionary, "ContainsKey", 1, args, ok), ok);
            if (!ok) {
                if (!g_session.effect_error_logged) Log("Aglina v9: effect dictionary lookup unavailable.");
                g_session.effect_error_logged = true;
                continue;
            }
            if (!contains) continue; // Initial asynchronous creation can still be pending.
            void* boxed_ptr = InvokeNamed(dictionary, "get_Item", 1, args, ok);
            void* object = InvokeNamed(boxed_ptr, "Lock", 0, nullptr, ok, true);
            if (!ok || !object || !TargetEffectName(object, slot, ok)) {
                if (!g_session.effect_error_logged) Log("Aglina v9: effect pointer/identity not ready; no VFX mutation.");
                g_session.effect_error_logged = true;
                continue;
            }
            effect.object = object;
            effect.root = g_host->gchandle_new(g_host->context, object, 1);
            if (!effect.root) { effect = {}; continue; }
        }
        void* go = Invoke(EffectObject, effect.object, nullptr, ok);
        if (!ok || !UnityObjectAlive(go)) continue; // Still loading: do not mutate an absent instance.
        bool inactive = true;
        void* component_args[]{g_particle_type, &inactive};
        void* array = Invoke(ComponentsInChildren, go, component_args, ok);
        int count = Value<int>(ArrayLength, array, ok);
        if (!ok || count <= 0 || count > 8) {
            if (count == 0 && ok) continue;
            RestoreEffect(effect, true);
            if (!g_session.effect_error_logged) Log("Aglina v9: particle enumeration failed; VFX hold skipped.");
            g_session.effect_error_logged = true;
            continue;
        }
        for (int i = 0; i < count && ok; ++i) {
            void* item_args[]{&i};
            void* particle = Invoke(ArrayItem, array, item_args, ok);
            if (!UnityObjectAlive(particle)) { ok = false; break; }
            void* main = ParticleMainValue(particle, ok);
            bool old_loop = Value<bool>(ParticleLoop, main, ok);
            float old_duration = Value<float>(ParticleDuration, main, ok);
            if (!ok || !std::isfinite(old_duration) || old_duration <= 0) { ok = false; break; }
            auto& saved = effect.particles[effect.count];
            saved = {particle, g_host->gchandle_new(g_host->context, particle, 1), old_loop, old_duration};
            if (!saved.root) { ok = false; break; }
            ++effect.count; // Save original state before any instance mutation.
            // The _02 glow is a single 3-second particle in a 5-second system.
            // A plain loop flag would still leave a 2-second gap; overlap renewals.
            if (slot == 1 && std::fabs(old_duration - 5.0f) < .01f) {
                StopParticle(particle, ok);
                float renewal = 2.5f;
                void* duration_args[]{&renewal};
                Invoke(ParticleSetDuration, main, duration_args, ok);
            }
            bool looping = true;
            void* loop_args[]{&looping};
            Invoke(ParticleSetLoop, main, loop_args, ok);
            bool children = false;
            void* play_args[]{&children};
            Invoke(ParticlePlay, particle, play_args, ok);
        }
        if (!ok) {
            RestoreEffect(effect, true);
            Log("Aglina v9: particle setup failed; original instance settings restored.");
        } else {
            effect.prepared = true;
            if (g_session.config->diagnostics) {
                char message[144];
                std::snprintf(message, sizeof(message), "Aglina v9: effect %d continuous particles prepared: count=%d", slot, effect.count);
                Log(message);
            }
        }
    }
}
void* CurrentRequestHandle(void* component) {
    void* request = FieldObject(Request, component);
    return request ? FieldObject(RequestHandle, request) : nullptr;
}
bool CanMove(void* component, bool verify_character) {
    bool ok = true;
    void* entity = Object(GetEntity, component, ok);
    if (!Value<bool>(EntityValid, entity, ok) || !ok) return false;
    if (verify_character) {
        void* data = Object(GetTemplate, entity, ok);
        if (!ok || !StringEquals(FieldObject(TemplateId, data), kCharacter)) return false;
    }
    void* control = Object(GetCharCtrl, entity, ok);
    if (!Value<bool>(IsMain, control, ok) || !ok) return false;
    void* blackboard = Object(GetBlackboard, component, ok);
    if (Value<bool>(IsDead, blackboard, ok) || !ok) return false;
    void* movement = Object(GetMovement, entity, ok);
    int mode = Value<int>(MoveMode, movement, ok);
    bool moving = Value<bool>(Moving, movement, ok);
    bool airborne = Value<bool>(Airborne, movement, ok);
    void* gait = Object(GetGait, movement, ok);
    int value = Value<int>(GaitValue, gait, ok);
    return ok && IsDashOrGroundSprint(mode == g_dash_mode, mode == g_grounded_mode, value == g_sprint, moving, airborne);
}
bool ReadFrame(void* component, Frame& frame, int& current_hash) {
    bool ok = true;
    void* animator = Object(GetAnimator, component, ok);
    if (!ok) return false;
    int layer = 0;
    void* args[]{&layer};
    frame.transitioning = Value<bool>(InTransition, animator, ok, args);
    void* boxed = Invoke(CurrentState, animator, args, ok);
    void* state = boxed ? g_host->object_unbox(g_host->context, boxed) : nullptr;
    current_hash = Value<int>(StateHash, state, ok);
    frame.time = Value<float>(StateTime, state, ok);
    frame.length = Value<float>(StateLength, state, ok);
    frame.clip_loop = Value<bool>(StateLoop, state, ok);
    frame.current_special = current_hash == g_hashes[0] || current_hash == g_hashes[1];
    if (frame.transitioning) {
        boxed = Invoke(NextState, animator, args, ok);
        state = boxed ? g_host->object_unbox(g_host->context, boxed) : nullptr;
        int next = Value<int>(StateHash, state, ok);
        frame.next_hash = next;
        frame.next_time = Value<float>(StateTime, state, ok);
        frame.entering_special = next == g_hashes[0] || next == g_hashes[1];
        frame.outgoing = !frame.entering_special;
        frame.returning_to_locomotion = next == g_locomotion_hashes[0] || next == g_locomotion_hashes[1];
    }
    return ok;
}
#include "external_loop.inl"
#include "pose_overlay.inl"
void Cancel(const char* reason, bool stop_perform, bool restore_parameter = true) {
    if (!g_session.component) return;
    Session old = std::move(g_session);
    g_session = {};
    if (g_pose_owner.component == old.component) g_pose_owner.clock.End();
    if (g_external_owner.component == old.component) g_external_owner.pending_restore = true;
    const bool restore_effects = !g_stopping && GetCurrentThreadId() == g_game_thread.load(std::memory_order_relaxed);
    for (auto& effect : old.effects) RestoreEffect(effect, restore_effects);
    if (old.config && old.config->diagnostics) {
        char text[192];
        std::snprintf(text, sizeof(text), "Aglina dash ended: %s, state_changes=%u, natural_end_deferrals=%u, replays=%u", reason, old.side_changes, old.natural_end_deferrals, old.animation_replays);
        Log(text);
    }
    // Restore the latest value requested by the game's own blackboard update.
    // Never call into released components or Unity during worker-thread shutdown.
    if (restore_parameter && old.overridden_dashing && old.blackboard && g_set_dashing)
        g_set_dashing(old.blackboard, old.last_requested_dashing, g_methods[SetDashing].resolved.method_info);
    // Clear ownership before invoking the game; stop callbacks may re-enter us.
    if (restore_parameter && old.deferred_flying_stop && old.entity && g_audio_entity) {
        bool ok = true;
        if (Value<bool>(EntityValid, old.entity, ok) && ok)
            g_audio_entity(old.entity, g_flying_stop_hash, g_audio_none, nullptr, nullptr,
                g_methods[AudioEntityPost].resolved.method_info);
    }
    if (stop_perform && CurrentRequestHandle(old.component) == old.handle && g_interrupt)
        g_interrupt(old.component, g_methods[Interrupt].resolved.method_info);
    if (old.root) g_host->gchandle_free(g_host->context, old.root);
    if (old.handle_root) g_host->gchandle_free(g_host->context, old.handle_root);
}
bool HoldContext(void* component);
bool ShouldHold(void* component) {
    if (g_stopping || !component || component != g_session.component ||
        g_config.load() != g_session.config || !g_session.config->enabled) return false;
    if (!CanMove(component, false) || CurrentRequestHandle(component) != g_session.handle) return false;
    return HoldContext(component);
}
// IsPlayingSpDashPerform can be cleared before the queued SpDashEnd command
// reaches _TryExit. The pinned handle remains the ownership proof for the
// two exit hooks, so this context deliberately omits that transient predicate.
bool HoldContext(void* component) {
    if (g_stopping || !component || component != g_session.component ||
        g_config.load() != g_session.config || !g_session.config->enabled ||
        !CanMove(component, false)) return false;
    // Keep the natural entry alive until the Animator sees Dash's CrossFade.
    if (g_session.policy.WithinEntryWindow()) return true;
    Frame frame;
    int hash = 0;
    return ReadFrame(component, frame, hash) &&
        (frame.current_special || frame.entering_special) &&
        !frame.outgoing;
}
void __fastcall EffectDurationDetour(void* instance, float delta, const void* method) {
    if (GetCurrentThreadId() == g_game_thread.load(std::memory_order_relaxed)) {
        for (auto& effect : g_session.effects) {
            if (effect.object == instance && effect.prepared && ShouldHold(g_session.component)) {
                if (++effect.ticks == 1 && g_session.config->diagnostics)
                    Log("Aglina v9: held target effect lifetime/auto-fade clock; particles keep simulating.");
                delta = 0; // Only the effect lifetime clock, not ParticleSystem/FollowTick.
                break;
            }
        }
    }
    g_effect_duration(instance, delta, method);
}
void RestoreFinishedEffect(void* instance) {
    if (GetCurrentThreadId() != g_game_thread.load(std::memory_order_relaxed)) return;
    for (auto& effect : g_session.effects)
        if (effect.object == instance) RestoreEffect(effect, true);
}
void __fastcall EffectFinishDetour(void* instance, bool force, bool func, const void* method) {
    RestoreFinishedEffect(instance);
    g_effect_finish(instance, force, func, method);
}
void __fastcall EffectStopDetour(void* instance, const void* method) {
    RestoreFinishedEffect(instance);
    g_effect_stop(instance, method);
}
bool ReadMovementState(void* component, int& mode, int& gait, bool& moving, bool& airborne) {
    bool ok = true;
    void* entity = Object(GetEntity, component, ok);
    if (!Value<bool>(EntityValid, entity, ok) || !ok) return false;
    void* movement = Object(GetMovement, entity, ok);
    mode = Value<int>(MoveMode, movement, ok);
    moving = Value<bool>(Moving, movement, ok);
    airborne = Value<bool>(Airborne, movement, ok);
    void* gait_object = Object(GetGait, movement, ok);
    gait = Value<int>(GaitValue, gait_object, ok);
    return ok;
}
void __fastcall StateInterruptDetour(void* component, const void* method) {
    // The generic callback calls ForceExit and clears m_currentRequest directly.
    if (ShouldHold(component)) {
        if (!g_session.held_state_interrupt && g_session.config->diagnostics)
            Log("Aglina: holding _TickStatePerformInterrupt for the owned sprint session (v4).");
        g_session.held_state_interrupt = true;
        return;
    }
    g_tick_state_interrupt(component, method);
}
bool __fastcall TryExitDetour(void* handle, void* command, const void* method) {
    if (handle == g_session.handle && g_session.component) {
        ++g_session.try_exit_calls;
        bool ok = true;
        int type = command ? Unbox<int>(FieldObject(CommandType, command), ok) : -1;
        int reason = command ? Unbox<int>(FieldObject(CommandReason, command), ok) : -1;
        int mode = -1, gait = -1;
        bool moving = false, airborne = false;
        bool movement_ok = ReadMovementState(g_session.component, mode, gait, moving, airborne);
        bool owned = OwnedPerform(g_session.component) == g_session.handle;
        bool hold = ok && movement_ok && HoldContext(g_session.component);
        if (g_session.config->diagnostics &&
            (g_session.try_exit_calls == 1 || (command && (type != g_session.last_interrupt_type || reason != g_session.last_interrupt_reason)))) {
            g_session.last_interrupt_type = type;
            g_session.last_interrupt_reason = reason;
            char text[256];
            std::snprintf(text, sizeof(text), "Aglina Perform _TryExit: command=%p, reason=%d, type=%d, suppress=%d, hold=%d, mode=%d, gait=%d, moving=%d, airborne=%d, owned=%d",
                command, reason, type, hold && reason == g_sp_dash_end && type == g_force_exit,
                hold, mode, gait, moving, airborne, owned);
            Log(text);
        }
        // The game's special-dash end command is the expected transition from
        // dash to sprint. Let every other explicit exit retain its semantics.
        if (hold && reason == g_sp_dash_end && type == g_force_exit)
            return false;
    }
    return g_try_exit(handle, command, method);
}
void __fastcall DashingDetour(void* blackboard, bool value, const void* method) {
    if (blackboard == g_session.blackboard && g_session.component) {
        g_session.last_requested_dashing = value;
        if (!value && ShouldHold(g_session.component)) {
            if (!g_session.overridden_dashing && g_session.config->diagnostics)
                Log("Aglina: preserving native isDashing across Dash -> grounded Sprint.");
            g_session.overridden_dashing = true;
            value = true;
        }
    }
    g_set_dashing(blackboard, value, method);
}

struct FlowScope {
    void* previous;
    explicit FlowScope(void* handle) : previous(g_main_flow_handle) { g_main_flow_handle = handle; }
    ~FlowScope() { g_main_flow_handle = previous; }
};
void __fastcall FlowDetour(void* handle, float delta, const void* method) {
    FlowScope scope(handle);
    g_flow_tick(handle, delta, method);
}
// This resource creates props/effects in End. Do not hook/block entry to End.
bool __fastcall TrackEndDetour(void* handle, const void* method) {
    bool result = g_check_track(handle, method);
    if (!result || handle != g_session.handle || !g_session.component) return result;
    bool ok = true;
    bool command = Unbox<bool>(FieldObject(HasCommand, handle), ok);
    int phase = Value<int>(PerformState, handle, ok);
    bool alive = Value<bool>(HandleAlive, handle, ok);
    bool defer = ok && CanDeferNaturalEnd(true, g_main_flow_handle == handle,
        g_session.policy.active, ShouldHold(g_session.component), command, alive, phase == g_perform_end);
    if (defer) {
        if (++g_session.natural_end_deferrals == 1 && g_session.config->diagnostics)
            Log("Aglina v8: deferred owned Perform End track cleanup.");
        return false;
    }
    return result;
}
void __fastcall ShowObjectDetour(void* handle, void* action, const void* method) {
    if (action && handle == g_session.handle && g_session.policy.active && ShouldHold(g_session.component)) {
        bool ok = true;
        auto logic = Unbox<uint32_t>(FieldObject(ActionLogic, action), ok);
        int index = Unbox<int>(FieldObject(ActionIndex, action), ok);
        bool show = Unbox<bool>(FieldObject(ActionShow, action), ok);
        float delay = Unbox<float>(FieldObject(ActionDelay, action), ok);
        if (ok && IsTargetHide(logic, index, show, delay)) {
            if (++g_session.hidden_deferrals == 1 && g_session.config->diagnostics)
                Log("Aglina v8: deferred logic 11 prop hide; native Clear still owns cleanup.");
            return;
        }
    }
    g_show(handle, action, method);
}
bool DeferFlyingStop(uint32_t hash) {
    if (hash != g_flying_stop_hash || !g_session.policy.active || !ShouldHold(g_session.component)) return false;
    if (!g_session.deferred_flying_stop && g_session.config->diagnostics)
        Log("Aglina v8: deferred Flying_Stop until session exit.");
    g_session.deferred_flying_stop = true;
    return true;
}
void __fastcall AudioMonoDetour(void* mono, uint32_t hash, float clip_in, const void* method) {
    if (GetCurrentThreadId() == g_game_thread.load(std::memory_order_relaxed) &&
        hash == g_flying_stop_hash && g_session.component) {
        bool ok = true;
        void* component = Object(AudioMonoComponent, mono, ok);
        if (ok && component == g_session.component && DeferFlyingStop(hash)) return;
    }
    g_audio_mono(mono, hash, clip_in, method);
}
uint32_t __fastcall AudioEntityDetour(void* entity, uint32_t hash, int callback_type,
    void* callback, void* cookie, const void* method) {
    if (GetCurrentThreadId() == g_game_thread.load(std::memory_order_relaxed) &&
        entity == g_session.entity && !callback && !cookie && callback_type == g_audio_none && DeferFlyingStop(hash))
        return 0; // No playing instance or callback was requested for this deferred stop.
    return g_audio_entity(entity, hash, callback_type, callback, cookie, method);
}
void __fastcall PerformClearDetour(void* handle, bool releasing, const void* method) {
    if (handle == g_session.handle && g_session.component) {
        if (g_session.config->diagnostics) {
            void* stack[12]{};
            USHORT count = CaptureStackBackTrace(0, 12, stack, nullptr);
            auto base = reinterpret_cast<uintptr_t>(GetModuleHandleW(L"GameAssembly.dll"));
            char text[384] = "Aglina Clear stack (GameAssembly-relative):";
            size_t used = std::strlen(text);
            for (USHORT i = 0; i < count && used < sizeof(text) - 24; ++i) {
                auto address = reinterpret_cast<uintptr_t>(stack[i]);
                if (address >= base && address - base < 0x20000000)
                    used += std::snprintf(text + used, sizeof(text) - used, " %llx", static_cast<unsigned long long>(address - base));
            }
            Log(text);
        }
        Cancel(releasing ? "perform released" : "perform cleared", false, !releasing);
    }
    g_perform_clear(handle, releasing, method);
}
void __fastcall CommandDetour(void* handle, int reason, int type, bool weak, const void* method) {
    if (handle == g_session.handle && g_session.component && g_session.config->diagnostics) {
        char text[144];
        std::snprintf(text, sizeof(text), "Aglina Perform interrupt command: reason=%d, type=%d, weak=%d", reason, type, weak);
        Log(text);
    }
    if (handle == g_session.handle && reason == g_sp_dash_end && type == g_force_exit &&
        ShouldHold(g_session.component)) return;
    if (handle == g_session.handle && !(reason == g_sp_dash_end && type == g_force_exit))
        Cancel("explicit perform command", false);
    // Explicit interrupt commands always keep their original behavior.
    g_add_command(handle, reason, type, weak, method);
}
void __fastcall StartDetour(void* component, int index, const void* method) {
    g_game_thread.store(GetCurrentThreadId(), std::memory_order_relaxed);
    g_start(component, index, method);
    auto config = g_config.load();
    if (g_stopping || !config->enabled || !component || index < 0) return;
    if (!CanMove(component, true)) return;
    bool ok = true;
    // This class is already initialized by the original StartSpDash. Read the
    // game's real state identifiers, not names guessed from documentation.
    for (int side = 0; side < 2; ++side)
        g_hashes[side] = Unbox<int>(g_host->field_get_value_object(g_host->context, g_hash_fields[side], nullptr), ok);
    for (int i = 0; i < 2; ++i)
        g_locomotion_hashes[i] = Unbox<int>(g_host->field_get_value_object(g_host->context, g_locomotion_fields[i], nullptr), ok);
    if (!ok || !g_hashes[0] || !g_hashes[1] || g_hashes[0] == g_hashes[1]) return;
    void* blackboard = Object(GetBlackboard, component, ok);
    if (!ok) return;
    void* handle = OwnedPerform(component);
    if (!handle) return;
    // Repeated calls for the same perform are not a fresh animation session.
    if (g_session.component == component && g_session.handle == handle) return;
    Cancel("new special dash", true);
    g_session.component = component;
    g_session.handle = handle;
    g_session.config = config;
    g_session.blackboard = blackboard;
    g_session.entity = Object(GetEntity, component, ok);
    if (!ok) { Cancel("entity unavailable", false); return; }
    g_session.root = g_host->gchandle_new(g_host->context, component, 1);
    g_session.handle_root = g_host->gchandle_new(g_host->context, handle, 1);
    if (!g_session.root || !g_session.handle_root) Cancel("root failed", false);
    else {
        if (config->external_loop) {
            BeginPoseOverlay(component);
        }
        if (!g_session.external_expected && g_external_owner.component == component) {
            Cancel("external controller rollback pending", true); return;
        }
        if (config->diagnostics) {
            char text[160];
            std::snprintf(text, sizeof(text), "Aglina native dash armed: left_hash=%d, right_hash=%d", g_hashes[0], g_hashes[1]);
            Log(text);
        }
    }
}
void __fastcall PreLateDetour(void* component, float delta, const void* method) {
    ExternalRestoreWhenIdle(component);
    if (g_session.component) {
        if (g_stopping || g_config.load() != g_session.config)
            Cancel("configuration changed", true);
        else if (!CanMove(g_session.component, false))
            Cancel("movement or owner ended", true);
        else if (g_session.component == component) {
            void* observed_handle = g_session.handle;
            Frame frame;
            frame.delta = delta;
            frame.allowed = CurrentRequestHandle(component) == g_session.handle;
            int hash = 0;
            if (!ReadFrame(component, frame, hash)) frame.allowed = false;
            if (g_session.external_expected) {
                if (!ExternalControllerOwned(component)) frame.allowed = false;
                else if (!g_session.external_confirmed && frame.current_special && !frame.transitioning) {
                    if (ExternalClipPlaying(component, hash)) {
                        g_session.external_confirmed = true;
                        Log("Aglina v11: external return clip confirmed active; using baked closed-loop window.");
                    } else if ((g_session.external_wait += delta) > .35f) {
                        RestoreExternal(true);
                        g_session.external_expected = false;
                        if (g_external_owner.component == component) frame.allowed = false;
                        else Log("Aglina v11: imported clip not observed; original controller restored, using v9 loop.");
                    }
                }
            }
            if (g_session.last_special_hash && frame.current_special && hash != g_session.last_special_hash)
                frame.allowed = false; // The native selected side is fixed for this session.
            auto loop_action = g_session.external_confirmed ?
                g_session.imported_loop.Update(g_session.policy.active, frame) :
                g_session.loop.Update(g_session.policy.active, frame);
            if (loop_action == LoopAction::Abort) {
                if (g_session.config->diagnostics) {
                    char detail[192];
                    std::snprintf(detail, sizeof(detail), "Aglina %s loop rejected: current=%.3f next=%.3f transition=%d expected_target=%.3f wait=%.3f",
                        g_session.external_confirmed ? "v11 imported" : "v9",
                        frame.time, frame.next_time, frame.transitioning,
                        g_session.external_confirmed ? ImportedLoopSchedule::TargetAt(g_session.imported_loop.submitted_time) : LoopSchedule::TargetAt(g_session.loop.submitted_time),
                        g_session.external_confirmed ? g_session.imported_loop.waiting : g_session.loop.waiting);
                    Log(detail);
                }
                Cancel("loop interrupted, late, or not acknowledged", true);
            } else if (loop_action == LoopAction::Blend) {
                bool ok = true;
                int layer = 0;
                float duration = g_session.external_confirmed ? 0.0f : kLoopBlend;
                float offset = g_session.external_confirmed ? ImportedLoopSchedule::TargetAt(frame.time) : LoopSchedule::TargetAt(frame.time);
                float transition_time = 0;
                bool blend_root_motion = false;
                void* animator = Object(GetAnimator, component, ok);
                void* args[]{&hash, &duration, &layer, &offset, &transition_time, &g_blend_style, &g_blend_interrupt, &blend_root_motion};
                void* expected_handle = g_session.handle;
                if (g_session.external_confirmed) g_session.imported_loop.Submitted(frame.time);
                else g_session.loop.Submitted(frame.time);
                Invoke(CrossFade, animator, args, ok);
                if (g_session.component != component || g_session.handle != expected_handle) {
                    // Managed playback may synchronously deliver a native exit.
                } else if (ok) {
                    ++g_session.animation_replays;
                    if (g_session.config->diagnostics && (g_session.animation_replays <= 4 || g_session.animation_replays % 16 == 0)) {
                        char text[240];
                        std::snprintf(text, sizeof(text), "Aglina %s: hash=%d, source=%.3f, target=%.3f, duration=%.3f, count=%u",
                            g_session.external_confirmed ? "v11 imported loop wrap" : "v9 normalized bob blend", hash, frame.time, offset, duration, g_session.animation_replays);
                        Log(text);
                    }
                    // Observe the actual transition/rewind on following ticks;
                    // never fabricate a successful seek or reset frame.time.
                } else Cancel("loop blend invocation failed", true);
            }
            if (g_session.component && g_session.handle != observed_handle) {
                g_pre_late(component, delta, method);
                return; // A synchronous callback started a new session; discard the old frame.
            }
            Decision decision = g_session.component ? g_session.policy.Update(frame) : Decision::Wait;
            if (decision == Decision::Keep) MaintainEffects();
            if (decision == Decision::Cancel) {
                if (g_session.config->diagnostics) {
                    char text[280];
                    std::snprintf(text, sizeof(text),
                        "Aglina exit: hash=%d, next=%d, time=%.3f, length=%.3f, clip_loop=%d, perform=%d, special=%d, entering=%d, outgoing=%d, pending=%.3f",
                        hash, frame.next_hash, frame.time, frame.length, frame.clip_loop, frame.allowed, frame.current_special, frame.entering_special,
                        frame.outgoing, g_session.policy.pending_time);
                    Log(text);
                }
                Cancel("native state or perform ended", true);
            } else if (g_session.component && frame.current_special && hash != g_session.last_special_hash) {
                if (g_session.last_special_hash) ++g_session.side_changes;
                g_session.last_special_hash = hash;
                if (g_session.config->diagnostics && (g_session.side_changes < 5 || g_session.side_changes % 16 == 0)) {
                    char text[160];
                    std::snprintf(text, sizeof(text), "Aglina selected state=%s, time=%.3f, length=%.3f, clip_loop=%d",
                        hash == g_hashes[0] ? "left" : "right", frame.time, frame.length, frame.clip_loop);
                    Log(text);
                }
            }
            if (g_session.component && frame.current_special && std::isfinite(frame.time)) {
                int bucket = static_cast<int>(std::min(100.0f, std::max(0.0f, frame.time)) * 10.0f);
                if (bucket != g_session.last_progress_bucket) {
                    g_session.last_progress_bucket = bucket;
                    if (g_session.config->diagnostics) {
                        char text[224];
                        std::snprintf(text, sizeof(text), "Aglina animation progress: time=%.3f, next=%.3f, transition=%d, length=%.3f, clip_loop=%d, deferred=%u",
                            frame.time, frame.next_time, frame.transitioning, frame.length, frame.clip_loop, g_session.natural_end_deferrals);
                        Log(text);
                    }
                }
            }
        }
    }
    // Run the game's tick after any targeted same-side blend request.
    g_pre_late(component, delta, method);
}
bool __fastcall CheckDetour(void* component, const void* method) {
    // Always retain the original result for every other role and perform.
    bool result = g_check(component, method);
    return result && !ShouldHold(component);
}
void __fastcall InterruptDetour(void* component, const void* method) {
    if (component == g_session.component && HoldContext(component)) {
        ++g_session.spdash_interrupts_suppressed;
        if (g_session.config->diagnostics && g_session.spdash_interrupts_suppressed == 1)
            Log("Aglina: suppressed InterruptSpDashPerform while the selected special state is active (v7).");
        return;
    }
    if (component == g_session.component) Cancel("native dash interruption", false);
    g_interrupt(component, method);
}
void __fastcall ForceDetour(void* component, const void* method) {
    if (component == g_session.component) Cancel("force stop", false);
    g_force(component, method);
}
void __fastcall ForceCurrentDetour(void* component, bool stop_animation, const void* method) {
    if (component == g_session.component) Cancel("force current perform", false);
    g_force_current(component, stop_animation, method);
}
void __fastcall ReleaseDetour(void* component, bool full, const void* method) {
    if (component == g_session.component) Cancel("component release", false, false);
    if (component == g_external_owner.component) RestoreExternal(true);
    if (component == g_pose_owner.component) ReleasePoseOverlay(true);
    g_release(component, full, method);
}

std::string_view Trim(std::string_view s) {
    constexpr char whitespace[] = " \t\r\n";
    auto first = s.find_first_not_of(whitespace);
    if (first == s.npos) return {};
    return s.substr(first, s.find_last_not_of(whitespace) - first + 1);
}
bool ParseBool(std::string_view s, bool& out) {
    if (s == "true" || s == "1") { out = true; return true; }
    if (s == "false" || s == "0") { out = false; return true; }
    return false;
}
BE_Result BE_CALL Configure(const char* raw) {
    auto config = std::make_shared<Configuration>();
    std::string_view remaining = raw ? raw : "";
    bool valid = true;
    bool schema_present = false;
    while (!remaining.empty()) {
        auto end = remaining.find('\n');
        auto line = Trim(remaining.substr(0, end));
        remaining = end == remaining.npos ? std::string_view{} : remaining.substr(end + 1);
        if (line.empty() || line.front() == '[' || line.front() == ';' || line.front() == '#') continue;
        auto equals = line.find('=');
        if (equals == line.npos) { valid = false; break; }
        auto key = Trim(line.substr(0, equals));
        auto value = Trim(line.substr(equals + 1));
        if (key == "schema_version") { schema_present = true; valid &= value == "1" || value == "2"; }
        else if (key == "enabled") valid &= ParseBool(value, config->enabled);
        else if (key == "diagnostics") valid &= ParseBool(value, config->diagnostics);
        else if (key == "external_loop") valid &= ParseBool(value, config->external_loop);
    }
    if (!valid || (config->enabled && !schema_present)) {
        // A bad live edit must never leave the old hold enabled indefinitely.
        config->enabled = false;
        Log("Invalid action configuration; special dash disabled.");
    }
    if (*g_config.load() == *config) return BE_Result_Ok;
    g_config.store(config);
    if (config->enabled && config->diagnostics) {
        char text[160];
        std::snprintf(text, sizeof(text), "Aglina action constants: SpDashEnd=%d, ForceExit=%d, DashMode=%d, GroundedMode=%d, SprintGait=%d",
            g_sp_dash_end, g_force_exit, g_dash_mode, g_grounded_mode, g_sprint);
        Log(text);
    }
    Log(!config->enabled ? "Actions disabled." : config->external_loop ?
        "Actions enabled: Aglina v12 bone-pose overlay; native movement/Animator retained, no external clip load." :
        "Actions enabled: Aglina v9 loop; external pose overlay disabled.");
    return BE_Result_Ok;
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->resolve_method || !host->resolve_field || !host->resolve_class || !host->runtime_invoke ||
        !host->object_unbox || !host->field_get_value_object ||
        !host->copy_managed_string || !host->gchandle_new || !host->gchandle_free ||
        !host->create_hook || !host->release_module_hooks) return BE_Result_InvalidArgument;
    g_host = host;
    for (auto& method : g_methods) {
        if (host->resolve_method(host->context, &method.desc, &method.resolved) != BE_Result_Ok) {
            Log((std::string("Missing action method: ") + method.desc.class_name + "." + method.desc.method_name).c_str());
            if (&method >= &g_methods[PoseTail]) { g_pose_contract = false; continue; }
            if (&method >= &g_methods[BundleLoad]) { g_external_contract = false; continue; }
            return BE_Result_ContractMismatch;
        }
    }
    for (auto& field : g_fields) {
        if (host->resolve_field(host->context, &field.desc, &field.resolved) != BE_Result_Ok) {
            Log((std::string("Missing action field: ") + field.desc.field_name).c_str());
            return BE_Result_ContractMismatch;
        }
    }
    // Protect against the easy-to-miss static ShouldInterruptSpDash ABI.
    using FlagsFn = uint32_t(*)(const void*, uint32_t*);
    auto flags = reinterpret_cast<FlagsFn>(GetProcAddress(GetModuleHandleW(L"GameAssembly.dll"), "il2cpp_method_get_flags"));
    if (!flags) return BE_Result_ContractMismatch;
    for (int i = Start; i <= Release; ++i) {
        uint32_t impl = 0;
        bool is_static = (flags(g_methods[i].resolved.method_info, &impl) & 0x10u) != 0;
        if (is_static != (i == ShouldInterrupt)) {
            Log("Action hook static/instance contract mismatch.");
            return BE_Result_ContractMismatch;
        }
    }
    bool ok = true;
    // Host resolve_field deliberately requires an instance offset. Enum literals
    // may have no such offset: get this constant through metadata, never a raw
    // offset or a hard-coded numeric Sprint value.
    using FindFieldFn = void*(*)(const void*, const char*);
    auto find_field = reinterpret_cast<FindFieldFn>(GetProcAddress(GetModuleHandleW(L"GameAssembly.dll"), "il2cpp_class_get_field_from_name"));
    if (!find_field) return BE_Result_ContractMismatch;
    auto static_field = [&](const char* ns, const char* cls, const char* name) -> const void* {
        BE_ResolvedClassV1 resolved{};
        if (host->resolve_class(host->context, kGame, ns, cls, &resolved) != BE_Result_Ok) return nullptr;
        return find_field(resolved.class_info, name);
    };
    auto constant = [&](const char* ns, const char* cls, const char* name) {
        const void* field = static_field(ns, cls, name);
        if (!field) { ok = false; return -1; }
        return Unbox<int>(host->field_get_value_object(host->context, field, nullptr), ok);
    };
    g_sprint = constant(kCore, "MovementComponent.GroundedMoveGait", "Sprint");
    g_dash_mode = constant(kCore, "MovementComponent.MoveMode", "Dash");
    g_grounded_mode = constant(kCore, "MovementComponent.MoveMode", "Grounded");
    g_perform_end = constant("Beyond.Gameplay", "CharInteractPerformEnums.CharInteractPerformState", "End");
    g_sp_dash_end = constant("Beyond.Gameplay", "CharInteractPerformEnums.InterruptReason", "SpDashEnd");
    g_force_exit = constant("Beyond.Gameplay", "CharInteractPerformEnums.InterruptType", "ForceExit");
    auto other_constant = [&](const char* assembly, const char* ns, const char* cls, const char* name) {
        BE_ResolvedClassV1 resolved{};
        if (host->resolve_class(host->context, assembly, ns, cls, &resolved) != BE_Result_Ok) { ok = false; return -1; }
        auto field = find_field(resolved.class_info, name);
        if (!field) { ok = false; return -1; }
        return Unbox<int>(host->field_get_value_object(host->context, field, nullptr), ok);
    };
    g_blend_style = other_constant(kUnity, "UnityEngine", "AnimationBlendStyle", "HermiteCubic");
    g_blend_interrupt = other_constant(kUnity, "UnityEngine", "AnimatorTransitionInterruptionSource", "CurrentThenNext");
    g_audio_none = other_constant("Audio.Beyond.dll", "Beyond.Audio", "AudioCallbackType", "None");
    g_particle_stop_clear = other_constant(kParticles, "UnityEngine", "ParticleSystemStopBehavior", "StopEmittingAndClear");
    using NewStringFn = void*(*)(const char*);
    auto new_string = reinterpret_cast<NewStringFn>(GetProcAddress(GetModuleHandleW(L"GameAssembly.dll"), "il2cpp_string_new"));
    if (!new_string) return BE_Result_ContractMismatch;
    void* stop_name = new_string("Aglina_SprintDashSp_Flying_Stop");
    void* hash_args[]{stop_name};
    if (!stop_name) return BE_Result_Failed;
    g_flying_stop_hash = Unbox<uint32_t>(Invoke(AudioHash, nullptr, hash_args, ok), ok);
    if (!g_flying_stop_hash) return BE_Result_ContractMismatch;
    g_hash_fields[0] = static_field(kView, kCharacterComp, "HASH_SP_DASH_L");
    g_hash_fields[1] = static_field(kView, kCharacterComp, "HASH_SP_DASH_R");
    g_locomotion_fields[0] = static_field(kView, "CharacterAnimationBlackboard", "HASH_STATE_RUN");
    g_locomotion_fields[1] = static_field(kView, "CharacterAnimationBlackboard", "HASH_STATE_SPRINT");
    uint32_t impl = 0;
    if (!ok || !g_hash_fields[0] || !g_hash_fields[1] || !g_locomotion_fields[0] || !g_locomotion_fields[1] ||
        (flags(g_methods[SetDashing].resolved.method_info, &impl) & 0x10u)) return BE_Result_ContractMismatch;
    for (MethodId id : {FlowTick, PerformClear, AddCommand, TickStateInterrupt, CrossFade, TryExit,
        CheckTrackEnd, ShowObject, AudioMonoPost, AudioMonoComponent, EffectDuration, EffectFinish, EffectStop})
        if (flags(g_methods[id].resolved.method_info, &impl) & 0x10u) return BE_Result_ContractMismatch;
    for (MethodId id : {AudioEntityPost, AudioHash, UnityAlive})
        if (!(flags(g_methods[id].resolved.method_info, &impl) & 0x10u)) return BE_Result_ContractMismatch;
    auto game_module = GetModuleHandleW(L"GameAssembly.dll");
    g_object_class = reinterpret_cast<ObjectClassFn>(GetProcAddress(game_module, "il2cpp_object_get_class"));
    g_class_method = reinterpret_cast<ClassMethodFn>(GetProcAddress(game_module, "il2cpp_class_get_method_from_name"));
    g_class_parent = reinterpret_cast<ClassParentFn>(GetProcAddress(game_module, "il2cpp_class_get_parent"));
    using ClassTypeFn = const void*(*)(void*);
    using TypeObjectFn = void*(*)(const void*);
    auto class_type = reinterpret_cast<ClassTypeFn>(GetProcAddress(game_module, "il2cpp_class_get_type"));
    auto type_object = reinterpret_cast<TypeObjectFn>(GetProcAddress(game_module, "il2cpp_type_get_object"));
    BE_ResolvedClassV1 particle_class{};
    if (!g_object_class || !g_class_method || !g_class_parent || !class_type || !type_object ||
        host->resolve_class(host->context, kParticles, "UnityEngine", "ParticleSystem", &particle_class) != BE_Result_Ok)
        return BE_Result_ContractMismatch;
    g_particle_type = type_object(class_type(const_cast<void*>(particle_class.class_info)));
    if (!g_particle_type) return BE_Result_ContractMismatch;
    g_particle_type_root = host->gchandle_new(host->context, g_particle_type, 1);
    if (!g_particle_type_root) return BE_Result_Failed;
    struct Hook { MethodId id; void* detour; void** original; };
    Hook hooks[]{
        {TickStateInterrupt, reinterpret_cast<void*>(&StateInterruptDetour), reinterpret_cast<void**>(&g_tick_state_interrupt)},
        {TryExit, reinterpret_cast<void*>(&TryExitDetour), reinterpret_cast<void**>(&g_try_exit)},
        {FlowTick, reinterpret_cast<void*>(&FlowDetour), reinterpret_cast<void**>(&g_flow_tick)},
        {CheckTrackEnd, reinterpret_cast<void*>(&TrackEndDetour), reinterpret_cast<void**>(&g_check_track)},
        {ShowObject, reinterpret_cast<void*>(&ShowObjectDetour), reinterpret_cast<void**>(&g_show)},
        {AudioMonoPost, reinterpret_cast<void*>(&AudioMonoDetour), reinterpret_cast<void**>(&g_audio_mono)},
        {AudioEntityPost, reinterpret_cast<void*>(&AudioEntityDetour), reinterpret_cast<void**>(&g_audio_entity)},
        {EffectDuration, reinterpret_cast<void*>(&EffectDurationDetour), reinterpret_cast<void**>(&g_effect_duration)},
        {EffectFinish, reinterpret_cast<void*>(&EffectFinishDetour), reinterpret_cast<void**>(&g_effect_finish)},
        {EffectStop, reinterpret_cast<void*>(&EffectStopDetour), reinterpret_cast<void**>(&g_effect_stop)},
        {PerformClear, reinterpret_cast<void*>(&PerformClearDetour), reinterpret_cast<void**>(&g_perform_clear)},
        {AddCommand, reinterpret_cast<void*>(&CommandDetour), reinterpret_cast<void**>(&g_add_command)},
        {SetDashing, reinterpret_cast<void*>(&DashingDetour), reinterpret_cast<void**>(&g_set_dashing)},
        {Start, reinterpret_cast<void*>(&StartDetour), reinterpret_cast<void**>(&g_start)},
        {PreLate, reinterpret_cast<void*>(&PreLateDetour), reinterpret_cast<void**>(&g_pre_late)},
        {ShouldInterrupt, reinterpret_cast<void*>(&CheckDetour), reinterpret_cast<void**>(&g_check)},
        {Interrupt, reinterpret_cast<void*>(&InterruptDetour), reinterpret_cast<void**>(&g_interrupt)},
        {ForceStop, reinterpret_cast<void*>(&ForceDetour), reinterpret_cast<void**>(&g_force)},
        {ForceCurrent, reinterpret_cast<void*>(&ForceCurrentDetour), reinterpret_cast<void**>(&g_force_current)},
        {Release, reinterpret_cast<void*>(&ReleaseDetour), reinterpret_cast<void**>(&g_release)},
    };
    for (const auto& hook : hooks) {
        if (host->create_hook(host->context, kId, g_methods[hook.id].resolved.method_pointer,
                hook.detour, hook.original) != BE_Result_Ok) {
            host->release_module_hooks(host->context, kId);
            host->gchandle_free(host->context, g_particle_type_root);
            g_particle_type_root = 0;
            g_particle_type = nullptr;
            Log("Action hooks rolled back after installation failure.");
            return BE_Result_Failed;
        }
    }
    if (g_pose_contract && host->create_hook(host->context, kId, g_methods[PoseTail].resolved.method_pointer,
            reinterpret_cast<void*>(&PoseTailDetour), reinterpret_cast<void**>(&g_pose_tail)) != BE_Result_Ok) {
        g_pose_contract = false;
        Log("Aglina v12: TailLate hook unavailable; pose overlay disabled, native v9 hold remains.");
    }
    Log("Actions module ready; scoped to Aglina special dash, disabled by default.");
    return BE_Result_Ok;
}
void BE_CALL Shutdown() {
    g_stopping = true;
    if (!g_host) return;
    g_host->release_module_hooks(g_host->context, kId);
    // Host unload is during shutdown. Do not invoke Unity on this worker thread.
    Cancel("module shutdown", false, false);
    RestoreExternal(false);
    FreeExternalAssets(false);
    ReleasePoseOverlay(false);
    if (g_particle_type_root) g_host->gchandle_free(g_host->context, g_particle_type_root);
    g_particle_type_root = 0;
    g_particle_type = nullptr;
}
const BE_ModuleApiV1 kApi{{kId, "Character Actions", "1.11.0", BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize, &Configure, &Shutdown};
} // namespace
} // namespace BetterEndfield::Actions

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::Actions::kApi;
}
