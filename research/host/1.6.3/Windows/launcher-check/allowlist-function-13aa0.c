char __fastcall sub_180013AA0(__int64 a1, __int64 a2)
{
  HMODULE *v3; // rax
  _DWORD v5[4]; // [rsp+20h] [rbp-39h] BYREF
  __int128 v6; // [rsp+30h] [rbp-29h] BYREF
  __int64 v7; // [rsp+40h] [rbp-19h]
  __int64 n2; // [rsp+48h] [rbp-11h]
  __int64 v9; // [rsp+50h] [rbp-9h]
  _QWORD *v10; // [rsp+58h] [rbp-1h]
  __int128 v11; // [rsp+60h] [rbp+7h]
  __int64 v12; // [rsp+70h] [rbp+17h]
  __int64 n16; // [rsp+78h] [rbp+1Fh]
  __int64 v14; // [rsp+80h] [rbp+27h]
  _QWORD v15[2]; // [rsp+90h] [rbp+37h] BYREF
  __int128 v16; // [rsp+A0h] [rbp+47h]

  v3 = sub_18000EA00();
  if ( v3 )
  {
    v15[0] = 32;
    v15[1] = a2;
    v14 = 0;
    v9 = 1;
    v6 = 0;
    LODWORD(v6) = 88;
    v7 = 0;
    v10 = v15;
    v11 = 0;
    LODWORD(v11) = 0;
    v12 = 0;
    n2 = 2;
    v16 = 0;
    n16 = 16;
    v5[0] = 11191659;
    v5[1] = 298896708;
    v5[2] = -1073692020;
    v5[3] = -292175281;
    LOBYTE(v3) = ((unsigned int (__fastcall *)(_QWORD, _DWORD *, __int128 *))v3[2])(0, v5, &v6) == 0;
  }
  return (char)v3;
}
