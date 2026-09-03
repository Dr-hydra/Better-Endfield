#pragma once

#include "core/hook_broker.h"
#include "core/runtime.h"
#include "modules/module.h"

#include <atomic>
#include <array>
#include <cstddef>
#include <cstdint>
#include <mutex>
#include <string>
#include <string_view>
#include <vector>

namespace betterendfield {

struct MethodInfo;

class CharacterVoiceModule final : public Module {
public:
    const char* Id() const override;
    ModuleResult Start(Il2CppRuntime& runtime) override;

private:
    struct Catalog {
        std::string file_name;
        std::vector<std::byte> blob;
    };

    struct VoiceRule {
        std::string speaker;
        int language = -1;
    };

    struct Route {
        uint32_t source_id = 0;
        uint32_t reserved = 0;
        const void* media_memory = nullptr;
        uint32_t media_size = 0;
        uint32_t padding = 0;
    };

    static_assert(sizeof(Route) == 24);

    using PlayVoiceFn = void (*)(void* voice_context, const void* method_info);
    using MediaFn = int (*)(void* settings, uint32_t count, const void* method_info);
    using ExternalEventFn = uint32_t (*)(
        void* event_name,
        uint64_t audio_object_id,
        void* external_source_key,
        uint32_t external_cookie,
        uint32_t callback_type,
        void* callback,
        void* cookie,
        uint32_t codec,
        const void* method_info);
    using LoadFilePackageFn = int (*)(
        void* package_path, uint32_t* package_id, const void* method_info);
    using UnloadFilePackageFn = int (*)(
        uint32_t package_id, const void* method_info);
    using DurationFn = bool (*)(
        void* voice_id, float* duration, const void* method_info);
    using GetCurrentLanguageFn = int (*)(const void* method_info);
    using LipDialogFn = void (*)(
        void* instance, void* action_data, void* entity, const void* method_info);
    using LipPathFn = void* (*)(
        int language, void* voice_id, void* suffix, const void* method_info);
    using LipLoadFn = bool (*)(
        void* line_id, void** track, const void* method_info);
    using InternalPlayVoiceFn = uint32_t (*)(
        void* voice_context_reference, const void* method_info);
    using ChannelPlayVoiceFn = void (*)(
        void* instance, void* voice_context, const void* method_info);
    using PlayEventFn = uint32_t (*)(
        void* event_name,
        uint64_t audio_object_id,
        uint32_t handle_id,
        const void* method_info);
    using NarrativeFn = uint32_t (*)(
        void* instance,
        void* voice_id,
        uint64_t audio_object_id,
        void* config,
        const void* method_info);

    static void HookPlayVoice(void* voice_context, const void* method_info);
    static int HookSetMedia(
        void* settings, uint32_t count, const void* method_info);
    static int HookUnsetMedia(
        void* settings, uint32_t count, const void* method_info);
    static uint32_t HookExternalEvent(
        void* event_name,
        uint64_t audio_object_id,
        void* external_source_key,
        uint32_t external_cookie,
        uint32_t callback_type,
        void* callback,
        void* cookie,
        uint32_t codec,
        const void* method_info);
    static int HookLoadFilePackage(
        void* package_path, uint32_t* package_id, const void* method_info);
    static int HookUnloadFilePackage(
        uint32_t package_id, const void* method_info);
    static bool HookDuration(
        void* voice_id, float* duration, const void* method_info);
    static int HookGetCurrentLanguage(const void* method_info);
    static void HookLipDialog(
        void* instance, void* action_data, void* entity, const void* method_info);
    static void* HookLipPath(
        int language, void* voice_id, void* suffix, const void* method_info);
    static bool HookLipLoad(
        void* line_id, void** track, const void* method_info);
    static uint32_t HookInternalPlayVoice(
        void* voice_context_reference, const void* method_info);
    static void HookChannelPlayVoice(
        void* instance, void* voice_context, const void* method_info);
    static uint32_t HookPlayEvent(
        void* event_name,
        uint64_t audio_object_id,
        uint32_t handle_id,
        const void* method_info);
    static uint32_t HookNarrative(
        void* instance,
        void* voice_id,
        uint64_t audio_object_id,
        void* config,
        const void* method_info);

    bool ApplyRoutes();
    bool EnsureLanguagePackage(int language);
    int SelectLanguage(
        std::string_view value,
        bool allow_token_match = true,
        std::string* speaker = nullptr) const;
    std::string ReadManagedGetter(
        const MethodInfo* getter, void* instance) const;
    std::string SpeakerFromContext(void* voice_context) const;
    void ReassertTouched(void* settings, uint32_t count, const char* source);
    void RemoveHooks();

    std::vector<Catalog> catalogs_;
    std::vector<Route> routes_;
    std::vector<VoiceRule> rules_;
    Il2CppRuntime* runtime_ = nullptr;
    HookBroker hook_broker_;
    std::mutex route_mutex_;
    bool routes_applied_ = false;
    void* play_voice_original_ = nullptr;
    void* set_media_original_ = nullptr;
    void* unset_media_original_ = nullptr;
    void* external_event_original_ = nullptr;
    void* load_file_package_original_ = nullptr;
    void* unload_file_package_original_ = nullptr;
    void* get_current_language_ = nullptr;
    void* get_language_name_ = nullptr;
    void* try_load_language_package_ = nullptr;
    void* duration_original_ = nullptr;
    void* get_current_language_original_ = nullptr;
    void* lip_dialog_original_ = nullptr;
    void* lip_path_original_ = nullptr;
    void* lip_load_original_ = nullptr;
    const MethodInfo* lip_real_actor_getter_ = nullptr;
    const MethodInfo* lip_actor_getter_ = nullptr;
    const MethodInfo* lip_trunk_getter_ = nullptr;
    ResolvedField voice_context_voice_data_;
    ResolvedField runtime_voice_data_speaker_channel_;
    void* internal_play_voice_original_ = nullptr;
    void* channel_play_voice_original_ = nullptr;
    void* play_event_original_ = nullptr;
    void* narrative_original_ = nullptr;
    void* play_voice_stub_ = nullptr;
    void* set_media_stub_ = nullptr;
    void* unset_media_stub_ = nullptr;
    void* external_event_stub_ = nullptr;
    void* load_file_package_stub_ = nullptr;
    void* unload_file_package_stub_ = nullptr;
    void* duration_stub_ = nullptr;
    void* get_current_language_stub_ = nullptr;
    void* lip_dialog_stub_ = nullptr;
    void* lip_path_stub_ = nullptr;
    void* lip_load_stub_ = nullptr;
    void* internal_play_voice_stub_ = nullptr;
    void* channel_play_voice_stub_ = nullptr;
    void* play_event_stub_ = nullptr;
    void* narrative_stub_ = nullptr;
    std::atomic_uint64_t play_voice_hits_{0};
    std::atomic_uint64_t set_media_hits_{0};
    std::atomic_uint64_t unset_media_hits_{0};
    std::atomic_uint64_t external_event_hits_{0};
    std::atomic_uint64_t package_load_hits_{0};
    std::atomic_uint64_t package_unload_hits_{0};
    std::atomic_uint64_t duration_hits_{0};
    std::atomic_uint64_t language_override_hits_{0};
    std::atomic_uint64_t lip_dialog_hits_{0};
    std::atomic_uint64_t lip_path_hits_{0};
    std::atomic_uint64_t lip_load_hits_{0};
    std::atomic_uint64_t internal_play_voice_hits_{0};
    std::atomic_uint64_t channel_play_voice_hits_{0};
    std::atomic_uint64_t play_event_hits_{0};
    std::atomic_uint64_t narrative_hits_{0};
    std::mutex package_mutex_;
    std::array<bool, 4> language_package_ready_{};
    std::array<uint64_t, 4> language_package_retry_at_ms_{};

    static std::atomic<CharacterVoiceModule*> active_;
};

}  // namespace betterendfield
