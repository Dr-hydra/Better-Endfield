LPVOID __fastcall sub_1800AFAF0(__int64 a1, __int64 a2, __int64 a3)
{
  LPVOID n15; // rax
  __int64 v4; // rsi
  __int64 n15_1; // r14
  char *v6; // rdi
  char *v7; // r15
  unsigned __int64 n0x10; // r12
  __int64 v9; // r13
  __int64 v10; // rax
  __int128 *v11; // rbx
  char *v12; // rbx
  char *v13; // r14
  __int128 *v14; // rsi
  unsigned __int64 v15; // rdi
  char *v16; // rcx
  __int128 v17; // xmm0
  char *v18; // r14
  _QWORD *v19; // rcx
  __int128 v20; // xmm0
  unsigned __int64 n0x10_1; // rax
  void **v22; // rsi
  char *n15_3; // r8
  __int128 v24; // xmm0
  __int16 v25; // bx
  __int64 v26; // r14
  __int128 v27; // rdi
  __int128 v28; // xmm1
  __int128 v29; // xmm2
  __int64 v30; // r15
  _QWORD *v31; // rcx
  _OWORD *v32; // r12
  unsigned __int64 n0x10_3; // rax
  _QWORD *v34; // rcx
  void *v35; // rcx
  void *v36; // rcx
  unsigned __int64 n0x10_2; // rax
  _QWORD *v38; // rcx
  __int64 v39; // rax
  unsigned int v40; // eax
  __int64 v41; // rdi
  __int128 *v42; // rbx
  __int64 v43; // rcx
  _QWORD *v44; // rax
  _QWORD *v45; // rax
  _QWORD *v46; // rcx
  LPVOID v47; // rdx
  unsigned __int64 n0x18_1; // r8
  __int64 v49; // r8
  unsigned __int64 v50; // r9
  __m128i v51; // xmm0
  __int64 v52; // r10
  __int128 *v53; // r14
  char *v54; // rsi
  __int64 v55; // rax
  __int64 v56; // r14
  int v57; // esi
  __int128 v1023_1; // xmm0
  __int128 v59; // xmm2
  __int128 v60; // xmm3
  unsigned int v61; // esi
  __int64 v62; // rax
  unsigned int v63; // eax
  __int64 v64; // rdi
  int v65; // eax
  void (__fastcall *v66)(__int64, __int64, _QWORD, _QWORD, int, int); // rsi
  int n0x1000_1; // r14d
  __int64 v68; // rax
  __int64 v69; // rax
  char *v70; // rdx
  unsigned __int64 v71; // r8
  unsigned __int64 v72; // r9
  __int64 n0x1000_2; // r14
  __int64 v74; // rdi
  __int64 v75; // rcx
  unsigned __int64 v76; // rax
  __int64 v77; // rsi
  __int64 n7_1; // rdi
  __int128 *v79; // r14
  __int64 v80; // r12
  unsigned __int64 v81; // rdx
  __int64 v82; // rax
  _QWORD *v83; // r15
  LPVOID *v84; // rsi
  _QWORD *v85; // rsi
  _QWORD *v86; // rdx
  __int64 v87; // rax
  __int64 v88; // rdx
  __int64 v89; // r8
  __int64 n7_2; // r14
  __int64 n22; // rdi
  __int64 v92; // rcx
  unsigned __int64 v93; // rax
  unsigned __int64 v94; // rax
  float v95; // xmm0_4
  unsigned __int64 n0x200; // rdi
  float v97; // xmm2_4
  unsigned __int64 n8_1; // rdx
  __int64 v99; // r8
  __int64 v100; // r9
  unsigned __int64 n8; // rax
  __int64 n8_2; // rcx
  unsigned __int64 n0x80; // rdi
  unsigned __int64 v104; // rcx
  char v105; // cl
  __int64 n0x200_1; // rsi
  _QWORD *v107; // rax
  _QWORD *v108; // rdx
  __int64 v109; // r8
  __int64 v110; // rbx
  __int64 v111; // rax
  _QWORD *v112; // rdi
  unsigned __int64 v113; // r14
  unsigned __int64 n0x18; // r8
  __int64 v115; // r8
  unsigned __int64 v116; // rcx
  __m128i v117; // xmm0
  __int64 v118; // r9
  __int64 v119; // rax
  void *v120; // rcx
  unsigned __int64 v121; // rax
  __m128i v122; // xmm0
  __int64 v123; // rdx
  __int64 v124; // rax
  __int64 v125; // rax
  _QWORD *v126; // r11
  _QWORD *v127; // rbx
  unsigned __int64 n4; // r14
  __int64 v129; // r15
  unsigned __int64 v130; // rcx
  unsigned __int64 v131; // rdx
  unsigned __int64 v132; // r8
  __int64 v133; // rcx
  char *v134; // rcx
  __int64 v135; // r8
  __int64 v136; // rdx
  __int64 v137; // rax
  __int64 v138; // r15
  _QWORD *v139; // r13
  _QWORD *v140; // rdi
  _QWORD *v141; // rdx
  _QWORD *v142; // rdx
  _QWORD *v143; // rax
  _QWORD *v144; // r9
  _QWORD *v145; // rcx
  _QWORD *v146; // rdx
  _QWORD *v147; // r8
  _QWORD *v148; // r8
  _QWORD *v149; // rax
  _QWORD *v150; // rcx
  _QWORD *v151; // rdx
  _QWORD *v152; // rax
  _QWORD *v153; // r9
  _QWORD *v154; // rcx
  _QWORD *v155; // rdx
  _QWORD *v156; // r8
  __int64 v157; // rax
  LPVOID *v158; // rdi
  LPVOID v159; // rbx
  LPVOID *v160; // rsi
  _QWORD *v161; // rdx
  _QWORD *v162; // rcx
  __int64 v163; // rax
  __int64 v164; // r12
  LPVOID *v165; // rdx
  _QWORD *v166; // rcx
  __int64 v167; // rax
  __int64 v168; // rsi
  __int64 v169; // rax
  void *v170; // rcx
  void *v171; // rcx
  void **p_??_7runtime_error@std@@6B@; // [rsp+38h] [rbp-48h] BYREF
  __int128 v173; // [rsp+40h] [rbp-40h] BYREF
  void **p_??_7runtime_error@std@@6B@_2; // [rsp+50h] [rbp-30h] BYREF
  __int128 v175; // [rsp+58h] [rbp-28h] BYREF
  void **p_??_7runtime_error@std@@6B@_1; // [rsp+68h] [rbp-18h] BYREF
  __int128 v177; // [rsp+70h] [rbp-10h] BYREF
  void **p_??_7exception@std@@6B@; // [rsp+80h] [rbp+0h] BYREF
  __int128 v179; // [rsp+88h] [rbp+8h] BYREF
  __int64 v180; // [rsp+98h] [rbp+18h] BYREF
  __int128 v1023; // [rsp+A0h] [rbp+20h] BYREF
  __int128 v182; // [rsp+B0h] [rbp+30h]
  __int128 v183; // [rsp+C0h] [rbp+40h]
  _BYTE v184[48]; // [rsp+D0h] [rbp+50h] BYREF
  __int128 v185; // [rsp+100h] [rbp+80h]
  __int128 v186; // [rsp+110h] [rbp+90h]
  __int64 v187; // [rsp+120h] [rbp+A0h]
  __int64 v188; // [rsp+128h] [rbp+A8h]
  unsigned __int64 v189; // [rsp+130h] [rbp+B0h]
  __int64 v190; // [rsp+138h] [rbp+B8h]
  __int128 v191; // [rsp+140h] [rbp+C0h] BYREF
  __int64 v192; // [rsp+150h] [rbp+D0h]
  void *n15_2; // [rsp+158h] [rbp+D8h]
  _QWORD *v194; // [rsp+168h] [rbp+E8h]
  _QWORD *v195; // [rsp+170h] [rbp+F0h]
  __int64 v196; // [rsp+178h] [rbp+F8h]
  __int64 v197; // [rsp+180h] [rbp+100h]
  int n0x1000; // [rsp+188h] [rbp+108h]
  __int128 v199; // [rsp+190h] [rbp+110h] BYREF
  __int128 n7; // [rsp+1A0h] [rbp+120h]
  __int64 v201; // [rsp+1B0h] [rbp+130h]
  _QWORD *v202; // [rsp+1B8h] [rbp+138h]
  __int128 v203; // [rsp+1C0h] [rbp+140h] BYREF
  __int64 v204; // [rsp+1D0h] [rbp+150h]
  _QWORD *v205; // [rsp+1E0h] [rbp+160h]
  unsigned int v206; // [rsp+1ECh] [rbp+16Ch]
  __int64 v207; // [rsp+1F0h] [rbp+170h]

  v207 = -2;
  n15 = n15;
  if ( n15 != *(&n15 + 1) )
  {
    v4 = *((_QWORD *)n15 + 4);
    n15_1 = 0x7FFFFFFFFFFFFFFFLL;
    if ( v4 == 0x7FFFFFFFFFFFFFFFLL )
      std::vector<void *>::_Xlen();
    v6 = (char *)n15 + 16;
    v7 = (char *)*((_QWORD *)n15 + 2);
    n0x10 = *((_QWORD *)n15 + 5);
    v191 = 0;
    v9 = v4 + 1;
    if ( (unsigned __int64)(v4 + 1) < 0x10 )
    {
      v11 = &v191;
      n15_1 = 15;
    }
    else
    {
      if ( v4 < -1 )
        goto LABEL_8;
      n15_1 = 22;
      if ( (v9 | 0xFuLL) >= 0x17 )
        n15_1 = v9 | 0xF;
      if ( (v9 | 0xFuLL) < 0xFFF )
      {
        v11 = (__int128 *)sub_1800FFF00(n15_1 + 1, a2, a3);
      }
      else
      {
LABEL_8:
        v10 = sub_1800FFF00(n15_1 + 40, a2, a3);
        v11 = (__int128 *)((v10 + 39) & 0xFFFFFFFFFFFFFFE0uLL);
        *((_QWORD *)v11 - 1) = v10;
      }
      *(_QWORD *)&v191 = v11;
    }
    if ( n0x10 >= 0x10 )
      v6 = v7;
    v192 = v4 + 1;
    n15_2 = (void *)n15_1;
    sub_180200FC0(v11, v6, v4);
    *(_WORD *)((char *)v11 + v4) = 45;
    v12 = (char *)*(&xmmword_18026E6D8 + 1);
    v13 = (char *)xmmword_18026E6D8;
    if ( xmmword_18026E6D8 != *(&xmmword_18026E6D8 + 1) )
    {
      if ( (unsigned __int64)n15_2 < 0x10 )
        v14 = &v191;
      else
        v14 = (__int128 *)v191;
      v15 = v192;
      while ( 1 )
      {
        if ( *((_QWORD *)v13 + 4) >= v15 )
        {
          v16 = *((_QWORD *)v13 + 5) >= 0x10u ? (char *)*((_QWORD *)v13 + 2) : v13 + 16;
          if ( !(unsigned int)sub_180200EC0(v16, v14) )
            break;
        }
        v13 += 48;
        if ( v13 == v12 )
          goto LABEL_35;
      }
    }
    if ( v13 != v12 )
    {
      n0x1000 = *((_DWORD *)v13 + 2);
      v197 = *(_QWORD *)v13;
      n7 = 0;
      v199 = 0;
      v17 = *((_OWORD *)v13 + 1);
      n7 = *((_OWORD *)v13 + 2);
      v199 = v17;
      *((_QWORD *)v13 + 4) = 0;
      *((_QWORD *)v13 + 5) = 15;
      v13[16] = 0;
      v18 = v13 + 48;
      if ( v18 != v12 )
      {
        do
        {
          *((_DWORD *)v18 - 10) = *((_DWORD *)v18 + 2);
          n0x10_1 = *((_QWORD *)v18 - 1);
          *((_QWORD *)v18 - 6) = *(_QWORD *)v18;
          v22 = (void **)(v18 - 32);
          if ( n0x10_1 >= 0x10 )
          {
            v19 = *v22;
            if ( n0x10_1 + 1 >= 0x1000 )
            {
              if ( (unsigned __int64)v19 - *(v19 - 1) - 8 >= 0x20 )
                goto LABEL_34;
              v19 = (_QWORD *)*(v19 - 1);
            }
            sub_1800FFFE0(v19);
          }
          *((_QWORD *)v18 - 2) = 0;
          *((_QWORD *)v18 - 1) = 15;
          v20 = *((_OWORD *)v18 + 1);
          *((_OWORD *)v18 - 1) = *((_OWORD *)v18 + 2);
          *(_OWORD *)v22 = v20;
          *((_QWORD *)v18 + 4) = 0;
          *((_QWORD *)v18 + 5) = 15;
          v18[16] = 0;
          v18 += 48;
        }
        while ( v18 != v12 );
        v12 = (char *)*(&xmmword_18026E6D8 + 1);
      }
      n0x10_2 = *((_QWORD *)v12 - 1);
      if ( n0x10_2 >= 0x10 )
      {
        v38 = (_QWORD *)*((_QWORD *)v12 - 4);
        if ( n0x10_2 + 1 >= 0x1000 )
        {
          if ( (unsigned __int64)v38 - *(v38 - 1) - 8 >= 0x20 )
            goto LABEL_34;
          v38 = (_QWORD *)*(v38 - 1);
        }
        sub_1800FFFE0(v38);
      }
      *((_QWORD *)v12 - 2) = 0;
      *((_QWORD *)v12 - 1) = 15;
      *(v12 - 32) = 0;
      *(&xmmword_18026E6D8 + 1) = (char *)*(&xmmword_18026E6D8 + 1) - 48;
      v39 = qword_18026FB20(qword_18026FB28, n0x1000);
      if ( !v39 )
        goto LABEL_251;
      v40 = qword_18026F590(v39, 0);
      if ( !v40 )
        goto LABEL_251;
      v206 = v40;
      memset(v184, 0, 44);
      v183 = 0;
      v182 = 0;
      v1023 = 0;
      v41 = qword_18026F598((unsigned int)v197);
      v42 = &v199;
      if ( (unsigned __int64)n7 < 7 )
      {
        v43 = v206;
        goto LABEL_87;
      }
      v53 = &v199;
      if ( *((_QWORD *)&n7 + 1) >= 0x10u )
        v53 = (__int128 *)v199;
      v54 = (char *)v53 + n7;
      v55 = sub_180121AD0(v53, (char *)v53 + n7, "-stream", 7);
      v43 = v206;
      if ( (char *)v55 == v54 || v55 - (_QWORD)v53 == -1 )
      {
LABEL_87:
        v66 = (void (__fastcall *)(__int64, __int64, _QWORD, _QWORD, int, int))qword_18026FB18;
        n0x1000_1 = n0x1000;
        v68 = qword_18026F598(v43);
        v66(v41, v68, 0, 0, n0x1000_1, 1);
        v69 = qword_18026F598(v206);
        n0x1000_2 = n0x1000;
        v203 = 0;
        v204 = 0;
        if ( n0x1000 )
        {
          if ( n0x1000 < 0 )
            std::vector<void *>::_Xlen();
          v74 = v69;
          if ( (unsigned int)n0x1000 < 0x1000 )
          {
            v76 = sub_1800FFF00(n0x1000, v70, v71);
          }
          else
          {
            v75 = sub_1800FFF00(n0x1000 + 39LL, v70, v71);
            v76 = (v75 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
            *(_QWORD *)(v76 - 8) = v75;
          }
          *(_QWORD *)&v203 = v76;
          v77 = v76 + n0x1000_2;
          v204 = v76 + n0x1000_2;
          v189 = v76;
          sub_180200FC0(v76, v74 + 32, n0x1000_2);
          v190 = v77;
          *((_QWORD *)&v203 + 1) = v77;
        }
        else
        {
          v190 = 0;
          v189 = 0;
        }
        n7_1 = n7;
        v79 = &v199;
        if ( *((_QWORD *)&n7 + 1) >= 0x10u )
          v79 = (__int128 *)v199;
        if ( !(_QWORD)n7 )
        {
          v80 = 0xCBF29CE484222325uLL;
          goto LABEL_111;
        }
        if ( (unsigned __int64)n7 < 4 )
        {
          v80 = 0xCBF29CE484222325uLL;
          v81 = 0;
          goto LABEL_109;
        }
        if ( (unsigned __int64)(n7 - 4) >> 2 )
        {
          v71 = ((unsigned __int64)(n7 - 4) >> 2) + 1;
          v72 = v71 & 0xFFFFFFFFFFFFFFFEuLL;
          v80 = 0xCBF29CE484222325uLL;
          v70 = 0;
          do
          {
            v80 = 0x100000001B3LL
                * ((0x100000001B3LL
                  * ((0x100000001B3LL
                    * ((0x100000001B3LL
                      * ((0x100000001B3LL
                        * ((0x100000001B3LL
                          * ((0x100000001B3LL
                            * ((0x100000001B3LL * (v80 ^ (unsigned __int8)v70[(_QWORD)v79]))
                             ^ (unsigned __int8)v70[(_QWORD)v79 + 1]))
                           ^ (unsigned __int8)v70[(_QWORD)v79 + 2]))
                         ^ (unsigned __int8)v70[(_QWORD)v79 + 3]))
                       ^ (unsigned __int8)v70[(_QWORD)v79 + 4]))
                     ^ (unsigned __int8)v70[(_QWORD)v79 + 5]))
                   ^ (unsigned __int8)v70[(_QWORD)v79 + 6]))
                 ^ (unsigned __int8)v70[(_QWORD)v79 + 7]);
            v70 += 8;
            v72 -= 2LL;
          }
          while ( v72 );
          if ( (v71 & 1) == 0 )
            goto LABEL_107;
        }
        else
        {
          v80 = 0xCBF29CE484222325uLL;
          v70 = 0;
        }
        v72 = 0x100000001B3LL
            * ((0x100000001B3LL * (v80 ^ (unsigned __int8)v70[(_QWORD)v79])) ^ (unsigned __int8)v70[(_QWORD)v79 + 1]);
        v71 = 0x100000001B3LL * (v72 ^ (unsigned __int8)v70[(_QWORD)v79 + 2]);
        v80 = 0x100000001B3LL * (v71 ^ (unsigned __int8)v70[(_QWORD)v79 + 3]);
LABEL_107:
        if ( (n7 & 3) == 0 )
          goto LABEL_111;
        v81 = n7 & 0xFFFFFFFFFFFFFFFCuLL;
LABEL_109:
        v70 = (char *)v79 + v81;
        v71 = 0;
        v72 = v80;
        do
        {
          v80 = 0x100000001B3LL * (v72 ^ (unsigned __int8)v70[v71++]);
          v72 = v80;
        }
        while ( (n7 & 3) != v71 );
LABEL_111:
        v82 = 16 * (v80 & qword_18026E750);
        v83 = *(_QWORD **)(xmmword_18026E738 + v82 + 8);
        v84 = (LPVOID *)xmmword_18026E728;
        if ( v83 != xmmword_18026E728 )
        {
          v85 = *(_QWORD **)(xmmword_18026E738 + v82);
          if ( !(_QWORD)n7 )
          {
            while ( v83[4] )
            {
              if ( v83 == v85 )
                goto LABEL_125;
              v83 = (_QWORD *)v83[1];
            }
            goto LABEL_243;
          }
          if ( (_QWORD)n7 != v83[4] )
            goto LABEL_118;
LABEL_114:
          if ( v83[5] < 0x10u )
            v86 = v83 + 2;
          else
            v86 = (_QWORD *)v83[2];
          if ( !(unsigned int)sub_180200EC0(v79, v86) )
            goto LABEL_243;
LABEL_118:
          while ( v83 != v85 )
          {
            v83 = (_QWORD *)v83[1];
            if ( n7_1 == v83[4] )
              goto LABEL_114;
          }
LABEL_125:
          v84 = (LPVOID *)v83;
        }
        if ( *(&xmmword_18026E728 + 1) == (LPVOID)0x38E38E38E38E38ELL )
          sub_180103890("unordered_map/set too long", v70, v71, v72);
        v201 = 0;
        v87 = sub_1800FFF00(72, v70, v71);
        v83 = (_QWORD *)v87;
        *(_OWORD *)(v87 + 32) = 0;
        *(_OWORD *)(v87 + 16) = 0;
        n7_2 = n7;
        if ( *((_QWORD *)&n7 + 1) >= 0x10u )
          v42 = (__int128 *)v199;
        if ( (__int64)n7 < 0 )
        {
          v201 = v87;
          std::vector<void *>::_Xlen();
        }
        v196 = v87 + 16;
        if ( (unsigned __int64)n7 > 0xF )
        {
          n22 = 22;
          if ( ((unsigned __int64)n7 | 0xF) >= 0x17 )
            n22 = n7 | 0xF;
          v201 = v87;
          if ( ((unsigned __int64)n7 | 0xF) < 0xFFF )
          {
            v93 = sub_1800FFF00(n22 + 1, v88, v89);
          }
          else
          {
            v92 = sub_1800FFF00(n22 + 40, v88, v89);
            v93 = (v92 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
            *(_QWORD *)(v93 - 8) = v92;
          }
          v83[2] = v93;
          v83[4] = n7_2;
          v83[5] = n22;
          sub_180200FC0(v93, v42, n7_2 + 1);
        }
        else
        {
          *(_QWORD *)(v87 + 32) = n7;
          *(_QWORD *)(v87 + 40) = 15;
          *(_OWORD *)(v87 + 16) = *v42;
        }
        *((_OWORD *)v83 + 3) = 0;
        v83[8] = 0;
        v94 = (unsigned __int64)*(&xmmword_18026E728 + 1) + 1;
        if ( (__int64)*(&xmmword_18026E728 + 1) + 1 < 0 )
          v95 = (float)(int)((v94 >> 1) | v94 & 1) + (float)(int)((v94 >> 1) | v94 & 1);
        else
          v95 = (float)(int)v94;
        n0x200 = n0x200;
        if ( n0x200 < 0 )
          v97 = (float)(int)(((unsigned __int64)n0x200 >> 1) | n0x200 & 1)
              + (float)(int)(((unsigned __int64)n0x200 >> 1) | n0x200 & 1);
        else
          v97 = (float)(int)n0x200;
        if ( (float)(v95 / v97) <= *(float *)&dword_18026E720 )
          goto LABEL_235;
        n8_1 = (unsigned int)(int)sub_180200C80();
        n8 = 8;
        if ( n8_1 >= 9 )
          n8 = n8_1;
        if ( n0x200 < n8 )
        {
          n8_2 = 8 * n0x200;
          if ( 8 * n0x200 <= n8 )
            n8_2 = n8;
          if ( n0x200 >= 0x200 )
            n8_2 = n8;
          n0x200 = n8_2;
        }
        v195 = v83;
        if ( n0x200 >= 0x800000000000001LL )
          sub_180103890("invalid hash bucket count", n8_1, v99, v100);
        n0x80 = n0x200 - 1;
        _BitScanReverse64(&v104, n0x80 | 1);
        v105 = v104 + 1;
        n0x200_1 = 1LL << v105;
        v205 = xmmword_18026E728;
        v107 = (_QWORD *)*((_QWORD *)&xmmword_18026E738 + 1);
        v108 = (_QWORD *)xmmword_18026E738;
        v109 = *((_QWORD *)&xmmword_18026E738 + 1) - xmmword_18026E738;
        if ( (__int64)(*((_QWORD *)&xmmword_18026E738 + 1) - xmmword_18026E738) >> 3 < (unsigned __int64)(2LL << v105) )
        {
          v110 = 16LL << v105;
          if ( n0x80 < 0x80 )
          {
            v119 = sub_1800FFF00(16LL << v105, xmmword_18026E738, v109);
            v112 = v205;
            v113 = v119;
          }
          else
          {
            v111 = sub_1800FFF00(v110 + 39, xmmword_18026E738, v109);
            v112 = v205;
            v113 = (v111 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
            *(_QWORD *)(v113 - 8) = v111;
          }
          v120 = (void *)xmmword_18026E738;
          v83 = v195;
          if ( qword_18026E748 != (_QWORD)xmmword_18026E738 )
          {
            if ( (unsigned __int64)(qword_18026E748 - xmmword_18026E738) >= 0x1000 )
            {
              if ( (unsigned __int64)(xmmword_18026E738 - 8 - *(_QWORD *)(xmmword_18026E738 - 8)) >= 0x20 )
                goto LABEL_34;
              v120 = *(void **)(xmmword_18026E738 - 8);
            }
            sub_1800FFFE0(v120);
          }
          *(_QWORD *)&xmmword_18026E738 = v113;
          *((_QWORD *)&xmmword_18026E738 + 1) = v113 + v110;
          qword_18026E748 = v113 + v110;
          v121 = (((unsigned __int64)(v110 - 8) >> 3) + 1) & 0xFFFFFFFFFFFFFFFCuLL;
          v122 = _mm_shuffle_epi32((__m128i)(unsigned __int64)v112, 68);
          v123 = 0;
          do
          {
            *(__m128i *)(v113 + 8 * v123) = v122;
            *(__m128i *)(v113 + 8 * v123 + 16) = v122;
            v123 += 4;
          }
          while ( v121 != v123 );
          if ( ((unsigned __int64)(v110 - 8) >> 3) + 1 != v121 )
          {
            v124 = 8 * v121;
            do
            {
              *(_QWORD *)(v113 + v124) = v112;
              v124 += 8;
            }
            while ( v110 != v124 );
          }
          goto LABEL_176;
        }
        v112 = v205;
        if ( (_QWORD)xmmword_18026E738 != *((_QWORD *)&xmmword_18026E738 + 1) )
        {
          n0x18 = v109 - 8;
          if ( n0x18 < 0x18 )
            goto LABEL_163;
          v115 = (n0x18 >> 3) + 1;
          v116 = v115 & 0xFFFFFFFFFFFFFFFCuLL;
          v117 = _mm_shuffle_epi32((__m128i)(unsigned __int64)v205, 68);
          v118 = 0;
          do
          {
            *(__m128i *)&v108[v118] = v117;
            *(__m128i *)&v108[v118 + 2] = v117;
            v118 += 4;
          }
          while ( v116 != v118 );
          if ( v115 != v116 )
          {
            v108 += v116;
            do
LABEL_163:
              *v108++ = v112;
            while ( v108 != v107 );
          }
        }
LABEL_176:
        v125 = n0x200_1 - 1;
        qword_18026E750 = n0x200_1 - 1;
        n0x200 = n0x200_1;
        v84 = (LPVOID *)xmmword_18026E728;
        v126 = *(_QWORD **)xmmword_18026E728;
        if ( *(_QWORD **)xmmword_18026E728 == v112 )
          goto LABEL_218;
        while ( 1 )
        {
          if ( v126[5] < 0x10u )
            v127 = v126 + 2;
          else
            v127 = (_QWORD *)v126[2];
          n4 = v126[4];
          v129 = 0xCBF29CE484222325uLL;
          if ( !n4 )
            goto LABEL_197;
          if ( n4 < 4 )
          {
            v130 = 0;
            goto LABEL_195;
          }
          if ( (n4 - 4) >> 2 )
          {
            v131 = ((n4 - 4) >> 2) + 1;
            v132 = v131 & 0xFFFFFFFFFFFFFFFEuLL;
            v133 = 0;
            do
            {
              v129 = 0x100000001B3LL
                   * ((0x100000001B3LL
                     * ((0x100000001B3LL
                       * ((0x100000001B3LL
                         * ((0x100000001B3LL
                           * ((0x100000001B3LL
                             * ((0x100000001B3LL * ((0x100000001B3LL * (v129 ^ LOBYTE(v127[v133]))) ^ BYTE1(v127[v133])))
                              ^ BYTE2(v127[v133])))
                            ^ BYTE3(v127[v133])))
                          ^ BYTE4(v127[v133])))
                        ^ BYTE5(v127[v133])))
                      ^ BYTE6(v127[v133])))
                    ^ HIBYTE(v127[v133]));
              ++v133;
              v132 -= 2LL;
            }
            while ( v132 );
            if ( (v131 & 1) == 0 )
            {
              if ( (n4 & 3) != 0 )
                goto LABEL_194;
              goto LABEL_197;
            }
          }
          else
          {
            v133 = 0;
          }
          v129 = 0x100000001B3LL
               * ((0x100000001B3LL
                 * ((0x100000001B3LL * ((0x100000001B3LL * (v129 ^ LOBYTE(v127[v133]))) ^ BYTE1(v127[v133])))
                  ^ BYTE2(v127[v133])))
                ^ BYTE3(v127[v133]));
          if ( (n4 & 3) != 0 )
          {
LABEL_194:
            v130 = n4 & 0xFFFFFFFFFFFFFFFCuLL;
LABEL_195:
            v134 = (char *)v127 + v130;
            v135 = v129;
            v136 = 0;
            do
            {
              v129 = 0x100000001B3LL * (v135 ^ (unsigned __int8)v134[v136++]);
              v135 = v129;
            }
            while ( (v126[4] & 3LL) != v136 );
          }
LABEL_197:
          v202 = (_QWORD *)*v126;
          v137 = xmmword_18026E738;
          v138 = 16 * (qword_18026E750 & v129);
          v139 = *(_QWORD **)(xmmword_18026E738 + v138);
          if ( v139 == v112 )
          {
            *(_QWORD *)(xmmword_18026E738 + v138) = v126;
            *(_QWORD *)(v137 + v138 + 8) = v126;
            v126 = v202;
          }
          else
          {
            v194 = v126;
            v188 = xmmword_18026E738;
            v140 = *(_QWORD **)(xmmword_18026E738 + v138 + 8);
            if ( v140[5] < 0x10u )
            {
              v141 = v140 + 2;
              if ( n4 != v140[4] )
                goto LABEL_204;
            }
            else
            {
              v141 = (_QWORD *)v140[2];
              if ( n4 != v140[4] )
                goto LABEL_204;
            }
            if ( n4 && (unsigned int)sub_180200EC0(v127, v141) )
            {
              while ( 1 )
              {
                while ( 1 )
                {
LABEL_204:
                  if ( v139 == v140 )
                  {
                    v148 = v194;
                    v149 = (_QWORD *)v194[1];
                    v126 = v202;
                    *v149 = v202;
                    v150 = (_QWORD *)v126[1];
                    *v150 = v140;
                    v151 = (_QWORD *)v140[1];
                    *v151 = v148;
                    v140[1] = v150;
                    v126[1] = v149;
                    v148[1] = v151;
                    *(_QWORD *)(v188 + v138) = v148;
                    goto LABEL_216;
                  }
                  v140 = (_QWORD *)v140[1];
                  if ( v140[5] >= 0x10u )
                    break;
                  v142 = v140 + 2;
                  if ( n4 == v140[4] )
                    goto LABEL_209;
                }
                v142 = (_QWORD *)v140[2];
                if ( n4 == v140[4] )
                {
LABEL_209:
                  if ( !n4 || !(unsigned int)sub_180200EC0(v127, v142) )
                  {
                    v143 = (_QWORD *)*v140;
                    v144 = v194;
                    v145 = (_QWORD *)v194[1];
                    v126 = v202;
                    *v145 = v202;
                    v146 = (_QWORD *)v126[1];
                    *v146 = v143;
                    v147 = (_QWORD *)v143[1];
                    *v147 = v144;
                    v143[1] = v146;
                    v126[1] = v145;
                    v144[1] = v147;
                    goto LABEL_216;
                  }
                }
              }
            }
            v152 = (_QWORD *)*v140;
            v153 = v194;
            v126 = v202;
            if ( (_QWORD *)*v140 != v194 )
            {
              v154 = (_QWORD *)v194[1];
              *v154 = v202;
              v155 = (_QWORD *)v126[1];
              *v155 = v152;
              v156 = (_QWORD *)v152[1];
              *v156 = v153;
              v152[1] = v155;
              v126[1] = v154;
              v153[1] = v156;
            }
            *(_QWORD *)(v188 + v138 + 8) = v153;
LABEL_216:
            v112 = v205;
          }
          v83 = v195;
          if ( v126 == v112 )
          {
            v125 = qword_18026E750;
            v84 = (LPVOID *)xmmword_18026E728;
LABEL_218:
            v157 = 16 * (v80 & v125);
            v158 = *(LPVOID **)(xmmword_18026E738 + v157 + 8);
            if ( v158 != v84 )
            {
              v159 = (LPVOID)v83[4];
              if ( v83[5] >= 0x10u )
                v196 = v83[2];
              v160 = *(LPVOID **)(xmmword_18026E738 + v157);
              if ( v159 )
              {
                if ( v159 != v158[4] )
                  goto LABEL_227;
LABEL_223:
                if ( (unsigned __int64)v158[5] < 0x10 )
                  v161 = v158 + 2;
                else
                  v161 = v158[2];
                if ( (unsigned int)sub_180200EC0(v196, v161) )
                {
LABEL_227:
                  while ( v158 != v160 )
                  {
                    v158 = (LPVOID *)v158[1];
                    if ( v159 == v158[4] )
                      goto LABEL_223;
                  }
LABEL_234:
                  v84 = v158;
                  goto LABEL_235;
                }
              }
              else
              {
                while ( v158[4] )
                {
                  if ( v158 == v160 )
                    goto LABEL_234;
                  v158 = (LPVOID *)v158[1];
                }
              }
              v84 = (LPVOID *)*v158;
            }
LABEL_235:
            v162 = v84[1];
            *(&xmmword_18026E728 + 1) = (char *)*(&xmmword_18026E728 + 1) + 1;
            *v83 = v84;
            v83[1] = v162;
            *v162 = v83;
            v84[1] = v83;
            v163 = xmmword_18026E738;
            v164 = 16 * (qword_18026E750 & v80);
            v165 = *(LPVOID **)(xmmword_18026E738 + v164);
            if ( v165 == xmmword_18026E728 )
            {
              *(_QWORD *)(xmmword_18026E738 + v164) = v83;
              goto LABEL_240;
            }
            if ( v165 == v84 )
            {
              *(_QWORD *)(xmmword_18026E738 + v164) = v83;
            }
            else if ( *(_QWORD **)(xmmword_18026E738 + v164 + 8) == v162 )
            {
LABEL_240:
              *(_QWORD *)(v163 + v164 + 8) = v83;
            }
            sub_1800BE3A0(0);
LABEL_243:
            v166 = (_QWORD *)v83[6];
            if ( !v166 )
              goto LABEL_248;
            if ( v83[8] - (_QWORD)v166 < 0x1000u )
              goto LABEL_247;
            if ( (unsigned __int64)v166 - *(v166 - 1) - 8 < 0x20 )
            {
              v166 = (_QWORD *)*(v166 - 1);
LABEL_247:
              sub_1800FFFE0(v166);
              *((_OWORD *)v83 + 3) = 0;
              v83[8] = 0;
LABEL_248:
              v83[6] = v189;
              v167 = v190;
              v83[7] = v190;
              v83[8] = v167;
              if ( *(_DWORD *)&v184[40] )
              {
                v168 = qword_18026FB38;
                *(_QWORD *)(qword_18026F598(*(unsigned int *)&v184[40]) + v168) = 0;
                qword_18026EC40(*(unsigned int *)&v184[40]);
              }
              qword_18026EC40(v206);
LABEL_251:
              v169 = qword_18026F598((unsigned int)v197);
              if ( qword_18026FB40 )
              {
                v180 = 0;
                qword_18026F348(qword_18026FB40, v169, 0, &v180);
              }
              qword_18026EC40((unsigned int)v197);
              qword_18026EC40(HIDWORD(v197));
              if ( *((_QWORD *)&n7 + 1) < 0x10u )
                goto LABEL_258;
              v170 = (void *)v199;
              if ( (unsigned __int64)(*((_QWORD *)&n7 + 1) + 1LL) < 0x1000 )
                goto LABEL_257;
              if ( (unsigned __int64)(v199 - 8 - *(_QWORD *)(v199 - 8)) < 0x20 )
              {
                v170 = *(void **)(v199 - 8);
LABEL_257:
                sub_1800FFFE0(v170);
                goto LABEL_258;
              }
            }
LABEL_34:
            BUG();
          }
        }
      }
      v56 = *(_QWORD *)(v41 + qword_18026FB38);
      if ( !v56 )
      {
        v173 = 0;
        *(_QWORD *)&v203 = "Missing native buffer descriptor";
        BYTE8(v203) = 1;
        sub_18017B450(&v203, &v173);
        p_??_7runtime_error@std@@6B@ = &std::runtime_error::`vftable';
        sub_180179A30(&p_??_7runtime_error@std@@6B@, &_TI2_AVruntime_error_std__);
        goto LABEL_273;
      }
      v57 = qword_18026FB10(v41);
      if ( *(_DWORD *)(v56 + 28) == v57 && *(_QWORD *)(v56 + 8) * *(_QWORD *)(v56 + 16) == n0x1000 )
      {
        v1023_1 = *(_OWORD *)v56;
        v59 = *(_OWORD *)(v56 + 32);
        v60 = *(_OWORD *)(v56 + 48);
        v182 = *(_OWORD *)(v56 + 16);
        *(_QWORD *)&v184[32] = *(_QWORD *)(v56 + 80);
        *(_OWORD *)&v184[16] = *(_OWORD *)(v56 + 64);
        *(_OWORD *)v184 = v60;
        v183 = v59;
        v1023 = v1023_1;
        v61 = v57 & 0xFFFFFFFD;
        HIDWORD(v182) = v61;
        v62 = qword_18026FB08(qword_18026FB30);
        if ( v62 )
        {
          v63 = qword_18026F590(v62, 0);
          *(_DWORD *)&v184[40] = v63;
          if ( v63 )
          {
            v64 = qword_18026FB38;
            *(_QWORD *)(qword_18026F598(v63) + v64) = &v1023;
            v41 = qword_18026F598(*(unsigned int *)&v184[40]);
            v65 = qword_18026FB10(v41);
            v43 = v206;
            if ( v65 == v61 )
              goto LABEL_87;
            p_??_7exception@std@@6B@ = &std::exception::`vftable';
            v179 = 0;
            *(_QWORD *)&v203 = "Alias target mismatch";
            BYTE8(v203) = 1;
            sub_18017B450(&v203, &v179);
            p_??_7exception@std@@6B@ = &std::runtime_error::`vftable';
            sub_180179A30(&p_??_7exception@std@@6B@, &_TI2_AVruntime_error_std__);
LABEL_273:
            __debugbreak();
            JUMPOUT(0x1800B10C6LL);
          }
        }
        else
        {
          *(_DWORD *)&v184[40] = 0;
        }
        v177 = 0;
        *(_QWORD *)&v203 = "Cannot root read alias";
        BYTE8(v203) = 1;
        sub_18017B450(&v203, &v177);
        p_??_7runtime_error@std@@6B@_1 = &std::runtime_error::`vftable';
        sub_180179A30(&p_??_7runtime_error@std@@6B@_1, &_TI2_AVruntime_error_std__);
        goto LABEL_273;
      }
      v175 = 0;
      *(_QWORD *)&v203 = "Buffer descriptor mismatch";
      BYTE8(v203) = 1;
      sub_18017B450(&v203, &v175);
      p_??_7runtime_error@std@@6B@_2 = &std::runtime_error::`vftable';
      sub_180179A30(&p_??_7runtime_error@std@@6B@_2, &_TI2_AVruntime_error_std__);
      goto LABEL_273;
    }
LABEL_35:
    n15_3 = (char *)n15;
    v1023 = *(_OWORD *)n15;
    v24 = *((_OWORD *)n15 + 1);
    v183 = *((_OWORD *)n15 + 2);
    v182 = v24;
    *((_QWORD *)n15 + 4) = 0;
    *((_QWORD *)n15_3 + 5) = 15;
    n15_3[16] = 0;
    v25 = *((_WORD *)n15_3 + 24);
    *(_WORD *)v184 = v25;
    *((_QWORD *)&v27 + 1) = *((_QWORD *)n15_3 + 9);
    *((_QWORD *)n15_3 + 9) = 0;
    v26 = *((_QWORD *)n15_3 + 7);
    *(_QWORD *)&v27 = *((_QWORD *)n15_3 + 8);
    *(_OWORD *)(n15_3 + 56) = 0;
    *(_QWORD *)&v184[8] = v26;
    *(_OWORD *)&v184[16] = v27;
    v28 = *((_OWORD *)n15_3 + 6);
    v29 = *((_OWORD *)n15_3 + 7);
    *(_OWORD *)&v184[32] = *((_OWORD *)n15_3 + 5);
    v185 = v28;
    v186 = v29;
    v187 = *((_QWORD *)n15_3 + 16);
    sub_1800C3AB0(n15_3 + 136, *(&n15 + 1));
    v30 = (__int64)*(&n15 + 1);
    v31 = (_QWORD *)*((_QWORD *)*(&n15 + 1) - 10);
    if ( v31 )
    {
      if ( *((_QWORD *)*(&n15 + 1) - 8) - (_QWORD)v31 >= 0x1000u )
      {
        if ( (unsigned __int64)v31 - *(v31 - 1) - 8 >= 0x20 )
          goto LABEL_34;
        v31 = (_QWORD *)*(v31 - 1);
      }
      v32 = (char *)*(&n15 + 1) - 80;
      sub_1800FFFE0(v31);
      *v32 = 0;
      *(_QWORD *)(v30 - 64) = 0;
    }
    n0x10_3 = *(_QWORD *)(v30 - 96);
    if ( n0x10_3 >= 0x10 )
    {
      v34 = *(_QWORD **)(v30 - 120);
      if ( n0x10_3 + 1 >= 0x1000 )
      {
        if ( (unsigned __int64)v34 - *(v34 - 1) - 8 >= 0x20 )
          goto LABEL_34;
        v34 = (_QWORD *)*(v34 - 1);
      }
      sub_1800FFFE0(v34);
    }
    *(_QWORD *)(v30 - 104) = 0;
    *(_QWORD *)(v30 - 96) = 15;
    *(_BYTE *)(v30 - 120) = 0;
    *(&n15 + 1) = (char *)*(&n15 + 1) - 136;
    v1023 = v1023;
    byte_18026FB58 = HIBYTE(v25);
    byte_18026FB59 = v25;
    if ( *((_QWORD *)&xmmword_180265D50 + 1) >= 0x10u )
    {
      v35 = (void *)xmmword_180265D40;
      if ( (unsigned __int64)(*((_QWORD *)&xmmword_180265D50 + 1) + 1LL) >= 0x1000 )
      {
        if ( (unsigned __int64)(xmmword_180265D40 - 8 - *(_QWORD *)(xmmword_180265D40 - 8)) >= 0x20 )
          goto LABEL_34;
        v35 = *(void **)(xmmword_180265D40 - 8);
      }
      sub_1800FFFE0(v35);
    }
    xmmword_180265D50 = v183;
    xmmword_180265D40 = v182;
    *(_QWORD *)&v183 = 0;
    *((_QWORD *)&v183 + 1) = 15;
    LOBYTE(v182) = 0;
    v36 = (void *)unk_18026E760;
    if ( unk_18026E760 )
    {
      if ( *((_QWORD *)&xmmword_18026E768 + 1) - unk_18026E760 >= 0x1000u )
      {
        if ( (unsigned __int64)(unk_18026E760 - 8LL - *(_QWORD *)(unk_18026E760 - 8LL)) >= 0x20 )
          goto LABEL_34;
        v36 = *(void **)(unk_18026E760 - 8LL);
      }
      sub_1800FFFE0(v36);
      unk_18026E760 = 0;
      *((_QWORD *)&xmmword_18026E768 + 1) = 0;
    }
    unk_18026E760 = v26;
    xmmword_18026E768 = v27;
    memset(&v184[8], 0, 24);
    qword_18026FB90 = v187;
    xmmword_18026FB80 = v186;
    xmmword_18026FB70 = v185;
    xmmword_18026FB60 = *(_OWORD *)&v184[32];
    sub_1800B2A90();
    if ( n15 == *(&n15 + 1) && *(&xmmword_18026E728 + 1) )
    {
      if ( (unsigned __int64)n0x200 >> 3 > (unsigned __int64)*(&xmmword_18026E728 + 1) )
      {
        sub_1800BDD10(*(_QWORD *)xmmword_18026E728);
        goto LABEL_258;
      }
      sub_180033AB0(xmmword_18026E728);
      v44 = xmmword_18026E728;
      *(_QWORD *)xmmword_18026E728 = xmmword_18026E728;
      v44[1] = v44;
      *(&xmmword_18026E728 + 1) = 0;
      v45 = (_QWORD *)*((_QWORD *)&xmmword_18026E738 + 1);
      v46 = (_QWORD *)xmmword_18026E738;
      if ( (_QWORD)xmmword_18026E738 != *((_QWORD *)&xmmword_18026E738 + 1) )
      {
        v47 = xmmword_18026E728;
        n0x18_1 = *((_QWORD *)&xmmword_18026E738 + 1) - xmmword_18026E738 - 8;
        if ( n0x18_1 >= 0x18 )
        {
          v49 = (n0x18_1 >> 3) + 1;
          v50 = v49 & 0xFFFFFFFFFFFFFFFCuLL;
          v51 = _mm_shuffle_epi32((__m128i)(unsigned __int64)xmmword_18026E728, 68);
          v52 = 0;
          do
          {
            *(__m128i *)&v46[v52] = v51;
            *(__m128i *)&v46[v52 + 2] = v51;
            v52 += 4;
          }
          while ( v50 != v52 );
          if ( v49 == v50 )
            goto LABEL_258;
          v46 += v50;
        }
        do
          *v46++ = v47;
        while ( v46 != v45 );
      }
    }
LABEL_258:
    n15 = n15_2;
    if ( (unsigned __int64)n15_2 >= 0x10 )
    {
      v171 = (void *)v191;
      if ( (unsigned __int64)n15_2 + 1 >= 0x1000 )
      {
        if ( (unsigned __int64)(v191 - 8 - *(_QWORD *)(v191 - 8)) >= 0x20 )
          goto LABEL_34;
        v171 = *(void **)(v191 - 8);
      }
      return (LPVOID)sub_1800FFFE0(v171);
    }
  }
  return n15;
}
