
// Beyond.Gameplay.View.CharacterAnimationComponent::_TickStatePerformInterrupt @0x5172410
char __fastcall Beyond_Gameplay_View_CharacterAnimationComponent___TickStatePerformInterrupt_5172410(
        _QWORD *a1,
        __int64 a2)
{
  __int64 v2; // rax
  _QWORD *v4; // rcx
  _QWORD *v5; // rcx
  __int64 v6; // rdx
  __int64 v7; // rcx
  __int64 (__fastcall *v8)(_QWORD, _QWORD *, __int64); // r9
  __int64 (__fastcall *v9)(_QWORD, _QWORD *, _QWORD); // r10
  __int64 (__fastcall *v10)(_QWORD, __int64, _QWORD); // r9
  __int64 (__fastcall *v11)(_QWORD, __int64, _QWORD); // rax
  __int64 self; // rcx

  LOBYTE(v2) = MEMORY[0x1171F250];
  v4 = *(_QWORD **)(MEMORY[0x1171F250] + 64LL);
  if ( !v4 )
    goto LABEL_2;
  v8 = (__int64 (__fastcall *)(_QWORD, _QWORD *, __int64))v4[6422];
  if ( v8 )
  {
    LOBYTE(v2) = v8(v4[6423], a1, a2);
    return v2;
  }
  v9 = (__int64 (__fastcall *)(_QWORD, _QWORD *, _QWORD))v4[6502];
  if ( !v9 )
  {
LABEL_2:
    if ( !a1[487] )
      return v2;
    v5 = *(_QWORD **)(MEMORY[0x1171F250] + 120LL);
    v6 = a1[487];
    if ( !v5 )
      goto LABEL_4;
    v10 = (__int64 (__fastcall *)(_QWORD, __int64, _QWORD))v5[1202];
    if ( v10 )
    {
      LOBYTE(v2) = v10(v5[1203], v6, 0);
      goto LABEL_7;
    }
    v11 = (__int64 (__fastcall *)(_QWORD, __int64, _QWORD))v5[982];
    if ( v11 )
      v2 = v11(v5[983], v6, 0);
    else
LABEL_4:
      v2 = *(_QWORD *)(v6 + 232);
    if ( v2 )
      LOBYTE(v2) = 1;
    goto LABEL_7;
  }
  LOBYTE(v2) = v9(v4[6503], a1 + 487, 0);
LABEL_7:
  if ( (_BYTE)v2 )
  {
    v7 = a1[488];
    if ( v7 )
    {
      LOBYTE(v2) = sub_55B5060(v7, a1, 0);
      if ( (_BYTE)v2 )
      {
        self = a1[487];
        if ( !self )
        {
          sub_DE940();
          __debugbreak();
        }
        Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase__ForceExit_68e91a0(self, 0, 0);
        *(_OWORD *)(a1 + 487) = 0;
        a1[489] = 0;
        LOBYTE(v2) = 0;
      }
    }
  }
  return v2;
}


// Beyond.Gameplay.Core.CharInteractPerform.CharPerformHandleBase::_TryExit @0x5342950
char __fastcall Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___TryExit_5342950(
        __int64 self,
        __int64 a2,
        __int64 a3)
{
  __int64 v4; // rcx
  __int64 (__fastcall *v6)(_QWORD, __int64, __int64, __int64); // rax

  v4 = *(_QWORD *)(MEMORY[0x1171F250] + 120LL);
  if ( v4 )
  {
    v6 = *(__int64 (__fastcall **)(_QWORD, __int64, __int64, __int64))(v4 + 9120);
    if ( v6 )
      return v6(*(_QWORD *)(v4 + 9128), self, a2, a3);
  }
  if ( (!*(_BYTE *)(self + 544) || !a2 || *(_DWORD *)(a2 + 16) != 2)
    && Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckEntityAlive_5342a40(self, 0) )
  {
    return 0;
  }
  Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___DoExit_68e9390(self, 0);
  return 1;
}


// Beyond.Gameplay.Core.CharInteractPerform.CharPerformHandleBase::_CheckTrackEnd @0x5345820
char __fastcall Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckTrackEnd_5345820(
        __int64 self,
        __int64 a2)
{
  __int64 v3; // rcx
  __int64 normalTrack_5345890; // rax
  __int64 normalTrack_5345890_1; // rax
  __int64 (__fastcall *v7)(_QWORD, __int64, __int64); // rax

  v3 = *(_QWORD *)(MEMORY[0x1171F250] + 120LL);
  if ( !v3 || (v7 = *(__int64 (__fastcall **)(_QWORD, __int64, __int64))(v3 + 9184)) == 0 )
  {
    normalTrack_5345890 = Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase__get_normalTrack_5345890(
                            self,
                            0);
    if ( normalTrack_5345890 )
    {
      if ( !(unsigned __int8)sub_5345990(normalTrack_5345890, 0) )
        return 1;
      normalTrack_5345890_1 = Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase__get_normalTrack_5345890(
                                self,
                                0);
      if ( normalTrack_5345890_1 )
        return !(unsigned __int8)sub_53456E0(normalTrack_5345890_1, 0);
    }
    sub_DE940();
    __debugbreak();
  }
  return v7(*(_QWORD *)(v3 + 9192), self, a2);
}


// Beyond.Gameplay.Core.CharInteractPerform.CharPerformHandleBase::_TickMainFlow @0x5342130
void __fastcall Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___TickMainFlow_5342130(
        __int64 self,
        float a2,
        __int64 a3,
        __int64 a4,
        __int64 a5,
        __int64 a6,
        __int64 a7,
        __int64 a8,
        __int64 a9,
        __int64 a10,
        __int64 a11)
{
  __int64 v11; // rdx
  __int64 v13; // rcx
  void (__fastcall *v14)(_QWORD, __int64, __int64, __int64); // rax
  float v15; // xmm1_4
  __int64 v16; // rcx
  void (__fastcall *v17)(_QWORD, __int64, __int64, __int64 *); // rax
  __int64 v18; // rcx
  __int64 v19; // rax
  __int64 (__fastcall *v20)(_QWORD, __int64, _QWORD); // r9
  int n5; // eax
  __int64 v22; // rax
  __int64 v23; // r9
  __int64 (__fastcall *v24)(_QWORD, _QWORD, _QWORD); // r10
  __int64 v25; // r10
  __int64 v26; // rax
  __int64 (__fastcall *v27)(_QWORD, __int64, __int64, _QWORD); // r11
  char v28; // al
  __int64 v29; // r10
  unsigned __int8 (__fastcall *v30)(_QWORD, __int64, __int64, _QWORD); // rax
  __int64 v31; // rcx
  __int64 (__fastcall *v32)(_QWORD, __int64, _QWORD); // rax
  char v33; // al
  __int64 normalTrack_5345890; // rax
  __int64 normalTrack_5345890_1; // rax
  __int64 v36; // rcx
  void (__fastcall *v37)(_QWORD, __int64, __int64, _QWORD); // r10
  __int64 v38; // r10
  __int64 (__fastcall *v39)(_QWORD, __int64, __int64, _QWORD); // r11
  char v40; // al
  __int64 v41; // r10
  unsigned __int8 (__fastcall *v42)(_QWORD, __int64, __int64, _QWORD); // rax
  __int64 normalTrack_5345890_2; // rax
  __int64 interactPerformCfg_5342100; // rax
  __int64 interactPerformCfg_5342100_1; // rax
  __int64 v46; // rax
  __int64 v47; // r10
  __int64 (__fastcall *v48)(_QWORD, __int64, __int64, _QWORD); // r11
  char v49; // al
  __int64 v50; // r10
  unsigned __int8 (__fastcall *v51)(_QWORD, __int64, __int64, _QWORD); // rax
  __int64 normalTrack_5345890_3; // rax
  __int64 v53; // rcx
  unsigned __int8 (__fastcall *v54)(_QWORD, __int64, __int64, _QWORD); // r10
  __int64 normalTrack_5345890_4; // rax

  v11 = MEMORY[0x1171F250];
  v13 = *(_QWORD *)(MEMORY[0x1171F250] + 120LL);
  if ( v13 )
  {
    v14 = *(void (__fastcall **)(_QWORD, __int64, __int64, __int64))(v13 + 9152);
    if ( v14 )
    {
      v14(*(_QWORD *)(v13 + 9160), self, a3, a3);
      return;
    }
  }
  v15 = a2 + *(float *)(self + 408);
  a11 = 0;
  *(float *)(self + 408) = v15;
  v16 = *(_QWORD *)(v11 + 120);
  if ( v16 && (v17 = *(void (__fastcall **)(_QWORD, __int64, __int64, __int64 *))(v16 + 9424)) != 0 )
    v17(*(_QWORD *)(v16 + 9432), self, self + 544, &a11);
  else
    *(_BYTE *)(self + 544) |= Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___TryGetCurInterruptCommand_53429a0(
                                self,
                                (__int64)&a11,
                                0);
  v18 = MEMORY[0x1171F250];
  v19 = *(_QWORD *)(MEMORY[0x1171F250] + 120LL);
  if ( v19 && (v20 = *(__int64 (__fastcall **)(_QWORD, __int64, _QWORD))(v19 + 8000)) != 0 )
  {
    n5 = v20(*(_QWORD *)(v19 + 8008), self, 0);
    v18 = MEMORY[0x1171F250];
  }
  else
  {
    v22 = *(_QWORD *)(self + 392);
    if ( !v22 )
      return;
    v23 = *(_QWORD *)(MEMORY[0x1171F250] + 120LL);
    if ( v23 && (v24 = *(__int64 (__fastcall **)(_QWORD, _QWORD, _QWORD))(v23 + 11456)) != 0 )
    {
      n5 = v24(*(_QWORD *)(v23 + 11464), *(_QWORD *)(self + 392), 0);
      v18 = MEMORY[0x1171F250];
    }
    else
    {
      n5 = *(_DWORD *)(v22 + 32);
    }
  }
  if ( n5 != 2 )
  {
    if ( n5 != 3 )
    {
      if ( n5 != 4 )
      {
        if ( n5 != 5 )
          return;
        v25 = *(_QWORD *)(v18 + 120);
        v26 = a11;
        if ( v25 )
        {
          v27 = *(__int64 (__fastcall **)(_QWORD, __int64, __int64, _QWORD))(v25 + 9168);
          if ( v27 )
          {
            v28 = v27(*(_QWORD *)(v25 + 9176), self, a11, 0);
            goto LABEL_27;
          }
        }
        if ( !*(_BYTE *)(self + 544) || !a11 || *(_DWORD *)(a11 + 16) != 2 )
        {
          v28 = !Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckEntityAlive_5342a40(self, 0);
LABEL_27:
          if ( !v28 )
          {
            v29 = *(_QWORD *)(MEMORY[0x1171F250] + 120LL);
            if ( v29 && (v30 = *(unsigned __int8 (__fastcall **)(_QWORD, __int64, __int64, _QWORD))(v29 + 9120)) != 0 )
            {
              if ( v30(*(_QWORD *)(v29 + 9128), self, a11, 0) )
                return;
            }
            else if ( *(_BYTE *)(self + 544) && a11 && *(_DWORD *)(a11 + 16) == 2
                   || !Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckEntityAlive_5342a40(
                         self,
                         0) )
            {
              goto LABEL_53;
            }
            v31 = *(_QWORD *)(MEMORY[0x1171F250] + 120LL);
            if ( v31 )
            {
              v32 = *(__int64 (__fastcall **)(_QWORD, __int64, _QWORD))(v31 + 9184);
              if ( v32 )
              {
                v33 = v32(*(_QWORD *)(v31 + 9192), self, 0);
                goto LABEL_43;
              }
            }
            normalTrack_5345890 = Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase__get_normalTrack_5345890(
                                    self,
                                    0);
            if ( normalTrack_5345890 )
            {
              if ( !(unsigned __int8)sub_5345990(normalTrack_5345890, 0) )
                goto LABEL_53;
              normalTrack_5345890_1 = Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase__get_normalTrack_5345890(
                                        self,
                                        0);
              if ( normalTrack_5345890_1 )
              {
                v33 = sub_53456E0(normalTrack_5345890_1, 0) ^ 1;
LABEL_43:
                if ( !v33 )
                  return;
LABEL_53:
                Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___DoExit_68e9390(self, 0);
                return;
              }
            }
LABEL_119:
            sub_DE930();
            __debugbreak();
          }
          goto LABEL_45;
        }
LABEL_46:
        v36 = *(_QWORD *)(v18 + 120);
        if ( v36 )
        {
          v37 = *(void (__fastcall **)(_QWORD, __int64, __int64, _QWORD))(v36 + 9120);
          if ( v37 )
          {
LABEL_48:
            v37(*(_QWORD *)(v36 + 9128), self, v26, 0);
            return;
          }
        }
        goto LABEL_49;
      }
      v38 = *(_QWORD *)(v18 + 120);
      v26 = a11;
      if ( v38 && (v39 = *(__int64 (__fastcall **)(_QWORD, __int64, __int64, _QWORD))(v38 + 9168)) != 0 )
      {
        v40 = v39(*(_QWORD *)(v38 + 9176), self, a11, 0);
      }
      else
      {
        if ( *(_BYTE *)(self + 544) && a11 && *(_DWORD *)(a11 + 16) == 2 )
          goto LABEL_84;
        v40 = !Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckEntityAlive_5342a40(self, 0);
      }
      if ( !v40 )
      {
        v41 = *(_QWORD *)(MEMORY[0x1171F250] + 120LL);
        if ( v41 && (v42 = *(unsigned __int8 (__fastcall **)(_QWORD, __int64, __int64, _QWORD))(v41 + 9120)) != 0 )
        {
          if ( v42(*(_QWORD *)(v41 + 9128), self, a11, 0) )
            return;
        }
        else if ( *(_BYTE *)(self + 544) && a11 && *(_DWORD *)(a11 + 16) == 2
               || !Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckEntityAlive_5342a40(self, 0) )
        {
          goto LABEL_53;
        }
        if ( !Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckEnd_65c0610(self, 0) )
        {
          normalTrack_5345890_2 = Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase__get_normalTrack_5345890(
                                    self,
                                    0);
          if ( !normalTrack_5345890_2 )
            goto LABEL_119;
          if ( (unsigned __int8)sub_5345990(normalTrack_5345890_2, 0) )
            return;
          if ( *(_BYTE *)(self + 400) )
          {
            interactPerformCfg_5342100 = Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase__get_interactPerformCfg_5342100(
                                           self,
                                           0);
            if ( !interactPerformCfg_5342100 )
              goto LABEL_119;
            if ( *(_BYTE *)(interactPerformCfg_5342100 + 76) )
            {
              interactPerformCfg_5342100_1 = Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase__get_interactPerformCfg_5342100(
                                               self,
                                               0);
              if ( !interactPerformCfg_5342100_1 )
                goto LABEL_119;
              v46 = *(_QWORD *)(interactPerformCfg_5342100_1 + 176);
              if ( !v46 )
                goto LABEL_119;
              if ( *(int *)(v46 + 24) > 0 )
              {
                Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase__DoLoop_6e11f20(self, 0, 0);
                return;
              }
            }
            if ( *(_BYTE *)(self + 400) )
              return;
          }
        }
        goto LABEL_81;
      }
      v26 = a11;
      v18 = MEMORY[0x1171F250];
LABEL_84:
      v36 = *(_QWORD *)(v18 + 120);
      if ( v36 )
      {
        v37 = *(void (__fastcall **)(_QWORD, __int64, __int64, _QWORD))(v36 + 9120);
        if ( v37 )
          goto LABEL_48;
      }
LABEL_49:
      if ( *(_BYTE *)(self + 544) && v26 && *(_DWORD *)(v26 + 16) == 2
        || !Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckEntityAlive_5342a40(self, 0) )
      {
        goto LABEL_53;
      }
      return;
    }
    v47 = *(_QWORD *)(v18 + 120);
    v26 = a11;
    if ( v47 && (v48 = *(__int64 (__fastcall **)(_QWORD, __int64, __int64, _QWORD))(v47 + 9168)) != 0 )
    {
      v49 = v48(*(_QWORD *)(v47 + 9176), self, a11, 0);
    }
    else
    {
      if ( *(_BYTE *)(self + 544) && a11 && *(_DWORD *)(a11 + 16) == 2 )
        goto LABEL_46;
      v49 = !Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckEntityAlive_5342a40(self, 0);
    }
    if ( v49 )
    {
LABEL_45:
      v26 = a11;
      v18 = MEMORY[0x1171F250];
      goto LABEL_46;
    }
    v50 = *(_QWORD *)(MEMORY[0x1171F250] + 120LL);
    if ( v50 && (v51 = *(unsigned __int8 (__fastcall **)(_QWORD, __int64, __int64, _QWORD))(v50 + 9120)) != 0 )
    {
      if ( v51(*(_QWORD *)(v50 + 9128), self, a11, 0) )
        return;
    }
    else if ( *(_BYTE *)(self + 544) && a11 && *(_DWORD *)(a11 + 16) == 2
           || !Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckEntityAlive_5342a40(self, 0) )
    {
      goto LABEL_53;
    }
    if ( !Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckEnd_65c0610(self, 0) )
    {
      normalTrack_5345890_3 = Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase__get_normalTrack_5345890(
                                self,
                                0);
      if ( !normalTrack_5345890_3 )
        goto LABEL_119;
      if ( !(unsigned __int8)sub_5345990(normalTrack_5345890_3, 0) )
        Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase__DoLoop_6e11f20(self, 1, 0);
      return;
    }
LABEL_81:
    Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___DoEnd_6b0b760(self, 0);
    return;
  }
  v53 = *(_QWORD *)(v18 + 120);
  if ( v53 && (v54 = *(unsigned __int8 (__fastcall **)(_QWORD, __int64, __int64, _QWORD))(v53 + 9120)) != 0 )
  {
    if ( v54(*(_QWORD *)(v53 + 9128), self, a11, 0) )
      return;
  }
  else if ( *(_BYTE *)(self + 544) && a11 && *(_DWORD *)(a11 + 16) == 2
         || !Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckEntityAlive_5342a40(self, 0) )
  {
    goto LABEL_53;
  }
  if ( Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___CheckEnd_65c0610(self, 0) )
    goto LABEL_81;
  normalTrack_5345890_4 = Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase__get_normalTrack_5345890(
                            self,
                            0);
  if ( !normalTrack_5345890_4 )
    goto LABEL_119;
  if ( !(unsigned __int8)sub_5345990(normalTrack_5345890_4, 0) )
    Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___DoStart_5e1daf0(self, 0);
}


// Beyond.Gameplay.Core.CharInteractPerform.CharPerformHandleBase::_DoExit @0x68e9390
__int64 __fastcall Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___DoExit_68e9390(
        __int64 self,
        __int64 a2)
{
  __int64 v4; // rcx
  __int64 v5; // rdx
  __int64 (__fastcall *v7)(_QWORD, __int64, __int64); // rax

  if ( !MEMORY[0x1172526F] )
  {
    sub_414A0(276086472);
    sub_414A0(276291672);
    MEMORY[0x1172526F] = 1;
  }
  v4 = *(_QWORD *)(MEMORY[0x1171F250] + 120LL);
  if ( v4 )
  {
    v7 = *(__int64 (__fastcall **)(_QWORD, __int64, __int64))(v4 + 9280);
    if ( v7 )
      return v7(*(_QWORD *)(v4 + 9288), self, a2);
  }
  Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___FirePhaseEndWindowActions_53465c0(self, 0);
  if ( !*(_DWORD *)(MEMORY[0x1074BEC8] + 224LL) )
    sub_165E0();
  LOBYTE(v5) = 4;
  sub_5DBD640(9, v5, MEMORY[0x1077E058], 0);
  Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___BeforeExit_68e9470(self, 0);
  Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___RecoverEntityState_5f7acf0(self, 0);
  Beyond_Gameplay_Core_CharInteractPerform_CharPerformHandleBase___SwitchState_5f76ee0(self, 5, 0, 0);
  return sub_3A4C0(14, self, 0);
}

