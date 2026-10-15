// Hidden C++ exception states: #wind=5
void __fastcall sub_18002D420(_BYTE *Src)
{
  size_t Size; // r8
  __int64 Srca_1; // rdi
  void *v3; // rcx
  void *v4; // rax
  __int64 v5; // rax
  __int128 v6; // xmm6
  __int128 v7; // xmm7
  void *v8; // rcx
  void *v9; // rcx
  void **Srca_2; // rdx
  __int64 v11; // rcx
  void *v12; // rcx
  __int64 v13; // rax
  void *v14; // rdx
  _BYTE *v15; // rcx
  void *v16; // rcx
  void *v17; // rcx
  void *Srca[2]; // [rsp+20h] [rbp-E8h] BYREF
  __int128 v19; // [rsp+30h] [rbp-D8h]
  void *WideCharStr[2]; // [rsp+40h] [rbp-C8h] BYREF
  void *v21; // [rsp+50h] [rbp-B8h]
  unsigned __int64 n8; // [rsp+58h] [rbp-B0h]
  void *lpMultiByteStr[2]; // [rsp+60h] [rbp-A8h] BYREF
  CHAR MultiByteStr[16]; // [rsp+70h] [rbp-98h] BYREF
  __int64 v25; // [rsp+80h] [rbp-88h]
  unsigned __int64 n15; // [rsp+88h] [rbp-80h]
  __int128 v27; // [rsp+90h] [rbp-78h]
  void *v28; // [rsp+A0h] [rbp-68h]
  void *v29; // [rsp+A8h] [rbp-60h] BYREF
  unsigned __int64 n0x10; // [rsp+C0h] [rbp-48h]
  void **p_??_7exception@std@@6B@; // [rsp+C8h] [rbp-40h] BYREF
  __int64 v32; // [rsp+D0h] [rbp-38h] BYREF

  *(_QWORD *)MultiByteStr = 0;
  v25 = 0;
  n15 = 15;
  Size = -1;
  do
    ++Size;
  while ( Src[Size] );
  sub_180002E70(MultiByteStr, Src, Size);
  Srca[0] = 0;
  *(_QWORD *)&v19 = 0;
  *((_QWORD *)&v19 + 1) = 7;
  if ( !v25 )
    goto LABEL_12;
  Srca_1 = sub_1800087D0(WideCharStr, MultiByteStr);
  if ( Srca != (void **)Srca_1 )
  {
    *(_QWORD *)&v19 = 0;
    *((_QWORD *)&v19 + 1) = 7;
    LOWORD(Srca[0]) = 0;
    *(_OWORD *)Srca = *(_OWORD *)Srca_1;
    v19 = *(_OWORD *)(Srca_1 + 16);
    *(_QWORD *)(Srca_1 + 16) = 0;
    *(_QWORD *)(Srca_1 + 24) = 7;
    *(_WORD *)Srca_1 = 0;
  }
  if ( n8 >= 8 )
  {
    v3 = WideCharStr[0];
    if ( 2 * n8 + 2 >= 0x1000 )
    {
      v3 = (void *)*((_QWORD *)WideCharStr[0] - 1);
      if ( (unsigned __int64)((char *)WideCharStr[0] - (char *)v3 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v3);
  }
  sub_180007570(Srca, L"\\sdklogs\\OneChannelLC.log");
  if ( !(_QWORD)v19 )
  {
LABEL_12:
    v4 = (void *)sub_180008430(WideCharStr);
    v5 = sub_180007570(v4, L"\\sdklogs\\OneChannelLC.log");
    v6 = *(_OWORD *)v5;
    v7 = *(_OWORD *)(v5 + 16);
    *(_QWORD *)(v5 + 16) = 0;
    *(_QWORD *)(v5 + 24) = 7;
    *(_WORD *)v5 = 0;
    if ( *((_QWORD *)&v19 + 1) >= 8u )
    {
      v8 = Srca[0];
      if ( (unsigned __int64)(2LL * *((_QWORD *)&v19 + 1) + 2) >= 0x1000 )
      {
        v8 = (void *)*((_QWORD *)Srca[0] - 1);
        if ( (unsigned __int64)((char *)Srca[0] - (char *)v8 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v8);
    }
    *(_OWORD *)Srca = v6;
    v19 = v7;
    if ( n8 >= 8 )
    {
      v9 = WideCharStr[0];
      if ( 2 * n8 + 2 >= 0x1000 )
      {
        v9 = (void *)*((_QWORD *)WideCharStr[0] - 1);
        if ( (unsigned __int64)((char *)WideCharStr[0] - (char *)v9 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v9);
    }
  }
  Srca_2 = Srca;
  if ( *((_QWORD *)&v19 + 1) >= 8u )
    Srca_2 = (void **)Srca[0];
  try
  {
    WideCharStr[0] = 0;
    v21 = 0;
    n8 = 7;
    sub_180007910(WideCharStr, Srca_2);
    sub_18002CA20(WideCharStr);
    if ( n8 >= 8 )
    {
      v12 = WideCharStr[0];
      if ( 2 * n8 + 2 >= 0x1000 )
      {
        v12 = (void *)*((_QWORD *)WideCharStr[0] - 1);
        if ( (unsigned __int64)((char *)WideCharStr[0] - (char *)v12 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v12);
    }
    v13 = sub_18002D880(v11, &v29);
    *(_QWORD *)&v27 = &unk_18004B9FB;
    DWORD2(v27) = 0;
    v28 = &unk_18004B9FB;
    if ( *(_QWORD *)&xmmword_180058068 )
    {
      v14 = (void *)v13;
      if ( *(_QWORD *)(v13 + 24) >= 0x10u )
        v14 = *(void **)v13;
      lpMultiByteStr[0] = v14;
      lpMultiByteStr[1] = *(void **)(v13 + 16);
      *(_OWORD *)WideCharStr = v27;
      v21 = v28;
      sub_18001A5A0(*(_QWORD *)&xmmword_180058068, WideCharStr, 2, lpMultiByteStr);
    }
    if ( n0x10 >= 0x10 )
    {
      v15 = v29;
      if ( n0x10 + 1 >= 0x1000 )
      {
        v15 = (_BYTE *)*((_QWORD *)v29 - 1);
        if ( (unsigned __int64)((_BYTE *)v29 - v15 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v15);
    }
  }
  catch ( std::exception p_??_7exception@std@@6B@ )
  {
    p_??_7exception@std@@6B@ = &std::exception::`vftable';
    _std_exception_destroy(&v32);
  }
  if ( *((_QWORD *)&v19 + 1) >= 8u )
  {
    v16 = Srca[0];
    if ( (unsigned __int64)(2LL * *((_QWORD *)&v19 + 1) + 2) >= 0x1000 )
    {
      v16 = (void *)*((_QWORD *)Srca[0] - 1);
      if ( (unsigned __int64)((char *)Srca[0] - (char *)v16 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v16);
  }
  *(_QWORD *)&v19 = 0;
  *((_QWORD *)&v19 + 1) = 7;
  LOWORD(Srca[0]) = 0;
  if ( n15 >= 0x10 )
  {
    v17 = *(void **)MultiByteStr;
    if ( n15 + 1 >= 0x1000 )
    {
      v17 = *(void **)(*(_QWORD *)MultiByteStr - 8LL);
      if ( (unsigned __int64)(*(_QWORD *)MultiByteStr - (_QWORD)v17 - 8LL) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v17);
  }
}
