// Hidden C++ exception states: #wind=1
_BOOL8 sub_1800089A0()
{
  void **Buf2_1; // rdi
  void **Buf2_2; // rsi
  void **Buf2_4; // rsi
  void **Buf2_3; // rbx
  signed __int64 v4; // rdi
  void **Buf1_1; // rsi
  void **Buf1_6; // rdi
  unsigned __int64 n0x10_2; // r14
  void **Buf1_2; // r15
  size_t Size_1; // r8
  void **Buf1_4; // r15
  void **Buf1_3; // rbx
  signed __int64 v12; // rsi
  void **Buf2_5; // rdx
  char *v14; // rbx
  unsigned __int64 n0x10_3; // rsi
  void **Buf1_5; // rcx
  bool v17; // r15
  void **Buf1_7; // rax
  char *v19; // rax
  void *Buf1[2]; // [rsp+20h] [rbp-40h] BYREF
  size_t Size; // [rsp+30h] [rbp-30h]
  unsigned __int64 n0x10_1; // [rsp+38h] [rbp-28h]
  void *Buf2[2]; // [rsp+40h] [rbp-20h] BYREF
  __int64 Size_2; // [rsp+50h] [rbp-10h]
  unsigned __int64 n0x10; // [rsp+58h] [rbp-8h]

  sub_180002CD0(Buf2);
  Buf2_1 = Buf2;
  if ( n0x10 >= 0x10 )
    Buf2_1 = (void **)Buf2[0];
  Buf2_2 = Buf2;
  if ( n0x10 >= 0x10 )
    Buf2_2 = (void **)Buf2[0];
  Buf2_4 = (void **)((char *)Buf2_2 + Size_2);
  Buf2_3 = Buf2;
  if ( n0x10 >= 0x10 )
    Buf2_3 = (void **)Buf2[0];
  if ( Buf2_3 != Buf2_4 )
  {
    v4 = (char *)Buf2_1 - (char *)Buf2_3;
    do
    {
      *((_BYTE *)Buf2_3 + v4) = tolower(*(unsigned __int8 *)Buf2_3);
      Buf2_3 = (void **)((char *)Buf2_3 + 1);
    }
    while ( Buf2_3 != Buf2_4 );
  }
  sub_180002CD0(Buf1);
  Buf1_1 = Buf1;
  Buf1_6 = (void **)Buf1[0];
  n0x10_2 = n0x10_1;
  if ( n0x10_1 >= 0x10 )
    Buf1_1 = (void **)Buf1[0];
  Buf1_2 = Buf1;
  if ( n0x10_1 >= 0x10 )
    Buf1_2 = (void **)Buf1[0];
  Size_1 = Size;
  Buf1_4 = (void **)((char *)Buf1_2 + Size);
  Buf1_3 = Buf1;
  if ( n0x10_1 >= 0x10 )
    Buf1_3 = (void **)Buf1[0];
  if ( Buf1_3 != Buf1_4 )
  {
    v12 = (char *)Buf1_1 - (char *)Buf1_3;
    do
    {
      *((_BYTE *)Buf1_3 + v12) = tolower(*(unsigned __int8 *)Buf1_3);
      Buf1_3 = (void **)((char *)Buf1_3 + 1);
    }
    while ( Buf1_3 != Buf1_4 );
    n0x10_2 = n0x10_1;
    Size_1 = Size;
    Buf1_6 = (void **)Buf1[0];
  }
  Buf2_5 = Buf2;
  v14 = (char *)Buf2[0];
  n0x10_3 = n0x10;
  if ( n0x10 >= 0x10 )
    Buf2_5 = (void **)Buf2[0];
  Buf1_5 = Buf1;
  if ( n0x10_2 >= 0x10 )
    Buf1_5 = Buf1_6;
  v17 = Size_1 == Size_2 && !memcmp(Buf1_5, Buf2_5, Size_1);
  if ( n0x10_2 >= 0x10 )
  {
    Buf1_7 = Buf1_6;
    if ( n0x10_2 + 1 >= 0x1000 )
    {
      Buf1_6 = (void **)*(Buf1_6 - 1);
      if ( (unsigned __int64)((char *)Buf1_7 - (char *)Buf1_6 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(Buf1_6);
    n0x10_3 = n0x10;
    v14 = (char *)Buf2[0];
  }
  Size = 0;
  n0x10_1 = 15;
  LOBYTE(Buf1[0]) = 0;
  if ( n0x10_3 >= 0x10 )
  {
    v19 = v14;
    if ( n0x10_3 + 1 >= 0x1000 )
    {
      v14 = (char *)*((_QWORD *)v14 - 1);
      if ( (unsigned __int64)(v19 - v14 - 8) > 0x1F )
        invalid_parameter_noinfo_noreturn();
    }
    j_j_free(v14);
  }
  return v17;
}
