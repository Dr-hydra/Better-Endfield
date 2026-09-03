// ========================================================
// Dumped by @desirepro
// Assembly: Slate.dll
// Classes:  273
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000004  // size: 0x80
    public sealed class SubtitlesGUIDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0AC66EE4  token: 0x6000049
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x03A1B910  token: 0x600004A
        public virtual System.Void Invoke(System.String text, UnityEngine.Color color) { }
        // RVA: 0x0AC66E5C  token: 0x600004B
        public virtual System.IAsyncResult BeginInvoke(System.String text, UnityEngine.Color color, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x600004C
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000005  // size: 0x80
    public sealed class TextOverlayGUIDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0AC670F4  token: 0x600004D
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0AC670AC  token: 0x600004E
        public virtual System.Void Invoke(System.String text, UnityEngine.Color color, System.Single size, UnityEngine.TextAnchor alignment, UnityEngine.Vector2 position) { }
        // RVA: 0x0AC66FB8  token: 0x600004F
        public virtual System.IAsyncResult BeginInvoke(System.String text, UnityEngine.Color color, System.Single size, UnityEngine.TextAnchor alignment, UnityEngine.Vector2 position, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000050
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000006  // size: 0x80
    public sealed class ImageOverlayGUIDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0AC659E8  token: 0x6000051
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0AC659B0  token: 0x6000052
        public virtual System.Void Invoke(UnityEngine.Texture texture, UnityEngine.Color color, UnityEngine.Vector2 scale, UnityEngine.Vector2 position) { }
        // RVA: 0x0AC658F0  token: 0x6000053
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Texture texture, UnityEngine.Color color, UnityEngine.Vector2 scale, UnityEngine.Vector2 position, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000054
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000007  // size: 0x80
    public sealed class ScreenFadeGUIDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x07C41054  token: 0x6000055
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B0B020  token: 0x6000056
        public virtual System.Void Invoke(UnityEngine.Color color) { }
        // RVA: 0x0AC661D4  token: 0x6000057
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Color color, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000058
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000008  // size: 0x80
    public sealed class LetterboxGUIDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0932FEB0  token: 0x6000059
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x600005A
        public virtual System.Void Invoke(System.Single completion) { }
        // RVA: 0x0AC65B10  token: 0x600005B
        public virtual System.IAsyncResult BeginInvoke(System.Single completion, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x600005C
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000009  // size: 0x80
    public sealed class CameraDissolveDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0AC5A8A0  token: 0x600005D
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x600005E
        public virtual System.Void Invoke(UnityEngine.Texture texture, System.Single completion) { }
        // RVA: 0x0AC5A820  token: 0x600005F
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Texture texture, System.Single completion, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000060
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200000C  // size: 0x14
    public sealed struct TrackingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.DynamicCameraController.Transposer.TrackingMode None;  // const
        public static Slate.DynamicCameraController.Transposer.TrackingMode OffsetTracking;  // const
        public static Slate.DynamicCameraController.Transposer.TrackingMode RailTracking;  // const

    }

    // TypeToken: 0x200000D  // size: 0x14
    public sealed struct OffsetMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.DynamicCameraController.Transposer.OffsetMode LocalSpace;  // const
        public static Slate.DynamicCameraController.Transposer.OffsetMode WorldSpace;  // const

    }

    // TypeToken: 0x200000B  // size: 0x50
    public class Transposer
    {
        // Fields
        public Slate.DynamicCameraController.Transposer.TrackingMode trackingMode;  // 0x10
        public UnityEngine.Transform target;  // 0x18
        public UnityEngine.Vector3 targetOffset;  // 0x20
        public Slate.DynamicCameraController.Transposer.OffsetMode offsetMode;  // 0x2c
        public UnityEngine.Vector3 railStart;  // 0x30
        public UnityEngine.Vector3 railEnd;  // 0x3c
        public System.Single railOffset;  // 0x48
        public System.Single smoothDamping;  // 0x4c

        // Methods
        // RVA: 0x04DA9C30  token: 0x600006A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000F  // size: 0x14
    public sealed struct TrackingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.DynamicCameraController.Composer.TrackingMode None;  // const
        public static Slate.DynamicCameraController.Composer.TrackingMode FrameComposition;  // const

    }

    // TypeToken: 0x200000E  // size: 0x50
    public class Composer
    {
        // Fields
        public Slate.DynamicCameraController.Composer.TrackingMode trackingMode;  // 0x10
        public UnityEngine.Transform target;  // 0x18
        public UnityEngine.Vector3 targetOffset;  // 0x20
        public System.Single targetSize;  // 0x2c
        public UnityEngine.Vector2 frameCenter;  // 0x30
        public UnityEngine.Vector2 frameExtends;  // 0x38
        public System.Single dutchTilt;  // 0x40
        public System.Boolean zoomAtTargetFrame;  // 0x44
        public System.Single smoothDamping;  // 0x48

        // Methods
        // RVA: 0x04DA9B90  token: 0x600006B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000013  // size: 0x18
    public sealed class <>c__DisplayClass47_0
    {
        // Fields
        public System.String shotName;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000A8
        public System.Void .ctor() { }
        // RVA: 0x0AC6738C  token: 0x60000A9
        private System.Boolean <Find>b__0(Slate.ShotCamera s) { }

    }

    // TypeToken: 0x2000017  // size: 0x18
    public sealed class <>c__DisplayClass19_0
    {
        // Fields
        public System.String name;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000CC
        public System.Void .ctor() { }
        // RVA: 0x0AC672D4  token: 0x60000CD
        private System.Boolean <FindExpressionByName>b__0(Slate.BlendShapeGroup x) { }

    }

    // TypeToken: 0x2000018  // size: 0x18
    public sealed class <>c__DisplayClass20_0
    {
        // Fields
        public System.String UID;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000CE
        public System.Void .ctor() { }
        // RVA: 0x0AC672F0  token: 0x60000CF
        private System.Boolean <FindExpressionByUID>b__0(Slate.BlendShapeGroup x) { }

    }

    // TypeToken: 0x2000037  // size: 0x14
    public sealed struct BlendInEffectType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.CameraShot.BlendInEffectType None;  // const
        public static Slate.CameraShot.BlendInEffectType FadeFromColor;  // const
        public static Slate.CameraShot.BlendInEffectType CrossDissolve;  // const
        public static Slate.CameraShot.BlendInEffectType EaseIn;  // const

    }

    // TypeToken: 0x2000038  // size: 0x14
    public sealed struct BlendOutEffectType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.CameraShot.BlendOutEffectType None;  // const
        public static Slate.CameraShot.BlendOutEffectType FadeToColor;  // const

    }

    // TypeToken: 0x2000039  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Slate.CameraShot.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x0AC67428  token: 0x6000115
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000116
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000043  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Slate.AnimatorTrack.<>c <>9;  // static @ 0x0
        public static System.Func<Slate.AnimatorTrack,System.Boolean> <>9__36_0;  // static @ 0x8

        // Methods
        // RVA: 0x0AC6748C  token: 0x6000177
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000178
        public System.Void .ctor() { }
        // RVA: 0x0AC671C8  token: 0x6000179
        private System.Boolean <OnInitialize>b__36_0(Slate.AnimatorTrack t) { }

    }

    // TypeToken: 0x200004E  // size: 0x50
    public class SerializationMetaData
    {
        // Fields
        public System.String parameterName;  // 0x10
        public System.String declaringTypeName;  // 0x18
        public System.String transformHierarchyPath;  // 0x20
        public Slate.AnimatedParameter.ParameterType parameterType;  // 0x28
        private System.Type <declaringType>k__BackingField;  // 0x30
        private System.Reflection.PropertyInfo <property>k__BackingField;  // 0x38
        private System.Reflection.FieldInfo <field>k__BackingField;  // 0x40
        private System.Type <animatedType>k__BackingField;  // 0x48

        // Properties
        System.Type declaringType { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        System.Reflection.PropertyInfo property { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        System.Reflection.FieldInfo field { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        System.Type animatedType { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }

        // Methods
        // RVA: 0x0AC66254  token: 0x6000272
        public System.Void Deserialize() { }
        // RVA: 0x041E1670  token: 0x6000273
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x14
    public sealed struct ParameterType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.AnimatedParameter.ParameterType NotSet;  // const
        public static Slate.AnimatedParameter.ParameterType Property;  // const
        public static Slate.AnimatedParameter.ParameterType Field;  // const

    }

    // TypeToken: 0x2000050  // size: 0x18
    public sealed class <>c__DisplayClass112_0
    {
        // Fields
        public System.Single time;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000274
        public System.Void .ctor() { }
        // RVA: 0x0AC67228  token: 0x6000275
        private System.Single <GetKeyLabel>b__0(UnityEngine.AnimationCurve c) { }

    }

    // TypeToken: 0x200005D  // size: 0x80
    public sealed class AddParameterDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0AC5111C  token: 0x60002E3
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x60002E4
        public virtual System.Boolean Invoke(System.Type type, System.String memberPath, System.String transformPath) { }
        // RVA: 0x05FD2238  token: 0x60002E5
        public virtual System.IAsyncResult BeginInvoke(System.Type type, System.String memberPath, System.String transformPath, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE668  token: 0x60002E6
        public virtual System.Boolean EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200005E  // size: 0x18
    public sealed class <>c__DisplayClass12_0
    {
        // Fields
        public Slate.AnimatedParameter newParam;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60002E7
        public System.Void .ctor() { }
        // RVA: 0x0AC6724C  token: 0x60002E8
        private System.Boolean <TryAddParameter>b__0(Slate.AnimatedParameter p) { }

    }

    // TypeToken: 0x200005F  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Slate.AnimationDataCollection.<>c <>9;  // static @ 0x0
        public static System.Func<Slate.AnimatedParameter,System.String> <>9__14_0;  // static @ 0x8
        public static System.Func<Slate.AnimatedParameter,System.String> <>9__14_1;  // static @ 0x10
        public static System.Func<System.Single,System.Single> <>9__31_1;  // static @ 0x18
        public static System.Func<System.Single,System.Single> <>9__32_1;  // static @ 0x20

        // Methods
        // RVA: 0x0AC673C4  token: 0x60002E9
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60002EA
        public System.Void .ctor() { }
        // RVA: 0x0AC671E8  token: 0x60002EB
        private System.String <ReOrderParameters>b__14_0(Slate.AnimatedParameter p) { }
        // RVA: 0x0AC67208  token: 0x60002EC
        private System.String <ReOrderParameters>b__14_1(Slate.AnimatedParameter p) { }
        // RVA: 0x04D9A920  token: 0x60002ED
        private System.Single <GetKeyNext>b__31_1(System.Single t) { }
        // RVA: 0x04D9A920  token: 0x60002EE
        private System.Single <GetKeyPrevious>b__32_1(System.Single t) { }

    }

    // TypeToken: 0x2000060  // size: 0x18
    public sealed class <>c__DisplayClass15_0
    {
        // Fields
        public System.String name;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60002EF
        public System.Void .ctor() { }
        // RVA: 0x0AC67274  token: 0x60002F0
        private System.Boolean <GetParameterOfName>b__0(Slate.AnimatedParameter d) { }

    }

    // TypeToken: 0x2000061  // size: 0x18
    public sealed class <>c__DisplayClass31_0
    {
        // Fields
        public System.Single time;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60002F1
        public System.Void .ctor() { }
        // RVA: 0x0AC6730C  token: 0x60002F2
        private System.Single <GetKeyNext>b__0(Slate.AnimatedParameter p) { }
        // RVA: 0x0AC67330  token: 0x60002F3
        private System.Boolean <GetKeyNext>b__2(System.Single t) { }

    }

    // TypeToken: 0x2000062  // size: 0x18
    public sealed class <>c__DisplayClass32_0
    {
        // Fields
        public System.Single time;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60002F4
        public System.Void .ctor() { }
        // RVA: 0x0AC67338  token: 0x60002F5
        private System.Single <GetKeyPrevious>b__0(Slate.AnimatedParameter p) { }
        // RVA: 0x0AC6735C  token: 0x60002F6
        private System.Boolean <GetKeyPrevious>b__2(System.Single t) { }

    }

    // TypeToken: 0x2000063  // size: 0x18
    public sealed class <>c__DisplayClass33_0
    {
        // Fields
        public System.Single time;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60002F7
        public System.Void .ctor() { }
        // RVA: 0x0AC67368  token: 0x60002F8
        private System.Boolean <GetKeyLabel>b__0(Slate.AnimatedParameter p) { }

    }

    // TypeToken: 0x2000065  // size: 0x14
    public sealed struct WrapMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.Cutscene.WrapMode Once;  // const
        public static Slate.Cutscene.WrapMode Loop;  // const
        public static Slate.Cutscene.WrapMode PingPong;  // const

    }

    // TypeToken: 0x2000066  // size: 0x14
    public sealed struct StopMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.Cutscene.StopMode Skip;  // const
        public static Slate.Cutscene.StopMode Rewind;  // const
        public static Slate.Cutscene.StopMode Hold;  // const
        public static Slate.Cutscene.StopMode SkipRewindNoUndo;  // const

    }

    // TypeToken: 0x2000067  // size: 0x14
    public sealed struct UpdateMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.Cutscene.UpdateMode Normal;  // const
        public static Slate.Cutscene.UpdateMode AnimatePhysics;  // const
        public static Slate.Cutscene.UpdateMode UnscaledTime;  // const
        public static Slate.Cutscene.UpdateMode Manual;  // const

    }

    // TypeToken: 0x2000068  // size: 0x14
    public sealed struct PlayingDirection
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.Cutscene.PlayingDirection Forwards;  // const
        public static Slate.Cutscene.PlayingDirection Backwards;  // const

    }

    // TypeToken: 0x2000069  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Slate.Cutscene.<>c <>9;  // static @ 0x0
        public static System.Predicate<Slate.CutsceneGroup> <>9__62_0;  // static @ 0x8
        public static System.Predicate<Slate.CutsceneGroup> <>9__64_0;  // static @ 0x10
        public static System.Predicate<Slate.CutsceneTrack> <>9__74_0;  // static @ 0x18
        public static System.Func<Slate.ActorGroup,UnityEngine.GameObject> <>9__135_0;  // static @ 0x20
        public static System.Func<Slate.IDirectableTimePointer,System.Single> <>9__136_0;  // static @ 0x28
        public static System.Comparison<Slate.IDirectableTimePointer> <>9__170_0;  // static @ 0x30
        public static System.Func<Slate.Section,System.String> <>9__190_0;  // static @ 0x38
        public static System.Func<Slate.IEvent,System.String> <>9__191_0;  // static @ 0x40

        // Methods
        // RVA: 0x0AC79E58  token: 0x600037A
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600037B
        public System.Void .ctor() { }
        // RVA: 0x0AC79954  token: 0x600037C
        private System.Boolean <get_directorGroup>b__62_0(Slate.CutsceneGroup g) { }
        // RVA: 0x0AC798D4  token: 0x600037D
        private System.Boolean <get_actorGroup>b__64_0(Slate.CutsceneGroup g) { }
        // RVA: 0x0AC79914  token: 0x600037E
        private System.Boolean <get_cameraTrack>b__74_0(Slate.CutsceneTrack t) { }
        // RVA: 0x0AC796DC  token: 0x600037F
        private UnityEngine.GameObject <GetAffectedActors>b__135_0(Slate.ActorGroup g) { }
        // RVA: 0x0AC79738  token: 0x6000380
        private System.Single <GetPointerTimes>b__136_0(Slate.IDirectableTimePointer t) { }
        // RVA: 0x0AC79754  token: 0x6000381
        private System.Int32 <InitializeTimePointers>b__170_0(Slate.IDirectableTimePointer a, Slate.IDirectableTimePointer b) { }
        // RVA: 0x06C08908  token: 0x6000382
        private System.String <GetSectionNames>b__190_0(Slate.Section s) { }
        // RVA: 0x0AC796FC  token: 0x6000383
        private System.String <GetDefinedEventNames>b__191_0(Slate.IEvent d) { }

    }

    // TypeToken: 0x200006A  // size: 0x20
    public sealed class <>c__DisplayClass177_0
    {
        // Fields
        public System.Action callback;  // 0x10
        public Slate.Cutscene instance;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000384
        public System.Void .ctor() { }
        // RVA: 0x0AC79AF0  token: 0x6000385
        private System.Void <Play>b__0() { }

    }

    // TypeToken: 0x200006B  // size: 0x18
    public sealed class <>c__DisplayClass182_0
    {
        // Fields
        public System.String groupName;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000386
        public System.Void .ctor() { }
        // RVA: 0x0AC79B98  token: 0x6000387
        private System.Boolean <SetGroupActorOfName>b__0(Slate.ActorGroup g) { }

    }

    // TypeToken: 0x200006C  // size: 0x18
    public sealed class <>c__DisplayClass184_0
    {
        // Fields
        public System.String[] split;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000388
        public System.Void .ctor() { }
        // RVA: 0x0AC79BF4  token: 0x6000389
        private System.Boolean <FindElement>b__0(Slate.CutsceneGroup g) { }

    }

    // TypeToken: 0x200006D  // size: 0x50
    public sealed class <Internal_RenderCutscene>d__194 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Int32 frameRate;  // 0x20
        public Slate.Cutscene <>4__this;  // 0x28
        public System.Int32 width;  // 0x30
        public System.Int32 height;  // 0x34
        public System.Action<UnityEngine.Texture2D[]> callback;  // 0x38
        private System.Collections.Generic.List<UnityEngine.Texture2D> <renderSequence>5__2;  // 0x40
        private System.Single <sampleRate>5__3;  // 0x48
        private System.Single <i>5__4;  // 0x4c

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600038A
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600038B
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0AC79450  token: 0x600038C
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0AC79690  token: 0x600038E
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200006F  // size: 0x14
    public sealed struct ActorReferenceMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.CutsceneGroup.ActorReferenceMode UseOriginal;  // const
        public static Slate.CutsceneGroup.ActorReferenceMode UseInstanceHideOriginal;  // const

    }

    // TypeToken: 0x2000070  // size: 0x14
    public sealed struct ActorInitialTransformation
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.CutsceneGroup.ActorInitialTransformation UseOriginal;  // const
        public static Slate.CutsceneGroup.ActorInitialTransformation UseLocal;  // const

    }

    // TypeToken: 0x2000071  // size: 0x30
    public class CustomToggleInfo
    {
        // Fields
        public System.Int32 startIndex;  // 0x10
        public System.Int32 endIndex;  // 0x14
        public System.String desc;  // 0x18
        public System.Boolean isCollapsed;  // 0x20
        private System.Int32 <index>k__BackingField;  // 0x24
        private Slate.CutsceneGroup <group>k__BackingField;  // 0x28

        // Properties
        System.Int32 index { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        Slate.CutsceneGroup group { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }

        // Methods
        // RVA: 0x0AC6C4A4  token: 0x60003D8
        private System.Void _OnToggleChanged() { }
        // RVA: 0x0AC6C354  token: 0x60003D9
        public System.Collections.IEnumerable _GetAllTrackNames() { }
        // RVA: 0x04D9E080  token: 0x60003DA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000072  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Slate.CutsceneGroup.<>c <>9;  // static @ 0x0
        public static System.Func<Slate.CutsceneGroup.CustomToggleInfo,System.Int32> <>9__48_0;  // static @ 0x8
        public static System.Func<Slate.CutsceneTrack,System.Boolean> <>9__88_0;  // static @ 0x10
        public static System.Func<Slate.Section,System.Single> <>9__88_1;  // static @ 0x18

        // Methods
        // RVA: 0x0AC79F20  token: 0x60003DB
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60003DC
        public System.Void .ctor() { }
        // RVA: 0x0AA421AC  token: 0x60003DD
        private System.Int32 <_OnCustomToggleValueChanged>b__48_0(Slate.CutsceneGroup.CustomToggleInfo t) { }
        // RVA: 0x0AC7986C  token: 0x60003DE
        private System.Boolean <Validate>b__88_0(Slate.CutsceneTrack t) { }
        // RVA: 0x0AC798B8  token: 0x60003DF
        private System.Single <Validate>b__88_1(Slate.Section s) { }

    }

    // TypeToken: 0x2000073  // size: 0x18
    public sealed class <>c__DisplayClass89_0
    {
        // Fields
        public System.String name;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003E0
        public System.Void .ctor() { }
        // RVA: 0x0AC79CC0  token: 0x60003E1
        private System.Boolean <GetSectionByName>b__0(Slate.Section s) { }

    }

    // TypeToken: 0x2000074  // size: 0x18
    public sealed class <>c__DisplayClass90_0
    {
        // Fields
        public System.String UID;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003E2
        public System.Void .ctor() { }
        // RVA: 0x06CC7538  token: 0x60003E3
        private System.Boolean <GetSectionByUID>b__0(Slate.Section s) { }

    }

    // TypeToken: 0x2000075  // size: 0x18
    public sealed class <>c__DisplayClass91_0
    {
        // Fields
        public System.Single time;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003E4
        public System.Void .ctor() { }
        // RVA: 0x0AC79D88  token: 0x60003E5
        private System.Boolean <GetSectionAfter>b__0(Slate.Section s) { }

    }

    // TypeToken: 0x2000076  // size: 0x18
    public sealed class <>c__DisplayClass92_0
    {
        // Fields
        public System.Single time;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003E6
        public System.Void .ctor() { }
        // RVA: 0x0AC79DD4  token: 0x60003E7
        private System.Boolean <GetSectionBefore>b__0(Slate.Section s) { }

    }

    // TypeToken: 0x2000078  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Slate.CutsceneTrack.<>c <>9;  // static @ 0x0
        public static System.Func<Slate.ActionClip,System.Single> <>9__72_0;  // static @ 0x8

        // Methods
        // RVA: 0x0AC79EBC  token: 0x600042A
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600042B
        public System.Void .ctor() { }
        // RVA: 0x0AC79850  token: 0x600042C
        private System.Single <Validate>b__72_0(Slate.ActionClip a) { }

    }

    // TypeToken: 0x2000079  // size: 0x18
    public sealed class <>c__DisplayClass92_0
    {
        // Fields
        public Slate.ActionClip newAction;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600042D
        public System.Void .ctor() { }
        // RVA: 0x0AC79DA8  token: 0x600042E
        private System.Boolean <AddAction>b__0(Slate.ActionClip a) { }

    }

    // TypeToken: 0x200007F  // size: 0x18
    public sealed class <>c__DisplayClass12_0
    {
        // Fields
        public System.String name;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000483
        public System.Void .ctor() { }
        // RVA: 0x0AC799F4  token: 0x6000484
        private System.Boolean <FindChild>b__0(Slate.IDirectable d) { }

    }

    // TypeToken: 0x2000080  // size: 0x18
    public sealed class <>c__DisplayClass14_0
    {
        // Fields
        public Slate.IDirectable directable;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000485
        public System.Void .ctor() { }
        // RVA: 0x0AC79A50  token: 0x6000486
        private System.Boolean <GetPreviousSibling>b__0(Slate.IDirectable d) { }

    }

    // TypeToken: 0x2000081  // size: 0x18
    public sealed class <>c__DisplayClass16_0
    {
        // Fields
        public Slate.IDirectable directable;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000487
        public System.Void .ctor() { }
        // RVA: 0x0AC79AA0  token: 0x6000488
        private System.Boolean <GetNextSibling>b__0(Slate.IDirectable d) { }

    }

    // TypeToken: 0x2000084  // size: 0x14
    public sealed struct ExitMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.Section.ExitMode Continue;  // const
        public static Slate.Section.ExitMode Loop;  // const

    }

    // TypeToken: 0x200008A  // size: 0x14
    public sealed struct HandleStyle
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.BezierPoint.HandleStyle Connected;  // const
        public static Slate.BezierPoint.HandleStyle Broken;  // const

    }

    // TypeToken: 0x200008F  // size: 0x24
    public sealed struct SampleSettings
    {
        // Fields
        public System.Single volume;  // 0x10
        public System.Single pitch;  // 0x14
        public System.Single pan;  // 0x18
        public System.Single spatialBlend;  // 0x1c
        public System.Boolean ignoreTimescale;  // 0x20
        public System.Boolean bypassReverb;  // 0x21

        // Methods
        // RVA: 0x04DA9CC0  token: 0x60004FB
        public static Slate.AudioSampler.SampleSettings Default() { }

    }

    // TypeToken: 0x2000093  // size: 0x18
    public sealed class <>c__DisplayClass1_0
    {
        // Fields
        public System.Single time;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000506
        public System.Void .ctor() { }
        // RVA: 0x0AC79C68  token: 0x6000507
        private System.Boolean <GetKeyNext>b__1(UnityEngine.Keyframe k) { }

    }

    // TypeToken: 0x2000094  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Slate.CurveUtility.<>c <>9;  // static @ 0x0
        public static System.Func<UnityEngine.Keyframe,System.Single> <>9__1_0;  // static @ 0x8
        public static System.Func<UnityEngine.Keyframe,System.Single> <>9__2_0;  // static @ 0x10

        // Methods
        // RVA: 0x0AC79DF4  token: 0x6000508
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000509
        public System.Void .ctor() { }
        // RVA: 0x04DA9D30  token: 0x600050A
        private System.Single <GetKeyNext>b__1_0(UnityEngine.Keyframe k) { }
        // RVA: 0x04DA9D30  token: 0x600050B
        private System.Single <GetKeyPrevious>b__2_0(UnityEngine.Keyframe k) { }

    }

    // TypeToken: 0x2000095  // size: 0x18
    public sealed class <>c__DisplayClass2_0
    {
        // Fields
        public System.Single time;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600050C
        public System.Void .ctor() { }
        // RVA: 0x0AC79C84  token: 0x600050D
        private System.Boolean <GetKeyPrevious>b__1(UnityEngine.Keyframe k) { }

    }

    // TypeToken: 0x2000096  // size: 0x20
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.Single time;  // 0x10
        public System.Func<UnityEngine.Keyframe,System.Boolean> <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600050E
        public System.Void .ctor() { }
        // RVA: 0x0AC79C9C  token: 0x600050F
        private System.Boolean <HasKey>b__0(UnityEngine.Keyframe k) { }

    }

    // TypeToken: 0x20000A0  // size: 0x20
    public sealed class <>c__DisplayClass8_0
    {
        // Fields
        public System.Type type;  // 0x10
        public System.Func<System.Type,System.Boolean> <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600056D
        public System.Void .ctor() { }
        // RVA: 0x0AC79D14  token: 0x600056E
        private System.Boolean <GetImplementationsOf>b__0(System.Type t) { }

    }

    // TypeToken: 0x20000A1
    public sealed class <>c__DisplayClass28_0`2
    {
        // Fields
        public System.Reflection.MemberInfo info;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600056F
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000570
        private System.Void <GetFieldOrPropSetter>b__0(T x, TValue v) { }

    }

    // TypeToken: 0x20000A6  // size: 0x14
    public sealed struct StoreMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.TransformSnapshot.StoreMode All;  // const
        public static Slate.TransformSnapshot.StoreMode RootOnly;  // const
        public static Slate.TransformSnapshot.StoreMode ChildrenOnly;  // const

    }

    // TypeToken: 0x20000A7  // size: 0x48
    public sealed struct TransformData
    {
        // Fields
        public UnityEngine.Transform transform;  // 0x10
        public UnityEngine.Transform parent;  // 0x18
        public UnityEngine.Vector3 pos;  // 0x20
        public UnityEngine.Quaternion rot;  // 0x2c
        public UnityEngine.Vector3 scale;  // 0x3c

        // Methods
        // RVA: 0x0AC788AC  token: 0x600058C
        public System.Void .ctor(UnityEngine.Transform transform, UnityEngine.Transform parent, UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 scale) { }

    }

    // TypeToken: 0x20000A9  // size: 0x18
    public sealed class <>c__DisplayClass10_0
    {
        // Fields
        public System.String name;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600059B
        public System.Void .ctor() { }
        // RVA: 0x0AC79994  token: 0x600059C
        private System.Boolean <FindInChildren>b__0(UnityEngine.Transform t) { }

    }

    // TypeToken: 0x20000AB  // size: 0x14
    public sealed struct VideoRenderTarget
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.VideoSampler.VideoRenderTarget CameraBackground;  // const
        public static Slate.VideoSampler.VideoRenderTarget CameraForeground;  // const

    }

    // TypeToken: 0x20000AC  // size: 0x24
    public sealed struct SampleSettings
    {
        // Fields
        public Slate.VideoSampler.VideoRenderTarget renderTarget;  // 0x10
        public System.Single playbackSpeed;  // 0x14
        public System.Single alpha;  // 0x18
        public UnityEngine.Video.VideoAspectRatio aspectRatio;  // 0x1c
        public System.Single audioVolume;  // 0x20

        // Methods
        // RVA: 0x04DA9CE0  token: 0x60005A2
        public static Slate.VideoSampler.SampleSettings Default() { }

    }

    // TypeToken: 0x2000103  // size: 0x14
    public sealed struct StartingTransformsMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.ActionClips.PlayAnimatorClip.StartingTransformsMode AutoMatchTransforms;  // const
        public static Slate.ActionClips.PlayAnimatorClip.StartingTransformsMode ManualSetTransforms;  // const

    }

    // TypeToken: 0x2000104  // size: 0x14
    public sealed struct ClipWrapMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.ActionClips.PlayAnimatorClip.ClipWrapMode Loop;  // const
        public static Slate.ActionClips.PlayAnimatorClip.ClipWrapMode PingPong;  // const

    }

    // TypeToken: 0x200010B  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Slate.ActionClips.AnimateLimbIK.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x0AC87854  token: 0x60008BA
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60008BB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010D  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Slate.ActionClips.AnimateLookAtIK.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x0AC877F0  token: 0x60008CE
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60008CF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000111  // size: 0x18
    public sealed class <>c__DisplayClass2_0
    {
        // Fields
        public System.String name;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60008DF
        public System.Void .ctor() { }
        // RVA: 0x06CC7538  token: 0x60008E0
        private System.Boolean <HasParameter>b__0(UnityEngine.AnimatorControllerParameter p) { }

    }

namespace Slate
{

    // TypeToken: 0x2000002  // size: 0x30
    public class DirectorCamera : UnityEngine.MonoBehaviour, Slate.IDirectableCamera
    {
        // Fields
        private System.Boolean _matchMainWhenActive;  // 0x18
        private System.Boolean _setMainWhenActive;  // 0x19
        private System.Boolean _autoHandleActiveState;  // 0x1a
        private System.Boolean _ignoreFOVChanges;  // 0x1b
        private System.Boolean _dontDestroyOnLoad;  // 0x1c
        public static System.Single MAX_DAMP;  // const
        private static System.Action<Slate.IDirectableCamera> onCut;  // static @ 0x0
        private static System.Action onActivate;  // static @ 0x8
        private static System.Action onDeactivate;  // static @ 0x10
        private static Slate.DirectorCamera _current;  // static @ 0x18
        private static UnityEngine.Camera _cam;  // static @ 0x20
        private static Slate.IDirectableCamera lastTargetShot;  // static @ 0x28
        private System.Single <focalDistance>k__BackingField;  // 0x20
        private System.Single <focalLength>k__BackingField;  // 0x24
        private System.Single <focalAperture>k__BackingField;  // 0x28
        private static Slate.GameCamera <gameCamera>k__BackingField;  // static @ 0x30
        private static System.Boolean <isEnabled>k__BackingField;  // static @ 0x38
        private static System.Single noiseTimer;  // static @ 0x3c
        private static UnityEngine.Vector3 noisePosOffset;  // static @ 0x40
        private static UnityEngine.Vector3 noiseRotOffset;  // static @ 0x4c
        private static UnityEngine.Vector3 noiseTargetPosOffset;  // static @ 0x58
        private static UnityEngine.Vector3 noiseTargetRotOffset;  // static @ 0x64
        private static UnityEngine.Vector3 noiseCamPosVel;  // static @ 0x70
        private static UnityEngine.Vector3 noiseCamRotVel;  // static @ 0x7c

        // Properties
        Slate.DirectorCamera current { get; /* RVA: 0x0AC62EEC */ }
        UnityEngine.Camera cam { get; /* RVA: 0x0AC62DBC */ }
        UnityEngine.Vector3 position { get; /* RVA: 0x0AC63200 */ set; /* RVA: 0x0AC63668 */ }
        UnityEngine.Quaternion rotation { get; /* RVA: 0x0AC63278 */ set; /* RVA: 0x0AC636BC */ }
        System.Single fieldOfView { get; /* RVA: 0x0AC630F0 */ set; /* RVA: 0x0AC63528 */ }
        System.Single focalDistance { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x011EC9E0 */ }
        System.Single focalLength { get; /* RVA: 0x04D89850 */ set; /* RVA: 0x04D89860 */ }
        System.Single focalAperture { get; /* RVA: 0x04D86BF0 */ set; /* RVA: 0x04D86C00 */ }
        System.Boolean matchMainWhenActive { get; /* RVA: 0x0AC631E0 */ set; /* RVA: 0x0AC63644 */ }
        System.Boolean setMainWhenActive { get; /* RVA: 0x0AC632C4 */ set; /* RVA: 0x0AC63708 */ }
        System.Boolean autoHandleActiveState { get; /* RVA: 0x0AC62D9C */ set; /* RVA: 0x0AC634E0 */ }
        System.Boolean ignoreFOVChanges { get; /* RVA: 0x0AC63188 */ set; /* RVA: 0x0AC635E4 */ }
        System.Boolean dontDestroyOnLoad { get; /* RVA: 0x0AC630D0 */ set; /* RVA: 0x0AC63504 */ }
        UnityEngine.Camera renderCamera { get; /* RVA: 0x0AC63254 */ }
        Slate.GameCamera gameCamera { get; /* RVA: 0x0AC63150 */ set; /* RVA: 0x0AC63590 */ }
        System.Boolean isEnabled { get; /* RVA: 0x0AC631A8 */ set; /* RVA: 0x0AC63608 */ }

        // Events
        event System.Action<Slate.IDirectableCamera> onCut;
        event System.Action onActivate;
        event System.Action onDeactivate;

        // Methods
        // RVA: 0x0AC61CC8  token: 0x6000024
        private System.Void Awake() { }
        // RVA: 0x0AC61DEC  token: 0x6000025
        private UnityEngine.Camera CreateRenderCamera() { }
        // RVA: 0x0AC620AC  token: 0x6000026
        public static System.Void Enable() { }
        // RVA: 0x0AC61F84  token: 0x6000027
        public static System.Void Disable() { }
        // RVA: 0x0AC624C8  token: 0x6000028
        public static System.Void Update(Slate.IDirectableCamera source, Slate.IDirectableCamera target, Slate.EaseType interpolation, System.Single weight, System.Single damping) { }
        // RVA: 0x0AC618DC  token: 0x6000029
        public static System.Void ApplyNoise(System.Single magnitude, System.Single weight) { }
        // RVA: 0x04D77850  token: 0x600002A
        public System.Void .ctor() { }
        // RVA: 0x053916BC  token: 0x600002B
        private virtual UnityEngine.GameObject Slate.IDirectableCamera.get_gameObject() { }

    }

    // TypeToken: 0x2000003  // size: 0x18
    public class DirectorGUI : UnityEngine.MonoBehaviour
    {
        // Fields
        private static Slate.DirectorGUI.SubtitlesGUIDelegate OnSubtitlesGUI;  // static @ 0x0
        private static Slate.DirectorGUI.TextOverlayGUIDelegate OnTextOverlayGUI;  // static @ 0x8
        private static Slate.DirectorGUI.ImageOverlayGUIDelegate OnImageOverlayGUI;  // static @ 0x10
        private static Slate.DirectorGUI.ScreenFadeGUIDelegate OnScreenFadeGUI;  // static @ 0x18
        private static Slate.DirectorGUI.LetterboxGUIDelegate OnLetterboxGUI;  // static @ 0x20
        private static Slate.DirectorGUI.CameraDissolveDelegate OnCameraDissolve;  // static @ 0x28
        private static System.Action OnGUIEnable;  // static @ 0x30
        private static System.Action OnGUIDisable;  // static @ 0x38
        private static Slate.DirectorGUI _current;  // static @ 0x40
        private static UnityEngine.Color <lastFadeColor>k__BackingField;  // static @ 0x48

        // Properties
        Slate.DirectorGUI current { get; /* RVA: 0x0AC641D8 */ }
        UnityEngine.Color lastFadeColor { get; /* RVA: 0x0AC64364 */ set; /* RVA: 0x0AC64920 */ }

        // Events
        event Slate.DirectorGUI.SubtitlesGUIDelegate OnSubtitlesGUI;
        event Slate.DirectorGUI.TextOverlayGUIDelegate OnTextOverlayGUI;
        event Slate.DirectorGUI.ImageOverlayGUIDelegate OnImageOverlayGUI;
        event Slate.DirectorGUI.ScreenFadeGUIDelegate OnScreenFadeGUI;
        event Slate.DirectorGUI.LetterboxGUIDelegate OnLetterboxGUI;
        event Slate.DirectorGUI.CameraDissolveDelegate OnCameraDissolve;
        event System.Action OnGUIEnable;
        event System.Action OnGUIDisable;

        // Methods
        // RVA: 0x0AC6372C  token: 0x600003D
        private System.Void Awake() { }
        // RVA: 0x0AC63920  token: 0x600003E
        private System.Void OnEnable() { }
        // RVA: 0x0AC6380C  token: 0x600003F
        private System.Void OnDisable() { }
        // RVA: 0x0AC63A58  token: 0x6000040
        public static System.Void UpdateLetterbox(System.Single completion) { }
        // RVA: 0x0AC63964  token: 0x6000041
        public static System.Void UpdateDissolve(UnityEngine.Texture texture, System.Single completion) { }
        // RVA: 0x0AC639C8  token: 0x6000044
        public static System.Void UpdateFade(UnityEngine.Color color) { }
        // RVA: 0x0AC63BF4  token: 0x6000045
        public static System.Void UpdateSubtitles(System.String text, UnityEngine.Color color) { }
        // RVA: 0x0AC63B50  token: 0x6000046
        public static System.Void UpdateOverlayText(System.String text, UnityEngine.Color color, System.Single size, UnityEngine.TextAnchor anchor, UnityEngine.Vector2 pos) { }
        // RVA: 0x0AC63AB4  token: 0x6000047
        public static System.Void UpdateOverlayImage(UnityEngine.Texture texture, UnityEngine.Color color, UnityEngine.Vector2 scale, UnityEngine.Vector2 positionOffset) { }
        // RVA: 0x05393520  token: 0x6000048
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x28
    public class DynamicCameraController
    {
        // Fields
        private static System.Single MIN_DAMP;  // const
        private static System.Single MAX_DAMP;  // const
        private Slate.DynamicCameraController.Transposer _transposer;  // 0x10
        private Slate.DynamicCameraController.Composer _composer;  // 0x18
        private System.Int32 lastUpdateFrame;  // 0x20

        // Properties
        Slate.DynamicCameraController.Transposer transposer { get; /* RVA: 0x02B2ECC0 */ }
        Slate.DynamicCameraController.Composer composer { get; /* RVA: 0x0385B100 */ }
        System.Boolean controlsPosition { get; /* RVA: 0x0AC65748 */ }
        System.Boolean controlsRotation { get; /* RVA: 0x0AC65760 */ }
        System.Boolean controlsFieldOfView { get; /* RVA: 0x0AC6572C */ }

        // Methods
        // RVA: 0x0AC649FC  token: 0x6000066
        public System.Void UpdateControllerHard(Slate.IDirectableCamera directableCamera, Slate.IDirectable directable) { }
        // RVA: 0x0AC64A18  token: 0x6000067
        public System.Void UpdateControllerSoft(Slate.IDirectableCamera directableCamera, Slate.IDirectable directable) { }
        // RVA: 0x0AC64A34  token: 0x6000068
        private System.Void UpdateController(Slate.IDirectableCamera directableCamera, Slate.IDirectable directable, System.Boolean isHard) { }
        // RVA: 0x0AC65680  token: 0x6000069
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000010  // size: 0x20
    public class GameCamera : UnityEngine.MonoBehaviour, Slate.IDirectableCamera
    {
        // Fields
        private UnityEngine.Camera _cam;  // 0x18

        // Properties
        UnityEngine.Camera cam { get; /* RVA: 0x0AC65778 */ }
        UnityEngine.Vector3 position { get; /* RVA: 0x0AC65864 */ set; /* RVA: 0x041E1670 */ }
        UnityEngine.Quaternion rotation { get; /* RVA: 0x0AC658AC */ set; /* RVA: 0x041E1670 */ }
        System.Single fieldOfView { get; /* RVA: 0x0AC65804 */ set; /* RVA: 0x041E1670 */ }
        System.Single focalDistance { get; /* RVA: 0x04DA9C10 */ set; /* RVA: 0x041E1670 */ }
        System.Single focalLength { get; /* RVA: 0x04DA9C20 */ set; /* RVA: 0x041E1670 */ }
        System.Single focalAperture { get; /* RVA: 0x04DA9C00 */ set; /* RVA: 0x041E1670 */ }

        // Methods
        // RVA: 0x05393520  token: 0x6000079
        public System.Void .ctor() { }
        // RVA: 0x053916BC  token: 0x600007A
        private virtual UnityEngine.GameObject Slate.IDirectableCamera.get_gameObject() { }

    }

    // TypeToken: 0x2000011
    public interface IDirectableCamera
    {
        // Properties
        UnityEngine.GameObject gameObject { get; /* RVA: -1  // abstract */ }
        UnityEngine.Camera cam { get; /* RVA: -1  // abstract */ }
        UnityEngine.Vector3 position { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        UnityEngine.Quaternion rotation { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Single fieldOfView { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Single focalDistance { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Single focalLength { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Single focalAperture { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000012  // size: 0x38
    public class ShotCamera : UnityEngine.MonoBehaviour, Slate.IDirectableCamera
    {
        // Fields
        public static System.String DEFAULT_NAME;  // const
        private Slate.DynamicCameraController _dynamicController;  // 0x18
        private System.Single _focalDistance;  // 0x20
        private System.Single _focalLength;  // 0x24
        private System.Single _focalAperture;  // 0x28
        private UnityEngine.Camera _cam;  // 0x30

        // Properties
        UnityEngine.Camera cam { get; /* RVA: 0x0AC66B18 */ }
        UnityEngine.Vector3 position { get; /* RVA: 0x0AC65864 */ set; /* RVA: 0x0AC66E18 */ }
        UnityEngine.Quaternion rotation { get; /* RVA: 0x0AC658AC */ set; /* RVA: 0x06C5993C */ }
        UnityEngine.Vector3 localPosition { get; /* RVA: 0x0AC66CC0 */ set; /* RVA: 0x0AC66DD4 */ }
        UnityEngine.Vector3 localEulerAngles { get; /* RVA: 0x0AC66C4C */ set; /* RVA: 0x0AC66D60 */ }
        System.Single fieldOfView { get; /* RVA: 0x0AC66BEC */ set; /* RVA: 0x0AC66D08 */ }
        System.Single focalDistance { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x011EC9E0 */ }
        System.Single focalLength { get; /* RVA: 0x04D89850 */ set; /* RVA: 0x04D89860 */ }
        System.Single focalAperture { get; /* RVA: 0x04D86BF0 */ set; /* RVA: 0x04D86C00 */ }
        Slate.DynamicCameraController dynamicController { get; /* RVA: 0x0385B100 */ }
        System.Boolean dynamicControlledPosition { get; /* RVA: 0x0AC66BBC */ }
        System.Boolean dynamicControlledRotation { get; /* RVA: 0x0AC66BD4 */ }
        System.Boolean dynamicControlledFieldOfView { get; /* RVA: 0x0AC66BA4 */ }

        // Methods
        // RVA: 0x0AC663CC  token: 0x600009E
        private System.Void Awake() { }
        // RVA: 0x0AC66A44  token: 0x600009F
        public System.Void UpdateDynamicControllerHard(Slate.IDirectable directable) { }
        // RVA: 0x0AC66A6C  token: 0x60000A0
        public System.Void UpdateDynamicControllerSoft(Slate.IDirectable directable) { }
        // RVA: 0x0AC66A38  token: 0x60000A1
        public System.Void SetDynamicControllerTargets(UnityEngine.Transform target) { }
        // RVA: 0x0AC669E4  token: 0x60000A2
        public System.Void SetDynamicControllerTargets(UnityEngine.Transform transposerTarget, UnityEngine.Transform composerTarget) { }
        // RVA: 0x0AC66840  token: 0x60000A3
        public UnityEngine.RenderTexture GetRenderTexture(System.Int32 width, System.Int32 height) { }
        // RVA: 0x0AC664C0  token: 0x60000A4
        public static Slate.ShotCamera Create(UnityEngine.Transform targetParent) { }
        // RVA: 0x0AC66748  token: 0x60000A5
        public static Slate.ShotCamera Find(System.String shotName) { }
        // RVA: 0x0AC66A94  token: 0x60000A6
        public System.Void .ctor() { }
        // RVA: 0x053916BC  token: 0x60000A7
        private virtual UnityEngine.GameObject Slate.IDirectableCamera.get_gameObject() { }

    }

    // TypeToken: 0x2000014  // size: 0x28
    public class BlendShape
    {
        // Fields
        private UnityEngine.SkinnedMeshRenderer _skin;  // 0x10
        private System.String _name;  // 0x18
        private System.Single _weight;  // 0x20

        // Properties
        UnityEngine.SkinnedMeshRenderer skin { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String name { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Single weight { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x011EC9E0 */ }

        // Methods
        // RVA: 0x0AC5A754  token: 0x60000B0
        public System.Void SetRealWeight(System.Single modWeight) { }
        // RVA: 0x0385B100  token: 0x60000B1
        public virtual System.String ToString() { }
        // RVA: 0x041E1670  token: 0x60000B2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000015  // size: 0x30
    public class BlendShapeGroup
    {
        // Fields
        private System.String _UID;  // 0x10
        private System.String _name;  // 0x18
        private System.Single _weight;  // 0x20
        private System.Collections.Generic.List<Slate.BlendShape> _blendShapes;  // 0x28

        // Properties
        System.String UID { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String name { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Single weight { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x0AC5A748 */ }
        System.Collections.Generic.List<Slate.BlendShape> blendShapes { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x0AC5A67C  token: 0x60000BA
        public System.Void .ctor() { }
        // RVA: 0x0AC5A5FC  token: 0x60000BB
        private System.Void SetBlendWeights() { }
        // RVA: 0x0385B100  token: 0x60000BC
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000016  // size: 0x48
    public class Character : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.List<Slate.BlendShapeGroup> _expressions;  // 0x18
        private UnityEngine.Transform _neckTransform;  // 0x20
        private UnityEngine.Transform _headTransform;  // 0x28
        private UnityEngine.Vector3 _upVector;  // 0x30
        private UnityEngine.Vector3 _rotationOffset;  // 0x3c

        // Properties
        UnityEngine.Transform neck { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.Transform head { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        UnityEngine.Vector3 upVector { get; /* RVA: 0x04D88ED0 */ set; /* RVA: 0x04D88F10 */ }
        UnityEngine.Vector3 rotationOffset { get; /* RVA: 0x04D88EB0 */ set; /* RVA: 0x04D88F00 */ }
        System.Collections.Generic.List<Slate.BlendShapeGroup> expressions { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0AC5C730  token: 0x60000C6
        public Slate.BlendShapeGroup FindExpressionByName(System.String name) { }
        // RVA: 0x0AC5C800  token: 0x60000C7
        public Slate.BlendShapeGroup FindExpressionByUID(System.String UID) { }
        // RVA: 0x0AC5C950  token: 0x60000C8
        public System.Void SetExpressionWeightByName(System.String name, System.Single weight) { }
        // RVA: 0x0AC5C984  token: 0x60000C9
        public System.Void SetExpressionWeightByUID(System.String UID, System.Single weight) { }
        // RVA: 0x0AC5C8D0  token: 0x60000CA
        public System.Void ResetExpressions() { }
        // RVA: 0x0AC5C9B8  token: 0x60000CB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x18
    public class NameAttribute : System.Attribute
    {
        // Fields
        public readonly System.String name;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60000D0
        public System.Void .ctor(System.String name) { }

    }

    // TypeToken: 0x200001A  // size: 0x18
    public class CategoryAttribute : System.Attribute
    {
        // Fields
        public readonly System.String category;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60000D1
        public System.Void .ctor(System.String category) { }

    }

    // TypeToken: 0x200001B  // size: 0x18
    public class DescriptionAttribute : System.Attribute
    {
        // Fields
        public readonly System.String description;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60000D2
        public System.Void .ctor(System.String description) { }

    }

    // TypeToken: 0x200001C  // size: 0x20
    public class IconAttribute : System.Attribute
    {
        // Fields
        public readonly System.String iconName;  // 0x10
        public readonly System.Type fromType;  // 0x18

        // Methods
        // RVA: 0x053908C0  token: 0x60000D3
        public System.Void .ctor(System.String iconName) { }
        // RVA: 0x05392C40  token: 0x60000D4
        public System.Void .ctor(System.Type fromType) { }

    }

    // TypeToken: 0x200001D  // size: 0x18
    public class AttachableAttribute : System.Attribute
    {
        // Fields
        public readonly System.Type[] types;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60000D5
        public System.Void .ctor(System.Type[] types) { }

    }

    // TypeToken: 0x200001E  // size: 0x10
    public class UniqueElementAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000D6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x10
    public class ShowTrajectoryAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000D7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000020  // size: 0x10
    public class PositionHandleAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000D8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000021  // size: 0x18
    public class RotationHandleAttribute : System.Attribute
    {
        // Fields
        public readonly System.String positionPropertyName;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60000D9
        public System.Void .ctor(System.String positionPropertyName) { }

    }

    // TypeToken: 0x2000022  // size: 0x30
    public class AnimatableParameterAttribute : UnityEngine.PropertyAttribute
    {
        // Fields
        public System.String link;  // 0x10
        public readonly System.Nullable<System.Single> min;  // 0x18
        public readonly System.Nullable<System.Single> max;  // 0x20
        public readonly System.String customName;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x60000DA
        public System.Void .ctor() { }
        // RVA: 0x02FDB880  token: 0x60000DB
        public System.Void .ctor(System.String customName) { }
        // RVA: 0x0AC51244  token: 0x60000DC
        public System.Void .ctor(System.String customName, System.Single min, System.Single max) { }
        // RVA: 0x0AC512D8  token: 0x60000DD
        public System.Void .ctor(System.Single min, System.Single max) { }

    }

    // TypeToken: 0x2000023  // size: 0x10
    public class ParseAnimatableParametersAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000DE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000024  // size: 0x18
    public class ShaderPropertyPopupAttribute : UnityEngine.PropertyAttribute
    {
        // Fields
        public readonly System.Type propertyType;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000DF
        public System.Void .ctor() { }
        // RVA: 0x053908C0  token: 0x60000E0
        public System.Void .ctor(System.Type propertyType) { }

    }

    // TypeToken: 0x2000025  // size: 0x10
    public class LeftToggleAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000E1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000026  // size: 0x18
    public class MinAttribute : UnityEngine.PropertyAttribute
    {
        // Fields
        public readonly System.Single min;  // 0x10

        // Methods
        // RVA: 0x04D88280  token: 0x60000E2
        public System.Void .ctor(System.Single min) { }

    }

    // TypeToken: 0x2000027  // size: 0x18
    public class ExampleTextAttribute : UnityEngine.PropertyAttribute
    {
        // Fields
        public readonly System.String text;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60000E3
        public System.Void .ctor(System.String text) { }

    }

    // TypeToken: 0x2000028  // size: 0x18
    public class HelpBoxAttribute : UnityEngine.PropertyAttribute
    {
        // Fields
        public readonly System.String text;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60000E4
        public System.Void .ctor(System.String text) { }

    }

    // TypeToken: 0x2000029  // size: 0x20
    public class ShowIfAttribute : UnityEngine.PropertyAttribute
    {
        // Fields
        public readonly System.String propertyName;  // 0x10
        public readonly System.Int32 value;  // 0x18

        // Methods
        // RVA: 0x03B46FF0  token: 0x60000E5
        public System.Void .ctor(System.String propertyName, System.Int32 value) { }

    }

    // TypeToken: 0x200002A  // size: 0x20
    public class EnabledIfAttribute : UnityEngine.PropertyAttribute
    {
        // Fields
        public readonly System.String propertyName;  // 0x10
        public readonly System.Int32 value;  // 0x18

        // Methods
        // RVA: 0x03B46FF0  token: 0x60000E6
        public System.Void .ctor(System.String propertyName, System.Int32 value) { }

    }

    // TypeToken: 0x200002B  // size: 0x18
    public class CallbackAttribute : UnityEngine.PropertyAttribute
    {
        // Fields
        public readonly System.String methodName;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60000E7
        public System.Void .ctor(System.String methodName) { }

    }

    // TypeToken: 0x200002C  // size: 0x10
    public class RequiredAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000E8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002D  // size: 0x10
    public class PlaybackProtectedAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000E9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002E  // size: 0x10
    public class ReadOnlyAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000EA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002F  // size: 0x10
    public class SortingLayerAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000EB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000030  // size: 0x10
    public class ActorGroupPopupAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60000EC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000031  // size: 0x40
    public abstract class DirectorActionClip : Slate.ActionClip
    {
        // Methods
        // RVA: 0x05393520  token: 0x60000ED
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000032
    public interface IEvent
    {
        // Properties
        System.String name { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60000EF
        public virtual System.Void Invoke() { }

    }

    // TypeToken: 0x2000033  // size: 0x14
    public sealed struct TransformSpace
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.TransformSpace CutsceneSpace;  // const
        public static Slate.TransformSpace ActorSpace;  // const
        public static Slate.TransformSpace WorldSpace;  // const
        public static Slate.TransformSpace ParentSpace;  // const

    }

    // TypeToken: 0x2000034  // size: 0x14
    public sealed struct MiniTransformSpace
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.MiniTransformSpace CutsceneSpace;  // const
        public static Slate.MiniTransformSpace WorldSpace;  // const
        public static Slate.MiniTransformSpace ParentSpace;  // const

    }

    // TypeToken: 0x2000035  // size: 0x14
    public sealed struct ActiveState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.ActiveState Disable;  // const
        public static Slate.ActiveState Enable;  // const
        public static Slate.ActiveState Toggle;  // const

    }

    // TypeToken: 0x2000036  // size: 0xA8
    public class CameraShot : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        private Slate.ShotCamera _targetShot;  // 0x50
        public Slate.CameraShot.BlendInEffectType blendInEffect;  // 0x58
        public Slate.CameraShot.BlendOutEffectType blendOutEffect;  // 0x5c
        public System.Single steadyCamEffect;  // 0x60
        public UnityEngine.Color fadeToColor;  // 0x64
        public UnityEngine.Color fadeFromColor;  // 0x74
        public Slate.ActorGroup overrideShotTargetActorGroup;  // 0x88
        private UnityEngine.Color lastFadeColor;  // 0x90
        private Slate.CameraShot <previousShot>k__BackingField;  // 0xa0

        // Properties
        System.String info { get; /* RVA: 0x0AC5B6E8 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC5B768 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Boolean canCrossBlend { get; /* RVA: 0x0AC5B51C */ }
        UnityEngine.GameObject actor { get; /* RVA: 0x0AC5B4A8 */ }
        Slate.CameraShot previousShot { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x05391334 */ }
        Slate.ShotCamera targetShot { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x0AC5BC48 */ }
        Slate.CameraTrack track { get; /* RVA: 0x0AC5B8E8 */ }
        UnityEngine.Vector3 position { get; /* RVA: 0x0AC5B7B8 */ set; /* RVA: 0x0AC5BB28 */ }
        UnityEngine.Vector3 rotation { get; /* RVA: 0x0AC5B850 */ set; /* RVA: 0x0AC5BBB8 */ }
        System.Single fieldOfView { get; /* RVA: 0x0AC5B524 */ set; /* RVA: 0x0AC5B924 */ }
        System.Single focalPoint { get; /* RVA: 0x0AC5B608 */ set; /* RVA: 0x0AC5BA38 */ }
        System.Single focalRange { get; /* RVA: 0x0AC5B678 */ set; /* RVA: 0x0AC5BAB0 */ }
        System.Single focalAperture { get; /* RVA: 0x0AC5B598 */ set; /* RVA: 0x0AC5B9C0 */ }

        // Methods
        // RVA: 0x0AC5A9C8  token: 0x600010B
        protected virtual System.Void OnAfterValidate() { }
        // RVA: 0x0AC5AF9C  token: 0x600010C
        protected virtual System.Void OnRootEnabled() { }
        // RVA: 0x0AC5AF10  token: 0x600010D
        protected virtual System.Void OnRootDisabled() { }
        // RVA: 0x0AC5B028  token: 0x600010E
        protected virtual System.Void OnRootUpdated(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC5B384  token: 0x600010F
        public System.Void TryUpdateShotTargetOverride() { }
        // RVA: 0x0AC5AE98  token: 0x6000110
        protected virtual System.Boolean OnInitialize() { }
        // RVA: 0x0AC5ADAC  token: 0x6000111
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC5B0B4  token: 0x6000112
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC5AEAC  token: 0x6000113
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC5B458  token: 0x6000114
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003A  // size: 0x70
    public class PlayVideo : Slate.ActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public UnityEngine.Video.VideoClip videoClip;  // 0x50
        private Slate.VideoSampler.SampleSettings settings;  // 0x58

        // Properties
        System.Boolean isValid { get; /* RVA: 0x0AC65FF4 */ }
        System.String info { get; /* RVA: 0x0AC65F74 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        Slate.VideoTrack track { get; /* RVA: 0x0AC66044 */ }

        // Methods
        // RVA: 0x0AC65E38  token: 0x6000120
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC65E38  token: 0x6000121
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC65E40  token: 0x6000122
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC65E40  token: 0x6000123
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC65DC0  token: 0x6000124
        private System.Void Enable() { }
        // RVA: 0x0AC65E48  token: 0x6000125
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC65D94  token: 0x6000126
        private System.Void Disable() { }
        // RVA: 0x0AC65F48  token: 0x6000127
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003B  // size: 0xA8
    public class ActorGroup : Slate.CutsceneGroup
    {
        // Fields
        private System.String _name;  // 0x70
        private UnityEngine.GameObject _actor;  // 0x78
        private Slate.CutsceneGroup.ActorReferenceMode _referenceMode;  // 0x80
        private Slate.CutsceneGroup.ActorInitialTransformation _initialCoordinates;  // 0x84
        private UnityEngine.Vector3 _initialLocalPosition;  // 0x88
        private UnityEngine.Vector3 _initialLocalRotation;  // 0x94
        private System.Boolean _displayVirtualMeshGizmo;  // 0xa0

        // Properties
        System.String name { get; /* RVA: 0x0AC50F24 */ set; /* RVA: 0x063AD760 */ }
        UnityEngine.GameObject actor { get; /* RVA: 0x0AC50F18 */ set; /* RVA: 0x0AC50FBC */ }
        Slate.CutsceneGroup.ActorReferenceMode referenceMode { get; /* RVA: 0x04D863A0 */ set; /* RVA: 0x04D86420 */ }
        Slate.CutsceneGroup.ActorInitialTransformation initialTransformation { get; /* RVA: 0x04D8FCE0 */ set; /* RVA: 0x04D8FCF0 */ }
        UnityEngine.Vector3 initialLocalPosition { get; /* RVA: 0x04DA9AD0 */ set; /* RVA: 0x04DA9B10 */ }
        UnityEngine.Vector3 initialLocalRotation { get; /* RVA: 0x04DA9AF0 */ set; /* RVA: 0x04DA9B30 */ }
        System.Boolean displayVirtualMeshGizmo { get; /* RVA: 0x04D88020 */ set; /* RVA: 0x04D88040 */ }

        // Methods
        // RVA: 0x0AC50EC8  token: 0x6000136
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003C  // size: 0x70
    public class DirectorGroup : Slate.CutsceneGroup
    {
        // Properties
        System.String name { get; /* RVA: 0x0AC649D0 */ set; /* RVA: 0x041E1670 */ }
        UnityEngine.GameObject actor { get; /* RVA: 0x0AC649AC */ set; /* RVA: 0x041E1670 */ }
        Slate.CutsceneGroup.ActorReferenceMode referenceMode { get; /* RVA: 0x011EC580 */ set; /* RVA: 0x041E1670 */ }
        Slate.CutsceneGroup.ActorInitialTransformation initialTransformation { get; /* RVA: 0x011EC580 */ set; /* RVA: 0x041E1670 */ }
        UnityEngine.Vector3 initialLocalPosition { get; /* RVA: 0x09D954F8 */ set; /* RVA: 0x041E1670 */ }
        UnityEngine.Vector3 initialLocalRotation { get; /* RVA: 0x09D954F8 */ set; /* RVA: 0x041E1670 */ }
        System.Boolean displayVirtualMeshGizmo { get; /* RVA: 0x012081B0 */ set; /* RVA: 0x041E1670 */ }

        // Methods
        // RVA: 0x0AC64960  token: 0x6000145
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003D  // size: 0x50
    public abstract class ActionTrack : Slate.CutsceneTrack
    {
        // Methods
        // RVA: 0x0AC50EA8  token: 0x6000146
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200003E  // size: 0x50
    public class ActorActionTrack : Slate.ActionTrack
    {
        // Methods
        // RVA: 0x0AC50EA8  token: 0x6000147
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003F  // size: 0x98
    public class ActorAudioTrack : Slate.AudioTrack
    {
        // Fields
        protected System.Boolean _useAudioSourceOnActor;  // 0x90

        // Properties
        System.Boolean useAudioSourceOnActor { get; /* RVA: 0x02EC6590 */ }

        // Methods
        // RVA: 0x0AC50EB0  token: 0x6000149
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000040  // size: 0x58
    public class ActorPropertiesTrack : Slate.PropertiesTrack
    {
        // Methods
        // RVA: 0x0AC5102C  token: 0x600014A
        protected virtual System.Void OnCreate() { }
        // RVA: 0x0AC51114  token: 0x600014B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000041  // size: 0x78
    public class AnimationTrack : Slate.CutsceneTrack
    {
        // Fields
        private System.Single _weight;  // 0x50
        private System.Single _blendIn;  // 0x54
        private System.Single _blendOut;  // 0x58
        private UnityEngine.AnimationBlendMode _animationBlendMode;  // 0x5c
        private System.String _mixTransformName;  // 0x60
        private UnityEngine.Animation anim;  // 0x68
        private UnityEngine.AnimationState state;  // 0x70

        // Properties
        System.String info { get; /* RVA: 0x0AC580C0 */ }
        System.Single blendIn { get; /* RVA: 0x04D86E00 */ }
        System.Single blendOut { get; /* RVA: 0x04D88400 */ }
        System.Single weight { get; /* RVA: 0x04D885A0 */ }
        UnityEngine.AnimationBlendMode animationBlendMode { get; /* RVA: 0x04D86560 */ set; /* RVA: 0x04D865C0 */ }
        System.String mixTransformName { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }

        // Methods
        // RVA: 0x0AC57E68  token: 0x6000154
        protected virtual System.Boolean OnInitialize() { }
        // RVA: 0x0AC57C54  token: 0x6000155
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC57F84  token: 0x6000156
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC57E28  token: 0x6000157
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC57F44  token: 0x6000158
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC57E28  token: 0x6000159
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC57AD4  token: 0x600015A
        public UnityEngine.Transform GetMixTransform() { }
        // RVA: 0x0AC5805C  token: 0x600015B
        public System.Void .ctor() { }
        // RVA: 0x0AC58024  token: 0x600015C
        private System.Boolean <GetMixTransform>b__27_0(UnityEngine.Transform t) { }

    }

    // TypeToken: 0x2000042  // size: 0xF8
    public class AnimatorTrack : Slate.CutsceneTrack
    {
        // Fields
        private static System.Int32 ROOTMOTION_FRAMERATE;  // const
        public UnityEngine.AvatarMask mask;  // 0x50
        public UnityEngine.AnimationBlendMode blendMode;  // 0x58
        public System.Single weight;  // 0x5c
        public System.Boolean useRootMotion;  // 0x60
        public System.Boolean applyFootIK;  // 0x61
        public System.Boolean isRootMotionPreBaked;  // 0x62
        private System.Collections.Generic.List<UnityEngine.Vector3> rmPositions;  // 0x68
        private System.Collections.Generic.List<UnityEngine.Quaternion> rmRotations;  // 0x70
        private System.Int32 activeClips;  // 0x78
        private System.Single activeClipsWeight;  // 0x7c
        private System.Collections.Generic.Dictionary<Slate.ActionClips.PlayAnimatorClip,System.Int32> ports;  // 0x80
        private UnityEngine.Playables.PlayableGraph graph;  // 0x88
        private UnityEngine.Animations.AnimationPlayableOutput animationOutput;  // 0x98
        private UnityEngine.Animations.AnimationLayerMixerPlayable masterMixer;  // 0xa8
        private UnityEngine.Animations.AnimationMixerPlayable clipsMixer;  // 0xb8
        private UnityEngine.Animations.AnimatorControllerPlayable animatorPlayable;  // 0xc8
        private System.Collections.Generic.List<Slate.AnimatorTrack> siblingTracks;  // 0xd8
        private System.Boolean wasRootMotion;  // 0xe0
        private UnityEngine.AnimatorCullingMode wasCullingMode;  // 0xe4
        private System.Boolean useBakedRootMotion;  // 0xe8
        private UnityEngine.Animator _animator;  // 0xf0

        // Properties
        UnityEngine.Animator animator { get; /* RVA: 0x0AC59F28 */ }
        System.String info { get; /* RVA: 0x0AC5A034 */ }
        System.Boolean isLocked { get; /* RVA: 0x0AC5A1AC */ }
        Slate.AnimatorTrack masterTrack { get; /* RVA: 0x0AC5A1E4 */ }
        System.Boolean isMasterTrack { get; /* RVA: 0x0AC5A1DC */ }
        System.Boolean isLastTrack { get; /* RVA: 0x0AC5A164 */ }
        System.Single compoundTrackWeight { get; /* RVA: 0x0AC5A01C */ }

        // Methods
        // RVA: 0x0AC596E0  token: 0x6000164
        protected virtual System.Boolean OnInitialize() { }
        // RVA: 0x0AC59548  token: 0x6000165
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC59A18  token: 0x6000166
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC59BD0  token: 0x6000167
        private System.Void PostUpdateMasterTrack(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC598F0  token: 0x6000168
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC596A0  token: 0x6000169
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC59B18  token: 0x600016A
        private System.Void PostExitMasterTrack() { }
        // RVA: 0x0AC5998C  token: 0x600016B
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC59140  token: 0x600016C
        public System.Void EnableClip(Slate.ActionClips.PlayAnimatorClip playAnimClip, System.Single blendWeight, System.Single subClipWeight) { }
        // RVA: 0x0AC59E08  token: 0x600016D
        public System.Void UpdateClip(Slate.ActionClips.PlayAnimatorClip playAnimClip, System.Single clipTime, System.Single clipPrevious, System.Single blendWeight, System.Single subClipWeight) { }
        // RVA: 0x0AC59098  token: 0x600016E
        public System.Void DisableClip(Slate.ActionClips.PlayAnimatorClip playAnimClip, System.Single blendWeight, System.Single subClipWeight) { }
        // RVA: 0x0AC58A18  token: 0x600016F
        private System.Void CreateAndPlayTree() { }
        // RVA: 0x0AC58DC4  token: 0x6000170
        private UnityEngine.Playables.Playable CreateClipsMixer(UnityEngine.Playables.PlayableGraph graph) { }
        // RVA: 0x0AC59DB8  token: 0x6000171
        public System.Void PreBakeRootMotion() { }
        // RVA: 0x0AC58954  token: 0x6000172
        public System.Void ClearPreBakeRootMotion() { }
        // RVA: 0x0AC58490  token: 0x6000173
        private System.Void BakeRootMotion() { }
        // RVA: 0x0AC591FC  token: 0x6000174
        private System.Void EvaluateTrackClips(System.Single time, System.Single previousTime, System.Int32& tempActiveClips) { }
        // RVA: 0x0AC581C0  token: 0x6000175
        private System.Void ApplyBakedRootMotion(System.Single time) { }
        // RVA: 0x0AC59F14  token: 0x6000176
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000044  // size: 0x58
    public class MecanimTrack : Slate.CutsceneTrack
    {
        // Fields
        private UnityEngine.Animator animator;  // 0x50

        // Methods
        // RVA: 0x0AC65C28  token: 0x600017A
        protected virtual System.Boolean OnInitialize() { }
        // RVA: 0x0AC65C20  token: 0x600017B
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC65C20  token: 0x600017C
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC65B8C  token: 0x600017D
        private System.Void DestroyDispatcher() { }
        // RVA: 0x0AC50EA8  token: 0x600017E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000045  // size: 0x90
    public abstract class AudioTrack : Slate.CutsceneTrack
    {
        // Fields
        protected UnityEngine.Audio.AudioMixerGroup _outputMixer;  // 0x50
        protected System.Single _masterVolume;  // 0x58
        protected System.Single _masterPitch;  // 0x5c
        protected System.Single _masterStereoPan;  // 0x60
        protected System.Single _masterSpatialBlend;  // 0x64
        protected System.Boolean _ignoreTimeScale;  // 0x68
        protected System.Boolean _bypassReverb;  // 0x69
        private UnityEngine.AudioSource <source>k__BackingField;  // 0x70
        private Slate.AudioSampler.SampleSettings <sampleSettings>k__BackingField;  // 0x78

        // Properties
        System.String info { get; /* RVA: 0x0AC5A52C */ }
        UnityEngine.AudioSource source { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }
        Slate.AudioSampler.SampleSettings sampleSettings { get; /* RVA: 0x04DA9B50 */ set; /* RVA: 0x04DA9B70 */ }
        UnityEngine.Audio.AudioMixerGroup mixer { get; /* RVA: 0x04D86270 */ }
        System.Boolean useAudioSourceOnActor { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x0AC5A36C  token: 0x6000186
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC5A36C  token: 0x6000187
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC5A37C  token: 0x6000188
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC5A374  token: 0x6000189
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC5A374  token: 0x600018A
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC5A290  token: 0x600018B
        private System.Void Enable() { }
        // RVA: 0x0AC5A230  token: 0x600018C
        private System.Void Disable() { }
        // RVA: 0x0AC5A480  token: 0x600018D
        private System.Void SetAndApplySettings() { }
        // RVA: 0x0AC5A520  token: 0x600018E
        public System.Void SetVolume(System.Single volume) { }
        // RVA: 0x0AC50EB0  token: 0x600018F
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000046  // size: 0x98
    public class CameraTrack : Slate.CutsceneTrack
    {
        // Fields
        private static Slate.CameraTrack activeCameraTrack;  // static @ 0x0
        private System.Single _startTimeOffset;  // 0x50
        private System.Single _endTimeOffset;  // 0x54
        public System.Single _blendIn;  // 0x58
        public System.Single _blendOut;  // 0x5c
        public Slate.EaseType interpolation;  // 0x60
        public System.Single cineBoxFadeTime;  // 0x64
        public System.Single appliedSmoothing;  // 0x68
        public UnityEngine.Camera exitCameraOverride;  // 0x70
        private Slate.GameCamera entryCamera;  // 0x78
        private Slate.CameraShot <firstShot>k__BackingField;  // 0x80
        private Slate.CameraShot <lastShot>k__BackingField;  // 0x88
        private Slate.CameraShot <currentShot>k__BackingField;  // 0x90

        // Properties
        Slate.CameraShot firstShot { get; /* RVA: 0x04D86260 */ set; /* RVA: 0x04CD9970 */ }
        Slate.CameraShot lastShot { get; /* RVA: 0x04D861F0 */ set; /* RVA: 0x05395D30 */ }
        Slate.CameraShot currentShot { get; /* RVA: 0x04D86220 */ set; /* RVA: 0x06402330 */ }
        System.String info { get; /* RVA: 0x0AC5C604 */ }
        System.Single startTime { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x0AC5C6DC */ }
        System.Single endTime { get; /* RVA: 0x0AC5C5D8 */ set; /* RVA: 0x0AC5C668 */ }
        System.Single blendIn { get; /* RVA: 0x0AC5C484 */ set; /* RVA: 0x04D88430 */ }
        System.Single blendOut { get; /* RVA: 0x0AC5C52C */ set; /* RVA: 0x04D91510 */ }

        // Methods
        // RVA: 0x0AC5BCC4  token: 0x600019F
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC5C0B4  token: 0x60001A0
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC5BEE0  token: 0x60001A1
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC5BF7C  token: 0x60001A2
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC5C018  token: 0x60001A3
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC5C46C  token: 0x60001A4
        public System.Void .ctor() { }
        // RVA: 0x0AC5C3EC  token: 0x60001A5
        private System.Boolean <OnEnter>b__36_0(Slate.ActionClip s) { }
        // RVA: 0x0AC5C424  token: 0x60001A6
        private System.Boolean <OnEnter>b__36_1(Slate.ActionClip s) { }

    }

    // TypeToken: 0x2000047  // size: 0x50
    public class DirectorActionTrack : Slate.ActionTrack
    {
        // Methods
        // RVA: 0x0AC50EA8  token: 0x60001A7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000048  // size: 0x90
    public class DirectorAudioTrack : Slate.AudioTrack
    {
        // Methods
        // RVA: 0x0AC50EB0  token: 0x60001A8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000049  // size: 0x58
    public class DirectorPropertiesTrack : Slate.PropertiesTrack
    {
        // Methods
        // RVA: 0x0AC51114  token: 0x60001A9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004A  // size: 0x60
    public class VideoTrack : Slate.CutsceneTrack
    {
        // Fields
        public Slate.VideoSampler.VideoRenderTarget renderTarget;  // 0x50
        public UnityEngine.Video.VideoAspectRatio aspectRatio;  // 0x54
        private UnityEngine.Video.VideoPlayer <source>k__BackingField;  // 0x58

        // Properties
        UnityEngine.Video.VideoPlayer source { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x036E5590 */ }
        System.String info { get; /* RVA: 0x0AC675C0 */ }

        // Methods
        // RVA: 0x0AC675A0  token: 0x60001AD
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC675A0  token: 0x60001AE
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC675A8  token: 0x60001AF
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC675A8  token: 0x60001B0
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC6753C  token: 0x60001B1
        private System.Void Enable() { }
        // RVA: 0x0AC674F0  token: 0x60001B2
        private System.Void Disable() { }
        // RVA: 0x0AC675B0  token: 0x60001B3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004B  // size: 0x58
    public abstract class PropertiesTrack : Slate.CutsceneTrack, Slate.IKeyable, Slate.IDirectable
    {
        // Fields
        private Slate.AnimationDataCollection _animationData;  // 0x50

        // Properties
        Slate.AnimationDataCollection animationData { get; /* RVA: 0x04D86270 */ }
        System.Object animatedParametersTarget { get; /* RVA: 0x0AC661CC */ }

        // Methods
        // RVA: 0x0AC66080  token: 0x60001B6
        protected virtual System.Void OnAfterValidate() { }
        // RVA: 0x0AC660A8  token: 0x60001B7
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC6611C  token: 0x60001B8
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC660FC  token: 0x60001B9
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC66174  token: 0x60001BA
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200004C  // size: 0x40
    public abstract class ActionClip : UnityEngine.MonoBehaviour, Slate.IDirectable, Slate.IKeyable
    {
        // Fields
        private System.Single _startTime;  // 0x18
        private Slate.AnimationDataCollection _animationData;  // 0x20
        private Slate.IDirectable <parent>k__BackingField;  // 0x28
        private System.Boolean <isHighlight>k__BackingField;  // 0x30
        private System.String[] _cachedAnimParamPaths;  // 0x38

        // Properties
        Slate.IDirector root { get; /* RVA: 0x0AC50B90 */ }
        Slate.IDirectable parent { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        UnityEngine.GameObject actor { get; /* RVA: 0x0AC5087C */ }
        System.Collections.Generic.IEnumerable<Slate.IDirectable> Slate.IDirectable.children { get; /* RVA: 0x011EC580 */ }
        Slate.AnimationDataCollection animationData { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Single startTime { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x0AC50DAC */ }
        System.Single endTime { get; /* RVA: 0x0AC508D4 */ set; /* RVA: 0x0AC50C90 */ }
        System.Boolean isActive { get; /* RVA: 0x0AC50A84 */ }
        System.Boolean isCollapsed { get; /* RVA: 0x0AC50AC0 */ }
        System.Boolean isLocked { get; /* RVA: 0x0AC50AE0 */ }
        System.Single length { get; /* RVA: 0x04D879D0 */ set; /* RVA: 0x041E1670 */ }
        System.Single blendIn { get; /* RVA: 0x04D879D0 */ set; /* RVA: 0x041E1670 */ }
        System.Single blendOut { get; /* RVA: 0x04D879D0 */ set; /* RVA: 0x041E1670 */ }
        System.Boolean canCrossBlend { get; /* RVA: 0x012081B0 */ }
        System.String info { get; /* RVA: 0x0AC509E0 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC50B20 */ }
        UnityEngine.Color color { get; /* RVA: 0x0A96C614 */ }
        UnityEngine.Color textColor { get; /* RVA: 0x0AC50BB0 */ }
        System.Int32 textSizeInner { get; /* RVA: 0x04DA5360 */ }
        System.Int32 textSizeOuter { get; /* RVA: 0x04DA4FC0 */ }
        System.Single interval { get; /* RVA: 0x04D879D0 */ }
        System.String url { get; /* RVA: 0x0AC50BD0 */ }
        System.Boolean isHighlight { get; /* RVA: 0x04D865F0 */ set; /* RVA: 0x04D86620 */ }
        System.Boolean isReadOnly { get; /* RVA: 0x0AC50B00 */ }
        Slate.TransformSpace defaultTransformSpace { get; /* RVA: 0x011EC580 */ }
        System.String[] animatedParameterPaths { get; /* RVA: 0x0AC5089C */ }
        System.Boolean handleParametersRegistrationManually { get; /* RVA: 0x0AC50900 */ }
        System.Object animatedParametersTarget { get; /* RVA: 0x0464E630 */ }
        Slate.EaseType animatedParametersInterpolation { get; /* RVA: 0x011EC580 */ }
        System.Boolean useWeightInParameters { get; /* RVA: 0x012081B0 */ }
        System.Boolean hasParameters { get; /* RVA: 0x0AC509C8 */ }
        System.Boolean hasActiveParameters { get; /* RVA: 0x0AC50924 */ }
        System.Boolean needActor { get; /* RVA: 0x0AC50B70 */ }
        System.Boolean canSample { get; /* RVA: 0x02FFF600 */ }
        System.Int32 updateOrder { get; /* RVA: 0x04DA3400 */ }

        // Methods
        // RVA: 0x0AC501F4  token: 0x60001E6
        private virtual System.Boolean Slate.IDirectable.Initialize() { }
        // RVA: 0x0AC501C0  token: 0x60001E7
        private virtual System.Void Slate.IDirectable.Enter() { }
        // RVA: 0x0AC50284  token: 0x60001E8
        private virtual System.Void Slate.IDirectable.Update(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC501E4  token: 0x60001E9
        private virtual System.Void Slate.IDirectable.Exit() { }
        // RVA: 0x0AC50204  token: 0x60001EA
        private virtual System.Void Slate.IDirectable.ReverseEnter() { }
        // RVA: 0x0AC50214  token: 0x60001EB
        private virtual System.Void Slate.IDirectable.Reverse() { }
        // RVA: 0x0AC50250  token: 0x60001EC
        private virtual System.Void Slate.IDirectable.RootEnabled() { }
        // RVA: 0x0AC50238  token: 0x60001ED
        private virtual System.Void Slate.IDirectable.RootDisabled() { }
        // RVA: 0x0AC50268  token: 0x60001EE
        private virtual System.Void Slate.IDirectable.RootUpdated(System.Single time, System.Single previousTime) { }
        // RVA: 0x04D83230  token: 0x60001EF
        private virtual System.Void Slate.IDirectable.RootDestroyed() { }
        // RVA: 0x0AC50070  token: 0x60001F0
        public System.Void PostCreate(Slate.IDirectable parent) { }
        // RVA: 0x0AC50828  token: 0x60001F1
        public System.Void Validate() { }
        // RVA: 0x0AC50838  token: 0x60001F2
        public virtual System.Void Validate(Slate.IDirector root, Slate.IDirectable parent) { }
        // RVA: 0x02FFF600  token: 0x60001F3
        protected virtual System.Boolean OnInitialize() { }
        // RVA: 0x041E1670  token: 0x60001F4
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC50060  token: 0x60001F5
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x041E1670  token: 0x60001F6
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x041E1670  token: 0x60001F7
        protected virtual System.Void OnExit() { }
        // RVA: 0x041E1670  token: 0x60001F8
        protected virtual System.Void OnReverse() { }
        // RVA: 0x041E1670  token: 0x60001F9
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x041E1670  token: 0x60001FA
        protected virtual System.Void OnDrawGizmosSelected() { }
        // RVA: 0x041E1670  token: 0x60001FB
        protected virtual System.Void OnSceneGUI() { }
        // RVA: 0x041E1670  token: 0x60001FC
        protected virtual System.Void OnCreate() { }
        // RVA: 0x041E1670  token: 0x60001FD
        protected virtual System.Void OnAfterValidate() { }
        // RVA: 0x041E1670  token: 0x60001FE
        protected virtual System.Void OnRootEnabled() { }
        // RVA: 0x041E1670  token: 0x60001FF
        protected virtual System.Void OnRootDisabled() { }
        // RVA: 0x041E1670  token: 0x6000200
        protected virtual System.Void OnRootUpdated(System.Single time, System.Single previousTime) { }
        // RVA: 0x041E1670  token: 0x6000201
        protected virtual System.Void OnRootDestroyed() { }
        // RVA: 0x0AC500F0  token: 0x6000202
        public System.Boolean RootTimeWithinRange() { }
        // RVA: 0x0AC502C8  token: 0x6000203
        public UnityEngine.Vector3 TransformPosition(UnityEngine.Vector3 point, Slate.TransformSpace space) { }
        // RVA: 0x0AC4FFD0  token: 0x6000204
        public UnityEngine.Vector3 InverseTransformPosition(UnityEngine.Vector3 point, Slate.TransformSpace space) { }
        // RVA: 0x0AC50314  token: 0x6000205
        public UnityEngine.Quaternion TransformRotation(UnityEngine.Vector3 euler, Slate.TransformSpace space) { }
        // RVA: 0x0AC5001C  token: 0x6000206
        public UnityEngine.Vector3 InverseTransformRotation(UnityEngine.Quaternion rot, Slate.TransformSpace space) { }
        // RVA: 0x0AC4FD60  token: 0x6000207
        public UnityEngine.Vector3 ActorPositionInSpace(Slate.TransformSpace space) { }
        // RVA: 0x0AC4FFC8  token: 0x6000208
        public UnityEngine.Transform GetSpaceTransform(Slate.TransformSpace space, UnityEngine.GameObject actorOverride) { }
        // RVA: 0x0AC4FF8C  token: 0x6000209
        public Slate.ActionClip GetPreviousClip() { }
        // RVA: 0x0AC4FF30  token: 0x600020A
        public Slate.ActionClip GetNextClip() { }
        // RVA: 0x0AC4FE6C  token: 0x600020B
        public System.Single GetClipWeight() { }
        // RVA: 0x0AC4FED4  token: 0x600020C
        public System.Single GetClipWeight(System.Single time) { }
        // RVA: 0x0AC4FEA8  token: 0x600020D
        public System.Single GetClipWeight(System.Single time, System.Single blendInOut) { }
        // RVA: 0x0AC4FEC4  token: 0x600020E
        public System.Single GetClipWeight(System.Single time, System.Single blendIn, System.Single blendOut) { }
        // RVA: 0x0AC50494  token: 0x600020F
        public System.Void TryMatchSubClipLength() { }
        // RVA: 0x0AC5042C  token: 0x6000210
        public System.Void TryMatchPreviousSubClipLoop() { }
        // RVA: 0x0AC50358  token: 0x6000211
        public System.Void TryMatchNexSubClipLoop() { }
        // RVA: -1  // generic def  token: 0x6000212
        private System.String GetParameterName(System.Linq.Expressions.Expression<System.Func<T,TResult>> func) { }
        // RVA: -1  // generic def  token: 0x6000213
        public Slate.AnimatedParameter GetParameter(System.Linq.Expressions.Expression<System.Func<T,TResult>> func) { }
        // RVA: 0x0AC4FF6C  token: 0x6000214
        public Slate.AnimatedParameter GetParameter(System.String paramName) { }
        // RVA: -1  // generic def  token: 0x6000215
        public System.Void SetParameterEnabled(System.Linq.Expressions.Expression<System.Func<T,TResult>> func, System.Boolean enabled) { }
        // RVA: 0x0AC50158  token: 0x6000216
        public System.Void SetParameterEnabled(System.String paramName, System.Boolean enabled) { }
        // RVA: 0x0AC500A4  token: 0x6000217
        public System.Void ResetAnimatedParameters() { }
        // RVA: 0x0AC4FD90  token: 0x6000218
        private System.Void CreateAnimationDataCollection() { }
        // RVA: 0x0AC5062C  token: 0x6000219
        private System.Void ValidateAnimParams() { }
        // RVA: 0x0AC500F8  token: 0x600021A
        private System.Void SetAnimParamsSnapshot() { }
        // RVA: 0x0AC50550  token: 0x600021B
        private System.Void UpdateAnimParams(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC500B8  token: 0x600021C
        private System.Void RestoreAnimParamsSnapshot() { }
        // RVA: 0x05393520  token: 0x600021D
        protected System.Void .ctor() { }
        // RVA: 0x05396F58  token: 0x600021E
        private virtual System.String Slate.IDirectable.get_name() { }

    }

    // TypeToken: 0x200004D  // size: 0x70
    public class AnimatedParameter : Slate.IAnimatableData, UnityEngine.ISerializationCallbackReceiver
    {
        // Fields
        private static System.Action<Slate.IAnimatableData> onParameterChanged;  // static @ 0x0
        private System.String _serializedData;  // 0x10
        private System.Boolean _isDisabled;  // 0x18
        private UnityEngine.AnimationCurve[] _curves;  // 0x20
        private System.String _scriptExpression;  // 0x28
        private Slate.AnimatedParameter.SerializationMetaData _data;  // 0x30
        private Slate.IAnimatedParameterModel _parameterModel;  // 0x38
        private Slate.IKeyable <keyable>k__BackingField;  // 0x40
        private System.Single[] <snapshot>k__BackingField;  // 0x48
        private System.Single[] <lastEval>k__BackingField;  // 0x50
        private UnityEngine.Transform <virtualTransformParent>k__BackingField;  // 0x58
        private static System.Collections.Generic.Dictionary<System.Type,System.Type> parameterModelsMap;  // static @ 0x8
        public static readonly System.Type[] supportedTypes;  // static @ 0x10
        private System.Object _animatableAttribute;  // 0x60
        private System.Object _resolvedMemberObject;  // 0x68

        // Properties
        System.Boolean enabled { get; /* RVA: 0x0AA1EEF0 */ }
        System.String serializedData { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        Slate.AnimatedParameter.SerializationMetaData data { get; /* RVA: 0x0AC535E4 */ }
        Slate.IAnimatedParameterModel parameterModel { get; /* RVA: 0x0AC53748 */ }
        UnityEngine.AnimationCurve[] curves { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.String scriptExpression { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x0AC53998 */ }
        System.Boolean hasActiveExpression { get; /* RVA: 0x0AC53690 */ }
        System.String parameterName { get; /* RVA: 0x0AC53834 */ }
        System.Type animatedType { get; /* RVA: 0x0AC535C4 */ }
        Slate.AnimatedParameter.ParameterType parameterType { get; /* RVA: 0x0AC53854 */ }
        System.String transformHierarchyPath { get; /* RVA: 0x0AC538B4 */ }
        System.Type declaringType { get; /* RVA: 0x0AC53650 */ }
        System.Reflection.PropertyInfo property { get; /* RVA: 0x0AC53874 */ }
        System.Reflection.FieldInfo field { get; /* RVA: 0x0AC53670 */ }
        System.Boolean isProperty { get; /* RVA: 0x0AC536C0 */ }
        Slate.IKeyable keyable { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        System.Single[] snapshot { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        System.Single[] lastEval { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        UnityEngine.Transform virtualTransformParent { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x036E5590 */ }
        System.Object targetObject { get; /* RVA: 0x0AC53894 */ }
        Slate.AnimatableParameterAttribute animatableAttribute { get; /* RVA: 0x0AC534EC */ }
        System.Boolean isExternal { get; /* RVA: 0x0AC536A8 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC536D8 */ }

        // Events
        event System.Action<Slate.IAnimatableData> onParameterChanged;

        // Methods
        // RVA: 0x0AC53038  token: 0x600021F
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize() { }
        // RVA: 0x0AC52FFC  token: 0x6000220
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize() { }
        // RVA: 0x0AC52DD8  token: 0x6000224
        public System.Void SetEnabled(System.Boolean value, System.Single time) { }
        // RVA: 0x0AC53068  token: 0x6000242
        private static System.Void .cctor() { }
        // RVA: 0x0AC532D8  token: 0x6000243
        public System.Void .ctor(Slate.IKeyable keyable, System.Type type, System.String memberPath, System.String transformPath) { }
        // RVA: 0x0AC51488  token: 0x6000244
        private System.Void ConstructWithField(System.Reflection.FieldInfo targetField, System.Type type, System.String memberPath, System.String transformPath) { }
        // RVA: 0x0AC51658  token: 0x6000245
        private System.Void ConstructWithProperty(System.Reflection.PropertyInfo targetProperty, System.Type type, System.String memberPath, System.String transformPath) { }
        // RVA: 0x0AC522E0  token: 0x6000246
        public System.Reflection.MemberInfo GetMemberInfo() { }
        // RVA: 0x04D862C0  token: 0x6000247
        public virtual UnityEngine.AnimationCurve[] GetCurves() { }
        // RVA: 0x0AC513B4  token: 0x6000248
        public System.Boolean CompareTo(Slate.AnimatedParameter other) { }
        // RVA: 0x0AC523B8  token: 0x6000249
        private System.Void InitializeCurves() { }
        // RVA: 0x042B4AE0  token: 0x600024A
        public virtual System.Void Validate(Slate.IKeyable keyable) { }
        // RVA: 0x036E5590  token: 0x600024B
        public virtual System.Void SetVirtualTransformParent(UnityEngine.Transform virtualTransformParent) { }
        // RVA: 0x0AC52F24  token: 0x600024C
        public virtual System.Void SetSnapshot() { }
        // RVA: 0x0AC52F9C  token: 0x600024D
        public virtual System.Boolean TryAutoKey(System.Single time) { }
        // RVA: 0x0AC51988  token: 0x600024E
        public virtual System.Void Evaluate(System.Single time, System.Single previousTime, System.Single weight) { }
        // RVA: 0x0AC5189C  token: 0x600024F
        private System.Void Evaluate_1_Curves(System.Single time, System.Single previousTime, System.Single weight) { }
        // RVA: 0x012081B0  token: 0x6000250
        private System.Boolean Evaluate_2_Expression(System.Single time, System.Single previousTime, System.Single weight) { }
        // RVA: 0x0AC52910  token: 0x6000251
        public virtual System.Void RestoreSnapshot() { }
        // RVA: 0x0AC520D8  token: 0x6000252
        public System.Object GetEvalValue(System.Single time) { }
        // RVA: 0x0AC526DC  token: 0x6000253
        public System.Object ResolvedMemberObject() { }
        // RVA: 0x0AC52094  token: 0x6000254
        public System.Object GetCurrentValueAsObject() { }
        // RVA: 0x0AC51C94  token: 0x6000255
        private System.Single[] GetCurrentValueAsFloats() { }
        // RVA: 0x0AC52970  token: 0x6000256
        public System.Void SetCurrentValue(System.Object value) { }
        // RVA: 0x0AC529B8  token: 0x6000257
        private System.Void SetCurrentValue(System.Single[] floats) { }
        // RVA: 0x0AC52330  token: 0x6000258
        public virtual System.Boolean HasChanged() { }
        // RVA: 0x0AC52324  token: 0x6000259
        public virtual System.Boolean HasAnyKey() { }
        // RVA: 0x0AC523A8  token: 0x600025A
        public virtual System.Boolean HasKey(System.Single time) { }
        // RVA: 0x0AC522C0  token: 0x600025B
        public virtual System.Single GetKeyNext(System.Single time) { }
        // RVA: 0x0AC522D0  token: 0x600025C
        public virtual System.Single GetKeyPrevious(System.Single time) { }
        // RVA: 0x012081B0  token: 0x600025D
        public virtual System.Boolean TryKeyIdentity(System.Single time) { }
        // RVA: 0x041E1670  token: 0x600025E
        public virtual System.Void SetKeyCurrent(System.Single time) { }
        // RVA: 0x041E1670  token: 0x600025F
        public virtual System.Void RemoveKey(System.Single time) { }
        // RVA: 0x0AC52EB4  token: 0x6000260
        public virtual System.Void SetPreWrapMode(UnityEngine.WrapMode mode) { }
        // RVA: 0x0AC52E44  token: 0x6000261
        public virtual System.Void SetPostWrapMode(UnityEngine.WrapMode mode) { }
        // RVA: 0x0AC52584  token: 0x6000262
        public System.Void OffsetValue(System.Object deltaValue) { }
        // RVA: 0x0AC52614  token: 0x6000263
        public virtual System.Void Reset() { }
        // RVA: 0x0AC51354  token: 0x6000264
        public System.Void ChangeMemberType(Slate.AnimatedParameter.ParameterType newType) { }
        // RVA: 0x0AC52F94  token: 0x6000265
        public virtual System.String ToString() { }
        // RVA: 0x0AC51A14  token: 0x6000266
        public System.String FriendlyName() { }
        // RVA: 0x0AC521AC  token: 0x6000267
        public virtual System.String GetKeyLabel(System.Single time) { }
        // RVA: 0x041E1670  token: 0x6000268
        private System.Void RecordUndo() { }
        // RVA: 0x0AC5250C  token: 0x6000269
        private System.Void NotifyChange() { }

    }

    // TypeToken: 0x2000051
    public interface IAnimatedParameterModel
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000276
        public virtual System.Boolean ForceStepMode() { }
        // RVA: -1  // abstract  token: 0x6000277
        public virtual System.Int32 RequiredCurvesCount() { }
        // RVA: -1  // abstract  token: 0x6000278
        public virtual System.Object ConvertToObject(System.Single[] floats) { }
        // RVA: -1  // abstract  token: 0x6000279
        public virtual System.Single[] ConvertToFloats(System.Object value) { }
        // RVA: -1  // abstract  token: 0x600027A
        public virtual System.String GetKeyLabel(System.Single[] floats) { }
        // RVA: -1  // abstract  token: 0x600027B
        public virtual System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats) { }
        // RVA: -1  // abstract  token: 0x600027C
        public virtual System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info) { }

    }

    // TypeToken: 0x2000052  // size: 0x18
    public sealed struct Animated_Bool : Slate.IAnimatedParameterModel
    {
        // Fields
        private System.Action<System.Object,System.Boolean> setter;  // 0x10

        // Methods
        // RVA: 0x02FFF600  token: 0x600027D
        public virtual System.Boolean ForceStepMode() { }
        // RVA: 0x04D85B80  token: 0x600027E
        public virtual System.Int32 RequiredCurvesCount() { }
        // RVA: 0x0AC53A6C  token: 0x600027F
        public virtual System.Object ConvertToObject(System.Single[] floats) { }
        // RVA: 0x0AC539D0  token: 0x6000280
        public virtual System.Single[] ConvertToFloats(System.Object value) { }
        // RVA: 0x0AC53BA4  token: 0x6000281
        public virtual System.String GetKeyLabel(System.Single[] floats) { }
        // RVA: 0x0AC53C28  token: 0x6000282
        public virtual System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats) { }
        // RVA: 0x0AC53AD0  token: 0x6000283
        public virtual System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info) { }

    }

    // TypeToken: 0x2000053  // size: 0x18
    public sealed struct Animated_Int : Slate.IAnimatedParameterModel
    {
        // Fields
        private System.Action<System.Object,System.Int32> setter;  // 0x10

        // Methods
        // RVA: 0x02FFF600  token: 0x6000284
        public virtual System.Boolean ForceStepMode() { }
        // RVA: 0x04D85B80  token: 0x6000285
        public virtual System.Int32 RequiredCurvesCount() { }
        // RVA: 0x0AC54BE0  token: 0x6000286
        public virtual System.Object ConvertToObject(System.Single[] floats) { }
        // RVA: 0x0AC54B54  token: 0x6000287
        public virtual System.Single[] ConvertToFloats(System.Object value) { }
        // RVA: 0x0AC54CFC  token: 0x6000288
        public virtual System.String GetKeyLabel(System.Single[] floats) { }
        // RVA: 0x0AC54D78  token: 0x6000289
        public virtual System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats) { }
        // RVA: 0x0AC54C3C  token: 0x600028A
        public virtual System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info) { }

    }

    // TypeToken: 0x2000054  // size: 0x18
    public sealed struct Animated_Float : Slate.IAnimatedParameterModel
    {
        // Fields
        private System.Action<System.Object,System.Single> setter;  // 0x10

        // Methods
        // RVA: 0x012081B0  token: 0x600028B
        public virtual System.Boolean ForceStepMode() { }
        // RVA: 0x04D85B80  token: 0x600028C
        public virtual System.Int32 RequiredCurvesCount() { }
        // RVA: 0x0AC54900  token: 0x600028D
        public virtual System.Object ConvertToObject(System.Single[] floats) { }
        // RVA: 0x0AC54878  token: 0x600028E
        public virtual System.Single[] ConvertToFloats(System.Object value) { }
        // RVA: 0x0AC54A20  token: 0x600028F
        public virtual System.String GetKeyLabel(System.Single[] floats) { }
        // RVA: 0x0AC54A9C  token: 0x6000290
        public virtual System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats) { }
        // RVA: 0x0AC54960  token: 0x6000291
        public virtual System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info) { }

    }

    // TypeToken: 0x2000055  // size: 0x18
    public sealed struct Animated_Vector2 : Slate.IAnimatedParameterModel
    {
        // Fields
        private System.Action<System.Object,UnityEngine.Vector2> setter;  // 0x10

        // Methods
        // RVA: 0x012081B0  token: 0x6000292
        public virtual System.Boolean ForceStepMode() { }
        // RVA: 0x02BBE540  token: 0x6000293
        public virtual System.Int32 RequiredCurvesCount() { }
        // RVA: 0x0AC55874  token: 0x6000294
        public virtual System.Object ConvertToObject(System.Single[] floats) { }
        // RVA: 0x0AC557D8  token: 0x6000295
        public virtual System.Single[] ConvertToFloats(System.Object value) { }
        // RVA: 0x0AC559B4  token: 0x6000296
        public virtual System.String GetKeyLabel(System.Single[] floats) { }
        // RVA: 0x0AC55A64  token: 0x6000297
        public virtual System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats) { }
        // RVA: 0x0AC558E0  token: 0x6000298
        public virtual System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info) { }

    }

    // TypeToken: 0x2000056  // size: 0x18
    public sealed struct Animated_Vector3 : Slate.IAnimatedParameterModel
    {
        // Fields
        private System.Action<System.Object,UnityEngine.Vector3> setter;  // 0x10

        // Methods
        // RVA: 0x012081B0  token: 0x6000299
        public virtual System.Boolean ForceStepMode() { }
        // RVA: 0x04D8C9E0  token: 0x600029A
        public virtual System.Int32 RequiredCurvesCount() { }
        // RVA: 0x0AC55BD0  token: 0x600029B
        public virtual System.Object ConvertToObject(System.Single[] floats) { }
        // RVA: 0x0AC55B30  token: 0x600029C
        public virtual System.Single[] ConvertToFloats(System.Object value) { }
        // RVA: 0x0AC55D28  token: 0x600029D
        public virtual System.String GetKeyLabel(System.Single[] floats) { }
        // RVA: 0x0AC55E0C  token: 0x600029E
        public virtual System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats) { }
        // RVA: 0x0AC55C50  token: 0x600029F
        public virtual System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info) { }

    }

    // TypeToken: 0x2000057  // size: 0x18
    public sealed struct Animated_Color : Slate.IAnimatedParameterModel
    {
        // Fields
        private System.Action<System.Object,UnityEngine.Color> setter;  // 0x10

        // Methods
        // RVA: 0x012081B0  token: 0x60002A0
        public virtual System.Boolean ForceStepMode() { }
        // RVA: 0x04D882B0  token: 0x60002A1
        public virtual System.Int32 RequiredCurvesCount() { }
        // RVA: 0x0AC544A4  token: 0x60002A2
        public virtual System.Object ConvertToObject(System.Single[] floats) { }
        // RVA: 0x0AC543F0  token: 0x60002A3
        public virtual System.Single[] ConvertToFloats(System.Object value) { }
        // RVA: 0x0AC5462C  token: 0x60002A4
        public virtual System.String GetKeyLabel(System.Single[] floats) { }
        // RVA: 0x0AC54778  token: 0x60002A5
        public virtual System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats) { }
        // RVA: 0x0AC54540  token: 0x60002A6
        public virtual System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info) { }

    }

    // TypeToken: 0x2000058  // size: 0x18
    public sealed struct Animated_Vector4 : Slate.IAnimatedParameterModel
    {
        // Fields
        private System.Action<System.Object,UnityEngine.Vector4> setter;  // 0x10

        // Methods
        // RVA: 0x012081B0  token: 0x60002A7
        public virtual System.Boolean ForceStepMode() { }
        // RVA: 0x04D882B0  token: 0x60002A8
        public virtual System.Int32 RequiredCurvesCount() { }
        // RVA: 0x0AC55FA0  token: 0x60002A9
        public virtual System.Object ConvertToObject(System.Single[] floats) { }
        // RVA: 0x0AC55EEC  token: 0x60002AA
        public virtual System.Single[] ConvertToFloats(System.Object value) { }
        // RVA: 0x0AC56128  token: 0x60002AB
        public virtual System.String GetKeyLabel(System.Single[] floats) { }
        // RVA: 0x0AC562C0  token: 0x60002AC
        public virtual System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats) { }
        // RVA: 0x0AC5603C  token: 0x60002AD
        public virtual System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info) { }

    }

    // TypeToken: 0x2000059  // size: 0x18
    public sealed struct Animated_Quaternion : Slate.IAnimatedParameterModel
    {
        // Fields
        private System.Action<System.Object,UnityEngine.Quaternion> setter;  // 0x10

        // Methods
        // RVA: 0x012081B0  token: 0x60002AE
        public virtual System.Boolean ForceStepMode() { }
        // RVA: 0x04D882B0  token: 0x60002AF
        public virtual System.Int32 RequiredCurvesCount() { }
        // RVA: 0x0AC54EE4  token: 0x60002B0
        public virtual System.Object ConvertToObject(System.Single[] floats) { }
        // RVA: 0x0AC54E30  token: 0x60002B1
        public virtual System.Single[] ConvertToFloats(System.Object value) { }
        // RVA: 0x0AC5506C  token: 0x60002B2
        public virtual System.String GetKeyLabel(System.Single[] floats) { }
        // RVA: 0x0AC55204  token: 0x60002B3
        public virtual System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats) { }
        // RVA: 0x0AC54F80  token: 0x60002B4
        public virtual System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info) { }

    }

    // TypeToken: 0x200005A  // size: 0x18
    public sealed struct Animated_Rect : Slate.IAnimatedParameterModel
    {
        // Fields
        private System.Action<System.Object,UnityEngine.Rect> setter;  // 0x10

        // Methods
        // RVA: 0x012081B0  token: 0x60002B5
        public virtual System.Boolean ForceStepMode() { }
        // RVA: 0x04D882B0  token: 0x60002B6
        public virtual System.Int32 RequiredCurvesCount() { }
        // RVA: 0x0AC553B8  token: 0x60002B7
        public virtual System.Object ConvertToObject(System.Single[] floats) { }
        // RVA: 0x0AC55304  token: 0x60002B8
        public virtual System.Single[] ConvertToFloats(System.Object value) { }
        // RVA: 0x0AC55540  token: 0x60002B9
        public virtual System.String GetKeyLabel(System.Single[] floats) { }
        // RVA: 0x0AC556D8  token: 0x60002BA
        public virtual System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats) { }
        // RVA: 0x0AC55454  token: 0x60002BB
        public virtual System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info) { }

    }

    // TypeToken: 0x200005B  // size: 0x18
    public sealed struct Animated_Bounds : Slate.IAnimatedParameterModel
    {
        // Fields
        private System.Action<System.Object,UnityEngine.Bounds> setter;  // 0x10

        // Methods
        // RVA: 0x012081B0  token: 0x60002BC
        public virtual System.Boolean ForceStepMode() { }
        // RVA: 0x04D9EC60  token: 0x60002BD
        public virtual System.Int32 RequiredCurvesCount() { }
        // RVA: 0x0AC53E10  token: 0x60002BE
        public virtual System.Object ConvertToObject(System.Single[] floats) { }
        // RVA: 0x0AC53CEC  token: 0x60002BF
        public virtual System.Single[] ConvertToFloats(System.Object value) { }
        // RVA: 0x0AC54070  token: 0x60002C0
        public virtual System.String GetKeyLabel(System.Single[] floats) { }
        // RVA: 0x0AC54284  token: 0x60002C1
        public virtual System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats) { }
        // RVA: 0x0AC53F10  token: 0x60002C2
        public virtual System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info) { }

    }

    // TypeToken: 0x200005C  // size: 0x18
    public class AnimationDataCollection : Slate.IAnimatableData
    {
        // Fields
        private System.Collections.Generic.List<Slate.AnimatedParameter> _animatedParameters;  // 0x10

        // Properties
        System.Collections.Generic.List<Slate.AnimatedParameter> animatedParameters { get; /* RVA: 0x02B2ECC0 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC57A90 */ }
        Slate.AnimatedParameter Item { get; /* RVA: 0x0AC57A20 */ }
        Slate.AnimatedParameter Item { get; /* RVA: 0x0AC57A20 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60002C7
        public System.Void .ctor() { }
        // RVA: 0x0AC5799C  token: 0x60002C8
        public System.Void .ctor(Slate.IKeyable keyable, System.Type type, System.String[] memberPaths, System.String transformPath) { }
        // RVA: 0x0AC57594  token: 0x60002C9
        public System.Boolean TryAddParameter(Slate.IKeyable keyable, System.Type type, System.String memberPath, System.String transformPath) { }
        // RVA: 0x0AC570E4  token: 0x60002CA
        public System.Void RemoveParameter(Slate.AnimatedParameter animParam) { }
        // RVA: 0x0AC56E78  token: 0x60002CB
        public System.Void ReOrderParameters() { }
        // RVA: 0x0AC56A98  token: 0x60002CC
        public Slate.AnimatedParameter GetParameterOfName(System.String name) { }
        // RVA: 0x0AC56484  token: 0x60002CD
        public virtual UnityEngine.AnimationCurve[] GetCurves() { }
        // RVA: 0x0AC56478  token: 0x60002CE
        public UnityEngine.AnimationCurve[] GetCurvesAll() { }
        // RVA: 0x0AC56D24  token: 0x60002CF
        private UnityEngine.AnimationCurve[] Internal_GetCurves(System.Boolean enabledParamsOnly) { }
        // RVA: 0x0AC57908  token: 0x60002D0
        public virtual System.Void Validate(Slate.IKeyable keyable) { }
        // RVA: 0x0AC5745C  token: 0x60002D1
        public virtual System.Void SetVirtualTransformParent(UnityEngine.Transform virtualTransformParent) { }
        // RVA: 0x0AC573D8  token: 0x60002D2
        public virtual System.Void SetSnapshot() { }
        // RVA: 0x0AC577D0  token: 0x60002D3
        public virtual System.Boolean TryAutoKey(System.Single time) { }
        // RVA: 0x0AC563C0  token: 0x60002D4
        public virtual System.Void Evaluate(System.Single time, System.Single previousTime, System.Single weight) { }
        // RVA: 0x0AC571BC  token: 0x60002D5
        public virtual System.Void RestoreSnapshot() { }
        // RVA: 0x0AC5788C  token: 0x60002D6
        public virtual System.Boolean TryKeyIdentity(System.Single time) { }
        // RVA: 0x0AC5706C  token: 0x60002D7
        public virtual System.Void RemoveKey(System.Single time) { }
        // RVA: 0x0AC56BFC  token: 0x60002D8
        public virtual System.Boolean HasChanged() { }
        // RVA: 0x0AC56C88  token: 0x60002D9
        public virtual System.Boolean HasKey(System.Single time) { }
        // RVA: 0x0AC56B70  token: 0x60002DA
        public virtual System.Boolean HasAnyKey() { }
        // RVA: 0x0AC57240  token: 0x60002DB
        public virtual System.Void SetKeyCurrent(System.Single time) { }
        // RVA: 0x0AC56640  token: 0x60002DC
        public virtual System.Single GetKeyNext(System.Single time) { }
        // RVA: 0x0AC5686C  token: 0x60002DD
        public virtual System.Single GetKeyPrevious(System.Single time) { }
        // RVA: 0x0AC56490  token: 0x60002DE
        public virtual System.String GetKeyLabel(System.Single time) { }
        // RVA: 0x0AC57348  token: 0x60002DF
        public virtual System.Void SetPreWrapMode(UnityEngine.WrapMode mode) { }
        // RVA: 0x0AC572B8  token: 0x60002E0
        public virtual System.Void SetPostWrapMode(UnityEngine.WrapMode mode) { }
        // RVA: 0x0AC57138  token: 0x60002E1
        public virtual System.Void Reset() { }
        // RVA: 0x0AC574F0  token: 0x60002E2
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000064  // size: 0xA8
    public class Cutscene : UnityEngine.MonoBehaviour, Slate.IDirector
    {
        // Fields
        public static System.Single VERSION_NUMBER;  // const
        private static System.Action<Slate.Cutscene> OnCutsceneStarted;  // static @ 0x0
        private static System.Action<Slate.Cutscene> OnCutsceneStopped;  // static @ 0x8
        private System.Action<Slate.Section> OnSectionReached;  // 0x18
        private System.Action<System.String,System.Object> OnGlobalMessageSend;  // 0x20
        private System.Action OnStop;  // 0x28
        private Slate.Cutscene.UpdateMode _updateMode;  // 0x30
        private Slate.Cutscene.WrapMode _defaultWrapMode;  // 0x34
        private Slate.Cutscene.StopMode _defaultStopMode;  // 0x38
        private System.Single _playbackSpeed;  // 0x3c
        private System.Boolean _playOnStart;  // 0x40
        private System.Boolean _explicitActiveLayers;  // 0x41
        private UnityEngine.LayerMask _activeLayers;  // 0x44
        public System.Collections.Generic.List<Slate.CutsceneGroup> groups;  // 0x48
        private System.Single _length;  // 0x50
        private System.Single _viewTimeMin;  // 0x54
        private System.Single _viewTimeMax;  // 0x58
        private System.Single _currentTime;  // 0x5c
        private System.Single _playTimeMin;  // 0x60
        private System.Single _playTimeMax;  // 0x64
        private UnityEngine.Transform _groupsRoot;  // 0x68
        private System.Collections.Generic.List<Slate.IDirectableTimePointer> timePointers;  // 0x70
        private System.Collections.Generic.List<Slate.IDirectableTimePointer> unsortedStartTimePointers;  // 0x78
        private System.Collections.Generic.Dictionary<UnityEngine.GameObject,System.Boolean> affectedLayerGOStates;  // 0x80
        private static System.Collections.Generic.Dictionary<System.String,Slate.Cutscene> allSceneCutscenes;  // static @ 0x10
        private System.Boolean preInitialized;  // 0x88
        private System.Boolean _isReSampleFrame;  // 0x89
        private System.Boolean <isBakingRootMotion>k__BackingField;  // 0x8a
        private Slate.Cutscene.PlayingDirection <playingDirection>k__BackingField;  // 0x8c
        private Slate.Cutscene.WrapMode <playingWrapMode>k__BackingField;  // 0x90
        private System.Collections.Generic.List<Slate.IDirectable> <directables>k__BackingField;  // 0x98
        private System.Boolean <isActive>k__BackingField;  // 0xa0
        private System.Boolean <isPaused>k__BackingField;  // 0xa1
        private System.Single <previousTime>k__BackingField;  // 0xa4

        // Properties
        UnityEngine.Transform groupsRoot { get; /* RVA: 0x0AC61294 */ }
        Slate.Cutscene.UpdateMode updateMode { get; /* RVA: 0x04D864F0 */ set; /* RVA: 0x04D86740 */ }
        Slate.Cutscene.WrapMode defaultWrapMode { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x04D86760 */ }
        Slate.Cutscene.StopMode defaultStopMode { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }
        System.Boolean playOnStart { get; /* RVA: 0x02D480A0 */ set; /* RVA: 0x02D480B0 */ }
        System.Boolean explicitActiveLayers { get; /* RVA: 0x04D86F40 */ set; /* RVA: 0x04D86F60 */ }
        UnityEngine.LayerMask activeLayers { get; /* RVA: 0x04D86530 */ set; /* RVA: 0x04D86580 */ }
        Slate.DirectorGroup directorGroup { get; /* RVA: 0x0AC610EC */ }
        Slate.ActorGroup actorGroup { get; /* RVA: 0x0AC60E88 */ }
        System.Boolean abortDirectorCamera { get; /* RVA: 0x012081B0 */ }
        System.Boolean multiSelectOverlapCheck { get; /* RVA: 0x012081B0 */ }
        System.Boolean isBakingRootMotion { get; /* RVA: 0x04D890B0 */ set; /* RVA: 0x04DA9BE0 */ }
        Slate.CameraTrack cameraTrack { get; /* RVA: 0x0AC60FB0 */ }
        System.Single currentTime { get; /* RVA: 0x04D91500 */ set; /* RVA: 0x0AC617AC */ }
        System.Single length { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x0AC617D0 */ }
        System.Single viewTimeMin { get; /* RVA: 0x04D86E00 */ set; /* RVA: 0x0AC618B8 */ }
        System.Single viewTimeMax { get; /* RVA: 0x04D88400 */ set; /* RVA: 0x0AC6182C */ }
        System.Single playTimeMin { get; /* RVA: 0x04D88480 */ set; /* RVA: 0x0AC61808 */ }
        System.Single playTimeMax { get; /* RVA: 0x04D88470 */ set; /* RVA: 0x0AC617E0 */ }
        System.Single playbackSpeed { get; /* RVA: 0x04D863E0 */ set; /* RVA: 0x04D86460 */ }
        Slate.Cutscene.PlayingDirection playingDirection { get; /* RVA: 0x04D90C40 */ set; /* RVA: 0x04D90C60 */ }
        Slate.Cutscene.WrapMode playingWrapMode { get; /* RVA: 0x04D86B60 */ set; /* RVA: 0x04D86BC0 */ }
        System.Collections.Generic.List<Slate.IDirectable> directables { get; /* RVA: 0x04D85EE0 */ set; /* RVA: 0x06352D0C */ }
        System.Boolean isActive { get; /* RVA: 0x04D88020 */ set; /* RVA: 0x04D88040 */ }
        System.Boolean isPaused { get; /* RVA: 0x04D88030 */ set; /* RVA: 0x04D88050 */ }
        System.Single previousTime { get; /* RVA: 0x04DA94B0 */ set; /* RVA: 0x04DA9BF0 */ }
        System.Boolean Slate.IDirector.isReSampleFrame { get; /* RVA: 0x04D89060 */ }
        UnityEngine.GameObject Slate.IDirector.context { get; /* RVA: 0x0AC5FE10 */ }
        System.Collections.Generic.IEnumerable<Slate.IDirectable> Slate.IDirector.children { get; /* RVA: 0x0AC5FDD4 */ }
        System.Single remainingTime { get; /* RVA: 0x0AC61448 */ }

        // Events
        event System.Action<Slate.Cutscene> OnCutsceneStarted;
        event System.Action<Slate.Cutscene> OnCutsceneStopped;
        event System.Action<Slate.Section> OnSectionReached;
        event System.Action<System.String,System.Object> OnGlobalMessageSend;
        event System.Action OnStop;

        // Methods
        // RVA: 0x0AC5CA64  token: 0x6000335
        protected System.Void Awake() { }
        // RVA: 0x0AC5FE6C  token: 0x6000336
        protected System.Void Start() { }
        // RVA: 0x0AC5E674  token: 0x6000337
        protected System.Void OnDestroy() { }
        // RVA: 0x0AC5E640  token: 0x6000338
        public System.Void ManualUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC5E5DC  token: 0x6000339
        protected System.Void LateUpdate() { }
        // RVA: 0x0AC5D05C  token: 0x600033A
        protected System.Void FixedUpdate() { }
        // RVA: 0x0AC5CBF4  token: 0x600033B
        private System.Void DirectorSectionReached(Slate.Section section) { }
        // RVA: 0x0AC5D0A4  token: 0x600033C
        public virtual System.Collections.Generic.IEnumerable<UnityEngine.GameObject> GetAffectedActors() { }
        // RVA: 0x0AC5D310  token: 0x600033D
        public System.Single[] GetPointerTimes() { }
        // RVA: 0x0AC5F330  token: 0x600033E
        public virtual System.Void Play() { }
        // RVA: 0x0AC5F09C  token: 0x600033F
        public System.Void Play(System.Action callback) { }
        // RVA: 0x0AC5F0AC  token: 0x6000340
        public System.Void Play(System.Single startTime) { }
        // RVA: 0x0AC5EE94  token: 0x6000341
        public System.Void Play(System.Single startTime, System.Action callback) { }
        // RVA: 0x0AC5F0D4  token: 0x6000342
        public System.Void Play(System.Single startTime, System.Single endTime, Slate.Cutscene.WrapMode wrapMode, System.Action callback, Slate.Cutscene.PlayingDirection playDirection) { }
        // RVA: 0x0AC60044  token: 0x6000343
        public virtual System.Void Stop() { }
        // RVA: 0x0AC5FF18  token: 0x6000344
        public System.Void Stop(Slate.Cutscene.StopMode stopMode) { }
        // RVA: 0x0AC5EC8C  token: 0x6000345
        public System.Void PlayReverse() { }
        // RVA: 0x0AC5EC9C  token: 0x6000346
        public System.Void PlayReverse(System.Single startTime, System.Single endTime) { }
        // RVA: 0x04DA9BC0  token: 0x6000347
        public virtual System.Void Pause() { }
        // RVA: 0x04DA9BD0  token: 0x6000348
        public System.Void Resume() { }
        // RVA: 0x0AC5FD14  token: 0x6000349
        public System.Void SkipAll() { }
        // RVA: 0x0AC5F694  token: 0x600034A
        public System.Void Rewind() { }
        // RVA: 0x0AC5F650  token: 0x600034B
        public System.Void RewindNoUndo() { }
        // RVA: 0x04DA9BB0  token: 0x600034C
        public System.Void BreakCutsceneLoop() { }
        // RVA: 0x0AC5CB9C  token: 0x600034D
        public System.Void BreakSectionLoop(System.Boolean alsoSkip) { }
        // RVA: 0x0AC5FDCC  token: 0x600034E
        public System.Void Skip() { }
        // RVA: 0x0AC5FD40  token: 0x600034F
        public System.Void SkipCurrentSection() { }
        // RVA: 0x0AC5E5B8  token: 0x6000350
        public System.Boolean JumpToSection(System.String name) { }
        // RVA: 0x0AC5E530  token: 0x6000351
        public System.Boolean JumpToSection(Slate.Section section) { }
        // RVA: 0x0AC5EB48  token: 0x6000352
        public System.Boolean PlayFromSection(System.String name) { }
        // RVA: 0x0AC5EB88  token: 0x6000353
        public System.Boolean PlayFromSection(System.String name, Slate.Cutscene.WrapMode wrap, System.Action callback) { }
        // RVA: 0x0AC5EB68  token: 0x6000354
        public System.Boolean PlayFromSection(Slate.Section section) { }
        // RVA: 0x0AC5EBD4  token: 0x6000355
        public System.Boolean PlayFromSection(Slate.Section section, Slate.Cutscene.WrapMode wrap, System.Action callback) { }
        // RVA: 0x0AC5EE14  token: 0x6000356
        public System.Boolean PlaySection(System.String name) { }
        // RVA: 0x0AC5EDA8  token: 0x6000357
        public System.Boolean PlaySection(System.String name, Slate.Cutscene.WrapMode wrap, System.Action callback) { }
        // RVA: 0x0AC5EDF4  token: 0x6000358
        public System.Boolean PlaySection(Slate.Section section) { }
        // RVA: 0x0AC5ECC4  token: 0x6000359
        public System.Boolean PlaySection(Slate.Section section, Slate.Cutscene.WrapMode wrap, System.Action callback) { }
        // RVA: 0x0AC5F6C4  token: 0x600035A
        public System.Void Sample() { }
        // RVA: 0x0AC5F6D4  token: 0x600035B
        public virtual System.Void Sample(System.Single time) { }
        // RVA: 0x0AC5E138  token: 0x600035C
        private System.Void Internal_SamplePointers(System.Single currentTime, System.Single previousTime) { }
        // RVA: 0x0AC5F35C  token: 0x600035D
        public virtual System.Void ReSample() { }
        // RVA: 0x0AC60238  token: 0x600035E
        public virtual System.Void Validate() { }
        // RVA: 0x0AC5D6B4  token: 0x600035F
        private System.Void InitializeTimePointers() { }
        // RVA: 0x0AC5E930  token: 0x6000360
        private System.Void OnSampleStarted() { }
        // RVA: 0x0AC5E770  token: 0x6000361
        private System.Void OnSampleEnded() { }
        // RVA: 0x0AC5FBC4  token: 0x6000362
        private System.Void SetLayersActive() { }
        // RVA: 0x0AC5F4C4  token: 0x6000363
        private System.Void RestoreLayersActive() { }
        // RVA: 0x0AC60098  token: 0x6000364
        private System.Void UpdateCutscene(System.Single delta) { }
        // RVA: 0x0AC5EE48  token: 0x6000365
        public static Slate.Cutscene Play(System.String name) { }
        // RVA: 0x0AC5EEBC  token: 0x6000366
        public static Slate.Cutscene Play(System.String name, System.Action callback) { }
        // RVA: 0x0AC5CE48  token: 0x6000367
        public static Slate.Cutscene FindFromResources(System.String name) { }
        // RVA: 0x0AC5CF90  token: 0x6000368
        public static Slate.Cutscene Find(System.String name) { }
        // RVA: 0x0AC5FE80  token: 0x6000369
        public static System.Void StopAllCutscenes() { }
        // RVA: 0x0AC5F7E4  token: 0x600036A
        public virtual System.Void SendGlobalMessage(System.String message, System.Object value) { }
        // RVA: 0x0AC5F9E0  token: 0x600036B
        public System.Void SetGroupActorOfName(System.String groupName, UnityEngine.GameObject newActor) { }
        // RVA: -1  // generic def  token: 0x600036C
        public T FindElement(System.String path) { }
        // RVA: 0x0AC5CCC4  token: 0x600036D
        public Slate.IDirectable FindElement(System.String path) { }
        // RVA: 0x0AC60050  token: 0x600036E
        public virtual System.String ToString() { }
        // RVA: 0x0AC5D444  token: 0x600036F
        public Slate.Section GetSectionByName(System.String name) { }
        // RVA: 0x0AC5D474  token: 0x6000370
        public Slate.Section GetSectionByUID(System.String UID) { }
        // RVA: 0x0AC5D660  token: 0x6000371
        public Slate.Section[] GetSections() { }
        // RVA: 0x0AC5D4A4  token: 0x6000372
        public System.Single GetSectionLength(System.String name) { }
        // RVA: 0x0AC5D528  token: 0x6000373
        public System.String[] GetSectionNames() { }
        // RVA: 0x0AC5D1CC  token: 0x6000374
        public System.String[] GetDefinedEventNames() { }
        // RVA: 0x0AC5F33C  token: 0x6000375
        public System.Void PreInitialize() { }
        // RVA: 0x0AC5F3CC  token: 0x6000376
        public System.Void RenderCutscene(System.Int32 width, System.Int32 height, System.Int32 frameRate, System.Action<UnityEngine.Texture2D[]> callback) { }
        // RVA: 0x0AC5E09C  token: 0x6000377
        private System.Collections.IEnumerator Internal_RenderCutscene(System.Int32 width, System.Int32 height, System.Int32 frameRate, System.Action<UnityEngine.Texture2D[]> callback) { }
        // RVA: 0x0AC60AB8  token: 0x6000378
        public System.Void .ctor() { }
        // RVA: 0x0AC60A24  token: 0x6000379
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006E  // size: 0x70
    public abstract class CutsceneGroup : UnityEngine.MonoBehaviour, Slate.IDirectable
    {
        // Fields
        private System.Action<Slate.Section> OnSectionReached;  // 0x18
        private System.Collections.Generic.List<Slate.CutsceneTrack> _tracks;  // 0x20
        private System.Collections.Generic.List<Slate.Section> _sections;  // 0x28
        private System.Boolean _isCollapsed;  // 0x30
        private System.Boolean _active;  // 0x31
        private System.Boolean _isLocked;  // 0x32
        private System.Boolean _isReadOnly;  // 0x33
        private Slate.TransformSnapshot transformSnapshot;  // 0x38
        private Slate.ObjectSnapshot objectSnapshot;  // 0x40
        private UnityEngine.GameObject refDuplicateActor;  // 0x48
        private static System.Int32 totalCustomToggleNum;  // static @ 0x0
        public System.Collections.Generic.List<Slate.CutsceneGroup.CustomToggleInfo> customToggle;  // 0x50
        private System.Boolean m_customToggleValid;  // 0x58
        private System.Boolean m_customToggleCheckDirty;  // 0x59
        public System.Collections.Generic.Dictionary<System.Int32,Slate.CutsceneGroup.CustomToggleInfo> customInfoCache;  // 0x60
        private Slate.IDirector <root>k__BackingField;  // 0x68

        // Properties
        System.String name { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        UnityEngine.GameObject actor { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        Slate.CutsceneGroup.ActorReferenceMode referenceMode { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        Slate.CutsceneGroup.ActorInitialTransformation initialTransformation { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        UnityEngine.Vector3 initialLocalPosition { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        UnityEngine.Vector3 initialLocalRotation { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean displayVirtualMeshGizmo { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean needActor { get; /* RVA: 0x02FFF600 */ }
        System.Int32 updateOrder { get; /* RVA: 0x04DA3400 */ }
        System.Boolean customToggleValid { get; /* RVA: 0x0AC6E080 */ }
        System.Collections.Generic.List<Slate.CutsceneTrack> tracks { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Collections.Generic.List<Slate.Section> sections { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        System.Collections.Generic.IEnumerable<Slate.IDirectable> Slate.IDirectable.children { get; /* RVA: 0x0AC6D04C */ }
        System.Single Slate.IDirectable.startTime { get; /* RVA: 0x04D879D0 */ }
        System.Single Slate.IDirectable.endTime { get; /* RVA: 0x0AC6D088 */ }
        System.Single Slate.IDirectable.blendIn { get; /* RVA: 0x04D879D0 */ }
        System.Single Slate.IDirectable.blendOut { get; /* RVA: 0x04D879D0 */ }
        System.Boolean Slate.IDirectable.canCrossBlend { get; /* RVA: 0x012081B0 */ }
        Slate.IDirectable Slate.IDirectable.parent { get; /* RVA: 0x011EC580 */ }
        Slate.IDirector root { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }
        System.Boolean isActive { get; /* RVA: 0x04D86AB0 */ set; /* RVA: 0x0AC6E188 */ }
        System.Boolean isCollapsed { get; /* RVA: 0x04D865F0 */ set; /* RVA: 0x04D86620 */ }
        System.Boolean isLocked { get; /* RVA: 0x0AC6E0EC */ set; /* RVA: 0x04D86DD0 */ }
        System.Boolean isReadOnly { get; /* RVA: 0x04DA9CA0 */ set; /* RVA: 0x04DA9CB0 */ }

        // Events
        event System.Action<Slate.Section> OnSectionReached;

        // Methods
        // RVA: 0x0AC6CC90  token: 0x60003A2
        public System.Void SetCustomToggleDirty() { }
        // RVA: 0x0AC6D604  token: 0x60003A4
        private System.Void _OnCustomToggleValueChanged() { }
        // RVA: 0x0AC6C4BC  token: 0x60003A5
        public System.Boolean CheckCustomCollapsed(System.Int32 index) { }
        // RVA: 0x0AC6D760  token: 0x60003A6
        private System.Void _TryRefreshCustomToggleInfo() { }
        // RVA: 0x0AC6D190  token: 0x60003BC
        public virtual System.String ToString() { }
        // RVA: 0x0AC6D310  token: 0x60003BD
        public virtual System.Void Validate(Slate.IDirector root, Slate.IDirectable parent) { }
        // RVA: 0x0AC6C7E4  token: 0x60003BE
        public Slate.Section GetSectionByName(System.String name) { }
        // RVA: 0x0AC6C934  token: 0x60003BF
        public Slate.Section GetSectionByUID(System.String UID) { }
        // RVA: 0x0AC6C64C  token: 0x60003C0
        public Slate.Section GetSectionAfter(System.Single time) { }
        // RVA: 0x0AC6C718  token: 0x60003C1
        public Slate.Section GetSectionBefore(System.Single time) { }
        // RVA: 0x0AC6CD70  token: 0x60003C2
        private virtual System.Boolean Slate.IDirectable.Initialize() { }
        // RVA: 0x0AC6CC9C  token: 0x60003C3
        private virtual System.Void Slate.IDirectable.Enter() { }
        // RVA: 0x0AC6CEB4  token: 0x60003C4
        private virtual System.Void Slate.IDirectable.Reverse() { }
        // RVA: 0x0AC6CF14  token: 0x60003C5
        private virtual System.Void Slate.IDirectable.Update(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC6CD10  token: 0x60003C6
        private virtual System.Void Slate.IDirectable.Exit() { }
        // RVA: 0x0AC6CE54  token: 0x60003C7
        private virtual System.Void Slate.IDirectable.ReverseEnter() { }
        // RVA: 0x041E1670  token: 0x60003C8
        private virtual System.Void Slate.IDirectable.RootEnabled() { }
        // RVA: 0x041E1670  token: 0x60003C9
        private virtual System.Void Slate.IDirectable.RootDisabled() { }
        // RVA: 0x041E1670  token: 0x60003CA
        private virtual System.Void Slate.IDirectable.RootUpdated(System.Single time, System.Single previousTime) { }
        // RVA: 0x041E1670  token: 0x60003CB
        private virtual System.Void Slate.IDirectable.RootDestroyed() { }
        // RVA: 0x0AC6D0A4  token: 0x60003CC
        private System.Void StoreActorState() { }
        // RVA: 0x0AC6CB14  token: 0x60003CD
        private System.Void RestoreActorState() { }
        // RVA: 0x0AC6D1A0  token: 0x60003CE
        private System.Void TryInstantiateLocalActor() { }
        // RVA: 0x0AC6CA04  token: 0x60003CF
        private System.Void ReleaseLocalActorInstance() { }
        // RVA: 0x0AC6CB64  token: 0x60003D0
        private System.Void SetActorLocalCoords() { }
        // RVA: 0x0AC6CAA8  token: 0x60003D1
        protected UnityEngine.GameObject ResolveActor(UnityEngine.GameObject original) { }
        // RVA: 0x0AC6DE44  token: 0x60003D2
        protected System.Void .ctor() { }
        // RVA: 0x0AC6DE08  token: 0x60003D3
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000077  // size: 0x50
    public abstract class CutsceneTrack : UnityEngine.MonoBehaviour, Slate.IDirectable
    {
        // Fields
        private System.String _name;  // 0x18
        private UnityEngine.Color _color;  // 0x20
        private System.Boolean _active;  // 0x30
        private System.Boolean _isLocked;  // 0x31
        private System.Collections.Generic.List<Slate.ActionClip> _actionClips;  // 0x38
        private System.Boolean _isReadOnly;  // 0x40
        private System.Int32 <layerOrder>k__BackingField;  // 0x44
        private Slate.IDirectable <parent>k__BackingField;  // 0x48

        // Properties
        UnityEngine.GameObject actor { get; /* RVA: 0x0AC6ECE0 */ }
        System.String name { get; /* RVA: 0x0AC6EE48 */ set; /* RVA: 0x0AC6EF2C */ }
        UnityEngine.Color color { get; /* RVA: 0x0AC6ED08 */ }
        System.Collections.Generic.List<Slate.ActionClip> clips { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        System.String info { get; /* RVA: 0x0AC6ED60 */ }
        System.Collections.Generic.IEnumerable<Slate.IDirectable> Slate.IDirectable.children { get; /* RVA: 0x0AC6E92C */ }
        System.Int32 layerOrder { get; /* RVA: 0x04D86530 */ set; /* RVA: 0x04D86580 */ }
        Slate.IDirector root { get; /* RVA: 0x0AC6EEAC */ }
        Slate.IDirectable parent { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        System.Boolean isCollapsed { get; /* RVA: 0x0AC6EDC4 */ }
        System.Boolean isActive { get; /* RVA: 0x0AC6ED98 */ set; /* RVA: 0x0AC6EEEC */ }
        System.Boolean isLocked { get; /* RVA: 0x0AC6EDE4 */ set; /* RVA: 0x04D86AC0 */ }
        System.Single startTime { get; /* RVA: 0x0AC6EECC */ set; /* RVA: 0x041E1670 */ }
        System.Single endTime { get; /* RVA: 0x0AC6ED40 */ set; /* RVA: 0x041E1670 */ }
        System.Single blendIn { get; /* RVA: 0x04D879D0 */ set; /* RVA: 0x041E1670 */ }
        System.Single blendOut { get; /* RVA: 0x04D879D0 */ set; /* RVA: 0x041E1670 */ }
        System.Boolean canCrossBlend { get; /* RVA: 0x012081B0 */ }
        System.Boolean needActor { get; /* RVA: 0x0AC6EE8C */ }
        System.Int32 updateOrder { get; /* RVA: 0x04DA3400 */ }
        System.Boolean isReadOnly { get; /* RVA: 0x0AC6EE18 */ set; /* RVA: 0x02D480B0 */ }
        System.Boolean canDelete { get; /* RVA: 0x0AC6ED00 */ }

        // Methods
        // RVA: 0x0AC6E8F8  token: 0x6000408
        private virtual System.Boolean Slate.IDirectable.Initialize() { }
        // RVA: 0x0AC6E8D8  token: 0x6000409
        private virtual System.Void Slate.IDirectable.Enter() { }
        // RVA: 0x0AC6E918  token: 0x600040A
        private virtual System.Void Slate.IDirectable.Update(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC6E8E8  token: 0x600040B
        private virtual System.Void Slate.IDirectable.Exit() { }
        // RVA: 0x0AC6E908  token: 0x600040C
        private virtual System.Void Slate.IDirectable.ReverseEnter() { }
        // RVA: 0x085D1220  token: 0x600040D
        private virtual System.Void Slate.IDirectable.Reverse() { }
        // RVA: 0x041E1670  token: 0x600040E
        private virtual System.Void Slate.IDirectable.RootEnabled() { }
        // RVA: 0x041E1670  token: 0x600040F
        private virtual System.Void Slate.IDirectable.RootDisabled() { }
        // RVA: 0x041E1670  token: 0x6000410
        private virtual System.Void Slate.IDirectable.RootUpdated(System.Single time, System.Single previousTime) { }
        // RVA: 0x041E1670  token: 0x6000411
        private virtual System.Void Slate.IDirectable.RootDestroyed() { }
        // RVA: 0x0AC6E8B0  token: 0x6000412
        public System.Void PostCreate(Slate.IDirectable parent) { }
        // RVA: 0x0AC6E9EC  token: 0x6000413
        public virtual System.Void Validate(Slate.IDirector root, Slate.IDirectable parent) { }
        // RVA: 0x041E1670  token: 0x6000414
        protected virtual System.Void OnCreate() { }
        // RVA: 0x041E1670  token: 0x6000415
        protected virtual System.Void OnAfterValidate() { }
        // RVA: 0x02FFF600  token: 0x6000416
        protected virtual System.Boolean OnInitialize() { }
        // RVA: 0x041E1670  token: 0x6000417
        protected virtual System.Void OnEnter() { }
        // RVA: 0x041E1670  token: 0x6000418
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x041E1670  token: 0x6000419
        protected virtual System.Void OnExit() { }
        // RVA: 0x041E1670  token: 0x600041A
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x041E1670  token: 0x600041B
        protected virtual System.Void OnReverse() { }
        // RVA: 0x041E1670  token: 0x600041C
        protected virtual System.Void OnDrawGizmosSelected() { }
        // RVA: 0x041E1670  token: 0x600041D
        protected virtual System.Void OnSceneGUI() { }
        // RVA: 0x041E1670  token: 0x600041E
        protected virtual System.Void OnRootEnabled() { }
        // RVA: 0x041E1670  token: 0x600041F
        protected virtual System.Void OnRootDisabled() { }
        // RVA: 0x041E1670  token: 0x6000420
        protected virtual System.Void OnRootUpdated(System.Single time, System.Single previousTime) { }
        // RVA: 0x041E1670  token: 0x6000421
        protected virtual System.Void OnRootDestroyed() { }
        // RVA: 0x0AC6E818  token: 0x6000422
        public System.Single GetTrackWeight() { }
        // RVA: 0x0AC6E7BC  token: 0x6000423
        public System.Single GetTrackWeight(System.Single time) { }
        // RVA: 0x0AC4FEA8  token: 0x6000424
        public System.Single GetTrackWeight(System.Single time, System.Single blendInOut) { }
        // RVA: 0x0AC4FEC4  token: 0x6000425
        public System.Single GetTrackWeight(System.Single time, System.Single blendIn, System.Single blendOut) { }
        // RVA: -1  // generic def  token: 0x6000426
        public T AddAction(System.Single time) { }
        // RVA: 0x0AC6E4DC  token: 0x6000427
        public Slate.ActionClip AddAction(System.Type type, System.Single time) { }
        // RVA: 0x0AC6EC48  token: 0x6000428
        protected System.Void .ctor() { }
        // RVA: 0x0AC6E968  token: 0x6000429
        private System.Boolean <Validate>b__72_1(Slate.IDirectable t) { }

    }

    // TypeToken: 0x200007A
    public interface IAnimatableData
    {
        // Properties
        System.Boolean isValid { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000430
        public virtual UnityEngine.AnimationCurve[] GetCurves() { }
        // RVA: -1  // abstract  token: 0x6000431
        public virtual System.Void Validate(Slate.IKeyable keyable) { }
        // RVA: -1  // abstract  token: 0x6000432
        public virtual System.Void SetVirtualTransformParent(UnityEngine.Transform virtualTransformParent) { }
        // RVA: -1  // abstract  token: 0x6000433
        public virtual System.Void SetSnapshot() { }
        // RVA: -1  // abstract  token: 0x6000434
        public virtual System.Void Evaluate(System.Single time, System.Single previousTime, System.Single weight) { }
        // RVA: -1  // abstract  token: 0x6000435
        public virtual System.Void RestoreSnapshot() { }
        // RVA: -1  // abstract  token: 0x6000436
        public virtual System.Void SetKeyCurrent(System.Single time) { }
        // RVA: -1  // abstract  token: 0x6000437
        public virtual System.Boolean TryAutoKey(System.Single time) { }
        // RVA: -1  // abstract  token: 0x6000438
        public virtual System.Boolean TryKeyIdentity(System.Single time) { }
        // RVA: -1  // abstract  token: 0x6000439
        public virtual System.Void RemoveKey(System.Single time) { }
        // RVA: -1  // abstract  token: 0x600043A
        public virtual System.Boolean HasKey(System.Single time) { }
        // RVA: -1  // abstract  token: 0x600043B
        public virtual System.Boolean HasAnyKey() { }
        // RVA: -1  // abstract  token: 0x600043C
        public virtual System.Boolean HasChanged() { }
        // RVA: -1  // abstract  token: 0x600043D
        public virtual System.Single GetKeyNext(System.Single time) { }
        // RVA: -1  // abstract  token: 0x600043E
        public virtual System.Single GetKeyPrevious(System.Single time) { }
        // RVA: -1  // abstract  token: 0x600043F
        public virtual System.String GetKeyLabel(System.Single time) { }
        // RVA: -1  // abstract  token: 0x6000440
        public virtual System.Void SetPreWrapMode(UnityEngine.WrapMode mode) { }
        // RVA: -1  // abstract  token: 0x6000441
        public virtual System.Void SetPostWrapMode(UnityEngine.WrapMode mode) { }
        // RVA: -1  // abstract  token: 0x6000442
        public virtual System.Void Reset() { }

    }

    // TypeToken: 0x200007B
    public interface IDirectable
    {
        // Properties
        Slate.IDirector root { get; /* RVA: -1  // abstract */ }
        Slate.IDirectable parent { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.IEnumerable<Slate.IDirectable> children { get; /* RVA: -1  // abstract */ }
        UnityEngine.GameObject actor { get; /* RVA: -1  // abstract */ }
        System.String name { get; /* RVA: -1  // abstract */ }
        System.Boolean isActive { get; /* RVA: -1  // abstract */ }
        System.Boolean isCollapsed { get; /* RVA: -1  // abstract */ }
        System.Boolean isLocked { get; /* RVA: -1  // abstract */ }
        System.Single startTime { get; /* RVA: -1  // abstract */ }
        System.Single endTime { get; /* RVA: -1  // abstract */ }
        System.Single blendIn { get; /* RVA: -1  // abstract */ }
        System.Single blendOut { get; /* RVA: -1  // abstract */ }
        System.Boolean canCrossBlend { get; /* RVA: -1  // abstract */ }
        System.Boolean needActor { get; /* RVA: -1  // abstract */ }
        System.Int32 updateOrder { get; /* RVA: -1  // abstract */ }
        System.Boolean isReadOnly { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000450
        public virtual System.Void Validate(Slate.IDirector root, Slate.IDirectable parent) { }
        // RVA: -1  // abstract  token: 0x6000451
        public virtual System.Boolean Initialize() { }
        // RVA: -1  // abstract  token: 0x6000452
        public virtual System.Void Enter() { }
        // RVA: -1  // abstract  token: 0x6000453
        public virtual System.Void Exit() { }
        // RVA: -1  // abstract  token: 0x6000454
        public virtual System.Void Update(System.Single time, System.Single previousTime) { }
        // RVA: -1  // abstract  token: 0x6000455
        public virtual System.Void ReverseEnter() { }
        // RVA: -1  // abstract  token: 0x6000456
        public virtual System.Void Reverse() { }
        // RVA: -1  // abstract  token: 0x6000457
        public virtual System.Void RootEnabled() { }
        // RVA: -1  // abstract  token: 0x6000458
        public virtual System.Void RootUpdated(System.Single time, System.Single previousTime) { }
        // RVA: -1  // abstract  token: 0x6000459
        public virtual System.Void RootDisabled() { }
        // RVA: -1  // abstract  token: 0x600045A
        public virtual System.Void RootDestroyed() { }

    }

    // TypeToken: 0x200007C
    public interface IKeyable : Slate.IDirectable
    {
        // Properties
        Slate.AnimationDataCollection animationData { get; /* RVA: -1  // abstract */ }
        System.Object animatedParametersTarget { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x200007D
    public interface ISubClipContainable : Slate.IDirectable
    {
        // Properties
        System.Single subClipOffset { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Single subClipSpeed { get; /* RVA: -1  // abstract */ }
        System.Single subClipLength { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x200007E  // size: 0x10
    public static class IDirectableExtensions
    {
        // Methods
        // RVA: 0x0AC71500  token: 0x6000464
        public static System.Single GetLength(Slate.IDirectable directable) { }
        // RVA: 0x0AC71D54  token: 0x6000465
        public static System.Single RootTimeToLocalTime(Slate.IDirectable directable) { }
        // RVA: 0x0AC71D18  token: 0x6000466
        public static System.Single RootTimeToLocalTimeUnclamped(Slate.IDirectable directable) { }
        // RVA: 0x0AC71C90  token: 0x6000467
        public static System.Boolean IsRootTimeWithinClip(Slate.IDirectable directable) { }
        // RVA: 0x0AC71DC0  token: 0x6000468
        public static System.Single ToLocalTime(Slate.IDirectable directable, System.Single time) { }
        // RVA: 0x0AC71D90  token: 0x6000469
        public static System.Single ToLocalTimeUnclamped(Slate.IDirectable directable, System.Single time) { }
        // RVA: 0x0AC71CCC  token: 0x600046A
        public static System.Boolean IsTimeWithinClip(Slate.IDirectable directable, System.Single time) { }
        // RVA: 0x0AC71218  token: 0x600046B
        public static System.Boolean CanCrossBlend(Slate.IDirectable directable, Slate.IDirectable other) { }
        // RVA: 0x0AC71028  token: 0x600046C
        public static System.Boolean CanBlendIn(Slate.IDirectable directable) { }
        // RVA: 0x0AC71120  token: 0x600046D
        public static System.Boolean CanBlendOut(Slate.IDirectable directable) { }
        // RVA: 0x0AC712A4  token: 0x600046E
        public static System.Boolean CanScale(Slate.IDirectable directable) { }
        // RVA: 0x0AC72004  token: 0x600046F
        public static System.Boolean WithinBufferTriggerRange(Slate.IDirectable directable, System.Single time, System.Single previousTime, System.Boolean bypass) { }
        // RVA: 0x0AC7138C  token: 0x6000470
        public static Slate.IDirectable FindChild(Slate.IDirectable directable, System.String name) { }
        // RVA: -1  // generic def  token: 0x6000471
        public static T GetPreviousSibling(Slate.IDirectable directable) { }
        // RVA: 0x0AC71768  token: 0x6000472
        public static Slate.IDirectable GetPreviousSibling(Slate.IDirectable directable) { }
        // RVA: -1  // generic def  token: 0x6000473
        public static T GetNextSibling(Slate.IDirectable directable) { }
        // RVA: 0x0AC715DC  token: 0x6000474
        public static Slate.IDirectable GetNextSibling(Slate.IDirectable directable) { }
        // RVA: -1  // generic def  token: 0x6000475
        public static T GetFirstParentOfType(Slate.IDirectable directable) { }
        // RVA: 0x0AC71A94  token: 0x6000476
        public static System.Single GetWeight(Slate.IDirectable directable) { }
        // RVA: 0x0AC719A8  token: 0x6000477
        public static System.Single GetWeight(Slate.IDirectable directable, System.Single time) { }
        // RVA: 0x0AC4FEA8  token: 0x6000478
        public static System.Single GetWeight(Slate.IDirectable directable, System.Single time, System.Single blendInOut) { }
        // RVA: 0x0AC71A00  token: 0x6000479
        public static System.Single GetWeight(Slate.IDirectable directable, System.Single time, System.Single blendIn, System.Single blendOut) { }
        // RVA: 0x0AC71864  token: 0x600047A
        public static UnityEngine.Transform GetSpaceTransform(Slate.IDirectable directable, Slate.TransformSpace space, UnityEngine.GameObject actorOverride) { }
        // RVA: 0x0AC71E20  token: 0x600047B
        public static UnityEngine.Vector3 TransformPosition(Slate.IDirectable directable, UnityEngine.Vector3 point, Slate.TransformSpace space) { }
        // RVA: 0x0AC71AB8  token: 0x600047C
        public static UnityEngine.Vector3 InverseTransformPosition(Slate.IDirectable directable, UnityEngine.Vector3 point, Slate.TransformSpace space) { }
        // RVA: 0x0AC71EEC  token: 0x600047D
        public static UnityEngine.Quaternion TransformRotation(Slate.IDirectable directable, UnityEngine.Vector3 euler, Slate.TransformSpace space) { }
        // RVA: 0x0AC71B84  token: 0x600047E
        public static UnityEngine.Vector3 InverseTransformRotation(Slate.IDirectable directable, UnityEngine.Quaternion rot, Slate.TransformSpace space) { }
        // RVA: 0x0AC70EF4  token: 0x600047F
        public static UnityEngine.Vector3 ActorPositionInSpace(Slate.IDirectable directable, Slate.TransformSpace space) { }
        // RVA: 0x0AC716D8  token: 0x6000480
        public static System.Single GetPreviousLoopLocalTime(Slate.ISubClipContainable clip) { }
        // RVA: 0x0AC7153C  token: 0x6000481
        public static System.Single GetNextLoopLocalTime(Slate.ISubClipContainable clip) { }
        // RVA: 0x0AC71474  token: 0x6000482
        public static UnityEngine.AnimationCurve[] GetCurvesAll(Slate.IKeyable keyable) { }

    }

    // TypeToken: 0x2000082
    public interface IDirector
    {
        // Properties
        System.Collections.Generic.IEnumerable<Slate.IDirectable> children { get; /* RVA: -1  // abstract */ }
        UnityEngine.GameObject context { get; /* RVA: -1  // abstract */ }
        System.Single length { get; /* RVA: -1  // abstract */ }
        System.Single currentTime { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Single previousTime { get; /* RVA: -1  // abstract */ }
        System.Single playbackSpeed { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean isActive { get; /* RVA: -1  // abstract */ }
        System.Boolean isPaused { get; /* RVA: -1  // abstract */ }
        System.Boolean isReSampleFrame { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000494
        public virtual System.Collections.Generic.IEnumerable<UnityEngine.GameObject> GetAffectedActors() { }
        // RVA: -1  // abstract  token: 0x6000495
        public virtual System.Void Play() { }
        // RVA: -1  // abstract  token: 0x6000496
        public virtual System.Void Pause() { }
        // RVA: -1  // abstract  token: 0x6000497
        public virtual System.Void Stop() { }
        // RVA: -1  // abstract  token: 0x6000498
        public virtual System.Void Sample(System.Single time) { }
        // RVA: -1  // abstract  token: 0x6000499
        public virtual System.Void ReSample() { }
        // RVA: -1  // abstract  token: 0x600049A
        public virtual System.Void Validate() { }
        // RVA: -1  // abstract  token: 0x600049B
        public virtual System.Void SendGlobalMessage(System.String message, System.Object value) { }

    }

    // TypeToken: 0x2000083  // size: 0x48
    public class Section
    {
        // Fields
        public static readonly UnityEngine.Color DEFAULT_COLOR;  // static @ 0x0
        private System.String _UID;  // 0x10
        private System.String _name;  // 0x18
        private System.Single _time;  // 0x20
        private Slate.Section.ExitMode _exitMode;  // 0x24
        private System.Int32 _loopCount;  // 0x28
        private UnityEngine.Color _color;  // 0x2c
        private System.Boolean _colorizeBackground;  // 0x3c
        private System.Int32 <currentLoopIteration>k__BackingField;  // 0x40

        // Properties
        System.Int32 currentLoopIteration { get; /* RVA: 0x04D865E0 */ set; /* RVA: 0x04D86610 */ }
        System.String UID { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String name { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Single time { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x011EC9E0 */ }
        Slate.Section.ExitMode exitMode { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        System.Int32 loopCount { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        UnityEngine.Color color { get; /* RVA: 0x0AC77064 */ set; /* RVA: 0x04D8D3A0 */ }
        System.Boolean colorizeBackground { get; /* RVA: 0x04D86BD0 */ set; /* RVA: 0x04D86BE0 */ }

        // Methods
        // RVA: 0x0AC76FB4  token: 0x60004AC
        public System.Void .ctor(System.String name, System.Single time) { }
        // RVA: 0x04DA9D20  token: 0x60004AD
        public System.Void ResetLoops() { }
        // RVA: 0x04DA9D10  token: 0x60004AE
        public System.Void BreakLoop() { }
        // RVA: 0x0AC76F28  token: 0x60004AF
        public System.Boolean TryUpdateLoop() { }
        // RVA: 0x0AC76EB4  token: 0x60004B0
        public virtual System.String ToString() { }
        // RVA: 0x0AC76F48  token: 0x60004B1
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000085
    public interface IDirectableTimePointer
    {
        // Properties
        Slate.IDirectable target { get; /* RVA: -1  // abstract */ }
        System.Single time { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60004B4
        public virtual System.Void TriggerForward(System.Single currentTime, System.Single previousTime) { }
        // RVA: -1  // abstract  token: 0x60004B5
        public virtual System.Void TriggerBackward(System.Single currentTime, System.Single previousTime) { }
        // RVA: -1  // abstract  token: 0x60004B6
        public virtual System.Void Update(System.Single currentTime, System.Single previousTime) { }

    }

    // TypeToken: 0x2000086  // size: 0x20
    public sealed struct StartTimePointer : Slate.IDirectableTimePointer
    {
        // Fields
        private System.Boolean triggered;  // 0x10
        private System.Single lastTargetStartTime;  // 0x14
        private Slate.IDirectable <target>k__BackingField;  // 0x18

        // Properties
        Slate.IDirectable target { get; /* RVA: 0x04D88320 */ set; /* RVA: 0x067D5E9C */ }
        System.Single Slate.IDirectableTimePointer.time { get; /* RVA: 0x0AC78388 */ }

        // Methods
        // RVA: 0x0AC783A4  token: 0x60004BA
        public System.Void .ctor(Slate.IDirectable target) { }
        // RVA: 0x0AC781BC  token: 0x60004BB
        private virtual System.Void Slate.IDirectableTimePointer.TriggerForward(System.Single currentTime, System.Single previousTime) { }
        // RVA: 0x0AC78238  token: 0x60004BC
        private virtual System.Void Slate.IDirectableTimePointer.Update(System.Single currentTime, System.Single previousTime) { }
        // RVA: 0x0AC78120  token: 0x60004BD
        private virtual System.Void Slate.IDirectableTimePointer.TriggerBackward(System.Single currentTime, System.Single previousTime) { }

    }

    // TypeToken: 0x2000087  // size: 0x20
    public sealed struct EndTimePointer : Slate.IDirectableTimePointer
    {
        // Fields
        private System.Boolean triggered;  // 0x10
        private Slate.IDirectable <target>k__BackingField;  // 0x18

        // Properties
        Slate.IDirectable target { get; /* RVA: 0x04D88320 */ set; /* RVA: 0x067D5E9C */ }
        System.Single Slate.IDirectableTimePointer.time { get; /* RVA: 0x0AC70874 */ }

        // Methods
        // RVA: 0x0AC70890  token: 0x60004C1
        public System.Void .ctor(Slate.IDirectable target) { }
        // RVA: 0x0AC70794  token: 0x60004C2
        private virtual System.Void Slate.IDirectableTimePointer.TriggerForward(System.Single currentTime, System.Single previousTime) { }
        // RVA: 0x0AC70828  token: 0x60004C3
        private virtual System.Void Slate.IDirectableTimePointer.Update(System.Single currentTime, System.Single previousTime) { }
        // RVA: 0x0AC7070C  token: 0x60004C4
        private virtual System.Void Slate.IDirectableTimePointer.TriggerBackward(System.Single currentTime, System.Single previousTime) { }

    }

    // TypeToken: 0x2000088  // size: 0x38
    public class BezierPath : Slate.Path
    {
        // Fields
        public System.Int32 resolution;  // 0x18
        private System.Collections.Generic.List<Slate.BezierPoint> _points;  // 0x20
        private UnityEngine.Vector3[] _sampledPathPoints;  // 0x28
        private System.Single _length;  // 0x30

        // Properties
        System.Collections.Generic.List<Slate.BezierPoint> points { get; /* RVA: 0x04D862C0 */ }
        Slate.BezierPoint Item { get; /* RVA: 0x0AC6A8DC */ }
        System.Int32 pointCount { get; /* RVA: 0x0AC6A930 */ }
        System.Single length { get; /* RVA: 0x04D86E20 */ }

        // Methods
        // RVA: 0x09DEBAEC  token: 0x60004C9
        private System.Void Awake() { }
        // RVA: 0x09DEBAEC  token: 0x60004CA
        private System.Void OnValidate() { }
        // RVA: 0x0AC6A19C  token: 0x60004CB
        public virtual System.Void Compute() { }
        // RVA: 0x09DEBAEC  token: 0x60004CC
        public System.Void SetDirty() { }
        // RVA: 0x0AC6A010  token: 0x60004CD
        private System.Void ComputeLength() { }
        // RVA: 0x0AC6A030  token: 0x60004CE
        private System.Void ComputeSampledPathPoints() { }
        // RVA: 0x0AC6A1C4  token: 0x60004CF
        public static Slate.BezierPath Create(UnityEngine.Transform targetParent) { }
        // RVA: 0x0AC69F28  token: 0x60004D0
        public Slate.BezierPoint AddPointAt(UnityEngine.Vector3 position, System.Int32 index) { }
        // RVA: 0x0AC6A7F4  token: 0x60004D1
        public System.Void RemovePoint(Slate.BezierPoint point) { }
        // RVA: 0x0AC6A48C  token: 0x60004D2
        public System.Int32 GetPointIndex(Slate.BezierPoint point) { }
        // RVA: 0x0AC6A6E8  token: 0x60004D3
        public static UnityEngine.Vector3[] GetSampledPathPositions(Slate.BezierPoint p1, Slate.BezierPoint p2, System.Int32 resolution) { }
        // RVA: 0x0AC6A618  token: 0x60004D4
        public virtual UnityEngine.Vector3 GetPositionAt(System.Single t) { }
        // RVA: 0x0AC6A510  token: 0x60004D5
        public static UnityEngine.Vector3 GetPositionAt(Slate.BezierPoint p1, Slate.BezierPoint p2, System.Single t) { }
        // RVA: 0x0AC6A854  token: 0x60004D6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000089  // size: 0x40
    public class BezierPoint
    {
        // Fields
        private Slate.BezierPath _path;  // 0x10
        public UnityEngine.Vector3 _position;  // 0x18
        private UnityEngine.Vector3 _handle1;  // 0x24
        private UnityEngine.Vector3 _handle2;  // 0x30
        private Slate.BezierPoint.HandleStyle _handleStyle;  // 0x3c

        // Properties
        Slate.BezierPoint.HandleStyle handleStyle { get; /* RVA: 0x04D86640 */ set; /* RVA: 0x04D86670 */ }
        Slate.BezierPath path { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.Vector3 position { get; /* RVA: 0x0AC6AB08 */ set; /* RVA: 0x0AC6AE60 */ }
        UnityEngine.Vector3 handle1LocalPosition { get; /* RVA: 0x04D905E0 */ set; /* RVA: 0x0AC6AB78 */ }
        UnityEngine.Vector3 handle2LocalPosition { get; /* RVA: 0x04D88ED0 */ set; /* RVA: 0x0AC6ACEC */ }
        UnityEngine.Vector3 handle1Position { get; /* RVA: 0x0AC6A9B0 */ set; /* RVA: 0x0AC6AC30 */ }
        UnityEngine.Vector3 handle2Position { get; /* RVA: 0x0AC6AA5C */ set; /* RVA: 0x0AC6ADA4 */ }

        // Methods
        // RVA: 0x0AC6A970  token: 0x60004D7
        public System.Void .ctor(Slate.BezierPath path, UnityEngine.Vector3 position) { }

    }

    // TypeToken: 0x200008B  // size: 0x18
    public abstract class Path : UnityEngine.MonoBehaviour
    {
        // Properties
        System.Single length { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60004E7
        public virtual UnityEngine.Vector3 GetPositionAt(System.Single t) { }
        // RVA: -1  // abstract  token: 0x60004E8
        public virtual System.Void Compute() { }
        // RVA: 0x0AC72AE0  token: 0x60004E9
        public static UnityEngine.Vector3 GetPositionAlongCurve(UnityEngine.Vector3 from, UnityEngine.Vector3 to, UnityEngine.Vector3 fromTangent, UnityEngine.Vector3 toTangent, System.Single t) { }
        // RVA: 0x0AC72CD8  token: 0x60004EA
        public static UnityEngine.Vector3 GetPosition(System.Single t, UnityEngine.Vector3[] path) { }
        // RVA: 0x0AC72A34  token: 0x60004EB
        public static System.Single GetLength(UnityEngine.Vector3[] path) { }
        // RVA: 0x05393520  token: 0x60004EC
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200008C  // size: 0x10
    public static class AnimationDataUtility
    {
        // Methods
        // RVA: 0x0AC69048  token: 0x60004ED
        public static System.String[] GetAnimatableMemberPaths(System.Object root) { }
        // RVA: 0x0AC690A4  token: 0x60004EE
        private static System.String[] Internal_GetAnimatableMemberPaths(System.Type type, System.String path) { }

    }

    // TypeToken: 0x200008D  // size: 0x28
    public class AnimatorDispatcher : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Action<System.Int32> onAnimatorIK;  // 0x18
        private UnityEngine.Animator _animator;  // 0x20

        // Properties
        UnityEngine.Animator animator { get; /* RVA: 0x0AC694BC */ }

        // Events
        event System.Action<System.Int32> onAnimatorIK;

        // Methods
        // RVA: 0x0AC6941C  token: 0x60004F2
        private System.Void OnAnimatorIK(System.Int32 index) { }
        // RVA: 0x05393520  token: 0x60004F3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008E  // size: 0x10
    public static class AudioSampler
    {
        // Fields
        private static System.String ROOT_NAME;  // const
        private static UnityEngine.GameObject root;  // static @ 0x0
        private static System.Collections.Generic.Dictionary<System.Object,UnityEngine.AudioSource> sources;  // static @ 0x8

        // Methods
        // RVA: 0x0AC695D4  token: 0x60004F4
        public static UnityEngine.AudioSource GetSourceForID(System.Object keyID) { }
        // RVA: 0x0AC698B0  token: 0x60004F5
        public static System.Void ReleaseSourceForID(System.Object keyID) { }
        // RVA: 0x0AC69A3C  token: 0x60004F6
        public static System.Void SampleForID(System.Object keyID, UnityEngine.AudioClip clip, System.Single time, System.Single previousTime, System.Single volume) { }
        // RVA: 0x0AC69B00  token: 0x60004F7
        public static System.Void SampleForID(System.Object keyID, UnityEngine.AudioClip clip, System.Single time, System.Single previousTime, Slate.AudioSampler.SampleSettings settings) { }
        // RVA: 0x0AC69BA4  token: 0x60004F8
        public static System.Void Sample(UnityEngine.AudioSource source, UnityEngine.AudioClip clip, System.Single time, System.Single previousTime, System.Single volume) { }
        // RVA: 0x0AC69C68  token: 0x60004F9
        public static System.Void Sample(UnityEngine.AudioSource source, UnityEngine.AudioClip clip, System.Single time, System.Single previousTime, Slate.AudioSampler.SampleSettings settings) { }
        // RVA: 0x0AC69E94  token: 0x60004FA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000090  // size: 0x10
    public static class ColorUtility
    {
        // Methods
        // RVA: 0x04DA6870  token: 0x60004FC
        public static UnityEngine.Color Grey(System.Single value) { }
        // RVA: 0x04DA6890  token: 0x60004FD
        public static UnityEngine.Color WithAlpha(UnityEngine.Color color, System.Single alpha) { }

    }

    // TypeToken: 0x2000091  // size: 0x14
    public sealed struct TangentMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.TangentMode Editable;  // const
        public static Slate.TangentMode Smooth;  // const
        public static Slate.TangentMode Linear;  // const
        public static Slate.TangentMode Constant;  // const

    }

    // TypeToken: 0x2000092  // size: 0x10
    public static class CurveUtility
    {
        // Fields
        private static System.Single KEY_PROXIMITY_TOLERANCE;  // const

        // Methods
        // RVA: 0x0AC6BA24  token: 0x60004FE
        public static System.Single GetKeyNext(System.Single time, UnityEngine.AnimationCurve[] curves) { }
        // RVA: 0x0AC6BC70  token: 0x60004FF
        public static System.Single GetKeyPrevious(System.Single time, UnityEngine.AnimationCurve[] curves) { }
        // RVA: 0x0AC6BF2C  token: 0x6000500
        public static System.Boolean HasKey(System.Single time, UnityEngine.AnimationCurve[] curves) { }
        // RVA: 0x0AC6BED8  token: 0x6000501
        public static System.Boolean HasAnyKey(UnityEngine.AnimationCurve[] curves) { }
        // RVA: 0x0AC6C160  token: 0x6000502
        public static System.Void OffsetCurveValue(UnityEngine.AnimationCurve curve, System.Single deltaValue) { }
        // RVA: 0x0AC6C070  token: 0x6000503
        public static System.Void OffsetCurveTime(UnityEngine.AnimationCurve curve, System.Single deltaTime) { }
        // RVA: 0x0AC6C340  token: 0x6000504
        public static System.Void RemoveNegativeKeys(UnityEngine.AnimationCurve curve) { }
        // RVA: 0x0AC6C200  token: 0x6000505
        public static System.Void RemoveKeysOffRange(UnityEngine.AnimationCurve curve, System.Single min, System.Single max) { }

    }

    // TypeToken: 0x2000097  // size: 0x18
    public class DecoratorAttribute : System.Attribute
    {
        // Fields
        public readonly System.Type targetType;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000510
        public System.Void .ctor(System.Type targetType) { }

    }

    // TypeToken: 0x2000098
    public interface IDecorator
    {
        // Properties
        System.Object Target { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000099  // size: 0x10
    public static class DecoratorFactory
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.Type,System.Type> decoratorsTypeMap;  // static @ 0x0
        private static System.Collections.Generic.Dictionary<System.Object,Slate.IDecorator> decoratorsInstanceMap;  // static @ 0x8

        // Methods
        // RVA: -1  // generic def  token: 0x6000513
        public static T Decorator(System.Object target) { }
        // RVA: -1  // generic def  token: 0x6000514
        public static T GetDecorator(System.Object target) { }
        // RVA: -1  // generic def  token: 0x6000515
        private static T CreateDecorator(System.Type targetType) { }
        // RVA: 0x0AC6EF74  token: 0x6000516
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200009A  // size: 0x14
    public sealed struct EaseType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Slate.EaseType Linear;  // const
        public static Slate.EaseType QuadraticIn;  // const
        public static Slate.EaseType QuadraticOut;  // const
        public static Slate.EaseType QuadraticInOut;  // const
        public static Slate.EaseType QuarticIn;  // const
        public static Slate.EaseType QuarticOut;  // const
        public static Slate.EaseType QuarticInOut;  // const
        public static Slate.EaseType QuinticIn;  // const
        public static Slate.EaseType QuinticOut;  // const
        public static Slate.EaseType QuinticInOut;  // const
        public static Slate.EaseType CubicIn;  // const
        public static Slate.EaseType CubicOut;  // const
        public static Slate.EaseType CubicInOut;  // const
        public static Slate.EaseType ExponentialIn;  // const
        public static Slate.EaseType ExponentialOut;  // const
        public static Slate.EaseType ExponentialInOut;  // const
        public static Slate.EaseType CircularIn;  // const
        public static Slate.EaseType CircularOut;  // const
        public static Slate.EaseType CircularInOut;  // const
        public static Slate.EaseType SinusoidalIn;  // const
        public static Slate.EaseType SinusoidalOut;  // const
        public static Slate.EaseType SinusoidalInOut;  // const
        public static Slate.EaseType ElasticIn;  // const
        public static Slate.EaseType ElasticOut;  // const
        public static Slate.EaseType ElasticInOut;  // const
        public static Slate.EaseType BounceIn;  // const
        public static Slate.EaseType BounceOut;  // const
        public static Slate.EaseType BounceInOut;  // const
        public static Slate.EaseType BackIn;  // const
        public static Slate.EaseType BackOut;  // const
        public static Slate.EaseType BackInOut;  // const

    }

    // TypeToken: 0x200009B  // size: 0x10
    public static class Easing
    {
        // Methods
        // RVA: 0x0AC6FCF4  token: 0x6000517
        public static System.Single Ease(Slate.EaseType type, System.Single from, System.Single to, System.Single t) { }
        // RVA: 0x0AC6FEF8  token: 0x6000518
        public static UnityEngine.Vector3 Ease(Slate.EaseType type, UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single t) { }
        // RVA: 0x0AC6FD78  token: 0x6000519
        public static UnityEngine.Quaternion Ease(Slate.EaseType type, UnityEngine.Quaternion from, UnityEngine.Quaternion to, System.Single t) { }
        // RVA: 0x0AC6FE3C  token: 0x600051A
        public static UnityEngine.Color Ease(Slate.EaseType type, UnityEngine.Color from, UnityEngine.Color to, System.Single t) { }
        // RVA: 0x0AC6FFDC  token: 0x600051B
        public static System.Func<System.Single,System.Single> Function(Slate.EaseType type) { }
        // RVA: 0x041E1670  token: 0x600051C
        public static System.Single Linear(System.Single t) { }
        // RVA: 0x04D886E0  token: 0x600051D
        public static System.Single QuadraticIn(System.Single t) { }
        // RVA: 0x04DA6AB0  token: 0x600051E
        public static System.Single QuadraticOut(System.Single t) { }
        // RVA: 0x0A9A0E30  token: 0x600051F
        public static System.Single QuadraticInOut(System.Single t) { }
        // RVA: 0x04DA6AD0  token: 0x6000520
        public static System.Single QuarticIn(System.Single t) { }
        // RVA: 0x04DA6AE0  token: 0x6000521
        public static System.Single QuarticOut(System.Single t) { }
        // RVA: 0x0A9A0E8C  token: 0x6000522
        public static System.Single QuarticInOut(System.Single t) { }
        // RVA: 0x04DA6B10  token: 0x6000523
        public static System.Single QuinticIn(System.Single t) { }
        // RVA: 0x04DA6B30  token: 0x6000524
        public static System.Single QuinticOut(System.Single t) { }
        // RVA: 0x0A9A0EE0  token: 0x6000525
        public static System.Single QuinticInOut(System.Single t) { }
        // RVA: 0x04D89930  token: 0x6000526
        public static System.Single CubicIn(System.Single t) { }
        // RVA: 0x04DA69A0  token: 0x6000527
        public static System.Single CubicOut(System.Single t) { }
        // RVA: 0x0A9A01E0  token: 0x6000528
        public static System.Single CubicInOut(System.Single t) { }
        // RVA: 0x0A9A0F3C  token: 0x6000529
        public static System.Single SinusoidalIn(System.Single t) { }
        // RVA: 0x0A9A0F6C  token: 0x600052A
        public static System.Single SinusoidalOut(System.Single t) { }
        // RVA: 0x04DA6B60  token: 0x600052B
        public static System.Single SinusoidalInOut(System.Single t) { }
        // RVA: 0x0A9A0690  token: 0x600052C
        public static System.Single ExponentialIn(System.Single t) { }
        // RVA: 0x0A9A06BC  token: 0x600052D
        public static System.Single ExponentialOut(System.Single t) { }
        // RVA: 0x0A9A0610  token: 0x600052E
        public static System.Single ExponentialInOut(System.Single t) { }
        // RVA: 0x04DA6910  token: 0x600052F
        public static System.Single CircularIn(System.Single t) { }
        // RVA: 0x04DA6970  token: 0x6000530
        public static System.Single CircularOut(System.Single t) { }
        // RVA: 0x0A9A017C  token: 0x6000531
        public static System.Single CircularInOut(System.Single t) { }
        // RVA: 0x04DA69C0  token: 0x6000532
        public static System.Single ElasticIn(System.Single t) { }
        // RVA: 0x04DA6A40  token: 0x6000533
        public static System.Single ElasticOut(System.Single t) { }
        // RVA: 0x0A9A0530  token: 0x6000534
        public static System.Single ElasticInOut(System.Single t) { }
        // RVA: 0x0A9A00B0  token: 0x6000535
        public static System.Single BounceIn(System.Single t) { }
        // RVA: 0x0A9A00E4  token: 0x6000536
        public static System.Single BounceOut(System.Single t) { }
        // RVA: 0x0A9A0044  token: 0x6000537
        public static System.Single BounceInOut(System.Single t) { }
        // RVA: 0x04DA68C0  token: 0x6000538
        public static System.Single BackIn(System.Single t) { }
        // RVA: 0x04DA68E0  token: 0x6000539
        public static System.Single BackOut(System.Single t) { }
        // RVA: 0x0A99FFDC  token: 0x600053A
        public static System.Single BackInOut(System.Single t) { }

    }

    // TypeToken: 0x200009C
    public interface ITransformRefParameter
    {
        // Properties
        UnityEngine.Transform transform { get; /* RVA: -1  // abstract */ }
        Slate.TransformSpace space { get; /* RVA: -1  // abstract */ }
        System.Boolean useAnimation { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x200009D  // size: 0x18
    public class ObjectSnapshot
    {
        // Fields
        private static System.Collections.Generic.List<System.Type> ignoreTypes;  // static @ 0x0
        private System.Collections.Generic.Dictionary<UnityEngine.Object,System.String> serialized;  // 0x10

        // Methods
        // RVA: 0x0AC7235C  token: 0x600053E
        public static System.Void AppendIgnoreType(System.Type type) { }
        // RVA: 0x0AC72994  token: 0x600053F
        public System.Void .ctor(UnityEngine.Object target, System.Boolean fullObjectHierarchy) { }
        // RVA: 0x0AC7264C  token: 0x6000540
        public System.Void Store(UnityEngine.Object target, System.Boolean fullObjectHierarchy) { }
        // RVA: 0x0AC7240C  token: 0x6000541
        public System.Void Restore() { }
        // RVA: 0x0AC72908  token: 0x6000542
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200009E  // size: 0x10
    public static class RectUtility
    {
        // Methods
        // RVA: 0x0A996768  token: 0x6000543
        public static UnityEngine.Rect GetBoundRect(UnityEngine.Rect[] rects) { }
        // RVA: 0x0A996888  token: 0x6000544
        public static UnityEngine.Rect GetBoundRect(UnityEngine.Vector2[] positions) { }
        // RVA: 0x0AC74978  token: 0x6000545
        public static System.Boolean Encapsulates(UnityEngine.Rect a, UnityEngine.Rect b) { }
        // RVA: 0x0AC74A94  token: 0x6000546
        public static UnityEngine.Rect ExpandBy(UnityEngine.Rect rect, System.Single margin) { }
        // RVA: 0x0AC74A30  token: 0x6000547
        public static UnityEngine.Rect ExpandBy(UnityEngine.Rect rect, System.Single xMargin, System.Single yMargin) { }
        // RVA: 0x0A996A18  token: 0x6000548
        public static UnityEngine.Rect TransformSpace(UnityEngine.Rect rect, UnityEngine.Rect oldContainer, UnityEngine.Rect newContainer) { }
        // RVA: 0x0AC74AD0  token: 0x6000549
        public static UnityEngine.Rect ToViewRect(UnityEngine.Bounds b, UnityEngine.Camera cam) { }

    }

    // TypeToken: 0x200009F  // size: 0x10
    public static class ReflectionTools
    {
        // Fields
        private static System.Reflection.BindingFlags flagsEverything;  // const
        private static System.Collections.Generic.List<System.Reflection.Assembly> _loadedAssemblies;  // static @ 0x0
        private static System.Collections.Generic.Dictionary<System.String,System.Type> typeMap;  // static @ 0x8
        private static System.Collections.Generic.Dictionary<System.Type,System.Type[]> subTypesMap;  // static @ 0x10

        // Properties
        System.Collections.Generic.List<System.Reflection.Assembly> loadedAssemblies { get; /* RVA: 0x0AC76DE4 */ }

        // Methods
        // RVA: 0x0AC76184  token: 0x600054B
        public static System.Type GetType(System.String typeName) { }
        // RVA: 0x0AC75334  token: 0x600054C
        public static System.Type[] GetAllTypes() { }
        // RVA: 0x0AC75584  token: 0x600054D
        public static System.Type[] GetImplementationsOf(System.Type type) { }
        // RVA: 0x0AC7662C  token: 0x600054E
        private static System.Type[] RTGetExportedTypes(System.Reflection.Assembly asm) { }
        // RVA: 0x0AC75244  token: 0x600054F
        public static System.String FriendlyName(System.Type type) { }
        // RVA: 0x0AC76AB4  token: 0x6000550
        public static System.Boolean RTIsStatic(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: 0x0A989920  token: 0x6000551
        public static System.Boolean RTIsAbstract(System.Type type) { }
        // RVA: 0x0A9899E8  token: 0x6000552
        public static System.Boolean RTIsSubclassOf(System.Type type, System.Type other) { }
        // RVA: 0x0AC76A90  token: 0x6000553
        public static System.Boolean RTIsAssignableFrom(System.Type type, System.Type second) { }
        // RVA: 0x0AC768FC  token: 0x6000554
        public static System.Reflection.FieldInfo RTGetField(System.Type type, System.String name) { }
        // RVA: 0x0AC76A6C  token: 0x6000555
        public static System.Reflection.PropertyInfo RTGetProperty(System.Type type, System.String name) { }
        // RVA: 0x0AC76A24  token: 0x6000556
        public static System.Reflection.MethodInfo RTGetMethod(System.Type type, System.String name) { }
        // RVA: 0x0AC76A00  token: 0x6000557
        public static System.Reflection.FieldInfo[] RTGetFields(System.Type type) { }
        // RVA: 0x0AC76A48  token: 0x6000558
        public static System.Reflection.PropertyInfo[] RTGetProperties(System.Type type) { }
        // RVA: 0x0A989524  token: 0x6000559
        public static System.Reflection.MethodInfo RTGetGetMethod(System.Reflection.PropertyInfo prop) { }
        // RVA: 0x0A989904  token: 0x600055A
        public static System.Reflection.MethodInfo RTGetSetMethod(System.Reflection.PropertyInfo prop) { }
        // RVA: 0x0AC76B64  token: 0x600055B
        public static System.Type RTReflectedType(System.Type type) { }
        // RVA: 0x0AC76B64  token: 0x600055C
        public static System.Type RTReflectedType(System.Reflection.MemberInfo member) { }
        // RVA: -1  // generic def  token: 0x600055D
        public static T RTGetAttribute(System.Type type, System.Boolean inherited) { }
        // RVA: -1  // generic def  token: 0x600055E
        public static T RTGetAttribute(System.Reflection.MemberInfo member, System.Boolean inherited) { }
        // RVA: -1  // generic def  token: 0x600055F
        public static System.Boolean RTIsDefined(System.Reflection.MemberInfo member, System.Boolean inherited) { }
        // RVA: -1  // generic def  token: 0x6000560
        public static T RTCreateDelegate(System.Reflection.MethodInfo method, System.Object instance) { }
        // RVA: -1  // generic def  token: 0x6000561
        public static System.Action<T,TValue> GetFieldOrPropSetter(System.Reflection.MemberInfo info) { }
        // RVA: 0x0AC76920  token: 0x6000562
        public static System.Reflection.MemberInfo[] RTGetFieldsAndProps(System.Type type) { }
        // RVA: 0x0AC7686C  token: 0x6000563
        public static System.Reflection.MemberInfo RTGetFieldOrProp(System.Type type, System.String name) { }
        // RVA: 0x0AC76710  token: 0x6000564
        public static System.Object RTGetFieldOrPropValue(System.Reflection.MemberInfo member, System.Object instance, System.Int32 index) { }
        // RVA: 0x0AC76B84  token: 0x6000565
        public static System.Void RTSetFieldOrPropValue(System.Reflection.MemberInfo member, System.Object instance, System.Object value, System.Int32 index) { }
        // RVA: 0x0AC7664C  token: 0x6000566
        public static System.Type RTGetFieldOrPropType(System.Reflection.MemberInfo member) { }
        // RVA: 0x0AC76110  token: 0x6000567
        public static System.Reflection.MemberInfo GetRelativeMember(System.Object root, System.String path) { }
        // RVA: 0x0AC75FBC  token: 0x6000568
        public static System.Reflection.MemberInfo GetRelativeMember(System.Type type, System.String path) { }
        // RVA: 0x0AC75E78  token: 0x6000569
        public static System.Object GetRelativeMemberParent(System.Object root, System.String path) { }
        // RVA: -1  // generic def  token: 0x600056A
        public static System.String GetMemberPath(System.Linq.Expressions.Expression<System.Func<T,TResult>> func) { }
        // RVA: 0x0AC75A34  token: 0x600056B
        public static System.String[] GetMemberPaths(System.Type type, System.Predicate<System.Type> shouldInclude, System.Predicate<System.Type> shouldContinue, System.String currentPath, System.Collections.Generic.List<System.Type> recursionCheck) { }
        // RVA: 0x0AC76CE8  token: 0x600056C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A2  // size: 0x10
    public static class StringUtility
    {
        // Methods
        // RVA: 0x0AC787A8  token: 0x6000571
        public static System.String SplitCamelCase(System.String s) { }
        // RVA: 0x0AC786CC  token: 0x6000572
        public static System.String AbsToRelativePath(System.String absolutepath) { }

    }

    // TypeToken: 0x20000A3  // size: 0x30
    public sealed struct TransformRefPosition : Slate.ITransformRefParameter
    {
        // Fields
        private Slate.CutsceneGroup _group;  // 0x10
        private UnityEngine.Transform _transform;  // 0x18
        private UnityEngine.Vector3 _vector;  // 0x20
        private Slate.TransformSpace _space;  // 0x2c

        // Properties
        System.Boolean useAnimation { get; /* RVA: 0x0AC78D84 */ }
        Slate.CutsceneGroup group { get; /* RVA: 0x02B76770 */ set; /* RVA: 0x05392320 */ }
        UnityEngine.Transform transform { get; /* RVA: 0x0AC78CCC */ set; /* RVA: 0x067D5E9C */ }
        UnityEngine.Vector3 value { get; /* RVA: 0x0AC78E30 */ set; /* RVA: 0x04D8C970 */ }
        Slate.TransformSpace space { get; /* RVA: 0x0AC78CA8 */ set; /* RVA: 0x04D86320 */ }

        // Methods
        // RVA: 0x0AC78C20  token: 0x600057C
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20000A4  // size: 0x40
    public sealed struct TransformRefPositionRotation : Slate.ITransformRefParameter
    {
        // Fields
        private Slate.CutsceneGroup _group;  // 0x10
        private UnityEngine.Transform _transform;  // 0x18
        private UnityEngine.Vector3 _position;  // 0x20
        private UnityEngine.Vector3 _rotation;  // 0x2c
        private Slate.TransformSpace _space;  // 0x38

        // Properties
        System.Boolean useAnimation { get; /* RVA: 0x0AC78B74 */ }
        Slate.CutsceneGroup group { get; /* RVA: 0x02B76770 */ set; /* RVA: 0x05392320 */ }
        UnityEngine.Transform transform { get; /* RVA: 0x0AC78ABC */ set; /* RVA: 0x067D5E9C */ }
        UnityEngine.Vector3 position { get; /* RVA: 0x0AC789C0 */ set; /* RVA: 0x04D8C970 */ }
        UnityEngine.Vector3 rotation { get; /* RVA: 0x0AC78A2C */ set; /* RVA: 0x04D90500 */ }
        Slate.TransformSpace space { get; /* RVA: 0x0AC78A98 */ set; /* RVA: 0x011F2AA0 */ }

        // Methods
        // RVA: 0x0AC78908  token: 0x6000588
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20000A5  // size: 0x18
    public class TransformSnapshot
    {
        // Fields
        private System.Collections.Generic.List<Slate.TransformSnapshot.TransformData> data;  // 0x10

        // Methods
        // RVA: 0x0AC793B0  token: 0x6000589
        public System.Void .ctor(UnityEngine.GameObject root, Slate.TransformSnapshot.StoreMode mode) { }
        // RVA: 0x0AC7900C  token: 0x600058A
        public System.Void Store(UnityEngine.GameObject root, Slate.TransformSnapshot.StoreMode mode) { }
        // RVA: 0x0AC78E9C  token: 0x600058B
        public System.Void Restore() { }

    }

    // TypeToken: 0x20000A8  // size: 0x10
    public static class UnityObjectUtility
    {
        // Fields
        private static System.Reflection.MethodInfo _getLocalEulerAngles;  // static @ 0x0
        private static System.Reflection.MethodInfo _setLocalEulerAngles;  // static @ 0x8
        private static System.Reflection.PropertyInfo _rotationOrder;  // static @ 0x10
        private static System.Object[] _rotationOrderArgGet;  // static @ 0x18
        private static System.Object[] _rotationOrderArgSet;  // static @ 0x20

        // Methods
        // RVA: 0x0AC7AAAC  token: 0x600058D
        private static System.Void .cctor() { }
        // RVA: 0x0AC7A4D0  token: 0x600058E
        public static UnityEngine.Vector3 GetLocalEulerAngles(UnityEngine.Transform transform) { }
        // RVA: 0x0AC7A94C  token: 0x600058F
        public static System.Void SetLocalEulerAngles(UnityEngine.Transform transform, UnityEngine.Vector3 value) { }
        // RVA: 0x0AC79F84  token: 0x6000590
        public static System.String CalculateTransformPath(UnityEngine.Transform root, UnityEngine.Transform child) { }
        // RVA: 0x0AC7A76C  token: 0x6000591
        public static UnityEngine.Transform ResolveTransformPath(UnityEngine.Transform root, System.String path) { }
        // RVA: 0x0AC7A138  token: 0x6000592
        public static UnityEngine.Transform FindInChildren(UnityEngine.Transform root, System.String name, System.Boolean includeHidden) { }
        // RVA: 0x0AC7A6C8  token: 0x6000593
        public static System.Void ResetLocalCoords(UnityEngine.Transform transform, System.Boolean includeScale) { }
        // RVA: 0x0AC7A890  token: 0x6000594
        public static System.Void SetLocalCoordsFrom(UnityEngine.Transform transform, UnityEngine.Transform source) { }
        // RVA: 0x0AC7A5F0  token: 0x6000595
        public static UnityEngine.Transform InsertParentInChain(UnityEngine.Transform transform, System.String name) { }
        // RVA: -1  // generic def  token: 0x6000596
        public static T GetAddComponent(UnityEngine.GameObject go) { }
        // RVA: -1  // generic def  token: 0x6000597
        public static T GetAddComponent(UnityEngine.Component comp) { }
        // RVA: 0x0AC7A274  token: 0x6000598
        public static UnityEngine.Component GetAddComponent(UnityEngine.GameObject go, System.Type type) { }
        // RVA: 0x0AC7A3C8  token: 0x6000599
        public static System.Collections.Generic.List<System.String> GetBlendShapeNames(UnityEngine.SkinnedMeshRenderer skinnedMesh) { }
        // RVA: 0x0AC7A304  token: 0x600059A
        public static System.Int32 GetBlendShapeIndex(UnityEngine.SkinnedMeshRenderer skinnedMesh, System.String shapeName) { }

    }

    // TypeToken: 0x20000AA  // size: 0x10
    public static class VideoSampler
    {
        // Fields
        private static System.String ROOT_NAME;  // const
        private static UnityEngine.GameObject root;  // static @ 0x0
        private static System.Collections.Generic.Dictionary<System.Object,UnityEngine.Video.VideoPlayer> sources;  // static @ 0x8

        // Methods
        // RVA: 0x0AC7AC74  token: 0x600059D
        public static UnityEngine.Video.VideoPlayer GetSourceForID(System.Object keyID) { }
        // RVA: 0x0AC7AFBC  token: 0x600059E
        public static System.Void ReleaseSourceForID(System.Object keyID) { }
        // RVA: 0x0AC7B170  token: 0x600059F
        public static System.Void SampleForID(System.Object keyID, UnityEngine.Video.VideoClip clip, System.Single time, System.Single previousTime, Slate.VideoSampler.SampleSettings settings) { }
        // RVA: 0x0AC7B214  token: 0x60005A0
        public static System.Void Sample(UnityEngine.Video.VideoPlayer source, UnityEngine.Video.VideoClip clip, System.Single time, System.Single previousTime, Slate.VideoSampler.SampleSettings settings) { }
        // RVA: 0x0AC7B414  token: 0x60005A1
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AD  // size: 0x38
    public class CutsceneSequencePlayer : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Boolean playOnStart;  // 0x18
        public System.Collections.Generic.List<Slate.Cutscene> cutscenes;  // 0x20
        public UnityEngine.Events.UnityEvent onFinish;  // 0x28
        private System.Int32 currentIndex;  // 0x30
        private System.Boolean isPlaying;  // 0x34

        // Methods
        // RVA: 0x0AC6E45C  token: 0x60005A3
        private System.Void Start() { }
        // RVA: 0x0AC6E388  token: 0x60005A4
        public System.Void Play() { }
        // RVA: 0x0AC6E46C  token: 0x60005A5
        public System.Void Stop() { }
        // RVA: 0x0AC6E224  token: 0x60005A6
        private System.Void MoveNext() { }
        // RVA: 0x0AC6E1A4  token: 0x60005A7
        public static UnityEngine.GameObject Create() { }
        // RVA: 0x05394740  token: 0x60005A8
        public System.Void .ctor() { }
        // RVA: 0x0AC6E4D4  token: 0x60005A9
        private System.Void <MoveNext>b__8_0() { }

    }

    // TypeToken: 0x20000AE  // size: 0x98
    public class DirectorGUITemplate : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Boolean dontDestroyOnLoad;  // 0x18
        public UnityEngine.UI.CanvasScaler absScaler;  // 0x20
        public UnityEngine.UI.CanvasScaler refScaler;  // 0x28
        public UnityEngine.CanvasGroup cameraDissolveGroup;  // 0x30
        public UnityEngine.UI.RawImage dissolverImage;  // 0x38
        public UnityEngine.CanvasGroup letterboxGroup;  // 0x40
        public UnityEngine.UI.RawImage letterboxTop;  // 0x48
        public UnityEngine.UI.RawImage letterboxBottom;  // 0x50
        public UnityEngine.CanvasGroup screenFadeGroup;  // 0x58
        public UnityEngine.UI.RawImage screenFadeImage;  // 0x60
        public UnityEngine.CanvasGroup overlayImageGroup;  // 0x68
        public UnityEngine.UI.RawImage overlayImage;  // 0x70
        public UnityEngine.CanvasGroup overlayTextGroup;  // 0x78
        public UnityEngine.UI.Text overlayText;  // 0x80
        public UnityEngine.CanvasGroup subtitlesGroup;  // 0x88
        public UnityEngine.UI.Text subtitlesText;  // 0x90
        private static Slate.DirectorGUITemplate _current;  // static @ 0x0

        // Methods
        // RVA: 0x0AC6F068  token: 0x60005AA
        private System.Void Awake() { }
        // RVA: 0x0AC6FBF8  token: 0x60005AB
        private System.Void ShowAll() { }
        // RVA: 0x0AC6F188  token: 0x60005AC
        private System.Void HideAll() { }
        // RVA: 0x0AC6F51C  token: 0x60005AD
        private System.Void OnEnable() { }
        // RVA: 0x0AC6F300  token: 0x60005AE
        private System.Void OnDisable() { }
        // RVA: 0x0AC6FCA0  token: 0x60005AF
        private System.Void Update() { }
        // RVA: 0x0AC6F218  token: 0x60005B0
        private System.Void OnCameraDissolve(UnityEngine.Texture texture, System.Single completion) { }
        // RVA: 0x0AC6F738  token: 0x60005B1
        private System.Void OnImageOverlayGUI(UnityEngine.Texture texture, UnityEngine.Color color, UnityEngine.Vector2 scale, UnityEngine.Vector2 position) { }
        // RVA: 0x0AC6F8A0  token: 0x60005B2
        private System.Void OnLetterboxGUI(System.Single completion) { }
        // RVA: 0x0AC6FA30  token: 0x60005B3
        private System.Void OnScreenFadeGUI(UnityEngine.Color color) { }
        // RVA: 0x0AC6FB0C  token: 0x60005B4
        private System.Void OnTextOverlayGUI(System.String text, UnityEngine.Color color, System.Single size, UnityEngine.TextAnchor anchor, UnityEngine.Vector2 position) { }
        // RVA: 0x0AC6FA88  token: 0x60005B5
        private System.Void OnSubtitlesGUI(System.String text, UnityEngine.Color color) { }
        // RVA: 0x05394740  token: 0x60005B6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AF  // size: 0x30
    public class PlayCutsceneOnClick : UnityEngine.MonoBehaviour
    {
        // Fields
        public Slate.Cutscene cutscene;  // 0x18
        public System.Single startTime;  // 0x20
        public UnityEngine.Events.UnityEvent onFinish;  // 0x28

        // Methods
        // RVA: 0x0AC742C4  token: 0x60005B7
        private System.Void OnMouseDown() { }
        // RVA: 0x0AC743D8  token: 0x60005B8
        private System.Void Reset() { }
        // RVA: 0x0AC74254  token: 0x60005B9
        public static UnityEngine.GameObject Create() { }
        // RVA: 0x05393520  token: 0x60005BA
        public System.Void .ctor() { }
        // RVA: 0x0AC74478  token: 0x60005BB
        private System.Void <OnMouseDown>b__3_0() { }

    }

    // TypeToken: 0x20000B0  // size: 0x30
    public class PlayCutsceneOnStart : UnityEngine.MonoBehaviour
    {
        // Fields
        public Slate.Cutscene cutscene;  // 0x18
        public System.Single startTime;  // 0x20
        public UnityEngine.Events.UnityEvent onFinish;  // 0x28

        // Methods
        // RVA: 0x0AC74524  token: 0x60005BC
        private System.Void Start() { }
        // RVA: 0x0AC74498  token: 0x60005BD
        public static UnityEngine.GameObject Create() { }
        // RVA: 0x05393520  token: 0x60005BE
        public System.Void .ctor() { }
        // RVA: 0x0AC74478  token: 0x60005BF
        private System.Void <Start>b__3_0() { }

    }

    // TypeToken: 0x20000B1  // size: 0x40
    public class PlayCutsceneOnTrigger : UnityEngine.MonoBehaviour
    {
        // Fields
        public Slate.Cutscene cutscene;  // 0x18
        public System.Single startTime;  // 0x20
        public System.Boolean checkSpecificTagOnly;  // 0x24
        public System.String tagName;  // 0x28
        public System.Boolean once;  // 0x30
        public UnityEngine.Events.UnityEvent onFinish;  // 0x38

        // Methods
        // RVA: 0x0AC746C4  token: 0x60005C0
        private System.Void OnTriggerEnter(UnityEngine.Collider other) { }
        // RVA: 0x0AC7483C  token: 0x60005C1
        private System.Void Reset() { }
        // RVA: 0x0AC74638  token: 0x60005C2
        public static UnityEngine.GameObject Create() { }
        // RVA: 0x0AC7492C  token: 0x60005C3
        public System.Void .ctor() { }
        // RVA: 0x0AC748B4  token: 0x60005C4
        private System.Void <OnTriggerEnter>b__6_0() { }

    }

    // TypeToken: 0x20000B2  // size: 0x28
    public class StopCutsceneOnClick : UnityEngine.MonoBehaviour
    {
        // Fields
        public Slate.Cutscene cutscene;  // 0x18
        public Slate.Cutscene.StopMode stopMode;  // 0x20

        // Methods
        // RVA: 0x0AC783E0  token: 0x60005C5
        private System.Void OnMouseDown() { }
        // RVA: 0x0AC78494  token: 0x60005C6
        private System.Void Reset() { }
        // RVA: 0x05393520  token: 0x60005C7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B3  // size: 0x38
    public class StopCutsceneOnTrigger : UnityEngine.MonoBehaviour
    {
        // Fields
        public Slate.Cutscene cutscene;  // 0x18
        public System.Boolean checkSpecificTagOnly;  // 0x20
        public System.String tagName;  // 0x28
        public Slate.Cutscene.StopMode stopMode;  // 0x30

        // Methods
        // RVA: 0x0AC784F4  token: 0x60005C8
        private System.Void OnTriggerEnter(UnityEngine.Collider other) { }
        // RVA: 0x0AC78608  token: 0x60005C9
        private System.Void Reset() { }
        // RVA: 0x0AC78680  token: 0x60005CA
        public System.Void .ctor() { }

    }

}

namespace Slate.ActionClips
{

    // TypeToken: 0x20000B4  // size: 0x40
    public abstract class ActorActionClip : Slate.ActionClip
    {
        // Methods
        // RVA: 0x05393520  token: 0x60005CB
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000B5
    public abstract class ActorActionClip`1 : Slate.ActionClip
    {
        // Fields
        private T _actorComponent;  // 0x0

        // Properties
        T actor { get; /* RVA: -1  // not resolved */ }
        System.Boolean isValid { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60005CE
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000B6  // size: 0x60
    public class AnimateProperties : Slate.ActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        protected System.String _name;  // 0x50
        public Slate.EaseType interpolation;  // 0x58

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC509C8 */ }
        System.String info { get; /* RVA: 0x0AC68FD8 */ }
        System.Object animatedParametersTarget { get; /* RVA: 0x0AC68FD0 */ }
        Slate.EaseType animatedParametersInterpolation { get; /* RVA: 0x04D86570 */ }
        System.Boolean useWeightInParameters { get; /* RVA: 0x02FFF600 */ }

        // Methods
        // RVA: 0x0AC68FAC  token: 0x60005DA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B7  // size: 0x78
    public class AnimateBlendShape : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        private System.String _skinName;  // 0x50
        private System.String _shapeName;  // 0x58
        public System.Single weight;  // 0x60
        private System.Single originalWeight;  // 0x64
        private System.Int32 index;  // 0x68
        private UnityEngine.SkinnedMeshRenderer _skinnedMesh;  // 0x70

        // Properties
        System.String info { get; /* RVA: 0x0AC68148 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC68188 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Boolean canCrossBlend { get; /* RVA: 0x02FFF600 */ }
        System.String skinName { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x0AC68388 */ }
        System.String shapeName { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x036E5590 */ }
        UnityEngine.SkinnedMeshRenderer skinnedMesh { get; /* RVA: 0x0AC68230 */ }

        // Methods
        // RVA: 0x0AC67F34  token: 0x60005E9
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC68054  token: 0x60005EA
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC68018  token: 0x60005EB
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC68114  token: 0x60005EC
        public System.Void .ctor() { }
        // RVA: 0x0AC680DC  token: 0x60005ED
        private System.Boolean <get_skinnedMesh>b__31_0(UnityEngine.SkinnedMeshRenderer s) { }

    }

    // TypeToken: 0x20000B8  // size: 0x78
    public class CharacterExpression : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        private System.Single _length;  // 0x48
        private System.Single _blendIn;  // 0x4c
        private System.Single _blendOut;  // 0x50
        public System.String expressionName;  // 0x58
        public System.String expressionUID;  // 0x60
        public System.Single weight;  // 0x68
        private System.Single originalWeight;  // 0x6c
        private Slate.BlendShapeGroup expression;  // 0x70

        // Properties
        System.String info { get; /* RVA: 0x0AC6B0A4 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC6B158 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single blendOut { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        System.Boolean canCrossBlend { get; /* RVA: 0x02FFF600 */ }

        // Methods
        // RVA: 0x0AC6AFB4  token: 0x60005F7
        private Slate.BlendShapeGroup ResolveExpression() { }
        // RVA: 0x0AC6AECC  token: 0x60005F8
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC6AF44  token: 0x60005F9
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC6AF00  token: 0x60005FA
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC6B040  token: 0x60005FB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B9  // size: 0xA0
    public class CharacterLookAt : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        private System.Single _length;  // 0x48
        private System.Single _blendIn;  // 0x4c
        private System.Single _blendOut;  // 0x50
        public Slate.EaseType interpolation;  // 0x54
        public System.Single weight;  // 0x58
        public Slate.TransformRefPosition targetPosition;  // 0x60
        private UnityEngine.Quaternion wasNeckRot;  // 0x80
        private UnityEngine.Quaternion wasHeadRot;  // 0x90

        // Properties
        UnityEngine.Vector3 targetPositionVector { get; /* RVA: 0x0AC6B9F0 */ set; /* RVA: 0x04DA9C90 */ }
        System.String info { get; /* RVA: 0x0AC6B898 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC6B910 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single blendOut { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }

        // Methods
        // RVA: 0x0AC6B208  token: 0x6000606
        protected virtual System.Void OnCreate() { }
        // RVA: 0x0AC6B1BC  token: 0x6000607
        protected virtual System.Void OnAfterValidate() { }
        // RVA: 0x0AC6B2D0  token: 0x6000608
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC6B410  token: 0x6000609
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC6B370  token: 0x600060A
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC6B840  token: 0x600060B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BA
    public abstract class SendMessage`1 : Slate.ActionClips.SendMessage
    {
        // Fields
        public T value;  // 0x0

        // Properties
        System.String info { get; /* RVA: -1  // not resolved */ }
        System.Boolean isValid { get; /* RVA: -1  // not resolved */ }
        System.Type parameterType { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600060F
        protected virtual System.Void OnEnter() { }
        // RVA: -1  // not resolved  token: 0x6000610
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000BB  // size: 0x50
    public class SendMessage : Slate.ActionClips.ActorActionClip
    {
        // Fields
        public System.String message;  // 0x40
        public System.Int32 componentIndex;  // 0x48

        // Properties
        System.String info { get; /* RVA: 0x0AC774A8 */ }
        System.Boolean isValid { get; /* RVA: 0x091DE054 */ }
        System.Type parameterType { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x0AC771D8  token: 0x6000614
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7748C  token: 0x6000615
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BC  // size: 0x58
    public class SendMessageBoolean : Slate.ActionClips.SendMessage`1
    {
        // Methods
        // RVA: 0x0AC770D4  token: 0x6000616
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BD  // size: 0x58
    public class SendMessageFloat : Slate.ActionClips.SendMessage`1
    {
        // Methods
        // RVA: 0x0AC77108  token: 0x6000617
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BE  // size: 0x58
    public class SendMessageInteger : Slate.ActionClips.SendMessage`1
    {
        // Methods
        // RVA: 0x0AC7713C  token: 0x6000618
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BF  // size: 0x58
    public class SendMessageObject : Slate.ActionClips.SendMessage`1
    {
        // Methods
        // RVA: 0x0AC77170  token: 0x6000619
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C0  // size: 0x58
    public class SendMessageString : Slate.ActionClips.SendMessage`1
    {
        // Methods
        // RVA: 0x0AC771A4  token: 0x600061A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C1  // size: 0x48
    public class AnimateActorVisibility : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public System.Boolean visible;  // 0x44
        private System.Boolean wasVisible;  // 0x45

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }

        // Methods
        // RVA: 0x0AC67E58  token: 0x600061D
        protected virtual System.Void OnCreate() { }
        // RVA: 0x0AC67E88  token: 0x600061E
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC67EE8  token: 0x600061F
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC67EB8  token: 0x6000620
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC67F18  token: 0x6000621
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C2  // size: 0x58
    public class MakeCameraTarget : Slate.ActionClips.ActorActionClip
    {
        // Fields
        public System.Boolean setTransposerTarget;  // 0x40
        public System.Boolean setComposerTarget;  // 0x41
        private UnityEngine.Transform wasTransposerTarget;  // 0x48
        private UnityEngine.Transform wasComposerTarget;  // 0x50

        // Methods
        // RVA: 0x0AC72094  token: 0x6000622
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC72220  token: 0x6000623
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC72340  token: 0x6000624
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C3  // size: 0x50
    public class SetActorActiveState : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public Slate.ActiveState activeState;  // 0x44
        private System.Boolean lastState;  // 0x48
        private System.Boolean currentState;  // 0x49
        private System.Boolean temporary;  // 0x4a

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.String info { get; /* RVA: 0x0AC77674 */ }

        // Methods
        // RVA: 0x0AC774E8  token: 0x6000628
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC775B0  token: 0x6000629
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC775EC  token: 0x600062A
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC77628  token: 0x600062B
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC77658  token: 0x600062C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C4  // size: 0x50
    public class SetActorVisibilityTemporary : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public Slate.ActiveState activeState;  // 0x44
        private System.Boolean lastState;  // 0x48
        private System.Boolean currentState;  // 0x49
        private System.Boolean temporary;  // 0x4a

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.String info { get; /* RVA: 0x0AC776F8 */ }

        // Methods
        // RVA: 0x0AC774E8  token: 0x6000630
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC775B0  token: 0x6000631
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC775EC  token: 0x6000632
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC77628  token: 0x6000633
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC776D4  token: 0x6000634
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C5  // size: 0x70
    public class SetBehavioursActiveState : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public System.Collections.Generic.List<System.String> behaviourNames;  // 0x48
        public Slate.ActiveState activeState;  // 0x50
        private System.Collections.Generic.Dictionary<UnityEngine.Behaviour,System.Boolean> originalStates;  // 0x58
        private System.Collections.Generic.Dictionary<UnityEngine.Behaviour,System.Boolean> currentStates;  // 0x60
        private System.Boolean temporary;  // 0x68

        // Properties
        System.String info { get; /* RVA: 0x0AC78064 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }

        // Methods
        // RVA: 0x0AC77758  token: 0x6000638
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC77A78  token: 0x6000639
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC77C2C  token: 0x600063A
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC77DDC  token: 0x600063B
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC77FDC  token: 0x600063C
        public System.Void .ctor() { }
        // RVA: 0x0AC77F64  token: 0x600063D
        private System.Boolean <OnEnter>b__11_0(UnityEngine.Behaviour c) { }

    }

    // TypeToken: 0x20000C6  // size: 0x80
    public class AnimateOnPath : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        public Slate.Path path;  // 0x48
        public System.Single positionOnPath;  // 0x50
        public UnityEngine.Vector3 lookAtTargetPosition;  // 0x54
        public Slate.EaseType blendInterpolation;  // 0x60
        private UnityEngine.Vector3 wasPosition;  // 0x64
        private UnityEngine.Quaternion wasRotation;  // 0x70

        // Properties
        System.String info { get; /* RVA: 0x0AC68EBC */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC68F5C */ }

        // Methods
        // RVA: 0x0AC68A70  token: 0x6000644
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC68B9C  token: 0x6000645
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC68B10  token: 0x6000646
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC68E98  token: 0x6000647
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C7  // size: 0x90
    public class FollowPath : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public Slate.Path path;  // 0x50
        public System.Boolean useSpeed;  // 0x58
        public System.Single speed;  // 0x5c
        public System.Single lookAhead;  // 0x60
        public UnityEngine.Vector3 upVector;  // 0x64
        public Slate.EaseType blendInterpolation;  // 0x70
        private UnityEngine.Vector3 lastPos;  // 0x74
        private UnityEngine.Quaternion lastRot;  // 0x80

        // Properties
        System.String info { get; /* RVA: 0x0AC70D78 */ }
        System.Single length { get; /* RVA: 0x0AC70E68 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC70E18 */ }

        // Methods
        // RVA: 0x0AC708B0  token: 0x6000650
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC709E4  token: 0x6000651
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC70954  token: 0x6000652
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC70D30  token: 0x6000653
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C8  // size: 0xC8
    public class PathfindFromTo : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _blendIn;  // 0x40
        public System.Single speed;  // 0x44
        public Slate.TransformRefPosition sourcePosition;  // 0x48
        public Slate.TransformRefPosition targetPosition;  // 0x68
        private UnityEngine.Vector3[] pathPoints;  // 0x88
        private UnityEngine.Vector3 wasPosition;  // 0x90
        private UnityEngine.Quaternion wasRotation;  // 0x9c
        private UnityEngine.Vector3 lastFrom;  // 0xac
        private UnityEngine.Vector3 lastTo;  // 0xb8

        // Properties
        System.String info { get; /* RVA: 0x0AC73828 */ }
        System.Single length { get; /* RVA: 0x0AC73870 */ }
        System.Single blendIn { get; /* RVA: 0x0AC737F8 */ set; /* RVA: 0x04D86470 */ }

        // Methods
        // RVA: 0x0AC72EDC  token: 0x6000658
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7313C  token: 0x6000659
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC73090  token: 0x600065A
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC73578  token: 0x600065B
        private System.Void TryCalculatePath() { }
        // RVA: 0x0AC737D4  token: 0x600065C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C9  // size: 0xA8
    public class PathfindTo : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _blendIn;  // 0x40
        public System.Single speed;  // 0x44
        public Slate.TransformRefPosition targetPosition;  // 0x48
        private UnityEngine.Vector3[] pathPoints;  // 0x68
        private UnityEngine.Vector3 wasPosition;  // 0x70
        private UnityEngine.Quaternion wasRotation;  // 0x7c
        private UnityEngine.Vector3 lastFrom;  // 0x8c
        private UnityEngine.Vector3 lastTo;  // 0x98

        // Properties
        System.String info { get; /* RVA: 0x0AC741CC */ }
        System.Single length { get; /* RVA: 0x0AC74214 */ }
        System.Single blendIn { get; /* RVA: 0x0AC737F8 */ set; /* RVA: 0x04D86470 */ }

        // Methods
        // RVA: 0x0AC738B4  token: 0x6000661
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC73AEC  token: 0x6000662
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC73A54  token: 0x6000663
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC73F10  token: 0x6000664
        private System.Void TryCalculatePath() { }
        // RVA: 0x0AC737D4  token: 0x6000665
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CA  // size: 0x88
    public class AnimateMaterialColor : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        private System.Single _length;  // 0x48
        private System.Single _blendIn;  // 0x4c
        private System.Single _blendOut;  // 0x50
        public System.String propertyName;  // 0x58
        public UnityEngine.Color color;  // 0x60
        public Slate.EaseType interpolation;  // 0x70
        private UnityEngine.Color originalColor;  // 0x74

        // Properties
        System.String info { get; /* RVA: 0x0AC68568 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC685A8 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single blendOut { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        UnityEngine.Material targetMaterial { get; /* RVA: 0x0AC68694 */ }

        // Methods
        // RVA: 0x0AC68430  token: 0x600066F
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC68430  token: 0x6000670
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC68440  token: 0x6000671
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC68438  token: 0x6000672
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC68438  token: 0x6000673
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC683F4  token: 0x6000674
        private System.Void DoSet() { }
        // RVA: 0x0AC683B4  token: 0x6000675
        private System.Void DoReset() { }
        // RVA: 0x0AC684EC  token: 0x6000676
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CB  // size: 0x70
    public class AnimateMaterialFloat : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        private System.Single _length;  // 0x48
        private System.Single _blendIn;  // 0x4c
        private System.Single _blendOut;  // 0x50
        public System.String propertyName;  // 0x58
        public System.Single value;  // 0x60
        public Slate.EaseType interpolation;  // 0x64
        private System.Single originalValue;  // 0x68

        // Properties
        System.String info { get; /* RVA: 0x0AC6889C */ }
        System.Boolean isValid { get; /* RVA: 0x0AC688DC */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single blendOut { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        UnityEngine.Material targetMaterial { get; /* RVA: 0x0AC689C8 */ }

        // Methods
        // RVA: 0x0AC687A8  token: 0x6000680
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC687A8  token: 0x6000681
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC687B8  token: 0x6000682
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC687B0  token: 0x6000683
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC687B0  token: 0x6000684
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC68770  token: 0x6000685
        private System.Void DoSet() { }
        // RVA: 0x0AC6873C  token: 0x6000686
        private System.Void DoReset() { }
        // RVA: 0x0AC68838  token: 0x6000687
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CC  // size: 0x88
    public class AnimateMaterialTexture : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        private System.Single _length;  // 0x48
        private System.Single _blendIn;  // 0x4c
        private System.Single _blendOut;  // 0x50
        public System.String propertyName;  // 0x58
        public UnityEngine.Vector2 offset;  // 0x60
        public UnityEngine.Vector2 scale;  // 0x68
        public Slate.EaseType interpolation;  // 0x70
        private UnityEngine.Vector2 originalOffset;  // 0x74
        private UnityEngine.Vector2 originalScale;  // 0x7c

        // Properties
        System.String info { get; /* RVA: 0x0AC7DD38 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC7DD78 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single blendOut { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        UnityEngine.Material targetMaterial { get; /* RVA: 0x0AC7DE64 */ }

        // Methods
        // RVA: 0x0AC7DAF8  token: 0x6000691
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7DAF8  token: 0x6000692
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC7DB08  token: 0x6000693
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC7DB00  token: 0x6000694
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7DB00  token: 0x6000695
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC7DA70  token: 0x6000696
        private System.Void DoSet() { }
        // RVA: 0x0AC7D9FC  token: 0x6000697
        private System.Void DoReset() { }
        // RVA: 0x0AC7DCC4  token: 0x6000698
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CD  // size: 0x70
    public class ScrollMaterialTexture : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        private System.Single _length;  // 0x48
        public System.String propertyName;  // 0x50
        public UnityEngine.Vector2 speed;  // 0x58
        public Slate.EaseType interpolation;  // 0x60
        private UnityEngine.Vector2 originalOffset;  // 0x64

        // Properties
        System.String info { get; /* RVA: 0x0AC84B3C */ }
        System.Boolean isValid { get; /* RVA: 0x0AC84BA0 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x0AC7EA6C */ }
        UnityEngine.Material targetMaterial { get; /* RVA: 0x0AC84C4C */ }

        // Methods
        // RVA: 0x0AC848C8  token: 0x600069F
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC84958  token: 0x60006A0
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC84914  token: 0x60006A1
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC84AD4  token: 0x60006A2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CE  // size: 0x68
    public class SetMaterial : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        private System.Single _length;  // 0x48
        public UnityEngine.Material material;  // 0x50
        private UnityEngine.Material sharedMat;  // 0x58
        private System.Boolean temporary;  // 0x60

        // Properties
        System.String info { get; /* RVA: 0x0AC853A8 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC8530C  token: 0x60006A6
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC85350  token: 0x60006A7
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC85360  token: 0x60006A8
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC85340  token: 0x60006A9
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC8528C  token: 0x60006AA
        private System.Void DoSet() { }
        // RVA: 0x0AC85238  token: 0x60006AB
        private System.Void DoReset() { }
        // RVA: 0x0AC85368  token: 0x60006AC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CF  // size: 0x70
    public class SetMaterialTexture : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        private System.Single _length;  // 0x48
        public System.String propertyName;  // 0x50
        public UnityEngine.Texture texture;  // 0x58
        private UnityEngine.Texture originalTexture;  // 0x60
        private System.Boolean temporary;  // 0x68

        // Properties
        System.String info { get; /* RVA: 0x0AC85004 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC850A4 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        UnityEngine.Material targetMaterial { get; /* RVA: 0x0AC85190 */ }

        // Methods
        // RVA: 0x0AC84F58  token: 0x60006B2
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC84F9C  token: 0x60006B3
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC84FAC  token: 0x60006B4
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC84F8C  token: 0x60006B5
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC84EF8  token: 0x60006B6
        private System.Void DoSet() { }
        // RVA: 0x0AC84EC4  token: 0x60006B7
        private System.Void DoReset() { }
        // RVA: 0x0AC84FB4  token: 0x60006B8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D0  // size: 0x60
    public class SetSortingLayer : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        public System.Int32 sortingOrder;  // 0x48
        public System.Int32 sortingLayerID;  // 0x4c
        private System.String _lastLayer;  // 0x50
        private System.Int32 _lastOrder;  // 0x58

        // Properties
        System.String info { get; /* RVA: 0x0AC85A34 */ }

        // Methods
        // RVA: 0x0AC858B8  token: 0x60006BA
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC8597C  token: 0x60006BB
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC859F4  token: 0x60006BC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D1  // size: 0x98
    public class SetSprite : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        public UnityEngine.Sprite sprite;  // 0x48
        public UnityEngine.Color color;  // 0x50
        public System.Boolean flipX;  // 0x60
        public System.Boolean flipY;  // 0x61
        public System.Boolean changeSorting;  // 0x62
        public System.Int32 sortingLayerID;  // 0x64
        public System.Int32 sortingOrder;  // 0x68
        private UnityEngine.Sprite lastSprite;  // 0x70
        private UnityEngine.Color lastColor;  // 0x78
        private System.Boolean lastFlipX;  // 0x88
        private System.Boolean lastFlipY;  // 0x89
        private System.Int32 lastSortingLayerID;  // 0x8c
        private System.Int32 lastSortingOrder;  // 0x90

        // Properties
        System.String info { get; /* RVA: 0x0AC85E8C */ }

        // Methods
        // RVA: 0x0AC85AEC  token: 0x60006BE
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC85D10  token: 0x60006BF
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC85E34  token: 0x60006C0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D2  // size: 0x68
    public class SpriteFlipbook : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        private System.Single _length;  // 0x48
        public System.Int32 loops;  // 0x4c
        public System.Collections.Generic.List<UnityEngine.Sprite> sprites;  // 0x50
        public System.Boolean endWithPrevious;  // 0x58
        private UnityEngine.Sprite lastSprite;  // 0x60

        // Properties
        System.String info { get; /* RVA: 0x0AC86AD8 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC86B98 */ }

        // Methods
        // RVA: 0x0AC867D8  token: 0x60006C5
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC868E4  token: 0x60006C6
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC86834  token: 0x60006C7
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC86890  token: 0x60006C8
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC86A40  token: 0x60006C9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D3  // size: 0x90
    public class AttachObject : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public UnityEngine.Transform targetObject;  // 0x48
        public System.String childTransformName;  // 0x50
        public UnityEngine.Vector3 localPosition;  // 0x58
        public UnityEngine.Vector3 localRotation;  // 0x64
        public UnityEngine.Vector3 localScale;  // 0x70
        private Slate.TransformSnapshot snapshot;  // 0x80
        private System.Boolean temporary;  // 0x88

        // Properties
        System.Boolean isValid { get; /* RVA: 0x0AC7E81C */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }

        // Methods
        // RVA: 0x0AC7E794  token: 0x60006CD
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7E7DC  token: 0x60006CE
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC7E7C8  token: 0x60006CF
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC7E7F0  token: 0x60006D0
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7E568  token: 0x60006D1
        private System.Void Do() { }
        // RVA: 0x0AC7E7F8  token: 0x60006D2
        private System.Void UnDo() { }
        // RVA: 0x0AC7E4E4  token: 0x60006D3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D4  // size: 0x98
    public class AttachObjectPrefab : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public UnityEngine.Transform targetPrefab;  // 0x48
        public System.String childTransformName;  // 0x50
        public UnityEngine.Vector3 localPosition;  // 0x58
        public UnityEngine.Vector3 localRotation;  // 0x64
        public UnityEngine.Vector3 localScale;  // 0x70
        private UnityEngine.Transform instance;  // 0x80
        private Slate.TransformSnapshot snapshot;  // 0x88
        private System.Boolean temporary;  // 0x90

        // Properties
        System.Boolean isValid { get; /* RVA: 0x0AC7E518 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }

        // Methods
        // RVA: 0x0AC7E3E0  token: 0x60006D7
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7E428  token: 0x60006D8
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC7E414  token: 0x60006D9
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC7E43C  token: 0x60006DA
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7E1D4  token: 0x60006DB
        private System.Void Do() { }
        // RVA: 0x0AC7E444  token: 0x60006DC
        private System.Void UnDo() { }
        // RVA: 0x0AC7E4E4  token: 0x60006DD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D5  // size: 0x88
    public class LookAt : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public System.Boolean verticalOnly;  // 0x4c
        public Slate.EaseType interpolation;  // 0x50
        public Slate.TransformRefPosition targetPosition;  // 0x58
        private UnityEngine.Quaternion wasRotation;  // 0x78

        // Properties
        UnityEngine.Vector3 targetPositionVector { get; /* RVA: 0x0AC801D0 */ set; /* RVA: 0x04D91E40 */ }
        System.String info { get; /* RVA: 0x0AC80158 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC7FE38  token: 0x60006E7
        protected virtual System.Void OnCreate() { }
        // RVA: 0x0AC7FDEC  token: 0x60006E8
        protected virtual System.Void OnAfterValidate() { }
        // RVA: 0x0AC7FE74  token: 0x60006E9
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7FF0C  token: 0x60006EA
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC7FEC0  token: 0x60006EB
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC80124  token: 0x60006EC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D6  // size: 0xB8
    public class MatchTransformsToTarget : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public UnityEngine.Transform targetObject;  // 0x50
        public Slate.EaseType interpolation;  // 0x58
        public System.Boolean matchPosition;  // 0x5c
        public UnityEngine.Vector3 positionOffset;  // 0x60
        public System.Boolean matchRotation;  // 0x6c
        public UnityEngine.Vector3 rotationOffset;  // 0x70
        public System.Boolean matchScale;  // 0x7c
        public UnityEngine.Vector3 scaleOffset;  // 0x80
        private UnityEngine.Vector3 lastPos;  // 0x8c
        private UnityEngine.Quaternion lastRot;  // 0x98
        private UnityEngine.Vector3 lastScale;  // 0xa8

        // Properties
        System.String info { get; /* RVA: 0x0AC80908 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC809A8 */ }

        // Methods
        // RVA: 0x0AC80204  token: 0x60006F5
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC803C4  token: 0x60006F6
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC802E0  token: 0x60006F7
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC808CC  token: 0x60006F8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D7  // size: 0x78
    public class Noise : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public UnityEngine.Vector3 seed;  // 0x4c
        public UnityEngine.Vector3 amplitude;  // 0x58
        public System.Single frequency;  // 0x64
        private UnityEngine.Vector3 wasPosition;  // 0x68

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC80D98  token: 0x60006FF
        protected virtual System.Void OnCreate() { }
        // RVA: 0x0AC80E10  token: 0x6000700
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC80E60  token: 0x6000701
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC81038  token: 0x6000702
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D8  // size: 0xA0
    public class RotateAround : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public UnityEngine.Vector3 rotation;  // 0x44
        public System.Boolean perSecond;  // 0x50
        public System.Boolean lookTarget;  // 0x51
        public Slate.EaseType interpolation;  // 0x54
        public Slate.TransformRefPosition targetPosition;  // 0x58
        private UnityEngine.Vector3 wasPosition;  // 0x78
        private UnityEngine.Quaternion wasRotation;  // 0x84
        private UnityEngine.Vector3 targetWasPosition;  // 0x94

        // Properties
        UnityEngine.Vector3 targetPositionVector { get; /* RVA: 0x0AC801D0 */ set; /* RVA: 0x04D91E40 */ }
        System.String info { get; /* RVA: 0x0AC83588 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x0AC7EA6C */ }

        // Methods
        // RVA: 0x0AC82F8C  token: 0x6000709
        protected virtual System.Void OnAfterValidate() { }
        // RVA: 0x0AC82FD8  token: 0x600070A
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC83174  token: 0x600070B
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC830E4  token: 0x600070C
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC83548  token: 0x600070D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D9  // size: 0x68
    public class RotateBy : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public UnityEngine.Vector3 rotation;  // 0x44
        public System.Boolean perSecond;  // 0x50
        public Slate.EaseType interpolation;  // 0x54
        private UnityEngine.Vector3 originalRot;  // 0x58

        // Properties
        System.String info { get; /* RVA: 0x0AC83964 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x0AC7EA6C */ }

        // Methods
        // RVA: 0x0AC83670  token: 0x6000712
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC83784  token: 0x6000713
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC836F4  token: 0x6000714
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC83924  token: 0x6000715
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DA  // size: 0x60
    public class RotateTo : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public UnityEngine.Vector3 targetRotation;  // 0x44
        public Slate.EaseType interpolation;  // 0x50
        private UnityEngine.Vector3 originalRot;  // 0x54

        // Properties
        System.String info { get; /* RVA: 0x0AC83C18 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x0AC7EA6C */ }

        // Methods
        // RVA: 0x0AC83A14  token: 0x600071A
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC83AB8  token: 0x600071B
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC83A64  token: 0x600071C
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7E034  token: 0x600071D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DB  // size: 0x68
    public class ScaleBy : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public UnityEngine.Vector3 scale;  // 0x44
        public System.Boolean perSecond;  // 0x50
        public Slate.EaseType interpolation;  // 0x54
        private UnityEngine.Vector3 originalScale;  // 0x58

        // Properties
        System.String info { get; /* RVA: 0x0AC844A0 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x0AC7EA6C */ }

        // Methods
        // RVA: 0x0AC84240  token: 0x6000722
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC842E4  token: 0x6000723
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC84290  token: 0x6000724
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC84464  token: 0x6000725
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DC  // size: 0x60
    public class ScaleTo : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public UnityEngine.Vector3 targetScale;  // 0x44
        public Slate.EaseType interpolation;  // 0x50
        private UnityEngine.Vector3 originalPos;  // 0x54

        // Properties
        System.String info { get; /* RVA: 0x0AC84754 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x0AC7EA6C */ }

        // Methods
        // RVA: 0x0AC84550  token: 0x600072A
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC845F4  token: 0x600072B
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC845A0  token: 0x600072C
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7E034  token: 0x600072D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DD  // size: 0x88
    public class SetParentTemporary : Slate.ActionClips.ActorActionClip
    {
        // Fields
        public System.Single _length;  // 0x40
        public UnityEngine.Transform newParent;  // 0x48
        public System.Boolean matchPosition;  // 0x50
        public System.Boolean matchRotation;  // 0x51
        public System.Boolean matchScale;  // 0x52
        private UnityEngine.Transform originalParent;  // 0x58
        private UnityEngine.Vector3 originalPos;  // 0x60
        private UnityEngine.Quaternion originalRot;  // 0x6c
        private UnityEngine.Vector3 originalScale;  // 0x7c

        // Properties
        System.String info { get; /* RVA: 0x0AC85818 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }

        // Methods
        // RVA: 0x0AC85448  token: 0x6000731
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC85664  token: 0x6000732
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC85554  token: 0x6000733
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC67F18  token: 0x6000734
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DE  // size: 0x90
    public class SetTransformParent : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public UnityEngine.Transform newParent;  // 0x48
        public System.Boolean resetPosition;  // 0x50
        public System.Boolean resetRotation;  // 0x51
        public System.Boolean resetScale;  // 0x52
        private UnityEngine.Transform originalParent;  // 0x58
        private UnityEngine.Vector3 originalPos;  // 0x60
        private UnityEngine.Quaternion originalRot;  // 0x6c
        private UnityEngine.Vector3 originalScale;  // 0x7c
        private System.Boolean temporary;  // 0x88

        // Properties
        System.String info { get; /* RVA: 0x0AC861CC */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }

        // Methods
        // RVA: 0x0AC86168  token: 0x6000738
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC861B0  token: 0x6000739
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC8619C  token: 0x600073A
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC861C4  token: 0x600073B
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC85F0C  token: 0x600073C
        private System.Void Do() { }
        // RVA: 0x0AC85554  token: 0x600073D
        private System.Void UnDo() { }
        // RVA: 0x05393520  token: 0x600073E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DF  // size: 0x80
    public class SetTransformValues : Slate.ActionClips.ActorActionClip
    {
        // Fields
        public Slate.MiniTransformSpace space;  // 0x40
        public System.Boolean setPosition;  // 0x44
        public UnityEngine.Vector3 position;  // 0x48
        public System.Boolean setRotation;  // 0x54
        public UnityEngine.Vector3 rotation;  // 0x58
        public System.Boolean setScale;  // 0x64
        public UnityEngine.Vector3 scale;  // 0x68
        private Slate.TransformSnapshot undo;  // 0x78

        // Methods
        // RVA: 0x0AC8626C  token: 0x600073F
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC86438  token: 0x6000740
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC86458  token: 0x6000741
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E0  // size: 0xA0
    public class SimpleGrounder : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public System.Single maxCheckDistance;  // 0x44
        public System.Single offset;  // 0x48
        private UnityEngine.RaycastHit hit;  // 0x50
        private UnityEngine.Vector3 lastPos;  // 0x90

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }

        // Methods
        // RVA: 0x0AC8648C  token: 0x6000744
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC8653C  token: 0x6000745
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC864E4  token: 0x6000746
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC867AC  token: 0x6000747
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E1  // size: 0x68
    public class TranslateBy : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public UnityEngine.Vector3 translation;  // 0x44
        public System.Boolean perSecond;  // 0x50
        public Slate.EaseType interpolation;  // 0x54
        private UnityEngine.Vector3 originalPos;  // 0x58

        // Properties
        System.String info { get; /* RVA: 0x0AC87490 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x0AC7EA6C */ }

        // Methods
        // RVA: 0x0AC8722C  token: 0x600074C
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC872D0  token: 0x600074D
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC8727C  token: 0x600074E
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC87450  token: 0x600074F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E2  // size: 0x68
    public class TranslateTo : Slate.ActionClips.ActorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public UnityEngine.Vector3 targetPosition;  // 0x44
        public Slate.MiniTransformSpace space;  // 0x50
        public Slate.EaseType interpolation;  // 0x54
        private UnityEngine.Vector3 wasPosition;  // 0x58

        // Properties
        System.String info { get; /* RVA: 0x0AC87784 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x0AC7EA6C */ }

        // Methods
        // RVA: 0x0AC87540  token: 0x6000754
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC875E4  token: 0x6000755
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC87590  token: 0x6000756
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC87760  token: 0x6000757
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E3  // size: 0x88
    public class AdditiveScene : Slate.DirectorActionClip, Slate.ISubClipContainable, Slate.IDirectable
    {
        // Fields
        private System.Single _length;  // 0x40
        protected System.String _scenePath;  // 0x48
        public UnityEngine.Vector3 scenePosition;  // 0x50
        public UnityEngine.Vector3 sceneRotation;  // 0x5c
        public Slate.MiniTransformSpace space;  // 0x68
        public System.Boolean updateRootCutscenes;  // 0x6c
        public System.Single timeOffset;  // 0x70
        private UnityEngine.SceneManagement.Scene subScene;  // 0x74
        private System.Collections.Generic.List<Slate.Cutscene> rootCutscenes;  // 0x78
        private System.Boolean temporary;  // 0x80
        private System.Boolean waitLoad;  // 0x81

        // Properties
        System.Single Slate.ISubClipContainable.subClipOffset { get; /* RVA: 0x04D90610 */ set; /* RVA: 0x04D90600 */ }
        System.Single Slate.ISubClipContainable.subClipLength { get; /* RVA: 0x04D879D0 */ }
        System.Single Slate.ISubClipContainable.subClipSpeed { get; /* RVA: 0x04D879D0 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC7BD48 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }

        // Methods
        // RVA: 0x0AC7BBAC  token: 0x600075F
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7BC14  token: 0x6000760
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC7BC4C  token: 0x6000761
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC7BBE0  token: 0x6000762
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC7BC28  token: 0x6000763
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7B4E4  token: 0x6000764
        private System.Void Activate() { }
        // RVA: 0x0AC7B5FC  token: 0x6000765
        private System.Void Deactivate() { }
        // RVA: 0x0AC7B578  token: 0x6000766
        private System.String CleanPath(System.String path) { }
        // RVA: 0x0AC7B83C  token: 0x6000767
        private System.Void InitializeSubSceneCutscenes() { }
        // RVA: 0x0AC7B670  token: 0x6000768
        private System.Void DenitializeSubSceneCutscenes(System.Boolean forward) { }
        // RVA: 0x0AC7BD28  token: 0x6000769
        public System.Void .ctor() { }
        // RVA: 0x05396F58  token: 0x600076A
        private virtual System.String Slate.IDirectable.get_name() { }

    }

    // TypeToken: 0x20000E4  // size: 0x58
    public class SubCutscene : Slate.DirectorActionClip, Slate.ISubClipContainable, Slate.IDirectable
    {
        // Fields
        private System.Single _length;  // 0x40
        public Slate.Cutscene subCutscene;  // 0x48
        public System.Single subCutsceneTimeOffset;  // 0x50
        private System.Boolean wasCamTrackActive;  // 0x54

        // Properties
        System.String info { get; /* RVA: 0x0AC87010 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC870D8 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        UnityEngine.GameObject actor { get; /* RVA: 0x0AC86FCC */ }
        System.Single Slate.ISubClipContainable.subClipOffset { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        System.Single Slate.ISubClipContainable.subClipLength { get; /* RVA: 0x0AC86F54 */ }
        System.Single Slate.ISubClipContainable.subClipSpeed { get; /* RVA: 0x0AC86F90 */ }

        // Methods
        // RVA: 0x0AC86C2C  token: 0x6000774
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC86D9C  token: 0x6000775
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC86CF4  token: 0x6000776
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC86E68  token: 0x6000777
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC86F08  token: 0x6000778
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC67F18  token: 0x6000779
        public System.Void .ctor() { }
        // RVA: 0x05396F58  token: 0x600077A
        private virtual System.String Slate.IDirectable.get_name() { }

    }

    // TypeToken: 0x20000E5  // size: 0x48
    public class DestroyGameObjects : Slate.DirectorActionClip
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.GameObject> targetObjects;  // 0x40

        // Properties
        System.String info { get; /* RVA: 0x0AC7ED70 */ }

        // Methods
        // RVA: 0x0AC7EB9C  token: 0x600077C
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7ECF0  token: 0x600077D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E6  // size: 0x50
    public class DirectorDisableGameObjects : Slate.DirectorActionClip
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.GameObject> targetObjects;  // 0x40
        private System.Collections.Generic.Dictionary<UnityEngine.GameObject,System.Boolean> states;  // 0x48

        // Properties
        System.String info { get; /* RVA: 0x0AC7F1C0 */ }

        // Methods
        // RVA: 0x0AC7EDEC  token: 0x600077F
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7EFB4  token: 0x6000780
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7F140  token: 0x6000781
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E7  // size: 0x50
    public class DirectorEnableGameObjects : Slate.DirectorActionClip
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.GameObject> targetObjects;  // 0x40
        private System.Collections.Generic.Dictionary<UnityEngine.GameObject,System.Boolean> states;  // 0x48

        // Properties
        System.String info { get; /* RVA: 0x0AC7F610 */ }

        // Methods
        // RVA: 0x0AC7F23C  token: 0x6000783
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7F404  token: 0x6000784
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7F590  token: 0x6000785
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E8  // size: 0x98
    public class InstantiateObject : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public UnityEngine.GameObject targetObject;  // 0x50
        public UnityEngine.Transform optionalParent;  // 0x58
        public UnityEngine.Vector3 targetPosition;  // 0x60
        public UnityEngine.Vector3 targetRotation;  // 0x6c
        public Slate.MiniTransformSpace space;  // 0x78
        public Slate.EaseType popupInterpolation;  // 0x7c
        private UnityEngine.GameObject instance;  // 0x80
        private UnityEngine.Vector3 wasScale;  // 0x88

        // Properties
        System.Boolean isValid { get; /* RVA: 0x0AC7FD9C */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.String info { get; /* RVA: 0x0AC7FCFC */ }
        UnityEngine.GameObject actor { get; /* RVA: 0x04D86260 */ }

        // Methods
        // RVA: 0x0AC7F788  token: 0x600078F
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7FB88  token: 0x6000790
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC7FA98  token: 0x6000791
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC7FAFC  token: 0x6000792
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC7FB34  token: 0x6000793
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7FCD8  token: 0x6000794
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E9  // size: 0x60
    public class SampleParticleSystem : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public UnityEngine.ParticleSystem particles;  // 0x48
        public System.Boolean simulationSync;  // 0x50
        private UnityEngine.ParticleSystem.EmissionModule em;  // 0x58

        // Properties
        System.String info { get; /* RVA: 0x0AC83F44 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC8405C */ }
        System.Single length { get; /* RVA: 0x0AC840AC */ set; /* RVA: 0x04D86470 */ }
        System.Single blendOut { get; /* RVA: 0x0AC83E78 */ }
        System.Boolean loop { get; /* RVA: 0x0AC8413C */ }
        System.Single duration { get; /* RVA: 0x0AC83EC0 */ }
        System.Single startLifetime { get; /* RVA: 0x0AC841BC */ }

        // Methods
        // RVA: 0x0AC83C84  token: 0x600079D
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC83C84  token: 0x600079E
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC83C8C  token: 0x600079F
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC83C8C  token: 0x60007A0
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC83CE0  token: 0x60007A1
        protected virtual System.Void OnRootEnabled() { }
        // RVA: 0x0AC83C94  token: 0x60007A2
        protected virtual System.Void OnRootDisabled() { }
        // RVA: 0x0AC83D9C  token: 0x60007A3
        private System.Void Play() { }
        // RVA: 0x0AC83D30  token: 0x60007A4
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC83E18  token: 0x60007A5
        private System.Void Stop() { }
        // RVA: 0x0AC83E58  token: 0x60007A6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EA  // size: 0x60
    public class TransitAudioMixerSnapshot : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        public UnityEngine.Audio.AudioMixer audioMixer;  // 0x48
        public System.String snapshotName;  // 0x50
        private UnityEngine.Audio.AudioMixerSnapshot snapshot;  // 0x58

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x0AC7EA6C */ }
        System.Boolean isValid { get; /* RVA: 0x0AC871DC */ }

        // Methods
        // RVA: 0x0AC871D4  token: 0x60007AB
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC871D4  token: 0x60007AC
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC87144  token: 0x60007AD
        private System.Void Do() { }
        // RVA: 0x0AC67F18  token: 0x60007AE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EB  // size: 0x78
    public class AnimateAmbientLighting : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public System.Single ambientIntensity;  // 0x4c
        public UnityEngine.Color ambientColor;  // 0x50
        private System.Single wasIntensity;  // 0x60
        private UnityEngine.Color wasColor;  // 0x64

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC7BD60  token: 0x60007B5
        protected virtual System.Void OnCreate() { }
        // RVA: 0x0AC7BD90  token: 0x60007B6
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7BDF0  token: 0x60007B7
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC7BDC0  token: 0x60007B8
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC67F18  token: 0x60007B9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EC  // size: 0x88
    public class AnimateFog : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public UnityEngine.Color fogColor;  // 0x4c
        public System.Single fogDensity;  // 0x5c
        public System.Single linearFogStartDistance;  // 0x60
        public System.Single linearFogEndDistance;  // 0x64
        private UnityEngine.Color wasColor;  // 0x68
        private System.Single wasDensity;  // 0x78
        private System.Single wasStartDistance;  // 0x7c
        private System.Single wasEndDistance;  // 0x80

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC7C284  token: 0x60007C0
        protected virtual System.Void OnCreate() { }
        // RVA: 0x0AC7C2CC  token: 0x60007C1
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7C364  token: 0x60007C2
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC7C318  token: 0x60007C3
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC67F18  token: 0x60007C4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000ED  // size: 0x68
    public class AnimateGravity : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public UnityEngine.Vector3 gravity;  // 0x4c
        private UnityEngine.Vector3 wasGravity;  // 0x58

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC7C418  token: 0x60007CB
        protected virtual System.Void OnCreate() { }
        // RVA: 0x0AC7C444  token: 0x60007CC
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7C4FC  token: 0x60007CD
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC7C470  token: 0x60007CE
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC67F18  token: 0x60007CF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EE  // size: 0x58
    public class AnimateTimeScale : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public System.Single timeScale;  // 0x4c
        public Slate.EaseType interpolation;  // 0x50
        private System.Single wasScale;  // 0x54

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC7E058  token: 0x60007D6
        protected virtual System.Void OnCreate() { }
        // RVA: 0x0AC7E074  token: 0x60007D7
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7E09C  token: 0x60007D8
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC7E090  token: 0x60007D9
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7E034  token: 0x60007DA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EF  // size: 0x58
    public class EasySlowMotion : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public System.Single timeScale;  // 0x4c
        public Slate.EaseType interpolation;  // 0x50
        private System.Single lastTimeScale;  // 0x54

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.String info { get; /* RVA: 0x0AC7F72C */ }

        // Methods
        // RVA: 0x0AC7E074  token: 0x60007E2
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7F68C  token: 0x60007E3
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC7E090  token: 0x60007E4
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7F6F4  token: 0x60007E5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F0  // size: 0x58
    public class RaiseUnityEvent : Slate.DirectorActionClip
    {
        // Fields
        public System.String customLabel;  // 0x40
        public UnityEngine.Events.UnityEvent forwardEvent;  // 0x48
        public UnityEngine.Events.UnityEvent reverseEvent;  // 0x50

        // Properties
        System.String info { get; /* RVA: 0x0AC82D68 */ }

        // Methods
        // RVA: 0x0AC82C8C  token: 0x60007E7
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC82CAC  token: 0x60007E8
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC82CCC  token: 0x60007E9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F1
    public abstract class SendGlobalMessage`1 : Slate.ActionClips.SendGlobalMessage
    {
        // Fields
        public T value;  // 0x0

        // Properties
        System.String info { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60007EB
        protected virtual System.Void OnEnter() { }
        // RVA: -1  // not resolved  token: 0x60007EC
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000F2  // size: 0x48
    public class SendGlobalMessage : Slate.DirectorActionClip, Slate.IEvent
    {
        // Fields
        public System.String message;  // 0x40

        // Properties
        System.String info { get; /* RVA: 0x0AC84E84 */ }
        System.Boolean isValid { get; /* RVA: 0x091DE054 */ }
        System.String Slate.IEvent.name { get; /* RVA: 0x04D85A60 */ }

        // Methods
        // RVA: 0x0AC84E74  token: 0x60007F0
        private virtual System.Void Slate.IEvent.Invoke() { }
        // RVA: 0x0AC84E34  token: 0x60007F1
        protected virtual System.Void OnEnter() { }
        // RVA: 0x05393520  token: 0x60007F2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F3  // size: 0x50
    public class SendGlobalMessageBoolean : Slate.ActionClips.SendGlobalMessage`1
    {
        // Methods
        // RVA: 0x0AC84CF4  token: 0x60007F3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F4  // size: 0x50
    public class SendGlobalMessageFloat : Slate.ActionClips.SendGlobalMessage`1
    {
        // Methods
        // RVA: 0x0AC84D34  token: 0x60007F4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F5  // size: 0x50
    public class SendGlobalMessageInteger : Slate.ActionClips.SendGlobalMessage`1
    {
        // Methods
        // RVA: 0x0AC84D74  token: 0x60007F5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F6  // size: 0x50
    public class SendGlobalMessageObject : Slate.ActionClips.SendGlobalMessage`1
    {
        // Methods
        // RVA: 0x0AC84DB4  token: 0x60007F6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F7  // size: 0x50
    public class SendGlobalMessageString : Slate.ActionClips.SendGlobalMessage`1
    {
        // Methods
        // RVA: 0x0AC84DF4  token: 0x60007F7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F8  // size: 0x70
    public class Captions : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public System.String text;  // 0x50
        public UnityEngine.Color color;  // 0x58
        public Slate.EaseType interpolation;  // 0x68

        // Properties
        System.String info { get; /* RVA: 0x0AC7E954 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC7E86C  token: 0x60007FF
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC7E8E0  token: 0x6000800
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F9  // size: 0x50
    public class CineBox : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC7E994  token: 0x6000807
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC7E9AC  token: 0x6000808
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FA  // size: 0x80
    public class OverlayText : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public System.String text;  // 0x50
        public UnityEngine.TextAnchor anchor;  // 0x58
        public Slate.EaseType interpolation;  // 0x5c
        public UnityEngine.Color color;  // 0x60
        public System.Single size;  // 0x70
        public UnityEngine.Vector2 position;  // 0x74

        // Properties
        System.String info { get; /* RVA: 0x0AC8119C */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC81084  token: 0x6000810
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC81118  token: 0x6000811
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FB  // size: 0x80
    public class OverlayTexture : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public UnityEngine.Texture texture;  // 0x50
        public UnityEngine.Color color;  // 0x58
        public UnityEngine.Vector2 scale;  // 0x68
        public UnityEngine.Vector2 position;  // 0x70
        public Slate.EaseType interpolation;  // 0x78

        // Properties
        System.String info { get; /* RVA: 0x0AC812C8 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC811DC  token: 0x6000819
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC81278  token: 0x600081A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FC  // size: 0x78
    public class ScreenFader : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public System.Single fade;  // 0x4c
        public UnityEngine.Color outColor;  // 0x50
        public Slate.EaseType interpolation;  // 0x60
        private UnityEngine.Color lastColor;  // 0x64

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC847C0  token: 0x6000821
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC84800  token: 0x6000822
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC847E0  token: 0x6000823
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC8486C  token: 0x6000824
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FD  // size: 0x58
    public class AnimatePlaybackSpeed : Slate.DirectorActionClip
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public System.Single speed;  // 0x4c
        public Slate.EaseType interpolation;  // 0x50
        private System.Single wasSpeed;  // 0x54

        // Properties
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC7DF0C  token: 0x600082B
        protected virtual System.Void OnCreate() { }
        // RVA: 0x0AC7DF3C  token: 0x600082C
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7DF9C  token: 0x600082D
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC7DF6C  token: 0x600082E
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7E034  token: 0x600082F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FE  // size: 0x50
    public class DebugLog : Slate.DirectorActionClip
    {
        // Fields
        public System.Boolean neverSkip;  // 0x40
        public System.String text;  // 0x48

        // Properties
        System.String info { get; /* RVA: 0x0AC7EB5C */ }

        // Methods
        // RVA: 0x0AC7EABC  token: 0x6000831
        protected virtual System.Void OnEnter() { }
        // RVA: 0x05393520  token: 0x6000832
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FF  // size: 0x40
    public class PauseCutscene : Slate.DirectorActionClip
    {
        // Methods
        // RVA: 0x0AC81368  token: 0x6000833
        protected virtual System.Void OnEnter() { }
        // RVA: 0x05393520  token: 0x6000834
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000100  // size: 0x40
    public class ScreenCapture : Slate.DirectorActionClip
    {
        // Methods
        // RVA: 0x05393520  token: 0x6000835
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000101  // size: 0x88
    public class PlayAnimationClip : Slate.ActionClips.ActorActionClip`1, Slate.ISubClipContainable, Slate.IDirectable
    {
        // Fields
        private System.Single _length;  // 0x48
        private System.Single _blendIn;  // 0x4c
        private System.Single _blendOut;  // 0x50
        public UnityEngine.AnimationClip animationClip;  // 0x58
        public System.Single clipOffset;  // 0x60
        public System.Single playbackSpeed;  // 0x64
        private Slate.TransformSnapshot snapShot;  // 0x68
        private UnityEngine.Transform mixTransform;  // 0x70
        private UnityEngine.AnimationState state;  // 0x78
        private System.Boolean isListClip;  // 0x80

        // Properties
        System.Single Slate.ISubClipContainable.subClipOffset { get; /* RVA: 0x04D88480 */ set; /* RVA: 0x04D884A0 */ }
        System.Single Slate.ISubClipContainable.subClipLength { get; /* RVA: 0x0AC81A4C */ }
        System.Single Slate.ISubClipContainable.subClipSpeed { get; /* RVA: 0x04D88470 */ }
        System.String info { get; /* RVA: 0x0AC81B10 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC81B84 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single blendOut { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        System.Boolean canCrossBlend { get; /* RVA: 0x02FFF600 */ }
        Slate.AnimationTrack track { get; /* RVA: 0x0AC81C14 */ }

        // Methods
        // RVA: 0x0AC8145C  token: 0x6000844
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC8170C  token: 0x6000845
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC8168C  token: 0x6000846
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC81644  token: 0x6000847
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC81668  token: 0x6000848
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC81ABC  token: 0x6000849
        public System.Void .ctor() { }
        // RVA: 0x05396F58  token: 0x600084A
        private virtual System.String Slate.IDirectable.get_name() { }

    }

    // TypeToken: 0x2000102  // size: 0xB0
    public class PlayAnimatorClip : Slate.ActionClips.ActorActionClip, Slate.ISubClipContainable, Slate.IDirectable
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public UnityEngine.AnimationClip animationClip;  // 0x50
        public System.Single clipOffset;  // 0x58
        public Slate.ActionClips.PlayAnimatorClip.ClipWrapMode clipWrapMode;  // 0x5c
        public System.Single clipWeight;  // 0x60
        public System.Single playbackSpeed;  // 0x64
        public Slate.ActionClips.PlayAnimatorClip.StartingTransformsMode startingTransformsMode;  // 0x68
        public Slate.MiniTransformSpace transformSpace;  // 0x6c
        public UnityEngine.Vector3 startingPosition;  // 0x70
        public UnityEngine.Vector3 startingRotation;  // 0x7c
        public UnityEngine.Vector2 steerLocalRotation;  // 0x88
        protected UnityEngine.Vector3 wasPosition;  // 0x90
        protected UnityEngine.Quaternion wasRotation;  // 0x9c

        // Properties
        System.Boolean isMasterAndManualSet { get; /* RVA: 0x0AC82578 */ }
        System.Single Slate.ISubClipContainable.subClipOffset { get; /* RVA: 0x04D88400 */ set; /* RVA: 0x04D88430 */ }
        System.Single Slate.ISubClipContainable.subClipLength { get; /* RVA: 0x0AC82440 */ }
        System.Single Slate.ISubClipContainable.subClipSpeed { get; /* RVA: 0x04D88470 */ }
        System.String info { get; /* RVA: 0x0AC82500 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC825C0 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Boolean canCrossBlend { get; /* RVA: 0x02FFF600 */ }
        Slate.AnimatorTrack track { get; /* RVA: 0x0AC8266C */ }
        UnityEngine.Animator animator { get; /* RVA: 0x0AC824DC */ }
        System.Boolean isMasterTrack { get; /* RVA: 0x0AC8259C */ }

        // Methods
        // RVA: 0x0AC81C50  token: 0x600085C
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC81ED8  token: 0x600085D
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC8201C  token: 0x600085E
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC81E74  token: 0x600085F
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC81F3C  token: 0x6000860
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC824B0  token: 0x6000861
        public System.Void .ctor() { }
        // RVA: 0x05396F58  token: 0x6000862
        private virtual System.String Slate.IDirectable.get_name() { }

    }

    // TypeToken: 0x2000105  // size: 0x80
    public class PlayAudio : Slate.ActionClip, Slate.ISubClipContainable, Slate.IDirectable
    {
        // Fields
        private System.Single _length;  // 0x40
        private System.Single _blendIn;  // 0x44
        private System.Single _blendOut;  // 0x48
        public UnityEngine.AudioClip audioClip;  // 0x50
        public System.Single volume;  // 0x58
        public System.Single pitch;  // 0x5c
        public System.Single stereoPan;  // 0x60
        public System.Single clipOffset;  // 0x64
        public System.String subtitlesText;  // 0x68
        public UnityEngine.Color subtitlesColor;  // 0x70

        // Properties
        System.Single Slate.ISubClipContainable.subClipOffset { get; /* RVA: 0x04D88470 */ set; /* RVA: 0x04D88490 */ }
        System.Single Slate.ISubClipContainable.subClipLength { get; /* RVA: 0x0AC82A0C */ }
        System.Single Slate.ISubClipContainable.subClipSpeed { get; /* RVA: 0x04D88DD0 */ }
        System.Single length { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Single blendIn { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single blendOut { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Boolean isValid { get; /* RVA: 0x0AC82BE0 */ }
        System.String info { get; /* RVA: 0x0AC82B58 */ }
        Slate.AudioTrack track { get; /* RVA: 0x0AC82C50 */ }
        UnityEngine.AudioSource source { get; /* RVA: 0x0AC82C30 */ }

        // Methods
        // RVA: 0x0AC8272C  token: 0x6000871
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC8272C  token: 0x6000872
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC82734  token: 0x6000873
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC82734  token: 0x6000874
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC826A8  token: 0x6000875
        private System.Void Do() { }
        // RVA: 0x0AC8273C  token: 0x6000876
        protected virtual System.Void OnUpdate(System.Single time, System.Single previousTime) { }
        // RVA: 0x0AC82A7C  token: 0x6000877
        private System.Void Undo() { }
        // RVA: 0x0AC82AFC  token: 0x6000878
        public System.Void .ctor() { }
        // RVA: 0x05396F58  token: 0x6000879
        private virtual System.String Slate.IDirectable.get_name() { }

    }

    // TypeToken: 0x2000106  // size: 0x60
    public class AnimateBoolParameter : Slate.ActionClips.MecanimBaseClip
    {
        // Fields
        private System.Single _length;  // 0x48
        public System.String parameterName;  // 0x50
        public System.Boolean value;  // 0x58
        private System.Boolean lastValue;  // 0x59

        // Properties
        System.Boolean isValid { get; /* RVA: 0x0AC7C048 */ }
        System.String info { get; /* RVA: 0x0AC7C008 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC7BE70  token: 0x600087E
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7BF9C  token: 0x600087F
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC7BEC8  token: 0x6000880
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC7BF38  token: 0x6000881
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7BFF4  token: 0x6000882
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000107  // size: 0x68
    public class AnimateFloatParameter : Slate.ActionClips.MecanimBaseClip
    {
        // Fields
        private System.Single _length;  // 0x48
        private System.Single _blendIn;  // 0x4c
        private System.Single _blendOut;  // 0x50
        public System.String parameterName;  // 0x58
        public System.Single value;  // 0x60
        private System.Single lastValue;  // 0x64

        // Properties
        System.Boolean isValid { get; /* RVA: 0x0AC7C254 */ }
        System.String info { get; /* RVA: 0x0AC7C214 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single blendOut { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }

        // Methods
        // RVA: 0x0AC7C078  token: 0x600088B
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7C138  token: 0x600088C
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC7C0D4  token: 0x600088D
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7C1F0  token: 0x600088E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000108  // size: 0x68
    public class AnimateIntegerParameter : Slate.ActionClips.MecanimBaseClip
    {
        // Fields
        private System.Single _length;  // 0x48
        private System.Single _blendIn;  // 0x4c
        private System.Single _blendOut;  // 0x50
        public System.String parameterName;  // 0x58
        public System.Int32 value;  // 0x60
        private System.Int32 lastValue;  // 0x64

        // Properties
        System.Boolean isValid { get; /* RVA: 0x0AC7C254 */ }
        System.String info { get; /* RVA: 0x0AC7C774 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single blendOut { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }

        // Methods
        // RVA: 0x0AC7C5E0  token: 0x6000897
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7C69C  token: 0x6000898
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC7C638  token: 0x6000899
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7C758  token: 0x600089A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000109  // size: 0x60
    public class AnimateLayerWeight : Slate.ActionClips.MecanimBaseClip
    {
        // Fields
        private System.Single _length;  // 0x48
        private System.Single _blendIn;  // 0x4c
        private System.Single _blendOut;  // 0x50
        public System.Int32 layerIndex;  // 0x54
        public System.Single weight;  // 0x58
        private System.Single lastValue;  // 0x5c

        // Properties
        System.String info { get; /* RVA: 0x0AC7C94C */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single blendOut { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }

        // Methods
        // RVA: 0x0AC7C7B4  token: 0x60008A2
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7C870  token: 0x60008A3
        protected virtual System.Void OnUpdate(System.Single deltaTime) { }
        // RVA: 0x0AC7C80C  token: 0x60008A4
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7C928  token: 0x60008A5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010A  // size: 0x98
    public class AnimateLimbIK : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        private System.Single _length;  // 0x48
        private System.Single _blendIn;  // 0x4c
        private System.Single _blendOut;  // 0x50
        public UnityEngine.AvatarIKGoal IKGoal;  // 0x54
        public System.Single weight;  // 0x58
        public Slate.TransformRefPositionRotation IKTarget;  // 0x60
        private Slate.AnimatorDispatcher dispatcher;  // 0x90

        // Properties
        UnityEngine.Vector3 targetPosition { get; /* RVA: 0x0AC7D244 */ set; /* RVA: 0x04DA9C90 */ }
        UnityEngine.Vector3 targetRotation { get; /* RVA: 0x0AC7D278 */ set; /* RVA: 0x04DA9D40 */ }
        System.String info { get; /* RVA: 0x0AC7D1D4 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single blendOut { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }

        // Methods
        // RVA: 0x0AC7CDBC  token: 0x60008B1
        protected virtual System.Void OnCreate() { }
        // RVA: 0x0AC7C9AC  token: 0x60008B2
        protected virtual System.Void OnAfterValidate() { }
        // RVA: 0x0AC7CDF8  token: 0x60008B3
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7CF84  token: 0x60008B4
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC7D07C  token: 0x60008B5
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7CEF0  token: 0x60008B6
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC7D110  token: 0x60008B7
        protected virtual System.Void OnRootDisabled() { }
        // RVA: 0x0AC7CBF8  token: 0x60008B8
        private System.Void OnAnimatorIK(System.Int32 index) { }
        // RVA: 0x0AC7D188  token: 0x60008B9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010C  // size: 0x90
    public class AnimateLookAtIK : Slate.ActionClips.ActorActionClip`1
    {
        // Fields
        private System.Single _length;  // 0x48
        private System.Single _blendIn;  // 0x4c
        private System.Single _blendOut;  // 0x50
        public System.Single weight;  // 0x54
        public System.Single bodyWeight;  // 0x58
        public System.Single headWeight;  // 0x5c
        public System.Single eyesWeight;  // 0x60
        public Slate.TransformRefPosition targetPosition;  // 0x68
        private Slate.AnimatorDispatcher dispatcher;  // 0x88

        // Properties
        UnityEngine.Vector3 targetPositionVector { get; /* RVA: 0x0AC7D9C8 */ set; /* RVA: 0x04DA9D60 */ }
        System.String info { get; /* RVA: 0x0AC7D99C */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single blendOut { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }

        // Methods
        // RVA: 0x0AC7D574  token: 0x60008C5
        protected virtual System.Void OnCreate() { }
        // RVA: 0x0AC7D2AC  token: 0x60008C6
        protected virtual System.Void OnAfterValidate() { }
        // RVA: 0x0AC7D5B4  token: 0x60008C7
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7D740  token: 0x60008C8
        protected virtual System.Void OnReverseEnter() { }
        // RVA: 0x0AC7D838  token: 0x60008C9
        protected virtual System.Void OnReverse() { }
        // RVA: 0x0AC7D6AC  token: 0x60008CA
        protected virtual System.Void OnExit() { }
        // RVA: 0x0AC7D8CC  token: 0x60008CB
        protected virtual System.Void OnRootDisabled() { }
        // RVA: 0x0AC7D430  token: 0x60008CC
        private System.Void OnAnimatorIK(System.Int32 index) { }
        // RVA: 0x0AC7D944  token: 0x60008CD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010E  // size: 0x60
    public class AnimateTrigger : Slate.ActionClips.MecanimBaseClip
    {
        // Fields
        private System.Single _length;  // 0x48
        public System.String triggerName;  // 0x50
        public System.Boolean value;  // 0x58

        // Properties
        System.Boolean isValid { get; /* RVA: 0x0AC7C048 */ }
        System.String info { get; /* RVA: 0x0AC7E194 */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }

        // Methods
        // RVA: 0x0AC7E10C  token: 0x60008D4
        protected virtual System.Void OnUpdate(System.Single time) { }
        // RVA: 0x0AC7E184  token: 0x60008D5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010F  // size: 0x58
    public class CrossFadeState : Slate.ActionClips.MecanimBaseClip
    {
        // Fields
        private System.Single _length;  // 0x48
        public System.String stateName;  // 0x50

        // Properties
        System.String info { get; /* RVA: 0x0AC7EA7C */ }
        System.Single length { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single blendIn { get; /* RVA: 0x0AC7EA6C */ }

        // Methods
        // RVA: 0x0AC7E9D8  token: 0x60008DA
        protected virtual System.Void OnEnter() { }
        // RVA: 0x0AC7E184  token: 0x60008DB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000110  // size: 0x48
    public abstract class MecanimBaseClip : Slate.ActionClips.ActorActionClip`1
    {
        // Properties
        System.Boolean isValid { get; /* RVA: 0x0AC80CE4 */ }

        // Methods
        // RVA: 0x0AC809F8  token: 0x60008DD
        protected System.Boolean HasParameter(System.String name) { }
        // RVA: 0x0AC80CA4  token: 0x60008DE
        protected System.Void .ctor() { }

    }

}

