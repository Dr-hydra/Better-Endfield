// Hidden C++ exception states: #wind=1
void **__fastcall sub_180015330(__int64 a1, void **lpWideCharStr)
{
  unsigned __int64 n0x2C; // rbx
  _BYTE *v4; // rax
  char v5; // r8
  char *n0x10_1; // rcx
  char *n0x10; // rdx
  void **lpWideCharStr_1; // rax
  _OWORD v10[2]; // [rsp+28h] [rbp-40h] BYREF
  int n618296315; // [rsp+48h] [rbp-20h]
  int n550581490; // [rsp+4Ch] [rbp-1Ch]
  int n350560942; // [rsp+50h] [rbp-18h]
  char v14; // [rsp+54h] [rbp-14h]

  n0x2C = 0;
  v10[0] = _mm_load_si128((const __m128i *)&xmmword_18004E260);
  v10[1] = _mm_load_si128((const __m128i *)&xmmword_18004E230);
  n618296315 = 618296315;
  n550581490 = 550581490;
  n350560942 = 350560942;
  v14 = -66;
  *lpWideCharStr = 0;
  lpWideCharStr[2] = 0;
  lpWideCharStr[3] = (void *)15;
  *(_BYTE *)lpWideCharStr = 0;
  v4 = operator new(0x30u);
  lpWideCharStr[2] = (void *)44;
  lpWideCharStr[3] = (void *)47;
  *v4 = *(_BYTE *)lpWideCharStr;
  *lpWideCharStr = v4;
  lpWideCharStr[2] = 0;
  do
  {
    v5 = *((_BYTE *)v10 + n0x2C)
       ^ (-94 - 125 * n0x2C)
       ^ (0x1944F257u >> (8 * (n0x2C & 3)))
       ^ (0xFA6DCFD3 >> (8 * ((1LL - (_QWORD)v10 + (unsigned __int8)v10 + n0x2C) & 3)));
    n0x10_1 = (char *)lpWideCharStr[2];
    n0x10 = (char *)lpWideCharStr[3];
    if ( n0x10_1 >= n0x10 )
    {
      sub_180009740(lpWideCharStr);
    }
    else
    {
      lpWideCharStr[2] = n0x10_1 + 1;
      lpWideCharStr_1 = lpWideCharStr;
      if ( (unsigned __int64)n0x10 >= 0x10 )
        lpWideCharStr_1 = (void **)*lpWideCharStr;
      n0x10_1[(_QWORD)lpWideCharStr_1] = v5;
      n0x10_1[(_QWORD)lpWideCharStr_1 + 1] = 0;
    }
    ++n0x2C;
  }
  while ( n0x2C < 0x2C );
  return lpWideCharStr;
}
