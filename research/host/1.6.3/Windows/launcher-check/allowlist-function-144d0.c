// Hidden C++ exception states: #wind=7
void __fastcall sub_1800144D0(__int64 a1)
{
  unsigned __int64 v2; // rdi
  int pid; // eax
  DWORD dwProcessId; // eax
  wint_t *v5; // rax
  void *v6; // r13
  void **lpWideCharStr_4; // rdx
  void *v8; // rcx
  void *C; // rcx
  void **v10; // rcx
  char *i_1; // rbx
  char *i; // rax
  char *v13; // r11
  char *i_2; // rdx
  __int16 n92; // cx
  __int64 v16; // rax
  void *C_1; // rcx
  void *v18; // rcx
  char p_LastError_1; // r14
  unsigned __int64 v20; // r8
  __int64 v21; // rcx
  __int64 v22; // r15
  __int64 v23; // rdx
  __int64 v24; // rbx
  _QWORD *v25; // rax
  __int64 v26; // rcx
  bool v27; // bl
  void *v28; // rcx
  _QWORD *v29; // rax
  void **v30; // rbx
  void **v31; // rsi
  __int64 v32; // rdi
  char *v33; // rbx
  char *v34; // rdi
  unsigned __int64 n0x10; // rdx
  _QWORD *v36; // rcx
  char *v37; // rax
  void *v38; // rcx
  char v39; // dl
  unsigned __int64 v40; // r13
  __int64 v41; // r14
  unsigned __int64 v42; // rbx
  __int64 v43; // rcx
  void **v44; // rax
  void *v45; // rcx
  void *v46; // rcx
  __int64 v47; // rax
  void *v48; // rcx
  __int64 v49; // rcx
  void **v50; // rax
  void *v51; // rcx
  void *v52; // rcx
  __int64 v53; // rax
  __int64 v54; // rcx
  void **v55; // rax
  void *v56; // rcx
  void *v57; // rcx
  __int64 v58; // rax
  __int64 v59; // rax
  void *v60; // rcx
  void *v61; // rcx
  __int64 v62; // rax
  _BYTE *lpWideCharStr_2; // r10
  unsigned __int64 v64; // r9
  __int64 v65; // rbx
  char v66; // r8
  void **lpWideCharStr_1; // rax
  void **lpWideCharStr_3; // rax
  void *v69; // rcx
  __int64 v70; // rax
  void *v71; // rcx
  void *lpWideCharStr[2]; // [rsp+30h] [rbp-79h] BYREF
  __m128i si128; // [rsp+40h] [rbp-69h]
  void *MultiByteStr[4]; // [rsp+50h] [rbp-59h] BYREF
  __int128 v75; // [rsp+70h] [rbp-39h]
  void *v76; // [rsp+80h] [rbp-29h]
  wint_t p_C[8]; // [rsp+90h] [rbp-19h] BYREF
  void *v78; // [rsp+A0h] [rbp-9h]
  unsigned __int64 n8; // [rsp+A8h] [rbp-1h]
  void *v80[3]; // [rsp+B0h] [rbp+7h] BYREF
  unsigned __int64 n8_1; // [rsp+C8h] [rbp+1Fh]
  WINERROR p_LastError; // [rsp+110h] [rbp+67h] BYREF
  char v83; // [rsp+118h] [rbp+6Fh]
  unsigned __int64 v84; // [rsp+120h] [rbp+77h]

  v2 = 0;
  p_LastError = ERROR_SEVERITY_SUCCESS;
  sub_18000A450(a1 + 24);
  pid = getpid();
  dwProcessId = sub_180008C70(pid);
  *(_DWORD *)(a1 + 24) = dwProcessId;
  v5 = sub_180008D00(p_C, dwProcessId);
  sub_1800067E0(lpWideCharStr, v5);
  v6 = (void *)(a1 + 32);
  if ( (void **)(a1 + 32) != lpWideCharStr )
  {
    lpWideCharStr_4 = lpWideCharStr;
    if ( si128.m128i_i64[1] >= 8uLL )
      lpWideCharStr_4 = (void **)lpWideCharStr[0];
    sub_180007910((void *)(a1 + 32), lpWideCharStr_4);
  }
  if ( si128.m128i_i64[1] >= 8uLL )
  {
    v8 = lpWideCharStr[0];
    if ( (unsigned __int64)(2 * si128.m128i_i64[1] + 2) >= 0x1000 )
    {
      v8 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
      if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v8 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v8);
  }
  si128 = _mm_load_si128((const __m128i *)&xmmword_18004DF40);
  LOWORD(lpWideCharStr[0]) = 0;
  if ( n8 >= 8 )
  {
    C = *(void **)p_C;
    if ( 2 * n8 + 2 >= 0x1000 )
    {
      C = *(void **)(*(_QWORD *)p_C - 8LL);
      if ( (unsigned __int64)(*(_QWORD *)p_C - (_QWORD)C - 8LL) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(C);
  }
  sub_180007130((LPCWCH)(a1 + 32));
  v10 = v80;
  if ( n8_1 >= 8 )
    v10 = (void **)v80[0];
  i_1 = (char *)v10 + 2 * (__int64)v80[2];
  for ( i = (char *)sub_180006360(v10, i_1); i != i_1; i += 2 )
  {
    if ( *(_WORD *)i != 92 && *(_WORD *)i != 47 )
    {
      do
      {
        i_2 = v13 - 2;
        n92 = *((_WORD *)v13 - 1);
        if ( n92 == 92 )
          break;
        if ( n92 == 47 )
          break;
        v13 -= 2;
      }
      while ( i != i_2 );
      break;
    }
  }
  lpWideCharStr[0] = 0;
  si128.m128i_i64[0] = 0;
  si128.m128i_i64[1] = 7;
  sub_180007910(lpWideCharStr, v13);
  v16 = sub_180006620((LPCWCH)lpWideCharStr);
  sub_180002C00(a1 + 64, v16);
  if ( n8 >= 0x10 )
  {
    C_1 = *(void **)p_C;
    if ( n8 + 1 >= 0x1000 )
    {
      C_1 = *(void **)(*(_QWORD *)p_C - 8LL);
      if ( (unsigned __int64)(*(_QWORD *)p_C - (_QWORD)C_1 - 8LL) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(C_1);
  }
  v78 = 0;
  n8 = 15;
  LOBYTE(p_C[0]) = 0;
  if ( si128.m128i_i64[1] >= 8uLL )
  {
    v18 = lpWideCharStr[0];
    if ( (unsigned __int64)(2 * si128.m128i_i64[1] + 2) >= 0x1000 )
    {
      v18 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
      if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v18 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v18);
  }
  p_LastError_1 = 0;
  LOBYTE(p_LastError) = 0;
  v20 = 0;
  v84 = 0;
  v21 = *(_QWORD *)a1;
  if ( 0xCCCCCCCCCCCCCCCDuLL * ((__int64)(*(_QWORD *)(a1 + 8) - *(_QWORD *)a1) >> 4) )
  {
    v22 = 0;
    v23 = *(_QWORD *)a1;
    while ( 1 )
    {
      if ( (__int64)(*(_QWORD *)(v22 + v21 + 40) - *(_QWORD *)(v22 + v21 + 32)) >> 5 )
      {
        v24 = 0;
        while ( !sub_1800089A0() )
        {
          ++v2;
          v24 += 32;
          v23 = *(_QWORD *)a1;
          if ( v2 >= (__int64)(*(_QWORD *)(*(_QWORD *)a1 + v22 + 40) - *(_QWORD *)(*(_QWORD *)a1 + v22 + 32)) >> 5 )
          {
            v20 = v84;
            goto LABEL_42;
          }
        }
        LOBYTE(p_LastError) = 1;
      }
      else
      {
LABEL_42:
        v21 = v23;
        if ( !p_LastError_1 )
          goto LABEL_85;
      }
      v25 = (_QWORD *)sub_180006E40(v6);
      if ( v25[3] >= 8u )
        v25 = (_QWORD *)*v25;
      v27 = sub_180013AA0(v26, (__int64)v25) == 0;
      if ( si128.m128i_i64[1] >= 8uLL )
      {
        v28 = lpWideCharStr[0];
        if ( (unsigned __int64)(2 * si128.m128i_i64[1] + 2) >= 0x1000 )
        {
          v28 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
          if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v28 - 8) > 0x1F )
            invalid_parameter_noinfo_noreturn();
        }
        j_j_free(v28);
      }
      if ( v27 )
      {
        p_LastError = GetLastError();
        v55 = sub_1800151F0(v54, lpWideCharStr);
        *(_QWORD *)&v75 = &unk_18004B9FB;
        DWORD2(v75) = 0;
        v76 = &unk_18004B9FB;
        if ( *(_QWORD *)&xmmword_180058068 )
        {
          v56 = v55;
          if ( (unsigned __int64)v55[3] >= 0x10 )
            v56 = *v55;
          MultiByteStr[0] = v56;
          MultiByteStr[1] = v55[2];
          *(_OWORD *)p_C = v75;
          v78 = v76;
          sub_18001C700(
            xmmword_180058068,
            (unsigned int)p_C,
            xmmword_180058068,
            (unsigned int)MultiByteStr,
            a1 + 24,
            &p_LastError);
        }
        if ( si128.m128i_i64[1] >= 0x10uLL )
        {
          v57 = lpWideCharStr[0];
          if ( (unsigned __int64)(si128.m128i_i64[1] + 1) >= 0x1000 )
          {
            v57 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
            if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v57 - 8) > 0x1F )
              invalid_parameter_noinfo_noreturn();
          }
          j_j_free(v57);
        }
        v58 = sub_180006E40(v6);
        sub_1800103B0(a1, 0, v58, 10101);
        if ( si128.m128i_i64[1] >= 8uLL )
        {
          v48 = lpWideCharStr[0];
          if ( (unsigned __int64)(2 * si128.m128i_i64[1] + 2) >= 0x1000 )
          {
            v48 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
            if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v48 - 8) > 0x1F )
              invalid_parameter_noinfo_noreturn();
          }
          goto LABEL_164;
        }
        goto LABEL_165;
      }
      v29 = (_QWORD *)sub_180006E40(v6);
      if ( v29[3] >= 8u )
        v29 = (_QWORD *)*v29;
      v30 = sub_1800140C0(a1, MultiByteStr, (__int64)v29);
      v31 = (void **)(a1 + 96);
      if ( (void **)(a1 + 96) == v30 )
      {
        v32 = 0;
      }
      else
      {
        sub_1800174B0(a1 + 96);
        *v31 = *v30;
        *(_QWORD *)(a1 + 104) = v30[1];
        *(_QWORD *)(a1 + 112) = v30[2];
        v32 = 0;
        *v30 = 0;
        v30[1] = 0;
        v30[2] = 0;
      }
      v33 = (char *)MultiByteStr[0];
      if ( MultiByteStr[0] )
      {
        v34 = (char *)MultiByteStr[1];
        if ( MultiByteStr[0] != MultiByteStr[1] )
        {
          do
          {
            n0x10 = *((_QWORD *)v33 + 3);
            if ( n0x10 >= 0x10 )
            {
              v36 = *(_QWORD **)v33;
              if ( n0x10 + 1 >= 0x1000 )
              {
                if ( (unsigned __int64)v36 - *(v36 - 1) - 8 > 0x1F )
                  goto LABEL_88;
                v36 = (_QWORD *)*(v36 - 1);
              }
              j_j_free(v36);
            }
            *((_QWORD *)v33 + 2) = 0;
            *((_QWORD *)v33 + 3) = 15;
            *v33 = 0;
            v33 += 32;
          }
          while ( v33 != v34 );
          v33 = (char *)MultiByteStr[0];
        }
        v37 = v33;
        if ( (((char *)MultiByteStr[2] - (char *)v33) & 0xFFFFFFFFFFFFFFE0uLL) >= 0x1000 )
        {
          v33 = (char *)*((_QWORD *)v33 - 1);
          if ( (unsigned __int64)(v37 - v33 - 8) > 0x1F )
LABEL_88:
            invalid_parameter_noinfo_noreturn();
        }
        j_j_free(v33);
        v32 = 0;
        memset(MultiByteStr, 0, 24);
      }
      if ( si128.m128i_i64[1] >= 8uLL )
      {
        v38 = lpWideCharStr[0];
        if ( (unsigned __int64)(2 * si128.m128i_i64[1] + 2) >= 0x1000 )
        {
          v38 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
          if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v38 - 8) > 0x1F )
            invalid_parameter_noinfo_noreturn();
        }
        j_j_free(v38);
      }
      if ( *v31 == *(void **)(a1 + 104) )
      {
        p_LastError = GetLastError();
        v50 = sub_180015330(v49, lpWideCharStr);
        *(_QWORD *)&v75 = &unk_18004B9FB;
        DWORD2(v75) = 0;
        v76 = &unk_18004B9FB;
        if ( *(_QWORD *)&xmmword_180058068 )
        {
          v51 = v50;
          if ( (unsigned __int64)v50[3] >= 0x10 )
            v51 = *v50;
          MultiByteStr[0] = v51;
          MultiByteStr[1] = v50[2];
          *(_OWORD *)p_C = v75;
          v78 = v76;
          sub_18001C700(
            xmmword_180058068,
            (unsigned int)p_C,
            xmmword_180058068,
            (unsigned int)MultiByteStr,
            a1 + 24,
            &p_LastError);
        }
        if ( si128.m128i_i64[1] >= 0x10uLL )
        {
          v52 = lpWideCharStr[0];
          if ( (unsigned __int64)(si128.m128i_i64[1] + 1) >= 0x1000 )
          {
            v52 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
            if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v52 - 8) > 0x1F )
              invalid_parameter_noinfo_noreturn();
          }
          j_j_free(v52);
        }
        v53 = sub_180006E40(v6);
        sub_1800103B0(a1, 0, v53, 10101);
        if ( si128.m128i_i64[1] >= 8uLL )
        {
          v48 = lpWideCharStr[0];
          if ( (unsigned __int64)(2 * si128.m128i_i64[1] + 2) >= 0x1000 )
          {
            v48 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
            if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v48 - 8) > 0x1F )
              invalid_parameter_noinfo_noreturn();
          }
          goto LABEL_164;
        }
        goto LABEL_165;
      }
      v21 = *(_QWORD *)a1;
      if ( (__int64)(*(_QWORD *)(*(_QWORD *)a1 + v22 + 64) - *(_QWORD *)(*(_QWORD *)a1 + v22 + 56)) >> 5 )
      {
        v39 = 0;
        v83 = 0;
        v40 = 0;
        v41 = 0;
        do
        {
          v42 = 0;
          if ( (__int64)(*(_QWORD *)(a1 + 104) - *(_QWORD *)(a1 + 96)) >> 5 )
          {
            while ( !sub_1800089A0() )
            {
              ++v42;
              v32 += 32;
              if ( v42 >= (__int64)(*(_QWORD *)(a1 + 104) - *(_QWORD *)(a1 + 96)) >> 5 )
              {
                v39 = v83;
                goto LABEL_80;
              }
            }
            v39 = 1;
            v83 = 1;
LABEL_80:
            v32 = 0;
          }
          ++v40;
          v41 += 32;
          v21 = *(_QWORD *)a1;
        }
        while ( v40 < (__int64)(*(_QWORD *)(*(_QWORD *)a1 + v22 + 64) - *(_QWORD *)(*(_QWORD *)a1 + v22 + 56)) >> 5 );
        if ( !v39 )
        {
          p_LastError = GetLastError();
          v44 = sub_180015470(v43, lpWideCharStr);
          *(_QWORD *)&v75 = &unk_18004B9FB;
          DWORD2(v75) = 0;
          v76 = &unk_18004B9FB;
          if ( *(_QWORD *)&xmmword_180058068 )
          {
            v45 = v44;
            if ( (unsigned __int64)v44[3] >= 0x10 )
              v45 = *v44;
            MultiByteStr[0] = v45;
            MultiByteStr[1] = v44[2];
            *(_OWORD *)p_C = v75;
            v78 = v76;
            sub_18001C700(
              xmmword_180058068,
              (unsigned int)p_C,
              xmmword_180058068,
              (unsigned int)MultiByteStr,
              a1 + 24,
              &p_LastError);
          }
          if ( si128.m128i_i64[1] >= 0x10uLL )
          {
            v46 = lpWideCharStr[0];
            if ( (unsigned __int64)(si128.m128i_i64[1] + 1) >= 0x1000 )
            {
              v46 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
              if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v46 - 8) > 0x1F )
                invalid_parameter_noinfo_noreturn();
            }
            j_j_free(v46);
          }
          v47 = sub_180006E40((void *)(a1 + 32));
          sub_1800103B0(a1, 0, v47, 10102);
          if ( si128.m128i_i64[1] >= 8uLL )
          {
            v48 = lpWideCharStr[0];
            if ( (unsigned __int64)(2 * si128.m128i_i64[1] + 2) >= 0x1000 )
            {
              v48 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
              if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v48 - 8) > 0x1F )
                invalid_parameter_noinfo_noreturn();
            }
            goto LABEL_164;
          }
          goto LABEL_165;
        }
        v6 = (void *)(a1 + 32);
      }
      v20 = v84;
LABEL_85:
      v84 = ++v20;
      v22 += 80;
      v23 = v21;
      if ( v20 >= 0xCCCCCCCCCCCCCCCDuLL * ((*(_QWORD *)(a1 + 8) - v21) >> 4) )
      {
        if ( !(_BYTE)p_LastError )
          break;
        v59 = sub_1800155B0(v21, lpWideCharStr);
        *(_QWORD *)&v75 = &unk_18004B9FB;
        DWORD2(v75) = 0;
        v76 = &unk_18004B9FB;
        if ( *(_QWORD *)&xmmword_180058068 )
        {
          v60 = (void *)v59;
          if ( *(_QWORD *)(v59 + 24) >= 0x10u )
            v60 = *(void **)v59;
          MultiByteStr[0] = v60;
          MultiByteStr[1] = *(void **)(v59 + 16);
          *(_OWORD *)p_C = v75;
          v78 = v76;
          sub_18001C930(xmmword_180058068, (unsigned int)p_C, xmmword_180058068, (unsigned int)MultiByteStr, a1 + 24);
        }
        if ( si128.m128i_i64[1] >= 0x10uLL )
        {
          v61 = lpWideCharStr[0];
          if ( (unsigned __int64)(si128.m128i_i64[1] + 1) >= 0x1000 )
          {
            v61 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
            if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v61 - 8) > 0x1F )
              invalid_parameter_noinfo_noreturn();
          }
          j_j_free(v61);
        }
        v62 = sub_180006E40(v6);
        sub_1800103B0(a1, 1, v62, 0);
        if ( si128.m128i_i64[1] >= 8uLL )
        {
          v48 = lpWideCharStr[0];
          if ( (unsigned __int64)(2 * si128.m128i_i64[1] + 2) >= 0x1000 )
          {
            v48 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
            if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v48 - 8) > 0x1F )
              invalid_parameter_noinfo_noreturn();
          }
          goto LABEL_164;
        }
        goto LABEL_165;
      }
      p_LastError_1 = p_LastError;
      v2 = 0;
    }
  }
  *(__m128i *)MultiByteStr = _mm_load_si128((const __m128i *)&xmmword_18004E2C0);
  LODWORD(MultiByteStr[2]) = -1001238737;
  WORD2(MultiByteStr[2]) = 15890;
  BYTE6(MultiByteStr[2]) = 48;
  lpWideCharStr[0] = 0;
  si128.m128i_i64[0] = 0;
  si128.m128i_i64[1] = 15;
  p_LastError = ERROR_CRC;
  lpWideCharStr_2 = operator new(0x20u);
  si128.m128i_i64[1] = 31;
  *lpWideCharStr_2 = lpWideCharStr[0];
  lpWideCharStr[0] = lpWideCharStr_2;
  v64 = 0;
  si128.m128i_i64[0] = 0;
  v65 = 0;
  while ( 1 )
  {
    v66 = *((_BYTE *)MultiByteStr + v65)
        ^ (44 - 125 * v65)
        ^ (0x98BBF80E >> (8 * (v65 & 3)))
        ^ (0xE54819D8 >> (8 * (((unsigned __int8)MultiByteStr + v65 + 1LL - (_QWORD)MultiByteStr) & 3)));
    if ( v64 >= si128.m128i_i64[1] )
    {
      sub_180009740(lpWideCharStr);
    }
    else
    {
      si128.m128i_i64[0] = v64 + 1;
      lpWideCharStr_1 = lpWideCharStr;
      if ( si128.m128i_i64[1] >= 0x10uLL )
        lpWideCharStr_1 = (void **)lpWideCharStr_2;
      *((_BYTE *)lpWideCharStr_1 + v64) = v66;
      *((_BYTE *)lpWideCharStr_1 + v64 + 1) = 0;
    }
    if ( (unsigned __int64)++v65 >= 0x16 )
      break;
    v64 = si128.m128i_i64[0];
    lpWideCharStr_2 = lpWideCharStr[0];
  }
  *(_QWORD *)&v75 = &unk_18004B9FB;
  DWORD2(v75) = 0;
  v76 = &unk_18004B9FB;
  if ( *(_QWORD *)&xmmword_180058068 )
  {
    lpWideCharStr_3 = lpWideCharStr;
    if ( si128.m128i_i64[1] >= 0x10uLL )
      lpWideCharStr_3 = (void **)lpWideCharStr[0];
    MultiByteStr[0] = lpWideCharStr_3;
    MultiByteStr[1] = (void *)si128.m128i_i64[0];
    *(_OWORD *)p_C = v75;
    v78 = v76;
    sub_18001A5A0(*(_QWORD *)&xmmword_180058068, (void **)p_C, 2, MultiByteStr);
  }
  if ( si128.m128i_i64[1] >= 0x10uLL )
  {
    v69 = lpWideCharStr[0];
    if ( (unsigned __int64)(si128.m128i_i64[1] + 1) >= 0x1000 )
    {
      v69 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
      if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v69 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v69);
  }
  v70 = sub_180006E40(v6);
  sub_1800103B0(a1, 0, v70, 10100);
  if ( si128.m128i_i64[1] >= 8uLL )
  {
    v48 = lpWideCharStr[0];
    if ( (unsigned __int64)(2 * si128.m128i_i64[1] + 2) >= 0x1000 )
    {
      v48 = (void *)*((_QWORD *)lpWideCharStr[0] - 1);
      if ( (unsigned __int64)((char *)lpWideCharStr[0] - (char *)v48 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
LABEL_164:
    j_j_free(v48);
  }
LABEL_165:
  if ( n8_1 >= 8 )
  {
    v71 = v80[0];
    if ( 2 * n8_1 + 2 >= 0x1000 )
    {
      v71 = (void *)*((_QWORD *)v80[0] - 1);
      if ( (unsigned __int64)((char *)v80[0] - (char *)v71 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v71);
  }
}
