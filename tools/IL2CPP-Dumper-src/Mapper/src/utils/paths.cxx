#include "utils/paths.hxx"
#include <windows.h>
#include <shlwapi.h>

#pragma comment(lib, "shlwapi.lib")


std::string GetExecutableDirectory( ) {
    char path [ MAX_PATH ];
    GetModuleFileNameA( nullptr, path, MAX_PATH );
    PathRemoveFileSpecA( path );
    return std::string( path );
}
