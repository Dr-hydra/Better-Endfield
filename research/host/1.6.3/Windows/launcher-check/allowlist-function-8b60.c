// Hidden C++ exception states: #wind=1
_QWORD *__fastcall sub_180008B60(_QWORD *Src, _BYTE *a2, unsigned int a3)
{
  __int64 v3; // rsi
  char v6; // r9
  unsigned __int64 n0x10_1; // rcx
  unsigned __int64 n0x10; // rdx
  _QWORD *Src_1; // rax
  char v10; // r9
  unsigned __int64 n0x10_3; // rcx
  unsigned __int64 n0x10_2; // rdx
  _QWORD *Src_2; // rax

  v3 = a3;
  *Src = 0;
  Src[2] = 0;
  Src[3] = 15;
  *(_BYTE *)Src = 0;
  sub_180009390(Src, 2 * a3);
  if ( (_DWORD)v3 )
  {
    do
    {
      v6 = a0123456789abcd[(unsigned __int64)(unsigned __int8)*a2 >> 4];
      n0x10_1 = Src[2];
      n0x10 = Src[3];
      if ( n0x10_1 >= n0x10 )
      {
        sub_180009740(Src);
      }
      else
      {
        Src[2] = n0x10_1 + 1;
        Src_1 = Src;
        if ( n0x10 >= 0x10 )
          Src_1 = (_QWORD *)*Src;
        *((_BYTE *)Src_1 + n0x10_1) = v6;
        *((_BYTE *)Src_1 + n0x10_1 + 1) = 0;
      }
      v10 = a0123456789abcd[*a2 & 0xF];
      n0x10_3 = Src[2];
      n0x10_2 = Src[3];
      if ( n0x10_3 >= n0x10_2 )
      {
        sub_180009740(Src);
      }
      else
      {
        Src[2] = n0x10_3 + 1;
        Src_2 = Src;
        if ( n0x10_2 >= 0x10 )
          Src_2 = (_QWORD *)*Src;
        *((_BYTE *)Src_2 + n0x10_3) = v10;
        *((_BYTE *)Src_2 + n0x10_3 + 1) = 0;
      }
      ++a2;
      --v3;
    }
    while ( v3 );
  }
  return Src;
}
