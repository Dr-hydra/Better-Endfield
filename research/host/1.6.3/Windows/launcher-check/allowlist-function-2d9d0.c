__int64 __fastcall OCL_1(_BYTE *a1, _BYTE *a2)
{
  size_t Size; // rbx
  __int64 v4; // rax
  __int64 v5; // rbx
  void *v6; // rcx
  bool v7; // cc
  _QWORD *v8; // r14
  bool v9; // bl
  void *v10; // rcx
  _QWORD *v11; // rax
  bool v12; // zf
  __int64 v13; // rbx
  void *v15; // rcx
  void *MultiByteStr[2]; // [rsp+20h] [rbp-20h] BYREF
  __int64 v17; // [rsp+30h] [rbp-10h]
  unsigned __int64 n7; // [rsp+38h] [rbp-8h]

  sub_18002D420(a1);
  Size = -1;
  v4 = -1;
  do
    ++v4;
  while ( a2[v4] );
  if ( !v4 )
  {
    n7 = 7;
    MultiByteStr[0] = 0;
    v17 = 0;
    v5 = sub_18002D230();
    sub_180007910(MultiByteStr, &unk_18004CCDC);
    sub_1800103B0(v5, 0, (__int64)MultiByteStr, 10000);
    if ( n7 >= 8 )
    {
      v6 = MultiByteStr[0];
      if ( 2 * n7 + 2 >= 0x1000 )
      {
        v6 = (void *)*((_QWORD *)MultiByteStr[0] - 1);
        v7 = (unsigned __int64)((char *)MultiByteStr[0] - (char *)v6 - 8) <= 0x1F;
        goto LABEL_18;
      }
      goto LABEL_19;
    }
    return 10000;
  }
  n7 = 15;
  MultiByteStr[0] = 0;
  v8 = (_QWORD *)sub_18002D230();
  v17 = 0;
  do
    ++Size;
  while ( a2[Size] );
  sub_180002E70(MultiByteStr, a2, Size);
  v9 = sub_18000EA70(v8, MultiByteStr) == 0;
  if ( n7 >= 0x10 )
  {
    v10 = MultiByteStr[0];
    if ( n7 + 1 >= 0x1000 )
    {
      v10 = (void *)*((_QWORD *)MultiByteStr[0] - 1);
      if ( (unsigned __int64)((char *)MultiByteStr[0] - (char *)v10 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v10);
  }
  v11 = (_QWORD *)sub_18002D230();
  v12 = !v9;
  v13 = (__int64)v11;
  if ( !v12 )
  {
    MultiByteStr[0] = 0;
    v17 = 0;
    n7 = 7;
    sub_180007910(MultiByteStr, &unk_18004CCDC);
    sub_1800103B0(v13, 0, (__int64)MultiByteStr, 10000);
    if ( n7 >= 8 )
    {
      v6 = MultiByteStr[0];
      if ( 2 * n7 + 2 >= 0x1000 )
      {
        v6 = (void *)*((_QWORD *)MultiByteStr[0] - 1);
        v7 = (unsigned __int64)((char *)MultiByteStr[0] - (char *)v6 - 8) <= 0x1F;
LABEL_18:
        if ( v7 )
          goto LABEL_19;
LABEL_25:
        invalid_parameter_noinfo_noreturn();
      }
LABEL_19:
      j_j_free(v6);
    }
    return 10000;
  }
  if ( v11[1] == *v11 )
  {
    MultiByteStr[0] = 0;
    v17 = 0;
    n7 = 7;
    sub_180007910(MultiByteStr, L"Could not found launch check config");
    sub_1800103B0(v13, 0, (__int64)MultiByteStr, 10000);
    if ( n7 >= 8 )
    {
      v15 = MultiByteStr[0];
      if ( 2 * n7 + 2 >= 0x1000 )
      {
        v15 = (void *)*((_QWORD *)MultiByteStr[0] - 1);
        if ( (unsigned __int64)((char *)MultiByteStr[0] - (char *)v15 - 8) > 0x1F )
          goto LABEL_25;
      }
      j_j_free(v15);
    }
  }
  else
  {
    sub_1800144D0((__int64)v11);
  }
  return *(unsigned int *)(sub_18002D230() + 192);
}
