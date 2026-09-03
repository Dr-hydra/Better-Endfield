// ========================================================
// Dumped by @desirepro
// Assembly: LibScePad.Runtime.dll
// Classes:  90
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000003  // size: 0x10
    public static class Audio
    {
        // Methods
        // RVA: 0x09EB3504  token: 0x6000002
        public static System.Int32 IsSupportAudioFunction(System.Int32 handle) { }
        // RVA: 0x09EB3554  token: 0x6000003
        public static System.Int32 SetAudioOutPath(System.Int32 handle, Plugins.LibScePad.ScePadAudioOutPath path) { }
        // RVA: 0x09EB35B0  token: 0x6000004
        public static System.Int32 SetVolumeGain(System.Int32 handle, System.Byte speakerVol, System.Byte jackVol, System.Byte micGain) { }
        // RVA: 0x09EB3494  token: 0x6000005
        public static Plugins.LibScePad.ScePadHeadsetType GetHeadSetType(System.Int32 handle) { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public static class Basic
    {
        // Methods
        // RVA: 0x03329C50  token: 0x6000006
        public static System.Int32 Init() { }
        // RVA: 0x09EB37B0  token: 0x6000007
        public static System.Int32 GetOrOpen(System.Int32 userId, Plugins.LibScePad.ScePadType type) { }
        // RVA: 0x09EB3750  token: 0x6000008
        public static System.Int32 GetHandle(System.Int32 userId, Plugins.LibScePad.ScePadType type) { }
        // RVA: 0x09EB3644  token: 0x6000009
        public static System.Int32 Close(System.Int32 handle) { }
        // RVA: 0x09EB3948  token: 0x600000A
        public static System.Int32 ResetLightBar(System.Int32 handle) { }
        // RVA: 0x09EB3AA0  token: 0x600000B
        public static System.Int32 SetLightBar(System.Int32 handle, UnityEngine.Color color) { }
        // RVA: 0x09EB3B34  token: 0x600000C
        public static System.Int32 SetLightBar(System.Int32 handle, System.Byte r, System.Byte g, System.Byte b) { }
        // RVA: 0x09EB3694  token: 0x600000D
        public static System.Int32 GetControllerInformation(System.Int32 handle, Plugins.LibScePad.ScePadControllerInformation& info) { }
        // RVA: 0x09EB381C  token: 0x600000E
        public static System.Int32 ReadState(System.Int32 handle, Plugins.LibScePad.ScePadData& data) { }
        // RVA: 0x09EB3BC8  token: 0x600000F
        public static System.Int32 SetMotionSensorState(System.Int32 handle, System.Boolean enable) { }
        // RVA: 0x09EB3C24  token: 0x6000010
        public static System.Int32 SetTiltCorrectionState(System.Int32 handle, System.Boolean enable) { }
        // RVA: 0x09EB3A44  token: 0x6000011
        public static System.Int32 SetAngularVelocityDeadbandState(System.Int32 handle, System.Boolean enable) { }
        // RVA: 0x09EB39E8  token: 0x6000012
        public static System.Int32 SetAngularVelocityBiasCorrectionState(System.Int32 handle, System.Boolean enable) { }
        // RVA: 0x09EB3998  token: 0x6000013
        public static System.Int32 ResetOrientation(System.Int32 handle) { }
        // RVA: 0x09EB3C80  token: 0x6000014
        public static System.Int32 SetVibrationMode(System.Int32 handle, Plugins.LibScePad.ScePadVibrationMode mode) { }
        // RVA: 0x03329CA0  token: 0x6000015
        private static System.Int32 _InitWithBluetooth() { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public static class Trigger
    {
        // Fields
        private static Plugins.LibScePad.ScePadTriggerEffectOffParamIternal.Command[] s_cachedOffEffectCmdArray;  // static @ 0x0
        private static Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal.Command[] s_cachedFeedBackEffectCmdArray;  // static @ 0x8
        private static Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal.Command[] s_cachedVibrationEffectCmdArray;  // static @ 0x10
        private static Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal.Command[] s_cachedWeaponEffectCmdArray;  // static @ 0x18
        private static Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal.Command[] s_cachedSlopeEffectCmdArray;  // static @ 0x20
        private static Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal.Command[] s_cachedMulPosEffectCmdArray;  // static @ 0x28
        private static Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal.Command[] s_cachedMulVibEffectCmdArray;  // static @ 0x30

        // Methods
        // RVA: 0x09EB4E40  token: 0x6000016
        public static System.Int32 GetTriggerEffectStates(System.Int32 handle, Plugins.LibScePad.ScePadTriggerEffectState& l2, Plugins.LibScePad.ScePadTriggerEffectState& r2) { }
        // RVA: 0x09EB4EC4  token: 0x6000017
        public static System.Int32 SetTriggerEffectOff(System.Int32 handle, Plugins.LibScePad.TriggerMask mask) { }
        // RVA: 0x09EB4FC0  token: 0x6000018
        public static System.Int32 SetTriggerEffect(System.Int32 handle, Plugins.LibScePad.TriggerEffectCommandUnion command) { }
        // RVA: 0x09EB51EC  token: 0x6000019
        private static System.Int32 _SetTriggerEffectFeedback(System.Int32 handle, Plugins.LibScePad.TriggerMask mask, Plugins.LibScePad.TriggerFeedbackEffect paramL2, Plugins.LibScePad.TriggerFeedbackEffect paramR2) { }
        // RVA: 0x09EB59DC  token: 0x600001A
        private static System.Int32 _SetTriggerEffectVibration(System.Int32 handle, Plugins.LibScePad.TriggerMask mask, Plugins.LibScePad.TriggerVibrationEffect paramL2, Plugins.LibScePad.TriggerVibrationEffect paramR2) { }
        // RVA: 0x09EB5BFC  token: 0x600001B
        private static System.Int32 _SetTriggerEffectWeapon(System.Int32 handle, Plugins.LibScePad.TriggerMask mask, Plugins.LibScePad.TriggerWeaponEffect paramL2, Plugins.LibScePad.TriggerWeaponEffect paramR2) { }
        // RVA: 0x09EB5770  token: 0x600001C
        private static System.Int32 _SetTriggerEffectSlope(System.Int32 handle, Plugins.LibScePad.TriggerMask mask, Plugins.LibScePad.TriggerSlopeFeedbackEffect slopeL2, Plugins.LibScePad.TriggerSlopeFeedbackEffect slopeR2) { }
        // RVA: 0x09EB55B8  token: 0x600001D
        private static System.Int32 _SetTriggerEffectMultiPosition(System.Int32 handle, Plugins.LibScePad.TriggerMask mask, Plugins.LibScePad.TriggerMultiPositionFeedbackEffect paramL2, Plugins.LibScePad.TriggerMultiPositionFeedbackEffect paramR2) { }
        // RVA: 0x09EB53B0  token: 0x600001E
        private static System.Int32 _SetTriggerEffectMultiPositionVibration(System.Int32 handle, Plugins.LibScePad.TriggerMask mask, Plugins.LibScePad.TriggerMultiPositionVibrationEffect paramL2, Plugins.LibScePad.TriggerMultiPositionVibrationEffect paramR2) { }
        // RVA: 0x09EB5E1C  token: 0x600001F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001E  // size: 0x20
    public sealed struct <reserve>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000020  // size: 0x12
    public sealed struct <reserve>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000021  // size: 0x1C
    public sealed struct <deviceUniqueData>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000023  // size: 0x1C
    public sealed struct <_>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000026  // size: 0x12
    public sealed struct <_>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000028  // size: 0x13
    public sealed struct <reserve>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x200002A  // size: 0x13
    public sealed struct <reserve>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x200002C  // size: 0x30
    public sealed struct <reserve>e__FixedBuffer
    {
        // Fields
        public System.UInt32 FixedElementField;  // 0x10

    }

    // TypeToken: 0x200002E  // size: 0x11
    public sealed struct <reserve>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x200002F  // size: 0x1A
    public sealed struct <data>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000032  // size: 0x1C
    public sealed struct <reserve>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000037  // size: 0x44
    public sealed struct <_>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000036  // size: 0x48
    public sealed struct Command
    {
        // Fields
        public Plugins.LibScePad.ScePadTriggerEffectMode mode;  // 0x10
        private Plugins.LibScePad.ScePadTriggerEffectOffParamIternal.Command.<_>e__FixedBuffer _;  // 0x14

    }

    // TypeToken: 0x2000038  // size: 0x17
    public sealed struct <_>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x200003B  // size: 0x14
    public sealed struct <_1>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x200003C  // size: 0x3E
    public sealed struct <_2>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x200003A  // size: 0x48
    public sealed struct Command
    {
        // Fields
        public Plugins.LibScePad.ScePadTriggerEffectMode mode;  // 0x10
        private Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal.Command.<_1>e__FixedBuffer _1;  // 0x14
        public System.Byte position;  // 0x18
        public System.Byte strength;  // 0x19
        private Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal.Command.<_2>e__FixedBuffer _2;  // 0x1a

    }

    // TypeToken: 0x200003D  // size: 0x17
    public sealed struct <_>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000040  // size: 0x14
    public sealed struct <_1>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000041  // size: 0x3D
    public sealed struct <_2>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x200003F  // size: 0x48
    public sealed struct Command
    {
        // Fields
        public Plugins.LibScePad.ScePadTriggerEffectMode mode;  // 0x10
        private Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal.Command.<_1>e__FixedBuffer _1;  // 0x14
        public System.Byte startPosition;  // 0x18
        public System.Byte endPosition;  // 0x19
        public System.Byte strength;  // 0x1a
        private Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal.Command.<_2>e__FixedBuffer _2;  // 0x1b

    }

    // TypeToken: 0x2000042  // size: 0x17
    public sealed struct <_>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000045  // size: 0x14
    public sealed struct <_1>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000046  // size: 0x3D
    public sealed struct <_2>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000044  // size: 0x48
    public sealed struct Command
    {
        // Fields
        public Plugins.LibScePad.ScePadTriggerEffectMode mode;  // 0x10
        private Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal.Command.<_1>e__FixedBuffer _1;  // 0x14
        public System.Byte position;  // 0x18
        public System.Byte amplitude;  // 0x19
        public System.Byte frequency;  // 0x1a
        private Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal.Command.<_2>e__FixedBuffer _2;  // 0x1b

    }

    // TypeToken: 0x2000047  // size: 0x17
    public sealed struct <_>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x200004A  // size: 0x14
    public sealed struct <_1>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x200004B  // size: 0x3C
    public sealed struct <_2>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000049  // size: 0x48
    public sealed struct Command
    {
        // Fields
        public Plugins.LibScePad.ScePadTriggerEffectMode mode;  // 0x10
        private Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal.Command.<_1>e__FixedBuffer _1;  // 0x14
        public System.Byte startPosition;  // 0x18
        public System.Byte endPosition;  // 0x19
        public System.Byte startStrength;  // 0x1a
        public System.Byte endStrength;  // 0x1b
        private Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal.Command.<_2>e__FixedBuffer _2;  // 0x1c

    }

    // TypeToken: 0x200004C  // size: 0x17
    public sealed struct <_>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x200004F  // size: 0x14
    public sealed struct <_1>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000050  // size: 0x1A
    public sealed struct <strength>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000051  // size: 0x36
    public sealed struct <_2>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x200004E  // size: 0x48
    public sealed struct Command
    {
        // Fields
        public Plugins.LibScePad.ScePadTriggerEffectMode mode;  // 0x10
        private Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal.Command.<_1>e__FixedBuffer _1;  // 0x14
        public Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal.Command.<strength>e__FixedBuffer strength;  // 0x18
        private Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal.Command.<_2>e__FixedBuffer _2;  // 0x22

    }

    // TypeToken: 0x2000052  // size: 0x17
    public sealed struct <_>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000055  // size: 0x14
    public sealed struct <_1>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000056  // size: 0x1A
    public sealed struct <amplitude>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000057  // size: 0x35
    public sealed struct <_2>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x2000054  // size: 0x48
    public sealed struct Command
    {
        // Fields
        public Plugins.LibScePad.ScePadTriggerEffectMode mode;  // 0x10
        private Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal.Command.<_1>e__FixedBuffer _1;  // 0x14
        public System.Byte frequency;  // 0x18
        public Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal.Command.<amplitude>e__FixedBuffer amplitude;  // 0x19
        private Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal.Command.<_2>e__FixedBuffer _2;  // 0x23

    }

    // TypeToken: 0x2000058  // size: 0x17
    public sealed struct <_>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x200005A  // size: 0x18
    public sealed struct <state>e__FixedBuffer
    {
        // Fields
        public System.Int32 FixedElementField;  // 0x10

    }

namespace Plugins.LibScePad
{

    // TypeToken: 0x2000002  // size: 0x10
    public static class LibScePad
    {
        // Fields
        private static System.Boolean s_available;  // static @ 0x0

        // Properties
        System.Boolean available { get; /* RVA: 0x09EB4E08 */ }

    }

    // TypeToken: 0x2000006  // size: 0x14
    public sealed struct ScePadType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Plugins.LibScePad.ScePadType SCE_PAD_PORT_TYPE_STANDARD;  // const
        public static Plugins.LibScePad.ScePadType SCE_PAD_PORT_TYPE_SPECIAL;  // const
        public static Plugins.LibScePad.ScePadType SCE_PAD_PORT_TYPE_REMOTE_CONTROL;  // const

    }

    // TypeToken: 0x2000007  // size: 0x14
    public sealed struct ScePadSupportBusType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Plugins.LibScePad.ScePadSupportBusType SCE_PAD_SUPPORT_BUS_TYPE_USB;  // const
        public static Plugins.LibScePad.ScePadSupportBusType SCE_PAD_SUPPORT_BUS_TYPE_USB_BT;  // const

    }

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct ScePadVibrationMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Plugins.LibScePad.ScePadVibrationMode SCE_PAD_VIBRATION_MODE_ADVANCED;  // const
        public static Plugins.LibScePad.ScePadVibrationMode SCE_PAD_VIBRATION_MODE_COMPATIBLE;  // const
        public static Plugins.LibScePad.ScePadVibrationMode SCE_PAD_VIBRATION_MODE_COMPATIBLE2;  // const
        public static Plugins.LibScePad.ScePadVibrationMode SCE_PAD_VIBRATION_MODE_ADVANCED_AND_COMPATIBLE;  // const
        public static Plugins.LibScePad.ScePadVibrationMode SCE_PAD_VIBRATION_MODE_ADVANCED_AND_COMPATIBLE2;  // const

    }

    // TypeToken: 0x2000009  // size: 0x14
    public sealed struct ScePadAudioOutPath
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Plugins.LibScePad.ScePadAudioOutPath SCE_PAD_AUDIO_OUT_PATH_STEREO_HEADSET;  // const
        public static Plugins.LibScePad.ScePadAudioOutPath SCE_PAD_AUDIO_OUT_PATH_MONO_HEADSET;  // const
        public static Plugins.LibScePad.ScePadAudioOutPath SCE_PAD_AUDIO_OUT_PATH_MONO_HEADSET_SPEAKER;  // const
        public static Plugins.LibScePad.ScePadAudioOutPath SCE_PAD_AUDIO_OUT_PATH_SPEAKER;  // const
        public static Plugins.LibScePad.ScePadAudioOutPath SCE_PAD_AUDIO_OUT_PATH_OFF;  // const

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct ScePadHeadsetType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Plugins.LibScePad.ScePadHeadsetType SCE_PAD_HEADSET_TYPE_NONE;  // const
        public static Plugins.LibScePad.ScePadHeadsetType SCE_PAD_HEADSET_TYPE_HEADPHONE;  // const
        public static Plugins.LibScePad.ScePadHeadsetType SCE_PAD_HEADSET_TYPE_HEADSET;  // const

    }

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct ScePadTriggerEffectMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Plugins.LibScePad.ScePadTriggerEffectMode SCE_PAD_TRIGGER_EFFECT_MODE_OFF;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectMode SCE_PAD_TRIGGER_EFFECT_MODE_FEEDBACK;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectMode SCE_PAD_TRIGGER_EFFECT_MODE_WEAPON;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectMode SCE_PAD_TRIGGER_EFFECT_MODE_VIBRATION;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectMode SCE_PAD_TRIGGER_EFFECT_MODE_MULTIPLE_POSITION_FEEDBACK;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectMode SCE_PAD_TRIGGER_EFFECT_MODE_SLOPE_FEEDBACK;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectMode SCE_PAD_TRIGGER_EFFECT_MODE_MULTIPLE_POSITION_VIBRATION;  // const

    }

    // TypeToken: 0x200000C  // size: 0x14
    public sealed struct ScePadTriggerEffectState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Plugins.LibScePad.ScePadTriggerEffectState SCE_PAD_TRIGGER_EFFECT_STATE_OFF;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectState SCE_PAD_TRIGGER_EFFECT_STATE_FEEDBACK_STANDBY;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectState SCE_PAD_TRIGGER_EFFECT_STATE_FEEDBACK_ACTIVE;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectState SCE_PAD_TRIGGER_EFFECT_STATE_WEAPON_STANDBY;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectState SCE_PAD_TRIGGER_EFFECT_STATE_WEAPON_PULLING;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectState SCE_PAD_TRIGGER_EFFECT_STATE_WEAPON_FIRED;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectState SCE_PAD_TRIGGER_EFFECT_STATE_VIBRATION_STANDBY;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectState SCE_PAD_TRIGGER_EFFECT_STATE_VIBRATION_ACTIVE;  // const
        public static Plugins.LibScePad.ScePadTriggerEffectState SCE_PAD_TRIGGER_EFFECT_STATE_INTERCEPTED;  // const

    }

    // TypeToken: 0x200000D  // size: 0x11
    public sealed struct TriggerMask
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Plugins.LibScePad.TriggerMask NONE;  // const
        public static Plugins.LibScePad.TriggerMask L2;  // const
        public static Plugins.LibScePad.TriggerMask R2;  // const
        public static Plugins.LibScePad.TriggerMask ALL;  // const

    }

    // TypeToken: 0x200000E  // size: 0x20
    public sealed struct TriggerEffectCommandUnion
    {
        // Fields
        public Plugins.LibScePad.TriggerFeedbackEffect feedback;  // 0x10
        public Plugins.LibScePad.TriggerWeaponEffect weapon;  // 0x10
        public Plugins.LibScePad.TriggerVibrationEffect vibration;  // 0x10
        public Plugins.LibScePad.TriggerSlopeFeedbackEffect slopeFeedback;  // 0x10
        public Plugins.LibScePad.TriggerMultiPositionFeedbackEffect multiPositionFeedback;  // 0x10
        public Plugins.LibScePad.TriggerMultiPositionVibrationEffect multiPositionVibration;  // 0x10
        public Plugins.LibScePad.TriggerMask mask;  // 0x1b
        public Plugins.LibScePad.ScePadTriggerEffectMode mode;  // 0x1c

    }

    // TypeToken: 0x200000F  // size: 0x12
    public sealed struct TriggerFeedbackEffect
    {
        // Fields
        public System.Byte position;  // 0x10
        public System.Byte strength;  // 0x11

    }

    // TypeToken: 0x2000010  // size: 0x13
    public sealed struct TriggerWeaponEffect
    {
        // Fields
        public System.Byte startPosition;  // 0x10
        public System.Byte endPosition;  // 0x11
        public System.Byte strength;  // 0x12

    }

    // TypeToken: 0x2000011  // size: 0x13
    public sealed struct TriggerVibrationEffect
    {
        // Fields
        public System.Byte position;  // 0x10
        public System.Byte amplitude;  // 0x11
        public System.Byte frequency;  // 0x12

    }

    // TypeToken: 0x2000012  // size: 0x14
    public sealed struct TriggerSlopeFeedbackEffect
    {
        // Fields
        public System.Byte startPosition;  // 0x10
        public System.Byte endPosition;  // 0x11
        public System.Byte startStrength;  // 0x12
        public System.Byte endStrength;  // 0x13

    }

    // TypeToken: 0x2000013  // size: 0x1A
    public sealed struct TriggerMultiPositionFeedbackEffect
    {
        // Fields
        public System.Byte strength0;  // 0x10
        public System.Byte strength1;  // 0x11
        public System.Byte strength2;  // 0x12
        public System.Byte strength3;  // 0x13
        public System.Byte strength4;  // 0x14
        public System.Byte strength5;  // 0x15
        public System.Byte strength6;  // 0x16
        public System.Byte strength7;  // 0x17
        public System.Byte strength8;  // 0x18
        public System.Byte strength9;  // 0x19

    }

    // TypeToken: 0x2000014  // size: 0x1B
    public sealed struct TriggerMultiPositionVibrationEffect
    {
        // Fields
        public System.Byte amplitude0;  // 0x10
        public System.Byte amplitude1;  // 0x11
        public System.Byte amplitude2;  // 0x12
        public System.Byte amplitude3;  // 0x13
        public System.Byte amplitude4;  // 0x14
        public System.Byte amplitude5;  // 0x15
        public System.Byte amplitude6;  // 0x16
        public System.Byte amplitude7;  // 0x17
        public System.Byte amplitude8;  // 0x18
        public System.Byte amplitude9;  // 0x19
        public System.Byte frequency;  // 0x1a

    }

    // TypeToken: 0x2000015  // size: 0x18
    public sealed struct ScePadTouchPadInformation
    {
        // Fields
        public System.Single pixelDensity;  // 0x10
        public System.UInt16 resolutionX;  // 0x14
        public System.UInt16 resolutionY;  // 0x16

    }

    // TypeToken: 0x2000016  // size: 0x12
    public sealed struct ScePadStickInformation
    {
        // Fields
        public System.Byte deadZoneLeft;  // 0x10
        public System.Byte deadZoneRight;  // 0x11

    }

    // TypeToken: 0x2000017  // size: 0x12
    public sealed struct ScePadAnalogStick
    {
        // Fields
        public System.Byte x;  // 0x10
        public System.Byte y;  // 0x11

    }

    // TypeToken: 0x2000018  // size: 0x12
    public sealed struct ScePadAnalogButtons
    {
        // Fields
        public System.Byte l2;  // 0x10
        public System.Byte r2;  // 0x11

    }

    // TypeToken: 0x2000019  // size: 0x58
    public sealed struct ScePadData
    {
        // Fields
        public System.UInt32 buttons;  // 0x10
        public Plugins.LibScePad.ScePadAnalogStick leftStick;  // 0x14
        public Plugins.LibScePad.ScePadAnalogStick rightStick;  // 0x16
        public Plugins.LibScePad.ScePadAnalogButtons analogButtons;  // 0x18
        public UnityEngine.Quaternion orientation;  // 0x1c
        public UnityEngine.Vector3 acceleration;  // 0x2c
        public UnityEngine.Vector3 angularVelocity;  // 0x38
        public System.Boolean connected;  // 0x44
        public System.UInt64 timeStamp;  // 0x48
        public System.Byte connectedCount;  // 0x50

        // Methods
        // RVA: 0x04DA2FF0  token: 0x6000020
        private System.Void .ctor(Plugins.LibScePad.ScePadDataInternal data) { }

    }

    // TypeToken: 0x200001A  // size: 0x20
    public sealed struct ScePadControllerInformation
    {
        // Fields
        public Plugins.LibScePad.ScePadTouchPadInformation touchPadInfo;  // 0x10
        public Plugins.LibScePad.ScePadStickInformation stickInfo;  // 0x18
        public System.Byte connectionType;  // 0x1a
        public System.Byte connectionCount;  // 0x1b
        public System.Boolean connected;  // 0x1c

        // Methods
        // RVA: 0x04DA2FC0  token: 0x6000021
        private System.Void .ctor(Plugins.LibScePad.ScePadControllerInformationInternal info) { }

    }

    // TypeToken: 0x200001B  // size: 0x10
    public static class LibScePadInvoke
    {
        // Fields
        private static System.String LibName;  // const

        // Methods
        // RVA: 0x09EB405C  token: 0x6000022
        private static System.Int32 IsSupportedAudioFunction(System.Int32 handle) { }
        // RVA: 0x09EB45B8  token: 0x6000023
        private static System.Int32 SetAudioOutPath(System.Int32 handle, Plugins.LibScePad.ScePadAudioOutPath path) { }
        // RVA: 0x09EB4D88  token: 0x6000024
        private static System.Int32 SetVolumeGain(System.Int32 handle, Plugins.LibScePad.ScePadVolumeGainInternal& gain) { }
        // RVA: 0x09EB3EF4  token: 0x6000025
        private static System.Int32 GetJackState(System.Int32 handle, Plugins.LibScePad.ScePadJackStateInternal& headSetType) { }
        // RVA: 0x09EB3FF4  token: 0x6000026
        private static System.Int32 Init() { }
        // RVA: 0x03826D50  token: 0x6000027
        private static System.Int32 Init3_WindowsOnly(Plugins.LibScePad.ScePadInit3ParamInternal init3Param) { }
        // RVA: 0x09EB3CDC  token: 0x6000028
        private static System.Int32 Close(System.Int32 handle) { }
        // RVA: 0x09EB40D4  token: 0x6000029
        private static System.Int32 Open(System.Int32 userId, Plugins.LibScePad.ScePadType type, System.Int32 index, System.IntPtr param) { }
        // RVA: 0x09EB3E68  token: 0x600002A
        private static System.Int32 GetHandle(System.Int32 userId, Plugins.LibScePad.ScePadType type, System.Int32 index) { }
        // RVA: 0x09EB43C8  token: 0x600002B
        private static System.Int32 ResetLightBar(System.Int32 handle) { }
        // RVA: 0x09EB4638  token: 0x600002C
        private static System.Int32 SetLightBar(System.Int32 handle, Plugins.LibScePad.ScePadColorInternal& color) { }
        // RVA: 0x09EB4170  token: 0x600002D
        private static System.Int32 ReadState(System.Int32 handle, Plugins.LibScePad.ScePadDataInternal& data) { }
        // RVA: 0x09EB4294  token: 0x600002E
        private static System.Int32 Read(System.Int32 handle, Plugins.LibScePad.ScePadDataInternal& data, System.Int32 num) { }
        // RVA: 0x09EB3D54  token: 0x600002F
        private static System.Int32 GetControllerInformation(System.Int32 handle, Plugins.LibScePad.ScePadControllerInformationInternal& info) { }
        // RVA: 0x09EB46B8  token: 0x6000030
        private static System.Int32 SetMotionSensorState(System.Int32 handle, System.Boolean enable) { }
        // RVA: 0x09EB4738  token: 0x6000031
        private static System.Int32 SetTiltCorrectionState(System.Int32 handle, System.Boolean enable) { }
        // RVA: 0x09EB4538  token: 0x6000032
        private static System.Int32 SetAngularVelocityDeadbandState(System.Int32 handle, System.Boolean enable) { }
        // RVA: 0x09EB44B8  token: 0x6000033
        private static System.Int32 SetAngularVelocityBiasCorrectionState(System.Int32 handle, System.Boolean enable) { }
        // RVA: 0x09EB4440  token: 0x6000034
        private static System.Int32 ResetOrientation(System.Int32 handle) { }
        // RVA: 0x09EB4C88  token: 0x6000035
        private static System.Int32 SetVibrationMode(System.Int32 handle, Plugins.LibScePad.ScePadVibrationMode mode) { }
        // RVA: 0x09EB4D08  token: 0x6000036
        private static System.Int32 SetVibration(System.Int32 handle, Plugins.LibScePad.ScePadVibrationParamInternal& mode) { }
        // RVA: 0x09EB3F74  token: 0x6000037
        private static System.Int32 GetTriggerEffectState(System.Int32 handle, Plugins.LibScePad.ScePadTriggerEffectStateInformationInternal& info) { }
        // RVA: 0x09EB49C8  token: 0x6000038
        private static System.Int32 SetTriggerEffectOff(System.Int32 handle, Plugins.LibScePad.ScePadTriggerEffectOffParamIternal& param) { }
        // RVA: 0x09EB47B8  token: 0x6000039
        private static System.Int32 SetTriggerEffectFeedback(System.Int32 handle, Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal& param) { }
        // RVA: 0x09EB4B28  token: 0x600003A
        private static System.Int32 SetTriggerEffectVibration(System.Int32 handle, Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal& param) { }
        // RVA: 0x09EB4BD8  token: 0x600003B
        private static System.Int32 SetTriggerEffectWeapon(System.Int32 handle, Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal& param) { }
        // RVA: 0x09EB4A78  token: 0x600003C
        private static System.Int32 SetTriggerEffectSlope(System.Int32 handle, Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal& param) { }
        // RVA: 0x09EB4918  token: 0x600003D
        private static System.Int32 SetTriggerEffectMultiPosition(System.Int32 handle, Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal& param) { }
        // RVA: 0x09EB4868  token: 0x600003E
        private static System.Int32 SetTriggerEffectMultiPositionVibration(System.Int32 handle, Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal& param) { }

    }

    // TypeToken: 0x200001C  // size: 0x14
    public sealed struct ScePadColorInternal
    {
        // Fields
        public System.Byte r;  // 0x10
        public System.Byte g;  // 0x11
        public System.Byte b;  // 0x12
        public System.Byte reserved;  // 0x13

    }

    // TypeToken: 0x200001D  // size: 0x38
    public sealed struct ScePadInit3ParamInternal
    {
        // Fields
        public System.IntPtr allocFunc;  // 0x10
        public System.IntPtr freeFunc;  // 0x18
        public Plugins.LibScePad.ScePadSupportBusType supportBusType;  // 0x20
        private Plugins.LibScePad.ScePadInit3ParamInternal.<reserve>e__FixedBuffer reserve;  // 0x24

    }

    // TypeToken: 0x200001F  // size: 0x88
    public sealed struct ScePadDataInternal
    {
        // Fields
        public System.UInt32 buttons;  // 0x10
        public Plugins.LibScePad.ScePadAnalogStick leftStick;  // 0x14
        public Plugins.LibScePad.ScePadAnalogStick rightStick;  // 0x16
        public Plugins.LibScePad.ScePadAnalogButtonsInternal analogButtons;  // 0x18
        public Plugins.LibScePad.SceFQuaternionInternal orientation;  // 0x1c
        public Plugins.LibScePad.SceFVector3Internal acceleration;  // 0x2c
        public Plugins.LibScePad.SceFVector3Internal angularVelocity;  // 0x38
        public Plugins.LibScePad.ScePadTouchDataInternal touchData;  // 0x44
        public System.Boolean connected;  // 0x5c
        public System.UInt64 timestamp;  // 0x60
        public Plugins.LibScePad.ScePadExtensionUnitDataInternal extennsionnUnitData;  // 0x68
        public System.Byte connectedCount;  // 0x78
        private Plugins.LibScePad.ScePadDataInternal.<reserve>e__FixedBuffer reserve;  // 0x79
        public System.Byte deviceUniqueDataLen;  // 0x7b
        public Plugins.LibScePad.ScePadDataInternal.<deviceUniqueData>e__FixedBuffer deviceUniqueData;  // 0x7c

    }

    // TypeToken: 0x2000022  // size: 0x2C
    public sealed struct ScePadControllerInformationInternal
    {
        // Fields
        public Plugins.LibScePad.ScePadTouchPadInformation touchPadInfo;  // 0x10
        public Plugins.LibScePad.ScePadStickInformation stickInfo;  // 0x18
        public System.Byte connectionType;  // 0x1a
        public System.Byte connectionCount;  // 0x1b
        public System.Boolean connected;  // 0x1c
        private Plugins.LibScePad.ScePadControllerInformationInternal.<_>e__FixedBuffer _;  // 0x1d

    }

    // TypeToken: 0x2000024  // size: 0x12
    public sealed struct ScePadVibrationParamInternal
    {
        // Fields
        public System.Byte largeMotor;  // 0x10
        public System.Byte smallMotor;  // 0x11

    }

    // TypeToken: 0x2000025  // size: 0x14
    public sealed struct ScePadAnalogButtonsInternal
    {
        // Fields
        public System.Byte l2;  // 0x10
        public System.Byte r2;  // 0x11
        private Plugins.LibScePad.ScePadAnalogButtonsInternal.<_>e__FixedBuffer _;  // 0x12

    }

    // TypeToken: 0x2000027  // size: 0x28
    public sealed struct ScePadTouchDataInternal
    {
        // Fields
        public System.Byte touchNum;  // 0x10
        private Plugins.LibScePad.ScePadTouchDataInternal.<reserve>e__FixedBuffer reserve;  // 0x11
        private System.UInt32 reserve1;  // 0x14
        public Plugins.LibScePad.ScePadTouchInternal touch1;  // 0x18
        public Plugins.LibScePad.ScePadTouchInternal touch2;  // 0x20

    }

    // TypeToken: 0x2000029  // size: 0x18
    public sealed struct ScePadTouchInternal
    {
        // Fields
        public System.UInt16 x;  // 0x10
        public System.UInt16 y;  // 0x12
        public System.Byte id;  // 0x14
        private Plugins.LibScePad.ScePadTouchInternal.<reserve>e__FixedBuffer reserve;  // 0x15

    }

    // TypeToken: 0x200002B  // size: 0x30
    public sealed struct ScePadOpenParamInternal
    {
        // Fields
        public Plugins.LibScePad.ScePadOpenParamInternal.<reserve>e__FixedBuffer reserve;  // 0x10

    }

    // TypeToken: 0x200002D  // size: 0x20
    public sealed struct ScePadExtensionUnitDataInternal
    {
        // Fields
        public System.UInt32 extensionUnitId;  // 0x10
        private Plugins.LibScePad.ScePadExtensionUnitDataInternal.<reserve>e__FixedBuffer reserve;  // 0x14
        public System.Byte dateLength;  // 0x15
        public Plugins.LibScePad.ScePadExtensionUnitDataInternal.<data>e__FixedBuffer data;  // 0x16

    }

    // TypeToken: 0x2000030  // size: 0x14
    public sealed struct ScePadVolumeGainInternal
    {
        // Fields
        public System.Byte speakerVol;  // 0x10
        public System.Byte jackVol;  // 0x11
        private System.Byte reserved;  // 0x12
        public System.Byte micGain;  // 0x13

    }

    // TypeToken: 0x2000031  // size: 0x20
    public sealed struct ScePadJackStateInternal
    {
        // Fields
        public Plugins.LibScePad.ScePadHeadsetType headsetType;  // 0x10
        private Plugins.LibScePad.ScePadJackStateInternal.<reserve>e__FixedBuffer reserve;  // 0x14

    }

    // TypeToken: 0x2000033  // size: 0x1C
    public sealed struct SceFVector3Internal
    {
        // Fields
        public System.Single x;  // 0x10
        public System.Single y;  // 0x14
        public System.Single z;  // 0x18

    }

    // TypeToken: 0x2000034  // size: 0x20
    public sealed struct SceFQuaternionInternal
    {
        // Fields
        public System.Single x;  // 0x10
        public System.Single y;  // 0x14
        public System.Single z;  // 0x18
        public System.Single w;  // 0x1c

    }

    // TypeToken: 0x2000035  // size: 0x20
    public sealed struct ScePadTriggerEffectOffParamIternal
    {
        // Fields
        public System.Byte triggerMask;  // 0x10
        private Plugins.LibScePad.ScePadTriggerEffectOffParamIternal.<_>e__FixedBuffer _;  // 0x11
        public Plugins.LibScePad.ScePadTriggerEffectOffParamIternal.Command[] command;  // 0x18

    }

    // TypeToken: 0x2000039  // size: 0x20
    public sealed struct ScePadTriggerFeedbackEffectParamInternal
    {
        // Fields
        public System.Byte triggerMask;  // 0x10
        private Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal.<_>e__FixedBuffer _;  // 0x11
        public Plugins.LibScePad.ScePadTriggerFeedbackEffectParamInternal.Command[] command;  // 0x18

    }

    // TypeToken: 0x200003E  // size: 0x20
    public sealed struct ScePadTriggerWeaponEffectParamInternal
    {
        // Fields
        public System.Byte triggerMask;  // 0x10
        private Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal.<_>e__FixedBuffer _;  // 0x11
        public Plugins.LibScePad.ScePadTriggerWeaponEffectParamInternal.Command[] command;  // 0x18

    }

    // TypeToken: 0x2000043  // size: 0x20
    public sealed struct ScePadTriggerVibrationEffectParamInternal
    {
        // Fields
        public System.Byte triggerMask;  // 0x10
        private Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal.<_>e__FixedBuffer _;  // 0x11
        public Plugins.LibScePad.ScePadTriggerVibrationEffectParamInternal.Command[] command;  // 0x18

    }

    // TypeToken: 0x2000048  // size: 0x20
    public sealed struct ScePadTriggerSlopeFeedbackEffectParamInternal
    {
        // Fields
        public System.Byte triggerMask;  // 0x10
        public Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal.<_>e__FixedBuffer _;  // 0x11
        public Plugins.LibScePad.ScePadTriggerSlopeFeedbackEffectParamInternal.Command[] command;  // 0x18

    }

    // TypeToken: 0x200004D  // size: 0x20
    public sealed struct ScePadTriggerMultiPositionFeedbackEffectParamInternal
    {
        // Fields
        public System.Byte triggerMask;  // 0x10
        public Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal.<_>e__FixedBuffer _;  // 0x11
        public Plugins.LibScePad.ScePadTriggerMultiPositionFeedbackEffectParamInternal.Command[] command;  // 0x18

    }

    // TypeToken: 0x2000053  // size: 0x20
    public sealed struct ScePadTriggerMultiPositionVibrationEffectParamInternal
    {
        // Fields
        public System.Byte triggerMask;  // 0x10
        public Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal.<_>e__FixedBuffer _;  // 0x11
        public Plugins.LibScePad.ScePadTriggerMultiPositionVibrationEffectParamInternal.Command[] command;  // 0x18

    }

    // TypeToken: 0x2000059  // size: 0x18
    public sealed struct ScePadTriggerEffectStateInformationInternal
    {
        // Fields
        public Plugins.LibScePad.ScePadTriggerEffectStateInformationInternal.<state>e__FixedBuffer state;  // 0x10

    }

}

