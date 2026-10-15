// Hidden C++ exception states: #wind=2
wint_t *__fastcall sub_180008D00(wint_t *p_C, DWORD dwProcessId)
{
  unsigned __int64 n8; // r13
  unsigned __int64 n7; // r12
  __int64 v5; // rcx
  HANDLE hProcess; // r15
  WCHAR *lpExeName_1; // rdi
  WCHAR *v8; // rsi
  __int64 dwSize_1; // rax
  unsigned __int64 dwSize_2; // rcx
  size_t Size; // rbx
  WCHAR *lpExeName_2; // rax
  void **Src_1; // rdi
  char *v14; // r15
  wint_t *p_C_1; // rdi
  wint_t *p_C_2; // rbx
  wint_t *p_C_3; // rsi
  signed __int64 v18; // rdi
  char *v19; // rax
  unsigned __int64 n8_1; // rbx
  void *v21; // rax
  LPWSTR lpExeName; // [rsp+20h] [rbp-40h] BYREF
  void *v24; // [rsp+28h] [rbp-38h]
  __int64 v25; // [rsp+30h] [rbp-30h]
  void *Src[2]; // [rsp+38h] [rbp-28h] BYREF
  unsigned __int64 n8_2; // [rsp+48h] [rbp-18h]
  __int64 n7_1; // [rsp+50h] [rbp-10h]
  DWORD dwSize; // [rsp+A0h] [rbp+40h] BYREF

  Src[0] = 0;
  n8 = 0;
  n8_2 = 0;
  n7 = 7;
  n7_1 = 7;
  hProcess = OpenProcess(0x1000u, 0, dwProcessId);
  if ( hProcess )
  {
    dwSize = 260;
    sub_1800092D0(&lpExeName, 260);
    lpExeName_1 = lpExeName;
    if ( !QueryFullProcessImageNameW(hProcess, 0, lpExeName, &dwSize) )
    {
      v8 = (WCHAR *)v24;
      while ( 1 )
      {
        if ( GetLastError() != ERROR_INSUFFICIENT_BUFFER )
          goto LABEL_14;
        dwSize_1 = 2 * dwSize;
        dwSize = dwSize_1;
        dwSize_2 = v8 - lpExeName_1;
        if ( (unsigned int)dwSize_1 < dwSize_2 )
          break;
        if ( (unsigned int)dwSize_1 > dwSize_2 )
        {
          if ( (unsigned int)dwSize_1 <= (unsigned __int64)((v25 - (__int64)lpExeName_1) >> 1) )
          {
            Size = 2 * ((unsigned int)dwSize_1 - dwSize_2);
            memset(v8, 0, Size);
            v8 = (WCHAR *)((char *)v8 + Size);
            goto LABEL_11;
          }
          sub_180009B10(&lpExeName, (unsigned int)dwSize_1);
          v8 = (WCHAR *)v24;
          lpExeName_1 = lpExeName;
        }
LABEL_12:
        if ( QueryFullProcessImageNameW(hProcess, 0, lpExeName_1, &dwSize) )
          goto LABEL_13;
      }
      v8 = &lpExeName_1[dwSize_1];
LABEL_11:
      v24 = v8;
      goto LABEL_12;
    }
LABEL_13:
    sub_180007910(Src, lpExeName_1);
    n7 = n7_1;
    n8 = n8_2;
LABEL_14:
    CloseHandle(hProcess);
    if ( lpExeName_1 )
    {
      lpExeName_2 = lpExeName_1;
      if ( (unsigned __int64)(2 * ((v25 - (__int64)lpExeName_1) >> 1)) >= 0x1000 )
      {
        lpExeName_1 = (WCHAR *)*((_QWORD *)lpExeName_1 - 1);
        if ( (unsigned __int64)((char *)lpExeName_2 - (char *)lpExeName_1 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(lpExeName_1);
    }
  }
  *(_QWORD *)p_C = 0;
  *((_QWORD *)p_C + 2) = 0;
  *((_QWORD *)p_C + 3) = 0;
  Src_1 = Src;
  v14 = (char *)Src[0];
  if ( n7 >= 8 )
    Src_1 = (void **)Src[0];
  if ( n8 < 8 )
  {
    *(_OWORD *)p_C = *(_OWORD *)Src_1;
    *((_QWORD *)p_C + 2) = n8;
    *((_QWORD *)p_C + 3) = 7;
    p_C_1 = p_C;
LABEL_23:
    p_C_2 = p_C;
    p_C_3 = &p_C[n8];
    goto LABEL_24;
  }
  n8_1 = n8 | 7;
  if ( (n8 | 7) > 0x7FFFFFFFFFFFFFFELL )
    n8_1 = 0x7FFFFFFFFFFFFFFELL;
  v21 = (void *)sub_180007CF0(v5, n8_1 + 1);
  *(_QWORD *)p_C = v21;
  memcpy(v21, Src_1, 2 * n8 + 2);
  *((_QWORD *)p_C + 2) = n8;
  *((_QWORD *)p_C + 3) = n8_1;
  p_C_1 = p_C;
  if ( n8_1 < 8 )
    goto LABEL_23;
  p_C_2 = *(wint_t **)p_C;
  p_C_1 = *(wint_t **)p_C;
  p_C_3 = (wint_t *)(*(_QWORD *)p_C + 2 * n8);
LABEL_24:
  if ( p_C_2 != p_C_3 )
  {
    v18 = (char *)p_C_1 - (char *)p_C_2;
    do
    {
      *(wint_t *)((char *)p_C_2 + v18) = towlower(*p_C_2);
      ++p_C_2;
    }
    while ( p_C_2 != p_C_3 );
  }
  if ( n7 >= 8 )
  {
    v19 = v14;
    if ( 2 * n7 + 2 >= 0x1000 )
    {
      v14 = (char *)*((_QWORD *)v14 - 1);
      if ( (unsigned __int64)(v19 - v14 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v14);
  }
  return p_C;
}
