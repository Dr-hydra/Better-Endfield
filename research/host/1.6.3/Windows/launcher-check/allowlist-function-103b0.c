// Hidden C++ exception states: #wind=13
void __fastcall sub_1800103B0(__int64 a1, int a2, __int64 a3, int a4)
{
  _OWORD *v5; // rdi
  unsigned __int64 n8; // rsi
  __int64 v9; // rbx
  int pid; // eax
  DWORD dwProcessId; // eax
  wint_t *WideCharStr_1; // rbx
  void *v13; // rcx
  void *v14; // rcx
  __int64 v15; // rax
  _QWORD *i_1; // rdi
  void *v17; // rcx
  _QWORD *v18; // rsi
  __int64 v19; // rcx
  __int16 n182; // ax
  _BYTE *i; // rbx
  size_t n0xD; // rbx
  unsigned __int64 v23; // r15
  char *v24; // rbx
  _BYTE *v25; // r8
  size_t Size_1; // rcx
  __int64 v27; // rbx
  size_t Size_2; // rdi
  char *v29; // rax
  size_t Size_3; // r8
  _QWORD *v31; // rbx
  _BYTE *p_C_2; // r10
  unsigned __int64 n8_2; // r9
  __int64 v34; // rbx
  unsigned int v35; // r8d
  void **p_C_1; // rax
  void **p_C_3; // rax
  void *v38; // rcx
  void *v39; // rdi
  _QWORD *v40; // rbx
  void *v41; // rcx
  _QWORD *v42; // rax
  void *v43; // rcx
  unsigned int v44[4]; // [rsp+30h] [rbp-D0h] BYREF
  __int64 v45[2]; // [rsp+40h] [rbp-C0h] BYREF
  void *p_C[2]; // [rsp+50h] [rbp-B0h] BYREF
  unsigned __int64 n8_3; // [rsp+60h] [rbp-A0h]
  unsigned __int64 n8_1; // [rsp+68h] [rbp-98h]
  WCHAR WideCharStr[8]; // [rsp+70h] [rbp-90h] BYREF
  __int128 v50; // [rsp+80h] [rbp-80h]
  unsigned int p_si128[4]; // [rsp+90h] [rbp-70h] BYREF
  __int64 v52; // [rsp+A0h] [rbp-60h]
  char v53; // [rsp+A8h] [rbp-58h]
  void *v54[2]; // [rsp+B0h] [rbp-50h] BYREF
  void *Src[2]; // [rsp+C0h] [rbp-40h]
  char *v56; // [rsp+D0h] [rbp-30h]
  size_t Size; // [rsp+D8h] [rbp-28h]
  __int128 v58; // [rsp+E0h] [rbp-20h] BYREF
  __int128 v59; // [rsp+F0h] [rbp-10h] BYREF
  __int128 v60; // [rsp+100h] [rbp+0h]
  void *v61; // [rsp+110h] [rbp+10h]
  __int128 v62; // [rsp+120h] [rbp+20h] BYREF
  void *v63[2]; // [rsp+130h] [rbp+30h]
  void *v64[2]; // [rsp+140h] [rbp+40h]
  void *v65[2]; // [rsp+150h] [rbp+50h]
  __int64 v66; // [rsp+160h] [rbp+60h]
  __int64 n1024; // [rsp+168h] [rbp+68h]
  int v68; // [rsp+170h] [rbp+70h]
  __int64 v69; // [rsp+178h] [rbp+78h]
  _QWORD v70[2]; // [rsp+180h] [rbp+80h] BYREF
  void *v71[2]; // [rsp+190h] [rbp+90h]
  __int128 v72; // [rsp+1A0h] [rbp+A0h]
  __int64 n512; // [rsp+1B0h] [rbp+B0h]
  int n324; // [rsp+1B8h] [rbp+B8h]
  char v75; // [rsp+1BCh] [rbp+BCh]

  v5 = (_OWORD *)a3;
  if ( !*(_BYTE *)(a1 + 152) )
  {
    *(_QWORD *)WideCharStr = 0;
    v50 = 0u;
    n8 = *(_QWORD *)(a3 + 16);
    if ( *(_QWORD *)(a3 + 24) >= 8u )
      v5 = *(_OWORD **)a3;
    if ( n8 >= 8 )
    {
      v9 = n8 | 7;
      if ( (n8 | 7) > 0x7FFFFFFFFFFFFFFELL )
        v9 = 0x7FFFFFFFFFFFFFFELL;
      *(_QWORD *)WideCharStr = sub_180007CF0(a1, v9 + 1);
      memcpy(*(void **)WideCharStr, v5, 2 * n8 + 2);
      *((_QWORD *)&v50 + 1) = v9;
    }
    else
    {
      *(_OWORD *)WideCharStr = *v5;
      *((_QWORD *)&v50 + 1) = 7;
    }
    *(_QWORD *)&v50 = n8;
    if ( !n8 )
    {
      pid = getpid();
      dwProcessId = sub_180008C70(pid);
      WideCharStr_1 = sub_180008D00((wint_t *)p_C, dwProcessId);
      if ( WideCharStr != WideCharStr_1 )
      {
        if ( *((_QWORD *)&v50 + 1) >= 8u )
        {
          v13 = *(void **)WideCharStr;
          if ( (unsigned __int64)(2LL * *((_QWORD *)&v50 + 1) + 2) >= 0x1000 )
          {
            v13 = *(void **)(*(_QWORD *)WideCharStr - 8LL);
            if ( (unsigned __int64)(*(_QWORD *)WideCharStr - (_QWORD)v13 - 8LL) > 0x1F )
LABEL_19:
              invalid_parameter_noinfo_noreturn();
          }
          j_j_free(v13);
        }
        *(_QWORD *)&v50 = 0;
        *((_QWORD *)&v50 + 1) = 7;
        WideCharStr[0] = 0;
        *(_OWORD *)WideCharStr = *(_OWORD *)WideCharStr_1;
        v50 = *((_OWORD *)WideCharStr_1 + 1);
        *((_QWORD *)WideCharStr_1 + 2) = 0;
        *((_QWORD *)WideCharStr_1 + 3) = 7;
        *WideCharStr_1 = 0;
      }
      if ( n8_1 >= 8 )
      {
        v14 = p_C[0];
        if ( 2 * n8_1 + 2 >= 0x1000 )
        {
          v14 = (void *)*((_QWORD *)p_C[0] - 1);
          if ( (unsigned __int64)((char *)p_C[0] - (char *)v14 - 8) > 0x1F )
            goto LABEL_19;
        }
        j_j_free(v14);
      }
    }
    *(_BYTE *)(a1 + 152) = 1;
    *(_DWORD *)(a1 + 156) = a2;
    v15 = sub_180008600(p_C, WideCharStr);
    i_1 = (_QWORD *)(a1 + 160);
    sub_180002C00(a1 + 160, v15);
    if ( n8_1 >= 0x10 )
    {
      v17 = p_C[0];
      if ( n8_1 + 1 >= 0x1000 )
      {
        v17 = (void *)*((_QWORD *)p_C[0] - 1);
        if ( (unsigned __int64)((char *)p_C[0] - (char *)v17 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v17);
    }
    *(_DWORD *)(a1 + 192) = a4;
    v62 = 0;
    HIWORD(v62) = 0;
    *(_OWORD *)v63 = 0;
    *(_OWORD *)v64 = 0;
    *(_OWORD *)v65 = 0;
    v66 = 0;
    n1024 = 1024;
    v68 = 0;
    v69 = 0;
    v18 = operator new(0x28u);
    *v18 = 0;
    v18[1] = 0x10000;
    v18[2] = 0;
    v18[3] = 0;
    v18[4] = 0;
    v63[0] = v18;
    v63[1] = v18;
    v62 = 0;
    HIWORD(v62) = 3;
    v59 = 0;
    HIWORD(v59) = 3;
    v58 = 0;
    HIWORD(v58) = 3;
    *(_OWORD *)v45 = 0;
    HIWORD(v45[1]) = 1029;
    v45[1] = (unsigned __int64)"code" | v45[1] & 0xFFFF000000000000uLL;
    LODWORD(v45[0]) = 4;
    *(_QWORD *)&v44[2] = 0x1F6000000000000LL;
    *(_QWORD *)v44 = 2003;
    sub_180002920(&v62, v45, v44, v18);
    *(_OWORD *)v45 = 0;
    HIWORD(v45[1]) = 1029;
    v45[1] = (unsigned __int64)"code" | v45[1] & 0xFFFF000000000000uLL;
    LODWORD(v45[0]) = 4;
    *(_OWORD *)v44 = 0;
    *(_QWORD *)v44 = *(_DWORD *)(a1 + 156) == 0;
    HIWORD(v44[3]) = 502;
    sub_180002920(&v59, v45, v44, v18);
    if ( !*(_DWORD *)(a1 + 156) )
    {
      v19 = *(int *)(a1 + 192);
      *(_OWORD *)v45 = 0;
      HIWORD(v45[1]) = 1029;
      v45[1] = (unsigned __int64)"errorCode" | v45[1] & 0xFFFF000000000000uLL;
      LODWORD(v45[0]) = 9;
      *(_QWORD *)&v44[2] = 0;
      *(_QWORD *)v44 = v19;
      n182 = 182;
      if ( (int)v19 >= 0 )
        n182 = 502;
      HIWORD(v44[3]) = n182;
      sub_180002920(&v58, v45, v44, v18);
    }
    if ( *(_QWORD *)(a1 + 184) >= 0x10u )
      i_1 = (_QWORD *)*i_1;
    *(_OWORD *)v45 = 0;
    for ( i = i_1; *i; ++i )
      ;
    n0xD = (unsigned int)((_DWORD)i - (_DWORD)i_1);
    if ( (unsigned int)n0xD > 0xD )
    {
      HIWORD(v45[1]) = 3077;
      LODWORD(v45[0]) = n0xD;
      v23 = sub_180002DB0(v18, (unsigned int)(n0xD + 1));
      v45[1] = v23 | v45[1] & 0xFFFF000000000000uLL;
    }
    else
    {
      HIWORD(v45[1]) = 7173;
      BYTE5(v45[1]) = 13 - n0xD;
      v23 = (unsigned __int64)v45;
    }
    memcpy((void *)v23, i_1, n0xD);
    *(_BYTE *)(v23 + n0xD) = 0;
    *(_OWORD *)v44 = 0;
    HIWORD(v44[3]) = 1029;
    *(_QWORD *)&v44[2] = (unsigned __int64)"filePath" | *(_QWORD *)&v44[2] & 0xFFFF000000000000uLL;
    v44[0] = 8;
    sub_180002920(&v58, v44, v45, v18);
    *(_OWORD *)v44 = 0;
    HIWORD(v44[3]) = 1029;
    *(_QWORD *)&v44[2] = (unsigned __int64)"msg" | *(_QWORD *)&v44[2] & 0xFFFF000000000000uLL;
    v44[0] = 3;
    sub_180002920(&v59, v44, &v58, v18);
    *(_OWORD *)v44 = 0;
    HIWORD(v44[3]) = 1029;
    *(_QWORD *)&v44[2] = (unsigned __int64)"msg" | *(_QWORD *)&v44[2] & 0xFFFF000000000000uLL;
    v44[0] = 3;
    sub_180002920(&v62, v44, &v59, v18);
    *(_OWORD *)v54 = 0;
    *(_OWORD *)Src = 0;
    v56 = 0;
    Size = 256;
    v70[0] = v54;
    v70[1] = 0;
    *(_OWORD *)v71 = 0;
    v72 = 0;
    n512 = 512;
    n324 = 324;
    v75 = 0;
    sub_1800031F0(&v62, v70);
    v24 = (char *)Src[1];
    if ( (char *)Src[1] + 1 > v56 )
    {
      v25 = Src[0];
      if ( Src[0] )
      {
        Size_1 = ((unsigned __int64)(v56 - (char *)Src[0] + 1) >> 1) + v56 - (char *)Src[0];
      }
      else
      {
        if ( !v54[0] )
        {
          v54[0] = operator new((unsigned int)(LODWORD(Src[0]) + 1));
          v54[1] = v54[0];
          v24 = (char *)Src[1];
          v25 = Src[0];
        }
        Size_1 = Size;
      }
      v27 = v24 - v25;
      Size_2 = v27 + 1;
      if ( Size_1 >= v27 + 1 )
        Size_2 = Size_1;
      if ( Size_2 )
      {
        v29 = (char *)realloc(v25, Size_2);
      }
      else
      {
        free(v25);
        v29 = 0;
      }
      Src[0] = v29;
      v24 = &v29[v27];
      v56 = &v29[Size_2];
    }
    Src[1] = v24 + 1;
    *v24 = 0;
    --Src[1];
    Size_3 = -1;
    do
      ++Size_3;
    while ( *((_BYTE *)Src[0] + Size_3) );
    v31 = (_QWORD *)(a1 + 200);
    sub_180002E70((void *)(a1 + 200), Src[0], Size_3);
    if ( *(_QWORD *)(a1 + 224) >= 0x10u )
      v31 = (_QWORD *)*v31;
    v45[0] = (__int64)v31;
    *(__m128i *)p_si128 = _mm_load_si128((const __m128i *)&xmmword_18004E1E0);
    v52 = 0x3D3F8FE0D9FE504LL;
    v53 = -103;
    p_C[0] = 0;
    n8_3 = 0;
    n8_1 = 15;
    p_C_2 = operator new(0x20u);
    n8_1 = 31;
    *p_C_2 = p_C[0];
    p_C[0] = p_C_2;
    n8_2 = 0;
    n8_3 = 0;
    v34 = 0;
    while ( 1 )
    {
      v35 = 0x54C32DD8u >> (8 * ((1LL - (_QWORD)p_si128 + (unsigned __int8)p_si128 + v34) & 3));
      LOBYTE(v35) = *((_BYTE *)p_si128 + v34) ^ (78 - 125 * v34) ^ (0xE83B4B22 >> (8 * (v34 & 3))) ^ v35;
      if ( n8_2 >= n8_1 )
      {
        sub_180009740(p_C);
      }
      else
      {
        n8_3 = n8_2 + 1;
        p_C_1 = p_C;
        if ( n8_1 >= 0x10 )
          p_C_1 = (void **)p_C_2;
        *((_BYTE *)p_C_1 + n8_2) = v35;
        *((_BYTE *)p_C_1 + n8_2 + 1) = 0;
      }
      if ( (unsigned __int64)++v34 >= 0x18 )
        break;
      n8_2 = n8_3;
      p_C_2 = p_C[0];
    }
    *(_QWORD *)&v60 = &unk_18004B9FB;
    DWORD2(v60) = 0;
    v61 = &unk_18004B9FB;
    if ( *(_QWORD *)&xmmword_180058068 )
    {
      p_C_3 = p_C;
      if ( n8_1 >= 0x10 )
        p_C_3 = (void **)p_C[0];
      *(_QWORD *)v44 = p_C_3;
      *(_QWORD *)&v44[2] = n8_3;
      *(_OWORD *)p_si128 = v60;
      v52 = (__int64)v61;
      sub_18001B7E0(xmmword_180058068, (unsigned int)p_si128, v35, (unsigned int)v44, (__int64)v45);
    }
    if ( n8_1 >= 0x10 )
    {
      v38 = p_C[0];
      if ( n8_1 + 1 >= 0x1000 )
      {
        v38 = (void *)*((_QWORD *)p_C[0] - 1);
        if ( (unsigned __int64)((char *)p_C[0] - (char *)v38 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v38);
    }
    free(v71[1]);
    j_j_free(v71[0]);
    free(Src[0]);
    j_j_free(v54[1]);
    v39 = v63[1];
    if ( v63[1] )
    {
      if ( *(_QWORD *)v63[1] )
      {
        v40 = *(_QWORD **)v63[1];
        do
        {
          v41 = v40;
          if ( v40 == *((_QWORD **)v39 + 2) )
            break;
          v40 = (_QWORD *)v40[2];
          free(v41);
          *(_QWORD *)v39 = v40;
        }
        while ( v40 );
      }
      v42 = *(_QWORD **)v39;
      if ( *(_QWORD *)v39 && v42 == *((_QWORD **)v39 + 2) )
        v42[1] = 0;
      j_j_free(*((void **)v39 + 4));
      j_j_free(v39);
    }
    free(v65[0]);
    j_j_free(v64[1]);
    if ( *((_QWORD *)&v50 + 1) >= 8u )
    {
      v43 = *(void **)WideCharStr;
      if ( (unsigned __int64)(2LL * *((_QWORD *)&v50 + 1) + 2) >= 0x1000 )
      {
        v43 = *(void **)(*(_QWORD *)WideCharStr - 8LL);
        if ( (unsigned __int64)(*(_QWORD *)WideCharStr - (_QWORD)v43 - 8LL) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v43);
    }
  }
}
