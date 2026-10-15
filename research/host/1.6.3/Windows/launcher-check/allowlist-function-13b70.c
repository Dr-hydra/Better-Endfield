// Hidden C++ exception states: #wind=3
void __fastcall sub_180013B70(__int64 a1, __int64 a2, __int64 a3, __int64 a4)
{
  __int64 v4; // r15
  __int64 v5; // rsi
  HMODULE *v6; // r14
  _BYTE *v7; // r11
  unsigned __int64 n31; // r10
  unsigned __int64 n31_2; // r9
  __int64 v10; // rbx
  char v11; // r8
  void **v12; // rax
  int v13; // eax
  char *v14; // rcx
  unsigned int Size_3; // edi
  size_t Size_1; // rbx
  char *v17; // r13
  char *v18; // rsi
  void *v19; // rax
  char *v20; // r12
  __int64 v21; // rax
  size_t Size_2; // rcx
  __int64 v23; // rbx
  unsigned int v24; // r15d
  void **v25; // r13
  __int64 v26; // rdi
  char *v27; // rax
  void **v28; // r8
  signed __int64 v29; // r8
  int v30; // ecx
  int v31; // edx
  unsigned int v32; // ebx
  char *v33; // rax
  char *v34; // rax
  int Size_5; // [rsp+60h] [rbp-A0h]
  unsigned int Size; // [rsp+64h] [rbp-9Ch] BYREF
  unsigned int Size_4; // [rsp+68h] [rbp-98h] BYREF
  int v38; // [rsp+6Ch] [rbp-94h]
  void *Block[2]; // [rsp+70h] [rbp-90h] BYREF
  unsigned __int64 n31_3; // [rsp+80h] [rbp-80h]
  unsigned __int64 n31_1; // [rsp+88h] [rbp-78h]
  _DWORD v42[4]; // [rsp+90h] [rbp-70h] BYREF
  __int64 v43; // [rsp+A0h] [rbp-60h]
  __int64 v44; // [rsp+A8h] [rbp-58h]
  __m128i si128; // [rsp+B0h] [rbp-50h] BYREF
  int n1729414956; // [rsp+C0h] [rbp-40h]
  __int16 v47; // [rsp+C4h] [rbp-3Ch]
  char *v48; // [rsp+C8h] [rbp-38h]
  char *v49; // [rsp+D0h] [rbp-30h]
  __int128 v50; // [rsp+D8h] [rbp-28h]
  char *v51; // [rsp+E8h] [rbp-18h]
  _OWORD v52[17]; // [rsp+F0h] [rbp-10h] BYREF

  v4 = a4;
  v5 = a3;
  v6 = sub_18000EA00();
  if ( v6 )
  {
    si128 = _mm_load_si128((const __m128i *)&xmmword_18004E0C0);
    n1729414956 = 1729414956;
    v47 = -717;
    v7 = operator new(0x20u);
    n31 = 31;
    n31_1 = 31;
    *v7 = 0;
    Block[0] = v7;
    n31_2 = 0;
    n31_3 = 0;
    v10 = 0;
    while ( 1 )
    {
      v11 = si128.m128i_i8[v10]
          ^ (27 - 125 * v10)
          ^ (0xBACFC73F >> (8 * (v10 & 3)))
          ^ (0xBEE06A27 >> (8 * (((unsigned __int8)&si128.m128i_u8[v10] + 1LL - (_QWORD)&si128) & 3)));
      if ( n31_2 >= n31 )
      {
        sub_180009740(Block);
      }
      else
      {
        n31_3 = n31_2 + 1;
        v12 = Block;
        if ( n31 >= 0x10 )
          v12 = (void **)v7;
        *((_BYTE *)v12 + n31_2) = v11;
        *((_BYTE *)v12 + n31_2 + 1) = 0;
      }
      if ( (unsigned __int64)++v10 >= 0x15 )
        break;
      n31 = n31_1;
      n31_2 = n31_3;
      v7 = Block[0];
    }
    Size_4 = 0;
    v42[0] = 4;
    v13 = ((__int64 (__fastcall *)(__int64, __int64, _QWORD, unsigned int *, _DWORD *))v6[3])(v5, 5, 0, &Size_4, v42);
    v14 = (char *)Block[0];
    if ( v13 )
    {
      Size_3 = 0;
      Size_5 = 0;
      if ( Size_4 )
      {
        do
        {
          Size = 0;
          if ( ((unsigned int (__fastcall *)(__int64, __int64, _QWORD))v6[3])(v5, 6, Size_3) )
          {
            Size_1 = Size;
            v50 = 0;
            v17 = 0;
            v48 = 0;
            v51 = 0;
            v18 = 0;
            if ( Size )
            {
              if ( Size < 0x1000uLL )
              {
                v18 = (char *)operator new(Size);
              }
              else
              {
                if ( (unsigned __int64)Size + 39 < Size )
                  Concurrency::cancel_current_task();
                v19 = operator new(Size + 39LL);
                if ( !v19 )
LABEL_57:
                  invalid_parameter_noinfo_noreturn();
                v18 = (char *)(((unsigned __int64)v19 + 39) & 0xFFFFFFFFFFFFFFE0uLL);
                *((_QWORD *)v18 - 1) = v19;
              }
              v20 = v18;
              v49 = v18;
              *(_QWORD *)&v50 = v18;
              v17 = &v18[Size_1];
              v48 = &v18[Size_1];
              v51 = &v18[Size_1];
              memset(v18, 0, Size_1);
              *((_QWORD *)&v50 + 1) = &v18[Size_1];
            }
            else
            {
              v20 = (char *)v50;
              v49 = (char *)v50;
            }
            if ( ((unsigned int (__fastcall *)(__int64, __int64, _QWORD, char *, unsigned int *))v6[3])(
                   a3,
                   6,
                   Size_3,
                   v18,
                   &Size) )
            {
              memset(v52, 0, 0xD0u);
              v52[3] = *(_OWORD *)(v18 + 8);
              *(_OWORD *)((char *)v52 + 8) = *(_OWORD *)(v18 + 24);
              v21 = ((__int64 (__fastcall *)(__int64, __int64, _QWORD, __int64, _OWORD *, _QWORD))v6[4])(
                      a2,
                      65537,
                      0,
                      720896,
                      v52,
                      0);
              v23 = v21;
              if ( v21 )
              {
                sub_1800141D0(Size_2, v21, v4);
                ((void (__fastcall *)(__int64))v6[5])(v23);
              }
              v24 = 0;
              v38 = 0;
              if ( *((_DWORD *)v18 + 30) )
              {
                v25 = (void **)Block[0];
                do
                {
                  v26 = *((_QWORD *)v18 + 16) + 24LL * v24;
                  v27 = *(char **)v26;
                  if ( *(_QWORD *)v26 )
                  {
                    v28 = Block;
                    if ( n31_1 >= 0x10 )
                      v28 = v25;
                    v29 = (char *)v28 - v27;
                    do
                    {
                      v30 = (unsigned __int8)v27[v29];
                      v31 = (unsigned __int8)*v27 - v30;
                      if ( v31 )
                        break;
                      ++v27;
                    }
                    while ( v30 );
                    if ( !v31 )
                    {
                      v32 = 0;
                      if ( *(_DWORD *)(v26 + 8) )
                      {
                        do
                        {
                          si128 = *(__m128i *)(*(_QWORD *)(v26 + 16) + 16LL * v32);
                          v44 = 0;
                          v43 = 0;
                          v42[3] = 0;
                          v42[2] = 0;
                          v42[1] = 0;
                          if ( ((unsigned int (__fastcall *)(__int64, __m128i *, __int64))v6[6])(2, &si128, 16382) )
                          {
                            sub_180013B70(a1, v44, v43, a4);
                            ((void (__fastcall *)(__int64))v6[7])(v43);
                            ((void (__fastcall *)(__int64, _QWORD))v6[8])(v44, 0);
                          }
                          ++v32;
                        }
                        while ( v32 < *(_DWORD *)(v26 + 8) );
                        v24 = v38;
                      }
                    }
                  }
                  v38 = ++v24;
                }
                while ( v24 < *((_DWORD *)v18 + 30) );
                v17 = v48;
                v20 = v49;
                Size_3 = Size_5;
              }
              if ( v20 )
              {
                v33 = v20;
                if ( (unsigned __int64)(v17 - v20) >= 0x1000 )
                {
                  v20 = (char *)*((_QWORD *)v20 - 1);
                  if ( (unsigned __int64)(v33 - v20 - 8) > 0x1F )
                    goto LABEL_57;
                }
                j_j_free(v20);
              }
              v4 = a4;
            }
            else if ( v18 )
            {
              if ( (unsigned __int64)(v17 - v18) >= 0x1000 )
              {
                if ( (unsigned __int64)&v18[-*((_QWORD *)v18 - 1) - 8] > 0x1F )
                  goto LABEL_57;
                v18 = (char *)*((_QWORD *)v18 - 1);
              }
              j_j_free(v18);
            }
            v5 = a3;
          }
          Size_5 = ++Size_3;
        }
        while ( Size_3 < Size_4 );
        v14 = (char *)Block[0];
      }
    }
    if ( n31_1 >= 0x10 )
    {
      v34 = v14;
      if ( n31_1 + 1 >= 0x1000 )
      {
        v14 = (char *)*((_QWORD *)v14 - 1);
        if ( (unsigned __int64)(v34 - v14 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v14);
    }
  }
}
