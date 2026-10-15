// Hidden C++ exception states: #wind=16
char sub_18000DB10()
{
  unsigned __int64 n15_2; // r9
  unsigned __int64 n15; // r10
  __int64 v2; // rbx
  char v3; // r8
  LPCSTR *v4; // rax
  const CHAR *v5; // rcx
  CHAR *v6; // rcx
  unsigned __int64 n15_5; // r9
  unsigned __int64 n15_4; // r10
  __int64 v9; // rbx
  char v10; // r8
  LPCSTR *v11; // rax
  const CHAR *v12; // rcx
  HMODULE Library; // rax
  CHAR *v14; // rcx
  unsigned __int64 n15_8; // r9
  unsigned __int64 n15_6; // r10
  __int64 v17; // rbx
  char v18; // r8
  LPCSTR *v19; // rax
  char n3; // bl
  const CHAR *v21; // rdx
  CHAR *Src_2; // r10
  unsigned __int64 n15_11; // r9
  __int64 v24; // rbx
  char v25; // r8
  LPCSTR *Src_1; // rax
  const CHAR *Src_3; // rdx
  CHAR *v28; // r10
  unsigned __int64 n15_14; // r9
  __int64 v30; // rbx
  char v31; // r8
  LPCSTR *v32; // rax
  const CHAR *v33; // rdx
  __int64 v34; // rcx
  CHAR *v35; // r10
  unsigned __int64 n15_17; // r9
  __int64 v37; // rbx
  char v38; // r8
  LPCSTR *v39; // rax
  const CHAR *v40; // rdx
  __int64 v41; // rcx
  CHAR *v42; // r10
  unsigned __int64 n15_20; // r9
  __int64 v44; // rbx
  char v45; // r8
  LPCSTR *v46; // rax
  const CHAR *v47; // rdx
  unsigned __int64 n15_23; // r9
  unsigned __int64 n15_22; // r10
  __int64 v50; // rbx
  char v51; // r8
  LPCSTR *v52; // rax
  const CHAR *v53; // rdx
  unsigned __int64 n15_26; // r9
  unsigned __int64 n15_24; // r10
  __int64 v56; // rbx
  char v57; // r8
  LPCSTR *v58; // rax
  const CHAR *v59; // rdx
  __int64 v60; // rcx
  __int64 v61; // rax
  char v62; // di
  CHAR *v63; // rcx
  CHAR *v64; // rcx
  CHAR *v65; // rcx
  CHAR *v66; // rcx
  CHAR *v67; // rcx
  CHAR *v68; // rcx
  CHAR *v69; // rcx
  __m128i si128; // [rsp+20h] [rbp-E0h] BYREF
  int v72; // [rsp+30h] [rbp-D0h]
  int v73; // [rsp+34h] [rbp-CCh]
  __int16 n14357; // [rsp+38h] [rbp-C8h]
  char n120; // [rsp+3Ah] [rbp-C6h]
  LPCSTR lpLibFileName[2]; // [rsp+40h] [rbp-C0h] BYREF
  unsigned __int64 n15_3; // [rsp+50h] [rbp-B0h]
  unsigned __int64 n15_1; // [rsp+58h] [rbp-A8h]
  LPCSTR v79[2]; // [rsp+60h] [rbp-A0h] BYREF
  unsigned __int64 n15_21; // [rsp+70h] [rbp-90h]
  unsigned __int64 n15_19; // [rsp+78h] [rbp-88h]
  LPCSTR v82[2]; // [rsp+80h] [rbp-80h] BYREF
  unsigned __int64 n15_18; // [rsp+90h] [rbp-70h]
  unsigned __int64 n15_16; // [rsp+98h] [rbp-68h]
  LPCSTR v85[2]; // [rsp+A0h] [rbp-60h] BYREF
  unsigned __int64 n15_15; // [rsp+B0h] [rbp-50h]
  unsigned __int64 n15_13; // [rsp+B8h] [rbp-48h]
  LPCSTR Src[2]; // [rsp+C0h] [rbp-40h] BYREF
  unsigned __int64 n15_12; // [rsp+D0h] [rbp-30h]
  unsigned __int64 n15_10; // [rsp+D8h] [rbp-28h]
  LPCSTR v91[2]; // [rsp+E0h] [rbp-20h] BYREF
  unsigned __int64 n15_27; // [rsp+F0h] [rbp-10h]
  unsigned __int64 n15_25; // [rsp+F8h] [rbp-8h]
  LPCSTR lpProcName[2]; // [rsp+100h] [rbp+0h] BYREF
  unsigned __int64 n15_9; // [rsp+110h] [rbp+10h]
  unsigned __int64 n15_7; // [rsp+118h] [rbp+18h]
  _BYTE v97[32]; // [rsp+120h] [rbp+20h] BYREF

  si128.m128i_i64[0] = 0x1BD0DCF5B96F69CELL;
  si128.m128i_i32[2] = 562204433;
  lpLibFileName[0] = 0;
  n15_2 = 0;
  n15_3 = 0;
  n15 = 15;
  n15_1 = 15;
  v2 = 0;
  while ( 1 )
  {
    v3 = si128.m128i_i8[v2]
       ^ (113 - 125 * v2)
       ^ (0x780A7983u >> (8 * (v2 & 3)))
       ^ (0x6B967F4Bu >> (8 * ((1LL - (_QWORD)&si128 + (unsigned __int8)&si128.m128i_u8[v2]) & 3)));
    if ( n15_2 >= n15 )
    {
      sub_180009740(lpLibFileName);
    }
    else
    {
      n15_3 = n15_2 + 1;
      v4 = lpLibFileName;
      if ( n15 >= 0x10 )
        v4 = (LPCSTR *)lpLibFileName[0];
      *((_BYTE *)v4 + n15_2) = v3;
      *((_BYTE *)v4 + n15_2 + 1) = 0;
    }
    if ( (unsigned __int64)++v2 >= 0xB )
      break;
    n15 = n15_1;
    n15_2 = n15_3;
  }
  v5 = (const CHAR *)lpLibFileName;
  if ( n15_1 >= 0x10 )
    v5 = lpLibFileName[0];
  hModule_0 = LoadLibraryExA(v5, 0, 0x800u);
  if ( n15_1 >= 0x10 )
  {
    v6 = (CHAR *)lpLibFileName[0];
    if ( n15_1 + 1 >= 0x1000 )
    {
      v6 = (CHAR *)*((_QWORD *)lpLibFileName[0] - 1);
      if ( (unsigned __int64)(lpLibFileName[0] - (LPCSTR)v6 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v6);
  }
  si128.m128i_i64[0] = 0xB6AA15EDAAAB1DC4uLL;
  si128.m128i_i32[2] = -1702819675;
  si128.m128i_i8[12] = -73;
  lpLibFileName[0] = 0;
  n15_5 = 0;
  n15_3 = 0;
  n15_4 = 15;
  n15_1 = 15;
  v9 = 0;
  while ( 1 )
  {
    v10 = si128.m128i_i8[v9]
        ^ (-126 - 125 * v9)
        ^ (0xFEBC2D2F >> (8 * (v9 & 3)))
        ^ (0xF15C3E2B >> (8 * ((1LL - (_QWORD)&si128 + (unsigned __int8)&si128.m128i_u8[v9]) & 3)));
    if ( n15_5 >= n15_4 )
    {
      sub_180009740(lpLibFileName);
    }
    else
    {
      n15_3 = n15_5 + 1;
      v11 = lpLibFileName;
      if ( n15_4 >= 0x10 )
        v11 = (LPCSTR *)lpLibFileName[0];
      *((_BYTE *)v11 + n15_5) = v10;
      *((_BYTE *)v11 + n15_5 + 1) = 0;
    }
    if ( (unsigned __int64)++v9 >= 0xC )
      break;
    n15_4 = n15_1;
    n15_5 = n15_3;
  }
  v12 = (const CHAR *)lpLibFileName;
  if ( n15_1 >= 0x10 )
    v12 = lpLibFileName[0];
  Library = LoadLibraryExA(v12, 0, 0x800u);
  hModule = Library;
  if ( n15_1 >= 0x10 )
  {
    v14 = (CHAR *)lpLibFileName[0];
    if ( n15_1 + 1 >= 0x1000 )
    {
      v14 = (CHAR *)*((_QWORD *)lpLibFileName[0] - 1);
      if ( (unsigned __int64)(lpLibFileName[0] - (LPCSTR)v14 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v14);
    Library = hModule;
  }
  if ( !hModule_0 || !Library )
    return 0;
  si128.m128i_i64[0] = 0x43023D386719321ELL;
  si128.m128i_i32[2] = 745346856;
  si128.m128i_i16[6] = 17238;
  si128.m128i_i8[14] = 19;
  lpProcName[0] = 0;
  n15_8 = 0;
  n15_9 = 0;
  n15_6 = 15;
  n15_7 = 15;
  v17 = 0;
  while ( 1 )
  {
    v18 = si128.m128i_i8[v17]
        ^ (-92 - 125 * v17)
        ^ (0x2AAE184Cu >> (8 * (v17 & 3)))
        ^ (0x7364A136u >> (8 * ((1LL - (_QWORD)&si128 + (unsigned __int8)&si128.m128i_u8[v17]) & 3)));
    if ( n15_8 >= n15_6 )
    {
      sub_180009740(lpProcName);
    }
    else
    {
      n15_9 = n15_8 + 1;
      v19 = lpProcName;
      if ( n15_6 >= 0x10 )
        v19 = (LPCSTR *)lpProcName[0];
      *((_BYTE *)v19 + n15_8) = v18;
      *((_BYTE *)v19 + n15_8 + 1) = 0;
    }
    if ( (unsigned __int64)++v17 >= 0xE )
      break;
    n15_6 = n15_7;
    n15_8 = n15_9;
  }
  n3 = 1;
  v21 = (const CHAR *)lpProcName;
  if ( n15_7 >= 0x10 )
    v21 = lpProcName[0];
  qword_180058970 = (__int64)GetProcAddress(hModule, v21);
  if ( !qword_180058970 )
    goto LABEL_114;
  si128 = _mm_load_si128((const __m128i *)&xmmword_18004E2B0);
  LOBYTE(v72) = 102;
  Src[0] = 0;
  n15_12 = 0;
  n15_10 = 15;
  Src_2 = (CHAR *)operator new(0x20u);
  n15_10 = 31;
  *Src_2 = (CHAR)Src[0];
  Src[0] = Src_2;
  n15_11 = 0;
  n15_12 = 0;
  v24 = 0;
  while ( 1 )
  {
    v25 = si128.m128i_i8[v24]
        ^ (-58 - 125 * v24)
        ^ (0xAFD014B9 >> (8 * (v24 & 3)))
        ^ (0xC660295D >> (8 * ((1LL - (_QWORD)&si128 + (unsigned __int8)&si128.m128i_u8[v24]) & 3)));
    if ( n15_11 >= n15_10 )
    {
      sub_180009740(Src);
    }
    else
    {
      n15_12 = n15_11 + 1;
      Src_1 = Src;
      if ( n15_10 >= 0x10 )
        Src_1 = (LPCSTR *)Src_2;
      *((_BYTE *)Src_1 + n15_11) = v25;
      *((_BYTE *)Src_1 + n15_11 + 1) = 0;
    }
    if ( (unsigned __int64)++v24 >= 0x10 )
      break;
    n15_11 = n15_12;
    Src_2 = (CHAR *)Src[0];
  }
  n3 = 3;
  Src_3 = (const CHAR *)Src;
  if ( n15_10 >= 0x10 )
    Src_3 = Src[0];
  ::Src = (__int64)GetProcAddress(hModule_0, Src_3);
  if ( !::Src )
    goto LABEL_114;
  si128 = _mm_load_si128((const __m128i *)&xmmword_18004E240);
  v72 = -905597488;
  v73 = -268400941;
  n14357 = -23333;
  n120 = 120;
  v85[0] = 0;
  n15_15 = 0;
  n15_13 = 15;
  v28 = (CHAR *)operator new(0x20u);
  n15_13 = 31;
  *v28 = (CHAR)v85[0];
  v85[0] = v28;
  n15_14 = 0;
  n15_15 = 0;
  v30 = 0;
  while ( 1 )
  {
    v31 = si128.m128i_i8[v30]
        ^ (112 - 125 * v30)
        ^ (0xEA5478D6 >> (8 * (v30 & 3)))
        ^ (0x9282C740 >> (8 * ((1LL - (_QWORD)&si128 + (unsigned __int8)&si128.m128i_u8[v30]) & 3)));
    if ( n15_14 >= n15_13 )
    {
      sub_180009740(v85);
    }
    else
    {
      n15_15 = n15_14 + 1;
      v32 = v85;
      if ( n15_13 >= 0x10 )
        v32 = (LPCSTR *)v28;
      *((_BYTE *)v32 + n15_14) = v31;
      *((_BYTE *)v32 + n15_14 + 1) = 0;
    }
    if ( (unsigned __int64)++v30 >= 0x1A )
      break;
    n15_14 = n15_15;
    v28 = (CHAR *)v85[0];
  }
  n3 = 7;
  v33 = (const CHAR *)v85;
  if ( n15_13 >= 0x10 )
    v33 = v85[0];
  qword_180058980 = (__int64)GetProcAddress(hModule_0, v33);
  if ( !qword_180058980 )
    goto LABEL_114;
  si128 = _mm_load_si128((const __m128i *)&xmmword_18004E190);
  v72 = -1284628460;
  v73 = -1989462506;
  n14357 = 14357;
  n120 = 19;
  v82[0] = 0;
  n15_18 = 0;
  n15_16 = 15;
  v35 = (CHAR *)sub_180003190(v34, 32);
  n15_16 = 31;
  *v35 = (CHAR)v82[0];
  v82[0] = v35;
  n15_17 = 0;
  n15_18 = 0;
  v37 = 0;
  while ( 1 )
  {
    v38 = si128.m128i_i8[v37]
        ^ (112 - 125 * v37)
        ^ (0x6C402641u >> (8 * (v37 & 3)))
        ^ (0xED5194B5 >> (8 * (((unsigned __int8)&si128.m128i_u8[v37] + 1LL - (_QWORD)&si128) & 3)));
    if ( n15_17 >= n15_16 )
    {
      sub_180009740(v82);
    }
    else
    {
      n15_18 = n15_17 + 1;
      v39 = v82;
      if ( n15_16 >= 0x10 )
        v39 = (LPCSTR *)v35;
      *((_BYTE *)v39 + n15_17) = v38;
      *((_BYTE *)v39 + n15_17 + 1) = 0;
    }
    if ( (unsigned __int64)++v37 >= 0x1A )
      break;
    n15_17 = n15_18;
    v35 = (CHAR *)v82[0];
  }
  n3 = 15;
  v40 = (const CHAR *)v82;
  if ( n15_16 >= 0x10 )
    v40 = v82[0];
  qword_180058988 = (__int64)GetProcAddress(hModule_0, v40);
  if ( !qword_180058988 )
    goto LABEL_114;
  si128 = _mm_load_si128((const __m128i *)&xmmword_18004E0A0);
  LOBYTE(v72) = -70;
  v79[0] = 0;
  n15_21 = 0;
  n15_19 = 15;
  v42 = (CHAR *)sub_180003190(v41, 32);
  n15_19 = 31;
  *v42 = (CHAR)v79[0];
  v79[0] = v42;
  n15_20 = 0;
  n15_21 = 0;
  v44 = 0;
  while ( 1 )
  {
    v45 = si128.m128i_i8[v44]
        ^ (-58 - 125 * v44)
        ^ (0xD994B47E >> (8 * (v44 & 3)))
        ^ (0xBE4832BC >> (8 * (((unsigned __int8)&si128.m128i_u8[v44] + 1LL - (_QWORD)&si128) & 3)));
    if ( n15_20 >= n15_19 )
    {
      sub_180009740(v79);
    }
    else
    {
      n15_21 = n15_20 + 1;
      v46 = v79;
      if ( n15_19 >= 0x10 )
        v46 = (LPCSTR *)v42;
      *((_BYTE *)v46 + n15_20) = v45;
      *((_BYTE *)v46 + n15_20 + 1) = 0;
    }
    if ( (unsigned __int64)++v44 >= 0x10 )
      break;
    n15_20 = n15_21;
    v42 = (CHAR *)v79[0];
  }
  n3 = 31;
  v47 = (const CHAR *)v79;
  if ( n15_19 >= 0x10 )
    v47 = v79[0];
  qword_180058990 = (__int64)GetProcAddress(hModule_0, v47);
  if ( !qword_180058990 )
    goto LABEL_114;
  si128.m128i_i64[0] = 0x950BB0DEB63DBBE5uLL;
  si128.m128i_i32[2] = -1660707363;
  si128.m128i_i16[6] = -6681;
  lpLibFileName[0] = 0;
  n15_23 = 0;
  n15_3 = 0;
  n15_22 = 15;
  n15_1 = 15;
  v50 = 0;
  while ( 1 )
  {
    v51 = si128.m128i_i8[v50]
        ^ (-109 - 125 * v50)
        ^ (0x30A9E514u >> (8 * (v50 & 3)))
        ^ (0x743A21EAu >> (8 * (((unsigned __int8)&si128.m128i_u8[v50] + 1LL - (_QWORD)&si128) & 3)));
    if ( n15_23 >= n15_22 )
    {
      sub_180009740(lpLibFileName);
    }
    else
    {
      n15_3 = n15_23 + 1;
      v52 = lpLibFileName;
      if ( n15_22 >= 0x10 )
        v52 = (LPCSTR *)lpLibFileName[0];
      *((_BYTE *)v52 + n15_23) = v51;
      *((_BYTE *)v52 + n15_23 + 1) = 0;
    }
    if ( (unsigned __int64)++v50 >= 0xD )
      break;
    n15_22 = n15_1;
    n15_23 = n15_3;
  }
  n3 = 63;
  v53 = (const CHAR *)lpLibFileName;
  if ( n15_1 >= 0x10 )
    v53 = lpLibFileName[0];
  qword_180058998 = (__int64)GetProcAddress(hModule_0, v53);
  if ( !qword_180058998 )
    goto LABEL_114;
  si128.m128i_i64[0] = 0x843A32A9973B2FBDuLL;
  si128.m128i_i32[2] = -464191101;
  si128.m128i_i16[6] = 17376;
  si128.m128i_i8[14] = 45;
  v91[0] = 0;
  n15_26 = 0;
  n15_27 = 0;
  n15_24 = 15;
  n15_25 = 15;
  v56 = 0;
  while ( 1 )
  {
    v57 = si128.m128i_i8[v56]
        ^ (-92 - 125 * v56)
        ^ (0xC620D9CE >> (8 * (v56 & 3)))
        ^ (0xC3B49408 >> (8 * (((unsigned __int8)&si128.m128i_u8[v56] + 1LL - (_QWORD)&si128) & 3)));
    if ( n15_26 >= n15_24 )
    {
      sub_180009740(v91);
    }
    else
    {
      n15_27 = n15_26 + 1;
      v58 = v91;
      if ( n15_24 >= 0x10 )
        v58 = (LPCSTR *)v91[0];
      *((_BYTE *)v58 + n15_26) = v57;
      *((_BYTE *)v58 + n15_26 + 1) = 0;
    }
    if ( (unsigned __int64)++v56 >= 0xE )
      break;
    n15_24 = n15_25;
    n15_26 = n15_27;
  }
  n3 = 127;
  v59 = (const CHAR *)v91;
  if ( n15_25 >= 0x10 )
    v59 = v91[0];
  qword_1800589A0 = (__int64)GetProcAddress(hModule_0, v59);
  if ( !qword_1800589A0 )
    goto LABEL_114;
  v61 = sub_18000E8D0(v60, v97);
  n3 = -1;
  if ( *(_QWORD *)(v61 + 24) >= 0x10u )
    v61 = *(_QWORD *)v61;
  qword_1800589A8 = (__int64)GetProcAddress(hModule_0, (LPCSTR)v61);
  if ( qword_1800589A8 )
    v62 = 1;
  else
LABEL_114:
    v62 = 0;
  if ( n3 < 0 )
  {
    n3 &= ~0x80u;
    sub_180002BA0(v97);
  }
  if ( (n3 & 0x40) != 0 )
  {
    n3 &= ~0x40u;
    if ( n15_25 >= 0x10 )
    {
      v63 = (CHAR *)v91[0];
      if ( n15_25 + 1 >= 0x1000 )
      {
        v63 = (CHAR *)*((_QWORD *)v91[0] - 1);
        if ( (unsigned __int64)(v91[0] - (LPCSTR)v63 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v63);
    }
    n15_27 = 0;
    n15_25 = 15;
    LOBYTE(v91[0]) = 0;
  }
  if ( (n3 & 0x20) != 0 )
  {
    n3 &= ~0x20u;
    if ( n15_1 >= 0x10 )
    {
      v64 = (CHAR *)lpLibFileName[0];
      if ( n15_1 + 1 >= 0x1000 )
      {
        v64 = (CHAR *)*((_QWORD *)lpLibFileName[0] - 1);
        if ( (unsigned __int64)(lpLibFileName[0] - (LPCSTR)v64 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v64);
    }
    n15_3 = 0;
    n15_1 = 15;
    LOBYTE(lpLibFileName[0]) = 0;
  }
  if ( (n3 & 0x10) != 0 )
  {
    n3 &= ~0x10u;
    if ( n15_19 >= 0x10 )
    {
      v65 = (CHAR *)v79[0];
      if ( n15_19 + 1 >= 0x1000 )
      {
        v65 = (CHAR *)*((_QWORD *)v79[0] - 1);
        if ( (unsigned __int64)(v79[0] - (LPCSTR)v65 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v65);
    }
    n15_21 = 0;
    n15_19 = 15;
    LOBYTE(v79[0]) = 0;
  }
  if ( (n3 & 8) != 0 )
  {
    n3 &= ~8u;
    if ( n15_16 >= 0x10 )
    {
      v66 = (CHAR *)v82[0];
      if ( n15_16 + 1 >= 0x1000 )
      {
        v66 = (CHAR *)*((_QWORD *)v82[0] - 1);
        if ( (unsigned __int64)(v82[0] - (LPCSTR)v66 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v66);
    }
    n15_18 = 0;
    n15_16 = 15;
    LOBYTE(v82[0]) = 0;
  }
  if ( (n3 & 4) != 0 )
  {
    n3 &= ~4u;
    if ( n15_13 >= 0x10 )
    {
      v67 = (CHAR *)v85[0];
      if ( n15_13 + 1 >= 0x1000 )
      {
        v67 = (CHAR *)*((_QWORD *)v85[0] - 1);
        if ( (unsigned __int64)(v85[0] - (LPCSTR)v67 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v67);
    }
    n15_15 = 0;
    n15_13 = 15;
    LOBYTE(v85[0]) = 0;
  }
  if ( (n3 & 2) != 0 )
  {
    n3 &= ~2u;
    if ( n15_10 >= 0x10 )
    {
      v68 = (CHAR *)Src[0];
      if ( n15_10 + 1 >= 0x1000 )
      {
        v68 = (CHAR *)*((_QWORD *)Src[0] - 1);
        if ( (unsigned __int64)(Src[0] - (LPCSTR)v68 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v68);
    }
    n15_12 = 0;
    n15_10 = 15;
    LOBYTE(Src[0]) = 0;
  }
  if ( (n3 & 1) != 0 && n15_7 >= 0x10 )
  {
    v69 = (CHAR *)lpProcName[0];
    if ( n15_7 + 1 >= 0x1000 )
    {
      v69 = (CHAR *)*((_QWORD *)lpProcName[0] - 1);
      if ( (unsigned __int64)(lpProcName[0] - (LPCSTR)v69 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v69);
  }
  return v62;
}
