// Hidden C++ exception states: #wind=21
char __fastcall sub_18000EA70(_QWORD *a1, _QWORD *MultiByteStr_1)
{
  _QWORD *v3; // r14
  __int64 v4; // rdi
  __int64 v5; // rbx
  _QWORD *v6; // rax
  unsigned __int64 n15_4; // r9
  unsigned __int64 n0xA; // r8
  unsigned __int64 n0xA_1; // rbx
  char v10; // dl
  void **k_17; // rax
  unsigned __int64 n15_7; // r9
  unsigned __int64 n15_5; // r10
  unsigned __int64 n4; // r8
  unsigned __int64 n4_1; // rbx
  char v16; // dl
  void **v17; // rax
  unsigned __int64 n15_9; // r9
  unsigned __int64 n15_8; // r10
  unsigned __int64 n0xB; // r8
  unsigned __int64 n0xB_1; // rbx
  char v22; // dl
  void **k_14; // rax
  unsigned __int64 n15_12; // r9
  unsigned __int64 n15_10; // r10
  unsigned __int64 n0xC; // r8
  unsigned __int64 n0xC_1; // rbx
  char v28; // dl
  void **k_4; // rax
  unsigned int n340; // r13d
  void **k_15; // rdx
  unsigned int *v32; // r15
  unsigned __int64 i; // rcx
  __int64 i_2; // r12
  __int64 v35; // rdi
  __int64 v36; // rbx
  void *v37; // rcx
  void *v38; // rcx
  void *v39; // rcx
  unsigned int *v40; // rbx
  void **k_6; // r8
  void **k_8; // rdx
  unsigned int *v43; // r15
  unsigned __int64 j; // r14
  _WORD *v45; // rdx
  size_t Size; // r8
  void **v47; // rsi
  void **v48; // rdi
  void **v49; // rbx
  signed __int64 v50; // rsi
  __int64 v51; // rdx
  unsigned __int64 n15_13; // rax
  void *v53; // rcx
  void *v54; // rcx
  __int64 v55; // r12
  unsigned int *v56; // rbx
  void **k_5; // r8
  unsigned int *v58; // rbx
  void **k_1; // rdx
  _BYTE *k_2; // rax
  int k; // ecx
  __int128 *v62; // r15
  unsigned __int64 m; // r14
  _WORD *v64; // rdx
  size_t Size_1; // r8
  void **Src_1; // rsi
  void **Src_2; // rdi
  void **Src_3; // rbx
  signed __int64 v69; // rsi
  __int64 v70; // rdx
  void *v71; // rcx
  int v72; // r13d
  unsigned __int64 v73; // r9
  unsigned __int64 n0x1B; // r8
  unsigned __int64 n0x1B_1; // rbx
  char v76; // dl
  void **Src_4; // rax
  void **Src_5; // rax
  void *v79; // rcx
  _QWORD *v80; // r9
  __int64 v81; // rdx
  __int64 v82; // rcx
  __int64 v83; // rax
  __int64 v84; // rdx
  __int64 v85; // rcx
  __int64 v86; // rax
  __int64 v87; // rbx
  __int64 v88; // rdi
  __int64 v89; // rbx
  __int64 v90; // rdi
  void *v91; // rcx
  void *v92; // rcx
  void *v93; // rcx
  void *v94; // rcx
  void *v95; // rcx
  unsigned __int64 n15_14; // r9
  __int64 v97; // rbx
  unsigned int v98; // r8d
  void **k_9; // rax
  void **k_10; // rax
  void *v101; // rcx
  _BYTE *k_12; // r10
  unsigned __int64 n15_1; // r9
  __int64 v105; // rbx
  char v106; // r8
  void **k_11; // rax
  void **k_13; // rax
  void *v109; // rcx
  void *v110; // rdi
  _QWORD *v111; // rbx
  void *v112; // rcx
  _QWORD *v113; // rax
  __int64 v114; // rax
  void *MultiByteStr; // [rsp+40h] [rbp-238h] BYREF
  _QWORD *MultiByteStr_2; // [rsp+48h] [rbp-230h]
  void *k_7[2]; // [rsp+50h] [rbp-228h] BYREF
  unsigned __int64 n15_2; // [rsp+60h] [rbp-218h]
  unsigned __int64 n15; // [rsp+68h] [rbp-210h]
  void *Block[2]; // [rsp+70h] [rbp-208h] BYREF
  __int128 v121; // [rsp+80h] [rbp-1F8h]
  void *Src[2]; // [rsp+90h] [rbp-1E8h] BYREF
  __int128 v123; // [rsp+A0h] [rbp-1D8h]
  void *lpMultiByteStr[2]; // [rsp+B0h] [rbp-1C8h] BYREF
  int v125; // [rsp+C0h] [rbp-1B8h]
  int v126; // [rsp+C4h] [rbp-1B4h]
  int v127; // [rsp+C8h] [rbp-1B0h]
  void *p_C[2]; // [rsp+D0h] [rbp-1A8h] BYREF
  __int64 v129; // [rsp+E0h] [rbp-198h]
  __int16 n30348; // [rsp+E8h] [rbp-190h]
  char v131; // [rsp+EAh] [rbp-18Eh]
  void *v132[2]; // [rsp+F0h] [rbp-188h] BYREF
  __int128 v133; // [rsp+100h] [rbp-178h]
  __int128 v134; // [rsp+110h] [rbp-168h] BYREF
  __int128 v135; // [rsp+120h] [rbp-158h] BYREF
  __int128 v136; // [rsp+130h] [rbp-148h]
  void *k_3[2]; // [rsp+140h] [rbp-138h] BYREF
  unsigned __int64 n15_17; // [rsp+150h] [rbp-128h]
  unsigned __int64 n15_11; // [rsp+158h] [rbp-120h]
  __int128 v140; // [rsp+160h] [rbp-118h]
  void *k_16[2]; // [rsp+170h] [rbp-108h] BYREF
  unsigned __int64 n15_15; // [rsp+180h] [rbp-F8h]
  unsigned __int64 n15_3; // [rsp+188h] [rbp-F0h]
  __int128 v144; // [rsp+190h] [rbp-E8h] BYREF
  void *v145[2]; // [rsp+1A0h] [rbp-D8h] BYREF
  unsigned __int64 n15_16; // [rsp+1B0h] [rbp-C8h]
  unsigned __int64 n15_6; // [rsp+1B8h] [rbp-C0h]
  __int64 v148; // [rsp+1C0h] [rbp-B8h] BYREF
  __int64 v149; // [rsp+1C8h] [rbp-B0h] BYREF
  __int128 v150; // [rsp+1D0h] [rbp-A8h] BYREF
  void *v151[2]; // [rsp+1E0h] [rbp-98h]
  void *v152[2]; // [rsp+1F0h] [rbp-88h]
  void *v153[2]; // [rsp+200h] [rbp-78h]
  __int64 v154; // [rsp+210h] [rbp-68h]
  __int64 n1024; // [rsp+218h] [rbp-60h]
  int v156; // [rsp+220h] [rbp-58h]
  __int64 v157; // [rsp+228h] [rbp-50h]
  char p_MultiByteStr[72]; // [rsp+230h] [rbp-48h] BYREF
  unsigned __int64 v159; // [rsp+280h] [rbp+8h] BYREF
  _QWORD *v160; // [rsp+288h] [rbp+10h]
  unsigned int *v161; // [rsp+290h] [rbp+18h]
  unsigned __int64 i_1; // [rsp+298h] [rbp+20h]

  v159 = (unsigned __int64)a1;
  v3 = a1;
  LODWORD(v160) = 0;
  v4 = a1[1];
  v5 = *a1;
  if ( *a1 != v4 )
  {
    do
    {
      sub_180010330(v5);
      v5 += 80;
    }
    while ( v5 != v4 );
    v5 = *v3;
  }
  v3[1] = v5;
  *((_BYTE *)v3 + 152) = 0;
  *((_DWORD *)v3 + 39) = 0;
  sub_180002E70(v3 + 20, &unk_18004B9FB, 0);
  *((_DWORD *)v3 + 48) = 0;
  sub_180002E70(v3 + 25, &unk_18004B9FB, 0);
  v150 = 0;
  HIWORD(v150) = 0;
  *(_OWORD *)v152 = 0;
  *(_OWORD *)v153 = 0;
  v154 = 0;
  n1024 = 1024;
  v156 = 0;
  v157 = 0;
  v6 = operator new(0x28u);
  v160 = v6;
  *v6 = 0;
  v6[1] = 0x10000;
  v6[2] = 0;
  v6[3] = 0;
  v6[4] = 0;
  v151[0] = v6;
  v151[1] = v6;
  if ( MultiByteStr_1[3] >= 0x10u )
    MultiByteStr_1 = (_QWORD *)*MultiByteStr_1;
  MultiByteStr = MultiByteStr_1;
  MultiByteStr_2 = MultiByteStr_1;
  if ( *(_DWORD *)(sub_1800043F0(&v150, &MultiByteStr) + 80) || HIWORD(v150) != 3 )
  {
    *(__m128i *)lpMultiByteStr = _mm_load_si128((const __m128i *)&xmmword_18004E0B0);
    LOWORD(v125) = -13420;
    k_7[0] = 0;
    n15_2 = 0;
    n15 = 15;
    LODWORD(v160) = 2;
    k_12 = operator new(0x20u);
    n15 = 31;
    *k_12 = k_7[0];
    k_7[0] = k_12;
    n15_1 = 0;
    n15_2 = 0;
    v105 = 0;
    while ( 1 )
    {
      v106 = *((_BYTE *)lpMultiByteStr + v105)
           ^ (-41 - 125 * v105)
           ^ (0x5F758FC1u >> (8 * (v105 & 3)))
           ^ (0xACCE3EDD >> (8 * (((unsigned __int8)lpMultiByteStr + v105 + 1LL - (_QWORD)lpMultiByteStr) & 3)));
      if ( n15_1 >= n15 )
      {
        sub_180009740(k_7);
      }
      else
      {
        n15_2 = n15_1 + 1;
        k_11 = k_7;
        if ( n15 >= 0x10 )
          k_11 = (void **)k_12;
        *((_BYTE *)k_11 + n15_1) = v106;
        *((_BYTE *)k_11 + n15_1 + 1) = 0;
      }
      if ( (unsigned __int64)++v105 >= 0x11 )
        break;
      n15_1 = n15_2;
      k_12 = k_7[0];
    }
    Block[0] = &unk_18004B9FB;
    LODWORD(Block[1]) = 0;
    *(_QWORD *)&v121 = &unk_18004B9FB;
    if ( *(_QWORD *)&xmmword_180058068 )
    {
      k_13 = k_7;
      if ( n15 >= 0x10 )
        k_13 = (void **)k_7[0];
      *(_QWORD *)&v140 = k_13;
      *((_QWORD *)&v140 + 1) = n15_2;
      *(_OWORD *)lpMultiByteStr = v140;
      *(_OWORD *)p_C = *(_OWORD *)Block;
      v129 = v121;
      sub_18001A5A0(*(_QWORD *)&xmmword_180058068, p_C, 2, lpMultiByteStr);
    }
    if ( n15 >= 0x10 )
    {
      v109 = k_7[0];
      if ( n15 + 1 >= 0x1000 )
      {
        v109 = (void *)*((_QWORD *)k_7[0] - 1);
        if ( (unsigned __int64)((char *)k_7[0] - (char *)v109 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v109);
    }
    v110 = v151[1];
    if ( v151[1] )
    {
      if ( *(_QWORD *)v151[1] )
      {
        v111 = *(_QWORD **)v151[1];
        do
        {
          v112 = v111;
          if ( v111 == *((_QWORD **)v110 + 2) )
            break;
          v111 = (_QWORD *)v111[2];
          free(v112);
          *(_QWORD *)v110 = v111;
        }
        while ( v111 );
      }
      v113 = *(_QWORD **)v110;
      if ( *(_QWORD *)v110 && v113 == *((_QWORD **)v110 + 2) )
        v113[1] = 0;
      j_j_free(*((void **)v110 + 4));
      j_j_free(v110);
    }
    free(v153[0]);
    j_j_free(v152[1]);
  }
  else
  {
    MultiByteStr = (void *)0x263261618660B12LL;
    LOWORD(MultiByteStr_2) = 2052;
    BYTE2(MultiByteStr_2) = 27;
    k_16[0] = 0;
    n15_4 = 0;
    n15_15 = 0;
    n15_3 = 15;
    LODWORD(v160) = 8;
    n0xA = 0;
    try
    {
      while ( n0xA < 0xA )
      {
        n0xA_1 = n0xA + 1;
        v10 = *((_BYTE *)&MultiByteStr + n0xA)
            ^ (96 - 125 * n0xA)
            ^ (0x25965411u >> (8 * ((n0xA + 1) & 3)))
            ^ (0x83401645 >> (8 * (n0xA & 3)));
        if ( n15_4 >= n15_3 )
        {
          sub_180009740(k_16);
          n0xA = n0xA_1;
          n15_4 = n15_15;
        }
        else
        {
          n15_15 = n15_4 + 1;
          k_17 = k_16;
          if ( n15_3 >= 0x10 )
            k_17 = (void **)k_16[0];
          *((_BYTE *)k_17 + n15_4) = v10;
          *((_BYTE *)k_17 + n15_4 + 1) = 0;
          ++n0xA;
          n15_4 = n15_15;
        }
      }
      LODWORD(v161) = 180827616;
      BYTE4(v161) = 114;
      v145[0] = 0;
      n15_7 = 0;
      n15_16 = 0;
      n15_5 = 15;
      n15_6 = 15;
      LODWORD(v160) = 36;
      n4 = 0;
      while ( n4 < 4 )
      {
        n4_1 = n4 + 1;
        v16 = *((_BYTE *)&v161 + n4)
            ^ (-6 - 125 * n4)
            ^ (0x17CE0AD4u >> (8 * ((n4 + 1) & 3)))
            ^ (0x38BDE77Eu >> (8 * (n4 & 3)));
        if ( n15_7 >= n15_5 )
        {
          sub_180009740(v145);
          n4 = n4_1;
          n15_5 = n15_6;
          n15_7 = n15_16;
        }
        else
        {
          n15_16 = n15_7 + 1;
          v17 = v145;
          if ( n15_5 >= 0x10 )
            v17 = (void **)v145[0];
          *((_BYTE *)v17 + n15_7) = v16;
          *((_BYTE *)v17 + n15_7 + 1) = 0;
          ++n4;
          n15_5 = n15_6;
          n15_7 = n15_16;
        }
      }
      MultiByteStr = (void *)0xA803D8D050CB39C2LL;
      LODWORD(MultiByteStr_2) = -583875289;
      k_7[0] = 0;
      n15_9 = 0;
      n15_2 = 0;
      n15_8 = 15;
      n15 = 15;
      LODWORD(v160) = 148;
      n0xB = 0;
      while ( n0xB < 0xB )
      {
        n0xB_1 = n0xB + 1;
        v22 = *((_BYTE *)&MultiByteStr + n0xB)
            ^ (113 - 125 * n0xB)
            ^ (0xD0EEF55C >> (8 * ((n0xB + 1) & 3)))
            ^ (0x931E4236 >> (8 * (n0xB & 3)));
        if ( n15_9 >= n15_8 )
        {
          sub_180009740(k_7);
          n0xB = n0xB_1;
          n15_8 = n15;
          n15_9 = n15_2;
        }
        else
        {
          n15_2 = n15_9 + 1;
          k_14 = k_7;
          if ( n15_8 >= 0x10 )
            k_14 = (void **)k_7[0];
          *((_BYTE *)k_14 + n15_9) = v22;
          *((_BYTE *)k_14 + n15_9 + 1) = 0;
          ++n0xB;
          n15_8 = n15;
          n15_9 = n15_2;
        }
      }
      MultiByteStr = (void *)0xAA6B8FABA36980A4LL;
      LODWORD(MultiByteStr_2) = -1621385293;
      BYTE4(MultiByteStr_2) = -26;
      k_3[0] = 0;
      n15_12 = 0;
      n15_17 = 0;
      n15_10 = 15;
      n15_11 = 15;
      LODWORD(v160) = 596;
      n0xC = 0;
      while ( n0xC < 0xC )
      {
        n0xC_1 = n0xC + 1;
        v28 = *((_BYTE *)&MultiByteStr + n0xC)
            ^ (-126 - 125 * n0xC)
            ^ (0x4F97B0D6u >> (8 * ((n0xC + 1) & 3)))
            ^ (0x19C07BF0u >> (8 * (n0xC & 3)));
        if ( n15_12 >= n15_10 )
        {
          sub_180009740(k_3);
          n0xC = n0xC_1;
          n15_10 = n15_11;
          n15_12 = n15_17;
        }
        else
        {
          n15_17 = n15_12 + 1;
          k_4 = k_3;
          if ( n15_10 >= 0x10 )
            k_4 = (void **)k_3[0];
          *((_BYTE *)k_4 + n15_12) = v28;
          *((_BYTE *)k_4 + n15_12 + 1) = 0;
          ++n0xC;
          n15_10 = n15_11;
          n15_12 = n15_17;
        }
      }
      n340 = 340;
      k_15 = k_16;
      if ( n15_3 >= 0x10 )
        k_15 = (void **)k_16[0];
      v32 = (unsigned int *)sub_180017D50(&v150, k_15);
      v161 = v32;
      for ( i = 0; ; i = i_1 + 1 )
      {
        i_1 = i;
        if ( i >= *v32 )
          break;
        v132[0] = 0;
        *(_QWORD *)&v133 = 0;
        *((_QWORD *)&v133 + 1) = 15;
        v134 = 0;
        v135 = 0;
        v136 = 0;
        Src[0] = 0;
        *((_QWORD *)&v123 + 1) = 15;
        *(_QWORD *)&v123 = 0;
        i_2 = (unsigned int)i;
        v35 = 16LL * (unsigned int)i;
        v36 = sub_180002660(Block);
        if ( v132 != (void **)v36 )
        {
          if ( *((_QWORD *)&v133 + 1) >= 0x10u )
          {
            v37 = v132[0];
            if ( (unsigned __int64)(*((_QWORD *)&v133 + 1) + 1LL) >= 0x1000 )
            {
              v37 = (void *)*((_QWORD *)v132[0] - 1);
              if ( (unsigned __int64)((char *)v132[0] - (char *)v37 - 8) > 0x1F )
LABEL_49:
                invalid_parameter_noinfo_noreturn();
            }
            j_j_free(v37);
          }
          *(_QWORD *)&v133 = 0;
          *((_QWORD *)&v133 + 1) = 15;
          LOBYTE(v132[0]) = 0;
          *(_OWORD *)v132 = *(_OWORD *)v36;
          v133 = *(_OWORD *)(v36 + 16);
          *(_QWORD *)(v36 + 16) = 0;
          *(_QWORD *)(v36 + 24) = 15;
          *(_BYTE *)v36 = 0;
        }
        if ( *((_QWORD *)&v121 + 1) >= 0x10u )
        {
          v38 = Block[0];
          if ( (unsigned __int64)(*((_QWORD *)&v121 + 1) + 1LL) >= 0x1000 )
          {
            v38 = (void *)*((_QWORD *)Block[0] - 1);
            if ( (unsigned __int64)((char *)Block[0] - (char *)v38 - 8) > 0x1F )
              goto LABEL_49;
          }
          j_j_free(v38);
        }
        *(_QWORD *)&v121 = 0;
        *((_QWORD *)&v121 + 1) = 15;
        LOBYTE(Block[0]) = 0;
        if ( *((_QWORD *)&v123 + 1) >= 0x10u )
        {
          v39 = Src[0];
          if ( (unsigned __int64)(*((_QWORD *)&v123 + 1) + 1LL) >= 0x1000 )
          {
            v39 = (void *)*((_QWORD *)Src[0] - 1);
            if ( (unsigned __int64)((char *)Src[0] - (char *)v39 - 8) > 0x1F )
              invalid_parameter_noinfo_noreturn();
          }
          j_j_free(v39);
        }
        k_6 = k_7;
        if ( n15 >= 0x10 )
          k_6 = (void **)k_7[0];
        v40 = (unsigned int *)(v35 + (*((_QWORD *)v32 + 1) & 0xFFFFFFFFFFFFLL));
        if ( *(_QWORD *)sub_180002A80(v40, p_MultiByteStr, k_6) != (*((_QWORD *)v40 + 1) & 0xFFFFFFFFFFFFLL)
                                                                 + 32LL * *v40 )
        {
          k_8 = k_7;
          if ( n15 >= 0x10 )
            k_8 = (void **)k_7[0];
          v43 = (unsigned int *)sub_180017D50(v35 + (*((_QWORD *)v32 + 1) & 0xFFFFFFFFFFFFLL), k_8);
          for ( j = 0; j < *v43; ++j )
          {
            v45 = (_WORD *)((*((_QWORD *)v43 + 1) & 0xFFFFFFFFFFFFLL) + 16LL * (unsigned int)j);
            if ( (v45[7] & 0x1000) == 0 )
              v45 = (_WORD *)(*(_QWORD *)((*((_QWORD *)v43 + 1) & 0xFFFFFFFFFFFFLL) + 16LL * (unsigned int)j + 8)
                            & 0xFFFFFFFFFFFFLL);
            Src[0] = 0;
            *(_QWORD *)&v123 = 0;
            *((_QWORD *)&v123 + 1) = 15;
            Size = -1;
            do
              ++Size;
            while ( *((_BYTE *)v45 + Size) );
            sub_180002E70(Src, v45, Size);
            sub_180002CD0(Block);
            n340 |= 0x400u;
            v47 = Block;
            if ( *((_QWORD *)&v121 + 1) < 0x10u )
            {
              v49 = (void **)((char *)Block + v121);
              v48 = Block;
            }
            else
            {
              v48 = (void **)Block[0];
              v49 = (void **)((char *)Block[0] + v121);
              v47 = (void **)Block[0];
            }
            if ( v48 != v49 )
            {
              v50 = (char *)v47 - (char *)v48;
              do
              {
                *((_BYTE *)v48 + v50) = tolower(*(unsigned __int8 *)v48);
                v48 = (void **)((char *)v48 + 1);
              }
              while ( v48 != v49 );
            }
            v51 = *((_QWORD *)&v134 + 1);
            if ( *((_QWORD *)&v134 + 1) == (_QWORD)v135 )
            {
              sub_180017E00(&v134, *((_QWORD *)&v134 + 1), Block);
              n15_13 = *((_QWORD *)&v121 + 1);
            }
            else
            {
              **((_QWORD **)&v134 + 1) = 0;
              *(_QWORD *)(v51 + 16) = 0;
              *(_QWORD *)(v51 + 24) = 0;
              *(_OWORD *)v51 = *(_OWORD *)Block;
              *(_OWORD *)(v51 + 16) = v121;
              *(_QWORD *)&v121 = 0;
              n15_13 = 15;
              *((_QWORD *)&v121 + 1) = 15;
              LOBYTE(Block[0]) = 0;
              *((_QWORD *)&v134 + 1) += 32LL;
            }
            if ( n15_13 >= 0x10 )
            {
              v53 = Block[0];
              if ( n15_13 + 1 >= 0x1000 )
              {
                v53 = (void *)*((_QWORD *)Block[0] - 1);
                if ( (unsigned __int64)((char *)Block[0] - (char *)v53 - 8) > 0x1F )
                  invalid_parameter_noinfo_noreturn();
              }
              j_j_free(v53);
            }
            *(_QWORD *)&v121 = 0;
            *((_QWORD *)&v121 + 1) = 15;
            LOBYTE(Block[0]) = 0;
            if ( *((_QWORD *)&v123 + 1) >= 0x10u )
            {
              v54 = Src[0];
              if ( (unsigned __int64)(*((_QWORD *)&v123 + 1) + 1LL) >= 0x1000 )
              {
                v54 = (void *)*((_QWORD *)Src[0] - 1);
                if ( (unsigned __int64)((char *)Src[0] - (char *)v54 - 8) > 0x1F )
                  invalid_parameter_noinfo_noreturn();
              }
              j_j_free(v54);
            }
          }
          v32 = v161;
        }
        v55 = 16 * i_2;
        k_5 = k_3;
        if ( n15_11 >= 0x10 )
          k_5 = (void **)k_3[0];
        v56 = (unsigned int *)(v55 + (*((_QWORD *)v32 + 1) & 0xFFFFFFFFFFFFLL));
        if ( *(_QWORD *)sub_180002A80(v56, &MultiByteStr, k_5) != (*((_QWORD *)v56 + 1) & 0xFFFFFFFFFFFFLL)
                                                                + 32LL * *v56 )
        {
          v58 = (unsigned int *)(v55 + (*((_QWORD *)v32 + 1) & 0xFFFFFFFFFFFFLL));
          k_1 = k_3;
          if ( n15_11 >= 0x10 )
            k_1 = (void **)k_3[0];
          k_2 = k_1;
          for ( k = (int)k_1; *k_2; k = (int)k_2 )
            ++k_2;
          v144 = 0;
          HIWORD(v144) = 1029;
          *((_QWORD *)&v144 + 1) = (unsigned __int64)k_1 | *((_QWORD *)&v144 + 1) & 0xFFFF000000000000uLL;
          LODWORD(v144) = k - (_DWORD)k_1;
          sub_1800038A0(v58, &v148, &v144);
          if ( v148 == (*((_QWORD *)v58 + 1) & 0xFFFFFFFFFFFFLL) + 32LL * *v58 )
          {
            xmmword_180058A80 = 0;
            v62 = &xmmword_180058A80;
          }
          else
          {
            v62 = (__int128 *)(v148 + 16);
          }
          for ( m = 0; m < *(unsigned int *)v62; ++m )
          {
            v64 = (_WORD *)((*((_QWORD *)v62 + 1) & 0xFFFFFFFFFFFFLL) + 16LL * (unsigned int)m);
            if ( (v64[7] & 0x1000) == 0 )
              v64 = (_WORD *)(*(_QWORD *)((*((_QWORD *)v62 + 1) & 0xFFFFFFFFFFFFLL) + 16LL * (unsigned int)m + 8)
                            & 0xFFFFFFFFFFFFLL);
            Block[0] = 0;
            *(_QWORD *)&v121 = 0;
            *((_QWORD *)&v121 + 1) = 15;
            Size_1 = -1;
            do
              ++Size_1;
            while ( *((_BYTE *)v64 + Size_1) );
            sub_180002E70(Block, v64, Size_1);
            sub_180002CD0(Src);
            n340 |= 0x800u;
            Src_1 = Src;
            if ( *((_QWORD *)&v123 + 1) < 0x10u )
            {
              Src_3 = (void **)((char *)Src + v123);
              Src_2 = Src;
            }
            else
            {
              Src_2 = (void **)Src[0];
              Src_3 = (void **)((char *)Src[0] + v123);
              Src_1 = (void **)Src[0];
            }
            if ( Src_2 != Src_3 )
            {
              v69 = (char *)Src_1 - (char *)Src_2;
              do
              {
                *((_BYTE *)Src_2 + v69) = tolower(*(unsigned __int8 *)Src_2);
                Src_2 = (void **)((char *)Src_2 + 1);
              }
              while ( Src_2 != Src_3 );
            }
            v70 = v136;
            if ( (_QWORD)v136 == *((_QWORD *)&v136 + 1) )
            {
              sub_180017E00((char *)&v135 + 8, v136, Src);
            }
            else
            {
              *(_QWORD *)v136 = 0;
              *(_QWORD *)(v70 + 16) = 0;
              *(_QWORD *)(v70 + 24) = 0;
              *(_OWORD *)v70 = *(_OWORD *)Src;
              *(_OWORD *)(v70 + 16) = v123;
              *(_QWORD *)&v123 = 0;
              *((_QWORD *)&v123 + 1) = 15;
              LOBYTE(Src[0]) = 0;
              *(_QWORD *)&v136 = v136 + 32;
            }
            sub_180002BA0(Src);
            if ( *((_QWORD *)&v121 + 1) >= 0x10u )
            {
              v71 = Block[0];
              if ( (unsigned __int64)(*((_QWORD *)&v121 + 1) + 1LL) >= 0x1000 )
              {
                v71 = (void *)*((_QWORD *)Block[0] - 1);
                if ( (unsigned __int64)((char *)Block[0] - (char *)v71 - 8) > 0x1F )
                  invalid_parameter_noinfo_noreturn();
              }
              j_j_free(v71);
            }
          }
          v32 = v161;
        }
        v149 = (__int64)(v136 - *((_QWORD *)&v135 + 1)) >> 5;
        *(_QWORD *)&v144 = (__int64)(*((_QWORD *)&v134 + 1) - v134) >> 5;
        *(__m128i *)lpMultiByteStr = _mm_load_si128((const __m128i *)&xmmword_18004E180);
        v125 = -179452448;
        v126 = -1992707594;
        v127 = -1321878142;
        Src[0] = 0;
        *(_QWORD *)&v123 = 0;
        *((_QWORD *)&v123 + 1) = 15;
        v72 = n340 | 0x2000;
        LODWORD(v160) = v72;
        sub_1800095F0(Src);
        v73 = 0;
        *(_QWORD *)&v123 = 0;
        n0x1B = 0;
        while ( n0x1B < 0x1B )
        {
          n0x1B_1 = n0x1B + 1;
          v76 = *((_BYTE *)lpMultiByteStr + n0x1B)
              ^ (-127 - 125 * n0x1B)
              ^ (0x3B1E6332u >> (8 * (n0x1B & 3)))
              ^ (0x99E959D8 >> (8 * ((n0x1B + 1) & 3)));
          if ( v73 >= *((_QWORD *)&v123 + 1) )
          {
            sub_180009740(Src);
            n0x1B = n0x1B_1;
            v73 = v123;
          }
          else
          {
            *(_QWORD *)&v123 = v73 + 1;
            Src_4 = Src;
            if ( *((_QWORD *)&v123 + 1) >= 0x10u )
              Src_4 = (void **)Src[0];
            *((_BYTE *)Src_4 + v73) = v76;
            *((_BYTE *)Src_4 + v73 + 1) = 0;
            ++n0x1B;
            v73 = v123;
          }
        }
        n340 = v72 & 0xFFFFCFFF | 0x1000;
        Block[0] = &unk_18004B9FB;
        LODWORD(Block[1]) = 0;
        *(_QWORD *)&v121 = &unk_18004B9FB;
        if ( *(_QWORD *)&xmmword_180058068 )
        {
          Src_5 = Src;
          if ( *((_QWORD *)&v123 + 1) >= 0x10u )
            Src_5 = (void **)Src[0];
          *(_QWORD *)&v140 = Src_5;
          *((_QWORD *)&v140 + 1) = v73;
          *(_OWORD *)lpMultiByteStr = v140;
          *(_OWORD *)p_C = *(_OWORD *)Block;
          v129 = v121;
          sub_18001B370(
            xmmword_180058068,
            (unsigned int)p_C,
            n0x1B,
            (unsigned int)lpMultiByteStr,
            (__int64)v132,
            (__int64)&v144,
            (__int64)&v149);
        }
        if ( *((_QWORD *)&v123 + 1) >= 0x10u )
        {
          v79 = Src[0];
          if ( (unsigned __int64)(*((_QWORD *)&v123 + 1) + 1LL) >= 0x1000 )
          {
            v79 = (void *)*((_QWORD *)Src[0] - 1);
            if ( (unsigned __int64)((char *)Src[0] - (char *)v79 - 8) > 0x1F )
              invalid_parameter_noinfo_noreturn();
          }
          j_j_free(v79);
        }
        v3 = (_QWORD *)v159;
        v80 = *(_QWORD **)(v159 + 8);
        if ( v80 == *(_QWORD **)(v159 + 16) )
        {
          sub_180017F30(v159, *(_QWORD *)(v159 + 8), v132);
        }
        else
        {
          *v80 = 0;
          v80[2] = 0;
          v80[3] = 0;
          *(_OWORD *)v80 = *(_OWORD *)v132;
          *((_OWORD *)v80 + 1) = v133;
          *(_QWORD *)&v133 = 0;
          *((_QWORD *)&v133 + 1) = 15;
          LOBYTE(v132[0]) = 0;
          v81 = v135;
          *(_QWORD *)&v135 = 0;
          v82 = *((_QWORD *)&v134 + 1);
          v83 = v134;
          v134 = 0u;
          v80[4] = v83;
          v80[5] = v82;
          v80[6] = v81;
          v84 = *((_QWORD *)&v136 + 1);
          v85 = v136;
          v136 = 0u;
          v86 = *((_QWORD *)&v135 + 1);
          *((_QWORD *)&v135 + 1) = 0;
          v80[7] = v86;
          v80[8] = v85;
          v80[9] = v84;
          v3[1] += 80LL;
        }
        v87 = *((_QWORD *)&v135 + 1);
        if ( *((_QWORD *)&v135 + 1) )
        {
          v88 = v136;
          if ( *((_QWORD *)&v135 + 1) != (_QWORD)v136 )
          {
            do
            {
              sub_180004650(v87);
              v87 += 32;
            }
            while ( v87 != v88 );
            v87 = *((_QWORD *)&v135 + 1);
          }
          sub_180017530(v85, v87, (*((_QWORD *)&v136 + 1) - v87) >> 5);
          *((_QWORD *)&v135 + 1) = 0;
          v136 = 0;
        }
        v89 = v134;
        if ( (_QWORD)v134 )
        {
          v90 = *((_QWORD *)&v134 + 1);
          if ( (_QWORD)v134 != *((_QWORD *)&v134 + 1) )
          {
            do
            {
              sub_180004650(v89);
              v89 += 32;
            }
            while ( v89 != v90 );
            v89 = v134;
          }
          sub_180017530(v85, v89, ((__int64)v135 - v89) >> 5);
          v134 = 0;
          *(_QWORD *)&v135 = 0;
        }
        if ( *((_QWORD *)&v133 + 1) >= 0x10u )
        {
          v91 = v132[0];
          if ( (unsigned __int64)(*((_QWORD *)&v133 + 1) + 1LL) >= 0x1000 )
          {
            v91 = (void *)*((_QWORD *)v132[0] - 1);
            if ( (unsigned __int64)((char *)v132[0] - (char *)v91 - 8) > 0x1F )
              invalid_parameter_noinfo_noreturn();
          }
          j_j_free(v91);
        }
      }
      if ( n15_11 >= 0x10 )
      {
        v92 = k_3[0];
        if ( n15_11 + 1 >= 0x1000 )
        {
          v92 = (void *)*((_QWORD *)k_3[0] - 1);
          if ( (unsigned __int64)((char *)k_3[0] - (char *)v92 - 8) > 0x1F )
            invalid_parameter_noinfo_noreturn();
        }
        j_j_free(v92);
      }
      n15_17 = 0;
      n15_11 = 15;
      LOBYTE(k_3[0]) = 0;
      if ( n15 >= 0x10 )
      {
        v93 = k_7[0];
        if ( n15 + 1 >= 0x1000 )
        {
          v93 = (void *)*((_QWORD *)k_7[0] - 1);
          if ( (unsigned __int64)((char *)k_7[0] - (char *)v93 - 8) > 0x1F )
            invalid_parameter_noinfo_noreturn();
        }
        j_j_free(v93);
      }
      n15_2 = 0;
      n15 = 15;
      LOBYTE(k_7[0]) = 0;
      if ( n15_6 >= 0x10 )
      {
        v94 = v145[0];
        if ( n15_6 + 1 >= 0x1000 )
        {
          v94 = (void *)*((_QWORD *)v145[0] - 1);
          if ( (unsigned __int64)((char *)v145[0] - (char *)v94 - 8) > 0x1F )
            invalid_parameter_noinfo_noreturn();
        }
        j_j_free(v94);
      }
      n15_16 = 0;
      n15_6 = 15;
      LOBYTE(v145[0]) = 0;
      if ( n15_3 >= 0x10 )
      {
        v95 = k_16[0];
        if ( n15_3 + 1 >= 0x1000 )
        {
          v95 = (void *)*((_QWORD *)k_16[0] - 1);
          if ( (unsigned __int64)((char *)k_16[0] - (char *)v95 - 8) > 0x1F )
            invalid_parameter_noinfo_noreturn();
        }
        j_j_free(v95);
      }
    }
    catch ( ... )
    {
      v114 = sub_180010210(i, k_16);
      Block[0] = &unk_18004B9FB;
      LODWORD(Block[1]) = 0;
      *(_QWORD *)&v121 = &unk_18004B9FB;
      sub_1800181C0(Block, v114);
      sub_180002BA0(k_16);
      sub_180016CE0(v159);
      goto LABEL_194;
    }
    v159 = 0xCCCCCCCCCCCCCCCDuLL * ((__int64)(v3[1] - *v3) >> 4);
    *(__m128i *)p_C = _mm_load_si128((const __m128i *)&xmmword_18004E090);
    v129 = 0x24BB658A71EA788CLL;
    n30348 = 30348;
    v131 = -46;
    k_7[0] = 0;
    n15_2 = 0;
    n15 = 15;
    LODWORD(v160) = n340 | 0x8000;
    sub_1800095F0(k_7);
    n15_14 = 0;
    n15_2 = 0;
    v97 = 0;
    while ( 1 )
    {
      v98 = 0xDA0C0D54 >> (8 * (((unsigned __int8)p_C + v97 + 1LL - (_QWORD)p_C) & 3));
      LOBYTE(v98) = *((_BYTE *)p_C + v97) ^ (112 - 125 * v97) ^ (0x7FB63C42u >> (8 * (v97 & 3))) ^ v98;
      if ( n15_14 >= n15 )
      {
        sub_180009740(k_7);
      }
      else
      {
        n15_2 = n15_14 + 1;
        k_9 = k_7;
        if ( n15 >= 0x10 )
          k_9 = (void **)k_7[0];
        *((_BYTE *)k_9 + n15_14) = v98;
        *((_BYTE *)k_9 + n15_14 + 1) = 0;
      }
      if ( (unsigned __int64)++v97 >= 0x1A )
        break;
      n15_14 = n15_2;
    }
    Block[0] = &unk_18004B9FB;
    LODWORD(Block[1]) = 0;
    *(_QWORD *)&v121 = &unk_18004B9FB;
    if ( *(_QWORD *)&xmmword_180058068 )
    {
      k_10 = k_7;
      if ( n15 >= 0x10 )
        k_10 = (void **)k_7[0];
      *(_QWORD *)&v140 = k_10;
      *((_QWORD *)&v140 + 1) = n15_2;
      *(_OWORD *)lpMultiByteStr = v140;
      *(_OWORD *)p_C = *(_OWORD *)Block;
      v129 = v121;
      sub_18001B5D0(xmmword_180058068, (unsigned int)p_C, v98, (unsigned int)lpMultiByteStr, (__int64)&v159);
    }
    if ( n15 >= 0x10 )
    {
      v101 = k_7[0];
      if ( n15 + 1 >= 0x1000 )
      {
        v101 = (void *)*((_QWORD *)k_7[0] - 1);
        if ( (unsigned __int64)((char *)k_7[0] - (char *)v101 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v101);
    }
    if ( sub_180016CC0(v3) )
    {
      sub_1800027F0(&v150);
      return 1;
    }
LABEL_194:
    sub_1800027F0(&v150);
  }
  return 0;
}
