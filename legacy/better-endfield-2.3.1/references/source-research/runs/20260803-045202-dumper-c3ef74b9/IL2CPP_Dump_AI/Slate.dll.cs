// ========================================================
// Dumped by @desirepro
// Assembly: Slate.dll
// Classes:  273
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

CLASS: SubtitlesGUIDelegate
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0AC66EE4  token=0x6000049  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x03A1B910  token=0x600004A  System.Void Invoke(System.String text, UnityEngine.Color color)
  RVA=0x0AC66E5C  token=0x600004B  System.IAsyncResult BeginInvoke(System.String text, UnityEngine.Color color, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x600004C  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: TextOverlayGUIDelegate
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0AC670F4  token=0x600004D  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0AC670AC  token=0x600004E  System.Void Invoke(System.String text, UnityEngine.Color color, System.Single size, UnityEngine.TextAnchor alignment, UnityEngine.Vector2 position)
  RVA=0x0AC66FB8  token=0x600004F  System.IAsyncResult BeginInvoke(System.String text, UnityEngine.Color color, System.Single size, UnityEngine.TextAnchor alignment, UnityEngine.Vector2 position, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000050  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ImageOverlayGUIDelegate
TYPE:  sealed class
TOKEN: 0x2000006
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0AC659E8  token=0x6000051  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0AC659B0  token=0x6000052  System.Void Invoke(UnityEngine.Texture texture, UnityEngine.Color color, UnityEngine.Vector2 scale, UnityEngine.Vector2 position)
  RVA=0x0AC658F0  token=0x6000053  System.IAsyncResult BeginInvoke(UnityEngine.Texture texture, UnityEngine.Color color, UnityEngine.Vector2 scale, UnityEngine.Vector2 position, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000054  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ScreenFadeGUIDelegate
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x07C41054  token=0x6000055  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B0B020  token=0x6000056  System.Void Invoke(UnityEngine.Color color)
  RVA=0x0AC661D4  token=0x6000057  System.IAsyncResult BeginInvoke(UnityEngine.Color color, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000058  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: LetterboxGUIDelegate
TYPE:  sealed class
TOKEN: 0x2000008
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0932FEB0  token=0x6000059  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600005A  System.Void Invoke(System.Single completion)
  RVA=0x0AC65B10  token=0x600005B  System.IAsyncResult BeginInvoke(System.Single completion, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x600005C  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: CameraDissolveDelegate
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0AC5A8A0  token=0x600005D  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x600005E  System.Void Invoke(UnityEngine.Texture texture, System.Single completion)
  RVA=0x0AC5A820  token=0x600005F  System.IAsyncResult BeginInvoke(UnityEngine.Texture texture, System.Single completion, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000060  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: TrackingMode
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.DynamicCameraController.Transposer.TrackingModeNone  // const
  public    static  Slate.DynamicCameraController.Transposer.TrackingModeOffsetTracking  // const
  public    static  Slate.DynamicCameraController.Transposer.TrackingModeRailTracking  // const
METHODS:
END_CLASS

CLASS: OffsetMode
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.DynamicCameraController.Transposer.OffsetModeLocalSpace  // const
  public    static  Slate.DynamicCameraController.Transposer.OffsetModeWorldSpace  // const
METHODS:
END_CLASS

CLASS: Transposer
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x50
FIELDS:
  public            Slate.DynamicCameraController.Transposer.TrackingModetrackingMode  // 0x10
  public            UnityEngine.Transform           target  // 0x18
  public            UnityEngine.Vector3             targetOffset  // 0x20
  public            Slate.DynamicCameraController.Transposer.OffsetModeoffsetMode  // 0x2c
  public            UnityEngine.Vector3             railStart  // 0x30
  public            UnityEngine.Vector3             railEnd  // 0x3c
  public            System.Single                   railOffset  // 0x48
  public            System.Single                   smoothDamping  // 0x4c
METHODS:
  RVA=0x04DA9C30  token=0x600006A  System.Void .ctor()
END_CLASS

CLASS: TrackingMode
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.DynamicCameraController.Composer.TrackingModeNone  // const
  public    static  Slate.DynamicCameraController.Composer.TrackingModeFrameComposition  // const
METHODS:
END_CLASS

CLASS: Composer
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x50
FIELDS:
  public            Slate.DynamicCameraController.Composer.TrackingModetrackingMode  // 0x10
  public            UnityEngine.Transform           target  // 0x18
  public            UnityEngine.Vector3             targetOffset  // 0x20
  public            System.Single                   targetSize  // 0x2c
  public            UnityEngine.Vector2             frameCenter  // 0x30
  public            UnityEngine.Vector2             frameExtends  // 0x38
  public            System.Single                   dutchTilt  // 0x40
  public            System.Boolean                  zoomAtTargetFrame  // 0x44
  public            System.Single                   smoothDamping  // 0x48
METHODS:
  RVA=0x04DA9B90  token=0x600006B  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass47_0
TYPE:  sealed class
TOKEN: 0x2000013
SIZE:  0x18
FIELDS:
  public            System.String                   shotName  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000A8  System.Void .ctor()
  RVA=0x0AC6738C  token=0x60000A9  System.Boolean <Find>b__0(Slate.ShotCamera s)
END_CLASS

CLASS: <>c__DisplayClass19_0
TYPE:  sealed class
TOKEN: 0x2000017
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000CC  System.Void .ctor()
  RVA=0x0AC672D4  token=0x60000CD  System.Boolean <FindExpressionByName>b__0(Slate.BlendShapeGroup x)
END_CLASS

CLASS: <>c__DisplayClass20_0
TYPE:  sealed class
TOKEN: 0x2000018
SIZE:  0x18
FIELDS:
  public            System.String                   UID  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000CE  System.Void .ctor()
  RVA=0x0AC672F0  token=0x60000CF  System.Boolean <FindExpressionByUID>b__0(Slate.BlendShapeGroup x)
END_CLASS

CLASS: BlendInEffectType
TYPE:  sealed struct
TOKEN: 0x2000037
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.CameraShot.BlendInEffectTypeNone  // const
  public    static  Slate.CameraShot.BlendInEffectTypeFadeFromColor  // const
  public    static  Slate.CameraShot.BlendInEffectTypeCrossDissolve  // const
  public    static  Slate.CameraShot.BlendInEffectTypeEaseIn  // const
METHODS:
END_CLASS

CLASS: BlendOutEffectType
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.CameraShot.BlendOutEffectTypeNone  // const
  public    static  Slate.CameraShot.BlendOutEffectTypeFadeToColor  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000039
SIZE:  0x10
FIELDS:
  public    static readonly Slate.CameraShot.<>c            <>9  // static @ 0x0
METHODS:
  RVA=0x0AC67428  token=0x6000115  System.Void .cctor()
  RVA=0x041E1670  token=0x6000116  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000043
SIZE:  0x10
FIELDS:
  public    static readonly Slate.AnimatorTrack.<>c         <>9  // static @ 0x0
  public    static  System.Func<Slate.AnimatorTrack,System.Boolean><>9__36_0  // static @ 0x8
METHODS:
  RVA=0x0AC6748C  token=0x6000177  System.Void .cctor()
  RVA=0x041E1670  token=0x6000178  System.Void .ctor()
  RVA=0x0AC671C8  token=0x6000179  System.Boolean <OnInitialize>b__36_0(Slate.AnimatorTrack t)
END_CLASS

CLASS: SerializationMetaData
TYPE:  class
TOKEN: 0x200004E
SIZE:  0x50
FIELDS:
  public            System.String                   parameterName  // 0x10
  public            System.String                   declaringTypeName  // 0x18
  public            System.String                   transformHierarchyPath  // 0x20
  public            Slate.AnimatedParameter.ParameterTypeparameterType  // 0x28
  private           System.Type                     <declaringType>k__BackingField  // 0x30
  private           System.Reflection.PropertyInfo  <property>k__BackingField  // 0x38
  private           System.Reflection.FieldInfo     <field>k__BackingField  // 0x40
  private           System.Type                     <animatedType>k__BackingField  // 0x48
PROPERTIES:
  declaringType  get=0x011F36E0  set=0x05396200
  property  get=0x04D85A50  set=0x046A91B0
  field  get=0x04D85A60  set=0x042B4AE0
  animatedType  get=0x04D86200  set=0x035A41A0
METHODS:
  RVA=0x0AC66254  token=0x6000272  System.Void Deserialize()
  RVA=0x041E1670  token=0x6000273  System.Void .ctor()
END_CLASS

CLASS: ParameterType
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.AnimatedParameter.ParameterTypeNotSet  // const
  public    static  Slate.AnimatedParameter.ParameterTypeProperty  // const
  public    static  Slate.AnimatedParameter.ParameterTypeField  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass112_0
TYPE:  sealed class
TOKEN: 0x2000050
SIZE:  0x18
FIELDS:
  public            System.Single                   time  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000274  System.Void .ctor()
  RVA=0x0AC67228  token=0x6000275  System.Single <GetKeyLabel>b__0(UnityEngine.AnimationCurve c)
END_CLASS

CLASS: AddParameterDelegate
TYPE:  sealed class
TOKEN: 0x200005D
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0AC5111C  token=0x60002E3  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014F3120  token=0x60002E4  System.Boolean Invoke(System.Type type, System.String memberPath, System.String transformPath)
  RVA=0x05FD2238  token=0x60002E5  System.IAsyncResult BeginInvoke(System.Type type, System.String memberPath, System.String transformPath, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE668  token=0x60002E6  System.Boolean EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: <>c__DisplayClass12_0
TYPE:  sealed class
TOKEN: 0x200005E
SIZE:  0x18
FIELDS:
  public            Slate.AnimatedParameter         newParam  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60002E7  System.Void .ctor()
  RVA=0x0AC6724C  token=0x60002E8  System.Boolean <TryAddParameter>b__0(Slate.AnimatedParameter p)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200005F
SIZE:  0x10
FIELDS:
  public    static readonly Slate.AnimationDataCollection.<>c<>9  // static @ 0x0
  public    static  System.Func<Slate.AnimatedParameter,System.String><>9__14_0  // static @ 0x8
  public    static  System.Func<Slate.AnimatedParameter,System.String><>9__14_1  // static @ 0x10
  public    static  System.Func<System.Single,System.Single><>9__31_1  // static @ 0x18
  public    static  System.Func<System.Single,System.Single><>9__32_1  // static @ 0x20
METHODS:
  RVA=0x0AC673C4  token=0x60002E9  System.Void .cctor()
  RVA=0x041E1670  token=0x60002EA  System.Void .ctor()
  RVA=0x0AC671E8  token=0x60002EB  System.String <ReOrderParameters>b__14_0(Slate.AnimatedParameter p)
  RVA=0x0AC67208  token=0x60002EC  System.String <ReOrderParameters>b__14_1(Slate.AnimatedParameter p)
  RVA=0x04D9A920  token=0x60002ED  System.Single <GetKeyNext>b__31_1(System.Single t)
  RVA=0x04D9A920  token=0x60002EE  System.Single <GetKeyPrevious>b__32_1(System.Single t)
END_CLASS

CLASS: <>c__DisplayClass15_0
TYPE:  sealed class
TOKEN: 0x2000060
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60002EF  System.Void .ctor()
  RVA=0x0AC67274  token=0x60002F0  System.Boolean <GetParameterOfName>b__0(Slate.AnimatedParameter d)
END_CLASS

CLASS: <>c__DisplayClass31_0
TYPE:  sealed class
TOKEN: 0x2000061
SIZE:  0x18
FIELDS:
  public            System.Single                   time  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60002F1  System.Void .ctor()
  RVA=0x0AC6730C  token=0x60002F2  System.Single <GetKeyNext>b__0(Slate.AnimatedParameter p)
  RVA=0x0AC67330  token=0x60002F3  System.Boolean <GetKeyNext>b__2(System.Single t)
END_CLASS

CLASS: <>c__DisplayClass32_0
TYPE:  sealed class
TOKEN: 0x2000062
SIZE:  0x18
FIELDS:
  public            System.Single                   time  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60002F4  System.Void .ctor()
  RVA=0x0AC67338  token=0x60002F5  System.Single <GetKeyPrevious>b__0(Slate.AnimatedParameter p)
  RVA=0x0AC6735C  token=0x60002F6  System.Boolean <GetKeyPrevious>b__2(System.Single t)
END_CLASS

CLASS: <>c__DisplayClass33_0
TYPE:  sealed class
TOKEN: 0x2000063
SIZE:  0x18
FIELDS:
  public            System.Single                   time  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60002F7  System.Void .ctor()
  RVA=0x0AC67368  token=0x60002F8  System.Boolean <GetKeyLabel>b__0(Slate.AnimatedParameter p)
END_CLASS

CLASS: WrapMode
TYPE:  sealed struct
TOKEN: 0x2000065
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.Cutscene.WrapMode         Once  // const
  public    static  Slate.Cutscene.WrapMode         Loop  // const
  public    static  Slate.Cutscene.WrapMode         PingPong  // const
METHODS:
END_CLASS

CLASS: StopMode
TYPE:  sealed struct
TOKEN: 0x2000066
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.Cutscene.StopMode         Skip  // const
  public    static  Slate.Cutscene.StopMode         Rewind  // const
  public    static  Slate.Cutscene.StopMode         Hold  // const
  public    static  Slate.Cutscene.StopMode         SkipRewindNoUndo  // const
METHODS:
END_CLASS

CLASS: UpdateMode
TYPE:  sealed struct
TOKEN: 0x2000067
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.Cutscene.UpdateMode       Normal  // const
  public    static  Slate.Cutscene.UpdateMode       AnimatePhysics  // const
  public    static  Slate.Cutscene.UpdateMode       UnscaledTime  // const
  public    static  Slate.Cutscene.UpdateMode       Manual  // const
METHODS:
END_CLASS

CLASS: PlayingDirection
TYPE:  sealed struct
TOKEN: 0x2000068
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.Cutscene.PlayingDirection Forwards  // const
  public    static  Slate.Cutscene.PlayingDirection Backwards  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000069
SIZE:  0x10
FIELDS:
  public    static readonly Slate.Cutscene.<>c              <>9  // static @ 0x0
  public    static  System.Predicate<Slate.CutsceneGroup><>9__62_0  // static @ 0x8
  public    static  System.Predicate<Slate.CutsceneGroup><>9__64_0  // static @ 0x10
  public    static  System.Predicate<Slate.CutsceneTrack><>9__74_0  // static @ 0x18
  public    static  System.Func<Slate.ActorGroup,UnityEngine.GameObject><>9__135_0  // static @ 0x20
  public    static  System.Func<Slate.IDirectableTimePointer,System.Single><>9__136_0  // static @ 0x28
  public    static  System.Comparison<Slate.IDirectableTimePointer><>9__170_0  // static @ 0x30
  public    static  System.Func<Slate.Section,System.String><>9__190_0  // static @ 0x38
  public    static  System.Func<Slate.IEvent,System.String><>9__191_0  // static @ 0x40
METHODS:
  RVA=0x0AC79E58  token=0x600037A  System.Void .cctor()
  RVA=0x041E1670  token=0x600037B  System.Void .ctor()
  RVA=0x0AC79954  token=0x600037C  System.Boolean <get_directorGroup>b__62_0(Slate.CutsceneGroup g)
  RVA=0x0AC798D4  token=0x600037D  System.Boolean <get_actorGroup>b__64_0(Slate.CutsceneGroup g)
  RVA=0x0AC79914  token=0x600037E  System.Boolean <get_cameraTrack>b__74_0(Slate.CutsceneTrack t)
  RVA=0x0AC796DC  token=0x600037F  UnityEngine.GameObject <GetAffectedActors>b__135_0(Slate.ActorGroup g)
  RVA=0x0AC79738  token=0x6000380  System.Single <GetPointerTimes>b__136_0(Slate.IDirectableTimePointer t)
  RVA=0x0AC79754  token=0x6000381  System.Int32 <InitializeTimePointers>b__170_0(Slate.IDirectableTimePointer a, Slate.IDirectableTimePointer b)
  RVA=0x06C08908  token=0x6000382  System.String <GetSectionNames>b__190_0(Slate.Section s)
  RVA=0x0AC796FC  token=0x6000383  System.String <GetDefinedEventNames>b__191_0(Slate.IEvent d)
END_CLASS

CLASS: <>c__DisplayClass177_0
TYPE:  sealed class
TOKEN: 0x200006A
SIZE:  0x20
FIELDS:
  public            System.Action                   callback  // 0x10
  public            Slate.Cutscene                  instance  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000384  System.Void .ctor()
  RVA=0x0AC79AF0  token=0x6000385  System.Void <Play>b__0()
END_CLASS

CLASS: <>c__DisplayClass182_0
TYPE:  sealed class
TOKEN: 0x200006B
SIZE:  0x18
FIELDS:
  public            System.String                   groupName  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000386  System.Void .ctor()
  RVA=0x0AC79B98  token=0x6000387  System.Boolean <SetGroupActorOfName>b__0(Slate.ActorGroup g)
END_CLASS

CLASS: <>c__DisplayClass184_0
TYPE:  sealed class
TOKEN: 0x200006C
SIZE:  0x18
FIELDS:
  public            System.String[]                 split  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000388  System.Void .ctor()
  RVA=0x0AC79BF4  token=0x6000389  System.Boolean <FindElement>b__0(Slate.CutsceneGroup g)
END_CLASS

CLASS: <Internal_RenderCutscene>d__194
TYPE:  sealed class
TOKEN: 0x200006D
SIZE:  0x50
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Int32                    frameRate  // 0x20
  public            Slate.Cutscene                  <>4__this  // 0x28
  public            System.Int32                    width  // 0x30
  public            System.Int32                    height  // 0x34
  public            System.Action<UnityEngine.Texture2D[]>callback  // 0x38
  private           System.Collections.Generic.List<UnityEngine.Texture2D><renderSequence>5__2  // 0x40
  private           System.Single                   <sampleRate>5__3  // 0x48
  private           System.Single                   <i>5__4  // 0x4c
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600038A  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600038B  System.Void System.IDisposable.Dispose()
  RVA=0x0AC79450  token=0x600038C  System.Boolean MoveNext()
  RVA=0x0AC79690  token=0x600038E  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: ActorReferenceMode
TYPE:  sealed struct
TOKEN: 0x200006F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.CutsceneGroup.ActorReferenceModeUseOriginal  // const
  public    static  Slate.CutsceneGroup.ActorReferenceModeUseInstanceHideOriginal  // const
METHODS:
END_CLASS

CLASS: ActorInitialTransformation
TYPE:  sealed struct
TOKEN: 0x2000070
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.CutsceneGroup.ActorInitialTransformationUseOriginal  // const
  public    static  Slate.CutsceneGroup.ActorInitialTransformationUseLocal  // const
METHODS:
END_CLASS

CLASS: CustomToggleInfo
TYPE:  class
TOKEN: 0x2000071
SIZE:  0x30
FIELDS:
  public            System.Int32                    startIndex  // 0x10
  public            System.Int32                    endIndex  // 0x14
  public            System.String                   desc  // 0x18
  public            System.Boolean                  isCollapsed  // 0x20
  private           System.Int32                    <index>k__BackingField  // 0x24
  private           Slate.CutsceneGroup             <group>k__BackingField  // 0x28
PROPERTIES:
  index  get=0x011F0020  set=0x011F0030
  group  get=0x04D86240  set=0x02FDB880
METHODS:
  RVA=0x0AC6C4A4  token=0x60003D8  System.Void _OnToggleChanged()
  RVA=0x0AC6C354  token=0x60003D9  System.Collections.IEnumerable _GetAllTrackNames()
  RVA=0x04D9E080  token=0x60003DA  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000072
SIZE:  0x10
FIELDS:
  public    static readonly Slate.CutsceneGroup.<>c         <>9  // static @ 0x0
  public    static  System.Func<Slate.CutsceneGroup.CustomToggleInfo,System.Int32><>9__48_0  // static @ 0x8
  public    static  System.Func<Slate.CutsceneTrack,System.Boolean><>9__88_0  // static @ 0x10
  public    static  System.Func<Slate.Section,System.Single><>9__88_1  // static @ 0x18
METHODS:
  RVA=0x0AC79F20  token=0x60003DB  System.Void .cctor()
  RVA=0x041E1670  token=0x60003DC  System.Void .ctor()
  RVA=0x0AA421AC  token=0x60003DD  System.Int32 <_OnCustomToggleValueChanged>b__48_0(Slate.CutsceneGroup.CustomToggleInfo t)
  RVA=0x0AC7986C  token=0x60003DE  System.Boolean <Validate>b__88_0(Slate.CutsceneTrack t)
  RVA=0x0AC798B8  token=0x60003DF  System.Single <Validate>b__88_1(Slate.Section s)
END_CLASS

CLASS: <>c__DisplayClass89_0
TYPE:  sealed class
TOKEN: 0x2000073
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003E0  System.Void .ctor()
  RVA=0x0AC79CC0  token=0x60003E1  System.Boolean <GetSectionByName>b__0(Slate.Section s)
END_CLASS

CLASS: <>c__DisplayClass90_0
TYPE:  sealed class
TOKEN: 0x2000074
SIZE:  0x18
FIELDS:
  public            System.String                   UID  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003E2  System.Void .ctor()
  RVA=0x06CC7538  token=0x60003E3  System.Boolean <GetSectionByUID>b__0(Slate.Section s)
END_CLASS

CLASS: <>c__DisplayClass91_0
TYPE:  sealed class
TOKEN: 0x2000075
SIZE:  0x18
FIELDS:
  public            System.Single                   time  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003E4  System.Void .ctor()
  RVA=0x0AC79D88  token=0x60003E5  System.Boolean <GetSectionAfter>b__0(Slate.Section s)
END_CLASS

CLASS: <>c__DisplayClass92_0
TYPE:  sealed class
TOKEN: 0x2000076
SIZE:  0x18
FIELDS:
  public            System.Single                   time  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003E6  System.Void .ctor()
  RVA=0x0AC79DD4  token=0x60003E7  System.Boolean <GetSectionBefore>b__0(Slate.Section s)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000078
SIZE:  0x10
FIELDS:
  public    static readonly Slate.CutsceneTrack.<>c         <>9  // static @ 0x0
  public    static  System.Func<Slate.ActionClip,System.Single><>9__72_0  // static @ 0x8
METHODS:
  RVA=0x0AC79EBC  token=0x600042A  System.Void .cctor()
  RVA=0x041E1670  token=0x600042B  System.Void .ctor()
  RVA=0x0AC79850  token=0x600042C  System.Single <Validate>b__72_0(Slate.ActionClip a)
END_CLASS

CLASS: <>c__DisplayClass92_0
TYPE:  sealed class
TOKEN: 0x2000079
SIZE:  0x18
FIELDS:
  public            Slate.ActionClip                newAction  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600042D  System.Void .ctor()
  RVA=0x0AC79DA8  token=0x600042E  System.Boolean <AddAction>b__0(Slate.ActionClip a)
END_CLASS

CLASS: <>c__DisplayClass12_0
TYPE:  sealed class
TOKEN: 0x200007F
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000483  System.Void .ctor()
  RVA=0x0AC799F4  token=0x6000484  System.Boolean <FindChild>b__0(Slate.IDirectable d)
END_CLASS

CLASS: <>c__DisplayClass14_0
TYPE:  sealed class
TOKEN: 0x2000080
SIZE:  0x18
FIELDS:
  public            Slate.IDirectable               directable  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000485  System.Void .ctor()
  RVA=0x0AC79A50  token=0x6000486  System.Boolean <GetPreviousSibling>b__0(Slate.IDirectable d)
END_CLASS

CLASS: <>c__DisplayClass16_0
TYPE:  sealed class
TOKEN: 0x2000081
SIZE:  0x18
FIELDS:
  public            Slate.IDirectable               directable  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000487  System.Void .ctor()
  RVA=0x0AC79AA0  token=0x6000488  System.Boolean <GetNextSibling>b__0(Slate.IDirectable d)
END_CLASS

CLASS: ExitMode
TYPE:  sealed struct
TOKEN: 0x2000084
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.Section.ExitMode          Continue  // const
  public    static  Slate.Section.ExitMode          Loop  // const
METHODS:
END_CLASS

CLASS: HandleStyle
TYPE:  sealed struct
TOKEN: 0x200008A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.BezierPoint.HandleStyle   Connected  // const
  public    static  Slate.BezierPoint.HandleStyle   Broken  // const
METHODS:
END_CLASS

CLASS: SampleSettings
TYPE:  sealed struct
TOKEN: 0x200008F
SIZE:  0x24
FIELDS:
  public            System.Single                   volume  // 0x10
  public            System.Single                   pitch  // 0x14
  public            System.Single                   pan  // 0x18
  public            System.Single                   spatialBlend  // 0x1c
  public            System.Boolean                  ignoreTimescale  // 0x20
  public            System.Boolean                  bypassReverb  // 0x21
METHODS:
  RVA=0x04DA9CC0  token=0x60004FB  Slate.AudioSampler.SampleSettings Default()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000093
SIZE:  0x18
FIELDS:
  public            System.Single                   time  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000506  System.Void .ctor()
  RVA=0x0AC79C68  token=0x6000507  System.Boolean <GetKeyNext>b__1(UnityEngine.Keyframe k)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000094
SIZE:  0x10
FIELDS:
  public    static readonly Slate.CurveUtility.<>c          <>9  // static @ 0x0
  public    static  System.Func<UnityEngine.Keyframe,System.Single><>9__1_0  // static @ 0x8
  public    static  System.Func<UnityEngine.Keyframe,System.Single><>9__2_0  // static @ 0x10
METHODS:
  RVA=0x0AC79DF4  token=0x6000508  System.Void .cctor()
  RVA=0x041E1670  token=0x6000509  System.Void .ctor()
  RVA=0x04DA9D30  token=0x600050A  System.Single <GetKeyNext>b__1_0(UnityEngine.Keyframe k)
  RVA=0x04DA9D30  token=0x600050B  System.Single <GetKeyPrevious>b__2_0(UnityEngine.Keyframe k)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000095
SIZE:  0x18
FIELDS:
  public            System.Single                   time  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600050C  System.Void .ctor()
  RVA=0x0AC79C84  token=0x600050D  System.Boolean <GetKeyPrevious>b__1(UnityEngine.Keyframe k)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000096
SIZE:  0x20
FIELDS:
  public            System.Single                   time  // 0x10
  public            System.Func<UnityEngine.Keyframe,System.Boolean><>9__0  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600050E  System.Void .ctor()
  RVA=0x0AC79C9C  token=0x600050F  System.Boolean <HasKey>b__0(UnityEngine.Keyframe k)
END_CLASS

CLASS: <>c__DisplayClass8_0
TYPE:  sealed class
TOKEN: 0x20000A0
SIZE:  0x20
FIELDS:
  public            System.Type                     type  // 0x10
  public            System.Func<System.Type,System.Boolean><>9__0  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600056D  System.Void .ctor()
  RVA=0x0AC79D14  token=0x600056E  System.Boolean <GetImplementationsOf>b__0(System.Type t)
END_CLASS

CLASS: <>c__DisplayClass28_0`2
TYPE:  sealed class
TOKEN: 0x20000A1
FIELDS:
  public            System.Reflection.MemberInfo    info  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600056F  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000570  System.Void <GetFieldOrPropSetter>b__0(T x, TValue v)
END_CLASS

CLASS: StoreMode
TYPE:  sealed struct
TOKEN: 0x20000A6
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.TransformSnapshot.StoreModeAll  // const
  public    static  Slate.TransformSnapshot.StoreModeRootOnly  // const
  public    static  Slate.TransformSnapshot.StoreModeChildrenOnly  // const
METHODS:
END_CLASS

CLASS: TransformData
TYPE:  sealed struct
TOKEN: 0x20000A7
SIZE:  0x48
FIELDS:
  public            UnityEngine.Transform           transform  // 0x10
  public            UnityEngine.Transform           parent  // 0x18
  public            UnityEngine.Vector3             pos  // 0x20
  public            UnityEngine.Quaternion          rot  // 0x2c
  public            UnityEngine.Vector3             scale  // 0x3c
METHODS:
  RVA=0x0AC788AC  token=0x600058C  System.Void .ctor(UnityEngine.Transform transform, UnityEngine.Transform parent, UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 scale)
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed class
TOKEN: 0x20000A9
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600059B  System.Void .ctor()
  RVA=0x0AC79994  token=0x600059C  System.Boolean <FindInChildren>b__0(UnityEngine.Transform t)
END_CLASS

CLASS: VideoRenderTarget
TYPE:  sealed struct
TOKEN: 0x20000AB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.VideoSampler.VideoRenderTargetCameraBackground  // const
  public    static  Slate.VideoSampler.VideoRenderTargetCameraForeground  // const
METHODS:
END_CLASS

CLASS: SampleSettings
TYPE:  sealed struct
TOKEN: 0x20000AC
SIZE:  0x24
FIELDS:
  public            Slate.VideoSampler.VideoRenderTargetrenderTarget  // 0x10
  public            System.Single                   playbackSpeed  // 0x14
  public            System.Single                   alpha  // 0x18
  public            UnityEngine.Video.VideoAspectRatioaspectRatio  // 0x1c
  public            System.Single                   audioVolume  // 0x20
METHODS:
  RVA=0x04DA9CE0  token=0x60005A2  Slate.VideoSampler.SampleSettings Default()
END_CLASS

CLASS: StartingTransformsMode
TYPE:  sealed struct
TOKEN: 0x2000103
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.ActionClips.PlayAnimatorClip.StartingTransformsModeAutoMatchTransforms  // const
  public    static  Slate.ActionClips.PlayAnimatorClip.StartingTransformsModeManualSetTransforms  // const
METHODS:
END_CLASS

CLASS: ClipWrapMode
TYPE:  sealed struct
TOKEN: 0x2000104
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.ActionClips.PlayAnimatorClip.ClipWrapModeLoop  // const
  public    static  Slate.ActionClips.PlayAnimatorClip.ClipWrapModePingPong  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200010B
SIZE:  0x10
FIELDS:
  public    static readonly Slate.ActionClips.AnimateLimbIK.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x0AC87854  token=0x60008BA  System.Void .cctor()
  RVA=0x041E1670  token=0x60008BB  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200010D
SIZE:  0x10
FIELDS:
  public    static readonly Slate.ActionClips.AnimateLookAtIK.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x0AC877F0  token=0x60008CE  System.Void .cctor()
  RVA=0x041E1670  token=0x60008CF  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000111
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60008DF  System.Void .ctor()
  RVA=0x06CC7538  token=0x60008E0  System.Boolean <HasParameter>b__0(UnityEngine.AnimatorControllerParameter p)
END_CLASS

CLASS: Slate.DirectorCamera
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Slate.IDirectableCamera
FIELDS:
  private           System.Boolean                  _matchMainWhenActive  // 0x18
  private           System.Boolean                  _setMainWhenActive  // 0x19
  private           System.Boolean                  _autoHandleActiveState  // 0x1a
  private           System.Boolean                  _ignoreFOVChanges  // 0x1b
  private           System.Boolean                  _dontDestroyOnLoad  // 0x1c
  public    static  System.Single                   MAX_DAMP  // const
  private   static  System.Action<Slate.IDirectableCamera>onCut  // static @ 0x0
  private   static  System.Action                   onActivate  // static @ 0x8
  private   static  System.Action                   onDeactivate  // static @ 0x10
  private   static  Slate.DirectorCamera            _current  // static @ 0x18
  private   static  UnityEngine.Camera              _cam  // static @ 0x20
  private   static  Slate.IDirectableCamera         lastTargetShot  // static @ 0x28
  private           System.Single                   <focalDistance>k__BackingField  // 0x20
  private           System.Single                   <focalLength>k__BackingField  // 0x24
  private           System.Single                   <focalAperture>k__BackingField  // 0x28
  private   static  Slate.GameCamera                <gameCamera>k__BackingField  // static @ 0x30
  private   static  System.Boolean                  <isEnabled>k__BackingField  // static @ 0x38
  private   static  System.Single                   noiseTimer  // static @ 0x3c
  private   static  UnityEngine.Vector3             noisePosOffset  // static @ 0x40
  private   static  UnityEngine.Vector3             noiseRotOffset  // static @ 0x4c
  private   static  UnityEngine.Vector3             noiseTargetPosOffset  // static @ 0x58
  private   static  UnityEngine.Vector3             noiseTargetRotOffset  // static @ 0x64
  private   static  UnityEngine.Vector3             noiseCamPosVel  // static @ 0x70
  private   static  UnityEngine.Vector3             noiseCamRotVel  // static @ 0x7c
PROPERTIES:
  current  get=0x0AC62EEC
  cam  get=0x0AC62DBC
  position  get=0x0AC63200  set=0x0AC63668
  rotation  get=0x0AC63278  set=0x0AC636BC
  fieldOfView  get=0x0AC630F0  set=0x0AC63528
  focalDistance  get=0x011EC8A0  set=0x011EC9E0
  focalLength  get=0x04D89850  set=0x04D89860
  focalAperture  get=0x04D86BF0  set=0x04D86C00
  matchMainWhenActive  get=0x0AC631E0  set=0x0AC63644
  setMainWhenActive  get=0x0AC632C4  set=0x0AC63708
  autoHandleActiveState  get=0x0AC62D9C  set=0x0AC634E0
  ignoreFOVChanges  get=0x0AC63188  set=0x0AC635E4
  dontDestroyOnLoad  get=0x0AC630D0  set=0x0AC63504
  renderCamera  get=0x0AC63254
  gameCamera  get=0x0AC63150  set=0x0AC63590
  isEnabled  get=0x0AC631A8  set=0x0AC63608
EVENTS:
  onCut  add=add_onCut  remove=remove_onCut
  onActivate  add=add_onActivate  remove=remove_onActivate
  onDeactivate  add=add_onDeactivate  remove=remove_onDeactivate
METHODS:
  RVA=0x0AC61CC8  token=0x6000024  System.Void Awake()
  RVA=0x0AC61DEC  token=0x6000025  UnityEngine.Camera CreateRenderCamera()
  RVA=0x0AC620AC  token=0x6000026  System.Void Enable()
  RVA=0x0AC61F84  token=0x6000027  System.Void Disable()
  RVA=0x0AC624C8  token=0x6000028  System.Void Update(Slate.IDirectableCamera source, Slate.IDirectableCamera target, Slate.EaseType interpolation, System.Single weight, System.Single damping)
  RVA=0x0AC618DC  token=0x6000029  System.Void ApplyNoise(System.Single magnitude, System.Single weight)
  RVA=0x04D77850  token=0x600002A  System.Void .ctor()
  RVA=0x053916BC  token=0x600002B  UnityEngine.GameObject Slate.IDirectableCamera.get_gameObject()
END_CLASS

CLASS: Slate.DirectorGUI
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  Slate.DirectorGUI.SubtitlesGUIDelegateOnSubtitlesGUI  // static @ 0x0
  private   static  Slate.DirectorGUI.TextOverlayGUIDelegateOnTextOverlayGUI  // static @ 0x8
  private   static  Slate.DirectorGUI.ImageOverlayGUIDelegateOnImageOverlayGUI  // static @ 0x10
  private   static  Slate.DirectorGUI.ScreenFadeGUIDelegateOnScreenFadeGUI  // static @ 0x18
  private   static  Slate.DirectorGUI.LetterboxGUIDelegateOnLetterboxGUI  // static @ 0x20
  private   static  Slate.DirectorGUI.CameraDissolveDelegateOnCameraDissolve  // static @ 0x28
  private   static  System.Action                   OnGUIEnable  // static @ 0x30
  private   static  System.Action                   OnGUIDisable  // static @ 0x38
  private   static  Slate.DirectorGUI               _current  // static @ 0x40
  private   static  UnityEngine.Color               <lastFadeColor>k__BackingField  // static @ 0x48
PROPERTIES:
  current  get=0x0AC641D8
  lastFadeColor  get=0x0AC64364  set=0x0AC64920
EVENTS:
  OnSubtitlesGUI  add=add_OnSubtitlesGUI  remove=remove_OnSubtitlesGUI
  OnTextOverlayGUI  add=add_OnTextOverlayGUI  remove=remove_OnTextOverlayGUI
  OnImageOverlayGUI  add=add_OnImageOverlayGUI  remove=remove_OnImageOverlayGUI
  OnScreenFadeGUI  add=add_OnScreenFadeGUI  remove=remove_OnScreenFadeGUI
  OnLetterboxGUI  add=add_OnLetterboxGUI  remove=remove_OnLetterboxGUI
  OnCameraDissolve  add=add_OnCameraDissolve  remove=remove_OnCameraDissolve
  OnGUIEnable  add=add_OnGUIEnable  remove=remove_OnGUIEnable
  OnGUIDisable  add=add_OnGUIDisable  remove=remove_OnGUIDisable
METHODS:
  RVA=0x0AC6372C  token=0x600003D  System.Void Awake()
  RVA=0x0AC63920  token=0x600003E  System.Void OnEnable()
  RVA=0x0AC6380C  token=0x600003F  System.Void OnDisable()
  RVA=0x0AC63A58  token=0x6000040  System.Void UpdateLetterbox(System.Single completion)
  RVA=0x0AC63964  token=0x6000041  System.Void UpdateDissolve(UnityEngine.Texture texture, System.Single completion)
  RVA=0x0AC639C8  token=0x6000044  System.Void UpdateFade(UnityEngine.Color color)
  RVA=0x0AC63BF4  token=0x6000045  System.Void UpdateSubtitles(System.String text, UnityEngine.Color color)
  RVA=0x0AC63B50  token=0x6000046  System.Void UpdateOverlayText(System.String text, UnityEngine.Color color, System.Single size, UnityEngine.TextAnchor anchor, UnityEngine.Vector2 pos)
  RVA=0x0AC63AB4  token=0x6000047  System.Void UpdateOverlayImage(UnityEngine.Texture texture, UnityEngine.Color color, UnityEngine.Vector2 scale, UnityEngine.Vector2 positionOffset)
  RVA=0x05393520  token=0x6000048  System.Void .ctor()
END_CLASS

CLASS: Slate.DynamicCameraController
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x28
FIELDS:
  private   static  System.Single                   MIN_DAMP  // const
  private   static  System.Single                   MAX_DAMP  // const
  private           Slate.DynamicCameraController.Transposer_transposer  // 0x10
  private           Slate.DynamicCameraController.Composer_composer  // 0x18
  private           System.Int32                    lastUpdateFrame  // 0x20
PROPERTIES:
  transposer  get=0x02B2ECC0
  composer  get=0x0385B100
  controlsPosition  get=0x0AC65748
  controlsRotation  get=0x0AC65760
  controlsFieldOfView  get=0x0AC6572C
METHODS:
  RVA=0x0AC649FC  token=0x6000066  System.Void UpdateControllerHard(Slate.IDirectableCamera directableCamera, Slate.IDirectable directable)
  RVA=0x0AC64A18  token=0x6000067  System.Void UpdateControllerSoft(Slate.IDirectableCamera directableCamera, Slate.IDirectable directable)
  RVA=0x0AC64A34  token=0x6000068  System.Void UpdateController(Slate.IDirectableCamera directableCamera, Slate.IDirectable directable, System.Boolean isHard)
  RVA=0x0AC65680  token=0x6000069  System.Void .ctor()
END_CLASS

CLASS: Slate.GameCamera
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Slate.IDirectableCamera
FIELDS:
  private           UnityEngine.Camera              _cam  // 0x18
PROPERTIES:
  cam  get=0x0AC65778
  position  get=0x0AC65864  set=0x041E1670
  rotation  get=0x0AC658AC  set=0x041E1670
  fieldOfView  get=0x0AC65804  set=0x041E1670
  focalDistance  get=0x04DA9C10  set=0x041E1670
  focalLength  get=0x04DA9C20  set=0x041E1670
  focalAperture  get=0x04DA9C00  set=0x041E1670
METHODS:
  RVA=0x05393520  token=0x6000079  System.Void .ctor()
  RVA=0x053916BC  token=0x600007A  UnityEngine.GameObject Slate.IDirectableCamera.get_gameObject()
END_CLASS

CLASS: Slate.IDirectableCamera
TYPE:  interface
TOKEN: 0x2000011
FIELDS:
PROPERTIES:
  gameObject  get=-1  // abstract
  cam  get=-1  // abstract
  position  get=-1  // abstract  set=-1  // abstract
  rotation  get=-1  // abstract  set=-1  // abstract
  fieldOfView  get=-1  // abstract  set=-1  // abstract
  focalDistance  get=-1  // abstract  set=-1  // abstract
  focalLength  get=-1  // abstract  set=-1  // abstract
  focalAperture  get=-1  // abstract  set=-1  // abstract
METHODS:
END_CLASS

CLASS: Slate.ShotCamera
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Slate.IDirectableCamera
FIELDS:
  public    static  System.String                   DEFAULT_NAME  // const
  private           Slate.DynamicCameraController   _dynamicController  // 0x18
  private           System.Single                   _focalDistance  // 0x20
  private           System.Single                   _focalLength  // 0x24
  private           System.Single                   _focalAperture  // 0x28
  private           UnityEngine.Camera              _cam  // 0x30
PROPERTIES:
  cam  get=0x0AC66B18
  position  get=0x0AC65864  set=0x0AC66E18
  rotation  get=0x0AC658AC  set=0x06C5993C
  localPosition  get=0x0AC66CC0  set=0x0AC66DD4
  localEulerAngles  get=0x0AC66C4C  set=0x0AC66D60
  fieldOfView  get=0x0AC66BEC  set=0x0AC66D08
  focalDistance  get=0x011EC8A0  set=0x011EC9E0
  focalLength  get=0x04D89850  set=0x04D89860
  focalAperture  get=0x04D86BF0  set=0x04D86C00
  dynamicController  get=0x0385B100
  dynamicControlledPosition  get=0x0AC66BBC
  dynamicControlledRotation  get=0x0AC66BD4
  dynamicControlledFieldOfView  get=0x0AC66BA4
METHODS:
  RVA=0x0AC663CC  token=0x600009E  System.Void Awake()
  RVA=0x0AC66A44  token=0x600009F  System.Void UpdateDynamicControllerHard(Slate.IDirectable directable)
  RVA=0x0AC66A6C  token=0x60000A0  System.Void UpdateDynamicControllerSoft(Slate.IDirectable directable)
  RVA=0x0AC66A38  token=0x60000A1  System.Void SetDynamicControllerTargets(UnityEngine.Transform target)
  RVA=0x0AC669E4  token=0x60000A2  System.Void SetDynamicControllerTargets(UnityEngine.Transform transposerTarget, UnityEngine.Transform composerTarget)
  RVA=0x0AC66840  token=0x60000A3  UnityEngine.RenderTexture GetRenderTexture(System.Int32 width, System.Int32 height)
  RVA=0x0AC664C0  token=0x60000A4  Slate.ShotCamera Create(UnityEngine.Transform targetParent)
  RVA=0x0AC66748  token=0x60000A5  Slate.ShotCamera Find(System.String shotName)
  RVA=0x0AC66A94  token=0x60000A6  System.Void .ctor()
  RVA=0x053916BC  token=0x60000A7  UnityEngine.GameObject Slate.IDirectableCamera.get_gameObject()
END_CLASS

CLASS: Slate.BlendShape
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x28
FIELDS:
  private           UnityEngine.SkinnedMeshRenderer _skin  // 0x10
  private           System.String                   _name  // 0x18
  private           System.Single                   _weight  // 0x20
PROPERTIES:
  skin  get=0x02B2ECC0  set=0x053908C0
  name  get=0x0385B100  set=0x05392C40
  weight  get=0x011EC8A0  set=0x011EC9E0
METHODS:
  RVA=0x0AC5A754  token=0x60000B0  System.Void SetRealWeight(System.Single modWeight)
  RVA=0x0385B100  token=0x60000B1  System.String ToString()
  RVA=0x041E1670  token=0x60000B2  System.Void .ctor()
END_CLASS

CLASS: Slate.BlendShapeGroup
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x30
FIELDS:
  private           System.String                   _UID  // 0x10
  private           System.String                   _name  // 0x18
  private           System.Single                   _weight  // 0x20
  private           System.Collections.Generic.List<Slate.BlendShape>_blendShapes  // 0x28
PROPERTIES:
  UID  get=0x02B2ECC0  set=0x053908C0
  name  get=0x0385B100  set=0x05392C40
  weight  get=0x011EC8A0  set=0x0AC5A748
  blendShapes  get=0x04D86240
METHODS:
  RVA=0x0AC5A67C  token=0x60000BA  System.Void .ctor()
  RVA=0x0AC5A5FC  token=0x60000BB  System.Void SetBlendWeights()
  RVA=0x0385B100  token=0x60000BC  System.String ToString()
END_CLASS

CLASS: Slate.Character
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<Slate.BlendShapeGroup>_expressions  // 0x18
  private           UnityEngine.Transform           _neckTransform  // 0x20
  private           UnityEngine.Transform           _headTransform  // 0x28
  private           UnityEngine.Vector3             _upVector  // 0x30
  private           UnityEngine.Vector3             _rotationOffset  // 0x3c
PROPERTIES:
  neck  get=0x04D862C0  set=0x05390F40
  head  get=0x04D86240  set=0x02FDB880
  upVector  get=0x04D88ED0  set=0x04D88F10
  rotationOffset  get=0x04D88EB0  set=0x04D88F00
  expressions  get=0x0385B100
METHODS:
  RVA=0x0AC5C730  token=0x60000C6  Slate.BlendShapeGroup FindExpressionByName(System.String name)
  RVA=0x0AC5C800  token=0x60000C7  Slate.BlendShapeGroup FindExpressionByUID(System.String UID)
  RVA=0x0AC5C950  token=0x60000C8  System.Void SetExpressionWeightByName(System.String name, System.Single weight)
  RVA=0x0AC5C984  token=0x60000C9  System.Void SetExpressionWeightByUID(System.String UID, System.Single weight)
  RVA=0x0AC5C8D0  token=0x60000CA  System.Void ResetExpressions()
  RVA=0x0AC5C9B8  token=0x60000CB  System.Void .ctor()
END_CLASS

CLASS: Slate.NameAttribute
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   name  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60000D0  System.Void .ctor(System.String name)
END_CLASS

CLASS: Slate.CategoryAttribute
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   category  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60000D1  System.Void .ctor(System.String category)
END_CLASS

CLASS: Slate.DescriptionAttribute
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   description  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60000D2  System.Void .ctor(System.String description)
END_CLASS

CLASS: Slate.IconAttribute
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   iconName  // 0x10
  public    readonly System.Type                     fromType  // 0x18
METHODS:
  RVA=0x053908C0  token=0x60000D3  System.Void .ctor(System.String iconName)
  RVA=0x05392C40  token=0x60000D4  System.Void .ctor(System.Type fromType)
END_CLASS

CLASS: Slate.AttachableAttribute
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Type[]                   types  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60000D5  System.Void .ctor(System.Type[] types)
END_CLASS

CLASS: Slate.UniqueElementAttribute
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60000D6  System.Void .ctor()
END_CLASS

CLASS: Slate.ShowTrajectoryAttribute
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60000D7  System.Void .ctor()
END_CLASS

CLASS: Slate.PositionHandleAttribute
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60000D8  System.Void .ctor()
END_CLASS

CLASS: Slate.RotationHandleAttribute
TYPE:  class
TOKEN: 0x2000021
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   positionPropertyName  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60000D9  System.Void .ctor(System.String positionPropertyName)
END_CLASS

CLASS: Slate.AnimatableParameterAttribute
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x30
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public            System.String                   link  // 0x10
  public    readonly System.Nullable<System.Single>  min  // 0x18
  public    readonly System.Nullable<System.Single>  max  // 0x20
  public    readonly System.String                   customName  // 0x28
METHODS:
  RVA=0x041E1670  token=0x60000DA  System.Void .ctor()
  RVA=0x02FDB880  token=0x60000DB  System.Void .ctor(System.String customName)
  RVA=0x0AC51244  token=0x60000DC  System.Void .ctor(System.String customName, System.Single min, System.Single max)
  RVA=0x0AC512D8  token=0x60000DD  System.Void .ctor(System.Single min, System.Single max)
END_CLASS

CLASS: Slate.ParseAnimatableParametersAttribute
TYPE:  class
TOKEN: 0x2000023
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60000DE  System.Void .ctor()
END_CLASS

CLASS: Slate.ShaderPropertyPopupAttribute
TYPE:  class
TOKEN: 0x2000024
SIZE:  0x18
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public    readonly System.Type                     propertyType  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000DF  System.Void .ctor()
  RVA=0x053908C0  token=0x60000E0  System.Void .ctor(System.Type propertyType)
END_CLASS

CLASS: Slate.LeftToggleAttribute
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60000E1  System.Void .ctor()
END_CLASS

CLASS: Slate.MinAttribute
TYPE:  class
TOKEN: 0x2000026
SIZE:  0x18
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public    readonly System.Single                   min  // 0x10
METHODS:
  RVA=0x04D88280  token=0x60000E2  System.Void .ctor(System.Single min)
END_CLASS

CLASS: Slate.ExampleTextAttribute
TYPE:  class
TOKEN: 0x2000027
SIZE:  0x18
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public    readonly System.String                   text  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60000E3  System.Void .ctor(System.String text)
END_CLASS

CLASS: Slate.HelpBoxAttribute
TYPE:  class
TOKEN: 0x2000028
SIZE:  0x18
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public    readonly System.String                   text  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60000E4  System.Void .ctor(System.String text)
END_CLASS

CLASS: Slate.ShowIfAttribute
TYPE:  class
TOKEN: 0x2000029
SIZE:  0x20
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public    readonly System.String                   propertyName  // 0x10
  public    readonly System.Int32                    value  // 0x18
METHODS:
  RVA=0x03B46FF0  token=0x60000E5  System.Void .ctor(System.String propertyName, System.Int32 value)
END_CLASS

CLASS: Slate.EnabledIfAttribute
TYPE:  class
TOKEN: 0x200002A
SIZE:  0x20
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public    readonly System.String                   propertyName  // 0x10
  public    readonly System.Int32                    value  // 0x18
METHODS:
  RVA=0x03B46FF0  token=0x60000E6  System.Void .ctor(System.String propertyName, System.Int32 value)
END_CLASS

CLASS: Slate.CallbackAttribute
TYPE:  class
TOKEN: 0x200002B
SIZE:  0x18
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public    readonly System.String                   methodName  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60000E7  System.Void .ctor(System.String methodName)
END_CLASS

CLASS: Slate.RequiredAttribute
TYPE:  class
TOKEN: 0x200002C
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60000E8  System.Void .ctor()
END_CLASS

CLASS: Slate.PlaybackProtectedAttribute
TYPE:  class
TOKEN: 0x200002D
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60000E9  System.Void .ctor()
END_CLASS

CLASS: Slate.ReadOnlyAttribute
TYPE:  class
TOKEN: 0x200002E
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60000EA  System.Void .ctor()
END_CLASS

CLASS: Slate.SortingLayerAttribute
TYPE:  class
TOKEN: 0x200002F
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60000EB  System.Void .ctor()
END_CLASS

CLASS: Slate.ActorGroupPopupAttribute
TYPE:  class
TOKEN: 0x2000030
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60000EC  System.Void .ctor()
END_CLASS

CLASS: Slate.DirectorActionClip
TYPE:  abstract class
TOKEN: 0x2000031
SIZE:  0x40
EXTENDS: Slate.ActionClip
FIELDS:
METHODS:
  RVA=0x05393520  token=0x60000ED  System.Void .ctor()
END_CLASS

CLASS: Slate.IEvent
TYPE:  interface
TOKEN: 0x2000032
FIELDS:
PROPERTIES:
  name  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60000EF  System.Void Invoke()
END_CLASS

CLASS: Slate.TransformSpace
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.TransformSpace            CutsceneSpace  // const
  public    static  Slate.TransformSpace            ActorSpace  // const
  public    static  Slate.TransformSpace            WorldSpace  // const
  public    static  Slate.TransformSpace            ParentSpace  // const
METHODS:
END_CLASS

CLASS: Slate.MiniTransformSpace
TYPE:  sealed struct
TOKEN: 0x2000034
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.MiniTransformSpace        CutsceneSpace  // const
  public    static  Slate.MiniTransformSpace        WorldSpace  // const
  public    static  Slate.MiniTransformSpace        ParentSpace  // const
METHODS:
END_CLASS

CLASS: Slate.ActiveState
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.ActiveState               Disable  // const
  public    static  Slate.ActiveState               Enable  // const
  public    static  Slate.ActiveState               Toggle  // const
METHODS:
END_CLASS

CLASS: Slate.CameraShot
TYPE:  class
TOKEN: 0x2000036
SIZE:  0xA8
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  private           Slate.ShotCamera                _targetShot  // 0x50
  public            Slate.CameraShot.BlendInEffectTypeblendInEffect  // 0x58
  public            Slate.CameraShot.BlendOutEffectTypeblendOutEffect  // 0x5c
  public            System.Single                   steadyCamEffect  // 0x60
  public            UnityEngine.Color               fadeToColor  // 0x64
  public            UnityEngine.Color               fadeFromColor  // 0x74
  public            Slate.ActorGroup                overrideShotTargetActorGroup  // 0x88
  private           UnityEngine.Color               lastFadeColor  // 0x90
  private           Slate.CameraShot                <previousShot>k__BackingField  // 0xa0
PROPERTIES:
  info  get=0x0AC5B6E8
  isValid  get=0x0AC5B768
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
  canCrossBlend  get=0x0AC5B51C
  actor  get=0x0AC5B4A8
  previousShot  get=0x04D86210  set=0x05391334
  targetShot  get=0x04D86270  set=0x0AC5BC48
  track  get=0x0AC5B8E8
  position  get=0x0AC5B7B8  set=0x0AC5BB28
  rotation  get=0x0AC5B850  set=0x0AC5BBB8
  fieldOfView  get=0x0AC5B524  set=0x0AC5B924
  focalPoint  get=0x0AC5B608  set=0x0AC5BA38
  focalRange  get=0x0AC5B678  set=0x0AC5BAB0
  focalAperture  get=0x0AC5B598  set=0x0AC5B9C0
METHODS:
  RVA=0x0AC5A9C8  token=0x600010B  System.Void OnAfterValidate()
  RVA=0x0AC5AF9C  token=0x600010C  System.Void OnRootEnabled()
  RVA=0x0AC5AF10  token=0x600010D  System.Void OnRootDisabled()
  RVA=0x0AC5B028  token=0x600010E  System.Void OnRootUpdated(System.Single time, System.Single previousTime)
  RVA=0x0AC5B384  token=0x600010F  System.Void TryUpdateShotTargetOverride()
  RVA=0x0AC5AE98  token=0x6000110  System.Boolean OnInitialize()
  RVA=0x0AC5ADAC  token=0x6000111  System.Void OnEnter()
  RVA=0x0AC5B0B4  token=0x6000112  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x0AC5AEAC  token=0x6000113  System.Void OnReverse()
  RVA=0x0AC5B458  token=0x6000114  System.Void .ctor()
END_CLASS

CLASS: Slate.PlayVideo
TYPE:  class
TOKEN: 0x200003A
SIZE:  0x70
EXTENDS: Slate.ActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            UnityEngine.Video.VideoClip     videoClip  // 0x50
  private           Slate.VideoSampler.SampleSettingssettings  // 0x58
PROPERTIES:
  isValid  get=0x0AC65FF4
  info  get=0x0AC65F74
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
  track  get=0x0AC66044
METHODS:
  RVA=0x0AC65E38  token=0x6000120  System.Void OnEnter()
  RVA=0x0AC65E38  token=0x6000121  System.Void OnReverseEnter()
  RVA=0x0AC65E40  token=0x6000122  System.Void OnReverse()
  RVA=0x0AC65E40  token=0x6000123  System.Void OnExit()
  RVA=0x0AC65DC0  token=0x6000124  System.Void Enable()
  RVA=0x0AC65E48  token=0x6000125  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x0AC65D94  token=0x6000126  System.Void Disable()
  RVA=0x0AC65F48  token=0x6000127  System.Void .ctor()
END_CLASS

CLASS: Slate.ActorGroup
TYPE:  class
TOKEN: 0x200003B
SIZE:  0xA8
EXTENDS: Slate.CutsceneGroup
FIELDS:
  private           System.String                   _name  // 0x70
  private           UnityEngine.GameObject          _actor  // 0x78
  private           Slate.CutsceneGroup.ActorReferenceMode_referenceMode  // 0x80
  private           Slate.CutsceneGroup.ActorInitialTransformation_initialCoordinates  // 0x84
  private           UnityEngine.Vector3             _initialLocalPosition  // 0x88
  private           UnityEngine.Vector3             _initialLocalRotation  // 0x94
  private           System.Boolean                  _displayVirtualMeshGizmo  // 0xa0
PROPERTIES:
  name  get=0x0AC50F24  set=0x063AD760
  actor  get=0x0AC50F18  set=0x0AC50FBC
  referenceMode  get=0x04D863A0  set=0x04D86420
  initialTransformation  get=0x04D8FCE0  set=0x04D8FCF0
  initialLocalPosition  get=0x04DA9AD0  set=0x04DA9B10
  initialLocalRotation  get=0x04DA9AF0  set=0x04DA9B30
  displayVirtualMeshGizmo  get=0x04D88020  set=0x04D88040
METHODS:
  RVA=0x0AC50EC8  token=0x6000136  System.Void .ctor()
END_CLASS

CLASS: Slate.DirectorGroup
TYPE:  class
TOKEN: 0x200003C
SIZE:  0x70
EXTENDS: Slate.CutsceneGroup
FIELDS:
PROPERTIES:
  name  get=0x0AC649D0  set=0x041E1670
  actor  get=0x0AC649AC  set=0x041E1670
  referenceMode  get=0x011EC580  set=0x041E1670
  initialTransformation  get=0x011EC580  set=0x041E1670
  initialLocalPosition  get=0x09D954F8  set=0x041E1670
  initialLocalRotation  get=0x09D954F8  set=0x041E1670
  displayVirtualMeshGizmo  get=0x012081B0  set=0x041E1670
METHODS:
  RVA=0x0AC64960  token=0x6000145  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionTrack
TYPE:  abstract class
TOKEN: 0x200003D
SIZE:  0x50
EXTENDS: Slate.CutsceneTrack
FIELDS:
METHODS:
  RVA=0x0AC50EA8  token=0x6000146  System.Void .ctor()
END_CLASS

CLASS: Slate.ActorActionTrack
TYPE:  class
TOKEN: 0x200003E
SIZE:  0x50
EXTENDS: Slate.ActionTrack
FIELDS:
METHODS:
  RVA=0x0AC50EA8  token=0x6000147  System.Void .ctor()
END_CLASS

CLASS: Slate.ActorAudioTrack
TYPE:  class
TOKEN: 0x200003F
SIZE:  0x98
EXTENDS: Slate.AudioTrack
FIELDS:
  protected         System.Boolean                  _useAudioSourceOnActor  // 0x90
PROPERTIES:
  useAudioSourceOnActor  get=0x02EC6590
METHODS:
  RVA=0x0AC50EB0  token=0x6000149  System.Void .ctor()
END_CLASS

CLASS: Slate.ActorPropertiesTrack
TYPE:  class
TOKEN: 0x2000040
SIZE:  0x58
EXTENDS: Slate.PropertiesTrack
FIELDS:
METHODS:
  RVA=0x0AC5102C  token=0x600014A  System.Void OnCreate()
  RVA=0x0AC51114  token=0x600014B  System.Void .ctor()
END_CLASS

CLASS: Slate.AnimationTrack
TYPE:  class
TOKEN: 0x2000041
SIZE:  0x78
EXTENDS: Slate.CutsceneTrack
FIELDS:
  private           System.Single                   _weight  // 0x50
  private           System.Single                   _blendIn  // 0x54
  private           System.Single                   _blendOut  // 0x58
  private           UnityEngine.AnimationBlendMode  _animationBlendMode  // 0x5c
  private           System.String                   _mixTransformName  // 0x60
  private           UnityEngine.Animation           anim  // 0x68
  private           UnityEngine.AnimationState      state  // 0x70
PROPERTIES:
  info  get=0x0AC580C0
  blendIn  get=0x04D86E00
  blendOut  get=0x04D88400
  weight  get=0x04D885A0
  animationBlendMode  get=0x04D86560  set=0x04D865C0
  mixTransformName  get=0x04D86280  set=0x063CC164
METHODS:
  RVA=0x0AC57E68  token=0x6000154  System.Boolean OnInitialize()
  RVA=0x0AC57C54  token=0x6000155  System.Void OnEnter()
  RVA=0x0AC57F84  token=0x6000156  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x0AC57E28  token=0x6000157  System.Void OnExit()
  RVA=0x0AC57F44  token=0x6000158  System.Void OnReverseEnter()
  RVA=0x0AC57E28  token=0x6000159  System.Void OnReverse()
  RVA=0x0AC57AD4  token=0x600015A  UnityEngine.Transform GetMixTransform()
  RVA=0x0AC5805C  token=0x600015B  System.Void .ctor()
  RVA=0x0AC58024  token=0x600015C  System.Boolean <GetMixTransform>b__27_0(UnityEngine.Transform t)
END_CLASS

CLASS: Slate.AnimatorTrack
TYPE:  class
TOKEN: 0x2000042
SIZE:  0xF8
EXTENDS: Slate.CutsceneTrack
FIELDS:
  private   static  System.Int32                    ROOTMOTION_FRAMERATE  // const
  public            UnityEngine.AvatarMask          mask  // 0x50
  public            UnityEngine.AnimationBlendMode  blendMode  // 0x58
  public            System.Single                   weight  // 0x5c
  public            System.Boolean                  useRootMotion  // 0x60
  public            System.Boolean                  applyFootIK  // 0x61
  public            System.Boolean                  isRootMotionPreBaked  // 0x62
  private           System.Collections.Generic.List<UnityEngine.Vector3>rmPositions  // 0x68
  private           System.Collections.Generic.List<UnityEngine.Quaternion>rmRotations  // 0x70
  private           System.Int32                    activeClips  // 0x78
  private           System.Single                   activeClipsWeight  // 0x7c
  private           System.Collections.Generic.Dictionary<Slate.ActionClips.PlayAnimatorClip,System.Int32>ports  // 0x80
  private           UnityEngine.Playables.PlayableGraphgraph  // 0x88
  private           UnityEngine.Animations.AnimationPlayableOutputanimationOutput  // 0x98
  private           UnityEngine.Animations.AnimationLayerMixerPlayablemasterMixer  // 0xa8
  private           UnityEngine.Animations.AnimationMixerPlayableclipsMixer  // 0xb8
  private           UnityEngine.Animations.AnimatorControllerPlayableanimatorPlayable  // 0xc8
  private           System.Collections.Generic.List<Slate.AnimatorTrack>siblingTracks  // 0xd8
  private           System.Boolean                  wasRootMotion  // 0xe0
  private           UnityEngine.AnimatorCullingMode wasCullingMode  // 0xe4
  private           System.Boolean                  useBakedRootMotion  // 0xe8
  private           UnityEngine.Animator            _animator  // 0xf0
PROPERTIES:
  animator  get=0x0AC59F28
  info  get=0x0AC5A034
  isLocked  get=0x0AC5A1AC
  masterTrack  get=0x0AC5A1E4
  isMasterTrack  get=0x0AC5A1DC
  isLastTrack  get=0x0AC5A164
  compoundTrackWeight  get=0x0AC5A01C
METHODS:
  RVA=0x0AC596E0  token=0x6000164  System.Boolean OnInitialize()
  RVA=0x0AC59548  token=0x6000165  System.Void OnEnter()
  RVA=0x0AC59A18  token=0x6000166  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x0AC59BD0  token=0x6000167  System.Void PostUpdateMasterTrack(System.Single time, System.Single previousTime)
  RVA=0x0AC598F0  token=0x6000168  System.Void OnReverseEnter()
  RVA=0x0AC596A0  token=0x6000169  System.Void OnExit()
  RVA=0x0AC59B18  token=0x600016A  System.Void PostExitMasterTrack()
  RVA=0x0AC5998C  token=0x600016B  System.Void OnReverse()
  RVA=0x0AC59140  token=0x600016C  System.Void EnableClip(Slate.ActionClips.PlayAnimatorClip playAnimClip, System.Single blendWeight, System.Single subClipWeight)
  RVA=0x0AC59E08  token=0x600016D  System.Void UpdateClip(Slate.ActionClips.PlayAnimatorClip playAnimClip, System.Single clipTime, System.Single clipPrevious, System.Single blendWeight, System.Single subClipWeight)
  RVA=0x0AC59098  token=0x600016E  System.Void DisableClip(Slate.ActionClips.PlayAnimatorClip playAnimClip, System.Single blendWeight, System.Single subClipWeight)
  RVA=0x0AC58A18  token=0x600016F  System.Void CreateAndPlayTree()
  RVA=0x0AC58DC4  token=0x6000170  UnityEngine.Playables.Playable CreateClipsMixer(UnityEngine.Playables.PlayableGraph graph)
  RVA=0x0AC59DB8  token=0x6000171  System.Void PreBakeRootMotion()
  RVA=0x0AC58954  token=0x6000172  System.Void ClearPreBakeRootMotion()
  RVA=0x0AC58490  token=0x6000173  System.Void BakeRootMotion()
  RVA=0x0AC591FC  token=0x6000174  System.Void EvaluateTrackClips(System.Single time, System.Single previousTime, System.Int32& tempActiveClips)
  RVA=0x0AC581C0  token=0x6000175  System.Void ApplyBakedRootMotion(System.Single time)
  RVA=0x0AC59F14  token=0x6000176  System.Void .ctor()
END_CLASS

CLASS: Slate.MecanimTrack
TYPE:  class
TOKEN: 0x2000044
SIZE:  0x58
EXTENDS: Slate.CutsceneTrack
FIELDS:
  private           UnityEngine.Animator            animator  // 0x50
METHODS:
  RVA=0x0AC65C28  token=0x600017A  System.Boolean OnInitialize()
  RVA=0x0AC65C20  token=0x600017B  System.Void OnReverse()
  RVA=0x0AC65C20  token=0x600017C  System.Void OnExit()
  RVA=0x0AC65B8C  token=0x600017D  System.Void DestroyDispatcher()
  RVA=0x0AC50EA8  token=0x600017E  System.Void .ctor()
END_CLASS

CLASS: Slate.AudioTrack
TYPE:  abstract class
TOKEN: 0x2000045
SIZE:  0x90
EXTENDS: Slate.CutsceneTrack
FIELDS:
  protected         UnityEngine.Audio.AudioMixerGroup_outputMixer  // 0x50
  protected         System.Single                   _masterVolume  // 0x58
  protected         System.Single                   _masterPitch  // 0x5c
  protected         System.Single                   _masterStereoPan  // 0x60
  protected         System.Single                   _masterSpatialBlend  // 0x64
  protected         System.Boolean                  _ignoreTimeScale  // 0x68
  protected         System.Boolean                  _bypassReverb  // 0x69
  private           UnityEngine.AudioSource         <source>k__BackingField  // 0x70
  private           Slate.AudioSampler.SampleSettings<sampleSettings>k__BackingField  // 0x78
PROPERTIES:
  info  get=0x0AC5A52C
  source  get=0x04D862B0  set=0x063AD760
  sampleSettings  get=0x04DA9B50  set=0x04DA9B70
  mixer  get=0x04D86270
  useAudioSourceOnActor  get=0x012081B0
METHODS:
  RVA=0x0AC5A36C  token=0x6000186  System.Void OnEnter()
  RVA=0x0AC5A36C  token=0x6000187  System.Void OnReverseEnter()
  RVA=0x0AC5A37C  token=0x6000188  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x0AC5A374  token=0x6000189  System.Void OnExit()
  RVA=0x0AC5A374  token=0x600018A  System.Void OnReverse()
  RVA=0x0AC5A290  token=0x600018B  System.Void Enable()
  RVA=0x0AC5A230  token=0x600018C  System.Void Disable()
  RVA=0x0AC5A480  token=0x600018D  System.Void SetAndApplySettings()
  RVA=0x0AC5A520  token=0x600018E  System.Void SetVolume(System.Single volume)
  RVA=0x0AC50EB0  token=0x600018F  System.Void .ctor()
END_CLASS

CLASS: Slate.CameraTrack
TYPE:  class
TOKEN: 0x2000046
SIZE:  0x98
EXTENDS: Slate.CutsceneTrack
FIELDS:
  private   static  Slate.CameraTrack               activeCameraTrack  // static @ 0x0
  private           System.Single                   _startTimeOffset  // 0x50
  private           System.Single                   _endTimeOffset  // 0x54
  public            System.Single                   _blendIn  // 0x58
  public            System.Single                   _blendOut  // 0x5c
  public            Slate.EaseType                  interpolation  // 0x60
  public            System.Single                   cineBoxFadeTime  // 0x64
  public            System.Single                   appliedSmoothing  // 0x68
  public            UnityEngine.Camera              exitCameraOverride  // 0x70
  private           Slate.GameCamera                entryCamera  // 0x78
  private           Slate.CameraShot                <firstShot>k__BackingField  // 0x80
  private           Slate.CameraShot                <lastShot>k__BackingField  // 0x88
  private           Slate.CameraShot                <currentShot>k__BackingField  // 0x90
PROPERTIES:
  firstShot  get=0x04D86260  set=0x04CD9970
  lastShot  get=0x04D861F0  set=0x05395D30
  currentShot  get=0x04D86220  set=0x06402330
  info  get=0x0AC5C604
  startTime  get=0x04D885A0  set=0x0AC5C6DC
  endTime  get=0x0AC5C5D8  set=0x0AC5C668
  blendIn  get=0x0AC5C484  set=0x04D88430
  blendOut  get=0x0AC5C52C  set=0x04D91510
METHODS:
  RVA=0x0AC5BCC4  token=0x600019F  System.Void OnEnter()
  RVA=0x0AC5C0B4  token=0x60001A0  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x0AC5BEE0  token=0x60001A1  System.Void OnExit()
  RVA=0x0AC5BF7C  token=0x60001A2  System.Void OnReverseEnter()
  RVA=0x0AC5C018  token=0x60001A3  System.Void OnReverse()
  RVA=0x0AC5C46C  token=0x60001A4  System.Void .ctor()
  RVA=0x0AC5C3EC  token=0x60001A5  System.Boolean <OnEnter>b__36_0(Slate.ActionClip s)
  RVA=0x0AC5C424  token=0x60001A6  System.Boolean <OnEnter>b__36_1(Slate.ActionClip s)
END_CLASS

CLASS: Slate.DirectorActionTrack
TYPE:  class
TOKEN: 0x2000047
SIZE:  0x50
EXTENDS: Slate.ActionTrack
FIELDS:
METHODS:
  RVA=0x0AC50EA8  token=0x60001A7  System.Void .ctor()
END_CLASS

CLASS: Slate.DirectorAudioTrack
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x90
EXTENDS: Slate.AudioTrack
FIELDS:
METHODS:
  RVA=0x0AC50EB0  token=0x60001A8  System.Void .ctor()
END_CLASS

CLASS: Slate.DirectorPropertiesTrack
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x58
EXTENDS: Slate.PropertiesTrack
FIELDS:
METHODS:
  RVA=0x0AC51114  token=0x60001A9  System.Void .ctor()
END_CLASS

CLASS: Slate.VideoTrack
TYPE:  class
TOKEN: 0x200004A
SIZE:  0x60
EXTENDS: Slate.CutsceneTrack
FIELDS:
  public            Slate.VideoSampler.VideoRenderTargetrenderTarget  // 0x50
  public            UnityEngine.Video.VideoAspectRatioaspectRatio  // 0x54
  private           UnityEngine.Video.VideoPlayer   <source>k__BackingField  // 0x58
PROPERTIES:
  source  get=0x02E56440  set=0x036E5590
  info  get=0x0AC675C0
METHODS:
  RVA=0x0AC675A0  token=0x60001AD  System.Void OnEnter()
  RVA=0x0AC675A0  token=0x60001AE  System.Void OnReverseEnter()
  RVA=0x0AC675A8  token=0x60001AF  System.Void OnReverse()
  RVA=0x0AC675A8  token=0x60001B0  System.Void OnExit()
  RVA=0x0AC6753C  token=0x60001B1  System.Void Enable()
  RVA=0x0AC674F0  token=0x60001B2  System.Void Disable()
  RVA=0x0AC675B0  token=0x60001B3  System.Void .ctor()
END_CLASS

CLASS: Slate.PropertiesTrack
TYPE:  abstract class
TOKEN: 0x200004B
SIZE:  0x58
EXTENDS: Slate.CutsceneTrack
IMPLEMENTS: Slate.IKeyable Slate.IDirectable
FIELDS:
  private           Slate.AnimationDataCollection   _animationData  // 0x50
PROPERTIES:
  animationData  get=0x04D86270
  animatedParametersTarget  get=0x0AC661CC
METHODS:
  RVA=0x0AC66080  token=0x60001B6  System.Void OnAfterValidate()
  RVA=0x0AC660A8  token=0x60001B7  System.Void OnEnter()
  RVA=0x0AC6611C  token=0x60001B8  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x0AC660FC  token=0x60001B9  System.Void OnReverse()
  RVA=0x0AC66174  token=0x60001BA  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClip
TYPE:  abstract class
TOKEN: 0x200004C
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Slate.IDirectable Slate.IKeyable
FIELDS:
  private           System.Single                   _startTime  // 0x18
  private           Slate.AnimationDataCollection   _animationData  // 0x20
  private           Slate.IDirectable               <parent>k__BackingField  // 0x28
  private           System.Boolean                  <isHighlight>k__BackingField  // 0x30
  private           System.String[]                 _cachedAnimParamPaths  // 0x38
PROPERTIES:
  root  get=0x0AC50B90
  parent  get=0x04D86240  set=0x02FDB880
  actor  get=0x0AC5087C
  Slate.IDirectable.children  get=0x011EC580
  animationData  get=0x04D862C0  set=0x05390F40
  startTime  get=0x04D86380  set=0x0AC50DAC
  endTime  get=0x0AC508D4  set=0x0AC50C90
  isActive  get=0x0AC50A84
  isCollapsed  get=0x0AC50AC0
  isLocked  get=0x0AC50AE0
  length  get=0x04D879D0  set=0x041E1670
  blendIn  get=0x04D879D0  set=0x041E1670
  blendOut  get=0x04D879D0  set=0x041E1670
  canCrossBlend  get=0x012081B0
  info  get=0x0AC509E0
  isValid  get=0x0AC50B20
  color  get=0x0A96C614
  textColor  get=0x0AC50BB0
  textSizeInner  get=0x04DA5360
  textSizeOuter  get=0x04DA4FC0
  interval  get=0x04D879D0
  url  get=0x0AC50BD0
  isHighlight  get=0x04D865F0  set=0x04D86620
  isReadOnly  get=0x0AC50B00
  defaultTransformSpace  get=0x011EC580
  animatedParameterPaths  get=0x0AC5089C
  handleParametersRegistrationManually  get=0x0AC50900
  animatedParametersTarget  get=0x0464E630
  animatedParametersInterpolation  get=0x011EC580
  useWeightInParameters  get=0x012081B0
  hasParameters  get=0x0AC509C8
  hasActiveParameters  get=0x0AC50924
  needActor  get=0x0AC50B70
  canSample  get=0x02FFF600
  updateOrder  get=0x04DA3400
METHODS:
  RVA=0x0AC501F4  token=0x60001E6  System.Boolean Slate.IDirectable.Initialize()
  RVA=0x0AC501C0  token=0x60001E7  System.Void Slate.IDirectable.Enter()
  RVA=0x0AC50284  token=0x60001E8  System.Void Slate.IDirectable.Update(System.Single time, System.Single previousTime)
  RVA=0x0AC501E4  token=0x60001E9  System.Void Slate.IDirectable.Exit()
  RVA=0x0AC50204  token=0x60001EA  System.Void Slate.IDirectable.ReverseEnter()
  RVA=0x0AC50214  token=0x60001EB  System.Void Slate.IDirectable.Reverse()
  RVA=0x0AC50250  token=0x60001EC  System.Void Slate.IDirectable.RootEnabled()
  RVA=0x0AC50238  token=0x60001ED  System.Void Slate.IDirectable.RootDisabled()
  RVA=0x0AC50268  token=0x60001EE  System.Void Slate.IDirectable.RootUpdated(System.Single time, System.Single previousTime)
  RVA=0x04D83230  token=0x60001EF  System.Void Slate.IDirectable.RootDestroyed()
  RVA=0x0AC50070  token=0x60001F0  System.Void PostCreate(Slate.IDirectable parent)
  RVA=0x0AC50828  token=0x60001F1  System.Void Validate()
  RVA=0x0AC50838  token=0x60001F2  System.Void Validate(Slate.IDirector root, Slate.IDirectable parent)
  RVA=0x02FFF600  token=0x60001F3  System.Boolean OnInitialize()
  RVA=0x041E1670  token=0x60001F4  System.Void OnEnter()
  RVA=0x0AC50060  token=0x60001F5  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x041E1670  token=0x60001F6  System.Void OnUpdate(System.Single time)
  RVA=0x041E1670  token=0x60001F7  System.Void OnExit()
  RVA=0x041E1670  token=0x60001F8  System.Void OnReverse()
  RVA=0x041E1670  token=0x60001F9  System.Void OnReverseEnter()
  RVA=0x041E1670  token=0x60001FA  System.Void OnDrawGizmosSelected()
  RVA=0x041E1670  token=0x60001FB  System.Void OnSceneGUI()
  RVA=0x041E1670  token=0x60001FC  System.Void OnCreate()
  RVA=0x041E1670  token=0x60001FD  System.Void OnAfterValidate()
  RVA=0x041E1670  token=0x60001FE  System.Void OnRootEnabled()
  RVA=0x041E1670  token=0x60001FF  System.Void OnRootDisabled()
  RVA=0x041E1670  token=0x6000200  System.Void OnRootUpdated(System.Single time, System.Single previousTime)
  RVA=0x041E1670  token=0x6000201  System.Void OnRootDestroyed()
  RVA=0x0AC500F0  token=0x6000202  System.Boolean RootTimeWithinRange()
  RVA=0x0AC502C8  token=0x6000203  UnityEngine.Vector3 TransformPosition(UnityEngine.Vector3 point, Slate.TransformSpace space)
  RVA=0x0AC4FFD0  token=0x6000204  UnityEngine.Vector3 InverseTransformPosition(UnityEngine.Vector3 point, Slate.TransformSpace space)
  RVA=0x0AC50314  token=0x6000205  UnityEngine.Quaternion TransformRotation(UnityEngine.Vector3 euler, Slate.TransformSpace space)
  RVA=0x0AC5001C  token=0x6000206  UnityEngine.Vector3 InverseTransformRotation(UnityEngine.Quaternion rot, Slate.TransformSpace space)
  RVA=0x0AC4FD60  token=0x6000207  UnityEngine.Vector3 ActorPositionInSpace(Slate.TransformSpace space)
  RVA=0x0AC4FFC8  token=0x6000208  UnityEngine.Transform GetSpaceTransform(Slate.TransformSpace space, UnityEngine.GameObject actorOverride)
  RVA=0x0AC4FF8C  token=0x6000209  Slate.ActionClip GetPreviousClip()
  RVA=0x0AC4FF30  token=0x600020A  Slate.ActionClip GetNextClip()
  RVA=0x0AC4FE6C  token=0x600020B  System.Single GetClipWeight()
  RVA=0x0AC4FED4  token=0x600020C  System.Single GetClipWeight(System.Single time)
  RVA=0x0AC4FEA8  token=0x600020D  System.Single GetClipWeight(System.Single time, System.Single blendInOut)
  RVA=0x0AC4FEC4  token=0x600020E  System.Single GetClipWeight(System.Single time, System.Single blendIn, System.Single blendOut)
  RVA=0x0AC50494  token=0x600020F  System.Void TryMatchSubClipLength()
  RVA=0x0AC5042C  token=0x6000210  System.Void TryMatchPreviousSubClipLoop()
  RVA=0x0AC50358  token=0x6000211  System.Void TryMatchNexSubClipLoop()
  RVA=-1  // generic def  token=0x6000212  System.String GetParameterName(System.Linq.Expressions.Expression<System.Func<T,TResult>> func)
  RVA=-1  // generic def  token=0x6000213  Slate.AnimatedParameter GetParameter(System.Linq.Expressions.Expression<System.Func<T,TResult>> func)
  RVA=0x0AC4FF6C  token=0x6000214  Slate.AnimatedParameter GetParameter(System.String paramName)
  RVA=-1  // generic def  token=0x6000215  System.Void SetParameterEnabled(System.Linq.Expressions.Expression<System.Func<T,TResult>> func, System.Boolean enabled)
  RVA=0x0AC50158  token=0x6000216  System.Void SetParameterEnabled(System.String paramName, System.Boolean enabled)
  RVA=0x0AC500A4  token=0x6000217  System.Void ResetAnimatedParameters()
  RVA=0x0AC4FD90  token=0x6000218  System.Void CreateAnimationDataCollection()
  RVA=0x0AC5062C  token=0x6000219  System.Void ValidateAnimParams()
  RVA=0x0AC500F8  token=0x600021A  System.Void SetAnimParamsSnapshot()
  RVA=0x0AC50550  token=0x600021B  System.Void UpdateAnimParams(System.Single time, System.Single previousTime)
  RVA=0x0AC500B8  token=0x600021C  System.Void RestoreAnimParamsSnapshot()
  RVA=0x05393520  token=0x600021D  System.Void .ctor()
  RVA=0x05396F58  token=0x600021E  System.String Slate.IDirectable.get_name()
END_CLASS

CLASS: Slate.AnimatedParameter
TYPE:  class
TOKEN: 0x200004D
SIZE:  0x70
IMPLEMENTS: Slate.IAnimatableData UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private   static  System.Action<Slate.IAnimatableData>onParameterChanged  // static @ 0x0
  private           System.String                   _serializedData  // 0x10
  private           System.Boolean                  _isDisabled  // 0x18
  private           UnityEngine.AnimationCurve[]    _curves  // 0x20
  private           System.String                   _scriptExpression  // 0x28
  private           Slate.AnimatedParameter.SerializationMetaData_data  // 0x30
  private           Slate.IAnimatedParameterModel   _parameterModel  // 0x38
  private           Slate.IKeyable                  <keyable>k__BackingField  // 0x40
  private           System.Single[]                 <snapshot>k__BackingField  // 0x48
  private           System.Single[]                 <lastEval>k__BackingField  // 0x50
  private           UnityEngine.Transform           <virtualTransformParent>k__BackingField  // 0x58
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Type>parameterModelsMap  // static @ 0x8
  public    static readonly System.Type[]                   supportedTypes  // static @ 0x10
  private           System.Object                   _animatableAttribute  // 0x60
  private           System.Object                   _resolvedMemberObject  // 0x68
PROPERTIES:
  enabled  get=0x0AA1EEF0
  serializedData  get=0x02B2ECC0  set=0x053908C0
  data  get=0x0AC535E4
  parameterModel  get=0x0AC53748
  curves  get=0x04D862C0  set=0x05390F40
  scriptExpression  get=0x04D86240  set=0x0AC53998
  hasActiveExpression  get=0x0AC53690
  parameterName  get=0x0AC53834
  animatedType  get=0x0AC535C4
  parameterType  get=0x0AC53854
  transformHierarchyPath  get=0x0AC538B4
  declaringType  get=0x0AC53650
  property  get=0x0AC53874
  field  get=0x0AC53670
  isProperty  get=0x0AC536C0
  keyable  get=0x04D85A60  set=0x042B4AE0
  snapshot  get=0x04D86200  set=0x035A41A0
  lastEval  get=0x04D86270  set=0x02FDAEA0
  virtualTransformParent  get=0x02E56440  set=0x036E5590
  targetObject  get=0x0AC53894
  animatableAttribute  get=0x0AC534EC
  isExternal  get=0x0AC536A8
  isValid  get=0x0AC536D8
EVENTS:
  onParameterChanged  add=add_onParameterChanged  remove=remove_onParameterChanged
METHODS:
  RVA=0x0AC53038  token=0x600021F  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0AC52FFC  token=0x6000220  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x0AC52DD8  token=0x6000224  System.Void SetEnabled(System.Boolean value, System.Single time)
  RVA=0x0AC53068  token=0x6000242  System.Void .cctor()
  RVA=0x0AC532D8  token=0x6000243  System.Void .ctor(Slate.IKeyable keyable, System.Type type, System.String memberPath, System.String transformPath)
  RVA=0x0AC51488  token=0x6000244  System.Void ConstructWithField(System.Reflection.FieldInfo targetField, System.Type type, System.String memberPath, System.String transformPath)
  RVA=0x0AC51658  token=0x6000245  System.Void ConstructWithProperty(System.Reflection.PropertyInfo targetProperty, System.Type type, System.String memberPath, System.String transformPath)
  RVA=0x0AC522E0  token=0x6000246  System.Reflection.MemberInfo GetMemberInfo()
  RVA=0x04D862C0  token=0x6000247  UnityEngine.AnimationCurve[] GetCurves()
  RVA=0x0AC513B4  token=0x6000248  System.Boolean CompareTo(Slate.AnimatedParameter other)
  RVA=0x0AC523B8  token=0x6000249  System.Void InitializeCurves()
  RVA=0x042B4AE0  token=0x600024A  System.Void Validate(Slate.IKeyable keyable)
  RVA=0x036E5590  token=0x600024B  System.Void SetVirtualTransformParent(UnityEngine.Transform virtualTransformParent)
  RVA=0x0AC52F24  token=0x600024C  System.Void SetSnapshot()
  RVA=0x0AC52F9C  token=0x600024D  System.Boolean TryAutoKey(System.Single time)
  RVA=0x0AC51988  token=0x600024E  System.Void Evaluate(System.Single time, System.Single previousTime, System.Single weight)
  RVA=0x0AC5189C  token=0x600024F  System.Void Evaluate_1_Curves(System.Single time, System.Single previousTime, System.Single weight)
  RVA=0x012081B0  token=0x6000250  System.Boolean Evaluate_2_Expression(System.Single time, System.Single previousTime, System.Single weight)
  RVA=0x0AC52910  token=0x6000251  System.Void RestoreSnapshot()
  RVA=0x0AC520D8  token=0x6000252  System.Object GetEvalValue(System.Single time)
  RVA=0x0AC526DC  token=0x6000253  System.Object ResolvedMemberObject()
  RVA=0x0AC52094  token=0x6000254  System.Object GetCurrentValueAsObject()
  RVA=0x0AC51C94  token=0x6000255  System.Single[] GetCurrentValueAsFloats()
  RVA=0x0AC52970  token=0x6000256  System.Void SetCurrentValue(System.Object value)
  RVA=0x0AC529B8  token=0x6000257  System.Void SetCurrentValue(System.Single[] floats)
  RVA=0x0AC52330  token=0x6000258  System.Boolean HasChanged()
  RVA=0x0AC52324  token=0x6000259  System.Boolean HasAnyKey()
  RVA=0x0AC523A8  token=0x600025A  System.Boolean HasKey(System.Single time)
  RVA=0x0AC522C0  token=0x600025B  System.Single GetKeyNext(System.Single time)
  RVA=0x0AC522D0  token=0x600025C  System.Single GetKeyPrevious(System.Single time)
  RVA=0x012081B0  token=0x600025D  System.Boolean TryKeyIdentity(System.Single time)
  RVA=0x041E1670  token=0x600025E  System.Void SetKeyCurrent(System.Single time)
  RVA=0x041E1670  token=0x600025F  System.Void RemoveKey(System.Single time)
  RVA=0x0AC52EB4  token=0x6000260  System.Void SetPreWrapMode(UnityEngine.WrapMode mode)
  RVA=0x0AC52E44  token=0x6000261  System.Void SetPostWrapMode(UnityEngine.WrapMode mode)
  RVA=0x0AC52584  token=0x6000262  System.Void OffsetValue(System.Object deltaValue)
  RVA=0x0AC52614  token=0x6000263  System.Void Reset()
  RVA=0x0AC51354  token=0x6000264  System.Void ChangeMemberType(Slate.AnimatedParameter.ParameterType newType)
  RVA=0x0AC52F94  token=0x6000265  System.String ToString()
  RVA=0x0AC51A14  token=0x6000266  System.String FriendlyName()
  RVA=0x0AC521AC  token=0x6000267  System.String GetKeyLabel(System.Single time)
  RVA=0x041E1670  token=0x6000268  System.Void RecordUndo()
  RVA=0x0AC5250C  token=0x6000269  System.Void NotifyChange()
END_CLASS

CLASS: Slate.IAnimatedParameterModel
TYPE:  interface
TOKEN: 0x2000051
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000276  System.Boolean ForceStepMode()
  RVA=-1  // abstract  token=0x6000277  System.Int32 RequiredCurvesCount()
  RVA=-1  // abstract  token=0x6000278  System.Object ConvertToObject(System.Single[] floats)
  RVA=-1  // abstract  token=0x6000279  System.Single[] ConvertToFloats(System.Object value)
  RVA=-1  // abstract  token=0x600027A  System.String GetKeyLabel(System.Single[] floats)
  RVA=-1  // abstract  token=0x600027B  System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats)
  RVA=-1  // abstract  token=0x600027C  System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info)
END_CLASS

CLASS: Slate.Animated_Bool
TYPE:  sealed struct
TOKEN: 0x2000052
SIZE:  0x18
IMPLEMENTS: Slate.IAnimatedParameterModel
FIELDS:
  private           System.Action<System.Object,System.Boolean>setter  // 0x10
METHODS:
  RVA=0x02FFF600  token=0x600027D  System.Boolean ForceStepMode()
  RVA=0x04D85B80  token=0x600027E  System.Int32 RequiredCurvesCount()
  RVA=0x0AC53A6C  token=0x600027F  System.Object ConvertToObject(System.Single[] floats)
  RVA=0x0AC539D0  token=0x6000280  System.Single[] ConvertToFloats(System.Object value)
  RVA=0x0AC53BA4  token=0x6000281  System.String GetKeyLabel(System.Single[] floats)
  RVA=0x0AC53C28  token=0x6000282  System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats)
  RVA=0x0AC53AD0  token=0x6000283  System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info)
END_CLASS

CLASS: Slate.Animated_Int
TYPE:  sealed struct
TOKEN: 0x2000053
SIZE:  0x18
IMPLEMENTS: Slate.IAnimatedParameterModel
FIELDS:
  private           System.Action<System.Object,System.Int32>setter  // 0x10
METHODS:
  RVA=0x02FFF600  token=0x6000284  System.Boolean ForceStepMode()
  RVA=0x04D85B80  token=0x6000285  System.Int32 RequiredCurvesCount()
  RVA=0x0AC54BE0  token=0x6000286  System.Object ConvertToObject(System.Single[] floats)
  RVA=0x0AC54B54  token=0x6000287  System.Single[] ConvertToFloats(System.Object value)
  RVA=0x0AC54CFC  token=0x6000288  System.String GetKeyLabel(System.Single[] floats)
  RVA=0x0AC54D78  token=0x6000289  System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats)
  RVA=0x0AC54C3C  token=0x600028A  System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info)
END_CLASS

CLASS: Slate.Animated_Float
TYPE:  sealed struct
TOKEN: 0x2000054
SIZE:  0x18
IMPLEMENTS: Slate.IAnimatedParameterModel
FIELDS:
  private           System.Action<System.Object,System.Single>setter  // 0x10
METHODS:
  RVA=0x012081B0  token=0x600028B  System.Boolean ForceStepMode()
  RVA=0x04D85B80  token=0x600028C  System.Int32 RequiredCurvesCount()
  RVA=0x0AC54900  token=0x600028D  System.Object ConvertToObject(System.Single[] floats)
  RVA=0x0AC54878  token=0x600028E  System.Single[] ConvertToFloats(System.Object value)
  RVA=0x0AC54A20  token=0x600028F  System.String GetKeyLabel(System.Single[] floats)
  RVA=0x0AC54A9C  token=0x6000290  System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats)
  RVA=0x0AC54960  token=0x6000291  System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info)
END_CLASS

CLASS: Slate.Animated_Vector2
TYPE:  sealed struct
TOKEN: 0x2000055
SIZE:  0x18
IMPLEMENTS: Slate.IAnimatedParameterModel
FIELDS:
  private           System.Action<System.Object,UnityEngine.Vector2>setter  // 0x10
METHODS:
  RVA=0x012081B0  token=0x6000292  System.Boolean ForceStepMode()
  RVA=0x02BBE540  token=0x6000293  System.Int32 RequiredCurvesCount()
  RVA=0x0AC55874  token=0x6000294  System.Object ConvertToObject(System.Single[] floats)
  RVA=0x0AC557D8  token=0x6000295  System.Single[] ConvertToFloats(System.Object value)
  RVA=0x0AC559B4  token=0x6000296  System.String GetKeyLabel(System.Single[] floats)
  RVA=0x0AC55A64  token=0x6000297  System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats)
  RVA=0x0AC558E0  token=0x6000298  System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info)
END_CLASS

CLASS: Slate.Animated_Vector3
TYPE:  sealed struct
TOKEN: 0x2000056
SIZE:  0x18
IMPLEMENTS: Slate.IAnimatedParameterModel
FIELDS:
  private           System.Action<System.Object,UnityEngine.Vector3>setter  // 0x10
METHODS:
  RVA=0x012081B0  token=0x6000299  System.Boolean ForceStepMode()
  RVA=0x04D8C9E0  token=0x600029A  System.Int32 RequiredCurvesCount()
  RVA=0x0AC55BD0  token=0x600029B  System.Object ConvertToObject(System.Single[] floats)
  RVA=0x0AC55B30  token=0x600029C  System.Single[] ConvertToFloats(System.Object value)
  RVA=0x0AC55D28  token=0x600029D  System.String GetKeyLabel(System.Single[] floats)
  RVA=0x0AC55E0C  token=0x600029E  System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats)
  RVA=0x0AC55C50  token=0x600029F  System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info)
END_CLASS

CLASS: Slate.Animated_Color
TYPE:  sealed struct
TOKEN: 0x2000057
SIZE:  0x18
IMPLEMENTS: Slate.IAnimatedParameterModel
FIELDS:
  private           System.Action<System.Object,UnityEngine.Color>setter  // 0x10
METHODS:
  RVA=0x012081B0  token=0x60002A0  System.Boolean ForceStepMode()
  RVA=0x04D882B0  token=0x60002A1  System.Int32 RequiredCurvesCount()
  RVA=0x0AC544A4  token=0x60002A2  System.Object ConvertToObject(System.Single[] floats)
  RVA=0x0AC543F0  token=0x60002A3  System.Single[] ConvertToFloats(System.Object value)
  RVA=0x0AC5462C  token=0x60002A4  System.String GetKeyLabel(System.Single[] floats)
  RVA=0x0AC54778  token=0x60002A5  System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats)
  RVA=0x0AC54540  token=0x60002A6  System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info)
END_CLASS

CLASS: Slate.Animated_Vector4
TYPE:  sealed struct
TOKEN: 0x2000058
SIZE:  0x18
IMPLEMENTS: Slate.IAnimatedParameterModel
FIELDS:
  private           System.Action<System.Object,UnityEngine.Vector4>setter  // 0x10
METHODS:
  RVA=0x012081B0  token=0x60002A7  System.Boolean ForceStepMode()
  RVA=0x04D882B0  token=0x60002A8  System.Int32 RequiredCurvesCount()
  RVA=0x0AC55FA0  token=0x60002A9  System.Object ConvertToObject(System.Single[] floats)
  RVA=0x0AC55EEC  token=0x60002AA  System.Single[] ConvertToFloats(System.Object value)
  RVA=0x0AC56128  token=0x60002AB  System.String GetKeyLabel(System.Single[] floats)
  RVA=0x0AC562C0  token=0x60002AC  System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats)
  RVA=0x0AC5603C  token=0x60002AD  System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info)
END_CLASS

CLASS: Slate.Animated_Quaternion
TYPE:  sealed struct
TOKEN: 0x2000059
SIZE:  0x18
IMPLEMENTS: Slate.IAnimatedParameterModel
FIELDS:
  private           System.Action<System.Object,UnityEngine.Quaternion>setter  // 0x10
METHODS:
  RVA=0x012081B0  token=0x60002AE  System.Boolean ForceStepMode()
  RVA=0x04D882B0  token=0x60002AF  System.Int32 RequiredCurvesCount()
  RVA=0x0AC54EE4  token=0x60002B0  System.Object ConvertToObject(System.Single[] floats)
  RVA=0x0AC54E30  token=0x60002B1  System.Single[] ConvertToFloats(System.Object value)
  RVA=0x0AC5506C  token=0x60002B2  System.String GetKeyLabel(System.Single[] floats)
  RVA=0x0AC55204  token=0x60002B3  System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats)
  RVA=0x0AC54F80  token=0x60002B4  System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info)
END_CLASS

CLASS: Slate.Animated_Rect
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x18
IMPLEMENTS: Slate.IAnimatedParameterModel
FIELDS:
  private           System.Action<System.Object,UnityEngine.Rect>setter  // 0x10
METHODS:
  RVA=0x012081B0  token=0x60002B5  System.Boolean ForceStepMode()
  RVA=0x04D882B0  token=0x60002B6  System.Int32 RequiredCurvesCount()
  RVA=0x0AC553B8  token=0x60002B7  System.Object ConvertToObject(System.Single[] floats)
  RVA=0x0AC55304  token=0x60002B8  System.Single[] ConvertToFloats(System.Object value)
  RVA=0x0AC55540  token=0x60002B9  System.String GetKeyLabel(System.Single[] floats)
  RVA=0x0AC556D8  token=0x60002BA  System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats)
  RVA=0x0AC55454  token=0x60002BB  System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info)
END_CLASS

CLASS: Slate.Animated_Bounds
TYPE:  sealed struct
TOKEN: 0x200005B
SIZE:  0x18
IMPLEMENTS: Slate.IAnimatedParameterModel
FIELDS:
  private           System.Action<System.Object,UnityEngine.Bounds>setter  // 0x10
METHODS:
  RVA=0x012081B0  token=0x60002BC  System.Boolean ForceStepMode()
  RVA=0x04D9EC60  token=0x60002BD  System.Int32 RequiredCurvesCount()
  RVA=0x0AC53E10  token=0x60002BE  System.Object ConvertToObject(System.Single[] floats)
  RVA=0x0AC53CEC  token=0x60002BF  System.Single[] ConvertToFloats(System.Object value)
  RVA=0x0AC54070  token=0x60002C0  System.String GetKeyLabel(System.Single[] floats)
  RVA=0x0AC54284  token=0x60002C1  System.Void SetDirect(System.Object target, System.Reflection.MemberInfo info, System.Single[] floats)
  RVA=0x0AC53F10  token=0x60002C2  System.Single[] GetDirect(System.Object target, System.Reflection.MemberInfo info)
END_CLASS

CLASS: Slate.AnimationDataCollection
TYPE:  class
TOKEN: 0x200005C
SIZE:  0x18
IMPLEMENTS: Slate.IAnimatableData
FIELDS:
  private           System.Collections.Generic.List<Slate.AnimatedParameter>_animatedParameters  // 0x10
PROPERTIES:
  animatedParameters  get=0x02B2ECC0
  isValid  get=0x0AC57A90
  Item  get=0x0AC57A20
  Item  get=0x0AC57A20
METHODS:
  RVA=0x041E1670  token=0x60002C7  System.Void .ctor()
  RVA=0x0AC5799C  token=0x60002C8  System.Void .ctor(Slate.IKeyable keyable, System.Type type, System.String[] memberPaths, System.String transformPath)
  RVA=0x0AC57594  token=0x60002C9  System.Boolean TryAddParameter(Slate.IKeyable keyable, System.Type type, System.String memberPath, System.String transformPath)
  RVA=0x0AC570E4  token=0x60002CA  System.Void RemoveParameter(Slate.AnimatedParameter animParam)
  RVA=0x0AC56E78  token=0x60002CB  System.Void ReOrderParameters()
  RVA=0x0AC56A98  token=0x60002CC  Slate.AnimatedParameter GetParameterOfName(System.String name)
  RVA=0x0AC56484  token=0x60002CD  UnityEngine.AnimationCurve[] GetCurves()
  RVA=0x0AC56478  token=0x60002CE  UnityEngine.AnimationCurve[] GetCurvesAll()
  RVA=0x0AC56D24  token=0x60002CF  UnityEngine.AnimationCurve[] Internal_GetCurves(System.Boolean enabledParamsOnly)
  RVA=0x0AC57908  token=0x60002D0  System.Void Validate(Slate.IKeyable keyable)
  RVA=0x0AC5745C  token=0x60002D1  System.Void SetVirtualTransformParent(UnityEngine.Transform virtualTransformParent)
  RVA=0x0AC573D8  token=0x60002D2  System.Void SetSnapshot()
  RVA=0x0AC577D0  token=0x60002D3  System.Boolean TryAutoKey(System.Single time)
  RVA=0x0AC563C0  token=0x60002D4  System.Void Evaluate(System.Single time, System.Single previousTime, System.Single weight)
  RVA=0x0AC571BC  token=0x60002D5  System.Void RestoreSnapshot()
  RVA=0x0AC5788C  token=0x60002D6  System.Boolean TryKeyIdentity(System.Single time)
  RVA=0x0AC5706C  token=0x60002D7  System.Void RemoveKey(System.Single time)
  RVA=0x0AC56BFC  token=0x60002D8  System.Boolean HasChanged()
  RVA=0x0AC56C88  token=0x60002D9  System.Boolean HasKey(System.Single time)
  RVA=0x0AC56B70  token=0x60002DA  System.Boolean HasAnyKey()
  RVA=0x0AC57240  token=0x60002DB  System.Void SetKeyCurrent(System.Single time)
  RVA=0x0AC56640  token=0x60002DC  System.Single GetKeyNext(System.Single time)
  RVA=0x0AC5686C  token=0x60002DD  System.Single GetKeyPrevious(System.Single time)
  RVA=0x0AC56490  token=0x60002DE  System.String GetKeyLabel(System.Single time)
  RVA=0x0AC57348  token=0x60002DF  System.Void SetPreWrapMode(UnityEngine.WrapMode mode)
  RVA=0x0AC572B8  token=0x60002E0  System.Void SetPostWrapMode(UnityEngine.WrapMode mode)
  RVA=0x0AC57138  token=0x60002E1  System.Void Reset()
  RVA=0x0AC574F0  token=0x60002E2  System.String ToString()
END_CLASS

CLASS: Slate.Cutscene
TYPE:  class
TOKEN: 0x2000064
SIZE:  0xA8
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Slate.IDirector
FIELDS:
  public    static  System.Single                   VERSION_NUMBER  // const
  private   static  System.Action<Slate.Cutscene>   OnCutsceneStarted  // static @ 0x0
  private   static  System.Action<Slate.Cutscene>   OnCutsceneStopped  // static @ 0x8
  private           System.Action<Slate.Section>    OnSectionReached  // 0x18
  private           System.Action<System.String,System.Object>OnGlobalMessageSend  // 0x20
  private           System.Action                   OnStop  // 0x28
  private           Slate.Cutscene.UpdateMode       _updateMode  // 0x30
  private           Slate.Cutscene.WrapMode         _defaultWrapMode  // 0x34
  private           Slate.Cutscene.StopMode         _defaultStopMode  // 0x38
  private           System.Single                   _playbackSpeed  // 0x3c
  private           System.Boolean                  _playOnStart  // 0x40
  private           System.Boolean                  _explicitActiveLayers  // 0x41
  private           UnityEngine.LayerMask           _activeLayers  // 0x44
  public            System.Collections.Generic.List<Slate.CutsceneGroup>groups  // 0x48
  private           System.Single                   _length  // 0x50
  private           System.Single                   _viewTimeMin  // 0x54
  private           System.Single                   _viewTimeMax  // 0x58
  private           System.Single                   _currentTime  // 0x5c
  private           System.Single                   _playTimeMin  // 0x60
  private           System.Single                   _playTimeMax  // 0x64
  private           UnityEngine.Transform           _groupsRoot  // 0x68
  private           System.Collections.Generic.List<Slate.IDirectableTimePointer>timePointers  // 0x70
  private           System.Collections.Generic.List<Slate.IDirectableTimePointer>unsortedStartTimePointers  // 0x78
  private           System.Collections.Generic.Dictionary<UnityEngine.GameObject,System.Boolean>affectedLayerGOStates  // 0x80
  private   static  System.Collections.Generic.Dictionary<System.String,Slate.Cutscene>allSceneCutscenes  // static @ 0x10
  private           System.Boolean                  preInitialized  // 0x88
  private           System.Boolean                  _isReSampleFrame  // 0x89
  private           System.Boolean                  <isBakingRootMotion>k__BackingField  // 0x8a
  private           Slate.Cutscene.PlayingDirection <playingDirection>k__BackingField  // 0x8c
  private           Slate.Cutscene.WrapMode         <playingWrapMode>k__BackingField  // 0x90
  private           System.Collections.Generic.List<Slate.IDirectable><directables>k__BackingField  // 0x98
  private           System.Boolean                  <isActive>k__BackingField  // 0xa0
  private           System.Boolean                  <isPaused>k__BackingField  // 0xa1
  private           System.Single                   <previousTime>k__BackingField  // 0xa4
PROPERTIES:
  groupsRoot  get=0x0AC61294
  updateMode  get=0x04D864F0  set=0x04D86740
  defaultWrapMode  get=0x04D86730  set=0x04D86760
  defaultStopMode  get=0x04D863B0  set=0x04D86430
  playOnStart  get=0x02D480A0  set=0x02D480B0
  explicitActiveLayers  get=0x04D86F40  set=0x04D86F60
  activeLayers  get=0x04D86530  set=0x04D86580
  directorGroup  get=0x0AC610EC
  actorGroup  get=0x0AC60E88
  abortDirectorCamera  get=0x012081B0
  multiSelectOverlapCheck  get=0x012081B0
  isBakingRootMotion  get=0x04D890B0  set=0x04DA9BE0
  cameraTrack  get=0x0AC60FB0
  currentTime  get=0x04D91500  set=0x0AC617AC
  length  get=0x04D885A0  set=0x0AC617D0
  viewTimeMin  get=0x04D86E00  set=0x0AC618B8
  viewTimeMax  get=0x04D88400  set=0x0AC6182C
  playTimeMin  get=0x04D88480  set=0x0AC61808
  playTimeMax  get=0x04D88470  set=0x0AC617E0
  playbackSpeed  get=0x04D863E0  set=0x04D86460
  playingDirection  get=0x04D90C40  set=0x04D90C60
  playingWrapMode  get=0x04D86B60  set=0x04D86BC0
  directables  get=0x04D85EE0  set=0x06352D0C
  isActive  get=0x04D88020  set=0x04D88040
  isPaused  get=0x04D88030  set=0x04D88050
  previousTime  get=0x04DA94B0  set=0x04DA9BF0
  Slate.IDirector.isReSampleFrame  get=0x04D89060
  Slate.IDirector.context  get=0x0AC5FE10
  Slate.IDirector.children  get=0x0AC5FDD4
  remainingTime  get=0x0AC61448
EVENTS:
  OnCutsceneStarted  add=add_OnCutsceneStarted  remove=remove_OnCutsceneStarted
  OnCutsceneStopped  add=add_OnCutsceneStopped  remove=remove_OnCutsceneStopped
  OnSectionReached  add=add_OnSectionReached  remove=remove_OnSectionReached
  OnGlobalMessageSend  add=add_OnGlobalMessageSend  remove=remove_OnGlobalMessageSend
  OnStop  add=add_OnStop  remove=remove_OnStop
METHODS:
  RVA=0x0AC5CA64  token=0x6000335  System.Void Awake()
  RVA=0x0AC5FE6C  token=0x6000336  System.Void Start()
  RVA=0x0AC5E674  token=0x6000337  System.Void OnDestroy()
  RVA=0x0AC5E640  token=0x6000338  System.Void ManualUpdate(System.Single deltaTime)
  RVA=0x0AC5E5DC  token=0x6000339  System.Void LateUpdate()
  RVA=0x0AC5D05C  token=0x600033A  System.Void FixedUpdate()
  RVA=0x0AC5CBF4  token=0x600033B  System.Void DirectorSectionReached(Slate.Section section)
  RVA=0x0AC5D0A4  token=0x600033C  System.Collections.Generic.IEnumerable<UnityEngine.GameObject> GetAffectedActors()
  RVA=0x0AC5D310  token=0x600033D  System.Single[] GetPointerTimes()
  RVA=0x0AC5F330  token=0x600033E  System.Void Play()
  RVA=0x0AC5F09C  token=0x600033F  System.Void Play(System.Action callback)
  RVA=0x0AC5F0AC  token=0x6000340  System.Void Play(System.Single startTime)
  RVA=0x0AC5EE94  token=0x6000341  System.Void Play(System.Single startTime, System.Action callback)
  RVA=0x0AC5F0D4  token=0x6000342  System.Void Play(System.Single startTime, System.Single endTime, Slate.Cutscene.WrapMode wrapMode, System.Action callback, Slate.Cutscene.PlayingDirection playDirection)
  RVA=0x0AC60044  token=0x6000343  System.Void Stop()
  RVA=0x0AC5FF18  token=0x6000344  System.Void Stop(Slate.Cutscene.StopMode stopMode)
  RVA=0x0AC5EC8C  token=0x6000345  System.Void PlayReverse()
  RVA=0x0AC5EC9C  token=0x6000346  System.Void PlayReverse(System.Single startTime, System.Single endTime)
  RVA=0x04DA9BC0  token=0x6000347  System.Void Pause()
  RVA=0x04DA9BD0  token=0x6000348  System.Void Resume()
  RVA=0x0AC5FD14  token=0x6000349  System.Void SkipAll()
  RVA=0x0AC5F694  token=0x600034A  System.Void Rewind()
  RVA=0x0AC5F650  token=0x600034B  System.Void RewindNoUndo()
  RVA=0x04DA9BB0  token=0x600034C  System.Void BreakCutsceneLoop()
  RVA=0x0AC5CB9C  token=0x600034D  System.Void BreakSectionLoop(System.Boolean alsoSkip)
  RVA=0x0AC5FDCC  token=0x600034E  System.Void Skip()
  RVA=0x0AC5FD40  token=0x600034F  System.Void SkipCurrentSection()
  RVA=0x0AC5E5B8  token=0x6000350  System.Boolean JumpToSection(System.String name)
  RVA=0x0AC5E530  token=0x6000351  System.Boolean JumpToSection(Slate.Section section)
  RVA=0x0AC5EB48  token=0x6000352  System.Boolean PlayFromSection(System.String name)
  RVA=0x0AC5EB88  token=0x6000353  System.Boolean PlayFromSection(System.String name, Slate.Cutscene.WrapMode wrap, System.Action callback)
  RVA=0x0AC5EB68  token=0x6000354  System.Boolean PlayFromSection(Slate.Section section)
  RVA=0x0AC5EBD4  token=0x6000355  System.Boolean PlayFromSection(Slate.Section section, Slate.Cutscene.WrapMode wrap, System.Action callback)
  RVA=0x0AC5EE14  token=0x6000356  System.Boolean PlaySection(System.String name)
  RVA=0x0AC5EDA8  token=0x6000357  System.Boolean PlaySection(System.String name, Slate.Cutscene.WrapMode wrap, System.Action callback)
  RVA=0x0AC5EDF4  token=0x6000358  System.Boolean PlaySection(Slate.Section section)
  RVA=0x0AC5ECC4  token=0x6000359  System.Boolean PlaySection(Slate.Section section, Slate.Cutscene.WrapMode wrap, System.Action callback)
  RVA=0x0AC5F6C4  token=0x600035A  System.Void Sample()
  RVA=0x0AC5F6D4  token=0x600035B  System.Void Sample(System.Single time)
  RVA=0x0AC5E138  token=0x600035C  System.Void Internal_SamplePointers(System.Single currentTime, System.Single previousTime)
  RVA=0x0AC5F35C  token=0x600035D  System.Void ReSample()
  RVA=0x0AC60238  token=0x600035E  System.Void Validate()
  RVA=0x0AC5D6B4  token=0x600035F  System.Void InitializeTimePointers()
  RVA=0x0AC5E930  token=0x6000360  System.Void OnSampleStarted()
  RVA=0x0AC5E770  token=0x6000361  System.Void OnSampleEnded()
  RVA=0x0AC5FBC4  token=0x6000362  System.Void SetLayersActive()
  RVA=0x0AC5F4C4  token=0x6000363  System.Void RestoreLayersActive()
  RVA=0x0AC60098  token=0x6000364  System.Void UpdateCutscene(System.Single delta)
  RVA=0x0AC5EE48  token=0x6000365  Slate.Cutscene Play(System.String name)
  RVA=0x0AC5EEBC  token=0x6000366  Slate.Cutscene Play(System.String name, System.Action callback)
  RVA=0x0AC5CE48  token=0x6000367  Slate.Cutscene FindFromResources(System.String name)
  RVA=0x0AC5CF90  token=0x6000368  Slate.Cutscene Find(System.String name)
  RVA=0x0AC5FE80  token=0x6000369  System.Void StopAllCutscenes()
  RVA=0x0AC5F7E4  token=0x600036A  System.Void SendGlobalMessage(System.String message, System.Object value)
  RVA=0x0AC5F9E0  token=0x600036B  System.Void SetGroupActorOfName(System.String groupName, UnityEngine.GameObject newActor)
  RVA=-1  // generic def  token=0x600036C  T FindElement(System.String path)
  RVA=0x0AC5CCC4  token=0x600036D  Slate.IDirectable FindElement(System.String path)
  RVA=0x0AC60050  token=0x600036E  System.String ToString()
  RVA=0x0AC5D444  token=0x600036F  Slate.Section GetSectionByName(System.String name)
  RVA=0x0AC5D474  token=0x6000370  Slate.Section GetSectionByUID(System.String UID)
  RVA=0x0AC5D660  token=0x6000371  Slate.Section[] GetSections()
  RVA=0x0AC5D4A4  token=0x6000372  System.Single GetSectionLength(System.String name)
  RVA=0x0AC5D528  token=0x6000373  System.String[] GetSectionNames()
  RVA=0x0AC5D1CC  token=0x6000374  System.String[] GetDefinedEventNames()
  RVA=0x0AC5F33C  token=0x6000375  System.Void PreInitialize()
  RVA=0x0AC5F3CC  token=0x6000376  System.Void RenderCutscene(System.Int32 width, System.Int32 height, System.Int32 frameRate, System.Action<UnityEngine.Texture2D[]> callback)
  RVA=0x0AC5E09C  token=0x6000377  System.Collections.IEnumerator Internal_RenderCutscene(System.Int32 width, System.Int32 height, System.Int32 frameRate, System.Action<UnityEngine.Texture2D[]> callback)
  RVA=0x0AC60AB8  token=0x6000378  System.Void .ctor()
  RVA=0x0AC60A24  token=0x6000379  System.Void .cctor()
END_CLASS

CLASS: Slate.CutsceneGroup
TYPE:  abstract class
TOKEN: 0x200006E
SIZE:  0x70
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Slate.IDirectable
FIELDS:
  private           System.Action<Slate.Section>    OnSectionReached  // 0x18
  private           System.Collections.Generic.List<Slate.CutsceneTrack>_tracks  // 0x20
  private           System.Collections.Generic.List<Slate.Section>_sections  // 0x28
  private           System.Boolean                  _isCollapsed  // 0x30
  private           System.Boolean                  _active  // 0x31
  private           System.Boolean                  _isLocked  // 0x32
  private           System.Boolean                  _isReadOnly  // 0x33
  private           Slate.TransformSnapshot         transformSnapshot  // 0x38
  private           Slate.ObjectSnapshot            objectSnapshot  // 0x40
  private           UnityEngine.GameObject          refDuplicateActor  // 0x48
  private   static  System.Int32                    totalCustomToggleNum  // static @ 0x0
  public            System.Collections.Generic.List<Slate.CutsceneGroup.CustomToggleInfo>customToggle  // 0x50
  private           System.Boolean                  m_customToggleValid  // 0x58
  private           System.Boolean                  m_customToggleCheckDirty  // 0x59
  public            System.Collections.Generic.Dictionary<System.Int32,Slate.CutsceneGroup.CustomToggleInfo>customInfoCache  // 0x60
  private           Slate.IDirector                 <root>k__BackingField  // 0x68
PROPERTIES:
  name  get=-1  // abstract  set=-1  // abstract
  actor  get=-1  // abstract  set=-1  // abstract
  referenceMode  get=-1  // abstract  set=-1  // abstract
  initialTransformation  get=-1  // abstract  set=-1  // abstract
  initialLocalPosition  get=-1  // abstract  set=-1  // abstract
  initialLocalRotation  get=-1  // abstract  set=-1  // abstract
  displayVirtualMeshGizmo  get=-1  // abstract  set=-1  // abstract
  needActor  get=0x02FFF600
  updateOrder  get=0x04DA3400
  customToggleValid  get=0x0AC6E080
  tracks  get=0x04D862C0  set=0x05390F40
  sections  get=0x04D86240  set=0x02FDB880
  Slate.IDirectable.children  get=0x0AC6D04C
  Slate.IDirectable.startTime  get=0x04D879D0
  Slate.IDirectable.endTime  get=0x0AC6D088
  Slate.IDirectable.blendIn  get=0x04D879D0
  Slate.IDirectable.blendOut  get=0x04D879D0
  Slate.IDirectable.canCrossBlend  get=0x012081B0
  Slate.IDirectable.parent  get=0x011EC580
  root  get=0x04D86230  set=0x0485F170
  isActive  get=0x04D86AB0  set=0x0AC6E188
  isCollapsed  get=0x04D865F0  set=0x04D86620
  isLocked  get=0x0AC6E0EC  set=0x04D86DD0
  isReadOnly  get=0x04DA9CA0  set=0x04DA9CB0
EVENTS:
  OnSectionReached  add=add_OnSectionReached  remove=remove_OnSectionReached
METHODS:
  RVA=0x0AC6CC90  token=0x60003A2  System.Void SetCustomToggleDirty()
  RVA=0x0AC6D604  token=0x60003A4  System.Void _OnCustomToggleValueChanged()
  RVA=0x0AC6C4BC  token=0x60003A5  System.Boolean CheckCustomCollapsed(System.Int32 index)
  RVA=0x0AC6D760  token=0x60003A6  System.Void _TryRefreshCustomToggleInfo()
  RVA=0x0AC6D190  token=0x60003BC  System.String ToString()
  RVA=0x0AC6D310  token=0x60003BD  System.Void Validate(Slate.IDirector root, Slate.IDirectable parent)
  RVA=0x0AC6C7E4  token=0x60003BE  Slate.Section GetSectionByName(System.String name)
  RVA=0x0AC6C934  token=0x60003BF  Slate.Section GetSectionByUID(System.String UID)
  RVA=0x0AC6C64C  token=0x60003C0  Slate.Section GetSectionAfter(System.Single time)
  RVA=0x0AC6C718  token=0x60003C1  Slate.Section GetSectionBefore(System.Single time)
  RVA=0x0AC6CD70  token=0x60003C2  System.Boolean Slate.IDirectable.Initialize()
  RVA=0x0AC6CC9C  token=0x60003C3  System.Void Slate.IDirectable.Enter()
  RVA=0x0AC6CEB4  token=0x60003C4  System.Void Slate.IDirectable.Reverse()
  RVA=0x0AC6CF14  token=0x60003C5  System.Void Slate.IDirectable.Update(System.Single time, System.Single previousTime)
  RVA=0x0AC6CD10  token=0x60003C6  System.Void Slate.IDirectable.Exit()
  RVA=0x0AC6CE54  token=0x60003C7  System.Void Slate.IDirectable.ReverseEnter()
  RVA=0x041E1670  token=0x60003C8  System.Void Slate.IDirectable.RootEnabled()
  RVA=0x041E1670  token=0x60003C9  System.Void Slate.IDirectable.RootDisabled()
  RVA=0x041E1670  token=0x60003CA  System.Void Slate.IDirectable.RootUpdated(System.Single time, System.Single previousTime)
  RVA=0x041E1670  token=0x60003CB  System.Void Slate.IDirectable.RootDestroyed()
  RVA=0x0AC6D0A4  token=0x60003CC  System.Void StoreActorState()
  RVA=0x0AC6CB14  token=0x60003CD  System.Void RestoreActorState()
  RVA=0x0AC6D1A0  token=0x60003CE  System.Void TryInstantiateLocalActor()
  RVA=0x0AC6CA04  token=0x60003CF  System.Void ReleaseLocalActorInstance()
  RVA=0x0AC6CB64  token=0x60003D0  System.Void SetActorLocalCoords()
  RVA=0x0AC6CAA8  token=0x60003D1  UnityEngine.GameObject ResolveActor(UnityEngine.GameObject original)
  RVA=0x0AC6DE44  token=0x60003D2  System.Void .ctor()
  RVA=0x0AC6DE08  token=0x60003D3  System.Void .cctor()
END_CLASS

CLASS: Slate.CutsceneTrack
TYPE:  abstract class
TOKEN: 0x2000077
SIZE:  0x50
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Slate.IDirectable
FIELDS:
  private           System.String                   _name  // 0x18
  private           UnityEngine.Color               _color  // 0x20
  private           System.Boolean                  _active  // 0x30
  private           System.Boolean                  _isLocked  // 0x31
  private           System.Collections.Generic.List<Slate.ActionClip>_actionClips  // 0x38
  private           System.Boolean                  _isReadOnly  // 0x40
  private           System.Int32                    <layerOrder>k__BackingField  // 0x44
  private           Slate.IDirectable               <parent>k__BackingField  // 0x48
PROPERTIES:
  actor  get=0x0AC6ECE0
  name  get=0x0AC6EE48  set=0x0AC6EF2C
  color  get=0x0AC6ED08
  clips  get=0x04D85A50  set=0x046A91B0
  info  get=0x0AC6ED60
  Slate.IDirectable.children  get=0x0AC6E92C
  layerOrder  get=0x04D86530  set=0x04D86580
  root  get=0x0AC6EEAC
  parent  get=0x04D86200  set=0x035A41A0
  isCollapsed  get=0x0AC6EDC4
  isActive  get=0x0AC6ED98  set=0x0AC6EEEC
  isLocked  get=0x0AC6EDE4  set=0x04D86AC0
  startTime  get=0x0AC6EECC  set=0x041E1670
  endTime  get=0x0AC6ED40  set=0x041E1670
  blendIn  get=0x04D879D0  set=0x041E1670
  blendOut  get=0x04D879D0  set=0x041E1670
  canCrossBlend  get=0x012081B0
  needActor  get=0x0AC6EE8C
  updateOrder  get=0x04DA3400
  isReadOnly  get=0x0AC6EE18  set=0x02D480B0
  canDelete  get=0x0AC6ED00
METHODS:
  RVA=0x0AC6E8F8  token=0x6000408  System.Boolean Slate.IDirectable.Initialize()
  RVA=0x0AC6E8D8  token=0x6000409  System.Void Slate.IDirectable.Enter()
  RVA=0x0AC6E918  token=0x600040A  System.Void Slate.IDirectable.Update(System.Single time, System.Single previousTime)
  RVA=0x0AC6E8E8  token=0x600040B  System.Void Slate.IDirectable.Exit()
  RVA=0x0AC6E908  token=0x600040C  System.Void Slate.IDirectable.ReverseEnter()
  RVA=0x085D1220  token=0x600040D  System.Void Slate.IDirectable.Reverse()
  RVA=0x041E1670  token=0x600040E  System.Void Slate.IDirectable.RootEnabled()
  RVA=0x041E1670  token=0x600040F  System.Void Slate.IDirectable.RootDisabled()
  RVA=0x041E1670  token=0x6000410  System.Void Slate.IDirectable.RootUpdated(System.Single time, System.Single previousTime)
  RVA=0x041E1670  token=0x6000411  System.Void Slate.IDirectable.RootDestroyed()
  RVA=0x0AC6E8B0  token=0x6000412  System.Void PostCreate(Slate.IDirectable parent)
  RVA=0x0AC6E9EC  token=0x6000413  System.Void Validate(Slate.IDirector root, Slate.IDirectable parent)
  RVA=0x041E1670  token=0x6000414  System.Void OnCreate()
  RVA=0x041E1670  token=0x6000415  System.Void OnAfterValidate()
  RVA=0x02FFF600  token=0x6000416  System.Boolean OnInitialize()
  RVA=0x041E1670  token=0x6000417  System.Void OnEnter()
  RVA=0x041E1670  token=0x6000418  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x041E1670  token=0x6000419  System.Void OnExit()
  RVA=0x041E1670  token=0x600041A  System.Void OnReverseEnter()
  RVA=0x041E1670  token=0x600041B  System.Void OnReverse()
  RVA=0x041E1670  token=0x600041C  System.Void OnDrawGizmosSelected()
  RVA=0x041E1670  token=0x600041D  System.Void OnSceneGUI()
  RVA=0x041E1670  token=0x600041E  System.Void OnRootEnabled()
  RVA=0x041E1670  token=0x600041F  System.Void OnRootDisabled()
  RVA=0x041E1670  token=0x6000420  System.Void OnRootUpdated(System.Single time, System.Single previousTime)
  RVA=0x041E1670  token=0x6000421  System.Void OnRootDestroyed()
  RVA=0x0AC6E818  token=0x6000422  System.Single GetTrackWeight()
  RVA=0x0AC6E7BC  token=0x6000423  System.Single GetTrackWeight(System.Single time)
  RVA=0x0AC4FEA8  token=0x6000424  System.Single GetTrackWeight(System.Single time, System.Single blendInOut)
  RVA=0x0AC4FEC4  token=0x6000425  System.Single GetTrackWeight(System.Single time, System.Single blendIn, System.Single blendOut)
  RVA=-1  // generic def  token=0x6000426  T AddAction(System.Single time)
  RVA=0x0AC6E4DC  token=0x6000427  Slate.ActionClip AddAction(System.Type type, System.Single time)
  RVA=0x0AC6EC48  token=0x6000428  System.Void .ctor()
  RVA=0x0AC6E968  token=0x6000429  System.Boolean <Validate>b__72_1(Slate.IDirectable t)
END_CLASS

CLASS: Slate.IAnimatableData
TYPE:  interface
TOKEN: 0x200007A
FIELDS:
PROPERTIES:
  isValid  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000430  UnityEngine.AnimationCurve[] GetCurves()
  RVA=-1  // abstract  token=0x6000431  System.Void Validate(Slate.IKeyable keyable)
  RVA=-1  // abstract  token=0x6000432  System.Void SetVirtualTransformParent(UnityEngine.Transform virtualTransformParent)
  RVA=-1  // abstract  token=0x6000433  System.Void SetSnapshot()
  RVA=-1  // abstract  token=0x6000434  System.Void Evaluate(System.Single time, System.Single previousTime, System.Single weight)
  RVA=-1  // abstract  token=0x6000435  System.Void RestoreSnapshot()
  RVA=-1  // abstract  token=0x6000436  System.Void SetKeyCurrent(System.Single time)
  RVA=-1  // abstract  token=0x6000437  System.Boolean TryAutoKey(System.Single time)
  RVA=-1  // abstract  token=0x6000438  System.Boolean TryKeyIdentity(System.Single time)
  RVA=-1  // abstract  token=0x6000439  System.Void RemoveKey(System.Single time)
  RVA=-1  // abstract  token=0x600043A  System.Boolean HasKey(System.Single time)
  RVA=-1  // abstract  token=0x600043B  System.Boolean HasAnyKey()
  RVA=-1  // abstract  token=0x600043C  System.Boolean HasChanged()
  RVA=-1  // abstract  token=0x600043D  System.Single GetKeyNext(System.Single time)
  RVA=-1  // abstract  token=0x600043E  System.Single GetKeyPrevious(System.Single time)
  RVA=-1  // abstract  token=0x600043F  System.String GetKeyLabel(System.Single time)
  RVA=-1  // abstract  token=0x6000440  System.Void SetPreWrapMode(UnityEngine.WrapMode mode)
  RVA=-1  // abstract  token=0x6000441  System.Void SetPostWrapMode(UnityEngine.WrapMode mode)
  RVA=-1  // abstract  token=0x6000442  System.Void Reset()
END_CLASS

CLASS: Slate.IDirectable
TYPE:  interface
TOKEN: 0x200007B
FIELDS:
PROPERTIES:
  root  get=-1  // abstract
  parent  get=-1  // abstract
  children  get=-1  // abstract
  actor  get=-1  // abstract
  name  get=-1  // abstract
  isActive  get=-1  // abstract
  isCollapsed  get=-1  // abstract
  isLocked  get=-1  // abstract
  startTime  get=-1  // abstract
  endTime  get=-1  // abstract
  blendIn  get=-1  // abstract
  blendOut  get=-1  // abstract
  canCrossBlend  get=-1  // abstract
  needActor  get=-1  // abstract
  updateOrder  get=-1  // abstract
  isReadOnly  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000450  System.Void Validate(Slate.IDirector root, Slate.IDirectable parent)
  RVA=-1  // abstract  token=0x6000451  System.Boolean Initialize()
  RVA=-1  // abstract  token=0x6000452  System.Void Enter()
  RVA=-1  // abstract  token=0x6000453  System.Void Exit()
  RVA=-1  // abstract  token=0x6000454  System.Void Update(System.Single time, System.Single previousTime)
  RVA=-1  // abstract  token=0x6000455  System.Void ReverseEnter()
  RVA=-1  // abstract  token=0x6000456  System.Void Reverse()
  RVA=-1  // abstract  token=0x6000457  System.Void RootEnabled()
  RVA=-1  // abstract  token=0x6000458  System.Void RootUpdated(System.Single time, System.Single previousTime)
  RVA=-1  // abstract  token=0x6000459  System.Void RootDisabled()
  RVA=-1  // abstract  token=0x600045A  System.Void RootDestroyed()
END_CLASS

CLASS: Slate.IKeyable
TYPE:  interface
TOKEN: 0x200007C
IMPLEMENTS: Slate.IDirectable
FIELDS:
PROPERTIES:
  animationData  get=-1  // abstract
  animatedParametersTarget  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Slate.ISubClipContainable
TYPE:  interface
TOKEN: 0x200007D
IMPLEMENTS: Slate.IDirectable
FIELDS:
PROPERTIES:
  subClipOffset  get=-1  // abstract  set=-1  // abstract
  subClipSpeed  get=-1  // abstract
  subClipLength  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Slate.IDirectableExtensions
TYPE:  static class
TOKEN: 0x200007E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AC71500  token=0x6000464  System.Single GetLength(Slate.IDirectable directable)
  RVA=0x0AC71D54  token=0x6000465  System.Single RootTimeToLocalTime(Slate.IDirectable directable)
  RVA=0x0AC71D18  token=0x6000466  System.Single RootTimeToLocalTimeUnclamped(Slate.IDirectable directable)
  RVA=0x0AC71C90  token=0x6000467  System.Boolean IsRootTimeWithinClip(Slate.IDirectable directable)
  RVA=0x0AC71DC0  token=0x6000468  System.Single ToLocalTime(Slate.IDirectable directable, System.Single time)
  RVA=0x0AC71D90  token=0x6000469  System.Single ToLocalTimeUnclamped(Slate.IDirectable directable, System.Single time)
  RVA=0x0AC71CCC  token=0x600046A  System.Boolean IsTimeWithinClip(Slate.IDirectable directable, System.Single time)
  RVA=0x0AC71218  token=0x600046B  System.Boolean CanCrossBlend(Slate.IDirectable directable, Slate.IDirectable other)
  RVA=0x0AC71028  token=0x600046C  System.Boolean CanBlendIn(Slate.IDirectable directable)
  RVA=0x0AC71120  token=0x600046D  System.Boolean CanBlendOut(Slate.IDirectable directable)
  RVA=0x0AC712A4  token=0x600046E  System.Boolean CanScale(Slate.IDirectable directable)
  RVA=0x0AC72004  token=0x600046F  System.Boolean WithinBufferTriggerRange(Slate.IDirectable directable, System.Single time, System.Single previousTime, System.Boolean bypass)
  RVA=0x0AC7138C  token=0x6000470  Slate.IDirectable FindChild(Slate.IDirectable directable, System.String name)
  RVA=-1  // generic def  token=0x6000471  T GetPreviousSibling(Slate.IDirectable directable)
  RVA=0x0AC71768  token=0x6000472  Slate.IDirectable GetPreviousSibling(Slate.IDirectable directable)
  RVA=-1  // generic def  token=0x6000473  T GetNextSibling(Slate.IDirectable directable)
  RVA=0x0AC715DC  token=0x6000474  Slate.IDirectable GetNextSibling(Slate.IDirectable directable)
  RVA=-1  // generic def  token=0x6000475  T GetFirstParentOfType(Slate.IDirectable directable)
  RVA=0x0AC71A94  token=0x6000476  System.Single GetWeight(Slate.IDirectable directable)
  RVA=0x0AC719A8  token=0x6000477  System.Single GetWeight(Slate.IDirectable directable, System.Single time)
  RVA=0x0AC4FEA8  token=0x6000478  System.Single GetWeight(Slate.IDirectable directable, System.Single time, System.Single blendInOut)
  RVA=0x0AC71A00  token=0x6000479  System.Single GetWeight(Slate.IDirectable directable, System.Single time, System.Single blendIn, System.Single blendOut)
  RVA=0x0AC71864  token=0x600047A  UnityEngine.Transform GetSpaceTransform(Slate.IDirectable directable, Slate.TransformSpace space, UnityEngine.GameObject actorOverride)
  RVA=0x0AC71E20  token=0x600047B  UnityEngine.Vector3 TransformPosition(Slate.IDirectable directable, UnityEngine.Vector3 point, Slate.TransformSpace space)
  RVA=0x0AC71AB8  token=0x600047C  UnityEngine.Vector3 InverseTransformPosition(Slate.IDirectable directable, UnityEngine.Vector3 point, Slate.TransformSpace space)
  RVA=0x0AC71EEC  token=0x600047D  UnityEngine.Quaternion TransformRotation(Slate.IDirectable directable, UnityEngine.Vector3 euler, Slate.TransformSpace space)
  RVA=0x0AC71B84  token=0x600047E  UnityEngine.Vector3 InverseTransformRotation(Slate.IDirectable directable, UnityEngine.Quaternion rot, Slate.TransformSpace space)
  RVA=0x0AC70EF4  token=0x600047F  UnityEngine.Vector3 ActorPositionInSpace(Slate.IDirectable directable, Slate.TransformSpace space)
  RVA=0x0AC716D8  token=0x6000480  System.Single GetPreviousLoopLocalTime(Slate.ISubClipContainable clip)
  RVA=0x0AC7153C  token=0x6000481  System.Single GetNextLoopLocalTime(Slate.ISubClipContainable clip)
  RVA=0x0AC71474  token=0x6000482  UnityEngine.AnimationCurve[] GetCurvesAll(Slate.IKeyable keyable)
END_CLASS

CLASS: Slate.IDirector
TYPE:  interface
TOKEN: 0x2000082
FIELDS:
PROPERTIES:
  children  get=-1  // abstract
  context  get=-1  // abstract
  length  get=-1  // abstract
  currentTime  get=-1  // abstract  set=-1  // abstract
  previousTime  get=-1  // abstract
  playbackSpeed  get=-1  // abstract  set=-1  // abstract
  isActive  get=-1  // abstract
  isPaused  get=-1  // abstract
  isReSampleFrame  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000494  System.Collections.Generic.IEnumerable<UnityEngine.GameObject> GetAffectedActors()
  RVA=-1  // abstract  token=0x6000495  System.Void Play()
  RVA=-1  // abstract  token=0x6000496  System.Void Pause()
  RVA=-1  // abstract  token=0x6000497  System.Void Stop()
  RVA=-1  // abstract  token=0x6000498  System.Void Sample(System.Single time)
  RVA=-1  // abstract  token=0x6000499  System.Void ReSample()
  RVA=-1  // abstract  token=0x600049A  System.Void Validate()
  RVA=-1  // abstract  token=0x600049B  System.Void SendGlobalMessage(System.String message, System.Object value)
END_CLASS

CLASS: Slate.Section
TYPE:  class
TOKEN: 0x2000083
SIZE:  0x48
FIELDS:
  public    static readonly UnityEngine.Color               DEFAULT_COLOR  // static @ 0x0
  private           System.String                   _UID  // 0x10
  private           System.String                   _name  // 0x18
  private           System.Single                   _time  // 0x20
  private           Slate.Section.ExitMode          _exitMode  // 0x24
  private           System.Int32                    _loopCount  // 0x28
  private           UnityEngine.Color               _color  // 0x2c
  private           System.Boolean                  _colorizeBackground  // 0x3c
  private           System.Int32                    <currentLoopIteration>k__BackingField  // 0x40
PROPERTIES:
  currentLoopIteration  get=0x04D865E0  set=0x04D86610
  UID  get=0x02B2ECC0  set=0x053908C0
  name  get=0x0385B100  set=0x05392C40
  time  get=0x011EC8A0  set=0x011EC9E0
  exitMode  get=0x011F0020  set=0x011F0030
  loopCount  get=0x011F2A90  set=0x011F2AA0
  color  get=0x0AC77064  set=0x04D8D3A0
  colorizeBackground  get=0x04D86BD0  set=0x04D86BE0
METHODS:
  RVA=0x0AC76FB4  token=0x60004AC  System.Void .ctor(System.String name, System.Single time)
  RVA=0x04DA9D20  token=0x60004AD  System.Void ResetLoops()
  RVA=0x04DA9D10  token=0x60004AE  System.Void BreakLoop()
  RVA=0x0AC76F28  token=0x60004AF  System.Boolean TryUpdateLoop()
  RVA=0x0AC76EB4  token=0x60004B0  System.String ToString()
  RVA=0x0AC76F48  token=0x60004B1  System.Void .cctor()
END_CLASS

CLASS: Slate.IDirectableTimePointer
TYPE:  interface
TOKEN: 0x2000085
FIELDS:
PROPERTIES:
  target  get=-1  // abstract
  time  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60004B4  System.Void TriggerForward(System.Single currentTime, System.Single previousTime)
  RVA=-1  // abstract  token=0x60004B5  System.Void TriggerBackward(System.Single currentTime, System.Single previousTime)
  RVA=-1  // abstract  token=0x60004B6  System.Void Update(System.Single currentTime, System.Single previousTime)
END_CLASS

CLASS: Slate.StartTimePointer
TYPE:  sealed struct
TOKEN: 0x2000086
SIZE:  0x20
IMPLEMENTS: Slate.IDirectableTimePointer
FIELDS:
  private           System.Boolean                  triggered  // 0x10
  private           System.Single                   lastTargetStartTime  // 0x14
  private           Slate.IDirectable               <target>k__BackingField  // 0x18
PROPERTIES:
  target  get=0x04D88320  set=0x067D5E9C
  Slate.IDirectableTimePointer.time  get=0x0AC78388
METHODS:
  RVA=0x0AC783A4  token=0x60004BA  System.Void .ctor(Slate.IDirectable target)
  RVA=0x0AC781BC  token=0x60004BB  System.Void Slate.IDirectableTimePointer.TriggerForward(System.Single currentTime, System.Single previousTime)
  RVA=0x0AC78238  token=0x60004BC  System.Void Slate.IDirectableTimePointer.Update(System.Single currentTime, System.Single previousTime)
  RVA=0x0AC78120  token=0x60004BD  System.Void Slate.IDirectableTimePointer.TriggerBackward(System.Single currentTime, System.Single previousTime)
END_CLASS

CLASS: Slate.EndTimePointer
TYPE:  sealed struct
TOKEN: 0x2000087
SIZE:  0x20
IMPLEMENTS: Slate.IDirectableTimePointer
FIELDS:
  private           System.Boolean                  triggered  // 0x10
  private           Slate.IDirectable               <target>k__BackingField  // 0x18
PROPERTIES:
  target  get=0x04D88320  set=0x067D5E9C
  Slate.IDirectableTimePointer.time  get=0x0AC70874
METHODS:
  RVA=0x0AC70890  token=0x60004C1  System.Void .ctor(Slate.IDirectable target)
  RVA=0x0AC70794  token=0x60004C2  System.Void Slate.IDirectableTimePointer.TriggerForward(System.Single currentTime, System.Single previousTime)
  RVA=0x0AC70828  token=0x60004C3  System.Void Slate.IDirectableTimePointer.Update(System.Single currentTime, System.Single previousTime)
  RVA=0x0AC7070C  token=0x60004C4  System.Void Slate.IDirectableTimePointer.TriggerBackward(System.Single currentTime, System.Single previousTime)
END_CLASS

CLASS: Slate.BezierPath
TYPE:  class
TOKEN: 0x2000088
SIZE:  0x38
EXTENDS: Slate.Path
FIELDS:
  public            System.Int32                    resolution  // 0x18
  private           System.Collections.Generic.List<Slate.BezierPoint>_points  // 0x20
  private           UnityEngine.Vector3[]           _sampledPathPoints  // 0x28
  private           System.Single                   _length  // 0x30
PROPERTIES:
  points  get=0x04D862C0
  Item  get=0x0AC6A8DC
  pointCount  get=0x0AC6A930
  length  get=0x04D86E20
METHODS:
  RVA=0x09DEBAEC  token=0x60004C9  System.Void Awake()
  RVA=0x09DEBAEC  token=0x60004CA  System.Void OnValidate()
  RVA=0x0AC6A19C  token=0x60004CB  System.Void Compute()
  RVA=0x09DEBAEC  token=0x60004CC  System.Void SetDirty()
  RVA=0x0AC6A010  token=0x60004CD  System.Void ComputeLength()
  RVA=0x0AC6A030  token=0x60004CE  System.Void ComputeSampledPathPoints()
  RVA=0x0AC6A1C4  token=0x60004CF  Slate.BezierPath Create(UnityEngine.Transform targetParent)
  RVA=0x0AC69F28  token=0x60004D0  Slate.BezierPoint AddPointAt(UnityEngine.Vector3 position, System.Int32 index)
  RVA=0x0AC6A7F4  token=0x60004D1  System.Void RemovePoint(Slate.BezierPoint point)
  RVA=0x0AC6A48C  token=0x60004D2  System.Int32 GetPointIndex(Slate.BezierPoint point)
  RVA=0x0AC6A6E8  token=0x60004D3  UnityEngine.Vector3[] GetSampledPathPositions(Slate.BezierPoint p1, Slate.BezierPoint p2, System.Int32 resolution)
  RVA=0x0AC6A618  token=0x60004D4  UnityEngine.Vector3 GetPositionAt(System.Single t)
  RVA=0x0AC6A510  token=0x60004D5  UnityEngine.Vector3 GetPositionAt(Slate.BezierPoint p1, Slate.BezierPoint p2, System.Single t)
  RVA=0x0AC6A854  token=0x60004D6  System.Void .ctor()
END_CLASS

CLASS: Slate.BezierPoint
TYPE:  class
TOKEN: 0x2000089
SIZE:  0x40
FIELDS:
  private           Slate.BezierPath                _path  // 0x10
  public            UnityEngine.Vector3             _position  // 0x18
  private           UnityEngine.Vector3             _handle1  // 0x24
  private           UnityEngine.Vector3             _handle2  // 0x30
  private           Slate.BezierPoint.HandleStyle   _handleStyle  // 0x3c
PROPERTIES:
  handleStyle  get=0x04D86640  set=0x04D86670
  path  get=0x02B2ECC0  set=0x053908C0
  position  get=0x0AC6AB08  set=0x0AC6AE60
  handle1LocalPosition  get=0x04D905E0  set=0x0AC6AB78
  handle2LocalPosition  get=0x04D88ED0  set=0x0AC6ACEC
  handle1Position  get=0x0AC6A9B0  set=0x0AC6AC30
  handle2Position  get=0x0AC6AA5C  set=0x0AC6ADA4
METHODS:
  RVA=0x0AC6A970  token=0x60004D7  System.Void .ctor(Slate.BezierPath path, UnityEngine.Vector3 position)
END_CLASS

CLASS: Slate.Path
TYPE:  abstract class
TOKEN: 0x200008B
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
PROPERTIES:
  length  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60004E7  UnityEngine.Vector3 GetPositionAt(System.Single t)
  RVA=-1  // abstract  token=0x60004E8  System.Void Compute()
  RVA=0x0AC72AE0  token=0x60004E9  UnityEngine.Vector3 GetPositionAlongCurve(UnityEngine.Vector3 from, UnityEngine.Vector3 to, UnityEngine.Vector3 fromTangent, UnityEngine.Vector3 toTangent, System.Single t)
  RVA=0x0AC72CD8  token=0x60004EA  UnityEngine.Vector3 GetPosition(System.Single t, UnityEngine.Vector3[] path)
  RVA=0x0AC72A34  token=0x60004EB  System.Single GetLength(UnityEngine.Vector3[] path)
  RVA=0x05393520  token=0x60004EC  System.Void .ctor()
END_CLASS

CLASS: Slate.AnimationDataUtility
TYPE:  static class
TOKEN: 0x200008C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AC69048  token=0x60004ED  System.String[] GetAnimatableMemberPaths(System.Object root)
  RVA=0x0AC690A4  token=0x60004EE  System.String[] Internal_GetAnimatableMemberPaths(System.Type type, System.String path)
END_CLASS

CLASS: Slate.AnimatorDispatcher
TYPE:  class
TOKEN: 0x200008D
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Action<System.Int32>     onAnimatorIK  // 0x18
  private           UnityEngine.Animator            _animator  // 0x20
PROPERTIES:
  animator  get=0x0AC694BC
EVENTS:
  onAnimatorIK  add=add_onAnimatorIK  remove=remove_onAnimatorIK
METHODS:
  RVA=0x0AC6941C  token=0x60004F2  System.Void OnAnimatorIK(System.Int32 index)
  RVA=0x05393520  token=0x60004F3  System.Void .ctor()
END_CLASS

CLASS: Slate.AudioSampler
TYPE:  static class
TOKEN: 0x200008E
SIZE:  0x10
FIELDS:
  private   static  System.String                   ROOT_NAME  // const
  private   static  UnityEngine.GameObject          root  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<System.Object,UnityEngine.AudioSource>sources  // static @ 0x8
METHODS:
  RVA=0x0AC695D4  token=0x60004F4  UnityEngine.AudioSource GetSourceForID(System.Object keyID)
  RVA=0x0AC698B0  token=0x60004F5  System.Void ReleaseSourceForID(System.Object keyID)
  RVA=0x0AC69A3C  token=0x60004F6  System.Void SampleForID(System.Object keyID, UnityEngine.AudioClip clip, System.Single time, System.Single previousTime, System.Single volume)
  RVA=0x0AC69B00  token=0x60004F7  System.Void SampleForID(System.Object keyID, UnityEngine.AudioClip clip, System.Single time, System.Single previousTime, Slate.AudioSampler.SampleSettings settings)
  RVA=0x0AC69BA4  token=0x60004F8  System.Void Sample(UnityEngine.AudioSource source, UnityEngine.AudioClip clip, System.Single time, System.Single previousTime, System.Single volume)
  RVA=0x0AC69C68  token=0x60004F9  System.Void Sample(UnityEngine.AudioSource source, UnityEngine.AudioClip clip, System.Single time, System.Single previousTime, Slate.AudioSampler.SampleSettings settings)
  RVA=0x0AC69E94  token=0x60004FA  System.Void .cctor()
END_CLASS

CLASS: Slate.ColorUtility
TYPE:  static class
TOKEN: 0x2000090
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04DA6870  token=0x60004FC  UnityEngine.Color Grey(System.Single value)
  RVA=0x04DA6890  token=0x60004FD  UnityEngine.Color WithAlpha(UnityEngine.Color color, System.Single alpha)
END_CLASS

CLASS: Slate.TangentMode
TYPE:  sealed struct
TOKEN: 0x2000091
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.TangentMode               Editable  // const
  public    static  Slate.TangentMode               Smooth  // const
  public    static  Slate.TangentMode               Linear  // const
  public    static  Slate.TangentMode               Constant  // const
METHODS:
END_CLASS

CLASS: Slate.CurveUtility
TYPE:  static class
TOKEN: 0x2000092
SIZE:  0x10
FIELDS:
  private   static  System.Single                   KEY_PROXIMITY_TOLERANCE  // const
METHODS:
  RVA=0x0AC6BA24  token=0x60004FE  System.Single GetKeyNext(System.Single time, UnityEngine.AnimationCurve[] curves)
  RVA=0x0AC6BC70  token=0x60004FF  System.Single GetKeyPrevious(System.Single time, UnityEngine.AnimationCurve[] curves)
  RVA=0x0AC6BF2C  token=0x6000500  System.Boolean HasKey(System.Single time, UnityEngine.AnimationCurve[] curves)
  RVA=0x0AC6BED8  token=0x6000501  System.Boolean HasAnyKey(UnityEngine.AnimationCurve[] curves)
  RVA=0x0AC6C160  token=0x6000502  System.Void OffsetCurveValue(UnityEngine.AnimationCurve curve, System.Single deltaValue)
  RVA=0x0AC6C070  token=0x6000503  System.Void OffsetCurveTime(UnityEngine.AnimationCurve curve, System.Single deltaTime)
  RVA=0x0AC6C340  token=0x6000504  System.Void RemoveNegativeKeys(UnityEngine.AnimationCurve curve)
  RVA=0x0AC6C200  token=0x6000505  System.Void RemoveKeysOffRange(UnityEngine.AnimationCurve curve, System.Single min, System.Single max)
END_CLASS

CLASS: Slate.DecoratorAttribute
TYPE:  class
TOKEN: 0x2000097
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Type                     targetType  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000510  System.Void .ctor(System.Type targetType)
END_CLASS

CLASS: Slate.IDecorator
TYPE:  interface
TOKEN: 0x2000098
FIELDS:
PROPERTIES:
  Target  get=-1  // abstract  set=-1  // abstract
METHODS:
END_CLASS

CLASS: Slate.DecoratorFactory
TYPE:  static class
TOKEN: 0x2000099
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Type>decoratorsTypeMap  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<System.Object,Slate.IDecorator>decoratorsInstanceMap  // static @ 0x8
METHODS:
  RVA=-1  // generic def  token=0x6000513  T Decorator(System.Object target)
  RVA=-1  // generic def  token=0x6000514  T GetDecorator(System.Object target)
  RVA=-1  // generic def  token=0x6000515  T CreateDecorator(System.Type targetType)
  RVA=0x0AC6EF74  token=0x6000516  System.Void .cctor()
END_CLASS

CLASS: Slate.EaseType
TYPE:  sealed struct
TOKEN: 0x200009A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Slate.EaseType                  Linear  // const
  public    static  Slate.EaseType                  QuadraticIn  // const
  public    static  Slate.EaseType                  QuadraticOut  // const
  public    static  Slate.EaseType                  QuadraticInOut  // const
  public    static  Slate.EaseType                  QuarticIn  // const
  public    static  Slate.EaseType                  QuarticOut  // const
  public    static  Slate.EaseType                  QuarticInOut  // const
  public    static  Slate.EaseType                  QuinticIn  // const
  public    static  Slate.EaseType                  QuinticOut  // const
  public    static  Slate.EaseType                  QuinticInOut  // const
  public    static  Slate.EaseType                  CubicIn  // const
  public    static  Slate.EaseType                  CubicOut  // const
  public    static  Slate.EaseType                  CubicInOut  // const
  public    static  Slate.EaseType                  ExponentialIn  // const
  public    static  Slate.EaseType                  ExponentialOut  // const
  public    static  Slate.EaseType                  ExponentialInOut  // const
  public    static  Slate.EaseType                  CircularIn  // const
  public    static  Slate.EaseType                  CircularOut  // const
  public    static  Slate.EaseType                  CircularInOut  // const
  public    static  Slate.EaseType                  SinusoidalIn  // const
  public    static  Slate.EaseType                  SinusoidalOut  // const
  public    static  Slate.EaseType                  SinusoidalInOut  // const
  public    static  Slate.EaseType                  ElasticIn  // const
  public    static  Slate.EaseType                  ElasticOut  // const
  public    static  Slate.EaseType                  ElasticInOut  // const
  public    static  Slate.EaseType                  BounceIn  // const
  public    static  Slate.EaseType                  BounceOut  // const
  public    static  Slate.EaseType                  BounceInOut  // const
  public    static  Slate.EaseType                  BackIn  // const
  public    static  Slate.EaseType                  BackOut  // const
  public    static  Slate.EaseType                  BackInOut  // const
METHODS:
END_CLASS

CLASS: Slate.Easing
TYPE:  static class
TOKEN: 0x200009B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AC6FCF4  token=0x6000517  System.Single Ease(Slate.EaseType type, System.Single from, System.Single to, System.Single t)
  RVA=0x0AC6FEF8  token=0x6000518  UnityEngine.Vector3 Ease(Slate.EaseType type, UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single t)
  RVA=0x0AC6FD78  token=0x6000519  UnityEngine.Quaternion Ease(Slate.EaseType type, UnityEngine.Quaternion from, UnityEngine.Quaternion to, System.Single t)
  RVA=0x0AC6FE3C  token=0x600051A  UnityEngine.Color Ease(Slate.EaseType type, UnityEngine.Color from, UnityEngine.Color to, System.Single t)
  RVA=0x0AC6FFDC  token=0x600051B  System.Func<System.Single,System.Single> Function(Slate.EaseType type)
  RVA=0x041E1670  token=0x600051C  System.Single Linear(System.Single t)
  RVA=0x04D886E0  token=0x600051D  System.Single QuadraticIn(System.Single t)
  RVA=0x04DA6AB0  token=0x600051E  System.Single QuadraticOut(System.Single t)
  RVA=0x0A9A0E30  token=0x600051F  System.Single QuadraticInOut(System.Single t)
  RVA=0x04DA6AD0  token=0x6000520  System.Single QuarticIn(System.Single t)
  RVA=0x04DA6AE0  token=0x6000521  System.Single QuarticOut(System.Single t)
  RVA=0x0A9A0E8C  token=0x6000522  System.Single QuarticInOut(System.Single t)
  RVA=0x04DA6B10  token=0x6000523  System.Single QuinticIn(System.Single t)
  RVA=0x04DA6B30  token=0x6000524  System.Single QuinticOut(System.Single t)
  RVA=0x0A9A0EE0  token=0x6000525  System.Single QuinticInOut(System.Single t)
  RVA=0x04D89930  token=0x6000526  System.Single CubicIn(System.Single t)
  RVA=0x04DA69A0  token=0x6000527  System.Single CubicOut(System.Single t)
  RVA=0x0A9A01E0  token=0x6000528  System.Single CubicInOut(System.Single t)
  RVA=0x0A9A0F3C  token=0x6000529  System.Single SinusoidalIn(System.Single t)
  RVA=0x0A9A0F6C  token=0x600052A  System.Single SinusoidalOut(System.Single t)
  RVA=0x04DA6B60  token=0x600052B  System.Single SinusoidalInOut(System.Single t)
  RVA=0x0A9A0690  token=0x600052C  System.Single ExponentialIn(System.Single t)
  RVA=0x0A9A06BC  token=0x600052D  System.Single ExponentialOut(System.Single t)
  RVA=0x0A9A0610  token=0x600052E  System.Single ExponentialInOut(System.Single t)
  RVA=0x04DA6910  token=0x600052F  System.Single CircularIn(System.Single t)
  RVA=0x04DA6970  token=0x6000530  System.Single CircularOut(System.Single t)
  RVA=0x0A9A017C  token=0x6000531  System.Single CircularInOut(System.Single t)
  RVA=0x04DA69C0  token=0x6000532  System.Single ElasticIn(System.Single t)
  RVA=0x04DA6A40  token=0x6000533  System.Single ElasticOut(System.Single t)
  RVA=0x0A9A0530  token=0x6000534  System.Single ElasticInOut(System.Single t)
  RVA=0x0A9A00B0  token=0x6000535  System.Single BounceIn(System.Single t)
  RVA=0x0A9A00E4  token=0x6000536  System.Single BounceOut(System.Single t)
  RVA=0x0A9A0044  token=0x6000537  System.Single BounceInOut(System.Single t)
  RVA=0x04DA68C0  token=0x6000538  System.Single BackIn(System.Single t)
  RVA=0x04DA68E0  token=0x6000539  System.Single BackOut(System.Single t)
  RVA=0x0A99FFDC  token=0x600053A  System.Single BackInOut(System.Single t)
END_CLASS

CLASS: Slate.ITransformRefParameter
TYPE:  interface
TOKEN: 0x200009C
FIELDS:
PROPERTIES:
  transform  get=-1  // abstract
  space  get=-1  // abstract
  useAnimation  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Slate.ObjectSnapshot
TYPE:  class
TOKEN: 0x200009D
SIZE:  0x18
FIELDS:
  private   static  System.Collections.Generic.List<System.Type>ignoreTypes  // static @ 0x0
  private           System.Collections.Generic.Dictionary<UnityEngine.Object,System.String>serialized  // 0x10
METHODS:
  RVA=0x0AC7235C  token=0x600053E  System.Void AppendIgnoreType(System.Type type)
  RVA=0x0AC72994  token=0x600053F  System.Void .ctor(UnityEngine.Object target, System.Boolean fullObjectHierarchy)
  RVA=0x0AC7264C  token=0x6000540  System.Void Store(UnityEngine.Object target, System.Boolean fullObjectHierarchy)
  RVA=0x0AC7240C  token=0x6000541  System.Void Restore()
  RVA=0x0AC72908  token=0x6000542  System.Void .cctor()
END_CLASS

CLASS: Slate.RectUtility
TYPE:  static class
TOKEN: 0x200009E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A996768  token=0x6000543  UnityEngine.Rect GetBoundRect(UnityEngine.Rect[] rects)
  RVA=0x0A996888  token=0x6000544  UnityEngine.Rect GetBoundRect(UnityEngine.Vector2[] positions)
  RVA=0x0AC74978  token=0x6000545  System.Boolean Encapsulates(UnityEngine.Rect a, UnityEngine.Rect b)
  RVA=0x0AC74A94  token=0x6000546  UnityEngine.Rect ExpandBy(UnityEngine.Rect rect, System.Single margin)
  RVA=0x0AC74A30  token=0x6000547  UnityEngine.Rect ExpandBy(UnityEngine.Rect rect, System.Single xMargin, System.Single yMargin)
  RVA=0x0A996A18  token=0x6000548  UnityEngine.Rect TransformSpace(UnityEngine.Rect rect, UnityEngine.Rect oldContainer, UnityEngine.Rect newContainer)
  RVA=0x0AC74AD0  token=0x6000549  UnityEngine.Rect ToViewRect(UnityEngine.Bounds b, UnityEngine.Camera cam)
END_CLASS

CLASS: Slate.ReflectionTools
TYPE:  static class
TOKEN: 0x200009F
SIZE:  0x10
FIELDS:
  private   static  System.Reflection.BindingFlags  flagsEverything  // const
  private   static  System.Collections.Generic.List<System.Reflection.Assembly>_loadedAssemblies  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<System.String,System.Type>typeMap  // static @ 0x8
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Type[]>subTypesMap  // static @ 0x10
PROPERTIES:
  loadedAssemblies  get=0x0AC76DE4
METHODS:
  RVA=0x0AC76184  token=0x600054B  System.Type GetType(System.String typeName)
  RVA=0x0AC75334  token=0x600054C  System.Type[] GetAllTypes()
  RVA=0x0AC75584  token=0x600054D  System.Type[] GetImplementationsOf(System.Type type)
  RVA=0x0AC7662C  token=0x600054E  System.Type[] RTGetExportedTypes(System.Reflection.Assembly asm)
  RVA=0x0AC75244  token=0x600054F  System.String FriendlyName(System.Type type)
  RVA=0x0AC76AB4  token=0x6000550  System.Boolean RTIsStatic(System.Reflection.PropertyInfo propertyInfo)
  RVA=0x0A989920  token=0x6000551  System.Boolean RTIsAbstract(System.Type type)
  RVA=0x0A9899E8  token=0x6000552  System.Boolean RTIsSubclassOf(System.Type type, System.Type other)
  RVA=0x0AC76A90  token=0x6000553  System.Boolean RTIsAssignableFrom(System.Type type, System.Type second)
  RVA=0x0AC768FC  token=0x6000554  System.Reflection.FieldInfo RTGetField(System.Type type, System.String name)
  RVA=0x0AC76A6C  token=0x6000555  System.Reflection.PropertyInfo RTGetProperty(System.Type type, System.String name)
  RVA=0x0AC76A24  token=0x6000556  System.Reflection.MethodInfo RTGetMethod(System.Type type, System.String name)
  RVA=0x0AC76A00  token=0x6000557  System.Reflection.FieldInfo[] RTGetFields(System.Type type)
  RVA=0x0AC76A48  token=0x6000558  System.Reflection.PropertyInfo[] RTGetProperties(System.Type type)
  RVA=0x0A989524  token=0x6000559  System.Reflection.MethodInfo RTGetGetMethod(System.Reflection.PropertyInfo prop)
  RVA=0x0A989904  token=0x600055A  System.Reflection.MethodInfo RTGetSetMethod(System.Reflection.PropertyInfo prop)
  RVA=0x0AC76B64  token=0x600055B  System.Type RTReflectedType(System.Type type)
  RVA=0x0AC76B64  token=0x600055C  System.Type RTReflectedType(System.Reflection.MemberInfo member)
  RVA=-1  // generic def  token=0x600055D  T RTGetAttribute(System.Type type, System.Boolean inherited)
  RVA=-1  // generic def  token=0x600055E  T RTGetAttribute(System.Reflection.MemberInfo member, System.Boolean inherited)
  RVA=-1  // generic def  token=0x600055F  System.Boolean RTIsDefined(System.Reflection.MemberInfo member, System.Boolean inherited)
  RVA=-1  // generic def  token=0x6000560  T RTCreateDelegate(System.Reflection.MethodInfo method, System.Object instance)
  RVA=-1  // generic def  token=0x6000561  System.Action<T,TValue> GetFieldOrPropSetter(System.Reflection.MemberInfo info)
  RVA=0x0AC76920  token=0x6000562  System.Reflection.MemberInfo[] RTGetFieldsAndProps(System.Type type)
  RVA=0x0AC7686C  token=0x6000563  System.Reflection.MemberInfo RTGetFieldOrProp(System.Type type, System.String name)
  RVA=0x0AC76710  token=0x6000564  System.Object RTGetFieldOrPropValue(System.Reflection.MemberInfo member, System.Object instance, System.Int32 index)
  RVA=0x0AC76B84  token=0x6000565  System.Void RTSetFieldOrPropValue(System.Reflection.MemberInfo member, System.Object instance, System.Object value, System.Int32 index)
  RVA=0x0AC7664C  token=0x6000566  System.Type RTGetFieldOrPropType(System.Reflection.MemberInfo member)
  RVA=0x0AC76110  token=0x6000567  System.Reflection.MemberInfo GetRelativeMember(System.Object root, System.String path)
  RVA=0x0AC75FBC  token=0x6000568  System.Reflection.MemberInfo GetRelativeMember(System.Type type, System.String path)
  RVA=0x0AC75E78  token=0x6000569  System.Object GetRelativeMemberParent(System.Object root, System.String path)
  RVA=-1  // generic def  token=0x600056A  System.String GetMemberPath(System.Linq.Expressions.Expression<System.Func<T,TResult>> func)
  RVA=0x0AC75A34  token=0x600056B  System.String[] GetMemberPaths(System.Type type, System.Predicate<System.Type> shouldInclude, System.Predicate<System.Type> shouldContinue, System.String currentPath, System.Collections.Generic.List<System.Type> recursionCheck)
  RVA=0x0AC76CE8  token=0x600056C  System.Void .cctor()
END_CLASS

CLASS: Slate.StringUtility
TYPE:  static class
TOKEN: 0x20000A2
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AC787A8  token=0x6000571  System.String SplitCamelCase(System.String s)
  RVA=0x0AC786CC  token=0x6000572  System.String AbsToRelativePath(System.String absolutepath)
END_CLASS

CLASS: Slate.TransformRefPosition
TYPE:  sealed struct
TOKEN: 0x20000A3
SIZE:  0x30
IMPLEMENTS: Slate.ITransformRefParameter
FIELDS:
  private           Slate.CutsceneGroup             _group  // 0x10
  private           UnityEngine.Transform           _transform  // 0x18
  private           UnityEngine.Vector3             _vector  // 0x20
  private           Slate.TransformSpace            _space  // 0x2c
PROPERTIES:
  useAnimation  get=0x0AC78D84
  group  get=0x02B76770  set=0x05392320
  transform  get=0x0AC78CCC  set=0x067D5E9C
  value  get=0x0AC78E30  set=0x04D8C970
  space  get=0x0AC78CA8  set=0x04D86320
METHODS:
  RVA=0x0AC78C20  token=0x600057C  System.String ToString()
END_CLASS

CLASS: Slate.TransformRefPositionRotation
TYPE:  sealed struct
TOKEN: 0x20000A4
SIZE:  0x40
IMPLEMENTS: Slate.ITransformRefParameter
FIELDS:
  private           Slate.CutsceneGroup             _group  // 0x10
  private           UnityEngine.Transform           _transform  // 0x18
  private           UnityEngine.Vector3             _position  // 0x20
  private           UnityEngine.Vector3             _rotation  // 0x2c
  private           Slate.TransformSpace            _space  // 0x38
PROPERTIES:
  useAnimation  get=0x0AC78B74
  group  get=0x02B76770  set=0x05392320
  transform  get=0x0AC78ABC  set=0x067D5E9C
  position  get=0x0AC789C0  set=0x04D8C970
  rotation  get=0x0AC78A2C  set=0x04D90500
  space  get=0x0AC78A98  set=0x011F2AA0
METHODS:
  RVA=0x0AC78908  token=0x6000588  System.String ToString()
END_CLASS

CLASS: Slate.TransformSnapshot
TYPE:  class
TOKEN: 0x20000A5
SIZE:  0x18
FIELDS:
  private           System.Collections.Generic.List<Slate.TransformSnapshot.TransformData>data  // 0x10
METHODS:
  RVA=0x0AC793B0  token=0x6000589  System.Void .ctor(UnityEngine.GameObject root, Slate.TransformSnapshot.StoreMode mode)
  RVA=0x0AC7900C  token=0x600058A  System.Void Store(UnityEngine.GameObject root, Slate.TransformSnapshot.StoreMode mode)
  RVA=0x0AC78E9C  token=0x600058B  System.Void Restore()
END_CLASS

CLASS: Slate.UnityObjectUtility
TYPE:  static class
TOKEN: 0x20000A8
SIZE:  0x10
FIELDS:
  private   static  System.Reflection.MethodInfo    _getLocalEulerAngles  // static @ 0x0
  private   static  System.Reflection.MethodInfo    _setLocalEulerAngles  // static @ 0x8
  private   static  System.Reflection.PropertyInfo  _rotationOrder  // static @ 0x10
  private   static  System.Object[]                 _rotationOrderArgGet  // static @ 0x18
  private   static  System.Object[]                 _rotationOrderArgSet  // static @ 0x20
METHODS:
  RVA=0x0AC7AAAC  token=0x600058D  System.Void .cctor()
  RVA=0x0AC7A4D0  token=0x600058E  UnityEngine.Vector3 GetLocalEulerAngles(UnityEngine.Transform transform)
  RVA=0x0AC7A94C  token=0x600058F  System.Void SetLocalEulerAngles(UnityEngine.Transform transform, UnityEngine.Vector3 value)
  RVA=0x0AC79F84  token=0x6000590  System.String CalculateTransformPath(UnityEngine.Transform root, UnityEngine.Transform child)
  RVA=0x0AC7A76C  token=0x6000591  UnityEngine.Transform ResolveTransformPath(UnityEngine.Transform root, System.String path)
  RVA=0x0AC7A138  token=0x6000592  UnityEngine.Transform FindInChildren(UnityEngine.Transform root, System.String name, System.Boolean includeHidden)
  RVA=0x0AC7A6C8  token=0x6000593  System.Void ResetLocalCoords(UnityEngine.Transform transform, System.Boolean includeScale)
  RVA=0x0AC7A890  token=0x6000594  System.Void SetLocalCoordsFrom(UnityEngine.Transform transform, UnityEngine.Transform source)
  RVA=0x0AC7A5F0  token=0x6000595  UnityEngine.Transform InsertParentInChain(UnityEngine.Transform transform, System.String name)
  RVA=-1  // generic def  token=0x6000596  T GetAddComponent(UnityEngine.GameObject go)
  RVA=-1  // generic def  token=0x6000597  T GetAddComponent(UnityEngine.Component comp)
  RVA=0x0AC7A274  token=0x6000598  UnityEngine.Component GetAddComponent(UnityEngine.GameObject go, System.Type type)
  RVA=0x0AC7A3C8  token=0x6000599  System.Collections.Generic.List<System.String> GetBlendShapeNames(UnityEngine.SkinnedMeshRenderer skinnedMesh)
  RVA=0x0AC7A304  token=0x600059A  System.Int32 GetBlendShapeIndex(UnityEngine.SkinnedMeshRenderer skinnedMesh, System.String shapeName)
END_CLASS

CLASS: Slate.VideoSampler
TYPE:  static class
TOKEN: 0x20000AA
SIZE:  0x10
FIELDS:
  private   static  System.String                   ROOT_NAME  // const
  private   static  UnityEngine.GameObject          root  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<System.Object,UnityEngine.Video.VideoPlayer>sources  // static @ 0x8
METHODS:
  RVA=0x0AC7AC74  token=0x600059D  UnityEngine.Video.VideoPlayer GetSourceForID(System.Object keyID)
  RVA=0x0AC7AFBC  token=0x600059E  System.Void ReleaseSourceForID(System.Object keyID)
  RVA=0x0AC7B170  token=0x600059F  System.Void SampleForID(System.Object keyID, UnityEngine.Video.VideoClip clip, System.Single time, System.Single previousTime, Slate.VideoSampler.SampleSettings settings)
  RVA=0x0AC7B214  token=0x60005A0  System.Void Sample(UnityEngine.Video.VideoPlayer source, UnityEngine.Video.VideoClip clip, System.Single time, System.Single previousTime, Slate.VideoSampler.SampleSettings settings)
  RVA=0x0AC7B414  token=0x60005A1  System.Void .cctor()
END_CLASS

CLASS: Slate.CutsceneSequencePlayer
TYPE:  class
TOKEN: 0x20000AD
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Boolean                  playOnStart  // 0x18
  public            System.Collections.Generic.List<Slate.Cutscene>cutscenes  // 0x20
  public            UnityEngine.Events.UnityEvent   onFinish  // 0x28
  private           System.Int32                    currentIndex  // 0x30
  private           System.Boolean                  isPlaying  // 0x34
METHODS:
  RVA=0x0AC6E45C  token=0x60005A3  System.Void Start()
  RVA=0x0AC6E388  token=0x60005A4  System.Void Play()
  RVA=0x0AC6E46C  token=0x60005A5  System.Void Stop()
  RVA=0x0AC6E224  token=0x60005A6  System.Void MoveNext()
  RVA=0x0AC6E1A4  token=0x60005A7  UnityEngine.GameObject Create()
  RVA=0x05394740  token=0x60005A8  System.Void .ctor()
  RVA=0x0AC6E4D4  token=0x60005A9  System.Void <MoveNext>b__8_0()
END_CLASS

CLASS: Slate.DirectorGUITemplate
TYPE:  class
TOKEN: 0x20000AE
SIZE:  0x98
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Boolean                  dontDestroyOnLoad  // 0x18
  public            UnityEngine.UI.CanvasScaler     absScaler  // 0x20
  public            UnityEngine.UI.CanvasScaler     refScaler  // 0x28
  public            UnityEngine.CanvasGroup         cameraDissolveGroup  // 0x30
  public            UnityEngine.UI.RawImage         dissolverImage  // 0x38
  public            UnityEngine.CanvasGroup         letterboxGroup  // 0x40
  public            UnityEngine.UI.RawImage         letterboxTop  // 0x48
  public            UnityEngine.UI.RawImage         letterboxBottom  // 0x50
  public            UnityEngine.CanvasGroup         screenFadeGroup  // 0x58
  public            UnityEngine.UI.RawImage         screenFadeImage  // 0x60
  public            UnityEngine.CanvasGroup         overlayImageGroup  // 0x68
  public            UnityEngine.UI.RawImage         overlayImage  // 0x70
  public            UnityEngine.CanvasGroup         overlayTextGroup  // 0x78
  public            UnityEngine.UI.Text             overlayText  // 0x80
  public            UnityEngine.CanvasGroup         subtitlesGroup  // 0x88
  public            UnityEngine.UI.Text             subtitlesText  // 0x90
  private   static  Slate.DirectorGUITemplate       _current  // static @ 0x0
METHODS:
  RVA=0x0AC6F068  token=0x60005AA  System.Void Awake()
  RVA=0x0AC6FBF8  token=0x60005AB  System.Void ShowAll()
  RVA=0x0AC6F188  token=0x60005AC  System.Void HideAll()
  RVA=0x0AC6F51C  token=0x60005AD  System.Void OnEnable()
  RVA=0x0AC6F300  token=0x60005AE  System.Void OnDisable()
  RVA=0x0AC6FCA0  token=0x60005AF  System.Void Update()
  RVA=0x0AC6F218  token=0x60005B0  System.Void OnCameraDissolve(UnityEngine.Texture texture, System.Single completion)
  RVA=0x0AC6F738  token=0x60005B1  System.Void OnImageOverlayGUI(UnityEngine.Texture texture, UnityEngine.Color color, UnityEngine.Vector2 scale, UnityEngine.Vector2 position)
  RVA=0x0AC6F8A0  token=0x60005B2  System.Void OnLetterboxGUI(System.Single completion)
  RVA=0x0AC6FA30  token=0x60005B3  System.Void OnScreenFadeGUI(UnityEngine.Color color)
  RVA=0x0AC6FB0C  token=0x60005B4  System.Void OnTextOverlayGUI(System.String text, UnityEngine.Color color, System.Single size, UnityEngine.TextAnchor anchor, UnityEngine.Vector2 position)
  RVA=0x0AC6FA88  token=0x60005B5  System.Void OnSubtitlesGUI(System.String text, UnityEngine.Color color)
  RVA=0x05394740  token=0x60005B6  System.Void .ctor()
END_CLASS

CLASS: Slate.PlayCutsceneOnClick
TYPE:  class
TOKEN: 0x20000AF
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Slate.Cutscene                  cutscene  // 0x18
  public            System.Single                   startTime  // 0x20
  public            UnityEngine.Events.UnityEvent   onFinish  // 0x28
METHODS:
  RVA=0x0AC742C4  token=0x60005B7  System.Void OnMouseDown()
  RVA=0x0AC743D8  token=0x60005B8  System.Void Reset()
  RVA=0x0AC74254  token=0x60005B9  UnityEngine.GameObject Create()
  RVA=0x05393520  token=0x60005BA  System.Void .ctor()
  RVA=0x0AC74478  token=0x60005BB  System.Void <OnMouseDown>b__3_0()
END_CLASS

CLASS: Slate.PlayCutsceneOnStart
TYPE:  class
TOKEN: 0x20000B0
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Slate.Cutscene                  cutscene  // 0x18
  public            System.Single                   startTime  // 0x20
  public            UnityEngine.Events.UnityEvent   onFinish  // 0x28
METHODS:
  RVA=0x0AC74524  token=0x60005BC  System.Void Start()
  RVA=0x0AC74498  token=0x60005BD  UnityEngine.GameObject Create()
  RVA=0x05393520  token=0x60005BE  System.Void .ctor()
  RVA=0x0AC74478  token=0x60005BF  System.Void <Start>b__3_0()
END_CLASS

CLASS: Slate.PlayCutsceneOnTrigger
TYPE:  class
TOKEN: 0x20000B1
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Slate.Cutscene                  cutscene  // 0x18
  public            System.Single                   startTime  // 0x20
  public            System.Boolean                  checkSpecificTagOnly  // 0x24
  public            System.String                   tagName  // 0x28
  public            System.Boolean                  once  // 0x30
  public            UnityEngine.Events.UnityEvent   onFinish  // 0x38
METHODS:
  RVA=0x0AC746C4  token=0x60005C0  System.Void OnTriggerEnter(UnityEngine.Collider other)
  RVA=0x0AC7483C  token=0x60005C1  System.Void Reset()
  RVA=0x0AC74638  token=0x60005C2  UnityEngine.GameObject Create()
  RVA=0x0AC7492C  token=0x60005C3  System.Void .ctor()
  RVA=0x0AC748B4  token=0x60005C4  System.Void <OnTriggerEnter>b__6_0()
END_CLASS

CLASS: Slate.StopCutsceneOnClick
TYPE:  class
TOKEN: 0x20000B2
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Slate.Cutscene                  cutscene  // 0x18
  public            Slate.Cutscene.StopMode         stopMode  // 0x20
METHODS:
  RVA=0x0AC783E0  token=0x60005C5  System.Void OnMouseDown()
  RVA=0x0AC78494  token=0x60005C6  System.Void Reset()
  RVA=0x05393520  token=0x60005C7  System.Void .ctor()
END_CLASS

CLASS: Slate.StopCutsceneOnTrigger
TYPE:  class
TOKEN: 0x20000B3
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Slate.Cutscene                  cutscene  // 0x18
  public            System.Boolean                  checkSpecificTagOnly  // 0x20
  public            System.String                   tagName  // 0x28
  public            Slate.Cutscene.StopMode         stopMode  // 0x30
METHODS:
  RVA=0x0AC784F4  token=0x60005C8  System.Void OnTriggerEnter(UnityEngine.Collider other)
  RVA=0x0AC78608  token=0x60005C9  System.Void Reset()
  RVA=0x0AC78680  token=0x60005CA  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.ActorActionClip
TYPE:  abstract class
TOKEN: 0x20000B4
SIZE:  0x40
EXTENDS: Slate.ActionClip
FIELDS:
METHODS:
  RVA=0x05393520  token=0x60005CB  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.ActorActionClip`1
TYPE:  abstract class
TOKEN: 0x20000B5
EXTENDS: Slate.ActionClip
FIELDS:
  private           T                               _actorComponent  // 0x0
PROPERTIES:
  actor  get=-1  // not resolved
  isValid  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60005CE  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateProperties
TYPE:  class
TOKEN: 0x20000B6
SIZE:  0x60
EXTENDS: Slate.ActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  protected         System.String                   _name  // 0x50
  public            Slate.EaseType                  interpolation  // 0x58
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
  isValid  get=0x0AC509C8
  info  get=0x0AC68FD8
  animatedParametersTarget  get=0x0AC68FD0
  animatedParametersInterpolation  get=0x04D86570
  useWeightInParameters  get=0x02FFF600
METHODS:
  RVA=0x0AC68FAC  token=0x60005DA  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateBlendShape
TYPE:  class
TOKEN: 0x20000B7
SIZE:  0x78
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  private           System.String                   _skinName  // 0x50
  private           System.String                   _shapeName  // 0x58
  public            System.Single                   weight  // 0x60
  private           System.Single                   originalWeight  // 0x64
  private           System.Int32                    index  // 0x68
  private           UnityEngine.SkinnedMeshRenderer _skinnedMesh  // 0x70
PROPERTIES:
  info  get=0x0AC68148
  isValid  get=0x0AC68188
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
  canCrossBlend  get=0x02FFF600
  skinName  get=0x04D86270  set=0x0AC68388
  shapeName  get=0x02E56440  set=0x036E5590
  skinnedMesh  get=0x0AC68230
METHODS:
  RVA=0x0AC67F34  token=0x60005E9  System.Void OnEnter()
  RVA=0x0AC68054  token=0x60005EA  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC68018  token=0x60005EB  System.Void OnReverse()
  RVA=0x0AC68114  token=0x60005EC  System.Void .ctor()
  RVA=0x0AC680DC  token=0x60005ED  System.Boolean <get_skinnedMesh>b__31_0(UnityEngine.SkinnedMeshRenderer s)
END_CLASS

CLASS: Slate.ActionClips.CharacterExpression
TYPE:  class
TOKEN: 0x20000B8
SIZE:  0x78
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  private           System.Single                   _length  // 0x48
  private           System.Single                   _blendIn  // 0x4c
  private           System.Single                   _blendOut  // 0x50
  public            System.String                   expressionName  // 0x58
  public            System.String                   expressionUID  // 0x60
  public            System.Single                   weight  // 0x68
  private           System.Single                   originalWeight  // 0x6c
  private           Slate.BlendShapeGroup           expression  // 0x70
PROPERTIES:
  info  get=0x0AC6B0A4
  isValid  get=0x0AC6B158
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x04D87860  set=0x04D91480
  blendOut  get=0x04D885A0  set=0x04D88FF0
  canCrossBlend  get=0x02FFF600
METHODS:
  RVA=0x0AC6AFB4  token=0x60005F7  Slate.BlendShapeGroup ResolveExpression()
  RVA=0x0AC6AECC  token=0x60005F8  System.Void OnEnter()
  RVA=0x0AC6AF44  token=0x60005F9  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC6AF00  token=0x60005FA  System.Void OnReverse()
  RVA=0x0AC6B040  token=0x60005FB  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.CharacterLookAt
TYPE:  class
TOKEN: 0x20000B9
SIZE:  0xA0
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  private           System.Single                   _length  // 0x48
  private           System.Single                   _blendIn  // 0x4c
  private           System.Single                   _blendOut  // 0x50
  public            Slate.EaseType                  interpolation  // 0x54
  public            System.Single                   weight  // 0x58
  public            Slate.TransformRefPosition      targetPosition  // 0x60
  private           UnityEngine.Quaternion          wasNeckRot  // 0x80
  private           UnityEngine.Quaternion          wasHeadRot  // 0x90
PROPERTIES:
  targetPositionVector  get=0x0AC6B9F0  set=0x04DA9C90
  info  get=0x0AC6B898
  isValid  get=0x0AC6B910
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x04D87860  set=0x04D91480
  blendOut  get=0x04D885A0  set=0x04D88FF0
METHODS:
  RVA=0x0AC6B208  token=0x6000606  System.Void OnCreate()
  RVA=0x0AC6B1BC  token=0x6000607  System.Void OnAfterValidate()
  RVA=0x0AC6B2D0  token=0x6000608  System.Void OnEnter()
  RVA=0x0AC6B410  token=0x6000609  System.Void OnUpdate(System.Single time)
  RVA=0x0AC6B370  token=0x600060A  System.Void OnReverse()
  RVA=0x0AC6B840  token=0x600060B  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendMessage`1
TYPE:  abstract class
TOKEN: 0x20000BA
EXTENDS: Slate.ActionClips.SendMessage
FIELDS:
  public            T                               value  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
  isValid  get=-1  // not resolved
  parameterType  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600060F  System.Void OnEnter()
  RVA=-1  // not resolved  token=0x6000610  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendMessage
TYPE:  class
TOKEN: 0x20000BB
SIZE:  0x50
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  public            System.String                   message  // 0x40
  public            System.Int32                    componentIndex  // 0x48
PROPERTIES:
  info  get=0x0AC774A8
  isValid  get=0x091DE054
  parameterType  get=0x011EC580
METHODS:
  RVA=0x0AC771D8  token=0x6000614  System.Void OnEnter()
  RVA=0x0AC7748C  token=0x6000615  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendMessageBoolean
TYPE:  class
TOKEN: 0x20000BC
SIZE:  0x58
EXTENDS: Slate.ActionClips.SendMessage`1
FIELDS:
METHODS:
  RVA=0x0AC770D4  token=0x6000616  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendMessageFloat
TYPE:  class
TOKEN: 0x20000BD
SIZE:  0x58
EXTENDS: Slate.ActionClips.SendMessage`1
FIELDS:
METHODS:
  RVA=0x0AC77108  token=0x6000617  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendMessageInteger
TYPE:  class
TOKEN: 0x20000BE
SIZE:  0x58
EXTENDS: Slate.ActionClips.SendMessage`1
FIELDS:
METHODS:
  RVA=0x0AC7713C  token=0x6000618  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendMessageObject
TYPE:  class
TOKEN: 0x20000BF
SIZE:  0x58
EXTENDS: Slate.ActionClips.SendMessage`1
FIELDS:
METHODS:
  RVA=0x0AC77170  token=0x6000619  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendMessageString
TYPE:  class
TOKEN: 0x20000C0
SIZE:  0x58
EXTENDS: Slate.ActionClips.SendMessage`1
FIELDS:
METHODS:
  RVA=0x0AC771A4  token=0x600061A  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateActorVisibility
TYPE:  class
TOKEN: 0x20000C1
SIZE:  0x48
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            System.Boolean                  visible  // 0x44
  private           System.Boolean                  wasVisible  // 0x45
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
METHODS:
  RVA=0x0AC67E58  token=0x600061D  System.Void OnCreate()
  RVA=0x0AC67E88  token=0x600061E  System.Void OnEnter()
  RVA=0x0AC67EE8  token=0x600061F  System.Void OnUpdate(System.Single time)
  RVA=0x0AC67EB8  token=0x6000620  System.Void OnReverse()
  RVA=0x0AC67F18  token=0x6000621  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.MakeCameraTarget
TYPE:  class
TOKEN: 0x20000C2
SIZE:  0x58
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  public            System.Boolean                  setTransposerTarget  // 0x40
  public            System.Boolean                  setComposerTarget  // 0x41
  private           UnityEngine.Transform           wasTransposerTarget  // 0x48
  private           UnityEngine.Transform           wasComposerTarget  // 0x50
METHODS:
  RVA=0x0AC72094  token=0x6000622  System.Void OnEnter()
  RVA=0x0AC72220  token=0x6000623  System.Void OnReverse()
  RVA=0x0AC72340  token=0x6000624  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SetActorActiveState
TYPE:  class
TOKEN: 0x20000C3
SIZE:  0x50
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            Slate.ActiveState               activeState  // 0x44
  private           System.Boolean                  lastState  // 0x48
  private           System.Boolean                  currentState  // 0x49
  private           System.Boolean                  temporary  // 0x4a
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  info  get=0x0AC77674
METHODS:
  RVA=0x0AC774E8  token=0x6000628  System.Void OnEnter()
  RVA=0x0AC775B0  token=0x6000629  System.Void OnExit()
  RVA=0x0AC775EC  token=0x600062A  System.Void OnReverseEnter()
  RVA=0x0AC77628  token=0x600062B  System.Void OnReverse()
  RVA=0x0AC77658  token=0x600062C  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SetActorVisibilityTemporary
TYPE:  class
TOKEN: 0x20000C4
SIZE:  0x50
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            Slate.ActiveState               activeState  // 0x44
  private           System.Boolean                  lastState  // 0x48
  private           System.Boolean                  currentState  // 0x49
  private           System.Boolean                  temporary  // 0x4a
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  info  get=0x0AC776F8
METHODS:
  RVA=0x0AC774E8  token=0x6000630  System.Void OnEnter()
  RVA=0x0AC775B0  token=0x6000631  System.Void OnExit()
  RVA=0x0AC775EC  token=0x6000632  System.Void OnReverseEnter()
  RVA=0x0AC77628  token=0x6000633  System.Void OnReverse()
  RVA=0x0AC776D4  token=0x6000634  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SetBehavioursActiveState
TYPE:  class
TOKEN: 0x20000C5
SIZE:  0x70
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            System.Collections.Generic.List<System.String>behaviourNames  // 0x48
  public            Slate.ActiveState               activeState  // 0x50
  private           System.Collections.Generic.Dictionary<UnityEngine.Behaviour,System.Boolean>originalStates  // 0x58
  private           System.Collections.Generic.Dictionary<UnityEngine.Behaviour,System.Boolean>currentStates  // 0x60
  private           System.Boolean                  temporary  // 0x68
PROPERTIES:
  info  get=0x0AC78064
  length  get=0x04D863F0  set=0x04D86470
METHODS:
  RVA=0x0AC77758  token=0x6000638  System.Void OnEnter()
  RVA=0x0AC77A78  token=0x6000639  System.Void OnExit()
  RVA=0x0AC77C2C  token=0x600063A  System.Void OnReverseEnter()
  RVA=0x0AC77DDC  token=0x600063B  System.Void OnReverse()
  RVA=0x0AC77FDC  token=0x600063C  System.Void .ctor()
  RVA=0x0AC77F64  token=0x600063D  System.Boolean <OnEnter>b__11_0(UnityEngine.Behaviour c)
END_CLASS

CLASS: Slate.ActionClips.AnimateOnPath
TYPE:  class
TOKEN: 0x20000C6
SIZE:  0x80
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  public            Slate.Path                      path  // 0x48
  public            System.Single                   positionOnPath  // 0x50
  public            UnityEngine.Vector3             lookAtTargetPosition  // 0x54
  public            Slate.EaseType                  blendInterpolation  // 0x60
  private           UnityEngine.Vector3             wasPosition  // 0x64
  private           UnityEngine.Quaternion          wasRotation  // 0x70
PROPERTIES:
  info  get=0x0AC68EBC
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  isValid  get=0x0AC68F5C
METHODS:
  RVA=0x0AC68A70  token=0x6000644  System.Void OnEnter()
  RVA=0x0AC68B9C  token=0x6000645  System.Void OnUpdate(System.Single time)
  RVA=0x0AC68B10  token=0x6000646  System.Void OnReverse()
  RVA=0x0AC68E98  token=0x6000647  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.FollowPath
TYPE:  class
TOKEN: 0x20000C7
SIZE:  0x90
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            Slate.Path                      path  // 0x50
  public            System.Boolean                  useSpeed  // 0x58
  public            System.Single                   speed  // 0x5c
  public            System.Single                   lookAhead  // 0x60
  public            UnityEngine.Vector3             upVector  // 0x64
  public            Slate.EaseType                  blendInterpolation  // 0x70
  private           UnityEngine.Vector3             lastPos  // 0x74
  private           UnityEngine.Quaternion          lastRot  // 0x80
PROPERTIES:
  info  get=0x0AC70D78
  length  get=0x0AC70E68  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
  isValid  get=0x0AC70E18
METHODS:
  RVA=0x0AC708B0  token=0x6000650  System.Void OnEnter()
  RVA=0x0AC709E4  token=0x6000651  System.Void OnUpdate(System.Single time)
  RVA=0x0AC70954  token=0x6000652  System.Void OnReverse()
  RVA=0x0AC70D30  token=0x6000653  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.PathfindFromTo
TYPE:  class
TOKEN: 0x20000C8
SIZE:  0xC8
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _blendIn  // 0x40
  public            System.Single                   speed  // 0x44
  public            Slate.TransformRefPosition      sourcePosition  // 0x48
  public            Slate.TransformRefPosition      targetPosition  // 0x68
  private           UnityEngine.Vector3[]           pathPoints  // 0x88
  private           UnityEngine.Vector3             wasPosition  // 0x90
  private           UnityEngine.Quaternion          wasRotation  // 0x9c
  private           UnityEngine.Vector3             lastFrom  // 0xac
  private           UnityEngine.Vector3             lastTo  // 0xb8
PROPERTIES:
  info  get=0x0AC73828
  length  get=0x0AC73870
  blendIn  get=0x0AC737F8  set=0x04D86470
METHODS:
  RVA=0x0AC72EDC  token=0x6000658  System.Void OnEnter()
  RVA=0x0AC7313C  token=0x6000659  System.Void OnUpdate(System.Single time)
  RVA=0x0AC73090  token=0x600065A  System.Void OnReverse()
  RVA=0x0AC73578  token=0x600065B  System.Void TryCalculatePath()
  RVA=0x0AC737D4  token=0x600065C  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.PathfindTo
TYPE:  class
TOKEN: 0x20000C9
SIZE:  0xA8
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _blendIn  // 0x40
  public            System.Single                   speed  // 0x44
  public            Slate.TransformRefPosition      targetPosition  // 0x48
  private           UnityEngine.Vector3[]           pathPoints  // 0x68
  private           UnityEngine.Vector3             wasPosition  // 0x70
  private           UnityEngine.Quaternion          wasRotation  // 0x7c
  private           UnityEngine.Vector3             lastFrom  // 0x8c
  private           UnityEngine.Vector3             lastTo  // 0x98
PROPERTIES:
  info  get=0x0AC741CC
  length  get=0x0AC74214
  blendIn  get=0x0AC737F8  set=0x04D86470
METHODS:
  RVA=0x0AC738B4  token=0x6000661  System.Void OnEnter()
  RVA=0x0AC73AEC  token=0x6000662  System.Void OnUpdate(System.Single time)
  RVA=0x0AC73A54  token=0x6000663  System.Void OnReverse()
  RVA=0x0AC73F10  token=0x6000664  System.Void TryCalculatePath()
  RVA=0x0AC737D4  token=0x6000665  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateMaterialColor
TYPE:  class
TOKEN: 0x20000CA
SIZE:  0x88
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  private           System.Single                   _length  // 0x48
  private           System.Single                   _blendIn  // 0x4c
  private           System.Single                   _blendOut  // 0x50
  public            System.String                   propertyName  // 0x58
  public            UnityEngine.Color               color  // 0x60
  public            Slate.EaseType                  interpolation  // 0x70
  private           UnityEngine.Color               originalColor  // 0x74
PROPERTIES:
  info  get=0x0AC68568
  isValid  get=0x0AC685A8
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x04D87860  set=0x04D91480
  blendOut  get=0x04D885A0  set=0x04D88FF0
  targetMaterial  get=0x0AC68694
METHODS:
  RVA=0x0AC68430  token=0x600066F  System.Void OnEnter()
  RVA=0x0AC68430  token=0x6000670  System.Void OnReverseEnter()
  RVA=0x0AC68440  token=0x6000671  System.Void OnUpdate(System.Single time)
  RVA=0x0AC68438  token=0x6000672  System.Void OnReverse()
  RVA=0x0AC68438  token=0x6000673  System.Void OnExit()
  RVA=0x0AC683F4  token=0x6000674  System.Void DoSet()
  RVA=0x0AC683B4  token=0x6000675  System.Void DoReset()
  RVA=0x0AC684EC  token=0x6000676  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateMaterialFloat
TYPE:  class
TOKEN: 0x20000CB
SIZE:  0x70
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  private           System.Single                   _length  // 0x48
  private           System.Single                   _blendIn  // 0x4c
  private           System.Single                   _blendOut  // 0x50
  public            System.String                   propertyName  // 0x58
  public            System.Single                   value  // 0x60
  public            Slate.EaseType                  interpolation  // 0x64
  private           System.Single                   originalValue  // 0x68
PROPERTIES:
  info  get=0x0AC6889C
  isValid  get=0x0AC688DC
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x04D87860  set=0x04D91480
  blendOut  get=0x04D885A0  set=0x04D88FF0
  targetMaterial  get=0x0AC689C8
METHODS:
  RVA=0x0AC687A8  token=0x6000680  System.Void OnEnter()
  RVA=0x0AC687A8  token=0x6000681  System.Void OnReverseEnter()
  RVA=0x0AC687B8  token=0x6000682  System.Void OnUpdate(System.Single time)
  RVA=0x0AC687B0  token=0x6000683  System.Void OnReverse()
  RVA=0x0AC687B0  token=0x6000684  System.Void OnExit()
  RVA=0x0AC68770  token=0x6000685  System.Void DoSet()
  RVA=0x0AC6873C  token=0x6000686  System.Void DoReset()
  RVA=0x0AC68838  token=0x6000687  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateMaterialTexture
TYPE:  class
TOKEN: 0x20000CC
SIZE:  0x88
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  private           System.Single                   _length  // 0x48
  private           System.Single                   _blendIn  // 0x4c
  private           System.Single                   _blendOut  // 0x50
  public            System.String                   propertyName  // 0x58
  public            UnityEngine.Vector2             offset  // 0x60
  public            UnityEngine.Vector2             scale  // 0x68
  public            Slate.EaseType                  interpolation  // 0x70
  private           UnityEngine.Vector2             originalOffset  // 0x74
  private           UnityEngine.Vector2             originalScale  // 0x7c
PROPERTIES:
  info  get=0x0AC7DD38
  isValid  get=0x0AC7DD78
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x04D87860  set=0x04D91480
  blendOut  get=0x04D885A0  set=0x04D88FF0
  targetMaterial  get=0x0AC7DE64
METHODS:
  RVA=0x0AC7DAF8  token=0x6000691  System.Void OnEnter()
  RVA=0x0AC7DAF8  token=0x6000692  System.Void OnReverseEnter()
  RVA=0x0AC7DB08  token=0x6000693  System.Void OnUpdate(System.Single time)
  RVA=0x0AC7DB00  token=0x6000694  System.Void OnReverse()
  RVA=0x0AC7DB00  token=0x6000695  System.Void OnExit()
  RVA=0x0AC7DA70  token=0x6000696  System.Void DoSet()
  RVA=0x0AC7D9FC  token=0x6000697  System.Void DoReset()
  RVA=0x0AC7DCC4  token=0x6000698  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.ScrollMaterialTexture
TYPE:  class
TOKEN: 0x20000CD
SIZE:  0x70
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  private           System.Single                   _length  // 0x48
  public            System.String                   propertyName  // 0x50
  public            UnityEngine.Vector2             speed  // 0x58
  public            Slate.EaseType                  interpolation  // 0x60
  private           UnityEngine.Vector2             originalOffset  // 0x64
PROPERTIES:
  info  get=0x0AC84B3C
  isValid  get=0x0AC84BA0
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x0AC7EA6C
  targetMaterial  get=0x0AC84C4C
METHODS:
  RVA=0x0AC848C8  token=0x600069F  System.Void OnEnter()
  RVA=0x0AC84958  token=0x60006A0  System.Void OnUpdate(System.Single time)
  RVA=0x0AC84914  token=0x60006A1  System.Void OnReverse()
  RVA=0x0AC84AD4  token=0x60006A2  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SetMaterial
TYPE:  class
TOKEN: 0x20000CE
SIZE:  0x68
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  private           System.Single                   _length  // 0x48
  public            UnityEngine.Material            material  // 0x50
  private           UnityEngine.Material            sharedMat  // 0x58
  private           System.Boolean                  temporary  // 0x60
PROPERTIES:
  info  get=0x0AC853A8
  length  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC8530C  token=0x60006A6  System.Void OnEnter()
  RVA=0x0AC85350  token=0x60006A7  System.Void OnReverseEnter()
  RVA=0x0AC85360  token=0x60006A8  System.Void OnReverse()
  RVA=0x0AC85340  token=0x60006A9  System.Void OnExit()
  RVA=0x0AC8528C  token=0x60006AA  System.Void DoSet()
  RVA=0x0AC85238  token=0x60006AB  System.Void DoReset()
  RVA=0x0AC85368  token=0x60006AC  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SetMaterialTexture
TYPE:  class
TOKEN: 0x20000CF
SIZE:  0x70
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  private           System.Single                   _length  // 0x48
  public            System.String                   propertyName  // 0x50
  public            UnityEngine.Texture             texture  // 0x58
  private           UnityEngine.Texture             originalTexture  // 0x60
  private           System.Boolean                  temporary  // 0x68
PROPERTIES:
  info  get=0x0AC85004
  isValid  get=0x0AC850A4
  length  get=0x04D8D350  set=0x04D8D380
  targetMaterial  get=0x0AC85190
METHODS:
  RVA=0x0AC84F58  token=0x60006B2  System.Void OnEnter()
  RVA=0x0AC84F9C  token=0x60006B3  System.Void OnReverseEnter()
  RVA=0x0AC84FAC  token=0x60006B4  System.Void OnReverse()
  RVA=0x0AC84F8C  token=0x60006B5  System.Void OnExit()
  RVA=0x0AC84EF8  token=0x60006B6  System.Void DoSet()
  RVA=0x0AC84EC4  token=0x60006B7  System.Void DoReset()
  RVA=0x0AC84FB4  token=0x60006B8  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SetSortingLayer
TYPE:  class
TOKEN: 0x20000D0
SIZE:  0x60
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  public            System.Int32                    sortingOrder  // 0x48
  public            System.Int32                    sortingLayerID  // 0x4c
  private           System.String                   _lastLayer  // 0x50
  private           System.Int32                    _lastOrder  // 0x58
PROPERTIES:
  info  get=0x0AC85A34
METHODS:
  RVA=0x0AC858B8  token=0x60006BA  System.Void OnEnter()
  RVA=0x0AC8597C  token=0x60006BB  System.Void OnReverse()
  RVA=0x0AC859F4  token=0x60006BC  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SetSprite
TYPE:  class
TOKEN: 0x20000D1
SIZE:  0x98
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  public            UnityEngine.Sprite              sprite  // 0x48
  public            UnityEngine.Color               color  // 0x50
  public            System.Boolean                  flipX  // 0x60
  public            System.Boolean                  flipY  // 0x61
  public            System.Boolean                  changeSorting  // 0x62
  public            System.Int32                    sortingLayerID  // 0x64
  public            System.Int32                    sortingOrder  // 0x68
  private           UnityEngine.Sprite              lastSprite  // 0x70
  private           UnityEngine.Color               lastColor  // 0x78
  private           System.Boolean                  lastFlipX  // 0x88
  private           System.Boolean                  lastFlipY  // 0x89
  private           System.Int32                    lastSortingLayerID  // 0x8c
  private           System.Int32                    lastSortingOrder  // 0x90
PROPERTIES:
  info  get=0x0AC85E8C
METHODS:
  RVA=0x0AC85AEC  token=0x60006BE  System.Void OnEnter()
  RVA=0x0AC85D10  token=0x60006BF  System.Void OnReverse()
  RVA=0x0AC85E34  token=0x60006C0  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SpriteFlipbook
TYPE:  class
TOKEN: 0x20000D2
SIZE:  0x68
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  private           System.Single                   _length  // 0x48
  public            System.Int32                    loops  // 0x4c
  public            System.Collections.Generic.List<UnityEngine.Sprite>sprites  // 0x50
  public            System.Boolean                  endWithPrevious  // 0x58
  private           UnityEngine.Sprite              lastSprite  // 0x60
PROPERTIES:
  info  get=0x0AC86AD8
  length  get=0x04D8D350  set=0x04D8D380
  isValid  get=0x0AC86B98
METHODS:
  RVA=0x0AC867D8  token=0x60006C5  System.Void OnEnter()
  RVA=0x0AC868E4  token=0x60006C6  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC86834  token=0x60006C7  System.Void OnExit()
  RVA=0x0AC86890  token=0x60006C8  System.Void OnReverse()
  RVA=0x0AC86A40  token=0x60006C9  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AttachObject
TYPE:  class
TOKEN: 0x20000D3
SIZE:  0x90
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            UnityEngine.Transform           targetObject  // 0x48
  public            System.String                   childTransformName  // 0x50
  public            UnityEngine.Vector3             localPosition  // 0x58
  public            UnityEngine.Vector3             localRotation  // 0x64
  public            UnityEngine.Vector3             localScale  // 0x70
  private           Slate.TransformSnapshot         snapshot  // 0x80
  private           System.Boolean                  temporary  // 0x88
PROPERTIES:
  isValid  get=0x0AC7E81C
  length  get=0x04D863F0  set=0x04D86470
METHODS:
  RVA=0x0AC7E794  token=0x60006CD  System.Void OnEnter()
  RVA=0x0AC7E7DC  token=0x60006CE  System.Void OnReverseEnter()
  RVA=0x0AC7E7C8  token=0x60006CF  System.Void OnExit()
  RVA=0x0AC7E7F0  token=0x60006D0  System.Void OnReverse()
  RVA=0x0AC7E568  token=0x60006D1  System.Void Do()
  RVA=0x0AC7E7F8  token=0x60006D2  System.Void UnDo()
  RVA=0x0AC7E4E4  token=0x60006D3  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AttachObjectPrefab
TYPE:  class
TOKEN: 0x20000D4
SIZE:  0x98
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            UnityEngine.Transform           targetPrefab  // 0x48
  public            System.String                   childTransformName  // 0x50
  public            UnityEngine.Vector3             localPosition  // 0x58
  public            UnityEngine.Vector3             localRotation  // 0x64
  public            UnityEngine.Vector3             localScale  // 0x70
  private           UnityEngine.Transform           instance  // 0x80
  private           Slate.TransformSnapshot         snapshot  // 0x88
  private           System.Boolean                  temporary  // 0x90
PROPERTIES:
  isValid  get=0x0AC7E518
  length  get=0x04D863F0  set=0x04D86470
METHODS:
  RVA=0x0AC7E3E0  token=0x60006D7  System.Void OnEnter()
  RVA=0x0AC7E428  token=0x60006D8  System.Void OnReverseEnter()
  RVA=0x0AC7E414  token=0x60006D9  System.Void OnExit()
  RVA=0x0AC7E43C  token=0x60006DA  System.Void OnReverse()
  RVA=0x0AC7E1D4  token=0x60006DB  System.Void Do()
  RVA=0x0AC7E444  token=0x60006DC  System.Void UnDo()
  RVA=0x0AC7E4E4  token=0x60006DD  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.LookAt
TYPE:  class
TOKEN: 0x20000D5
SIZE:  0x88
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            System.Boolean                  verticalOnly  // 0x4c
  public            Slate.EaseType                  interpolation  // 0x50
  public            Slate.TransformRefPosition      targetPosition  // 0x58
  private           UnityEngine.Quaternion          wasRotation  // 0x78
PROPERTIES:
  targetPositionVector  get=0x0AC801D0  set=0x04D91E40
  info  get=0x0AC80158
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC7FE38  token=0x60006E7  System.Void OnCreate()
  RVA=0x0AC7FDEC  token=0x60006E8  System.Void OnAfterValidate()
  RVA=0x0AC7FE74  token=0x60006E9  System.Void OnEnter()
  RVA=0x0AC7FF0C  token=0x60006EA  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC7FEC0  token=0x60006EB  System.Void OnReverse()
  RVA=0x0AC80124  token=0x60006EC  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.MatchTransformsToTarget
TYPE:  class
TOKEN: 0x20000D6
SIZE:  0xB8
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            UnityEngine.Transform           targetObject  // 0x50
  public            Slate.EaseType                  interpolation  // 0x58
  public            System.Boolean                  matchPosition  // 0x5c
  public            UnityEngine.Vector3             positionOffset  // 0x60
  public            System.Boolean                  matchRotation  // 0x6c
  public            UnityEngine.Vector3             rotationOffset  // 0x70
  public            System.Boolean                  matchScale  // 0x7c
  public            UnityEngine.Vector3             scaleOffset  // 0x80
  private           UnityEngine.Vector3             lastPos  // 0x8c
  private           UnityEngine.Quaternion          lastRot  // 0x98
  private           UnityEngine.Vector3             lastScale  // 0xa8
PROPERTIES:
  info  get=0x0AC80908
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
  isValid  get=0x0AC809A8
METHODS:
  RVA=0x0AC80204  token=0x60006F5  System.Void OnEnter()
  RVA=0x0AC803C4  token=0x60006F6  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC802E0  token=0x60006F7  System.Void OnReverse()
  RVA=0x0AC808CC  token=0x60006F8  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.Noise
TYPE:  class
TOKEN: 0x20000D7
SIZE:  0x78
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            UnityEngine.Vector3             seed  // 0x4c
  public            UnityEngine.Vector3             amplitude  // 0x58
  public            System.Single                   frequency  // 0x64
  private           UnityEngine.Vector3             wasPosition  // 0x68
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC80D98  token=0x60006FF  System.Void OnCreate()
  RVA=0x0AC80E10  token=0x6000700  System.Void OnEnter()
  RVA=0x0AC80E60  token=0x6000701  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x0AC81038  token=0x6000702  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.RotateAround
TYPE:  class
TOKEN: 0x20000D8
SIZE:  0xA0
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            UnityEngine.Vector3             rotation  // 0x44
  public            System.Boolean                  perSecond  // 0x50
  public            System.Boolean                  lookTarget  // 0x51
  public            Slate.EaseType                  interpolation  // 0x54
  public            Slate.TransformRefPosition      targetPosition  // 0x58
  private           UnityEngine.Vector3             wasPosition  // 0x78
  private           UnityEngine.Quaternion          wasRotation  // 0x84
  private           UnityEngine.Vector3             targetWasPosition  // 0x94
PROPERTIES:
  targetPositionVector  get=0x0AC801D0  set=0x04D91E40
  info  get=0x0AC83588
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x0AC7EA6C
METHODS:
  RVA=0x0AC82F8C  token=0x6000709  System.Void OnAfterValidate()
  RVA=0x0AC82FD8  token=0x600070A  System.Void OnEnter()
  RVA=0x0AC83174  token=0x600070B  System.Void OnUpdate(System.Single time)
  RVA=0x0AC830E4  token=0x600070C  System.Void OnReverse()
  RVA=0x0AC83548  token=0x600070D  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.RotateBy
TYPE:  class
TOKEN: 0x20000D9
SIZE:  0x68
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            UnityEngine.Vector3             rotation  // 0x44
  public            System.Boolean                  perSecond  // 0x50
  public            Slate.EaseType                  interpolation  // 0x54
  private           UnityEngine.Vector3             originalRot  // 0x58
PROPERTIES:
  info  get=0x0AC83964
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x0AC7EA6C
METHODS:
  RVA=0x0AC83670  token=0x6000712  System.Void OnEnter()
  RVA=0x0AC83784  token=0x6000713  System.Void OnUpdate(System.Single time)
  RVA=0x0AC836F4  token=0x6000714  System.Void OnReverse()
  RVA=0x0AC83924  token=0x6000715  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.RotateTo
TYPE:  class
TOKEN: 0x20000DA
SIZE:  0x60
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            UnityEngine.Vector3             targetRotation  // 0x44
  public            Slate.EaseType                  interpolation  // 0x50
  private           UnityEngine.Vector3             originalRot  // 0x54
PROPERTIES:
  info  get=0x0AC83C18
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x0AC7EA6C
METHODS:
  RVA=0x0AC83A14  token=0x600071A  System.Void OnEnter()
  RVA=0x0AC83AB8  token=0x600071B  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC83A64  token=0x600071C  System.Void OnReverse()
  RVA=0x0AC7E034  token=0x600071D  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.ScaleBy
TYPE:  class
TOKEN: 0x20000DB
SIZE:  0x68
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            UnityEngine.Vector3             scale  // 0x44
  public            System.Boolean                  perSecond  // 0x50
  public            Slate.EaseType                  interpolation  // 0x54
  private           UnityEngine.Vector3             originalScale  // 0x58
PROPERTIES:
  info  get=0x0AC844A0
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x0AC7EA6C
METHODS:
  RVA=0x0AC84240  token=0x6000722  System.Void OnEnter()
  RVA=0x0AC842E4  token=0x6000723  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC84290  token=0x6000724  System.Void OnReverse()
  RVA=0x0AC84464  token=0x6000725  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.ScaleTo
TYPE:  class
TOKEN: 0x20000DC
SIZE:  0x60
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            UnityEngine.Vector3             targetScale  // 0x44
  public            Slate.EaseType                  interpolation  // 0x50
  private           UnityEngine.Vector3             originalPos  // 0x54
PROPERTIES:
  info  get=0x0AC84754
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x0AC7EA6C
METHODS:
  RVA=0x0AC84550  token=0x600072A  System.Void OnEnter()
  RVA=0x0AC845F4  token=0x600072B  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC845A0  token=0x600072C  System.Void OnReverse()
  RVA=0x0AC7E034  token=0x600072D  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SetParentTemporary
TYPE:  class
TOKEN: 0x20000DD
SIZE:  0x88
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  public            System.Single                   _length  // 0x40
  public            UnityEngine.Transform           newParent  // 0x48
  public            System.Boolean                  matchPosition  // 0x50
  public            System.Boolean                  matchRotation  // 0x51
  public            System.Boolean                  matchScale  // 0x52
  private           UnityEngine.Transform           originalParent  // 0x58
  private           UnityEngine.Vector3             originalPos  // 0x60
  private           UnityEngine.Quaternion          originalRot  // 0x6c
  private           UnityEngine.Vector3             originalScale  // 0x7c
PROPERTIES:
  info  get=0x0AC85818
  length  get=0x04D863F0  set=0x04D86470
METHODS:
  RVA=0x0AC85448  token=0x6000731  System.Void OnEnter()
  RVA=0x0AC85664  token=0x6000732  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC85554  token=0x6000733  System.Void OnReverse()
  RVA=0x0AC67F18  token=0x6000734  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SetTransformParent
TYPE:  class
TOKEN: 0x20000DE
SIZE:  0x90
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            UnityEngine.Transform           newParent  // 0x48
  public            System.Boolean                  resetPosition  // 0x50
  public            System.Boolean                  resetRotation  // 0x51
  public            System.Boolean                  resetScale  // 0x52
  private           UnityEngine.Transform           originalParent  // 0x58
  private           UnityEngine.Vector3             originalPos  // 0x60
  private           UnityEngine.Quaternion          originalRot  // 0x6c
  private           UnityEngine.Vector3             originalScale  // 0x7c
  private           System.Boolean                  temporary  // 0x88
PROPERTIES:
  info  get=0x0AC861CC
  length  get=0x04D863F0  set=0x04D86470
METHODS:
  RVA=0x0AC86168  token=0x6000738  System.Void OnEnter()
  RVA=0x0AC861B0  token=0x6000739  System.Void OnReverseEnter()
  RVA=0x0AC8619C  token=0x600073A  System.Void OnExit()
  RVA=0x0AC861C4  token=0x600073B  System.Void OnReverse()
  RVA=0x0AC85F0C  token=0x600073C  System.Void Do()
  RVA=0x0AC85554  token=0x600073D  System.Void UnDo()
  RVA=0x05393520  token=0x600073E  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SetTransformValues
TYPE:  class
TOKEN: 0x20000DF
SIZE:  0x80
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  public            Slate.MiniTransformSpace        space  // 0x40
  public            System.Boolean                  setPosition  // 0x44
  public            UnityEngine.Vector3             position  // 0x48
  public            System.Boolean                  setRotation  // 0x54
  public            UnityEngine.Vector3             rotation  // 0x58
  public            System.Boolean                  setScale  // 0x64
  public            UnityEngine.Vector3             scale  // 0x68
  private           Slate.TransformSnapshot         undo  // 0x78
METHODS:
  RVA=0x0AC8626C  token=0x600073F  System.Void OnEnter()
  RVA=0x0AC86438  token=0x6000740  System.Void OnReverse()
  RVA=0x0AC86458  token=0x6000741  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SimpleGrounder
TYPE:  class
TOKEN: 0x20000E0
SIZE:  0xA0
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            System.Single                   maxCheckDistance  // 0x44
  public            System.Single                   offset  // 0x48
  private           UnityEngine.RaycastHit          hit  // 0x50
  private           UnityEngine.Vector3             lastPos  // 0x90
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
METHODS:
  RVA=0x0AC8648C  token=0x6000744  System.Void OnEnter()
  RVA=0x0AC8653C  token=0x6000745  System.Void OnUpdate(System.Single time)
  RVA=0x0AC864E4  token=0x6000746  System.Void OnReverse()
  RVA=0x0AC867AC  token=0x6000747  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.TranslateBy
TYPE:  class
TOKEN: 0x20000E1
SIZE:  0x68
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            UnityEngine.Vector3             translation  // 0x44
  public            System.Boolean                  perSecond  // 0x50
  public            Slate.EaseType                  interpolation  // 0x54
  private           UnityEngine.Vector3             originalPos  // 0x58
PROPERTIES:
  info  get=0x0AC87490
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x0AC7EA6C
METHODS:
  RVA=0x0AC8722C  token=0x600074C  System.Void OnEnter()
  RVA=0x0AC872D0  token=0x600074D  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC8727C  token=0x600074E  System.Void OnReverse()
  RVA=0x0AC87450  token=0x600074F  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.TranslateTo
TYPE:  class
TOKEN: 0x20000E2
SIZE:  0x68
EXTENDS: Slate.ActionClips.ActorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            UnityEngine.Vector3             targetPosition  // 0x44
  public            Slate.MiniTransformSpace        space  // 0x50
  public            Slate.EaseType                  interpolation  // 0x54
  private           UnityEngine.Vector3             wasPosition  // 0x58
PROPERTIES:
  info  get=0x0AC87784
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x0AC7EA6C
METHODS:
  RVA=0x0AC87540  token=0x6000754  System.Void OnEnter()
  RVA=0x0AC875E4  token=0x6000755  System.Void OnUpdate(System.Single time)
  RVA=0x0AC87590  token=0x6000756  System.Void OnReverse()
  RVA=0x0AC87760  token=0x6000757  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AdditiveScene
TYPE:  class
TOKEN: 0x20000E3
SIZE:  0x88
EXTENDS: Slate.DirectorActionClip
IMPLEMENTS: Slate.ISubClipContainable Slate.IDirectable
FIELDS:
  private           System.Single                   _length  // 0x40
  protected         System.String                   _scenePath  // 0x48
  public            UnityEngine.Vector3             scenePosition  // 0x50
  public            UnityEngine.Vector3             sceneRotation  // 0x5c
  public            Slate.MiniTransformSpace        space  // 0x68
  public            System.Boolean                  updateRootCutscenes  // 0x6c
  public            System.Single                   timeOffset  // 0x70
  private           UnityEngine.SceneManagement.ScenesubScene  // 0x74
  private           System.Collections.Generic.List<Slate.Cutscene>rootCutscenes  // 0x78
  private           System.Boolean                  temporary  // 0x80
  private           System.Boolean                  waitLoad  // 0x81
PROPERTIES:
  Slate.ISubClipContainable.subClipOffset  get=0x04D90610  set=0x04D90600
  Slate.ISubClipContainable.subClipLength  get=0x04D879D0
  Slate.ISubClipContainable.subClipSpeed  get=0x04D879D0
  isValid  get=0x0AC7BD48
  length  get=0x04D863F0  set=0x04D86470
METHODS:
  RVA=0x0AC7BBAC  token=0x600075F  System.Void OnEnter()
  RVA=0x0AC7BC14  token=0x6000760  System.Void OnReverseEnter()
  RVA=0x0AC7BC4C  token=0x6000761  System.Void OnUpdate(System.Single time)
  RVA=0x0AC7BBE0  token=0x6000762  System.Void OnExit()
  RVA=0x0AC7BC28  token=0x6000763  System.Void OnReverse()
  RVA=0x0AC7B4E4  token=0x6000764  System.Void Activate()
  RVA=0x0AC7B5FC  token=0x6000765  System.Void Deactivate()
  RVA=0x0AC7B578  token=0x6000766  System.String CleanPath(System.String path)
  RVA=0x0AC7B83C  token=0x6000767  System.Void InitializeSubSceneCutscenes()
  RVA=0x0AC7B670  token=0x6000768  System.Void DenitializeSubSceneCutscenes(System.Boolean forward)
  RVA=0x0AC7BD28  token=0x6000769  System.Void .ctor()
  RVA=0x05396F58  token=0x600076A  System.String Slate.IDirectable.get_name()
END_CLASS

CLASS: Slate.ActionClips.SubCutscene
TYPE:  class
TOKEN: 0x20000E4
SIZE:  0x58
EXTENDS: Slate.DirectorActionClip
IMPLEMENTS: Slate.ISubClipContainable Slate.IDirectable
FIELDS:
  private           System.Single                   _length  // 0x40
  public            Slate.Cutscene                  subCutscene  // 0x48
  public            System.Single                   subCutsceneTimeOffset  // 0x50
  private           System.Boolean                  wasCamTrackActive  // 0x54
PROPERTIES:
  info  get=0x0AC87010
  isValid  get=0x0AC870D8
  length  get=0x04D863F0  set=0x04D86470
  actor  get=0x0AC86FCC
  Slate.ISubClipContainable.subClipOffset  get=0x04D885A0  set=0x04D88FF0
  Slate.ISubClipContainable.subClipLength  get=0x0AC86F54
  Slate.ISubClipContainable.subClipSpeed  get=0x0AC86F90
METHODS:
  RVA=0x0AC86C2C  token=0x6000774  System.Void OnEnter()
  RVA=0x0AC86D9C  token=0x6000775  System.Void OnReverseEnter()
  RVA=0x0AC86CF4  token=0x6000776  System.Void OnExit()
  RVA=0x0AC86E68  token=0x6000777  System.Void OnReverse()
  RVA=0x0AC86F08  token=0x6000778  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x0AC67F18  token=0x6000779  System.Void .ctor()
  RVA=0x05396F58  token=0x600077A  System.String Slate.IDirectable.get_name()
END_CLASS

CLASS: Slate.ActionClips.DestroyGameObjects
TYPE:  class
TOKEN: 0x20000E5
SIZE:  0x48
EXTENDS: Slate.DirectorActionClip
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.GameObject>targetObjects  // 0x40
PROPERTIES:
  info  get=0x0AC7ED70
METHODS:
  RVA=0x0AC7EB9C  token=0x600077C  System.Void OnEnter()
  RVA=0x0AC7ECF0  token=0x600077D  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.DirectorDisableGameObjects
TYPE:  class
TOKEN: 0x20000E6
SIZE:  0x50
EXTENDS: Slate.DirectorActionClip
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.GameObject>targetObjects  // 0x40
  private           System.Collections.Generic.Dictionary<UnityEngine.GameObject,System.Boolean>states  // 0x48
PROPERTIES:
  info  get=0x0AC7F1C0
METHODS:
  RVA=0x0AC7EDEC  token=0x600077F  System.Void OnEnter()
  RVA=0x0AC7EFB4  token=0x6000780  System.Void OnReverse()
  RVA=0x0AC7F140  token=0x6000781  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.DirectorEnableGameObjects
TYPE:  class
TOKEN: 0x20000E7
SIZE:  0x50
EXTENDS: Slate.DirectorActionClip
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.GameObject>targetObjects  // 0x40
  private           System.Collections.Generic.Dictionary<UnityEngine.GameObject,System.Boolean>states  // 0x48
PROPERTIES:
  info  get=0x0AC7F610
METHODS:
  RVA=0x0AC7F23C  token=0x6000783  System.Void OnEnter()
  RVA=0x0AC7F404  token=0x6000784  System.Void OnReverse()
  RVA=0x0AC7F590  token=0x6000785  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.InstantiateObject
TYPE:  class
TOKEN: 0x20000E8
SIZE:  0x98
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            UnityEngine.GameObject          targetObject  // 0x50
  public            UnityEngine.Transform           optionalParent  // 0x58
  public            UnityEngine.Vector3             targetPosition  // 0x60
  public            UnityEngine.Vector3             targetRotation  // 0x6c
  public            Slate.MiniTransformSpace        space  // 0x78
  public            Slate.EaseType                  popupInterpolation  // 0x7c
  private           UnityEngine.GameObject          instance  // 0x80
  private           UnityEngine.Vector3             wasScale  // 0x88
PROPERTIES:
  isValid  get=0x0AC7FD9C
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
  info  get=0x0AC7FCFC
  actor  get=0x04D86260
METHODS:
  RVA=0x0AC7F788  token=0x600078F  System.Void OnEnter()
  RVA=0x0AC7FB88  token=0x6000790  System.Void OnUpdate(System.Single time)
  RVA=0x0AC7FA98  token=0x6000791  System.Void OnExit()
  RVA=0x0AC7FAFC  token=0x6000792  System.Void OnReverseEnter()
  RVA=0x0AC7FB34  token=0x6000793  System.Void OnReverse()
  RVA=0x0AC7FCD8  token=0x6000794  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SampleParticleSystem
TYPE:  class
TOKEN: 0x20000E9
SIZE:  0x60
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            UnityEngine.ParticleSystem      particles  // 0x48
  public            System.Boolean                  simulationSync  // 0x50
  private           UnityEngine.ParticleSystem.EmissionModuleem  // 0x58
PROPERTIES:
  info  get=0x0AC83F44
  isValid  get=0x0AC8405C
  length  get=0x0AC840AC  set=0x04D86470
  blendOut  get=0x0AC83E78
  loop  get=0x0AC8413C
  duration  get=0x0AC83EC0
  startLifetime  get=0x0AC841BC
METHODS:
  RVA=0x0AC83C84  token=0x600079D  System.Void OnEnter()
  RVA=0x0AC83C84  token=0x600079E  System.Void OnReverseEnter()
  RVA=0x0AC83C8C  token=0x600079F  System.Void OnExit()
  RVA=0x0AC83C8C  token=0x60007A0  System.Void OnReverse()
  RVA=0x0AC83CE0  token=0x60007A1  System.Void OnRootEnabled()
  RVA=0x0AC83C94  token=0x60007A2  System.Void OnRootDisabled()
  RVA=0x0AC83D9C  token=0x60007A3  System.Void Play()
  RVA=0x0AC83D30  token=0x60007A4  System.Void OnUpdate(System.Single time)
  RVA=0x0AC83E18  token=0x60007A5  System.Void Stop()
  RVA=0x0AC83E58  token=0x60007A6  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.TransitAudioMixerSnapshot
TYPE:  class
TOKEN: 0x20000EA
SIZE:  0x60
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  public            UnityEngine.Audio.AudioMixer    audioMixer  // 0x48
  public            System.String                   snapshotName  // 0x50
  private           UnityEngine.Audio.AudioMixerSnapshotsnapshot  // 0x58
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x0AC7EA6C
  isValid  get=0x0AC871DC
METHODS:
  RVA=0x0AC871D4  token=0x60007AB  System.Void OnReverseEnter()
  RVA=0x0AC871D4  token=0x60007AC  System.Void OnEnter()
  RVA=0x0AC87144  token=0x60007AD  System.Void Do()
  RVA=0x0AC67F18  token=0x60007AE  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateAmbientLighting
TYPE:  class
TOKEN: 0x20000EB
SIZE:  0x78
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            System.Single                   ambientIntensity  // 0x4c
  public            UnityEngine.Color               ambientColor  // 0x50
  private           System.Single                   wasIntensity  // 0x60
  private           UnityEngine.Color               wasColor  // 0x64
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC7BD60  token=0x60007B5  System.Void OnCreate()
  RVA=0x0AC7BD90  token=0x60007B6  System.Void OnEnter()
  RVA=0x0AC7BDF0  token=0x60007B7  System.Void OnUpdate(System.Single time)
  RVA=0x0AC7BDC0  token=0x60007B8  System.Void OnReverse()
  RVA=0x0AC67F18  token=0x60007B9  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateFog
TYPE:  class
TOKEN: 0x20000EC
SIZE:  0x88
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            UnityEngine.Color               fogColor  // 0x4c
  public            System.Single                   fogDensity  // 0x5c
  public            System.Single                   linearFogStartDistance  // 0x60
  public            System.Single                   linearFogEndDistance  // 0x64
  private           UnityEngine.Color               wasColor  // 0x68
  private           System.Single                   wasDensity  // 0x78
  private           System.Single                   wasStartDistance  // 0x7c
  private           System.Single                   wasEndDistance  // 0x80
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC7C284  token=0x60007C0  System.Void OnCreate()
  RVA=0x0AC7C2CC  token=0x60007C1  System.Void OnEnter()
  RVA=0x0AC7C364  token=0x60007C2  System.Void OnUpdate(System.Single time)
  RVA=0x0AC7C318  token=0x60007C3  System.Void OnReverse()
  RVA=0x0AC67F18  token=0x60007C4  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateGravity
TYPE:  class
TOKEN: 0x20000ED
SIZE:  0x68
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            UnityEngine.Vector3             gravity  // 0x4c
  private           UnityEngine.Vector3             wasGravity  // 0x58
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC7C418  token=0x60007CB  System.Void OnCreate()
  RVA=0x0AC7C444  token=0x60007CC  System.Void OnEnter()
  RVA=0x0AC7C4FC  token=0x60007CD  System.Void OnUpdate(System.Single time)
  RVA=0x0AC7C470  token=0x60007CE  System.Void OnReverse()
  RVA=0x0AC67F18  token=0x60007CF  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateTimeScale
TYPE:  class
TOKEN: 0x20000EE
SIZE:  0x58
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            System.Single                   timeScale  // 0x4c
  public            Slate.EaseType                  interpolation  // 0x50
  private           System.Single                   wasScale  // 0x54
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC7E058  token=0x60007D6  System.Void OnCreate()
  RVA=0x0AC7E074  token=0x60007D7  System.Void OnEnter()
  RVA=0x0AC7E09C  token=0x60007D8  System.Void OnUpdate(System.Single time)
  RVA=0x0AC7E090  token=0x60007D9  System.Void OnReverse()
  RVA=0x0AC7E034  token=0x60007DA  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.EasySlowMotion
TYPE:  class
TOKEN: 0x20000EF
SIZE:  0x58
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            System.Single                   timeScale  // 0x4c
  public            Slate.EaseType                  interpolation  // 0x50
  private           System.Single                   lastTimeScale  // 0x54
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
  info  get=0x0AC7F72C
METHODS:
  RVA=0x0AC7E074  token=0x60007E2  System.Void OnEnter()
  RVA=0x0AC7F68C  token=0x60007E3  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC7E090  token=0x60007E4  System.Void OnReverse()
  RVA=0x0AC7F6F4  token=0x60007E5  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.RaiseUnityEvent
TYPE:  class
TOKEN: 0x20000F0
SIZE:  0x58
EXTENDS: Slate.DirectorActionClip
FIELDS:
  public            System.String                   customLabel  // 0x40
  public            UnityEngine.Events.UnityEvent   forwardEvent  // 0x48
  public            UnityEngine.Events.UnityEvent   reverseEvent  // 0x50
PROPERTIES:
  info  get=0x0AC82D68
METHODS:
  RVA=0x0AC82C8C  token=0x60007E7  System.Void OnEnter()
  RVA=0x0AC82CAC  token=0x60007E8  System.Void OnReverse()
  RVA=0x0AC82CCC  token=0x60007E9  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendGlobalMessage`1
TYPE:  abstract class
TOKEN: 0x20000F1
EXTENDS: Slate.ActionClips.SendGlobalMessage
FIELDS:
  public            T                               value  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60007EB  System.Void OnEnter()
  RVA=-1  // not resolved  token=0x60007EC  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendGlobalMessage
TYPE:  class
TOKEN: 0x20000F2
SIZE:  0x48
EXTENDS: Slate.DirectorActionClip
IMPLEMENTS: Slate.IEvent
FIELDS:
  public            System.String                   message  // 0x40
PROPERTIES:
  info  get=0x0AC84E84
  isValid  get=0x091DE054
  Slate.IEvent.name  get=0x04D85A60
METHODS:
  RVA=0x0AC84E74  token=0x60007F0  System.Void Slate.IEvent.Invoke()
  RVA=0x0AC84E34  token=0x60007F1  System.Void OnEnter()
  RVA=0x05393520  token=0x60007F2  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendGlobalMessageBoolean
TYPE:  class
TOKEN: 0x20000F3
SIZE:  0x50
EXTENDS: Slate.ActionClips.SendGlobalMessage`1
FIELDS:
METHODS:
  RVA=0x0AC84CF4  token=0x60007F3  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendGlobalMessageFloat
TYPE:  class
TOKEN: 0x20000F4
SIZE:  0x50
EXTENDS: Slate.ActionClips.SendGlobalMessage`1
FIELDS:
METHODS:
  RVA=0x0AC84D34  token=0x60007F4  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendGlobalMessageInteger
TYPE:  class
TOKEN: 0x20000F5
SIZE:  0x50
EXTENDS: Slate.ActionClips.SendGlobalMessage`1
FIELDS:
METHODS:
  RVA=0x0AC84D74  token=0x60007F5  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendGlobalMessageObject
TYPE:  class
TOKEN: 0x20000F6
SIZE:  0x50
EXTENDS: Slate.ActionClips.SendGlobalMessage`1
FIELDS:
METHODS:
  RVA=0x0AC84DB4  token=0x60007F6  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.SendGlobalMessageString
TYPE:  class
TOKEN: 0x20000F7
SIZE:  0x50
EXTENDS: Slate.ActionClips.SendGlobalMessage`1
FIELDS:
METHODS:
  RVA=0x0AC84DF4  token=0x60007F7  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.Captions
TYPE:  class
TOKEN: 0x20000F8
SIZE:  0x70
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            System.String                   text  // 0x50
  public            UnityEngine.Color               color  // 0x58
  public            Slate.EaseType                  interpolation  // 0x68
PROPERTIES:
  info  get=0x0AC7E954
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC7E86C  token=0x60007FF  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC7E8E0  token=0x6000800  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.CineBox
TYPE:  class
TOKEN: 0x20000F9
SIZE:  0x50
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC7E994  token=0x6000807  System.Void OnUpdate(System.Single time)
  RVA=0x0AC7E9AC  token=0x6000808  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.OverlayText
TYPE:  class
TOKEN: 0x20000FA
SIZE:  0x80
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            System.String                   text  // 0x50
  public            UnityEngine.TextAnchor          anchor  // 0x58
  public            Slate.EaseType                  interpolation  // 0x5c
  public            UnityEngine.Color               color  // 0x60
  public            System.Single                   size  // 0x70
  public            UnityEngine.Vector2             position  // 0x74
PROPERTIES:
  info  get=0x0AC8119C
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC81084  token=0x6000810  System.Void OnUpdate(System.Single time)
  RVA=0x0AC81118  token=0x6000811  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.OverlayTexture
TYPE:  class
TOKEN: 0x20000FB
SIZE:  0x80
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            UnityEngine.Texture             texture  // 0x50
  public            UnityEngine.Color               color  // 0x58
  public            UnityEngine.Vector2             scale  // 0x68
  public            UnityEngine.Vector2             position  // 0x70
  public            Slate.EaseType                  interpolation  // 0x78
PROPERTIES:
  info  get=0x0AC812C8
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC811DC  token=0x6000819  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC81278  token=0x600081A  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.ScreenFader
TYPE:  class
TOKEN: 0x20000FC
SIZE:  0x78
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            System.Single                   fade  // 0x4c
  public            UnityEngine.Color               outColor  // 0x50
  public            Slate.EaseType                  interpolation  // 0x60
  private           UnityEngine.Color               lastColor  // 0x64
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC847C0  token=0x6000821  System.Void OnEnter()
  RVA=0x0AC84800  token=0x6000822  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC847E0  token=0x6000823  System.Void OnReverse()
  RVA=0x0AC8486C  token=0x6000824  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimatePlaybackSpeed
TYPE:  class
TOKEN: 0x20000FD
SIZE:  0x58
EXTENDS: Slate.DirectorActionClip
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            System.Single                   speed  // 0x4c
  public            Slate.EaseType                  interpolation  // 0x50
  private           System.Single                   wasSpeed  // 0x54
PROPERTIES:
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC7DF0C  token=0x600082B  System.Void OnCreate()
  RVA=0x0AC7DF3C  token=0x600082C  System.Void OnEnter()
  RVA=0x0AC7DF9C  token=0x600082D  System.Void OnUpdate(System.Single time)
  RVA=0x0AC7DF6C  token=0x600082E  System.Void OnReverse()
  RVA=0x0AC7E034  token=0x600082F  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.DebugLog
TYPE:  class
TOKEN: 0x20000FE
SIZE:  0x50
EXTENDS: Slate.DirectorActionClip
FIELDS:
  public            System.Boolean                  neverSkip  // 0x40
  public            System.String                   text  // 0x48
PROPERTIES:
  info  get=0x0AC7EB5C
METHODS:
  RVA=0x0AC7EABC  token=0x6000831  System.Void OnEnter()
  RVA=0x05393520  token=0x6000832  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.PauseCutscene
TYPE:  class
TOKEN: 0x20000FF
SIZE:  0x40
EXTENDS: Slate.DirectorActionClip
FIELDS:
METHODS:
  RVA=0x0AC81368  token=0x6000833  System.Void OnEnter()
  RVA=0x05393520  token=0x6000834  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.ScreenCapture
TYPE:  class
TOKEN: 0x2000100
SIZE:  0x40
EXTENDS: Slate.DirectorActionClip
FIELDS:
METHODS:
  RVA=0x05393520  token=0x6000835  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.PlayAnimationClip
TYPE:  class
TOKEN: 0x2000101
SIZE:  0x88
EXTENDS: Slate.ActionClips.ActorActionClip`1
IMPLEMENTS: Slate.ISubClipContainable Slate.IDirectable
FIELDS:
  private           System.Single                   _length  // 0x48
  private           System.Single                   _blendIn  // 0x4c
  private           System.Single                   _blendOut  // 0x50
  public            UnityEngine.AnimationClip       animationClip  // 0x58
  public            System.Single                   clipOffset  // 0x60
  public            System.Single                   playbackSpeed  // 0x64
  private           Slate.TransformSnapshot         snapShot  // 0x68
  private           UnityEngine.Transform           mixTransform  // 0x70
  private           UnityEngine.AnimationState      state  // 0x78
  private           System.Boolean                  isListClip  // 0x80
PROPERTIES:
  Slate.ISubClipContainable.subClipOffset  get=0x04D88480  set=0x04D884A0
  Slate.ISubClipContainable.subClipLength  get=0x0AC81A4C
  Slate.ISubClipContainable.subClipSpeed  get=0x04D88470
  info  get=0x0AC81B10
  isValid  get=0x0AC81B84
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x04D87860  set=0x04D91480
  blendOut  get=0x04D885A0  set=0x04D88FF0
  canCrossBlend  get=0x02FFF600
  track  get=0x0AC81C14
METHODS:
  RVA=0x0AC8145C  token=0x6000844  System.Void OnEnter()
  RVA=0x0AC8170C  token=0x6000845  System.Void OnUpdate(System.Single time)
  RVA=0x0AC8168C  token=0x6000846  System.Void OnReverse()
  RVA=0x0AC81644  token=0x6000847  System.Void OnExit()
  RVA=0x0AC81668  token=0x6000848  System.Void OnReverseEnter()
  RVA=0x0AC81ABC  token=0x6000849  System.Void .ctor()
  RVA=0x05396F58  token=0x600084A  System.String Slate.IDirectable.get_name()
END_CLASS

CLASS: Slate.ActionClips.PlayAnimatorClip
TYPE:  class
TOKEN: 0x2000102
SIZE:  0xB0
EXTENDS: Slate.ActionClips.ActorActionClip
IMPLEMENTS: Slate.ISubClipContainable Slate.IDirectable
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            UnityEngine.AnimationClip       animationClip  // 0x50
  public            System.Single                   clipOffset  // 0x58
  public            Slate.ActionClips.PlayAnimatorClip.ClipWrapModeclipWrapMode  // 0x5c
  public            System.Single                   clipWeight  // 0x60
  public            System.Single                   playbackSpeed  // 0x64
  public            Slate.ActionClips.PlayAnimatorClip.StartingTransformsModestartingTransformsMode  // 0x68
  public            Slate.MiniTransformSpace        transformSpace  // 0x6c
  public            UnityEngine.Vector3             startingPosition  // 0x70
  public            UnityEngine.Vector3             startingRotation  // 0x7c
  public            UnityEngine.Vector2             steerLocalRotation  // 0x88
  protected         UnityEngine.Vector3             wasPosition  // 0x90
  protected         UnityEngine.Quaternion          wasRotation  // 0x9c
PROPERTIES:
  isMasterAndManualSet  get=0x0AC82578
  Slate.ISubClipContainable.subClipOffset  get=0x04D88400  set=0x04D88430
  Slate.ISubClipContainable.subClipLength  get=0x0AC82440
  Slate.ISubClipContainable.subClipSpeed  get=0x04D88470
  info  get=0x0AC82500
  isValid  get=0x0AC825C0
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
  canCrossBlend  get=0x02FFF600
  track  get=0x0AC8266C
  animator  get=0x0AC824DC
  isMasterTrack  get=0x0AC8259C
METHODS:
  RVA=0x0AC81C50  token=0x600085C  System.Void OnEnter()
  RVA=0x0AC81ED8  token=0x600085D  System.Void OnReverseEnter()
  RVA=0x0AC8201C  token=0x600085E  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x0AC81E74  token=0x600085F  System.Void OnExit()
  RVA=0x0AC81F3C  token=0x6000860  System.Void OnReverse()
  RVA=0x0AC824B0  token=0x6000861  System.Void .ctor()
  RVA=0x05396F58  token=0x6000862  System.String Slate.IDirectable.get_name()
END_CLASS

CLASS: Slate.ActionClips.PlayAudio
TYPE:  class
TOKEN: 0x2000105
SIZE:  0x80
EXTENDS: Slate.ActionClip
IMPLEMENTS: Slate.ISubClipContainable Slate.IDirectable
FIELDS:
  private           System.Single                   _length  // 0x40
  private           System.Single                   _blendIn  // 0x44
  private           System.Single                   _blendOut  // 0x48
  public            UnityEngine.AudioClip           audioClip  // 0x50
  public            System.Single                   volume  // 0x58
  public            System.Single                   pitch  // 0x5c
  public            System.Single                   stereoPan  // 0x60
  public            System.Single                   clipOffset  // 0x64
  public            System.String                   subtitlesText  // 0x68
  public            UnityEngine.Color               subtitlesColor  // 0x70
PROPERTIES:
  Slate.ISubClipContainable.subClipOffset  get=0x04D88470  set=0x04D88490
  Slate.ISubClipContainable.subClipLength  get=0x0AC82A0C
  Slate.ISubClipContainable.subClipSpeed  get=0x04D88DD0
  length  get=0x04D863F0  set=0x04D86470
  blendIn  get=0x04D86400  set=0x04D86480
  blendOut  get=0x04D8D350  set=0x04D8D380
  isValid  get=0x0AC82BE0
  info  get=0x0AC82B58
  track  get=0x0AC82C50
  source  get=0x0AC82C30
METHODS:
  RVA=0x0AC8272C  token=0x6000871  System.Void OnEnter()
  RVA=0x0AC8272C  token=0x6000872  System.Void OnReverseEnter()
  RVA=0x0AC82734  token=0x6000873  System.Void OnExit()
  RVA=0x0AC82734  token=0x6000874  System.Void OnReverse()
  RVA=0x0AC826A8  token=0x6000875  System.Void Do()
  RVA=0x0AC8273C  token=0x6000876  System.Void OnUpdate(System.Single time, System.Single previousTime)
  RVA=0x0AC82A7C  token=0x6000877  System.Void Undo()
  RVA=0x0AC82AFC  token=0x6000878  System.Void .ctor()
  RVA=0x05396F58  token=0x6000879  System.String Slate.IDirectable.get_name()
END_CLASS

CLASS: Slate.ActionClips.AnimateBoolParameter
TYPE:  class
TOKEN: 0x2000106
SIZE:  0x60
EXTENDS: Slate.ActionClips.MecanimBaseClip
FIELDS:
  private           System.Single                   _length  // 0x48
  public            System.String                   parameterName  // 0x50
  public            System.Boolean                  value  // 0x58
  private           System.Boolean                  lastValue  // 0x59
PROPERTIES:
  isValid  get=0x0AC7C048
  info  get=0x0AC7C008
  length  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC7BE70  token=0x600087E  System.Void OnEnter()
  RVA=0x0AC7BF9C  token=0x600087F  System.Void OnUpdate(System.Single time)
  RVA=0x0AC7BEC8  token=0x6000880  System.Void OnExit()
  RVA=0x0AC7BF38  token=0x6000881  System.Void OnReverse()
  RVA=0x0AC7BFF4  token=0x6000882  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateFloatParameter
TYPE:  class
TOKEN: 0x2000107
SIZE:  0x68
EXTENDS: Slate.ActionClips.MecanimBaseClip
FIELDS:
  private           System.Single                   _length  // 0x48
  private           System.Single                   _blendIn  // 0x4c
  private           System.Single                   _blendOut  // 0x50
  public            System.String                   parameterName  // 0x58
  public            System.Single                   value  // 0x60
  private           System.Single                   lastValue  // 0x64
PROPERTIES:
  isValid  get=0x0AC7C254
  info  get=0x0AC7C214
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x04D87860  set=0x04D91480
  blendOut  get=0x04D885A0  set=0x04D88FF0
METHODS:
  RVA=0x0AC7C078  token=0x600088B  System.Void OnEnter()
  RVA=0x0AC7C138  token=0x600088C  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC7C0D4  token=0x600088D  System.Void OnReverse()
  RVA=0x0AC7C1F0  token=0x600088E  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateIntegerParameter
TYPE:  class
TOKEN: 0x2000108
SIZE:  0x68
EXTENDS: Slate.ActionClips.MecanimBaseClip
FIELDS:
  private           System.Single                   _length  // 0x48
  private           System.Single                   _blendIn  // 0x4c
  private           System.Single                   _blendOut  // 0x50
  public            System.String                   parameterName  // 0x58
  public            System.Int32                    value  // 0x60
  private           System.Int32                    lastValue  // 0x64
PROPERTIES:
  isValid  get=0x0AC7C254
  info  get=0x0AC7C774
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x04D87860  set=0x04D91480
  blendOut  get=0x04D885A0  set=0x04D88FF0
METHODS:
  RVA=0x0AC7C5E0  token=0x6000897  System.Void OnEnter()
  RVA=0x0AC7C69C  token=0x6000898  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC7C638  token=0x6000899  System.Void OnReverse()
  RVA=0x0AC7C758  token=0x600089A  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateLayerWeight
TYPE:  class
TOKEN: 0x2000109
SIZE:  0x60
EXTENDS: Slate.ActionClips.MecanimBaseClip
FIELDS:
  private           System.Single                   _length  // 0x48
  private           System.Single                   _blendIn  // 0x4c
  private           System.Single                   _blendOut  // 0x50
  public            System.Int32                    layerIndex  // 0x54
  public            System.Single                   weight  // 0x58
  private           System.Single                   lastValue  // 0x5c
PROPERTIES:
  info  get=0x0AC7C94C
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x04D87860  set=0x04D91480
  blendOut  get=0x04D885A0  set=0x04D88FF0
METHODS:
  RVA=0x0AC7C7B4  token=0x60008A2  System.Void OnEnter()
  RVA=0x0AC7C870  token=0x60008A3  System.Void OnUpdate(System.Single deltaTime)
  RVA=0x0AC7C80C  token=0x60008A4  System.Void OnReverse()
  RVA=0x0AC7C928  token=0x60008A5  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateLimbIK
TYPE:  class
TOKEN: 0x200010A
SIZE:  0x98
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  private           System.Single                   _length  // 0x48
  private           System.Single                   _blendIn  // 0x4c
  private           System.Single                   _blendOut  // 0x50
  public            UnityEngine.AvatarIKGoal        IKGoal  // 0x54
  public            System.Single                   weight  // 0x58
  public            Slate.TransformRefPositionRotationIKTarget  // 0x60
  private           Slate.AnimatorDispatcher        dispatcher  // 0x90
PROPERTIES:
  targetPosition  get=0x0AC7D244  set=0x04DA9C90
  targetRotation  get=0x0AC7D278  set=0x04DA9D40
  info  get=0x0AC7D1D4
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x04D87860  set=0x04D91480
  blendOut  get=0x04D885A0  set=0x04D88FF0
METHODS:
  RVA=0x0AC7CDBC  token=0x60008B1  System.Void OnCreate()
  RVA=0x0AC7C9AC  token=0x60008B2  System.Void OnAfterValidate()
  RVA=0x0AC7CDF8  token=0x60008B3  System.Void OnEnter()
  RVA=0x0AC7CF84  token=0x60008B4  System.Void OnReverseEnter()
  RVA=0x0AC7D07C  token=0x60008B5  System.Void OnReverse()
  RVA=0x0AC7CEF0  token=0x60008B6  System.Void OnExit()
  RVA=0x0AC7D110  token=0x60008B7  System.Void OnRootDisabled()
  RVA=0x0AC7CBF8  token=0x60008B8  System.Void OnAnimatorIK(System.Int32 index)
  RVA=0x0AC7D188  token=0x60008B9  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateLookAtIK
TYPE:  class
TOKEN: 0x200010C
SIZE:  0x90
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
  private           System.Single                   _length  // 0x48
  private           System.Single                   _blendIn  // 0x4c
  private           System.Single                   _blendOut  // 0x50
  public            System.Single                   weight  // 0x54
  public            System.Single                   bodyWeight  // 0x58
  public            System.Single                   headWeight  // 0x5c
  public            System.Single                   eyesWeight  // 0x60
  public            Slate.TransformRefPosition      targetPosition  // 0x68
  private           Slate.AnimatorDispatcher        dispatcher  // 0x88
PROPERTIES:
  targetPositionVector  get=0x0AC7D9C8  set=0x04DA9D60
  info  get=0x0AC7D99C
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x04D87860  set=0x04D91480
  blendOut  get=0x04D885A0  set=0x04D88FF0
METHODS:
  RVA=0x0AC7D574  token=0x60008C5  System.Void OnCreate()
  RVA=0x0AC7D2AC  token=0x60008C6  System.Void OnAfterValidate()
  RVA=0x0AC7D5B4  token=0x60008C7  System.Void OnEnter()
  RVA=0x0AC7D740  token=0x60008C8  System.Void OnReverseEnter()
  RVA=0x0AC7D838  token=0x60008C9  System.Void OnReverse()
  RVA=0x0AC7D6AC  token=0x60008CA  System.Void OnExit()
  RVA=0x0AC7D8CC  token=0x60008CB  System.Void OnRootDisabled()
  RVA=0x0AC7D430  token=0x60008CC  System.Void OnAnimatorIK(System.Int32 index)
  RVA=0x0AC7D944  token=0x60008CD  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.AnimateTrigger
TYPE:  class
TOKEN: 0x200010E
SIZE:  0x60
EXTENDS: Slate.ActionClips.MecanimBaseClip
FIELDS:
  private           System.Single                   _length  // 0x48
  public            System.String                   triggerName  // 0x50
  public            System.Boolean                  value  // 0x58
PROPERTIES:
  isValid  get=0x0AC7C048
  info  get=0x0AC7E194
  length  get=0x04D8D350  set=0x04D8D380
METHODS:
  RVA=0x0AC7E10C  token=0x60008D4  System.Void OnUpdate(System.Single time)
  RVA=0x0AC7E184  token=0x60008D5  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.CrossFadeState
TYPE:  class
TOKEN: 0x200010F
SIZE:  0x58
EXTENDS: Slate.ActionClips.MecanimBaseClip
FIELDS:
  private           System.Single                   _length  // 0x48
  public            System.String                   stateName  // 0x50
PROPERTIES:
  info  get=0x0AC7EA7C
  length  get=0x04D8D350  set=0x04D8D380
  blendIn  get=0x0AC7EA6C
METHODS:
  RVA=0x0AC7E9D8  token=0x60008DA  System.Void OnEnter()
  RVA=0x0AC7E184  token=0x60008DB  System.Void .ctor()
END_CLASS

CLASS: Slate.ActionClips.MecanimBaseClip
TYPE:  abstract class
TOKEN: 0x2000110
SIZE:  0x48
EXTENDS: Slate.ActionClips.ActorActionClip`1
FIELDS:
PROPERTIES:
  isValid  get=0x0AC80CE4
METHODS:
  RVA=0x0AC809F8  token=0x60008DD  System.Boolean HasParameter(System.String name)
  RVA=0x0AC80CA4  token=0x60008DE  System.Void .ctor()
END_CLASS

