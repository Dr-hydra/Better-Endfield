// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.VideoModule.dll
// Classes:  15
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x200000C  // size: 0x80
    public sealed class EventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x082A4F84  token: 0x6000080
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000081
        public virtual System.Void Invoke(UnityEngine.Video.VideoPlayer source) { }

    }

    // TypeToken: 0x200000D  // size: 0x80
    public sealed class ErrorEventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0AAFCFE0  token: 0x6000082
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x6000083
        public virtual System.Void Invoke(UnityEngine.Video.VideoPlayer source, System.String message) { }

    }

    // TypeToken: 0x200000E  // size: 0x80
    public sealed class FrameReadyEventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x08EF5378  token: 0x6000084
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x6000085
        public virtual System.Void Invoke(UnityEngine.Video.VideoPlayer source, System.Int64 frameIdx) { }

    }

    // TypeToken: 0x200000F  // size: 0x80
    public sealed class TimeEventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B58E0EC  token: 0x6000086
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x6000087
        public virtual System.Void Invoke(UnityEngine.Video.VideoPlayer source, System.Double seconds) { }

    }

namespace UnityEngine.Experimental.Video
{

    // TypeToken: 0x2000002  // size: 0x20
    public sealed struct VideoClipPlayable : UnityEngine.Playables.IPlayable, System.IEquatable`1
    {
        // Fields
        private UnityEngine.Playables.PlayableHandle m_Handle;  // 0x10

        // Methods
        // RVA: 0x04D9E560  token: 0x6000001
        public virtual UnityEngine.Playables.PlayableHandle GetHandle() { }
        // RVA: 0x0B58E1C0  token: 0x6000002
        public virtual System.Boolean Equals(UnityEngine.Experimental.Video.VideoClipPlayable other) { }

    }

}

namespace UnityEngine.Video
{

    // TypeToken: 0x2000003  // size: 0x18
    public sealed class VideoClip : UnityEngine.Object
    {
        // Properties
        System.String originalPath { get; /* RVA: 0x0B58E444 */ }
        System.UInt64 frameCount { get; /* RVA: 0x0B58E374 */ }
        System.Double frameRate { get; /* RVA: 0x0B58E3A8 */ }
        System.Double length { get; /* RVA: 0x0B58E410 */ }
        System.UInt32 width { get; /* RVA: 0x0B58E514 */ }
        System.UInt32 height { get; /* RVA: 0x0B58E3DC */ }
        System.UInt32 pixelAspectRatioNumerator { get; /* RVA: 0x0B58E4AC */ }
        System.UInt32 pixelAspectRatioDenominator { get; /* RVA: 0x0B58E478 */ }
        System.Boolean sRGB { get; /* RVA: 0x0B58E4E0 */ }
        System.UInt16 audioTrackCount { get; /* RVA: 0x0B58E340 */ }

        // Methods
        // RVA: 0x0B58E300  token: 0x6000003
        private System.Void .ctor() { }
        // RVA: 0x0B58E234  token: 0x600000E
        public System.UInt16 GetAudioChannelCount(System.UInt16 audioTrackIdx) { }
        // RVA: 0x0B58E2BC  token: 0x600000F
        public System.UInt32 GetAudioSampleRate(System.UInt16 audioTrackIdx) { }
        // RVA: 0x0B58E278  token: 0x6000010
        public System.String GetAudioLanguage(System.UInt16 audioTrackIdx) { }

    }

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct VideoRenderMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Video.VideoRenderMode CameraFarPlane;  // const
        public static UnityEngine.Video.VideoRenderMode CameraNearPlane;  // const
        public static UnityEngine.Video.VideoRenderMode RenderTexture;  // const
        public static UnityEngine.Video.VideoRenderMode MaterialOverride;  // const
        public static UnityEngine.Video.VideoRenderMode APIOnly;  // const

    }

    // TypeToken: 0x2000005  // size: 0x14
    public sealed struct Video3DLayout
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Video.Video3DLayout No3D;  // const
        public static UnityEngine.Video.Video3DLayout SideBySide3D;  // const
        public static UnityEngine.Video.Video3DLayout OverUnder3D;  // const

    }

    // TypeToken: 0x2000006  // size: 0x14
    public sealed struct VideoAspectRatio
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Video.VideoAspectRatio NoScaling;  // const
        public static UnityEngine.Video.VideoAspectRatio FitVertically;  // const
        public static UnityEngine.Video.VideoAspectRatio FitHorizontally;  // const
        public static UnityEngine.Video.VideoAspectRatio FitInside;  // const
        public static UnityEngine.Video.VideoAspectRatio FitOutside;  // const
        public static UnityEngine.Video.VideoAspectRatio Stretch;  // const

    }

    // TypeToken: 0x2000007  // size: 0x14
    public sealed struct VideoTimeSource
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Video.VideoTimeSource AudioDSPTimeSource;  // const
        public static UnityEngine.Video.VideoTimeSource GameTimeSource;  // const

    }

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct VideoTimeReference
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Video.VideoTimeReference Freerun;  // const
        public static UnityEngine.Video.VideoTimeReference InternalTime;  // const
        public static UnityEngine.Video.VideoTimeReference ExternalTime;  // const

    }

    // TypeToken: 0x2000009  // size: 0x14
    public sealed struct VideoSource
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Video.VideoSource VideoClip;  // const
        public static UnityEngine.Video.VideoSource Url;  // const

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct VideoAudioOutputMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Video.VideoAudioOutputMode None;  // const
        public static UnityEngine.Video.VideoAudioOutputMode AudioSource;  // const
        public static UnityEngine.Video.VideoAudioOutputMode Direct;  // const
        public static UnityEngine.Video.VideoAudioOutputMode APIOnly;  // const

    }

    // TypeToken: 0x200000B  // size: 0x58
    public sealed class VideoPlayer : UnityEngine.Behaviour
    {
        // Fields
        private UnityEngine.Video.VideoPlayer.EventHandler prepareCompleted;  // 0x18
        private UnityEngine.Video.VideoPlayer.EventHandler loopPointReached;  // 0x20
        private UnityEngine.Video.VideoPlayer.EventHandler started;  // 0x28
        private UnityEngine.Video.VideoPlayer.EventHandler frameDropped;  // 0x30
        private UnityEngine.Video.VideoPlayer.ErrorEventHandler errorReceived;  // 0x38
        private UnityEngine.Video.VideoPlayer.EventHandler seekCompleted;  // 0x40
        private UnityEngine.Video.VideoPlayer.TimeEventHandler clockResyncOccurred;  // 0x48
        private UnityEngine.Video.VideoPlayer.FrameReadyEventHandler frameReady;  // 0x50

        // Properties
        UnityEngine.Video.VideoSource source { get; /* RVA: 0x0B58F5F8 */ set; /* RVA: 0x0B59011C */ }
        System.String url { get; /* RVA: 0x0B58F834 */ set; /* RVA: 0x0B5903B4 */ }
        UnityEngine.Video.VideoClip clip { get; /* RVA: 0x0B58F1E4 */ set; /* RVA: 0x0B58FDF0 */ }
        UnityEngine.Video.VideoRenderMode renderMode { get; /* RVA: 0x0B58F55C */ set; /* RVA: 0x0B590054 */ }
        UnityEngine.Camera targetCamera { get; /* RVA: 0x0B58F694 */ set; /* RVA: 0x0B5901E0 */ }
        UnityEngine.RenderTexture targetTexture { get; /* RVA: 0x0B58F730 */ set; /* RVA: 0x0B5902AC */ }
        UnityEngine.Renderer targetMaterialRenderer { get; /* RVA: 0x0B58F6FC */ set; /* RVA: 0x0B590268 */ }
        System.String targetMaterialProperty { get; /* RVA: 0x0B58F6C8 */ set; /* RVA: 0x0B590224 */ }
        UnityEngine.Video.VideoAspectRatio aspectRatio { get; /* RVA: 0x0B58F010 */ set; /* RVA: 0x0B58FD70 */ }
        System.Single targetCameraAlpha { get; /* RVA: 0x0B58F660 */ set; /* RVA: 0x0B59019C */ }
        UnityEngine.Video.Video3DLayout targetCamera3DLayout { get; /* RVA: 0x0B58F62C */ set; /* RVA: 0x0B59015C */ }
        UnityEngine.Texture texture { get; /* RVA: 0x0B58F764 */ }
        System.Boolean isPrepared { get; /* RVA: 0x0B58F424 */ }
        System.Boolean waitForFirstFrame { get; /* RVA: 0x0B58F868 */ set; /* RVA: 0x0B5903F8 */ }
        System.Boolean playOnAwake { get; /* RVA: 0x0B58F4F4 */ set; /* RVA: 0x0B58FFCC */ }
        System.Boolean isPlaying { get; /* RVA: 0x0B58F3F0 */ }
        System.Boolean isPaused { get; /* RVA: 0x0B58F3BC */ }
        System.Boolean canSetTime { get; /* RVA: 0x0B58F17C */ }
        System.Double time { get; /* RVA: 0x0B58F800 */ set; /* RVA: 0x0B590370 */ }
        System.Int64 frame { get; /* RVA: 0x0B58F320 */ set; /* RVA: 0x0B58FF44 */ }
        System.Double clockTime { get; /* RVA: 0x0B58F218 */ }
        System.Boolean canStep { get; /* RVA: 0x0B58F1B0 */ }
        System.Boolean canSetPlaybackSpeed { get; /* RVA: 0x0B58F0E0 */ }
        System.Single playbackSpeed { get; /* RVA: 0x0B58F528 */ set; /* RVA: 0x0B590010 */ }
        System.Boolean isLooping { get; /* RVA: 0x0B58F388 */ set; /* RVA: 0x0B58FF88 */ }
        System.Boolean canSetTimeSource { get; /* RVA: 0x0B58F148 */ }
        UnityEngine.Video.VideoTimeSource timeSource { get; /* RVA: 0x0B58F7CC */ set; /* RVA: 0x0B590330 */ }
        UnityEngine.Video.VideoTimeReference timeReference { get; /* RVA: 0x0B58F798 */ set; /* RVA: 0x0B5902F0 */ }
        System.Double externalReferenceTime { get; /* RVA: 0x0B58F284 */ set; /* RVA: 0x0B58FF00 */ }
        System.Boolean canSetSkipOnDrop { get; /* RVA: 0x0B58F114 */ }
        System.Boolean skipOnDrop { get; /* RVA: 0x0B58F5C4 */ set; /* RVA: 0x0B5900D8 */ }
        System.UInt64 frameCount { get; /* RVA: 0x0B58F2B8 */ }
        System.Single frameRate { get; /* RVA: 0x0B58F2EC */ }
        System.Double length { get; /* RVA: 0x0B58F458 */ }
        System.UInt32 width { get; /* RVA: 0x0B58F89C */ }
        System.UInt32 height { get; /* RVA: 0x0B58F354 */ }
        System.UInt32 pixelAspectRatioNumerator { get; /* RVA: 0x0B58F4C0 */ }
        System.UInt32 pixelAspectRatioDenominator { get; /* RVA: 0x0B58F48C */ }
        System.UInt16 audioTrackCount { get; /* RVA: 0x0B58F078 */ }
        System.UInt16 controlledAudioTrackMaxCount { get; /* RVA: 0x0B58F258 */ }
        System.UInt16 controlledAudioTrackCount { get; /* RVA: 0x0B58F24C */ set; /* RVA: 0x0B58FE34 */ }
        UnityEngine.Video.VideoAudioOutputMode audioOutputMode { get; /* RVA: 0x0B58F044 */ set; /* RVA: 0x0B58FDB0 */ }
        System.Boolean canSetDirectAudioVolume { get; /* RVA: 0x0B58F0AC */ }
        System.Boolean sendFrameReadyEvents { get; /* RVA: 0x0B58F590 */ set; /* RVA: 0x0B590094 */ }

        // Events
        event UnityEngine.Video.VideoPlayer.EventHandler prepareCompleted;
        event UnityEngine.Video.VideoPlayer.EventHandler loopPointReached;
        event UnityEngine.Video.VideoPlayer.EventHandler started;
        event UnityEngine.Video.VideoPlayer.EventHandler frameDropped;
        event UnityEngine.Video.VideoPlayer.ErrorEventHandler errorReceived;
        event UnityEngine.Video.VideoPlayer.EventHandler seekCompleted;
        event UnityEngine.Video.VideoPlayer.TimeEventHandler clockResyncOccurred;
        event UnityEngine.Video.VideoPlayer.FrameReadyEventHandler frameReady;

        // Methods
        // RVA: 0x0B58E994  token: 0x6000028
        public System.Void Prepare() { }
        // RVA: 0x0B58E960  token: 0x600002E
        public System.Void Play() { }
        // RVA: 0x0B58E92C  token: 0x600002F
        public System.Void Pause() { }
        // RVA: 0x0B58EB3C  token: 0x6000030
        public System.Void Stop() { }
        // RVA: 0x0B58EB08  token: 0x600003A
        public System.Void StepForward() { }
        // RVA: 0x0B58E5E0  token: 0x6000052
        public System.String GetAudioLanguageCode(System.UInt16 trackIndex) { }
        // RVA: 0x0B58E59C  token: 0x6000053
        public System.UInt16 GetAudioChannelCount(System.UInt16 trackIndex) { }
        // RVA: 0x0B58E624  token: 0x6000054
        public System.UInt32 GetAudioSampleRate(System.UInt16 trackIndex) { }
        // RVA: 0x0B58E668  token: 0x6000058
        private System.UInt16 GetControlledAudioTrackCount() { }
        // RVA: 0x0B58E9C8  token: 0x6000059
        private System.Void SetControlledAudioTrackCount(System.UInt16 value) { }
        // RVA: 0x0B58E548  token: 0x600005A
        public System.Void EnableAudioTrack(System.UInt16 trackIndex, System.Boolean enabled) { }
        // RVA: 0x0B58E8E8  token: 0x600005B
        public System.Boolean IsAudioTrackEnabled(System.UInt16 trackIndex) { }
        // RVA: 0x0B58E6E0  token: 0x600005F
        public System.Single GetDirectAudioVolume(System.UInt16 trackIndex) { }
        // RVA: 0x0B58EA60  token: 0x6000060
        public System.Void SetDirectAudioVolume(System.UInt16 trackIndex, System.Single volume) { }
        // RVA: 0x0B58E69C  token: 0x6000061
        public System.Boolean GetDirectAudioMute(System.UInt16 trackIndex) { }
        // RVA: 0x0B58EA0C  token: 0x6000062
        public System.Void SetDirectAudioMute(System.UInt16 trackIndex, System.Boolean mute) { }
        // RVA: 0x0B58E724  token: 0x6000063
        public UnityEngine.AudioSource GetTargetAudioSource(System.UInt16 trackIndex) { }
        // RVA: 0x0B58EAB4  token: 0x6000064
        public System.Void SetTargetAudioSource(System.UInt16 trackIndex, UnityEngine.AudioSource source) { }
        // RVA: 0x0B58E858  token: 0x6000077
        private static System.Void InvokePrepareCompletedCallback_Internal(UnityEngine.Video.VideoPlayer source) { }
        // RVA: 0x0B58E7F8  token: 0x6000078
        private static System.Void InvokeFrameReadyCallback_Internal(UnityEngine.Video.VideoPlayer source, System.Int64 frameIdx) { }
        // RVA: 0x0B58E828  token: 0x6000079
        private static System.Void InvokeLoopPointReachedCallback_Internal(UnityEngine.Video.VideoPlayer source) { }
        // RVA: 0x0B58E8B8  token: 0x600007A
        private static System.Void InvokeStartedCallback_Internal(UnityEngine.Video.VideoPlayer source) { }
        // RVA: 0x0B58E7C8  token: 0x600007B
        private static System.Void InvokeFrameDroppedCallback_Internal(UnityEngine.Video.VideoPlayer source) { }
        // RVA: 0x0B58E798  token: 0x600007C
        private static System.Void InvokeErrorReceivedCallback_Internal(UnityEngine.Video.VideoPlayer source, System.String errorStr) { }
        // RVA: 0x0B58E888  token: 0x600007D
        private static System.Void InvokeSeekCompletedCallback_Internal(UnityEngine.Video.VideoPlayer source) { }
        // RVA: 0x0B58E768  token: 0x600007E
        private static System.Void InvokeClockResyncOccurredCallback_Internal(UnityEngine.Video.VideoPlayer source, System.Double seconds) { }
        // RVA: 0x05393520  token: 0x600007F
        public System.Void .ctor() { }

    }

}

