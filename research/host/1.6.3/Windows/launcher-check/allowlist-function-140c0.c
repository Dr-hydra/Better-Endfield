// Hidden C++ exception states: #wind=3
void **__fastcall sub_1800140C0(__int64 a1, void **lpMultiByteStr, __int64 a3)
{
  HMODULE *v6; // rax
  HMODULE *v7; // rdi
  int v9; // [rsp+60h] [rbp+17h] BYREF
  int v10; // [rsp+64h] [rbp+1Bh] BYREF
  __int64 v11; // [rsp+68h] [rbp+1Fh] BYREF
  __int64 v12; // [rsp+70h] [rbp+27h] BYREF
  __int128 v13; // [rsp+78h] [rbp+2Fh] BYREF
  void *v14; // [rsp+88h] [rbp+3Fh]
  int v15; // [rsp+C8h] [rbp+7Fh] BYREF

  v13 = 0;
  v14 = 0;
  v6 = sub_18000EA00();
  v7 = v6;
  if ( v6 )
  {
    v12 = 0;
    v11 = 0;
    v10 = 0;
    v9 = 0;
    v15 = 0;
    if ( ((unsigned int (__fastcall *)(__int64, __int64, __int64, __int64, _DWORD, int *, int *, int *, __int64 *, __int64 *, _QWORD))v6[6])(
           1,
           a3,
           1024,
           2,
           0,
           &v10,
           &v9,
           &v15,
           &v12,
           &v11,
           0) )
    {
      sub_180013B70(a1, v12, v11, &v13);
      ((void (__fastcall *)(__int64))v7[7])(v11);
      ((void (__fastcall *)(__int64, _QWORD))v7[8])(v12, 0);
    }
  }
  *(_OWORD *)lpMultiByteStr = v13;
  lpMultiByteStr[2] = v14;
  return lpMultiByteStr;
}
