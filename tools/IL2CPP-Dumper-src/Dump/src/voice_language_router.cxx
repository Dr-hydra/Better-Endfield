#include "../include/voice_language_router.hxx"

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

    std::atomic< bool > g_enabled { false };
    std::atomic< bool > g_shuttingDown { false };
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
    std::array< std::atomic< bool >, 4 > g_languagePackageAttempted { };
    std::array< std::atomic< bool >, 4 > g_languagePackageReady { };
    std::atomic< bool > g_auxiliaryPackageLoad { false };
    std::atomic< int > g_auxiliaryPackageLanguage { FOLLOW_GLOBAL_LANGUAGE };
    std::atomic< std::uint32_t > g_auxiliaryPackageLoads { 0 };
    std::atomic< std::uint32_t > g_auxiliaryPackageUnloadsSuppressed { 0 };
    std::unordered_map< std::string, int > g_rules;
    std::unordered_set< std::string > g_observedSpeakers;
    int g_defaultLanguage = NO_DEFAULT_LANGUAGE;
    SRWLOCK g_playLock = SRWLOCK_INIT;
    SRWLOCK g_observedLock = SRWLOCK_INIT;
    thread_local bool g_routingVoice = false;

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
    VoiceI18nSetLanguageFn g_setLanguage = nullptr;
    VoiceI18nGetCurrentLanguageFn g_getCurrentLanguage = nullptr;
    VoiceI18nGetLanguageNameFn g_getLanguageName = nullptr;
    AkSoundEngineGetCurrentLanguageFn g_getWwiseCurrentLanguage = nullptr;
    Il2CppStringNewFn g_il2cppStringNew = nullptr;
    // Manual mapping does not provide the full CRT TLS lifecycle required by
    // non-trivial thread_local objects such as std::string. Routing is already
    // serialized by g_playLock, so a process-wide state is sufficient here.
    RoutingState g_activeRouting;
    bool g_ownsMinHook = false;
    std::array< std::uint8_t, HOOK_FINGERPRINT_SIZE > g_playVoiceHookBytes { };
    bool g_playVoiceHookBytesCaptured = false;
    std::atomic< bool > g_healthReported { false };
    std::atomic< bool > g_healthRepairAttempted { false };

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

        // Only redirect localized character media. This avoids touching other
        // external sources that happen to share the same Wwise submit path.
        const std::string suffix = source.substr( languageEnd + 1 );
        if ( suffix.find( "characters/" ) == std::string::npos )
            return false;

        replacement = "voice/";
        replacement += targetName;
        replacement.push_back( '/' );
        replacement += suffix;
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

    static void AddRule( const std::string & sourceKey, int language ) {
        const std::string key = Normalize( sourceKey );
        if ( key.empty( ) )
            return;
        if ( key == "*" ) {
            g_defaultLanguage = language;
            return;
        }

        g_rules [ key ] = language;
        if ( key.rfind( "chr_", 0 ) == 0 ) {
            const std::size_t suffixStart = key.find( '_', 4 );
            if ( suffixStart != std::string::npos && suffixStart + 1 < key.size( ) )
                g_rules.emplace( key.substr( suffixStart + 1 ), language );
        }
    }

    static void ParseRules( const std::string & source ) {
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
            AddRule( key, language );
        }
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
        const std::string & identity, int & language, std::string & matchedKey ) {
        if ( identity.empty( ) )
            return false;

        auto found = g_rules.find( identity );
        if ( found != g_rules.end( ) ) {
            language = found->second;
            matchedKey = found->first;
            return true;
        }

        if ( identity.rfind( "chr_", 0 ) == 0 ) {
            const std::size_t suffixStart = identity.find( '_', 4 );
            if ( suffixStart != std::string::npos &&
                suffixStart + 1 < identity.size( ) ) {
                found = g_rules.find( identity.substr( suffixStart + 1 ) );
                if ( found != g_rules.end( ) ) {
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
        if ( ResolveExplicitLanguage(
            speaker, language, matchedIdentity ) )
            return true;

        for ( const auto & [ key, configuredLanguage ] : g_rules ) {
            if ( ContainsIdentityToken( voiceData, key ) ||
                ContainsIdentityToken( wwiseEvent, key ) ) {
                language = configuredLanguage;
                matchedIdentity = key;
                return true;
            }
        }

        if ( g_defaultLanguage != NO_DEFAULT_LANGUAGE ) {
            language = g_defaultLanguage;
            matchedIdentity = "*";
            return true;
        }
        return false;
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

    static bool BeginResolvedRouting(
        int targetLanguage, const std::string & matchedIdentity,
        const char * source ) {
        if ( targetLanguage == FOLLOW_GLOBAL_LANGUAGE ||
            !g_enabled.load( std::memory_order_acquire ) ||
            g_shuttingDown.load( std::memory_order_acquire ) ||
            g_routingVoice )
            return false;

        AcquireSRWLockExclusive( &g_playLock );
        g_routingVoice = true;
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
        g_routingVoice = false;
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
        const bool matched = ResolveLanguage(
            std::string( ), source, event, targetLanguage, matchedIdentity ) &&
            targetLanguage != FOLLOW_GLOBAL_LANGUAGE;
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
                        " replacementSource=<not-character-voice>" );
                }
            }
        }

        const std::uint32_t result =
            g_originalAudioAdapterPostEventExternal(
                eventName, audioObjectId, routedExternalSourceKey, externalCookie,
                callbackType, callback, cookie, codec );
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
        g_rules.clear( );
        g_observedSpeakers.clear( );
        g_defaultLanguage = NO_DEFAULT_LANGUAGE;
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
        g_auxiliaryPackageLoad.store( false, std::memory_order_release );
        g_auxiliaryPackageLanguage.store(
            FOLLOW_GLOBAL_LANGUAGE, std::memory_order_release );
        g_auxiliaryPackageLoads.store( 0, std::memory_order_release );
        g_auxiliaryPackageUnloadsSuppressed.store(
            0, std::memory_order_release );

        const bool enabled = ReadConfigBool(
            configPath, "voice_router_enabled", false );
        ParseRules( ReadConfigRules( configPath ) );
        g_enabled.store( enabled, std::memory_order_release );

        Log( "[voice-lang] configured enabled=" +
            std::string( enabled ? "true" : "false" ) +
            " rules=" + std::to_string( g_rules.size( ) ) +
            " default=" + LanguageName( g_defaultLanguage ) );
        return true;
    }

    bool IsEnabled( ) {
        return g_enabled.load( std::memory_order_acquire );
    }

    bool Initialize( ) {
        if ( !g_enabled.load( std::memory_order_acquire ) )
            return true;
        if ( g_voicePlayerPlayVoiceTarget )
            return true;

        HMODULE gameAssembly = GetModuleHandleA( "GameAssembly.dll" );
        if ( !gameAssembly ) {
            Log( "[voice-lang] GameAssembly.dll is not loaded" );
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
                g_voiceManagerSpeakNarrativeTarget,
                VOICE_MANAGER_SPEAK_NARRATIVE_SIGNATURE,
                sizeof( VOICE_MANAGER_SPEAK_NARRATIVE_SIGNATURE ) ) ||
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
            g_enabled.store( false, std::memory_order_release );
            ResetVoiceHookPointers( );
            return false;
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
                g_voiceManagerSpeakNarrativeTarget,
                reinterpret_cast< void * >( &HookVoiceManagerSpeakNarrative ),
                reinterpret_cast< void ** >(
                    &g_originalVoiceManagerSpeakNarrative ),
                "VoiceManager._SpeakNarrative" ) &&
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
                g_voiceManagerSpeakNarrativeTarget,
                "VoiceManager._SpeakNarrative" ) &&
            EnableVoiceHook(
                g_akSoundEngineLoadFilePackageTarget,
                "AkSoundEngine.LoadFilePackage" ) &&
            EnableVoiceHook(
                g_akSoundEngineUnloadFilePackageTarget,
                "AkSoundEngine.UnloadFilePackage" );

        if ( !enabled ) {
            RemoveVoiceHooks( );
            if ( g_ownsMinHook ) {
                MH_Uninitialize( );
                g_ownsMinHook = false;
            }
            ResetVoiceHookPointers( );
            return false;
        }

        g_playVoiceHookBytesCaptured = TryCopyBytes(
            g_voicePlayerPlayVoiceTarget, g_playVoiceHookBytes.data( ),
            g_playVoiceHookBytes.size( ) );
        g_shuttingDown.store( false, std::memory_order_release );
        Log( "[voice-lang] ten voice hooks installed after IL2CPP stabilization" );
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
        if ( !g_enabled.load( std::memory_order_acquire ) ||
            g_shuttingDown.load( std::memory_order_acquire ) ||
            !g_voicePlayerPlayVoiceTarget ||
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
        RemoveVoiceHooks( );
        ResetVoiceHookPointers( );
        if ( g_ownsMinHook ) {
            MH_Uninitialize( );
            g_ownsMinHook = false;
        }
        g_rules.clear( );
        g_observedSpeakers.clear( );
        g_defaultLanguage = NO_DEFAULT_LANGUAGE;
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
