#include "../include/voice_language_router.hxx"

#include "../include/il2cpp_api.hxx"
#include "../include/utils.hxx"
#include "../../third_party/minhook/include/MinHook.h"

#include <algorithm>
#include <array>
#include <atomic>
#include <cctype>
#include <cstring>
#include <cstdint>
#include <string>
#include <unordered_map>
#include <unordered_set>
#include <windows.h>

namespace {

    constexpr uintptr_t VOICE_MANAGER_SPEAK_STRING_RVA = 0x03ABC4C0;
    constexpr uintptr_t VOICE_MANAGER_SPEAK_RVA = 0x03AC0EB0;
    constexpr uintptr_t VOICE_SPEAK_CHANNEL_PLAY_VOICE_RVA = 0x03ABF3A0;
    constexpr uintptr_t VOICE_UTILS_SELECT_WWISE_EVENT_RVA = 0x03ABF630;
    constexpr uintptr_t AUDIO_ADAPTER_POST_EVENT_EXTERNAL_RVA = 0x03ABF0A0;
    constexpr uintptr_t VOICE_PLAYER_PLAY_VOICE_RVA = 0x03ABF7B0;
    constexpr uintptr_t VOICE_PLAYER_PLAY_VOICE_INTERNAL_RVA = 0x03ABF1B0;
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
    constexpr int NO_DEFAULT_LANGUAGE = -2;
    constexpr int FOLLOW_GLOBAL_LANGUAGE = -1;
    constexpr std::size_t HOOK_FINGERPRINT_SIZE = 16;
    constexpr std::uint32_t DIAGNOSTIC_HIT_LIMIT = 8;
    constexpr std::uint32_t VOICE_SELECT_MATCH_LOG_LIMIT = 32;
    constexpr std::uint32_t VOICE_SUBMIT_MATCH_LOG_LIMIT = 64;
    constexpr std::uint32_t LIP_ROUTE_LOG_LIMIT = 64;
    constexpr std::uint32_t LIP_DIAGNOSTIC_LOG_LIMIT = 32;
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
    constexpr std::uint8_t VOICE_PLAYER_PLAY_VOICE_SIGNATURE [ ] = {
        0x48, 0x89, 0x4C, 0x24, 0x08, 0x53, 0x48, 0x83,
        0xEC, 0x20, 0x48, 0x8B, 0xD9, 0x33, 0xD2
    };
    constexpr std::uint8_t VOICE_PLAYER_PLAY_VOICE_INTERNAL_SIGNATURE [ ] = {
        0x40, 0x53, 0x48, 0x83, 0xEC, 0x50, 0x80, 0x3D
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
    using Il2CppStringNewFn = void * ( __fastcall * )( const char * );
    using DialogManagerPlayLipSyncTrackFn = void ( __fastcall * )(
        void *, void *, void *, void * );
    using LipSyncGetTrackPathFn = void * ( __fastcall * )(
        int, void *, void *, void * );
    using LipSyncTryLoadTrackFn = bool ( __fastcall * )(
        void *, void **, void * );

    struct RoutingState {
        bool active = false;
        bool cacheReadable = false;
        bool wwiseReadable = false;
        bool setVerified = false;
        int previousCache = FOLLOW_GLOBAL_LANGUAGE;
        int target = FOLLOW_GLOBAL_LANGUAGE;
        char previousWwise [ 64 ] = { 0 };
        char matchedIdentity [ 192 ] = { 0 };
        char source [ 96 ] = { 0 };
    };

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

    struct ThreadRoutingContext {
        bool routingVoice = false;
        LipRoutingContext lipRouting;
        PendingLipRoute pendingLipRoute;
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
        VoiceRuleMap rules;
        int defaultLanguage = NO_DEFAULT_LANGUAGE;
    };

    std::atomic< bool > g_enabled { false };
    std::atomic< bool > g_replaceNarrativeVoice { true };
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
    std::atomic< bool > g_lipHooksAttempted { false };
    std::atomic< bool > g_lipHooksReady { false };
    std::array< std::atomic< bool >, 4 > g_languagePackageAttempted { };
    std::array< std::atomic< bool >, 4 > g_languagePackageReady { };
    std::atomic< bool > g_auxiliaryPackageLoad { false };
    std::atomic< int > g_auxiliaryPackageLanguage { FOLLOW_GLOBAL_LANGUAGE };
    std::atomic< std::uint32_t > g_auxiliaryPackageLoads { 0 };
    std::atomic< std::uint32_t > g_auxiliaryPackageUnloadsSuppressed { 0 };
    VoiceRuleMap g_rules;
    std::unordered_set< std::string > g_observedSpeakers;
    int g_defaultLanguage = NO_DEFAULT_LANGUAGE;
    std::string g_configPath;
    ConfigFileStamp g_appliedConfigStamp;
    ConfigFileStamp g_pendingConfigStamp;
    bool g_configReloadPending = false;
    std::uint64_t g_configReloadDetectedAt = 0;
    SRWLOCK g_playLock = SRWLOCK_INIT;
    SRWLOCK g_rulesLock = SRWLOCK_INIT;
    SRWLOCK g_observedLock = SRWLOCK_INIT;
    SRWLOCK g_routingTlsInitLock = SRWLOCK_INIT;
    // Keep the slot for the game process lifetime. TlsFree does not clear
    // values owned by other threads, so reuse after manual unload is unsafe.
    std::atomic< DWORD > g_routingTlsIndex { TLS_OUT_OF_INDEXES };

    void * g_voiceManagerSpeakStringTarget = nullptr;
    void * g_voiceManagerSpeakTarget = nullptr;
    void * g_voiceSpeakChannelPlayVoiceTarget = nullptr;
    void * g_voiceUtilsSelectWwiseEventTarget = nullptr;
    void * g_audioAdapterPostEventExternalTarget = nullptr;
    void * g_voicePlayerPlayVoiceTarget = nullptr;
    void * g_voicePlayerPlayVoiceInternalTarget = nullptr;
    void * g_voiceManagerSpeakNarrativeTarget = nullptr;
    void * g_akSoundEngineLoadFilePackageTarget = nullptr;
    void * g_akSoundEngineUnloadFilePackageTarget = nullptr;
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
    VoiceContextStaticVoidFn g_originalVoicePlayerPlayVoice = nullptr;
    VoiceContextRefStaticUintFn g_originalVoicePlayerPlayVoiceInternal = nullptr;
    VoiceManagerSpeakNarrativeFn g_originalVoiceManagerSpeakNarrative = nullptr;
    AudioVfsTryLoadLanguagePckFn g_tryLoadLanguagePck = nullptr;
    AkSoundEngineLoadFilePackageFn g_originalAkSoundEngineLoadFilePackage = nullptr;
    AkSoundEngineUnloadFilePackageFn g_originalAkSoundEngineUnloadFilePackage = nullptr;
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
    Il2CppStringNewFn g_il2cppStringNew = nullptr;
    // Manual mapping does not provide the CRT static-TLS lifecycle. Voice
    // routing itself is serialized by g_playLock; nested per-thread state is
    // stored through the Win32 TLS API above.
    RoutingState g_activeRouting;
    bool g_ownsMinHook = false;
    std::array< std::uint8_t, HOOK_FINGERPRINT_SIZE > g_playVoiceHookBytes { };
    bool g_playVoiceHookBytesCaptured = false;
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
        const std::size_t ruleCount = snapshot.rules.size( );
        const int defaultLanguage = snapshot.defaultLanguage;

        g_enabled.store( false, std::memory_order_release );
        AcquireSRWLockExclusive( &g_rulesLock );
        g_rules.swap( snapshot.rules );
        g_defaultLanguage = defaultLanguage;
        g_replaceNarrativeVoice.store(
            replaceNarrativeVoice, std::memory_order_release );
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
        }

        Log( std::string( hotReload
                ? "[voice-hot-reload] applied"
                : "[voice-lang] configured" ) +
            " generation=" + std::to_string( generation ) +
            " enabled=" + ( enabled ? "true" : "false" ) +
            " narrative=" +
                ( replaceNarrativeVoice ? "true" : "false" ) +
            " win32Tls=" + ( tlsReady ? "ready" : "failed" ) +
            " hookHost=" +
                ( hookHostAvailable ? "ready" : "unavailable" ) +
            " rules=" + std::to_string( ruleCount ) +
            " default=" + LanguageName( defaultLanguage ) );
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

    static bool TryParseWwiseLanguage(
        const std::string & source, int & language ) {
        std::string value = Normalize( source );
        if ( value == "zh-cn" || value == "zh" || value == "cn" ||
            value.find( "chinese" ) != std::string::npos ) {
            language = 0;
            return true;
        }
        if ( value == "en-us" || value == "en" ||
            value.find( "english" ) != std::string::npos ) {
            language = 1;
            return true;
        }
        if ( value == "ja-jp" || value == "ja" || value == "jp" ||
            value.find( "japanese" ) != std::string::npos ) {
            language = 2;
            return true;
        }
        if ( value == "ko-kr" || value == "ko" || value == "kr" ||
            value.find( "korean" ) != std::string::npos ) {
            language = 3;
            return true;
        }
        return false;
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

    static bool BeginResolvedRouting(
        int targetLanguage, const std::string & matchedIdentity,
        const char * source ) {
        ThreadRoutingContext * threadContext = GetThreadRoutingContext( );
        if ( targetLanguage == FOLLOW_GLOBAL_LANGUAGE ||
            !g_enabled.load( std::memory_order_acquire ) ||
            g_shuttingDown.load( std::memory_order_acquire ) ||
            !threadContext || threadContext->routingVoice )
            return false;

        AcquireSRWLockExclusive( &g_playLock );
        threadContext->routingVoice = true;
        g_activeRouting = RoutingState { };
        g_activeRouting.active = true;
        g_activeRouting.target = targetLanguage;
        strncpy_s(
            g_activeRouting.matchedIdentity,
            matchedIdentity.c_str( ), _TRUNCATE );
        strncpy_s(
            g_activeRouting.source,
            source ? source : "unknown", _TRUNCATE );
        g_activeRouting.cacheReadable = TryReadCachedLanguage(
            &g_activeRouting.previousCache );
        std::string previousWwise;
        g_activeRouting.wwiseReadable = TryReadWwiseLanguage(
            previousWwise );
        if ( g_activeRouting.wwiseReadable )
            strncpy_s(
                g_activeRouting.previousWwise,
                previousWwise.c_str( ), _TRUNCATE );

        const bool switched = TrySetLanguage( targetLanguage );
        int verifiedCache = FOLLOW_GLOBAL_LANGUAGE;
        g_activeRouting.setVerified = switched &&
            TryReadCachedLanguage( &verifiedCache ) &&
            verifiedCache == targetLanguage;

        Log( "[voice-route] matched=" + matchedIdentity +
            " source=" + std::string( g_activeRouting.source ) +
            " requested=" + LanguageName( targetLanguage ) +
            "(" + std::to_string( targetLanguage ) + ")" +
            " cache=" +
            ( g_activeRouting.cacheReadable
                ? std::string( LanguageName( g_activeRouting.previousCache ) ) +
                    "(" + std::to_string( g_activeRouting.previousCache ) + ")"
                : "<unreadable>" ) +
            " wwise=" +
            ( g_activeRouting.wwiseReadable
                ? std::string( g_activeRouting.previousWwise )
                : "<unreadable>" ) +
            " forced=true set=" + ( switched ? "ok" : "failed" ) +
            " verified=" + ( g_activeRouting.setVerified ? "true" : "false" ) );
        return true;
    }

    static void EndRouting( bool routed ) {
        if ( !routed )
            return;

        int restoreLanguage = FOLLOW_GLOBAL_LANGUAGE;
        bool restoreResolved = false;
        if ( g_activeRouting.wwiseReadable )
            restoreResolved = TryParseWwiseLanguage(
                std::string( g_activeRouting.previousWwise ), restoreLanguage );
        if ( !restoreResolved && g_activeRouting.cacheReadable ) {
            restoreLanguage = g_activeRouting.previousCache;
            restoreResolved = restoreLanguage >= 0 && restoreLanguage <= 3;
        }

        bool restored = false;
        if ( restoreResolved )
            restored = TrySetLanguage( restoreLanguage );
        Log( std::string( "[voice-route] restored=" ) +
            ( restored ? LanguageName( restoreLanguage ) : "failed" ) +
            ( restoreResolved
                ? "(" + std::to_string( restoreLanguage ) + ")"
                : "(<unreadable>)" ) +
            " source=" + std::string( g_activeRouting.source ) +
            " matched=" + std::string( g_activeRouting.matchedIdentity ) );

        g_activeRouting = RoutingState { };
        ReleaseSRWLockExclusive( &g_playLock );
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        if ( threadContext )
            threadContext->routingVoice = false;
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

        int targetLanguage = FOLLOW_GLOBAL_LANGUAGE;
        std::string matchedIdentity;
        const bool narrativeBlocked = IsNarrativeVoiceSource( source ) &&
            !g_replaceNarrativeVoice.load( std::memory_order_acquire );
        const bool matched = !narrativeBlocked && ResolveLanguage(
            std::string( ), source, event, targetLanguage, matchedIdentity ) &&
            targetLanguage != FOLLOW_GLOBAL_LANGUAGE;
        if ( narrativeBlocked && g_narrativeSkipLogs.fetch_add(
            1, std::memory_order_relaxed ) < DIAGNOSTIC_HIT_LIMIT ) {
            Log( "[voice-route] narrative replacement disabled; preserved source=" +
                ( source.empty( )
                    ? std::string( "<empty>" ) : source ) );
        }
        bool logMatch = false;
        std::string replacementSource;
        void * routedExternalSourceKey = externalSourceKey;
        bool sourceReplaced = false;
        const bool replacementPathRecognized = matched &&
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

        const std::uint32_t result =
            g_originalAudioAdapterPostEventExternal(
                eventName, audioObjectId, routedExternalSourceKey, externalCookie,
                callbackType, callback, cookie, codec );
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
        g_originalVoicePlayerPlayVoice( voiceContext );
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
            g_voicePlayerPlayVoiceTarget,
            g_voicePlayerPlayVoiceInternalTarget,
            g_voiceManagerSpeakNarrativeTarget,
            g_akSoundEngineLoadFilePackageTarget,
            g_akSoundEngineUnloadFilePackageTarget
        };
        for ( void * target : targets ) {
            if ( !target )
                continue;
            MH_DisableHook( target );
            MH_RemoveHook( target );
        }
    }

    static void ResetVoiceHookPointers( ) {
        g_voiceManagerSpeakStringTarget = nullptr;
        g_voiceManagerSpeakTarget = nullptr;
        g_voiceSpeakChannelPlayVoiceTarget = nullptr;
        g_voiceUtilsSelectWwiseEventTarget = nullptr;
        g_audioAdapterPostEventExternalTarget = nullptr;
        g_voicePlayerPlayVoiceTarget = nullptr;
        g_voicePlayerPlayVoiceInternalTarget = nullptr;
        g_voiceManagerSpeakNarrativeTarget = nullptr;
        g_akSoundEngineLoadFilePackageTarget = nullptr;
        g_akSoundEngineUnloadFilePackageTarget = nullptr;
        g_originalVoiceManagerSpeakString = nullptr;
        g_originalVoiceManagerSpeak = nullptr;
        g_originalVoiceSpeakChannelPlayVoice = nullptr;
        g_originalVoiceUtilsSelectWwiseEvent = nullptr;
        g_originalAudioAdapterPostEventExternal = nullptr;
        g_originalVoicePlayerPlayVoice = nullptr;
        g_originalVoicePlayerPlayVoiceInternal = nullptr;
        g_originalVoiceManagerSpeakNarrative = nullptr;
        g_tryLoadLanguagePck = nullptr;
        g_originalAkSoundEngineLoadFilePackage = nullptr;
        g_originalAkSoundEngineUnloadFilePackage = nullptr;
        g_setLanguage = nullptr;
        g_getCurrentLanguage = nullptr;
        g_getLanguageName = nullptr;
        g_getWwiseCurrentLanguage = nullptr;
        g_il2cppStringNew = nullptr;
        g_activeRouting = RoutingState { };
        ThreadRoutingContext * threadContext =
            GetThreadRoutingContext( false );
        if ( threadContext )
            *threadContext = ThreadRoutingContext { };
        g_auxiliaryPackageLoad.store( false, std::memory_order_release );
        g_auxiliaryPackageLanguage.store(
            FOLLOW_GLOBAL_LANGUAGE, std::memory_order_release );
        g_playVoiceHookBytesCaptured = false;
        g_healthReported.store( false, std::memory_order_release );
        g_healthRepairAttempted.store( false, std::memory_order_release );
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

} // namespace

namespace VoiceLanguageRouter {

    bool LoadConfiguration( const std::string & configPath ) {
        g_enabled.store( false, std::memory_order_release );
        g_replaceNarrativeVoice.store( true, std::memory_order_release );
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

        if ( !narrativeSignatureMatches ) {
            Log( "[compat] narrative voice entry is already modified or incompatible; "
                "battle and exploration routing will remain available" );
            g_voiceManagerSpeakNarrativeTarget = nullptr;
        }

        if ( !MatchesSignature(
                reinterpret_cast< const void * >( g_getWwiseCurrentLanguage ),
                AK_SOUND_ENGINE_GET_CURRENT_LANGUAGE_SIGNATURE,
                sizeof( AK_SOUND_ENGINE_GET_CURRENT_LANGUAGE_SIGNATURE ) ) ) {
            Log( "[voice-route] Wwise GetCurrentLanguage signature unavailable; restore will use VoiceI18n cache" );
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
        Log( "[voice-lang] nine core voice hooks installed after IL2CPP stabilization" );
        Log( std::string( "[compat] voice groups core=active narrative=" ) +
            ( narrativeReady ? "active" : "unavailable" ) +
            " lip=" + ( narrativeReady ? "deferred" : "unavailable" ) );
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

    void Shutdown( ) {
        g_shuttingDown.store( true, std::memory_order_release );
        g_enabled.store( false, std::memory_order_release );
        RemoveLipSyncHooks( );
        RemoveVoiceHooks( );
        ResetVoiceHookPointers( );
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
    }

} // namespace VoiceLanguageRouter
