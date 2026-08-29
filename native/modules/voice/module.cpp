#include "BetterEndfield/ModuleApi.h"
#include "BetterEndfield/VoiceCatalog.h"

#include <Windows.h>

#include <algorithm>
#include <array>
#include <atomic>
#include <cctype>
#include <cstdint>
#include <cstdio>
#include <fstream>
#include <filesystem>
#include <mutex>
#include <string>
#include <string_view>
#include <unordered_map>
#include <unordered_set>
#include <vector>
#include <utility>

namespace BetterEndfield::Voice {
namespace {

constexpr char kModuleId[] = "betterendfield.voice";
constexpr int kAkSuccess = 1;

enum class ModuleState : uint8_t {
    Created,
    Resolving,
    Ready,
    Active,
    Disabled,
    ContractMismatch,
    Failed,
    Stopped,
};

struct VoiceRule {
    std::string speaker;
    int language = -1;
};

struct VoiceConfiguration {
    bool enabled = false;
    bool diagnostics = true;
    bool replace_narrative = true;
    std::string language_rules;
    std::vector<VoiceRule> rules;
};

struct VoiceRequestRoute {
    bool active = false;
    int language = -1;
    char speaker[128]{};
};

struct PendingLipRoute {
    bool armed = false;
    int language = -1;
    uint64_t configuration_generation = 0;
    char line_id[256]{};
    char speaker[128]{};
};

struct CatalogEntry {
    uint32_t source_media_id = 0;
    uint32_t target_media_id = 0;
    uint64_t data_offset = 0;
    uint32_t data_size = 0;
};

struct ResidentCatalog {
    std::string speaker;
    int language = -1;
    bool wildcard = false;
    std::vector<uint8_t> blob;
    std::vector<CatalogEntry> entries;
};

struct MethodContract {
    const char* key;
    BE_MethodDescriptorV1 descriptor;
    bool required;
    void* pointer = nullptr;
    const void* method_info = nullptr;
    bool resolved = false;
};

const BE_HostApiV1* g_host = nullptr;
std::atomic<ModuleState> g_state{ModuleState::Created};
VoiceConfiguration g_configuration;
std::mutex g_configuration_mutex;
std::atomic_bool g_diagnostics_enabled{true};
std::atomic_bool g_replace_narrative{true};
std::atomic<uint64_t> g_configuration_generation{1};

std::atomic<uint64_t> g_play_voice_hits{0};
std::atomic<uint64_t> g_internal_play_voice_hits{0};
std::atomic<uint64_t> g_channel_play_hits{0};
std::atomic<uint64_t> g_event_hits{0};
std::atomic<uint64_t> g_narrative_hits{0};
std::atomic<uint64_t> g_duration_hits{0};
std::atomic<uint64_t> g_language_override_hits{0};
std::atomic<uint64_t> g_lip_dialog_hits{0};
std::atomic<uint64_t> g_lip_path_hits{0};
std::atomic<uint64_t> g_lip_load_hits{0};
std::atomic<uint64_t> g_external_hits{0};
std::atomic<uint64_t> g_set_media_hits{0};
std::atomic<uint64_t> g_unset_media_hits{0};
std::atomic<uint64_t> g_package_load_hits{0};
std::atomic<uint64_t> g_package_unload_hits{0};

using VoiceContextFn = void(__fastcall*)(void* voice_context, void* method);
using InternalVoiceContextFn = uint32_t(__fastcall*)(
    void* voice_context_reference, void* method);
using ChannelPlayFn = void(__fastcall*)(
    void* instance, void* voice_context, void* method);
using PlayEventFn = uint32_t(__fastcall*)(
    void* event_name, uint64_t audio_object_id, uint32_t handle_id, void* method);
using NarrativeFn = uint32_t(__fastcall*)(
    void* instance, void* voice_id, uint64_t audio_object_id, void* config,
    void* method);
using DurationFn = bool(__fastcall*)(
    void* voice_id, float* duration, void* method);
using GetCurrentLanguageFn = int(__fastcall*)(void* method);
using LipDialogFn = void(__fastcall*)(
    void* instance, void* action_data, void* entity, void* method);
using LipPathFn = void*(__fastcall*)(
    int language, void* voice_id, void* suffix, void* method);
using LipLoadFn = bool(__fastcall*)(void* line_id, void** track, void* method);
using WwiseMediaFn = int(__fastcall*)(
    void* settings, uint32_t count, void* method);
using ExternalEventFn = uint32_t(__fastcall*)(
    void* event_name, uint64_t audio_object_id, void* external_source_key,
    uint32_t external_cookie, uint32_t callback_type, void* callback,
    void* cookie, uint32_t codec, void* method);
using TryLoadLanguagePackageFn = bool(__fastcall*)(void* language, void* method);
using GetLanguageNameFn = void*(__fastcall*)(int language, void* method);
using LoadFilePackageFn = int(__fastcall*)(
    void* package_path, uint32_t* package_id, void* method);
using UnloadFilePackageFn = int(__fastcall*)(uint32_t package_id, void* method);

#pragma pack(push, 1)
struct NativeAkSourceSettings {
    uint32_t source_id;
    uint32_t reserved;
    const void* media_memory;
    uint32_t media_size;
    uint32_t padding;
};
#pragma pack(pop)

static_assert(sizeof(NativeAkSourceSettings) == 24);

VoiceContextFn g_original_play_voice = nullptr;
InternalVoiceContextFn g_original_internal_play_voice = nullptr;
ChannelPlayFn g_original_channel_play = nullptr;
PlayEventFn g_original_play_event = nullptr;
NarrativeFn g_original_narrative = nullptr;
DurationFn g_original_duration = nullptr;
GetCurrentLanguageFn g_original_voice_language = nullptr;
LipDialogFn g_original_lip_dialog = nullptr;
LipPathFn g_original_lip_path = nullptr;
LipLoadFn g_original_lip_load = nullptr;
WwiseMediaFn g_original_set_media = nullptr;
WwiseMediaFn g_original_unset_media = nullptr;
ExternalEventFn g_original_external_event = nullptr;
LoadFilePackageFn g_original_load_file_package = nullptr;
UnloadFilePackageFn g_original_unload_file_package = nullptr;
std::vector<ResidentCatalog> g_resident_catalogs;
std::vector<NativeAkSourceSettings> g_catalog_routes;
std::vector<NativeAkSourceSettings> g_catalog_unset_routes;
std::unordered_set<uint32_t> g_catalog_missing_routes;
bool g_catalog_loaded = false;
bool g_catalog_applied = false;
std::mutex g_catalog_mutex;
std::mutex g_package_mutex;
std::array<bool, 4> g_language_package_ready{};
std::array<uint64_t, 4> g_language_package_retry_at{};
thread_local bool g_loading_auxiliary_package = false;
thread_local int g_duration_language_override = -1;
thread_local int g_lip_language_override = -1;
thread_local VoiceRequestRoute g_voice_request_route{};
thread_local PendingLipRoute g_pending_lip_route{};
BE_ResolvedFieldV1 g_voice_context_voice_data{};
BE_ResolvedFieldV1 g_runtime_voice_data_speaker_channel{};

bool LoadConfiguredCatalogs(const std::vector<VoiceRule>& rules);
bool ApplyCatalog();
bool UnapplyCatalog();
bool StopHooks();

MethodContract g_play_voice{
    "voice.player.play",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.Audio", "VoicePlayer", "PlayVoice",
        nullptr, "System.Void", 1},
    true};
MethodContract g_internal_play_voice{
    "voice.player.play-internal",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.Audio", "VoicePlayer", "_PlayVoice",
        "Beyond.Gameplay.Audio.VoiceContext&", "System.UInt32", 1},
    false};
MethodContract g_channel_play{
    "voice.channel.play",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.Audio",
        "VoiceSpeakChannelProcessor", "_PlayVoice", nullptr, "System.Void", 1},
    false};
MethodContract g_play_event{
    "voice.player.event",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.Audio", "VoicePlayer", "_PlayEvent",
        nullptr, "System.UInt32", 3},
    false};
MethodContract g_narrative{
    "voice.manager.narrative",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.Audio", "VoiceManager",
        "_SpeakNarrative", nullptr, "System.UInt32", 3},
    false};
MethodContract g_duration{
    "voice.duration",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.Audio", "VoiceUtils",
        "TryGetVoiceDuration", nullptr, "System.Boolean", 2},
    false};
MethodContract g_set_media{
    "wwise.media.set",
    {"AK.Wwise.Unity.API.dll", "", "AkSoundEnginePINVOKE", "CSharp_SetMedia",
        "System.IntPtr|System.UInt32", "System.Int32", 2},
    true};
MethodContract g_unset_media{
    "wwise.media.unset",
    {"AK.Wwise.Unity.API.dll", "", "AkSoundEnginePINVOKE", "CSharp_UnsetMedia",
        "System.IntPtr|System.UInt32", "System.Int32", 2},
    true};
MethodContract g_external_event{
    "audio.external-event",
    {"Audio.Beyond.dll", "Beyond.Audio", "AudioAdapter", "PostEventExternal",
        "System.String|System.UInt64|System.String|System.UInt32|Beyond.Audio.AudioCallbackType|Beyond.Audio.AudioEventCallback|System.Object|Beyond.Audio.AudioCodec",
        "System.UInt32", 8},
    true};
MethodContract g_try_load_language_package{
    "audio.language-package",
    {"Audio.Beyond.dll", "Beyond.Audio", "AudioVFSLoader",
        "TryLoadLanguagePck", "System.String", "System.Boolean", 1},
    true};
MethodContract g_get_language_name{
    "voice.language-name",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.Audio", "VoiceI18n",
        "GetLanguageName", "Beyond.I18n.AudioLang", "System.String", 1},
    true};
MethodContract g_load_file_package{
    "wwise.package.load",
    {"AK.Wwise.Unity.API.dll", "", "AkSoundEngine", "LoadFilePackage",
        "System.String|System.UInt32&", "AKRESULT", 2},
    true};
MethodContract g_unload_file_package{
    "wwise.package.unload",
    {"AK.Wwise.Unity.API.dll", "", "AkSoundEngine", "UnloadFilePackage",
        "System.UInt32", "AKRESULT", 1},
    true};

MethodContract g_voice_language{
    "voice.language",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.Audio", "VoiceI18n", "GetCurrentLanguage",
        nullptr, "Beyond.I18n.AudioLang", 0},
    true};
MethodContract g_lip_dialog{
    "lip.dialog.play",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "DialogManager",
        "_PlayLipSyncTrack", nullptr, "System.Void", 2},
    false};
MethodContract g_lip_path{
    "lip.track.path",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.View.LipSync", "LipSyncUtils",
        "GetLipSyncTrackPath", nullptr, "System.String", 3},
    false};
MethodContract g_lip_load{
    "lip.track.load",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.View.LipSync", "LipSyncUtils",
        "TryLoadTrack", nullptr, "System.Boolean", 2},
    false};
MethodContract g_lip_real_actor{
    "lip.dialog.real-actor",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay", "DialogPlayTrunkActionData",
        "get_realActorNameId", nullptr, "System.String", 0},
    false};
MethodContract g_lip_actor{
    "lip.dialog.actor",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay", "DialogPlayTrunkActionData",
        "get_actorNameId", nullptr, "System.String", 0},
    false};
MethodContract g_lip_trunk{
    "lip.dialog.trunk",
    {"Gameplay.Beyond.dll", "Beyond.Gameplay", "DialogPlayTrunkActionData",
        "get_trunkId", nullptr, "System.String", 0},
    false};

std::string_view Trim(std::string_view value) {
    while (!value.empty() &&
        std::isspace(static_cast<unsigned char>(value.front()))) {
        value.remove_prefix(1);
    }
    while (!value.empty() &&
        std::isspace(static_cast<unsigned char>(value.back()))) {
        value.remove_suffix(1);
    }
    return value;
}

bool ParseBool(std::string_view value, bool& result) {
    value = Trim(value);
    std::string lower(value);
    std::transform(lower.begin(), lower.end(), lower.begin(), [](unsigned char c) {
        return static_cast<char>(std::tolower(c));
    });
    if (lower == "true" || lower == "1" || lower == "yes" || lower == "on") {
        result = true;
        return true;
    }
    if (lower == "false" || lower == "0" || lower == "no" || lower == "off") {
        result = false;
        return true;
    }
    return false;
}

int ParseLanguage(std::string_view value) {
    std::string normalized(Trim(value));
    std::transform(normalized.begin(), normalized.end(), normalized.begin(),
        [](unsigned char item) { return static_cast<char>(std::tolower(item)); });
    if (normalized == "chinese") return 0;
    if (normalized == "english") return 1;
    if (normalized == "japanese") return 2;
    if (normalized == "korean") return 3;
    if (normalized == "followglobal") return -1;
    return -2;
}

const char* StateName(ModuleState state) {
    switch (state) {
    case ModuleState::Created: return "created";
    case ModuleState::Resolving: return "resolving";
    case ModuleState::Ready: return "ready";
    case ModuleState::Active: return "active";
    case ModuleState::Disabled: return "disabled";
    case ModuleState::ContractMismatch: return "contract-mismatch";
    case ModuleState::Failed: return "failed";
    case ModuleState::Stopped: return "stopped";
    }
    return "unknown";
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
    }
    return "unknown";
}

void Log(const std::string& message) {
    if (g_host && g_host->log) {
        g_host->log(g_host->context, kModuleId, message.c_str());
    }
}

void LogState(ModuleState state, const char* reason) {
    std::string message = "[voice-state] state=";
    message += StateName(state);
    if (reason && *reason) {
        message += " reason=";
        message += reason;
    }
    Log(message);
}

void ResetContract(MethodContract& contract) {
    contract.pointer = nullptr;
    contract.method_info = nullptr;
    contract.resolved = false;
}

bool ResolveContract(MethodContract& contract) {
    ResetContract(contract);
    if (!g_host || !g_host->resolve_method) {
        Log(std::string("[voice-contract] resolver unavailable key=") + contract.key);
        return false;
    }

    BE_ResolvedMethodV1 result{};
    const BE_Result status = g_host->resolve_method(
        g_host->context, &contract.descriptor, &result);
    if (status == BE_Result_Ok && result.method_pointer) {
        contract.pointer = result.method_pointer;
        contract.method_info = result.method_info;
        contract.resolved = true;
        std::string message = "[voice-contract] resolved key=";
        message += contract.key;
        message += " assembly=";
        message += contract.descriptor.assembly_name;
        message += " class=";
        message += contract.descriptor.namespace_name;
        if (*contract.descriptor.namespace_name) {
            message += ".";
        }
        message += contract.descriptor.class_name;
        message += " method=";
        message += contract.descriptor.method_name;
        Log(message);
        return true;
    }

    std::string message = "[voice-contract] missing key=";
    message += contract.key;
    message += " assembly=";
    message += contract.descriptor.assembly_name;
    message += " class=";
    message += contract.descriptor.namespace_name;
    if (*contract.descriptor.namespace_name) {
        message += ".";
    }
    message += contract.descriptor.class_name;
    message += " method=";
    message += contract.descriptor.method_name;
    message += " params=";
    message += std::to_string(contract.descriptor.parameter_count);
    message += " result=";
    message += ResultName(status);
    message += " required=";
    message += contract.required ? "true" : "false";
    Log(message);
    return false;
}

bool ResolveRuntimeContract() {
    g_state.store(ModuleState::Resolving, std::memory_order_release);
    LogState(ModuleState::Resolving, "dynamic metadata lookup started");

    size_t required_missing = 0;
    MethodContract* required[] = {
        &g_play_voice,
        &g_set_media,
        &g_unset_media,
        &g_external_event,
        &g_try_load_language_package,
        &g_get_language_name,
        &g_voice_language,
        &g_load_file_package,
        &g_unload_file_package,
    };
    for (MethodContract* contract : required) {
        if (!ResolveContract(*contract)) {
            ++required_missing;
        }
    }

    MethodContract* optional[] = {
        &g_internal_play_voice,
        &g_channel_play,
        &g_play_event,
        &g_narrative,
        &g_duration,
        &g_lip_dialog,
        &g_lip_path,
        &g_lip_load,
        &g_lip_real_actor,
        &g_lip_actor,
        &g_lip_trunk,
    };
    for (MethodContract* contract : optional) {
        ResolveContract(*contract);
    }

    const BE_FieldDescriptorV1 voice_data_field{
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Audio", "VoiceContext",
        "voiceData", "Beyond.Gameplay.Audio.RuntimeVoiceData"};
    const BE_FieldDescriptorV1 speaker_channel_field{
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Audio", "RuntimeVoiceData",
        "speakerChannel", "System.String"};
    const BE_Result voice_data_status = g_host->resolve_field(
        g_host->context, &voice_data_field, &g_voice_context_voice_data);
    const BE_Result speaker_status = g_host->resolve_field(
        g_host->context, &speaker_channel_field,
        &g_runtime_voice_data_speaker_channel);
    Log(std::string("[voice-contract] field voiceData=") +
        ResultName(voice_data_status) +
        " RuntimeVoiceData.speakerChannel=" + ResultName(speaker_status));
    if (voice_data_status != BE_Result_Ok || speaker_status != BE_Result_Ok) {
        ++required_missing;
    }

    if (required_missing != 0) {
        char reason[128]{};
        std::snprintf(reason, sizeof(reason),
            "required dynamic voice/Wwise contracts missing=%zu", required_missing);
        g_state.store(ModuleState::ContractMismatch, std::memory_order_release);
        LogState(ModuleState::ContractMismatch, reason);
        Log("[voice-contract] module will not install a partial hook set");
        return false;
    }

    g_state.store(ModuleState::Ready, std::memory_order_release);
    LogState(ModuleState::Ready,
        "dynamic VoicePlayer and Wwise media contracts accepted");
    return true;
}

bool ShouldLog(uint64_t count) {
    return g_diagnostics_enabled.load(std::memory_order_relaxed) &&
        (count <= 32 || count % 256 == 0);
}

std::string SpeakerFromContext(void* voice_context) {
    if (!voice_context || !g_voice_context_voice_data.field_info ||
        g_voice_context_voice_data.offset < 0 ||
        !g_runtime_voice_data_speaker_channel.field_info ||
        g_runtime_voice_data_speaker_channel.offset < 0 ||
        !g_host->field_get_value_object || !g_host->copy_managed_string) {
        return {};
    }
    void* voice_data = g_host->field_get_value_object(g_host->context,
        g_voice_context_voice_data.field_info, voice_context);
    if (!voice_data) {
        return {};
    }
    void* speaker_channel = g_host->field_get_value_object(g_host->context,
        g_runtime_voice_data_speaker_channel.field_info, voice_data);
    char value[256]{};
    return g_host->copy_managed_string(g_host->context, speaker_channel, value,
        sizeof(value)) > 0 ? std::string(value) : std::string{};
}

std::string_view SpeakerCodename(std::string_view speaker) {
    if (!speaker.starts_with("chr_")) {
        return speaker;
    }
    const size_t separator = speaker.find('_', 4);
    return separator == std::string_view::npos || separator + 1 >= speaker.size()
        ? speaker
        : speaker.substr(separator + 1);
}

bool SpeakerMatches(std::string_view rule, std::string_view runtime) {
    if (_stricmp(std::string(rule).c_str(), std::string(runtime).c_str()) == 0) {
        return true;
    }
    const std::string rule_codename(SpeakerCodename(rule));
    const std::string runtime_codename(SpeakerCodename(runtime));
    return _stricmp(rule_codename.c_str(), runtime_codename.c_str()) == 0;
}

std::string Normalize(std::string value) {
    std::transform(value.begin(), value.end(), value.begin(),
        [](unsigned char item) { return static_cast<char>(std::tolower(item)); });
    return value;
}

std::string ManagedString(void* value, size_t capacity = 1024) {
    if (!value || !g_host || !g_host->copy_managed_string || capacity < 2) {
        return {};
    }
    std::vector<char> buffer(capacity);
    return g_host->copy_managed_string(g_host->context, value, buffer.data(),
        buffer.size()) > 0 ? std::string(buffer.data()) : std::string{};
}

bool TryReadManagedGetter(const MethodContract& contract, void* instance,
    void*& value) {
    value = nullptr;
    if (!contract.resolved || !contract.method_info || !instance ||
        !g_host || !g_host->runtime_invoke) {
        return false;
    }
    void* exception = nullptr;
    __try {
        value = g_host->runtime_invoke(g_host->context, contract.method_info,
            instance, nullptr, &exception);
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        value = nullptr;
        return false;
    }
    if (exception) {
        value = nullptr;
        return false;
    }
    return value != nullptr;
}

bool ContainsIdentityToken(std::string_view source, std::string_view identity) {
    if (source.empty() || identity.size() < 2) {
        return false;
    }
    size_t position = source.find(identity);
    while (position != std::string_view::npos) {
        const auto identifier = [](unsigned char item) {
            return std::isalnum(item) != 0;
        };
        const bool begins = position == 0 ||
            !identifier(static_cast<unsigned char>(source[position - 1]));
        const size_t end = position + identity.size();
        const bool ends = end == source.size() ||
            !identifier(static_cast<unsigned char>(source[end]));
        if (begins && ends) {
            return true;
        }
        position = source.find(identity, position + 1);
    }
    return false;
}

bool SelectRule(std::string_view identity, bool allow_token_match,
    VoiceRule& selected) {
    std::vector<VoiceRule> rules;
    {
        std::lock_guard lock(g_configuration_mutex);
        rules = g_configuration.rules;
    }
    const VoiceRule* wildcard = nullptr;
    for (const VoiceRule& rule : rules) {
        if (rule.speaker == "*") {
            wildcard = &rule;
            continue;
        }
        if ((!identity.empty() && SpeakerMatches(rule.speaker, identity)) ||
            (allow_token_match && ContainsIdentityToken(identity, rule.speaker))) {
            selected = rule;
            return true;
        }
    }
    if (wildcard) {
        selected = *wildcard;
        return true;
    }
    return false;
}

class ScopedVoiceRequestRoute {
public:
    explicit ScopedVoiceRequestRoute(const VoiceRule* rule) {
        if (!rule || rule->language < 0 || rule->language > 3) {
            return;
        }
        previous_ = g_voice_request_route;
        g_voice_request_route = {};
        g_voice_request_route.active = true;
        g_voice_request_route.language = rule->language;
        std::snprintf(g_voice_request_route.speaker,
            sizeof(g_voice_request_route.speaker), "%s", rule->speaker.c_str());
        applied_ = true;
    }

    ~ScopedVoiceRequestRoute() {
        if (applied_) {
            g_voice_request_route = previous_;
        }
    }

    ScopedVoiceRequestRoute(const ScopedVoiceRequestRoute&) = delete;
    ScopedVoiceRequestRoute& operator=(const ScopedVoiceRequestRoute&) = delete;

private:
    VoiceRequestRoute previous_{};
    bool applied_ = false;
};

const char* LanguagePathName(int language) {
    static constexpr const char* names[]{
        "chinese", "english", "japanese", "korean"
    };
    return language >= 0 && language < static_cast<int>(std::size(names))
        ? names[language] : nullptr;
}

bool BuildVoiceReplacementSource(const std::string& source, int language,
    std::string& replacement) {
    replacement.clear();
    const char* target = LanguagePathName(language);
    if (!target || !source.starts_with("voice/")) {
        return false;
    }
    const size_t language_start = std::string_view("voice/").size();
    const size_t language_end = source.find('/', language_start);
    if (language_end == std::string::npos || language_end + 1 >= source.size()) {
        return false;
    }
    const std::string suffix = source.substr(language_end + 1);
    if (suffix.find("characters/") == std::string::npos &&
        suffix.find("narrating/") == std::string::npos) {
        return false;
    }
    replacement = "voice/";
    replacement += target;
    replacement.push_back('/');
    replacement += suffix;
    return true;
}

bool IsNarrativeSource(std::string_view source) {
    return source.starts_with("voice/") &&
        source.find("/narrating/") != std::string_view::npos;
}

std::string ExtractVoiceLineId(std::string_view source) {
    if (source.empty()) {
        return {};
    }
    const size_t separator = source.find_last_of("/\\");
    std::string line(separator == std::string_view::npos
        ? source : source.substr(separator + 1));
    if (line.size() >= 4 && line.compare(line.size() - 4, 4, ".wem") == 0) {
        line.erase(line.size() - 4);
    }
    return Normalize(std::move(line));
}

void ClearPendingLipRoute() {
    g_pending_lip_route = {};
}

bool ArmPendingLipRoute(int language, std::string_view speaker,
    std::string_view source) {
    const std::string line_id = ExtractVoiceLineId(source);
    if (language < 0 || language > 3 || line_id.empty()) {
        ClearPendingLipRoute();
        return false;
    }
    PendingLipRoute pending;
    pending.armed = true;
    pending.language = language;
    pending.configuration_generation = g_configuration_generation.load(
        std::memory_order_acquire);
    std::snprintf(pending.line_id, sizeof(pending.line_id), "%s",
        line_id.c_str());
    const int speaker_length = static_cast<int>(std::min(
        speaker.size(), sizeof(pending.speaker) - 1));
    std::snprintf(pending.speaker, sizeof(pending.speaker), "%.*s",
        speaker_length, speaker.data());
    g_pending_lip_route = pending;
    return true;
}

bool TryGetCurrentLanguage(int& language) {
    language = -1;
    if (!g_voice_language.pointer) {
        return false;
    }
    __try {
        const void* entry = g_original_voice_language
            ? reinterpret_cast<const void*>(g_original_voice_language)
            : g_voice_language.pointer;
        language = reinterpret_cast<GetCurrentLanguageFn>(
            const_cast<void*>(entry))(nullptr);
        return language >= 0 && language <= 3;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        language = -1;
        return false;
    }
}

// Keep SEH in a leaf helper. MSVC rejects __try in a function that also owns
// C++ objects with destructors (for example std::lock_guard or std::string).
bool TryLoadLanguagePackageRaw(int language, void*& managed_language) {
    managed_language = nullptr;
    if (!g_get_language_name.pointer || !g_try_load_language_package.pointer) {
        return false;
    }
    bool loaded = false;
    __try {
        managed_language = reinterpret_cast<GetLanguageNameFn>(
            g_get_language_name.pointer)(language, nullptr);
        if (managed_language) {
            g_loading_auxiliary_package = true;
            loaded = reinterpret_cast<TryLoadLanguagePackageFn>(
                g_try_load_language_package.pointer)(managed_language, nullptr);
            g_loading_auxiliary_package = false;
        }
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        g_loading_auxiliary_package = false;
        loaded = false;
    }
    return loaded;
}

bool EnsureLanguagePackageReady(int language) {
    if (language < 0 || language > 3 || !g_try_load_language_package.pointer ||
        !g_get_language_name.pointer || !g_original_load_file_package ||
        !g_original_unload_file_package) {
        return false;
    }
    std::lock_guard lock(g_package_mutex);
    int current = -1;
    if (TryGetCurrentLanguage(current) && current == language) {
        g_language_package_ready[language] = true;
        return true;
    }
    if (g_language_package_ready[language]) {
        return true;
    }
    const uint64_t now = GetTickCount64();
    if (now < g_language_package_retry_at[language]) {
        return false;
    }

    void* managed_language = nullptr;
    const bool loaded = TryLoadLanguagePackageRaw(language, managed_language);
    g_language_package_ready[language] = loaded;
    g_language_package_retry_at[language] = loaded ? 0 : now + 5000;
    Log("[voice-pck] language=" + std::to_string(language) +
        " name=" + ManagedString(managed_language, 64) +
        " result=" + (loaded ? "success" : "failed"));
    return loaded;
}

int __fastcall HookGetCurrentLanguage(void* method) {
    const int duration_language = g_duration_language_override;
    const int lip_language = g_lip_language_override;
    const int override = duration_language >= 0 && duration_language <= 3
        ? duration_language
        : (lip_language >= 0 && lip_language <= 3 ? lip_language : -1);
    if (override >= 0) {
        const uint64_t hit = g_language_override_hits.fetch_add(
            1, std::memory_order_relaxed) + 1;
        if (ShouldLog(hit)) {
            Log("[voice-language] thread-local override=" +
                std::to_string(override));
        }
        return override;
    }
    return g_original_voice_language
        ? g_original_voice_language(method) : 0;
}

bool EnsureCatalogForSpeaker(const std::string& speaker) {
    VoiceRule selected;
    const bool matched = SelectRule(speaker, false, selected);
    if (!matched || selected.language < 0) {
        // An unconfigured speaker must never evict the resident routes for
        // configured characters. FollowGlobal rules also need no media map.
        return true;
    }
    std::lock_guard lock(g_catalog_mutex);
    if (!g_catalog_loaded) {
        return false;
    }
    if (!g_catalog_applied) {
        if (!g_catalog_unset_routes.empty()) {
            Log("[voice-catalog] deferred activation blocked by an incomplete "
                "registration cleanup");
            return false;
        }
        if (!ApplyCatalog()) {
            Log("[voice-catalog] deferred activation was not accepted; "
                "the next configured voice will retry");
            return false;
        }
        Log("[voice-catalog] deferred resident routes activated at the first "
            "configured voice");
    }
    if (g_catalog_loaded && g_catalog_applied &&
        !g_catalog_missing_routes.empty() && g_original_set_media) {
        std::unordered_set<uint32_t> still_missing;
        for (const NativeAkSourceSettings& route : g_catalog_routes) {
            if (!g_catalog_missing_routes.contains(route.source_id)) {
                continue;
            }
            NativeAkSourceSettings request = route;
            const int result = g_original_set_media(&request, 1, nullptr);
            if (result != kAkSuccess) {
                still_missing.insert(route.source_id);
            }
        }
        if (still_missing.size() != g_catalog_missing_routes.size()) {
            Log("[voice-catalog] repaired routes=" +
                std::to_string(g_catalog_missing_routes.size() -
                    still_missing.size()) + " remaining=" +
                std::to_string(still_missing.size()));
        }
        g_catalog_missing_routes = std::move(still_missing);
    }
    return g_catalog_loaded && g_catalog_applied &&
        g_catalog_unset_routes.size() == g_catalog_routes.size() &&
        g_catalog_missing_routes.empty();
}

void __fastcall HookPlayVoice(void* voice_context, void* method) {
    const uint64_t hit = g_play_voice_hits.fetch_add(1, std::memory_order_relaxed) + 1;
    const std::string speaker = SpeakerFromContext(voice_context);
    VoiceRule rule;
    const bool matched = SelectRule(speaker, false, rule) && rule.language >= 0;
    const bool catalog_ready = EnsureCatalogForSpeaker(speaker);
    if (ShouldLog(hit)) {
        Log("[voice-hook] VoicePlayer.PlayVoice hit=" + std::to_string(hit) +
            " speaker=" + (speaker.empty() ? "<unknown>" : speaker) +
            " target=" + (matched ? std::to_string(rule.language) : "global") +
            " catalogReady=" + (catalog_ready ? "true" : "false") +
            " dynamic=true");
    }
    const ScopedVoiceRequestRoute route(
        matched && catalog_ready ? &rule : nullptr);
    if (g_original_play_voice) {
        g_original_play_voice(voice_context, method);
    }
}

// Keep the SEH dereference in a leaf helper. VoicePlayer._PlayVoice receives a
// managed by-reference argument, while the public and channel entry points
// receive the VoiceContext object directly.
void* TryDereferenceVoiceContext(void* voice_context_reference) {
    if (!voice_context_reference) {
        return nullptr;
    }
    __try {
        return *static_cast<void**>(voice_context_reference);
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

uint32_t __fastcall HookInternalPlayVoice(void* voice_context_reference,
    void* method) {
    const uint64_t hit = g_internal_play_voice_hits.fetch_add(
        1, std::memory_order_relaxed) + 1;
    void* voice_context = TryDereferenceVoiceContext(voice_context_reference);
    const std::string speaker = SpeakerFromContext(voice_context);
    VoiceRule rule;
    const bool matched = SelectRule(speaker, false, rule) && rule.language >= 0;
    const bool catalog_ready = EnsureCatalogForSpeaker(speaker);
    if (ShouldLog(hit)) {
        Log("[voice-hook] VoicePlayer._PlayVoice(ref) hit=" +
            std::to_string(hit) + " speaker=" +
            (speaker.empty() ? "<unknown>" : speaker) + " target=" +
            (matched ? std::to_string(rule.language) : "global") +
            " catalogReady=" + (catalog_ready ? "true" : "false"));
    }
    const ScopedVoiceRequestRoute route(
        matched && catalog_ready ? &rule : nullptr);
    return g_original_internal_play_voice
        ? g_original_internal_play_voice(voice_context_reference, method) : 0;
}

void __fastcall HookChannelPlay(void* instance, void* voice_context, void* method) {
    const uint64_t hit = g_channel_play_hits.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const std::string speaker = SpeakerFromContext(voice_context);
    VoiceRule rule;
    const bool matched = SelectRule(speaker, false, rule) && rule.language >= 0;
    const bool catalog_ready = EnsureCatalogForSpeaker(speaker);
    if (ShouldLog(hit)) {
        Log("[voice-hook] VoiceSpeakChannelProcessor._PlayVoice hit=" +
            std::to_string(hit) + " speaker=" +
            (speaker.empty() ? "<unknown>" : speaker) +
            " target=" + (matched ? std::to_string(rule.language) : "global") +
            " catalogReady=" + (catalog_ready ? "true" : "false"));
    }
    const ScopedVoiceRequestRoute route(
        matched && catalog_ready ? &rule : nullptr);
    if (g_original_channel_play) {
        g_original_channel_play(instance, voice_context, method);
    }
}

uint32_t __fastcall HookPlayEvent(void* event_name, uint64_t audio_object_id,
    uint32_t handle_id, void* method) {
    const uint64_t hit = g_event_hits.fetch_add(1, std::memory_order_relaxed) + 1;
    if (ShouldLog(hit)) {
        bool catalog_ready = false;
        {
            std::lock_guard lock(g_catalog_mutex);
            catalog_ready = g_catalog_loaded && g_catalog_applied &&
                g_catalog_missing_routes.empty();
        }
        Log("[voice-hook] VoicePlayer._PlayEvent hit=" + std::to_string(hit) +
            " audioObject=" + std::to_string(audio_object_id) +
            " handle=" + std::to_string(handle_id) +
            " requestScope=" + (g_voice_request_route.active ? "active" : "none") +
            " catalogReady=" + (catalog_ready ? "true" : "false"));
    }
    return g_original_play_event
        ? g_original_play_event(event_name, audio_object_id, handle_id, method)
        : 0;
}

uint32_t __fastcall HookNarrative(void* instance, void* voice_id,
    uint64_t audio_object_id, void* config, void* method) {
    const uint64_t hit = g_narrative_hits.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const std::string identity = Normalize(ManagedString(voice_id));
    VoiceRule rule;
    const bool narrative_enabled = g_replace_narrative.load(
        std::memory_order_relaxed);
    bool catalog_ready = false;
    if (narrative_enabled) {
        // Narrative IDs include the line key, so allow token matching to map
        // e.g. chr_0013_aglina_sim_talk_* to the configured aglina rule.
        if (SelectRule(identity, true, rule) && rule.language >= 0) {
            catalog_ready = EnsureCatalogForSpeaker(rule.speaker == "*"
                ? std::string{} : rule.speaker);
        }
    }
    const bool matched = narrative_enabled && rule.language >= 0 &&
        catalog_ready;
    if (ShouldLog(hit)) {
        Log("[voice-hook] VoiceManager._SpeakNarrative hit=" +
            std::to_string(hit) + " identity=" +
            (identity.empty() ? "<unknown>" : identity) +
            " replacement=" + (narrative_enabled ? "enabled" : "disabled") +
            " target=" + (matched ? std::to_string(rule.language) : "global") +
            " catalogReady=" + (catalog_ready ? "true" : "false"));
    }
    const ScopedVoiceRequestRoute route(matched ? &rule : nullptr);
    return g_original_narrative
        ? g_original_narrative(instance, voice_id, audio_object_id, config, method)
        : 0;
}

bool TryReadFloat(float* value, float& result) {
    result = -1.0f;
    if (!value) {
        return false;
    }
    __try {
        result = *value;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        result = -1.0f;
        return false;
    }
    return true;
}

bool TryWriteFloat(float* value, float result) {
    if (!value) {
        return false;
    }
    __try {
        *value = result;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
    return true;
}

bool __fastcall HookDuration(void* voice_id, float* duration, void* method) {
    const uint64_t hit = g_duration_hits.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const std::string identity = Normalize(ManagedString(voice_id));
    VoiceRule rule;
    const bool matched = SelectRule(identity, true, rule) && rule.language >= 0;
    const int target = matched ? rule.language : -1;
    const int current = [&]() {
        int value = -1;
        TryGetCurrentLanguage(value);
        return value;
    }();

    bool result = g_original_duration
        ? g_original_duration(voice_id, duration, method) : false;
    float global_duration = -1.0f;
    TryReadFloat(duration, global_duration);
    float routed_duration = global_duration;
    bool override_applied = false;
    if (matched && target != current && g_original_duration) {
        const int previous = g_duration_language_override;
        g_duration_language_override = target;
        const bool routed_result = g_original_duration(voice_id, duration, method);
        g_duration_language_override = previous;
        if (routed_result && TryReadFloat(duration, routed_duration) &&
            routed_duration > 0.0f) {
            result = true;
            override_applied = true;
        } else {
            TryWriteFloat(duration, global_duration);
            routed_duration = global_duration;
        }
    }
    if (ShouldLog(hit) && matched) {
        Log("[voice-duration] identity=" +
            (identity.empty() ? "<unknown>" : identity) +
            " target=" + std::to_string(target) +
            " global=" + std::to_string(global_duration) +
            " routed=" + std::to_string(routed_duration) +
            " override=" + (override_applied ? "true" : "false"));
    }
    return result;
}

std::string ReadLipActor(void* action_data) {
    void* value = nullptr;
    if (TryReadManagedGetter(g_lip_real_actor, action_data, value)) {
        const std::string actor = Normalize(ManagedString(value));
        if (!actor.empty()) {
            return actor;
        }
    }
    if (TryReadManagedGetter(g_lip_actor, action_data, value)) {
        return Normalize(ManagedString(value));
    }
    return {};
}

std::string ReadLipTrunk(void* action_data) {
    void* value = nullptr;
    return TryReadManagedGetter(g_lip_trunk, action_data, value)
        ? Normalize(ManagedString(value)) : std::string{};
}

void __fastcall HookLipDialog(void* instance, void* action_data,
    void* entity, void* method) {
    const uint64_t hit = g_lip_dialog_hits.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const std::string actor = ReadLipActor(action_data);
    const std::string trunk = ReadLipTrunk(action_data);
    VoiceRule rule;
    const bool matched = g_replace_narrative.load(std::memory_order_relaxed) &&
        SelectRule(actor, false, rule) && rule.language >= 0;
    const int previous = g_lip_language_override;
    if (matched) {
        g_lip_language_override = rule.language;
    }
    if (ShouldLog(hit)) {
        Log("[lip-route] dialog hit=" + std::to_string(hit) +
            " actor=" + (actor.empty() ? "<unknown>" : actor) +
            " trunk=" + (trunk.empty() ? "<unknown>" : trunk) +
            " target=" + (matched ? std::to_string(rule.language) : "global"));
    }
    if (g_original_lip_dialog) {
        g_original_lip_dialog(instance, action_data, entity, method);
    }
    g_lip_language_override = previous;
}

void* __fastcall HookLipPath(int language, void* voice_id, void* suffix,
    void* method) {
    const int override = g_lip_language_override;
    const int routed_language = override >= 0 && override <= 3
        ? override : language;
    const uint64_t hit = g_lip_path_hits.fetch_add(
        1, std::memory_order_relaxed) + 1;
    void* result = g_original_lip_path
        ? g_original_lip_path(routed_language, voice_id, suffix, method)
        : nullptr;
    if (ShouldLog(hit)) {
        const std::string voice = ManagedString(voice_id);
        const std::string suffix_text = ManagedString(suffix);
        const std::string path = ManagedString(result);
        Log("[lip-route] path hit=" + std::to_string(hit) +
            " language=" + std::to_string(language) +
            " -> " + std::to_string(routed_language) +
            " voice=" + (voice.empty() ? "<empty>" : voice) +
            " suffix=" + (suffix_text.empty() ? "<empty>" : suffix_text) +
            " result=" + (path.empty() ? "<empty>" : path));
    }
    return result;
}

bool __fastcall HookLipLoad(void* line_id, void** track, void* method) {
    const uint64_t hit = g_lip_load_hits.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const std::string line = ExtractVoiceLineId(
        Normalize(ManagedString(line_id)));
    const PendingLipRoute pending = g_pending_lip_route;
    const uint64_t generation = g_configuration_generation.load(
        std::memory_order_acquire);
    const bool pending_stale = pending.armed &&
        pending.configuration_generation != generation;
    const bool pending_matches = pending.armed && !pending_stale &&
        !line.empty() && line == pending.line_id;
    if (pending_stale) {
        ClearPendingLipRoute();
    }

    const int previous = g_lip_language_override;
    const bool dialog_routed = previous >= 0 && previous <= 3;
    const bool routed = pending_matches || dialog_routed;
    const int target = pending_matches ? pending.language : previous;
    if (routed) {
        g_lip_language_override = target;
    }
    bool result = g_original_lip_load
        ? g_original_lip_load(line_id, track, method) : false;
    bool fallback = false;
    if (routed && !result && g_original_lip_load) {
        g_lip_language_override = -1;
        result = g_original_lip_load(line_id, track, method);
        fallback = result;
    }
    g_lip_language_override = previous;
    if (pending_matches) {
        ClearPendingLipRoute();
    }

    if (ShouldLog(hit)) {
        const char* pending_state = pending_matches ? "matched"
            : (pending_stale ? "stale" : (pending.armed ? "mismatch" : "none"));
        Log("[lip-route] load hit=" + std::to_string(hit) +
            " line=" + (line.empty() ? "<empty>" : line) +
            " routed=" + (routed ? "true" : "false") +
            " target=" + (routed ? std::to_string(target) : "global") +
            " pending=" + pending_state +
            " speaker=" + (pending.speaker[0]
                ? std::string(pending.speaker) : "<none>") +
            " result=" + (result ? "true" : "false") +
            " fallback=" + (fallback ? "true" : "false"));
    }
    return result;
}

uint32_t __fastcall HookExternalEvent(void* event_name, uint64_t audio_object_id,
    void* external_source_key, uint32_t external_cookie, uint32_t callback_type,
    void* callback, void* cookie, uint32_t codec, void* method) {
    const uint64_t hit = g_external_hits.fetch_add(1, std::memory_order_relaxed) + 1;
    const std::string event = Normalize(ManagedString(event_name));
    const std::string source = Normalize(ManagedString(external_source_key));
    VoiceRule rule;
    const bool narrative_blocked = IsNarrativeSource(source) &&
        !g_replace_narrative.load(std::memory_order_relaxed);
    const char* matched_by = "none";
    bool matched = false;
    if (!narrative_blocked && g_voice_request_route.active &&
        g_voice_request_route.language >= 0 &&
        g_voice_request_route.language <= 3) {
        rule.speaker = g_voice_request_route.speaker;
        rule.language = g_voice_request_route.language;
        matched = true;
        matched_by = "request";
    }
    if (!narrative_blocked && !matched && SelectRule(source, true, rule) &&
        rule.language >= 0) {
        matched = true;
        matched_by = "source";
    }
    if (!narrative_blocked && !matched && SelectRule(event, true, rule) &&
        rule.language >= 0) {
        matched = true;
        matched_by = "event";
    }
    std::string replacement;
    void* routed_source = external_source_key;
    bool replaced = false;
    if (matched && BuildVoiceReplacementSource(source, rule.language, replacement) &&
        replacement != source && EnsureLanguagePackageReady(rule.language)) {
        routed_source = g_host->string_new(g_host->context, replacement.c_str());
        replaced = routed_source != nullptr;
    }
    const bool narrative = IsNarrativeSource(source);
    bool lip_armed = false;
    if (narrative) {
        if (matched && replaced) {
            lip_armed = ArmPendingLipRoute(rule.language, rule.speaker, source);
        } else {
            ClearPendingLipRoute();
        }
    }
    if (ShouldLog(hit)) {
        Log("[voice-external] hit=" + std::to_string(hit) +
            " matched=" + (matched ? rule.speaker : "<none>") +
            " matchedBy=" + matched_by +
            " narrative=" + (narrative ? "true" : "false") +
            " replaced=" + (replaced ? "true" : "false") +
            " lipArmed=" + (lip_armed ? "true" : "false") +
            " event=" + (event.empty() ? "<empty>" : event) +
            " source=" + (source.empty() ? "<empty>" : source) +
            " target=" + (replacement.empty() ? "<unchanged>" : replacement));
    }
    const uint32_t result = g_original_external_event
        ? g_original_external_event(event_name, audio_object_id, routed_source,
            external_cookie, callback_type, callback, cookie, codec, method)
        : 0;
    if (lip_armed && result == 0) {
        ClearPendingLipRoute();
    }
    return result;
}

int __fastcall HookLoadFilePackage(void* package_path, uint32_t* package_id,
    void* method) {
    const int result = g_original_load_file_package
        ? g_original_load_file_package(package_path, package_id, method)
        : 0;
    if (g_loading_auxiliary_package) {
        const uint64_t hit =
            g_package_load_hits.fetch_add(1, std::memory_order_relaxed) + 1;
        if (ShouldLog(hit)) {
            Log("[voice-pck] auxiliary-load hit=" + std::to_string(hit) +
                " result=" + std::to_string(result) +
                " packageId=" + std::to_string(package_id ? *package_id : 0) +
                " path=" + ManagedString(package_path));
        }
    }
    return result;
}

int __fastcall HookUnloadFilePackage(uint32_t package_id, void* method) {
    if (g_loading_auxiliary_package) {
        const uint64_t hit =
            g_package_unload_hits.fetch_add(1, std::memory_order_relaxed) + 1;
        if (ShouldLog(hit)) {
            Log("[voice-pck] preserved packageId=" + std::to_string(package_id) +
                " while mounting auxiliary language");
        }
        return kAkSuccess;
    }
    return g_original_unload_file_package
        ? g_original_unload_file_package(package_id, method)
        : 0;
}

bool TryReadMediaIds(void* settings, uint32_t count, uint32_t* destination) {
    if (!settings || !destination || count == 0) {
        return false;
    }
    __try {
        const auto* values = static_cast<const NativeAkSourceSettings*>(settings);
        for (uint32_t index = 0; index < count; ++index) {
            destination[index] = values[index].source_id;
        }
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
    return true;
}

void ReassertCatalogRoutes(void* settings, uint32_t count, const char* source) {
    if (!settings || count == 0 || count > 65536 || !g_original_set_media) {
        return;
    }
    std::vector<uint32_t> media_ids(count);
    if (!TryReadMediaIds(settings, count, media_ids.data())) {
        Log(std::string("[voice-catalog] unable to inspect game ") + source +
            " request");
        return;
    }
    std::unordered_set<uint32_t> touched;
    touched.reserve(count);
    touched.insert(media_ids.begin(), media_ids.end());

    std::lock_guard lock(g_catalog_mutex);
    if (!g_catalog_applied || g_catalog_routes.empty()) {
        return;
    }
    size_t restored = 0;
    bool failed = false;
    for (const NativeAkSourceSettings& route : g_catalog_routes) {
        if (!touched.contains(route.source_id)) {
            continue;
        }
        NativeAkSourceSettings request = route;
        const int result = g_original_set_media(&request, 1, nullptr);
        if (result == kAkSuccess) {
            ++restored;
            g_catalog_missing_routes.erase(route.source_id);
        } else {
            failed = true;
            g_catalog_missing_routes.insert(route.source_id);
            Log("[voice-catalog] reassert failed source=" +
                std::to_string(route.source_id) + " result=" +
                std::to_string(result));
        }
    }
    if (restored != 0 || failed) {
        Log(std::string("[voice-catalog] game ") + source +
            " touched active routes restored=" + std::to_string(restored) +
            " failed=" + (failed ? "true" : "false") +
            " missing=" + std::to_string(g_catalog_missing_routes.size()));
    }
}

int __fastcall HookSetMedia(void* settings, uint32_t count, void* method) {
    const uint64_t hit = g_set_media_hits.fetch_add(1, std::memory_order_relaxed) + 1;
    if (ShouldLog(hit)) {
        Log("[voice-wwise] CSharp_SetMedia hit=" + std::to_string(hit) +
            " count=" + std::to_string(count) +
            " catalog-routing=guarded pass-through=true");
    }
    const int result = g_original_set_media
        ? g_original_set_media(settings, count, method) : 0;
    if (result == kAkSuccess) {
        ReassertCatalogRoutes(settings, count, "SetMedia");
    }
    return result;
}

int __fastcall HookUnsetMedia(void* settings, uint32_t count, void* method) {
    const uint64_t hit = g_unset_media_hits.fetch_add(1, std::memory_order_relaxed) + 1;
    if (ShouldLog(hit)) {
        Log("[voice-wwise] CSharp_UnsetMedia hit=" + std::to_string(hit) +
            " count=" + std::to_string(count) +
            " catalog-routing=guarded pass-through=true");
    }
    const int result = g_original_unset_media
        ? g_original_unset_media(settings, count, method) : 0;
    if (result == kAkSuccess) {
        ReassertCatalogRoutes(settings, count, "UnsetMedia");
    }
    return result;
}

bool InstallHook(const char* key, MethodContract& contract, void* detour,
    void** original, bool required) {
    if (!contract.resolved || !contract.pointer) {
        if (required) {
            Log(std::string("[voice-hook] required target unavailable key=") + key);
            return false;
        }
        return true;
    }
    const BE_Result status = g_host->create_hook(
        g_host->context, kModuleId, contract.pointer, detour, original);
    if (status != BE_Result_Ok) {
        Log(std::string("[voice-hook] failed key=") + key +
            " result=" + ResultName(status));
        return !required;
    }
    Log(std::string("[voice-hook] installed key=") + key + " dynamic=true");
    return true;
}

std::filesystem::path ResolveCatalogPath(const std::filesystem::path& root,
    int language, const std::string& speaker) {
    static constexpr const char* languages[] = {
        "chinese", "english", "japanese", "korean"
    };
    const std::string prefix = std::string("voice.") + languages[language];
    std::filesystem::path path = root /
        (prefix + (speaker.empty() ? "" : "." + speaker) + ".becat");
    if (!speaker.empty() && !std::filesystem::is_regular_file(path)) {
        const std::string suffix = "_" + speaker + ".becat";
        std::error_code error;
        for (const auto& entry : std::filesystem::directory_iterator(root, error)) {
            const std::string name = entry.path().filename().string();
            if (!error && entry.is_regular_file(error) &&
                name.starts_with(prefix + ".chr_") &&
                name.ends_with(suffix)) {
                path = entry.path();
                break;
            }
        }
    }
    return path;
}

bool LoadCatalogFile(const std::filesystem::path& root, const VoiceRule& rule,
    ResidentCatalog& catalog) {
    const std::string speaker = rule.speaker == "*"
        ? std::string{} : rule.speaker;
    const std::filesystem::path path = ResolveCatalogPath(
        root, rule.language, speaker);
    std::ifstream stream(path, std::ios::binary | std::ios::ate);
    if (!stream) {
        Log("[voice-catalog] catalog file is missing: " + path.string());
        return false;
    }
    const std::streamsize size = stream.tellg();
    if (size < static_cast<std::streamsize>(sizeof(VoiceCatalogHeaderV1))) {
        Log("[voice-catalog] catalog file is truncated");
        return false;
    }
    stream.seekg(0, std::ios::beg);
    catalog = {};
    catalog.speaker = speaker;
    catalog.language = rule.language;
    catalog.wildcard = rule.speaker == "*";
    catalog.blob.resize(static_cast<size_t>(size));
    if (!stream.read(reinterpret_cast<char*>(catalog.blob.data()), size)) {
        catalog.blob.clear();
        Log("[voice-catalog] catalog file could not be read");
        return false;
    }

    const auto* header = reinterpret_cast<const VoiceCatalogHeaderV1*>(
        catalog.blob.data());
    const uint64_t blob_size = catalog.blob.size();
    const bool entries_in_bounds = header->entry_offset <= blob_size &&
        header->entry_count <=
            (blob_size - header->entry_offset) / sizeof(VoiceCatalogEntryV1);
    if (std::memcmp(header->magic, "BEVCAT01", 8) != 0 || header->version != 1 ||
        header->language != static_cast<uint16_t>(rule.language) ||
        !entries_in_bounds || header->data_offset > blob_size) {
        catalog = {};
        Log("[voice-catalog] catalog header validation failed");
        return false;
    }

    const auto* entries = reinterpret_cast<const VoiceCatalogEntryV1*>(
        catalog.blob.data() + header->entry_offset);
    catalog.entries.reserve(header->entry_count);
    for (uint32_t index = 0; index < header->entry_count; ++index) {
        const auto& entry = entries[index];
        if (entry.data_offset < header->data_offset ||
            entry.data_offset > blob_size ||
            entry.data_size > blob_size - entry.data_offset ||
            entry.source_media_id == 0 || entry.target_media_id == 0 ||
            entry.data_size == 0) {
            catalog = {};
            Log("[voice-catalog] catalog media bounds validation failed");
            return false;
        }
        catalog.entries.push_back({
            entry.source_media_id, entry.target_media_id,
            entry.data_offset, entry.data_size
        });
    }
    Log("[voice-catalog] staged entries=" +
        std::to_string(catalog.entries.size()) + " bytes=" +
        std::to_string(catalog.blob.size()) +
        " speaker=" + (speaker.empty() ? "*" : speaker));
    return !catalog.entries.empty();
}

bool LoadConfiguredCatalogs(const std::vector<VoiceRule>& rules) {
    if (!g_host || !g_host->copy_catalog_root) {
        Log("[voice-catalog] Host did not provide catalog access");
        return false;
    }
    if (!g_catalog_unset_routes.empty() || g_catalog_applied) {
        Log("[voice-catalog] refusing to replace resident storage while Wwise "
            "routes are registered");
        return false;
    }

    char root_buffer[4096]{};
    if (g_host->copy_catalog_root(g_host->context, root_buffer,
            sizeof(root_buffer)) <= 0) {
        Log("[voice-catalog] Host did not provide a catalog root");
        return false;
    }

    std::vector<VoiceRule> active_rules;
    active_rules.reserve(rules.size());
    for (const VoiceRule& rule : rules) {
        if (rule.language >= 0 && rule.language <= 3) {
            active_rules.push_back(rule);
        }
    }
    std::stable_sort(active_rules.begin(), active_rules.end(),
        [](const VoiceRule& left, const VoiceRule& right) {
            return left.speaker == "*" && right.speaker != "*";
        });

    std::vector<ResidentCatalog> staged;
    staged.reserve(active_rules.size());
    const std::filesystem::path root(root_buffer);
    uint64_t resident_bytes = 0;
    for (const VoiceRule& rule : active_rules) {
        ResidentCatalog catalog;
        if (!LoadCatalogFile(root, rule, catalog)) {
            return false;
        }
        resident_bytes += catalog.blob.size();
        staged.push_back(std::move(catalog));
    }

    struct Selection {
        size_t catalog_index = 0;
        size_t entry_index = 0;
        bool wildcard = false;
    };
    std::unordered_map<uint32_t, Selection> selected;
    for (size_t catalog_index = 0; catalog_index < staged.size();
        ++catalog_index) {
        const ResidentCatalog& catalog = staged[catalog_index];
        for (size_t entry_index = 0; entry_index < catalog.entries.size();
            ++entry_index) {
            const CatalogEntry& entry = catalog.entries[entry_index];
            const auto found = selected.find(entry.source_media_id);
            if (found == selected.end()) {
                selected.emplace(entry.source_media_id,
                    Selection{catalog_index, entry_index, catalog.wildcard});
                continue;
            }

            const Selection& previous = found->second;
            const CatalogEntry& previous_entry =
                staged[previous.catalog_index].entries[previous.entry_index];
            if (previous.wildcard && !catalog.wildcard) {
                found->second = {catalog_index, entry_index, false};
                continue;
            }
            if (!previous.wildcard && catalog.wildcard) {
                continue;
            }
            if (previous_entry.target_media_id != entry.target_media_id) {
                Log("[voice-catalog] conflicting Media route source=" +
                    std::to_string(entry.source_media_id) + " speakers=" +
                    (staged[previous.catalog_index].speaker.empty()
                        ? std::string("*")
                        : staged[previous.catalog_index].speaker) + "," +
                    (catalog.speaker.empty() ? std::string("*") : catalog.speaker));
                return false;
            }
        }
    }

    std::vector<uint32_t> source_ids;
    source_ids.reserve(selected.size());
    for (const auto& [source_id, ignored] : selected) {
        (void)ignored;
        source_ids.push_back(source_id);
    }
    std::sort(source_ids.begin(), source_ids.end());

    g_resident_catalogs = std::move(staged);
    g_catalog_routes.clear();
    g_catalog_unset_routes.clear();
    g_catalog_missing_routes.clear();
    g_catalog_routes.reserve(source_ids.size());
    for (uint32_t source_id : source_ids) {
        const Selection& selection = selected.at(source_id);
        const ResidentCatalog& catalog =
            g_resident_catalogs[selection.catalog_index];
        const CatalogEntry& entry = catalog.entries[selection.entry_index];
        g_catalog_routes.push_back({
            entry.source_media_id, 0,
            catalog.blob.data() + entry.data_offset, entry.data_size, 0
        });
    }
    g_catalog_loaded = !g_catalog_routes.empty();
    g_catalog_applied = false;
    Log("[voice-catalog] resident set ready catalogs=" +
        std::to_string(g_resident_catalogs.size()) + " entries=" +
        std::to_string(g_catalog_routes.size()) + " bytes=" +
        std::to_string(resident_bytes));
    return true;
}

bool ApplyCatalog() {
    if (g_catalog_routes.empty()) {
        g_catalog_loaded = false;
        g_catalog_applied = false;
        return true;
    }
    if (!g_catalog_loaded || !g_original_set_media) {
        Log("[voice-catalog] no resident media routes are available");
        return false;
    }
    if (g_catalog_applied &&
        g_catalog_unset_routes.size() == g_catalog_routes.size()) {
        return true;
    }
    if (!g_catalog_unset_routes.empty()) {
        Log("[voice-catalog] refusing SetMedia while an incomplete registration "
            "is still tracked");
        return false;
    }

    g_catalog_unset_routes.reserve(g_catalog_routes.size());
    g_catalog_missing_routes.clear();
    for (const NativeAkSourceSettings& route : g_catalog_routes) {
        const int result = g_original_set_media(
            const_cast<NativeAkSourceSettings*>(&route), 1, nullptr);
        if (result != kAkSuccess) {
            Log("[voice-catalog] CSharp_SetMedia rejected source=" +
                std::to_string(route.source_id) + " result=" +
                std::to_string(result) + " expected=1 applied=" +
                std::to_string(g_catalog_unset_routes.size()));
            g_catalog_applied = false;
            return false;
        }
        g_catalog_unset_routes.push_back(
            {route.source_id, 0, nullptr, 0, 0});
    }
    g_catalog_applied = true;
    Log("[voice-catalog] native media routes applied entries=" +
        std::to_string(g_catalog_routes.size()));
    return true;
}

bool UnapplyCatalog() {
    if (!g_catalog_unset_routes.empty()) {
        if (!g_original_unset_media) {
            Log("[voice-catalog] CSharp_UnsetMedia is unavailable; resident "
                "catalog memory was retained");
            return false;
        }
        std::vector<NativeAkSourceSettings> failed;
        failed.reserve(g_catalog_unset_routes.size());
        for (const NativeAkSourceSettings& route : g_catalog_unset_routes) {
            NativeAkSourceSettings request = route;
            const int result = g_original_unset_media(&request, 1, nullptr);
            if (result != kAkSuccess) {
                failed.push_back(route);
                Log("[voice-catalog] CSharp_UnsetMedia rejected source=" +
                    std::to_string(route.source_id) + " result=" +
                    std::to_string(result) + " expected=1");
            }
        }
        g_catalog_unset_routes = std::move(failed);
        g_catalog_applied = false;
        if (!g_catalog_unset_routes.empty()) {
            Log("[voice-catalog] unload incomplete; registered=" +
                std::to_string(g_catalog_unset_routes.size()) +
                " catalog memory remains resident");
            return false;
        }
    }
    g_catalog_loaded = false;
    g_catalog_applied = false;
    g_catalog_routes.clear();
    g_catalog_unset_routes.clear();
    g_catalog_missing_routes.clear();
    g_resident_catalogs.clear();
    return true;
}

void ClearOriginals() {
    g_original_play_voice = nullptr;
    g_original_internal_play_voice = nullptr;
    g_original_channel_play = nullptr;
    g_original_play_event = nullptr;
    g_original_narrative = nullptr;
    g_original_duration = nullptr;
    g_original_voice_language = nullptr;
    g_original_lip_dialog = nullptr;
    g_original_lip_path = nullptr;
    g_original_lip_load = nullptr;
    g_original_set_media = nullptr;
    g_original_unset_media = nullptr;
    g_original_external_event = nullptr;
    g_original_load_file_package = nullptr;
    g_original_unload_file_package = nullptr;
}

bool InstallHooks() {
    const ModuleState state = g_state.load(std::memory_order_acquire);
    if (!g_host || !g_host->create_hook ||
        (state != ModuleState::Ready && state != ModuleState::Disabled)) {
        LogState(ModuleState::Failed, "hook broker unavailable or contract not ready");
        g_state.store(ModuleState::Failed, std::memory_order_release);
        return false;
    }

    const bool required =
        InstallHook("voice.player.play", g_play_voice,
            reinterpret_cast<void*>(&HookPlayVoice),
            reinterpret_cast<void**>(&g_original_play_voice), true) &&
        InstallHook("wwise.media.set", g_set_media,
            reinterpret_cast<void*>(&HookSetMedia),
            reinterpret_cast<void**>(&g_original_set_media), true) &&
        InstallHook("wwise.media.unset", g_unset_media,
            reinterpret_cast<void*>(&HookUnsetMedia),
            reinterpret_cast<void**>(&g_original_unset_media), true) &&
        InstallHook("audio.external-event", g_external_event,
            reinterpret_cast<void*>(&HookExternalEvent),
            reinterpret_cast<void**>(&g_original_external_event), true) &&
        InstallHook("wwise.package.load", g_load_file_package,
            reinterpret_cast<void*>(&HookLoadFilePackage),
            reinterpret_cast<void**>(&g_original_load_file_package), true) &&
        InstallHook("wwise.package.unload", g_unload_file_package,
            reinterpret_cast<void*>(&HookUnloadFilePackage),
            reinterpret_cast<void**>(&g_original_unload_file_package), true);
    if (!required) {
        if (g_host->release_module_hooks) {
            g_host->release_module_hooks(g_host->context, kModuleId);
        }
        ClearOriginals();
        g_state.store(ModuleState::Failed, std::memory_order_release);
        LogState(ModuleState::Failed, "required dynamic voice hooks could not be installed");
        return false;
    }

    // These routes are optional because different client builds may omit or
    // rename auxiliary voice paths. The core external-source route remains
    // active when an auxiliary contract is unavailable.
    InstallHook("voice.player.play-internal", g_internal_play_voice,
        reinterpret_cast<void*>(&HookInternalPlayVoice),
        reinterpret_cast<void**>(&g_original_internal_play_voice), false);
    InstallHook("voice.channel.play", g_channel_play,
        reinterpret_cast<void*>(&HookChannelPlay),
        reinterpret_cast<void**>(&g_original_channel_play), false);
    InstallHook("voice.player.event", g_play_event,
        reinterpret_cast<void*>(&HookPlayEvent),
        reinterpret_cast<void**>(&g_original_play_event), false);
    InstallHook("voice.manager.narrative", g_narrative,
        reinterpret_cast<void*>(&HookNarrative),
        reinterpret_cast<void**>(&g_original_narrative), false);
    InstallHook("voice.duration", g_duration,
        reinterpret_cast<void*>(&HookDuration),
        reinterpret_cast<void**>(&g_original_duration), false);

    const bool needs_language_override = g_duration.resolved ||
        g_lip_dialog.resolved || g_lip_path.resolved || g_lip_load.resolved;
    if (needs_language_override) {
        InstallHook("voice.language.override", g_voice_language,
            reinterpret_cast<void*>(&HookGetCurrentLanguage),
            reinterpret_cast<void**>(&g_original_voice_language), false);
    }
    InstallHook("lip.dialog.play", g_lip_dialog,
        reinterpret_cast<void*>(&HookLipDialog),
        reinterpret_cast<void**>(&g_original_lip_dialog), false);
    InstallHook("lip.track.path", g_lip_path,
        reinterpret_cast<void*>(&HookLipPath),
        reinterpret_cast<void**>(&g_original_lip_path), false);
    InstallHook("lip.track.load", g_lip_load,
        reinterpret_cast<void*>(&HookLipLoad),
        reinterpret_cast<void**>(&g_original_lip_load), false);

    std::vector<VoiceRule> configured_rules;
    {
        std::lock_guard lock(g_configuration_mutex);
        configured_rules = g_configuration.rules;
    }
    if (configured_rules.empty()) {
        if (g_host->release_module_hooks) {
            g_host->release_module_hooks(g_host->context, kModuleId);
        }
        ClearOriginals();
        g_state.store(ModuleState::Failed, std::memory_order_release);
        LogState(ModuleState::Failed, "voice routing is enabled without language rules");
        return false;
    }
    bool catalog_staged = false;
    bool cleanup_ready = true;
    {
        std::lock_guard lock(g_catalog_mutex);
        // Wwise may not be initialized when the Host configures modules. Keep
        // every configured catalog resident now, but preserve the old proven
        // activation point: the first matching VoicePlayer request.
        catalog_staged = LoadConfiguredCatalogs(configured_rules);
        if (!catalog_staged) {
            cleanup_ready = UnapplyCatalog();
        }
    }
    if (!catalog_staged) {
        bool hooks_released = false;
        if (cleanup_ready && g_host->release_module_hooks) {
            hooks_released = g_host->release_module_hooks(
                g_host->context, kModuleId) == BE_Result_Ok;
        }
        if (hooks_released) {
            ClearOriginals();
        }
        g_state.store(ModuleState::Failed, std::memory_order_release);
        LogState(ModuleState::Failed, cleanup_ready && hooks_released
            ? "configured voice catalogs could not be staged"
            : "catalog staging cleanup failed; process teardown is required");
        return false;
    }

    g_state.store(ModuleState::Active, std::memory_order_release);
    Log("[voice-route] external WEM, auxiliary PCK, and merged resident catalog routing active");
    Log("[voice-route] all configured Media routes are resident; Wwise activation "
        "is deferred until the first configured voice");
    return true;
}

bool StopHooks() {
    {
        std::lock_guard lock(g_catalog_mutex);
        if (!UnapplyCatalog()) {
            Log("[voice-catalog] hooks were retained because Wwise still owns "
                "one or more pointers into catalog memory");
            return false;
        }
    }
    if (g_host && g_host->release_module_hooks) {
        const BE_Result result = g_host->release_module_hooks(
            g_host->context, kModuleId);
        if (result != BE_Result_Ok) {
            Log("[voice-hook] release failed result=" +
                std::string(ResultName(result)));
            return false;
        }
    }
    ClearOriginals();
    return true;
}

bool ParseConfiguration(const char* text, VoiceConfiguration& output,
    std::string& error) {
    output = {};
    if (!text) {
        return true;
    }
    std::string_view remaining(text);
    while (!remaining.empty()) {
        const size_t line_end = remaining.find('\n');
        std::string_view line = remaining.substr(0, line_end);
        remaining = line_end == std::string_view::npos
            ? std::string_view{}
            : remaining.substr(line_end + 1);
        line = Trim(line);
        if (line.empty() || line.front() == ';' || line.front() == '#') {
            continue;
        }
        const size_t separator = line.find('=');
        if (separator == std::string_view::npos) {
            continue;
        }
        const std::string key(Trim(line.substr(0, separator)));
        const std::string_view value = Trim(line.substr(separator + 1));
        bool parsed = false;
        if (key == "enabled" || key == "voice_router_enabled") {
            if (!ParseBool(value, parsed)) {
                error = key + " must be a boolean";
                return false;
            }
            output.enabled = parsed;
        } else if (key == "voice_diagnostics") {
            if (!ParseBool(value, parsed)) {
                error = "voice_diagnostics must be a boolean";
                return false;
            }
            output.diagnostics = parsed;
        } else if (key == "replace_narrative_voice") {
            if (!ParseBool(value, parsed)) {
                error = "replace_narrative_voice must be a boolean";
                return false;
            }
            output.replace_narrative = parsed;
        } else if (key == "voice_language_rules") {
            output.language_rules.assign(value);
        }
    }
    std::string rules = output.language_rules;
    while (!rules.empty()) {
        const size_t separator = rules.find_first_of(",;");
        const std::string item = std::string(Trim(rules.substr(0, separator)));
        const size_t equals = item.find('=');
        const size_t colon = item.find(':');
        size_t pair_separator = std::string::npos;
        if (equals != std::string::npos && colon != std::string::npos) {
            pair_separator = std::min(equals, colon);
        } else if (equals != std::string::npos) {
            pair_separator = equals;
        } else {
            pair_separator = colon;
        }
        if (pair_separator == std::string::npos || pair_separator == 0 ||
            pair_separator + 1 >= item.size()) {
            error = "voice_language_rules contains an invalid rule";
            return false;
        }
        std::string speaker(Trim(item.substr(0, pair_separator)));
        std::transform(speaker.begin(), speaker.end(), speaker.begin(),
            [](unsigned char value) { return static_cast<char>(std::tolower(value)); });
        const int language = ParseLanguage(item.substr(pair_separator + 1));
        if (language < -1) {
            error = "voice_language_rules contains an unsupported language";
            return false;
        }
        const auto existing = std::find_if(output.rules.begin(), output.rules.end(),
            [&speaker](const VoiceRule& rule) { return rule.speaker == speaker; });
        if (existing == output.rules.end()) {
            output.rules.push_back({std::move(speaker), language});
        } else {
            existing->language = language;
        }
        if (separator == std::string::npos) break;
        rules.erase(0, separator + 1);
    }
    return true;
}

bool SameRoutingRules(const std::vector<VoiceRule>& left,
    const std::vector<VoiceRule>& right) {
    if (left.size() != right.size()) {
        return false;
    }
    for (size_t index = 0; index < left.size(); ++index) {
        if (left[index].speaker != right[index].speaker ||
            left[index].language != right[index].language) {
            return false;
        }
    }
    return true;
}

void CommitConfiguration(VoiceConfiguration configuration) {
    const bool enabled = configuration.enabled;
    const bool diagnostics = configuration.diagnostics;
    const bool replace_narrative = configuration.replace_narrative;
    const bool has_rules = !configuration.rules.empty();
    {
        std::lock_guard lock(g_configuration_mutex);
        g_configuration = std::move(configuration);
    }
    g_configuration_generation.fetch_add(1, std::memory_order_acq_rel);
    g_diagnostics_enabled.store(diagnostics, std::memory_order_release);
    g_replace_narrative.store(replace_narrative, std::memory_order_release);
    Log("[voice-config] enabled=" + std::string(enabled ? "true" : "false") +
        " diagnostics=" + (diagnostics ? "true" : "false") +
        " narrative=" + (replace_narrative ? "true" : "false") +
        " rules=" + (has_rules ? "configured" : "none"));
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->resolve_method || !host->create_hook ||
        !host->release_module_hooks || !host->resolve_field ||
        !host->copy_catalog_root || !host->copy_managed_string || !host->string_new ||
        !host->runtime_invoke || !host->field_get_value_object || !host->log) {
        return BE_Result_ContractMismatch;
    }
    g_host = host;
    g_configuration = {};
    g_configuration_generation.store(1, std::memory_order_release);
    g_play_voice_hits.store(0, std::memory_order_release);
    g_internal_play_voice_hits.store(0, std::memory_order_release);
    g_channel_play_hits.store(0, std::memory_order_release);
    g_event_hits.store(0, std::memory_order_release);
    g_narrative_hits.store(0, std::memory_order_release);
    g_duration_hits.store(0, std::memory_order_release);
    g_language_override_hits.store(0, std::memory_order_release);
    g_lip_dialog_hits.store(0, std::memory_order_release);
    g_lip_path_hits.store(0, std::memory_order_release);
    g_lip_load_hits.store(0, std::memory_order_release);
    g_set_media_hits.store(0, std::memory_order_release);
    g_unset_media_hits.store(0, std::memory_order_release);
    g_external_hits.store(0, std::memory_order_release);
    g_package_load_hits.store(0, std::memory_order_release);
    g_package_unload_hits.store(0, std::memory_order_release);
    {
        std::lock_guard lock(g_package_mutex);
        g_language_package_ready.fill(false);
        g_language_package_retry_at.fill(0);
    }
    if (!ResolveRuntimeContract()) {
        return BE_Result_ContractMismatch;
    }
    Log("[voice-config] waiting for BetterEndfield module configuration");
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char* configuration) {
    if (!g_host) {
        return BE_Result_NotReady;
    }

    VoiceConfiguration next;
    std::string error;
    if (!ParseConfiguration(configuration, next, error)) {
        Log("[voice-config] rejected configuration: " + error);
        return BE_Result_InvalidArgument;
    }
    if (next.enabled && next.rules.empty()) {
        Log("[voice-config] enabled voice routing requires at least one language rule");
        return BE_Result_InvalidArgument;
    }
    const ModuleState state = g_state.load(std::memory_order_acquire);
    if (state == ModuleState::ContractMismatch || state == ModuleState::Failed ||
        state == ModuleState::Stopped) {
        return BE_Result_ContractMismatch;
    }
    VoiceConfiguration previous;
    {
        std::lock_guard lock(g_configuration_mutex);
        previous = g_configuration;
    }

    if (!next.enabled) {
        if (state == ModuleState::Active && !StopHooks()) {
            LogState(ModuleState::Active,
                "disable refused because catalog cleanup was incomplete");
            return BE_Result_Failed;
        }
        CommitConfiguration(std::move(next));
        g_state.store(ModuleState::Disabled, std::memory_order_release);
        LogState(ModuleState::Disabled, "configuration disabled voice routing");
        return BE_Result_Ok;
    }
    if (state == ModuleState::Ready || state == ModuleState::Disabled) {
        CommitConfiguration(std::move(next));
        return InstallHooks() ? BE_Result_Ok : BE_Result_Failed;
    }
    if (state == ModuleState::Active) {
        if (SameRoutingRules(previous.rules, next.rules)) {
            CommitConfiguration(std::move(next));
            return BE_Result_Ok;
        }

        bool staged = false;
        bool old_routes_restored = false;
        bool cleanup_ready = true;
        {
            std::lock_guard lock(g_catalog_mutex);
            if (!UnapplyCatalog()) {
                Log("[voice-config] hot update refused because previous catalog "
                    "routes remain registered");
                return BE_Result_Failed;
            }
            staged = LoadConfiguredCatalogs(next.rules);
            if (!staged) {
                cleanup_ready = UnapplyCatalog();
                if (cleanup_ready) {
                    old_routes_restored =
                        LoadConfiguredCatalogs(previous.rules);
                }
            }
        }
        if (staged) {
            CommitConfiguration(std::move(next));
            Log("[voice-config] merged resident routes updated; Wwise activation "
                "will occur at the next configured voice");
            return BE_Result_Ok;
        }
        if (old_routes_restored) {
            Log("[voice-config] catalog update failed; previous resident routes "
                "restaged for deferred activation");
        } else if (!cleanup_ready) {
            Log("[voice-config] catalog update cleanup is incomplete; resident "
                "storage and hooks were retained");
        } else {
            Log("[voice-config] catalog update and previous-route restore both failed");
        }
        return BE_Result_Failed;
    }
    return BE_Result_NotReady;
}

void BE_CALL Shutdown() {
    if (g_original_play_voice || g_original_internal_play_voice ||
        g_original_set_media || g_original_unset_media ||
        g_original_external_event || g_original_load_file_package ||
        g_original_unload_file_package || g_original_channel_play ||
        g_original_play_event || g_original_narrative || g_original_duration ||
        g_original_voice_language || g_original_lip_dialog ||
        g_original_lip_path || g_original_lip_load) {
        if (!StopHooks()) {
            Log("[voice-state] shutdown cleanup incomplete; catalog storage "
                "remains resident until process teardown");
        }
    }
    g_state.store(ModuleState::Stopped, std::memory_order_release);
    LogState(ModuleState::Stopped, "module shutdown");
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Voice Language", "2.3.1", BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown};

} // namespace
} // namespace BetterEndfield::Voice

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::Voice::kApi;
}
