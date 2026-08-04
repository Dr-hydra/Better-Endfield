#include "../include/voice_language_router.hxx"

#include "../include/utils.hxx"
#include "../../third_party/minhook/include/MinHook.h"

#include <algorithm>
#include <atomic>
#include <cctype>
#include <cstring>
#include <cstdint>
#include <string>
#include <unordered_map>
#include <unordered_set>
#include <windows.h>

namespace {

    constexpr uintptr_t VOICE_PLAYER_PLAY_VOICE_RVA = 0x03ABF7B0;
    constexpr uintptr_t VOICE_I18N_SET_LANGUAGE_RVA = 0x03EB3F70;
    constexpr uintptr_t VOICE_I18N_GET_CURRENT_LANGUAGE_RVA = 0x03AC13B0;
    constexpr std::size_t VOICE_CONTEXT_SPEAKER_CHANNEL_OFFSET = 0x50;
    constexpr int NO_DEFAULT_LANGUAGE = -2;
    constexpr int FOLLOW_GLOBAL_LANGUAGE = -1;
    constexpr std::uint8_t VOICE_PLAYER_PLAY_VOICE_SIGNATURE [ ] = {
        0x48, 0x89, 0x4C, 0x24, 0x08, 0x53, 0x48, 0x83,
        0xEC, 0x20, 0x48, 0x8B, 0xD9, 0x33, 0xD2
    };
    constexpr std::uint8_t VOICE_I18N_SET_LANGUAGE_SIGNATURE [ ] = {
        0x40, 0x53, 0x48, 0x83, 0xEC, 0x20, 0x80, 0x3D
    };
    constexpr std::uint8_t VOICE_I18N_GET_CURRENT_LANGUAGE_SIGNATURE [ ] = {
        0x48, 0x83, 0xEC, 0x28, 0x33, 0xD2, 0xB9, 0x87,
        0x23, 0x00, 0x00
    };

    using VoicePlayerPlayVoiceFn = void ( __fastcall * )( void *, void * );
    using VoiceI18nSetLanguageFn = void ( __fastcall * )( int, void * );
    using VoiceI18nGetCurrentLanguageFn = int ( __fastcall * )( void * );

    std::atomic< bool > g_enabled { false };
    std::atomic< bool > g_shuttingDown { false };
    std::unordered_map< std::string, int > g_rules;
    std::unordered_set< std::string > g_observedSpeakers;
    int g_defaultLanguage = NO_DEFAULT_LANGUAGE;
    SRWLOCK g_playLock = SRWLOCK_INIT;
    SRWLOCK g_observedLock = SRWLOCK_INIT;
    thread_local bool g_routingVoice = false;

    void * g_voicePlayerPlayVoiceTarget = nullptr;
    VoicePlayerPlayVoiceFn g_originalVoicePlayerPlayVoice = nullptr;
    VoiceI18nSetLanguageFn g_setLanguage = nullptr;
    VoiceI18nGetCurrentLanguageFn g_getCurrentLanguage = nullptr;
    bool g_ownsMinHook = false;

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

    static bool TryCopySpeakerChannel(
        void * voiceContext, char * destination, std::size_t capacity ) {
        if ( !voiceContext || !destination || capacity < 2 )
            return false;

        __try {
            auto * contextBytes = static_cast< std::uint8_t * >( voiceContext );
            void * stringObject = *reinterpret_cast< void ** >(
                contextBytes + VOICE_CONTEXT_SPEAKER_CHANNEL_OFFSET );
            if ( !stringObject )
                return false;

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

    static bool TrySwitchLanguage( int targetLanguage, int * previousLanguage ) {
        if ( !g_setLanguage || !g_getCurrentLanguage || !previousLanguage )
            return false;

        __try {
            *previousLanguage = g_getCurrentLanguage( nullptr );
            if ( *previousLanguage != targetLanguage )
                g_setLanguage( targetLanguage, nullptr );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }
    }

    static bool ResolveLanguage( const std::string & speaker, int & language ) {
        auto found = g_rules.find( speaker );
        if ( found != g_rules.end( ) ) {
            language = found->second;
            return true;
        }

        if ( speaker.rfind( "chr_", 0 ) == 0 ) {
            const std::size_t suffixStart = speaker.find( '_', 4 );
            if ( suffixStart != std::string::npos && suffixStart + 1 < speaker.size( ) ) {
                found = g_rules.find( speaker.substr( suffixStart + 1 ) );
                if ( found != g_rules.end( ) ) {
                    language = found->second;
                    return true;
                }
            }
        }

        if ( g_defaultLanguage != NO_DEFAULT_LANGUAGE ) {
            language = g_defaultLanguage;
            return true;
        }
        return false;
    }

    static bool MarkSpeakerObserved( const std::string & speaker ) {
        AcquireSRWLockExclusive( &g_observedLock );
        const bool inserted = g_observedSpeakers.insert( speaker ).second;
        ReleaseSRWLockExclusive( &g_observedLock );
        return inserted;
    }

    static void __fastcall HookVoicePlayerPlayVoice(
        void * voiceContext, void * methodInfo ) {
        if ( !g_originalVoicePlayerPlayVoice )
            return;

        if ( !g_enabled.load( std::memory_order_acquire ) ||
            g_shuttingDown.load( std::memory_order_acquire ) ||
            g_routingVoice || !voiceContext ) {
            g_originalVoicePlayerPlayVoice( voiceContext, methodInfo );
            return;
        }

        char speakerBuffer [ 192 ] = { 0 };
        if ( !TryCopySpeakerChannel(
            voiceContext, speakerBuffer, sizeof( speakerBuffer ) ) ) {
            g_originalVoicePlayerPlayVoice( voiceContext, methodInfo );
            return;
        }

        const std::string speaker = Normalize( speakerBuffer );
        if ( speaker.empty( ) ) {
            g_originalVoicePlayerPlayVoice( voiceContext, methodInfo );
            return;
        }

        if ( MarkSpeakerObserved( speaker ) )
            Log( "[voice-lang] observed speakerChannel=" + speaker );

        int targetLanguage = FOLLOW_GLOBAL_LANGUAGE;
        if ( !ResolveLanguage( speaker, targetLanguage ) ||
            targetLanguage == FOLLOW_GLOBAL_LANGUAGE ) {
            g_originalVoicePlayerPlayVoice( voiceContext, methodInfo );
            return;
        }

        g_routingVoice = true;
        AcquireSRWLockExclusive( &g_playLock );

        int previousLanguage = -1;
        const bool switched = TrySwitchLanguage(
            targetLanguage, &previousLanguage );
        if ( !switched ) {
            Log( "[voice-lang] language switch failed for speaker=" + speaker );
        }
        else if ( previousLanguage != targetLanguage ) {
            Log( "[voice-lang] switched speaker=" + speaker + " " +
                LanguageName( previousLanguage ) + " -> " +
                LanguageName( targetLanguage ) );
        }

        g_originalVoicePlayerPlayVoice( voiceContext, methodInfo );

        ReleaseSRWLockExclusive( &g_playLock );
        g_routingVoice = false;
    }

} // namespace

namespace VoiceLanguageRouter {

    bool LoadConfiguration( const std::string & configPath ) {
        g_enabled.store( false, std::memory_order_release );
        g_rules.clear( );
        g_observedSpeakers.clear( );
        g_defaultLanguage = NO_DEFAULT_LANGUAGE;

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
        g_voicePlayerPlayVoiceTarget = reinterpret_cast< void * >(
            base + VOICE_PLAYER_PLAY_VOICE_RVA );
        g_setLanguage = reinterpret_cast< VoiceI18nSetLanguageFn >(
            base + VOICE_I18N_SET_LANGUAGE_RVA );
        g_getCurrentLanguage = reinterpret_cast< VoiceI18nGetCurrentLanguageFn >(
            base + VOICE_I18N_GET_CURRENT_LANGUAGE_RVA );

        if ( !MatchesSignature(
            g_voicePlayerPlayVoiceTarget, VOICE_PLAYER_PLAY_VOICE_SIGNATURE,
            sizeof( VOICE_PLAYER_PLAY_VOICE_SIGNATURE ) ) ||
            !MatchesSignature(
                reinterpret_cast< const void * >( g_setLanguage ),
                VOICE_I18N_SET_LANGUAGE_SIGNATURE,
                sizeof( VOICE_I18N_SET_LANGUAGE_SIGNATURE ) ) ||
            !MatchesSignature(
                reinterpret_cast< const void * >( g_getCurrentLanguage ),
                VOICE_I18N_GET_CURRENT_LANGUAGE_SIGNATURE,
                sizeof( VOICE_I18N_GET_CURRENT_LANGUAGE_SIGNATURE ) ) ) {
            Log( "[voice-lang] signature mismatch; voice routing disabled for this game build" );
            g_enabled.store( false, std::memory_order_release );
            g_voicePlayerPlayVoiceTarget = nullptr;
            g_setLanguage = nullptr;
            g_getCurrentLanguage = nullptr;
            return false;
        }

        const MH_STATUS initializeStatus = MH_Initialize( );
        if ( initializeStatus == MH_OK )
            g_ownsMinHook = true;
        else if ( initializeStatus != MH_ERROR_ALREADY_INITIALIZED ) {
            Log( "[voice-lang] MH_Initialize failed: " + std::to_string(
                static_cast< int >( initializeStatus ) ) );
            g_voicePlayerPlayVoiceTarget = nullptr;
            g_setLanguage = nullptr;
            g_getCurrentLanguage = nullptr;
            return false;
        }

        MH_STATUS status = MH_CreateHook(
            g_voicePlayerPlayVoiceTarget,
            reinterpret_cast< void * >( &HookVoicePlayerPlayVoice ),
            reinterpret_cast< void ** >( &g_originalVoicePlayerPlayVoice ) );
        if ( status != MH_OK ) {
            Log( "[voice-lang] MH_CreateHook(VoicePlayer.PlayVoice) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            if ( g_ownsMinHook ) {
                MH_Uninitialize( );
                g_ownsMinHook = false;
            }
            g_voicePlayerPlayVoiceTarget = nullptr;
            g_setLanguage = nullptr;
            g_getCurrentLanguage = nullptr;
            return false;
        }

        status = MH_EnableHook( g_voicePlayerPlayVoiceTarget );
        if ( status != MH_OK ) {
            Log( "[voice-lang] MH_EnableHook(VoicePlayer.PlayVoice) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            MH_RemoveHook( g_voicePlayerPlayVoiceTarget );
            if ( g_ownsMinHook ) {
                MH_Uninitialize( );
                g_ownsMinHook = false;
            }
            g_voicePlayerPlayVoiceTarget = nullptr;
            g_originalVoicePlayerPlayVoice = nullptr;
            g_setLanguage = nullptr;
            g_getCurrentLanguage = nullptr;
            return false;
        }

        g_shuttingDown.store( false, std::memory_order_release );
        Log( "[voice-lang] VoicePlayer.PlayVoice hook installed" );
        return true;
    }

    void Shutdown( ) {
        g_shuttingDown.store( true, std::memory_order_release );
        g_enabled.store( false, std::memory_order_release );
        if ( g_voicePlayerPlayVoiceTarget ) {
            MH_DisableHook( g_voicePlayerPlayVoiceTarget );
            MH_RemoveHook( g_voicePlayerPlayVoiceTarget );
        }
        g_voicePlayerPlayVoiceTarget = nullptr;
        g_originalVoicePlayerPlayVoice = nullptr;
        g_setLanguage = nullptr;
        g_getCurrentLanguage = nullptr;
        if ( g_ownsMinHook ) {
            MH_Uninitialize( );
            g_ownsMinHook = false;
        }
        g_rules.clear( );
        g_observedSpeakers.clear( );
        g_defaultLanguage = NO_DEFAULT_LANGUAGE;
    }

} // namespace VoiceLanguageRouter
