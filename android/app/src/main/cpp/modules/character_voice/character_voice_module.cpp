#include "character_voice_module.h"

#include "core/log.h"
#include "core/runtime.h"

#include <algorithm>
#include <chrono>
#include <cctype>
#include <cstdlib>
#include <cstring>
#include <filesystem>
#include <fstream>
#include <string_view>
#include <unordered_map>
#include <unordered_set>
#include <utility>

namespace betterendfield {
namespace {

constexpr char kCatalogRoot[] = "BETTER_ENDFIELD_VOICE_CATALOG_ROOT";
constexpr char kVoiceRules[] = "BETTER_ENDFIELD_VOICE_RULES";
constexpr int kAkSuccess = 1;
thread_local bool g_loading_auxiliary_package = false;
thread_local int g_duration_language_override = -1;
thread_local int g_lip_language_override = -1;

struct PendingLipRoute {
    bool armed = false;
    int language = -1;
    std::string line_id;
};

thread_local PendingLipRoute g_pending_lip_route;

struct VoiceRequestRoute {
    bool active = false;
    int language = -1;
    std::string speaker;
};

thread_local VoiceRequestRoute g_voice_request_route;

class ScopedVoiceRequestRoute final {
public:
    ScopedVoiceRequestRoute(bool active, int language, std::string speaker) {
        if (!active || language < 0 || language > 3) {
            return;
        }
        previous_ = g_voice_request_route;
        g_voice_request_route.active = true;
        g_voice_request_route.language = language;
        g_voice_request_route.speaker = std::move(speaker);
        applied_ = true;
    }

    ~ScopedVoiceRequestRoute() {
        if (applied_) {
            g_voice_request_route = std::move(previous_);
        }
    }

private:
    bool applied_ = false;
    VoiceRequestRoute previous_;
};

#pragma pack(push, 1)
struct CatalogHeader {
    char magic[8];
    uint16_t version;
    uint16_t language;
    uint32_t entry_count;
    uint32_t reserved;
    uint64_t entry_offset;
    uint64_t data_offset;
};

struct CatalogEntry {
    uint32_t source_media_id;
    uint32_t target_media_id;
    uint64_t data_offset;
    uint32_t data_size;
    uint32_t reserved;
};
#pragma pack(pop)

static_assert(sizeof(CatalogHeader) == 36);
static_assert(sizeof(CatalogEntry) == 24);

int ParseLanguage(std::string_view language) {
    if (language == "Chinese") return 0;
    if (language == "English") return 1;
    if (language == "Japanese") return 2;
    if (language == "Korean") return 3;
    return -1;
}

const char* LanguagePathName(int language) {
    static constexpr const char* names[]{
        "chinese", "english", "japanese", "korean"
    };
    return language >= 0 && language < 4 ? names[language] : nullptr;
}

std::vector<std::pair<std::string, int>> ParseRules(const char* raw) {
    std::vector<std::pair<std::string, int>> result;
    if (raw == nullptr) {
        return result;
    }
    std::string_view remaining(raw);
    while (!remaining.empty()) {
        const size_t separator = remaining.find(';');
        std::string_view item = remaining.substr(0, separator);
        const size_t colon = item.find(':');
        if (colon > 0 && colon + 1 < item.size()) {
            const int language = ParseLanguage(item.substr(colon + 1));
            if (language >= 0) {
                result.emplace_back(std::string(item.substr(0, colon)), language);
            }
        }
        if (separator == std::string_view::npos) {
            break;
        }
        remaining.remove_prefix(separator + 1);
    }
    return result;
}

std::string CatalogFileName(std::string_view speaker, int language) {
    const char* name = LanguagePathName(language);
    if (name == nullptr || speaker.empty()) {
        return {};
    }
    if (speaker == "*") {
        return "voice." + std::string(name) + ".becat";
    }
    return "voice." + std::string(name) + "." + std::string(speaker) + ".becat";
}

bool ReadFile(const std::filesystem::path& path, std::vector<std::byte>& bytes) {
    std::ifstream stream(path, std::ios::binary | std::ios::ate);
    if (!stream) {
        return false;
    }
    const std::streamsize size = stream.tellg();
    if (size < static_cast<std::streamsize>(sizeof(CatalogHeader))) {
        return false;
    }
    bytes.resize(static_cast<size_t>(size));
    stream.seekg(0, std::ios::beg);
    return stream.read(
        reinterpret_cast<char*>(bytes.data()), size).good();
}

bool ShouldLog(uint64_t hit) {
    return hit <= 10 || (hit & (hit - 1)) == 0;
}

std::string Normalize(std::string value) {
    std::transform(value.begin(), value.end(), value.begin(), [](unsigned char item) {
        return static_cast<char>(std::tolower(item));
    });
    std::replace(value.begin(), value.end(), '\\', '/');
    return value;
}

bool BuildReplacementSource(
    const std::string& source, int language, std::string& replacement) {
    replacement.clear();
    const char* language_name = LanguagePathName(language);
    if (language_name == nullptr || !source.starts_with("voice/")) {
        return false;
    }
    constexpr size_t language_start = sizeof("voice/") - 1;
    const size_t language_end = source.find('/', language_start);
    if (language_end == std::string::npos || language_end + 1 >= source.size()) {
        return false;
    }
    const std::string_view suffix(source.data() + language_end + 1,
        source.size() - language_end - 1);
    if (suffix.find("characters/") == std::string_view::npos &&
        suffix.find("narrating/") == std::string_view::npos) {
        return false;
    }
    replacement = "voice/";
    replacement += language_name;
    replacement.push_back('/');
    replacement.append(suffix);
    return replacement != source;
}

uint64_t MonotonicMilliseconds() {
    return static_cast<uint64_t>(std::chrono::duration_cast<std::chrono::milliseconds>(
        std::chrono::steady_clock::now().time_since_epoch()).count());
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

}  // namespace

std::atomic<CharacterVoiceModule*> CharacterVoiceModule::active_{nullptr};

const char* CharacterVoiceModule::Id() const {
    return "voice.character";
}

ModuleResult CharacterVoiceModule::Start(Il2CppRuntime& runtime) {
    runtime_ = &runtime;
    const char* root = std::getenv(kCatalogRoot);
    const std::vector<std::pair<std::string, int>> configured_rules = ParseRules(
        std::getenv(kVoiceRules));
    if (configured_rules.empty()) {
        return {false, "disabled; no per-character voice rules were selected"};
    }

    const ResolvedMethod set_media = runtime.ResolveMethod(
        "AK.Wwise.Unity.API.dll",
        "",
        "AkSoundEnginePINVOKE",
        "CSharp_SetMedia",
        2);
    const ResolvedMethod unset_media = runtime.ResolveMethod(
        "AK.Wwise.Unity.API.dll",
        "",
        "AkSoundEnginePINVOKE",
        "CSharp_UnsetMedia",
        2);
    const ResolvedMethod play_voice = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Audio",
        "VoicePlayer",
        "PlayVoice",
        1);
    const ResolvedMethod internal_play_voice = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Audio",
        "VoicePlayer",
        "_PlayVoice",
        1);
    const ResolvedMethod channel_play_voice = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Audio",
        "VoiceSpeakChannelProcessor",
        "_PlayVoice",
        1);
    const ResolvedMethod play_event = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Audio",
        "VoicePlayer",
        "_PlayEvent",
        3);
    const ResolvedMethod narrative = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Audio",
        "VoiceManager",
        "_SpeakNarrative",
        3);
    const ResolvedMethod external_event = runtime.ResolveMethod(
        "Audio.Beyond.dll",
        "Beyond.Audio",
        "AudioAdapter",
        "PostEventExternal",
        8);
    const ResolvedMethod try_load_language_package = runtime.ResolveMethod(
        "Audio.Beyond.dll",
        "Beyond.Audio",
        "AudioVFSLoader",
        "TryLoadLanguagePck",
        1);
    const ResolvedMethod get_language_name = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Audio",
        "VoiceI18n",
        "GetLanguageName",
        1);
    const ResolvedMethod get_current_language = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Audio",
        "VoiceI18n",
        "GetCurrentLanguage",
        0);
    const ResolvedMethod duration = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Audio",
        "VoiceUtils",
        "TryGetVoiceDuration",
        2);
    const ResolvedMethod lip_dialog = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Core",
        "DialogManager",
        "_PlayLipSyncTrack",
        2);
    const ResolvedMethod lip_path = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.View.LipSync",
        "LipSyncUtils",
        "GetLipSyncTrackPath",
        3);
    const ResolvedMethod lip_load = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.View.LipSync",
        "LipSyncUtils",
        "TryLoadTrack",
        2);
    const ResolvedMethod lip_real_actor = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay",
        "DialogPlayTrunkActionData",
        "get_realActorNameId",
        0);
    const ResolvedMethod lip_actor = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay",
        "DialogPlayTrunkActionData",
        "get_actorNameId",
        0);
    const ResolvedMethod lip_trunk = runtime.ResolveMethod(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay",
        "DialogPlayTrunkActionData",
        "get_trunkId",
        0);
    const ResolvedMethod load_file_package = runtime.ResolveMethod(
        "AK.Wwise.Unity.API.dll",
        "",
        "AkSoundEngine",
        "LoadFilePackage",
        2);
    const ResolvedMethod unload_file_package = runtime.ResolveMethod(
        "AK.Wwise.Unity.API.dll",
        "",
        "AkSoundEngine",
        "UnloadFilePackage",
        1);
    voice_context_voice_data_ = runtime.ResolveField(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Audio",
        "VoiceContext",
        "voiceData");
    if (voice_context_voice_data_.info == nullptr) {
        voice_context_voice_data_ = runtime.ResolveField(
            "Gameplay.Beyond.dll",
            "Beyond.Gameplay.Audio",
            "VoiceContext",
            "VoiceData");
    }
    runtime_voice_data_speaker_channel_ = runtime.ResolveField(
        "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Audio",
        "RuntimeVoiceData",
        "speakerChannel");
    if (set_media.entry == nullptr || unset_media.entry == nullptr ||
        play_voice.entry == nullptr || external_event.entry == nullptr ||
        try_load_language_package.entry == nullptr ||
        get_language_name.entry == nullptr ||
        get_current_language.entry == nullptr ||
        load_file_package.entry == nullptr || unload_file_package.entry == nullptr) {
        return {
            false,
            "contract mismatch: a required voice/PCK function was not resolved",
        };
    }

    get_current_language_ = get_current_language.entry;
    get_language_name_ = get_language_name.entry;
    try_load_language_package_ = try_load_language_package.entry;
    lip_real_actor_getter_ = lip_real_actor.info;
    lip_actor_getter_ = lip_actor.info;
    lip_trunk_getter_ = lip_trunk.info;
    const bool duration_contract_ready = duration.entry != nullptr;
    const bool lip_contract_ready = lip_dialog.entry != nullptr &&
        lip_path.entry != nullptr && lip_load.entry != nullptr &&
        lip_real_actor.info != nullptr && lip_actor.info != nullptr &&
        lip_trunk.info != nullptr;
    const bool context_contract_ready = internal_play_voice.entry != nullptr &&
        channel_play_voice.entry != nullptr && play_event.entry != nullptr &&
        narrative.entry != nullptr;

    catalogs_.clear();
    routes_.clear();
    rules_.clear();
    rules_.reserve(configured_rules.size());
    for (const auto& [speaker, language] : configured_rules) {
        rules_.push_back({Normalize(speaker), language});
    }
    catalogs_.reserve(rules_.size());
    struct SelectedTarget {
        uint32_t target_media_id = 0;
        size_t route_index = 0;
        bool wildcard = false;
    };
    std::unordered_map<uint32_t, SelectedTarget> selected_targets;

    for (const VoiceRule& rule : rules_) {
        const bool wildcard = rule.speaker == "*";
        const std::string file_name = CatalogFileName(rule.speaker, rule.language);
        if (file_name.empty() || root == nullptr || *root == '\0') {
            continue;
        }

        Catalog catalog;
        catalog.file_name = file_name;
        if (!ReadFile(std::filesystem::path(root) / file_name, catalog.blob)) {
            continue;
        }

        const auto* header = reinterpret_cast<const CatalogHeader*>(
            catalog.blob.data());
        const uint64_t size = catalog.blob.size();
        const bool entries_in_bounds = header->entry_offset <= size &&
            header->entry_count <=
                (size - header->entry_offset) / sizeof(CatalogEntry);
        if (std::memcmp(header->magic, "BEVCAT01", 8) != 0 ||
            header->version != 1 || header->language != rule.language ||
            !entries_in_bounds || header->data_offset > size) {
            return {false, "catalog header validation failed: " + catalog.file_name};
        }

        catalogs_.push_back(std::move(catalog));
        const Catalog& resident = catalogs_.back();
        header = reinterpret_cast<const CatalogHeader*>(resident.blob.data());
        const auto* entries = reinterpret_cast<const CatalogEntry*>(
            resident.blob.data() + header->entry_offset);
        for (uint32_t index = 0; index < header->entry_count; ++index) {
            const CatalogEntry& entry = entries[index];
            if (entry.source_media_id == 0 || entry.target_media_id == 0 ||
                entry.data_size == 0 || entry.data_offset < header->data_offset ||
                entry.data_offset > size || entry.data_size > size - entry.data_offset) {
                return {false, "catalog media bounds validation failed: " + resident.file_name};
            }

            const auto [found, inserted] = selected_targets.emplace(
                entry.source_media_id,
                SelectedTarget{entry.target_media_id, routes_.size(), wildcard});
            if (!inserted) {
                if (found->second.wildcard && !wildcard) {
                    routes_[found->second.route_index] = {
                        entry.source_media_id,
                        0,
                        resident.blob.data() + entry.data_offset,
                        entry.data_size,
                        0,
                    };
                    found->second.target_media_id = entry.target_media_id;
                    found->second.wildcard = false;
                    continue;
                }
                if (!found->second.wildcard && wildcard) {
                    continue;
                }
                if (found->second.target_media_id != entry.target_media_id) {
                    return {false, "conflicting character Media routes"};
                }
                continue;
            }
            routes_.push_back({
                entry.source_media_id,
                0,
                resident.blob.data() + entry.data_offset,
                entry.data_size,
                0,
            });
        }
    }

    std::string hook_error;
    if (!hook_broker_.Initialize(hook_error)) {
        return {false, std::move(hook_error)};
    }

    routes_applied_ = false;
    language_package_ready_.fill(false);
    language_package_retry_at_ms_.fill(0);
    active_.store(this, std::memory_order_release);
    if (!hook_broker_.Install(
            load_file_package.entry,
            reinterpret_cast<void*>(&HookLoadFilePackage),
            &load_file_package_original_,
            load_file_package_stub_,
            hook_error) ||
        !hook_broker_.Install(
            unload_file_package.entry,
            reinterpret_cast<void*>(&HookUnloadFilePackage),
            &unload_file_package_original_,
            unload_file_package_stub_,
            hook_error) ||
        !hook_broker_.Install(
            external_event.entry,
            reinterpret_cast<void*>(&HookExternalEvent),
            &external_event_original_,
            external_event_stub_,
            hook_error) ||
        !hook_broker_.Install(
            get_current_language.entry,
            reinterpret_cast<void*>(&HookGetCurrentLanguage),
            &get_current_language_original_,
            get_current_language_stub_,
            hook_error) ||
        !hook_broker_.Install(
            set_media.entry,
            reinterpret_cast<void*>(&HookSetMedia),
            &set_media_original_,
            set_media_stub_,
            hook_error) ||
        !hook_broker_.Install(
            unset_media.entry,
            reinterpret_cast<void*>(&HookUnsetMedia),
            &unset_media_original_,
            unset_media_stub_,
            hook_error) ||
        !hook_broker_.Install(
            play_voice.entry,
            reinterpret_cast<void*>(&HookPlayVoice),
            &play_voice_original_,
            play_voice_stub_,
            hook_error)) {
        RemoveHooks();
        return {false, std::move(hook_error)};
    }

    if (duration_contract_ready && !hook_broker_.Install(
            duration.entry,
            reinterpret_cast<void*>(&HookDuration),
            &duration_original_,
            duration_stub_,
            hook_error)) {
        RemoveHooks();
        return {false, std::move(hook_error)};
    }
    if (lip_contract_ready &&
        (!hook_broker_.Install(
             lip_dialog.entry,
             reinterpret_cast<void*>(&HookLipDialog),
             &lip_dialog_original_,
             lip_dialog_stub_,
             hook_error) ||
         !hook_broker_.Install(
             lip_path.entry,
             reinterpret_cast<void*>(&HookLipPath),
             &lip_path_original_,
             lip_path_stub_,
             hook_error) ||
         !hook_broker_.Install(
             lip_load.entry,
             reinterpret_cast<void*>(&HookLipLoad),
             &lip_load_original_,
             lip_load_stub_,
             hook_error))) {
        RemoveHooks();
        return {false, std::move(hook_error)};
    }
    if (context_contract_ready &&
        (!hook_broker_.Install(
             internal_play_voice.entry,
             reinterpret_cast<void*>(&HookInternalPlayVoice),
             &internal_play_voice_original_,
             internal_play_voice_stub_,
             hook_error) ||
         !hook_broker_.Install(
             channel_play_voice.entry,
             reinterpret_cast<void*>(&HookChannelPlayVoice),
             &channel_play_voice_original_,
             channel_play_voice_stub_,
             hook_error) ||
         !hook_broker_.Install(
             play_event.entry,
             reinterpret_cast<void*>(&HookPlayEvent),
             &play_event_original_,
             play_event_stub_,
             hook_error) ||
         !hook_broker_.Install(
             narrative.entry,
             reinterpret_cast<void*>(&HookNarrative),
             &narrative_original_,
             narrative_stub_,
             hook_error))) {
        RemoveHooks();
        return {false, std::move(hook_error)};
    }

    return {
        true,
        "armed; function-name PlayVoice/ExternalEvent/PCK/Media hooks active; character catalogs=" +
            std::to_string(catalogs_.size()) + " Media routes=" +
            std::to_string(routes_.size()) + " duration=" +
            (duration_contract_ready ? "active" : "unavailable") + " lip=" +
            (lip_contract_ready ? "active" : "unavailable") + " context=" +
            (context_contract_ready ? "active" : "unavailable"),
    };
}

bool CharacterVoiceModule::ApplyRoutes() {
    std::lock_guard lock(route_mutex_);
    if (routes_applied_) {
        return true;
    }
    const auto original = reinterpret_cast<MediaFn>(set_media_original_);
    if (original == nullptr) {
        return false;
    }

    size_t applied = 0;
    for (Route& route : routes_) {
        if (original(&route, 1, nullptr) != kAkSuccess) {
            LogError(
                Id(),
                ("deferred Media activation rejected after routes=" +
                    std::to_string(applied)).c_str());
            return false;
        }
        ++applied;
    }
    routes_applied_ = true;
    LogInfo(
        Id(),
        ("deferred Media routes activated at PlayVoice; routes=" +
            std::to_string(applied)).c_str());
    return true;
}

int CharacterVoiceModule::SelectLanguage(
    std::string_view value,
    bool allow_token_match,
    std::string* speaker) const {
    const VoiceRule* wildcard = nullptr;
    for (const VoiceRule& rule : rules_) {
        if (rule.speaker == "*") {
            wildcard = &rule;
            continue;
        }
        const size_t second_separator = rule.speaker.find('_', 4);
        const std::string_view codename = second_separator == std::string::npos
            ? std::string_view(rule.speaker)
            : std::string_view(rule.speaker).substr(second_separator + 1);
        const bool exact = value == rule.speaker || value == codename;
        const bool token = allow_token_match && !value.empty() &&
            (value.find(rule.speaker) != std::string_view::npos ||
                (!codename.empty() && value.find(codename) != std::string_view::npos));
        if (exact || token) {
            if (speaker != nullptr) *speaker = rule.speaker;
            return rule.language;
        }
    }
    if (wildcard != nullptr) {
        if (speaker != nullptr) *speaker = wildcard->speaker;
        return wildcard->language;
    }
    return -1;
}

std::string CharacterVoiceModule::ReadManagedGetter(
    const MethodInfo* getter, void* instance) const {
    if (runtime_ == nullptr || getter == nullptr || instance == nullptr) {
        return {};
    }
    void* exception = nullptr;
    void* value = runtime_->Invoke(getter, instance, nullptr, &exception);
    return exception == nullptr ? Normalize(runtime_->CopyString(value)) : std::string{};
}

std::string CharacterVoiceModule::SpeakerFromContext(void* voice_context) const {
    if (runtime_ == nullptr || voice_context == nullptr) {
        return {};
    }
    void* voice_data = runtime_->ReadFieldObject(
        voice_context_voice_data_, voice_context);
    void* speaker = runtime_->ReadFieldObject(
        runtime_voice_data_speaker_channel_, voice_data);
    return Normalize(runtime_->CopyString(speaker));
}

bool CharacterVoiceModule::EnsureLanguagePackage(int language_index) {
    using GetCurrentLanguageFn = int (*)(const void* method_info);
    using GetLanguageNameFn = void* (*)(int language, const void* method_info);
    using TryLoadLanguagePackageFn = bool (*)(
        void* language, const void* method_info);

    std::lock_guard lock(package_mutex_);
    if (language_index < 0 || language_index >= 4) {
        return false;
    }
    if (language_package_ready_[language_index]) {
        return true;
    }
    if (get_current_language_ == nullptr || get_language_name_ == nullptr ||
        try_load_language_package_ == nullptr ||
        load_file_package_original_ == nullptr ||
        unload_file_package_original_ == nullptr || runtime_ == nullptr) {
        return false;
    }

    const void* current_entry = get_current_language_original_ != nullptr
        ? get_current_language_original_ : get_current_language_;
    const int current = reinterpret_cast<GetCurrentLanguageFn>(
        const_cast<void*>(current_entry))(nullptr);
    if (current == language_index) {
        language_package_ready_[language_index] = true;
        LogInfo(Id(), "target PCK already active as the global voice package");
        return true;
    }

    const uint64_t now = MonotonicMilliseconds();
    if (now < language_package_retry_at_ms_[language_index]) {
        return false;
    }
    void* language = reinterpret_cast<GetLanguageNameFn>(
        get_language_name_)(language_index, nullptr);
    bool loaded = false;
    if (language != nullptr) {
        g_loading_auxiliary_package = true;
        loaded = reinterpret_cast<TryLoadLanguagePackageFn>(
            try_load_language_package_)(language, nullptr);
        g_loading_auxiliary_package = false;
    }
    language_package_ready_[language_index] = loaded;
    language_package_retry_at_ms_[language_index] = loaded ? 0 : now + 5000;
    LogInfo(
        Id(),
        ("target PCK mount result=" + std::string(loaded ? "success" : "failed") +
            " language=" + runtime_->CopyString(language) +
            " index=" + std::to_string(language_index) +
            " global=" + std::to_string(current)).c_str());
    return loaded;
}

void CharacterVoiceModule::ReassertTouched(
    void* settings, uint32_t count, const char* source) {
    if (settings == nullptr || count == 0 || count > 65536) {
        return;
    }
    const auto* game_routes = static_cast<const Route*>(settings);
    std::unordered_set<uint32_t> touched;
    touched.reserve(count);
    for (uint32_t index = 0; index < count; ++index) {
        touched.insert(game_routes[index].source_id);
    }

    std::lock_guard lock(route_mutex_);
    if (!routes_applied_) {
        return;
    }
    const auto original = reinterpret_cast<MediaFn>(set_media_original_);
    if (original == nullptr) {
        return;
    }

    size_t restored = 0;
    size_t failed = 0;
    for (Route& route : routes_) {
        if (!touched.contains(route.source_id)) {
            continue;
        }
        if (original(&route, 1, nullptr) == kAkSuccess) {
            ++restored;
        } else {
            ++failed;
        }
    }
    if (restored != 0 || failed != 0) {
        LogInfo(
            Id(),
            (std::string("game ") + source + " touched character routes; restored=" +
                std::to_string(restored) + " failed=" +
                std::to_string(failed)).c_str());
    }
}

void CharacterVoiceModule::RemoveHooks() {
    active_.store(nullptr, std::memory_order_release);
    hook_broker_.Remove(narrative_stub_);
    hook_broker_.Remove(play_event_stub_);
    hook_broker_.Remove(channel_play_voice_stub_);
    hook_broker_.Remove(internal_play_voice_stub_);
    hook_broker_.Remove(lip_load_stub_);
    hook_broker_.Remove(lip_path_stub_);
    hook_broker_.Remove(lip_dialog_stub_);
    hook_broker_.Remove(duration_stub_);
    hook_broker_.Remove(play_voice_stub_);
    hook_broker_.Remove(unset_media_stub_);
    hook_broker_.Remove(set_media_stub_);
    hook_broker_.Remove(get_current_language_stub_);
    hook_broker_.Remove(external_event_stub_);
    hook_broker_.Remove(unload_file_package_stub_);
    hook_broker_.Remove(load_file_package_stub_);
}

void CharacterVoiceModule::HookPlayVoice(
    void* voice_context, const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return;
    }
    const uint64_t hit = module->play_voice_hits_.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const std::string speaker = module->SpeakerFromContext(voice_context);
    const int target_language = module->SelectLanguage(speaker, false);
    const bool selected = target_language >= 0;
    const bool ready = module->ApplyRoutes();
    if (ShouldLog(hit)) {
        LogInfo(
            module->Id(),
            ("PlayVoice hit=" + std::to_string(hit) +
                " speaker=" + (speaker.empty() ? "<empty>" : speaker) +
                " selected=" + (selected ? "true" : "false") +
                " catalogReady=" + (ready ? "true" : "false")).c_str());
    }
    const ScopedVoiceRequestRoute route(
        selected && ready, target_language, speaker);
    const auto original = reinterpret_cast<PlayVoiceFn>(
        module->play_voice_original_);
    if (original != nullptr) {
        original(voice_context, method_info);
    }
}

uint32_t CharacterVoiceModule::HookInternalPlayVoice(
    void* voice_context_reference, const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return 0;
    }
    const uint64_t hit = module->internal_play_voice_hits_.fetch_add(
        1, std::memory_order_relaxed) + 1;
    void* voice_context = voice_context_reference == nullptr
        ? nullptr : *static_cast<void**>(voice_context_reference);
    const std::string speaker = module->SpeakerFromContext(voice_context);
    const int target_language = module->SelectLanguage(speaker, false);
    const bool selected = target_language >= 0;
    const bool ready = module->ApplyRoutes();
    if (ShouldLog(hit)) {
        LogInfo(
            module->Id(),
            ("VoicePlayer._PlayVoice hit=" + std::to_string(hit) +
                " speaker=" + (speaker.empty() ? "<empty>" : speaker) +
                " selected=" + (selected ? "true" : "false") +
                " catalogReady=" + (ready ? "true" : "false")).c_str());
    }
    const ScopedVoiceRequestRoute route(
        selected && ready, target_language, speaker);
    const auto original = reinterpret_cast<InternalPlayVoiceFn>(
        module->internal_play_voice_original_);
    return original == nullptr
        ? 0 : original(voice_context_reference, method_info);
}

void CharacterVoiceModule::HookChannelPlayVoice(
    void* instance, void* voice_context, const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return;
    }
    const uint64_t hit = module->channel_play_voice_hits_.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const std::string speaker = module->SpeakerFromContext(voice_context);
    const int target_language = module->SelectLanguage(speaker, false);
    const bool selected = target_language >= 0;
    const bool ready = module->ApplyRoutes();
    if (ShouldLog(hit)) {
        LogInfo(
            module->Id(),
            ("VoiceSpeakChannelProcessor._PlayVoice hit=" +
                std::to_string(hit) + " speaker=" +
                (speaker.empty() ? "<empty>" : speaker) + " selected=" +
                (selected ? "true" : "false") + " catalogReady=" +
                (ready ? "true" : "false")).c_str());
    }
    const ScopedVoiceRequestRoute route(
        selected && ready, target_language, speaker);
    const auto original = reinterpret_cast<ChannelPlayVoiceFn>(
        module->channel_play_voice_original_);
    if (original != nullptr) {
        original(instance, voice_context, method_info);
    }
}

uint32_t CharacterVoiceModule::HookPlayEvent(
    void* event_name,
    uint64_t audio_object_id,
    uint32_t handle_id,
    const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return 0;
    }
    const uint64_t hit = module->play_event_hits_.fetch_add(
        1, std::memory_order_relaxed) + 1;
    if (ShouldLog(hit)) {
        LogInfo(
            module->Id(),
            ("VoicePlayer._PlayEvent hit=" + std::to_string(hit) +
                " requestScope=" +
                (g_voice_request_route.active ? "active" : "none") +
                " event=" + module->runtime_->CopyString(event_name) +
                " audioObject=" + std::to_string(audio_object_id) +
                " handle=" + std::to_string(handle_id)).c_str());
    }
    const auto original = reinterpret_cast<PlayEventFn>(module->play_event_original_);
    return original == nullptr
        ? 0 : original(event_name, audio_object_id, handle_id, method_info);
}

uint32_t CharacterVoiceModule::HookNarrative(
    void* instance,
    void* voice_id,
    uint64_t audio_object_id,
    void* config,
    const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return 0;
    }
    const uint64_t hit = module->narrative_hits_.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const std::string identity = Normalize(module->runtime_->CopyString(voice_id));
    const int target_language = module->SelectLanguage(identity, true);
    const bool selected = target_language >= 0;
    const bool ready = module->ApplyRoutes();
    if (ShouldLog(hit) || selected) {
        LogInfo(
            module->Id(),
            ("VoiceManager._SpeakNarrative hit=" + std::to_string(hit) +
                " identity=" + (identity.empty() ? "<empty>" : identity) +
                " selected=" + (selected ? "true" : "false") +
                " catalogReady=" + (ready ? "true" : "false")).c_str());
    }
    const ScopedVoiceRequestRoute route(
        selected && ready, target_language, identity);
    const auto original = reinterpret_cast<NarrativeFn>(module->narrative_original_);
    return original == nullptr
        ? 0 : original(instance, voice_id, audio_object_id, config, method_info);
}

int CharacterVoiceModule::HookSetMedia(
    void* settings, uint32_t count, const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return 0;
    }
    const uint64_t hit = module->set_media_hits_.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const auto original = reinterpret_cast<MediaFn>(module->set_media_original_);
    const int result = original != nullptr
        ? original(settings, count, method_info) : 0;
    if (result == kAkSuccess) {
        module->ReassertTouched(settings, count, "SetMedia");
    }
    if (ShouldLog(hit)) {
        LogInfo(
            module->Id(),
            ("CSharp_SetMedia hit=" + std::to_string(hit) + " count=" +
                std::to_string(count) + " result=" +
                std::to_string(result)).c_str());
    }
    return result;
}

int CharacterVoiceModule::HookUnsetMedia(
    void* settings, uint32_t count, const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return 0;
    }
    const uint64_t hit = module->unset_media_hits_.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const auto original = reinterpret_cast<MediaFn>(module->unset_media_original_);
    const int result = original != nullptr
        ? original(settings, count, method_info) : 0;
    if (result == kAkSuccess) {
        module->ReassertTouched(settings, count, "UnsetMedia");
    }
    if (ShouldLog(hit)) {
        LogInfo(
            module->Id(),
            ("CSharp_UnsetMedia hit=" + std::to_string(hit) + " count=" +
                std::to_string(count) + " result=" +
                std::to_string(result)).c_str());
    }
    return result;
}

bool CharacterVoiceModule::HookDuration(
    void* voice_id, float* duration, const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return false;
    }
    const uint64_t hit = module->duration_hits_.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const std::string identity = Normalize(module->runtime_->CopyString(voice_id));
    const int target_language = module->SelectLanguage(identity, true);
    const bool selected = target_language >= 0;
    const auto original = reinterpret_cast<DurationFn>(module->duration_original_);
    const auto get_language = reinterpret_cast<GetCurrentLanguageFn>(
        module->get_current_language_original_);
    const int current = get_language == nullptr ? -1 : get_language(nullptr);

    bool result = original != nullptr
        ? original(voice_id, duration, method_info) : false;
    const float global_duration = duration == nullptr ? -1.0f : *duration;
    float routed_duration = global_duration;
    bool override_applied = false;
    if (selected && current != target_language && original != nullptr) {
        const int previous = g_duration_language_override;
        g_duration_language_override = target_language;
        const bool routed_result = original(voice_id, duration, method_info);
        g_duration_language_override = previous;
        routed_duration = duration == nullptr ? -1.0f : *duration;
        if (routed_result && routed_duration > 0.0f) {
            result = true;
            override_applied = true;
        } else {
            if (duration != nullptr) {
                *duration = global_duration;
            }
            routed_duration = global_duration;
        }
    }
    if (selected && ShouldLog(hit)) {
        LogInfo(
            module->Id(),
            ("voice duration identity=" +
                (identity.empty() ? std::string("<empty>") : identity) +
                " global=" + std::to_string(global_duration) +
                " target=" + std::to_string(target_language) +
                " routed=" + std::to_string(routed_duration) +
                " override=" + (override_applied ? "true" : "false")).c_str());
    }
    return result;
}

int CharacterVoiceModule::HookGetCurrentLanguage(const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return 0;
    }
    const int override = g_duration_language_override >= 0
        ? g_duration_language_override : g_lip_language_override;
    if (override >= 0 && override <= 3) {
        const uint64_t hit = module->language_override_hits_.fetch_add(
            1, std::memory_order_relaxed) + 1;
        if (ShouldLog(hit)) {
            LogInfo(
                module->Id(),
                ("thread-local voice language override=" +
                    std::to_string(override)).c_str());
        }
        return override;
    }
    const auto original = reinterpret_cast<GetCurrentLanguageFn>(
        module->get_current_language_original_);
    return original == nullptr ? 0 : original(method_info);
}

void CharacterVoiceModule::HookLipDialog(
    void* instance,
    void* action_data,
    void* entity,
    const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return;
    }
    const uint64_t hit = module->lip_dialog_hits_.fetch_add(
        1, std::memory_order_relaxed) + 1;
    std::string actor = module->ReadManagedGetter(
        module->lip_real_actor_getter_, action_data);
    if (actor.empty()) {
        actor = module->ReadManagedGetter(module->lip_actor_getter_, action_data);
    }
    const std::string trunk = module->ReadManagedGetter(
        module->lip_trunk_getter_, action_data);
    const int target_language = module->SelectLanguage(actor, false);
    const bool selected = target_language >= 0;
    const int previous = g_lip_language_override;
    if (selected) {
        g_lip_language_override = target_language;
    }
    if (ShouldLog(hit) || selected) {
        LogInfo(
            module->Id(),
            ("lip dialog hit=" + std::to_string(hit) + " actor=" +
                (actor.empty() ? "<empty>" : actor) + " trunk=" +
                (trunk.empty() ? "<empty>" : trunk) + " target=" +
                (selected ? std::to_string(target_language) : "global")).c_str());
    }
    const auto original = reinterpret_cast<LipDialogFn>(module->lip_dialog_original_);
    if (original != nullptr) {
        original(instance, action_data, entity, method_info);
    }
    g_lip_language_override = previous;
}

void* CharacterVoiceModule::HookLipPath(
    int language,
    void* voice_id,
    void* suffix,
    const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return nullptr;
    }
    const int routed_language = g_lip_language_override >= 0
        ? g_lip_language_override : language;
    const uint64_t hit = module->lip_path_hits_.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const auto original = reinterpret_cast<LipPathFn>(module->lip_path_original_);
    void* result = original == nullptr
        ? nullptr : original(routed_language, voice_id, suffix, method_info);
    if (ShouldLog(hit) || routed_language != language) {
        LogInfo(
            module->Id(),
            ("lip path hit=" + std::to_string(hit) + " language=" +
                std::to_string(language) + "->" + std::to_string(routed_language) +
                " voice=" + module->runtime_->CopyString(voice_id) +
                " result=" + module->runtime_->CopyString(result)).c_str());
    }
    return result;
}

bool CharacterVoiceModule::HookLipLoad(
    void* line_id, void** track, const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return false;
    }
    const uint64_t hit = module->lip_load_hits_.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const std::string line = ExtractVoiceLineId(
        Normalize(module->runtime_->CopyString(line_id)));
    const bool pending_matches = g_pending_lip_route.armed &&
        !line.empty() && line == g_pending_lip_route.line_id;
    const int previous = g_lip_language_override;
    const bool dialog_routed = previous >= 0 && previous <= 3;
    const bool routed = pending_matches || dialog_routed;
    const int target_language = pending_matches
        ? g_pending_lip_route.language : previous;
    if (routed) {
        g_lip_language_override = target_language;
    }
    const auto original = reinterpret_cast<LipLoadFn>(module->lip_load_original_);
    bool result = original != nullptr
        ? original(line_id, track, method_info) : false;
    bool fallback = false;
    if (routed && !result && original != nullptr) {
        g_lip_language_override = -1;
        result = original(line_id, track, method_info);
        fallback = result;
    }
    g_lip_language_override = previous;
    if (pending_matches) {
        g_pending_lip_route = {};
    }
    if (ShouldLog(hit) || routed) {
        LogInfo(
            module->Id(),
            ("lip track load hit=" + std::to_string(hit) + " line=" +
                (line.empty() ? "<empty>" : line) + " routed=" +
                (routed ? "true" : "false") + " target=" +
                (routed ? std::to_string(target_language) : "global") +
                " result=" +
                (result ? "true" : "false") + " fallback=" +
                (fallback ? "true" : "false")).c_str());
    }
    return result;
}

uint32_t CharacterVoiceModule::HookExternalEvent(
    void* event_name,
    uint64_t audio_object_id,
    void* external_source_key,
    uint32_t external_cookie,
    uint32_t callback_type,
    void* callback,
    void* cookie,
    uint32_t codec,
    const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return 0;
    }
    const uint64_t hit = module->external_event_hits_.fetch_add(
        1, std::memory_order_relaxed) + 1;
    const std::string event = Normalize(module->runtime_->CopyString(event_name));
    const std::string source = Normalize(
        module->runtime_->CopyString(external_source_key));
    int target_language = g_voice_request_route.active
        ? g_voice_request_route.language
        : module->SelectLanguage(source, true);
    if (target_language < 0) {
        target_language = module->SelectLanguage(event, true);
    }
    const bool selected = target_language >= 0;

    std::string replacement;
    void* routed_source = external_source_key;
    bool replaced = false;
    if (selected && BuildReplacementSource(source, target_language, replacement) &&
        module->EnsureLanguagePackage(target_language)) {
        routed_source = module->runtime_->NewString(replacement.c_str());
        replaced = routed_source != nullptr;
    }
    const bool narrative = source.starts_with("voice/") &&
        source.find("/narrating/") != std::string::npos;
    bool lip_armed = false;
    if (narrative) {
        if (selected && replaced) {
            g_pending_lip_route.armed = true;
            g_pending_lip_route.language = target_language;
            g_pending_lip_route.line_id = ExtractVoiceLineId(source);
            lip_armed = !g_pending_lip_route.line_id.empty();
        } else {
            g_pending_lip_route = {};
        }
    }

    const auto original = reinterpret_cast<ExternalEventFn>(
        module->external_event_original_);
    const uint32_t result = original == nullptr ? 0 : original(
        event_name,
        audio_object_id,
        routed_source,
        external_cookie,
        callback_type,
        callback,
        cookie,
        codec,
        method_info);
    if (ShouldLog(hit) || selected) {
        LogInfo(
            module->Id(),
            ("PostEventExternal hit=" + std::to_string(hit) +
                " selected=" + (selected ? "true" : "false") +
                " language=" +
                (selected ? std::to_string(target_language) : "global") +
                " replaced=" + (replaced ? "true" : "false") +
                " lipArmed=" + (lip_armed ? "true" : "false") +
                " result=" + std::to_string(result) +
                " event=" + (event.empty() ? "<empty>" : event) +
                " source=" + (source.empty() ? "<empty>" : source) +
                " target=" + (replacement.empty() ? "<unchanged>" : replacement)).c_str());
    }
    if (lip_armed && result == 0) {
        g_pending_lip_route = {};
    }
    return result;
}

int CharacterVoiceModule::HookLoadFilePackage(
    void* package_path, uint32_t* package_id, const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return 0;
    }
    const auto original = reinterpret_cast<LoadFilePackageFn>(
        module->load_file_package_original_);
    const int result = original == nullptr
        ? 0 : original(package_path, package_id, method_info);
    if (g_loading_auxiliary_package) {
        const uint64_t hit = module->package_load_hits_.fetch_add(
            1, std::memory_order_relaxed) + 1;
        if (ShouldLog(hit)) {
            LogInfo(
                module->Id(),
                ("auxiliary PCK load hit=" + std::to_string(hit) +
                    " result=" + std::to_string(result) +
                    " packageId=" + std::to_string(package_id ? *package_id : 0) +
                    " path=" + module->runtime_->CopyString(package_path)).c_str());
        }
    }
    return result;
}

int CharacterVoiceModule::HookUnloadFilePackage(
    uint32_t package_id, const void* method_info) {
    CharacterVoiceModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr) {
        return 0;
    }
    if (g_loading_auxiliary_package) {
        const uint64_t hit = module->package_unload_hits_.fetch_add(
            1, std::memory_order_relaxed) + 1;
        if (ShouldLog(hit)) {
            LogInfo(
                module->Id(),
                ("preserved active PCK during auxiliary mount; hit=" +
                    std::to_string(hit) + " packageId=" +
                    std::to_string(package_id)).c_str());
        }
        return kAkSuccess;
    }
    const auto original = reinterpret_cast<UnloadFilePackageFn>(
        module->unload_file_package_original_);
    return original == nullptr ? 0 : original(package_id, method_info);
}

}  // namespace betterendfield
