unsigned __int64 __fastcall sub_18009EEB0(__int64 a1, __int64 a2, __int64 a3)
{
  char v5; // di
  unsigned __int8 v6; // bp
  char v7; // si
  unsigned int v8; // r13d
  unsigned int v9; // r15d
  __m128 v10; // xmm12
  __int128 v11; // xmm6
  __m128 v12; // xmm13
  __m128 v13; // xmm14
  int v14; // xmm10_4
  __int64 v15; // rax
  __int64 v16; // r14
  __int64 v17; // rdi
  __int64 v18; // rax
  __int64 v19; // rbp
  __int64 v20; // rax
  unsigned int v21; // r14d
  unsigned int v22; // ebp
  __int64 v23; // rcx
  unsigned __int64 result; // rax
  __int64 v25; // rax
  __m128 v26; // xmm15
  __m128 v27; // xmm6
  __int64 v28; // rax
  __m128 v29; // xmm15
  __m128 v30; // xmm0
  float v31; // xmm1_4
  __m128i v32; // xmm1
  bool v33; // cl
  __m128 v34; // xmm1
  float v35; // xmm3_4
  float v36; // xmm3_4
  __m128i v37; // xmm1
  bool v38; // cl
  __int64 v39; // rsi
  __int64 v40; // rdx
  __int64 v41; // rdi
  __int64 v42; // r8
  unsigned int v43; // r14d
  unsigned int v44; // ebp
  __int64 v45; // rax
  __int64 v46; // r14
  __int64 v47; // rcx
  bool v48; // r14
  __int64 v49; // rsi
  double v50; // xmm0_8
  unsigned __int64 v51; // xmm15_8
  int v52; // xmm11_4
  unsigned __int64 v53; // xmm0_8
  __m128 v54; // xmm11
  __m128 v55; // xmm11
  int v56; // r8d
  int v57; // r9d
  __int64 v58; // r14
  __int32 v59; // eax
  unsigned __int64 v60; // xmm10_8
  int v61; // xmm11_4
  __m128 v62; // xmm6
  __m128 v63; // xmm15
  __int128 *v64; // r8
  __m128i v65; // xmm10
  __m128i v66; // xmm0
  __m128i v67; // xmm10
  char *v68; // rdx
  __int128 v69; // xmm8
  __int128 v70; // xmm9
  int n3; // esi
  __int64 v72; // rax
  __int64 v73; // rcx
  bool v74; // si
  __int128 v75; // xmm7
  __int128 v76; // xmm8
  __int64 v77; // rcx
  __m128 v78; // xmm0
  float v79; // xmm7_4
  __m128 v80; // xmm8
  __m128 v81; // xmm0
  __m128 v82; // xmm1
  __m128i v83; // xmm1
  float v84; // xmm7_4
  float v85; // xmm8_4
  __m128 v86; // xmm0
  __m128 v87; // xmm9
  float v88; // xmm8_4
  __m128 v89; // xmm0
  __m128 v90; // xmm6
  __m128 v91; // xmm6
  __m128d v92; // xmm0
  int v93; // r9d
  int v94; // xmm15_4
  float v95; // xmm1_4
  __m128 v96; // xmm1
  __int64 v97; // rax
  __int128 v98; // xmm6
  unsigned __int64 v99; // xmm0_8
  __m128 v100; // xmm7
  __m128 v101; // xmm0
  __int64 v102; // rax
  __m128 v103; // xmm1
  float v104; // xmm8_4
  __m128 v105; // xmm9
  __m128 v106; // xmm0
  __m128 v107; // xmm9
  __int64 v108; // rdx
  __int64 v109; // rcx
  __int64 v110; // r8
  _BYTE v111[32]; // [rsp+0h] [rbp-528h] BYREF
  char v112; // [rsp+2Eh] [rbp-4FAh]
  unsigned __int8 v113; // [rsp+2Fh] [rbp-4F9h]
  __int64 v114; // [rsp+30h] [rbp-4F8h]
  char v115; // [rsp+3Fh] [rbp-4E9h]
  __int64 v116; // [rsp+40h] [rbp-4E8h]
  __int64 v117; // [rsp+48h] [rbp-4E0h]
  int v118; // [rsp+54h] [rbp-4D4h]
  float v119; // [rsp+58h] [rbp-4D0h]
  int v120; // [rsp+5Ch] [rbp-4CCh]
  __int64 v121; // [rsp+60h] [rbp-4C8h]
  float v122; // [rsp+68h] [rbp-4C0h]
  int v123; // [rsp+6Ch] [rbp-4BCh]
  __m128 v124; // [rsp+70h] [rbp-4B8h]
  __int64 v125; // [rsp+80h] [rbp-4A8h]
  int v126; // [rsp+88h] [rbp-4A0h]
  int v127; // [rsp+8Ch] [rbp-49Ch]
  __m128 v128; // [rsp+90h] [rbp-498h]
  __m128 v129; // [rsp+A0h] [rbp-488h]
  __m128 v130; // [rsp+B0h] [rbp-478h]
  __int64 v131; // [rsp+C8h] [rbp-460h]
  __m128 v132; // [rsp+D0h] [rbp-458h]
  __int64 v133; // [rsp+E8h] [rbp-440h]
  __m128i v134; // [rsp+F0h] [rbp-438h]
  __int128 v135; // [rsp+100h] [rbp-428h]
  __m128 v136; // [rsp+110h] [rbp-418h] BYREF
  __m128 v137; // [rsp+120h] [rbp-408h] BYREF
  __int128 v138; // [rsp+130h] [rbp-3F8h] BYREF
  __int128 v139; // [rsp+140h] [rbp-3E8h] BYREF
  __int128 v140; // [rsp+150h] [rbp-3D8h] BYREF
  __int128 v141; // [rsp+160h] [rbp-3C8h] BYREF
  __int128 v142; // [rsp+170h] [rbp-3B8h] BYREF
  __int128 v143; // [rsp+180h] [rbp-3A8h] BYREF
  __int128 v144; // [rsp+190h] [rbp-398h] BYREF
  __int128 v145; // [rsp+1A0h] [rbp-388h] BYREF
  __int128 v146; // [rsp+1B0h] [rbp-378h] BYREF
  __m128 v147; // [rsp+1C0h] [rbp-368h] BYREF
  __m128 v148; // [rsp+1D0h] [rbp-358h] BYREF
  __m128 v149; // [rsp+1E0h] [rbp-348h] BYREF
  _BYTE v150[16]; // [rsp+1F0h] [rbp-338h] BYREF
  __int64 v151; // [rsp+200h] [rbp-328h] BYREF
  int n1065353216_1; // [rsp+208h] [rbp-320h]
  __int64 v153; // [rsp+210h] [rbp-318h] BYREF
  int v154; // [rsp+218h] [rbp-310h]
  unsigned __int64 v155; // [rsp+220h] [rbp-308h] BYREF
  unsigned int v156; // [rsp+228h] [rbp-300h]
  __int64 v157; // [rsp+230h] [rbp-2F8h] BYREF
  int n1065353216; // [rsp+238h] [rbp-2F0h]
  unsigned __int64 v159; // [rsp+240h] [rbp-2E8h] BYREF
  unsigned int v160; // [rsp+248h] [rbp-2E0h]
  __int64 v161; // [rsp+250h] [rbp-2D8h] BYREF
  int v162; // [rsp+258h] [rbp-2D0h]
  unsigned __int64 v163; // [rsp+260h] [rbp-2C8h] BYREF
  unsigned int v164; // [rsp+268h] [rbp-2C0h]
  __int32 v165; // [rsp+270h] [rbp-2B8h] BYREF
  unsigned __int64 v166; // [rsp+274h] [rbp-2B4h]
  int v167; // [rsp+27Ch] [rbp-2ACh]
  __int128 v168; // [rsp+280h] [rbp-2A8h] BYREF
  __m128 v169; // [rsp+290h] [rbp-298h] BYREF
  __int32 v170; // [rsp+2A0h] [rbp-288h] BYREF
  __int64 v171; // [rsp+2A4h] [rbp-284h]
  int v172; // [rsp+2ACh] [rbp-27Ch]
  _BYTE v173[16]; // [rsp+2B0h] [rbp-278h] BYREF
  float v174[4]; // [rsp+2C0h] [rbp-268h] BYREF
  __int128 v175; // [rsp+2D0h] [rbp-258h] BYREF
  __int64 v176; // [rsp+2E0h] [rbp-248h] BYREF
  float v177; // [rsp+2E8h] [rbp-240h]
  __int64 v178; // [rsp+2F0h] [rbp-238h] BYREF
  int v179; // [rsp+2F8h] [rbp-230h]
  __int64 v180; // [rsp+300h] [rbp-228h] BYREF
  int v181; // [rsp+308h] [rbp-220h]
  _QWORD v182[4]; // [rsp+310h] [rbp-218h] BYREF
  int v183; // [rsp+330h] [rbp-1F8h]
  int n1022739087; // [rsp+338h] [rbp-1F0h]
  float v185; // [rsp+340h] [rbp-1E8h]
  __m128 v186; // [rsp+390h] [rbp-198h]
  unsigned __int64 v187; // [rsp+3A0h] [rbp-188h]
  int v188; // [rsp+3A8h] [rbp-180h]
  __int128 v189; // [rsp+3C8h] [rbp-160h]
  __int64 v190; // [rsp+438h] [rbp-F0h]

  v121 = a3;
  AcquireSRWLockShared(&SRWLock);
  v5 = byte_18026F49C;
  v6 = dword_18026F49D;
  v7 = BYTE1(dword_18026F49D);
  v8 = BYTE2(dword_18026F49D);
  v113 = HIBYTE(dword_18026F49D);
  v9 = (unsigned __int8)byte_18026F4A1;
  v10 = (__m128)(unsigned __int64)qword_18026F4A4;
  v11 = (unsigned int)dword_18026F4AC;
  v120 = dword_18026F4B8;
  v127 = dword_18026F4BC;
  v12 = (__m128)(unsigned int)dword_18026F4C4;
  v13 = (__m128)(unsigned int)dword_18026F4C8;
  v14 = HIDWORD(qword_18026F4CC);
  v123 = dword_18026F4D4;
  v115 = byte_18026F4D8;
  v112 = byte_18026F4E0;
  ReleaseSRWLockShared(&SRWLock);
  if ( v5 != 1
    || (byte_18026ECEC & 1) != 0
    || (*(_QWORD *)&v175 = 0, qword_18026F580(qword_18026F5D8, &v175), !(_QWORD)v175)
    || !qword_18026F5F0
    || (v182[0] = 0, (v15 = qword_18026F348(qword_18026F5F0, v175, 0, v182)) == 0)
    || v182[0] )
  {
    sub_1800A0DC0();
    goto LABEL_15;
  }
  v16 = v175;
  v17 = v15;
  v18 = qword_18026F588(v15);
  if ( (v18 != qword_18026F5C0 || (v7 & 1) == 0)
    && (v6 & (v18 == qword_18026F5C8 || v18 == qword_18026F5D0)) == 0
    && (((unsigned __int8)v112 & v8 & v6 & 1) == 0 || !(unsigned __int8)sub_1800A0F90(v17)) )
  {
    sub_1800A0DC0();
    if ( v16 )
    {
      if ( qword_18026F5F0 )
      {
        v182[0] = 0;
        v25 = qword_18026F348(qword_18026F5F0, v16, 0, v182);
        if ( v25 )
        {
          if ( !v182[0]
            && ((unsigned __int8)v8 & (qword_18026F588(v25) == qword_18026F678) & (unsigned __int8)v112) != 0 )
          {
LABEL_24:
            v180 = 0;
            v181 = 0;
            sub_1800A73F0(0, &v180);
            sub_1800A9FB0(0);
            if ( qword_18026F9D0 )
            {
              sub_18009EC40();
              if ( dword_18026F9DC )
                qword_18026EC40((unsigned int)dword_18026F9DC);
              if ( dword_18026FA7C )
                qword_18026EC40((unsigned int)dword_18026FA7C);
              dword_18026FA7C = 0;
              dword_18026F9DC = 0;
              dword_180266674 = -1;
              byte_18026FA80 = 0;
              byte_18026FA81 = 0;
              byte_18026FA82 = 0;
              xmmword_180266680 = xmmword_18021D9B0;
              qword_18026FA88 = 0;
              dword_180266690 = -1;
              byte_18026FA90 = 0;
              if ( (_DWORD)qword_18026F9D0 )
                qword_18026EC40((unsigned int)qword_18026F9D0);
              if ( HIDWORD(qword_18026F9D0) )
                qword_18026EC40(HIDWORD(qword_18026F9D0));
              qword_18026F9D0 = 0;
              dword_18026FA78 = 0;
            }
            byte_18026F48C = 0;
            n3 = 0;
LABEL_35:
            v23 = a1;
LABEL_36:
            qword_18026F660(v23, a2, v121);
            goto LABEL_37;
          }
        }
      }
    }
LABEL_15:
    v21 = dword_18026F9F0;
    v22 = dword_18026F9F4;
    if ( dword_18026F9EC )
      qword_18026EC40((unsigned int)dword_18026F9EC);
    if ( v21 )
      qword_18026EC40(v21);
    if ( v22 )
      qword_18026EC40(v22);
    dword_18026F9F4 = 0;
    dword_18026F9F0 = 0;
    dword_18026F9EC = 0;
    byte_18026F9D8 = 0;
    byte_18026F9D9 = 0;
    byte_18026F9DA = 0;
    if ( dword_18026F9E8 )
      qword_18026EC40((unsigned int)dword_18026F9E8);
    dword_18026F9E8 = 0;
    goto LABEL_24;
  }
  sub_1800A0DC0();
  v19 = a1;
  if ( !a2 )
    goto LABEL_49;
  if ( qword_18026F5F8 )
  {
    v182[0] = 0;
    v20 = qword_18026F348(qword_18026F5F8, v16, 0, v182);
    if ( v182[0] )
      v20 = 0;
  }
  else
  {
    v20 = 0;
  }
  if ( v20 != a1 )
  {
LABEL_49:
    v23 = a1;
    goto LABEL_36;
  }
  v135 = v11;
  sub_180200FC0(v182, a2, 288);
  v124 = (__m128)*(unsigned int *)(a2 + 128);
  v26 = (__m128)*(unsigned __int64 *)(a2 + 132);
  v27 = (__m128)*(unsigned __int64 *)(a2 + 172);
  v134 = (__m128i)_mm_shuffle_ps(v27, v27, 85);
  v119 = COERCE_FLOAT(_mm_cvtsi128_si32(_mm_cvtsi32_si128(*(_DWORD *)(a2 + 180))));
  v130 = *(__m128 *)(a2 + 140);
  v129 = *(__m128 *)(a2 + 184);
  v117 = v17;
  v28 = qword_18026F588(v17);
  v132 = v27;
  v128 = v26;
  v29 = _mm_shuffle_ps(v26, _mm_shuffle_ps(v27, v26, 16), 32);
  v29.m128_f32[0] = v124.m128_f32[0];
  if ( ((_mm_movemask_ps((__m128)_mm_cmpgt_epi32(
                                   (__m128i)_mm_and_ps(v29, (__m128)xmmword_18021D900),
                                   (__m128i)xmmword_18021DBC0)) == 0)
      & ((_mm_cvtsi128_si32(v134) & 0x7FFFFFFFu) < 0x7F800000 && (LODWORD(v119) & 0x7FFFFFFFu) < 0x7F800000)) != 1 )
    goto LABEL_62;
  v30 = _mm_mul_ps(v130, v130);
  v31 = (float)(v130.m128_f32[0] * v130.m128_f32[0]) + _mm_shuffle_ps(v30, v30, 85).m128_f32[0];
  v30.m128_f32[0] = *(float *)_mm_unpackhi_pd((__m128d)v130, (__m128d)v130).m128d_f64;
  v30.m128_f32[0] = (float)(v30.m128_f32[0] * v30.m128_f32[0]) + v31;
  v32 = (__m128i)_mm_shuffle_ps(v130, v130, 255);
  *(float *)v32.m128i_i32 = (float)(*(float *)v32.m128i_i32 * *(float *)v32.m128i_i32) + v30.m128_f32[0];
  v33 = (_mm_cvtsi128_si32(v32) & 0x7FFFFFFFu) < 0x7F800000;
  if ( *(float *)v32.m128i_i32 >= 1.02
    || *(float *)v32.m128i_i32 <= 0.98000002
    || !v33
    || (v34 = _mm_mul_ps(v129, v129),
        v35 = *(float *)_mm_unpackhi_pd((__m128d)v129, (__m128d)v129).m128d_f64,
        v36 = (float)(v35 * v35)
            + (float)((float)(v129.m128_f32[0] * v129.m128_f32[0]) + _mm_shuffle_ps(v34, v34, 85).m128_f32[0]),
        v37 = (__m128i)_mm_shuffle_ps(v129, v129, 255),
        *(float *)v37.m128i_i32 = (float)(*(float *)v37.m128i_i32 * *(float *)v37.m128i_i32) + v36,
        v38 = (_mm_cvtsi128_si32(v37) & 0x7FFFFFFFu) < 0x7F800000,
        *(float *)v37.m128i_i32 >= 1.02)
    || *(float *)v37.m128i_i32 <= 0.98000002
    || !v38 )
  {
LABEL_62:
    byte_18026FA98 = 0;
    sub_18009DF90();
    v43 = dword_18026F9F0;
    v44 = dword_18026F9F4;
    if ( dword_18026F9EC )
      qword_18026EC40((unsigned int)dword_18026F9EC);
    if ( v43 )
      qword_18026EC40(v43);
    if ( v44 )
      qword_18026EC40(v44);
    dword_18026F9F4 = 0;
    dword_18026F9F0 = 0;
    dword_18026F9EC = 0;
    byte_18026F9D8 = 0;
    byte_18026F9D9 = 0;
    byte_18026F9DA = 0;
    if ( dword_18026F9E8 )
      qword_18026EC40((unsigned int)dword_18026F9E8);
    dword_18026F9E8 = 0;
    v178 = 0;
    v179 = 0;
    sub_1800A73F0(0, &v178);
    sub_1800A9FB0(0);
    if ( qword_18026F9D0 )
    {
      sub_18009EC40();
      if ( dword_18026F9DC )
        qword_18026EC40((unsigned int)dword_18026F9DC);
      if ( dword_18026FA7C )
        qword_18026EC40((unsigned int)dword_18026FA7C);
      dword_18026FA7C = 0;
      dword_18026F9DC = 0;
      dword_180266674 = -1;
      byte_18026FA80 = 0;
      byte_18026FA81 = 0;
      byte_18026FA82 = 0;
      xmmword_180266680 = xmmword_18021D9B0;
      qword_18026FA88 = 0;
      dword_180266690 = -1;
      byte_18026FA90 = 0;
      if ( (_DWORD)qword_18026F9D0 )
        qword_18026EC40((unsigned int)qword_18026F9D0);
      if ( HIDWORD(qword_18026F9D0) )
        qword_18026EC40(HIDWORD(qword_18026F9D0));
      qword_18026F9D0 = 0;
      dword_18026FA78 = 0;
    }
    byte_18026F48C = 0;
    n3 = 3;
    goto LABEL_35;
  }
  v114 = v28;
  v39 = qword_18026F678;
  if ( !(unsigned __int8)sub_1800AA690(a1, a2, v121) )
  {
    v41 = v117;
    if ( (_BYTE)v8 != 1 || qword_18026F588(v117) == qword_18026F5C0 && (*(_BYTE *)(v117 + qword_18026F5E0) & 1) != 0 )
    {
      v177 = 0.0;
      v176 = 0;
      LOBYTE(v40) = 1;
      v125 = 0;
      v42 = v8;
    }
    else
    {
      v45 = sub_1800ABCC0(v117);
      v177 = 0.0;
      v176 = 0;
      if ( v45 )
      {
        v46 = v45;
        qword_18026F5B0(v45, &v176);
        v47 = v176 & 0x7FFFFFFF;
        v40 = 0;
        v125 = v46;
        if ( ((unsigned int)v176 & 0x7FFFFFFF) <= 0x7F7FFFFF && (HIDWORD(v176) & 0x7FFFFFFFu) < 0x7F800000 )
        {
          LOBYTE(v47) = (LODWORD(v177) & 0x7FFFFFFFu) < 0x7F800000;
          v40 = 0;
          v42 = 0;
LABEL_88:
          v116 = v39;
          v48 = v114 != v39;
          if ( !(_BYTE)v47 && v114 == v39 )
          {
            sub_1800AC080(v47, v40, v42);
            sub_1800A9FB0(0);
            sub_1800AC1B0();
            byte_18026F48C = 0;
            goto LABEL_49;
          }
          v133 = v42;
          v126 = v40;
          v49 = v47;
          v50 = sub_1801B3230();
          v122 = 0.0;
          v174[0] = *(float *)&v50 * 0.0;
          v174[1] = *(float *)&v50;
          v174[2] = *(float *)&v50 * 0.0;
          LODWORD(v174[3]) = sub_1801AC4E0();
          v149 = v130;
          sub_1800A5BC0(&v175, v174, &v149);
          v130 = (__m128)(unsigned int)v175;
          v51 = *(_QWORD *)((char *)&v175 + 4);
          v52 = HIDWORD(v175);
          v131 = v49;
          if ( (_BYTE)v49 )
          {
            v170 = v130.m128_i32[0];
            v171 = *(_QWORD *)((char *)&v175 + 4);
            v172 = HIDWORD(v175);
            v148 = v129;
            sub_1800A5BC0(v173, &v170, &v148);
            v122 = sub_1800AC280(v173);
          }
          v118 = v52;
          *(double *)&v53 = sub_1801B3230();
          v54 = _mm_mul_ps(_mm_shuffle_ps((__m128)v53, (__m128)v53, 0), (__m128)0x3F800000u);
          v55 = _mm_shuffle_ps(v54, _mm_shuffle_ps((__m128)COERCE_UNSIGNED_INT64(sub_1801AC4E0()), v54, 228), 36);
          v169 = v129;
          v147 = v55;
          sub_1800A5BC0(&v175, &v169, &v147);
          if ( ((unsigned __int8)v112 & (unsigned __int8)v131 & v48) == 1 )
          {
            v58 = v116;
            if ( qword_18026F588(v117) == qword_18026F5C8 || qword_18026F588(v117) == qword_18026F5D0 )
              v59 = sub_1800AC4A0(v117, a1, v56, v57, v14);
            else
              v59 = 0;
          }
          else
          {
            v59 = 0;
            v58 = v116;
          }
          v60 = v51;
          v61 = v118;
          v62 = v124;
          v129.m128_i32[0] = v59;
          v124.m128_i8[0] = v59 | (v114 == v58);
          v63 = v130;
          if ( v124.m128_i8[0] == 1 )
          {
            v64 = &xmmword_180266640;
            if ( ((unsigned __int8)byte_18026F9D8 & (v114 == v58)) != 0 )
              v64 = &xmmword_180266630;
            v63 = (__m128)*(unsigned int *)v64;
            v65 = _mm_srai_epi32(
                    _mm_slli_epi32(
                      _mm_shuffle_epi32(_mm_cvtsi32_si128((unsigned __int8)byte_18026F9D8 & (v114 == v58)), 0),
                      0x1Fu),
                    0x1Fu);
            v66 = _mm_and_si128(_mm_loadl_epi64((const __m128i *)((char *)&xmmword_180266630 + 4)), v65);
            v67 = _mm_andnot_si128(v65, _mm_loadl_epi64((const __m128i *)((char *)&xmmword_180266640 + 4)));
            v68 = (char *)&xmmword_180266640 + 12;
            if ( ((unsigned __int8)byte_18026F9D8 & (v114 == v58)) != 0 )
              v68 = (char *)&xmmword_180266630 + 12;
            v60 = _mm_or_si128(v67, v66).m128i_u64[0];
            v61 = *(_DWORD *)v68;
            v175 = xmmword_18021D9B0;
          }
          if ( v114 == v58 )
            byte_18026F9D9 = 1;
          v165 = v63.m128_i32[0];
          v166 = v60;
          v167 = v61;
          v146 = v175;
          sub_1800A5BC0(&v168, &v165, &v146);
          _mm_storel_ps((double *)&v161, (__m128)0x3F800000u);
          v162 = 0;
          v69 = (unsigned __int64)v168;
          v70 = *((unsigned __int64 *)&v168 + 1);
          v144 = *((unsigned __int64 *)&v168 + 1);
          v145 = (unsigned __int64)v168;
          sub_1800A5530(&v163, &v145, &v144, &v161);
          v157 = 0;
          n1065353216 = 1065353216;
          v142 = v70;
          v143 = v69;
          sub_1800A5530(&v159, &v143, &v142, &v157);
          if ( (_BYTE)v8 == 1 )
          {
            if ( (((unsigned __int8)v133 | (unsigned __int8)v126) & 1) == 0 )
            {
              v72 = v114;
              if ( (_BYTE)v131 )
              {
                v85 = *(float *)&v159;
                v87 = (__m128)v160;
                v86.m128_f32[0] = ((float (*)(void))sub_1801A8140)();
                v87.m128_f32[0] = v87.m128_f32[0] / v86.m128_f32[0];
                if ( v86.m128_f32[0] > 0.001 )
                  v88 = v85 / v86.m128_f32[0];
                else
                  v88 = 0.0;
                v103 = _mm_cmplt_ss((__m128)0x3A83126Fu, v86);
                v104 = v88 * v13.m128_f32[0];
                v128 = _mm_mul_ps(
                         _mm_shuffle_ps(
                           (__m128)_mm_move_epi64((__m128i)_mm_or_ps(
                                                             _mm_andnot_ps(v103, (__m128)0x3F800000u),
                                                             _mm_and_ps(v87, v103))),
                           (__m128)0LL,
                           226),
                         _mm_shuffle_ps(v13, v13, 0));
                v105 = v12;
                v62 = (__m128)(unsigned int)v176;
                v106 = (__m128)LODWORD(v177);
                v62.m128_f32[0] = *(float *)&v176 - v132.m128_f32[0];
                v105.m128_f32[0] = (float)(v12.m128_f32[0] - *(float *)v134.m128i_i32) + *((float *)&v176 + 1);
                v106.m128_f32[0] = v177 - v119;
                v107 = _mm_unpacklo_ps(v105, v106);
                n1022739087 = 1022739087;
                if ( v113 == 1 )
                {
                  sub_1800ACF30(v9);
                  while ( n15 != *(&n15 + 1) )
                    sub_1800AFAF0(v109, v108, v110);
                }
                v62.m128_f32[0] = v62.m128_f32[0] + v104;
                v128 = _mm_add_ps(v128, v107);
                n3 = 2;
                v156 = v160;
                v155 = v159;
                LOBYTE(v9) = 1;
                v73 = 0;
                if ( v115 != 1 || v114 == v116 )
                {
                  v72 = v114;
                  v19 = a1;
                  v41 = v117;
                }
                else
                {
                  v41 = v117;
                  LOBYTE(v73) = qword_18026F588(v117) != qword_18026F5C0;
                  v72 = v114;
                  v19 = a1;
                }
                v58 = v116;
LABEL_114:
                v74 = v72 == v58;
                sub_1800A73F0(v73, &v155);
                if ( !(v74 | (unsigned __int8)v9 ^ 1 | v129.m128_i8[0])
                  && (qword_18026F588(v41) == qword_18026F5C8 || qword_18026F588(v41) == qword_18026F5D0)
                  && (unsigned __int8)sub_1800B1310(v41, &v168) )
                {
                  v63 = (__m128)(unsigned int)v168;
                  v75 = (unsigned __int64)v168;
                  v60 = *(_QWORD *)((char *)&v168 + 4);
                  v76 = *((unsigned __int64 *)&v168 + 1);
                  v61 = HIDWORD(v168);
                  v175 = xmmword_18021D9B0;
                  _mm_storel_ps((double *)&v153, (__m128)0x3F800000u);
                  v154 = 0;
                  v140 = v76;
                  v141 = v75;
                  sub_1800A5530(&v163, &v141, &v140, &v153);
                  v151 = 0;
                  n1065353216_1 = 1065353216;
                  v138 = v76;
                  v139 = v75;
                  sub_1800A5530(&v159, &v139, &v138, &v151);
                }
                if ( (_BYTE)v9
                  && (qword_18026F5B0(v125, &v176),
                      v77 = LODWORD(v177) & 0x7FFFFFFF,
                      ((unsigned int)v176 & 0x7FFFFFFF) <= 0x7F7FFFFF) )
                {
                  v78 = v128;
                  if ( (HIDWORD(v176) & 0x7FFFFFFFu) < 0x7F800000 && (LODWORD(v177) & 0x7FFFFFFFu) < 0x7F800000 )
                  {
                    v79 = *(float *)&v159;
                    v80 = (__m128)v160;
                    LOBYTE(v77) = (LODWORD(v177) & 0x7FFFFFFFu) < 0x7F800000;
                    v81.m128_f32[0] = sub_1801A8140(v77);
                    v80.m128_f32[0] = v80.m128_f32[0] / v81.m128_f32[0];
                    v82 = _mm_cmplt_ss((__m128)0x3A83126Fu, v81);
                    v83 = (__m128i)_mm_or_ps(_mm_andnot_ps(v82, (__m128)0x3F800000u), _mm_and_ps(v80, v82));
                    if ( v81.m128_f32[0] > 0.001 )
                      v84 = v79 / v81.m128_f32[0];
                    else
                      v84 = 0.0;
                    v62 = (__m128)(unsigned int)v176;
                    v89 = (__m128)LODWORD(v177);
                    v62.m128_f32[0] = (float)(*(float *)&v176 - v132.m128_f32[0]) + (float)(v84 * v13.m128_f32[0]);
                    v12.m128_f32[0] = (float)(v12.m128_f32[0] - *(float *)v134.m128i_i32) + *((float *)&v176 + 1);
                    v89.m128_f32[0] = v177 - v119;
                    v78 = _mm_add_ps(
                            _mm_unpacklo_ps(v12, v89),
                            _mm_mul_ps(
                              _mm_shuffle_ps((__m128)_mm_move_epi64(v83), (__m128)0LL, 226),
                              _mm_shuffle_ps(v13, v13, 0)));
                    v19 = a1;
                    v41 = v117;
                    v58 = v116;
                  }
                }
                else
                {
                  v78 = v128;
                }
                v90 = _mm_shuffle_ps(_mm_shuffle_ps(v62, v63, 4), v78, 76);
                sub_1800A9FB0(v9 & v113);
                byte_18026F48C = v9;
                v91 = _mm_add_ps(
                        _mm_add_ps(
                          _mm_shuffle_ps(v90, v90, 120),
                          _mm_shuffle_ps((__m128)_mm_move_epi64((__m128i)v10), (__m128)xmmword_18021DBE0, 226)),
                        _mm_mul_ps(
                          _mm_shuffle_ps((__m128)v163, _mm_shuffle_ps((__m128)v164, (__m128)xmmword_18021D9B0, 48), 132),
                          _mm_shuffle_ps(v10, _mm_shuffle_ps((__m128)0x80000000, v10, 212), 37)));
                if ( (_BYTE)v9 )
                {
                  if ( v114 == v58 )
                  {
                    v92 = _mm_sub_pd(
                            (__m128d)_mm_unpacklo_epi32((__m128i)GetTickCount64(), (__m128i)xmmword_18021DA60),
                            (__m128d)xmmword_18021DA70);
                    v137 = _mm_add_ps(v132, v91);
                    sub_1800B1890(
                      (unsigned int)&v168,
                      v41,
                      (unsigned int)&v137,
                      v93,
                      COERCE__INT64((_mm_unpackhi_pd(v92, v92).m128d_f64[0] + v92.m128d_f64[0]) * 0.001));
                    v60 = *(_QWORD *)((char *)&v168 + 4);
                    v61 = HIDWORD(v168);
                    v91 = _mm_shuffle_ps(v91, _mm_shuffle_ps((__m128)(unsigned int)v168, v91, 228), 36);
                  }
                  v94 = v123;
                  v95 = *(float *)&v120;
                }
                else
                {
                  v96 = 0;
                  v96.m128_f32[0] = *(float *)&v135;
                  v91 = _mm_sub_ps(
                          v91,
                          _mm_mul_ps(
                            _mm_shuffle_ps(
                              (__m128)v159,
                              _mm_shuffle_ps((__m128)v160, (__m128)xmmword_18021D9B0, 48),
                              132),
                            _mm_shuffle_ps(v96, v96, 64)));
                  v95 = *(float *)&v120;
                  v94 = v127;
                }
                v186 = v91;
                v187 = v60;
                v188 = v61;
                v189 = v175;
                v185 = v95 + *(float *)(a2 + 48);
                v183 = v94;
                if ( v124.m128_i8[0] )
                  goto LABEL_145;
                if ( ((unsigned __int8)v9 & (unsigned __int8)v112) == 1 )
                {
                  if ( qword_18026F588(v41) == qword_18026F5C8 )
                  {
                    byte_18026F9DA = 1;
LABEL_144:
                    xmmword_180266640 = v168;
                    sub_1800B1DB0(v117);
LABEL_145:
                    qword_18026F660(v19, v182, v121);
                    v98 = LODWORD(v185);
                    *(double *)&v99 = sub_1801B3230();
                    v100 = _mm_mul_ps(_mm_shuffle_ps((__m128)v99, (__m128)v99, 0), (__m128)xmmword_18021D7B0);
                    v101.m128_u64[1] = *((_QWORD *)&v98 + 1);
                    *(double *)v101.m128_u64 = sub_1801AC4E0();
                    v136 = _mm_shuffle_ps(v100, _mm_shuffle_ps(v101, v100, 212), 32);
                    sub_1800A5BC0(v150, &v168, &v136);
                    sub_1800B2080(v19, v9, v124.m128_u8[0], v150);
                    if ( dword_18026F9DC )
                    {
                      v102 = qword_18026F598((unsigned int)dword_18026F9DC);
                      sub_1800A1F90(v102);
                    }
                    goto LABEL_37;
                  }
                  v97 = qword_18026F588(v117);
                  byte_18026F9DA = v97 == qword_18026F5D0;
                  if ( v97 == qword_18026F5D0 )
                    goto LABEL_144;
                }
                else
                {
                  byte_18026F9DA = 0;
                }
                sub_1800B1FF0();
                goto LABEL_145;
              }
LABEL_113:
              v156 = v160;
              v155 = v159;
              v9 = 0;
              v73 = 0;
              goto LABEL_114;
            }
            n3 = v133 & 1 ^ 5;
          }
          else
          {
            n3 = 1;
            if ( qword_18026F9D0 )
              sub_1800AC1B0();
          }
          v72 = v114;
          n3 = n3;
          goto LABEL_113;
        }
      }
      else
      {
        LOBYTE(v40) = 1;
        v125 = 0;
      }
      v42 = 0;
    }
    v47 = 0;
    goto LABEL_88;
  }
LABEL_37:
  result = (unsigned __int64)v111 ^ v190;
  if ( _security_cookie != ((unsigned __int64)v111 ^ v190) )
    JUMPOUT(0x1800A042ELL);
  return result;
}
