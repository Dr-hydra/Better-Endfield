#define _CRT_SECURE_NO_WARNINGS

#include "../include/dumper.hxx"
#include "../include/dumper_config.hxx"
#include "../include/il2cpp_api.hxx"
#include "../include/model_replacer.hxx"
#include "../include/scene_dumper.hxx"
#include "../include/utils.hxx"
#include "../include/voice_language_router.hxx"
#include <cstring>
#include <iostream>
#include <windows.h>

static void * g_il2cppThread = nullptr;
static bool g_gcRegistered = false;

static bool RegisterThreadWithGC( ) {
    if ( g_gcRegistered )
        return true;
    if ( !api::gc_register_my_thread )
        return false;

    ULONG_PTR low = 0, high = 0;
    GetCurrentThreadStackLimits( &low, &high );

    struct GCStackBase {
        void * mem_base;
        void * reg_base;
    } sb;
    sb.mem_base = reinterpret_cast< void * >( high ); // top of stack
    sb.reg_base = nullptr;

    int rc = -1;
    __try {
        rc = api::gc_register_my_thread( &sb );
    }
    __except ( EXCEPTION_EXECUTE_HANDLER ) {
        return false;
    }

    // 0 = GC_SUCCESS, 1 = GC_DUPLICATE
    if ( rc == 0 || rc == 1 ) {
        g_gcRegistered = true;
        Log( "thread registered with GC (rc=" + std::to_string( rc ) + ")" );
        return true;
    }
    Log( "GC_register_my_thread failed, rc=" + std::to_string( rc ) );
    return false;
}

static bool WaitForIl2CppReady( ) {
    const DWORD totalTimeoutMs = 90'000;
    DWORD start = GetTickCount( );

    while ( !GetModuleHandleA( "GameAssembly.dll" ) ) {
        if ( GetTickCount( ) - start > totalTimeoutMs ) {
            Log( "[error] GameAssembly.dll never loaded" );
            return false;
        }
        Sleep( 500 );
    }

    if ( !ModelReplacer::Initialize( nullptr ) )
        Log( "[redirect] early login actor hook was not installed" );

    if ( !api::initialized )
        api::init( );
    if ( !api::initialized ) {
        Log( "[error] api init failed" );
        return false;
    }

    while ( !( api::get_domain && api::get_domain( ) ) ) {
        if ( GetTickCount( ) - start > totalTimeoutMs ) {
            Log( "[error] il2cpp_domain_get never returned non-null" );
            return false;
        }
        Sleep( 500 );
    }

    Log( "Domain ready, settling for 12s before touching IL2CPP..." );
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
    FILE * output = Startup( );

    Log( "DLL injected. Starting deferred Main actor load probe..." );

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

    bool redirectReady = ModelReplacer::Initialize( hModule );
    if ( !redirectReady )
        Log( "[redirect] initialization failed; no game code will be called" );
    if ( !VoiceLanguageRouter::Initialize( ) )
        Log( "[voice-lang] initialization failed; game voice remains unchanged" );

    Log( "" );
    Log( "Deferred-load mode (Main asset load only inside a captured game callback)" );
    Log( "  F9   show deferred Main actor load status" );
    Log( "  F10  disable future deferred loads" );
    Log( "  F6   exit & unload" );
    Log( "" );

    bool exitRequested = false;
    bool prev [ 3 ] = { false };
    auto edge = [ & ] ( int slot, int vk ) -> bool {
        bool down = ( GetAsyncKeyState( vk ) & 0x8000 ) != 0;
        bool fired = down && !prev [ slot ];
        prev [ slot ] = down;
        return fired;
        };

    while ( !exitRequested ) {
        if ( edge( 0, VK_F9 ) )
            ModelReplacer::QueueReplace( );
        if ( edge( 1, VK_F10 ) )
            ModelReplacer::QueueRestore( );
        if ( edge( 2, VK_F6 ) )
            exitRequested = true;
        Sleep( 50 );
    }

    VoiceLanguageRouter::Shutdown( );
    ModelReplacer::Shutdown( );

    Log( "unloading..." );

    if ( output )
        fclose( output );
    FreeConsole( );

    FreeLibraryAndExitThread( hModule, 0 );
    return 0;
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
