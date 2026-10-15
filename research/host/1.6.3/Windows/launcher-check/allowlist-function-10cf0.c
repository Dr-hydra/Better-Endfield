// Hidden C++ exception states: #wind=79
__int64 __fastcall sub_180010CF0(__int64 a1, int a2)
{
  _BYTE *lpMultiByteStr_27; // r10
  char *n31_1; // r9
  __int64 v6; // rbx
  unsigned int n10; // edi
  unsigned int v8; // r8d
  void **lpMultiByteStr_26; // rax
  void **lpMultiByteStr_28; // rax
  void *v11; // rcx
  __int64 v12; // rax
  void *v13; // rcx
  __int64 v14; // rax
  LPSTR v15; // rcx
  void *v16; // rcx
  __int64 v17; // rax
  WCHAR *lpWideCharStr_8; // rax
  void *v19; // rcx
  void *v20; // rcx
  void *v21; // rcx
  void *v22; // rcx
  void *v23; // rcx
  void *v24; // rcx
  void *v25; // rcx
  void *v26; // rcx
  void *v27; // rcx
  const WCHAR *lpWideCharStr_3; // rcx
  int stats; // eax
  int v30; // eax
  int v31; // eax
  int n70; // eax
  const WCHAR *lpWideCharStr_7; // r15
  int cchWideChar_1; // r14d
  unsigned __int64 v35; // rax
  int v36; // ebx
  unsigned __int64 v37; // rax
  __int64 v38; // rdx
  LPSTR *lpMultiByteStr_22; // rax
  unsigned __int64 v40; // rsi
  LPSTR *lpMultiByteStr_21; // rdi
  char *v42; // rdi
  CHAR *lpMultiByteStr_23; // r9
  unsigned __int64 v44; // rax
  _BYTE *lpMultiByteStr_25; // r10
  char *n31_8; // r9
  __int64 v47; // rbx
  char v48; // r8
  void **lpMultiByteStr_24; // rax
  void *v50; // rcx
  LPSTR v51; // rcx
  unsigned __int8 v52; // bl
  LPCWCH *lpWideCharStr_4; // rdx
  __int64 v54; // rbx
  __int64 v55; // rcx
  __int64 v56; // rax
  void *v57; // rcx
  unsigned __int64 n15_1; // r9
  unsigned __int64 n15; // r10
  __int64 v60; // rbx
  char v61; // r8
  void **Src_1; // rax
  void **Src_2; // rdx
  char *n31_3; // r9
  __int64 v65; // rbx
  char v66; // r8
  void **lpMultiByteStr_5; // rax
  void *v68; // rcx
  _QWORD *v69; // rax
  void *v70; // rcx
  LPSTR v71; // rcx
  LPSTR v72; // rcx
  void *v73; // rcx
  char *n31_5; // r9
  __int64 v75; // rbx
  unsigned int v76; // r8d
  void **lpMultiByteStr_7; // rax
  void **lpMultiByteStr_8; // rax
  void *v79; // rcx
  unsigned __int64 v80; // r9
  __int64 v81; // rbx
  char v82; // r8
  LPSTR *lpMultiByteStr_10; // rax
  void *lpMultiByteStr_11; // rdx
  char v85; // bl
  char *n31_7; // r9
  __int64 v87; // rbx
  char v88; // r8
  void **lpMultiByteStr_12; // rax
  __int64 v90; // rcx
  __int64 v91; // rax
  __int64 v92; // rcx
  __int64 v93; // rbx
  __int64 v94; // rax
  const CHAR *lpMultiByteStr_13; // rax
  LPSTR *lpMultiByteStr_15; // rbx
  __int64 v97; // rcx
  void **lpMultiByteStr_14; // rax
  __int64 v99; // rax
  __int64 v100; // rbx
  __int64 v101; // rcx
  __int64 v102; // rax
  __int64 v103; // rax
  __int64 v104; // rcx
  LPSTR *lpMultiByteStr_19; // rbx
  __int64 v106; // rcx
  void **lpMultiByteStr_18; // rax
  unsigned __int64 v108; // rbx
  __int64 v109; // rcx
  unsigned __int64 v110; // rsi
  __int64 v111; // rax
  __int64 v112; // rdi
  __int64 v113; // rax
  __int64 v114; // rax
  __int64 v115; // r15
  __int64 v116; // r13
  __int64 v117; // rax
  _BYTE *v118; // rcx
  _BYTE *v119; // rdx
  __int64 v120; // rax
  char p_n32_1; // al
  unsigned __int64 v122; // r14
  unsigned __int64 v123; // r13
  unsigned __int64 v124; // rbx
  unsigned __int64 v125; // rsi
  _BYTE *v126; // rdi
  __int64 v127; // rbx
  __int64 v128; // rax
  __int64 v129; // rax
  const CHAR *lpMultiByteStr_20; // rax
  const WCHAR *lpFile; // rbx
  __int64 v132; // rcx
  WCHAR *lpWideCharStr_6; // rax
  const WCHAR *lpOperation; // rax
  HINSTANCE p_n32_2; // rbx
  __int64 v136; // rbx
  __int64 v137; // rcx
  __int64 v138; // rax
  __int64 v139; // rax
  __int64 v140; // rbx
  __int64 v141; // rcx
  __int64 v142; // rax
  LPSTR *lpMultiByteStr_17; // rbx
  __int64 v144; // rcx
  void **lpMultiByteStr_16; // rax
  char *n31_6; // r9
  __int64 v147; // rbx
  char v148; // r8
  void **lpMultiByteStr_9; // rax
  void *v150; // rcx
  char *n31_4; // r9
  __int64 v152; // rbx
  char v153; // r8
  void **lpMultiByteStr_6; // rax
  void *v155; // rcx
  LPSTR lpMultiByteStr[2]; // [rsp+30h] [rbp-D0h] BYREF
  __m128i si128; // [rsp+40h] [rbp-C0h]
  void *lpMultiByteStr_2[2]; // [rsp+50h] [rbp-B0h] BYREF
  char *n31_2; // [rsp+60h] [rbp-A0h]
  unsigned __int64 n31; // [rsp+68h] [rbp-98h]
  void *MultiByteStr[2]; // [rsp+70h] [rbp-90h] BYREF
  void *v163; // [rsp+80h] [rbp-80h]
  unsigned __int64 n8; // [rsp+88h] [rbp-78h]
  WCHAR lpWideCharStr_1[8]; // [rsp+90h] [rbp-70h] BYREF
  __m128i v166; // [rsp+A0h] [rbp-60h]
  void *lpMultiByteStr_1[2]; // [rsp+B0h] [rbp-50h] BYREF
  int v168; // [rsp+C0h] [rbp-40h]
  int v169; // [rsp+C4h] [rbp-3Ch]
  unsigned __int64 n8_2; // [rsp+C8h] [rbp-38h]
  void *Src[2]; // [rsp+D0h] [rbp-30h] BYREF
  unsigned __int64 n15_2; // [rsp+E0h] [rbp-20h]
  unsigned __int64 n8_1; // [rsp+E8h] [rbp-18h]
  void *lpMultiByteStr_3[2]; // [rsp+F0h] [rbp-10h] BYREF
  __m128i v175; // [rsp+100h] [rbp+0h]
  LPCWCH lpWideCharStr[2]; // [rsp+110h] [rbp+10h] BYREF
  int cchWideChar[2]; // [rsp+120h] [rbp+20h]
  unsigned __int64 n8_3; // [rsp+128h] [rbp+28h]
  void *v179[2]; // [rsp+130h] [rbp+30h] BYREF
  __m128i v180; // [rsp+140h] [rbp+40h]
  void *v181[2]; // [rsp+150h] [rbp+50h] BYREF
  __m128i v182; // [rsp+160h] [rbp+60h]
  WCHAR lpWideCharStr_5[8]; // [rsp+170h] [rbp+70h] BYREF
  __int64 v184; // [rsp+180h] [rbp+80h]
  void *lpMultiByteStr_4[2]; // [rsp+190h] [rbp+90h] BYREF
  __m128i v186; // [rsp+1A0h] [rbp+A0h]
  _QWORD v187[2]; // [rsp+1B0h] [rbp+B0h] BYREF
  __m128i v188; // [rsp+1C0h] [rbp+C0h]
  void *v189[2]; // [rsp+1D0h] [rbp+D0h] BYREF
  __m128i v190; // [rsp+1E0h] [rbp+E0h]
  void *v191[2]; // [rsp+1F0h] [rbp+F0h] BYREF
  __m128i v192; // [rsp+200h] [rbp+100h]
  __int64 v193; // [rsp+210h] [rbp+110h] BYREF
  __m128i v194; // [rsp+220h] [rbp+120h]
  __int64 v195[4]; // [rsp+230h] [rbp+130h] BYREF
  _QWORD v196[2]; // [rsp+250h] [rbp+150h] BYREF
  __m128i v197; // [rsp+260h] [rbp+160h]
  WCHAR lpWideCharStr_2[16]; // [rsp+270h] [rbp+170h] BYREF
  WCHAR WideCharStr_1[16]; // [rsp+290h] [rbp+190h] BYREF
  __int64 v200[4]; // [rsp+2B0h] [rbp+1B0h] BYREF
  __int64 v201[4]; // [rsp+2D0h] [rbp+1D0h] BYREF
  WCHAR WideCharStr[16]; // [rsp+2F0h] [rbp+1F0h] BYREF
  __int64 v203; // [rsp+310h] [rbp+210h] BYREF
  __int16 n3; // [rsp+31Eh] [rbp+21Eh]
  int v205; // [rsp+36Ch] [rbp+26Ch]
  _QWORD v206[2]; // [rsp+370h] [rbp+270h] BYREF
  _BYTE v207[128]; // [rsp+380h] [rbp+280h] BYREF
  __int64 v208; // [rsp+400h] [rbp+300h]
  __int64 v209; // [rsp+4C8h] [rbp+3C8h] BYREF
  HINSTANCE p_n32; // [rsp+4D0h] [rbp+3D0h] BYREF

  LODWORD(v209) = a2;
  *(__m128i *)MultiByteStr = _mm_load_si128((const __m128i *)&xmmword_18004E2A0);
  LODWORD(v163) = -1851938150;
  BYTE4(v163) = 69;
  LODWORD(p_n32) = 8;
  lpMultiByteStr_27 = operator new(0x20u);
  n31 = 31;
  *lpMultiByteStr_27 = 0;
  lpMultiByteStr_2[0] = lpMultiByteStr_27;
  n31_1 = 0;
  n31_2 = 0;
  v6 = 0;
  n10 = 10;
  while ( 1 )
  {
    v8 = 0xE83369D8 >> (8 * (((unsigned __int8)MultiByteStr + v6 + 1LL - (_QWORD)MultiByteStr) & 3));
    LOBYTE(v8) = *((_BYTE *)MultiByteStr + v6) ^ (10 - 125 * v6) ^ (0xF74E9E6A >> (8 * (v6 & 3))) ^ v8;
    if ( (unsigned __int64)n31_1 >= n31 )
    {
      sub_180009740(lpMultiByteStr_2);
    }
    else
    {
      n31_2 = n31_1 + 1;
      lpMultiByteStr_26 = lpMultiByteStr_2;
      if ( n31 >= 0x10 )
        lpMultiByteStr_26 = (void **)lpMultiByteStr_27;
      n31_1[(_QWORD)lpMultiByteStr_26] = v8;
      n31_1[(_QWORD)lpMultiByteStr_26 + 1] = 0;
    }
    if ( (unsigned __int64)++v6 >= 0x14 )
      break;
    n31_1 = n31_2;
    lpMultiByteStr_27 = lpMultiByteStr_2[0];
  }
  *(_QWORD *)lpWideCharStr_1 = &unk_18004B9FB;
  *(_DWORD *)&lpWideCharStr_1[4] = 0;
  v166.m128i_i64[0] = (__int64)&unk_18004B9FB;
  if ( *(_QWORD *)&xmmword_180058068 )
  {
    lpMultiByteStr_28 = lpMultiByteStr_2;
    if ( n31 >= 0x10 )
      lpMultiByteStr_28 = (void **)lpMultiByteStr_2[0];
    MultiByteStr[0] = lpMultiByteStr_28;
    MultiByteStr[1] = n31_2;
    *(_OWORD *)lpMultiByteStr = *(_OWORD *)lpWideCharStr_1;
    si128.m128i_i64[0] = v166.m128i_i64[0];
    sub_18001B9F0(xmmword_180058068, (unsigned int)lpMultiByteStr, v8, (unsigned int)MultiByteStr, (__int64)&v209);
  }
  if ( n31 >= 0x10 )
  {
    v11 = lpMultiByteStr_2[0];
    if ( n31 + 1 >= 0x1000 )
    {
      v11 = (void *)*((_QWORD *)lpMultiByteStr_2[0] - 1);
      if ( (unsigned __int64)((char *)lpMultiByteStr_2[0] - (char *)v11 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v11);
  }
  v12 = sub_180008430(MultiByteStr);
  sub_1800067E0(lpWideCharStr_2, v12);
  if ( n8 >= 8 )
  {
    v13 = MultiByteStr[0];
    if ( 2 * n8 + 2 >= 0x1000 )
    {
      v13 = (void *)*((_QWORD *)MultiByteStr[0] - 1);
      if ( (unsigned __int64)((char *)MultiByteStr[0] - (char *)v13 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v13);
  }
  v163 = 0;
  n8 = 7;
  LOWORD(MultiByteStr[0]) = 0;
  wmemcpy(lpWideCharStr_1, L"孂少巑庯彯恄憙", 7);
  sub_1800167B0(lpWideCharStr_1, lpMultiByteStr_1);
  sub_1800067E0(v189, lpMultiByteStr_1);
  wmemcpy(lpWideCharStr_1, L"孴屆巻庨彲恂憙", 7);
  sub_1800167B0(lpWideCharStr_1, lpMultiByteStr_2);
  sub_1800067E0(v179, lpMultiByteStr_2);
  lpMultiByteStr[0] = (LPSTR)0x5EB85DC25C395BF7LL;
  lpMultiByteStr[1] = (LPSTR)0x62A061D660345FDCLL;
  si128.m128i_i64[0] = 0x66AA65DA643D639DLL;
  si128.m128i_i32[2] = 1750362070;
  sub_180016520(lpMultiByteStr, Src);
  sub_1800067E0(v181, Src);
  sub_180007790(lpMultiByteStr, lpWideCharStr_2);
  v14 = sub_180006450(lpMultiByteStr, v189);
  sub_180007790(lpWideCharStr_1, v14);
  if ( si128.m128i_i64[1] >= 8uLL )
  {
    v15 = lpMultiByteStr[0];
    if ( (unsigned __int64)(2 * si128.m128i_i64[1] + 2) >= 0x1000 )
    {
      v15 = (LPSTR)*((_QWORD *)lpMultiByteStr[0] - 1);
      if ( (unsigned __int64)(lpMultiByteStr[0] - v15 - 8) > 0x1F )
LABEL_30:
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v15);
  }
  si128 = _mm_load_si128((const __m128i *)&xmmword_18004DF40);
  LOWORD(lpMultiByteStr[0]) = 0;
  sub_180006AE0(v191, lpWideCharStr_1);
  if ( v166.m128i_i64[1] >= 8uLL )
  {
    v16 = *(void **)lpWideCharStr_1;
    if ( (unsigned __int64)(2 * v166.m128i_i64[1] + 2) >= 0x1000 )
    {
      v16 = *(void **)(*(_QWORD *)lpWideCharStr_1 - 8LL);
      if ( (unsigned __int64)(*(_QWORD *)lpWideCharStr_1 - (_QWORD)v16 - 8LL) > 0x1F )
        goto LABEL_30;
    }
    j_j_free(v16);
  }
  v166 = _mm_load_si128((const __m128i *)&xmmword_18004DF40);
  lpWideCharStr_1[0] = 0;
  v17 = sub_180007030(v191, lpMultiByteStr_3, v179);
  lpWideCharStr_8 = (WCHAR *)sub_180007030(v17, lpMultiByteStr_4, v181);
  if ( lpWideCharStr_2 != lpWideCharStr_8 )
  {
    if ( *((_QWORD *)lpWideCharStr_8 + 3) >= 8u )
      lpWideCharStr_8 = *(WCHAR **)lpWideCharStr_8;
    sub_180007910(lpWideCharStr_2, lpWideCharStr_8);
  }
  if ( v186.m128i_i64[1] >= 8uLL )
  {
    v19 = lpMultiByteStr_4[0];
    if ( (unsigned __int64)(2 * v186.m128i_i64[1] + 2) >= 0x1000 )
    {
      v19 = (void *)*((_QWORD *)lpMultiByteStr_4[0] - 1);
      if ( (unsigned __int64)((char *)lpMultiByteStr_4[0] - (char *)v19 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v19);
  }
  v186 = _mm_load_si128((const __m128i *)&xmmword_18004DF40);
  LOWORD(lpMultiByteStr_4[0]) = 0;
  if ( v175.m128i_i64[1] >= 8uLL )
  {
    v20 = lpMultiByteStr_3[0];
    if ( (unsigned __int64)(2 * v175.m128i_i64[1] + 2) >= 0x1000 )
    {
      v20 = (void *)*((_QWORD *)lpMultiByteStr_3[0] - 1);
      if ( (unsigned __int64)((char *)lpMultiByteStr_3[0] - (char *)v20 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v20);
  }
  v175 = _mm_load_si128((const __m128i *)&xmmword_18004DF40);
  LOWORD(lpMultiByteStr_3[0]) = 0;
  if ( v192.m128i_i64[1] >= 8uLL )
  {
    v21 = v191[0];
    if ( (unsigned __int64)(2 * v192.m128i_i64[1] + 2) >= 0x1000 )
    {
      v21 = (void *)*((_QWORD *)v191[0] - 1);
      if ( (unsigned __int64)((char *)v191[0] - (char *)v21 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v21);
  }
  v192 = _mm_load_si128((const __m128i *)&xmmword_18004DF40);
  LOWORD(v191[0]) = 0;
  if ( v182.m128i_i64[1] >= 8uLL )
  {
    v22 = v181[0];
    if ( (unsigned __int64)(2 * v182.m128i_i64[1] + 2) >= 0x1000 )
    {
      v22 = (void *)*((_QWORD *)v181[0] - 1);
      if ( (unsigned __int64)((char *)v181[0] - (char *)v22 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v22);
  }
  v182 = _mm_load_si128((const __m128i *)&xmmword_18004DF40);
  LOWORD(v181[0]) = 0;
  if ( n8_1 >= 8 )
  {
    v23 = Src[0];
    if ( 2 * n8_1 + 2 >= 0x1000 )
    {
      v23 = (void *)*((_QWORD *)Src[0] - 1);
      if ( (unsigned __int64)((char *)Src[0] - (char *)v23 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v23);
  }
  n15_2 = 0;
  n8_1 = 7;
  LOWORD(Src[0]) = 0;
  if ( v180.m128i_i64[1] >= 8uLL )
  {
    v24 = v179[0];
    if ( (unsigned __int64)(2 * v180.m128i_i64[1] + 2) >= 0x1000 )
    {
      v24 = (void *)*((_QWORD *)v179[0] - 1);
      if ( (unsigned __int64)((char *)v179[0] - (char *)v24 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v24);
  }
  v180 = _mm_load_si128((const __m128i *)&xmmword_18004DF40);
  LOWORD(v179[0]) = 0;
  if ( n31 >= 8 )
  {
    v25 = lpMultiByteStr_2[0];
    if ( 2 * n31 + 2 >= 0x1000 )
    {
      v25 = (void *)*((_QWORD *)lpMultiByteStr_2[0] - 1);
      if ( (unsigned __int64)((char *)lpMultiByteStr_2[0] - (char *)v25 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v25);
  }
  n31_2 = 0;
  n31 = 7;
  LOWORD(lpMultiByteStr_2[0]) = 0;
  if ( v190.m128i_i64[1] >= 8uLL )
  {
    v26 = v189[0];
    if ( (unsigned __int64)(2 * v190.m128i_i64[1] + 2) >= 0x1000 )
    {
      v26 = (void *)*((_QWORD *)v189[0] - 1);
      if ( (unsigned __int64)((char *)v189[0] - (char *)v26 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v26);
  }
  v190 = _mm_load_si128((const __m128i *)&xmmword_18004DF40);
  LOWORD(v189[0]) = 0;
  if ( n8_2 >= 8 )
  {
    v27 = lpMultiByteStr_1[0];
    if ( 2 * n8_2 + 2 >= 0x1000 )
    {
      v27 = (void *)*((_QWORD *)lpMultiByteStr_1[0] - 1);
      if ( (unsigned __int64)((char *)lpMultiByteStr_1[0] - (char *)v27 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v27);
  }
  sub_180007130(lpWideCharStr_2);
  lpWideCharStr_3 = (const WCHAR *)lpWideCharStr;
  if ( n8_3 >= 8 )
    lpWideCharStr_3 = lpWideCharStr[0];
  stats = _std_fs_get_stats(lpWideCharStr_3);
  if ( stats )
  {
    v30 = stats - 2;
    if ( v30 )
    {
      v31 = v30 - 1;
      if ( v31 )
      {
        n70 = v31 - 50;
        if ( n70 )
        {
          if ( n70 != 70 )
            goto LABEL_95;
        }
      }
    }
    n10 = 1;
  }
  else
  {
    if ( (v168 & 0x400) == 0 )
      goto LABEL_88;
    if ( v169 == -1610612724 )
    {
      n10 = 4;
      goto LABEL_94;
    }
    if ( v169 != -1610612733 )
LABEL_88:
      n10 = (v168 & 0x10 | 0x20u) >> 4;
  }
LABEL_94:
  if ( n10 != 1 )
  {
    lpWideCharStr_4 = lpWideCharStr;
    if ( n8_3 >= 8 )
      lpWideCharStr_4 = (LPCWCH *)lpWideCharStr[0];
    sub_180018800(v206, lpWideCharStr_4);
    *(_QWORD *)((char *)v206 + *(int *)(v206[0] + 4LL)) = &off_18004CDE8;
    *(int *)((char *)&v205 + *(int *)(v206[0] + 4LL)) = *(_DWORD *)(v206[0] + 4LL) - 176;
    if ( !v208 )
    {
      LODWORD(p_n32) = GetLastError();
      v54 = sub_180006620((LPCWCH)lpWideCharStr);
      v56 = sub_180012C00(v55, lpMultiByteStr_1);
      *(_QWORD *)lpWideCharStr_1 = &unk_18004B9FB;
      *(_DWORD *)&lpWideCharStr_1[4] = 0;
      v166.m128i_i64[0] = (__int64)&unk_18004B9FB;
      if ( *(_QWORD *)&xmmword_180058068 )
      {
        v57 = (void *)v56;
        if ( *(_QWORD *)(v56 + 24) >= 0x10u )
          v57 = *(void **)v56;
        MultiByteStr[0] = v57;
        MultiByteStr[1] = *(void **)(v56 + 16);
        *(_OWORD *)lpMultiByteStr = *(_OWORD *)lpWideCharStr_1;
        si128.m128i_i64[0] = v166.m128i_i64[0];
        sub_18001BE10(
          xmmword_180058068,
          (unsigned int)lpMultiByteStr,
          xmmword_180058068,
          (unsigned int)MultiByteStr,
          v54,
          &p_n32);
      }
      sub_180002BA0(lpMultiByteStr_1);
      sub_180002BA0(lpMultiByteStr_3);
      v52 = 0;
      goto LABEL_300;
    }
    *(_QWORD *)lpWideCharStr_1 = 0;
    lpWideCharStr_1[4] = 1;
    MultiByteStr[0] = *(void **)&v207[*(int *)(v206[0] + 4LL) + 56];
    LOBYTE(MultiByteStr[1]) = MultiByteStr[0] == 0;
    BYTE1(MultiByteStr[1]) = 0;
    v196[0] = 0;
    v197 = _mm_load_si128((const __m128i *)&xmmword_18004DF50);
    sub_18001A160(v196);
    if ( !sub_180017400(v207) )
      std::ios::setstate((char *)v206 + *(int *)(v206[0] + 4LL), 2);
    v187[0] = 0;
    v188 = _mm_load_si128((const __m128i *)&xmmword_18004DF50);
    wmemcpy(lpWideCharStr_1, L"ᖀ疍糲握", 4);
    LOBYTE(lpWideCharStr_1[4]) = -86;
    Src[0] = 0;
    n15_1 = 0;
    n15_2 = 0;
    n15 = 15;
    n8_1 = 15;
    LODWORD(p_n32) = 16884;
    v60 = 0;
    while ( 1 )
    {
      v61 = *((_BYTE *)lpWideCharStr_1 + v60)
          ^ (62 - 125 * v60)
          ^ (0xC66AD9Cu >> (8 * (v60 & 3)))
          ^ (0x975F608A >> (8 * (((unsigned __int8)lpWideCharStr_1 + v60 + 1LL - (_QWORD)lpWideCharStr_1) & 3)));
      if ( n15_1 >= n15 )
      {
        sub_180009740(Src);
      }
      else
      {
        n15_2 = n15_1 + 1;
        Src_1 = Src;
        if ( n15 >= 0x10 )
          Src_1 = (void **)Src[0];
        *((_BYTE *)Src_1 + n15_1) = v61;
        *((_BYTE *)Src_1 + n15_1 + 1) = 0;
      }
      if ( (unsigned __int64)++v60 >= 8 )
        break;
      n15 = n8_1;
      n15_1 = n15_2;
    }
    Src_2 = Src;
    if ( n8_1 >= 0x10 )
      Src_2 = (void **)Src[0];
    if ( !(unsigned __int8)sub_180008FE0(v196, Src_2, v187) )
    {
      *(__m128i *)MultiByteStr = _mm_load_si128((const __m128i *)&xmmword_18004E150);
      LOWORD(v163) = -584;
      BYTE2(v163) = 88;
      lpMultiByteStr_2[0] = 0;
      n31_2 = 0;
      n31 = 15;
      LODWORD(p_n32) = 74228;
      sub_1800095F0(lpMultiByteStr_2);
      n31_3 = 0;
      n31_2 = 0;
      v65 = 0;
      while ( 1 )
      {
        v66 = *((_BYTE *)MultiByteStr + v65)
            ^ (-24 - 125 * v65)
            ^ (0x8A1B04AF >> (8 * (v65 & 3)))
            ^ (0x5D066AEBu >> (8 * ((1LL - (_QWORD)MultiByteStr + (unsigned __int8)MultiByteStr + v65) & 3)));
        if ( (unsigned __int64)n31_3 >= n31 )
        {
          sub_180009740(lpMultiByteStr_2);
        }
        else
        {
          n31_2 = n31_3 + 1;
          lpMultiByteStr_5 = lpMultiByteStr_2;
          if ( n31 >= 0x10 )
            lpMultiByteStr_5 = (void **)lpMultiByteStr_2[0];
          n31_3[(_QWORD)lpMultiByteStr_5] = v66;
          n31_3[(_QWORD)lpMultiByteStr_5 + 1] = 0;
        }
        if ( (unsigned __int64)++v65 >= 0x12 )
          break;
        n31_3 = n31_2;
      }
      MultiByteStr[0] = &unk_18004B9FB;
      LODWORD(MultiByteStr[1]) = 0;
      v163 = &unk_18004B9FB;
      sub_1800181C0(MultiByteStr, lpMultiByteStr_2);
      if ( n31 >= 0x10 )
      {
        v68 = lpMultiByteStr_2[0];
        if ( n31 + 1 >= 0x1000 )
        {
          v68 = (void *)*((_QWORD *)lpMultiByteStr_2[0] - 1);
          if ( (unsigned __int64)((char *)lpMultiByteStr_2[0] - (char *)v68 - 8) > 0x1F )
            invalid_parameter_noinfo_noreturn();
        }
        j_j_free(v68);
      }
      v52 = 0;
      goto LABEL_299;
    }
    sub_180002890(&v203);
    v69 = v187;
    if ( v188.m128i_i64[1] >= 0x10uLL )
      v69 = (_QWORD *)v187[0];
    MultiByteStr[0] = v69;
    MultiByteStr[1] = v69;
    if ( *(_DWORD *)(sub_1800043F0(&v203, MultiByteStr) + 80) || n3 != 3 )
    {
      *(__m128i *)MultiByteStr = _mm_load_si128((const __m128i *)&xmmword_18004E1C0);
      LOBYTE(v163) = -53;
      lpMultiByteStr_2[0] = 0;
      n31_2 = 0;
      n31 = 15;
      LODWORD(p_n32) = 270836;
      sub_1800095F0(lpMultiByteStr_2);
      n31_4 = 0;
      n31_2 = 0;
      v152 = 0;
      while ( 1 )
      {
        v153 = *((_BYTE *)MultiByteStr + v152)
             ^ (-58 - 125 * v152)
             ^ (0xBE6581AB >> (8 * (v152 & 3)))
             ^ (0xBCD8960F >> (8 * ((1LL - (_QWORD)MultiByteStr + (unsigned __int8)MultiByteStr + v152) & 3)));
        if ( (unsigned __int64)n31_4 >= n31 )
        {
          sub_180009740(lpMultiByteStr_2);
        }
        else
        {
          n31_2 = n31_4 + 1;
          lpMultiByteStr_6 = lpMultiByteStr_2;
          if ( n31 >= 0x10 )
            lpMultiByteStr_6 = (void **)lpMultiByteStr_2[0];
          n31_4[(_QWORD)lpMultiByteStr_6] = v153;
          n31_4[(_QWORD)lpMultiByteStr_6 + 1] = 0;
        }
        if ( (unsigned __int64)++v152 >= 0x10 )
          break;
        n31_4 = n31_2;
      }
      lpMultiByteStr[0] = (LPSTR)&unk_18004B9FB;
      LODWORD(lpMultiByteStr[1]) = 0;
      si128.m128i_i64[0] = (__int64)&unk_18004B9FB;
      sub_1800181C0(lpMultiByteStr, lpMultiByteStr_2);
      if ( n31 >= 0x10 )
      {
        v155 = lpMultiByteStr_2[0];
        if ( n31 + 1 >= 0x1000 )
        {
          v155 = (void *)*((_QWORD *)lpMultiByteStr_2[0] - 1);
          if ( (unsigned __int64)((char *)lpMultiByteStr_2[0] - (char *)v155 - 8) > 0x1F )
            invalid_parameter_noinfo_noreturn();
        }
        j_j_free(v155);
      }
      v52 = 0;
      goto LABEL_298;
    }
    lpMultiByteStr[0] = 0;
    si128.m128i_i64[0] = 0;
    si128.m128i_i64[1] = 15;
    sub_180002E70(lpMultiByteStr, &unk_18004B9FB, 0);
    lpMultiByteStr_2[0] = 0;
    n31_2 = 0;
    n31 = 15;
    sub_180002E70(lpMultiByteStr_2, "appCode", 7u);
    sub_180002660(v195);
    if ( n31 >= 0x10 )
    {
      v70 = lpMultiByteStr_2[0];
      if ( n31 + 1 >= 0x1000 )
      {
        v70 = (void *)*((_QWORD *)lpMultiByteStr_2[0] - 1);
        if ( (unsigned __int64)((char *)lpMultiByteStr_2[0] - (char *)v70 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v70);
    }
    n31_2 = 0;
    n31 = 15;
    LOBYTE(lpMultiByteStr_2[0]) = 0;
    if ( si128.m128i_i64[1] >= 0x10uLL )
    {
      v71 = lpMultiByteStr[0];
      if ( (unsigned __int64)(si128.m128i_i64[1] + 1) >= 0x1000 )
      {
        v71 = (LPSTR)*((_QWORD *)lpMultiByteStr[0] - 1);
        if ( (unsigned __int64)(lpMultiByteStr[0] - v71 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v71);
    }
    MultiByteStr[0] = 0;
    v163 = 0;
    n8 = 15;
    sub_180002E70(MultiByteStr, &unk_18004B9FB, 0);
    lpMultiByteStr[0] = 0;
    si128.m128i_i64[0] = 0;
    si128.m128i_i64[1] = 15;
    sub_180002E70(lpMultiByteStr, "gameName", 8u);
    sub_180002660(v201);
    if ( si128.m128i_i64[1] >= 0x10uLL )
    {
      v72 = lpMultiByteStr[0];
      if ( (unsigned __int64)(si128.m128i_i64[1] + 1) >= 0x1000 )
      {
        v72 = (LPSTR)*((_QWORD *)lpMultiByteStr[0] - 1);
        if ( (unsigned __int64)(lpMultiByteStr[0] - v72 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v72);
    }
    si128.m128i_i64[0] = 0;
    si128.m128i_i64[1] = 15;
    LOBYTE(lpMultiByteStr[0]) = 0;
    if ( n8 >= 0x10 )
    {
      v73 = MultiByteStr[0];
      if ( n8 + 1 >= 0x1000 )
      {
        v73 = (void *)*((_QWORD *)MultiByteStr[0] - 1);
        if ( (unsigned __int64)((char *)MultiByteStr[0] - (char *)v73 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v73);
    }
    *(__m128i *)lpMultiByteStr = _mm_load_si128((const __m128i *)&xmmword_18004E2D0);
    si128.m128i_i64[0] = 0x457E8AE84170DB59LL;
    si128.m128i_i8[8] = -111;
    lpMultiByteStr_2[0] = 0;
    n31_2 = 0;
    n31 = 15;
    LODWORD(p_n32) = 1057268;
    sub_1800095F0(lpMultiByteStr_2);
    n31_5 = 0;
    n31_2 = 0;
    v75 = 0;
    while ( 1 )
    {
      v76 = 0x6A610655u >> (8 * ((1LL - (_QWORD)lpMultiByteStr + (unsigned __int8)lpMultiByteStr + v75) & 3));
      LOBYTE(v76) = *((_BYTE *)lpMultiByteStr + v75) ^ (78 - 125 * v75) ^ (0x7EFFDC01u >> (8 * (v75 & 3))) ^ v76;
      if ( (unsigned __int64)n31_5 >= n31 )
      {
        sub_180009740(lpMultiByteStr_2);
      }
      else
      {
        n31_2 = n31_5 + 1;
        lpMultiByteStr_7 = lpMultiByteStr_2;
        if ( n31 >= 0x10 )
          lpMultiByteStr_7 = (void **)lpMultiByteStr_2[0];
        n31_5[(_QWORD)lpMultiByteStr_7] = v76;
        n31_5[(_QWORD)lpMultiByteStr_7 + 1] = 0;
      }
      if ( (unsigned __int64)++v75 >= 0x18 )
        break;
      n31_5 = n31_2;
    }
    *(_QWORD *)lpWideCharStr_1 = &unk_18004B9FB;
    *(_DWORD *)&lpWideCharStr_1[4] = 0;
    v166.m128i_i64[0] = (__int64)&unk_18004B9FB;
    if ( *(_QWORD *)&xmmword_180058068 )
    {
      lpMultiByteStr_8 = lpMultiByteStr_2;
      if ( n31 >= 0x10 )
        lpMultiByteStr_8 = (void **)lpMultiByteStr_2[0];
      MultiByteStr[0] = lpMultiByteStr_8;
      MultiByteStr[1] = n31_2;
      *(_OWORD *)lpMultiByteStr = *(_OWORD *)lpWideCharStr_1;
      si128.m128i_i64[0] = v166.m128i_i64[0];
      sub_18001C040(
        xmmword_180058068,
        (unsigned int)lpMultiByteStr,
        v76,
        (unsigned int)MultiByteStr,
        (__int64)v195,
        (__int64)v201);
    }
    if ( n31 >= 0x10 )
    {
      v79 = lpMultiByteStr_2[0];
      if ( n31 + 1 >= 0x1000 )
      {
        v79 = (void *)*((_QWORD *)lpMultiByteStr_2[0] - 1);
        if ( (unsigned __int64)((char *)lpMultiByteStr_2[0] - (char *)v79 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v79);
    }
    if ( !v195[2] || !v201[2] )
    {
      *(__m128i *)lpMultiByteStr = _mm_load_si128((const __m128i *)&xmmword_18004E040);
      si128.m128i_i64[0] = 0x2EAF0C9F468E008FLL;
      si128.m128i_i16[4] = 21951;
      lpMultiByteStr_2[0] = 0;
      n31_2 = 0;
      n31 = 15;
      LODWORD(p_n32) = 4727284;
      sub_1800095F0(lpMultiByteStr_2);
      n31_6 = 0;
      n31_2 = 0;
      v147 = 0;
      while ( 1 )
      {
        v148 = *((_BYTE *)lpMultiByteStr + v147)
             ^ (95 - 125 * v147)
             ^ (0x7676EC16u >> (8 * (v147 & 3)))
             ^ (0x8937708u >> (8 * ((1LL - (_QWORD)lpMultiByteStr + (unsigned __int8)lpMultiByteStr + v147) & 3)));
        if ( (unsigned __int64)n31_6 >= n31 )
        {
          sub_180009740(lpMultiByteStr_2);
        }
        else
        {
          n31_2 = n31_6 + 1;
          lpMultiByteStr_9 = lpMultiByteStr_2;
          if ( n31 >= 0x10 )
            lpMultiByteStr_9 = (void **)lpMultiByteStr_2[0];
          n31_6[(_QWORD)lpMultiByteStr_9] = v148;
          n31_6[(_QWORD)lpMultiByteStr_9 + 1] = 0;
        }
        if ( (unsigned __int64)++v147 >= 0x19 )
          break;
        n31_6 = n31_2;
      }
      lpMultiByteStr[0] = (LPSTR)&unk_18004B9FB;
      LODWORD(lpMultiByteStr[1]) = 0;
      si128.m128i_i64[0] = (__int64)&unk_18004B9FB;
      sub_1800181C0(lpMultiByteStr, lpMultiByteStr_2);
      if ( n31 >= 0x10 )
      {
        v150 = lpMultiByteStr_2[0];
        if ( n31 + 1 >= 0x1000 )
        {
          v150 = (void *)*((_QWORD *)lpMultiByteStr_2[0] - 1);
          if ( (unsigned __int64)((char *)lpMultiByteStr_2[0] - (char *)v150 - 8) > 0x1F )
            invalid_parameter_noinfo_noreturn();
        }
        j_j_free(v150);
      }
      v52 = 0;
      goto LABEL_283;
    }
    if ( a2 )
    {
      *(__m128i *)MultiByteStr = _mm_load_si128((const __m128i *)&xmmword_18004E250);
      LODWORD(v163) = -1409330727;
      lpMultiByteStr[0] = 0;
      si128.m128i_i64[0] = 0;
      si128.m128i_i64[1] = 15;
      LODWORD(p_n32) = 17310196;
      sub_1800095F0(lpMultiByteStr);
      v80 = 0;
      si128.m128i_i64[0] = 0;
      v81 = 0;
      while ( 1 )
      {
        v82 = *((_BYTE *)MultiByteStr + v81)
            ^ (-7 - 125 * v81)
            ^ (0x8A514E1B >> (8 * (v81 & 3)))
            ^ (0xACC18E93 >> (8 * ((1LL - (_QWORD)MultiByteStr + (unsigned __int8)MultiByteStr + v81) & 3)));
        if ( v80 >= si128.m128i_i64[1] )
        {
          sub_180009740(lpMultiByteStr);
        }
        else
        {
          si128.m128i_i64[0] = v80 + 1;
          lpMultiByteStr_10 = lpMultiByteStr;
          if ( si128.m128i_i64[1] >= 0x10uLL )
            lpMultiByteStr_10 = (LPSTR *)lpMultiByteStr[0];
          *((_BYTE *)lpMultiByteStr_10 + v80) = v82;
          *((_BYTE *)lpMultiByteStr_10 + v80 + 1) = 0;
        }
        if ( (unsigned __int64)++v81 >= 0x13 )
          break;
        v80 = si128.m128i_i64[0];
      }
      lpMultiByteStr_11 = lpMultiByteStr;
      v85 = -11;
    }
    else
    {
      *(__m128i *)MultiByteStr = _mm_load_si128((const __m128i *)&xmmword_18004E130);
      LOWORD(v163) = 21576;
      BYTE2(v163) = -29;
      lpMultiByteStr_2[0] = 0;
      n31_2 = 0;
      n31 = 15;
      LODWORD(p_n32) = 67641844;
      sub_1800095F0(lpMultiByteStr_2);
      n31_7 = 0;
      n31_2 = 0;
      v87 = 0;
      while ( 1 )
      {
        v88 = *((_BYTE *)MultiByteStr + v87)
            ^ (-24 - 125 * v87)
            ^ (0x5E7B971u >> (8 * (v87 & 3)))
            ^ (0x1A5B5375u >> (8 * ((1LL - (_QWORD)MultiByteStr + (unsigned __int8)MultiByteStr + v87) & 3)));
        if ( (unsigned __int64)n31_7 >= n31 )
        {
          sub_180009740(lpMultiByteStr_2);
        }
        else
        {
          n31_2 = n31_7 + 1;
          lpMultiByteStr_12 = lpMultiByteStr_2;
          if ( n31 >= 0x10 )
            lpMultiByteStr_12 = (void **)lpMultiByteStr_2[0];
          n31_7[(_QWORD)lpMultiByteStr_12] = v88;
          n31_7[(_QWORD)lpMultiByteStr_12 + 1] = 0;
        }
        if ( (unsigned __int64)++v87 >= 0x12 )
          break;
        n31_7 = n31_2;
      }
      lpMultiByteStr_11 = lpMultiByteStr_2;
      v85 = -10;
    }
    sub_1800078E0(v200, lpMultiByteStr_11);
    if ( (v85 & 2) != 0 )
    {
      v85 &= ~2u;
      sub_180002BA0(lpMultiByteStr_2);
    }
    if ( (v85 & 1) != 0 )
      sub_180002BA0(lpMultiByteStr);
    v91 = sub_180012D30(v90, lpMultiByteStr_1);
    MultiByteStr[0] = &unk_18004B9FB;
    LODWORD(MultiByteStr[1]) = 0;
    v163 = &unk_18004B9FB;
    sub_1800182A0(MultiByteStr, v91, &v209, v200);
    sub_180002BA0(lpMultiByteStr_1);
    v93 = sub_180012E60(v92, lpMultiByteStr_4);
    v94 = sub_180018310(lpMultiByteStr_3);
    lpMultiByteStr_13 = (const CHAR *)sub_180009560(lpMultiByteStr_1, v94, v93);
    sub_1800087D0(WideCharStr, lpMultiByteStr_13);
    sub_180002BA0(lpMultiByteStr_1);
    sub_180002BA0(lpMultiByteStr_3);
    sub_180002BA0(lpMultiByteStr_4);
    v193 = 0;
    v194 = _mm_load_si128((const __m128i *)&xmmword_18004DF40);
    if ( !(unsigned __int8)sub_18000D740(WideCharStr) )
    {
      lpMultiByteStr_15 = (LPSTR *)sub_180008600(lpMultiByteStr_3, WideCharStr);
      lpMultiByteStr_14 = (void **)sub_180012F80(v97, lpMultiByteStr_1);
      MultiByteStr[0] = &unk_18004B9FB;
      LODWORD(MultiByteStr[1]) = 0;
      v163 = &unk_18004B9FB;
      sub_180018230((LPSTR *)MultiByteStr, lpMultiByteStr_14, lpMultiByteStr_15);
      sub_180002BA0(lpMultiByteStr_1);
      sub_180002BA0(lpMultiByteStr_3);
      v52 = 0;
LABEL_268:
      unknown_libname_20(&v193);
      unknown_libname_20(WideCharStr);
      sub_180002BA0(v200);
LABEL_283:
      sub_180002BA0(v201);
      sub_180002BA0(v195);
LABEL_298:
      sub_1800027F0(&v203);
LABEL_299:
      sub_180002BA0(Src);
      sub_180002BA0(v187);
      sub_180002BA0(v196);
LABEL_300:
      sub_180013A30(v206);
      goto LABEL_301;
    }
    sub_18000D4C0(lpWideCharStr_5);
    if ( !v184 || (v99 = sub_180007400(lpWideCharStr_5), !(unsigned __int8)sub_180008FB0(v99)) )
    {
      lpMultiByteStr_17 = (LPSTR *)sub_180008600(lpMultiByteStr_3, lpWideCharStr_5);
      lpMultiByteStr_16 = (void **)sub_1800130B0(v144, lpMultiByteStr_1);
      lpMultiByteStr[0] = (LPSTR)&unk_18004B9FB;
      LODWORD(lpMultiByteStr[1]) = 0;
      si128.m128i_i64[0] = (__int64)&unk_18004B9FB;
      sub_180018230(lpMultiByteStr, lpMultiByteStr_16, lpMultiByteStr_17);
      sub_180002BA0(lpMultiByteStr_1);
      goto LABEL_266;
    }
    v100 = sub_180008600(lpMultiByteStr_3, lpWideCharStr_5);
    v102 = sub_1800131D0(v101, lpMultiByteStr_1);
    MultiByteStr[0] = &unk_18004B9FB;
    LODWORD(MultiByteStr[1]) = 0;
    v163 = &unk_18004B9FB;
    sub_1800183D0((LPSTR *)MultiByteStr, v102, v100);
    sub_180002BA0(lpMultiByteStr_1);
    sub_180002BA0(lpMultiByteStr_3);
    v103 = sub_180007400(lpWideCharStr_5);
    if ( !sub_180013AA0(v104, v103) )
    {
      lpMultiByteStr_19 = (LPSTR *)sub_180008600(lpMultiByteStr_3, lpWideCharStr_5);
      lpMultiByteStr_18 = (void **)sub_1800132C0(v106, lpMultiByteStr_1);
      MultiByteStr[0] = &unk_18004B9FB;
      LODWORD(MultiByteStr[1]) = 0;
      v163 = &unk_18004B9FB;
      sub_180018230((LPSTR *)MultiByteStr, lpMultiByteStr_18, lpMultiByteStr_19);
      sub_180002BA0(lpMultiByteStr_1);
LABEL_266:
      sub_180002BA0(lpMultiByteStr_3);
      v52 = 0;
      goto LABEL_267;
    }
    *(_OWORD *)lpWideCharStr_1 = 0;
    v166.m128i_i64[0] = 0;
    v108 = 0;
    if ( sub_180016CC0(a1) )
    {
      do
      {
        v110 = 0;
        v111 = sub_180016CB0(a1, v108);
        if ( (__int64)(*(_QWORD *)(v111 + 64) - *(_QWORD *)(v111 + 56)) >> 5 )
        {
          v112 = 0;
          do
          {
            v113 = sub_180016CB0(a1, v108);
            sub_1800133F0(lpWideCharStr_1, v112 + *(_QWORD *)(v113 + 56));
            ++v110;
            v112 += 32;
            v114 = sub_180016CB0(a1, v108);
          }
          while ( v110 < (__int64)(*(_QWORD *)(v114 + 64) - *(_QWORD *)(v114 + 56)) >> 5 );
        }
        ++v108;
      }
      while ( v108 < sub_180016CC0(a1) );
      v115 = *(_QWORD *)lpWideCharStr_1;
      v116 = *(_QWORD *)&lpWideCharStr_1[4];
      if ( *(_QWORD *)lpWideCharStr_1 != *(_QWORD *)&lpWideCharStr_1[4] )
      {
        v117 = sub_180007400(lpWideCharStr_5);
        sub_1800140C0(a1, MultiByteStr, v117);
        v118 = MultiByteStr[0];
        v119 = MultiByteStr[1];
        if ( MultiByteStr[0] == MultiByteStr[1] )
        {
          v120 = sub_180013430(MultiByteStr[0], lpMultiByteStr_1);
          lpMultiByteStr[0] = (LPSTR)&unk_18004B9FB;
          LODWORD(lpMultiByteStr[1]) = 0;
          si128.m128i_i64[0] = (__int64)&unk_18004B9FB;
          sub_1800181C0(lpMultiByteStr, v120);
LABEL_261:
          sub_180002BA0(lpMultiByteStr_1);
          sub_1800174B0(MultiByteStr);
          v52 = 0;
LABEL_264:
          sub_1800174B0(lpWideCharStr_1);
LABEL_267:
          unknown_libname_20(lpWideCharStr_5);
          goto LABEL_268;
        }
        p_n32_1 = 0;
        LOBYTE(p_n32) = 0;
        v122 = 0;
        v123 = (v116 - v115) >> 5;
        if ( !v123 )
        {
LABEL_260:
          v139 = sub_180013560(v118, lpMultiByteStr_1);
          lpMultiByteStr[0] = (LPSTR)&unk_18004B9FB;
          LODWORD(lpMultiByteStr[1]) = 0;
          si128.m128i_i64[0] = (__int64)&unk_18004B9FB;
          sub_1800181C0(lpMultiByteStr, v139);
          goto LABEL_261;
        }
        while ( !p_n32_1 )
        {
          v124 = 0;
          v125 = (v119 - v118) >> 5;
          if ( v125 )
          {
            v126 = v118;
            while ( !sub_1800089A0() )
            {
              ++v124;
              v126 += 32;
              if ( v124 >= v125 )
              {
                p_n32_1 = (char)p_n32;
                goto LABEL_254;
              }
            }
            p_n32_1 = 1;
            LOBYTE(p_n32) = 1;
LABEL_254:
            v119 = MultiByteStr[1];
            v118 = MultiByteStr[0];
          }
          ++v122;
          v115 += 32;
          if ( v122 >= v123 )
          {
            if ( !p_n32_1 )
              goto LABEL_260;
            break;
          }
        }
        sub_1800174B0(MultiByteStr);
      }
    }
    v127 = sub_180013690(v109, v189);
    v128 = sub_180018310(lpMultiByteStr_4);
    v129 = sub_180009560(lpMultiByteStr_3, v128, v127);
    lpMultiByteStr_20 = (const CHAR *)sub_180018370(lpMultiByteStr_1, v129, v201);
    sub_1800087D0(WideCharStr_1, lpMultiByteStr_20);
    sub_180002BA0(lpMultiByteStr_1);
    sub_180002BA0(lpMultiByteStr_3);
    sub_180002BA0(lpMultiByteStr_4);
    sub_180002BA0(v189);
    lpFile = (const WCHAR *)sub_180007400(WideCharStr_1);
    lpWideCharStr_6 = (WCHAR *)sub_180013790(v132, v179);
    lpOperation = (const WCHAR *)sub_180007400(lpWideCharStr_6);
    p_n32_2 = ShellExecuteW(0, lpOperation, lpFile, 0, 0, 1);
    unknown_libname_20(v179);
    if ( (__int64)p_n32_2 > 32 )
    {
      v140 = sub_180008600(v181, WideCharStr_1);
      v142 = sub_180013900(v141, v179);
      lpMultiByteStr[0] = (LPSTR)&unk_18004B9FB;
      LODWORD(lpMultiByteStr[1]) = 0;
      si128.m128i_i64[0] = (__int64)&unk_18004B9FB;
      sub_1800183D0(lpMultiByteStr, v142, v140);
      sub_180002BA0(v179);
      sub_180002BA0(v181);
      v52 = 1;
    }
    else
    {
      p_n32 = p_n32_2;
      v136 = sub_180008600(v181, WideCharStr_1);
      v138 = sub_1800137D0(v137, v179);
      lpMultiByteStr[0] = (LPSTR)&unk_18004B9FB;
      LODWORD(lpMultiByteStr[1]) = 0;
      si128.m128i_i64[0] = (__int64)&unk_18004B9FB;
      sub_180018550(lpMultiByteStr, v138, v136, &p_n32);
      sub_180002BA0(v179);
      sub_180002BA0(v181);
      v52 = 0;
    }
    unknown_libname_20(WideCharStr_1);
    goto LABEL_264;
  }
LABEL_95:
  lpWideCharStr_7 = (const WCHAR *)lpWideCharStr;
  if ( n8_3 >= 8 )
    lpWideCharStr_7 = lpWideCharStr[0];
  cchWideChar_1 = cchWideChar[0];
  lpMultiByteStr[0] = 0;
  si128.m128i_i64[0] = 0;
  si128.m128i_i64[1] = 15;
  LODWORD(p_n32) = 1524;
  if ( *(_QWORD *)cchWideChar )
  {
    if ( *(_QWORD *)cchWideChar > 0x7FFFFFFFu )
      sub_180005F70();
    v35 = _std_fs_convert_wide_to_narrow(0xFDE9u, lpWideCharStr_7, cchWideChar[0], 0, 0);
    v36 = v35;
    v37 = HIDWORD(v35);
    if ( (_DWORD)v37 )
      sub_180006320((unsigned int)v37);
    v38 = si128.m128i_i64[0];
    if ( (unsigned __int64)v36 > si128.m128i_i64[0] )
    {
      v40 = v36 - si128.m128i_i64[0];
      if ( v40 > si128.m128i_i64[1] - si128.m128i_i64[0] )
      {
        sub_180007DD0(lpMultiByteStr);
      }
      else
      {
        si128.m128i_i64[0] = v36;
        lpMultiByteStr_21 = lpMultiByteStr;
        if ( si128.m128i_i64[1] >= 0x10uLL )
          lpMultiByteStr_21 = (LPSTR *)lpMultiByteStr[0];
        v42 = (char *)lpMultiByteStr_21 + v38;
        memset(v42, 0, v36 - v38);
        v42[v40] = 0;
      }
    }
    else
    {
      lpMultiByteStr_22 = lpMultiByteStr;
      if ( si128.m128i_i64[1] >= 0x10uLL )
        lpMultiByteStr_22 = (LPSTR *)lpMultiByteStr[0];
      si128.m128i_i64[0] = v36;
      *((_BYTE *)lpMultiByteStr_22 + v36) = 0;
    }
    lpMultiByteStr_23 = (CHAR *)lpMultiByteStr;
    if ( si128.m128i_i64[1] >= 0x10uLL )
      lpMultiByteStr_23 = lpMultiByteStr[0];
    v44 = (unsigned __int64)_std_fs_convert_wide_to_narrow(
                              0xFDE9u,
                              lpWideCharStr_7,
                              cchWideChar_1,
                              lpMultiByteStr_23,
                              v36) >> 32;
    if ( (_DWORD)v44 )
      sub_180006320((unsigned int)v44);
  }
  *(__m128i *)MultiByteStr = _mm_load_si128((const __m128i *)&xmmword_18004DFC0);
  v163 = (void *)0x9FE9378F18EC33C8LL;
  lpMultiByteStr_2[0] = 0;
  n31_2 = 0;
  n31 = 15;
  LODWORD(p_n32) = 5108;
  lpMultiByteStr_25 = operator new(0x20u);
  n31 = 31;
  *lpMultiByteStr_25 = lpMultiByteStr_2[0];
  lpMultiByteStr_2[0] = lpMultiByteStr_25;
  n31_8 = 0;
  n31_2 = 0;
  v47 = 0;
  while ( 1 )
  {
    v48 = *((_BYTE *)MultiByteStr + v47)
        ^ (61 - 125 * v47)
        ^ (0x94C7AF6D >> (8 * (v47 & 3)))
        ^ (0x2C1FA109u >> (8 * (((unsigned __int8)MultiByteStr + v47 + 1LL - (_QWORD)MultiByteStr) & 3)));
    if ( (unsigned __int64)n31_8 >= n31 )
    {
      sub_180009740(lpMultiByteStr_2);
    }
    else
    {
      n31_2 = n31_8 + 1;
      lpMultiByteStr_24 = lpMultiByteStr_2;
      if ( n31 >= 0x10 )
        lpMultiByteStr_24 = (void **)lpMultiByteStr_25;
      n31_8[(_QWORD)lpMultiByteStr_24] = v48;
      n31_8[(_QWORD)lpMultiByteStr_24 + 1] = 0;
    }
    if ( (unsigned __int64)++v47 >= 0x17 )
      break;
    n31_8 = n31_2;
    lpMultiByteStr_25 = lpMultiByteStr_2[0];
  }
  MultiByteStr[0] = &unk_18004B9FB;
  LODWORD(MultiByteStr[1]) = 0;
  v163 = &unk_18004B9FB;
  sub_180018230((LPSTR *)MultiByteStr, lpMultiByteStr_2, lpMultiByteStr);
  if ( n31 >= 0x10 )
  {
    v50 = lpMultiByteStr_2[0];
    if ( n31 + 1 >= 0x1000 )
    {
      v50 = (void *)*((_QWORD *)lpMultiByteStr_2[0] - 1);
      if ( (unsigned __int64)((char *)lpMultiByteStr_2[0] - (char *)v50 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v50);
  }
  n31_2 = 0;
  n31 = 15;
  LOBYTE(lpMultiByteStr_2[0]) = 0;
  if ( si128.m128i_i64[1] >= 0x10uLL )
  {
    v51 = lpMultiByteStr[0];
    if ( (unsigned __int64)(si128.m128i_i64[1] + 1) >= 0x1000 )
    {
      v51 = (LPSTR)*((_QWORD *)lpMultiByteStr[0] - 1);
      if ( (unsigned __int64)(lpMultiByteStr[0] - v51 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v51);
  }
  v52 = 0;
LABEL_301:
  unknown_libname_20(lpWideCharStr);
  unknown_libname_20(lpWideCharStr_2);
  return v52;
}
