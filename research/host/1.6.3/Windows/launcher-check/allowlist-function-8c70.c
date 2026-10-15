__int64 __fastcall sub_180008C70(int th32ProcessID)
{
  DWORD th32ParentProcessID; // esi
  HANDLE Toolhelp32Snapshot; // rax
  void *Toolhelp32Snapshot_1; // rbx
  PROCESSENTRY32W pe; // [rsp+20h] [rbp-248h] BYREF

  th32ParentProcessID = 0;
  Toolhelp32Snapshot = CreateToolhelp32Snapshot(2u, 0);
  Toolhelp32Snapshot_1 = Toolhelp32Snapshot;
  if ( Toolhelp32Snapshot != (HANDLE)-1LL )
  {
    pe.dwSize = 568;
    if ( Process32FirstW(Toolhelp32Snapshot, &pe) )
    {
      while ( pe.th32ProcessID != th32ProcessID )
      {
        if ( !Process32NextW(Toolhelp32Snapshot_1, &pe) )
          goto LABEL_7;
      }
      th32ParentProcessID = pe.th32ParentProcessID;
    }
LABEL_7:
    CloseHandle(Toolhelp32Snapshot_1);
  }
  return th32ParentProcessID;
}
