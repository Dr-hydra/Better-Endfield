// ========================================================
// Dumped by @desirepro
// Assembly: CriMw.CriWare.Runtime.dll
// Classes:  330
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000009  // size: 0x12
    public sealed struct AudioPortType
    {
        // Fields
        public System.UInt16 value__;  // 0x10
        public static CriWare.CriWarePS5.AudioPortType Main;  // const
        public static CriWare.CriWarePS5.AudioPortType Bgm;  // const
        public static CriWare.CriWarePS5.AudioPortType Voice;  // const
        public static CriWare.CriWarePS5.AudioPortType PadSpeaker;  // const
        public static CriWare.CriWarePS5.AudioPortType Personal;  // const
        public static CriWare.CriWarePS5.AudioPortType Aux;  // const
        public static CriWare.CriWarePS5.AudioPortType Vibration;  // const
        public static CriWare.CriWarePS5.AudioPortType ObjectMain;  // const
        public static CriWare.CriWarePS5.AudioPortType ObjectVoice;  // const
        public static CriWare.CriWarePS5.AudioPortType ObjecPersonal;  // const

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct AttributeId
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriWarePS5.AttributeId Pcm;  // const
        public static CriWare.CriWarePS5.AttributeId Gain;  // const
        public static CriWare.CriWarePS5.AttributeId Priority;  // const
        public static CriWare.CriWarePS5.AttributeId Position;  // const
        public static CriWare.CriWarePS5.AttributeId Spread;  // const
        public static CriWare.CriWarePS5.AttributeId Passthrough;  // const
        public static CriWare.CriWarePS5.AttributeId ResetState;  // const
        public static CriWare.CriWarePS5.AttributeId ApplicationSpecific;  // const
        public static CriWare.CriWarePS5.AttributeId Ambisonics;  // const
        public static CriWare.CriWarePS5.AttributeId Restricted;  // const
        public static CriWare.CriWarePS5.AttributeId MixToMainGain;  // const
        public static CriWare.CriWarePS5.AttributeId DebugName;  // const
        public static CriWare.CriWarePS5.AttributeId MAX;  // const

    }

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct PortFlag
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static CriWare.CriWarePS5.PortFlag Restricted;  // const
        public static CriWare.CriWarePS5.PortFlag Passthrough;  // const

    }

    // TypeToken: 0x200000C  // size: 0x10
    public static class SafeNativeMethods
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000009
        public static System.Void criFsUnity_Setup_PS5(System.Boolean enable_apr_direct_mode) { }

    }

    // TypeToken: 0x200000E  // size: 0x80
    public sealed class CallbackFromNativeDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068D29C8  token: 0x6000047
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014E2060  token: 0x6000048
        public virtual System.UInt64 Invoke(System.IntPtr ptr1) { }
        // RVA: 0x068D2950  token: 0x6000049
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr ptr1, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DEC64  token: 0x600004A
        public virtual System.UInt64 EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000011  // size: 0x28
    public sealed class <>c__DisplayClass58_0
    {
        // Fields
        public CriWare.CriAtom <>4__this;  // 0x10
        public System.Int32 i;  // 0x18
        public System.Predicate<CriWare.CriAtomCueSheet> <>9__0;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x600008A
        public System.Void .ctor() { }
        // RVA: 0x068E984C  token: 0x600008B
        private System.Boolean <MargeCueSheet>b__0(CriWare.CriAtomCueSheet sheet) { }

    }

    // TypeToken: 0x2000012  // size: 0x58
    public sealed class <LoadAcbFileCoroutine>d__62 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public CriWare.CriAtomCueSheet cueSheet;  // 0x20
        public CriWare.CriFsBinder binder;  // 0x28
        public System.String acbPath;  // 0x30
        public System.String awbPath;  // 0x38
        public CriWare.CriAtom <>4__this;  // 0x40
        public System.Boolean loadAwbOnMemory;  // 0x48
        private CriWare.CriAtomExAcbLoader <asyncLoader>5__2;  // 0x50

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600008C
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x068E9648  token: 0x600008D
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x068E903C  token: 0x600008E
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x068E96C0  token: 0x600008F
        private System.Void <>m__Finally1() { }
        // RVA: 0x068E95FC  token: 0x6000091
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000013  // size: 0x58
    public sealed class <LoadAcbDataCoroutine>d__64 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public CriWare.CriAtomCueSheet cueSheet;  // 0x20
        public System.String awbPath;  // 0x28
        public CriWare.CriFsBinder awbBinder;  // 0x30
        public CriWare.CriAtom <>4__this;  // 0x38
        public System.Byte[] acbData;  // 0x40
        public System.Boolean loadAwbOnMemory;  // 0x48
        private CriWare.CriAtomExAcbLoader <asyncLoader>5__2;  // 0x50

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000093
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x068E8F7C  token: 0x6000094
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x068E8A78  token: 0x6000095
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x068E8FF4  token: 0x6000096
        private System.Void <>m__Finally1() { }
        // RVA: 0x068E8F30  token: 0x6000098
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000016  // size: 0x14
    public sealed struct OutputDeviceType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomOutputDeviceObserver.OutputDeviceType BuiltinSpeaker;  // const
        public static CriWare.CriAtomOutputDeviceObserver.OutputDeviceType WiredDevice;  // const
        public static CriWare.CriAtomOutputDeviceObserver.OutputDeviceType WirelessDevice;  // const

    }

    // TypeToken: 0x2000017  // size: 0x80
    public sealed class DeviceConnectionChangeCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068E7D64  token: 0x60000B8
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x60000B9
        public virtual System.Void Invoke(System.Boolean isConnected, CriWare.CriAtomOutputDeviceObserver.OutputDeviceType deviceType) { }
        // RVA: 0x068E7CD0  token: 0x60000BA
        public virtual System.IAsyncResult BeginInvoke(System.Boolean isConnected, CriWare.CriAtomOutputDeviceObserver.OutputDeviceType deviceType, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x60000BB
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000018  // size: 0x10
    public static class UnsafeNativeMethods
    {
    }

    // TypeToken: 0x200001C  // size: 0x38
    public sealed class <PlayAsync>d__11 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public CriWare.CriAtomSource <>4__this;  // 0x20
        public System.String cueName;  // 0x28
        private CriWare.CriAtomExAcb <acb>5__2;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000DD
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000DE
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x068E9708  token: 0x60000DF
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x068E9800  token: 0x60000E1
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200001E  // size: 0x14
    public sealed struct Status
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomSourceBase.Status Stop;  // const
        public static CriWare.CriAtomSourceBase.Status Prep;  // const
        public static CriWare.CriAtomSourceBase.Status Playing;  // const
        public static CriWare.CriAtomSourceBase.Status PlayEnd;  // const
        public static CriWare.CriAtomSourceBase.Status Error;  // const

    }

    // TypeToken: 0x2000020  // size: 0x80
    public sealed class SetControlIdMethod : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068E89B0  token: 0x6000142
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000143
        public virtual System.Void Invoke(System.UInt16 id) { }
        // RVA: 0x068E8938  token: 0x6000144
        public virtual System.IAsyncResult BeginInvoke(System.UInt16 id, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000145
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000023  // size: 0x14
    public sealed struct CharacterEncoding
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomEx.CharacterEncoding Utf8;  // const
        public static CriWare.CriAtomEx.CharacterEncoding Sjis;  // const

    }

    // TypeToken: 0x2000024  // size: 0x14
    public sealed struct SoundRendererType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomEx.SoundRendererType Default;  // const
        public static CriWare.CriAtomEx.SoundRendererType Native;  // const
        public static CriWare.CriAtomEx.SoundRendererType Asr;  // const
        public static CriWare.CriAtomEx.SoundRendererType Hw1;  // const
        public static CriWare.CriAtomEx.SoundRendererType Hw2;  // const
        public static CriWare.CriAtomEx.SoundRendererType Haptic;  // const

    }

    // TypeToken: 0x2000025  // size: 0x14
    public sealed struct VoiceAllocationMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomEx.VoiceAllocationMethod Once;  // const
        public static CriWare.CriAtomEx.VoiceAllocationMethod Retry;  // const

    }

    // TypeToken: 0x2000026  // size: 0x14
    public sealed struct BiquadFilterType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomEx.BiquadFilterType Off;  // const
        public static CriWare.CriAtomEx.BiquadFilterType LowPass;  // const
        public static CriWare.CriAtomEx.BiquadFilterType HighPass;  // const
        public static CriWare.CriAtomEx.BiquadFilterType Notch;  // const
        public static CriWare.CriAtomEx.BiquadFilterType LowShelf;  // const
        public static CriWare.CriAtomEx.BiquadFilterType HighShelf;  // const
        public static CriWare.CriAtomEx.BiquadFilterType Peaking;  // const

    }

    // TypeToken: 0x2000027  // size: 0x14
    public sealed struct ResumeMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomEx.ResumeMode AllPlayback;  // const
        public static CriWare.CriAtomEx.ResumeMode PausedPlayback;  // const
        public static CriWare.CriAtomEx.ResumeMode PreparedPlayback;  // const

    }

    // TypeToken: 0x2000028  // size: 0x14
    public sealed struct PanType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomEx.PanType Unknown;  // const
        public static CriWare.CriAtomEx.PanType Pan3d;  // const
        public static CriWare.CriAtomEx.PanType Pos3d;  // const
        public static CriWare.CriAtomEx.PanType Auto;  // const

    }

    // TypeToken: 0x2000029  // size: 0x14
    public sealed struct VoiceControlMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomEx.VoiceControlMethod PreferLast;  // const
        public static CriWare.CriAtomEx.VoiceControlMethod PreferFirst;  // const

    }

    // TypeToken: 0x200002A  // size: 0x14
    public sealed struct Parameter
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomEx.Parameter Volume;  // const
        public static CriWare.CriAtomEx.Parameter Pitch;  // const
        public static CriWare.CriAtomEx.Parameter Pan3dAngle;  // const
        public static CriWare.CriAtomEx.Parameter Pan3dDistance;  // const
        public static CriWare.CriAtomEx.Parameter Pan3dVolume;  // const
        public static CriWare.CriAtomEx.Parameter BusSendLevel0;  // const
        public static CriWare.CriAtomEx.Parameter BusSendLevel1;  // const
        public static CriWare.CriAtomEx.Parameter BusSendLevel2;  // const
        public static CriWare.CriAtomEx.Parameter BusSendLevel3;  // const
        public static CriWare.CriAtomEx.Parameter BusSendLevel4;  // const
        public static CriWare.CriAtomEx.Parameter BusSendLevel5;  // const
        public static CriWare.CriAtomEx.Parameter BusSendLevel6;  // const
        public static CriWare.CriAtomEx.Parameter BusSendLevel7;  // const
        public static CriWare.CriAtomEx.Parameter BandPassFilterCofLow;  // const
        public static CriWare.CriAtomEx.Parameter BandPassFilterCofHigh;  // const
        public static CriWare.CriAtomEx.Parameter BiquadFilterType;  // const
        public static CriWare.CriAtomEx.Parameter BiquadFilterFreq;  // const
        public static CriWare.CriAtomEx.Parameter BiquadFIlterQ;  // const
        public static CriWare.CriAtomEx.Parameter BiquadFilterGain;  // const
        public static CriWare.CriAtomEx.Parameter EnvelopeAttackTime;  // const
        public static CriWare.CriAtomEx.Parameter EnvelopeHoldTime;  // const
        public static CriWare.CriAtomEx.Parameter EnvelopeDecayTime;  // const
        public static CriWare.CriAtomEx.Parameter EnvelopeReleaseTime;  // const
        public static CriWare.CriAtomEx.Parameter EnvelopeSustainLevel;  // const
        public static CriWare.CriAtomEx.Parameter StartTime;  // const
        public static CriWare.CriAtomEx.Parameter Priority;  // const

    }

    // TypeToken: 0x200002B  // size: 0x14
    public sealed struct Speaker
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomEx.Speaker FrontLeft;  // const
        public static CriWare.CriAtomEx.Speaker FrontRight;  // const
        public static CriWare.CriAtomEx.Speaker FrontCenter;  // const
        public static CriWare.CriAtomEx.Speaker LowFrequency;  // const
        public static CriWare.CriAtomEx.Speaker SurroundLeft;  // const
        public static CriWare.CriAtomEx.Speaker SurroundRight;  // const
        public static CriWare.CriAtomEx.Speaker SurroundBackLeft;  // const
        public static CriWare.CriAtomEx.Speaker SurroundBackRight;  // const

    }

    // TypeToken: 0x200002C  // size: 0x14
    public sealed struct Format
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static CriWare.CriAtomEx.Format ADX;  // const
        public static CriWare.CriAtomEx.Format HCA;  // const
        public static CriWare.CriAtomEx.Format HCA_MX;  // const
        public static CriWare.CriAtomEx.Format WAVE;  // const
        public static CriWare.CriAtomEx.Format RAW_PCM;  // const
        public static CriWare.CriAtomEx.Format AUDIO_BUFFER;  // const

    }

    // TypeToken: 0x200002D  // size: 0x14
    public sealed struct SpeakerSystem
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static CriWare.CriAtomEx.SpeakerSystem Surround_5_1;  // const
        public static CriWare.CriAtomEx.SpeakerSystem Surround_7_1;  // const

    }

    // TypeToken: 0x200002E  // size: 0x28
    public sealed struct SpeakerAngles6ch
    {
        // Fields
        public System.Single frontLeft;  // 0x10
        public System.Single frontRight;  // 0x14
        public System.Single frontCenter;  // 0x18
        public System.Single lowFrequency;  // 0x1c
        public System.Single surroundLeft;  // 0x20
        public System.Single surroundRight;  // 0x24

        // Methods
        // RVA: 0x04D88F60  token: 0x6000194
        public static CriWare.CriAtomEx.SpeakerAngles6ch Default() { }

    }

    // TypeToken: 0x200002F  // size: 0x30
    public sealed struct SpeakerAngles8ch
    {
        // Fields
        public System.Single frontLeft;  // 0x10
        public System.Single frontRight;  // 0x14
        public System.Single frontCenter;  // 0x18
        public System.Single lowFrequency;  // 0x1c
        public System.Single surroundLeft;  // 0x20
        public System.Single surroundRight;  // 0x24
        public System.Single surroundBackLeft;  // 0x28
        public System.Single surroundBackRight;  // 0x2c

        // Methods
        // RVA: 0x04D88F90  token: 0x6000195
        public static CriWare.CriAtomEx.SpeakerAngles8ch Default() { }

    }

    // TypeToken: 0x2000030  // size: 0x38
    public sealed struct FormatInfo
    {
        // Fields
        public CriWare.CriAtomEx.Format format;  // 0x10
        public System.Int32 samplingRate;  // 0x14
        public System.Int64 numSamples;  // 0x18
        public System.Int64 loopOffset;  // 0x20
        public System.Int64 loopLength;  // 0x28
        public System.Int32 numChannels;  // 0x30
        public System.UInt32 reserved;  // 0x34

    }

    // TypeToken: 0x2000031  // size: 0x20
    public sealed struct AisacControlInfo
    {
        // Fields
        public readonly System.String name;  // 0x10
        public System.UInt32 id;  // 0x18

        // Methods
        // RVA: 0x068D249C  token: 0x6000196
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x2000032  // size: 0x14
    public sealed struct Randomize3dCalcType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomEx.Randomize3dCalcType None;  // const
        public static CriWare.CriAtomEx.Randomize3dCalcType Rectangle;  // const
        public static CriWare.CriAtomEx.Randomize3dCalcType Cuboid;  // const
        public static CriWare.CriAtomEx.Randomize3dCalcType Circle;  // const
        public static CriWare.CriAtomEx.Randomize3dCalcType Cylinder;  // const
        public static CriWare.CriAtomEx.Randomize3dCalcType Sphere;  // const
        public static CriWare.CriAtomEx.Randomize3dCalcType List;  // const

    }

    // TypeToken: 0x2000033  // size: 0x14
    public sealed struct Randomize3dParamType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomEx.Randomize3dParamType None;  // const
        public static CriWare.CriAtomEx.Randomize3dParamType Width;  // const
        public static CriWare.CriAtomEx.Randomize3dParamType Depth;  // const
        public static CriWare.CriAtomEx.Randomize3dParamType Height;  // const
        public static CriWare.CriAtomEx.Randomize3dParamType Radius;  // const

    }

    // TypeToken: 0x2000034  // size: 0x20
    public sealed struct Randomize3dConfig
    {
        // Fields
        public static System.Int32 NumOfCalcParams;  // const
        private System.Boolean followsOriginalSource;  // 0x10
        private CriWare.CriAtomEx.Randomize3dCalcType calculationType;  // 0x14
        private System.Single[] calculationParameters;  // 0x18

        // Properties
        System.Boolean FollowsOriginalSource { get; /* RVA: 0x04D883E0 */ }
        CriWare.CriAtomEx.Randomize3dCalcType CalculationType { get; /* RVA: 0x04D88B20 */ }
        System.Single CalculationParameter1 { get; /* RVA: 0x068E8720 */ }
        System.Single CalculationParameter2 { get; /* RVA: 0x068E874C */ }
        System.Single CalculationParameter3 { get; /* RVA: 0x068E8778 */ }

        // Methods
        // RVA: 0x068E85BC  token: 0x600019C
        private System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }
        // RVA: 0x068E84F0  token: 0x600019D
        public System.Void .ctor(System.Boolean followsOriginalSource, CriWare.CriAtomEx.Randomize3dCalcType calculationType, System.Single param1, System.Single param2, System.Single param3) { }
        // RVA: 0x068E86C0  token: 0x600019E
        public System.Void .ctor(System.Int32 dummy) { }
        // RVA: 0x068E8114  token: 0x600019F
        public System.Void ClearCalcParams(System.Single initVal) { }
        // RVA: 0x068E814C  token: 0x60001A0
        public System.Boolean GetParamByType(CriWare.CriAtomEx.Randomize3dParamType paramType, System.Single& paramVal) { }
        // RVA: 0x068E8318  token: 0x60001A1
        public System.Boolean SetParamByType(CriWare.CriAtomEx.Randomize3dParamType paramType, System.Single paramVal) { }

    }

    // TypeToken: 0x2000035  // size: 0x58
    public sealed struct CuePos3dInfo
    {
        // Fields
        public System.Single coneInsideAngle;  // 0x10
        public System.Single coneOutsideAngle;  // 0x14
        public System.Single minAttenuationDistance;  // 0x18
        public System.Single maxAttenuationDistance;  // 0x1c
        public System.Single sourceRadius;  // 0x20
        public System.Single interiorDistance;  // 0x24
        public System.Single dopplerFactor;  // 0x28
        public CriWare.CriAtomEx.Randomize3dConfig randomPos;  // 0x30
        public System.UInt32 distanceAisacControl;  // 0x40
        public System.UInt32 listenerBaseAngleAisacControl;  // 0x44
        public System.UInt32 sourceBaseAngleAisacControl;  // 0x48
        public System.UInt32 listenerBaseElevationAisacControl;  // 0x4c
        public System.UInt32 sourceBaseElevationAisacControl;  // 0x50

        // Methods
        // RVA: 0x068E7B78  token: 0x60001A2
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x2000036  // size: 0x20
    public sealed struct GameVariableInfo
    {
        // Fields
        public readonly System.String name;  // 0x10
        public System.UInt32 id;  // 0x18
        public System.Single gameValue;  // 0x1c

        // Methods
        // RVA: 0x068E7EAC  token: 0x60001A3
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }
        // RVA: 0x068E7F68  token: 0x60001A4
        public System.Void .ctor(System.String name, System.UInt32 id, System.Single gameValue) { }

    }

    // TypeToken: 0x2000037  // size: 0x14
    public sealed struct CueType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomEx.CueType Polyphonic;  // const
        public static CriWare.CriAtomEx.CueType Sequential;  // const
        public static CriWare.CriAtomEx.CueType Shuffle;  // const
        public static CriWare.CriAtomEx.CueType Random;  // const
        public static CriWare.CriAtomEx.CueType RandomNoRepeat;  // const
        public static CriWare.CriAtomEx.CueType SwitchGameVariable;  // const
        public static CriWare.CriAtomEx.CueType ComboSequential;  // const
        public static CriWare.CriAtomEx.CueType SwitchSelector;  // const
        public static CriWare.CriAtomEx.CueType TrackTransitionBySelector;  // const

    }

    // TypeToken: 0x2000038  // size: 0xA0
    public sealed struct CueInfo
    {
        // Fields
        public System.Int32 id;  // 0x10
        public CriWare.CriAtomEx.CueType type;  // 0x14
        public readonly System.String name;  // 0x18
        public readonly System.String userData;  // 0x20
        public System.Int64 length;  // 0x28
        public System.UInt16[] categories;  // 0x30
        public System.Int16 numLimits;  // 0x38
        public System.UInt16 numBlocks;  // 0x3a
        public System.UInt16 numTracks;  // 0x3c
        public System.UInt16 numRelatedWaveForms;  // 0x3e
        public System.Byte priority;  // 0x40
        public System.Byte headerVisibility;  // 0x41
        public System.Byte ignore_player_parameter;  // 0x42
        public System.Byte probability;  // 0x43
        public CriWare.CriAtomEx.PanType panType;  // 0x44
        public CriWare.CriAtomEx.CuePos3dInfo pos3dInfo;  // 0x48
        public CriWare.CriAtomEx.GameVariableInfo gameVariableInfo;  // 0x90

        // Methods
        // RVA: 0x068E7820  token: 0x60001A5
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x2000039  // size: 0x38
    public sealed struct WaveformInfo
    {
        // Fields
        public System.Int32 waveId;  // 0x10
        public System.UInt32 format;  // 0x14
        public System.Int32 samplingRate;  // 0x18
        public System.Int32 numChannels;  // 0x1c
        public System.Int64 numSamples;  // 0x20
        public System.Boolean streamingFlag;  // 0x28
        public System.UInt32[] reserved;  // 0x30

        // Methods
        // RVA: 0x068EA220  token: 0x60001A6
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x200003A  // size: 0x30
    public sealed struct AisacInfo
    {
        // Fields
        public readonly System.String name;  // 0x10
        public System.Boolean defaultControlFlag;  // 0x18
        public System.Single defaultControlValue;  // 0x1c
        public System.UInt32 controlId;  // 0x20
        public readonly System.String controlName;  // 0x28

        // Methods
        // RVA: 0x068D2638  token: 0x60001A7
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x200003B  // size: 0x2C
    public sealed struct PerformanceInfo
    {
        // Fields
        public System.UInt32 serverProcessCount;  // 0x10
        public System.UInt32 lastServerTime;  // 0x14
        public System.UInt32 maxServerTime;  // 0x18
        public System.UInt32 averageServerTime;  // 0x1c
        public System.UInt32 lastServerInterval;  // 0x20
        public System.UInt32 maxServerInterval;  // 0x24
        public System.UInt32 averageServerInterval;  // 0x28

    }

    // TypeToken: 0x200003C  // size: 0x18
    public sealed struct ResourceUsage
    {
        // Fields
        public System.UInt32 useCount;  // 0x10
        public System.UInt32 limit;  // 0x14

    }

    // TypeToken: 0x200003D  // size: 0x1C
    public sealed struct NativeVector
    {
        // Fields
        public System.Single x;  // 0x10
        public System.Single y;  // 0x14
        public System.Single z;  // 0x18

        // Methods
        // RVA: 0x04D88F30  token: 0x60001A8
        public System.Void .ctor(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x04D88F40  token: 0x60001A9
        public System.Void .ctor(UnityEngine.Vector3 vector) { }

    }

    // TypeToken: 0x200003E  // size: 0x28
    public sealed struct CueLinkInfo
    {
        // Fields
        public System.IntPtr nativePlayerHn;  // 0x10
        public System.UInt32 basePlaybackId;  // 0x18
        public System.UInt32 targetPlaybackId;  // 0x1c
        public System.Int32 cueLinkType;  // 0x20

    }

    // TypeToken: 0x200003F  // size: 0x80
    public sealed class CueLinkCbFunc : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068D2BF8  token: 0x60001AA
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x60001AB
        public virtual System.Void Invoke(CriWare.CriAtomEx.CueLinkInfo& info) { }
        // RVA: 0x068E7AF8  token: 0x60001AC
        public virtual System.IAsyncResult BeginInvoke(CriWare.CriAtomEx.CueLinkInfo& info, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05FD0344  token: 0x60001AD
        public virtual System.Void EndInvoke(CriWare.CriAtomEx.CueLinkInfo& info, System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000041  // size: 0x14
    public sealed struct ReactType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExCategory.ReactType Ducker;  // const
        public static CriWare.CriAtomExCategory.ReactType AisacModulationTrigger;  // const

    }

    // TypeToken: 0x2000042  // size: 0x14
    public sealed struct ReactDuckerTargetType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExCategory.ReactDuckerTargetType Volume;  // const
        public static CriWare.CriAtomExCategory.ReactDuckerTargetType AisacControlValue;  // const

    }

    // TypeToken: 0x2000043  // size: 0x14
    public sealed struct ReactDuckerCurveType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExCategory.ReactDuckerCurveType Linear;  // const
        public static CriWare.CriAtomExCategory.ReactDuckerCurveType Square;  // const
        public static CriWare.CriAtomExCategory.ReactDuckerCurveType SquareReverse;  // const
        public static CriWare.CriAtomExCategory.ReactDuckerCurveType SCurve;  // const
        public static CriWare.CriAtomExCategory.ReactDuckerCurveType FlatAtHalf;  // const

    }

    // TypeToken: 0x2000044  // size: 0x1C
    public sealed struct ReactFadeParameter
    {
        // Fields
        public CriWare.CriAtomExCategory.ReactDuckerCurveType curveType;  // 0x10
        public System.Single curveStrength;  // 0x14
        public System.UInt16 fadeTimeMs;  // 0x18

    }

    // TypeToken: 0x2000045  // size: 0x14
    public sealed struct ReactHoldType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExCategory.ReactHoldType WhilePlaying;  // const
        public static CriWare.CriAtomExCategory.ReactHoldType FixedTime;  // const

    }

    // TypeToken: 0x2000047  // size: 0x14
    public sealed struct Volume
    {
        // Fields
        public System.Single level;  // 0x10

    }

    // TypeToken: 0x2000048  // size: 0x18
    public sealed struct AisacControl
    {
        // Fields
        public System.UInt32 id;  // 0x10
        public System.Single value;  // 0x14

    }

    // TypeToken: 0x2000049  // size: 0x18
    public sealed struct Target
    {
        // Fields
        public CriWare.CriAtomExCategory.ReactDuckerParameter.Volume volume;  // 0x10
        public CriWare.CriAtomExCategory.ReactDuckerParameter.AisacControl aisacControl;  // 0x10

    }

    // TypeToken: 0x2000046  // size: 0x3C
    public sealed struct ReactDuckerParameter
    {
        // Fields
        public CriWare.CriAtomExCategory.ReactDuckerParameter.Target target;  // 0x10
        public CriWare.CriAtomExCategory.ReactDuckerTargetType targetType;  // 0x18
        public CriWare.CriAtomExCategory.ReactFadeParameter entry;  // 0x1c
        public CriWare.CriAtomExCategory.ReactFadeParameter exit;  // 0x28
        public CriWare.CriAtomExCategory.ReactHoldType holdType;  // 0x34
        public System.UInt16 holdTimeMs;  // 0x38

    }

    // TypeToken: 0x200004A  // size: 0x20
    public sealed struct ReactAisacModulationParameter
    {
        // Fields
        private System.Int32 _enableDecrementAisacModulationKey;  // 0x10
        public System.UInt32 decrementAisacModulationKey;  // 0x14
        private System.Int32 _enableIncrementAisacModulationKey;  // 0x18
        public System.UInt32 incrementAisacModulationKey;  // 0x1c

        // Properties
        System.Boolean enableDecrementAisacModulationKey { get; /* RVA: 0x068E87A4 */ }
        System.Boolean enableIncrementAisacModulationKey { get; /* RVA: 0x068E87AC */ }

    }

    // TypeToken: 0x200004C  // size: 0x3C
    public sealed struct Parameter
    {
        // Fields
        public CriWare.CriAtomExCategory.ReactDuckerParameter ducker;  // 0x10
        public CriWare.CriAtomExCategory.ReactAisacModulationParameter aisacModulation;  // 0x10

    }

    // TypeToken: 0x200004B  // size: 0x44
    public sealed struct ReactParameter
    {
        // Fields
        public CriWare.CriAtomExCategory.ReactParameter.Parameter parameter;  // 0x10
        public CriWare.CriAtomExCategory.ReactType type;  // 0x3c
        public System.Boolean enablePausingCue;  // 0x40

    }

    // TypeToken: 0x200004D  // size: 0x14
    public sealed struct ReactStatus
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExCategory.ReactStatus Stop;  // const
        public static CriWare.CriAtomExCategory.ReactStatus FadeOut;  // const
        public static CriWare.CriAtomExCategory.ReactStatus Hold;  // const
        public static CriWare.CriAtomExCategory.ReactStatus FadeIn;  // const
        public static CriWare.CriAtomExCategory.ReactStatus Error;  // const

    }

    // TypeToken: 0x200004F  // size: 0x38
    public sealed struct CriAtomExSequenceEventInfo
    {
        // Fields
        public System.UInt64 position;  // 0x10
        public System.IntPtr playerHn;  // 0x18
        public readonly System.String tag;  // 0x20
        public System.UInt32 playbackId;  // 0x28
        private System.Int32 type;  // 0x2c
        public System.UInt32 id;  // 0x30
        private System.UInt32 reserved;  // 0x34

    }

    // TypeToken: 0x2000050  // size: 0x80
    public sealed class EventCbFunc : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x60001E5
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x60001E6
        public virtual System.Void Invoke(System.String eventParamsString) { }
        // RVA: 0x047FDD70  token: 0x60001E7
        public virtual System.IAsyncResult BeginInvoke(System.String eventParamsString, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x60001E8
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000051  // size: 0x80
    public sealed class EventCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068D2BF8  token: 0x60001E9
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x60001EA
        public virtual System.Void Invoke(CriWare.CriAtomExSequencer.CriAtomExSequenceEventInfo& criAtomExSequenceInfo) { }
        // RVA: 0x068E7E2C  token: 0x60001EB
        public virtual System.IAsyncResult BeginInvoke(CriWare.CriAtomExSequencer.CriAtomExSequenceEventInfo& criAtomExSequenceInfo, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05FD0344  token: 0x60001EC
        public virtual System.Void EndInvoke(CriWare.CriAtomExSequencer.CriAtomExSequenceEventInfo& criAtomExSequenceInfo, System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000053  // size: 0x38
    public sealed struct Info
    {
        // Fields
        public System.IntPtr playerHn;  // 0x10
        public System.UInt32 playbackId;  // 0x18
        public System.UInt32 barCount;  // 0x1c
        public System.UInt32 beatCount;  // 0x20
        public System.Single beatProgress;  // 0x24
        public System.Single bpm;  // 0x28
        public System.Int32 offset;  // 0x2c
        public System.UInt32 numBeats;  // 0x30

    }

    // TypeToken: 0x2000054  // size: 0x80
    public sealed class CbFunc : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068D2BF8  token: 0x60001F0
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x60001F1
        public virtual System.Void Invoke(CriWare.CriAtomExBeatSync.Info& info) { }
        // RVA: 0x068D2B78  token: 0x60001F2
        public virtual System.IAsyncResult BeginInvoke(CriWare.CriAtomExBeatSync.Info& info, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05FD0344  token: 0x60001F3
        public virtual System.Void EndInvoke(CriWare.CriAtomExBeatSync.Info& info, System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000056  // size: 0x18
    public sealed struct BusAnalyzerConfig
    {
        // Fields
        public System.Int32 interval;  // 0x10
        public System.Int32 peakHoldTime;  // 0x14

    }

    // TypeToken: 0x2000057  // size: 0x30
    public sealed struct BusAnalyzerInfo
    {
        // Fields
        public System.Int32 numChannels;  // 0x10
        public System.Single[] rmsLevels;  // 0x18
        public System.Single[] peakLevels;  // 0x20
        public System.Single[] peakHoldLevels;  // 0x28

        // Methods
        // RVA: 0x068D272C  token: 0x600021D
        public System.Void .ctor(System.Byte[] data) { }

    }

    // TypeToken: 0x2000059  // size: 0x14
    public sealed struct Status
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExLatencyEstimator.Status Stop;  // const
        public static CriWare.CriAtomExLatencyEstimator.Status Processing;  // const
        public static CriWare.CriAtomExLatencyEstimator.Status Done;  // const
        public static CriWare.CriAtomExLatencyEstimator.Status Error;  // const

    }

    // TypeToken: 0x200005A  // size: 0x18
    public sealed struct EstimatorInfo
    {
        // Fields
        public CriWare.CriAtomExLatencyEstimator.Status status;  // 0x10
        public System.UInt32 estimated_latency;  // 0x14

    }

    // TypeToken: 0x200005C  // size: 0x14
    public sealed struct Config
    {
        // Fields
        public System.Int32 reserved;  // 0x10

    }

    // TypeToken: 0x200005E  // size: 0x18
    public sealed struct Config
    {
        // Fields
        public System.Boolean enableVoicePriorityDecay;  // 0x10
        public System.UInt32 randomPositionListMaxLength;  // 0x14

        // Methods
        // RVA: 0x04D88DA0  token: 0x6000274
        public System.Void .ctor(System.Boolean enableVoicePriorityDecay, System.UInt32 randomPositionListMaxLength) { }

    }

    // TypeToken: 0x2000060  // size: 0x14
    public sealed struct Config
    {
        // Fields
        public System.Int32 reserved;  // 0x10

    }

    // TypeToken: 0x2000061  // size: 0x10
    public static class UnsafeNativeMethods
    {
        // Methods
        // RVA: 0x068E99C0  token: 0x600028D
        private static System.IntPtr criAtomEx3dTransceiver_Create(CriWare.CriAtomEx3dTransceiver.Config& config, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068E9A54  token: 0x600028E
        private static System.Void criAtomEx3dTransceiver_Destroy(System.IntPtr ex_3d_transceiver) { }
        // RVA: 0x068EA1EC  token: 0x600028F
        private static System.Void criAtomEx3dTransceiver_Update(System.IntPtr ex_3d_transceiver) { }
        // RVA: 0x068E9CC8  token: 0x6000290
        private static System.Void criAtomEx3dTransceiver_SetInputPosition(System.IntPtr ex_3d_transceiver, CriWare.CriAtomEx.NativeVector& position) { }
        // RVA: 0x068EA01C  token: 0x6000291
        private static System.Void criAtomEx3dTransceiver_SetOutputPosition(System.IntPtr ex_3d_transceiver, CriWare.CriAtomEx.NativeVector& position) { }
        // RVA: 0x068E9C34  token: 0x6000292
        private static System.Void criAtomEx3dTransceiver_SetInputOrientation(System.IntPtr ex_3d_transceiver, CriWare.CriAtomEx.NativeVector& front, CriWare.CriAtomEx.NativeVector& top) { }
        // RVA: 0x068E9F88  token: 0x6000293
        private static System.Void criAtomEx3dTransceiver_SetOutputOrientation(System.IntPtr ex_3d_transceiver, CriWare.CriAtomEx.NativeVector& front, CriWare.CriAtomEx.NativeVector& top) { }
        // RVA: 0x068E9E90  token: 0x6000294
        private static System.Void criAtomEx3dTransceiver_SetOutputConeParameter(System.IntPtr ex_3d_transceiver, System.Single inside_angle, System.Single outside_angle, System.Single outside_volume) { }
        // RVA: 0x068E9F3C  token: 0x6000295
        private static System.Void criAtomEx3dTransceiver_SetOutputMinMaxAttenuationDistance(System.IntPtr ex_3d_transceiver, System.Single min_attenuation_distance, System.Single max_attenuation_distance) { }
        // RVA: 0x068E9EF0  token: 0x6000296
        private static System.Void criAtomEx3dTransceiver_SetOutputInteriorPanField(System.IntPtr ex_3d_transceiver, System.Single transceiver_radius, System.Single interior_distance) { }
        // RVA: 0x068E9BE8  token: 0x6000297
        private static System.Void criAtomEx3dTransceiver_SetInputCrossFadeField(System.IntPtr ex_3d_transceiver, System.Single direct_audio_radius, System.Single crossfade_distance) { }
        // RVA: 0x068EA0A0  token: 0x6000298
        private static System.Void criAtomEx3dTransceiver_SetOutputVolume(System.IntPtr ex_3d_transceiver, System.Single volume) { }
        // RVA: 0x068E9968  token: 0x6000299
        private static System.Void criAtomEx3dTransceiver_AttachAisac(System.IntPtr ex_3d_transceiver, System.String global_aisac_name) { }
        // RVA: 0x068E9A88  token: 0x600029A
        private static System.Void criAtomEx3dTransceiver_DetachAisac(System.IntPtr ex_3d_transceiver, System.String global_aisac_name) { }
        // RVA: 0x068E9E54  token: 0x600029B
        private static System.Void criAtomEx3dTransceiver_SetMaxAngleAisacDelta(System.IntPtr ex_3d_transceiver, System.Single max_delta) { }
        // RVA: 0x068E9B64  token: 0x600029C
        private static System.Void criAtomEx3dTransceiver_SetDistanceAisacControlId(System.IntPtr ex_3d_transceiver, System.UInt16 aisac_control_id) { }
        // RVA: 0x068E9D4C  token: 0x600029D
        private static System.Void criAtomEx3dTransceiver_SetListenerBasedAzimuthAngleAisacControlId(System.IntPtr ex_3d_transceiver, System.UInt16 aisac_control_id) { }
        // RVA: 0x068E9DD0  token: 0x600029E
        private static System.Void criAtomEx3dTransceiver_SetListenerBasedElevationAngleAisacControlId(System.IntPtr ex_3d_transceiver, System.UInt16 aisac_control_id) { }
        // RVA: 0x068EA0E4  token: 0x600029F
        private static System.Void criAtomEx3dTransceiver_SetTransceiverOutputBasedAzimuthAngleAisacControlId(System.IntPtr ex_3d_transceiver, System.UInt16 aisac_control_id) { }
        // RVA: 0x068EA168  token: 0x60002A0
        private static System.Void criAtomEx3dTransceiver_SetTransceiverOutputBasedElevationAngleAisacControlId(System.IntPtr ex_3d_transceiver, System.UInt16 aisac_control_id) { }
        // RVA: 0x068E9AE0  token: 0x60002A1
        private static System.Void criAtomEx3dTransceiver_Set3dRegionHn(System.IntPtr ex_3d_transceiver, System.IntPtr ex_3d_region) { }

    }

    // TypeToken: 0x2000063  // size: 0x14
    public sealed struct Config
    {
        // Fields
        public System.Int32 reserved;  // 0x10

    }

    // TypeToken: 0x2000064  // size: 0x10
    public static class UnsafeNativeMethods
    {
        // Methods
        // RVA: 0x068E98A0  token: 0x60002A7
        private static System.IntPtr criAtomEx3dRegion_Create(CriWare.CriAtomEx3dRegion.Config& config, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068E9934  token: 0x60002A8
        private static System.Void criAtomEx3dRegion_Destroy(System.IntPtr ex_3d_region) { }

    }

    // TypeToken: 0x2000067  // size: 0x14
    public sealed struct Status
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExAcbLoader.Status Stop;  // const
        public static CriWare.CriAtomExAcbLoader.Status Loading;  // const
        public static CriWare.CriAtomExAcbLoader.Status Complete;  // const
        public static CriWare.CriAtomExAcbLoader.Status Error;  // const

    }

    // TypeToken: 0x2000068  // size: 0x11
    public sealed struct LoaderConfig
    {
        // Fields
        public System.Boolean shouldLoadAwbOnMemory;  // 0x10

    }

    // TypeToken: 0x200006A  // size: 0x38
    public sealed struct AcfDspSettingInfo
    {
        // Fields
        public System.String name;  // 0x10
        public System.UInt16[] busIndexes;  // 0x18
        public System.UInt16[] extendBusIndexes;  // 0x20
        public System.UInt16 snapshotStartIndex;  // 0x28
        public System.Byte numBuses;  // 0x2a
        public System.Byte numExtendBuses;  // 0x2b
        public System.UInt16 numSnapshots;  // 0x2c
        public System.UInt16 snapshotWorkSize;  // 0x2e
        public System.UInt16 numMixerAisacs;  // 0x30
        public System.UInt16 mixerAisacStartIndex;  // 0x32

        // Methods
        // RVA: 0x068D1E40  token: 0x6000334
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x200006B  // size: 0x38
    public sealed struct AcfDspSettingSnapshotInfo
    {
        // Fields
        public System.String name;  // 0x10
        public System.Byte numBuses;  // 0x18
        public System.Byte numExtendBuses;  // 0x19
        public System.Byte[] reserved;  // 0x20
        public System.UInt16[] busIndexes;  // 0x28
        public System.UInt16[] extendBusIndexes;  // 0x30

        // Methods
        // RVA: 0x068D2074  token: 0x6000335
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x200006C  // size: 0x48
    public sealed struct AcfDspBusInfo
    {
        // Fields
        public System.String name;  // 0x10
        public System.Single volume;  // 0x18
        public System.Single pan3dVolume;  // 0x1c
        public System.Single pan3dAngle;  // 0x20
        public System.Single pan3dDistance;  // 0x24
        public System.Single pan3dSpread;  // 0x28
        public System.UInt16[] fxIndexes;  // 0x30
        public System.UInt16[] busLinkIndexes;  // 0x38
        public System.UInt16 busNo;  // 0x40
        public System.Byte numFxes;  // 0x42
        public System.Byte numBusLinks;  // 0x43

        // Methods
        // RVA: 0x068D1AE4  token: 0x6000336
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x200006D  // size: 0x14
    public sealed struct AcfDspBusLinkType
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static CriWare.CriAtomExAcf.AcfDspBusLinkType preVolume;  // const
        public static CriWare.CriAtomExAcf.AcfDspBusLinkType postVolume;  // const
        public static CriWare.CriAtomExAcf.AcfDspBusLinkType postPan;  // const

    }

    // TypeToken: 0x200006E  // size: 0x1C
    public sealed struct AcfDspBusLinkInfo
    {
        // Fields
        public CriWare.CriAtomExAcf.AcfDspBusLinkType type;  // 0x10
        public System.Single sendLevel;  // 0x14
        public System.UInt16 busNo;  // 0x18
        public System.UInt16 busId;  // 0x1a

        // Methods
        // RVA: 0x068D1D14  token: 0x6000337
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x200006F  // size: 0x28
    public sealed struct CategoryInfo
    {
        // Fields
        public System.UInt32 groupNo;  // 0x10
        public System.UInt32 id;  // 0x14
        public System.String name;  // 0x18
        public System.UInt32 numCueLimits;  // 0x20
        public System.Single volume;  // 0x24

        // Methods
        // RVA: 0x068D2A90  token: 0x6000338
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x2000070  // size: 0x14
    public sealed struct AcfAisacType
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static CriWare.CriAtomExAcf.AcfAisacType normal;  // const
        public static CriWare.CriAtomExAcf.AcfAisacType autoModulation;  // const

    }

    // TypeToken: 0x2000071  // size: 0x28
    public sealed struct GlobalAisacInfo
    {
        // Fields
        public System.String name;  // 0x10
        public System.UInt16 index;  // 0x18
        public System.UInt16 numGraphs;  // 0x1a
        public CriWare.CriAtomExAcf.AcfAisacType type;  // 0x1c
        public System.Single randomRange;  // 0x20
        public System.UInt16 controlId;  // 0x24
        public System.UInt16 dummy;  // 0x26

        // Methods
        // RVA: 0x068E7F8C  token: 0x6000339
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x2000072  // size: 0x14
    public sealed struct AisacGraphType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExAcf.AisacGraphType none;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType volume;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType pitch;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType bandpassHigh;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType bandpassLow;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType biquadFreq;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType biquadQ;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType busSend0;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType busSend1;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType busSend2;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType busSend3;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType busSend4;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType busSend5;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType busSend6;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType busSend7;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType pan3dAngel;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType pan3dVolume;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType pan3dInteriorDistance;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType pan3dCenter;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType pan3dLfe;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac0;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac1;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac2;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac3;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac4;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac5;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac6;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac7;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac8;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac9;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac10;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac11;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac12;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac13;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac14;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType aisac15;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType priority;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType preDelayTime;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType biquadGain;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType pan3dMixdownCenter;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType pan3dMixdownLfe;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType egAttack;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType egRelease;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType playbackRatio;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType drySendL;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType drySendR;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType drySendCenter;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType drySendLfe;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType drySendSl;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType drySendSr;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType drySendEx1;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType drySendEx2;  // const
        public static CriWare.CriAtomExAcf.AisacGraphType panSpread;  // const

    }

    // TypeToken: 0x2000073  // size: 0x14
    public sealed struct AisacGraphInfo
    {
        // Fields
        public CriWare.CriAtomExAcf.AisacGraphType type;  // 0x10

        // Methods
        // RVA: 0x068D2544  token: 0x600033A
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x2000074  // size: 0x14
    public sealed struct CharacterEncoding
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static CriWare.CriAtomExAcf.CharacterEncoding utf8;  // const
        public static CriWare.CriAtomExAcf.CharacterEncoding sjis;  // const

    }

    // TypeToken: 0x2000075  // size: 0x50
    public sealed struct AcfInfo
    {
        // Fields
        public System.String name;  // 0x10
        public System.UInt32 size;  // 0x18
        public System.UInt32 version;  // 0x1c
        public CriWare.CriAtomExAcf.CharacterEncoding characterEncoding;  // 0x20
        public System.Int32 numDspSettings;  // 0x24
        public System.Int32 numCategories;  // 0x28
        public System.Int32 numCategoriesPerPlayback;  // 0x2c
        public System.Int32 numReacts;  // 0x30
        public System.Int32 numAisacControls;  // 0x34
        public System.Int32 numGlobalAisacs;  // 0x38
        public System.Int32 numGameVariables;  // 0x3c
        public System.Int32 maxBusesOfDspBusSettings;  // 0x40
        public System.Int32 numBuses;  // 0x44
        public System.Int32 numVoiceLimitGroups;  // 0x48
        public System.Int32 numOutputPorts;  // 0x4c

        // Methods
        // RVA: 0x068D2290  token: 0x600033B
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x2000076  // size: 0x20
    public sealed struct SelectorInfo
    {
        // Fields
        public System.String name;  // 0x10
        public System.UInt16 index;  // 0x18
        public System.UInt16 numLabels;  // 0x1a
        public System.UInt16 globalLabelIndex;  // 0x1c

        // Methods
        // RVA: 0x068E87B4  token: 0x600033C
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x2000077  // size: 0x20
    public sealed struct SelectorLabelInfo
    {
        // Fields
        public System.String selectorName;  // 0x10
        public System.String labelName;  // 0x18

        // Methods
        // RVA: 0x068E8880  token: 0x600033D
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x2000079  // size: 0x14
    public sealed struct SpeakerMapping
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExAsrRack.SpeakerMapping Auto;  // const
        public static CriWare.CriAtomExAsrRack.SpeakerMapping Monaural;  // const
        public static CriWare.CriAtomExAsrRack.SpeakerMapping Stereo;  // const
        public static CriWare.CriAtomExAsrRack.SpeakerMapping Ch5_1;  // const
        public static CriWare.CriAtomExAsrRack.SpeakerMapping Ch7_1;  // const
        public static CriWare.CriAtomExAsrRack.SpeakerMapping Ch5_1_2;  // const
        public static CriWare.CriAtomExAsrRack.SpeakerMapping Ch7_1_4;  // const

    }

    // TypeToken: 0x200007A  // size: 0x38
    public sealed struct Config
    {
        // Fields
        public System.Single serverFrequency;  // 0x10
        public System.Int32 numBuses;  // 0x14
        public System.Int32 outputChannels;  // 0x18
        public CriWare.CriAtomExAsrRack.SpeakerMapping speakerMapping;  // 0x1c
        public System.Int32 outputSamplingRate;  // 0x20
        public CriWare.CriAtomEx.SoundRendererType soundRendererType;  // 0x24
        public System.Int32 outputRackId;  // 0x28
        public System.IntPtr context;  // 0x30

    }

    // TypeToken: 0x200007B  // size: 0x11
    public sealed struct PlatformConfig
    {
        // Fields
        public System.Byte reserved;  // 0x10

    }

    // TypeToken: 0x200007C  // size: 0x38
    public sealed struct PerformanceInfo
    {
        // Fields
        public System.UInt32 processCount;  // 0x10
        public System.UInt32 lastProcessTime;  // 0x14
        public System.UInt32 maxProcessTime;  // 0x18
        public System.UInt32 averageProcessTime;  // 0x1c
        public System.UInt32 lastProcessInterval;  // 0x20
        public System.UInt32 maxProcessInterval;  // 0x24
        public System.UInt32 averageProcessInterval;  // 0x28
        public System.UInt32 lastProcessSamples;  // 0x2c
        public System.UInt32 maxProcessSamples;  // 0x30
        public System.UInt32 averageProcessSamples;  // 0x34

    }

    // TypeToken: 0x200007E  // size: 0x1C
    public sealed struct Config
    {
        // Fields
        public System.Int32 maxChannels;  // 0x10
        public System.Int32 maxSamplingRate;  // 0x14
        public CriWare.CriAtomEx.SoundRendererType soundRendererType;  // 0x18

        // Properties
        CriWare.CriAtomExAuxIn.Config Default { get; /* RVA: 0x04D88DB0 */ }

    }

    // TypeToken: 0x2000080  // size: 0x30
    public sealed struct DeviceInfo
    {
        // Fields
        public System.String deviceId;  // 0x10
        public System.String deviceName;  // 0x18
        public System.UInt32 deviceFlags;  // 0x20
        public System.Int32 maxChannels;  // 0x24
        public System.Int32 maxSamplingRate;  // 0x28

    }

    // TypeToken: 0x2000081  // size: 0x38
    public sealed struct Config
    {
        // Fields
        public System.String deviceId;  // 0x10
        public System.UInt32 flags;  // 0x18
        public System.Int32 numChannels;  // 0x1c
        public System.Int32 samplingRate;  // 0x20
        public System.UInt32 frameSize;  // 0x24
        public System.UInt32 bufferingTime;  // 0x28
        public System.IntPtr context;  // 0x30

        // Properties
        CriWare.CriAtomExMic.Config Default { get; /* RVA: 0x068D2CCC */ }

    }

    // TypeToken: 0x2000082  // size: 0x20
    public class Effect
    {
        // Fields
        private System.IntPtr <handle>k__BackingField;  // 0x10
        private System.IntPtr <afxInstance>k__BackingField;  // 0x18

        // Properties
        System.IntPtr handle { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x04D88BF0 */ }
        System.IntPtr afxInstance { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x011EC9D0 */ }

        // Methods
        // RVA: 0x04D88F20  token: 0x60003B2
        public System.Void .ctor(System.IntPtr handle, System.IntPtr afxInstance) { }

    }

    // TypeToken: 0x2000084  // size: 0x80
    public sealed class PcmCaptureCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FEB2C  token: 0x60003D0
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015547C0  token: 0x60003D1
        public virtual System.Void Invoke(System.Single[] dataL, System.Single[] dataR, System.Int32 numChannels, System.Int32 numData) { }
        // RVA: 0x068FEA84  token: 0x60003D2
        public virtual System.IAsyncResult BeginInvoke(System.Single[] dataL, System.Single[] dataR, System.Int32 numChannels, System.Int32 numData, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x60003D3
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000085  // size: 0x1C
    public sealed struct Config
    {
        // Fields
        public System.Boolean enableLevelmeter;  // 0x10
        public System.Boolean enableSpectrumAnalyzer;  // 0x11
        public System.Boolean enablePcmCapture;  // 0x12
        public System.Boolean enablePcmCaptureCallback;  // 0x13
        public System.Int32 numSpectrumAnalyzerBands;  // 0x14
        public System.Int32 numCapturedPcmSamples;  // 0x18

    }

    // TypeToken: 0x2000086  // size: 0x80
    public sealed class InternalPcmCaptureCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FE700  token: 0x60003D4
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015547C0  token: 0x60003D5
        public virtual System.Void Invoke(System.IntPtr dataL, System.IntPtr dataR, System.Int32 numChannels, System.Int32 numData) { }
        // RVA: 0x068FE644  token: 0x60003D6
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr dataL, System.IntPtr dataR, System.Int32 numChannels, System.Int32 numData, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x60003D7
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000088  // size: 0x14
    public sealed struct Status
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExPlayback.Status Prep;  // const
        public static CriWare.CriAtomExPlayback.Status Playing;  // const
        public static CriWare.CriAtomExPlayback.Status Removed;  // const

    }

    // TypeToken: 0x2000089  // size: 0x28
    public sealed struct TrackInfo
    {
        // Fields
        public System.UInt32 id;  // 0x10
        public CriWare.CriAtomEx.CueType sequenceType;  // 0x14
        public System.IntPtr playerHn;  // 0x18
        public System.UInt16 trackNo;  // 0x20
        public System.UInt16 reserved;  // 0x22

    }

    // TypeToken: 0x200008B  // size: 0x14
    public sealed struct Status
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExPlayer.Status Stop;  // const
        public static CriWare.CriAtomExPlayer.Status Prep;  // const
        public static CriWare.CriAtomExPlayer.Status Playing;  // const
        public static CriWare.CriAtomExPlayer.Status PlayEnd;  // const
        public static CriWare.CriAtomExPlayer.Status Error;  // const

    }

    // TypeToken: 0x200008C  // size: 0x24
    public sealed struct Config
    {
        // Fields
        public CriWare.CriAtomEx.VoiceAllocationMethod voiceAllocationMethod;  // 0x10
        public System.Int32 maxPathStrings;  // 0x14
        public System.Int32 maxPath;  // 0x18
        public System.Int32 maxAisacs;  // 0x1c
        public System.Boolean updatesTime;  // 0x20
        public System.Boolean enableAudioSyncedTimer;  // 0x21

    }

    // TypeToken: 0x200008D  // size: 0x14
    public sealed struct TimeStretchParameterId
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExPlayer.TimeStretchParameterId Ratio;  // const
        public static CriWare.CriAtomExPlayer.TimeStretchParameterId FrameTime;  // const
        public static CriWare.CriAtomExPlayer.TimeStretchParameterId Quality;  // const

    }

    // TypeToken: 0x200008E  // size: 0x14
    public sealed struct PitchShifterParameterId
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExPlayer.PitchShifterParameterId Pitch;  // const
        public static CriWare.CriAtomExPlayer.PitchShifterParameterId Formant;  // const
        public static CriWare.CriAtomExPlayer.PitchShifterParameterId Mode;  // const

    }

    // TypeToken: 0x2000090  // size: 0x14
    public sealed struct Type
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExPlayerOutputAnalyzer.Type LevelMeter;  // const
        public static CriWare.CriAtomExPlayerOutputAnalyzer.Type SpectrumAnalyzer;  // const
        public static CriWare.CriAtomExPlayerOutputAnalyzer.Type PcmCapture;  // const

    }

    // TypeToken: 0x2000091  // size: 0x18
    public sealed struct Config
    {
        // Fields
        public System.Int32 num_spectrum_analyzer_bands;  // 0x10
        public System.Int32 num_stored_output_data;  // 0x14

        // Methods
        // RVA: 0x04D85C40  token: 0x60004DC
        public System.Void .ctor(System.Int32 num_spectrum_analyzer_bands, System.Int32 num_stored_output_data) { }

    }

    // TypeToken: 0x2000093  // size: 0x12
    public sealed struct Config
    {
        // Fields
        public System.Boolean enableVoiceLimitScope;  // 0x10
        public System.Boolean enableCategoryCueLimitScope;  // 0x11

    }

    // TypeToken: 0x2000095  // size: 0x14
    public sealed struct ParameterType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExTween.ParameterType Basic;  // const
        public static CriWare.CriAtomExTween.ParameterType Aisac;  // const

    }

    // TypeToken: 0x2000097  // size: 0x14
    public sealed struct Target
    {
        // Fields
        public CriWare.CriAtomEx.Parameter parameterId;  // 0x10
        public System.UInt32 aisacIds;  // 0x10

    }

    // TypeToken: 0x2000096  // size: 0x18
    public sealed struct Config
    {
        // Fields
        public CriWare.CriAtomExTween.Config.Target target;  // 0x10
        public CriWare.CriAtomExTween.ParameterType parameterType;  // 0x14

    }

    // TypeToken: 0x2000099  // size: 0x14
    public sealed struct VoicePoolId
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExVoicePool.VoicePoolId StandardMemory;  // const
        public static CriWare.CriAtomExVoicePool.VoicePoolId StandardStreaming;  // const
        public static CriWare.CriAtomExVoicePool.VoicePoolId HcaMxMemory;  // const
        public static CriWare.CriAtomExVoicePool.VoicePoolId HcaMxStreaming;  // const

    }

    // TypeToken: 0x200009A  // size: 0x14
    public sealed struct PitchShifterMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExVoicePool.PitchShifterMode Music;  // const
        public static CriWare.CriAtomExVoicePool.PitchShifterMode Vocal;  // const
        public static CriWare.CriAtomExVoicePool.PitchShifterMode SoundEffect;  // const
        public static CriWare.CriAtomExVoicePool.PitchShifterMode Speech;  // const

    }

    // TypeToken: 0x200009B  // size: 0x18
    public sealed struct UsedVoicesInfo
    {
        // Fields
        public System.Int32 numUsedVoices;  // 0x10
        public System.Int32 numPoolVoices;  // 0x14

    }

    // TypeToken: 0x200009C  // size: 0x30
    public sealed struct PlayerConfig
    {
        // Fields
        public System.Int32 maxChannels;  // 0x10
        public System.Int32 maxSamplingRate;  // 0x14
        public System.Boolean streamingFlag;  // 0x18
        public System.Int32 soundRendererType;  // 0x1c
        public System.Int32 decodeLatency;  // 0x20
        private System.IntPtr context;  // 0x28

    }

    // TypeToken: 0x200009D  // size: 0x38
    public sealed struct VoicePoolConfig
    {
        // Fields
        public System.UInt32 identifier;  // 0x10
        public System.Int32 numVoices;  // 0x14
        public CriWare.CriAtomExVoicePool.PlayerConfig playerConfig;  // 0x18

    }

    // TypeToken: 0x200009E  // size: 0x1C
    public sealed struct PitchShifterConfig
    {
        // Fields
        public System.Int32 mode;  // 0x10
        public System.Int32 windowSize;  // 0x14
        public System.Int32 overlapTimes;  // 0x18

    }

    // TypeToken: 0x200009F  // size: 0x28
    public sealed struct ExPitchShifterConfig
    {
        // Fields
        public System.Int32 numDsp;  // 0x10
        public System.Int32 maxChannels;  // 0x14
        public System.Int32 maxSamplingRate;  // 0x18
        public CriWare.CriAtomExVoicePool.PitchShifterConfig config;  // 0x1c

    }

    // TypeToken: 0x20000A0  // size: 0x14
    public sealed struct TimeStretchConfig
    {
        // Fields
        public System.Int32 reserved;  // 0x10

    }

    // TypeToken: 0x20000A1  // size: 0x20
    public sealed struct ExTimeStretchConfig
    {
        // Fields
        public System.Int32 numDsp;  // 0x10
        public System.Int32 maxChannels;  // 0x14
        public System.Int32 maxSamplingRate;  // 0x18
        public CriWare.CriAtomExVoicePool.TimeStretchConfig config;  // 0x1c

    }

    // TypeToken: 0x20000A5  // size: 0x14
    public sealed struct RawPcmFormat
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExRawPcmVoicePool.RawPcmFormat Sint16;  // const
        public static CriWare.CriAtomExRawPcmVoicePool.RawPcmFormat Float32;  // const

    }

    // TypeToken: 0x20000A6  // size: 0x30
    public sealed struct RawPcmPlayerConfig
    {
        // Fields
        public CriWare.CriAtomExRawPcmVoicePool.RawPcmFormat format;  // 0x10
        public System.Int32 maxChannels;  // 0x14
        public System.Int32 maxSamplingRate;  // 0x18
        public System.Int32 soundRendererType;  // 0x1c
        public System.Int32 decodeLatency;  // 0x20
        private System.IntPtr context;  // 0x28

    }

    // TypeToken: 0x20000A7  // size: 0x38
    public sealed struct RawPcmVoicePoolConfig
    {
        // Fields
        public System.UInt32 identifier;  // 0x10
        public System.Int32 numVoices;  // 0x14
        public CriWare.CriAtomExRawPcmVoicePool.RawPcmPlayerConfig playerConfig;  // 0x18

    }

    // TypeToken: 0x20000A9  // size: 0x80
    public sealed class InternalDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FE57C  token: 0x600051E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x600051F
        public virtual System.UInt32 Invoke(System.IntPtr cbobj, System.IntPtr data, System.UInt32 numSamples) { }
        // RVA: 0x068FE4D8  token: 0x6000520
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr cbobj, System.IntPtr data, System.UInt32 numSamples, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539AB64  token: 0x6000521
        public virtual System.UInt32 EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000AA  // size: 0x80
    public sealed class Delegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FDFD8  token: 0x6000522
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015802E0  token: 0x6000523
        public virtual System.UInt32 Invoke(System.Single[][] buffer, System.UInt32 numSamples) { }
        // RVA: 0x068FDED8  token: 0x6000524
        public virtual System.IAsyncResult BeginInvoke(System.Single[][] buffer, System.UInt32 numSamples, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539AB64  token: 0x6000525
        public virtual System.UInt32 EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000AB  // size: 0x30
    public sealed class <>c__DisplayClass12_0
    {
        // Fields
        public System.Int32 bufferSize;  // 0x10
        public CriWare.CriAudioReadStream.Delegate callback;  // 0x18
        public System.Int32 numChannels;  // 0x20
        public System.Single[][] buffer;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000526
        public System.Void .ctor() { }
        // RVA: 0x068FF0B8  token: 0x6000527
        private System.UInt32 <.ctor>b__0(System.IntPtr cbobj, System.IntPtr data, System.UInt32 numSamples) { }

    }

    // TypeToken: 0x20000AD  // size: 0x80
    public sealed class InternalDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FE57C  token: 0x600052E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x600052F
        public virtual System.UInt32 Invoke(System.IntPtr cbobj, System.IntPtr data, System.UInt32 numSamples) { }
        // RVA: 0x068FE434  token: 0x6000530
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr cbobj, System.IntPtr data, System.UInt32 numSamples, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539AB64  token: 0x6000531
        public virtual System.UInt32 EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000AE  // size: 0x80
    public sealed class Delegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FDFD8  token: 0x6000532
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015802E0  token: 0x6000533
        public virtual System.UInt32 Invoke(System.Single[][] buffer, System.UInt32 numSamples) { }
        // RVA: 0x068FDF58  token: 0x6000534
        public virtual System.IAsyncResult BeginInvoke(System.Single[][] buffer, System.UInt32 numSamples, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539AB64  token: 0x6000535
        public virtual System.UInt32 EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000AF  // size: 0x28
    public sealed class <>c__DisplayClass12_0
    {
        // Fields
        public System.Int32 bufferSize;  // 0x10
        public System.Int32 numChannels;  // 0x14
        public CriWare.CriAudioWriteStream.Delegate callback;  // 0x18
        public System.Single[][] buffer;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000536
        public System.Void .ctor() { }
        // RVA: 0x068FEFDC  token: 0x6000537
        private System.UInt32 <.ctor>b__0(System.IntPtr cbobj, System.IntPtr data, System.UInt32 numSamples) { }

    }

    // TypeToken: 0x20000B1  // size: 0x14
    public sealed struct Status
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriFsLoader.Status Stop;  // const
        public static CriWare.CriFsLoader.Status Loading;  // const
        public static CriWare.CriFsLoader.Status Complete;  // const
        public static CriWare.CriFsLoader.Status Error;  // const

    }

    // TypeToken: 0x20000B3  // size: 0x14
    public sealed struct Status
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriFsInstaller.Status Stop;  // const
        public static CriWare.CriFsInstaller.Status Busy;  // const
        public static CriWare.CriFsInstaller.Status Complete;  // const
        public static CriWare.CriFsInstaller.Status Error;  // const

    }

    // TypeToken: 0x20000B4  // size: 0x14
    public sealed struct CopyPolicy
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriFsInstaller.CopyPolicy Always;  // const

    }

    // TypeToken: 0x20000B6  // size: 0x14
    public sealed struct Status
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriFsBinder.Status None;  // const
        public static CriWare.CriFsBinder.Status Analyze;  // const
        public static CriWare.CriFsBinder.Status Complete;  // const
        public static CriWare.CriFsBinder.Status Unbind;  // const
        public static CriWare.CriFsBinder.Status Removed;  // const
        public static CriWare.CriFsBinder.Status Invalid;  // const
        public static CriWare.CriFsBinder.Status Error;  // const

    }

    // TypeToken: 0x20000B7  // size: 0x40
    public sealed struct ContentsFileInfo
    {
        // Fields
        public readonly System.String directory;  // 0x10
        public readonly System.String fileName;  // 0x18
        public System.UInt32 readSize;  // 0x20
        public System.UInt32 extractSize;  // 0x24
        public System.UInt64 offset;  // 0x28
        public System.Int32 fileId;  // 0x30
        private System.String userStr;  // 0x38

        // Methods
        // RVA: 0x068EA738  token: 0x600057F
        public System.Void .ctor(System.Byte[] data, System.Int32 startIndex) { }

    }

    // TypeToken: 0x20000B9  // size: 0x80
    public sealed class DoneDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FE0AC  token: 0x6000591
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000592
        public virtual System.Void Invoke(CriWare.CriFsRequest request) { }
        // RVA: 0x047FDD70  token: 0x6000593
        public virtual System.IAsyncResult BeginInvoke(CriWare.CriFsRequest request, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000594
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000BA  // size: 0x28
    public sealed class <CheckDone>d__23 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public CriWare.CriFsRequest <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000595
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000596
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x068FEE14  token: 0x6000597
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x068FEE5C  token: 0x6000599
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000BC  // size: 0x14
    public sealed struct Phase
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriFsLoadFileRequest.Phase Stop;  // const
        public static CriWare.CriFsLoadFileRequest.Phase Bind;  // const
        public static CriWare.CriFsLoadFileRequest.Phase Load;  // const
        public static CriWare.CriFsLoadFileRequest.Phase Done;  // const
        public static CriWare.CriFsLoadFileRequest.Phase Error;  // const

    }

    // TypeToken: 0x20000C2  // size: 0x14
    public sealed struct BindType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriFsBindRequest.BindType Cpk;  // const
        public static CriWare.CriFsBindRequest.BindType Directory;  // const
        public static CriWare.CriFsBindRequest.BindType File;  // const

    }

    // TypeToken: 0x20000C7  // size: 0x14
    public sealed struct Status
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriFsWebInstaller.Status Stop;  // const
        public static CriWare.CriFsWebInstaller.Status Busy;  // const
        public static CriWare.CriFsWebInstaller.Status Complete;  // const
        public static CriWare.CriFsWebInstaller.Status Error;  // const

    }

    // TypeToken: 0x20000C8  // size: 0x14
    public sealed struct Error
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriFsWebInstaller.Error None;  // const
        public static CriWare.CriFsWebInstaller.Error Timeout;  // const
        public static CriWare.CriFsWebInstaller.Error Memory;  // const
        public static CriWare.CriFsWebInstaller.Error LocalFs;  // const
        public static CriWare.CriFsWebInstaller.Error DNS;  // const
        public static CriWare.CriFsWebInstaller.Error Connection;  // const
        public static CriWare.CriFsWebInstaller.Error SSL;  // const
        public static CriWare.CriFsWebInstaller.Error HTTP;  // const
        public static CriWare.CriFsWebInstaller.Error Internal;  // const

    }

    // TypeToken: 0x20000C9  // size: 0x30
    public sealed struct StatusInfo
    {
        // Fields
        public CriWare.CriFsWebInstaller.Status status;  // 0x10
        public CriWare.CriFsWebInstaller.Error error;  // 0x14
        public System.Int32 httpStatusCode;  // 0x18
        public System.Int64 contentsSize;  // 0x20
        public System.Int64 receivedSize;  // 0x28

    }

    // TypeToken: 0x20000CA  // size: 0x38
    public sealed struct ModuleConfig
    {
        // Fields
        public System.UInt32 numInstallers;  // 0x10
        public System.String proxyHost;  // 0x18
        public System.UInt16 proxyPort;  // 0x20
        public System.String userAgent;  // 0x28
        public System.UInt32 inactiveTimeoutSec;  // 0x30
        public System.Boolean allowInsecureSSL;  // 0x34
        public System.Boolean crcEnabled;  // 0x35
        public CriWare.CriFsWebInstaller.ModulePlatformConfig platformConfig;  // 0x36

    }

    // TypeToken: 0x20000CB  // size: 0x11
    public sealed struct ModulePlatformConfig
    {
        // Fields
        public System.Byte reserved;  // 0x10

        // Properties
        CriWare.CriFsWebInstaller.ModulePlatformConfig defaultConfig { get; /* RVA: 0x012081B0 */ }

    }

    // TypeToken: 0x20000D2  // size: 0x14
    public sealed struct MaxFrameDrop
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriManaMovieMaterialBase.MaxFrameDrop Disabled;  // const
        public static CriWare.CriManaMovieMaterialBase.MaxFrameDrop One;  // const
        public static CriWare.CriManaMovieMaterialBase.MaxFrameDrop Two;  // const
        public static CriWare.CriManaMovieMaterialBase.MaxFrameDrop Three;  // const
        public static CriWare.CriManaMovieMaterialBase.MaxFrameDrop Four;  // const
        public static CriWare.CriManaMovieMaterialBase.MaxFrameDrop Five;  // const
        public static CriWare.CriManaMovieMaterialBase.MaxFrameDrop Six;  // const
        public static CriWare.CriManaMovieMaterialBase.MaxFrameDrop Seven;  // const
        public static CriWare.CriManaMovieMaterialBase.MaxFrameDrop Eight;  // const
        public static CriWare.CriManaMovieMaterialBase.MaxFrameDrop Nine;  // const
        public static CriWare.CriManaMovieMaterialBase.MaxFrameDrop Ten;  // const
        public static CriWare.CriManaMovieMaterialBase.MaxFrameDrop Infinite;  // const

    }

    // TypeToken: 0x20000D3  // size: 0x14
    public sealed struct RenderMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriManaMovieMaterialBase.RenderMode Always;  // const
        public static CriWare.CriManaMovieMaterialBase.RenderMode OnVisibility;  // const
        public static CriWare.CriManaMovieMaterialBase.RenderMode Never;  // const

    }

    // TypeToken: 0x20000D4  // size: 0x80
    public sealed class OnApplicationPauseCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FE95C  token: 0x6000691
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x6000692
        public virtual System.Void Invoke(CriWare.CriManaMovieMaterialBase manaMovieMaterial, System.Boolean appPause) { }
        // RVA: 0x068FE8DC  token: 0x6000693
        public virtual System.IAsyncResult BeginInvoke(CriWare.CriManaMovieMaterialBase manaMovieMaterial, System.Boolean appPause, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000694
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000D5  // size: 0x48
    public sealed struct <AsyncDispose>d__53 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncVoidMethodBuilder <>t__builder;  // 0x18
        public CriWare.CriMana.Player player;  // 0x38
        private System.Runtime.CompilerServices.TaskAwaiter <>u__1;  // 0x40

        // Methods
        // RVA: 0x068FEC94  token: 0x6000695
        private virtual System.Void MoveNext() { }
        // RVA: 0x053982D4  token: 0x6000696
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x20000D6  // size: 0x28
    public sealed class <RestartPlayerRoutine>d__60 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public CriWare.CriManaMovieMaterialBase <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000697
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000698
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x068FEEA8  token: 0x6000699
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x068FEF90  token: 0x600069B
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000D9  // size: 0x1C
    public sealed struct CpuUsage
    {
        // Fields
        public System.Single last;  // 0x10
        public System.Single average;  // 0x14
        public System.Single peak;  // 0x18

    }

    // TypeToken: 0x20000DB  // size: 0x80
    public sealed class CallbackFromNativeDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x04CF43E0  token: 0x60006B7
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014E2060  token: 0x60006B8
        public virtual System.UInt64 Invoke(System.IntPtr ptr1) { }
        // RVA: 0x068EA504  token: 0x60006B9
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr ptr1, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DEC64  token: 0x60006BA
        public virtual System.UInt64 EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000DD  // size: 0x80
    public sealed class Callback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x60006CA
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x60006CB
        public virtual System.Void Invoke(System.String message) { }
        // RVA: 0x047FDD70  token: 0x60006CC
        public virtual System.IAsyncResult BeginInvoke(System.String message, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x60006CD
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000DF  // size: 0x80
    public sealed class Callback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x60006D6
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x60006D7
        public virtual System.Void Invoke(System.String message) { }
        // RVA: 0x047FDD70  token: 0x60006D8
        public virtual System.IAsyncResult BeginInvoke(System.String message, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x60006D9
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000E0  // size: 0x80
    public sealed class ErrorCallbackFunc : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FE314  token: 0x60006DA
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015547C0  token: 0x60006DB
        public virtual System.Void Invoke(System.IntPtr errmsgPtr, System.UInt32 p1, System.UInt32 p2, System.IntPtr parray) { }
        // RVA: 0x068FE258  token: 0x60006DC
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr errmsgPtr, System.UInt32 p1, System.UInt32 p2, System.IntPtr parray, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x60006DD
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000E1  // size: 0x10
    public static class NativeMethod
    {
        // Methods
        // RVA: 0x068FE85C  token: 0x60006DE
        private static System.Void criErr_SetCallback(CriWare.CriErrorNotifier.ErrorCallbackFunc callback) { }
        // RVA: 0x068FE7C8  token: 0x60006DF
        private static System.IntPtr criErr_ConvertIdToMessage(System.IntPtr errmsgPtr, System.UInt32 p1, System.UInt32 p2) { }

    }

    // TypeToken: 0x20000E4  // size: 0x18
    public class StandardVoicePoolConfig
    {
        // Fields
        public System.Int32 memoryVoices;  // 0x10
        public System.Int32 streamingVoices;  // 0x14

        // Methods
        // RVA: 0x04D89120  token: 0x60006E2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E5  // size: 0x18
    public class HcaMxVoicePoolConfig
    {
        // Fields
        public System.Int32 memoryVoices;  // 0x10
        public System.Int32 streamingVoices;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x60006E3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E6  // size: 0x14
    public sealed struct InGamePreviewSwitchMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomConfig.InGamePreviewSwitchMode Disable;  // const
        public static CriWare.CriAtomConfig.InGamePreviewSwitchMode Enable;  // const
        public static CriWare.CriAtomConfig.InGamePreviewSwitchMode FollowBuildSetting;  // const
        public static CriWare.CriAtomConfig.InGamePreviewSwitchMode Default;  // const

    }

    // TypeToken: 0x20000E7  // size: 0x20
    public class InGamePreviewConfig
    {
        // Fields
        public System.Int32 maxPreviewObjects;  // 0x10
        public System.Int32 communicationBufferSize;  // 0x14
        public System.Int32 playbackPositionUpdateInterval;  // 0x18

        // Methods
        // RVA: 0x04D890F0  token: 0x60006E4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E8  // size: 0x18
    public class EditorPcmOutputConfig
    {
        // Fields
        public System.Boolean enable;  // 0x10
        public System.Int32 bufferLength;  // 0x14

        // Methods
        // RVA: 0x04D890E0  token: 0x60006E5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E9  // size: 0x14
    public sealed struct LinuxOutput
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomConfig.LinuxOutput Default;  // const
        public static CriWare.CriAtomConfig.LinuxOutput PulseAudio;  // const
        public static CriWare.CriAtomConfig.LinuxOutput ALSA;  // const

    }

    // TypeToken: 0x20000EA  // size: 0x18
    public class AndroidLowLatencyStandardVoicePoolConfig
    {
        // Fields
        public System.Int32 memoryVoices;  // 0x10
        public System.Int32 streamingVoices;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x60006E6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EB  // size: 0x18
    public class VitaManaVoicePoolConfig
    {
        // Fields
        public System.Int32 numberOfManaDecoders;  // 0x10

        // Methods
        // RVA: 0x04D89140  token: 0x60006E7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EC  // size: 0x18
    public class VitaAtrac9VoicePoolConfig
    {
        // Fields
        public System.Int32 memoryVoices;  // 0x10
        public System.Int32 streamingVoices;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x60006E8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000ED  // size: 0x18
    public class Ps4Atrac9VoicePoolConfig
    {
        // Fields
        public System.Int32 memoryVoices;  // 0x10
        public System.Int32 streamingVoices;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x60006E9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EE  // size: 0x18
    public class Ps5PortConfig
    {
        // Fields
        public System.Int32 maxChannelPorts;  // 0x10
        public System.Int32 maxObjectPorts;  // 0x14

        // Methods
        // RVA: 0x04D89110  token: 0x60006EA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EF  // size: 0x18
    public class SwitchOpusVoicePoolConfig
    {
        // Fields
        public System.Int32 memoryVoices;  // 0x10
        public System.Int32 streamingVoices;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x60006EB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F1  // size: 0x18
    public class VoicePoolConfig
    {
        // Fields
        public System.Int32 memoryVoices;  // 0x10
        public System.Int32 streamingVoices;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x60006ED
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F0  // size: 0x20
    public class Ps4Audio3dConfig
    {
        // Fields
        public System.Boolean useAudio3D;  // 0x10
        public CriWare.CriAtomConfig.Ps4Audio3dConfig.VoicePoolConfig voicePoolConfig;  // 0x18

        // Methods
        // RVA: 0x046D9090  token: 0x60006EC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F2  // size: 0x18
    public class WebGLWebAudioVoicePoolConfig
    {
        // Fields
        public System.Int32 voices;  // 0x10

        // Methods
        // RVA: 0x04D89150  token: 0x60006EE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F4  // size: 0x18
    public class PCH264PlaybackConfig
    {
        // Fields
        public System.Boolean useH264Playback;  // 0x10

        // Methods
        // RVA: 0x015EFCD0  token: 0x60006F0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F5  // size: 0x20
    public class VitaH264PlaybackConfig
    {
        // Fields
        public System.Boolean useH264Playback;  // 0x10
        public System.Int32 maxWidth;  // 0x14
        public System.Int32 maxHeight;  // 0x18
        public System.Boolean getMemoryFromTexture;  // 0x1c

        // Methods
        // RVA: 0x04D89130  token: 0x60006F1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F6  // size: 0x20
    public class WebGLConfig
    {
        // Fields
        public System.String webworkerPath;  // 0x10
        public System.Int32 heapSize;  // 0x18

        // Methods
        // RVA: 0x046D8CA0  token: 0x60006F2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FA  // size: 0x30
    public sealed struct ResourcesInfo
    {
        // Fields
        public CriWare.CriAtomEx.ResourceUsage virtualVoiceUsage;  // 0x10
        public CriWare.CriAtomEx.ResourceUsage sequenceUsage;  // 0x18
        public CriWare.CriAtomEx.ResourceUsage sequenceTrackUsage;  // 0x20
        public CriWare.CriAtomEx.ResourceUsage sequenceTrackItemUsage;  // 0x28

    }

    // TypeToken: 0x20000FC  // size: 0x28
    public sealed struct CategoryInfo
    {
        // Fields
        public System.UInt32 groupNo;  // 0x10
        public System.UInt32 id;  // 0x14
        public System.String name;  // 0x18
        public System.UInt32 numCueLimits;  // 0x20
        public System.Single volume;  // 0x24

    }

    // TypeToken: 0x20000FD  // size: 0x48
    public sealed struct DspBusInfo
    {
        // Fields
        public System.String name;  // 0x10
        public System.Single volume;  // 0x18
        public System.Single pan3dVolume;  // 0x1c
        public System.Single pan3dAngle;  // 0x20
        public System.Single pan3dDistance;  // 0x24
        public System.Single pan3dSpread;  // 0x28
        public System.UInt16[] fxIndexes;  // 0x30
        public System.UInt16[] busLinkIndexes;  // 0x38
        public System.UInt16 busNo;  // 0x40
        public System.Byte numFxes;  // 0x42
        public System.Byte numBusLinks;  // 0x43

    }

    // TypeToken: 0x20000FE  // size: 0x20
    public sealed struct AisacControlInfo
    {
        // Fields
        public System.String name;  // 0x10
        public System.UInt32 id;  // 0x18

    }

    // TypeToken: 0x20000FF  // size: 0x14
    public sealed struct AisacType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriAtomExAcfDebug.AisacType Normal;  // const
        public static CriWare.CriAtomExAcfDebug.AisacType AutoModulation;  // const

    }

    // TypeToken: 0x2000100  // size: 0x28
    public sealed struct GlobalAisacInfo
    {
        // Fields
        public System.String name;  // 0x10
        public System.UInt16 index;  // 0x18
        public System.UInt16 numGraphs;  // 0x1a
        public CriWare.CriAtomExAcfDebug.AisacType type;  // 0x1c
        public System.Single randomRange;  // 0x20
        public System.UInt16 controlId;  // 0x24

    }

    // TypeToken: 0x2000101  // size: 0x20
    public sealed struct SelectorInfo
    {
        // Fields
        public System.String name;  // 0x10
        public System.UInt16 index;  // 0x18
        public System.UInt16 numLabels;  // 0x1a
        public System.UInt16 globalLabelIndex;  // 0x1c

    }

    // TypeToken: 0x2000102  // size: 0x20
    public sealed struct SelectorLabelInfo
    {
        // Fields
        public System.String selectorName;  // 0x10
        public System.String labelName;  // 0x18

    }

    // TypeToken: 0x2000103  // size: 0x28
    public sealed struct CategoryInfoForMarshaling
    {
        // Fields
        public System.UInt32 groupNo;  // 0x10
        public System.UInt32 id;  // 0x14
        public System.IntPtr namePtr;  // 0x18
        public System.UInt32 numCueLimits;  // 0x20
        public System.Single volume;  // 0x24

        // Methods
        // RVA: 0x068EA57C  token: 0x6000727
        public System.Void Convert(CriWare.CriAtomExAcfDebug.CategoryInfo& x) { }

    }

    // TypeToken: 0x2000104  // size: 0x48
    public sealed struct DspBusInfoForMarshaling
    {
        // Fields
        public System.IntPtr namePtr;  // 0x10
        public System.Single volume;  // 0x18
        public System.Single pan3dVolume;  // 0x1c
        public System.Single pan3dAngle;  // 0x20
        public System.Single pan3dDistance;  // 0x24
        public System.Single pan3dSpread;  // 0x28
        public System.UInt16[] fxIndexes;  // 0x30
        public System.UInt16[] busLinkIndexes;  // 0x38
        public System.UInt16 busNo;  // 0x40
        public System.Byte numFxes;  // 0x42
        public System.Byte numBusLinks;  // 0x43

        // Methods
        // RVA: 0x068FE1D0  token: 0x6000728
        public System.Void Convert(CriWare.CriAtomExAcfDebug.DspBusInfo& x) { }

    }

    // TypeToken: 0x2000105  // size: 0x20
    public sealed struct AisacControlInfoForMarshaling
    {
        // Fields
        public System.IntPtr namePtr;  // 0x10
        public System.UInt32 id;  // 0x18

        // Methods
        // RVA: 0x068EA4CC  token: 0x6000729
        public System.Void Convert(CriWare.CriAtomExAcfDebug.AisacControlInfo& x) { }

    }

    // TypeToken: 0x2000106  // size: 0x28
    public sealed struct GlobalAisacInfoForMarshaling
    {
        // Fields
        public System.IntPtr namePtr;  // 0x10
        public System.UInt16 index;  // 0x18
        public System.UInt16 numGraphs;  // 0x1a
        public System.UInt32 type;  // 0x1c
        public System.Single randomRange;  // 0x20
        public System.UInt16 controlId;  // 0x24
        public System.UInt16 dummy;  // 0x26

        // Methods
        // RVA: 0x068FE3DC  token: 0x600072A
        public System.Void Convert(CriWare.CriAtomExAcfDebug.GlobalAisacInfo& x) { }

    }

    // TypeToken: 0x2000107  // size: 0x20
    public sealed struct SelectorInfoForMarshaling
    {
        // Fields
        public System.IntPtr namePtr;  // 0x10
        public System.UInt16 index;  // 0x18
        public System.UInt16 numLabels;  // 0x1a
        public System.UInt16 globalLabelIndex;  // 0x1c

        // Methods
        // RVA: 0x068FEC00  token: 0x600072B
        public System.Void Convert(CriWare.CriAtomExAcfDebug.SelectorInfo& x) { }

    }

    // TypeToken: 0x2000108  // size: 0x20
    public sealed struct SelectorLabelInfoForMarshaling
    {
        // Fields
        public System.IntPtr selectorNamePtr;  // 0x10
        public System.IntPtr labelNamePtr;  // 0x18

        // Methods
        // RVA: 0x068FEC4C  token: 0x600072C
        public System.Void Convert(CriWare.CriAtomExAcfDebug.SelectorLabelInfo& x) { }

    }

    // TypeToken: 0x200010A  // size: 0x30
    public sealed struct AcbInfo
    {
        // Fields
        public System.String name;  // 0x10
        public System.UInt32 size;  // 0x18
        public System.UInt32 version;  // 0x1c
        public CriWare.CriAtomEx.CharacterEncoding characterEncoding;  // 0x20
        public System.Single volume;  // 0x24
        public System.Int32 numCues;  // 0x28

    }

    // TypeToken: 0x200010B  // size: 0x30
    public sealed struct AcbInfoForMarshaling
    {
        // Fields
        public System.IntPtr namePtr;  // 0x10
        public System.UInt32 size;  // 0x18
        public System.UInt32 version;  // 0x1c
        public CriWare.CriAtomEx.CharacterEncoding characterEncoding;  // 0x20
        public System.Single volume;  // 0x24
        public System.Int32 numCues;  // 0x28

        // Methods
        // RVA: 0x068EA47C  token: 0x600072F
        public System.Void Convert(CriWare.CriAtomExAcbDebug.AcbInfo& x) { }

    }

    // TypeToken: 0x200010F  // size: 0x14
    public sealed struct ModuleType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriDisposableObjectManager.ModuleType Atom;  // const
        public static CriWare.CriDisposableObjectManager.ModuleType AtomMic;  // const
        public static CriWare.CriDisposableObjectManager.ModuleType Fs;  // const
        public static CriWare.CriDisposableObjectManager.ModuleType FsWeb;  // const
        public static CriWare.CriDisposableObjectManager.ModuleType Mana;  // const
        public static CriWare.CriDisposableObjectManager.ModuleType Lips;  // const
        public static CriWare.CriDisposableObjectManager.ModuleType Vip;  // const
        public static CriWare.CriDisposableObjectManager.ModuleType Rtc;  // const

    }

    // TypeToken: 0x2000110  // size: 0x30
    public sealed struct ObjectRef
    {
        // Fields
        public System.Guid guid;  // 0x10
        public CriWare.CriDisposableObjectManager.ModuleType type;  // 0x20
        public CriWare.CriDisposable disposable;  // 0x28

        // Methods
        // RVA: 0x05397DE4  token: 0x6000744
        public System.Void .ctor(System.Guid _guid, CriWare.CriDisposable _disposable, CriWare.CriDisposableObjectManager.ModuleType _type) { }

    }

    // TypeToken: 0x2000117  // size: 0x34
    public sealed struct MovieInfoStruct
    {
        // Fields
        public System.UInt32 width;  // 0x10
        public System.UInt32 height;  // 0x14
        public System.UInt32 dispWidth;  // 0x18
        public System.UInt32 dispHeight;  // 0x1c
        public System.UInt32 framerateN;  // 0x20
        public System.UInt32 framerateD;  // 0x24
        public System.UInt32 totalFrames;  // 0x28
        public CriWare.CriMana.CodecType _codecType;  // 0x2c
        public CriWare.CriMana.CodecType _alphaCodecType;  // 0x30

    }

    // TypeToken: 0x2000119  // size: 0x14
    public sealed struct MovieMixerState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriTimeline.Mana.CriManaMixerBehaviour.MovieMixerState Preloading;  // const
        public static CriWare.CriTimeline.Mana.CriManaMixerBehaviour.MovieMixerState Ready;  // const
        public static CriWare.CriTimeline.Mana.CriManaMixerBehaviour.MovieMixerState Playing;  // const
        public static CriWare.CriTimeline.Mana.CriManaMixerBehaviour.MovieMixerState Stopping;  // const
        public static CriWare.CriTimeline.Mana.CriManaMixerBehaviour.MovieMixerState Stopped;  // const

    }

    // TypeToken: 0x200011A  // size: 0x14
    public sealed struct ClipState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriTimeline.Mana.CriManaMixerBehaviour.ClipState Idle;  // const
        public static CriWare.CriTimeline.Mana.CriManaMixerBehaviour.ClipState Prepare;  // const
        public static CriWare.CriTimeline.Mana.CriManaMixerBehaviour.ClipState Play;  // const
        public static CriWare.CriTimeline.Mana.CriManaMixerBehaviour.ClipState Seek;  // const

    }

    // TypeToken: 0x200011B  // size: 0x68
    public sealed struct <ProcessFrameOnSeeking>d__31 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncVoidMethodBuilder <>t__builder;  // 0x18
        public CriWare.CriTimeline.Mana.CriManaMixerBehaviour <>4__this;  // 0x38
        public UnityEngine.Timeline.TimelineClip activeClip;  // 0x40
        public CriWare.CriTimeline.Mana.CriManaClipBase clip;  // 0x48
        public System.Double frameTime;  // 0x50
        private System.Boolean <movieInfoReplaced>5__2;  // 0x58
        private System.Runtime.CompilerServices.TaskAwaiter <>u__1;  // 0x60

        // Methods
        // RVA: 0x069098FC  token: 0x6000784
        private virtual System.Void MoveNext() { }
        // RVA: 0x053982D4  token: 0x6000785
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x200011C  // size: 0x28
    public sealed class <seekPlaybackEnumerator>d__35 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public CriWare.CriTimeline.Mana.CriManaMixerBehaviour <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000786
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000787
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0690A044  token: 0x6000788
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0690A108  token: 0x600078A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000124  // size: 0x20
    public sealed struct PlayerSource
    {
        // Fields
        public readonly CriWare.CriAtomExPlayer player;  // 0x10
        public readonly CriWare.CriAtomEx3dSource source3d;  // 0x18

        // Methods
        // RVA: 0x06902A44  token: 0x60007CF
        public System.Void .ctor(CriWare.CriAtomEx3dListener listener) { }

    }

    // TypeToken: 0x200012E  // size: 0x14
    public sealed struct Status
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriMana.Player.Status Stop;  // const
        public static CriWare.CriMana.Player.Status Dechead;  // const
        public static CriWare.CriMana.Player.Status WaitPrep;  // const
        public static CriWare.CriMana.Player.Status Prep;  // const
        public static CriWare.CriMana.Player.Status Ready;  // const
        public static CriWare.CriMana.Player.Status Playing;  // const
        public static CriWare.CriMana.Player.Status PlayEnd;  // const
        public static CriWare.CriMana.Player.Status Error;  // const
        public static CriWare.CriMana.Player.Status StopProcessing;  // const
        public static CriWare.CriMana.Player.Status ReadyForRendering;  // const

    }

    // TypeToken: 0x200012F  // size: 0x14
    public sealed struct SetMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriMana.Player.SetMode New;  // const
        public static CriWare.CriMana.Player.SetMode Append;  // const
        public static CriWare.CriMana.Player.SetMode AppendRepeatedly;  // const

    }

    // TypeToken: 0x2000130  // size: 0x14
    public sealed struct MovieEventSyncMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriMana.Player.MovieEventSyncMode FrameTime;  // const
        public static CriWare.CriMana.Player.MovieEventSyncMode PlayBackTime;  // const

    }

    // TypeToken: 0x2000131  // size: 0x14
    public sealed struct AudioTrack
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriMana.Player.AudioTrack Off;  // const
        public static CriWare.CriMana.Player.AudioTrack Auto;  // const

    }

    // TypeToken: 0x2000132  // size: 0x14
    public sealed struct TimerType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriMana.Player.TimerType None;  // const
        public static CriWare.CriMana.Player.TimerType System;  // const
        public static CriWare.CriMana.Player.TimerType Audio;  // const
        public static CriWare.CriMana.Player.TimerType User;  // const
        public static CriWare.CriMana.Player.TimerType Manual;  // const

    }

    // TypeToken: 0x2000133  // size: 0x80
    public sealed class CuePointCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068D2BF8  token: 0x6000881
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000882
        public virtual System.Void Invoke(CriWare.CriMana.EventPoint& eventPoint) { }
        // RVA: 0x06902820  token: 0x6000883
        public virtual System.IAsyncResult BeginInvoke(CriWare.CriMana.EventPoint& eventPoint, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05FD0344  token: 0x6000884
        public virtual System.Void EndInvoke(CriWare.CriMana.EventPoint& eventPoint, System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000134  // size: 0x80
    public sealed class StatusChangeCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x069095FC  token: 0x6000885
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000886
        public virtual System.Void Invoke(CriWare.CriMana.Player.Status status) { }
        // RVA: 0x06909584  token: 0x6000887
        public virtual System.IAsyncResult BeginInvoke(CriWare.CriMana.Player.Status status, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000888
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000135  // size: 0x80
    public sealed class SubtitleChangeCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0690973C  token: 0x6000889
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x600088A
        public virtual System.Void Invoke(System.IntPtr subtitleBuffer) { }
        // RVA: 0x069096C4  token: 0x600088B
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr subtitleBuffer, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x600088C
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000136  // size: 0x80
    public sealed class ShaderDispatchCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0690945C  token: 0x600088D
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015802E0  token: 0x600088E
        public virtual UnityEngine.Shader Invoke(CriWare.CriMana.MovieInfo movieInfo, System.Boolean additiveMode) { }
        // RVA: 0x069093DC  token: 0x600088F
        public virtual System.IAsyncResult BeginInvoke(CriWare.CriMana.MovieInfo movieInfo, System.Boolean additiveMode, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05397E84  token: 0x6000890
        public virtual UnityEngine.Shader EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000137  // size: 0x80
    public sealed class CuePointCallbackFromNativeDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x06902758  token: 0x6000891
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x01546280  token: 0x6000892
        public virtual System.Void Invoke(System.IntPtr ptr1, System.IntPtr ptr2, CriWare.CriMana.EventPoint& eventPoint) { }
        // RVA: 0x069026AC  token: 0x6000893
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr ptr1, System.IntPtr ptr2, CriWare.CriMana.EventPoint& eventPoint, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05FD0344  token: 0x6000894
        public virtual System.Void EndInvoke(CriWare.CriMana.EventPoint& eventPoint, System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000138  // size: 0x14
    public sealed struct CriManaUnityPlayer_RenderEventAction
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriMana.Player.CriManaUnityPlayer_RenderEventAction UPDATE;  // const
        public static CriWare.CriMana.Player.CriManaUnityPlayer_RenderEventAction INITIALIZE;  // const
        public static CriWare.CriMana.Player.CriManaUnityPlayer_RenderEventAction RENDER;  // const
        public static CriWare.CriMana.Player.CriManaUnityPlayer_RenderEventAction DESTROY;  // const

    }

    // TypeToken: 0x2000139  // size: 0x14
    public sealed struct OperationType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriMana.Player.OperationType Normal;  // const
        public static CriWare.CriMana.Player.OperationType SetFile;  // const
        public static CriWare.CriMana.Player.OperationType SeekToTime;  // const
        public static CriWare.CriMana.Player.OperationType Pause;  // const

    }

    // TypeToken: 0x200013A  // size: 0x40
    public sealed struct Operation
    {
        // Fields
        public CriWare.CriMana.Player.OperationType opType;  // 0x10
        public CriWare.CriMana.Player.Status targetStatus;  // 0x14
        public System.Boolean isStopForSeek;  // 0x18
        public CriWare.CriFsBinder binder;  // 0x20
        public System.String moviePath;  // 0x28
        public CriWare.CriMana.Player.SetMode setMode;  // 0x30
        public System.Single timeToSeek;  // 0x34
        public System.Boolean sw;  // 0x38

        // Methods
        // RVA: 0x04D89250  token: 0x6000895
        public static CriWare.CriMana.Player.Operation Start() { }
        // RVA: 0x04D891A0  token: 0x6000896
        public static CriWare.CriMana.Player.Operation Pause(System.Boolean sw) { }
        // RVA: 0x04D892A0  token: 0x6000897
        public static CriWare.CriMana.Player.Operation Stop() { }
        // RVA: 0x04D89280  token: 0x6000898
        public static CriWare.CriMana.Player.Operation StopForSeek() { }
        // RVA: 0x04D891F0  token: 0x6000899
        public static CriWare.CriMana.Player.Operation Prepare() { }
        // RVA: 0x04D891C0  token: 0x600089A
        public static CriWare.CriMana.Player.Operation PrepareForRendering() { }
        // RVA: 0x06902A00  token: 0x600089B
        public static CriWare.CriMana.Player.Operation SetFile(CriWare.CriFsBinder binder, System.String moviePath, CriWare.CriMana.Player.SetMode setMode) { }
        // RVA: 0x04D89220  token: 0x600089C
        public static CriWare.CriMana.Player.Operation SeekToTime(System.Single time) { }

    }

    // TypeToken: 0x200013B  // size: 0x40
    public sealed class <IssuePluginUpdatesForFrames>d__152 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public CriWare.CriMana.Player <>4__this;  // 0x20
        public System.Int32 playerId;  // 0x28
        public System.Int32 frameCount;  // 0x2c
        public System.Boolean destroy;  // 0x30
        public UnityEngine.MonoBehaviour playerHolder;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600089D
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600089E
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x06909804  token: 0x600089F
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069098B0  token: 0x60008A1
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200013D  // size: 0x18
    public class RendererResourceFactoryH264Yuv : CriWare.CriMana.Detail.RendererResourceFactory
    {
        // Methods
        // RVA: 0x069067A8  token: 0x60008A4
        public virtual CriWare.CriMana.Detail.RendererResource CreateRendererResource(System.Int32 playerId, CriWare.CriMana.MovieInfo movieInfo, System.Boolean additive, UnityEngine.Shader userShader) { }
        // RVA: 0x041E1670  token: 0x60008A5
        protected virtual System.Void OnDisposeManaged() { }
        // RVA: 0x041E1670  token: 0x60008A6
        protected virtual System.Void OnDisposeUnmanaged() { }
        // RVA: 0x04D7BFB0  token: 0x60008A7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200013E  // size: 0x18
    public class RendererResourceFactorySofdecPrimeYuv : CriWare.CriMana.Detail.RendererResourceFactory
    {
        // Methods
        // RVA: 0x069068F0  token: 0x60008A8
        public virtual CriWare.CriMana.Detail.RendererResource CreateRendererResource(System.Int32 playerId, CriWare.CriMana.MovieInfo movieInfo, System.Boolean additive, UnityEngine.Shader userShader) { }
        // RVA: 0x041E1670  token: 0x60008A9
        protected virtual System.Void OnDisposeManaged() { }
        // RVA: 0x041E1670  token: 0x60008AA
        protected virtual System.Void OnDisposeUnmanaged() { }
        // RVA: 0x04D7BF30  token: 0x60008AB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200013F  // size: 0x18
    public class RendererResourceFactorySofdecPrimeYuvRawData : CriWare.CriMana.Detail.RendererResourceFactory
    {
        // Methods
        // RVA: 0x06906844  token: 0x60008AC
        public virtual CriWare.CriMana.Detail.RendererResource CreateRendererResource(System.Int32 playerId, CriWare.CriMana.MovieInfo movieInfo, System.Boolean additive, UnityEngine.Shader userShader) { }
        // RVA: 0x041E1670  token: 0x60008AD
        protected virtual System.Void OnDisposeManaged() { }
        // RVA: 0x041E1670  token: 0x60008AE
        protected virtual System.Void OnDisposeUnmanaged() { }
        // RVA: 0x04D7BF70  token: 0x60008AF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000147  // size: 0x1C
    public sealed struct __StaticArrayInitTypeSize=12
    {
    }

    // TypeToken: 0x2000146  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=12 4636993D3E1DA4E9D6B8F87B79E8F7C6D018580D52661950EABC3845C5897A4D;  // static @ 0x0

    }

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x2000003  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000002
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000003
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000004
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000005
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x20
    public class DataNAttribute : System.Attribute
    {
        // Fields
        public System.Int32 capacity;  // 0x10
        public System.Int32 tSize;  // 0x14
        public System.Boolean ring;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000006
        public System.Void .ctor() { }

    }

}

namespace CriWare
{

    // TypeToken: 0x2000008  // size: 0x10
    public class CriWarePS5
    {
        // Fields
        public static System.Int32 SystemUserID;  // const

        // Methods
        // RVA: 0x041E1670  token: 0x6000007
        private static System.Void SetupFileSystem(System.Boolean enableAprDirectMode) { }
        // RVA: 0x041E1670  token: 0x6000008
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public static class CriAtomPlugin
    {
        // Fields
        private static System.Int32 initializationCount;  // static @ 0x0
        private static System.Collections.Generic.List<System.IntPtr> effectInterfaceList;  // static @ 0x8
        private static System.Boolean isConfigured;  // static @ 0x10
        private static System.Single timeSinceStartup;  // static @ 0x14
        private static CriWare.Common.CpuUsage cpuUsage;  // static @ 0x18
        private static System.Int32 CRIATOMUNITY_PARAMETER_ID_LOOP_COUNT;  // static @ 0x24
        private static System.UInt16 CRIATOMPARAMETER2_ID_INVALID;  // static @ 0x28
        private static System.UInt64 temporalStorage;  // static @ 0x30

        // Properties
        System.Boolean isInitialized { get; /* RVA: 0x068E32C4 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600000A
        public static System.Void Log(System.String log) { }
        // RVA: 0x03C8D5F0  token: 0x600000C
        public static System.Void ExecuteQueuedCueLinkCallbacks() { }
        // RVA: 0x03C8D570  token: 0x600000D
        public static System.Void ExecuteQueuedEventCallbacks() { }
        // RVA: 0x03C8D4F0  token: 0x600000E
        public static System.Void ExecuteQueuedBeatSyncCallbacks() { }
        // RVA: 0x068E2C68  token: 0x600000F
        public static System.Boolean GetAudioEffectInterfaceList(System.Collections.Generic.List<System.IntPtr>& effect_interface_list) { }
        // RVA: 0x068E307C  token: 0x6000010
        private static System.IntPtr GetSpatializerCoreInterfaceFromAtomOculusAudioBridge() { }
        // RVA: 0x03A081A0  token: 0x6000011
        public static System.Void SetConfigParameters(System.Int32 max_virtual_voices, System.Int32 max_voice_limit_groups, System.Int32 max_categories, System.Int32 max_aisacs, System.Int32 max_bus_sends, System.Int32 max_sequence_events_per_frame, System.Int32 max_beatsync_callbacks_per_frame, System.Int32 max_cuelink_callbacks_per_frame, System.Int32 num_standard_memory_voices, System.Int32 num_standard_streaming_voices, System.Int32 num_hca_mx_memory_voices, System.Int32 num_hca_mx_streaming_voices, System.Int32 output_sampling_rate, System.Int32 num_asr_output_channels, System.Boolean uses_in_game_preview, System.Single server_frequency, System.Int32 max_parameter_blocks, System.Int32 categories_per_playback, System.Int32 max_faders, System.Int32 num_buses, System.Boolean vr_mode) { }
        // RVA: 0x03A06C50  token: 0x6000012
        public static System.Void SetConfigMonitorParametes(System.Int32 max_preview_objects, System.Int32 communication_buffer_size, System.Int32 playback_position_update_interval) { }
        // RVA: 0x03A06AF0  token: 0x6000013
        public static System.Void SetConfigAdditionalParameters_EDITOR(System.Boolean enable_user_pcm_output, System.Int32 user_pcm_buffer_length) { }
        // RVA: 0x03A06AA0  token: 0x6000014
        public static System.Void SetConfigAdditionalParameters_PC(System.Int64 buffering_time_pc) { }
        // RVA: 0x03A06B80  token: 0x6000015
        public static System.Void SetConfigAdditionalParameters_LINUX(CriWare.CriAtomConfig.LinuxOutput output, System.Int32 pulse_latency_usec) { }
        // RVA: 0x03A06BE0  token: 0x6000016
        public static System.Void SetConfigAdditionalParameters_IOS(System.Boolean enable_sonicsync, System.UInt32 buffering_time_ios, System.Boolean override_ipod_music_ios) { }
        // RVA: 0x03A06CC0  token: 0x6000017
        public static System.Void SetConfigAdditionalParameters_ANDROID(System.Boolean enable_sonicsync, System.Int32 num_low_delay_memory_voices, System.Int32 num_low_delay_streaming_voices, System.Int32 sound_buffering_time, System.Int32 sound_start_buffering_time, System.Boolean use_fast_mixer, System.Boolean use_aaudio, System.Int32 stream_type) { }
        // RVA: 0x041E1670  token: 0x6000018
        public static System.Void SetConfigAdditionalParameters_VITA(System.Int32 num_atrac9_memory_voices, System.Int32 num_atrac9_streaming_voices, System.Int32 num_mana_decoders) { }
        // RVA: 0x041E1670  token: 0x6000019
        public static System.Void SetConfigAdditionalParameters_PS4(System.Int32 num_atrac9_memory_voices, System.Int32 num_atrac9_streaming_voices, System.Boolean use_audio3d, System.Int32 num_audio3d_memory_voices, System.Int32 num_audio3d_streaming_voices) { }
        // RVA: 0x041E1670  token: 0x600001A
        public static System.Void SetConfigAdditionalParameters_PS5(System.Int32 max_channel_ports, System.Int32 max_object_ports) { }
        // RVA: 0x041E1670  token: 0x600001B
        public static System.Void SetConfigAdditionalParameters_SWITCH(System.Boolean enable_sonicsync, System.Int32 num_opus_memory_voices, System.Int32 num_opus_streaming_voices, System.Boolean init_socket) { }
        // RVA: 0x041E1670  token: 0x600001C
        public static System.Void SetConfigAdditionalParameters_WEBGL(System.Int32 num_webaudio_voices) { }
        // RVA: 0x068E326C  token: 0x600001D
        public static System.Void SetMaxSamplingRateForStandardVoicePool(System.Int32 sampling_rate_for_memory, System.Int32 sampling_rate_for_streaming) { }
        // RVA: 0x03A06790  token: 0x600001E
        public static System.Int32 GetRequiredMaxVirtualVoices(CriWare.CriAtomConfig atomConfig) { }
        // RVA: 0x03A05F80  token: 0x600001F
        public static System.Void InitializeLibrary() { }
        // RVA: 0x03A06A10  token: 0x6000020
        public static System.Boolean IsLibraryInitialized() { }
        // RVA: 0x068E2AB4  token: 0x6000021
        public static System.Void FinalizeLibrary() { }
        // RVA: 0x068E3214  token: 0x6000022
        public static System.Void Pause(System.Boolean pause) { }
        // RVA: 0x068E2D8C  token: 0x6000023
        public static CriWare.Common.CpuUsage GetCpuUsage() { }
        // RVA: 0x068E303C  token: 0x6000024
        public static System.Int32 GetOutputSamplingRate() { }
        // RVA: 0x068E2FFC  token: 0x6000025
        public static System.Int32 GetOutputChannels() { }
        // RVA: 0x012081B0  token: 0x6000026
        public static System.Boolean IsInitializedForPcmOutput() { }
        // RVA: 0x068E2F48  token: 0x6000027
        public static System.UInt16 GetLoopCountParameterId() { }
        // RVA: 0x068E29E8  token: 0x6000028
        public static System.Void DecryptAcb(System.IntPtr acb_hn, System.UInt64 key, System.UInt64 nonce) { }
        // RVA: 0x068E2998  token: 0x6000029
        private static System.UInt64 CallbackFromNative(System.IntPtr ptr1) { }
        // RVA: 0x03827F70  token: 0x600002A
        private static System.Void CRIWARE009CE561(System.Int32 max_virtual_voices, System.Int32 max_voice_limit_groups, System.Int32 max_categories, System.Int32 max_aisacs, System.Int32 max_bus_sends, System.Int32 max_sequence_events_per_frame, System.Int32 max_beatsync_callbacks_per_frame, System.Int32 max_cuelink_callbacks_per_frame, System.Int32 num_standard_memory_voices, System.Int32 num_standard_streaming_voices, System.Int32 num_hca_mx_memory_voices, System.Int32 num_hca_mx_streaming_voices, System.Int32 output_sampling_rate, System.Int32 num_asr_output_channels, System.Boolean uses_in_game_preview, System.Single server_frequency, System.Int32 max_parameter_blocks, System.Int32 categories_per_playback, System.Int32 max_faders, System.Int32 num_buses, System.Boolean use_ambisonics, System.IntPtr spatializer_core_interface) { }
        // RVA: 0x038274A0  token: 0x600002B
        private static System.Void CRIWARE8384362B(System.UInt32 max_preivew_objects, System.UInt32 communication_buffer_size, System.Int32 playback_position_update_interval) { }
        // RVA: 0x03A06B40  token: 0x600002C
        private static System.Void CRIWARE237CA5E3(System.Boolean enable_user_pcm_out_mode) { }
        // RVA: 0x03827F30  token: 0x600002D
        private static System.Void CRIWARE780E6ED2(System.Int64 buffering_time_pc) { }
        // RVA: 0x03827EF0  token: 0x600002E
        private static System.Void CRIWAREE996B258(System.Int32 output, System.Int32 pulse_latency_usec) { }
        // RVA: 0x03827400  token: 0x600002F
        private static System.Void CRIWARE2F034134(System.Boolean enable_sonicsync, System.UInt32 buffering_time_ios, System.Boolean override_ipod_music_ios) { }
        // RVA: 0x03827340  token: 0x6000030
        private static System.Void CRIWARE70981A20(System.Boolean enable_sonicsync, System.Int32 num_low_delay_memory_voices, System.Int32 num_low_delay_streaming_voices, System.Int32 sound_buffering_time, System.Int32 sound_start_buffering_time, System.Boolean apply_hw_property, System.Int32 stream_type) { }
        // RVA: 0x03A06130  token: 0x6000031
        private static System.Void CRIWARE2626EF5B() { }
        // RVA: 0x03A06A60  token: 0x6000032
        public static System.Boolean CRIWAREC176B113() { }
        // RVA: 0x068E27C4  token: 0x6000033
        private static System.Void CRIWARE63E1F278() { }
        // RVA: 0x068E28A8  token: 0x6000034
        private static System.Void CRIWAREB64E0C3B(System.Boolean pause) { }
        // RVA: 0x068E287C  token: 0x6000035
        public static System.UInt32 CRIWAREA703719E() { }
        // RVA: 0x068E28DC  token: 0x6000036
        public static System.Void CRIWAREBF97E87F(System.Int32 code) { }
        // RVA: 0x068E256C  token: 0x6000037
        public static System.Void CRIWARE15B0037A(System.IntPtr cbfunc, System.String separator_string) { }
        // RVA: 0x068E2638  token: 0x6000038
        public static System.Void CRIWARE28E5ABA4(System.IntPtr cbfunc) { }
        // RVA: 0x03C8D5C0  token: 0x6000039
        private static System.Void CRIWARECCA1268B() { }
        // RVA: 0x068E281C  token: 0x600003A
        public static System.Void CRIWARE88175F19(System.IntPtr cbfunc) { }
        // RVA: 0x03C8D540  token: 0x600003B
        private static System.Void CRIWARE7581CF8C() { }
        // RVA: 0x068E2604  token: 0x600003C
        public static System.Void CRIWARE15F30D07(System.IntPtr cbfunc) { }
        // RVA: 0x03C8D640  token: 0x600003D
        private static System.Void CRIWARED1FD92AA() { }
        // RVA: 0x068E266C  token: 0x600003E
        private static System.Void CRIWARE2CFF30B7(System.Int32 sampling_rate_for_memory, System.Int32 sampling_rate_for_streaming) { }
        // RVA: 0x068E27F0  token: 0x600003F
        public static System.Void CRIWARE797E0462() { }
        // RVA: 0x068E2910  token: 0x6000040
        public static System.Void CRIWAREC54D0552() { }
        // RVA: 0x068E2724  token: 0x6000041
        public static System.Void CRIWARE5BC8942D(System.IntPtr acb_hn, CriWare.CriAtomPlugin.CallbackFromNativeDelegate func, System.IntPtr obj) { }
        // RVA: 0x068E26AC  token: 0x6000042
        public static System.UInt16 CRIWARE5B1C5B3B(System.Int32 id) { }
        // RVA: 0x068E293C  token: 0x6000043
        private static System.Boolean CRIWAREE097EA6E() { }
        // RVA: 0x068E296C  token: 0x6000044
        private static System.Int32 CRIWAREE320F01B() { }
        // RVA: 0x068E2850  token: 0x6000045
        private static System.Int32 CRIWARE98A6BE5C() { }
        // RVA: 0x04CDA3F0  token: 0x6000046
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000F  // size: 0x38
    public class CriAtomCueSheet
    {
        // Fields
        public System.String name;  // 0x10
        public System.String acbFile;  // 0x18
        public System.String awbFile;  // 0x20
        public CriWare.CriAtomExAcb acb;  // 0x28
        public CriWare.CriAtomExAcbLoader.Status loaderStatus;  // 0x30

        // Properties
        System.Boolean IsLoading { get; /* RVA: 0x068D2DA0 */ }
        System.Boolean IsError { get; /* RVA: 0x068D2D84 */ }

        // Methods
        // RVA: 0x068D2D1C  token: 0x600004D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000010  // size: 0x60
    public class CriAtom : CriWare.CriMonoBehaviour
    {
        // Fields
        public System.String acfFile;  // 0x28
        private System.Boolean acfIsLoading;  // 0x30
        public CriWare.CriAtomCueSheet[] cueSheets;  // 0x38
        public System.String dspBusSetting;  // 0x40
        public System.Boolean dontDestroyOnLoad;  // 0x48
        private static CriWare.CriAtomExSequencer.EventCallback eventUserCallback;  // static @ 0x0
        private static CriWare.CriAtomExSequencer.EventCbFunc eventUserCbFunc;  // static @ 0x8
        private static CriWare.CriAtomExBeatSync.CbFunc beatsyncUserCbFunc;  // static @ 0x10
        private static CriWare.CriAtomExBeatSync.CbFunc obsoleteBeatSyncFunc;  // static @ 0x18
        private static CriWare.CriAtomEx.CueLinkCbFunc cueLinkUserCbFunc;  // static @ 0x20
        private static CriWare.CriAtom <instance>k__BackingField;  // static @ 0x28
        private System.Runtime.InteropServices.GCHandle acfRegisterGCHandle;  // 0x50
        public System.Boolean dontRemoveExistsCueSheet;  // 0x58

        // Properties
        CriWare.CriAtom instance { get; /* RVA: 0x068E7634 */ set; /* RVA: 0x068E77CC */ }
        System.Boolean CueSheetsAreLoading { get; /* RVA: 0x068E75A0 */ }

        // Events
        event CriWare.CriAtomExSequencer.EventCallback OnEventSequencerCallback;
        event CriWare.CriAtomExBeatSync.CbFunc beatsyncUserCbFunc;
        event CriWare.CriAtomExBeatSync.CbFunc OnBeatSyncCallback;
        event CriWare.CriAtomEx.CueLinkCbFunc cueLinkUserCbFunc;
        event CriWare.CriAtomEx.CueLinkCbFunc OnCueLinkCallback;

        // Methods
        // RVA: 0x068E5AF0  token: 0x600005A
        public static System.Void AttachDspBusSetting(System.String settingName) { }
        // RVA: 0x068E5D84  token: 0x600005B
        public static System.Void DetachDspBusSetting() { }
        // RVA: 0x068E5FB8  token: 0x600005C
        public static CriWare.CriAtomCueSheet GetCueSheet(System.String name) { }
        // RVA: 0x068E5A48  token: 0x600005D
        public static CriWare.CriAtomCueSheet AddCueSheet(System.String name, System.String acbFile, System.String awbFile, CriWare.CriFsBinder binder) { }
        // RVA: 0x068E5774  token: 0x600005E
        public static CriWare.CriAtomCueSheet AddCueSheetAsync(System.String name, System.String acbFile, System.String awbFile, CriWare.CriFsBinder binder, System.Boolean loadAwbOnMemory) { }
        // RVA: 0x068E5980  token: 0x600005F
        public static CriWare.CriAtomCueSheet AddCueSheet(System.String name, System.Byte[] acbData, System.String awbFile, CriWare.CriFsBinder awbBinder) { }
        // RVA: 0x068E56B4  token: 0x6000060
        public static CriWare.CriAtomCueSheet AddCueSheetAsync(System.String name, System.Byte[] acbData, System.String awbFile, CriWare.CriFsBinder awbBinder, System.Boolean loadAwbOnMemory) { }
        // RVA: 0x068E6B4C  token: 0x6000061
        public static System.Void RemoveCueSheet(System.String name) { }
        // RVA: 0x068E5DF0  token: 0x6000063
        public static CriWare.CriAtomExAcb GetAcb(System.String cueSheetName) { }
        // RVA: 0x068DC850  token: 0x6000064
        public static System.Void SetCategoryVolume(System.String name, System.Single volume) { }
        // RVA: 0x068DC848  token: 0x6000065
        public static System.Void SetCategoryVolume(System.Int32 id, System.Single volume) { }
        // RVA: 0x068DC7C4  token: 0x6000066
        public static System.Single GetCategoryVolume(System.String name) { }
        // RVA: 0x068DC7CC  token: 0x6000067
        public static System.Single GetCategoryVolume(System.Int32 id) { }
        // RVA: 0x068E6D78  token: 0x6000068
        public static System.Void SetBusAnalyzer(System.String busName, System.Boolean sw) { }
        // RVA: 0x068E6DA8  token: 0x6000069
        public static System.Void SetBusAnalyzer(System.Boolean sw) { }
        // RVA: 0x068E5EE0  token: 0x600006A
        public static CriWare.CriAtomExAsr.BusAnalyzerInfo GetBusAnalyzerInfo(System.String busName) { }
        // RVA: 0x068E5F10  token: 0x600006B
        public static CriWare.CriAtomExAsr.BusAnalyzerInfo GetBusAnalyzerInfo(System.Int32 busId) { }
        // RVA: 0x068E6EE8  token: 0x600006C
        public System.Void Setup() { }
        // RVA: 0x068E7108  token: 0x600006D
        public System.Void Shutdown() { }
        // RVA: 0x068E5B70  token: 0x600006E
        private System.Void Awake() { }
        // RVA: 0x068E6630  token: 0x600006F
        protected virtual System.Void OnEnable() { }
        // RVA: 0x068E653C  token: 0x6000070
        private System.Void OnDestroy() { }
        // RVA: 0x041E1670  token: 0x6000071
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x041E1670  token: 0x6000072
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: 0x068E5F3C  token: 0x6000073
        public CriWare.CriAtomCueSheet GetCueSheetInternal(System.String name) { }
        // RVA: 0x068E5814  token: 0x6000074
        public CriWare.CriAtomCueSheet AddCueSheetInternal(System.String name, System.String acbFile, System.String awbFile, CriWare.CriFsBinder binder) { }
        // RVA: 0x068E69E4  token: 0x6000075
        public System.Void RemoveCueSheetInternal(System.String name) { }
        // RVA: 0x068E639C  token: 0x6000076
        private System.Void MargeCueSheet(CriWare.CriAtomCueSheet[] newCueSheets, System.Boolean newDontRemoveExistsCueSheet) { }
        // RVA: 0x068E62A4  token: 0x6000077
        private CriWare.CriAtomExAcb LoadAcbFile(CriWare.CriFsBinder binder, System.String acbFile, System.String awbFile) { }
        // RVA: 0x068E60E8  token: 0x6000078
        private CriWare.CriAtomExAcb LoadAcbData(System.Byte[] acbData, CriWare.CriFsBinder binder, System.String awbFile) { }
        // RVA: 0x068E6198  token: 0x6000079
        private System.Void LoadAcbFileAsync(CriWare.CriAtomCueSheet cueSheet, CriWare.CriFsBinder binder, System.String acbFile, System.String awbFile, System.Boolean loadAwbOnMemory) { }
        // RVA: 0x068E61E0  token: 0x600007A
        private System.Collections.IEnumerator LoadAcbFileCoroutine(CriWare.CriAtomCueSheet cueSheet, CriWare.CriFsBinder binder, System.String acbPath, System.String awbPath, System.Boolean loadAwbOnMemory) { }
        // RVA: 0x068E5FE4  token: 0x600007B
        private System.Void LoadAcbDataAsync(CriWare.CriAtomCueSheet cueSheet, System.Byte[] acbData, CriWare.CriFsBinder awbBinder, System.String awbFile, System.Boolean loadAwbOnMemory) { }
        // RVA: 0x068E6024  token: 0x600007C
        private System.Collections.IEnumerator LoadAcbDataCoroutine(CriWare.CriAtomCueSheet cueSheet, System.Byte[] acbData, CriWare.CriFsBinder awbBinder, System.String awbPath, System.Boolean loadAwbOnMemory) { }
        // RVA: 0x068E6C18  token: 0x600007D
        public static System.Void SequenceEventCallbackFromNative(System.String eventString) { }
        // RVA: 0x068E6BC4  token: 0x600007E
        private static System.Void SequenceCallbackFromNative(CriWare.CriAtomExSequencer.CriAtomExSequenceEventInfo& criAtomExSequenceInfo) { }
        // RVA: 0x068E5CDC  token: 0x600007F
        public static System.Void BeatSyncCallbackFromNative(CriWare.CriAtomExBeatSync.Info& info) { }
        // RVA: 0x068E5D30  token: 0x6000080
        public static System.Void CueLinkCallbackFromNative(CriWare.CriAtomEx.CueLinkInfo& info) { }
        // RVA: 0x068E6DD4  token: 0x6000081
        public static System.Void SetEventCallback(CriWare.CriAtomExSequencer.EventCbFunc func, System.String separator) { }
        // RVA: 0x068E6880  token: 0x6000082
        private static System.Void RegisterEventCallbackChain(CriWare.CriAtomExSequencer.EventCallback func) { }
        // RVA: 0x068E72D0  token: 0x6000083
        private static System.Void UnregisterEventCallbackChain(CriWare.CriAtomExSequencer.EventCallback func) { }
        // RVA: 0x068E6C6C  token: 0x6000084
        public static System.Void SetBeatSyncCallback(CriWare.CriAtomExBeatSync.CbFunc func) { }
        // RVA: 0x068E66A0  token: 0x6000085
        private static System.Void RegisterBeatSyncCallbackChain(CriWare.CriAtomExBeatSync.CbFunc func) { }
        // RVA: 0x068E71F8  token: 0x6000086
        private static System.Void UnregisterBeatSyncCallbackChain(CriWare.CriAtomExBeatSync.CbFunc func) { }
        // RVA: 0x068E6790  token: 0x6000087
        private static System.Void RegisterCueLinkCallbackChain(CriWare.CriAtomEx.CueLinkCbFunc func) { }
        // RVA: 0x068E7264  token: 0x6000088
        private static System.Void UnregisterCueLinkCallbackChain(CriWare.CriAtomEx.CueLinkCbFunc func) { }
        // RVA: 0x068E73BC  token: 0x6000089
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000014  // size: 0x58
    public class CriAtomListener : CriWare.CriMonoBehaviour
    {
        // Fields
        private CriWare.CriAtomEx3dListener <nativeListener>k__BackingField;  // 0x28
        private CriWare.CriAtomRegion regionOnStart;  // 0x30
        public System.Boolean activateListenerOnEnable;  // 0x38
        private static System.Collections.Generic.List<CriWare.CriAtomListener> listenersList;  // static @ 0x0
        private static CriWare.CriAtomEx3dListener dummyNativeListener;  // static @ 0x8
        private UnityEngine.Vector3 lastPosition;  // 0x3c
        private CriWare.CriAtomRegion currentRegion;  // 0x48
        private System.Boolean _isActive;  // 0x50

        // Properties
        CriWare.CriAtomEx3dListener nativeListener { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        System.Boolean isActive { get; /* RVA: 0x04D86800 */ set; /* RVA: 0x068E1E1C */ }
        CriWare.CriAtomRegion region3d { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x068E1E7C */ }

        // Methods
        // RVA: 0x03A07A80  token: 0x600009A
        public static System.Void CreateDummyNativeListener() { }
        // RVA: 0x068E19FC  token: 0x600009B
        public static System.Void DestroyDummyNativeListener() { }
        // RVA: 0x068E18D0  token: 0x60000A2
        private System.Void Awake() { }
        // RVA: 0x068E1BBC  token: 0x60000A3
        private System.Void Start() { }
        // RVA: 0x068E1B98  token: 0x60000A4
        protected virtual System.Void OnEnable() { }
        // RVA: 0x068E1B74  token: 0x60000A5
        protected virtual System.Void OnDisable() { }
        // RVA: 0x068E1A98  token: 0x60000A6
        private System.Void OnDestroy() { }
        // RVA: 0x041E1670  token: 0x60000A7
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x068E19EC  token: 0x60000A8
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: 0x068E1C20  token: 0x60000A9
        private System.Void UpdatePosition() { }
        // RVA: 0x068E172C  token: 0x60000AA
        public System.Void ActivateListener(System.Boolean exclusive) { }
        // RVA: 0x068E1E10  token: 0x60000AB
        public System.Void .ctor() { }
        // RVA: 0x04D18690  token: 0x60000AC
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000015  // size: 0x38
    public class CriAtomOutputDeviceObserver : CriWare.CriMonoBehaviour
    {
        // Fields
        private System.Boolean dontDestroyOnLoad;  // 0x28
        private System.Boolean lastIsConnected;  // 0x29
        private System.Boolean isConnected;  // 0x2a
        private CriWare.CriAtomOutputDeviceObserver.OutputDeviceType lastDeviceType;  // 0x2c
        private CriWare.CriAtomOutputDeviceObserver.OutputDeviceType deviceType;  // 0x30
        private static CriWare.CriAtomOutputDeviceObserver instance;  // static @ 0x0
        private static CriWare.CriAtomOutputDeviceObserver.DeviceConnectionChangeCallback _onDeviceConnectionChanged;  // static @ 0x8

        // Properties
        System.Boolean IsDeviceConnected { get; /* RVA: 0x068E2454 */ }
        CriWare.CriAtomOutputDeviceObserver.OutputDeviceType DeviceType { get; /* RVA: 0x068E23F4 */ }

        // Events
        event CriWare.CriAtomOutputDeviceObserver.DeviceConnectionChangeCallback OnDeviceConnectionChanged;
        event CriWare.CriAtomOutputDeviceObserver.DeviceConnectionChangeCallback _onDeviceConnectionChanged;

        // Methods
        // RVA: 0x068E2038  token: 0x60000B3
        private System.Void Awake() { }
        // RVA: 0x068E2210  token: 0x60000B4
        private System.Void OnDestroy() { }
        // RVA: 0x068E218C  token: 0x60000B5
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x041E1670  token: 0x60000B6
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: 0x046D88E0  token: 0x60000B7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x48
    public class CriAtomRegion : CriWare.CriMonoBehaviour
    {
        // Fields
        private CriWare.CriAtomEx3dRegion <region3dHn>k__BackingField;  // 0x28
        private System.Collections.Generic.List<CriWare.CriAtomSourceBase> referringSources;  // 0x30
        private System.Collections.Generic.List<CriWare.CriAtomListener> referringListeners;  // 0x38
        private System.Collections.Generic.List<CriWare.CriAtomTransceiver> referringTransceivers;  // 0x40

        // Properties
        CriWare.CriAtomEx3dRegion region3dHn { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }

        // Methods
        // RVA: 0x068E330C  token: 0x60000BE
        private System.Void Awake() { }
        // RVA: 0x068E35B4  token: 0x60000BF
        protected virtual System.Void OnEnable() { }
        // RVA: 0x068E35AC  token: 0x60000C0
        private System.Void OnDestroy() { }
        // RVA: 0x068E3528  token: 0x60000C1
        protected virtual System.Void InternalInitialize() { }
        // RVA: 0x068E3370  token: 0x60000C2
        protected virtual System.Void InternalFinalize() { }
        // RVA: 0x068E3314  token: 0x60000C3
        protected virtual System.Void InitializeParameters() { }
        // RVA: 0x041E1670  token: 0x60000C4
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x041E1670  token: 0x60000C5
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: 0x068E35D4  token: 0x60000C6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x38
    public class CriAtomServer : CriWare.CriMonoBehaviour
    {
        // Fields
        private static CriWare.CriAtomServer _instance;  // static @ 0x0
        public System.Action<System.Boolean> onApplicationPausePreProcess;  // 0x28
        public System.Action<System.Boolean> onApplicationPausePostProcess;  // 0x30
        public static System.Boolean KeepPlayingSoundOnPause;  // static @ 0x8
        public static System.Boolean EnableAutoConsumePcmOutput;  // static @ 0x9

        // Properties
        CriWare.CriAtomServer instance { get; /* RVA: 0x068E38F4 */ }

        // Methods
        // RVA: 0x03A06E50  token: 0x60000C8
        public static System.Void CreateInstance() { }
        // RVA: 0x068E36E8  token: 0x60000C9
        public static System.Void DestroyInstance() { }
        // RVA: 0x04CE6640  token: 0x60000CA
        private System.Void Awake() { }
        // RVA: 0x03A06E40  token: 0x60000CB
        protected virtual System.Void OnEnable() { }
        // RVA: 0x068E3794  token: 0x60000CC
        protected virtual System.Void OnDisable() { }
        // RVA: 0x03C8D4A0  token: 0x60000CD
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x041E1670  token: 0x60000CE
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: 0x041E1670  token: 0x60000CF
        private System.Void ConsumePcmOutput() { }
        // RVA: 0x068E378C  token: 0x60000D0
        private System.Void OnApplicationPause(System.Boolean appPause) { }
        // RVA: 0x068E384C  token: 0x60000D1
        private System.Void ProcessApplicationPause(System.Boolean appPause) { }
        // RVA: 0x046D88E0  token: 0x60000D2
        public System.Void .ctor() { }
        // RVA: 0x04D360B0  token: 0x60000D3
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001B  // size: 0xB0
    public class CriAtomSource : CriWare.CriAtomSourceBase
    {
        // Fields
        private System.String _cueName;  // 0xa0
        private System.String _cueSheet;  // 0xa8

        // Properties
        System.String cueName { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x05391334 */ }
        System.String cueSheet { get; /* RVA: 0x04D86290 */ set; /* RVA: 0x04D84CC0 */ }

        // Methods
        // RVA: 0x068E49A0  token: 0x60000D8
        public virtual CriWare.CriAtomExPlayback Play() { }
        // RVA: 0x068E48C0  token: 0x60000D9
        protected virtual CriWare.CriAtomExAcb GetAcb() { }
        // RVA: 0x068E4960  token: 0x60000DA
        protected virtual System.Void PlayOnStart() { }
        // RVA: 0x068E48EC  token: 0x60000DB
        private System.Collections.IEnumerator PlayAsync(System.String cueName) { }
        // RVA: 0x068E49B0  token: 0x60000DC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001D  // size: 0xA0
    public abstract class CriAtomSourceBase : CriWare.CriMonoBehaviour
    {
        // Fields
        private CriWare.CriAtomExPlayer <player>k__BackingField;  // 0x28
        private CriWare.CriAtomEx3dSource <source>k__BackingField;  // 0x30
        protected System.Boolean initialized;  // 0x38
        protected UnityEngine.Vector3 lastPosition;  // 0x3c
        protected System.Boolean hasValidPosition;  // 0x48
        private CriWare.CriAtomRegion currentRegion;  // 0x50
        private CriWare.CriAtomListener currentListener;  // 0x58
        private System.Boolean _playOnStart;  // 0x60
        private CriWare.CriAtomRegion _regionOnStart;  // 0x68
        private CriWare.CriAtomListener _listenerOnStart;  // 0x70
        private System.Boolean _use3dPositioning;  // 0x78
        private System.Boolean _freezeOrientation;  // 0x79
        private System.Boolean _loop;  // 0x7a
        private System.Single _volume;  // 0x7c
        private System.Single _pitch;  // 0x80
        private System.Boolean _androidUseLowLatencyVoicePool;  // 0x84
        private System.Boolean need_to_player_update_all;  // 0x85
        private System.Boolean _use3dRandomization;  // 0x86
        private System.UInt32 _randomPositionListMaxLength;  // 0x88
        private CriWare.CriAtomEx.Randomize3dConfig randomize3dConfig;  // 0x90

        // Properties
        CriWare.CriAtomExPlayer player { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        CriWare.CriAtomEx3dSource source { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        System.Boolean playOnStart { get; /* RVA: 0x04D88340 */ set; /* RVA: 0x04D88350 */ }
        System.Boolean use3dPositioning { get; /* RVA: 0x04D86F90 */ set; /* RVA: 0x068E4784 */ }
        System.Boolean freezeOrientation { get; /* RVA: 0x04D88E40 */ set; /* RVA: 0x04D88E90 */ }
        System.Boolean use3dRandomization { get; /* RVA: 0x04D88E70 */ set; /* RVA: 0x068E47BC */ }
        System.UInt32 randomPositionListMaxLength { get; /* RVA: 0x04D86890 */ set; /* RVA: 0x068E44F4 */ }
        CriWare.CriAtomRegion region3d { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x068E4564 */ }
        CriWare.CriAtomListener listener { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x068E4398 */ }
        CriWare.CriAtomRegion regionOnStart { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }
        CriWare.CriAtomListener listenerOnStart { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }
        System.Boolean loop { get; /* RVA: 0x04D88E50 */ set; /* RVA: 0x04D88EA0 */ }
        System.Single volume { get; /* RVA: 0x04D88E80 */ set; /* RVA: 0x068E488C */ }
        System.Single pitch { get; /* RVA: 0x04D88E60 */ set; /* RVA: 0x068E44BC */ }
        System.Single pan3dAngle { get; /* RVA: 0x068E4298 */ set; /* RVA: 0x068E445C */ }
        System.Single pan3dDistance { get; /* RVA: 0x068E42C0 */ set; /* RVA: 0x068E448C */ }
        System.Int32 startTime { get; /* RVA: 0x068E42E8 */ set; /* RVA: 0x068E4754 */ }
        System.Int64 time { get; /* RVA: 0x068E4338 */ }
        CriWare.CriAtomSourceBase.Status status { get; /* RVA: 0x068E4310 */ }
        System.Boolean attenuationDistanceSetting { get; /* RVA: 0x068E4274 */ set; /* RVA: 0x068E435C */ }
        System.Boolean androidUseLowLatencyVoicePool { get; /* RVA: 0x02A80FA0 */ set; /* RVA: 0x02A80FB0 */ }

        // Methods
        // RVA: 0x04D88E30  token: 0x600010B
        protected System.Void SetNeedToPlayerUpdateAll() { }
        // RVA: 0x068E3A84  token: 0x600010C
        protected virtual System.Void InternalInitialize() { }
        // RVA: 0x068E39EC  token: 0x600010D
        protected virtual System.Void InternalFinalize() { }
        // RVA: 0x068E397C  token: 0x600010E
        private System.Void Awake() { }
        // RVA: 0x068E3BE0  token: 0x600010F
        protected virtual System.Void OnEnable() { }
        // RVA: 0x068E3BD8  token: 0x6000110
        private System.Void OnDestroy() { }
        // RVA: 0x068E3F08  token: 0x6000111
        protected System.Boolean SetInitialSourcePosition() { }
        // RVA: 0x068E3E50  token: 0x6000112
        protected virtual System.Void SetInitialParameters() { }
        // RVA: 0x068E4050  token: 0x6000113
        protected virtual System.Void UpdatePosition() { }
        // RVA: 0x068E3F90  token: 0x6000114
        private System.Void Start() { }
        // RVA: 0x041E1670  token: 0x6000115
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x068E3984  token: 0x6000116
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: -1  // abstract  token: 0x6000117
        public virtual CriWare.CriAtomExPlayback Play() { }
        // RVA: -1  // abstract  token: 0x6000118
        protected virtual CriWare.CriAtomExAcb GetAcb() { }
        // RVA: -1  // abstract  token: 0x6000119
        protected virtual System.Void PlayOnStart() { }
        // RVA: 0x068E3C3C  token: 0x600011A
        public CriWare.CriAtomExPlayback Play(System.String cueName) { }
        // RVA: 0x068E3CB0  token: 0x600011B
        public CriWare.CriAtomExPlayback Play(System.Int32 cueId) { }
        // RVA: 0x068E3B5C  token: 0x600011C
        protected CriWare.CriAtomExPlayback InternalPlayCue() { }
        // RVA: 0x068E403C  token: 0x600011D
        public System.Void Stop() { }
        // RVA: 0x068E3C0C  token: 0x600011E
        public System.Void Pause(System.Boolean sw) { }
        // RVA: 0x068E3BB4  token: 0x600011F
        public System.Boolean IsPaused() { }
        // RVA: 0x068E3E20  token: 0x6000120
        public System.Void SetBusSendLevel(System.String busName, System.Single level) { }
        // RVA: 0x068E3DF0  token: 0x6000121
        public System.Void SetBusSendLevel(System.Int32 busId, System.Single level) { }
        // RVA: 0x068E3DC0  token: 0x6000122
        public System.Void SetBusSendLevelOffset(System.String busName, System.Single levelOffset) { }
        // RVA: 0x068E3D90  token: 0x6000123
        public System.Void SetBusSendLevelOffset(System.Int32 busId, System.Single levelOffset) { }
        // RVA: 0x068E3D20  token: 0x6000124
        public System.Void SetAisacControl(System.String controlName, System.Single value) { }
        // RVA: 0x068E3D88  token: 0x6000125
        public System.Void SetAisac(System.String controlName, System.Single value) { }
        // RVA: 0x068E3D50  token: 0x6000126
        public System.Void SetAisacControl(System.UInt32 controlId, System.Single value) { }
        // RVA: 0x068E3D80  token: 0x6000127
        public System.Void SetAisac(System.UInt32 controlId, System.Single value) { }
        // RVA: 0x068E3948  token: 0x6000128
        public System.Void AttachToAnalyzer(CriWare.CriAtomExOutputAnalyzer analyzer) { }
        // RVA: 0x068E39CC  token: 0x6000129
        public System.Void DetachFromAnalyzer(CriWare.CriAtomExOutputAnalyzer analyzer) { }
        // RVA: 0x068E4218  token: 0x600012A
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0xE0
    public class CriAtomTransceiver : CriWare.CriMonoBehaviour
    {
        // Fields
        private CriWare.CriAtomEx3dTransceiver <transceiverHn>k__BackingField;  // 0x28
        private UnityEngine.Vector3 <inputPos>k__BackingField;  // 0x30
        private UnityEngine.Vector3 <inputFront>k__BackingField;  // 0x3c
        private UnityEngine.Vector3 <inputUp>k__BackingField;  // 0x48
        private CriWare.CriAtomRegion regionOnStart;  // 0x58
        private System.Boolean useDedicatedInput;  // 0x60
        private UnityEngine.GameObject dedicatedInput;  // 0x68
        private System.Single outputVolume;  // 0x70
        private System.Single directAudioRadius;  // 0x74
        private System.Single crossFadeDistance;  // 0x78
        private System.Single coneInsideAngle;  // 0x7c
        private System.Single coneOutsideAngle;  // 0x80
        private System.Single coneOutsideVolume;  // 0x84
        private System.Single transceiverRadius;  // 0x88
        private System.Single interiorDistance;  // 0x8c
        public System.Single minAttenuation;  // 0x90
        public System.Single maxAttenuation;  // 0x94
        private System.String globalAisacName;  // 0x98
        private System.Single maxAngleAisacDelta;  // 0xa0
        private System.String distanceAisacControlId;  // 0xa8
        private System.String listenerAzimuthAisacControlId;  // 0xb0
        private System.String listenerElevationAisacControlId;  // 0xb8
        private System.String outputAzimuthAisacControlId;  // 0xc0
        private System.String outputElevationAisacControlId;  // 0xc8
        public System.Boolean inspectorAisacSettingFoldout;  // 0xd0
        private System.Boolean isInitialized;  // 0xd1
        private System.Boolean dedicatedInputNotSetWarned;  // 0xd2
        private CriWare.CriAtomRegion currentRegion;  // 0xd8

        // Properties
        CriWare.CriAtomEx3dTransceiver transceiverHn { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        UnityEngine.Vector3 inputPos { get; /* RVA: 0x04D88ED0 */ set; /* RVA: 0x04D88F10 */ }
        UnityEngine.Vector3 inputFront { get; /* RVA: 0x04D88EB0 */ set; /* RVA: 0x04D88F00 */ }
        UnityEngine.Vector3 inputUp { get; /* RVA: 0x04D87680 */ set; /* RVA: 0x04D876A0 */ }
        CriWare.CriAtomRegion region3d { get; /* RVA: 0x04D88EF0 */ set; /* RVA: 0x068E54DC */ }

        // Methods
        // RVA: 0x068E5070  token: 0x6000135
        private System.Void Awake() { }
        // RVA: 0x068E5220  token: 0x6000136
        private System.Void Start() { }
        // RVA: 0x068E5200  token: 0x6000137
        protected virtual System.Void OnEnable() { }
        // RVA: 0x068E51F8  token: 0x6000138
        private System.Void OnDestroy() { }
        // RVA: 0x068E516C  token: 0x6000139
        protected virtual System.Void InternalInitialize() { }
        // RVA: 0x068E50F4  token: 0x600013A
        protected virtual System.Void InternalFinalize() { }
        // RVA: 0x068E5080  token: 0x600013B
        protected virtual System.Void InitializeParameters() { }
        // RVA: 0x041E1670  token: 0x600013C
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x068E5078  token: 0x600013D
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: 0x068E4A18  token: 0x600013E
        private System.Void ApplyCurrentPosition() { }
        // RVA: 0x068E4D50  token: 0x600013F
        private System.Void ApplyParameters() { }
        // RVA: 0x068E5284  token: 0x6000140
        private System.Void TrySetAisacControlId(System.String strId, CriWare.CriAtomTransceiver.SetControlIdMethod callback) { }
        // RVA: 0x068E5388  token: 0x6000141
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000021
    public class CriStructMemory`1 : System.IDisposable
    {
        // Fields
        private System.Byte[] <bytes>k__BackingField;  // 0x0
        private System.Runtime.InteropServices.GCHandle gch;  // 0x0

        // Properties
        System.Byte[] bytes { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.IntPtr ptr { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000149
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600014A
        public System.Void .ctor(System.Int32 num) { }
        // RVA: -1  // not resolved  token: 0x600014B
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x2000022  // size: 0x10
    public static class CriAtomEx
    {
        // Fields
        public static System.UInt32 InvalidAisacControlId;  // const
        public static readonly System.Collections.Generic.Dictionary<CriWare.CriAtomEx.Randomize3dCalcType,CriWare.CriAtomEx.Randomize3dParamType[]> randomize3dParamTable;  // static @ 0x0

        // Events
        event CriWare.CriAtomEx.CueLinkCbFunc OnCueLinkCallback;

        // Methods
        // RVA: 0x068E0694  token: 0x600014C
        public static System.Void SetSpeakerAngle(CriWare.CriAtomEx.SpeakerAngles6ch speakerAngle) { }
        // RVA: 0x068E06E4  token: 0x600014D
        public static System.Void SetSpeakerAngle(CriWare.CriAtomEx.SpeakerAngles8ch speakerAngle) { }
        // RVA: 0x068E0734  token: 0x600014E
        public static System.Void SetVirtualSpeakerAngle(CriWare.CriAtomEx.SpeakerAngles6ch speakerAngle) { }
        // RVA: 0x068E0784  token: 0x600014F
        public static System.Void SetVirtualSpeakerAngle(CriWare.CriAtomEx.SpeakerAngles8ch speakerAngle) { }
        // RVA: 0x068DFD7C  token: 0x6000150
        public static System.Void ControlVirtualSpeakerSetting(System.Boolean sw) { }
        // RVA: 0x068E0334  token: 0x6000153
        public static System.Void RegisterAcf(CriWare.CriFsBinder binder, System.String acfPath) { }
        // RVA: 0x068E0398  token: 0x6000154
        public static System.Void RegisterAcf(System.IntPtr acfData, System.Int32 dataSize) { }
        // RVA: 0x068E02D8  token: 0x6000155
        public static System.Void RegisterAcf(System.Byte[] acfData) { }
        // RVA: 0x068E0814  token: 0x6000156
        public static System.Void UnregisterAcf() { }
        // RVA: 0x068DFE54  token: 0x6000157
        public static System.String GetAppliedDspBusSnapshotName() { }
        // RVA: 0x068DFD2C  token: 0x6000158
        public static System.Void AttachDspBusSetting(System.String settingName) { }
        // RVA: 0x068DFDC8  token: 0x6000159
        public static System.Void DetachDspBusSetting() { }
        // RVA: 0x068DFCD4  token: 0x600015A
        public static System.Void ApplyDspBusSnapshot(System.String snapshot_name, System.Int32 time_ms) { }
        // RVA: 0x068E01CC  token: 0x600015B
        public static System.Int32 GetNumGameVariables() { }
        // RVA: 0x068DFF28  token: 0x600015C
        public static System.Boolean GetGameVariableInfo(System.UInt16 index, CriWare.CriAtomEx.GameVariableInfo& info) { }
        // RVA: 0x068E0144  token: 0x600015D
        public static System.Single GetGameVariable(System.UInt32 game_variable_id) { }
        // RVA: 0x068E00F8  token: 0x600015E
        public static System.Single GetGameVariable(System.String game_variable_name) { }
        // RVA: 0x068E048C  token: 0x600015F
        public static System.Void SetGameVariable(System.UInt32 game_variable_id, System.Single game_variable_value) { }
        // RVA: 0x068E0430  token: 0x6000160
        public static System.Void SetGameVariable(System.String game_variable_name, System.Single game_variable_value) { }
        // RVA: 0x03A067D0  token: 0x6000161
        public static System.Void SetRandomSeed(System.UInt32 seed) { }
        // RVA: 0x068E03F0  token: 0x6000162
        public static System.Void ResetPerformanceMonitor() { }
        // RVA: 0x068E020C  token: 0x6000163
        public static System.Void GetPerformanceInfo(CriWare.CriAtomEx.PerformanceInfo& info) { }
        // RVA: 0x068E04E4  token: 0x6000164
        public static System.Void SetGlobalLabelToSelectorByIndex(System.UInt16 selector_index, System.UInt16 label_index) { }
        // RVA: 0x068E0540  token: 0x6000165
        public static System.Void SetGlobalLabelToSelectorByName(System.String selector_name, System.String label_name) { }
        // RVA: 0x068E0298  token: 0x6000166
        public static System.Void Lock() { }
        // RVA: 0x068E07D4  token: 0x6000167
        public static System.Void Unlock() { }
        // RVA: 0x068E059C  token: 0x6000168
        public static System.Void SetOutputAudioDevice_PC(System.String deviceId) { }
        // RVA: 0x068E0258  token: 0x6000169
        public static System.Boolean LoadAudioDeviceList_PC() { }
        // RVA: 0x068E018C  token: 0x600016A
        public static System.Int32 GetNumAudioDevices_PC() { }
        // RVA: 0x068DFEBC  token: 0x600016B
        public static System.String GetAudioDeviceName_PC(System.Int32 index) { }
        // RVA: 0x068E0644  token: 0x600016C
        public static System.Void SetOutputAudioDevice_PC(System.Int32 index) { }
        // RVA: 0x041E1670  token: 0x600016D
        public static System.Void SetOutputVolume_VITA(System.Single volume) { }
        // RVA: 0x02FFF600  token: 0x600016E
        public static System.Boolean IsBgmPortAcquired_VITA() { }
        // RVA: 0x012081B0  token: 0x600016F
        public static System.Boolean IsSoundStopped_IOS() { }
        // RVA: 0x041E1670  token: 0x6000170
        public static System.Void EnableAudioSessionRestoration_IOS(System.Boolean flag) { }
        // RVA: 0x068DFE08  token: 0x6000171
        public static System.Void EnableBusSendOffsetWhenLevelNotExist(System.Boolean enable) { }
        // RVA: 0x068E0F34  token: 0x6000172
        private static System.Boolean criAtomEx_RegisterAcfFile(System.IntPtr binder, System.String path, System.IntPtr work, System.Int32 workSize) { }
        // RVA: 0x068E0DE8  token: 0x6000173
        private static System.Void criAtomEx_RegisterAcfData(System.IntPtr acfData, System.Int32 acfDataSize, System.IntPtr work, System.Int32 workSize) { }
        // RVA: 0x068E0E88  token: 0x6000174
        private static System.Void criAtomEx_RegisterAcfData(System.Byte[] acfData, System.Int32 acfDataSize, System.IntPtr work, System.Int32 workSize) { }
        // RVA: 0x068E1304  token: 0x6000175
        private static System.Void criAtomEx_UnregisterAcf() { }
        // RVA: 0x068E09E0  token: 0x6000176
        private static System.Void criAtomEx_AttachDspBusSetting(System.String settingName, System.IntPtr work, System.Int32 workSize) { }
        // RVA: 0x068E0B00  token: 0x6000177
        private static System.Void criAtomEx_DetachDspBusSetting() { }
        // RVA: 0x068E098C  token: 0x6000178
        private static System.Void criAtomEx_ApplyDspBusSnapshot(System.String snapshot_name, System.Int32 time_ms) { }
        // RVA: 0x068E0BE0  token: 0x6000179
        private static System.IntPtr criAtomEx_GetAppliedDspBusSnapshotName() { }
        // RVA: 0x068E0D54  token: 0x600017A
        private static System.Int32 criAtomEx_GetNumGameVariables() { }
        // RVA: 0x068E0CC8  token: 0x600017B
        private static System.Boolean criAtomEx_GetGameVariableInfo(System.UInt16 index, System.IntPtr game_variable_info) { }
        // RVA: 0x068E0C48  token: 0x600017C
        private static System.Single criAtomEx_GetGameVariableById(System.UInt32 game_variable_id) { }
        // RVA: 0x068E0C74  token: 0x600017D
        private static System.Single criAtomEx_GetGameVariableByName(System.String game_variable_name) { }
        // RVA: 0x068E0FF8  token: 0x600017E
        private static System.Void criAtomEx_SetGameVariableById(System.UInt32 game_variable_id, System.Single game_variable_value) { }
        // RVA: 0x068E1034  token: 0x600017F
        private static System.Void criAtomEx_SetGameVariableByName(System.String game_variable_name, System.Single game_variable_value) { }
        // RVA: 0x03827880  token: 0x6000180
        private static System.Void criAtomEx_SetRandomSeed(System.UInt32 seed) { }
        // RVA: 0x068E0D80  token: 0x6000181
        private static System.Void criAtomEx_Lock() { }
        // RVA: 0x068E129C  token: 0x6000182
        private static System.Void criAtomEx_Unlock() { }
        // RVA: 0x068E15AC  token: 0x6000183
        private static System.Void criAtom_ResetPerformanceMonitor() { }
        // RVA: 0x068E1534  token: 0x6000184
        private static System.Void criAtom_GetPerformanceInfo(CriWare.CriAtomEx.PerformanceInfo& info) { }
        // RVA: 0x068E085C  token: 0x6000185
        private static System.Void criAtomExAcf_SetGlobalLabelToSelectorByIndex(System.UInt16 selector_index, System.UInt16 label_index) { }
        // RVA: 0x068E08E0  token: 0x6000186
        private static System.Void criAtomExAcf_SetGlobalLabelToSelectorByName(System.String selector_name, System.String label_name) { }
        // RVA: 0x068E108C  token: 0x6000187
        private static System.Void criAtomEx_SetSpeakerAngleArray(CriWare.CriAtomEx.SpeakerSystem speaker_system, CriWare.CriAtomEx.SpeakerAngles6ch& angle_array) { }
        // RVA: 0x068E1110  token: 0x6000188
        private static System.Void criAtomEx_SetSpeakerAngleArray(CriWare.CriAtomEx.SpeakerSystem speaker_system, CriWare.CriAtomEx.SpeakerAngles8ch& angle_array) { }
        // RVA: 0x068E1194  token: 0x6000189
        private static System.Void criAtomEx_SetVirtualSpeakerAngleArray(CriWare.CriAtomEx.SpeakerSystem speaker_system, CriWare.CriAtomEx.SpeakerAngles6ch& angle_array) { }
        // RVA: 0x068E1218  token: 0x600018A
        private static System.Void criAtomEx_SetVirtualSpeakerAngleArray(CriWare.CriAtomEx.SpeakerSystem speaker_system, CriWare.CriAtomEx.SpeakerAngles8ch& angle_array) { }
        // RVA: 0x068E0A88  token: 0x600018B
        private static System.Void criAtomEx_ControlVirtualSpeakerSetting(System.Boolean sw) { }
        // RVA: 0x068E0B68  token: 0x600018C
        private static System.Void criAtomEx_EnableBusSendOffsetWhenLevelNotExist(System.Boolean enable) { }
        // RVA: 0x068E1698  token: 0x600018D
        private static System.Void criAtom_SetDeviceId_WASAPI(CriWare.CriAtomEx.SoundRendererType soundRendererType, System.String deviceId) { }
        // RVA: 0x068E1614  token: 0x600018E
        private static System.Void criAtom_SetDeviceId_WASAPI(CriWare.CriAtomEx.SoundRendererType type, System.IntPtr deviceId) { }
        // RVA: 0x068E14C4  token: 0x600018F
        private static System.Boolean criAtomUnity_LoadAudioDeviceList_PC() { }
        // RVA: 0x068E145C  token: 0x6000190
        private static System.Int32 criAtomUnity_GetNumAudioDevices_PC() { }
        // RVA: 0x068E13E4  token: 0x6000191
        private static System.IntPtr criAtomUnity_GetAudioDeviceName_PC(System.Int32 index) { }
        // RVA: 0x068E136C  token: 0x6000192
        private static System.IntPtr criAtomUnity_GetAudioDeviceId_PC(System.Int32 index) { }
        // RVA: 0x0488E700  token: 0x6000193
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000040  // size: 0x10
    public static class CriAtomExCategory
    {
        // Methods
        // RVA: 0x068DC850  token: 0x60001AE
        public static System.Void SetVolume(System.String name, System.Single volume) { }
        // RVA: 0x068DC848  token: 0x60001AF
        public static System.Void SetVolume(System.Int32 id, System.Single volume) { }
        // RVA: 0x068DC7C4  token: 0x60001B0
        public static System.Single GetVolume(System.String name) { }
        // RVA: 0x068DC7CC  token: 0x60001B1
        public static System.Single GetVolume(System.Int32 id) { }
        // RVA: 0x068DC804  token: 0x60001B2
        public static System.Void Mute(System.String name, System.Boolean mute) { }
        // RVA: 0x068DC80C  token: 0x60001B3
        public static System.Void Mute(System.Int32 id, System.Boolean mute) { }
        // RVA: 0x068DC7DC  token: 0x60001B4
        public static System.Boolean IsMuted(System.String name) { }
        // RVA: 0x068DC7D4  token: 0x60001B5
        public static System.Boolean IsMuted(System.Int32 id) { }
        // RVA: 0x068DC860  token: 0x60001B6
        public static System.Void Solo(System.String name, System.Boolean solo, System.Single muteVolume) { }
        // RVA: 0x068DC858  token: 0x60001B7
        public static System.Void Solo(System.Int32 id, System.Boolean solo, System.Single muteVolume) { }
        // RVA: 0x068DC7FC  token: 0x60001B8
        public static System.Boolean IsSoloed(System.String name) { }
        // RVA: 0x068DC7F4  token: 0x60001B9
        public static System.Boolean IsSoloed(System.Int32 id) { }
        // RVA: 0x068DC814  token: 0x60001BA
        public static System.Void Pause(System.String name, System.Boolean pause) { }
        // RVA: 0x068DC81C  token: 0x60001BB
        public static System.Void Pause(System.Int32 id, System.Boolean pause) { }
        // RVA: 0x068DC7E4  token: 0x60001BC
        public static System.Boolean IsPaused(System.String name) { }
        // RVA: 0x068DC7EC  token: 0x60001BD
        public static System.Boolean IsPaused(System.Int32 id) { }
        // RVA: 0x068DC82C  token: 0x60001BE
        public static System.Void SetAisacControl(System.String name, System.String controlName, System.Single value) { }
        // RVA: 0x068DC82C  token: 0x60001BF
        public static System.Void SetAisac(System.String name, System.String controlName, System.Single value) { }
        // RVA: 0x068DC824  token: 0x60001C0
        public static System.Void SetAisacControl(System.Int32 id, System.Int32 controlId, System.Single value) { }
        // RVA: 0x068DC824  token: 0x60001C1
        public static System.Void SetAisac(System.Int32 id, System.Int32 controlId, System.Single value) { }
        // RVA: 0x068DC834  token: 0x60001C2
        public static System.Void SetReactParameter(System.String name, CriWare.CriAtomExCategory.ReactParameter parameter) { }
        // RVA: 0x068DC7B4  token: 0x60001C3
        public static System.Boolean GetReactParameter(System.String name, CriWare.CriAtomExCategory.ReactParameter& parameter) { }
        // RVA: 0x068DC3C8  token: 0x60001C4
        public static System.Boolean GetAttachedAisacInfoById(System.UInt32 id, System.Int32 aisacAttachedIndex, CriWare.CriAtomEx.AisacInfo& aisacInfo) { }
        // RVA: 0x068DC5B4  token: 0x60001C5
        public static System.Boolean GetAttachedAisacInfoByName(System.String name, System.Int32 aisacAttachedIndex, CriWare.CriAtomEx.AisacInfo& aisacInfo) { }
        // RVA: 0x068DC7AC  token: 0x60001C6
        public static System.Boolean GetCurrentAisacControlValue(System.String categoryName, System.String aisacControlName, System.Single& controlValue) { }
        // RVA: 0x068DC7BC  token: 0x60001C7
        public static CriWare.CriAtomExCategory.ReactStatus GetReactStatus(System.String reactName) { }
        // RVA: 0x068DD2D0  token: 0x60001C8
        private static System.Void criAtomExCategory_SetVolumeByName(System.String name, System.Single volume) { }
        // RVA: 0x068DCC1C  token: 0x60001C9
        private static System.Single criAtomExCategory_GetVolumeByName(System.String name) { }
        // RVA: 0x068DD294  token: 0x60001CA
        private static System.Void criAtomExCategory_SetVolumeById(System.Int32 id, System.Single volume) { }
        // RVA: 0x068DCBF0  token: 0x60001CB
        private static System.Single criAtomExCategory_GetVolumeById(System.Int32 id) { }
        // RVA: 0x068DCEB0  token: 0x60001CC
        private static System.Void criAtomExCategory_MuteById(System.Int32 id, System.Boolean mute) { }
        // RVA: 0x068DCC70  token: 0x60001CD
        private static System.Boolean criAtomExCategory_IsMutedById(System.Int32 id) { }
        // RVA: 0x068DCF34  token: 0x60001CE
        private static System.Void criAtomExCategory_MuteByName(System.String name, System.Boolean mute) { }
        // RVA: 0x068DCCE8  token: 0x60001CF
        private static System.Boolean criAtomExCategory_IsMutedByName(System.String name) { }
        // RVA: 0x068DD320  token: 0x60001D0
        private static System.Void criAtomExCategory_SoloById(System.Int32 id, System.Boolean solo, System.Single volume) { }
        // RVA: 0x068DCE1C  token: 0x60001D1
        private static System.Boolean criAtomExCategory_IsSoloedById(System.Int32 id) { }
        // RVA: 0x068DD368  token: 0x60001D2
        private static System.Void criAtomExCategory_SoloByName(System.String name, System.Boolean solo, System.Single volume) { }
        // RVA: 0x068DCE54  token: 0x60001D3
        private static System.Boolean criAtomExCategory_IsSoloedByName(System.String name) { }
        // RVA: 0x068DCFCC  token: 0x60001D4
        private static System.Void criAtomExCategory_PauseById(System.Int32 id, System.Boolean pause) { }
        // RVA: 0x068DCD88  token: 0x60001D5
        private static System.Boolean criAtomExCategory_IsPausedById(System.Int32 id) { }
        // RVA: 0x068DD050  token: 0x60001D6
        private static System.Void criAtomExCategory_PauseByName(System.String name, System.Boolean pause) { }
        // RVA: 0x068DCDC0  token: 0x60001D7
        private static System.Boolean criAtomExCategory_IsPausedByName(System.String name) { }
        // RVA: 0x068DD0A8  token: 0x60001D8
        private static System.Void criAtomExCategory_SetAisacControlById(System.Int32 id, System.UInt16 controlId, System.Single value) { }
        // RVA: 0x068DD0F4  token: 0x60001D9
        private static System.Void criAtomExCategory_SetAisacControlByName(System.String name, System.String controlName, System.Single value) { }
        // RVA: 0x068DD168  token: 0x60001DA
        private static System.Void criAtomExCategory_SetReactParameter(System.String react_name, CriWare.CriAtomExCategory.ReactParameter& parameter) { }
        // RVA: 0x068DCA78  token: 0x60001DB
        private static System.Boolean criAtomExCategory_GetReactParameter(System.String react_name, CriWare.CriAtomExCategory.ReactParameter& parameter) { }
        // RVA: 0x068DC868  token: 0x60001DC
        private static System.Boolean criAtomExCategory_GetAttachedAisacInfoById(System.UInt32 id, System.Int32 aisacAttachedIndex, System.IntPtr aisacInfo) { }
        // RVA: 0x068DC8FC  token: 0x60001DD
        private static System.Boolean criAtomExCategory_GetAttachedAisacInfoByName(System.String name, System.Int32 aisacAttachedIndex, System.IntPtr aisacInfo) { }
        // RVA: 0x068DC9B0  token: 0x60001DE
        private static System.Boolean criAtomExCategory_GetCurrentAisacControlValueByName(System.String category_name, System.String aisac_control_name, System.Single& control_value) { }
        // RVA: 0x068DCB98  token: 0x60001DF
        private static CriWare.CriAtomExCategory.ReactStatus criAtomExCategory_GetReactStatus(System.String react_name) { }

    }

    // TypeToken: 0x200004E  // size: 0x10
    public static class CriAtomExSequencer
    {
        // Events
        event CriWare.CriAtomExSequencer.EventCallback OnCallback;

        // Methods
        // RVA: 0x068DFCBC  token: 0x60001E4
        public static System.Void SetEventCallback(CriWare.CriAtomExSequencer.EventCbFunc func, System.String separator) { }

    }

    // TypeToken: 0x2000052  // size: 0x10
    public static class CriAtomExBeatSync
    {
        // Events
        event CriWare.CriAtomExBeatSync.CbFunc OnCallback;

        // Methods
        // RVA: 0x068DC3B0  token: 0x60001EF
        public static System.Void SetCallback(CriWare.CriAtomExBeatSync.CbFunc func) { }

    }

    // TypeToken: 0x2000055  // size: 0x10
    public class CriAtomExAsr
    {
        // Methods
        // RVA: 0x068DB038  token: 0x60001F4
        public static System.Void AttachBusAnalyzer(System.String busName, System.Int32 interval, System.Int32 peakHoldTime) { }
        // RVA: 0x068DB00C  token: 0x60001F5
        public static System.Void AttachBusAnalyzer(System.Int32 interval, System.Int32 peakHoldTime) { }
        // RVA: 0x068DB058  token: 0x60001F6
        public static System.Void DetachBusAnalyzer(System.String busName) { }
        // RVA: 0x068DB060  token: 0x60001F7
        public static System.Void DetachBusAnalyzer() { }
        // RVA: 0x068DB23C  token: 0x60001F8
        public static System.Void GetBusAnalyzerInfo(System.String busName, CriWare.CriAtomExAsr.BusAnalyzerInfo& info) { }
        // RVA: 0x068DB080  token: 0x60001F9
        public static System.Void GetBusAnalyzerInfo(System.Int32 busId, CriWare.CriAtomExAsr.BusAnalyzerInfo& info) { }
        // RVA: 0x068DB438  token: 0x60001FA
        public static System.Void SetBusVolume(System.String busName, System.Single volume) { }
        // RVA: 0x068DB440  token: 0x60001FB
        public static System.Void SetBusVolume(System.Int32 busId, System.Single volume) { }
        // RVA: 0x068DB430  token: 0x60001FC
        public static System.Void SetBusSendLevel(System.String busName, System.String sendTo, System.Single level) { }
        // RVA: 0x068DB428  token: 0x60001FD
        public static System.Void SetBusSendLevel(System.Int32 busId, System.Int32 sendTo, System.Single level) { }
        // RVA: 0x068DB418  token: 0x60001FE
        public static System.Void SetBusMatrix(System.String busName, System.Int32 inputChannels, System.Int32 outputChannels, System.Single[] matrix) { }
        // RVA: 0x068DB408  token: 0x60001FF
        public static System.Void SetBusMatrix(System.Int32 busId, System.Int32 inputChannels, System.Int32 outputChannels, System.Single[] matrix) { }
        // RVA: 0x068DB448  token: 0x6000200
        public static System.Void SetEffectBypass(System.String busName, System.String effectName, System.Boolean bypass) { }
        // RVA: 0x068DB450  token: 0x6000201
        public static System.Void SetEffectParameter(System.String busName, System.String effectName, System.UInt32 parameterIndex, System.Single parameterValue) { }
        // RVA: 0x068DB3F8  token: 0x6000202
        public static System.Single GetEffectParameter(System.String busName, System.String effectName, System.UInt32 parameterIndex) { }
        // RVA: 0x068DB400  token: 0x6000203
        public static System.Boolean RegisterEffectInterface(System.IntPtr afx_interface) { }
        // RVA: 0x068DB488  token: 0x6000204
        public static System.Void UnregisterEffectInterface(System.IntPtr afx_interface) { }
        // RVA: 0x068DB3F0  token: 0x6000205
        public static System.Void GetBusVolume(System.String busName, System.Single& volume) { }
        // RVA: 0x011EC580  token: 0x6000206
        public static System.Int32 GetPcmOutput(System.Int32 outputChannels, System.Int32 outputSamples, System.Single[][] buffer) { }
        // RVA: 0x011EC580  token: 0x6000207
        public static System.Int32 GetNumBufferedPcmOutputSamples() { }
        // RVA: 0x041E1670  token: 0x6000208
        public static System.Void SetPcmBufferSize(System.Int32 numSamples) { }
        // RVA: 0x068DB490  token: 0x6000209
        private static System.Void criAtomExAsr_AttachBusAnalyzerByName(System.String busName, CriWare.CriAtomExAsr.BusAnalyzerConfig& config) { }
        // RVA: 0x068DB528  token: 0x600020A
        private static System.Void criAtomExAsr_AttachBusAnalyzer(System.Int32 busNo, CriWare.CriAtomExAsr.BusAnalyzerConfig& config) { }
        // RVA: 0x068DB5AC  token: 0x600020B
        private static System.Void criAtomExAsr_DetachBusAnalyzerByName(System.String busName) { }
        // RVA: 0x068DB638  token: 0x600020C
        private static System.Void criAtomExAsr_DetachBusAnalyzer(System.Int32 busNo) { }
        // RVA: 0x068DB6B0  token: 0x600020D
        private static System.Void criAtomExAsr_GetBusAnalyzerInfoByName(System.String busName, System.IntPtr info) { }
        // RVA: 0x068DB748  token: 0x600020E
        private static System.Void criAtomExAsr_GetBusAnalyzerInfo(System.Int32 busNo, System.IntPtr info) { }
        // RVA: 0x068DBB48  token: 0x600020F
        private static System.Void criAtomExAsr_SetBusVolumeByName(System.String busName, System.Single volume) { }
        // RVA: 0x068DBBA0  token: 0x6000210
        private static System.Void criAtomExAsr_SetBusVolume(System.Int32 busNo, System.Single volume) { }
        // RVA: 0x068DBA8C  token: 0x6000211
        private static System.Void criAtomExAsr_SetBusSendLevelByName(System.String busName, System.String sendtoName, System.Single level) { }
        // RVA: 0x068DBB00  token: 0x6000212
        private static System.Void criAtomExAsr_SetBusSendLevel(System.Int32 busNo, System.Int32 sendtoNo, System.Single level) { }
        // RVA: 0x068DB924  token: 0x6000213
        private static System.Void criAtomExAsr_SetBusMatrixByName(System.String busName, System.Int32 inputChannels, System.Int32 outputChannels, System.Single[] matrix) { }
        // RVA: 0x068DB9E4  token: 0x6000214
        private static System.Void criAtomExAsr_SetBusMatrix(System.Int32 busNo, System.Int32 inputChannels, System.Int32 outputChannels, System.Single[] matrix) { }
        // RVA: 0x068DBBDC  token: 0x6000215
        private static System.Void criAtomExAsr_SetEffectBypass(System.String busName, System.String effectName, System.Boolean bypass) { }
        // RVA: 0x068DBD50  token: 0x6000216
        private static System.Void criAtomExAsr_UpdateEffectParameters(System.String busName, System.String effectName) { }
        // RVA: 0x068DBC98  token: 0x6000217
        private static System.Void criAtomExAsr_SetEffectParameter(System.String busName, System.String effectName, System.UInt32 parameterIndex, System.Single parameterValue) { }
        // RVA: 0x068DB864  token: 0x6000218
        private static System.Single criAtomExAsr_GetEffectParameter(System.String busName, System.String effectName, System.UInt32 parameterIndex) { }
        // RVA: 0x068DB8EC  token: 0x6000219
        private static System.Boolean criAtomExAsr_RegisterEffectInterface(System.IntPtr afx_interface) { }
        // RVA: 0x068DBD1C  token: 0x600021A
        private static System.Void criAtomExAsr_UnregisterEffectInterface(System.IntPtr afx_interface) { }
        // RVA: 0x068DB7CC  token: 0x600021B
        private static System.Void criAtomExAsr_GetBusVolumeByName(System.String busName, System.Single& volume) { }
        // RVA: 0x041E1670  token: 0x600021C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000058  // size: 0x10
    public static class CriAtomExLatencyEstimator
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600021E
        public static System.Void InitializeModule() { }
        // RVA: 0x041E1670  token: 0x600021F
        public static System.Void FinalizeModule() { }
        // RVA: 0x04D88E20  token: 0x6000220
        public static CriWare.CriAtomExLatencyEstimator.EstimatorInfo GetCurrentInfo() { }

    }

    // TypeToken: 0x200005B  // size: 0x28
    public class CriAtomEx3dListener : CriWare.CriDisposable
    {
        // Fields
        private System.IntPtr handle;  // 0x20

        // Properties
        System.IntPtr nativeHandle { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x038280C0  token: 0x6000221
        public System.Void .ctor() { }
        // RVA: 0x068D2DA8  token: 0x6000222
        public virtual System.Void Dispose() { }
        // RVA: 0x068D2DB4  token: 0x6000223
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068D3130  token: 0x6000225
        public System.Void Update() { }
        // RVA: 0x068D2EC0  token: 0x6000226
        public System.Void ResetParameters() { }
        // RVA: 0x068D30C8  token: 0x6000227
        public System.Void SetPosition(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x068D30FC  token: 0x6000228
        public System.Void SetVelocity(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x068D306C  token: 0x6000229
        public System.Void SetOrientation(System.Single fx, System.Single fy, System.Single fz, System.Single ux, System.Single uy, System.Single uz) { }
        // RVA: 0x068D2F14  token: 0x600022A
        public System.Void SetDistanceFactor(System.Single distanceFactor) { }
        // RVA: 0x068D2FB8  token: 0x600022B
        public System.Void SetDopplerMultiplier(System.Single dopplerMultiplier) { }
        // RVA: 0x068D3038  token: 0x600022C
        public System.Void SetFocusPoint(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x068D2FA4  token: 0x600022D
        public System.Void SetDistanceFocusLevel(System.Single distanceFocusLevel) { }
        // RVA: 0x068D2F00  token: 0x600022E
        public System.Void SetDirectionFocusLevel(System.Single directionFocusLevel) { }
        // RVA: 0x068D2ED4  token: 0x600022F
        public System.Void Set3dRegion(CriWare.CriAtomEx3dRegion region3d) { }
        // RVA: 0x068D2EB4  token: 0x6000230
        public System.Boolean IsDestroyable() { }
        // RVA: 0x068D2E40  token: 0x6000231
        protected virtual System.Void Finalize() { }
        // RVA: 0x03827AB0  token: 0x6000232
        private static System.IntPtr criAtomEx3dListener_Create(CriWare.CriAtomEx3dListener.Config& config, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068D3144  token: 0x6000233
        private static System.Void criAtomEx3dListener_Destroy(System.IntPtr ex_3d_listener) { }
        // RVA: 0x068D350C  token: 0x6000234
        private static System.Void criAtomEx3dListener_Update(System.IntPtr ex_3d_listener) { }
        // RVA: 0x068D31F4  token: 0x6000235
        private static System.Void criAtomEx3dListener_ResetParameters(System.IntPtr ex_3d_listener) { }
        // RVA: 0x068D3484  token: 0x6000236
        private static System.Void criAtomEx3dListener_SetPosition(System.IntPtr ex_3d_listener, CriWare.CriAtomEx.NativeVector& position) { }
        // RVA: 0x068D34C8  token: 0x6000237
        private static System.Void criAtomEx3dListener_SetVelocity(System.IntPtr ex_3d_listener, CriWare.CriAtomEx.NativeVector& velocity) { }
        // RVA: 0x068D33F0  token: 0x6000238
        private static System.Void criAtomEx3dListener_SetOrientation(System.IntPtr ex_3d_listener, CriWare.CriAtomEx.NativeVector& front, CriWare.CriAtomEx.NativeVector& top) { }
        // RVA: 0x068D32A8  token: 0x6000239
        private static System.Void criAtomEx3dListener_SetDistanceFactor(System.IntPtr ex_3d_listener, System.Single distance_factor) { }
        // RVA: 0x068D3328  token: 0x600023A
        private static System.Void criAtomEx3dListener_SetDopplerMultiplier(System.IntPtr ex_3d_listener, System.Single doppler_multiplier) { }
        // RVA: 0x068D336C  token: 0x600023B
        private static System.Void criAtomEx3dListener_SetFocusPoint(System.IntPtr ex_3d_listener, CriWare.CriAtomEx.NativeVector& focus_point) { }
        // RVA: 0x068D32EC  token: 0x600023C
        private static System.Void criAtomEx3dListener_SetDistanceFocusLevel(System.IntPtr ex_3d_listener, System.Single distance_focus_level) { }
        // RVA: 0x068D326C  token: 0x600023D
        private static System.Void criAtomEx3dListener_SetDirectionFocusLevel(System.IntPtr ex_3d_listener, System.Single direction_focus_level) { }
        // RVA: 0x068D3178  token: 0x600023E
        private static System.Boolean criAtomEx3dListener_IsDestroyable(System.IntPtr ex_3d_listener) { }
        // RVA: 0x068D3228  token: 0x600023F
        private static System.Void criAtomEx3dListener_Set3dRegionHn(System.IntPtr ex_3d_listener, System.IntPtr ex_3d_region) { }

    }

    // TypeToken: 0x200005D  // size: 0x30
    public class CriAtomEx3dSource : CriWare.CriDisposable
    {
        // Fields
        private System.UInt32 currentRandomPositionListMaxLength;  // 0x20
        private System.IntPtr handle;  // 0x28

        // Properties
        System.IntPtr nativeHandle { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x068D3B94  token: 0x6000240
        public System.Void .ctor(System.Boolean enableVoicePriorityDecay, System.UInt32 randomPositionListMaxLength) { }
        // RVA: 0x068D36BC  token: 0x6000241
        public virtual System.Void Dispose() { }
        // RVA: 0x068D36C8  token: 0x6000242
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068D3B88  token: 0x6000244
        public System.Void Update() { }
        // RVA: 0x068D3814  token: 0x6000245
        public System.Void ResetParameters() { }
        // RVA: 0x068D392C  token: 0x6000246
        public System.Void SetPosition(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x068D3B50  token: 0x6000247
        public System.Void SetVelocity(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x068D38CC  token: 0x6000248
        public System.Void SetOrientation(UnityEngine.Vector3 front, UnityEngine.Vector3 top) { }
        // RVA: 0x068D3844  token: 0x6000249
        public System.Void SetConeOrientation(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x068D3870  token: 0x600024A
        public System.Void SetConeParameter(System.Single insideAngle, System.Single outsideAngle, System.Single outsideVolume) { }
        // RVA: 0x068D38C0  token: 0x600024B
        public System.Void SetMinMaxDistance(System.Single minDistance, System.Single maxDistance) { }
        // RVA: 0x068D389C  token: 0x600024C
        public System.Void SetInteriorPanField(System.Single sourceRadius, System.Single interiorDistance) { }
        // RVA: 0x068D3890  token: 0x600024D
        public System.Void SetDopplerFactor(System.Single dopplerFactor) { }
        // RVA: 0x068D3B7C  token: 0x600024E
        public System.Void SetVolume(System.Single volume) { }
        // RVA: 0x068D38B4  token: 0x600024F
        public System.Void SetMaxAngleAisacDelta(System.Single maxDelta) { }
        // RVA: 0x068D3838  token: 0x6000250
        public System.Void SetAttenuationDistanceSetting(System.Boolean flag) { }
        // RVA: 0x068D37C8  token: 0x6000251
        public System.Boolean GetAttenuationDistanceSetting() { }
        // RVA: 0x068D3958  token: 0x6000252
        public System.Void SetRandomPositionConfig(System.Nullable<CriWare.CriAtomEx.Randomize3dConfig> config) { }
        // RVA: 0x068D3A04  token: 0x6000253
        public System.Void SetRandomPositionList(UnityEngine.Vector3[] positionList) { }
        // RVA: 0x068D3820  token: 0x6000254
        public System.Void Set3dRegion(CriWare.CriAtomEx3dRegion region3d) { }
        // RVA: 0x068D38A8  token: 0x6000255
        public System.Void SetListenerBasedElevationAngleAisacControlId(System.UInt16 aisacControlId) { }
        // RVA: 0x068D3B44  token: 0x6000256
        public System.Void SetSourceBasedElevationAngleAisacControlId(System.UInt16 aisacControlId) { }
        // RVA: 0x068D3884  token: 0x6000257
        public System.Void SetDistanceAisacControlId(System.UInt16 aisacControlId) { }
        // RVA: 0x068D3808  token: 0x6000258
        public System.Boolean IsDestroyable() { }
        // RVA: 0x068D37D4  token: 0x6000259
        public CriWare.CriAtomEx.NativeVector GetPosition() { }
        // RVA: 0x068D3754  token: 0x600025A
        protected virtual System.Void Finalize() { }
        // RVA: 0x068D3C28  token: 0x600025B
        private static System.IntPtr criAtomEx3dSource_Create(CriWare.CriAtomEx3dSource.Config& config, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068D3CEC  token: 0x600025C
        private static System.Void criAtomEx3dSource_Destroy(System.IntPtr ex_3d_source) { }
        // RVA: 0x068D46C8  token: 0x600025D
        private static System.Void criAtomEx3dSource_Update(System.IntPtr ex_3d_source) { }
        // RVA: 0x068D3EB0  token: 0x600025E
        private static System.Void criAtomEx3dSource_ResetParameters(System.IntPtr ex_3d_source) { }
        // RVA: 0x068D4388  token: 0x600025F
        private static System.Void criAtomEx3dSource_SetPosition(System.IntPtr ex_3d_source, CriWare.CriAtomEx.NativeVector& position) { }
        // RVA: 0x068D4648  token: 0x6000260
        private static System.Void criAtomEx3dSource_SetVelocity(System.IntPtr ex_3d_source, CriWare.CriAtomEx.NativeVector& velocity) { }
        // RVA: 0x068D42F4  token: 0x6000261
        private static System.Void criAtomEx3dSource_SetOrientation(System.IntPtr ex_3d_source, CriWare.CriAtomEx.NativeVector& front, CriWare.CriAtomEx.NativeVector& top) { }
        // RVA: 0x068D3FF0  token: 0x6000262
        private static System.Void criAtomEx3dSource_SetConeOrientation(System.IntPtr ex_3d_source, CriWare.CriAtomEx.NativeVector& cone_orient) { }
        // RVA: 0x068D4074  token: 0x6000263
        private static System.Void criAtomEx3dSource_SetConeParameter(System.IntPtr ex_3d_source, System.Single inside_angle, System.Single outside_angle, System.Single outside_volume) { }
        // RVA: 0x068D42A8  token: 0x6000264
        private static System.Void criAtomEx3dSource_SetMinMaxAttenuationDistance(System.IntPtr ex_3d_source, System.Single min_distance, System.Single max_distance) { }
        // RVA: 0x068D4194  token: 0x6000265
        private static System.Void criAtomEx3dSource_SetInteriorPanField(System.IntPtr ex_3d_source, System.Single source_radius, System.Single interior_distance) { }
        // RVA: 0x068D4158  token: 0x6000266
        private static System.Void criAtomEx3dSource_SetDopplerFactor(System.IntPtr ex_3d_source, System.Single doppler_factor) { }
        // RVA: 0x068D468C  token: 0x6000267
        private static System.Void criAtomEx3dSource_SetVolume(System.IntPtr ex_3d_source, System.Single volume) { }
        // RVA: 0x068D4264  token: 0x6000268
        private static System.Void criAtomEx3dSource_SetMaxAngleAisacDelta(System.IntPtr ex_3d_source, System.Single max_delta) { }
        // RVA: 0x068D3F6C  token: 0x6000269
        private static System.Void criAtomEx3dSource_SetAttenuationDistanceSetting(System.IntPtr ex_3d_source, System.Boolean flag) { }
        // RVA: 0x068D3D20  token: 0x600026A
        private static System.Boolean criAtomEx3dSource_GetAttenuationDistanceSetting(System.IntPtr ex_3d_source) { }
        // RVA: 0x068D4450  token: 0x600026B
        private static System.Void criAtomEx3dSource_SetRandomPositionConfig(System.IntPtr ex_3d_source, CriWare.CriAtomEx.Randomize3dConfig& config) { }
        // RVA: 0x068D43CC  token: 0x600026C
        private static System.Void criAtomEx3dSource_SetRandomPositionConfig(System.IntPtr ex_3d_source, System.IntPtr config) { }
        // RVA: 0x068D4524  token: 0x600026D
        private static System.Void criAtomEx3dSource_SetRandomPositionList(System.IntPtr ex_3d_source, CriWare.CriAtomEx.NativeVector[] position_list, System.UInt32 length) { }
        // RVA: 0x068D40D4  token: 0x600026E
        private static System.Void criAtomEx3dSource_SetDistanceAisacControlId(System.IntPtr ex_3d_source, System.UInt16 aisac_control_id) { }
        // RVA: 0x068D3E34  token: 0x600026F
        private static System.Boolean criAtomEx3dSource_IsDestroyable(System.IntPtr ex_3d_source) { }
        // RVA: 0x068D3D9C  token: 0x6000270
        private static CriWare.CriAtomEx.NativeVector criAtomEx3dSource_GetPosition(System.IntPtr ex_3d_source) { }
        // RVA: 0x068D3F28  token: 0x6000271
        private static System.Void criAtomEx3dSource_Set3dRegionHn(System.IntPtr ex_3d_source, System.IntPtr ex_3d_region) { }
        // RVA: 0x068D41E0  token: 0x6000272
        private static System.Void criAtomEx3dSource_SetListenerBasedElevationAngleAisacControlId(System.IntPtr ex_3d_source, System.UInt16 aisac_control_id) { }
        // RVA: 0x068D45C4  token: 0x6000273
        private static System.Void criAtomEx3dSource_SetSourceBasedElevationAngleAisacControlId(System.IntPtr ex_3d_source, System.UInt16 aisac_control_id) { }

    }

    // TypeToken: 0x200005F  // size: 0x28
    public class CriAtomEx3dTransceiver : CriWare.CriDisposable
    {
        // Fields
        private System.IntPtr handle;  // 0x20

        // Properties
        System.IntPtr nativeHandle { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x068D4A74  token: 0x6000275
        public System.Void .ctor() { }
        // RVA: 0x068D47E4  token: 0x6000276
        public virtual System.Void Dispose() { }
        // RVA: 0x068D4758  token: 0x6000277
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068D4A68  token: 0x6000279
        public System.Void Update() { }
        // RVA: 0x068D4908  token: 0x600027A
        public System.Void SetInputPosition(UnityEngine.Vector3 position) { }
        // RVA: 0x068D4A08  token: 0x600027B
        public System.Void SetOutputPosition(UnityEngine.Vector3 position) { }
        // RVA: 0x068D4894  token: 0x600027C
        public System.Void SetInputOrientation(UnityEngine.Vector3 front, UnityEngine.Vector3 top) { }
        // RVA: 0x068D4994  token: 0x600027D
        public System.Void SetOutputOrientation(UnityEngine.Vector3 front, UnityEngine.Vector3 top) { }
        // RVA: 0x068D4968  token: 0x600027E
        public System.Void SetOutputConeParameter(System.Single insideAngle, System.Single outsideAngle, System.Single outsideVolume) { }
        // RVA: 0x068D4988  token: 0x600027F
        public System.Void SetOutputMinMaxDistance(System.Single minDistance, System.Single maxDistance) { }
        // RVA: 0x068D497C  token: 0x6000280
        public System.Void SetOutputInteriorPanField(System.Single radius, System.Single interiorDistance) { }
        // RVA: 0x068D4888  token: 0x6000281
        public System.Void SetInputCrossFadeField(System.Single directAudioRadius, System.Single crossfadeDistance) { }
        // RVA: 0x068D4A44  token: 0x6000282
        public System.Void SetOutputVolume(System.Single volume) { }
        // RVA: 0x068D4740  token: 0x6000283
        public System.Void AttachAisac(System.String globalAisacName) { }
        // RVA: 0x068D474C  token: 0x6000284
        public System.Void DetachAisac(System.String globalAisacName) { }
        // RVA: 0x068D495C  token: 0x6000285
        public System.Void SetMaxAngleAisacDelta(System.Single maxDelta) { }
        // RVA: 0x068D487C  token: 0x6000286
        public System.Void SetDistanceAisacControlId(System.UInt16 aisacControlId) { }
        // RVA: 0x068D4944  token: 0x6000287
        public System.Void SetListenerBasedAzimuthAngleAisacControlId(System.UInt16 aisacControlId) { }
        // RVA: 0x068D4950  token: 0x6000288
        public System.Void SetListenerBasedElevationAngleAisacControlId(System.UInt16 aisacControlId) { }
        // RVA: 0x068D4A50  token: 0x6000289
        public System.Void SetTransceiverOutputBasedAzimuthAngleAisacControlId(System.UInt16 aisacControlId) { }
        // RVA: 0x068D4A5C  token: 0x600028A
        public System.Void SetTransceiverOutputBasedElevationAngleAisacControlId(System.UInt16 aisacControlId) { }
        // RVA: 0x068D4864  token: 0x600028B
        public System.Void Set3dRegion(CriWare.CriAtomEx3dRegion region3d) { }
        // RVA: 0x068D47F0  token: 0x600028C
        protected virtual System.Void Finalize() { }

    }

    // TypeToken: 0x2000062  // size: 0x28
    public class CriAtomEx3dRegion : CriWare.CriDisposable
    {
        // Fields
        private System.IntPtr handle;  // 0x20

        // Properties
        System.IntPtr nativeHandle { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x068D364C  token: 0x60002A2
        public System.Void .ctor() { }
        // RVA: 0x068D3540  token: 0x60002A3
        public virtual System.Void Dispose() { }
        // RVA: 0x068D354C  token: 0x60002A4
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068D35D8  token: 0x60002A5
        protected virtual System.Void Finalize() { }

    }

    // TypeToken: 0x2000065  // size: 0x30
    public class CriAtomExAcb : CriWare.CriDisposable
    {
        // Fields
        private System.IntPtr handle;  // 0x20
        private System.Runtime.InteropServices.GCHandle dataHandle;  // 0x28

        // Properties
        System.IntPtr nativeHandle { get; /* RVA: 0x04D862C0 */ }
        System.Boolean isAvailable { get; /* RVA: 0x068D77F0 */ }

        // Methods
        // RVA: 0x068D67A0  token: 0x60002AB
        public static CriWare.CriAtomExAcb LoadAcbFile(CriWare.CriFsBinder binder, System.String acbPath, System.String awbPath) { }
        // RVA: 0x068D6684  token: 0x60002AC
        public static CriWare.CriAtomExAcb LoadAcbData(System.Byte[] acbData, CriWare.CriFsBinder awbBinder, System.String awbPath) { }
        // RVA: 0x068D5458  token: 0x60002AD
        public virtual System.Void Dispose() { }
        // RVA: 0x068D54A8  token: 0x60002AE
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068D5524  token: 0x60002AF
        public System.Boolean Exists(System.String cueName) { }
        // RVA: 0x068D5530  token: 0x60002B0
        public System.Boolean Exists(System.Int32 cueId) { }
        // RVA: 0x068D5ADC  token: 0x60002B1
        public System.Boolean GetCueInfo(System.String cueName, CriWare.CriAtomEx.CueInfo& info) { }
        // RVA: 0x068D58A8  token: 0x60002B2
        public System.Boolean GetCueInfo(System.Int32 cueId, CriWare.CriAtomEx.CueInfo& info) { }
        // RVA: 0x068D55C8  token: 0x60002B3
        public System.Boolean GetCueInfoByIndex(System.Int32 index, CriWare.CriAtomEx.CueInfo& info) { }
        // RVA: 0x068D57FC  token: 0x60002B4
        public CriWare.CriAtomEx.CueInfo[] GetCueInfoList() { }
        // RVA: 0x068D6284  token: 0x60002B5
        public System.Boolean GetWaveFormInfo(System.String cueName, CriWare.CriAtomEx.WaveformInfo& info) { }
        // RVA: 0x068D6478  token: 0x60002B6
        public System.Boolean GetWaveFormInfo(System.Int32 cueId, CriWare.CriAtomEx.WaveformInfo& info) { }
        // RVA: 0x068D5D1C  token: 0x60002B7
        public System.Int32 GetNumCuePlaying(System.String name) { }
        // RVA: 0x068D5D10  token: 0x60002B8
        public System.Int32 GetNumCuePlaying(System.Int32 id) { }
        // RVA: 0x068D55BC  token: 0x60002B9
        public System.Int32 GetBlockIndex(System.String cueName, System.String blockName) { }
        // RVA: 0x068D55B0  token: 0x60002BA
        public System.Int32 GetBlockIndex(System.Int32 cueId, System.String blockName) { }
        // RVA: 0x068D5D28  token: 0x60002BB
        public System.Int32 GetNumUsableAisacControls(System.String cueName) { }
        // RVA: 0x068D5D34  token: 0x60002BC
        public System.Int32 GetNumUsableAisacControls(System.Int32 cueId) { }
        // RVA: 0x068D60AC  token: 0x60002BD
        public System.Boolean GetUsableAisacControl(System.String cueName, System.Int32 index, CriWare.CriAtomEx.AisacControlInfo& info) { }
        // RVA: 0x068D5EC4  token: 0x60002BE
        public System.Boolean GetUsableAisacControl(System.Int32 cueId, System.Int32 index, CriWare.CriAtomEx.AisacControlInfo& info) { }
        // RVA: 0x068D5D40  token: 0x60002BF
        public CriWare.CriAtomEx.AisacControlInfo[] GetUsableAisacControlList(System.String cueName) { }
        // RVA: 0x068D5E04  token: 0x60002C0
        public CriWare.CriAtomEx.AisacControlInfo[] GetUsableAisacControlList(System.Int32 cueId) { }
        // RVA: 0x068D6878  token: 0x60002C1
        public System.Void ResetCueTypeState(System.String cueName) { }
        // RVA: 0x068D686C  token: 0x60002C2
        public System.Void ResetCueTypeState(System.Int32 cueId) { }
        // RVA: 0x068D53A0  token: 0x60002C3
        public System.Void AttachAwbFile(CriWare.CriFsBinder awb_binder, System.String awb_path, System.String awb_name) { }
        // RVA: 0x068D5444  token: 0x60002C4
        public System.Void DetachAwbFile(System.String awb_name) { }
        // RVA: 0x068D666C  token: 0x60002C5
        public System.Boolean IsReadyToRelease() { }
        // RVA: 0x04D88DD0  token: 0x60002C6
        public System.Single GetLoadProgress() { }
        // RVA: 0x068D53D8  token: 0x60002C7
        public System.Void Decrypt(System.UInt64 key, System.UInt64 nonce) { }
        // RVA: 0x068D6884  token: 0x60002C8
        private System.Void .ctor(System.IntPtr handle, System.Nullable<System.Runtime.InteropServices.GCHandle> dataHandle) { }
        // RVA: 0x068D553C  token: 0x60002C9
        protected virtual System.Void Finalize() { }
        // RVA: 0x068D75EC  token: 0x60002CA
        private static System.IntPtr criAtomExAcb_LoadAcbFile(System.IntPtr acb_binder, System.String acb_path, System.IntPtr awb_binder, System.String awb_path, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068D7508  token: 0x60002CB
        private static System.IntPtr criAtomExAcb_LoadAcbData(System.IntPtr acb_data, System.Int32 acb_data_size, System.IntPtr awb_binder, System.String awb_path, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068D76E4  token: 0x60002CC
        private static System.Void criAtomExAcb_Release(System.IntPtr acb_hn) { }
        // RVA: 0x068D7070  token: 0x60002CD
        private static System.Int32 criAtomExAcb_GetNumCues(System.IntPtr acb_hn) { }
        // RVA: 0x068D6AA0  token: 0x60002CE
        private static System.Boolean criAtomExAcb_ExistsId(System.IntPtr acb_hn, System.Int32 id) { }
        // RVA: 0x068D6B28  token: 0x60002CF
        private static System.Boolean criAtomExAcb_ExistsName(System.IntPtr acb_hn, System.String name) { }
        // RVA: 0x068D70A4  token: 0x60002D0
        private static System.Int32 criAtomExAcb_GetNumUsableAisacControlsById(System.IntPtr acb_hn, System.Int32 id) { }
        // RVA: 0x068D7128  token: 0x60002D1
        private static System.Int32 criAtomExAcb_GetNumUsableAisacControlsByName(System.IntPtr acb_hn, System.String name) { }
        // RVA: 0x068D71D4  token: 0x60002D2
        private static System.Boolean criAtomExAcb_GetUsableAisacControlById(System.IntPtr acb_hn, System.Int32 id, System.UInt16 index, System.IntPtr info) { }
        // RVA: 0x068D727C  token: 0x60002D3
        private static System.Boolean criAtomExAcb_GetUsableAisacControlByName(System.IntPtr acb_hn, System.String name, System.UInt16 index, System.IntPtr info) { }
        // RVA: 0x068D7340  token: 0x60002D4
        private static System.Boolean criAtomExAcb_GetWaveformInfoById(System.IntPtr acb_hn, System.Int32 id, System.IntPtr waveform_info) { }
        // RVA: 0x068D73D8  token: 0x60002D5
        private static System.Boolean criAtomExAcb_GetWaveformInfoByName(System.IntPtr acb_hn, System.String name, System.IntPtr waveform_info) { }
        // RVA: 0x068D6E8C  token: 0x60002D6
        private static System.Boolean criAtomExAcb_GetCueInfoByName(System.IntPtr acb_hn, System.String name, System.IntPtr info) { }
        // RVA: 0x068D6D5C  token: 0x60002D7
        private static System.Boolean criAtomExAcb_GetCueInfoById(System.IntPtr acb_hn, System.Int32 id, System.IntPtr info) { }
        // RVA: 0x068D6DF4  token: 0x60002D8
        private static System.Boolean criAtomExAcb_GetCueInfoByIndex(System.IntPtr acb_hn, System.Int32 index, System.IntPtr info) { }
        // RVA: 0x068D6FC4  token: 0x60002D9
        private static System.Int32 criAtomExAcb_GetNumCuePlayingCountByName(System.IntPtr acb_hn, System.String name) { }
        // RVA: 0x068D6F40  token: 0x60002DA
        private static System.Int32 criAtomExAcb_GetNumCuePlayingCountById(System.IntPtr acb_hn, System.Int32 id) { }
        // RVA: 0x068D6BD8  token: 0x60002DB
        private static System.Int32 criAtomExAcb_GetBlockIndexById(System.IntPtr acb_hn, System.Int32 id, System.String block_name) { }
        // RVA: 0x068D6C90  token: 0x60002DC
        private static System.Int32 criAtomExAcb_GetBlockIndexByName(System.IntPtr acb_hn, System.String name, System.String block_name) { }
        // RVA: 0x068D7758  token: 0x60002DD
        private static System.Void criAtomExAcb_ResetCueTypeStateByName(System.IntPtr acb_hn, System.String name) { }
        // RVA: 0x068D7718  token: 0x60002DE
        private static System.Void criAtomExAcb_ResetCueTypeStateById(System.IntPtr acb_hn, System.Int32 id) { }
        // RVA: 0x068D6924  token: 0x60002DF
        private static System.Void criAtomExAcb_AttachAwbFile(System.IntPtr acb_hn, System.IntPtr awb_binder, System.String awb_path, System.String awb_name, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068D6A08  token: 0x60002E0
        private static System.Void criAtomExAcb_DetachAwbFile(System.IntPtr acb_hn, System.String awb_name) { }
        // RVA: 0x068D748C  token: 0x60002E1
        private static System.Boolean criAtomExAcb_IsReadyToRelease(System.IntPtr acb_hn) { }

    }

    // TypeToken: 0x2000066  // size: 0x38
    public class CriAtomExAcbLoader : CriWare.CriDisposable
    {
        // Fields
        private System.IntPtr handle;  // 0x20
        private System.Nullable<System.Runtime.InteropServices.GCHandle> gch;  // 0x28

        // Methods
        // RVA: 0x068D4DB4  token: 0x60002E2
        public static CriWare.CriAtomExAcbLoader LoadAcbFileAsync(CriWare.CriFsBinder binder, System.String acbPath, System.String awbPath, System.Boolean loadAwbOnMemory) { }
        // RVA: 0x068D4C78  token: 0x60002E3
        public static CriWare.CriAtomExAcbLoader LoadAcbDataAsync(System.Byte[] acbData, CriWare.CriFsBinder awbBinder, System.String awbPath, System.Boolean loadAwbOnMemory) { }
        // RVA: 0x068D4C6C  token: 0x60002E4
        public CriWare.CriAtomExAcbLoader.Status GetStatus() { }
        // RVA: 0x068D4E9C  token: 0x60002E5
        public CriWare.CriAtomExAcb MoveAcb() { }
        // RVA: 0x068D4AE4  token: 0x60002E6
        public virtual System.Void Dispose() { }
        // RVA: 0x068D4B34  token: 0x60002E7
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068D4F3C  token: 0x60002E8
        private System.Void .ctor(System.IntPtr handle, System.Nullable<System.Runtime.InteropServices.GCHandle> dataHandle) { }
        // RVA: 0x068D4BF8  token: 0x60002E9
        protected virtual System.Void Finalize() { }
        // RVA: 0x068D5000  token: 0x60002EA
        private static System.IntPtr criAtomExAcbLoader_Create(CriWare.CriAtomExAcbLoader.LoaderConfig& config) { }
        // RVA: 0x068D5080  token: 0x60002EB
        private static System.Void criAtomExAcbLoader_Destroy(System.IntPtr acb_loader) { }
        // RVA: 0x068D5204  token: 0x60002EC
        private static System.Boolean criAtomExAcbLoader_LoadAcbFileAsync(System.IntPtr acb_loader, System.IntPtr acb_binder, System.String acb_path, System.IntPtr awb_binder, System.String awb_path) { }
        // RVA: 0x068D512C  token: 0x60002ED
        private static System.Boolean criAtomExAcbLoader_LoadAcbDataAsync(System.IntPtr acb_loader, System.IntPtr acb_data, System.Int32 acb_size, System.IntPtr awb_binder, System.String awb_path) { }
        // RVA: 0x068D50B4  token: 0x60002EE
        private static CriWare.CriAtomExAcbLoader.Status criAtomExAcbLoader_GetStatus(System.IntPtr acb_loader) { }
        // RVA: 0x068D5368  token: 0x60002EF
        private static System.Boolean criAtomExAcbLoader_WaitForCompletion(System.IntPtr acb_loader) { }
        // RVA: 0x068D52F0  token: 0x60002F0
        private static System.IntPtr criAtomExAcbLoader_MoveAcbHandle(System.IntPtr acb_loader) { }

    }

    // TypeToken: 0x2000069  // size: 0x10
    public class CriAtomExAcf
    {
        // Methods
        // RVA: 0x068D9034  token: 0x60002F1
        public static System.Int32 GetNumAisacControls() { }
        // RVA: 0x068D79F4  token: 0x60002F2
        public static System.Boolean GetAisacControlInfo(System.UInt16 index, CriWare.CriAtomEx.AisacControlInfo& info) { }
        // RVA: 0x068D905C  token: 0x60002F3
        public static System.Int32 GetNumDspSettings() { }
        // RVA: 0x068D9054  token: 0x60002F4
        public static System.Int32 GetNumDspSettings(System.IntPtr acfData, System.Int32 size) { }
        // RVA: 0x068D8684  token: 0x60002F5
        public static System.String GetDspSettingNameByIndex(System.UInt16 index) { }
        // RVA: 0x068D86DC  token: 0x60002F6
        public static System.String GetDspSettingNameByIndex(System.IntPtr acfData, System.Int32 size, System.UInt16 index) { }
        // RVA: 0x068D84A0  token: 0x60002F7
        public static System.Boolean GetDspSettingInformation(System.String name, CriWare.CriAtomExAcf.AcfDspSettingInfo& info) { }
        // RVA: 0x068D8750  token: 0x60002F8
        public static System.Boolean GetDspSettingSnapshotInformation(System.UInt16 index, CriWare.CriAtomExAcf.AcfDspSettingSnapshotInfo& info) { }
        // RVA: 0x068D8120  token: 0x60002F9
        public static System.Boolean GetDspBusInformation(System.UInt16 index, CriWare.CriAtomExAcf.AcfDspBusInfo& info) { }
        // RVA: 0x068D8320  token: 0x60002FA
        public static System.Boolean GetDspBusLinkInformation(System.UInt16 index, CriWare.CriAtomExAcf.AcfDspBusLinkInfo& info) { }
        // RVA: 0x068D904C  token: 0x60002FB
        public static System.Int32 GetNumCategories() { }
        // RVA: 0x068D9044  token: 0x60002FC
        public static System.Int32 GetNumCategoriesPerPlayback() { }
        // RVA: 0x068D7D80  token: 0x60002FD
        public static System.Boolean GetCategoryInfoByIndex(System.UInt16 index, CriWare.CriAtomExAcf.CategoryInfo& info) { }
        // RVA: 0x068D7F58  token: 0x60002FE
        public static System.Boolean GetCategoryInfoByName(System.String name, CriWare.CriAtomExAcf.CategoryInfo& info) { }
        // RVA: 0x068D7BAC  token: 0x60002FF
        public static System.Boolean GetCategoryInfoById(System.UInt32 id, CriWare.CriAtomExAcf.CategoryInfo& info) { }
        // RVA: 0x068D9064  token: 0x6000300
        public static System.Int32 GetNumGlobalAisacs() { }
        // RVA: 0x068D8B80  token: 0x6000301
        public static System.Boolean GetGlobalAisacInfoByIndex(System.UInt16 index, CriWare.CriAtomExAcf.GlobalAisacInfo& info) { }
        // RVA: 0x068D8D54  token: 0x6000302
        public static System.Boolean GetGlobalAisacInfoByName(System.String name, CriWare.CriAtomExAcf.GlobalAisacInfo& info) { }
        // RVA: 0x068D893C  token: 0x6000303
        public static System.Boolean GetGlobalAisacGraphInfo(CriWare.CriAtomExAcf.GlobalAisacInfo aisacInfo, System.UInt16 graphIndex, CriWare.CriAtomExAcf.AisacGraphInfo& graphInfo) { }
        // RVA: 0x068D8F1C  token: 0x6000304
        public static System.Boolean GetGlobalAisacValue(CriWare.CriAtomExAcf.GlobalAisacInfo aisacInfo, System.Single control, CriWare.CriAtomExAcf.AisacGraphType type, System.Single& value) { }
        // RVA: 0x068D7804  token: 0x6000305
        public static System.Boolean GetAcfInfo(CriWare.CriAtomExAcf.AcfInfo& acfInfo) { }
        // RVA: 0x068D906C  token: 0x6000306
        public static System.Int32 GetNumSelectors() { }
        // RVA: 0x068D9074  token: 0x6000307
        public static System.Boolean GetSelectorInfoByIndex(System.UInt16 index, CriWare.CriAtomExAcf.SelectorInfo& info) { }
        // RVA: 0x068D922C  token: 0x6000308
        public static System.Boolean GetSelectorInfoByName(System.String name, CriWare.CriAtomExAcf.SelectorInfo& info) { }
        // RVA: 0x068D93E4  token: 0x6000309
        public static System.Boolean GetSelectorLabelInfo(CriWare.CriAtomExAcf.SelectorInfo selectorInfo, System.UInt16 labelIndex, CriWare.CriAtomExAcf.SelectorLabelInfo& info) { }
        // RVA: 0x068D903C  token: 0x600030A
        public static System.Int32 GetNumBuses() { }
        // RVA: 0x068D902C  token: 0x600030B
        public static System.Int32 GetMaxBusesOfDspBusSettings() { }
        // RVA: 0x068D77FC  token: 0x600030C
        public static System.String FindBusName(System.String busName) { }
        // RVA: 0x068DA1F4  token: 0x600030D
        private static System.Int32 criAtomExAcf_GetNumAisacControls() { }
        // RVA: 0x068D9848  token: 0x600030E
        private static System.Boolean criAtomExAcf_GetAisacControlInfo(System.UInt16 index, System.IntPtr info) { }
        // RVA: 0x068D97F4  token: 0x600030F
        private static System.UInt32 criAtomExAcf_GetAisacControlIdByName(System.String name) { }
        // RVA: 0x068D98D4  token: 0x6000310
        private static System.String criAtomExAcf_GetAisacControlNameById(System.UInt32 id) { }
        // RVA: 0x068DA468  token: 0x6000311
        private static System.Int32 criAtomExAcf_GetNumDspSettings() { }
        // RVA: 0x068DA3E4  token: 0x6000312
        private static System.Int32 criAtomExAcf_GetNumDspSettingsFromAcfData(System.IntPtr acf_data, System.Int32 acf_data_size) { }
        // RVA: 0x068D9E9C  token: 0x6000313
        private static System.IntPtr criAtomExAcf_GetDspSettingNameByIndex(System.UInt16 index) { }
        // RVA: 0x068D9E08  token: 0x6000314
        private static System.IntPtr criAtomExAcf_GetDspSettingNameByIndexFromAcfData(System.IntPtr acf_data, System.Int32 acf_data_size, System.UInt16 index) { }
        // RVA: 0x068D9DA4  token: 0x6000315
        private static System.Boolean criAtomExAcf_GetDspSettingInformation(System.String name, System.IntPtr info) { }
        // RVA: 0x068D9F14  token: 0x6000316
        private static System.Boolean criAtomExAcf_GetDspSettingSnapshotInformation(System.UInt16 index, System.IntPtr info) { }
        // RVA: 0x068D9AE4  token: 0x6000317
        private static System.Boolean criAtomExAcf_GetDspBusInformation(System.UInt16 index, System.IntPtr info) { }
        // RVA: 0x068D9D2C  token: 0x6000318
        private static System.Int32 criAtomExAcf_GetDspFxType(System.UInt16 index) { }
        // RVA: 0x068D9BFC  token: 0x6000319
        private static System.String criAtomExAcf_GetDspFxName(System.UInt16 index) { }
        // RVA: 0x068D9C94  token: 0x600031A
        private static System.Boolean criAtomExAcf_GetDspFxParameters(System.UInt16 index, System.IntPtr parameters, System.Int32 size) { }
        // RVA: 0x068D9B70  token: 0x600031B
        private static System.Boolean criAtomExAcf_GetDspBusLinkInformation(System.UInt16 index, System.IntPtr info) { }
        // RVA: 0x068DA2C8  token: 0x600031C
        private static System.Int32 criAtomExAcf_GetNumCategoriesFromAcfData(System.IntPtr acf_data, System.Int32 acf_data_size) { }
        // RVA: 0x068DA3B8  token: 0x600031D
        private static System.Int32 criAtomExAcf_GetNumCategories() { }
        // RVA: 0x068DA34C  token: 0x600031E
        private static System.Int32 criAtomExAcf_GetNumCategoriesPerPlaybackFromAcfData(System.IntPtr acf_data, System.Int32 acf_data_size) { }
        // RVA: 0x068DA38C  token: 0x600031F
        private static System.Int32 criAtomExAcf_GetNumCategoriesPerPlayback() { }
        // RVA: 0x068D9A58  token: 0x6000320
        private static System.Boolean criAtomExAcf_GetCategoryInfo(System.UInt16 index, System.IntPtr info) { }
        // RVA: 0x068D99F4  token: 0x6000321
        private static System.Boolean criAtomExAcf_GetCategoryInfoByName(System.String name, System.IntPtr info) { }
        // RVA: 0x068D996C  token: 0x6000322
        private static System.Boolean criAtomExAcf_GetCategoryInfoById(System.UInt32 id, System.IntPtr info) { }
        // RVA: 0x068DA4D0  token: 0x6000323
        private static System.Int32 criAtomExAcf_GetNumGlobalAisacs() { }
        // RVA: 0x068DA09C  token: 0x6000324
        private static System.Boolean criAtomExAcf_GetGlobalAisacInfo(System.UInt16 index, System.IntPtr info) { }
        // RVA: 0x068DA038  token: 0x6000325
        private static System.Boolean criAtomExAcf_GetGlobalAisacInfoByName(System.String name, System.IntPtr info) { }
        // RVA: 0x068D9FA0  token: 0x6000326
        private static System.Boolean criAtomExAcf_GetGlobalAisacGraphInfo(System.IntPtr aisac_info, System.UInt16 graph_index, System.IntPtr graph_info) { }
        // RVA: 0x068DA128  token: 0x6000327
        private static System.Boolean criAtomExAcf_GetGlobalAisacValue(System.IntPtr aisac_info, System.Single control, CriWare.CriAtomExAcf.AisacGraphType type, System.Single& value) { }
        // RVA: 0x068D97BC  token: 0x6000328
        private static System.Boolean criAtomExAcf_GetAcfInfo(System.IntPtr acf_info) { }
        // RVA: 0x068D9724  token: 0x6000329
        private static System.Boolean criAtomExAcf_GetAcfInfoFromAcfData(System.IntPtr acf_data, System.Int32 acf_data_size, System.IntPtr acf_info) { }
        // RVA: 0x068DA4F4  token: 0x600032A
        private static System.Int32 criAtomExAcf_GetNumSelectors() { }
        // RVA: 0x068DA518  token: 0x600032B
        private static System.Boolean criAtomExAcf_GetSelectorInfoByIndex(System.UInt16 index, System.IntPtr info) { }
        // RVA: 0x068DA5A4  token: 0x600032C
        private static System.Boolean criAtomExAcf_GetSelectorInfoByName(System.String name, System.IntPtr info) { }
        // RVA: 0x068DA608  token: 0x600032D
        private static System.Boolean criAtomExAcf_GetSelectorLabelInfo(System.IntPtr selector_info, System.UInt16 label_index, System.IntPtr info) { }
        // RVA: 0x068DA218  token: 0x600032E
        private static System.Int32 criAtomExAcf_GetNumBusesFromAcfData(System.IntPtr acf_data, System.Int32 acf_data_size) { }
        // RVA: 0x068DA29C  token: 0x600032F
        private static System.Int32 criAtomExAcf_GetNumBuses() { }
        // RVA: 0x068DA188  token: 0x6000330
        private static System.Int32 criAtomExAcf_GetMaxBusesOfDspBusSettingsFromAcfData(System.IntPtr acf_data, System.Int32 acf_data_size) { }
        // RVA: 0x068DA1C8  token: 0x6000331
        private static System.Int32 criAtomExAcf_GetMaxBusesOfDspBusSettings() { }
        // RVA: 0x068D966C  token: 0x6000332
        private static System.String criAtomExAcf_FindBusName(System.String bus_name) { }
        // RVA: 0x041E1670  token: 0x6000333
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000078  // size: 0x28
    public class CriAtomExAsrRack : CriWare.CriDisposable
    {
        // Fields
        public static System.Int32 defaultRackId;  // const
        private System.Int32 _rackId;  // 0x20
        private System.Boolean hasExistingRackId;  // 0x24

        // Properties
        System.Int32 rackId { get; /* RVA: 0x02B70F90 */ }
        CriWare.CriAtomExAsrRack.Config defaultConfig { get; /* RVA: 0x04D88DE0 */ }

        // Methods
        // RVA: 0x068DA9D8  token: 0x600033E
        public System.Void .ctor(CriWare.CriAtomExAsrRack.Config config, CriWare.CriAtomExAsrRack.PlatformConfig platformConfig) { }
        // RVA: 0x068DAAE4  token: 0x600033F
        public System.Void .ctor(System.Int32 existingRackId) { }
        // RVA: 0x068DA6AC  token: 0x6000340
        public System.Void AttachDspBusSetting(System.String settingName) { }
        // RVA: 0x068DA750  token: 0x6000341
        public System.Void DetachDspBusSetting() { }
        // RVA: 0x068DA6A0  token: 0x6000342
        public System.Void ApplyDspBusSnapshot(System.String snapshotName, System.Int32 timeMs) { }
        // RVA: 0x068DA8A4  token: 0x6000343
        public static System.String GetAppliedDspBusSnapshotName(System.Int32 rackId) { }
        // RVA: 0x068DA84C  token: 0x6000344
        public System.String GetAppliedDspBusSnapshotName() { }
        // RVA: 0x068DA92C  token: 0x6000345
        public CriWare.CriAtomExAsrRack.PerformanceInfo GetPerformanceInfo() { }
        // RVA: 0x068DA8F8  token: 0x6000346
        public static CriWare.CriAtomExAsrRack.PerformanceInfo GetPerformanceInfoByRackId(System.Int32 rackId) { }
        // RVA: 0x068DA9BC  token: 0x6000347
        public System.Void ResetPerformanceMonitor() { }
        // RVA: 0x068DA9B4  token: 0x6000348
        public static System.Void ResetPerformanceMonitorByRackId(System.Int32 rackId) { }
        // RVA: 0x068DA9C8  token: 0x6000349
        public static System.Void SetAisacControl(System.Int32 rackId, System.String controlName, System.Single value) { }
        // RVA: 0x068DA9D0  token: 0x600034A
        public static System.Void SetAisacControl(System.Int32 rackId, System.Int32 controlId, System.Single value) { }
        // RVA: 0x068DA75C  token: 0x600034B
        public virtual System.Void Dispose() { }
        // RVA: 0x068DA7D8  token: 0x600034E
        protected virtual System.Void Finalize() { }
        // RVA: 0x068DA6CC  token: 0x600034F
        private static System.Int32 CRIWARE6158351D(CriWare.CriAtomExAsrRack.Config& config, CriWare.CriAtomExAsrRack.PlatformConfig& platformConfig) { }
        // RVA: 0x068DACFC  token: 0x6000350
        private static System.Void criAtomExAsrRack_Destroy(System.Int32 rackId) { }
        // RVA: 0x068DAC48  token: 0x6000351
        private static System.Void criAtomExAsrRack_AttachDspBusSetting(System.Int32 rackId, System.String setting, System.IntPtr work, System.Int32 workSize) { }
        // RVA: 0x068DAD74  token: 0x6000352
        private static System.Void criAtomExAsrRack_DetachDspBusSetting(System.Int32 rackId) { }
        // RVA: 0x068DADEC  token: 0x6000353
        private static System.IntPtr criAtomExAsrRack_GetAppliedDspBusSnapshotName(System.Int32 rackId) { }
        // RVA: 0x068DABA0  token: 0x6000354
        private static System.Void criAtomExAsrRack_ApplyDspBusSnapshot(System.Int32 rackId, System.String snapshotName, System.Int32 timeMs) { }
        // RVA: 0x068DAE64  token: 0x6000355
        private static System.Void criAtomExAsrRack_GetPerformanceInfo(System.Int32 rackId, CriWare.CriAtomExAsrRack.PerformanceInfo& perfInfo) { }
        // RVA: 0x068DAEE8  token: 0x6000356
        private static System.Void criAtomExAsrRack_ResetPerformanceMonitor(System.Int32 rackId) { }
        // RVA: 0x068DAF60  token: 0x6000357
        private static System.Void criAtomExAsrRack_SetAisacControlById(System.Int32 rackId, System.UInt16 controlId, System.Single value) { }
        // RVA: 0x068DAFAC  token: 0x6000358
        private static System.Void criAtomExAsrRack_SetAisacControlByName(System.Int32 rackId, System.String controlName, System.Single value) { }

    }

    // TypeToken: 0x200007D  // size: 0x30
    public class CriAtomExAuxIn : CriWare.CriDisposable
    {
        // Fields
        private static System.String errorInvalidHandle;  // const
        private System.IntPtr handle;  // 0x20
        private CriWare.CriAudioReadStream inputReadStream;  // 0x28

        // Methods
        // RVA: 0x068DBF10  token: 0x6000359
        public System.Void .ctor(System.Nullable<CriWare.CriAtomExAuxIn.Config> config) { }
        // RVA: 0x068DA7D8  token: 0x600035A
        protected virtual System.Void Finalize() { }
        // RVA: 0x068DBDFC  token: 0x600035B
        public virtual System.Void Dispose() { }
        // RVA: 0x068DBEF0  token: 0x600035C
        public System.Void Start() { }
        // RVA: 0x068DBEFC  token: 0x600035D
        public System.Void Stop() { }
        // RVA: 0x068DBE90  token: 0x600035E
        public System.Void SetFormat(System.Int32 numChannels, System.Int32 samplingRate) { }
        // RVA: 0x068DBE78  token: 0x600035F
        public System.Void GetFormat(System.Int32& numChannels, System.Int32& samplingRate) { }
        // RVA: 0x068DBEE4  token: 0x6000360
        public System.Void SetVolume(System.Single volume) { }
        // RVA: 0x068DBE9C  token: 0x6000361
        public System.Void SetFrequencyRatio(System.Single frequencyRatio) { }
        // RVA: 0x068DBE84  token: 0x6000362
        public System.Void SetBusSendLevel(System.String busName, System.Single level) { }
        // RVA: 0x068DBEA8  token: 0x6000363
        public System.Void SetInputReadStream(CriWare.CriAudioReadStream stream) { }
        // RVA: 0x068DBFE0  token: 0x6000364
        private static System.IntPtr criAtomAuxIn_Create(CriWare.CriAtomExAuxIn.Config& config, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068DC074  token: 0x6000365
        private static System.Void criAtomAuxIn_Destroy(System.IntPtr aux_in) { }
        // RVA: 0x068DC304  token: 0x6000366
        private static System.Void criAtomAuxIn_Start(System.IntPtr aux_in) { }
        // RVA: 0x068DC338  token: 0x6000367
        private static System.Void criAtomAuxIn_Stop(System.IntPtr aux_in) { }
        // RVA: 0x068DC2C8  token: 0x6000368
        private static System.Void criAtomAuxIn_SetVolume(System.IntPtr aux_in, System.Single volume) { }
        // RVA: 0x068DC230  token: 0x6000369
        private static System.Void criAtomAuxIn_SetFrequencyRatio(System.IntPtr aux_in, System.Single ratio) { }
        // RVA: 0x068DC13C  token: 0x600036A
        private static System.Void criAtomAuxIn_SetBusSendLevelByName(System.IntPtr aux_in, System.String bus_name, System.Single level) { }
        // RVA: 0x068DC19C  token: 0x600036B
        private static System.Void criAtomAuxIn_SetFormat(System.IntPtr aux_in, System.Int32 num_channels, System.Int32 sampling_rate) { }
        // RVA: 0x068DC0A8  token: 0x600036C
        private static System.Void criAtomAuxIn_GetFormat(System.IntPtr aux_in, System.Int32& num_channels, System.Int32& sampling_rate) { }
        // RVA: 0x068DC274  token: 0x600036D
        private static System.Void criAtomAuxIn_SetInputReadStream(System.IntPtr aux_in, System.IntPtr stream_cbfunc, System.IntPtr stream_ptr) { }

    }

    // TypeToken: 0x200007F  // size: 0x40
    public class CriAtomExMic : CriWare.CriDisposable
    {
        // Fields
        private static System.String errorInvalidHandle;  // const
        private static System.String errorInvalidBufferLength;  // const
        private static System.String errorInvalidNumBuffers;  // const
        private static System.String errorAlreadyInitialized;  // const
        private static System.String errorNotInitialized;  // const
        private static System.Boolean <isInitialized>k__BackingField;  // static @ 0x0
        private System.IntPtr handle;  // 0x20
        private System.IntPtr[] bufferPointers;  // 0x28
        private System.Runtime.InteropServices.GCHandle[] gcHandles;  // 0x30
        private CriWare.CriAudioWriteStream outputWriteStream;  // 0x38
        private static System.Int32 _initializationCount;  // static @ 0x4

        // Properties
        System.Boolean isInitialized { get; /* RVA: 0x068DEB9C */ set; /* RVA: 0x068DEBD4 */ }

        // Methods
        // RVA: 0x068DD9D0  token: 0x6000371
        public static System.Void InitializeModule() { }
        // RVA: 0x068DD688  token: 0x6000372
        public static System.Void FinalizeModule() { }
        // RVA: 0x041E1670  token: 0x6000373
        public static System.Void SetupOutputCategoryForMic_IOS(System.Boolean enable) { }
        // RVA: 0x068DD84C  token: 0x6000374
        public static CriWare.CriAtomExMic.DeviceInfo[] GetDevices() { }
        // RVA: 0x068DD958  token: 0x6000375
        public static System.Int32 GetNumDevices() { }
        // RVA: 0x068DD74C  token: 0x6000376
        public static System.Nullable<CriWare.CriAtomExMic.DeviceInfo> GetDefaultDevice() { }
        // RVA: 0x068DDB24  token: 0x6000377
        public static System.Boolean IsFormatSupported(CriWare.CriAtomExMic.Config config) { }
        // RVA: 0x068DD470  token: 0x6000378
        public static CriWare.CriAtomExMic Create(System.Nullable<CriWare.CriAtomExMic.Config> config) { }
        // RVA: 0x068DDF40  token: 0x6000379
        private System.Void .ctor(System.IntPtr handle) { }
        // RVA: 0x068DA7D8  token: 0x600037A
        protected virtual System.Void Finalize() { }
        // RVA: 0x068DD638  token: 0x600037B
        public virtual System.Void Dispose() { }
        // RVA: 0x068DD5E0  token: 0x600037C
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068DDF04  token: 0x600037D
        public System.Void Start() { }
        // RVA: 0x068DDF10  token: 0x600037E
        public System.Void Stop() { }
        // RVA: 0x068DD94C  token: 0x600037F
        public System.Int32 GetNumChannels() { }
        // RVA: 0x068DD9C4  token: 0x6000380
        public System.Int32 GetSamplingRate() { }
        // RVA: 0x068DD940  token: 0x6000381
        public System.UInt32 GetNumBufferedSamples() { }
        // RVA: 0x068DD940  token: 0x6000382
        public System.UInt32 GetNumBufferredSamples() { }
        // RVA: 0x068DDB04  token: 0x6000383
        public System.Boolean IsAvailable() { }
        // RVA: 0x068DDB98  token: 0x6000384
        public System.UInt32 ReadData(System.Single[] bufferMono) { }
        // RVA: 0x068DDCDC  token: 0x6000385
        public System.UInt32 ReadData(System.Single[] bufferMono, System.UInt32 numToRead) { }
        // RVA: 0x068DDB34  token: 0x6000386
        public System.UInt32 ReadData(System.Single[] bufferL, System.Single[] bufferR) { }
        // RVA: 0x068DDBB8  token: 0x6000387
        public System.UInt32 ReadData(System.Single[] bufferL, System.Single[] bufferR, System.UInt32 numToRead) { }
        // RVA: 0x068DDB5C  token: 0x6000388
        public System.UInt32 ReadData(System.Single[][] buffers) { }
        // RVA: 0x068DDD6C  token: 0x6000389
        public System.UInt32 ReadData(System.Single[][] buffers, System.UInt32 numToRead) { }
        // RVA: 0x068DDEC4  token: 0x600038A
        public System.Void SetOutputWriteStream(CriWare.CriAudioWriteStream stream) { }
        // RVA: 0x068DD960  token: 0x600038B
        public CriWare.CriAudioReadStream GetOutputReadStream() { }
        // RVA: 0x068DD3C8  token: 0x600038C
        public CriWare.CriAtomExMic.Effect AttachEffect(System.IntPtr afxInterface, System.Single[] configParameters) { }
        // RVA: 0x068DD5BC  token: 0x600038D
        public System.Void DetachEffect(CriWare.CriAtomExMic.Effect effect) { }
        // RVA: 0x068DDE98  token: 0x600038E
        public System.Void SetEffectParameter(CriWare.CriAtomExMic.Effect effect, System.Int32 parameterIndex, System.Single parameterValue) { }
        // RVA: 0x068DD91C  token: 0x600038F
        public System.Single GetEffectParameter(CriWare.CriAtomExMic.Effect effect, System.Int32 parameterIndex) { }
        // RVA: 0x068DDE74  token: 0x6000390
        public System.Void SetEffectBypass(CriWare.CriAtomExMic.Effect effect, System.Boolean bypass) { }
        // RVA: 0x068DDF1C  token: 0x6000391
        public System.Void UpdateEffectParameters(CriWare.CriAtomExMic.Effect effect) { }
        // RVA: 0x068DDAF0  token: 0x6000392
        private System.UInt32 InternalReadDataFromBufferPointers(System.UInt32 numToRead) { }
        // RVA: 0x068DDA6C  token: 0x6000393
        private System.Void InternalClearBuffers() { }
        // RVA: 0x068DE068  token: 0x6000394
        private static System.Void criAtomMicUnity_Initialize() { }
        // RVA: 0x068DE000  token: 0x6000395
        private static System.Void criAtomMicUnity_Finalize() { }
        // RVA: 0x068DE714  token: 0x6000396
        private static System.Int32 criAtomMic_GetNumDevices() { }
        // RVA: 0x068DE49C  token: 0x6000397
        private static System.Boolean criAtomMic_GetDevice(System.Int32 index, CriWare.CriAtomExMic.DeviceInfo& info) { }
        // RVA: 0x068DE3AC  token: 0x6000398
        private static System.Boolean criAtomMic_GetDefaultDevice(CriWare.CriAtomExMic.DeviceInfo& info) { }
        // RVA: 0x068DE81C  token: 0x6000399
        private static System.Boolean criAtomMic_IsFormatSupported(CriWare.CriAtomExMic.Config& config) { }
        // RVA: 0x068DE22C  token: 0x600039A
        private static System.IntPtr criAtomMic_Create(CriWare.CriAtomExMic.Config& config, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068DE2F4  token: 0x600039B
        private static System.Void criAtomMic_Destroy(System.IntPtr mic) { }
        // RVA: 0x068DEAAC  token: 0x600039C
        private static System.Void criAtomMic_Start(System.IntPtr mic) { }
        // RVA: 0x068DEB24  token: 0x600039D
        private static System.Void criAtomMic_Stop(System.IntPtr mic) { }
        // RVA: 0x068DE6E0  token: 0x600039E
        private static System.Int32 criAtomMic_GetNumChannels(System.IntPtr mic) { }
        // RVA: 0x068DE76C  token: 0x600039F
        private static System.Int32 criAtomMic_GetSamplingRate(System.IntPtr mic) { }
        // RVA: 0x068DE668  token: 0x60003A0
        private static System.UInt32 criAtomMic_GetNumBufferedSamples(System.IntPtr mic) { }
        // RVA: 0x068DE7E4  token: 0x60003A1
        private static System.Boolean criAtomMic_IsAvailable(System.IntPtr mic) { }
        // RVA: 0x068DE8C8  token: 0x60003A2
        private static System.UInt32 criAtomMic_ReadData(System.IntPtr mic, System.IntPtr[] data, System.UInt32 num_samples) { }
        // RVA: 0x068DEA58  token: 0x60003A3
        private static System.Void criAtomMic_SetOutputWriteStream(System.IntPtr mic, System.IntPtr stream_cbfunc, System.IntPtr stream_ptr) { }
        // RVA: 0x068DE740  token: 0x60003A4
        private static System.IntPtr criAtomMic_GetOutputReadStream() { }
        // RVA: 0x068DE180  token: 0x60003A5
        private static System.Int32 criAtomMic_CalculateWorkSizeForEffect(System.IntPtr mic, System.IntPtr afx_interface, System.Single[] config_parameters, System.UInt32 num_config_parameters) { }
        // RVA: 0x068DE0D0  token: 0x60003A6
        private static System.IntPtr criAtomMic_AttachEffect(System.IntPtr mic, System.IntPtr afx_interface, System.Single[] config_parameters, System.UInt32 num_config_parameters, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068DE328  token: 0x60003A7
        private static System.Void criAtomMic_DetachEffect(System.IntPtr mic, System.IntPtr effect) { }
        // RVA: 0x068DE598  token: 0x60003A8
        private static System.IntPtr criAtomMic_GetEffectInstance(System.IntPtr mic, System.IntPtr effect) { }
        // RVA: 0x068DE968  token: 0x60003A9
        private static System.Void criAtomMic_SetEffectBypass(System.IntPtr mic, System.IntPtr effect, System.Boolean bypass) { }
        // RVA: 0x068DE9FC  token: 0x60003AA
        private static System.Void criAtomMic_SetEffectParameter(System.IntPtr mic, System.IntPtr effect, System.UInt32 parameter_index, System.Single parameter_value) { }
        // RVA: 0x068DE61C  token: 0x60003AB
        private static System.Single criAtomMic_GetEffectParameter(System.IntPtr mic, System.IntPtr effect, System.UInt32 parameter_index) { }
        // RVA: 0x068DEB58  token: 0x60003AC
        private static System.Void criAtomMic_UpdateEffectParameters(System.IntPtr mic, System.IntPtr effect) { }

    }

    // TypeToken: 0x2000083  // size: 0x58
    public class CriAtomExOutputAnalyzer : CriWare.CriDisposable
    {
        // Fields
        public static System.Int32 MaximumSpectrumBands;  // const
        protected System.IntPtr handle;  // 0x20
        protected CriWare.CriAtomExPlayer player;  // 0x28
        protected System.String busName;  // 0x30
        protected System.Int32 numBands;  // 0x38
        protected System.Int32 numCapturedPcmSamples;  // 0x3c
        protected CriWare.CriAtomExOutputAnalyzer.PcmCaptureCallback userPcmCaptureCallback;  // 0x40
        protected System.Single[] dataL;  // 0x48
        protected System.Single[] dataR;  // 0x50
        protected static System.Int32 pcmCapturerNumMaxData;  // const
        protected static System.IntPtr InternalCallbackFunctionPointer;  // static @ 0x0
        protected static CriWare.CriAtomExOutputAnalyzer.InternalPcmCaptureCallback DelegateObject;  // static @ 0x8
        protected static System.Single[] DataL;  // static @ 0x10
        protected static System.Single[] DataR;  // static @ 0x18
        protected static CriWare.CriAtomExOutputAnalyzer.PcmCaptureCallback UserPcmCaptureCallback;  // static @ 0x20

        // Properties
        System.IntPtr nativeHandle { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x068DF84C  token: 0x60003B4
        public System.Void .ctor(CriWare.CriAtomExOutputAnalyzer.Config config) { }
        // RVA: 0x068DF034  token: 0x60003B5
        public virtual System.Void Dispose() { }
        // RVA: 0x068DF040  token: 0x60003B6
        protected System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068DECA4  token: 0x60003B7
        public System.Boolean AttachExPlayer(CriWare.CriAtomExPlayer player) { }
        // RVA: 0x068DEF48  token: 0x60003B8
        public System.Void DetachExPlayer() { }
        // RVA: 0x068DEC10  token: 0x60003B9
        public System.Boolean AttachDspBus(System.String busName) { }
        // RVA: 0x068DEECC  token: 0x60003BA
        public System.Void DetachDspBus() { }
        // RVA: 0x068DF4EC  token: 0x60003BB
        public System.Single GetRms(System.Int32 channel) { }
        // RVA: 0x068DF594  token: 0x60003BC
        public System.Void GetSpectrumLevels(System.Single[]& levels) { }
        // RVA: 0x068DF3F4  token: 0x60003BD
        public System.Void GetPcmData(System.Single[]& data, System.Int32 ch) { }
        // RVA: 0x042B4AE0  token: 0x60003BE
        public System.Void SetPcmCaptureCallback(CriWare.CriAtomExOutputAnalyzer.PcmCaptureCallback callback) { }
        // RVA: 0x068DF128  token: 0x60003BF
        public System.Void ExecutePcmCaptureCallback() { }
        // RVA: 0x068DF104  token: 0x60003C0
        public System.Void ExecutePcmCaptureCallback(CriWare.CriAtomExOutputAnalyzer.PcmCaptureCallback callback) { }
        // RVA: 0x068DF7EC  token: 0x60003C1
        protected System.Void .ctor() { }
        // RVA: 0x068DF380  token: 0x60003C2
        protected virtual System.Void Finalize() { }
        // RVA: 0x068DF680  token: 0x60003C3
        protected System.Void InitializeWithConfig(CriWare.CriAtomExOutputAnalyzer.Config config) { }
        // RVA: 0x068DED5C  token: 0x60003C4
        private static System.Void Callback(System.IntPtr ptrL, System.IntPtr ptrR, System.Int32 numChannels, System.Int32 numData) { }
        // RVA: 0x068DF978  token: 0x60003C5
        protected static System.IntPtr criAtomExOutputAnalyzer_Create(CriWare.CriAtomExOutputAnalyzer.Config& config) { }
        // RVA: 0x068DFA30  token: 0x60003C6
        protected static System.Void criAtomExOutputAnalyzer_Destroy(System.IntPtr analyzer) { }
        // RVA: 0x068DF934  token: 0x60003C7
        protected static System.Void criAtomExOutputAnalyzer_AttachExPlayer(System.IntPtr analyzer, System.IntPtr player) { }
        // RVA: 0x068DFABC  token: 0x60003C8
        protected static System.Void criAtomExOutputAnalyzer_DetachExPlayer(System.IntPtr analyzer, System.IntPtr player) { }
        // RVA: 0x068DF8DC  token: 0x60003C9
        protected static System.Void criAtomExOutputAnalyzer_AttachDspBusByName(System.IntPtr analyzer, System.String busName) { }
        // RVA: 0x068DFA64  token: 0x60003CA
        protected static System.Void criAtomExOutputAnalyzer_DetachDspBusByName(System.IntPtr analyzer, System.String busName) { }
        // RVA: 0x068DFC08  token: 0x60003CB
        protected static System.Single criAtomExOutputAnalyzer_GetRms(System.IntPtr analyzer, System.Int32 channel) { }
        // RVA: 0x068DFC44  token: 0x60003CC
        protected static System.IntPtr criAtomExOutputAnalyzer_GetSpectrumLevels(System.IntPtr analyzer) { }
        // RVA: 0x068DFB84  token: 0x60003CD
        protected static System.IntPtr criAtomExOutputAnalyzer_GetPcmData(System.IntPtr analyzer, System.Int32 ch) { }
        // RVA: 0x068DFB00  token: 0x60003CE
        protected static System.Void criAtomExOutputAnalyzer_ExecuteQueuedPcmCapturerCallbacks(System.IntPtr analyzer, System.IntPtr callback) { }
        // RVA: 0x068DF78C  token: 0x60003CF
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000087  // size: 0x14
    public sealed struct CriAtomExPlayback
    {
        // Fields
        private System.UInt32 <id>k__BackingField;  // 0x10
        public static System.UInt32 invalidId;  // const

        // Properties
        System.UInt32 id { get; /* RVA: 0x02B77620 */ set; /* RVA: 0x04D86140 */ }
        CriWare.CriAtomExPlayback.Status status { get; /* RVA: 0x068EB85C */ }
        System.Int64 time { get; /* RVA: 0x068EB874 */ }
        System.Int64 timeSyncedWithAudio { get; /* RVA: 0x068EB868 */ }

        // Methods
        // RVA: 0x04D86140  token: 0x60003D8
        public System.Void .ctor(System.UInt32 id) { }
        // RVA: 0x068EB920  token: 0x60003D9
        public System.Void Stop(System.Boolean ignoresReleaseTime) { }
        // RVA: 0x068EB8A4  token: 0x60003DA
        public System.Void Pause() { }
        // RVA: 0x068EB8B0  token: 0x60003DB
        public System.Void Resume(CriWare.CriAtomEx.ResumeMode mode) { }
        // RVA: 0x068EB88C  token: 0x60003DC
        public System.Boolean IsPaused() { }
        // RVA: 0x068EB838  token: 0x60003DD
        public System.Boolean GetFormatInfo(CriWare.CriAtomEx.FormatInfo& info) { }
        // RVA: 0x068EB85C  token: 0x60003DE
        public CriWare.CriAtomExPlayback.Status GetStatus() { }
        // RVA: 0x068EB874  token: 0x60003DF
        public System.Int64 GetTime() { }
        // RVA: 0x068EB868  token: 0x60003E0
        public System.Int64 GetTimeSyncedWithAudio() { }
        // RVA: 0x068EB844  token: 0x60003E1
        public System.Boolean GetNumPlayedSamples(System.Int64& numSamples, System.Int32& samplingRate) { }
        // RVA: 0x068EB850  token: 0x60003E2
        public System.Int64 GetSequencePosition() { }
        // RVA: 0x068EB82C  token: 0x60003E3
        public System.Int32 GetCurrentBlockIndex() { }
        // RVA: 0x068EB880  token: 0x60003E4
        public System.Boolean GetTrackInfo(CriWare.CriAtomExPlayback.TrackInfo& info) { }
        // RVA: 0x068EB820  token: 0x60003E5
        public System.Boolean GetBeatSyncInfo(CriWare.CriAtomExBeatSync.Info& info) { }
        // RVA: 0x068EB8C8  token: 0x60003E6
        public System.Void SetNextBlockIndex(System.Int32 index) { }
        // RVA: 0x068EB8BC  token: 0x60003E7
        public System.Boolean SetBeatSyncOffset(System.Int16 timeMs) { }
        // RVA: 0x068EB984  token: 0x60003ED
        public System.Void Stop() { }
        // RVA: 0x068EB8D4  token: 0x60003EE
        public System.Void StopWithoutReleaseTime() { }
        // RVA: 0x068EB898  token: 0x60003EF
        public System.Void Pause(System.Boolean sw) { }
        // RVA: 0x068EC154  token: 0x60003F0
        private static System.Void criAtomExPlayback_Stop(System.UInt32 id) { }
        // RVA: 0x068EC0DC  token: 0x60003F1
        private static System.Void criAtomExPlayback_StopWithoutReleaseTime(System.UInt32 id) { }
        // RVA: 0x068EBED0  token: 0x60003F2
        private static System.Void criAtomExPlayback_Pause(System.UInt32 id, System.Boolean sw) { }
        // RVA: 0x068EBF54  token: 0x60003F3
        private static System.Void criAtomExPlayback_Resume(System.UInt32 id, CriWare.CriAtomEx.ResumeMode mode) { }
        // RVA: 0x068EBE58  token: 0x60003F4
        private static System.Boolean criAtomExPlayback_IsPaused(System.UInt32 id) { }
        // RVA: 0x068EBCF0  token: 0x60003F5
        private static CriWare.CriAtomExPlayback.Status criAtomExPlayback_GetStatus(System.UInt32 id) { }
        // RVA: 0x068EBAD0  token: 0x60003F6
        private static System.Boolean criAtomExPlayback_GetFormatInfo(System.UInt32 id, CriWare.CriAtomEx.FormatInfo& info) { }
        // RVA: 0x068EBDE0  token: 0x60003F7
        private static System.Int64 criAtomExPlayback_GetTime(System.UInt32 id) { }
        // RVA: 0x068EBD68  token: 0x60003F8
        private static System.Int64 criAtomExPlayback_GetTimeSyncedWithAudio(System.UInt32 id) { }
        // RVA: 0x068EBB58  token: 0x60003F9
        private static System.Boolean criAtomExPlayback_GetNumPlayedSamples(System.UInt32 id, System.Int64& num_samples, System.Int32& sampling_rate) { }
        // RVA: 0x068EBC78  token: 0x60003FA
        private static System.Int64 criAtomExPlayback_GetSequencePosition(System.UInt32 id) { }
        // RVA: 0x068EC05C  token: 0x60003FB
        private static System.Void criAtomExPlayback_SetNextBlockIndex(System.UInt32 id, System.Int32 index) { }
        // RVA: 0x068EBA58  token: 0x60003FC
        private static System.Int32 criAtomExPlayback_GetCurrentBlockIndex(System.UInt32 id) { }
        // RVA: 0x068EBBF0  token: 0x60003FD
        private static System.Boolean criAtomExPlayback_GetPlaybackTrackInfo(System.UInt32 id, CriWare.CriAtomExPlayback.TrackInfo& info) { }
        // RVA: 0x068EB9D0  token: 0x60003FE
        private static System.Boolean criAtomExPlayback_GetBeatSyncInfo(System.UInt32 id, CriWare.CriAtomExBeatSync.Info& info) { }
        // RVA: 0x068EBFD4  token: 0x60003FF
        private static System.Boolean criAtomExPlayback_SetBeatSyncOffset(System.UInt32 id, System.Int16 timeMs) { }

    }

    // TypeToken: 0x200008A  // size: 0x50
    public class CriAtomExPlayer : CriWare.CriDisposable
    {
        // Fields
        private CriWare.CriAtomExBeatSync.CbFunc _onBeatSyncCallback;  // 0x20
        private CriWare.CriAtomExSequencer.EventCallback _onSequenceCallback;  // 0x28
        private System.Boolean hasExistingNativeHandle;  // 0x30
        private System.IntPtr entryPoolHandle;  // 0x38
        private System.Int32 _entryPoolCapacity;  // 0x40
        private System.Int32 max_path;  // 0x44
        private System.IntPtr handle;  // 0x48

        // Properties
        System.IntPtr nativeHandle { get; /* RVA: 0x04D86200 */ }
        System.Boolean isAvailable { get; /* RVA: 0x068EF998 */ }
        System.Int32 entryPoolCapacity { get; /* RVA: 0x04D865E0 */ }

        // Events
        event CriWare.CriAtomExBeatSync.CbFunc _onBeatSyncCallback;
        event CriWare.CriAtomExBeatSync.CbFunc OnBeatSyncCallback;
        event CriWare.CriAtomExSequencer.EventCallback _onSequenceCallback;
        event CriWare.CriAtomExSequencer.EventCallback OnSequenceCallback;

        // Methods
        // RVA: 0x068ED3A4  token: 0x600040A
        public System.Void .ctor() { }
        // RVA: 0x068ED384  token: 0x600040B
        public System.Void .ctor(System.Int32 maxPath, System.Int32 maxPathStrings) { }
        // RVA: 0x068ED360  token: 0x600040C
        public System.Void .ctor(System.Boolean enableAudioSyncedTimer) { }
        // RVA: 0x068ED344  token: 0x600040D
        public System.Void .ctor(System.Int32 maxPath, System.Int32 maxPathStrings, System.Boolean enableAudioSyncedTimer) { }
        // RVA: 0x068ED320  token: 0x600040E
        public System.Void .ctor(System.IntPtr existingNativeHandle) { }
        // RVA: 0x068ED3C8  token: 0x600040F
        public System.Void .ctor(System.Int32 maxPath, System.Int32 maxPathStrings, System.Boolean enableAudioSyncedTimer, System.IntPtr existingNativeHandle) { }
        // RVA: 0x068EC8D8  token: 0x6000410
        public virtual System.Void Dispose() { }
        // RVA: 0x068ECFFC  token: 0x6000411
        public System.Void SetCue(CriWare.CriAtomExAcb acb, System.String name) { }
        // RVA: 0x068ED014  token: 0x6000412
        public System.Void SetCue(CriWare.CriAtomExAcb acb, System.Int32 id) { }
        // RVA: 0x068ECFD8  token: 0x6000413
        public System.Void SetCueIndex(CriWare.CriAtomExAcb acb, System.Int32 index) { }
        // RVA: 0x068ECFC0  token: 0x6000414
        public System.Void SetContentId(CriWare.CriFsBinder binder, System.Int32 contentId) { }
        // RVA: 0x068ED0F4  token: 0x6000415
        public System.Void SetFile(CriWare.CriFsBinder binder, System.String path) { }
        // RVA: 0x068ED038  token: 0x6000416
        public System.Void SetData(System.Byte[] buffer, System.Int32 size) { }
        // RVA: 0x068ED02C  token: 0x6000417
        public System.Void SetData(System.IntPtr buffer, System.Int32 size) { }
        // RVA: 0x068ED118  token: 0x6000418
        public System.Void SetFormat(CriWare.CriAtomEx.Format format) { }
        // RVA: 0x068ED130  token: 0x6000419
        public System.Void SetNumChannels(System.Int32 numChannels) { }
        // RVA: 0x068ED19C  token: 0x600041A
        public System.Void SetSamplingRate(System.Int32 samplingRate) { }
        // RVA: 0x068ECE68  token: 0x600041B
        public System.Void PrepareEntryPool(System.Int32 capacity, System.Boolean stopOnEmpty) { }
        // RVA: 0x068ECCF4  token: 0x600041C
        public System.Int32 GetNumEntries() { }
        // RVA: 0x068ECCD4  token: 0x600041D
        public System.Int32 GetNumConsumedEntries() { }
        // RVA: 0x068ECA8C  token: 0x600041F
        public System.Boolean EntryFile(CriWare.CriFsBinder binder, System.String path, System.Boolean repeat) { }
        // RVA: 0x068EC9EC  token: 0x6000420
        public System.Boolean EntryContentId(CriWare.CriFsBinder binder, System.Int32 contentId, System.Boolean repeat) { }
        // RVA: 0x068ECA68  token: 0x6000421
        public System.Boolean EntryData(System.Byte[] buffer, System.Int32 size, System.Boolean repeat) { }
        // RVA: 0x068ECA44  token: 0x6000422
        public System.Boolean EntryData(System.IntPtr buffer, System.Int32 size, System.Boolean repeat) { }
        // RVA: 0x068ECA18  token: 0x6000423
        public System.Boolean EntryCue(CriWare.CriAtomExAcb acb, System.String name, System.Boolean repeat) { }
        // RVA: 0x068ED21C  token: 0x6000424
        public CriWare.CriAtomExPlayback Start() { }
        // RVA: 0x068ECED4  token: 0x6000425
        public CriWare.CriAtomExPlayback Prepare() { }
        // RVA: 0x068ED27C  token: 0x6000426
        public System.Void Stop(System.Boolean ignoresReleaseTime) { }
        // RVA: 0x068ECE4C  token: 0x6000427
        public System.Void Pause() { }
        // RVA: 0x068ECEF8  token: 0x6000428
        public System.Void Resume(CriWare.CriAtomEx.ResumeMode mode) { }
        // RVA: 0x068ECD5C  token: 0x6000429
        public System.Boolean IsPaused() { }
        // RVA: 0x068ED210  token: 0x600042A
        public System.Void SetVolume(System.Single volume) { }
        // RVA: 0x068ED16C  token: 0x600042B
        public System.Void SetPitch(System.Single pitch) { }
        // RVA: 0x068ED178  token: 0x600042C
        public System.Void SetPlaybackRatio(System.Single ratio) { }
        // RVA: 0x068ED13C  token: 0x600042D
        public System.Void SetPan3dAngle(System.Single angle) { }
        // RVA: 0x068ED148  token: 0x600042E
        public System.Void SetPan3dInteriorDistance(System.Single distance) { }
        // RVA: 0x068ED154  token: 0x600042F
        public System.Void SetPan3dVolume(System.Single volume) { }
        // RVA: 0x068ED160  token: 0x6000430
        public System.Void SetPanType(CriWare.CriAtomEx.PanType panType) { }
        // RVA: 0x068ED1B4  token: 0x6000431
        public System.Void SetSendLevel(System.Int32 channel, CriWare.CriAtomEx.Speaker id, System.Single level) { }
        // RVA: 0x068ECF64  token: 0x6000432
        public System.Void SetBiquadFilterParameters(CriWare.CriAtomEx.BiquadFilterType type, System.Single frequency, System.Single gain, System.Single q) { }
        // RVA: 0x068ECF58  token: 0x6000433
        public System.Void SetBandpassFilterParameters(System.Single cofLow, System.Single cofHigh) { }
        // RVA: 0x068ECF90  token: 0x6000434
        public System.Void SetBusSendLevel(System.String busName, System.Single level) { }
        // RVA: 0x068ECCC8  token: 0x6000435
        public System.Boolean GetBusSendLevel(System.String busName, System.Single& level) { }
        // RVA: 0x068ECF9C  token: 0x6000436
        public System.Void SetBusSendLevel(System.Int32 busId, System.Single level) { }
        // RVA: 0x068ECF78  token: 0x6000437
        public System.Void SetBusSendLevelOffset(System.String busName, System.Single levelOffset) { }
        // RVA: 0x068ECCBC  token: 0x6000438
        public System.Boolean GetBusSendLevelOffset(System.String busName, System.Single& level) { }
        // RVA: 0x068ECF84  token: 0x6000439
        public System.Void SetBusSendLevelOffset(System.Int32 busId, System.Single levelOffset) { }
        // RVA: 0x068EC2EC  token: 0x600043A
        public System.Void AttachAisac(System.String globalAisacName) { }
        // RVA: 0x068EC890  token: 0x600043B
        public System.Void DetachAisac(System.String globalAisacName) { }
        // RVA: 0x068ECF34  token: 0x600043C
        public System.Void SetAisacControl(System.String controlName, System.Single value) { }
        // RVA: 0x068ECF34  token: 0x600043D
        public System.Void SetAisac(System.String controlName, System.Single value) { }
        // RVA: 0x068ECF40  token: 0x600043E
        public System.Void SetAisacControl(System.UInt32 controlId, System.Single value) { }
        // RVA: 0x068ECF40  token: 0x600043F
        public System.Void SetAisac(System.UInt32 controlId, System.Single value) { }
        // RVA: 0x068ECAC0  token: 0x6000440
        public System.Boolean GetAttachedAisacInfo(System.Int32 aisacAttachedIndex, CriWare.CriAtomEx.AisacInfo& aisacInfo) { }
        // RVA: 0x068ECF1C  token: 0x6000441
        public System.Void Set3dSource(CriWare.CriAtomEx3dSource source) { }
        // RVA: 0x068ECF04  token: 0x6000442
        public System.Void Set3dListener(CriWare.CriAtomEx3dListener listener) { }
        // RVA: 0x068ED1E0  token: 0x6000443
        public System.Void SetStartTime(System.Int64 startTimeMs) { }
        // RVA: 0x068ED10C  token: 0x6000444
        public System.Void SetFirstBlockIndex(System.Int32 index) { }
        // RVA: 0x068ED1A8  token: 0x6000445
        public System.Void SetSelectorLabel(System.String selector, System.String label) { }
        // RVA: 0x068ED2FC  token: 0x6000446
        public System.Void UnsetSelectorLabel(System.String selector) { }
        // RVA: 0x068EC884  token: 0x6000447
        public System.Void ClearSelectorLabels() { }
        // RVA: 0x068ECFA8  token: 0x6000448
        public System.Void SetCategory(System.Int32 categoryId) { }
        // RVA: 0x068ECFB4  token: 0x6000449
        public System.Void SetCategory(System.String categoryName) { }
        // RVA: 0x068ED2F0  token: 0x600044A
        public System.Void UnsetCategory() { }
        // RVA: 0x068ECFF0  token: 0x600044B
        public System.Void SetCuePriority(System.Int32 priority) { }
        // RVA: 0x068ED204  token: 0x600044C
        public System.Void SetVoicePriority(System.Int32 priority) { }
        // RVA: 0x068ED1EC  token: 0x600044D
        public System.Void SetVoiceControlMethod(CriWare.CriAtomEx.VoiceControlMethod method) { }
        // RVA: 0x068ED184  token: 0x600044E
        public System.Void SetPreDelayTime(System.Single time) { }
        // RVA: 0x068ED088  token: 0x600044F
        public System.Void SetEnvelopeAttackTime(System.Single time) { }
        // RVA: 0x068ED0A0  token: 0x6000450
        public System.Void SetEnvelopeHoldTime(System.Single time) { }
        // RVA: 0x068ED094  token: 0x6000451
        public System.Void SetEnvelopeDecayTime(System.Single time) { }
        // RVA: 0x068ED0AC  token: 0x6000452
        public System.Void SetEnvelopeReleaseTime(System.Single time) { }
        // RVA: 0x068ED0B8  token: 0x6000453
        public System.Void SetEnvelopeSustainLevel(System.Single level) { }
        // RVA: 0x068EC2F8  token: 0x6000454
        public System.Void AttachFader() { }
        // RVA: 0x068EC89C  token: 0x6000455
        public System.Void DetachFader() { }
        // RVA: 0x068ED0E8  token: 0x6000456
        public System.Void SetFadeOutTime(System.Int32 ms) { }
        // RVA: 0x068ED0D0  token: 0x6000457
        public System.Void SetFadeInTime(System.Int32 ms) { }
        // RVA: 0x068ED0C4  token: 0x6000458
        public System.Void SetFadeInStartOffset(System.Int32 ms) { }
        // RVA: 0x068ED0DC  token: 0x6000459
        public System.Void SetFadeOutEndDelay(System.Int32 ms) { }
        // RVA: 0x068ECD50  token: 0x600045A
        public System.Boolean IsFading() { }
        // RVA: 0x068ECEE0  token: 0x600045B
        public System.Void ResetFaderParameters() { }
        // RVA: 0x068ED124  token: 0x600045C
        public System.Void SetGroupNumber(System.Int32 group_no) { }
        // RVA: 0x068ED314  token: 0x600045D
        public System.Void Update(CriWare.CriAtomExPlayback playback) { }
        // RVA: 0x068ED308  token: 0x600045E
        public System.Void UpdateAll() { }
        // RVA: 0x068ECEEC  token: 0x600045F
        public System.Void ResetParameters() { }
        // RVA: 0x068ECD44  token: 0x6000460
        public System.Int64 GetTime() { }
        // RVA: 0x068ECD38  token: 0x6000461
        public CriWare.CriAtomExPlayer.Status GetStatus() { }
        // RVA: 0x068ECD14  token: 0x6000462
        public System.Single GetParameterFloat32(CriWare.CriAtomEx.Parameter id) { }
        // RVA: 0x068ECD2C  token: 0x6000463
        public System.UInt32 GetParameterUint32(CriWare.CriAtomEx.Parameter id) { }
        // RVA: 0x068ECD20  token: 0x6000464
        public System.Int32 GetParameterSint32(CriWare.CriAtomEx.Parameter id) { }
        // RVA: 0x068ED1D4  token: 0x6000465
        public System.Void SetSoundRendererType(CriWare.CriAtomEx.SoundRendererType type) { }
        // RVA: 0x068ED190  token: 0x6000466
        public System.Void SetRandomSeed(System.UInt32 seed) { }
        // RVA: 0x068ECD68  token: 0x6000467
        public System.Void Loop(System.Boolean sw) { }
        // RVA: 0x068ECF4C  token: 0x6000468
        public System.Void SetAsrRackId(System.Int32 asr_rack_id) { }
        // RVA: 0x068ED1F8  token: 0x6000469
        public System.Void SetVoicePoolIdentifier(System.UInt32 identifier) { }
        // RVA: 0x068ED074  token: 0x600046A
        public System.Void SetDspTimeStretchRatio(System.Single ratio) { }
        // RVA: 0x068ED050  token: 0x600046B
        public System.Void SetDspPitchShifterPitch(System.Single pitch) { }
        // RVA: 0x068ED044  token: 0x600046C
        public System.Void SetDspParameter(System.Int32 id, System.Single value) { }
        // RVA: 0x068ED1C8  token: 0x600046D
        public System.Void SetSequencePrepareTime(System.UInt32 ms) { }
        // RVA: 0x068EC31C  token: 0x600046E
        public System.Void AttachTween(CriWare.CriAtomExTween tween) { }
        // RVA: 0x068EC8B4  token: 0x600046F
        public System.Void DetachTween(CriWare.CriAtomExTween tween) { }
        // RVA: 0x068EC8A8  token: 0x6000470
        public System.Void DetachTweenAll() { }
        // RVA: 0x068ED2BC  token: 0x6000471
        public System.Void Stop() { }
        // RVA: 0x068ED248  token: 0x6000472
        public System.Void StopWithoutReleaseTime() { }
        // RVA: 0x068ECE5C  token: 0x6000473
        public System.Void Pause(System.Boolean sw) { }
        // RVA: 0x068DA7D8  token: 0x6000474
        protected virtual System.Void Finalize() { }
        // RVA: 0x068ECDE4  token: 0x6000475
        private System.Void OnBeatSyncCallbackChainInternal(CriWare.CriAtomExBeatSync.Info& info) { }
        // RVA: 0x068ECE18  token: 0x6000476
        private System.Void OnSequenceCallbackChainInternal(CriWare.CriAtomExSequencer.CriAtomExSequenceEventInfo& info) { }
        // RVA: 0x068ED97C  token: 0x6000477
        private static System.IntPtr criAtomExPlayer_Create(CriWare.CriAtomExPlayer.Config& config, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068EDA90  token: 0x6000478
        private static System.Void criAtomExPlayer_Destroy(System.IntPtr player) { }
        // RVA: 0x068EE8C8  token: 0x6000479
        private static System.Void criAtomExPlayer_SetCueId(System.IntPtr player, System.IntPtr acb_hn, System.Int32 id) { }
        // RVA: 0x068EE9F0  token: 0x600047A
        private static System.Void criAtomExPlayer_SetCueName(System.IntPtr player, System.IntPtr acb_hn, System.String cue_name) { }
        // RVA: 0x068EE95C  token: 0x600047B
        private static System.Void criAtomExPlayer_SetCueIndex(System.IntPtr player, System.IntPtr acb_hn, System.Int32 index) { }
        // RVA: 0x068EEEE4  token: 0x600047C
        private static System.Void criAtomExPlayer_SetFile(System.IntPtr player, System.IntPtr binder, System.String path) { }
        // RVA: 0x068EEB6C  token: 0x600047D
        private static System.Void criAtomExPlayer_SetData(System.IntPtr player, System.Byte[] buffer, System.Int32 size) { }
        // RVA: 0x068EEAD8  token: 0x600047E
        private static System.Void criAtomExPlayer_SetData(System.IntPtr player, System.IntPtr buffer, System.Int32 size) { }
        // RVA: 0x068EE834  token: 0x600047F
        private static System.Void criAtomExPlayer_SetContentId(System.IntPtr player, System.IntPtr binder, System.Int32 id) { }
        // RVA: 0x068EF674  token: 0x6000480
        private static System.Void criAtomExPlayer_SetVoicePoolIdentifier(System.IntPtr player, System.UInt32 identifier) { }
        // RVA: 0x068EF774  token: 0x6000481
        private static System.UInt32 criAtomExPlayer_Start(System.IntPtr player) { }
        // RVA: 0x068EE164  token: 0x6000482
        private static System.UInt32 criAtomExPlayer_Prepare(System.IntPtr player) { }
        // RVA: 0x068EF820  token: 0x6000483
        private static System.Void criAtomExPlayer_Stop(System.IntPtr player) { }
        // RVA: 0x068EF7EC  token: 0x6000484
        private static System.Void criAtomExPlayer_StopWithoutReleaseTime(System.IntPtr player) { }
        // RVA: 0x068EE0E0  token: 0x6000485
        private static System.Void criAtomExPlayer_Pause(System.IntPtr player, System.Boolean sw) { }
        // RVA: 0x068EE288  token: 0x6000486
        private static System.Void criAtomExPlayer_Resume(System.IntPtr player, CriWare.CriAtomEx.ResumeMode mode) { }
        // RVA: 0x068EE068  token: 0x6000487
        private static System.Boolean criAtomExPlayer_IsPaused(System.IntPtr player) { }
        // RVA: 0x068EDF84  token: 0x6000488
        private static CriWare.CriAtomExPlayer.Status criAtomExPlayer_GetStatus(System.IntPtr player) { }
        // RVA: 0x068EDFB8  token: 0x6000489
        private static System.Int64 criAtomExPlayer_GetTime(System.IntPtr player) { }
        // RVA: 0x068EEFCC  token: 0x600048A
        private static System.Void criAtomExPlayer_SetFormat(System.IntPtr player, CriWare.CriAtomEx.Format format) { }
        // RVA: 0x068EF090  token: 0x600048B
        private static System.Void criAtomExPlayer_SetNumChannels(System.IntPtr player, System.Int32 num_channels) { }
        // RVA: 0x068EF350  token: 0x600048C
        private static System.Void criAtomExPlayer_SetSamplingRate(System.IntPtr player, System.Int32 sampling_rate) { }
        // RVA: 0x068EC728  token: 0x600048D
        private static System.IntPtr CRIWAREF1DB6C3C(System.IntPtr player, System.Int32 capacity, System.Int32 max_path, System.Boolean stopOnEmpty) { }
        // RVA: 0x068EC45C  token: 0x600048E
        private static System.Void CRIWARE348B99F2(System.IntPtr pool) { }
        // RVA: 0x068EC6C0  token: 0x600048F
        private static System.Int32 CRIWAREB0CA8CB3(System.IntPtr pool) { }
        // RVA: 0x068EC7CC  token: 0x6000490
        private static System.Int32 CRIWAREF2104D05(System.IntPtr pool) { }
        // RVA: 0x068EC6F4  token: 0x6000491
        private static System.Void CRIWAREBAFBC575(System.IntPtr pool) { }
        // RVA: 0x068EC490  token: 0x6000492
        private static System.Boolean CRIWARE5277731E(System.IntPtr pool, System.IntPtr binder, System.String path, System.Boolean repeat, System.Int32 max_path) { }
        // RVA: 0x068EC624  token: 0x6000493
        private static System.Boolean CRIWARE78E74FE0(System.IntPtr pool, System.IntPtr binder, System.Int32 id, System.Boolean repeat) { }
        // RVA: 0x068EC340  token: 0x6000494
        private static System.Boolean CRIWARE10CB0D07(System.IntPtr pool, System.Byte[] buffer, System.Int32 size, System.Boolean repeat) { }
        // RVA: 0x068EC3F4  token: 0x6000495
        private static System.Boolean CRIWARE10CB0D07(System.IntPtr pool, System.IntPtr buffer, System.Int32 size, System.Boolean repeat) { }
        // RVA: 0x068EC560  token: 0x6000496
        private static System.Boolean CRIWARE59CCEB17(System.IntPtr pool, System.IntPtr acbhn, System.String name, System.Boolean repeat) { }
        // RVA: 0x068EC68C  token: 0x6000497
        private static System.Void CRIWARE94DBB2A3(System.IntPtr pool) { }
        // RVA: 0x068EF56C  token: 0x6000498
        private static System.Void criAtomExPlayer_SetStartTime(System.IntPtr player, System.Int64 start_time_ms) { }
        // RVA: 0x068EF4EC  token: 0x6000499
        private static System.Void criAtomExPlayer_SetSequencePrepareTime(System.IntPtr player, System.UInt32 seq_prep_time_ms) { }
        // RVA: 0x068EE0A0  token: 0x600049A
        private static System.Void criAtomExPlayer_LimitLoopCount(System.IntPtr player, System.Int32 count) { }
        // RVA: 0x068EF914  token: 0x600049B
        private static System.Void criAtomExPlayer_Update(System.IntPtr player, System.UInt32 id) { }
        // RVA: 0x068EF8E0  token: 0x600049C
        private static System.Void criAtomExPlayer_UpdateAll(System.IntPtr player) { }
        // RVA: 0x068EE254  token: 0x600049D
        private static System.Void criAtomExPlayer_ResetParameters(System.IntPtr player) { }
        // RVA: 0x068EDDC8  token: 0x600049E
        private static System.Single criAtomExPlayer_GetParameterFloat32(System.IntPtr player, CriWare.CriAtomEx.Parameter id) { }
        // RVA: 0x068EDE88  token: 0x600049F
        private static System.UInt32 criAtomExPlayer_GetParameterUint32(System.IntPtr player, CriWare.CriAtomEx.Parameter id) { }
        // RVA: 0x068EDE04  token: 0x60004A0
        private static System.Int32 criAtomExPlayer_GetParameterSint32(System.IntPtr player, CriWare.CriAtomEx.Parameter id) { }
        // RVA: 0x068EDF0C  token: 0x60004A1
        private static System.IntPtr criAtomExPlayer_GetPlayerParameter(System.IntPtr player) { }
        // RVA: 0x068ED784  token: 0x60004A2
        private static System.Void criAtomExPlayerParameter_RemoveParameter(System.IntPtr player_parameter, System.UInt16 id) { }
        // RVA: 0x068EF738  token: 0x60004A3
        private static System.Void criAtomExPlayer_SetVolume(System.IntPtr player, System.Single volume) { }
        // RVA: 0x068EF218  token: 0x60004A4
        private static System.Void criAtomExPlayer_SetPitch(System.IntPtr player, System.Single pitch) { }
        // RVA: 0x068EF254  token: 0x60004A5
        private static System.Void criAtomExPlayer_SetPlaybackRatio(System.IntPtr player, System.Single playback_ratio) { }
        // RVA: 0x068EF0D0  token: 0x60004A6
        private static System.Void criAtomExPlayer_SetPan3dAngle(System.IntPtr player, System.Single pan3d_angle) { }
        // RVA: 0x068EF10C  token: 0x60004A7
        private static System.Void criAtomExPlayer_SetPan3dInteriorDistance(System.IntPtr player, System.Single pan3d_interior_distance) { }
        // RVA: 0x068EF150  token: 0x60004A8
        private static System.Void criAtomExPlayer_SetPan3dVolume(System.IntPtr player, System.Single pan3d_volume) { }
        // RVA: 0x068EF194  token: 0x60004A9
        private static System.Void criAtomExPlayer_SetPanType(System.IntPtr player, CriWare.CriAtomEx.PanType panType) { }
        // RVA: 0x068EF490  token: 0x60004AA
        private static System.Void criAtomExPlayer_SetSendLevel(System.IntPtr player, System.Int32 channel, CriWare.CriAtomEx.Speaker id, System.Single level) { }
        // RVA: 0x068EE6C8  token: 0x60004AB
        private static System.Void criAtomExPlayer_SetBusSendLevel(System.IntPtr player, System.Int32 bus_id, System.Single level) { }
        // RVA: 0x068EE5B4  token: 0x60004AC
        private static System.Void criAtomExPlayer_SetBusSendLevelByName(System.IntPtr player, System.String bus_name, System.Single level) { }
        // RVA: 0x068EDC60  token: 0x60004AD
        private static System.Boolean criAtomExPlayer_GetBusSendLevelByName(System.IntPtr player, System.String bus_name, System.Single& level) { }
        // RVA: 0x068EE67C  token: 0x60004AE
        private static System.Void criAtomExPlayer_SetBusSendLevelOffset(System.IntPtr player, System.Int32 bus_id, System.Single level_offset) { }
        // RVA: 0x068EE61C  token: 0x60004AF
        private static System.Void criAtomExPlayer_SetBusSendLevelOffsetByName(System.IntPtr player, System.String bus_name, System.Single level_offset) { }
        // RVA: 0x068EDD14  token: 0x60004B0
        private static System.Boolean criAtomExPlayer_GetBusSendLevelOffsetByName(System.IntPtr player, System.String bus_name, System.Single& level_offset) { }
        // RVA: 0x068EE50C  token: 0x60004B1
        private static System.Void criAtomExPlayer_SetBandpassFilterParameters(System.IntPtr player, System.Single cof_low, System.Single cof_high) { }
        // RVA: 0x068EE558  token: 0x60004B2
        private static System.Void criAtomExPlayer_SetBiquadFilterParameters(System.IntPtr player, CriWare.CriAtomEx.BiquadFilterType type, System.Single frequency, System.Single gain, System.Single q) { }
        // RVA: 0x068EF6B4  token: 0x60004B3
        private static System.Void criAtomExPlayer_SetVoicePriority(System.IntPtr player, System.Int32 priority) { }
        // RVA: 0x068EF5F0  token: 0x60004B4
        private static System.Void criAtomExPlayer_SetVoiceControlMethod(System.IntPtr player, CriWare.CriAtomEx.VoiceControlMethod method) { }
        // RVA: 0x068EE3D4  token: 0x60004B5
        private static System.Void criAtomExPlayer_SetAisacControlById(System.IntPtr player, System.UInt16 control_id, System.Single control_value) { }
        // RVA: 0x068EE420  token: 0x60004B6
        private static System.Void criAtomExPlayer_SetAisacControlByName(System.IntPtr player, System.String control_name, System.Single control_value) { }
        // RVA: 0x068EE350  token: 0x60004B7
        private static System.Void criAtomExPlayer_Set3dSourceHn(System.IntPtr player, System.IntPtr source) { }
        // RVA: 0x068EE30C  token: 0x60004B8
        private static System.Void criAtomExPlayer_Set3dListenerHn(System.IntPtr player, System.IntPtr listener) { }
        // RVA: 0x068EE718  token: 0x60004B9
        private static System.Void criAtomExPlayer_SetCategoryById(System.IntPtr player, System.UInt32 category_id) { }
        // RVA: 0x068EE79C  token: 0x60004BA
        private static System.Void criAtomExPlayer_SetCategoryByName(System.IntPtr player, System.String category_name) { }
        // RVA: 0x068EF854  token: 0x60004BB
        private static System.Void criAtomExPlayer_UnsetCategory(System.IntPtr player) { }
        // RVA: 0x068EEA98  token: 0x60004BC
        private static System.Void criAtomExPlayer_SetCuePriority(System.IntPtr player, System.Int32 cue_priority) { }
        // RVA: 0x068EF290  token: 0x60004BD
        private static System.Void criAtomExPlayer_SetPreDelayTime(System.IntPtr player, System.Single predelay_time_ms) { }
        // RVA: 0x068EEC5C  token: 0x60004BE
        private static System.Void criAtomExPlayer_SetEnvelopeAttackTime(System.IntPtr player, System.Single attack_time_ms) { }
        // RVA: 0x068EECDC  token: 0x60004BF
        private static System.Void criAtomExPlayer_SetEnvelopeHoldTime(System.IntPtr player, System.Single hold_time_ms) { }
        // RVA: 0x068EECA0  token: 0x60004C0
        private static System.Void criAtomExPlayer_SetEnvelopeDecayTime(System.IntPtr player, System.Single decay_time_ms) { }
        // RVA: 0x068EED18  token: 0x60004C1
        private static System.Void criAtomExPlayer_SetEnvelopeReleaseTime(System.IntPtr player, System.Single release_time_ms) { }
        // RVA: 0x068EED5C  token: 0x60004C2
        private static System.Void criAtomExPlayer_SetEnvelopeSustainLevel(System.IntPtr player, System.Single susutain_level) { }
        // RVA: 0x068ED860  token: 0x60004C3
        private static System.Void criAtomExPlayer_AttachFader(System.IntPtr player, System.IntPtr config, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068ED808  token: 0x60004C4
        private static System.Void criAtomExPlayer_AttachAisac(System.IntPtr player, System.String globalAisacName) { }
        // RVA: 0x068EDAC4  token: 0x60004C5
        private static System.Void criAtomExPlayer_DetachAisac(System.IntPtr player, System.String globalAisacName) { }
        // RVA: 0x068EDB1C  token: 0x60004C6
        private static System.Void criAtomExPlayer_DetachFader(System.IntPtr player) { }
        // RVA: 0x068EEEA4  token: 0x60004C7
        private static System.Void criAtomExPlayer_SetFadeOutTime(System.IntPtr player, System.Int32 ms) { }
        // RVA: 0x068EEDE0  token: 0x60004C8
        private static System.Void criAtomExPlayer_SetFadeInTime(System.IntPtr player, System.Int32 ms) { }
        // RVA: 0x068EEDA0  token: 0x60004C9
        private static System.Void criAtomExPlayer_SetFadeInStartOffset(System.IntPtr player, System.Int32 ms) { }
        // RVA: 0x068EEE64  token: 0x60004CA
        private static System.Void criAtomExPlayer_SetFadeOutEndDelay(System.IntPtr player, System.Int32 ms) { }
        // RVA: 0x068EE030  token: 0x60004CB
        private static System.Boolean criAtomExPlayer_IsFading(System.IntPtr player) { }
        // RVA: 0x068EE1DC  token: 0x60004CC
        private static System.Void criAtomExPlayer_ResetFaderParameters(System.IntPtr player) { }
        // RVA: 0x068EF050  token: 0x60004CD
        private static System.Void criAtomExPlayer_SetGroupNumber(System.IntPtr player, System.Int32 group_no) { }
        // RVA: 0x068EDBC8  token: 0x60004CE
        private static System.Boolean criAtomExPlayer_GetAttachedAisacInfo(System.IntPtr player, System.Int32 aisac_attached_index, System.IntPtr aisac_info) { }
        // RVA: 0x068EEF8C  token: 0x60004CF
        private static System.Void criAtomExPlayer_SetFirstBlockIndex(System.IntPtr player, System.Int32 index) { }
        // RVA: 0x068EF3D4  token: 0x60004D0
        private static System.Void criAtomExPlayer_SetSelectorLabel(System.IntPtr player, System.String selector, System.String label) { }
        // RVA: 0x068EF888  token: 0x60004D1
        private static System.Void criAtomExPlayer_UnsetSelectorLabel(System.IntPtr player, System.String selector) { }
        // RVA: 0x068ED948  token: 0x60004D2
        private static System.Void criAtomExPlayer_ClearSelectorLabels(System.IntPtr player) { }
        // RVA: 0x068EF52C  token: 0x60004D3
        private static System.Void criAtomExPlayer_SetSoundRendererType(System.IntPtr player, CriWare.CriAtomEx.SoundRendererType type) { }
        // RVA: 0x068EF2CC  token: 0x60004D4
        private static System.Void criAtomExPlayer_SetRandomSeed(System.IntPtr player, System.UInt32 seed) { }
        // RVA: 0x068EC800  token: 0x60004D5
        private static System.Void CRIWAREF466419F(System.IntPtr player, System.Boolean sw) { }
        // RVA: 0x068EE488  token: 0x60004D6
        private static System.Void criAtomExPlayer_SetAsrRackId(System.IntPtr player, System.Int32 asr_rack_id) { }
        // RVA: 0x068EEC0C  token: 0x60004D7
        private static System.Void criAtomExPlayer_SetDspParameter(System.IntPtr player, System.Int32 id, System.Single value) { }
        // RVA: 0x068ED904  token: 0x60004D8
        private static System.Void criAtomExPlayer_AttachTween(System.IntPtr player, System.IntPtr tween) { }
        // RVA: 0x068EDB84  token: 0x60004D9
        private static System.Void criAtomExPlayer_DetachTween(System.IntPtr player, System.IntPtr tween) { }
        // RVA: 0x068EDB50  token: 0x60004DA
        private static System.Void criAtomExPlayer_DetachTweenAll(System.IntPtr player) { }

    }

    // TypeToken: 0x200008F  // size: 0x58
    public class CriAtomExPlayerOutputAnalyzer : CriWare.CriAtomExOutputAnalyzer
    {
        // Methods
        // RVA: 0x068EC1CC  token: 0x60004DB
        public System.Void .ctor(CriWare.CriAtomExPlayerOutputAnalyzer.Type[] types, CriWare.CriAtomExPlayerOutputAnalyzer.Config[] configs) { }

    }

    // TypeToken: 0x2000092  // size: 0x28
    public class CriAtomExSoundObject : CriWare.CriDisposable
    {
        // Fields
        private System.IntPtr handle;  // 0x20

        // Properties
        System.IntPtr nativeHandle { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x068EFE7C  token: 0x60004DE
        public System.Void .ctor(System.Boolean enableVoiceLimitScope, System.Boolean enableCategoryCueLimitScope) { }
        // RVA: 0x068EFE00  token: 0x60004DF
        public virtual System.Void Dispose() { }
        // RVA: 0x068EFDAC  token: 0x60004E0
        public System.Void AddPlayer(CriWare.CriAtomExPlayer player) { }
        // RVA: 0x068EFDDC  token: 0x60004E1
        public System.Void DeletePlayer(CriWare.CriAtomExPlayer player) { }
        // RVA: 0x068EFDD0  token: 0x60004E2
        public System.Void DeleteAllPlayers() { }
        // RVA: 0x068DA7D8  token: 0x60004E3
        protected virtual System.Void Finalize() { }
        // RVA: 0x068F0004  token: 0x60004E4
        private static System.IntPtr criAtomExSoundObject_Create(CriWare.CriAtomExSoundObject.Config& config, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068F0184  token: 0x60004E5
        private static System.Void criAtomExSoundObject_Destroy(System.IntPtr soundObject) { }
        // RVA: 0x068EFF80  token: 0x60004E6
        private static System.Void criAtomExSoundObject_AddPlayer(System.IntPtr soundObject, System.IntPtr player) { }
        // RVA: 0x068F0140  token: 0x60004E7
        private static System.Void criAtomExSoundObject_DeletePlayer(System.IntPtr soundObject, System.IntPtr player) { }
        // RVA: 0x068F00C8  token: 0x60004E8
        private static System.Void criAtomExSoundObject_DeleteAllPlayers(System.IntPtr soundObject) { }

    }

    // TypeToken: 0x2000094  // size: 0x28
    public class CriAtomExTween : CriWare.CriDisposable
    {
        // Fields
        private System.IntPtr handle;  // 0x20

        // Properties
        System.IntPtr nativeHandle { get; /* RVA: 0x04D862C0 */ }
        System.Single Value { get; /* RVA: 0x068F0914 */ }
        System.Boolean IsActive { get; /* RVA: 0x068F0908 */ }

        // Methods
        // RVA: 0x068F0524  token: 0x60004EA
        public System.Void .ctor() { }
        // RVA: 0x068F0644  token: 0x60004EB
        public System.Void .ctor(CriWare.CriAtomEx.Parameter parameterId) { }
        // RVA: 0x068F0634  token: 0x60004EC
        public System.Void .ctor(System.UInt32 aisacId) { }
        // RVA: 0x068F0534  token: 0x60004ED
        public System.Void .ctor(CriWare.CriAtomExTween.ParameterType parameterType, System.UInt32 targetId) { }
        // RVA: 0x068F03E8  token: 0x60004EE
        public virtual System.Void Dispose() { }
        // RVA: 0x068F0500  token: 0x60004F1
        public System.Void MoveTo(System.UInt16 durationMs, System.Single value) { }
        // RVA: 0x068F04F4  token: 0x60004F2
        public System.Void MoveFrom(System.UInt16 durationMs, System.Single value) { }
        // RVA: 0x068F0518  token: 0x60004F3
        public System.Void Stop() { }
        // RVA: 0x068F050C  token: 0x60004F4
        public System.Void Reset() { }
        // RVA: 0x068F0480  token: 0x60004F5
        protected virtual System.Void Finalize() { }
        // RVA: 0x068F03F4  token: 0x60004F6
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068F0654  token: 0x60004F7
        private static System.IntPtr criAtomExTween_Create(CriWare.CriAtomExTween.Config& config, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068F06E8  token: 0x60004F8
        private static System.Void criAtomExTween_Destroy(System.IntPtr tween) { }
        // RVA: 0x068F0760  token: 0x60004F9
        private static System.Single criAtomExTween_GetValue(System.IntPtr tween) { }
        // RVA: 0x068F0810  token: 0x60004FA
        private static System.Void criAtomExTween_MoveTo(System.IntPtr tween, System.UInt16 time_ms, System.Single value) { }
        // RVA: 0x068F07C4  token: 0x60004FB
        private static System.Void criAtomExTween_MoveFrom(System.IntPtr tween, System.UInt16 time_ms, System.Single value) { }
        // RVA: 0x068F0890  token: 0x60004FC
        private static System.Void criAtomExTween_Stop(System.IntPtr tween) { }
        // RVA: 0x068F085C  token: 0x60004FD
        private static System.Void criAtomExTween_Reset(System.IntPtr tween) { }
        // RVA: 0x068F078C  token: 0x60004FE
        private static System.Boolean criAtomExTween_IsActive(System.IntPtr tween) { }

    }

    // TypeToken: 0x2000098  // size: 0x38
    public abstract class CriAtomExVoicePool : CriWare.CriDisposable
    {
        // Fields
        public static System.Int32 StandardMemoryAsrVoicePoolId;  // const
        public static System.Int32 StandardStreamingAsrVoicePoolId;  // const
        public static System.Int32 StandardMemoryNsrVoicePoolId;  // const
        public static System.Int32 StandardStreamingNsrVoicePoolId;  // const
        protected System.IntPtr _handle;  // 0x20
        protected System.UInt32 _identifier;  // 0x28
        protected System.Int32 _numVoices;  // 0x2c
        protected System.Int32 _maxChannels;  // 0x30
        protected System.Int32 _maxSamplingRate;  // 0x34

        // Properties
        System.IntPtr nativeHandle { get; /* RVA: 0x04D862C0 */ }
        System.UInt32 identifier { get; /* RVA: 0x011F2A90 */ }
        System.Int32 numVoices { get; /* RVA: 0x04D86710 */ }
        System.Int32 maxChannels { get; /* RVA: 0x04D864F0 */ }
        System.Int32 maxSamplingRate { get; /* RVA: 0x04D86730 */ }

        // Methods
        // RVA: 0x068F0AD4  token: 0x60004FF
        public static CriWare.CriAtomExVoicePool.UsedVoicesInfo GetNumUsedVoices(CriWare.CriAtomExVoicePool.VoicePoolId voicePoolId) { }
        // RVA: 0x068F0A58  token: 0x6000505
        public virtual System.Void Dispose() { }
        // RVA: 0x068F0B00  token: 0x6000506
        public CriWare.CriAtomExVoicePool.UsedVoicesInfo GetNumUsedVoices() { }
        // RVA: 0x068F096C  token: 0x6000507
        public System.Void AttachDspTimeStretch() { }
        // RVA: 0x068F0920  token: 0x6000508
        public System.Void AttachDspPitchShifter(CriWare.CriAtomExVoicePool.PitchShifterMode mode, System.Int32 windosSize, System.Int32 overlapTimes) { }
        // RVA: 0x068F0A44  token: 0x6000509
        public System.Void DetachDsp() { }
        // RVA: 0x068DA7D8  token: 0x600050A
        protected virtual System.Void Finalize() { }
        // RVA: 0x068F09B0  token: 0x600050B
        private static System.Void CRIWARED389B244(System.Int32 voice_pool_id, System.Int32& num_used_voices, System.Int32& num_pool_voices) { }
        // RVA: 0x068F0CF4  token: 0x600050C
        private static System.Void criAtomExVoicePool_GetNumUsedVoices(System.IntPtr pool, System.Int32& num_used_voices, System.Int32& num_pool_voices) { }
        // RVA: 0x068F0CC0  token: 0x600050D
        public static System.Void criAtomExVoicePool_Free(System.IntPtr pool) { }
        // RVA: 0x068F0BE8  token: 0x600050E
        private static System.Void criAtomExVoicePool_AttachDspTimeStretch(System.IntPtr pool, CriWare.CriAtomExVoicePool.ExTimeStretchConfig& config, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068F0B44  token: 0x600050F
        private static System.Void criAtomExVoicePool_AttachDspPitchShifter(System.IntPtr pool, CriWare.CriAtomExVoicePool.ExPitchShifterConfig& config, System.IntPtr work, System.Int32 work_size) { }
        // RVA: 0x068F0C8C  token: 0x6000510
        private static System.Void criAtomExVoicePool_DetachDsp(System.IntPtr pool) { }
        // RVA: 0x068F0B34  token: 0x6000511
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000A2  // size: 0x38
    public class CriAtomExStandardVoicePool : CriWare.CriAtomExVoicePool
    {
        // Methods
        // RVA: 0x068F01B8  token: 0x6000512
        public System.Void .ctor(System.Int32 numVoices, System.Int32 maxChannels, System.Int32 maxSamplingRate, System.Boolean streamingFlag, System.UInt32 identifier) { }
        // RVA: 0x068F02E4  token: 0x6000513
        private static System.IntPtr criAtomExVoicePool_AllocateStandardVoicePool(CriWare.CriAtomExVoicePool.VoicePoolConfig& config, System.IntPtr work, System.Int32 work_size) { }

    }

    // TypeToken: 0x20000A3  // size: 0x38
    public class CriAtomExWaveVoicePool : CriWare.CriAtomExVoicePool
    {
        // Methods
        // RVA: 0x068F0D88  token: 0x6000514
        public System.Void .ctor(System.Int32 numVoices, System.Int32 maxChannels, System.Int32 maxSamplingRate, System.Boolean streamingFlag, System.UInt32 identifier) { }
        // RVA: 0x068F0EB4  token: 0x6000515
        private static System.IntPtr criAtomExVoicePool_AllocateWaveVoicePool(CriWare.CriAtomExVoicePool.VoicePoolConfig& config, System.IntPtr work, System.Int32 work_size) { }

    }

    // TypeToken: 0x20000A4  // size: 0x38
    public class CriAtomExRawPcmVoicePool : CriWare.CriAtomExVoicePool
    {
        // Methods
        // RVA: 0x068EFBF4  token: 0x6000516
        public System.Void .ctor(System.Int32 numVoices, System.Int32 maxChannels, System.Int32 maxSamplingRate, CriWare.CriAtomExRawPcmVoicePool.RawPcmFormat format, System.UInt32 identifier) { }
        // RVA: 0x068EFD1C  token: 0x6000517
        private static System.IntPtr criAtomExVoicePool_AllocateRawPcmVoicePool(CriWare.CriAtomExRawPcmVoicePool.RawPcmVoicePoolConfig& config, System.IntPtr work, System.Int32 work_size) { }

    }

    // TypeToken: 0x20000A8  // size: 0x28
    public class CriAudioReadStream
    {
        // Fields
        private CriWare.CriAudioReadStream.InternalDelegate internalDelegate;  // 0x10
        private System.IntPtr <callbackFunction>k__BackingField;  // 0x18
        private System.IntPtr <callbackPointer>k__BackingField;  // 0x20

        // Properties
        System.IntPtr callbackFunction { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x011EC9D0 */ }
        System.IntPtr callbackPointer { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x04D86500 */ }

        // Methods
        // RVA: 0x04D88FE0  token: 0x600051C
        public System.Void .ctor(System.IntPtr callbackFunction, System.IntPtr callbackPointer) { }
        // RVA: 0x068F0FB8  token: 0x600051D
        public System.Void .ctor(CriWare.CriAudioReadStream.Delegate callback, System.Int32 numChannels, System.Int32 bufferSize) { }

    }

    // TypeToken: 0x20000AC  // size: 0x28
    public class CriAudioWriteStream
    {
        // Fields
        private CriWare.CriAudioWriteStream.InternalDelegate internalDelegate;  // 0x10
        private System.IntPtr <callbackFunction>k__BackingField;  // 0x18
        private System.IntPtr <callbackPointer>k__BackingField;  // 0x20

        // Properties
        System.IntPtr callbackFunction { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x011EC9D0 */ }
        System.IntPtr callbackPointer { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x04D86500 */ }

        // Methods
        // RVA: 0x04D88FE0  token: 0x600052C
        public System.Void .ctor(System.IntPtr callbackFunction, System.IntPtr callbackPointer) { }
        // RVA: 0x068F1138  token: 0x600052D
        public System.Void .ctor(CriWare.CriAudioWriteStream.Delegate callback, System.Int32 numChannels, System.Int32 bufferSize) { }

    }

    // TypeToken: 0x20000B0  // size: 0x38
    public class CriFsLoader : CriWare.CriDisposable
    {
        // Fields
        private System.IntPtr handle;  // 0x20
        private System.Runtime.InteropServices.GCHandle dstGch;  // 0x28
        private System.Runtime.InteropServices.GCHandle srcGch;  // 0x30

        // Methods
        // RVA: 0x068F4D94  token: 0x6000538
        public System.Void .ctor() { }
        // RVA: 0x068F4988  token: 0x6000539
        public virtual System.Void Dispose() { }
        // RVA: 0x068F48F8  token: 0x600053A
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068F4CB8  token: 0x600053B
        public System.Void Load(CriWare.CriFsBinder binder, System.String path, System.Int64 fileOffset, System.Int64 loadSize, System.Byte[] buffer) { }
        // RVA: 0x068F4AA8  token: 0x600053C
        public System.Void LoadById(CriWare.CriFsBinder binder, System.Int32 id, System.Int64 fileOffset, System.Int64 loadSize, System.Byte[] buffer) { }
        // RVA: 0x068F4C08  token: 0x600053D
        public System.Void LoadWithoutDecompression(CriWare.CriFsBinder binder, System.String path, System.Int64 fileOffset, System.Int64 loadSize, System.Byte[] buffer) { }
        // RVA: 0x068F4B58  token: 0x600053E
        public System.Void LoadWithoutDecompressionById(CriWare.CriFsBinder binder, System.Int32 id, System.Int64 fileOffset, System.Int64 loadSize, System.Byte[] buffer) { }
        // RVA: 0x068F4854  token: 0x600053F
        public System.Void DecompressData(System.Int64 srcSize, System.Byte[] srcBuffer, System.Int64 dstSize, System.Byte[] dstBuffer) { }
        // RVA: 0x068F4D80  token: 0x6000540
        public System.Void Stop() { }
        // RVA: 0x068F4A4C  token: 0x6000541
        public CriWare.CriFsLoader.Status GetStatus() { }
        // RVA: 0x068F4D68  token: 0x6000542
        public System.Void SetReadUnitSize(System.Int32 unit_size) { }
        // RVA: 0x068F49D8  token: 0x6000543
        protected virtual System.Void Finalize() { }
        // RVA: 0x068F4EC4  token: 0x6000544
        private static System.Int32 criFsLoader_Create(System.IntPtr& loader) { }
        // RVA: 0x068F4FE0  token: 0x6000545
        private static System.Int32 criFsLoader_Destroy(System.IntPtr loader) { }
        // RVA: 0x068F52C8  token: 0x6000546
        private static System.Int32 criFsLoader_Load(System.IntPtr loader, System.IntPtr binder, System.String path, System.Int64 offset, System.Int64 load_size, System.IntPtr buffer, System.Int64 buffer_size) { }
        // RVA: 0x068F5098  token: 0x6000547
        private static System.Int32 criFsLoader_LoadById(System.IntPtr loader, System.IntPtr binder, System.Int32 id, System.Int64 offset, System.Int64 load_size, System.IntPtr buffer, System.Int64 buffer_size) { }
        // RVA: 0x068F5434  token: 0x6000548
        private static System.Int32 criFsLoader_Stop(System.IntPtr loader) { }
        // RVA: 0x068F5014  token: 0x6000549
        private static System.Int32 criFsLoader_GetStatus(System.IntPtr loader, CriWare.CriFsLoader.Status& status) { }
        // RVA: 0x068F53B0  token: 0x600054A
        private static System.Int32 criFsLoader_SetReadUnitSize(System.IntPtr loader, System.Int64 unit_size) { }
        // RVA: 0x068F51E0  token: 0x600054B
        private static System.Int32 criFsLoader_LoadWithoutDecompression(System.IntPtr loader, System.IntPtr binder, System.String path, System.Int64 offset, System.Int64 load_size, System.IntPtr buffer, System.Int64 buffer_size) { }
        // RVA: 0x068F513C  token: 0x600054C
        private static System.Int32 criFsLoader_LoadWithoutDecompressionById(System.IntPtr loader, System.IntPtr binder, System.Int32 id, System.Int64 offset, System.Int64 load_size, System.IntPtr buffer, System.Int64 buffer_size) { }
        // RVA: 0x068F4F3C  token: 0x600054D
        private static System.Int32 criFsLoader_DecompressData(System.IntPtr loader, System.IntPtr src, System.Int64 src_size, System.IntPtr dst, System.Int64 dst_size) { }

    }

    // TypeToken: 0x20000B2  // size: 0x38
    public class CriFsInstaller : CriWare.CriDisposable
    {
        // Fields
        private System.Byte[] installBuffer;  // 0x20
        private System.Runtime.InteropServices.GCHandle installBufferGch;  // 0x28
        private System.IntPtr handle;  // 0x30

        // Methods
        // RVA: 0x068F3CFC  token: 0x600054E
        public System.Void .ctor() { }
        // RVA: 0x068F3B34  token: 0x600054F
        public virtual System.Void Dispose() { }
        // RVA: 0x068F3B84  token: 0x6000550
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068F3904  token: 0x6000551
        public System.Void Copy(CriWare.CriFsBinder binder, System.String srcPath, System.String dstPath, System.Int32 installBufferSize) { }
        // RVA: 0x068F3CE8  token: 0x6000552
        public System.Void Stop() { }
        // RVA: 0x068F3CBC  token: 0x6000553
        public CriWare.CriFsInstaller.Status GetStatus() { }
        // RVA: 0x068F3C8C  token: 0x6000554
        public System.Single GetProgress() { }
        // RVA: 0x068F3C10  token: 0x6000555
        public static System.Void ExecuteMain() { }
        // RVA: 0x068F3C18  token: 0x6000556
        protected virtual System.Void Finalize() { }
        // RVA: 0x068F3FE0  token: 0x6000557
        private static System.Int32 criFsInstaller_ExecuteMain() { }
        // RVA: 0x068F3F28  token: 0x6000558
        private static System.Int32 criFsInstaller_Create(System.IntPtr& installer, CriWare.CriFsInstaller.CopyPolicy option) { }
        // RVA: 0x068F3FAC  token: 0x6000559
        private static System.Int32 criFsInstaller_Destroy(System.IntPtr installer) { }
        // RVA: 0x068F3E30  token: 0x600055A
        private static System.Int32 criFsInstaller_Copy(System.IntPtr installer, System.IntPtr binder, System.String src_path, System.String dst_path, System.IntPtr buffer, System.Int64 buffer_size) { }
        // RVA: 0x068F4114  token: 0x600055B
        private static System.Int32 criFsInstaller_Stop(System.IntPtr installer) { }
        // RVA: 0x068F4090  token: 0x600055C
        private static System.Int32 criFsInstaller_GetStatus(System.IntPtr installer, CriWare.CriFsInstaller.Status& status) { }
        // RVA: 0x068F400C  token: 0x600055D
        private static System.Int32 criFsInstaller_GetProgress(System.IntPtr installer, System.Single& progress) { }

    }

    // TypeToken: 0x20000B5  // size: 0x28
    public class CriFsBinder : CriWare.CriDisposable
    {
        // Fields
        private System.IntPtr handle;  // 0x20

        // Properties
        System.IntPtr nativeHandle { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x068F2C48  token: 0x600055E
        public System.Void .ctor() { }
        // RVA: 0x068F228C  token: 0x600055F
        public virtual System.Void Dispose() { }
        // RVA: 0x068F22DC  token: 0x6000560
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068F20E4  token: 0x6000561
        public System.UInt32 BindCpk(CriWare.CriFsBinder srcBinder, System.String path) { }
        // RVA: 0x068F212C  token: 0x6000562
        public System.UInt32 BindDirectory(CriWare.CriFsBinder srcBinder, System.String path) { }
        // RVA: 0x068F21D4  token: 0x6000563
        public System.UInt32 BindFile(CriWare.CriFsBinder srcBinder, System.String path) { }
        // RVA: 0x068F2174  token: 0x6000564
        public System.UInt32 BindFileSection(CriWare.CriFsBinder srcBinder, System.String path, System.UInt64 offset, System.Int32 size, System.String sectionName) { }
        // RVA: 0x068F2BFC  token: 0x6000565
        public static System.Void Unbind(System.UInt32 bindId) { }
        // RVA: 0x068F2B48  token: 0x6000566
        public static CriWare.CriFsBinder.Status GetStatus(System.UInt32 bindId) { }
        // RVA: 0x068F2B04  token: 0x6000567
        public System.Int64 GetFileSize(System.String path) { }
        // RVA: 0x068F2AC8  token: 0x6000568
        public System.Int64 GetFileSize(System.Int32 id) { }
        // RVA: 0x068F26E8  token: 0x6000569
        public System.Boolean GetContentsFileInfo(System.String path, CriWare.CriFsBinder.ContentsFileInfo& info) { }
        // RVA: 0x068F28D8  token: 0x600056A
        public System.Boolean GetContentsFileInfo(System.Int32 id, CriWare.CriFsBinder.ContentsFileInfo& info) { }
        // RVA: 0x068F23A8  token: 0x600056B
        public static System.Boolean GetContentsFileInfoByIndex(System.UInt32 bindId, System.Int32 index, System.Int32 numFiles, CriWare.CriFsBinder.ContentsFileInfo[]& info) { }
        // RVA: 0x068F2B40  token: 0x600056C
        public static System.Int32 GetNumContentsFiles(System.UInt32 bindId) { }
        // RVA: 0x068F2BA4  token: 0x600056D
        public static System.Void SetPriority(System.UInt32 bindId, System.Int32 priority) { }
        // RVA: 0x068F2334  token: 0x600056F
        protected virtual System.Void Finalize() { }
        // RVA: 0x068F3120  token: 0x6000570
        private static System.UInt32 criFsBinder_Create(System.IntPtr& binder) { }
        // RVA: 0x068F3198  token: 0x6000571
        private static System.UInt32 criFsBinder_Destroy(System.IntPtr binder) { }
        // RVA: 0x068F2D78  token: 0x6000572
        private static System.UInt32 criFsBinder_BindCpk(System.IntPtr binder, System.IntPtr srcBinder, System.String path, System.IntPtr work, System.Int32 worksize, System.UInt32& bindId) { }
        // RVA: 0x068F2E50  token: 0x6000573
        private static System.UInt32 criFsBinder_BindDirectory(System.IntPtr binder, System.IntPtr srcBinder, System.String path, System.IntPtr work, System.Int32 worksize, System.UInt32& bindId) { }
        // RVA: 0x068F3048  token: 0x6000574
        private static System.UInt32 criFsBinder_BindFile(System.IntPtr binder, System.IntPtr srcBinder, System.String path, System.IntPtr work, System.Int32 worksize, System.UInt32& bindId) { }
        // RVA: 0x068F2F28  token: 0x6000575
        private static System.UInt32 criFsBinder_BindFileSection(System.IntPtr binder, System.IntPtr srcBinder, System.String path, System.UInt64 offset, System.Int32 size, System.String sectionName, System.IntPtr work, System.Int32 worksize, System.UInt32& bindId) { }
        // RVA: 0x068F363C  token: 0x6000576
        private static System.Int32 criFsBinder_Unbind(System.UInt32 bindId) { }
        // RVA: 0x068F3538  token: 0x6000577
        private static System.Int32 criFsBinder_GetStatus(System.UInt32 bindId, CriWare.CriFsBinder.Status& status) { }
        // RVA: 0x068F3488  token: 0x6000578
        private static System.Int32 criFsBinder_GetFileSize(System.IntPtr binder, System.String path, System.Int64& size) { }
        // RVA: 0x068F33F4  token: 0x6000579
        private static System.Int32 criFsBinder_GetFileSizeById(System.IntPtr binder, System.Int32 id, System.Int64& size) { }
        // RVA: 0x068F35BC  token: 0x600057A
        private static System.Int32 criFsBinder_SetPriority(System.UInt32 bindId, System.Int32 priority) { }
        // RVA: 0x068F3344  token: 0x600057B
        private static System.Int32 criFsBinder_GetContentsFileInfo(System.IntPtr binder, System.String path, System.IntPtr info) { }
        // RVA: 0x068F3210  token: 0x600057C
        private static System.Int32 criFsBinder_GetContentsFileInfoById(System.IntPtr binder, System.Int32 id, System.IntPtr info) { }
        // RVA: 0x068F32A4  token: 0x600057D
        private static System.Int32 criFsBinder_GetContentsFileInfoByIndex(System.UInt32 id, System.Int32 index, System.IntPtr info, System.Int32 num) { }
        // RVA: 0x068F2214  token: 0x600057E
        private static System.Int32 CRIWAREF02BA103(System.UInt32 id) { }

    }

    // TypeToken: 0x20000B8  // size: 0x40
    public class CriFsRequest : CriWare.CriDisposable
    {
        // Fields
        private CriWare.CriFsRequest.DoneDelegate <doneDelegate>k__BackingField;  // 0x20
        private System.Boolean <isDone>k__BackingField;  // 0x28
        private System.String <error>k__BackingField;  // 0x30
        private System.Boolean <isDisposed>k__BackingField;  // 0x38

        // Properties
        CriWare.CriFsRequest.DoneDelegate doneDelegate { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Boolean isDone { get; /* RVA: 0x011F33C0 */ set; /* RVA: 0x011F33D0 */ }
        System.String error { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        System.Boolean isDisposed { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }

        // Methods
        // RVA: 0x068F59C4  token: 0x6000588
        public virtual System.Void Dispose() { }
        // RVA: 0x041E1670  token: 0x6000589
        public virtual System.Void Stop() { }
        // RVA: 0x068F5AC4  token: 0x600058A
        public UnityEngine.YieldInstruction WaitForDone(UnityEngine.MonoBehaviour mb) { }
        // RVA: 0x041E1670  token: 0x600058B
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x041E1670  token: 0x600058C
        public virtual System.Void Update() { }
        // RVA: 0x068F5A20  token: 0x600058D
        protected System.Void Done() { }
        // RVA: 0x068F5968  token: 0x600058E
        private System.Collections.IEnumerator CheckDone() { }
        // RVA: 0x068F5A40  token: 0x600058F
        protected virtual System.Void Finalize() { }
        // RVA: 0x068F38FC  token: 0x6000590
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BB  // size: 0x88
    public class CriFsLoadFileRequest : CriWare.CriFsRequest
    {
        // Fields
        private System.String <path>k__BackingField;  // 0x40
        private System.Byte[] <bytes>k__BackingField;  // 0x48
        private CriWare.CriFsLoadFileRequest.Phase phase;  // 0x50
        private CriWare.CriFsBinder refBinder;  // 0x58
        private CriWare.CriFsBinder newBinder;  // 0x60
        private System.UInt32 bindId;  // 0x68
        private CriWare.CriFsLoader loader;  // 0x70
        private System.Int32 readUnitSize;  // 0x78
        private System.Int64 fileSize;  // 0x80

        // Properties
        System.String path { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        System.Byte[] bytes { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }

        // Methods
        // RVA: 0x068F4708  token: 0x600059F
        public System.Void .ctor(CriWare.CriFsBinder srcBinder, System.String path, CriWare.CriFsRequest.DoneDelegate doneDelegate, System.Int32 readUnitSize) { }
        // RVA: 0x068F4368  token: 0x60005A0
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068F44B0  token: 0x60005A1
        public virtual System.Void Stop() { }
        // RVA: 0x068F46CC  token: 0x60005A2
        public virtual System.Void Update() { }
        // RVA: 0x068F44DC  token: 0x60005A3
        private System.Void UpdateBinder() { }
        // RVA: 0x068F4544  token: 0x60005A4
        private System.Void UpdateLoader() { }
        // RVA: 0x068F43FC  token: 0x60005A5
        private System.Void OnError() { }

    }

    // TypeToken: 0x20000BD  // size: 0x60
    public class CriFsLoadAssetBundleRequest : CriWare.CriFsRequest
    {
        // Fields
        private System.String <path>k__BackingField;  // 0x40
        private UnityEngine.AssetBundle <assetBundle>k__BackingField;  // 0x48
        private CriWare.CriFsLoadFileRequest loadFileReq;  // 0x50
        private UnityEngine.AssetBundleCreateRequest assetBundleReq;  // 0x58

        // Properties
        System.String path { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        UnityEngine.AssetBundle assetBundle { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }

        // Methods
        // RVA: 0x068F42C8  token: 0x60005AA
        public System.Void .ctor(CriWare.CriFsBinder binder, System.String path, System.Int32 readUnitSize) { }
        // RVA: 0x068F41AC  token: 0x60005AB
        public virtual System.Void Update() { }
        // RVA: 0x068F4148  token: 0x60005AC
        protected virtual System.Void Dispose(System.Boolean disposing) { }

    }

    // TypeToken: 0x20000BE  // size: 0x58
    public class CriFsInstallRequest : CriWare.CriFsRequest
    {
        // Fields
        private System.String <sourcePath>k__BackingField;  // 0x40
        private System.String <destinationPath>k__BackingField;  // 0x48
        private System.Single <progress>k__BackingField;  // 0x50

        // Properties
        System.String sourcePath { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        System.String destinationPath { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        System.Single progress { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }

        // Methods
        // RVA: 0x068F38FC  token: 0x60005B3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BF  // size: 0x60
    public class CriFsInstallRequestLegacy : CriWare.CriFsInstallRequest
    {
        // Fields
        private CriWare.CriFsInstaller installer;  // 0x58

        // Methods
        // RVA: 0x068F3718  token: 0x60005B4
        public virtual System.Void Stop() { }
        // RVA: 0x068F37E8  token: 0x60005B5
        public System.Void .ctor(CriWare.CriFsBinder srcBinder, System.String srcPath, System.String dstPath, CriWare.CriFsRequest.DoneDelegate doneDelegate, System.Int32 installBufferSize) { }
        // RVA: 0x068F3740  token: 0x60005B6
        public virtual System.Void Update() { }
        // RVA: 0x068F36B4  token: 0x60005B7
        protected virtual System.Void Dispose(System.Boolean disposing) { }

    }

    // TypeToken: 0x20000C0  // size: 0x68
    public class CriFsWebInstallRequest : CriWare.CriFsInstallRequest
    {
        // Fields
        private CriWare.CriFsWebInstaller installer;  // 0x58
        private System.UInt32 crc32;  // 0x60
        private System.Boolean crc32_set;  // 0x64

        // Methods
        // RVA: 0x068F6C2C  token: 0x60005B8
        public virtual System.Void Stop() { }
        // RVA: 0x04D89000  token: 0x60005B9
        public System.Boolean GetCRC32(System.UInt32& ret_val) { }
        // RVA: 0x068F6DCC  token: 0x60005BA
        public System.Void .ctor(System.String srcPath, System.String dstPath, CriWare.CriFsRequest.DoneDelegate doneDelegate) { }
        // RVA: 0x068F6C54  token: 0x60005BB
        public virtual System.Void Update() { }
        // RVA: 0x068F6BC8  token: 0x60005BC
        protected virtual System.Void Dispose(System.Boolean disposing) { }

    }

    // TypeToken: 0x20000C1  // size: 0x50
    public class CriFsBindRequest : CriWare.CriFsRequest
    {
        // Fields
        private System.String <path>k__BackingField;  // 0x40
        private System.UInt32 <bindId>k__BackingField;  // 0x48

        // Properties
        System.String path { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        System.UInt32 bindId { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x04D86450 */ }

        // Methods
        // RVA: 0x068F1FAC  token: 0x60005C1
        public System.Void .ctor(CriWare.CriFsBindRequest.BindType type, CriWare.CriFsBinder targetBinder, CriWare.CriFsBinder srcBinder, System.String path) { }
        // RVA: 0x041E1670  token: 0x60005C2
        public virtual System.Void Stop() { }
        // RVA: 0x068F1F48  token: 0x60005C3
        public virtual System.Void Update() { }
        // RVA: 0x068F1EFC  token: 0x60005C4
        protected virtual System.Void Dispose(System.Boolean disposing) { }

    }

    // TypeToken: 0x20000C3  // size: 0x10
    public static class CriFsUtility
    {
        // Fields
        public static System.Int32 DefaultReadUnitSize;  // const

        // Methods
        // RVA: 0x068F6B10  token: 0x60005C5
        public static CriWare.CriFsLoadFileRequest LoadFile(System.String path, System.Int32 readUnitSize) { }
        // RVA: 0x068F6A64  token: 0x60005C6
        public static CriWare.CriFsLoadFileRequest LoadFile(System.String path, CriWare.CriFsRequest.DoneDelegate doneDelegate, System.Int32 readUnitSize) { }
        // RVA: 0x068F6AB8  token: 0x60005C7
        public static CriWare.CriFsLoadFileRequest LoadFile(CriWare.CriFsBinder binder, System.String path, System.Int32 readUnitSize) { }
        // RVA: 0x068F6A54  token: 0x60005C8
        public static CriWare.CriFsLoadAssetBundleRequest LoadAssetBundle(System.String path, System.Int32 readUnitSize) { }
        // RVA: 0x068F6A00  token: 0x60005C9
        public static CriWare.CriFsLoadAssetBundleRequest LoadAssetBundle(CriWare.CriFsBinder binder, System.String path, System.Int32 readUnitSize) { }
        // RVA: 0x068F6978  token: 0x60005CA
        public static CriWare.CriFsInstallRequest Install(System.String srcPath, System.String dstPath) { }
        // RVA: 0x068F68FC  token: 0x60005CB
        public static CriWare.CriFsInstallRequest Install(System.String srcPath, System.String dstPath, CriWare.CriFsRequest.DoneDelegate doneDeleagate) { }
        // RVA: 0x068F6920  token: 0x60005CC
        public static CriWare.CriFsInstallRequest Install(CriWare.CriFsBinder srcBinder, System.String srcPath, System.String dstPath) { }
        // RVA: 0x068F699C  token: 0x60005CD
        public static CriWare.CriFsInstallRequest Install(CriWare.CriFsBinder srcBinder, System.String srcPath, System.String dstPath, CriWare.CriFsRequest.DoneDelegate doneDeleagate) { }
        // RVA: 0x068F6B74  token: 0x60005CE
        public static CriWare.CriFsInstallRequest WebInstall(System.String srcPath, System.String dstPath, CriWare.CriFsRequest.DoneDelegate doneDeleagate) { }
        // RVA: 0x068F6604  token: 0x60005CF
        public static CriWare.CriFsBindRequest BindCpk(CriWare.CriFsBinder targetBinder, System.String srcPath) { }
        // RVA: 0x068F6614  token: 0x60005D0
        public static CriWare.CriFsBindRequest BindCpk(CriWare.CriFsBinder targetBinder, CriWare.CriFsBinder srcBinder, System.String srcPath) { }
        // RVA: 0x068F6668  token: 0x60005D1
        public static CriWare.CriFsBindRequest BindDirectory(CriWare.CriFsBinder targetBinder, System.String srcPath) { }
        // RVA: 0x068F66B0  token: 0x60005D2
        public static CriWare.CriFsBindRequest BindDirectory(CriWare.CriFsBinder targetBinder, CriWare.CriFsBinder srcBinder, System.String srcPath) { }
        // RVA: 0x068F6758  token: 0x60005D3
        public static CriWare.CriFsBindRequest BindFile(CriWare.CriFsBinder targetBinder, System.String srcPath) { }
        // RVA: 0x068F6704  token: 0x60005D4
        public static CriWare.CriFsBindRequest BindFile(CriWare.CriFsBinder targetBinder, CriWare.CriFsBinder srcBinder, System.String srcPath) { }
        // RVA: 0x068F6B6C  token: 0x60005D5
        public static System.Void SetUserAgentString(System.String userAgentString) { }
        // RVA: 0x068F6B64  token: 0x60005D6
        public static System.Void SetProxyServer(System.String proxyPath, System.UInt16 proxyPort) { }
        // RVA: 0x068F6B5C  token: 0x60005D7
        public static System.Void SetPathSeparator(System.String filter) { }
        // RVA: 0x068F68A0  token: 0x60005D8
        private static System.Boolean CRIWARECD0DC04A(System.String userAgentString) { }
        // RVA: 0x068F67FC  token: 0x60005D9
        private static System.Boolean CRIWARE48E31D64(System.String proxyPath, System.UInt16 proxyPort) { }
        // RVA: 0x068F67A0  token: 0x60005DA
        private static System.Boolean CRIWARE0C80F382(System.String filter) { }

    }

    // TypeToken: 0x20000C4  // size: 0x10
    public static class CriFsPlugin
    {
        // Fields
        private static System.Int32 initializationCount;  // static @ 0x0
        private static System.Boolean isConfigured;  // static @ 0x4
        public static System.Int32 defaultInstallBufferSize;  // static @ 0x8
        public static System.Int32 installBufferSize;  // static @ 0xc

        // Properties
        System.Boolean isInitialized { get; /* RVA: 0x068F5920 */ }

        // Methods
        // RVA: 0x03A080E0  token: 0x60005DC
        public static System.Void SetConfigParameters(System.Int32 num_loaders, System.Int32 num_binders, System.Int32 num_installers, System.Int32 argInstallBufferSize, System.Int32 max_path, System.Boolean minimize_file_descriptor_usage, System.Boolean enable_crc_check) { }
        // RVA: 0x068F55EC  token: 0x60005DD
        public static System.Void SetReadDeviceEnabled(System.Int32 deviceId, System.Boolean enabled) { }
        // RVA: 0x041E1670  token: 0x60005DE
        public static System.Void SetConfigAdditionalParameters_ANDROID(System.Int32 device_read_bps) { }
        // RVA: 0x041E1670  token: 0x60005DF
        public static System.Void SetMemoryFileSystemThreadPriorityExperimentalAndroid(System.Int32 prio) { }
        // RVA: 0x041E1670  token: 0x60005E0
        public static System.Void SetDataDecompressionThreadPriorityExperimentalAndroid(System.Int32 prio) { }
        // RVA: 0x03A07B60  token: 0x60005E1
        public static System.Void InitializeLibrary() { }
        // RVA: 0x03A07FC0  token: 0x60005E2
        public static System.Boolean IsLibraryInitialized() { }
        // RVA: 0x068F5504  token: 0x60005E3
        public static System.Void FinalizeLibrary() { }
        // RVA: 0x03827280  token: 0x60005E4
        private static System.Void CRIWARE8451156E(System.Int32 num_loaders, System.Int32 num_binders, System.Int32 num_installers, System.Int32 max_path, System.Boolean minimize_file_descriptor_usage, System.Boolean enable_crc_check) { }
        // RVA: 0x03A07C50  token: 0x60005E5
        private static System.Void CRIWARE601ABE43() { }
        // RVA: 0x03A08010  token: 0x60005E6
        public static System.Boolean CRIWARE1012AF20() { }
        // RVA: 0x068F54AC  token: 0x60005E7
        private static System.Void CRIWARE74A6103A() { }
        // RVA: 0x068F54D8  token: 0x60005E8
        public static System.UInt32 CRIWARE785081A5() { }
        // RVA: 0x068F5678  token: 0x60005E9
        public static System.UInt32 criFsLoader_GetRetryCount() { }
        // RVA: 0x068F56E0  token: 0x60005EA
        public static System.Int32 criFs_GetNumBinds(System.Int32& cur, System.Int32& max, System.Int32& limit) { }
        // RVA: 0x068F5808  token: 0x60005EB
        public static System.Int32 criFs_GetNumUsedLoaders(System.Int32& cur, System.Int32& max, System.Int32& limit) { }
        // RVA: 0x068F5774  token: 0x60005EC
        public static System.Int32 criFs_GetNumUsedInstallers(System.Int32& cur, System.Int32& max, System.Int32& limit) { }
        // RVA: 0x068F589C  token: 0x60005ED
        private static System.Int32 criFs_SetReadDeviceEnabled(System.Int32 device_id, System.Boolean enabled) { }
        // RVA: 0x04D34720  token: 0x60005EE
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C5  // size: 0x38
    public class CriFsServer : CriWare.CriMonoBehaviour
    {
        // Fields
        private static CriWare.CriFsServer _instance;  // static @ 0x0
        private System.Collections.Generic.List<CriWare.CriFsRequest> requestList;  // 0x28
        private System.Int32 <installBufferSize>k__BackingField;  // 0x30

        // Properties
        CriWare.CriFsServer instance { get; /* RVA: 0x068F65C4 */ }
        System.Int32 installBufferSize { get; /* RVA: 0x04D864F0 */ set; /* RVA: 0x04D86740 */ }

        // Methods
        // RVA: 0x068F5EA8  token: 0x60005F2
        public static System.Void CreateInstance() { }
        // RVA: 0x068F60AC  token: 0x60005F3
        public static System.Void DestroyInstance() { }
        // RVA: 0x068F5B48  token: 0x60005F4
        private System.Void Awake() { }
        // RVA: 0x068F6348  token: 0x60005F5
        private System.Void OnDestroy() { }
        // RVA: 0x068F5F7C  token: 0x60005F6
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x041E1670  token: 0x60005F7
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: 0x068F5AF4  token: 0x60005F8
        public System.Void AddRequest(CriWare.CriFsRequest request) { }
        // RVA: 0x068F62A0  token: 0x60005F9
        public CriWare.CriFsLoadFileRequest LoadFile(CriWare.CriFsBinder binder, System.String path, CriWare.CriFsRequest.DoneDelegate doneDelegate, System.Int32 readUnitSize) { }
        // RVA: 0x068F6200  token: 0x60005FA
        public CriWare.CriFsLoadAssetBundleRequest LoadAssetBundle(CriWare.CriFsBinder binder, System.String path, System.Int32 readUnitSize) { }
        // RVA: 0x068F6138  token: 0x60005FB
        public CriWare.CriFsInstallRequest Install(CriWare.CriFsBinder srcBinder, System.String srcPath, System.String dstPath, CriWare.CriFsRequest.DoneDelegate doneDelegate) { }
        // RVA: 0x068F6510  token: 0x60005FC
        public CriWare.CriFsInstallRequest WebInstall(System.String srcPath, System.String dstPath, CriWare.CriFsRequest.DoneDelegate doneDelegate) { }
        // RVA: 0x068F5CB4  token: 0x60005FD
        public CriWare.CriFsBindRequest BindCpk(CriWare.CriFsBinder targetBinder, CriWare.CriFsBinder srcBinder, System.String path) { }
        // RVA: 0x068F5D58  token: 0x60005FE
        public CriWare.CriFsBindRequest BindDirectory(CriWare.CriFsBinder targetBinder, CriWare.CriFsBinder srcBinder, System.String path) { }
        // RVA: 0x068F5E00  token: 0x60005FF
        public CriWare.CriFsBindRequest BindFile(CriWare.CriFsBinder targetBinder, CriWare.CriFsBinder srcBinder, System.String path) { }
        // RVA: 0x046D88E0  token: 0x6000600
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C6  // size: 0x28
    public class CriFsWebInstaller : CriWare.CriDisposable
    {
        // Fields
        private static System.Boolean <isInitialized>k__BackingField;  // static @ 0x0
        private static System.Boolean <isCrcEnabled>k__BackingField;  // static @ 0x1
        public static System.Int32 InvalidHttpStatusCode;  // const
        public static System.Int64 InvalidContentsSize;  // const
        private System.IntPtr handle;  // 0x20

        // Properties
        System.Boolean isInitialized { get; /* RVA: 0x068F79AC */ set; /* RVA: 0x068F7A20 */ }
        System.Boolean isCrcEnabled { get; /* RVA: 0x068F7974 */ set; /* RVA: 0x068F79E4 */ }
        CriWare.CriFsWebInstaller.ModuleConfig defaultModuleConfig { get; /* RVA: 0x068F791C */ }

        // Methods
        // RVA: 0x068F73EC  token: 0x6000606
        public System.Void .ctor() { }
        // RVA: 0x068F709C  token: 0x6000607
        protected virtual System.Void Finalize() { }
        // RVA: 0x068F6EDC  token: 0x6000608
        public virtual System.Void Dispose() { }
        // RVA: 0x068F6ED0  token: 0x6000609
        public System.Void Copy(System.String url, System.String dstPath) { }
        // RVA: 0x068F73D8  token: 0x600060A
        public System.Void Stop() { }
        // RVA: 0x068F7194  token: 0x600060B
        public CriWare.CriFsWebInstaller.StatusInfo GetStatusInfo() { }
        // RVA: 0x068F7110  token: 0x600060C
        public System.Boolean GetCRC32(System.UInt32& ret_val) { }
        // RVA: 0x068F71E4  token: 0x600060D
        public static System.Void InitializeModule(CriWare.CriFsWebInstaller.ModuleConfig config) { }
        // RVA: 0x068F712C  token: 0x600060E
        private static System.Type GetCriFsWebInstallerCurlExpansionClass() { }
        // RVA: 0x068F6FF4  token: 0x600060F
        public static System.Void FinalizeModule() { }
        // RVA: 0x068F6FEC  token: 0x6000610
        public static System.Void ExecuteMain() { }
        // RVA: 0x068F73C0  token: 0x6000611
        public static System.Boolean SetRequestHeader(System.String field, System.String value) { }
        // RVA: 0x068F6F2C  token: 0x6000612
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x068F7780  token: 0x6000613
        private static System.Int32 criFsWebInstaller_Initialize(CriWare.CriFsWebInstaller.ModuleConfig& config) { }
        // RVA: 0x068F764C  token: 0x6000614
        private static System.Int32 criFsWebInstaller_Finalize() { }
        // RVA: 0x068F7620  token: 0x6000615
        private static System.Int32 criFsWebInstaller_ExecuteMain() { }
        // RVA: 0x068F7574  token: 0x6000616
        private static System.Int32 criFsWebInstaller_Create(System.IntPtr& installer) { }
        // RVA: 0x068F75EC  token: 0x6000617
        private static System.Int32 criFsWebInstaller_Destroy(System.IntPtr installer) { }
        // RVA: 0x068F74A8  token: 0x6000618
        private static System.Int32 criFsWebInstaller_Copy(System.IntPtr installer, System.String url, System.String dstPath) { }
        // RVA: 0x068F78E8  token: 0x6000619
        private static System.Int32 criFsWebInstaller_Stop(System.IntPtr installer) { }
        // RVA: 0x068F76FC  token: 0x600061A
        private static System.Int32 criFsWebInstaller_GetStatusInfo(System.IntPtr installer, CriWare.CriFsWebInstaller.StatusInfo& status) { }
        // RVA: 0x068F7678  token: 0x600061B
        private static System.Int32 criFsWebInstaller_GetCRC32(System.IntPtr installer, System.UInt32& crc32) { }
        // RVA: 0x068F7828  token: 0x600061C
        private static System.Int32 criFsWebInstaller_SetRequestHeader(System.String field, System.String value) { }

    }

    // TypeToken: 0x20000CC  // size: 0x10
    public class CriManaPlugin
    {
        // Fields
        private static System.Int32 initializationCount;  // static @ 0x0
        private static System.Boolean isConfigured;  // static @ 0x4
        private static System.Boolean enabledMultithreadedRendering;  // static @ 0x5
        public static System.Int32 renderingEventOffset;  // static @ 0x8

        // Properties
        System.Boolean isInitialized { get; /* RVA: 0x068FCEB4 */ }
        System.Boolean isMultithreadedRenderingEnabled { get; /* RVA: 0x068FCEFC */ }

        // Methods
        // RVA: 0x03A06D70  token: 0x6000620
        public static System.Void SetConfigParameters(System.Boolean graphicsMultiThreaded, System.Int32 num_decoders, System.Int32 max_num_of_entries) { }
        // RVA: 0x03A05CE0  token: 0x6000621
        private static System.Void SetupVp9() { }
        // RVA: 0x041E1670  token: 0x6000622
        public static System.Void SetConfigAdditonalParameters_VITA(System.Boolean use_h264_playback, System.Int32 width, System.Int32 height) { }
        // RVA: 0x03A08050  token: 0x6000623
        public static System.Void SetConfigAdditonalParameters_PC(System.Boolean use_h264_playback) { }
        // RVA: 0x041E1670  token: 0x6000624
        public static System.Void SetConfigAdditonalParameters_ANDROID(System.Boolean enable_buffer_output_for_h264, System.Boolean enable_buffer_output_for_vp9) { }
        // RVA: 0x041E1670  token: 0x6000625
        public static System.Void SetConfigAdditonalParameters_WEBGL(System.String webworkerPath, System.UInt32 heapSize) { }
        // RVA: 0x03A06160  token: 0x6000626
        public static System.Void InitializeLibrary() { }
        // RVA: 0x03A06980  token: 0x6000627
        public static System.Boolean IsLibraryInitialized() { }
        // RVA: 0x068FCAC8  token: 0x6000628
        public static System.Void FinalizeLibrary() { }
        // RVA: 0x068FCC38  token: 0x6000629
        public static System.Boolean IsCodecSupported(CriWare.CriMana.CodecType codecType) { }
        // RVA: 0x03A05ED0  token: 0x600062A
        private static System.Type GetVp9ExpansionClass() { }
        // RVA: 0x03A05DE0  token: 0x600062B
        private static System.Boolean IsVp9CodecSupported() { }
        // RVA: 0x02FFF600  token: 0x600062C
        private static System.Boolean IsH264CodecSupported() { }
        // RVA: 0x041E1670  token: 0x600062D
        public static System.Void SetDecodeThreadPriorityAndroidExperimental(System.Int32 prio) { }
        // RVA: 0x012081B0  token: 0x600062E
        public static System.Boolean ShouldSampleRed(UnityEngine.Rendering.GraphicsDeviceType type, System.IntPtr tex_ptr) { }
        // RVA: 0x068FCD24  token: 0x600062F
        public static System.Void Lock() { }
        // RVA: 0x068FCD64  token: 0x6000630
        public static System.Void Unlock() { }
        // RVA: 0x068FCBF8  token: 0x6000631
        public static System.UInt32 GetPrimeBufferAlignmentSize() { }
        // RVA: 0x038271E0  token: 0x6000632
        private static System.Void CRIWARECFA78B41(System.Int32 graphics_api, System.Boolean graphics_multi_threaded, System.Int32 num_decoders, System.Int32 num_of_max_entries) { }
        // RVA: 0x03A062B0  token: 0x6000633
        private static System.Void CRIWARE7BFC56AA() { }
        // RVA: 0x03A069D0  token: 0x6000634
        public static System.Boolean CRIWARE611B78A5() { }
        // RVA: 0x068FCA9C  token: 0x6000635
        private static System.Void CRIWARED672C36F() { }
        // RVA: 0x068FCA68  token: 0x6000636
        public static System.Void CRIWARE9F182D39(System.Boolean flag) { }
        // RVA: 0x068FC9E4  token: 0x6000637
        public static System.Void CRIWARE06C87176() { }
        // RVA: 0x068FCA3C  token: 0x6000638
        public static System.Void CRIWARE46E1A37B() { }
        // RVA: 0x068FCDA4  token: 0x6000639
        private static System.UInt32 criManaUnity_GetPrimeBufferAlignmentSize() { }
        // RVA: 0x068FCE3C  token: 0x600063A
        public static System.Void criMana_UseStreamerManager(System.Boolean flag) { }
        // RVA: 0x068FCE0C  token: 0x600063B
        public static System.Boolean criMana_IsStreamerManagerUsed() { }
        // RVA: 0x068FCA10  token: 0x600063C
        public static System.UInt32 CRIWARE257CA763() { }
        // RVA: 0x03A080A0  token: 0x600063D
        public static System.Void CRIWARE62B7B053(System.Boolean enable) { }
        // RVA: 0x041E1670  token: 0x600063E
        public System.Void .ctor() { }
        // RVA: 0x04D36030  token: 0x600063F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000CD  // size: 0x58
    public class CriManaAmbisonicSource : CriWare.CriMonoBehaviour
    {
        // Fields
        private CriWare.CriAtomEx3dSource atomEx3DsourceForAmbisonics;  // 0x28
        private UnityEngine.Vector3 ambisonicSourceOrientationFront;  // 0x30
        private UnityEngine.Vector3 ambisonicSourceOrientationTop;  // 0x3c
        private UnityEngine.Vector3 lastEulerOfAmbisonicSource;  // 0x48

        // Methods
        // RVA: 0x068F7A5C  token: 0x6000640
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x041E1670  token: 0x6000641
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: 0x068F7B0C  token: 0x6000642
        protected virtual System.Void OnEnable() { }
        // RVA: 0x068F7A64  token: 0x6000643
        private System.Void ForceUpdateAmbisonicSourceOrientation() { }
        // RVA: 0x068F7FFC  token: 0x6000644
        private System.Void UpdateAmbisonicSourceOrientation() { }
        // RVA: 0x068F7BF0  token: 0x6000645
        private System.Void RoatateAmbisonicSourceOrientationByTransformOfChild(UnityEngine.Vector3& input_euler) { }
        // RVA: 0x046D88E0  token: 0x6000646
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CE  // size: 0xB0
    public class CriManaMovieController : CriWare.CriManaMovieMaterial
    {
        // Fields
        public UnityEngine.Renderer target;  // 0x98
        public System.Boolean useOriginalMaterial;  // 0xa0
        private UnityEngine.Material originalMaterial;  // 0xa8

        // Methods
        // RVA: 0x068FB43C  token: 0x6000647
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x068FB64C  token: 0x6000648
        public virtual System.Boolean RenderTargetManualSetup() { }
        // RVA: 0x068FB5A0  token: 0x6000649
        public virtual System.Void RenderTargetManualFinalize() { }
        // RVA: 0x068FB4DC  token: 0x600064A
        protected virtual System.Void OnMaterialAvailableChanged() { }
        // RVA: 0x068FB434  token: 0x600064B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CF  // size: 0xB0
    public class CriManaMovieControllerForUI : CriWare.CriManaMovieMaterial
    {
        // Fields
        public UnityEngine.UI.Graphic target;  // 0x98
        public System.Boolean useOriginalMaterial;  // 0xa0
        private UnityEngine.Material originalMaterial;  // 0xa8

        // Methods
        // RVA: 0x068FB0B0  token: 0x600064C
        protected virtual System.Void Awake() { }
        // RVA: 0x068FB0D4  token: 0x600064D
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x068FB2F0  token: 0x600064E
        public virtual System.Boolean RenderTargetManualSetup() { }
        // RVA: 0x068FB240  token: 0x600064F
        public virtual System.Void RenderTargetManualFinalize() { }
        // RVA: 0x068FB178  token: 0x6000650
        protected virtual System.Void OnMaterialAvailableChanged() { }
        // RVA: 0x068FB434  token: 0x6000651
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D0  // size: 0x98
    public class CriManaMovieMaterial : CriWare.CriManaMovieMaterialBase
    {
        // Fields
        private System.String _moviePath;  // 0x80
        private System.Boolean _loop;  // 0x88
        private System.Boolean _additiveMode;  // 0x89
        private System.Boolean _useMaterialBlendMode;  // 0x8a
        private System.Boolean _advancedAudio;  // 0x8b
        private System.Boolean _ambisonics;  // 0x8c
        private System.Boolean _applyTargetAlpha;  // 0x8d
        private System.Boolean _uiRenderMode;  // 0x8e
        private UnityEngine.GameObject ambisonicSource;  // 0x90

        // Properties
        System.String moviePath { get; /* RVA: 0x04D86260 */ set; /* RVA: 0x068FC848 */ }
        System.Boolean loop { get; /* RVA: 0x011F2750 */ set; /* RVA: 0x068FC7DC */ }
        System.Boolean advancedAudio { get; /* RVA: 0x04D89070 */ set; /* RVA: 0x068FC274 */ }
        System.Boolean ambisonics { get; /* RVA: 0x04D89080 */ set; /* RVA: 0x068FC2F4 */ }
        System.Boolean additiveMode { get; /* RVA: 0x04D89060 */ set; /* RVA: 0x068FC208 */ }
        System.Boolean useMaterialBlendMode { get; /* RVA: 0x04D890B0 */ set; /* RVA: 0x068FC92C */ }
        System.Boolean applyTargetAlpha { get; /* RVA: 0x04D89090 */ set; /* RVA: 0x068FC770 */ }
        System.Boolean uiRenderMode { get; /* RVA: 0x04D890A0 */ set; /* RVA: 0x068FC8C0 */ }
        System.UInt32 FilePathLength { get; /* RVA: 0x068FC1B8 */ }
        System.Boolean initializeWithAdvancedAudio { get; /* RVA: 0x04D89070 */ }
        System.Boolean initializeWithAmbisonics { get; /* RVA: 0x04D89080 */ }

        // Methods
        // RVA: 0x068FC0F4  token: 0x6000665
        protected virtual System.Void SetDataToPlayer() { }
        // RVA: 0x068FB434  token: 0x6000666
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D1  // size: 0x80
    public abstract class CriManaMovieMaterialBase : CriWare.CriMonoBehaviour
    {
        // Fields
        public System.Boolean playOnStart;  // 0x28
        public System.Boolean restartOnEnable;  // 0x29
        private System.Boolean <isMaterialAvailable>k__BackingField;  // 0x2a
        private CriWare.CriMana.Player <player>k__BackingField;  // 0x30
        public CriWare.CriManaMovieMaterialBase.RenderMode renderMode;  // 0x38
        public CriWare.CriManaMovieMaterialBase.OnApplicationPauseCallback onApplicationPauseCallback;  // 0x40
        private CriWare.CriMana.Player.TimerType _timerType;  // 0x48
        private UnityEngine.Material _material;  // 0x50
        private CriWare.CriManaMovieMaterialBase.MaxFrameDrop _maxFrameDrop;  // 0x58
        private System.Boolean materialOwn;  // 0x5c
        protected System.Boolean isMonoBehaviourStartCalled;  // 0x5d
        private System.Boolean wasDisabled;  // 0x5e
        private System.Boolean wasPausedOnDisable;  // 0x5f
        private System.Boolean previousOnApplicationPauseStatus;  // 0x60
        private UnityEngine.WaitForEndOfFrame frameEnd;  // 0x68
        private System.Boolean unpauseOnApplicationUnpause;  // 0x70
        private System.Boolean <HaveRendererOwner>k__BackingField;  // 0x71
        private CriWare.CriManaMoviePlayerHolder playerHolder;  // 0x78

        // Properties
        CriWare.CriManaMovieMaterialBase.MaxFrameDrop maxFrameDrop { get; /* RVA: 0x04D86570 */ set; /* RVA: 0x068FC0BC */ }
        System.Boolean initializeWithAdvancedAudio { get; /* RVA: -1  // abstract */ }
        System.Boolean initializeWithAmbisonics { get; /* RVA: -1  // abstract */ }
        System.Boolean isMaterialAvailable { get; /* RVA: 0x04D86CA0 */ set; /* RVA: 0x04D86CD0 */ }
        CriWare.CriMana.Player player { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        UnityEngine.Material material { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x068FC024 */ }
        CriWare.CriMana.Player.TimerType timerType { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x068FC0D4 */ }
        System.UInt32 FilePathLength { get; /* RVA: -1  // abstract */ }
        System.Boolean HaveRendererOwner { get; /* RVA: 0x04D89040 */ set; /* RVA: 0x04D89050 */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000674
        protected virtual System.Void SetDataToPlayer() { }
        // RVA: 0x068FBA6C  token: 0x6000677
        public System.Void Play() { }
        // RVA: 0x068FBF8C  token: 0x6000678
        public System.Void Stop() { }
        // RVA: 0x068FBA38  token: 0x6000679
        public System.Void Pause(System.Boolean sw) { }
        // RVA: 0x041E1670  token: 0x600067A
        protected virtual System.Void OnMaterialAvailableChanged() { }
        // RVA: 0x041E1670  token: 0x600067B
        protected virtual System.Void OnMaterialUpdated() { }
        // RVA: 0x068FBAE0  token: 0x600067C
        public System.Void PlayerManualInitialize() { }
        // RVA: 0x068FBAA0  token: 0x600067D
        public System.Void PlayerManualFinalize() { }
        // RVA: 0x068FB78C  token: 0x600067E
        private System.Void AsyncDispose(CriWare.CriMana.Player player) { }
        // RVA: 0x068FBC80  token: 0x600067F
        public System.Void PlayerManualSetup() { }
        // RVA: 0x02FFF600  token: 0x6000680
        public virtual System.Boolean RenderTargetManualSetup() { }
        // RVA: 0x041E1670  token: 0x6000681
        public virtual System.Void RenderTargetManualFinalize() { }
        // RVA: 0x068FBD34  token: 0x6000682
        public System.Void PlayerManualUpdate() { }
        // RVA: 0x068FB82C  token: 0x6000683
        protected virtual System.Void Awake() { }
        // RVA: 0x068FB9A8  token: 0x6000684
        protected virtual System.Void OnEnable() { }
        // RVA: 0x068FBED0  token: 0x6000685
        private System.Collections.IEnumerator RestartPlayerRoutine() { }
        // RVA: 0x068FB954  token: 0x6000686
        protected virtual System.Void OnDisable() { }
        // RVA: 0x068FB92C  token: 0x6000687
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x068FBF2C  token: 0x6000688
        protected virtual System.Void Start() { }
        // RVA: 0x068FB91C  token: 0x6000689
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x068FBEA8  token: 0x600068A
        public virtual System.Void RenderMovie() { }
        // RVA: 0x068FB8EC  token: 0x600068B
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: 0x068FBA08  token: 0x600068C
        protected virtual System.Void OnWillRenderObject() { }
        // RVA: 0x068FB924  token: 0x600068D
        private System.Void OnApplicationPause(System.Boolean appPause) { }
        // RVA: 0x068FBE0C  token: 0x600068E
        private System.Void ProcessApplicationPause(System.Boolean appPause) { }
        // RVA: 0x068FB834  token: 0x600068F
        private System.Void CreateMaterial() { }
        // RVA: 0x068FBFC4  token: 0x6000690
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000D7  // size: 0x30
    public class CriManaMoviePlayerHolder : CriWare.CriMonoBehaviour
    {
        // Fields
        private CriWare.CriMana.Player _player;  // 0x28

        // Properties
        CriWare.CriMana.Player player { set; /* RVA: 0x02FDB880 */ }

        // Methods
        // RVA: 0x068FC998  token: 0x600069E
        private System.Void Awake() { }
        // RVA: 0x041E1670  token: 0x600069F
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x041E1670  token: 0x60006A0
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: 0x041E1670  token: 0x60006A1
        private System.Void Start() { }
        // RVA: 0x046D88E0  token: 0x60006A2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D8  // size: 0x10
    public class Common
    {
        // Fields
        private static System.String scriptVersionString;  // const
        public static System.Boolean supportsCriFsInstaller;  // const
        public static System.Boolean supportsCriFsWebInstaller;  // const
        public static System.String pluginName;  // const
        public static System.Runtime.InteropServices.CallingConvention pluginCallingConvention;  // const
        private static UnityEngine.GameObject _managerObject;  // static @ 0x0

        // Properties
        System.String streamingAssetsPath { get; /* RVA: 0x03828730 */ }
        System.String installTargetPath { get; /* RVA: 0x068EA730 */ }
        System.String installCachePath { get; /* RVA: 0x068EA708 */ }
        UnityEngine.GameObject managerObject { get; /* RVA: 0x03A078A0 */ }

        // Methods
        // RVA: 0x04D231A0  token: 0x60006A6
        public static System.Boolean IsStreamingAssetsPath(System.String path) { }
        // RVA: 0x068EA6DC  token: 0x60006A8
        public static System.String GetScriptVersionString() { }
        // RVA: 0x05394810  token: 0x60006A9
        public static System.Int32 GetBinaryVersionNumber() { }
        // RVA: 0x04D88FD0  token: 0x60006AA
        public static System.Int32 GetRequiredBinaryVersionNumber() { }
        // RVA: 0x03A09410  token: 0x60006AB
        public static System.Boolean CheckBinaryVersionCompatibility() { }
        // RVA: 0x068EA65C  token: 0x60006AC
        public static System.UInt32 GetFsMemoryUsage() { }
        // RVA: 0x068EA61C  token: 0x60006AD
        public static System.UInt32 GetAtomMemoryUsage() { }
        // RVA: 0x068EA69C  token: 0x60006AE
        public static System.UInt32 GetManaMemoryUsage() { }
        // RVA: 0x068EA5C8  token: 0x60006AF
        public static CriWare.Common.CpuUsage GetAtomCpuUsage() { }
        // RVA: 0x04D30EE0  token: 0x60006B0
        public static System.Int32 CRIWAREC8077C29() { }
        // RVA: 0x03827CD0  token: 0x60006B1
        public static System.Void criWareUnity_SetRenderingEventOffsetForMana(System.Int32 offset) { }
        // RVA: 0x041E1670  token: 0x60006B2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DA  // size: 0x10
    public static class CriWareDecrypter
    {
        // Fields
        private static System.UInt64 temporalStorage;  // static @ 0x0

        // Methods
        // RVA: 0x068FD118  token: 0x60006B3
        public static System.Boolean Initialize(CriWare.CriWareDecrypterConfig config) { }
        // RVA: 0x03828430  token: 0x60006B4
        public static System.Boolean Initialize(System.String key, System.String authenticationFile, System.Boolean enableAtomDecryption, System.Boolean enableManaDecryption) { }
        // RVA: 0x068FD0E0  token: 0x60006B5
        private static System.UInt64 CallbackFromNative(System.IntPtr ptr1) { }
        // RVA: 0x03827130  token: 0x60006B6
        public static System.Int32 CRIWARE05CA8EB2(System.Boolean enable_atom_decryption, System.Boolean enable_mana_decryption, CriWare.CriWareDecrypter.CallbackFromNativeDelegate func, System.IntPtr obj) { }

    }

    // TypeToken: 0x20000DC  // size: 0x38
    public class CriWareErrorHandler : CriWare.CriMonoBehaviour
    {
        // Fields
        public System.Boolean enableDebugPrintOnTerminal;  // 0x28
        public System.Boolean enableForceCrashOnError;  // 0x29
        public System.Boolean dontDestroyOnLoad;  // 0x2a
        private static System.String <errorMessage>k__BackingField;  // static @ 0x0
        public static readonly System.String logPrefix;  // static @ 0x8
        private static CriWare.CriWareErrorHandler.Callback OnCallback;  // static @ 0x10
        public static CriWare.CriWareErrorHandler.Callback callback;  // static @ 0x18
        public System.UInt32 messageBufferCounts;  // 0x2c
        private System.Collections.Concurrent.ConcurrentQueue<System.String> unThreadSafeMessages;  // 0x30
        private static System.Int32 initializationCount;  // static @ 0x20

        // Properties
        System.String errorMessage { get; /* RVA: 0x068FDC00 */ set; /* RVA: 0x068FDD24 */ }

        // Events
        event CriWare.CriWareErrorHandler.Callback OnCallback;

        // Methods
        // RVA: 0x068FD148  token: 0x60006BF
        private System.Void Awake() { }
        // RVA: 0x068FD6C4  token: 0x60006C0
        protected virtual System.Void OnEnable() { }
        // RVA: 0x068FD5E4  token: 0x60006C1
        protected virtual System.Void OnDisable() { }
        // RVA: 0x068FD2C4  token: 0x60006C2
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x041E1670  token: 0x60006C3
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: 0x068FD4C8  token: 0x60006C4
        private System.Void OnDestroy() { }
        // RVA: 0x068FD2CC  token: 0x60006C5
        private System.Void DequeueErrorMessages() { }
        // RVA: 0x068FD404  token: 0x60006C6
        private System.Void HandleMessage(System.String errmsg) { }
        // RVA: 0x068FD7A4  token: 0x60006C7
        private static System.Void OutputDefaultLog(System.String errmsg) { }
        // RVA: 0x068FDAA0  token: 0x60006C8
        public System.Void .ctor() { }
        // RVA: 0x068FD9D8  token: 0x60006C9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000DE  // size: 0x10
    public static class CriErrorNotifier
    {
        // Fields
        private static CriWare.CriErrorNotifier.Callback _onCallbackThreadUnsafe;  // static @ 0x0
        private static System.Object objectLock;  // static @ 0x8

        // Events
        event CriWare.CriErrorNotifier.Callback _onCallbackThreadUnsafe;
        event CriWare.CriErrorNotifier.Callback OnCallbackThreadUnsafe;

        // Methods
        // RVA: 0x068F18F8  token: 0x60006D2
        public static System.Boolean IsRegistered(CriWare.CriErrorNotifier.Callback target) { }
        // RVA: 0x068F17EC  token: 0x60006D3
        public static System.Void CallEvent(System.String message) { }
        // RVA: 0x068F1868  token: 0x60006D4
        private static System.Void ErrorCallbackFromNative(System.IntPtr errmsgPtr, System.UInt32 p1, System.UInt32 p2, System.IntPtr parray) { }
        // RVA: 0x068F19D8  token: 0x60006D5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E2  // size: 0x38
    public class CriFsConfig
    {
        // Fields
        public static System.Int32 defaultAndroidDeviceReadBitrate;  // const
        public System.Int32 numberOfLoaders;  // 0x10
        public System.Int32 numberOfBinders;  // 0x14
        public System.Int32 numberOfInstallers;  // 0x18
        public System.Int32 installBufferSize;  // 0x1c
        public System.Int32 maxPath;  // 0x20
        public System.String userAgentString;  // 0x28
        public System.Boolean minimizeFileDescriptorUsage;  // 0x30
        public System.Boolean enableCrcCheck;  // 0x31
        public System.Int32 androidDeviceReadBitrate;  // 0x34

        // Methods
        // RVA: 0x046D8B10  token: 0x60006E0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E3  // size: 0xF8
    public class CriAtomConfig
    {
        // Fields
        public System.String acfFileName;  // 0x10
        public System.Int32 maxVirtualVoices;  // 0x18
        public System.Int32 maxVoiceLimitGroups;  // 0x1c
        public System.Int32 maxCategories;  // 0x20
        public System.Int32 maxAisacs;  // 0x24
        public System.Int32 maxBusSends;  // 0x28
        public System.Int32 maxSequenceEventsPerFrame;  // 0x2c
        public System.Int32 maxBeatSyncCallbacksPerFrame;  // 0x30
        public System.Int32 maxCueLinkCallbacksPerFrame;  // 0x34
        public CriWare.CriAtomConfig.StandardVoicePoolConfig standardVoicePoolConfig;  // 0x38
        public CriWare.CriAtomConfig.HcaMxVoicePoolConfig hcaMxVoicePoolConfig;  // 0x40
        public System.Int32 outputSamplingRate;  // 0x48
        public System.Boolean usesInGamePreview;  // 0x4c
        public CriWare.CriAtomConfig.InGamePreviewSwitchMode inGamePreviewMode;  // 0x50
        public System.Boolean switchInitializeSocket;  // 0x54
        public CriWare.CriAtomConfig.InGamePreviewConfig inGamePreviewConfig;  // 0x58
        public System.Single serverFrequency;  // 0x60
        public System.Int32 asrOutputChannels;  // 0x64
        public System.Boolean useRandomSeedWithTime;  // 0x68
        public System.Int32 categoriesPerPlayback;  // 0x6c
        public System.Int32 maxFaders;  // 0x70
        public System.Int32 maxBuses;  // 0x74
        public System.Int32 maxParameterBlocks;  // 0x78
        public System.Boolean vrMode;  // 0x7c
        public System.Boolean keepPlayingSoundOnPause;  // 0x7d
        public CriWare.CriAtomConfig.EditorPcmOutputConfig editorPcmOutputConfig;  // 0x80
        public System.Int32 pcBufferingTime;  // 0x88
        public CriWare.CriAtomConfig.LinuxOutput linuxOutput;  // 0x8c
        public System.Int32 linuxPulseLatencyUsec;  // 0x90
        public System.Boolean iosEnableSonicSync;  // 0x94
        public System.Int32 iosBufferingTime;  // 0x98
        public System.Boolean iosOverrideIPodMusic;  // 0x9c
        public System.Boolean androidEnableSonicSync;  // 0x9d
        public System.Int32 androidBufferingTime;  // 0xa0
        public System.Int32 androidStartBufferingTime;  // 0xa4
        public CriWare.CriAtomConfig.AndroidLowLatencyStandardVoicePoolConfig androidLowLatencyStandardVoicePoolConfig;  // 0xa8
        public System.Boolean androidUsesAndroidFastMixer;  // 0xb0
        public System.Boolean androidForceToUseAsrForDefaultPlayback;  // 0xb1
        public System.Boolean androidUsesAAudio;  // 0xb2
        public System.Int32 androidStreamType;  // 0xb4
        public CriWare.CriAtomConfig.VitaManaVoicePoolConfig vitaManaVoicePoolConfig;  // 0xb8
        public CriWare.CriAtomConfig.VitaAtrac9VoicePoolConfig vitaAtrac9VoicePoolConfig;  // 0xc0
        public CriWare.CriAtomConfig.Ps4Atrac9VoicePoolConfig ps4Atrac9VoicePoolConfig;  // 0xc8
        public CriWare.CriAtomConfig.Ps5PortConfig ps5PortConfig;  // 0xd0
        public System.Boolean switchEnableSonicSync;  // 0xd8
        public CriWare.CriAtomConfig.SwitchOpusVoicePoolConfig switchOpusVoicePoolConfig;  // 0xe0
        public CriWare.CriAtomConfig.Ps4Audio3dConfig ps4Audio3dConfig;  // 0xe8
        public CriWare.CriAtomConfig.WebGLWebAudioVoicePoolConfig webglWebAudioVoicePoolConfig;  // 0xf0

        // Methods
        // RVA: 0x046D8CF0  token: 0x60006E1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F3  // size: 0x38
    public class CriManaConfig
    {
        // Fields
        public System.Int32 numberOfDecoders;  // 0x10
        public System.Int32 numberOfMaxEntries;  // 0x14
        public readonly System.Boolean graphicsMultiThreaded;  // 0x18
        public CriWare.CriManaConfig.PCH264PlaybackConfig pcH264PlaybackConfig;  // 0x20
        public CriWare.CriManaConfig.VitaH264PlaybackConfig vitaH264PlaybackConfig;  // 0x28
        public CriWare.CriManaConfig.WebGLConfig webglConfig;  // 0x30

        // Methods
        // RVA: 0x046D8BC0  token: 0x60006EF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F7  // size: 0x28
    public class CriWareDecrypterConfig
    {
        // Fields
        public System.String key;  // 0x10
        public System.String authenticationFile;  // 0x18
        public System.Boolean enableAtomDecryption;  // 0x20
        public System.Boolean enableManaDecryption;  // 0x21

        // Methods
        // RVA: 0x046D8A20  token: 0x60006F3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F8  // size: 0x70
    public class CriWareInitializer : CriWare.CriMonoBehaviour
    {
        // Fields
        public System.Boolean initializesFileSystem;  // 0x28
        public CriWare.CriFsConfig fileSystemConfig;  // 0x30
        public System.Boolean initializesAtom;  // 0x38
        public CriWare.CriAtomConfig atomConfig;  // 0x40
        public System.Boolean initializesMana;  // 0x48
        public CriWare.CriManaConfig manaConfig;  // 0x50
        public System.Boolean useDecrypter;  // 0x58
        public CriWare.CriWareDecrypterConfig decrypterConfig;  // 0x60
        public System.Boolean dontInitializeOnAwake;  // 0x68
        public System.Boolean dontDestroyOnLoad;  // 0x69
        private static System.Int32 initializationCount;  // static @ 0x0

        // Methods
        // RVA: 0x03A08340  token: 0x60006F4
        private System.Void Awake() { }
        // RVA: 0x03A06E40  token: 0x60006F5
        protected virtual System.Void OnEnable() { }
        // RVA: 0x041E1670  token: 0x60006F6
        private System.Void Start() { }
        // RVA: 0x068FDE00  token: 0x60006F7
        private System.Void OnDestroy() { }
        // RVA: 0x041E1670  token: 0x60006F8
        public virtual System.Void CriInternalUpdate() { }
        // RVA: 0x041E1670  token: 0x60006F9
        public virtual System.Void CriInternalLateUpdate() { }
        // RVA: 0x03A07DD0  token: 0x60006FA
        public System.Void Initialize() { }
        // RVA: 0x068FDE08  token: 0x60006FB
        public System.Void Shutdown() { }
        // RVA: 0x03A093C0  token: 0x60006FC
        public static System.Boolean IsInitialized() { }
        // RVA: 0x068FDD80  token: 0x60006FD
        public static System.Void AddAudioEffectInterface(System.IntPtr effect_interface) { }
        // RVA: 0x03A07C80  token: 0x60006FE
        public static System.Boolean InitializeFileSystem(CriWare.CriFsConfig config) { }
        // RVA: 0x03A062E0  token: 0x60006FF
        public static System.Boolean InitializeAtom(CriWare.CriAtomConfig config) { }
        // RVA: 0x03A06820  token: 0x6000700
        public static System.Boolean InitializeMana(CriWare.CriManaConfig config) { }
        // RVA: 0x068FDDF8  token: 0x6000701
        public static System.Boolean InitializeDecrypter(CriWare.CriWareDecrypterConfig config) { }
        // RVA: 0x046D88F0  token: 0x6000702
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F9  // size: 0x10
    public static class CriAtomExDebug
    {
        // Methods
        // RVA: 0x068EB438  token: 0x6000703
        public static System.Void GetResourcesInfo(CriWare.CriAtomExDebug.ResourcesInfo& resourcesInfo) { }
        // RVA: 0x068EB440  token: 0x6000704
        private static System.Void criAtomExDebug_GetResourcesInfo(CriWare.CriAtomExDebug.ResourcesInfo& resourcesInfo) { }

    }

    // TypeToken: 0x20000FB  // size: 0x10
    public static class CriAtomExAcfDebug
    {
        // Methods
        // RVA: 0x068EAB7C  token: 0x6000705
        public static System.Int32 GetNumCategories() { }
        // RVA: 0x068EA9E4  token: 0x6000706
        public static System.Boolean GetCategoryInfoByIndex(System.UInt16 index, CriWare.CriAtomExAcfDebug.CategoryInfo& categoryInfo) { }
        // RVA: 0x068EAA30  token: 0x6000707
        public static System.Boolean GetCategoryInfoByName(System.String name, CriWare.CriAtomExAcfDebug.CategoryInfo& categoryInfo) { }
        // RVA: 0x068EA998  token: 0x6000708
        public static System.Boolean GetCategoryInfoById(System.UInt32 id, CriWare.CriAtomExAcfDebug.CategoryInfo& categoryInfo) { }
        // RVA: 0x068EAB74  token: 0x6000709
        public static System.Int32 GetNumBuses() { }
        // RVA: 0x068EAA7C  token: 0x600070A
        public static System.Boolean GetDspBusInformation(System.UInt16 index, CriWare.CriAtomExAcfDebug.DspBusInfo& dspBusInfo) { }
        // RVA: 0x068EAB6C  token: 0x600070B
        public static System.Int32 GetNumAisacControls() { }
        // RVA: 0x068EA938  token: 0x600070C
        public static System.Boolean GetAisacControlInfo(System.UInt16 index, CriWare.CriAtomExAcfDebug.AisacControlInfo& info) { }
        // RVA: 0x068EA930  token: 0x600070D
        public static System.UInt32 GetAisacControlIdByName(System.String name) { }
        // RVA: 0x068EA97C  token: 0x600070E
        public static System.String GetAisacControlNameById(System.UInt32 id) { }
        // RVA: 0x068EAB84  token: 0x600070F
        public static System.Int32 GetNumGlobalAisacs() { }
        // RVA: 0x068EAB20  token: 0x6000710
        public static System.Boolean GetGlobalAisacInfo(System.UInt16 index, CriWare.CriAtomExAcfDebug.GlobalAisacInfo& info) { }
        // RVA: 0x068EAAD4  token: 0x6000711
        public static System.Boolean GetGlobalAisacInfoByName(System.String name, CriWare.CriAtomExAcfDebug.GlobalAisacInfo& info) { }
        // RVA: 0x068EAB8C  token: 0x6000712
        public static System.Int32 GetNumSelectors() { }
        // RVA: 0x068EAB94  token: 0x6000713
        public static System.Boolean GetSelectorInfoByIndex(System.UInt16 index, CriWare.CriAtomExAcfDebug.SelectorInfo& info) { }
        // RVA: 0x068EABD8  token: 0x6000714
        public static System.Boolean GetSelectorInfoByName(System.String name, CriWare.CriAtomExAcfDebug.SelectorInfo& info) { }
        // RVA: 0x068EAC1C  token: 0x6000715
        public static System.Boolean GetSelectorLabelInfo(CriWare.CriAtomExAcfDebug.SelectorInfo& selectorInfo, System.UInt16 index, CriWare.CriAtomExAcfDebug.SelectorLabelInfo& labelInfo) { }
        // RVA: 0x068EB208  token: 0x6000716
        private static System.Int32 criAtomExAcf_GetNumCategories() { }
        // RVA: 0x068EAEF8  token: 0x6000717
        private static System.Int32 criAtomExAcf_GetCategoryInfo(System.UInt16 index, CriWare.CriAtomExAcfDebug.CategoryInfoForMarshaling& categoryInfo) { }
        // RVA: 0x068EAE54  token: 0x6000718
        private static System.Int32 criAtomExAcf_GetCategoryInfoByName(System.String name, CriWare.CriAtomExAcfDebug.CategoryInfoForMarshaling& categoryInfo) { }
        // RVA: 0x068EADD0  token: 0x6000719
        private static System.Int32 criAtomExAcf_GetCategoryInfoById(System.UInt32 id, CriWare.CriAtomExAcfDebug.CategoryInfoForMarshaling& categoryInfo) { }
        // RVA: 0x068EB1DC  token: 0x600071A
        private static System.Int32 criAtomExAcf_GetNumBuses() { }
        // RVA: 0x068EAF7C  token: 0x600071B
        private static System.Int32 criAtomExAcf_GetDspBusInformation(System.UInt16 index, CriWare.CriAtomExAcfDebug.DspBusInfoForMarshaling& dspBusInfo) { }
        // RVA: 0x068EB1B8  token: 0x600071C
        private static System.Int32 criAtomExAcf_GetNumAisacControls() { }
        // RVA: 0x068EACD4  token: 0x600071D
        private static System.Int32 criAtomExAcf_GetAisacControlInfo(System.UInt16 index, CriWare.CriAtomExAcfDebug.AisacControlInfoForMarshaling& info) { }
        // RVA: 0x068EAC80  token: 0x600071E
        private static System.UInt32 criAtomExAcf_GetAisacControlIdByName(System.String name) { }
        // RVA: 0x068EAD58  token: 0x600071F
        private static System.IntPtr criAtomExAcf_GetAisacControlNameById(System.UInt32 id) { }
        // RVA: 0x068EB234  token: 0x6000720
        private static System.Int32 criAtomExAcf_GetNumGlobalAisacs() { }
        // RVA: 0x068EB134  token: 0x6000721
        private static System.Int32 criAtomExAcf_GetGlobalAisacInfo(System.UInt16 index, CriWare.CriAtomExAcfDebug.GlobalAisacInfoForMarshaling& info) { }
        // RVA: 0x068EB090  token: 0x6000722
        private static System.Int32 criAtomExAcf_GetGlobalAisacInfoByName(System.String name, CriWare.CriAtomExAcfDebug.GlobalAisacInfoForMarshaling& info) { }
        // RVA: 0x068EB258  token: 0x6000723
        private static System.Int32 criAtomExAcf_GetNumSelectors() { }
        // RVA: 0x068EB27C  token: 0x6000724
        private static System.Int32 criAtomExAcf_GetSelectorInfoByIndex(System.UInt16 index, CriWare.CriAtomExAcfDebug.SelectorInfoForMarshaling& info) { }
        // RVA: 0x068EB300  token: 0x6000725
        private static System.Int32 criAtomExAcf_GetSelectorInfoByName(System.String name, CriWare.CriAtomExAcfDebug.SelectorInfoForMarshaling& info) { }
        // RVA: 0x068EB3A4  token: 0x6000726
        private static System.Int32 criAtomExAcf_GetSelectorLabelInfo(CriWare.CriAtomExAcfDebug.SelectorInfoForMarshaling& info, System.UInt16 labelIndex, CriWare.CriAtomExAcfDebug.SelectorLabelInfoForMarshaling& label_info) { }

    }

    // TypeToken: 0x2000109  // size: 0x10
    public static class CriAtomExAcbDebug
    {
        // Methods
        // RVA: 0x068EA850  token: 0x600072D
        public static System.Boolean GetAcbInfo(CriWare.CriAtomExAcb acb, CriWare.CriAtomExAcbDebug.AcbInfo& acbInfo) { }
        // RVA: 0x068EA8AC  token: 0x600072E
        private static System.Int32 criAtomExAcb_GetAcbInfo(System.IntPtr acbHn, CriWare.CriAtomExAcbDebug.AcbInfoForMarshaling& acbInfo) { }

    }

    // TypeToken: 0x200010C  // size: 0x10
    public static class CriAtomExPlaybackDebug
    {
        // Methods
        // RVA: 0x068EB500  token: 0x6000730
        public static System.Boolean GetParameter(CriWare.CriAtomExPlayback playback, CriWare.CriAtomEx.Parameter parameterId, System.Single& value) { }
        // RVA: 0x068EB518  token: 0x6000731
        public static System.Boolean GetParameter(CriWare.CriAtomExPlayback playback, CriWare.CriAtomEx.Parameter parameterId, System.UInt32& value) { }
        // RVA: 0x068EB4E8  token: 0x6000732
        public static System.Boolean GetParameter(CriWare.CriAtomExPlayback playback, CriWare.CriAtomEx.Parameter parameterId, System.Int32& value) { }
        // RVA: 0x068EB4B8  token: 0x6000733
        public static System.Boolean GetAisacControl(CriWare.CriAtomExPlayback playback, System.UInt32 controlId, System.Single& value) { }
        // RVA: 0x068EB4D0  token: 0x6000734
        public static System.Boolean GetAisacControl(CriWare.CriAtomExPlayback playback, System.String controlName, System.Single& value) { }
        // RVA: 0x068EB670  token: 0x6000735
        private static System.Int32 criAtomExPlayback_GetParameterFloat32(System.UInt32 id, System.Int32 parameterId, System.Single& value) { }
        // RVA: 0x068EB790  token: 0x6000736
        private static System.Int32 criAtomExPlayback_GetParameterUint32(System.UInt32 id, System.Int32 parameterId, System.UInt32& value) { }
        // RVA: 0x068EB700  token: 0x6000737
        private static System.Int32 criAtomExPlayback_GetParameterSint32(System.UInt32 id, System.Int32 parameterId, System.Int32& value) { }
        // RVA: 0x068EB530  token: 0x6000738
        private static System.Int32 criAtomExPlayback_GetAisacControlById(System.UInt32 id, System.UInt32 controlId, System.Single& value) { }
        // RVA: 0x068EB5C0  token: 0x6000739
        private static System.Int32 criAtomExPlayback_GetAisacControlByName(System.UInt32 id, System.String controlName, System.Single& value) { }

    }

    // TypeToken: 0x200010D  // size: 0x20
    public abstract class CriDisposable : System.IDisposable
    {
        // Fields
        public System.Guid guid;  // 0x10

        // Methods
        // RVA: 0x04D80980  token: 0x600073A
        public System.Void .ctor() { }
        // RVA: -1  // abstract  token: 0x600073B
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x200010E  // size: 0x10
    public static class CriDisposableObjectManager
    {
        // Fields
        private static System.Collections.Generic.List<CriWare.CriDisposableObjectManager.ObjectRef> refList;  // static @ 0x0

        // Methods
        // RVA: 0x048DF4D0  token: 0x600073C
        private static System.Int32 SearchForDisposable(CriWare.CriDisposable disposable) { }
        // RVA: 0x048DF120  token: 0x600073D
        public static System.Void Register(CriWare.CriDisposable disposable, CriWare.CriDisposableObjectManager.ModuleType type) { }
        // RVA: 0x068F16A8  token: 0x600073E
        public static System.Boolean Unregister(CriWare.CriDisposable disposable) { }
        // RVA: 0x068F1664  token: 0x600073F
        public static System.Boolean IsDisposed(CriWare.CriDisposable disposable) { }
        // RVA: 0x068F12B8  token: 0x6000740
        public static System.Void CallOnModuleFinalization(CriWare.CriDisposableObjectManager.ModuleType type) { }
        // RVA: 0x068F159C  token: 0x6000741
        private static System.Int32 GetNextWithType(CriWare.CriDisposableObjectManager.ModuleType type) { }
        // RVA: 0x068F1300  token: 0x6000742
        public static System.Void DisposeAll(CriWare.CriDisposableObjectManager.ModuleType type) { }
        // RVA: 0x04D18600  token: 0x6000743
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000111  // size: 0x28
    public abstract class CriMonoBehaviour : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Guid <guid>k__BackingField;  // 0x18

        // Properties
        System.Guid guid { get; /* RVA: 0x04D890C0 */ set; /* RVA: 0x04D890D0 */ }

        // Methods
        // RVA: 0x046D8A80  token: 0x6000747
        public System.Void .ctor() { }
        // RVA: 0x03A06F30  token: 0x6000748
        protected virtual System.Void OnEnable() { }
        // RVA: 0x068FD094  token: 0x6000749
        protected virtual System.Void OnDisable() { }
        // RVA: -1  // abstract  token: 0x600074A
        public virtual System.Void CriInternalUpdate() { }
        // RVA: -1  // abstract  token: 0x600074B
        public virtual System.Void CriInternalLateUpdate() { }

    }

    // TypeToken: 0x2000112  // size: 0x18
    public class CriMonoBehaviourManager : UnityEngine.MonoBehaviour
    {
        // Fields
        private static CriWare.CriMonoBehaviourManager _instance;  // static @ 0x0
        private static System.Collections.Generic.List<CriWare.CriMonoBehaviour> criMonoBehaviourList;  // static @ 0x8

        // Properties
        CriWare.CriMonoBehaviourManager instance { get; /* RVA: 0x03A06F90 */ }

        // Methods
        // RVA: 0x03A06FF0  token: 0x600074D
        public static System.Void CreateInstance() { }
        // RVA: 0x03A07310  token: 0x600074E
        private static System.Int32 GetIndex(CriWare.CriMonoBehaviour criMonoBehaviour) { }
        // RVA: 0x03A070D0  token: 0x600074F
        public System.Boolean Register(CriWare.CriMonoBehaviour criMonoBehaviour) { }
        // RVA: 0x068FCF4C  token: 0x6000750
        public static System.Boolean UnRegister(CriWare.CriMonoBehaviour criMonoBehaviour) { }
        // RVA: 0x04CE6540  token: 0x6000751
        private System.Void Awake() { }
        // RVA: 0x03A07670  token: 0x6000752
        private System.Void Update() { }
        // RVA: 0x03A07440  token: 0x6000753
        private System.Void LateUpdate() { }
        // RVA: 0x05393520  token: 0x6000754
        public System.Void .ctor() { }
        // RVA: 0x04CDF490  token: 0x6000755
        private static System.Void .cctor() { }

    }

}

namespace CriWare.CriAtomDebugDetail
{

    // TypeToken: 0x2000113  // size: 0x10
    public class Utility
    {
        // Methods
        // RVA: 0x068FF1A8  token: 0x6000756
        public static System.String PtrToStringAutoOrNull(System.IntPtr stringPtr) { }
        // RVA: 0x041E1670  token: 0x6000757
        public System.Void .ctor() { }

    }

}

namespace CriWare.CriMana
{

    // TypeToken: 0x2000126  // size: 0x14
    public sealed struct CodecType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriMana.CodecType Unknown;  // const
        public static CriWare.CriMana.CodecType SofdecPrime;  // const
        public static CriWare.CriMana.CodecType H264;  // const
        public static CriWare.CriMana.CodecType VP9;  // const

    }

    // TypeToken: 0x2000127  // size: 0x14
    public sealed struct AlphaType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CriWare.CriMana.AlphaType CompoOpaq;  // const
        public static CriWare.CriMana.AlphaType CompoAlphaFull;  // const
        public static CriWare.CriMana.AlphaType CompoAlpha3Step;  // const
        public static CriWare.CriMana.AlphaType CompoAlpha32Bit;  // const

    }

    // TypeToken: 0x2000128  // size: 0x1C
    public sealed struct AudioInfo
    {
        // Fields
        public System.UInt32 samplingRate;  // 0x10
        public System.UInt32 numChannels;  // 0x14
        public System.UInt32 totalSamples;  // 0x18

    }

    // TypeToken: 0x2000129  // size: 0x58
    public class MovieInfo
    {
        // Fields
        private System.UInt32 _reserved1;  // 0x10
        public System.UInt32 numAlphaStreams;  // 0x14
        public System.UInt32 width;  // 0x18
        public System.UInt32 height;  // 0x1c
        public System.UInt32 dispWidth;  // 0x20
        public System.UInt32 dispHeight;  // 0x24
        public System.UInt32 framerateN;  // 0x28
        public System.UInt32 framerateD;  // 0x2c
        public System.UInt32 totalFrames;  // 0x30
        public CriWare.CriMana.CodecType codecType;  // 0x34
        public CriWare.CriMana.CodecType alphaCodecType;  // 0x38
        public System.UInt32 numAudioStreams;  // 0x3c
        public CriWare.CriMana.AudioInfo[] audioPrm;  // 0x40
        public System.UInt32 numSubtitleChannels;  // 0x48
        public System.UInt32 maxSubtitleSize;  // 0x4c
        public System.UInt32 maxChunkSize;  // 0x50

        // Properties
        System.Boolean hasAlpha { get; /* RVA: 0x069029DC */ set; /* RVA: 0x069029E4 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60007D4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200012A  // size: 0x58
    public class FrameInfo
    {
        // Fields
        public System.Int32 frameNo;  // 0x10
        public System.Int32 frameNoPerFile;  // 0x14
        public System.UInt32 width;  // 0x18
        public System.UInt32 height;  // 0x1c
        public System.UInt32 dispWidth;  // 0x20
        public System.UInt32 dispHeight;  // 0x24
        public System.UInt32 numImages;  // 0x28
        public System.UInt32 framerateN;  // 0x2c
        public System.UInt32 framerateD;  // 0x30
        private System.UInt32 _reserved1;  // 0x34
        public System.UInt64 time;  // 0x38
        public System.UInt64 tunit;  // 0x40
        public System.UInt32 cntConcatenatedMovie;  // 0x48
        private CriWare.CriMana.AlphaType alphaType;  // 0x4c
        public System.UInt32 cntSkippedFrames;  // 0x50
        public System.UInt32 totalFramesPerFile;  // 0x54

        // Methods
        // RVA: 0x041E1670  token: 0x60007D5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200012B  // size: 0x48
    public sealed struct EventPoint
    {
        // Fields
        public System.IntPtr cueName;  // 0x10
        public System.UInt32 cueNameSize;  // 0x18
        public System.UInt64 time;  // 0x20
        public System.UInt64 tunit;  // 0x28
        public System.Int32 type;  // 0x30
        public System.IntPtr paramString;  // 0x38
        public System.UInt32 paramStringSize;  // 0x40
        public System.UInt32 cntCallback;  // 0x44

    }

    // TypeToken: 0x200012C  // size: 0x10
    public static class Settings
    {
        // Methods
        // RVA: 0x06909390  token: 0x60007D6
        public static System.Void SetDecodeSkippingEnabled(System.Boolean enabled) { }

    }

    // TypeToken: 0x200012D  // size: 0x118
    public class Player : CriWare.CriDisposable
    {
        // Fields
        private static System.Int32 InvalidPlayerId;  // const
        private static CriWare.CriMana.Player updatingPlayer;  // static @ 0x0
        private System.Int32 playerId;  // 0x20
        private System.Boolean isDisposed;  // 0x24
        private CriWare.CriMana.Player.Status internalrequiredStatus;  // 0x28
        private CriWare.CriMana.Player.Status _nativeStatus;  // 0x2c
        private System.Nullable<CriWare.CriMana.Player.Status> lastNativeStatus;  // 0x30
        private System.Nullable<CriWare.CriMana.Player.Status> lastPlayerStatus;  // 0x38
        private System.Boolean wasStopping;  // 0x40
        private System.Boolean isPreparingForRendering;  // 0x41
        private System.Boolean isNativeStartInvoked;  // 0x42
        private System.Boolean isNativeInitialized;  // 0x43
        private CriWare.CriMana.Detail.RendererResource rendererResource;  // 0x48
        private CriWare.CriMana.MovieInfo _movieInfo;  // 0x50
        private CriWare.CriMana.FrameInfo _frameInfo;  // 0x58
        private System.Boolean isMovieInfoAvailable;  // 0x60
        private System.Boolean isFrameInfoAvailable;  // 0x61
        private CriWare.CriMana.Player.ShaderDispatchCallback _shaderDispatchCallback;  // 0x68
        private System.Boolean enableSubtitle;  // 0x70
        private System.Int32 subtitleBufferSize;  // 0x74
        private System.UInt32 droppedFrameCount;  // 0x78
        private CriWare.CriAtomExPlayer _atomExPlayer;  // 0x80
        private CriWare.CriAtomEx3dSource _atomEx3Dsource;  // 0x88
        private CriWare.CriMana.Player.TimerType _timerType;  // 0x90
        private System.Boolean isStoppingForSeek;  // 0x94
        public CriWare.CriMana.Player.CuePointCallback cuePointCallback;  // 0x98
        public CriWare.CriMana.Player.StatusChangeCallback statusChangeCallback;  // 0xa0
        private CriWare.CriMana.Player.SubtitleChangeCallback OnSubtitleChanged;  // 0xa8
        private System.Boolean <additiveMode>k__BackingField;  // 0xb0
        private System.Boolean <useMaterialBlendMode>k__BackingField;  // 0xb1
        private System.Int32 <maxFrameDrop>k__BackingField;  // 0xb4
        private System.Boolean <applyTargetAlpha>k__BackingField;  // 0xb8
        private System.Boolean <uiRenderMode>k__BackingField;  // 0xb9
        private System.IntPtr <subtitleBuffer>k__BackingField;  // 0xc0
        private System.Int32 <subtitleSize>k__BackingField;  // 0xc8
        private CriWare.CriManaMoviePlayerHolder <playerHolder>k__BackingField;  // 0xd0
        protected System.Collections.Generic.Queue<CriWare.CriMana.Player.Operation> operationQueue;  // 0xd8
        protected System.Nullable<CriWare.CriMana.Player.Operation> cntOperation;  // 0xe0

        // Properties
        CriWare.CriMana.Player.Status requiredStatus { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x06906788 */ }
        System.Boolean additiveMode { get; /* RVA: 0x04D88CC0 */ set; /* RVA: 0x04D88D20 */ }
        System.Boolean useMaterialBlendMode { get; /* RVA: 0x04D88CB0 */ set; /* RVA: 0x04D88D10 */ }
        System.Int32 maxFrameDrop { get; /* RVA: 0x04D892E0 */ set; /* RVA: 0x04D89330 */ }
        System.Boolean applyTargetAlpha { get; /* RVA: 0x04D892C0 */ set; /* RVA: 0x04D89320 */ }
        System.Boolean uiRenderMode { get; /* RVA: 0x04D89310 */ set; /* RVA: 0x04D89360 */ }
        System.Boolean isFrameAvailable { get; /* RVA: 0x04D892D0 */ }
        CriWare.CriMana.MovieInfo movieInfo { get; /* RVA: 0x06906660 */ }
        CriWare.CriMana.FrameInfo frameInfo { get; /* RVA: 0x06906648 */ }
        CriWare.CriMana.Player.Status status { get; /* RVA: 0x0690667C */ }
        CriWare.CriMana.Player.Status nativeStatus { get; /* RVA: 0x04D86710 */ }
        System.Int32 numberOfEntries { get; /* RVA: 0x06906670 */ }
        System.IntPtr subtitleBuffer { get; /* RVA: 0x04D892F0 */ set; /* RVA: 0x04D89340 */ }
        System.Int32 subtitleSize { get; /* RVA: 0x04D89300 */ set; /* RVA: 0x04D89350 */ }
        CriWare.CriAtomExPlayer atomExPlayer { get; /* RVA: 0x04D86260 */ }
        CriWare.CriAtomEx3dSource atomEx3DsourceForAmbisonics { get; /* RVA: 0x04D861F0 */ }
        CriWare.CriMana.Player.TimerType timerType { get; /* RVA: 0x04D86B60 */ }
        CriWare.CriManaMoviePlayerHolder playerHolder { get; /* RVA: 0x04D876B0 */ set; /* RVA: 0x0678D7BC */ }
        System.Boolean isAlive { get; /* RVA: 0x06906658 */ }

        // Events
        event CriWare.CriMana.Player.SubtitleChangeCallback OnSubtitleChanged;

        // Methods
        // RVA: 0x069062A4  token: 0x60007F4
        public System.Void .ctor() { }
        // RVA: 0x06905FB4  token: 0x60007F5
        public System.Void .ctor(System.Boolean advanced_audio_mode, System.Boolean ambisonics_mode, System.UInt32 max_path_length) { }
        // RVA: 0x06904E64  token: 0x60007F6
        protected virtual System.Void Finalize() { }
        // RVA: 0x069045C8  token: 0x60007F7
        public virtual System.Void Dispose() { }
        // RVA: 0x069040C0  token: 0x60007F8
        public System.Void CreateRendererResource(System.Int32 width, System.Int32 height, System.Boolean alpha) { }
        // RVA: 0x06904338  token: 0x60007F9
        public System.Void DisposeRendererResource() { }
        // RVA: 0x069056E0  token: 0x60007FA
        public System.Void Prepare() { }
        // RVA: 0x06905600  token: 0x60007FB
        public System.Void PrepareForRendering() { }
        // RVA: 0x06905B68  token: 0x60007FC
        public System.Void Start() { }
        // RVA: 0x06905C7C  token: 0x60007FD
        public System.Void Stop() { }
        // RVA: 0x06905BC8  token: 0x60007FE
        public System.Void StopForSeek() { }
        // RVA: 0x069055E8  token: 0x60007FF
        public System.Void Pause(System.Boolean sw) { }
        // RVA: 0x0690547C  token: 0x6000800
        public System.Boolean IsPaused() { }
        // RVA: 0x0690590C  token: 0x6000801
        public System.Boolean SetFile(CriWare.CriFsBinder binder, System.String moviePath, CriWare.CriMana.Player.SetMode setMode) { }
        // RVA: 0x06905850  token: 0x6000802
        public System.Boolean SetData(System.IntPtr data, System.Int64 dataSize, CriWare.CriMana.Player.SetMode setMode) { }
        // RVA: 0x06905820  token: 0x6000803
        public System.Boolean SetData(System.Byte[] data, System.Int64 datasize, CriWare.CriMana.Player.SetMode setMode) { }
        // RVA: 0x06905774  token: 0x6000804
        public System.Boolean SetContentId(CriWare.CriFsBinder binder, System.Int32 contentId, CriWare.CriMana.Player.SetMode setMode) { }
        // RVA: 0x069058D4  token: 0x6000805
        public System.Boolean SetFileRange(System.String filePath, System.UInt64 offset, System.Int64 range, CriWare.CriMana.Player.SetMode setMode) { }
        // RVA: 0x0690559C  token: 0x6000806
        public System.Void Loop(System.Boolean sw) { }
        // RVA: 0x06905A58  token: 0x6000807
        public System.Void SetMasterTimerType(CriWare.CriMana.Player.TimerType timerType) { }
        // RVA: 0x06905A90  token: 0x6000808
        public System.Void SetSeekPosition(System.Int32 frameNumber) { }
        // RVA: 0x06905A84  token: 0x6000809
        public System.Void SetMovieEventSyncMode(CriWare.CriMana.Player.MovieEventSyncMode mode) { }
        // RVA: 0x06905A9C  token: 0x600080A
        public System.Void SetSpeed(System.Single speed) { }
        // RVA: 0x06905A6C  token: 0x600080B
        public System.Void SetMaxPictureDataSize(System.UInt32 maxDataSize) { }
        // RVA: 0x0690575C  token: 0x600080C
        public System.Void SetBufferingTime(System.Single sec) { }
        // RVA: 0x06905A78  token: 0x600080D
        public System.Void SetMinBufferSize(System.Int32 min_buffer_size) { }
        // RVA: 0x06905750  token: 0x600080E
        public System.Void SetAudioTrack(System.Int32 track) { }
        // RVA: 0x0690572C  token: 0x600080F
        public System.Void SetAudioTrack(CriWare.CriMana.Player.AudioTrack track) { }
        // RVA: 0x06905AB4  token: 0x6000810
        public System.Void SetSubAudioTrack(System.Int32 track) { }
        // RVA: 0x06905AC0  token: 0x6000811
        public System.Void SetSubAudioTrack(CriWare.CriMana.Player.AudioTrack track) { }
        // RVA: 0x0690588C  token: 0x6000812
        public System.Void SetExtraAudioTrack(System.Int32 track) { }
        // RVA: 0x06905898  token: 0x6000813
        public System.Void SetExtraAudioTrack(CriWare.CriMana.Player.AudioTrack track) { }
        // RVA: 0x06905B5C  token: 0x6000814
        public System.Void SetVolume(System.Single volume) { }
        // RVA: 0x06904F90  token: 0x6000815
        public System.Single GetVolume() { }
        // RVA: 0x06905AE4  token: 0x6000816
        public System.Void SetSubAudioVolume(System.Single volume) { }
        // RVA: 0x06904EF0  token: 0x6000817
        public System.Single GetSubAudioVolume() { }
        // RVA: 0x069058BC  token: 0x6000818
        public System.Void SetExtraAudioVolume(System.Single volume) { }
        // RVA: 0x06904EE4  token: 0x6000819
        public System.Single GetExtraAudioVolume() { }
        // RVA: 0x06905768  token: 0x600081A
        public System.Void SetBusSendLevel(System.String bus_name, System.Single level) { }
        // RVA: 0x06905AA8  token: 0x600081B
        public System.Void SetSubAudioBusSendLevel(System.String bus_name, System.Single volume) { }
        // RVA: 0x06905880  token: 0x600081C
        public System.Void SetExtraAudioBusSendLevel(System.String bus_name, System.Single volume) { }
        // RVA: 0x06905AF0  token: 0x600081D
        public System.Void SetSubtitleChannel(System.Int32 channel) { }
        // RVA: 0x0485F170  token: 0x600081E
        public System.Void SetShaderDispatchCallback(CriWare.CriMana.Player.ShaderDispatchCallback shaderDispatchCallback) { }
        // RVA: 0x06904EFC  token: 0x600081F
        public System.Int64 GetTime() { }
        // RVA: 0x06904ED8  token: 0x6000820
        public System.Int32 GetDisplayedFrameNo() { }
        // RVA: 0x06904F9C  token: 0x6000821
        public System.Boolean HasRenderedNewFrame() { }
        // RVA: 0x06905720  token: 0x6000822
        public System.Void SetAsrRackId(System.Int32 asrRackId) { }
        // RVA: 0x06905B50  token: 0x6000823
        public System.Void SetUserTimeCorrectionThreshold(System.UInt32 threshold_msec) { }
        // RVA: 0x069058C8  token: 0x6000824
        public System.Void SetExtraFramePoolSize(System.UInt32 size) { }
        // RVA: 0x06905F08  token: 0x6000825
        public System.Void UpdateWithUserTime(System.UInt64 timeCount, System.UInt64 timeUnit) { }
        // RVA: 0x069059D4  token: 0x6000826
        public System.Void SetManualTimerUnit(System.UInt64 timeUnitN, System.UInt64 timeUnitD) { }
        // RVA: 0x06905E98  token: 0x6000827
        public System.Void UpdateWithManualTimeAdvanced() { }
        // RVA: 0x06905F98  token: 0x6000828
        public System.Void Update() { }
        // RVA: 0x069055AC  token: 0x6000829
        public System.Void OnWillRenderObject(CriWare.CriManaMovieMaterialBase sender) { }
        // RVA: 0x06905CC4  token: 0x600082A
        public System.Boolean UpdateMaterial(UnityEngine.Material material) { }
        // RVA: 0x06905488  token: 0x600082C
        public System.Void IssuePluginEvent(CriWare.CriMana.Player.CriManaUnityPlayer_RenderEventAction renderEventAction) { }
        // RVA: 0x06904370  token: 0x600082D
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x06904FC4  token: 0x600082E
        private System.Void InternalUpdate() { }
        // RVA: 0x06905500  token: 0x600082F
        private System.Collections.IEnumerator IssuePluginUpdatesForFrames(System.Int32 frameCount, UnityEngine.MonoBehaviour playerHolder, System.Boolean destroy, System.Int32 playerId) { }
        // RVA: 0x0690431C  token: 0x6000830
        private System.Void DisableInfos(System.Boolean keepFrameInfo) { }
        // RVA: 0x06905644  token: 0x6000831
        private System.Void PrepareNativePlayer() { }
        // RVA: 0x06905D2C  token: 0x6000832
        private System.Void UpdateNativePlayer() { }
        // RVA: 0x069053C0  token: 0x6000833
        private System.Void InvokePlayerStatusCheck() { }
        // RVA: 0x06902B64  token: 0x6000834
        private System.Void AllocateSubtitleBuffer(System.Int32 size) { }
        // RVA: 0x069042A4  token: 0x6000835
        private System.Void DeallocateSubtitleBuffer() { }
        // RVA: 0x069055E8  token: 0x6000836
        private System.Void PauseOnApplicationPause(System.Boolean sw) { }
        // RVA: 0x0690423C  token: 0x6000837
        private static System.Void CuePointCallbackFromNative(System.IntPtr ptr1, System.IntPtr ptr2, CriWare.CriMana.EventPoint& eventPoint) { }
        // RVA: 0x0690404C  token: 0x6000838
        private static System.Int32 CRIWAREFD186F02() { }
        // RVA: 0x06903574  token: 0x6000839
        private static System.Int32 CRIWARE72A5B8BF() { }
        // RVA: 0x069037B0  token: 0x600083A
        private static System.Int32 CRIWARE95A85424(System.Boolean useAtomExPlayer, System.UInt32 maxPathLength) { }
        // RVA: 0x0690393C  token: 0x600083B
        private static System.Void CRIWAREABF659EC(System.Int32 player_id) { }
        // RVA: 0x06903F08  token: 0x600083C
        private static System.Void CRIWAREEDB46019(System.Int32 player_id, System.IntPtr binder, System.String path) { }
        // RVA: 0x069039D4  token: 0x600083D
        private static System.Void CRIWAREB28B5D50(System.Int32 player_id, System.IntPtr binder, System.Int32 content_id) { }
        // RVA: 0x069033D4  token: 0x600083E
        private static System.Void CRIWARE65553651(System.Int32 player_id, System.String path, System.UInt64 offset, System.Int64 range) { }
        // RVA: 0x06903608  token: 0x600083F
        private static System.Void CRIWARE7C753A2E(System.Int32 player_id, System.IntPtr data, System.Int64 datasize) { }
        // RVA: 0x0690369C  token: 0x6000840
        private static System.Void CRIWARE7C753A2E(System.Int32 player_id, System.Byte[] data, System.Int64 datasize) { }
        // RVA: 0x069030B8  token: 0x6000841
        private static System.Boolean CRIWARE328F2911(System.Int32 player_id, System.IntPtr binder, System.String path, System.Boolean repeat) { }
        // RVA: 0x06903DEC  token: 0x6000842
        private static System.Boolean CRIWAREDA0693CD(System.Int32 player_id, System.IntPtr binder, System.Int32 content_id, System.Boolean repeat) { }
        // RVA: 0x06902CAC  token: 0x6000843
        private static System.Boolean CRIWARE162DD162(System.Int32 player_id, System.String path, System.UInt64 offset, System.Int64 range, System.Boolean repeat) { }
        // RVA: 0x06903ADC  token: 0x6000844
        private static System.Boolean CRIWAREC35C3DC3(System.Int32 player_id, System.IntPtr data, System.Int64 datasize, System.Boolean repeat) { }
        // RVA: 0x06903B84  token: 0x6000845
        private static System.Boolean CRIWAREC35C3DC3(System.Int32 player_id, System.Byte[] data, System.Int64 datasize, System.Boolean repeat) { }
        // RVA: 0x06903488  token: 0x6000846
        private static System.Void CRIWARE66A63134(System.Int32 player_id) { }
        // RVA: 0x06903738  token: 0x6000847
        private static System.Int32 CRIWARE88D04318(System.Int32 player_id) { }
        // RVA: 0x06903D60  token: 0x6000848
        private static System.Void CRIWARED9F7168D(System.Int32 player_id, CriWare.CriMana.Player.CuePointCallbackFromNativeDelegate cbfunc) { }
        // RVA: 0x06902DF0  token: 0x6000849
        private static System.Void CRIWARE1E785A43(System.Int32 player_id, CriWare.CriMana.MovieInfo movie_info) { }
        // RVA: 0x069031E4  token: 0x600084A
        private static System.Int32 CRIWARE3CE8CE6A(System.Int32 player_id, System.IntPtr subtitle_buffer, System.UInt32& subtitle_size) { }
        // RVA: 0x069031B0  token: 0x600084B
        private static System.Void CRIWARE362A826C(System.Int32 player_id) { }
        // RVA: 0x069033A0  token: 0x600084C
        private static System.Void CRIWARE61523A0C(System.Int32 player_id) { }
        // RVA: 0x06903A68  token: 0x600084D
        private static System.Void CRIWAREBB898C91(System.Int32 player_id) { }
        // RVA: 0x069034FC  token: 0x600084E
        private static System.Void CRIWARE705B8C6D(System.Int32 player_id, System.Int32 seek_frame_no) { }
        // RVA: 0x06903EC8  token: 0x600084F
        private static System.Void CRIWAREEB32FC73(System.Int32 player_id, CriWare.CriMana.Player.MovieEventSyncMode mode) { }
        // RVA: 0x06902DB0  token: 0x6000850
        private static System.Void CRIWARE18BFCB88(System.Int32 player_id, System.Int32 sw) { }
        // RVA: 0x06903FB0  token: 0x6000851
        private static System.Boolean CRIWAREEFA5210A(System.Int32 player_id) { }
        // RVA: 0x0690332C  token: 0x6000852
        private static System.Void CRIWARE4331E077(System.Int32 player_id, System.Int32 sw) { }
        // RVA: 0x069035D4  token: 0x6000853
        private static System.Int64 CRIWARE7779586C(System.Int32 player_id) { }
        // RVA: 0x0690317C  token: 0x6000854
        private static System.Int32 CRIWARE33981CC9(System.Int32 player_id) { }
        // RVA: 0x06902D7C  token: 0x6000855
        private static System.IntPtr CRIWARE165B6B70(System.Int32 player_id) { }
        // RVA: 0x069035A0  token: 0x6000856
        private static System.Int32 CRIWARE7500F0DD(System.Int32 player_id) { }
        // RVA: 0x06903D20  token: 0x6000857
        private static System.Void CRIWARED99932F6(System.Int32 player_id, System.Int32 track) { }
        // RVA: 0x06902F10  token: 0x6000858
        private static System.Void CRIWARE1FF2F5BE(System.Int32 player_id, System.Single vol) { }
        // RVA: 0x06903CEC  token: 0x6000859
        private static System.Single CRIWARED30C9C6F(System.Int32 player_id) { }
        // RVA: 0x06902C6C  token: 0x600085A
        private static System.Void CRIWARE0EA59F65(System.Int32 player_id, System.Int32 track) { }
        // RVA: 0x06902ECC  token: 0x600085B
        private static System.Void CRIWARE1EBB12C5(System.Int32 player_id, System.Single vol) { }
        // RVA: 0x06903834  token: 0x600085C
        private static System.Single CRIWARE966C4BA9(System.Int32 player_id) { }
        // RVA: 0x06902F54  token: 0x600085D
        private static System.Void CRIWARE213452EF(System.Int32 player_id, System.Int32 track) { }
        // RVA: 0x06902C28  token: 0x600085E
        private static System.Void CRIWARE0348BF1B(System.Int32 player_id, System.Single vol) { }
        // RVA: 0x06903C34  token: 0x600085F
        private static System.Single CRIWAREC5A3A88D(System.Int32 player_id) { }
        // RVA: 0x06903054  token: 0x6000860
        private static System.Void CRIWARE31668293(System.Int32 player_id, System.String bus_name, System.Single level) { }
        // RVA: 0x06903970  token: 0x6000861
        private static System.Void CRIWAREB2460D7A(System.Int32 player_id, System.String bus_name, System.Single level) { }
        // RVA: 0x06903FE8  token: 0x6000862
        private static System.Void CRIWAREF4B0CC4E(System.Int32 player_id, System.String bus_name, System.Single level) { }
        // RVA: 0x06903014  token: 0x6000863
        private static System.Void CRIWARE2C8FF595(System.Int32 player_id, System.Int32 channel) { }
        // RVA: 0x069038F8  token: 0x6000864
        private static System.Void CRIWAREAB576D15(System.Int32 player_id, System.Single speed) { }
        // RVA: 0x06903278  token: 0x6000865
        private static System.Void CRIWARE3F9C5D06(System.Int32 player_id, System.UInt32 max_data_size) { }
        // RVA: 0x0690376C  token: 0x6000866
        public static System.Void CRIWARE8A87737B(System.Int32 player_id, System.Single sec) { }
        // RVA: 0x069034BC  token: 0x6000867
        public static System.Void CRIWARE6756AE56(System.Int32 player_id, System.Int32 min_buffer_size) { }
        // RVA: 0x069038B8  token: 0x6000868
        public static System.Void CRIWAREA7F6DE6B(System.Int32 player_id, System.Int32 asr_rack_id) { }
        // RVA: 0x06903E94  token: 0x6000869
        private static System.Void CRIWAREE2DF1163(System.Int32 player_id) { }
        // RVA: 0x06903A9C  token: 0x600086A
        private static System.Void CRIWAREBD24DE27(System.Int32 player_id, CriWare.CriMana.Player.TimerType timer_type) { }
        // RVA: 0x06903C68  token: 0x600086B
        private static System.Void CRIWAREC7C5CC77(System.Int32 player_id, System.UInt64 user_count, System.UInt64 user_unit) { }
        // RVA: 0x06903868  token: 0x600086C
        private static System.Void CRIWAREA59E0F01(System.Int32 player_id, System.UInt64 timer_unit_n, System.UInt64 timer_unit_d) { }
        // RVA: 0x0690336C  token: 0x600086D
        private static System.Void CRIWARE55ECC3E4(System.Int32 player_id) { }
        // RVA: 0x06903CB8  token: 0x600086E
        private static System.Void CRIWARECEA2FA98(System.Int32 player_id) { }
        // RVA: 0x06902F94  token: 0x600086F
        private static System.IntPtr CRIWARE27ED314F(System.Int32 player_id, System.Int32 bufferSize) { }
        // RVA: 0x0690353C  token: 0x6000870
        private static System.Boolean CRIWARE725ED4B4(System.Int32 player_id) { }
        // RVA: 0x069032F8  token: 0x6000871
        private static System.Void CRIWARE419ECAF8(System.Int32 player_id) { }
        // RVA: 0x069065E0  token: 0x6000872
        private static System.IntPtr criWareUnity_GetRenderEventFunc() { }
        // RVA: 0x06906560  token: 0x6000873
        private static System.Void criManaUnityPlayer_SetUserTimeCorrectionThreshold(System.Int32 player_id, System.UInt32 threshold_msec) { }
        // RVA: 0x069064E0  token: 0x6000874
        private static System.Void criManaUnityPlayer_SetExtraFramePoolSize(System.Int32 player_id, System.UInt32 size) { }
        // RVA: 0x06904078  token: 0x6000875
        public System.Void ClearAllOperation() { }
        // RVA: 0x06904BA8  token: 0x6000876
        protected System.Void ExecuteOperation() { }
        // RVA: 0x06904724  token: 0x6000877
        public System.Void EasyPrepare() { }
        // RVA: 0x069046B0  token: 0x6000878
        public System.Void EasyPrepareForRendering() { }
        // RVA: 0x06904A5C  token: 0x6000879
        public System.Void EasyStart() { }
        // RVA: 0x06904618  token: 0x600087A
        public System.Void EasyPause(System.Boolean sw) { }
        // RVA: 0x06904B44  token: 0x600087B
        public System.Void EasyStop() { }
        // RVA: 0x06904AD0  token: 0x600087C
        public System.Void EasyStopForSeek() { }
        // RVA: 0x06904798  token: 0x600087D
        public System.Void EasySetFile(CriWare.CriFsBinder binder, System.String moviePath, CriWare.CriMana.Player.SetMode setMode) { }
        // RVA: 0x06904884  token: 0x600087E
        public System.Void EasySetSeekTime(System.Single time) { }
        // RVA: 0x06904920  token: 0x600087F
        public System.Void EasyStartAtTime(System.Single time) { }
        // RVA: 0x06904F08  token: 0x6000880
        public System.Single GetVideoLength() { }

    }

}

namespace CriWare.CriMana.Detail
{

    // TypeToken: 0x200013C  // size: 0x10
    public static class AutoResisterRendererResourceFactories
    {
        // Methods
        // RVA: 0x039EC640  token: 0x60008A3
        public static System.Void InvokeAutoRegister() { }

    }

    // TypeToken: 0x2000140  // size: 0x98
    public class RendererResourceH264Yuv : CriWare.CriMana.Detail.RendererResourceSofdecPrimeYuv
    {
        // Methods
        // RVA: 0x06906EB4  token: 0x60008B0
        public System.Void .ctor(System.Int32 playerId, CriWare.CriMana.MovieInfo movieInfo, System.Boolean additive, UnityEngine.Shader userShader) { }

    }

    // TypeToken: 0x2000141  // size: 0x30
    public abstract class RendererResource : System.IDisposable
    {
        // Fields
        private System.Boolean disposed;  // 0x10
        protected UnityEngine.Shader shader;  // 0x18
        protected UnityEngine.Material currentMaterial;  // 0x20
        protected System.Boolean hasAlpha;  // 0x28
        protected System.Boolean additive;  // 0x29
        protected System.Boolean applyTargetAlpha;  // 0x2a
        protected System.Boolean ui;  // 0x2b
        private System.Boolean _useMaterialBlendMode;  // 0x2c

        // Properties
        System.Boolean useMaterialBlendMode { get; /* RVA: 0x04D869F0 */ set; /* RVA: 0x04D86A00 */ }

        // Methods
        // RVA: 0x06909078  token: 0x60008B3
        protected virtual System.Void Finalize() { }
        // RVA: 0x06908FF0  token: 0x60008B4
        public virtual System.Void Dispose() { }
        // RVA: 0x06909040  token: 0x60008B5
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x06909120  token: 0x60008B6
        public System.Int32 GetNumberOfFrameBeforeDestroy(System.Int32 playerId) { }
        // RVA: 0x0690918C  token: 0x60008B7
        protected System.Void SetupStaticMaterialProperties() { }
        // RVA: 0x069090EC  token: 0x60008B8
        private System.Void GetBlendModes(System.Int32& srcBlendMode, System.Int32& dstBlendMode) { }
        // RVA: 0x06909140  token: 0x60008B9
        public virtual System.Void SetApplyTargetAlpha(System.Boolean flag) { }
        // RVA: 0x06909180  token: 0x60008BA
        public virtual System.Void SetUiRenderMode(System.Boolean flag) { }
        // RVA: -1  // abstract  token: 0x60008BB
        protected virtual System.Void OnDisposeManaged() { }
        // RVA: -1  // abstract  token: 0x60008BC
        protected virtual System.Void OnDisposeUnmanaged() { }
        // RVA: -1  // abstract  token: 0x60008BD
        public virtual System.Boolean IsPrepared() { }
        // RVA: -1  // abstract  token: 0x60008BE
        public virtual System.Boolean ContinuePreparing() { }
        // RVA: -1  // abstract  token: 0x60008BF
        public virtual System.Void AttachToPlayer(System.Int32 playerId) { }
        // RVA: -1  // abstract  token: 0x60008C0
        public virtual System.Boolean UpdateFrame(System.Int32 playerId, CriWare.CriMana.FrameInfo frameInfo, System.Boolean& frameDrop) { }
        // RVA: -1  // abstract  token: 0x60008C1
        public virtual System.Boolean UpdateMaterial(UnityEngine.Material material) { }
        // RVA: -1  // abstract  token: 0x60008C2
        public virtual System.Void UpdateTextures() { }
        // RVA: -1  // abstract  token: 0x60008C3
        public virtual System.Boolean IsSuitable(System.Int32 playerId, CriWare.CriMana.MovieInfo movieInfo, System.Boolean additive, UnityEngine.Shader userShader) { }
        // RVA: 0x041E1670  token: 0x60008C4
        public virtual System.Void OnPlayerPause(System.Boolean pauseStatus, System.Boolean triggredFromApplciationPause) { }
        // RVA: 0x041E1670  token: 0x60008C5
        public virtual System.Void OnPlayerStop() { }
        // RVA: 0x012081B0  token: 0x60008C6
        public virtual System.Boolean OnPlayerStopForSeek() { }
        // RVA: 0x041E1670  token: 0x60008C7
        public virtual System.Void OnPlayerStart() { }
        // RVA: 0x012081B0  token: 0x60008C8
        public virtual System.Boolean ShouldSkipDestroyOnStopForSeek() { }
        // RVA: 0x02FFF600  token: 0x60008C9
        public virtual System.Boolean HasRenderedNewFrame() { }
        // RVA: 0x04D89420  token: 0x60008CA
        public static System.UInt32 NextPowerOfTwo(System.UInt32 x) { }
        // RVA: 0x06909138  token: 0x60008CB
        public static System.Int32 NextPowerOfTwo(System.Int32 x) { }
        // RVA: 0x04D89410  token: 0x60008CC
        public static System.Int32 CeilingWith(System.Int32 x, System.Int32 ceilingValue) { }
        // RVA: 0x04D893D0  token: 0x60008CD
        public static System.Int32 Ceiling16(System.Int32 x) { }
        // RVA: 0x04D893F0  token: 0x60008CE
        public static System.Int32 Ceiling32(System.Int32 x) { }
        // RVA: 0x04D89400  token: 0x60008CF
        public static System.Int32 Ceiling64(System.Int32 x) { }
        // RVA: 0x04D893E0  token: 0x60008D0
        public static System.Int32 Ceiling256(System.Int32 x) { }
        // RVA: 0x06908F44  token: 0x60008D1
        protected static System.Void DisposeTextures(UnityEngine.Texture[] textures) { }
        // RVA: 0x0690914C  token: 0x60008D2
        protected static System.Void SetKeyword(UnityEngine.Material material, System.String keyword, System.Boolean flag) { }
        // RVA: 0x06908D2C  token: 0x60008D3
        protected static System.Boolean CRIWARE98318E69(System.Int32 player_id, System.Int32 num_textures, System.IntPtr[] tex_ptrs, CriWare.CriMana.FrameInfo frame_info, System.Boolean& frame_drop) { }
        // RVA: 0x06908E0C  token: 0x60008D4
        protected static System.Boolean CRIWARED618B369(System.Int32 player_id, System.Int32 num_textures, System.IntPtr[] tex_ptrs) { }
        // RVA: 0x06908CCC  token: 0x60008D5
        protected static System.Boolean CRIWARE5267266A(System.Int32 player_id, System.Int32 num_textures, System.IntPtr[] tex_ptrs) { }
        // RVA: 0x06908E6C  token: 0x60008D6
        protected static System.Boolean CRIWAREE519EFD4(System.Int32 player_id, System.Int32 num_textures, System.IntPtr[] tex_ptrs) { }
        // RVA: 0x06908ECC  token: 0x60008D7
        protected static System.SByte CRIWAREF14F77D8(System.Int32 player_id) { }
        // RVA: 0x041E1670  token: 0x60008D8
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000142  // size: 0x18
    public abstract class RendererResourceFactory : System.IDisposable
    {
        // Fields
        private static System.Collections.Generic.SortedList<System.Int32,CriWare.CriMana.Detail.RendererResourceFactory> factoryList;  // static @ 0x0
        private System.Boolean disposed;  // 0x10

        // Methods
        // RVA: 0x039EC5B0  token: 0x60008D9
        public static System.Void RegisterFactory(CriWare.CriMana.Detail.RendererResourceFactory factory, System.Int32 priority) { }
        // RVA: 0x06906C20  token: 0x60008DA
        public static System.Void DisposeAllFactories() { }
        // RVA: 0x0690699C  token: 0x60008DB
        public static CriWare.CriMana.Detail.RendererResource DispatchAndCreate(System.Int32 playerId, CriWare.CriMana.MovieInfo movieInfo, System.Boolean additive, UnityEngine.Shader userShader) { }
        // RVA: 0x06906E40  token: 0x60008DC
        protected virtual System.Void Finalize() { }
        // RVA: 0x06906DF0  token: 0x60008DD
        public virtual System.Void Dispose() { }
        // RVA: 0x06906DB8  token: 0x60008DE
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: -1  // abstract  token: 0x60008DF
        protected virtual System.Void OnDisposeManaged() { }
        // RVA: -1  // abstract  token: 0x60008E0
        protected virtual System.Void OnDisposeUnmanaged() { }
        // RVA: -1  // abstract  token: 0x60008E1
        public virtual CriWare.CriMana.Detail.RendererResource CreateRendererResource(System.Int32 playerId, CriWare.CriMana.MovieInfo movieInfo, System.Boolean additive, UnityEngine.Shader userShader) { }
        // RVA: 0x041E1670  token: 0x60008E2
        protected System.Void .ctor() { }
        // RVA: 0x04D16F80  token: 0x60008E3
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000143  // size: 0x18
    public class RendererResourceFactoryPriorityAttribute : System.Attribute
    {
        // Fields
        public readonly System.Int32 priority;  // 0x10

        // Methods
        // RVA: 0x014F51F0  token: 0x60008E4
        public System.Void .ctor(System.Int32 priority) { }

    }

    // TypeToken: 0x2000144  // size: 0x98
    public class RendererResourceSofdecPrimeYuv : CriWare.CriMana.Detail.RendererResource
    {
        // Fields
        private System.Int32 width;  // 0x30
        private System.Int32 height;  // 0x34
        private System.Boolean useUserShader;  // 0x38
        private CriWare.CriMana.CodecType codecType;  // 0x3c
        private UnityEngine.Vector4 movieTextureST;  // 0x40
        private UnityEngine.Vector4 movieChromaTextureST;  // 0x50
        private UnityEngine.Texture2D[] textureY;  // 0x60
        private UnityEngine.Texture2D[] textureU;  // 0x68
        private UnityEngine.Texture2D[] textureV;  // 0x70
        private UnityEngine.Texture2D[] textureA;  // 0x78
        private System.IntPtr[] nativeTextures;  // 0x80
        private System.Int32 currentTextureSet;  // 0x88
        private System.Int32 drawTextureSet;  // 0x8c
        private System.Int32 playerID;  // 0x90
        private System.Boolean hasTextureUpdated;  // 0x94
        private System.Boolean isTextureReady;  // 0x95
        private System.Boolean hasRenderedNewFrame;  // 0x96

        // Properties
        System.Int32 NumTextureSets { get; /* RVA: 0x04D85B80 */ }
        System.Boolean IsEditor { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x06908764  token: 0x60008E6
        public System.Void .ctor(System.Int32 playerId, CriWare.CriMana.MovieInfo movieInfo, System.Boolean additive, UnityEngine.Shader userShader) { }
        // RVA: 0x041E1670  token: 0x60008E7
        protected virtual System.Void OnDisposeManaged() { }
        // RVA: 0x06907F84  token: 0x60008E9
        protected virtual System.Void OnDisposeUnmanaged() { }
        // RVA: 0x04D893B0  token: 0x60008EA
        public virtual System.Boolean IsPrepared() { }
        // RVA: 0x02FFF600  token: 0x60008EB
        public virtual System.Boolean ContinuePreparing() { }
        // RVA: 0x06907E94  token: 0x60008EC
        public virtual System.Boolean IsSuitable(System.Int32 playerId, CriWare.CriMana.MovieInfo movieInfo, System.Boolean additive, UnityEngine.Shader userShader) { }
        // RVA: 0x04D893C0  token: 0x60008ED
        public virtual System.Boolean OnPlayerStopForSeek() { }
        // RVA: 0x04D893A0  token: 0x60008EE
        public virtual System.Boolean HasRenderedNewFrame() { }
        // RVA: 0x06907E28  token: 0x60008EF
        public virtual System.Void AttachToPlayer(System.Int32 playerId) { }
        // RVA: 0x06908200  token: 0x60008F0
        public virtual System.Boolean UpdateFrame(System.Int32 playerId, CriWare.CriMana.FrameInfo frameInfo, System.Boolean& frameDrop) { }
        // RVA: 0x0690829C  token: 0x60008F1
        public virtual System.Boolean UpdateMaterial(UnityEngine.Material material) { }
        // RVA: 0x069084C4  token: 0x60008F2
        private System.Void UpdateMovieTextureST(System.UInt32 dispWidth, System.UInt32 dispHeight) { }
        // RVA: 0x069085D0  token: 0x60008F3
        public virtual System.Void UpdateTextures() { }

    }

    // TypeToken: 0x2000145  // size: 0x88
    public class RendererResourceSofdecPrimeYuvRawData : CriWare.CriMana.Detail.RendererResource
    {
        // Fields
        private System.Int32 width;  // 0x30
        private System.Int32 height;  // 0x34
        private System.Int32 chromaWidth;  // 0x38
        private System.Int32 chromaHeight;  // 0x3c
        private System.Boolean useUserShader;  // 0x40
        private CriWare.CriMana.CodecType codecType;  // 0x44
        private UnityEngine.Vector4 movieTextureST;  // 0x48
        private UnityEngine.Vector4 movieChromaTextureST;  // 0x58
        private UnityEngine.Texture2D[][] textures;  // 0x68
        private System.Int32 currentTextureSet;  // 0x70
        private System.Int32 drawTextureSet;  // 0x74
        private System.IntPtr[] nativePixels;  // 0x78
        private System.Int32 playerID;  // 0x80
        private System.Boolean hasTextureUpdated;  // 0x84
        private System.Boolean isTextureReady;  // 0x85
        private System.Boolean hasRenderedNewFrame;  // 0x86

        // Properties
        System.Int32 NumTextureSets { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x069078FC  token: 0x60008F5
        public System.Void .ctor(System.Int32 playerId, CriWare.CriMana.MovieInfo movieInfo, System.Boolean additive, UnityEngine.Shader userShader) { }
        // RVA: 0x041E1670  token: 0x60008F6
        protected virtual System.Void OnDisposeManaged() { }
        // RVA: 0x06907148  token: 0x60008F7
        protected virtual System.Void OnDisposeUnmanaged() { }
        // RVA: 0x04D89370  token: 0x60008F8
        public virtual System.Boolean IsPrepared() { }
        // RVA: 0x02FFF600  token: 0x60008F9
        public virtual System.Boolean ContinuePreparing() { }
        // RVA: 0x06907058  token: 0x60008FA
        public virtual System.Boolean IsSuitable(System.Int32 playerId, CriWare.CriMana.MovieInfo movieInfo, System.Boolean additive, UnityEngine.Shader userShader) { }
        // RVA: 0x04D89380  token: 0x60008FB
        public virtual System.Boolean OnPlayerStopForSeek() { }
        // RVA: 0x04D88E70  token: 0x60008FC
        public virtual System.Boolean HasRenderedNewFrame() { }
        // RVA: 0x06906EC4  token: 0x60008FD
        public virtual System.Void AttachToPlayer(System.Int32 playerId) { }
        // RVA: 0x069071B4  token: 0x60008FE
        public virtual System.Boolean UpdateFrame(System.Int32 playerId, CriWare.CriMana.FrameInfo frameInfo, System.Boolean& frameDrop) { }
        // RVA: 0x06907248  token: 0x60008FF
        public virtual System.Boolean UpdateMaterial(UnityEngine.Material material) { }
        // RVA: 0x069074B8  token: 0x6000900
        private System.Void UpdateMovieTextureST(System.UInt32 dispWidth, System.UInt32 dispHeight) { }
        // RVA: 0x069075D4  token: 0x6000901
        public virtual System.Void UpdateTextures() { }
        // RVA: 0x06906F30  token: 0x6000902
        private static System.Void CalculateTextureSize(System.Int32& w, System.Int32& h, System.Int32 videoWidth, System.Int32 videoHeight, CriWare.CriMana.CodecType type, System.Boolean isChroma) { }

    }

}

namespace CriWare.CriTimeline.Atom
{

    // TypeToken: 0x200011E  // size: 0x30
    public sealed struct CriAtomClipPlayConfig
    {
        // Fields
        public readonly CriWare.CriTimeline.Atom.CriAtomClipBase clip;  // 0x10
        public readonly System.Int64 startTimeMs;  // 0x18
        public readonly System.Double speedRate;  // 0x20
        public readonly System.Boolean loop;  // 0x28

        // Methods
        // RVA: 0x068FF974  token: 0x6000794
        public System.Void .ctor(CriWare.CriTimeline.Atom.CriAtomClipBase clip, System.Int64 startTimeMs, System.Double speedRate, System.Boolean loop) { }

    }

    // TypeToken: 0x200011F  // size: 0x40
    public class CriAtomBehaviour : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        public System.Single volume;  // 0x10
        public System.Single pitch;  // 0x14
        public System.Single AISACValue;  // 0x18
        private static System.Int32 cPreviewStopTimeMs;  // static @ 0x0
        private CriWare.CriAtomExAcb m_acb;  // 0x20
        private System.String m_lastCueSheetPath;  // 0x28
        private CriWare.CriAtomExPlayback <playback>k__BackingField;  // 0x30
        private System.Boolean _IsClipPlaying;  // 0x34
        private System.Double _CueLength;  // 0x38

        // Properties
        CriWare.CriAtomExPlayback playback { get; /* RVA: 0x04D864F0 */ set; /* RVA: 0x04D86740 */ }
        System.Boolean IsClipPlaying { get; /* RVA: 0x04D86A90 */ set; /* RVA: 0x04D86AA0 */ }
        System.Double CueLength { get; /* RVA: 0x04D869C0 */ set; /* RVA: 0x04D869E0 */ }

        // Methods
        // RVA: 0x04D89160  token: 0x600079B
        public virtual System.Void OnGraphStop(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x068FF250  token: 0x600079C
        public System.Void Play(CriWare.CriAtomSourceBase atomSource, CriWare.CriTimeline.Atom.CriAtomClipPlayConfig config) { }
        // RVA: 0x068FF434  token: 0x600079D
        public System.Void PreviewPlay(System.Guid trackId, System.Boolean instantStop, CriWare.CriTimeline.Atom.CriAtomClipPlayConfig config) { }
        // RVA: 0x068FF748  token: 0x600079E
        private System.Void WaitAndStop() { }
        // RVA: 0x068FF6C4  token: 0x600079F
        public System.Void Stop(System.Boolean noReleaseTime) { }
        // RVA: 0x068FF1F4  token: 0x60007A0
        private System.Double GetCueLengthSec(CriWare.CriAtomExAcb acb, System.String cueName) { }
        // RVA: 0x04D87330  token: 0x60007A1
        public System.Void .ctor() { }
        // RVA: 0x068FF7F4  token: 0x60007A2
        private static System.Void .cctor() { }
        // RVA: 0x068FF6EC  token: 0x60007A3
        private System.Void <WaitAndStop>b__21_0() { }

    }

    // TypeToken: 0x2000120  // size: 0x40
    public class CriAtomClip : CriWare.CriTimeline.Atom.CriAtomClipBase
    {
        // Fields
        public System.String cueSheet;  // 0x30
        public System.String cueName;  // 0x38

        // Properties
        System.String CueName { get; /* RVA: 0x04D85A50 */ }
        System.String AcbPath { get; /* RVA: 0x068FFAA4 */ }
        System.String AwbPath { get; /* RVA: 0x068FFB2C */ }

        // Methods
        // RVA: 0x068FF9A0  token: 0x60007A5
        public virtual CriWare.CriAtomExAcb GetAcb() { }
        // RVA: 0x068FF9AC  token: 0x60007A6
        private CriWare.CriAtomCueSheet GetCueSheet() { }
        // RVA: 0x068FF9B8  token: 0x60007A9
        public virtual System.Void SetCueFromAtomSource(CriWare.CriAtomSourceBase atomSource) { }
        // RVA: 0x068FFA9C  token: 0x60007AA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000121  // size: 0x30
    public abstract class CriAtomClipBase : UnityEngine.Playables.PlayableAsset, UnityEngine.Timeline.ITimelineClipAsset
    {
        // Fields
        public System.Boolean stopWithoutRelease;  // 0x18
        public System.Boolean muted;  // 0x19
        public System.Boolean ignoreBlend;  // 0x1a
        public System.Boolean loopWithinClip;  // 0x1b
        public System.Boolean stopAtClipEnd;  // 0x1c
        public CriWare.CriTimeline.Atom.CriAtomBehaviour templateBehaviour;  // 0x20
        private System.Double clipDuration;  // 0x28

        // Properties
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x04D89170 */ }
        System.Double duration { get; /* RVA: 0x068FF958 */ }
        System.String CueName { get; /* RVA: -1  // abstract */ }
        System.String AcbPath { get; /* RVA: -1  // abstract */ }
        System.String AwbPath { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: 0x068FF830  token: 0x60007AC
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner) { }
        // RVA: 0x04D86D70  token: 0x60007AD
        public System.Void SetClipDuration(System.Double clipDuration) { }
        // RVA: -1  // abstract  token: 0x60007B0
        public virtual CriWare.CriAtomExAcb GetAcb() { }
        // RVA: -1  // abstract  token: 0x60007B3
        public virtual System.Void SetCueFromAtomSource(CriWare.CriAtomSourceBase atomSource) { }
        // RVA: 0x068FF8F4  token: 0x60007B4
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000122  // size: 0x58
    public class CriAtomMixerBehaviour : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        private UnityEngine.Playables.PlayableDirector m_Director;  // 0x10
        private System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TimelineClip> m_Clips;  // 0x18
        private CriWare.CriAtomSourceBase m_Bind;  // 0x20
        private System.String m_AisacControls;  // 0x28
        private System.Boolean m_StopOnWrapping;  // 0x30
        private System.Boolean m_StopAtGraphEnd;  // 0x31
        private System.Guid <m_Guid>k__BackingField;  // 0x34
        private static System.Int32 cScratchTimeIntervalMs;  // const
        private System.DateTime m_lastScrubTime;  // 0x48
        private System.Double m_lastDirectorTime;  // 0x50

        // Properties
        System.Guid m_Guid { get; /* RVA: 0x04D89180 */ set; /* RVA: 0x04D89190 */ }
        System.Boolean IsEditor { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x068FFC2C  token: 0x60007B7
        public virtual System.Void OnPlayableCreate(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x068FFBB4  token: 0x60007B8
        public virtual System.Void OnGraphStop(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x068FFCAC  token: 0x60007B9
        public virtual System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData) { }
        // RVA: 0x041E1670  token: 0x60007BB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000123  // size: 0x38
    public class CriAtomTimelinePreviewer : System.IDisposable
    {
        // Fields
        private static CriWare.CriTimeline.Atom.CriAtomTimelinePreviewer instance;  // static @ 0x0
        private CriWare.CriAtom atom;  // 0x10
        private System.String lastAcfFile;  // 0x18
        private System.Collections.Generic.Dictionary<System.String,CriWare.CriAtomExAcb> acbTable;  // 0x20
        private System.Collections.Generic.Dictionary<System.Guid,CriWare.CriTimeline.Atom.CriAtomTimelinePreviewer.PlayerSource> playerTable;  // 0x28
        private CriWare.CriAtomEx3dListener listener3d;  // 0x30

        // Properties
        CriWare.CriTimeline.Atom.CriAtomTimelinePreviewer Instance { get; /* RVA: 0x069012B0 */ }
        System.Boolean IsInitialized { get; /* RVA: 0x06901340 */ }

        // Methods
        // RVA: 0x06900CB0  token: 0x60007BD
        public static System.Void InstanceDispose() { }
        // RVA: 0x0690115C  token: 0x60007BF
        public System.Void .ctor() { }
        // RVA: 0x06900AD4  token: 0x60007C0
        public CriWare.CriAtomExPlayer GetPlayer(System.Guid trackId) { }
        // RVA: 0x06900DD8  token: 0x60007C1
        public System.Void SetCue(System.Guid trackId, CriWare.CriAtomExAcb acb, System.String cueName) { }
        // RVA: 0x06900714  token: 0x60007C2
        public CriWare.CriAtomExAcb GetAcb(System.String acbPath, System.String awbPath) { }
        // RVA: 0x06900D18  token: 0x60007C3
        public CriWare.CriAtomExPlayback Play(System.Guid trackId) { }
        // RVA: 0x06901108  token: 0x60007C4
        public System.Void StopTrack(System.Guid trackId, System.Boolean stopWithoutRelease) { }
        // RVA: 0x06900FA0  token: 0x60007C5
        public System.Void StopAllTracks(System.Boolean stopWithoutRelease) { }
        // RVA: 0x06900F1C  token: 0x60007C6
        public System.Void SetStartTime(System.Guid trackId, System.Int64 startTimeMs) { }
        // RVA: 0x06900E98  token: 0x60007C7
        public System.Void SetLoop(System.Guid trackId, System.Boolean sw) { }
        // RVA: 0x06900F5C  token: 0x60007C8
        public System.Void SetVolume(System.Guid trackId, System.Single volume) { }
        // RVA: 0x06900ED8  token: 0x60007C9
        public System.Void SetPitch(System.Guid trackId, System.Single pitch) { }
        // RVA: 0x06900D88  token: 0x60007CA
        public System.Void SetAISAC(System.Guid trackId, System.String controlName, System.Single value) { }
        // RVA: 0x06900D4C  token: 0x60007CB
        public System.Void PlayerUpdateParameter(System.Guid trackId, CriWare.CriAtomExPlayback atomExPlayback) { }
        // RVA: 0x069006A0  token: 0x60007CC
        protected virtual System.Void Finalize() { }
        // RVA: 0x069002C8  token: 0x60007CD
        public virtual System.Void Dispose() { }
        // RVA: 0x06900318  token: 0x60007CE
        private System.Void Dispose(System.Boolean disposing) { }

    }

    // TypeToken: 0x2000125  // size: 0xD0
    public class CriAtomTrack : UnityEngine.Timeline.TrackAsset
    {
        // Fields
        public System.String m_AisacControls;  // 0xc0
        public System.Boolean m_StopOnWrapping;  // 0xc8
        public System.Boolean m_StopAtGraphEnd;  // 0xc9

        // Methods
        // RVA: 0x0690137C  token: 0x60007D0
        public virtual UnityEngine.Playables.Playable CreateTrackMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner, System.Int32 inputCount) { }
        // RVA: 0x069017E8  token: 0x60007D1
        public System.Void .ctor() { }

    }

}

namespace CriWare.CriTimeline.Mana
{

    // TypeToken: 0x2000114  // size: 0x10
    public class CriManaBehaviour : UnityEngine.Playables.PlayableBehaviour
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000758
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000115  // size: 0xB8
    public class CriManaClip : CriWare.CriTimeline.Mana.CriManaClipBase
    {
        // Fields
        public System.String m_moviePath;  // 0xa8
        public UnityEngine.TextAsset m_movieData;  // 0xb0

        // Properties
        System.String MoviePath { get; /* RVA: 0x04D86290 */ }
        System.Byte[] MovieData { get; /* RVA: 0x068F87A0 */ }
        System.String MovieName { get; /* RVA: 0x068F8818 */ }
        System.Int32 DataId { get; /* RVA: 0x068F86F8 */ }

        // Methods
        // RVA: 0x068F86A8  token: 0x600075D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000116  // size: 0xA8
    public abstract class CriManaClipBase : UnityEngine.Playables.PlayableAsset, UnityEngine.Timeline.ITimelineClipAsset
    {
        // Fields
        public readonly System.Guid guid;  // 0x18
        public System.Boolean m_loopWithinClip;  // 0x28
        public System.Boolean m_useOnMemoryPlayback;  // 0x29
        public System.Runtime.InteropServices.GCHandle gcHandle;  // 0x30
        private System.Double m_movieFrameRate;  // 0x38
        private System.Double m_clipDuration;  // 0x40
        public System.Single m_fadeinDuration;  // 0x48
        public UnityEngine.AnimationCurve m_fadeinCurve;  // 0x50
        public System.Single m_fadeoutDuration;  // 0x58
        public UnityEngine.AnimationCurve m_fadeoutCurve;  // 0x60
        public System.Boolean m_fadeAudio;  // 0x68
        public UnityEngine.Timeline.TimelineClip m_clip;  // 0x70
        public CriWare.CriTimeline.Mana.CriManaBehaviour m_manaBehaviour;  // 0x78
        private System.Nullable<CriWare.CriTimeline.Mana.CriManaClipBase.MovieInfoStruct> m_movieInfoStruct;  // 0x80

        // Properties
        System.String MoviePath { get; /* RVA: -1  // abstract */ }
        System.Byte[] MovieData { get; /* RVA: -1  // abstract */ }
        System.String MovieName { get; /* RVA: -1  // abstract */ }
        System.Int32 DataId { get; /* RVA: -1  // abstract */ }
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x04D85B80 */ }
        System.Boolean IsMovieInfoReady { get; /* RVA: 0x068F8650 */ }
        System.Double duration { get; /* RVA: 0x068F868C */ }

        // Methods
        // RVA: 0x068F84B4  token: 0x6000762
        private System.Nullable<CriWare.CriTimeline.Mana.CriManaClipBase.MovieInfoStruct> StructToMovieInfo(CriWare.CriMana.MovieInfo movieInfo) { }
        // RVA: 0x068F809C  token: 0x6000764
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner) { }
        // RVA: 0x068F8308  token: 0x6000765
        public System.Void ReplaceMovieInfo(CriWare.CriMana.MovieInfo movieInfo) { }
        // RVA: 0x068F822C  token: 0x6000766
        public System.Boolean IsSameMovie(CriWare.CriMana.MovieInfo movieInfo) { }
        // RVA: 0x068F8160  token: 0x6000768
        public System.Int32 GetSeekFrame(System.Double seekTimeSec, System.Boolean loop) { }
        // RVA: 0x068F858C  token: 0x600076A
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000118  // size: 0x88
    public class CriManaMixerBehaviour : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        private UnityEngine.Playables.PlayableDirector m_PlayableDirector;  // 0x10
        private System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TimelineClip> m_clips;  // 0x18
        private CriWare.CriManaMovieMaterialBase m_boundMovieMaterial;  // 0x20
        private System.Collections.Generic.Dictionary<System.Int32,System.Runtime.InteropServices.GCHandle> m_gcHandleList;  // 0x28
        private System.Boolean m_frameSync;  // 0x30
        private static System.Double cPreloadTimeSec;  // static @ 0x0
        private System.Nullable<System.Guid> m_lastClipId;  // 0x34
        private System.Double startTime;  // 0x48
        private System.Double m_currentSeekingFrameTime;  // 0x50
        private System.Single m_originalAudioVolume;  // 0x58
        private System.Single m_originalSubAudioVolume;  // 0x5c
        private System.Single m_originalExtraAudioVolume;  // 0x60
        private System.Boolean enableTimelineScrubPlayback;  // 0x64
        public System.Boolean IsControllingUI;  // 0x65
        private CriWare.CriManaMovieControllerForUI m_controllerForUI;  // 0x68
        public System.Action<CriWare.CriMana.MovieInfo> MovieReadyToPlay;  // 0x70
        public System.Action<System.Boolean> OnToggleFMVNode;  // 0x78
        private CriWare.CriTimeline.Mana.CriManaMixerBehaviour.MovieMixerState m_movieMixerState;  // 0x80

        // Properties
        System.Boolean IsEditMode { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x068F8BA4  token: 0x600076C
        private System.Void KeepAudioVolume(System.Boolean fadeAudio) { }
        // RVA: 0x068F9228  token: 0x600076D
        private System.Boolean PlayMovie(CriWare.CriTimeline.Mana.CriManaClipBase clipAsset, System.Int32 startFrame, System.Double startTime) { }
        // RVA: 0x068F95EC  token: 0x600076E
        private System.Boolean PrepareMovie(CriWare.CriTimeline.Mana.CriManaClipBase clipAsset) { }
        // RVA: 0x068FAD60  token: 0x600076F
        private System.Boolean StopMovie() { }
        // RVA: 0x068FACE4  token: 0x6000770
        private System.Boolean StopForSeekMovie() { }
        // RVA: 0x068F8B08  token: 0x6000771
        private static System.Boolean IsPlayerPreparing(CriWare.CriMana.Player player) { }
        // RVA: 0x068F8B84  token: 0x6000772
        private static System.Boolean IsPlayerStopped(CriWare.CriMana.Player player) { }
        // RVA: 0x068F8AE4  token: 0x6000773
        private static System.Boolean IsPlayerError(CriWare.CriMana.Player player) { }
        // RVA: 0x068F8B38  token: 0x6000774
        private static System.Boolean IsPlayerReadyOrPlaying(CriWare.CriMana.Player player) { }
        // RVA: 0x068F9844  token: 0x6000775
        private System.Void ProcessFrameOnSeeking(UnityEngine.Timeline.TimelineClip activeClip, CriWare.CriTimeline.Mana.CriManaClipBase clip, System.Double frameTime) { }
        // RVA: 0x068F88B4  token: 0x6000776
        private System.Void ForceSyncedStop(System.Boolean keepLastFrame) { }
        // RVA: 0x068FAE68  token: 0x6000777
        private System.Void ToggleFMVNode(UnityEngine.Playables.FrameData info, System.Boolean isOn, System.Single weight) { }
        // RVA: 0x068F9920  token: 0x6000778
        public virtual System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData) { }
        // RVA: 0x068FB054  token: 0x6000779
        private System.Collections.IEnumerator seekPlaybackEnumerator() { }
        // RVA: 0x068F8C6C  token: 0x600077A
        public virtual System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x068F8C60  token: 0x600077B
        public virtual System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x068F91A0  token: 0x600077C
        private System.Void PausePlayer(System.Boolean pause) { }
        // RVA: 0x068F8C78  token: 0x600077D
        public virtual System.Void OnGraphStart(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x068F8F54  token: 0x600077E
        public virtual System.Void OnGraphStop(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x068F90BC  token: 0x600077F
        public virtual System.Void OnPlayableCreate(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x068F9154  token: 0x6000780
        public virtual System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x068F8964  token: 0x6000781
        public System.Void InitMovieMixerState() { }
        // RVA: 0x04D89010  token: 0x6000782
        public System.Void .ctor() { }
        // RVA: 0x068FB014  token: 0x6000783
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200011D  // size: 0xE0
    public class CriManaTrack : UnityEngine.Timeline.TrackAsset
    {
        // Fields
        public System.Boolean frameSync;  // 0xc0
        public readonly System.Guid guid;  // 0xc4
        private static System.Collections.Generic.Dictionary<System.Int32,System.Guid> bindDict;  // static @ 0x0
        private CriWare.CriTimeline.Mana.CriManaMixerBehaviour <MixerBehaviour>k__BackingField;  // 0xd8

        // Properties
        CriWare.CriTimeline.Mana.CriManaMixerBehaviour MixerBehaviour { get; /* RVA: 0x04D88EF0 */ set; /* RVA: 0x06902698 */ }

        // Methods
        // RVA: 0x06901CB0  token: 0x600078E
        public virtual UnityEngine.Playables.Playable CreateTrackMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount) { }
        // RVA: 0x0690183C  token: 0x600078F
        public System.Void BindBehaviourMovieMaterial(UnityEngine.Playables.PlayableDirector director, CriWare.CriManaMovieMaterialBase boundMovieMaterial) { }
        // RVA: 0x0690212C  token: 0x6000790
        private System.Void OnDestroy() { }
        // RVA: 0x06902180  token: 0x6000791
        private static System.Void RemoveTrackFromBindDict(CriWare.CriTimeline.Mana.CriManaTrack trackAsset) { }
        // RVA: 0x06902638  token: 0x6000792
        public System.Void .ctor() { }
        // RVA: 0x069025AC  token: 0x6000793
        private static System.Void .cctor() { }

    }

}

namespace HG.Rendering.Runtime
{

    // TypeToken: 0x2000002  // size: 0x10
    public class HGConstantBufferLayoutAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x2000148  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x6000903
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x069028A0  token: 0x6000904
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000149  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x06902948  token: 0x6000905
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x06902904  token: 0x6000906
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x200014A  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000907
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0690A33C  token: 0x6000908
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x0690A42C  token: 0x6000909
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x0690A1F8  token: 0x600090A
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x0690A2C4  token: 0x600090B
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x0690A3A0  token: 0x600090C
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x0690A154  token: 0x600090D
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

}

