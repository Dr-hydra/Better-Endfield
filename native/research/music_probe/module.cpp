#include <BetterEndfield/ModuleApi.h>

#include <Windows.h>
#include <intrin.h>

#include <algorithm>
#include <array>
#include <atomic>
#include <cmath>
#include <cstdint>
#include <cstdio>
#include <cstring>
#include <string>
#include <string_view>
#include <vector>

namespace {

constexpr const char* kModuleId = "betterendfield.music-probe";
constexpr const char* kGameplayAssembly = "Gameplay.Beyond.dll";
constexpr const char* kMusicNamespace = "Beyond.Gameplay.Audio";
constexpr const char* kMusicClass = "AudioMusicSystem";
constexpr const char* kWwiseAssembly = "AK.Wwise.Unity.API.dll";
constexpr const char* kAudioInputClass = "AkAudioInputManager";
constexpr const char* kWwisePInvokeClass = "AkSoundEnginePINVOKE";
constexpr const char* kUnityCoreAssembly = "UnityEngine.CoreModule.dll";

constexpr uint32_t kProbeBankId = 3704265910u;
constexpr uint32_t kProbeEventId = 95937692u;
constexpr uint32_t kMonoSpeakerMask = 0x4u;
constexpr int32_t kSourcePluginType = 2;
constexpr uint32_t kAudiokineticCompanyId = 0u;
constexpr uint32_t kAudioInputPluginId = 200u;
constexpr uint32_t kMonitorErrorLevelAll = 0xFFFFFFFFu;
constexpr uint32_t kProbeDurationSeconds = 8u;
constexpr double kProbeFrequencyHz = 440.0;
constexpr float kProbeAmplitude = 0.25f;
constexpr double kPi = 3.14159265358979323846;

constexpr std::array<uint32_t, 10> kMusicStateGroups{
    0xE414D158u, 0xB3D78A5Du, 0x4D9E8C28u, 0x2560A0EEu, 0x3B650E3Du,
    0xA4C62908u, 0x75C98B29u, 0x6401EC38u, 0x654423EEu, 0xC52AA6BCu,
};

const BE_HostApiV1* g_host = nullptr;
std::atomic<uint64_t> g_sequence{0};
std::array<std::atomic<uint32_t>, 64> g_tracked_playing_ids{};
std::atomic<uint32_t> g_tracked_cursor{0};
bool g_hooks_installed = false;

enum class ProbeState : uint32_t {
    Idle,
    Starting,
    Playing,
    Finished,
    Failed,
};

std::atomic<ProbeState> g_probe_state{ProbeState::Idle};
std::atomic<uint32_t> g_probe_playing_id{0};
std::atomic<uint32_t> g_probe_sample_rate{48000};
std::atomic<uint64_t> g_probe_frames{0};
std::atomic<uint64_t> g_probe_sample_callbacks{0};
std::atomic<bool> g_probe_format_seen{false};
std::atomic<bool> g_managed_callbacks_ready{false};
std::atomic<uint32_t> g_try_initialize_calls{0};
void* g_probe_channel_config = nullptr;
uint32_t g_probe_sample_type = 0;
uint32_t g_probe_interleave = 0;
double g_probe_phase = 0.0;
BE_ResolvedClassV1 g_probe_game_object_class{};
void* g_probe_game_object = nullptr;
uint32_t g_probe_game_object_handle = 0;

struct RuntimeMethod {
    const char* key = nullptr;
    BE_MethodDescriptorV1 descriptor{};
    BE_ResolvedMethodV1 resolved{};
};

struct RuntimeField {
    const char* key = nullptr;
    BE_FieldDescriptorV1 descriptor{};
    int32_t offset = -1;
};

struct MusicSnapshot {
    uint32_t login = 0;
    uint32_t meta = 0;
    uint32_t gameplay = 0;
    bool paused = false;
};

void Log(const std::string& message) {
    if (g_host && g_host->log) {
        g_host->log(g_host->context, kModuleId, message.c_str());
    }
}

const char* ResultName(BE_Result result) {
    switch (result) {
    case BE_Result_Ok: return "ok";
    case BE_Result_InvalidArgument: return "invalid-argument";
    case BE_Result_NotReady: return "not-ready";
    case BE_Result_NotFound: return "not-found";
    case BE_Result_ContractMismatch: return "contract-mismatch";
    case BE_Result_Conflict: return "conflict";
    case BE_Result_Failed: return "failed";
    default: return "unknown";
    }
}

std::string ManagedString(void* value) {
    if (!value || !g_host || !g_host->copy_managed_string) {
        return value ? "<unavailable>" : "<null>";
    }
    std::array<char, 1024> buffer{};
    const int copied = g_host->copy_managed_string(
        g_host->context, value, buffer.data(), buffer.size());
    return copied >= 0 ? std::string(buffer.data()) : "<invalid>";
}

uintptr_t ExecutableImageEnd(uintptr_t image) {
    if (!image) {
        return 0;
    }
    __try {
        const auto* dos = reinterpret_cast<const IMAGE_DOS_HEADER*>(image);
        const auto* nt = reinterpret_cast<const IMAGE_NT_HEADERS*>(
            image + static_cast<uintptr_t>(dos->e_lfanew));
        if (dos->e_magic == IMAGE_DOS_SIGNATURE &&
            nt->Signature == IMAGE_NT_SIGNATURE) {
            return image + nt->OptionalHeader.SizeOfImage;
        }
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
    }
    return image;
}

std::string CallerText() {
    const auto game_assembly = reinterpret_cast<uintptr_t>(
        GetModuleHandleW(L"GameAssembly.dll"));
    const uintptr_t game_assembly_end = ExecutableImageEnd(game_assembly);
    std::array<void*, 16> frames{};
    const USHORT frame_count = CaptureStackBackTrace(
        1, static_cast<DWORD>(frames.size()), frames.data(), nullptr);
    uintptr_t caller = 0;
    for (USHORT index = 0; index < frame_count; ++index) {
        const uintptr_t candidate = reinterpret_cast<uintptr_t>(frames[index]);
        if (candidate >= game_assembly && candidate < game_assembly_end) {
            caller = candidate;
            break;
        }
    }
    char buffer[96]{};
    if (caller) {
        std::snprintf(buffer, sizeof(buffer), "GameAssembly+0x%llX",
            static_cast<unsigned long long>(caller - game_assembly));
    } else {
        std::snprintf(buffer, sizeof(buffer), "unresolved");
    }
    return buffer;
}

template <typename T>
T ReadInstanceField(void* instance, int32_t offset, T fallback = {}) {
    if (!instance || offset < 0) {
        return fallback;
    }
    __try {
        return *reinterpret_cast<T*>(
            static_cast<uint8_t*>(instance) + static_cast<size_t>(offset));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return fallback;
    }
}

RuntimeField g_login_id_field{
    "loginPlayingId",
    {kGameplayAssembly, kMusicNamespace, kMusicClass,
        "m_loginMusicPlayingId", "System.UInt32"}};
RuntimeField g_meta_id_field{
    "metaPlayingId",
    {kGameplayAssembly, kMusicNamespace, kMusicClass,
        "m_metaMusicPlayingId", "System.UInt32"}};
RuntimeField g_gameplay_id_field{
    "gameplayPlayingId",
    {kGameplayAssembly, kMusicNamespace, kMusicClass,
        "m_gameplayMusicPlayingId", "System.UInt32"}};
RuntimeField g_paused_field{
    "isMusicPaused",
    {kGameplayAssembly, kMusicNamespace, kMusicClass,
        "m_isMusicPaused", "System.Boolean"}};

MusicSnapshot Snapshot(void* instance) {
    MusicSnapshot result;
    result.login = ReadInstanceField<uint32_t>(instance, g_login_id_field.offset);
    result.meta = ReadInstanceField<uint32_t>(instance, g_meta_id_field.offset);
    result.gameplay = ReadInstanceField<uint32_t>(
        instance, g_gameplay_id_field.offset);
    result.paused = ReadInstanceField<uint8_t>(instance, g_paused_field.offset) != 0;
    return result;
}

void TrackPlayingId(uint32_t playing_id) {
    if (playing_id == 0) {
        return;
    }
    const uint32_t cursor = g_tracked_cursor.fetch_add(1);
    g_tracked_playing_ids[cursor % g_tracked_playing_ids.size()].store(playing_id);
}

void TrackSnapshot(const MusicSnapshot& snapshot) {
    TrackPlayingId(snapshot.login);
    TrackPlayingId(snapshot.meta);
    TrackPlayingId(snapshot.gameplay);
}

bool IsTrackedPlayingId(uint32_t playing_id) {
    if (playing_id == 0) {
        return false;
    }
    for (const auto& candidate : g_tracked_playing_ids) {
        if (candidate.load() == playing_id) {
            return true;
        }
    }
    return false;
}

std::string SnapshotText(const MusicSnapshot& snapshot) {
    char buffer[160]{};
    std::snprintf(buffer, sizeof(buffer),
        "login=%u meta=%u gameplay=%u paused=%s",
        snapshot.login, snapshot.meta, snapshot.gameplay,
        snapshot.paused ? "true" : "false");
    return buffer;
}

void LogEvent(std::string_view source, std::string_view phase,
    const std::string& data) {
    char prefix[160]{};
    std::snprintf(prefix, sizeof(prefix),
        "[music-life] seq=%llu tid=%lu source=%.*s phase=%.*s caller=%s ",
        static_cast<unsigned long long>(g_sequence.fetch_add(1) + 1),
        static_cast<unsigned long>(GetCurrentThreadId()),
        static_cast<int>(source.size()), source.data(),
        static_cast<int>(phase.size()), phase.data(), CallerText().c_str());
    Log(std::string(prefix) + data);
}

bool IsMusicStateGroup(uint32_t group) {
    for (const uint32_t candidate : kMusicStateGroups) {
        if (candidate == group) {
            return true;
        }
    }
    return false;
}

const char* StateGroupName(uint32_t group) {
    switch (group) {
    case 0xE414D158u: return "Music";
    case 0xB3D78A5Du: return "MusicMap";
    case 0x4D9E8C28u: return "Battle";
    case 0x2560A0EEu: return "BattleIntensity";
    case 0x3B650E3Du: return "Mission";
    case 0xA4C62908u: return "Dialog";
    case 0x75C98B29u: return "Cutscene";
    case 0x6401EC38u: return "Login";
    case 0x654423EEu: return "Meta";
    case 0xC52AA6BCu: return "RemoteComm";
    default: return "Other";
    }
}

const char* ActionName(int32_t action) {
    switch (action) {
    case 0: return "Stop";
    case 1: return "Pause";
    case 2: return "Resume";
    case 3: return "Break";
    case 4: return "ReleaseEnvelope";
    default: return "Unknown";
    }
}

bool Resolve(RuntimeMethod& method, bool required = true) {
    const BE_Result status = g_host->resolve_method(
        g_host->context, &method.descriptor, &method.resolved);
    if (status == BE_Result_Ok && method.resolved.method_pointer) {
        Log(std::string("[music-contract] resolved method=") + method.key);
        return true;
    }
    Log(std::string("[music-contract] ") + (required ? "required" : "optional") +
        " method unavailable=" + method.key + " result=" + ResultName(status));
    return false;
}

bool Resolve(RuntimeField& field) {
    BE_ResolvedFieldV1 resolved{};
    const BE_Result status = g_host->resolve_field(
        g_host->context, &field.descriptor, &resolved);
    if (status == BE_Result_Ok && resolved.offset >= 0) {
        field.offset = resolved.offset;
        Log(std::string("[music-contract] resolved field=") + field.key +
            " offset=" + std::to_string(field.offset));
        return true;
    }
    Log(std::string("[music-contract] field unavailable=") + field.key +
        " result=" + ResultName(status));
    return false;
}

bool Hook(RuntimeMethod& method, void* detour, void** original,
    bool required = true) {
    if (!Resolve(method, required)) {
        return !required;
    }
    const BE_Result status = g_host->create_hook(g_host->context, kModuleId,
        method.resolved.method_pointer, detour, original);
    if (status == BE_Result_Ok) {
        Log(std::string("[music-hook] installed method=") + method.key);
        return true;
    }
    Log(std::string("[music-hook] ") + (required ? "required" : "optional") +
        " install failed method=" + method.key + " result=" + ResultName(status));
    return !required;
}

#define MUSIC_METHOD(variable, key_name, method_name, parameters, return_type, count) \
    RuntimeMethod variable{key_name, {kGameplayAssembly, kMusicNamespace, \
        kMusicClass, method_name, parameters, return_type, count}}

#define WWISE_METHOD(variable, key_name, method_name, parameters, return_type, count) \
    RuntimeMethod variable{key_name, {kWwiseAssembly, "", "AkSoundEngine", \
        method_name, parameters, return_type, count}}

#define AUDIO_INPUT_METHOD(variable, key_name, method_name, parameters, return_type, count) \
    RuntimeMethod variable{key_name, {kWwiseAssembly, "", kAudioInputClass, \
        method_name, parameters, return_type, count}}

#define WWISE_PINVOKE_METHOD(variable, key_name, method_name, parameters, return_type, count) \
    RuntimeMethod variable{key_name, {kWwiseAssembly, "", kWwisePInvokeClass, \
        method_name, parameters, return_type, count}}

MUSIC_METHOD(g_post_music_event, "AudioMusicSystem.PostMusicEvent",
    "PostMusicEvent",
    "System.String|Beyond.Gameplay.Audio.AudioMusicSystem.EMusicEventPreAction",
    "System.UInt32", 2);
MUSIC_METHOD(g_start_music_event, "AudioMusicSystem._StartMusicWithEvent",
    "_StartMusicWithEvent",
    "System.String|System.UInt32&|Beyond.Audio.AudioCallbackType|Beyond.Audio.AudioEventCallback",
    "System.Void", 4);
MUSIC_METHOD(g_stop_by_id, "AudioMusicSystem._StopMusicByPlayingId",
    "_StopMusicByPlayingId", "System.UInt32&", "System.Void", 1);
MUSIC_METHOD(g_pause_music, "AudioMusicSystem.PauseMusic",
    "PauseMusic", "", "System.Void", 0);
MUSIC_METHOD(g_resume_music, "AudioMusicSystem.ResumeMusic",
    "ResumeMusic", "", "System.Void", 0);
MUSIC_METHOD(g_stop_music, "AudioMusicSystem.StopMusic",
    "StopMusic", "", "System.Void", 0);
MUSIC_METHOD(g_timeline_pause, "AudioMusicSystem.OnTimelinePause",
    "OnTimelinePause", "", "System.Void", 0);
MUSIC_METHOD(g_timeline_resume, "AudioMusicSystem.OnTimelineResume",
    "OnTimelineResume", "", "System.Void", 0);
MUSIC_METHOD(g_timeline_skipped, "AudioMusicSystem.OnTimelineSkipped",
    "OnTimelineSkipped", "", "System.Void", 0);
MUSIC_METHOD(g_mute_full, "AudioMusicSystem.MuteMusic/full", "MuteMusic",
    "System.String|System.Boolean|System.Boolean|System.Boolean",
    "System.Void", 4);
MUSIC_METHOD(g_mute_simple, "AudioMusicSystem.MuteMusic/simple", "MuteMusic",
    "System.String", "System.Void", 1);
MUSIC_METHOD(g_unmute_named, "AudioMusicSystem.UnmuteMusic/named", "UnmuteMusic",
    "System.String", "System.Void", 1);
MUSIC_METHOD(g_unmute_all, "AudioMusicSystem.UnmuteMusic/all", "UnmuteMusic",
    "", "System.Void", 0);

#define MUSIC_STATE_METHOD(variable, key_name, method_name, parameter) \
    MUSIC_METHOD(variable, key_name, method_name, parameter, "System.Void", 1)

MUSIC_STATE_METHOD(g_set_music_state, "AudioMusicSystem._SetWwiseMusicState",
    "_SetWwiseMusicState", "Beyond.Gameplay.Audio.AudioMusicSystem.EWwiseMusicState");
MUSIC_STATE_METHOD(g_set_map_state, "AudioMusicSystem._SetWwiseMusicMapState",
    "_SetWwiseMusicMapState", "Beyond.Gameplay.Audio.AudioMusicSystem.EWwiseMusicMapState");
MUSIC_STATE_METHOD(g_set_battle_state, "AudioMusicSystem._SetWwiseBattleMusicState",
    "_SetWwiseBattleMusicState", "Beyond.Gameplay.Audio.AudioMusicSystem.EWwiseBattleMusicState");
MUSIC_STATE_METHOD(g_set_intensity_state,
    "AudioMusicSystem._SetWwiseBattleMusicIntensityState",
    "_SetWwiseBattleMusicIntensityState",
    "Beyond.Gameplay.Audio.AudioMusicSystem.EWwiseBattleMusicIntensityState");
MUSIC_STATE_METHOD(g_set_mission_state, "AudioMusicSystem._SetWwiseMissionMusicState",
    "_SetWwiseMissionMusicState", "Beyond.Gameplay.Audio.AudioMusicSystem.EWwiseMissionMusicState");
MUSIC_STATE_METHOD(g_set_dialog_state, "AudioMusicSystem._SetWwiseDialogMusicState",
    "_SetWwiseDialogMusicState", "Beyond.Gameplay.Audio.AudioMusicSystem.EWwiseDialogMusicState");
MUSIC_STATE_METHOD(g_set_cutscene_state, "AudioMusicSystem._SetWwiseCutsceneMusicState",
    "_SetWwiseCutsceneMusicState", "Beyond.Gameplay.Audio.AudioMusicSystem.EWwiseCutsceneMusicState");
MUSIC_STATE_METHOD(g_set_login_state, "AudioMusicSystem._SetWwiseLoginMenuMusicState",
    "_SetWwiseLoginMenuMusicState", "Beyond.Gameplay.Audio.AudioMusicSystem.EWwiseLoginMusicState");
MUSIC_STATE_METHOD(g_set_meta_state, "AudioMusicSystem._SetWwiseMetaMusicState",
    "_SetWwiseMetaMusicState", "Beyond.Gameplay.Audio.AudioMusicSystem.EWwiseMetaMusicState");
MUSIC_STATE_METHOD(g_set_remote_state, "AudioMusicSystem._SetWwiseRemoteCommMusicState",
    "_SetWwiseRemoteCommMusicState", "Beyond.Gameplay.Audio.AudioMusicSystem.EWwiseRemoteCommMusicState");

WWISE_METHOD(g_ak_set_state, "AkSoundEngine.SetState/id", "SetState",
    "System.UInt32|System.UInt32", "AKRESULT", 2);
WWISE_METHOD(g_ak_stop_full, "AkSoundEngine.StopPlayingID/full", "StopPlayingID",
    "System.UInt32|System.Int32|AkCurveInterpolation", "System.Void", 3);
WWISE_METHOD(g_ak_stop_duration, "AkSoundEngine.StopPlayingID/duration", "StopPlayingID",
    "System.UInt32|System.Int32", "System.Void", 2);
WWISE_METHOD(g_ak_stop_simple, "AkSoundEngine.StopPlayingID/simple", "StopPlayingID",
    "System.UInt32", "System.Void", 1);
WWISE_METHOD(g_ak_action_full, "AkSoundEngine.ExecuteActionOnPlayingID/full",
    "ExecuteActionOnPlayingID",
    "AkActionOnEventType|System.UInt32|System.Int32|AkCurveInterpolation",
    "System.Void", 4);
WWISE_METHOD(g_ak_action_duration, "AkSoundEngine.ExecuteActionOnPlayingID/duration",
    "ExecuteActionOnPlayingID",
    "AkActionOnEventType|System.UInt32|System.Int32", "System.Void", 3);
WWISE_METHOD(g_ak_action_simple, "AkSoundEngine.ExecuteActionOnPlayingID/simple",
    "ExecuteActionOnPlayingID", "AkActionOnEventType|System.UInt32",
    "System.Void", 2);
WWISE_METHOD(g_ak_mute_background, "AkSoundEngine.MuteBackgroundMusic",
    "MuteBackgroundMusic", "System.Boolean", "System.Void", 1);

AUDIO_INPUT_METHOD(g_audio_input_try_initialize,
    "AkAudioInputManager.TryInitialize", "TryInitialize", "", "System.Void", 0);
AUDIO_INPUT_METHOD(g_audio_input_post_event,
    "AkAudioInputManager.PostAudioInputEvent/id-object", "PostAudioInputEvent",
    "System.UInt32|UnityEngine.GameObject|AkAudioInputManager.AudioSamplesDelegate|AkAudioInputManager.AudioFormatDelegate",
    "System.UInt32", 4);
AUDIO_INPUT_METHOD(g_audio_input_samples,
    "AkAudioInputManager.InternalAudioSamplesDelegate",
    "InternalAudioSamplesDelegate",
    "System.UInt32|System.Single[]|System.UInt32|System.UInt32",
    "System.Boolean", 4);
AUDIO_INPUT_METHOD(g_audio_input_format,
    "AkAudioInputManager.InternalAudioFormatDelegate",
    "InternalAudioFormatDelegate", "System.UInt32|System.IntPtr",
    "System.Void", 2);
WWISE_METHOD(g_ak_load_bank_memory_copy,
    "AkSoundEngine.LoadBankMemoryCopy", "LoadBankMemoryCopy",
    "System.IntPtr|System.UInt32|System.UInt32&", "AKRESULT", 3);
WWISE_METHOD(g_ak_get_sample_rate,
    "AkSoundEngine.GetSampleRate", "GetSampleRate", "", "System.UInt32", 0);
WWISE_METHOD(g_ak_register_game_object,
    "AkSoundEngine.RegisterGameObj/object", "RegisterGameObj",
    "UnityEngine.GameObject", "AKRESULT", 1);
RuntimeMethod g_game_object_ctor{
    "UnityEngine.GameObject..ctor",
    {kUnityCoreAssembly, "UnityEngine", "GameObject", ".ctor",
        "System.String", "System.Void", 1}};
RuntimeMethod g_ak_set_audio_input_callbacks{
    "AkSoundEngine.SetAudioInputCallbacks",
    {kWwiseAssembly, "", "AkSoundEngine", "SetAudioInputCallbacks",
        "AkAudioInputManager.AudioSamplesInteropDelegate|AkAudioInputManager.AudioFormatInteropDelegate",
        "System.Void", 2}};
WWISE_PINVOKE_METHOD(g_channel_config_standard,
    "AkSoundEnginePINVOKE.CSharp_AkChannelConfig_Standard",
    "CSharp_AkChannelConfig_Standard", "System.UInt32", "System.IntPtr", 1);
WWISE_PINVOKE_METHOD(g_audio_format_set_all,
    "AkSoundEnginePINVOKE.CSharp_AkAudioFormat_SetAll",
    "CSharp_AkAudioFormat_SetAll",
    "System.IntPtr|System.UInt32|System.IntPtr|System.UInt32|System.UInt32|System.UInt32|System.UInt32",
    "System.Void", 7);
WWISE_PINVOKE_METHOD(g_ak_float_get,
    "AkSoundEnginePINVOKE.CSharp_AK_FLOAT_get", "CSharp_AK_FLOAT_get",
    "", "System.UInt16", 0);
WWISE_PINVOKE_METHOD(g_ak_noninterleaved_get,
    "AkSoundEnginePINVOKE.CSharp_AK_NONINTERLEAVED_get",
    "CSharp_AK_NONINTERLEAVED_get", "", "System.Byte", 0);
WWISE_METHOD(g_ak_is_plugin_registered,
    "AkSoundEngine.IsPluginRegistered", "IsPluginRegistered",
    "AkPluginType|System.UInt32|System.UInt32", "System.Boolean", 3);
WWISE_METHOD(g_ak_get_event_from_playing,
    "AkSoundEngine.GetEventIDFromPlayingID", "GetEventIDFromPlayingID",
    "System.UInt32", "System.UInt32", 1);
WWISE_METHOD(g_ak_get_game_object_from_playing,
    "AkSoundEngine.GetGameObjectFromPlayingID", "GetGameObjectFromPlayingID",
    "System.UInt32", "System.UInt64", 1);
WWISE_METHOD(g_ak_get_source_position,
    "AkSoundEngine.GetSourcePlayPosition", "GetSourcePlayPosition",
    "System.UInt32|System.Int32&|System.Boolean", "AKRESULT", 3);
WWISE_PINVOKE_METHOD(g_pinvoke_set_error_logger,
    "AkSoundEnginePINVOKE.CSharp_SetErrorLogger",
    "CSharp_SetErrorLogger__SWIG_0", "AkLogger.ErrorLoggerInteropDelegate",
    "System.Void", 1);
RuntimeMethod g_callback_manager_set_local_output{
    "AkCallbackManager.SetLocalOutput",
    {kWwiseAssembly, "", "AkCallbackManager", "SetLocalOutput",
        "AkMonitorErrorLevel", "System.Void", 1}};
RuntimeMethod g_callback_manager_set_monitoring_callback{
    "AkCallbackManager.SetMonitoringCallback",
    {kWwiseAssembly, "", "AkCallbackManager", "SetMonitoringCallback",
        "AkMonitorErrorLevel|AkCallbackManager.MonitoringCallback",
        "System.Void", 2}};
RuntimeMethod g_callback_serializer_set_local_output_api{
    "AkCallbackSerializer.SetLocalOutput",
    {kWwiseAssembly, "", "AkCallbackSerializer", "SetLocalOutput",
        "System.UInt32", "System.Void", 1}};
WWISE_PINVOKE_METHOD(g_callback_serializer_set_local_output,
    "AkSoundEnginePINVOKE.CSharp_AkCallbackSerializer_SetLocalOutput",
    "CSharp_AkCallbackSerializer_SetLocalOutput",
    "System.UInt32|System.String|System.UInt32|System.String|System.UInt32|System.UInt32",
    "System.Void", 6);

using VoidInstanceFn = void(__fastcall*)(void*, void*);
using PostMusicEventFn = uint32_t(__fastcall*)(void*, void*, int32_t, void*);
using StartMusicEventFn = void(__fastcall*)(
    void*, void*, uint32_t*, int32_t, void*, void*);
using StopByIdFn = void(__fastcall*)(void*, uint32_t*, void*);
using MuteFullFn = void(__fastcall*)(void*, void*, bool, bool, bool, void*);
using MuteNamedFn = void(__fastcall*)(void*, void*, void*);
using StateSetterFn = void(__fastcall*)(void*, int32_t, void*);
using AkSetStateFn = int32_t(__fastcall*)(uint32_t, uint32_t, void*);
using AkStopFullFn = void(__fastcall*)(uint32_t, int32_t, int32_t, void*);
using AkStopDurationFn = void(__fastcall*)(uint32_t, int32_t, void*);
using AkStopSimpleFn = void(__fastcall*)(uint32_t, void*);
using AkActionFullFn = void(__fastcall*)(int32_t, uint32_t, int32_t, int32_t, void*);
using AkActionDurationFn = void(__fastcall*)(int32_t, uint32_t, int32_t, void*);
using AkActionSimpleFn = void(__fastcall*)(int32_t, uint32_t, void*);
using AkMuteBackgroundFn = void(__fastcall*)(bool, void*);
using AudioInputSamplesFn = bool(__fastcall*)(
    uint32_t, void*, uint32_t, uint32_t, void*);
using AudioInputFormatFn = void(__fastcall*)(uint32_t, void*, void*);
using TryInitializeFn = void(__fastcall*)(void*);
using LoadBankMemoryCopyFn = int32_t(__fastcall*)(
    void*, uint32_t, uint32_t*, void*);
using GetSampleRateFn = uint32_t(__fastcall*)(void*);
using ChannelConfigStandardFn = void*(__fastcall*)(uint32_t, void*);
using AudioFormatSetAllFn = void(__fastcall*)(
    void*, uint32_t, void*, uint32_t, uint32_t, uint32_t, uint32_t, void*);
using GetUInt16ConstantFn = uint16_t(__fastcall*)(void*);
using GetByteConstantFn = uint8_t(__fastcall*)(void*);
using SetAudioInputCallbacksFn = void(__fastcall*)(void*, void*, void*);
using Il2CppDomainGetFn = void*(__fastcall*)();
using Il2CppDomainGetAssembliesFn = void**(__fastcall*)(void*, size_t*);
using Il2CppAssemblyGetImageFn = void*(__fastcall*)(void*);
using Il2CppImageGetNameFn = const char*(__fastcall*)(void*);
using Il2CppClassFromNameFn = void*(__fastcall*)(void*, const char*, const char*);
using Il2CppClassGetNameFn = const char*(__fastcall*)(void*);
using Il2CppClassGetNestedTypesFn = void*(__fastcall*)(void*, void**);
using Il2CppClassGetMethodsFn = void*(__fastcall*)(void*, void**);
using Il2CppClassGetFieldsFn = void*(__fastcall*)(void*, void**);
using Il2CppMethodGetNameFn = const char*(__fastcall*)(void*);
using Il2CppMethodGetParamCountFn = uint32_t(__fastcall*)(void*);
using Il2CppMethodGetParamFn = const void*(__fastcall*)(void*, uint32_t);
using Il2CppMethodGetReturnTypeFn = void*(__fastcall*)(void*);
using Il2CppTypeGetNameFn = const char*(__fastcall*)(void*);
using Il2CppFieldGetNameFn = const char*(__fastcall*)(void*);
using Il2CppFieldGetTypeFn = const void*(__fastcall*)(void*);
using Il2CppFieldGetFlagsFn = uint32_t(__fastcall*)(void*);
using Il2CppFieldGetOffsetFn = size_t(__fastcall*)(void*);
using Il2CppFieldStaticGetValueFn = void(__fastcall*)(void*, void*);
using Il2CppThreadCurrentFn = void*(__fastcall*)();
using Il2CppThreadAttachFn = void*(__fastcall*)(void*);
using Il2CppThreadDetachFn = void(__fastcall*)(void*);
using IsPluginRegisteredFn = bool(__fastcall*)(
    int32_t, uint32_t, uint32_t, void*);
using GetEventFromPlayingFn = uint32_t(__fastcall*)(uint32_t, void*);
using GetGameObjectFromPlayingFn = uint64_t(__fastcall*)(uint32_t, void*);
using GetSourcePositionFn = int32_t(__fastcall*)(
    uint32_t, int32_t*, bool, void*);
using PInvokeSetErrorLoggerFn = void(__fastcall*)(void*, void*);
using NativeErrorLoggerFn = void(__fastcall*)(const char*);
using NativeSetErrorLoggerFn = void(__fastcall*)(NativeErrorLoggerFn);
using SetLocalOutputFn = void(__fastcall*)(uint32_t, void*);
using CallbackSerializerSetLocalOutputFn = void(__fastcall*)(
    uint32_t, void*, uint32_t, void*, uint32_t, uint32_t, void*);

PostMusicEventFn g_original_post_music_event = nullptr;
StartMusicEventFn g_original_start_music_event = nullptr;
StopByIdFn g_original_stop_by_id = nullptr;
VoidInstanceFn g_original_pause_music = nullptr;
VoidInstanceFn g_original_resume_music = nullptr;
VoidInstanceFn g_original_stop_music = nullptr;
VoidInstanceFn g_original_timeline_pause = nullptr;
VoidInstanceFn g_original_timeline_resume = nullptr;
VoidInstanceFn g_original_timeline_skipped = nullptr;
MuteFullFn g_original_mute_full = nullptr;
MuteNamedFn g_original_mute_simple = nullptr;
MuteNamedFn g_original_unmute_named = nullptr;
VoidInstanceFn g_original_unmute_all = nullptr;
StateSetterFn g_original_set_music_state = nullptr;
StateSetterFn g_original_set_map_state = nullptr;
StateSetterFn g_original_set_battle_state = nullptr;
StateSetterFn g_original_set_intensity_state = nullptr;
StateSetterFn g_original_set_mission_state = nullptr;
StateSetterFn g_original_set_dialog_state = nullptr;
StateSetterFn g_original_set_cutscene_state = nullptr;
StateSetterFn g_original_set_login_state = nullptr;
StateSetterFn g_original_set_meta_state = nullptr;
StateSetterFn g_original_set_remote_state = nullptr;
AkSetStateFn g_original_ak_set_state = nullptr;
AkStopFullFn g_original_ak_stop_full = nullptr;
AkStopDurationFn g_original_ak_stop_duration = nullptr;
AkStopSimpleFn g_original_ak_stop_simple = nullptr;
AkActionFullFn g_original_ak_action_full = nullptr;
AkActionDurationFn g_original_ak_action_duration = nullptr;
AkActionSimpleFn g_original_ak_action_simple = nullptr;
AkMuteBackgroundFn g_original_ak_mute_background = nullptr;
AudioInputSamplesFn g_original_audio_input_samples = nullptr;
AudioInputFormatFn g_original_audio_input_format = nullptr;
TryInitializeFn g_original_audio_input_try_initialize = nullptr;
SetAudioInputCallbacksFn g_original_set_audio_input_callbacks = nullptr;
CallbackSerializerSetLocalOutputFn
    g_original_callback_serializer_set_local_output = nullptr;
Il2CppDomainGetFn g_il2cpp_domain_get = nullptr;
Il2CppDomainGetAssembliesFn g_il2cpp_domain_get_assemblies = nullptr;
Il2CppAssemblyGetImageFn g_il2cpp_assembly_get_image = nullptr;
Il2CppImageGetNameFn g_il2cpp_image_get_name = nullptr;
Il2CppClassFromNameFn g_il2cpp_class_from_name = nullptr;
Il2CppClassGetNameFn g_il2cpp_class_get_name = nullptr;
Il2CppClassGetNestedTypesFn g_il2cpp_class_get_nested_types = nullptr;
Il2CppClassGetMethodsFn g_il2cpp_class_get_methods = nullptr;
Il2CppClassGetFieldsFn g_il2cpp_class_get_fields = nullptr;
Il2CppMethodGetNameFn g_il2cpp_method_get_name = nullptr;
Il2CppMethodGetParamCountFn g_il2cpp_method_get_param_count = nullptr;
Il2CppMethodGetParamFn g_il2cpp_method_get_param = nullptr;
Il2CppMethodGetReturnTypeFn g_il2cpp_method_get_return_type = nullptr;
Il2CppTypeGetNameFn g_il2cpp_type_get_name = nullptr;
Il2CppFieldGetNameFn g_il2cpp_field_get_name = nullptr;
Il2CppFieldGetTypeFn g_il2cpp_field_get_type = nullptr;
Il2CppFieldGetFlagsFn g_il2cpp_field_get_flags = nullptr;
Il2CppFieldGetOffsetFn g_il2cpp_field_get_offset = nullptr;
Il2CppFieldStaticGetValueFn g_il2cpp_field_static_get_value = nullptr;
Il2CppThreadCurrentFn g_il2cpp_thread_current = nullptr;
Il2CppThreadAttachFn g_il2cpp_thread_attach = nullptr;
Il2CppThreadDetachFn g_il2cpp_thread_detach = nullptr;

bool IsMonitorMethodName(const char* name) {
    if (!name) {
        return false;
    }
    const std::string_view value(name);
    return value.find("LocalOutput") != std::string_view::npos ||
        value.find("Monitor") != std::string_view::npos ||
        value.find("ErrorLogger") != std::string_view::npos;
}

void* ReadRuntimeMethodPointer(void* method) noexcept {
    if (!method) {
        return nullptr;
    }
    __try {
        // MethodInfo::methodPointer is the first field in this Unity IL2CPP ABI.
        return *static_cast<void**>(method);
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

std::string CodeLocation(void* target) {
    if (!target) {
        return "<null>";
    }
    const auto address = reinterpret_cast<uintptr_t>(target);
    const auto game_assembly = reinterpret_cast<uintptr_t>(
        GetModuleHandleW(L"GameAssembly.dll"));
    const auto wwise = reinterpret_cast<uintptr_t>(
        GetModuleHandleW(L"AkSoundEngine.dll"));
    char buffer[96]{};
    if (game_assembly && address >= game_assembly &&
        address < ExecutableImageEnd(game_assembly)) {
        std::snprintf(buffer, sizeof(buffer), "GameAssembly+0x%llX",
            static_cast<unsigned long long>(address - game_assembly));
    } else if (wwise && address >= wwise &&
        address < ExecutableImageEnd(wwise)) {
        std::snprintf(buffer, sizeof(buffer), "AkSoundEngine+0x%llX",
            static_cast<unsigned long long>(address - wwise));
    } else {
        std::snprintf(buffer, sizeof(buffer), "%p", target);
    }
    return buffer;
}

size_t CopyMethodCode(void* target, uint8_t* output, size_t capacity) noexcept {
    if (!target || !output || !capacity) {
        return 0;
    }
    __try {
        MEMORY_BASIC_INFORMATION memory{};
        if (!VirtualQuery(target, &memory, sizeof(memory))) {
            return 0;
        }
        const uintptr_t address = reinterpret_cast<uintptr_t>(target);
        const uintptr_t region_end = reinterpret_cast<uintptr_t>(
            memory.BaseAddress) + memory.RegionSize;
        const size_t byte_count = static_cast<size_t>(
            std::min<uintptr_t>(capacity, region_end - address));
        const auto* bytes = static_cast<const uint8_t*>(target);
        for (size_t index = 0; index < byte_count; ++index) {
            output[index] = bytes[index];
        }
        return byte_count;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return 0;
    }
}

void LogMethodCode(const std::string& label, void* target) {
    Log("[wwise-monitor] code method=" + label + " target=" +
        CodeLocation(target));
    std::array<uint8_t, 128> bytes{};
    const size_t byte_count = CopyMethodCode(
        target, bytes.data(), bytes.size());
    if (!byte_count) {
        if (target) {
            Log("[wwise-monitor] code unreadable method=" + label);
        }
        return;
    }
    for (size_t start = 0; start < byte_count; start += 32) {
        const size_t end = std::min(start + 32, byte_count);
        char offset[24]{};
        std::snprintf(offset, sizeof(offset), " +0x%02llX ",
            static_cast<unsigned long long>(start));
        std::string line = "[wwise-monitor] code method=" + label + offset;
        char value[4]{};
        for (size_t index = start; index < end; ++index) {
            std::snprintf(value, sizeof(value), "%02X", bytes[index]);
            line += value;
        }
        Log(line);
    }
}

void* FindRuntimeImage(const char* assembly_name) {
    if (!assembly_name || !g_il2cpp_domain_get ||
        !g_il2cpp_domain_get_assemblies || !g_il2cpp_assembly_get_image ||
        !g_il2cpp_image_get_name) {
        return nullptr;
    }
    void* domain = g_il2cpp_domain_get();
    size_t assembly_count = 0;
    void** assemblies = domain
        ? g_il2cpp_domain_get_assemblies(domain, &assembly_count) : nullptr;
    for (size_t index = 0; assemblies && index < assembly_count; ++index) {
        void* image = g_il2cpp_assembly_get_image(assemblies[index]);
        const char* image_name = image ? g_il2cpp_image_get_name(image) : nullptr;
        if (image_name && _stricmp(image_name, assembly_name) == 0) {
            return image;
        }
    }
    return nullptr;
}

void* FindAudioInputClass() {
    void* image = FindRuntimeImage(kWwiseAssembly);
    return image && g_il2cpp_class_from_name
        ? g_il2cpp_class_from_name(image, "", kAudioInputClass) : nullptr;
}

bool CanReadAudioInputStaticValue(const char* type_name) {
    if (!type_name) {
        return false;
    }
    const std::string_view type(type_name);
    return type.find("Delegate") != std::string_view::npos ||
        type == "System.Boolean" || type == "System.Byte" ||
        type == "System.Int32" || type == "System.UInt32" ||
        type == "System.Int64" || type == "System.UInt64" ||
        type == "System.IntPtr" || type == "System.UIntPtr";
}

void LogAudioInputStaticFields(const char* stage) {
    if (!g_il2cpp_class_get_fields || !g_il2cpp_field_get_name ||
        !g_il2cpp_field_get_type || !g_il2cpp_field_get_flags ||
        !g_il2cpp_field_get_offset || !g_il2cpp_type_get_name) {
        Log("[music-input-diag] field enumeration API is incomplete");
        return;
    }
    void* klass = FindAudioInputClass();
    if (!klass) {
        Log("[music-input-diag] class unavailable=AkAudioInputManager");
        return;
    }

    uint32_t field_count = 0;
    uint32_t static_count = 0;
    void* iterator = nullptr;
    while (void* field = g_il2cpp_class_get_fields(klass, &iterator)) {
        ++field_count;
        const char* field_name = g_il2cpp_field_get_name(field);
        const void* field_type = g_il2cpp_field_get_type(field);
        const char* type_name = field_type
            ? g_il2cpp_type_get_name(const_cast<void*>(field_type)) : nullptr;
        const uint32_t flags = g_il2cpp_field_get_flags(field);
        const bool is_static = (flags & 0x10u) != 0;
        if (!is_static) {
            continue;
        }
        ++static_count;
        const size_t offset = g_il2cpp_field_get_offset(field);
        uintptr_t raw_value = 0;
        const bool value_readable = g_il2cpp_field_static_get_value &&
            CanReadAudioInputStaticValue(type_name);
        if (value_readable) {
            g_il2cpp_field_static_get_value(field, &raw_value);
        }
        char line[512]{};
        std::snprintf(line, sizeof(line),
            "[music-input-diag] stage=%s field=%s type=%s flags=0x%X "
            "offset=0x%llX value=%s%p",
            stage ? stage : "<null>",
            field_name ? field_name : "<unknown>",
            type_name ? type_name : "<unknown>", flags,
            static_cast<unsigned long long>(offset),
            value_readable ? "" : "<not-read> ",
            reinterpret_cast<void*>(raw_value));
        Log(line);
    }
    Log("[music-input-diag] stage=" + std::string(stage ? stage : "<null>") +
        " fields=" + std::to_string(field_count) + " static=" +
        std::to_string(static_count));
}

void LogAudioInputContracts() {
    if (!g_il2cpp_class_get_methods || !g_il2cpp_method_get_name ||
        !g_il2cpp_method_get_param_count || !g_il2cpp_method_get_param ||
        !g_il2cpp_method_get_return_type || !g_il2cpp_type_get_name) {
        Log("[music-input-diag] method enumeration API is incomplete");
        return;
    }
    void* klass = FindAudioInputClass();
    if (!klass) {
        Log("[music-input-diag] class unavailable=AkAudioInputManager");
        return;
    }

    uint32_t method_count = 0;
    void* iterator = nullptr;
    while (void* method = g_il2cpp_class_get_methods(klass, &iterator)) {
        const char* method_name = g_il2cpp_method_get_name(method);
        std::string signature = "[music-input-diag] method=";
        signature += method_name ? method_name : "<unknown>";
        signature += " params=(";
        const uint32_t parameter_count =
            g_il2cpp_method_get_param_count(method);
        for (uint32_t index = 0; index < parameter_count; ++index) {
            if (index) {
                signature += "|";
            }
            const void* parameter_type =
                g_il2cpp_method_get_param(method, index);
            const char* parameter_name = parameter_type
                ? g_il2cpp_type_get_name(
                    const_cast<void*>(parameter_type)) : nullptr;
            signature += parameter_name ? parameter_name : "<unknown>";
        }
        const char* return_name = g_il2cpp_type_get_name(
            g_il2cpp_method_get_return_type(method));
        signature += ") return=";
        signature += return_name ? return_name : "<unknown>";
        signature += " target=";
        signature += CodeLocation(ReadRuntimeMethodPointer(method));
        Log(signature);
        ++method_count;
    }
    Log("[music-input-diag] methods=" + std::to_string(method_count));
    LogAudioInputStaticFields("module-initialize");
}

void LogMonitorMethodContracts(const char* class_name) {
    if (!class_name || !g_il2cpp_class_from_name ||
        !g_il2cpp_class_get_methods || !g_il2cpp_method_get_name ||
        !g_il2cpp_method_get_param_count || !g_il2cpp_method_get_param ||
        !g_il2cpp_method_get_return_type || !g_il2cpp_type_get_name) {
        Log("[wwise-monitor] IL2CPP metadata enumeration API is incomplete");
        return;
    }
    void* image = FindRuntimeImage(kWwiseAssembly);
    void* klass = image
        ? g_il2cpp_class_from_name(image, "", class_name) : nullptr;
    if (!klass) {
        Log("[wwise-monitor] class unavailable=" + std::string(class_name));
        return;
    }

    uint32_t matched = 0;
    void* iterator = nullptr;
    while (void* method = g_il2cpp_class_get_methods(klass, &iterator)) {
        const char* method_name = g_il2cpp_method_get_name(method);
        if (!IsMonitorMethodName(method_name)) {
            continue;
        }
        std::string signature = "[wwise-monitor] contract class=";
        signature += class_name;
        signature += " method=";
        signature += method_name;
        signature += " params=(";
        const uint32_t parameter_count =
            g_il2cpp_method_get_param_count(method);
        for (uint32_t index = 0; index < parameter_count; ++index) {
            if (index) {
                signature += "|";
            }
            const void* parameter_type = g_il2cpp_method_get_param(method, index);
            const char* parameter_name = parameter_type
                ? g_il2cpp_type_get_name(const_cast<void*>(parameter_type)) : nullptr;
            signature += parameter_name ? parameter_name : "<unknown>";
        }
        const char* return_name = g_il2cpp_type_get_name(
            g_il2cpp_method_get_return_type(method));
        signature += ") return=";
        signature += return_name ? return_name : "<unknown>";
        void* method_pointer = ReadRuntimeMethodPointer(method);
        signature += " target=";
        signature += CodeLocation(method_pointer);
        Log(signature);
        if (std::strcmp(method_name, "SetLocalOutput") == 0 ||
            std::strcmp(method_name, "SetMonitoringCallback") == 0) {
            LogMethodCode(std::string(class_name) + "." + method_name,
                method_pointer);
        }
        ++matched;
    }
    Log("[wwise-monitor] class=" + std::string(class_name) +
        " matchingMethods=" + std::to_string(matched));
}

void LogMonitorNestedContracts() {
    if (!g_il2cpp_class_from_name || !g_il2cpp_class_get_name ||
        !g_il2cpp_class_get_nested_types || !g_il2cpp_class_get_methods ||
        !g_il2cpp_method_get_name || !g_il2cpp_method_get_param_count ||
        !g_il2cpp_method_get_param || !g_il2cpp_method_get_return_type ||
        !g_il2cpp_type_get_name) {
        Log("[wwise-monitor] nested type enumeration API is incomplete");
        return;
    }
    void* image = FindRuntimeImage(kWwiseAssembly);
    void* manager = image
        ? g_il2cpp_class_from_name(image, "", "AkCallbackManager") : nullptr;
    if (!manager) {
        Log("[wwise-monitor] nested owner unavailable=AkCallbackManager");
        return;
    }

    uint32_t nested_count = 0;
    void* nested_iterator = nullptr;
    while (void* nested = g_il2cpp_class_get_nested_types(
        manager, &nested_iterator)) {
        const char* nested_name = g_il2cpp_class_get_name(nested);
        ++nested_count;
        Log("[wwise-monitor] nested class=AkCallbackManager." +
            std::string(nested_name ? nested_name : "<unknown>"));
        if (!nested_name || std::strcmp(nested_name, "MonitoringCallback") != 0) {
            continue;
        }

        uint32_t method_count = 0;
        void* method_iterator = nullptr;
        while (void* method = g_il2cpp_class_get_methods(
            nested, &method_iterator)) {
            const char* method_name = g_il2cpp_method_get_name(method);
            std::string signature =
                "[wwise-monitor] nested contract class=AkCallbackManager.";
            signature += nested_name;
            signature += " method=";
            signature += method_name ? method_name : "<unknown>";
            signature += " params=(";
            const uint32_t parameter_count =
                g_il2cpp_method_get_param_count(method);
            for (uint32_t index = 0; index < parameter_count; ++index) {
                if (index) {
                    signature += "|";
                }
                const void* parameter_type =
                    g_il2cpp_method_get_param(method, index);
                const char* parameter_name = parameter_type
                    ? g_il2cpp_type_get_name(
                        const_cast<void*>(parameter_type)) : nullptr;
                signature += parameter_name ? parameter_name : "<unknown>";
            }
            const char* return_name = g_il2cpp_type_get_name(
                g_il2cpp_method_get_return_type(method));
            void* method_pointer = ReadRuntimeMethodPointer(method);
            signature += ") return=";
            signature += return_name ? return_name : "<unknown>";
            signature += " target=";
            signature += CodeLocation(method_pointer);
            Log(signature);
            ++method_count;
        }
        Log("[wwise-monitor] nested class=AkCallbackManager.MonitoringCallback"
            " methods=" + std::to_string(method_count));
    }
    Log("[wwise-monitor] nested owner=AkCallbackManager classes=" +
        std::to_string(nested_count));
}

struct Il2CppArrayHeader {
    void* klass;
    void* monitor;
    void* bounds;
    uintptr_t max_length;
};

bool TryGetArrayStorage(
    void* samples, float** values, uintptr_t* capacity) noexcept {
    if (!values || !capacity) {
        return false;
    }
    __try {
        auto* array = static_cast<Il2CppArrayHeader*>(samples);
        *capacity = array ? array->max_length : 0;
        *values = array ? reinterpret_cast<float*>(array + 1) : nullptr;
        return *values && *capacity;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        *values = nullptr;
        *capacity = 0;
        return false;
    }
}

bool AttachAudioThread(bool* attached_here, void** thread) noexcept {
    if (!attached_here || !thread || !g_il2cpp_domain_get ||
        !g_il2cpp_thread_current || !g_il2cpp_thread_attach) {
        return false;
    }
    *attached_here = false;
    *thread = g_il2cpp_thread_current();
    if (*thread) {
        return true;
    }
    void* domain = g_il2cpp_domain_get();
    if (!domain) {
        return false;
    }
    *thread = g_il2cpp_thread_attach(domain);
    *attached_here = *thread != nullptr;
    return *thread != nullptr;
}

bool IsExecutableInModule(void* address, HMODULE module) noexcept {
    if (!address || !module) {
        return false;
    }
    MEMORY_BASIC_INFORMATION memory{};
    if (!VirtualQuery(address, &memory, sizeof(memory)) ||
        memory.AllocationBase != module) {
        return false;
    }
    const DWORD protection = memory.Protect & 0xFFu;
    return protection == PAGE_EXECUTE ||
        protection == PAGE_EXECUTE_READ ||
        protection == PAGE_EXECUTE_READWRITE ||
        protection == PAGE_EXECUTE_WRITECOPY;
}

void* FindResolvedPInvokeTarget(void* wrapper) noexcept {
    const HMODULE wwise = GetModuleHandleW(L"AkSoundEngine.dll");
    if (!wrapper || !wwise) {
        return nullptr;
    }
    __try {
        const auto* code = static_cast<const uint8_t*>(wrapper);
        for (size_t offset = 0; offset + 7 <= 256; ++offset) {
            if (code[offset] != 0x48 || code[offset + 1] != 0xFF ||
                code[offset + 2] != 0x25) {
                continue;
            }
            const int32_t displacement =
                *reinterpret_cast<const int32_t*>(code + offset + 3);
            void** slot = reinterpret_cast<void**>(
                const_cast<uint8_t*>(code) + offset + 7 + displacement);
            void* target = *slot;
            if (IsExecutableInModule(target, wwise)) {
                return target;
            }
        }
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
    }
    return nullptr;
}

size_t SafeNativeStringLength(const char* value) noexcept {
    if (!value) {
        return 0;
    }
    __try {
        size_t length = 0;
        while (length < 2048 && value[length]) {
            ++length;
        }
        return length;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return 0;
    }
}

void __fastcall NativeErrorLogger(const char* message) {
    const size_t length = SafeNativeStringLength(message);
    Log("[wwise-error] " + (message && length
        ? std::string(message, length)
        : std::string(message ? "<invalid>" : "<null>")));
}

void InstallNativeErrorLogger() {
    const auto set_error_logger = reinterpret_cast<PInvokeSetErrorLoggerFn>(
        g_pinvoke_set_error_logger.resolved.method_pointer);
    if (!set_error_logger) {
        Log("[wwise-error] P/Invoke wrapper is unavailable");
        return;
    }
    // Resolve the obfuscated AkSoundEngine export using its normal wrapper.
    set_error_logger(
        nullptr, const_cast<void*>(g_pinvoke_set_error_logger.resolved.method_info));
    void* native_target = FindResolvedPInvokeTarget(
        g_pinvoke_set_error_logger.resolved.method_pointer);
    if (native_target) {
        reinterpret_cast<NativeSetErrorLoggerFn>(native_target)(
            &NativeErrorLogger);
    }
    char buffer[160]{};
    std::snprintf(buffer, sizeof(buffer),
        "[wwise-error] native logger target=%p registration=%s",
        native_target, native_target ? "installed" : "failed");
    Log(buffer);
}

void __fastcall CallbackSerializerSetLocalOutputHook(
    uint32_t error_code, void* error, uint32_t error_level, void* base_path,
    uint32_t playing_id, uint32_t game_object_id, void* method) {
    Log("[wwise-monitor] output errorCode=" + std::to_string(error_code) +
        " errorLevel=" + std::to_string(error_level) +
        " playingId=" + std::to_string(playing_id) +
        " gameObject=" + std::to_string(game_object_id) +
        " message=\"" + ManagedString(error) + "\" basePath=\"" +
        ManagedString(base_path) + "\"");
    if (g_original_callback_serializer_set_local_output) {
        g_original_callback_serializer_set_local_output(error_code, error,
            error_level, base_path, playing_id, game_object_id, method);
    }
}

void EnableWwiseMonitorOutput() {
    const bool serializer_available =
        g_callback_serializer_set_local_output_api.resolved.method_pointer !=
        nullptr;
    const auto set_local_output = reinterpret_cast<SetLocalOutputFn>(
        serializer_available
            ? g_callback_serializer_set_local_output_api.resolved.method_pointer
            : g_callback_manager_set_local_output.resolved.method_pointer);
    if (!set_local_output) {
        Log("[wwise-monitor] SetLocalOutput call target is unavailable");
        return;
    }
    set_local_output(kMonitorErrorLevelAll,
        const_cast<void*>(serializer_available
            ? g_callback_serializer_set_local_output_api.resolved.method_info
            : g_callback_manager_set_local_output.resolved.method_info));
    Log(std::string("[wwise-monitor] SetLocalOutput enabled errorLevel=all source=") +
        (serializer_available ? "AkCallbackSerializer" :
            "AkCallbackManager-empty-fallback"));
}

const char* ProbeStateName(ProbeState state) {
    switch (state) {
    case ProbeState::Idle: return "idle";
    case ProbeState::Starting: return "starting";
    case ProbeState::Playing: return "playing";
    case ProbeState::Finished: return "finished";
    case ProbeState::Failed: return "failed";
    default: return "unknown";
    }
}

bool IsProbeCallback(uint32_t playing_id) {
    if (!playing_id) {
        return false;
    }
    uint32_t current = g_probe_playing_id.load(std::memory_order_acquire);
    if (current == playing_id) {
        return true;
    }
    if (!current && g_probe_state.load(std::memory_order_acquire) ==
            ProbeState::Starting) {
        g_probe_playing_id.compare_exchange_strong(
            current, playing_id, std::memory_order_acq_rel);
        return g_probe_playing_id.load(std::memory_order_acquire) == playing_id;
    }
    return false;
}

bool __fastcall AudioInputSamplesHook(uint32_t playing_id, void* samples,
    uint32_t channel_index, uint32_t frames, void* method) {
    if (!IsProbeCallback(playing_id)) {
        return g_original_audio_input_samples
            ? g_original_audio_input_samples(
                playing_id, samples, channel_index, frames, method)
            : false;
    }

    float* values = nullptr;
    uintptr_t capacity = 0;
    if (!TryGetArrayStorage(samples, &values, &capacity)) {
        g_probe_state.store(ProbeState::Failed, std::memory_order_release);
        Log("[music-input] sample callback received an invalid IL2CPP array");
        return false;
    }

    const uint32_t writable = static_cast<uint32_t>(
        std::min<uintptr_t>(capacity, frames));
    const uint32_t sample_rate = std::max(
        g_probe_sample_rate.load(std::memory_order_relaxed), 1u);
    const double phase_step = 2.0 * kPi * kProbeFrequencyHz /
        static_cast<double>(sample_rate);
    for (uint32_t index = 0; index < writable; ++index) {
        values[index] = channel_index == 0
            ? kProbeAmplitude * static_cast<float>(std::sin(g_probe_phase))
            : 0.0f;
        g_probe_phase += phase_step;
        if (g_probe_phase >= 2.0 * kPi) {
            g_probe_phase -= 2.0 * kPi;
        }
    }
    for (uintptr_t index = writable; index < capacity; ++index) {
        values[index] = 0.0f;
    }

    const uint64_t callback_index =
        g_probe_sample_callbacks.fetch_add(1, std::memory_order_relaxed) + 1;
    const uint64_t total_frames =
        g_probe_frames.fetch_add(writable, std::memory_order_relaxed) + writable;
    if (callback_index == 1) {
        Log("[music-input] first sample callback playingId=" +
            std::to_string(playing_id) + " frames=" +
            std::to_string(writable) + " sampleRate=" +
            std::to_string(sample_rate));
    }

    const uint64_t frame_limit =
        static_cast<uint64_t>(sample_rate) * kProbeDurationSeconds;
    if (total_frames >= frame_limit) {
        ProbeState expected = ProbeState::Playing;
        if (g_probe_state.compare_exchange_strong(
                expected, ProbeState::Finished, std::memory_order_acq_rel)) {
            Log("[music-input] sine source completed playingId=" +
                std::to_string(playing_id) + " callbacks=" +
                std::to_string(callback_index) + " frames=" +
                std::to_string(total_frames));
        }
        return false;
    }
    return true;
}

void __fastcall AudioInputFormatHook(
    uint32_t playing_id, void* format, void* method) {
    if (!IsProbeCallback(playing_id)) {
        if (g_original_audio_input_format) {
            g_original_audio_input_format(playing_id, format, method);
        }
        return;
    }

    const auto set_all = reinterpret_cast<AudioFormatSetAllFn>(
        g_audio_format_set_all.resolved.method_pointer);
    if (!format || !g_probe_channel_config || !set_all) {
        g_probe_state.store(ProbeState::Failed, std::memory_order_release);
        Log("[music-input] format callback prerequisites are unavailable");
        return;
    }
    const uint32_t sample_rate =
        g_probe_sample_rate.load(std::memory_order_relaxed);
    set_all(format, sample_rate, g_probe_channel_config,
        32u, 4u, g_probe_sample_type, g_probe_interleave,
        const_cast<void*>(g_audio_format_set_all.resolved.method_info));
    if (!g_probe_format_seen.exchange(true, std::memory_order_acq_rel)) {
        Log("[music-input] format callback playingId=" +
            std::to_string(playing_id) + " sampleRate=" +
            std::to_string(sample_rate) + " channels=1 bits=32 blockAlign=4");
    }
}

void __fastcall AudioInputTryInitializeHook(void* method) {
    const uint32_t call_index =
        g_try_initialize_calls.fetch_add(1, std::memory_order_acq_rel) + 1;
    Log("[music-input-diag] TryInitialize enter call=" +
        std::to_string(call_index));
    LogAudioInputStaticFields("before-try-initialize");
    if (g_original_audio_input_try_initialize) {
        g_original_audio_input_try_initialize(method);
    }
    LogAudioInputStaticFields("after-try-initialize");
    Log("[music-input-diag] TryInitialize exit call=" +
        std::to_string(call_index) + " callbacksReady=" +
        std::string(g_managed_callbacks_ready.load(std::memory_order_acquire)
            ? "true" : "false"));
}

bool ReadProbeBank(std::vector<uint8_t>& result, std::wstring& path) {
    HMODULE module = nullptr;
    if (!GetModuleHandleExW(
            GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
                GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(&g_host), &module)) {
        return false;
    }
    std::array<wchar_t, 32768> module_path{};
    const DWORD length = GetModuleFileNameW(
        module, module_path.data(), static_cast<DWORD>(module_path.size()));
    if (!length || length >= module_path.size()) {
        return false;
    }
    path.assign(module_path.data(), length);
    const size_t separator = path.find_last_of(L"\\/");
    if (separator == std::wstring::npos) {
        return false;
    }
    path.resize(separator + 1);
    path += L"BetterEndfield.MusicProbe.bnk";

    const HANDLE file = CreateFileW(path.c_str(), GENERIC_READ, FILE_SHARE_READ,
        nullptr, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, nullptr);
    if (file == INVALID_HANDLE_VALUE) {
        return false;
    }
    LARGE_INTEGER size{};
    const bool valid_size = GetFileSizeEx(file, &size) &&
        size.QuadPart > 0 && size.QuadPart <= 1024 * 1024;
    if (!valid_size) {
        CloseHandle(file);
        return false;
    }
    result.resize(static_cast<size_t>(size.QuadPart));
    DWORD read = 0;
    const bool read_ok = ReadFile(file, result.data(),
        static_cast<DWORD>(result.size()), &read, nullptr) != FALSE;
    CloseHandle(file);
    return read_ok && read == result.size();
}

void __fastcall SetAudioInputCallbacksHook(
    void* samples, void* format, void* method) {
    if (g_original_set_audio_input_callbacks) {
        g_original_set_audio_input_callbacks(samples, format, method);
    }
    const bool callbacks_ready = samples != nullptr && format != nullptr;
    g_managed_callbacks_ready.store(callbacks_ready, std::memory_order_release);
    char callback_log[192]{};
    std::snprintf(callback_log, sizeof(callback_log),
        "[music-input] SetAudioInputCallbacks managedSamples=%s "
        "managedFormat=%s mode=managed-original ready=%s",
        samples ? "non-null" : "null", format ? "non-null" : "null",
        callbacks_ready ? "true" : "false");
    Log(callback_log);
}

bool CreateProbeGameObject() {
    if (g_probe_game_object) {
        return true;
    }
    if (!g_host || !g_host->object_new || !g_host->string_new ||
        !g_host->runtime_invoke || !g_host->gchandle_new ||
        !g_probe_game_object_class.class_info ||
        !g_game_object_ctor.resolved.method_info) {
        Log("[music-input] GameObject creation contract is unavailable");
        return false;
    }

    void* game_object = g_host->object_new(
        g_host->context, g_probe_game_object_class.class_info);
    void* name = g_host->string_new(
        g_host->context, "BetterEndfield.AudioInputProbe");
    if (!game_object || !name) {
        Log("[music-input] managed GameObject allocation failed");
        return false;
    }
    void* parameters[1]{name};
    void* exception = nullptr;
    g_host->runtime_invoke(g_host->context,
        g_game_object_ctor.resolved.method_info, game_object,
        parameters, &exception);
    if (exception) {
        Log("[music-input] GameObject constructor raised a managed exception");
        return false;
    }
    const uint32_t handle = g_host->gchandle_new(
        g_host->context, game_object, 0);
    if (!handle) {
        Log("[music-input] GameObject GC handle allocation failed");
        return false;
    }
    g_probe_game_object = game_object;
    g_probe_game_object_handle = handle;
    char message[192]{};
    std::snprintf(message, sizeof(message),
        "[music-input] probe GameObject created object=%p gcHandle=%u",
        game_object, handle);
    Log(message);
    return true;
}

bool RegisterProbeGameObject() {
    if (!g_host || !g_host->runtime_invoke || !g_host->object_unbox ||
        !g_ak_register_game_object.resolved.method_info ||
        !g_probe_game_object) {
        return false;
    }
    void* parameters[1]{g_probe_game_object};
    void* exception = nullptr;
    void* boxed = g_host->runtime_invoke(g_host->context,
        g_ak_register_game_object.resolved.method_info, nullptr,
        parameters, &exception);
    int32_t result = 0;
    void* value = boxed
        ? g_host->object_unbox(g_host->context, boxed) : nullptr;
    if (value) {
        std::memcpy(&result, value, sizeof(result));
    }
    Log("[music-input] RegisterGameObj(GameObject) result=" +
        std::to_string(result) + " exception=" +
        std::string(exception ? "true" : "false"));
    return !exception && value && result == 1;
}

uint32_t PostManagedAudioInputEvent() {
    if (!g_host || !g_host->runtime_invoke || !g_host->object_unbox ||
        !g_audio_input_post_event.resolved.method_info ||
        !g_probe_game_object) {
        return 0;
    }
    uint32_t event_id = kProbeEventId;
    void* parameters[4]{&event_id, g_probe_game_object, nullptr, nullptr};
    void* exception = nullptr;
    void* boxed = g_host->runtime_invoke(g_host->context,
        g_audio_input_post_event.resolved.method_info, nullptr,
        parameters, &exception);
    if (exception || !boxed) {
        Log(std::string("[music-input] PostAudioInputEvent failed exception=") +
            (exception ? "true" : "false") + " boxed=" +
            (boxed ? "non-null" : "null"));
        return 0;
    }
    void* value = g_host->object_unbox(g_host->context, boxed);
    uint32_t playing_id = 0;
    if (value) {
        std::memcpy(&playing_id, value, sizeof(playing_id));
    }
    Log("[music-input] PostAudioInputEvent returned playingId=" +
        std::to_string(playing_id) + " delegates=null/null");
    return playing_id;
}

void StartSineProbe() {
    ProbeState expected = ProbeState::Idle;
    if (!g_probe_state.compare_exchange_strong(
            expected, ProbeState::Starting, std::memory_order_acq_rel)) {
        return;
    }

    std::vector<uint8_t> bank;
    std::wstring bank_path;
    if (!ReadProbeBank(bank, bank_path)) {
        g_probe_state.store(ProbeState::Failed, std::memory_order_release);
        Log("[music-input] probe bank could not be read beside the module");
        return;
    }

    const auto load_bank = reinterpret_cast<LoadBankMemoryCopyFn>(
        g_ak_load_bank_memory_copy.resolved.method_pointer);
    const auto get_sample_rate = reinterpret_cast<GetSampleRateFn>(
        g_ak_get_sample_rate.resolved.method_pointer);
    const auto standard_config = reinterpret_cast<ChannelConfigStandardFn>(
        g_channel_config_standard.resolved.method_pointer);
    const auto get_float = reinterpret_cast<GetUInt16ConstantFn>(
        g_ak_float_get.resolved.method_pointer);
    const auto get_noninterleaved = reinterpret_cast<GetByteConstantFn>(
        g_ak_noninterleaved_get.resolved.method_pointer);
    const auto is_plugin_registered = reinterpret_cast<IsPluginRegisteredFn>(
        g_ak_is_plugin_registered.resolved.method_pointer);
    const auto get_event_from_playing =
        reinterpret_cast<GetEventFromPlayingFn>(
            g_ak_get_event_from_playing.resolved.method_pointer);
    const auto get_game_object_from_playing =
        reinterpret_cast<GetGameObjectFromPlayingFn>(
            g_ak_get_game_object_from_playing.resolved.method_pointer);
    const auto get_source_position = reinterpret_cast<GetSourcePositionFn>(
        g_ak_get_source_position.resolved.method_pointer);
    if (!load_bank || !get_sample_rate || !standard_config || !get_float ||
        !get_noninterleaved || !is_plugin_registered ||
        !get_event_from_playing || !get_game_object_from_playing ||
        !get_source_position) {
        g_probe_state.store(ProbeState::Failed, std::memory_order_release);
        Log("[music-input] resolved call target is null");
        return;
    }

    InstallNativeErrorLogger();
    EnableWwiseMonitorOutput();
    const bool plugin_registered = is_plugin_registered(kSourcePluginType,
        kAudiokineticCompanyId, kAudioInputPluginId,
        const_cast<void*>(g_ak_is_plugin_registered.resolved.method_info));
    Log("[music-input] IsPluginRegistered type=Source company=0 plugin=200 result=" +
        std::string(plugin_registered ? "true" : "false"));
    if (!plugin_registered) {
        g_probe_state.store(ProbeState::Failed, std::memory_order_release);
        return;
    }

    uint32_t loaded_bank_id = 0;
    const int32_t bank_result = load_bank(bank.data(),
        static_cast<uint32_t>(bank.size()), &loaded_bank_id,
        const_cast<void*>(g_ak_load_bank_memory_copy.resolved.method_info));
    Log("[music-input] LoadBankMemoryCopy result=" +
        std::to_string(bank_result) + " bankId=" +
        std::to_string(loaded_bank_id) + " bytes=" +
        std::to_string(bank.size()));
    if (bank_result != 1 || loaded_bank_id != kProbeBankId) {
        g_probe_state.store(ProbeState::Failed, std::memory_order_release);
        return;
    }

    if (!CreateProbeGameObject() || !RegisterProbeGameObject()) {
        g_probe_state.store(ProbeState::Failed, std::memory_order_release);
        return;
    }

    g_probe_sample_rate.store(std::max(get_sample_rate(
        const_cast<void*>(g_ak_get_sample_rate.resolved.method_info)), 1u));
    g_probe_channel_config = standard_config(kMonoSpeakerMask,
        const_cast<void*>(g_channel_config_standard.resolved.method_info));
    g_probe_sample_type = get_float(
        const_cast<void*>(g_ak_float_get.resolved.method_info));
    g_probe_interleave = get_noninterleaved(
        const_cast<void*>(g_ak_noninterleaved_get.resolved.method_info));
    if (!g_probe_channel_config) {
        g_probe_state.store(ProbeState::Failed, std::memory_order_release);
        Log("[music-input] mono channel configuration allocation failed");
        return;
    }

    const uint32_t playing_id = PostManagedAudioInputEvent();
    if (!playing_id) {
        g_probe_state.store(ProbeState::Failed, std::memory_order_release);
        Log("[music-input] PostAudioInputEvent returned zero playing ID");
        return;
    }
    if (!g_managed_callbacks_ready.load(std::memory_order_acquire)) {
        g_probe_state.store(ProbeState::Failed, std::memory_order_release);
        Log("[music-input] managed Audio Input callbacks are unavailable");
        return;
    }
    uint32_t captured = g_probe_playing_id.load(std::memory_order_acquire);
    if (!captured) {
        g_probe_playing_id.store(playing_id, std::memory_order_release);
    } else if (captured != playing_id) {
        g_probe_state.store(ProbeState::Failed, std::memory_order_release);
        Log("[music-input] callback playing ID did not match PostEvent result");
        return;
    }
    g_probe_state.store(ProbeState::Playing, std::memory_order_release);
    TrackPlayingId(playing_id);
    int32_t source_position = -1;
    const uint32_t resolved_event = get_event_from_playing(playing_id,
        const_cast<void*>(g_ak_get_event_from_playing.resolved.method_info));
    const uint64_t resolved_object = get_game_object_from_playing(playing_id,
        const_cast<void*>(g_ak_get_game_object_from_playing.resolved.method_info));
    const int32_t position_result = get_source_position(playing_id,
        &source_position, true,
        const_cast<void*>(g_ak_get_source_position.resolved.method_info));
    Log("[music-input] sine event posted playingId=" +
        std::to_string(playing_id) + " eventId=" +
        std::to_string(kProbeEventId) + " sampleRate=" +
        std::to_string(g_probe_sample_rate.load()) + " durationSeconds=" +
        std::to_string(kProbeDurationSeconds) + " queryEventId=" +
        std::to_string(resolved_event) + " queryGameObject=" +
        std::to_string(resolved_object) + " positionResult=" +
        std::to_string(position_result) + " positionMs=" +
        std::to_string(source_position));
}

uint32_t __fastcall PostMusicEventHook(void* instance, void* event_name,
    int32_t pre_action, void* method) {
    const std::string event = ManagedString(event_name);
    const MusicSnapshot before = Snapshot(instance);
    LogEvent("AudioMusicSystem.PostMusicEvent", "enter",
        "event=\"" + event + "\" preAction=" +
        std::to_string(pre_action) + " " + SnapshotText(before));
    const uint32_t result = g_original_post_music_event
        ? g_original_post_music_event(instance, event_name, pre_action, method) : 0;
    const MusicSnapshot after = Snapshot(instance);
    TrackPlayingId(result);
    TrackSnapshot(after);
    LogEvent("AudioMusicSystem.PostMusicEvent", "exit",
        "resultPlayingId=" + std::to_string(result) + " " + SnapshotText(after));
    if (result && event == "au_music_login") {
        StartSineProbe();
    }
    return result;
}

void __fastcall StartMusicEventHook(void* instance, void* event_name,
    uint32_t* playing_id, int32_t callback_type, void* callback, void* method) {
    const std::string event = ManagedString(event_name);
    const uint32_t before_id = playing_id ? *playing_id : 0;
    LogEvent("AudioMusicSystem._StartMusicWithEvent", "enter",
        "event=\"" + event + "\" refPlayingId=" +
        std::to_string(before_id) + " callbackType=" +
        std::to_string(callback_type) + " " + SnapshotText(Snapshot(instance)));
    if (g_original_start_music_event) {
        g_original_start_music_event(instance, event_name, playing_id,
            callback_type, callback, method);
    }
    const uint32_t after_id = playing_id ? *playing_id : 0;
    TrackPlayingId(after_id);
    const MusicSnapshot after = Snapshot(instance);
    TrackSnapshot(after);
    LogEvent("AudioMusicSystem._StartMusicWithEvent", "exit",
        "refPlayingId=" + std::to_string(after_id) + " " + SnapshotText(after));
    if (after_id && event == "au_music_login") {
        StartSineProbe();
    }
}

void __fastcall StopByIdHook(void* instance, uint32_t* playing_id, void* method) {
    const uint32_t before_id = playing_id ? *playing_id : 0;
    TrackPlayingId(before_id);
    LogEvent("AudioMusicSystem._StopMusicByPlayingId", "enter",
        "refPlayingId=" + std::to_string(before_id) + " " +
        SnapshotText(Snapshot(instance)));
    if (g_original_stop_by_id) {
        g_original_stop_by_id(instance, playing_id, method);
    }
    LogEvent("AudioMusicSystem._StopMusicByPlayingId", "exit",
        "refPlayingId=" + std::to_string(playing_id ? *playing_id : 0) + " " +
        SnapshotText(Snapshot(instance)));
}

#define DEFINE_LIFECYCLE_HOOK(function_name, source_name, original_name) \
    void __fastcall function_name(void* instance, void* method) { \
        const MusicSnapshot before = Snapshot(instance); \
        TrackSnapshot(before); \
        LogEvent(source_name, "enter", SnapshotText(before)); \
        if (original_name) { original_name(instance, method); } \
        const MusicSnapshot after = Snapshot(instance); \
        TrackSnapshot(after); \
        LogEvent(source_name, "exit", SnapshotText(after)); \
    }

DEFINE_LIFECYCLE_HOOK(PauseMusicHook, "AudioMusicSystem.PauseMusic", g_original_pause_music)
DEFINE_LIFECYCLE_HOOK(ResumeMusicHook, "AudioMusicSystem.ResumeMusic", g_original_resume_music)
DEFINE_LIFECYCLE_HOOK(StopMusicHook, "AudioMusicSystem.StopMusic", g_original_stop_music)
DEFINE_LIFECYCLE_HOOK(TimelinePauseHook, "AudioMusicSystem.OnTimelinePause", g_original_timeline_pause)
DEFINE_LIFECYCLE_HOOK(TimelineResumeHook, "AudioMusicSystem.OnTimelineResume", g_original_timeline_resume)
DEFINE_LIFECYCLE_HOOK(TimelineSkippedHook, "AudioMusicSystem.OnTimelineSkipped", g_original_timeline_skipped)
DEFINE_LIFECYCLE_HOOK(UnmuteAllHook, "AudioMusicSystem.UnmuteMusic/all", g_original_unmute_all)

void __fastcall MuteFullHook(void* instance, void* identifier,
    bool normal_loading, bool teleport_loading, bool exit_dialog, void* method) {
    LogEvent("AudioMusicSystem.MuteMusic/full", "enter",
        "identifier=\"" + ManagedString(identifier) + "\" normalLoading=" +
        (normal_loading ? "true" : "false") + " teleportLoading=" +
        (teleport_loading ? "true" : "false") + " exitDialog=" +
        (exit_dialog ? "true" : "false") + " " + SnapshotText(Snapshot(instance)));
    if (g_original_mute_full) {
        g_original_mute_full(instance, identifier, normal_loading,
            teleport_loading, exit_dialog, method);
    }
    LogEvent("AudioMusicSystem.MuteMusic/full", "exit",
        SnapshotText(Snapshot(instance)));
}

void __fastcall MuteSimpleHook(void* instance, void* identifier, void* method) {
    LogEvent("AudioMusicSystem.MuteMusic/simple", "enter",
        "identifier=\"" + ManagedString(identifier) + "\" " +
        SnapshotText(Snapshot(instance)));
    if (g_original_mute_simple) {
        g_original_mute_simple(instance, identifier, method);
    }
    LogEvent("AudioMusicSystem.MuteMusic/simple", "exit",
        SnapshotText(Snapshot(instance)));
}

void __fastcall UnmuteNamedHook(void* instance, void* identifier, void* method) {
    LogEvent("AudioMusicSystem.UnmuteMusic/named", "enter",
        "identifier=\"" + ManagedString(identifier) + "\" " +
        SnapshotText(Snapshot(instance)));
    if (g_original_unmute_named) {
        g_original_unmute_named(instance, identifier, method);
    }
    LogEvent("AudioMusicSystem.UnmuteMusic/named", "exit",
        SnapshotText(Snapshot(instance)));
}

#define DEFINE_STATE_SETTER_HOOK(function_name, source_name, original_name) \
    void __fastcall function_name(void* instance, int32_t state, void* method) { \
        LogEvent(source_name, "enter", "state=" + std::to_string(state)); \
        if (original_name) { original_name(instance, state, method); } \
        LogEvent(source_name, "exit", "state=" + std::to_string(state)); \
    }

DEFINE_STATE_SETTER_HOOK(SetMusicStateHook, "AudioMusicSystem._SetWwiseMusicState", g_original_set_music_state)
DEFINE_STATE_SETTER_HOOK(SetMapStateHook, "AudioMusicSystem._SetWwiseMusicMapState", g_original_set_map_state)
DEFINE_STATE_SETTER_HOOK(SetBattleStateHook, "AudioMusicSystem._SetWwiseBattleMusicState", g_original_set_battle_state)
DEFINE_STATE_SETTER_HOOK(SetIntensityStateHook, "AudioMusicSystem._SetWwiseBattleMusicIntensityState", g_original_set_intensity_state)
DEFINE_STATE_SETTER_HOOK(SetMissionStateHook, "AudioMusicSystem._SetWwiseMissionMusicState", g_original_set_mission_state)
DEFINE_STATE_SETTER_HOOK(SetDialogStateHook, "AudioMusicSystem._SetWwiseDialogMusicState", g_original_set_dialog_state)
DEFINE_STATE_SETTER_HOOK(SetCutsceneStateHook, "AudioMusicSystem._SetWwiseCutsceneMusicState", g_original_set_cutscene_state)
DEFINE_STATE_SETTER_HOOK(SetLoginStateHook, "AudioMusicSystem._SetWwiseLoginMenuMusicState", g_original_set_login_state)
DEFINE_STATE_SETTER_HOOK(SetMetaStateHook, "AudioMusicSystem._SetWwiseMetaMusicState", g_original_set_meta_state)
DEFINE_STATE_SETTER_HOOK(SetRemoteStateHook, "AudioMusicSystem._SetWwiseRemoteCommMusicState", g_original_set_remote_state)

int32_t __fastcall AkSetStateHook(uint32_t group, uint32_t state, void* method) {
    if (IsMusicStateGroup(group)) {
        char data[160]{};
        std::snprintf(data, sizeof(data),
            "group=%s groupId=0x%08X stateId=0x%08X",
            StateGroupName(group), group, state);
        LogEvent("AkSoundEngine.SetState", "enter", data);
    }
    const int32_t result = g_original_ak_set_state
        ? g_original_ak_set_state(group, state, method) : 0;
    if (IsMusicStateGroup(group)) {
        LogEvent("AkSoundEngine.SetState", "exit",
            "group=" + std::string(StateGroupName(group)) +
            " result=" + std::to_string(result));
    }
    return result;
}

void LogAkStop(std::string_view source, uint32_t playing_id,
    int32_t duration, int32_t curve) {
    if (!IsTrackedPlayingId(playing_id)) {
        return;
    }
    LogEvent(source, "enter", "playingId=" + std::to_string(playing_id) +
        " tracked=true durationMs=" + std::to_string(duration) +
        " curve=" + std::to_string(curve));
}

void __fastcall AkStopFullHook(uint32_t id, int32_t duration,
    int32_t curve, void* method) {
    LogAkStop("AkSoundEngine.StopPlayingID/full", id, duration, curve);
    if (g_original_ak_stop_full) {
        g_original_ak_stop_full(id, duration, curve, method);
    }
}

void __fastcall AkStopDurationHook(uint32_t id, int32_t duration, void* method) {
    LogAkStop("AkSoundEngine.StopPlayingID/duration", id, duration, -1);
    if (g_original_ak_stop_duration) {
        g_original_ak_stop_duration(id, duration, method);
    }
}

void __fastcall AkStopSimpleHook(uint32_t id, void* method) {
    LogAkStop("AkSoundEngine.StopPlayingID/simple", id, -1, -1);
    if (g_original_ak_stop_simple) {
        g_original_ak_stop_simple(id, method);
    }
}

void LogAkAction(std::string_view source, int32_t action, uint32_t playing_id,
    int32_t duration, int32_t curve) {
    if (!IsTrackedPlayingId(playing_id)) {
        return;
    }
    LogEvent(source, "enter", "action=" + std::string(ActionName(action)) +
        " actionValue=" + std::to_string(action) +
        " playingId=" + std::to_string(playing_id) +
        " tracked=true durationMs=" + std::to_string(duration) +
        " curve=" + std::to_string(curve));
}

void __fastcall AkActionFullHook(int32_t action, uint32_t id,
    int32_t duration, int32_t curve, void* method) {
    LogAkAction("AkSoundEngine.ExecuteActionOnPlayingID/full",
        action, id, duration, curve);
    if (g_original_ak_action_full) {
        g_original_ak_action_full(action, id, duration, curve, method);
    }
}

void __fastcall AkActionDurationHook(int32_t action, uint32_t id,
    int32_t duration, void* method) {
    LogAkAction("AkSoundEngine.ExecuteActionOnPlayingID/duration",
        action, id, duration, -1);
    if (g_original_ak_action_duration) {
        g_original_ak_action_duration(action, id, duration, method);
    }
}

void __fastcall AkActionSimpleHook(int32_t action, uint32_t id, void* method) {
    LogAkAction("AkSoundEngine.ExecuteActionOnPlayingID/simple",
        action, id, -1, -1);
    if (g_original_ak_action_simple) {
        g_original_ak_action_simple(action, id, method);
    }
}

void __fastcall AkMuteBackgroundHook(bool mute, void* method) {
    LogEvent("AkSoundEngine.MuteBackgroundMusic", "enter",
        std::string("mute=") + (mute ? "true" : "false"));
    if (g_original_ak_mute_background) {
        g_original_ak_mute_background(mute, method);
    }
}

bool ResolveFields() {
    return Resolve(g_login_id_field) && Resolve(g_meta_id_field) &&
        Resolve(g_gameplay_id_field) && Resolve(g_paused_field);
}

bool InstallHooks() {
    bool required_ok = true;
    required_ok &= Resolve(g_ak_load_bank_memory_copy);
    required_ok &= Resolve(g_audio_input_post_event);
    required_ok &= Resolve(g_ak_get_sample_rate);
    required_ok &= Resolve(g_ak_register_game_object);
    required_ok &= Resolve(g_game_object_ctor);
    required_ok &= Resolve(g_channel_config_standard);
    required_ok &= Resolve(g_audio_format_set_all);
    required_ok &= Resolve(g_ak_float_get);
    required_ok &= Resolve(g_ak_noninterleaved_get);
    required_ok &= Resolve(g_ak_is_plugin_registered);
    required_ok &= Resolve(g_ak_get_event_from_playing);
    required_ok &= Resolve(g_ak_get_game_object_from_playing);
    required_ok &= Resolve(g_ak_get_source_position);
    required_ok &= Resolve(g_pinvoke_set_error_logger);
    required_ok &= Resolve(g_callback_manager_set_local_output);
    LogMethodCode(g_callback_manager_set_local_output.key,
        g_callback_manager_set_local_output.resolved.method_pointer);
    if (Resolve(g_callback_serializer_set_local_output_api, false)) {
        LogMethodCode(g_callback_serializer_set_local_output_api.key,
            g_callback_serializer_set_local_output_api.resolved.method_pointer);
    }
    if (Resolve(g_callback_manager_set_monitoring_callback, false)) {
        LogMethodCode(g_callback_manager_set_monitoring_callback.key,
            g_callback_manager_set_monitoring_callback.resolved.method_pointer);
    }
    required_ok &= Hook(g_audio_input_samples,
        reinterpret_cast<void*>(&AudioInputSamplesHook),
        reinterpret_cast<void**>(&g_original_audio_input_samples));
    required_ok &= Hook(g_audio_input_format,
        reinterpret_cast<void*>(&AudioInputFormatHook),
        reinterpret_cast<void**>(&g_original_audio_input_format));
    required_ok &= Hook(g_audio_input_try_initialize,
        reinterpret_cast<void*>(&AudioInputTryInitializeHook),
        reinterpret_cast<void**>(&g_original_audio_input_try_initialize));
    required_ok &= Hook(g_ak_set_audio_input_callbacks,
        reinterpret_cast<void*>(&SetAudioInputCallbacksHook),
        reinterpret_cast<void**>(&g_original_set_audio_input_callbacks));
    required_ok &= Hook(g_callback_serializer_set_local_output,
        reinterpret_cast<void*>(&CallbackSerializerSetLocalOutputHook),
        reinterpret_cast<void**>(
            &g_original_callback_serializer_set_local_output));
    required_ok &= Hook(g_post_music_event,
        reinterpret_cast<void*>(&PostMusicEventHook),
        reinterpret_cast<void**>(&g_original_post_music_event));
    required_ok &= Hook(g_start_music_event,
        reinterpret_cast<void*>(&StartMusicEventHook),
        reinterpret_cast<void**>(&g_original_start_music_event));
    required_ok &= Hook(g_stop_by_id, reinterpret_cast<void*>(&StopByIdHook),
        reinterpret_cast<void**>(&g_original_stop_by_id));
    required_ok &= Hook(g_pause_music, reinterpret_cast<void*>(&PauseMusicHook),
        reinterpret_cast<void**>(&g_original_pause_music));
    required_ok &= Hook(g_resume_music, reinterpret_cast<void*>(&ResumeMusicHook),
        reinterpret_cast<void**>(&g_original_resume_music));
    required_ok &= Hook(g_stop_music, reinterpret_cast<void*>(&StopMusicHook),
        reinterpret_cast<void**>(&g_original_stop_music));

#define OPTIONAL_HOOK(method, detour, original) \
    Hook(method, reinterpret_cast<void*>(detour), \
        reinterpret_cast<void**>(original), false)

    OPTIONAL_HOOK(g_timeline_pause, &TimelinePauseHook, &g_original_timeline_pause);
    OPTIONAL_HOOK(g_timeline_resume, &TimelineResumeHook, &g_original_timeline_resume);
    OPTIONAL_HOOK(g_timeline_skipped, &TimelineSkippedHook, &g_original_timeline_skipped);
    OPTIONAL_HOOK(g_mute_full, &MuteFullHook, &g_original_mute_full);
    OPTIONAL_HOOK(g_mute_simple, &MuteSimpleHook, &g_original_mute_simple);
    OPTIONAL_HOOK(g_unmute_named, &UnmuteNamedHook, &g_original_unmute_named);
    OPTIONAL_HOOK(g_unmute_all, &UnmuteAllHook, &g_original_unmute_all);
    OPTIONAL_HOOK(g_set_music_state, &SetMusicStateHook, &g_original_set_music_state);
    OPTIONAL_HOOK(g_set_map_state, &SetMapStateHook, &g_original_set_map_state);
    OPTIONAL_HOOK(g_set_battle_state, &SetBattleStateHook, &g_original_set_battle_state);
    OPTIONAL_HOOK(g_set_intensity_state, &SetIntensityStateHook, &g_original_set_intensity_state);
    OPTIONAL_HOOK(g_set_mission_state, &SetMissionStateHook, &g_original_set_mission_state);
    OPTIONAL_HOOK(g_set_dialog_state, &SetDialogStateHook, &g_original_set_dialog_state);
    OPTIONAL_HOOK(g_set_cutscene_state, &SetCutsceneStateHook, &g_original_set_cutscene_state);
    OPTIONAL_HOOK(g_set_login_state, &SetLoginStateHook, &g_original_set_login_state);
    OPTIONAL_HOOK(g_set_meta_state, &SetMetaStateHook, &g_original_set_meta_state);
    OPTIONAL_HOOK(g_set_remote_state, &SetRemoteStateHook, &g_original_set_remote_state);
    OPTIONAL_HOOK(g_ak_set_state, &AkSetStateHook, &g_original_ak_set_state);
    OPTIONAL_HOOK(g_ak_stop_full, &AkStopFullHook, &g_original_ak_stop_full);
    OPTIONAL_HOOK(g_ak_stop_duration, &AkStopDurationHook, &g_original_ak_stop_duration);
    OPTIONAL_HOOK(g_ak_stop_simple, &AkStopSimpleHook, &g_original_ak_stop_simple);
    OPTIONAL_HOOK(g_ak_action_full, &AkActionFullHook, &g_original_ak_action_full);
    OPTIONAL_HOOK(g_ak_action_duration, &AkActionDurationHook, &g_original_ak_action_duration);
    OPTIONAL_HOOK(g_ak_action_simple, &AkActionSimpleHook, &g_original_ak_action_simple);
    OPTIONAL_HOOK(g_ak_mute_background, &AkMuteBackgroundHook, &g_original_ak_mute_background);

    if (!required_ok) {
        g_host->release_module_hooks(g_host->context, kModuleId);
        Log("[music-hook] required lifecycle contract incomplete; hooks released");
        return false;
    }
    g_hooks_installed = true;
    Log("[music-hook] lifecycle observation and Audio Input sine probe active");
    return true;
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->log || !host->resolve_method || !host->resolve_field ||
        !host->create_hook || !host->release_module_hooks ||
        !host->copy_managed_string || !host->resolve_class ||
        !host->object_new || !host->string_new || !host->runtime_invoke ||
        !host->object_unbox || !host->gchandle_new || !host->gchandle_free) {
        return BE_Result_ContractMismatch;
    }
    g_host = host;
    const HMODULE game_assembly = GetModuleHandleW(L"GameAssembly.dll");
    if (!game_assembly) {
        return BE_Result_NotReady;
    }
    g_il2cpp_domain_get = reinterpret_cast<Il2CppDomainGetFn>(
        GetProcAddress(game_assembly, "il2cpp_domain_get"));
    g_il2cpp_domain_get_assemblies =
        reinterpret_cast<Il2CppDomainGetAssembliesFn>(
            GetProcAddress(game_assembly, "il2cpp_domain_get_assemblies"));
    g_il2cpp_assembly_get_image =
        reinterpret_cast<Il2CppAssemblyGetImageFn>(
            GetProcAddress(game_assembly, "il2cpp_assembly_get_image"));
    g_il2cpp_image_get_name = reinterpret_cast<Il2CppImageGetNameFn>(
        GetProcAddress(game_assembly, "il2cpp_image_get_name"));
    g_il2cpp_class_from_name = reinterpret_cast<Il2CppClassFromNameFn>(
        GetProcAddress(game_assembly, "il2cpp_class_from_name"));
    g_il2cpp_class_get_name = reinterpret_cast<Il2CppClassGetNameFn>(
        GetProcAddress(game_assembly, "il2cpp_class_get_name"));
    g_il2cpp_class_get_nested_types =
        reinterpret_cast<Il2CppClassGetNestedTypesFn>(
            GetProcAddress(game_assembly, "il2cpp_class_get_nested_types"));
    g_il2cpp_class_get_methods = reinterpret_cast<Il2CppClassGetMethodsFn>(
        GetProcAddress(game_assembly, "il2cpp_class_get_methods"));
    g_il2cpp_class_get_fields = reinterpret_cast<Il2CppClassGetFieldsFn>(
        GetProcAddress(game_assembly, "il2cpp_class_get_fields"));
    g_il2cpp_method_get_name = reinterpret_cast<Il2CppMethodGetNameFn>(
        GetProcAddress(game_assembly, "il2cpp_method_get_name"));
    g_il2cpp_method_get_param_count =
        reinterpret_cast<Il2CppMethodGetParamCountFn>(
            GetProcAddress(game_assembly, "il2cpp_method_get_param_count"));
    g_il2cpp_method_get_param = reinterpret_cast<Il2CppMethodGetParamFn>(
        GetProcAddress(game_assembly, "il2cpp_method_get_param"));
    g_il2cpp_method_get_return_type =
        reinterpret_cast<Il2CppMethodGetReturnTypeFn>(
            GetProcAddress(game_assembly, "il2cpp_method_get_return_type"));
    g_il2cpp_type_get_name = reinterpret_cast<Il2CppTypeGetNameFn>(
        GetProcAddress(game_assembly, "il2cpp_type_get_name"));
    g_il2cpp_field_get_name = reinterpret_cast<Il2CppFieldGetNameFn>(
        GetProcAddress(game_assembly, "il2cpp_field_get_name"));
    g_il2cpp_field_get_type = reinterpret_cast<Il2CppFieldGetTypeFn>(
        GetProcAddress(game_assembly, "il2cpp_field_get_type"));
    g_il2cpp_field_get_flags = reinterpret_cast<Il2CppFieldGetFlagsFn>(
        GetProcAddress(game_assembly, "il2cpp_field_get_flags"));
    g_il2cpp_field_get_offset = reinterpret_cast<Il2CppFieldGetOffsetFn>(
        GetProcAddress(game_assembly, "il2cpp_field_get_offset"));
    g_il2cpp_field_static_get_value =
        reinterpret_cast<Il2CppFieldStaticGetValueFn>(
            GetProcAddress(game_assembly, "il2cpp_field_static_get_value"));
    g_il2cpp_thread_current = reinterpret_cast<Il2CppThreadCurrentFn>(
        GetProcAddress(game_assembly, "il2cpp_thread_current"));
    g_il2cpp_thread_attach = reinterpret_cast<Il2CppThreadAttachFn>(
        GetProcAddress(game_assembly, "il2cpp_thread_attach"));
    g_il2cpp_thread_detach = reinterpret_cast<Il2CppThreadDetachFn>(
        GetProcAddress(game_assembly, "il2cpp_thread_detach"));
    if (!g_il2cpp_domain_get || !g_il2cpp_thread_current ||
        !g_il2cpp_thread_attach || !g_il2cpp_thread_detach) {
        return BE_Result_ContractMismatch;
    }
    const BE_Result game_object_status = g_host->resolve_class(
        g_host->context, kUnityCoreAssembly, "UnityEngine", "GameObject",
        &g_probe_game_object_class);
    if (game_object_status != BE_Result_Ok ||
        !g_probe_game_object_class.class_info) {
        Log("[music-input] UnityEngine.GameObject class unavailable result=" +
            std::string(ResultName(game_object_status)));
        return BE_Result_ContractMismatch;
    }
    Log("[music-probe] version=0.7.8 mode=registered-audio-input-object");
    LogAudioInputContracts();
    LogMonitorMethodContracts("AkSoundEngine");
    LogMonitorMethodContracts("AkSoundEnginePINVOKE");
    LogMonitorMethodContracts("AkCallbackManager");
    LogMonitorMethodContracts("AkCallbackSerializer");
    LogMonitorMethodContracts("AkLogger");
    LogMonitorNestedContracts();
    if (!ResolveFields()) {
        return BE_Result_ContractMismatch;
    }
    return InstallHooks() ? BE_Result_Ok : BE_Result_ContractMismatch;
}

BE_Result BE_CALL ConfigurationChanged(const char*) {
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    if (g_hooks_installed && g_host && g_host->release_module_hooks) {
        g_host->release_module_hooks(g_host->context, kModuleId);
    }
    g_hooks_installed = false;
    if (g_probe_game_object_handle && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, g_probe_game_object_handle);
        g_probe_game_object_handle = 0;
        g_probe_game_object = nullptr;
    }
    Log("[music-input] shutdown state=" + std::string(ProbeStateName(
        g_probe_state.load())) + " playingId=" +
        std::to_string(g_probe_playing_id.load()) + " callbacks=" +
        std::to_string(g_probe_sample_callbacks.load()) + " frames=" +
        std::to_string(g_probe_frames.load()));
    Log("[music-hook] lifecycle observation and Audio Input sine probe stopped");
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Better Endfield Music Probe", "0.7.8",
        BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown,
};

} // namespace

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1(void) {
    return &kApi;
}
