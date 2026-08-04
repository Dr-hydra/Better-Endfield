#include "core/injector.hxx"
#include "utils/paths.hxx"
#include "resources.h"
#include <windows.h>
#include <commdlg.h>
#include <shlwapi.h>
#include <shellapi.h>
#include <vector>
#include <string>
#include <cstdio>
#include <cstdlib>

#pragma comment(lib, "shlwapi.lib")
#pragma comment(lib, "comdlg32.lib")
#pragma comment(lib, "shell32.lib")


#pragma warning( push )
#pragma warning( disable : 4302 4311 4312 )
static std::vector<BYTE> LoadEmbeddedDll( ) {
    HMODULE self = GetModuleHandleW( nullptr );
    HRSRC   hRes = FindResourceW( self, MAKEINTRESOURCEW( IDR_DUMPER_DLL ),
        MAKEINTRESOURCEW( RT_RCDATA ) );
    if ( !hRes ) return {};

    HGLOBAL hData = LoadResource( self, hRes );
    if ( !hData ) return {};

    DWORD  size = SizeofResource( self, hRes );
    LPVOID ptr = LockResource( hData );
    if ( !ptr || size < 4096 ) return {};

    return std::vector<BYTE>( static_cast< BYTE * >( ptr ),
        static_cast< BYTE * >( ptr ) + size );
}
#pragma warning( pop )


namespace {
    const WORD COLOR_WHITE = FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_INTENSITY;
    const WORD COLOR_DEFAULT = FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE;
}


static void PrintBanner( ) {
    HANDLE hConsole = GetStdHandle( STD_OUTPUT_HANDLE );
    SetConsoleTextAttribute( hConsole, COLOR_WHITE );
    printf( "\n  IL2CPP Dumper\n  by @desirepro\n\n" );
    SetConsoleTextAttribute( hConsole, COLOR_DEFAULT );
}


static std::string PickExecutable( ) {
    wchar_t      file [ MAX_PATH * 2 ] = { 0 };
    OPENFILENAMEW ofn = { 0 };
    ofn.lStructSize = sizeof( ofn );
    ofn.hwndOwner = GetConsoleWindow( );
    ofn.lpstrFile = file;
    ofn.nMaxFile = sizeof( file ) / sizeof( file [ 0 ] );
    ofn.lpstrFilter = L"Executables (*.exe)\0*.exe\0All files (*.*)\0*.*\0";
    ofn.nFilterIndex = 1;
    ofn.lpstrTitle = L"Select target IL2CPP executable";
    ofn.Flags = OFN_FILEMUSTEXIST | OFN_PATHMUSTEXIST | OFN_HIDEREADONLY | OFN_NOCHANGEDIR;

    if ( !GetOpenFileNameW( &ofn ) ) return "";

    char narrow [ MAX_PATH * 2 ] = { 0 };
    WideCharToMultiByte( CP_ACP, 0, file, -1, narrow, sizeof( narrow ), nullptr, nullptr );
    return std::string( narrow );
}


static bool LaunchAndInject( const std::string & exePath, const std::vector<BYTE> & dllBytes ) {
    if ( GetFileAttributesA( exePath.c_str( ) ) == INVALID_FILE_ATTRIBUTES ) {
        Log( LogLevel::ERROR_LEVEL, "Executable not found: %s\n", exePath.c_str( ) );
        return false;
    }

    EnableDebugPrivilege( );

    char workDir [ MAX_PATH ] = { 0 };
    strcpy_s( workDir, exePath.c_str( ) );
    PathRemoveFileSpecA( workDir );

    std::string cmdLine = "\"" + exePath + "\"";

    PROCESS_INFORMATION pi = { 0 };
    STARTUPINFOA        si = { sizeof( STARTUPINFOA ) };

    Log( LogLevel::INFO, "Launching suspended: %s\n", exePath.c_str( ) );

    if ( !CreateProcessA( nullptr, &cmdLine [ 0 ], nullptr, nullptr,
        FALSE, CREATE_SUSPENDED,
        nullptr, workDir [ 0 ] ? workDir : nullptr,
        &si, &pi ) ) {
        Log( LogLevel::ERROR_LEVEL, "CreateProcess failed (0x%X)\n", GetLastError( ) );
        return false;
    }

    Log( LogLevel::SUCCESS, "Process created (PID %lu, suspended)\n", pi.dwProcessId );

    if ( !CheckProcessArchitecture( pi.hProcess ) ) {
        Log( LogLevel::ERROR_LEVEL, "Architecture mismatch (target must be x64)\n" );
        TerminateProcess( pi.hProcess, 1 );
        CloseHandle( pi.hThread );
        CloseHandle( pi.hProcess );
        return false;
    }

    std::string outputDir = GetExecutableDirectory( );
    Log( LogLevel::INFO, "Manual-mapping embedded il2cpp.dll (%zu bytes)...\n", dllBytes.size( ) );
    Log( LogLevel::INFO, "Dump output dir: %s\n", outputDir.c_str( ) );

    if ( !ManualMapDLLBytes( pi.hProcess, dllBytes, outputDir ) ) {
        Log( LogLevel::ERROR_LEVEL, "Injection failed; terminating target.\n" );
        TerminateProcess( pi.hProcess, 1 );
        CloseHandle( pi.hThread );
        CloseHandle( pi.hProcess );
        return false;
    }

    if ( ResumeThread( pi.hThread ) == ( DWORD ) -1 ) {
        Log( LogLevel::ERROR_LEVEL, "ResumeThread failed (0x%X)\n", GetLastError( ) );
        TerminateProcess( pi.hProcess, 1 );
        CloseHandle( pi.hThread );
        CloseHandle( pi.hProcess );
        return false;
    }

    Log( LogLevel::SUCCESS, "Game resumed; dumper waiting for IL2CPP runtime inside target.\n" );
    CloseHandle( pi.hThread );
    CloseHandle( pi.hProcess );
    return true;
}


static bool RelaunchAsAdmin( int argc, wchar_t * argv[ ] ) {
    wchar_t exe [ MAX_PATH ] = { 0 };
    GetModuleFileNameW( nullptr, exe, MAX_PATH );

    std::wstring params;
    for ( int i = 1; i < argc; ++i ) {
        if ( i > 1 ) params += L" ";
        params += L"\"";
        params += argv [ i ];
        params += L"\"";
    }

    SHELLEXECUTEINFOW sei = { sizeof( sei ) };
    sei.lpVerb = L"runas";
    sei.lpFile = exe;
    sei.lpParameters = params.empty( ) ? nullptr : params.c_str( );
    sei.nShow = SW_SHOWNORMAL;

    return ShellExecuteExW( &sei ) == TRUE;
}


int wmain( int argc, wchar_t * argv[ ] ) {
    InitConsole( );
    PrintBanner( );

    if ( !IsAdmin( ) ) {
        Log( LogLevel::WARNING, "Not elevated. Relaunching with admin rights...\n" );
        if ( RelaunchAsAdmin( argc, argv ) ) {
            return 0;
        }
        Log( LogLevel::ERROR_LEVEL, "UAC elevation cancelled or failed.\n" );
        Log( LogLevel::INFO, "Right-click Il2cppDumper.exe -> Run as administrator.\n" );
        Log( LogLevel::INFO, "Press ENTER to exit...\n" );
        ( void ) getchar( );
        return 1;
    }

    std::vector<BYTE> dllBytes = LoadEmbeddedDll( );
    if ( dllBytes.empty( ) ) {
        Log( LogLevel::ERROR_LEVEL, "Embedded il2cpp.dll resource missing or corrupt\n" );
        Log( LogLevel::INFO, "Press ENTER to exit...\n" );
        ( void ) getchar( );
        return 1;
    }
    Log( LogLevel::SUCCESS, "il2cpp.dll loaded (%zu bytes)\n", dllBytes.size( ) );

    std::string exePath;
    if ( argc > 1 ) {
        char narrow [ MAX_PATH * 2 ] = { 0 };
        WideCharToMultiByte( CP_ACP, 0, argv [ 1 ], -1, narrow, sizeof( narrow ), nullptr, nullptr );
        exePath = narrow;
        Log( LogLevel::INFO, "Target: %s\n", exePath.c_str( ) );
    }
    else {
        Log( LogLevel::INFO, "Pick target .exe...\n" );
        exePath = PickExecutable( );
        if ( exePath.empty( ) ) {
            Log( LogLevel::INFO, "Cancelled.\n" );
            return 0;
        }
    }

    if ( !LaunchAndInject( exePath, dllBytes ) ) {
        Log( LogLevel::INFO, "Press ENTER to exit...\n" );
        ( void ) getchar( );
        return 1;
    }

    Log( LogLevel::INFO, "Done. Closing in 5 seconds.\n" );
    Sleep( 5000 );
    return 0;
}
