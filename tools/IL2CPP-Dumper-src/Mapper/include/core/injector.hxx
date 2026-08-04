#pragma once

#include <windows.h>
#include <tlhelp32.h>
#include <psapi.h>
#include <string>
#include <fstream>
#include <vector>
#include <cstdarg>
#include <cstdio>

using f_LoadLibraryA = HMODULE( __stdcall * )( LPCSTR );
using f_GetProcAddress = FARPROC( __stdcall * )( HMODULE, LPCSTR );
using f_DLL_ENTRY_POINT = BOOL( __stdcall * )( void *, DWORD, void * );
using f_RtlAddFunctionTable = BOOL( __stdcall * )( PRUNTIME_FUNCTION, DWORD, DWORD64 );

struct MANUAL_MAPPING_DATA {
    f_LoadLibraryA pLoadLibraryA;
    f_GetProcAddress pGetProcAddress;
    f_RtlAddFunctionTable pRtlAddFunctionTable;
    BYTE * pbase;
    HINSTANCE hMod;
    DWORD fdwReasonParam;
    LPVOID reservedParam;
    BOOL SEHSupport;
};

#pragma push_macro("ERROR")
#undef ERROR
enum class LogLevel {
    DEBUG,
    INFO,
    SUCCESS,
    WARNING,
    ERROR_LEVEL
};
#pragma pop_macro("ERROR")

void InitConsole( );
void Log( LogLevel level, const char * format, ... );
void DebugLog( const char * format, ... );
bool IsAdmin( );
bool IsProcessRunning( const std::string & processName );
bool LaunchProcessSuspended( const std::string & exePath, PROCESS_INFORMATION & pi );
bool EnableDebugPrivilege( );
bool CheckProcessArchitecture( HANDLE hProcess );
bool ManualMapDLL( HANDLE hProcess, const std::string & dllPath );
bool ManualMapDLLBytes( HANDLE hProcess, const std::vector<BYTE> & dllData,
    const std::string & outputDir = "" );
void CleanupProcess( PROCESS_INFORMATION & pi );

#pragma runtime_checks("", off)
#pragma optimize("", off)
extern "C" void __stdcall Shellcode( MANUAL_MAPPING_DATA * pData );
