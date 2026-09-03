// ========================================================
// Dumped by @desirepro
// Assembly: Unity.Timeline.dll
// Classes:  196
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x200001E  // size: 0x14
    public sealed struct PostPlaybackState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.ActivationTrack.PostPlaybackState Active;  // const
        public static UnityEngine.Timeline.ActivationTrack.PostPlaybackState Inactive;  // const
        public static UnityEngine.Timeline.ActivationTrack.PostPlaybackState Revert;  // const
        public static UnityEngine.Timeline.ActivationTrack.PostPlaybackState LeaveAsIs;  // const

    }

    // TypeToken: 0x2000020  // size: 0x38
    public sealed struct WeightInfo
    {
        // Fields
        public UnityEngine.Playables.Playable mixer;  // 0x10
        public UnityEngine.Playables.Playable parentMixer;  // 0x20
        public System.Int32 port;  // 0x30

    }

    // TypeToken: 0x2000022  // size: 0x14
    public sealed struct LoopMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.AnimationPlayableAsset.LoopMode UseSourceAsset;  // const
        public static UnityEngine.Timeline.AnimationPlayableAsset.LoopMode On;  // const
        public static UnityEngine.Timeline.AnimationPlayableAsset.LoopMode Off;  // const

    }

    // TypeToken: 0x2000023  // size: 0x14
    public sealed struct ClothResetOption
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.AnimationPlayableAsset.ClothResetOption SoftReset;  // const
        public static UnityEngine.Timeline.AnimationPlayableAsset.ClothResetOption UseClothRelativeTeleport;  // const
        public static UnityEngine.Timeline.AnimationPlayableAsset.ClothResetOption UseStablizationTime;  // const
        public static UnityEngine.Timeline.AnimationPlayableAsset.ClothResetOption DisablePhysics;  // const

    }

    // TypeToken: 0x2000024  // size: 0x14
    public sealed struct AnimationEventTriggerMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode Default;  // const
        public static UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode MuteAllEvents;  // const
        public static UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode SeekAllEvents;  // const

    }

    // TypeToken: 0x2000025  // size: 0x14
    public sealed struct ClipEndAudioBehaviour
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviour Default;  // const
        public static UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviour MuteAll;  // const

    }

    // TypeToken: 0x2000026  // size: 0x14
    public sealed struct Versions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.AnimationPlayableAsset.Versions Initial;  // const
        public static UnityEngine.Timeline.AnimationPlayableAsset.Versions RotationAsEuler;  // const

    }

    // TypeToken: 0x2000027  // size: 0x10
    public static class AnimationPlayableAssetUpgrade
    {
        // Methods
        // RVA: 0x0B33B550  token: 0x60000B4
        public static System.Void ConvertRotationToEuler(UnityEngine.Timeline.AnimationPlayableAsset asset) { }

    }

    // TypeToken: 0x2000028  // size: 0x48
    public sealed class <get_outputs>d__123 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private UnityEngine.Playables.PlayableBinding <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x38
        public UnityEngine.Timeline.AnimationPlayableAsset <>4__this;  // 0x40

        // Properties
        UnityEngine.Playables.PlayableBinding System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding>.Current { get; /* RVA: 0x02EC7B40 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x02EC7BC0 */ }

        // Methods
        // RVA: 0x03A10F90  token: 0x60000B5
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000B6
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x02EC7680  token: 0x60000B7
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x02EC7B70  token: 0x60000B9
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x02EC77A0  token: 0x60000BB
        private virtual System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding>.GetEnumerator() { }
        // RVA: 0x02EC7B60  token: 0x60000BC
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000030  // size: 0x10
    public static class AnimationTrackUpgrade
    {
        // Methods
        // RVA: 0x0B33C064  token: 0x6000122
        public static System.Void ConvertRotationsToEuler(UnityEngine.Timeline.AnimationTrack track) { }
        // RVA: 0x0B33BFFC  token: 0x6000123
        public static System.Void ConvertRootMotion(UnityEngine.Timeline.AnimationTrack track) { }
        // RVA: 0x0B33BFBC  token: 0x6000124
        public static System.Void ConvertInfiniteTrack(UnityEngine.Timeline.AnimationTrack track) { }

    }

    // TypeToken: 0x2000031  // size: 0x48
    public sealed class <get_outputs>d__76 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private UnityEngine.Playables.PlayableBinding <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x38
        public UnityEngine.Timeline.AnimationTrack <>4__this;  // 0x40

        // Properties
        UnityEngine.Playables.PlayableBinding System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding>.Current { get; /* RVA: 0x02EC7B40 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0B345DE4 */ }

        // Methods
        // RVA: 0x03A10F90  token: 0x6000125
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000126
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0441D910  token: 0x6000127
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B345D98  token: 0x6000129
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x04CE4250  token: 0x600012B
        private virtual System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding>.GetEnumerator() { }
        // RVA: 0x0B345D90  token: 0x600012C
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000034  // size: 0x14
    public sealed struct Versions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.TimelineClip.Versions Initial;  // const
        public static UnityEngine.Timeline.TimelineClip.Versions ClipInFromGlobalToLocal;  // const

    }

    // TypeToken: 0x2000035  // size: 0x10
    public static class TimelineClipUpgrade
    {
        // Methods
        // RVA: 0x0B341458  token: 0x6000170
        public static System.Void UpgradeClipInFromGlobalToLocal(UnityEngine.Timeline.TimelineClip clip) { }

    }

    // TypeToken: 0x2000036  // size: 0x14
    public sealed struct ClipExtrapolation
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.TimelineClip.ClipExtrapolation None;  // const
        public static UnityEngine.Timeline.TimelineClip.ClipExtrapolation Hold;  // const
        public static UnityEngine.Timeline.TimelineClip.ClipExtrapolation Loop;  // const
        public static UnityEngine.Timeline.TimelineClip.ClipExtrapolation PingPong;  // const
        public static UnityEngine.Timeline.TimelineClip.ClipExtrapolation Continue;  // const

    }

    // TypeToken: 0x2000037  // size: 0x14
    public sealed struct BlendCurveMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.TimelineClip.BlendCurveMode Auto;  // const
        public static UnityEngine.Timeline.TimelineClip.BlendCurveMode Manual;  // const

    }

    // TypeToken: 0x2000039  // size: 0x14
    public sealed struct Versions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.TimelineAsset.Versions Initial;  // const

    }

    // TypeToken: 0x200003A  // size: 0x10
    public static class TimelineAssetUpgrade
    {
    }

    // TypeToken: 0x200003B  // size: 0x14
    public sealed struct MediaType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.TimelineAsset.MediaType Animation;  // const
        public static UnityEngine.Timeline.TimelineAsset.MediaType Audio;  // const
        public static UnityEngine.Timeline.TimelineAsset.MediaType Texture;  // const
        public static UnityEngine.Timeline.TimelineAsset.MediaType Video;  // const
        public static UnityEngine.Timeline.TimelineAsset.MediaType Script;  // const
        public static UnityEngine.Timeline.TimelineAsset.MediaType Hybrid;  // const
        public static UnityEngine.Timeline.TimelineAsset.MediaType Group;  // const

    }

    // TypeToken: 0x200003C  // size: 0x14
    public sealed struct DurationMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.TimelineAsset.DurationMode BasedOnClips;  // const
        public static UnityEngine.Timeline.TimelineAsset.DurationMode FixedLength;  // const

    }

    // TypeToken: 0x200003D  // size: 0x20
    public class EditorSettings
    {
        // Fields
        private static readonly System.Double kMinFrameRate;  // static @ 0x0
        private static readonly System.Double kMaxFrameRate;  // static @ 0x8
        private static readonly System.Double kDefaultFrameRate;  // static @ 0x10
        private System.Double m_Framerate;  // 0x10
        private System.Boolean m_ScenePreview;  // 0x18

        // Properties
        System.Double frameRate { get; /* RVA: 0x04D96320 */ set; /* RVA: 0x0B33F644 */ }

        // Methods
        // RVA: 0x039F4B00  token: 0x60001A3
        public System.Void .ctor() { }
        // RVA: 0x04D1DB60  token: 0x60001A4
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003E  // size: 0x58
    public sealed class <get_outputs>d__28 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private UnityEngine.Playables.PlayableBinding <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x38
        public UnityEngine.Timeline.TimelineAsset <>4__this;  // 0x40
        private System.Collections.Generic.IEnumerator<UnityEngine.Timeline.TrackAsset> <>7__wrap1;  // 0x48
        private System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> <>7__wrap2;  // 0x50

        // Properties
        UnityEngine.Playables.PlayableBinding System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding>.Current { get; /* RVA: 0x02EC7B40 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0B345B6C */ }

        // Methods
        // RVA: 0x03A10F90  token: 0x60001A5
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x04CD8100  token: 0x60001A6
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03FC8FA0  token: 0x60001A7
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x03FC8DD0  token: 0x60001A8
        private System.Void <>m__Finally1() { }
        // RVA: 0x03FC8F40  token: 0x60001A9
        private System.Void <>m__Finally2() { }
        // RVA: 0x0B345B20  token: 0x60001AB
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x04A21450  token: 0x60001AD
        private virtual System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding>.GetEnumerator() { }
        // RVA: 0x0B345B18  token: 0x60001AE
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000040  // size: 0x14
    public sealed struct Versions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.TrackAsset.Versions Initial;  // const
        public static UnityEngine.Timeline.TrackAsset.Versions RotationAsEuler;  // const
        public static UnityEngine.Timeline.TrackAsset.Versions RootMotionUpgrade;  // const
        public static UnityEngine.Timeline.TrackAsset.Versions AnimatedTrackProperties;  // const

    }

    // TypeToken: 0x2000041  // size: 0x10
    public static class TrackAssetUpgrade
    {
    }

    // TypeToken: 0x2000042  // size: 0x28
    public sealed struct TransientBuildData
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Timeline.TrackAsset> trackList;  // 0x10
        public System.Collections.Generic.List<UnityEngine.Timeline.TimelineClip> clipList;  // 0x18
        public System.Collections.Generic.List<UnityEngine.Timeline.IMarker> markerList;  // 0x20

        // Methods
        // RVA: 0x048BB060  token: 0x600021B
        public static UnityEngine.Timeline.TrackAsset.TransientBuildData Create() { }
        // RVA: 0x039EDBA0  token: 0x600021C
        public System.Void Clear() { }

    }

    // TypeToken: 0x2000043  // size: 0x48
    public sealed class <get_outputs>d__75 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private UnityEngine.Playables.PlayableBinding <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x38
        public UnityEngine.Timeline.TrackAsset <>4__this;  // 0x40

        // Properties
        UnityEngine.Playables.PlayableBinding System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding>.Current { get; /* RVA: 0x02EC7B40 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0B345D40 */ }

        // Methods
        // RVA: 0x03A10F90  token: 0x600021D
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600021E
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0441DAE0  token: 0x600021F
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B345CF4  token: 0x6000221
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x03A10FB0  token: 0x6000223
        private virtual System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding>.GetEnumerator() { }
        // RVA: 0x0B345CEC  token: 0x6000224
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000044  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Timeline.TrackAsset.<>c <>9;  // static @ 0x0
        public static System.Comparison<UnityEngine.Timeline.TimelineClip> <>9__132_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D489D0  token: 0x6000225
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000226
        public System.Void .ctor() { }
        // RVA: 0x0B34592C  token: 0x6000227
        private System.Int32 <SortClips>b__132_0(UnityEngine.Timeline.TimelineClip clip1, UnityEngine.Timeline.TimelineClip clip2) { }

    }

    // TypeToken: 0x200004C  // size: 0x48
    public sealed class <get_outputs>d__16 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private UnityEngine.Playables.PlayableBinding <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x38
        public UnityEngine.Timeline.AudioPlayableAsset <>4__this;  // 0x40

        // Properties
        UnityEngine.Playables.PlayableBinding System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding>.Current { get; /* RVA: 0x02EC7B40 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0B345AC8 */ }

        // Methods
        // RVA: 0x03A10F90  token: 0x6000239
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600023A
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B345968  token: 0x600023B
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B345A7C  token: 0x600023D
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0B3459E8  token: 0x600023F
        private virtual System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding>.GetEnumerator() { }
        // RVA: 0x0B345A74  token: 0x6000240
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200004E  // size: 0x48
    public sealed class <get_outputs>d__4 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private UnityEngine.Playables.PlayableBinding <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x38
        public UnityEngine.Timeline.AudioTrack <>4__this;  // 0x40

        // Properties
        UnityEngine.Playables.PlayableBinding System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding>.Current { get; /* RVA: 0x02EC7B40 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0B345C9C */ }

        // Methods
        // RVA: 0x03A10F90  token: 0x6000246
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000247
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B345968  token: 0x6000248
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B345C50  token: 0x600024A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0B345BBC  token: 0x600024C
        private virtual System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding>.GetEnumerator() { }
        // RVA: 0x0B345C48  token: 0x600024D
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000055  // size: 0x14
    public sealed struct DescriptorBlendTreeType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.BlendTreeDescriptor.DescriptorBlendTreeType Direct;  // const
        public static UnityEngine.Timeline.BlendTreeDescriptor.DescriptorBlendTreeType Simple1D;  // const
        public static UnityEngine.Timeline.BlendTreeDescriptor.DescriptorBlendTreeType Simple2D;  // const
        public static UnityEngine.Timeline.BlendTreeDescriptor.DescriptorBlendTreeType Complex2D;  // const

    }

    // TypeToken: 0x200005D  // size: 0x48
    public sealed class <GetControlableScripts>d__53 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private UnityEngine.MonoBehaviour <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20
        private UnityEngine.GameObject root;  // 0x28
        public UnityEngine.GameObject <>3__root;  // 0x30
        private UnityEngine.MonoBehaviour[] <>7__wrap1;  // 0x38
        private System.Int32 <>7__wrap2;  // 0x40

        // Properties
        UnityEngine.MonoBehaviour System.Collections.Generic.IEnumerator<UnityEngine.MonoBehaviour>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x600028F
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000290
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0470FB40  token: 0x6000291
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B3521C8  token: 0x6000293
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x049D6A30  token: 0x6000295
        private virtual System.Collections.Generic.IEnumerator<UnityEngine.MonoBehaviour> System.Collections.Generic.IEnumerable<UnityEngine.MonoBehaviour>.GetEnumerator() { }
        // RVA: 0x0B3521C0  token: 0x6000296
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000060  // size: 0x38
    public class TransformAdditive
    {
        // Fields
        public UnityEngine.Vector3 position;  // 0x10
        public UnityEngine.Vector3 rotationEuler;  // 0x1c
        public UnityEngine.Vector3 scale;  // 0x28

        // Methods
        // RVA: 0x0B3521B8  token: 0x60002A0
        public System.Void .ctor() { }
        // RVA: 0x0B352158  token: 0x60002A1
        public System.Void Reset() { }

    }

    // TypeToken: 0x2000062  // size: 0x38
    public class TransformAdditive
    {
        // Fields
        public UnityEngine.Vector3 position;  // 0x10
        public UnityEngine.Vector3 rotationEular;  // 0x1c
        public UnityEngine.Vector3 scale;  // 0x28

        // Methods
        // RVA: 0x0B352158  token: 0x60002A7
        public System.Void Reset() { }
        // RVA: 0x041E1670  token: 0x60002A8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000065  // size: 0x28
    public class AnimationCurveInfo
    {
        // Fields
        public System.String path;  // 0x10
        public System.String propertyName;  // 0x18
        public UnityEngine.AnimationCurve curve;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x60002B4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000077
    public sealed struct Entry
    {
        // Fields
        public System.Int64 intervalStart;  // 0x0
        public System.Int64 intervalEnd;  // 0x0
        public T item;  // 0x0

    }

    // TypeToken: 0x2000085  // size: 0x20
    public class EventKeyValue
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.Timeline.SignalAsset> m_Signals;  // 0x10
        private System.Collections.Generic.List<UnityEngine.Events.UnityEvent> m_Events;  // 0x18

        // Properties
        System.Collections.Generic.List<UnityEngine.Timeline.SignalAsset> signals { get; /* RVA: 0x02B2ECC0 */ }
        System.Collections.Generic.List<UnityEngine.Events.UnityEvent> events { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0B349290  token: 0x6000381
        public System.Boolean TryGetValue(UnityEngine.Timeline.SignalAsset key, UnityEngine.Events.UnityEvent& value) { }
        // RVA: 0x0B3490E8  token: 0x6000382
        public System.Void Append(UnityEngine.Timeline.SignalAsset key, UnityEngine.Events.UnityEvent value) { }
        // RVA: 0x0B349214  token: 0x6000383
        public System.Void Remove(System.Int32 idx) { }
        // RVA: 0x0B349170  token: 0x6000384
        public System.Void Remove(UnityEngine.Timeline.SignalAsset key) { }
        // RVA: 0x0B349338  token: 0x6000387
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008C  // size: 0x14
    public sealed struct PostPlaybackState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState Active;  // const
        public static UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState Inactive;  // const
        public static UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState Revert;  // const

    }

    // TypeToken: 0x200008D  // size: 0x14
    public sealed struct InitialState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.ActivationControlPlayable.InitialState Unset;  // const
        public static UnityEngine.Timeline.ActivationControlPlayable.InitialState Active;  // const
        public static UnityEngine.Timeline.ActivationControlPlayable.InitialState Inactive;  // const

    }

    // TypeToken: 0x2000098  // size: 0x28
    public sealed struct NotificationEntry
    {
        // Fields
        public System.Double time;  // 0x10
        public UnityEngine.Playables.INotification payload;  // 0x18
        public System.Boolean notificationFired;  // 0x20
        public UnityEngine.Timeline.NotificationFlags flags;  // 0x22

        // Properties
        System.Boolean triggerInEditor { get; /* RVA: 0x0AE0AE18 */ }
        System.Boolean prewarm { get; /* RVA: 0x0B349EA4 */ }
        System.Boolean triggerOnce { get; /* RVA: 0x0B349EAC */ }

    }

    // TypeToken: 0x2000099  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Timeline.TimeNotificationBehaviour.<>c <>9;  // static @ 0x0
        public static System.Comparison<UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry> <>9__15_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B3522CC  token: 0x60003E9
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60003EA
        public System.Void .ctor() { }
        // RVA: 0x0B352214  token: 0x60003EB
        private System.Int32 <SortNotifications>b__15_0(UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry x, UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry y) { }

    }

    // TypeToken: 0x20000A9  // size: 0x14
    public sealed struct UseEditMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.TimeFieldAttribute.UseEditMode None;  // const
        public static UnityEngine.Timeline.TimeFieldAttribute.UseEditMode ApplyEditMode;  // const

    }

    // TypeToken: 0x20000B3  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Timeline.Extrapolation.<>c <>9;  // static @ 0x0
        public static System.Comparison<UnityEngine.Timeline.TimelineClip> <>9__2_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D47230  token: 0x600043C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600043D
        public System.Void .ctor() { }
        // RVA: 0x0B34592C  token: 0x600043E
        private System.Int32 <SortClipsByStartTime>b__2_0(UnityEngine.Timeline.TimelineClip clip1, UnityEngine.Timeline.TimelineClip clip2) { }

    }

    // TypeToken: 0x20000BB  // size: 0x18
    public sealed class <>c__DisplayClass0_0
    {
        // Fields
        public System.String name;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000459
        public System.Void .ctor() { }
        // RVA: 0x0B352230  token: 0x600045A
        private System.Boolean <GenerateUniqueActorName>b__0(UnityEngine.ScriptableObject x) { }

    }

    // TypeToken: 0x20000BC  // size: 0x18
    public sealed class <>c__DisplayClass0_1
    {
        // Fields
        public System.String result;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600045B
        public System.Void .ctor() { }
        // RVA: 0x0B352230  token: 0x600045C
        private System.Boolean <GenerateUniqueActorName>b__1(UnityEngine.ScriptableObject x) { }

    }

    // TypeToken: 0x20000C1  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Timeline.TimeUtility.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x0B352268  token: 0x600046A
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600046B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C4  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Methods
        // RVA: 0x0687A850  token: 0x600046F
        private static System.UInt32 ComputeStringHash(System.String s) { }

    }

namespace Beyond.Gameplay.Core
{

    // TypeToken: 0x2000010  // size: 0x10
    public class DontAutoBindAttribute : Beyond.Gameplay.Core.DontAutoBindRuntimeAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000012
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x10
    public class DontAutoBindRuntimeAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000013
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x30
    public class BeyondUseAutoBindingPlayableAsset : UnityEngine.Playables.PlayableAsset
    {
        // Fields
        public System.Boolean useAutoBinding;  // 0x18
        public System.String autoBindingPath;  // 0x20
        protected UnityEngine.GameObject m_bindingGo;  // 0x28

        // Properties
        System.Boolean showBaseParam { get; /* RVA: 0x02FFF600 */ }
        UnityEngine.GameObject bindingGameObject { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }

        // Methods
        // RVA: 0x0B33E76C  token: 0x6000017
        public System.Void SetBindingGameObject(UnityEngine.GameObject tBindingGameObject) { }
        // RVA: 0x041E1670  token: 0x6000018
        protected virtual System.Void OnBindGoChanged() { }
        // RVA: 0x0B33E720  token: 0x6000019
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner) { }
        // RVA: 0x03573370  token: 0x600001A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000013  // size: 0x10
    public class RuntimeJumpBehaviour : UnityEngine.Playables.PlayableBehaviour
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600001B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000014  // size: 0x30
    public class RuntimeJumpClip : UnityEngine.Playables.PlayableAsset, UnityEngine.Timeline.ITimelineClipAsset
    {
        // Fields
        public System.Boolean isReverseJump;  // 0x18
        public System.Boolean needChangeOptionAfterJump;  // 0x19
        public System.Int32 optionIndexAfterJump;  // 0x1c
        public System.Double crossFadeDurationAfterJump;  // 0x20
        public System.Boolean isJumpFirst;  // 0x28

        // Properties
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x04DBA570 */ }

        // Methods
        // RVA: 0x0B33F664  token: 0x600001D
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner) { }
        // RVA: 0x03573370  token: 0x600001E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000015  // size: 0xC0
    public class RuntimeJumpTrack : UnityEngine.Timeline.TrackAsset
    {
        // Methods
        // RVA: 0x0B33F6FC  token: 0x600001F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000016  // size: 0x10
    public class RuntimeLoopBehaviour : UnityEngine.Playables.PlayableBehaviour
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000020
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x20
    public class RuntimeLoopClip : UnityEngine.Playables.PlayableAsset, UnityEngine.Timeline.ITimelineClipAsset
    {
        // Fields
        public System.Double crossFadeDuration;  // 0x18

        // Properties
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x04DB8E50 */ }

        // Methods
        // RVA: 0x0B33F748  token: 0x6000022
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner) { }
        // RVA: 0x03573370  token: 0x6000023
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000018  // size: 0xC0
    public class RuntimeLoopTrack : UnityEngine.Timeline.TrackAsset
    {
        // Methods
        // RVA: 0x0B33F7E0  token: 0x6000024
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x40
    public class TimelineNPCLookAtController : UnityEngine.MonoBehaviour
    {
        // Fields
        private static readonly System.Collections.Generic.HashSet<Beyond.Gameplay.Core.TimelineNPCLookAtController> s_instances;  // static @ 0x0
        public System.Boolean m_lookAtDirty;  // 0x18
        public ScriptAnimation.TimelineNPCLookAtTarget lookAtTarget;  // 0x20
        public ScriptAnimation.TimelineNPCLookAtEyeTarget lookAtEyeTarget;  // 0x28
        public ScriptAnimation.TimelineNPCLookAtTargetV2 lookAtTargetV2;  // 0x30
        public System.Boolean lookAtEnabled;  // 0x38
        public System.Boolean lookAtEyeEnabled;  // 0x39

        // Properties
        System.Collections.Generic.IEnumerable<Beyond.Gameplay.Core.TimelineNPCLookAtController> Instances { get; /* RVA: 0x0B342314 */ }
        System.Boolean lookAtDirty { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }
        System.Single lookAtWeight { get; /* RVA: 0x0B342B08 */ set; /* RVA: 0x0B3432B8 */ }
        System.Single lookAtBodyWeight { get; /* RVA: 0x0B3425D8 */ set; /* RVA: 0x0B342E0C */ }
        System.Single lookAtBlendWeight { get; /* RVA: 0x0B342564 */ set; /* RVA: 0x0B342D94 */ }
        System.Single lookAtOverrideWeight { get; /* RVA: 0x0B342888 */ }
        System.Single lookAtAdditiveWeight { get; /* RVA: 0x0B3424E4 */ }
        System.Single lookAtRoll { get; /* RVA: 0x0B342924 */ set; /* RVA: 0x0B3430C8 */ }
        System.Single lookAtHorizontal { get; /* RVA: 0x0B3427D0 */ set; /* RVA: 0x0B343014 */ }
        System.Single lookAtVertical { get; /* RVA: 0x0B342A50 */ set; /* RVA: 0x0B343204 */ }
        System.Single lookAtEyeWeight { get; /* RVA: 0x0B342754 */ set; /* RVA: 0x0B342F9C */ }
        System.Boolean lookAtUseDegree { get; /* RVA: 0x0B3429C0 */ set; /* RVA: 0x0B343168 */ }
        System.Boolean EyeUseDegree { get; /* RVA: 0x0B3422A8 */ set; /* RVA: 0x0B342BA0 */ }
        System.Single lookAtEyeUp { get; /* RVA: 0x0B3426E4 */ set; /* RVA: 0x0B342F24 */ }
        System.Single lookAtEyeDown { get; /* RVA: 0x0B342674 */ set; /* RVA: 0x0B342EAC */ }
        UnityEngine.Vector3 finalPosition { get; /* RVA: 0x0B342410 */ set; /* RVA: 0x0B342CB8 */ }
        UnityEngine.Vector3 finalEyePosition { get; /* RVA: 0x0B342364 */ set; /* RVA: 0x0B342C18 */ }

        // Methods
        // RVA: 0x0B342190  token: 0x6000026
        private System.Void OnEnable() { }
        // RVA: 0x0B342120  token: 0x6000027
        private System.Void OnDisable() { }
        // RVA: 0x041E1670  token: 0x6000046
        public System.Void MarkLookAtDirty() { }
        // RVA: 0x0B34228C  token: 0x6000047
        public System.Void .ctor() { }
        // RVA: 0x0B342200  token: 0x6000048
        private static System.Void .cctor() { }

    }

}

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x200000B  // size: 0x10
    public class DataNAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600000E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x10
    public class ECSComponentAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600000F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000010
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000E  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
    }

    // TypeToken: 0x200000F  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000011
        public System.Void .ctor() { }

    }

}

namespace Events
{

    // TypeToken: 0x2000009
    public interface INotificationTrack
    {
    }

    // TypeToken: 0x200000A
    public interface ITimelineNotification
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600000D
        public virtual System.Void OnNotify(UnityEngine.Timeline.ITimelineRootMarker root) { }

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

namespace ScriptAnimation
{

    // TypeToken: 0x2000003  // size: 0x38
    public class TimelineNPCClothWeightController : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Boolean forceSetPhysicsClothWeight;  // 0x18
        public System.Single physicsClothWeight;  // 0x1c
        public System.Boolean forceSetPhysicsClothPart1Weight;  // 0x20
        public System.Single physicsClothPart1Weight;  // 0x24
        public System.Boolean forceSetPhysicsClothPart2Weight;  // 0x28
        public System.Single physicsClothPart2Weight;  // 0x2c
        public System.Boolean forceSetLoopBodyAddWeight;  // 0x30
        public System.Single loopBodyAddWeight;  // 0x34

        // Methods
        // RVA: 0x0B3420EC  token: 0x6000002
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x28
    public class TimelineNPCLookAtEyeTarget : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Single lookAtWeight;  // 0x18
        public System.Boolean useDegree;  // 0x1c
        public System.Single lookAtUp;  // 0x20
        public System.Single lookAtDown;  // 0x24

        // Methods
        // RVA: 0x04D9A6D0  token: 0x6000003
        public System.Void ResetLookAtWeight() { }
        // RVA: 0x05393520  token: 0x6000004
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x30
    public class TimelineNPCLookAtTarget : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Single lookAtWeight;  // 0x18
        public System.Single lookAtBodyWeight;  // 0x1c
        public System.Boolean useDegree;  // 0x20
        public System.Single lookAtHorizontal;  // 0x24
        public System.Single lookAtVertical;  // 0x28
        public System.Single lookAtRoll;  // 0x2c

        // Methods
        // RVA: 0x0B343350  token: 0x6000005
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x30
    public class TimelineNPCLookAtTargetV2 : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Single lookAtBlendWeight;  // 0x18
        public System.Single lookAtBodyWeight;  // 0x1c
        public System.Boolean useDegree;  // 0x20
        public System.Single lookAtHorizontal;  // 0x24
        public System.Single lookAtVertical;  // 0x28
        public System.Single lookAtRoll;  // 0x2c

        // Methods
        // RVA: 0x0B343350  token: 0x6000006
        public System.Void .ctor() { }

    }

}

namespace ScriptAnimation.ScriptAnimationApply
{

    // TypeToken: 0x2000007  // size: 0x18
    public class ScriptAnimationApplyBehaviour : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        private static readonly Unity.Profiling.ProfilerMarker s_calcLayerMainStreamMarker;  // static @ 0x0
        private UnityEngine.Timeline.IScriptAnimationJobSync m_JobSync;  // 0x10

        // Methods
        // RVA: 0x0B33F82C  token: 0x6000007
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x053908C0  token: 0x6000008
        public System.Void InitBehaviour(UnityEngine.Timeline.IScriptAnimationJobSync jobSync) { }
        // RVA: 0x041E1670  token: 0x6000009
        public System.Void .ctor() { }
        // RVA: 0x02EC7070  token: 0x600000A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x18
    public class ScriptAnimationApplyPlayable : UnityEngine.Playables.PlayableAsset
    {
        // Methods
        // RVA: 0x0B33F980  token: 0x600000B
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner) { }
        // RVA: 0x03573370  token: 0x600000C
        public System.Void .ctor() { }

    }

}

namespace UnityEngine
{

    // TypeToken: 0x200001A  // size: 0x10
    public static class PlayerLoopChecker
    {
    }

}

namespace UnityEngine.Timeline
{

    // TypeToken: 0x200001B  // size: 0x38
    public class ActivationMixerPlayable : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        private UnityEngine.Timeline.ActivationTrack.PostPlaybackState m_PostPlaybackState;  // 0x10
        private System.Boolean m_BoundGameObjectInitialStateIsActive;  // 0x14
        private System.Boolean m_useAutoBinding;  // 0x15
        private UnityEngine.Timeline.ITimelineRootMarker m_RootMarker;  // 0x18
        private System.String m_uniqueId;  // 0x20
        private System.Nullable<System.Boolean> m_lastEntityActive;  // 0x28
        private UnityEngine.GameObject m_BoundGameObject;  // 0x30

        // Properties
        UnityEngine.Timeline.ActivationTrack.PostPlaybackState postPlaybackState { set; /* RVA: 0x014F51F0 */ }

        // Methods
        // RVA: 0x0B33AA78  token: 0x6000049
        public static UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.ActivationMixerPlayable> Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount) { }
        // RVA: 0x0B33AD24  token: 0x600004B
        public System.Void SetBoundGameObject(UnityEngine.GameObject go) { }
        // RVA: 0x0B33ACFC  token: 0x600004C
        public System.Void SetAutoBindingData(System.Boolean useAutoBinding, UnityEngine.Timeline.ITimelineRootMarker rootMarker, System.String uniqueId) { }
        // RVA: 0x041E1670  token: 0x600004D
        public virtual System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x0B33AB04  token: 0x600004E
        public virtual System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B33AC1C  token: 0x600004F
        public virtual System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData) { }
        // RVA: 0x0B33AB48  token: 0x6000050
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B33ADB4  token: 0x6000051
        private System.Void _DoSetActive(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData) { }
        // RVA: 0x041E1670  token: 0x6000052
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x18
    public class ActivationPlayableAsset : UnityEngine.Playables.PlayableAsset, UnityEngine.Timeline.ITimelineClipAsset
    {
        // Properties
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x0B33B024  token: 0x6000054
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go) { }
        // RVA: 0x03573370  token: 0x6000055
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001D  // size: 0xD0
    public class ActivationTrack : UnityEngine.Timeline.TrackAsset
    {
        // Fields
        private UnityEngine.Timeline.ActivationTrack.PostPlaybackState m_PostPlaybackState;  // 0xc0
        private UnityEngine.Timeline.ActivationMixerPlayable m_ActivationMixer;  // 0xc8

        // Properties
        UnityEngine.Timeline.ActivationTrack.PostPlaybackState postPlaybackState { get; /* RVA: 0x04DA4B70 */ set; /* RVA: 0x0B33B540 */ }

        // Methods
        // RVA: 0x0B33B090  token: 0x6000056
        private virtual System.Boolean CanCompileClips() { }
        // RVA: 0x0B33B0C0  token: 0x6000059
        public virtual UnityEngine.Playables.Playable CreateTrackMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount) { }
        // RVA: 0x0B33B498  token: 0x600005A
        private System.Void _BindUniqueId(UnityEngine.Playables.PlayableGraph graph) { }
        // RVA: 0x0B33B32C  token: 0x600005B
        private UnityEngine.GameObject GetBinding(UnityEngine.Playables.PlayableDirector director) { }
        // RVA: 0x0B33B47C  token: 0x600005C
        private System.Void UpdateTrackMode() { }
        // RVA: 0x0B33B28C  token: 0x600005D
        public virtual System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver) { }
        // RVA: 0x0B33B42C  token: 0x600005E
        protected virtual System.Void OnCreateClip(UnityEngine.Timeline.TimelineClip clip) { }
        // RVA: 0x0B33B4EC  token: 0x600005F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x30
    public class AnimationOutputWeightProcessor : UnityEngine.Timeline.ITimelineEvaluateCallback
    {
        // Fields
        private UnityEngine.Animator m_outputAnimator;  // 0x10
        private UnityEngine.Animations.AnimationPlayableOutput m_Output;  // 0x18
        private readonly System.Collections.Generic.List<UnityEngine.Timeline.AnimationOutputWeightProcessor.WeightInfo> m_Mixers;  // 0x28

        // Methods
        // RVA: 0x03E117C0  token: 0x6000060
        public System.Void .ctor(UnityEngine.Animations.AnimationPlayableOutput output, UnityEngine.Animator animator) { }
        // RVA: 0x03E10EA0  token: 0x6000061
        private System.Void FindMixers() { }
        // RVA: 0x03E0FE10  token: 0x6000062
        private System.Void FindMixers(UnityEngine.Playables.Playable parent, System.Int32 port, UnityEngine.Playables.Playable node) { }
        // RVA: 0x03E118A0  token: 0x6000063
        public virtual System.Void Evaluate() { }

    }

    // TypeToken: 0x2000021  // size: 0xB8
    public class AnimationPlayableAsset : UnityEngine.Playables.PlayableAsset, UnityEngine.Timeline.ITimelineClipAsset, UnityEngine.Timeline.IPropertyPreview, UnityEngine.Timeline.ICustomControlBehaviour, UnityEngine.ISerializationCallbackReceiver
    {
        // Fields
        private UnityEngine.AnimationClip m_Clip;  // 0x18
        private UnityEngine.Vector3 m_Position;  // 0x20
        private UnityEngine.Vector3 m_EulerAngles;  // 0x2c
        private System.Boolean m_UseTrackMatchFields;  // 0x38
        private UnityEngine.Timeline.MatchTargetFields m_MatchTargetFields;  // 0x3c
        private System.Boolean m_RemoveStartOffset;  // 0x40
        private System.Boolean m_ApplyFootIK;  // 0x41
        private UnityEngine.Timeline.AnimationPlayableAsset.LoopMode m_Loop;  // 0x44
        private System.Boolean m_IgniteOnce;  // 0x48
        private System.Boolean m_OnlyDeltaTime;  // 0x49
        private System.Boolean m_DynamicLink;  // 0x4a
        private UnityEngine.Timeline.AnimationPlayableAsset.ClothResetOption m_ClothResetOption;  // 0x4c
        private UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode m_AnimationEventTriggerMode;  // 0x50
        private System.Single m_AnimationEventTriggerWeightThreshold;  // 0x54
        private UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode m_CustomFootStepTriggerMode;  // 0x58
        private System.Single m_CustomFootStepTriggerWeightThreshold;  // 0x5c
        private UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviour m_ClipEndAudioBehaviour;  // 0x60
        private System.Int32 m_ClipEndAudioFadeOutTimeMs;  // 0x64
        private System.Boolean m_MixWitherAnotherClip;  // 0x68
        private System.Single m_MainClipMixWeight;  // 0x6c
        private UnityEngine.AnimationClip m_AnotherClip;  // 0x70
        private UnityEngine.AnimationClip m_blendShapeControlClip;  // 0x78
        private System.Boolean m_UseTimeRemapCurve;  // 0x80
        private UnityEngine.AnimationCurve m_TimeRemapCurve;  // 0x88
        private System.Boolean <useAnotherClipTimeAdapter>k__BackingField;  // 0x90
        public System.Boolean DisableBlendShapeControl;  // 0x91
        private UnityEngine.Timeline.AppliedOffsetMode <appliedOffsetMode>k__BackingField;  // 0x94
        private UnityEngine.Timeline.ITimelineAnimationEventContextBinding <TimelineAnimationEventContextBinding>k__BackingField;  // 0x98
        private static readonly System.Int32 k_LatestVersion;  // static @ 0x0
        private System.Int32 m_Version;  // 0xa0
        private UnityEngine.Quaternion m_Rotation;  // 0xa4

        // Properties
        System.Boolean useAnotherClipTimeAdapter { get; /* RVA: 0x02EC6590 */ set; /* RVA: 0x02EC65A0 */ }
        UnityEngine.Vector3 position { get; /* RVA: 0x04D90830 */ set; /* RVA: 0x04D90820 */ }
        UnityEngine.Quaternion rotation { get; /* RVA: 0x0B33BE88 */ set; /* RVA: 0x0B33BF94 */ }
        UnityEngine.Vector3 eulerAngles { get; /* RVA: 0x04D90540 */ set; /* RVA: 0x04D90510 */ }
        System.Boolean useTrackMatchFields { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        UnityEngine.Timeline.MatchTargetFields matchTargetFields { get; /* RVA: 0x04D86640 */ set; /* RVA: 0x04D86670 */ }
        System.Boolean removeStartOffset { get; /* RVA: 0x02D480A0 */ set; /* RVA: 0x02D480B0 */ }
        System.Boolean applyFootIK { get; /* RVA: 0x04D86F40 */ set; /* RVA: 0x04D86F60 */ }
        UnityEngine.Timeline.AnimationPlayableAsset.LoopMode loop { get; /* RVA: 0x04D86530 */ set; /* RVA: 0x04D86580 */ }
        System.Boolean IgniteOnce { get; /* RVA: 0x04D867D0 */ set; /* RVA: 0x04D86810 */ }
        System.Boolean OnlyDeltaTime { get; /* RVA: 0x04D867E0 */ set; /* RVA: 0x04D86820 */ }
        System.Boolean DynamicLink { get; /* RVA: 0x04D86F10 */ set; /* RVA: 0x04D86F20 */ }
        System.Boolean MixWithAnotherClip { get; /* RVA: 0x04D86B40 */ set; /* RVA: 0x04D86BA0 */ }
        UnityEngine.AnimationClip AnotherClip { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }
        UnityEngine.AnimationClip BlendShapeControlClip { get; /* RVA: 0x04D862A0 */ set; /* RVA: 0x06402320 */ }
        System.Boolean useTimeRemapCurve { get; /* RVA: 0x04D8D1F0 */ set; /* RVA: 0x04D8D200 */ }
        UnityEngine.AnimationCurve timeRemapCurve { get; /* RVA: 0x04D861F0 */ set; /* RVA: 0x05395D30 */ }
        System.Single MainClipMixWeight { get; /* RVA: 0x04DA3230 */ set; /* RVA: 0x04DA3330 */ }
        System.Boolean hasRootTransforms { get; /* RVA: 0x03FA5BC0 */ }
        UnityEngine.Timeline.AppliedOffsetMode appliedOffsetMode { get; /* RVA: 0x04D86B20 */ set; /* RVA: 0x04D86B80 */ }
        UnityEngine.Timeline.ITimelineAnimationEventContextBinding TimelineAnimationEventContextBinding { get; /* RVA: 0x04D85EE0 */ set; /* RVA: 0x06352D0C */ }
        UnityEngine.AnimationClip clip { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x0B33BEC4 */ }
        System.Double duration { get; /* RVA: 0x0B33BDB0 */ }
        UnityEngine.Timeline.AnimationPlayableAsset.ClothResetOption clothResetOption { get; /* RVA: 0x04D86600 */ set; /* RVA: 0x04D86630 */ }
        UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode animationEventTriggerMode { get; /* RVA: 0x04D86550 */ set; /* RVA: 0x04D865A0 */ }
        System.Single animationEventTriggerWeightThreshold { get; /* RVA: 0x04D86E00 */ set; /* RVA: 0x04D86E10 */ }
        UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode customFootStepTriggerMode { get; /* RVA: 0x04D86570 */ set; /* RVA: 0x04D865D0 */ }
        System.Single customFootStepTriggerWeightThreshold { get; /* RVA: 0x04D91500 */ set; /* RVA: 0x04D91510 */ }
        UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviour clipEndAudioBehaviour { get; /* RVA: 0x04D86790 */ set; /* RVA: 0x04D86D50 */ }
        System.Int32 clipEndAudioFadeOutTimeMs { get; /* RVA: 0x04D88110 */ set; /* RVA: 0x04D88120 */ }
        System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding> outputs { get; /* RVA: 0x0B33BE18 */ }
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x039F6DB0 */ }

        // Methods
        // RVA: 0x0B33B8C4  token: 0x6000086
        public System.Single EvaluateTimeRemap(System.Single normalizedTime) { }
        // RVA: 0x03FC5360  token: 0x60000A0
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go) { }
        // RVA: 0x0B33B5AC  token: 0x60000A1
        private static UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.AnimationClip clip, UnityEngine.Vector3 positionOffset, UnityEngine.Vector3 eulerOffset, System.Boolean removeStartOffset, UnityEngine.Timeline.AppliedOffsetMode mode, System.Boolean applyFootIK, UnityEngine.Timeline.AnimationPlayableAsset.LoopMode loop) { }
        // RVA: 0x0315BC80  token: 0x60000A2
        private static UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Timeline.AnimationPlayableAsset playableAsset) { }
        // RVA: 0x04CDB250  token: 0x60000A3
        private static UnityEngine.Animations.AnimationClipPlayable CreateAnimationClipPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.AnimationClip clip, System.Boolean removeStartOffset, System.Boolean applyFootIK, UnityEngine.Timeline.AnimationPlayableAsset.LoopMode loop) { }
        // RVA: 0x03FA56B0  token: 0x60000A4
        private static System.Boolean ShouldApplyOffset(UnityEngine.Timeline.AppliedOffsetMode mode, UnityEngine.AnimationClip clip) { }
        // RVA: 0x04D2D300  token: 0x60000A5
        private static System.Boolean ShouldApplyEventControl(UnityEngine.Timeline.AnimationPlayableAsset playableAsset) { }
        // RVA: 0x0B33BB38  token: 0x60000A6
        private static UnityEngine.Playables.Playable TryApplyAnimationEventControl(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable root, UnityEngine.Timeline.AnimationPlayableAsset playableAsset) { }
        // RVA: 0x04D7F830  token: 0x60000A7
        private static System.Boolean ShouldApplyScaleRemove(UnityEngine.Timeline.AppliedOffsetMode mode) { }
        // RVA: 0x0B33BB14  token: 0x60000A9
        public System.Void ResetOffsets() { }
        // RVA: 0x0B33B8FC  token: 0x60000AA
        public virtual System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver) { }
        // RVA: 0x03FA5C80  token: 0x60000AB
        private static System.Boolean HasRootTransforms(UnityEngine.AnimationClip clip) { }
        // RVA: 0x0B33B58C  token: 0x60000AC
        public UnityEngine.AnimationEvent[] CollectAnimationEvents() { }
        // RVA: 0x041E1670  token: 0x60000AD
        public virtual System.Void OnClipEnable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B33B94C  token: 0x60000AE
        public virtual System.Void OnClipDisable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B33BD54  token: 0x60000AF
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize() { }
        // RVA: 0x04A05920  token: 0x60000B0
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize() { }
        // RVA: 0x0B33BB04  token: 0x60000B1
        private System.Void OnUpgradeFromVersion(System.Int32 oldVersion) { }
        // RVA: 0x039F4BC0  token: 0x60000B2
        public System.Void .ctor() { }
        // RVA: 0x04D7AE50  token: 0x60000B3
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000029  // size: 0x30
    public class AnotherClipTimeAdapterBehaviour : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        private UnityEngine.Playables.Playable m_AnotherClipPlayable;  // 0x10
        private System.Boolean m_Initialized;  // 0x20
        private System.Double m_Time;  // 0x28

        // Methods
        // RVA: 0x02EC65B0  token: 0x60000BD
        public System.Void Initialize(UnityEngine.Playables.Playable anotherClipPlayable) { }
        // RVA: 0x02EC65D0  token: 0x60000BE
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x041E1670  token: 0x60000BF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002A  // size: 0x10
    public class AnimationPreviewUpdateCallback
    {
    }

    // TypeToken: 0x200002B  // size: 0x14
    public sealed struct MatchTargetFields
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.MatchTargetFields PositionX;  // const
        public static UnityEngine.Timeline.MatchTargetFields PositionY;  // const
        public static UnityEngine.Timeline.MatchTargetFields PositionZ;  // const
        public static UnityEngine.Timeline.MatchTargetFields RotationX;  // const
        public static UnityEngine.Timeline.MatchTargetFields RotationY;  // const
        public static UnityEngine.Timeline.MatchTargetFields RotationZ;  // const

    }

    // TypeToken: 0x200002C  // size: 0x14
    public sealed struct TrackOffset
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.TrackOffset ApplyTransformOffsets;  // const
        public static UnityEngine.Timeline.TrackOffset ApplySceneOffsets;  // const
        public static UnityEngine.Timeline.TrackOffset Auto;  // const

    }

    // TypeToken: 0x200002D  // size: 0x14
    public sealed struct AppliedOffsetMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.AppliedOffsetMode NoRootTransform;  // const
        public static UnityEngine.Timeline.AppliedOffsetMode TransformOffset;  // const
        public static UnityEngine.Timeline.AppliedOffsetMode SceneOffset;  // const
        public static UnityEngine.Timeline.AppliedOffsetMode TransformOffsetLegacy;  // const
        public static UnityEngine.Timeline.AppliedOffsetMode SceneOffsetLegacy;  // const
        public static UnityEngine.Timeline.AppliedOffsetMode SceneOffsetEditor;  // const
        public static UnityEngine.Timeline.AppliedOffsetMode SceneOffsetLegacyEditor;  // const

    }

    // TypeToken: 0x200002E  // size: 0x10
    public static class MatchTargetFieldConstants
    {
        // Fields
        public static UnityEngine.Timeline.MatchTargetFields All;  // static @ 0x0
        public static UnityEngine.Timeline.MatchTargetFields None;  // static @ 0x4
        public static UnityEngine.Timeline.MatchTargetFields Position;  // static @ 0x8
        public static UnityEngine.Timeline.MatchTargetFields Rotation;  // static @ 0xc

        // Methods
        // RVA: 0x04D35AB0  token: 0x60000C0
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002F  // size: 0x188
    public class AnimationTrack : UnityEngine.Timeline.TrackAsset, UnityEngine.Timeline.ILayerable
    {
        // Fields
        private static System.String k_DefaultInfiniteClipName;  // const
        private static System.String k_DefaultRecordableClipName;  // const
        private UnityEngine.Timeline.TimelineClip.ClipExtrapolation m_InfiniteClipPreExtrapolation;  // 0xc0
        private UnityEngine.Timeline.TimelineClip.ClipExtrapolation m_InfiniteClipPostExtrapolation;  // 0xc4
        private UnityEngine.Vector3 m_InfiniteClipOffsetPosition;  // 0xc8
        private UnityEngine.Vector3 m_InfiniteClipOffsetEulerAngles;  // 0xd4
        private System.Double m_InfiniteClipTimeOffset;  // 0xe0
        private System.Boolean m_InfiniteClipRemoveOffset;  // 0xe8
        private System.Boolean m_InfiniteClipApplyFootIK;  // 0xe9
        private UnityEngine.Timeline.AnimationPlayableAsset.LoopMode mInfiniteClipLoop;  // 0xec
        private UnityEngine.Timeline.MatchTargetFields m_MatchTargetFields;  // 0xf0
        private UnityEngine.Vector3 m_Position;  // 0xf4
        private UnityEngine.Vector3 m_EulerAngles;  // 0x100
        private UnityEngine.AvatarMask m_AvatarMask;  // 0x110
        private System.Boolean m_ApplyAvatarMask;  // 0x118
        private System.Boolean m_DisableFloatCurveOnMask;  // 0x119
        private UnityEngine.Timeline.TrackOffset m_TrackOffset;  // 0x11c
        private UnityEngine.AnimationClip m_InfiniteClip;  // 0x120
        private System.Boolean m_IsAdditive;  // 0x128
        private System.Boolean m_IsAdditiveToLast;  // 0x129
        public System.Boolean muteScriptAnimAddon;  // 0x12a
        private System.Boolean m_UseScriptAnimAddon;  // 0x12b
        private System.Boolean m_useTPoseAsDefaultPose;  // 0x12c
        private UnityEngine.AnimationClip m_TPoseClip;  // 0x130
        private UnityEngine.ScriptableObject m_TimelineNPCDescriptor;  // 0x138
        private System.String m_LookAtTargetPath;  // 0x140
        private System.Boolean m_LookAtUseAdditive;  // 0x148
        private System.Boolean m_UseLookAtV2;  // 0x149
        private System.Boolean m_WriteTransformDefAsPostProcess;  // 0x14a
        private UnityEngine.Transform m_LookAtTarget;  // 0x150
        private System.Collections.Generic.HashSet<UnityEngine.Timeline.RuntimeElement> m_allRuntimeClipUnderTrack;  // 0x158
        private static readonly System.Collections.Generic.Queue<UnityEngine.Transform> s_CachedQueue;  // static @ 0x0
        private UnityEngine.Quaternion m_OpenClipOffsetRotation;  // 0x160
        private UnityEngine.Quaternion m_Rotation;  // 0x170
        private System.Boolean m_ApplyOffsets;  // 0x180

        // Properties
        UnityEngine.Vector3 position { get; /* RVA: 0x04DBA4A0 */ set; /* RVA: 0x04DBA550 */ }
        UnityEngine.Quaternion rotation { get; /* RVA: 0x0B33D7C4 */ set; /* RVA: 0x0B33D8B0 */ }
        UnityEngine.Vector3 eulerAngles { get; /* RVA: 0x04DBA430 */ set; /* RVA: 0x04DBA4C0 */ }
        System.Boolean applyOffsets { get; /* RVA: 0x012081B0 */ set; /* RVA: 0x041E1670 */ }
        UnityEngine.Timeline.TrackOffset trackOffset { get; /* RVA: 0x04DB9B70 */ set; /* RVA: 0x04DB9C20 */ }
        UnityEngine.Timeline.MatchTargetFields matchTargetFields { get; /* RVA: 0x04D885B0 */ set; /* RVA: 0x0B33D834 */ }
        UnityEngine.AnimationClip infiniteClip { get; /* RVA: 0x04D8DC70 */ set; /* RVA: 0x053971C0 */ }
        System.Boolean infiniteClipRemoveOffset { get; /* RVA: 0x04D8C8C0 */ set; /* RVA: 0x04D8C8F0 */ }
        UnityEngine.AvatarMask avatarMask { get; /* RVA: 0x04D8D1B0 */ set; /* RVA: 0x05397170 */ }
        System.Boolean applyAvatarMask { get; /* RVA: 0x04D92E30 */ set; /* RVA: 0x04D92E40 */ }
        System.Boolean disableFloatCurveOnMask { get; /* RVA: 0x04D8E510 */ set; /* RVA: 0x04D8E540 */ }
        System.Boolean isUsingScriptAnimAddon { get; /* RVA: 0x04DBA490 */ set; /* RVA: 0x04DBA540 */ }
        System.Boolean lookAtUseAdditive { get; /* RVA: 0x04D8FE60 */ set; /* RVA: 0x04D8FE90 */ }
        System.Boolean useLookAtV2 { get; /* RVA: 0x04D8FE50 */ set; /* RVA: 0x04D8FE80 */ }
        System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding> outputs { get; /* RVA: 0x04B87C60 */ }
        System.Boolean inClipMode { get; /* RVA: 0x039F4E60 */ }
        UnityEngine.Vector3 infiniteClipOffsetPosition { get; /* RVA: 0x04DBA470 */ set; /* RVA: 0x04DBA510 */ }
        UnityEngine.Quaternion infiniteClipOffsetRotation { get; /* RVA: 0x0B33D75C */ set; /* RVA: 0x0B33D804 */ }
        UnityEngine.Vector3 infiniteClipOffsetEulerAngles { get; /* RVA: 0x04DBA450 */ set; /* RVA: 0x04DBA4F0 */ }
        System.Boolean infiniteClipApplyFootIK { get; /* RVA: 0x04DAA0F0 */ set; /* RVA: 0x04DBA4E0 */ }
        System.Double infiniteClipTimeOffset { get; /* RVA: 0x04DA03A0 */ set; /* RVA: 0x04DBA530 */ }
        UnityEngine.Timeline.TimelineClip.ClipExtrapolation infiniteClipPreExtrapolation { get; /* RVA: 0x04DA4B70 */ set; /* RVA: 0x04DA5DC0 */ }
        UnityEngine.Timeline.TimelineClip.ClipExtrapolation infiniteClipPostExtrapolation { get; /* RVA: 0x04D9DF00 */ set; /* RVA: 0x04D9DFC0 */ }
        UnityEngine.Timeline.AnimationPlayableAsset.LoopMode infiniteClipLoop { get; /* RVA: 0x04D9DF40 */ set; /* RVA: 0x04D9E020 */ }
        UnityEngine.Vector3 openClipOffsetPosition { get; /* RVA: 0x04DBA470 */ set; /* RVA: 0x04DBA510 */ }
        UnityEngine.Quaternion openClipOffsetRotation { get; /* RVA: 0x0B33D79C */ set; /* RVA: 0x0B33D890 */ }
        UnityEngine.Vector3 openClipOffsetEulerAngles { get; /* RVA: 0x04DBA450 */ set; /* RVA: 0x04DBA4F0 */ }
        UnityEngine.Timeline.TimelineClip.ClipExtrapolation openClipPreExtrapolation { get; /* RVA: 0x04DA4B70 */ set; /* RVA: 0x04DA5DC0 */ }
        UnityEngine.Timeline.TimelineClip.ClipExtrapolation openClipPostExtrapolation { get; /* RVA: 0x04D9DF00 */ set; /* RVA: 0x04D9DFC0 */ }

        // Methods
        // RVA: 0x0B33D584  token: 0x60000DD
        public System.Boolean IsControlByThisTrack(UnityEngine.Timeline.RuntimeElement element) { }
        // RVA: 0x04B30E40  token: 0x60000DE
        private virtual System.Boolean CanCompileClips() { }
        // RVA: 0x0B33D658  token: 0x60000F1
        private System.Void ResetOffsets() { }
        // RVA: 0x0B33CA00  token: 0x60000F2
        public UnityEngine.Timeline.TimelineClip CreateClip(UnityEngine.AnimationClip clip) { }
        // RVA: 0x0B33CA94  token: 0x60000F3
        public System.Void CreateInfiniteClip(System.String infiniteClipName) { }
        // RVA: 0x0B33CEE8  token: 0x60000F4
        public UnityEngine.Timeline.TimelineClip CreateRecordableClip(System.String animClipName) { }
        // RVA: 0x0B33D5E0  token: 0x60000F5
        protected virtual System.Void OnCreateClip(UnityEngine.Timeline.TimelineClip clip) { }
        // RVA: 0x04970AF0  token: 0x60000F6
        protected internal virtual System.Int32 CalculateItemsHash() { }
        // RVA: 0x041E1670  token: 0x60000F7
        private System.Void UpdateClipOffsets() { }
        // RVA: 0x03FC53E0  token: 0x60000F8
        private UnityEngine.Playables.Playable _CreatePlayableFromClip(UnityEngine.Timeline.TimelineClip c, UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, UnityEngine.Timeline.AppliedOffsetMode mode, UnityEngine.Timeline.ITimelineAnimationEventContextBinding timelineAnimationEventContextBinding) { }
        // RVA: 0x03FA6280  token: 0x60000F9
        private UnityEngine.Playables.Playable CompileTrackPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Timeline.AnimationTrack track, UnityEngine.GameObject go, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree, UnityEngine.Timeline.AppliedOffsetMode mode, UnityEngine.Timeline.ITimelineAnimationEventContextBinding timelineAnimationEventContextBinding) { }
        // RVA: 0x0B33D688  token: 0x60000FA
        private System.Boolean ShouldLinkWithMainTrackMixer(UnityEngine.Timeline.AnimationTrack animationTrack) { }
        // RVA: 0x0B33C0CC  token: 0x60000FB
        private UnityEngine.Playables.Playable ApplyScriptAnimationPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Animations.AnimationLayerMixerPlayable mixer, UnityEngine.GameObject go) { }
        // RVA: 0x0B33D70C  token: 0x60000FC
        private virtual UnityEngine.Playables.Playable UnityEngine.Timeline.ILayerable.CreateLayerMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount) { }
        // RVA: 0x03FA7330  token: 0x60000FD
        private virtual UnityEngine.Playables.Playable CreateMixerPlayableGraph(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree) { }
        // RVA: 0x041E1670  token: 0x60000FE
        private System.Void AttachCustomBlend(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Animations.AnimationLayerMixerPlayable layerMixer) { }
        // RVA: 0x04A1F7B0  token: 0x60000FF
        private System.Void AttachLastAdditiveClip(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Animations.AnimationLayerMixerPlayable layerMixer, UnityEngine.GameObject go, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree, System.Collections.Generic.List<UnityEngine.Timeline.AnimationTrack> flattenTracks, UnityEngine.Timeline.ITimelineAnimationEventContextBinding timelineAnimationEventContextBinding) { }
        // RVA: 0x04D85B80  token: 0x6000100
        private System.Int32 GetDefaultBlendCount() { }
        // RVA: 0x041E1670  token: 0x6000101
        private System.Void AttachDefaultBlend(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Animations.AnimationLayerMixerPlayable mixer, System.Boolean requireOffset, UnityEngine.Animator animator) { }
        // RVA: 0x0B33C8C8  token: 0x6000102
        private UnityEngine.Playables.Playable AttachOffsetPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable playable, UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x03FA91A0  token: 0x6000103
        private System.Boolean RequiresMotionXPlayable(UnityEngine.Timeline.AppliedOffsetMode mode, UnityEngine.GameObject gameObject) { }
        // RVA: 0x03FA6270  token: 0x6000104
        private static System.Boolean UsesAbsoluteMotion(UnityEngine.Timeline.AppliedOffsetMode mode) { }
        // RVA: 0x0B33D4A4  token: 0x6000105
        private System.Boolean HasController(UnityEngine.GameObject gameObject) { }
        // RVA: 0x03FA6B90  token: 0x6000106
        private UnityEngine.Animator GetBinding(UnityEngine.Playables.PlayableDirector director) { }
        // RVA: 0x03FA7170  token: 0x6000107
        private static UnityEngine.Animations.AnimationLayerMixerPlayable CreateGroupMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount) { }
        // RVA: 0x0B33CB80  token: 0x6000108
        private UnityEngine.Playables.Playable CreateInfiniteTrackPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree, UnityEngine.Timeline.AppliedOffsetMode mode) { }
        // RVA: 0x0315B9B0  token: 0x6000109
        private UnityEngine.Playables.Playable ApplyTrackOffset(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable root, UnityEngine.GameObject go, UnityEngine.Timeline.AppliedOffsetMode mode) { }
        // RVA: 0x03FA5F20  token: 0x600010A
        private System.Void _AddClipIntoTree(UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree, UnityEngine.Timeline.RuntimeClip clip) { }
        // RVA: 0x04D38A60  token: 0x600010B
        private virtual System.Void GetEvaluationTime(System.Double& outStart, System.Double& outDuration) { }
        // RVA: 0x04864F30  token: 0x600010C
        private virtual System.Void GetSequenceTime(System.Double& outStart, System.Double& outDuration) { }
        // RVA: 0x0B33C6E4  token: 0x600010D
        private System.Void AssignAnimationClip(UnityEngine.Timeline.TimelineClip clip, UnityEngine.AnimationClip animClip) { }
        // RVA: 0x041E1670  token: 0x600010E
        public virtual System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver) { }
        // RVA: 0x0B33D1B4  token: 0x600010F
        private System.Void GetAnimationClips(System.Collections.Generic.List<UnityEngine.AnimationClip> animClips) { }
        // RVA: 0x03FA9170  token: 0x6000110
        private UnityEngine.Timeline.AppliedOffsetMode GetOffsetMode(UnityEngine.GameObject go, System.Boolean animatesRootTransform) { }
        // RVA: 0x03FA9210  token: 0x6000111
        private System.Boolean IsRootTransformDisabledByMask(UnityEngine.GameObject gameObject, UnityEngine.Transform genericRootNode) { }
        // RVA: 0x03FA6E00  token: 0x6000112
        private UnityEngine.Transform GetGenericRootNode(UnityEngine.GameObject gameObject) { }
        // RVA: 0x03FA5720  token: 0x6000113
        private System.Boolean AnimatesRootTransform() { }
        // RVA: 0x0B33CFF4  token: 0x6000114
        private static UnityEngine.Transform FindInHierarchyBreadthFirst(UnityEngine.Transform t, System.String name) { }
        // RVA: 0x0B33D618  token: 0x600011F
        private virtual System.Void OnUpgradeFromVersion(System.Int32 oldVersion) { }
        // RVA: 0x039F4CC0  token: 0x6000120
        public System.Void .ctor() { }
        // RVA: 0x04D11C90  token: 0x6000121
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000032
    public interface ICurvesOwner
    {
        // Properties
        System.String defaultCurvesName { get; /* RVA: -1  // abstract */ }
        UnityEngine.Object asset { get; /* RVA: -1  // abstract */ }
        UnityEngine.Object assetOwner { get; /* RVA: -1  // abstract */ }
        UnityEngine.Timeline.TrackAsset targetTrack { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000033  // size: 0xB8
    public class TimelineClip : UnityEngine.Timeline.ICurvesOwner, UnityEngine.ISerializationCallbackReceiver
    {
        // Fields
        private static System.Int32 k_LatestVersion;  // const
        private System.Int32 m_Version;  // 0x10
        public static readonly UnityEngine.Timeline.ClipCaps kDefaultClipCaps;  // static @ 0x0
        public static readonly System.Single kDefaultClipDurationInSeconds;  // static @ 0x4
        public static readonly System.Double kTimeScaleMin;  // static @ 0x8
        public static readonly System.Double kTimeScaleMax;  // static @ 0x10
        private static readonly System.String kDefaultCurvesName;  // static @ 0x18
        private static readonly System.Double kMinDuration;  // static @ 0x20
        private static readonly System.Double kMaxTimeValue;  // static @ 0x28
        public System.Int32 optionIndex;  // 0x14
        private System.Double m_Start;  // 0x18
        private System.Double m_ClipIn;  // 0x20
        private UnityEngine.Object m_Asset;  // 0x28
        private System.Double m_Duration;  // 0x30
        private System.Double m_TimeScale;  // 0x38
        private UnityEngine.Timeline.TrackAsset m_ParentTrack;  // 0x40
        private System.Double m_EaseInDuration;  // 0x48
        private System.Double m_EaseOutDuration;  // 0x50
        private System.Double m_BlendInDuration;  // 0x58
        private System.Double m_BlendOutDuration;  // 0x60
        private UnityEngine.AnimationCurve m_MixInCurve;  // 0x68
        private UnityEngine.AnimationCurve m_MixOutCurve;  // 0x70
        private UnityEngine.Timeline.TimelineClip.BlendCurveMode m_BlendInCurveMode;  // 0x78
        private UnityEngine.Timeline.TimelineClip.BlendCurveMode m_BlendOutCurveMode;  // 0x7c
        private System.Collections.Generic.List<System.String> m_ExposedParameterNames;  // 0x80
        private UnityEngine.AnimationClip m_AnimationCurves;  // 0x88
        private System.Boolean m_Recordable;  // 0x90
        private UnityEngine.Timeline.TimelineClip.ClipExtrapolation m_PostExtrapolationMode;  // 0x94
        private UnityEngine.Timeline.TimelineClip.ClipExtrapolation m_PreExtrapolationMode;  // 0x98
        private System.Double m_PostExtrapolationTime;  // 0xa0
        private System.Double m_PreExtrapolationTime;  // 0xa8
        private System.String m_DisplayName;  // 0xb0

        // Properties
        System.Double timeScale { get; /* RVA: 0x039F6A20 */ }
        System.Double start { get; /* RVA: 0x04D86360 */ set; /* RVA: 0x0B342000 */ }
        System.Double duration { get; /* RVA: 0x04D86A10 */ set; /* RVA: 0x0B341D2C */ }
        System.Double end { get; /* RVA: 0x04DBA5B0 */ }
        System.Double clipIn { get; /* RVA: 0x039F6B10 */ }
        System.String displayName { get; /* RVA: 0x04D86250 */ set; /* RVA: 0x04D84CE0 */ }
        System.Double clipAssetDuration { get; /* RVA: 0x0B341BD0 */ }
        UnityEngine.AnimationClip curves { get; /* RVA: 0x04D861F0 */ }
        System.String UnityEngine.Timeline.ICurvesOwner.defaultCurvesName { get; /* RVA: 0x0B341B00 */ }
        UnityEngine.Object asset { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        UnityEngine.Object UnityEngine.Timeline.ICurvesOwner.assetOwner { get; /* RVA: 0x04D85A60 */ }
        UnityEngine.Timeline.TrackAsset UnityEngine.Timeline.ICurvesOwner.targetTrack { get; /* RVA: 0x04D85A60 */ }
        System.Double easeInDuration { get; /* RVA: 0x047A2B30 */ set; /* RVA: 0x0B341DC0 */ }
        System.Double easeOutDuration { get; /* RVA: 0x047A2C40 */ set; /* RVA: 0x0B341EA4 */ }
        System.Double blendInDuration { get; /* RVA: 0x0B341B88 */ }
        System.Double blendOutDuration { get; /* RVA: 0x0B341BAC */ }
        System.Boolean hasBlendIn { get; /* RVA: 0x047A2BE0 */ }
        System.Boolean hasBlendOut { get; /* RVA: 0x047A2CF0 */ }
        UnityEngine.AnimationCurve mixInCurve { get; /* RVA: 0x0B341C28 */ set; /* RVA: 0x0485F170 */ }
        System.Double mixInDuration { get; /* RVA: 0x047A2B00 */ }
        UnityEngine.AnimationCurve mixOutCurve { get; /* RVA: 0x0B341C90 */ set; /* RVA: 0x063AD760 */ }
        System.Double mixOutTime { get; /* RVA: 0x0B341CF8 */ }
        System.Double mixOutDuration { get; /* RVA: 0x047A2C10 */ }
        System.Boolean recordable { get; /* RVA: 0x02EC6590 */ set; /* RVA: 0x02EC65A0 */ }
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x039F6B40 */ }
        UnityEngine.Timeline.TimelineClip.ClipExtrapolation postExtrapolationMode { get; /* RVA: 0x039F6630 */ set; /* RVA: 0x0B341F88 */ }
        UnityEngine.Timeline.TimelineClip.ClipExtrapolation preExtrapolationMode { get; /* RVA: 0x039F66A0 */ set; /* RVA: 0x0B341FC4 */ }
        System.Double extrapolatedStart { get; /* RVA: 0x04891CD0 */ }
        System.Double extrapolatedDuration { get; /* RVA: 0x039F7450 */ }

        // Methods
        // RVA: 0x0B341B50  token: 0x6000131
        private System.Void UpgradeToLatestVersion() { }
        // RVA: 0x0B341B60  token: 0x6000132
        private System.Void .ctor(UnityEngine.Timeline.TrackAsset parent) { }
        // RVA: 0x04D85A60  token: 0x6000143
        public UnityEngine.Timeline.TrackAsset GetParentTrack() { }
        // RVA: 0x0B3418A4  token: 0x6000144
        private System.Void SetParentTrack_Internal(UnityEngine.Timeline.TrackAsset newParentTrack) { }
        // RVA: 0x03AC40C0  token: 0x6000157
        private System.Int32 Hash() { }
        // RVA: 0x039F75E0  token: 0x6000158
        public System.Single EvaluateMixOut(System.Double time) { }
        // RVA: 0x0B341558  token: 0x6000159
        public System.Single EvaluateMixOutAtGivenTime(System.Double time, System.Double mixOutTime, System.Double mixOutDuration) { }
        // RVA: 0x039F76E0  token: 0x600015A
        public System.Single EvaluateMixIn(System.Double time) { }
        // RVA: 0x0B341494  token: 0x600015B
        public System.Single EvaluateMixInAtGivenTime(System.Double time, System.Double runtimeMixInTime, System.Double mixInDuration) { }
        // RVA: 0x0B34161C  token: 0x600015C
        private static UnityEngine.AnimationCurve GetDefaultMixInCurve() { }
        // RVA: 0x0B341644  token: 0x600015D
        private static UnityEngine.AnimationCurve GetDefaultMixOutCurve() { }
        // RVA: 0x039F5D40  token: 0x600015E
        public System.Double ToLocalTime(System.Double time) { }
        // RVA: 0x0B3417AC  token: 0x600015F
        private static System.Double SanitizeTimeValue(System.Double value, System.Double defaultValue) { }
        // RVA: 0x04DBA590  token: 0x6000164
        private System.Void SetPostExtrapolationTime(System.Double time) { }
        // RVA: 0x04DBA5A0  token: 0x6000165
        private System.Void SetPreExtrapolationTime(System.Double time) { }
        // RVA: 0x039F6660  token: 0x6000166
        public System.Boolean IsPreExtrapolatedTime(System.Double sequenceTime) { }
        // RVA: 0x039F65F0  token: 0x6000167
        public System.Boolean IsPostExtrapolatedTime(System.Double sequenceTime) { }
        // RVA: 0x0B34166C  token: 0x600016A
        private static System.Double GetExtrapolatedTime(System.Double time, UnityEngine.Timeline.TimelineClip.ClipExtrapolation mode, System.Double duration) { }
        // RVA: 0x04D89110  token: 0x600016B
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize() { }
        // RVA: 0x04CC60D0  token: 0x600016C
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize() { }
        // RVA: 0x0B341984  token: 0x600016D
        public virtual System.String ToString() { }
        // RVA: 0x041E1670  token: 0x600016E
        private System.Void UpdateDirty(System.Double oldValue, System.Double newValue) { }
        // RVA: 0x04CC6370  token: 0x600016F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000038  // size: 0x68
    public class TimelineAsset : UnityEngine.Playables.PlayableAsset, UnityEngine.ISerializationCallbackReceiver, UnityEngine.Timeline.ITimelineClipAsset, UnityEngine.Timeline.IPropertyPreview
    {
        // Fields
        private static System.Int32 k_LatestVersion;  // const
        private System.Int32 m_Version;  // 0x18
        private System.Collections.Generic.List<UnityEngine.ScriptableObject> m_Tracks;  // 0x20
        private System.Double m_FixedDuration;  // 0x28
        private UnityEngine.Timeline.TrackAsset[] m_CacheOutputTracks;  // 0x30
        private UnityEngine.Timeline.GroupTrack[] m_CacheGroupTracks;  // 0x38
        private System.Collections.Generic.List<UnityEngine.Timeline.TrackAsset> m_CacheRootTracks;  // 0x40
        private UnityEngine.Timeline.TrackAsset[] m_CacheFlattenedTracks;  // 0x48
        private UnityEngine.Timeline.TimelineAsset.EditorSettings m_EditorSettings;  // 0x50
        private UnityEngine.Timeline.TimelineAsset.DurationMode m_DurationMode;  // 0x58
        private UnityEngine.Timeline.MarkerTrack m_MarkerTrack;  // 0x60

        // Properties
        UnityEngine.Timeline.TimelineAsset.EditorSettings editorSettings { get; /* RVA: 0x04D86270 */ }
        System.Double duration { get; /* RVA: 0x039F2D40 */ }
        System.Double fixedDuration { get; /* RVA: 0x0B341310 */ set; /* RVA: 0x0B341400 */ }
        UnityEngine.Timeline.TimelineAsset.DurationMode durationMode { get; /* RVA: 0x04D86570 */ set; /* RVA: 0x04D865D0 */ }
        System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding> outputs { get; /* RVA: 0x048FD6E0 */ }
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x0B34105C */ }
        System.Int32 outputTrackCount { get; /* RVA: 0x0B341390 */ }
        System.Int32 rootTrackCount { get; /* RVA: 0x0B3413B8 */ }
        UnityEngine.Timeline.TrackAsset[] flattenedTracks { get; /* RVA: 0x039F35B0 */ }
        UnityEngine.Timeline.MarkerTrack markerTrack { get; /* RVA: 0x04D86280 */ }
        System.Collections.Generic.List<UnityEngine.ScriptableObject> trackObjects { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000171
        private System.Void UpgradeToLatestVersion() { }
        // RVA: 0x0B340F0C  token: 0x600017C
        private System.Void OnValidate() { }
        // RVA: 0x0B340A30  token: 0x600017D
        public UnityEngine.Timeline.TrackAsset GetRootTrack(System.Int32 index) { }
        // RVA: 0x0B340A8C  token: 0x600017E
        public System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> GetRootTracks() { }
        // RVA: 0x0B3409EC  token: 0x600017F
        public UnityEngine.Timeline.TrackAsset GetOutputTrack(System.Int32 index) { }
        // RVA: 0x053947BC  token: 0x6000180
        public System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> GetOutputTracks() { }
        // RVA: 0x05396C14  token: 0x6000181
        public System.Collections.Generic.IEnumerable<UnityEngine.Timeline.GroupTrack> GetGroupTracks() { }
        // RVA: 0x0B340AA8  token: 0x6000182
        private static System.Double GetValidFrameRate(System.Double frameRate) { }
        // RVA: 0x039F3DE0  token: 0x6000183
        private System.Void UpdateRootTrackCache() { }
        // RVA: 0x039F2A60  token: 0x6000184
        private System.Void UpdateOutputTrackCache() { }
        // RVA: 0x0B3409E4  token: 0x6000186
        public UnityEngine.Timeline.TrackAsset[] GetAllTracks() { }
        // RVA: 0x0B33FA18  token: 0x6000189
        private System.Void AddTrackInternal(UnityEngine.Timeline.TrackAsset track) { }
        // RVA: 0x0B340F40  token: 0x600018A
        private System.Void RemoveTrack(UnityEngine.Timeline.TrackAsset track) { }
        // RVA: 0x03FC6180  token: 0x600018B
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go) { }
        // RVA: 0x04DBA580  token: 0x600018C
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize() { }
        // RVA: 0x039ED100  token: 0x600018D
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize() { }
        // RVA: 0x045A5340  token: 0x600018E
        private System.Void __internalAwake() { }
        // RVA: 0x0B340880  token: 0x600018F
        public virtual System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver) { }
        // RVA: 0x0B33FCE4  token: 0x6000190
        public System.Void CreateMarkerTrack() { }
        // RVA: 0x039ED6C0  token: 0x6000191
        private System.Void Invalidate() { }
        // RVA: 0x0B341000  token: 0x6000192
        private System.Void UpdateFixedDurationWithItemsDuration() { }
        // RVA: 0x039F2E40  token: 0x6000193
        private UnityEngine.Timeline.DiscreteTime CalculateItemsDuration() { }
        // RVA: 0x039F3730  token: 0x6000194
        private static System.Void AddSubTracksRecursive(UnityEngine.Timeline.TrackAsset track, System.Collections.Generic.List<UnityEngine.Timeline.TrackAsset>& allTracks) { }
        // RVA: 0x0B33FDB8  token: 0x6000195
        public UnityEngine.Timeline.TrackAsset CreateTrack(System.Type type, UnityEngine.Timeline.TrackAsset parent, System.String name) { }
        // RVA: -1  // generic def  token: 0x6000196
        public T CreateTrack(UnityEngine.Timeline.TrackAsset parent, System.String trackName) { }
        // RVA: -1  // generic def  token: 0x6000197
        public T CreateTrack(System.String trackName) { }
        // RVA: -1  // generic def  token: 0x6000198
        public T CreateTrack() { }
        // RVA: 0x0B3400D4  token: 0x6000199
        public System.Boolean DeleteClip(UnityEngine.Timeline.TimelineClip clip) { }
        // RVA: 0x0B3404F4  token: 0x600019A
        public System.Boolean DeleteTrack(UnityEngine.Timeline.TrackAsset track) { }
        // RVA: 0x0B340B48  token: 0x600019B
        private System.Void MoveLastTrackBefore(UnityEngine.Timeline.TrackAsset asset) { }
        // RVA: 0x0B340D04  token: 0x600019C
        public System.Void MoveTrackAfter(UnityEngine.Timeline.TrackAsset trackToMove, UnityEngine.Timeline.TrackAsset targetTrack) { }
        // RVA: 0x0B33FA88  token: 0x600019D
        private UnityEngine.Timeline.TrackAsset AllocateTrack(UnityEngine.Timeline.TrackAsset trackAssetParent, System.String trackName, System.Type trackType) { }
        // RVA: 0x0B340270  token: 0x600019E
        private System.Void DeleteRecordedAnimation(UnityEngine.Timeline.TrackAsset track) { }
        // RVA: 0x0B340374  token: 0x600019F
        private System.Void DeleteRecordedAnimation(UnityEngine.Timeline.TimelineClip clip) { }
        // RVA: 0x039F4A90  token: 0x60001A0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003F  // size: 0xC0
    public abstract class TrackAsset : UnityEngine.Playables.PlayableAsset, UnityEngine.ISerializationCallbackReceiver, UnityEngine.Timeline.IPropertyPreview, UnityEngine.Timeline.ICurvesOwner, UnityEngine.Timeline.IDynamicBinding
    {
        // Fields
        private static System.Int32 k_LatestVersion;  // const
        private System.Int32 m_Version;  // 0x18
        private UnityEngine.AnimationClip m_AnimClip;  // 0x20
        public System.Boolean useAutoBinding;  // 0x28
        public System.String autoBindingPath;  // 0x30
        public System.String uniqueId;  // 0x38
        public System.Int32 uiBindingType;  // 0x40
        private static UnityEngine.Timeline.TrackAsset.TransientBuildData s_BuildData;  // static @ 0x0
        private static System.String kDefaultCurvesName;  // const
        private static System.Action<UnityEngine.Timeline.TimelineClip,UnityEngine.GameObject,UnityEngine.Playables.Playable> OnClipPlayableCreate;  // static @ 0x18
        private static System.Action<UnityEngine.Timeline.TrackAsset,UnityEngine.GameObject,UnityEngine.Playables.Playable> OnTrackAnimationPlayableCreate;  // static @ 0x20
        private System.Boolean m_RuntimeMuted;  // 0x44
        private System.Boolean m_Locked;  // 0x45
        private System.Boolean m_Muted;  // 0x46
        private System.String m_CustomPlayableFullTypename;  // 0x48
        private UnityEngine.AnimationClip m_Curves;  // 0x50
        private UnityEngine.Playables.PlayableAsset m_Parent;  // 0x58
        private System.Collections.Generic.List<UnityEngine.ScriptableObject> m_Children;  // 0x60
        private System.Int32 m_ItemsHash;  // 0x68
        private UnityEngine.Timeline.TimelineClip[] m_ClipsCache;  // 0x70
        private UnityEngine.Timeline.DiscreteTime m_Start;  // 0x78
        private UnityEngine.Timeline.DiscreteTime m_End;  // 0x80
        private System.Boolean m_CacheSorted;  // 0x88
        private System.Nullable<System.Boolean> m_SupportsNotifications;  // 0x89
        private static UnityEngine.Timeline.TrackAsset[] s_EmptyCache;  // static @ 0x28
        private System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> m_ChildTrackCache;  // 0x90
        private static System.Collections.Generic.Dictionary<System.Type,UnityEngine.Timeline.TrackBindingTypeAttribute> s_TrackBindingTypeAttributeCache;  // static @ 0x30
        protected internal System.Collections.Generic.List<UnityEngine.Timeline.TimelineClip> m_Clips;  // 0x98
        private UnityEngine.Timeline.MarkerList m_Markers;  // 0xa0
        public System.Int32 OptionIndex;  // 0xb8
        public System.Boolean useRuntimeMuted;  // 0xbc

        // Properties
        System.Double start { get; /* RVA: 0x0B34574C */ }
        System.Double end { get; /* RVA: 0x039F2DE0 */ }
        System.Double duration { get; /* RVA: 0x0B3455C0 */ }
        System.Boolean muted { get; /* RVA: 0x039F34A0 */ set; /* RVA: 0x04D90900 */ }
        System.Boolean runtimeMuted { get; /* RVA: 0x04D86A30 */ set; /* RVA: 0x04D86A40 */ }
        System.Boolean mutedInHierarchy { get; /* RVA: 0x039F33B0 */ }
        UnityEngine.Timeline.TimelineAsset timelineAsset { get; /* RVA: 0x039ED500 */ }
        UnityEngine.Playables.PlayableAsset parent { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x036E5590 */ }
        UnityEngine.Timeline.TimelineClip[] clips { get; /* RVA: 0x039F4650 */ }
        System.Boolean isEmpty { get; /* RVA: 0x0B345624 */ }
        System.Boolean hasClips { get; /* RVA: 0x0468DAA0 */ }
        System.Boolean hasCurves { get; /* RVA: 0x03E0E860 */ }
        System.Boolean isSubTrack { get; /* RVA: 0x039F40B0 */ }
        System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding> outputs { get; /* RVA: 0x03A10EB0 */ }
        System.String customPlayableTypename { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        UnityEngine.AnimationClip curves { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        System.String UnityEngine.Timeline.ICurvesOwner.defaultCurvesName { get; /* RVA: 0x0B3451FC */ }
        UnityEngine.Object UnityEngine.Timeline.ICurvesOwner.asset { get; /* RVA: 0x0464E630 */ }
        UnityEngine.Object UnityEngine.Timeline.ICurvesOwner.assetOwner { get; /* RVA: 0x0B3451F4 */ }
        UnityEngine.Timeline.TrackAsset UnityEngine.Timeline.ICurvesOwner.targetTrack { get; /* RVA: 0x0464E630 */ }
        System.Collections.Generic.List<UnityEngine.ScriptableObject> subTracksObjects { get; /* RVA: 0x04D86280 */ }
        System.Boolean locked { get; /* RVA: 0x04D86EF0 */ set; /* RVA: 0x04D86F00 */ }
        System.Boolean lockedInHierarchy { get; /* RVA: 0x0B345660 */ }
        System.Boolean supportsNotifications { get; /* RVA: 0x039EDE90 */ }

        // Events
        event System.Action<UnityEngine.Timeline.TimelineClip,UnityEngine.GameObject,UnityEngine.Playables.Playable> OnClipPlayableCreate;
        event System.Action<UnityEngine.Timeline.TrackAsset,UnityEngine.GameObject,UnityEngine.Playables.Playable> OnTrackAnimationPlayableCreate;

        // Methods
        // RVA: 0x041E1670  token: 0x60001AF
        protected virtual System.Void OnBeforeTrackSerialize() { }
        // RVA: 0x041E1670  token: 0x60001B0
        protected virtual System.Void OnAfterTrackDeserialize() { }
        // RVA: 0x041E1670  token: 0x60001B1
        private virtual System.Void OnUpgradeFromVersion(System.Int32 oldVersion) { }
        // RVA: 0x0B3450E0  token: 0x60001B2
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize() { }
        // RVA: 0x039ED110  token: 0x60001B3
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize() { }
        // RVA: 0x041E1670  token: 0x60001B4
        private System.Void UpgradeToLatestVersion() { }
        // RVA: 0x053947D8  token: 0x60001C4
        public System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TimelineClip> GetClips() { }
        // RVA: 0x053947E0  token: 0x60001CB
        public System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> GetChildTracks() { }
        // RVA: 0x047FDE30  token: 0x60001D9
        private System.Void __internalAwake() { }
        // RVA: 0x0B343DAC  token: 0x60001DA
        public virtual System.Void CreateCurves(System.String curvesClipName) { }
        // RVA: 0x03E0EB60  token: 0x60001DB
        public virtual UnityEngine.Playables.Playable CreateTrackMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount) { }
        // RVA: 0x0B34440C  token: 0x60001DC
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go) { }
        // RVA: 0x0B343E4C  token: 0x60001DD
        public UnityEngine.Timeline.TimelineClip CreateDefaultClip() { }
        // RVA: -1  // generic def  token: 0x60001DE
        public UnityEngine.Timeline.TimelineClip CreateClip() { }
        // RVA: 0x0B34445C  token: 0x60001DF
        public System.Boolean DeleteClip(UnityEngine.Timeline.TimelineClip clip) { }
        // RVA: 0x0B3441AC  token: 0x60001E0
        public UnityEngine.Timeline.IMarker CreateMarker(System.Type type, System.Double time) { }
        // RVA: -1  // generic def  token: 0x60001E1
        public T CreateMarker(System.Double time) { }
        // RVA: 0x0B3445AC  token: 0x60001E2
        public System.Boolean DeleteMarker(UnityEngine.Timeline.IMarker marker) { }
        // RVA: 0x039ED390  token: 0x60001E3
        public System.Collections.Generic.IEnumerable<UnityEngine.Timeline.IMarker> GetMarkers() { }
        // RVA: 0x0B344938  token: 0x60001E4
        public System.Collections.Generic.IEnumerable<UnityEngine.Timeline.IMarker> GetClipMarkers() { }
        // RVA: 0x0B344C40  token: 0x60001E5
        public System.Int32 GetMarkerCount() { }
        // RVA: 0x0B344C50  token: 0x60001E6
        public UnityEngine.Timeline.IMarker GetMarker(System.Int32 idx) { }
        // RVA: 0x0B343C90  token: 0x60001E7
        private UnityEngine.Timeline.TimelineClip CreateClip(System.Type requestedType) { }
        // RVA: 0x0B3435DC  token: 0x60001E8
        private UnityEngine.Timeline.TimelineClip CreateAndAddNewClipOfType(System.Type requestedType) { }
        // RVA: 0x0B343A78  token: 0x60001E9
        private UnityEngine.Timeline.TimelineClip CreateClipOfType(System.Type requestedType) { }
        // RVA: 0x0B343834  token: 0x60001EA
        private UnityEngine.Timeline.TimelineClip CreateClipFromPlayableAsset(UnityEngine.Playables.IPlayableAsset asset) { }
        // RVA: 0x0B343610  token: 0x60001EB
        private UnityEngine.Timeline.TimelineClip CreateClipFromAsset(UnityEngine.ScriptableObject playableAsset) { }
        // RVA: 0x04D86210  token: 0x60001EC
        private System.Collections.Generic.IEnumerable<UnityEngine.ScriptableObject> GetMarkersRaw() { }
        // RVA: 0x0B34354C  token: 0x60001ED
        private System.Void ClearMarkers() { }
        // RVA: 0x0B3434B0  token: 0x60001EE
        private System.Void AddMarker(UnityEngine.ScriptableObject e) { }
        // RVA: 0x0B34456C  token: 0x60001EF
        private System.Boolean DeleteMarkerRaw(UnityEngine.ScriptableObject marker) { }
        // RVA: 0x03AC3FA0  token: 0x60001F0
        private System.Int32 GetTimeRangeHash() { }
        // RVA: 0x0B34341C  token: 0x60001F1
        private System.Void AddClip(UnityEngine.Timeline.TimelineClip newClip) { }
        // RVA: 0x039ECE10  token: 0x60001F2
        private UnityEngine.Playables.Playable CreateNotificationsPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable mixerPlayable, UnityEngine.GameObject go, UnityEngine.Playables.Playable timelinePlayable) { }
        // RVA: 0x03433C20  token: 0x60001F3
        private UnityEngine.Playables.Playable CreatePlayableGraph(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree, UnityEngine.Playables.Playable timelinePlayable) { }
        // RVA: 0x03E0DAE0  token: 0x60001F4
        private virtual UnityEngine.Playables.Playable CompileClips(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Collections.Generic.IList<UnityEngine.Timeline.TimelineClip> timelineClips, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree) { }
        // RVA: 0x04683BA0  token: 0x60001F5
        private System.Void GatherCompilableTracks(System.Collections.Generic.IList<UnityEngine.Timeline.TrackAsset> tracks) { }
        // RVA: 0x039EDC30  token: 0x60001F6
        private System.Void GatherNotifications(System.Collections.Generic.List<UnityEngine.Timeline.IMarker> markers) { }
        // RVA: 0x039F4170  token: 0x60001F7
        private virtual UnityEngine.Playables.Playable CreateMixerPlayableGraph(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree) { }
        // RVA: 0x03E0E7D0  token: 0x60001F8
        private System.Void ConfigureTrackAnimation(UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree, UnityEngine.GameObject go, UnityEngine.Playables.Playable blend) { }
        // RVA: 0x039F46F0  token: 0x60001F9
        private System.Void SortClips() { }
        // RVA: 0x0B3434C0  token: 0x60001FA
        private System.Void ClearClipsInternal() { }
        // RVA: 0x0B34355C  token: 0x60001FB
        private System.Void ClearSubTracksInternal() { }
        // RVA: 0x04DA66E0  token: 0x60001FC
        private System.Void OnClipMove() { }
        // RVA: 0x0B3441D0  token: 0x60001FD
        private UnityEngine.Timeline.TimelineClip CreateNewClipContainerInternal() { }
        // RVA: 0x0B343380  token: 0x60001FE
        private System.Void AddChild(UnityEngine.Timeline.TrackAsset child) { }
        // RVA: 0x0B344E40  token: 0x60001FF
        private System.Void MoveLastTrackBefore(UnityEngine.Timeline.TrackAsset asset) { }
        // RVA: 0x0B345064  token: 0x6000200
        private System.Boolean RemoveSubTrack(UnityEngine.Timeline.TrackAsset child) { }
        // RVA: 0x0B344FFC  token: 0x6000201
        private System.Void RemoveClip(UnityEngine.Timeline.TimelineClip clip) { }
        // RVA: 0x039F5F70  token: 0x6000202
        private virtual System.Void GetEvaluationTime(System.Double& outStart, System.Double& outDuration) { }
        // RVA: 0x04864FB0  token: 0x6000203
        private virtual System.Void GetSequenceTime(System.Double& outStart, System.Double& outDuration) { }
        // RVA: 0x0B3445BC  token: 0x6000204
        public virtual System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver) { }
        // RVA: 0x0B344B34  token: 0x6000205
        private UnityEngine.GameObject GetGameObjectBinding(UnityEngine.Playables.PlayableDirector director) { }
        // RVA: 0x0B345228  token: 0x6000206
        private System.Boolean ValidateClipType(System.Type clipType) { }
        // RVA: 0x041E1670  token: 0x6000207
        protected virtual System.Void OnCreateClip(UnityEngine.Timeline.TimelineClip clip) { }
        // RVA: 0x039F34C0  token: 0x6000208
        private System.Void UpdateDuration() { }
        // RVA: 0x03AC2060  token: 0x6000209
        protected internal virtual System.Int32 CalculateItemsHash() { }
        // RVA: 0x03E0DEC0  token: 0x600020A
        protected virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject gameObject, UnityEngine.Timeline.TimelineClip clip) { }
        // RVA: 0x039ED440  token: 0x600020B
        private System.Void Invalidate() { }
        // RVA: 0x0B344C60  token: 0x600020C
        private System.Double GetNotificationDuration() { }
        // RVA: 0x0468DA80  token: 0x600020D
        private virtual System.Boolean CanCompileClips() { }
        // RVA: 0x0B344DB4  token: 0x600020E
        public System.Boolean HasChildTracks() { }
        // RVA: 0x05394800  token: 0x600020F
        public virtual System.Boolean CanCreateTrackMixer() { }
        // RVA: 0x039F4860  token: 0x6000210
        private System.Boolean IsCompilable() { }
        // RVA: 0x045D14E0  token: 0x6000211
        private System.Void UpdateChildTrackCache() { }
        // RVA: 0x0B344DFC  token: 0x6000212
        private virtual System.Int32 Hash() { }
        // RVA: 0x03AC42F0  token: 0x6000213
        private System.Int32 GetClipsHash() { }
        // RVA: 0x03AC2110  token: 0x6000214
        protected static System.Int32 GetAnimationClipHash(UnityEngine.AnimationClip clip) { }
        // RVA: 0x039EC510  token: 0x6000215
        private System.Boolean HasNotifications() { }
        // RVA: 0x039EDE60  token: 0x6000216
        private System.Boolean CanCompileNotifications() { }
        // RVA: 0x04A28E30  token: 0x6000217
        private System.Boolean CanCreateMixerRecursive() { }
        // RVA: 0x0B34406C  token: 0x6000218
        public UnityEngine.Playables.Playable CreateLayerMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount) { }
        // RVA: 0x039F3990  token: 0x6000219
        protected System.Void .ctor() { }
        // RVA: 0x048BAF40  token: 0x600021A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000045  // size: 0x10
    public class ForbidScriptDefaultValueWhenExportAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000228
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000046  // size: 0x10
    public class TimelineHelpURLAttribute : System.Attribute
    {
    }

    // TypeToken: 0x2000047  // size: 0x20
    public class TrackColorAttribute : System.Attribute
    {
        // Fields
        private UnityEngine.Color m_Color;  // 0x10

        // Methods
        // RVA: 0x047F9F60  token: 0x6000229
        public System.Void .ctor(System.Single r, System.Single g, System.Single b) { }

    }

    // TypeToken: 0x2000048  // size: 0x10
    public class UseScriptDefaultValueWhenExportAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600022A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000049  // size: 0x18
    public class AudioClipProperties : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        public System.Single volume;  // 0x10

        // Methods
        // RVA: 0x04D87330  token: 0x600022B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004A  // size: 0x20
    public class AudioMixerProperties : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        public System.Single volume;  // 0x10
        public System.Single stereoPan;  // 0x14
        public System.Single spatialBlend;  // 0x18

        // Methods
        // RVA: 0x0B33D8E0  token: 0x600022C
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x04D87330  token: 0x600022D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004B  // size: 0x30
    public class AudioPlayableAsset : UnityEngine.Playables.PlayableAsset, UnityEngine.Timeline.ITimelineClipAsset
    {
        // Fields
        private UnityEngine.AudioClip m_Clip;  // 0x18
        private System.Boolean m_Loop;  // 0x20
        private System.Single m_bufferingTime;  // 0x24
        private UnityEngine.Timeline.AudioClipProperties m_ClipProperties;  // 0x28

        // Properties
        System.Single bufferingTime { get; /* RVA: 0x04D89850 */ set; /* RVA: 0x04D89860 */ }
        UnityEngine.AudioClip clip { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Boolean loop { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }
        System.Double duration { get; /* RVA: 0x0B33DD04 */ }
        System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding> outputs { get; /* RVA: 0x0B33DDAC */ }
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x0B33DCF4 */ }

        // Methods
        // RVA: 0x0B33DB70  token: 0x6000236
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go) { }
        // RVA: 0x0B33DC8C  token: 0x6000238
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004D  // size: 0xC8
    public class AudioTrack : UnityEngine.Timeline.TrackAsset
    {
        // Fields
        private UnityEngine.Timeline.AudioMixerProperties m_TrackProperties;  // 0xc0

        // Properties
        System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding> outputs { get; /* RVA: 0x0B33E550 */ }

        // Methods
        // RVA: 0x0B33E350  token: 0x6000241
        public UnityEngine.Timeline.TimelineClip CreateClip(UnityEngine.AudioClip clip) { }
        // RVA: 0x0B33DE1C  token: 0x6000242
        private virtual UnityEngine.Playables.Playable CompileClips(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Collections.Generic.IList<UnityEngine.Timeline.TimelineClip> timelineClips, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree) { }
        // RVA: 0x0B33E464  token: 0x6000244
        private System.Void OnValidate() { }
        // RVA: 0x0B33E4D0  token: 0x6000245
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x28
    public sealed struct ACConditionDescriptor
    {
        // Fields
        private System.Int32 m_ConditionMode;  // 0x10
        private System.String m_ConditionEvent;  // 0x18
        private System.Single m_EventTreshold;  // 0x20

    }

    // TypeToken: 0x2000050  // size: 0x10
    public class ACTransitionDescriptor
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600024E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000051  // size: 0x14
    public sealed struct DialogTimelineTrackType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.DialogTimelineTrackType Default;  // const
        public static UnityEngine.Timeline.DialogTimelineTrackType Morph;  // const
        public static UnityEngine.Timeline.DialogTimelineTrackType Animation;  // const
        public static UnityEngine.Timeline.DialogTimelineTrackType Offset;  // const
        public static UnityEngine.Timeline.DialogTimelineTrackType LookAt;  // const

    }

    // TypeToken: 0x2000052  // size: 0x10
    public static class BeyondCommunicator
    {
        // Fields
        public static System.Boolean setAddonLayerAdditive;  // static @ 0x0
        private static System.Action<UnityEngine.Animator,UnityEngine.Animations.AnimationLayerMixerPlayable,UnityEngine.Timeline.AnimationTrack,UnityEngine.ScriptableObject,UnityEngine.Transform,UnityEngine.Timeline.CommunicatorParam> <ConnectToJobSync>k__BackingField;  // static @ 0x8
        private static System.Func<UnityEngine.Timeline.ITimelineRootMarker,UnityEngine.GameObject,System.String,System.Boolean,System.Boolean> <TrySetTrackBindingEntityVisible>k__BackingField;  // static @ 0x10

        // Properties
        System.Action<UnityEngine.Animator,UnityEngine.Animations.AnimationLayerMixerPlayable,UnityEngine.Timeline.AnimationTrack,UnityEngine.ScriptableObject,UnityEngine.Transform,UnityEngine.Timeline.CommunicatorParam> ConnectToJobSync { get; /* RVA: 0x0B33E5C0 */ set; /* RVA: 0x0B33E660 */ }
        System.Func<UnityEngine.Timeline.ITimelineRootMarker,UnityEngine.GameObject,System.String,System.Boolean,System.Boolean> TrySetTrackBindingEntityVisible { get; /* RVA: 0x0B33E610 */ set; /* RVA: 0x0B33E6C0 */ }

        // Methods
        // RVA: 0x04D7AC90  token: 0x6000253
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000053  // size: 0x30
    public class BlendMotionDescriptor
    {
        // Fields
        public UnityEngine.AnimationClip motion;  // 0x10
        public System.String poseTimerParam;  // 0x18
        public System.String blendParam;  // 0x20
        public System.Single threshold;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000254
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000054  // size: 0x30
    public class BlendTreeDescriptor
    {
        // Fields
        public UnityEngine.Timeline.BlendTreeDescriptor.DescriptorBlendTreeType blendType;  // 0x10
        public System.Collections.Generic.List<UnityEngine.Timeline.BlendMotionDescriptor> motionDescriptorList;  // 0x18
        private System.String <speedParameter>k__BackingField;  // 0x20
        private System.Boolean <speedParameterActive>k__BackingField;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000255
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000056
    public interface ITimelineRootMarker
    {
        // Properties
        UnityEngine.Playables.PlayableDirector topDirector { get; /* RVA: -1  // abstract */ }
        UnityEngine.GameObject rootGo { get; /* RVA: -1  // abstract */ }
        System.Boolean useEarlyUpdate { get; /* RVA: -1  // abstract */ }
        System.Boolean enableLoop { get; /* RVA: -1  // abstract */ }
        System.Boolean isOptimizingHolding { get; /* RVA: -1  // abstract */ }
        System.Boolean enableTimelineAsyncCompile { get; /* RVA: -1  // abstract */ }
        System.Boolean isTimelineAsyncCompiling { get; /* RVA: -1  // abstract */ }
        System.Boolean isTimelineAsyncCompileComplete { get; /* RVA: -1  // abstract */ }
        System.Boolean onlyEvaluateTopDirector { get; /* RVA: -1  // abstract */ }
        System.Single fixedFrameRate { get; /* RVA: -1  // abstract */ }
        System.Single manualTickDeltaTime { get; /* RVA: -1  // abstract */ }
        System.Boolean isManualTicking { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.Dictionary<UnityEngine.Playables.PlayableDirector,UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.DirectorControlPlayable>> director2ControlPlayable { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000259
        public virtual System.Void RegisterLoopSegmentRuntimeClip(System.String key, UnityEngine.Timeline.RuntimeClip runtimeClip) { }
        // RVA: -1  // abstract  token: 0x600025A
        public virtual System.Void UpdateAllVFXFollowBoneTools() { }
        // RVA: -1  // abstract  token: 0x6000266
        public virtual System.Void RegisterAsyncScriptAnimationJobSyncInit(UnityEngine.Timeline.IAsyncScriptAnimationJobSyncInit sync) { }

    }

    // TypeToken: 0x2000057  // size: 0x38
    public class LayerDescriptor : UnityEngine.ScriptableObject
    {
        // Fields
        public System.String layerName;  // 0x18
        public UnityEngine.AvatarMask avatarMask;  // 0x20
        public System.Boolean isAdditive;  // 0x28
        public System.Collections.Generic.List<UnityEngine.Timeline.StateDescriptor> stateDescriptors;  // 0x30

        // Methods
        // RVA: 0x03573370  token: 0x6000267
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000058  // size: 0x58
    public class MotionDescriptor
    {
        // Fields
        public System.String stateName;  // 0x10
        public UnityEngine.AnimationClip motion;  // 0x18
        public System.Single speed;  // 0x20
        public System.String speedParameter;  // 0x28
        public System.Boolean speedParameterActive;  // 0x30
        public System.String mirrorParameter;  // 0x38
        public System.Boolean mirrorParameterActive;  // 0x40
        public System.String timeParameter;  // 0x48
        public System.Boolean timeParameterActive;  // 0x50

        // Methods
        // RVA: 0x041E1670  token: 0x6000268
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000059  // size: 0x40
    public class StateDescriptor
    {
        // Fields
        public System.String stateName;  // 0x10
        public System.Boolean isDefaultState;  // 0x18
        public System.String stateWeightParameter;  // 0x20
        public System.Boolean stateWeightParameterActive;  // 0x28
        public System.Boolean isBlendTree;  // 0x29
        public UnityEngine.Timeline.MotionDescriptor motionDescriptor;  // 0x30
        public UnityEngine.Timeline.BlendTreeDescriptor blendTreeDescriptor;  // 0x38

        // Methods
        // RVA: 0x041E1670  token: 0x6000269
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005A  // size: 0x14
    public sealed struct ClipCaps
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.ClipCaps None;  // const
        public static UnityEngine.Timeline.ClipCaps Looping;  // const
        public static UnityEngine.Timeline.ClipCaps Extrapolation;  // const
        public static UnityEngine.Timeline.ClipCaps ClipIn;  // const
        public static UnityEngine.Timeline.ClipCaps SpeedMultiplier;  // const
        public static UnityEngine.Timeline.ClipCaps Blending;  // const
        public static UnityEngine.Timeline.ClipCaps AutoScale;  // const
        public static UnityEngine.Timeline.ClipCaps ClipMarker;  // const
        public static UnityEngine.Timeline.ClipCaps DialogLoopSegment;  // const
        public static UnityEngine.Timeline.ClipCaps IgniteOnce;  // const
        public static UnityEngine.Timeline.ClipCaps OnlyDeltaTime;  // const
        public static UnityEngine.Timeline.ClipCaps DialogJumpSegment;  // const
        public static UnityEngine.Timeline.ClipCaps EvaluateTwiceWhenEnabled;  // const
        public static UnityEngine.Timeline.ClipCaps DynamicLink;  // const
        public static UnityEngine.Timeline.ClipCaps CustomBehaviourControl;  // const
        public static UnityEngine.Timeline.ClipCaps ConditionBlend;  // const
        public static UnityEngine.Timeline.ClipCaps All;  // const

    }

    // TypeToken: 0x200005B  // size: 0x10
    public static class TimelineClipCapsExtensions
    {
        // Methods
        // RVA: 0x04B074C0  token: 0x600026A
        public static System.Boolean SupportsExtrapolation(UnityEngine.Timeline.TimelineClip clip) { }
        // RVA: 0x039F6DA0  token: 0x600026B
        public static System.Boolean HasAny(UnityEngine.Timeline.ClipCaps caps, UnityEngine.Timeline.ClipCaps flags) { }

    }

    // TypeToken: 0x200005C  // size: 0x88
    public class ControlPlayableAsset : Beyond.Gameplay.Core.BeyondUseAutoBindingPlayableAsset, UnityEngine.Timeline.IPropertyPreview, UnityEngine.Timeline.ITimelineClipAsset
    {
        // Fields
        private static System.Int32 k_MaxRandInt;  // const
        private static readonly System.Collections.Generic.List<UnityEngine.Playables.PlayableDirector> k_EmptyDirectorsList;  // static @ 0x0
        private static readonly System.Collections.Generic.List<UnityEngine.ParticleSystem> k_EmptyParticlesList;  // static @ 0x8
        private static readonly System.Collections.Generic.HashSet<UnityEngine.ParticleSystem> s_SubEmitterCollector;  // static @ 0x10
        public UnityEngine.ExposedReference<UnityEngine.GameObject> sourceGameObject;  // 0x30
        public UnityEngine.GameObject prefabGameObject;  // 0x40
        public System.Boolean updateParticle;  // 0x48
        public System.UInt32 particleRandomSeed;  // 0x4c
        public System.Boolean forceRuntimeSimulate;  // 0x50
        public System.Boolean updateDirector;  // 0x51
        public System.String directorControlPath;  // 0x58
        public System.Boolean updateITimeControl;  // 0x60
        public System.Boolean searchHierarchy;  // 0x61
        public System.Boolean active;  // 0x62
        public UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState postPlayback;  // 0x64
        private System.Boolean m_OnlyDeltaTime;  // 0x68
        private System.Boolean m_IgniteOnce;  // 0x69
        private UnityEngine.Playables.PlayableAsset m_ControlDirectorAsset;  // 0x70
        private System.Double m_Duration;  // 0x78
        private System.Boolean m_SupportLoop;  // 0x80
        private static System.Collections.Generic.HashSet<UnityEngine.Playables.PlayableDirector> s_ProcessedDirectors;  // static @ 0x18
        private static System.Collections.Generic.HashSet<UnityEngine.GameObject> s_CreatedPrefabs;  // static @ 0x20
        private System.Boolean <controllingDirectors>k__BackingField;  // 0x81
        private System.Boolean <controllingParticles>k__BackingField;  // 0x82

        // Properties
        UnityEngine.GameObject bindingGameObject { get; /* RVA: 0x04A30BD0 */ }
        System.Boolean controllingDirectors { get; /* RVA: 0x04D8FEE0 */ set; /* RVA: 0x04D8FEF0 */ }
        System.Boolean controllingParticles { get; /* RVA: 0x04DA5E30 */ set; /* RVA: 0x04DA5E60 */ }
        System.Boolean OnlyDeltaTime { get; /* RVA: 0x04D86B40 */ set; /* RVA: 0x04D86BA0 */ }
        System.Boolean IgniteOnce { get; /* RVA: 0x04D86B10 */ set; /* RVA: 0x04D86B70 */ }
        System.Double duration { get; /* RVA: 0x04D86E70 */ }
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x039F6D70 */ }

        // Methods
        // RVA: 0x04CEBB80  token: 0x6000275
        public System.Void OnEnable() { }
        // RVA: 0x039F0970  token: 0x6000278
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go) { }
        // RVA: 0x03E0E320  token: 0x6000279
        private static UnityEngine.Playables.Playable ConnectPlayablesToMixer(UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.List<UnityEngine.Playables.Playable> playables) { }
        // RVA: 0x0450F390  token: 0x600027A
        private System.Void CreateActivationPlayable(UnityEngine.GameObject root, UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.List<UnityEngine.Playables.Playable> outplayables) { }
        // RVA: 0x047C3FF0  token: 0x600027B
        protected virtual System.Void SearchHierarchyAndConnectParticleSystem(System.Collections.Generic.IEnumerable<UnityEngine.ParticleSystem> particleSystems, System.Single clipIn, System.Single speedMultiplier, UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.List<UnityEngine.Playables.Playable> outplayables) { }
        // RVA: 0x0450F680  token: 0x600027C
        private System.Void SearchHierarchyAndConnectDirector(System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableDirector> directors, UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.List<UnityEngine.Playables.Playable> outplayables, System.Boolean disableSelfReferences) { }
        // RVA: 0x047DABD0  token: 0x600027D
        private static System.Void SearchHierarchyAndConnectControlableScripts(System.Collections.Generic.IEnumerable<UnityEngine.MonoBehaviour> controlableScripts, UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.List<UnityEngine.Playables.Playable> outplayables) { }
        // RVA: 0x03E0E520  token: 0x600027E
        private static System.Void ConnectMixerAndPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable mixer, UnityEngine.Playables.Playable playable, System.Int32 portIndex) { }
        // RVA: 0x04892110  token: 0x600027F
        protected virtual UnityEngine.GameObject ResolveSourceGameObject(UnityEngine.IExposedPropertyTable resolver) { }
        // RVA: -1  // generic def  token: 0x6000280
        private System.Collections.Generic.IList<T> GetComponent(UnityEngine.GameObject gameObject) { }
        // RVA: 0x049722F0  token: 0x6000281
        private System.Collections.Generic.IList<UnityEngine.Playables.PlayableDirector> GetControllableDirectors(UnityEngine.GameObject gameObject) { }
        // RVA: 0x049B3AA0  token: 0x6000282
        private static System.Collections.Generic.IEnumerable<UnityEngine.MonoBehaviour> GetControlableScripts(UnityEngine.GameObject root) { }
        // RVA: 0x039EFB70  token: 0x6000283
        private System.Void UpdateDurationAndLoopFlag(System.Collections.Generic.IList<UnityEngine.Playables.PlayableDirector> directors, System.Collections.Generic.IList<UnityEngine.ParticleSystem> particleSystems) { }
        // RVA: 0x04898B60  token: 0x6000284
        private System.Collections.Generic.IList<UnityEngine.ParticleSystem> GetControllableParticleSystems(UnityEngine.GameObject go) { }
        // RVA: 0x0B33EAF8  token: 0x6000285
        private static System.Void GetControllableParticleSystems(UnityEngine.Transform t, System.Collections.Generic.ICollection<UnityEngine.ParticleSystem> roots, System.Collections.Generic.HashSet<UnityEngine.ParticleSystem> subEmitters) { }
        // RVA: 0x0B33E794  token: 0x6000286
        private static System.Void CacheSubEmitters(UnityEngine.ParticleSystem ps, System.Collections.Generic.HashSet<UnityEngine.ParticleSystem> subEmitters) { }
        // RVA: 0x0B33E86C  token: 0x6000287
        public virtual System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver) { }
        // RVA: 0x0B33EF6C  token: 0x6000288
        private System.Void PreviewParticlesWithControlPlayableAsset(UnityEngine.Timeline.IPropertyCollector driver, System.Collections.Generic.IEnumerable<UnityEngine.ParticleSystem> particles) { }
        // RVA: 0x0B33EFC8  token: 0x6000289
        private static System.Void PreviewParticles(UnityEngine.Timeline.IPropertyCollector driver, System.Collections.Generic.IEnumerable<UnityEngine.ParticleSystem> particles) { }
        // RVA: 0x0B33EC48  token: 0x600028A
        private static System.Void PreviewActivation(UnityEngine.Timeline.IPropertyCollector driver, System.Collections.Generic.IEnumerable<UnityEngine.GameObject> objects) { }
        // RVA: 0x0B33F16C  token: 0x600028B
        private static System.Void PreviewTimeControl(UnityEngine.Timeline.IPropertyCollector driver, UnityEngine.Playables.PlayableDirector director, System.Collections.Generic.IEnumerable<UnityEngine.MonoBehaviour> scripts) { }
        // RVA: 0x0B33ED94  token: 0x600028C
        private static System.Void PreviewDirectors(UnityEngine.Timeline.IPropertyCollector driver, System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableDirector> directors) { }
        // RVA: 0x047D9DB0  token: 0x600028D
        public System.Void .ctor() { }
        // RVA: 0x049C5970  token: 0x600028E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005E  // size: 0xC0
    public class ControlTrack : UnityEngine.Timeline.TrackAsset
    {
        // Methods
        // RVA: 0x039F4B70  token: 0x6000297
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005F  // size: 0x30
    public class AdditiveAnimApplyMono : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Timeline.AdditiveAnimApplyMono.TransformAdditive> pendingAdditives;  // 0x18
        private System.Boolean hasnewPendingUpdates;  // 0x20
        private UnityEngine.Timeline.ITimelineRootMarker _parentTimelineRoot;  // 0x28

        // Methods
        // RVA: 0x0B346AF0  token: 0x6000298
        private System.Void Awake() { }
        // RVA: 0x0B346D2C  token: 0x6000299
        public System.Void RecordAdditiveTransform(UnityEngine.Transform bone, UnityEngine.Vector3 position, UnityEngine.Vector3 rotationEuler, UnityEngine.Vector3 scale) { }
        // RVA: 0x0B346CBC  token: 0x600029A
        private System.Void LateUpdate() { }
        // RVA: 0x0B34672C  token: 0x600029B
        public System.Void ApplyPendingTransforms() { }
        // RVA: 0x0B346B68  token: 0x600029C
        public System.Void ClearPendingTransforms() { }
        // RVA: 0x0B346CE8  token: 0x600029D
        private System.Void NotifyCutsceneRootToUpdateVFXTools() { }
        // RVA: 0x0B346E88  token: 0x600029E
        public System.Void RegisterRootComponent() { }
        // RVA: 0x0B346F08  token: 0x600029F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000061  // size: 0x48
    public class AdditiveAnimMixer : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        public UnityEngine.Timeline.AdditiveAnimTrack track;  // 0x10
        private UnityEngine.GameObject bindingGo;  // 0x18
        private System.Boolean thisFrameAlreadyCalcualted;  // 0x20
        private System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Timeline.AdditiveAnimMixer.TransformAdditive> additiveCache;  // 0x28
        private System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform> bonePaths;  // 0x30
        private System.Collections.Generic.HashSet<System.String> notFoundBonePaths;  // 0x38
        private UnityEngine.Timeline.AdditiveAnimApplyMono cachedApplyMono;  // 0x40

        // Methods
        // RVA: 0x04D90CE0  token: 0x60002A2
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B346F88  token: 0x60002A3
        public virtual System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData) { }
        // RVA: 0x0B347338  token: 0x60002A4
        private System.Void _AddToAdditiveCache(UnityEngine.Timeline.AdditiveAnimPlayable additiveAnimPlayable, System.Single clipTime, System.Single weight) { }
        // RVA: 0x0B347A88  token: 0x60002A5
        private System.Void _ApplyAdditive() { }
        // RVA: 0x0B347D48  token: 0x60002A6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000063  // size: 0x20
    public class AdditiveAnimPlayable : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Timeline.AdditiveAnimPlayableAsset.AnimationCurveInfo> extractedCurves;  // 0x10
        private System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<UnityEngine.Timeline.AdditiveAnimPlayableAsset.AnimationCurveInfo>> _curveCache;  // 0x18

        // Methods
        // RVA: 0x0B348358  token: 0x60002A9
        public virtual System.Void OnPlayableCreate(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x0B3480C8  token: 0x60002AA
        public System.Void InitializeCurveCache() { }
        // RVA: 0x0B3483D0  token: 0x60002AB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000064  // size: 0x48
    public class AdditiveAnimPlayableAsset : UnityEngine.Playables.PlayableAsset, UnityEngine.Timeline.ITimelineClipAsset
    {
        // Fields
        public UnityEngine.AnimationClip additiveClip;  // 0x18
        private UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.AdditiveAnimPlayable> additiveAnimPlayable;  // 0x20
        public System.Collections.Generic.List<UnityEngine.Timeline.AdditiveAnimPlayableAsset.AnimationCurveInfo> extractedCurves;  // 0x30
        private System.Boolean m_OnlyDeltaTime;  // 0x38
        private System.Boolean m_IgniteOnce;  // 0x39
        private UnityEngine.AnimationClip _lastProcessedClip;  // 0x40

        // Properties
        System.Boolean OnlyDeltaTime { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        System.Boolean IgniteOnce { get; /* RVA: 0x04D86650 */ set; /* RVA: 0x04D86680 */ }
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x0B3480A4 */ }

        // Methods
        // RVA: 0x0B347E54  token: 0x60002B0
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner) { }
        // RVA: 0x0B347F80  token: 0x60002B1
        private System.Void _InitializeBehaviourCurveCache() { }
        // RVA: 0x0B348024  token: 0x60002B3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000066  // size: 0xD0
    public class AdditiveAnimTrack : UnityEngine.Timeline.TrackAsset
    {
        // Fields
        private static System.String k_DefaultRecordableClipName;  // const
        private System.Boolean m_InfiniteClip;  // 0xc0
        private UnityEngine.AnimationClip m_RecordableClip;  // 0xc8

        // Methods
        // RVA: 0x0B348470  token: 0x60002B5
        public virtual UnityEngine.Playables.Playable CreateTrackMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount) { }
        // RVA: 0x0B34856C  token: 0x60002B6
        public virtual System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver) { }
        // RVA: 0x0B348448  token: 0x60002B7
        public UnityEngine.Timeline.TimelineClip CreateRecordableClip(System.String animClipName) { }
        // RVA: 0x0B348744  token: 0x60002B8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000067  // size: 0x10
    public class CombineToTopPlayableAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002B9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000068  // size: 0x20
    public class CustomControlPlayableAsset : UnityEngine.Playables.PlayableAsset, UnityEngine.Timeline.ITimelineClipAsset, UnityEngine.Timeline.ICustomControlBehaviour
    {
        // Fields
        protected UnityEngine.Timeline.CustomControlPlayableBehaviour m_CustomControlBehaviour;  // 0x18

        // Properties
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x0B348E50 */ }
        UnityEngine.Timeline.ClipCaps extraClipCaps { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x0B348D48  token: 0x60002BC
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner) { }
        // RVA: 0x0B348DF4  token: 0x60002BD
        public virtual System.Void OnClipEnable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B348D98  token: 0x60002BE
        public virtual System.Void OnClipDisable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x03573370  token: 0x60002BF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000069  // size: 0x10
    public class CustomControlPlayableBehaviour : UnityEngine.Playables.PlayableBehaviour, UnityEngine.Timeline.ICustomControlBehaviour
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002C0
        public virtual System.Void OnClipEnable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x041E1670  token: 0x60002C1
        public virtual System.Void OnClipDisable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x041E1670  token: 0x60002C2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006A
    public interface ICustomControlBehaviour
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60002C3
        public virtual System.Void OnClipEnable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: -1  // abstract  token: 0x60002C4
        public virtual System.Void OnClipDisable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }

    }

    // TypeToken: 0x200006B
    public interface IDynamicBinding
    {
    }

    // TypeToken: 0x200006C
    public interface ITimelineAnimationEventContextBinding
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60002C5
        public virtual System.Void RegisterAnimationEventRuntimeContext(System.Int32 playableId, UnityEngine.AnimationEvent[] animationEvents, UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode postAudioEventTriggerMode, System.Single postAudioEventTriggerThreshold, UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode customFootStepTriggerMode, System.Single customFootStepTriggerThreshold, System.Single curClipTime, System.Single curWeight, UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviour clipEndAudioBehaviour) { }
        // RVA: -1  // abstract  token: 0x60002C6
        public virtual System.Void UnregisterAnimationEventRuntimeContext(System.Int32 playableId, UnityEngine.AnimationEvent[] animationEvents) { }
        // RVA: -1  // abstract  token: 0x60002C7
        public virtual System.Void UpdateClipInfo(System.Int32 playableId, System.Single curClipTime, System.Single curClipWeight) { }
        // RVA: -1  // abstract  token: 0x60002C8
        public virtual System.Void StopAllAudioForPlayable(System.Int32 playableId, System.Int32 fadeOutTimeMs) { }

    }

    // TypeToken: 0x200006D  // size: 0x10
    public class SegmentLoopPlayableBehaviour : UnityEngine.Playables.PlayableBehaviour
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002C9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006E  // size: 0x20
    public class SegmentLoopPlayableClip : UnityEngine.Playables.PlayableAsset, UnityEngine.Timeline.ITimelineClipAsset
    {
        // Fields
        public System.String loopSegmentKey;  // 0x18

        // Properties
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x04DB8E50 */ }

        // Methods
        // RVA: 0x0B34BF90  token: 0x60002CA
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner) { }
        // RVA: 0x03573370  token: 0x60002CC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006F  // size: 0xC0
    public class SegmentLoopTrack : UnityEngine.Timeline.TrackAsset
    {
        // Methods
        // RVA: 0x0B34C03C  token: 0x60002CD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000070  // size: 0x18
    public sealed struct DiscreteTime : System.IComparable
    {
        // Fields
        private static System.Double k_Tick;  // const
        public static readonly UnityEngine.Timeline.DiscreteTime kMaxTime;  // static @ 0x0
        private readonly System.Int64 m_DiscreteTime;  // 0x10

        // Properties
        System.Double tickValue { get; /* RVA: 0x04DBA600 */ }

        // Methods
        // RVA: 0x04D88B60  token: 0x60002CF
        private System.Void .ctor(System.Int64 time) { }
        // RVA: 0x039F3020  token: 0x60002D0
        public System.Void .ctor(System.Double time) { }
        // RVA: 0x039F30E0  token: 0x60002D1
        public System.Void .ctor(System.Int32 time) { }
        // RVA: 0x04DBA5D0  token: 0x60002D2
        public UnityEngine.Timeline.DiscreteTime OneTickBefore() { }
        // RVA: 0x04DBA5C0  token: 0x60002D3
        public UnityEngine.Timeline.DiscreteTime OneTickAfter() { }
        // RVA: 0x0464E630  token: 0x60002D4
        public static UnityEngine.Timeline.DiscreteTime FromTicks(System.Int64 ticks) { }
        // RVA: 0x0B348FC0  token: 0x60002D5
        public virtual System.Int32 CompareTo(System.Object obj) { }
        // RVA: 0x02EECF30  token: 0x60002D6
        public System.Boolean Equals(UnityEngine.Timeline.DiscreteTime other) { }
        // RVA: 0x0B349028  token: 0x60002D7
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x039F31A0  token: 0x60002D8
        private static System.Int64 DoubleToDiscreteTime(System.Double time) { }
        // RVA: 0x039F3140  token: 0x60002D9
        private static System.Int64 IntToDiscreteTime(System.Int32 time) { }
        // RVA: 0x04DBA5E0  token: 0x60002DA
        private static System.Double ToDouble(System.Int64 time) { }
        // RVA: 0x039F3080  token: 0x60002DB
        public static System.Double op_Explicit(UnityEngine.Timeline.DiscreteTime b) { }
        // RVA: 0x039F2FF0  token: 0x60002DC
        public static UnityEngine.Timeline.DiscreteTime op_Explicit(System.Double time) { }
        // RVA: 0x039F2FC0  token: 0x60002DD
        public static UnityEngine.Timeline.DiscreteTime op_Implicit(System.Int32 time) { }
        // RVA: 0x02FB1840  token: 0x60002DE
        public static System.Boolean op_Equality(UnityEngine.Timeline.DiscreteTime lhs, UnityEngine.Timeline.DiscreteTime rhs) { }
        // RVA: 0x0B34909C  token: 0x60002DF
        public static System.Boolean op_Inequality(UnityEngine.Timeline.DiscreteTime lhs, UnityEngine.Timeline.DiscreteTime rhs) { }
        // RVA: 0x039F3720  token: 0x60002E0
        public static System.Boolean op_LessThanOrEqual(UnityEngine.Timeline.DiscreteTime lhs, UnityEngine.Timeline.DiscreteTime rhs) { }
        // RVA: 0x02FCE050  token: 0x60002E1
        public static System.Boolean op_GreaterThanOrEqual(UnityEngine.Timeline.DiscreteTime lhs, UnityEngine.Timeline.DiscreteTime rhs) { }
        // RVA: 0x04DBA610  token: 0x60002E2
        public static UnityEngine.Timeline.DiscreteTime op_Subtraction(UnityEngine.Timeline.DiscreteTime lhs, UnityEngine.Timeline.DiscreteTime rhs) { }
        // RVA: 0x0B349094  token: 0x60002E3
        public virtual System.String ToString() { }
        // RVA: 0x04DA4FD0  token: 0x60002E4
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x039F3210  token: 0x60002E5
        public static UnityEngine.Timeline.DiscreteTime Max(UnityEngine.Timeline.DiscreteTime lhs, UnityEngine.Timeline.DiscreteTime rhs) { }
        // RVA: 0x039F2A00  token: 0x60002E6
        public static System.Int64 GetNearestTick(System.Double time) { }
        // RVA: 0x04D75940  token: 0x60002E7
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000071  // size: 0x30
    public class InfiniteRuntimeClip : UnityEngine.Timeline.RuntimeElement
    {
        // Fields
        private UnityEngine.Playables.Playable m_Playable;  // 0x18
        private static readonly System.Int64 kIntervalEnd;  // static @ 0x0
        private System.Int32 <optionIndex>k__BackingField;  // 0x28

        // Properties
        System.Int64 intervalStart { get; /* RVA: 0x011EC580 */ }
        System.Int64 intervalEnd { get; /* RVA: 0x0B349590 */ }
        System.Int32 optionIndex { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        System.Boolean enable { set; /* RVA: 0x0B3495E0 */ }

        // Methods
        // RVA: 0x04D890D0  token: 0x60002E8
        public System.Void .ctor(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x0B3494A8  token: 0x60002EE
        public virtual System.Void EvaluateAt(System.Double localTime, UnityEngine.Playables.FrameData frameData, UnityEngine.Timeline.ITimelineRootMarker timelineRoot) { }
        // RVA: 0x0B349448  token: 0x60002EF
        public virtual System.Void DisableAt(System.Double localTime, System.Double rootDuration, UnityEngine.Playables.FrameData frameData) { }
        // RVA: 0x0B349500  token: 0x60002F0
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000072
    public interface IInterval
    {
        // Properties
        System.Int64 intervalStart { get; /* RVA: -1  // abstract */ }
        System.Int64 intervalEnd { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000073
    public interface IRuntimeReset
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60002F3
        public virtual System.Void RuntimeReset() { }

    }

    // TypeToken: 0x2000074
    public interface IOption
    {
    }

    // TypeToken: 0x2000075  // size: 0x28
    public sealed struct IntervalTreeNode
    {
        // Fields
        public System.Int64 center;  // 0x10
        public System.Int32 first;  // 0x18
        public System.Int32 last;  // 0x1c
        public System.Int32 left;  // 0x20
        public System.Int32 right;  // 0x24

    }

    // TypeToken: 0x2000076
    public class IntervalTree`1
    {
        // Fields
        private static System.Int32 kMinNodeSize;  // const
        private static System.Int32 kInvalidNode;  // const
        private static System.Int64 kCenterUnknown;  // const
        private readonly System.Collections.Generic.List<UnityEngine.Timeline.IntervalTree.Entry<T>> m_Entries;  // 0x0
        private readonly System.Collections.Generic.List<UnityEngine.Timeline.IntervalTreeNode> m_Nodes;  // 0x0
        private System.Boolean <dirty>k__BackingField;  // 0x0

        // Properties
        System.Boolean dirty { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60002F6
        public System.Void Add(T item) { }
        // RVA: -1  // not resolved  token: 0x60002F7
        public System.Void IntersectsWith(System.Int64 value, System.Collections.Generic.List<T> results) { }
        // RVA: -1  // not resolved  token: 0x60002F8
        public System.Void IntersectsWithRange(System.Int64 start, System.Int64 end, System.Collections.Generic.List<T> results) { }
        // RVA: -1  // not resolved  token: 0x60002F9
        public System.Void UpdateIntervals() { }
        // RVA: -1  // not resolved  token: 0x60002FA
        public System.Void RuntimeReset() { }
        // RVA: -1  // not resolved  token: 0x60002FB
        private System.Void Query(UnityEngine.Timeline.IntervalTreeNode intervalTreeNode, System.Int64 value, System.Collections.Generic.List<T> results) { }
        // RVA: -1  // not resolved  token: 0x60002FC
        private System.Void QueryRange(UnityEngine.Timeline.IntervalTreeNode intervalTreeNode, System.Int64 start, System.Int64 end, System.Collections.Generic.List<T> results) { }
        // RVA: -1  // not resolved  token: 0x60002FD
        public System.Void Rebuild() { }
        // RVA: -1  // not resolved  token: 0x60002FE
        private System.Int32 Rebuild(System.Int32 start, System.Int32 end) { }
        // RVA: -1  // not resolved  token: 0x60002FF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000078  // size: 0xA8
    public class RuntimeClip : UnityEngine.Timeline.RuntimeClipBase
    {
        // Fields
        public System.Double runtimeLocalTimeOffset;  // 0x20
        public System.Double runtimeMixOutTime;  // 0x28
        public System.Double runtimeMixOutDuration;  // 0x30
        public System.Double runtimeMixInTime;  // 0x38
        public System.Double runtimeMixInDuration;  // 0x40
        public System.Boolean runtimeLooping;  // 0x48
        public System.Boolean runtimeLoopingMute;  // 0x49
        private System.Boolean _isInMixIn;  // 0x4a
        private System.Boolean _isMixIn;  // 0x4b
        private System.Boolean _hadIgniteOnce;  // 0x4c
        private System.Double lastTickTime;  // 0x50
        private System.Boolean _onlyDeltaTimeLooped;  // 0x58
        private System.Int32 <optionIndex>k__BackingField;  // 0x5c
        private System.Int32 m_originalPort;  // 0x60
        private System.Boolean _hadClipEnabled;  // 0x64
        public System.Boolean isRuntimeJumpExhaust;  // 0x65
        private UnityEngine.Timeline.TimelineClip m_Clip;  // 0x68
        private UnityEngine.Playables.Playable m_Playable;  // 0x70
        private UnityEngine.Playables.Playable m_ParentMixer;  // 0x80
        private UnityEngine.Playables.Playable m_RootPlayable;  // 0x90
        private System.Boolean m_enabled;  // 0xa0

        // Properties
        System.Int32 optionIndex { get; /* RVA: 0x04D86560 */ set; /* RVA: 0x04D865C0 */ }
        System.Boolean dynamicLink { get; /* RVA: 0x04B09EB0 */ }
        System.Boolean customBehaviourControl { get; /* RVA: 0x04B09EE0 */ }
        System.Double start { get; /* RVA: 0x04891CA0 */ }
        System.Double end { get; /* RVA: 0x0B34B9B0 */ }
        System.Double duration { get; /* RVA: 0x039F7540 */ }
        UnityEngine.Timeline.TimelineClip clip { get; /* RVA: 0x04D86230 */ }
        UnityEngine.Playables.Playable mixer { get; /* RVA: 0x04D90620 */ }
        UnityEngine.Playables.Playable playable { get; /* RVA: 0x04D8FD40 */ }
        System.Int64 intervalStart { get; /* RVA: 0x045C5CC0 */ }
        System.Int64 intervalEnd { get; /* RVA: 0x045C5D30 */ }
        System.Boolean enable { set; /* RVA: 0x039F5E10 */ }
        System.Boolean isEnabling { get; /* RVA: 0x04D88020 */ }

        // Methods
        // RVA: 0x03E0E630  token: 0x6000307
        public System.Void .ctor(UnityEngine.Timeline.TimelineClip clip, UnityEngine.Playables.Playable clipPlayable, UnityEngine.Playables.Playable parentMixer, System.Int32 originalPort) { }
        // RVA: 0x03E0E690  token: 0x6000308
        private System.Void Create(UnityEngine.Timeline.TimelineClip clip, UnityEngine.Playables.Playable clipPlayable, UnityEngine.Playables.Playable parentMixer, System.Int32 originalPort) { }
        // RVA: 0x039F7020  token: 0x6000310
        public System.Void SetTime(System.Double time) { }
        // RVA: 0x03E0E2C0  token: 0x6000311
        public System.Void SetDuration(System.Double duration) { }
        // RVA: 0x039F7080  token: 0x6000312
        public virtual System.Void EvaluateAt(System.Double localTime, UnityEngine.Playables.FrameData frameData, UnityEngine.Timeline.ITimelineRootMarker timelineRoot) { }
        // RVA: 0x039F7660  token: 0x6000313
        public System.Single EvaluateMixIn(System.Single localTime) { }
        // RVA: 0x039F7570  token: 0x6000314
        public System.Single EvaluateMixOut(System.Single localTime) { }
        // RVA: 0x039F72E0  token: 0x6000315
        public System.Double ToLocalTime(System.Double localTime, UnityEngine.Playables.FrameData frameData, UnityEngine.Timeline.ITimelineRootMarker timelineRoot) { }
        // RVA: 0x039F73A0  token: 0x6000316
        private System.Double ApplyTimeRemapCurve(System.Double localTime, System.Double clipTime) { }
        // RVA: 0x0B34B508  token: 0x6000317
        public virtual System.Void DisableAt(System.Double localTime, System.Double rootDuration, UnityEngine.Playables.FrameData frameData) { }
        // RVA: 0x0489D700  token: 0x6000318
        public virtual System.Boolean CheckIfRuntimeTimeLoop(System.Boolean& canSkip, System.Double& reverseTime) { }
        // RVA: 0x0B34B3DC  token: 0x6000319
        public virtual System.Boolean CheckIfRuntimeTimeJump(System.Double& jumpTime, System.Boolean& isReverseJump) { }
        // RVA: 0x0B34B8B8  token: 0x600031A
        public virtual System.Void RuntimeLoopOver() { }
        // RVA: 0x0B34B888  token: 0x600031B
        public virtual System.Void RuntimeLoopOn() { }
        // RVA: 0x0B34B6A8  token: 0x600031C
        public virtual System.Void GetRuntimeCrossFadeDuration(System.Double& duration, System.Double& jumpTargetTime) { }
        // RVA: 0x0B34B7CC  token: 0x600031D
        public virtual System.Void RecalculateRuntimeCrossFadeMixIn(System.Double mixInTime, System.Double mixInDuration) { }
        // RVA: 0x0B34B81C  token: 0x600031E
        public virtual System.Void RecalculateRuntimeCrossFadeMixOut(System.Double curTime, System.Double mixStartTime, System.Double mixDuration) { }
        // RVA: 0x0B34B8E8  token: 0x600031F
        public System.Boolean TryGetJumpClip(Beyond.Gameplay.Core.RuntimeJumpClip& jumpClip) { }
        // RVA: 0x039F66D0  token: 0x6000320
        public virtual System.Void RuntimeReset() { }

    }

    // TypeToken: 0x2000079  // size: 0x20
    public abstract class RuntimeClipBase : UnityEngine.Timeline.RuntimeElement
    {
        // Fields
        private System.Int32 <optionIndex>k__BackingField;  // 0x18

        // Properties
        System.Double start { get; /* RVA: -1  // abstract */ }
        System.Double duration { get; /* RVA: -1  // abstract */ }
        System.Double end { get; /* RVA: -1  // abstract */ }
        System.Int32 optionIndex { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Int64 intervalStart { get; /* RVA: 0x0B34B380 */ }
        System.Int64 intervalEnd { get; /* RVA: 0x0B34B308 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000328
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200007A  // size: 0x18
    public abstract class RuntimeElement : UnityEngine.Timeline.IInterval, UnityEngine.Timeline.IOption, UnityEngine.Timeline.IRuntimeReset
    {
        // Fields
        private System.Int32 <intervalBit>k__BackingField;  // 0x10
        private System.Boolean <isLooping>k__BackingField;  // 0x14

        // Properties
        System.Int64 intervalStart { get; /* RVA: -1  // abstract */ }
        System.Int64 intervalEnd { get; /* RVA: -1  // abstract */ }
        System.Int32 optionIndex { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Int32 intervalBit { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        System.Boolean isLooping { get; /* RVA: 0x04D8EE50 */ set; /* RVA: 0x04D8EE70 */ }
        System.Boolean enable { set; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000332
        public virtual System.Void EvaluateAt(System.Double localTime, UnityEngine.Playables.FrameData frameData, UnityEngine.Timeline.ITimelineRootMarker timelineRoot) { }
        // RVA: -1  // abstract  token: 0x6000333
        public virtual System.Void DisableAt(System.Double localTime, System.Double rootDuration, UnityEngine.Playables.FrameData frameData) { }
        // RVA: 0x04DBA6E0  token: 0x6000334
        public virtual System.Boolean CheckIfRuntimeTimeLoop(System.Boolean& canSkip, System.Double& reverseTime) { }
        // RVA: 0x04DBA6D0  token: 0x6000335
        public virtual System.Boolean CheckIfRuntimeTimeJump(System.Double& jumpTime, System.Boolean& isReverseJump) { }
        // RVA: 0x041E1670  token: 0x6000336
        public virtual System.Void RuntimeLoopOver() { }
        // RVA: 0x041E1670  token: 0x6000337
        public virtual System.Void RuntimeLoopOn() { }
        // RVA: 0x04DBA6F0  token: 0x6000338
        public virtual System.Void GetRuntimeCrossFadeDuration(System.Double& duration, System.Double& jumpTargetTime) { }
        // RVA: 0x041E1670  token: 0x6000339
        public virtual System.Void RecalculateRuntimeCrossFadeMixIn(System.Double mixInTime, System.Double mixDuration) { }
        // RVA: 0x041E1670  token: 0x600033A
        public virtual System.Void RecalculateRuntimeCrossFadeMixOut(System.Double curTime, System.Double mixStartTime, System.Double mixDuration) { }
        // RVA: 0x041E1670  token: 0x600033B
        public virtual System.Void RuntimeReset() { }
        // RVA: 0x041E1670  token: 0x600033C
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200007B  // size: 0x60
    public class ScheduleRuntimeClip : UnityEngine.Timeline.RuntimeClipBase
    {
        // Fields
        private UnityEngine.Timeline.TimelineClip m_Clip;  // 0x20
        private UnityEngine.Playables.Playable m_Playable;  // 0x28
        private UnityEngine.Playables.Playable m_ParentMixer;  // 0x38
        private System.Double m_StartDelay;  // 0x48
        private System.Double m_FinishTail;  // 0x50
        private System.Boolean m_Started;  // 0x58

        // Properties
        System.Double start { get; /* RVA: 0x0B34BE20 */ }
        System.Double duration { get; /* RVA: 0x0B34BD9C */ }
        System.Double end { get; /* RVA: 0x0B34BDE4 */ }
        UnityEngine.Timeline.TimelineClip clip { get; /* RVA: 0x04D862C0 */ }
        UnityEngine.Playables.Playable mixer { get; /* RVA: 0x04D8FD20 */ }
        UnityEngine.Playables.Playable playable { get; /* RVA: 0x04D8FAD0 */ }
        System.Boolean enable { set; /* RVA: 0x0B34BE98 */ }

        // Methods
        // RVA: 0x0B34BD4C  token: 0x6000343
        public System.Void .ctor(UnityEngine.Timeline.TimelineClip clip, UnityEngine.Playables.Playable clipPlayable, UnityEngine.Playables.Playable parentMixer, System.Double startDelay, System.Double finishTail) { }
        // RVA: 0x0B34BA08  token: 0x6000344
        private System.Void Create(UnityEngine.Timeline.TimelineClip clip, UnityEngine.Playables.Playable clipPlayable, UnityEngine.Playables.Playable parentMixer, System.Double startDelay, System.Double finishTail) { }
        // RVA: 0x0B34BAA0  token: 0x6000346
        public virtual System.Void EvaluateAt(System.Double localTime, UnityEngine.Playables.FrameData frameData, UnityEngine.Timeline.ITimelineRootMarker timelineRoot) { }
        // RVA: 0x069734E8  token: 0x6000347
        public virtual System.Void DisableAt(System.Double localTime, System.Double rootDuration, UnityEngine.Playables.FrameData frameData) { }

    }

    // TypeToken: 0x200007C
    public interface IMarker
    {
        // Properties
        System.Double time { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        UnityEngine.Timeline.TrackAsset parent { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x600034B
        public virtual System.Void Initialize(UnityEngine.Timeline.TrackAsset parent) { }

    }

    // TypeToken: 0x200007D
    public interface INotificationOptionProvider
    {
        // Properties
        UnityEngine.Timeline.NotificationFlags flags { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x200007E  // size: 0x28
    public abstract class Marker : UnityEngine.ScriptableObject, UnityEngine.Timeline.IMarker
    {
        // Fields
        private System.Double m_Time;  // 0x18
        private UnityEngine.Timeline.TrackAsset <parent>k__BackingField;  // 0x20

        // Properties
        UnityEngine.Timeline.TrackAsset parent { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Double time { get; /* RVA: 0x04D86360 */ set; /* RVA: 0x0B349E4C */ }

        // Methods
        // RVA: 0x0B349D2C  token: 0x6000351
        private virtual System.Void UnityEngine.Timeline.IMarker.Initialize(UnityEngine.Timeline.TrackAsset parentTrack) { }
        // RVA: 0x041E1670  token: 0x6000352
        public virtual System.Void OnInitialize(UnityEngine.Timeline.TrackAsset aPent) { }
        // RVA: 0x03573370  token: 0x6000353
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200007F  // size: 0x28
    public sealed struct MarkerList : UnityEngine.ISerializationCallbackReceiver
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.ScriptableObject> m_Objects;  // 0x10
        private System.Collections.Generic.List<UnityEngine.Timeline.IMarker> m_Cache;  // 0x18
        private System.Boolean m_CacheDirty;  // 0x20
        private System.Boolean m_HasNotifications;  // 0x21

        // Properties
        System.Collections.Generic.List<UnityEngine.Timeline.IMarker> markers { get; /* RVA: 0x05394794 */ }
        System.Int32 Count { get; /* RVA: 0x03AC4070 */ }
        UnityEngine.Timeline.IMarker Item { get; /* RVA: 0x0B349B28 */ }

        // Methods
        // RVA: 0x039F3AA0  token: 0x6000355
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: 0x0B349644  token: 0x6000356
        public System.Void Add(UnityEngine.ScriptableObject item) { }
        // RVA: 0x0B349974  token: 0x6000357
        public System.Boolean Remove(UnityEngine.Timeline.IMarker item) { }
        // RVA: 0x0B349A78  token: 0x6000358
        public System.Boolean Remove(UnityEngine.ScriptableObject item, UnityEngine.Timeline.TimelineAsset timelineAsset, UnityEngine.Playables.PlayableAsset thingToDirty) { }
        // RVA: 0x0B3496C8  token: 0x6000359
        public System.Void Clear() { }
        // RVA: 0x05394794  token: 0x600035A
        public System.Collections.Generic.IEnumerable<UnityEngine.Timeline.IMarker> GetMarkers() { }
        // RVA: 0x02B76770  token: 0x600035D
        public System.Collections.Generic.List<UnityEngine.ScriptableObject> GetRawMarkerList() { }
        // RVA: 0x0B349714  token: 0x600035E
        public UnityEngine.Timeline.IMarker CreateMarker(System.Type type, System.Double time, UnityEngine.Timeline.TrackAsset owner) { }
        // RVA: 0x05394778  token: 0x600035F
        public System.Boolean HasNotifications() { }
        // RVA: 0x041E1670  token: 0x6000360
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize() { }
        // RVA: 0x015EFCD0  token: 0x6000361
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize() { }
        // RVA: 0x039F3B80  token: 0x6000362
        private System.Void BuildCache() { }

    }

    // TypeToken: 0x2000080  // size: 0xC0
    public class MarkerTrack : UnityEngine.Timeline.TrackAsset, Events.INotificationTrack
    {
        // Properties
        System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding> outputs { get; /* RVA: 0x0B349BD0 */ }

        // Methods
        // RVA: 0x0B349B84  token: 0x6000364
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000081  // size: 0x10
    public class CustomSignalEventDrawer : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000365
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000082  // size: 0x18
    public class SignalAsset : UnityEngine.ScriptableObject
    {
        // Fields
        private static System.Action<UnityEngine.Timeline.SignalAsset> OnEnableCallback;  // static @ 0x0

        // Events
        event System.Action<UnityEngine.Timeline.SignalAsset> OnEnableCallback;

        // Methods
        // RVA: 0x0B34C088  token: 0x6000368
        private System.Void OnEnable() { }
        // RVA: 0x03573370  token: 0x6000369
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000083  // size: 0x38
    public class SignalEmitter : UnityEngine.Timeline.Marker, UnityEngine.Playables.INotification, UnityEngine.Timeline.INotificationOptionProvider
    {
        // Fields
        private System.Boolean m_Retroactive;  // 0x28
        private System.Boolean m_EmitOnce;  // 0x29
        private UnityEngine.Timeline.SignalAsset m_Asset;  // 0x30

        // Properties
        System.Boolean retroactive { get; /* RVA: 0x011F33C0 */ set; /* RVA: 0x011F33D0 */ }
        System.Boolean emitOnce { get; /* RVA: 0x04D867B0 */ set; /* RVA: 0x04D867C0 */ }
        UnityEngine.Timeline.SignalAsset asset { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        UnityEngine.PropertyName UnityEngine.Playables.INotification.id { get; /* RVA: 0x0B34C210 */ }
        UnityEngine.Timeline.NotificationFlags UnityEngine.Timeline.INotificationOptionProvider.flags { get; /* RVA: 0x0B34C2B4 */ }

        // Methods
        // RVA: 0x03573370  token: 0x6000372
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000084  // size: 0x20
    public class SignalReceiver : UnityEngine.MonoBehaviour, UnityEngine.Playables.INotificationReceiver
    {
        // Fields
        private UnityEngine.Timeline.SignalReceiver.EventKeyValue m_Events;  // 0x18

        // Methods
        // RVA: 0x0B34C9D0  token: 0x6000373
        public virtual System.Void OnNotify(UnityEngine.Playables.Playable origin, UnityEngine.Playables.INotification notification, System.Object context) { }
        // RVA: 0x0B34C348  token: 0x6000374
        public System.Void AddReaction(UnityEngine.Timeline.SignalAsset asset, UnityEngine.Events.UnityEvent reaction) { }
        // RVA: 0x0B34C2DC  token: 0x6000375
        public System.Int32 AddEmptyReaction(UnityEngine.Events.UnityEvent reaction) { }
        // RVA: 0x0B34CB58  token: 0x6000376
        public System.Void Remove(UnityEngine.Timeline.SignalAsset asset) { }
        // RVA: 0x0A8DA4D0  token: 0x6000377
        public System.Collections.Generic.IEnumerable<UnityEngine.Timeline.SignalAsset> GetRegisteredSignals() { }
        // RVA: 0x0B34C8DC  token: 0x6000378
        public UnityEngine.Events.UnityEvent GetReaction(UnityEngine.Timeline.SignalAsset key) { }
        // RVA: 0x0B34C7D8  token: 0x6000379
        public System.Int32 Count() { }
        // RVA: 0x0B34C570  token: 0x600037A
        public System.Void ChangeSignalAtIndex(System.Int32 idx, UnityEngine.Timeline.SignalAsset newKey) { }
        // RVA: 0x0B34CAB0  token: 0x600037B
        public System.Void RemoveAtIndex(System.Int32 idx) { }
        // RVA: 0x0B34C4A4  token: 0x600037C
        public System.Void ChangeReactionAtIndex(System.Int32 idx, UnityEngine.Events.UnityEvent reaction) { }
        // RVA: 0x0B34C820  token: 0x600037D
        public UnityEngine.Events.UnityEvent GetReactionAtIndex(System.Int32 idx) { }
        // RVA: 0x0B34C914  token: 0x600037E
        public UnityEngine.Timeline.SignalAsset GetSignalAssetAtIndex(System.Int32 idx) { }
        // RVA: 0x041E1670  token: 0x600037F
        private System.Void OnEnable() { }
        // RVA: 0x0B34CC20  token: 0x6000380
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000086  // size: 0xC0
    public class SignalTrack : UnityEngine.Timeline.MarkerTrack
    {
        // Methods
        // RVA: 0x0B34CC90  token: 0x6000388
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000087  // size: 0x10
    public static class TrackAssetExtensions
    {
    }

    // TypeToken: 0x2000088  // size: 0xC8
    public class GroupTrack : UnityEngine.Timeline.TrackAsset
    {
        // Fields
        public Beyond.PackageBridge.Timeline.TimelineConditionBase enableOnCondition;  // 0xc0

        // Properties
        System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding> outputs { get; /* RVA: 0x0B3493F8 */ }

        // Methods
        // RVA: 0x04D35910  token: 0x6000389
        public System.Void CalculateRuntimeMuted() { }
        // RVA: 0x012081B0  token: 0x600038A
        private virtual System.Boolean CanCompileClips() { }
        // RVA: 0x04D5DEB0  token: 0x600038C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000089
    public interface ILayerable
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600038D
        public virtual UnityEngine.Playables.Playable CreateLayerMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount) { }

    }

    // TypeToken: 0x200008A
    public interface ITimeDilationListener
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600038E
        public virtual System.Void OnTimeDilationChanged(System.Single timeDilation) { }

    }

    // TypeToken: 0x200008B  // size: 0x20
    public class ActivationControlPlayable : UnityEngine.Timeline.ControlPlayableBehaviour
    {
        // Fields
        public UnityEngine.GameObject gameObject;  // 0x10
        public UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState postPlayback;  // 0x18
        private UnityEngine.Timeline.ActivationControlPlayable.InitialState m_InitialState;  // 0x1c

        // Methods
        // RVA: 0x0450F4F0  token: 0x600038F
        public static UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.ActivationControlPlayable> Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject gameObject, UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState postPlaybackState) { }
        // RVA: 0x04A052D0  token: 0x6000390
        public virtual System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B3465FC  token: 0x6000391
        public virtual System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x04A05220  token: 0x6000392
        public virtual System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object userData) { }
        // RVA: 0x04972880  token: 0x6000393
        public virtual System.Void OnGraphStart(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x0B346688  token: 0x6000394
        public virtual System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x041E1670  token: 0x6000395
        public virtual System.Void RuntimeReset() { }
        // RVA: 0x04D8D750  token: 0x6000396
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008E  // size: 0x40
    public class AnimationEventRuntimeContextPlayable : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        private UnityEngine.Timeline.ITimelineAnimationEventContextBinding m_TimelineAnimationEventContextBinding;  // 0x10
        private UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode m_PostAudioEventTriggerMode;  // 0x18
        private UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode m_CustomFootStepTriggerMode;  // 0x1c
        private UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviour m_ClipEndAudioBehaviour;  // 0x20
        private UnityEngine.AnimationEvent[] m_CachedAnimationEvents;  // 0x28
        private System.Single m_ClipDuration;  // 0x30
        private System.Single m_PostAudioEventTriggerThreshold;  // 0x34
        private System.Single m_CustomFootStepTriggerThreshold;  // 0x38
        private System.Boolean m_IsRegistered;  // 0x3c
        private System.Boolean m_HasRegisteredOnce;  // 0x3d

        // Methods
        // RVA: 0x0B348790  token: 0x6000397
        public static UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.AnimationEventRuntimeContextPlayable> Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Timeline.ITimelineAnimationEventContextBinding timelineAnimationEventContextBinding, UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode postAudioEventTriggerMode, System.Single postAudioEventTriggerThreshold, UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode customFootStepTriggerMode, System.Single customFootStepTriggerThreshold, UnityEngine.AnimationEvent[] cachedAnimationEvents, System.Single clipDuration, UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviour clipEndAudioBehaviour, System.Int32 inputCount) { }
        // RVA: 0x0B348908  token: 0x6000398
        public System.Void Initialize(UnityEngine.Timeline.ITimelineAnimationEventContextBinding timelineAnimationEventContextBinding, UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode postAudioEventTriggerMode, System.Single postAudioEventTriggerThreshold, UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode customFootStepTriggerMode, System.Single customFootStepTriggerThreshold, UnityEngine.AnimationEvent[] cachedAnimationEvents, System.Single clipDuration, UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviour clipEndAudioBehaviour) { }
        // RVA: 0x0B348B18  token: 0x6000399
        public System.Void ResetRegistration(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x0B348970  token: 0x600039A
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x041E1670  token: 0x600039B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008F  // size: 0x18
    public class BasicPlayableBehaviour : UnityEngine.ScriptableObject, UnityEngine.Playables.IPlayableAsset, UnityEngine.Playables.IPlayableBehaviour
    {
        // Properties
        System.Double duration { get; /* RVA: 0x0B348CA8 */ }
        System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding> outputs { get; /* RVA: 0x0B348CF8 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600039E
        public virtual System.Void OnGraphStart(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x041E1670  token: 0x600039F
        public virtual System.Void OnGraphStop(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x041E1670  token: 0x60003A0
        public virtual System.Void OnPlayableCreate(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x041E1670  token: 0x60003A1
        public virtual System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x041E1670  token: 0x60003A2
        public virtual System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x041E1670  token: 0x60003A3
        public virtual System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x041E1670  token: 0x60003A4
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x041E1670  token: 0x60003A5
        public virtual System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData) { }
        // RVA: 0x0B348BE8  token: 0x60003A6
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner) { }
        // RVA: 0x03573370  token: 0x60003A7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000090  // size: 0x10
    public class ControlPlayableBehaviour : UnityEngine.Playables.PlayableBehaviour, UnityEngine.Timeline.IBehaviourRuntimeReset
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60003A8
        public virtual System.Void RuntimeReset() { }
        // RVA: 0x041E1670  token: 0x60003A9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000091  // size: 0x38
    public class DirectorControlPlayable : UnityEngine.Timeline.ControlPlayableBehaviour
    {
        // Fields
        public UnityEngine.Playables.PlayableDirector director;  // 0x10
        public UnityEngine.Timeline.ITimelineRootMarker rootMarker;  // 0x18
        private static System.Single SYNC_START_FORBID_THRESHOLD;  // const
        private System.Boolean m_SyncTime;  // 0x20
        private System.Double m_AssetDuration;  // 0x28
        private UnityEngine.Timeline.TimelinePlayable m_TimelinePlayable;  // 0x30

        // Methods
        // RVA: 0x039F7760  token: 0x60003AA
        public static UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.DirectorControlPlayable> Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableDirector director) { }
        // RVA: 0x0B348F24  token: 0x60003AB
        public virtual System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x03E0D6A0  token: 0x60003AC
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x039EE7D0  token: 0x60003AD
        public virtual System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B348E6C  token: 0x60003AE
        public virtual System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x03E0D8F0  token: 0x60003AF
        public virtual System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData) { }
        // RVA: 0x03E0E900  token: 0x60003B0
        private System.Void SyncSpeed(System.Double speed) { }
        // RVA: 0x03E0D590  token: 0x60003B1
        private System.Void SyncStart(UnityEngine.Playables.PlayableGraph graph, System.Double time) { }
        // RVA: 0x03E0D610  token: 0x60003B2
        private System.Void SyncStop(UnityEngine.Playables.PlayableGraph graph, System.Double time) { }
        // RVA: 0x041E1670  token: 0x60003B3
        public virtual System.Void RuntimeReset() { }
        // RVA: 0x04D77940  token: 0x60003B4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000092
    public interface ITimeControl
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60003B5
        public virtual System.Void SetTime(System.Double time) { }
        // RVA: -1  // abstract  token: 0x60003B6
        public virtual System.Void OnControlTimeStart() { }
        // RVA: -1  // abstract  token: 0x60003B7
        public virtual System.Void OnControlTimeStop() { }

    }

    // TypeToken: 0x2000093  // size: 0x12
    public sealed struct NotificationFlags
    {
        // Fields
        public System.Int16 value__;  // 0x10
        public static UnityEngine.Timeline.NotificationFlags TriggerInEditMode;  // const
        public static UnityEngine.Timeline.NotificationFlags Retroactive;  // const
        public static UnityEngine.Timeline.NotificationFlags TriggerOnce;  // const

    }

    // TypeToken: 0x2000094  // size: 0x40
    public class ParticleControlPlayable : UnityEngine.Timeline.ControlPlayableBehaviour, UnityEngine.Timeline.ITimeDilationListener
    {
        // Fields
        private System.Single m_LastPlayableTime;  // 0x10
        private System.Single m_LastParticleTime;  // 0x14
        private System.UInt32 m_RandomSeed;  // 0x18
        private System.Boolean m_forceRuntimeSimulate;  // 0x1c
        private System.Single m_clipIn;  // 0x20
        private System.Boolean m_hadInitialized;  // 0x24
        private System.Single originalSimulateSpeed;  // 0x28
        private System.Single simulateSpeedModifier;  // 0x2c
        private UnityEngine.ParticleSystem <particleSystem>k__BackingField;  // 0x30
        private System.Boolean m_hadIgnite;  // 0x38
        private System.Boolean m_hadSimulate;  // 0x39
        private System.Boolean m_hadPlayed;  // 0x3a

        // Properties
        UnityEngine.ParticleSystem particleSystem { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        System.Boolean shouldSimulate { get; /* RVA: 0x0B34A95C */ }

        // Methods
        // RVA: 0x0B349EB4  token: 0x60003B8
        public static UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.ParticleControlPlayable> Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.ParticleSystem component, System.UInt32 randomSeed, System.Single clipIn, System.Single speedMultiplier, System.Boolean forceRuntimeSimulate) { }
        // RVA: 0x0B34A048  token: 0x60003BC
        public System.Void Initialize(UnityEngine.ParticleSystem ps, System.UInt32 randomSeed, System.Single clipIn, System.Single simulateSpeed, System.Boolean forceRuntimeSimulate) { }
        // RVA: 0x0B34A3C4  token: 0x60003BD
        private System.Void SetOriginalSimulateSpeed(UnityEngine.ParticleSystem particleSystem, System.Double simulateSpeed) { }
        // RVA: 0x0B34A4FC  token: 0x60003BE
        private static System.Void SetRandomSeed(UnityEngine.ParticleSystem particleSystem, System.UInt32 randomSeed) { }
        // RVA: 0x0B34A220  token: 0x60003BF
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData data) { }
        // RVA: 0x0B34A8D0  token: 0x60003C0
        private System.Void _SimulateParticle(System.Single time, System.Single particleTime, System.Boolean previewMode) { }
        // RVA: 0x04DBA680  token: 0x60003C1
        public virtual System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B34A7B8  token: 0x60003C2
        private System.Void _PlayOrQueuedParticleSystem() { }
        // RVA: 0x0B34A108  token: 0x60003C3
        public virtual System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B34A5F8  token: 0x60003C4
        private System.Void Simulate(System.Single time, System.Boolean restart) { }
        // RVA: 0x04DBA690  token: 0x60003C5
        public virtual System.Void RuntimeReset() { }
        // RVA: 0x0B34A1C4  token: 0x60003C6
        public virtual System.Void OnTimeDilationChanged(System.Single timeScale) { }
        // RVA: 0x04DBA6A0  token: 0x60003C7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000095  // size: 0x18
    public class PrefabControlPlayable : UnityEngine.Timeline.ControlPlayableBehaviour
    {
        // Fields
        private UnityEngine.GameObject m_Instance;  // 0x10

        // Properties
        UnityEngine.GameObject prefabInstance { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0B34ABD8  token: 0x60003C8
        public static UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.PrefabControlPlayable> Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject prefabGameObject, UnityEngine.Transform parentTransform) { }
        // RVA: 0x0B34AD08  token: 0x60003CA
        public UnityEngine.GameObject Initialize(UnityEngine.GameObject prefabGameObject, UnityEngine.Transform parentTransform) { }
        // RVA: 0x0B34B058  token: 0x60003CB
        public virtual System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x0B34AFE8  token: 0x60003CC
        public virtual System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B34AF5C  token: 0x60003CD
        public virtual System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B34B0E0  token: 0x60003CE
        private static System.Void SetHideFlagsRecursive(UnityEngine.GameObject gameObject) { }
        // RVA: 0x041E1670  token: 0x60003CF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000096  // size: 0x20
    public class TimeControlPlayable : UnityEngine.Timeline.ControlPlayableBehaviour
    {
        // Fields
        private UnityEngine.Timeline.ITimeControl m_timeControl;  // 0x10
        private System.Boolean m_started;  // 0x18

        // Methods
        // RVA: 0x0B34CC98  token: 0x60003D0
        public static UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.TimeControlPlayable> Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Timeline.ITimeControl timeControl) { }
        // RVA: 0x053908C0  token: 0x60003D1
        public System.Void Initialize(UnityEngine.Timeline.ITimeControl timeControl) { }
        // RVA: 0x0B34CE30  token: 0x60003D2
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B34CDDC  token: 0x60003D3
        public virtual System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B34CD88  token: 0x60003D4
        public virtual System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x041E1670  token: 0x60003D5
        public virtual System.Void RuntimeReset() { }
        // RVA: 0x041E1670  token: 0x60003D6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000097  // size: 0x38
    public class TimeNotificationBehaviour : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        private readonly System.Collections.Generic.List<UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry> m_Notifications;  // 0x10
        private System.Double m_PreviousTime;  // 0x18
        private System.Boolean m_NeedSortNotifications;  // 0x20
        private System.Boolean m_IsManualUpdate;  // 0x21
        private UnityEngine.Playables.Playable m_TimeSource;  // 0x28

        // Properties
        UnityEngine.Playables.Playable timeSource { set; /* RVA: 0x04D8FAE0 */ }
        System.Boolean isManualUpdate { set; /* RVA: 0x04D86860 */ }

        // Methods
        // RVA: 0x0B34CF8C  token: 0x60003D9
        public static UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.TimeNotificationBehaviour> Create(UnityEngine.Playables.PlayableGraph graph, System.Double duration, UnityEngine.Playables.DirectorWrapMode loopMode) { }
        // RVA: 0x0B34CEA8  token: 0x60003DA
        public System.Void AddNotification(System.Double time, UnityEngine.Playables.INotification payload, UnityEngine.Timeline.NotificationFlags flags) { }
        // RVA: 0x0B34D244  token: 0x60003DB
        public virtual System.Void OnGraphStart(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x0B34D09C  token: 0x60003DC
        public virtual System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B34D3E4  token: 0x60003DD
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B34D818  token: 0x60003DE
        private System.Void SortNotifications() { }
        // RVA: 0x0B34CF5C  token: 0x60003DF
        private static System.Boolean CanRestoreNotification(UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry e, UnityEngine.Playables.FrameData info, System.Double currentTime, System.Double previousTime) { }
        // RVA: 0x0B34DA20  token: 0x60003E0
        private System.Void TriggerNotificationsInRange(System.Double start, System.Double end, UnityEngine.Playables.FrameData info, UnityEngine.Playables.Playable playable, System.Boolean checkState) { }
        // RVA: 0x0B34D940  token: 0x60003E1
        private System.Void SyncDurationWithExternalSource(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x0B34DC00  token: 0x60003E2
        private static System.Void Trigger_internal(UnityEngine.Playables.Playable playable, UnityEngine.Playables.PlayableOutput output, UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry& e) { }
        // RVA: 0x04D8CCB0  token: 0x60003E3
        private static System.Void Restore_internal(UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry& e) { }
        // RVA: 0x0B34DC7C  token: 0x60003E4
        private static System.Double _GetPlayableTime(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x0B34DD40  token: 0x60003E5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009A
    public interface IAsyncScriptAnimationJobSyncInit
    {
        // Properties
        System.Boolean isAsyncInitPending { get; /* RVA: -1  // abstract */ }
        System.Boolean isAsyncBindStreamPropertiesComplete { get; /* RVA: -1  // abstract */ }
        System.Boolean isAsyncBuildNecessaryLayersComplete { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60003EF
        public virtual System.Boolean StepAsyncBindStreamProperties(System.Int32& bindCountBudget) { }
        // RVA: -1  // abstract  token: 0x60003F0
        public virtual System.Boolean StepAsyncBuildNecessaryLayers() { }
        // RVA: -1  // abstract  token: 0x60003F1
        public virtual System.Boolean StepAsyncFinishInit() { }
        // RVA: -1  // abstract  token: 0x60003F2
        public virtual System.Void ClearAsyncInitState() { }

    }

    // TypeToken: 0x200009B  // size: 0x30
    public class CommunicatorParam
    {
        // Fields
        public UnityEngine.Timeline.IScriptAnimationJobSync jobSync;  // 0x10
        public UnityEngine.Timeline.IAsyncScriptAnimationJobSyncInit asyncJobSyncInit;  // 0x18
        public System.String uniqueId;  // 0x20
        public System.Boolean lookAtUseAdditive;  // 0x28
        public System.Boolean useLookAtV2;  // 0x29
        public System.Boolean useAsyncJobSyncInit;  // 0x2a

        // Methods
        // RVA: 0x041E1670  token: 0x60003F3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009C
    public interface IAutoGenMono
    {
    }

    // TypeToken: 0x200009D
    public interface IBehaviourRuntimeReset
    {
    }

    // TypeToken: 0x200009E
    public interface IScriptAnimationJobSync : UnityEngine.Timeline.IAutoGenMono
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60003F4
        public virtual System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: -1  // abstract  token: 0x60003F5
        public virtual System.Void CalcLayerMainStream(System.Single deltaTime) { }
        // RVA: -1  // abstract  token: 0x60003F6
        public virtual System.Void ResetCloth(System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> activeRuntimeElements, System.Boolean useSoftReset, System.Boolean keepPose, System.Boolean useRelativeTeleport) { }

    }

    // TypeToken: 0x200009F  // size: 0x20
    public sealed struct TimelineScriptAnimationJob : UnityEngine.Animations.IAnimationJob
    {
        // Fields
        private static readonly Unity.Profiling.ProfilerMarker s_processRootMotionMarker;  // static @ 0x0
        public UnityEngine.Timeline.IScriptAnimationJobSync bindedScriptAnimationJobSync;  // 0x10
        public UnityEngine.Animator bindedAnimator;  // 0x18

        // Methods
        // RVA: 0x039CF630  token: 0x60003F7
        public System.Void .ctor(UnityEngine.Timeline.IScriptAnimationJobSync job, UnityEngine.Animator animator) { }
        // RVA: 0x041E1670  token: 0x60003F8
        public virtual System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x0B351F80  token: 0x60003F9
        public virtual System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream) { }
        // RVA: 0x02ED08A0  token: 0x60003FA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A0  // size: 0xC0
    public class PlayableTrack : UnityEngine.Timeline.TrackAsset
    {
        // Methods
        // RVA: 0x0B34AAF4  token: 0x60003FB
        protected virtual System.Void OnCreateClip(UnityEngine.Timeline.TimelineClip clip) { }
        // RVA: 0x0B34AB8C  token: 0x60003FC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A1  // size: 0x10
    public class TrackMediaType : System.Attribute
    {
    }

    // TypeToken: 0x20000A2  // size: 0x20
    public class TrackClipTypeAttribute : System.Attribute
    {
        // Fields
        public readonly System.Type inspectedType;  // 0x10
        public readonly System.Boolean allowAutoCreate;  // 0x18

        // Methods
        // RVA: 0x04774A10  token: 0x60003FD
        public System.Void .ctor(System.Type clipClass) { }
        // RVA: 0x04D831B0  token: 0x60003FE
        public System.Void .ctor(System.Type clipClass, System.Boolean allowAutoCreate) { }

    }

    // TypeToken: 0x20000A3  // size: 0x10
    public class NotKeyableAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60003FF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A4  // size: 0x14
    public sealed struct TrackBindingFlags
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.TrackBindingFlags None;  // const
        public static UnityEngine.Timeline.TrackBindingFlags AllowCreateComponent;  // const
        public static UnityEngine.Timeline.TrackBindingFlags All;  // const

    }

    // TypeToken: 0x20000A5  // size: 0x20
    public class TrackBindingTypeAttribute : System.Attribute
    {
        // Fields
        public readonly System.Type type;  // 0x10
        public readonly UnityEngine.Timeline.TrackBindingFlags flags;  // 0x18

        // Methods
        // RVA: 0x04824300  token: 0x6000400
        public System.Void .ctor(System.Type type) { }
        // RVA: 0x03B46FF0  token: 0x6000401
        public System.Void .ctor(System.Type type, UnityEngine.Timeline.TrackBindingFlags flags) { }

    }

    // TypeToken: 0x20000A6  // size: 0x20
    public class SupportsChildTracksAttribute : System.Attribute
    {
        // Fields
        public readonly System.Type childType;  // 0x10
        public readonly System.Int32 levels;  // 0x18

        // Methods
        // RVA: 0x03B46FF0  token: 0x6000402
        public System.Void .ctor(System.Type childType, System.Int32 levels) { }

    }

    // TypeToken: 0x20000A7  // size: 0x10
    public class IgnoreOnPlayableTrackAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000403
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A8  // size: 0x18
    public class TimeFieldAttribute : UnityEngine.PropertyAttribute
    {
        // Fields
        private readonly UnityEngine.Timeline.TimeFieldAttribute.UseEditMode <useEditMode>k__BackingField;  // 0x10

        // Methods
        // RVA: 0x014F51F0  token: 0x6000404
        public System.Void .ctor(UnityEngine.Timeline.TimeFieldAttribute.UseEditMode useEditMode) { }

    }

    // TypeToken: 0x20000AA  // size: 0x10
    public class FrameRateFieldAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000405
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AB  // size: 0x10
    public class HideInMenuAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000406
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AC  // size: 0x18
    public class CustomStyleAttribute : System.Attribute
    {
        // Fields
        public readonly System.String ussStyle;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000407
        public System.Void .ctor(System.String ussStyle) { }

    }

    // TypeToken: 0x20000AD  // size: 0x10
    public class MenuCategoryAttribute : System.Attribute
    {
    }

    // TypeToken: 0x20000AE
    public interface ITimelineClipAsset
    {
        // Properties
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x20000AF
    public interface ITimelineEvaluateCallback
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000409
        public virtual System.Void Evaluate() { }

    }

    // TypeToken: 0x20000B0  // size: 0x108
    public class TimelinePlayable : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        public System.Double timeOffset;  // 0x10
        public System.Int32 newOptionIndex;  // 0x18
        public System.Int32 curOptionIndex;  // 0x1c
        public System.Int32 lastOptionIndex;  // 0x20
        public System.Boolean checkTimeJumpForNextSeek;  // 0x24
        private UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> m_IntervalTree;  // 0x28
        private System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> m_ActiveClips;  // 0x30
        private System.Collections.Generic.HashSet<UnityEngine.Timeline.RuntimeElement> m_ActiveClipsSet;  // 0x38
        private System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> m_CurrentListOfActiveClips;  // 0x40
        private System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> m_ActiveClipsCalcCache;  // 0x48
        private System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> m_RuntimeResetClipCache;  // 0x50
        private System.Collections.Generic.List<UnityEngine.Timeline.ITimeDilationListener> m_timeDialationListeners;  // 0x58
        private System.Int32 m_ActiveBit;  // 0x60
        private System.Boolean m_needEvaluateTwice;  // 0x64
        private System.Boolean m_isTimelineEnded;  // 0x65
        private System.Collections.Generic.List<UnityEngine.Timeline.ITimelineEvaluateCallback> m_EvaluateCallbacks;  // 0x68
        private System.Collections.Generic.Dictionary<UnityEngine.Timeline.TrackAsset,UnityEngine.Playables.Playable> m_PlayableCache;  // 0x70
        private UnityEngine.Playables.PlayableDirector m_TopDirector;  // 0x78
        private System.Collections.Generic.List<UnityEngine.Playables.PlayableDirector> m_AllDirectorUnderRoot;  // 0x80
        private UnityEngine.Playables.PlayableDirector m_DirectorHoldingThisPlayable;  // 0x88
        private UnityEngine.Timeline.ITimelineRootMarker m_RootMarker;  // 0x90
        private System.Collections.Generic.List<UnityEngine.Timeline.IScriptAnimationJobSync> m_ScriptAnimationJobSyncs;  // 0x98
        private System.Collections.Generic.HashSet<UnityEngine.Playables.Playable> m_forbidPostProcessMixers;  // 0xa0
        private System.Boolean m_isAsyncCompileEnabled;  // 0xa8
        private System.Boolean m_isAsyncCompileComplete;  // 0xa9
        private System.Boolean m_isAsyncCompileFailed;  // 0xaa
        private UnityEngine.Playables.PlayableGraph m_AsyncCompileGraph;  // 0xb0
        private UnityEngine.Playables.Playable m_AsyncTimelinePlayable;  // 0xc0
        private System.Collections.Generic.List<UnityEngine.Timeline.TrackAsset> m_AsyncOutputTrackList;  // 0xd0
        private UnityEngine.GameObject m_AsyncCompileGo;  // 0xd8
        private System.Boolean m_AsyncAutoRebalance;  // 0xe0
        private System.Boolean m_AsyncCreateOutputs;  // 0xe1
        private System.Int32 m_AsyncOutputTrackIndex;  // 0xe4
        private System.Boolean m_AsyncCombineToTopCompiled;  // 0xe8
        private UnityEngine.Playables.PlayableOutput m_AsyncCompileDummyOutput;  // 0xf0
        private static readonly Unity.Profiling.ProfilerMarker s_compileSingleTrackMarker;  // static @ 0x0
        private static readonly Unity.Profiling.ProfilerMarker s_prepareFrameMarker;  // static @ 0x8
        private static readonly Unity.Profiling.ProfilerMarker s_evaluateMarker;  // static @ 0x10
        private static readonly Unity.Profiling.ProfilerMarker s_queryActiveClipsMarker;  // static @ 0x18
        private static readonly Unity.Profiling.ProfilerMarker s_filterActiveClipsMarker;  // static @ 0x20
        private static readonly Unity.Profiling.ProfilerMarker s_disableInactiveClipsMarker;  // static @ 0x28
        private static readonly Unity.Profiling.ProfilerMarker s_evaluateActiveClipsMarker;  // static @ 0x30
        private static readonly Unity.Profiling.ProfilerMarker s_evaluateCallbacksMarker;  // static @ 0x38
        private static readonly Unity.Profiling.ProfilerMarker s_resetScriptAnimationJobSyncsMarker;  // static @ 0x40
        private static readonly Unity.Profiling.ProfilerMarker s_evaluateTwiceMarker;  // static @ 0x48
        private static System.Boolean muteAudioScrubbing;  // static @ 0x50
        private System.Double m_timeForNextFrame;  // 0x100

        // Properties
        System.Double timeForNextFrame { get; /* RVA: 0x04D925A0 */ }
        UnityEngine.Timeline.ITimelineRootMarker timelineRootMarker { get; /* RVA: 0x04D86220 */ }
        System.Boolean isAsyncCompileComplete { get; /* RVA: 0x02ED0880 */ }
        System.Boolean isAsyncCompileFailed { get; /* RVA: 0x02ED0890 */ }
        System.Boolean enableLoop { get; /* RVA: 0x0B34F3AC */ }

        // Methods
        // RVA: 0x03FC6800  token: 0x600040F
        public static UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.TimelinePlayable> Create(UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> tracks, UnityEngine.GameObject go, System.Boolean autoRebalance, System.Boolean createOutputs) { }
        // RVA: 0x03FC7E20  token: 0x6000410
        public System.Void Compile(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> tracks, UnityEngine.GameObject go, System.Boolean autoRebalance, System.Boolean createOutputs) { }
        // RVA: 0x02EC7ED0  token: 0x6000411
        public System.Void BeginAsyncCompile(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> tracks, UnityEngine.GameObject go, System.Boolean autoRebalance, System.Boolean createOutputs) { }
        // RVA: 0x02ECFD60  token: 0x6000412
        public System.Boolean StepAsyncCompile(System.Int32 outputGroupBudget, System.Boolean isInBurstMode) { }
        // RVA: 0x02ECCBB0  token: 0x6000413
        private System.Void InitializeCompileContext(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> tracks, UnityEngine.GameObject go, System.Boolean autoRebalance, System.Boolean createOutputs) { }
        // RVA: 0x041E1670  token: 0x6000414
        private System.Void FinalizeCompile() { }
        // RVA: 0x02EC9CD0  token: 0x6000415
        private System.Void CreateAsyncCompileDummyOutput(UnityEngine.Playables.PlayableGraph graph) { }
        // RVA: 0x02EC7F90  token: 0x6000416
        private System.Void ClearAsyncCompileDummyOutput() { }
        // RVA: 0x04DBA700  token: 0x6000417
        public System.Void MarkTimelineEnd() { }
        // RVA: 0x04DAA1E0  token: 0x6000418
        public System.Boolean IsTimelineEnded() { }
        // RVA: 0x039EF290  token: 0x6000419
        private System.Void CompileCombineTopTopTrackList(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, System.Boolean createOutputs) { }
        // RVA: 0x039F4EB0  token: 0x600041A
        private static System.Void CollectCombineToTopTracksRecursive(UnityEngine.Timeline.TrackAsset track, System.Collections.Generic.List<UnityEngine.Timeline.TrackAsset> combineTracks) { }
        // RVA: 0x039F5080  token: 0x600041B
        private System.Void CompileTrackList(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> tracks, UnityEngine.GameObject go, System.Boolean createOutputs, System.Boolean isCompilingCombineToTopTracks) { }
        // RVA: 0x02EC86D0  token: 0x600041C
        private System.Boolean CompileSingleTrack(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, UnityEngine.Timeline.TrackAsset track, UnityEngine.GameObject go, System.Boolean createOutputs, System.Boolean isCompilingCombineToTopTracks) { }
        // RVA: 0x02ECF130  token: 0x600041D
        private static System.Boolean IsAsyncCompileFrameBarrierTrack(UnityEngine.Timeline.TrackAsset track) { }
        // RVA: 0x03FC97E0  token: 0x600041E
        private System.Void CreateTrackOutput(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Timeline.TrackAsset track, UnityEngine.GameObject go, UnityEngine.Playables.Playable playable, System.Int32 port) { }
        // RVA: 0x03E11710  token: 0x600041F
        private System.Void EvaluateWeightsForAnimationPlayableOutput(UnityEngine.Timeline.TrackAsset track, UnityEngine.Animations.AnimationPlayableOutput animOutput, UnityEngine.Animator animator) { }
        // RVA: 0x03433E70  token: 0x6000420
        private UnityEngine.Playables.Playable CreateTrackPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, UnityEngine.Timeline.TrackAsset track, UnityEngine.GameObject go, System.Boolean createOutputs) { }
        // RVA: 0x0B34EEB4  token: 0x6000421
        public System.Void UpdateIntervalTree() { }
        // RVA: 0x0B34EC78  token: 0x6000422
        public System.Void RuntimeResetIntervalTree() { }
        // RVA: 0x02ECF440  token: 0x6000423
        public System.Boolean IsDynamicLinkAnimationOutput(UnityEngine.Playables.PlayableOutput output) { }
        // RVA: 0x02ECA7B0  token: 0x6000424
        private static System.Boolean HasDynamicLinkAnimationClip(UnityEngine.Timeline.TrackAsset track) { }
        // RVA: 0x03E0B9F0  token: 0x6000425
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x0B34F1A4  token: 0x6000426
        private System.Void _ResetScriptAnimationJobySyncs() { }
        // RVA: 0x0B34EEFC  token: 0x6000427
        private System.Void _EvaluateTwice() { }
        // RVA: 0x04D85A60  token: 0x6000428
        public System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> GetCurrentActiveClips() { }
        // RVA: 0x0489B450  token: 0x6000429
        public System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> GetActiveClipsAtGivenTime(System.Double givenTime, System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> activeClips) { }
        // RVA: 0x0B34EAC4  token: 0x600042A
        public System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> GetActiveClipsAtGivenTimeRange(System.Double startTime, System.Double endTime, System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> activeClips) { }
        // RVA: 0x039F7A20  token: 0x600042B
        protected virtual System.Void Evaluate(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData frameData) { }
        // RVA: 0x0B34E854  token: 0x600042C
        private System.Boolean DoReverseLoop(System.Double reverseTime) { }
        // RVA: 0x0B34E778  token: 0x600042D
        private System.Boolean DoReverseJump(UnityEngine.Timeline.RuntimeClip runtimeClip, System.Double jumpTime) { }
        // RVA: 0x0B34E6C0  token: 0x600042E
        private System.Boolean DoJump(UnityEngine.Timeline.RuntimeClip runtimeClip, System.Double localTime, System.Double jumpTime) { }
        // RVA: 0x039EC880  token: 0x600042F
        private System.Boolean _CheckIfTimeJumping(UnityEngine.Playables.FrameData frameData, System.Double localTime) { }
        // RVA: 0x049CB200  token: 0x6000430
        private System.Void CacheTrack(UnityEngine.Timeline.TrackAsset track, UnityEngine.Playables.Playable playable, System.Int32 port, UnityEngine.Playables.Playable parent) { }
        // RVA: 0x04D925E0  token: 0x6000431
        public System.Void SetTimeForNextFrame(System.Double newTime) { }
        // RVA: 0x0B34EC24  token: 0x6000432
        public System.Void RegisterTimeDilationListener(UnityEngine.Timeline.ITimeDilationListener listener) { }
        // RVA: 0x0B34EBC0  token: 0x6000433
        public System.Void MarkMixerForbidPostProcess(UnityEngine.Playables.Playable mixer) { }
        // RVA: 0x03E10BF0  token: 0x6000434
        public System.Boolean CheckIfMixerForbidPostProcess(UnityEngine.Playables.Playable mixer) { }
        // RVA: 0x0B34ECC0  token: 0x6000435
        public System.Void SetTimeScale(System.Single usingTimeScale) { }
        // RVA: 0x0B34F058  token: 0x6000436
        private System.Void _NotifyTimeScaleChanged(System.Single usingTimeScale) { }
        // RVA: 0x03FC8600  token: 0x6000437
        public System.Void .ctor() { }
        // RVA: 0x04D77960  token: 0x6000438
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B1  // size: 0x10
    public static class AnimationPreviewUtilities
    {
    }

    // TypeToken: 0x20000B2  // size: 0x10
    public static class Extrapolation
    {
        // Fields
        private static readonly System.Double kMinExtrapolationTime;  // static @ 0x0

        // Methods
        // RVA: 0x039F6420  token: 0x6000439
        private static System.Void CalculateExtrapolationTimes(UnityEngine.Timeline.TrackAsset asset) { }
        // RVA: 0x04B20B90  token: 0x600043A
        private static UnityEngine.Timeline.TimelineClip[] SortClipsByStartTime(UnityEngine.Timeline.TimelineClip[] clips) { }
        // RVA: 0x03B0C320  token: 0x600043B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B4  // size: 0x10
    public static class HashUtility
    {
        // Methods
        // RVA: 0x04DBA660  token: 0x600043F
        public static System.Int32 CombineHash(System.Int32 h1, System.Int32 h2) { }
        // RVA: 0x04DBA620  token: 0x6000440
        public static System.Int32 CombineHash(System.Int32 h1, System.Int32 h2, System.Int32 h3) { }
        // RVA: 0x03AC42A0  token: 0x6000441
        public static System.Int32 CombineHash(System.Int32 h1, System.Int32 h2, System.Int32 h3, System.Int32 h4) { }
        // RVA: 0x03AC4260  token: 0x6000442
        public static System.Int32 CombineHash(System.Int32 h1, System.Int32 h2, System.Int32 h3, System.Int32 h4, System.Int32 h5) { }
        // RVA: 0x03AC4200  token: 0x6000443
        public static System.Int32 CombineHash(System.Int32 h1, System.Int32 h2, System.Int32 h3, System.Int32 h4, System.Int32 h5, System.Int32 h6) { }

    }

    // TypeToken: 0x20000B5
    public interface IPropertyCollector
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000444
        public virtual System.Void PushActiveGameObject(UnityEngine.GameObject gameObject) { }
        // RVA: -1  // abstract  token: 0x6000445
        public virtual System.Void PopActiveGameObject() { }
        // RVA: -1  // abstract  token: 0x6000446
        public virtual System.Void AddFromClip(UnityEngine.AnimationClip clip) { }
        // RVA: -1  // abstract  token: 0x6000447
        public virtual System.Void AddFromName(System.String name) { }
        // RVA: -1  // abstract  token: 0x6000448
        public virtual System.Void AddFromName(UnityEngine.GameObject obj, System.String name) { }
        // RVA: -1  // abstract  token: 0x6000449
        public virtual System.Void AddFromName(UnityEngine.GameObject obj, System.String name) { }
        // RVA: -1  // abstract  token: 0x600044A
        public virtual System.Void AddFromComponent(UnityEngine.GameObject obj, UnityEngine.Component component) { }
        // RVA: -1  // abstract  token: 0x600044B
        public virtual System.Void AddObjectProperties(UnityEngine.Object obj, UnityEngine.AnimationClip clip) { }

    }

    // TypeToken: 0x20000B6
    public interface IPropertyPreview
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600044C
        public virtual System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver) { }

    }

    // TypeToken: 0x20000B7  // size: 0x10
    public static class NotificationUtilities
    {
        // Methods
        // RVA: 0x039ED700  token: 0x600044D
        public static UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.TimeNotificationBehaviour> CreateNotificationsPlayable(UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.IEnumerable<UnityEngine.Timeline.IMarker> markers, System.Double duration, UnityEngine.Playables.DirectorWrapMode extrapolationMode) { }
        // RVA: 0x039EE2D0  token: 0x600044E
        public static System.Boolean TrackTypeSupportsNotifications(System.Type type) { }

    }

    // TypeToken: 0x20000B8  // size: 0x10
    public static class PlayableDirectorUtility
    {
        // Methods
        // RVA: 0x0B34AAC4  token: 0x600044F
        public static System.Void SetOptionIndexSingleDirector(UnityEngine.Playables.PlayableDirector director, System.Int32 curOptionIndex, System.Int32 lastOptionIndex) { }
        // RVA: 0x0B34AA48  token: 0x6000450
        public static UnityEngine.Timeline.TimelinePlayable GetTimelinePlayable(UnityEngine.Playables.PlayableDirector director) { }
        // RVA: 0x03E10640  token: 0x6000451
        public static UnityEngine.Timeline.TimelinePlayable GetTimelinePlayable(UnityEngine.Playables.PlayableGraph graph) { }
        // RVA: 0x03E103D0  token: 0x6000452
        public static UnityEngine.Timeline.TimelinePlayable GetTimelinePlayableFromChildPlayable(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x0B34A984  token: 0x6000453
        public static System.Boolean CheckIfTimelineFunctional(UnityEngine.Playables.Playable playable) { }

    }

    // TypeToken: 0x20000B9  // size: 0x10
    public static class TimelineClipExtensions
    {
        // Fields
        private static readonly System.String k_UndoSetParentTrackText;  // static @ 0x0

        // Methods
        // RVA: 0x0B34DE8C  token: 0x6000454
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000BA  // size: 0x10
    public static class TimelineCreateUtilities
    {
        // Methods
        // RVA: 0x0B34E128  token: 0x6000455
        public static System.String GenerateUniqueActorName(System.Collections.Generic.List<UnityEngine.ScriptableObject> tracks, System.String name) { }
        // RVA: 0x0B34E3DC  token: 0x6000456
        public static System.Void SaveAssetIntoObject(UnityEngine.Object childAsset, UnityEngine.Object masterAsset) { }
        // RVA: 0x0B34DF84  token: 0x6000457
        public static UnityEngine.AnimationClip CreateAnimationClipForTrack(System.String name, UnityEngine.Timeline.TrackAsset track, System.Boolean isLegacy) { }
        // RVA: 0x0B34E49C  token: 0x6000458
        public static System.Boolean ValidateParentTrack(UnityEngine.Timeline.TrackAsset parent, System.Type childType) { }

    }

    // TypeToken: 0x20000BD  // size: 0x10
    public class TimelineRuntimeUtils
    {
        // Methods
        // RVA: 0x03E105E0  token: 0x600045D
        public static System.Double GetTimelineDeltaTime(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData frameData) { }
        // RVA: 0x0B34F3F8  token: 0x600045E
        public static System.Boolean CheckWillRuntimeElementEnabled(UnityEngine.Timeline.RuntimeElement element, System.Int32 curOptionIndex, System.Int32 lastOptionIndex, System.Int64 curDiscreteTime) { }
        // RVA: 0x0B3504A0  token: 0x600045F
        public static System.Void DoReverseJump(UnityEngine.Timeline.RuntimeClip jumpClip, UnityEngine.Timeline.ITimelineRootMarker timelineRoot, System.Double curTime, System.Double jumpTime, System.Double mixDuration, System.Int32 curOptionIndex, System.Int32 lastOptionIndex, System.Boolean needChangeOptionAfterJump, System.Int32 newOptionIndex) { }
        // RVA: 0x0B34F49C  token: 0x6000460
        public static System.Void DoJump(UnityEngine.Timeline.RuntimeClip jumpClip, UnityEngine.Timeline.ITimelineRootMarker timelineRoot, System.Double curTime, System.Double jumpTime, System.Double mixDuration, System.Int32 curOptionIndex, System.Int32 lastOptionIndex, System.Boolean needChangeOptionAfterJump, System.Int32 optionIndexChangingTo) { }
        // RVA: 0x0B351940  token: 0x6000461
        public static System.Double GetSubDirectorTimeForDirectorControl(UnityEngine.Playables.Playable playable, UnityEngine.Playables.PlayableDirector director) { }
        // RVA: 0x0B351CC0  token: 0x6000462
        public static System.Void SetNewTimeForCutsceneRoot(UnityEngine.Timeline.ITimelineRootMarker timelineRoot, System.Double newTime, System.Boolean useThisTimeNextFrame) { }
        // RVA: 0x0B35149C  token: 0x6000463
        public static UnityEngine.AnimationClip FindTPoseClipFromLayerDescriptors(System.Collections.Generic.List<UnityEngine.Timeline.LayerDescriptor> layerDescriptors) { }
        // RVA: 0x0B351C34  token: 0x6000464
        public static System.Void LinkDefaultTPoseIfNeeded(UnityEngine.Animations.AnimationLayerMixerPlayable mixer, System.Collections.Generic.List<UnityEngine.Timeline.LayerDescriptor> layerDescriptors, System.String gameObjectName, System.Boolean disableBaseTPoseClip) { }
        // RVA: 0x0B351A98  token: 0x6000465
        public static System.Void LinkDefaultTPoseClip(UnityEngine.Animations.AnimationLayerMixerPlayable mixer, UnityEngine.AnimationClip tposeClip, System.String gameObjectName) { }
        // RVA: 0x041E1670  token: 0x6000466
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BE  // size: 0x10
    public static class TimelineUndo
    {
        // Methods
        // RVA: 0x0B3520F8  token: 0x6000467
        public static System.Void PushDestroyUndo(UnityEngine.Timeline.TimelineAsset timeline, UnityEngine.Object thingToDirty, UnityEngine.Object objectToDestroy) { }

    }

    // TypeToken: 0x20000BF  // size: 0x14
    public sealed struct StandardFrameRates
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Timeline.StandardFrameRates Fps24;  // const
        public static UnityEngine.Timeline.StandardFrameRates Fps23_97;  // const
        public static UnityEngine.Timeline.StandardFrameRates Fps25;  // const
        public static UnityEngine.Timeline.StandardFrameRates Fps30;  // const
        public static UnityEngine.Timeline.StandardFrameRates Fps29_97;  // const
        public static UnityEngine.Timeline.StandardFrameRates Fps50;  // const
        public static UnityEngine.Timeline.StandardFrameRates Fps60;  // const
        public static UnityEngine.Timeline.StandardFrameRates Fps59_94;  // const

    }

    // TypeToken: 0x20000C0  // size: 0x10
    public static class TimeUtility
    {
        // Fields
        public static readonly System.Double kTimeEpsilon;  // static @ 0x0
        public static readonly System.Double kFrameRateEpsilon;  // static @ 0x8
        public static readonly System.Double k_MaxTimelineDurationInSeconds;  // static @ 0x10
        public static readonly System.Double kFrameRateRounding;  // static @ 0x18

        // Methods
        // RVA: 0x0B34DDB8  token: 0x6000468
        public static System.Double GetAnimationClipLength(UnityEngine.AnimationClip clip) { }
        // RVA: 0x04D27990  token: 0x6000469
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C2  // size: 0x10
    public static class WeightUtility
    {
        // Methods
        // RVA: 0x03E11A60  token: 0x600046C
        public static System.Single NormalizeMixer(UnityEngine.Playables.Playable mixer) { }

    }

}

namespace UnityEngine.Timeline.Condition
{

    // TypeToken: 0x20000C3  // size: 0x20
    public class TimelineConditionControlMono : UnityEngine.MonoBehaviour
    {
        // Fields
        public Beyond.PackageBridge.Timeline.TimelineConditionBase condition;  // 0x18

        // Methods
        // RVA: 0x0B34DEE8  token: 0x600046D
        public System.Void RefreshOnCondition() { }
        // RVA: 0x05393520  token: 0x600046E
        public System.Void .ctor() { }

    }

}

