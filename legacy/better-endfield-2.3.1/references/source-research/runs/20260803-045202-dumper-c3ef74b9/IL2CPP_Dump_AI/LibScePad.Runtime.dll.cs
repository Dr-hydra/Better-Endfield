// ========================================================
// Dumped by @desirepro
// Assembly: LibScePad.Runtime.dll
// Classes:  90
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

# AI-FRIENDLY STRUCTURED DUMP
# Optimized for LLM parsing / code generation

CLASS: <Module>
TYPE:  class
TOKEN: 0x2000001
SIZE:  0x1
FIELDS:
METHODS:
END_CLASS

CLASS: Audio
TYPE:  static class
TOKEN: 0x2000003
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09EB3504  token=0x6000002  System.Int32 IsSupportAudioFunction(System.Int32 handle)
  RVA=0x09EB3554  token=0x6000003  System.Int32 SetAudioOutPath(System.Int32 handle, Plugins.LibScePad.ScePadAudioOutPath path)
  RVA=0x09EB35B0  token=0x6000004  System.Int32 SetVolumeGain(System.Int32 handle, System.Byte speakerVol, System.Byte jackVol, System.Byte micGain)
  RVA=0x09EB3494  token=0x6000005  Plugins.LibScePad.ScePadHeadsetType GetHeadSetType(System.Int32 handle)
END_CLASS

CLASS: Basic
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03329C50  token=0x6000006  System.Int32 Init()
  RVA=0x09EB37B0  token=0x6000007  System.Int32 GetOrOpen(System.Int32 userId, Plugins.LibScePad.ScePadType type)
  RVA=0x09EB3750  token=0x6000008  System.Int32 GetHandle(System.Int32 userId, Plugins.LibScePad.ScePadType type)
  RVA=0x09EB3644  token=0x6000009  System.Int32 Close(System.Int32 handle)
  RVA=0x09EB3948  token=0x600000A  System.Int32 ResetLightBar(System.Int32 handle)
  RVA=0x09EB3AA0  token=0x600000B  System.Int32 SetLightBar(System.Int32 handle, UnityEngine.Color color)
  RVA=0x09EB3B34  token=0x600000C  System.Int32 SetLightBar(System.Int32 handle, System.Byte r, System.Byte g, System.Byte b)
  RVA=0x09EB3694  token=0x600000D  System.Int32 GetControllerInformation(System.Int32 handle, Plugins.LibScePad.ScePadControllerInformation& info)
  RVA=0x09EB381C  token=0x600000E  System.Int32 ReadState(System.Int32 handle, Plugins.LibScePad.ScePadData& data)
  RVA=0x09EB3BC8  token=0x600000F  System.Int32 SetMotionSensorState(System.Int32 handle, System.Boolean enable)
  RVA=0x09EB3C24  token=0x6000010  System.Int32 SetTiltCorrectionState(System.Int32 handle, System.Boolean enable)
  RVA=0x09EB3A44  token=0x6000011  System.Int32 SetAngularVelocityDeadbandState(System.Int32 handle, System.Boolean enable)
  RVA=0x09EB39E8  token=0x6000012  System.Int32 SetAngularVelocityBiasCorrectionState(System.Int32 handle, System.Boolean enable)
  RVA=0x09EB3998  token=0x6000013  System.Int32 ResetOrientation(System.Int32 handle)
  RVA=0x09EB3C80  token=0x6000014  System.Int32 SetVibrationMode(System.Int32 handle, Plugins.LibScePad.ScePadVibrationMode mode)
  RVA=0x03329CA0  token=0x6000015  System.Int32 _InitWithBluetooth()
END_CLASS

CLASS: Trigger
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
  private   static  Plugins.LibScePad.ScePadTriggerEffectOffParamIternal.Command[]s_cachedOffEffectCmdArray  // static @ 0x0
  private   static  Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal.Command[]s_cachedFeedBackEffectCmdArray  // static @ 0x8
  private   static  Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal.Command[]s_cachedVibrationEffectCmdArray  // static @ 0x10
  private   static  Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal.Command[]s_cachedWeaponEffectCmdArray  // static @ 0x18
  private   static  Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal.Command[]s_cachedSlopeEffectCmdArray  // static @ 0x20
  private   static  Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal.Command[]s_cachedMulPosEffectCmdArray  // static @ 0x28
  private   static  Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal.Command[]s_cachedMulVibEffectCmdArray  // static @ 0x30
METHODS:
  RVA=0x09EB4E40  token=0x6000016  System.Int32 GetTriggerEffectStates(System.Int32 handle, Plugins.LibScePad.ScePadTriggerEffectState& l2, Plugins.LibScePad.ScePadTriggerEffectState& r2)
  RVA=0x09EB4EC4  token=0x6000017  System.Int32 SetTriggerEffectOff(System.Int32 handle, Plugins.LibScePad.TriggerMask mask)
  RVA=0x09EB4FC0  token=0x6000018  System.Int32 SetTriggerEffect(System.Int32 handle, Plugins.LibScePad.TriggerEffectCommandUnion command)
  RVA=0x09EB51EC  token=0x6000019  System.Int32 _SetTriggerEffectFeedback(System.Int32 handle, Plugins.LibScePad.TriggerMask mask, Plugins.LibScePad.TriggerFeedbackEffect paramL2, Plugins.LibScePad.TriggerFeedbackEffect paramR2)
  RVA=0x09EB59DC  token=0x600001A  System.Int32 _SetTriggerEffectVibration(System.Int32 handle, Plugins.LibScePad.TriggerMask mask, Plugins.LibScePad.TriggerVibrationEffect paramL2, Plugins.LibScePad.TriggerVibrationEffect paramR2)
  RVA=0x09EB5BFC  token=0x600001B  System.Int32 _SetTriggerEffectWeapon(System.Int32 handle, Plugins.LibScePad.TriggerMask mask, Plugins.LibScePad.TriggerWeaponEffect paramL2, Plugins.LibScePad.TriggerWeaponEffect paramR2)
  RVA=0x09EB5770  token=0x600001C  System.Int32 _SetTriggerEffectSlope(System.Int32 handle, Plugins.LibScePad.TriggerMask mask, Plugins.LibScePad.TriggerSlopeFeedbackEffect slopeL2, Plugins.LibScePad.TriggerSlopeFeedbackEffect slopeR2)
  RVA=0x09EB55B8  token=0x600001D  System.Int32 _SetTriggerEffectMultiPosition(System.Int32 handle, Plugins.LibScePad.TriggerMask mask, Plugins.LibScePad.TriggerMultiPositionFeedbackEffect paramL2, Plugins.LibScePad.TriggerMultiPositionFeedbackEffect paramR2)
  RVA=0x09EB53B0  token=0x600001E  System.Int32 _SetTriggerEffectMultiPositionVibration(System.Int32 handle, Plugins.LibScePad.TriggerMask mask, Plugins.LibScePad.TriggerMultiPositionVibrationEffect paramL2, Plugins.LibScePad.TriggerMultiPositionVibrationEffect paramR2)
  RVA=0x09EB5E1C  token=0x600001F  System.Void .cctor()
END_CLASS

CLASS: <reserve>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x20
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <reserve>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x12
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <deviceUniqueData>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0x1C
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x1C
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x12
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <reserve>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000028
SIZE:  0x13
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <reserve>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200002A
SIZE:  0x13
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <reserve>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200002C
SIZE:  0x30
FIELDS:
  public            System.UInt32                   FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <reserve>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200002E
SIZE:  0x11
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <data>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x1A
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <reserve>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000032
SIZE:  0x1C
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000037
SIZE:  0x44
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Command
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x48
FIELDS:
  public            Plugins.LibScePad.ScePadTriggerEffectModemode  // 0x10
  private           Plugins.LibScePad.ScePadTriggerEffectOffParamIternal.Command.<_>e__FixedBuffer_  // 0x14
METHODS:
END_CLASS

CLASS: <_>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x17
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_1>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200003B
SIZE:  0x14
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_2>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200003C
SIZE:  0x3E
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Command
TYPE:  sealed struct
TOKEN: 0x200003A
SIZE:  0x48
FIELDS:
  public            Plugins.LibScePad.ScePadTriggerEffectModemode  // 0x10
  private           Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal.Command.<_1>e__FixedBuffer_1  // 0x14
  public            System.Byte                     position  // 0x18
  public            System.Byte                     strength  // 0x19
  private           Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal.Command.<_2>e__FixedBuffer_2  // 0x1a
METHODS:
END_CLASS

CLASS: <_>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200003D
SIZE:  0x17
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_1>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000040
SIZE:  0x14
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_2>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000041
SIZE:  0x3D
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Command
TYPE:  sealed struct
TOKEN: 0x200003F
SIZE:  0x48
FIELDS:
  public            Plugins.LibScePad.ScePadTriggerEffectModemode  // 0x10
  private           Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal.Command.<_1>e__FixedBuffer_1  // 0x14
  public            System.Byte                     startPosition  // 0x18
  public            System.Byte                     endPosition  // 0x19
  public            System.Byte                     strength  // 0x1a
  private           Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal.Command.<_2>e__FixedBuffer_2  // 0x1b
METHODS:
END_CLASS

CLASS: <_>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000042
SIZE:  0x17
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_1>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000045
SIZE:  0x14
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_2>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000046
SIZE:  0x3D
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Command
TYPE:  sealed struct
TOKEN: 0x2000044
SIZE:  0x48
FIELDS:
  public            Plugins.LibScePad.ScePadTriggerEffectModemode  // 0x10
  private           Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal.Command.<_1>e__FixedBuffer_1  // 0x14
  public            System.Byte                     position  // 0x18
  public            System.Byte                     amplitude  // 0x19
  public            System.Byte                     frequency  // 0x1a
  private           Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal.Command.<_2>e__FixedBuffer_2  // 0x1b
METHODS:
END_CLASS

CLASS: <_>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000047
SIZE:  0x17
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_1>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200004A
SIZE:  0x14
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_2>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200004B
SIZE:  0x3C
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Command
TYPE:  sealed struct
TOKEN: 0x2000049
SIZE:  0x48
FIELDS:
  public            Plugins.LibScePad.ScePadTriggerEffectModemode  // 0x10
  private           Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal.Command.<_1>e__FixedBuffer_1  // 0x14
  public            System.Byte                     startPosition  // 0x18
  public            System.Byte                     endPosition  // 0x19
  public            System.Byte                     startStrength  // 0x1a
  public            System.Byte                     endStrength  // 0x1b
  private           Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal.Command.<_2>e__FixedBuffer_2  // 0x1c
METHODS:
END_CLASS

CLASS: <_>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200004C
SIZE:  0x17
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_1>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0x14
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <strength>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000050
SIZE:  0x1A
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_2>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000051
SIZE:  0x36
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Command
TYPE:  sealed struct
TOKEN: 0x200004E
SIZE:  0x48
FIELDS:
  public            Plugins.LibScePad.ScePadTriggerEffectModemode  // 0x10
  private           Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal.Command.<_1>e__FixedBuffer_1  // 0x14
  public            Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal.Command.<strength>e__FixedBufferstrength  // 0x18
  private           Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal.Command.<_2>e__FixedBuffer_2  // 0x22
METHODS:
END_CLASS

CLASS: <_>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000052
SIZE:  0x17
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_1>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000055
SIZE:  0x14
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <amplitude>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000056
SIZE:  0x1A
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <_2>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000057
SIZE:  0x35
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Command
TYPE:  sealed struct
TOKEN: 0x2000054
SIZE:  0x48
FIELDS:
  public            Plugins.LibScePad.ScePadTriggerEffectModemode  // 0x10
  private           Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal.Command.<_1>e__FixedBuffer_1  // 0x14
  public            System.Byte                     frequency  // 0x18
  public            Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal.Command.<amplitude>e__FixedBufferamplitude  // 0x19
  private           Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal.Command.<_2>e__FixedBuffer_2  // 0x23
METHODS:
END_CLASS

CLASS: <_>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x2000058
SIZE:  0x17
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: <state>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x18
FIELDS:
  public            System.Int32                    FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.LibScePad
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_available  // static @ 0x0
PROPERTIES:
  available  get=0x09EB4E08
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadType
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Plugins.LibScePad.ScePadType    SCE_PAD_PORT_TYPE_STANDARD  // const
  public    static  Plugins.LibScePad.ScePadType    SCE_PAD_PORT_TYPE_SPECIAL  // const
  public    static  Plugins.LibScePad.ScePadType    SCE_PAD_PORT_TYPE_REMOTE_CONTROL  // const
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadSupportBusType
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Plugins.LibScePad.ScePadSupportBusTypeSCE_PAD_SUPPORT_BUS_TYPE_USB  // const
  public    static  Plugins.LibScePad.ScePadSupportBusTypeSCE_PAD_SUPPORT_BUS_TYPE_USB_BT  // const
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadVibrationMode
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Plugins.LibScePad.ScePadVibrationModeSCE_PAD_VIBRATION_MODE_ADVANCED  // const
  public    static  Plugins.LibScePad.ScePadVibrationModeSCE_PAD_VIBRATION_MODE_COMPATIBLE  // const
  public    static  Plugins.LibScePad.ScePadVibrationModeSCE_PAD_VIBRATION_MODE_COMPATIBLE2  // const
  public    static  Plugins.LibScePad.ScePadVibrationModeSCE_PAD_VIBRATION_MODE_ADVANCED_AND_COMPATIBLE  // const
  public    static  Plugins.LibScePad.ScePadVibrationModeSCE_PAD_VIBRATION_MODE_ADVANCED_AND_COMPATIBLE2  // const
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadAudioOutPath
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Plugins.LibScePad.ScePadAudioOutPathSCE_PAD_AUDIO_OUT_PATH_STEREO_HEADSET  // const
  public    static  Plugins.LibScePad.ScePadAudioOutPathSCE_PAD_AUDIO_OUT_PATH_MONO_HEADSET  // const
  public    static  Plugins.LibScePad.ScePadAudioOutPathSCE_PAD_AUDIO_OUT_PATH_MONO_HEADSET_SPEAKER  // const
  public    static  Plugins.LibScePad.ScePadAudioOutPathSCE_PAD_AUDIO_OUT_PATH_SPEAKER  // const
  public    static  Plugins.LibScePad.ScePadAudioOutPathSCE_PAD_AUDIO_OUT_PATH_OFF  // const
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadHeadsetType
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Plugins.LibScePad.ScePadHeadsetTypeSCE_PAD_HEADSET_TYPE_NONE  // const
  public    static  Plugins.LibScePad.ScePadHeadsetTypeSCE_PAD_HEADSET_TYPE_HEADPHONE  // const
  public    static  Plugins.LibScePad.ScePadHeadsetTypeSCE_PAD_HEADSET_TYPE_HEADSET  // const
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTriggerEffectMode
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Plugins.LibScePad.ScePadTriggerEffectModeSCE_PAD_TRIGGER_EFFECT_MODE_OFF  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectModeSCE_PAD_TRIGGER_EFFECT_MODE_FEEDBACK  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectModeSCE_PAD_TRIGGER_EFFECT_MODE_WEAPON  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectModeSCE_PAD_TRIGGER_EFFECT_MODE_VIBRATION  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectModeSCE_PAD_TRIGGER_EFFECT_MODE_MULTIPLE_POSITION_FEEDBACK  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectModeSCE_PAD_TRIGGER_EFFECT_MODE_SLOPE_FEEDBACK  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectModeSCE_PAD_TRIGGER_EFFECT_MODE_MULTIPLE_POSITION_VIBRATION  // const
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTriggerEffectState
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Plugins.LibScePad.ScePadTriggerEffectStateSCE_PAD_TRIGGER_EFFECT_STATE_OFF  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectStateSCE_PAD_TRIGGER_EFFECT_STATE_FEEDBACK_STANDBY  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectStateSCE_PAD_TRIGGER_EFFECT_STATE_FEEDBACK_ACTIVE  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectStateSCE_PAD_TRIGGER_EFFECT_STATE_WEAPON_STANDBY  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectStateSCE_PAD_TRIGGER_EFFECT_STATE_WEAPON_PULLING  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectStateSCE_PAD_TRIGGER_EFFECT_STATE_WEAPON_FIRED  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectStateSCE_PAD_TRIGGER_EFFECT_STATE_VIBRATION_STANDBY  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectStateSCE_PAD_TRIGGER_EFFECT_STATE_VIBRATION_ACTIVE  // const
  public    static  Plugins.LibScePad.ScePadTriggerEffectStateSCE_PAD_TRIGGER_EFFECT_STATE_INTERCEPTED  // const
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.TriggerMask
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  Plugins.LibScePad.TriggerMask   NONE  // const
  public    static  Plugins.LibScePad.TriggerMask   L2  // const
  public    static  Plugins.LibScePad.TriggerMask   R2  // const
  public    static  Plugins.LibScePad.TriggerMask   ALL  // const
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.TriggerEffectCommandUnion
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x20
FIELDS:
  public            Plugins.LibScePad.TriggerFeedbackEffectfeedback  // 0x10
  public            Plugins.LibScePad.TriggerWeaponEffectweapon  // 0x10
  public            Plugins.LibScePad.TriggerVibrationEffectvibration  // 0x10
  public            Plugins.LibScePad.TriggerSlopeFeedbackEffectslopeFeedback  // 0x10
  public            Plugins.LibScePad.TriggerMultiPositionFeedbackEffectmultiPositionFeedback  // 0x10
  public            Plugins.LibScePad.TriggerMultiPositionVibrationEffectmultiPositionVibration  // 0x10
  public            Plugins.LibScePad.TriggerMask   mask  // 0x1b
  public            Plugins.LibScePad.ScePadTriggerEffectModemode  // 0x1c
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.TriggerFeedbackEffect
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x12
FIELDS:
  public            System.Byte                     position  // 0x10
  public            System.Byte                     strength  // 0x11
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.TriggerWeaponEffect
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x13
FIELDS:
  public            System.Byte                     startPosition  // 0x10
  public            System.Byte                     endPosition  // 0x11
  public            System.Byte                     strength  // 0x12
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.TriggerVibrationEffect
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x13
FIELDS:
  public            System.Byte                     position  // 0x10
  public            System.Byte                     amplitude  // 0x11
  public            System.Byte                     frequency  // 0x12
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.TriggerSlopeFeedbackEffect
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x14
FIELDS:
  public            System.Byte                     startPosition  // 0x10
  public            System.Byte                     endPosition  // 0x11
  public            System.Byte                     startStrength  // 0x12
  public            System.Byte                     endStrength  // 0x13
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.TriggerMultiPositionFeedbackEffect
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x1A
FIELDS:
  public            System.Byte                     strength0  // 0x10
  public            System.Byte                     strength1  // 0x11
  public            System.Byte                     strength2  // 0x12
  public            System.Byte                     strength3  // 0x13
  public            System.Byte                     strength4  // 0x14
  public            System.Byte                     strength5  // 0x15
  public            System.Byte                     strength6  // 0x16
  public            System.Byte                     strength7  // 0x17
  public            System.Byte                     strength8  // 0x18
  public            System.Byte                     strength9  // 0x19
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.TriggerMultiPositionVibrationEffect
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x1B
FIELDS:
  public            System.Byte                     amplitude0  // 0x10
  public            System.Byte                     amplitude1  // 0x11
  public            System.Byte                     amplitude2  // 0x12
  public            System.Byte                     amplitude3  // 0x13
  public            System.Byte                     amplitude4  // 0x14
  public            System.Byte                     amplitude5  // 0x15
  public            System.Byte                     amplitude6  // 0x16
  public            System.Byte                     amplitude7  // 0x17
  public            System.Byte                     amplitude8  // 0x18
  public            System.Byte                     amplitude9  // 0x19
  public            System.Byte                     frequency  // 0x1a
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTouchPadInformation
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x18
FIELDS:
  public            System.Single                   pixelDensity  // 0x10
  public            System.UInt16                   resolutionX  // 0x14
  public            System.UInt16                   resolutionY  // 0x16
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadStickInformation
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x12
FIELDS:
  public            System.Byte                     deadZoneLeft  // 0x10
  public            System.Byte                     deadZoneRight  // 0x11
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadAnalogStick
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x12
FIELDS:
  public            System.Byte                     x  // 0x10
  public            System.Byte                     y  // 0x11
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadAnalogButtons
TYPE:  sealed struct
TOKEN: 0x2000018
SIZE:  0x12
FIELDS:
  public            System.Byte                     l2  // 0x10
  public            System.Byte                     r2  // 0x11
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadData
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x58
FIELDS:
  public            System.UInt32                   buttons  // 0x10
  public            Plugins.LibScePad.ScePadAnalogStickleftStick  // 0x14
  public            Plugins.LibScePad.ScePadAnalogStickrightStick  // 0x16
  public            Plugins.LibScePad.ScePadAnalogButtonsanalogButtons  // 0x18
  public            UnityEngine.Quaternion          orientation  // 0x1c
  public            UnityEngine.Vector3             acceleration  // 0x2c
  public            UnityEngine.Vector3             angularVelocity  // 0x38
  public            System.Boolean                  connected  // 0x44
  public            System.UInt64                   timeStamp  // 0x48
  public            System.Byte                     connectedCount  // 0x50
METHODS:
  RVA=0x04DA2FF0  token=0x6000020  System.Void .ctor(Plugins.LibScePad.ScePadDataInternal data)
END_CLASS

CLASS: Plugins.LibScePad.ScePadControllerInformation
TYPE:  sealed struct
TOKEN: 0x200001A
SIZE:  0x20
FIELDS:
  public            Plugins.LibScePad.ScePadTouchPadInformationtouchPadInfo  // 0x10
  public            Plugins.LibScePad.ScePadStickInformationstickInfo  // 0x18
  public            System.Byte                     connectionType  // 0x1a
  public            System.Byte                     connectionCount  // 0x1b
  public            System.Boolean                  connected  // 0x1c
METHODS:
  RVA=0x04DA2FC0  token=0x6000021  System.Void .ctor(Plugins.LibScePad.ScePadControllerInformationInternal info)
END_CLASS

CLASS: Plugins.LibScePad.LibScePadInvoke
TYPE:  static class
TOKEN: 0x200001B
SIZE:  0x10
FIELDS:
  private   static  System.String                   LibName  // const
METHODS:
  RVA=0x09EB405C  token=0x6000022  System.Int32 IsSupportedAudioFunction(System.Int32 handle)
  RVA=0x09EB45B8  token=0x6000023  System.Int32 SetAudioOutPath(System.Int32 handle, Plugins.LibScePad.ScePadAudioOutPath path)
  RVA=0x09EB4D88  token=0x6000024  System.Int32 SetVolumeGain(System.Int32 handle, Plugins.LibScePad.ScePadVolumeGainInternal& gain)
  RVA=0x09EB3EF4  token=0x6000025  System.Int32 GetJackState(System.Int32 handle, Plugins.LibScePad.ScePadJackStateInternal& headSetType)
  RVA=0x09EB3FF4  token=0x6000026  System.Int32 Init()
  RVA=0x03826D50  token=0x6000027  System.Int32 Init3_WindowsOnly(Plugins.LibScePad.ScePadInit3ParamInternal init3Param)
  RVA=0x09EB3CDC  token=0x6000028  System.Int32 Close(System.Int32 handle)
  RVA=0x09EB40D4  token=0x6000029  System.Int32 Open(System.Int32 userId, Plugins.LibScePad.ScePadType type, System.Int32 index, System.IntPtr param)
  RVA=0x09EB3E68  token=0x600002A  System.Int32 GetHandle(System.Int32 userId, Plugins.LibScePad.ScePadType type, System.Int32 index)
  RVA=0x09EB43C8  token=0x600002B  System.Int32 ResetLightBar(System.Int32 handle)
  RVA=0x09EB4638  token=0x600002C  System.Int32 SetLightBar(System.Int32 handle, Plugins.LibScePad.ScePadColorInternal& color)
  RVA=0x09EB4170  token=0x600002D  System.Int32 ReadState(System.Int32 handle, Plugins.LibScePad.ScePadDataInternal& data)
  RVA=0x09EB4294  token=0x600002E  System.Int32 Read(System.Int32 handle, Plugins.LibScePad.ScePadDataInternal& data, System.Int32 num)
  RVA=0x09EB3D54  token=0x600002F  System.Int32 GetControllerInformation(System.Int32 handle, Plugins.LibScePad.ScePadControllerInformationInternal& info)
  RVA=0x09EB46B8  token=0x6000030  System.Int32 SetMotionSensorState(System.Int32 handle, System.Boolean enable)
  RVA=0x09EB4738  token=0x6000031  System.Int32 SetTiltCorrectionState(System.Int32 handle, System.Boolean enable)
  RVA=0x09EB4538  token=0x6000032  System.Int32 SetAngularVelocityDeadbandState(System.Int32 handle, System.Boolean enable)
  RVA=0x09EB44B8  token=0x6000033  System.Int32 SetAngularVelocityBiasCorrectionState(System.Int32 handle, System.Boolean enable)
  RVA=0x09EB4440  token=0x6000034  System.Int32 ResetOrientation(System.Int32 handle)
  RVA=0x09EB4C88  token=0x6000035  System.Int32 SetVibrationMode(System.Int32 handle, Plugins.LibScePad.ScePadVibrationMode mode)
  RVA=0x09EB4D08  token=0x6000036  System.Int32 SetVibration(System.Int32 handle, Plugins.LibScePad.ScePadVibrationParamInternal& mode)
  RVA=0x09EB3F74  token=0x6000037  System.Int32 GetTriggerEffectState(System.Int32 handle, Plugins.LibScePad.ScePadTriggerEffectStateInformationInternal& info)
  RVA=0x09EB49C8  token=0x6000038  System.Int32 SetTriggerEffectOff(System.Int32 handle, Plugins.LibScePad.ScePadTriggerEffectOffParamIternal& param)
  RVA=0x09EB47B8  token=0x6000039  System.Int32 SetTriggerEffectFeedback(System.Int32 handle, Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal& param)
  RVA=0x09EB4B28  token=0x600003A  System.Int32 SetTriggerEffectVibration(System.Int32 handle, Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal& param)
  RVA=0x09EB4BD8  token=0x600003B  System.Int32 SetTriggerEffectWeapon(System.Int32 handle, Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal& param)
  RVA=0x09EB4A78  token=0x600003C  System.Int32 SetTriggerEffectSlope(System.Int32 handle, Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal& param)
  RVA=0x09EB4918  token=0x600003D  System.Int32 SetTriggerEffectMultiPosition(System.Int32 handle, Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal& param)
  RVA=0x09EB4868  token=0x600003E  System.Int32 SetTriggerEffectMultiPositionVibration(System.Int32 handle, Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal& param)
END_CLASS

CLASS: Plugins.LibScePad.ScePadColorInternal
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x14
FIELDS:
  public            System.Byte                     r  // 0x10
  public            System.Byte                     g  // 0x11
  public            System.Byte                     b  // 0x12
  public            System.Byte                     reserved  // 0x13
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadInit3ParamInternal
TYPE:  sealed struct
TOKEN: 0x200001D
SIZE:  0x38
FIELDS:
  public            System.IntPtr                   allocFunc  // 0x10
  public            System.IntPtr                   freeFunc  // 0x18
  public            Plugins.LibScePad.ScePadSupportBusTypesupportBusType  // 0x20
  private           Plugins.LibScePad.ScePadInit3ParamInternal.<reserve>e__FixedBufferreserve  // 0x24
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadDataInternal
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x88
FIELDS:
  public            System.UInt32                   buttons  // 0x10
  public            Plugins.LibScePad.ScePadAnalogStickleftStick  // 0x14
  public            Plugins.LibScePad.ScePadAnalogStickrightStick  // 0x16
  public            Plugins.LibScePad.ScePadAnalogButtonsInternalanalogButtons  // 0x18
  public            Plugins.LibScePad.SceFQuaternionInternalorientation  // 0x1c
  public            Plugins.LibScePad.SceFVector3Internalacceleration  // 0x2c
  public            Plugins.LibScePad.SceFVector3InternalangularVelocity  // 0x38
  public            Plugins.LibScePad.ScePadTouchDataInternaltouchData  // 0x44
  public            System.Boolean                  connected  // 0x5c
  public            System.UInt64                   timestamp  // 0x60
  public            Plugins.LibScePad.ScePadExtensionUnitDataInternalextennsionnUnitData  // 0x68
  public            System.Byte                     connectedCount  // 0x78
  private           Plugins.LibScePad.ScePadDataInternal.<reserve>e__FixedBufferreserve  // 0x79
  public            System.Byte                     deviceUniqueDataLen  // 0x7b
  public            Plugins.LibScePad.ScePadDataInternal.<deviceUniqueData>e__FixedBufferdeviceUniqueData  // 0x7c
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadControllerInformationInternal
TYPE:  sealed struct
TOKEN: 0x2000022
SIZE:  0x2C
FIELDS:
  public            Plugins.LibScePad.ScePadTouchPadInformationtouchPadInfo  // 0x10
  public            Plugins.LibScePad.ScePadStickInformationstickInfo  // 0x18
  public            System.Byte                     connectionType  // 0x1a
  public            System.Byte                     connectionCount  // 0x1b
  public            System.Boolean                  connected  // 0x1c
  private           Plugins.LibScePad.ScePadControllerInformationInternal.<_>e__FixedBuffer_  // 0x1d
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadVibrationParamInternal
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0x12
FIELDS:
  public            System.Byte                     largeMotor  // 0x10
  public            System.Byte                     smallMotor  // 0x11
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadAnalogButtonsInternal
TYPE:  sealed struct
TOKEN: 0x2000025
SIZE:  0x14
FIELDS:
  public            System.Byte                     l2  // 0x10
  public            System.Byte                     r2  // 0x11
  private           Plugins.LibScePad.ScePadAnalogButtonsInternal.<_>e__FixedBuffer_  // 0x12
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTouchDataInternal
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x28
FIELDS:
  public            System.Byte                     touchNum  // 0x10
  private           Plugins.LibScePad.ScePadTouchDataInternal.<reserve>e__FixedBufferreserve  // 0x11
  private           System.UInt32                   reserve1  // 0x14
  public            Plugins.LibScePad.ScePadTouchInternaltouch1  // 0x18
  public            Plugins.LibScePad.ScePadTouchInternaltouch2  // 0x20
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTouchInternal
TYPE:  sealed struct
TOKEN: 0x2000029
SIZE:  0x18
FIELDS:
  public            System.UInt16                   x  // 0x10
  public            System.UInt16                   y  // 0x12
  public            System.Byte                     id  // 0x14
  private           Plugins.LibScePad.ScePadTouchInternal.<reserve>e__FixedBufferreserve  // 0x15
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadOpenParamInternal
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x30
FIELDS:
  public            Plugins.LibScePad.ScePadOpenParamInternal.<reserve>e__FixedBufferreserve  // 0x10
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadExtensionUnitDataInternal
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x20
FIELDS:
  public            System.UInt32                   extensionUnitId  // 0x10
  private           Plugins.LibScePad.ScePadExtensionUnitDataInternal.<reserve>e__FixedBufferreserve  // 0x14
  public            System.Byte                     dateLength  // 0x15
  public            Plugins.LibScePad.ScePadExtensionUnitDataInternal.<data>e__FixedBufferdata  // 0x16
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadVolumeGainInternal
TYPE:  sealed struct
TOKEN: 0x2000030
SIZE:  0x14
FIELDS:
  public            System.Byte                     speakerVol  // 0x10
  public            System.Byte                     jackVol  // 0x11
  private           System.Byte                     reserved  // 0x12
  public            System.Byte                     micGain  // 0x13
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadJackStateInternal
TYPE:  sealed struct
TOKEN: 0x2000031
SIZE:  0x20
FIELDS:
  public            Plugins.LibScePad.ScePadHeadsetTypeheadsetType  // 0x10
  private           Plugins.LibScePad.ScePadJackStateInternal.<reserve>e__FixedBufferreserve  // 0x14
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.SceFVector3Internal
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x1C
FIELDS:
  public            System.Single                   x  // 0x10
  public            System.Single                   y  // 0x14
  public            System.Single                   z  // 0x18
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.SceFQuaternionInternal
TYPE:  sealed struct
TOKEN: 0x2000034
SIZE:  0x20
FIELDS:
  public            System.Single                   x  // 0x10
  public            System.Single                   y  // 0x14
  public            System.Single                   z  // 0x18
  public            System.Single                   w  // 0x1c
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTriggerEffectOffParamIternal
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x20
FIELDS:
  public            System.Byte                     triggerMask  // 0x10
  private           Plugins.LibScePad.ScePadTriggerEffectOffParamIternal.<_>e__FixedBuffer_  // 0x11
  public            Plugins.LibScePad.ScePadTriggerEffectOffParamIternal.Command[]command  // 0x18
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal
TYPE:  sealed struct
TOKEN: 0x2000039
SIZE:  0x20
FIELDS:
  public            System.Byte                     triggerMask  // 0x10
  private           Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal.<_>e__FixedBuffer_  // 0x11
  public            Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal.Command[]command  // 0x18
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal
TYPE:  sealed struct
TOKEN: 0x200003E
SIZE:  0x20
FIELDS:
  public            System.Byte                     triggerMask  // 0x10
  private           Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal.<_>e__FixedBuffer_  // 0x11
  public            Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal.Command[]command  // 0x18
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal
TYPE:  sealed struct
TOKEN: 0x2000043
SIZE:  0x20
FIELDS:
  public            System.Byte                     triggerMask  // 0x10
  private           Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal.<_>e__FixedBuffer_  // 0x11
  public            Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal.Command[]command  // 0x18
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal
TYPE:  sealed struct
TOKEN: 0x2000048
SIZE:  0x20
FIELDS:
  public            System.Byte                     triggerMask  // 0x10
  public            Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal.<_>e__FixedBuffer_  // 0x11
  public            Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal.Command[]command  // 0x18
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal
TYPE:  sealed struct
TOKEN: 0x200004D
SIZE:  0x20
FIELDS:
  public            System.Byte                     triggerMask  // 0x10
  public            Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal.<_>e__FixedBuffer_  // 0x11
  public            Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal.Command[]command  // 0x18
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal
TYPE:  sealed struct
TOKEN: 0x2000053
SIZE:  0x20
FIELDS:
  public            System.Byte                     triggerMask  // 0x10
  public            Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal.<_>e__FixedBuffer_  // 0x11
  public            Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal.Command[]command  // 0x18
METHODS:
END_CLASS

CLASS: Plugins.LibScePad.ScePadTriggerEffectStateInformationInternal
TYPE:  sealed struct
TOKEN: 0x2000059
SIZE:  0x18
FIELDS:
  public            Plugins.LibScePad.ScePadTriggerEffectStateInformationInternal.<state>e__FixedBufferstate  // 0x10
METHODS:
END_CLASS

