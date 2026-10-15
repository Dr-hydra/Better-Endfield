// Hidden C++ exception states: #wind=1
void **__fastcall sub_1800151F0(__int64 a1, void **lpWideCharStr)
{
  unsigned __int64 n0x2D; // rbx
  _BYTE *v4; // rax
  char v5; // r8
  char *n0x10_1; // rcx
  char *n0x10; // rdx
  void **lpWideCharStr_1; // rax
  _OWORD v10[2]; // [rsp+28h] [rbp-40h] BYREF
  int n2060599363; // [rsp+48h] [rbp-20h]
  int n1505587532; // [rsp+4Ch] [rbp-1Ch]
  int n1169372512; // [rsp+50h] [rbp-18h]
  __int16 n3452; // [rsp+54h] [rbp-14h]

  n0x2D = 0;
  v10[0] = _mm_load_si128((const __m128i *)&xmmword_18004E170);
  v10[1] = _mm_load_si128((const __m128i *)&xmmword_18004E020);
  n2060599363 = 2060599363;
  n1505587532 = 1505587532;
  n1169372512 = 1169372512;
  n3452 = 3452;
  *lpWideCharStr = 0;
  lpWideCharStr[2] = 0;
  lpWideCharStr[3] = (void *)15;
  *(_BYTE *)lpWideCharStr = 0;
  v4 = operator new(0x30u);
  lpWideCharStr[2] = (void *)45;
  lpWideCharStr[3] = (void *)47;
  *v4 = *(_BYTE *)lpWideCharStr;
  *lpWideCharStr = v4;
  lpWideCharStr[2] = 0;
  do
  {
    v5 = *((_BYTE *)v10 + n0x2D)
       ^ (-77 - 125 * n0x2D)
       ^ (0xDB58BDCD >> (8 * (n0x2D & 3)))
       ^ (0xFA0AFB51 >> (8 * ((1LL - (_QWORD)v10 + (unsigned __int8)v10 + n0x2D) & 3)));
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
    ++n0x2D;
  }
  while ( n0x2D < 0x2D );
  return lpWideCharStr;
}
