// ========================================================
// Dumped by @desirepro
// Assembly: Cinemachine.dll
// Classes:  204
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

CLASS: CinemachineCameraOffset
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x40
EXTENDS: Cinemachine.CinemachineExtension
FIELDS:
  public            UnityEngine.Vector3             m_Offset  // 0x28
  public            Cinemachine.CinemachineCore.Stagem_ApplyAfter  // 0x34
  public            System.Boolean                  m_PreserveComposition  // 0x38
METHODS:
  RVA=0x0678E3A8  token=0x6000001  System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x0678E6D0  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: CinemachineRecomposer
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x48
EXTENDS: Cinemachine.CinemachineExtension
FIELDS:
  public            Cinemachine.CinemachineCore.Stagem_ApplyAfter  // 0x28
  public            System.Single                   m_Tilt  // 0x2c
  public            System.Single                   m_Pan  // 0x30
  public            System.Single                   m_Dutch  // 0x34
  public            System.Single                   m_ZoomScale  // 0x38
  public            System.Single                   m_FollowAttachment  // 0x3c
  public            System.Single                   m_LookAtAttachment  // 0x40
METHODS:
  RVA=0x04D879E0  token=0x6000003  System.Void Reset()
  RVA=0x0679A754  token=0x6000004  System.Void OnValidate()
  RVA=0x04073A30  token=0x6000005  System.Void PrePipelineMutateCameraStateCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x031FE9B0  token=0x6000006  System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x05393520  token=0x6000007  System.Void .ctor()
END_CLASS

CLASS: CinemachineBeyondCommunicator
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
  private   static  System.Func<Cinemachine.CinemachineVirtualCameraBase><GetLevelCamera>k__BackingField  // static @ 0x0
PROPERTIES:
  GetLevelCamera  get=0x0678C1C0  set=0x0678C1F8
METHODS:
END_CLASS

CLASS: CinemachineTouchInputMapper
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Single                   TouchSensitivityX  // 0x18
  public            System.Single                   TouchSensitivityY  // 0x1c
  public            System.String                   TouchXInputMapTo  // 0x20
  public            System.String                   TouchYInputMapTo  // 0x28
METHODS:
  RVA=0x0679AD50  token=0x600000A  System.Void Start()
  RVA=0x0679ACB0  token=0x600000B  System.Single GetInputAxis(System.String axisName)
  RVA=0x0679AE00  token=0x600000C  System.Void .ctor()
END_CLASS

CLASS: CinemachineMixer
TYPE:  sealed class
TOKEN: 0x2000006
SIZE:  0x20
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  private           Cinemachine.CinemachineBrain    mBrain  // 0x10
  private           System.Int32                    mBrainOverrideId  // 0x18
  private           System.Boolean                  mPreviewPlay  // 0x1c
METHODS:
  RVA=0x06798578  token=0x600000D  System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable)
  RVA=0x04D87900  token=0x600000E  System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info)
  RVA=0x067985EC  token=0x600000F  System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData)
  RVA=0x06798528  token=0x6000010  System.Single GetDeltaTime(System.Single deltaTime)
  RVA=0x04D87910  token=0x6000011  System.Void .ctor()
END_CLASS

CLASS: CinemachineShot
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x38
EXTENDS: UnityEngine.Playables.PlayableAsset
IMPLEMENTS: UnityEngine.Timeline.IPropertyPreview UnityEngine.Timeline.ITimelineClipAsset
FIELDS:
  public            System.Boolean                  DisableEvaluateTwiceWhenEnabled  // 0x18
  public            System.Boolean                  UseVcamBeforeTimeline  // 0x19
  public            System.Boolean                  UseLevelCamera  // 0x1a
  public            System.String                   DisplayName  // 0x20
  public            UnityEngine.ExposedReference<Cinemachine.CinemachineVirtualCameraBase>VirtualCamera  // 0x28
PROPERTIES:
  clipCaps  get=0x0679AC9C
METHODS:
  RVA=0x0679A7E0  token=0x6000013  UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner)
  RVA=0x0679AB00  token=0x6000014  System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver)
  RVA=0x03573370  token=0x6000015  System.Void .ctor()
END_CLASS

CLASS: CinemachineShotPlayable
TYPE:  sealed class
TOKEN: 0x2000008
SIZE:  0x18
EXTENDS: UnityEngine.Playables.PlayableBehaviour
FIELDS:
  public            Cinemachine.CinemachineVirtualCameraBaseVirtualCamera  // 0x10
PROPERTIES:
  IsValid  get=0x0679A790
METHODS:
  RVA=0x041E1670  token=0x6000017  System.Void .ctor()
END_CLASS

CLASS: CinemachineTrack
TYPE:  class
TOKEN: 0x2000009
SIZE:  0xC0
EXTENDS: UnityEngine.Timeline.TrackAsset
FIELDS:
METHODS:
  RVA=0x0679AE74  token=0x6000018  UnityEngine.Playables.Playable CreateTrackMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount)
  RVA=0x0679AF54  token=0x6000019  System.Void .ctor()
END_CLASS

CLASS: Instruction
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x38
FIELDS:
  public            Cinemachine.CinemachineVirtualCameraBasem_VirtualCamera  // 0x10
  public            System.Single                   m_Hold  // 0x18
  public            Cinemachine.CinemachineBlendDefinitionm_Blend  // 0x20
METHODS:
END_CLASS

CLASS: UpdateMethod
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineBrain.UpdateMethodFixedUpdate  // const
  public    static  Cinemachine.CinemachineBrain.UpdateMethodLateUpdate  // const
  public    static  Cinemachine.CinemachineBrain.UpdateMethodSmartUpdate  // const
  public    static  Cinemachine.CinemachineBrain.UpdateMethodManualUpdate  // const
METHODS:
END_CLASS

CLASS: BrainUpdateMethod
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineBrain.BrainUpdateMethodFixedUpdate  // const
  public    static  Cinemachine.CinemachineBrain.BrainUpdateMethodLateUpdate  // const
METHODS:
END_CLASS

CLASS: BrainEvent
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x03FCFD10  token=0x6000061  System.Void .ctor()
END_CLASS

CLASS: VcamActivatedEvent
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`2
FIELDS:
METHODS:
  RVA=0x03FCFB00  token=0x6000062  System.Void .ctor()
END_CLASS

CLASS: BrainFrame
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x38
FIELDS:
  public            System.Int32                    id  // 0x10
  public            Cinemachine.CinemachineBlend    blend  // 0x18
  public            Cinemachine.CinemachineBlend    workingBlend  // 0x20
  public            Cinemachine.BlendSourceVirtualCameraworkingBlendSource  // 0x28
  public            System.Single                   deltaTimeOverride  // 0x30
PROPERTIES:
  Active  get=0x0678B750
METHODS:
  RVA=0x03FCFD50  token=0x6000064  System.Void .ctor()
END_CLASS

CLASS: <AfterPhysics>d__36
TYPE:  sealed class
TOKEN: 0x2000013
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Cinemachine.CinemachineBrain    <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000065  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000066  System.Void System.IDisposable.Dispose()
  RVA=0x03D30400  token=0x6000067  System.Boolean MoveNext()
  RVA=0x0679BB74  token=0x6000069  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Pair
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x18
FIELDS:
  public            System.Int32                    a  // 0x10
  public            System.Single                   b  // 0x14
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000016
SIZE:  0x10
FIELDS:
  public    static readonly Cinemachine.CinemachineClearShot.<>c<>9  // static @ 0x0
  public    static  System.Comparison<Cinemachine.CinemachineClearShot.Pair><>9__47_0  // static @ 0x8
METHODS:
  RVA=0x0679BBC0  token=0x6000085  System.Void .cctor()
  RVA=0x041E1670  token=0x6000086  System.Void .ctor()
  RVA=0x04D87A20  token=0x6000087  System.Int32 <Randomize>b__47_0(Cinemachine.CinemachineClearShot.Pair p1, Cinemachine.CinemachineClearShot.Pair p2)
END_CLASS

CLASS: ResolutionStrategy
TYPE:  sealed struct
TOKEN: 0x2000018
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineCollider.ResolutionStrategyPullCameraForward  // const
  public    static  Cinemachine.CinemachineCollider.ResolutionStrategyPreserveCameraHeight  // const
  public    static  Cinemachine.CinemachineCollider.ResolutionStrategyPreserveCameraDistance  // const
METHODS:
END_CLASS

CLASS: VcamExtraState
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3             previousDisplacement  // 0x10
  public            System.Single                   previousLookAt2DisplacedPosLen  // 0x1c
  public            System.Boolean                  shouldPerformCollisionDamping  // 0x20
  public            System.Boolean                  previousHitSomething  // 0x21
  public            System.Boolean                  targetObscured  // 0x22
  public            System.Single                   occlusionStartTime  // 0x24
  public            System.Collections.Generic.List<UnityEngine.Vector3>debugResolutionPath  // 0x28
  private           System.Single                   m_SmoothedDistance  // 0x30
  private           System.Single                   m_SmoothedTime  // 0x34
METHODS:
  RVA=0x041E1670  token=0x600009D  System.Void AddPointToDebugPath(UnityEngine.Vector3 p)
  RVA=0x0403BFB0  token=0x600009E  System.Single ApplyDistanceSmoothing(System.Single distance, System.Single smoothingTime)
  RVA=0x0679BC24  token=0x600009F  System.Void UpdateDistanceSmoothing(System.Single distance)
  RVA=0x03FC2820  token=0x60000A0  System.Void ResetDistanceSmoothing(System.Single smoothingTime)
  RVA=0x041E1670  token=0x60000A1  System.Void .ctor()
END_CLASS

CLASS: Mode
TYPE:  sealed struct
TOKEN: 0x200001B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineConfiner.ModeConfine2D  // const
  public    static  Cinemachine.CinemachineConfiner.ModeConfine3D  // const
METHODS:
END_CLASS

CLASS: VcamExtraState
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x20
FIELDS:
  public            UnityEngine.Vector3             m_previousDisplacement  // 0x10
  public            System.Single                   confinerDisplacement  // 0x1c
METHODS:
  RVA=0x041E1670  token=0x60000AE  System.Void .ctor()
END_CLASS

CLASS: VcamExtraState
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3             m_PreviousDisplacement  // 0x10
  public            UnityEngine.Vector3             m_DampedDisplacement  // 0x1c
  public            Cinemachine.ConfinerOven.BakedSolutionm_BakedSolution  // 0x28
  public            Cinemachine.CinemachineVirtualCameraBasem_vcam  // 0x30
METHODS:
  RVA=0x041E1670  token=0x60000B6  System.Void .ctor()
END_CLASS

CLASS: ShapeCache
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0xF8
FIELDS:
  public            Cinemachine.ConfinerOven        m_confinerOven  // 0x10
  public            System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Vector2>>m_OriginalPath  // 0x18
  public            UnityEngine.Matrix4x4           m_DeltaWorldToBaked  // 0x20
  public            UnityEngine.Matrix4x4           m_DeltaBakedToWorld  // 0x60
  private           System.Single                   m_aspectRatio  // 0xa0
  private           System.Single                   m_maxWindowSize  // 0xa4
  private           System.Single                   m_maxComputationTimePerFrameInSeconds  // 0xa8
  private           UnityEngine.Matrix4x4           m_bakedToWorld  // 0xac
  private           UnityEngine.Collider2D          m_boundingShape2D  // 0xf0
METHODS:
  RVA=0x0679B384  token=0x60000B7  System.Void Invalidate()
  RVA=0x0679B53C  token=0x60000B8  System.Boolean ValidateCache(UnityEngine.Collider2D boundingShape2D, System.Single maxWindowSize, System.Single aspectRatio, System.Boolean& confinerStateChanged)
  RVA=0x0679B424  token=0x60000B9  System.Boolean IsValid(UnityEngine.Collider2D& boundingShape2D, System.Single& aspectRatio, System.Single& maxOrthoSize)
  RVA=0x0679B19C  token=0x60000BA  System.Void CalculateDeltaTransformationMatrix()
END_CLASS

CLASS: UpdateMethod
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineDollyCart.UpdateMethodUpdate  // const
  public    static  Cinemachine.CinemachineDollyCart.UpdateMethodFixedUpdate  // const
  public    static  Cinemachine.CinemachineDollyCart.UpdateMethodLateUpdate  // const
METHODS:
END_CLASS

CLASS: VcamExtraState
TYPE:  class
TOKEN: 0x2000024
SIZE:  0x18
FIELDS:
  public            System.Single                   m_previousFrameZoom  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000CB  System.Void .ctor()
END_CLASS

CLASS: Orbit
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x18
FIELDS:
  public            System.Single                   m_Height  // 0x10
  public            System.Single                   m_Radius  // 0x14
METHODS:
  RVA=0x04D87A10  token=0x60000EE  System.Void .ctor(System.Single h, System.Single r)
END_CLASS

CLASS: CreateRigDelegate
TYPE:  sealed class
TOKEN: 0x2000027
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679AFA0  token=0x60000EF  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014F3120  token=0x60000F0  Cinemachine.CinemachineVirtualCamera Invoke(Cinemachine.CinemachineFreeLook vcam, System.String name, Cinemachine.CinemachineVirtualCamera copyFrom)
END_CLASS

CLASS: DestroyRigDelegate
TYPE:  sealed class
TOKEN: 0x2000028
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679B0C8  token=0x60000F1  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x60000F2  System.Void Invoke(UnityEngine.GameObject rig)
END_CLASS

CLASS: Waypoint
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x2C
FIELDS:
  public            UnityEngine.Vector3             position  // 0x10
  public            UnityEngine.Vector3             tangent  // 0x1c
  public            System.Single                   roll  // 0x28
METHODS:
END_CLASS

CLASS: Waypoint
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x20
FIELDS:
  public            UnityEngine.Vector3             position  // 0x10
  public            System.Single                   roll  // 0x1c
PROPERTIES:
  AsVector4  get=0x0328FB00
METHODS:
  RVA=0x067A9D14  token=0x6000126  Cinemachine.CinemachineSmoothPath.Waypoint FromVector4(UnityEngine.Vector4 v)
END_CLASS

CLASS: Instruction
TYPE:  sealed struct
TOKEN: 0x2000031
SIZE:  0x28
FIELDS:
  public            System.Int32                    m_FullHash  // 0x10
  public            Cinemachine.CinemachineVirtualCameraBasem_VirtualCamera  // 0x18
  public            System.Single                   m_ActivateAfter  // 0x20
  public            System.Single                   m_MinDuration  // 0x24
METHODS:
END_CLASS

CLASS: ParentHash
TYPE:  sealed struct
TOKEN: 0x2000032
SIZE:  0x18
FIELDS:
  public            System.Int32                    m_Hash  // 0x10
  public            System.Int32                    m_ParentHash  // 0x14
METHODS:
END_CLASS

CLASS: HashPair
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x18
FIELDS:
  public            System.Int32                    parentHash  // 0x10
  public            System.Int32                    hash  // 0x14
METHODS:
END_CLASS

CLASS: FillStrategy
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineStoryboard.FillStrategyBestFit  // const
  public    static  Cinemachine.CinemachineStoryboard.FillStrategyCropImageToFit  // const
  public    static  Cinemachine.CinemachineStoryboard.FillStrategyStretchToFit  // const
METHODS:
END_CLASS

CLASS: CanvasInfo
TYPE:  class
TOKEN: 0x2000036
SIZE:  0x38
FIELDS:
  public            UnityEngine.GameObject          mCanvas  // 0x10
  public            UnityEngine.Canvas              mCanvasComponent  // 0x18
  public            Cinemachine.CinemachineBrain    mCanvasParent  // 0x20
  public            UnityEngine.RectTransform       mViewport  // 0x28
  public            UnityEngine.UI.RawImage         mRawImage  // 0x30
METHODS:
  RVA=0x041E1670  token=0x600014F  System.Void .ctor()
END_CLASS

CLASS: StoryboardRenderMode
TYPE:  sealed struct
TOKEN: 0x2000037
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineStoryboard.StoryboardRenderModeScreenSpaceOverlay  // const
  public    static  Cinemachine.CinemachineStoryboard.StoryboardRenderModeScreenSpaceCamera  // const
METHODS:
END_CLASS

CLASS: Target
TYPE:  sealed struct
TOKEN: 0x200003A
SIZE:  0x20
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
  public            System.Single                   weight  // 0x18
  public            System.Single                   radius  // 0x1c
METHODS:
END_CLASS

CLASS: PositionMode
TYPE:  sealed struct
TOKEN: 0x200003B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineTargetGroup.PositionModeGroupCenter  // const
  public    static  Cinemachine.CinemachineTargetGroup.PositionModeGroupAverage  // const
METHODS:
END_CLASS

CLASS: RotationMode
TYPE:  sealed struct
TOKEN: 0x200003C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineTargetGroup.RotationModeManual  // const
  public    static  Cinemachine.CinemachineTargetGroup.RotationModeGroupAverage  // const
METHODS:
END_CLASS

CLASS: UpdateMethod
TYPE:  sealed struct
TOKEN: 0x200003D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineTargetGroup.UpdateMethodUpdate  // const
  public    static  Cinemachine.CinemachineTargetGroup.UpdateMethodFixedUpdate  // const
  public    static  Cinemachine.CinemachineTargetGroup.UpdateMethodLateUpdate  // const
METHODS:
END_CLASS

CLASS: CreatePipelineDelegate
TYPE:  sealed class
TOKEN: 0x200003F
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679AFA0  token=0x6000189  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014F3120  token=0x600018A  UnityEngine.Transform Invoke(Cinemachine.CinemachineVirtualCamera vcam, System.String name, Cinemachine.CinemachineComponentBase[] copyFrom)
END_CLASS

CLASS: DestroyPipelineDelegate
TYPE:  sealed class
TOKEN: 0x2000040
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679B0C8  token=0x600018B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600018C  System.Void Invoke(UnityEngine.GameObject pipeline)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000041
SIZE:  0x10
FIELDS:
  public    static readonly Cinemachine.CinemachineVirtualCamera.<>c<>9  // static @ 0x0
  public    static  System.Comparison<Cinemachine.CinemachineComponentBase><>9__39_0  // static @ 0x8
  public    static  System.Func<Cinemachine.CinemachineComponentBase,System.Boolean><>9__48_0  // static @ 0x10
METHODS:
  RVA=0x04D4C1B0  token=0x600018D  System.Void .cctor()
  RVA=0x041E1670  token=0x600018E  System.Void .ctor()
  RVA=0x0389DF10  token=0x600018F  System.Int32 <UpdateComponentPipeline>b__39_0(Cinemachine.CinemachineComponentBase c1, Cinemachine.CinemachineComponentBase c2)
  RVA=0x067A9A18  token=0x6000190  System.Boolean <RequiresUserInput>b__48_0(Cinemachine.CinemachineComponentBase c)
END_CLASS

CLASS: FovCache
TYPE:  sealed struct
TOKEN: 0x2000045
SIZE:  0x60
FIELDS:
  public            UnityEngine.Rect                mFovSoftGuideRect  // 0x10
  public            UnityEngine.Rect                mFovHardGuideRect  // 0x20
  public            System.Single                   mFovH  // 0x30
  public            System.Single                   mFov  // 0x34
  private           System.Single                   mOrthoSizeOverDistance  // 0x38
  private           System.Single                   mAspect  // 0x3c
  private           UnityEngine.Rect                mSoftGuideRect  // 0x40
  private           UnityEngine.Rect                mHardGuideRect  // 0x50
METHODS:
  RVA=0x032499E0  token=0x60001B9  System.Void UpdateCache(Cinemachine.LensSettings lens, UnityEngine.Rect softGuide, UnityEngine.Rect hardGuide, System.Single targetDistance)
  RVA=0x0434E5D0  token=0x60001BA  UnityEngine.Rect ScreenToFOV(UnityEngine.Rect rScreen, System.Single fov, System.Single fovH, System.Single aspect)
END_CLASS

CLASS: FramingMode
TYPE:  sealed struct
TOKEN: 0x2000047
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineFramingTransposer.FramingModeHorizontal  // const
  public    static  Cinemachine.CinemachineFramingTransposer.FramingModeVertical  // const
  public    static  Cinemachine.CinemachineFramingTransposer.FramingModeHorizontalAndVertical  // const
  public    static  Cinemachine.CinemachineFramingTransposer.FramingModeNone  // const
METHODS:
END_CLASS

CLASS: AdjustmentMode
TYPE:  sealed struct
TOKEN: 0x2000048
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineFramingTransposer.AdjustmentModeZoomOnly  // const
  public    static  Cinemachine.CinemachineFramingTransposer.AdjustmentModeDollyOnly  // const
  public    static  Cinemachine.CinemachineFramingTransposer.AdjustmentModeDollyThenZoom  // const
METHODS:
END_CLASS

CLASS: FramingMode
TYPE:  sealed struct
TOKEN: 0x200004A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineGroupComposer.FramingModeHorizontal  // const
  public    static  Cinemachine.CinemachineGroupComposer.FramingModeVertical  // const
  public    static  Cinemachine.CinemachineGroupComposer.FramingModeHorizontalAndVertical  // const
METHODS:
END_CLASS

CLASS: AdjustmentMode
TYPE:  sealed struct
TOKEN: 0x200004B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineGroupComposer.AdjustmentModeZoomOnly  // const
  public    static  Cinemachine.CinemachineGroupComposer.AdjustmentModeDollyOnly  // const
  public    static  Cinemachine.CinemachineGroupComposer.AdjustmentModeDollyThenZoom  // const
METHODS:
END_CLASS

CLASS: HeadingDefinition
TYPE:  sealed struct
TOKEN: 0x2000050
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineOrbitalTransposer.Heading.HeadingDefinitionPositionDelta  // const
  public    static  Cinemachine.CinemachineOrbitalTransposer.Heading.HeadingDefinitionVelocity  // const
  public    static  Cinemachine.CinemachineOrbitalTransposer.Heading.HeadingDefinitionTargetForward  // const
  public    static  Cinemachine.CinemachineOrbitalTransposer.Heading.HeadingDefinitionWorldForward  // const
METHODS:
END_CLASS

CLASS: Heading
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0x1C
FIELDS:
  public            Cinemachine.CinemachineOrbitalTransposer.Heading.HeadingDefinitionm_Definition  // 0x10
  public            System.Int32                    m_VelocityFilterStrength  // 0x14
  public            System.Single                   m_Bias  // 0x18
METHODS:
  RVA=0x04D87E60  token=0x60001F5  System.Void .ctor(Cinemachine.CinemachineOrbitalTransposer.Heading.HeadingDefinition def, System.Int32 filterStrength, System.Single bias)
END_CLASS

CLASS: UpdateHeadingDelegate
TYPE:  sealed class
TOKEN: 0x2000051
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x067A9BEC  token=0x60001F6  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x067A9BB4  token=0x60001F7  System.Single Invoke(Cinemachine.CinemachineOrbitalTransposer orbital, System.Single deltaTime, UnityEngine.Vector3 up)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000052
SIZE:  0x10
FIELDS:
  public    static readonly Cinemachine.CinemachineOrbitalTransposer.<>c<>9  // static @ 0x0
  public    static  Cinemachine.CinemachineOrbitalTransposer.UpdateHeadingDelegate<>9__30_0  // static @ 0x8
METHODS:
  RVA=0x067A9B50  token=0x60001F8  System.Void .cctor()
  RVA=0x041E1670  token=0x60001F9  System.Void .ctor()
  RVA=0x067A9A80  token=0x60001FA  System.Single <.ctor>b__30_0(Cinemachine.CinemachineOrbitalTransposer orbital, System.Single deltaTime, UnityEngine.Vector3 up)
END_CLASS

CLASS: RecenterTargetMode
TYPE:  sealed struct
TOKEN: 0x2000054
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachinePOV.RecenterTargetModeNone  // const
  public    static  Cinemachine.CinemachinePOV.RecenterTargetModeFollowTargetForward  // const
  public    static  Cinemachine.CinemachinePOV.RecenterTargetModeLookAtTargetForward  // const
METHODS:
END_CLASS

CLASS: CameraUpMode
TYPE:  sealed struct
TOKEN: 0x2000057
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineTrackedDolly.CameraUpModeDefault  // const
  public    static  Cinemachine.CinemachineTrackedDolly.CameraUpModePath  // const
  public    static  Cinemachine.CinemachineTrackedDolly.CameraUpModePathNoRoll  // const
  public    static  Cinemachine.CinemachineTrackedDolly.CameraUpModeFollowTarget  // const
  public    static  Cinemachine.CinemachineTrackedDolly.CameraUpModeFollowTargetNoRoll  // const
METHODS:
END_CLASS

CLASS: AutoDolly
TYPE:  sealed struct
TOKEN: 0x2000058
SIZE:  0x20
FIELDS:
  public            System.Boolean                  m_Enabled  // 0x10
  public            System.Single                   m_PositionOffset  // 0x14
  public            System.Int32                    m_SearchRadius  // 0x18
  public            System.Int32                    m_SearchResolution  // 0x1c
METHODS:
  RVA=0x04D87EF0  token=0x6000215  System.Void .ctor(System.Boolean enabled, System.Single positionOffset, System.Int32 searchRadius, System.Int32 stepsPerSegment)
END_CLASS

CLASS: BindingMode
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineTransposer.BindingModeLockToTargetOnAssign  // const
  public    static  Cinemachine.CinemachineTransposer.BindingModeLockToTargetWithWorldUp  // const
  public    static  Cinemachine.CinemachineTransposer.BindingModeLockToTargetNoRoll  // const
  public    static  Cinemachine.CinemachineTransposer.BindingModeLockToTarget  // const
  public    static  Cinemachine.CinemachineTransposer.BindingModeWorldSpace  // const
  public    static  Cinemachine.CinemachineTransposer.BindingModeSimpleFollowWithWorldUp  // const
METHODS:
END_CLASS

CLASS: AngularDampingMode
TYPE:  sealed struct
TOKEN: 0x200005B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineTransposer.AngularDampingModeEuler  // const
  public    static  Cinemachine.CinemachineTransposer.AngularDampingModeQuaternion  // const
METHODS:
END_CLASS

CLASS: SpeedMode
TYPE:  sealed struct
TOKEN: 0x200005D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.AxisState.SpeedMode MaxSpeed  // const
  public    static  Cinemachine.AxisState.SpeedMode InputValueGain  // const
METHODS:
END_CLASS

CLASS: IInputAxisProvider
TYPE:  interface
TOKEN: 0x200005E
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000232  System.Single GetAxisValue(System.Int32 axis)
END_CLASS

CLASS: Recentering
TYPE:  sealed struct
TOKEN: 0x200005F
SIZE:  0x2C
FIELDS:
  public            System.Boolean                  m_enabled  // 0x10
  public            System.Single                   m_WaitTime  // 0x14
  public            System.Single                   m_RecenteringTime  // 0x18
  private           System.Single                   mLastAxisInputTime  // 0x1c
  private           System.Single                   mRecenteringVelocity  // 0x20
  private           System.Int32                    m_LegacyHeadingDefinition  // 0x24
  private           System.Int32                    m_LegacyVelocityFilterStrength  // 0x28
METHODS:
  RVA=0x04D88080  token=0x6000233  System.Void .ctor(System.Boolean enabled, System.Single waitTime, System.Single recenteringTime)
  RVA=0x067B1C84  token=0x6000234  System.Void Validate()
  RVA=0x067B1A6C  token=0x6000235  System.Void CancelRecentering()
  RVA=0x067B1AC0  token=0x6000236  System.Void DoRecentering(Cinemachine.AxisState& axis, System.Single deltaTime, System.Single recenterTarget)
  RVA=0x067B1C5C  token=0x6000237  System.Boolean LegacyUpgrade(System.Int32& heading, System.Int32& velocityFilter)
END_CLASS

CLASS: HGDOFParams
TYPE:  sealed struct
TOKEN: 0x2000061
SIZE:  0x30
FIELDS:
  public            System.Boolean                  enabledForVirtualCamera  // 0x10
  public            System.Boolean                  enable  // 0x11
  public            System.Single                   nearRadius  // 0x14
  public            System.Single                   nearFocusStart  // 0x18
  public            System.Single                   nearFocusEnd  // 0x1c
  public            System.Single                   farRadius  // 0x20
  public            System.Single                   farFocusStart  // 0x24
  public            System.Single                   farFocusEnd  // 0x28
  public            System.Single                   temporalFactor  // 0x2c
METHODS:
END_CLASS

CLASS: BlendHintValue
TYPE:  sealed struct
TOKEN: 0x2000062
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CameraState.BlendHintValueNothing  // const
  public    static  Cinemachine.CameraState.BlendHintValueNoPosition  // const
  public    static  Cinemachine.CameraState.BlendHintValueNoOrientation  // const
  public    static  Cinemachine.CameraState.BlendHintValueNoTransform  // const
  public    static  Cinemachine.CameraState.BlendHintValueSphericalPositionBlend  // const
  public    static  Cinemachine.CameraState.BlendHintValueCylindricalPositionBlend  // const
  public    static  Cinemachine.CameraState.BlendHintValueRadialAimBlend  // const
  public    static  Cinemachine.CameraState.BlendHintValueIgnoreLookAtTarget  // const
  public    static  Cinemachine.CameraState.BlendHintValueNoLens  // const
METHODS:
END_CLASS

CLASS: CustomBlendable
TYPE:  sealed struct
TOKEN: 0x2000063
SIZE:  0x20
FIELDS:
  public            UnityEngine.Object              m_Custom  // 0x10
  public            System.Single                   m_Weight  // 0x18
METHODS:
  RVA=0x067B1770  token=0x6000249  System.Void .ctor(UnityEngine.Object custom, System.Single weight)
END_CLASS

CLASS: Style
TYPE:  sealed struct
TOKEN: 0x2000066
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineBlendDefinition.StyleCut  // const
  public    static  Cinemachine.CinemachineBlendDefinition.StyleEaseInOut  // const
  public    static  Cinemachine.CinemachineBlendDefinition.StyleEaseIn  // const
  public    static  Cinemachine.CinemachineBlendDefinition.StyleEaseOut  // const
  public    static  Cinemachine.CinemachineBlendDefinition.StyleHardIn  // const
  public    static  Cinemachine.CinemachineBlendDefinition.StyleHardOut  // const
  public    static  Cinemachine.CinemachineBlendDefinition.StyleLinear  // const
  public    static  Cinemachine.CinemachineBlendDefinition.StyleCustom  // const
METHODS:
END_CLASS

CLASS: CustomBlend
TYPE:  sealed struct
TOKEN: 0x200006A
SIZE:  0x38
FIELDS:
  public            System.String                   m_From  // 0x10
  public            System.String                   m_To  // 0x18
  public            Cinemachine.CinemachineBlendDefinitionm_Blend  // 0x20
METHODS:
END_CLASS

CLASS: CustomBlendGroup
TYPE:  class
TOKEN: 0x200006B
SIZE:  0x78
FIELDS:
  public            System.Collections.Generic.List<System.String>cameras  // 0x10
  public            Cinemachine.CinemachineBlendDefinitionfromSameCamera  // 0x18
  public            Cinemachine.CinemachineBlendDefinitioninsideGroup  // 0x30
  public            Cinemachine.CinemachineBlendDefinitiontoGroup  // 0x48
  public            Cinemachine.CinemachineBlendDefinitionfromGroup  // 0x60
METHODS:
  RVA=0x041E1670  token=0x6000278  System.Void .ctor()
END_CLASS

CLASS: Stage
TYPE:  sealed struct
TOKEN: 0x200006E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineCore.StageBody  // const
  public    static  Cinemachine.CinemachineCore.StageAim  // const
  public    static  Cinemachine.CinemachineCore.StageNoise  // const
  public    static  Cinemachine.CinemachineCore.StageFinalize  // const
METHODS:
END_CLASS

CLASS: AxisInputDelegate
TYPE:  sealed class
TOKEN: 0x200006F
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x03FCFEE0  token=0x60002AB  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014E2060  token=0x60002AC  System.Single Invoke(System.String axisName)
END_CLASS

CLASS: GetBlendOverrideDelegate
TYPE:  sealed class
TOKEN: 0x2000070
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0394E680  token=0x60002AD  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x067B178C  token=0x60002AE  Cinemachine.CinemachineBlendDefinition Invoke(Cinemachine.ICinemachineCamera fromVcam, Cinemachine.ICinemachineCamera toVcam, Cinemachine.CinemachineBlendDefinition defaultBlend, UnityEngine.MonoBehaviour owner)
END_CLASS

CLASS: UpdateStatus
TYPE:  class
TOKEN: 0x2000071
SIZE:  0x20
FIELDS:
  public            System.Int32                    lastUpdateFrame  // 0x10
  public            System.Int32                    lastUpdateFixedFrame  // 0x14
  public            Cinemachine.UpdateTracker.UpdateClocklastUpdateMode  // 0x18
  public            System.Single                   lastUpdateDeltaTime  // 0x1c
METHODS:
  RVA=0x041E1670  token=0x60002AF  System.Void .ctor()
END_CLASS

CLASS: UpdateFilter
TYPE:  sealed struct
TOKEN: 0x2000072
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineCore.UpdateFilterFixed  // const
  public    static  Cinemachine.CinemachineCore.UpdateFilterLate  // const
  public    static  Cinemachine.CinemachineCore.UpdateFilterSmart  // const
  public    static  Cinemachine.CinemachineCore.UpdateFilterSmartFixed  // const
  public    static  Cinemachine.CinemachineCore.UpdateFilterSmartLate  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000073
SIZE:  0x10
FIELDS:
  public    static readonly Cinemachine.CinemachineCore.<>c <>9  // static @ 0x0
  public    static  System.Comparison<Cinemachine.CinemachineVirtualCameraBase><>9__30_0  // static @ 0x8
METHODS:
  RVA=0x067B272C  token=0x60002B0  System.Void .cctor()
  RVA=0x041E1670  token=0x60002B1  System.Void .ctor()
  RVA=0x067B263C  token=0x60002B2  System.Int32 <GetVirtualCamera>b__30_0(Cinemachine.CinemachineVirtualCameraBase x, Cinemachine.CinemachineVirtualCameraBase y)
END_CLASS

CLASS: Appearance
TYPE:  class
TOKEN: 0x2000076
SIZE:  0x38
FIELDS:
  public            UnityEngine.Color               pathColor  // 0x10
  public            UnityEngine.Color               inactivePathColor  // 0x20
  public            System.Single                   width  // 0x30
METHODS:
  RVA=0x067AA0EC  token=0x60002DB  System.Void .ctor()
END_CLASS

CLASS: PositionUnits
TYPE:  sealed struct
TOKEN: 0x2000077
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachinePathBase.PositionUnitsPathUnits  // const
  public    static  Cinemachine.CinemachinePathBase.PositionUnitsDistance  // const
  public    static  Cinemachine.CinemachinePathBase.PositionUnitsNormalized  // const
METHODS:
END_CLASS

CLASS: Level
TYPE:  sealed struct
TOKEN: 0x2000082
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.DocumentationSortingAttribute.LevelUndoc  // const
  public    static  Cinemachine.DocumentationSortingAttribute.LevelAPI  // const
  public    static  Cinemachine.DocumentationSortingAttribute.LevelUserRef  // const
METHODS:
END_CLASS

CLASS: StandbyUpdateMode
TYPE:  sealed struct
TOKEN: 0x2000084
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineVirtualCameraBase.StandbyUpdateModeNever  // const
  public    static  Cinemachine.CinemachineVirtualCameraBase.StandbyUpdateModeAlways  // const
  public    static  Cinemachine.CinemachineVirtualCameraBase.StandbyUpdateModeRoundRobin  // const
METHODS:
END_CLASS

CLASS: BlendHint
TYPE:  sealed struct
TOKEN: 0x2000085
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineVirtualCameraBase.BlendHintNone  // const
  public    static  Cinemachine.CinemachineVirtualCameraBase.BlendHintSphericalPosition  // const
  public    static  Cinemachine.CinemachineVirtualCameraBase.BlendHintCylindricalPosition  // const
  public    static  Cinemachine.CinemachineVirtualCameraBase.BlendHintScreenSpaceAimWhenTargetsDiffer  // const
METHODS:
END_CLASS

CLASS: TransitionParams
TYPE:  sealed struct
TOKEN: 0x2000086
SIZE:  0x20
FIELDS:
  public            Cinemachine.CinemachineVirtualCameraBase.BlendHintm_BlendHint  // 0x10
  public            System.Boolean                  m_InheritPosition  // 0x14
  public            Cinemachine.CinemachineBrain.VcamActivatedEventm_OnCameraLive  // 0x18
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000087
SIZE:  0x10
FIELDS:
  public    static readonly Cinemachine.CinemachineVirtualCameraBase.<>c<>9  // static @ 0x0
  public    static  System.Func<Cinemachine.CinemachineExtension,System.Boolean><>9__67_0  // static @ 0x8
METHODS:
  RVA=0x067B26C8  token=0x6000329  System.Void .cctor()
  RVA=0x041E1670  token=0x600032A  System.Void .ctor()
  RVA=0x067B2670  token=0x600032B  System.Boolean <RequiresUserInput>b__67_0(Cinemachine.CinemachineExtension extension)
END_CLASS

CLASS: BakedSolution
TYPE:  class
TOKEN: 0x2000089
SIZE:  0x40
FIELDS:
  private   readonly System.Single                   <FrustumHeight>k__BackingField  // 0x10
  private           System.Single                   m_frustumSizeIntSpace  // 0x14
  private   readonly Cinemachine.ConfinerOven.AspectStretcherm_AspectStretcher  // 0x18
  private   readonly System.Boolean                  m_HasBones  // 0x24
  private   readonly System.Double                   m_SqrPolygonDiagonal  // 0x28
  private           System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>m_OriginalPolygon  // 0x30
  public            System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>m_Solution  // 0x38
PROPERTIES:
  FrustumHeight  get=0x04D85F70
METHODS:
  RVA=0x067AB2F0  token=0x6000335  System.Void .ctor(System.Single aspectRatio, System.Single frustumHeight, System.Boolean hasBones, UnityEngine.Rect polygonBounds, System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>> originalPolygon, System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>> solution)
  RVA=0x067AB2C8  token=0x6000336  System.Boolean IsValid(System.Single frustumHeight)
  RVA=0x067AA9A0  token=0x6000337  UnityEngine.Vector2 ConfinePoint(UnityEngine.Vector2& pointToConfine)
  RVA=0x067AB22C  token=0x6000338  System.Boolean IsInsideOriginal(ClipperLib.IntPoint p)
  RVA=0x067AA920  token=0x6000339  System.Single ClosestPointOnSegment(ClipperLib.IntPoint p, ClipperLib.IntPoint s0, ClipperLib.IntPoint s1)
  RVA=0x067AB198  token=0x600033A  ClipperLib.IntPoint IntPointLerp(ClipperLib.IntPoint a, ClipperLib.IntPoint b, System.Single lerp)
  RVA=0x067AAD74  token=0x600033B  System.Boolean DoesIntersectOriginal(ClipperLib.IntPoint l1, ClipperLib.IntPoint l2)
  RVA=0x067AAF74  token=0x600033C  System.Int32 FindIntersection(ClipperLib.IntPoint& p1, ClipperLib.IntPoint& p2, ClipperLib.IntPoint& p3, ClipperLib.IntPoint& p4)
  RVA=0x04D87F40  token=0x600033D  System.Double IntPointDiffSqrMagnitude(ClipperLib.IntPoint p1, ClipperLib.IntPoint p2)
END_CLASS

CLASS: AspectStretcher
TYPE:  sealed struct
TOKEN: 0x200008A
SIZE:  0x1C
FIELDS:
  private   readonly System.Single                   <Aspect>k__BackingField  // 0x10
  private   readonly System.Single                   m_InverseAspect  // 0x14
  private   readonly System.Single                   m_CenterX  // 0x18
PROPERTIES:
  Aspect  get=0x04D871D0
METHODS:
  RVA=0x04D87ED0  token=0x600033F  System.Void .ctor(System.Single aspect, System.Single centerX)
  RVA=0x04D87E70  token=0x6000340  UnityEngine.Vector2 Stretch(UnityEngine.Vector2 p)
  RVA=0x04D87EA0  token=0x6000341  UnityEngine.Vector2 Unstretch(UnityEngine.Vector2 p)
END_CLASS

CLASS: PolygonSolution
TYPE:  sealed struct
TOKEN: 0x200008B
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>m_Polygons  // 0x10
  public            System.Single                   m_FrustumHeight  // 0x18
PROPERTIES:
  IsEmpty  get=0x0314CD60
METHODS:
  RVA=0x067B1998  token=0x6000342  System.Boolean StateChanged(System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>& paths)
END_CLASS

CLASS: BakingState
TYPE:  sealed struct
TOKEN: 0x200008C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.ConfinerOven.BakingStateBAKING  // const
  public    static  Cinemachine.ConfinerOven.BakingStateBAKED  // const
  public    static  Cinemachine.ConfinerOven.BakingStateTIMEOUT  // const
METHODS:
END_CLASS

CLASS: BakingStateCache
TYPE:  sealed struct
TOKEN: 0x200008D
SIZE:  0x58
FIELDS:
  public            ClipperLib.ClipperOffset        offsetter  // 0x10
  public            System.Collections.Generic.List<Cinemachine.ConfinerOven.PolygonSolution>solutions  // 0x18
  public            Cinemachine.ConfinerOven.PolygonSolutionrightCandidate  // 0x20
  public            Cinemachine.ConfinerOven.PolygonSolutionleftCandidate  // 0x30
  public            System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>maxCandidate  // 0x40
  public            System.Single                   stepSize  // 0x48
  public            System.Single                   maxFrustumHeight  // 0x4c
  public            System.Single                   currentFrustumHeight  // 0x50
  public            System.Single                   bakeTime  // 0x54
METHODS:
END_CLASS

CLASS: OverrideModes
TYPE:  sealed struct
TOKEN: 0x2000090
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.LensSettings.OverrideModesNone  // const
  public    static  Cinemachine.LensSettings.OverrideModesOrthographic  // const
  public    static  Cinemachine.LensSettings.OverrideModesPerspective  // const
  public    static  Cinemachine.LensSettings.OverrideModesPhysical  // const
METHODS:
END_CLASS

CLASS: NoiseParams
TYPE:  sealed struct
TOKEN: 0x2000092
SIZE:  0x1C
FIELDS:
  public            System.Single                   Frequency  // 0x10
  public            System.Single                   Amplitude  // 0x14
  public            System.Boolean                  Constant  // 0x18
METHODS:
  RVA=0x03C01300  token=0x600035D  System.Single GetValueAt(System.Single time, System.Single timeOffset)
END_CLASS

CLASS: TransformNoiseParams
TYPE:  sealed struct
TOKEN: 0x2000093
SIZE:  0x34
FIELDS:
  public            Cinemachine.NoiseSettings.NoiseParamsX  // 0x10
  public            Cinemachine.NoiseSettings.NoiseParamsY  // 0x1c
  public            Cinemachine.NoiseSettings.NoiseParamsZ  // 0x28
METHODS:
  RVA=0x03C01260  token=0x600035E  UnityEngine.Vector3 GetValueAt(System.Single time, UnityEngine.Vector3 timeOffsets)
END_CLASS

CLASS: OverlapSphereNonAllocDelegate
TYPE:  sealed class
TOKEN: 0x2000095
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0454E620  token=0x600036A  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x062248AC  token=0x600036B  System.Int32 Invoke(UnityEngine.Vector3 position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
END_CLASS

CLASS: RaycastDelegate
TYPE:  sealed class
TOKEN: 0x2000096
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0454E590  token=0x600036C  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x06221374  token=0x600036D  System.Boolean Invoke(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
END_CLASS

CLASS: RaycastNonAllocDelegate
TYPE:  sealed class
TOKEN: 0x2000097
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0454E500  token=0x600036E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x06221374  token=0x600036F  System.Int32 Invoke(UnityEngine.Ray ray, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
END_CLASS

CLASS: SphereCastNonAllocDelegate
TYPE:  sealed class
TOKEN: 0x2000098
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0454E470  token=0x6000370  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x06225970  token=0x6000371  System.Int32 Invoke(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
END_CLASS

CLASS: ComputePenetrationDelegate
TYPE:  sealed class
TOKEN: 0x2000099
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0454E3E0  token=0x6000372  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x06220F5C  token=0x6000373  System.Boolean Invoke(UnityEngine.ECSColliderResultProxy colliderA, UnityEngine.Vector3 positionA, UnityEngine.Quaternion rotationA, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
END_CLASS

CLASS: Mode
TYPE:  sealed struct
TOKEN: 0x200009D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.TargetPositionCache.ModeDisabled  // const
  public    static  Cinemachine.TargetPositionCache.ModeRecord  // const
  public    static  Cinemachine.TargetPositionCache.ModePlayback  // const
METHODS:
END_CLASS

CLASS: Item
TYPE:  sealed struct
TOKEN: 0x200009F
SIZE:  0x2C
FIELDS:
  public            UnityEngine.Vector3             Pos  // 0x10
  public            UnityEngine.Quaternion          Rot  // 0x1c
PROPERTIES:
  Empty  get=0x067B1904
METHODS:
  RVA=0x067B17FC  token=0x600037E  Cinemachine.TargetPositionCache.CacheCurve.Item Lerp(Cinemachine.TargetPositionCache.CacheCurve.Item a, Cinemachine.TargetPositionCache.CacheCurve.Item b, System.Single t)
END_CLASS

CLASS: CacheCurve
TYPE:  class
TOKEN: 0x200009E
SIZE:  0x20
FIELDS:
  public            System.Single                   StartTime  // 0x10
  public            System.Single                   StepSize  // 0x14
  private           System.Collections.Generic.List<Cinemachine.TargetPositionCache.CacheCurve.Item>m_Cache  // 0x18
METHODS:
  RVA=0x067AB550  token=0x600037D  Cinemachine.TargetPositionCache.CacheCurve.Item Evaluate(System.Single time)
END_CLASS

CLASS: RecordingItem
TYPE:  sealed struct
TOKEN: 0x20000A1
SIZE:  0x34
FIELDS:
  public            System.Single                   Time  // 0x10
  public            System.Boolean                  IsCut  // 0x14
  public            Cinemachine.TargetPositionCache.CacheCurve.ItemItem  // 0x18
METHODS:
END_CLASS

CLASS: CacheEntry
TYPE:  class
TOKEN: 0x20000A0
SIZE:  0x20
FIELDS:
  public            Cinemachine.TargetPositionCache.CacheCurveCurve  // 0x10
  private           System.Collections.Generic.List<Cinemachine.TargetPositionCache.CacheEntry.RecordingItem>RawItems  // 0x18
METHODS:
  RVA=0x067AB6F0  token=0x6000380  System.Void AddRawItem(System.Single time, System.Boolean isCut, UnityEngine.Transform target)
  RVA=0x067AB988  token=0x6000381  System.Void .ctor()
END_CLASS

CLASS: TimeRange
TYPE:  sealed struct
TOKEN: 0x20000A2
SIZE:  0x18
FIELDS:
  public            System.Single                   Start  // 0x10
  public            System.Single                   End  // 0x14
PROPERTIES:
  IsEmpty  get=0x067B2630
  Empty  get=0x04D880A0
METHODS:
  RVA=0x067B25FC  token=0x6000383  System.Boolean Contains(System.Single time)
  RVA=0x067B2610  token=0x6000385  System.Void Include(System.Single time)
END_CLASS

CLASS: UpdateClock
TYPE:  sealed struct
TOKEN: 0x20000A4
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.UpdateTracker.UpdateClockFixed  // const
  public    static  Cinemachine.UpdateTracker.UpdateClockLate  // const
METHODS:
END_CLASS

CLASS: UpdateStatus
TYPE:  class
TOKEN: 0x20000A5
SIZE:  0x68
FIELDS:
  private           System.Int32                    windowStart  // 0x10
  private           System.Int32                    numWindowLateUpdateMoves  // 0x14
  private           System.Int32                    numWindowFixedUpdateMoves  // 0x18
  private           System.Int32                    numWindows  // 0x1c
  private           System.Int32                    lastFrameUpdated  // 0x20
  private           UnityEngine.Matrix4x4           lastPos  // 0x24
  private           Cinemachine.UpdateTracker.UpdateClock<PreferredUpdate>k__BackingField  // 0x64
PROPERTIES:
  PreferredUpdate  get=0x04D88110  set=0x04D88120
METHODS:
  RVA=0x067B87C0  token=0x600038D  System.Void .ctor(System.Int32 currentFrame, UnityEngine.Matrix4x4 pos)
  RVA=0x067B86B0  token=0x600038E  System.Void OnUpdate(System.Int32 currentFrame, Cinemachine.UpdateTracker.UpdateClock currentClock, UnityEngine.Matrix4x4 pos)
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x20000A7
SIZE:  0x18
FIELDS:
  public            UnityEngine.InputSystem.InputActionReferenceaRef  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000394  System.Void .ctor()
  RVA=0x067B8068  token=0x6000395  System.Boolean <ResolveForPlayer>b__1(UnityEngine.InputSystem.InputAction x)
END_CLASS

CLASS: Mode
TYPE:  sealed struct
TOKEN: 0x20000AA
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineTriggerAction.ActionSettings.ModeCustom  // const
  public    static  Cinemachine.CinemachineTriggerAction.ActionSettings.ModePriorityBoost  // const
  public    static  Cinemachine.CinemachineTriggerAction.ActionSettings.ModeActivate  // const
  public    static  Cinemachine.CinemachineTriggerAction.ActionSettings.ModeDeactivate  // const
  public    static  Cinemachine.CinemachineTriggerAction.ActionSettings.ModeEnable  // const
  public    static  Cinemachine.CinemachineTriggerAction.ActionSettings.ModeDisable  // const
  public    static  Cinemachine.CinemachineTriggerAction.ActionSettings.ModePlay  // const
  public    static  Cinemachine.CinemachineTriggerAction.ActionSettings.ModeStop  // const
METHODS:
END_CLASS

CLASS: TriggerEvent
TYPE:  class
TOKEN: 0x20000AB
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent
FIELDS:
METHODS:
  RVA=0x05393538  token=0x60003A5  System.Void .ctor()
END_CLASS

CLASS: TimeMode
TYPE:  sealed struct
TOKEN: 0x20000AC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineTriggerAction.ActionSettings.TimeModeFromStart  // const
  public    static  Cinemachine.CinemachineTriggerAction.ActionSettings.TimeModeFromEnd  // const
  public    static  Cinemachine.CinemachineTriggerAction.ActionSettings.TimeModeBeforeNow  // const
  public    static  Cinemachine.CinemachineTriggerAction.ActionSettings.TimeModeAfterNow  // const
METHODS:
END_CLASS

CLASS: ActionSettings
TYPE:  sealed struct
TOKEN: 0x20000A9
SIZE:  0x38
FIELDS:
  public            Cinemachine.CinemachineTriggerAction.ActionSettings.Modem_Action  // 0x10
  public            UnityEngine.Object              m_Target  // 0x18
  public            System.Int32                    m_BoostAmount  // 0x20
  public            System.Single                   m_StartTime  // 0x24
  public            Cinemachine.CinemachineTriggerAction.ActionSettings.TimeModem_Mode  // 0x28
  public            Cinemachine.CinemachineTriggerAction.ActionSettings.TriggerEventm_Event  // 0x30
METHODS:
  RVA=0x067B3140  token=0x60003A3  System.Void .ctor(Cinemachine.CinemachineTriggerAction.ActionSettings.Mode action)
  RVA=0x067B2CDC  token=0x60003A4  System.Void Invoke()
END_CLASS

CLASS: ImpulseShapes
TYPE:  sealed struct
TOKEN: 0x20000B1
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineImpulseDefinition.ImpulseShapesCustom  // const
  public    static  Cinemachine.CinemachineImpulseDefinition.ImpulseShapesRecoil  // const
  public    static  Cinemachine.CinemachineImpulseDefinition.ImpulseShapesBump  // const
  public    static  Cinemachine.CinemachineImpulseDefinition.ImpulseShapesExplosion  // const
  public    static  Cinemachine.CinemachineImpulseDefinition.ImpulseShapesRumble  // const
METHODS:
END_CLASS

CLASS: ImpulseTypes
TYPE:  sealed struct
TOKEN: 0x20000B2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineImpulseDefinition.ImpulseTypesUniform  // const
  public    static  Cinemachine.CinemachineImpulseDefinition.ImpulseTypesDissipating  // const
  public    static  Cinemachine.CinemachineImpulseDefinition.ImpulseTypesPropagating  // const
  public    static  Cinemachine.CinemachineImpulseDefinition.ImpulseTypesLegacy  // const
METHODS:
END_CLASS

CLASS: RepeatMode
TYPE:  sealed struct
TOKEN: 0x20000B3
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineImpulseDefinition.RepeatModeStretch  // const
  public    static  Cinemachine.CinemachineImpulseDefinition.RepeatModeLoop  // const
METHODS:
END_CLASS

CLASS: SignalSource
TYPE:  class
TOKEN: 0x20000B4
SIZE:  0x28
IMPLEMENTS: Cinemachine.ISignalSource6D
FIELDS:
  private           Cinemachine.CinemachineImpulseDefinitionm_Def  // 0x10
  private           UnityEngine.Vector3             m_Velocity  // 0x18
PROPERTIES:
  SignalDuration  get=0x067B70D8
METHODS:
  RVA=0x067B70A8  token=0x60003C3  System.Void .ctor(Cinemachine.CinemachineImpulseDefinition def, UnityEngine.Vector3 velocity)
  RVA=0x067B6FD0  token=0x60003C5  System.Void GetSignal(System.Single timeSinceSignalStart, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot)
END_CLASS

CLASS: LegacySignalSource
TYPE:  class
TOKEN: 0x20000B5
SIZE:  0x28
IMPLEMENTS: Cinemachine.ISignalSource6D
FIELDS:
  private           Cinemachine.CinemachineImpulseDefinitionm_Def  // 0x10
  private           UnityEngine.Vector3             m_Velocity  // 0x18
  private           System.Single                   m_StartTimeOffset  // 0x24
PROPERTIES:
  SignalDuration  get=0x033CD640
METHODS:
  RVA=0x033CEB50  token=0x60003C6  System.Void .ctor(Cinemachine.CinemachineImpulseDefinition def, UnityEngine.Vector3 velocity)
  RVA=0x033CD430  token=0x60003C8  System.Void GetSignal(System.Single timeSinceSignalStart, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot)
END_CLASS

CLASS: ImpulseReaction
TYPE:  sealed struct
TOKEN: 0x20000B7
SIZE:  0x40
FIELDS:
  public            Cinemachine.NoiseSettings       m_SecondaryNoise  // 0x10
  public            System.Single                   m_AmplitudeGain  // 0x18
  public            System.Single                   m_FrequencyGain  // 0x1c
  public            System.Single                   m_Duration  // 0x20
  private           System.Single                   m_CurrentAmount  // 0x24
  private           System.Single                   m_CurrentTime  // 0x28
  private           System.Single                   m_CurrentDamping  // 0x2c
  private           System.Boolean                  m_Initialized  // 0x30
  private           UnityEngine.Vector3             m_NoiseOffsets  // 0x34
METHODS:
  RVA=0x067B6CE0  token=0x60003CC  System.Void ReSeed()
  RVA=0x03E32D20  token=0x60003CD  System.Boolean GetReaction(System.Single deltaTime, UnityEngine.Vector3 impulsePos, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot)
END_CLASS

CLASS: EnvelopeDefinition
TYPE:  sealed struct
TOKEN: 0x20000BA
SIZE:  0x30
FIELDS:
  public            UnityEngine.AnimationCurve      m_AttackShape  // 0x10
  public            UnityEngine.AnimationCurve      m_DecayShape  // 0x18
  public            System.Single                   m_AttackTime  // 0x20
  public            System.Single                   m_SustainTime  // 0x24
  public            System.Single                   m_DecayTime  // 0x28
  public            System.Boolean                  m_ScaleWithImpact  // 0x2c
  public            System.Boolean                  m_HoldForever  // 0x2d
PROPERTIES:
  Duration  get=0x033CD220
METHODS:
  RVA=0x04D880C0  token=0x60003D8  Cinemachine.CinemachineImpulseManager.EnvelopeDefinition Default()
  RVA=0x033CD2B0  token=0x60003DA  System.Single GetValueAt(System.Single offset)
  RVA=0x067B6378  token=0x60003DB  System.Void ChangeStopTime(System.Single offset, System.Boolean forceNoDecay)
  RVA=0x033CEB20  token=0x60003DC  System.Void Clear()
  RVA=0x067B63A4  token=0x60003DD  System.Void Validate()
END_CLASS

CLASS: DirectionMode
TYPE:  sealed struct
TOKEN: 0x20000BC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineImpulseManager.ImpulseEvent.DirectionModeFixed  // const
  public    static  Cinemachine.CinemachineImpulseManager.ImpulseEvent.DirectionModeRotateTowardSource  // const
METHODS:
END_CLASS

CLASS: DissipationMode
TYPE:  sealed struct
TOKEN: 0x20000BD
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Cinemachine.CinemachineImpulseManager.ImpulseEvent.DissipationModeLinearDecay  // const
  public    static  Cinemachine.CinemachineImpulseManager.ImpulseEvent.DissipationModeSoftDecay  // const
  public    static  Cinemachine.CinemachineImpulseManager.ImpulseEvent.DissipationModeExponentialDecay  // const
METHODS:
END_CLASS

CLASS: ImpulseEvent
TYPE:  class
TOKEN: 0x20000BB
SIZE:  0x70
FIELDS:
  public            System.Single                   m_StartTime  // 0x10
  public            Cinemachine.CinemachineImpulseManager.EnvelopeDefinitionm_Envelope  // 0x18
  public            Cinemachine.ISignalSource6D     m_SignalSource  // 0x38
  public            UnityEngine.Vector3             m_Position  // 0x40
  public            System.Single                   m_Radius  // 0x4c
  public            Cinemachine.CinemachineImpulseManager.ImpulseEvent.DirectionModem_DirectionMode  // 0x50
  public            System.Int32                    m_Channel  // 0x54
  public            Cinemachine.CinemachineImpulseManager.ImpulseEvent.DissipationModem_DissipationMode  // 0x58
  public            System.Single                   m_DissipationDistance  // 0x5c
  public            System.Single                   m_CustomDissipation  // 0x60
  public            System.Single                   m_PropagationSpeed  // 0x64
  public            System.Boolean                  m_IgnoreDistance  // 0x68
PROPERTIES:
  Expired  get=0x033CD100
METHODS:
  RVA=0x067B6CC8  token=0x60003DF  System.Void Cancel(System.Single time, System.Boolean forceNoDecay)
  RVA=0x033CD280  token=0x60003E0  System.Single DistanceDecay(System.Single distance)
  RVA=0x033CCC40  token=0x60003E1  System.Boolean GetDecayedSignal(UnityEngine.Vector3 listenerPosition, System.Boolean use2D, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot)
  RVA=0x033CEAC0  token=0x60003E2  System.Void Clear()
  RVA=0x041E1670  token=0x60003E3  System.Void .ctor()
END_CLASS

CLASS: OnGUIDelegate
TYPE:  sealed class
TOKEN: 0x20000C3
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x02F28650  token=0x60003FA  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x60003FB  System.Void Invoke()
END_CLASS

CLASS: Item
TYPE:  sealed struct
TOKEN: 0x20000C7
SIZE:  0x24
FIELDS:
  public            UnityEngine.Vector3             velocity  // 0x10
  public            System.Single                   weight  // 0x1c
  public            System.Single                   time  // 0x20
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=12
TYPE:  sealed struct
TOKEN: 0x20000CC
SIZE:  0x1C
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x20000CB
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=12AA5072B96927F710758B3E5237A84AC68E397BC4E1BF7E864A950696D743CC32  // static @ 0x0
METHODS:
END_CLASS

CLASS: Cinemachine.Cinemachine3rdPersonAim
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x58
EXTENDS: Cinemachine.CinemachineExtension
FIELDS:
  public            UnityEngine.LayerMask           AimCollisionFilter  // 0x28
  public            System.String                   IgnoreTag  // 0x30
  public            System.Single                   AimDistance  // 0x38
  public            UnityEngine.RectTransform       AimTargetReticle  // 0x40
  private           UnityEngine.Vector3             <AimTarget>k__BackingField  // 0x48
PROPERTIES:
  AimTarget  get=0x04D87680  set=0x04D876A0
METHODS:
  RVA=0x0678BEE8  token=0x600001C  System.Void OnValidate()
  RVA=0x0678C108  token=0x600001D  System.Void Reset()
  RVA=0x0678BDBC  token=0x600001E  System.Boolean OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x0678BC00  token=0x600001F  System.Void DrawReticle(Cinemachine.CinemachineBrain brain)
  RVA=0x0678B8FC  token=0x6000020  UnityEngine.Vector3 ComputeLookAtPoint(UnityEngine.Vector3 camPos, UnityEngine.Transform player)
  RVA=0x0678B770  token=0x6000021  UnityEngine.Vector3 ComputeAimTarget(UnityEngine.Vector3 cameraLookAt, UnityEngine.Transform player)
  RVA=0x0678BF00  token=0x6000022  System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x0678C16C  token=0x6000023  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineBlendListCamera
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x210
EXTENDS: Cinemachine.CinemachineVirtualCameraBase
FIELDS:
  public            UnityEngine.Transform           m_LookAt  // 0xa8
  public            UnityEngine.Transform           m_Follow  // 0xb0
  public            System.Boolean                  m_ShowDebugText  // 0xb8
  public            System.Boolean                  m_Loop  // 0xb9
  private           Cinemachine.CinemachineVirtualCameraBase[]m_ChildCameras  // 0xc0
  public            Cinemachine.CinemachineBlendListCamera.Instruction[]m_Instructions  // 0xc8
  private           Cinemachine.ICinemachineCamera  <LiveChild>k__BackingField  // 0xd0
  private           Cinemachine.ICinemachineCamera  m_TransitioningFrom  // 0xd8
  private           Cinemachine.CameraState         m_State  // 0xe0
  private           System.Single                   mActivationTime  // 0x200
  private           System.Int32                    mCurrentInstruction  // 0x204
  private           Cinemachine.CinemachineBlend    mActiveBlend  // 0x208
PROPERTIES:
  Description  get=0x0678D6A4
  LiveChild  get=0x04D876B0  set=0x0678D7BC
  State  get=0x04D876C0
  LookAt  get=0x0678D7AC  set=0x04D84CC0
  Follow  get=0x0678D790  set=0x04D84CE0
  ChildCameras  get=0x0678D684
  IsBlending  get=0x0678D7A0
METHODS:
  RVA=0x0678D1C4  token=0x6000025  System.Void Reset()
  RVA=0x0678CB48  token=0x6000028  System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly)
  RVA=0x0678CFD0  token=0x600002E  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x0678C44C  token=0x600002F  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x0678D09C  token=0x6000030  System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x0678C52C  token=0x6000031  System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x0678CC88  token=0x6000032  System.Void OnEnable()
  RVA=0x0678CB70  token=0x6000033  System.Void OnDisable()
  RVA=0x0678D094  token=0x6000034  System.Void OnTransformChildrenChanged()
  RVA=0x0678CE8C  token=0x6000035  System.Void OnGuiHandler()
  RVA=0x0678CB14  token=0x6000038  System.Void InvalidateListOfChildren()
  RVA=0x0678D228  token=0x6000039  System.Void UpdateListOfChildren()
  RVA=0x0678D3C8  token=0x600003A  System.Void ValidateInstructions()
  RVA=0x0678C248  token=0x600003B  System.Void AdvanceCurrentInstruction(System.Single deltaTime)
  RVA=0x0678D54C  token=0x600003C  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineBrain
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x1D0
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Boolean                  m_ShowDebugText  // 0x18
  public            System.Boolean                  m_ShowCameraFrustum  // 0x19
  public            System.Boolean                  m_IgnoreTimeScale  // 0x1a
  public            UnityEngine.Transform           m_WorldUpOverride  // 0x20
  public            Cinemachine.CinemachineBrain.UpdateMethodm_UpdateMethod  // 0x28
  public            Cinemachine.CinemachineBrain.BrainUpdateMethodm_BlendUpdateMethod  // 0x2c
  public            Cinemachine.CinemachineBlendDefinitionm_DefaultBlend  // 0x30
  public            Cinemachine.CinemachineBlenderSettingsm_CustomBlends  // 0x48
  private           UnityEngine.Camera              m_OutputCamera  // 0x50
  private           HG.Rendering.Runtime.HGDepthOfFieldm_OutputDOF  // 0x58
  public            Cinemachine.CinemachineBrain.BrainEventm_CameraCutEvent  // 0x60
  public            Cinemachine.CinemachineBrain.VcamActivatedEventm_CameraActivatedEvent  // 0x68
  private   static  Cinemachine.ICinemachineCamera  mSoloCamera  // static @ 0x0
  private           UnityEngine.Coroutine           mPhysicsCoroutine  // 0x70
  private           System.Int32                    m_LastFrameUpdated  // 0x78
  private           UnityEngine.WaitForFixedUpdate  mWaitForFixedUpdate  // 0x80
  private           System.Collections.Generic.List<Cinemachine.CinemachineBrain.BrainFrame>mFrameStack  // 0x88
  private           System.Int32                    mNextFrameId  // 0x90
  private           Cinemachine.CinemachineBlend    mCurrentLiveCameras  // 0x98
  private   static readonly UnityEngine.AnimationCurve      mDefaultLinearAnimationCurve  // static @ 0x8
  private           Cinemachine.ICinemachineCamera  mActiveCameraPreviousFrame  // 0xa0
  private           UnityEngine.GameObject          mActiveCameraPreviousFrameGameObject  // 0xa8
  private           Cinemachine.CameraState         <CurrentCameraState>k__BackingField  // 0xb0
PROPERTIES:
  OutputCamera  get=0x033C8A00
  OutputDOF  get=0x033C7310
  SoloCamera  get=0x0678E20C  set=0x0678E2B8
  DefaultWorldUp  get=0x033C8F90
  ActiveVirtualCamera  get=0x033C9960
  IsBlending  get=0x033C7B70
  ActiveBlend  get=0x033C9850
  CurrentCameraState  get=0x04D87740  set=0x0678E240
METHODS:
  RVA=0x0678D8DC  token=0x6000041  UnityEngine.Color GetSoloGUIColor()
  RVA=0x03FCE9A0  token=0x6000043  System.Void OnEnable()
  RVA=0x0678DA00  token=0x6000044  System.Void OnDisable()
  RVA=0x0678DEF0  token=0x6000045  System.Void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
  RVA=0x04D7AB90  token=0x6000046  System.Void OnSceneUnloaded(UnityEngine.SceneManagement.Scene scene)
  RVA=0x04D83C20  token=0x6000047  System.Void Start()
  RVA=0x0678DC30  token=0x6000048  System.Void OnGuiHandler()
  RVA=0x03FCED60  token=0x6000049  System.Collections.IEnumerator AfterPhysics()
  RVA=0x041AEA60  token=0x600004A  System.Void LateUpdate()
  RVA=0x033CA8C0  token=0x600004B  System.Void ManualUpdate(System.Single deltaTime)
  RVA=0x033CAA10  token=0x600004C  System.Single GetEffectiveDeltaTime(System.Boolean fixedDelta)
  RVA=0x033C8B10  token=0x600004D  System.Void UpdateVirtualCameras(Cinemachine.CinemachineCore.UpdateFilter updateFilter, System.Single deltaTime)
  RVA=0x033C9A50  token=0x600004F  Cinemachine.ICinemachineCamera DeepCamBFromBlend(Cinemachine.CinemachineBlend blend)
  RVA=0x0678D938  token=0x6000050  System.Boolean IsLiveInBlend(Cinemachine.ICinemachineCamera vcam)
  RVA=0x0678D7D0  token=0x6000053  System.Int32 GetBrainFrame(System.Int32 withId)
  RVA=0x0678E00C  token=0x6000054  System.Int32 SetCameraOverride(System.Int32 overrideId, Cinemachine.ICinemachineCamera camA, Cinemachine.ICinemachineCamera camB, System.Single weightB, System.Single deltaTime)
  RVA=0x0678DF54  token=0x6000055  System.Void ReleaseCameraOverride(System.Int32 overrideId)
  RVA=0x033CA000  token=0x6000056  System.Void ProcessActiveCamera(System.Single deltaTime)
  RVA=0x033C6D10  token=0x6000057  System.Void UpdateFrame0(System.Single deltaTime)
  RVA=0x033C7010  token=0x6000058  System.Void ComputeCurrentBlend(Cinemachine.CinemachineBlend& outputBlend, System.Int32 numTopLayersToExclude)
  RVA=0x033C98A0  token=0x6000059  System.Boolean IsLive(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly)
  RVA=0x033C8670  token=0x600005C  Cinemachine.ICinemachineCamera TopCameraFromPriorityQueue()
  RVA=0x040F7700  token=0x600005D  Cinemachine.CinemachineBlendDefinition LookupBlend(Cinemachine.ICinemachineCamera fromKey, Cinemachine.ICinemachineCamera toKey)
  RVA=0x033C7390  token=0x600005E  System.Void PushStateToUnityCamera(Cinemachine.CameraState& state)
  RVA=0x03FCF900  token=0x600005F  System.Void .ctor()
  RVA=0x04D34F10  token=0x6000060  System.Void .cctor()
END_CLASS

CLASS: Cinemachine.CinemachineClearShot
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x250
EXTENDS: Cinemachine.CinemachineVirtualCameraBase
FIELDS:
  public            UnityEngine.Transform           m_LookAt  // 0xa8
  public            UnityEngine.Transform           m_Follow  // 0xb0
  public            System.Boolean                  m_ShowDebugText  // 0xb8
  private           Cinemachine.CinemachineVirtualCameraBase[]m_ChildCameras  // 0xc0
  public            System.Single                   m_ActivateAfter  // 0xc8
  public            System.Single                   m_MinDuration  // 0xcc
  public            System.Boolean                  m_RandomizeChoice  // 0xd0
  public            Cinemachine.CinemachineBlendDefinitionm_DefaultBlend  // 0xd8
  public            Cinemachine.CinemachineBlenderSettingsm_CustomBlends  // 0xf0
  private           Cinemachine.ICinemachineCamera  <LiveChild>k__BackingField  // 0xf8
  private           Cinemachine.CameraState         m_State  // 0x100
  private           System.Single                   mActivationTime  // 0x220
  private           System.Single                   mPendingActivationTime  // 0x224
  private           Cinemachine.ICinemachineCamera  mPendingCamera  // 0x228
  private           Cinemachine.CinemachineBlend    mActiveBlend  // 0x230
  private           System.Boolean                  mRandomizeNow  // 0x238
  private           Cinemachine.CinemachineVirtualCameraBase[]m_RandomizedChilden  // 0x240
  private           Cinemachine.ICinemachineCamera  m_TransitioningFrom  // 0x248
PROPERTIES:
  Description  get=0x067901A0
  LiveChild  get=0x04D877C0  set=0x06790298
  State  get=0x04D877D0
  LookAt  get=0x0678D7AC  set=0x04D84CC0
  Follow  get=0x0678D790  set=0x04D84CE0
  IsBlending  get=0x0679028C
  ChildCameras  get=0x06790180
METHODS:
  RVA=0x0678F39C  token=0x600006F  System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly)
  RVA=0x0678F9E8  token=0x6000074  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x0678ED80  token=0x6000075  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x0678EE60  token=0x6000076  System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x0678F6B4  token=0x6000077  System.Void OnEnable()
  RVA=0x0678F59C  token=0x6000078  System.Void OnDisable()
  RVA=0x0678FAAC  token=0x6000079  System.Void OnTransformChildrenChanged()
  RVA=0x0678F8A4  token=0x600007A  System.Void OnGuiHandler()
  RVA=0x0678F354  token=0x600007D  System.Void InvalidateListOfChildren()
  RVA=0x0678FDFC  token=0x600007E  System.Void ResetRandomization()
  RVA=0x0678FE28  token=0x600007F  System.Void UpdateListOfChildren()
  RVA=0x0678E700  token=0x6000080  Cinemachine.ICinemachineCamera ChooseCurrentCamera(UnityEngine.Vector3 worldUp)
  RVA=0x0678FBC4  token=0x6000081  Cinemachine.CinemachineVirtualCameraBase[] Randomize(Cinemachine.CinemachineVirtualCameraBase[] src)
  RVA=0x0678F3C4  token=0x6000082  Cinemachine.CinemachineBlendDefinition LookupBlend(Cinemachine.ICinemachineCamera fromKey, Cinemachine.ICinemachineCamera toKey)
  RVA=0x0678FACC  token=0x6000083  System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x06790008  token=0x6000084  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineCollider
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x78
EXTENDS: Cinemachine.CinemachineExtension
FIELDS:
  public            UnityEngine.LayerMask           m_CollideAgainst  // 0x28
  public            System.String                   m_IgnoreTag  // 0x30
  public            UnityEngine.LayerMask           m_TransparentLayers  // 0x38
  public            System.Single                   m_MinimumDistanceFromTarget  // 0x3c
  public            System.Boolean                  m_AvoidObstacles  // 0x40
  public            System.Single                   m_DistanceLimit  // 0x44
  public            System.Single                   m_MinimumOcclusionTime  // 0x48
  public            System.Single                   m_CameraRadius  // 0x4c
  public            Cinemachine.CinemachineCollider.ResolutionStrategym_Strategy  // 0x50
  public            System.Boolean                  m_UseFollowInsteadOfLookAt  // 0x54
  public            System.Int32                    m_MaximumEffort  // 0x58
  public            System.Single                   m_SmoothingTime  // 0x5c
  public            System.Single                   m_Damping  // 0x60
  public            System.Single                   m_DampingWhenOccluded  // 0x64
  public            System.Single                   m_OptimalTargetDistance  // 0x68
  private   static  System.Single                   k_PrecisionSlush  // const
  private           UnityEngine.RaycastHit[]        m_CornerBuffer  // 0x70
  private   static  System.Single                   k_AngleThreshold  // const
  private   static  UnityEngine.ECSColliderResultProxy[]s_ColliderBuffer  // static @ 0x0
PROPERTIES:
  DebugPaths  get=0x06792350
METHODS:
  RVA=0x06790FB0  token=0x6000088  System.Boolean IsTargetObscured(Cinemachine.ICinemachineCamera vcam)
  RVA=0x067902AC  token=0x6000089  System.Boolean CameraWasDisplaced(Cinemachine.ICinemachineCamera vcam)
  RVA=0x067906D4  token=0x600008A  System.Single GetCameraDisplacementDistance(Cinemachine.ICinemachineCamera vcam)
  RVA=0x06791050  token=0x600008B  System.Void OnValidate()
  RVA=0x06791008  token=0x600008C  System.Void OnDestroy()
  RVA=0x06790730  token=0x600008E  System.Single GetMaxDampTime()
  RVA=0x03200260  token=0x600008F  System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x067917EC  token=0x6000090  System.Void ResetExtraData(Cinemachine.ICinemachineCamera vcam)
  RVA=0x03200320  token=0x6000091  System.Void GuardedPostPipelineStageCallback(Cinemachine.ICinemachineCamera vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x03249DD0  token=0x6000092  UnityEngine.Vector3 PreserveLineOfSight(Cinemachine.CameraState& state, Cinemachine.CinemachineCollider.VcamExtraState& extra)
  RVA=0x0324A0D0  token=0x6000093  UnityEngine.Vector3 PullCameraInFrontOfNearestObstacle(UnityEngine.Vector3 cameraPos, UnityEngine.Vector3 lookAtPos, System.Int32 layerMask, UnityEngine.RaycastHit& hitInfo)
  RVA=0x0679109C  token=0x6000094  UnityEngine.Vector3 PushCameraBack(UnityEngine.Vector3 currentPos, UnityEngine.Vector3 pushDir, UnityEngine.RaycastHit obstacle, UnityEngine.Vector3 lookAtPos, UnityEngine.Plane startPlane, System.Single targetDistance, System.Int32 iterations, Cinemachine.CinemachineCollider.VcamExtraState& extra)
  RVA=0x067908AC  token=0x6000095  System.Boolean GetWalkingDirection(UnityEngine.Vector3 pos, UnityEngine.Vector3 pushDir, UnityEngine.RaycastHit obstacle, UnityEngine.Vector3& outDir)
  RVA=0x06790748  token=0x6000096  System.Single GetPushBackDistance(UnityEngine.Ray ray, UnityEngine.Plane startPlane, System.Single targetDistance, UnityEngine.Vector3 lookAtPos)
  RVA=0x067902C8  token=0x6000097  System.Single ClampRayToBounds(UnityEngine.Ray ray, System.Single distance, UnityEngine.Bounds bounds)
  RVA=0x0679188C  token=0x6000098  UnityEngine.Vector3 RespectCameraRadius(UnityEngine.Vector3 cameraPos, UnityEngine.Vector3 lookAtPos)
  RVA=0x03248E30  token=0x6000099  System.Boolean CheckForTargetObstructions(Cinemachine.CameraState& state)
  RVA=0x032492D0  token=0x600009A  System.Boolean IsTargetOffscreen(Cinemachine.CameraState& state)
  RVA=0x049C8EB0  token=0x600009B  System.Void .ctor()
  RVA=0x04D38910  token=0x600009C  System.Void .cctor()
END_CLASS

CLASS: Cinemachine.CinemachineConfiner
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x60
EXTENDS: Cinemachine.CinemachineExtension
FIELDS:
  public            Cinemachine.CinemachineConfiner.Modem_ConfineMode  // 0x28
  public            UnityEngine.Collider            m_BoundingVolume  // 0x30
  public            UnityEngine.Collider2D          m_BoundingShape2D  // 0x38
  private           UnityEngine.Collider2D          m_BoundingShape2DCache  // 0x40
  public            System.Boolean                  m_ConfineScreenEdges  // 0x48
  public            System.Single                   m_Damping  // 0x4c
  private           System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Vector2>>m_pathCache  // 0x50
  private           System.Int32                    m_pathTotalPointCount  // 0x58
PROPERTIES:
  IsValid  get=0x06793EA0
METHODS:
  RVA=0x06792B88  token=0x60000A2  System.Boolean CameraWasDisplaced(Cinemachine.CinemachineVirtualCameraBase vcam)
  RVA=0x06793668  token=0x60000A3  System.Single GetCameraDisplacementDistance(Cinemachine.CinemachineVirtualCameraBase vcam)
  RVA=0x067936EC  token=0x60000A4  System.Void OnValidate()
  RVA=0x06793660  token=0x60000A5  System.Void ConnectToVcam(System.Boolean connect)
  RVA=0x04D87860  token=0x60000A7  System.Single GetMaxDampTime()
  RVA=0x067936FC  token=0x60000A8  System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x067936C4  token=0x60000A9  System.Void InvalidatePathCache()
  RVA=0x06793994  token=0x60000AA  System.Boolean ValidatePathCache()
  RVA=0x06792BA4  token=0x60000AB  UnityEngine.Vector3 ConfinePoint(UnityEngine.Vector3 camPos)
  RVA=0x0679301C  token=0x60000AC  UnityEngine.Vector3 ConfineScreenEdges(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CameraState& state)
  RVA=0x06793E84  token=0x60000AD  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineConfiner2D
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x128
EXTENDS: Cinemachine.CinemachineExtension
FIELDS:
  public            UnityEngine.Collider2D          m_BoundingShape2D  // 0x28
  public            System.Single                   m_Damping  // 0x30
  public            System.Single                   m_MaxWindowSize  // 0x34
  private           System.Single                   m_MaxComputationTimePerFrameInSeconds  // 0x38
  private   static  System.Single                   k_cornerAngleTreshold  // const
  private           Cinemachine.CinemachineConfiner2D.ShapeCachem_shapeCache  // 0x40
METHODS:
  RVA=0x067925B4  token=0x60000AF  System.Void InvalidateCache()
  RVA=0x06792B30  token=0x60000B0  System.Boolean ValidateCache(System.Single cameraAspectRatio)
  RVA=0x067925DC  token=0x60000B1  System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x06792518  token=0x60000B2  System.Single CalculateHalfFrustumHeight(Cinemachine.CameraState& state, System.Single& cameraPosLocalZ)
  RVA=0x067925C0  token=0x60000B3  System.Void OnValidate()
  RVA=0x04D87850  token=0x60000B4  System.Void Reset()
  RVA=0x06792B6C  token=0x60000B5  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineDollyCart
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Cinemachine.CinemachinePathBase m_Path  // 0x18
  public            System.Boolean                  forbidRotation  // 0x20
  public            Cinemachine.CinemachineDollyCart.UpdateMethodm_UpdateMethod  // 0x24
  public            Cinemachine.CinemachinePathBase.PositionUnitsm_PositionUnits  // 0x28
  public            System.Single                   m_Speed  // 0x2c
  public            System.Single                   m_Position  // 0x30
METHODS:
  RVA=0x06793FA0  token=0x60000BB  System.Void FixedUpdate()
  RVA=0x0679419C  token=0x60000BC  System.Void Update()
  RVA=0x06793FF0  token=0x60000BD  System.Void LateUpdate()
  RVA=0x06794054  token=0x60000BE  System.Void SetCartPosition(System.Single distanceAlongPath)
  RVA=0x067941FC  token=0x60000BF  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineExternalCamera
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x1E8
EXTENDS: Cinemachine.CinemachineVirtualCameraBase
FIELDS:
  public            UnityEngine.Transform           m_LookAt  // 0xa8
  private           UnityEngine.Camera              m_Camera  // 0xb0
  private           Cinemachine.CameraState         m_State  // 0xb8
  private           UnityEngine.Transform           <Follow>k__BackingField  // 0x1d8
  public            Cinemachine.CinemachineVirtualCameraBase.BlendHintm_BlendHint  // 0x1e0
PROPERTIES:
  State  get=0x0441A2B0
  LookAt  get=0x04D86290  set=0x04D849C0
  Follow  get=0x04D87870  set=0x04D849E0
METHODS:
  RVA=0x0391CE60  token=0x60000C5  System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x04220280  token=0x60000C6  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineFollowZoom
TYPE:  class
TOKEN: 0x2000023
SIZE:  0x38
EXTENDS: Cinemachine.CinemachineExtension
FIELDS:
  public            System.Single                   m_Width  // 0x28
  public            System.Single                   m_Damping  // 0x2c
  public            System.Single                   m_MinFOV  // 0x30
  public            System.Single                   m_MaxFOV  // 0x34
METHODS:
  RVA=0x06794218  token=0x60000C7  System.Void OnValidate()
  RVA=0x04D86AF0  token=0x60000C8  System.Single GetMaxDampTime()
  RVA=0x06794274  token=0x60000C9  System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x06794530  token=0x60000CA  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineFreeLook
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x3B8
EXTENDS: Cinemachine.CinemachineVirtualCameraBase
FIELDS:
  public            UnityEngine.Transform           m_LookAt  // 0xa8
  public            UnityEngine.Transform           m_Follow  // 0xb0
  public            System.Boolean                  m_CommonLens  // 0xb8
  public            Cinemachine.LensSettings        m_Lens  // 0xbc
  public            Cinemachine.CinemachineVirtualCameraBase.TransitionParamsm_Transitions  // 0xf0
  private           Cinemachine.CinemachineVirtualCameraBase.BlendHintm_LegacyBlendHint  // 0x100
  public            Cinemachine.AxisState           m_YAxis  // 0x108
  public            Cinemachine.AxisState.Recenteringm_YAxisRecentering  // 0x178
  public            Cinemachine.AxisState           m_XAxis  // 0x198
  public            Cinemachine.CinemachineOrbitalTransposer.Headingm_Heading  // 0x208
  public            Cinemachine.AxisState.Recenteringm_RecenterToTargetHeading  // 0x214
  public            Cinemachine.CinemachineTransposer.BindingModem_BindingMode  // 0x230
  public            System.Single                   m_SplineCurvature  // 0x234
  public            Cinemachine.CinemachineFreeLook.Orbit[]m_Orbits  // 0x238
  private           System.Single                   m_LegacyHeadingBias  // 0x240
  private           System.Boolean                  mUseLegacyRigDefinitions  // 0x244
  private           System.Boolean                  mIsDestroyed  // 0x245
  private           Cinemachine.CameraState         m_State  // 0x248
  private           Cinemachine.CinemachineVirtualCamera[]m_Rigs  // 0x368
  private           Cinemachine.CinemachineOrbitalTransposer[]mOrbitals  // 0x370
  private           Cinemachine.CinemachineBlend    mBlendA  // 0x378
  private           Cinemachine.CinemachineBlend    mBlendB  // 0x380
  public    static  Cinemachine.CinemachineFreeLook.CreateRigDelegateCreateRigOverride  // static @ 0x0
  public    static  Cinemachine.CinemachineFreeLook.DestroyRigDelegateDestroyRigOverride  // static @ 0x8
  private           System.Single                   m_CachedXAxisHeading  // 0x388
  private           Cinemachine.CinemachineFreeLook.Orbit[]m_CachedOrbits  // 0x390
  private           System.Single                   m_CachedTension  // 0x398
  private           UnityEngine.Vector4[]           m_CachedKnots  // 0x3a0
  private           UnityEngine.Vector4[]           m_CachedCtrl1  // 0x3a8
  private           UnityEngine.Vector4[]           m_CachedCtrl2  // 0x3b0
PROPERTIES:
  RigNames  get=0x06798398
  PreviousStateIsValid  get=0x04D86F70  set=0x06798440
  State  get=0x04D87880
  LookAt  get=0x0678D7AC  set=0x04D84CC0
  Follow  get=0x0678D790  set=0x04D84CE0
METHODS:
  RVA=0x06796E2C  token=0x60000CC  System.Void OnValidate()
  RVA=0x06795620  token=0x60000CD  Cinemachine.CinemachineVirtualCamera GetRig(System.Int32 i)
  RVA=0x067969A4  token=0x60000CF  System.Void OnEnable()
  RVA=0x06797A6C  token=0x60000D0  System.Void UpdateInputAxisProvider()
  RVA=0x06796888  token=0x60000D1  System.Void OnDestroy()
  RVA=0x06796060  token=0x60000D2  System.Void OnTransformChildrenChanged()
  RVA=0x067975C4  token=0x60000D3  System.Void Reset()
  RVA=0x06796078  token=0x60000DB  System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly)
  RVA=0x067969E4  token=0x60000DC  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x06795100  token=0x60000DD  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x06795A44  token=0x60000DE  System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x06796AA8  token=0x60000DF  System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x02FFF600  token=0x60000E0  System.Boolean RequiresUserInput()
  RVA=0x06795670  token=0x60000E1  System.Single GetYAxisClosestValue(UnityEngine.Vector3 cameraPos, UnityEngine.Vector3 up)
  RVA=0x06796060  token=0x60000E2  System.Void InvalidateRigCache()
  RVA=0x06794C7C  token=0x60000E3  System.Void DestroyRigs()
  RVA=0x06794828  token=0x60000E4  Cinemachine.CinemachineVirtualCamera[] CreateRigs(Cinemachine.CinemachineVirtualCamera[] copyFrom)
  RVA=0x06797B00  token=0x60000E5  System.Void UpdateRigCache()
  RVA=0x06796184  token=0x60000E6  System.Int32 LocateExistingRigs(System.String[] rigNames, System.Boolean forceOrbital)
  RVA=0x06797D38  token=0x60000E7  System.Single UpdateXAxisHeading(Cinemachine.CinemachineOrbitalTransposer orbital, System.Single deltaTime, UnityEngine.Vector3 up)
  RVA=0x06796F6C  token=0x60000E8  System.Void PushSettingsToRigs()
  RVA=0x06795A14  token=0x60000E9  System.Single GetYAxisValue()
  RVA=0x06794564  token=0x60000EA  Cinemachine.CameraState CalculateNewState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x067953E0  token=0x60000EB  UnityEngine.Vector3 GetLocalPositionForCameraFromInput(System.Single t)
  RVA=0x067975CC  token=0x60000EC  System.Void UpdateCachedSpline()
  RVA=0x06797EC8  token=0x60000ED  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineMixingCamera
TYPE:  class
TOKEN: 0x2000029
SIZE:  0x210
EXTENDS: Cinemachine.CinemachineVirtualCameraBase
FIELDS:
  public    static  System.Int32                    MaxCameras  // const
  public            System.Single                   m_Weight0  // 0xa8
  public            System.Single                   m_Weight1  // 0xac
  public            System.Single                   m_Weight2  // 0xb0
  public            System.Single                   m_Weight3  // 0xb4
  public            System.Single                   m_Weight4  // 0xb8
  public            System.Single                   m_Weight5  // 0xbc
  public            System.Single                   m_Weight6  // 0xc0
  public            System.Single                   m_Weight7  // 0xc4
  private           Cinemachine.CameraState         m_State  // 0xc8
  private           Cinemachine.ICinemachineCamera  <LiveChild>k__BackingField  // 0x1e8
  private           UnityEngine.Transform           <LookAt>k__BackingField  // 0x1f0
  private           UnityEngine.Transform           <Follow>k__BackingField  // 0x1f8
  private           Cinemachine.CinemachineVirtualCameraBase[]m_ChildCameras  // 0x200
  private           System.Collections.Generic.Dictionary<Cinemachine.CinemachineVirtualCameraBase,System.Int32>m_indexMap  // 0x208
PROPERTIES:
  LiveChild  get=0x04D87930  set=0x06799DA0
  State  get=0x04D87950
  LookAt  get=0x04D87940  set=0x06799DB4
  Follow  get=0x04D87920  set=0x06799D8C
  ChildCameras  get=0x06799D6C
METHODS:
  RVA=0x06798F24  token=0x60000F3  System.Single GetWeight(System.Int32 index)
  RVA=0x067998C8  token=0x60000F4  System.Void SetWeight(System.Int32 index, System.Single w)
  RVA=0x06798E08  token=0x60000F5  System.Single GetWeight(Cinemachine.CinemachineVirtualCameraBase vcam)
  RVA=0x067997A0  token=0x60000F6  System.Void SetWeight(Cinemachine.CinemachineVirtualCameraBase vcam, System.Single w)
  RVA=0x06799524  token=0x60000FE  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x06798D28  token=0x60000FF  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x06799504  token=0x6000100  System.Void OnEnable()
  RVA=0x067995E8  token=0x6000101  System.Void OnTransformChildrenChanged()
  RVA=0x06799744  token=0x6000102  System.Void OnValidate()
  RVA=0x06799468  token=0x6000103  System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly)
  RVA=0x06799420  token=0x6000105  System.Void InvalidateListOfChildren()
  RVA=0x067999D8  token=0x6000106  System.Void ValidateListOfChildren()
  RVA=0x067995F0  token=0x6000107  System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x06799028  token=0x6000108  System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x06799C08  token=0x6000109  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachinePath
TYPE:  class
TOKEN: 0x200002A
SIZE:  0x58
EXTENDS: Cinemachine.CinemachinePathBase
FIELDS:
  public            System.Boolean                  m_Looped  // 0x48
  public            Cinemachine.CinemachinePath.Waypoint[]m_Waypoints  // 0x50
PROPERTIES:
  MinPos  get=0x04D879D0
  MaxPos  get=0x0679A718
  Looped  get=0x04D867D0
  DistanceCacheSampleStepsPerSegment  get=0x011EF5B0
METHODS:
  RVA=0x0679A5A8  token=0x600010D  System.Void Reset()
  RVA=0x0679A4C0  token=0x600010F  System.Single GetBoundingIndices(System.Single pos, System.Int32& indexA, System.Int32& indexB)
  RVA=0x0679A058  token=0x6000110  UnityEngine.Vector3 EvaluatePosition(System.Single pos)
  RVA=0x0679A268  token=0x6000111  UnityEngine.Vector3 EvaluateTangent(System.Single pos)
  RVA=0x06799DC8  token=0x6000112  UnityEngine.Quaternion EvaluateOrientation(System.Single pos)
  RVA=0x0679A598  token=0x6000113  System.Void OnValidate()
  RVA=0x0679A6C8  token=0x6000114  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachinePipeline
TYPE:  sealed class
TOKEN: 0x200002C
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x03695570  token=0x6000115  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachinePixelPerfect
TYPE:  class
TOKEN: 0x200002D
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x05393520  token=0x6000116  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineSmoothPath
TYPE:  class
TOKEN: 0x200002E
SIZE:  0x70
EXTENDS: Cinemachine.CinemachinePathBase
FIELDS:
  public            System.Boolean                  m_Looped  // 0x48
  public            Cinemachine.CinemachineSmoothPath.Waypoint[]m_Waypoints  // 0x50
  private           Cinemachine.CinemachineSmoothPath.Waypoint[]m_ControlPoints1  // 0x58
  private           Cinemachine.CinemachineSmoothPath.Waypoint[]m_ControlPoints2  // 0x60
  private           System.Boolean                  m_IsLoopedCache  // 0x68
PROPERTIES:
  MinPos  get=0x04D879D0
  MaxPos  get=0x0679A718
  Looped  get=0x04D867D0
  DistanceCacheSampleStepsPerSegment  get=0x011EF5B0
METHODS:
  RVA=0x0679A598  token=0x600011B  System.Void OnValidate()
  RVA=0x067A3C1C  token=0x600011C  System.Void Reset()
  RVA=0x067A3BE8  token=0x600011D  System.Void InvalidateDistanceCache()
  RVA=0x067A3D24  token=0x600011E  System.Void UpdateControlPoints()
  RVA=0x067A3B3C  token=0x600011F  System.Single GetBoundingIndices(System.Single pos, System.Int32& indexA, System.Int32& indexB)
  RVA=0x067A36E8  token=0x6000120  UnityEngine.Vector3 EvaluatePosition(System.Single pos)
  RVA=0x067A38E0  token=0x6000121  UnityEngine.Vector3 EvaluateTangent(System.Single pos)
  RVA=0x067A3458  token=0x6000122  UnityEngine.Quaternion EvaluateOrientation(System.Single pos)
  RVA=0x04D87D00  token=0x6000123  UnityEngine.Quaternion RollAroundForward(System.Single angle)
  RVA=0x067A3F2C  token=0x6000124  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineStateDrivenCamera
TYPE:  class
TOKEN: 0x2000030
SIZE:  0x298
EXTENDS: Cinemachine.CinemachineVirtualCameraBase
FIELDS:
  public            UnityEngine.Transform           m_LookAt  // 0xa8
  public            UnityEngine.Transform           m_Follow  // 0xb0
  public            UnityEngine.Animator            m_AnimatedTarget  // 0xb8
  public            System.Int32                    m_LayerIndex  // 0xc0
  public            System.Boolean                  m_ShowDebugText  // 0xc4
  private           Cinemachine.CinemachineVirtualCameraBase[]m_ChildCameras  // 0xc8
  public            Cinemachine.CinemachineStateDrivenCamera.Instruction[]m_Instructions  // 0xd0
  public            Cinemachine.CinemachineBlendDefinitionm_DefaultBlend  // 0xd8
  public            Cinemachine.CinemachineBlenderSettingsm_CustomBlends  // 0xf0
  private           Cinemachine.CinemachineStateDrivenCamera.ParentHash[]m_ParentHash  // 0xf8
  private           Cinemachine.ICinemachineCamera  <LiveChild>k__BackingField  // 0x100
  private           Cinemachine.ICinemachineCamera  m_TransitioningFrom  // 0x108
  private           Cinemachine.CameraState         m_State  // 0x110
  private           System.Collections.Generic.Dictionary<UnityEngine.AnimationClip,System.Collections.Generic.List<Cinemachine.CinemachineStateDrivenCamera.HashPair>>mHashCache  // 0x230
  private           System.Single                   mActivationTime  // 0x238
  private           Cinemachine.CinemachineStateDrivenCamera.InstructionmActiveInstruction  // 0x240
  private           System.Single                   mPendingActivationTime  // 0x258
  private           Cinemachine.CinemachineStateDrivenCamera.InstructionmPendingInstruction  // 0x260
  private           Cinemachine.CinemachineBlend    mActiveBlend  // 0x278
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>mInstructionDictionary  // 0x280
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>mStateParentLookup  // 0x288
  private           System.Collections.Generic.List<UnityEngine.AnimatorClipInfo>m_clipInfoList  // 0x290
PROPERTIES:
  Description  get=0x067A5DA8
  LiveChild  get=0x04D87D50  set=0x067A5EA0
  State  get=0x04D87D60
  LookAt  get=0x0678D7AC  set=0x04D84CC0
  Follow  get=0x0678D790  set=0x04D84CE0
  ChildCameras  get=0x067A5D88
  IsBlending  get=0x067A5E94
METHODS:
  RVA=0x067A4D3C  token=0x600012A  System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly)
  RVA=0x067A55A4  token=0x6000130  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x067A458C  token=0x6000131  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x067A5670  token=0x6000132  System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x067A4788  token=0x6000133  System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x067A5270  token=0x6000134  System.Void OnEnable()
  RVA=0x067A5158  token=0x6000135  System.Void OnDisable()
  RVA=0x067A5668  token=0x6000136  System.Void OnTransformChildrenChanged()
  RVA=0x067A5460  token=0x6000137  System.Void OnGuiHandler()
  RVA=0x067A4510  token=0x600013A  System.Int32 CreateFakeHash(System.Int32 parentHash, UnityEngine.AnimationClip clip)
  RVA=0x067A4F3C  token=0x600013B  System.Int32 LookupFakeHash(System.Int32 parentHash, UnityEngine.AnimationClip clip)
  RVA=0x067A4D08  token=0x600013C  System.Void InvalidateListOfChildren()
  RVA=0x067A572C  token=0x600013D  System.Void UpdateListOfChildren()
  RVA=0x067A58E0  token=0x600013E  System.Void ValidateInstructions()
  RVA=0x067A3F60  token=0x600013F  Cinemachine.CinemachineVirtualCameraBase ChooseCurrentCamera()
  RVA=0x067A466C  token=0x6000140  System.Int32 GetClipHash(System.Int32 hash, System.Collections.Generic.List<UnityEngine.AnimatorClipInfo> clips)
  RVA=0x067A4D64  token=0x6000141  Cinemachine.CinemachineBlendDefinition LookupBlend(Cinemachine.ICinemachineCamera fromKey, Cinemachine.ICinemachineCamera toKey)
  RVA=0x067A5BAC  token=0x6000142  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineStoryboard
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x78
EXTENDS: Cinemachine.CinemachineExtension
FIELDS:
  public    static  System.Boolean                  s_StoryboardGlobalMute  // static @ 0x0
  public            System.Boolean                  m_ShowImage  // 0x28
  public            UnityEngine.Texture             m_Image  // 0x30
  public            Cinemachine.CinemachineStoryboard.FillStrategym_Aspect  // 0x38
  public            System.Single                   m_Alpha  // 0x3c
  public            UnityEngine.Vector2             m_Center  // 0x40
  public            UnityEngine.Vector3             m_Rotation  // 0x48
  public            UnityEngine.Vector2             m_Scale  // 0x54
  public            System.Boolean                  m_SyncScale  // 0x5c
  public            System.Boolean                  m_MuteCamera  // 0x5d
  public            System.Single                   m_SplitView  // 0x60
  public            Cinemachine.CinemachineStoryboard.StoryboardRenderModem_RenderMode  // 0x64
  public            System.Int32                    m_SortingOrder  // 0x68
  public            System.Single                   m_PlaneDistance  // 0x6c
  private           System.Collections.Generic.List<Cinemachine.CinemachineStoryboard.CanvasInfo>mCanvasInfo  // 0x70
PROPERTIES:
  CanvasName  get=0x067A75B8
METHODS:
  RVA=0x067A7220  token=0x6000143  System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x067A731C  token=0x6000144  System.Void UpdateRenderCanvas()
  RVA=0x067A6064  token=0x6000145  System.Void ConnectToVcam(System.Boolean connect)
  RVA=0x067A5EB4  token=0x6000147  System.Void CameraUpdatedCallback(Cinemachine.CinemachineBrain brain)
  RVA=0x067A6804  token=0x6000148  Cinemachine.CinemachineStoryboard.CanvasInfo LocateMyCanvas(Cinemachine.CinemachineBrain parent, System.Boolean createIfNotFound)
  RVA=0x067A61C8  token=0x6000149  System.Void CreateCanvas(Cinemachine.CinemachineStoryboard.CanvasInfo ci)
  RVA=0x067A6614  token=0x600014A  System.Void DestroyCanvas()
  RVA=0x067A6BE8  token=0x600014B  System.Void PlaceImage(Cinemachine.CinemachineStoryboard.CanvasInfo ci, System.Single alpha)
  RVA=0x031294E0  token=0x600014C  System.Void StaticBlendingHandler(Cinemachine.CinemachineBrain brain)
  RVA=0x0394EED0  token=0x600014D  System.Void InitializeModule()
  RVA=0x067A74C0  token=0x600014E  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.ICinemachineTargetGroup
TYPE:  interface
TOKEN: 0x2000038
FIELDS:
PROPERTIES:
  Sphere  get=-1  // abstract
  IsEmpty  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000152  UnityEngine.Bounds GetViewSpaceBoundingBox(UnityEngine.Matrix4x4 observer)
  RVA=-1  // abstract  token=0x6000153  System.Void GetViewSpaceAngularBounds(UnityEngine.Matrix4x4 observer, UnityEngine.Vector2& minAngles, UnityEngine.Vector2& maxAngles, UnityEngine.Vector2& zRange)
END_CLASS

CLASS: Cinemachine.CinemachineTargetGroup
TYPE:  class
TOKEN: 0x2000039
SIZE:  0x68
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Cinemachine.ICinemachineTargetGroup
FIELDS:
  public            Cinemachine.CinemachineTargetGroup.PositionModem_PositionMode  // 0x18
  public            Cinemachine.CinemachineTargetGroup.RotationModem_RotationMode  // 0x1c
  public            Cinemachine.CinemachineTargetGroup.UpdateMethodm_UpdateMethod  // 0x20
  public            Cinemachine.CinemachineTargetGroup.Target[]m_Targets  // 0x28
  private           UnityEngine.Bounds              <BoundingBox>k__BackingField  // 0x30
  private           System.Single                   m_MaxWeight  // 0x48
  private           UnityEngine.Vector3             m_AveragePos  // 0x4c
  private           UnityEngine.BoundingSphere      m_BoundingSphere  // 0x58
PROPERTIES:
  Transform  get=0x053953F4
  BoundingBox  get=0x04D87DE0  set=0x04D87E10
  Sphere  get=0x04D87E00
  IsEmpty  get=0x067A8BE8
METHODS:
  RVA=0x067A7624  token=0x6000159  System.Void AddMember(UnityEngine.Transform t, System.Single weight, System.Single radius)
  RVA=0x067A89EC  token=0x600015A  System.Void RemoveMember(UnityEngine.Transform t)
  RVA=0x067A814C  token=0x600015B  System.Int32 FindMember(UnityEngine.Transform t)
  RVA=0x067A8894  token=0x600015C  UnityEngine.BoundingSphere GetWeightedBoundsForMember(System.Int32 index)
  RVA=0x067A869C  token=0x600015D  UnityEngine.Bounds GetViewSpaceBoundingBox(UnityEngine.Matrix4x4 observer)
  RVA=0x067A8AE0  token=0x600015E  UnityEngine.BoundingSphere WeightedMemberBounds(Cinemachine.CinemachineTargetGroup.Target t, UnityEngine.Vector3 avgPos, System.Single maxWeight)
  RVA=0x067A7FFC  token=0x600015F  System.Void DoUpdate()
  RVA=0x067A7D3C  token=0x6000160  UnityEngine.BoundingSphere CalculateBoundingSphere(System.Single maxWeight)
  RVA=0x067A7930  token=0x6000161  UnityEngine.Vector3 CalculateAveragePosition(System.Single& maxWeight)
  RVA=0x067A773C  token=0x6000162  UnityEngine.Quaternion CalculateAverageOrientation()
  RVA=0x067A7B58  token=0x6000163  UnityEngine.Bounds CalculateBoundingBox(UnityEngine.Vector3 avgPos, System.Single maxWeight)
  RVA=0x067A893C  token=0x6000164  System.Void OnValidate()
  RVA=0x067A81EC  token=0x6000165  System.Void FixedUpdate()
  RVA=0x067A8AB0  token=0x6000166  System.Void Update()
  RVA=0x067A892C  token=0x6000167  System.Void LateUpdate()
  RVA=0x067A81FC  token=0x6000168  System.Void GetViewSpaceAngularBounds(UnityEngine.Matrix4x4 observer, UnityEngine.Vector2& minAngles, UnityEngine.Vector2& maxAngles, UnityEngine.Vector2& zRange)
  RVA=0x04CEAC30  token=0x6000169  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineVirtualCamera
TYPE:  class
TOKEN: 0x200003E
SIZE:  0x240
EXTENDS: Cinemachine.CinemachineVirtualCameraBase
FIELDS:
  public            UnityEngine.Transform           m_LookAt  // 0xa8
  public            UnityEngine.Transform           m_Follow  // 0xb0
  public            Cinemachine.LensSettings        m_Lens  // 0xb8
  public            Cinemachine.CinemachineVirtualCameraBase.TransitionParamsm_Transitions  // 0xe8
  private           Cinemachine.CinemachineVirtualCameraBase.BlendHintm_LegacyBlendHint  // 0xf8
  public    static  System.String                   PipelineName  // const
  public    static  Cinemachine.CinemachineVirtualCamera.CreatePipelineDelegateCreatePipelineOverride  // static @ 0x0
  public    static  Cinemachine.CinemachineVirtualCamera.DestroyPipelineDelegateDestroyPipelineOverride  // static @ 0x8
  private           System.Boolean                  m_UserIsDragging  // 0xfc
  protected         Cinemachine.CameraState         m_State  // 0x100
  private           Cinemachine.CinemachineComponentBase[]m_ComponentPipeline  // 0x220
  private           UnityEngine.Transform           m_ComponentOwner  // 0x228
  private           UnityEngine.Transform           mCachedLookAtTarget  // 0x230
  private           Cinemachine.CinemachineVirtualCameraBasemCachedLookAtTargetVcam  // 0x238
PROPERTIES:
  State  get=0x04D877D0
  LookAt  get=0x031FD680  set=0x04D84CC0
  Follow  get=0x031FE3A0  set=0x04D84CE0
METHODS:
  RVA=0x04D87E30  token=0x600016F  System.Void SetFieldOfView(System.Single fov)
  RVA=0x067A9404  token=0x6000170  System.Single GetMaxDampTime()
  RVA=0x031FD040  token=0x6000171  System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x0413B3D0  token=0x6000172  System.Void OnEnable()
  RVA=0x067A9490  token=0x6000173  System.Void OnDestroy()
  RVA=0x067A9870  token=0x6000174  System.Void OnValidate()
  RVA=0x0539700C  token=0x6000175  System.Void OnTransformChildrenChanged()
  RVA=0x067A9A10  token=0x6000176  System.Void Reset()
  RVA=0x067A8E04  token=0x6000177  System.Void DestroyPipeline()
  RVA=0x067A8C90  token=0x6000178  UnityEngine.Transform CreatePipeline(Cinemachine.CinemachineVirtualCamera copyFrom)
  RVA=0x0539700C  token=0x6000179  System.Void InvalidateComponentPipeline()
  RVA=0x067A93E4  token=0x600017A  UnityEngine.Transform GetComponentOwner()
  RVA=0x05397AD4  token=0x600017B  Cinemachine.CinemachineComponentBase[] GetComponentPipeline()
  RVA=0x04638530  token=0x600017C  Cinemachine.CinemachineComponentBase GetCinemachineComponent(Cinemachine.CinemachineCore.Stage stage)
  RVA=-1  // generic def  token=0x600017D  T GetCinemachineComponent()
  RVA=-1  // generic def  token=0x600017E  T AddCinemachineComponent()
  RVA=-1  // generic def  token=0x600017F  System.Void DestroyCinemachineComponent()
  RVA=0x03E69980  token=0x6000180  System.Void UpdateComponentPipeline()
  RVA=0x03E6A460  token=0x6000181  System.Void SetFlagsForHiddenChild(UnityEngine.GameObject child)
  RVA=0x031FD930  token=0x6000182  Cinemachine.CameraState CalculateNewState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x067A969C  token=0x6000183  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x067A9274  token=0x6000184  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x04D87E40  token=0x6000185  System.Void SetStateRawPosition(UnityEngine.Vector3 pos)
  RVA=0x03E695D0  token=0x6000186  System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x067A98DC  token=0x6000187  System.Boolean RequiresUserInput()
  RVA=0x042203E0  token=0x6000188  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.Cinemachine3rdPersonFollow
TYPE:  class
TOKEN: 0x2000042
SIZE:  0x80
EXTENDS: Cinemachine.CinemachineComponentBase
FIELDS:
  public            UnityEngine.Vector3             Damping  // 0x20
  public            UnityEngine.Vector3             ShoulderOffset  // 0x2c
  public            System.Single                   VerticalArmLength  // 0x38
  public            System.Single                   CameraSide  // 0x3c
  public            System.Single                   CameraDistance  // 0x40
  public            UnityEngine.LayerMask           CameraCollisionFilter  // 0x44
  public            System.String                   IgnoreTag  // 0x48
  public            System.Single                   CameraRadius  // 0x50
  public            System.Single                   DampingIntoCollision  // 0x54
  public            System.Single                   DampingFromCollision  // 0x58
  public            System.Boolean                  DisableDampingCorrection  // 0x5c
  private           UnityEngine.Vector3             m_PreviousFollowTargetPosition  // 0x60
  private           UnityEngine.Vector3             m_DampingCorrection  // 0x6c
  private           System.Single                   m_CamPosCollisionCorrection  // 0x78
PROPERTIES:
  IsValid  get=0x031FA930
  Stage  get=0x011EC580
METHODS:
  RVA=0x0679C2FC  token=0x6000191  System.Void OnValidate()
  RVA=0x04D87A40  token=0x6000192  System.Void Reset()
  RVA=0x0679C1FC  token=0x6000193  System.Void OnDestroy()
  RVA=0x0679BFEC  token=0x6000196  System.Single GetMaxDampTime()
  RVA=0x03248DB0  token=0x6000197  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x0679C23C  token=0x6000198  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x0324B240  token=0x6000199  System.Void PositionCamera(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x0679C010  token=0x600019A  System.Void GetRigPositions(UnityEngine.Vector3& root, UnityEngine.Vector3& shoulder, UnityEngine.Vector3& hand)
  RVA=0x0324B060  token=0x600019B  UnityEngine.Quaternion GetHeading(UnityEngine.Quaternion targetRot, UnityEngine.Vector3 up)
  RVA=0x03D42CA0  token=0x600019C  System.Void GetRawRigPositions(UnityEngine.Vector3 root, UnityEngine.Quaternion targetRot, UnityEngine.Quaternion heading, UnityEngine.Vector3& shoulder, UnityEngine.Vector3& hand)
  RVA=0x0679BD94  token=0x600019D  UnityEngine.Vector3 BeyondResolveCollisions(UnityEngine.Vector3 root, UnityEngine.Vector3 tip, System.Single cameraRadius)
  RVA=0x0679C378  token=0x600019E  UnityEngine.Vector3 ResolveCollisions(UnityEngine.Vector3 root, UnityEngine.Vector3 tip, System.Single deltaTime, System.Single cameraRadius, System.Single& collisionCorrection)
  RVA=0x0418E850  token=0x600019F  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineBasicMultiChannelPerlin
TYPE:  class
TOKEN: 0x2000043
SIZE:  0x50
EXTENDS: Cinemachine.CinemachineComponentBase
FIELDS:
  public            Cinemachine.NoiseSettings       m_NoiseProfile  // 0x20
  public            UnityEngine.Vector3             m_PivotOffset  // 0x28
  public            System.Single                   m_AmplitudeGain  // 0x34
  public            System.Single                   m_FrequencyGain  // 0x38
  private           System.Boolean                  mInitialized  // 0x3c
  private           System.Single                   mNoiseTime  // 0x40
  private           UnityEngine.Vector3             mNoiseOffsets  // 0x44
PROPERTIES:
  IsValid  get=0x0679CDAC
  Stage  get=0x02BBE540
METHODS:
  RVA=0x0679C780  token=0x60001A2  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x0679CCD0  token=0x60001A3  System.Void ReSeed()
  RVA=0x0679C6C8  token=0x60001A4  System.Void Initialize()
  RVA=0x0679CD54  token=0x60001A5  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineComposer
TYPE:  class
TOKEN: 0x2000044
SIZE:  0x110
EXTENDS: Cinemachine.CinemachineComponentBase
FIELDS:
  public            UnityEngine.Vector3             m_TrackedObjectOffset  // 0x20
  public            System.Single                   m_LookaheadTime  // 0x2c
  public            System.Single                   m_LookaheadSmoothing  // 0x30
  public            System.Boolean                  m_LookaheadIgnoreY  // 0x34
  public            System.Single                   m_HorizontalDamping  // 0x38
  public            System.Single                   m_VerticalDamping  // 0x3c
  public            System.Single                   m_ScreenX  // 0x40
  public            System.Single                   m_ScreenY  // 0x44
  public            System.Single                   m_DeadZoneWidth  // 0x48
  public            System.Single                   m_DeadZoneHeight  // 0x4c
  public            System.Single                   m_SoftZoneWidth  // 0x50
  public            System.Single                   m_SoftZoneHeight  // 0x54
  public            System.Single                   m_BiasX  // 0x58
  public            System.Single                   m_BiasY  // 0x5c
  public            System.Boolean                  m_CenterOnActivate  // 0x60
  private           UnityEngine.Vector3             <TrackedPoint>k__BackingField  // 0x64
  private           UnityEngine.Vector3             m_CameraPosPrevFrame  // 0x70
  private           UnityEngine.Vector3             m_LookAtPrevFrame  // 0x7c
  private           UnityEngine.Vector2             m_ScreenOffsetPrevFrame  // 0x88
  private           UnityEngine.Quaternion          m_CameraOrientationPrevFrame  // 0x90
  private           Cinemachine.Utility.PositionPredictorm_Predictor  // 0xa0
  private           Cinemachine.CinemachineComposer.FovCachemCache  // 0xa8
  private           UnityEngine.Vector2             _lastExtra  // 0xf8
  private           UnityEngine.Vector2             _lastOriginalRotToRect  // 0x100
  public            UnityEngine.Vector2             deadZoneDampFactor  // 0x108
PROPERTIES:
  IsValid  get=0x031FC1A0
  Stage  get=0x04D85B80
  TrackedPoint  get=0x04D87AF0  set=0x04D87B10
  useTrackedObjectOffsetCollision  get=0x02FFF600
  SoftGuideRect  get=0x03F650D0  set=0x0679D0BC
  HardGuideRect  get=0x03E319F0  set=0x0679CF54
METHODS:
  RVA=0x0324B6C0  token=0x60001AA  UnityEngine.Vector3 GetLookAtPointAndSetTrackedPoint(UnityEngine.Vector3 lookAt, UnityEngine.Vector3 up, System.Single deltaTime)
  RVA=0x0679CE18  token=0x60001AC  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x04D87AD0  token=0x60001AD  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x0679CE08  token=0x60001AE  System.Single GetMaxDampTime()
  RVA=0x0324AF50  token=0x60001AF  System.Void PrePipelineMutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x031FEC00  token=0x60001B0  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x04D87AB0  token=0x60001B5  System.Void ClearOriginalRotToRect()
  RVA=0x031FC230  token=0x60001B6  System.Void RotateToScreenBounds(Cinemachine.CameraState& state, UnityEngine.Rect screenRect, UnityEngine.Vector3 trackedPoint, UnityEngine.Quaternion& rigOrientation, System.Single fov, System.Single fovH, System.Single deltaTime)
  RVA=0x031FC110  token=0x60001B7  System.Boolean ClampVerticalBounds(UnityEngine.Rect& r, UnityEngine.Vector3 dir, UnityEngine.Vector3 up, System.Single fov)
  RVA=0x0418EA10  token=0x60001B8  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineFramingTransposer
TYPE:  class
TOKEN: 0x2000046
SIZE:  0x138
EXTENDS: Cinemachine.CinemachineComponentBase
FIELDS:
  public            UnityEngine.Vector3             m_TrackedObjectOffset  // 0x20
  public            System.Single                   m_LookaheadTime  // 0x2c
  public            System.Single                   m_LookaheadSmoothing  // 0x30
  public            System.Boolean                  m_LookaheadIgnoreY  // 0x34
  public            System.Single                   m_XDamping  // 0x38
  public            System.Single                   m_YDamping  // 0x3c
  public            System.Single                   m_ZDamping  // 0x40
  public            System.Boolean                  m_TargetMovementOnly  // 0x44
  public            System.Single                   m_ScreenX  // 0x48
  public            System.Single                   m_ScreenY  // 0x4c
  public            System.Single                   m_CameraDistance  // 0x50
  public            System.Single                   m_DeadZoneWidth  // 0x54
  public            System.Single                   m_DeadZoneHeight  // 0x58
  public            System.Single                   m_DeadZoneDepth  // 0x5c
  public            System.Boolean                  m_UnlimitedSoftZone  // 0x60
  public            System.Single                   m_SoftZoneWidth  // 0x64
  public            System.Single                   m_SoftZoneHeight  // 0x68
  public            System.Single                   m_BiasX  // 0x6c
  public            System.Single                   m_BiasY  // 0x70
  public            System.Boolean                  m_CenterOnActivate  // 0x74
  public            Cinemachine.CinemachineFramingTransposer.FramingModem_GroupFramingMode  // 0x78
  public            Cinemachine.CinemachineFramingTransposer.AdjustmentModem_AdjustmentMode  // 0x7c
  public            System.Single                   m_GroupFramingSize  // 0x80
  public            System.Single                   m_MaxDollyIn  // 0x84
  public            System.Single                   m_MaxDollyOut  // 0x88
  public            System.Single                   m_MinimumDistance  // 0x8c
  public            System.Single                   m_MaximumDistance  // 0x90
  public            System.Single                   m_MinimumFOV  // 0x94
  public            System.Single                   m_MaximumFOV  // 0x98
  public            System.Single                   m_MinimumOrthoSize  // 0x9c
  public            System.Single                   m_MaximumOrthoSize  // 0xa0
  private   static  System.Single                   kMinimumCameraDistance  // const
  private   static  System.Single                   kMinimumGroupSize  // const
  private           UnityEngine.Vector3             m_PreviousCameraPosition  // 0xa4
  private           Cinemachine.Utility.PositionPredictorm_Predictor  // 0xb0
  private           UnityEngine.Vector3             <TrackedPoint>k__BackingField  // 0xb8
  private           System.Boolean                  m_InheritingPosition  // 0xc4
  private           System.Single                   m_prevFOV  // 0xc8
  private           UnityEngine.Quaternion          m_prevRotation  // 0xcc
  private           UnityEngine.Bounds              <LastBounds>k__BackingField  // 0xdc
  private           UnityEngine.Matrix4x4           <LastBoundsMatrix>k__BackingField  // 0xf4
PROPERTIES:
  SoftGuideRect  get=0x0679F488  set=0x0679F638
  HardGuideRect  get=0x0679F36C  set=0x0679F4D0
  IsValid  get=0x0679F424
  Stage  get=0x011EC580
  BodyAppliesAfterAim  get=0x02FFF600
  TrackedPoint  get=0x04D87BA0  set=0x04D87C10
  LastBounds  get=0x04D87B80  set=0x04D87BF0
  LastBoundsMatrix  get=0x04D87B50  set=0x04D87BC0
METHODS:
  RVA=0x0679EFCC  token=0x60001BF  System.Void OnValidate()
  RVA=0x0679ECFC  token=0x60001C5  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x04D87B20  token=0x60001C6  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x0679D608  token=0x60001C7  System.Single GetMaxDampTime()
  RVA=0x0679EDFC  token=0x60001C8  System.Boolean OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime, Cinemachine.CinemachineVirtualCameraBase.TransitionParams& transitionParams)
  RVA=0x0679F19C  token=0x60001C9  UnityEngine.Rect ScreenToOrtho(UnityEngine.Rect rScreen, System.Single orthoSize, System.Single aspect)
  RVA=0x0679F0B8  token=0x60001CA  UnityEngine.Vector3 OrthoOffsetToScreenBounds(UnityEngine.Vector3 targetPos2D, UnityEngine.Rect screenRect)
  RVA=0x0679DBC8  token=0x60001CF  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x0679DA2C  token=0x60001D0  System.Single GetTargetHeight(UnityEngine.Vector2 boundsSize)
  RVA=0x0679D17C  token=0x60001D1  UnityEngine.Vector3 ComputeGroupBounds(Cinemachine.ICinemachineTargetGroup group, Cinemachine.CameraState& curState)
  RVA=0x0679D620  token=0x60001D2  UnityEngine.Bounds GetScreenSpaceGroupBoundingBox(Cinemachine.ICinemachineTargetGroup group, UnityEngine.Vector3& pos, UnityEngine.Quaternion orientation)
  RVA=0x0679F270  token=0x60001D3  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineGroupComposer
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x1A0
EXTENDS: Cinemachine.CinemachineComposer
FIELDS:
  public            System.Single                   m_GroupFramingSize  // 0x110
  public            Cinemachine.CinemachineGroupComposer.FramingModem_FramingMode  // 0x114
  public            System.Single                   m_FrameDamping  // 0x118
  public            Cinemachine.CinemachineGroupComposer.AdjustmentModem_AdjustmentMode  // 0x11c
  public            System.Single                   m_MaxDollyIn  // 0x120
  public            System.Single                   m_MaxDollyOut  // 0x124
  public            System.Single                   m_MinimumDistance  // 0x128
  public            System.Single                   m_MaximumDistance  // 0x12c
  public            System.Single                   m_MinimumFOV  // 0x130
  public            System.Single                   m_MaximumFOV  // 0x134
  public            System.Single                   m_MinimumOrthoSize  // 0x138
  public            System.Single                   m_MaximumOrthoSize  // 0x13c
  private           System.Single                   m_prevFramingDistance  // 0x140
  private           System.Single                   m_prevFOV  // 0x144
  private           UnityEngine.Bounds              <LastBounds>k__BackingField  // 0x148
  private           UnityEngine.Matrix4x4           <LastBoundsMatrix>k__BackingField  // 0x160
PROPERTIES:
  LastBounds  get=0x04D87C60  set=0x04D87CB0
  LastBoundsMatrix  get=0x04D87C30  set=0x04D87C80
METHODS:
  RVA=0x067A06F0  token=0x60001D4  System.Void OnValidate()
  RVA=0x0679F6F8  token=0x60001D9  System.Single GetMaxDampTime()
  RVA=0x0679FB74  token=0x60001DA  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x0679F99C  token=0x60001DB  System.Single GetTargetHeight(UnityEngine.Vector2 boundsSize)
  RVA=0x0679F710  token=0x60001DC  UnityEngine.Bounds GetScreenSpaceGroupBoundingBox(Cinemachine.ICinemachineTargetGroup group, UnityEngine.Matrix4x4 observer, UnityEngine.Vector3& newFwd)
  RVA=0x067A07C0  token=0x60001DD  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineHardLockToTarget
TYPE:  class
TOKEN: 0x200004C
SIZE:  0x30
EXTENDS: Cinemachine.CinemachineComponentBase
FIELDS:
  public            System.Single                   m_Damping  // 0x20
  private           UnityEngine.Vector3             m_PreviousTargetPosition  // 0x24
PROPERTIES:
  IsValid  get=0x067A0994
  Stage  get=0x011EC580
METHODS:
  RVA=0x011EC8A0  token=0x60001E0  System.Single GetMaxDampTime()
  RVA=0x067A0838  token=0x60001E1  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x03695570  token=0x60001E2  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineHardLookAt
TYPE:  class
TOKEN: 0x200004D
SIZE:  0x20
EXTENDS: Cinemachine.CinemachineComponentBase
FIELDS:
PROPERTIES:
  IsValid  get=0x067A0BC0
  Stage  get=0x04D85B80
METHODS:
  RVA=0x067A09F8  token=0x60001E5  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x05393520  token=0x60001E6  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineOrbitalTransposer
TYPE:  class
TOKEN: 0x200004E
SIZE:  0x180
EXTENDS: Cinemachine.CinemachineTransposer
FIELDS:
  public            Cinemachine.CinemachineOrbitalTransposer.Headingm_Heading  // 0x98
  public            Cinemachine.AxisState.Recenteringm_RecenterToTargetHeading  // 0xa4
  public            Cinemachine.AxisState           m_XAxis  // 0xc0
  private           System.Single                   m_LegacyRadius  // 0x130
  private           System.Single                   m_LegacyHeightOffset  // 0x134
  private           System.Single                   m_LegacyHeadingBias  // 0x138
  public            System.Boolean                  m_HeadingIsSlave  // 0x13c
  private           Cinemachine.CinemachineOrbitalTransposer.UpdateHeadingDelegateHeadingUpdater  // 0x140
  private           UnityEngine.Vector3             m_LastTargetPosition  // 0x148
  private           Cinemachine.Utility.HeadingTrackermHeadingTracker  // 0x158
  private           UnityEngine.Rigidbody           m_TargetRigidBody  // 0x160
  private           UnityEngine.Transform           m_PreviousTarget  // 0x168
  private           UnityEngine.Vector3             m_LastCameraPosition  // 0x170
  private           System.Single                   m_LastHeading  // 0x17c
PROPERTIES:
  RequiresUserInput  get=0x02FFF600
METHODS:
  RVA=0x067A1FBC  token=0x60001E7  System.Void OnValidate()
  RVA=0x067A2114  token=0x60001E8  System.Single UpdateHeading(System.Single deltaTime, UnityEngine.Vector3 up, Cinemachine.AxisState& axis)
  RVA=0x067A2154  token=0x60001E9  System.Single UpdateHeading(System.Single deltaTime, UnityEngine.Vector3 up, Cinemachine.AxisState& axis, Cinemachine.AxisState.Recentering& recentering, System.Boolean isLive)
  RVA=0x067A1C80  token=0x60001EA  System.Void OnEnable()
  RVA=0x067A2288  token=0x60001EB  System.Void UpdateInputAxisProvider()
  RVA=0x067A1CC8  token=0x60001EC  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x067A0C24  token=0x60001ED  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x067A1E08  token=0x60001EE  System.Boolean OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime, Cinemachine.CinemachineVirtualCameraBase.TransitionParams& transitionParams)
  RVA=0x067A0D64  token=0x60001EF  System.Single GetAxisClosestValue(UnityEngine.Vector3 cameraPos, UnityEngine.Vector3 up)
  RVA=0x067A15A0  token=0x60001F0  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x067A1084  token=0x60001F1  UnityEngine.Vector3 GetTargetCameraPosition(UnityEngine.Vector3 worldUp)
  RVA=0x067A11E0  token=0x60001F3  System.Single GetTargetHeading(System.Single currentHeading, UnityEngine.Quaternion targetOrientation)
  RVA=0x067A2348  token=0x60001F4  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachinePOV
TYPE:  class
TOKEN: 0x2000053
SIZE:  0x148
EXTENDS: Cinemachine.CinemachineComponentBase
FIELDS:
  public            Cinemachine.CinemachinePOV.RecenterTargetModem_RecenterTarget  // 0x20
  public            Cinemachine.AxisState           m_VerticalAxis  // 0x28
  public            Cinemachine.AxisState.Recenteringm_VerticalRecentering  // 0x98
  public            Cinemachine.AxisState           m_HorizontalAxis  // 0xb8
  public            Cinemachine.AxisState.Recenteringm_HorizontalRecentering  // 0x128
  public            System.Boolean                  m_ApplyBeforeBody  // 0x144
PROPERTIES:
  IsValid  get=0x067A3450
  Stage  get=0x04D85B80
  RequiresUserInput  get=0x02FFF600
METHODS:
  RVA=0x067A2C60  token=0x60001FD  System.Void OnValidate()
  RVA=0x067A2AB8  token=0x60001FE  System.Void OnEnable()
  RVA=0x067A30FC  token=0x60001FF  System.Void UpdateInputAxisProvider()
  RVA=0x041E1670  token=0x6000200  System.Void PrePipelineMutateCameraState(Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x067A2824  token=0x6000201  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x067A25F4  token=0x6000202  UnityEngine.Vector2 GetRecenterTarget()
  RVA=0x04D87CD0  token=0x6000203  System.Single NormalizeAngle(System.Single angle)
  RVA=0x067A25D4  token=0x6000204  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x067A2AC0  token=0x6000205  System.Boolean OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime, Cinemachine.CinemachineVirtualCameraBase.TransitionParams& transitionParams)
  RVA=0x067A2CE0  token=0x6000207  System.Void SetAxesForRotation(UnityEngine.Quaternion targetRot)
  RVA=0x067A31F4  token=0x6000208  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineSameAsFollowTarget
TYPE:  class
TOKEN: 0x2000055
SIZE:  0x38
EXTENDS: Cinemachine.CinemachineComponentBase
FIELDS:
  public            System.Single                   m_Damping  // 0x20
  private           UnityEngine.Quaternion          m_PreviousReferenceOrientation  // 0x24
PROPERTIES:
  IsValid  get=0x067AD968
  Stage  get=0x04D85B80
METHODS:
  RVA=0x011EC8A0  token=0x600020B  System.Single GetMaxDampTime()
  RVA=0x067AD870  token=0x600020C  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x0418E810  token=0x600020D  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineTrackedDolly
TYPE:  class
TOKEN: 0x2000056
SIZE:  0x88
EXTENDS: Cinemachine.CinemachineComponentBase
FIELDS:
  public            Cinemachine.CinemachinePathBase m_Path  // 0x20
  public            System.Single                   m_PathPosition  // 0x28
  public            Cinemachine.CinemachinePathBase.PositionUnitsm_PositionUnits  // 0x2c
  public            UnityEngine.Vector3             m_PathOffset  // 0x30
  public            System.Single                   m_XDamping  // 0x3c
  public            System.Single                   m_YDamping  // 0x40
  public            System.Single                   m_ZDamping  // 0x44
  public            Cinemachine.CinemachineTrackedDolly.CameraUpModem_CameraUp  // 0x48
  public            System.Single                   m_PitchDamping  // 0x4c
  public            System.Single                   m_YawDamping  // 0x50
  public            System.Single                   m_RollDamping  // 0x54
  public            Cinemachine.CinemachineTrackedDolly.AutoDollym_AutoDolly  // 0x58
  private           System.Single                   m_PreviousPathPosition  // 0x68
  private           UnityEngine.Quaternion          m_PreviousOrientation  // 0x6c
  private           UnityEngine.Vector3             m_PreviousCameraPosition  // 0x7c
PROPERTIES:
  IsValid  get=0x067AE4C0
  Stage  get=0x011EC580
  AngularDamping  get=0x067AE450
METHODS:
  RVA=0x067ADBF8  token=0x6000210  System.Single GetMaxDampTime()
  RVA=0x067ADC50  token=0x6000211  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x067AD9CC  token=0x6000212  UnityEngine.Quaternion GetCameraOrientationAtPathPoint(UnityEngine.Quaternion pathOrientation, UnityEngine.Vector3 up)
  RVA=0x0418E8C0  token=0x6000214  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineTransposer
TYPE:  class
TOKEN: 0x2000059
SIZE:  0x98
EXTENDS: Cinemachine.CinemachineComponentBase
FIELDS:
  public            Cinemachine.CinemachineTransposer.BindingModem_BindingMode  // 0x20
  public            UnityEngine.Vector3             m_FollowOffset  // 0x24
  public            System.Single                   m_XDamping  // 0x30
  public            System.Single                   m_YDamping  // 0x34
  public            System.Single                   m_ZDamping  // 0x38
  public            Cinemachine.CinemachineTransposer.AngularDampingModem_AngularDampingMode  // 0x3c
  public            System.Single                   m_PitchDamping  // 0x40
  public            System.Single                   m_YawDamping  // 0x44
  public            System.Single                   m_RollDamping  // 0x48
  public            System.Single                   m_AngularDamping  // 0x4c
  private           System.Boolean                  <HideOffsetInInspector>k__BackingField  // 0x50
  private           UnityEngine.Vector3             m_PreviousTargetPosition  // 0x54
  private           UnityEngine.Quaternion          m_PreviousReferenceOrientation  // 0x60
  private           UnityEngine.Quaternion          m_targetOrientationOnAssign  // 0x70
  private           UnityEngine.Vector3             m_PreviousOffset  // 0x80
  private           UnityEngine.Transform           m_previousTarget  // 0x90
PROPERTIES:
  HideOffsetInInspector  get=0x04D86800  set=0x04D86840
  EffectiveOffset  get=0x067AFC3C
  IsValid  get=0x067AFC74
  Stage  get=0x011EC580
  Damping  get=0x067AFC1C
  AngularDamping  get=0x067AFBA4
METHODS:
  RVA=0x067AF3E4  token=0x6000216  System.Void OnValidate()
  RVA=0x067AE688  token=0x600021C  System.Single GetMaxDampTime()
  RVA=0x067AF088  token=0x600021D  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x067AF324  token=0x600021E  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x067AE508  token=0x600021F  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x067AEF54  token=0x6000220  System.Void InitPrevFrameStateInfo(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x067AF414  token=0x6000221  System.Void TrackTarget(System.Single deltaTime, UnityEngine.Vector3 up, UnityEngine.Vector3 desiredCameraOffset, UnityEngine.Vector3& outTargetPosition, UnityEngine.Quaternion& outTargetOrient)
  RVA=0x067AE708  token=0x6000222  UnityEngine.Vector3 GetOffsetForMinimumTargetDistance(UnityEngine.Vector3 dampedTargetPos, UnityEngine.Vector3 cameraOffset, UnityEngine.Vector3 cameraFwd, UnityEngine.Vector3 up, UnityEngine.Vector3 actualTargetPos)
  RVA=0x067AEE40  token=0x6000225  UnityEngine.Vector3 GetTargetCameraPosition(UnityEngine.Vector3 worldUp)
  RVA=0x067AEAD8  token=0x6000226  UnityEngine.Quaternion GetReferenceOrientation(UnityEngine.Vector3 worldUp)
  RVA=0x0418E950  token=0x6000227  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.AxisState
TYPE:  sealed struct
TOKEN: 0x200005C
SIZE:  0x80
FIELDS:
  public            System.Single                   Value  // 0x10
  public            Cinemachine.AxisState.SpeedMode m_SpeedMode  // 0x14
  public            System.Single                   m_MaxSpeed  // 0x18
  public            System.Single                   m_AccelTime  // 0x1c
  public            System.Single                   m_DecelTime  // 0x20
  public            System.String                   m_InputAxisName  // 0x28
  public            System.Single                   m_InputAxisValue  // 0x30
  public            System.Boolean                  m_InvertInput  // 0x34
  public            System.Single                   m_MinValue  // 0x38
  public            System.Single                   m_MaxValue  // 0x3c
  public            System.Boolean                  m_Wrap  // 0x40
  public            Cinemachine.AxisState.Recenteringm_Recentering  // 0x44
  private           System.Single                   m_CurrentSpeed  // 0x60
  private           System.Single                   m_LastUpdateTime  // 0x64
  private           System.Int32                    m_LastUpdateFrame  // 0x68
  private   static  System.Single                   Epsilon  // const
  private           Cinemachine.AxisState.IInputAxisProviderm_InputAxisProvider  // 0x70
  private           System.Int32                    m_InputAxisIndex  // 0x78
  private           System.Boolean                  <ValueRangeLocked>k__BackingField  // 0x7c
  private           System.Boolean                  <HasRecentering>k__BackingField  // 0x7d
PROPERTIES:
  ValueRangeLocked  set=0x04D87F30
  HasRecentering  set=0x04D87F20
METHODS:
  RVA=0x067AA840  token=0x6000228  System.Void .ctor(System.Single minValue, System.Single maxValue, System.Boolean wrap, System.Boolean rangeLocked, System.Single maxSpeed, System.Single accelTime, System.Single decelTime, System.String name, System.Boolean invert)
  RVA=0x067AA7F8  token=0x6000229  System.Void Validate()
  RVA=0x04D87F10  token=0x600022A  System.Void Reset()
  RVA=0x067AA3EC  token=0x600022B  System.Void SetInputAxisProvider(System.Int32 axis, Cinemachine.AxisState.IInputAxisProvider provider)
  RVA=0x067AA3FC  token=0x600022C  System.Boolean Update(System.Single deltaTime)
  RVA=0x067AA124  token=0x600022D  System.Single ClampValue(System.Single v)
  RVA=0x067AA20C  token=0x600022E  System.Boolean MaxSpeedUpdate(System.Single input, System.Single deltaTime)
  RVA=0x067AA194  token=0x600022F  System.Single GetMaxSpeed()
END_CLASS

CLASS: Cinemachine.CameraState
TYPE:  sealed struct
TOKEN: 0x2000060
SIZE:  0x130
FIELDS:
  public            Cinemachine.CameraState.HGDOFParamsDofParams  // 0x10
  public            Cinemachine.LensSettings        Lens  // 0x30
  public            UnityEngine.Vector3             ReferenceUp  // 0x60
  public            UnityEngine.Vector3             ReferenceLookAt  // 0x6c
  public            UnityEngine.Vector3             Pivot  // 0x78
  public            UnityEngine.Vector3             NoOffsetPivot  // 0x84
  public    static  UnityEngine.Vector3             kNoPoint  // static @ 0x0
  public            UnityEngine.Vector3             RawPosition  // 0x90
  public            UnityEngine.Quaternion          RawOrientation  // 0x9c
  public            UnityEngine.Vector3             PositionDampingBypass  // 0xac
  public            System.Single                   ShotQuality  // 0xb8
  public            UnityEngine.Vector3             PositionCorrection  // 0xbc
  public            UnityEngine.Quaternion          OrientationCorrection  // 0xc8
  public            Cinemachine.CameraState.BlendHintValueBlendHint  // 0xd8
  private           Cinemachine.CameraState.CustomBlendablemCustom0  // 0xe0
  private           Cinemachine.CameraState.CustomBlendablemCustom1  // 0xf0
  private           Cinemachine.CameraState.CustomBlendablemCustom2  // 0x100
  private           Cinemachine.CameraState.CustomBlendablemCustom3  // 0x110
  private           System.Collections.Generic.List<Cinemachine.CameraState.CustomBlendable>m_CustomOverflow  // 0x120
  private           System.Int32                    <NumCustomBlendables>k__BackingField  // 0x128
PROPERTIES:
  HasLookAt  get=0x05391AA8
  CorrectedPosition  get=0x031FF6B0
  CorrectedOrientation  get=0x03249990
  FinalPosition  get=0x031FF6B0
  FinalOrientation  get=0x03249910
  Default  get=0x0324BFC0
  NumCustomBlendables  get=0x04D87FF0  set=0x04D88000
METHODS:
  RVA=0x067ABD30  token=0x6000240  Cinemachine.CameraState.CustomBlendable GetCustomBlendable(System.Int32 index)
  RVA=0x067ABBAC  token=0x6000241  System.Int32 FindCustomBlendable(UnityEngine.Object custom)
  RVA=0x067ABA00  token=0x6000242  System.Void AddCustomBlendable(Cinemachine.CameraState.CustomBlendable b)
  RVA=0x03245B00  token=0x6000243  Cinemachine.CameraState Lerp(Cinemachine.CameraState& stateA, Cinemachine.CameraState& stateB, System.Single t)
  RVA=0x067ABE1C  token=0x6000244  System.Single InterpolateFOV(System.Single fovA, System.Single fovB, System.Single dA, System.Single dB, System.Single t)
  RVA=0x03246C90  token=0x6000245  UnityEngine.Vector3 ApplyPosBlendHint(UnityEngine.Vector3 posA, Cinemachine.CameraState.BlendHintValue hintA, UnityEngine.Vector3 posB, Cinemachine.CameraState.BlendHintValue hintB, UnityEngine.Vector3 original, UnityEngine.Vector3 blended)
  RVA=0x03246C60  token=0x6000246  UnityEngine.Quaternion ApplyRotBlendHint(UnityEngine.Quaternion rotA, Cinemachine.CameraState.BlendHintValue hintA, UnityEngine.Quaternion rotB, Cinemachine.CameraState.BlendHintValue hintB, UnityEngine.Quaternion original, UnityEngine.Quaternion blended)
  RVA=0x03245530  token=0x6000247  UnityEngine.Vector3 InterpolatePosition(UnityEngine.Vector3 posA, UnityEngine.Vector3 pivotA, UnityEngine.Vector3 posB, UnityEngine.Vector3 pivotB, System.Single t, System.Int32& positionCylindricalDir)
  RVA=0x04D59CF0  token=0x6000248  System.Void .cctor()
END_CLASS

CLASS: Cinemachine.CinemachineBlend
TYPE:  class
TOKEN: 0x2000064
SIZE:  0x398
FIELDS:
  private           Cinemachine.ICinemachineCamera  m_CamA  // 0x10
  public            Cinemachine.ICinemachineCamera  CamB  // 0x18
  public            System.Boolean                  useSimpleInertialization  // 0x20
  public            System.Boolean                  InitStateGot  // 0x21
  public            Cinemachine.CameraState         InitStateA  // 0x28
  public            Cinemachine.CameraState         CurrStateA  // 0x148
  public            Cinemachine.CameraState         InitStateB  // 0x268
  public            UnityEngine.AnimationCurve      BlendCurve  // 0x388
  public            System.Single                   TimeInBlend  // 0x390
  public            System.Single                   Duration  // 0x394
PROPERTIES:
  CamA  get=0x02B2ECC0  set=0x033C72F0
  BlendWeight  get=0x067ABF48
  IsValid  get=0x067AC190
  IsComplete  get=0x067AC168
  Description  get=0x067ABFC0
  State  get=0x033CA470
METHODS:
  RVA=0x04D88010  token=0x600024C  System.Void StartSimpleInertialization()
  RVA=0x033C9BF0  token=0x6000251  System.Boolean Uses(Cinemachine.ICinemachineCamera cam)
  RVA=0x03FCFE70  token=0x6000252  System.Void .ctor(Cinemachine.ICinemachineCamera a, Cinemachine.ICinemachineCamera b, UnityEngine.AnimationCurve curve, System.Single duration, System.Single t)
  RVA=0x033C9F50  token=0x6000253  System.Void UpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
END_CLASS

CLASS: Cinemachine.CinemachineBlendDefinition
TYPE:  sealed struct
TOKEN: 0x2000065
SIZE:  0x28
FIELDS:
  public            Cinemachine.CinemachineBlendDefinition.Stylem_Style  // 0x10
  public            System.Single                   m_Time  // 0x14
  public            UnityEngine.AnimationCurve      m_CustomCurve  // 0x18
  private   static  UnityEngine.AnimationCurve[]    sStandardCurves  // static @ 0x0
  public            System.Boolean                  m_UseSimpleInertialization  // 0x20
PROPERTIES:
  BlendTime  get=0x03245AF0
  BlendCurve  get=0x03247400
METHODS:
  RVA=0x05396C64  token=0x6000256  System.Void .ctor(Cinemachine.CinemachineBlendDefinition.Style style, System.Single time)
  RVA=0x046FFE80  token=0x6000257  System.Void CreateStandardCurves()
END_CLASS

CLASS: Cinemachine.StaticPointVirtualCamera
TYPE:  class
TOKEN: 0x2000067
SIZE:  0x150
IMPLEMENTS: Cinemachine.ICinemachineCamera
FIELDS:
  private           System.String                   <Name>k__BackingField  // 0x10
  private           System.Int32                    <Priority>k__BackingField  // 0x18
  private           UnityEngine.Transform           <LookAt>k__BackingField  // 0x20
  private           UnityEngine.Transform           <Follow>k__BackingField  // 0x28
  private           Cinemachine.CameraState         <State>k__BackingField  // 0x30
PROPERTIES:
  Name  get=0x02B2ECC0  set=0x053908C0
  Priority  get=0x011EF5B0
  LookAt  get=0x04D862C0
  Follow  get=0x04D86240
  State  get=0x04D87F70  set=0x067AB4DC
  VirtualCameraGameObject  get=0x011EC580
  IsValid  get=0x02FFF600
  ParentCamera  get=0x011EC580
METHODS:
  RVA=0x067B2390  token=0x6000259  System.Void .ctor(Cinemachine.CameraState& state, System.String name)
  RVA=0x012081B0  token=0x6000264  System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly)
  RVA=0x041E1670  token=0x6000265  System.Void UpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x041E1670  token=0x6000266  System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
END_CLASS

CLASS: Cinemachine.BlendSourceVirtualCamera
TYPE:  class
TOKEN: 0x2000068
SIZE:  0x150
IMPLEMENTS: Cinemachine.ICinemachineCamera
FIELDS:
  private           Cinemachine.CinemachineBlend    <Blend>k__BackingField  // 0x10
  private           System.Int32                    <Priority>k__BackingField  // 0x18
  private           UnityEngine.Transform           <LookAt>k__BackingField  // 0x20
  private           UnityEngine.Transform           <Follow>k__BackingField  // 0x28
  private           Cinemachine.CameraState         <State>k__BackingField  // 0x30
PROPERTIES:
  Blend  get=0x02B2ECC0  set=0x053908C0
  Name  get=0x067AB4B0
  Priority  get=0x011EF5B0
  LookAt  get=0x04D862C0
  Follow  get=0x04D86240
  State  get=0x04D87F70  set=0x067AB4DC
  VirtualCameraGameObject  get=0x011EC580
  IsValid  get=0x067AB498
  ParentCamera  get=0x011EC580
METHODS:
  RVA=0x053908C0  token=0x6000267  System.Void .ctor(Cinemachine.CinemachineBlend blend)
  RVA=0x067AB3A4  token=0x6000273  System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly)
  RVA=0x067AB3C4  token=0x6000274  System.Void UpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x041E1670  token=0x6000275  System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
END_CLASS

CLASS: Cinemachine.CinemachineBlenderSettings
TYPE:  sealed class
TOKEN: 0x2000069
SIZE:  0x28
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public            Cinemachine.CinemachineBlenderSettings.CustomBlend[]m_CustomBlends  // 0x18
  public            Cinemachine.CinemachineBlenderSettings.CustomBlendGroup[]m_CustomBlendGroups  // 0x20
  public    static  System.String                   kBlendFromAnyCameraLabel  // const
METHODS:
  RVA=0x040F7B40  token=0x6000276  Cinemachine.CinemachineBlendDefinition GetBlendForVirtualCameras(System.String fromCameraName, System.String toCameraName, Cinemachine.CinemachineBlendDefinition defaultBlend)
  RVA=0x03573370  token=0x6000277  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineComponentBase
TYPE:  abstract class
TOKEN: 0x200006C
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  protected static  System.Single                   Epsilon  // const
  private           Cinemachine.CinemachineVirtualCameraBasem_vcamOwner  // 0x18
PROPERTIES:
  VirtualCamera  get=0x031FD750
  FollowTarget  get=0x031FC8C0
  LookAtTarget  get=0x031FD590
  AbstractFollowTargetGroup  get=0x067AC1D8
  FollowTargetGroup  get=0x067AC220
  FollowTargetPosition  get=0x0324B8D0
  FollowTargetRotation  get=0x0324BBA0
  AbstractLookAtTargetGroup  get=0x067AC1FC
  LookAtTargetGroup  get=0x067AC264
  LookAtTargetPosition  get=0x067AC2A8
  LookAtTargetRotation  get=0x0324BA40
  VcamState  get=0x067AC430
  IsValid  get=-1  // abstract
  Stage  get=-1  // abstract
  BodyAppliesAfterAim  get=0x012081B0
  RequiresUserInput  get=0x012081B0
METHODS:
  RVA=0x041E1670  token=0x6000286  System.Void PrePipelineMutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=-1  // abstract  token=0x6000289  System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x012081B0  token=0x600028A  System.Boolean OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime, Cinemachine.CinemachineVirtualCameraBase.TransitionParams& transitionParams)
  RVA=0x041E1670  token=0x600028B  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x041E1670  token=0x600028C  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x04D879D0  token=0x600028D  System.Single GetMaxDampTime()
  RVA=0x05393520  token=0x600028F  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineCore
TYPE:  sealed class
TOKEN: 0x200006D
SIZE:  0x48
FIELDS:
  public    static readonly System.Int32                    kStreamingVersion  // static @ 0x0
  public    static readonly System.String                   kVersionString  // static @ 0x8
  private   static  Cinemachine.CinemachineCore     sInstance  // static @ 0x10
  public    static  System.Boolean                  sShowHiddenObjects  // static @ 0x18
  public    static  Cinemachine.CinemachineCore.AxisInputDelegateGetInputAxis  // static @ 0x20
  public    static  System.Single                   UniformDeltaTimeOverride  // static @ 0x28
  public    static  System.Single                   CurrentTimeOverride  // static @ 0x2c
  public    static  Cinemachine.CinemachineCore.GetBlendOverrideDelegateGetBlendOverride  // static @ 0x30
  public    static  Cinemachine.CinemachineBrain.BrainEventCameraUpdatedEvent  // static @ 0x38
  public    static  Cinemachine.CinemachineBrain.BrainEventCameraCutEvent  // static @ 0x40
  private           System.Collections.Generic.List<Cinemachine.CinemachineBrain>mActiveBrains  // 0x10
  private           System.Collections.Generic.List<Cinemachine.CinemachineVirtualCameraBase>mActiveCameras  // 0x18
  private           System.Boolean                  m_ActiveCamerasAreSorted  // 0x20
  private           System.Int32                    m_ActivationSequence  // 0x24
  private           System.Collections.Generic.List<System.Collections.Generic.List<Cinemachine.CinemachineVirtualCameraBase>>mAllCameras  // 0x28
  private           Cinemachine.CinemachineVirtualCameraBasemRoundRobinVcamLastFrame  // 0x30
  private   static  System.Single                   s_LastUpdateTime  // static @ 0x48
  private   static  System.Int32                    s_FixedFrameCount  // static @ 0x4c
  private           System.Collections.Generic.Dictionary<Cinemachine.CinemachineVirtualCameraBase,Cinemachine.CinemachineCore.UpdateStatus>mUpdateStatus  // 0x38
  private           Cinemachine.CinemachineCore.UpdateFilterm_CurrentUpdateFilter  // 0x40
PROPERTIES:
  Instance  get=0x033C8EA0
  DeltaTime  get=0x067ACB74
  CurrentTime  get=0x033C7B20
  BrainCount  get=0x03140AD0
  VirtualCameraCount  get=0x033C9030
METHODS:
  RVA=0x033C91D0  token=0x6000294  Cinemachine.CinemachineBrain GetActiveBrain(System.Int32 index)
  RVA=0x03FCF5F0  token=0x6000295  System.Void AddActiveBrain(Cinemachine.CinemachineBrain brain)
  RVA=0x03FCF660  token=0x6000296  System.Void RemoveActiveBrain(Cinemachine.CinemachineBrain brain)
  RVA=0x033C8A80  token=0x6000298  Cinemachine.CinemachineVirtualCameraBase GetVirtualCamera(System.Int32 index)
  RVA=0x0413BBA0  token=0x6000299  System.Void AddActiveCamera(Cinemachine.CinemachineVirtualCameraBase vcam)
  RVA=0x0413BB40  token=0x600029A  System.Void RemoveActiveCamera(Cinemachine.CinemachineVirtualCameraBase vcam)
  RVA=0x067AC598  token=0x600029B  System.Void CameraDestroyed(Cinemachine.CinemachineVirtualCameraBase vcam)
  RVA=0x0413BBE0  token=0x600029C  System.Void CameraEnabled(Cinemachine.CinemachineVirtualCameraBase vcam)
  RVA=0x0413B8D0  token=0x600029D  System.Void CameraDisabled(Cinemachine.CinemachineVirtualCameraBase vcam)
  RVA=0x033C7BC0  token=0x600029E  System.Void UpdateAllActiveVirtualCameras(System.Int32 layerMask, UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x033C9C60  token=0x600029F  System.Void UpdateVirtualCamera(Cinemachine.CinemachineVirtualCameraBase vcam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x04CE97C0  token=0x60002A0  System.Void InitializeModule()
  RVA=0x067AC8AC  token=0x60002A1  UnityEngine.Transform GetUpdateTarget(Cinemachine.CinemachineVirtualCameraBase vcam)
  RVA=0x067AC9A4  token=0x60002A2  Cinemachine.UpdateTracker.UpdateClock GetVcamUpdateStatus(Cinemachine.CinemachineVirtualCameraBase vcam)
  RVA=0x033C9050  token=0x60002A3  System.Boolean IsLive(Cinemachine.ICinemachineCamera vcam)
  RVA=0x067ACA20  token=0x60002A4  System.Boolean IsLiveInBlend(Cinemachine.ICinemachineCamera vcam)
  RVA=0x067AC664  token=0x60002A5  System.Void GenerateCameraActivationEvent(Cinemachine.ICinemachineCamera vcam, Cinemachine.ICinemachineCamera vcamFrom)
  RVA=0x067AC75C  token=0x60002A6  System.Void GenerateCameraCutEvent(Cinemachine.ICinemachineCamera vcam)
  RVA=0x033C8210  token=0x60002A7  Cinemachine.CinemachineBrain FindPotentialTargetBrain(Cinemachine.CinemachineVirtualCameraBase vcam)
  RVA=0x067ACAE0  token=0x60002A8  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x0413B2C0  token=0x60002A9  System.Void .ctor()
  RVA=0x03FCFB10  token=0x60002AA  System.Void .cctor()
END_CLASS

CLASS: Cinemachine.CinemachineExtension
TYPE:  abstract class
TOKEN: 0x2000074
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  protected static  System.Single                   Epsilon  // const
  private           Cinemachine.CinemachineVirtualCameraBasem_vcamOwner  // 0x18
  private           System.Collections.Generic.Dictionary<Cinemachine.ICinemachineCamera,System.Object>mExtraState  // 0x20
PROPERTIES:
  VirtualCamera  get=0x03FD98F0
  RequiresUserInput  get=0x012081B0
METHODS:
  RVA=0x04CAE4D0  token=0x60002B4  System.Void Awake()
  RVA=0x041E1670  token=0x60002B5  System.Void OnEnable()
  RVA=0x067ACBE4  token=0x60002B6  System.Void OnDestroy()
  RVA=0x04CAE4D0  token=0x60002B7  System.Void EnsureStarted()
  RVA=0x03FD9750  token=0x60002B8  System.Void ConnectToVcam(System.Boolean connect)
  RVA=0x041E1670  token=0x60002B9  System.Void PrePipelineMutateCameraStateCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CameraState& curState, System.Single deltaTime)
  RVA=0x05391850  token=0x60002BA  System.Void InvokePostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=-1  // abstract  token=0x60002BB  System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x041E1670  token=0x60002BC  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x041E1670  token=0x60002BD  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x012081B0  token=0x60002BE  System.Boolean OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x04D879D0  token=0x60002BF  System.Single GetMaxDampTime()
  RVA=-1  // generic def  token=0x60002C1  T GetExtraState(Cinemachine.ICinemachineCamera vcam)
  RVA=-1  // generic def  token=0x60002C2  System.Collections.Generic.List<T> GetAllExtraStates()
  RVA=0x05393520  token=0x60002C3  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachinePathBase
TYPE:  abstract class
TOKEN: 0x2000075
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Int32                    m_Resolution  // 0x18
  public            Cinemachine.CinemachinePathBase.Appearancem_Appearance  // 0x20
  private           System.Single[]                 m_DistanceToPos  // 0x28
  private           System.Single[]                 m_PosToDistance  // 0x30
  private           System.Int32                    m_CachedSampleSteps  // 0x38
  private           System.Single                   m_PathLength  // 0x3c
  private           System.Single                   m_cachedPosStepSize  // 0x40
  private           System.Single                   m_cachedDistanceStepSize  // 0x44
PROPERTIES:
  MinPos  get=-1  // abstract
  MaxPos  get=-1  // abstract
  Looped  get=-1  // abstract
  DistanceCacheSampleStepsPerSegment  get=-1  // abstract
  PathLength  get=0x067AD81C
METHODS:
  RVA=0x067AD588  token=0x60002C7  System.Single StandardizePos(System.Single pos)
  RVA=-1  // abstract  token=0x60002C8  UnityEngine.Vector3 EvaluatePosition(System.Single pos)
  RVA=-1  // abstract  token=0x60002C9  UnityEngine.Vector3 EvaluateTangent(System.Single pos)
  RVA=-1  // abstract  token=0x60002CA  UnityEngine.Quaternion EvaluateOrientation(System.Single pos)
  RVA=0x067ACD60  token=0x60002CB  System.Single FindClosestPoint(UnityEngine.Vector3 p, System.Int32 startSegment, System.Int32 searchRadius, System.Int32 stepsPerSegment)
  RVA=0x067AD1D8  token=0x60002CC  System.Single MinUnit(Cinemachine.CinemachinePathBase.PositionUnits units)
  RVA=0x067AD1A0  token=0x60002CD  System.Single MaxUnit(Cinemachine.CinemachinePathBase.PositionUnits units)
  RVA=0x067AD62C  token=0x60002CE  System.Single StandardizeUnit(System.Single pos, Cinemachine.CinemachinePathBase.PositionUnits units)
  RVA=0x067ACCB8  token=0x60002CF  UnityEngine.Vector3 EvaluatePositionAtUnit(System.Single pos, Cinemachine.CinemachinePathBase.PositionUnits units)
  RVA=0x067ACD0C  token=0x60002D0  UnityEngine.Vector3 EvaluateTangentAtUnit(System.Single pos, Cinemachine.CinemachinePathBase.PositionUnits units)
  RVA=0x067ACC6C  token=0x60002D1  UnityEngine.Quaternion EvaluateOrientationAtUnit(System.Single pos, Cinemachine.CinemachinePathBase.PositionUnits units)
  RVA=0x067AD170  token=0x60002D3  System.Void InvalidateDistanceCache()
  RVA=0x067ACBF4  token=0x60002D4  System.Boolean DistanceCacheIsValid()
  RVA=0x067AD4F4  token=0x60002D6  System.Single StandardizePathDistance(System.Single distance)
  RVA=0x067AD6A4  token=0x60002D7  System.Single ToNativePathUnits(System.Single pos, Cinemachine.CinemachinePathBase.PositionUnits units)
  RVA=0x067AD074  token=0x60002D8  System.Single FromPathNativeUnits(System.Single pos, Cinemachine.CinemachinePathBase.PositionUnits units)
  RVA=0x067AD200  token=0x60002D9  System.Void ResamplePath(System.Int32 stepsPerSegment)
  RVA=0x067AD7C0  token=0x60002DA  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.AxisStatePropertyAttribute
TYPE:  sealed class
TOKEN: 0x2000078
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002DC  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.OrbitalTransposerHeadingPropertyAttribute
TYPE:  sealed class
TOKEN: 0x2000079
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002DD  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.VcamTargetPropertyAttribute
TYPE:  sealed class
TOKEN: 0x200007A
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002DE  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineBlendDefinitionPropertyAttribute
TYPE:  sealed class
TOKEN: 0x200007B
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002DF  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.SaveDuringPlayAttribute
TYPE:  sealed class
TOKEN: 0x200007C
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002E0  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.NoSaveDuringPlayAttribute
TYPE:  sealed class
TOKEN: 0x200007D
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002E1  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.TagFieldAttribute
TYPE:  sealed class
TOKEN: 0x200007E
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002E2  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.NoiseSettingsPropertyAttribute
TYPE:  sealed class
TOKEN: 0x200007F
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002E3  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineEmbeddedAssetPropertyAttribute
TYPE:  sealed class
TOKEN: 0x2000080
SIZE:  0x18
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public            System.Boolean                  WarnIfNull  // 0x10
METHODS:
  RVA=0x04D86130  token=0x60002E4  System.Void .ctor(System.Boolean warnIfNull)
END_CLASS

CLASS: Cinemachine.DocumentationSortingAttribute
TYPE:  sealed class
TOKEN: 0x2000081
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           Cinemachine.DocumentationSortingAttribute.Level<Category>k__BackingField  // 0x10
PROPERTIES:
  Category  set=0x014F51F0
METHODS:
  RVA=0x014F51F0  token=0x60002E6  System.Void .ctor(Cinemachine.DocumentationSortingAttribute.Level category)
END_CLASS

CLASS: Cinemachine.CinemachineVirtualCameraBase
TYPE:  abstract class
TOKEN: 0x2000083
SIZE:  0xA8
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Cinemachine.ICinemachineCamera
FIELDS:
  public            System.String[]                 m_ExcludedPropertiesInInspector  // 0x18
  public            Cinemachine.CinemachineCore.Stage[]m_LockStageInInspector  // 0x20
  private           System.Int32                    m_ValidatingStreamVersion  // 0x28
  private           System.Boolean                  m_OnValidateCalled  // 0x2c
  private           System.Int32                    m_StreamingVersion  // 0x30
  public            System.Int32                    m_Priority  // 0x34
  private           System.Int32                    m_ActivationId  // 0x38
  public            System.Single                   FollowTargetAttachment  // 0x3c
  public            System.Single                   LookAtTargetAttachment  // 0x40
  protected         System.Boolean                  _ignoreLookAtBlend  // 0x44
  public            Cinemachine.CinemachineVirtualCameraBase.StandbyUpdateModem_StandbyUpdate  // 0x48
  private           System.Collections.Generic.List<Cinemachine.CinemachineExtension><mExtensions>k__BackingField  // 0x50
  private           System.Boolean                  <PreviousStateIsValid>k__BackingField  // 0x58
  private           System.Boolean                  m_WasStarted  // 0x59
  private           System.Boolean                  mSlaveStatusUpdated  // 0x5a
  private           Cinemachine.CinemachineVirtualCameraBasem_parentVcam  // 0x60
  private           System.Int32                    m_QueuePriority  // 0x68
  private           UnityEngine.Transform           m_CachedFollowTarget  // 0x70
  private           Cinemachine.CinemachineVirtualCameraBasem_CachedFollowTargetVcam  // 0x78
  private           Cinemachine.ICinemachineTargetGroupm_CachedFollowTargetGroup  // 0x80
  private           UnityEngine.Transform           m_CachedLookAtTarget  // 0x88
  private           Cinemachine.CinemachineVirtualCameraBasem_CachedLookAtTargetVcam  // 0x90
  private           Cinemachine.ICinemachineTargetGroupm_CachedLookAtTargetGroup  // 0x98
  private           System.Boolean                  <FollowTargetChanged>k__BackingField  // 0xa0
  private           System.Boolean                  <LookAtTargetChanged>k__BackingField  // 0xa1
PROPERTIES:
  ValidatingStreamVersion  get=0x0413B590  set=0x011F2AA0
  mExtensions  get=0x04D86270  set=0x02FDAEA0
  Name  get=0x05396F58
  Description  get=0x067B05F8
  Priority  get=0x04D86730  set=0x04D86760
  VirtualCameraGameObject  get=0x033CA6B0
  IsValid  get=0x033C9B80
  State  get=-1  // abstract
  ParentCamera  get=0x031FE360
  LookAt  get=-1  // abstract  set=-1  // abstract
  Follow  get=-1  // abstract  set=-1  // abstract
  PreviousStateIsValid  get=0x04D86F70  set=0x04D86F80
  FollowTargetChanged  get=0x04D88020  set=0x04D88040
  LookAtTargetChanged  get=0x04D88030  set=0x04D88050
  AbstractFollowTargetGroup  get=0x04D86260
  FollowTargetAsVcam  get=0x04D862A0
  AbstractLookAtTargetGroup  get=0x04D85EE0
  LookAtTargetAsVcam  get=0x04D86220
METHODS:
  RVA=0x067B02C4  token=0x60002E9  System.Single GetMaxDampTime()
  RVA=0x067AFF74  token=0x60002EA  System.Single DetachedFollowTargetDamp(System.Single initial, System.Single dampTime, System.Single deltaTime)
  RVA=0x031FA6A0  token=0x60002EB  UnityEngine.Vector3 DetachedFollowTargetDamp(UnityEngine.Vector3 initial, UnityEngine.Vector3 dampTime, System.Single deltaTime)
  RVA=0x067AFEF4  token=0x60002EC  UnityEngine.Vector3 DetachedFollowTargetDamp(UnityEngine.Vector3 initial, System.Single dampTime, System.Single deltaTime)
  RVA=0x031FAEE0  token=0x60002ED  System.Single DetachedLookAtTargetDamp(System.Single initial, System.Single dampTime, System.Single deltaTime)
  RVA=0x067AFFCC  token=0x60002EE  UnityEngine.Vector3 DetachedLookAtTargetDamp(UnityEngine.Vector3 initial, UnityEngine.Vector3 dampTime, System.Single deltaTime)
  RVA=0x067B00BC  token=0x60002EF  UnityEngine.Vector3 DetachedLookAtTargetDamp(UnityEngine.Vector3 initial, System.Single dampTime, System.Single deltaTime)
  RVA=0x03FD9990  token=0x60002F0  System.Void AddExtension(Cinemachine.CinemachineExtension extension)
  RVA=0x067B048C  token=0x60002F1  System.Void RemoveExtension(Cinemachine.CinemachineExtension extension)
  RVA=0x031FE0B0  token=0x60002F4  System.Void InvokePostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& newState, System.Single deltaTime)
  RVA=0x031FC9B0  token=0x60002F5  System.Void InvokePrePipelineMutateCameraStateCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CameraState& newState, System.Single deltaTime)
  RVA=0x03E697F0  token=0x60002F6  System.Boolean InvokeOnTransitionInExtensions(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x0391CE50  token=0x60002FB  System.Void ApplyPositionBlendMethod(Cinemachine.CameraState& state, Cinemachine.CinemachineVirtualCameraBase.BlendHint hint)
  RVA=0x012081B0  token=0x6000300  System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly)
  RVA=0x033C8190  token=0x6000307  System.Void UpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=-1  // abstract  token=0x6000308  System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x03E697B0  token=0x6000309  System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=0x067B0368  token=0x600030A  System.Void OnDestroy()
  RVA=0x0413B830  token=0x600030B  System.Void OnTransformParentChanged()
  RVA=0x04D84E30  token=0x600030C  System.Void Start()
  RVA=0x067B04E0  token=0x600030D  System.Boolean RequiresUserInput()
  RVA=0x067B013C  token=0x600030E  System.Void EnsureStarted()
  RVA=0x067B0238  token=0x600030F  Cinemachine.AxisState.IInputAxisProvider GetInputAxisProvider()
  RVA=0x067B0438  token=0x6000310  System.Void OnValidate()
  RVA=0x0413B650  token=0x6000311  System.Void OnEnable()
  RVA=0x0413B600  token=0x6000312  System.Void OnDisable()
  RVA=0x041190F0  token=0x6000313  System.Void Update()
  RVA=0x0413B970  token=0x6000314  System.Void UpdateSlaveStatus()
  RVA=0x031FD690  token=0x6000315  UnityEngine.Transform ResolveLookAt(UnityEngine.Transform localLookAt)
  RVA=0x031FE3B0  token=0x6000316  UnityEngine.Transform ResolveFollow(UnityEngine.Transform localFollow)
  RVA=0x0413BA30  token=0x6000317  System.Void UpdateVcamPoolStatus()
  RVA=0x067B0360  token=0x6000318  System.Void MoveToTopOfPrioritySubqueue()
  RVA=0x067B03C0  token=0x6000319  System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta)
  RVA=0x067B01C0  token=0x600031A  System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x067AFCBC  token=0x600031B  Cinemachine.CinemachineBlend CreateBlend(Cinemachine.ICinemachineCamera camA, Cinemachine.ICinemachineCamera camB, Cinemachine.CinemachineBlendDefinition blendDef, Cinemachine.CinemachineBlend activeBlend)
  RVA=0x0324BD00  token=0x600031C  Cinemachine.CameraState PullStateFromVirtualCamera(UnityEngine.Vector3 worldUp, Cinemachine.LensSettings& lens)
  RVA=0x0413B7B0  token=0x600031D  System.Void InvalidateCachedTargets()
  RVA=0x031FCBF0  token=0x6000322  System.Void UpdateTargetCache()
  RVA=-1  // generic def  token=0x6000327  System.Object GetCinemachineComponent()
  RVA=0x04220570  token=0x6000328  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.ConfinerOven
TYPE:  class
TOKEN: 0x2000088
SIZE:  0x98
FIELDS:
  private           System.Single                   m_MinFrustumHeightWithBones  // 0x10
  private           System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>m_OriginalPolygon  // 0x18
  private           System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>m_Skeleton  // 0x20
  private   static  System.Int64                    k_FloatToIntScaler  // const
  private   static  System.Single                   k_IntToFloatScaler  // const
  private   static  System.Single                   k_MinStepSize  // const
  private           UnityEngine.Rect                m_PolygonRect  // 0x28
  private           Cinemachine.ConfinerOven.AspectStretcherm_AspectStretcher  // 0x38
  private           System.Single                   m_maxComputationTimeForFullSkeletonBakeInSeconds  // 0x44
  private           Cinemachine.ConfinerOven.BakingState<State>k__BackingField  // 0x48
  public            System.Single                   m_BakeProgress  // 0x4c
  private           Cinemachine.ConfinerOven.BakingStateCachem_Cache  // 0x50
PROPERTIES:
  State  get=0x04D863D0  set=0x04D86450
METHODS:
  RVA=0x067B1698  token=0x600032C  System.Void .ctor(System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Vector2>>& inputPath, System.Single& aspectRatio, System.Single maxFrustumHeight)
  RVA=0x067B0D70  token=0x600032D  Cinemachine.ConfinerOven.BakedSolution GetBakedSolution(System.Single frustumHeight)
  RVA=0x067B116C  token=0x6000330  System.Void Initialize(System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Vector2>>& inputPath, System.Single& aspectRatio, System.Single maxFrustumHeight)
  RVA=0x067B0624  token=0x6000331  System.Void BakeConfiner(System.Single maxComputationTimePerFrameInSeconds)
  RVA=0x067B0FF4  token=0x6000332  UnityEngine.Rect GetPolygonBoundingBox(System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Vector2>>& polygons)
  RVA=0x067B09E8  token=0x6000333  System.Void ComputeSkeleton(System.Collections.Generic.List<Cinemachine.ConfinerOven.PolygonSolution>& solutions)
END_CLASS

CLASS: Cinemachine.ICinemachineCamera
TYPE:  interface
TOKEN: 0x200008E
FIELDS:
PROPERTIES:
  Name  get=-1  // abstract
  Priority  get=-1  // abstract
  LookAt  get=-1  // abstract
  Follow  get=-1  // abstract
  State  get=-1  // abstract
  VirtualCameraGameObject  get=-1  // abstract
  IsValid  get=-1  // abstract
  ParentCamera  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x600034C  System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly)
  RVA=-1  // abstract  token=0x600034D  System.Void UpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime)
  RVA=-1  // abstract  token=0x600034E  System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime)
END_CLASS

CLASS: Cinemachine.LensSettings
TYPE:  sealed struct
TOKEN: 0x200008F
SIZE:  0x40
FIELDS:
  public    static  Cinemachine.LensSettings        Default  // static @ 0x0
  public            System.Single                   FieldOfView  // 0x10
  public            System.Single                   OrthographicSize  // 0x14
  public            System.Single                   NearClipPlane  // 0x18
  public            System.Single                   FarClipPlane  // 0x1c
  public            System.Single                   Dutch  // 0x20
  public            Cinemachine.LensSettings.OverrideModesModeOverride  // 0x24
  public            UnityEngine.Vector2             LensShift  // 0x28
  public            UnityEngine.Camera.GateFitMode  GateFit  // 0x30
  private           UnityEngine.Vector2             m_SensorSize  // 0x34
  private           System.Boolean                  m_OrthoFromCamera  // 0x3c
  private           System.Boolean                  m_PhysicalFromCamera  // 0x3d
PROPERTIES:
  Orthographic  get=0x03249DB0
  SensorSize  get=0x04D88060
  Aspect  get=0x03249D50
  IsPhysicalCamera  get=0x0324C270
METHODS:
  RVA=0x0391D6D0  token=0x6000353  Cinemachine.LensSettings FromCamera(UnityEngine.Camera fromCamera)
  RVA=0x0324C290  token=0x6000354  System.Void SnapshotCameraReadOnlyProperties(UnityEngine.Camera camera)
  RVA=0x04CB6510  token=0x6000355  System.Void .ctor(System.Single verticalFOV, System.Single orthographicSize, System.Single nearClip, System.Single farClip, System.Single dutch)
  RVA=0x03245990  token=0x6000356  Cinemachine.LensSettings Lerp(Cinemachine.LensSettings lensA, Cinemachine.LensSettings lensB, System.Single t)
  RVA=0x067B192C  token=0x6000357  System.Void Validate()
  RVA=0x04CB6470  token=0x6000358  System.Void .cctor()
END_CLASS

CLASS: Cinemachine.NoiseSettings
TYPE:  sealed class
TOKEN: 0x2000091
SIZE:  0x28
EXTENDS: Cinemachine.SignalSourceAsset
FIELDS:
  public            Cinemachine.NoiseSettings.TransformNoiseParams[]PositionNoise  // 0x18
  public            Cinemachine.NoiseSettings.TransformNoiseParams[]OrientationNoise  // 0x20
PROPERTIES:
  SignalDuration  get=0x04D879D0
METHODS:
  RVA=0x03C01150  token=0x6000359  UnityEngine.Vector3 GetCombinedFilterResults(Cinemachine.NoiseSettings.TransformNoiseParams[] noiseParams, System.Single time, UnityEngine.Vector3 timeOffsets)
  RVA=0x03C01010  token=0x600035B  System.Void GetSignal(System.Single timeSinceSignalStart, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot)
  RVA=0x04955230  token=0x600035C  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.RuntimeUtility
TYPE:  static class
TOKEN: 0x2000094
SIZE:  0x10
FIELDS:
  public    static  Cinemachine.RuntimeUtility.OverlapSphereNonAllocDelegateOverlapSphereNonAlloc  // static @ 0x0
  public    static  Cinemachine.RuntimeUtility.RaycastDelegateRaycast  // static @ 0x8
  public    static  Cinemachine.RuntimeUtility.RaycastNonAllocDelegateRaycastNonAlloc  // static @ 0x10
  public    static  Cinemachine.RuntimeUtility.SphereCastNonAllocDelegateSphereCastNonAlloc  // static @ 0x18
  public    static  Cinemachine.RuntimeUtility.ComputePenetrationDelegateComputePenetration  // static @ 0x20
  public    static  System.Int32                    ignoreColliderOption  // static @ 0x28
  private   static  UnityEngine.RaycastHit[]        s_HitBuffer  // static @ 0x30
  private   static  System.Int32[]                  s_PenetrationIndexBuffer  // static @ 0x38
  private   static  UnityEngine.SphereCollider      s_ScratchCollider  // static @ 0x40
  private   static  UnityEngine.GameObject          s_ScratchColliderGameObject  // static @ 0x48
METHODS:
  RVA=0x067B1CA4  token=0x600035F  System.Void DestroyObject(UnityEngine.Object obj)
  RVA=0x012081B0  token=0x6000360  System.Boolean IsPrefab(UnityEngine.GameObject gameObject)
  RVA=0x032489A0  token=0x6000361  System.Boolean RaycastIgnoreTag(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single rayLength, System.Int32 layerMask, System.String& ignoreTag)
  RVA=0x044C5210  token=0x6000362  System.Int32 GetColliderOptionsFromHit(UnityEngine.RaycastHit hit)
  RVA=0x044C5150  token=0x6000363  System.Boolean HasIgnoreColliderOptionFromHit(UnityEngine.RaycastHit hit)
  RVA=0x067B2100  token=0x6000364  System.Boolean HasIgnoreColliderOptionFromCollider(UnityEngine.ECSColliderResultProxy collider)
  RVA=0x0324A310  token=0x6000365  System.Boolean SphereCastIgnoreTag(UnityEngine.Vector3 rayStart, System.Single radius, UnityEngine.Vector3 dir, UnityEngine.RaycastHit& hitInfo, System.Single rayLength, System.Int32 layerMask, System.String& ignoreTag)
  RVA=0x067B1E78  token=0x6000366  UnityEngine.SphereCollider GetScratchCollider()
  RVA=0x067B1D04  token=0x6000367  System.Void DestroyScratchCollider()
  RVA=0x067B2170  token=0x6000368  UnityEngine.AnimationCurve NormalizeCurve(UnityEngine.AnimationCurve curve, System.Boolean normalizeX, System.Boolean normalizeY)
  RVA=0x0454DE60  token=0x6000369  System.Void .cctor()
END_CLASS

CLASS: Cinemachine.ISignalSource6D
TYPE:  interface
TOKEN: 0x200009A
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000374  System.Void GetSignal(System.Single timeSinceSignalStart, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot)
END_CLASS

CLASS: Cinemachine.SignalSourceAsset
TYPE:  abstract class
TOKEN: 0x200009B
SIZE:  0x18
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: Cinemachine.ISignalSource6D
FIELDS:
PROPERTIES:
  SignalDuration  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000376  System.Void GetSignal(System.Single timeSinceSignalStart, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot)
  RVA=0x03573370  token=0x6000377  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.TargetPositionCache
TYPE:  class
TOKEN: 0x200009C
SIZE:  0x10
FIELDS:
  public    static  System.Boolean                  UseCache  // static @ 0x0
  private   static  Cinemachine.TargetPositionCache.Modem_CacheMode  // static @ 0x4
  public    static  System.Single                   CurrentTime  // static @ 0x8
  public    static  System.Int32                    CurrentFrame  // static @ 0xc
  public    static  System.Boolean                  IsCameraCut  // static @ 0x10
  private   static  System.Collections.Generic.Dictionary<UnityEngine.Transform,Cinemachine.TargetPositionCache.CacheEntry>m_Cache  // static @ 0x18
  private   static  Cinemachine.TargetPositionCache.TimeRangem_CacheTimeRange  // static @ 0x20
PROPERTIES:
  CacheMode  get=0x067B2580
  HasCurrentTime  get=0x067B25B8
METHODS:
  RVA=0x067B2488  token=0x600037A  System.Void ClearCache()
  RVA=0x0324C1D0  token=0x600037B  UnityEngine.Vector3 GetTargetPosition(UnityEngine.Transform target)
  RVA=0x0324C110  token=0x600037C  UnityEngine.Quaternion GetTargetRotation(UnityEngine.Transform target)
END_CLASS

CLASS: Cinemachine.UpdateTracker
TYPE:  class
TOKEN: 0x20000A3
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<UnityEngine.Transform,Cinemachine.UpdateTracker.UpdateStatus>mUpdateStatus  // static @ 0x0
  private   static  System.Collections.Generic.List<UnityEngine.Transform>sToDelete  // static @ 0x8
  private   static  System.Single                   mLastUpdateTime  // static @ 0x10
METHODS:
  RVA=0x04D34810  token=0x6000386  System.Void InitializeModule()
  RVA=0x067B2A1C  token=0x6000387  System.Void UpdateTargets(Cinemachine.UpdateTracker.UpdateClock currentClock)
  RVA=0x067B2790  token=0x6000388  Cinemachine.UpdateTracker.UpdateClock GetPreferredUpdate(UnityEngine.Transform target)
  RVA=0x067B2980  token=0x6000389  System.Void OnUpdate(Cinemachine.UpdateTracker.UpdateClock currentClock)
  RVA=0x04CA3930  token=0x600038A  System.Void .cctor()
END_CLASS

CLASS: Cinemachine.CinemachineInputProvider
TYPE:  class
TOKEN: 0x20000A6
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: IInputAxisProvider
FIELDS:
  public            System.Int32                    PlayerIndex  // 0x18
  public            System.Boolean                  AutoEnableInputs  // 0x1c
  public            UnityEngine.InputSystem.InputActionReferenceXYAxis  // 0x20
  public            UnityEngine.InputSystem.InputActionReferenceZAxis  // 0x28
  private   static  System.Int32                    NUM_AXES  // const
  private           UnityEngine.InputSystem.InputAction[]m_cachedActions  // 0x30
METHODS:
  RVA=0x067B5934  token=0x600038F  System.Single GetAxisValue(System.Int32 axis)
  RVA=0x067B5A14  token=0x6000390  UnityEngine.InputSystem.InputAction ResolveForPlayer(System.Int32 axis, UnityEngine.InputSystem.InputActionReference actionRef)
  RVA=0x067B5A00  token=0x6000391  System.Void OnDisable()
  RVA=0x067B5E74  token=0x6000392  System.Void .ctor()
  RVA=0x067B5D7C  token=0x6000393  UnityEngine.InputSystem.InputAction <ResolveForPlayer>g__GetFirstMatch|7_0(UnityEngine.InputSystem.Users.InputUser& user, UnityEngine.InputSystem.InputActionReference aRef)
END_CLASS

CLASS: Cinemachine.CinemachineTriggerAction
TYPE:  class
TOKEN: 0x20000A8
SIZE:  0x90
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.LayerMask           m_LayerMask  // 0x18
  public            System.String                   m_WithTag  // 0x20
  public            System.String                   m_WithoutTag  // 0x28
  public            System.Int32                    m_SkipFirst  // 0x30
  public            System.Boolean                  m_Repeating  // 0x34
  public            Cinemachine.CinemachineTriggerAction.ActionSettingsm_OnObjectEnter  // 0x38
  public            Cinemachine.CinemachineTriggerAction.ActionSettingsm_OnObjectExit  // 0x60
  private           System.Collections.Generic.HashSet<UnityEngine.GameObject>m_ActiveTriggerObjects  // 0x88
METHODS:
  RVA=0x067B5E94  token=0x6000396  System.Boolean Filter(UnityEngine.GameObject other)
  RVA=0x067B5F24  token=0x6000397  System.Void InternalDoTriggerEnter(UnityEngine.GameObject other)
  RVA=0x067B5FB4  token=0x6000398  System.Void InternalDoTriggerExit(UnityEngine.GameObject other)
  RVA=0x067B6120  token=0x6000399  System.Void OnTriggerEnter(UnityEngine.Collider other)
  RVA=0x067B6154  token=0x600039A  System.Void OnTriggerExit(UnityEngine.Collider other)
  RVA=0x067B6084  token=0x600039B  System.Void OnCollisionEnter(UnityEngine.Collision other)
  RVA=0x067B60EC  token=0x600039C  System.Void OnCollisionExit(UnityEngine.Collision other)
  RVA=0x067B6120  token=0x600039D  System.Void OnTriggerEnter2D(UnityEngine.Collider2D other)
  RVA=0x067B6154  token=0x600039E  System.Void OnTriggerExit2D(UnityEngine.Collider2D other)
  RVA=0x067B6050  token=0x600039F  System.Void OnCollisionEnter2D(UnityEngine.Collision2D other)
  RVA=0x067B60B8  token=0x60003A0  System.Void OnCollisionExit2D(UnityEngine.Collision2D other)
  RVA=0x041E1670  token=0x60003A1  System.Void OnEnable()
  RVA=0x067B6188  token=0x60003A2  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.GroupWeightManipulator
TYPE:  class
TOKEN: 0x20000AD
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Single                   m_Weight0  // 0x18
  public            System.Single                   m_Weight1  // 0x1c
  public            System.Single                   m_Weight2  // 0x20
  public            System.Single                   m_Weight3  // 0x24
  public            System.Single                   m_Weight4  // 0x28
  public            System.Single                   m_Weight5  // 0x2c
  public            System.Single                   m_Weight6  // 0x30
  public            System.Single                   m_Weight7  // 0x34
  private           Cinemachine.CinemachineTargetGroupm_group  // 0x38
METHODS:
  RVA=0x067B643C  token=0x60003A6  System.Void Start()
  RVA=0x067B63D0  token=0x60003A7  System.Void OnValidate()
  RVA=0x067B65B8  token=0x60003A8  System.Void Update()
  RVA=0x067B6484  token=0x60003A9  System.Void UpdateWeights()
  RVA=0x067B6618  token=0x60003AA  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineCollisionImpulseSource
TYPE:  class
TOKEN: 0x20000AE
SIZE:  0x58
EXTENDS: Cinemachine.CinemachineImpulseSource
FIELDS:
  public            UnityEngine.LayerMask           m_LayerMask  // 0x30
  public            System.String                   m_IgnoreTag  // 0x38
  public            System.Boolean                  m_UseImpactDirection  // 0x40
  public            System.Boolean                  m_ScaleImpactWithMass  // 0x41
  public            System.Boolean                  m_ScaleImpactWithSpeed  // 0x42
  private           UnityEngine.Rigidbody           mRigidBody  // 0x48
  private           UnityEngine.Rigidbody2D         mRigidBody2D  // 0x50
METHODS:
  RVA=0x067B3B08  token=0x60003AB  System.Void Start()
  RVA=0x041E1670  token=0x60003AC  System.Void OnEnable()
  RVA=0x067B3A78  token=0x60003AD  System.Void OnCollisionEnter(UnityEngine.Collision c)
  RVA=0x067B3ADC  token=0x60003AE  System.Void OnTriggerEnter(UnityEngine.Collider c)
  RVA=0x067B37D0  token=0x60003AF  System.Single GetMassAndVelocity(UnityEngine.Collider other, UnityEngine.Vector3& vel)
  RVA=0x067B33B0  token=0x60003B0  System.Void GenerateImpactEvent(UnityEngine.Collider other, UnityEngine.Vector3 vel)
  RVA=0x067B3A04  token=0x60003B1  System.Void OnCollisionEnter2D(UnityEngine.Collision2D c)
  RVA=0x067B3AB0  token=0x60003B2  System.Void OnTriggerEnter2D(UnityEngine.Collider2D c)
  RVA=0x067B359C  token=0x60003B3  System.Single GetMassAndVelocity2D(UnityEngine.Collider2D other2d, UnityEngine.Vector3& vel)
  RVA=0x067B31C4  token=0x60003B4  System.Void GenerateImpactEvent2D(UnityEngine.Collider2D other2d, UnityEngine.Vector3 vel)
  RVA=0x067B3B78  token=0x60003B5  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineFixedSignal
TYPE:  class
TOKEN: 0x20000AF
SIZE:  0x30
EXTENDS: Cinemachine.SignalSourceAsset
FIELDS:
  public            UnityEngine.AnimationCurve      m_XCurve  // 0x18
  public            UnityEngine.AnimationCurve      m_YCurve  // 0x20
  public            UnityEngine.AnimationCurve      m_ZCurve  // 0x28
PROPERTIES:
  SignalDuration  get=0x03DF0B90
METHODS:
  RVA=0x03DF0BF0  token=0x60003B7  System.Single AxisDuration(UnityEngine.AnimationCurve axis)
  RVA=0x043F9E10  token=0x60003B8  System.Void GetSignal(System.Single timeSinceSignalStart, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot)
  RVA=0x043F9EB0  token=0x60003B9  System.Single AxisValue(UnityEngine.AnimationCurve axis, System.Single time)
  RVA=0x03573370  token=0x60003BA  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineImpulseDefinition
TYPE:  class
TOKEN: 0x20000B0
SIZE:  0x80
FIELDS:
  public            System.Int32                    m_ImpulseChannel  // 0x10
  public            Cinemachine.CinemachineImpulseDefinition.ImpulseShapesm_ImpulseShape  // 0x14
  public            UnityEngine.AnimationCurve      m_CustomImpulseShape  // 0x18
  public            System.Single                   m_ImpulseDuration  // 0x20
  public            Cinemachine.CinemachineImpulseDefinition.ImpulseTypesm_ImpulseType  // 0x24
  public            System.Single                   m_DissipationRate  // 0x28
  public            Cinemachine.SignalSourceAsset   m_RawSignal  // 0x30
  public            System.Single                   m_AmplitudeGain  // 0x38
  public            System.Single                   m_FrequencyGain  // 0x3c
  public            Cinemachine.CinemachineImpulseDefinition.RepeatModem_RepeatMode  // 0x40
  public            System.Boolean                  m_Randomize  // 0x44
  public            Cinemachine.CinemachineImpulseManager.EnvelopeDefinitionm_TimeEnvelope  // 0x48
  public            System.Single                   m_ImpactRadius  // 0x68
  public            Cinemachine.CinemachineImpulseManager.ImpulseEvent.DirectionModem_DirectionMode  // 0x6c
  public            Cinemachine.CinemachineImpulseManager.ImpulseEvent.DissipationModem_DissipationMode  // 0x70
  public            System.Single                   m_DissipationDistance  // 0x74
  public            System.Single                   m_PropagationSpeed  // 0x78
  private   static  UnityEngine.AnimationCurve[]    sStandardShapes  // static @ 0x0
PROPERTIES:
  ImpulseCurve  get=0x067B4EAC
METHODS:
  RVA=0x067B4DD4  token=0x60003BB  System.Void OnValidate()
  RVA=0x067B41FC  token=0x60003BC  System.Void CreateStandardShapes()
  RVA=0x067B4D60  token=0x60003BD  UnityEngine.AnimationCurve GetStandardCurve(Cinemachine.CinemachineImpulseDefinition.ImpulseShapes shape)
  RVA=0x067B41BC  token=0x60003BF  System.Void CreateEvent(UnityEngine.Vector3 position, UnityEngine.Vector3 velocity)
  RVA=0x033CDDB0  token=0x60003C0  Cinemachine.CinemachineImpulseManager.ImpulseEvent CreateAndReturnEvent(UnityEngine.Vector3 position, UnityEngine.Vector3 velocity)
  RVA=0x033CD670  token=0x60003C1  Cinemachine.CinemachineImpulseManager.ImpulseEvent LegacyCreateAndReturnEvent(UnityEngine.Vector3 position, UnityEngine.Vector3 velocity)
  RVA=0x0395F2B0  token=0x60003C2  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineImpulseListener
TYPE:  class
TOKEN: 0x20000B6
SIZE:  0x68
EXTENDS: Cinemachine.CinemachineExtension
FIELDS:
  public            Cinemachine.CinemachineCore.Stagem_ApplyAfter  // 0x28
  public            System.Int32                    m_ChannelMask  // 0x2c
  public            System.Single                   m_Gain  // 0x30
  public            System.Boolean                  m_Use2DDistance  // 0x34
  public            System.Boolean                  m_UseCameraSpace  // 0x35
  public            Cinemachine.CinemachineImpulseListener.ImpulseReactionm_ReactionSettings  // 0x38
METHODS:
  RVA=0x067B4F04  token=0x60003C9  System.Void Reset()
  RVA=0x031FE6C0  token=0x60003CA  System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x049C8F70  token=0x60003CB  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineImpulseChannelPropertyAttribute
TYPE:  sealed class
TOKEN: 0x20000B8
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60003CE  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineImpulseManager
TYPE:  class
TOKEN: 0x20000B9
SIZE:  0x28
FIELDS:
  private   static  Cinemachine.CinemachineImpulseManagersInstance  // static @ 0x0
  private           System.Collections.Generic.List<Cinemachine.CinemachineImpulseManager.ImpulseEvent>m_ExpiredEvents  // 0x10
  private           System.Collections.Generic.List<Cinemachine.CinemachineImpulseManager.ImpulseEvent>m_ActiveEvents  // 0x18
  public            System.Boolean                  IgnoreTimeScale  // 0x20
PROPERTIES:
  Instance  get=0x033CD1B0
  CurrentTime  get=0x033CD240
METHODS:
  RVA=0x041E1670  token=0x60003CF  System.Void .ctor()
  RVA=0x04D75DC0  token=0x60003D1  System.Void InitializeModule()
  RVA=0x067B5008  token=0x60003D2  System.Single EvaluateDissipationScale(System.Single spread, System.Single normalizedDistance)
  RVA=0x033CCE90  token=0x60003D3  System.Boolean GetImpulseAt(UnityEngine.Vector3 listenerLocation, System.Boolean distance2D, System.Int32 channelMask, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot)
  RVA=0x033CDA50  token=0x60003D5  Cinemachine.CinemachineImpulseManager.ImpulseEvent NewImpulseEvent()
  RVA=0x033CDE20  token=0x60003D6  System.Void AddImpulseEvent(Cinemachine.CinemachineImpulseManager.ImpulseEvent e)
  RVA=0x067B4F68  token=0x60003D7  System.Void Clear()
END_CLASS

CLASS: Cinemachine.CinemachineImpulseSource
TYPE:  class
TOKEN: 0x20000BE
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            Cinemachine.CinemachineImpulseDefinitionm_ImpulseDefinition  // 0x18
  public            UnityEngine.Vector3             m_DefaultVelocity  // 0x20
METHODS:
  RVA=0x067B52A0  token=0x60003E4  System.Void OnValidate()
  RVA=0x067B52C0  token=0x60003E5  System.Void Reset()
  RVA=0x067B50A8  token=0x60003E6  System.Void GenerateImpulseAtPositionWithVelocity(UnityEngine.Vector3 position, UnityEngine.Vector3 velocity)
  RVA=0x067B51D4  token=0x60003E7  System.Void GenerateImpulseWithVelocity(UnityEngine.Vector3 velocity)
  RVA=0x067B5134  token=0x60003E8  System.Void GenerateImpulseWithForce(System.Single force)
  RVA=0x067B5248  token=0x60003E9  System.Void GenerateImpulse()
  RVA=0x067B50F4  token=0x60003EA  System.Void GenerateImpulseAt(UnityEngine.Vector3 position, UnityEngine.Vector3 velocity)
  RVA=0x067B5278  token=0x60003EB  System.Void GenerateImpulse(UnityEngine.Vector3 velocity)
  RVA=0x067B5270  token=0x60003EC  System.Void GenerateImpulse(System.Single force)
  RVA=0x067B53B4  token=0x60003ED  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.CinemachineIndependentImpulseListener
TYPE:  class
TOKEN: 0x20000BF
SIZE:  0x70
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Vector3             impulsePosLastFrame  // 0x18
  private           UnityEngine.Quaternion          impulseRotLastFrame  // 0x24
  public            System.Int32                    m_ChannelMask  // 0x34
  public            System.Single                   m_Gain  // 0x38
  public            System.Boolean                  m_Use2DDistance  // 0x3c
  public            System.Boolean                  m_UseLocalSpace  // 0x3d
  public            Cinemachine.CinemachineImpulseListener.ImpulseReactionm_ReactionSettings  // 0x40
METHODS:
  RVA=0x067B57A4  token=0x60003EE  System.Void Reset()
  RVA=0x067B5774  token=0x60003EF  System.Void OnEnable()
  RVA=0x067B5800  token=0x60003F0  System.Void Update()
  RVA=0x067B543C  token=0x60003F1  System.Void LateUpdate()
  RVA=0x05393520  token=0x60003F2  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.PostFX.CinemachinePostProcessing
TYPE:  class
TOKEN: 0x20000C0
SIZE:  0x28
EXTENDS: Cinemachine.CinemachineExtension
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60003F3  System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime)
  RVA=0x05393520  token=0x60003F4  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.PostFX.CinemachineVolumeSettings
TYPE:  class
TOKEN: 0x20000C1
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x05393520  token=0x60003F5  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.Utility.CinemachineDebug
TYPE:  class
TOKEN: 0x20000C2
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.HashSet<UnityEngine.Object>mClients  // static @ 0x0
  public    static  Cinemachine.Utility.CinemachineDebug.OnGUIDelegateOnGUIHandlers  // static @ 0x8
  private   static  System.Collections.Generic.List<System.Text.StringBuilder>mAvailableStringBuilders  // static @ 0x10
METHODS:
  RVA=0x067B3F4C  token=0x60003F6  System.Void ReleaseScreenPos(UnityEngine.Object client)
  RVA=0x067B3BD4  token=0x60003F7  UnityEngine.Rect GetScreenPos(UnityEngine.Object client, System.String text, UnityEngine.GUIStyle style)
  RVA=0x067B40C8  token=0x60003F8  System.Text.StringBuilder SBFromPool()
  RVA=0x067B3FE4  token=0x60003F9  System.Void ReturnToPool(System.Text.StringBuilder sb)
END_CLASS

CLASS: Cinemachine.Utility.PositionPredictor
TYPE:  class
TOKEN: 0x20000C4
SIZE:  0x40
FIELDS:
  private           UnityEngine.Vector3             m_Velocity  // 0x10
  private           UnityEngine.Vector3             m_SmoothDampVelocity  // 0x1c
  private           UnityEngine.Vector3             m_Pos  // 0x28
  private           System.Boolean                  m_HavePos  // 0x34
  public            System.Single                   Smoothing  // 0x38
METHODS:
  RVA=0x067B6F14  token=0x60003FC  System.Void ApplyTransformDelta(UnityEngine.Vector3 positionDelta)
  RVA=0x067B6FA8  token=0x60003FD  System.Void Reset()
  RVA=0x067B6D64  token=0x60003FE  System.Void AddPosition(UnityEngine.Vector3 pos, System.Single deltaTime, System.Single lookaheadTime)
  RVA=0x067B6F68  token=0x60003FF  UnityEngine.Vector3 PredictPositionDelta(System.Single lookaheadTime)
  RVA=0x041E1670  token=0x6000400  System.Void .ctor()
END_CLASS

CLASS: Cinemachine.Utility.Damper
TYPE:  static class
TOKEN: 0x20000C5
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x031FBEF0  token=0x6000401  System.Single Damp(System.Single initial, System.Single dampTime, System.Single deltaTime)
  RVA=0x031FB600  token=0x6000402  UnityEngine.Vector3 Damp(UnityEngine.Vector3 initial, UnityEngine.Vector3 dampTime, System.Single deltaTime)
  RVA=0x067B62F8  token=0x6000403  UnityEngine.Vector3 Damp(UnityEngine.Vector3 initial, System.Single dampTime, System.Single deltaTime)
END_CLASS

CLASS: Cinemachine.Utility.HeadingTracker
TYPE:  class
TOKEN: 0x20000C6
SIZE:  0x48
FIELDS:
  private           Cinemachine.Utility.HeadingTracker.Item[]mHistory  // 0x10
  private           System.Int32                    mTop  // 0x18
  private           System.Int32                    mBottom  // 0x1c
  private           System.Int32                    mCount  // 0x20
  private           UnityEngine.Vector3             mHeadingSum  // 0x24
  private           System.Single                   mWeightSum  // 0x30
  private           System.Single                   mWeightTime  // 0x34
  private           UnityEngine.Vector3             mLastGoodHeading  // 0x38
  private   static  System.Single                   mDecayExponent  // static @ 0x0
PROPERTIES:
  FilterSize  get=0x067B6CAC
METHODS:
  RVA=0x067B6BD0  token=0x6000404  System.Void .ctor(System.Int32 filterSize)
  RVA=0x067B6824  token=0x6000406  System.Void ClearHistory()
  RVA=0x067B6904  token=0x6000407  System.Single Decay(System.Single time)
  RVA=0x067B6668  token=0x6000408  System.Void Add(UnityEngine.Vector3 velocity)
  RVA=0x067B6A44  token=0x6000409  System.Void PopBottom()
  RVA=0x067B6844  token=0x600040A  System.Void DecayHistory()
  RVA=0x067B6950  token=0x600040B  UnityEngine.Vector3 GetReliableHeading()
END_CLASS

CLASS: Cinemachine.Utility.SplineHelpers
TYPE:  static class
TOKEN: 0x20000C8
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x067B7184  token=0x600040C  UnityEngine.Vector3 Bezier3(System.Single t, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Vector3 p3)
  RVA=0x067B7310  token=0x600040D  UnityEngine.Vector3 BezierTangent3(System.Single t, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Vector3 p3)
  RVA=0x067B70F8  token=0x600040E  System.Single Bezier1(System.Single t, System.Single p0, System.Single p1, System.Single p2, System.Single p3)
  RVA=0x067B7924  token=0x600040F  System.Void ComputeSmoothControlPoints(UnityEngine.Vector4[]& knot, UnityEngine.Vector4[]& ctrl1, UnityEngine.Vector4[]& ctrl2)
  RVA=0x067B7668  token=0x6000410  System.Void ComputeSmoothControlPointsLooped(UnityEngine.Vector4[]& knot, UnityEngine.Vector4[]& ctrl1, UnityEngine.Vector4[]& ctrl2)
END_CLASS

CLASS: Cinemachine.Utility.UnityVectorExtensions
TYPE:  static class
TOKEN: 0x20000C9
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0364D1E0  token=0x6000411  System.Boolean IsNaN(UnityEngine.Vector3 v)
  RVA=0x067B848C  token=0x6000412  System.Single ClosestPointOnSegment(UnityEngine.Vector3 p, UnityEngine.Vector3 s0, UnityEngine.Vector3 s1)
  RVA=0x067B83F4  token=0x6000413  System.Single ClosestPointOnSegment(UnityEngine.Vector2 p, UnityEngine.Vector2 s0, UnityEngine.Vector2 s1)
  RVA=0x03201FB0  token=0x6000414  UnityEngine.Vector3 ProjectOntoPlane(UnityEngine.Vector3 vector, UnityEngine.Vector3 planeNormal)
  RVA=0x04D880E0  token=0x6000415  UnityEngine.Vector2 Abs(UnityEngine.Vector2 v)
  RVA=0x067B856C  token=0x6000416  System.Boolean IsUniform(UnityEngine.Vector2 v)
  RVA=0x03201F90  token=0x6000417  System.Boolean AlmostZero(UnityEngine.Vector3 v)
  RVA=0x02F9FBB0  token=0x6000418  System.Single Angle(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2)
  RVA=0x03202070  token=0x6000419  System.Single SignedAngle(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, UnityEngine.Vector3 up)
  RVA=0x067B85C4  token=0x600041A  UnityEngine.Quaternion SafeFromToRotation(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, UnityEngine.Vector3 up)
END_CLASS

CLASS: Cinemachine.Utility.UnityQuaternionExtensions
TYPE:  static class
TOKEN: 0x20000CA
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x067B80E4  token=0x600041B  UnityEngine.Quaternion SlerpWithReferenceUp(UnityEngine.Quaternion qA, UnityEngine.Quaternion qB, System.Single t, UnityEngine.Vector3 up)
  RVA=0x031CC020  token=0x600041C  UnityEngine.Quaternion Normalized(UnityEngine.Quaternion q)
  RVA=0x03201980  token=0x600041D  UnityEngine.Vector2 GetCameraRotationToTarget(UnityEngine.Quaternion orient, UnityEngine.Vector3 lookAtDir, UnityEngine.Vector3 worldUp)
  RVA=0x03200080  token=0x600041E  UnityEngine.Quaternion ApplyCameraRotation(UnityEngine.Quaternion orient, UnityEngine.Vector2 rot, UnityEngine.Vector3 worldUp)
END_CLASS

