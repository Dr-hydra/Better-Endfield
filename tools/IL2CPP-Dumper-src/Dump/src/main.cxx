#define _CRT_SECURE_NO_WARNINGS

#include "../include/dumper.hxx"
#include "../include/dumper_config.hxx"
#include "../include/model_replacer.hxx"
#include "../include/scene_dumper.hxx"
#include "../include/utils.hxx"
#include "../include/voice_language_router.hxx"
#include <cstring>
#include <iostream>
#include <windows.h>

static bool WaitForVoiceHookInstallWindow( ) {
    const DWORD totalTimeoutMs = 90'000;
    DWORD start = GetTickCount( );

    while ( !GetModuleHandleA( "GameAssembly.dll" ) ) {
        if ( GetTickCount( ) - start > totalTimeoutMs ) {
            Log( "[error] GameAssembly.dll never loaded" );
            return false;
        }
        Sleep( 500 );
    }

    Log( "[voice-lang] GameAssembly loaded; waiting 12s without calling IL2CPP from the injector thread" );
    Sleep( 12000 );
    return true;
}

static FILE * Startup( ) {
    AllocConsole( );

    FILE * fDummy = nullptr;
    freopen_s( &fDummy, "CONOUT$", "w", stdout );
    freopen_s( &fDummy, "CONIN$", "r", stdin );

    SetConsoleTitleA( "IL2CPP Dumper - auto" );
    return fDummy;
}

DWORD WINAPI EntryPoint( LPVOID lpParam ) {
    HMODULE hModule = ( HMODULE ) lpParam;
    Startup( );

    Log( "DLL injected. Starting EF Start Change runtime..." );

    const std::string configPath = g_outputDir.empty( )
        ? std::string( "EFStartChange.ini" )
        : g_outputDir + "\\EFStartChange.ini";
    ModelReplacer::LoadConfiguration( configPath );
    VoiceLanguageRouter::LoadConfiguration( configPath );

    const DWORD timeoutMs = 90'000;
    DWORD waitStart = GetTickCount( );
    while ( !GetModuleHandleA( "GameAssembly.dll" ) &&
        GetTickCount( ) - waitStart <= timeoutMs )
        Sleep( 50 );

    const bool redirectReady = ModelReplacer::Initialize( hModule );
    if ( !redirectReady )
        Log( "[compat] model replacement unavailable; voice initialization will continue" );
    bool voiceReady = false;
    if ( !WaitForVoiceHookInstallWindow( ) ) {
        Log( "[voice-lang] hook install window was not reached; game voice remains unchanged" );
    }
    else {
        voiceReady = VoiceLanguageRouter::Initialize( );
        if ( !voiceReady )
            Log( "[compat] voice routing unavailable; model replacement state is preserved" );
    }

    Log( "" );
    Log( std::string( "[compat] feature hosts model=" ) +
        ( redirectReady ? "ready" : "unavailable" ) +
        " voice=" + ( voiceReady ? "ready" : "unavailable" ) );
    Log( "[compat] global F6/F9/F10 diagnostic hotkeys are disabled" );
    Log( "" );

    DWORD lastVoiceHealthPoll = GetTickCount( );
    for ( ;; ) {
        const DWORD now = GetTickCount( );
        if ( now - lastVoiceHealthPoll >= 1000 ) {
            VoiceLanguageRouter::PollHealth( );
            lastVoiceHealthPoll = now;
        }
        Sleep( 100 );
    }
}

BOOL APIENTRY DllMain( HMODULE hModule, DWORD reason, LPVOID lpReserved ) {
    switch ( reason ) {
    case DLL_PROCESS_ATTACH:
        DisableThreadLibraryCalls( hModule );

        if ( lpReserved ) {
            __try {
                auto * cfg = reinterpret_cast< DumperConfig * >( lpReserved );
                if ( memcmp( cfg->magic, DUMPER_CONFIG_MAGIC, 7 ) == 0 ) {
                    char buf [ sizeof( cfg->outputDir ) + 1 ] = { 0 };
                    memcpy( buf, cfg->outputDir, sizeof( cfg->outputDir ) );
                    buf [ sizeof( cfg->outputDir ) ] = '\0';
                    g_outputDir = buf;
                }
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                g_outputDir.clear( );
            }
        }

        CreateThread( nullptr, 0, EntryPoint, hModule, 0, nullptr );
        break;
    }
    return TRUE;
}
