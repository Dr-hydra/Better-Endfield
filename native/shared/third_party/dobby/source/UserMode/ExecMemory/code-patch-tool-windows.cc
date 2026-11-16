#include "dobby_internal.h"

#include <windows.h>

using namespace zz;

PUBLIC MemoryOperationError CodePatch(void *address, uint8_t *buffer, uint32_t buffer_size) {
  DWORD oldProtect;
  int pageSize;

  // Get page size
  SYSTEM_INFO si;
  GetSystemInfo(&si);
  pageSize = si.dwPageSize;

  void *addressPageAlign = (void *)ALIGN(address, pageSize);

  HANDLE hProc = NULL;
  if (!VirtualProtect(addressPageAlign, pageSize, PAGE_EXECUTE_READWRITE, &oldProtect)) {
    // Some games fiddles with VirtualProtect
    DWORD pid = GetCurrentProcessId();
    hProc = OpenProcess(PROCESS_VM_OPERATION, FALSE, pid);
    if (!VirtualProtectEx(hProc, addressPageAlign, pageSize, PAGE_EXECUTE_READWRITE, &oldProtect)) {
      CloseHandle(hProc);
      return kMemoryOperationError;
    }
  }

  memcpy(address, buffer, buffer_size);

  if (hProc) {
    BOOL ok = VirtualProtectEx(hProc, addressPageAlign, pageSize, oldProtect, &oldProtect);
    CloseHandle(hProc);
    if (!ok)
      return kMemoryOperationError;
  } else if (!VirtualProtect(addressPageAlign, pageSize, oldProtect, &oldProtect))
    return kMemoryOperationError;

  return kMemoryOperationSuccess;
}
