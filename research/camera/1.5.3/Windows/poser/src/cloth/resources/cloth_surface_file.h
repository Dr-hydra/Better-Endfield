#pragma once
#include "cloth_resource_limits.h"
struct SurfaceInputFile {
  wchar_t path[MAX_PATH]{};
  HANDLE hold = INVALID_HANDLE_VALUE;
  BY_HANDLE_FILE_INFORMATION identity{};
  bool Remove() {
    if (hold != INVALID_HANDLE_VALUE) { CloseHandle(hold); hold = INVALID_HANDLE_VALUE; }
    if (!path[0]) return true;
    HANDLE file = CreateFileW(path,DELETE|FILE_READ_ATTRIBUTES,FILE_SHARE_READ|FILE_SHARE_WRITE|FILE_SHARE_DELETE,
        nullptr,OPEN_EXISTING,FILE_FLAG_OPEN_REPARSE_POINT,nullptr);
    if (file == INVALID_HANDLE_VALUE) {
      if (GetLastError() == ERROR_FILE_NOT_FOUND) { path[0]=0; return true; }
      return false;
    }
    BY_HANDLE_FILE_INFORMATION current{};
    bool own = GetFileInformationByHandle(file,&current) &&
        !(current.dwFileAttributes & (FILE_ATTRIBUTE_DIRECTORY|FILE_ATTRIBUTE_REPARSE_POINT)) &&
        current.dwVolumeSerialNumber==identity.dwVolumeSerialNumber &&
        current.nFileIndexHigh==identity.nFileIndexHigh && current.nFileIndexLow==identity.nFileIndexLow;
    FILE_DISPOSITION_INFO disposition{TRUE};
    bool removed = own && SetFileInformationByHandle(file,FileDispositionInfo,&disposition,sizeof(disposition));
    CloseHandle(file);
    if (removed) path[0]=0;
    return removed;
  }
  bool Create(const void *bytes, DWORD length,size_t limit=eiem_cloth_resource::AuthoredBytes) {
    if (path[0] || hold != INVALID_HANDLE_VALUE || !bytes || !eiem_cloth_resource::Fits(length,limit)) return false;
    wchar_t temp[MAX_PATH]{};
    DWORD count=GetTempPathW(MAX_PATH,temp);
    if (!count || count>=MAX_PATH) return false;
    static unsigned serial=0;
    HANDLE file=INVALID_HANDLE_VALUE;
    for (int attempt=0;attempt<8;++attempt) {
      if (_snwprintf_s(path,_TRUNCATE,L"%seiem-cloth-%lu-%llu-%u.bundle",temp,GetCurrentProcessId(),GetTickCount64(),++serial)<0) {
        path[0]=0; return false;
      }
      file=CreateFileW(path,GENERIC_READ|GENERIC_WRITE,FILE_SHARE_READ,nullptr,CREATE_NEW,FILE_ATTRIBUTE_TEMPORARY,nullptr);
      if (file != INVALID_HANDLE_VALUE) break;
      path[0]=0;
      if (GetLastError()!=ERROR_FILE_EXISTS && GetLastError()!=ERROR_ALREADY_EXISTS) return false;
    }
    if (file==INVALID_HANDLE_VALUE) return false;
    hold=file;
    if (!GetFileInformationByHandle(file,&identity)) return false;
    DWORD written=0,read=0; LARGE_INTEGER zero{};
    std::vector<unsigned char> verify(length);
    if (!WriteFile(file,bytes,length,&written,nullptr) || written!=length || !FlushFileBuffers(file) ||
        !SetFilePointerEx(file,zero,nullptr,FILE_BEGIN) || !ReadFile(file,verify.data(),length,&read,nullptr) ||
        read!=length || memcmp(bytes,verify.data(),length)) return false;
    CloseHandle(hold); hold=INVALID_HANDLE_VALUE;
    file=CreateFileW(path,GENERIC_READ,FILE_SHARE_READ,nullptr,OPEN_EXISTING,FILE_FLAG_OPEN_REPARSE_POINT,nullptr);
    if (file==INVALID_HANDLE_VALUE) return false;
    hold=file; BY_HANDLE_FILE_INFORMATION current{};
    return GetFileInformationByHandle(file,&current) &&
        !(current.dwFileAttributes & (FILE_ATTRIBUTE_DIRECTORY|FILE_ATTRIBUTE_REPARSE_POINT)) &&
        current.dwVolumeSerialNumber==identity.dwVolumeSerialNumber && current.nFileIndexHigh==identity.nFileIndexHigh &&
        current.nFileIndexLow==identity.nFileIndexLow && !current.nFileSizeHigh && current.nFileSizeLow==length &&
        ReadFile(file,verify.data(),length,&read,nullptr) && read==length && !memcmp(bytes,verify.data(),length);
  }
};
