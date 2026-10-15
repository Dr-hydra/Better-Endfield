// Hidden C++ exception states: #wind=2
void __fastcall sub_1800141D0(size_t Size, __int64 a2, __int64 a3)
{
  __int64 v3; // r15
  HMODULE *v5; // rax
  HMODULE *v6; // r13
  char *v7; // rdi
  size_t Sizea_1; // r14
  char *v9; // rsi
  _QWORD *v10; // rbx
  void *v11; // rax
  _BYTE *v12; // r15
  _QWORD *v13; // rax
  void *v14; // rcx
  size_t Sizea_2; // rsi
  _QWORD *v16; // rbx
  void *v17; // rax
  _BYTE *v18; // r14
  _QWORD *v19; // rax
  void *v20; // rcx
  void *Block[3]; // [rsp+38h] [rbp-28h] BYREF
  unsigned __int64 n0x10; // [rsp+50h] [rbp-10h]
  size_t Sizea; // [rsp+A0h] [rbp+40h] BYREF
  __int64 v24; // [rsp+B0h] [rbp+50h]

  v24 = a3;
  Sizea = Size;
  v3 = a3;
  v5 = sub_18000EA00();
  v6 = v5;
  if ( !v5 )
    return;
  v7 = 0;
  LODWORD(Sizea) = 0;
  if ( ((unsigned int (__fastcall *)(__int64, __int64, _QWORD, size_t *))v5[9])(a2, 3, 0, &Sizea) )
  {
    Sizea_1 = (unsigned int)Sizea;
    v9 = 0;
    v10 = 0;
    if ( (_DWORD)Sizea )
    {
      if ( (unsigned int)Sizea < 0x1000uLL )
      {
        v10 = operator new((unsigned int)Sizea);
      }
      else
      {
        if ( (unsigned __int64)(unsigned int)Sizea + 39 < (unsigned int)Sizea )
          goto LABEL_45;
        v11 = operator new((unsigned int)Sizea + 39LL);
        if ( !v11 )
          goto LABEL_44;
        v10 = (_QWORD *)(((unsigned __int64)v11 + 39) & 0xFFFFFFFFFFFFFFE0uLL);
        *(v10 - 1) = v11;
      }
      v12 = v10;
      v9 = (char *)v10 + Sizea_1;
      memset(v10, 0, Sizea_1);
    }
    else
    {
      v12 = 0;
    }
    if ( ((unsigned int (__fastcall *)(__int64, __int64, _BYTE *, size_t *))v6[9])(a2, 3, v12, &Sizea) )
    {
      v13 = sub_180008B60(Block, v12, Sizea);
      v3 = v24;
      sub_180016E30(v24, v13);
      if ( n0x10 >= 0x10 )
      {
        v14 = Block[0];
        if ( n0x10 + 1 >= 0x1000 )
        {
          v14 = (void *)*((_QWORD *)Block[0] - 1);
          if ( (unsigned __int64)((char *)Block[0] - (char *)v14 - 8) > 0x1F )
            invalid_parameter_noinfo_noreturn();
        }
        j_j_free(v14);
      }
    }
    else
    {
      v3 = v24;
    }
    if ( v10 )
    {
      if ( (unsigned __int64)(v9 - (char *)v10) >= 0x1000 )
      {
        if ( (unsigned __int64)v10 - *(v10 - 1) - 8 > 0x1F )
          goto LABEL_44;
        v10 = (_QWORD *)*(v10 - 1);
      }
      j_j_free(v10);
    }
  }
  LODWORD(Sizea) = 0;
  if ( !((unsigned int (__fastcall *)(__int64, __int64, _QWORD, size_t *))v6[9])(a2, 107, 0, &Sizea) )
    return;
  Sizea_2 = (unsigned int)Sizea;
  v16 = 0;
  if ( !(_DWORD)Sizea )
  {
    v18 = 0;
    goto LABEL_32;
  }
  if ( (unsigned int)Sizea >= 0x1000uLL )
  {
    if ( (unsigned __int64)(unsigned int)Sizea + 39 >= (unsigned int)Sizea )
    {
      v17 = operator new((unsigned int)Sizea + 39LL);
      if ( !v17 )
        goto LABEL_44;
      v16 = (_QWORD *)(((unsigned __int64)v17 + 39) & 0xFFFFFFFFFFFFFFE0uLL);
      *(v16 - 1) = v17;
      goto LABEL_30;
    }
LABEL_45:
    Concurrency::cancel_current_task();
  }
  v16 = operator new((unsigned int)Sizea);
LABEL_30:
  v18 = v16;
  v7 = (char *)v16 + Sizea_2;
  memset(v16, 0, Sizea_2);
LABEL_32:
  if ( ((unsigned int (__fastcall *)(__int64, __int64, _BYTE *, size_t *))v6[9])(a2, 107, v18, &Sizea) )
  {
    v19 = sub_180008B60(Block, v18, Sizea);
    sub_180016E30(v3, v19);
    if ( n0x10 >= 0x10 )
    {
      v20 = Block[0];
      if ( n0x10 + 1 >= 0x1000 )
      {
        v20 = (void *)*((_QWORD *)Block[0] - 1);
        if ( (unsigned __int64)((char *)Block[0] - (char *)v20 - 8) > 0x1F )
          invalid_parameter_noinfo_noreturn();
      }
      j_j_free(v20);
    }
  }
  if ( v16 )
  {
    if ( (unsigned __int64)(v7 - (char *)v16) < 0x1000 )
    {
LABEL_42:
      j_j_free(v16);
      return;
    }
    if ( (unsigned __int64)v16 - *(v16 - 1) - 8 <= 0x1F )
    {
      v16 = (_QWORD *)*(v16 - 1);
      goto LABEL_42;
    }
LABEL_44:
    invalid_parameter_noinfo_noreturn();
  }
}
