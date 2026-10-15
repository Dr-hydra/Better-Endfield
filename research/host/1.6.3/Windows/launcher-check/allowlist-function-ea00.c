// Hidden C++ exception states: #wind=16
HMODULE *sub_18000EA00()
{
  HMODULE *result; // rax

  if ( dword_180058A98 > *(_DWORD *)(*((_QWORD *)NtCurrentTeb()->ThreadLocalStoragePointer + (unsigned int)TlsIndex)
                                   + 4LL) )
  {
    Init_thread_header(&dword_180058A98);
    if ( dword_180058A98 == -1 )
    {
      byte_180058A9C = sub_18000DB10();
      Init_thread_footer(&dword_180058A98);
    }
  }
  result = 0;
  if ( byte_180058A9C )
    return &hModule_0;
  return result;
}
