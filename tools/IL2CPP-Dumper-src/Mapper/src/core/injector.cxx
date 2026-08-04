#include "core/injector.hxx"
#include "dumper_config.hxx"
#include <cstdio>
#include <io.h>
#include <fcntl.h>
#include <cstdlib>
#include <cstring>
#include <winternl.h>
#include <shlwapi.h>

#pragma comment(lib, "shlwapi.lib")

#define RELOC_FLAG64(RelInfo) ((RelInfo >> 0x0C) == IMAGE_REL_BASED_DIR64)
#ifndef IMAGE_SNAP_BY_ORDINAL
#define IMAGE_SNAP_BY_ORDINAL(Ordinal) ((Ordinal & IMAGE_ORDINAL_FLAG) != 0)
#endif

void InitConsole( ) {
    AllocConsole( );
    SetConsoleOutputCP( CP_UTF8 );
    SetConsoleCP( CP_UTF8 );
    FILE * pCout;
    FILE * pCin;
    FILE * pCerr;
    freopen_s( &pCout, "CONOUT$", "w", stdout );
    freopen_s( &pCin, "CONIN$", "r", stdin );
    freopen_s( &pCerr, "CONOUT$", "w", stderr );
    SetConsoleTitleA( "Goida Launcher" );
    HANDLE hConsole = GetStdHandle( STD_OUTPUT_HANDLE );
    CONSOLE_FONT_INFOEX cfi = { sizeof( CONSOLE_FONT_INFOEX ) };
    GetCurrentConsoleFontEx( hConsole, FALSE, &cfi );
    wcscpy_s( cfi.FaceName, L"Consolas" );
    cfi.dwFontSize.Y = 14;
    SetCurrentConsoleFontEx( hConsole, FALSE, &cfi );
}

void Log( LogLevel level, const char * format, ... ) {
    HANDLE hConsole = GetStdHandle( STD_OUTPUT_HANDLE );
    WORD color = FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_RED;
    const char * prefix = "";

    switch ( level ) {
    case LogLevel::DEBUG:
        color = FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_RED;
        prefix = "[DEBUG]";
        break;
    case LogLevel::INFO:
        color = FOREGROUND_BLUE | FOREGROUND_GREEN | FOREGROUND_INTENSITY;
        prefix = "[INFO] ";
        break;
    case LogLevel::SUCCESS:
        color = FOREGROUND_GREEN | FOREGROUND_INTENSITY;
        prefix = "[ OK ] ";
        break;
    case LogLevel::WARNING:
        color = FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_INTENSITY;
        prefix = "[WARN] ";
        break;
    case LogLevel::ERROR_LEVEL:
        color = FOREGROUND_RED | FOREGROUND_INTENSITY;
        prefix = "[ERROR]";
        break;
    }

    SetConsoleTextAttribute( hConsole, color );
    printf( "%s ", prefix );

    va_list args;
    va_start( args, format );
    char buffer [ 2048 ];
    vsnprintf( buffer, sizeof( buffer ), format, args );
    va_end( args );
    printf( "%s", buffer );

    SetConsoleTextAttribute( hConsole, FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_RED );
}

void DebugLog( const char * format, ... ) {
    static FILE * logFile = nullptr;
    if ( !logFile ) {
        char logPath [ MAX_PATH ];
        GetModuleFileNameA( nullptr, logPath, MAX_PATH );
        PathRemoveFileSpecA( logPath );
        strcat_s( logPath, MAX_PATH, "\\debug.log" );
        fopen_s( &logFile, logPath, "a" );
        if ( logFile ) {
            SYSTEMTIME st;
            GetLocalTime( &st );
            fprintf( logFile, "\n[%04d-%02d-%02d %02d:%02d:%02d] Session started\n",
                st.wYear, st.wMonth, st.wDay, st.wHour, st.wMinute, st.wSecond );
            fflush( logFile );
        }
    }

    if ( logFile ) {
        va_list args;
        va_start( args, format );
        vfprintf( logFile, format, args );
        va_end( args );
        fflush( logFile );
    }
}

bool IsAdmin( ) {
    BOOL isMember = FALSE;
    PSID adminGroup = nullptr;
    SID_IDENTIFIER_AUTHORITY ntAuthority = SECURITY_NT_AUTHORITY;

    if ( AllocateAndInitializeSid( &ntAuthority, 2, SECURITY_BUILTIN_DOMAIN_RID,
        DOMAIN_ALIAS_RID_ADMINS, 0, 0, 0, 0, 0, 0, &adminGroup ) ) {
        CheckTokenMembership( nullptr, adminGroup, &isMember );
        FreeSid( adminGroup );
    }

    return isMember == TRUE;
}

bool IsProcessRunning( const std::string & processName ) {
    HANDLE snapshot = CreateToolhelp32Snapshot( TH32CS_SNAPPROCESS, 0 );
    if ( snapshot == INVALID_HANDLE_VALUE ) {
        return false;
    }

    PROCESSENTRY32 entry = { sizeof( PROCESSENTRY32 ) };
    if ( Process32First( snapshot, &entry ) ) {
        do {
            char exeFileA [ 260 ];
            WideCharToMultiByte( CP_ACP, 0, entry.szExeFile, -1, exeFileA, sizeof( exeFileA ), nullptr, nullptr );
            if ( _stricmp( exeFileA, processName.c_str( ) ) == 0 ) {
                CloseHandle( snapshot );
                return true;
            }
        }
        while ( Process32Next( snapshot, &entry ) );
    }

    CloseHandle( snapshot );
    return false;
}

bool LaunchProcessSuspended( const std::string & exePath, PROCESS_INFORMATION & pi ) {
    STARTUPINFOA si = { sizeof( STARTUPINFOA ) };
    ZeroMemory( &pi, sizeof( pi ) );
    std::string cmdLine = "\"" + exePath + "\" -force-d3d11";

    if ( CreateProcessA( nullptr, const_cast< char * >( cmdLine.c_str( ) ), nullptr, nullptr,
        FALSE, CREATE_SUSPENDED, nullptr, nullptr, &si, &pi ) ) {
        Log( LogLevel::SUCCESS, "Game launched successfully (PID: %lu)\n", pi.dwProcessId );
        return true;
    }

    Log( LogLevel::ERROR_LEVEL, "Failed to launch process (0x%X)\n", GetLastError( ) );
    return false;
}

bool EnableDebugPrivilege( ) {
    HANDLE token = nullptr;
    if ( !OpenProcessToken( GetCurrentProcess( ), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &token ) ) {
        return false;
    }

    TOKEN_PRIVILEGES privileges = { 1, {{0, 0, SE_PRIVILEGE_ENABLED}} };
    if ( !LookupPrivilegeValueA( nullptr, "SeDebugPrivilege", &privileges.Privileges [ 0 ].Luid ) ) {
        CloseHandle( token );
        return false;
    }

    AdjustTokenPrivileges( token, FALSE, &privileges, 0, nullptr, nullptr );
    CloseHandle( token );
    return GetLastError( ) == ERROR_SUCCESS;
}

bool CheckProcessArchitecture( HANDLE hProcess ) {
    BOOL isTargetWow64 = FALSE;
    BOOL isCurrentWow64 = FALSE;

    if ( !IsWow64Process( hProcess, &isTargetWow64 ) || !IsWow64Process( GetCurrentProcess( ), &isCurrentWow64 ) ) {
        return false;
    }

    return isTargetWow64 == isCurrentWow64;
}

std::vector<BYTE> ReadFileToMemory( const std::string & filePath ) {
    std::ifstream file( filePath, std::ios::binary | std::ios::ate );
    if ( !file.is_open( ) ) {
        return {};
    }

    size_t fileSize = file.tellg( );
    std::vector<BYTE> buffer( fileSize );
    file.seekg( 0, std::ios::beg );
    file.read( reinterpret_cast< char * >( buffer.data( ) ), fileSize );
    return buffer;
}

#pragma runtime_checks("", off)
#pragma optimize("", off)
void __stdcall Shellcode( MANUAL_MAPPING_DATA * pData ) {
    if ( !pData ) {
        pData->hMod = reinterpret_cast< HINSTANCE >( 0x404040 );
        return;
    }

    BYTE * pBase = pData->pbase;
    auto * pOpt = &reinterpret_cast< IMAGE_NT_HEADERS * >( pBase + reinterpret_cast< IMAGE_DOS_HEADER * >( ( uintptr_t ) pBase )->e_lfanew )->OptionalHeader;

    auto _LoadLibraryA = pData->pLoadLibraryA;
    auto _GetProcAddress = pData->pGetProcAddress;
    auto _RtlAddFunctionTable = pData->pRtlAddFunctionTable;
    auto _DllMain = reinterpret_cast< f_DLL_ENTRY_POINT >( pBase + pOpt->AddressOfEntryPoint );

    BYTE * LocationDelta = pBase - pOpt->ImageBase;
    if ( LocationDelta && pOpt->DataDirectory [ IMAGE_DIRECTORY_ENTRY_BASERELOC ].Size ) {
        auto * pRelocData = reinterpret_cast< IMAGE_BASE_RELOCATION * >( pBase + pOpt->DataDirectory [ IMAGE_DIRECTORY_ENTRY_BASERELOC ].VirtualAddress );
        const auto * pRelocEnd = reinterpret_cast< IMAGE_BASE_RELOCATION * >( reinterpret_cast< uintptr_t >( pRelocData ) + pOpt->DataDirectory [ IMAGE_DIRECTORY_ENTRY_BASERELOC ].Size );
        while ( pRelocData < pRelocEnd && pRelocData->SizeOfBlock ) {
            UINT AmountOfEntries = ( pRelocData->SizeOfBlock - sizeof( IMAGE_BASE_RELOCATION ) ) / sizeof( WORD );
            WORD * pRelativeInfo = reinterpret_cast< WORD * >( pRelocData + 1 );

            for ( UINT i = 0; i != AmountOfEntries; ++i, ++pRelativeInfo ) {
                if ( RELOC_FLAG64( *pRelativeInfo ) ) {
                    UINT_PTR * pPatch = reinterpret_cast< UINT_PTR * >( pBase + pRelocData->VirtualAddress + ( ( *pRelativeInfo ) & 0xFFF ) );
                    *pPatch += reinterpret_cast< UINT_PTR >( LocationDelta );
                }
            }
            pRelocData = reinterpret_cast< IMAGE_BASE_RELOCATION * >( reinterpret_cast< BYTE * >( pRelocData ) + pRelocData->SizeOfBlock );
        }
    }

    if ( pOpt->DataDirectory [ IMAGE_DIRECTORY_ENTRY_IMPORT ].Size ) {
        auto * pImportDescr = reinterpret_cast< IMAGE_IMPORT_DESCRIPTOR * >( pBase + pOpt->DataDirectory [ IMAGE_DIRECTORY_ENTRY_IMPORT ].VirtualAddress );
        while ( pImportDescr->Name ) {
            char * szMod = reinterpret_cast< char * >( pBase + pImportDescr->Name );
            HINSTANCE hDll = _LoadLibraryA( szMod );

            ULONG_PTR * pThunkRef = reinterpret_cast< ULONG_PTR * >( pBase + pImportDescr->OriginalFirstThunk );
            ULONG_PTR * pFuncRef = reinterpret_cast< ULONG_PTR * >( pBase + pImportDescr->FirstThunk );

            if ( !pImportDescr->OriginalFirstThunk )
                pThunkRef = pFuncRef;

            for ( ; *pThunkRef; ++pThunkRef, ++pFuncRef ) {
                if ( IMAGE_SNAP_BY_ORDINAL( *pThunkRef ) ) {
                    *pFuncRef = ( ULONG_PTR ) _GetProcAddress( hDll, reinterpret_cast< LPCSTR >( *pThunkRef & 0xFFFF ) );
                }
                else {
                    auto * pImport = reinterpret_cast< IMAGE_IMPORT_BY_NAME * >( pBase + ( *pThunkRef ) );
                    *pFuncRef = ( ULONG_PTR ) _GetProcAddress( hDll, pImport->Name );
                }
            }
            ++pImportDescr;
        }
    }

    if ( pOpt->DataDirectory [ IMAGE_DIRECTORY_ENTRY_TLS ].Size ) {
        auto * pTLS = reinterpret_cast< IMAGE_TLS_DIRECTORY * >( pBase + pOpt->DataDirectory [ IMAGE_DIRECTORY_ENTRY_TLS ].VirtualAddress );
        auto * pCallback = reinterpret_cast< PIMAGE_TLS_CALLBACK * >( pTLS->AddressOfCallBacks );
        for ( ; pCallback && *pCallback; ++pCallback )
            ( *pCallback )( pBase, DLL_PROCESS_ATTACH, nullptr );
    }

    bool ExceptionSupportFailed = false;
    if ( pData->SEHSupport ) {
        auto excep = pOpt->DataDirectory [ IMAGE_DIRECTORY_ENTRY_EXCEPTION ];
        if ( excep.Size ) {
            if ( !_RtlAddFunctionTable(
                reinterpret_cast< PRUNTIME_FUNCTION >( pBase + excep.VirtualAddress ),
                excep.Size / sizeof( RUNTIME_FUNCTION ), ( DWORD64 ) pBase ) ) {
                ExceptionSupportFailed = true;
            }
        }
    }

    _DllMain( pBase, pData->fdwReasonParam, pData->reservedParam );

    pData->hMod = ExceptionSupportFailed ? reinterpret_cast< HINSTANCE >( 0x505050 ) : reinterpret_cast< HINSTANCE >( pBase );
}
#pragma runtime_checks("", restore)
#pragma optimize("", on)

bool ManualMapDLL( HANDLE hProcess, const std::string & dllPath ) {
    std::vector<BYTE> dllData = ReadFileToMemory( dllPath );
    return ManualMapDLLBytes( hProcess, dllData, "" );
}


bool ManualMapDLLBytes( HANDLE hProcess, const std::vector<BYTE> & dllData,
    const std::string & outputDir ) {
    if ( dllData.size( ) < 4096 ) {
        Log( LogLevel::ERROR_LEVEL, "Invalid DLL data\n" );
        return false;
    }

    BYTE * dataMut = const_cast< BYTE * >( dllData.data( ) );

    PIMAGE_DOS_HEADER dosHeader = reinterpret_cast< PIMAGE_DOS_HEADER >( dataMut );
    if ( dosHeader->e_magic != IMAGE_DOS_SIGNATURE ) {
        Log( LogLevel::ERROR_LEVEL, "Invalid file format\n" );
        return false;
    }

    PIMAGE_NT_HEADERS ntHeaders = reinterpret_cast< PIMAGE_NT_HEADERS >( dataMut + dosHeader->e_lfanew );
    if ( ntHeaders->Signature != IMAGE_NT_SIGNATURE || ntHeaders->FileHeader.Machine != IMAGE_FILE_MACHINE_AMD64 ) {
        Log( LogLevel::ERROR_LEVEL, "Invalid PE format\n" );
        return false;
    }

    SIZE_T imageSize = ntHeaders->OptionalHeader.SizeOfImage;
    LPVOID pRemoteImage = VirtualAllocEx( hProcess, nullptr, imageSize, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE );
    if ( !pRemoteImage ) {
        Log( LogLevel::ERROR_LEVEL, "Memory allocation failed (0x%X)\n", GetLastError( ) );
        return false;
    }

    DWORD oldProtect = 0;
    VirtualProtectEx( hProcess, pRemoteImage, imageSize, PAGE_EXECUTE_READWRITE, &oldProtect );

    LPVOID pRemoteConfig = nullptr;
    if ( !outputDir.empty( ) ) {
        pRemoteConfig = VirtualAllocEx( hProcess, nullptr, sizeof( DumperConfig ),
            MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE );
        if ( pRemoteConfig ) {
            DumperConfig cfg = { 0 };
            memcpy( cfg.magic, DUMPER_CONFIG_MAGIC, sizeof( cfg.magic ) );
            strncpy_s( cfg.outputDir, outputDir.c_str( ), sizeof( cfg.outputDir ) - 1 );

            if ( !WriteProcessMemory( hProcess, pRemoteConfig, &cfg, sizeof( cfg ), nullptr ) ) {
                VirtualFreeEx( hProcess, pRemoteConfig, 0, MEM_RELEASE );
                pRemoteConfig = nullptr;
            }
        }
    }

    MANUAL_MAPPING_DATA mappingData = { 0 };
    mappingData.pLoadLibraryA = LoadLibraryA;
    mappingData.pGetProcAddress = GetProcAddress;
    mappingData.pRtlAddFunctionTable = reinterpret_cast< f_RtlAddFunctionTable >( RtlAddFunctionTable );
    mappingData.pbase = reinterpret_cast< BYTE * >( pRemoteImage );
    mappingData.fdwReasonParam = DLL_PROCESS_ATTACH;
    mappingData.reservedParam = pRemoteConfig;
    mappingData.SEHSupport = TRUE;

    if ( !WriteProcessMemory( hProcess, pRemoteImage, dataMut, 0x1000, nullptr ) ) {
        Log( LogLevel::ERROR_LEVEL, "Failed to write header (0x%X)\n", GetLastError( ) );
        VirtualFreeEx( hProcess, pRemoteImage, 0, MEM_RELEASE );
        return false;
    }

    PIMAGE_SECTION_HEADER sectionHeader = IMAGE_FIRST_SECTION( ntHeaders );
    for ( WORD i = 0; i < ntHeaders->FileHeader.NumberOfSections; i++ ) {
        if ( sectionHeader [ i ].SizeOfRawData ) {
            LPVOID pSectionDest = reinterpret_cast< BYTE * >( pRemoteImage ) + sectionHeader [ i ].VirtualAddress;
            LPVOID pSectionSrc = dataMut + sectionHeader [ i ].PointerToRawData;

            if ( !WriteProcessMemory( hProcess, pSectionDest, pSectionSrc, sectionHeader [ i ].SizeOfRawData, nullptr ) ) {
                Log( LogLevel::ERROR_LEVEL, "Failed to write section (0x%X)\n", GetLastError( ) );
                VirtualFreeEx( hProcess, pRemoteImage, 0, MEM_RELEASE );
                return false;
            }
        }
    }

    LPVOID pMappingData = VirtualAllocEx( hProcess, nullptr, sizeof( MANUAL_MAPPING_DATA ), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE );
    if ( !pMappingData ) {
        VirtualFreeEx( hProcess, pRemoteImage, 0, MEM_RELEASE );
        return false;
    }

    if ( !WriteProcessMemory( hProcess, pMappingData, &mappingData, sizeof( MANUAL_MAPPING_DATA ), nullptr ) ) {
        VirtualFreeEx( hProcess, pRemoteImage, 0, MEM_RELEASE );
        VirtualFreeEx( hProcess, pMappingData, 0, MEM_RELEASE );
        return false;
    }

    LPVOID pShellcode = VirtualAllocEx( hProcess, nullptr, 0x1000, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE );
    if ( !pShellcode ) {
        VirtualFreeEx( hProcess, pRemoteImage, 0, MEM_RELEASE );
        VirtualFreeEx( hProcess, pMappingData, 0, MEM_RELEASE );
        return false;
    }

    void * pShellcodeFunc = reinterpret_cast< void * >( Shellcode );
    if ( !WriteProcessMemory( hProcess, pShellcode, pShellcodeFunc, 0x1000, nullptr ) ) {
        Log( LogLevel::ERROR_LEVEL, "Failed to write shellcode (0x%X)\n", GetLastError( ) );
        VirtualFreeEx( hProcess, pRemoteImage, 0, MEM_RELEASE );
        VirtualFreeEx( hProcess, pMappingData, 0, MEM_RELEASE );
        VirtualFreeEx( hProcess, pShellcode, 0, MEM_RELEASE );
        return false;
    }

    HANDLE hThread = CreateRemoteThread( hProcess, nullptr, 0, reinterpret_cast< LPTHREAD_START_ROUTINE >( pShellcode ), pMappingData, 0, nullptr );
    if ( !hThread ) {
        Log( LogLevel::ERROR_LEVEL, "Thread creation failed (0x%X)\n", GetLastError( ) );
        VirtualFreeEx( hProcess, pRemoteImage, 0, MEM_RELEASE );
        VirtualFreeEx( hProcess, pMappingData, 0, MEM_RELEASE );
        VirtualFreeEx( hProcess, pShellcode, 0, MEM_RELEASE );
        return false;
    }
    CloseHandle( hThread );

    HINSTANCE hCheck = nullptr;
    while ( !hCheck ) {
        DWORD exitCode = 0;
        if ( !GetExitCodeProcess( hProcess, &exitCode ) ) {
            Log( LogLevel::ERROR_LEVEL, "Failed to get process exit code (0x%X)\n", GetLastError( ) );
            break;
        }

        if ( exitCode != STILL_ACTIVE ) {
            Log( LogLevel::ERROR_LEVEL, "Process crashed, exit code: %lu\n", exitCode );
            VirtualFreeEx( hProcess, pRemoteImage, 0, MEM_RELEASE );
            VirtualFreeEx( hProcess, pMappingData, 0, MEM_RELEASE );
            VirtualFreeEx( hProcess, pShellcode, 0, MEM_RELEASE );
            return false;
        }

        MANUAL_MAPPING_DATA dataChecked = { 0 };
        SIZE_T bytesRead = 0;
        if ( !ReadProcessMemory( hProcess, pMappingData, &dataChecked, sizeof( MANUAL_MAPPING_DATA ), &bytesRead ) ) {
            Log( LogLevel::ERROR_LEVEL, "Failed to read mapping data (0x%X)\n", GetLastError( ) );
            break;
        }

        hCheck = dataChecked.hMod;

        if ( hCheck == reinterpret_cast< HINSTANCE >( 0x404040 ) ) {
            Log( LogLevel::ERROR_LEVEL, "Wrong mapping ptr\n" );
            VirtualFreeEx( hProcess, pRemoteImage, 0, MEM_RELEASE );
            VirtualFreeEx( hProcess, pMappingData, 0, MEM_RELEASE );
            VirtualFreeEx( hProcess, pShellcode, 0, MEM_RELEASE );
            return false;
        }

        if ( hCheck == reinterpret_cast< HINSTANCE >( 0x505050 ) ) {
            Log( LogLevel::WARNING, "Exception support failed!\n" );
        }

        Sleep( 10 );
    }

    BYTE emptyBuffer [ 0x1000 ] = { 0 };
    WriteProcessMemory( hProcess, pRemoteImage, emptyBuffer, 0x1000, nullptr );

    PIMAGE_SECTION_HEADER sectionHeader2 = IMAGE_FIRST_SECTION( ntHeaders );
    for ( WORD i = 0; i < ntHeaders->FileHeader.NumberOfSections; i++ ) {
        if ( sectionHeader2 [ i ].Misc.VirtualSize ) {
            const char * sectionName = reinterpret_cast< const char * >( sectionHeader2 [ i ].Name );
            if ( strcmp( sectionName, ".rsrc" ) == 0 || strcmp( sectionName, ".reloc" ) == 0 ) {
                SIZE_T sectionSize = sectionHeader2 [ i ].Misc.VirtualSize;
                if ( sectionSize > sizeof( emptyBuffer ) ) {
                    std::vector< BYTE > largeBuffer( sectionSize, 0 );
                    WriteProcessMemory( hProcess, reinterpret_cast< BYTE * >( pRemoteImage ) + sectionHeader2 [ i ].VirtualAddress, largeBuffer.data( ), sectionSize, nullptr );
                }
                else {
                    WriteProcessMemory( hProcess, reinterpret_cast< BYTE * >( pRemoteImage ) + sectionHeader2 [ i ].VirtualAddress, emptyBuffer, sectionSize, nullptr );
                }
            }
        }
    }

    for ( WORD i = 0; i < ntHeaders->FileHeader.NumberOfSections; i++ ) {
        if ( sectionHeader2 [ i ].Misc.VirtualSize ) {
            DWORD oldProtect2 = 0;
            DWORD newProtect = PAGE_READONLY;
            if ( sectionHeader2 [ i ].Characteristics & IMAGE_SCN_MEM_WRITE ) {
                newProtect = PAGE_READWRITE;
            }
            else if ( sectionHeader2 [ i ].Characteristics & IMAGE_SCN_MEM_EXECUTE ) {
                newProtect = PAGE_EXECUTE_READ;
            }
            VirtualProtectEx( hProcess, reinterpret_cast< BYTE * >( pRemoteImage ) + sectionHeader2 [ i ].VirtualAddress, sectionHeader2 [ i ].Misc.VirtualSize, newProtect, &oldProtect2 );
        }
    }

    DWORD oldProtect3 = 0;
    VirtualProtectEx( hProcess, pRemoteImage, IMAGE_FIRST_SECTION( ntHeaders )->VirtualAddress, PAGE_READONLY, &oldProtect3 );

    WriteProcessMemory( hProcess, pShellcode, emptyBuffer, 0x1000, nullptr );
    VirtualFreeEx( hProcess, pShellcode, 0, MEM_RELEASE );
    VirtualFreeEx( hProcess, pMappingData, 0, MEM_RELEASE );
    if ( pRemoteConfig ) VirtualFreeEx( hProcess, pRemoteConfig, 0, MEM_RELEASE );

    Log( LogLevel::SUCCESS, "Injection completed (manual mapping)\n" );
    return true;
}

void CleanupProcess( PROCESS_INFORMATION & pi ) {
    if ( pi.hProcess ) {
        TerminateProcess( pi.hProcess, 1 );
        CloseHandle( pi.hProcess );
    }
    if ( pi.hThread ) {
        CloseHandle( pi.hThread );
    }
    ZeroMemory( &pi, sizeof( pi ) );
}
