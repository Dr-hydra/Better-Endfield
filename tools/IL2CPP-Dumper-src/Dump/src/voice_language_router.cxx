#include "../include/voice_language_router.hxx"

#include "../include/generated/voice_runtime_map.generated.hxx"
#include "../include/il2cpp_api.hxx"
#include "../include/utils.hxx"
#include "../../third_party/minhook/include/MinHook.h"

#include <algorithm>
#include <array>
#include <atomic>
#include <bcrypt.h>
#include <cctype>
#include <cmath>
#include <cstdio>
#include <cstring>
#include <cstdint>
#include <intrin.h>
#include <string>
#include <unordered_map>
#include <unordered_set>
#include <vector>
#include <windows.h>

#pragma comment( lib, "bcrypt.lib" )

namespace {

    constexpr uintptr_t VOICE_MANAGER_SPEAK_STRING_RVA = 0x03ABC4C0;
    constexpr uintptr_t VOICE_MANAGER_SPEAK_RVA = 0x03AC0EB0;
    constexpr uintptr_t VOICE_SPEAK_CHANNEL_PLAY_VOICE_RVA = 0x03ABF3A0;
    constexpr uintptr_t VOICE_UTILS_SELECT_WWISE_EVENT_RVA = 0x03ABF630;
    constexpr uintptr_t AUDIO_ADAPTER_POST_EVENT_EXTERNAL_RVA = 0x03ABF0A0;
    constexpr uintptr_t AUDIO_ADAPTER_POST_EVENT_INTERNAL_RVA = 0x03ABEA70;
    constexpr uintptr_t AUDIO_ADAPTER_POST_EVENT_STRING_RVA = 0x0635A760;
    constexpr uintptr_t AUDIO_ADAPTER_POST_EVENT_ID_RVA = 0x0635A65C;
    constexpr uintptr_t AUDIO_ADAPTER_POST_EVENT_NORMAL_INTERNAL_RVA =
        0x0328A690;
    constexpr uintptr_t AUDIO_ADAPTER_NORMAL_CALLBACK_RVA = 0x0328D3E0;
    constexpr uintptr_t AUDIO_ADAPTER_STOP_BY_PLAYING_ID_RVA = 0x0635BBD0;
    constexpr uintptr_t AUDIO_ADAPTER_EXECUTE_ACTION_RVA = 0x03870420;
    constexpr uintptr_t AUDIO_ACTION_QUEUE_EXECUTE_RVA = 0x03870520;
    constexpr uintptr_t AUDIO_ACTION_QUEUE_CONSUME_EXECUTE_RVA = 0x0328C150;
    constexpr uintptr_t AUDIO_ADAPTER_EXTERNAL_CALLBACK_RVA = 0x043C7930;
    constexpr uintptr_t AUDIO_ADAPTER_TRY_GET_REAL_PLAYING_ID_RVA = 0x0436E720;
    constexpr uintptr_t AK_SOUND_ENGINE_GET_SOURCE_POSITION_RVA = 0x053D250C;
    constexpr uintptr_t AK_EVENT_CALLBACK_GET_PLAYING_ID_RVA = 0x053A1688;
    constexpr uintptr_t VOICE_PLAYER_PLAY_VOICE_RVA = 0x03ABF7B0;
    constexpr uintptr_t VOICE_PLAYER_PLAY_VOICE_INTERNAL_RVA = 0x03ABF1B0;
    constexpr uintptr_t VOICE_PLAYER_PLAY_EXTERNAL_RVA = 0x06AF0458;
    constexpr uintptr_t VOICE_PLAYER_PLAY_EVENT_RVA = 0x03ABF2D0;
    constexpr uintptr_t VOICE_SPEAK_CHANNEL_STOP_RVA = 0x03ABC9A0;
    constexpr uintptr_t VOICE_PLAYER_STOP_VOICE_RVA = 0x03ABCA20;
    constexpr uintptr_t VOICE_UTILS_TRY_GET_DURATION_RVA = 0x03ABB800;
    constexpr uintptr_t VOICE_MANAGER_SPEAK_NARRATIVE_RVA = 0x045E3F20;
    constexpr uintptr_t VOICE_I18N_SET_LANGUAGE_RVA = 0x03EB3F70;
    constexpr uintptr_t VOICE_I18N_GET_CURRENT_LANGUAGE_RVA = 0x03AC13B0;
    constexpr uintptr_t VOICE_I18N_GET_LANGUAGE_NAME_RVA = 0x03EB40D0;
    constexpr uintptr_t AUDIO_VFS_TRY_LOAD_LANGUAGE_PCK_RVA = 0x03EB45A0;
    constexpr uintptr_t AK_SOUND_ENGINE_LOAD_FILE_PACKAGE_RVA = 0x03EB5CD0;
    constexpr uintptr_t AK_SOUND_ENGINE_UNLOAD_FILE_PACKAGE_RVA = 0x053DC840;
    constexpr uintptr_t AK_SOUND_ENGINE_GET_CURRENT_LANGUAGE_RVA = 0x053D10A4;
    constexpr std::size_t VOICE_CONTEXT_WWISE_EVENT_OFFSET = 0x20;
    constexpr std::size_t VOICE_CONTEXT_SPEAKER_CHANNEL_OFFSET = 0x50;
    constexpr std::size_t VOICE_CONTEXT_DATA_OFFSET = 0x60;
    constexpr std::size_t VOICE_CONTEXT_CODEC_OFFSET = 0x68;
    constexpr int NO_DEFAULT_LANGUAGE = -2;
    constexpr int FOLLOW_GLOBAL_LANGUAGE = -1;
    constexpr std::size_t HOOK_FINGERPRINT_SIZE = 16;
    constexpr std::uint32_t DIAGNOSTIC_HIT_LIMIT = 8;
    constexpr std::uint32_t VOICE_SELECT_MATCH_LOG_LIMIT = 32;
    constexpr std::uint32_t VOICE_SUBMIT_MATCH_LOG_LIMIT = 64;
    constexpr std::uint32_t PACKAGED_MEDIA_DIAGNOSTIC_LOG_LIMIT = 96;
    constexpr std::uint32_t LIP_ROUTE_LOG_LIMIT = 64;
    constexpr std::uint32_t LIP_DIAGNOSTIC_LOG_LIMIT = 32;
    constexpr std::uint32_t VOICE_LIFECYCLE_LOG_LIMIT = 2048;
    constexpr std::size_t VOICE_LIFECYCLE_TRACE_LIMIT = 2048;
    constexpr std::uint64_t VOICE_REQUEST_DIAGNOSTIC_GRACE_MS = 1000;
#ifdef EFSTARTCHANGE_VOICE_DIAGNOSTIC_BUILD
    constexpr bool VOICE_DIAGNOSTICS_DEFAULT = true;
    constexpr const char * VOICE_DIAGNOSTIC_BUILD_MARKER =
        "Better Endfield native-container voice map v10";
#else
    constexpr bool VOICE_DIAGNOSTICS_DEFAULT = false;
#endif
    constexpr std::uint8_t VOICE_MANAGER_SPEAK_STRING_SIGNATURE [ ] = {
        0x48, 0x89, 0x6C, 0x24, 0x10, 0x48, 0x89, 0x74,
        0x24, 0x18, 0x48, 0x89, 0x7C, 0x24, 0x20
    };
    constexpr std::uint8_t VOICE_MANAGER_SPEAK_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x74,
        0x24, 0x10, 0x57, 0x48, 0x83, 0xEC, 0x70
    };
    constexpr std::uint8_t VOICE_SPEAK_CHANNEL_PLAY_VOICE_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x74,
        0x24, 0x10, 0x57, 0x48, 0x83, 0xEC, 0x30
    };
    constexpr std::uint8_t VOICE_UTILS_SELECT_WWISE_EVENT_SIGNATURE [ ] = {
        0x40, 0x53, 0x48, 0x83, 0xEC, 0x40, 0x48, 0x8B,
        0xD9, 0x0F, 0x57, 0xC0
    };
    constexpr std::uint8_t AUDIO_ADAPTER_POST_EVENT_EXTERNAL_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x6C,
        0x24, 0x10, 0x48, 0x89, 0x74, 0x24, 0x18
    };
    constexpr std::uint8_t AUDIO_ADAPTER_POST_EVENT_INTERNAL_SIGNATURE [ ] = {
        0x89, 0x4C, 0x24, 0x08, 0x53, 0x55, 0x56, 0x57,
        0x48, 0x81, 0xEC, 0x98, 0x00, 0x00, 0x00
    };
    constexpr std::uint8_t AUDIO_ADAPTER_POST_EVENT_STRING_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x6C,
        0x24, 0x10, 0x48, 0x89, 0x74, 0x24, 0x18
    };
    constexpr std::uint8_t AUDIO_ADAPTER_POST_EVENT_ID_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x6C,
        0x24, 0x10, 0x48, 0x89, 0x74, 0x24, 0x18
    };
    constexpr std::uint8_t
        AUDIO_ADAPTER_POST_EVENT_NORMAL_INTERNAL_SIGNATURE [ ] = {
        0x48, 0x89, 0x6C, 0x24, 0x18, 0x57, 0x41, 0x56,
        0x41, 0x57, 0x48, 0x81, 0xEC, 0x80, 0x00, 0x00,
        0x00
    };
    constexpr std::uint8_t AUDIO_ADAPTER_NORMAL_CALLBACK_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x18, 0x48, 0x89, 0x74,
        0x24, 0x20, 0x55, 0x57, 0x41, 0x57, 0x48
    };
    constexpr std::uint8_t AUDIO_ADAPTER_STOP_BY_PLAYING_ID_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x57, 0x48, 0x83,
        0xEC, 0x20
    };
    constexpr std::uint8_t AUDIO_ADAPTER_EXECUTE_ACTION_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x74,
        0x24, 0x10, 0x57, 0x48, 0x83, 0xEC, 0x40
    };
    constexpr std::uint8_t AUDIO_ACTION_QUEUE_EXECUTE_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x6C,
        0x24, 0x10, 0x48, 0x89, 0x74, 0x24, 0x18
    };
    constexpr std::uint8_t AUDIO_ACTION_QUEUE_CONSUME_EXECUTE_SIGNATURE [ ] = {
        0x40, 0x53, 0x48, 0x83, 0xEC, 0x50, 0x8B, 0xD9,
        0xC7, 0x44, 0x24, 0x70, 0x00, 0x00, 0x00, 0x00
    };
    constexpr std::uint8_t AUDIO_ADAPTER_EXTERNAL_CALLBACK_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x74,
        0x24, 0x10, 0x57, 0x48, 0x81, 0xEC, 0x80, 0x00,
        0x00, 0x00
    };
    constexpr std::uint8_t AUDIO_ADAPTER_TRY_GET_REAL_PLAYING_ID_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x57, 0x48, 0x83,
        0xEC, 0x20
    };
    constexpr std::uint8_t AK_SOUND_ENGINE_GET_SOURCE_POSITION_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x74,
        0x24, 0x10, 0x57, 0x48, 0x83, 0xEC, 0x20
    };
    constexpr std::uint8_t AK_EVENT_CALLBACK_GET_PLAYING_ID_SIGNATURE [ ] = {
        0x40, 0x53, 0x48, 0x83, 0xEC, 0x20
    };
    constexpr std::uint8_t VOICE_PLAYER_PLAY_VOICE_SIGNATURE [ ] = {
        0x48, 0x89, 0x4C, 0x24, 0x08, 0x53, 0x48, 0x83,
        0xEC, 0x20, 0x48, 0x8B, 0xD9, 0x33, 0xD2
    };
    constexpr std::uint8_t VOICE_PLAYER_PLAY_VOICE_INTERNAL_SIGNATURE [ ] = {
        0x40, 0x53, 0x48, 0x83, 0xEC, 0x50, 0x80, 0x3D
    };
    constexpr std::uint8_t VOICE_PLAYER_PLAY_EXTERNAL_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x6C,
        0x24, 0x18, 0x56, 0x57, 0x41, 0x56, 0x48, 0x83
    };
    constexpr std::uint8_t VOICE_PLAYER_PLAY_EVENT_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x10, 0x48, 0x89, 0x6C,
        0x24, 0x18, 0x56, 0x48, 0x83, 0xEC, 0x40
    };
    constexpr std::uint8_t VOICE_SPEAK_CHANNEL_STOP_SIGNATURE [ ] = {
        0x40, 0x53, 0x48, 0x83, 0xEC, 0x20, 0x80, 0x3D
    };
    constexpr std::uint8_t VOICE_PLAYER_STOP_VOICE_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x74,
        0x24, 0x10, 0x57, 0x48, 0x83, 0xEC, 0x20
    };
    constexpr std::uint8_t VOICE_UTILS_TRY_GET_DURATION_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x48, 0x89, 0x74,
        0x24, 0x10, 0x57, 0x48, 0x83, 0xEC, 0x50
    };
    constexpr std::uint8_t VOICE_MANAGER_SPEAK_NARRATIVE_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x10, 0x48, 0x89, 0x74,
        0x24, 0x18, 0x57, 0x41, 0x56, 0x41, 0x57
    };
    constexpr std::uint8_t VOICE_I18N_SET_LANGUAGE_SIGNATURE [ ] = {
        0x40, 0x53, 0x48, 0x83, 0xEC, 0x20, 0x80, 0x3D
    };
    constexpr std::uint8_t VOICE_I18N_GET_CURRENT_LANGUAGE_SIGNATURE [ ] = {
        0x48, 0x83, 0xEC, 0x28, 0x33, 0xD2, 0xB9, 0x87,
        0x23, 0x00, 0x00
    };
    constexpr std::uint8_t VOICE_I18N_GET_LANGUAGE_NAME_SIGNATURE [ ] = {
        0x40, 0x53, 0x48, 0x83, 0xEC, 0x20
    };
    constexpr std::uint8_t AUDIO_VFS_TRY_LOAD_LANGUAGE_PCK_SIGNATURE [ ] = {
        0x40, 0x53, 0x48, 0x83, 0xEC, 0x20, 0x80, 0x3D
    };
    constexpr std::uint8_t AK_SOUND_ENGINE_LOAD_FILE_PACKAGE_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x57, 0x48, 0x83,
        0xEC, 0x20
    };
    constexpr std::uint8_t AK_SOUND_ENGINE_UNLOAD_FILE_PACKAGE_SIGNATURE [ ] = {
        0x40, 0x53, 0x48, 0x83, 0xEC, 0x20, 0x80, 0x3D
    };
    constexpr std::uint8_t AK_EXTERNAL_SOURCE_FILE_SETTER_SIGNATURE [ ] = {
        0x48, 0x89, 0x5C, 0x24, 0x08, 0x57, 0x48, 0x83,
        0xEC, 0x20
    };
    // The Wwise getter is used as an optional diagnostic/restore source. Keep
    // this fingerprint short so a harmless compiler prologue change does not
    // disable the otherwise valid voice hooks.
    constexpr std::uint8_t AK_SOUND_ENGINE_GET_CURRENT_LANGUAGE_SIGNATURE [ ] = {
        0x40, 0x53, 0x48, 0x83, 0xEC, 0x20
    };

    using VoiceManagerSpeakStringFn = bool ( __fastcall * )(
        void *, void *, std::uint64_t, std::uint32_t * );
    using VoiceContextInstanceUintFn = std::uint32_t ( __fastcall * )(
        void *, void * );
    using VoiceContextInstanceVoidFn = void ( __fastcall * )( void *, void * );
    using VoiceContextStaticVoidFn = void ( __fastcall * )( void * );
    using VoiceContextRefStaticUintFn = std::uint32_t ( __fastcall * )( void * );
    using VoicePlayerPlayEventFn = std::uint32_t ( __fastcall * )(
        void *, std::uint64_t, std::uint32_t, void * );
    using VoicePlayerPlayExternalFn = std::uint32_t ( __fastcall * )(
        void *, void *, std::uint64_t, std::uint32_t, std::uint32_t, void * );
    using VoiceSpeakChannelStopFn = void ( __fastcall * )( void *, void * );
    using VoicePlayerStopVoiceFn = void ( __fastcall * )(
        std::uint32_t, std::int32_t, void * );
    using VoiceUtilsTryGetDurationFn = bool ( __fastcall * )(
        void *, float *, void * );
    using VoiceManagerSpeakNarrativeFn = std::uint32_t ( __fastcall * )(
        void *, void *, std::uint64_t, void * );
    using VoiceI18nSetLanguageFn = void ( __fastcall * )( int, void * );
    using VoiceI18nGetCurrentLanguageFn = int ( __fastcall * )( void * );
    using VoiceI18nGetLanguageNameFn = void * ( __fastcall * )(
        int, void * );
    using AudioVfsTryLoadLanguagePckFn = bool ( __fastcall * )(
        void *, void * );
    using AkSoundEngineLoadFilePackageFn = int ( __fastcall * )(
        void *, std::uint32_t *, void * );
    using AkSoundEngineUnloadFilePackageFn = int ( __fastcall * )(
        std::uint32_t, void * );
    using AkSoundEngineGetCurrentLanguageFn = void * ( __fastcall * )( void * );
    using AkSoundEngineSetMediaFn = int ( __fastcall * )(
        void *, std::uint32_t, void * );
    using AkSoundEngineUnsetMediaFn = int ( __fastcall * )(
        void *, std::uint32_t, void * );
    using AkExternalSourceFileSetterFn = void ( __fastcall * )(
        void *, void *, void * );
    using AkExternalSourceMemorySetterFn = void ( __fastcall * )(
        void *, std::intptr_t, void * );
    using AkExternalSourceMemorySizeSetterFn = void ( __fastcall * )(
        void *, std::uint32_t, void * );
    using AudioAdapterPostEventInternalFn = std::uint32_t ( __fastcall * )(
        std::uint32_t, std::uint64_t, void *, std::uint32_t, std::uint32_t,
        void *, void *, std::uint32_t );
    using AudioAdapterPostEventStringFn = std::uint32_t ( __fastcall * )(
        void *, std::uint64_t, std::uint32_t, void *, void * );
    using AudioAdapterPostEventIdFn = std::uint32_t ( __fastcall * )(
        std::uint32_t, std::uint64_t, std::uint32_t, void *, void * );
    using AudioAdapterPostEventNormalInternalFn = std::uint32_t (
        __fastcall * )(
        std::uint32_t, std::uint64_t, std::uint32_t, void *, void * );
    using AudioAdapterStopByPlayingIdFn = void ( __fastcall * )(
        std::uint32_t, std::int32_t );
    using AudioAdapterExecuteActionFn = void ( __fastcall * )(
        std::int32_t, std::uint32_t, std::int32_t );
    using AudioActionQueueExecuteFn = void ( __fastcall * )(
        std::uint32_t, std::int32_t, std::int32_t );
    using AudioActionQueueConsumeExecuteFn = void ( __fastcall * )(
        std::int32_t );
    using AudioAdapterExternalCallbackFn = void ( __fastcall * )(
        void *, std::int32_t, void * );
    using AudioAdapterTryGetRealPlayingIdFn = bool ( __fastcall * )(
        std::uint32_t, std::uint32_t *, void * );
    using AkSoundEngineGetSourcePositionFn = std::int32_t ( __fastcall * )(
        std::uint32_t, std::int32_t *, bool, void * );
    using AkEventCallbackGetPlayingIdFn = std::uint32_t ( __fastcall * )(
        void *, void * );
    using Il2CppStringNewFn = void * ( __fastcall * )( const char * );
    using DialogManagerPlayLipSyncTrackFn = void ( __fastcall * )(
        void *, void *, void *, void * );
    using LipSyncGetTrackPathFn = void * ( __fastcall * )(
        int, void *, void *, void * );
    using LipSyncTryLoadTrackFn = bool ( __fastcall * )(
        void *, void **, void * );

    struct LipRoutingContext {
        bool active = false;
        int target = FOLLOW_GLOBAL_LANGUAGE;
        int sourceLanguage = FOLLOW_GLOBAL_LANGUAGE;
        char actor [ 192 ] = { 0 };
        char matchedIdentity [ 192 ] = { 0 };
        char trunkId [ 256 ] = { 0 };
    };

    struct PendingLipRoute {
        bool armed = false;
        int target = FOLLOW_GLOBAL_LANGUAGE;
        std::uint64_t generation = 0;
        char lineId [ 256 ] = { 0 };
        char matchedIdentity [ 192 ] = { 0 };
    };

    struct VoiceRequestDiagnosticContext {
        bool matched = false;
        bool scopeActive = false;
        int target = FOLLOW_GLOBAL_LANGUAGE;
        std::uint32_t codec = 0;
        std::uint64_t observedAtMs = 0;
        void * wwiseEventObject = nullptr;
        char matchedIdentity [ 192 ] = { 0 };
        char speaker [ 192 ] = { 0 };
        char data [ 512 ] = { 0 };
        char event [ 192 ] = { 0 };
    };

    struct DurationRoutingContext {
        bool active = false;
        int target = FOLLOW_GLOBAL_LANGUAGE;
        char voiceId [ 256 ] = { 0 };
        char matchedIdentity [ 192 ] = { 0 };
    };

    struct PackagedMediaSetterContext {
        bool active = false;
        bool setterObserved = false;
        bool setterRouted = false;
        int target = FOLLOW_GLOBAL_LANGUAGE;
        std::uint32_t eventId = 0;
        std::uint32_t mediaId = 0;
        std::uint32_t expectedCodec = 0;
        const void * memory = nullptr;
        std::uint32_t memorySize = 0;
        char voiceId [ 256 ] = { 0 };
    };

    struct ResidentWem {
        void * memory = nullptr;
        std::uint32_t size = 0;
    };

#pragma pack( push, 1 )
    struct NativeAkSourceSettings {
        std::uint32_t sourceId;
        std::uint32_t reserved;
        const void * mediaMemory;
        std::uint32_t mediaSize;
        std::uint32_t padding;
    };
#pragma pack( pop )
    static_assert( sizeof( NativeAkSourceSettings ) == 24 );

    struct ThreadRoutingContext {
        bool routingVoice = false;
        bool bankEventRouted = false;
        bool inExternalSourceSubmit = false;
        bool inNormalPostEventString = false;
        bool inNormalPostEventId = false;
        bool consumingActionQueue = false;
        std::int32_t actionQueueFrame = -1;
        VoiceRequestDiagnosticContext voiceRequest;
        DurationRoutingContext durationRouting;
        PackagedMediaSetterContext packagedMediaSetter;
        LipRoutingContext lipRouting;
        PendingLipRoute pendingLipRoute;
    };

    struct VoicePlaybackTrace {
        std::uint32_t playingId = 0;
        std::uint64_t audioObjectId = 0;
        std::uint32_t externalCookie = 0;
        std::uint32_t callbackType = 0;
        std::uint32_t codec = 0;
        std::uint64_t startedAtMs = 0;
        std::uint64_t lastActionAtMs = 0;
        std::int32_t lastAction = -1;
        std::int32_t lastActionDurationMs = 0;
        std::int32_t globalDurationMs = -1;
        std::int32_t configuredDurationMs = -1;
        bool matched = false;
        bool replaced = false;
        bool bypassedStringHook = false;
        int targetLanguage = FOLLOW_GLOBAL_LANGUAGE;
        std::string matchedIdentity;
        std::string event;
        std::string originalSource;
        std::string submittedSource;
    };

    struct ManagedGetterStringResult {
        std::string raw;
        std::string normalized;
        const char * status = "not-called";
        void * managedException = nullptr;
    };

    struct ConfigFileStamp {
        bool exists = false;
        std::uint64_t writeTime = 0;
        std::uint64_t size = 0;
    };

    using VoiceRuleMap = std::unordered_map< std::string, int >;

    struct VoiceConfigurationSnapshot {
        bool enabled = false;
        bool replaceNarrativeVoice = true;
        bool diagnostics = VOICE_DIAGNOSTICS_DEFAULT;
        VoiceRuleMap rules;
        int defaultLanguage = NO_DEFAULT_LANGUAGE;
    };

    std::atomic< bool > g_enabled { false };
    std::atomic< bool > g_replaceNarrativeVoice { true };
    std::atomic< bool > g_diagnosticsEnabled { VOICE_DIAGNOSTICS_DEFAULT };
    std::atomic< bool > g_shuttingDown { false };
    std::atomic< bool > g_hookHostAvailable { true };
    std::atomic< std::uint64_t > g_configGeneration { 0 };
    std::atomic< std::uint32_t > g_playVoiceHits { 0 };
    std::atomic< std::uint32_t > g_managerSpeakStringHits { 0 };
    std::atomic< std::uint32_t > g_managerSpeakHits { 0 };
    std::atomic< std::uint32_t > g_channelPlayVoiceHits { 0 };
    std::atomic< std::uint32_t > g_selectWwiseEventHits { 0 };
    std::atomic< std::uint32_t > g_selectWwiseEventMatchLogs { 0 };
    std::atomic< std::uint32_t > g_submitExternalHits { 0 };
    std::atomic< std::uint32_t > g_submitExternalMatchLogs { 0 };
    std::atomic< std::uint32_t > g_normalPostStringHits { 0 };
    std::atomic< std::uint32_t > g_normalPostIdHits { 0 };
    std::atomic< std::uint32_t > g_normalPostInternalHits { 0 };
    std::atomic< std::uint32_t > g_internalPlayVoiceHits { 0 };
    std::atomic< std::uint32_t > g_narrativeHits { 0 };
    std::atomic< std::uint32_t > g_identityFailures { 0 };
    std::atomic< std::uint32_t > g_narrativeSkipLogs { 0 };
    std::atomic< std::uint32_t > g_lipRouteLogs { 0 };
    std::atomic< std::uint32_t > g_lipDialogHits { 0 };
    std::atomic< std::uint32_t > g_lipPathHits { 0 };
    std::atomic< std::uint32_t > g_lipLoadHits { 0 };
    std::atomic< std::uint32_t > g_lipArmLogs { 0 };
    std::atomic< std::uint32_t > g_lipLanguageOverrideHits { 0 };
    std::atomic< std::uint32_t > g_durationLanguageOverrideHits { 0 };
    std::atomic< std::uint32_t > g_packagedMediaDiagnosticLogs { 0 };
    std::atomic< std::uint32_t > g_lifecycleLogs { 0 };
    std::atomic< bool > g_lipHooksAttempted { false };
    std::atomic< bool > g_lipHooksReady { false };
    std::array< std::atomic< bool >, 4 > g_languagePackageAttempted { };
    std::array< std::atomic< bool >, 4 > g_languagePackageReady { };
    std::atomic< bool > g_auxiliaryPackageLoad { false };
    std::atomic< int > g_auxiliaryPackageLanguage { FOLLOW_GLOBAL_LANGUAGE };
    std::atomic< std::uint32_t > g_auxiliaryPackageLoads { 0 };
    std::atomic< std::uint32_t > g_auxiliaryPackageUnloadsSuppressed { 0 };
    std::atomic< bool > g_packagedMemoryReady { false };
    std::atomic< bool > g_nativeMediaRouteReady { false };
    std::unordered_map< std::uint32_t, ResidentWem > g_residentWems;
    std::unordered_map< std::uint32_t, std::uint32_t >
        g_registeredNativeMedia;
    std::unordered_set< std::uint32_t > g_uncertainNativeMediaSources;
    std::atomic< bool > g_nativeMediaUnloadSafe { true };
    VoiceRuleMap g_rules;
    std::unordered_set< std::string > g_observedSpeakers;
    int g_defaultLanguage = NO_DEFAULT_LANGUAGE;
    std::string g_configPath;
    ConfigFileStamp g_appliedConfigStamp;
    ConfigFileStamp g_pendingConfigStamp;
    bool g_configReloadPending = false;
    std::uint64_t g_configReloadDetectedAt = 0;
    SRWLOCK g_rulesLock = SRWLOCK_INIT;
    SRWLOCK g_observedLock = SRWLOCK_INIT;
    SRWLOCK g_routingTlsInitLock = SRWLOCK_INIT;
    SRWLOCK g_lifecycleLock = SRWLOCK_INIT;
    SRWLOCK g_residentWemsLock = SRWLOCK_INIT;
    SRWLOCK g_packagedPrewarmLock = SRWLOCK_INIT;
    SRWLOCK g_nativeMediaRouteLock = SRWLOCK_INIT;
    // Keep the slot for the game process lifetime. TlsFree does not clear
    // values owned by other threads, so reuse after manual unload is unsafe.
    std::atomic< DWORD > g_routingTlsIndex { TLS_OUT_OF_INDEXES };
    std::unordered_map< std::uint32_t, VoicePlaybackTrace >
        g_voicePlaybackTraces;
    std::unordered_map< std::uint64_t, std::uint32_t >
        g_lastPlayingIdByAudioObject;
    std::unordered_map< std::string, std::int32_t >
        g_configuredDurationMsByVoiceId;
    std::unordered_map< std::string, std::int32_t >
        g_globalDurationMsByVoiceId;

    void * g_voiceManagerSpeakStringTarget = nullptr;
    void * g_voiceManagerSpeakTarget = nullptr;
    void * g_voiceSpeakChannelPlayVoiceTarget = nullptr;
    void * g_voiceUtilsSelectWwiseEventTarget = nullptr;
    void * g_audioAdapterPostEventExternalTarget = nullptr;
    void * g_audioAdapterPostEventInternalTarget = nullptr;
    void * g_audioAdapterPostEventStringTarget = nullptr;
    void * g_audioAdapterPostEventIdTarget = nullptr;
    void * g_audioAdapterPostEventNormalInternalTarget = nullptr;
    void * g_audioAdapterNormalCallbackTarget = nullptr;
    void * g_audioAdapterStopByPlayingIdTarget = nullptr;
    void * g_audioAdapterExecuteActionTarget = nullptr;
    void * g_audioActionQueueExecuteTarget = nullptr;
    void * g_audioActionQueueConsumeExecuteTarget = nullptr;
    void * g_audioAdapterExternalCallbackTarget = nullptr;
    void * g_voicePlayerPlayVoiceTarget = nullptr;
    void * g_voicePlayerPlayVoiceInternalTarget = nullptr;
    void * g_voicePlayerPlayExternalTarget = nullptr;
    void * g_voicePlayerPlayEventTarget = nullptr;
    void * g_voiceSpeakChannelStopTarget = nullptr;
    void * g_voicePlayerStopVoiceTarget = nullptr;
    void * g_voiceUtilsTryGetDurationTarget = nullptr;
    void * g_voiceManagerSpeakNarrativeTarget = nullptr;
    void * g_akSoundEngineLoadFilePackageTarget = nullptr;
    void * g_akSoundEngineUnloadFilePackageTarget = nullptr;
    void * g_akExternalSourceFileSetterTarget = nullptr;
    void * g_dialogManagerPlayLipSyncTrackTarget = nullptr;
    void * g_lipSyncGetTrackPathTarget = nullptr;
    void * g_lipSyncTryLoadTrackTarget = nullptr;
    void * g_voiceI18nGetCurrentLanguageTarget = nullptr;
    VoiceManagerSpeakStringFn g_originalVoiceManagerSpeakString = nullptr;
    VoiceContextInstanceUintFn g_originalVoiceManagerSpeak = nullptr;
    VoiceContextInstanceVoidFn g_originalVoiceSpeakChannelPlayVoice = nullptr;
    VoiceContextStaticVoidFn g_originalVoiceUtilsSelectWwiseEvent = nullptr;
    using AudioAdapterPostEventExternalFn = std::uint32_t ( __fastcall * )(
        void *, std::uint64_t, void *, std::uint32_t, std::uint32_t,
        void *, void *, std::uint32_t );
    AudioAdapterPostEventExternalFn g_originalAudioAdapterPostEventExternal = nullptr;
    AudioAdapterPostEventInternalFn g_originalAudioAdapterPostEventInternal = nullptr;
    AudioAdapterPostEventStringFn g_originalAudioAdapterPostEventString = nullptr;
    AudioAdapterPostEventIdFn g_originalAudioAdapterPostEventId = nullptr;
    AudioAdapterPostEventNormalInternalFn
        g_originalAudioAdapterPostEventNormalInternal = nullptr;
    AudioAdapterExternalCallbackFn g_originalAudioAdapterNormalCallback = nullptr;
    AudioAdapterStopByPlayingIdFn g_originalAudioAdapterStopByPlayingId = nullptr;
    AudioAdapterExecuteActionFn g_originalAudioAdapterExecuteAction = nullptr;
    AudioActionQueueExecuteFn g_originalAudioActionQueueExecute = nullptr;
    AudioActionQueueConsumeExecuteFn
        g_originalAudioActionQueueConsumeExecute = nullptr;
    AudioAdapterExternalCallbackFn g_originalAudioAdapterExternalCallback = nullptr;
    VoiceContextStaticVoidFn g_originalVoicePlayerPlayVoice = nullptr;
    VoiceContextRefStaticUintFn g_originalVoicePlayerPlayVoiceInternal = nullptr;
    VoicePlayerPlayExternalFn g_voicePlayerPlayExternal = nullptr;
    VoicePlayerPlayEventFn g_originalVoicePlayerPlayEvent = nullptr;
    VoiceSpeakChannelStopFn g_originalVoiceSpeakChannelStop = nullptr;
    VoicePlayerStopVoiceFn g_originalVoicePlayerStopVoice = nullptr;
    VoiceUtilsTryGetDurationFn g_originalVoiceUtilsTryGetDuration = nullptr;
    VoiceManagerSpeakNarrativeFn g_originalVoiceManagerSpeakNarrative = nullptr;
    AudioVfsTryLoadLanguagePckFn g_tryLoadLanguagePck = nullptr;
    AkSoundEngineLoadFilePackageFn g_originalAkSoundEngineLoadFilePackage = nullptr;
    AkSoundEngineUnloadFilePackageFn g_originalAkSoundEngineUnloadFilePackage = nullptr;
    AkExternalSourceFileSetterFn g_originalAkExternalSourceFileSetter = nullptr;
    AkExternalSourceMemorySetterFn g_akExternalSourceMemorySetter = nullptr;
    AkExternalSourceMemorySizeSetterFn g_akExternalSourceMemorySizeSetter = nullptr;
    DialogManagerPlayLipSyncTrackFn g_originalDialogManagerPlayLipSyncTrack = nullptr;
    LipSyncGetTrackPathFn g_originalLipSyncGetTrackPath = nullptr;
    LipSyncTryLoadTrackFn g_originalLipSyncTryLoadTrack = nullptr;
    VoiceI18nGetCurrentLanguageFn g_originalVoiceI18nGetCurrentLanguage = nullptr;
    void * g_dialogActionGetRealActorNameId = nullptr;
    void * g_dialogActionGetActorNameId = nullptr;
    void * g_dialogActionGetTrunkId = nullptr;
    VoiceI18nSetLanguageFn g_setLanguage = nullptr;
    VoiceI18nGetCurrentLanguageFn g_getCurrentLanguage = nullptr;
    VoiceI18nGetLanguageNameFn g_getLanguageName = nullptr;
    AkSoundEngineGetCurrentLanguageFn g_getWwiseCurrentLanguage = nullptr;
    AkSoundEngineSetMediaFn g_setMedia = nullptr;
    AkSoundEngineUnsetMediaFn g_unsetMedia = nullptr;
    AudioAdapterTryGetRealPlayingIdFn g_tryGetRealPlayingId = nullptr;
    AkSoundEngineGetSourcePositionFn g_getSourcePlayPosition = nullptr;
    AkEventCallbackGetPlayingIdFn g_getCallbackPlayingId = nullptr;
    Il2CppStringNewFn g_il2cppStringNew = nullptr;
    // Manual mapping does not provide the CRT static-TLS lifecycle. Nested
    // per-thread state is stored through the Win32 TLS API above.
    bool g_ownsMinHook = false;
    std::array< std::uint8_t, HOOK_FINGERPRINT_SIZE > g_playVoiceHookBytes { };
    bool g_playVoiceHookBytesCaptured = false;
    bool g_lifecycleInternalHookCreated = false;
    bool g_lifecycleActionHookCreated = false;
    bool g_lifecycleCallbackHookCreated = false;
    bool g_bankPostStringHookCreated = false;
    bool g_bankPostIdHookCreated = false;
    bool g_bankPostInternalHookCreated = false;
    bool g_bankCallbackHookCreated = false;
    bool g_bankRouteHookCreated = false;
    bool g_stopEntryHookCreated = false;
    bool g_actionQueueHookCreated = false;
    bool g_actionQueueConsumeHookCreated = false;
    bool g_channelStopHookCreated = false;
    bool g_voiceStopHookCreated = false;
    bool g_durationHookCreated = false;
    bool g_packagedMediaSetterHookCreated = false;
    uintptr_t g_gameAssemblyBase = 0;
    std::size_t g_gameAssemblySize = 0;
    std::atomic< bool > g_healthReported { false };
    std::atomic< bool > g_healthRepairAttempted { false };

    static bool EnsureRoutingTlsIndex( ) {
        if ( g_routingTlsIndex.load( std::memory_order_acquire ) !=
            TLS_OUT_OF_INDEXES )
            return true;

        AcquireSRWLockExclusive( &g_routingTlsInitLock );
        DWORD index = g_routingTlsIndex.load( std::memory_order_relaxed );
        if ( index == TLS_OUT_OF_INDEXES ) {
            index = TlsAlloc( );
            if ( index != TLS_OUT_OF_INDEXES )
                g_routingTlsIndex.store( index, std::memory_order_release );
        }
        ReleaseSRWLockExclusive( &g_routingTlsInitLock );
        return index != TLS_OUT_OF_INDEXES;
    }

    static ThreadRoutingContext * GetThreadRoutingContext(
        bool create = true ) {
        DWORD index = g_routingTlsIndex.load( std::memory_order_acquire );
        if ( index == TLS_OUT_OF_INDEXES ) {
            if ( !create || !EnsureRoutingTlsIndex( ) )
                return nullptr;
            index = g_routingTlsIndex.load( std::memory_order_acquire );
        }

        auto * context = static_cast< ThreadRoutingContext * >(
            TlsGetValue( index ) );
        if ( context || !create )
            return context;

        context = static_cast< ThreadRoutingContext * >(
            HeapAlloc( GetProcessHeap( ), HEAP_ZERO_MEMORY,
                sizeof( ThreadRoutingContext ) ) );
        if ( !context )
            return nullptr;
        *context = ThreadRoutingContext { };
        if ( !TlsSetValue( index, context ) ) {
            HeapFree( GetProcessHeap( ), 0, context );
            return nullptr;
        }
        return context;
    }

    static bool MatchesSignature(
        const void * target, const std::uint8_t * signature,
        std::size_t signatureLength ) {
        if ( !target || !signature || signatureLength == 0 )
            return false;
        __try {
            return std::memcmp( target, signature, signatureLength ) == 0;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }
    }

    static std::string Trim( std::string value ) {
        const auto isNotSpace = [ ] ( unsigned char ch ) {
            return !std::isspace( ch );
            };
        value.erase( value.begin( ), std::find_if(
            value.begin( ), value.end( ), isNotSpace ) );
        value.erase( std::find_if(
            value.rbegin( ), value.rend( ), isNotSpace ).base( ), value.end( ) );
        return value;
    }

    static std::string Normalize( std::string value ) {
        value = Trim( std::move( value ) );
        std::transform(
            value.begin( ), value.end( ), value.begin( ),
            [ ] ( unsigned char ch ) {
                return static_cast< char >( std::tolower( ch ) );
            } );
        return value;
    }

    static std::string CanonicalVoiceDurationKey( std::string value ) {
        value = Normalize( std::move( value ) );
        std::replace( value.begin( ), value.end( ), '\\', '/' );
        const std::size_t separator = value.find_last_of( '/' );
        if ( separator != std::string::npos )
            value.erase( 0, separator + 1 );
        const std::size_t suffix = value.rfind( ".wem" );
        if ( suffix != std::string::npos && suffix + 4 == value.size( ) )
            value.erase( suffix );
        return value;
    }

    static void RememberConfiguredVoiceDuration(
        const std::string & voiceId, std::int32_t durationMs ) {
        const std::string key = CanonicalVoiceDurationKey( voiceId );
        if ( key.empty( ) || durationMs <= 0 )
            return;
        AcquireSRWLockExclusive( &g_lifecycleLock );
        g_configuredDurationMsByVoiceId [ key ] = durationMs;
        ReleaseSRWLockExclusive( &g_lifecycleLock );
    }

    static void RememberGlobalVoiceDuration(
        const std::string & voiceId, std::int32_t durationMs ) {
        const std::string key = CanonicalVoiceDurationKey( voiceId );
        if ( key.empty( ) || durationMs <= 0 )
            return;
        AcquireSRWLockExclusive( &g_lifecycleLock );
        g_globalDurationMsByVoiceId [ key ] = durationMs;
        ReleaseSRWLockExclusive( &g_lifecycleLock );
    }

    static std::int32_t FindConfiguredVoiceDuration(
        const std::string & voiceId ) {
        const std::string key = CanonicalVoiceDurationKey( voiceId );
        if ( key.empty( ) )
            return -1;
        std::int32_t durationMs = -1;
        AcquireSRWLockShared( &g_lifecycleLock );
        const auto found = g_configuredDurationMsByVoiceId.find( key );
        if ( found != g_configuredDurationMsByVoiceId.end( ) )
            durationMs = found->second;
        ReleaseSRWLockShared( &g_lifecycleLock );
        return durationMs;
    }

    static std::int32_t FindGlobalVoiceDuration(
        const std::string & voiceId ) {
        const std::string key = CanonicalVoiceDurationKey( voiceId );
        if ( key.empty( ) )
            return -1;
        std::int32_t durationMs = -1;
        AcquireSRWLockShared( &g_lifecycleLock );
        const auto found = g_globalDurationMsByVoiceId.find( key );
        if ( found != g_globalDurationMsByVoiceId.end( ) )
            durationMs = found->second;
        ReleaseSRWLockShared( &g_lifecycleLock );
        return durationMs;
    }

    static bool ReadConfigBool(
        const std::string & configPath, const char * key, bool fallback ) {
        char value [ 32 ] = { 0 };
        GetPrivateProfileStringA(
            "EFStartChange", key, fallback ? "true" : "false",
            value, static_cast< DWORD >( sizeof( value ) ), configPath.c_str( ) );
        const std::string normalized = Normalize( value );
        if ( normalized == "true" || normalized == "1" ||
            normalized == "yes" || normalized == "on" )
            return true;
        if ( normalized == "false" || normalized == "0" ||
            normalized == "no" || normalized == "off" )
            return false;
        return fallback;
    }

    static std::string ReadConfigRules( const std::string & configPath ) {
        char value [ 16384 ] = { 0 };
        GetPrivateProfileStringA(
            "EFStartChange", "voice_language_rules", "", value,
            static_cast< DWORD >( sizeof( value ) ), configPath.c_str( ) );
        return value;
    }

    static bool ParseLanguage( const std::string & source, int & language ) {
        const std::string value = Normalize( source );
        if ( value == "followglobal" || value == "global" ||
            value == "gamedefault" || value == "default" ) {
            language = FOLLOW_GLOBAL_LANGUAGE;
            return true;
        }
        if ( value == "chinese" || value == "cn" || value == "zh" ||
            value == "zh-cn" ) {
            language = 0;
            return true;
        }
        if ( value == "english" || value == "en" ) {
            language = 1;
            return true;
        }
        if ( value == "japanese" || value == "jp" || value == "ja" ) {
            language = 2;
            return true;
        }
        if ( value == "korean" || value == "kr" || value == "ko" ) {
            language = 3;
            return true;
        }
        return false;
    }

    static const char * LanguageName( int language ) {
        switch ( language ) {
        case 0: return "Chinese";
        case 1: return "English";
        case 2: return "Japanese";
        case 3: return "Korean";
        case FOLLOW_GLOBAL_LANGUAGE: return "FollowGlobal";
        default: return "Unknown";
        }
    }

    static const char * VoiceLanguagePathName( int language ) {
        switch ( language ) {
        case 0: return "chinese";
        case 1: return "english";
        case 2: return "japanese";
        case 3: return "korean";
        default: return nullptr;
        }
    }

    static bool BuildVoiceReplacementSource(
        const std::string & source, int targetLanguage,
        std::string & replacement ) {
        replacement.clear( );
        const char * targetName = VoiceLanguagePathName( targetLanguage );
        if ( !targetName || source.rfind( "voice/", 0 ) != 0 )
            return false;

        const std::size_t languageStart = std::strlen( "voice/" );
        const std::size_t languageEnd = source.find( '/', languageStart );
        if ( languageEnd == std::string::npos || languageEnd + 1 >= source.size( ) )
            return false;

        // Only redirect known localized voice roots. This avoids touching
        // unrelated external sources that share the same Wwise submit path.
        const std::string suffix = source.substr( languageEnd + 1 );
        const bool isCharacterVoice =
            suffix.find( "characters/" ) != std::string::npos;
        const bool isNarrativeVoice =
            suffix.find( "narrating/" ) != std::string::npos;
        if ( !isCharacterVoice && !isNarrativeVoice )
            return false;

        replacement = "voice/";
        replacement += targetName;
        replacement.push_back( '/' );
        replacement += suffix;
        return true;
    }

    static bool IsNarrativeVoiceSource( const std::string & source ) {
        return source.rfind( "voice/", 0 ) == 0 &&
            source.find( "/narrating/" ) != std::string::npos;
    }

    static std::string ExtractVoiceLineId( const std::string & source ) {
        if ( source.empty( ) )
            return { };
        const std::size_t separator = source.find_last_of( "/\\" );
        std::string lineId = separator == std::string::npos
            ? source : source.substr( separator + 1 );
        constexpr const char * extension = ".wem";
        if ( lineId.size( ) >= std::strlen( extension ) &&
            lineId.compare(
                lineId.size( ) - std::strlen( extension ),
                std::strlen( extension ), extension ) == 0 ) {
            lineId.erase( lineId.size( ) - std::strlen( extension ) );
        }
        return Normalize( std::move( lineId ) );
    }

    static void ClearPendingLipRoute( ThreadRoutingContext * context ) {
        if ( context )
            context->pendingLipRoute = PendingLipRoute { };
    }

    static bool ArmPendingLipRoute(
        int targetLanguage, const std::string & matchedIdentity,
        const std::string & source ) {
        ThreadRoutingContext * context = GetThreadRoutingContext( );
        const std::string lineId = ExtractVoiceLineId( source );
        if ( !context || lineId.empty( ) || targetLanguage < 0 ||
            targetLanguage > 3 ) {
            ClearPendingLipRoute( context );
            return false;
        }

        PendingLipRoute pending;
        pending.armed = true;
        pending.target = targetLanguage;
        pending.generation = g_configGeneration.load(
            std::memory_order_acquire );
        strncpy_s( pending.lineId, lineId.c_str( ), _TRUNCATE );
        strncpy_s(
            pending.matchedIdentity, matchedIdentity.c_str( ), _TRUNCATE );
        context->pendingLipRoute = pending;
        if ( g_lipArmLogs.fetch_add(
            1, std::memory_order_relaxed ) < LIP_ROUTE_LOG_LIMIT ) {
            Log( "[lip-route] armed line=" + lineId +
                " matched=" + ( matchedIdentity.empty( )
                    ? std::string( "<empty>" ) : matchedIdentity ) +
                " requested=" + LanguageName( targetLanguage ) +
                "(" + std::to_string( targetLanguage ) + ")" );
        }
        return true;
    }

    static void * CreateManagedString( const std::string & value ) {
        if ( !g_il2cppStringNew || value.empty( ) )
            return nullptr;
        __try {
            return g_il2cppStringNew( value.c_str( ) );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return nullptr;
        }
    }

    static std::uint32_t WwiseStringId( const std::string & value ) {
        std::uint32_t hash = 2166136261u;
        for ( unsigned char ch : value ) {
            const unsigned char normalized = static_cast< unsigned char >(
                std::tolower( ch ) );
            hash = hash * 16777619u;
            hash ^= normalized;
        }
        return hash;
    }

    static bool HasSuffix(
        const std::string & value, const char * suffix ) {
        const std::size_t suffixLength = std::strlen( suffix );
        return value.size( ) >= suffixLength &&
            value.compare(
                value.size( ) - suffixLength,
            suffixLength, suffix ) == 0;
    }

#pragma pack( push, 1 )
    struct VoiceRuntimeMapHeader {
        char magic [ 8 ];
        std::uint16_t version;
        std::uint16_t headerSize;
        std::uint32_t totalSize;
        std::uint32_t languageCount;
        std::uint32_t packageCount;
        std::uint32_t characterCount;
        std::uint32_t voiceCount;
        std::uint32_t routeCount;
        std::uint32_t mediaCount;
        std::uint32_t nativeSlotCount;
        std::uint32_t stringSize;
        std::uint32_t packageOffset;
        std::uint32_t characterOffset;
        std::uint32_t voiceOffset;
        std::uint32_t routeOffset;
        std::uint32_t mediaOffset;
        std::uint32_t nativeSlotOffset;
        std::uint32_t stringOffset;
        std::uint8_t audioDialogSha256 [ 32 ];
    };

    struct VoiceRuntimePackageRow {
        std::uint32_t sourceOffset;
        std::uint16_t sourceLength;
        std::uint8_t language;
        std::uint8_t reserved;
        std::uint64_t fileSize;
        std::uint32_t headerSize;
        std::uint8_t headerSha256 [ 32 ];
    };

    struct VoiceRuntimeCharacterRow {
        std::uint32_t idOffset;
        std::uint16_t idLength;
        std::uint16_t reserved;
    };

    struct VoiceRuntimeVoiceRow {
        std::uint32_t eventId;
        std::uint32_t nameOffset;
        std::uint16_t nameLength;
        std::uint8_t characterIndex;
        std::uint8_t codec;
        std::uint32_t firstRoute;
    };

    struct VoiceRuntimeRouteRow {
        std::uint32_t firstMedia;
        std::uint16_t mediaCount;
        std::uint16_t reserved;
    };

    struct VoiceRuntimeNativeSlotRow {
        std::uint32_t mediaIds [ 4 ];
        std::uint8_t characterIndex;
        std::uint8_t reserved [ 3 ];
    };
#pragma pack( pop )

    static_assert( sizeof( VoiceRuntimeMapHeader ) == 108 );
    static_assert( sizeof( VoiceRuntimePackageRow ) == 52 );
    static_assert( sizeof( VoiceRuntimeCharacterRow ) == 8 );
    static_assert( sizeof( VoiceRuntimeVoiceRow ) == 16 );
    static_assert( sizeof( VoiceRuntimeRouteRow ) == 8 );
    static_assert( sizeof( VoiceRuntimeNativeSlotRow ) == 20 );

    static constexpr char VOICE_RUNTIME_MAP_MAGIC [ 8 ] = {
        'E', 'F', 'V', 'R', 'M', 'A', 'P', '\0'
    };
    static constexpr std::uint16_t VOICE_RUNTIME_MAP_VERSION = 2;
    static constexpr std::uint32_t VOICE_RUNTIME_LANGUAGE_COUNT = 4;

    static const VoiceRuntimeMapHeader * RuntimeMapHeader( ) {
        if ( GeneratedVoiceRuntimeMap::kSize <
            sizeof( VoiceRuntimeMapHeader ) )
            return nullptr;
        return reinterpret_cast< const VoiceRuntimeMapHeader * >(
            GeneratedVoiceRuntimeMap::kData );
    }

    static bool RuntimeMapRangeValid(
        std::uint32_t offset, std::uint64_t size ) {
        return offset <= GeneratedVoiceRuntimeMap::kSize &&
            size <= GeneratedVoiceRuntimeMap::kSize - offset;
    }

    static bool ValidateRuntimeMap( ) {
        const VoiceRuntimeMapHeader * header = RuntimeMapHeader( );
        if ( !header || std::memcmp(
                header->magic, VOICE_RUNTIME_MAP_MAGIC,
                sizeof( VOICE_RUNTIME_MAP_MAGIC ) ) != 0 ||
            header->version != VOICE_RUNTIME_MAP_VERSION ||
            header->headerSize != sizeof( VoiceRuntimeMapHeader ) ||
            header->totalSize != GeneratedVoiceRuntimeMap::kSize ||
            header->languageCount != VOICE_RUNTIME_LANGUAGE_COUNT ||
            header->packageCount != VOICE_RUNTIME_LANGUAGE_COUNT )
            return false;

        const std::uint64_t packageBytes =
            static_cast< std::uint64_t >( header->packageCount ) *
            sizeof( VoiceRuntimePackageRow );
        const std::uint64_t characterBytes =
            static_cast< std::uint64_t >( header->characterCount ) *
            sizeof( VoiceRuntimeCharacterRow );
        const std::uint64_t voiceBytes =
            static_cast< std::uint64_t >( header->voiceCount ) *
            sizeof( VoiceRuntimeVoiceRow );
        const std::uint64_t routeBytes =
            static_cast< std::uint64_t >( header->routeCount ) *
            sizeof( VoiceRuntimeRouteRow );
        const std::uint64_t mediaBytes =
            static_cast< std::uint64_t >( header->mediaCount ) *
            sizeof( std::uint32_t );
        const std::uint64_t nativeSlotBytes =
            static_cast< std::uint64_t >( header->nativeSlotCount ) *
            sizeof( VoiceRuntimeNativeSlotRow );
        return RuntimeMapRangeValid( header->packageOffset, packageBytes ) &&
            RuntimeMapRangeValid( header->characterOffset, characterBytes ) &&
            RuntimeMapRangeValid( header->voiceOffset, voiceBytes ) &&
            RuntimeMapRangeValid( header->routeOffset, routeBytes ) &&
            RuntimeMapRangeValid( header->mediaOffset, mediaBytes ) &&
            RuntimeMapRangeValid(
                header->nativeSlotOffset, nativeSlotBytes ) &&
            RuntimeMapRangeValid( header->stringOffset, header->stringSize ) &&
            header->packageOffset == sizeof( VoiceRuntimeMapHeader ) &&
            header->characterOffset == header->packageOffset + packageBytes &&
            header->voiceOffset == header->characterOffset + characterBytes &&
            header->routeOffset == header->voiceOffset + voiceBytes &&
            header->mediaOffset == header->routeOffset + routeBytes &&
            header->nativeSlotOffset == header->mediaOffset + mediaBytes &&
            header->stringOffset ==
                header->nativeSlotOffset + nativeSlotBytes &&
            header->stringOffset + header->stringSize == header->totalSize &&
            header->routeCount == header->voiceCount * header->languageCount;
    }

    template< typename T >
    static const T * RuntimeMapRows( std::uint32_t offset ) {
        return reinterpret_cast< const T * >(
            GeneratedVoiceRuntimeMap::kData + offset );
    }

    static bool RuntimeMapStringEquals(
        std::uint32_t offset, std::uint16_t length,
        const std::string & value ) {
        const VoiceRuntimeMapHeader * header = RuntimeMapHeader( );
        return header && value.size( ) == length &&
            offset <= header->stringSize &&
            length <= header->stringSize - offset &&
            std::memcmp(
                GeneratedVoiceRuntimeMap::kData + header->stringOffset + offset,
                value.data( ), length ) == 0;
    }

    static std::string RuntimeMapString(
        std::uint32_t offset, std::uint16_t length ) {
        const VoiceRuntimeMapHeader * header = RuntimeMapHeader( );
        if ( !header || offset > header->stringSize ||
            length > header->stringSize - offset )
            return { };
        return std::string(
            reinterpret_cast< const char * >(
                GeneratedVoiceRuntimeMap::kData +
                header->stringOffset + offset ), length );
    }

    static const VoiceRuntimeVoiceRow * FindRuntimeVoice(
        std::uint32_t eventId, const std::string & voiceId ) {
        const VoiceRuntimeMapHeader * header = RuntimeMapHeader( );
        if ( !header || !header->voiceCount )
            return nullptr;
        const VoiceRuntimeVoiceRow * voices =
            RuntimeMapRows< VoiceRuntimeVoiceRow >( header->voiceOffset );
        std::size_t first = 0;
        std::size_t last = header->voiceCount;
        while ( first < last ) {
            const std::size_t middle = first + ( last - first ) / 2;
            if ( voices [ middle ].eventId < eventId )
                first = middle + 1;
            else
                last = middle;
        }
        for ( std::size_t index = first;
            index < header->voiceCount &&
            voices [ index ].eventId == eventId; ++index ) {
            if ( RuntimeMapStringEquals(
                    voices [ index ].nameOffset,
                    voices [ index ].nameLength, voiceId ) )
                return &voices [ index ];
        }
        return nullptr;
    }

    static bool RuntimeVoiceMedia(
        const VoiceRuntimeVoiceRow & voice, int language,
        const std::uint32_t *& mediaIds, std::size_t & mediaCount,
        bool * nativeEligible = nullptr ) {
        mediaIds = nullptr;
        mediaCount = 0;
        if ( nativeEligible )
            *nativeEligible = false;
        const VoiceRuntimeMapHeader * header = RuntimeMapHeader( );
        if ( !header || language < 0 ||
            language >= static_cast< int >( header->languageCount ) ||
            voice.firstRoute + static_cast< std::uint32_t >( language ) >=
                header->routeCount )
            return false;
        const VoiceRuntimeRouteRow * routes =
            RuntimeMapRows< VoiceRuntimeRouteRow >( header->routeOffset );
        const VoiceRuntimeRouteRow & route =
            routes [ voice.firstRoute + language ];
        if ( !route.mediaCount || route.firstMedia > header->mediaCount ||
            route.mediaCount > header->mediaCount - route.firstMedia )
            return false;
        mediaIds = RuntimeMapRows< std::uint32_t >(
            header->mediaOffset ) + route.firstMedia;
        mediaCount = route.mediaCount;
        if ( nativeEligible )
            *nativeEligible = ( route.reserved & 1u ) != 0;
        return true;
    }

    static bool ResolveRuntimeCharacterIndex(
        const std::string & configuredIdentity, std::uint8_t & index ) {
        const VoiceRuntimeMapHeader * header = RuntimeMapHeader( );
        if ( !header || configuredIdentity.empty( ) ||
            configuredIdentity == "*" )
            return false;
        const VoiceRuntimeCharacterRow * characters =
            RuntimeMapRows< VoiceRuntimeCharacterRow >(
                header->characterOffset );
        for ( std::uint32_t candidate = 0;
            candidate < header->characterCount; ++candidate ) {
            const VoiceRuntimeCharacterRow & row = characters [ candidate ];
            const std::string characterId = RuntimeMapString(
                row.idOffset, row.idLength );
            if ( characterId == configuredIdentity ) {
                index = static_cast< std::uint8_t >( candidate );
                return true;
            }
            if ( characterId.rfind( "chr_", 0 ) == 0 ) {
                const std::size_t suffix = characterId.find( '_', 4 );
                if ( suffix != std::string::npos &&
                    characterId.substr( suffix + 1 ) == configuredIdentity ) {
                    index = static_cast< std::uint8_t >( candidate );
                    return true;
                }
            }
        }
        return false;
    }

    static bool RuntimeVoiceMatchesConfiguredRule(
        const VoiceRuntimeVoiceRow & voice, int targetLanguage ) {
        const VoiceRuntimeMapHeader * header = RuntimeMapHeader( );
        if ( !header || voice.characterIndex >= header->characterCount )
            return false;
        const VoiceRuntimeCharacterRow * characters =
            RuntimeMapRows< VoiceRuntimeCharacterRow >(
                header->characterOffset );
        const VoiceRuntimeCharacterRow & character =
            characters [ voice.characterIndex ];
        const std::string characterId = RuntimeMapString(
            character.idOffset, character.idLength );
        int configured = NO_DEFAULT_LANGUAGE;
        AcquireSRWLockShared( &g_rulesLock );
        auto found = g_rules.find( characterId );
        if ( found == g_rules.end( ) && characterId.rfind( "chr_", 0 ) == 0 ) {
            const std::size_t suffix = characterId.find( '_', 4 );
            if ( suffix != std::string::npos )
                found = g_rules.find( characterId.substr( suffix + 1 ) );
        }
        configured = found != g_rules.end( )
            ? found->second : g_defaultLanguage;
        ReleaseSRWLockShared( &g_rulesLock );
        return configured == targetLanguage;
    }

    static bool ResolvePackagedMedia(
        const VoiceRequestDiagnosticContext & request,
        std::uint32_t handleId, std::uint32_t & eventId,
        std::uint32_t & mediaId, std::uint32_t & codec,
        std::string & failure ) {
        eventId = 0;
        mediaId = 0;
        codec = request.codec;
        failure.clear( );

        const std::string voiceId = Normalize( request.data );
        if ( request.target < 0 || request.target > 3 || voiceId.empty( ) ) {
            failure = "unsupported-language-or-voice-id";
            return false;
        }
        eventId = WwiseStringId( voiceId );
        const VoiceRuntimeVoiceRow * voice =
            FindRuntimeVoice( eventId, voiceId );
        if ( !voice ) {
            failure = "media-map-miss";
            return false;
        }
        if ( !RuntimeVoiceMatchesConfiguredRule( *voice, request.target ) ) {
            failure = "voice-rule-mismatch";
            return false;
        }
        if ( codec != voice->codec ) {
            failure = "unexpected-codec-" + std::to_string( codec ) +
                "-expected-" + std::to_string( voice->codec );
            return false;
        }
        const std::uint32_t * candidates = nullptr;
        std::size_t candidateCount = 0;
        if ( !RuntimeVoiceMedia(
                *voice, request.target, candidates, candidateCount ) ) {
            failure = "language-route-miss";
            return false;
        }
        mediaId = candidates [ ( handleId ? handleId : eventId ) %
            candidateCount ];
        return mediaId != 0;
    }

    static bool NativeRouteEligible(
        const VoiceRequestDiagnosticContext & request ) {
        if ( !g_nativeMediaRouteReady.load( std::memory_order_acquire ) ||
            request.target < 0 || request.target > 3 )
            return false;
        const std::string voiceId = Normalize( request.data );
        if ( voiceId.empty( ) )
            return false;
        const VoiceRuntimeVoiceRow * voice = FindRuntimeVoice(
            WwiseStringId( voiceId ), voiceId );
        if ( !voice || !RuntimeVoiceMatchesConfiguredRule(
                *voice, request.target ) )
            return false;
        const std::uint32_t * mediaIds = nullptr;
        std::size_t mediaCount = 0;
        bool nativeEligible = false;
        return RuntimeVoiceMedia(
            *voice, request.target, mediaIds, mediaCount, &nativeEligible ) &&
            nativeEligible;
    }

    struct PckMediaEntry {
        std::uint64_t offset = 0;
        std::uint32_t size = 0;
    };

    struct ExclusiveSrwLockGuard {
        explicit ExclusiveSrwLockGuard( SRWLOCK & lock ) : lock_( &lock ) {
            AcquireSRWLockExclusive( lock_ );
        }

        ~ExclusiveSrwLockGuard( ) {
            ReleaseSRWLockExclusive( lock_ );
        }

        ExclusiveSrwLockGuard( const ExclusiveSrwLockGuard & ) = delete;
        ExclusiveSrwLockGuard & operator=(
            const ExclusiveSrwLockGuard & ) = delete;

    private:
        SRWLOCK * lock_;
    };

    static std::uint32_t ReadU32(
        const std::vector< std::uint8_t > & data, std::size_t offset,
        bool & valid ) {
        if ( offset > data.size( ) || data.size( ) - offset < 4 ) {
            valid = false;
            return 0;
        }
        std::uint32_t value = 0;
        std::memcpy( &value, data.data( ) + offset, sizeof( value ) );
        return value;
    }

    static std::uint64_t ReadU64(
        const std::vector< std::uint8_t > & data, std::size_t offset,
        bool & valid ) {
        if ( offset > data.size( ) || data.size( ) - offset < 8 ) {
            valid = false;
            return 0;
        }
        std::uint64_t value = 0;
        std::memcpy( &value, data.data( ) + offset, sizeof( value ) );
        return value;
    }

    static std::uint32_t DeriveVfsKey( std::uint32_t seed ) {
        constexpr std::uint32_t multiplier = 81861667u;
        constexpr std::uint32_t xorValue = 0x9C5A0B29u;
        std::uint32_t key = ( ( seed & 0xFFu ) ^ xorValue ) * multiplier;
        key = ( key ^ ( ( seed >> 8 ) & 0xFFu ) ) * multiplier;
        key = ( key ^ ( ( seed >> 16 ) & 0xFFu ) ) * multiplier;
        key = ( key ^ ( ( seed >> 24 ) & 0xFFu ) ) * multiplier;
        return key;
    }

    static void DecryptVfsBytes(
        std::uint8_t * data, std::size_t length, std::uint32_t seed,
        std::uint64_t dataOffset = 0 ) {
        if ( !data || !length )
            return;
        std::uint32_t keyIndex = seed +
            static_cast< std::uint32_t >( dataOffset >> 2 );
        std::size_t position = 0;
        const std::size_t alignment = static_cast< std::size_t >(
            dataOffset & 3u );
        if ( alignment ) {
            const std::uint32_t key = DeriveVfsKey( keyIndex );
            const std::size_t count = ( std::min )(
                4u - alignment, length );
            for ( std::size_t index = 0; index < count; ++index )
                data [ position++ ] ^= static_cast< std::uint8_t >(
                    key >> ( ( alignment + index ) * 8 ) );
            ++keyIndex;
        }
        while ( length - position >= 4 ) {
            std::uint32_t value = 0;
            std::memcpy( &value, data + position, sizeof( value ) );
            value ^= DeriveVfsKey( keyIndex++ );
            std::memcpy( data + position, &value, sizeof( value ) );
            position += 4;
        }
        if ( position < length ) {
            const std::uint32_t key = DeriveVfsKey( keyIndex );
            for ( std::size_t index = 0; position < length;
                ++index, ++position ) {
                data [ position ] ^= static_cast< std::uint8_t >(
                    key >> ( index * 8 ) );
            }
        }
    }

    static bool ReadFileRange(
        HANDLE file, std::uint64_t offset, void * target,
        std::uint32_t size ) {
        if ( file == INVALID_HANDLE_VALUE || !target || !size )
            return false;
        LARGE_INTEGER position { };
        position.QuadPart = static_cast< LONGLONG >( offset );
        if ( !SetFilePointerEx( file, position, nullptr, FILE_BEGIN ) )
            return false;
        DWORD read = 0;
        return ReadFile( file, target, size, &read, nullptr ) && read == size;
    }

    static bool ParsePckMediaSector(
        const std::vector< std::uint8_t > & header, std::size_t start,
        std::uint32_t size, bool external,
        const std::unordered_set< std::uint32_t > & targets,
        std::unordered_map< std::uint32_t, PckMediaEntry > & entries ) {
        bool valid = true;
        if ( size < 4 || start > header.size( ) ||
            header.size( ) - start < size )
            return false;
        const std::uint32_t count = ReadU32( header, start, valid );
        if ( !valid || !count )
            return valid;
        const std::uint32_t entrySize = ( size - 4 ) / count;
        if ( entrySize < 20 || 4ull +
            static_cast< std::uint64_t >( entrySize ) * count > size )
            return false;
        const bool alternate = entrySize == 0x18;
        for ( std::uint32_t index = 0; index < count; ++index ) {
            const std::size_t entry = start + 4ull +
                static_cast< std::size_t >( entrySize ) * index;
            const std::uint32_t mediaId = ReadU32( header, entry, valid );
            std::size_t cursor = entry + 4;
            if ( alternate && external )
                cursor += 4;
            const std::uint32_t blockSize = ReadU32(
                header, cursor, valid );
            cursor += 4;
            const std::uint64_t mediaSize = alternate && !external
                ? ReadU64( header, cursor, valid )
                : ReadU32( header, cursor, valid );
            cursor += alternate && !external ? 8 : 4;
            const std::uint32_t blockOffset = ReadU32(
                header, cursor, valid );
            if ( !valid )
                return false;
            if ( targets.find( mediaId ) == targets.end( ) )
                continue;
            const std::uint64_t offset = blockSize
                ? static_cast< std::uint64_t >( blockOffset ) * blockSize
                : blockOffset;
            if ( !mediaSize || mediaSize > UINT32_MAX )
                return false;
            entries [ mediaId ] = {
                offset, static_cast< std::uint32_t >( mediaSize )
            };
        }
        return true;
    }

    static std::string GameRootDirectory( ) {
        std::array< char, 32768 > path { };
        const DWORD length = GetModuleFileNameA(
            nullptr, path.data( ), static_cast< DWORD >( path.size( ) ) );
        if ( !length || length >= path.size( ) )
            return { };
        std::string result( path.data( ), length );
        const std::size_t separator = result.find_last_of( "\\/" );
        return separator == std::string::npos
            ? std::string( ) : result.substr( 0, separator );
    }

    static std::string NormalizeWindowsPath( std::string path ) {
        std::replace( path.begin( ), path.end( ), '/', '\\' );
        return path;
    }

    static bool TryGetFileSize(
        const std::string & path, std::uint64_t & size ) {
        size = 0;
        WIN32_FILE_ATTRIBUTE_DATA data { };
        if ( !GetFileAttributesExA(
                path.c_str( ), GetFileExInfoStandard, &data ) ||
            ( data.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY ) != 0 )
            return false;
        ULARGE_INTEGER value { };
        value.LowPart = data.nFileSizeLow;
        value.HighPart = data.nFileSizeHigh;
        size = value.QuadPart;
        return true;
    }

    static bool Sha256(
        const std::uint8_t * data, std::size_t size,
        std::array< std::uint8_t, 32 > & digest ) {
        BCRYPT_ALG_HANDLE algorithm = nullptr;
        BCRYPT_HASH_HANDLE hash = nullptr;
        DWORD objectSize = 0;
        DWORD resultSize = 0;
        std::vector< std::uint8_t > object;
        bool success = BCryptOpenAlgorithmProvider(
                &algorithm, BCRYPT_SHA256_ALGORITHM, nullptr, 0 ) >= 0 &&
            BCryptGetProperty(
                algorithm, BCRYPT_OBJECT_LENGTH,
                reinterpret_cast< PUCHAR >( &objectSize ), sizeof( objectSize ),
                &resultSize, 0 ) >= 0 && objectSize != 0;
        if ( success ) {
            object.resize( objectSize );
            success = BCryptCreateHash(
                    algorithm, &hash, object.data( ), objectSize,
                    nullptr, 0, 0 ) >= 0 &&
                size <= ULONG_MAX && BCryptHashData(
                    hash, const_cast< PUCHAR >( data ),
                    static_cast< ULONG >( size ), 0 ) >= 0 &&
                BCryptFinishHash(
                    hash, digest.data( ),
                    static_cast< ULONG >( digest.size( ) ), 0 ) >= 0;
        }
        if ( hash )
            BCryptDestroyHash( hash );
        if ( algorithm )
            BCryptCloseAlgorithmProvider( algorithm, 0 );
        return success;
    }

    static const VoiceRuntimePackageRow * RuntimePackageForLanguage(
        int language ) {
        const VoiceRuntimeMapHeader * header = RuntimeMapHeader( );
        if ( !header || language < 0 ||
            language >= static_cast< int >( header->languageCount ) )
            return nullptr;
        const VoiceRuntimePackageRow * packages =
            RuntimeMapRows< VoiceRuntimePackageRow >( header->packageOffset );
        for ( std::uint32_t index = 0;
            index < header->packageCount; ++index ) {
            if ( packages [ index ].language == language )
                return &packages [ index ];
        }
        return nullptr;
    }

    static std::string FindVoicePackage(
        const VoiceRuntimePackageRow & package ) {
        const std::string root = GameRootDirectory( );
        if ( root.empty( ) )
            return { };
        std::string relative = NormalizeWindowsPath( RuntimeMapString(
            package.sourceOffset, package.sourceLength ) );
        if ( relative.empty( ) )
            return { };
        std::string preferred = root + "\\" + relative;
        std::uint64_t size = 0;
        if ( TryGetFileSize( preferred, size ) && size == package.fileSize )
            return preferred;

        constexpr const char * persistent = "\\Persistent\\";
        constexpr const char * streaming = "\\StreamingAssets\\";
        const std::size_t marker = preferred.find( persistent );
        if ( marker != std::string::npos ) {
            std::string fallback = preferred;
            fallback.replace( marker, std::strlen( persistent ), streaming );
            if ( TryGetFileSize( fallback, size ) && size == package.fileSize )
                return fallback;
        }
        return { };
    }

    static bool CollectConfiguredMedia(
        std::array< std::unordered_set< std::uint32_t >, 4 > & targets,
        std::size_t & selectedCharacters ) {
        selectedCharacters = 0;
        if ( !ValidateRuntimeMap( ) )
            return false;
        VoiceRuleMap rules;
        int defaultLanguage = NO_DEFAULT_LANGUAGE;
        bool enabled = false;
        AcquireSRWLockShared( &g_rulesLock );
        rules = g_rules;
        defaultLanguage = g_defaultLanguage;
        enabled = g_enabled.load( std::memory_order_acquire );
        ReleaseSRWLockShared( &g_rulesLock );
        if ( !enabled )
            return true;

        const VoiceRuntimeMapHeader * header = RuntimeMapHeader( );
        const VoiceRuntimeCharacterRow * characters =
            RuntimeMapRows< VoiceRuntimeCharacterRow >(
                header->characterOffset );
        std::vector< int > languageByCharacter(
            header->characterCount, defaultLanguage );
        for ( std::uint32_t index = 0;
            index < header->characterCount; ++index ) {
            const std::string characterId = RuntimeMapString(
                characters [ index ].idOffset,
                characters [ index ].idLength );
            auto found = rules.find( characterId );
            if ( found == rules.end( ) && characterId.rfind( "chr_", 0 ) == 0 ) {
                const std::size_t suffix = characterId.find( '_', 4 );
                if ( suffix != std::string::npos )
                    found = rules.find( characterId.substr( suffix + 1 ) );
            }
            if ( found != rules.end( ) )
                languageByCharacter [ index ] = found->second;
        }

        for ( const auto & [ identity, language ] : rules ) {
            std::uint8_t index = 0;
            if ( ResolveRuntimeCharacterIndex( identity, index ) )
                languageByCharacter [ index ] = language;
        }
        selectedCharacters = static_cast< std::size_t >( std::count_if(
            languageByCharacter.begin( ), languageByCharacter.end( ),
            [ ] ( int language ) { return language >= 0 && language <= 3; } ) );

        const VoiceRuntimeVoiceRow * voices =
            RuntimeMapRows< VoiceRuntimeVoiceRow >( header->voiceOffset );
        for ( std::uint32_t index = 0; index < header->voiceCount; ++index ) {
            const VoiceRuntimeVoiceRow & voice = voices [ index ];
            if ( voice.characterIndex >= languageByCharacter.size( ) )
                return false;
            const int language = languageByCharacter [ voice.characterIndex ];
            const std::uint32_t * mediaIds = nullptr;
            std::size_t mediaCount = 0;
            if ( language < 0 || language > 3 ||
                !RuntimeVoiceMedia(
                    voice, language, mediaIds, mediaCount ) )
                continue;
            targets [ language ].insert( mediaIds, mediaIds + mediaCount );
        }
        return true;
    }

    static bool PrewarmLanguageMedia(
        int language,
        const std::unordered_set< std::uint32_t > & requestedTargets,
        std::size_t & loadedCount, std::size_t & loadedBytes ) {
        const ExclusiveSrwLockGuard prewarmGuard( g_packagedPrewarmLock );
        loadedCount = 0;
        loadedBytes = 0;
        std::unordered_set< std::uint32_t > targets;
        AcquireSRWLockShared( &g_residentWemsLock );
        for ( const std::uint32_t mediaId : requestedTargets ) {
            if ( g_residentWems.find( mediaId ) == g_residentWems.end( ) )
                targets.insert( mediaId );
        }
        ReleaseSRWLockShared( &g_residentWemsLock );
        if ( targets.empty( ) )
            return true;

        const VoiceRuntimePackageRow * package =
            RuntimePackageForLanguage( language );
        if ( !package )
            return false;
        const std::string packagePath = FindVoicePackage( *package );
        if ( packagePath.empty( ) ) {
            Log( "[voice-memory] " + std::string( LanguageName( language ) ) +
                " PCK not found or size mismatch; v6 fallback retained" );
            return false;
        }
        HANDLE file = CreateFileA(
            packagePath.c_str( ), GENERIC_READ,
            FILE_SHARE_READ | FILE_SHARE_WRITE | FILE_SHARE_DELETE,
            nullptr, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, nullptr );
        if ( file == INVALID_HANDLE_VALUE ) {
            Log( "[voice-memory] PCK open failed language=" +
                std::string( LanguageName( language ) ) + " error=" +
                std::to_string( GetLastError( ) ) + "; v6 fallback retained" );
            return false;
        }

        std::array< std::uint8_t, 12 > prefix { };
        bool success = ReadFileRange(
            file, 0, prefix.data( ), static_cast< std::uint32_t >(
                prefix.size( ) ) );
        std::uint32_t headerSize = 0;
        if ( success )
            std::memcpy( &headerSize, prefix.data( ) + 4, 4 );
        if ( !success || std::memcmp( prefix.data( ), ":)xD", 4 ) != 0 ||
            headerSize < 16 || headerSize > 16u * 1024u * 1024u ) {
            CloseHandle( file );
            Log( "[voice-memory] unsupported PCK header language=" +
                std::string( LanguageName( language ) ) +
                "; v6 fallback retained" );
            return false;
        }

        std::vector< std::uint8_t > header( headerSize + 8ull );
        success = ReadFileRange(
            file, 0, header.data( ), static_cast< std::uint32_t >(
                header.size( ) ) );
        if ( success ) {
            DecryptVfsBytes(
                header.data( ) + 12, headerSize - 4, headerSize );
            std::memcpy( header.data( ), "AKPK", 4 );
            const std::uint32_t flags = 1;
            std::memcpy( header.data( ) + 8, &flags, sizeof( flags ) );
        }
        std::array< std::uint8_t, 32 > headerDigest { };
        if ( !success || header.size( ) != package->headerSize ||
            !Sha256( header.data( ), header.size( ), headerDigest ) ||
            std::memcmp(
                headerDigest.data( ), package->headerSha256,
                headerDigest.size( ) ) != 0 ) {
            CloseHandle( file );
            Log( "[voice-memory] PCK identity mismatch language=" +
                std::string( LanguageName( language ) ) +
                "; v6 fallback retained" );
            return false;
        }

        bool valid = success;
        std::size_t cursor = 4;
        const std::uint32_t parsedHeaderSize = ReadU32(
            header, cursor, valid );
        cursor += 8;
        const std::uint32_t languagesSize = ReadU32(
            header, cursor, valid );
        cursor += 4;
        const std::uint32_t banksSize = ReadU32( header, cursor, valid );
        cursor += 4;
        const std::uint32_t soundsSize = ReadU32( header, cursor, valid );
        cursor += 4;
        std::uint32_t externalsSize = 0;
        if ( valid && static_cast< std::uint64_t >( languagesSize ) +
            banksSize + soundsSize + 0x10ull < parsedHeaderSize ) {
            externalsSize = ReadU32( header, cursor, valid );
            cursor += 4;
        }
        cursor += languagesSize + banksSize;
        std::unordered_map< std::uint32_t, PckMediaEntry > entries;
        success = valid && ParsePckMediaSector(
            header, cursor, soundsSize, false, targets, entries );
        cursor += soundsSize;
        if ( success && externalsSize )
            success = ParsePckMediaSector(
                header, cursor, externalsSize, true, targets, entries );
        if ( !success || entries.size( ) != targets.size( ) ) {
            CloseHandle( file );
            Log( "[voice-memory] PCK index incomplete language=" +
                std::string( LanguageName( language ) ) + " targets=" +
                std::to_string( entries.size( ) ) + "/" +
                std::to_string( targets.size( ) ) +
                "; v6 fallback retained" );
            return false;
        }

        std::unordered_map< std::uint32_t, ResidentWem > staged;
        for ( const std::uint32_t mediaId : targets ) {
            const PckMediaEntry entry = entries [ mediaId ];
            void * memory = VirtualAlloc(
                nullptr, entry.size, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE );
            if ( !memory ) {
                success = false;
                break;
            }
            if ( !ReadFileRange(
                    file, entry.offset, memory, entry.size ) ) {
                VirtualFree( memory, 0, MEM_RELEASE );
                success = false;
                break;
            }
            std::uint8_t * bytes = static_cast< std::uint8_t * >( memory );
            if ( std::memcmp( bytes, "RIFF", 4 ) != 0 &&
                std::memcmp( bytes, "RIFX", 4 ) != 0 )
                DecryptVfsBytes( bytes, entry.size, mediaId );
            if ( std::memcmp( bytes, "RIFF", 4 ) != 0 &&
                std::memcmp( bytes, "RIFX", 4 ) != 0 ) {
                VirtualFree( memory, 0, MEM_RELEASE );
                success = false;
                break;
            }
            DWORD oldProtect = 0;
            VirtualProtect( memory, entry.size, PAGE_READONLY, &oldProtect );
            staged [ mediaId ] = { memory, entry.size };
            loadedBytes += entry.size;
        }
        CloseHandle( file );
        if ( !success || staged.size( ) != targets.size( ) ) {
            for ( const auto & [ mediaId, wem ] : staged )
                VirtualFree( wem.memory, 0, MEM_RELEASE );
            loadedBytes = 0;
            Log( "[voice-memory] WEM preload failed language=" +
                std::string( LanguageName( language ) ) + " loaded=" +
                std::to_string( staged.size( ) ) + "/" +
                std::to_string( targets.size( ) ) +
                "; v6 fallback retained" );
            return false;
        }
        AcquireSRWLockExclusive( &g_residentWemsLock );
        for ( const auto & [ mediaId, wem ] : staged )
            g_residentWems.emplace( mediaId, wem );
        ReleaseSRWLockExclusive( &g_residentWemsLock );
        loadedCount = staged.size( );
        Log( "[voice-memory] language preload ready language=" +
            std::string( LanguageName( language ) ) + " media=" +
            std::to_string( loadedCount ) + " bytes=" +
            std::to_string( loadedBytes ) + " source=" + packagePath );
        return true;
    }

    static bool PrewarmPackagedMedia( ) {
        std::array< std::unordered_set< std::uint32_t >, 4 > targets;
        std::size_t selectedCharacters = 0;
        if ( !CollectConfiguredMedia( targets, selectedCharacters ) ) {
            g_packagedMemoryReady.store( false, std::memory_order_release );
            Log( "[voice-memory] embedded runtime map invalid; v6 fallback retained" );
            return false;
        }
        bool success = true;
        std::size_t requestedMedia = 0;
        std::size_t newlyLoaded = 0;
        std::size_t newlyLoadedBytes = 0;
        for ( int language = 0; language < 4; ++language ) {
            requestedMedia += targets [ language ].size( );
            std::size_t loaded = 0;
            std::size_t bytes = 0;
            if ( !targets [ language ].empty( ) && !PrewarmLanguageMedia(
                    language, targets [ language ], loaded, bytes ) )
                success = false;
            newlyLoaded += loaded;
            newlyLoadedBytes += bytes;
        }
        bool allRequestedReady = true;
        AcquireSRWLockShared( &g_residentWemsLock );
        for ( int language = 0; language < 4 && allRequestedReady; ++language ) {
            for ( const std::uint32_t mediaId : targets [ language ] ) {
                if ( g_residentWems.find( mediaId ) == g_residentWems.end( ) ) {
                    allRequestedReady = false;
                    break;
                }
            }
        }
        ReleaseSRWLockShared( &g_residentWemsLock );
        const bool routingReady = allRequestedReady &&
            ( requestedMedia == 0 || g_packagedMediaSetterHookCreated );
        g_packagedMemoryReady.store(
            routingReady, std::memory_order_release );
        Log( "[voice-memory] selective preload complete characters=" +
            std::to_string( selectedCharacters ) + " requestedMedia=" +
            std::to_string( requestedMedia ) + " newlyLoaded=" +
            std::to_string( newlyLoaded ) + " bytes=" +
            std::to_string( newlyLoadedBytes ) + " map=" +
            GeneratedVoiceRuntimeMap::kSha256 + " result=" +
            ( routingReady && success ? "ready" : "partial-v6-fallback" ) );
        return routingReady && success;
    }

    static std::vector< int > ConfiguredLanguageByCharacter( ) {
        const VoiceRuntimeMapHeader * header = RuntimeMapHeader( );
        if ( !header )
            return { };
        VoiceRuleMap rules;
        int defaultLanguage = NO_DEFAULT_LANGUAGE;
        bool enabled = false;
        AcquireSRWLockShared( &g_rulesLock );
        rules = g_rules;
        defaultLanguage = g_defaultLanguage;
        enabled = g_enabled.load( std::memory_order_acquire );
        ReleaseSRWLockShared( &g_rulesLock );
        std::vector< int > result(
            header->characterCount,
            enabled ? defaultLanguage : FOLLOW_GLOBAL_LANGUAGE );
        if ( !enabled )
            return result;
        const VoiceRuntimeCharacterRow * characters =
            RuntimeMapRows< VoiceRuntimeCharacterRow >(
                header->characterOffset );
        for ( std::uint32_t index = 0;
            index < header->characterCount; ++index ) {
            const std::string characterId = RuntimeMapString(
                characters [ index ].idOffset, characters [ index ].idLength );
            auto found = rules.find( characterId );
            if ( found == rules.end( ) && characterId.rfind( "chr_", 0 ) == 0 ) {
                const std::size_t suffix = characterId.find( '_', 4 );
                if ( suffix != std::string::npos )
                    found = rules.find( characterId.substr( suffix + 1 ) );
            }
            if ( found != rules.end( ) )
                result [ index ] = found->second;
        }
        for ( const auto & [ identity, language ] : rules ) {
            std::uint8_t index = 0;
            if ( ResolveRuntimeCharacterIndex( identity, index ) )
                result [ index ] = language;
        }
        return result;
    }

    static bool BuildNativeMediaOverrides(
        std::unordered_map< std::uint32_t, std::uint32_t > & overrides ) {
        overrides.clear( );
        if ( !ValidateRuntimeMap( ) )
            return false;
        const VoiceRuntimeMapHeader * header = RuntimeMapHeader( );
        const std::vector< int > languageByCharacter =
            ConfiguredLanguageByCharacter( );
        if ( languageByCharacter.size( ) != header->characterCount )
            return false;
        const VoiceRuntimeNativeSlotRow * slots =
            RuntimeMapRows< VoiceRuntimeNativeSlotRow >(
                header->nativeSlotOffset );
        for ( std::uint32_t index = 0;
            index < header->nativeSlotCount; ++index ) {
            const VoiceRuntimeNativeSlotRow & slot = slots [ index ];
            if ( slot.characterIndex >= languageByCharacter.size( ) )
                return false;
            const int targetLanguage =
                languageByCharacter [ slot.characterIndex ];
            if ( targetLanguage < 0 || targetLanguage > 3 )
                continue;
            const std::uint32_t targetMedia = slot.mediaIds [ targetLanguage ];
            for ( int sourceLanguage = 0; sourceLanguage < 4; ++sourceLanguage ) {
                const std::uint32_t sourceMedia =
                    slot.mediaIds [ sourceLanguage ];
                const auto [ found, inserted ] = overrides.emplace(
                    sourceMedia, targetMedia );
                if ( !inserted && found->second != targetMedia )
                    return false;
            }
        }
        return true;
    }

    static int CallSetMedia(
        const std::vector< NativeAkSourceSettings > & settings ) {
        if ( !g_setMedia || settings.empty( ) ||
            settings.size( ) > UINT32_MAX )
            return settings.empty( ) ? 1 : 0;
        int result = 0;
        __try {
            result = g_setMedia(
                const_cast< NativeAkSourceSettings * >( settings.data( ) ),
                static_cast< std::uint32_t >( settings.size( ) ), nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            result = 0;
        }
        return result;
    }

    static int CallUnsetMedia(
        const std::vector< NativeAkSourceSettings > & settings ) {
        if ( !g_unsetMedia || settings.empty( ) ||
            settings.size( ) > UINT32_MAX )
            return settings.empty( ) ? 1 : 0;
        int result = 0;
        __try {
            result = g_unsetMedia(
                const_cast< NativeAkSourceSettings * >( settings.data( ) ),
                static_cast< std::uint32_t >( settings.size( ) ), nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            result = 0;
        }
        return result;
    }

    static bool CallSetMediaIndividually(
        const std::vector< NativeAkSourceSettings > & settings ) {
        bool success = true;
        for ( const NativeAkSourceSettings & setting : settings ) {
            const std::vector< NativeAkSourceSettings > single { setting };
            if ( CallSetMedia( single ) != 1 )
                success = false;
        }
        return success;
    }

    static bool CallUnsetMediaIndividually(
        const std::vector< NativeAkSourceSettings > & settings,
        std::unordered_set< std::uint32_t > * failedSources = nullptr ) {
        if ( failedSources )
            failedSources->clear( );
        bool success = true;
        for ( const NativeAkSourceSettings & setting : settings ) {
            const std::vector< NativeAkSourceSettings > single { setting };
            if ( CallUnsetMedia( single ) != 1 ) {
                success = false;
                if ( failedSources )
                    failedSources->insert( setting.sourceId );
            }
        }
        return success;
    }

    static bool ApplyNativeMediaRoutes( ) {
        const ExclusiveSrwLockGuard guard( g_nativeMediaRouteLock );
        g_nativeMediaRouteReady.store( false, std::memory_order_release );
        if ( !g_setMedia || !g_unsetMedia )
            return false;
        if ( !g_uncertainNativeMediaSources.empty( ) ) {
            Log( "[voice-native] unresolved SetMedia state; "
                "v9 fallback retained until cleanup succeeds" );
            return false;
        }

        std::unordered_map< std::uint32_t, std::uint32_t > desired;
        if ( !BuildNativeMediaOverrides( desired ) ) {
            Log( "[voice-native] runtime route map invalid; v9 fallback retained" );
            return false;
        }
        std::vector< NativeAkSourceSettings > toUnset;
        std::vector< NativeAkSourceSettings > toSet;
        std::vector< NativeAkSourceSettings > toRestore;
        bool residentReady = true;
        AcquireSRWLockShared( &g_residentWemsLock );
        for ( const auto & [ sourceMedia, targetMedia ] :
            g_registeredNativeMedia ) {
            const auto wanted = desired.find( sourceMedia );
            if ( wanted == desired.end( ) || wanted->second != targetMedia ) {
                toUnset.push_back( { sourceMedia, 0, nullptr, 0, 0 } );
                const auto resident = g_residentWems.find( targetMedia );
                if ( resident == g_residentWems.end( ) ||
                    !resident->second.memory || !resident->second.size ) {
                    residentReady = false;
                    break;
                }
                toRestore.push_back( {
                    sourceMedia, 0, resident->second.memory,
                    resident->second.size, 0 } );
            }
        }
        for ( const auto & [ sourceMedia, targetMedia ] : desired ) {
            if ( !residentReady )
                break;
            const auto current = g_registeredNativeMedia.find( sourceMedia );
            if ( current != g_registeredNativeMedia.end( ) &&
                current->second == targetMedia )
                continue;
            const auto resident = g_residentWems.find( targetMedia );
            if ( resident == g_residentWems.end( ) ||
                !resident->second.memory || !resident->second.size ) {
                residentReady = false;
                break;
            }
            toSet.push_back( {
                sourceMedia, 0, resident->second.memory,
                resident->second.size, 0 } );
        }
        ReleaseSRWLockShared( &g_residentWemsLock );
        if ( !residentReady ) {
            Log( "[voice-native] target Media is not resident; v9 fallback retained" );
            return false;
        }

        const int unsetResult = CallUnsetMedia( toUnset );
        if ( unsetResult != 1 ) {
            for ( const NativeAkSourceSettings & setting : toUnset )
                g_uncertainNativeMediaSources.insert( setting.sourceId );
            const bool cleanupReady = CallUnsetMediaIndividually( toUnset );
            const bool restoreReady = CallSetMediaIndividually( toRestore );
            if ( cleanupReady && restoreReady ) {
                for ( const NativeAkSourceSettings & setting : toUnset )
                    g_uncertainNativeMediaSources.erase( setting.sourceId );
                g_nativeMediaUnloadSafe.store(
                    true, std::memory_order_release );
            }
            else {
                g_nativeMediaUnloadSafe.store(
                    false, std::memory_order_release );
            }
            Log( "[voice-native] UnsetMedia failed result=" +
                std::to_string( unsetResult ) + " cleanup=" +
                ( cleanupReady ? "ready" : "uncertain" ) + " restore=" +
                ( restoreReady ? "ready" : "uncertain" ) +
                "; v9 fallback retained" );
            return false;
        }
        const int setResult = CallSetMedia( toSet );
        if ( setResult != 1 ) {
            for ( const NativeAkSourceSettings & setting : toSet )
                g_uncertainNativeMediaSources.insert( setting.sourceId );
            const bool cleanupReady = CallUnsetMediaIndividually( toSet );
            const bool restoreReady = CallSetMediaIndividually( toRestore );
            if ( cleanupReady && restoreReady ) {
                for ( const NativeAkSourceSettings & setting : toSet )
                    g_uncertainNativeMediaSources.erase( setting.sourceId );
                g_nativeMediaUnloadSafe.store(
                    true, std::memory_order_release );
            }
            else {
                if ( !restoreReady ) {
                    for ( const NativeAkSourceSettings & setting : toUnset )
                        g_uncertainNativeMediaSources.insert(
                            setting.sourceId );
                }
                g_nativeMediaUnloadSafe.store(
                    false, std::memory_order_release );
            }
            Log( "[voice-native] SetMedia failed result=" +
                std::to_string( setResult ) + " cleanup=" +
                ( cleanupReady ? "ready" : "uncertain" ) + " restore=" +
                ( restoreReady ? "ready" : "uncertain" ) +
                "; v9 fallback retained" );
            return false;
        }
        g_registeredNativeMedia = std::move( desired );
        for ( const NativeAkSourceSettings & setting : toUnset )
            g_uncertainNativeMediaSources.erase( setting.sourceId );
        for ( const NativeAkSourceSettings & setting : toSet )
            g_uncertainNativeMediaSources.erase( setting.sourceId );
        g_nativeMediaUnloadSafe.store( true, std::memory_order_release );
        g_nativeMediaRouteReady.store( true, std::memory_order_release );
        Log( "[voice-native] SetMedia routes ready sources=" +
            std::to_string( g_registeredNativeMedia.size( ) ) +
            " changed=" + std::to_string( toSet.size( ) ) +
            " removed=" + std::to_string( toUnset.size( ) ) );
        return true;
    }

    static bool ClearNativeMediaRoutes( ) {
        const ExclusiveSrwLockGuard guard( g_nativeMediaRouteLock );
        g_nativeMediaRouteReady.store( false, std::memory_order_release );
        if ( g_registeredNativeMedia.empty( ) &&
            g_uncertainNativeMediaSources.empty( ) )
            return true;
        std::vector< NativeAkSourceSettings > settings;
        settings.reserve( g_registeredNativeMedia.size( ) +
            g_uncertainNativeMediaSources.size( ) );
        std::unordered_set< std::uint32_t > sources;
        for ( const auto & [ sourceMedia, _targetMedia ] :
            g_registeredNativeMedia )
            sources.insert( sourceMedia );
        sources.insert(
            g_uncertainNativeMediaSources.begin( ),
            g_uncertainNativeMediaSources.end( ) );
        for ( const std::uint32_t sourceMedia : sources )
            settings.push_back( { sourceMedia, 0, nullptr, 0, 0 } );
        std::unordered_set< std::uint32_t > failedSources;
        const bool success = CallUnsetMediaIndividually(
            settings, &failedSources );
        if ( !success ) {
            for ( const std::uint32_t sourceMedia : sources ) {
                if ( failedSources.find( sourceMedia ) ==
                    failedSources.end( ) ) {
                    g_registeredNativeMedia.erase( sourceMedia );
                    g_uncertainNativeMediaSources.erase( sourceMedia );
                }
            }
            g_uncertainNativeMediaSources.insert(
                failedSources.begin( ), failedSources.end( ) );
            g_nativeMediaUnloadSafe.store(
                false, std::memory_order_release );
            Log( "[voice-native] final UnsetMedia failed sources=" +
                std::to_string( failedSources.size( ) ) +
                "; registered memory must remain resident" );
            return false;
        }
        g_registeredNativeMedia.clear( );
        g_uncertainNativeMediaSources.clear( );
        g_nativeMediaUnloadSafe.store( true, std::memory_order_release );
        return true;
    }

    static std::string ConfigDirectory( ) {
        const std::size_t separator = g_configPath.find_last_of( "/\\" );
        if ( separator == std::string::npos )
            return { };
        return g_configPath.substr( 0, separator );
    }

    static bool IsReadableWem( const std::string & path ) {
        HANDLE file = CreateFileA(
            path.c_str( ), GENERIC_READ,
            FILE_SHARE_READ | FILE_SHARE_WRITE | FILE_SHARE_DELETE,
            nullptr, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, nullptr );
        if ( file == INVALID_HANDLE_VALUE )
            return false;

        std::uint8_t signature [ 4 ] = { 0 };
        DWORD read = 0;
        const bool readable = ReadFile(
            file, signature, sizeof( signature ), &read, nullptr ) &&
            read == sizeof( signature );
        CloseHandle( file );
        return readable && signature [ 0 ] == 'R' &&
            signature [ 1 ] == 'I' && signature [ 2 ] == 'F' &&
            ( signature [ 3 ] == 'F' || signature [ 3 ] == 'X' );
    }

    static bool ResolveCachedBankWem(
        const VoiceRequestDiagnosticContext & request,
        std::uint32_t handleId, std::uint32_t & eventId,
        std::string & path, std::string & failure ) {
        eventId = 0;
        path.clear( );
        failure.clear( );

        const char * language = VoiceLanguagePathName( request.target );
        const std::string voiceId = Normalize( request.data );
        if ( !language || voiceId.empty( ) || !HasSuffix( voiceId, "_sv" ) ) {
            failure = "unsupported-bank-id";
            return false;
        }

        const std::string root = ConfigDirectory( );
        if ( root.empty( ) ) {
            failure = "config-directory-unavailable";
            return false;
        }

        eventId = WwiseStringId( voiceId );
        const std::string directory = root + "\\voice-cache\\" +
            language + "\\" + std::to_string( eventId );
        const std::string pattern = directory + "\\*.wem";
        WIN32_FIND_DATAA findData { };
        HANDLE search = FindFirstFileA( pattern.c_str( ), &findData );
        if ( search == INVALID_HANDLE_VALUE ) {
            failure = "cache-miss";
            return false;
        }

        std::vector< std::string > candidates;
        do {
            if ( ( findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY ) != 0 )
                continue;
            const std::string candidate = directory + "\\" +
                findData.cFileName;
            if ( IsReadableWem( candidate ) )
                candidates.emplace_back( candidate );
        } while ( FindNextFileA( search, &findData ) );
        FindClose( search );

        if ( candidates.empty( ) ) {
            failure = "cache-has-no-readable-wem";
            return false;
        }

        std::sort( candidates.begin( ), candidates.end( ) );
        path = candidates [ ( handleId ? handleId : eventId ) %
            candidates.size( ) ];
        return true;
    }

    static bool TryPlayExternalVoice(
        void * externalSource, void * wwiseEvent,
        std::uint64_t audioObjectId, std::uint32_t handleId,
        std::uint32_t codec, std::uint32_t * playingId ) {
        if ( !g_voicePlayerPlayExternal || !externalSource || !wwiseEvent ||
            !playingId )
            return false;
        __try {
            *playingId = g_voicePlayerPlayExternal(
                externalSource, wwiseEvent, audioObjectId,
                handleId, codec, nullptr );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            *playingId = 0;
            return false;
        }
    }

    static bool PackagedMediaLogAllowed( ) {
        return g_packagedMediaDiagnosticLogs.fetch_add(
            1, std::memory_order_relaxed ) <
            PACKAGED_MEDIA_DIAGNOSTIC_LOG_LIMIT;
    }

    static void * FindImageByName( const char * requestedName ) {
        if ( !requestedName || !api::initialized || !api::get_domain ||
            !api::get_assemblies || !api::assembly_get_image ||
            !api::image_get_name )
            return nullptr;
        void * domain = api::get_domain( );
        if ( !domain )
            return nullptr;
        std::size_t count = 0;
        void ** assemblies = api::get_assemblies( domain, &count );
        for ( std::size_t index = 0; assemblies && index < count; ++index ) {
            void * image = api::assembly_get_image( assemblies [ index ] );
            const char * name = image ? api::image_get_name( image ) : nullptr;
            if ( name && _stricmp( name, requestedName ) == 0 )
                return image;
        }
        return nullptr;
    }

    static bool TryPlayMemoryExternalVoice(
        PackagedMediaSetterContext & route, void * placeholder,
        void * wwiseEvent, std::uint64_t audioObjectId,
        std::uint32_t handleId, std::uint32_t codec,
        std::uint32_t & playingId ) {
        playingId = 0;
        ThreadRoutingContext * threadContext = GetThreadRoutingContext( );
        if ( !threadContext )
            return false;

        PackagedMediaSetterContext previousSetter =
            threadContext->packagedMediaSetter;
        const bool previousBankEventRouted = threadContext->bankEventRouted;
        bool callCompleted = false;
        __try {
            threadContext->packagedMediaSetter = route;
            threadContext->bankEventRouted = true;
            callCompleted = TryPlayExternalVoice(
                placeholder, wwiseEvent, audioObjectId,
                handleId, codec, &playingId );
            route = threadContext->packagedMediaSetter;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            callCompleted = false;
            playingId = 0;
        }
        threadContext->packagedMediaSetter = previousSetter;
        threadContext->bankEventRouted = previousBankEventRouted;
        return callCompleted;
    }

    static void AddRule(
        VoiceRuleMap & rules, int & defaultLanguage,
        const std::string & sourceKey, int language ) {
        const std::string key = Normalize( sourceKey );
        if ( key.empty( ) )
            return;
        if ( key == "*" ) {
            defaultLanguage = language;
            return;
        }

        rules [ key ] = language;
        if ( key.rfind( "chr_", 0 ) == 0 ) {
            const std::size_t suffixStart = key.find( '_', 4 );
            if ( suffixStart != std::string::npos && suffixStart + 1 < key.size( ) )
                rules.emplace( key.substr( suffixStart + 1 ), language );
        }
    }

    static void ParseRules(
        const std::string & source, VoiceRuleMap & rules,
        int & defaultLanguage ) {
        std::string flattened = source;
        std::replace_if(
            flattened.begin( ), flattened.end( ),
            [ ] ( char ch ) { return ch == ';' || ch == '\r' || ch == '\n'; },
            ',' );

        std::size_t start = 0;
        while ( start <= flattened.size( ) ) {
            const std::size_t end = flattened.find( ',', start );
            std::string entry = Trim( flattened.substr(
                start, end == std::string::npos
                    ? std::string::npos
                    : end - start ) );
            start = end == std::string::npos ? flattened.size( ) + 1 : end + 1;
            if ( entry.empty( ) )
                continue;

            const std::size_t equals = entry.find( '=' );
            const std::size_t colon = entry.find( ':' );
            std::size_t separator = std::string::npos;
            if ( equals != std::string::npos && colon != std::string::npos )
                separator = equals < colon ? equals : colon;
            else if ( equals != std::string::npos )
                separator = equals;
            else
                separator = colon;

            if ( separator == std::string::npos ) {
                Log( "[voice-lang] ignored malformed rule: " + entry );
                continue;
            }

            const std::string key = Trim( entry.substr( 0, separator ) );
            const std::string value = Trim( entry.substr( separator + 1 ) );
            int language = FOLLOW_GLOBAL_LANGUAGE;
            if ( key.empty( ) || !ParseLanguage( value, language ) ) {
                Log( "[voice-lang] ignored invalid rule: " + entry );
                continue;
            }
            AddRule( rules, defaultLanguage, key, language );
        }
    }

    static ConfigFileStamp ReadConfigFileStamp(
        const std::string & configPath ) {
        ConfigFileStamp stamp;
        WIN32_FILE_ATTRIBUTE_DATA data { };
        if ( configPath.empty( ) || !GetFileAttributesExA(
            configPath.c_str( ), GetFileExInfoStandard, &data ) ||
            ( data.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY ) != 0 ) {
            return stamp;
        }

        ULARGE_INTEGER writeTime { };
        writeTime.LowPart = data.ftLastWriteTime.dwLowDateTime;
        writeTime.HighPart = data.ftLastWriteTime.dwHighDateTime;
        ULARGE_INTEGER size { };
        size.LowPart = data.nFileSizeLow;
        size.HighPart = data.nFileSizeHigh;
        stamp.exists = true;
        stamp.writeTime = writeTime.QuadPart;
        stamp.size = size.QuadPart;
        return stamp;
    }

    static bool ConfigFileStampsEqual(
        const ConfigFileStamp & left, const ConfigFileStamp & right ) {
        return left.exists == right.exists &&
            left.writeTime == right.writeTime && left.size == right.size;
    }

    static VoiceConfigurationSnapshot ReadVoiceConfiguration(
        const std::string & configPath ) {
        VoiceConfigurationSnapshot snapshot;
        snapshot.enabled = ReadConfigBool(
            configPath, "voice_router_enabled", false );
        snapshot.replaceNarrativeVoice = ReadConfigBool(
            configPath, "replace_narrative_voice", true );
        snapshot.diagnostics = ReadConfigBool(
            configPath, "voice_diagnostics", VOICE_DIAGNOSTICS_DEFAULT );
        ParseRules(
            ReadConfigRules( configPath ), snapshot.rules,
            snapshot.defaultLanguage );
        return snapshot;
    }

    static void ApplyVoiceConfiguration(
        VoiceConfigurationSnapshot && snapshot, bool hotReload ) {
        const bool tlsReady = EnsureRoutingTlsIndex( );
        const bool hookHostAvailable = g_hookHostAvailable.load(
            std::memory_order_acquire );
        const bool enabled =
            snapshot.enabled && tlsReady && hookHostAvailable;
        const bool replaceNarrativeVoice = snapshot.replaceNarrativeVoice;
        const bool diagnostics = snapshot.diagnostics;
        const std::size_t ruleCount = snapshot.rules.size( );
        const int defaultLanguage = snapshot.defaultLanguage;

        g_enabled.store( false, std::memory_order_release );
        AcquireSRWLockExclusive( &g_rulesLock );
        g_rules.swap( snapshot.rules );
        g_defaultLanguage = defaultLanguage;
        g_replaceNarrativeVoice.store(
            replaceNarrativeVoice, std::memory_order_release );
        g_diagnosticsEnabled.store( diagnostics, std::memory_order_release );
        const std::uint64_t generation = g_configGeneration.fetch_add(
            1, std::memory_order_acq_rel ) + 1;
        g_enabled.store( enabled, std::memory_order_release );
        ReleaseSRWLockExclusive( &g_rulesLock );

        if ( hotReload ) {
            g_selectWwiseEventMatchLogs.store( 0, std::memory_order_release );
            g_submitExternalMatchLogs.store( 0, std::memory_order_release );
            g_narrativeSkipLogs.store( 0, std::memory_order_release );
            g_lipRouteLogs.store( 0, std::memory_order_release );
            g_lipArmLogs.store( 0, std::memory_order_release );
            g_lipLanguageOverrideHits.store( 0, std::memory_order_release );
            g_durationLanguageOverrideHits.store(
                0, std::memory_order_release );
            g_packagedMediaDiagnosticLogs.store(
                0, std::memory_order_release );
        }

        Log( std::string( hotReload
                ? "[voice-hot-reload] applied"
                : "[voice-lang] configured" ) +
            " generation=" + std::to_string( generation ) +
            " enabled=" + ( enabled ? "true" : "false" ) +
            " narrative=" +
                ( replaceNarrativeVoice ? "true" : "false" ) +
            " diagnostics=" + ( diagnostics ? "true" : "false" ) +
            " win32Tls=" + ( tlsReady ? "ready" : "failed" ) +
            " hookHost=" +
                ( hookHostAvailable ? "ready" : "unavailable" ) +
            " rules=" + std::to_string( ruleCount ) +
            " default=" + LanguageName( defaultLanguage ) );
        if ( hotReload && g_packagedMediaSetterHookCreated ) {
            const bool prewarmed = PrewarmPackagedMedia( );
            const bool applied = prewarmed && g_setMedia && g_unsetMedia &&
                ApplyNativeMediaRoutes( );
            if ( !applied && !ClearNativeMediaRoutes( ) )
                Log( "[voice-native] hot-reload cleanup failed; "
                    "module unload is disabled for this process" );
        }
    }

    static void PollConfigurationReload( ) {
        if ( g_configPath.empty( ) )
            return;

        const ConfigFileStamp current = ReadConfigFileStamp( g_configPath );
        if ( !current.exists ) {
            g_configReloadPending = false;
            return;
        }
        if ( ConfigFileStampsEqual( current, g_appliedConfigStamp ) ) {
            g_configReloadPending = false;
            return;
        }

        const std::uint64_t now = GetTickCount64( );
        if ( !g_configReloadPending ||
            !ConfigFileStampsEqual( current, g_pendingConfigStamp ) ) {
            g_pendingConfigStamp = current;
            g_configReloadPending = true;
            g_configReloadDetectedAt = now;
            return;
        }
        if ( now - g_configReloadDetectedAt < 250 )
            return;

        VoiceConfigurationSnapshot snapshot =
            ReadVoiceConfiguration( g_configPath );
        ApplyVoiceConfiguration( std::move( snapshot ), true );
        g_appliedConfigStamp = current;
        g_configReloadPending = false;
    }

    static bool TryCopyBytes(
        const void * source, std::uint8_t * destination,
        std::size_t length ) {
        if ( !source || !destination || length == 0 )
            return false;

        __try {
            std::memcpy( destination, source, length );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }
    }

    static std::string FormatBytes(
        const std::uint8_t * bytes, std::size_t length ) {
        static constexpr char HEX [ ] = "0123456789ABCDEF";
        std::string result;
        result.reserve( length * 3 );
        for ( std::size_t index = 0; index < length; ++index ) {
            if ( index != 0 )
                result.push_back( ' ' );
            result.push_back( HEX [ bytes [ index ] >> 4 ] );
            result.push_back( HEX [ bytes [ index ] & 0x0f ] );
        }
        return result;
    }

    static bool TryCopyManagedStringObject(
        void * stringObject, char * destination, std::size_t capacity ) {
        if ( !stringObject || !destination || capacity < 2 )
            return false;

        __try {
            auto * stringBytes = static_cast< std::uint8_t * >( stringObject );
            const std::int32_t length = *reinterpret_cast< std::int32_t * >(
                stringBytes + 0x10 );
            if ( length <= 0 || static_cast< std::size_t >( length ) >= capacity )
                return false;

            auto * characters = reinterpret_cast< const char16_t * >(
                stringBytes + 0x14 );
            for ( std::int32_t index = 0; index < length; ++index ) {
                if ( characters [ index ] > 0x7f )
                    return false;
                destination [ index ] = static_cast< char >( characters [ index ] );
            }
            destination [ length ] = '\0';
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            destination [ 0 ] = '\0';
            return false;
        }
    }

    static bool TryCopyManagedStringField(
        void * voiceContext, std::size_t fieldOffset,
        char * destination, std::size_t capacity ) {
        if ( !voiceContext || !destination || capacity < 2 )
            return false;

        __try {
            auto * contextBytes = static_cast< std::uint8_t * >( voiceContext );
            void * stringObject = *reinterpret_cast< void ** >(
                contextBytes + fieldOffset );
            return TryCopyManagedStringObject(
                stringObject, destination, capacity );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            destination [ 0 ] = '\0';
            return false;
        }
    }

    static void * TryDereferenceVoiceContext( void * voiceContextReference ) {
        if ( !voiceContextReference )
            return nullptr;
        __try {
            return *reinterpret_cast< void ** >( voiceContextReference );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return nullptr;
        }
    }

    static bool TryReadCachedLanguage( int * language ) {
        if ( !g_getCurrentLanguage || !language )
            return false;
        __try {
            *language = g_getCurrentLanguage( nullptr );
            return *language >= 0 && *language <= 3;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }
    }

    static bool TryReadWwiseLanguage( std::string & language ) {
        language.clear( );
        if ( !g_getWwiseCurrentLanguage )
            return false;

        __try {
            void * managedLanguage = g_getWwiseCurrentLanguage( nullptr );
            char buffer [ 64 ] = { 0 };
            if ( !TryCopyManagedStringObject(
                managedLanguage, buffer, sizeof( buffer ) ) )
                return false;
            language = Normalize( buffer );
            return !language.empty( );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            language.clear( );
            return false;
        }
    }

    static bool TrySetLanguage( int language ) {
        if ( !g_setLanguage || language < 0 || language > 3 )
            return false;
        __try {
            g_setLanguage( language, nullptr );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }
    }

    static bool DiagnosticLogAllowed( ) {
        if ( !g_diagnosticsEnabled.load( std::memory_order_acquire ) )
            return false;
        const std::uint32_t count = g_lifecycleLogs.fetch_add(
            1, std::memory_order_relaxed );
        if ( count < VOICE_LIFECYCLE_LOG_LIMIT )
            return true;
        if ( count == VOICE_LIFECYCLE_LOG_LIMIT )
            Log( "[voice-life] diagnostic log limit reached; further lifecycle "
                "records are suppressed" );
        return false;
    }

    static std::string FormatCallerAddress( void * caller ) {
        const uintptr_t address = reinterpret_cast< uintptr_t >( caller );
        char buffer [ 96 ] = { 0 };
        if ( g_gameAssemblyBase && address >= g_gameAssemblyBase &&
            address - g_gameAssemblyBase < g_gameAssemblySize ) {
            sprintf_s( buffer, "GameAssembly+0x%llX",
                static_cast< unsigned long long >(
                    address - g_gameAssemblyBase ) );
        }
        else {
            sprintf_s( buffer, "0x%llX",
                static_cast< unsigned long long >( address ) );
        }
        return buffer;
    }

    static bool TryGetDiagnosticVoiceRequest(
        VoiceRequestDiagnosticContext & request,
        const char *& scope, std::uint64_t & ageMs ) {
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        if ( !threadContext || !threadContext->voiceRequest.matched )
            return false;

        request = threadContext->voiceRequest;
        const std::uint64_t now = GetTickCount64( );
        ageMs = now >= request.observedAtMs
            ? now - request.observedAtMs : 0;
        if ( !request.scopeActive &&
            ageMs > VOICE_REQUEST_DIAGNOSTIC_GRACE_MS )
            return false;
        scope = request.scopeActive ? "active" : "recent";
        return true;
    }

    static std::string ActionName( std::int32_t action ) {
        switch ( action ) {
        case 0: return "Stop";
        case 1: return "Pause";
        case 2: return "Resume";
        case 3: return "Break";
        case 4: return "ReleaseEnvelope";
        default: return "Unknown";
        }
    }

    static std::uint32_t ReadPayloadU32(
        void * payload, std::size_t offset ) {
        if ( !payload )
            return 0;
        __try {
            return *reinterpret_cast< std::uint32_t * >(
                static_cast< std::uint8_t * >( payload ) + offset );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return 0;
        }
    }

    static std::uint64_t ReadPayloadU64(
        void * payload, std::size_t offset ) {
        if ( !payload )
            return 0;
        __try {
            return *reinterpret_cast< std::uint64_t * >(
                static_cast< std::uint8_t * >( payload ) + offset );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return 0;
        }
    }

    static std::uint32_t TryResolveRealPlayingId(
        std::uint32_t playingId ) {
        if ( !playingId || !g_tryGetRealPlayingId )
            return playingId;
        std::uint32_t realPlayingId = 0;
        __try {
            if ( g_tryGetRealPlayingId(
                playingId, &realPlayingId, nullptr ) && realPlayingId )
                return realPlayingId;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
        }
        return playingId;
    }

    static std::string ReadSourcePosition(
        std::uint32_t playingId ) {
        if ( !playingId || !g_getSourcePlayPosition )
            return "unavailable";
        std::int32_t positionMs = -1;
        std::int32_t result = -1;
        __try {
            result = g_getSourcePlayPosition(
                playingId, &positionMs, true, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return "exception";
        }
        return "result=" + std::to_string( result ) +
            " positionMs=" + std::to_string( positionMs );
    }

    static void RecordPlaybackStart(
        std::uint32_t playingId, std::uint64_t audioObjectId,
        std::uint32_t externalCookie, std::uint32_t callbackType,
        std::uint32_t codec, const std::string & event,
        const std::string & originalSource, const std::string & submittedSource,
        bool matched, bool replaced, bool bypassedStringHook,
        int targetLanguage, const std::string & matchedIdentity ) {
        if ( !g_diagnosticsEnabled.load( std::memory_order_acquire ) ||
            !playingId )
            return;

        VoicePlaybackTrace previous;
        bool hasPrevious = false;
        const std::uint64_t now = GetTickCount64( );
        const std::int32_t globalDurationMs = FindGlobalVoiceDuration(
            originalSource.empty( ) ? event : originalSource );
        const std::int32_t configuredDurationMs = FindConfiguredVoiceDuration(
            originalSource.empty( ) ? event : originalSource );
        AcquireSRWLockExclusive( &g_lifecycleLock );
        const auto previousId = g_lastPlayingIdByAudioObject.find(
            audioObjectId );
        if ( previousId != g_lastPlayingIdByAudioObject.end( ) ) {
            const auto previousTrace = g_voicePlaybackTraces.find(
                previousId->second );
            if ( previousTrace != g_voicePlaybackTraces.end( ) ) {
                previous = previousTrace->second;
                hasPrevious = true;
            }
        }
        if ( g_voicePlaybackTraces.size( ) >= VOICE_LIFECYCLE_TRACE_LIMIT )
            g_voicePlaybackTraces.erase( g_voicePlaybackTraces.begin( ) );

        VoicePlaybackTrace trace;
        trace.playingId = playingId;
        trace.audioObjectId = audioObjectId;
        trace.externalCookie = externalCookie;
        trace.callbackType = callbackType;
        trace.codec = codec;
        trace.startedAtMs = now;
        trace.matched = matched;
        trace.replaced = replaced;
        trace.bypassedStringHook = bypassedStringHook;
        trace.targetLanguage = targetLanguage;
        trace.globalDurationMs = globalDurationMs;
        trace.configuredDurationMs = configuredDurationMs;
        trace.matchedIdentity = matchedIdentity;
        trace.event = event;
        trace.originalSource = originalSource;
        trace.submittedSource = submittedSource;
        g_voicePlaybackTraces [ playingId ] = trace;
        g_lastPlayingIdByAudioObject [ audioObjectId ] = playingId;
        ReleaseSRWLockExclusive( &g_lifecycleLock );

        if ( !DiagnosticLogAllowed( ) )
            return;
        Log( "[voice-life] start playingId=" + std::to_string( playingId ) +
            " audioObjectId=" + std::to_string( audioObjectId ) +
            " externalCookie=" + std::to_string( externalCookie ) +
            " callbackType=" + std::to_string( callbackType ) +
            " codec=" + std::to_string( codec ) +
            " matched=" + ( matched ? "true" : "false" ) +
            " replaced=" + ( replaced ? "true" : "false" ) +
            " bypassedStringHook=" +
                ( bypassedStringHook ? "true" : "false" ) +
            " target=" + LanguageName( targetLanguage ) +
            " globalDurationMs=" + std::to_string(
                trace.globalDurationMs ) +
            " configuredDurationMs=" + std::to_string(
                trace.configuredDurationMs ) +
            " identity=" + ( matchedIdentity.empty( )
                ? std::string( "<empty>" ) : matchedIdentity ) +
            " event=" + ( event.empty( ) ? std::string( "<empty>" ) : event ) +
            " source=" + ( originalSource.empty( )
                ? std::string( "<empty>" ) : originalSource ) +
            " submitted=" + ( submittedSource.empty( )
                ? std::string( "<empty>" ) : submittedSource ) +
            " sourcePosition=" + ReadSourcePosition( playingId ) );
        if ( hasPrevious && previous.playingId != playingId )
            Log( "[voice-life] audio-object-overlap audioObjectId=" +
                std::to_string( audioObjectId ) +
                " previousPlayingId=" + std::to_string( previous.playingId ) +
                " previousAgeMs=" + std::to_string(
                    now >= previous.startedAtMs
                        ? now - previous.startedAtMs : 0 ) +
                " previousSource=" + ( previous.submittedSource.empty( )
                    ? std::string( "<empty>" ) : previous.submittedSource ) );
    }

    static void RecordPlaybackAction(
        std::int32_t action, std::uint32_t requestedPlayingId,
        std::int32_t durationMs, const char * stage,
        void * caller, bool updateTrace, bool logUntracked,
        std::uint64_t audioObjectId = 0 ) {
        if ( !g_diagnosticsEnabled.load( std::memory_order_acquire ) )
            return;
        const std::uint32_t realPlayingId = TryResolveRealPlayingId(
            requestedPlayingId );
        VoicePlaybackTrace trace;
        bool found = false;
        const char * matchedBy = "none";
        const std::uint64_t now = GetTickCount64( );
        AcquireSRWLockExclusive( &g_lifecycleLock );
        auto it = g_voicePlaybackTraces.find( requestedPlayingId );
        if ( it != g_voicePlaybackTraces.end( ) )
            matchedBy = "requested-playing-id";
        if ( it == g_voicePlaybackTraces.end( ) ) {
            it = g_voicePlaybackTraces.find( realPlayingId );
            if ( it != g_voicePlaybackTraces.end( ) )
                matchedBy = "resolved-playing-id";
        }
        if ( it == g_voicePlaybackTraces.end( ) && audioObjectId ) {
            const auto objectIt = g_lastPlayingIdByAudioObject.find(
                audioObjectId );
            if ( objectIt != g_lastPlayingIdByAudioObject.end( ) ) {
                it = g_voicePlaybackTraces.find( objectIt->second );
                if ( it != g_voicePlaybackTraces.end( ) )
                    matchedBy = "audio-object";
            }
        }
        if ( it != g_voicePlaybackTraces.end( ) ) {
            if ( updateTrace ) {
                it->second.lastActionAtMs = now;
                it->second.lastAction = action;
                it->second.lastActionDurationMs = durationMs;
            }
            trace = it->second;
            found = true;
        }
        ReleaseSRWLockExclusive( &g_lifecycleLock );

        if ( !found && !logUntracked )
            return;
        if ( !DiagnosticLogAllowed( ) )
            return;
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        const bool throughQueue = threadContext &&
            threadContext->consumingActionQueue;
        const std::uint64_t ageMs = found && now >= trace.startedAtMs
            ? now - trace.startedAtMs : 0;
        const std::int64_t ageMinusGlobalMs = found &&
            trace.globalDurationMs > 0
            ? static_cast< std::int64_t >( ageMs ) -
                trace.globalDurationMs
            : 0;
        const std::int64_t ageMinusConfiguredMs = found &&
            trace.configuredDurationMs > 0
            ? static_cast< std::int64_t >( ageMs ) -
                trace.configuredDurationMs
            : 0;
        const bool originalDurationBoundary = found && action == 0 &&
            trace.replaced && trace.globalDurationMs > 0 &&
            trace.configuredDurationMs > 0 &&
            trace.globalDurationMs != trace.configuredDurationMs &&
            ageMinusGlobalMs >= -500 && ageMinusGlobalMs <= 750;
        const std::uint32_t positionPlayingId = found
            ? TryResolveRealPlayingId( trace.playingId ) : realPlayingId;
        Log( "[voice-life] actionStage=" + std::string(
                stage ? stage : "unknown" ) +
            " action=" + ActionName( action ) +
            " actionValue=" + std::to_string( action ) +
            " requestedPlayingId=" + std::to_string( requestedPlayingId ) +
            " realPlayingId=" + std::to_string( realPlayingId ) +
            " durationMs=" + std::to_string( durationMs ) +
            " caller=" + FormatCallerAddress( caller ) +
            " viaQueue=" + ( throughQueue ? "true" : "false" ) +
            ( throughQueue ? " queueFrame=" + std::to_string(
                threadContext->actionQueueFrame ) : std::string( ) ) +
            " tracked=" + ( found ? "true" : "false" ) +
            " matchedBy=" + matchedBy +
            " lookupAudioObjectId=" + std::to_string( audioObjectId ) +
            ( found ? " audioObjectId=" + std::to_string(
                trace.audioObjectId ) +
                " tracePlayingId=" + std::to_string( trace.playingId ) +
                " ageMs=" + std::to_string( ageMs ) +
                " globalDurationMs=" + std::to_string(
                    trace.globalDurationMs ) +
                ( trace.globalDurationMs > 0
                    ? " ageMinusGlobalMs=" + std::to_string(
                        ageMinusGlobalMs )
                    : std::string( ) ) +
                " configuredDurationMs=" + std::to_string(
                    trace.configuredDurationMs ) +
                ( trace.configuredDurationMs > 0
                    ? " ageMinusConfiguredMs=" + std::to_string(
                        ageMinusConfiguredMs )
                    : std::string( ) ) +
                " originalDurationBoundary=" +
                    ( originalDurationBoundary ? "true" : "false" ) +
                " matched=" + ( trace.matched ? "true" : "false" ) +
                " replaced=" + ( trace.replaced ? "true" : "false" ) +
                " identity=" + ( trace.matchedIdentity.empty( )
                    ? std::string( "<empty>" ) : trace.matchedIdentity ) +
                " source=" + ( trace.originalSource.empty( )
                    ? std::string( "<empty>" ) : trace.originalSource ) +
                " submitted=" + ( trace.submittedSource.empty( )
                    ? std::string( "<empty>" ) : trace.submittedSource ) +
                " sourcePosition=" + ReadSourcePosition( positionPlayingId )
                : std::string( ) ) );
        if ( originalDurationBoundary ) {
            Log( "[voice-truncation-candidate] actionStage=" + std::string(
                    stage ? stage : "unknown" ) +
                " caller=" + FormatCallerAddress( caller ) +
                " playingId=" + std::to_string( trace.playingId ) +
                " audioObjectId=" + std::to_string( trace.audioObjectId ) +
                " ageMs=" + std::to_string( ageMs ) +
                " globalDurationMs=" + std::to_string(
                    trace.globalDurationMs ) +
                " ageMinusGlobalMs=" + std::to_string(
                    ageMinusGlobalMs ) +
                " configuredDurationMs=" + std::to_string(
                    trace.configuredDurationMs ) +
                " ageMinusConfiguredMs=" + std::to_string(
                    ageMinusConfiguredMs ) +
                " source=" + ( trace.originalSource.empty( )
                    ? std::string( "<empty>" ) : trace.originalSource ) +
                " submitted=" + ( trace.submittedSource.empty( )
                    ? std::string( "<empty>" ) : trace.submittedSource ) +
                " sourcePosition=" + ReadSourcePosition( positionPlayingId ) );
        }
    }

    static void RecordPlaybackCallback(
        void * payload, std::int32_t callbackType, void * callbackInfo,
        const char * channel, bool logUntracked ) {
        if ( !g_diagnosticsEnabled.load( std::memory_order_acquire ) )
            return;
        const std::uint32_t internalPlayingId = ReadPayloadU32(
            payload, 0x1c );
        const std::uint64_t audioObjectId = ReadPayloadU64(
            payload, 0x10 );
        std::uint32_t callbackPlayingId = 0;
        if ( callbackInfo && g_getCallbackPlayingId ) {
            __try {
                callbackPlayingId = g_getCallbackPlayingId(
                    callbackInfo, nullptr );
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                callbackPlayingId = 0;
            }
        }
        const std::uint32_t realInternalPlayingId = TryResolveRealPlayingId(
            internalPlayingId );
        VoicePlaybackTrace trace;
        bool found = false;
        const std::uint64_t now = GetTickCount64( );
        AcquireSRWLockExclusive( &g_lifecycleLock );
        auto it = g_voicePlaybackTraces.end( );
        const auto tryMatch = [ & ] ( std::uint32_t playingId ) {
            if ( !playingId || it != g_voicePlaybackTraces.end( ) )
                return;
            const auto candidate = g_voicePlaybackTraces.find( playingId );
            if ( candidate == g_voicePlaybackTraces.end( ) )
                return;
            if ( audioObjectId && candidate->second.audioObjectId &&
                audioObjectId != candidate->second.audioObjectId )
                return;
            it = candidate;
        };
        tryMatch( callbackPlayingId );
        tryMatch( internalPlayingId );
        tryMatch( realInternalPlayingId );
        if ( it != g_voicePlaybackTraces.end( ) ) {
            trace = it->second;
            found = true;
            if ( ( callbackType & 1 ) != 0 ) {
                g_voicePlaybackTraces.erase( it );
                const auto objectIt = g_lastPlayingIdByAudioObject.find(
                    trace.audioObjectId );
                if ( objectIt != g_lastPlayingIdByAudioObject.end( ) &&
                    objectIt->second == trace.playingId )
                    g_lastPlayingIdByAudioObject.erase( objectIt );
            }
        }
        ReleaseSRWLockExclusive( &g_lifecycleLock );

        if ( !found && !logUntracked )
            return;
        if ( !DiagnosticLogAllowed( ) )
            return;
        const std::uint64_t ageMs = found && now >= trace.startedAtMs
            ? now - trace.startedAtMs : 0;
        const bool stoppedBeforeCallback = found && trace.lastAction == 0;
        const char * completionKind = ( callbackType & 1 ) == 0
            ? "non-terminal"
            : ( stoppedBeforeCallback ? "end-after-stop" : "natural-end" );
        const std::uint32_t positionPlayingId = callbackPlayingId
            ? callbackPlayingId
            : ( internalPlayingId ? internalPlayingId
                : realInternalPlayingId );
        Log( "[voice-life] callback channel=" + std::string(
                channel ? channel : "unknown" ) +
            " type=" + std::to_string( callbackType ) +
            " endOfEvent=" + ( ( callbackType & 1 ) ? "true" : "false" ) +
            " completionKind=" + completionKind +
            " internalPlayingId=" + std::to_string( internalPlayingId ) +
            " callbackPlayingId=" + std::to_string( callbackPlayingId ) +
            " resolvedPlayingId=" + std::to_string( realInternalPlayingId ) +
            " payloadAudioObjectId=" + std::to_string( audioObjectId ) +
            " tracked=" + ( found ? "true" : "false" ) +
            ( found ? " tracePlayingId=" + std::to_string( trace.playingId ) +
                " traceAudioObjectId=" + std::to_string(
                    trace.audioObjectId ) +
                " ageMs=" + std::to_string( ageMs ) +
                " globalDurationMs=" + std::to_string(
                    trace.globalDurationMs ) +
                " configuredDurationMs=" + std::to_string(
                    trace.configuredDurationMs ) +
                ( trace.configuredDurationMs > 0
                    ? " ageMinusConfiguredMs=" + std::to_string(
                        static_cast< std::int64_t >( ageMs ) -
                        trace.configuredDurationMs )
                    : std::string( ) ) +
                " lastAction=" + ActionName( trace.lastAction ) +
                " matched=" + ( trace.matched ? "true" : "false" ) +
                " replaced=" + ( trace.replaced ? "true" : "false" ) +
                " source=" + ( trace.originalSource.empty( )
                    ? std::string( "<empty>" ) : trace.originalSource ) +
                " submitted=" + ( trace.submittedSource.empty( )
                    ? std::string( "<empty>" ) : trace.submittedSource )
                : std::string( ) ) +
            " sourcePosition=" + ReadSourcePosition( positionPlayingId ) );
    }

    static int __fastcall HookAkSoundEngineLoadFilePackage(
        void * packagePath, std::uint32_t * packageId, void * method ) {
        const int result = g_originalAkSoundEngineLoadFilePackage(
            packagePath, packageId, method );
        if ( g_auxiliaryPackageLoad.load( std::memory_order_acquire ) ) {
            char pathBuffer [ 512 ] = { 0 };
            TryCopyManagedStringObject(
                packagePath, pathBuffer, sizeof( pathBuffer ) );
            const std::uint32_t id = packageId ? *packageId : 0;
            g_auxiliaryPackageLoads.fetch_add( 1, std::memory_order_relaxed );
            Log( "[voice-pck] auxiliary load language=" + std::string(
                    LanguageName( g_auxiliaryPackageLanguage.load(
                        std::memory_order_acquire ) ) ) +
                " result=" + std::to_string( result ) +
                " packageId=" + std::to_string( id ) +
                " path=" + ( pathBuffer [ 0 ]
                    ? std::string( pathBuffer ) : std::string( "<unreadable>" ) ) );
        }
        return result;
    }

    static int __fastcall HookAkSoundEngineUnloadFilePackage(
        std::uint32_t packageId, void * method ) {
        if ( g_auxiliaryPackageLoad.load( std::memory_order_acquire ) ) {
            g_auxiliaryPackageUnloadsSuppressed.fetch_add(
                1, std::memory_order_relaxed );
            Log( "[voice-pck] preserved current-language packageId=" +
                std::to_string( packageId ) +
                " while loading " + LanguageName(
                    g_auxiliaryPackageLanguage.load(
                        std::memory_order_acquire ) ) );
            // AK_Success. The loader may clear its bookkeeping entry while the
            // Wwise file package remains mounted alongside the routed language.
            return 1;
        }
        return g_originalAkSoundEngineUnloadFilePackage( packageId, method );
    }

    static void __fastcall HookAkExternalSourceFileSetter(
        void * self, void * managedFile, void * method ) {
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        PackagedMediaSetterContext * route = threadContext
            ? &threadContext->packagedMediaSetter : nullptr;
        if ( route && route->active ) {
            route->setterObserved = true;
            const bool eligible = g_akExternalSourceMemorySetter &&
                g_akExternalSourceMemorySizeSetter && route->memory &&
                route->memorySize != 0;
            bool routed = false;
            if ( eligible ) {
                __try {
                    g_akExternalSourceMemorySetter(
                        self, reinterpret_cast< std::intptr_t >(
                            route->memory ), nullptr );
                    g_akExternalSourceMemorySizeSetter(
                        self, route->memorySize, nullptr );
                    routed = true;
                }
                __except ( EXCEPTION_EXECUTE_HANDLER ) {
                    routed = false;
                }
            }
            route->setterRouted = routed;
            if ( PackagedMediaLogAllowed( ) ) {
                char fileBuffer [ 384 ] = { 0 };
                TryCopyManagedStringObject(
                    managedFile, fileBuffer, sizeof( fileBuffer ) );
                Log( "[voice-memory-v9] setter voiceId=" +
                    std::string( route->voiceId [ 0 ]
                        ? route->voiceId : "<empty>" ) +
                    " eventId=" + std::to_string( route->eventId ) +
                    " mediaId=" + std::to_string( route->mediaId ) +
                    " expectedCodec=" +
                        std::to_string( route->expectedCodec ) +
                    " memory=" + std::to_string(
                        reinterpret_cast< std::uintptr_t >( route->memory ) ) +
                    " bytes=" + std::to_string( route->memorySize ) +
                    " routed=" + ( routed ? "true" : "false" ) +
                    " placeholder=" + ( fileBuffer [ 0 ]
                        ? std::string( fileBuffer ) : "<unreadable>" ) );
            }
            if ( routed )
                return;
        }
        g_originalAkExternalSourceFileSetter(
            self, managedFile, method );
    }

    static bool EnsureLanguagePackageReady( int targetLanguage ) {
        if ( targetLanguage < 0 || targetLanguage > 3 ||
            !g_tryLoadLanguagePck ||
            !g_getLanguageName ||
            !g_originalAkSoundEngineLoadFilePackage ||
            !g_originalAkSoundEngineUnloadFilePackage )
            return false;

        int currentLanguage = FOLLOW_GLOBAL_LANGUAGE;
        if ( TryReadCachedLanguage( &currentLanguage ) &&
            currentLanguage == targetLanguage ) {
            g_languagePackageReady [ targetLanguage ].store(
                true, std::memory_order_release );
            return true;
        }

        if ( g_languagePackageReady [ targetLanguage ].load(
                std::memory_order_acquire ) )
            return true;

        bool expected = false;
        if ( !g_languagePackageAttempted [ targetLanguage ].compare_exchange_strong(
                expected, true, std::memory_order_acq_rel ) )
            return g_languagePackageReady [ targetLanguage ].load(
                std::memory_order_acquire );

        void * managedLanguage = nullptr;
        __try {
            managedLanguage = g_getLanguageName( targetLanguage, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            managedLanguage = nullptr;
        }
        if ( !managedLanguage ) {
            Log( "[voice-pck] cannot prepare " +
                std::string( LanguageName( targetLanguage ) ) +
                ": VoiceI18n.GetLanguageName failed" );
            return false;
        }

        char resolvedLanguageBuffer [ 64 ] = { 0 };
        TryCopyManagedStringObject(
            managedLanguage, resolvedLanguageBuffer,
            sizeof( resolvedLanguageBuffer ) );
        Log( "[voice-pck] resolved " +
            std::string( LanguageName( targetLanguage ) ) +
            " through VoiceI18n.GetLanguageName as " +
            ( resolvedLanguageBuffer [ 0 ]
                ? std::string( resolvedLanguageBuffer )
                : std::string( "<unreadable>" ) ) );

        g_auxiliaryPackageLanguage.store(
            targetLanguage, std::memory_order_release );
        g_auxiliaryPackageLoads.store( 0, std::memory_order_release );
        g_auxiliaryPackageUnloadsSuppressed.store(
            0, std::memory_order_release );
        g_auxiliaryPackageLoad.store( true, std::memory_order_release );

        bool loaded = false;
        __try {
            loaded = g_tryLoadLanguagePck( managedLanguage, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            loaded = false;
        }

        g_auxiliaryPackageLoad.store( false, std::memory_order_release );
        g_auxiliaryPackageLanguage.store(
            FOLLOW_GLOBAL_LANGUAGE, std::memory_order_release );
        g_languagePackageReady [ targetLanguage ].store(
            loaded, std::memory_order_release );

        Log( "[voice-pck] prepare " +
            std::string( LanguageName( targetLanguage ) ) +
            " result=" + ( loaded ? "success" : "failed" ) +
            " loads=" + std::to_string(
                g_auxiliaryPackageLoads.load( std::memory_order_acquire ) ) +
            " preserved=" + std::to_string(
                g_auxiliaryPackageUnloadsSuppressed.load(
                    std::memory_order_acquire ) ) +
            " globalCache=" + ( currentLanguage >= 0 && currentLanguage <= 3
                ? std::string( LanguageName( currentLanguage ) )
                : std::string( "<unreadable>" ) ) );
        return loaded;
    }

    static bool ResolveExplicitLanguage(
        const VoiceRuleMap & rules, const std::string & identity,
        int & language, std::string & matchedKey ) {
        if ( identity.empty( ) )
            return false;

        auto found = rules.find( identity );
        if ( found != rules.end( ) ) {
            language = found->second;
            matchedKey = found->first;
            return true;
        }

        if ( identity.rfind( "chr_", 0 ) == 0 ) {
            const std::size_t suffixStart = identity.find( '_', 4 );
            if ( suffixStart != std::string::npos &&
                suffixStart + 1 < identity.size( ) ) {
                found = rules.find( identity.substr( suffixStart + 1 ) );
                if ( found != rules.end( ) ) {
                    language = found->second;
                    matchedKey = found->first;
                    return true;
                }
            }
        }
        return false;
    }

    static bool ContainsIdentityToken(
        const std::string & source, const std::string & identity ) {
        if ( source.empty( ) || identity.size( ) < 2 )
            return false;

        std::size_t position = source.find( identity );
        while ( position != std::string::npos ) {
            const auto isIdentifierCharacter = [ ] ( unsigned char ch ) {
                return std::isalnum( ch ) != 0;
                };
            const bool startsAtBoundary = position == 0 ||
                !isIdentifierCharacter(
                    static_cast< unsigned char >( source [ position - 1 ] ) );
            const std::size_t end = position + identity.size( );
            const bool endsAtBoundary = end == source.size( ) ||
                !isIdentifierCharacter(
                    static_cast< unsigned char >( source [ end ] ) );
            if ( startsAtBoundary && endsAtBoundary )
                return true;
            position = source.find( identity, position + 1 );
        }
        return false;
    }

    static bool ResolveLanguage(
        const std::string & speaker, const std::string & voiceData,
        const std::string & wwiseEvent, int & language,
        std::string & matchedIdentity ) {
        bool resolved = false;
        AcquireSRWLockShared( &g_rulesLock );
        if ( !g_enabled.load( std::memory_order_acquire ) ) {
            ReleaseSRWLockShared( &g_rulesLock );
            return false;
        }
        if ( ResolveExplicitLanguage(
            g_rules, speaker, language, matchedIdentity ) ) {
            resolved = true;
        }
        else {
            for ( const auto & [ key, configuredLanguage ] : g_rules ) {
                if ( ContainsIdentityToken( voiceData, key ) ||
                    ContainsIdentityToken( wwiseEvent, key ) ) {
                    language = configuredLanguage;
                    matchedIdentity = key;
                    resolved = true;
                    break;
                }
            }
        }
        if ( !resolved && g_defaultLanguage != NO_DEFAULT_LANGUAGE ) {
            language = g_defaultLanguage;
            matchedIdentity = "*";
            resolved = true;
        }
        ReleaseSRWLockShared( &g_rulesLock );
        return resolved;
    }

    static const std::string & PreferredVoiceIdentity(
        const std::string & speaker, const std::string & voiceData,
        const std::string & wwiseEvent ) {
        if ( !speaker.empty( ) )
            return speaker;
        if ( !voiceData.empty( ) )
            return voiceData;
        return wwiseEvent;
    }

    static bool MarkSpeakerObserved( const std::string & speaker ) {
        AcquireSRWLockExclusive( &g_observedLock );
        const bool inserted = g_observedSpeakers.insert( speaker ).second;
        ReleaseSRWLockExclusive( &g_observedLock );
        return inserted;
    }

    struct VoiceIdentity {
        std::string speaker;
        std::string voiceData;
        std::string wwiseEvent;
    };

    static VoiceIdentity ReadVoiceIdentity( void * voiceContext ) {
        char speakerBuffer [ 192 ] = { 0 };
        char voiceDataBuffer [ 512 ] = { 0 };
        char wwiseEventBuffer [ 192 ] = { 0 };
        TryCopyManagedStringField(
            voiceContext, VOICE_CONTEXT_SPEAKER_CHANNEL_OFFSET,
            speakerBuffer, sizeof( speakerBuffer ) );
        TryCopyManagedStringField(
            voiceContext, VOICE_CONTEXT_DATA_OFFSET,
            voiceDataBuffer, sizeof( voiceDataBuffer ) );
        TryCopyManagedStringField(
            voiceContext, VOICE_CONTEXT_WWISE_EVENT_OFFSET,
            wwiseEventBuffer, sizeof( wwiseEventBuffer ) );

        return {
            Normalize( speakerBuffer ),
            Normalize( voiceDataBuffer ),
            Normalize( wwiseEventBuffer )
        };
    }

    static bool ArmDiagnosticVoiceRequest(
        void * voiceContext,
        VoiceRequestDiagnosticContext & previousRequest ) {
        if ( !g_enabled.load( std::memory_order_acquire ) ||
            g_shuttingDown.load( std::memory_order_acquire ) )
            return false;
        ThreadRoutingContext * threadContext = GetThreadRoutingContext( );
        if ( !threadContext )
            return false;

        previousRequest = threadContext->voiceRequest;
        VoiceRequestDiagnosticContext request;
        const VoiceIdentity identity = ReadVoiceIdentity( voiceContext );
        int targetLanguage = FOLLOW_GLOBAL_LANGUAGE;
        std::string matchedIdentity;
        const bool bankedCandidate = !identity.voiceData.empty( ) &&
            identity.voiceData.find( ".wem" ) == std::string::npos;
        request.matched = bankedCandidate && ResolveLanguage(
            identity.speaker, identity.voiceData, identity.wwiseEvent,
            targetLanguage, matchedIdentity ) &&
            targetLanguage != FOLLOW_GLOBAL_LANGUAGE;
        request.scopeActive = request.matched;
        request.target = targetLanguage;
        request.codec = ReadPayloadU32(
            voiceContext, VOICE_CONTEXT_CODEC_OFFSET );
        request.wwiseEventObject = reinterpret_cast< void * >(
            ReadPayloadU64( voiceContext,
                VOICE_CONTEXT_WWISE_EVENT_OFFSET ) );
        request.observedAtMs = GetTickCount64( );
        if ( request.matched ) {
            strncpy_s( request.matchedIdentity,
                matchedIdentity.c_str( ), _TRUNCATE );
            strncpy_s( request.speaker,
                identity.speaker.c_str( ), _TRUNCATE );
            strncpy_s( request.data,
                identity.voiceData.c_str( ), _TRUNCATE );
            strncpy_s( request.event,
                identity.wwiseEvent.c_str( ), _TRUNCATE );
            if ( DiagnosticLogAllowed( ) ) {
                Log( "[voice-bank] request armed thread=" +
                    std::to_string( GetCurrentThreadId( ) ) +
                    " matched=" + matchedIdentity +
                    " requested=" + LanguageName( targetLanguage ) +
                    " speaker=" + ( identity.speaker.empty( )
                        ? std::string( "<empty>" ) : identity.speaker ) +
                    " data=" + identity.voiceData +
                    " selectedEvent=" + ( identity.wwiseEvent.empty( )
                        ? std::string( "<empty>" ) : identity.wwiseEvent ) +
                    " codec=" + std::to_string( request.codec ) );
            }
        }
        threadContext->voiceRequest = request;
        return true;
    }

    static void FinishDiagnosticVoiceRequest(
        bool armed,
        const VoiceRequestDiagnosticContext & previousRequest ) {
        if ( !armed )
            return;
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        if ( !threadContext )
            return;
        if ( previousRequest.scopeActive )
            threadContext->voiceRequest = previousRequest;
        else
            threadContext->voiceRequest.scopeActive = false;
    }

    static bool TryGetActiveBankVoiceRequest(
        VoiceRequestDiagnosticContext & request ) {
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        if ( !threadContext || !threadContext->voiceRequest.matched ||
            !threadContext->voiceRequest.scopeActive )
            return false;
        request = threadContext->voiceRequest;
        return request.target >= 0 && request.target <= 3;
    }

    static void * FindGameplayImage( ) {
        if ( !api::initialized || !api::get_domain ||
            !api::get_assemblies || !api::assembly_get_image ||
            !api::image_get_name )
            return nullptr;

        void * domain = api::get_domain( );
        if ( !domain )
            return nullptr;
        size_t count = 0;
        void ** assemblies = api::get_assemblies( domain, &count );
        if ( !assemblies )
            return nullptr;
        for ( size_t index = 0; index < count; ++index ) {
            void * image = api::assembly_get_image( assemblies [ index ] );
            const char * name = image ? api::image_get_name( image ) : nullptr;
            if ( name && ( _stricmp( name, "Gameplay.Beyond.dll" ) == 0 ||
                _stricmp( name, "Gameplay.Beyond" ) == 0 ) )
                return image;
        }
        return nullptr;
    }

    static bool IsExecutableAddress( void * address ) {
        if ( !address )
            return false;
        MEMORY_BASIC_INFORMATION info { };
        if ( VirtualQuery( address, &info, sizeof( info ) ) == 0 )
            return false;
        const DWORD executable = PAGE_EXECUTE | PAGE_EXECUTE_READ |
            PAGE_EXECUTE_READWRITE | PAGE_EXECUTE_WRITECOPY;
        return info.State == MEM_COMMIT &&
            ( info.Protect & executable ) != 0;
    }

    static bool IsGameAssemblyMethodEntry( void * address ) {
        const uintptr_t value = reinterpret_cast< uintptr_t >( address );
        if ( !value || !api::module_base || value < api::module_base )
            return false;
        if ( api::module_size &&
            value >= api::module_base + api::module_size )
            return false;
        return IsExecutableAddress( address );
    }

    static std::string PointerText( const void * value ) {
        char buffer [ 32 ] = { 0 };
        sprintf_s( buffer, "%p", value );
        return buffer;
    }

    struct GameplayMethodResolution {
        void * klass = nullptr;
        void * methodInfo = nullptr;
        void * entry = nullptr;
    };

    static void * ReadGameplayMethodEntry(
        void * methodInfo, const char * & source ) {
        source = "none";
        if ( !methodInfo )
            return nullptr;

        if ( api::method_get_pointer ) {
            void * candidate = nullptr;
            __try {
                candidate = const_cast< void * >(
                    api::method_get_pointer( methodInfo ) );
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                candidate = nullptr;
            }
            if ( IsGameAssemblyMethodEntry( candidate ) ) {
                source = "export";
                return candidate;
            }
        }

        void * candidate = nullptr;
        __try {
            candidate = *reinterpret_cast< void ** >( methodInfo );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            candidate = nullptr;
        }
        if ( IsGameAssemblyMethodEntry( candidate ) ) {
            source = "MethodInfo[0]";
            return candidate;
        }
        return nullptr;
    }

    static GameplayMethodResolution ResolveGameplayMethod(
        void * image, const char * namespaze, const char * className,
        const char * methodName, int parameterCount, const char * label ) {
        GameplayMethodResolution result;
        if ( !image || !api::class_from_name ||
            !api::class_get_method_from_name ) {
            Log( "[lip-route] resolve " + std::string( label ) +
                " failed: reflection API unavailable" );
            return result;
        }

        result.klass = api::class_from_name(
            image, namespaze, className );
        if ( result.klass )
            result.methodInfo = api::class_get_method_from_name(
                result.klass, methodName, parameterCount );

        const char * entrySource = "none";
        result.entry = ReadGameplayMethodEntry(
            result.methodInfo, entrySource );
        Log( "[lip-route] resolve " + std::string( label ) +
            " class=" + ( result.klass ? "found" : "missing" ) +
            " methodInfo=" + PointerText( result.methodInfo ) +
            " entry=" + PointerText( result.entry ) +
            " entrySource=" + entrySource +
            " params=" + std::to_string( parameterCount ) );
        return result;
    }

    static ManagedGetterStringResult ReadManagedGetterString(
        void * getterMethodInfo, void * instance ) {
        ManagedGetterStringResult result;
        if ( !getterMethodInfo ) {
            result.status = "getter-missing";
            return result;
        }
        if ( !instance ) {
            result.status = "instance-null";
            return result;
        }
        if ( !api::runtime_invoke ) {
            result.status = "runtime-invoke-missing";
            return result;
        }

        void * exception = nullptr;
        void * value = nullptr;
        __try {
            value = api::runtime_invoke(
                getterMethodInfo, instance, nullptr, &exception );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            result.status = "native-exception";
            return result;
        }
        if ( exception ) {
            result.status = "managed-exception";
            result.managedException = exception;
            return result;
        }
        if ( !value ) {
            result.status = "value-null";
            return result;
        }

        char buffer [ 256 ] = { 0 };
        if ( !TryCopyManagedStringObject(
            value, buffer, sizeof( buffer ) ) ) {
            result.status = "copy-failed";
            return result;
        }
        result.raw = buffer;
        result.normalized = Normalize( buffer );
        result.status = result.raw.empty( ) ? "empty" : "ok";
        return result;
    }

    static bool g_lipDialogHookCreated = false;
    static bool g_lipPathHookCreated = false;
    static bool g_lipLoadHookCreated = false;
    static bool g_lipLanguageHookCreated = false;

    static void RemoveLipSyncHooks( ) {
        if ( g_lipDialogHookCreated && g_dialogManagerPlayLipSyncTrackTarget ) {
            MH_DisableHook( g_dialogManagerPlayLipSyncTrackTarget );
            MH_RemoveHook( g_dialogManagerPlayLipSyncTrackTarget );
        }
        if ( g_lipPathHookCreated && g_lipSyncGetTrackPathTarget ) {
            MH_DisableHook( g_lipSyncGetTrackPathTarget );
            MH_RemoveHook( g_lipSyncGetTrackPathTarget );
        }
        if ( g_lipLoadHookCreated && g_lipSyncTryLoadTrackTarget ) {
            MH_DisableHook( g_lipSyncTryLoadTrackTarget );
            MH_RemoveHook( g_lipSyncTryLoadTrackTarget );
        }
        if ( g_lipLanguageHookCreated &&
            g_voiceI18nGetCurrentLanguageTarget ) {
            MH_DisableHook( g_voiceI18nGetCurrentLanguageTarget );
            MH_RemoveHook( g_voiceI18nGetCurrentLanguageTarget );
        }
        g_lipDialogHookCreated = false;
        g_lipPathHookCreated = false;
        g_lipLoadHookCreated = false;
        g_lipLanguageHookCreated = false;
        g_dialogManagerPlayLipSyncTrackTarget = nullptr;
        g_lipSyncGetTrackPathTarget = nullptr;
        g_lipSyncTryLoadTrackTarget = nullptr;
        g_voiceI18nGetCurrentLanguageTarget = nullptr;
        g_originalDialogManagerPlayLipSyncTrack = nullptr;
        g_originalLipSyncGetTrackPath = nullptr;
        g_originalLipSyncTryLoadTrack = nullptr;
        g_originalVoiceI18nGetCurrentLanguage = nullptr;
        g_dialogActionGetRealActorNameId = nullptr;
        g_dialogActionGetActorNameId = nullptr;
        g_dialogActionGetTrunkId = nullptr;
        g_lipHooksReady.store( false, std::memory_order_release );
    }

    static int __fastcall HookVoiceI18nGetCurrentLanguage(
        void * methodInfo ) {
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        DurationRoutingContext * durationRouting = threadContext
            ? &threadContext->durationRouting : nullptr;
        if ( g_enabled.load( std::memory_order_acquire ) &&
            durationRouting && durationRouting->active &&
            durationRouting->target >= 0 &&
            durationRouting->target <= 3 ) {
            const std::uint32_t hit =
                g_durationLanguageOverrideHits.fetch_add(
                    1, std::memory_order_relaxed ) + 1;
            if ( hit <= LIP_DIAGNOSTIC_LOG_LIMIT &&
                g_diagnosticsEnabled.load( std::memory_order_acquire ) ) {
                Log( "[voice-duration-route] language-hit hit=" +
                    std::to_string( hit ) +
                    " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                    " voiceId=" + ( durationRouting->voiceId [ 0 ]
                        ? std::string( durationRouting->voiceId )
                        : std::string( "<empty>" ) ) +
                    " matched=" +
                        ( durationRouting->matchedIdentity [ 0 ]
                            ? std::string(
                                durationRouting->matchedIdentity )
                            : std::string( "<empty>" ) ) +
                    " override=" + LanguageName(
                        durationRouting->target ) +
                    "(" + std::to_string(
                        durationRouting->target ) + ")" );
            }
            return durationRouting->target;
        }
        LipRoutingContext * routing = threadContext
            ? &threadContext->lipRouting : nullptr;
        if ( g_enabled.load( std::memory_order_acquire ) &&
            routing && routing->active && routing->target >= 0 &&
            routing->target <= 3 ) {
            const std::uint32_t hit = g_lipLanguageOverrideHits.fetch_add(
                1, std::memory_order_relaxed ) + 1;
            if ( hit <= LIP_DIAGNOSTIC_LOG_LIMIT ) {
                Log( "[lip-route] language-hit hit=" +
                    std::to_string( hit ) +
                    " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                    " line=" + ( routing->trunkId [ 0 ]
                        ? std::string( routing->trunkId )
                        : std::string( "<empty>" ) ) +
                    " override=" + LanguageName( routing->target ) +
                    "(" + std::to_string( routing->target ) + ")" );
            }
            return routing->target;
        }
        return g_originalVoiceI18nGetCurrentLanguage
            ? g_originalVoiceI18nGetCurrentLanguage( methodInfo ) : 0;
    }

    static void __fastcall HookDialogManagerPlayLipSyncTrack(
        void * self, void * trunkActionData, void * entity,
        void * methodInfo ) {
        ThreadRoutingContext * threadContext = GetThreadRoutingContext( );
        const ManagedGetterStringResult realActorResult =
            ReadManagedGetterString(
                g_dialogActionGetRealActorNameId, trunkActionData );
        const ManagedGetterStringResult actorResult =
            ReadManagedGetterString(
                g_dialogActionGetActorNameId, trunkActionData );
        const ManagedGetterStringResult trunkResult =
            ReadManagedGetterString(
                g_dialogActionGetTrunkId, trunkActionData );
        const std::uint32_t dialogHit = g_lipDialogHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        if ( dialogHit <= LIP_DIAGNOSTIC_LOG_LIMIT ) {
            Log( "[lip-route] dialog-hit hit=" + std::to_string( dialogHit ) +
                " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                " context=" + ( threadContext ? "ready" : "missing" ) +
                " action=" + PointerText( trunkActionData ) +
                " entity=" + PointerText( entity ) +
                " realActor=" + ( realActorResult.raw.empty( )
                    ? std::string( "<empty>" ) : realActorResult.raw ) +
                " realStatus=" + realActorResult.status +
                " realException=" +
                    PointerText( realActorResult.managedException ) +
                " actor=" + ( actorResult.raw.empty( )
                    ? std::string( "<empty>" ) : actorResult.raw ) +
                " actorStatus=" + actorResult.status +
                " actorException=" +
                    PointerText( actorResult.managedException ) +
                " trunk=" + ( trunkResult.raw.empty( )
                    ? std::string( "<empty>" ) : trunkResult.raw ) +
                " trunkStatus=" + trunkResult.status +
                " trunkException=" +
                    PointerText( trunkResult.managedException ) );
        }
        if ( !threadContext ) {
            if ( g_originalDialogManagerPlayLipSyncTrack )
                g_originalDialogManagerPlayLipSyncTrack(
                    self, trunkActionData, entity, methodInfo );
            return;
        }

        LipRoutingContext & current = threadContext->lipRouting;
        const LipRoutingContext previous = current;
        LipRoutingContext next = previous;
        bool routed = false;
        if ( g_replaceNarrativeVoice.load( std::memory_order_acquire ) &&
            trunkActionData ) {
            std::string actor = realActorResult.normalized;
            if ( actor.empty( ) )
                actor = actorResult.normalized;

            int targetLanguage = FOLLOW_GLOBAL_LANGUAGE;
            std::string matchedIdentity;
            if ( !actor.empty( ) && ResolveLanguage(
                actor, std::string( ), std::string( ), targetLanguage,
                matchedIdentity ) &&
                targetLanguage != FOLLOW_GLOBAL_LANGUAGE ) {
                next = LipRoutingContext { };
                next.active = true;
                next.target = targetLanguage;
                strncpy_s( next.actor, actor.c_str( ), _TRUNCATE );
                strncpy_s(
                    next.matchedIdentity, matchedIdentity.c_str( ), _TRUNCATE );
                const std::string & trunkId = trunkResult.normalized;
                strncpy_s( next.trunkId, trunkId.c_str( ), _TRUNCATE );
                current = next;
                routed = true;
                if ( g_lipRouteLogs.fetch_add(
                    1, std::memory_order_relaxed ) < LIP_ROUTE_LOG_LIMIT ) {
                    Log( "[lip-route] enter actor=" + actor +
                        " matched=" + matchedIdentity +
                        " requested=" + LanguageName( targetLanguage ) +
                        "(" + std::to_string( targetLanguage ) + ")" +
                        " trunk=" + ( trunkId.empty( )
                            ? std::string( "<empty>" ) : trunkId ) );
                }
            }
        }

        if ( g_originalDialogManagerPlayLipSyncTrack )
            g_originalDialogManagerPlayLipSyncTrack(
                self, trunkActionData, entity, methodInfo );
        current = previous;
        if ( routed && g_lipRouteLogs.load( std::memory_order_relaxed ) <=
            LIP_ROUTE_LOG_LIMIT )
            Log( "[lip-route] leave actor=" +
                std::string( next.actor[ 0 ] ? next.actor : "<empty>" ) +
                " track=" + ( next.trunkId [ 0 ]
                    ? std::string( next.trunkId ) : std::string( "<empty>" ) ) );
    }

    static void * __fastcall HookLipSyncGetTrackPath(
        int language, void * voiceId, void * suffix, void * methodInfo ) {
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        LipRoutingContext * routing = threadContext
            ? &threadContext->lipRouting : nullptr;
        const bool routed = g_enabled.load( std::memory_order_acquire ) &&
            routing && routing->active &&
            routing->target != FOLLOW_GLOBAL_LANGUAGE;
        const int routedLanguage = routed ? routing->target : language;
        if ( routed )
            routing->sourceLanguage = language;
        void * result = g_originalLipSyncGetTrackPath
            ? g_originalLipSyncGetTrackPath(
                routedLanguage, voiceId, suffix, methodInfo )
            : nullptr;
        const std::uint32_t pathHit = g_lipPathHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        if ( pathHit <= LIP_DIAGNOSTIC_LOG_LIMIT ) {
            char voiceBuffer [ 256 ] = { 0 };
            char suffixBuffer [ 128 ] = { 0 };
            char resultBuffer [ 512 ] = { 0 };
            TryCopyManagedStringObject(
                voiceId, voiceBuffer, sizeof( voiceBuffer ) );
            TryCopyManagedStringObject(
                suffix, suffixBuffer, sizeof( suffixBuffer ) );
            TryCopyManagedStringObject(
                result, resultBuffer, sizeof( resultBuffer ) );
            Log( "[lip-route] path-hit hit=" + std::to_string( pathHit ) +
                " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                " routed=" + ( routed ? "true" : "false" ) +
                " language=" + LanguageName( language ) +
                "(" + std::to_string( language ) + ")" +
                " voiceId=" + ( voiceBuffer [ 0 ]
                    ? std::string( voiceBuffer ) : std::string( "<empty>" ) ) +
                " suffix=" + ( suffixBuffer [ 0 ]
                    ? std::string( suffixBuffer ) : std::string( "<empty>" ) ) +
                " path=" + ( resultBuffer [ 0 ]
                    ? std::string( resultBuffer ) : std::string( "<empty>" ) ) );
        }
        if ( routed && g_lipRouteLogs.fetch_add(
            1, std::memory_order_relaxed ) < LIP_ROUTE_LOG_LIMIT ) {
            char voiceBuffer [ 256 ] = { 0 };
            char suffixBuffer [ 128 ] = { 0 };
            char resultBuffer [ 512 ] = { 0 };
            TryCopyManagedStringObject(
                voiceId, voiceBuffer, sizeof( voiceBuffer ) );
            TryCopyManagedStringObject(
                suffix, suffixBuffer, sizeof( suffixBuffer ) );
            TryCopyManagedStringObject(
                result, resultBuffer, sizeof( resultBuffer ) );
            Log( "[lip-route] path actor=" +
                std::string( routing->actor[ 0 ]
                    ? routing->actor : "<empty>" ) +
                " from=" + LanguageName( language ) +
                " to=" + LanguageName( routedLanguage ) +
                " voiceId=" + ( voiceBuffer [ 0 ]
                    ? std::string( voiceBuffer ) : std::string( "<empty>" ) ) +
                " suffix=" + ( suffixBuffer [ 0 ]
                    ? std::string( suffixBuffer ) : std::string( "<empty>" ) ) +
                " path=" + ( resultBuffer [ 0 ]
                    ? std::string( resultBuffer ) : std::string( "<empty>" ) ) );
        }
        return result;
    }

    static bool __fastcall HookLipSyncTryLoadTrack(
        void * lineId, void ** track, void * methodInfo ) {
        char lineBuffer [ 256 ] = { 0 };
        TryCopyManagedStringObject(
            lineId, lineBuffer, sizeof( lineBuffer ) );
        const std::string normalizedLine = Normalize( lineBuffer );
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        LipRoutingContext * routing = threadContext
            ? &threadContext->lipRouting : nullptr;
        PendingLipRoute * pending = threadContext
            ? &threadContext->pendingLipRoute : nullptr;
        const PendingLipRoute pendingSnapshot = pending
            ? *pending : PendingLipRoute { };
        const bool pendingMatches =
            g_enabled.load( std::memory_order_acquire ) &&
            pendingSnapshot.armed &&
            pendingSnapshot.generation == g_configGeneration.load(
                std::memory_order_acquire ) &&
            !normalizedLine.empty( ) && pendingSnapshot.lineId [ 0 ] &&
            normalizedLine == pendingSnapshot.lineId;

        LipRoutingContext previousRouting;
        bool pendingInjected = false;
        if ( pendingMatches && routing && !routing->active ) {
            previousRouting = *routing;
            *routing = LipRoutingContext { };
            routing->active = true;
            routing->target = pendingSnapshot.target;
            strncpy_s(
                routing->actor, pendingSnapshot.matchedIdentity, _TRUNCATE );
            strncpy_s(
                routing->matchedIdentity,
                pendingSnapshot.matchedIdentity, _TRUNCATE );
            strncpy_s(
                routing->trunkId, normalizedLine.c_str( ), _TRUNCATE );
            pendingInjected = true;
        }
        const bool routed = g_enabled.load( std::memory_order_acquire ) &&
            routing && routing->active &&
            routing->target != FOLLOW_GLOBAL_LANGUAGE;
        const LipRoutingContext routedSnapshot = routed
            ? *routing : LipRoutingContext { };

        // TryLoadTrack's internal path helper reads VoiceI18n directly. The
        // getter hook below observes this TLS routing context only for the
        // duration of this call, leaving the global Wwise language untouched.
        bool result = g_originalLipSyncTryLoadTrack
            ? g_originalLipSyncTryLoadTrack(
                lineId, track, methodInfo ) : false;
        bool fallback = false;
        if ( routed && !result && g_originalLipSyncTryLoadTrack ) {
            const bool previousActive = routing->active;
            routing->active = false;
            result = g_originalLipSyncTryLoadTrack(
                lineId, track, methodInfo );
            routing->active = previousActive;
            fallback = result;
        }
        if ( pendingMatches )
            ClearPendingLipRoute( threadContext );
        if ( pendingInjected )
            *routing = previousRouting;

        void * resolvedTrack = nullptr;
        __try {
            resolvedTrack = track ? *track : nullptr;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            resolvedTrack = nullptr;
        }
        const std::uint32_t loadHit = g_lipLoadHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        if ( loadHit <= LIP_DIAGNOSTIC_LOG_LIMIT ) {
            Log( "[lip-route] load-hit hit=" + std::to_string( loadHit ) +
                " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                " routed=" + ( routed ? "true" : "false" ) +
                " pending=" + ( pendingMatches ? "matched" :
                    ( pendingSnapshot.armed ? "mismatch" : "none" ) ) +
                " pendingLine=" + ( pendingSnapshot.lineId [ 0 ]
                    ? std::string( pendingSnapshot.lineId )
                    : std::string( "<empty>" ) ) +
                " line=" + ( lineBuffer [ 0 ]
                    ? std::string( lineBuffer ) : std::string( "<empty>" ) ) +
                " result=" + ( result ? "true" : "false" ) +
                " fallback=" + ( fallback ? "true" : "false" ) +
                " track=" + PointerText( resolvedTrack ) );
        }
        if ( routed && g_lipRouteLogs.fetch_add(
            1, std::memory_order_relaxed ) < LIP_ROUTE_LOG_LIMIT ) {
            Log( "[lip-route] load actor=" +
                std::string( routedSnapshot.actor[ 0 ]
                    ? routedSnapshot.actor : "<empty>" ) +
                " line=" + ( lineBuffer [ 0 ]
                    ? std::string( lineBuffer ) : std::string( "<empty>" ) ) +
                " target=" + LanguageName( routedSnapshot.target ) +
                " result=" + ( result ? "true" : "false" ) +
                " fallback=" + ( fallback ? "true" : "false" ) );
        }
        return result;
    }

    static bool InstallLipSyncHook(
        void * target, void * detour, void ** original, const char * name,
        bool & created ) {
        if ( !target || !IsExecutableAddress( target ) ) {
            Log( "[lip-route] invalid target for " + std::string( name ) );
            return false;
        }
        const MH_STATUS status = MH_CreateHook( target, detour, original );
        if ( status != MH_OK ) {
            Log( "[lip-route] MH_CreateHook(" + std::string( name ) +
                ") failed: " + std::to_string( static_cast< int >( status ) ) );
            return false;
        }
        created = true;
        return true;
    }

    static bool EnsureLipSyncHooksOnGameThread( ) {
        if ( g_lipHooksReady.load( std::memory_order_acquire ) )
            return true;
        bool expected = false;
        if ( !g_lipHooksAttempted.compare_exchange_strong(
            expected, true, std::memory_order_acq_rel ) )
            return false;

        api::init( );
        void * image = FindGameplayImage( );
        if ( !image ) {
            Log( "[lip-route] Gameplay.Beyond image unavailable; lip hooks skipped" );
            return false;
        }

        const GameplayMethodResolution dialogPlay = ResolveGameplayMethod(
            image, "Beyond.Gameplay.Core", "DialogManager",
            "_PlayLipSyncTrack", 2, "DialogManager._PlayLipSyncTrack" );
        const GameplayMethodResolution getTrackPath = ResolveGameplayMethod(
            image, "Beyond.Gameplay.View.LipSync", "LipSyncUtils",
            "GetLipSyncTrackPath", 3,
            "LipSyncUtils.GetLipSyncTrackPath(AudioLang,string,string)" );
        const GameplayMethodResolution tryLoadTrack = ResolveGameplayMethod(
            image, "Beyond.Gameplay.View.LipSync", "LipSyncUtils",
            "TryLoadTrack", 2, "LipSyncUtils.TryLoadTrack" );
        const GameplayMethodResolution getRealActor = ResolveGameplayMethod(
            image, "Beyond.Gameplay", "DialogPlayTrunkActionData",
            "get_realActorNameId", 0,
            "DialogPlayTrunkActionData.get_realActorNameId" );
        const GameplayMethodResolution getActor = ResolveGameplayMethod(
            image, "Beyond.Gameplay", "DialogPlayTrunkActionData",
            "get_actorNameId", 0,
            "DialogPlayTrunkActionData.get_actorNameId" );
        const GameplayMethodResolution getTrunk = ResolveGameplayMethod(
            image, "Beyond.Gameplay", "DialogPlayTrunkActionData",
            "get_trunkId", 0,
            "DialogPlayTrunkActionData.get_trunkId" );

        g_dialogManagerPlayLipSyncTrackTarget = dialogPlay.entry;
        g_lipSyncGetTrackPathTarget = getTrackPath.entry;
        g_lipSyncTryLoadTrackTarget = tryLoadTrack.entry;
        g_voiceI18nGetCurrentLanguageTarget =
            reinterpret_cast< void * >( g_getCurrentLanguage );
        g_dialogActionGetRealActorNameId = getRealActor.methodInfo;
        g_dialogActionGetActorNameId = getActor.methodInfo;
        g_dialogActionGetTrunkId = getTrunk.methodInfo;

        if ( !g_dialogManagerPlayLipSyncTrackTarget ||
            !g_lipSyncGetTrackPathTarget || !g_lipSyncTryLoadTrackTarget ||
            !g_voiceI18nGetCurrentLanguageTarget ||
            !g_dialogActionGetRealActorNameId ) {
            Log( "[lip-route] required method resolution incomplete; lip hooks skipped" );
            RemoveLipSyncHooks( );
            return false;
        }

        const bool created =
            InstallLipSyncHook(
                g_dialogManagerPlayLipSyncTrackTarget,
                reinterpret_cast< void * >(
                    &HookDialogManagerPlayLipSyncTrack ),
                reinterpret_cast< void ** >(
                    &g_originalDialogManagerPlayLipSyncTrack ),
                "DialogManager._PlayLipSyncTrack", g_lipDialogHookCreated ) &&
            InstallLipSyncHook(
                g_lipSyncGetTrackPathTarget,
                reinterpret_cast< void * >( &HookLipSyncGetTrackPath ),
                reinterpret_cast< void ** >( &g_originalLipSyncGetTrackPath ),
                "LipSyncUtils.GetLipSyncTrackPath", g_lipPathHookCreated ) &&
            InstallLipSyncHook(
                g_lipSyncTryLoadTrackTarget,
                reinterpret_cast< void * >( &HookLipSyncTryLoadTrack ),
                reinterpret_cast< void ** >( &g_originalLipSyncTryLoadTrack ),
                "LipSyncUtils.TryLoadTrack", g_lipLoadHookCreated ) &&
            InstallLipSyncHook(
                g_voiceI18nGetCurrentLanguageTarget,
                reinterpret_cast< void * >(
                    &HookVoiceI18nGetCurrentLanguage ),
                reinterpret_cast< void ** >(
                    &g_originalVoiceI18nGetCurrentLanguage ),
                "VoiceI18n.GetCurrentLanguage",
                g_lipLanguageHookCreated );
        if ( !created ) {
            RemoveLipSyncHooks( );
            return false;
        }

        const bool enabled =
            MH_EnableHook( g_dialogManagerPlayLipSyncTrackTarget ) == MH_OK &&
            MH_EnableHook( g_lipSyncGetTrackPathTarget ) == MH_OK &&
            MH_EnableHook( g_lipSyncTryLoadTrackTarget ) == MH_OK &&
            MH_EnableHook( g_voiceI18nGetCurrentLanguageTarget ) == MH_OK;
        if ( !enabled ) {
            Log( "[lip-route] enabling lip hooks failed; preserving original lip sync" );
            RemoveLipSyncHooks( );
            return false;
        }

        g_lipHooksReady.store( true, std::memory_order_release );
        Log( "[lip-route] dynamic lip hooks installed on the game thread" );
        return true;
    }

    static void EndRouting( bool routed ) {
        ( void )routed;
    }

    static bool BeginContextRouting(
        void * voiceContext, const char * source, std::uint32_t hit ) {
        const VoiceIdentity identity = ReadVoiceIdentity( voiceContext );
        if ( hit <= DIAGNOSTIC_HIT_LIMIT ) {
            Log( "[voice-diag] " + std::string( source ) +
                " hit=" + std::to_string( hit ) +
                " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                " context=" + ( voiceContext ? "non-null" : "null" ) +
                " speaker=" +
                ( identity.speaker.empty( ) ? "<empty>" : identity.speaker ) +
                " data=" +
                ( identity.voiceData.empty( ) ? "<empty>" : identity.voiceData ) +
                " event=" +
                ( identity.wwiseEvent.empty( ) ? "<empty>" : identity.wwiseEvent ) );
        }

        const std::string & observedIdentity = PreferredVoiceIdentity(
            identity.speaker, identity.voiceData, identity.wwiseEvent );
        if ( !observedIdentity.empty( ) && MarkSpeakerObserved( observedIdentity ) ) {
            Log( "[voice-lang] observed speakerChannel=" +
                ( identity.speaker.empty( )
                    ? std::string( "<empty>" ) : identity.speaker ) +
                " data=" +
                ( identity.voiceData.empty( )
                    ? std::string( "<empty>" ) : identity.voiceData ) +
                " event=" +
                ( identity.wwiseEvent.empty( )
                    ? std::string( "<empty>" ) : identity.wwiseEvent ) );
        }

        if ( !voiceContext )
            return false;

        int targetLanguage = FOLLOW_GLOBAL_LANGUAGE;
        std::string matchedIdentity;
        if ( !ResolveLanguage(
            identity.speaker, identity.voiceData, identity.wwiseEvent,
            targetLanguage, matchedIdentity ) ||
            targetLanguage == FOLLOW_GLOBAL_LANGUAGE ) {
            if ( observedIdentity.empty( ) &&
                g_identityFailures.fetch_add(
                    1, std::memory_order_relaxed ) < 5 ) {
                Log( "[voice-lang] " + std::string( source ) +
                    " hit=" + std::to_string( hit ) +
                    " but no readable speaker/data/event identity was found" );
            }
            return false;
        }

        // This build observes request selection only. Per-request mutation is
        // deferred until we know which VoiceContext field identifies the
        // localized Wwise event or media selected by the game.
        return false;
    }

    static bool BeginStringRouting(
        void * managedString, const char * source, std::uint32_t hit ) {
        char buffer [ 512 ] = { 0 };
        TryCopyManagedStringObject( managedString, buffer, sizeof( buffer ) );
        const std::string identity = Normalize( buffer );
        if ( hit <= DIAGNOSTIC_HIT_LIMIT ) {
            Log( "[voice-diag] " + std::string( source ) +
                " hit=" + std::to_string( hit ) +
                " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                " id=" +
                ( identity.empty( ) ? "<unreadable>" : identity ) );
        }

        if ( identity.empty( ) )
            return false;

        int targetLanguage = FOLLOW_GLOBAL_LANGUAGE;
        std::string matchedIdentity;
        if ( !ResolveLanguage(
            std::string( ), identity, std::string( ),
            targetLanguage, matchedIdentity ) )
            return false;
        return false;
    }

    static bool __fastcall HookVoiceManagerSpeakString(
        void * self, void * voiceId, std::uint64_t audioObjectId,
        std::uint32_t * voiceHandleId ) {
        const std::uint32_t hit = g_managerSpeakStringHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        const bool routed = BeginStringRouting(
            voiceId, "VoiceManager.Speak(string)", hit );
        const bool result = g_originalVoiceManagerSpeakString(
            self, voiceId, audioObjectId, voiceHandleId );
        EndRouting( routed );
        return result;
    }

    static std::uint32_t __fastcall HookVoiceManagerSpeak(
        void * self, void * voiceContext ) {
        const std::uint32_t hit = g_managerSpeakHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        const bool routed = BeginContextRouting(
            voiceContext, "VoiceManager._Speak", hit );
        const std::uint32_t result = g_originalVoiceManagerSpeak(
            self, voiceContext );
        EndRouting( routed );
        return result;
    }

    static void __fastcall HookVoiceSpeakChannelPlayVoice(
        void * self, void * voiceContext ) {
        const std::uint32_t hit = g_channelPlayVoiceHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        const bool routed = BeginContextRouting(
            voiceContext, "VoiceSpeakChannelProcessor._PlayVoice", hit );
        g_originalVoiceSpeakChannelPlayVoice( self, voiceContext );
        EndRouting( routed );
    }

    static void __fastcall HookVoiceUtilsSelectWwiseEvent(
        void * voiceContext ) {
        EnsureLipSyncHooksOnGameThread( );
        const std::uint32_t hit = g_selectWwiseEventHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        const VoiceIdentity before = ReadVoiceIdentity( voiceContext );

        int targetLanguage = FOLLOW_GLOBAL_LANGUAGE;
        std::string matchedIdentity;
        const bool matchedBefore = voiceContext && ResolveLanguage(
            before.speaker, before.voiceData, before.wwiseEvent,
            targetLanguage, matchedIdentity ) &&
            targetLanguage != FOLLOW_GLOBAL_LANGUAGE;

        bool logMatch = false;
        if ( matchedBefore ) {
            logMatch = g_selectWwiseEventMatchLogs.fetch_add(
                1, std::memory_order_relaxed ) <
                VOICE_SELECT_MATCH_LOG_LIMIT;
            if ( logMatch ) {
                Log( "[voice-select] enter hit=" + std::to_string( hit ) +
                    " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                    " matched=" + matchedIdentity +
                    " requested=" + LanguageName( targetLanguage ) +
                    "(" + std::to_string( targetLanguage ) + ")" +
                    " speaker=" +
                    ( before.speaker.empty( )
                        ? std::string( "<empty>" ) : before.speaker ) +
                    " data=" +
                    ( before.voiceData.empty( )
                        ? std::string( "<empty>" ) : before.voiceData ) +
                    " event=" +
                    ( before.wwiseEvent.empty( )
                        ? std::string( "<empty>" ) : before.wwiseEvent ) );
            }
        }

        g_originalVoiceUtilsSelectWwiseEvent( voiceContext );

        const VoiceIdentity after = ReadVoiceIdentity( voiceContext );
        bool matchedAfter = matchedBefore;
        if ( !matchedAfter && voiceContext ) {
            matchedAfter = ResolveLanguage(
                after.speaker, after.voiceData, after.wwiseEvent,
                targetLanguage, matchedIdentity ) &&
                targetLanguage != FOLLOW_GLOBAL_LANGUAGE;
            if ( matchedAfter )
                logMatch = g_selectWwiseEventMatchLogs.fetch_add(
                    1, std::memory_order_relaxed ) <
                    VOICE_SELECT_MATCH_LOG_LIMIT;
        }

        if ( matchedAfter && logMatch ) {
            Log( "[voice-select] leave hit=" + std::to_string( hit ) +
                " matched=" + matchedIdentity +
                " requested=" + LanguageName( targetLanguage ) +
                "(" + std::to_string( targetLanguage ) + ")" +
                " matchedOnEnter=" +
                ( matchedBefore ? std::string( "true" ) : "false" ) +
                " eventChanged=" +
                ( before.wwiseEvent != after.wwiseEvent
                    ? std::string( "true" ) : "false" ) +
                " dataChanged=" +
                ( before.voiceData != after.voiceData
                    ? std::string( "true" ) : "false" ) +
                " beforeEvent=" +
                ( before.wwiseEvent.empty( )
                    ? std::string( "<empty>" ) : before.wwiseEvent ) +
                " afterEvent=" +
                ( after.wwiseEvent.empty( )
                    ? std::string( "<empty>" ) : after.wwiseEvent ) +
                " beforeData=" +
                ( before.voiceData.empty( )
                    ? std::string( "<empty>" ) : before.voiceData ) +
                " afterData=" +
                ( after.voiceData.empty( )
                    ? std::string( "<empty>" ) : after.voiceData ) );
        }
    }

    static std::uint32_t __fastcall HookVoicePlayerPlayEvent(
        void * eventName, std::uint64_t audioObjectId,
        std::uint32_t handleId, void * method ) {
        VoiceRequestDiagnosticContext request;
        const bool matched = TryGetActiveBankVoiceRequest( request );
        ThreadRoutingContext * threadContext = GetThreadRoutingContext( );
        const bool previousBankEventRouted = threadContext &&
            threadContext->bankEventRouted;
        char eventBuffer [ 192 ] = { 0 };
        if ( matched )
            TryCopyManagedStringObject(
                eventName, eventBuffer, sizeof( eventBuffer ) );

        if ( matched && threadContext && NativeRouteEligible( request ) ) {
            threadContext->bankEventRouted = false;
            const std::uint32_t nativePlayingId =
                g_originalVoicePlayerPlayEvent(
                    eventName, audioObjectId, handleId, method );
            threadContext->bankEventRouted = previousBankEventRouted;
            if ( PackagedMediaLogAllowed( ) ) {
                Log( "[voice-native] original Wwise container submitted voiceId=" +
                    std::string( request.data [ 0 ]
                        ? request.data : "<empty>" ) +
                    " target=" + LanguageName( request.target ) +
                    " playingId=" + std::to_string( nativePlayingId ) );
            }
            return nativePlayingId;
        }

        std::uint32_t packagedEventId = 0;
        std::uint32_t packagedMediaId = 0;
        std::uint32_t packagedCodec = 0;
        std::string packagedFailure;
        const bool packagedMapped = matched && ResolvePackagedMedia(
            request, handleId, packagedEventId, packagedMediaId,
            packagedCodec, packagedFailure );
        const bool residentTableReady = packagedMapped;
        ResidentWem resident { };
        if ( residentTableReady ) {
            AcquireSRWLockShared( &g_residentWemsLock );
            const auto found = g_residentWems.find( packagedMediaId );
            if ( found != g_residentWems.end( ) )
                resident = found->second;
            ReleaseSRWLockShared( &g_residentWemsLock );
        }
        const bool packagedMemoryReady = resident.memory && resident.size != 0;
        if ( packagedMapped && !residentTableReady )
            packagedFailure = "memory-route-not-ready";
        else if ( packagedMapped && !g_packagedMediaSetterHookCreated )
            packagedFailure = "memory-setter-hook-unavailable";
        else if ( packagedMapped && !packagedMemoryReady )
            packagedFailure = "resident-wem-unavailable";

        void * managedWwiseEvent = request.wwiseEventObject;
        if ( !managedWwiseEvent && request.event [ 0 ] )
            managedWwiseEvent = CreateManagedString( request.event );

        if ( packagedMapped && packagedMemoryReady &&
            g_packagedMediaSetterHookCreated && managedWwiseEvent &&
            g_voicePlayerPlayExternal && threadContext ) {
            const std::string placeholder =
                "efstartchange://packaged-media/" +
                std::to_string( packagedMediaId ) + ".wem";
            void * managedPlaceholder = CreateManagedString( placeholder );
            if ( managedPlaceholder ) {
                PackagedMediaSetterContext route;
                route.active = true;
                route.target = request.target;
                route.eventId = packagedEventId;
                route.mediaId = packagedMediaId;
                route.expectedCodec = packagedCodec;
                route.memory = resident.memory;
                route.memorySize = resident.size;
                std::snprintf(
                    route.voiceId, sizeof( route.voiceId ), "%s",
                    request.data [ 0 ] ? request.data : "<empty>" );
                std::uint32_t packagedPlayingId = 0;
                const bool callCompleted = TryPlayMemoryExternalVoice(
                    route, managedPlaceholder, managedWwiseEvent,
                    audioObjectId, handleId, packagedCodec,
                    packagedPlayingId );
                const PackagedMediaSetterContext result = route;

                if ( PackagedMediaLogAllowed( ) ) {
                    Log( "[voice-memory-v9] submit voiceId=" +
                        std::string( request.data [ 0 ]
                            ? request.data : "<empty>" ) +
                        " eventId=" + std::to_string( packagedEventId ) +
                        " mediaId=" + std::to_string( packagedMediaId ) +
                        " callCompleted=" +
                            ( callCompleted ? "true" : "false" ) +
                        " setterObserved=" +
                            ( result.setterObserved ? "true" : "false" ) +
                        " setterRouted=" +
                            ( result.setterRouted ? "true" : "false" ) +
                        " bytes=" + std::to_string( result.memorySize ) +
                        " playingId=" +
                            std::to_string( packagedPlayingId ) );
                }
                if ( callCompleted && result.setterObserved &&
                    result.setterRouted && packagedPlayingId != 0 ) {
                    return packagedPlayingId;
                }
                packagedFailure = !callCompleted
                    ? "memory-submit-exception-or-swap-failed"
                    : ( !result.setterObserved
                        ? "memory-setter-not-observed"
                        : ( !result.setterRouted
                            ? "memory-setter-not-routed"
                            : "memory-submit-returned-zero" ) );
            }
            else {
                packagedFailure = "placeholder-string-failed";
            }
        }
        else if ( packagedMapped && !managedWwiseEvent ) {
            packagedFailure = "managed-wwise-event-unavailable";
        }

        if ( matched && PackagedMediaLogAllowed( ) ) {
            Log( "[voice-memory-v9] fallback-to-v6 voiceId=" +
                std::string( request.data [ 0 ]
                    ? request.data : "<empty>" ) +
                " mapped=" + ( packagedMapped ? "true" : "false" ) +
                " memoryReady=" +
                    ( packagedMemoryReady ? "true" : "false" ) +
                " reason=" + ( packagedFailure.empty( )
                    ? "media-map-not-applicable" : packagedFailure ) );
        }

        std::uint32_t bankEventId = 0;
        std::string cachedWem;
        std::string fallbackReason;
        const bool cacheResolved = matched && ResolveCachedBankWem(
            request, handleId, bankEventId, cachedWem, fallbackReason );
        void * managedWem = nullptr;
        if ( cacheResolved && g_voicePlayerPlayExternal ) {
            managedWem = CreateManagedString( cachedWem );
            if ( !managedWem )
                fallbackReason = "managed-source-string-failed";
            else if ( !managedWwiseEvent )
                fallbackReason = "managed-wwise-event-unavailable";
        }
        else if ( cacheResolved && !g_voicePlayerPlayExternal ) {
            fallbackReason = "play-external-unavailable";
        }

        if ( matched && DiagnosticLogAllowed( ) ) {
            Log( "[voice-bank-external] enter matched=" +
                std::string( request.matchedIdentity ) +
                " requested=" + LanguageName( request.target ) +
                " event=" + ( eventBuffer [ 0 ]
                    ? Normalize( eventBuffer ) : std::string( "<unreadable>" ) ) +
                " selectedEvent=" + ( request.event [ 0 ]
                    ? std::string( request.event ) : "<empty>" ) +
                " eventId=" + std::to_string( bankEventId ) +
                " codec=" + std::to_string( request.codec ) +
                " cache=" + ( cacheResolved ? cachedWem : "<unavailable>" ) +
                " audioObjectId=" + std::to_string( audioObjectId ) +
                " handleId=" + std::to_string( handleId ) );
        }

        if ( managedWem && managedWwiseEvent && g_voicePlayerPlayExternal ) {
            std::uint32_t externalPlayingId = 0;
            threadContext->bankEventRouted = true;
            bool callCompleted = false;
            callCompleted = TryPlayExternalVoice(
                managedWem, managedWwiseEvent, audioObjectId,
                handleId, request.codec, &externalPlayingId );
            threadContext->bankEventRouted = previousBankEventRouted;

            if ( callCompleted && externalPlayingId != 0 ) {
                if ( DiagnosticLogAllowed( ) )
                    Log( "[voice-bank-external] routed playingId=" +
                        std::to_string( externalPlayingId ) +
                        " eventId=" + std::to_string( bankEventId ) +
                        " source=" + cachedWem );
                return externalPlayingId;
            }
            fallbackReason = callCompleted
                ? "play-external-returned-zero" : "play-external-exception";
        }

        threadContext->bankEventRouted = false;
        const std::uint32_t originalPlayingId =
            g_originalVoicePlayerPlayEvent(
                eventName, audioObjectId, handleId, method );
        threadContext->bankEventRouted = previousBankEventRouted;
        if ( matched && DiagnosticLogAllowed( ) ) {
            Log( "[voice-bank-external] fallback reason=" +
                ( fallbackReason.empty( )
                    ? std::string( "cache-not-resolved" ) : fallbackReason ) +
                " originalPlayingId=" +
                    std::to_string( originalPlayingId ) +
                " eventId=" + std::to_string( bankEventId ) );
        }
        return originalPlayingId;
    }

    static std::uint32_t __fastcall HookAudioAdapterPostEventInternal(
        std::uint32_t eventId, std::uint64_t audioObjectId,
        void * externalSourceKey, std::uint32_t externalCookie,
        std::uint32_t callbackType, void * callback, void * cookie,
        std::uint32_t codec ) {
        char sourceBuffer [ 512 ] = { 0 };
        TryCopyManagedStringObject(
            externalSourceKey, sourceBuffer, sizeof( sourceBuffer ) );
        const std::string source = Normalize( sourceBuffer );
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        const bool throughStringHook = threadContext &&
            threadContext->inExternalSourceSubmit;

        int targetLanguage = FOLLOW_GLOBAL_LANGUAGE;
        std::string matchedIdentity;
        const bool matched = ResolveLanguage(
            std::string( ), source, std::string( ),
            targetLanguage, matchedIdentity ) &&
            targetLanguage != FOLLOW_GLOBAL_LANGUAGE;
        std::string diagnosticReplacement;
        const bool replacementPathRecognized = matched &&
            BuildVoiceReplacementSource(
                source, targetLanguage, diagnosticReplacement );

        if ( DiagnosticLogAllowed( ) ) {
            Log( "[voice-internal] enter eventId=" +
                std::to_string( eventId ) +
                " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                " throughStringHook=" +
                    ( throughStringHook ? "true" : "false" ) +
                " matched=" + ( matched ? "true" : "false" ) +
                " identity=" + ( matchedIdentity.empty( )
                    ? std::string( "<empty>" ) : matchedIdentity ) +
                " target=" + LanguageName( targetLanguage ) +
                " pathRecognized=" +
                    ( replacementPathRecognized ? "true" : "false" ) +
                " externalSource=" + ( source.empty( )
                    ? std::string( "<empty>" ) : source ) +
                " externalCookie=" + std::to_string( externalCookie ) +
                " audioObjectId=" + std::to_string( audioObjectId ) +
                " callbackType=" + std::to_string( callbackType ) +
                " codec=" + std::to_string( codec ) );
        }

        const std::uint32_t result =
            g_originalAudioAdapterPostEventInternal(
                eventId, audioObjectId, externalSourceKey, externalCookie,
                callbackType, callback, cookie, codec );

        if ( !throughStringHook ) {
            if ( matched && DiagnosticLogAllowed( ) ) {
                Log( "[voice-gap] internal external-source submit bypassed "
                    "PostEventExternal(string) playingId=" +
                    std::to_string( result ) +
                    " matched=" + matchedIdentity +
                    " requested=" + LanguageName( targetLanguage ) +
                    " source=" + ( source.empty( )
                        ? std::string( "<empty>" ) : source ) +
                    " wouldReplace=" + ( replacementPathRecognized
                        ? diagnosticReplacement
                        : std::string( "<unsupported-voice-path>" ) ) );
            }
            RecordPlaybackStart(
                result, audioObjectId, externalCookie, callbackType, codec,
                "eventId:" + std::to_string( eventId ), source, source,
                matched, false, true, targetLanguage, matchedIdentity );
        }
        return result;
    }

    static std::uint32_t __fastcall HookAudioAdapterPostEventString(
        void * eventName, std::uint64_t audioObjectId,
        std::uint32_t callbackType, void * callback, void * cookie ) {
        void * caller = _ReturnAddress( );
        const std::uint32_t hit = g_normalPostStringHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        VoiceRequestDiagnosticContext request;
        const char * scope = "none";
        std::uint64_t ageMs = 0;
        const bool relevant = TryGetDiagnosticVoiceRequest(
            request, scope, ageMs ) && request.scopeActive;
        char eventBuffer [ 192 ] = { 0 };
        if ( relevant )
            TryCopyManagedStringObject(
                eventName, eventBuffer, sizeof( eventBuffer ) );

        ThreadRoutingContext * threadContext = GetThreadRoutingContext( );
        const bool previous = threadContext &&
            threadContext->inNormalPostEventString;
        if ( threadContext )
            threadContext->inNormalPostEventString = true;
        if ( relevant && DiagnosticLogAllowed( ) ) {
            Log( "[voice-bank] post-string enter hit=" +
                std::to_string( hit ) +
                " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                " caller=" + FormatCallerAddress( caller ) +
                " requestScope=" + scope +
                " requestAgeMs=" + std::to_string( ageMs ) +
                " matched=" + request.matchedIdentity +
                " requested=" + LanguageName( request.target ) +
                " event=" + ( eventBuffer [ 0 ]
                    ? Normalize( eventBuffer ) : std::string( "<unreadable>" ) ) +
                " selectedEvent=" + ( request.event [ 0 ]
                    ? std::string( request.event ) : "<empty>" ) +
                " data=" + ( request.data [ 0 ]
                    ? std::string( request.data ) : "<empty>" ) +
                " audioObjectId=" + std::to_string( audioObjectId ) +
                " callbackType=" + std::to_string( callbackType ) );
        }
        const std::uint32_t result = g_originalAudioAdapterPostEventString(
            eventName, audioObjectId, callbackType, callback, cookie );
        if ( threadContext )
            threadContext->inNormalPostEventString = previous;
        if ( relevant && DiagnosticLogAllowed( ) )
            Log( "[voice-bank] post-string leave hit=" +
                std::to_string( hit ) +
                " playingId=" + std::to_string( result ) );
        return result;
    }

    static std::uint32_t __fastcall HookAudioAdapterPostEventId(
        std::uint32_t eventId, std::uint64_t audioObjectId,
        std::uint32_t callbackType, void * callback, void * cookie ) {
        void * caller = _ReturnAddress( );
        const std::uint32_t hit = g_normalPostIdHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        VoiceRequestDiagnosticContext request;
        const char * scope = "none";
        std::uint64_t ageMs = 0;
        const bool relevant = TryGetDiagnosticVoiceRequest(
            request, scope, ageMs ) && request.scopeActive;
        ThreadRoutingContext * threadContext = GetThreadRoutingContext( );
        const bool previous = threadContext &&
            threadContext->inNormalPostEventId;
        if ( threadContext )
            threadContext->inNormalPostEventId = true;
        if ( relevant && DiagnosticLogAllowed( ) ) {
            Log( "[voice-bank] post-id enter hit=" +
                std::to_string( hit ) +
                " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                " caller=" + FormatCallerAddress( caller ) +
                " requestScope=" + scope +
                " requestAgeMs=" + std::to_string( ageMs ) +
                " matched=" + request.matchedIdentity +
                " requested=" + LanguageName( request.target ) +
                " eventId=" + std::to_string( eventId ) +
                " selectedEvent=" + ( request.event [ 0 ]
                    ? std::string( request.event ) : "<empty>" ) +
                " data=" + ( request.data [ 0 ]
                    ? std::string( request.data ) : "<empty>" ) +
                " audioObjectId=" + std::to_string( audioObjectId ) +
                " callbackType=" + std::to_string( callbackType ) );
        }
        const std::uint32_t result = g_originalAudioAdapterPostEventId(
            eventId, audioObjectId, callbackType, callback, cookie );
        if ( threadContext )
            threadContext->inNormalPostEventId = previous;
        if ( relevant && DiagnosticLogAllowed( ) )
            Log( "[voice-bank] post-id leave hit=" +
                std::to_string( hit ) +
                " playingId=" + std::to_string( result ) );
        return result;
    }

    static std::uint32_t __fastcall HookAudioAdapterPostEventNormalInternal(
        std::uint32_t eventId, std::uint64_t audioObjectId,
        std::uint32_t callbackType, void * callback, void * cookie ) {
        void * caller = _ReturnAddress( );
        const std::uint32_t hit = g_normalPostInternalHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        VoiceRequestDiagnosticContext request;
        const char * scope = "none";
        std::uint64_t ageMs = 0;
        const bool relevant = TryGetDiagnosticVoiceRequest(
            request, scope, ageMs ) && request.scopeActive;
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        const bool throughString = threadContext &&
            threadContext->inNormalPostEventString;
        const bool throughId = threadContext &&
            threadContext->inNormalPostEventId;
        const bool bankEventRouted = threadContext &&
            threadContext->bankEventRouted;
        if ( relevant && DiagnosticLogAllowed( ) ) {
            Log( "[voice-bank] internal enter hit=" +
                std::to_string( hit ) +
                " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                " caller=" + FormatCallerAddress( caller ) +
                " requestScope=" + scope +
                " requestAgeMs=" + std::to_string( ageMs ) +
                " throughString=" + ( throughString ? "true" : "false" ) +
                " throughId=" + ( throughId ? "true" : "false" ) +
                " bankEventRouted=" +
                    ( bankEventRouted ? "true" : "false" ) +
                " matched=" + request.matchedIdentity +
                " requested=" + LanguageName( request.target ) +
                " eventId=" + std::to_string( eventId ) +
                " selectedEvent=" + ( request.event [ 0 ]
                    ? std::string( request.event ) : "<empty>" ) +
                " data=" + ( request.data [ 0 ]
                    ? std::string( request.data ) : "<empty>" ) +
                " audioObjectId=" + std::to_string( audioObjectId ) +
                " callbackType=" + std::to_string( callbackType ) );
        }
        const std::uint32_t result =
            g_originalAudioAdapterPostEventNormalInternal(
                eventId, audioObjectId, callbackType, callback, cookie );
        if ( relevant ) {
            RecordPlaybackStart(
                result, audioObjectId, 0, callbackType, 0,
                request.event [ 0 ] ? std::string( request.event )
                    : "eventId:" + std::to_string( eventId ),
                request.data, request.data, true, bankEventRouted, false,
                request.target, request.matchedIdentity );
            if ( DiagnosticLogAllowed( ) )
                Log( "[voice-bank] internal leave hit=" +
                    std::to_string( hit ) +
                    " playingId=" + std::to_string( result ) );
        }
        return result;
    }

    static void __fastcall HookAudioAdapterNormalCallback(
        void * payload, std::int32_t callbackType, void * callbackInfo ) {
        RecordPlaybackCallback(
            payload, callbackType, callbackInfo, "normal", false );
        g_originalAudioAdapterNormalCallback(
            payload, callbackType, callbackInfo );
    }

    static void __fastcall HookAudioAdapterStopByPlayingId(
        std::uint32_t playingId, std::int32_t fadeTimeMs ) {
        void * caller = _ReturnAddress( );
        RecordPlaybackAction(
            0, playingId, fadeTimeMs, "stop-entry", caller, false, false );
        g_originalAudioAdapterStopByPlayingId( playingId, fadeTimeMs );
    }

    static void __fastcall HookVoiceSpeakChannelStop(
        void * channel, void * method ) {
        void * caller = _ReturnAddress( );
        const std::uint32_t handleId = ReadPayloadU32( channel, 0x38 );
        const std::uint64_t audioObjectId = ReadPayloadU64( channel, 0x40 );
        char channelName [ 192 ] = { 0 };
        TryCopyManagedStringField(
            channel, 0x20, channelName, sizeof( channelName ) );
        RecordPlaybackAction(
            0, handleId, 50, "channel-stop", caller, false, false,
            audioObjectId );
        if ( DiagnosticLogAllowed( ) ) {
            Log( "[voice-stop-origin] stage=StopChannel" +
                std::string( " caller=" ) + FormatCallerAddress( caller ) +
                " channel=" + PointerText( channel ) +
                " channelName=" + ( channelName [ 0 ]
                    ? Normalize( channelName ) : std::string( "<unreadable>" ) ) +
                " status=" + std::to_string(
                    ReadPayloadU32( channel, 0x10 ) ) +
                " handleId=" + std::to_string( handleId ) +
                " audioObjectId=" + std::to_string( audioObjectId ) +
                " fadeTimeMs=50" );
        }
        g_originalVoiceSpeakChannelStop( channel, method );
    }

    static void __fastcall HookVoicePlayerStopVoice(
        std::uint32_t handleId, std::int32_t fadeTimeMs, void * method ) {
        void * caller = _ReturnAddress( );
        RecordPlaybackAction(
            0, handleId, fadeTimeMs, "voice-player-stop",
            caller, false, false );
        if ( DiagnosticLogAllowed( ) ) {
            Log( "[voice-stop-origin] stage=VoicePlayer.StopVoice" +
                std::string( " caller=" ) + FormatCallerAddress( caller ) +
                " handleId=" + std::to_string( handleId ) +
                " fadeTimeMs=" + std::to_string( fadeTimeMs ) );
        }
        g_originalVoicePlayerStopVoice( handleId, fadeTimeMs, method );
    }

    static bool __fastcall HookVoiceUtilsTryGetDuration(
        void * voiceId, float * durationSeconds, void * method ) {
        void * caller = _ReturnAddress( );
        char voiceIdBuffer [ 512 ] = { 0 };
        TryCopyManagedStringObject(
            voiceId, voiceIdBuffer, sizeof( voiceIdBuffer ) );
        const std::string normalizedVoiceId = Normalize( voiceIdBuffer );
        int targetLanguage = FOLLOW_GLOBAL_LANGUAGE;
        std::string matchedIdentity;
        const bool matched = ResolveLanguage(
            std::string( ), normalizedVoiceId, std::string( ),
            targetLanguage, matchedIdentity ) &&
            targetLanguage != FOLLOW_GLOBAL_LANGUAGE;

        int globalLanguage = FOLLOW_GLOBAL_LANGUAGE;
        const bool globalLanguageReadable = TryReadCachedLanguage(
            &globalLanguage );
        float globalSeconds = -1.0f;
        float routedSeconds = -1.0f;
        bool overrideApplied = false;
        bool targetDurationFallback = false;
        bool result = false;

        if ( !matched || ( globalLanguageReadable &&
                globalLanguage == targetLanguage ) ) {
            result = g_originalVoiceUtilsTryGetDuration(
                voiceId, durationSeconds, method );
            __try {
                if ( durationSeconds )
                    routedSeconds = *durationSeconds;
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                routedSeconds = -1.0f;
            }
            globalSeconds = routedSeconds;
        }
        else {
            const bool globalResult = g_originalVoiceUtilsTryGetDuration(
                voiceId, &globalSeconds, method );
            ThreadRoutingContext * threadContext =
                GetThreadRoutingContext( );
            const bool languageHookReady =
                EnsureLipSyncHooksOnGameThread( );
            if ( globalResult && languageHookReady && threadContext ) {
                const DurationRoutingContext previous =
                    threadContext->durationRouting;
                threadContext->durationRouting = DurationRoutingContext { };
                threadContext->durationRouting.active = true;
                threadContext->durationRouting.target = targetLanguage;
                strncpy_s( threadContext->durationRouting.voiceId,
                    normalizedVoiceId.c_str( ), _TRUNCATE );
                strncpy_s(
                    threadContext->durationRouting.matchedIdentity,
                    matchedIdentity.c_str( ), _TRUNCATE );
                result = g_originalVoiceUtilsTryGetDuration(
                    voiceId, durationSeconds, method );
                threadContext->durationRouting = previous;
                __try {
                    if ( durationSeconds )
                        routedSeconds = *durationSeconds;
                }
                __except ( EXCEPTION_EXECUTE_HANDLER ) {
                    routedSeconds = -1.0f;
                }
                overrideApplied = true;
            }
            else {
                result = globalResult;
            }

            if ( !result || routedSeconds <= 0.0f ) {
                result = globalResult;
                routedSeconds = globalSeconds;
                targetDurationFallback = matched;
                __try {
                    if ( durationSeconds )
                        *durationSeconds = globalSeconds;
                }
                __except ( EXCEPTION_EXECUTE_HANDLER ) {
                }
            }
        }

        const auto toDurationMs = [ ] ( bool available, float seconds ) {
            return available && seconds > 0.0f
                ? static_cast< std::int32_t >(
                    std::lround( seconds * 1000.0f ) )
                : -1;
        };
        const std::int32_t globalDurationMs = toDurationMs(
            matched && globalSeconds > 0.0f, globalSeconds );
        const std::int32_t routedDurationMs = toDurationMs(
            result, routedSeconds );
        if ( matched && globalDurationMs > 0 )
            RememberGlobalVoiceDuration(
                normalizedVoiceId, globalDurationMs );
        if ( matched && routedDurationMs > 0 )
            RememberConfiguredVoiceDuration(
                normalizedVoiceId, routedDurationMs );
        if ( matched && DiagnosticLogAllowed( ) ) {
            Log( "[voice-duration] caller=" + FormatCallerAddress( caller ) +
                " voiceId=" + ( normalizedVoiceId.empty( )
                    ? std::string( "<unreadable>" ) : normalizedVoiceId ) +
                " matched=" + matchedIdentity +
                " global=" + ( globalLanguageReadable
                    ? std::string( LanguageName( globalLanguage ) ) +
                        "(" + std::to_string( globalLanguage ) + ")"
                    : std::string( "<unreadable>" ) ) +
                " requested=" + LanguageName( targetLanguage ) +
                    "(" + std::to_string( targetLanguage ) + ")" +
                " result=" + ( result ? "true" : "false" ) +
                " globalDurationMs=" +
                    std::to_string( globalDurationMs ) +
                " routedDurationMs=" +
                    std::to_string( routedDurationMs ) +
                " overrideApplied=" +
                    ( overrideApplied ? "true" : "false" ) +
                " targetFallback=" +
                    ( targetDurationFallback ? "true" : "false" ) );
        }
        return result;
    }

    static void __fastcall HookAudioActionQueueExecute(
        std::uint32_t playingId, std::int32_t action,
        std::int32_t durationMs ) {
        void * caller = _ReturnAddress( );
        RecordPlaybackAction(
            action, playingId, durationMs, "queue-enter",
            caller, false, false );
        g_originalAudioActionQueueExecute(
            playingId, action, durationMs );
    }

    static void __fastcall HookAudioActionQueueConsumeExecute(
        std::int32_t currentFrame ) {
        ThreadRoutingContext * threadContext = GetThreadRoutingContext( );
        const bool previousConsuming = threadContext &&
            threadContext->consumingActionQueue;
        const std::int32_t previousFrame = threadContext
            ? threadContext->actionQueueFrame : -1;
        if ( threadContext ) {
            threadContext->consumingActionQueue = true;
            threadContext->actionQueueFrame = currentFrame;
        }
        g_originalAudioActionQueueConsumeExecute( currentFrame );
        if ( threadContext ) {
            threadContext->consumingActionQueue = previousConsuming;
            threadContext->actionQueueFrame = previousFrame;
        }
    }

    static void __fastcall HookAudioAdapterExecuteAction(
        std::int32_t action, std::uint32_t playingId,
        std::int32_t durationMs ) {
        void * caller = _ReturnAddress( );
        RecordPlaybackAction(
            action, playingId, durationMs, "execute",
            caller, true, true );
        g_originalAudioAdapterExecuteAction(
            action, playingId, durationMs );
    }

    static void __fastcall HookAudioAdapterExternalCallback(
        void * payload, std::int32_t callbackType, void * callbackInfo ) {
        RecordPlaybackCallback(
            payload, callbackType, callbackInfo, "external", true );
        g_originalAudioAdapterExternalCallback(
            payload, callbackType, callbackInfo );
    }

    static std::uint32_t __fastcall HookAudioAdapterPostEventExternal(
        void * eventName, std::uint64_t audioObjectId,
        void * externalSourceKey, std::uint32_t externalCookie,
        std::uint32_t callbackType, void * callback, void * cookie,
        std::uint32_t codec ) {
        const std::uint32_t hit = g_submitExternalHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        char eventBuffer [ 192 ] = { 0 };
        char sourceBuffer [ 512 ] = { 0 };
        TryCopyManagedStringObject(
            eventName, eventBuffer, sizeof( eventBuffer ) );
        TryCopyManagedStringObject(
            externalSourceKey, sourceBuffer, sizeof( sourceBuffer ) );
        const std::string event = Normalize( eventBuffer );
        const std::string source = Normalize( sourceBuffer );

        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        VoiceRequestDiagnosticContext bankRequest;
        const bool bankExternal = threadContext &&
            threadContext->bankEventRouted &&
            TryGetActiveBankVoiceRequest( bankRequest );
        int targetLanguage = FOLLOW_GLOBAL_LANGUAGE;
        std::string matchedIdentity;
        if ( bankExternal ) {
            targetLanguage = bankRequest.target;
            matchedIdentity = bankRequest.matchedIdentity;
        }
        const bool narrativeBlocked = IsNarrativeVoiceSource( source ) &&
            !g_replaceNarrativeVoice.load( std::memory_order_acquire );
        const bool matched = bankExternal || ( !narrativeBlocked &&
            ResolveLanguage(
                std::string( ), source, event,
                targetLanguage, matchedIdentity ) &&
            targetLanguage != FOLLOW_GLOBAL_LANGUAGE );
        if ( narrativeBlocked && g_narrativeSkipLogs.fetch_add(
            1, std::memory_order_relaxed ) < DIAGNOSTIC_HIT_LIMIT ) {
            Log( "[voice-route] narrative replacement disabled; preserved source=" +
                ( source.empty( )
                    ? std::string( "<empty>" ) : source ) );
        }
        bool logMatch = false;
        std::string replacementSource;
        void * routedExternalSourceKey = externalSourceKey;
        bool sourceReplaced = bankExternal;
        const bool replacementPathRecognized = !bankExternal && matched &&
            BuildVoiceReplacementSource(
                source, targetLanguage, replacementSource );
        const bool replacementCandidate = replacementPathRecognized &&
            replacementSource != source;
        const bool packageReady = !replacementCandidate ||
            EnsureLanguagePackageReady( targetLanguage );
        if ( replacementCandidate && packageReady ) {
            routedExternalSourceKey = CreateManagedString( replacementSource );
            sourceReplaced = routedExternalSourceKey != nullptr;
        }
        bool lipArmed = false;
        if ( IsNarrativeVoiceSource( source ) ) {
            if ( matched && sourceReplaced ) {
                lipArmed = ArmPendingLipRoute(
                    targetLanguage, matchedIdentity, source );
            }
            else {
                ClearPendingLipRoute(
                    GetThreadRoutingContext( false ) );
            }
        }
        if ( matched ) {
            logMatch = g_submitExternalMatchLogs.fetch_add(
                1, std::memory_order_relaxed ) <
                VOICE_SUBMIT_MATCH_LOG_LIMIT;
            if ( logMatch ) {
                int cachedLanguage = FOLLOW_GLOBAL_LANGUAGE;
                const bool cacheReadable = TryReadCachedLanguage(
                    &cachedLanguage );
                std::string wwiseLanguage;
                const bool wwiseReadable = TryReadWwiseLanguage(
                    wwiseLanguage );
                Log( "[voice-submit] enter hit=" + std::to_string( hit ) +
                    " thread=" + std::to_string( GetCurrentThreadId( ) ) +
                    " matched=" + matchedIdentity +
                    " requested=" + LanguageName( targetLanguage ) +
                    "(" + std::to_string( targetLanguage ) + ")" +
                    " event=" + ( event.empty( )
                        ? std::string( "<empty>" ) : event ) +
                    " externalSource=" + ( source.empty( )
                        ? std::string( "<empty>" ) : source ) +
                    " audioObjectId=" + std::to_string( audioObjectId ) +
                    " externalCookie=" + std::to_string( externalCookie ) +
                    " callbackType=" + std::to_string( callbackType ) +
                    " codec=" + std::to_string( codec ) +
                    " packageReady=" + ( packageReady ? "true" : "false" ) +
                    " lipArmed=" + ( lipArmed ? "true" : "false" ) +
                    " cache=" + ( cacheReadable
                        ? std::string( LanguageName( cachedLanguage ) ) +
                            "(" + std::to_string( cachedLanguage ) + ")"
                        : std::string( "<unreadable>" ) ) +
                    " wwise=" + ( wwiseReadable
                        ? wwiseLanguage : std::string( "<unreadable>" ) ) );
            }

            if ( logMatch ) {
                if ( bankExternal ) {
                    Log( "[voice-bank-external] submit source=" + source +
                        " selectedEvent=" + ( bankRequest.event [ 0 ]
                            ? std::string( bankRequest.event ) : "<empty>" ) +
                        " data=" + ( bankRequest.data [ 0 ]
                            ? std::string( bankRequest.data ) : "<empty>" ) +
                        " audioObjectId=" + std::to_string( audioObjectId ) );
                }
                if ( replacementCandidate && sourceReplaced ) {
                    Log( "[voice-replace] matched=" + matchedIdentity +
                        " originalSource=" +
                        ( source.empty( ) ? std::string( "<empty>" ) : source ) +
                        " replacementSource=" + replacementSource );
                }
                else if ( replacementCandidate && !packageReady ) {
                    Log( "[voice-replace] matched=" + matchedIdentity +
                        " originalSource=" +
                        ( source.empty( ) ? std::string( "<empty>" ) : source ) +
                        " replacementSource=<target-package-unavailable; original-preserved>" );
                }
                else if ( replacementCandidate ) {
                    Log( "[voice-replace] matched=" + matchedIdentity +
                        " originalSource=" +
                        ( source.empty( ) ? std::string( "<empty>" ) : source ) +
                        " replacementSource=<string-new-failed>" );
                }
                else if ( replacementPathRecognized ) {
                    Log( "[voice-replace] matched=" + matchedIdentity +
                        " originalSource=" +
                        ( source.empty( ) ? std::string( "<empty>" ) : source ) +
                        " replacementSource=<same-as-original>" );
                }
                else {
                    Log( "[voice-replace] matched=" + matchedIdentity +
                        " originalSource=" +
                        ( source.empty( ) ? std::string( "<empty>" ) : source ) +
                        " replacementSource=<unsupported-voice-path>" );
                }
            }
        }

        if ( !threadContext )
            threadContext = GetThreadRoutingContext( );
        const bool previousExternalSubmit = threadContext &&
            threadContext->inExternalSourceSubmit;
        if ( threadContext )
            threadContext->inExternalSourceSubmit = true;
        const std::uint32_t result =
            g_originalAudioAdapterPostEventExternal(
                eventName, audioObjectId, routedExternalSourceKey, externalCookie,
                callbackType, callback, cookie, codec );
        if ( threadContext )
            threadContext->inExternalSourceSubmit = previousExternalSubmit;
        const std::string traceOriginalSource = bankExternal &&
            bankRequest.data [ 0 ]
            ? std::string( bankRequest.data ) : source;
        const std::string traceSubmittedSource = bankExternal
            ? source : ( sourceReplaced ? replacementSource : source );
        RecordPlaybackStart(
            result, audioObjectId, externalCookie, callbackType, codec,
            event, traceOriginalSource, traceSubmittedSource,
            matched, sourceReplaced, bankExternal,
            targetLanguage, matchedIdentity );
        if ( lipArmed && result == 0 )
            ClearPendingLipRoute( GetThreadRoutingContext( false ) );
        if ( matched && logMatch )
            Log( "[voice-submit] leave hit=" + std::to_string( hit ) +
                " matched=" + matchedIdentity +
                " playingId=" + std::to_string( result ) );
        return result;
    }

    static void __fastcall HookVoicePlayerPlayVoice( void * voiceContext ) {
        const std::uint32_t hit = g_playVoiceHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        const bool routed = BeginContextRouting(
            voiceContext, "VoicePlayer.PlayVoice", hit );
        VoiceRequestDiagnosticContext previousRequest;
        const bool diagnosticRequestArmed = ArmDiagnosticVoiceRequest(
            voiceContext, previousRequest );
        g_originalVoicePlayerPlayVoice( voiceContext );
        FinishDiagnosticVoiceRequest(
            diagnosticRequestArmed, previousRequest );
        EndRouting( routed );
    }

    static std::uint32_t __fastcall HookVoicePlayerPlayVoiceInternal(
        void * voiceContextReference ) {
        const std::uint32_t hit = g_internalPlayVoiceHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        void * voiceContext = TryDereferenceVoiceContext(
            voiceContextReference );
        const bool routed = BeginContextRouting(
            voiceContext, "VoicePlayer._PlayVoice(ref)", hit );
        const std::uint32_t result = g_originalVoicePlayerPlayVoiceInternal(
            voiceContextReference );
        EndRouting( routed );
        return result;
    }

    static std::uint32_t __fastcall HookVoiceManagerSpeakNarrative(
        void * self, void * voiceId, std::uint64_t audioObjectId,
        void * config ) {
        EnsureLipSyncHooksOnGameThread( );
        const std::uint32_t hit = g_narrativeHits.fetch_add(
            1, std::memory_order_relaxed ) + 1;
        const bool routed = BeginStringRouting(
            voiceId, "VoiceManager._SpeakNarrative", hit );
        const std::uint32_t result = g_originalVoiceManagerSpeakNarrative(
            self, voiceId, audioObjectId, config );
        EndRouting( routed );
        return result;
    }

    static void RemoveVoiceHooks( ) {
        void * targets [ ] = {
            g_voiceManagerSpeakStringTarget,
            g_voiceManagerSpeakTarget,
            g_voiceSpeakChannelPlayVoiceTarget,
            g_voiceUtilsSelectWwiseEventTarget,
            g_audioAdapterPostEventExternalTarget,
            g_audioAdapterPostEventInternalTarget,
            g_audioAdapterPostEventStringTarget,
            g_audioAdapterPostEventIdTarget,
            g_audioAdapterPostEventNormalInternalTarget,
            g_audioAdapterNormalCallbackTarget,
            g_audioAdapterStopByPlayingIdTarget,
            g_audioAdapterExecuteActionTarget,
            g_audioActionQueueExecuteTarget,
            g_audioActionQueueConsumeExecuteTarget,
            g_audioAdapterExternalCallbackTarget,
            g_voicePlayerPlayVoiceTarget,
            g_voicePlayerPlayVoiceInternalTarget,
            g_voicePlayerPlayEventTarget,
            g_voiceSpeakChannelStopTarget,
            g_voicePlayerStopVoiceTarget,
            g_voiceUtilsTryGetDurationTarget,
            g_voiceManagerSpeakNarrativeTarget,
            g_akSoundEngineLoadFilePackageTarget,
            g_akSoundEngineUnloadFilePackageTarget,
            g_akExternalSourceFileSetterTarget
        };
        for ( void * target : targets ) {
            if ( !target )
                continue;
            MH_DisableHook( target );
            MH_RemoveHook( target );
        }
        g_lifecycleInternalHookCreated = false;
        g_lifecycleActionHookCreated = false;
        g_lifecycleCallbackHookCreated = false;
        g_bankPostStringHookCreated = false;
        g_bankPostIdHookCreated = false;
        g_bankPostInternalHookCreated = false;
        g_bankCallbackHookCreated = false;
        g_bankRouteHookCreated = false;
        g_stopEntryHookCreated = false;
        g_actionQueueHookCreated = false;
        g_actionQueueConsumeHookCreated = false;
        g_channelStopHookCreated = false;
        g_voiceStopHookCreated = false;
        g_durationHookCreated = false;
        g_packagedMediaSetterHookCreated = false;
    }

    static bool ResetVoiceHookPointers( ) {
        if ( !ClearNativeMediaRoutes( ) )
            return false;
        g_voiceManagerSpeakStringTarget = nullptr;
        g_voiceManagerSpeakTarget = nullptr;
        g_voiceSpeakChannelPlayVoiceTarget = nullptr;
        g_voiceUtilsSelectWwiseEventTarget = nullptr;
        g_audioAdapterPostEventExternalTarget = nullptr;
        g_audioAdapterPostEventInternalTarget = nullptr;
        g_audioAdapterPostEventStringTarget = nullptr;
        g_audioAdapterPostEventIdTarget = nullptr;
        g_audioAdapterPostEventNormalInternalTarget = nullptr;
        g_audioAdapterNormalCallbackTarget = nullptr;
        g_audioAdapterStopByPlayingIdTarget = nullptr;
        g_audioAdapterExecuteActionTarget = nullptr;
        g_audioActionQueueExecuteTarget = nullptr;
        g_audioActionQueueConsumeExecuteTarget = nullptr;
        g_audioAdapterExternalCallbackTarget = nullptr;
        g_voicePlayerPlayVoiceTarget = nullptr;
        g_voicePlayerPlayVoiceInternalTarget = nullptr;
        g_voicePlayerPlayExternalTarget = nullptr;
        g_voicePlayerPlayEventTarget = nullptr;
        g_voiceSpeakChannelStopTarget = nullptr;
        g_voicePlayerStopVoiceTarget = nullptr;
        g_voiceUtilsTryGetDurationTarget = nullptr;
        g_voiceManagerSpeakNarrativeTarget = nullptr;
        g_akSoundEngineLoadFilePackageTarget = nullptr;
        g_akSoundEngineUnloadFilePackageTarget = nullptr;
        g_akExternalSourceFileSetterTarget = nullptr;
        g_originalVoiceManagerSpeakString = nullptr;
        g_originalVoiceManagerSpeak = nullptr;
        g_originalVoiceSpeakChannelPlayVoice = nullptr;
        g_originalVoiceUtilsSelectWwiseEvent = nullptr;
        g_originalAudioAdapterPostEventExternal = nullptr;
        g_originalAudioAdapterPostEventInternal = nullptr;
        g_originalAudioAdapterPostEventString = nullptr;
        g_originalAudioAdapterPostEventId = nullptr;
        g_originalAudioAdapterPostEventNormalInternal = nullptr;
        g_originalAudioAdapterNormalCallback = nullptr;
        g_originalAudioAdapterStopByPlayingId = nullptr;
        g_originalAudioAdapterExecuteAction = nullptr;
        g_originalAudioActionQueueExecute = nullptr;
        g_originalAudioActionQueueConsumeExecute = nullptr;
        g_originalAudioAdapterExternalCallback = nullptr;
        g_originalVoicePlayerPlayVoice = nullptr;
        g_originalVoicePlayerPlayVoiceInternal = nullptr;
        g_voicePlayerPlayExternal = nullptr;
        g_originalVoicePlayerPlayEvent = nullptr;
        g_originalVoiceSpeakChannelStop = nullptr;
        g_originalVoicePlayerStopVoice = nullptr;
        g_originalVoiceUtilsTryGetDuration = nullptr;
        g_originalVoiceManagerSpeakNarrative = nullptr;
        g_tryLoadLanguagePck = nullptr;
        g_originalAkSoundEngineLoadFilePackage = nullptr;
        g_originalAkSoundEngineUnloadFilePackage = nullptr;
        g_originalAkExternalSourceFileSetter = nullptr;
        g_akExternalSourceMemorySetter = nullptr;
        g_akExternalSourceMemorySizeSetter = nullptr;
        g_setLanguage = nullptr;
        g_getCurrentLanguage = nullptr;
        g_getLanguageName = nullptr;
        g_getWwiseCurrentLanguage = nullptr;
        g_setMedia = nullptr;
        g_unsetMedia = nullptr;
        g_tryGetRealPlayingId = nullptr;
        g_getSourcePlayPosition = nullptr;
        g_getCallbackPlayingId = nullptr;
        g_il2cppStringNew = nullptr;
        g_gameAssemblyBase = 0;
        g_gameAssemblySize = 0;
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        if ( threadContext )
            *threadContext = ThreadRoutingContext { };
        g_auxiliaryPackageLoad.store( false, std::memory_order_release );
        g_auxiliaryPackageLanguage.store(
            FOLLOW_GLOBAL_LANGUAGE, std::memory_order_release );
        g_packagedMediaSetterHookCreated = false;
        g_nativeMediaRouteReady.store( false, std::memory_order_release );
        g_playVoiceHookBytesCaptured = false;
        g_healthReported.store( false, std::memory_order_release );
        g_healthRepairAttempted.store( false, std::memory_order_release );
        return true;
    }

    static bool CreateVoiceHook(
        void * target, void * detour, void ** original, const char * name ) {
        const MH_STATUS status = MH_CreateHook( target, detour, original );
        if ( status == MH_OK )
            return true;
        Log( "[voice-lang] MH_CreateHook(" + std::string( name ) +
            ") failed: " + std::to_string( static_cast< int >( status ) ) );
        return false;
    }

    static bool EnableVoiceHook( void * target, const char * name ) {
        const MH_STATUS status = MH_EnableHook( target );
        if ( status == MH_OK )
            return true;
        Log( "[voice-lang] MH_EnableHook(" + std::string( name ) +
            ") failed: " + std::to_string( static_cast< int >( status ) ) );
        return false;
    }

    static bool InstallPackagedMediaSetterHook( uintptr_t base ) {
        ( void ) base;
        api::init( );
        void * attachedThread = nullptr;
        if ( api::thread_current && api::thread_attach && api::get_domain &&
            !api::thread_current( ) )
            attachedThread = api::thread_attach( api::get_domain( ) );
        void * wwiseImage = FindImageByName( "AK.Wwise.Unity.API.dll" );
        void * externalClass = wwiseImage && api::class_from_name
            ? api::class_from_name(
                wwiseImage, "", "AkExternalSourceInfo" ) : nullptr;
        void * fileMethod = externalClass && api::class_get_method_from_name
            ? api::class_get_method_from_name(
                externalClass, "set_szFile", 1 ) : nullptr;
        void * memoryMethod = externalClass && api::class_get_method_from_name
            ? api::class_get_method_from_name(
                externalClass, "set_pInMemory", 1 ) : nullptr;
        void * sizeMethod = externalClass && api::class_get_method_from_name
            ? api::class_get_method_from_name(
                externalClass, "set_uiMemorySize", 1 ) : nullptr;
        const char * source = "none";
        g_akExternalSourceFileSetterTarget = ReadGameplayMethodEntry(
            fileMethod, source );
        void * memorySetter = ReadGameplayMethodEntry(
            memoryMethod, source );
        void * sizeSetter = ReadGameplayMethodEntry(
            sizeMethod, source );
        if ( !g_akExternalSourceFileSetterTarget || !memorySetter ||
            !sizeSetter ) {
            Log( "[voice-memory-v9] AkExternalSourceInfo setters unavailable file=" +
                std::string( g_akExternalSourceFileSetterTarget
                    ? "true" : "false" ) + " memory=" +
                ( memorySetter ? "true" : "false" ) + " size=" +
                ( sizeSetter ? "true" : "false" ) +
                "; v6 fallback retained" );
            g_akExternalSourceFileSetterTarget = nullptr;
            if ( attachedThread && api::thread_detach )
                api::thread_detach( attachedThread );
            return false;
        }

        g_akExternalSourceMemorySetter =
            reinterpret_cast< AkExternalSourceMemorySetterFn >(
                memorySetter );
        g_akExternalSourceMemorySizeSetter =
            reinterpret_cast< AkExternalSourceMemorySizeSetterFn >(
                sizeSetter );
        if ( attachedThread && api::thread_detach )
            api::thread_detach( attachedThread );
        const bool created = CreateVoiceHook(
            g_akExternalSourceFileSetterTarget,
            reinterpret_cast< void * >( &HookAkExternalSourceFileSetter ),
            reinterpret_cast< void ** >(
                &g_originalAkExternalSourceFileSetter ),
            "AkExternalSourceInfo.set_szFile" );
        const bool enabled = created && EnableVoiceHook(
            g_akExternalSourceFileSetterTarget,
            "AkExternalSourceInfo.set_szFile" );
        if ( !enabled ) {
            if ( created ) {
                MH_DisableHook( g_akExternalSourceFileSetterTarget );
                MH_RemoveHook( g_akExternalSourceFileSetterTarget );
            }
            g_akExternalSourceFileSetterTarget = nullptr;
            g_originalAkExternalSourceFileSetter = nullptr;
            g_akExternalSourceMemorySetter = nullptr;
            g_akExternalSourceMemorySizeSetter = nullptr;
            Log( "[voice-memory-v9] setter hook unavailable; v6 fallback retained" );
            return false;
        }

        g_packagedMediaSetterHookCreated = true;
        Log( "[voice-memory-v9] in-memory setters active" );
        return true;
    }

    static bool ResolveNativeMediaApi( ) {
        api::init( );
        void * attachedThread = nullptr;
        if ( api::thread_current && api::thread_attach && api::get_domain &&
            !api::thread_current( ) )
            attachedThread = api::thread_attach( api::get_domain( ) );
        void * wwiseImage = FindImageByName( "AK.Wwise.Unity.API.dll" );
        void * pinvokeClass = wwiseImage && api::class_from_name
            ? api::class_from_name(
                wwiseImage, "", "AkSoundEnginePINVOKE" ) : nullptr;
        void * setMethod = pinvokeClass && api::class_get_method_from_name
            ? api::class_get_method_from_name(
                pinvokeClass, "CSharp_SetMedia", 2 ) : nullptr;
        void * unsetMethod = pinvokeClass && api::class_get_method_from_name
            ? api::class_get_method_from_name(
                pinvokeClass, "CSharp_UnsetMedia", 2 ) : nullptr;
        const char * source = "none";
        void * setEntry = ReadGameplayMethodEntry( setMethod, source );
        void * unsetEntry = ReadGameplayMethodEntry( unsetMethod, source );
        if ( attachedThread && api::thread_detach )
            api::thread_detach( attachedThread );
        if ( !setEntry || !unsetEntry ) {
            g_setMedia = nullptr;
            g_unsetMedia = nullptr;
            Log( "[voice-native] AkSoundEngine SetMedia API unavailable; "
                "v9 fallback retained" );
            return false;
        }
        g_setMedia = reinterpret_cast< AkSoundEngineSetMediaFn >( setEntry );
        g_unsetMedia = reinterpret_cast< AkSoundEngineUnsetMediaFn >(
            unsetEntry );
        Log( "[voice-native] SetMedia API resolved dynamically" );
        return true;
    }

    static bool InstallBankEventDiagnosticHooks( uintptr_t base ) {
        g_audioAdapterPostEventStringTarget = reinterpret_cast< void * >(
            base + AUDIO_ADAPTER_POST_EVENT_STRING_RVA );
        g_audioAdapterPostEventIdTarget = reinterpret_cast< void * >(
            base + AUDIO_ADAPTER_POST_EVENT_ID_RVA );
        g_audioAdapterPostEventNormalInternalTarget =
            reinterpret_cast< void * >(
                base + AUDIO_ADAPTER_POST_EVENT_NORMAL_INTERNAL_RVA );
        g_audioAdapterNormalCallbackTarget = reinterpret_cast< void * >(
            base + AUDIO_ADAPTER_NORMAL_CALLBACK_RVA );

        const bool signaturesMatch =
            MatchesSignature(
                g_audioAdapterPostEventStringTarget,
                AUDIO_ADAPTER_POST_EVENT_STRING_SIGNATURE,
                sizeof( AUDIO_ADAPTER_POST_EVENT_STRING_SIGNATURE ) ) &&
            MatchesSignature(
                g_audioAdapterPostEventIdTarget,
                AUDIO_ADAPTER_POST_EVENT_ID_SIGNATURE,
                sizeof( AUDIO_ADAPTER_POST_EVENT_ID_SIGNATURE ) ) &&
            MatchesSignature(
                g_audioAdapterPostEventNormalInternalTarget,
                AUDIO_ADAPTER_POST_EVENT_NORMAL_INTERNAL_SIGNATURE,
                sizeof(
                    AUDIO_ADAPTER_POST_EVENT_NORMAL_INTERNAL_SIGNATURE ) ) &&
            MatchesSignature(
                g_audioAdapterNormalCallbackTarget,
                AUDIO_ADAPTER_NORMAL_CALLBACK_SIGNATURE,
                sizeof( AUDIO_ADAPTER_NORMAL_CALLBACK_SIGNATURE ) );
        if ( !signaturesMatch ) {
            Log( "[voice-bank] normal PostEvent diagnostic signature "
                "mismatch; banked-voice hooks unavailable" );
            g_audioAdapterPostEventStringTarget = nullptr;
            g_audioAdapterPostEventIdTarget = nullptr;
            g_audioAdapterPostEventNormalInternalTarget = nullptr;
            g_audioAdapterNormalCallbackTarget = nullptr;
            return false;
        }

        g_bankPostStringHookCreated = CreateVoiceHook(
            g_audioAdapterPostEventStringTarget,
            reinterpret_cast< void * >(
                &HookAudioAdapterPostEventString ),
            reinterpret_cast< void ** >(
                &g_originalAudioAdapterPostEventString ),
            "AudioAdapter.PostEvent(string, object, callback)" );
        if ( g_bankPostStringHookCreated )
            g_bankPostIdHookCreated = CreateVoiceHook(
                g_audioAdapterPostEventIdTarget,
                reinterpret_cast< void * >(
                    &HookAudioAdapterPostEventId ),
                reinterpret_cast< void ** >(
                    &g_originalAudioAdapterPostEventId ),
                "AudioAdapter.PostEvent(id, object, callback)" );
        if ( g_bankPostIdHookCreated )
            g_bankPostInternalHookCreated = CreateVoiceHook(
                g_audioAdapterPostEventNormalInternalTarget,
                reinterpret_cast< void * >(
                    &HookAudioAdapterPostEventNormalInternal ),
                reinterpret_cast< void ** >(
                    &g_originalAudioAdapterPostEventNormalInternal ),
                "AudioAdapter._PostEvent" );
        if ( g_bankPostInternalHookCreated )
            g_bankCallbackHookCreated = CreateVoiceHook(
                g_audioAdapterNormalCallbackTarget,
                reinterpret_cast< void * >(
                    &HookAudioAdapterNormalCallback ),
                reinterpret_cast< void ** >(
                    &g_originalAudioAdapterNormalCallback ),
                "AudioAdapter._OnEventCallback" );

        const bool created = g_bankPostStringHookCreated &&
            g_bankPostIdHookCreated && g_bankPostInternalHookCreated &&
            g_bankCallbackHookCreated;
        const bool enabled = created &&
            EnableVoiceHook(
                g_audioAdapterPostEventStringTarget,
                "AudioAdapter.PostEvent(string, object, callback)" ) &&
            EnableVoiceHook(
                g_audioAdapterPostEventIdTarget,
                "AudioAdapter.PostEvent(id, object, callback)" ) &&
            EnableVoiceHook(
                g_audioAdapterPostEventNormalInternalTarget,
                "AudioAdapter._PostEvent" ) &&
            EnableVoiceHook(
                g_audioAdapterNormalCallbackTarget,
                "AudioAdapter._OnEventCallback" );
        if ( !enabled ) {
            void * targets [ ] = {
                g_audioAdapterPostEventStringTarget,
                g_audioAdapterPostEventIdTarget,
                g_audioAdapterPostEventNormalInternalTarget,
                g_audioAdapterNormalCallbackTarget
            };
            for ( void * target : targets ) {
                MH_DisableHook( target );
                MH_RemoveHook( target );
            }
            g_bankPostStringHookCreated = false;
            g_bankPostIdHookCreated = false;
            g_bankPostInternalHookCreated = false;
            g_bankCallbackHookCreated = false;
            Log( "[voice-bank] normal PostEvent diagnostic hooks unavailable" );
            return false;
        }

        Log( "[voice-bank] string/id/internal submit and normal callback "
            "diagnostic hooks active" );
        return true;
    }

    static bool InstallDurationRoutingHook( uintptr_t base ) {
        g_voiceUtilsTryGetDurationTarget = reinterpret_cast< void * >(
            base + VOICE_UTILS_TRY_GET_DURATION_RVA );
        if ( !MatchesSignature(
                g_voiceUtilsTryGetDurationTarget,
                VOICE_UTILS_TRY_GET_DURATION_SIGNATURE,
                sizeof( VOICE_UTILS_TRY_GET_DURATION_SIGNATURE ) ) ) {
            Log( "[voice-duration-route] signature mismatch; "
                "target-language duration routing unavailable" );
            g_voiceUtilsTryGetDurationTarget = nullptr;
            return false;
        }

        g_durationHookCreated = CreateVoiceHook(
            g_voiceUtilsTryGetDurationTarget,
            reinterpret_cast< void * >( &HookVoiceUtilsTryGetDuration ),
            reinterpret_cast< void ** >(
                &g_originalVoiceUtilsTryGetDuration ),
            "VoiceUtils.TryGetVoiceDuration(string)" );
        const bool enabled = g_durationHookCreated && EnableVoiceHook(
            g_voiceUtilsTryGetDurationTarget,
            "VoiceUtils.TryGetVoiceDuration(string)" );
        if ( !enabled ) {
            if ( g_durationHookCreated ) {
                MH_DisableHook( g_voiceUtilsTryGetDurationTarget );
                MH_RemoveHook( g_voiceUtilsTryGetDurationTarget );
            }
            g_durationHookCreated = false;
            g_voiceUtilsTryGetDurationTarget = nullptr;
            g_originalVoiceUtilsTryGetDuration = nullptr;
            Log( "[voice-duration-route] hook unavailable; "
                "original duration behavior preserved" );
            return false;
        }

        Log( "[voice-duration-route] target-language AudioDialog duration "
            "routing active" );
        return true;
    }

    static bool InstallActionOriginDiagnosticHooks( uintptr_t base ) {
        g_audioAdapterStopByPlayingIdTarget = reinterpret_cast< void * >(
            base + AUDIO_ADAPTER_STOP_BY_PLAYING_ID_RVA );
        g_audioActionQueueExecuteTarget = reinterpret_cast< void * >(
            base + AUDIO_ACTION_QUEUE_EXECUTE_RVA );
        g_audioActionQueueConsumeExecuteTarget = reinterpret_cast< void * >(
            base + AUDIO_ACTION_QUEUE_CONSUME_EXECUTE_RVA );
        g_voiceSpeakChannelStopTarget = reinterpret_cast< void * >(
            base + VOICE_SPEAK_CHANNEL_STOP_RVA );
        g_voicePlayerStopVoiceTarget = reinterpret_cast< void * >(
            base + VOICE_PLAYER_STOP_VOICE_RVA );

        const bool signaturesMatch =
            MatchesSignature(
                g_audioAdapterStopByPlayingIdTarget,
                AUDIO_ADAPTER_STOP_BY_PLAYING_ID_SIGNATURE,
                sizeof( AUDIO_ADAPTER_STOP_BY_PLAYING_ID_SIGNATURE ) ) &&
            MatchesSignature(
                g_audioActionQueueExecuteTarget,
                AUDIO_ACTION_QUEUE_EXECUTE_SIGNATURE,
                sizeof( AUDIO_ACTION_QUEUE_EXECUTE_SIGNATURE ) ) &&
            MatchesSignature(
                g_audioActionQueueConsumeExecuteTarget,
                AUDIO_ACTION_QUEUE_CONSUME_EXECUTE_SIGNATURE,
                sizeof( AUDIO_ACTION_QUEUE_CONSUME_EXECUTE_SIGNATURE ) ) &&
            MatchesSignature(
                g_voiceSpeakChannelStopTarget,
                VOICE_SPEAK_CHANNEL_STOP_SIGNATURE,
                sizeof( VOICE_SPEAK_CHANNEL_STOP_SIGNATURE ) ) &&
            MatchesSignature(
                g_voicePlayerStopVoiceTarget,
                VOICE_PLAYER_STOP_VOICE_SIGNATURE,
                sizeof( VOICE_PLAYER_STOP_VOICE_SIGNATURE ) );
        if ( !signaturesMatch ) {
            Log( "[voice-life] action-origin diagnostic signature mismatch; "
                "stop source hooks unavailable" );
            g_audioAdapterStopByPlayingIdTarget = nullptr;
            g_audioActionQueueExecuteTarget = nullptr;
            g_audioActionQueueConsumeExecuteTarget = nullptr;
            g_voiceSpeakChannelStopTarget = nullptr;
            g_voicePlayerStopVoiceTarget = nullptr;
            return false;
        }

        g_stopEntryHookCreated = CreateVoiceHook(
            g_audioAdapterStopByPlayingIdTarget,
            reinterpret_cast< void * >(
                &HookAudioAdapterStopByPlayingId ),
            reinterpret_cast< void ** >(
                &g_originalAudioAdapterStopByPlayingId ),
            "AudioAdapter.StopByPlayingId" );
        if ( g_stopEntryHookCreated )
            g_actionQueueHookCreated = CreateVoiceHook(
                g_audioActionQueueExecuteTarget,
                reinterpret_cast< void * >(
                    &HookAudioActionQueueExecute ),
                reinterpret_cast< void ** >(
                    &g_originalAudioActionQueueExecute ),
                "AudioActionQueueHelper.QueueExecuteAction" );
        if ( g_actionQueueHookCreated )
            g_actionQueueConsumeHookCreated = CreateVoiceHook(
                g_audioActionQueueConsumeExecuteTarget,
                reinterpret_cast< void * >(
                    &HookAudioActionQueueConsumeExecute ),
                reinterpret_cast< void ** >(
                    &g_originalAudioActionQueueConsumeExecute ),
                "AudioActionQueueHelper._ConsumeExecute" );
        if ( g_actionQueueConsumeHookCreated )
            g_channelStopHookCreated = CreateVoiceHook(
                g_voiceSpeakChannelStopTarget,
                reinterpret_cast< void * >( &HookVoiceSpeakChannelStop ),
                reinterpret_cast< void ** >(
                    &g_originalVoiceSpeakChannelStop ),
                "VoiceSpeakChannelHolder.StopChannel" );
        if ( g_channelStopHookCreated )
            g_voiceStopHookCreated = CreateVoiceHook(
                g_voicePlayerStopVoiceTarget,
                reinterpret_cast< void * >( &HookVoicePlayerStopVoice ),
                reinterpret_cast< void ** >(
                    &g_originalVoicePlayerStopVoice ),
                "VoicePlayer.StopVoice" );
        const bool created = g_stopEntryHookCreated &&
            g_actionQueueHookCreated && g_actionQueueConsumeHookCreated &&
            g_channelStopHookCreated && g_voiceStopHookCreated;
        const bool enabled = created &&
            EnableVoiceHook(
                g_audioAdapterStopByPlayingIdTarget,
                "AudioAdapter.StopByPlayingId" ) &&
            EnableVoiceHook(
                g_audioActionQueueExecuteTarget,
                "AudioActionQueueHelper.QueueExecuteAction" ) &&
            EnableVoiceHook(
                g_audioActionQueueConsumeExecuteTarget,
                "AudioActionQueueHelper._ConsumeExecute" ) &&
            EnableVoiceHook(
                g_voiceSpeakChannelStopTarget,
                "VoiceSpeakChannelHolder.StopChannel" ) &&
            EnableVoiceHook(
                g_voicePlayerStopVoiceTarget,
                "VoicePlayer.StopVoice" );
        if ( !enabled ) {
            void * targets [ ] = {
                g_audioAdapterStopByPlayingIdTarget,
                g_audioActionQueueExecuteTarget,
                g_audioActionQueueConsumeExecuteTarget,
                g_voiceSpeakChannelStopTarget,
                g_voicePlayerStopVoiceTarget
            };
            for ( void * target : targets ) {
                MH_DisableHook( target );
                MH_RemoveHook( target );
            }
            g_stopEntryHookCreated = false;
            g_actionQueueHookCreated = false;
            g_actionQueueConsumeHookCreated = false;
            g_channelStopHookCreated = false;
            g_voiceStopHookCreated = false;
            Log( "[voice-life] stop-entry and action-queue diagnostic hooks "
                "unavailable" );
            return false;
        }

        Log( "[voice-life] stop-entry, channel/voice stop and "
            "action-queue diagnostic hooks active" );
        return true;
    }

    static bool InstallLifecycleDiagnosticHooks( uintptr_t base ) {
        if ( !g_diagnosticsEnabled.load( std::memory_order_acquire ) ) {
            Log( "[voice-life] lifecycle diagnostics disabled" );
            return false;
        }
        Log( "[voice-life] diagnostic profile="
            "native-container-v10" );

        g_audioAdapterPostEventInternalTarget = reinterpret_cast< void * >(
            base + AUDIO_ADAPTER_POST_EVENT_INTERNAL_RVA );
        g_audioAdapterExecuteActionTarget = reinterpret_cast< void * >(
            base + AUDIO_ADAPTER_EXECUTE_ACTION_RVA );
        g_audioAdapterExternalCallbackTarget = reinterpret_cast< void * >(
            base + AUDIO_ADAPTER_EXTERNAL_CALLBACK_RVA );

        const bool signaturesMatch =
            MatchesSignature(
                g_audioAdapterPostEventInternalTarget,
                AUDIO_ADAPTER_POST_EVENT_INTERNAL_SIGNATURE,
                sizeof( AUDIO_ADAPTER_POST_EVENT_INTERNAL_SIGNATURE ) ) &&
            MatchesSignature(
                g_audioAdapterExecuteActionTarget,
                AUDIO_ADAPTER_EXECUTE_ACTION_SIGNATURE,
                sizeof( AUDIO_ADAPTER_EXECUTE_ACTION_SIGNATURE ) ) &&
            MatchesSignature(
                g_audioAdapterExternalCallbackTarget,
                AUDIO_ADAPTER_EXTERNAL_CALLBACK_SIGNATURE,
                sizeof( AUDIO_ADAPTER_EXTERNAL_CALLBACK_SIGNATURE ) );
        if ( !signaturesMatch ) {
            Log( "[voice-life] lifecycle diagnostic signature mismatch; "
                "lifecycle hooks unavailable" );
            g_audioAdapterPostEventInternalTarget = nullptr;
            g_audioAdapterExecuteActionTarget = nullptr;
            g_audioAdapterExternalCallbackTarget = nullptr;
            return false;
        }

        const uintptr_t tryRealAddress =
            base + AUDIO_ADAPTER_TRY_GET_REAL_PLAYING_ID_RVA;
        const uintptr_t sourcePositionAddress =
            base + AK_SOUND_ENGINE_GET_SOURCE_POSITION_RVA;
        const uintptr_t callbackPlayingIdAddress =
            base + AK_EVENT_CALLBACK_GET_PLAYING_ID_RVA;
        g_tryGetRealPlayingId = MatchesSignature(
            reinterpret_cast< void * >( tryRealAddress ),
            AUDIO_ADAPTER_TRY_GET_REAL_PLAYING_ID_SIGNATURE,
            sizeof( AUDIO_ADAPTER_TRY_GET_REAL_PLAYING_ID_SIGNATURE ) )
            ? reinterpret_cast< AudioAdapterTryGetRealPlayingIdFn >(
                tryRealAddress ) : nullptr;
        g_getSourcePlayPosition = MatchesSignature(
            reinterpret_cast< void * >( sourcePositionAddress ),
            AK_SOUND_ENGINE_GET_SOURCE_POSITION_SIGNATURE,
            sizeof( AK_SOUND_ENGINE_GET_SOURCE_POSITION_SIGNATURE ) )
            ? reinterpret_cast< AkSoundEngineGetSourcePositionFn >(
                sourcePositionAddress ) : nullptr;
        g_getCallbackPlayingId = MatchesSignature(
            reinterpret_cast< void * >( callbackPlayingIdAddress ),
            AK_EVENT_CALLBACK_GET_PLAYING_ID_SIGNATURE,
            sizeof( AK_EVENT_CALLBACK_GET_PLAYING_ID_SIGNATURE ) )
            ? reinterpret_cast< AkEventCallbackGetPlayingIdFn >(
                callbackPlayingIdAddress ) : nullptr;
        Log( "[voice-life] helper availability realPlayingId=" +
            std::string( g_tryGetRealPlayingId ? "yes" : "no" ) +
            " sourcePosition=" +
            ( g_getSourcePlayPosition ? "yes" : "no" ) +
            " callbackPlayingId=" +
            ( g_getCallbackPlayingId ? "yes" : "no" ) );

        MH_STATUS status = MH_CreateHook(
            g_audioAdapterPostEventInternalTarget,
            reinterpret_cast< void * >(
                &HookAudioAdapterPostEventInternal ),
            reinterpret_cast< void ** >(
                &g_originalAudioAdapterPostEventInternal ) );
        g_lifecycleInternalHookCreated = status == MH_OK;
        if ( !g_lifecycleInternalHookCreated )
            Log( "[voice-life] MH_CreateHook(internal submit) failed: " +
                std::to_string( static_cast< int >( status ) ) );

        if ( g_lifecycleInternalHookCreated ) {
            status = MH_CreateHook(
                g_audioAdapterExecuteActionTarget,
                reinterpret_cast< void * >(
                    &HookAudioAdapterExecuteAction ),
                reinterpret_cast< void ** >(
                    &g_originalAudioAdapterExecuteAction ) );
            g_lifecycleActionHookCreated = status == MH_OK;
            if ( !g_lifecycleActionHookCreated )
                Log( "[voice-life] MH_CreateHook(action) failed: " +
                    std::to_string( static_cast< int >( status ) ) );
        }

        if ( g_lifecycleActionHookCreated ) {
            status = MH_CreateHook(
                g_audioAdapterExternalCallbackTarget,
                reinterpret_cast< void * >(
                    &HookAudioAdapterExternalCallback ),
                reinterpret_cast< void ** >(
                    &g_originalAudioAdapterExternalCallback ) );
            g_lifecycleCallbackHookCreated = status == MH_OK;
            if ( !g_lifecycleCallbackHookCreated )
                Log( "[voice-life] MH_CreateHook(callback) failed: " +
                    std::to_string( static_cast< int >( status ) ) );
        }

        const bool created = g_lifecycleInternalHookCreated &&
            g_lifecycleActionHookCreated && g_lifecycleCallbackHookCreated;
        if ( !created ) {
            if ( g_lifecycleInternalHookCreated )
                MH_RemoveHook( g_audioAdapterPostEventInternalTarget );
            if ( g_lifecycleActionHookCreated )
                MH_RemoveHook( g_audioAdapterExecuteActionTarget );
            if ( g_lifecycleCallbackHookCreated )
                MH_RemoveHook( g_audioAdapterExternalCallbackTarget );
            g_lifecycleInternalHookCreated = false;
            g_lifecycleActionHookCreated = false;
            g_lifecycleCallbackHookCreated = false;
            return false;
        }

        const bool enabled =
            EnableVoiceHook(
                g_audioAdapterPostEventInternalTarget,
                "AudioAdapter._PostEventWithExternalSource" ) &&
            EnableVoiceHook(
                g_audioAdapterExecuteActionTarget,
                "AudioAdapter._ExecuteActionOnPlayingId" ) &&
            EnableVoiceHook(
                g_audioAdapterExternalCallbackTarget,
                "AudioAdapter._OnExternalSourceEventCallback" );
        if ( !enabled ) {
            MH_DisableHook( g_audioAdapterPostEventInternalTarget );
            MH_DisableHook( g_audioAdapterExecuteActionTarget );
            MH_DisableHook( g_audioAdapterExternalCallbackTarget );
            MH_RemoveHook( g_audioAdapterPostEventInternalTarget );
            MH_RemoveHook( g_audioAdapterExecuteActionTarget );
            MH_RemoveHook( g_audioAdapterExternalCallbackTarget );
            g_lifecycleInternalHookCreated = false;
            g_lifecycleActionHookCreated = false;
            g_lifecycleCallbackHookCreated = false;
            return false;
        }
        Log( "[voice-life] internal submit, action and external callback "
            "diagnostic hooks active" );
        const bool bankDiagnosticsReady =
            InstallBankEventDiagnosticHooks( base );
        const bool actionOriginDiagnosticsReady =
            InstallActionOriginDiagnosticHooks( base );
        Log( "[voice-life] diagnostic groups external=active bank=" +
            std::string( bankDiagnosticsReady ? "active" : "unavailable" ) +
            " actionOrigin=" + ( actionOriginDiagnosticsReady
                ? std::string( "active" ) : "unavailable" ) );
        return true;
    }

} // namespace

namespace VoiceLanguageRouter {

    bool LoadConfiguration( const std::string & configPath ) {
#ifdef EFSTARTCHANGE_VOICE_DIAGNOSTIC_BUILD
        Log( std::string( "[voice-life] build marker=" ) +
            VOICE_DIAGNOSTIC_BUILD_MARKER + " default=enabled" );
#endif
        g_enabled.store( false, std::memory_order_release );
        g_replaceNarrativeVoice.store( true, std::memory_order_release );
        g_diagnosticsEnabled.store(
            VOICE_DIAGNOSTICS_DEFAULT, std::memory_order_release );
        g_shuttingDown.store( false, std::memory_order_release );
        g_hookHostAvailable.store( true, std::memory_order_release );
        g_configGeneration.store( 0, std::memory_order_release );
        g_configPath = configPath;
        g_appliedConfigStamp = ConfigFileStamp { };
        g_pendingConfigStamp = ConfigFileStamp { };
        g_configReloadPending = false;
        g_configReloadDetectedAt = 0;
        g_observedSpeakers.clear( );
        g_playVoiceHits.store( 0, std::memory_order_release );
        g_managerSpeakStringHits.store( 0, std::memory_order_release );
        g_managerSpeakHits.store( 0, std::memory_order_release );
        g_channelPlayVoiceHits.store( 0, std::memory_order_release );
        g_selectWwiseEventHits.store( 0, std::memory_order_release );
        g_selectWwiseEventMatchLogs.store( 0, std::memory_order_release );
        g_submitExternalHits.store( 0, std::memory_order_release );
        g_submitExternalMatchLogs.store( 0, std::memory_order_release );
        g_normalPostStringHits.store( 0, std::memory_order_release );
        g_normalPostIdHits.store( 0, std::memory_order_release );
        g_normalPostInternalHits.store( 0, std::memory_order_release );
        g_internalPlayVoiceHits.store( 0, std::memory_order_release );
        g_narrativeHits.store( 0, std::memory_order_release );
        g_identityFailures.store( 0, std::memory_order_release );
        g_narrativeSkipLogs.store( 0, std::memory_order_release );
        g_lipRouteLogs.store( 0, std::memory_order_release );
        g_lipDialogHits.store( 0, std::memory_order_release );
        g_lipPathHits.store( 0, std::memory_order_release );
        g_lipLoadHits.store( 0, std::memory_order_release );
        g_lipArmLogs.store( 0, std::memory_order_release );
        g_lipLanguageOverrideHits.store(
            0, std::memory_order_release );
        g_durationLanguageOverrideHits.store(
            0, std::memory_order_release );
        g_packagedMediaDiagnosticLogs.store(
            0, std::memory_order_release );
        g_lifecycleLogs.store( 0, std::memory_order_release );
        AcquireSRWLockExclusive( &g_lifecycleLock );
        g_voicePlaybackTraces.clear( );
        g_lastPlayingIdByAudioObject.clear( );
        g_configuredDurationMsByVoiceId.clear( );
        g_globalDurationMsByVoiceId.clear( );
        ReleaseSRWLockExclusive( &g_lifecycleLock );
        g_lipHooksAttempted.store( false, std::memory_order_release );
        g_lipHooksReady.store( false, std::memory_order_release );
        for ( std::size_t index = 0;
            index < g_languagePackageAttempted.size( ); ++index ) {
            g_languagePackageAttempted [ index ].store(
                false, std::memory_order_release );
            g_languagePackageReady [ index ].store(
                false, std::memory_order_release );
        }
        g_auxiliaryPackageLoad.store( false, std::memory_order_release );
        g_auxiliaryPackageLanguage.store(
            FOLLOW_GLOBAL_LANGUAGE, std::memory_order_release );
        g_auxiliaryPackageLoads.store( 0, std::memory_order_release );
        g_auxiliaryPackageUnloadsSuppressed.store(
            0, std::memory_order_release );
        g_packagedMemoryReady.store( false, std::memory_order_release );
        g_nativeMediaRouteReady.store( false, std::memory_order_release );
        g_registeredNativeMedia.clear( );
        g_uncertainNativeMediaSources.clear( );
        g_nativeMediaUnloadSafe.store( true, std::memory_order_release );

        VoiceConfigurationSnapshot snapshot =
            ReadVoiceConfiguration( configPath );
        ApplyVoiceConfiguration( std::move( snapshot ), false );
        g_appliedConfigStamp = ReadConfigFileStamp( configPath );

        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( true );
        if ( threadContext )
            *threadContext = ThreadRoutingContext { };
        return true;
    }

    bool IsEnabled( ) {
        return g_enabled.load( std::memory_order_acquire );
    }

    bool Initialize( ) {
        if ( g_voicePlayerPlayVoiceTarget )
            return true;

        HMODULE gameAssembly = GetModuleHandleA( "GameAssembly.dll" );
        if ( !gameAssembly ) {
            Log( "[voice-lang] GameAssembly.dll is not loaded" );
            g_hookHostAvailable.store( false, std::memory_order_release );
            g_enabled.store( false, std::memory_order_release );
            return false;
        }

        const uintptr_t base = reinterpret_cast< uintptr_t >( gameAssembly );
        g_gameAssemblyBase = base;
        g_gameAssemblySize = 0;
        __try {
            const auto * dos = reinterpret_cast< const IMAGE_DOS_HEADER * >(
                gameAssembly );
            if ( dos->e_magic == IMAGE_DOS_SIGNATURE ) {
                const auto * nt = reinterpret_cast< const IMAGE_NT_HEADERS * >(
                    base + dos->e_lfanew );
                if ( nt->Signature == IMAGE_NT_SIGNATURE )
                    g_gameAssemblySize = nt->OptionalHeader.SizeOfImage;
            }
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            g_gameAssemblySize = 0;
        }
        g_voiceManagerSpeakStringTarget = reinterpret_cast< void * >(
            base + VOICE_MANAGER_SPEAK_STRING_RVA );
        g_voiceManagerSpeakTarget = reinterpret_cast< void * >(
            base + VOICE_MANAGER_SPEAK_RVA );
        g_voiceSpeakChannelPlayVoiceTarget = reinterpret_cast< void * >(
            base + VOICE_SPEAK_CHANNEL_PLAY_VOICE_RVA );
        g_voiceUtilsSelectWwiseEventTarget = reinterpret_cast< void * >(
            base + VOICE_UTILS_SELECT_WWISE_EVENT_RVA );
        g_audioAdapterPostEventExternalTarget = reinterpret_cast< void * >(
            base + AUDIO_ADAPTER_POST_EVENT_EXTERNAL_RVA );
        g_voicePlayerPlayVoiceTarget = reinterpret_cast< void * >(
            base + VOICE_PLAYER_PLAY_VOICE_RVA );
        g_voicePlayerPlayVoiceInternalTarget = reinterpret_cast< void * >(
            base + VOICE_PLAYER_PLAY_VOICE_INTERNAL_RVA );
        g_voicePlayerPlayExternalTarget = reinterpret_cast< void * >(
            base + VOICE_PLAYER_PLAY_EXTERNAL_RVA );
        g_voicePlayerPlayEventTarget = reinterpret_cast< void * >(
            base + VOICE_PLAYER_PLAY_EVENT_RVA );
        g_voiceManagerSpeakNarrativeTarget = reinterpret_cast< void * >(
            base + VOICE_MANAGER_SPEAK_NARRATIVE_RVA );
        g_akSoundEngineLoadFilePackageTarget = reinterpret_cast< void * >(
            base + AK_SOUND_ENGINE_LOAD_FILE_PACKAGE_RVA );
        g_akSoundEngineUnloadFilePackageTarget = reinterpret_cast< void * >(
            base + AK_SOUND_ENGINE_UNLOAD_FILE_PACKAGE_RVA );
        g_setLanguage = reinterpret_cast< VoiceI18nSetLanguageFn >(
            base + VOICE_I18N_SET_LANGUAGE_RVA );
        g_getCurrentLanguage = reinterpret_cast< VoiceI18nGetCurrentLanguageFn >(
            base + VOICE_I18N_GET_CURRENT_LANGUAGE_RVA );
        g_getLanguageName = reinterpret_cast< VoiceI18nGetLanguageNameFn >(
            base + VOICE_I18N_GET_LANGUAGE_NAME_RVA );
        g_getWwiseCurrentLanguage = reinterpret_cast< AkSoundEngineGetCurrentLanguageFn >(
            base + AK_SOUND_ENGINE_GET_CURRENT_LANGUAGE_RVA );
        g_tryLoadLanguagePck = reinterpret_cast< AudioVfsTryLoadLanguagePckFn >(
            base + AUDIO_VFS_TRY_LOAD_LANGUAGE_PCK_RVA );
        g_il2cppStringNew = reinterpret_cast< Il2CppStringNewFn >(
            GetProcAddress( gameAssembly, "il2cpp_string_new" ) );

        const bool narrativeSignatureMatches = MatchesSignature(
            g_voiceManagerSpeakNarrativeTarget,
            VOICE_MANAGER_SPEAK_NARRATIVE_SIGNATURE,
            sizeof( VOICE_MANAGER_SPEAK_NARRATIVE_SIGNATURE ) );

        if ( !MatchesSignature(
                g_voiceManagerSpeakStringTarget,
                VOICE_MANAGER_SPEAK_STRING_SIGNATURE,
                sizeof( VOICE_MANAGER_SPEAK_STRING_SIGNATURE ) ) ||
            !MatchesSignature(
                g_voiceManagerSpeakTarget,
                VOICE_MANAGER_SPEAK_SIGNATURE,
                sizeof( VOICE_MANAGER_SPEAK_SIGNATURE ) ) ||
            !MatchesSignature(
                g_voiceSpeakChannelPlayVoiceTarget,
                VOICE_SPEAK_CHANNEL_PLAY_VOICE_SIGNATURE,
                sizeof( VOICE_SPEAK_CHANNEL_PLAY_VOICE_SIGNATURE ) ) ||
            !MatchesSignature(
                g_voiceUtilsSelectWwiseEventTarget,
                VOICE_UTILS_SELECT_WWISE_EVENT_SIGNATURE,
                sizeof( VOICE_UTILS_SELECT_WWISE_EVENT_SIGNATURE ) ) ||
            !MatchesSignature(
                g_audioAdapterPostEventExternalTarget,
                AUDIO_ADAPTER_POST_EVENT_EXTERNAL_SIGNATURE,
                sizeof( AUDIO_ADAPTER_POST_EVENT_EXTERNAL_SIGNATURE ) ) ||
            !MatchesSignature(
                g_voicePlayerPlayVoiceTarget,
                VOICE_PLAYER_PLAY_VOICE_SIGNATURE,
                sizeof( VOICE_PLAYER_PLAY_VOICE_SIGNATURE ) ) ||
            !MatchesSignature(
                g_voicePlayerPlayVoiceInternalTarget,
                VOICE_PLAYER_PLAY_VOICE_INTERNAL_SIGNATURE,
                sizeof( VOICE_PLAYER_PLAY_VOICE_INTERNAL_SIGNATURE ) ) ||
            !MatchesSignature(
                g_voicePlayerPlayEventTarget,
                VOICE_PLAYER_PLAY_EVENT_SIGNATURE,
                sizeof( VOICE_PLAYER_PLAY_EVENT_SIGNATURE ) ) ||
            !MatchesSignature(
                reinterpret_cast< const void * >( g_setLanguage ),
                VOICE_I18N_SET_LANGUAGE_SIGNATURE,
                sizeof( VOICE_I18N_SET_LANGUAGE_SIGNATURE ) ) ||
            !MatchesSignature(
                reinterpret_cast< const void * >( g_getCurrentLanguage ),
                VOICE_I18N_GET_CURRENT_LANGUAGE_SIGNATURE,
                sizeof( VOICE_I18N_GET_CURRENT_LANGUAGE_SIGNATURE ) ) ||
            !MatchesSignature(
                reinterpret_cast< const void * >( g_getLanguageName ),
                VOICE_I18N_GET_LANGUAGE_NAME_SIGNATURE,
                sizeof( VOICE_I18N_GET_LANGUAGE_NAME_SIGNATURE ) ) ||
            !MatchesSignature(
                reinterpret_cast< const void * >( g_tryLoadLanguagePck ),
                AUDIO_VFS_TRY_LOAD_LANGUAGE_PCK_SIGNATURE,
                sizeof( AUDIO_VFS_TRY_LOAD_LANGUAGE_PCK_SIGNATURE ) ) ||
            !MatchesSignature(
                g_akSoundEngineLoadFilePackageTarget,
                AK_SOUND_ENGINE_LOAD_FILE_PACKAGE_SIGNATURE,
                sizeof( AK_SOUND_ENGINE_LOAD_FILE_PACKAGE_SIGNATURE ) ) ||
            !MatchesSignature(
                g_akSoundEngineUnloadFilePackageTarget,
                AK_SOUND_ENGINE_UNLOAD_FILE_PACKAGE_SIGNATURE,
                sizeof( AK_SOUND_ENGINE_UNLOAD_FILE_PACKAGE_SIGNATURE ) ) ) {
            Log( "[voice-lang] signature mismatch; voice routing disabled for this game build" );
            g_hookHostAvailable.store( false, std::memory_order_release );
            g_enabled.store( false, std::memory_order_release );
            ResetVoiceHookPointers( );
            return false;
        }

        if ( MatchesSignature(
                g_voicePlayerPlayExternalTarget,
                VOICE_PLAYER_PLAY_EXTERNAL_SIGNATURE,
                sizeof( VOICE_PLAYER_PLAY_EXTERNAL_SIGNATURE ) ) ) {
            g_voicePlayerPlayExternal =
                reinterpret_cast< VoicePlayerPlayExternalFn >(
                    g_voicePlayerPlayExternalTarget );
            Log( "[voice-bank-external] VoicePlayer._PlayExternal available" );
        }
        else {
            g_voicePlayerPlayExternalTarget = nullptr;
            g_voicePlayerPlayExternal = nullptr;
            Log( "[voice-bank-external] VoicePlayer._PlayExternal signature mismatch; "
                "bank voices will preserve the original event" );
        }

        if ( !narrativeSignatureMatches ) {
            Log( "[compat] narrative voice entry is already modified or incompatible; "
                "battle and exploration routing will remain available" );
            g_voiceManagerSpeakNarrativeTarget = nullptr;
        }

        if ( !MatchesSignature(
                reinterpret_cast< const void * >( g_getWwiseCurrentLanguage ),
                AK_SOUND_ENGINE_GET_CURRENT_LANGUAGE_SIGNATURE,
                sizeof( AK_SOUND_ENGINE_GET_CURRENT_LANGUAGE_SIGNATURE ) ) ) {
            Log( "[voice-route] Wwise GetCurrentLanguage diagnostic unavailable" );
            g_getWwiseCurrentLanguage = nullptr;
        }
        else {
            Log( "[voice-route] Wwise GetCurrentLanguage diagnostic active" );
        }
        Log( std::string( "[voice-route] per-character source replacement=" ) +
            ( g_il2cppStringNew ? "available" : "unavailable" ) );

        const MH_STATUS initializeStatus = MH_Initialize( );
        if ( initializeStatus == MH_OK )
            g_ownsMinHook = true;
        else if ( initializeStatus != MH_ERROR_ALREADY_INITIALIZED ) {
            Log( "[voice-lang] MH_Initialize failed: " + std::to_string(
                static_cast< int >( initializeStatus ) ) );
            g_hookHostAvailable.store( false, std::memory_order_release );
            g_enabled.store( false, std::memory_order_release );
            ResetVoiceHookPointers( );
            return false;
        }

        const bool created =
            CreateVoiceHook(
                g_voiceManagerSpeakStringTarget,
                reinterpret_cast< void * >( &HookVoiceManagerSpeakString ),
                reinterpret_cast< void ** >( &g_originalVoiceManagerSpeakString ),
                "VoiceManager.Speak(string)" ) &&
            CreateVoiceHook(
                g_voiceManagerSpeakTarget,
                reinterpret_cast< void * >( &HookVoiceManagerSpeak ),
                reinterpret_cast< void ** >( &g_originalVoiceManagerSpeak ),
                "VoiceManager._Speak" ) &&
            CreateVoiceHook(
                g_voiceSpeakChannelPlayVoiceTarget,
                reinterpret_cast< void * >( &HookVoiceSpeakChannelPlayVoice ),
                reinterpret_cast< void ** >(
                    &g_originalVoiceSpeakChannelPlayVoice ),
                "VoiceSpeakChannelProcessor._PlayVoice" ) &&
            CreateVoiceHook(
                g_voiceUtilsSelectWwiseEventTarget,
                reinterpret_cast< void * >(
                    &HookVoiceUtilsSelectWwiseEvent ),
                reinterpret_cast< void ** >(
                    &g_originalVoiceUtilsSelectWwiseEvent ),
                "VoiceUtilsInternal.SelectWwiseEvent" ) &&
            CreateVoiceHook(
                g_audioAdapterPostEventExternalTarget,
                reinterpret_cast< void * >(
                    &HookAudioAdapterPostEventExternal ),
                reinterpret_cast< void ** >(
                    &g_originalAudioAdapterPostEventExternal ),
                "AudioAdapter.PostEventExternal" ) &&
            CreateVoiceHook(
                g_voicePlayerPlayVoiceTarget,
                reinterpret_cast< void * >( &HookVoicePlayerPlayVoice ),
                reinterpret_cast< void ** >( &g_originalVoicePlayerPlayVoice ),
                "VoicePlayer.PlayVoice" ) &&
            CreateVoiceHook(
                g_voicePlayerPlayVoiceInternalTarget,
                reinterpret_cast< void * >( &HookVoicePlayerPlayVoiceInternal ),
                reinterpret_cast< void ** >(
                    &g_originalVoicePlayerPlayVoiceInternal ),
                "VoicePlayer._PlayVoice(ref)" ) &&
            CreateVoiceHook(
                g_voicePlayerPlayEventTarget,
                reinterpret_cast< void * >( &HookVoicePlayerPlayEvent ),
                reinterpret_cast< void ** >(
                    &g_originalVoicePlayerPlayEvent ),
                "VoicePlayer._PlayEvent" ) &&
            CreateVoiceHook(
                g_akSoundEngineLoadFilePackageTarget,
                reinterpret_cast< void * >(
                    &HookAkSoundEngineLoadFilePackage ),
                reinterpret_cast< void ** >(
                    &g_originalAkSoundEngineLoadFilePackage ),
                "AkSoundEngine.LoadFilePackage" ) &&
            CreateVoiceHook(
                g_akSoundEngineUnloadFilePackageTarget,
                reinterpret_cast< void * >(
                    &HookAkSoundEngineUnloadFilePackage ),
                reinterpret_cast< void ** >(
                    &g_originalAkSoundEngineUnloadFilePackage ),
                "AkSoundEngine.UnloadFilePackage" );

        const bool enabled = created &&
            EnableVoiceHook(
                g_voiceManagerSpeakStringTarget,
                "VoiceManager.Speak(string)" ) &&
            EnableVoiceHook(
                g_voiceManagerSpeakTarget,
                "VoiceManager._Speak" ) &&
            EnableVoiceHook(
                g_voiceSpeakChannelPlayVoiceTarget,
                "VoiceSpeakChannelProcessor._PlayVoice" ) &&
            EnableVoiceHook(
                g_voiceUtilsSelectWwiseEventTarget,
                "VoiceUtilsInternal.SelectWwiseEvent" ) &&
            EnableVoiceHook(
                g_audioAdapterPostEventExternalTarget,
                "AudioAdapter.PostEventExternal" ) &&
            EnableVoiceHook(
                g_voicePlayerPlayVoiceTarget,
                "VoicePlayer.PlayVoice" ) &&
            EnableVoiceHook(
                g_voicePlayerPlayVoiceInternalTarget,
                "VoicePlayer._PlayVoice(ref)" ) &&
            EnableVoiceHook(
                g_voicePlayerPlayEventTarget,
                "VoicePlayer._PlayEvent" ) &&
            EnableVoiceHook(
                g_akSoundEngineLoadFilePackageTarget,
                "AkSoundEngine.LoadFilePackage" ) &&
            EnableVoiceHook(
                g_akSoundEngineUnloadFilePackageTarget,
                "AkSoundEngine.UnloadFilePackage" );

        if ( !enabled ) {
            g_hookHostAvailable.store( false, std::memory_order_release );
            g_enabled.store( false, std::memory_order_release );
            RemoveVoiceHooks( );
            if ( g_ownsMinHook ) {
                MH_Uninitialize( );
                g_ownsMinHook = false;
            }
            ResetVoiceHookPointers( );
            return false;
        }

        const bool durationRoutingReady =
            InstallDurationRoutingHook( base );
        const bool lifecycleDiagnosticsReady =
            InstallLifecycleDiagnosticHooks( base );
        const bool packagedMediaSetterReady =
            InstallPackagedMediaSetterHook( base );
        const bool nativeMediaApiReady = ResolveNativeMediaApi( );
        const bool packagedMediaPrewarmReady = packagedMediaSetterReady &&
            PrewarmPackagedMedia( );
        const bool nativeMediaReady = packagedMediaPrewarmReady &&
            nativeMediaApiReady && ApplyNativeMediaRoutes( );

        bool narrativeReady = false;
        if ( g_voiceManagerSpeakNarrativeTarget ) {
            const bool narrativeCreated = CreateVoiceHook(
                g_voiceManagerSpeakNarrativeTarget,
                reinterpret_cast< void * >( &HookVoiceManagerSpeakNarrative ),
                reinterpret_cast< void ** >(
                    &g_originalVoiceManagerSpeakNarrative ),
                "VoiceManager._SpeakNarrative" );
            narrativeReady = narrativeCreated && EnableVoiceHook(
                g_voiceManagerSpeakNarrativeTarget,
                "VoiceManager._SpeakNarrative" );
            if ( !narrativeReady ) {
                if ( narrativeCreated ) {
                    MH_DisableHook( g_voiceManagerSpeakNarrativeTarget );
                    MH_RemoveHook( g_voiceManagerSpeakNarrativeTarget );
                }
                g_voiceManagerSpeakNarrativeTarget = nullptr;
                g_originalVoiceManagerSpeakNarrative = nullptr;
                Log( "[compat] narrative voice and lip routing disabled; "
                    "battle and exploration routing remain active" );
            }
        }

        g_playVoiceHookBytesCaptured = TryCopyBytes(
            g_voicePlayerPlayVoiceTarget, g_playVoiceHookBytes.data( ),
            g_playVoiceHookBytes.size( ) );
        g_hookHostAvailable.store( true, std::memory_order_release );
        g_shuttingDown.store( false, std::memory_order_release );
        g_bankRouteHookCreated = true;
        Log( "[voice-lang] ten core voice hooks installed after IL2CPP stabilization" );
        Log( std::string( "[compat] voice groups core=active narrative=" ) +
            ( narrativeReady ? "active" : "unavailable" ) +
            " lip=" + ( narrativeReady ? "deferred" : "unavailable" ) +
            " duration=" + ( durationRoutingReady
                ? "active" : "unavailable" ) +
            " lifecycle=" + ( lifecycleDiagnosticsReady
                ? "active"
                : ( g_diagnosticsEnabled.load( std::memory_order_acquire )
                    ? "unavailable" : "disabled" ) ) +
            " memoryRoute=" + ( packagedMediaPrewarmReady
                ? "active"
                : ( g_diagnosticsEnabled.load( std::memory_order_acquire )
                    ? "unavailable" : "disabled" ) ) +
            " nativeContainer=" + ( nativeMediaReady
                ? "active" : "v9-fallback" ) );
        Log( "[voice-route] per-character source replacement and auxiliary language-package mounting are active" );
        if ( g_playVoiceHookBytesCaptured ) {
            Log( "[voice-diag] VoicePlayer.PlayVoice patched entry=" +
                FormatBytes(
                    g_playVoiceHookBytes.data( ),
                    g_playVoiceHookBytes.size( ) ) );
        }
        return true;
    }

    void PollHealth( ) {
        if ( g_shuttingDown.load( std::memory_order_acquire ) )
            return;

        PollConfigurationReload( );
        if ( !g_voicePlayerPlayVoiceTarget ||
            !g_playVoiceHookBytesCaptured )
            return;

        std::array< std::uint8_t, HOOK_FINGERPRINT_SIZE > current { };
        if ( !TryCopyBytes(
            g_voicePlayerPlayVoiceTarget, current.data( ), current.size( ) ) )
            return;
        if ( current == g_playVoiceHookBytes ) {
            if ( !g_healthReported.exchange(
                true, std::memory_order_acq_rel ) )
                Log( "[voice-diag] VoicePlayer.PlayVoice hook health=active" );
            return;
        }

        if ( !g_healthRepairAttempted.exchange(
            true, std::memory_order_acq_rel ) ) {
            Log( "[voice-diag] VoicePlayer.PlayVoice entry changed after install current=" +
                FormatBytes( current.data( ), current.size( ) ) );
            if ( MatchesSignature(
                g_voicePlayerPlayVoiceTarget,
                VOICE_PLAYER_PLAY_VOICE_SIGNATURE,
                sizeof( VOICE_PLAYER_PLAY_VOICE_SIGNATURE ) ) ) {
                MH_DisableHook( g_voicePlayerPlayVoiceTarget );
                const MH_STATUS status = MH_EnableHook(
                    g_voicePlayerPlayVoiceTarget );
                if ( status == MH_OK && TryCopyBytes(
                    g_voicePlayerPlayVoiceTarget,
                    g_playVoiceHookBytes.data( ),
                    g_playVoiceHookBytes.size( ) ) ) {
                    Log( "[voice-diag] VoicePlayer.PlayVoice hook was overwritten by original bytes and has been re-enabled" );
                    return;
                }
                Log( "[voice-diag] VoicePlayer.PlayVoice automatic re-enable failed status=" +
                    std::to_string( static_cast< int >( status ) ) );
            }
            else {
                Log( "[voice-diag] VoicePlayer.PlayVoice entry was modified by another patch; automatic repair skipped" );
            }
        }
    }

    bool Shutdown( ) {
        g_shuttingDown.store( true, std::memory_order_release );
        g_enabled.store( false, std::memory_order_release );
        RemoveLipSyncHooks( );
        RemoveVoiceHooks( );
        if ( !ResetVoiceHookPointers( ) ||
            !g_nativeMediaUnloadSafe.load( std::memory_order_acquire ) ) {
            Log( "[voice-native] shutdown refused: Wwise Media overrides "
                "could not be released safely" );
            return false;
        }
        if ( g_ownsMinHook ) {
            MH_Uninitialize( );
            g_ownsMinHook = false;
        }
        AcquireSRWLockExclusive( &g_rulesLock );
        g_rules.clear( );
        g_defaultLanguage = NO_DEFAULT_LANGUAGE;
        ReleaseSRWLockExclusive( &g_rulesLock );
        g_observedSpeakers.clear( );
        g_configPath.clear( );
        g_appliedConfigStamp = ConfigFileStamp { };
        g_pendingConfigStamp = ConfigFileStamp { };
        g_configReloadPending = false;
        g_configReloadDetectedAt = 0;
        g_configGeneration.store( 0, std::memory_order_release );
        g_playVoiceHits.store( 0, std::memory_order_release );
        g_managerSpeakStringHits.store( 0, std::memory_order_release );
        g_managerSpeakHits.store( 0, std::memory_order_release );
        g_channelPlayVoiceHits.store( 0, std::memory_order_release );
        g_selectWwiseEventHits.store( 0, std::memory_order_release );
        g_selectWwiseEventMatchLogs.store( 0, std::memory_order_release );
        g_submitExternalHits.store( 0, std::memory_order_release );
        g_submitExternalMatchLogs.store( 0, std::memory_order_release );
        g_internalPlayVoiceHits.store( 0, std::memory_order_release );
        g_narrativeHits.store( 0, std::memory_order_release );
        g_identityFailures.store( 0, std::memory_order_release );
        g_packagedMediaDiagnosticLogs.store(
            0, std::memory_order_release );
        g_lifecycleLogs.store( 0, std::memory_order_release );
        AcquireSRWLockExclusive( &g_lifecycleLock );
        g_voicePlaybackTraces.clear( );
        g_lastPlayingIdByAudioObject.clear( );
        g_configuredDurationMsByVoiceId.clear( );
        g_globalDurationMsByVoiceId.clear( );
        ReleaseSRWLockExclusive( &g_lifecycleLock );
        for ( std::size_t index = 0;
            index < g_languagePackageAttempted.size( ); ++index ) {
            g_languagePackageAttempted [ index ].store(
                false, std::memory_order_release );
            g_languagePackageReady [ index ].store(
                false, std::memory_order_release );
        }
        g_auxiliaryPackageLoads.store( 0, std::memory_order_release );
        g_auxiliaryPackageUnloadsSuppressed.store(
            0, std::memory_order_release );
        return true;
    }

} // namespace VoiceLanguageRouter
