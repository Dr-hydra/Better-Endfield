// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.VideoModule.dll
// Classes:  15
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

CLASS: EventHandler
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x082A4F84  token=0x6000080  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000081  System.Void Invoke(UnityEngine.Video.VideoPlayer source)
END_CLASS

CLASS: ErrorEventHandler
TYPE:  sealed class
TOKEN: 0x200000D
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0AAFCFE0  token=0x6000082  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x6000083  System.Void Invoke(UnityEngine.Video.VideoPlayer source, System.String message)
END_CLASS

CLASS: FrameReadyEventHandler
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x08EF5378  token=0x6000084  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x6000085  System.Void Invoke(UnityEngine.Video.VideoPlayer source, System.Int64 frameIdx)
END_CLASS

CLASS: TimeEventHandler
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B58E0EC  token=0x6000086  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x6000087  System.Void Invoke(UnityEngine.Video.VideoPlayer source, System.Double seconds)
END_CLASS

CLASS: UnityEngine.Experimental.Video.VideoClipPlayable
TYPE:  sealed struct
TOKEN: 0x2000002
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
METHODS:
  RVA=0x04D9E560  token=0x6000001  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=0x0B58E1C0  token=0x6000002  System.Boolean Equals(UnityEngine.Experimental.Video.VideoClipPlayable other)
END_CLASS

CLASS: UnityEngine.Video.VideoClip
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
PROPERTIES:
  originalPath  get=0x0B58E444
  frameCount  get=0x0B58E374
  frameRate  get=0x0B58E3A8
  length  get=0x0B58E410
  width  get=0x0B58E514
  height  get=0x0B58E3DC
  pixelAspectRatioNumerator  get=0x0B58E4AC
  pixelAspectRatioDenominator  get=0x0B58E478
  sRGB  get=0x0B58E4E0
  audioTrackCount  get=0x0B58E340
METHODS:
  RVA=0x0B58E300  token=0x6000003  System.Void .ctor()
  RVA=0x0B58E234  token=0x600000E  System.UInt16 GetAudioChannelCount(System.UInt16 audioTrackIdx)
  RVA=0x0B58E2BC  token=0x600000F  System.UInt32 GetAudioSampleRate(System.UInt16 audioTrackIdx)
  RVA=0x0B58E278  token=0x6000010  System.String GetAudioLanguage(System.UInt16 audioTrackIdx)
END_CLASS

CLASS: UnityEngine.Video.VideoRenderMode
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Video.VideoRenderModeCameraFarPlane  // const
  public    static  UnityEngine.Video.VideoRenderModeCameraNearPlane  // const
  public    static  UnityEngine.Video.VideoRenderModeRenderTexture  // const
  public    static  UnityEngine.Video.VideoRenderModeMaterialOverride  // const
  public    static  UnityEngine.Video.VideoRenderModeAPIOnly  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Video.Video3DLayout
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Video.Video3DLayout No3D  // const
  public    static  UnityEngine.Video.Video3DLayout SideBySide3D  // const
  public    static  UnityEngine.Video.Video3DLayout OverUnder3D  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Video.VideoAspectRatio
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Video.VideoAspectRatioNoScaling  // const
  public    static  UnityEngine.Video.VideoAspectRatioFitVertically  // const
  public    static  UnityEngine.Video.VideoAspectRatioFitHorizontally  // const
  public    static  UnityEngine.Video.VideoAspectRatioFitInside  // const
  public    static  UnityEngine.Video.VideoAspectRatioFitOutside  // const
  public    static  UnityEngine.Video.VideoAspectRatioStretch  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Video.VideoTimeSource
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Video.VideoTimeSourceAudioDSPTimeSource  // const
  public    static  UnityEngine.Video.VideoTimeSourceGameTimeSource  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Video.VideoTimeReference
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Video.VideoTimeReferenceFreerun  // const
  public    static  UnityEngine.Video.VideoTimeReferenceInternalTime  // const
  public    static  UnityEngine.Video.VideoTimeReferenceExternalTime  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Video.VideoSource
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Video.VideoSource   VideoClip  // const
  public    static  UnityEngine.Video.VideoSource   Url  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Video.VideoAudioOutputMode
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Video.VideoAudioOutputModeNone  // const
  public    static  UnityEngine.Video.VideoAudioOutputModeAudioSource  // const
  public    static  UnityEngine.Video.VideoAudioOutputModeDirect  // const
  public    static  UnityEngine.Video.VideoAudioOutputModeAPIOnly  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Video.VideoPlayer
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x58
EXTENDS: UnityEngine.Behaviour
FIELDS:
  private           UnityEngine.Video.VideoPlayer.EventHandlerprepareCompleted  // 0x18
  private           UnityEngine.Video.VideoPlayer.EventHandlerloopPointReached  // 0x20
  private           UnityEngine.Video.VideoPlayer.EventHandlerstarted  // 0x28
  private           UnityEngine.Video.VideoPlayer.EventHandlerframeDropped  // 0x30
  private           UnityEngine.Video.VideoPlayer.ErrorEventHandlererrorReceived  // 0x38
  private           UnityEngine.Video.VideoPlayer.EventHandlerseekCompleted  // 0x40
  private           UnityEngine.Video.VideoPlayer.TimeEventHandlerclockResyncOccurred  // 0x48
  private           UnityEngine.Video.VideoPlayer.FrameReadyEventHandlerframeReady  // 0x50
PROPERTIES:
  source  get=0x0B58F5F8  set=0x0B59011C
  url  get=0x0B58F834  set=0x0B5903B4
  clip  get=0x0B58F1E4  set=0x0B58FDF0
  renderMode  get=0x0B58F55C  set=0x0B590054
  targetCamera  get=0x0B58F694  set=0x0B5901E0
  targetTexture  get=0x0B58F730  set=0x0B5902AC
  targetMaterialRenderer  get=0x0B58F6FC  set=0x0B590268
  targetMaterialProperty  get=0x0B58F6C8  set=0x0B590224
  aspectRatio  get=0x0B58F010  set=0x0B58FD70
  targetCameraAlpha  get=0x0B58F660  set=0x0B59019C
  targetCamera3DLayout  get=0x0B58F62C  set=0x0B59015C
  texture  get=0x0B58F764
  isPrepared  get=0x0B58F424
  waitForFirstFrame  get=0x0B58F868  set=0x0B5903F8
  playOnAwake  get=0x0B58F4F4  set=0x0B58FFCC
  isPlaying  get=0x0B58F3F0
  isPaused  get=0x0B58F3BC
  canSetTime  get=0x0B58F17C
  time  get=0x0B58F800  set=0x0B590370
  frame  get=0x0B58F320  set=0x0B58FF44
  clockTime  get=0x0B58F218
  canStep  get=0x0B58F1B0
  canSetPlaybackSpeed  get=0x0B58F0E0
  playbackSpeed  get=0x0B58F528  set=0x0B590010
  isLooping  get=0x0B58F388  set=0x0B58FF88
  canSetTimeSource  get=0x0B58F148
  timeSource  get=0x0B58F7CC  set=0x0B590330
  timeReference  get=0x0B58F798  set=0x0B5902F0
  externalReferenceTime  get=0x0B58F284  set=0x0B58FF00
  canSetSkipOnDrop  get=0x0B58F114
  skipOnDrop  get=0x0B58F5C4  set=0x0B5900D8
  frameCount  get=0x0B58F2B8
  frameRate  get=0x0B58F2EC
  length  get=0x0B58F458
  width  get=0x0B58F89C
  height  get=0x0B58F354
  pixelAspectRatioNumerator  get=0x0B58F4C0
  pixelAspectRatioDenominator  get=0x0B58F48C
  audioTrackCount  get=0x0B58F078
  controlledAudioTrackMaxCount  get=0x0B58F258
  controlledAudioTrackCount  get=0x0B58F24C  set=0x0B58FE34
  audioOutputMode  get=0x0B58F044  set=0x0B58FDB0
  canSetDirectAudioVolume  get=0x0B58F0AC
  sendFrameReadyEvents  get=0x0B58F590  set=0x0B590094
EVENTS:
  prepareCompleted  add=add_prepareCompleted  remove=remove_prepareCompleted
  loopPointReached  add=add_loopPointReached  remove=remove_loopPointReached
  started  add=add_started  remove=remove_started
  frameDropped  add=add_frameDropped  remove=remove_frameDropped
  errorReceived  add=add_errorReceived  remove=remove_errorReceived
  seekCompleted  add=add_seekCompleted  remove=remove_seekCompleted
  clockResyncOccurred  add=add_clockResyncOccurred  remove=remove_clockResyncOccurred
  frameReady  add=add_frameReady  remove=remove_frameReady
METHODS:
  RVA=0x0B58E994  token=0x6000028  System.Void Prepare()
  RVA=0x0B58E960  token=0x600002E  System.Void Play()
  RVA=0x0B58E92C  token=0x600002F  System.Void Pause()
  RVA=0x0B58EB3C  token=0x6000030  System.Void Stop()
  RVA=0x0B58EB08  token=0x600003A  System.Void StepForward()
  RVA=0x0B58E5E0  token=0x6000052  System.String GetAudioLanguageCode(System.UInt16 trackIndex)
  RVA=0x0B58E59C  token=0x6000053  System.UInt16 GetAudioChannelCount(System.UInt16 trackIndex)
  RVA=0x0B58E624  token=0x6000054  System.UInt32 GetAudioSampleRate(System.UInt16 trackIndex)
  RVA=0x0B58E668  token=0x6000058  System.UInt16 GetControlledAudioTrackCount()
  RVA=0x0B58E9C8  token=0x6000059  System.Void SetControlledAudioTrackCount(System.UInt16 value)
  RVA=0x0B58E548  token=0x600005A  System.Void EnableAudioTrack(System.UInt16 trackIndex, System.Boolean enabled)
  RVA=0x0B58E8E8  token=0x600005B  System.Boolean IsAudioTrackEnabled(System.UInt16 trackIndex)
  RVA=0x0B58E6E0  token=0x600005F  System.Single GetDirectAudioVolume(System.UInt16 trackIndex)
  RVA=0x0B58EA60  token=0x6000060  System.Void SetDirectAudioVolume(System.UInt16 trackIndex, System.Single volume)
  RVA=0x0B58E69C  token=0x6000061  System.Boolean GetDirectAudioMute(System.UInt16 trackIndex)
  RVA=0x0B58EA0C  token=0x6000062  System.Void SetDirectAudioMute(System.UInt16 trackIndex, System.Boolean mute)
  RVA=0x0B58E724  token=0x6000063  UnityEngine.AudioSource GetTargetAudioSource(System.UInt16 trackIndex)
  RVA=0x0B58EAB4  token=0x6000064  System.Void SetTargetAudioSource(System.UInt16 trackIndex, UnityEngine.AudioSource source)
  RVA=0x0B58E858  token=0x6000077  System.Void InvokePrepareCompletedCallback_Internal(UnityEngine.Video.VideoPlayer source)
  RVA=0x0B58E7F8  token=0x6000078  System.Void InvokeFrameReadyCallback_Internal(UnityEngine.Video.VideoPlayer source, System.Int64 frameIdx)
  RVA=0x0B58E828  token=0x6000079  System.Void InvokeLoopPointReachedCallback_Internal(UnityEngine.Video.VideoPlayer source)
  RVA=0x0B58E8B8  token=0x600007A  System.Void InvokeStartedCallback_Internal(UnityEngine.Video.VideoPlayer source)
  RVA=0x0B58E7C8  token=0x600007B  System.Void InvokeFrameDroppedCallback_Internal(UnityEngine.Video.VideoPlayer source)
  RVA=0x0B58E798  token=0x600007C  System.Void InvokeErrorReceivedCallback_Internal(UnityEngine.Video.VideoPlayer source, System.String errorStr)
  RVA=0x0B58E888  token=0x600007D  System.Void InvokeSeekCompletedCallback_Internal(UnityEngine.Video.VideoPlayer source)
  RVA=0x0B58E768  token=0x600007E  System.Void InvokeClockResyncOccurredCallback_Internal(UnityEngine.Video.VideoPlayer source, System.Double seconds)
  RVA=0x05393520  token=0x600007F  System.Void .ctor()
END_CLASS

