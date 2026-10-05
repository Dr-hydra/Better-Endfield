__int64 __fastcall sub_1800B2A90()
{
  __m128 v0; // xmm13
  __int64 v1023_1; // rax
  __int64 (__fastcall *v2)(_QWORD); // rax
  __m128 v3; // xmm2
  __m128 v4; // xmm1
  __int64 v5; // rax
  __int64 v6; // rsi
  __int64 v1001_1; // r14
  __int64 v8; // rbx
  __int64 v9; // rcx
  __int64 v1001_2; // rax
  __int64 v11; // rax
  __int64 v12; // rsi
  __m128i v13; // xmm4
  __m128i v14; // xmm1
  __m128i v15; // xmm3
  __m128i v16; // xmm2
  __m128 v17; // xmm3
  __m128i v18; // xmm5
  __m128i v19; // xmm4
  __m128i v20; // xmm1
  __int64 v21; // rsi
  __int64 v22; // rax
  __int64 v23; // rax
  __int64 v24; // rax
  __int64 v25; // rsi
  __int64 v26; // rax
  __int64 v27; // rax
  __int64 v28; // rax
  __int64 v29; // rsi
  __int64 v30; // rax
  __int64 v31; // rax
  __int64 v32; // rax
  __int64 v33; // rsi
  __int64 v34; // rax
  __int64 v35; // rax
  __int64 v36; // rax
  __int64 v37; // rsi
  __int64 v38; // rax
  __int64 v39; // rax
  __int64 v40; // rax
  int *v41; // rax
  int *v42; // rdi
  __int32 i; // eax
  __int64 v44; // rsi
  __int64 v45; // rax
  __int64 v46; // rax
  __int64 v47; // rax
  const __m128i *v48; // rax
  const __m128i *v49; // r15
  char *v50; // r13
  _QWORD *v51; // rsi
  signed __int64 n0x1000; // r13
  unsigned __int64 n0x100_2; // rdi
  unsigned __int64 v54; // rax
  unsigned __int64 v55; // rdx
  unsigned __int64 n0x100; // rax
  __int64 v57; // rbx
  __int64 v58; // rax
  _QWORD *v59; // r12
  int *v60; // r14
  int *v61; // rbx
  unsigned int n4; // esi
  __int64 v63; // rax
  int v64; // eax
  unsigned int n4_2; // r15d
  _BYTE *v1025_14; // rcx
  __m128i *p_p_n0x93; // r12
  void *v68; // rcx
  __m128i v69; // kr00_16
  __m128i *p_p_n0x93_1; // rbx
  __m128i *p_v1012_1; // rcx
  __m128i *p_v1012; // rsi
  __int64 n7; // r12
  __m128i v74; // xmm0
  __m128i *v75; // rsi
  _QWORD *v76; // rcx
  void *v77; // rcx
  unsigned int v1007_1; // edi
  bool v79; // bl
  unsigned __int64 n0x93_18; // r8
  unsigned __int64 v81; // rsi
  unsigned __int64 n12; // rcx
  unsigned __int64 n0x93_2; // rdx
  __int64 v84; // rdi
  __int64 v85; // rax
  __int64 p_m128i_i64_1; // r15
  __int64 *v87; // rsi
  __int64 v88; // rbx
  __int64 v89; // r14
  __int64 n0x93_3; // r12
  __int64 v91; // rax
  unsigned __int64 v92; // rcx
  __int64 v93; // r13
  __int64 v94; // rax
  unsigned __int64 v95; // r13
  __int64 v96; // rcx
  __int64 v97; // rdx
  __int16 v98; // r9
  unsigned __int64 v99; // r13
  __int64 v100; // rdi
  unsigned int n0x93_19; // eax
  __m128i v102; // xmm0
  int v103; // eax
  __int64 v104; // rax
  __int64 v105; // rcx
  __int16 v106; // dx
  unsigned __int64 v107; // rcx
  __int64 v108; // rax
  int *v109; // rdx
  int v110; // ecx
  int n3; // eax
  int *v112; // rdx
  int n4_1; // ebx
  __int32 v114; // r12d
  __int64 (__fastcall *v115)(__int64, __int64); // rdi
  __int64 v116; // rax
  unsigned int n0x93_17; // esi
  __int64 v118; // rax
  unsigned int v119; // r14d
  __int64 v120; // rax
  __int64 v1020_1; // rax
  char v122; // cl
  _WORD *v123; // r9
  __m128i v124; // xmm1
  __m128i v125; // xmm0
  __m128i v126; // xmm1
  __m128i v127; // xmm2
  __m128i v128; // xmm4
  __m128i v129; // xmm4
  __m128i v130; // xmm2
  __m128i v131; // xmm2
  __m128i v132; // xmm4
  __m128i v133; // xmm2
  __m128i v134; // xmm2
  __m128i v135; // xmm4
  __m128i v136; // xmm2
  __m128i v137; // xmm2
  __m128i v138; // xmm0
  __m128i v139; // xmm2
  __m128i v140; // xmm0
  __int64 v1020_3; // rcx
  unsigned __int64 n0x400_1; // rsi
  __int64 v143; // rdi
  __int64 v144; // rax
  _QWORD *v145; // rsi
  _BYTE *v146; // r14
  unsigned __int64 j_1; // r12
  unsigned __int64 j_2; // rbx
  unsigned int v1007_2; // ecx
  int v150; // eax
  char v151; // r13
  __int64 v152; // r10
  unsigned __int64 v153; // r9
  unsigned __int16 *v154; // r15
  unsigned __int16 *v155; // rdx
  char *v156; // r8
  __int64 v157; // rdx
  __int64 v158; // rax
  unsigned __int64 v159; // rcx
  unsigned __int64 v160; // rdx
  int v161; // r8d
  __int64 v162; // r8
  __int64 v163; // r9
  _DWORD *v164; // rax
  __int64 v165; // rcx
  unsigned __int64 v166; // rdx
  __int64 n7_1; // r8
  unsigned __int64 v168; // rcx
  unsigned __int64 n0x1B; // r8
  unsigned __int64 v170; // rdx
  __int64 v171; // r8
  __m128i si128; // xmm0
  __int64 v173; // r9
  __m128i v174; // xmm1
  __m128i v175; // xmm2
  __int64 v176; // r8
  __m128i v177; // xmm0
  unsigned __int64 v178; // r10
  __m128i v179; // xmm1
  __m128i v180; // xmm2
  _DWORD *v181; // rax
  __m128i v1012_1; // kr20_16
  unsigned __int64 n5; // rdi
  unsigned __int64 n2; // rbx
  __int64 n7_2; // rcx
  unsigned __int64 n2_6; // rax
  unsigned __int64 v187; // rax
  _DWORD *v188; // r8
  unsigned __int64 v189; // rcx
  __m128i v190; // xmm0
  __int64 v191; // r8
  __m128i v192; // xmm1
  __m128i v193; // xmm2
  __int64 n2_9; // rcx
  __m128i v195; // xmm0
  __int64 v196; // r9
  __m128i v197; // xmm1
  __m128i v198; // xmm2
  _DWORD *v199; // rcx
  __int64 v200; // rax
  unsigned __int64 j; // rcx
  __int64 v202; // r9
  unsigned int v203; // r10d
  __int64 v204; // r8
  __int64 k; // r9
  unsigned int k_1; // r10d
  __int64 k_2; // r11
  __int64 v208; // r9
  unsigned int v209; // r10d
  __int64 m; // r9
  unsigned int m_1; // r10d
  __int64 m_2; // r11
  double v1028_1; // r15
  double v1028_2; // r9
  __int64 n2_1; // r10
  __int64 v216; // r13
  __int64 v217; // r15
  __int64 v218; // rdi
  __int64 v219; // rbx
  float v220; // xmm0_4
  float v221; // xmm1_4
  unsigned __int64 v222; // rdi
  unsigned __int64 v223; // rbx
  unsigned __int64 v224; // rdi
  unsigned __int64 v225; // rbx
  __int64 n4_3; // rax
  __int64 v227; // rcx
  unsigned int v228; // edx
  __int64 v229; // rax
  __int64 n; // rcx
  unsigned int n_1; // edx
  __int64 n_2; // r8
  __int64 v233; // r8
  __int64 v234; // r9
  char v1020_4; // r13
  unsigned __int64 v236; // rdx
  __int64 v237; // rax
  __int64 n0x93_5; // rcx
  __int64 v239; // rdx
  unsigned __int64 v240; // r8
  unsigned int v241; // r11d
  unsigned __int64 v242; // r10
  unsigned __int64 v243; // rdi
  __int64 v244; // r11
  __int64 v245; // rbx
  unsigned __int64 p_m128i_i64_2; // rdi
  __int64 v247; // rbx
  unsigned __int64 p_m128i_i64_3; // rdi
  __int64 v249; // rbx
  unsigned __int64 p_m128i_i64_4; // rdi
  __int64 v251; // rdi
  __int64 v252; // rax
  __int64 v253; // rcx
  __int64 v254; // rdx
  __int64 n0x93_6; // r8
  __int64 v256; // r9
  unsigned __int64 ii; // r10
  int v258; // r11d
  __int64 v259; // rdi
  unsigned int v260; // ebx
  __int64 v261; // r11
  bool v262; // di
  unsigned __int64 v263; // r14
  unsigned __int64 v264; // r8
  unsigned __int64 v265; // rcx
  __int64 v1021_1; // rdx
  unsigned __int64 v1025_10; // rcx
  __int64 v268; // rsi
  __int64 v269; // r8
  unsigned __int64 n7_3; // rdi
  char *v271; // rdx
  __int64 v272; // rax
  __int64 v1025_15; // rax
  unsigned int n0x56_2; // edi
  int n0x56_1; // r14d
  __int64 v276; // r15
  __int64 v277; // rax
  unsigned __int64 v1020_5; // rsi
  __m128i *mm_2; // r15
  __int64 v280; // rax
  __int64 v281; // rbx
  int v282; // eax
  int v1021_6; // ecx
  unsigned __int64 v1025_16; // rax
  __int64 v285; // rbx
  __int64 v286; // r15
  __int64 v1025_1; // rdi
  unsigned __int64 v288; // r12
  unsigned __int64 n0x400_2; // rsi
  signed __int64 v1006_1; // r8
  __int64 v291; // r8
  __int64 v292; // rdi
  __int64 v293; // rax
  unsigned __int64 v294; // rbx
  __int64 v295; // rdx
  __int64 v296; // rcx
  __int64 v297; // r8
  unsigned __int64 n0x400_3; // r14
  unsigned __int64 v299; // r13
  unsigned __int64 v300; // rbx
  __int64 v301; // rcx
  unsigned __int64 v302; // rax
  unsigned __int64 v303; // r14
  __int64 n7_4; // rcx
  unsigned __int64 v305; // rax
  unsigned __int64 v306; // rax
  __m128i v307; // xmm0
  __int64 v308; // rdx
  __m128i v309; // xmm1
  __m128i v310; // xmm2
  __int64 v1004_1; // rsi
  __int64 v312; // rcx
  __int64 v313; // rcx
  __m128i v314; // xmm0
  __int64 v315; // r8
  __m128i v316; // xmm1
  __m128i v317; // xmm2
  int v318; // esi
  double v319; // xmm0_8
  __int64 v320; // rdi
  unsigned __int64 v1004_5; // rdx
  __int64 n0x93_7; // r14
  unsigned __int64 v323; // rax
  unsigned __int64 v1004_2; // rsi
  unsigned __int64 v1004_3; // r8
  unsigned __int64 v1004_6; // rsi
  __int64 v327; // rax
  double v328; // xmm6_8
  double v329; // xmm7_8
  double v330; // xmm8_8
  double v331; // xmm9_8
  double v332; // xmm10_8
  unsigned __int64 v1004_4; // r13
  unsigned __int64 v1004_7; // r15
  __int64 v335; // rdx
  __int64 v336; // rcx
  __int32 v337; // r8d
  int v338; // r8d
  __int32 v339; // r8d
  int v340; // r8d
  __int32 v341; // r8d
  int v342; // r8d
  __int32 v343; // r8d
  __int32 v344; // eax
  __int32 v345; // r9d
  __int32 v346; // r8d
  int v347; // eax
  int v348; // r10d
  __int32 v349; // r10d
  int v350; // eax
  int v351; // edx
  __int32 v352; // edx
  int v353; // eax
  int v354; // r10d
  int v355; // r10d
  int v356; // eax
  int v357; // edx
  int v358; // edx
  int v359; // eax
  int v360; // r10d
  int v361; // r10d
  int v362; // eax
  int v363; // edx
  int v364; // edx
  int v365; // eax
  int v366; // ecx
  __int64 v367; // r11
  __int64 v368; // rbx
  unsigned __int64 v369; // r13
  __int64 v370; // r9
  unsigned __int64 v371; // r10
  int v372; // eax
  unsigned __int64 n0x400_4; // rax
  unsigned __int64 n0x400_5; // rcx
  unsigned __int64 n0x400_6; // r8
  __int64 n0x93_9; // r15
  int v377; // eax
  int v378; // ecx
  int v379; // edx
  unsigned int v380; // edi
  unsigned int v381; // eax
  unsigned __int64 v382; // rcx
  __int64 jj; // rdx
  __int64 v384; // r14
  __int64 v385; // r8
  int v386; // esi
  int v387; // r12d
  unsigned __int64 v388; // r11
  unsigned __int64 v389; // rbx
  unsigned __int64 v390; // rdx
  unsigned __int64 n0x80; // rax
  __int64 v392; // rdx
  __int64 v393; // rax
  unsigned __int64 v394; // rcx
  __int64 n0x1000_6; // rax
  unsigned __int64 n0x1000_1; // r12
  __int64 v397; // rsi
  _QWORD *v398; // r14
  _QWORD *v399; // rcx
  __int64 v400; // rdi
  unsigned __int64 v401; // r14
  __int64 v402; // rsi
  unsigned __int64 v403; // r12
  unsigned __int64 v404; // rax
  unsigned __int64 v405; // rcx
  unsigned int v406; // r8d
  unsigned int v407; // r9d
  unsigned __int32 *v408; // rdx
  unsigned __int32 v409; // r10d
  unsigned __int32 v410; // r11d
  __int32 v411; // r13d
  __int64 v412; // rax
  __int64 v413; // rcx
  __int64 v414; // rax
  __int64 v415; // rcx
  bool v416; // zf
  __m128i *v417; // rdx
  __m128i v418; // xmm0
  __int64 v419; // rdx
  __m128i v420; // xmm0
  __int64 v421; // rax
  __m128i *v422; // rax
  double v423; // xmm7_8
  double v424; // xmm11_8
  double v425; // xmm6_8
  double v426; // xmm11_8
  double v427; // xmm10_8
  double v428; // xmm11_8
  double v429; // xmm7_8
  double v430; // xmm10_8
  double v431; // xmm3_8
  double v432; // xmm4_8
  double v433; // xmm5_8
  __m128i v434; // xmm1
  __int64 v435; // rcx
  char *v436; // r15
  unsigned __int64 n32; // rsi
  __int64 v438; // rdx
  __int64 v439; // rcx
  __int64 v440; // r8
  unsigned __int64 n0x400_7; // r14
  unsigned __int64 v442; // rdi
  __int64 v443; // rax
  unsigned __int64 v444; // rbx
  unsigned __int64 v445; // r14
  __int64 v446; // rax
  __int64 v447; // rdi
  __int64 v1021_2; // rcx
  unsigned __int64 v449; // r8
  __int64 v450; // r10
  int v1021_7; // r11d
  __int64 v452; // r10
  int v453; // r11d
  int v1021_3; // r9d
  unsigned __int64 v455; // rsi
  __int64 v456; // rbx
  __int64 v457; // r15
  __int64 v458; // rdi
  __int128 v459; // xmm9
  __m128d v460; // xmm11
  unsigned __int64 v461; // r12
  _DWORD *v462; // rdi
  int v463; // eax
  __int64 v464; // rsi
  __int64 v465; // r14
  unsigned __int64 v466; // rbx
  int v467; // r8d
  _QWORD *v468; // rcx
  __int64 v469; // rbx
  __int64 v470; // rax
  char *v471; // rbx
  __int64 v472; // rax
  _DWORD *v473; // r8
  unsigned int *v1025_2; // rbx
  signed __int64 n3_1; // rcx
  char v476; // r8
  unsigned int *v477; // r9
  _WORD *v478; // r11
  unsigned __int64 v479; // r10
  bool v480; // r10
  unsigned __int64 v481; // rsi
  unsigned __int64 v482; // rsi
  unsigned __int64 v483; // r11
  double v484; // xmm2_8
  double v485; // xmm5_8
  double v486; // xmm1_8
  __m128 v487; // xmm8
  char v488; // r8
  double v489; // xmm6_8
  double v490; // xmm0_8
  unsigned __int64 v491; // r9
  __m128 v492; // xmm8
  __m128 v493; // xmm10
  float v494; // xmm6_4
  unsigned int *v1025_4; // r9
  __int64 v496; // r11
  __m128 v497; // xmm7
  double v498; // xmm4_8
  float v499; // xmm11_4
  double v500; // xmm3_8
  double v501; // xmm15_8
  bool v502; // r10
  double v503; // xmm7_8
  double v504; // xmm3_8
  double v505; // xmm8_8
  double v506; // xmm6_8
  double v507; // xmm4_8
  __m128d v508; // xmm0
  double v509; // xmm1_8
  __int64 kk; // r8
  __int64 v511; // r10
  __int64 kk_1; // r9
  __int64 v513; // r11
  float v514; // xmm3_4
  __m128 v515; // xmm5
  __m128 v516; // xmm7
  __m128 v517; // xmm4
  __int64 v518; // rcx
  __int64 v519; // r8
  float v520; // xmm4_4
  __m128 v521; // xmm5
  __m128 v522; // xmm7
  __m128 v523; // xmm3
  __m128d v524; // xmm0
  __m128i v525; // xmm5
  double v526; // xmm6_8
  int n2_2; // r13d
  unsigned __int64 v528; // rdx
  bool v529; // cf
  __int64 n2_3; // r13
  double *v531; // rdi
  __int64 v532; // rcx
  double v533; // xmm6_8
  double v534; // xmm8_8
  _QWORD *v535; // rbx
  __int64 n0x1000_2; // rdi
  unsigned __int64 n0x100_3; // rsi
  unsigned __int64 v538; // rax
  unsigned __int64 v539; // rdx
  unsigned __int64 n0x100_1; // rax
  __int64 v541; // r14
  __int64 v542; // rax
  _QWORD *v543; // r15
  __int128 v544; // xmm14
  double *v545; // rbx
  __int64 v546; // rax
  signed __int64 n16; // rdi
  unsigned __int64 n2_4; // rax
  __m128d v549; // xmm0
  double v550; // xmm1_8
  __int64 n2_7; // rcx
  __m128d v552; // xmm3
  double v553; // xmm2_8
  __m128d *v554; // rdx
  __int64 n2_10; // r8
  __m128i v556; // xmm4
  __int64 n2_8; // rdx
  double v558; // xmm12_8
  __m128i v559; // xmm3
  __int128 v1025_3; // xmm0
  __int64 v561; // r10
  __int64 v562; // r8
  __int64 v563; // rcx
  __int64 v564; // r9
  __int64 v565; // r15
  double *v566; // rdi
  double *v567; // r14
  unsigned __int64 v568; // r13
  double *v569; // rsi
  __int64 v570; // r11
  __int64 v571; // r11
  double v572; // xmm3_8
  double v573; // xmm0_8
  double v574; // xmm2_8
  double v575; // xmm1_8
  double v576; // xmm4_8
  double v577; // xmm5_8
  double v578; // xmm7_8
  double v579; // xmm6_8
  double v580; // xmm8_8
  double v581; // xmm9_8
  double v582; // xmm1_8
  double v583; // xmm6_8
  double v584; // xmm4_8
  double v585; // xmm5_8
  signed __int64 v586; // rsi
  char *n0x93_13; // r13
  char *n0x93_11; // r14
  unsigned __int64 v589; // rax
  __int64 n3_2; // rdx
  unsigned __int64 v591; // rcx
  unsigned __int64 n0x17; // rcx
  unsigned __int64 v593; // rax
  char *n0x93_12; // rdx
  __int64 v595; // rcx
  __int64 v596; // rdx
  __m128i v597; // xmm0
  __m128i v598; // xmm4
  __m128i v599; // xmm5
  __int64 v600; // rdx
  unsigned __int64 v601; // r9
  __m128i v602; // xmm0
  __m128i v603; // xmm2
  __m128i v604; // xmm3
  int v605; // eax
  double v606; // xmm12_8
  _QWORD *v607; // rdx
  unsigned __int64 v608; // rcx
  unsigned __int64 v609; // r8
  __int64 v610; // rsi
  unsigned __int64 v611; // rdx
  __int64 v612; // rdi
  __int64 v613; // rax
  __int64 v614; // r10
  __int64 v615; // rdx
  __int64 v616; // rdx
  double v617; // xmm0_8
  double v618; // xmm8_8
  double v619; // xmm2_8
  double v620; // xmm3_8
  double v621; // xmm4_8
  double v622; // xmm5_8
  double v623; // xmm1_8
  double v624; // xmm6_8
  char *n0x93_14; // r11
  __int64 v626; // r15
  __int64 v627; // r15
  double v628; // xmm7_8
  double v629; // xmm15_8
  _QWORD *v630; // rdx
  __int64 *v631; // rdi
  __int64 *v632; // rsi
  __int64 v633; // rbx
  __int64 v634; // rdx
  void *v635; // rcx
  void *v636; // rcx
  void *n0x93_15; // rcx
  _QWORD *v638; // r15
  __int64 v639; // rbx
  __int64 v640; // r14
  __int128 v641; // kr50_16
  unsigned __int64 n0x400_8; // rsi
  __int64 v643; // rcx
  __int64 v644; // rax
  __int64 v645; // rax
  __int64 v646; // r12
  __m128i *mm; // rsi
  double v1028_3; // r13
  unsigned __int64 v1025_5; // rcx
  __int64 v650; // rbx
  __int64 v651; // r14
  unsigned __int64 n0x400_11; // r13
  unsigned __int64 n0x1000_3; // rdi
  unsigned __int64 v654; // rsi
  unsigned __int64 v655; // rcx
  unsigned __int64 n0x400_9; // rsi
  __int64 v657; // rcx
  __int64 v658; // rax
  unsigned __int64 v659; // r12
  unsigned __int64 v1025_12; // rsi
  unsigned __int64 v1025_6; // rcx
  unsigned __int64 v662; // rdx
  __int64 v663; // r12
  unsigned __int64 v1025_13; // rax
  __int64 v665; // r14
  _QWORD *v666; // rbx
  __int64 v667; // r14
  unsigned __int64 n0x400_12; // rsi
  unsigned __int64 n0x1000_4; // rdi
  unsigned __int64 v670; // r13
  unsigned __int64 v671; // rcx
  unsigned __int64 n0x400_10; // r13
  __int64 v673; // rcx
  __int64 v674; // rax
  unsigned __int64 v675; // r15
  __m128i *v1020_7; // rsi
  __int64 v677; // rcx
  _DWORD *v678; // rcx
  int v679; // eax
  void *v680; // rcx
  unsigned __int64 n0x1000_5; // rdi
  __int64 v682; // rax
  unsigned __int64 v683; // rsi
  unsigned int v1007_3; // edx
  unsigned __int64 v685; // rax
  unsigned int n0x10000; // ecx
  __int64 v687; // rsi
  __int64 v688; // rax
  int v1030_1; // edi
  __int64 v690; // rax
  __int64 v691; // rax
  __int64 (__fastcall *v692)(__int64); // rsi
  __int64 v693; // rax
  unsigned int v694; // edi
  __int64 v695; // rax
  __int64 v696; // rsi
  __int64 v697; // rax
  __int64 v698; // rdi
  __int64 v699; // rbx
  unsigned __int64 n0x93_16; // rax
  __int64 v701; // rax
  __int64 v702; // rax
  __int64 v703; // rax
  __int64 v704; // rsi
  char v705; // bl
  int v1029_1; // edi
  int v1025_7; // edi
  __int64 v708; // rax
  __int64 v709; // rdi
  _BYTE *v710; // rdi
  _BYTE *v711; // rbx
  __int64 v712; // rax
  unsigned int nn; // edi
  int v714; // r14d
  __int64 v715; // rax
  __int64 v716; // rbx
  __int64 v717; // rax
  __int64 v718; // rbx
  __int64 v719; // rdi
  __int64 v720; // rax
  unsigned __int64 v721; // rdi
  __int64 v722; // rax
  __int64 v723; // rbx
  __int64 v724; // rax
  __int64 v725; // rax
  __int64 v726; // rdi
  void (__fastcall *v727)(__int64, __int64, _QWORD *, __m128i *); // rbx
  __int64 v728; // rax
  unsigned int n0x56_3; // edi
  __int64 v1020_8; // rbx
  __int64 v731; // rax
  __int64 v732; // rax
  __int64 v733; // rax
  __int64 v734; // rdi
  __int64 v735; // rax
  __int64 v736; // rax
  __int64 v737; // rdi
  __int64 v738; // rax
  __int64 v739; // rdi
  __int64 v740; // rax
  __int64 v741; // rax
  __int64 v742; // rax
  _DWORD *v743; // rax
  __int64 v744; // rax
  __int64 v745; // rsi
  int i1; // edi
  __int64 v747; // rbx
  __int64 v748; // rax
  __int64 v749; // rax
  __int64 v750; // rax
  unsigned __int64 v751; // rbx
  __int64 (__fastcall *v752)(__int64, _QWORD); // rdi
  __int64 v753; // rax
  __int64 v754; // rax
  const __m128i *v755; // rdi
  int v756; // eax
  __m128i v757; // xmm0
  __m128i v758; // xmm2
  __m128i v1013_1; // xmm3
  __int64 v760; // rax
  unsigned int v761; // eax
  __int64 v762; // rdi
  void (__fastcall *v763)(__int64, __int64, _QWORD, _QWORD, _DWORD, int); // r13
  __int64 v764; // rdi
  __int64 v765; // rax
  __int64 v766; // rdi
  __int64 v767; // rax
  int v768; // edi
  __int64 v769; // rbx
  __int64 v770; // rax
  __int64 v771; // r12
  __int64 v772; // rsi
  __int64 n2_5; // r13
  __int8 v774; // bl
  __int64 v1022_1; // rcx
  __int64 v776; // rax
  __int64 v777; // rax
  __int64 (__fastcall *v778)(__int64, _QWORD); // r14
  bool v779; // zf
  __int64 v780; // rdi
  __int64 v781; // rbx
  __int64 v782; // rdi
  __int64 v783; // rax
  void (__fastcall *v784)(__int64, __int64, _QWORD, _QWORD, _DWORD, int); // r15
  __int64 v785; // r14
  __int64 v786; // rax
  __int64 v787; // rax
  int v788; // edi
  __int64 v789; // rax
  __int64 (__fastcall *v790)(__int64); // rsi
  __int64 v791; // rax
  __int64 v792; // rsi
  __int64 (__fastcall *v793)(__int64); // rsi
  __int64 v794; // rax
  __int64 v795; // rax
  int v1025_8; // esi
  unsigned int v797; // ebx
  __int64 v798; // rax
  __int64 v799; // rdi
  __int64 v800; // rax
  __int64 v801; // rbx
  __int64 v802; // r14
  __int64 v803; // rcx
  int v804; // edi
  int v805; // esi
  int v806; // r13d
  __int64 v807; // rbx
  unsigned __int64 v808; // r12
  unsigned __int64 v809; // r8
  __int64 v810; // r15
  unsigned __int64 v811; // rdx
  unsigned __int64 v812; // r12
  unsigned __int64 v813; // r8
  unsigned __int64 n0x80_1; // rax
  __int64 v815; // rcx
  __int64 v816; // rax
  unsigned __int64 v817; // r14
  __int64 v818; // r8
  __int64 v819; // rcx
  __int64 v820; // rdx
  void *v821; // rcx
  char *v822; // rsi
  void *v823; // rcx
  unsigned int v1025_9; // edi
  void *v825; // rcx
  void *v1020_9; // rcx
  void *v827; // rcx
  void *p_m128i_i64_6; // rcx
  void *v829; // rcx
  void *v830; // rcx
  unsigned int v1023_2; // esi
  __int64 v832; // rax
  __int64 v833; // rax
  __int64 (__fastcall *v834)(__int64, _QWORD); // rsi
  __int64 v835; // rax
  __int64 (__fastcall *v836)(__int64, _QWORD); // rsi
  __int64 v837; // rax
  __int64 (__fastcall *v838)(__int64, _QWORD); // rsi
  __int64 v839; // rax
  __int64 (__fastcall *v840)(__int64, _QWORD); // rsi
  __int64 v841; // rax
  __m128i *v842; // rcx
  void (__fastcall *v843)(__int64); // rsi
  __int64 v844; // rax
  void **p_??_7runtime_error@std@@6B@; // [rsp+48h] [rbp-38h] BYREF
  __int128 v846; // [rsp+50h] [rbp-30h] BYREF
  void **p_??_7runtime_error@std@@6B@_1; // [rsp+60h] [rbp-20h] BYREF
  __int128 v848; // [rsp+68h] [rbp-18h] BYREF
  void **p_??_7runtime_error@std@@6B@_2; // [rsp+78h] [rbp-8h] BYREF
  __int128 v850; // [rsp+80h] [rbp+0h] BYREF
  void **p_??_7exception@std@@6B@_1; // [rsp+90h] [rbp+10h] BYREF
  __int128 v852; // [rsp+98h] [rbp+18h] BYREF
  void **p_??_7exception@std@@6B@_2; // [rsp+A8h] [rbp+28h] BYREF
  __int128 v854; // [rsp+B0h] [rbp+30h] BYREF
  void **p_??_7exception@std@@6B@_3; // [rsp+C0h] [rbp+40h] BYREF
  __int128 v856; // [rsp+C8h] [rbp+48h] BYREF
  void **p_??_7exception@std@@6B@_4; // [rsp+D8h] [rbp+58h] BYREF
  __int128 v858; // [rsp+E0h] [rbp+60h] BYREF
  void **p_??_7exception@std@@6B@_5; // [rsp+F0h] [rbp+70h] BYREF
  __int128 v860; // [rsp+F8h] [rbp+78h] BYREF
  void **p_??_7exception@std@@6B@_6; // [rsp+108h] [rbp+88h] BYREF
  __int128 v862; // [rsp+110h] [rbp+90h] BYREF
  void **p_??_7exception@std@@6B@_7; // [rsp+120h] [rbp+A0h] BYREF
  __int128 v864; // [rsp+128h] [rbp+A8h] BYREF
  void **p_??_7exception@std@@6B@; // [rsp+138h] [rbp+B8h] BYREF
  __int128 v866; // [rsp+140h] [rbp+C0h] BYREF
  void **p_??_7exception@std@@6B@_8; // [rsp+150h] [rbp+D0h] BYREF
  __int128 v868; // [rsp+158h] [rbp+D8h] BYREF
  void **p_??_7exception@std@@6B@_9; // [rsp+168h] [rbp+E8h] BYREF
  __int128 v870; // [rsp+170h] [rbp+F0h] BYREF
  void **p_??_7exception@std@@6B@_10; // [rsp+180h] [rbp+100h] BYREF
  __int128 v872; // [rsp+188h] [rbp+108h] BYREF
  void **p_??_7exception@std@@6B@_11; // [rsp+198h] [rbp+118h] BYREF
  __int128 v874; // [rsp+1A0h] [rbp+120h] BYREF
  void **p_??_7exception@std@@6B@_12; // [rsp+1B0h] [rbp+130h] BYREF
  __int128 v876; // [rsp+1B8h] [rbp+138h] BYREF
  void **p_??_7exception@std@@6B@_13; // [rsp+1C8h] [rbp+148h] BYREF
  __int128 v878; // [rsp+1D0h] [rbp+150h] BYREF
  void **p_??_7exception@std@@6B@_14; // [rsp+1E0h] [rbp+160h] BYREF
  __int128 v880; // [rsp+1E8h] [rbp+168h] BYREF
  void **p_??_7exception@std@@6B@_15; // [rsp+1F8h] [rbp+178h] BYREF
  __int128 v882; // [rsp+200h] [rbp+180h] BYREF
  void **p_??_7exception@std@@6B@_16; // [rsp+210h] [rbp+190h] BYREF
  __int128 v884; // [rsp+218h] [rbp+198h] BYREF
  void **p_??_7exception@std@@6B@_17; // [rsp+228h] [rbp+1A8h] BYREF
  __int128 v886; // [rsp+230h] [rbp+1B0h] BYREF
  void **p_??_7exception@std@@6B@_19; // [rsp+240h] [rbp+1C0h] BYREF
  __int128 v888; // [rsp+248h] [rbp+1C8h] BYREF
  void **p_??_7exception@std@@6B@_18; // [rsp+258h] [rbp+1D8h] BYREF
  __int128 v890; // [rsp+260h] [rbp+1E0h] BYREF
  void **p_??_7exception@std@@6B@_20; // [rsp+270h] [rbp+1F0h] BYREF
  __int128 v892; // [rsp+278h] [rbp+1F8h] BYREF
  void **p_??_7exception@std@@6B@_21; // [rsp+288h] [rbp+208h] BYREF
  __int128 v894; // [rsp+290h] [rbp+210h] BYREF
  void **p_??_7exception@std@@6B@_22; // [rsp+2A0h] [rbp+220h] BYREF
  __int128 v896; // [rsp+2A8h] [rbp+228h] BYREF
  void **p_??_7exception@std@@6B@_23; // [rsp+2B8h] [rbp+238h] BYREF
  __int128 v898; // [rsp+2C0h] [rbp+240h] BYREF
  void **p_??_7exception@std@@6B@_24; // [rsp+2D0h] [rbp+250h] BYREF
  __int128 v900; // [rsp+2D8h] [rbp+258h] BYREF
  void **p_??_7exception@std@@6B@_25; // [rsp+2E8h] [rbp+268h] BYREF
  __int128 v902; // [rsp+2F0h] [rbp+270h] BYREF
  void **p_??_7exception@std@@6B@_26; // [rsp+300h] [rbp+280h] BYREF
  __int128 v904; // [rsp+308h] [rbp+288h] BYREF
  void **p_??_7exception@std@@6B@_27; // [rsp+318h] [rbp+298h] BYREF
  __int128 v906; // [rsp+320h] [rbp+2A0h] BYREF
  void **p_??_7exception@std@@6B@_28; // [rsp+330h] [rbp+2B0h] BYREF
  __int128 v908; // [rsp+338h] [rbp+2B8h] BYREF
  void **p_??_7exception@std@@6B@_29; // [rsp+348h] [rbp+2C8h] BYREF
  __int128 v910; // [rsp+350h] [rbp+2D0h] BYREF
  void **p_??_7exception@std@@6B@_30; // [rsp+360h] [rbp+2E0h] BYREF
  __int128 v912; // [rsp+368h] [rbp+2E8h] BYREF
  void **p_??_7exception@std@@6B@_31; // [rsp+378h] [rbp+2F8h] BYREF
  __int128 v914; // [rsp+380h] [rbp+300h] BYREF
  void **p_??_7exception@std@@6B@_35; // [rsp+390h] [rbp+310h] BYREF
  __int128 v916; // [rsp+398h] [rbp+318h] BYREF
  void **p_??_7exception@std@@6B@_37; // [rsp+3A8h] [rbp+328h] BYREF
  __int128 v918; // [rsp+3B0h] [rbp+330h] BYREF
  void **p_??_7exception@std@@6B@_36; // [rsp+3C0h] [rbp+340h] BYREF
  __int128 v920; // [rsp+3C8h] [rbp+348h] BYREF
  void **p_??_7exception@std@@6B@_32; // [rsp+3D8h] [rbp+358h] BYREF
  __int128 v922; // [rsp+3E0h] [rbp+360h] BYREF
  void **p_??_7exception@std@@6B@_33; // [rsp+3F0h] [rbp+370h] BYREF
  __int128 v924; // [rsp+3F8h] [rbp+378h] BYREF
  void **p_??_7exception@std@@6B@_34; // [rsp+408h] [rbp+388h] BYREF
  __int128 v926; // [rsp+410h] [rbp+390h] BYREF
  double v927; // [rsp+420h] [rbp+3A0h] BYREF
  __int128 n0x93_10; // [rsp+428h] [rbp+3A8h] BYREF
  __int64 v1025_11; // [rsp+440h] [rbp+3C0h]
  unsigned __int64 v930; // [rsp+448h] [rbp+3C8h]
  __m128i p_v1011; // [rsp+450h] [rbp+3D0h] BYREF
  __m128i v932; // [rsp+460h] [rbp+3E0h] BYREF
  __m128i v933; // [rsp+470h] [rbp+3F0h] BYREF
  __m128i v934; // [rsp+480h] [rbp+400h] BYREF
  __m128i v935; // [rsp+490h] [rbp+410h] BYREF
  __int64 v936; // [rsp+4A0h] [rbp+420h]
  unsigned __int64 v937; // [rsp+4A8h] [rbp+428h]
  __int128 v1020_6; // [rsp+4B0h] [rbp+430h]
  unsigned __int64 v939; // [rsp+4C0h] [rbp+440h]
  _QWORD v940[3]; // [rsp+4D0h] [rbp+450h] BYREF
  __m128i n0x93; // [rsp+4E8h] [rbp+468h] BYREF
  __int64 v942; // [rsp+4F8h] [rbp+478h]
  __int128 v943; // [rsp+500h] [rbp+480h] BYREF
  unsigned __int64 v944; // [rsp+510h] [rbp+490h]
  int v945; // [rsp+524h] [rbp+4A4h]
  __m128i *mm_1; // [rsp+528h] [rbp+4A8h]
  __m128i v947; // [rsp+530h] [rbp+4B0h] BYREF
  __int128 v948; // [rsp+540h] [rbp+4C0h] BYREF
  __int64 v949; // [rsp+550h] [rbp+4D0h]
  __int128 p_m128i_i64_5; // [rsp+560h] [rbp+4E0h]
  __int64 v951; // [rsp+570h] [rbp+4F0h]
  __m128i v1012; // [rsp+580h] [rbp+500h] BYREF
  __m128i v953; // [rsp+590h] [rbp+510h] BYREF
  __int64 n36; // [rsp+5A0h] [rbp+520h]
  __int64 n40; // [rsp+5A8h] [rbp+528h]
  __int64 n44; // [rsp+5B0h] [rbp+530h]
  __int64 (__fastcall *v957)(__int64, _QWORD); // [rsp+5B8h] [rbp+538h]
  LPVOID lpMem[2]; // [rsp+5C0h] [rbp+540h]
  char *v959; // [rsp+5D0h] [rbp+550h]
  LPVOID v960[2]; // [rsp+5E0h] [rbp+560h] BYREF
  __int64 v1021_4; // [rsp+5F0h] [rbp+570h]
  LPVOID v962[2]; // [rsp+600h] [rbp+580h] BYREF
  double *v963; // [rsp+610h] [rbp+590h]
  LPVOID v964[2]; // [rsp+620h] [rbp+5A0h]
  char *v965; // [rsp+630h] [rbp+5B0h]
  __int64 v1021_5; // [rsp+640h] [rbp+5C0h] BYREF
  unsigned __int64 n0x400; // [rsp+648h] [rbp+5C8h]
  __int64 v968; // [rsp+650h] [rbp+5D0h] BYREF
  int v969; // [rsp+658h] [rbp+5D8h]
  int v970; // [rsp+65Ch] [rbp+5DCh]
  __int64 v971; // [rsp+660h] [rbp+5E0h]
  __int64 v1020_2; // [rsp+668h] [rbp+5E8h]
  __m128i v1012__1; // [rsp+670h] [rbp+5F0h] BYREF
  __int64 v974; // [rsp+680h] [rbp+600h]
  _QWORD v975[3]; // [rsp+690h] [rbp+610h] BYREF
  int v976; // [rsp+6ACh] [rbp+62Ch]
  __int64 n0x56; // [rsp+6B0h] [rbp+630h]
  unsigned __int64 n0x93_1; // [rsp+6B8h] [rbp+638h]
  __int128 v979; // [rsp+6C0h] [rbp+640h]
  __int128 v980; // [rsp+6D0h] [rbp+650h] BYREF
  __int64 v981; // [rsp+6E0h] [rbp+660h]
  _OWORD v982[2]; // [rsp+6F0h] [rbp+670h] BYREF
  __int128 v983; // [rsp+710h] [rbp+690h]
  __int128 v984; // [rsp+720h] [rbp+6A0h]
  __int128 v985; // [rsp+730h] [rbp+6B0h]
  __int128 v986; // [rsp+740h] [rbp+6C0h]
  int v987; // [rsp+750h] [rbp+6D0h]
  int v988; // [rsp+754h] [rbp+6D4h]
  unsigned __int64 v989; // [rsp+758h] [rbp+6D8h]
  __int128 n0x93_4; // [rsp+760h] [rbp+6E0h] BYREF
  unsigned __int64 v991; // [rsp+770h] [rbp+6F0h]
  __m128i v1012_; // [rsp+780h] [rbp+700h] BYREF
  __int64 v993; // [rsp+790h] [rbp+710h]
  unsigned __int64 p_m128i_i64; // [rsp+798h] [rbp+718h] BYREF
  unsigned __int64 v995; // [rsp+7A0h] [rbp+720h]
  __int64 n0x93_8; // [rsp+7A8h] [rbp+728h]
  __m128i p_n0x93; // [rsp+7B0h] [rbp+730h] BYREF
  unsigned __int64 v998; // [rsp+7C0h] [rbp+740h]
  int v999; // [rsp+7D0h] [rbp+750h]
  int v1000; // [rsp+7D4h] [rbp+754h]
  __int64 v1001; // [rsp+7D8h] [rbp+758h]
  unsigned __int64 v1002; // [rsp+7E0h] [rbp+760h]
  unsigned __int64 v1004; // [rsp+7E8h] [rbp+768h]
  int var1E0; // [rsp+7F0h] [rbp+770h]
  int v1005; // [rsp+7F4h] [rbp+774h]
  signed __int64 v1006; // [rsp+7F8h] [rbp+778h]
  unsigned int v1007; // [rsp+804h] [rbp+784h]
  int v1008; // [rsp+808h] [rbp+788h]
  int v1009; // [rsp+80Ch] [rbp+78Ch]
  __m128i v1010; // [rsp+810h] [rbp+790h] BYREF
  __m128i v1011[2]; // [rsp+820h] [rbp+7A0h] BYREF
  __m128i var190; // [rsp+840h] [rbp+7C0h]
  __m128i v1013; // [rsp+850h] [rbp+7D0h]
  _OWORD v1015[2]; // [rsp+860h] [rbp+7E0h] BYREF
  __m128 var150; // [rsp+880h] [rbp+800h]
  int v1016; // [rsp+898h] [rbp+818h]
  int v1017; // [rsp+89Ch] [rbp+81Ch]
  int v1018; // [rsp+8A0h] [rbp+820h]
  int v1019; // [rsp+8A4h] [rbp+824h]
  __int64 v1020; // [rsp+8A8h] [rbp+828h]
  unsigned __int8 v1021; // [rsp+8B3h] [rbp+833h]
  unsigned int v1022; // [rsp+8B4h] [rbp+834h]
  int v1023; // [rsp+8B8h] [rbp+838h]
  int v1024; // [rsp+8BCh] [rbp+83Ch]
  __int128 v1025; // [rsp+8C0h] [rbp+840h]
  unsigned int v1026; // [rsp+8D0h] [rbp+850h]
  unsigned int v1027; // [rsp+8D4h] [rbp+854h]
  double v1028; // [rsp+8D8h] [rbp+858h]
  int v1029; // [rsp+8E0h] [rbp+860h]
  int v1030; // [rsp+8E4h] [rbp+864h]
  __int64 v1031; // [rsp+8E8h] [rbp+868h]

  v1031 = -2;
  v1023_1 = (unsigned int)::v1023;
  if ( !(_DWORD)::v1023 )
    return v1023_1;
  v1022 = ::v1023;
  LODWORD(::v1023) = 0;
  v935.m128i_i64[1] = qword_18026FAF8("UnityEngine.Object::Destroy");
  if ( !DWORD1(::v1023) )
    goto LABEL_1096;
  if ( !DWORD2(::v1023) )
    goto LABEL_1096;
  qword_18026FA68 = qword_18026FAF8("UnityEngine.SkinnedMeshRenderer::get_updateWhenOffscreen");
  qword_18026FA70 = qword_18026FAF8("UnityEngine.SkinnedMeshRenderer::set_updateWhenOffscreen");
  qword_18026FA38 = qword_18026FAF8("UnityEngine.SkinnedMeshRenderer::get_sharedMesh");
  qword_18026FA50 = qword_18026FAF8("UnityEngine.SkinnedMeshRenderer::set_sharedMesh");
  qword_18026FA40 = qword_18026FAF8("UnityEngine.Renderer::get_shadowProxyMesh");
  qword_18026FA48 = qword_18026FAF8("UnityEngine.Renderer::set_shadowProxyMesh");
  v1026 = 0;
  v1027 = 0;
  v2 = (__int64 (__fastcall *)(_QWORD))qword_18026FAF8("UnityEngine.SkinnedMeshRenderer::get_bones");
  qword_18026FA58 = v2;
  v3 = (__m128)_mm_cmpeq_epi32(
                 _mm_unpacklo_epi64(
                   _mm_loadl_epi64((const __m128i *)&qword_18026FA40),
                   _mm_loadl_epi64((const __m128i *)&qword_18026FA48)),
                 (__m128i)0LL);
  v4 = (__m128)_mm_cmpeq_epi32(
                 _mm_unpacklo_epi64(
                   _mm_loadl_epi64((const __m128i *)&qword_18026FA38),
                   _mm_loadl_epi64((const __m128i *)&qword_18026FA50)),
                 (__m128i)0LL);
  if ( _mm_movemask_ps(_mm_and_ps(_mm_shuffle_ps(v4, v3, 136), _mm_shuffle_ps(v4, v3, 221)))
    || !v2
    || !qword_18026FA68
    || !qword_18026FA70 )
  {
LABEL_1096:
    v846 = 0;
    *(_QWORD *)&v982[0] = "Renderer binding API unavailable";
    BYTE8(v982[0]) = 1;
    sub_18017B450(v982, &v846);
    p_??_7runtime_error@std@@6B@ = &std::runtime_error::`vftable';
    v1026 = 0;
    v1027 = 0;
    sub_180179A30(&p_??_7runtime_error@std@@6B@, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  v1026 = 0;
  v1027 = 0;
  v5 = qword_18026F300();
  if ( v5 )
  {
    *(_QWORD *)&v982[0] = 0;
    v1026 = 0;
    v1027 = 0;
    v6 = qword_18026F310(v5, v982);
    v1001_1 = 0;
    if ( v6 && *(_QWORD *)&v982[0] )
    {
      v8 = 0;
      while ( 1 )
      {
        v9 = *(_QWORD *)(v6 + 8 * v8);
        v1026 = 0;
        v1027 = 0;
        v1001_2 = qword_18026F318(v9);
        v1001_1 = v1001_2;
        if ( v1001_2 )
        {
          v1026 = 0;
          v1027 = 0;
          v11 = qword_18026F320(v1001_2);
          if ( v11 )
          {
            if ( sub_18017BA50(v11, "UnityEngine.CoreModule.dll") )
              break;
          }
        }
        if ( (unsigned __int64)++v8 >= *(_QWORD *)&v982[0] )
          goto LABEL_17;
      }
    }
  }
  else
  {
LABEL_17:
    v1001_1 = 0;
  }
  v1026 = 0;
  v1027 = 0;
  v1001 = v1001_1;
  v12 = qword_18026F328(v1001_1, "UnityEngine", "Mesh");
  v1026 = 0;
  v1027 = 0;
  v933.m128i_i64[1] = qword_18026FAF8("UnityEngine.Object::Internal_CloneSingle");
  v1026 = 0;
  v1027 = 0;
  v934.m128i_i64[0] = qword_18026FAF8("UnityEngine.Mesh::SetVertexBufferParamsFromPtr");
  v1026 = 0;
  v1027 = 0;
  v933.m128i_i64[0] = qword_18026FAF8("UnityEngine.Mesh::InternalSetVertexBufferData");
  v1026 = 0;
  v1027 = 0;
  v947.m128i_i64[0] = qword_18026FAF8("UnityEngine.Mesh::SetIndexBufferParams");
  v1026 = 0;
  v1027 = 0;
  v934.m128i_i64[1] = qword_18026FAF8("UnityEngine.Mesh::InternalSetIndexBufferData");
  v1026 = 0;
  v1027 = 0;
  v1010.m128i_i64[0] = qword_18026FAF8("UnityEngine.Mesh::GetSubMesh_Injected");
  v1026 = 0;
  v1027 = 0;
  v932.m128i_i64[0] = qword_18026FAF8("UnityEngine.Mesh::SetSubMesh_Injected");
  v1026 = 0;
  v1027 = 0;
  v947.m128i_i64[1] = qword_18026FAF8("UnityEngine.Mesh::get_bounds_Injected");
  v1026 = 0;
  v1027 = 0;
  v935.m128i_i64[0] = qword_18026FAF8("UnityEngine.Mesh::set_bounds_Injected");
  v1026 = 0;
  v1027 = 0;
  v932.m128i_i64[1] = qword_18026FAF8("UnityEngine.Mesh::GetIndexBufferImpl");
  v1026 = 0;
  v1027 = 0;
  v957 = (__int64 (__fastcall *)(__int64, _QWORD))qword_18026FAF8("UnityEngine.Mesh::GetVertexBufferStride");
  v13 = _mm_cmpeq_epi32(
          _mm_unpacklo_epi64(_mm_loadl_epi64((const __m128i *)&v934.m128i_u64[1]), _mm_loadl_epi64(&v1010)),
          (__m128i)0LL);
  v14 = _mm_cmpeq_epi32(_mm_unpacklo_epi64(_mm_loadl_epi64(&v933), _mm_loadl_epi64(&v947)), (__m128i)0LL);
  v15 = _mm_cmpeq_epi32(
          _mm_unpacklo_epi64(_mm_loadl_epi64((const __m128i *)&v933.m128i_u64[1]), _mm_loadl_epi64(&v934)),
          (__m128i)0LL);
  v16 = _mm_cmpeq_epi32(
          _mm_unpacklo_epi64(_mm_loadl_epi64((const __m128i *)&v935.m128i_u64[1]), (__m128i)(unsigned __int64)v12),
          (__m128i)0LL);
  v17 = (__m128)_mm_packs_epi32(
                  _mm_xor_si128(
                    _mm_packs_epi32(
                      _mm_and_si128(_mm_shuffle_epi32(v16, 177), v16),
                      _mm_and_si128(_mm_shuffle_epi32(v15, 177), v15)),
                    (__m128i)-1LL),
                  _mm_xor_si128(
                    _mm_packs_epi32(
                      _mm_and_si128(_mm_shuffle_epi32(v14, 177), v14),
                      _mm_and_si128(_mm_shuffle_epi32(v13, 177), v13)),
                    (__m128i)-1LL));
  v18 = _mm_cmpeq_epi32(
          _mm_unpacklo_epi64(_mm_loadl_epi64(&v932), _mm_loadl_epi64((const __m128i *)&v947.m128i_u64[1])),
          (__m128i)0LL);
  v19 = _mm_cmpeq_epi32(
          _mm_unpacklo_epi64(_mm_loadl_epi64(&v935), _mm_loadl_epi64((const __m128i *)&v932.m128i_u64[1])),
          (__m128i)0LL);
  v20 = (__m128i)_mm_shuffle_ps(
                   (__m128)_mm_and_si128(
                             _mm_unpacklo_epi32(
                               _mm_shufflelo_epi16(
                                 _mm_xor_si128(
                                   _mm_and_si128(_mm_shuffle_epi32(v18, 232), _mm_shuffle_epi32(v18, 189)),
                                   (__m128i)-1LL),
                                 232),
                               _mm_shufflelo_epi16(
                                 _mm_xor_si128(
                                   _mm_and_si128(_mm_shuffle_epi32(v19, 232), _mm_shuffle_epi32(v19, 189)),
                                   (__m128i)-1LL),
                                 232)),
                             (__m128i)v17),
                   v17,
                   228);
  if ( (unsigned __int8)_mm_movemask_epi8(_mm_packs_epi16(v20, v20)) != 0xFF || !v957 )
  {
    v848 = 0;
    *(_QWORD *)&v982[0] = "Required mesh API absent";
    BYTE8(v982[0]) = 1;
    sub_18017B450(v982, &v848);
    p_??_7runtime_error@std@@6B@_1 = &std::runtime_error::`vftable';
    v1026 = 0;
    v1027 = 0;
    sub_180179A30(&p_??_7runtime_error@std@@6B@_1, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  v1026 = 0;
  v1027 = 0;
  v21 = qword_18026F598(v1022);
  v1026 = 0;
  v1027 = 0;
  v22 = qword_18026F328(v1001, "UnityEngine", "Mesh");
  if ( !v22
    || (v1026 = 0, v1027 = 0, (v23 = qword_18026F330(v22, "get_vertexCount", 0)) == 0)
    || (*(_QWORD *)&v982[0] = 0, v1026 = 0, v1027 = 0, (v24 = qword_18026F348(v23, v21, 0, v982)) == 0)
    || *(_QWORD *)&v982[0] )
  {
    *(_QWORD *)&v982[0] = &std::exception::`vftable';
    *(_OWORD *)((char *)v982 + 8) = 0;
    v1011[0].m128i_i64[0] = (__int64)"Missing mesh property";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, (char *)v982 + 8);
    *(_QWORD *)&v982[0] = &std::runtime_error::`vftable';
    v1026 = 0;
    v1027 = 0;
    sub_180179A30(v982, &_TI2_AVruntime_error_std__);
LABEL_1091:
    *(_QWORD *)&v982[0] = &std::exception::`vftable';
    *(_OWORD *)((char *)v982 + 8) = 0;
    v1011[0].m128i_i64[0] = (__int64)"Missing mesh property";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, (char *)v982 + 8);
    *(_QWORD *)&v982[0] = &std::runtime_error::`vftable';
    v1026 = 0;
    v1027 = 0;
    sub_180179A30(v982, &_TI2_AVruntime_error_std__);
LABEL_1092:
    *(_QWORD *)&v982[0] = &std::exception::`vftable';
    *(_OWORD *)((char *)v982 + 8) = 0;
    v1011[0].m128i_i64[0] = (__int64)"Missing mesh property";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, (char *)v982 + 8);
    *(_QWORD *)&v982[0] = &std::runtime_error::`vftable';
    v1026 = 0;
    v1027 = 0;
    sub_180179A30(v982, &_TI2_AVruntime_error_std__);
LABEL_1093:
    *(_QWORD *)&v982[0] = &std::exception::`vftable';
    *(_OWORD *)((char *)v982 + 8) = 0;
    v1011[0].m128i_i64[0] = (__int64)"Missing mesh property";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, (char *)v982 + 8);
    *(_QWORD *)&v982[0] = &std::runtime_error::`vftable';
    v1026 = 0;
    v1027 = 0;
    sub_180179A30(v982, &_TI2_AVruntime_error_std__);
LABEL_1094:
    *(_QWORD *)&v982[0] = &std::exception::`vftable';
    *(_OWORD *)((char *)v982 + 8) = 0;
    v1011[0].m128i_i64[0] = (__int64)"Missing mesh property";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, (char *)v982 + 8);
    *(_QWORD *)&v982[0] = &std::runtime_error::`vftable';
    v1026 = 0;
    v1027 = 0;
    sub_180179A30(v982, &_TI2_AVruntime_error_std__);
LABEL_1095:
    p_??_7exception@std@@6B@ = &std::exception::`vftable';
    v866 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Short vertex stream";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v866);
    p_??_7exception@std@@6B@ = &std::runtime_error::`vftable';
    v1000 = 0;
    v999 = 0;
    sub_180179A30(&p_??_7exception@std@@6B@, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  v1026 = 0;
  v1027 = 0;
  n0x93_1 = *(unsigned int *)qword_18026F350(v24);
  v1026 = 0;
  v1027 = 0;
  v25 = qword_18026F598(v1022);
  v1026 = 0;
  v1027 = 0;
  v26 = qword_18026F328(v1001, "UnityEngine", "Mesh");
  if ( !v26 )
    goto LABEL_1091;
  v1026 = 0;
  v1027 = 0;
  v27 = qword_18026F330(v26, "get_vertexAttributeCount", 0);
  if ( !v27 )
    goto LABEL_1091;
  *(_QWORD *)&v982[0] = 0;
  v1026 = 0;
  v1027 = 0;
  v28 = qword_18026F348(v27, v25, 0, v982);
  if ( !v28 || *(_QWORD *)&v982[0] )
    goto LABEL_1091;
  v1026 = 0;
  v1027 = 0;
  *(_QWORD *)&v1025 = *(unsigned int *)qword_18026F350(v28);
  v1026 = 0;
  v1027 = 0;
  v29 = qword_18026F598(v1022);
  v1026 = 0;
  v1027 = 0;
  v30 = qword_18026F328(v1001, "UnityEngine", "Mesh");
  if ( !v30 )
    goto LABEL_1092;
  v1026 = 0;
  v1027 = 0;
  v31 = qword_18026F330(v30, "get_subMeshCount", 0);
  if ( !v31 )
    goto LABEL_1092;
  *(_QWORD *)&v982[0] = 0;
  v1026 = 0;
  v1027 = 0;
  v32 = qword_18026F348(v31, v29, 0, v982);
  if ( !v32 || *(_QWORD *)&v982[0] )
    goto LABEL_1092;
  v1026 = 0;
  v1027 = 0;
  n0x56 = *(unsigned int *)qword_18026F350(v32);
  v1026 = 0;
  v1027 = 0;
  v33 = qword_18026F598(v1022);
  v1026 = 0;
  v1027 = 0;
  v34 = qword_18026F328(v1001, "UnityEngine", "Mesh");
  if ( !v34 )
    goto LABEL_1093;
  v1026 = 0;
  v1027 = 0;
  v35 = qword_18026F330(v34, "get_indexFormat", 0);
  if ( !v35 )
    goto LABEL_1093;
  *(_QWORD *)&v982[0] = 0;
  v1026 = 0;
  v1027 = 0;
  v36 = qword_18026F348(v35, v33, 0, v982);
  if ( !v36 || *(_QWORD *)&v982[0] )
    goto LABEL_1093;
  v1026 = 0;
  v1027 = 0;
  v1007 = *(_DWORD *)qword_18026F350(v36);
  v1026 = 0;
  v1027 = 0;
  v37 = qword_18026F598(v1022);
  v1026 = 0;
  v1027 = 0;
  v38 = qword_18026F328(v1001, "UnityEngine", "Mesh");
  if ( !v38 )
    goto LABEL_1094;
  v1026 = 0;
  v1027 = 0;
  v39 = qword_18026F330(v38, "get_blendShapeCount", 0);
  if ( !v39 )
    goto LABEL_1094;
  *(_QWORD *)&v982[0] = 0;
  v1026 = 0;
  v1027 = 0;
  v40 = qword_18026F348(v39, v37, 0, v982);
  if ( !v40 || *(_QWORD *)&v982[0] )
    goto LABEL_1094;
  v1026 = 0;
  v1027 = 0;
  v41 = (int *)qword_18026F350(v40);
  if ( (unsigned int)(n0x93_1 - 200001) < 0xFFFCF2C0
    || (unsigned int)(v1025 - 17) < 0xFFFFFFF0
    || (unsigned int)(n0x56 - 65) < 0xFFFFFFC0
    || v1007 >= 2 )
  {
    v850 = 0;
    *(_QWORD *)&v982[0] = "Unsupported detached mesh shape";
    BYTE8(v982[0]) = 1;
    sub_18017B450(v982, &v850);
    p_??_7runtime_error@std@@6B@_2 = &std::runtime_error::`vftable';
    v1026 = 0;
    v1027 = 0;
    sub_180179A30(&p_??_7runtime_error@std@@6B@_2, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  v945 = *v41;
  *(_OWORD *)lpMem = 0;
  v959 = 0;
  v42 = 0;
  for ( i = 0; ; i = v1012.m128i_i32[0] + 1 )
  {
    v1012.m128i_i32[0] = i;
    if ( i >= (int)v1025 )
      break;
    v1011[0].m128i_i64[0] = (__int64)&v1012;
    v1024 = 0;
    v1023 = 0;
    v44 = qword_18026F598(v1022);
    v1024 = 0;
    v1023 = 0;
    v45 = qword_18026F328(v1001, "UnityEngine", "Mesh");
    if ( !v45
      || (v1024 = 0, v1023 = 0, (v46 = qword_18026F330(v45, "GetVertexAttribute", 1)) == 0)
      || (*(_QWORD *)&v982[0] = 0, v1024 = 0, v1023 = 0, (v47 = qword_18026F348(v46, v44, v1011, v982)) == 0)
      || *(_QWORD *)&v982[0] )
    {
      p_??_7exception@std@@6B@_1 = &std::exception::`vftable';
      v852 = 0;
      *(_QWORD *)&v982[0] = "Missing attribute";
      BYTE8(v982[0]) = 1;
      sub_18017B450(v982, &v852);
      p_??_7exception@std@@6B@_1 = &std::runtime_error::`vftable';
      v1024 = 0;
      v1023 = 0;
      sub_180179A30(&p_??_7exception@std@@6B@_1, &_TI2_AVruntime_error_std__);
      goto LABEL_1136;
    }
    v1024 = 0;
    v1023 = 0;
    v48 = (const __m128i *)qword_18026F350(v47);
    v49 = v48;
    v50 = (char *)lpMem[1];
    if ( lpMem[1] == v959 )
    {
      v51 = lpMem[0];
      n0x1000 = (char *)lpMem[1] - (char *)lpMem[0];
      n0x100_2 = (((char *)lpMem[1] - (char *)lpMem[0]) >> 4) + 1;
      v54 = (unsigned __int64)(((char *)lpMem[1] - (char *)lpMem[0]) >> 4) >> 1;
      v55 = 0xFFFFFFFFFFFFFFFLL - v54;
      n0x100 = (((char *)lpMem[1] - (char *)lpMem[0]) >> 4) + v54;
      if ( n0x100 <= n0x100_2 )
        n0x100 = (n0x1000 >> 4) + 1;
      if ( ((char *)lpMem[1] - (char *)lpMem[0]) >> 4 > v55 )
        n0x100 = 0xFFFFFFFFFFFFFFFLL;
      if ( n0x100 >> 60 )
      {
        v1024 = 0;
        v1023 = 0;
        sub_18002D1E0(n0x100 >> 60, v55, 0xFFFFFFFFFFFFFFFLL);
      }
      v57 = 2 * n0x100;
      if ( n0x100 )
      {
        if ( n0x100 < 0x100 )
        {
          v1024 = 0;
          v1023 = 0;
          v59 = (_QWORD *)sub_1800FFF00(16 * n0x100, v55, 0xFFFFFFFFFFFFFFFLL);
        }
        else
        {
          if ( n0x100 >= 0xFFFFFFFFFFFFFFELL )
          {
            v1024 = 0;
            v1023 = 0;
            sub_18002D1E0(0xFFFFFFFFFFFFFFELL, v55, 0xFFFFFFFFFFFFFFFLL);
          }
          v1024 = 0;
          v1023 = 0;
          v58 = sub_1800FFF00(v57 * 8 + 39, v55, 0xFFFFFFFFFFFFFFFLL);
          v59 = (_QWORD *)((v58 + 39) & 0xFFFFFFFFFFFFFFE0uLL);
          *(v59 - 1) = v58;
        }
      }
      else
      {
        v59 = 0;
      }
      *(__m128i *)((char *)v59 + n0x1000) = _mm_loadu_si128(v49);
      sub_180200FC0(v59, v51, n0x1000);
      if ( v51 )
      {
        if ( (unsigned __int64)n0x1000 >= 0x1000 )
        {
          if ( (unsigned __int64)v51 - *(v51 - 1) - 8 >= 0x20 )
            goto LABEL_109;
          v51 = (_QWORD *)*(v51 - 1);
        }
        sub_1800FFFE0(v51);
      }
      lpMem[0] = v59;
      v42 = (int *)&v59[2 * n0x100_2];
      lpMem[1] = v42;
      v959 = (char *)&v59[v57];
    }
    else
    {
      *(__m128i *)lpMem[1] = _mm_loadu_si128(v48);
      lpMem[1] = v50 + 16;
      v42 = (int *)(v50 + 16);
    }
  }
  v979 = 0;
  v60 = (int *)lpMem[0];
  v61 = (int *)lpMem[0];
  while ( v61 != v42 )
  {
    n4 = v61[3];
    if ( n4 >= 4 )
    {
      p_??_7exception@std@@6B@_2 = &std::exception::`vftable';
      v854 = 0;
      *(_QWORD *)&v982[0] = "Invalid vertex stream";
      BYTE8(v982[0]) = 1;
      sub_18017B450(v982, &v854);
      p_??_7exception@std@@6B@_2 = &std::runtime_error::`vftable';
      v1024 = 0;
      v1023 = 0;
      sub_180179A30(&p_??_7exception@std@@6B@_2, &_TI2_AVruntime_error_std__);
      goto LABEL_1136;
    }
    v1024 = 0;
    v1023 = 0;
    v63 = qword_18026F598(v1022);
    v1024 = 0;
    v1023 = 0;
    v64 = v957(v63, n4);
    *((_DWORD *)&v979 + v61[3]) = v64;
    v61 += 4;
    if ( (unsigned int)(v64 - 257) <= 0xFFFFFEFF )
    {
      p_??_7exception@std@@6B@_3 = &std::exception::`vftable';
      v856 = 0;
      *(_QWORD *)&v982[0] = "Invalid stride";
      BYTE8(v982[0]) = 1;
      sub_18017B450(v982, &v856);
      p_??_7exception@std@@6B@_3 = &std::runtime_error::`vftable';
      v1024 = 0;
      v1023 = 0;
      sub_180179A30(&p_??_7exception@std@@6B@_3, &_TI2_AVruntime_error_std__);
      goto LABEL_1136;
    }
  }
  n4_2 = 0;
  if ( byte_18026FB59 || (v1025_14 = (_BYTE *)v1025_0, v1025_0 == (_QWORD)::v1025) )
  {
LABEL_79:
    v1006 = 0;
    var150.m128_u64[0] = 0;
    LODWORD(v1025) = 0;
    v1002 = 0;
    v1010.m128i_i32[2] = 0;
    v1020_2 = 0;
    v1004 = 0;
    v1020 = 0;
    v989 = 0;
    n0x93_8 = 0;
  }
  else
  {
    while ( *v1025_14 == 1 )
    {
      if ( ++v1025_14 == (_BYTE *)::v1025 )
        goto LABEL_79;
    }
    if ( v60 == v42 )
    {
LABEL_168:
      *(_QWORD *)&v982[0] = &std::exception::`vftable';
      *(_OWORD *)((char *)v982 + 8) = 0;
      v1011[0].m128i_i64[0] = (__int64)"Missing skin/position attribute";
      v1011[0].m128i_i8[8] = 1;
      sub_18017B450(v1011, (char *)v982 + 8);
      *(_QWORD *)&v982[0] = &std::runtime_error::`vftable';
      v1024 = 0;
      v1023 = 0;
      sub_180179A30(v982, &_TI2_AVruntime_error_std__);
    }
    else
    {
      v109 = v60;
      while ( *v109 )
      {
        v109 += 4;
        if ( v109 == v42 )
          goto LABEL_168;
      }
    }
    v110 = v109[1];
    n3 = v109[2];
    v1004 = v109[3];
    v112 = v60;
    while ( *v112 != 12 )
    {
      v112 += 4;
      if ( v112 == v42 )
      {
        *(_QWORD *)&v982[0] = &std::exception::`vftable';
        *(_OWORD *)((char *)v982 + 8) = 0;
        v1011[0].m128i_i64[0] = (__int64)"Missing skin/position attribute";
        v1011[0].m128i_i8[8] = 1;
        sub_18017B450(v1011, (char *)v982 + 8);
        *(_QWORD *)&v982[0] = &std::runtime_error::`vftable';
        v1024 = 0;
        v1023 = 0;
        n3 = sub_180179A30(v982, &_TI2_AVruntime_error_std__);
        break;
      }
    }
    n4_1 = v112[1];
    v1010.m128i_i32[2] = v112[2];
    v1002 = v112[3];
    while ( *v60 != 13 )
    {
      v60 += 4;
      if ( v60 == v42 )
      {
        *(_QWORD *)&v982[0] = &std::exception::`vftable';
        *(_OWORD *)((char *)v982 + 8) = 0;
        v1011[0].m128i_i64[0] = (__int64)"Missing skin/position attribute";
        v1011[0].m128i_i8[8] = 1;
        sub_18017B450(v1011, (char *)v982 + 8);
        *(_QWORD *)&v982[0] = &std::runtime_error::`vftable';
        v1024 = 0;
        v1023 = 0;
        n3 = sub_180179A30(v982, &_TI2_AVruntime_error_std__);
        break;
      }
    }
    if ( v110
      || n3 < 3
      || (n4_1 & 0xFFFFFFFB) != 0
      || v1010.m128i_i32[2] <= 0
      || v1010.m128i_i32[2] > 4u
      || (LODWORD(v1025) = v60[1], (v1025 & 0xFFFFFFFD) != 8 && (_DWORD)v1025 != 6)
      || (v114 = v60[2], v114 < v1010.m128i_i32[2]) )
    {
      p_??_7exception@std@@6B@_4 = &std::exception::`vftable';
      v858 = 0;
      *(_QWORD *)&v982[0] = "Unsupported skin encoding";
      BYTE8(v982[0]) = 1;
      sub_18017B450(v982, &v858);
      p_??_7exception@std@@6B@_4 = &std::runtime_error::`vftable';
      v1024 = 0;
      v1023 = 0;
      sub_180179A30(&p_??_7exception@std@@6B@_4, &_TI2_AVruntime_error_std__);
      goto LABEL_1136;
    }
    var150.m128_u64[0] = v60[3];
    v1024 = 0;
    v1023 = 0;
    v115 = (__int64 (__fastcall *)(__int64, __int64))qword_18026FAF8("UnityEngine.Mesh::GetVertexAttributeOffset");
    if ( !v115 )
    {
      p_??_7exception@std@@6B@_5 = &std::exception::`vftable';
      v860 = 0;
      *(_QWORD *)&v982[0] = "Attribute offset API absent";
      BYTE8(v982[0]) = 1;
      sub_18017B450(v982, &v860);
      p_??_7exception@std@@6B@_5 = &std::runtime_error::`vftable';
      v1024 = 0;
      v1023 = 0;
      sub_180179A30(&p_??_7exception@std@@6B@_5, &_TI2_AVruntime_error_std__);
      goto LABEL_1136;
    }
    v1024 = 0;
    v1023 = 0;
    v116 = qword_18026F598(v1022);
    v1024 = 0;
    v1023 = 0;
    n0x93_17 = v115(v116, 0);
    v1024 = 0;
    v1023 = 0;
    v118 = qword_18026F598(v1022);
    v1024 = 0;
    v1023 = 0;
    v119 = v115(v118, 12);
    v1024 = 0;
    v1023 = 0;
    v120 = qword_18026F598(v1022);
    v1024 = 0;
    v1023 = 0;
    LODWORD(v1020_1) = v115(v120, 13);
    if ( (int)(v1020_1 | n0x93_17 | v119) < 0
      || (signed int)(n0x93_17 + 12) > *((_DWORD *)&v979 + v1004)
      || (signed int)(v119 + (v1010.m128i_i32[2] << ((n4_1 != 4) + 1))) > *((_DWORD *)&v979 + v1002) )
    {
      goto LABEL_1113;
    }
    v122 = 2 - ((_DWORD)v1025 == 8);
    if ( (_DWORD)v1025 == 6 )
      v122 = 0;
    if ( (int)v1020_1 + (v114 << v122) > *((_DWORD *)&v979 + var150.m128_u64[0]) )
    {
LABEL_1113:
      p_??_7exception@std@@6B@_6 = &std::exception::`vftable';
      v862 = 0;
      *(_QWORD *)&v982[0] = "Attribute exceeds stride";
      BYTE8(v982[0]) = 1;
      sub_18017B450(v982, &v862);
      p_??_7exception@std@@6B@_6 = &std::runtime_error::`vftable';
      v1024 = 0;
      v1023 = 0;
      sub_180179A30(&p_??_7exception@std@@6B@_6, &_TI2_AVruntime_error_std__);
      goto LABEL_1136;
    }
    n0x93_8 = n0x93_17;
    v989 = v119;
    v1020_1 = (unsigned int)v1020_1;
    v1020 = (unsigned int)v1020_1;
    LOBYTE(v1020_1) = n4_1 == 4;
    v1020_2 = v1020_1;
    LOBYTE(v1020_1) = 1;
    v1006 = v1020_1;
  }
  v986 = 0;
  v985 = 0;
  v984 = 0;
  v983 = 0;
  memset(v982, 0, sizeof(v982));
  p_p_n0x93 = &p_n0x93;
  while ( n4_2 < 4 )
  {
    if ( *((_DWORD *)&v979 + n4_2) )
    {
      v988 = 0;
      v987 = 0;
      sub_18002CCA0(&v1012, n4_2);
      v69 = v953;
      if ( v953.m128i_i64[1] - v953.m128i_i64[0] >= 7uLL )
      {
        p_p_n0x93_1 = p_p_n0x93;
        v953.m128i_i64[0] += 7;
        p_v1012_1 = (__m128i *)v1012.m128i_i64[0];
        p_v1012 = &v1012;
        if ( v69.m128i_i64[1] >= 0x10uLL )
          p_v1012 = (__m128i *)v1012.m128i_i64[0];
        n7 = 7;
        if ( p_v1012 < (__m128i *)"" && &p_v1012->m128i_i8[v69.m128i_i64[0]] >= "-stream" )
        {
          n7 = (char *)p_v1012 - "-stream";
          if ( p_v1012 <= (__m128i *)"-stream" )
            n7 = 0;
        }
        if ( v69.m128i_i64[1] < 0x10uLL )
          p_v1012_1 = &v1012;
        sub_180200FC0((char *)&p_v1012_1->m128i_u32[1] + 3, p_v1012, v69.m128i_i64[0] + 1);
        sub_180200FC0(p_v1012, "-stream", n7);
        sub_180200FC0(&p_v1012->m128i_i8[n7], &aStream[n7 + 7], 7 - n7);
        p_p_n0x93 = p_p_n0x93_1;
      }
      else
      {
        sub_180067E30(&v1012, 7, "-stream", 7);
      }
      v74 = _mm_loadu_si128(&v1012);
      v1011[1] = _mm_loadu_si128(&v953);
      v1011[0] = v74;
      v953.m128i_i64[0] = 0;
      v953.m128i_i64[1] = 15;
      v1012.m128i_i8[0] = 0;
      sub_1800BE540(p_p_n0x93, v1011);
      v75 = (__m128i *)((char *)&v850 + 24 * n4_2 + 1648);
      v76 = (_QWORD *)*((_QWORD *)v982 + 3 * n4_2);
      if ( v76 )
      {
        if ( v75[1].m128i_i64[0] - (__int64)v76 >= 0x1000uLL )
        {
          if ( (unsigned __int64)v76 - *(v76 - 1) - 8 >= 0x20 )
            goto LABEL_109;
          v76 = (_QWORD *)*(v76 - 1);
        }
        sub_1800FFFE0(v76);
      }
      *v75 = _mm_load_si128(&p_n0x93);
      v75[1].m128i_i64[0] = v998;
      if ( v1011[1].m128i_i64[1] >= 0x10uLL )
      {
        v77 = (void *)v1011[0].m128i_i64[0];
        if ( (unsigned __int64)(v1011[1].m128i_i64[1] + 1) >= 0x1000 )
        {
          if ( (unsigned __int64)(v1011[0].m128i_i64[0] - 8 - *(_QWORD *)(v1011[0].m128i_i64[0] - 8)) >= 0x20 )
            goto LABEL_109;
          v77 = *(void **)(v1011[0].m128i_i64[0] - 8);
        }
        sub_1800FFFE0(v77);
      }
      if ( v953.m128i_i64[1] >= 0x10uLL )
      {
        v68 = (void *)v1012.m128i_i64[0];
        if ( (unsigned __int64)(v953.m128i_i64[1] + 1) >= 0x1000 )
        {
          if ( (unsigned __int64)(v1012.m128i_i64[0] - 8 - *(_QWORD *)(v1012.m128i_i64[0] - 8)) >= 0x20 )
            goto LABEL_109;
          v68 = *(void **)(v1012.m128i_i64[0] - 8);
        }
        sub_1800FFFE0(v68);
      }
    }
    ++n4_2;
  }
  v1007_1 = v1007;
  v79 = v1007 != 0;
  v1011[1].m128i_i64[0] = 8;
  v1011[1].m128i_i64[1] = 15;
  v1011[0] = (__m128i)0x73656369646E692DuLL;
  sub_1800BE540(&n0x93, v1011);
  n0x93_18 = n0x93_1;
  if ( n0x93.m128i_i64[0] == n0x93.m128i_i64[1] )
    goto LABEL_127;
  v81 = n0x93.m128i_i64[1] - n0x93.m128i_i64[0];
  n12 = 12;
  if ( !v1007_1 )
    n12 = 6;
  if ( HIDWORD(v81) )
  {
    n0x93_2 = v81 % n12;
    if ( !(v81 % n12) )
      goto LABEL_115;
LABEL_127:
    p_??_7exception@std@@6B@_7 = &std::exception::`vftable';
    v864 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Invalid index byte count";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v864);
    p_??_7exception@std@@6B@_7 = &std::runtime_error::`vftable';
    v1000 = 0;
    v999 = 0;
    sub_180179A30(&p_??_7exception@std@@6B@_7, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  n0x93_2 = (unsigned int)v81 % (unsigned int)n12;
  if ( (_DWORD)n0x93_2 )
    goto LABEL_127;
LABEL_115:
  if ( (_DWORD)v979 && *((_QWORD *)&v982[0] + 1) - *(_QWORD *)&v982[0] < n0x93_1 * (int)v979
    || DWORD1(v979) && (_QWORD)v983 - *((_QWORD *)&v982[1] + 1) < n0x93_1 * SDWORD1(v979)
    || DWORD2(v979) && *((_QWORD *)&v984 + 1) - (_QWORD)v984 < n0x93_1 * SDWORD2(v979)
    || HIDWORD(v979) && (_QWORD)v986 - *((_QWORD *)&v985 + 1) < n0x93_1 * SHIDWORD(v979) )
  {
    goto LABEL_1095;
  }
  p_m128i_i64_5 = 0;
  v951 = 0;
  LOBYTE(v976) = v79;
  if ( !(_DWORD)n0x93_1 )
  {
    p_m128i_i64_1 = 0;
    goto LABEL_216;
  }
  v84 = 28 * n0x93_1;
  v1000 = 0;
  v999 = 0;
  if ( (unsigned int)n0x93_1 < 0x93 )
  {
    p_m128i_i64_1 = sub_1800FFF00(28 * n0x93_1, n0x93_2, n0x93_1);
  }
  else
  {
    v85 = sub_1800FFF00(v84 + 39, n0x93_2, n0x93_1);
    p_m128i_i64_1 = (v85 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
    *(_QWORD *)(p_m128i_i64_1 - 8) = v85;
  }
  *(_QWORD *)&p_m128i_i64_5 = p_m128i_i64_1;
  v951 = p_m128i_i64_1 + v84;
  sub_180201670(p_m128i_i64_1, 0, v84);
  *((_QWORD *)&p_m128i_i64_5 + 1) = p_m128i_i64_1 + v84;
  if ( !(_BYTE)v1006 )
  {
    n0x93_2 = n0x93_1;
    v107 = n0x93_1 & 7;
    v1007_1 = v1007;
    if ( (unsigned int)n0x93_1 < 8 )
    {
      v108 = 0;
LABEL_200:
      v124 = _mm_xor_si128(_mm_shuffle_epi32((__m128i)(v107 - 1), 68), (__m128i)xmmword_18020C960);
      v125 = _mm_shuffle_epi32(v124, 160);
      v126 = _mm_cmpeq_epi32(_mm_shuffle_epi32(v124, 245), (__m128i)xmmword_18020C960);
      v127 = _mm_and_si128(_mm_cmpgt_epi32(_mm_load_si128((const __m128i *)&xmmword_18021DC90), v125), v126);
      v128 = _mm_xor_si128(_mm_shufflelo_epi16(v127, 232), (__m128i)-1LL);
      v129 = _mm_packs_epi32(v128, v128);
      if ( (_mm_cvtsi128_si32(v129) & 1) != 0 )
        *(_WORD *)(p_m128i_i64_1 + 28 * v108 + 12) = -1;
      v130 = _mm_xor_si128(_mm_packs_epi32(v127, v127), (__m128i)-1LL);
      if ( (_mm_cvtsi128_si32(_mm_packs_epi32(v130, v130)) & 0x10000) != 0 )
        *(_WORD *)(p_m128i_i64_1 + 28 * v108 + 40) = -1;
      v131 = _mm_andnot_si128(_mm_cmpgt_epi32(v125, (__m128i)xmmword_18021DCA0), v126);
      v132 = _mm_xor_si128(_mm_packs_epi32(v129, v131), (__m128i)-1LL);
      if ( (_mm_extract_epi16(_mm_packs_epi32(v132, v132), 2) & 1) != 0 )
        *(_WORD *)(p_m128i_i64_1 + 28 * v108 + 68) = -1;
      v133 = _mm_xor_si128(_mm_shufflehi_epi16(v131, 132), (__m128i)-1LL);
      if ( (_mm_extract_epi16(_mm_packs_epi32(v133, v133), 3) & 1) != 0 )
        *(_WORD *)(p_m128i_i64_1 + 28 * v108 + 96) = -1;
      v134 = _mm_andnot_si128(_mm_cmpgt_epi32(v125, (__m128i)xmmword_18021DCB0), v126);
      v135 = _mm_xor_si128(_mm_shufflelo_epi16(v134, 232), (__m128i)-1LL);
      if ( (_mm_extract_epi16(_mm_packs_epi32(v135, v135), 4) & 1) != 0 )
        *(_WORD *)(p_m128i_i64_1 + 28 * v108 + 124) = -1;
      v136 = _mm_xor_si128(_mm_packs_epi32(v134, v134), (__m128i)-1LL);
      v137 = _mm_packs_epi32(v136, v136);
      if ( (_mm_extract_epi16(v137, 5) & 1) != 0 )
        *(_WORD *)(p_m128i_i64_1 + 28 * v108 + 152) = -1;
      v138 = _mm_andnot_si128(_mm_cmpgt_epi32(v125, (__m128i)xmmword_18021DCC0), v126);
      v139 = _mm_xor_si128(_mm_packs_epi32(v137, v138), (__m128i)-1LL);
      if ( (_mm_extract_epi16(_mm_packs_epi32(v139, v139), 6) & 1) != 0 )
        *(_WORD *)(p_m128i_i64_1 + 28 * v108 + 180) = -1;
      v140 = _mm_xor_si128(_mm_shufflehi_epi16(v138, 132), (__m128i)-1LL);
      if ( (_mm_extract_epi16(_mm_packs_epi32(v140, v140), 7) & 1) != 0 )
        *(_WORD *)(p_m128i_i64_1 + 28 * v108 + 208) = -1;
      goto LABEL_216;
    }
    if ( (n0x93_1 & 0x3FFF8) == 8 )
    {
      v108 = 0;
    }
    else
    {
      n0x93_2 = (((n0x93_1 & 0x3FFF8) - 8) >> 3) + 1;
      n0x93_18 = n0x93_2 & 0xFFFFFFFFFFFFFFFEuLL;
      v123 = (_WORD *)(p_m128i_i64_1 + 432);
      v108 = 0;
      do
      {
        *(v123 - 210) = -1;
        *(v123 - 196) = -1;
        *(v123 - 182) = -1;
        *(v123 - 168) = -1;
        *(v123 - 154) = -1;
        *(v123 - 140) = -1;
        *(v123 - 126) = -1;
        *(v123 - 112) = -1;
        *(v123 - 98) = -1;
        *(v123 - 84) = -1;
        *(v123 - 70) = -1;
        *(v123 - 56) = -1;
        *(v123 - 42) = -1;
        *(v123 - 28) = -1;
        *(v123 - 14) = -1;
        *v123 = -1;
        v108 += 16;
        v123 += 224;
        n0x93_18 -= 2LL;
      }
      while ( n0x93_18 );
      if ( (n0x93_2 & 1) == 0 )
      {
LABEL_199:
        if ( !v107 )
          goto LABEL_216;
        goto LABEL_200;
      }
    }
    n0x93_2 = 28 * v108;
    *(_WORD *)(p_m128i_i64_1 + n0x93_2 + 12) = -1;
    *(_WORD *)(p_m128i_i64_1 + n0x93_2 + 40) = -1;
    *(_WORD *)(p_m128i_i64_1 + n0x93_2 + 68) = -1;
    *(_WORD *)(p_m128i_i64_1 + n0x93_2 + 96) = -1;
    *(_WORD *)(p_m128i_i64_1 + n0x93_2 + 124) = -1;
    *(_WORD *)(p_m128i_i64_1 + n0x93_2 + 152) = -1;
    *(_WORD *)(p_m128i_i64_1 + n0x93_2 + 180) = -1;
    *(_WORD *)(p_m128i_i64_1 + n0x93_2 + 208) = -1;
    v108 += 8;
    goto LABEL_199;
  }
  mm_1 = (__m128i *)((char *)&v850 + 24 * v1004 + 1648);
  *(_QWORD *)&v1028 = (char *)v982 + 24 * v1002;
  v87 = (__int64 *)v982 + 3 * var150.m128_u64[0];
  v1004 = *((int *)&v979 + v1004);
  v88 = v1010.m128i_u32[2];
  v89 = 0;
  n0x93_3 = 0;
  do
  {
    v91 = 28 * n0x93_3;
    v92 = mm_1->m128i_i64[0] + v1004 * n0x93_3;
    n0x93_18 = n0x93_8;
    n0x93_2 = *(unsigned int *)(n0x93_8 + v92 + 8);
    *(_DWORD *)(p_m128i_i64_1 + v91 + 8) = n0x93_2;
    *(_QWORD *)(p_m128i_i64_1 + v91) = *(_QWORD *)(n0x93_18 + v92);
    if ( v1010.m128i_i32[2] > 0 )
    {
      v93 = *((int *)&v979 + v1002);
      if ( (_BYTE)v1020_2 )
      {
        v94 = v1020 + n0x93_3 * *((int *)&v979 + var150.m128_u64[0]);
        v95 = v989 + n0x93_3 * v93;
        v96 = 0;
        while ( 1 )
        {
          *(_WORD *)(v89 + p_m128i_i64_1 + 2 * v96 + 12) = *(_WORD *)(v95 + **(_QWORD **)&v1028 + 2 * v96);
          v97 = v94 + *v87;
          if ( (_DWORD)v1025 == 6 )
            n0x93_2 = *(unsigned __int8 *)(v96 + v97);
          else
            n0x93_2 = (_DWORD)v1025 == 8 ? *(unsigned __int16 *)(v97 + 2 * v96) : *(unsigned int *)(v97 + 4 * v96);
          p_m128i_i64_1 = p_m128i_i64_5;
          n0x93_18 = p_m128i_i64_5 + v89;
          v98 = *(_WORD *)(p_m128i_i64_5 + v89 + 2 * v96 + 12);
          if ( v98 )
          {
            if ( (_QWORD)::v1025 - v1025_0 <= (unsigned __int64)(unsigned int)n0x93_2 )
              break;
          }
          if ( !v98 )
            n0x93_2 = 0;
          *(_WORD *)(n0x93_18 + 2 * v96++ + 20) = n0x93_2;
          if ( v88 == v96 )
            goto LABEL_132;
        }
      }
      else
      {
        v99 = v989 + n0x93_3 * v93;
        v100 = 0;
        while ( 1 )
        {
          v102 = _mm_cvtsi32_si128(*(_DWORD *)(v99 + **(_QWORD **)&v1028 + 4 * v100));
          v103 = _mm_cvtsi128_si32(v102);
          if ( v103 < 0 && (v103 & 0x7FFFFFFFu) - 0x800000 < 0x7F000000
            || v103 < 0 && (v103 & 0x7FFFFFFFu) - 1 < 0x7FFFFF
            || (v103 & 0x7FFFFFFFu) >= 0x7F800000
            || *(float *)v102.m128i_i32 > 1.0 )
          {
            p_??_7exception@std@@6B@_8 = &std::exception::`vftable';
            v868 = 0;
            v1011[0].m128i_i64[0] = (__int64)"Invalid float skin weight";
            v1011[0].m128i_i8[8] = 1;
            sub_18017B450(v1011, &v868);
            p_??_7exception@std@@6B@_8 = &std::runtime_error::`vftable';
            v1005 = 0;
            var1E0 = 0;
            sub_180179A30(&p_??_7exception@std@@6B@_8, &_TI2_AVruntime_error_std__);
            goto LABEL_1136;
          }
          *(_WORD *)(v89 + p_m128i_i64_1 + 2 * v100 + 12) = sub_1801B04B0();
          v104 = *v87;
          v105 = n0x93_3 * *((int *)&v979 + var150.m128_u64[0]);
          if ( (_DWORD)v1025 == 6 )
            n0x93_19 = *(unsigned __int8 *)(v100 + v105 + v1020 + v104);
          else
            n0x93_19 = (_DWORD)v1025 == 8
                     ? *(unsigned __int16 *)(v105 + v1020 + v104 + 2 * v100)
                     : *(_DWORD *)(v105 + v1020 + v104 + 4 * v100);
          p_m128i_i64_1 = p_m128i_i64_5;
          v106 = *(_WORD *)(p_m128i_i64_5 + v89 + 2 * v100 + 12);
          if ( v106 )
          {
            n0x93_18 = n0x93_19;
            if ( (_QWORD)::v1025 - v1025_0 <= (unsigned __int64)n0x93_19 )
              break;
          }
          v416 = v106 == 0;
          n0x93_2 = 0;
          if ( v416 )
            LOWORD(n0x93_19) = 0;
          *(_WORD *)(p_m128i_i64_5 + v89 + 2 * v100++ + 20) = n0x93_19;
          if ( v88 == v100 )
            goto LABEL_132;
        }
      }
      p_??_7exception@std@@6B@_9 = &std::exception::`vftable';
      v870 = 0;
      v1011[0].m128i_i64[0] = (__int64)"Bone index outside renderer palette";
      v1011[0].m128i_i8[8] = 1;
      sub_18017B450(v1011, &v870);
      p_??_7exception@std@@6B@_9 = &std::runtime_error::`vftable';
      v1005 = 0;
      var1E0 = 0;
      sub_180179A30(&p_??_7exception@std@@6B@_9, &_TI2_AVruntime_error_std__);
      goto LABEL_1136;
    }
LABEL_132:
    ++n0x93_3;
    v89 += 28;
    v1007_1 = v1007;
  }
  while ( n0x93_3 != n0x93_1 );
  v81 = n0x93.m128i_i64[1] - n0x93.m128i_i64[0];
LABEL_216:
  v1020_3 = 2LL - (v1007_1 == 0);
  n0x400_1 = v81 >> v1020_3;
  *(_OWORD *)v964 = 0;
  v965 = 0;
  v1020_2 = v1020_3;
  if ( n0x400_1 )
  {
    if ( n0x400_1 >> 62 )
    {
      v1005 = 0;
      var1E0 = 0;
      std::vector<void *>::_Xlen(v1020_3, n0x93_2, n0x93_18);
    }
    v143 = 4 * n0x400_1;
    if ( n0x400_1 < 0x400 )
    {
      v1005 = 0;
      var1E0 = 0;
      v145 = (_QWORD *)sub_1800FFF00(4 * n0x400_1, n0x93_2, n0x93_18);
    }
    else
    {
      if ( n0x400_1 >= 0x3FFFFFFFFFFFFFF7LL )
      {
        v1005 = 0;
        var1E0 = 0;
        sub_18002D1E0(v1020_3, n0x93_2, n0x93_18);
      }
      v1005 = 0;
      var1E0 = 0;
      v144 = sub_1800FFF00(v143 + 39, n0x93_2, n0x93_18);
      v145 = (_QWORD *)((v144 + 39) & 0xFFFFFFFFFFFFFFE0uLL);
      *(v145 - 1) = v144;
    }
    v964[0] = v145;
    v965 = (char *)v145 + v143;
    j_2 = 0;
    sub_180201670(v145, 0, v143);
    v964[1] = (char *)v145 + v143;
    memset(v975, 0, sizeof(v975));
    v1007_2 = v1007;
    do
    {
      if ( v1007_2 )
        v150 = *(_DWORD *)(n0x93.m128i_i64[0] + 4 * j_2);
      else
        v150 = *(unsigned __int16 *)(n0x93.m128i_i64[0] + 2 * j_2);
      *((_DWORD *)v145 + j_2++) = v150;
      v145 = v964[0];
      v146 = v964[1];
      j_1 = ((char *)v964[1] - (char *)v964[0]) >> 2;
    }
    while ( j_2 < j_1 );
    p_m128i_i64_1 = p_m128i_i64_5;
  }
  else
  {
    memset(v975, 0, sizeof(v975));
    v146 = 0;
    v145 = 0;
    j_1 = 0;
  }
  v151 = byte_18026FB58;
  LOBYTE(v1028) = byte_18026FB59;
  *(_QWORD *)&v1025 = v1025_0;
  v152 = 0x6DB6DB6DB6DB6DB7LL * ((*((_QWORD *)&p_m128i_i64_5 + 1) - p_m128i_i64_1) >> 2);
  p_m128i_i64 = p_m128i_i64_1;
  v995 = v152;
  if ( (unsigned __int64)(v152 - 200001) < 0xFFFFFFFFFFFCF2C0uLL
    || !j_1
    || j_1 != 3 * (j_1 / 3)
    || (_QWORD)::v1025 == (_QWORD)v1025 )
  {
LABEL_1061:
    p_??_7exception@std@@6B@_10 = &std::exception::`vftable';
    v872 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Mesh validation failed";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v872);
    p_??_7exception@std@@6B@_10 = &std::runtime_error::`vftable';
    v1017 = 0;
    v1016 = 0;
    sub_180179A30(&p_??_7exception@std@@6B@_10, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  v976 = 2 * (unsigned __int8)v976 + 2;
  v153 = ::v1025 - v1025;
  v154 = (unsigned __int16 *)(p_m128i_i64_1 + 26);
  do
  {
    if ( (*(_DWORD *)(v154 - 13) & 0x7FFFFFFFu) > 0x7F7FFFFF )
      goto LABEL_1061;
    v155 = v154 - 13;
    if ( (*(_DWORD *)(v154 - 11) & 0x7FFFFFFFu) > 0x7F7FFFFF
      || (*(_DWORD *)(v154 - 9) & 0x7FFFFFFFu) > 0x7F7FFFFF
      || *(v154 - 7) && v153 <= *(v154 - 3) )
    {
      goto LABEL_1061;
    }
    if ( *(v154 - 6) && v153 <= *(v154 - 2)
      || *(v154 - 5) && v153 <= *(v154 - 1)
      || *(v154 - 4) && v153 <= *v154
      || *(v154 - 4) + *(v154 - 5) + *(v154 - 6) + (unsigned int)*(v154 - 7) - 65540 < 0xFFFFFFF7 )
    {
      goto LABEL_1061;
    }
    v154 += 14;
  }
  while ( v155 + 14 != *((unsigned __int16 **)&p_m128i_i64_5 + 1) );
  v156 = (char *)v145 + 4 * j_1;
  v157 = 0;
  do
  {
    if ( *(_DWORD *)((char *)v145 + v157) >= (unsigned int)v152 )
      goto LABEL_1061;
    v157 += 4;
  }
  while ( 4 * j_1 != v157 );
  if ( LOBYTE(v1028) )
  {
    v1017 = 0;
    v1016 = 0;
    sub_1800C2600(v1011, v145, v156);
    v158 = v1011[0].m128i_i64[0];
    if ( v1011[0].m128i_i64[1] != v1011[0].m128i_i64[0] )
    {
      v159 = (v1011[0].m128i_i64[1] - v1011[0].m128i_i64[0]) >> 2;
      v160 = 0;
      do
      {
        v161 = *(_DWORD *)(v158 + 4 * v160);
        *(_DWORD *)(v158 + 4 * v160 + 8) = v161;
        *(_DWORD *)(v158 + 4 * v160 + 4) = v161;
        v160 += 3LL;
      }
      while ( v160 < v159 );
    }
    sub_1800C2690(v975, v1011);
    goto LABEL_372;
  }
  v1017 = 0;
  v1016 = 0;
  sub_1800C2720(v1011, v152, v156, v153);
  sub_1800C2720(&v1012, v995, v162, v163);
  v165 = v1011[0].m128i_i64[1];
  v164 = (_DWORD *)v1011[0].m128i_i64[0];
  v166 = (v1011[0].m128i_i64[1] - v1011[0].m128i_i64[0]) >> 2;
  if ( HIDWORD(v166) )
  {
    if ( v1011[0].m128i_i64[0] == v1011[0].m128i_i64[1] )
      goto LABEL_272;
    n0x1B = v1011[0].m128i_i64[1] - v1011[0].m128i_i64[0] - 4;
    LODWORD(v170) = 0;
    if ( n0x1B > 0x1B )
    {
      v171 = (n0x1B >> 2) + 1;
      v170 = v171 & 0xFFFFFFFFFFFFFFF8uLL;
      si128 = _mm_load_si128((const __m128i *)&xmmword_18021DCE0);
      v173 = 0;
      v174 = _mm_load_si128((const __m128i *)&xmmword_18021DCF0);
      v175 = _mm_load_si128((const __m128i *)&xmmword_18021DD00);
      do
      {
        *(__m128i *)&v164[v173] = si128;
        *(__m128i *)&v164[v173 + 4] = _mm_add_epi32(si128, v174);
        v173 += 8;
        si128 = _mm_add_epi32(si128, v175);
      }
      while ( v170 != v173 );
      if ( v171 == v170 )
        goto LABEL_272;
      v164 += v170;
    }
    do
    {
      *v164++ = v170;
      LODWORD(v170) = v170 + 1;
    }
    while ( v164 != (_DWORD *)v165 );
    goto LABEL_272;
  }
  if ( v1011[0].m128i_i64[1] == v1011[0].m128i_i64[0] )
    goto LABEL_272;
  n7_1 = (unsigned int)(v166 - 1);
  if ( (unsigned int)n7_1 < 7 )
  {
    v168 = 0;
LABEL_270:
    v181 = &v164[v168];
    do
    {
      *v181 = v168;
      LODWORD(v168) = v168 + 1;
      ++v181;
    }
    while ( (_DWORD)v166 != (_DWORD)v168 );
    goto LABEL_272;
  }
  v176 = n7_1 + 1;
  v168 = v176 & 0xFFFFFFFFFFFFFFF8uLL;
  v177 = _mm_load_si128((const __m128i *)&xmmword_18021DCE0);
  v178 = 0;
  v179 = _mm_load_si128((const __m128i *)&xmmword_18021DCF0);
  v180 = _mm_load_si128((const __m128i *)&xmmword_18021DD00);
  do
  {
    *(__m128i *)&v164[v178 / 4] = v177;
    *(__m128i *)&v164[v178 / 4 + 4] = _mm_add_epi32(v177, v179);
    v177 = _mm_add_epi32(v177, v180);
    v178 += 32LL;
  }
  while ( ((4 * v176) & 0xFFFFFFFFFFFFFFE0uLL) != v178 );
  if ( v176 != v168 )
    goto LABEL_270;
LABEL_272:
  v1012_1 = v1012;
  n5 = v1012.m128i_i64[1] - v1012.m128i_i64[0];
  n2 = (v1012.m128i_i64[1] - v1012.m128i_i64[0]) >> 2;
  LOBYTE(v1020) = v151;
  if ( HIDWORD(n2) )
  {
    if ( v1012.m128i_i64[0] != v1012.m128i_i64[1] )
    {
      LODWORD(v187) = 0;
      v188 = (_DWORD *)v1012.m128i_i64[0];
      if ( n5 - 4 > 0x1B )
      {
        v189 = ((n5 - 4) >> 2) + 1;
        v187 = v189 & 0xFFFFFFFFFFFFFFF8uLL;
        v190 = _mm_load_si128((const __m128i *)&xmmword_18021DCE0);
        v191 = 0;
        v192 = _mm_load_si128((const __m128i *)&xmmword_18021DCF0);
        v193 = _mm_load_si128((const __m128i *)&xmmword_18021DD00);
        do
        {
          *(__m128i *)(v1012_1.m128i_i64[0] + 4 * v191) = v190;
          *(__m128i *)(v1012_1.m128i_i64[0] + 4 * v191 + 16) = _mm_add_epi32(v190, v192);
          v191 += 8;
          v190 = _mm_add_epi32(v190, v193);
        }
        while ( v187 != v191 );
        if ( v189 == v187 )
          goto LABEL_289;
        v188 = (_DWORD *)(v1012_1.m128i_i64[0] + 4 * v187);
      }
      do
      {
        *v188++ = v187;
        LODWORD(v187) = v187 + 1;
      }
      while ( v188 != (_DWORD *)v1012_1.m128i_i64[1] );
    }
  }
  else if ( v1012.m128i_i64[1] != v1012.m128i_i64[0] )
  {
    n7_2 = (unsigned int)(n2 - 1);
    if ( (unsigned int)n7_2 >= 7 )
    {
      n2_9 = n7_2 + 1;
      n2_6 = n2_9 & 0xFFFFFFFFFFFFFFF8uLL;
      v195 = _mm_load_si128((const __m128i *)&xmmword_18021DCE0);
      v196 = 0;
      v197 = _mm_load_si128((const __m128i *)&xmmword_18021DCF0);
      v198 = _mm_load_si128((const __m128i *)&xmmword_18021DD00);
      do
      {
        *(__m128i *)(v1012_1.m128i_i64[0] + v196) = v195;
        *(__m128i *)(v1012_1.m128i_i64[0] + v196 + 16) = _mm_add_epi32(v195, v197);
        v195 = _mm_add_epi32(v195, v198);
        v196 += 32;
      }
      while ( ((4 * n2_9) & 0xFFFFFFFFFFFFFFE0uLL) != v196 );
      if ( n2_9 == n2_6 )
        goto LABEL_289;
    }
    else
    {
      n2_6 = 0;
    }
    v199 = (_DWORD *)(v1012_1.m128i_i64[0] + 4 * n2_6);
    do
    {
      *v199 = n2_6;
      LODWORD(n2_6) = n2_6 + 1;
      ++v199;
    }
    while ( (_DWORD)n2 != (_DWORD)n2_6 );
  }
LABEL_289:
  v1028 = *(double *)v1012_1.m128i_i64;
  v200 = v1011[0].m128i_i64[0];
  for ( j = 0; j < j_1; j += 3LL )
  {
    v202 = *((unsigned int *)v145 + j);
    v203 = *(_DWORD *)(v200 + 4 * v202);
    LODWORD(v204) = *((_DWORD *)v145 + j);
    if ( v203 != (_DWORD)v202 )
    {
      do
      {
        v204 = *(unsigned int *)(v200 + 4LL * v203);
        *(_DWORD *)(v200 + 4 * v202) = v204;
        v203 = *(_DWORD *)(v200 + 4 * v204);
        v202 = v204;
      }
      while ( v203 != (_DWORD)v204 );
    }
    for ( k = *((unsigned int *)v145 + j + 1); ; *(_DWORD *)(v200 + 4 * k_2) = k )
    {
      k_1 = *(_DWORD *)(v200 + 4 * k);
      if ( k_1 == (_DWORD)k )
        break;
      k_2 = k;
      k = *(unsigned int *)(v200 + 4LL * k_1);
    }
    *(_DWORD *)(v200 + 4 * k) = v204;
    v208 = *((unsigned int *)v145 + j);
    v209 = *(_DWORD *)(v200 + 4 * v208);
    LODWORD(v204) = *((_DWORD *)v145 + j);
    if ( v209 != (_DWORD)v208 )
    {
      do
      {
        v204 = *(unsigned int *)(v200 + 4LL * v209);
        *(_DWORD *)(v200 + 4 * v208) = v204;
        v209 = *(_DWORD *)(v200 + 4 * v204);
        v208 = v204;
      }
      while ( v209 != (_DWORD)v204 );
    }
    for ( m = *((unsigned int *)v145 + j + 2); ; *(_DWORD *)(v200 + 4 * m_2) = m )
    {
      m_1 = *(_DWORD *)(v200 + 4 * m);
      if ( m_1 == (_DWORD)m )
        break;
      m_2 = m;
      m = *(unsigned int *)(v200 + 4LL * m_1);
    }
    *(_DWORD *)(v200 + 4 * m) = v204;
  }
  v1028_1 = v1028;
  sub_1800C27C0(*(_QWORD *)&v1028, v1012_1.m128i_i64[1], n2, &p_m128i_i64);
  if ( n5 >= 5 )
  {
    v1028_2 = v1028_1;
    n2_1 = 2;
    if ( n2 >= 3 )
      n2_1 = n2;
    v216 = 1;
    v217 = v1011[0].m128i_i64[0];
    var150.m128_u64[0] = n2_1;
    while ( 1 )
    {
      v218 = 28LL * *(unsigned int *)(*(_QWORD *)&v1028_2 + 4 * v216);
      v219 = 28LL * *(unsigned int *)(*(_QWORD *)&v1028_2 + 4 * v216 - 4);
      v220 = *(float *)(p_m128i_i64 + v219);
      v221 = *(float *)(p_m128i_i64 + v218);
      if ( v220 != v221 )
        goto LABEL_315;
      v222 = p_m128i_i64 + v218;
      v223 = p_m128i_i64 + v219;
      if ( *(float *)(v223 + 4) != *(float *)(v222 + 4) || *(float *)(v223 + 8) != *(float *)(v222 + 8) )
        break;
      if ( *(_QWORD *)(v223 + 12) == *(_QWORD *)(v222 + 12) )
      {
        v224 = v222 + 20;
        v225 = v223 + 20;
      }
      else
      {
        v224 = v222 + 12;
        v225 = v223 + 12;
      }
      n4_3 = sub_180122E60(v225, v224, 4);
      v1028_2 = v1028;
      n2_1 = var150.m128_u64[0];
      if ( n4_3 == 4 || *(_WORD *)(v225 + 2 * n4_3) >= *(_WORD *)(v224 + 2 * n4_3) )
      {
LABEL_320:
        v227 = *(unsigned int *)(*(_QWORD *)&v1028_2 + 4 * v216 - 4);
        v228 = *(_DWORD *)(v217 + 4 * v227);
        LODWORD(v229) = *(_DWORD *)(*(_QWORD *)&v1028_2 + 4 * v216 - 4);
        if ( v228 != (_DWORD)v227 )
        {
          do
          {
            v229 = *(unsigned int *)(v217 + 4LL * v228);
            *(_DWORD *)(v217 + 4 * v227) = v229;
            v228 = *(_DWORD *)(v217 + 4 * v229);
            v227 = v229;
          }
          while ( v228 != (_DWORD)v229 );
        }
        for ( n = *(unsigned int *)(*(_QWORD *)&v1028_2 + 4 * v216); ; *(_DWORD *)(v217 + 4 * n_2) = n )
        {
          n_1 = *(_DWORD *)(v217 + 4 * n);
          if ( n_1 == (_DWORD)n )
            break;
          n_2 = n;
          n = *(unsigned int *)(v217 + 4LL * n_1);
        }
        *(_DWORD *)(v217 + 4 * n) = v229;
      }
LABEL_306:
      if ( ++v216 == n2_1 )
        goto LABEL_325;
    }
    v220 = *(float *)(v223 + 4);
    v221 = *(float *)(v222 + 4);
    if ( v220 == v221 )
    {
      if ( *(float *)(v222 + 8) <= *(float *)(v223 + 8) )
        goto LABEL_320;
      goto LABEL_306;
    }
LABEL_315:
    if ( v221 <= v220 )
      goto LABEL_320;
    goto LABEL_306;
  }
LABEL_325:
  sub_1800C15C0((__int128 *)p_n0x93.m128i_i8, v995);
  sub_1800C15C0(&n0x93_4, v995);
  v1020_4 = v1020;
  if ( (_BYTE)v1020 )
    v236 = v995;
  else
    v236 = 0;
  sub_1800C2720(&v1012_, v236, v233, v234);
  if ( v995 )
  {
    v237 = v1011[0].m128i_i64[0];
    n0x93_5 = n0x93_4;
    v239 = p_n0x93.m128i_i64[0];
    v240 = 0;
    v1028 = *(double *)v1012_.m128i_i64;
    do
    {
      v241 = *(_DWORD *)(v237 + 4 * v240);
      v242 = v240;
      if ( v241 == (_DWORD)v240 )
      {
        v242 = (unsigned int)v240;
      }
      else
      {
        do
        {
          v243 = v242;
          v242 = *(unsigned int *)(v237 + 4LL * v241);
          *(_DWORD *)(v237 + 4 * v243) = v242;
          v241 = *(_DWORD *)(v237 + 4 * v242);
        }
        while ( v241 != (_DWORD)v242 );
      }
      v244 = 28 * v240;
      v245 = *(unsigned __int16 *)(p_m128i_i64 + 28 * v240 + 12);
      *(_QWORD *)(n0x93_5 + 8 * v242) += v245;
      p_m128i_i64_2 = p_m128i_i64;
      if ( v245 )
      {
        if ( *(_BYTE *)(v1025 + *(unsigned __int16 *)(p_m128i_i64 + v244 + 20)) == 1 )
        {
          *(_QWORD *)(v239 + 8 * v242) += v245;
          p_m128i_i64_2 = p_m128i_i64;
        }
        if ( v1020_4
          && (unsigned __int8)(*(_BYTE *)(v1025 + *(unsigned __int16 *)(p_m128i_i64_2 + v244 + 20)) - 1) <= 1u )
        {
          *(_DWORD *)(*(_QWORD *)&v1028 + 4 * v240) += (unsigned __int16)v245;
          p_m128i_i64_2 = p_m128i_i64;
        }
      }
      v247 = *(unsigned __int16 *)(p_m128i_i64_2 + v244 + 14);
      *(_QWORD *)(n0x93_5 + 8 * v242) += v247;
      p_m128i_i64_3 = p_m128i_i64;
      if ( v247 )
      {
        if ( *(_BYTE *)(v1025 + *(unsigned __int16 *)(p_m128i_i64 + v244 + 22)) == 1 )
        {
          *(_QWORD *)(v239 + 8 * v242) += v247;
          p_m128i_i64_3 = p_m128i_i64;
        }
        if ( v1020_4
          && (unsigned __int8)(*(_BYTE *)(v1025 + *(unsigned __int16 *)(p_m128i_i64_3 + v244 + 22)) - 1) <= 1u )
        {
          *(_DWORD *)(*(_QWORD *)&v1028 + 4 * v240) += (unsigned __int16)v247;
          p_m128i_i64_3 = p_m128i_i64;
        }
      }
      v249 = *(unsigned __int16 *)(p_m128i_i64_3 + v244 + 16);
      *(_QWORD *)(n0x93_5 + 8 * v242) += v249;
      p_m128i_i64_4 = p_m128i_i64;
      if ( v249 )
      {
        if ( *(_BYTE *)(v1025 + *(unsigned __int16 *)(p_m128i_i64 + v244 + 24)) == 1 )
        {
          *(_QWORD *)(v239 + 8 * v242) += v249;
          p_m128i_i64_4 = p_m128i_i64;
        }
        if ( v1020_4
          && (unsigned __int8)(*(_BYTE *)(v1025 + *(unsigned __int16 *)(p_m128i_i64_4 + v244 + 24)) - 1) <= 1u )
        {
          *(_DWORD *)(*(_QWORD *)&v1028 + 4 * v240) += (unsigned __int16)v249;
          p_m128i_i64_4 = p_m128i_i64;
        }
      }
      v251 = *(unsigned __int16 *)(p_m128i_i64_4 + v244 + 18);
      *(_QWORD *)(n0x93_5 + 8 * v242) += v251;
      if ( v251 )
      {
        if ( *(_BYTE *)(v1025 + *(unsigned __int16 *)(p_m128i_i64 + v244 + 26)) == 1 )
          *(_QWORD *)(v239 + 8 * v242) += v251;
        if ( v1020_4 && (unsigned __int8)(*(_BYTE *)(v1025 + *(unsigned __int16 *)(p_m128i_i64 + v244 + 26)) - 1) <= 1u )
          *(_DWORD *)(*(_QWORD *)&v1028 + 4 * v240) += (unsigned __int16)v251;
      }
      v240 = (unsigned int)(v240 + 1);
    }
    while ( v995 > v240 );
  }
  sub_1800C2600(&v1012__1, v145, (char *)v145 + 4 * j_1);
  v252 = v1011[0].m128i_i64[0];
  v253 = v1012_.m128i_i64[0];
  v254 = p_n0x93.m128i_i64[0];
  n0x93_6 = n0x93_4;
  v256 = v1012__1.m128i_i64[0];
  for ( ii = 0; ii < j_1; ii += 3LL )
  {
    v259 = *((unsigned int *)v145 + ii);
    v260 = *(_DWORD *)(v252 + 4 * v259);
    LODWORD(v261) = *((_DWORD *)v145 + ii);
    if ( v260 != (_DWORD)v259 )
    {
      do
      {
        v261 = *(unsigned int *)(v252 + 4LL * v260);
        *(_DWORD *)(v252 + 4 * v259) = v261;
        v260 = *(_DWORD *)(v252 + 4 * v261);
        v259 = v261;
      }
      while ( v260 != (_DWORD)v261 );
    }
    v262 = v1020_4
        && *(_DWORD *)(v253 + 4LL * *((unsigned int *)v145 + ii)) >= 0x8000u
        && *(_DWORD *)(v253 + 4LL * *((unsigned int *)v145 + ii + 1)) >= 0x8000u
        && *(_DWORD *)(v253 + 4LL * *((unsigned int *)v145 + ii + 2)) >= 0x8000u;
    if ( (unsigned __int64)(2LL * *(_QWORD *)(v254 + 8LL * (unsigned int)v261)) > *(_QWORD *)(n0x93_6
                                                                                            + 8LL * (unsigned int)v261)
      || v262 )
    {
      v258 = *(_DWORD *)(v256 + 4 * ii);
      *(_DWORD *)(v256 + 4 * ii + 4) = v258;
      *(_DWORD *)(v256 + 4 * ii + 8) = v258;
    }
  }
  sub_1800C2690(v975, &v1012__1);
  sub_180008570(&v1012__1);
  sub_180008570(&v1012_);
  sub_180035CE0(&n0x93_4);
  sub_180035CE0(&p_n0x93);
  sub_180008570(&v1012);
LABEL_372:
  sub_180008570(v1011);
  if ( v146 == (_BYTE *)v145 )
  {
    v264 = 0;
  }
  else
  {
    v263 = (v146 - (_BYTE *)v145) >> 2;
    v264 = 0;
    v265 = 0;
    do
    {
      if ( *(_DWORD *)(v975[0] + 4 * v265 + 4) != *((_DWORD *)v145 + v265 + 1)
        || *(_DWORD *)(v975[0] + 4 * v265 + 8) != *((_DWORD *)v145 + v265 + 2) )
      {
        ++v264;
      }
      v265 += 3LL;
    }
    while ( v265 < v263 );
  }
  v989 = v264;
  v1017 = 0;
  v1016 = 0;
  v268 = qword_18026F328(v1001, "UnityEngine.Rendering", "SubMeshDescriptor");
  v1012.m128i_i64[0] = 0;
  v1012.m128i_i64[1] = 24;
  v953.m128i_i64[0] = 28;
  v953.m128i_i64[1] = 32;
  n36 = 36;
  n40 = 40;
  n44 = 44;
  if ( !v268 )
  {
    p_??_7exception@std@@6B@_11 = &std::exception::`vftable';
    v874 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Submesh type absent";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v874);
    p_??_7exception@std@@6B@_11 = &std::runtime_error::`vftable';
    v1017 = 0;
    v1016 = 0;
    sub_180179A30(&p_??_7exception@std@@6B@_11, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  n7_3 = 0;
  while ( n7_3 < 7 )
  {
    v271 = (char *)dword_1802262C0 + dword_1802262C0[n7_3];
    v1017 = 0;
    v1016 = 0;
    v272 = qword_18026F338(v268, v271);
    if ( v272 )
    {
      v1017 = 0;
      v1016 = 0;
      v1025_15 = qword_18026F340(v272);
      v1025_10 = v1012.m128i_i64[n7_3++] + 16;
      if ( v1025_15 == v1025_10 )
        continue;
    }
    p_??_7exception@std@@6B@_12 = &std::exception::`vftable';
    v876 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Submesh metadata layout changed";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v876);
    p_??_7exception@std@@6B@_12 = &std::runtime_error::`vftable';
    v1017 = 0;
    v1016 = 0;
    sub_180179A30(&p_??_7exception@std@@6B@_12, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  v1020_6 = 0;
  v939 = 0;
  n0x56_2 = 0;
  n0x56_1 = n0x56;
  if ( (_DWORD)n0x56 )
  {
    v276 = 48 * n0x56;
    v1017 = 0;
    v1016 = 0;
    if ( (unsigned int)n0x56 < 0x56 )
    {
      v1020_5 = sub_1800FFF00(48 * n0x56, v1021_1, v269);
    }
    else
    {
      v277 = sub_1800FFF00(v276 + 39, v1021_1, v269);
      v1020_5 = (v277 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
      *(_QWORD *)(v1020_5 - 8) = v277;
    }
    *(_QWORD *)&v1020_6 = v1020_5;
    n0x56_1 = n0x56;
    v939 = v1020_5 + 48 * n0x56;
    sub_180201670(v1020_5, 0, v276);
    v1020 = v1020_5;
    mm_2 = (__m128i *)(v1020_5 + v276);
    *((_QWORD *)&v1020_6 + 1) = mm_2;
  }
  else
  {
    mm_2 = 0;
    v1020 = 0;
  }
  while ( (int)n0x56_2 < n0x56_1 )
  {
    v1009 = 0;
    v1008 = 0;
    v280 = qword_18026F598(v1022);
    v281 = 48LL * n0x56_2;
    v1009 = 0;
    v1008 = 0;
    ((void (__fastcall *)(__int64, _QWORD, __int64))v1010.m128i_i64[0])(v280, n0x56_2, v281 + v1020);
    v1020 = v1020_6;
    v282 = *(_DWORD *)(v1020_6 + v281 + 28);
    n0x56_1 = n0x56;
    if ( (unsigned int)(-1431655765 * v282) <= 0x55555555 && v282 >= 0 )
    {
      v1021_6 = *(_DWORD *)(v1020 + v281 + 32);
      if ( v1021_6 >= 0 )
      {
        v1021_1 = 3 * (v1021_6 / 3u);
        if ( v1021_6 == (_DWORD)v1021_1 )
        {
          v1025_16 = (unsigned int)(v1021_6 + v282);
          v1025_10 = ((char *)v964[1] - (char *)v964[0]) >> 2;
          if ( v1025_10 >= v1025_16 )
          {
            v285 = v1020 + v281;
            if ( !*(_DWORD *)(v285 + 24) )
            {
              ++n0x56_2;
              if ( !*(_DWORD *)(v285 + 36) )
                continue;
            }
          }
        }
      }
    }
    p_??_7exception@std@@6B@_13 = &std::exception::`vftable';
    v878 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Unsupported submesh topology/base vertex";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v878);
    p_??_7exception@std@@6B@_13 = &std::runtime_error::`vftable';
    v1009 = 0;
    v1008 = 0;
    sub_180179A30(&p_??_7exception@std@@6B@_13, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  mm_1 = mm_2;
  if ( ((unsigned __int8)v1006 & (unsigned __int8)byte_18026FAE0) == 0 )
  {
    v980 = 0;
    v981 = 0;
    v1002 = (unsigned __int64)v964[0];
    v1006_1 = (char *)v964[1] - (char *)v964[0];
    goto LABEL_814;
  }
  v286 = v975[0];
  v1025_1 = ::v1025;
  v1025_10 = v1025_0;
  v1002 = (unsigned __int64)v964[0];
  v288 = ((char *)v964[1] - (char *)v964[0]) >> 2;
  n0x400_2 = 0x6DB6DB6DB6DB6DB7LL * ((__int64)(*((_QWORD *)&p_m128i_i64_5 + 1) - p_m128i_i64_5) >> 2);
  v1021_5 = p_m128i_i64_5;
  n0x400 = n0x400_2;
  v1006 = v975[1] - v975[0];
  v1021_1 = (v975[1] - v975[0]) ^ ((char *)v964[1] - (char *)v964[0]) | (v288 % 3);
  if ( *((_QWORD *)&p_m128i_i64_5 + 1) == (_QWORD)p_m128i_i64_5 || v1021_1 != 0 )
  {
    v980 = 0;
    v981 = 0;
    v1006_1 = (char *)v964[1] - (char *)v964[0];
    goto LABEL_814;
  }
  v291 = ::v1025 - v1025_0;
  v1021_1 = qword_18026FB90 & 0x7FFFFFFFFFFFFFFFLL;
  LOBYTE(v1021_1) = (qword_18026FB90 & 0x7FFFFFFFFFFFFFFFuLL) > 0x7FEFFFFFFFFFFFFFLL
                 || *(double *)&qword_18026FB90 <= 0.000001;
  v1021 = v1021_1;
  v1025_11 = v1025_0;
  v930 = ::v1025 - v1025_0;
  if ( (_BYTE)v1021_1 )
  {
    LOBYTE(v291) = 2;
    if ( sub_18011D910(v1025_0, ::v1025, v291) == v1025_1 )
    {
      v980 = 0;
      v981 = 0;
      goto LABEL_813;
    }
    n0x400_2 = n0x400;
    p_n0x93 = 0;
    v998 = 0;
    if ( !n0x400 )
    {
      n0x93_4 = 0;
      v991 = 0;
      v294 = 0;
      v1004_1 = 0;
      goto LABEL_437;
    }
  }
  if ( n0x400_2 >> 62 )
  {
    v1009 = 0;
    v1008 = 0;
    std::vector<void *>::_Xlen(v1025_10, v1021_1, v291);
  }
  v292 = 4 * n0x400_2;
  if ( n0x400_2 < 0x400 )
  {
    v1009 = 0;
    v1008 = 0;
    v294 = sub_1800FFF00(4 * n0x400_2, v1021_1, v291);
  }
  else
  {
    if ( n0x400_2 >= 0x3FFFFFFFFFFFFFF7LL )
    {
      v1009 = 0;
      v1008 = 0;
      sub_18002D1E0(v1025_10, v1021_1, v291);
    }
    v1009 = 0;
    v1008 = 0;
    v293 = sub_1800FFF00(v292 + 39, v1021_1, v291);
    v294 = (v293 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
    *(_QWORD *)(v294 - 8) = v293;
  }
  p_n0x93.m128i_i64[0] = v294;
  v998 = v294 + v292;
  sub_180201670(v294, 0, 4 * n0x400_2);
  v1004 = v294 + v292;
  p_n0x93.m128i_i64[1] = v294 + v292;
  n0x400_3 = n0x400;
  n0x93_4 = 0;
  v991 = 0;
  if ( n0x400 )
  {
    if ( n0x400 >> 62 )
      std::vector<void *>::_Xlen(v296, v295, v297);
    v299 = v294;
    v300 = 4 * n0x400;
    if ( n0x400 < 0x400 )
    {
      v302 = sub_1800FFF00(4 * n0x400, v295, v297);
    }
    else
    {
      if ( n0x400 >= 0x3FFFFFFFFFFFFFF7LL )
        sub_18002D1E0(v296, v295, v297);
      v301 = sub_1800FFF00(v300 + 39, v295, v297);
      v302 = (v301 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
      *(_QWORD *)(v302 - 8) = v301;
    }
    *(_QWORD *)&n0x93_4 = v302;
    v303 = v302 + 4 * n0x400_3;
    v991 = v303;
    sub_180201670(v302, 0, v300);
    *((_QWORD *)&n0x93_4 + 1) = v303;
    v294 = v299;
  }
  if ( HIDWORD(n0x400_2) )
  {
    v306 = (((unsigned __int64)(v292 - 4) >> 2) + 1) & 0xFFFFFFFFFFFFFFF8uLL;
    v307 = _mm_load_si128((const __m128i *)&xmmword_18021DCE0);
    v308 = 0;
    v309 = _mm_load_si128((const __m128i *)&xmmword_18021DCF0);
    v310 = _mm_load_si128((const __m128i *)&xmmword_18021DD00);
    do
    {
      *(__m128i *)(v294 + 4 * v308) = v307;
      *(__m128i *)(v294 + 4 * v308 + 16) = _mm_add_epi32(v307, v309);
      v308 += 8;
      v307 = _mm_add_epi32(v307, v310);
    }
    while ( v306 != v308 );
    v1004_1 = v1004;
    if ( ((unsigned __int64)(v292 - 4) >> 2) + 1 != v306 )
    {
      v312 = 4 * v306;
      do
      {
        *(_DWORD *)(v294 + v312) = v306;
        LODWORD(v306) = v306 + 1;
        v312 += 4;
      }
      while ( v292 != v312 );
      v294 = p_n0x93.m128i_i64[0];
    }
    goto LABEL_437;
  }
  n7_4 = (unsigned int)(n0x400_2 - 1);
  if ( (unsigned int)n7_4 >= 7 )
  {
    v313 = n7_4 + 1;
    v305 = v313 & 0xFFFFFFFFFFFFFFF8uLL;
    v314 = _mm_load_si128((const __m128i *)&xmmword_18021DCE0);
    v315 = 0;
    v316 = _mm_load_si128((const __m128i *)&xmmword_18021DCF0);
    v317 = _mm_load_si128((const __m128i *)&xmmword_18021DD00);
    do
    {
      *(__m128i *)(v294 + v315) = v314;
      *(__m128i *)(v294 + v315 + 16) = _mm_add_epi32(v314, v316);
      v314 = _mm_add_epi32(v314, v317);
      v315 += 32;
    }
    while ( ((4 * v313) & 0xFFFFFFFFFFFFFFE0uLL) != v315 );
    if ( v313 == v305 )
      goto LABEL_435;
  }
  else
  {
    v305 = 0;
  }
  v318 = n0x400_2 - v305;
  do
  {
    *(_DWORD *)(v294 + 4 * v305) = v305;
    ++v305;
    --v318;
  }
  while ( v318 );
LABEL_435:
  v1004_1 = v1004;
LABEL_437:
  v936 = v286;
  v937 = v288;
  if ( v1021 )
    v319 = 0.000001;
  else
    v319 = *(double *)&qword_18026FB90 * 0.0001;
  v927 = v319;
  p_v1011.m128i_i64[0] = (__int64)&v1021_5;
  p_v1011.m128i_i64[1] = (__int64)&v927;
  sub_1800BF100(v294, v1004_1, (__int64)(v1004_1 - v294) >> 2, &p_v1011);
  v320 = p_n0x93.m128i_i64[0];
  v1004_5 = 0;
  n0x93_7 = n0x93_4;
  v323 = v294;
  n0x93_8 = n0x93_4;
  while ( 1 )
  {
    var150.m128_u64[0] = v323;
    v1004_2 = (__int64)(v1004_1 - v323) >> 2;
    if ( v1004_5 >= v1004_2 )
      break;
    v1010 = p_v1011;
    v1004 = v1004_2;
    *(_QWORD *)&v1028 = v1004_2 - 1;
    v1004_3 = v1004_5;
    v1004_6 = v1004_5;
    while ( 1 )
    {
      if ( *(_QWORD *)&v1028 == v1004_3 )
      {
        v1004_3 = v1004;
        goto LABEL_451;
      }
      v327 = *(unsigned int *)(var150.m128_u64[0] + 4 * v1004_3 + 4);
      *(_QWORD *)&v1025 = v1004_3 + 1;
      v0 = (__m128)*(unsigned int *)(*(_QWORD *)v1010.m128i_i64[0] + 28 * v327);
      v328 = sub_1801ED780();
      v329 = sub_1801ED780();
      v330 = sub_1801ED780();
      v331 = sub_1801ED780();
      v332 = sub_1801ED780();
      if ( v332 != sub_1801ED780() || v330 != v328 )
        break;
      n0x93_7 = n0x93_8;
      v1004_3 = v1025;
      if ( v331 != v329 )
        goto LABEL_451;
    }
    n0x93_7 = n0x93_8;
    v1004_3 = v1025;
LABEL_451:
    *(_QWORD *)&v1028 = v1004_3 - v1004_6;
    v1004_4 = v1004_6;
    *(_QWORD *)&v1025 = v1004_3;
    while ( v1004_4 < v1004_3 )
    {
      *(_DWORD *)(n0x93_7 + 4LL * *(unsigned int *)(v320 + 4 * v1004_4)) = *(_DWORD *)(v320 + 4 * v1004_4);
      if ( *(_QWORD *)&v1028 <= 0x40u )
      {
        v1004_7 = v1004_6;
        if ( v1004_6 < v1004_4 )
        {
          while ( 1 )
          {
            v335 = 28LL * *(unsigned int *)(v320 + 4 * v1004_4);
            v336 = 28LL * *(unsigned int *)(n0x93_7 + 4LL * *(unsigned int *)(v320 + 4 * v1004_7));
            v1013 = 0;
            var190 = 0;
            v1011[1] = 0;
            v1011[0] = 0;
            v337 = *(unsigned __int16 *)(v1021_5 + v335 + 12);
            v1011[0].m128i_i16[0] = *(_WORD *)(v1021_5 + v335 + 20);
            v1011[0].m128i_i32[1] = v337;
            v338 = -*(unsigned __int16 *)(v1021_5 + v336 + 12);
            var190.m128i_i16[0] = *(_WORD *)(v1021_5 + v336 + 20);
            var190.m128i_i32[1] = v338;
            v339 = *(unsigned __int16 *)(v1021_5 + v335 + 14);
            v1011[0].m128i_i16[4] = *(_WORD *)(v1021_5 + v335 + 22);
            v1011[0].m128i_i32[3] = v339;
            v340 = -*(unsigned __int16 *)(v1021_5 + v336 + 14);
            var190.m128i_i16[4] = *(_WORD *)(v1021_5 + v336 + 22);
            var190.m128i_i32[3] = v340;
            v341 = *(unsigned __int16 *)(v1021_5 + v335 + 16);
            v1011[1].m128i_i16[0] = *(_WORD *)(v1021_5 + v335 + 24);
            v1011[1].m128i_i32[1] = v341;
            v342 = -*(unsigned __int16 *)(v1021_5 + v336 + 16);
            v1013.m128i_i16[0] = *(_WORD *)(v1021_5 + v336 + 24);
            v1013.m128i_i32[1] = v342;
            v343 = *(unsigned __int16 *)(v1021_5 + v335 + 18);
            v1011[1].m128i_i16[4] = *(_WORD *)(v1021_5 + v335 + 26);
            v1011[1].m128i_i32[3] = v343;
            LODWORD(v335) = -*(unsigned __int16 *)(v1021_5 + v336 + 18);
            v1013.m128i_i16[4] = *(_WORD *)(v1021_5 + v336 + 26);
            v1013.m128i_i32[3] = v335;
            sub_1800BFCB0(v1011, v1015, 8);
            v344 = v1011[0].m128i_i32[1];
            v345 = -v1011[0].m128i_i32[1];
            if ( v1011[0].m128i_i32[1] > 0 )
              v345 = v1011[0].m128i_i32[1];
            v346 = 0;
            if ( v1011[0].m128i_i16[4] != v1011[0].m128i_i16[0] )
            {
              v344 = 0;
              v346 = v345;
            }
            v347 = v1011[0].m128i_i32[3] + v344;
            v348 = -v347;
            if ( v347 > 0 )
              v348 = v347;
            if ( v1011[1].m128i_i16[0] == v1011[0].m128i_i16[4] )
              v348 = 0;
            else
              v347 = 0;
            v349 = v346 + v348;
            v350 = v1011[1].m128i_i32[1] + v347;
            v351 = -v350;
            if ( v350 > 0 )
              v351 = v350;
            if ( v1011[1].m128i_i16[4] == v1011[1].m128i_i16[0] )
              v351 = 0;
            else
              v350 = 0;
            v352 = v349 + v351;
            v353 = v1011[1].m128i_i32[3] + v350;
            v354 = -v353;
            if ( v353 > 0 )
              v354 = v353;
            if ( var190.m128i_i16[0] == v1011[1].m128i_i16[4] )
              v354 = 0;
            else
              v353 = 0;
            v355 = v352 + v354;
            v356 = var190.m128i_i32[1] + v353;
            v357 = -v356;
            if ( v356 > 0 )
              v357 = v356;
            if ( var190.m128i_i16[4] == var190.m128i_i16[0] )
              v357 = 0;
            else
              v356 = 0;
            v358 = v355 + v357;
            v359 = var190.m128i_i32[3] + v356;
            v360 = -v359;
            if ( v359 > 0 )
              v360 = v359;
            if ( v1013.m128i_i16[0] == var190.m128i_i16[4] )
              v360 = 0;
            else
              v359 = 0;
            v361 = v358 + v360;
            v362 = v1013.m128i_i32[1] + v359;
            v363 = -v362;
            if ( v362 > 0 )
              v363 = v362;
            if ( v1013.m128i_i16[4] == v1013.m128i_i16[0] )
              v363 = 0;
            else
              v362 = 0;
            v364 = v361 + v363;
            v365 = v1013.m128i_i32[3] + v362;
            v366 = -v365;
            if ( v365 > 0 )
              v366 = v365;
            if ( (unsigned int)(v364 + v366) <= 0x80 )
              break;
            if ( ++v1004_7 >= v1004_4 )
              goto LABEL_453;
          }
          *(_DWORD *)(n0x93_7 + 4LL * *(unsigned int *)(v320 + 4 * v1004_4)) = *(_DWORD *)(n0x93_7
                                                                                         + 4LL
                                                                                         * *(unsigned int *)(v320 + 4 * v1004_7));
        }
      }
LABEL_453:
      ++v1004_4;
      v1004_3 = v1025;
    }
    v1004_1 = p_n0x93.m128i_i64[1];
    v323 = v320;
    v1004_5 = v1004_3;
  }
  v1012_ = 0;
  v993 = 0;
  var150.m128_u64[0] = v1002;
  v367 = 0;
  v1010.m128i_i64[0] = 0;
  v368 = 0;
  v369 = 0;
  v370 = v936;
  v371 = v937;
  while ( v369 < v371 )
  {
    v372 = *(_DWORD *)(v370 + 4 * v369);
    if ( v372 == *(_DWORD *)(v370 + 4 * v369 + 4) )
    {
      LOBYTE(v372) = v372 == *(_DWORD *)(v370 + 4 * v369 + 8);
      LODWORD(v1025) = v372;
    }
    else
    {
      LODWORD(v1025) = 0;
    }
    n0x400_4 = *(unsigned int *)(v1002 + 4 * v369);
    v1021_1 = n0x400;
    if ( n0x400 <= n0x400_4
      || (n0x400_5 = *(unsigned int *)(v1002 + 4 * v369 + 4), n0x400 <= n0x400_5)
      || (n0x400_6 = *(unsigned int *)(v1002 + 4 * v369 + 8), n0x400 <= n0x400_6) )
    {
      v980 = 0;
      v981 = 0;
      v1006_1 = v1006;
      v399 = (_QWORD *)v1010.m128i_i64[0];
      if ( v1010.m128i_i64[0] )
        goto LABEL_799;
      goto LABEL_803;
    }
    n0x93_9 = n0x93_4;
    v377 = *(_DWORD *)(n0x93_4 + 4 * n0x400_4);
    v378 = *(_DWORD *)(n0x93_4 + 4 * n0x400_5);
    if ( v377 != v378 )
    {
      v379 = *(_DWORD *)(n0x93_4 + 4 * n0x400_6);
      if ( v378 != v379 && v379 != v377 )
      {
        *(_QWORD *)&v1028 = v1002 + 4 * v369;
        v380 = 0;
LABEL_510:
        v381 = v380;
        v382 = var150.m128_u64[0] + 4LL * v380;
        for ( jj = 0; v381 + (unsigned int)jj <= 2; ++jj )
        {
          v384 = *(unsigned int *)(v382 + 4 * jj);
          v380 = v381 + jj + 1;
          v385 = v380;
          if ( v381 + (_DWORD)jj == 2 )
            v385 = 0;
          v386 = *(_DWORD *)(n0x93_9 + 4 * v384);
          v387 = *(_DWORD *)(n0x93_9 + 4LL * *(unsigned int *)(*(_QWORD *)&v1028 + 4 * v385));
          if ( v368 == v367 )
          {
            v1010.m128i_i64[1] = v1012_.m128i_i64[0];
            v1010.m128i_i64[0] = v367 - v1012_.m128i_i64[0];
            v388 = (v367 - v1012_.m128i_i64[0]) >> 5;
            v389 = v388 + 1;
            v390 = 0x7FFFFFFFFFFFFFFLL - (v388 >> 1);
            n0x80 = v388 + (v388 >> 1);
            if ( n0x80 <= v388 + 1 )
              n0x80 = v388 + 1;
            if ( v388 > v390 )
              n0x80 = 0x7FFFFFFFFFFFFFFLL;
            if ( n0x80 >> 59 )
              sub_18002D1E0(n0x80 >> 59, v390, 0x7FFFFFFFFFFFFFFLL);
            v392 = 32 * n0x80;
            n0x93_8 = 32 * n0x80;
            if ( n0x80 )
            {
              if ( n0x80 < 0x80 )
              {
                v394 = sub_1800FFF00(32 * n0x80, v392, 0x7FFFFFFFFFFFFFFLL);
              }
              else
              {
                if ( n0x80 == 0x7FFFFFFFFFFFFFFLL )
                  sub_18002D1E0(0x7FFFFFFFFFFFFFFLL, v392, 0x7FFFFFFFFFFFFFFLL);
                v393 = sub_1800FFF00(v392 + 39, v392, 0x7FFFFFFFFFFFFFFLL);
                v394 = (v393 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
                *(_QWORD *)(v394 - 8) = v393;
              }
            }
            else
            {
              v394 = 0;
            }
            n0x1000_6 = v1010.m128i_i64[0];
            *(_DWORD *)(v394 + v1010.m128i_i64[0]) = v386;
            *(_DWORD *)(v394 + n0x1000_6 + 4) = v387;
            n0x1000_1 = n0x1000_6;
            *(_DWORD *)(v394 + n0x1000_6 + 8) = v384;
            *(_QWORD *)(v394 + n0x1000_6 + 16) = v369;
            *(_BYTE *)(v394 + n0x1000_6 + 24) = v1025;
            v397 = v394;
            v398 = (_QWORD *)v1010.m128i_i64[1];
            sub_180200FC0(v394, v1010.m128i_i64[1], n0x1000_6);
            if ( !v398 )
              goto LABEL_532;
            if ( n0x1000_1 < 0x1000 )
              goto LABEL_531;
            if ( (unsigned __int64)v398 - *(v398 - 1) - 8 >= 0x20 )
              goto LABEL_109;
            v398 = (_QWORD *)*(v398 - 1);
LABEL_531:
            sub_1800FFFE0(v398);
LABEL_532:
            v1012_.m128i_i64[0] = v397;
            v368 = v397 + 32 * v389;
            v1012_.m128i_i64[1] = v368;
            v1010.m128i_i64[0] = v397;
            v367 = v397 + n0x93_8;
            v993 = v397 + n0x93_8;
            goto LABEL_510;
          }
          *(_DWORD *)v368 = v386;
          *(_DWORD *)(v368 + 4) = v387;
          *(_DWORD *)(v368 + 8) = v384;
          *(_QWORD *)(v368 + 16) = v369;
          *(_BYTE *)(v368 + 24) = v1025;
          v368 += 32;
          v1012_.m128i_i64[1] = v368;
        }
        v370 = v936;
        v371 = v937;
      }
    }
    v369 += 3LL;
    var150.m128_u64[0] += 12LL;
  }
  sub_1800C08A0(v1010.m128i_i64[0], v368, (v368 - v1010.m128i_i64[0]) >> 5, v370);
  v1012__1 = 0;
  v974 = 0;
  v400 = v1012_.m128i_i64[0];
  v401 = (v1012_.m128i_i64[1] - v1012_.m128i_i64[0]) >> 5;
  v402 = v1012_.m128i_i64[0] + 36;
  v403 = 0;
  while ( v403 < v401 )
  {
    v404 = v403;
    v405 = v403 + 1;
    v403 = v405;
    if ( v405 < v401 )
    {
      v406 = *(_DWORD *)(v400 + 32 * v404 + 4);
      v407 = *(_DWORD *)(v400 + 32 * v404);
      if ( v406 < v407 )
      {
        v407 = *(_DWORD *)(v400 + 32 * v404 + 4);
        v406 = *(_DWORD *)(v400 + 32 * v404);
      }
      v408 = (unsigned __int32 *)(v402 + 32 * v404);
      v403 = v405;
      while ( 1 )
      {
        v409 = *(v408 - 1);
        v410 = *v408;
        v411 = v409;
        if ( *v408 < v409 )
          v411 = *v408;
        v1011[0].m128i_i32[0] = v411;
        if ( v410 < v409 )
          v410 = v409;
        v1011[0].m128i_i32[1] = v410;
        v940[0] = __PAIR64__(v406, v407);
        if ( __PAIR64__(v406, v407) != v1011[0].m128i_i64[0] )
          break;
        ++v403;
        v408 += 8;
        if ( v401 == v403 )
        {
          v403 = v401;
          break;
        }
      }
    }
    v1021_1 = v403 - v404;
    if ( v403 - v404 == 1 )
    {
      v421 = 32 * v404;
      if ( !(v1021 | *(_BYTE *)(v400 + v421 + 24)) )
      {
        v422 = (__m128i *)(v400 + v421);
        v1021_1 = v422->m128i_u32[0];
        v423 = *(float *)(v1021_5 + 28 * v1021_1) - *(double *)&xmmword_18026FB60;
        v424 = *(float *)(v1021_5 + 28 * v1021_1 + 8) - *(double *)&xmmword_18026FB70;
        v0.m128_u64[1] = 0;
        *(double *)v0.m128_u64 = v424 * *((double *)&xmmword_18026FB80 + 1)
                               + (*(float *)(v1021_5 + 28 * v1021_1 + 4) - *((double *)&xmmword_18026FB60 + 1))
                               * *(double *)&xmmword_18026FB80
                               + v423 * *((double *)&xmmword_18026FB70 + 1)
                               + 0.0;
        v425 = *(double *)&qword_18026FB90 * 1.5;
        if ( *(double *)v0.m128_u64 >= *(double *)&qword_18026FB90 * -1.5 && v425 >= *(double *)v0.m128_u64 )
        {
          v426 = v424 - *(double *)v0.m128_u64 * *((double *)&xmmword_18026FB80 + 1);
          v427 = *(float *)(v1021_5 + 28 * v1021_1 + 4)
               - *((double *)&xmmword_18026FB60 + 1)
               - *(double *)v0.m128_u64 * *(double *)&xmmword_18026FB80;
          *(double *)v0.m128_u64 = *(double *)v0.m128_u64 * *((double *)&xmmword_18026FB70 + 1);
          v428 = v426 * v426 + v427 * v427 + (v423 - *(double *)v0.m128_u64) * (v423 - *(double *)v0.m128_u64) + 0.0;
          v429 = *(double *)&qword_18026FB90 * 9.0 * *(double *)&qword_18026FB90;
          if ( v429 >= v428 )
          {
            v1021_1 = v422->m128i_u32[1];
            v430 = *(float *)(v1021_5 + 28 * v1021_1) - *(double *)&xmmword_18026FB60;
            v431 = *(float *)(v1021_5 + 28 * v1021_1 + 4) - *((double *)&xmmword_18026FB60 + 1);
            v432 = *(float *)(v1021_5 + 28 * v1021_1 + 8) - *(double *)&xmmword_18026FB70;
            v433 = v432 * *((double *)&xmmword_18026FB80 + 1)
                 + v431 * *(double *)&xmmword_18026FB80
                 + v430 * *((double *)&xmmword_18026FB70 + 1)
                 + 0.0;
            if ( v433 >= *(double *)&qword_18026FB90 * -1.5
              && v425 >= v433
              && v429 >= (v432 - *((double *)&xmmword_18026FB80 + 1) * v433)
                       * (v432 - *((double *)&xmmword_18026FB80 + 1) * v433)
                       + (v431 - *(double *)&xmmword_18026FB80 * v433) * (v431 - *(double *)&xmmword_18026FB80 * v433)
                       + (v430 - *((double *)&xmmword_18026FB70 + 1) * v433)
                       * (v430 - *((double *)&xmmword_18026FB70 + 1) * v433)
                       + 0.0 )
            {
              v434 = _mm_loadu_si128(v422 + 1);
              v1011[0] = *v422;
              v1011[1] = v434;
              v435 = v1011[0].m128i_i64[0];
              v1011[0].m128i_i32[0] = v1011[0].m128i_i32[1];
              *(__int64 *)((char *)v1011[0].m128i_i64 + 4) = v435;
              v419 = v1012__1.m128i_i64[1];
              if ( v1012__1.m128i_i64[1] != v974 )
                goto LABEL_558;
LABEL_567:
              sub_1800C11D0((LPVOID *)&v1012__1, v419, v1011);
            }
          }
        }
      }
    }
    else if ( v1021_1 == 2 )
    {
      v412 = 32 * v404;
      v1021_1 = *(unsigned int *)(v400 + v412);
      v413 = 32 * v405;
      if ( (_DWORD)v1021_1 == *(_DWORD *)(v400 + v413 + 4) )
      {
        v414 = v400 + v412;
        v415 = v400 + v413;
        v1021_1 = *(unsigned int *)(v414 + 4);
        if ( (_DWORD)v1021_1 == *(_DWORD *)v415 )
        {
          v1021_1 = *(unsigned __int8 *)(v414 + 24);
          if ( (_BYTE)v1021_1 != *(_BYTE *)(v415 + 24) )
          {
            v416 = (_BYTE)v1021_1 == 0;
            v417 = (__m128i *)v415;
            if ( !v416 )
              v417 = (__m128i *)v414;
            v418 = *v417;
            v1011[1] = _mm_loadu_si128(v417 + 1);
            v1011[0] = v418;
            if ( !v416 )
              v414 = v415;
            v1011[1].m128i_i64[0] = *(_QWORD *)(v414 + 16);
            v419 = v1012__1.m128i_i64[1];
            if ( v1012__1.m128i_i64[1] == v974 )
              goto LABEL_567;
LABEL_558:
            v420 = v1011[0];
            *(__m128i *)(v419 + 16) = _mm_load_si128(&v1011[1]);
            *(__m128i *)v419 = v420;
            v1021_1 = v419 + 32;
            v1012__1.m128i_i64[1] = v1021_1;
          }
        }
      }
    }
  }
  v436 = (char *)v1012__1.m128i_i64[0];
  n32 = v1012__1.m128i_i64[1] - v1012__1.m128i_i64[0];
  if ( v1012__1.m128i_i64[1] == v1012__1.m128i_i64[0] || v1012__1.m128i_i64[1] - v1012__1.m128i_i64[0] >= 0x20001uLL )
  {
    v980 = 0;
    v981 = 0;
    goto LABEL_792;
  }
  sub_1800C1340(v940, n0x400, 0xFFFFFFFFLL);
  n0x400_7 = n0x400;
  v943 = 0;
  v944 = 0;
  if ( n0x400 )
  {
    if ( n0x400 >> 62 )
      std::vector<void *>::_Xlen();
    v442 = 4 * n0x400;
    if ( n0x400 < 0x400 )
    {
      v444 = sub_1800FFF00(4 * n0x400, v438, v440);
    }
    else
    {
      if ( n0x400 >= 0x3FFFFFFFFFFFFFF7LL )
        sub_18002D1E0(v439, v438, v440);
      v443 = sub_1800FFF00(v442 + 39, v438, v440);
      v444 = (v443 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
      *(_QWORD *)(v444 - 8) = v443;
    }
    *(_QWORD *)&v943 = v444;
    v445 = v444 + 4 * n0x400_7;
    v944 = v445;
    sub_180201670(v444, 0, v442);
    *((_QWORD *)&v943 + 1) = v445;
  }
  else
  {
    v444 = 0;
  }
  v446 = v940[0];
  v447 = n32 >> 5;
  if ( n32 == 32 )
  {
    v1021_2 = 0;
    goto LABEL_587;
  }
  v1021_1 = (n32 >> 5) & 0x1FFE;
  v449 = (unsigned __int64)(v436 + 36);
  v1021_2 = 0;
  do
  {
    v450 = *(unsigned int *)(v449 - 36);
    v1021_7 = -2;
    if ( *(_DWORD *)(v446 + 4 * v450) == -1 )
      v1021_7 = v1021_2;
    *(_DWORD *)(v446 + 4 * v450) = v1021_7;
    ++*(_DWORD *)(v444 + 4LL * *(unsigned int *)(v449 - 32));
    v452 = *(unsigned int *)(v449 - 4);
    v453 = v1021_2 + 1;
    if ( *(_DWORD *)(v446 + 4 * v452) != -1 )
      v453 = -2;
    *(_DWORD *)(v446 + 4 * v452) = v453;
    ++*(_DWORD *)(v444 + 4LL * *(unsigned int *)v449);
    v1021_2 += 2;
    v449 += 64LL;
  }
  while ( v1021_2 != v1021_1 );
  if ( (n32 & 0x20) != 0 )
  {
LABEL_587:
    v1021_1 = 32 * v1021_2;
    v449 = *(unsigned int *)&v436[32 * v1021_2];
    v1021_3 = -2;
    if ( *(_DWORD *)(v446 + 4 * v449) == -1 )
      v1021_3 = v1021_2;
    *(_DWORD *)(v446 + 4 * v449) = v1021_3;
    ++*(_DWORD *)(v444 + 4LL * *(unsigned int *)&v436[v1021_1 + 4]);
  }
  v455 = (unsigned __int64)(v447 + 31) >> 5;
  memset(v1011, 0, 24);
  if ( v455 )
  {
    v456 = sub_1800FFF00(4 * v455, v1021_1, v449);
    v1011[0].m128i_i64[0] = v456;
    v457 = v456 + 4 * v455;
    v1011[1].m128i_i64[0] = v457;
    sub_180201670(v456, 0, 4 * v455);
    v1011[0].m128i_i64[1] = v457;
  }
  else
  {
    v456 = 0;
    v455 = 0;
    v457 = 0;
  }
  v1011[1].m128i_i64[1] = 0;
  if ( v455 < (v457 - v456) >> 2 && v456 + 4 * v455 != v457 )
    v1011[0].m128i_i64[1] = v456 + 4 * v455;
  v1011[1].m128i_i64[1] = v447;
  v458 = v447 & 0x1F;
  if ( v458 )
    *(_DWORD *)(v456 + 4 * v455 - 4) &= ~(-1 << v458);
  v459 = 0;
  v980 = 0;
  v981 = 0;
  v460 = (__m128d)xmmword_18020CF00;
  v461 = 0;
LABEL_601:
  v436 = (char *)v1012__1.m128i_i64[0];
  v462 = (_DWORD *)v1011[0].m128i_i64[0];
  if ( v461 < (v1012__1.m128i_i64[1] - v1012__1.m128i_i64[0]) >> 5 )
  {
    v463 = *(_DWORD *)(v1011[0].m128i_i64[0] + 4 * (v461 >> 5));
    if ( _bittest(&v463, v461) )
      goto LABEL_600;
    *(_OWORD *)v960 = v459;
    v1021_4 = 0;
    v464 = v940[0];
    v465 = v943;
    v466 = v461;
    do
    {
      v467 = v462[v466 >> 5];
      if ( _bittest(&v467, v466) )
        goto LABEL_772;
      v468 = v960[0];
      v1021_1 = (__int64)v960[1];
      if ( (LPVOID)((char *)v960[1] - (char *)v960[0]) > (LPVOID)0x3FF )
        goto LABEL_773;
      v462[v466 >> 5] = v467 | (1 << v466);
      v469 = 32 * v466;
      v470 = *(unsigned int *)&v436[v469];
      if ( *(int *)(v464 + 4 * v470) < 0 )
        goto LABEL_772;
      if ( *(_DWORD *)(v465 + 4 * v470) != 1 )
        goto LABEL_772;
      v471 = &v436[v469];
      v472 = *((unsigned int *)v471 + 1);
      if ( *(int *)(v464 + 4 * v472) < 0 || *(_DWORD *)(v465 + 4 * v472) != 1 )
        goto LABEL_772;
      v473 = v471 + 8;
      if ( v1021_1 == v1021_4 )
      {
        sub_1800C1460(v960, v1021_1, v473);
      }
      else
      {
        *(_DWORD *)v1021_1 = *v473;
        v1021_1 += 4;
        v960[1] = (LPVOID)v1021_1;
      }
      v466 = *(int *)(v464 + 4LL * *((unsigned int *)v471 + 1));
    }
    while ( v461 != v466 );
    v1025_2 = (unsigned int *)v960[0];
    *(LPVOID *)&v1025 = v960[1];
    n3_1 = ((char *)v960[1] - (char *)v960[0]) >> 2;
    if ( (unsigned __int64)n3_1 < 3 )
      goto LABEL_772;
    if ( v960[0] == (LPVOID)v1025 )
    {
      if ( v1021 )
        goto LABEL_652;
      v1028 = *((double *)&xmmword_18026FB60 + 1);
      v484 = *((double *)&xmmword_18026FB70 + 1);
      v485 = *(double *)&xmmword_18026FB60;
      v486 = *(double *)&xmmword_18026FB80;
      var150.m128_u64[0] = xmmword_18026FB70;
      v487 = 0;
      v488 = 1;
      v489 = 0.0;
      v490 = *((double *)&xmmword_18026FB80 + 1);
      goto LABEL_648;
    }
    v476 = 1;
    v1021_1 = v1021_5;
    v477 = (unsigned int *)v960[0];
    while ( 1 )
    {
      v478 = (_WORD *)(v1021_5 + 28LL * *v477);
      if ( v478[6] && (v479 = (unsigned __int16)v478[10], v930 > v479) )
      {
        v480 = *(_BYTE *)(v1025_11 + v479) == 2;
        if ( v478[7] )
          goto LABEL_623;
      }
      else
      {
        v480 = 0;
        if ( v478[7] )
        {
LABEL_623:
          v481 = (unsigned __int16)v478[11];
          if ( v930 > v481 && *(_BYTE *)(v1025_11 + v481) == 2 )
            v480 = 1;
        }
      }
      if ( v478[8] )
      {
        v482 = (unsigned __int16)v478[12];
        if ( v930 > v482 && *(_BYTE *)(v1025_11 + v482) == 2 )
          v480 = 1;
      }
      if ( v478[9] )
      {
        v483 = (unsigned __int16)v478[13];
        if ( v930 > v483 && *(_BYTE *)(v1025_11 + v483) == 2 )
          v480 = 1;
      }
      v476 &= v480;
      if ( ++v477 == (unsigned int *)v1025 )
      {
        if ( v1021 )
        {
          v1025_2 = (unsigned int *)v960[0];
          if ( !v476 )
            goto LABEL_772;
LABEL_652:
          v508 = 0;
          v509 = 0.0;
          for ( kk = 1; kk != n3_1; ++kk )
          {
            v511 = 28LL * v1025_2[kk - 1];
            kk_1 = 0;
            if ( n3_1 != kk )
              kk_1 = kk;
            v513 = 28LL * v1025_2[kk_1];
            v514 = *(float *)(v1021_5 + v513);
            v515 = (__m128)*(unsigned __int64 *)(v1021_5 + v513 + 4);
            v516 = (__m128)*(unsigned int *)(v1021_5 + 28LL * v1025_2[kk - 1]);
            v516.m128_f32[0] = v516.m128_f32[0] + v514;
            v517 = _mm_add_ps((__m128)*(unsigned __int64 *)(v1021_5 + v511 + 4), v515);
            v508 = _mm_add_pd(
                     v508,
                     _mm_cvtps_pd(
                       _mm_mul_ps(
                         _mm_shuffle_ps(_mm_shuffle_ps(v516, v517, 212), v517, 82),
                         _mm_sub_ps((__m128)*(unsigned __int64 *)(v1021_5 + v511 + 4), v515))));
            v509 = v509 + (float)((float)(*(float *)(v1021_5 + 28LL * v1025_2[kk - 1]) - v514) * v517.m128_f32[0]);
          }
          v518 = 28LL * *(unsigned int *)((char *)v1025_2 + (char *)v960[1] - (char *)v960[0] - 4);
          v519 = 28LL * *v1025_2;
          v520 = *(float *)(v1021_5 + v519);
          v521 = (__m128)*(unsigned __int64 *)(v1021_5 + v519 + 4);
          v522 = (__m128)*(unsigned int *)(v1021_5 + v518);
          v522.m128_f32[0] = v522.m128_f32[0] + v520;
          v523 = _mm_add_ps((__m128)*(unsigned __int64 *)(v1021_5 + v518 + 4), v521);
          v524 = _mm_add_pd(
                   v508,
                   _mm_cvtps_pd(
                     _mm_mul_ps(
                       _mm_shuffle_ps(_mm_shuffle_ps(v522, v523, 212), v523, 82),
                       _mm_sub_ps((__m128)*(unsigned __int64 *)(v1021_5 + v518 + 4), v521))));
          v525 = (__m128i)_mm_and_pd(v524, v460);
          v526 = *(double *)_mm_shuffle_epi32(v525, 238).m128i_i64;
          n2_2 = 0;
          if ( v526 > *(double *)v525.m128i_i64 )
            *(_QWORD *)&v524.m128d_f64[0] = *(_OWORD *)&_mm_unpackhi_pd(v524, v524);
          LOBYTE(n2_2) = v526 > *(double *)v525.m128i_i64;
          if ( COERCE_DOUBLE(
                 COERCE_UNSIGNED_INT64(
                   v509
                 + (float)(v523.m128_f32[0]
                         * (float)(*(float *)(v1021_5
                                            + 28LL
                                            * *(unsigned int *)((char *)v1025_2 + (char *)v960[1] - (char *)v960[0] - 4))
                                 - v520)))
               & *(_QWORD *)&v460.m128d_f64[0]) > COERCE_DOUBLE(*(_QWORD *)&v524.m128d_f64[0] & *(_QWORD *)&v460.m128d_f64[0]) )
            n2_2 = 2;
          *(_OWORD *)v962 = v459;
          v963 = 0;
          v528 = (unsigned int)(n2_2 + 1);
          if ( n2_2 == 2 )
            v528 = 0;
          v529 = n2_2 == 0;
          n2_3 = (unsigned int)(n2_2 - 1);
          if ( v529 )
            n2_3 = 2;
          v531 = 0;
          var150.m128_u64[0] = v528;
          if ( v1025_2 != (unsigned int *)v1025 )
          {
            do
            {
              while ( 1 )
              {
                v532 = v1021_5 + 28LL * *v1025_2;
                v533 = *(float *)(v532 + 4 * v528);
                v534 = *(float *)(v532 + 4 * n2_3);
                if ( v531 == v963 )
                  break;
                *v531 = v533;
                v531[1] = v534;
                v531 = (double *)((char *)v962[1] + 16);
                v962[1] = (char *)v962[1] + 16;
                if ( ++v1025_2 == (unsigned int *)v1025 )
                  goto LABEL_685;
              }
              v1028 = *(double *)&v1025_2;
              v535 = v962[0];
              n0x1000_2 = (char *)v531 - (char *)v962[0];
              n0x100_3 = (n0x1000_2 >> 4) + 1;
              v538 = (unsigned __int64)(n0x1000_2 >> 4) >> 1;
              v539 = 0xFFFFFFFFFFFFFFFLL - v538;
              n0x100_1 = (n0x1000_2 >> 4) + v538;
              if ( n0x100_1 <= n0x100_3 )
                n0x100_1 = (n0x1000_2 >> 4) + 1;
              if ( n0x1000_2 >> 4 > v539 )
                n0x100_1 = 0xFFFFFFFFFFFFFFFLL;
              if ( n0x100_1 >> 60 )
                sub_18002D1E0(n0x100_1 >> 60, v539, 0xFFFFFFFFFFFFFFFLL);
              v541 = 2 * n0x100_1;
              if ( n0x100_1 )
              {
                if ( n0x100_1 < 0x100 )
                {
                  v543 = (_QWORD *)sub_1800FFF00(16 * n0x100_1, v539, 0xFFFFFFFFFFFFFFFLL);
                }
                else
                {
                  if ( n0x100_1 >= 0xFFFFFFFFFFFFFFELL )
                    sub_18002D1E0(0xFFFFFFFFFFFFFFELL, v539, 0xFFFFFFFFFFFFFFFLL);
                  v542 = sub_1800FFF00(v541 * 8 + 39, v539, 0xFFFFFFFFFFFFFFFLL);
                  v543 = (_QWORD *)((v542 + 39) & 0xFFFFFFFFFFFFFFE0uLL);
                  *(v543 - 1) = v542;
                }
              }
              else
              {
                v543 = 0;
              }
              *(double *)((char *)v543 + n0x1000_2) = v533;
              *(double *)((char *)v543 + n0x1000_2 + 8) = v534;
              sub_180200FC0(v543, v535, n0x1000_2);
              if ( v535 )
              {
                if ( (unsigned __int64)n0x1000_2 >= 0x1000 )
                {
                  if ( (unsigned __int64)v535 - *(v535 - 1) - 8 >= 0x20 )
                    goto LABEL_109;
                  v535 = (_QWORD *)*(v535 - 1);
                }
                sub_1800FFFE0(v535);
              }
              v962[0] = v543;
              v962[1] = &v543[2 * n0x100_3];
              v963 = (double *)&v543[v541];
              v1025_2 = (unsigned int *)(*(_QWORD *)&v1028 + 4LL);
              v531 = (double *)v962[1];
              v528 = var150.m128_u64[0];
            }
            while ( *(_QWORD *)&v1028 + 4LL != (_QWORD)v1025 );
          }
LABEL_685:
          v544 = 0;
          v545 = (double *)v962[0];
          v546 = (char *)v531 - (char *)v962[0];
          n16 = (char *)v531 - (char *)v962[0];
          if ( n16 )
          {
            n2_4 = v546 >> 4;
            v549 = *(__m128d *)v962[0];
            v550 = 0.0;
            n2_7 = 1;
            if ( n16 == 16 )
            {
              v552 = *(__m128d *)v962[0];
              v553 = 0.0;
            }
            else
            {
              v554 = (__m128d *)v962[0];
              v553 = 0.0;
              do
              {
                n2_10 = 0;
                if ( n2_4 != n2_7 )
                  n2_10 = n2_7;
                v550 = v550
                     + *((double *)v962[0] + 2 * n2_10 + 1) * v554->m128d_f64[0]
                     - _mm_unpackhi_pd(*v554, *v554).m128d_f64[0] * *((double *)v962[0] + 2 * n2_10);
                v556 = (__m128i)_mm_and_pd(_mm_sub_pd(*v554, v549), v460);
                v553 = fmax(fmax(*(double *)_mm_shuffle_epi32(v556, 238).m128i_i64, *(double *)v556.m128i_i64), v553);
                ++n2_7;
                ++v554;
              }
              while ( n2_4 != n2_7 );
              v552 = *(__m128d *)((char *)v962[0] + n16 - 16);
              n2_7 = n2_4;
            }
            n2_8 = 0;
            if ( n2_7 != n2_4 )
              n2_8 = n2_7;
            v558 = *((double *)v962[0] + 2 * n2_8 + 1) * v552.m128d_f64[0]
                 - _mm_unpackhi_pd(v552, v552).m128d_f64[0] * *((double *)v962[0] + 2 * n2_8)
                 + v550;
            v559 = (__m128i)_mm_and_pd(_mm_sub_pd(v552, v549), v460);
            v0 = (__m128)_mm_shuffle_epi32(v559, 238);
            *(double *)v0.m128_u64 = fmax(fmax(*(double *)v0.m128_u64, *(double *)v559.m128i_i64), v553);
            *(double *)v0.m128_u64 = fmax(*(double *)v0.m128_u64 * *(double *)v0.m128_u64 * 1.0e-10, 1.0e-16);
            if ( *(double *)v0.m128_u64 < COERCE_DOUBLE(*(_QWORD *)&v558 & *(_QWORD *)&v460.m128d_f64[0]) )
            {
              *(double *)&v1025_3 = -*(double *)v0.m128_u64;
              *((double *)&v1025_3 + 1) = -*(double *)&v0.m128_u64[1];
              v1025 = v1025_3;
              var150 = v0;
              if ( n2_4 >= 2 )
              {
                v561 = 1;
                v562 = 0;
                v563 = 0;
                while ( 1 )
                {
                  v564 = v563;
                  v563 = v561;
                  v565 = v564 + 2;
                  if ( v564 + 2 != n2_4 )
                    break;
LABEL_699:
                  ++v561;
                  --v562;
                  if ( v563 + 1 == n2_4 )
                    goto LABEL_717;
                }
                v566 = (double *)((char *)v962[0] + 16 * v561);
                LOBYTE(v513) = 1;
                LODWORD(v1028) = v513;
                while ( 2 )
                {
                  v567 = (double *)((char *)v962[0] + 16 * v565++ + 8);
                  while ( 1 )
                  {
                    if ( v562 + v565 != 2 )
                    {
                      v568 = v565 - n2_4;
                      v569 = (double *)((char *)v962[0] + 16 * v564);
                      if ( v564 || (v569 = (double *)v962[0], v568) )
                      {
                        v570 = v565;
                        if ( !v568 )
                          v570 = 0;
                        v571 = 16 * v570;
                        v572 = *v569;
                        v573 = *v566;
                        v574 = *(v567 - 1);
                        v575 = *(double *)((char *)v962[0] + v571);
                        if ( fmin(fmax(v575, v574), fmax(*v566, v572)) + *(double *)v0.m128_u64 >= fmax(
                                                                                                     fmin(v575, v574),
                                                                                                     fmin(*v566, v572)) )
                        {
                          v576 = v569[1];
                          v577 = v566[1];
                          v578 = *v567;
                          v579 = *(double *)((char *)v962[0] + v571 + 8);
                          if ( fmin(fmax(v579, v578), fmax(v577, v576)) + *(double *)v0.m128_u64 >= fmax(fmin(v579, v578), fmin(v577, v576)) )
                          {
                            v580 = (v578 - v576) * (v573 - v572) - (v574 - v572) * (v577 - v576);
                            v460 = (__m128d)xmmword_18020CF00;
                            v581 = (v579 - v576) * (v573 - v572) - (v575 - v572) * (v577 - v576);
                            v0 = var150;
                            if ( *(double *)var150.m128_u64 >= fmin(v581, v580) && fmax(v581, v580) >= *(double *)&v1025 )
                            {
                              v582 = v575 - v574;
                              v583 = v579 - v578;
                              v584 = (v576 - v578) * v582 - (v572 - v574) * v583;
                              v585 = (v577 - v578) * v582 - (v573 - v574) * v583;
                              if ( *(double *)var150.m128_u64 >= fmin(v585, v584)
                                && fmax(v585, v584) >= *(double *)&v1025 )
                              {
                                break;
                              }
                            }
                          }
                        }
                      }
                    }
                    v567 += 2;
                    v513 = v565 - n2_4 + 1;
                    ++v565;
                    if ( v513 == 1 )
                    {
                      if ( (LOBYTE(v1028) & 1) == 0 )
                        goto LABEL_771;
                      goto LABEL_699;
                    }
                  }
                  LODWORD(v1028) = 0;
                  if ( v568 )
                    continue;
                  goto LABEL_771;
                }
              }
LABEL_717:
              v1010.m128i_i64[0] = (__int64)v960[0];
              v586 = ((char *)v960[1] - (char *)v960[0]) >> 2;
              sub_1800C15C0(&n0x93_10, v586);
              n0x93_13 = (char *)*((_QWORD *)&n0x93_10 + 1);
              n0x93_11 = (char *)n0x93_10;
              v589 = (__int64)(*((_QWORD *)&n0x93_10 + 1) - n0x93_10) >> 3;
              if ( HIDWORD(v589) )
              {
                if ( (_QWORD)n0x93_10 != *((_QWORD *)&n0x93_10 + 1) )
                {
                  n0x17 = *((_QWORD *)&n0x93_10 + 1) - n0x93_10 - 8;
                  LODWORD(v593) = 0;
                  n0x93_12 = (char *)n0x93_10;
                  if ( n0x17 > 0x17 )
                  {
                    v595 = (n0x17 >> 3) + 1;
                    v593 = v595 & 0xFFFFFFFFFFFFFFFCuLL;
                    v596 = 0;
                    v597 = _mm_loadl_epi64((const __m128i *)&qword_18021DD50);
                    v598 = _mm_load_si128((const __m128i *)&xmmword_18021DD60);
                    v599 = _mm_load_si128((const __m128i *)&xmmword_18021DD70);
                    do
                    {
                      *(__m128i *)&n0x93_11[8 * v596] = _mm_unpacklo_epi32(v597, (__m128i)0LL);
                      *(__m128i *)&n0x93_11[8 * v596 + 16] = _mm_unpacklo_epi32(_mm_add_epi32(v597, v598), (__m128i)0LL);
                      v596 += 4;
                      v597 = _mm_add_epi32(v597, v599);
                    }
                    while ( v593 != v596 );
                    v544 = 0;
                    if ( v595 == v593 )
                      goto LABEL_734;
                    n0x93_12 = &n0x93_11[8 * v593];
                  }
                  do
                  {
                    *(_QWORD *)n0x93_12 = (unsigned int)v593;
                    n0x93_12 += 8;
                    LODWORD(v593) = v593 + 1;
                  }
                  while ( n0x93_12 != n0x93_13 );
                }
              }
              else if ( *((_QWORD *)&n0x93_10 + 1) != (_QWORD)n0x93_10 )
              {
                n3_2 = (unsigned int)(v589 - 1);
                if ( (unsigned int)n3_2 < 3 )
                {
                  v591 = 0;
                  goto LABEL_732;
                }
                v600 = n3_2 + 1;
                v591 = v600 & 0xFFFFFFFFFFFFFFFCuLL;
                v601 = 0;
                v602 = _mm_load_si128((const __m128i *)&xmmword_18021DD80);
                v603 = _mm_load_si128((const __m128i *)&xmmword_18021DD90);
                v604 = _mm_load_si128((const __m128i *)&xmmword_18021DDA0);
                do
                {
                  *(__m128i *)&n0x93_11[v601] = v602;
                  *(__m128i *)&n0x93_11[v601 + 16] = _mm_add_epi64(v602, v603);
                  v602 = _mm_add_epi64(v602, v604);
                  v601 += 32LL;
                }
                while ( ((8 * v600) & 0xFFFFFFFFFFFFFFE0uLL) != v601 );
                if ( v600 != v591 )
                {
LABEL_732:
                  v605 = v589 - v591;
                  do
                  {
                    *(_QWORD *)&n0x93_11[8 * v591] = v591;
                    ++v591;
                    --v605;
                  }
                  while ( v605 );
                }
              }
LABEL_734:
              v1010.m128i_i64[1] = v586;
              v948 = v544;
              v949 = 0;
              v606 = dbl_18021DDB0[v558 > 0.0];
              while ( (unsigned __int64)(n0x93_13 - n0x93_11) >= 0x11 && n0x93_13 != n0x93_11 )
              {
                v608 = (n0x93_13 - n0x93_11) >> 3;
                v609 = v608 - 1;
                v610 = 0;
                while ( 1 )
                {
                  if ( (v608 | (v610 + v609)) >> 32 )
                    v611 = (v610 + v609) % v608;
                  else
                    v611 = ((int)v610 + (int)v609) % (unsigned int)v608;
                  v612 = *(_QWORD *)&n0x93_11[8 * v611];
                  v613 = *(_QWORD *)&n0x93_11[8 * v610];
                  v614 = v610 + 1;
                  v615 = 0;
                  if ( v610 + 1 != v608 )
                    v615 = v610 + 1;
                  v616 = *(_QWORD *)&n0x93_11[8 * v615];
                  v617 = v545[2 * v613];
                  v618 = v545[2 * v613 + 1];
                  v619 = v545[2 * v612];
                  v620 = v545[2 * v612 + 1];
                  v621 = v617 - v619;
                  v622 = v545[2 * v616];
                  v0 = (__m128)*(unsigned __int64 *)&v545[2 * v616 + 1];
                  v623 = v618 - v620;
                  v624 = ((v545[2 * v616 + 1] - v620) * (v617 - v619) - (v622 - v619) * (v618 - v620)) * v606;
                  if ( *(double *)var150.m128_u64 >= COERCE_DOUBLE(*(_QWORD *)&v624 & *(_QWORD *)&v460.m128d_f64[0])
                    && *(double *)var150.m128_u64 >= (v617 - v622) * v621 + (v618 - *(double *)v0.m128_u64) * v623 )
                  {
                    break;
                  }
                  if ( *(double *)var150.m128_u64 < v624 )
                  {
                    v1028 = v619 - v622;
                    n0x93_14 = n0x93_11;
                    while ( 1 )
                    {
                      v626 = *(_QWORD *)n0x93_14;
                      if ( *(_QWORD *)n0x93_14 != v616 && v626 != v612 && v626 != v613 )
                      {
                        v627 = 2 * v626;
                        v628 = v545[v627];
                        v629 = v545[v627 + 1];
                        if ( ((v629 - v620) * v621 - (v628 - v619) * v623) * v606 >= *(double *)&v1025
                          && ((v629 - v618) * (v622 - v617) - (v628 - v617) * (*(double *)v0.m128_u64 - v618)) * v606 >= *(double *)&v1025
                          && ((v629 - *(double *)v0.m128_u64) * v1028 - (v628 - v622) * (v620 - *(double *)v0.m128_u64))
                           * v606 >= *(double *)&v1025 )
                        {
                          break;
                        }
                      }
                      n0x93_14 += 8;
                      if ( n0x93_14 == n0x93_13 )
                      {
                        LODWORD(v968) = *(_DWORD *)(v1010.m128i_i64[0] + 4 * v612);
                        HIDWORD(v968) = *(_DWORD *)(v1010.m128i_i64[0] + 4 * v613);
                        v969 = *(_DWORD *)(v1010.m128i_i64[0] + 4 * v616);
                        v607 = (_QWORD *)*((_QWORD *)&v948 + 1);
                        if ( *((_QWORD *)&v948 + 1) == v949 )
                        {
                          sub_1800C1660(&v948, *((_QWORD *)&v948 + 1), &v968);
                        }
                        else
                        {
                          *(_DWORD *)(*((_QWORD *)&v948 + 1) + 8LL) = v969;
                          *v607 = v968;
                          *((_QWORD *)&v948 + 1) = (char *)v607 + 12;
                        }
                        v460 = (__m128d)xmmword_18020CF00;
                        goto LABEL_738;
                      }
                    }
                  }
                  ++v610;
                  v460 = (__m128d)xmmword_18020CF00;
                  if ( v614 == v608 )
                    goto LABEL_764;
                }
                LODWORD(v968) = *(_DWORD *)(v1010.m128i_i64[0] + 4 * v612);
                HIDWORD(v968) = *(_DWORD *)(v1010.m128i_i64[0] + 4 * v613);
                v969 = *(_DWORD *)(v1010.m128i_i64[0] + 4 * v616);
                v630 = (_QWORD *)*((_QWORD *)&v948 + 1);
                if ( *((_QWORD *)&v948 + 1) == v949 )
                {
                  sub_1800C1660(&v948, *((_QWORD *)&v948 + 1), &v968);
                }
                else
                {
                  *(_DWORD *)(*((_QWORD *)&v948 + 1) + 8LL) = v969;
                  *v630 = v968;
                  *((_QWORD *)&v948 + 1) = (char *)v630 + 12;
                }
LABEL_738:
                sub_180200FC0(&n0x93_11[8 * v610], &n0x93_11[8 * v610 + 8], n0x93_13 - &n0x93_11[8 * v610 + 8]);
                n0x93_13 -= 8;
                *((_QWORD *)&n0x93_10 + 1) = n0x93_13;
              }
LABEL_764:
              v631 = (__int64 *)*((_QWORD *)&v948 + 1);
              v632 = (__int64 *)v948;
              if ( 0xAAAAAAAAAAAAAAABuLL * ((__int64)(*((_QWORD *)&v948 + 1) - v948) >> 2) == v1010.m128i_i64[1] - 2 )
              {
                v633 = v1012__1.m128i_i64[0] + 32 * v461;
                if ( (_QWORD)v948 != *((_QWORD *)&v948 + 1) )
                {
                  do
                  {
                    while ( 1 )
                    {
                      v969 = *((_DWORD *)v632 + 2);
                      v968 = *v632;
                      v971 = *(_QWORD *)(v633 + 16);
                      v634 = *((_QWORD *)&v980 + 1);
                      if ( *((_QWORD *)&v980 + 1) == v981 )
                        break;
                      *(_QWORD *)(*((_QWORD *)&v980 + 1) + 16LL) = v971;
                      *(_QWORD *)v634 = v968;
                      *(_DWORD *)(v634 + 8) = v969;
                      *(_DWORD *)(v634 + 12) = v970;
                      *((_QWORD *)&v980 + 1) = v634 + 24;
                      v632 = (__int64 *)((char *)v632 + 12);
                      if ( v632 == v631 )
                        goto LABEL_770;
                    }
                    sub_1800C1800(&v980, *((_QWORD *)&v980 + 1), &v968);
                    v632 = (__int64 *)((char *)v632 + 12);
                  }
                  while ( v632 != v631 );
                }
              }
LABEL_770:
              sub_1800C1990(&v948);
              sub_180035CE0(&n0x93_10);
            }
          }
LABEL_771:
          sub_1800C19F0(v962);
          v459 = 0;
          goto LABEL_772;
        }
        *(double *)v1010.m128i_i64 = *(double *)&qword_18026FB90 * 1.5;
        v1010.m128i_i64[1] = qword_18026FB90;
        v1025_2 = (unsigned int *)v960[0];
        if ( n3_1 < 0 )
        {
          v491 = ((unsigned __int64)(((char *)v960[1] - (char *)v960[0]) >> 2) >> 1)
               | (((char *)v960[1] - (char *)v960[0]) >> 2) & 1;
          v0.m128_f32[0] = (float)(int)v491 + (float)(int)v491;
        }
        else
        {
          v0.m128_f32[0] = (float)(int)n3_1;
        }
        v485 = *(double *)&xmmword_18026FB60;
        v484 = *((double *)&xmmword_18026FB70 + 1);
        v1028 = *((double *)&xmmword_18026FB60 + 1);
        v486 = *(double *)&xmmword_18026FB80;
        var150.m128_u64[0] = xmmword_18026FB70;
        v490 = *((double *)&xmmword_18026FB80 + 1);
        *(double *)&v1010.m128i_i64[1] = *(double *)&v1010.m128i_i64[1] * (*(double *)&qword_18026FB90 * 9.0);
        v492 = _mm_shuffle_ps(v0, v0, 0);
        v493 = 0;
        v488 = 1;
        v494 = 0.0;
        v1025_4 = (unsigned int *)v960[0];
        do
        {
          v496 = 28LL * *v1025_4;
          v497 = (__m128)*(unsigned __int64 *)(v1021_5 + v496);
          v498 = _mm_shuffle_ps(v497, v497, 85).m128_f32[0] - v1028;
          v499 = *(float *)(v1021_5 + v496 + 8);
          v500 = v499 - *(double *)var150.m128_u64;
          v501 = v500 * *((double *)&xmmword_18026FB80 + 1)
               + v498 * *(double *)&xmmword_18026FB80
               + (v497.m128_f32[0] - *(double *)&xmmword_18026FB60) * *((double *)&xmmword_18026FB70 + 1)
               + 0.0;
          v502 = 0;
          if ( v501 >= *(double *)&qword_18026FB90 * -1.5 && *(double *)v1010.m128i_i64 >= v501 )
            v502 = *(double *)&v1010.m128i_i64[1] >= (v500 - v501 * *((double *)&xmmword_18026FB80 + 1))
                                                   * (v500 - v501 * *((double *)&xmmword_18026FB80 + 1))
                                                   + (v498 - v501 * *(double *)&xmmword_18026FB80)
                                                   * (v498 - v501 * *(double *)&xmmword_18026FB80)
                                                   + (v497.m128_f32[0]
                                                    - *(double *)&xmmword_18026FB60
                                                    - v501 * *((double *)&xmmword_18026FB70 + 1))
                                                   * (v497.m128_f32[0]
                                                    - *(double *)&xmmword_18026FB60
                                                    - v501 * *((double *)&xmmword_18026FB70 + 1))
                                                   + 0.0;
          v488 &= v502;
          v493 = _mm_add_ps(v493, _mm_div_ps(v497, v492));
          v494 = v494 + (float)(v499 / v0.m128_f32[0]);
          ++v1025_4;
        }
        while ( v1025_4 != (unsigned int *)v1025 );
        v487 = (__m128)_mm_cvtps_pd(v493);
        v489 = v494;
        v459 = 0;
        v460 = (__m128d)xmmword_18020CF00;
LABEL_648:
        if ( v488 )
        {
          v503 = *(double *)v487.m128_u64 - v485;
          v504 = (*(double *)v487.m128_u64 - v485) * v484 + 0.0;
          v505 = *(double *)_mm_movehl_ps(v487, v487).m128_u64 - v1028;
          v506 = v489 - *(double *)var150.m128_u64;
          v507 = v506 * v490 + v505 * v486 + v504;
          if ( -*(double *)&qword_18026FB90 <= v507
            && v507 <= *(double *)&qword_18026FB90 * 1.25
            && (v506 - v490 * v507) * (v506 - v490 * v507)
             + (v505 - v486 * v507) * (v505 - v486 * v507)
             + (v503 - v484 * v507) * (v503 - v484 * v507)
             + 0.0 <= *(double *)&qword_18026FB90 * *(double *)&qword_18026FB90 * 0.64 )
          {
            goto LABEL_652;
          }
        }
LABEL_772:
        v468 = v960[0];
LABEL_773:
        if ( !v468 )
          goto LABEL_600;
        if ( (unsigned __int64)(v1021_4 - (_QWORD)v468) < 0x1000 )
          goto LABEL_599;
        if ( (unsigned __int64)v468 - *(v468 - 1) - 8 >= 0x20 )
          goto LABEL_109;
        v468 = (_QWORD *)*(v468 - 1);
LABEL_599:
        sub_1800FFFE0(v468);
LABEL_600:
        ++v461;
        goto LABEL_601;
      }
    }
  }
  if ( !v1011[0].m128i_i64[0] )
    goto LABEL_782;
  if ( v1011[1].m128i_i64[0] - v1011[0].m128i_i64[0] < 0x1000uLL )
    goto LABEL_781;
  if ( (unsigned __int64)(v1011[0].m128i_i64[0] - 8 - *(_QWORD *)(v1011[0].m128i_i64[0] - 8)) >= 0x20 )
    goto LABEL_109;
  v462 = *(_DWORD **)(v1011[0].m128i_i64[0] - 8);
LABEL_781:
  sub_1800FFFE0(v462);
LABEL_782:
  v635 = (void *)v943;
  v1006_1 = v1006;
  if ( !(_QWORD)v943 )
    goto LABEL_787;
  if ( v944 - (unsigned __int64)v943 < 0x1000 )
    goto LABEL_786;
  if ( (unsigned __int64)(v943 - 8 - *(_QWORD *)(v943 - 8)) >= 0x20 )
    goto LABEL_109;
  v635 = *(void **)(v943 - 8);
LABEL_786:
  sub_1800FFFE0(v635);
  v1006_1 = v1006;
LABEL_787:
  v636 = (void *)v940[0];
  if ( !v940[0] )
    goto LABEL_793;
  if ( v940[2] - v940[0] < 0x1000u )
    goto LABEL_791;
  if ( (unsigned __int64)(v940[0] - 8LL - *(_QWORD *)(v940[0] - 8LL)) >= 0x20 )
    goto LABEL_109;
  v636 = *(void **)(v940[0] - 8LL);
LABEL_791:
  sub_1800FFFE0(v636);
LABEL_792:
  v1006_1 = v1006;
LABEL_793:
  if ( !v436 )
    goto LABEL_798;
  if ( (unsigned __int64)(v974 - (_QWORD)v436) < 0x1000 )
    goto LABEL_797;
  if ( (unsigned __int64)&v436[-*((_QWORD *)v436 - 1) - 8] >= 0x20 )
    goto LABEL_109;
  v436 = (char *)*((_QWORD *)v436 - 1);
LABEL_797:
  sub_1800FFFE0(v436);
  v1006_1 = v1006;
LABEL_798:
  v399 = (_QWORD *)v1012_.m128i_i64[0];
  if ( !v1012_.m128i_i64[0] )
    goto LABEL_803;
LABEL_799:
  if ( (unsigned __int64)(v993 - (_QWORD)v399) < 0x1000 )
    goto LABEL_802;
  if ( (unsigned __int64)v399 - *(v399 - 1) - 8 >= 0x20 )
    goto LABEL_109;
  v399 = (_QWORD *)*(v399 - 1);
LABEL_802:
  sub_1800FFFE0(v399);
  v1006_1 = v1006;
LABEL_803:
  n0x93_15 = (void *)n0x93_4;
  if ( !(_QWORD)n0x93_4 )
    goto LABEL_808;
  if ( v991 - (unsigned __int64)n0x93_4 < 0x1000 )
    goto LABEL_807;
  if ( (unsigned __int64)(n0x93_4 - 8 - *(_QWORD *)(n0x93_4 - 8)) >= 0x20 )
    goto LABEL_109;
  n0x93_15 = *(void **)(n0x93_4 - 8);
LABEL_807:
  sub_1800FFFE0(n0x93_15);
  v1006_1 = v1006;
LABEL_808:
  v1025_10 = p_n0x93.m128i_i64[0];
  if ( !p_n0x93.m128i_i64[0] )
    goto LABEL_814;
  if ( v998 - p_n0x93.m128i_i64[0] < 0x1000 )
    goto LABEL_812;
  if ( (unsigned __int64)(p_n0x93.m128i_i64[0] - 8 - *(_QWORD *)(p_n0x93.m128i_i64[0] - 8)) >= 0x20 )
    goto LABEL_109;
  v1025_10 = *(_QWORD *)(p_n0x93.m128i_i64[0] - 8);
LABEL_812:
  sub_1800FFFE0((LPVOID)v1025_10);
LABEL_813:
  v1006_1 = v1006;
LABEL_814:
  v638 = (_QWORD *)v975[0];
  v639 = v975[1];
  v640 = v975[1] - v975[0];
  if ( v975[1] - v975[0] != v1006_1 )
  {
    v641 = v980;
    if ( (_QWORD)v980 == *((_QWORD *)&v980 + 1) )
      goto LABEL_882;
LABEL_819:
    memset(v1011, 0, 24);
    n0x400_8 = (v640 >> 2) + ((__int64)(*((_QWORD *)&v641 + 1) - v641) >> 3);
    if ( n0x400_8 )
    {
      if ( n0x400_8 >> 62 )
        std::vector<void *>::_Xlen(v1025_10, v1021_1, v1006_1);
      v643 = 4 * n0x400_8;
      if ( n0x400_8 < 0x400 )
      {
        v638 = (_QWORD *)sub_1800FFF00(v643, v1021_1, v1006_1);
      }
      else
      {
        v644 = sub_1800FFF00(v643 + 39, v1021_1, v1006_1);
        v638 = (_QWORD *)((v644 + 39) & 0xFFFFFFFFFFFFFFE0uLL);
        *(v638 - 1) = v644;
      }
      v1011[0].m128i_i64[0] = (__int64)v638;
      v1011[0].m128i_i64[1] = (__int64)v638;
      v1011[1].m128i_i64[0] = (__int64)v638 + 4 * n0x400_8;
    }
    else
    {
      v638 = 0;
    }
    p_n0x93 = 0;
    v998 = 0;
    v645 = qword_18026F598(v1022);
    ((void (__fastcall *)(__int64, __m128i *))v947.m128i_i64[1])(v645, &p_n0x93);
    v646 = (__int64)v638;
    for ( mm = (__m128i *)v1020; ; mm = v1020_7 + 3 )
    {
      if ( mm == mm_1 )
      {
        v680 = (void *)v975[0];
        if ( v975[0] )
        {
          if ( v975[2] - v975[0] >= 0x1000u )
          {
            if ( (unsigned __int64)(v975[0] - 8LL - *(_QWORD *)(v975[0] - 8LL)) >= 0x20 )
              goto LABEL_109;
            v680 = *(void **)(v975[0] - 8LL);
          }
          sub_1800FFFE0(v680);
        }
        v975[0] = v638;
        v639 = v1011[0].m128i_i64[1];
        *(__m128i *)&v975[1] = _mm_loadu_si128((const __m128i *)&v1011[0].m128i_u64[1]);
        v640 = v1011[0].m128i_i64[1] - (_QWORD)v638;
        goto LABEL_882;
      }
      *(_QWORD *)&v1028_3 = mm[1].m128i_i32[3];
      v1025_5 = mm[2].m128i_i32[0];
      v650 = v646 - (_QWORD)v638;
      v1020 = (__int64)mm;
      mm[1].m128i_i32[3] = (unsigned __int64)(v646 - (_QWORD)v638) >> 2;
      v1028 = v1028_3;
      if ( v1025_5 )
      {
        v651 = v975[0] + 4LL * *(_QWORD *)&v1028_3;
        if ( (v1011[1].m128i_i64[0] - v646) >> 2 >= v1025_5 )
        {
          v1011[0].m128i_i64[1] = v646 + 4 * v1025_5;
          v1025_12 = v1025_5;
          sub_180200FC0(v646, v975[0] + 4LL * *(_QWORD *)&v1028_3, 4 * v1025_5);
          v1025_5 = v1025_12;
        }
        else
        {
          if ( 0x3FFFFFFFFFFFFFFFLL - (v650 >> 2) < v1025_5 )
            std::vector<void *>::_Xlen(v1025_5, 0x3FFFFFFFFFFFFFFFLL, v1006_1);
          *(_QWORD *)&v1025 = v1025_5;
          n0x400_11 = v1025_5 + (v650 >> 2);
          n0x1000_3 = v1011[1].m128i_i64[0] - (_QWORD)v638;
          v654 = (unsigned __int64)((v1011[1].m128i_i64[0] - (__int64)v638) >> 2) >> 1;
          v655 = 0x3FFFFFFFFFFFFFFFLL - v654;
          n0x400_9 = ((v1011[1].m128i_i64[0] - (__int64)v638) >> 2) + v654;
          if ( n0x400_9 <= n0x400_11 )
            n0x400_9 = n0x400_11;
          if ( (v1011[1].m128i_i64[0] - (__int64)v638) >> 2 > v655 )
            n0x400_9 = 0x3FFFFFFFFFFFFFFFLL;
          if ( n0x400_9 >> 62 )
            sub_18002D1E0(v655, 0x3FFFFFFFFFFFFFFFLL, v1006_1);
          if ( n0x400_9 )
          {
            v657 = 4 * n0x400_9;
            if ( n0x400_9 < 0x400 )
            {
              v659 = sub_1800FFF00(v657, 0x3FFFFFFFFFFFFFFFLL, v1006_1);
            }
            else
            {
              if ( n0x400_9 >= 0x3FFFFFFFFFFFFFF7LL )
                sub_18002D1E0(v657, 0x3FFFFFFFFFFFFFFFLL, v1006_1);
              v658 = sub_1800FFF00(v657 + 39, 0x3FFFFFFFFFFFFFFFLL, v1006_1);
              v659 = (v658 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
              *(_QWORD *)(v659 - 8) = v658;
            }
          }
          else
          {
            v659 = 0;
          }
          sub_180200FC0(v659 + v650, v651, 4 * v1025);
          sub_180200FC0(v659, v638, v650);
          if ( v638 )
          {
            if ( n0x1000_3 >= 0x1000 )
            {
              if ( (unsigned __int64)v638 - *(v638 - 1) - 8 >= 0x20 )
                goto LABEL_109;
              v638 = (_QWORD *)*(v638 - 1);
            }
            sub_1800FFFE0(v638);
          }
          v1011[0].m128i_i64[0] = v659;
          v1011[0].m128i_i64[1] = v659 + 4 * n0x400_11;
          v1011[1].m128i_i64[0] = v659 + 4 * n0x400_9;
          v1028_3 = v1028;
          v1025_5 = v1025;
        }
      }
      v1025_6 = *(_QWORD *)&v1028_3 + v1025_5;
      v662 = *((_QWORD *)&v980 + 1);
      v663 = v980;
      *(_QWORD *)&v1025 = v1025_6;
      var150.m128_u64[0] = *((_QWORD *)&v980 + 1);
      if ( (_QWORD)v980 != *((_QWORD *)&v980 + 1) )
        break;
LABEL_874:
      v1020_7 = (__m128i *)v1020;
      v677 = *(int *)(v1020 + 28);
      *(_DWORD *)(v1020 + 32) = ((unsigned __int64)(v1011[0].m128i_i64[1] - v1011[0].m128i_i64[0]) >> 2) - v677;
      v1020_7[1].m128i_i64[0] = v998;
      *v1020_7 = _mm_load_si128(&p_n0x93);
      v646 = v1011[0].m128i_i64[1];
      v638 = (_QWORD *)v1011[0].m128i_i64[0];
      v1021_1 = v1011[0].m128i_i64[0] + 4 * v677;
      if ( v1021_1 != v1011[0].m128i_i64[1] )
      {
        sub_180112710(&n0x93_4, v1021_1, v1011[0].m128i_i64[1]);
        v678 = (_DWORD *)*((_QWORD *)&n0x93_4 + 1);
        v679 = *(_DWORD *)n0x93_4;
        v1020_7[2].m128i_i32[2] = *(_DWORD *)n0x93_4;
        v1020_7[2].m128i_i32[3] = *v678 - v679 + 1;
      }
    }
    while ( 1 )
    {
      v1025_13 = *(_QWORD *)(v663 + 16);
      if ( v1025_13 < *(_QWORD *)&v1028_3 || v1025_13 >= v1025_6 )
        goto LABEL_853;
      v665 = v1011[0].m128i_i64[1];
      if ( v1011[1].m128i_i64[0] - v1011[0].m128i_i64[1] > 8uLL )
      {
        v1011[0].m128i_i64[1] += 12;
        *(_DWORD *)(v665 + 8) = *(_DWORD *)(v663 + 8);
        *(_QWORD *)v665 = *(_QWORD *)v663;
LABEL_853:
        v663 += 24;
        if ( v663 == v662 )
          goto LABEL_874;
      }
      else
      {
        v666 = (_QWORD *)v1011[0].m128i_i64[0];
        v667 = v1011[0].m128i_i64[1] - v1011[0].m128i_i64[0];
        n0x400_12 = ((v1011[0].m128i_i64[1] - v1011[0].m128i_i64[0]) >> 2) + 3;
        n0x1000_4 = v1011[1].m128i_i64[0] - v1011[0].m128i_i64[0];
        v670 = (unsigned __int64)((v1011[1].m128i_i64[0] - v1011[0].m128i_i64[0]) >> 2) >> 1;
        v671 = 0x3FFFFFFFFFFFFFFFLL - v670;
        n0x400_10 = ((v1011[1].m128i_i64[0] - v1011[0].m128i_i64[0]) >> 2) + v670;
        if ( n0x400_10 <= n0x400_12 )
          n0x400_10 = (v667 >> 2) + 3;
        if ( (v1011[1].m128i_i64[0] - v1011[0].m128i_i64[0]) >> 2 > v671 )
          n0x400_10 = 0x3FFFFFFFFFFFFFFFLL;
        if ( n0x400_10 >> 62 )
          sub_18002D1E0(v671, 0x3FFFFFFFFFFFFFFFLL, v1006_1);
        if ( n0x400_10 )
        {
          v673 = 4 * n0x400_10;
          if ( n0x400_10 < 0x400 )
          {
            v675 = sub_1800FFF00(v673, 0x3FFFFFFFFFFFFFFFLL, v1006_1);
          }
          else
          {
            if ( n0x400_10 >= 0x3FFFFFFFFFFFFFF7LL )
              sub_18002D1E0(v673, 0x3FFFFFFFFFFFFFFFLL, v1006_1);
            v674 = sub_1800FFF00(v673 + 39, 0x3FFFFFFFFFFFFFFFLL, v1006_1);
            v675 = (v674 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
            *(_QWORD *)(v675 - 8) = v674;
          }
        }
        else
        {
          v675 = 0;
        }
        *(_DWORD *)(v675 + v667 + 8) = *(_DWORD *)(v663 + 8);
        *(_QWORD *)(v675 + v667) = *(_QWORD *)v663;
        sub_180200FC0(v675, v666, v667);
        if ( v666 )
        {
          if ( n0x1000_4 >= 0x1000 )
          {
            if ( (unsigned __int64)v666 - *(v666 - 1) - 8 >= 0x20 )
              goto LABEL_109;
            v666 = (_QWORD *)*(v666 - 1);
          }
          sub_1800FFFE0(v666);
        }
        v1011[0].m128i_i64[0] = v675;
        v1011[0].m128i_i64[1] = v675 + 4 * n0x400_12;
        v1011[1].m128i_i64[0] = v675 + 4 * n0x400_10;
        v1028_3 = v1028;
        v1025_6 = v1025;
        v662 = var150.m128_u64[0];
        v663 += 24;
        if ( v663 == var150.m128_u64[0] )
          goto LABEL_874;
      }
    }
  }
  LOBYTE(v1025_10) = (unsigned int)sub_180200EC0(v975[0], v1002) == 0;
  v641 = v980;
  LOBYTE(v1021_1) = v1025_10 & ((_QWORD)v980 == *((_QWORD *)&v980 + 1));
  if ( (_BYTE)v1021_1 == 1 )
  {
    v834 = qword_18026F590;
    v1019 = 0;
    v1018 = 0;
    v835 = qword_18026F598(DWORD1(::v1023));
    v1019 = 0;
    v1018 = 0;
    v1011[0].m128i_i32[0] = v834(v835, 0);
    v836 = qword_18026F590;
    v1019 = 0;
    v1018 = 0;
    v837 = qword_18026F598(v1022);
    v1019 = 0;
    v1018 = 0;
    v1011[0].m128i_i32[1] = v836(v837, 0);
    v838 = qword_18026F590;
    v1019 = 0;
    v1018 = 0;
    v839 = qword_18026F598(HIDWORD(::v1023));
    v1019 = 0;
    v1018 = 0;
    v1011[0].m128i_i32[2] = v838(v839, 0);
    v840 = qword_18026F590;
    v1019 = 0;
    v1018 = 0;
    v841 = qword_18026F598(DWORD2(::v1023));
    v1019 = 0;
    v1018 = 0;
    v1011[0].m128i_i32[3] = v840(v841, 0);
    v1011[1].m128i_i8[0] = 0;
    v842 = (__m128i *)*((_QWORD *)&xmmword_18026E790 + 1);
    if ( *((_QWORD *)&xmmword_18026E790 + 1) == qword_18026E7A0 )
    {
      v1019 = 0;
      v1018 = 0;
      sub_1800BEC80(*((_QWORD *)&xmmword_18026E790 + 1), v1011);
    }
    else
    {
      *(_DWORD *)(*((_QWORD *)&xmmword_18026E790 + 1) + 16LL) = v1011[1].m128i_i32[0];
      *v842 = _mm_loadu_si128(v1011);
      *((_QWORD *)&xmmword_18026E790 + 1) += 20LL;
    }
    p_??_7exception@std@@6B@_14 = &std::exception::`vftable';
    v880 = 0;
    v1011[0].m128i_i64[0] = (__int64)"No head components or neck caps selected";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v880);
    p_??_7exception@std@@6B@_14 = &std::runtime_error::`vftable';
    v1019 = 0;
    v1018 = 0;
    sub_180179A30(&p_??_7exception@std@@6B@_14, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  if ( (_QWORD)v980 != *((_QWORD *)&v980 + 1) )
    goto LABEL_819;
LABEL_882:
  n0x1000_5 = v640 >> 2 << v1020_2;
  p_n0x93 = 0;
  v998 = 0;
  if ( v640 )
  {
    if ( v640 >> 2 < 0 )
    {
      v1019 = 0;
      v1018 = 0;
      std::vector<void *>::_Xlen();
    }
    v1019 = 0;
    v1018 = 0;
    if ( n0x1000_5 < 0x1000 )
    {
      v683 = sub_1800FFF00(v640 >> 2 << v1020_2, v1021_1, v1006_1);
    }
    else
    {
      v682 = sub_1800FFF00(n0x1000_5 + 39, v1021_1, v1006_1);
      v683 = (v682 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
      *(_QWORD *)(v683 - 8) = v682;
    }
    p_n0x93.m128i_i64[0] = v683;
    v998 = v683 + n0x1000_5;
    sub_180201670(v683, 0, n0x1000_5);
    p_n0x93.m128i_i64[1] = v683 + n0x1000_5;
  }
  else
  {
    v683 = 0;
  }
  v1007_3 = v1007;
  if ( (_QWORD *)v639 != v638 )
  {
    v685 = 0;
    do
    {
      n0x10000 = *((_DWORD *)v638 + v685);
      if ( v1007_3 )
      {
        *(_DWORD *)(v683 + 4 * v685) = n0x10000;
      }
      else
      {
        if ( n0x10000 >= 0x10000 )
        {
          p_??_7exception@std@@6B@_15 = &std::exception::`vftable';
          v882 = 0;
          v1011[0].m128i_i64[0] = (__int64)"Index overflow";
          v1011[0].m128i_i8[8] = 1;
          sub_18017B450(v1011, &v882);
          p_??_7exception@std@@6B@_15 = &std::runtime_error::`vftable';
          v1030 = 0;
          v1029 = 0;
          sub_180179A30(&p_??_7exception@std@@6B@_15, &_TI2_AVruntime_error_std__);
          goto LABEL_1136;
        }
        *(_WORD *)(v683 + 2 * v685) = n0x10000;
      }
      ++v685;
      v638 = (_QWORD *)v975[0];
    }
    while ( v685 < (__int64)(v975[1] - v975[0]) >> 2 );
  }
  v1030 = 0;
  v1029 = 0;
  v687 = qword_18026F598(v1022);
  v1030 = 0;
  v1029 = 0;
  v688 = qword_18026F328(v1001, "UnityEngine", "Mesh");
  if ( !v688 )
  {
    v1030_1 = 0;
LABEL_1106:
    p_??_7exception@std@@6B@_16 = &std::exception::`vftable';
    v884 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Bind poses do not match bones";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v884);
    p_??_7exception@std@@6B@_16 = &std::runtime_error::`vftable';
    v1030 = v1030_1;
    v1029 = 0;
    sub_180179A30(&p_??_7exception@std@@6B@_16, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  v1030 = 0;
  v1029 = 0;
  v1030_1 = 0;
  v690 = qword_18026F330(v688, "get_bindposes", 0);
  if ( !v690 )
    goto LABEL_1106;
  v1011[0].m128i_i64[0] = 0;
  v1030 = 0;
  v1029 = 0;
  v1030_1 = 0;
  v691 = qword_18026F348(v690, v687, 0, v1011);
  if ( !v691 )
    goto LABEL_1106;
  if ( v1011[0].m128i_i64[0] )
    goto LABEL_1106;
  v1030 = 0;
  v1029 = 0;
  v1030_1 = 0;
  LODWORD(v1025) = qword_18026F590(v691, 0);
  if ( !(_DWORD)v1025 )
    goto LABEL_1106;
  v692 = qword_18026FA60;
  v1030 = v1025;
  v1029 = 0;
  v693 = qword_18026F598((unsigned int)v1025);
  v1030 = v1025;
  v1029 = 0;
  if ( v692(v693) != (_QWORD)::v1025 - v1025_0 )
  {
    v1030_1 = v1025;
    goto LABEL_1106;
  }
  v694 = HIDWORD(::v1023);
  v1030 = v1025;
  v1029 = 0;
  v695 = qword_18026F598(DWORD1(::v1023));
  if ( !v694 )
    goto LABEL_1086;
  if ( !v695 )
    goto LABEL_1086;
  if ( !qword_18026FA58 )
    goto LABEL_1086;
  v1030 = v1025;
  v1029 = 0;
  v696 = qword_18026FA58(v695);
  v1030 = v1025;
  v1029 = 0;
  v697 = qword_18026F598(v694);
  if ( !v696 )
    goto LABEL_1086;
  v698 = v697;
  if ( !v697 )
    goto LABEL_1086;
  v1030 = v1025;
  v1029 = 0;
  v699 = qword_18026FA60(v696);
  v1030 = v1025;
  v1029 = 0;
  if ( v699 != qword_18026FA60(v698)
    || (v1030 = v1025, v1029 = 0, qword_18026FA60(v698), (unsigned int)sub_180200EC0(v696 + 32, v698 + 32)) )
  {
LABEL_1086:
    p_??_7exception@std@@6B@_17 = &std::exception::`vftable';
    v886 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Bone palette changed during capture";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v886);
    p_??_7exception@std@@6B@_17 = &std::runtime_error::`vftable';
    v1030 = v1025;
    v1029 = 0;
    sub_180179A30(&p_??_7exception@std@@6B@_17, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  v1030 = v1025;
  v1029 = 0;
  n0x93_16 = *(_QWORD *)(qword_18026F598(v1022) + 16);
  n0x93_4 = n0x93_16;
  LOBYTE(v991) = 0;
  if ( !n0x93_16
    || (v701 = *((_QWORD *)NtCurrentTeb()->ThreadLocalStoragePointer + (unsigned int)TlsIndex), *(_QWORD *)(v701 + 1024)) )
  {
    v1011[0].m128i_i64[0] = (__int64)&std::exception::`vftable';
    *(__m128i *)((char *)v1011 + 8) = 0;
    v1012_.m128i_i64[0] = (__int64)"Invalid or nested mesh clone";
    v1012_.m128i_i8[8] = 1;
    sub_18017B450(&v1012_, &v1011[0].m128i_u64[1]);
    v1011[0].m128i_i64[0] = (__int64)&std::runtime_error::`vftable';
    v1030 = v1025;
    v1029 = 0;
    sub_180179A30(v1011, &_TI2_AVruntime_error_std__);
    goto LABEL_1108;
  }
  *(_QWORD *)&v1028 = v701 + 1024;
  *(_QWORD *)(v701 + 1024) = &n0x93_4;
  v702 = qword_18026F598(v1022);
  v703 = ((__int64 (__fastcall *)(__int64))v933.m128i_i64[1])(v702);
  v704 = *((_QWORD *)&n0x93_4 + 1);
  v705 = v991;
  **(_QWORD **)&v1028 = 0;
  if ( !v703 )
  {
LABEL_1108:
    v1029_1 = 0;
    goto LABEL_1110;
  }
  v1030 = v1025;
  v1029 = 0;
  v1029_1 = 0;
  LODWORD(v1028) = qword_18026F590(v703, 0);
  if ( !LODWORD(v1028) )
  {
LABEL_1110:
    p_??_7exception@std@@6B@_18 = &std::exception::`vftable';
    v890 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Cannot clone mesh independently";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v890);
    p_??_7exception@std@@6B@_18 = &std::runtime_error::`vftable';
    v1030 = v1025;
    v1029 = v1029_1;
    sub_180179A30(&p_??_7exception@std@@6B@_18, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  v1025_7 = v1025;
  if ( v705
    || !v704
    || (v1030 = v1025,
        v1029 = LODWORD(v1028),
        v708 = qword_18026F598(LODWORD(v1028)),
        v1025_7 = v1025,
        v704 != *(_QWORD *)(v708 + 16)) )
  {
    p_??_7exception@std@@6B@_19 = &std::exception::`vftable';
    v888 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Deferred initialization did not belong to the returned clone";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v888);
    p_??_7exception@std@@6B@_19 = &std::runtime_error::`vftable';
    v1030 = v1025_7;
    v1029 = LODWORD(v1028);
    sub_180179A30(&p_??_7exception@std@@6B@_19, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v709 = qword_18026F598(LODWORD(v1028));
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  if ( v709 == qword_18026F598(v1022) )
  {
    v1029_1 = LODWORD(v1028);
    goto LABEL_1110;
  }
  v710 = lpMem[0];
  v711 = lpMem[1];
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v712 = qword_18026F598(LODWORD(v1028));
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  ((void (__fastcall *)(__int64, unsigned __int64, _BYTE *, _QWORD))v934.m128i_i64[0])(
    v712,
    n0x93_1,
    v710,
    (unsigned int)((unsigned __int64)(v711 - v710) >> 4));
  for ( nn = 0; nn < 4; ++nn )
  {
    v714 = *((_DWORD *)&v979 + nn);
    if ( v714 )
    {
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v715 = qword_18026F598(LODWORD(v1028));
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      if ( (unsigned int)v957(v715, nn) != v714 )
      {
        p_??_7exception@std@@6B@_20 = &std::exception::`vftable';
        v892 = 0;
        v1011[0].m128i_i64[0] = (__int64)"Clone stream layout changed";
        v1011[0].m128i_i8[8] = 1;
        sub_18017B450(v1011, &v892);
        p_??_7exception@std@@6B@_20 = &std::runtime_error::`vftable';
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        sub_180179A30(&p_??_7exception@std@@6B@_20, &_TI2_AVruntime_error_std__);
        goto LABEL_1136;
      }
      v716 = *((_QWORD *)v982 + 3 * nn);
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v717 = qword_18026F598(LODWORD(v1028));
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      ((void (__fastcall *)(__int64, _QWORD, __int64, _QWORD, _DWORD, _DWORD, int, int))v933.m128i_i64[0])(
        v717,
        nn,
        v716,
        0,
        0,
        n0x93_1,
        v714,
        2);
    }
  }
  v718 = v975[0];
  v719 = v975[1];
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v720 = qword_18026F598(LODWORD(v1028));
  v721 = (unsigned __int64)(v719 - v718) >> 2;
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  ((void (__fastcall *)(__int64, _QWORD, _QWORD))v947.m128i_i64[0])(v720, (unsigned int)(v721 + 3), v1007);
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v722 = qword_18026F598(LODWORD(v1028));
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  ((void (__fastcall *)(__int64, _QWORD, _QWORD))v947.m128i_i64[0])(v722, (unsigned int)v721, v1007);
  v723 = p_n0x93.m128i_i64[0];
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v724 = qword_18026F598(LODWORD(v1028));
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  ((void (__fastcall *)(__int64, __int64, _QWORD, _QWORD, _DWORD, int, int))v934.m128i_i64[1])(
    v724,
    v723,
    0,
    0,
    v721,
    v976,
    2);
  v1012__1.m128i_i64[0] = 0;
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v725 = qword_18026F328(v1001, "UnityEngine", "Mesh");
  if ( !v725 || (v1030 = v1025, v1029 = LODWORD(v1028), (v726 = qword_18026F330(v725, "set_subMeshCount", 1)) == 0) )
  {
    p_??_7exception@std@@6B@_21 = &std::exception::`vftable';
    v894 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Submesh setter absent";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v894);
    p_??_7exception@std@@6B@_21 = &std::runtime_error::`vftable';
    v1030 = v1025;
    v1029 = LODWORD(v1028);
    sub_180179A30(&p_??_7exception@std@@6B@_21, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  LODWORD(v960[0]) = n0x56;
  v940[0] = v960;
  v727 = (void (__fastcall *)(__int64, __int64, _QWORD *, __m128i *))qword_18026F348;
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v728 = qword_18026F598(LODWORD(v1028));
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v727(v726, v728, v940, &v1012__1);
  if ( v1012__1.m128i_i64[0] )
  {
    p_??_7exception@std@@6B@_22 = &std::exception::`vftable';
    v896 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Cannot set submesh count";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v896);
    p_??_7exception@std@@6B@_22 = &std::runtime_error::`vftable';
    v1030 = v1025;
    v1029 = LODWORD(v1028);
    sub_180179A30(&p_??_7exception@std@@6B@_22, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  n0x56_3 = 0;
  v1020_8 = v1020_6;
  while ( (int)n0x56_3 < (int)n0x56 )
  {
    v1030 = v1025;
    v1029 = LODWORD(v1028);
    v731 = qword_18026F598(LODWORD(v1028));
    v1030 = v1025;
    v1029 = LODWORD(v1028);
    ((void (__fastcall *)(__int64, _QWORD, __int64, __int64))v932.m128i_i64[0])(
      v731,
      n0x56_3,
      v1020_8 + 48LL * n0x56_3,
      10);
    ++n0x56_3;
  }
  n0x93_4 = 0;
  v991 = 0;
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v732 = qword_18026F598(v1022);
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  ((void (__fastcall *)(__int64, __int128 *))v947.m128i_i64[1])(v732, &n0x93_4);
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v733 = qword_18026F598(LODWORD(v1028));
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  ((void (__fastcall *)(__int64, __int128 *))v935.m128i_i64[0])(v733, &n0x93_4);
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v734 = qword_18026F598(LODWORD(v1028));
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v735 = qword_18026F328(v1001, "UnityEngine", "Mesh");
  if ( !v735 )
    goto LABEL_1085;
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v736 = qword_18026F330(v735, "get_bindposes", 0);
  if ( !v736 )
    goto LABEL_1085;
  v1011[0].m128i_i64[0] = 0;
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v737 = qword_18026F348(v736, v734, 0, v1011);
  if ( !v737
    || v1011[0].m128i_i64[0]
    || (v1030 = v1025, v1029 = LODWORD(v1028), qword_18026FA60(v737) != (_QWORD)::v1025 - v1025_0)
    || (v1030 = v1025,
        v1029 = LODWORD(v1028),
        v738 = qword_18026F598((unsigned int)v1025),
        (unsigned int)sub_180200EC0(v737 + 32, v738 + 32)) )
  {
LABEL_1085:
    p_??_7exception@std@@6B@_23 = &std::exception::`vftable';
    v898 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Clone bind poses changed";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v898);
    p_??_7exception@std@@6B@_23 = &std::runtime_error::`vftable';
    v1030 = v1025;
    v1029 = LODWORD(v1028);
    sub_180179A30(&p_??_7exception@std@@6B@_23, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v739 = qword_18026F598(LODWORD(v1028));
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v740 = qword_18026F328(v1001, "UnityEngine", "Mesh");
  if ( !v740 )
    goto LABEL_1087;
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v741 = qword_18026F330(v740, "get_blendShapeCount", 0);
  if ( !v741
    || (v1011[0].m128i_i64[0] = 0,
        v1030 = v1025,
        v1029 = LODWORD(v1028),
        (v742 = qword_18026F348(v741, v739, 0, v1011)) == 0)
    || v1011[0].m128i_i64[0]
    || (v1030 = v1025, v1029 = LODWORD(v1028), v743 = (_DWORD *)qword_18026F350(v742), *v743 != v945) )
  {
LABEL_1087:
    p_??_7exception@std@@6B@_24 = &std::exception::`vftable';
    v900 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Clone blend shapes changed";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v900);
    p_??_7exception@std@@6B@_24 = &std::runtime_error::`vftable';
    v1030 = v1025;
    v1029 = LODWORD(v1028);
    sub_180179A30(&p_??_7exception@std@@6B@_24, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  qword_18026F648(v704);
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v744 = qword_18026F328(v1001, "UnityEngine", "Mesh");
  if ( !v744 || (v1030 = v1025, v1029 = LODWORD(v1028), (v745 = qword_18026F330(v744, "GetVertexBuffer", 1)) == 0) )
  {
    p_??_7exception@std@@6B@_25 = &std::exception::`vftable';
    v902 = 0;
    v1011[0].m128i_i64[0] = (__int64)"Vertex buffer accessor absent";
    v1011[0].m128i_i8[8] = 1;
    sub_18017B450(v1011, &v902);
    p_??_7exception@std@@6B@_25 = &std::runtime_error::`vftable';
    v1030 = v1025;
    v1029 = LODWORD(v1028);
    sub_180179A30(&p_??_7exception@std@@6B@_25, &_TI2_AVruntime_error_std__);
    goto LABEL_1136;
  }
  for ( i1 = 0; ; ++i1 )
  {
    LODWORD(v962[0]) = i1;
    if ( i1 >= 4 )
      break;
    v747 = *((int *)&v979 + i1);
    if ( *((_DWORD *)&v979 + i1) )
    {
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v748 = qword_18026F598(LODWORD(v1028));
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      if ( (unsigned int)v957(v748, (unsigned int)i1) != (_DWORD)v747 )
      {
        p_??_7exception@std@@6B@_26 = &std::exception::`vftable';
        v904 = 0;
        v1011[0].m128i_i64[0] = (__int64)"Uploaded layout changed";
        v1011[0].m128i_i8[8] = 1;
        sub_18017B450(v1011, &v904);
        p_??_7exception@std@@6B@_26 = &std::runtime_error::`vftable';
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        sub_180179A30(&p_??_7exception@std@@6B@_26, &_TI2_AVruntime_error_std__);
        goto LABEL_1136;
      }
      *(_QWORD *)&v943 = v962;
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v749 = qword_18026F598(LODWORD(v1028));
      v1011[0].m128i_i64[0] = 0;
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v750 = qword_18026F348(v745, v749, &v943, v1011);
      if ( !v750
        || v1011[0].m128i_i64[0]
        || (v1030 = v1025, v1029 = LODWORD(v1028), (var150.m128_i32[0] = qword_18026F590(v750, 0)) == 0) )
      {
        p_??_7exception@std@@6B@_27 = &std::exception::`vftable';
        v906 = 0;
        v1011[0].m128i_i64[0] = (__int64)"Missing uploaded vertex buffer";
        v1011[0].m128i_i8[8] = 1;
        sub_18017B450(v1011, &v906);
        p_??_7exception@std@@6B@_27 = &std::runtime_error::`vftable';
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        sub_180179A30(&p_??_7exception@std@@6B@_27, &_TI2_AVruntime_error_std__);
        goto LABEL_1136;
      }
      v751 = n0x93_1 * v747;
      v752 = qword_18026F590;
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v753 = qword_18026FB20(qword_18026FB28, v751);
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      LODWORD(v1020) = v752(v753, 0);
      if ( !(_DWORD)v1020 )
      {
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        v833 = qword_18026F598(var150.m128_u32[0]);
        if ( qword_18026FB40 )
        {
          v1011[0].m128i_i64[0] = 0;
          v1030 = v1025;
          v1029 = LODWORD(v1028);
          qword_18026F348(qword_18026FB40, v833, 0, v1011);
        }
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        qword_18026EC40(var150.m128_u32[0]);
        p_??_7exception@std@@6B@_28 = &std::exception::`vftable';
        v908 = 0;
        v1011[0].m128i_i64[0] = (__int64)"Vertex readback allocation failed";
        v1011[0].m128i_i8[8] = 1;
        sub_18017B450(v1011, &v908);
        p_??_7exception@std@@6B@_28 = &std::runtime_error::`vftable';
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        sub_180179A30(&p_??_7exception@std@@6B@_28, &_TI2_AVruntime_error_std__);
        goto LABEL_1136;
      }
      memset(v1015, 0, 28);
      v1013 = 0;
      var190 = 0;
      memset(v1011, 0, sizeof(v1011));
      v754 = qword_18026F598(var150.m128_u32[0]);
      v755 = *(const __m128i **)(v754 + qword_18026FB38);
      v756 = qword_18026FB10(v754);
      if ( !v755 || v755[1].m128i_i32[3] != v756 || v755->m128i_i64[1] * v755[1].m128i_i64[0] < v751 )
      {
        p_??_7exception@std@@6B@_29 = &std::exception::`vftable';
        v910 = 0;
        v1012_.m128i_i64[0] = (__int64)"Uploaded buffer descriptor mismatch";
        v1012_.m128i_i8[8] = 1;
        sub_18017B450(&v1012_, &v910);
        p_??_7exception@std@@6B@_29 = &std::runtime_error::`vftable';
        sub_180179A30(&p_??_7exception@std@@6B@_29, &_TI2_AVruntime_error_std__);
        goto LABEL_1136;
      }
      v757 = _mm_loadu_si128(v755);
      v758 = _mm_loadu_si128(v755 + 2);
      v1013_1 = _mm_loadu_si128(v755 + 3);
      v1011[1] = v755[1];
      *(_QWORD *)&v1015[1] = v755[5].m128i_i64[0];
      v1015[0] = _mm_loadu_si128(v755 + 4);
      v1013 = v1013_1;
      var190 = v758;
      v1011[0] = v757;
      v1011[1].m128i_i32[3] = v756 & 0xFFFFFFFD;
      v760 = qword_18026FB08(qword_18026FB30);
      if ( !v760 )
      {
        DWORD2(v1015[1]) = 0;
LABEL_1076:
        p_??_7exception@std@@6B@_30 = &std::exception::`vftable';
        v912 = 0;
        v1012_.m128i_i64[0] = (__int64)"Cannot root vertex read alias";
        v1012_.m128i_i8[8] = 1;
        sub_18017B450(&v1012_, &v912);
        p_??_7exception@std@@6B@_30 = &std::runtime_error::`vftable';
        sub_180179A30(&p_??_7exception@std@@6B@_30, &_TI2_AVruntime_error_std__);
        goto LABEL_1136;
      }
      v761 = qword_18026F590(v760, 0);
      DWORD2(v1015[1]) = v761;
      if ( !v761 )
        goto LABEL_1076;
      v762 = qword_18026FB38;
      *(_QWORD *)(qword_18026F598(v761) + v762) = v1011;
      v763 = (void (__fastcall *)(__int64, __int64, _QWORD, _QWORD, _DWORD, int))qword_18026FB18;
      v764 = qword_18026F598((unsigned int)v1020);
      v765 = qword_18026F598(DWORD2(v1015[1]));
      v763(v765, v764, 0, 0, v751, 1);
      v766 = *((_QWORD *)v982 + 3 * SLODWORD(v962[0]));
      v767 = qword_18026F598((unsigned int)v1020);
      v768 = sub_180200EC0(v767 + 32, v766);
      if ( DWORD2(v1015[1]) )
      {
        v769 = qword_18026FB38;
        *(_QWORD *)(qword_18026F598(DWORD2(v1015[1])) + v769) = 0;
        qword_18026EC40(DWORD2(v1015[1]));
      }
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      qword_18026EC40((unsigned int)v1020);
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v770 = qword_18026F598(var150.m128_u32[0]);
      if ( qword_18026FB40 )
      {
        v1011[0].m128i_i64[0] = 0;
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        qword_18026F348(qword_18026FB40, v770, 0, v1011);
      }
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      qword_18026EC40(var150.m128_u32[0]);
      if ( v768 )
      {
        p_??_7exception@std@@6B@_31 = &std::exception::`vftable';
        v914 = 0;
        v1011[0].m128i_i64[0] = (__int64)"Uploaded vertex content mismatch";
        v1011[0].m128i_i8[8] = 1;
        sub_18017B450(v1011, &v914);
        p_??_7exception@std@@6B@_31 = &std::runtime_error::`vftable';
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        sub_180179A30(&p_??_7exception@std@@6B@_31, &_TI2_AVruntime_error_std__);
        goto LABEL_1136;
      }
      i1 = (int)v962[0];
    }
  }
  v1012_.m128i_i16[0] = 256;
  v771 = p_n0x93.m128i_i64[1];
  v1010.m128i_i64[0] = p_n0x93.m128i_i64[0];
  v772 = n0x93.m128i_i64[1];
  v1010.m128i_i64[1] = n0x93.m128i_i64[0];
  n2_5 = 0;
  while ( 2 )
  {
    if ( n2_5 == 2 )
    {
      v790 = (__int64 (__fastcall *)(__int64))qword_18026FA38;
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v791 = qword_18026F598(DWORD1(::v1023));
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v792 = v790(v791);
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      if ( v792 != qword_18026F598(v1022) )
      {
        p_??_7exception@std@@6B@_32 = &std::exception::`vftable';
        v922 = 0;
        v1011[0].m128i_i64[0] = (__int64)"Renderer source changed during capture";
        v1011[0].m128i_i8[8] = 1;
        sub_18017B450(v1011, &v922);
        p_??_7exception@std@@6B@_32 = &std::runtime_error::`vftable';
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        sub_180179A30(&p_??_7exception@std@@6B@_32, &_TI2_AVruntime_error_std__);
        goto LABEL_1136;
      }
      v793 = (__int64 (__fastcall *)(__int64))qword_18026FA40;
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v794 = qword_18026F598(DWORD1(::v1023));
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v795 = v793(v794);
      if ( v795 )
      {
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        var150.m128_i32[0] = qword_18026F590(v795, 0);
        v1025_8 = v1025;
        if ( !var150.m128_i32[0] )
        {
          p_??_7exception@std@@6B@_33 = &std::exception::`vftable';
          v924 = 0;
          v1011[0].m128i_i64[0] = (__int64)"Cannot retain original shadow mesh";
          v1011[0].m128i_i8[8] = 1;
          sub_18017B450(v1011, &v924);
          p_??_7exception@std@@6B@_33 = &std::runtime_error::`vftable';
          v1030 = v1025_8;
          v1029 = LODWORD(v1028);
          sub_180179A30(&p_??_7exception@std@@6B@_33, &_TI2_AVruntime_error_std__);
          goto LABEL_1136;
        }
      }
      else
      {
        var150.m128_i32[0] = 0;
        v1025_8 = v1025;
      }
      v797 = HIDWORD(::v1023);
      v1030 = v1025_8;
      v1029 = LODWORD(v1028);
      v798 = qword_18026F598(DWORD1(::v1023));
      if ( !v797 )
        goto LABEL_1088;
      if ( !v798 )
        goto LABEL_1088;
      if ( !qword_18026FA58 )
        goto LABEL_1088;
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v799 = qword_18026FA58(v798);
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v800 = qword_18026F598(v797);
      if ( !v799 )
        goto LABEL_1088;
      v801 = v800;
      if ( !v800 )
        goto LABEL_1088;
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v802 = qword_18026FA60(v799);
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      if ( v802 != qword_18026FA60(v801)
        || (v1030 = v1025,
            v1029 = LODWORD(v1028),
            qword_18026FA60(v801),
            (unsigned int)sub_180200EC0(v799 + 32, v801 + 32)) )
      {
LABEL_1088:
        p_??_7exception@std@@6B@_34 = &std::exception::`vftable';
        v926 = 0;
        v1011[0].m128i_i64[0] = (__int64)"Bone palette changed before binding";
        v1011[0].m128i_i8[8] = 1;
        sub_18017B450(v1011, &v926);
        p_??_7exception@std@@6B@_34 = &std::runtime_error::`vftable';
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        sub_180179A30(&p_??_7exception@std@@6B@_34, &_TI2_AVruntime_error_std__);
        goto LABEL_1136;
      }
      v804 = DWORD1(::v1023);
      v805 = DWORD2(::v1023);
      v806 = HIDWORD(::v1023);
      v807 = *((_QWORD *)&xmmword_18026E778 + 1);
      if ( *((_QWORD *)&xmmword_18026E778 + 1) == qword_18026E788 )
      {
        v810 = *((_QWORD *)&xmmword_18026E778 + 1) - xmmword_18026E778;
        v811 = (__int64)(*((_QWORD *)&xmmword_18026E778 + 1) - xmmword_18026E778) >> 5;
        v812 = v811 + 1;
        v813 = 0x7FFFFFFFFFFFFFFLL - (v811 >> 1);
        n0x80_1 = v811 + (v811 >> 1);
        if ( n0x80_1 <= v811 + 1 )
          n0x80_1 = v811 + 1;
        if ( v811 > v813 )
          n0x80_1 = 0x7FFFFFFFFFFFFFFLL;
        if ( n0x80_1 >> 59 )
        {
          v1030 = v1025;
          v1029 = LODWORD(v1028);
          sub_18002D1E0(v803, n0x80_1 >> 59, v813);
        }
        v815 = 32 * n0x80_1;
        v1020 = 32 * n0x80_1;
        if ( n0x80_1 )
        {
          if ( n0x80_1 < 0x80 )
          {
            v1030 = v1025;
            v1029 = LODWORD(v1028);
            v817 = sub_1800FFF00(v815, 0, v813);
          }
          else
          {
            if ( n0x80_1 == 0x7FFFFFFFFFFFFFFLL )
            {
              v1030 = v1025;
              v1029 = LODWORD(v1028);
              sub_18002D1E0(v815, 0, v813);
            }
            v1030 = v1025;
            v1029 = LODWORD(v1028);
            v816 = sub_1800FFF00(v815 + 39, 0, v813);
            v817 = (v816 + 39) & 0xFFFFFFFFFFFFFFE0uLL;
            *(_QWORD *)(v817 - 8) = v816;
          }
        }
        else
        {
          v817 = 0;
        }
        *(_DWORD *)(v817 + v810) = v804;
        *(_DWORD *)(v817 + v810 + 4) = v1022;
        *(_DWORD *)(v817 + v810 + 8) = LODWORD(v1028);
        *(_DWORD *)(v817 + v810 + 12) = var150.m128_i32[0];
        *(_DWORD *)(v817 + v810 + 16) = v805;
        *(_WORD *)(v817 + v810 + 20) = 0;
        *(_DWORD *)(v817 + v810 + 24) = v806;
        *(_WORD *)(v817 + v810 + 28) = 0;
        if ( v807 == *((_QWORD *)&xmmword_18026E778 + 1) )
        {
          v820 = xmmword_18026E778;
          v819 = v817;
          v818 = v807 - xmmword_18026E778;
        }
        else
        {
          sub_180200FC0(v817, xmmword_18026E778, v807 - xmmword_18026E778);
          v818 = *((_QWORD *)&xmmword_18026E778 + 1) - v807;
          v819 = v817 + v810 + 32;
          v820 = v807;
        }
        sub_180200FC0(v819, v820, v818);
        v821 = (void *)xmmword_18026E778;
        v809 = v989;
        if ( (_QWORD)xmmword_18026E778 )
        {
          if ( (unsigned __int64)(qword_18026E788 - xmmword_18026E778) >= 0x1000 )
          {
            if ( (unsigned __int64)(xmmword_18026E778 - 8 - *(_QWORD *)(xmmword_18026E778 - 8)) >= 0x20 )
              goto LABEL_109;
            v821 = *(void **)(xmmword_18026E778 - 8);
          }
          sub_1800FFFE0(v821);
          v809 = v989;
        }
        *(_QWORD *)&xmmword_18026E778 = v817;
        v808 = v817 + 32 * v812;
        *((_QWORD *)&xmmword_18026E778 + 1) = v808;
        qword_18026E788 = v817 + v1020;
      }
      else
      {
        **((_DWORD **)&xmmword_18026E778 + 1) = DWORD1(::v1023);
        *(_DWORD *)(v807 + 4) = v1022;
        *(_DWORD *)(v807 + 8) = LODWORD(v1028);
        *(_DWORD *)(v807 + 12) = var150.m128_i32[0];
        *(_DWORD *)(v807 + 16) = v805;
        *(_WORD *)(v807 + 20) = 0;
        *(_DWORD *)(v807 + 24) = v806;
        *(_WORD *)(v807 + 28) = 0;
        *((_QWORD *)&xmmword_18026E778 + 1) = v807 + 32;
        v808 = v807 + 32;
        v809 = v989;
      }
      v822 = (char *)v964[0];
      *(_BYTE *)(v808 - 3) = v809 < (((char *)v964[1] - (char *)v964[0]) >> 2) / 3uLL;
      *((_QWORD *)&::v1023 + 1) = 0;
      DWORD1(::v1023) = 0;
      v823 = (void *)p_n0x93.m128i_i64[0];
      v1025_9 = v1025;
      if ( p_n0x93.m128i_i64[0] )
      {
        if ( v998 - p_n0x93.m128i_i64[0] >= 0x1000 )
        {
          if ( (unsigned __int64)(p_n0x93.m128i_i64[0] - 8 - *(_QWORD *)(p_n0x93.m128i_i64[0] - 8)) >= 0x20 )
            goto LABEL_109;
          v823 = *(void **)(p_n0x93.m128i_i64[0] - 8);
        }
        sub_1800FFFE0(v823);
      }
      v825 = (void *)v980;
      if ( (_QWORD)v980 )
      {
        if ( (unsigned __int64)(v981 - v980) >= 0x1000 )
        {
          if ( (unsigned __int64)(v980 - 8 - *(_QWORD *)(v980 - 8)) >= 0x20 )
            goto LABEL_109;
          v825 = *(void **)(v980 - 8);
        }
        sub_1800FFFE0(v825);
      }
      v1020_9 = (void *)v1020_6;
      if ( (_QWORD)v1020_6 )
      {
        if ( v939 - (unsigned __int64)v1020_6 >= 0x1000 )
        {
          if ( (unsigned __int64)(v1020_6 - 8 - *(_QWORD *)(v1020_6 - 8)) >= 0x20 )
            goto LABEL_109;
          v1020_9 = *(void **)(v1020_6 - 8);
        }
        sub_1800FFFE0(v1020_9);
      }
      v827 = (void *)v975[0];
      if ( v975[0] )
      {
        if ( v975[2] - v975[0] >= 0x1000u )
        {
          if ( (unsigned __int64)(v975[0] - 8LL - *(_QWORD *)(v975[0] - 8LL)) >= 0x20 )
            goto LABEL_109;
          v827 = *(void **)(v975[0] - 8LL);
        }
        sub_1800FFFE0(v827);
      }
      if ( v822 )
      {
        if ( (unsigned __int64)(v965 - v822) >= 0x1000 )
        {
          if ( (unsigned __int64)&v822[-*((_QWORD *)v822 - 1) - 8] >= 0x20 )
            goto LABEL_109;
          v822 = (char *)*((_QWORD *)v822 - 1);
        }
        sub_1800FFFE0(v822);
      }
      p_m128i_i64_6 = (void *)p_m128i_i64_5;
      if ( (_QWORD)p_m128i_i64_5 )
      {
        if ( (unsigned __int64)(v951 - p_m128i_i64_5) >= 0x1000 )
        {
          if ( (unsigned __int64)(p_m128i_i64_5 - 8 - *(_QWORD *)(p_m128i_i64_5 - 8)) >= 0x20 )
            goto LABEL_109;
          p_m128i_i64_6 = *(void **)(p_m128i_i64_5 - 8);
        }
        sub_1800FFFE0(p_m128i_i64_6);
      }
      v829 = (void *)n0x93.m128i_i64[0];
      if ( n0x93.m128i_i64[0] )
      {
        if ( (unsigned __int64)(v942 - n0x93.m128i_i64[0]) >= 0x1000 )
        {
          if ( (unsigned __int64)(n0x93.m128i_i64[0] - 8 - *(_QWORD *)(n0x93.m128i_i64[0] - 8)) >= 0x20 )
            goto LABEL_109;
          v829 = *(void **)(n0x93.m128i_i64[0] - 8);
        }
        sub_1800FFFE0(v829);
      }
      sub_1800BEE40(v982);
      v830 = lpMem[0];
      if ( !lpMem[0] )
        goto LABEL_1051;
      if ( (unsigned __int64)(v959 - (char *)lpMem[0]) < 0x1000 )
      {
LABEL_1050:
        sub_1800FFFE0(v830);
LABEL_1051:
        v1023_1 = 0;
        if ( !v1025_9 )
          goto LABEL_1053;
        goto LABEL_1052;
      }
      if ( (unsigned __int64)lpMem[0] - *((_QWORD *)lpMem[0] - 1) - 8 < 0x20 )
      {
        v830 = (void *)*((_QWORD *)lpMem[0] - 1);
        goto LABEL_1050;
      }
LABEL_109:
      BUG();
    }
    v774 = v1012_.m128i_i8[n2_5];
    v1022_1 = LODWORD(v1028);
    if ( v774 )
      v1022_1 = v1022;
    v1030 = v1025;
    v1029 = LODWORD(v1028);
    v776 = qword_18026F598(v1022_1);
    v1030 = v1025;
    v1029 = LODWORD(v1028);
    v777 = ((__int64 (__fastcall *)(__int64))v932.m128i_i64[1])(v776);
    if ( !v777 || (v1030 = v1025, v1029 = LODWORD(v1028), (var150.m128_i32[0] = qword_18026F590(v777, 0)) == 0) )
    {
      p_??_7exception@std@@6B@_35 = &std::exception::`vftable';
      v916 = 0;
      v1011[0].m128i_i64[0] = (__int64)"No uploaded index buffer";
      v1011[0].m128i_i8[8] = 1;
      sub_18017B450(v1011, &v916);
      p_??_7exception@std@@6B@_35 = &std::runtime_error::`vftable';
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      sub_180179A30(&p_??_7exception@std@@6B@_35, &_TI2_AVruntime_error_std__);
      goto LABEL_1136;
    }
    v778 = qword_18026F590;
    v779 = v774 == 0;
    v780 = v771;
    if ( v774 )
      v780 = v772;
    v781 = v1010.m128i_i64[0];
    if ( !v779 )
      v781 = v1010.m128i_i64[1];
    v782 = v780 - v781;
    v1030 = v1025;
    v1029 = LODWORD(v1028);
    v783 = qword_18026FB20(qword_18026FB28, v782);
    v1030 = v1025;
    v1029 = LODWORD(v1028);
    LODWORD(v1020) = v778(v783, 0);
    if ( (_DWORD)v1020 )
    {
      v784 = (void (__fastcall *)(__int64, __int64, _QWORD, _QWORD, _DWORD, int))qword_18026FB18;
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v785 = qword_18026F598((unsigned int)v1020);
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v786 = qword_18026F598(var150.m128_u32[0]);
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v784(v786, v785, 0, 0, v782, 1);
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v787 = qword_18026F598((unsigned int)v1020);
      v788 = sub_180200EC0(v787 + 32, v781);
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      qword_18026EC40((unsigned int)v1020);
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      v789 = qword_18026F598(var150.m128_u32[0]);
      if ( qword_18026FB40 )
      {
        v1011[0].m128i_i64[0] = 0;
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        qword_18026F348(qword_18026FB40, v789, 0, v1011);
      }
      v1030 = v1025;
      v1029 = LODWORD(v1028);
      qword_18026EC40(var150.m128_u32[0]);
      ++n2_5;
      if ( v788 )
      {
        p_??_7exception@std@@6B@_36 = &std::exception::`vftable';
        v920 = 0;
        v1011[0].m128i_i64[0] = (__int64)"Index ownership/content check failed";
        v1011[0].m128i_i8[8] = 1;
        sub_18017B450(v1011, &v920);
        p_??_7exception@std@@6B@_36 = &std::runtime_error::`vftable';
        v1030 = v1025;
        v1029 = LODWORD(v1028);
        sub_180179A30(&p_??_7exception@std@@6B@_36, &_TI2_AVruntime_error_std__);
        goto LABEL_1136;
      }
      continue;
    }
    break;
  }
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  v832 = qword_18026F598(var150.m128_u32[0]);
  if ( qword_18026FB40 )
  {
    v1011[0].m128i_i64[0] = 0;
    v1030 = v1025;
    v1029 = LODWORD(v1028);
    qword_18026F348(qword_18026FB40, v832, 0, v1011);
  }
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  qword_18026EC40(var150.m128_u32[0]);
  p_??_7exception@std@@6B@_37 = &std::exception::`vftable';
  v918 = 0;
  v1011[0].m128i_i64[0] = (__int64)"Readback allocation failed";
  v1011[0].m128i_i8[8] = 1;
  sub_18017B450(v1011, &v918);
  p_??_7exception@std@@6B@_37 = &std::runtime_error::`vftable';
  v1030 = v1025;
  v1029 = LODWORD(v1028);
  sub_180179A30(&p_??_7exception@std@@6B@_37, &_TI2_AVruntime_error_std__);
LABEL_1136:
  if ( v1027 )
  {
    v843 = (void (__fastcall *)(__int64))v935.m128i_i64[1];
    if ( v935.m128i_i64[1] )
    {
      v844 = qword_18026F598(v1027);
      v843(v844);
    }
    qword_18026EC40(v1027);
  }
  v1025_9 = v1026;
  v1023_1 = v1022;
  if ( v1026 )
  {
LABEL_1052:
    v1023_2 = v1023_1;
    qword_18026EC40(v1025_9);
    v1023_1 = v1023_2;
  }
LABEL_1053:
  if ( (_DWORD)v1023_1 )
    v1023_1 = qword_18026EC40((unsigned int)v1023_1);
  if ( DWORD1(::v1023) )
  {
    v1023_1 = qword_18026EC40(DWORD1(::v1023));
    DWORD1(::v1023) = 0;
  }
  if ( DWORD2(::v1023) )
  {
    v1023_1 = qword_18026EC40(DWORD2(::v1023));
    DWORD2(::v1023) = 0;
  }
  if ( HIDWORD(::v1023) )
  {
    v1023_1 = qword_18026EC40(HIDWORD(::v1023));
    HIDWORD(::v1023) = 0;
  }
  return v1023_1;
}
