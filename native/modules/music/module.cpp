#include <BetterEndfield/ModuleApi.h>

#include "music_bank.h"
#include "omni_pcm_abi.h"

#include <Windows.h>

#include <algorithm>
#include <array>
#include <atomic>
#include <chrono>
#include <cmath>
#include <cctype>
#include <cstdint>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <filesystem>
#include <memory>
#include <mutex>
#include <string>
#include <string_view>
#include <thread>
#include <unordered_map>
#include <utility>
#include <vector>

namespace {

using namespace BetterEndfield::Music;
namespace Omni = BetterEndfield::Music::Omni;

constexpr const char* kModuleId = "betterendfield.music";
constexpr const char* kGameplayAssembly = "Gameplay.Beyond.dll";
constexpr const char* kMusicNamespace = "Beyond.Gameplay.Audio";
constexpr const char* kMusicClass = "AudioMusicSystem";
constexpr const char* kWwiseAssembly = "AK.Wwise.Unity.API.dll";
constexpr const char* kAudioInputClass = "AkAudioInputManager";
constexpr const char* kWwisePInvokeClass = "AkSoundEnginePINVOKE";
constexpr const char* kUnityCoreAssembly = "UnityEngine.CoreModule.dll";
constexpr uint32_t kStereoSpeakerMask = 0x3u;
constexpr int32_t kSourcePluginType = 2;
constexpr uint32_t kAudiokineticCompanyId = 0u;
constexpr uint32_t kAudioInputPluginId = 200u;
constexpr uint32_t kRingCapacityFrames = 131072u;
constexpr uint32_t kMaximumCallbackFrames = 16384u;
constexpr uint32_t kWorkerReadFrames = 2048u;
constexpr uint64_t kSourceRetryDelayMs = 2000u;
constexpr uint32_t kAudibleOutputDelayMs = 100u;
constexpr uint64_t kCallbackDrainTimeoutMs = 2000u;

const BE_HostApiV1* g_host = nullptr;
bool g_hooks_installed = false;

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

std::string Trim(std::string value) {
    const auto whitespace = [](unsigned char character) {
        return std::isspace(character) != 0;
    };
    value.erase(value.begin(), std::find_if_not(value.begin(), value.end(), whitespace));
    value.erase(std::find_if_not(value.rbegin(), value.rend(), whitespace).base(),
        value.end());
    return value;
}

std::wstring Utf8ToWide(const std::string& value) {
    if (value.empty()) {
        return {};
    }
    const int size = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS,
        value.data(), static_cast<int>(value.size()), nullptr, 0);
    if (size <= 0) {
        return {};
    }
    std::wstring output(static_cast<size_t>(size), L'\0');
    MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, value.data(),
        static_cast<int>(value.size()), output.data(), size);
    return output;
}

std::string WideToUtf8(const std::wstring& value) {
    if (value.empty()) {
        return {};
    }
    const int size = WideCharToMultiByte(CP_UTF8, 0, value.data(),
        static_cast<int>(value.size()), nullptr, 0, nullptr, nullptr);
    if (size <= 0) {
        return {};
    }
    std::string output(static_cast<size_t>(size), '\0');
    WideCharToMultiByte(CP_UTF8, 0, value.data(),
        static_cast<int>(value.size()), output.data(), size, nullptr, nullptr);
    return output;
}

struct MusicConfig {
    bool enabled = false;
    std::wstring backend_exe;
    std::string client_id;
    bool replace_login = true;
    bool replace_meta = true;
    bool replace_gameplay = true;
    double target_latency = 0.4;
    uint32_t prebuffer_ms = 150;
    bool fallback_to_native = true;
    bool diagnostics = false;
};

std::mutex g_config_mutex;
MusicConfig g_config;
std::atomic<uint64_t> g_config_generation{0};
std::atomic<bool> g_enabled{false};
std::atomic<bool> g_replace_login{true};
std::atomic<bool> g_replace_meta{true};
std::atomic<bool> g_replace_gameplay{true};
std::atomic<bool> g_fallback_to_native{true};

bool ParseBool(const std::unordered_map<std::string, std::string>& values,
    const char* key, bool fallback) {
    const auto found = values.find(key);
    if (found == values.end()) {
        return fallback;
    }
    std::string value = found->second;
    std::transform(value.begin(), value.end(), value.begin(),
        [](unsigned char character) {
            return static_cast<char>(std::tolower(character));
        });
    return value == "true" || value == "1" || value == "yes" || value == "on";
}

double ParseNumber(const std::unordered_map<std::string, std::string>& values,
    const char* key, double fallback, double minimum, double maximum) {
    const auto found = values.find(key);
    if (found == values.end()) {
        return fallback;
    }
    char* end = nullptr;
    const double result = std::strtod(found->second.c_str(), &end);
    return end != found->second.c_str() && std::isfinite(result)
        ? std::clamp(result, minimum, maximum)
        : fallback;
}

MusicConfig ParseConfiguration(const char* configuration) {
    std::unordered_map<std::string, std::string> values;
    std::string_view source = configuration ? configuration : "";
    while (!source.empty()) {
        const size_t newline = source.find('\n');
        std::string line(source.substr(0, newline));
        if (newline == std::string_view::npos) {
            source = {};
        } else {
            source.remove_prefix(newline + 1);
        }
        line = Trim(std::move(line));
        if (line.empty() || line.front() == ';' || line.front() == '#') {
            continue;
        }
        const size_t separator = line.find('=');
        if (separator == std::string::npos || separator == 0) {
            continue;
        }
        std::string key = Trim(line.substr(0, separator));
        std::transform(key.begin(), key.end(), key.begin(),
            [](unsigned char character) {
                return static_cast<char>(std::tolower(character));
            });
        values[std::move(key)] = Trim(line.substr(separator + 1));
    }

    MusicConfig result;
    result.enabled = ParseBool(values, "enabled", false);
    if (const auto found = values.find("backend_exe"); found != values.end()) {
        result.backend_exe = Utf8ToWide(found->second);
    }
    if (const auto found = values.find("client_id"); found != values.end()) {
        result.client_id = found->second;
    }
    result.replace_login = ParseBool(values, "replace_login", true);
    result.replace_meta = ParseBool(values, "replace_meta", true);
    result.replace_gameplay = ParseBool(values, "replace_gameplay", true);
    result.target_latency = ParseNumber(
        values, "target_latency", 0.4, 0.1, 2.0);
    result.prebuffer_ms = static_cast<uint32_t>(ParseNumber(
        values, "prebuffer_ms", 150.0, 50.0, 1000.0));
    result.fallback_to_native = ParseBool(values, "fallback_to_native", true);
    result.diagnostics = ParseBool(values, "diagnostics", false);
    return result;
}

MusicConfig CopyConfiguration() {
    std::scoped_lock lock(g_config_mutex);
    return g_config;
}

class StereoFrameRing {
public:
    bool Initialize(uint32_t capacity_frames) {
        if (!capacity_frames) {
            return false;
        }
        samples_ = std::make_unique<float[]>(
            static_cast<size_t>(capacity_frames) * 2u);
        capacity_ = capacity_frames;
        read_.store(0, std::memory_order_release);
        write_.store(0, std::memory_order_release);
        return samples_ != nullptr;
    }

    uint64_t AvailableRead() const noexcept {
        const uint64_t write = write_.load(std::memory_order_acquire);
        const uint64_t read = read_.load(std::memory_order_acquire);
        return write >= read ? std::min<uint64_t>(write - read, capacity_) : 0;
    }

    uint64_t AvailableWrite() const noexcept {
        return capacity_ - std::min<uint64_t>(AvailableRead(), capacity_);
    }

    uint32_t Push(const float* interleaved, uint32_t frames) noexcept {
        if (!samples_ || !interleaved || !frames) {
            return 0;
        }
        const uint64_t write = write_.load(std::memory_order_relaxed);
        const uint64_t read = read_.load(std::memory_order_acquire);
        const uint64_t used = write >= read ? std::min<uint64_t>(write - read, capacity_) : 0;
        const uint32_t writable = static_cast<uint32_t>(
            std::min<uint64_t>(frames, capacity_ - used));
        for (uint32_t frame = 0; frame < writable; ++frame) {
            const size_t destination = static_cast<size_t>(
                (write + frame) % capacity_) * 2u;
            samples_[destination] = interleaved[static_cast<size_t>(frame) * 2u];
            samples_[destination + 1] = interleaved[static_cast<size_t>(frame) * 2u + 1u];
        }
        write_.store(write + writable, std::memory_order_release);
        return writable;
    }

    uint32_t Pop(float* interleaved, uint32_t frames) noexcept {
        if (!samples_ || !interleaved || !frames) {
            return 0;
        }
        uint64_t read = read_.load(std::memory_order_acquire);
        const uint64_t write = write_.load(std::memory_order_acquire);
        const uint32_t readable = static_cast<uint32_t>(std::min<uint64_t>(
            frames, write >= read ? std::min<uint64_t>(write - read, capacity_) : 0));
        for (uint32_t frame = 0; frame < readable; ++frame) {
            const size_t source = static_cast<size_t>(
                (read + frame) % capacity_) * 2u;
            interleaved[static_cast<size_t>(frame) * 2u] = samples_[source];
            interleaved[static_cast<size_t>(frame) * 2u + 1u] = samples_[source + 1u];
        }
        if (!read_.compare_exchange_strong(read, read + readable,
                std::memory_order_acq_rel)) {
            return 0;
        }
        return readable;
    }

    void Clear() noexcept {
        read_.store(write_.load(std::memory_order_acquire),
            std::memory_order_release);
    }

private:
    std::unique_ptr<float[]> samples_;
    uint64_t capacity_ = 0;
    std::atomic<uint64_t> read_{0};
    std::atomic<uint64_t> write_{0};
};

StereoFrameRing g_ring;

class LinearStereoResampler {
public:
    void Configure(int32_t sample_rate, int32_t channels, uint32_t output_rate) {
        source_rate_ = std::max(sample_rate, 1);
        channels_ = std::max(channels, 1);
        output_rate_ = std::max(output_rate, 1u);
        input_cursor_ = 0;
        next_output_position_ = 0.0;
        previous_left_ = 0.0f;
        previous_right_ = 0.0f;
        has_previous_ = false;
    }

    void Convert(const float* source, uint32_t frames, std::vector<float>& output) {
        output.clear();
        if (!source || !frames) {
            return;
        }
        const double step = static_cast<double>(source_rate_) /
            static_cast<double>(output_rate_);
        for (uint32_t frame = 0; frame < frames; ++frame) {
            const float left = source[static_cast<size_t>(frame) * channels_];
            const float right = channels_ > 1
                ? source[static_cast<size_t>(frame) * channels_ + 1u]
                : left;
            const double current_position = static_cast<double>(input_cursor_++);
            if (!has_previous_) {
                previous_left_ = left;
                previous_right_ = right;
                has_previous_ = true;
                output.push_back(left);
                output.push_back(right);
                next_output_position_ = step;
                continue;
            }
            const double previous_position = current_position - 1.0;
            while (next_output_position_ <= current_position + 1e-9) {
                const double mix = std::clamp(
                    next_output_position_ - previous_position, 0.0, 1.0);
                output.push_back(previous_left_ +
                    (left - previous_left_) * static_cast<float>(mix));
                output.push_back(previous_right_ +
                    (right - previous_right_) * static_cast<float>(mix));
                next_output_position_ += step;
            }
            previous_left_ = left;
            previous_right_ = right;
        }
    }

private:
    int32_t source_rate_ = 48000;
    int32_t channels_ = 2;
    uint32_t output_rate_ = 48000;
    int64_t input_cursor_ = 0;
    double next_output_position_ = 0.0;
    float previous_left_ = 0.0f;
    float previous_right_ = 0.0f;
    bool has_previous_ = false;
};

class AudibleCursorDelay {
public:
    void Reset(int64_t initial_cursor = 0) noexcept {
        next_ = 0;
        count_ = 0;
        initial_cursor_ = std::max<int64_t>(0, initial_cursor);
    }

    int64_t Observe(uint64_t tick, int64_t rendered_cursor,
        uint32_t delay_ms) noexcept {
        history_[next_] = {tick, std::max<int64_t>(0, rendered_cursor)};
        next_ = (next_ + 1u) % history_.size();
        count_ = std::min(count_ + 1u, history_.size());
        if (tick < delay_ms) {
            return initial_cursor_;
        }
        const uint64_t target_tick = tick - delay_ms;
        for (size_t offset = 0; offset < count_; ++offset) {
            const size_t index = (next_ + history_.size() - 1u - offset) %
                history_.size();
            if (history_[index].tick <= target_tick) {
                return history_[index].cursor;
            }
        }
        return initial_cursor_;
    }

private:
    struct Point {
        uint64_t tick = 0;
        int64_t cursor = 0;
    };

    std::array<Point, 64> history_{};
    size_t next_ = 0;
    size_t count_ = 0;
    int64_t initial_cursor_ = 0;
};

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

bool Resolve(RuntimeMethod& method, bool required = true) {
    const BE_Result status = g_host->resolve_method(
        g_host->context, &method.descriptor, &method.resolved);
    if (status == BE_Result_Ok && method.resolved.method_pointer) {
        return true;
    }
    Log(std::string("[music.contract] ") + (required ? "required" : "optional") +
        " method unavailable=" + method.key + " result=" + ResultName(status));
    return false;
}

bool Resolve(RuntimeField& field) {
    BE_ResolvedFieldV1 result{};
    const BE_Result status = g_host->resolve_field(
        g_host->context, &field.descriptor, &result);
    if (status == BE_Result_Ok && result.offset >= 0) {
        field.offset = result.offset;
        return true;
    }
    Log(std::string("[music.contract] field unavailable=") + field.key +
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
        return true;
    }
    Log(std::string("[music.contract] ") + (required ? "required" : "optional") +
        " hook failed=" + method.key + " result=" + ResultName(status));
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

RuntimeMethod g_main_thread_exec{
    "UnitySynchronizationContext.Exec",
    {kUnityCoreAssembly, "UnityEngine", "UnitySynchronizationContext",
        "Exec", "", "System.Void", 0}};

AUDIO_INPUT_METHOD(g_audio_input_try_initialize,
    "AkAudioInputManager.TryInitialize", "TryInitialize", "", "System.Void", 0);
AUDIO_INPUT_METHOD(g_audio_input_post_event,
    "AkAudioInputManager.PostAudioInputEvent", "PostAudioInputEvent",
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
    "AkSoundEngine.RegisterGameObj", "RegisterGameObj",
    "UnityEngine.GameObject", "AKRESULT", 1);
WWISE_METHOD(g_ak_execute_action,
    "AkSoundEngine.ExecuteActionOnPlayingID",
    "ExecuteActionOnPlayingID", "AkActionOnEventType|System.UInt32",
    "System.Void", 2);
WWISE_METHOD(g_ak_is_plugin_registered,
    "AkSoundEngine.IsPluginRegistered", "IsPluginRegistered",
    "AkPluginType|System.UInt32|System.UInt32", "System.Boolean", 3);

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

using VoidInstanceFn = void(__fastcall*)(void*, void*);
using MainThreadExecFn = void(__fastcall*)(void*, void*);
using PostMusicEventFn = uint32_t(__fastcall*)(void*, void*, int32_t, void*);
using StartMusicEventFn = void(__fastcall*)(
    void*, void*, uint32_t*, int32_t, void*, void*);
using StopByIdFn = void(__fastcall*)(void*, uint32_t*, void*);
using AudioInputSamplesFn = bool(__fastcall*)(
    uint32_t, void*, uint32_t, uint32_t, void*);
using AudioInputFormatFn = void(__fastcall*)(uint32_t, void*, void*);
using TryInitializeFn = void(__fastcall*)(void*);
using SetAudioInputCallbacksFn = void(__fastcall*)(void*, void*, void*);
using LoadBankMemoryCopyFn = int32_t(__fastcall*)(
    void*, uint32_t, uint32_t*, void*);
using GetSampleRateFn = uint32_t(__fastcall*)(void*);
using ChannelConfigStandardFn = void*(__fastcall*)(uint32_t, void*);
using AudioFormatSetAllFn = void(__fastcall*)(
    void*, uint32_t, void*, uint32_t, uint32_t, uint32_t, uint32_t, void*);
using GetUInt16ConstantFn = uint16_t(__fastcall*)(void*);
using GetByteConstantFn = uint8_t(__fastcall*)(void*);
using IsPluginRegisteredFn = bool(__fastcall*)(
    int32_t, uint32_t, uint32_t, void*);
using ExecuteActionFn = void(__fastcall*)(int32_t, uint32_t, void*);

MainThreadExecFn g_original_main_thread_exec = nullptr;
PostMusicEventFn g_original_post_music_event = nullptr;
StartMusicEventFn g_original_start_music_event = nullptr;
StopByIdFn g_original_stop_by_id = nullptr;
VoidInstanceFn g_original_pause_music = nullptr;
VoidInstanceFn g_original_resume_music = nullptr;
VoidInstanceFn g_original_stop_music = nullptr;
VoidInstanceFn g_original_timeline_pause = nullptr;
VoidInstanceFn g_original_timeline_resume = nullptr;
AudioInputSamplesFn g_original_audio_input_samples = nullptr;
AudioInputFormatFn g_original_audio_input_format = nullptr;
TryInitializeFn g_original_audio_input_try_initialize = nullptr;
SetAudioInputCallbacksFn g_original_set_audio_input_callbacks = nullptr;

std::array<std::atomic<uint32_t>, 3> g_native_playing_ids{};
std::array<uint32_t, 3> g_policy_paused_ids{};
std::atomic<bool> g_game_pause_requested{false};
std::atomic<bool> g_session_alive{false};
std::atomic<bool> g_stream_active{false};
std::atomic<bool> g_stream_paused{false};
std::atomic<bool> g_pcm_prebuffered{false};
std::atomic<bool> g_stream_activation_pending{false};
std::atomic<int64_t> g_stream_id{0};
std::atomic<uint32_t> g_output_sample_rate{48000};
std::atomic<uint64_t> g_sample_callbacks{0};
std::atomic<uint64_t> g_sample_frames{0};
std::atomic<uint64_t> g_underflow_callbacks{0};
std::atomic<uint64_t> g_last_audio_data_tick{0};
std::atomic<uint64_t> g_last_sample_callback_tick{0};
std::atomic<bool> g_format_seen{false};
std::atomic<bool> g_callbacks_ready{false};
std::atomic<uint32_t> g_custom_playing_id{0};
std::atomic<uint32_t> g_source_state{0}; // 0 idle, 1 starting, 2 running, 3 failed
std::atomic<uint64_t> g_source_started_tick{0};
std::atomic<uint64_t> g_source_retry_after_tick{0};
std::atomic<uint64_t> g_source_callback_baseline{0};
std::atomic<uint32_t> g_audio_callbacks_inflight{0};
std::atomic<bool> g_audio_callbacks_stopping{false};
std::atomic<bool> g_shutdown_requested{false};
std::atomic<bool> g_worker_started{false};
std::thread g_worker;

BE_ResolvedClassV1 g_game_object_class{};
void* g_audio_game_object = nullptr;
uint32_t g_audio_game_object_handle = 0;
void* g_channel_config = nullptr;
uint32_t g_sample_type = 0;
uint32_t g_interleave = 0;
bool g_bank_loaded = false;
bool g_audio_game_object_registered = false;
std::atomic<bool> g_custom_output_enabled{false};
bool g_replacement_engaged = false;

std::unique_ptr<float[]> g_callback_scratch;
uint32_t g_callback_scratch_frames = 0;
uint32_t g_callback_seen_channels = 0;
uint32_t g_callback_scratch_playing_id = 0;

class AudioCallbackScope {
public:
    AudioCallbackScope() noexcept {
        g_audio_callbacks_inflight.fetch_add(1, std::memory_order_acq_rel);
    }

    ~AudioCallbackScope() {
        g_audio_callbacks_inflight.fetch_sub(1, std::memory_order_acq_rel);
    }
};

void ScheduleSourceRetry() noexcept {
    g_source_retry_after_tick.store(
        GetTickCount64() + kSourceRetryDelayMs, std::memory_order_release);
    g_source_state.store(3, std::memory_order_release);
}

template <typename T>
T ReadInstanceField(void* instance, int32_t offset, T fallback = {}) noexcept {
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

void UpdateNativeSnapshot(void* instance) noexcept {
    g_native_playing_ids[0].store(
        ReadInstanceField<uint32_t>(instance, g_login_id_field.offset),
        std::memory_order_release);
    g_native_playing_ids[1].store(
        ReadInstanceField<uint32_t>(instance, g_meta_id_field.offset),
        std::memory_order_release);
    g_native_playing_ids[2].store(
        ReadInstanceField<uint32_t>(instance, g_gameplay_id_field.offset),
        std::memory_order_release);
}

struct Il2CppArrayHeader {
    void* klass;
    void* monitor;
    void* bounds;
    uintptr_t max_length;
};

bool TryGetArrayStorage(void* samples, float** values,
    uintptr_t* capacity) noexcept {
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

bool IsCustomCallback(uint32_t playing_id) noexcept {
    if (!playing_id) {
        return false;
    }
    uint32_t current = g_custom_playing_id.load(std::memory_order_acquire);
    if (current == playing_id) {
        return true;
    }
    if (!current && g_source_state.load(std::memory_order_acquire) == 1) {
        g_custom_playing_id.compare_exchange_strong(
            current, playing_id, std::memory_order_acq_rel);
        return g_custom_playing_id.load(std::memory_order_acquire) == playing_id;
    }
    return false;
}

bool __fastcall AudioInputSamplesHook(uint32_t playing_id, void* samples,
    uint32_t channel_index, uint32_t frames, void* method) {
    AudioCallbackScope callback_scope;
    if (g_audio_callbacks_stopping.load(std::memory_order_acquire)) {
        const uint32_t custom = g_custom_playing_id.load(
            std::memory_order_acquire);
        if (custom && custom == playing_id) {
            return false;
        }
        return g_original_audio_input_samples
            ? g_original_audio_input_samples(
                playing_id, samples, channel_index, frames, method)
            : false;
    }
    if (!IsCustomCallback(playing_id)) {
        return g_original_audio_input_samples
            ? g_original_audio_input_samples(
                playing_id, samples, channel_index, frames, method)
            : false;
    }

    float* destination = nullptr;
    uintptr_t capacity = 0;
    if (!TryGetArrayStorage(samples, &destination, &capacity)) {
        return true;
    }
    const uint32_t writable = static_cast<uint32_t>(
        std::min<uintptr_t>(capacity, frames));
    g_last_sample_callback_tick.store(
        GetTickCount64(), std::memory_order_release);
    if (!g_custom_output_enabled.load(std::memory_order_acquire)) {
        std::fill_n(destination, capacity, 0.0f);
        g_callback_scratch_frames = 0;
        g_callback_seen_channels = 0;
        g_sample_callbacks.fetch_add(1, std::memory_order_relaxed);
        return true;
    }
    if (!g_callback_scratch || writable > kMaximumCallbackFrames ||
        channel_index > 1) {
        std::fill_n(destination, capacity, 0.0f);
        g_underflow_callbacks.fetch_add(1, std::memory_order_relaxed);
        return true;
    }

    const uint32_t channel_bit = 1u << channel_index;
    const bool need_block = g_callback_scratch_frames != writable ||
        g_callback_scratch_playing_id != playing_id ||
        (g_callback_seen_channels & channel_bit) != 0;
    uint32_t available = writable;
    if (need_block) {
        available = g_ring.Pop(g_callback_scratch.get(), writable);
        for (uint32_t frame = available; frame < writable; ++frame) {
            g_callback_scratch[static_cast<size_t>(frame) * 2u] = 0.0f;
            g_callback_scratch[static_cast<size_t>(frame) * 2u + 1u] = 0.0f;
        }
        g_callback_scratch_frames = writable;
        g_callback_scratch_playing_id = playing_id;
        g_callback_seen_channels = 0;
        g_sample_frames.fetch_add(available, std::memory_order_relaxed);
        if (available) {
            g_last_audio_data_tick.store(GetTickCount64(), std::memory_order_release);
        } else {
            g_underflow_callbacks.fetch_add(1, std::memory_order_relaxed);
        }
    }

    for (uint32_t frame = 0; frame < writable; ++frame) {
        destination[frame] = g_callback_scratch[
            static_cast<size_t>(frame) * 2u + channel_index];
    }
    for (uintptr_t frame = writable; frame < capacity; ++frame) {
        destination[frame] = 0.0f;
    }
    g_callback_seen_channels |= channel_bit;
    if ((g_callback_seen_channels & 0x3u) == 0x3u) {
        g_callback_scratch_frames = 0;
        g_callback_seen_channels = 0;
    }
    g_sample_callbacks.fetch_add(1, std::memory_order_relaxed);
    return true;
}

void __fastcall AudioInputFormatHook(
    uint32_t playing_id, void* format, void* method) {
    AudioCallbackScope callback_scope;
    if (g_audio_callbacks_stopping.load(std::memory_order_acquire)) {
        const uint32_t custom = g_custom_playing_id.load(
            std::memory_order_acquire);
        if ((!custom || custom != playing_id) && g_original_audio_input_format) {
            g_original_audio_input_format(playing_id, format, method);
        }
        return;
    }
    if (!IsCustomCallback(playing_id)) {
        if (g_original_audio_input_format) {
            g_original_audio_input_format(playing_id, format, method);
        }
        return;
    }
    const auto set_all = reinterpret_cast<AudioFormatSetAllFn>(
        g_audio_format_set_all.resolved.method_pointer);
    if (!format || !g_channel_config || !set_all) {
        ScheduleSourceRetry();
        return;
    }
    set_all(format, g_output_sample_rate.load(std::memory_order_acquire),
        g_channel_config, 32u, 8u, g_sample_type, g_interleave,
        const_cast<void*>(g_audio_format_set_all.resolved.method_info));
    g_format_seen.store(true, std::memory_order_release);
}

void __fastcall AudioInputTryInitializeHook(void* method) {
    if (g_original_audio_input_try_initialize) {
        g_original_audio_input_try_initialize(method);
    }
}

void __fastcall SetAudioInputCallbacksHook(
    void* samples, void* format, void* method) {
    if (g_original_set_audio_input_callbacks) {
        g_original_set_audio_input_callbacks(samples, format, method);
    }
    g_callbacks_ready.store(
        samples != nullptr && format != nullptr, std::memory_order_release);
}

bool CreateAudioGameObject() {
    if (g_audio_game_object) {
        return true;
    }
    if (!g_host || !g_host->object_new || !g_host->string_new ||
        !g_host->runtime_invoke || !g_host->gchandle_new ||
        !g_game_object_class.class_info || !g_game_object_ctor.resolved.method_info) {
        return false;
    }
    void* object = g_host->object_new(
        g_host->context, g_game_object_class.class_info);
    void* name = g_host->string_new(
        g_host->context, "BetterEndfield.OmniMixAudioInput");
    if (!object || !name) {
        return false;
    }
    void* parameters[1]{name};
    void* exception = nullptr;
    g_host->runtime_invoke(g_host->context,
        g_game_object_ctor.resolved.method_info, object, parameters, &exception);
    if (exception) {
        return false;
    }
    const uint32_t handle = g_host->gchandle_new(g_host->context, object, 0);
    if (!handle) {
        return false;
    }
    g_audio_game_object = object;
    g_audio_game_object_handle = handle;
    return true;
}

bool RegisterAudioGameObject() {
    if (!g_host || !g_host->runtime_invoke || !g_host->object_unbox ||
        !g_audio_game_object || !g_ak_register_game_object.resolved.method_info) {
        return false;
    }
    void* parameters[1]{g_audio_game_object};
    void* exception = nullptr;
    void* boxed = g_host->runtime_invoke(g_host->context,
        g_ak_register_game_object.resolved.method_info, nullptr,
        parameters, &exception);
    void* value = boxed ? g_host->object_unbox(g_host->context, boxed) : nullptr;
    int32_t result = 0;
    if (value) {
        std::memcpy(&result, value, sizeof(result));
    }
    return !exception && value && result == 1;
}

uint32_t PostAudioInputEvent() {
    if (!g_host || !g_host->runtime_invoke || !g_host->object_unbox ||
        !g_audio_game_object || !g_audio_input_post_event.resolved.method_info) {
        return 0;
    }
    uint32_t event_id = kEventId;
    void* parameters[4]{&event_id, g_audio_game_object, nullptr, nullptr};
    void* exception = nullptr;
    void* boxed = g_host->runtime_invoke(g_host->context,
        g_audio_input_post_event.resolved.method_info, nullptr,
        parameters, &exception);
    void* value = boxed && !exception
        ? g_host->object_unbox(g_host->context, boxed) : nullptr;
    uint32_t playing_id = 0;
    if (value) {
        std::memcpy(&playing_id, value, sizeof(playing_id));
    }
    return playing_id;
}

void ExecuteAction(int32_t action, uint32_t playing_id) noexcept;

bool EnsureAudioInputSource() {
    uint32_t state = g_source_state.load(std::memory_order_acquire);
    if (state == 2) {
        return true;
    }
    if (state == 1) {
        return false;
    }
    if (state == 3) {
        if (GetTickCount64() < g_source_retry_after_tick.load(
                std::memory_order_acquire)) {
            return false;
        }
        const uint32_t stale_playing_id = g_custom_playing_id.exchange(
            0, std::memory_order_acq_rel);
        ExecuteAction(0, stale_playing_id);
        g_format_seen.store(false, std::memory_order_release);
        g_custom_output_enabled.store(false, std::memory_order_release);
    }
    uint32_t expected = state;
    if (!g_source_state.compare_exchange_strong(
            expected, 1, std::memory_order_acq_rel)) {
        return expected == 2;
    }

    const auto try_initialize = reinterpret_cast<TryInitializeFn>(
        g_audio_input_try_initialize.resolved.method_pointer);
    const auto is_plugin_registered = reinterpret_cast<IsPluginRegisteredFn>(
        g_ak_is_plugin_registered.resolved.method_pointer);
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
    if (!try_initialize || !is_plugin_registered || !load_bank ||
        !get_sample_rate || !standard_config || !get_float ||
        !get_noninterleaved) {
        ScheduleSourceRetry();
        return false;
    }

    try_initialize(const_cast<void*>(
        g_audio_input_try_initialize.resolved.method_info));
    if (!is_plugin_registered(kSourcePluginType, kAudiokineticCompanyId,
            kAudioInputPluginId,
            const_cast<void*>(g_ak_is_plugin_registered.resolved.method_info))) {
        Log("[music.wwise] Audio Input source plugin is unavailable");
        ScheduleSourceRetry();
        return false;
    }

    if (!g_bank_loaded) {
        uint32_t loaded_bank_id = 0;
        const int32_t result = load_bank(
            const_cast<uint8_t*>(kAudioInputBank.data()),
            static_cast<uint32_t>(kAudioInputBank.size()), &loaded_bank_id,
            const_cast<void*>(g_ak_load_bank_memory_copy.resolved.method_info));
        if (result != 1 || loaded_bank_id != kBankId) {
            Log("[music.wwise] embedded Audio Input bank load failed result=" +
                std::to_string(result) + " bank=" +
                std::to_string(loaded_bank_id));
            ScheduleSourceRetry();
            return false;
        }
        g_bank_loaded = true;
    }

    if (!CreateAudioGameObject() ||
        (!g_audio_game_object_registered && !RegisterAudioGameObject())) {
        Log("[music.wwise] Audio Input GameObject setup failed");
        ScheduleSourceRetry();
        return false;
    }
    g_audio_game_object_registered = true;
    const uint32_t sample_rate = std::max(get_sample_rate(
        const_cast<void*>(g_ak_get_sample_rate.resolved.method_info)), 1u);
    g_output_sample_rate.store(sample_rate, std::memory_order_release);
    g_channel_config = standard_config(kStereoSpeakerMask,
        const_cast<void*>(g_channel_config_standard.resolved.method_info));
    g_sample_type = get_float(
        const_cast<void*>(g_ak_float_get.resolved.method_info));
    g_interleave = get_noninterleaved(
        const_cast<void*>(g_ak_noninterleaved_get.resolved.method_info));
    if (!g_channel_config) {
        ScheduleSourceRetry();
        return false;
    }

    g_custom_playing_id.store(0, std::memory_order_release);
    g_format_seen.store(false, std::memory_order_release);
    g_source_callback_baseline.store(
        g_sample_callbacks.load(std::memory_order_acquire),
        std::memory_order_release);
    g_last_sample_callback_tick.store(0, std::memory_order_release);
    const uint32_t playing_id = PostAudioInputEvent();
    if (!playing_id) {
        Log("[music.wwise] PostAudioInputEvent returned zero");
        ScheduleSourceRetry();
        return false;
    }
    uint32_t captured = g_custom_playing_id.load(std::memory_order_acquire);
    if (!captured) {
        g_custom_playing_id.store(playing_id, std::memory_order_release);
    } else if (captured != playing_id) {
        Log("[music.wwise] callback Playing ID mismatch");
        ExecuteAction(0, playing_id);
        ScheduleSourceRetry();
        return false;
    }
    if (g_source_state.load(std::memory_order_acquire) == 3) {
        ExecuteAction(0, playing_id);
        return false;
    }
    g_source_state.store(2, std::memory_order_release);
    g_source_started_tick.store(GetTickCount64(), std::memory_order_release);
    Log("[music.wwise] persistent stereo Audio Input event started playingId=" +
        std::to_string(playing_id) + " sampleRate=" +
        std::to_string(sample_rate) + " callbacksReady=" +
        (g_callbacks_ready.load(std::memory_order_acquire) ? "true" : "false"));
    return true;
}

void ExecuteAction(int32_t action, uint32_t playing_id) noexcept {
    if (!playing_id || !g_ak_execute_action.resolved.method_pointer) {
        return;
    }
    reinterpret_cast<ExecuteActionFn>(g_ak_execute_action.resolved.method_pointer)(
        action, playing_id,
        const_cast<void*>(g_ak_execute_action.resolved.method_info));
}

bool ShouldReplaceSlot(size_t slot) noexcept {
    switch (slot) {
    case 0: return g_replace_login.load(std::memory_order_acquire);
    case 1: return g_replace_meta.load(std::memory_order_acquire);
    case 2: return g_replace_gameplay.load(std::memory_order_acquire);
    default: return false;
    }
}

bool HasSelectedNativeScope() noexcept {
    for (size_t slot = 0; slot < g_native_playing_ids.size(); ++slot) {
        if (ShouldReplaceSlot(slot) &&
            g_native_playing_ids[slot].load(std::memory_order_acquire) != 0) {
            return true;
        }
    }
    return false;
}

void PauseSelectedNativeMusic() noexcept {
    for (size_t slot = 0; slot < g_native_playing_ids.size(); ++slot) {
        const uint32_t current = g_native_playing_ids[slot].load(
            std::memory_order_acquire);
        uint32_t& paused = g_policy_paused_ids[slot];
        if (paused && (paused != current || !ShouldReplaceSlot(slot))) {
            if (!g_game_pause_requested.load(std::memory_order_acquire)) {
                ExecuteAction(2, paused);
            }
            paused = 0;
        }
        if (current && ShouldReplaceSlot(slot) && paused != current) {
            ExecuteAction(1, current);
            paused = current;
        }
    }
}

void RestoreNativeMusic() noexcept {
    const bool may_resume = !g_game_pause_requested.load(
        std::memory_order_acquire);
    for (uint32_t& paused : g_policy_paused_ids) {
        if (paused && may_resume) {
            ExecuteAction(2, paused);
        }
        paused = 0;
    }
}

void SetCustomPaused(bool paused) noexcept {
    g_custom_output_enabled.store(!paused, std::memory_order_release);
}

void EngageReplacement() {
    if (!g_replacement_engaged) {
        g_replacement_engaged = true;
        Log("[music.policy] custom music engaged");
    }
}

void DisengageReplacement(const char* reason) {
    if (g_replacement_engaged) {
        g_replacement_engaged = false;
        Log(std::string("[music.policy] custom music disengaged reason=") +
            (reason ? reason : "unknown"));
    }
}

void PumpMusicPolicy() {
    const bool enabled = g_enabled.load(std::memory_order_acquire);
    if (!enabled) {
        RestoreNativeMusic();
        SetCustomPaused(true);
        DisengageReplacement("disabled");
        return;
    }

    if (g_pcm_prebuffered.load(std::memory_order_acquire) &&
        g_stream_active.load(std::memory_order_acquire) &&
        HasSelectedNativeScope()) {
        EnsureAudioInputSource();
    }
    const uint64_t now = GetTickCount64();
    const uint64_t last_data = g_last_audio_data_tick.load(
        std::memory_order_acquire);
    const uint64_t last_callback = g_last_sample_callback_tick.load(
        std::memory_order_acquire);
    const bool callbacks_working = g_format_seen.load(std::memory_order_acquire) &&
        g_sample_callbacks.load(std::memory_order_acquire) >
            g_source_callback_baseline.load(std::memory_order_acquire);
    const bool stream_paused = g_stream_paused.load(std::memory_order_acquire);
    const bool transport_healthy = g_session_alive.load(std::memory_order_acquire) &&
        g_stream_active.load(std::memory_order_acquire) &&
        (stream_paused || (last_data && now - last_data < 1500));
    const bool selected_scope = HasSelectedNativeScope();
    if (!selected_scope) {
        SetCustomPaused(true);
        RestoreNativeMusic();
        DisengageReplacement("scope_unavailable");
        return;
    }
    if (g_stream_activation_pending.load(std::memory_order_acquire) &&
        g_source_state.load(std::memory_order_acquire) == 2 &&
        !stream_paused &&
        g_session_alive.load(std::memory_order_acquire) &&
        g_stream_active.load(std::memory_order_acquire) &&
        g_pcm_prebuffered.load(std::memory_order_acquire)) {
        g_source_callback_baseline.store(
            g_sample_callbacks.load(std::memory_order_acquire),
            std::memory_order_release);
        g_last_audio_data_tick.store(0, std::memory_order_release);
        g_source_started_tick.store(now, std::memory_order_release);
        SetCustomPaused(false);
        g_stream_activation_pending.store(false, std::memory_order_release);
        Log("[music.wwise] persistent Audio Input activated for stream=" +
            std::to_string(g_stream_id.load(std::memory_order_acquire)));
        if (g_fallback_to_native.load(std::memory_order_acquire)) {
            RestoreNativeMusic();
        }
        DisengageReplacement("stream_transition");
        return;
    }
    if (g_source_state.load(std::memory_order_acquire) == 2 &&
        !callbacks_working &&
        now - g_source_started_tick.load(std::memory_order_acquire) < 2000) {
        // Let the newly posted Event reach both Wwise callbacks before any
        // policy action can pause it. Native music keeps playing meanwhile.
        return;
    }
    const bool functional = g_source_state.load(std::memory_order_acquire) == 2 &&
        callbacks_working && transport_healthy && selected_scope;

    if (functional && !g_game_pause_requested.load(std::memory_order_acquire)) {
        if (stream_paused) {
            SetCustomPaused(true);
            if (g_replacement_engaged) {
                PauseSelectedNativeMusic();
            }
        } else {
            SetCustomPaused(false);
            PauseSelectedNativeMusic();
            EngageReplacement();
        }
        return;
    }

    const uint64_t source_started = g_source_started_tick.load(
        std::memory_order_acquire);
    const bool callback_stalled =
        g_source_state.load(std::memory_order_acquire) == 2 &&
        !stream_paused &&
        !g_game_pause_requested.load(std::memory_order_acquire) &&
        g_session_alive.load(std::memory_order_acquire) &&
        g_stream_active.load(std::memory_order_acquire) &&
        g_pcm_prebuffered.load(std::memory_order_acquire) &&
        source_started && now - source_started >= 3000 &&
        (!last_callback || now - last_callback >= 3000);
    if (callback_stalled) {
        Log("[music.wwise] Audio Input callback stalled; scheduling source retry");
        ScheduleSourceRetry();
        SetCustomPaused(true);
        if (g_fallback_to_native.load(std::memory_order_acquire)) {
            RestoreNativeMusic();
        }
        DisengageReplacement("source_stalled");
        return;
    }

    const bool can_probe_recovery =
        g_source_state.load(std::memory_order_acquire) == 2 &&
        selected_scope && !stream_paused &&
        !g_game_pause_requested.load(std::memory_order_acquire) &&
        g_session_alive.load(std::memory_order_acquire) &&
        g_stream_active.load(std::memory_order_acquire) &&
        g_pcm_prebuffered.load(std::memory_order_acquire);
    if (can_probe_recovery) {
        // Keep native music audible while the custom Event proves that Wwise
        // is consuming the refilled ring again.
        SetCustomPaused(false);
        if (g_fallback_to_native.load(std::memory_order_acquire)) {
            RestoreNativeMusic();
        }
        DisengageReplacement("recovery_probe");
        return;
    }

    SetCustomPaused(true);
    if (g_fallback_to_native.load(std::memory_order_acquire)) {
        RestoreNativeMusic();
    }
    DisengageReplacement(
        g_game_pause_requested.load(std::memory_order_acquire)
            ? "game_paused"
            : (!g_session_alive.load(std::memory_order_acquire)
                ? "session_unavailable"
                : (!g_stream_active.load(std::memory_order_acquire)
                    ? "stream_inactive"
                    : "transport_unhealthy")));
}

uint32_t __fastcall PostMusicEventHook(void* instance, void* event_name,
    int32_t pre_action, void* method) {
    const uint32_t result = g_original_post_music_event
        ? g_original_post_music_event(instance, event_name, pre_action, method)
        : 0;
    UpdateNativeSnapshot(instance);
    return result;
}

void __fastcall StartMusicEventHook(void* instance, void* event_name,
    uint32_t* playing_id, int32_t callback_type, void* callback, void* method) {
    if (g_original_start_music_event) {
        g_original_start_music_event(instance, event_name, playing_id,
            callback_type, callback, method);
    }
    UpdateNativeSnapshot(instance);
}

void __fastcall StopByIdHook(
    void* instance, uint32_t* playing_id, void* method) {
    if (g_original_stop_by_id) {
        g_original_stop_by_id(instance, playing_id, method);
    }
    UpdateNativeSnapshot(instance);
}

void __fastcall PauseMusicHook(void* instance, void* method) {
    if (g_original_pause_music) {
        g_original_pause_music(instance, method);
    }
    g_game_pause_requested.store(true, std::memory_order_release);
    SetCustomPaused(true);
    UpdateNativeSnapshot(instance);
}

void __fastcall ResumeMusicHook(void* instance, void* method) {
    if (g_original_resume_music) {
        g_original_resume_music(instance, method);
    }
    g_game_pause_requested.store(false, std::memory_order_release);
    UpdateNativeSnapshot(instance);
}

void __fastcall StopMusicHook(void* instance, void* method) {
    if (g_original_stop_music) {
        g_original_stop_music(instance, method);
    }
    UpdateNativeSnapshot(instance);
}

void __fastcall TimelinePauseHook(void* instance, void* method) {
    if (g_original_timeline_pause) {
        g_original_timeline_pause(instance, method);
    }
    g_game_pause_requested.store(true, std::memory_order_release);
    SetCustomPaused(true);
    UpdateNativeSnapshot(instance);
}

void __fastcall TimelineResumeHook(void* instance, void* method) {
    if (g_original_timeline_resume) {
        g_original_timeline_resume(instance, method);
    }
    g_game_pause_requested.store(false, std::memory_order_release);
    UpdateNativeSnapshot(instance);
}

void __fastcall MainThreadExecHook(void* instance, void* method) {
    if (g_original_main_thread_exec) {
        g_original_main_thread_exec(instance, method);
    }
    PumpMusicPolicy();
}

std::filesystem::path CurrentModulePath() {
    HMODULE module = nullptr;
    if (!GetModuleHandleExW(
            GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
                GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(&CurrentModulePath), &module)) {
        return {};
    }
    std::array<wchar_t, 32768> path{};
    const DWORD length = GetModuleFileNameW(
        module, path.data(), static_cast<DWORD>(path.size()));
    return length && length < path.size()
        ? std::filesystem::path(std::wstring(path.data(), length))
        : std::filesystem::path{};
}

template <typename T>
bool ResolveOmniFunction(HMODULE module, const char* name, T& destination) {
    destination = reinterpret_cast<T>(GetProcAddress(module, name));
    if (!destination) {
        Log(std::string("[music.omni] missing SDK export=") + name);
        return false;
    }
    return true;
}

struct OmniSession {
    HMODULE library = nullptr;
    Omni::Api api{};
    Omni::ClientHandle client = nullptr;
    Omni::Handle pcm = nullptr;
    std::string instance_id;
    bool backend_launched = false;
    uint64_t last_heartbeat = 0;
    uint64_t last_connect_error_log = 0;
    int64_t bound_stream_id = 0;
    int32_t format_generation = -1;
    int64_t seek_generation = -1;

    void ClosePcm() {
        if (pcm && api.close) {
            api.close(pcm);
        }
        pcm = nullptr;
        bound_stream_id = 0;
        format_generation = -1;
        seek_generation = -1;
    }

    void Disconnect() {
        ClosePcm();
        if (client && !instance_id.empty() && api.client_disconnect_instance) {
            api.client_disconnect_instance(client, instance_id.c_str());
        }
        instance_id.clear();
        if (client && api.client_destroy) {
            api.client_destroy(client);
        }
        client = nullptr;
        g_session_alive.store(false, std::memory_order_release);
        g_stream_active.store(false, std::memory_order_release);
        g_pcm_prebuffered.store(false, std::memory_order_release);
        g_ring.Clear();
    }

    void Unload() {
        Disconnect();
        if (library) {
            FreeLibrary(library);
        }
        library = nullptr;
        api = {};
        backend_launched = false;
        last_connect_error_log = 0;
    }
};

bool LoadOmniSdk(OmniSession& session, const MusicConfig& config) {
    if (session.library) {
        return true;
    }
    std::vector<std::filesystem::path> candidates;
    if (!config.backend_exe.empty()) {
        const std::filesystem::path backend(config.backend_exe);
        candidates.push_back(backend.parent_path() / L"native" / L"x64" /
            L"OmniPcmShared.dll");
        candidates.push_back(backend.parent_path() / L"OmniPcmShared.dll");
    }
    const std::filesystem::path module_path = CurrentModulePath();
    if (!module_path.empty()) {
        candidates.push_back(module_path.parent_path() / L"OmniPcmShared.dll");
    }

    for (const auto& candidate : candidates) {
        std::error_code error;
        if (!std::filesystem::is_regular_file(candidate, error)) {
            continue;
        }
        session.library = LoadLibraryExW(candidate.c_str(), nullptr,
            LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR | LOAD_LIBRARY_SEARCH_SYSTEM32);
        if (session.library) {
            Log("[music.omni] SDK loaded path=" + WideToUtf8(candidate.wstring()));
            break;
        }
    }
    if (!session.library) {
        Log("[music.omni] OmniPcmShared.dll was not found or could not be loaded");
        return false;
    }

    bool ok = true;
#define OMNI_RESOLVE(field, name) \
    ok &= ResolveOmniFunction(session.library, name, session.api.field)
    OMNI_RESOLVE(get_abi_version, "OmniPcm_GetAbiVersion");
    OMNI_RESOLVE(get_abi_info, "OmniPcm_GetAbiInfo");
    OMNI_RESOLVE(open_instance_utf8, "OmniPcm_OpenInstanceUtf8");
    OMNI_RESOLVE(close, "OmniPcm_Close");
    OMNI_RESOLVE(is_open, "OmniPcm_IsOpen");
    OMNI_RESOLVE(get_version, "OmniPcm_GetVersion");
    OMNI_RESOLVE(get_last_error, "OmniPcm_GetLastError");
    OMNI_RESOLVE(get_snapshot_v2, "OmniPcm_GetSnapshotV2");
    OMNI_RESOLVE(bind_current_stream, "OmniPcm_BindCurrentStream");
    OMNI_RESOLVE(read_frames, "OmniPcm_ReadFrames");
    OMNI_RESOLVE(set_audible_cursor, "OmniPcm_SetAudibleCursor");
    OMNI_RESOLVE(client_create, "OmniPcmClient_Create");
    OMNI_RESOLVE(client_destroy, "OmniPcmClient_Destroy");
    OMNI_RESOLVE(client_get_last_error, "OmniPcmClient_GetLastError");
    OMNI_RESOLVE(client_connect_instance, "OmniPcmClient_ConnectInstance");
    OMNI_RESOLVE(client_heartbeat, "OmniPcmClient_Heartbeat");
    OMNI_RESOLVE(client_disconnect_instance, "OmniPcmClient_DisconnectInstance");
    OMNI_RESOLVE(client_set_target_latency, "OmniPcmClient_SetTargetLatency");
#undef OMNI_RESOLVE
    if (!ok) {
        session.Unload();
        return false;
    }

    Omni::AbiInfo abi_info{};
    abi_info.size = sizeof(abi_info);
    const uint32_t encoded_abi = session.api.get_abi_version();
    const int abi_result = session.api.get_abi_info(&abi_info);
    const uint32_t required_sample_format =
        1u << Omni::kFloat32Interleaved;
    const bool compatible = abi_result == 0 &&
        (encoded_abi >> 16u) == Omni::kAbiMajor &&
        abi_info.abi_major == Omni::kAbiMajor &&
        abi_info.min_shared_protocol <= Omni::kVersion2 &&
        abi_info.max_shared_protocol >= Omni::kVersion2 &&
        (abi_info.sample_format_mask & required_sample_format) != 0;
    if (!compatible) {
        Log("[music.omni] incompatible SDK ABI encoded=" +
            std::to_string(encoded_abi) + " major=" +
            std::to_string(abi_info.abi_major) + " protocol=" +
            std::to_string(abi_info.min_shared_protocol) + ".." +
            std::to_string(abi_info.max_shared_protocol) + " formats=" +
            std::to_string(abi_info.sample_format_mask));
        session.Unload();
        return false;
    }
    Log("[music.omni] SDK ABI compatible version=" +
        std::to_string(abi_info.abi_major) + "." +
        std::to_string(abi_info.abi_minor));
    return true;
}

bool LaunchBackend(const std::wstring& backend_exe) {
    if (backend_exe.empty()) {
        return false;
    }
    std::error_code error;
    if (!std::filesystem::is_regular_file(backend_exe, error)) {
        return false;
    }
    std::wstring command_line = L"\"" + backend_exe + L"\"";
    std::vector<wchar_t> mutable_command(command_line.begin(), command_line.end());
    mutable_command.push_back(L'\0');
    STARTUPINFOW startup{};
    startup.cb = sizeof(startup);
    startup.dwFlags = STARTF_USESHOWWINDOW;
    startup.wShowWindow = SW_HIDE;
    PROCESS_INFORMATION process{};
    const std::filesystem::path working_directory =
        std::filesystem::path(backend_exe).parent_path();
    const BOOL started = CreateProcessW(backend_exe.c_str(),
        mutable_command.data(), nullptr, nullptr, FALSE,
        CREATE_NO_WINDOW | CREATE_NEW_PROCESS_GROUP, nullptr,
        working_directory.c_str(), &startup, &process);
    if (started) {
        CloseHandle(process.hThread);
        CloseHandle(process.hProcess);
    }
    return started != FALSE;
}

bool ConnectOmniSession(OmniSession& session, const MusicConfig& config) {
    if (session.pcm && session.client && !session.instance_id.empty()) {
        return true;
    }
    if (!LoadOmniSdk(session, config)) {
        return false;
    }
    if (!session.client) {
        const Omni::ClientConfig client_config{"127.0.0.1", 0, 1500};
        session.client = session.api.client_create(&client_config);
        if (!session.client) {
            return false;
        }
    }

    Omni::ConnectOptions options{};
    options.client_id = config.client_id.c_str();
    options.mod_id = kModuleId;
    options.game_name = "Arknights: Endfield";
    options.display_name = "Better Endfield";
    options.kind = Omni::GameMod;
    options.capability_flags = Omni::ServerControlledPlayback |
        Omni::QueueManagement | Omni::Seek | Omni::AudioPlayback;
    Omni::ConnectionInfo connection{};
    const int result = session.api.client_connect_instance(
        session.client, &options, &connection);
    if (result != 0 || !connection.instance_id[0]) {
        const uint64_t tick = GetTickCount64();
        if (!session.last_connect_error_log ||
            tick - session.last_connect_error_log >= 5000) {
            session.last_connect_error_log = tick;
            const char* client_error = session.api.client_get_last_error
                ? session.api.client_get_last_error(session.client) : nullptr;
            Log("[music.omni] instance connect failed result=" +
                std::to_string(result) + " error=" +
                (client_error && *client_error ? client_error : "<none>"));
        }
        if (!session.backend_launched) {
            session.backend_launched = LaunchBackend(config.backend_exe);
            Log(std::string("[music.omni] backend launch=") +
                (session.backend_launched ? "started" : "failed"));
        }
        if (session.client) {
            session.api.client_destroy(session.client);
            session.client = nullptr;
        }
        return false;
    }
    session.instance_id = connection.instance_id;
    session.last_connect_error_log = 0;
    const int latency_result = session.api.client_set_target_latency(
        session.client, session.instance_id.c_str(),
        static_cast<float>(config.target_latency));
    if (latency_result != 0) {
        Log("[music.omni] target latency update failed result=" +
            std::to_string(latency_result));
        session.Disconnect();
        return false;
    }
    const std::string map_name = "Global\\OmniMixPlayer_PCM_" +
        session.instance_id;
    session.pcm = session.api.open_instance_utf8(session.instance_id.c_str());
    if (!session.pcm || !session.api.is_open(session.pcm)) {
        session.ClosePcm();
        return false;
    }
    const uint32_t version = session.api.get_version(session.pcm);
    if (version != Omni::kVersion2) {
        Log("[music.omni] unsupported shared-memory version=" +
            std::to_string(version));
        session.ClosePcm();
        return false;
    }
    session.last_heartbeat = GetTickCount64();
    g_session_alive.store(true, std::memory_order_release);
    Log("[music.omni] session connected instance=" + session.instance_id +
        " map=" + map_name + " version=" + std::to_string(version));
    return true;
}

void WorkerMain() {
    OmniSession session;
    LinearStereoResampler resampler;
    AudibleCursorDelay audible_delay;
    std::vector<float> input(static_cast<size_t>(kWorkerReadFrames) * 8u);
    std::vector<float> converted;
    converted.reserve(static_cast<size_t>(kWorkerReadFrames) * 12u);
    uint64_t observed_config_generation = UINT64_MAX;
    uint32_t observed_output_rate = 0;
    int32_t source_rate = 0;
    int32_t source_channels = 0;
    int64_t local_read_cursor = 0;
    uint64_t last_diagnostics = 0;
    uint64_t next_connect_attempt = 0;

    while (!g_shutdown_requested.load(std::memory_order_acquire)) {
        const MusicConfig config = CopyConfiguration();
        const uint64_t generation = g_config_generation.load(
            std::memory_order_acquire);
        if (!config.enabled || config.backend_exe.empty() ||
            config.client_id.empty()) {
            if (session.library || session.client || session.pcm) {
                session.Unload();
            }
            g_stream_active.store(false, std::memory_order_release);
            g_pcm_prebuffered.store(false, std::memory_order_release);
            g_stream_activation_pending.store(false, std::memory_order_release);
            std::this_thread::sleep_for(std::chrono::milliseconds(100));
            observed_config_generation = generation;
            continue;
        }

        if (observed_config_generation != UINT64_MAX &&
            observed_config_generation != generation) {
            session.Unload();
            g_ring.Clear();
            audible_delay.Reset();
            source_rate = 0;
            source_channels = 0;
        }
        observed_config_generation = generation;

        const uint64_t now = GetTickCount64();
        if ((!session.pcm || !session.client) && now < next_connect_attempt) {
            std::this_thread::sleep_for(std::chrono::milliseconds(50));
            continue;
        }
        if (!ConnectOmniSession(session, config)) {
            next_connect_attempt = now + (session.backend_launched ? 500u : 2000u);
            g_session_alive.store(false, std::memory_order_release);
            g_stream_active.store(false, std::memory_order_release);
            g_pcm_prebuffered.store(false, std::memory_order_release);
            std::this_thread::sleep_for(std::chrono::milliseconds(50));
            continue;
        }

        if (now - session.last_heartbeat >= 1000) {
            int alive = 0;
            if (session.api.client_heartbeat(session.client,
                    session.instance_id.c_str(), &alive) != 0 || !alive) {
                Log("[music.omni] backend heartbeat lost; reconnecting");
                session.Unload();
                next_connect_attempt = now + 1000;
                continue;
            }
            session.last_heartbeat = now;
        }

        Omni::SnapshotV2 snapshot{};
        snapshot.size = sizeof(snapshot);
        if (session.api.get_snapshot_v2(session.pcm, &snapshot) != 0 ||
            (snapshot.abi_version >> 16u) != Omni::kAbiMajor ||
            snapshot.shared_protocol_version != Omni::kVersion2 ||
            snapshot.sample_format !=
                static_cast<int32_t>(Omni::kFloat32Interleaved) ||
            snapshot.sample_rate <= 0 || snapshot.sample_rate > 384000 ||
            snapshot.channels <= 0 || snapshot.channels > 8) {
            g_stream_active.store(false, std::memory_order_release);
            g_pcm_prebuffered.store(false, std::memory_order_release);
            std::this_thread::sleep_for(std::chrono::milliseconds(20));
            continue;
        }

        const bool format_ready = (snapshot.flags & Omni::FormatReady) != 0;
        const bool active = format_ready &&
            (snapshot.state == Omni::Playing ||
                snapshot.state == Omni::Paused ||
                snapshot.state == Omni::Draining);
        g_stream_active.store(active, std::memory_order_release);
        g_stream_paused.store(
            snapshot.state == Omni::Paused, std::memory_order_release);
        if (!active || (snapshot.flags & Omni::StreamError) != 0) {
            g_pcm_prebuffered.store(false, std::memory_order_release);
            g_ring.Clear();
            std::this_thread::sleep_for(std::chrono::milliseconds(20));
            continue;
        }

        const uint32_t output_rate = g_output_sample_rate.load(
            std::memory_order_acquire);
        const bool stream_changed = snapshot.stream_id != session.bound_stream_id ||
            snapshot.format_generation != session.format_generation ||
            snapshot.seek_generation != session.seek_generation ||
            output_rate != observed_output_rate;
        if (stream_changed) {
            if (session.api.bind_current_stream(session.pcm) != 0) {
                std::this_thread::sleep_for(std::chrono::milliseconds(20));
                continue;
            }
            session.bound_stream_id = snapshot.stream_id;
            session.format_generation = snapshot.format_generation;
            session.seek_generation = snapshot.seek_generation;
            observed_output_rate = output_rate;
            source_rate = snapshot.sample_rate;
            source_channels = snapshot.channels;
            local_read_cursor = snapshot.read_cursor;
            resampler.Configure(source_rate, source_channels, output_rate);
            audible_delay.Reset(snapshot.audible_cursor);
            g_ring.Clear();
            g_stream_id.store(snapshot.stream_id, std::memory_order_release);
            g_pcm_prebuffered.store(false, std::memory_order_release);
            g_stream_activation_pending.store(true, std::memory_order_release);
            Log("[music.omni] stream bound id=" +
                std::to_string(snapshot.stream_id) + " format=" +
                std::to_string(source_rate) + "Hz/" +
                std::to_string(source_channels) + "ch output=" +
                std::to_string(output_rate) + "Hz");
        }

        if (snapshot.state == Omni::Paused) {
            std::this_thread::sleep_for(std::chrono::milliseconds(20));
            continue;
        }

        if (g_ring.AvailableWrite() >= 8192u) {
            const int64_t read = session.api.read_frames(
                session.pcm, input.data(), kWorkerReadFrames);
            if (read > 0) {
                local_read_cursor += read;
                resampler.Convert(input.data(), static_cast<uint32_t>(read), converted);
                const uint32_t converted_frames = static_cast<uint32_t>(
                    converted.size() / 2u);
                if (g_ring.Push(converted.data(), converted_frames) !=
                    converted_frames) {
                    g_underflow_callbacks.fetch_add(1, std::memory_order_relaxed);
                }
            }
        }

        const uint64_t buffered = g_ring.AvailableRead();
        const uint64_t prebuffer_frames = static_cast<uint64_t>(output_rate) *
            config.prebuffer_ms / 1000u;
        g_pcm_prebuffered.store(
            buffered >= prebuffer_frames, std::memory_order_release);
        const int64_t buffered_source_frames = static_cast<int64_t>(
            buffered * static_cast<uint64_t>(std::max(source_rate, 1)) /
            std::max<uint32_t>(output_rate, 1u));
        const int64_t rendered_cursor = std::max<int64_t>(
            0, local_read_cursor - buffered_source_frames);
        const uint64_t audible_tick = GetTickCount64();
        const int64_t audible = audible_delay.Observe(
            audible_tick, rendered_cursor, kAudibleOutputDelayMs);
        session.api.set_audible_cursor(
            session.pcm, audible, stream_changed ? 1 : 0);

        if (config.diagnostics && now - last_diagnostics >= 5000) {
            last_diagnostics = now;
            Log("[music.diag] stream=" + std::to_string(snapshot.stream_id) +
                " state=" + std::to_string(snapshot.state) +
                " ringFrames=" + std::to_string(buffered) +
                " sharedRead=" + std::to_string(local_read_cursor) +
                " audible=" + std::to_string(audible) +
                " callbacks=" + std::to_string(g_sample_callbacks.load()) +
                " underflows=" + std::to_string(g_underflow_callbacks.load()));
        }
        std::this_thread::sleep_for(std::chrono::milliseconds(5));
    }
    session.Unload();
    g_worker_started.store(false, std::memory_order_release);
}

bool ResolveContractsAndInstallHooks() {
    bool ok = true;
    ok &= Resolve(g_audio_input_post_event);
    ok &= Resolve(g_ak_load_bank_memory_copy);
    ok &= Resolve(g_ak_get_sample_rate);
    ok &= Resolve(g_ak_register_game_object);
    ok &= Resolve(g_ak_execute_action);
    ok &= Resolve(g_ak_is_plugin_registered);
    ok &= Resolve(g_game_object_ctor);
    ok &= Resolve(g_channel_config_standard);
    ok &= Resolve(g_audio_format_set_all);
    ok &= Resolve(g_ak_float_get);
    ok &= Resolve(g_ak_noninterleaved_get);
    ok &= Resolve(g_login_id_field);
    ok &= Resolve(g_meta_id_field);
    ok &= Resolve(g_gameplay_id_field);
    ok &= Hook(g_main_thread_exec, reinterpret_cast<void*>(&MainThreadExecHook),
        reinterpret_cast<void**>(&g_original_main_thread_exec));
    ok &= Hook(g_audio_input_samples,
        reinterpret_cast<void*>(&AudioInputSamplesHook),
        reinterpret_cast<void**>(&g_original_audio_input_samples));
    ok &= Hook(g_audio_input_format,
        reinterpret_cast<void*>(&AudioInputFormatHook),
        reinterpret_cast<void**>(&g_original_audio_input_format));
    ok &= Hook(g_audio_input_try_initialize,
        reinterpret_cast<void*>(&AudioInputTryInitializeHook),
        reinterpret_cast<void**>(&g_original_audio_input_try_initialize));
    ok &= Hook(g_ak_set_audio_input_callbacks,
        reinterpret_cast<void*>(&SetAudioInputCallbacksHook),
        reinterpret_cast<void**>(&g_original_set_audio_input_callbacks));
    ok &= Hook(g_post_music_event,
        reinterpret_cast<void*>(&PostMusicEventHook),
        reinterpret_cast<void**>(&g_original_post_music_event));
    ok &= Hook(g_start_music_event,
        reinterpret_cast<void*>(&StartMusicEventHook),
        reinterpret_cast<void**>(&g_original_start_music_event));
    ok &= Hook(g_stop_by_id, reinterpret_cast<void*>(&StopByIdHook),
        reinterpret_cast<void**>(&g_original_stop_by_id));
    ok &= Hook(g_pause_music, reinterpret_cast<void*>(&PauseMusicHook),
        reinterpret_cast<void**>(&g_original_pause_music));
    ok &= Hook(g_resume_music, reinterpret_cast<void*>(&ResumeMusicHook),
        reinterpret_cast<void**>(&g_original_resume_music));
    ok &= Hook(g_stop_music, reinterpret_cast<void*>(&StopMusicHook),
        reinterpret_cast<void**>(&g_original_stop_music));
    ok &= Hook(g_timeline_pause, reinterpret_cast<void*>(&TimelinePauseHook),
        reinterpret_cast<void**>(&g_original_timeline_pause));
    ok &= Hook(g_timeline_resume, reinterpret_cast<void*>(&TimelineResumeHook),
        reinterpret_cast<void**>(&g_original_timeline_resume));
    if (!ok) {
        g_host->release_module_hooks(g_host->context, kModuleId);
        return false;
    }
    g_hooks_installed = true;
    return true;
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->log || !host->resolve_method || !host->resolve_field ||
        !host->create_hook || !host->release_module_hooks ||
        !host->resolve_class || !host->object_new || !host->string_new ||
        !host->runtime_invoke || !host->object_unbox ||
        !host->gchandle_new || !host->gchandle_free) {
        return BE_Result_ContractMismatch;
    }
    g_host = host;
    if (!g_ring.Initialize(kRingCapacityFrames)) {
        return BE_Result_Failed;
    }
    g_callback_scratch = std::make_unique<float[]>(
        static_cast<size_t>(kMaximumCallbackFrames) * 2u);
    if (!g_callback_scratch) {
        return BE_Result_Failed;
    }
    const BE_Result class_result = g_host->resolve_class(g_host->context,
        kUnityCoreAssembly, "UnityEngine", "GameObject", &g_game_object_class);
    if (class_result != BE_Result_Ok || !g_game_object_class.class_info) {
        return BE_Result_ContractMismatch;
    }
    if (!ResolveContractsAndInstallHooks()) {
        return BE_Result_ContractMismatch;
    }
    HMODULE pinned_module = nullptr;
    if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
            GET_MODULE_HANDLE_EX_FLAG_PIN,
            reinterpret_cast<LPCWSTR>(&Initialize), &pinned_module)) {
        g_host->release_module_hooks(g_host->context, kModuleId);
        g_hooks_installed = false;
        g_callback_scratch.reset();
        return BE_Result_Failed;
    }
    g_audio_callbacks_stopping.store(false, std::memory_order_release);
    g_audio_callbacks_inflight.store(0, std::memory_order_release);
    g_shutdown_requested.store(false, std::memory_order_release);
    g_worker_started.store(true, std::memory_order_release);
    g_worker = std::thread(&WorkerMain);
    Log("[music] module 1.0.0 initialized; metadata contracts only, no fixed RVA");
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char* configuration) {
    const MusicConfig parsed = ParseConfiguration(configuration);
    {
        std::scoped_lock lock(g_config_mutex);
        g_config = parsed;
    }
    g_replace_login.store(parsed.replace_login, std::memory_order_release);
    g_replace_meta.store(parsed.replace_meta, std::memory_order_release);
    g_replace_gameplay.store(parsed.replace_gameplay, std::memory_order_release);
    g_fallback_to_native.store(
        parsed.fallback_to_native, std::memory_order_release);
    g_enabled.store(parsed.enabled, std::memory_order_release);
    g_config_generation.fetch_add(1, std::memory_order_acq_rel);
    Log(std::string("[music.config] enabled=") +
        (parsed.enabled ? "true" : "false") +
        " backend=" + (parsed.backend_exe.empty() ? "missing" : "configured") +
        " clientId=" + (parsed.client_id.empty() ? "missing" : "configured") +
        " scopes=" + (parsed.replace_login ? "L" : "-") +
        (parsed.replace_meta ? "M" : "-") +
        (parsed.replace_gameplay ? "G" : "-"));
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    g_enabled.store(false, std::memory_order_release);
    g_shutdown_requested.store(true, std::memory_order_release);
    if (g_worker.joinable()) {
        g_worker.join();
    }
    g_audio_callbacks_stopping.store(true, std::memory_order_release);
    RestoreNativeMusic();
    ExecuteAction(0, g_custom_playing_id.exchange(
        0, std::memory_order_acq_rel));
    if (g_hooks_installed && g_host && g_host->release_module_hooks) {
        g_host->release_module_hooks(g_host->context, kModuleId);
    }
    g_hooks_installed = false;

    const uint64_t drain_deadline = GetTickCount64() + kCallbackDrainTimeoutMs;
    while (g_audio_callbacks_inflight.load(std::memory_order_acquire) != 0 &&
        GetTickCount64() < drain_deadline) {
        Sleep(1);
    }
    const bool callbacks_drained =
        g_audio_callbacks_inflight.load(std::memory_order_acquire) == 0;
    if (!callbacks_drained) {
        HMODULE pinned_module = nullptr;
        GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
                GET_MODULE_HANDLE_EX_FLAG_PIN,
            reinterpret_cast<LPCWSTR>(&Shutdown), &pinned_module);
        Log("[music] audio callback drain timed out; module pinned for safety");
    }
    if (callbacks_drained && g_audio_game_object_handle && g_host &&
        g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, g_audio_game_object_handle);
        g_audio_game_object_handle = 0;
        g_audio_game_object = nullptr;
    }
    Log("[music] shutdown callbacks=" +
        std::to_string(g_sample_callbacks.load()) + " frames=" +
        std::to_string(g_sample_frames.load()) + " underflows=" +
        std::to_string(g_underflow_callbacks.load()));
    if (callbacks_drained) {
        g_callback_scratch.reset();
        g_host = nullptr;
    }
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Better Endfield Music", "2.0.1",
        BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown,
};

} // namespace

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1(void) {
    return &kApi;
}
