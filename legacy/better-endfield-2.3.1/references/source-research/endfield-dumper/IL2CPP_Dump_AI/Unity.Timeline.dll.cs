// ========================================================
// Dumped by @desirepro
// Assembly: Unity.Timeline.dll
// Classes:  196
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

CLASS: PostPlaybackState
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.ActivationTrack.PostPlaybackStateActive  // const
  public    static  UnityEngine.Timeline.ActivationTrack.PostPlaybackStateInactive  // const
  public    static  UnityEngine.Timeline.ActivationTrack.PostPlaybackStateRevert  // const
  public    static  UnityEngine.Timeline.ActivationTrack.PostPlaybackStateLeaveAsIs  // const
METHODS:
END_CLASS

CLASS: WeightInfo
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x38
FIELDS:
  public            UnityEngine.Playables.Playable  mixer  // 0x10
  public            UnityEngine.Playables.Playable  parentMixer  // 0x20
  public            System.Int32                    port  // 0x30
METHODS:
END_CLASS

CLASS: LoopMode
TYPE:  sealed struct
TOKEN: 0x2000022
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.LoopModeUseSourceAsset  // const
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.LoopModeOn  // const
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.LoopModeOff  // const
METHODS:
END_CLASS

CLASS: ClothResetOption
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.ClothResetOptionSoftReset  // const
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.ClothResetOptionUseClothRelativeTeleport  // const
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.ClothResetOptionUseStablizationTime  // const
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.ClothResetOptionDisablePhysics  // const
METHODS:
END_CLASS

CLASS: AnimationEventTriggerMode
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerModeDefault  // const
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerModeMuteAllEvents  // const
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerModeSeekAllEvents  // const
METHODS:
END_CLASS

CLASS: ClipEndAudioBehaviour
TYPE:  sealed struct
TOKEN: 0x2000025
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviourDefault  // const
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviourMuteAll  // const
METHODS:
END_CLASS

CLASS: Versions
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.VersionsInitial  // const
  public    static  UnityEngine.Timeline.AnimationPlayableAsset.VersionsRotationAsEuler  // const
METHODS:
END_CLASS

CLASS: AnimationPlayableAssetUpgrade
TYPE:  static class
TOKEN: 0x2000027
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B33B550  token=0x60000B4  System.Void ConvertRotationToEuler(UnityEngine.Timeline.AnimationPlayableAsset asset)
END_CLASS

CLASS: <get_outputs>d__123
TYPE:  sealed class
TOKEN: 0x2000028
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.Playables.PlayableBinding<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x38
  public            UnityEngine.Timeline.AnimationPlayableAsset<>4__this  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding>.Current  get=0x02EC7B40
  System.Collections.IEnumerator.Current  get=0x02EC7BC0
METHODS:
  RVA=0x03A10F90  token=0x60000B5  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000B6  System.Void System.IDisposable.Dispose()
  RVA=0x02EC7680  token=0x60000B7  System.Boolean MoveNext()
  RVA=0x02EC7B70  token=0x60000B9  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x02EC77A0  token=0x60000BB  System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding>.GetEnumerator()
  RVA=0x02EC7B60  token=0x60000BC  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: AnimationTrackUpgrade
TYPE:  static class
TOKEN: 0x2000030
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B33C064  token=0x6000122  System.Void ConvertRotationsToEuler(UnityEngine.Timeline.AnimationTrack track)
  RVA=0x0B33BFFC  token=0x6000123  System.Void ConvertRootMotion(UnityEngine.Timeline.AnimationTrack track)
  RVA=0x0B33BFBC  token=0x6000124  System.Void ConvertInfiniteTrack(UnityEngine.Timeline.AnimationTrack track)
END_CLASS

CLASS: <get_outputs>d__76
TYPE:  sealed class
TOKEN: 0x2000031
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.Playables.PlayableBinding<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x38
  public            UnityEngine.Timeline.AnimationTrack<>4__this  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding>.Current  get=0x02EC7B40
  System.Collections.IEnumerator.Current  get=0x0B345DE4
METHODS:
  RVA=0x03A10F90  token=0x6000125  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000126  System.Void System.IDisposable.Dispose()
  RVA=0x0441D910  token=0x6000127  System.Boolean MoveNext()
  RVA=0x0B345D98  token=0x6000129  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x04CE4250  token=0x600012B  System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding>.GetEnumerator()
  RVA=0x0B345D90  token=0x600012C  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Versions
TYPE:  sealed struct
TOKEN: 0x2000034
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.TimelineClip.VersionsInitial  // const
  public    static  UnityEngine.Timeline.TimelineClip.VersionsClipInFromGlobalToLocal  // const
METHODS:
END_CLASS

CLASS: TimelineClipUpgrade
TYPE:  static class
TOKEN: 0x2000035
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B341458  token=0x6000170  System.Void UpgradeClipInFromGlobalToLocal(UnityEngine.Timeline.TimelineClip clip)
END_CLASS

CLASS: ClipExtrapolation
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.TimelineClip.ClipExtrapolationNone  // const
  public    static  UnityEngine.Timeline.TimelineClip.ClipExtrapolationHold  // const
  public    static  UnityEngine.Timeline.TimelineClip.ClipExtrapolationLoop  // const
  public    static  UnityEngine.Timeline.TimelineClip.ClipExtrapolationPingPong  // const
  public    static  UnityEngine.Timeline.TimelineClip.ClipExtrapolationContinue  // const
METHODS:
END_CLASS

CLASS: BlendCurveMode
TYPE:  sealed struct
TOKEN: 0x2000037
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.TimelineClip.BlendCurveModeAuto  // const
  public    static  UnityEngine.Timeline.TimelineClip.BlendCurveModeManual  // const
METHODS:
END_CLASS

CLASS: Versions
TYPE:  sealed struct
TOKEN: 0x2000039
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.TimelineAsset.VersionsInitial  // const
METHODS:
END_CLASS

CLASS: TimelineAssetUpgrade
TYPE:  static class
TOKEN: 0x200003A
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: MediaType
TYPE:  sealed struct
TOKEN: 0x200003B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.TimelineAsset.MediaTypeAnimation  // const
  public    static  UnityEngine.Timeline.TimelineAsset.MediaTypeAudio  // const
  public    static  UnityEngine.Timeline.TimelineAsset.MediaTypeTexture  // const
  public    static  UnityEngine.Timeline.TimelineAsset.MediaTypeVideo  // const
  public    static  UnityEngine.Timeline.TimelineAsset.MediaTypeScript  // const
  public    static  UnityEngine.Timeline.TimelineAsset.MediaTypeHybrid  // const
  public    static  UnityEngine.Timeline.TimelineAsset.MediaTypeGroup  // const
METHODS:
END_CLASS

CLASS: DurationMode
TYPE:  sealed struct
TOKEN: 0x200003C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.TimelineAsset.DurationModeBasedOnClips  // const
  public    static  UnityEngine.Timeline.TimelineAsset.DurationModeFixedLength  // const
METHODS:
END_CLASS

CLASS: EditorSettings
TYPE:  class
TOKEN: 0x200003D
SIZE:  0x20
FIELDS:
  private   static readonly System.Double                   kMinFrameRate  // static @ 0x0
  private   static readonly System.Double                   kMaxFrameRate  // static @ 0x8
  private   static readonly System.Double                   kDefaultFrameRate  // static @ 0x10
  private           System.Double                   m_Framerate  // 0x10
  private           System.Boolean                  m_ScenePreview  // 0x18
PROPERTIES:
  frameRate  get=0x04D96320  set=0x0B33F644
METHODS:
  RVA=0x039F4B00  token=0x60001A3  System.Void .ctor()
  RVA=0x04D1DB60  token=0x60001A4  System.Void .cctor()
END_CLASS

CLASS: <get_outputs>d__28
TYPE:  sealed class
TOKEN: 0x200003E
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.Playables.PlayableBinding<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x38
  public            UnityEngine.Timeline.TimelineAsset<>4__this  // 0x40
  private           System.Collections.Generic.IEnumerator<UnityEngine.Timeline.TrackAsset><>7__wrap1  // 0x48
  private           System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding><>7__wrap2  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding>.Current  get=0x02EC7B40
  System.Collections.IEnumerator.Current  get=0x0B345B6C
METHODS:
  RVA=0x03A10F90  token=0x60001A5  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x04CD8100  token=0x60001A6  System.Void System.IDisposable.Dispose()
  RVA=0x03FC8FA0  token=0x60001A7  System.Boolean MoveNext()
  RVA=0x03FC8DD0  token=0x60001A8  System.Void <>m__Finally1()
  RVA=0x03FC8F40  token=0x60001A9  System.Void <>m__Finally2()
  RVA=0x0B345B20  token=0x60001AB  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x04A21450  token=0x60001AD  System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding>.GetEnumerator()
  RVA=0x0B345B18  token=0x60001AE  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Versions
TYPE:  sealed struct
TOKEN: 0x2000040
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.TrackAsset.VersionsInitial  // const
  public    static  UnityEngine.Timeline.TrackAsset.VersionsRotationAsEuler  // const
  public    static  UnityEngine.Timeline.TrackAsset.VersionsRootMotionUpgrade  // const
  public    static  UnityEngine.Timeline.TrackAsset.VersionsAnimatedTrackProperties  // const
METHODS:
END_CLASS

CLASS: TrackAssetUpgrade
TYPE:  static class
TOKEN: 0x2000041
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: TransientBuildData
TYPE:  sealed struct
TOKEN: 0x2000042
SIZE:  0x28
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.Timeline.TrackAsset>trackList  // 0x10
  public            System.Collections.Generic.List<UnityEngine.Timeline.TimelineClip>clipList  // 0x18
  public            System.Collections.Generic.List<UnityEngine.Timeline.IMarker>markerList  // 0x20
METHODS:
  RVA=0x048BB060  token=0x600021B  UnityEngine.Timeline.TrackAsset.TransientBuildData Create()
  RVA=0x039EDBA0  token=0x600021C  System.Void Clear()
END_CLASS

CLASS: <get_outputs>d__75
TYPE:  sealed class
TOKEN: 0x2000043
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.Playables.PlayableBinding<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x38
  public            UnityEngine.Timeline.TrackAsset <>4__this  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding>.Current  get=0x02EC7B40
  System.Collections.IEnumerator.Current  get=0x0B345D40
METHODS:
  RVA=0x03A10F90  token=0x600021D  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600021E  System.Void System.IDisposable.Dispose()
  RVA=0x0441DAE0  token=0x600021F  System.Boolean MoveNext()
  RVA=0x0B345CF4  token=0x6000221  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x03A10FB0  token=0x6000223  System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding>.GetEnumerator()
  RVA=0x0B345CEC  token=0x6000224  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000044
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Timeline.TrackAsset.<>c<>9  // static @ 0x0
  public    static  System.Comparison<UnityEngine.Timeline.TimelineClip><>9__132_0  // static @ 0x8
METHODS:
  RVA=0x04D489D0  token=0x6000225  System.Void .cctor()
  RVA=0x041E1670  token=0x6000226  System.Void .ctor()
  RVA=0x0B34592C  token=0x6000227  System.Int32 <SortClips>b__132_0(UnityEngine.Timeline.TimelineClip clip1, UnityEngine.Timeline.TimelineClip clip2)
END_CLASS

CLASS: <get_outputs>d__16
TYPE:  sealed class
TOKEN: 0x200004C
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.Playables.PlayableBinding<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x38
  public            UnityEngine.Timeline.AudioPlayableAsset<>4__this  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding>.Current  get=0x02EC7B40
  System.Collections.IEnumerator.Current  get=0x0B345AC8
METHODS:
  RVA=0x03A10F90  token=0x6000239  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600023A  System.Void System.IDisposable.Dispose()
  RVA=0x0B345968  token=0x600023B  System.Boolean MoveNext()
  RVA=0x0B345A7C  token=0x600023D  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B3459E8  token=0x600023F  System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding>.GetEnumerator()
  RVA=0x0B345A74  token=0x6000240  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <get_outputs>d__4
TYPE:  sealed class
TOKEN: 0x200004E
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.Playables.PlayableBinding<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x38
  public            UnityEngine.Timeline.AudioTrack <>4__this  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding>.Current  get=0x02EC7B40
  System.Collections.IEnumerator.Current  get=0x0B345C9C
METHODS:
  RVA=0x03A10F90  token=0x6000246  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000247  System.Void System.IDisposable.Dispose()
  RVA=0x0B345968  token=0x6000248  System.Boolean MoveNext()
  RVA=0x0B345C50  token=0x600024A  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B345BBC  token=0x600024C  System.Collections.Generic.IEnumerator<UnityEngine.Playables.PlayableBinding> System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableBinding>.GetEnumerator()
  RVA=0x0B345C48  token=0x600024D  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: DescriptorBlendTreeType
TYPE:  sealed struct
TOKEN: 0x2000055
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.BlendTreeDescriptor.DescriptorBlendTreeTypeDirect  // const
  public    static  UnityEngine.Timeline.BlendTreeDescriptor.DescriptorBlendTreeTypeSimple1D  // const
  public    static  UnityEngine.Timeline.BlendTreeDescriptor.DescriptorBlendTreeTypeSimple2D  // const
  public    static  UnityEngine.Timeline.BlendTreeDescriptor.DescriptorBlendTreeTypeComplex2D  // const
METHODS:
END_CLASS

CLASS: <GetControlableScripts>d__53
TYPE:  sealed class
TOKEN: 0x200005D
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.MonoBehaviour       <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  private           UnityEngine.GameObject          root  // 0x28
  public            UnityEngine.GameObject          <>3__root  // 0x30
  private           UnityEngine.MonoBehaviour[]     <>7__wrap1  // 0x38
  private           System.Int32                    <>7__wrap2  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.MonoBehaviour>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x600028F  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000290  System.Void System.IDisposable.Dispose()
  RVA=0x0470FB40  token=0x6000291  System.Boolean MoveNext()
  RVA=0x0B3521C8  token=0x6000293  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x049D6A30  token=0x6000295  System.Collections.Generic.IEnumerator<UnityEngine.MonoBehaviour> System.Collections.Generic.IEnumerable<UnityEngine.MonoBehaviour>.GetEnumerator()
  RVA=0x0B3521C0  token=0x6000296  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: TransformAdditive
TYPE:  class
TOKEN: 0x2000060
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3             position  // 0x10
  public            UnityEngine.Vector3             rotationEuler  // 0x1c
  public            UnityEngine.Vector3             scale  // 0x28
METHODS:
  RVA=0x0B3521B8  token=0x60002A0  System.Void .ctor()
  RVA=0x0B352158  token=0x60002A1  System.Void Reset()
END_CLASS

CLASS: TransformAdditive
TYPE:  class
TOKEN: 0x2000062
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3             position  // 0x10
  public            UnityEngine.Vector3             rotationEular  // 0x1c
  public            UnityEngine.Vector3             scale  // 0x28
METHODS:
  RVA=0x0B352158  token=0x60002A7  System.Void Reset()
  RVA=0x041E1670  token=0x60002A8  System.Void .ctor()
END_CLASS

CLASS: AnimationCurveInfo
TYPE:  class
TOKEN: 0x2000065
SIZE:  0x28
FIELDS:
  public            System.String                   path  // 0x10
  public            System.String                   propertyName  // 0x18
  public            UnityEngine.AnimationCurve      curve  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60002B4  System.Void .ctor()
END_CLASS

CLASS: Entry
TYPE:  sealed struct
TOKEN: 0x2000077
FIELDS:
  public            System.Int64                    intervalStart  // 0x0
  public            System.Int64                    intervalEnd  // 0x0
  public            T                               item  // 0x0
METHODS:
END_CLASS

CLASS: EventKeyValue
TYPE:  class
TOKEN: 0x2000085
SIZE:  0x20
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.Timeline.SignalAsset>m_Signals  // 0x10
  private           System.Collections.Generic.List<UnityEngine.Events.UnityEvent>m_Events  // 0x18
PROPERTIES:
  signals  get=0x02B2ECC0
  events  get=0x0385B100
METHODS:
  RVA=0x0B349290  token=0x6000381  System.Boolean TryGetValue(UnityEngine.Timeline.SignalAsset key, UnityEngine.Events.UnityEvent& value)
  RVA=0x0B3490E8  token=0x6000382  System.Void Append(UnityEngine.Timeline.SignalAsset key, UnityEngine.Events.UnityEvent value)
  RVA=0x0B349214  token=0x6000383  System.Void Remove(System.Int32 idx)
  RVA=0x0B349170  token=0x6000384  System.Void Remove(UnityEngine.Timeline.SignalAsset key)
  RVA=0x0B349338  token=0x6000387  System.Void .ctor()
END_CLASS

CLASS: PostPlaybackState
TYPE:  sealed struct
TOKEN: 0x200008C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackStateActive  // const
  public    static  UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackStateInactive  // const
  public    static  UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackStateRevert  // const
METHODS:
END_CLASS

CLASS: InitialState
TYPE:  sealed struct
TOKEN: 0x200008D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.ActivationControlPlayable.InitialStateUnset  // const
  public    static  UnityEngine.Timeline.ActivationControlPlayable.InitialStateActive  // const
  public    static  UnityEngine.Timeline.ActivationControlPlayable.InitialStateInactive  // const
METHODS:
END_CLASS

CLASS: NotificationEntry
TYPE:  sealed struct
TOKEN: 0x2000098
SIZE:  0x28
FIELDS:
  public            System.Double                   time  // 0x10
  public            UnityEngine.Playables.INotificationpayload  // 0x18
  public            System.Boolean                  notificationFired  // 0x20
  public            UnityEngine.Timeline.NotificationFlagsflags  // 0x22
PROPERTIES:
  triggerInEditor  get=0x0AE0AE18
  prewarm  get=0x0B349EA4
  triggerOnce  get=0x0B349EAC
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000099
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Timeline.TimeNotificationBehaviour.<>c<>9  // static @ 0x0
  public    static  System.Comparison<UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry><>9__15_0  // static @ 0x8
METHODS:
  RVA=0x0B3522CC  token=0x60003E9  System.Void .cctor()
  RVA=0x041E1670  token=0x60003EA  System.Void .ctor()
  RVA=0x0B352214  token=0x60003EB  System.Int32 <SortNotifications>b__15_0(UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry x, UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry y)
END_CLASS

CLASS: UseEditMode
TYPE:  sealed struct
TOKEN: 0x20000A9
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.TimeFieldAttribute.UseEditModeNone  // const
  public    static  UnityEngine.Timeline.TimeFieldAttribute.UseEditModeApplyEditMode  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000B3
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Timeline.Extrapolation.<>c<>9  // static @ 0x0
  public    static  System.Comparison<UnityEngine.Timeline.TimelineClip><>9__2_0  // static @ 0x8
METHODS:
  RVA=0x04D47230  token=0x600043C  System.Void .cctor()
  RVA=0x041E1670  token=0x600043D  System.Void .ctor()
  RVA=0x0B34592C  token=0x600043E  System.Int32 <SortClipsByStartTime>b__2_0(UnityEngine.Timeline.TimelineClip clip1, UnityEngine.Timeline.TimelineClip clip2)
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x20000BB
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000459  System.Void .ctor()
  RVA=0x0B352230  token=0x600045A  System.Boolean <GenerateUniqueActorName>b__0(UnityEngine.ScriptableObject x)
END_CLASS

CLASS: <>c__DisplayClass0_1
TYPE:  sealed class
TOKEN: 0x20000BC
SIZE:  0x18
FIELDS:
  public            System.String                   result  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600045B  System.Void .ctor()
  RVA=0x0B352230  token=0x600045C  System.Boolean <GenerateUniqueActorName>b__1(UnityEngine.ScriptableObject x)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000C1
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Timeline.TimeUtility.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x0B352268  token=0x600046A  System.Void .cctor()
  RVA=0x041E1670  token=0x600046B  System.Void .ctor()
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x20000C4
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0687A850  token=0x600046F  System.UInt32 ComputeStringHash(System.String s)
END_CLASS

CLASS: Beyond.Gameplay.Core.DontAutoBindAttribute
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x10
EXTENDS: Beyond.Gameplay.Core.DontAutoBindRuntimeAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000012  System.Void .ctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.DontAutoBindRuntimeAttribute
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000013  System.Void .ctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.BeyondUseAutoBindingPlayableAsset
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x30
EXTENDS: UnityEngine.Playables.PlayableAsset
FIELDS:
  public            System.Boolean                  useAutoBinding  // 0x18
  public            System.String                   autoBindingPath  // 0x20
  protected         UnityEngine.GameObject          m_bindingGo  // 0x28
PROPERTIES:
  showBaseParam  get=0x02FFF600
  bindingGameObject  get=0x04D86240  set=0x02FDB880
METHODS:
  RVA=0x0B33E76C  token=0x6000017  System.Void SetBindingGameObject(UnityEngine.GameObject tBindingGameObject)
  RVA=0x041E1670  token=0x6000018  System.Void OnBindGoChanged()
  RVA=0x0B33E720  token=0x6000019  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner)
  RVA=0x03573370  token=0x600001A  System.Void .ctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.RuntimeJumpBehaviour
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x10
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600001B  System.Void .ctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.RuntimeJumpClip
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x30
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.Timeline.ITimelineClipAsset
FIELDS:
  public            System.Boolean                  isReverseJump  // 0x18
  public            System.Boolean                  needChangeOptionAfterJump  // 0x19
  public            System.Int32                    optionIndexAfterJump  // 0x1c
  public            System.Double                   crossFadeDurationAfterJump  // 0x20
  public            System.Boolean                  isJumpFirst  // 0x28
PROPERTIES:
  clipCaps  get=0x04DBA570
METHODS:
  RVA=0x0B33F664  token=0x600001D  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner)
  RVA=0x03573370  token=0x600001E  System.Void .ctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.RuntimeJumpTrack
TYPE:  class
TOKEN: 0x2000015
SIZE:  0xC0
EXTENDS: UnityEngine.Timeline.TrackAsset
FIELDS:
METHODS:
  RVA=0x0B33F6FC  token=0x600001F  System.Void .ctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.RuntimeLoopBehaviour
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x10
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000020  System.Void .ctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.RuntimeLoopClip
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x20
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.Timeline.ITimelineClipAsset
FIELDS:
  public            System.Double                   crossFadeDuration  // 0x18
PROPERTIES:
  clipCaps  get=0x04DB8E50
METHODS:
  RVA=0x0B33F748  token=0x6000022  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner)
  RVA=0x03573370  token=0x6000023  System.Void .ctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.RuntimeLoopTrack
TYPE:  class
TOKEN: 0x2000018
SIZE:  0xC0
EXTENDS: UnityEngine.Timeline.TrackAsset
FIELDS:
METHODS:
  RVA=0x0B33F7E0  token=0x6000024  System.Void .ctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.TimelineNPCLookAtController
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static readonly System.Collections.Generic.HashSet<Beyond.Gameplay.Core.TimelineNPCLookAtController>s_instances  // static @ 0x0
  public            System.Boolean                  m_lookAtDirty  // 0x18
  public            ScriptAnimation.TimelineNPCLookAtTargetlookAtTarget  // 0x20
  public            ScriptAnimation.TimelineNPCLookAtEyeTargetlookAtEyeTarget  // 0x28
  public            ScriptAnimation.TimelineNPCLookAtTargetV2lookAtTargetV2  // 0x30
  public            System.Boolean                  lookAtEnabled  // 0x38
  public            System.Boolean                  lookAtEyeEnabled  // 0x39
PROPERTIES:
  Instances  get=0x0B342314
  lookAtDirty  get=0x04D86340  set=0x04D86350
  lookAtWeight  get=0x0B342B08  set=0x0B3432B8
  lookAtBodyWeight  get=0x0B3425D8  set=0x0B342E0C
  lookAtBlendWeight  get=0x0B342564  set=0x0B342D94
  lookAtOverrideWeight  get=0x0B342888
  lookAtAdditiveWeight  get=0x0B3424E4
  lookAtRoll  get=0x0B342924  set=0x0B3430C8
  lookAtHorizontal  get=0x0B3427D0  set=0x0B343014
  lookAtVertical  get=0x0B342A50  set=0x0B343204
  lookAtEyeWeight  get=0x0B342754  set=0x0B342F9C
  lookAtUseDegree  get=0x0B3429C0  set=0x0B343168
  EyeUseDegree  get=0x0B3422A8  set=0x0B342BA0
  lookAtEyeUp  get=0x0B3426E4  set=0x0B342F24
  lookAtEyeDown  get=0x0B342674  set=0x0B342EAC
  finalPosition  get=0x0B342410  set=0x0B342CB8
  finalEyePosition  get=0x0B342364  set=0x0B342C18
METHODS:
  RVA=0x0B342190  token=0x6000026  System.Void OnEnable()
  RVA=0x0B342120  token=0x6000027  System.Void OnDisable()
  RVA=0x041E1670  token=0x6000046  System.Void MarkLookAtDirty()
  RVA=0x0B34228C  token=0x6000047  System.Void .ctor()
  RVA=0x0B342200  token=0x6000048  System.Void .cctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600000E  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600000F  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000010  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000011  System.Void .ctor()
END_CLASS

CLASS: Events.INotificationTrack
TYPE:  interface
TOKEN: 0x2000009
FIELDS:
METHODS:
END_CLASS

CLASS: Events.ITimelineNotification
TYPE:  interface
TOKEN: 0x200000A
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600000D  System.Void OnNotify(UnityEngine.Timeline.ITimelineRootMarker root)
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: ScriptAnimation.TimelineNPCClothWeightController
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Boolean                  forceSetPhysicsClothWeight  // 0x18
  public            System.Single                   physicsClothWeight  // 0x1c
  public            System.Boolean                  forceSetPhysicsClothPart1Weight  // 0x20
  public            System.Single                   physicsClothPart1Weight  // 0x24
  public            System.Boolean                  forceSetPhysicsClothPart2Weight  // 0x28
  public            System.Single                   physicsClothPart2Weight  // 0x2c
  public            System.Boolean                  forceSetLoopBodyAddWeight  // 0x30
  public            System.Single                   loopBodyAddWeight  // 0x34
METHODS:
  RVA=0x0B3420EC  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: ScriptAnimation.TimelineNPCLookAtEyeTarget
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Single                   lookAtWeight  // 0x18
  public            System.Boolean                  useDegree  // 0x1c
  public            System.Single                   lookAtUp  // 0x20
  public            System.Single                   lookAtDown  // 0x24
METHODS:
  RVA=0x04D9A6D0  token=0x6000003  System.Void ResetLookAtWeight()
  RVA=0x05393520  token=0x6000004  System.Void .ctor()
END_CLASS

CLASS: ScriptAnimation.TimelineNPCLookAtTarget
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Single                   lookAtWeight  // 0x18
  public            System.Single                   lookAtBodyWeight  // 0x1c
  public            System.Boolean                  useDegree  // 0x20
  public            System.Single                   lookAtHorizontal  // 0x24
  public            System.Single                   lookAtVertical  // 0x28
  public            System.Single                   lookAtRoll  // 0x2c
METHODS:
  RVA=0x0B343350  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: ScriptAnimation.TimelineNPCLookAtTargetV2
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Single                   lookAtBlendWeight  // 0x18
  public            System.Single                   lookAtBodyWeight  // 0x1c
  public            System.Boolean                  useDegree  // 0x20
  public            System.Single                   lookAtHorizontal  // 0x24
  public            System.Single                   lookAtVertical  // 0x28
  public            System.Single                   lookAtRoll  // 0x2c
METHODS:
  RVA=0x0B343350  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: ScriptAnimation.ScriptAnimationApply.ScriptAnimationApplyBehaviour
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x18
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  private   static readonly Unity.Profiling.ProfilerMarker  s_calcLayerMainStreamMarker  // static @ 0x0
  private           UnityEngine.Timeline.IScriptAnimationJobSyncm_JobSync  // 0x10
METHODS:
  RVA=0x0B33F82C  token=0x6000007  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x053908C0  token=0x6000008  System.Void InitBehaviour(UnityEngine.Timeline.IScriptAnimationJobSync jobSync)
  RVA=0x041E1670  token=0x6000009  System.Void .ctor()
  RVA=0x02EC7070  token=0x600000A  System.Void .cctor()
END_CLASS

CLASS: ScriptAnimation.ScriptAnimationApply.ScriptAnimationApplyPlayable
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x18
EXTENDS: UnityEngine.Playables.PlayableAsset
FIELDS:
METHODS:
  RVA=0x0B33F980  token=0x600000B  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner)
  RVA=0x03573370  token=0x600000C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.PlayerLoopChecker
TYPE:  static class
TOKEN: 0x200001A
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.ActivationMixerPlayable
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x38
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  private           UnityEngine.Timeline.ActivationTrack.PostPlaybackStatem_PostPlaybackState  // 0x10
  private           System.Boolean                  m_BoundGameObjectInitialStateIsActive  // 0x14
  private           System.Boolean                  m_useAutoBinding  // 0x15
  private           UnityEngine.Timeline.ITimelineRootMarkerm_RootMarker  // 0x18
  private           System.String                   m_uniqueId  // 0x20
  private           System.Nullable<System.Boolean> m_lastEntityActive  // 0x28
  private           UnityEngine.GameObject          m_BoundGameObject  // 0x30
PROPERTIES:
  postPlaybackState  set=0x014F51F0
METHODS:
  RVA=0x0B33AA78  token=0x6000049  UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.ActivationMixerPlayable> Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount)
  RVA=0x0B33AD24  token=0x600004B  System.Void SetBoundGameObject(UnityEngine.GameObject go)
  RVA=0x0B33ACFC  token=0x600004C  System.Void SetAutoBindingData(System.Boolean useAutoBinding, UnityEngine.Timeline.ITimelineRootMarker rootMarker, System.String uniqueId)
  RVA=0x041E1670  token=0x600004D  System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable)
  RVA=0x0B33AB04  token=0x600004E  System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B33AC1C  token=0x600004F  System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData)
  RVA=0x0B33AB48  token=0x6000050  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B33ADB4  token=0x6000051  System.Void _DoSetActive(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData)
  RVA=0x041E1670  token=0x6000052  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.ActivationPlayableAsset
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x18
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.Timeline.ITimelineClipAsset
FIELDS:
PROPERTIES:
  clipCaps  get=0x011EC580
METHODS:
  RVA=0x0B33B024  token=0x6000054  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go)
  RVA=0x03573370  token=0x6000055  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.ActivationTrack
TYPE:  class
TOKEN: 0x200001D
SIZE:  0xD0
EXTENDS: UnityEngine.Timeline.TrackAsset
FIELDS:
  private           UnityEngine.Timeline.ActivationTrack.PostPlaybackStatem_PostPlaybackState  // 0xc0
  private           UnityEngine.Timeline.ActivationMixerPlayablem_ActivationMixer  // 0xc8
PROPERTIES:
  postPlaybackState  get=0x04DA4B70  set=0x0B33B540
METHODS:
  RVA=0x0B33B090  token=0x6000056  System.Boolean CanCompileClips()
  RVA=0x0B33B0C0  token=0x6000059  UnityEngine.Playables.Playable CreateTrackMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount)
  RVA=0x0B33B498  token=0x600005A  System.Void _BindUniqueId(UnityEngine.Playables.PlayableGraph graph)
  RVA=0x0B33B32C  token=0x600005B  UnityEngine.GameObject GetBinding(UnityEngine.Playables.PlayableDirector director)
  RVA=0x0B33B47C  token=0x600005C  System.Void UpdateTrackMode()
  RVA=0x0B33B28C  token=0x600005D  System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver)
  RVA=0x0B33B42C  token=0x600005E  System.Void OnCreateClip(UnityEngine.Timeline.TimelineClip clip)
  RVA=0x0B33B4EC  token=0x600005F  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.AnimationOutputWeightProcessor
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x30
IMPLEMENTS: UnityEngine.Timeline.ITimelineEvaluateCallback
FIELDS:
  private           UnityEngine.Animator            m_outputAnimator  // 0x10
  private           UnityEngine.Animations.AnimationPlayableOutputm_Output  // 0x18
  private   readonly System.Collections.Generic.List<UnityEngine.Timeline.AnimationOutputWeightProcessor.WeightInfo>m_Mixers  // 0x28
METHODS:
  RVA=0x03E117C0  token=0x6000060  System.Void .ctor(UnityEngine.Animations.AnimationPlayableOutput output, UnityEngine.Animator animator)
  RVA=0x03E10EA0  token=0x6000061  System.Void FindMixers()
  RVA=0x03E0FE10  token=0x6000062  System.Void FindMixers(UnityEngine.Playables.Playable parent, System.Int32 port, UnityEngine.Playables.Playable node)
  RVA=0x03E118A0  token=0x6000063  System.Void Evaluate()
END_CLASS

CLASS: UnityEngine.Timeline.AnimationPlayableAsset
TYPE:  class
TOKEN: 0x2000021
SIZE:  0xB8
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.Timeline.ITimelineClipAsset UnityEngine.Timeline.IPropertyPreview UnityEngine.Timeline.ICustomControlBehaviour UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           UnityEngine.AnimationClip       m_Clip  // 0x18
  private           UnityEngine.Vector3             m_Position  // 0x20
  private           UnityEngine.Vector3             m_EulerAngles  // 0x2c
  private           System.Boolean                  m_UseTrackMatchFields  // 0x38
  private           UnityEngine.Timeline.MatchTargetFieldsm_MatchTargetFields  // 0x3c
  private           System.Boolean                  m_RemoveStartOffset  // 0x40
  private           System.Boolean                  m_ApplyFootIK  // 0x41
  private           UnityEngine.Timeline.AnimationPlayableAsset.LoopModem_Loop  // 0x44
  private           System.Boolean                  m_IgniteOnce  // 0x48
  private           System.Boolean                  m_OnlyDeltaTime  // 0x49
  private           System.Boolean                  m_DynamicLink  // 0x4a
  private           UnityEngine.Timeline.AnimationPlayableAsset.ClothResetOptionm_ClothResetOption  // 0x4c
  private           UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerModem_AnimationEventTriggerMode  // 0x50
  private           System.Single                   m_AnimationEventTriggerWeightThreshold  // 0x54
  private           UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerModem_CustomFootStepTriggerMode  // 0x58
  private           System.Single                   m_CustomFootStepTriggerWeightThreshold  // 0x5c
  private           UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviourm_ClipEndAudioBehaviour  // 0x60
  private           System.Int32                    m_ClipEndAudioFadeOutTimeMs  // 0x64
  private           System.Boolean                  m_MixWitherAnotherClip  // 0x68
  private           System.Single                   m_MainClipMixWeight  // 0x6c
  private           UnityEngine.AnimationClip       m_AnotherClip  // 0x70
  private           UnityEngine.AnimationClip       m_blendShapeControlClip  // 0x78
  private           System.Boolean                  m_UseTimeRemapCurve  // 0x80
  private           UnityEngine.AnimationCurve      m_TimeRemapCurve  // 0x88
  private           System.Boolean                  <useAnotherClipTimeAdapter>k__BackingField  // 0x90
  public            System.Boolean                  DisableBlendShapeControl  // 0x91
  private           UnityEngine.Timeline.AppliedOffsetMode<appliedOffsetMode>k__BackingField  // 0x94
  private           UnityEngine.Timeline.ITimelineAnimationEventContextBinding<TimelineAnimationEventContextBinding>k__BackingField  // 0x98
  private   static readonly System.Int32                    k_LatestVersion  // static @ 0x0
  private           System.Int32                    m_Version  // 0xa0
  private           UnityEngine.Quaternion          m_Rotation  // 0xa4
PROPERTIES:
  useAnotherClipTimeAdapter  get=0x02EC6590  set=0x02EC65A0
  position  get=0x04D90830  set=0x04D90820
  rotation  get=0x0B33BE88  set=0x0B33BF94
  eulerAngles  get=0x04D90540  set=0x04D90510
  useTrackMatchFields  get=0x011F2A70  set=0x011F2A80
  matchTargetFields  get=0x04D86640  set=0x04D86670
  removeStartOffset  get=0x02D480A0  set=0x02D480B0
  applyFootIK  get=0x04D86F40  set=0x04D86F60
  loop  get=0x04D86530  set=0x04D86580
  IgniteOnce  get=0x04D867D0  set=0x04D86810
  OnlyDeltaTime  get=0x04D867E0  set=0x04D86820
  DynamicLink  get=0x04D86F10  set=0x04D86F20
  MixWithAnotherClip  get=0x04D86B40  set=0x04D86BA0
  AnotherClip  get=0x04D862B0  set=0x063AD760
  BlendShapeControlClip  get=0x04D862A0  set=0x06402320
  useTimeRemapCurve  get=0x04D8D1F0  set=0x04D8D200
  timeRemapCurve  get=0x04D861F0  set=0x05395D30
  MainClipMixWeight  get=0x04DA3230  set=0x04DA3330
  hasRootTransforms  get=0x03FA5BC0
  appliedOffsetMode  get=0x04D86B20  set=0x04D86B80
  TimelineAnimationEventContextBinding  get=0x04D85EE0  set=0x06352D0C
  clip  get=0x0385B100  set=0x0B33BEC4
  duration  get=0x0B33BDB0
  clothResetOption  get=0x04D86600  set=0x04D86630
  animationEventTriggerMode  get=0x04D86550  set=0x04D865A0
  animationEventTriggerWeightThreshold  get=0x04D86E00  set=0x04D86E10
  customFootStepTriggerMode  get=0x04D86570  set=0x04D865D0
  customFootStepTriggerWeightThreshold  get=0x04D91500  set=0x04D91510
  clipEndAudioBehaviour  get=0x04D86790  set=0x04D86D50
  clipEndAudioFadeOutTimeMs  get=0x04D88110  set=0x04D88120
  outputs  get=0x0B33BE18
  clipCaps  get=0x039F6DB0
METHODS:
  RVA=0x0B33B8C4  token=0x6000086  System.Single EvaluateTimeRemap(System.Single normalizedTime)
  RVA=0x03FC5360  token=0x60000A0  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go)
  RVA=0x0B33B5AC  token=0x60000A1  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.AnimationClip clip, UnityEngine.Vector3 positionOffset, UnityEngine.Vector3 eulerOffset, System.Boolean removeStartOffset, UnityEngine.Timeline.AppliedOffsetMode mode, System.Boolean applyFootIK, UnityEngine.Timeline.AnimationPlayableAsset.LoopMode loop)
  RVA=0x0315BC80  token=0x60000A2  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Timeline.AnimationPlayableAsset playableAsset)
  RVA=0x04CDB250  token=0x60000A3  UnityEngine.Animations.AnimationClipPlayable CreateAnimationClipPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.AnimationClip clip, System.Boolean removeStartOffset, System.Boolean applyFootIK, UnityEngine.Timeline.AnimationPlayableAsset.LoopMode loop)
  RVA=0x03FA56B0  token=0x60000A4  System.Boolean ShouldApplyOffset(UnityEngine.Timeline.AppliedOffsetMode mode, UnityEngine.AnimationClip clip)
  RVA=0x04D2D300  token=0x60000A5  System.Boolean ShouldApplyEventControl(UnityEngine.Timeline.AnimationPlayableAsset playableAsset)
  RVA=0x0B33BB38  token=0x60000A6  UnityEngine.Playables.Playable TryApplyAnimationEventControl(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable root, UnityEngine.Timeline.AnimationPlayableAsset playableAsset)
  RVA=0x04D7F830  token=0x60000A7  System.Boolean ShouldApplyScaleRemove(UnityEngine.Timeline.AppliedOffsetMode mode)
  RVA=0x0B33BB14  token=0x60000A9  System.Void ResetOffsets()
  RVA=0x0B33B8FC  token=0x60000AA  System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver)
  RVA=0x03FA5C80  token=0x60000AB  System.Boolean HasRootTransforms(UnityEngine.AnimationClip clip)
  RVA=0x0B33B58C  token=0x60000AC  UnityEngine.AnimationEvent[] CollectAnimationEvents()
  RVA=0x041E1670  token=0x60000AD  System.Void OnClipEnable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B33B94C  token=0x60000AE  System.Void OnClipDisable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B33BD54  token=0x60000AF  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x04A05920  token=0x60000B0  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x0B33BB04  token=0x60000B1  System.Void OnUpgradeFromVersion(System.Int32 oldVersion)
  RVA=0x039F4BC0  token=0x60000B2  System.Void .ctor()
  RVA=0x04D7AE50  token=0x60000B3  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.AnotherClipTimeAdapterBehaviour
TYPE:  class
TOKEN: 0x2000029
SIZE:  0x30
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  private           UnityEngine.Playables.Playable  m_AnotherClipPlayable  // 0x10
  private           System.Boolean                  m_Initialized  // 0x20
  private           System.Double                   m_Time  // 0x28
METHODS:
  RVA=0x02EC65B0  token=0x60000BD  System.Void Initialize(UnityEngine.Playables.Playable anotherClipPlayable)
  RVA=0x02EC65D0  token=0x60000BE  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x041E1670  token=0x60000BF  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.AnimationPreviewUpdateCallback
TYPE:  class
TOKEN: 0x200002A
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.MatchTargetFields
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.MatchTargetFieldsPositionX  // const
  public    static  UnityEngine.Timeline.MatchTargetFieldsPositionY  // const
  public    static  UnityEngine.Timeline.MatchTargetFieldsPositionZ  // const
  public    static  UnityEngine.Timeline.MatchTargetFieldsRotationX  // const
  public    static  UnityEngine.Timeline.MatchTargetFieldsRotationY  // const
  public    static  UnityEngine.Timeline.MatchTargetFieldsRotationZ  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.TrackOffset
TYPE:  sealed struct
TOKEN: 0x200002C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.TrackOffsetApplyTransformOffsets  // const
  public    static  UnityEngine.Timeline.TrackOffsetApplySceneOffsets  // const
  public    static  UnityEngine.Timeline.TrackOffsetAuto  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.AppliedOffsetMode
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.AppliedOffsetModeNoRootTransform  // const
  public    static  UnityEngine.Timeline.AppliedOffsetModeTransformOffset  // const
  public    static  UnityEngine.Timeline.AppliedOffsetModeSceneOffset  // const
  public    static  UnityEngine.Timeline.AppliedOffsetModeTransformOffsetLegacy  // const
  public    static  UnityEngine.Timeline.AppliedOffsetModeSceneOffsetLegacy  // const
  public    static  UnityEngine.Timeline.AppliedOffsetModeSceneOffsetEditor  // const
  public    static  UnityEngine.Timeline.AppliedOffsetModeSceneOffsetLegacyEditor  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.MatchTargetFieldConstants
TYPE:  static class
TOKEN: 0x200002E
SIZE:  0x10
FIELDS:
  public    static  UnityEngine.Timeline.MatchTargetFieldsAll  // static @ 0x0
  public    static  UnityEngine.Timeline.MatchTargetFieldsNone  // static @ 0x4
  public    static  UnityEngine.Timeline.MatchTargetFieldsPosition  // static @ 0x8
  public    static  UnityEngine.Timeline.MatchTargetFieldsRotation  // static @ 0xc
METHODS:
  RVA=0x04D35AB0  token=0x60000C0  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.AnimationTrack
TYPE:  class
TOKEN: 0x200002F
SIZE:  0x188
EXTENDS: UnityEngine.Timeline.TrackAsset
IMPLEMENTS: UnityEngine.Timeline.ILayerable
FIELDS:
  private   static  System.String                   k_DefaultInfiniteClipName  // const
  private   static  System.String                   k_DefaultRecordableClipName  // const
  private           UnityEngine.Timeline.TimelineClip.ClipExtrapolationm_InfiniteClipPreExtrapolation  // 0xc0
  private           UnityEngine.Timeline.TimelineClip.ClipExtrapolationm_InfiniteClipPostExtrapolation  // 0xc4
  private           UnityEngine.Vector3             m_InfiniteClipOffsetPosition  // 0xc8
  private           UnityEngine.Vector3             m_InfiniteClipOffsetEulerAngles  // 0xd4
  private           System.Double                   m_InfiniteClipTimeOffset  // 0xe0
  private           System.Boolean                  m_InfiniteClipRemoveOffset  // 0xe8
  private           System.Boolean                  m_InfiniteClipApplyFootIK  // 0xe9
  private           UnityEngine.Timeline.AnimationPlayableAsset.LoopModemInfiniteClipLoop  // 0xec
  private           UnityEngine.Timeline.MatchTargetFieldsm_MatchTargetFields  // 0xf0
  private           UnityEngine.Vector3             m_Position  // 0xf4
  private           UnityEngine.Vector3             m_EulerAngles  // 0x100
  private           UnityEngine.AvatarMask          m_AvatarMask  // 0x110
  private           System.Boolean                  m_ApplyAvatarMask  // 0x118
  private           System.Boolean                  m_DisableFloatCurveOnMask  // 0x119
  private           UnityEngine.Timeline.TrackOffsetm_TrackOffset  // 0x11c
  private           UnityEngine.AnimationClip       m_InfiniteClip  // 0x120
  private           System.Boolean                  m_IsAdditive  // 0x128
  private           System.Boolean                  m_IsAdditiveToLast  // 0x129
  public            System.Boolean                  muteScriptAnimAddon  // 0x12a
  private           System.Boolean                  m_UseScriptAnimAddon  // 0x12b
  private           System.Boolean                  m_useTPoseAsDefaultPose  // 0x12c
  private           UnityEngine.AnimationClip       m_TPoseClip  // 0x130
  private           UnityEngine.ScriptableObject    m_TimelineNPCDescriptor  // 0x138
  private           System.String                   m_LookAtTargetPath  // 0x140
  private           System.Boolean                  m_LookAtUseAdditive  // 0x148
  private           System.Boolean                  m_UseLookAtV2  // 0x149
  private           System.Boolean                  m_WriteTransformDefAsPostProcess  // 0x14a
  private           UnityEngine.Transform           m_LookAtTarget  // 0x150
  private           System.Collections.Generic.HashSet<UnityEngine.Timeline.RuntimeElement>m_allRuntimeClipUnderTrack  // 0x158
  private   static readonly System.Collections.Generic.Queue<UnityEngine.Transform>s_CachedQueue  // static @ 0x0
  private           UnityEngine.Quaternion          m_OpenClipOffsetRotation  // 0x160
  private           UnityEngine.Quaternion          m_Rotation  // 0x170
  private           System.Boolean                  m_ApplyOffsets  // 0x180
PROPERTIES:
  position  get=0x04DBA4A0  set=0x04DBA550
  rotation  get=0x0B33D7C4  set=0x0B33D8B0
  eulerAngles  get=0x04DBA430  set=0x04DBA4C0
  applyOffsets  get=0x012081B0  set=0x041E1670
  trackOffset  get=0x04DB9B70  set=0x04DB9C20
  matchTargetFields  get=0x04D885B0  set=0x0B33D834
  infiniteClip  get=0x04D8DC70  set=0x053971C0
  infiniteClipRemoveOffset  get=0x04D8C8C0  set=0x04D8C8F0
  avatarMask  get=0x04D8D1B0  set=0x05397170
  applyAvatarMask  get=0x04D92E30  set=0x04D92E40
  disableFloatCurveOnMask  get=0x04D8E510  set=0x04D8E540
  isUsingScriptAnimAddon  get=0x04DBA490  set=0x04DBA540
  lookAtUseAdditive  get=0x04D8FE60  set=0x04D8FE90
  useLookAtV2  get=0x04D8FE50  set=0x04D8FE80
  outputs  get=0x04B87C60
  inClipMode  get=0x039F4E60
  infiniteClipOffsetPosition  get=0x04DBA470  set=0x04DBA510
  infiniteClipOffsetRotation  get=0x0B33D75C  set=0x0B33D804
  infiniteClipOffsetEulerAngles  get=0x04DBA450  set=0x04DBA4F0
  infiniteClipApplyFootIK  get=0x04DAA0F0  set=0x04DBA4E0
  infiniteClipTimeOffset  get=0x04DA03A0  set=0x04DBA530
  infiniteClipPreExtrapolation  get=0x04DA4B70  set=0x04DA5DC0
  infiniteClipPostExtrapolation  get=0x04D9DF00  set=0x04D9DFC0
  infiniteClipLoop  get=0x04D9DF40  set=0x04D9E020
  openClipOffsetPosition  get=0x04DBA470  set=0x04DBA510
  openClipOffsetRotation  get=0x0B33D79C  set=0x0B33D890
  openClipOffsetEulerAngles  get=0x04DBA450  set=0x04DBA4F0
  openClipPreExtrapolation  get=0x04DA4B70  set=0x04DA5DC0
  openClipPostExtrapolation  get=0x04D9DF00  set=0x04D9DFC0
METHODS:
  RVA=0x0B33D584  token=0x60000DD  System.Boolean IsControlByThisTrack(UnityEngine.Timeline.RuntimeElement element)
  RVA=0x04B30E40  token=0x60000DE  System.Boolean CanCompileClips()
  RVA=0x0B33D658  token=0x60000F1  System.Void ResetOffsets()
  RVA=0x0B33CA00  token=0x60000F2  UnityEngine.Timeline.TimelineClip CreateClip(UnityEngine.AnimationClip clip)
  RVA=0x0B33CA94  token=0x60000F3  System.Void CreateInfiniteClip(System.String infiniteClipName)
  RVA=0x0B33CEE8  token=0x60000F4  UnityEngine.Timeline.TimelineClip CreateRecordableClip(System.String animClipName)
  RVA=0x0B33D5E0  token=0x60000F5  System.Void OnCreateClip(UnityEngine.Timeline.TimelineClip clip)
  RVA=0x04970AF0  token=0x60000F6  System.Int32 CalculateItemsHash()
  RVA=0x041E1670  token=0x60000F7  System.Void UpdateClipOffsets()
  RVA=0x03FC53E0  token=0x60000F8  UnityEngine.Playables.Playable _CreatePlayableFromClip(UnityEngine.Timeline.TimelineClip c, UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, UnityEngine.Timeline.AppliedOffsetMode mode, UnityEngine.Timeline.ITimelineAnimationEventContextBinding timelineAnimationEventContextBinding)
  RVA=0x03FA6280  token=0x60000F9  UnityEngine.Playables.Playable CompileTrackPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Timeline.AnimationTrack track, UnityEngine.GameObject go, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree, UnityEngine.Timeline.AppliedOffsetMode mode, UnityEngine.Timeline.ITimelineAnimationEventContextBinding timelineAnimationEventContextBinding)
  RVA=0x0B33D688  token=0x60000FA  System.Boolean ShouldLinkWithMainTrackMixer(UnityEngine.Timeline.AnimationTrack animationTrack)
  RVA=0x0B33C0CC  token=0x60000FB  UnityEngine.Playables.Playable ApplyScriptAnimationPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Animations.AnimationLayerMixerPlayable mixer, UnityEngine.GameObject go)
  RVA=0x0B33D70C  token=0x60000FC  UnityEngine.Playables.Playable UnityEngine.Timeline.ILayerable.CreateLayerMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount)
  RVA=0x03FA7330  token=0x60000FD  UnityEngine.Playables.Playable CreateMixerPlayableGraph(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree)
  RVA=0x041E1670  token=0x60000FE  System.Void AttachCustomBlend(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Animations.AnimationLayerMixerPlayable layerMixer)
  RVA=0x04A1F7B0  token=0x60000FF  System.Void AttachLastAdditiveClip(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Animations.AnimationLayerMixerPlayable layerMixer, UnityEngine.GameObject go, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree, System.Collections.Generic.List<UnityEngine.Timeline.AnimationTrack> flattenTracks, UnityEngine.Timeline.ITimelineAnimationEventContextBinding timelineAnimationEventContextBinding)
  RVA=0x04D85B80  token=0x6000100  System.Int32 GetDefaultBlendCount()
  RVA=0x041E1670  token=0x6000101  System.Void AttachDefaultBlend(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Animations.AnimationLayerMixerPlayable mixer, System.Boolean requireOffset, UnityEngine.Animator animator)
  RVA=0x0B33C8C8  token=0x6000102  UnityEngine.Playables.Playable AttachOffsetPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable playable, UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x03FA91A0  token=0x6000103  System.Boolean RequiresMotionXPlayable(UnityEngine.Timeline.AppliedOffsetMode mode, UnityEngine.GameObject gameObject)
  RVA=0x03FA6270  token=0x6000104  System.Boolean UsesAbsoluteMotion(UnityEngine.Timeline.AppliedOffsetMode mode)
  RVA=0x0B33D4A4  token=0x6000105  System.Boolean HasController(UnityEngine.GameObject gameObject)
  RVA=0x03FA6B90  token=0x6000106  UnityEngine.Animator GetBinding(UnityEngine.Playables.PlayableDirector director)
  RVA=0x03FA7170  token=0x6000107  UnityEngine.Animations.AnimationLayerMixerPlayable CreateGroupMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount)
  RVA=0x0B33CB80  token=0x6000108  UnityEngine.Playables.Playable CreateInfiniteTrackPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree, UnityEngine.Timeline.AppliedOffsetMode mode)
  RVA=0x0315B9B0  token=0x6000109  UnityEngine.Playables.Playable ApplyTrackOffset(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable root, UnityEngine.GameObject go, UnityEngine.Timeline.AppliedOffsetMode mode)
  RVA=0x03FA5F20  token=0x600010A  System.Void _AddClipIntoTree(UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree, UnityEngine.Timeline.RuntimeClip clip)
  RVA=0x04D38A60  token=0x600010B  System.Void GetEvaluationTime(System.Double& outStart, System.Double& outDuration)
  RVA=0x04864F30  token=0x600010C  System.Void GetSequenceTime(System.Double& outStart, System.Double& outDuration)
  RVA=0x0B33C6E4  token=0x600010D  System.Void AssignAnimationClip(UnityEngine.Timeline.TimelineClip clip, UnityEngine.AnimationClip animClip)
  RVA=0x041E1670  token=0x600010E  System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver)
  RVA=0x0B33D1B4  token=0x600010F  System.Void GetAnimationClips(System.Collections.Generic.List<UnityEngine.AnimationClip> animClips)
  RVA=0x03FA9170  token=0x6000110  UnityEngine.Timeline.AppliedOffsetMode GetOffsetMode(UnityEngine.GameObject go, System.Boolean animatesRootTransform)
  RVA=0x03FA9210  token=0x6000111  System.Boolean IsRootTransformDisabledByMask(UnityEngine.GameObject gameObject, UnityEngine.Transform genericRootNode)
  RVA=0x03FA6E00  token=0x6000112  UnityEngine.Transform GetGenericRootNode(UnityEngine.GameObject gameObject)
  RVA=0x03FA5720  token=0x6000113  System.Boolean AnimatesRootTransform()
  RVA=0x0B33CFF4  token=0x6000114  UnityEngine.Transform FindInHierarchyBreadthFirst(UnityEngine.Transform t, System.String name)
  RVA=0x0B33D618  token=0x600011F  System.Void OnUpgradeFromVersion(System.Int32 oldVersion)
  RVA=0x039F4CC0  token=0x6000120  System.Void .ctor()
  RVA=0x04D11C90  token=0x6000121  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.ICurvesOwner
TYPE:  interface
TOKEN: 0x2000032
FIELDS:
PROPERTIES:
  defaultCurvesName  get=-1  // abstract
  asset  get=-1  // abstract
  assetOwner  get=-1  // abstract
  targetTrack  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.TimelineClip
TYPE:  class
TOKEN: 0x2000033
SIZE:  0xB8
IMPLEMENTS: UnityEngine.Timeline.ICurvesOwner UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private   static  System.Int32                    k_LatestVersion  // const
  private           System.Int32                    m_Version  // 0x10
  public    static readonly UnityEngine.Timeline.ClipCaps   kDefaultClipCaps  // static @ 0x0
  public    static readonly System.Single                   kDefaultClipDurationInSeconds  // static @ 0x4
  public    static readonly System.Double                   kTimeScaleMin  // static @ 0x8
  public    static readonly System.Double                   kTimeScaleMax  // static @ 0x10
  private   static readonly System.String                   kDefaultCurvesName  // static @ 0x18
  private   static readonly System.Double                   kMinDuration  // static @ 0x20
  private   static readonly System.Double                   kMaxTimeValue  // static @ 0x28
  public            System.Int32                    optionIndex  // 0x14
  private           System.Double                   m_Start  // 0x18
  private           System.Double                   m_ClipIn  // 0x20
  private           UnityEngine.Object              m_Asset  // 0x28
  private           System.Double                   m_Duration  // 0x30
  private           System.Double                   m_TimeScale  // 0x38
  private           UnityEngine.Timeline.TrackAsset m_ParentTrack  // 0x40
  private           System.Double                   m_EaseInDuration  // 0x48
  private           System.Double                   m_EaseOutDuration  // 0x50
  private           System.Double                   m_BlendInDuration  // 0x58
  private           System.Double                   m_BlendOutDuration  // 0x60
  private           UnityEngine.AnimationCurve      m_MixInCurve  // 0x68
  private           UnityEngine.AnimationCurve      m_MixOutCurve  // 0x70
  private           UnityEngine.Timeline.TimelineClip.BlendCurveModem_BlendInCurveMode  // 0x78
  private           UnityEngine.Timeline.TimelineClip.BlendCurveModem_BlendOutCurveMode  // 0x7c
  private           System.Collections.Generic.List<System.String>m_ExposedParameterNames  // 0x80
  private           UnityEngine.AnimationClip       m_AnimationCurves  // 0x88
  private           System.Boolean                  m_Recordable  // 0x90
  private           UnityEngine.Timeline.TimelineClip.ClipExtrapolationm_PostExtrapolationMode  // 0x94
  private           UnityEngine.Timeline.TimelineClip.ClipExtrapolationm_PreExtrapolationMode  // 0x98
  private           System.Double                   m_PostExtrapolationTime  // 0xa0
  private           System.Double                   m_PreExtrapolationTime  // 0xa8
  private           System.String                   m_DisplayName  // 0xb0
PROPERTIES:
  timeScale  get=0x039F6A20
  start  get=0x04D86360  set=0x0B342000
  duration  get=0x04D86A10  set=0x0B341D2C
  end  get=0x04DBA5B0
  clipIn  get=0x039F6B10
  displayName  get=0x04D86250  set=0x04D84CE0
  clipAssetDuration  get=0x0B341BD0
  curves  get=0x04D861F0
  UnityEngine.Timeline.ICurvesOwner.defaultCurvesName  get=0x0B341B00
  asset  get=0x04D86240  set=0x02FDB880
  UnityEngine.Timeline.ICurvesOwner.assetOwner  get=0x04D85A60
  UnityEngine.Timeline.ICurvesOwner.targetTrack  get=0x04D85A60
  easeInDuration  get=0x047A2B30  set=0x0B341DC0
  easeOutDuration  get=0x047A2C40  set=0x0B341EA4
  blendInDuration  get=0x0B341B88
  blendOutDuration  get=0x0B341BAC
  hasBlendIn  get=0x047A2BE0
  hasBlendOut  get=0x047A2CF0
  mixInCurve  get=0x0B341C28  set=0x0485F170
  mixInDuration  get=0x047A2B00
  mixOutCurve  get=0x0B341C90  set=0x063AD760
  mixOutTime  get=0x0B341CF8
  mixOutDuration  get=0x047A2C10
  recordable  get=0x02EC6590  set=0x02EC65A0
  clipCaps  get=0x039F6B40
  postExtrapolationMode  get=0x039F6630  set=0x0B341F88
  preExtrapolationMode  get=0x039F66A0  set=0x0B341FC4
  extrapolatedStart  get=0x04891CD0
  extrapolatedDuration  get=0x039F7450
METHODS:
  RVA=0x0B341B50  token=0x6000131  System.Void UpgradeToLatestVersion()
  RVA=0x0B341B60  token=0x6000132  System.Void .ctor(UnityEngine.Timeline.TrackAsset parent)
  RVA=0x04D85A60  token=0x6000143  UnityEngine.Timeline.TrackAsset GetParentTrack()
  RVA=0x0B3418A4  token=0x6000144  System.Void SetParentTrack_Internal(UnityEngine.Timeline.TrackAsset newParentTrack)
  RVA=0x03AC40C0  token=0x6000157  System.Int32 Hash()
  RVA=0x039F75E0  token=0x6000158  System.Single EvaluateMixOut(System.Double time)
  RVA=0x0B341558  token=0x6000159  System.Single EvaluateMixOutAtGivenTime(System.Double time, System.Double mixOutTime, System.Double mixOutDuration)
  RVA=0x039F76E0  token=0x600015A  System.Single EvaluateMixIn(System.Double time)
  RVA=0x0B341494  token=0x600015B  System.Single EvaluateMixInAtGivenTime(System.Double time, System.Double runtimeMixInTime, System.Double mixInDuration)
  RVA=0x0B34161C  token=0x600015C  UnityEngine.AnimationCurve GetDefaultMixInCurve()
  RVA=0x0B341644  token=0x600015D  UnityEngine.AnimationCurve GetDefaultMixOutCurve()
  RVA=0x039F5D40  token=0x600015E  System.Double ToLocalTime(System.Double time)
  RVA=0x0B3417AC  token=0x600015F  System.Double SanitizeTimeValue(System.Double value, System.Double defaultValue)
  RVA=0x04DBA590  token=0x6000164  System.Void SetPostExtrapolationTime(System.Double time)
  RVA=0x04DBA5A0  token=0x6000165  System.Void SetPreExtrapolationTime(System.Double time)
  RVA=0x039F6660  token=0x6000166  System.Boolean IsPreExtrapolatedTime(System.Double sequenceTime)
  RVA=0x039F65F0  token=0x6000167  System.Boolean IsPostExtrapolatedTime(System.Double sequenceTime)
  RVA=0x0B34166C  token=0x600016A  System.Double GetExtrapolatedTime(System.Double time, UnityEngine.Timeline.TimelineClip.ClipExtrapolation mode, System.Double duration)
  RVA=0x04D89110  token=0x600016B  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x04CC60D0  token=0x600016C  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x0B341984  token=0x600016D  System.String ToString()
  RVA=0x041E1670  token=0x600016E  System.Void UpdateDirty(System.Double oldValue, System.Double newValue)
  RVA=0x04CC6370  token=0x600016F  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.TimelineAsset
TYPE:  class
TOKEN: 0x2000038
SIZE:  0x68
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver UnityEngine.Timeline.ITimelineClipAsset UnityEngine.Timeline.IPropertyPreview
FIELDS:
  private   static  System.Int32                    k_LatestVersion  // const
  private           System.Int32                    m_Version  // 0x18
  private           System.Collections.Generic.List<UnityEngine.ScriptableObject>m_Tracks  // 0x20
  private           System.Double                   m_FixedDuration  // 0x28
  private           UnityEngine.Timeline.TrackAsset[]m_CacheOutputTracks  // 0x30
  private           UnityEngine.Timeline.GroupTrack[]m_CacheGroupTracks  // 0x38
  private           System.Collections.Generic.List<UnityEngine.Timeline.TrackAsset>m_CacheRootTracks  // 0x40
  private           UnityEngine.Timeline.TrackAsset[]m_CacheFlattenedTracks  // 0x48
  private           UnityEngine.Timeline.TimelineAsset.EditorSettingsm_EditorSettings  // 0x50
  private           UnityEngine.Timeline.TimelineAsset.DurationModem_DurationMode  // 0x58
  private           UnityEngine.Timeline.MarkerTrackm_MarkerTrack  // 0x60
PROPERTIES:
  editorSettings  get=0x04D86270
  duration  get=0x039F2D40
  fixedDuration  get=0x0B341310  set=0x0B341400
  durationMode  get=0x04D86570  set=0x04D865D0
  outputs  get=0x048FD6E0
  clipCaps  get=0x0B34105C
  outputTrackCount  get=0x0B341390
  rootTrackCount  get=0x0B3413B8
  flattenedTracks  get=0x039F35B0
  markerTrack  get=0x04D86280
  trackObjects  get=0x04D862C0
METHODS:
  RVA=0x041E1670  token=0x6000171  System.Void UpgradeToLatestVersion()
  RVA=0x0B340F0C  token=0x600017C  System.Void OnValidate()
  RVA=0x0B340A30  token=0x600017D  UnityEngine.Timeline.TrackAsset GetRootTrack(System.Int32 index)
  RVA=0x0B340A8C  token=0x600017E  System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> GetRootTracks()
  RVA=0x0B3409EC  token=0x600017F  UnityEngine.Timeline.TrackAsset GetOutputTrack(System.Int32 index)
  RVA=0x053947BC  token=0x6000180  System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> GetOutputTracks()
  RVA=0x05396C14  token=0x6000181  System.Collections.Generic.IEnumerable<UnityEngine.Timeline.GroupTrack> GetGroupTracks()
  RVA=0x0B340AA8  token=0x6000182  System.Double GetValidFrameRate(System.Double frameRate)
  RVA=0x039F3DE0  token=0x6000183  System.Void UpdateRootTrackCache()
  RVA=0x039F2A60  token=0x6000184  System.Void UpdateOutputTrackCache()
  RVA=0x0B3409E4  token=0x6000186  UnityEngine.Timeline.TrackAsset[] GetAllTracks()
  RVA=0x0B33FA18  token=0x6000189  System.Void AddTrackInternal(UnityEngine.Timeline.TrackAsset track)
  RVA=0x0B340F40  token=0x600018A  System.Void RemoveTrack(UnityEngine.Timeline.TrackAsset track)
  RVA=0x03FC6180  token=0x600018B  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go)
  RVA=0x04DBA580  token=0x600018C  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x039ED100  token=0x600018D  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x045A5340  token=0x600018E  System.Void __internalAwake()
  RVA=0x0B340880  token=0x600018F  System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver)
  RVA=0x0B33FCE4  token=0x6000190  System.Void CreateMarkerTrack()
  RVA=0x039ED6C0  token=0x6000191  System.Void Invalidate()
  RVA=0x0B341000  token=0x6000192  System.Void UpdateFixedDurationWithItemsDuration()
  RVA=0x039F2E40  token=0x6000193  UnityEngine.Timeline.DiscreteTime CalculateItemsDuration()
  RVA=0x039F3730  token=0x6000194  System.Void AddSubTracksRecursive(UnityEngine.Timeline.TrackAsset track, System.Collections.Generic.List<UnityEngine.Timeline.TrackAsset>& allTracks)
  RVA=0x0B33FDB8  token=0x6000195  UnityEngine.Timeline.TrackAsset CreateTrack(System.Type type, UnityEngine.Timeline.TrackAsset parent, System.String name)
  RVA=-1  // generic def  token=0x6000196  T CreateTrack(UnityEngine.Timeline.TrackAsset parent, System.String trackName)
  RVA=-1  // generic def  token=0x6000197  T CreateTrack(System.String trackName)
  RVA=-1  // generic def  token=0x6000198  T CreateTrack()
  RVA=0x0B3400D4  token=0x6000199  System.Boolean DeleteClip(UnityEngine.Timeline.TimelineClip clip)
  RVA=0x0B3404F4  token=0x600019A  System.Boolean DeleteTrack(UnityEngine.Timeline.TrackAsset track)
  RVA=0x0B340B48  token=0x600019B  System.Void MoveLastTrackBefore(UnityEngine.Timeline.TrackAsset asset)
  RVA=0x0B340D04  token=0x600019C  System.Void MoveTrackAfter(UnityEngine.Timeline.TrackAsset trackToMove, UnityEngine.Timeline.TrackAsset targetTrack)
  RVA=0x0B33FA88  token=0x600019D  UnityEngine.Timeline.TrackAsset AllocateTrack(UnityEngine.Timeline.TrackAsset trackAssetParent, System.String trackName, System.Type trackType)
  RVA=0x0B340270  token=0x600019E  System.Void DeleteRecordedAnimation(UnityEngine.Timeline.TrackAsset track)
  RVA=0x0B340374  token=0x600019F  System.Void DeleteRecordedAnimation(UnityEngine.Timeline.TimelineClip clip)
  RVA=0x039F4A90  token=0x60001A0  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.TrackAsset
TYPE:  abstract class
TOKEN: 0x200003F
SIZE:  0xC0
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver UnityEngine.Timeline.IPropertyPreview UnityEngine.Timeline.ICurvesOwner UnityEngine.Timeline.IDynamicBinding
FIELDS:
  private   static  System.Int32                    k_LatestVersion  // const
  private           System.Int32                    m_Version  // 0x18
  private           UnityEngine.AnimationClip       m_AnimClip  // 0x20
  public            System.Boolean                  useAutoBinding  // 0x28
  public            System.String                   autoBindingPath  // 0x30
  public            System.String                   uniqueId  // 0x38
  public            System.Int32                    uiBindingType  // 0x40
  private   static  UnityEngine.Timeline.TrackAsset.TransientBuildDatas_BuildData  // static @ 0x0
  private   static  System.String                   kDefaultCurvesName  // const
  private   static  System.Action<UnityEngine.Timeline.TimelineClip,UnityEngine.GameObject,UnityEngine.Playables.Playable>OnClipPlayableCreate  // static @ 0x18
  private   static  System.Action<UnityEngine.Timeline.TrackAsset,UnityEngine.GameObject,UnityEngine.Playables.Playable>OnTrackAnimationPlayableCreate  // static @ 0x20
  private           System.Boolean                  m_RuntimeMuted  // 0x44
  private           System.Boolean                  m_Locked  // 0x45
  private           System.Boolean                  m_Muted  // 0x46
  private           System.String                   m_CustomPlayableFullTypename  // 0x48
  private           UnityEngine.AnimationClip       m_Curves  // 0x50
  private           UnityEngine.Playables.PlayableAssetm_Parent  // 0x58
  private           System.Collections.Generic.List<UnityEngine.ScriptableObject>m_Children  // 0x60
  private           System.Int32                    m_ItemsHash  // 0x68
  private           UnityEngine.Timeline.TimelineClip[]m_ClipsCache  // 0x70
  private           UnityEngine.Timeline.DiscreteTimem_Start  // 0x78
  private           UnityEngine.Timeline.DiscreteTimem_End  // 0x80
  private           System.Boolean                  m_CacheSorted  // 0x88
  private           System.Nullable<System.Boolean> m_SupportsNotifications  // 0x89
  private   static  UnityEngine.Timeline.TrackAsset[]s_EmptyCache  // static @ 0x28
  private           System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset>m_ChildTrackCache  // 0x90
  private   static  System.Collections.Generic.Dictionary<System.Type,UnityEngine.Timeline.TrackBindingTypeAttribute>s_TrackBindingTypeAttributeCache  // static @ 0x30
  protected internal        System.Collections.Generic.List<UnityEngine.Timeline.TimelineClip>m_Clips  // 0x98
  private           UnityEngine.Timeline.MarkerList m_Markers  // 0xa0
  public            System.Int32                    OptionIndex  // 0xb8
  public            System.Boolean                  useRuntimeMuted  // 0xbc
PROPERTIES:
  start  get=0x0B34574C
  end  get=0x039F2DE0
  duration  get=0x0B3455C0
  muted  get=0x039F34A0  set=0x04D90900
  runtimeMuted  get=0x04D86A30  set=0x04D86A40
  mutedInHierarchy  get=0x039F33B0
  timelineAsset  get=0x039ED500
  parent  get=0x02E56440  set=0x036E5590
  clips  get=0x039F4650
  isEmpty  get=0x0B345624
  hasClips  get=0x0468DAA0
  hasCurves  get=0x03E0E860
  isSubTrack  get=0x039F40B0
  outputs  get=0x03A10EB0
  customPlayableTypename  get=0x04D86200  set=0x035A41A0
  curves  get=0x04D86270  set=0x02FDAEA0
  UnityEngine.Timeline.ICurvesOwner.defaultCurvesName  get=0x0B3451FC
  UnityEngine.Timeline.ICurvesOwner.asset  get=0x0464E630
  UnityEngine.Timeline.ICurvesOwner.assetOwner  get=0x0B3451F4
  UnityEngine.Timeline.ICurvesOwner.targetTrack  get=0x0464E630
  subTracksObjects  get=0x04D86280
  locked  get=0x04D86EF0  set=0x04D86F00
  lockedInHierarchy  get=0x0B345660
  supportsNotifications  get=0x039EDE90
EVENTS:
  OnClipPlayableCreate  add=add_OnClipPlayableCreate  remove=remove_OnClipPlayableCreate
  OnTrackAnimationPlayableCreate  add=add_OnTrackAnimationPlayableCreate  remove=remove_OnTrackAnimationPlayableCreate
METHODS:
  RVA=0x041E1670  token=0x60001AF  System.Void OnBeforeTrackSerialize()
  RVA=0x041E1670  token=0x60001B0  System.Void OnAfterTrackDeserialize()
  RVA=0x041E1670  token=0x60001B1  System.Void OnUpgradeFromVersion(System.Int32 oldVersion)
  RVA=0x0B3450E0  token=0x60001B2  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x039ED110  token=0x60001B3  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x60001B4  System.Void UpgradeToLatestVersion()
  RVA=0x053947D8  token=0x60001C4  System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TimelineClip> GetClips()
  RVA=0x053947E0  token=0x60001CB  System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> GetChildTracks()
  RVA=0x047FDE30  token=0x60001D9  System.Void __internalAwake()
  RVA=0x0B343DAC  token=0x60001DA  System.Void CreateCurves(System.String curvesClipName)
  RVA=0x03E0EB60  token=0x60001DB  UnityEngine.Playables.Playable CreateTrackMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount)
  RVA=0x0B34440C  token=0x60001DC  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go)
  RVA=0x0B343E4C  token=0x60001DD  UnityEngine.Timeline.TimelineClip CreateDefaultClip()
  RVA=-1  // generic def  token=0x60001DE  UnityEngine.Timeline.TimelineClip CreateClip()
  RVA=0x0B34445C  token=0x60001DF  System.Boolean DeleteClip(UnityEngine.Timeline.TimelineClip clip)
  RVA=0x0B3441AC  token=0x60001E0  UnityEngine.Timeline.IMarker CreateMarker(System.Type type, System.Double time)
  RVA=-1  // generic def  token=0x60001E1  T CreateMarker(System.Double time)
  RVA=0x0B3445AC  token=0x60001E2  System.Boolean DeleteMarker(UnityEngine.Timeline.IMarker marker)
  RVA=0x039ED390  token=0x60001E3  System.Collections.Generic.IEnumerable<UnityEngine.Timeline.IMarker> GetMarkers()
  RVA=0x0B344938  token=0x60001E4  System.Collections.Generic.IEnumerable<UnityEngine.Timeline.IMarker> GetClipMarkers()
  RVA=0x0B344C40  token=0x60001E5  System.Int32 GetMarkerCount()
  RVA=0x0B344C50  token=0x60001E6  UnityEngine.Timeline.IMarker GetMarker(System.Int32 idx)
  RVA=0x0B343C90  token=0x60001E7  UnityEngine.Timeline.TimelineClip CreateClip(System.Type requestedType)
  RVA=0x0B3435DC  token=0x60001E8  UnityEngine.Timeline.TimelineClip CreateAndAddNewClipOfType(System.Type requestedType)
  RVA=0x0B343A78  token=0x60001E9  UnityEngine.Timeline.TimelineClip CreateClipOfType(System.Type requestedType)
  RVA=0x0B343834  token=0x60001EA  UnityEngine.Timeline.TimelineClip CreateClipFromPlayableAsset(UnityEngine.Playables.IPlayableAsset asset)
  RVA=0x0B343610  token=0x60001EB  UnityEngine.Timeline.TimelineClip CreateClipFromAsset(UnityEngine.ScriptableObject playableAsset)
  RVA=0x04D86210  token=0x60001EC  System.Collections.Generic.IEnumerable<UnityEngine.ScriptableObject> GetMarkersRaw()
  RVA=0x0B34354C  token=0x60001ED  System.Void ClearMarkers()
  RVA=0x0B3434B0  token=0x60001EE  System.Void AddMarker(UnityEngine.ScriptableObject e)
  RVA=0x0B34456C  token=0x60001EF  System.Boolean DeleteMarkerRaw(UnityEngine.ScriptableObject marker)
  RVA=0x03AC3FA0  token=0x60001F0  System.Int32 GetTimeRangeHash()
  RVA=0x0B34341C  token=0x60001F1  System.Void AddClip(UnityEngine.Timeline.TimelineClip newClip)
  RVA=0x039ECE10  token=0x60001F2  UnityEngine.Playables.Playable CreateNotificationsPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable mixerPlayable, UnityEngine.GameObject go, UnityEngine.Playables.Playable timelinePlayable)
  RVA=0x03433C20  token=0x60001F3  UnityEngine.Playables.Playable CreatePlayableGraph(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree, UnityEngine.Playables.Playable timelinePlayable)
  RVA=0x03E0DAE0  token=0x60001F4  UnityEngine.Playables.Playable CompileClips(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Collections.Generic.IList<UnityEngine.Timeline.TimelineClip> timelineClips, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree)
  RVA=0x04683BA0  token=0x60001F5  System.Void GatherCompilableTracks(System.Collections.Generic.IList<UnityEngine.Timeline.TrackAsset> tracks)
  RVA=0x039EDC30  token=0x60001F6  System.Void GatherNotifications(System.Collections.Generic.List<UnityEngine.Timeline.IMarker> markers)
  RVA=0x039F4170  token=0x60001F7  UnityEngine.Playables.Playable CreateMixerPlayableGraph(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree)
  RVA=0x03E0E7D0  token=0x60001F8  System.Void ConfigureTrackAnimation(UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree, UnityEngine.GameObject go, UnityEngine.Playables.Playable blend)
  RVA=0x039F46F0  token=0x60001F9  System.Void SortClips()
  RVA=0x0B3434C0  token=0x60001FA  System.Void ClearClipsInternal()
  RVA=0x0B34355C  token=0x60001FB  System.Void ClearSubTracksInternal()
  RVA=0x04DA66E0  token=0x60001FC  System.Void OnClipMove()
  RVA=0x0B3441D0  token=0x60001FD  UnityEngine.Timeline.TimelineClip CreateNewClipContainerInternal()
  RVA=0x0B343380  token=0x60001FE  System.Void AddChild(UnityEngine.Timeline.TrackAsset child)
  RVA=0x0B344E40  token=0x60001FF  System.Void MoveLastTrackBefore(UnityEngine.Timeline.TrackAsset asset)
  RVA=0x0B345064  token=0x6000200  System.Boolean RemoveSubTrack(UnityEngine.Timeline.TrackAsset child)
  RVA=0x0B344FFC  token=0x6000201  System.Void RemoveClip(UnityEngine.Timeline.TimelineClip clip)
  RVA=0x039F5F70  token=0x6000202  System.Void GetEvaluationTime(System.Double& outStart, System.Double& outDuration)
  RVA=0x04864FB0  token=0x6000203  System.Void GetSequenceTime(System.Double& outStart, System.Double& outDuration)
  RVA=0x0B3445BC  token=0x6000204  System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver)
  RVA=0x0B344B34  token=0x6000205  UnityEngine.GameObject GetGameObjectBinding(UnityEngine.Playables.PlayableDirector director)
  RVA=0x0B345228  token=0x6000206  System.Boolean ValidateClipType(System.Type clipType)
  RVA=0x041E1670  token=0x6000207  System.Void OnCreateClip(UnityEngine.Timeline.TimelineClip clip)
  RVA=0x039F34C0  token=0x6000208  System.Void UpdateDuration()
  RVA=0x03AC2060  token=0x6000209  System.Int32 CalculateItemsHash()
  RVA=0x03E0DEC0  token=0x600020A  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject gameObject, UnityEngine.Timeline.TimelineClip clip)
  RVA=0x039ED440  token=0x600020B  System.Void Invalidate()
  RVA=0x0B344C60  token=0x600020C  System.Double GetNotificationDuration()
  RVA=0x0468DA80  token=0x600020D  System.Boolean CanCompileClips()
  RVA=0x0B344DB4  token=0x600020E  System.Boolean HasChildTracks()
  RVA=0x05394800  token=0x600020F  System.Boolean CanCreateTrackMixer()
  RVA=0x039F4860  token=0x6000210  System.Boolean IsCompilable()
  RVA=0x045D14E0  token=0x6000211  System.Void UpdateChildTrackCache()
  RVA=0x0B344DFC  token=0x6000212  System.Int32 Hash()
  RVA=0x03AC42F0  token=0x6000213  System.Int32 GetClipsHash()
  RVA=0x03AC2110  token=0x6000214  System.Int32 GetAnimationClipHash(UnityEngine.AnimationClip clip)
  RVA=0x039EC510  token=0x6000215  System.Boolean HasNotifications()
  RVA=0x039EDE60  token=0x6000216  System.Boolean CanCompileNotifications()
  RVA=0x04A28E30  token=0x6000217  System.Boolean CanCreateMixerRecursive()
  RVA=0x0B34406C  token=0x6000218  UnityEngine.Playables.Playable CreateLayerMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount)
  RVA=0x039F3990  token=0x6000219  System.Void .ctor()
  RVA=0x048BAF40  token=0x600021A  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.ForbidScriptDefaultValueWhenExportAttribute
TYPE:  class
TOKEN: 0x2000045
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000228  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.TimelineHelpURLAttribute
TYPE:  class
TOKEN: 0x2000046
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.TrackColorAttribute
TYPE:  class
TOKEN: 0x2000047
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  private           UnityEngine.Color               m_Color  // 0x10
METHODS:
  RVA=0x047F9F60  token=0x6000229  System.Void .ctor(System.Single r, System.Single g, System.Single b)
END_CLASS

CLASS: UnityEngine.Timeline.UseScriptDefaultValueWhenExportAttribute
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600022A  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.AudioClipProperties
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x18
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  public            System.Single                   volume  // 0x10
METHODS:
  RVA=0x04D87330  token=0x600022B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.AudioMixerProperties
TYPE:  class
TOKEN: 0x200004A
SIZE:  0x20
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  public            System.Single                   volume  // 0x10
  public            System.Single                   stereoPan  // 0x14
  public            System.Single                   spatialBlend  // 0x18
METHODS:
  RVA=0x0B33D8E0  token=0x600022C  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x04D87330  token=0x600022D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.AudioPlayableAsset
TYPE:  class
TOKEN: 0x200004B
SIZE:  0x30
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.Timeline.ITimelineClipAsset
FIELDS:
  private           UnityEngine.AudioClip           m_Clip  // 0x18
  private           System.Boolean                  m_Loop  // 0x20
  private           System.Single                   m_bufferingTime  // 0x24
  private           UnityEngine.Timeline.AudioClipPropertiesm_ClipProperties  // 0x28
PROPERTIES:
  bufferingTime  get=0x04D89850  set=0x04D89860
  clip  get=0x0385B100  set=0x05392C40
  loop  get=0x04D866B0  set=0x04D866D0
  duration  get=0x0B33DD04
  outputs  get=0x0B33DDAC
  clipCaps  get=0x0B33DCF4
METHODS:
  RVA=0x0B33DB70  token=0x6000236  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go)
  RVA=0x0B33DC8C  token=0x6000238  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.AudioTrack
TYPE:  class
TOKEN: 0x200004D
SIZE:  0xC8
EXTENDS: UnityEngine.Timeline.TrackAsset
FIELDS:
  private           UnityEngine.Timeline.AudioMixerPropertiesm_TrackProperties  // 0xc0
PROPERTIES:
  outputs  get=0x0B33E550
METHODS:
  RVA=0x0B33E350  token=0x6000241  UnityEngine.Timeline.TimelineClip CreateClip(UnityEngine.AudioClip clip)
  RVA=0x0B33DE1C  token=0x6000242  UnityEngine.Playables.Playable CompileClips(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Collections.Generic.IList<UnityEngine.Timeline.TimelineClip> timelineClips, UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement> tree)
  RVA=0x0B33E464  token=0x6000244  System.Void OnValidate()
  RVA=0x0B33E4D0  token=0x6000245  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.ACConditionDescriptor
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0x28
FIELDS:
  private           System.Int32                    m_ConditionMode  // 0x10
  private           System.String                   m_ConditionEvent  // 0x18
  private           System.Single                   m_EventTreshold  // 0x20
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.ACTransitionDescriptor
TYPE:  class
TOKEN: 0x2000050
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600024E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.DialogTimelineTrackType
TYPE:  sealed struct
TOKEN: 0x2000051
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.DialogTimelineTrackTypeDefault  // const
  public    static  UnityEngine.Timeline.DialogTimelineTrackTypeMorph  // const
  public    static  UnityEngine.Timeline.DialogTimelineTrackTypeAnimation  // const
  public    static  UnityEngine.Timeline.DialogTimelineTrackTypeOffset  // const
  public    static  UnityEngine.Timeline.DialogTimelineTrackTypeLookAt  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.BeyondCommunicator
TYPE:  static class
TOKEN: 0x2000052
SIZE:  0x10
FIELDS:
  public    static  System.Boolean                  setAddonLayerAdditive  // static @ 0x0
  private   static  System.Action<UnityEngine.Animator,UnityEngine.Animations.AnimationLayerMixerPlayable,UnityEngine.Timeline.AnimationTrack,UnityEngine.ScriptableObject,UnityEngine.Transform,UnityEngine.Timeline.CommunicatorParam><ConnectToJobSync>k__BackingField  // static @ 0x8
  private   static  System.Func<UnityEngine.Timeline.ITimelineRootMarker,UnityEngine.GameObject,System.String,System.Boolean,System.Boolean><TrySetTrackBindingEntityVisible>k__BackingField  // static @ 0x10
PROPERTIES:
  ConnectToJobSync  get=0x0B33E5C0  set=0x0B33E660
  TrySetTrackBindingEntityVisible  get=0x0B33E610  set=0x0B33E6C0
METHODS:
  RVA=0x04D7AC90  token=0x6000253  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.BlendMotionDescriptor
TYPE:  class
TOKEN: 0x2000053
SIZE:  0x30
FIELDS:
  public            UnityEngine.AnimationClip       motion  // 0x10
  public            System.String                   poseTimerParam  // 0x18
  public            System.String                   blendParam  // 0x20
  public            System.Single                   threshold  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000254  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.BlendTreeDescriptor
TYPE:  class
TOKEN: 0x2000054
SIZE:  0x30
FIELDS:
  public            UnityEngine.Timeline.BlendTreeDescriptor.DescriptorBlendTreeTypeblendType  // 0x10
  public            System.Collections.Generic.List<UnityEngine.Timeline.BlendMotionDescriptor>motionDescriptorList  // 0x18
  private           System.String                   <speedParameter>k__BackingField  // 0x20
  private           System.Boolean                  <speedParameterActive>k__BackingField  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000255  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.ITimelineRootMarker
TYPE:  interface
TOKEN: 0x2000056
FIELDS:
PROPERTIES:
  topDirector  get=-1  // abstract
  rootGo  get=-1  // abstract
  useEarlyUpdate  get=-1  // abstract
  enableLoop  get=-1  // abstract
  isOptimizingHolding  get=-1  // abstract
  enableTimelineAsyncCompile  get=-1  // abstract
  isTimelineAsyncCompiling  get=-1  // abstract
  isTimelineAsyncCompileComplete  get=-1  // abstract
  onlyEvaluateTopDirector  get=-1  // abstract
  fixedFrameRate  get=-1  // abstract
  manualTickDeltaTime  get=-1  // abstract
  isManualTicking  get=-1  // abstract
  director2ControlPlayable  get=-1  // abstract  set=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000259  System.Void RegisterLoopSegmentRuntimeClip(System.String key, UnityEngine.Timeline.RuntimeClip runtimeClip)
  RVA=-1  // abstract  token=0x600025A  System.Void UpdateAllVFXFollowBoneTools()
  RVA=-1  // abstract  token=0x6000266  System.Void RegisterAsyncScriptAnimationJobSyncInit(UnityEngine.Timeline.IAsyncScriptAnimationJobSyncInit sync)
END_CLASS

CLASS: UnityEngine.Timeline.LayerDescriptor
TYPE:  class
TOKEN: 0x2000057
SIZE:  0x38
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public            System.String                   layerName  // 0x18
  public            UnityEngine.AvatarMask          avatarMask  // 0x20
  public            System.Boolean                  isAdditive  // 0x28
  public            System.Collections.Generic.List<UnityEngine.Timeline.StateDescriptor>stateDescriptors  // 0x30
METHODS:
  RVA=0x03573370  token=0x6000267  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.MotionDescriptor
TYPE:  class
TOKEN: 0x2000058
SIZE:  0x58
FIELDS:
  public            System.String                   stateName  // 0x10
  public            UnityEngine.AnimationClip       motion  // 0x18
  public            System.Single                   speed  // 0x20
  public            System.String                   speedParameter  // 0x28
  public            System.Boolean                  speedParameterActive  // 0x30
  public            System.String                   mirrorParameter  // 0x38
  public            System.Boolean                  mirrorParameterActive  // 0x40
  public            System.String                   timeParameter  // 0x48
  public            System.Boolean                  timeParameterActive  // 0x50
METHODS:
  RVA=0x041E1670  token=0x6000268  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.StateDescriptor
TYPE:  class
TOKEN: 0x2000059
SIZE:  0x40
FIELDS:
  public            System.String                   stateName  // 0x10
  public            System.Boolean                  isDefaultState  // 0x18
  public            System.String                   stateWeightParameter  // 0x20
  public            System.Boolean                  stateWeightParameterActive  // 0x28
  public            System.Boolean                  isBlendTree  // 0x29
  public            UnityEngine.Timeline.MotionDescriptormotionDescriptor  // 0x30
  public            UnityEngine.Timeline.BlendTreeDescriptorblendTreeDescriptor  // 0x38
METHODS:
  RVA=0x041E1670  token=0x6000269  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.ClipCaps
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.ClipCaps   None  // const
  public    static  UnityEngine.Timeline.ClipCaps   Looping  // const
  public    static  UnityEngine.Timeline.ClipCaps   Extrapolation  // const
  public    static  UnityEngine.Timeline.ClipCaps   ClipIn  // const
  public    static  UnityEngine.Timeline.ClipCaps   SpeedMultiplier  // const
  public    static  UnityEngine.Timeline.ClipCaps   Blending  // const
  public    static  UnityEngine.Timeline.ClipCaps   AutoScale  // const
  public    static  UnityEngine.Timeline.ClipCaps   ClipMarker  // const
  public    static  UnityEngine.Timeline.ClipCaps   DialogLoopSegment  // const
  public    static  UnityEngine.Timeline.ClipCaps   IgniteOnce  // const
  public    static  UnityEngine.Timeline.ClipCaps   OnlyDeltaTime  // const
  public    static  UnityEngine.Timeline.ClipCaps   DialogJumpSegment  // const
  public    static  UnityEngine.Timeline.ClipCaps   EvaluateTwiceWhenEnabled  // const
  public    static  UnityEngine.Timeline.ClipCaps   DynamicLink  // const
  public    static  UnityEngine.Timeline.ClipCaps   CustomBehaviourControl  // const
  public    static  UnityEngine.Timeline.ClipCaps   ConditionBlend  // const
  public    static  UnityEngine.Timeline.ClipCaps   All  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.TimelineClipCapsExtensions
TYPE:  static class
TOKEN: 0x200005B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04B074C0  token=0x600026A  System.Boolean SupportsExtrapolation(UnityEngine.Timeline.TimelineClip clip)
  RVA=0x039F6DA0  token=0x600026B  System.Boolean HasAny(UnityEngine.Timeline.ClipCaps caps, UnityEngine.Timeline.ClipCaps flags)
END_CLASS

CLASS: UnityEngine.Timeline.ControlPlayableAsset
TYPE:  class
TOKEN: 0x200005C
SIZE:  0x88
EXTENDS: Beyond.Gameplay.Core.BeyondUseAutoBindingPlayableAsset
IMPLEMENTS: UnityEngine.Timeline.IPropertyPreview UnityEngine.Timeline.ITimelineClipAsset
FIELDS:
  private   static  System.Int32                    k_MaxRandInt  // const
  private   static readonly System.Collections.Generic.List<UnityEngine.Playables.PlayableDirector>k_EmptyDirectorsList  // static @ 0x0
  private   static readonly System.Collections.Generic.List<UnityEngine.ParticleSystem>k_EmptyParticlesList  // static @ 0x8
  private   static readonly System.Collections.Generic.HashSet<UnityEngine.ParticleSystem>s_SubEmitterCollector  // static @ 0x10
  public            UnityEngine.ExposedReference<UnityEngine.GameObject>sourceGameObject  // 0x30
  public            UnityEngine.GameObject          prefabGameObject  // 0x40
  public            System.Boolean                  updateParticle  // 0x48
  public            System.UInt32                   particleRandomSeed  // 0x4c
  public            System.Boolean                  forceRuntimeSimulate  // 0x50
  public            System.Boolean                  updateDirector  // 0x51
  public            System.String                   directorControlPath  // 0x58
  public            System.Boolean                  updateITimeControl  // 0x60
  public            System.Boolean                  searchHierarchy  // 0x61
  public            System.Boolean                  active  // 0x62
  public            UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackStatepostPlayback  // 0x64
  private           System.Boolean                  m_OnlyDeltaTime  // 0x68
  private           System.Boolean                  m_IgniteOnce  // 0x69
  private           UnityEngine.Playables.PlayableAssetm_ControlDirectorAsset  // 0x70
  private           System.Double                   m_Duration  // 0x78
  private           System.Boolean                  m_SupportLoop  // 0x80
  private   static  System.Collections.Generic.HashSet<UnityEngine.Playables.PlayableDirector>s_ProcessedDirectors  // static @ 0x18
  private   static  System.Collections.Generic.HashSet<UnityEngine.GameObject>s_CreatedPrefabs  // static @ 0x20
  private           System.Boolean                  <controllingDirectors>k__BackingField  // 0x81
  private           System.Boolean                  <controllingParticles>k__BackingField  // 0x82
PROPERTIES:
  bindingGameObject  get=0x04A30BD0
  controllingDirectors  get=0x04D8FEE0  set=0x04D8FEF0
  controllingParticles  get=0x04DA5E30  set=0x04DA5E60
  OnlyDeltaTime  get=0x04D86B40  set=0x04D86BA0
  IgniteOnce  get=0x04D86B10  set=0x04D86B70
  duration  get=0x04D86E70
  clipCaps  get=0x039F6D70
METHODS:
  RVA=0x04CEBB80  token=0x6000275  System.Void OnEnable()
  RVA=0x039F0970  token=0x6000278  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go)
  RVA=0x03E0E320  token=0x6000279  UnityEngine.Playables.Playable ConnectPlayablesToMixer(UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.List<UnityEngine.Playables.Playable> playables)
  RVA=0x0450F390  token=0x600027A  System.Void CreateActivationPlayable(UnityEngine.GameObject root, UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.List<UnityEngine.Playables.Playable> outplayables)
  RVA=0x047C3FF0  token=0x600027B  System.Void SearchHierarchyAndConnectParticleSystem(System.Collections.Generic.IEnumerable<UnityEngine.ParticleSystem> particleSystems, System.Single clipIn, System.Single speedMultiplier, UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.List<UnityEngine.Playables.Playable> outplayables)
  RVA=0x0450F680  token=0x600027C  System.Void SearchHierarchyAndConnectDirector(System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableDirector> directors, UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.List<UnityEngine.Playables.Playable> outplayables, System.Boolean disableSelfReferences)
  RVA=0x047DABD0  token=0x600027D  System.Void SearchHierarchyAndConnectControlableScripts(System.Collections.Generic.IEnumerable<UnityEngine.MonoBehaviour> controlableScripts, UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.List<UnityEngine.Playables.Playable> outplayables)
  RVA=0x03E0E520  token=0x600027E  System.Void ConnectMixerAndPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable mixer, UnityEngine.Playables.Playable playable, System.Int32 portIndex)
  RVA=0x04892110  token=0x600027F  UnityEngine.GameObject ResolveSourceGameObject(UnityEngine.IExposedPropertyTable resolver)
  RVA=-1  // generic def  token=0x6000280  System.Collections.Generic.IList<T> GetComponent(UnityEngine.GameObject gameObject)
  RVA=0x049722F0  token=0x6000281  System.Collections.Generic.IList<UnityEngine.Playables.PlayableDirector> GetControllableDirectors(UnityEngine.GameObject gameObject)
  RVA=0x049B3AA0  token=0x6000282  System.Collections.Generic.IEnumerable<UnityEngine.MonoBehaviour> GetControlableScripts(UnityEngine.GameObject root)
  RVA=0x039EFB70  token=0x6000283  System.Void UpdateDurationAndLoopFlag(System.Collections.Generic.IList<UnityEngine.Playables.PlayableDirector> directors, System.Collections.Generic.IList<UnityEngine.ParticleSystem> particleSystems)
  RVA=0x04898B60  token=0x6000284  System.Collections.Generic.IList<UnityEngine.ParticleSystem> GetControllableParticleSystems(UnityEngine.GameObject go)
  RVA=0x0B33EAF8  token=0x6000285  System.Void GetControllableParticleSystems(UnityEngine.Transform t, System.Collections.Generic.ICollection<UnityEngine.ParticleSystem> roots, System.Collections.Generic.HashSet<UnityEngine.ParticleSystem> subEmitters)
  RVA=0x0B33E794  token=0x6000286  System.Void CacheSubEmitters(UnityEngine.ParticleSystem ps, System.Collections.Generic.HashSet<UnityEngine.ParticleSystem> subEmitters)
  RVA=0x0B33E86C  token=0x6000287  System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver)
  RVA=0x0B33EF6C  token=0x6000288  System.Void PreviewParticlesWithControlPlayableAsset(UnityEngine.Timeline.IPropertyCollector driver, System.Collections.Generic.IEnumerable<UnityEngine.ParticleSystem> particles)
  RVA=0x0B33EFC8  token=0x6000289  System.Void PreviewParticles(UnityEngine.Timeline.IPropertyCollector driver, System.Collections.Generic.IEnumerable<UnityEngine.ParticleSystem> particles)
  RVA=0x0B33EC48  token=0x600028A  System.Void PreviewActivation(UnityEngine.Timeline.IPropertyCollector driver, System.Collections.Generic.IEnumerable<UnityEngine.GameObject> objects)
  RVA=0x0B33F16C  token=0x600028B  System.Void PreviewTimeControl(UnityEngine.Timeline.IPropertyCollector driver, UnityEngine.Playables.PlayableDirector director, System.Collections.Generic.IEnumerable<UnityEngine.MonoBehaviour> scripts)
  RVA=0x0B33ED94  token=0x600028C  System.Void PreviewDirectors(UnityEngine.Timeline.IPropertyCollector driver, System.Collections.Generic.IEnumerable<UnityEngine.Playables.PlayableDirector> directors)
  RVA=0x047D9DB0  token=0x600028D  System.Void .ctor()
  RVA=0x049C5970  token=0x600028E  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.ControlTrack
TYPE:  class
TOKEN: 0x200005E
SIZE:  0xC0
EXTENDS: UnityEngine.Timeline.TrackAsset
FIELDS:
METHODS:
  RVA=0x039F4B70  token=0x6000297  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.AdditiveAnimApplyMono
TYPE:  class
TOKEN: 0x200005F
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Timeline.AdditiveAnimApplyMono.TransformAdditive>pendingAdditives  // 0x18
  private           System.Boolean                  hasnewPendingUpdates  // 0x20
  private           UnityEngine.Timeline.ITimelineRootMarker_parentTimelineRoot  // 0x28
METHODS:
  RVA=0x0B346AF0  token=0x6000298  System.Void Awake()
  RVA=0x0B346D2C  token=0x6000299  System.Void RecordAdditiveTransform(UnityEngine.Transform bone, UnityEngine.Vector3 position, UnityEngine.Vector3 rotationEuler, UnityEngine.Vector3 scale)
  RVA=0x0B346CBC  token=0x600029A  System.Void LateUpdate()
  RVA=0x0B34672C  token=0x600029B  System.Void ApplyPendingTransforms()
  RVA=0x0B346B68  token=0x600029C  System.Void ClearPendingTransforms()
  RVA=0x0B346CE8  token=0x600029D  System.Void NotifyCutsceneRootToUpdateVFXTools()
  RVA=0x0B346E88  token=0x600029E  System.Void RegisterRootComponent()
  RVA=0x0B346F08  token=0x600029F  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.AdditiveAnimMixer
TYPE:  class
TOKEN: 0x2000061
SIZE:  0x48
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  public            UnityEngine.Timeline.AdditiveAnimTracktrack  // 0x10
  private           UnityEngine.GameObject          bindingGo  // 0x18
  private           System.Boolean                  thisFrameAlreadyCalcualted  // 0x20
  private           System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Timeline.AdditiveAnimMixer.TransformAdditive>additiveCache  // 0x28
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform>bonePaths  // 0x30
  private           System.Collections.Generic.HashSet<System.String>notFoundBonePaths  // 0x38
  private           UnityEngine.Timeline.AdditiveAnimApplyMonocachedApplyMono  // 0x40
METHODS:
  RVA=0x04D90CE0  token=0x60002A2  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B346F88  token=0x60002A3  System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData)
  RVA=0x0B347338  token=0x60002A4  System.Void _AddToAdditiveCache(UnityEngine.Timeline.AdditiveAnimPlayable additiveAnimPlayable, System.Single clipTime, System.Single weight)
  RVA=0x0B347A88  token=0x60002A5  System.Void _ApplyAdditive()
  RVA=0x0B347D48  token=0x60002A6  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.AdditiveAnimPlayable
TYPE:  class
TOKEN: 0x2000063
SIZE:  0x20
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.Timeline.AdditiveAnimPlayableAsset.AnimationCurveInfo>extractedCurves  // 0x10
  private           System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<UnityEngine.Timeline.AdditiveAnimPlayableAsset.AnimationCurveInfo>>_curveCache  // 0x18
METHODS:
  RVA=0x0B348358  token=0x60002A9  System.Void OnPlayableCreate(UnityEngine.Playables.Playable playable)
  RVA=0x0B3480C8  token=0x60002AA  System.Void InitializeCurveCache()
  RVA=0x0B3483D0  token=0x60002AB  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.AdditiveAnimPlayableAsset
TYPE:  class
TOKEN: 0x2000064
SIZE:  0x48
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.Timeline.ITimelineClipAsset
FIELDS:
  public            UnityEngine.AnimationClip       additiveClip  // 0x18
  private           UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.AdditiveAnimPlayable>additiveAnimPlayable  // 0x20
  public            System.Collections.Generic.List<UnityEngine.Timeline.AdditiveAnimPlayableAsset.AnimationCurveInfo>extractedCurves  // 0x30
  private           System.Boolean                  m_OnlyDeltaTime  // 0x38
  private           System.Boolean                  m_IgniteOnce  // 0x39
  private           UnityEngine.AnimationClip       _lastProcessedClip  // 0x40
PROPERTIES:
  OnlyDeltaTime  get=0x011F2A70  set=0x011F2A80
  IgniteOnce  get=0x04D86650  set=0x04D86680
  clipCaps  get=0x0B3480A4
METHODS:
  RVA=0x0B347E54  token=0x60002B0  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner)
  RVA=0x0B347F80  token=0x60002B1  System.Void _InitializeBehaviourCurveCache()
  RVA=0x0B348024  token=0x60002B3  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.AdditiveAnimTrack
TYPE:  class
TOKEN: 0x2000066
SIZE:  0xD0
EXTENDS: UnityEngine.Timeline.TrackAsset
FIELDS:
  private   static  System.String                   k_DefaultRecordableClipName  // const
  private           System.Boolean                  m_InfiniteClip  // 0xc0
  private           UnityEngine.AnimationClip       m_RecordableClip  // 0xc8
METHODS:
  RVA=0x0B348470  token=0x60002B5  UnityEngine.Playables.Playable CreateTrackMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount)
  RVA=0x0B34856C  token=0x60002B6  System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver)
  RVA=0x0B348448  token=0x60002B7  UnityEngine.Timeline.TimelineClip CreateRecordableClip(System.String animClipName)
  RVA=0x0B348744  token=0x60002B8  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.CombineToTopPlayableAttribute
TYPE:  class
TOKEN: 0x2000067
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002B9  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.CustomControlPlayableAsset
TYPE:  class
TOKEN: 0x2000068
SIZE:  0x20
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.Timeline.ITimelineClipAsset UnityEngine.Timeline.ICustomControlBehaviour
FIELDS:
  protected         UnityEngine.Timeline.CustomControlPlayableBehaviourm_CustomControlBehaviour  // 0x18
PROPERTIES:
  clipCaps  get=0x0B348E50
  extraClipCaps  get=0x011EC580
METHODS:
  RVA=0x0B348D48  token=0x60002BC  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner)
  RVA=0x0B348DF4  token=0x60002BD  System.Void OnClipEnable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B348D98  token=0x60002BE  System.Void OnClipDisable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x03573370  token=0x60002BF  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.CustomControlPlayableBehaviour
TYPE:  class
TOKEN: 0x2000069
SIZE:  0x10
EXTENDS: UnityEngine.Playables.PlayableBehaviour
IMPLEMENTS: UnityEngine.Timeline.ICustomControlBehaviour
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002C0  System.Void OnClipEnable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x041E1670  token=0x60002C1  System.Void OnClipDisable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x041E1670  token=0x60002C2  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.ICustomControlBehaviour
TYPE:  interface
TOKEN: 0x200006A
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60002C3  System.Void OnClipEnable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=-1  // abstract  token=0x60002C4  System.Void OnClipDisable(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
END_CLASS

CLASS: UnityEngine.Timeline.IDynamicBinding
TYPE:  interface
TOKEN: 0x200006B
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.ITimelineAnimationEventContextBinding
TYPE:  interface
TOKEN: 0x200006C
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60002C5  System.Void RegisterAnimationEventRuntimeContext(System.Int32 playableId, UnityEngine.AnimationEvent[] animationEvents, UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode postAudioEventTriggerMode, System.Single postAudioEventTriggerThreshold, UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode customFootStepTriggerMode, System.Single customFootStepTriggerThreshold, System.Single curClipTime, System.Single curWeight, UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviour clipEndAudioBehaviour)
  RVA=-1  // abstract  token=0x60002C6  System.Void UnregisterAnimationEventRuntimeContext(System.Int32 playableId, UnityEngine.AnimationEvent[] animationEvents)
  RVA=-1  // abstract  token=0x60002C7  System.Void UpdateClipInfo(System.Int32 playableId, System.Single curClipTime, System.Single curClipWeight)
  RVA=-1  // abstract  token=0x60002C8  System.Void StopAllAudioForPlayable(System.Int32 playableId, System.Int32 fadeOutTimeMs)
END_CLASS

CLASS: UnityEngine.Timeline.SegmentLoopPlayableBehaviour
TYPE:  class
TOKEN: 0x200006D
SIZE:  0x10
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002C9  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.SegmentLoopPlayableClip
TYPE:  class
TOKEN: 0x200006E
SIZE:  0x20
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.Timeline.ITimelineClipAsset
FIELDS:
  public            System.String                   loopSegmentKey  // 0x18
PROPERTIES:
  clipCaps  get=0x04DB8E50
METHODS:
  RVA=0x0B34BF90  token=0x60002CA  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner)
  RVA=0x03573370  token=0x60002CC  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.SegmentLoopTrack
TYPE:  class
TOKEN: 0x200006F
SIZE:  0xC0
EXTENDS: UnityEngine.Timeline.TrackAsset
FIELDS:
METHODS:
  RVA=0x0B34C03C  token=0x60002CD  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.DiscreteTime
TYPE:  sealed struct
TOKEN: 0x2000070
SIZE:  0x18
IMPLEMENTS: System.IComparable
FIELDS:
  private   static  System.Double                   k_Tick  // const
  public    static readonly UnityEngine.Timeline.DiscreteTimekMaxTime  // static @ 0x0
  private   readonly System.Int64                    m_DiscreteTime  // 0x10
PROPERTIES:
  tickValue  get=0x04DBA600
METHODS:
  RVA=0x04D88B60  token=0x60002CF  System.Void .ctor(System.Int64 time)
  RVA=0x039F3020  token=0x60002D0  System.Void .ctor(System.Double time)
  RVA=0x039F30E0  token=0x60002D1  System.Void .ctor(System.Int32 time)
  RVA=0x04DBA5D0  token=0x60002D2  UnityEngine.Timeline.DiscreteTime OneTickBefore()
  RVA=0x04DBA5C0  token=0x60002D3  UnityEngine.Timeline.DiscreteTime OneTickAfter()
  RVA=0x0464E630  token=0x60002D4  UnityEngine.Timeline.DiscreteTime FromTicks(System.Int64 ticks)
  RVA=0x0B348FC0  token=0x60002D5  System.Int32 CompareTo(System.Object obj)
  RVA=0x02EECF30  token=0x60002D6  System.Boolean Equals(UnityEngine.Timeline.DiscreteTime other)
  RVA=0x0B349028  token=0x60002D7  System.Boolean Equals(System.Object obj)
  RVA=0x039F31A0  token=0x60002D8  System.Int64 DoubleToDiscreteTime(System.Double time)
  RVA=0x039F3140  token=0x60002D9  System.Int64 IntToDiscreteTime(System.Int32 time)
  RVA=0x04DBA5E0  token=0x60002DA  System.Double ToDouble(System.Int64 time)
  RVA=0x039F3080  token=0x60002DB  System.Double op_Explicit(UnityEngine.Timeline.DiscreteTime b)
  RVA=0x039F2FF0  token=0x60002DC  UnityEngine.Timeline.DiscreteTime op_Explicit(System.Double time)
  RVA=0x039F2FC0  token=0x60002DD  UnityEngine.Timeline.DiscreteTime op_Implicit(System.Int32 time)
  RVA=0x02FB1840  token=0x60002DE  System.Boolean op_Equality(UnityEngine.Timeline.DiscreteTime lhs, UnityEngine.Timeline.DiscreteTime rhs)
  RVA=0x0B34909C  token=0x60002DF  System.Boolean op_Inequality(UnityEngine.Timeline.DiscreteTime lhs, UnityEngine.Timeline.DiscreteTime rhs)
  RVA=0x039F3720  token=0x60002E0  System.Boolean op_LessThanOrEqual(UnityEngine.Timeline.DiscreteTime lhs, UnityEngine.Timeline.DiscreteTime rhs)
  RVA=0x02FCE050  token=0x60002E1  System.Boolean op_GreaterThanOrEqual(UnityEngine.Timeline.DiscreteTime lhs, UnityEngine.Timeline.DiscreteTime rhs)
  RVA=0x04DBA610  token=0x60002E2  UnityEngine.Timeline.DiscreteTime op_Subtraction(UnityEngine.Timeline.DiscreteTime lhs, UnityEngine.Timeline.DiscreteTime rhs)
  RVA=0x0B349094  token=0x60002E3  System.String ToString()
  RVA=0x04DA4FD0  token=0x60002E4  System.Int32 GetHashCode()
  RVA=0x039F3210  token=0x60002E5  UnityEngine.Timeline.DiscreteTime Max(UnityEngine.Timeline.DiscreteTime lhs, UnityEngine.Timeline.DiscreteTime rhs)
  RVA=0x039F2A00  token=0x60002E6  System.Int64 GetNearestTick(System.Double time)
  RVA=0x04D75940  token=0x60002E7  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.InfiniteRuntimeClip
TYPE:  class
TOKEN: 0x2000071
SIZE:  0x30
EXTENDS: UnityEngine.Timeline.RuntimeElement
FIELDS:
  private           UnityEngine.Playables.Playable  m_Playable  // 0x18
  private   static readonly System.Int64                    kIntervalEnd  // static @ 0x0
  private           System.Int32                    <optionIndex>k__BackingField  // 0x28
PROPERTIES:
  intervalStart  get=0x011EC580
  intervalEnd  get=0x0B349590
  optionIndex  get=0x011F2A90  set=0x011F2AA0
  enable  set=0x0B3495E0
METHODS:
  RVA=0x04D890D0  token=0x60002E8  System.Void .ctor(UnityEngine.Playables.Playable playable)
  RVA=0x0B3494A8  token=0x60002EE  System.Void EvaluateAt(System.Double localTime, UnityEngine.Playables.FrameData frameData, UnityEngine.Timeline.ITimelineRootMarker timelineRoot)
  RVA=0x0B349448  token=0x60002EF  System.Void DisableAt(System.Double localTime, System.Double rootDuration, UnityEngine.Playables.FrameData frameData)
  RVA=0x0B349500  token=0x60002F0  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.IInterval
TYPE:  interface
TOKEN: 0x2000072
FIELDS:
PROPERTIES:
  intervalStart  get=-1  // abstract
  intervalEnd  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.IRuntimeReset
TYPE:  interface
TOKEN: 0x2000073
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60002F3  System.Void RuntimeReset()
END_CLASS

CLASS: UnityEngine.Timeline.IOption
TYPE:  interface
TOKEN: 0x2000074
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.IntervalTreeNode
TYPE:  sealed struct
TOKEN: 0x2000075
SIZE:  0x28
FIELDS:
  public            System.Int64                    center  // 0x10
  public            System.Int32                    first  // 0x18
  public            System.Int32                    last  // 0x1c
  public            System.Int32                    left  // 0x20
  public            System.Int32                    right  // 0x24
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.IntervalTree`1
TYPE:  class
TOKEN: 0x2000076
FIELDS:
  private   static  System.Int32                    kMinNodeSize  // const
  private   static  System.Int32                    kInvalidNode  // const
  private   static  System.Int64                    kCenterUnknown  // const
  private   readonly System.Collections.Generic.List<UnityEngine.Timeline.IntervalTree.Entry<T>>m_Entries  // 0x0
  private   readonly System.Collections.Generic.List<UnityEngine.Timeline.IntervalTreeNode>m_Nodes  // 0x0
  private           System.Boolean                  <dirty>k__BackingField  // 0x0
PROPERTIES:
  dirty  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60002F6  System.Void Add(T item)
  RVA=-1  // not resolved  token=0x60002F7  System.Void IntersectsWith(System.Int64 value, System.Collections.Generic.List<T> results)
  RVA=-1  // not resolved  token=0x60002F8  System.Void IntersectsWithRange(System.Int64 start, System.Int64 end, System.Collections.Generic.List<T> results)
  RVA=-1  // not resolved  token=0x60002F9  System.Void UpdateIntervals()
  RVA=-1  // not resolved  token=0x60002FA  System.Void RuntimeReset()
  RVA=-1  // not resolved  token=0x60002FB  System.Void Query(UnityEngine.Timeline.IntervalTreeNode intervalTreeNode, System.Int64 value, System.Collections.Generic.List<T> results)
  RVA=-1  // not resolved  token=0x60002FC  System.Void QueryRange(UnityEngine.Timeline.IntervalTreeNode intervalTreeNode, System.Int64 start, System.Int64 end, System.Collections.Generic.List<T> results)
  RVA=-1  // not resolved  token=0x60002FD  System.Void Rebuild()
  RVA=-1  // not resolved  token=0x60002FE  System.Int32 Rebuild(System.Int32 start, System.Int32 end)
  RVA=-1  // not resolved  token=0x60002FF  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.RuntimeClip
TYPE:  class
TOKEN: 0x2000078
SIZE:  0xA8
EXTENDS: UnityEngine.Timeline.RuntimeClipBase
FIELDS:
  public            System.Double                   runtimeLocalTimeOffset  // 0x20
  public            System.Double                   runtimeMixOutTime  // 0x28
  public            System.Double                   runtimeMixOutDuration  // 0x30
  public            System.Double                   runtimeMixInTime  // 0x38
  public            System.Double                   runtimeMixInDuration  // 0x40
  public            System.Boolean                  runtimeLooping  // 0x48
  public            System.Boolean                  runtimeLoopingMute  // 0x49
  private           System.Boolean                  _isInMixIn  // 0x4a
  private           System.Boolean                  _isMixIn  // 0x4b
  private           System.Boolean                  _hadIgniteOnce  // 0x4c
  private           System.Double                   lastTickTime  // 0x50
  private           System.Boolean                  _onlyDeltaTimeLooped  // 0x58
  private           System.Int32                    <optionIndex>k__BackingField  // 0x5c
  private           System.Int32                    m_originalPort  // 0x60
  private           System.Boolean                  _hadClipEnabled  // 0x64
  public            System.Boolean                  isRuntimeJumpExhaust  // 0x65
  private           UnityEngine.Timeline.TimelineClipm_Clip  // 0x68
  private           UnityEngine.Playables.Playable  m_Playable  // 0x70
  private           UnityEngine.Playables.Playable  m_ParentMixer  // 0x80
  private           UnityEngine.Playables.Playable  m_RootPlayable  // 0x90
  private           System.Boolean                  m_enabled  // 0xa0
PROPERTIES:
  optionIndex  get=0x04D86560  set=0x04D865C0
  dynamicLink  get=0x04B09EB0
  customBehaviourControl  get=0x04B09EE0
  start  get=0x04891CA0
  end  get=0x0B34B9B0
  duration  get=0x039F7540
  clip  get=0x04D86230
  mixer  get=0x04D90620
  playable  get=0x04D8FD40
  intervalStart  get=0x045C5CC0
  intervalEnd  get=0x045C5D30
  enable  set=0x039F5E10
  isEnabling  get=0x04D88020
METHODS:
  RVA=0x03E0E630  token=0x6000307  System.Void .ctor(UnityEngine.Timeline.TimelineClip clip, UnityEngine.Playables.Playable clipPlayable, UnityEngine.Playables.Playable parentMixer, System.Int32 originalPort)
  RVA=0x03E0E690  token=0x6000308  System.Void Create(UnityEngine.Timeline.TimelineClip clip, UnityEngine.Playables.Playable clipPlayable, UnityEngine.Playables.Playable parentMixer, System.Int32 originalPort)
  RVA=0x039F7020  token=0x6000310  System.Void SetTime(System.Double time)
  RVA=0x03E0E2C0  token=0x6000311  System.Void SetDuration(System.Double duration)
  RVA=0x039F7080  token=0x6000312  System.Void EvaluateAt(System.Double localTime, UnityEngine.Playables.FrameData frameData, UnityEngine.Timeline.ITimelineRootMarker timelineRoot)
  RVA=0x039F7660  token=0x6000313  System.Single EvaluateMixIn(System.Single localTime)
  RVA=0x039F7570  token=0x6000314  System.Single EvaluateMixOut(System.Single localTime)
  RVA=0x039F72E0  token=0x6000315  System.Double ToLocalTime(System.Double localTime, UnityEngine.Playables.FrameData frameData, UnityEngine.Timeline.ITimelineRootMarker timelineRoot)
  RVA=0x039F73A0  token=0x6000316  System.Double ApplyTimeRemapCurve(System.Double localTime, System.Double clipTime)
  RVA=0x0B34B508  token=0x6000317  System.Void DisableAt(System.Double localTime, System.Double rootDuration, UnityEngine.Playables.FrameData frameData)
  RVA=0x0489D700  token=0x6000318  System.Boolean CheckIfRuntimeTimeLoop(System.Boolean& canSkip, System.Double& reverseTime)
  RVA=0x0B34B3DC  token=0x6000319  System.Boolean CheckIfRuntimeTimeJump(System.Double& jumpTime, System.Boolean& isReverseJump)
  RVA=0x0B34B8B8  token=0x600031A  System.Void RuntimeLoopOver()
  RVA=0x0B34B888  token=0x600031B  System.Void RuntimeLoopOn()
  RVA=0x0B34B6A8  token=0x600031C  System.Void GetRuntimeCrossFadeDuration(System.Double& duration, System.Double& jumpTargetTime)
  RVA=0x0B34B7CC  token=0x600031D  System.Void RecalculateRuntimeCrossFadeMixIn(System.Double mixInTime, System.Double mixInDuration)
  RVA=0x0B34B81C  token=0x600031E  System.Void RecalculateRuntimeCrossFadeMixOut(System.Double curTime, System.Double mixStartTime, System.Double mixDuration)
  RVA=0x0B34B8E8  token=0x600031F  System.Boolean TryGetJumpClip(Beyond.Gameplay.Core.RuntimeJumpClip& jumpClip)
  RVA=0x039F66D0  token=0x6000320  System.Void RuntimeReset()
END_CLASS

CLASS: UnityEngine.Timeline.RuntimeClipBase
TYPE:  abstract class
TOKEN: 0x2000079
SIZE:  0x20
EXTENDS: UnityEngine.Timeline.RuntimeElement
FIELDS:
  private           System.Int32                    <optionIndex>k__BackingField  // 0x18
PROPERTIES:
  start  get=-1  // abstract
  duration  get=-1  // abstract
  end  get=-1  // abstract
  optionIndex  get=0x011EF5B0  set=0x011EF9B0
  intervalStart  get=0x0B34B380
  intervalEnd  get=0x0B34B308
METHODS:
  RVA=0x041E1670  token=0x6000328  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.RuntimeElement
TYPE:  abstract class
TOKEN: 0x200007A
SIZE:  0x18
IMPLEMENTS: UnityEngine.Timeline.IInterval UnityEngine.Timeline.IOption UnityEngine.Timeline.IRuntimeReset
FIELDS:
  private           System.Int32                    <intervalBit>k__BackingField  // 0x10
  private           System.Boolean                  <isLooping>k__BackingField  // 0x14
PROPERTIES:
  intervalStart  get=-1  // abstract
  intervalEnd  get=-1  // abstract
  optionIndex  get=-1  // abstract  set=-1  // abstract
  intervalBit  get=0x02B2E2D0  set=0x014F51F0
  isLooping  get=0x04D8EE50  set=0x04D8EE70
  enable  set=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000332  System.Void EvaluateAt(System.Double localTime, UnityEngine.Playables.FrameData frameData, UnityEngine.Timeline.ITimelineRootMarker timelineRoot)
  RVA=-1  // abstract  token=0x6000333  System.Void DisableAt(System.Double localTime, System.Double rootDuration, UnityEngine.Playables.FrameData frameData)
  RVA=0x04DBA6E0  token=0x6000334  System.Boolean CheckIfRuntimeTimeLoop(System.Boolean& canSkip, System.Double& reverseTime)
  RVA=0x04DBA6D0  token=0x6000335  System.Boolean CheckIfRuntimeTimeJump(System.Double& jumpTime, System.Boolean& isReverseJump)
  RVA=0x041E1670  token=0x6000336  System.Void RuntimeLoopOver()
  RVA=0x041E1670  token=0x6000337  System.Void RuntimeLoopOn()
  RVA=0x04DBA6F0  token=0x6000338  System.Void GetRuntimeCrossFadeDuration(System.Double& duration, System.Double& jumpTargetTime)
  RVA=0x041E1670  token=0x6000339  System.Void RecalculateRuntimeCrossFadeMixIn(System.Double mixInTime, System.Double mixDuration)
  RVA=0x041E1670  token=0x600033A  System.Void RecalculateRuntimeCrossFadeMixOut(System.Double curTime, System.Double mixStartTime, System.Double mixDuration)
  RVA=0x041E1670  token=0x600033B  System.Void RuntimeReset()
  RVA=0x041E1670  token=0x600033C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.ScheduleRuntimeClip
TYPE:  class
TOKEN: 0x200007B
SIZE:  0x60
EXTENDS: UnityEngine.Timeline.RuntimeClipBase
FIELDS:
  private           UnityEngine.Timeline.TimelineClipm_Clip  // 0x20
  private           UnityEngine.Playables.Playable  m_Playable  // 0x28
  private           UnityEngine.Playables.Playable  m_ParentMixer  // 0x38
  private           System.Double                   m_StartDelay  // 0x48
  private           System.Double                   m_FinishTail  // 0x50
  private           System.Boolean                  m_Started  // 0x58
PROPERTIES:
  start  get=0x0B34BE20
  duration  get=0x0B34BD9C
  end  get=0x0B34BDE4
  clip  get=0x04D862C0
  mixer  get=0x04D8FD20
  playable  get=0x04D8FAD0
  enable  set=0x0B34BE98
METHODS:
  RVA=0x0B34BD4C  token=0x6000343  System.Void .ctor(UnityEngine.Timeline.TimelineClip clip, UnityEngine.Playables.Playable clipPlayable, UnityEngine.Playables.Playable parentMixer, System.Double startDelay, System.Double finishTail)
  RVA=0x0B34BA08  token=0x6000344  System.Void Create(UnityEngine.Timeline.TimelineClip clip, UnityEngine.Playables.Playable clipPlayable, UnityEngine.Playables.Playable parentMixer, System.Double startDelay, System.Double finishTail)
  RVA=0x0B34BAA0  token=0x6000346  System.Void EvaluateAt(System.Double localTime, UnityEngine.Playables.FrameData frameData, UnityEngine.Timeline.ITimelineRootMarker timelineRoot)
  RVA=0x069734E8  token=0x6000347  System.Void DisableAt(System.Double localTime, System.Double rootDuration, UnityEngine.Playables.FrameData frameData)
END_CLASS

CLASS: UnityEngine.Timeline.IMarker
TYPE:  interface
TOKEN: 0x200007C
FIELDS:
PROPERTIES:
  time  get=-1  // abstract  set=-1  // abstract
  parent  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x600034B  System.Void Initialize(UnityEngine.Timeline.TrackAsset parent)
END_CLASS

CLASS: UnityEngine.Timeline.INotificationOptionProvider
TYPE:  interface
TOKEN: 0x200007D
FIELDS:
PROPERTIES:
  flags  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.Marker
TYPE:  abstract class
TOKEN: 0x200007E
SIZE:  0x28
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: UnityEngine.Timeline.IMarker
FIELDS:
  private           System.Double                   m_Time  // 0x18
  private           UnityEngine.Timeline.TrackAsset <parent>k__BackingField  // 0x20
PROPERTIES:
  parent  get=0x04D862C0  set=0x05390F40
  time  get=0x04D86360  set=0x0B349E4C
METHODS:
  RVA=0x0B349D2C  token=0x6000351  System.Void UnityEngine.Timeline.IMarker.Initialize(UnityEngine.Timeline.TrackAsset parentTrack)
  RVA=0x041E1670  token=0x6000352  System.Void OnInitialize(UnityEngine.Timeline.TrackAsset aPent)
  RVA=0x03573370  token=0x6000353  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.MarkerList
TYPE:  sealed struct
TOKEN: 0x200007F
SIZE:  0x28
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.ScriptableObject>m_Objects  // 0x10
  private           System.Collections.Generic.List<UnityEngine.Timeline.IMarker>m_Cache  // 0x18
  private           System.Boolean                  m_CacheDirty  // 0x20
  private           System.Boolean                  m_HasNotifications  // 0x21
PROPERTIES:
  markers  get=0x05394794
  Count  get=0x03AC4070
  Item  get=0x0B349B28
METHODS:
  RVA=0x039F3AA0  token=0x6000355  System.Void .ctor(System.Int32 capacity)
  RVA=0x0B349644  token=0x6000356  System.Void Add(UnityEngine.ScriptableObject item)
  RVA=0x0B349974  token=0x6000357  System.Boolean Remove(UnityEngine.Timeline.IMarker item)
  RVA=0x0B349A78  token=0x6000358  System.Boolean Remove(UnityEngine.ScriptableObject item, UnityEngine.Timeline.TimelineAsset timelineAsset, UnityEngine.Playables.PlayableAsset thingToDirty)
  RVA=0x0B3496C8  token=0x6000359  System.Void Clear()
  RVA=0x05394794  token=0x600035A  System.Collections.Generic.IEnumerable<UnityEngine.Timeline.IMarker> GetMarkers()
  RVA=0x02B76770  token=0x600035D  System.Collections.Generic.List<UnityEngine.ScriptableObject> GetRawMarkerList()
  RVA=0x0B349714  token=0x600035E  UnityEngine.Timeline.IMarker CreateMarker(System.Type type, System.Double time, UnityEngine.Timeline.TrackAsset owner)
  RVA=0x05394778  token=0x600035F  System.Boolean HasNotifications()
  RVA=0x041E1670  token=0x6000360  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x015EFCD0  token=0x6000361  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x039F3B80  token=0x6000362  System.Void BuildCache()
END_CLASS

CLASS: UnityEngine.Timeline.MarkerTrack
TYPE:  class
TOKEN: 0x2000080
SIZE:  0xC0
EXTENDS: UnityEngine.Timeline.TrackAsset
IMPLEMENTS: Events.INotificationTrack
FIELDS:
PROPERTIES:
  outputs  get=0x0B349BD0
METHODS:
  RVA=0x0B349B84  token=0x6000364  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.CustomSignalEventDrawer
TYPE:  class
TOKEN: 0x2000081
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000365  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.SignalAsset
TYPE:  class
TOKEN: 0x2000082
SIZE:  0x18
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private   static  System.Action<UnityEngine.Timeline.SignalAsset>OnEnableCallback  // static @ 0x0
EVENTS:
  OnEnableCallback  add=add_OnEnableCallback  remove=remove_OnEnableCallback
METHODS:
  RVA=0x0B34C088  token=0x6000368  System.Void OnEnable()
  RVA=0x03573370  token=0x6000369  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.SignalEmitter
TYPE:  class
TOKEN: 0x2000083
SIZE:  0x38
EXTENDS: UnityEngine.Timeline.Marker
IMPLEMENTS: UnityEngine.Playables.INotification UnityEngine.Timeline.INotificationOptionProvider
FIELDS:
  private           System.Boolean                  m_Retroactive  // 0x28
  private           System.Boolean                  m_EmitOnce  // 0x29
  private           UnityEngine.Timeline.SignalAssetm_Asset  // 0x30
PROPERTIES:
  retroactive  get=0x011F33C0  set=0x011F33D0
  emitOnce  get=0x04D867B0  set=0x04D867C0
  asset  get=0x011F36E0  set=0x05396200
  UnityEngine.Playables.INotification.id  get=0x0B34C210
  UnityEngine.Timeline.INotificationOptionProvider.flags  get=0x0B34C2B4
METHODS:
  RVA=0x03573370  token=0x6000372  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.SignalReceiver
TYPE:  class
TOKEN: 0x2000084
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.Playables.INotificationReceiver
FIELDS:
  private           UnityEngine.Timeline.SignalReceiver.EventKeyValuem_Events  // 0x18
METHODS:
  RVA=0x0B34C9D0  token=0x6000373  System.Void OnNotify(UnityEngine.Playables.Playable origin, UnityEngine.Playables.INotification notification, System.Object context)
  RVA=0x0B34C348  token=0x6000374  System.Void AddReaction(UnityEngine.Timeline.SignalAsset asset, UnityEngine.Events.UnityEvent reaction)
  RVA=0x0B34C2DC  token=0x6000375  System.Int32 AddEmptyReaction(UnityEngine.Events.UnityEvent reaction)
  RVA=0x0B34CB58  token=0x6000376  System.Void Remove(UnityEngine.Timeline.SignalAsset asset)
  RVA=0x0A8DA4D0  token=0x6000377  System.Collections.Generic.IEnumerable<UnityEngine.Timeline.SignalAsset> GetRegisteredSignals()
  RVA=0x0B34C8DC  token=0x6000378  UnityEngine.Events.UnityEvent GetReaction(UnityEngine.Timeline.SignalAsset key)
  RVA=0x0B34C7D8  token=0x6000379  System.Int32 Count()
  RVA=0x0B34C570  token=0x600037A  System.Void ChangeSignalAtIndex(System.Int32 idx, UnityEngine.Timeline.SignalAsset newKey)
  RVA=0x0B34CAB0  token=0x600037B  System.Void RemoveAtIndex(System.Int32 idx)
  RVA=0x0B34C4A4  token=0x600037C  System.Void ChangeReactionAtIndex(System.Int32 idx, UnityEngine.Events.UnityEvent reaction)
  RVA=0x0B34C820  token=0x600037D  UnityEngine.Events.UnityEvent GetReactionAtIndex(System.Int32 idx)
  RVA=0x0B34C914  token=0x600037E  UnityEngine.Timeline.SignalAsset GetSignalAssetAtIndex(System.Int32 idx)
  RVA=0x041E1670  token=0x600037F  System.Void OnEnable()
  RVA=0x0B34CC20  token=0x6000380  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.SignalTrack
TYPE:  class
TOKEN: 0x2000086
SIZE:  0xC0
EXTENDS: UnityEngine.Timeline.MarkerTrack
FIELDS:
METHODS:
  RVA=0x0B34CC90  token=0x6000388  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.TrackAssetExtensions
TYPE:  static class
TOKEN: 0x2000087
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.GroupTrack
TYPE:  class
TOKEN: 0x2000088
SIZE:  0xC8
EXTENDS: UnityEngine.Timeline.TrackAsset
FIELDS:
  public            Beyond.PackageBridge.Timeline.TimelineConditionBaseenableOnCondition  // 0xc0
PROPERTIES:
  outputs  get=0x0B3493F8
METHODS:
  RVA=0x04D35910  token=0x6000389  System.Void CalculateRuntimeMuted()
  RVA=0x012081B0  token=0x600038A  System.Boolean CanCompileClips()
  RVA=0x04D5DEB0  token=0x600038C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.ILayerable
TYPE:  interface
TOKEN: 0x2000089
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600038D  UnityEngine.Playables.Playable CreateLayerMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount)
END_CLASS

CLASS: UnityEngine.Timeline.ITimeDilationListener
TYPE:  interface
TOKEN: 0x200008A
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600038E  System.Void OnTimeDilationChanged(System.Single timeDilation)
END_CLASS

CLASS: UnityEngine.Timeline.ActivationControlPlayable
TYPE:  class
TOKEN: 0x200008B
SIZE:  0x20
EXTENDS: UnityEngine.Timeline.ControlPlayableBehaviour
FIELDS:
  public            UnityEngine.GameObject          gameObject  // 0x10
  public            UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackStatepostPlayback  // 0x18
  private           UnityEngine.Timeline.ActivationControlPlayable.InitialStatem_InitialState  // 0x1c
METHODS:
  RVA=0x0450F4F0  token=0x600038F  UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.ActivationControlPlayable> Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject gameObject, UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState postPlaybackState)
  RVA=0x04A052D0  token=0x6000390  System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B3465FC  token=0x6000391  System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x04A05220  token=0x6000392  System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object userData)
  RVA=0x04972880  token=0x6000393  System.Void OnGraphStart(UnityEngine.Playables.Playable playable)
  RVA=0x0B346688  token=0x6000394  System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable)
  RVA=0x041E1670  token=0x6000395  System.Void RuntimeReset()
  RVA=0x04D8D750  token=0x6000396  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.AnimationEventRuntimeContextPlayable
TYPE:  class
TOKEN: 0x200008E
SIZE:  0x40
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  private           UnityEngine.Timeline.ITimelineAnimationEventContextBindingm_TimelineAnimationEventContextBinding  // 0x10
  private           UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerModem_PostAudioEventTriggerMode  // 0x18
  private           UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerModem_CustomFootStepTriggerMode  // 0x1c
  private           UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviourm_ClipEndAudioBehaviour  // 0x20
  private           UnityEngine.AnimationEvent[]    m_CachedAnimationEvents  // 0x28
  private           System.Single                   m_ClipDuration  // 0x30
  private           System.Single                   m_PostAudioEventTriggerThreshold  // 0x34
  private           System.Single                   m_CustomFootStepTriggerThreshold  // 0x38
  private           System.Boolean                  m_IsRegistered  // 0x3c
  private           System.Boolean                  m_HasRegisteredOnce  // 0x3d
METHODS:
  RVA=0x0B348790  token=0x6000397  UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.AnimationEventRuntimeContextPlayable> Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Timeline.ITimelineAnimationEventContextBinding timelineAnimationEventContextBinding, UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode postAudioEventTriggerMode, System.Single postAudioEventTriggerThreshold, UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode customFootStepTriggerMode, System.Single customFootStepTriggerThreshold, UnityEngine.AnimationEvent[] cachedAnimationEvents, System.Single clipDuration, UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviour clipEndAudioBehaviour, System.Int32 inputCount)
  RVA=0x0B348908  token=0x6000398  System.Void Initialize(UnityEngine.Timeline.ITimelineAnimationEventContextBinding timelineAnimationEventContextBinding, UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode postAudioEventTriggerMode, System.Single postAudioEventTriggerThreshold, UnityEngine.Timeline.AnimationPlayableAsset.AnimationEventTriggerMode customFootStepTriggerMode, System.Single customFootStepTriggerThreshold, UnityEngine.AnimationEvent[] cachedAnimationEvents, System.Single clipDuration, UnityEngine.Timeline.AnimationPlayableAsset.ClipEndAudioBehaviour clipEndAudioBehaviour)
  RVA=0x0B348B18  token=0x6000399  System.Void ResetRegistration(UnityEngine.Playables.Playable playable)
  RVA=0x0B348970  token=0x600039A  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x041E1670  token=0x600039B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.BasicPlayableBehaviour
TYPE:  class
TOKEN: 0x200008F
SIZE:  0x18
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: UnityEngine.Playables.IPlayableAsset UnityEngine.Playables.IPlayableBehaviour
FIELDS:
PROPERTIES:
  duration  get=0x0B348CA8
  outputs  get=0x0B348CF8
METHODS:
  RVA=0x041E1670  token=0x600039E  System.Void OnGraphStart(UnityEngine.Playables.Playable playable)
  RVA=0x041E1670  token=0x600039F  System.Void OnGraphStop(UnityEngine.Playables.Playable playable)
  RVA=0x041E1670  token=0x60003A0  System.Void OnPlayableCreate(UnityEngine.Playables.Playable playable)
  RVA=0x041E1670  token=0x60003A1  System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable)
  RVA=0x041E1670  token=0x60003A2  System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x041E1670  token=0x60003A3  System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x041E1670  token=0x60003A4  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x041E1670  token=0x60003A5  System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData)
  RVA=0x0B348BE8  token=0x60003A6  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner)
  RVA=0x03573370  token=0x60003A7  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.ControlPlayableBehaviour
TYPE:  class
TOKEN: 0x2000090
SIZE:  0x10
EXTENDS: UnityEngine.Playables.PlayableBehaviour
IMPLEMENTS: UnityEngine.Timeline.IBehaviourRuntimeReset
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60003A8  System.Void RuntimeReset()
  RVA=0x041E1670  token=0x60003A9  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.DirectorControlPlayable
TYPE:  class
TOKEN: 0x2000091
SIZE:  0x38
EXTENDS: UnityEngine.Timeline.ControlPlayableBehaviour
FIELDS:
  public            UnityEngine.Playables.PlayableDirectordirector  // 0x10
  public            UnityEngine.Timeline.ITimelineRootMarkerrootMarker  // 0x18
  private   static  System.Single                   SYNC_START_FORBID_THRESHOLD  // const
  private           System.Boolean                  m_SyncTime  // 0x20
  private           System.Double                   m_AssetDuration  // 0x28
  private           UnityEngine.Timeline.TimelinePlayablem_TimelinePlayable  // 0x30
METHODS:
  RVA=0x039F7760  token=0x60003AA  UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.DirectorControlPlayable> Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableDirector director)
  RVA=0x0B348F24  token=0x60003AB  System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable)
  RVA=0x03E0D6A0  token=0x60003AC  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x039EE7D0  token=0x60003AD  System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B348E6C  token=0x60003AE  System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x03E0D8F0  token=0x60003AF  System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData)
  RVA=0x03E0E900  token=0x60003B0  System.Void SyncSpeed(System.Double speed)
  RVA=0x03E0D590  token=0x60003B1  System.Void SyncStart(UnityEngine.Playables.PlayableGraph graph, System.Double time)
  RVA=0x03E0D610  token=0x60003B2  System.Void SyncStop(UnityEngine.Playables.PlayableGraph graph, System.Double time)
  RVA=0x041E1670  token=0x60003B3  System.Void RuntimeReset()
  RVA=0x04D77940  token=0x60003B4  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.ITimeControl
TYPE:  interface
TOKEN: 0x2000092
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60003B5  System.Void SetTime(System.Double time)
  RVA=-1  // abstract  token=0x60003B6  System.Void OnControlTimeStart()
  RVA=-1  // abstract  token=0x60003B7  System.Void OnControlTimeStop()
END_CLASS

CLASS: UnityEngine.Timeline.NotificationFlags
TYPE:  sealed struct
TOKEN: 0x2000093
SIZE:  0x12
FIELDS:
  public            System.Int16                    value__  // 0x10
  public    static  UnityEngine.Timeline.NotificationFlagsTriggerInEditMode  // const
  public    static  UnityEngine.Timeline.NotificationFlagsRetroactive  // const
  public    static  UnityEngine.Timeline.NotificationFlagsTriggerOnce  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.ParticleControlPlayable
TYPE:  class
TOKEN: 0x2000094
SIZE:  0x40
EXTENDS: UnityEngine.Timeline.ControlPlayableBehaviour
IMPLEMENTS: UnityEngine.Timeline.ITimeDilationListener
FIELDS:
  private           System.Single                   m_LastPlayableTime  // 0x10
  private           System.Single                   m_LastParticleTime  // 0x14
  private           System.UInt32                   m_RandomSeed  // 0x18
  private           System.Boolean                  m_forceRuntimeSimulate  // 0x1c
  private           System.Single                   m_clipIn  // 0x20
  private           System.Boolean                  m_hadInitialized  // 0x24
  private           System.Single                   originalSimulateSpeed  // 0x28
  private           System.Single                   simulateSpeedModifier  // 0x2c
  private           UnityEngine.ParticleSystem      <particleSystem>k__BackingField  // 0x30
  private           System.Boolean                  m_hadIgnite  // 0x38
  private           System.Boolean                  m_hadSimulate  // 0x39
  private           System.Boolean                  m_hadPlayed  // 0x3a
PROPERTIES:
  particleSystem  get=0x011F36E0  set=0x05396200
  shouldSimulate  get=0x0B34A95C
METHODS:
  RVA=0x0B349EB4  token=0x60003B8  UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.ParticleControlPlayable> Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.ParticleSystem component, System.UInt32 randomSeed, System.Single clipIn, System.Single speedMultiplier, System.Boolean forceRuntimeSimulate)
  RVA=0x0B34A048  token=0x60003BC  System.Void Initialize(UnityEngine.ParticleSystem ps, System.UInt32 randomSeed, System.Single clipIn, System.Single simulateSpeed, System.Boolean forceRuntimeSimulate)
  RVA=0x0B34A3C4  token=0x60003BD  System.Void SetOriginalSimulateSpeed(UnityEngine.ParticleSystem particleSystem, System.Double simulateSpeed)
  RVA=0x0B34A4FC  token=0x60003BE  System.Void SetRandomSeed(UnityEngine.ParticleSystem particleSystem, System.UInt32 randomSeed)
  RVA=0x0B34A220  token=0x60003BF  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData data)
  RVA=0x0B34A8D0  token=0x60003C0  System.Void _SimulateParticle(System.Single time, System.Single particleTime, System.Boolean previewMode)
  RVA=0x04DBA680  token=0x60003C1  System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B34A7B8  token=0x60003C2  System.Void _PlayOrQueuedParticleSystem()
  RVA=0x0B34A108  token=0x60003C3  System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B34A5F8  token=0x60003C4  System.Void Simulate(System.Single time, System.Boolean restart)
  RVA=0x04DBA690  token=0x60003C5  System.Void RuntimeReset()
  RVA=0x0B34A1C4  token=0x60003C6  System.Void OnTimeDilationChanged(System.Single timeScale)
  RVA=0x04DBA6A0  token=0x60003C7  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.PrefabControlPlayable
TYPE:  class
TOKEN: 0x2000095
SIZE:  0x18
EXTENDS: UnityEngine.Timeline.ControlPlayableBehaviour
FIELDS:
  private           UnityEngine.GameObject          m_Instance  // 0x10
PROPERTIES:
  prefabInstance  get=0x02B2ECC0
METHODS:
  RVA=0x0B34ABD8  token=0x60003C8  UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.PrefabControlPlayable> Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject prefabGameObject, UnityEngine.Transform parentTransform)
  RVA=0x0B34AD08  token=0x60003CA  UnityEngine.GameObject Initialize(UnityEngine.GameObject prefabGameObject, UnityEngine.Transform parentTransform)
  RVA=0x0B34B058  token=0x60003CB  System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable)
  RVA=0x0B34AFE8  token=0x60003CC  System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B34AF5C  token=0x60003CD  System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B34B0E0  token=0x60003CE  System.Void SetHideFlagsRecursive(UnityEngine.GameObject gameObject)
  RVA=0x041E1670  token=0x60003CF  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.TimeControlPlayable
TYPE:  class
TOKEN: 0x2000096
SIZE:  0x20
EXTENDS: UnityEngine.Timeline.ControlPlayableBehaviour
FIELDS:
  private           UnityEngine.Timeline.ITimeControlm_timeControl  // 0x10
  private           System.Boolean                  m_started  // 0x18
METHODS:
  RVA=0x0B34CC98  token=0x60003D0  UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.TimeControlPlayable> Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Timeline.ITimeControl timeControl)
  RVA=0x053908C0  token=0x60003D1  System.Void Initialize(UnityEngine.Timeline.ITimeControl timeControl)
  RVA=0x0B34CE30  token=0x60003D2  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B34CDDC  token=0x60003D3  System.Void OnBehaviourPlay(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B34CD88  token=0x60003D4  System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x041E1670  token=0x60003D5  System.Void RuntimeReset()
  RVA=0x041E1670  token=0x60003D6  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.TimeNotificationBehaviour
TYPE:  class
TOKEN: 0x2000097
SIZE:  0x38
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  private   readonly System.Collections.Generic.List<UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry>m_Notifications  // 0x10
  private           System.Double                   m_PreviousTime  // 0x18
  private           System.Boolean                  m_NeedSortNotifications  // 0x20
  private           System.Boolean                  m_IsManualUpdate  // 0x21
  private           UnityEngine.Playables.Playable  m_TimeSource  // 0x28
PROPERTIES:
  timeSource  set=0x04D8FAE0
  isManualUpdate  set=0x04D86860
METHODS:
  RVA=0x0B34CF8C  token=0x60003D9  UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.TimeNotificationBehaviour> Create(UnityEngine.Playables.PlayableGraph graph, System.Double duration, UnityEngine.Playables.DirectorWrapMode loopMode)
  RVA=0x0B34CEA8  token=0x60003DA  System.Void AddNotification(System.Double time, UnityEngine.Playables.INotification payload, UnityEngine.Timeline.NotificationFlags flags)
  RVA=0x0B34D244  token=0x60003DB  System.Void OnGraphStart(UnityEngine.Playables.Playable playable)
  RVA=0x0B34D09C  token=0x60003DC  System.Void OnBehaviourPause(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B34D3E4  token=0x60003DD  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B34D818  token=0x60003DE  System.Void SortNotifications()
  RVA=0x0B34CF5C  token=0x60003DF  System.Boolean CanRestoreNotification(UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry e, UnityEngine.Playables.FrameData info, System.Double currentTime, System.Double previousTime)
  RVA=0x0B34DA20  token=0x60003E0  System.Void TriggerNotificationsInRange(System.Double start, System.Double end, UnityEngine.Playables.FrameData info, UnityEngine.Playables.Playable playable, System.Boolean checkState)
  RVA=0x0B34D940  token=0x60003E1  System.Void SyncDurationWithExternalSource(UnityEngine.Playables.Playable playable)
  RVA=0x0B34DC00  token=0x60003E2  System.Void Trigger_internal(UnityEngine.Playables.Playable playable, UnityEngine.Playables.PlayableOutput output, UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry& e)
  RVA=0x04D8CCB0  token=0x60003E3  System.Void Restore_internal(UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry& e)
  RVA=0x0B34DC7C  token=0x60003E4  System.Double _GetPlayableTime(UnityEngine.Playables.Playable playable)
  RVA=0x0B34DD40  token=0x60003E5  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.IAsyncScriptAnimationJobSyncInit
TYPE:  interface
TOKEN: 0x200009A
FIELDS:
PROPERTIES:
  isAsyncInitPending  get=-1  // abstract
  isAsyncBindStreamPropertiesComplete  get=-1  // abstract
  isAsyncBuildNecessaryLayersComplete  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60003EF  System.Boolean StepAsyncBindStreamProperties(System.Int32& bindCountBudget)
  RVA=-1  // abstract  token=0x60003F0  System.Boolean StepAsyncBuildNecessaryLayers()
  RVA=-1  // abstract  token=0x60003F1  System.Boolean StepAsyncFinishInit()
  RVA=-1  // abstract  token=0x60003F2  System.Void ClearAsyncInitState()
END_CLASS

CLASS: UnityEngine.Timeline.CommunicatorParam
TYPE:  class
TOKEN: 0x200009B
SIZE:  0x30
FIELDS:
  public            UnityEngine.Timeline.IScriptAnimationJobSyncjobSync  // 0x10
  public            UnityEngine.Timeline.IAsyncScriptAnimationJobSyncInitasyncJobSyncInit  // 0x18
  public            System.String                   uniqueId  // 0x20
  public            System.Boolean                  lookAtUseAdditive  // 0x28
  public            System.Boolean                  useLookAtV2  // 0x29
  public            System.Boolean                  useAsyncJobSyncInit  // 0x2a
METHODS:
  RVA=0x041E1670  token=0x60003F3  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.IAutoGenMono
TYPE:  interface
TOKEN: 0x200009C
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.IBehaviourRuntimeReset
TYPE:  interface
TOKEN: 0x200009D
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.IScriptAnimationJobSync
TYPE:  interface
TOKEN: 0x200009E
IMPLEMENTS: UnityEngine.Timeline.IAutoGenMono
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60003F4  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=-1  // abstract  token=0x60003F5  System.Void CalcLayerMainStream(System.Single deltaTime)
  RVA=-1  // abstract  token=0x60003F6  System.Void ResetCloth(System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> activeRuntimeElements, System.Boolean useSoftReset, System.Boolean keepPose, System.Boolean useRelativeTeleport)
END_CLASS

CLASS: UnityEngine.Timeline.TimelineScriptAnimationJob
TYPE:  sealed struct
TOKEN: 0x200009F
SIZE:  0x20
IMPLEMENTS: UnityEngine.Animations.IAnimationJob
FIELDS:
  private   static readonly Unity.Profiling.ProfilerMarker  s_processRootMotionMarker  // static @ 0x0
  public            UnityEngine.Timeline.IScriptAnimationJobSyncbindedScriptAnimationJobSync  // 0x10
  public            UnityEngine.Animator            bindedAnimator  // 0x18
METHODS:
  RVA=0x039CF630  token=0x60003F7  System.Void .ctor(UnityEngine.Timeline.IScriptAnimationJobSync job, UnityEngine.Animator animator)
  RVA=0x041E1670  token=0x60003F8  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B351F80  token=0x60003F9  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
  RVA=0x02ED08A0  token=0x60003FA  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.PlayableTrack
TYPE:  class
TOKEN: 0x20000A0
SIZE:  0xC0
EXTENDS: UnityEngine.Timeline.TrackAsset
FIELDS:
METHODS:
  RVA=0x0B34AAF4  token=0x60003FB  System.Void OnCreateClip(UnityEngine.Timeline.TimelineClip clip)
  RVA=0x0B34AB8C  token=0x60003FC  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.TrackMediaType
TYPE:  class
TOKEN: 0x20000A1
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.TrackClipTypeAttribute
TYPE:  class
TOKEN: 0x20000A2
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Type                     inspectedType  // 0x10
  public    readonly System.Boolean                  allowAutoCreate  // 0x18
METHODS:
  RVA=0x04774A10  token=0x60003FD  System.Void .ctor(System.Type clipClass)
  RVA=0x04D831B0  token=0x60003FE  System.Void .ctor(System.Type clipClass, System.Boolean allowAutoCreate)
END_CLASS

CLASS: UnityEngine.Timeline.NotKeyableAttribute
TYPE:  class
TOKEN: 0x20000A3
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60003FF  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.TrackBindingFlags
TYPE:  sealed struct
TOKEN: 0x20000A4
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.TrackBindingFlagsNone  // const
  public    static  UnityEngine.Timeline.TrackBindingFlagsAllowCreateComponent  // const
  public    static  UnityEngine.Timeline.TrackBindingFlagsAll  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.TrackBindingTypeAttribute
TYPE:  class
TOKEN: 0x20000A5
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Type                     type  // 0x10
  public    readonly UnityEngine.Timeline.TrackBindingFlagsflags  // 0x18
METHODS:
  RVA=0x04824300  token=0x6000400  System.Void .ctor(System.Type type)
  RVA=0x03B46FF0  token=0x6000401  System.Void .ctor(System.Type type, UnityEngine.Timeline.TrackBindingFlags flags)
END_CLASS

CLASS: UnityEngine.Timeline.SupportsChildTracksAttribute
TYPE:  class
TOKEN: 0x20000A6
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Type                     childType  // 0x10
  public    readonly System.Int32                    levels  // 0x18
METHODS:
  RVA=0x03B46FF0  token=0x6000402  System.Void .ctor(System.Type childType, System.Int32 levels)
END_CLASS

CLASS: UnityEngine.Timeline.IgnoreOnPlayableTrackAttribute
TYPE:  class
TOKEN: 0x20000A7
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000403  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.TimeFieldAttribute
TYPE:  class
TOKEN: 0x20000A8
SIZE:  0x18
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  private   readonly UnityEngine.Timeline.TimeFieldAttribute.UseEditMode<useEditMode>k__BackingField  // 0x10
METHODS:
  RVA=0x014F51F0  token=0x6000404  System.Void .ctor(UnityEngine.Timeline.TimeFieldAttribute.UseEditMode useEditMode)
END_CLASS

CLASS: UnityEngine.Timeline.FrameRateFieldAttribute
TYPE:  class
TOKEN: 0x20000AA
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000405  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.HideInMenuAttribute
TYPE:  class
TOKEN: 0x20000AB
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000406  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.CustomStyleAttribute
TYPE:  class
TOKEN: 0x20000AC
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   ussStyle  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000407  System.Void .ctor(System.String ussStyle)
END_CLASS

CLASS: UnityEngine.Timeline.MenuCategoryAttribute
TYPE:  class
TOKEN: 0x20000AD
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.ITimelineClipAsset
TYPE:  interface
TOKEN: 0x20000AE
FIELDS:
PROPERTIES:
  clipCaps  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.ITimelineEvaluateCallback
TYPE:  interface
TOKEN: 0x20000AF
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000409  System.Void Evaluate()
END_CLASS

CLASS: UnityEngine.Timeline.TimelinePlayable
TYPE:  class
TOKEN: 0x20000B0
SIZE:  0x108
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  public            System.Double                   timeOffset  // 0x10
  public            System.Int32                    newOptionIndex  // 0x18
  public            System.Int32                    curOptionIndex  // 0x1c
  public            System.Int32                    lastOptionIndex  // 0x20
  public            System.Boolean                  checkTimeJumpForNextSeek  // 0x24
  private           UnityEngine.Timeline.IntervalTree<UnityEngine.Timeline.RuntimeElement>m_IntervalTree  // 0x28
  private           System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement>m_ActiveClips  // 0x30
  private           System.Collections.Generic.HashSet<UnityEngine.Timeline.RuntimeElement>m_ActiveClipsSet  // 0x38
  private           System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement>m_CurrentListOfActiveClips  // 0x40
  private           System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement>m_ActiveClipsCalcCache  // 0x48
  private           System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement>m_RuntimeResetClipCache  // 0x50
  private           System.Collections.Generic.List<UnityEngine.Timeline.ITimeDilationListener>m_timeDialationListeners  // 0x58
  private           System.Int32                    m_ActiveBit  // 0x60
  private           System.Boolean                  m_needEvaluateTwice  // 0x64
  private           System.Boolean                  m_isTimelineEnded  // 0x65
  private           System.Collections.Generic.List<UnityEngine.Timeline.ITimelineEvaluateCallback>m_EvaluateCallbacks  // 0x68
  private           System.Collections.Generic.Dictionary<UnityEngine.Timeline.TrackAsset,UnityEngine.Playables.Playable>m_PlayableCache  // 0x70
  private           UnityEngine.Playables.PlayableDirectorm_TopDirector  // 0x78
  private           System.Collections.Generic.List<UnityEngine.Playables.PlayableDirector>m_AllDirectorUnderRoot  // 0x80
  private           UnityEngine.Playables.PlayableDirectorm_DirectorHoldingThisPlayable  // 0x88
  private           UnityEngine.Timeline.ITimelineRootMarkerm_RootMarker  // 0x90
  private           System.Collections.Generic.List<UnityEngine.Timeline.IScriptAnimationJobSync>m_ScriptAnimationJobSyncs  // 0x98
  private           System.Collections.Generic.HashSet<UnityEngine.Playables.Playable>m_forbidPostProcessMixers  // 0xa0
  private           System.Boolean                  m_isAsyncCompileEnabled  // 0xa8
  private           System.Boolean                  m_isAsyncCompileComplete  // 0xa9
  private           System.Boolean                  m_isAsyncCompileFailed  // 0xaa
  private           UnityEngine.Playables.PlayableGraphm_AsyncCompileGraph  // 0xb0
  private           UnityEngine.Playables.Playable  m_AsyncTimelinePlayable  // 0xc0
  private           System.Collections.Generic.List<UnityEngine.Timeline.TrackAsset>m_AsyncOutputTrackList  // 0xd0
  private           UnityEngine.GameObject          m_AsyncCompileGo  // 0xd8
  private           System.Boolean                  m_AsyncAutoRebalance  // 0xe0
  private           System.Boolean                  m_AsyncCreateOutputs  // 0xe1
  private           System.Int32                    m_AsyncOutputTrackIndex  // 0xe4
  private           System.Boolean                  m_AsyncCombineToTopCompiled  // 0xe8
  private           UnityEngine.Playables.PlayableOutputm_AsyncCompileDummyOutput  // 0xf0
  private   static readonly Unity.Profiling.ProfilerMarker  s_compileSingleTrackMarker  // static @ 0x0
  private   static readonly Unity.Profiling.ProfilerMarker  s_prepareFrameMarker  // static @ 0x8
  private   static readonly Unity.Profiling.ProfilerMarker  s_evaluateMarker  // static @ 0x10
  private   static readonly Unity.Profiling.ProfilerMarker  s_queryActiveClipsMarker  // static @ 0x18
  private   static readonly Unity.Profiling.ProfilerMarker  s_filterActiveClipsMarker  // static @ 0x20
  private   static readonly Unity.Profiling.ProfilerMarker  s_disableInactiveClipsMarker  // static @ 0x28
  private   static readonly Unity.Profiling.ProfilerMarker  s_evaluateActiveClipsMarker  // static @ 0x30
  private   static readonly Unity.Profiling.ProfilerMarker  s_evaluateCallbacksMarker  // static @ 0x38
  private   static readonly Unity.Profiling.ProfilerMarker  s_resetScriptAnimationJobSyncsMarker  // static @ 0x40
  private   static readonly Unity.Profiling.ProfilerMarker  s_evaluateTwiceMarker  // static @ 0x48
  private   static  System.Boolean                  muteAudioScrubbing  // static @ 0x50
  private           System.Double                   m_timeForNextFrame  // 0x100
PROPERTIES:
  timeForNextFrame  get=0x04D925A0
  timelineRootMarker  get=0x04D86220
  isAsyncCompileComplete  get=0x02ED0880
  isAsyncCompileFailed  get=0x02ED0890
  enableLoop  get=0x0B34F3AC
METHODS:
  RVA=0x03FC6800  token=0x600040F  UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.TimelinePlayable> Create(UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> tracks, UnityEngine.GameObject go, System.Boolean autoRebalance, System.Boolean createOutputs)
  RVA=0x03FC7E20  token=0x6000410  System.Void Compile(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> tracks, UnityEngine.GameObject go, System.Boolean autoRebalance, System.Boolean createOutputs)
  RVA=0x02EC7ED0  token=0x6000411  System.Void BeginAsyncCompile(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> tracks, UnityEngine.GameObject go, System.Boolean autoRebalance, System.Boolean createOutputs)
  RVA=0x02ECFD60  token=0x6000412  System.Boolean StepAsyncCompile(System.Int32 outputGroupBudget, System.Boolean isInBurstMode)
  RVA=0x02ECCBB0  token=0x6000413  System.Void InitializeCompileContext(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> tracks, UnityEngine.GameObject go, System.Boolean autoRebalance, System.Boolean createOutputs)
  RVA=0x041E1670  token=0x6000414  System.Void FinalizeCompile()
  RVA=0x02EC9CD0  token=0x6000415  System.Void CreateAsyncCompileDummyOutput(UnityEngine.Playables.PlayableGraph graph)
  RVA=0x02EC7F90  token=0x6000416  System.Void ClearAsyncCompileDummyOutput()
  RVA=0x04DBA700  token=0x6000417  System.Void MarkTimelineEnd()
  RVA=0x04DAA1E0  token=0x6000418  System.Boolean IsTimelineEnded()
  RVA=0x039EF290  token=0x6000419  System.Void CompileCombineTopTopTrackList(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, System.Boolean createOutputs)
  RVA=0x039F4EB0  token=0x600041A  System.Void CollectCombineToTopTracksRecursive(UnityEngine.Timeline.TrackAsset track, System.Collections.Generic.List<UnityEngine.Timeline.TrackAsset> combineTracks)
  RVA=0x039F5080  token=0x600041B  System.Void CompileTrackList(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> tracks, UnityEngine.GameObject go, System.Boolean createOutputs, System.Boolean isCompilingCombineToTopTracks)
  RVA=0x02EC86D0  token=0x600041C  System.Boolean CompileSingleTrack(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, UnityEngine.Timeline.TrackAsset track, UnityEngine.GameObject go, System.Boolean createOutputs, System.Boolean isCompilingCombineToTopTracks)
  RVA=0x02ECF130  token=0x600041D  System.Boolean IsAsyncCompileFrameBarrierTrack(UnityEngine.Timeline.TrackAsset track)
  RVA=0x03FC97E0  token=0x600041E  System.Void CreateTrackOutput(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Timeline.TrackAsset track, UnityEngine.GameObject go, UnityEngine.Playables.Playable playable, System.Int32 port)
  RVA=0x03E11710  token=0x600041F  System.Void EvaluateWeightsForAnimationPlayableOutput(UnityEngine.Timeline.TrackAsset track, UnityEngine.Animations.AnimationPlayableOutput animOutput, UnityEngine.Animator animator)
  RVA=0x03433E70  token=0x6000420  UnityEngine.Playables.Playable CreateTrackPlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.Playable timelinePlayable, UnityEngine.Timeline.TrackAsset track, UnityEngine.GameObject go, System.Boolean createOutputs)
  RVA=0x0B34EEB4  token=0x6000421  System.Void UpdateIntervalTree()
  RVA=0x0B34EC78  token=0x6000422  System.Void RuntimeResetIntervalTree()
  RVA=0x02ECF440  token=0x6000423  System.Boolean IsDynamicLinkAnimationOutput(UnityEngine.Playables.PlayableOutput output)
  RVA=0x02ECA7B0  token=0x6000424  System.Boolean HasDynamicLinkAnimationClip(UnityEngine.Timeline.TrackAsset track)
  RVA=0x03E0B9F0  token=0x6000425  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x0B34F1A4  token=0x6000426  System.Void _ResetScriptAnimationJobySyncs()
  RVA=0x0B34EEFC  token=0x6000427  System.Void _EvaluateTwice()
  RVA=0x04D85A60  token=0x6000428  System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> GetCurrentActiveClips()
  RVA=0x0489B450  token=0x6000429  System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> GetActiveClipsAtGivenTime(System.Double givenTime, System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> activeClips)
  RVA=0x0B34EAC4  token=0x600042A  System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> GetActiveClipsAtGivenTimeRange(System.Double startTime, System.Double endTime, System.Collections.Generic.List<UnityEngine.Timeline.RuntimeElement> activeClips)
  RVA=0x039F7A20  token=0x600042B  System.Void Evaluate(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData frameData)
  RVA=0x0B34E854  token=0x600042C  System.Boolean DoReverseLoop(System.Double reverseTime)
  RVA=0x0B34E778  token=0x600042D  System.Boolean DoReverseJump(UnityEngine.Timeline.RuntimeClip runtimeClip, System.Double jumpTime)
  RVA=0x0B34E6C0  token=0x600042E  System.Boolean DoJump(UnityEngine.Timeline.RuntimeClip runtimeClip, System.Double localTime, System.Double jumpTime)
  RVA=0x039EC880  token=0x600042F  System.Boolean _CheckIfTimeJumping(UnityEngine.Playables.FrameData frameData, System.Double localTime)
  RVA=0x049CB200  token=0x6000430  System.Void CacheTrack(UnityEngine.Timeline.TrackAsset track, UnityEngine.Playables.Playable playable, System.Int32 port, UnityEngine.Playables.Playable parent)
  RVA=0x04D925E0  token=0x6000431  System.Void SetTimeForNextFrame(System.Double newTime)
  RVA=0x0B34EC24  token=0x6000432  System.Void RegisterTimeDilationListener(UnityEngine.Timeline.ITimeDilationListener listener)
  RVA=0x0B34EBC0  token=0x6000433  System.Void MarkMixerForbidPostProcess(UnityEngine.Playables.Playable mixer)
  RVA=0x03E10BF0  token=0x6000434  System.Boolean CheckIfMixerForbidPostProcess(UnityEngine.Playables.Playable mixer)
  RVA=0x0B34ECC0  token=0x6000435  System.Void SetTimeScale(System.Single usingTimeScale)
  RVA=0x0B34F058  token=0x6000436  System.Void _NotifyTimeScaleChanged(System.Single usingTimeScale)
  RVA=0x03FC8600  token=0x6000437  System.Void .ctor()
  RVA=0x04D77960  token=0x6000438  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.AnimationPreviewUtilities
TYPE:  static class
TOKEN: 0x20000B1
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.Extrapolation
TYPE:  static class
TOKEN: 0x20000B2
SIZE:  0x10
FIELDS:
  private   static readonly System.Double                   kMinExtrapolationTime  // static @ 0x0
METHODS:
  RVA=0x039F6420  token=0x6000439  System.Void CalculateExtrapolationTimes(UnityEngine.Timeline.TrackAsset asset)
  RVA=0x04B20B90  token=0x600043A  UnityEngine.Timeline.TimelineClip[] SortClipsByStartTime(UnityEngine.Timeline.TimelineClip[] clips)
  RVA=0x03B0C320  token=0x600043B  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.HashUtility
TYPE:  static class
TOKEN: 0x20000B4
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04DBA660  token=0x600043F  System.Int32 CombineHash(System.Int32 h1, System.Int32 h2)
  RVA=0x04DBA620  token=0x6000440  System.Int32 CombineHash(System.Int32 h1, System.Int32 h2, System.Int32 h3)
  RVA=0x03AC42A0  token=0x6000441  System.Int32 CombineHash(System.Int32 h1, System.Int32 h2, System.Int32 h3, System.Int32 h4)
  RVA=0x03AC4260  token=0x6000442  System.Int32 CombineHash(System.Int32 h1, System.Int32 h2, System.Int32 h3, System.Int32 h4, System.Int32 h5)
  RVA=0x03AC4200  token=0x6000443  System.Int32 CombineHash(System.Int32 h1, System.Int32 h2, System.Int32 h3, System.Int32 h4, System.Int32 h5, System.Int32 h6)
END_CLASS

CLASS: UnityEngine.Timeline.IPropertyCollector
TYPE:  interface
TOKEN: 0x20000B5
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000444  System.Void PushActiveGameObject(UnityEngine.GameObject gameObject)
  RVA=-1  // abstract  token=0x6000445  System.Void PopActiveGameObject()
  RVA=-1  // abstract  token=0x6000446  System.Void AddFromClip(UnityEngine.AnimationClip clip)
  RVA=-1  // abstract  token=0x6000447  System.Void AddFromName(System.String name)
  RVA=-1  // abstract  token=0x6000448  System.Void AddFromName(UnityEngine.GameObject obj, System.String name)
  RVA=-1  // abstract  token=0x6000449  System.Void AddFromName(UnityEngine.GameObject obj, System.String name)
  RVA=-1  // abstract  token=0x600044A  System.Void AddFromComponent(UnityEngine.GameObject obj, UnityEngine.Component component)
  RVA=-1  // abstract  token=0x600044B  System.Void AddObjectProperties(UnityEngine.Object obj, UnityEngine.AnimationClip clip)
END_CLASS

CLASS: UnityEngine.Timeline.IPropertyPreview
TYPE:  interface
TOKEN: 0x20000B6
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600044C  System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver)
END_CLASS

CLASS: UnityEngine.Timeline.NotificationUtilities
TYPE:  static class
TOKEN: 0x20000B7
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x039ED700  token=0x600044D  UnityEngine.Playables.ScriptPlayable<UnityEngine.Timeline.TimeNotificationBehaviour> CreateNotificationsPlayable(UnityEngine.Playables.PlayableGraph graph, System.Collections.Generic.IEnumerable<UnityEngine.Timeline.IMarker> markers, System.Double duration, UnityEngine.Playables.DirectorWrapMode extrapolationMode)
  RVA=0x039EE2D0  token=0x600044E  System.Boolean TrackTypeSupportsNotifications(System.Type type)
END_CLASS

CLASS: UnityEngine.Timeline.PlayableDirectorUtility
TYPE:  static class
TOKEN: 0x20000B8
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B34AAC4  token=0x600044F  System.Void SetOptionIndexSingleDirector(UnityEngine.Playables.PlayableDirector director, System.Int32 curOptionIndex, System.Int32 lastOptionIndex)
  RVA=0x0B34AA48  token=0x6000450  UnityEngine.Timeline.TimelinePlayable GetTimelinePlayable(UnityEngine.Playables.PlayableDirector director)
  RVA=0x03E10640  token=0x6000451  UnityEngine.Timeline.TimelinePlayable GetTimelinePlayable(UnityEngine.Playables.PlayableGraph graph)
  RVA=0x03E103D0  token=0x6000452  UnityEngine.Timeline.TimelinePlayable GetTimelinePlayableFromChildPlayable(UnityEngine.Playables.Playable playable)
  RVA=0x0B34A984  token=0x6000453  System.Boolean CheckIfTimelineFunctional(UnityEngine.Playables.Playable playable)
END_CLASS

CLASS: UnityEngine.Timeline.TimelineClipExtensions
TYPE:  static class
TOKEN: 0x20000B9
SIZE:  0x10
FIELDS:
  private   static readonly System.String                   k_UndoSetParentTrackText  // static @ 0x0
METHODS:
  RVA=0x0B34DE8C  token=0x6000454  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.TimelineCreateUtilities
TYPE:  static class
TOKEN: 0x20000BA
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B34E128  token=0x6000455  System.String GenerateUniqueActorName(System.Collections.Generic.List<UnityEngine.ScriptableObject> tracks, System.String name)
  RVA=0x0B34E3DC  token=0x6000456  System.Void SaveAssetIntoObject(UnityEngine.Object childAsset, UnityEngine.Object masterAsset)
  RVA=0x0B34DF84  token=0x6000457  UnityEngine.AnimationClip CreateAnimationClipForTrack(System.String name, UnityEngine.Timeline.TrackAsset track, System.Boolean isLegacy)
  RVA=0x0B34E49C  token=0x6000458  System.Boolean ValidateParentTrack(UnityEngine.Timeline.TrackAsset parent, System.Type childType)
END_CLASS

CLASS: UnityEngine.Timeline.TimelineRuntimeUtils
TYPE:  class
TOKEN: 0x20000BD
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03E105E0  token=0x600045D  System.Double GetTimelineDeltaTime(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData frameData)
  RVA=0x0B34F3F8  token=0x600045E  System.Boolean CheckWillRuntimeElementEnabled(UnityEngine.Timeline.RuntimeElement element, System.Int32 curOptionIndex, System.Int32 lastOptionIndex, System.Int64 curDiscreteTime)
  RVA=0x0B3504A0  token=0x600045F  System.Void DoReverseJump(UnityEngine.Timeline.RuntimeClip jumpClip, UnityEngine.Timeline.ITimelineRootMarker timelineRoot, System.Double curTime, System.Double jumpTime, System.Double mixDuration, System.Int32 curOptionIndex, System.Int32 lastOptionIndex, System.Boolean needChangeOptionAfterJump, System.Int32 newOptionIndex)
  RVA=0x0B34F49C  token=0x6000460  System.Void DoJump(UnityEngine.Timeline.RuntimeClip jumpClip, UnityEngine.Timeline.ITimelineRootMarker timelineRoot, System.Double curTime, System.Double jumpTime, System.Double mixDuration, System.Int32 curOptionIndex, System.Int32 lastOptionIndex, System.Boolean needChangeOptionAfterJump, System.Int32 optionIndexChangingTo)
  RVA=0x0B351940  token=0x6000461  System.Double GetSubDirectorTimeForDirectorControl(UnityEngine.Playables.Playable playable, UnityEngine.Playables.PlayableDirector director)
  RVA=0x0B351CC0  token=0x6000462  System.Void SetNewTimeForCutsceneRoot(UnityEngine.Timeline.ITimelineRootMarker timelineRoot, System.Double newTime, System.Boolean useThisTimeNextFrame)
  RVA=0x0B35149C  token=0x6000463  UnityEngine.AnimationClip FindTPoseClipFromLayerDescriptors(System.Collections.Generic.List<UnityEngine.Timeline.LayerDescriptor> layerDescriptors)
  RVA=0x0B351C34  token=0x6000464  System.Void LinkDefaultTPoseIfNeeded(UnityEngine.Animations.AnimationLayerMixerPlayable mixer, System.Collections.Generic.List<UnityEngine.Timeline.LayerDescriptor> layerDescriptors, System.String gameObjectName, System.Boolean disableBaseTPoseClip)
  RVA=0x0B351A98  token=0x6000465  System.Void LinkDefaultTPoseClip(UnityEngine.Animations.AnimationLayerMixerPlayable mixer, UnityEngine.AnimationClip tposeClip, System.String gameObjectName)
  RVA=0x041E1670  token=0x6000466  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Timeline.TimelineUndo
TYPE:  static class
TOKEN: 0x20000BE
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B3520F8  token=0x6000467  System.Void PushDestroyUndo(UnityEngine.Timeline.TimelineAsset timeline, UnityEngine.Object thingToDirty, UnityEngine.Object objectToDestroy)
END_CLASS

CLASS: UnityEngine.Timeline.StandardFrameRates
TYPE:  sealed struct
TOKEN: 0x20000BF
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Timeline.StandardFrameRatesFps24  // const
  public    static  UnityEngine.Timeline.StandardFrameRatesFps23_97  // const
  public    static  UnityEngine.Timeline.StandardFrameRatesFps25  // const
  public    static  UnityEngine.Timeline.StandardFrameRatesFps30  // const
  public    static  UnityEngine.Timeline.StandardFrameRatesFps29_97  // const
  public    static  UnityEngine.Timeline.StandardFrameRatesFps50  // const
  public    static  UnityEngine.Timeline.StandardFrameRatesFps60  // const
  public    static  UnityEngine.Timeline.StandardFrameRatesFps59_94  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Timeline.TimeUtility
TYPE:  static class
TOKEN: 0x20000C0
SIZE:  0x10
FIELDS:
  public    static readonly System.Double                   kTimeEpsilon  // static @ 0x0
  public    static readonly System.Double                   kFrameRateEpsilon  // static @ 0x8
  public    static readonly System.Double                   k_MaxTimelineDurationInSeconds  // static @ 0x10
  public    static readonly System.Double                   kFrameRateRounding  // static @ 0x18
METHODS:
  RVA=0x0B34DDB8  token=0x6000468  System.Double GetAnimationClipLength(UnityEngine.AnimationClip clip)
  RVA=0x04D27990  token=0x6000469  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Timeline.WeightUtility
TYPE:  static class
TOKEN: 0x20000C2
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03E11A60  token=0x600046C  System.Single NormalizeMixer(UnityEngine.Playables.Playable mixer)
END_CLASS

CLASS: UnityEngine.Timeline.Condition.TimelineConditionControlMono
TYPE:  class
TOKEN: 0x20000C3
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Beyond.PackageBridge.Timeline.TimelineConditionBasecondition  // 0x18
METHODS:
  RVA=0x0B34DEE8  token=0x600046D  System.Void RefreshOnCondition()
  RVA=0x05393520  token=0x600046E  System.Void .ctor()
END_CLASS

