// ========================================================
// Dumped by @desirepro
// Assembly: RootMotion.dll
// Classes:  226
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

CLASS: Mode
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.Baker.Mode           AnimationClips  // const
  public    static  RootMotion.Baker.Mode           AnimationStates  // const
  public    static  RootMotion.Baker.Mode           PlayableDirector  // const
  public    static  RootMotion.Baker.Mode           Realtime  // const
METHODS:
END_CLASS

CLASS: BakerDelegate
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x08EF544C  token=0x6000014  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x6000015  System.Void Invoke(UnityEngine.AnimationClip clip, System.Single time)
  RVA=0x0ABD4E34  token=0x6000016  System.IAsyncResult BeginInvoke(UnityEngine.AnimationClip clip, System.Single time, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000017  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: BasedUponRotation
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.Baker.ClipSettings.BasedUponRotationOriginal  // const
  public    static  RootMotion.Baker.ClipSettings.BasedUponRotationBodyOrientation  // const
METHODS:
END_CLASS

CLASS: BasedUponY
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.Baker.ClipSettings.BasedUponYOriginal  // const
  public    static  RootMotion.Baker.ClipSettings.BasedUponYCenterOfMass  // const
  public    static  RootMotion.Baker.ClipSettings.BasedUponYFeet  // const
METHODS:
END_CLASS

CLASS: BasedUponXZ
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.Baker.ClipSettings.BasedUponXZOriginal  // const
  public    static  RootMotion.Baker.ClipSettings.BasedUponXZCenterOfMass  // const
METHODS:
END_CLASS

CLASS: ClipSettings
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x40
FIELDS:
  public            System.Boolean                  loopTime  // 0x10
  public            System.Boolean                  loopBlend  // 0x11
  public            System.Single                   cycleOffset  // 0x14
  public            System.Boolean                  loopBlendOrientation  // 0x18
  public            RootMotion.Baker.ClipSettings.BasedUponRotationbasedUponRotation  // 0x1c
  public            System.Single                   orientationOffsetY  // 0x20
  public            System.Boolean                  loopBlendPositionY  // 0x24
  public            RootMotion.Baker.ClipSettings.BasedUponYbasedUponY  // 0x28
  public            System.Single                   level  // 0x2c
  public            System.Boolean                  loopBlendPositionXZ  // 0x30
  public            RootMotion.Baker.ClipSettings.BasedUponXZbasedUponXZ  // 0x34
  public            System.Boolean                  mirror  // 0x38
METHODS:
  RVA=0x041E1670  token=0x6000018  System.Void .ctor()
END_CLASS

CLASS: LimbOrientation
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3             upperBoneForwardAxis  // 0x10
  public            UnityEngine.Vector3             lowerBoneForwardAxis  // 0x1c
  public            UnityEngine.Vector3             lastBoneLeftAxis  // 0x28
METHODS:
  RVA=0x04D8EB70  token=0x6000060  System.Void .ctor(UnityEngine.Vector3 upperBoneForwardAxis, UnityEngine.Vector3 lowerBoneForwardAxis, UnityEngine.Vector3 lastBoneLeftAxis)
END_CLASS

CLASS: BoneType
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.BipedNaming.BoneType Unassigned  // const
  public    static  RootMotion.BipedNaming.BoneType Spine  // const
  public    static  RootMotion.BipedNaming.BoneType Head  // const
  public    static  RootMotion.BipedNaming.BoneType Arm  // const
  public    static  RootMotion.BipedNaming.BoneType Leg  // const
  public    static  RootMotion.BipedNaming.BoneType Tail  // const
  public    static  RootMotion.BipedNaming.BoneType Eye  // const
METHODS:
END_CLASS

CLASS: BoneSide
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.BipedNaming.BoneSide Center  // const
  public    static  RootMotion.BipedNaming.BoneSide Left  // const
  public    static  RootMotion.BipedNaming.BoneSide Right  // const
METHODS:
END_CLASS

CLASS: AutoDetectParams
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x12
FIELDS:
  public            System.Boolean                  legsParentInSpine  // 0x10
  public            System.Boolean                  includeEyes  // 0x11
PROPERTIES:
  Default  get=0x04DA8EE0
METHODS:
  RVA=0x02BBF3D0  token=0x6000091  System.Void .ctor(System.Boolean legsParentInSpine, System.Boolean includeEyes)
END_CLASS

CLASS: Logger
TYPE:  sealed class
TOKEN: 0x200002E
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679B0C8  token=0x600012E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600012F  System.Void Invoke(System.String message)
  RVA=0x047FDD70  token=0x6000130  System.IAsyncResult BeginInvoke(System.String message, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000131  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: State
TYPE:  sealed struct
TOKEN: 0x2000030
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.Demos.Navigator.StateIdle  // const
  public    static  RootMotion.Demos.Navigator.StateSeeking  // const
  public    static  RootMotion.Demos.Navigator.StateOnPath  // const
METHODS:
END_CLASS

CLASS: DOF
TYPE:  sealed struct
TOKEN: 0x200003A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.Finger.DOF   One  // const
  public    static  RootMotion.FinalIK.Finger.DOF   Three  // const
METHODS:
END_CLASS

CLASS: GrounderDelegate
TYPE:  sealed class
TOKEN: 0x200003D
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x053DEDC8  token=0x6000194  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x6000195  System.Void Invoke()
  RVA=0x053DED94  token=0x6000196  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000197  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: SpineEffector
TYPE:  class
TOKEN: 0x2000040
SIZE:  0x20
FIELDS:
  public            RootMotion.FinalIK.FullBodyBipedEffectoreffectorType  // 0x10
  public            System.Single                   horizontalWeight  // 0x14
  public            System.Single                   verticalWeight  // 0x18
METHODS:
  RVA=0x015845C0  token=0x60001B9  System.Void .ctor()
  RVA=0x04DA9650  token=0x60001BA  System.Void .ctor(RootMotion.FinalIK.FullBodyBipedEffector effectorType, System.Single horizontalWeight, System.Single verticalWeight)
END_CLASS

CLASS: Foot
TYPE:  sealed struct
TOKEN: 0x2000043
SIZE:  0x38
FIELDS:
  public            RootMotion.FinalIK.IKSolver     solver  // 0x10
  public            UnityEngine.Transform           transform  // 0x18
  public            UnityEngine.Quaternion          rotation  // 0x20
  public            RootMotion.FinalIK.Grounding.Legleg  // 0x30
METHODS:
  RVA=0x0ABE52D0  token=0x60001D9  System.Void .ctor(RootMotion.FinalIK.IKSolver solver, UnityEngine.Transform transform)
END_CLASS

CLASS: Quality
TYPE:  sealed struct
TOKEN: 0x2000045
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.Grounding.QualityFastest  // const
  public    static  RootMotion.FinalIK.Grounding.QualitySimple  // const
  public    static  RootMotion.FinalIK.Grounding.QualityBest  // const
  public    static  RootMotion.FinalIK.Grounding.QualityCustom  // const
METHODS:
END_CLASS

CLASS: OnRaycastDelegate
TYPE:  sealed class
TOKEN: 0x2000046
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x044081D0  token=0x6000201  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x06226A0C  token=0x6000202  System.Boolean Invoke(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0ABEC01C  token=0x6000203  System.IAsyncResult BeginInvoke(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction, System.AsyncCallback callback, System.Object object)
  RVA=0x05FD1038  token=0x6000204  System.Boolean EndInvoke(UnityEngine.RaycastHit& hitInfo, System.IAsyncResult result)
END_CLASS

CLASS: OnCapsuleCastDelegate
TYPE:  sealed class
TOKEN: 0x2000047
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x04408260  token=0x6000205  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x06225F10  token=0x6000206  System.Boolean Invoke(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0ABEBEDC  token=0x6000207  System.IAsyncResult BeginInvoke(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction, System.AsyncCallback callback, System.Object object)
  RVA=0x05FD1038  token=0x6000208  System.Boolean EndInvoke(UnityEngine.RaycastHit& hitInfo, System.IAsyncResult result)
END_CLASS

CLASS: OnSphereCastDelegate
TYPE:  sealed class
TOKEN: 0x2000048
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x04408140  token=0x6000209  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x06225970  token=0x600020A  System.Boolean Invoke(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0ABEC148  token=0x600020B  System.IAsyncResult BeginInvoke(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction, System.AsyncCallback callback, System.Object object)
  RVA=0x05FD1038  token=0x600020C  System.Boolean EndInvoke(UnityEngine.RaycastHit& hitInfo, System.IAsyncResult result)
END_CLASS

CLASS: Leg
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x328
FIELDS:
  private           System.Boolean                  <isGrounded>k__BackingField  // 0x10
  private           UnityEngine.Vector3             <IKPosition>k__BackingField  // 0x14
  public            UnityEngine.Quaternion          rotationOffset  // 0x20
  private           System.Boolean                  <initiated>k__BackingField  // 0x30
  private           System.Single                   <heightFromGround>k__BackingField  // 0x34
  private           System.Single                   <fowardMoveFromGround>k__BackingField  // 0x38
  private           System.Single                   <rightMoveFromGround>k__BackingField  // 0x3c
  private           UnityEngine.Vector3             <velocity>k__BackingField  // 0x40
  private           UnityEngine.Transform           <transform>k__BackingField  // 0x50
  private           System.Single                   <IKOffset>k__BackingField  // 0x58
  private           System.Single                   <IKOffsetZ>k__BackingField  // 0x5c
  private           System.Single                   <IKOffsetX>k__BackingField  // 0x60
  private           UnityEngine.RaycastHit          <heelHit>k__BackingField  // 0x68
  private           UnityEngine.RaycastHit          <capsuleHit>k__BackingField  // 0xa8
  public            System.Boolean                  invertFootCenter  // 0xe8
  private           UnityEngine.Vector3             m_lastEntityPos  // 0xec
  private           UnityEngine.Vector3             m_lastFootPos  // 0xf8
  private           UnityEngine.Vector3             m_lastHeelPoint  // 0x104
  private           UnityEngine.Vector3             m_lastToePoint  // 0x110
  private           UnityEngine.Vector3             m_lastSidePoint  // 0x11c
  private           RootMotion.FinalIK.Grounding    grounding  // 0x128
  private           System.Single                   lastTime  // 0x130
  private           System.Single                   deltaTime  // 0x134
  private           UnityEngine.Vector3             lastPosition  // 0x138
  private           UnityEngine.Quaternion          toHitNormal  // 0x144
  private           UnityEngine.Quaternion          r  // 0x154
  private           UnityEngine.Vector3             up  // 0x164
  private           UnityEngine.Vector3             forward  // 0x170
  private           UnityEngine.Vector3             speedDir  // 0x17c
  private           UnityEngine.Vector3             lastTransformPosition  // 0x188
  private           System.Boolean                  doOverrideFootPosition  // 0x194
  private           UnityEngine.Vector3             overrideFootPosition  // 0x198
  public            UnityEngine.Vector3             transformPosition  // 0x1a4
  private   static readonly Unity.Profiling.ProfilerMarker  MARKER_TEMP_PREDICT_FOOT_CAST  // static @ 0x0
  private   static readonly Unity.Profiling.ProfilerMarker  MARKER_TEMP_BUILTIN_BEST_CAST  // static @ 0x8
  public    static  System.Single                   MAX_PREDICTION_DISTANCE  // const
  private           UnityEngine.Vector2             lastFramePredictPos  // 0x1b0
  public            UnityEngine.Vector3             predictOffset  // 0x1b8
  public            UnityEngine.Vector3             lastPredictOffset  // 0x1c4
  public            UnityEngine.Vector3             lastPredictOffsetwithPercent  // 0x1d0
  private           UnityEngine.Vector3             m_predictOffsetwithPercent  // 0x1dc
  private           System.Boolean                  hasPredictThisStep  // 0x1e8
  public            UnityEngine.RaycastHit          curPredictHit  // 0x1f0
  public            System.Single                   curStepTime  // 0x230
  public            UnityEngine.RaycastHit          curCapsuleHit  // 0x238
  public            UnityEngine.Vector3             lastLandFootPos  // 0x278
  private           System.Single                   feetOffsetFromRoot  // 0x284
  public            UnityEngine.Vector3             lastPathPoint  // 0x288
  public            UnityEngine.RaycastHit          m_heelHit  // 0x298
  public            UnityEngine.Vector3             curFeetCalculatePos  // 0x2d8
  private           UnityEngine.Vector3             lastCurHitPoint  // 0x2e4
  public            UnityEngine.Vector3             predictLimitForwardOffset  // 0x2f0
  private           System.Single                   m_maxStep  // 0x2fc
  private           UnityEngine.Vector3             m_lastHitNormal  // 0x300
  private           System.Boolean                  m_isInStair  // 0x30c
  public            System.Single                   footSpeed  // 0x310
  public            System.Single                   heightFromRoot  // 0x314
  public            System.Boolean                  isLastFrameLegStretch  // 0x318
  private           UnityEngine.Vector3             lastCapusuleHitPos  // 0x31c
PROPERTIES:
  isGrounded  get=0x015EFCE0  set=0x04D86130
  IKPosition  get=0x016057F0  set=0x04D91E50
  initiated  get=0x04D865F0  set=0x04D86620
  heightFromGround  get=0x04D8E050  set=0x04D8E060
  fowardMoveFromGround  get=0x04D88360  set=0x04D88370
  rightMoveFromGround  get=0x04D863E0  set=0x04D86460
  velocity  get=0x04D89660  set=0x04D89690
  transform  get=0x04D86270  set=0x02FDAEA0
  IKOffset  get=0x04D88400  set=0x04D88430
  IKOffsetZ  get=0x04D91500  set=0x04D91510
  IKOffsetX  get=0x04D88480  set=0x04D884A0
  heelHit  get=0x04DA95C0  set=0x04DA9620
  capsuleHit  get=0x04DA9590  set=0x04DA95F0
  GetHitPoint  get=0x0ABEBE48
  right  get=0x0326A020
  speedRight  get=0x0411E9B0
  predictOffsetwithPercent  get=0x03EE2080
  isLeft  get=0x03277EC0
  isMoving  get=0x0ABEBEBC
  stepHeightFromGround  get=0x03DB9310
  stepForwardFromGround  get=0x03DF3490
  stepRightFromGround  get=0x03DF34C0
  animFootGroundPos  get=0x03275760
  rootYOffset  get=0x03269C20
METHODS:
  RVA=0x04DA9570  token=0x6000228  System.Void SetFootPosition(UnityEngine.Vector3 position)
  RVA=0x03E5F250  token=0x600022B  System.Void Initiate(RootMotion.FinalIK.Grounding grounding, UnityEngine.Transform transform)
  RVA=0x03E5F330  token=0x600022C  System.Void OnEnable()
  RVA=0x0ABEBAE4  token=0x600022D  System.Void Reset()
  RVA=0x03BB64B0  token=0x600022F  System.Void _ResetPredictReference(UnityEngine.RaycastHit curHit)
  RVA=0x0327FC60  token=0x6000230  System.Void _PredictNextStepHit(UnityEngine.Vector3 prediction, UnityEngine.Vector3 predictVec, System.Boolean canChangeHeight)
  RVA=0x0327FEC0  token=0x6000231  System.Single _CanHitPointLand(UnityEngine.RaycastHit hit, UnityEngine.RaycastHit& predictHit)
  RVA=0x03277FD0  token=0x6000234  System.Void Process()
  RVA=0x03D24FE0  token=0x6000235  System.Void _UpdateHeelHit(System.Single curStepPercent, UnityEngine.Vector3 prediction)
  RVA=0x03D250E0  token=0x6000236  UnityEngine.Vector3 _UpdatePredictOffset(System.Single curStepPercent)
  RVA=0x0326BDB0  token=0x6000237  System.Void FinalSetIKPosition(UnityEngine.Vector3 thighPos)
  RVA=0x03913000  token=0x600023B  UnityEngine.RaycastHit GetCapsuleHit(UnityEngine.Vector3 offsetFromHeel, UnityEngine.Vector3 predictPos)
  RVA=0x0ABEAF98  token=0x600023C  UnityEngine.RaycastHit GetCapsuleHit(UnityEngine.Vector3 offsetFromHeel)
  RVA=0x032028C0  token=0x600023D  UnityEngine.RaycastHit GetRaycastHitCustom(UnityEngine.Vector3 offsetFromHeel)
  RVA=0x0ABEB694  token=0x600023E  UnityEngine.RaycastHit GetRaycastHit(UnityEngine.Vector3 offsetFromHeel)
  RVA=0x03268970  token=0x600023F  UnityEngine.Vector3 RotateNormal(UnityEngine.Vector3 normal)
  RVA=0x0ABEBD80  token=0x6000240  System.Void SetFootToPoint(UnityEngine.Vector3 normal, UnityEngine.Vector3 point)
  RVA=0x0ABEBB70  token=0x6000241  System.Void SetFootToPlane(UnityEngine.Vector3 planeNormal, UnityEngine.Vector3 planePoint, UnityEngine.Vector3 heelHitPoint)
  RVA=0x03268620  token=0x6000242  System.Void SetFootToPoint(UnityEngine.Vector3 normal, UnityEngine.Vector3 pointForXZ, UnityEngine.Vector3 pointForY)
  RVA=0x031CD2D0  token=0x6000243  System.Void SetFootToPlane(UnityEngine.Vector3 planeNormal, UnityEngine.Vector3 planePoint, UnityEngine.Vector3 heelHitPoint, UnityEngine.Vector3 footNormal, System.Boolean isLimitByComplex)
  RVA=0x03269590  token=0x6000244  System.Single GetHeightFromGround(UnityEngine.Vector3 hitPoint)
  RVA=0x0326C6A0  token=0x6000245  System.Void RotateFoot()
  RVA=0x04DA9560  token=0x6000246  UnityEngine.Quaternion GetRotationOffsetTarget()
  RVA=0x032689E0  token=0x6000249  UnityEngine.Vector3 RotateNormalWithLimit(UnityEngine.Vector3 normal)
  RVA=0x03E5F1E0  token=0x600024A  System.Void .ctor()
  RVA=0x04CE9F90  token=0x600024B  System.Void .cctor()
END_CLASS

CLASS: Pelvis
TYPE:  class
TOKEN: 0x200004A
SIZE:  0x48
FIELDS:
  private           UnityEngine.Vector3             <IKOffset>k__BackingField  // 0x10
  private           System.Single                   <heightOffset>k__BackingField  // 0x1c
  private           System.Single                   forwardOffset  // 0x20
  public            System.Single                   pelivsSpeedY  // 0x24
  private           RootMotion.FinalIK.Grounding    grounding  // 0x28
  private           UnityEngine.Vector3             lastRootPosition  // 0x30
  private           System.Single                   damperF  // 0x3c
  private           System.Boolean                  initiated  // 0x40
  private           System.Single                   lastTime  // 0x44
PROPERTIES:
  IKOffset  get=0x04D8C950  set=0x04D8C970
  heightOffset  get=0x04D868F0  set=0x04D86900
METHODS:
  RVA=0x03E5F390  token=0x6000250  System.Void Initiate(RootMotion.FinalIK.Grounding grounding)
  RVA=0x0ABEC63C  token=0x6000251  System.Void Reset()
  RVA=0x03E5F3C0  token=0x6000252  System.Void OnEnable()
  RVA=0x0326B180  token=0x6000253  System.Void Process(System.Single pelivsOffset, System.Single _forwardOffset, System.Boolean isGrounded, System.Boolean islastFrameLegStretch, System.Boolean isSameLegReflastFrame)
  RVA=0x0ABEC274  token=0x6000254  UnityEngine.RaycastHit GetRaycastHitCustom(UnityEngine.Vector3 rayOrigin)
  RVA=0x041E1670  token=0x6000255  System.Void .ctor()
END_CLASS

CLASS: References
TYPE:  class
TOKEN: 0x2000058
SIZE:  0xC0
FIELDS:
  public            UnityEngine.Transform           root  // 0x10
  public            UnityEngine.Transform           pelvis  // 0x18
  public            UnityEngine.Transform           spine  // 0x20
  public            UnityEngine.Transform           chest  // 0x28
  public            UnityEngine.Transform           neck  // 0x30
  public            UnityEngine.Transform           head  // 0x38
  public            UnityEngine.Transform           leftShoulder  // 0x40
  public            UnityEngine.Transform           leftUpperArm  // 0x48
  public            UnityEngine.Transform           leftForearm  // 0x50
  public            UnityEngine.Transform           leftHand  // 0x58
  public            UnityEngine.Transform           rightShoulder  // 0x60
  public            UnityEngine.Transform           rightUpperArm  // 0x68
  public            UnityEngine.Transform           rightForearm  // 0x70
  public            UnityEngine.Transform           rightHand  // 0x78
  public            UnityEngine.Transform           leftThigh  // 0x80
  public            UnityEngine.Transform           leftCalf  // 0x88
  public            UnityEngine.Transform           leftFoot  // 0x90
  public            UnityEngine.Transform           leftToes  // 0x98
  public            UnityEngine.Transform           rightThigh  // 0xa0
  public            UnityEngine.Transform           rightCalf  // 0xa8
  public            UnityEngine.Transform           rightFoot  // 0xb0
  public            UnityEngine.Transform           rightToes  // 0xb8
PROPERTIES:
  isFilled  get=0x0ABFE978
  isEmpty  get=0x0ABFE608
METHODS:
  RVA=0x041E1670  token=0x60002B1  System.Void .ctor()
  RVA=0x0ABFE3C4  token=0x60002B2  System.Void .ctor(RootMotion.BipedReferences b)
  RVA=0x0ABFE0A4  token=0x60002B3  UnityEngine.Transform[] GetTransforms()
  RVA=0x0ABFDC0C  token=0x60002B6  System.Boolean AutoDetectReferences(UnityEngine.Transform root, RootMotion.FinalIK.VRIK.References& references)
END_CLASS

CLASS: BendBone
TYPE:  class
TOKEN: 0x200005C
SIZE:  0x30
FIELDS:
  public            UnityEngine.Transform           transform  // 0x10
  public            System.Single                   weight  // 0x18
  private           UnityEngine.Quaternion          defaultLocalRotation  // 0x1c
METHODS:
  RVA=0x0ABEC75C  token=0x60002CF  System.Void .ctor()
  RVA=0x0ABEC724  token=0x60002D0  System.Void .ctor(UnityEngine.Transform transform, System.Single weight)
  RVA=0x0ABEC6F0  token=0x60002D1  System.Void StoreDefaultLocalState()
  RVA=0x0ABEC6BC  token=0x60002D2  System.Void FixTransforms()
END_CLASS

CLASS: ChildConstraint
TYPE:  class
TOKEN: 0x200005E
SIZE:  0x40
FIELDS:
  public            System.Single                   pushElasticity  // 0x10
  public            System.Single                   pullElasticity  // 0x14
  private           UnityEngine.Transform           bone1  // 0x18
  private           UnityEngine.Transform           bone2  // 0x20
  private           System.Single                   <nominalDistance>k__BackingField  // 0x28
  private           System.Boolean                  <isRigid>k__BackingField  // 0x2c
  private           System.Single                   crossFade  // 0x30
  private           System.Single                   inverseCrossFade  // 0x34
  private           System.Int32                    chain1Index  // 0x38
  private           System.Int32                    chain2Index  // 0x3c
PROPERTIES:
  nominalDistance  get=0x04D86BF0  set=0x04D86C00
  isRigid  get=0x04D869F0  set=0x04D86A00
METHODS:
  RVA=0x0ABEE7DC  token=0x60002EB  System.Void .ctor(UnityEngine.Transform bone1, UnityEngine.Transform bone2, System.Single pushElasticity, System.Single pullElasticity)
  RVA=0x0ABEE29C  token=0x60002EC  System.Void Initiate(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABEE2F4  token=0x60002ED  System.Void OnPreSolve(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABEE4C8  token=0x60002EE  System.Void Solve(RootMotion.FinalIK.IKSolverFullBody solver)
END_CLASS

CLASS: Smoothing
TYPE:  sealed struct
TOKEN: 0x200005F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.FBIKChain.SmoothingNone  // const
  public    static  RootMotion.FinalIK.FBIKChain.SmoothingExponential  // const
  public    static  RootMotion.FinalIK.FBIKChain.SmoothingCubic  // const
METHODS:
END_CLASS

CLASS: BoneMap
TYPE:  class
TOKEN: 0x2000063
SIZE:  0xD0
FIELDS:
  public            UnityEngine.Transform           transform  // 0x10
  public            System.Int32                    chainIndex  // 0x18
  public            System.Int32                    nodeIndex  // 0x1c
  public            UnityEngine.Vector3             defaultLocalPosition  // 0x20
  public            UnityEngine.Quaternion          defaultLocalRotation  // 0x2c
  public            UnityEngine.Vector3             localSwingAxis  // 0x3c
  public            UnityEngine.Vector3             localTwistAxis  // 0x48
  public            UnityEngine.Vector3             planePosition  // 0x54
  public            UnityEngine.Vector3             ikPosition  // 0x60
  public            UnityEngine.Quaternion          defaultLocalTargetRotation  // 0x6c
  private           UnityEngine.Quaternion          maintainRotation  // 0x7c
  public            System.Single                   length  // 0x8c
  public            UnityEngine.Quaternion          animatedRotation  // 0x90
  private           UnityEngine.Transform           planeBone1  // 0xa0
  private           UnityEngine.Transform           planeBone2  // 0xa8
  private           UnityEngine.Transform           planeBone3  // 0xb0
  private           System.Int32                    plane1ChainIndex  // 0xb8
  private           System.Int32                    plane1NodeIndex  // 0xbc
  private           System.Int32                    plane2ChainIndex  // 0xc0
  private           System.Int32                    plane2NodeIndex  // 0xc4
  private           System.Int32                    plane3ChainIndex  // 0xc8
  private           System.Int32                    plane3NodeIndex  // 0xcc
PROPERTIES:
  swingDirection  get=0x0ABEDA94
  isNodeBone  get=0x0ABED88C
  lastAnimatedTargetRotation  get=0x0ABED894
METHODS:
  RVA=0x0ABECBAC  token=0x600030F  System.Void Initiate(UnityEngine.Transform transform, RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABED324  token=0x6000311  System.Void StoreDefaultLocalState()
  RVA=0x0ABEC884  token=0x6000312  System.Void FixTransform(System.Boolean position)
  RVA=0x0ABECF80  token=0x6000314  System.Void SetLength(RootMotion.FinalIK.IKMapping.BoneMap nextBone)
  RVA=0x0ABED150  token=0x6000315  System.Void SetLocalSwingAxis(RootMotion.FinalIK.IKMapping.BoneMap swingTarget)
  RVA=0x0ABED020  token=0x6000316  System.Void SetLocalSwingAxis(RootMotion.FinalIK.IKMapping.BoneMap bone1, RootMotion.FinalIK.IKMapping.BoneMap bone2)
  RVA=0x0ABED15C  token=0x6000317  System.Void SetLocalTwistAxis(UnityEngine.Vector3 twistDirection, UnityEngine.Vector3 normalDirection)
  RVA=0x0ABED1FC  token=0x6000318  System.Void SetPlane(RootMotion.FinalIK.IKSolverFullBody solver, UnityEngine.Transform planeBone1, UnityEngine.Transform planeBone2, UnityEngine.Transform planeBone3)
  RVA=0x0ABED728  token=0x6000319  System.Void UpdatePlane(System.Boolean rotation, System.Boolean position)
  RVA=0x0ABECF44  token=0x600031A  System.Void SetIKPosition()
  RVA=0x0ABECBF8  token=0x600031B  System.Void MaintainRotation()
  RVA=0x0ABED2E8  token=0x600031C  System.Void SetToIKPosition()
  RVA=0x0ABEC780  token=0x600031D  System.Void FixToNode(RootMotion.FinalIK.IKSolverFullBody solver, System.Single weight, RootMotion.FinalIK.IKSolver.Node fixNode)
  RVA=0x0ABEC8EC  token=0x600031E  UnityEngine.Vector3 GetPlanePosition(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABECC2C  token=0x600031F  System.Void PositionToPlane(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABECE5C  token=0x6000320  System.Void RotateToPlane(RootMotion.FinalIK.IKSolverFullBody solver, System.Single weight)
  RVA=0x0ABED528  token=0x6000321  System.Void Swing(UnityEngine.Vector3 swingTarget, System.Single weight)
  RVA=0x0ABED380  token=0x6000322  System.Void Swing(UnityEngine.Vector3 pos1, UnityEngine.Vector3 pos2, System.Single weight)
  RVA=0x0ABED5AC  token=0x6000323  System.Void Twist(UnityEngine.Vector3 twistDirection, UnityEngine.Vector3 normalDirection, System.Single weight)
  RVA=0x0ABECDC4  token=0x6000324  System.Void RotateToMaintain(System.Single weight)
  RVA=0x0ABECC7C  token=0x6000325  System.Void RotateToEffector(RootMotion.FinalIK.IKSolverFullBody solver, System.Single weight)
  RVA=0x0ABEC9E4  token=0x6000326  UnityEngine.Quaternion GetTargetRotation(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x04DA9660  token=0x6000328  System.Void .ctor()
END_CLASS

CLASS: BoneMapType
TYPE:  sealed struct
TOKEN: 0x2000066
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.IKMappingLimb.BoneMapTypeParent  // const
  public    static  RootMotion.FinalIK.IKMappingLimb.BoneMapTypeBone1  // const
  public    static  RootMotion.FinalIK.IKMappingLimb.BoneMapTypeBone2  // const
  public    static  RootMotion.FinalIK.IKMappingLimb.BoneMapTypeBone3  // const
METHODS:
END_CLASS

CLASS: Point
TYPE:  class
TOKEN: 0x2000069
SIZE:  0x58
FIELDS:
  public            UnityEngine.Transform           transform  // 0x10
  public            System.Single                   weight  // 0x18
  public            UnityEngine.Vector3             solverPosition  // 0x1c
  public            UnityEngine.Quaternion          solverRotation  // 0x28
  public            UnityEngine.Vector3             defaultLocalPosition  // 0x38
  public            UnityEngine.Quaternion          defaultLocalRotation  // 0x44
METHODS:
  RVA=0x039DFD60  token=0x600035F  System.Void StoreDefaultLocalState()
  RVA=0x02FA5C50  token=0x6000360  System.Void FixTransform()
  RVA=0x0ABFDB74  token=0x6000361  System.Void UpdateSolverPosition()
  RVA=0x0ABFDADC  token=0x6000362  System.Void UpdateSolverLocalPosition()
  RVA=0x0ABFDBB0  token=0x6000363  System.Void UpdateSolverState()
  RVA=0x0ABFDB18  token=0x6000364  System.Void UpdateSolverLocalState()
  RVA=0x039DDFA0  token=0x6000365  System.Void .ctor()
END_CLASS

CLASS: Bone
TYPE:  class
TOKEN: 0x200006A
SIZE:  0x80
EXTENDS: Point
FIELDS:
  public            System.Single                   length  // 0x58
  public            System.Single                   sqrMag  // 0x5c
  public            UnityEngine.Vector3             axis  // 0x60
  private           RootMotion.FinalIK.RotationLimit_rotationLimit  // 0x70
  private           System.Boolean                  isLimited  // 0x78
PROPERTIES:
  rotationLimit  get=0x033B2120  set=0x0ABEE23C
METHODS:
  RVA=0x0ABEDF44  token=0x6000368  System.Void Swing(UnityEngine.Vector3 swingTarget, System.Single weight)
  RVA=0x0ABEDB4C  token=0x6000369  System.Void SolverSwing(RootMotion.FinalIK.IKSolver.Bone[] bones, System.Int32 index, UnityEngine.Vector3 swingTarget, System.Single weight)
  RVA=0x0ABEDD74  token=0x600036A  System.Void Swing2D(UnityEngine.Vector3 swingTarget, System.Single weight)
  RVA=0x0ABEDB10  token=0x600036B  System.Void SetToSolverPosition()
  RVA=0x039DDF40  token=0x600036C  System.Void .ctor()
  RVA=0x0ABEE1CC  token=0x600036D  System.Void .ctor(UnityEngine.Transform transform)
  RVA=0x0ABEE148  token=0x600036E  System.Void .ctor(UnityEngine.Transform transform, System.Single weight)
END_CLASS

CLASS: Node
TYPE:  class
TOKEN: 0x200006B
SIZE:  0x70
EXTENDS: Point
FIELDS:
  public            System.Single                   length  // 0x58
  public            System.Single                   effectorPositionWeight  // 0x5c
  public            System.Single                   effectorRotationWeight  // 0x60
  public            UnityEngine.Vector3             offset  // 0x64
METHODS:
  RVA=0x0ABFDA90  token=0x600036F  System.Void .ctor()
  RVA=0x0ABFDA60  token=0x6000370  System.Void .ctor(UnityEngine.Transform transform)
  RVA=0x0ABFDA98  token=0x6000371  System.Void .ctor(UnityEngine.Transform transform, System.Single weight)
END_CLASS

CLASS: UpdateDelegate
TYPE:  sealed class
TOKEN: 0x200006C
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x02F28650  token=0x6000372  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x6000373  System.Void Invoke()
  RVA=0x053DED94  token=0x6000374  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000375  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: IterationDelegate
TYPE:  sealed class
TOKEN: 0x200006D
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x069095FC  token=0x6000376  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000377  System.Void Invoke(System.Int32 i)
  RVA=0x0ABFD698  token=0x6000378  System.IAsyncResult BeginInvoke(System.Int32 i, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000379  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: BendModifier
TYPE:  sealed struct
TOKEN: 0x200007A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.IKSolverLimb.BendModifierAnimation  // const
  public    static  RootMotion.FinalIK.IKSolverLimb.BendModifierTarget  // const
  public    static  RootMotion.FinalIK.IKSolverLimb.BendModifierParent  // const
  public    static  RootMotion.FinalIK.IKSolverLimb.BendModifierArm  // const
  public    static  RootMotion.FinalIK.IKSolverLimb.BendModifierGoal  // const
METHODS:
END_CLASS

CLASS: AxisDirection
TYPE:  sealed struct
TOKEN: 0x200007B
SIZE:  0x2C
FIELDS:
  public            UnityEngine.Vector3             direction  // 0x10
  public            UnityEngine.Vector3             axis  // 0x1c
  public            System.Single                   dot  // 0x28
METHODS:
  RVA=0x033B3B20  token=0x6000421  System.Void .ctor(UnityEngine.Vector3 direction, UnityEngine.Vector3 axis)
END_CLASS

CLASS: LookAtBone
TYPE:  class
TOKEN: 0x200007D
SIZE:  0x98
EXTENDS: Bone
FIELDS:
  public            UnityEngine.Vector3             positionOffset  // 0x80
  public            UnityEngine.Vector3             baseForwardOffsetEuler  // 0x8c
PROPERTIES:
  forward  get=0x02F410B0
METHODS:
  RVA=0x039DDF20  token=0x6000443  System.Void .ctor()
  RVA=0x039DD630  token=0x6000444  System.Void .ctor(UnityEngine.Transform transform)
  RVA=0x043C8CC0  token=0x6000445  System.Void Initiate(UnityEngine.Transform root)
  RVA=0x03272B30  token=0x6000446  System.Void LookAt(UnityEngine.Vector3 direction, System.Single weight)
  RVA=0x0AC0E6A4  token=0x6000447  UnityEngine.Quaternion LookAtReturnRotation(UnityEngine.Vector3 direction, System.Single weight)
END_CLASS

CLASS: TrigonometricBone
TYPE:  class
TOKEN: 0x200007F
SIZE:  0xA0
EXTENDS: Bone
FIELDS:
  private           UnityEngine.Quaternion          targetToLocalSpace  // 0x80
  private           UnityEngine.Vector3             defaultLocalBendNormal  // 0x90
METHODS:
  RVA=0x04128940  token=0x6000460  System.Void Initiate(UnityEngine.Vector3 childPosition, UnityEngine.Vector3 bendNormal)
  RVA=0x03271130  token=0x6000461  UnityEngine.Quaternion GetRotation(UnityEngine.Vector3 direction, UnityEngine.Vector3 bendNormal)
  RVA=0x02F41150  token=0x6000462  UnityEngine.Vector3 GetBendNormalFromCurrentRotation()
  RVA=0x0539475C  token=0x6000463  System.Void .ctor()
END_CLASS

CLASS: ShoulderRotationMode
TYPE:  sealed struct
TOKEN: 0x2000082
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.IKSolverVR.Arm.ShoulderRotationModeYawPitch  // const
  public    static  RootMotion.FinalIK.IKSolverVR.Arm.ShoulderRotationModeFromTo  // const
METHODS:
END_CLASS

CLASS: Arm
TYPE:  class
TOKEN: 0x2000081
SIZE:  0x158
EXTENDS: BodyPart
FIELDS:
  public            UnityEngine.Transform           target  // 0x48
  public            System.Single                   positionWeight  // 0x50
  public            System.Single                   rotationWeight  // 0x54
  public            System.Single                   shoulderRotationWeight  // 0x58
  public            RootMotion.FinalIK.IKSolverVR.Arm.ShoulderRotationModeshoulderRotationMode  // 0x5c
  public            System.Single                   shoulderTwistWeight  // 0x60
  public            System.Single                   shoulderYawOffset  // 0x64
  public            System.Single                   shoulderPitchOffset  // 0x68
  public            UnityEngine.Transform           bendGoal  // 0x70
  public            System.Single                   bendGoalWeight  // 0x78
  public            System.Single                   swivelOffset  // 0x7c
  public            UnityEngine.Vector3             wristToPalmAxis  // 0x80
  public            UnityEngine.Vector3             palmToThumbAxis  // 0x8c
  public            System.Single                   armLengthMlp  // 0x98
  public            UnityEngine.AnimationCurve      stretchCurve  // 0xa0
  public            UnityEngine.Vector3             IKPosition  // 0xa8
  public            UnityEngine.Quaternion          IKRotation  // 0xb4
  public            UnityEngine.Vector3             bendDirection  // 0xc4
  public            UnityEngine.Vector3             handPositionOffset  // 0xd0
  private           UnityEngine.Vector3             <position>k__BackingField  // 0xdc
  private           UnityEngine.Quaternion          <rotation>k__BackingField  // 0xe8
  private           System.Boolean                  hasShoulder  // 0xf8
  private           UnityEngine.Vector3             chestForwardAxis  // 0xfc
  private           UnityEngine.Vector3             chestUpAxis  // 0x108
  private           UnityEngine.Quaternion          chestRotation  // 0x114
  private           UnityEngine.Vector3             chestForward  // 0x124
  private           UnityEngine.Vector3             chestUp  // 0x130
  private           UnityEngine.Quaternion          forearmRelToUpperArm  // 0x13c
  private           UnityEngine.Vector3             upperArmBendAxis  // 0x14c
PROPERTIES:
  position  get=0x04DA9720  set=0x04DA9740
  rotation  get=0x04DA0B10  set=0x04DA9760
  shoulder  get=0x0AC11E54
  upperArm  get=0x0AC11E7C
  forearm  get=0x0AC11DBC
  hand  get=0x0AC11E08
METHODS:
  RVA=0x0AC0EE9C  token=0x600048A  System.Void OnRead(UnityEngine.Vector3[] positions, UnityEngine.Quaternion[] rotations, System.Boolean hasChest, System.Boolean hasNeck, System.Boolean hasShoulders, System.Boolean hasToes, System.Boolean hasLegs, System.Int32 rootIndex, System.Int32 index)
  RVA=0x0AC0F89C  token=0x600048B  System.Void PreSolve(System.Single scale)
  RVA=0x0AC0E7A0  token=0x600048C  System.Void ApplyOffsets(System.Single scale)
  RVA=0x0AC1146C  token=0x600048D  System.Void Stretching()
  RVA=0x0AC0FAE4  token=0x600048E  System.Void Solve(System.Boolean isLeft)
  RVA=0x0AC0FAC8  token=0x600048F  System.Void ResetOffsets()
  RVA=0x0AC11A44  token=0x6000490  System.Void Write(UnityEngine.Vector3[]& solvedPositions, UnityEngine.Quaternion[]& solvedRotations)
  RVA=0x0AC0E808  token=0x6000491  System.Single DamperValue(System.Single value, System.Single min, System.Single max, System.Single weight)
  RVA=0x0AC0E8B0  token=0x6000492  UnityEngine.Vector3 GetBendNormal(UnityEngine.Vector3 dir)
  RVA=0x0AC1192C  token=0x6000493  System.Void Visualize(RootMotion.FinalIK.IKSolverVR.VirtualBone bone1, RootMotion.FinalIK.IKSolverVR.VirtualBone bone2, RootMotion.FinalIK.IKSolverVR.VirtualBone bone3, UnityEngine.Color color)
  RVA=0x0AC11C84  token=0x6000494  System.Void .ctor()
END_CLASS

CLASS: BodyPart
TYPE:  abstract class
TOKEN: 0x2000083
SIZE:  0x48
FIELDS:
  private           System.Single                   <sqrMag>k__BackingField  // 0x10
  private           System.Single                   <mag>k__BackingField  // 0x14
  public            RootMotion.FinalIK.IKSolverVR.VirtualBone[]bones  // 0x18
  protected         System.Boolean                  initiated  // 0x20
  protected         UnityEngine.Vector3             rootPosition  // 0x24
  protected         UnityEngine.Quaternion          rootRotation  // 0x30
  protected         System.Int32                    index  // 0x40
  protected         System.Int32                    LOD  // 0x44
PROPERTIES:
  sqrMag  get=0x04D85F70  set=0x04D88280
  mag  get=0x04D88D40  set=0x04D88D50
METHODS:
  RVA=-1  // abstract  token=0x6000495  System.Void OnRead(UnityEngine.Vector3[] positions, UnityEngine.Quaternion[] rotations, System.Boolean hasChest, System.Boolean hasNeck, System.Boolean hasShoulders, System.Boolean hasToes, System.Boolean hasLegs, System.Int32 rootIndex, System.Int32 index)
  RVA=-1  // abstract  token=0x6000496  System.Void PreSolve(System.Single scale)
  RVA=-1  // abstract  token=0x6000497  System.Void Write(UnityEngine.Vector3[]& solvedPositions, UnityEngine.Quaternion[]& solvedRotations)
  RVA=-1  // abstract  token=0x6000498  System.Void ApplyOffsets(System.Single scale)
  RVA=-1  // abstract  token=0x6000499  System.Void ResetOffsets()
  RVA=0x04D86580  token=0x600049E  System.Void SetLOD(System.Int32 LOD)
  RVA=0x0AC12064  token=0x600049F  System.Void Read(UnityEngine.Vector3[] positions, UnityEngine.Quaternion[] rotations, System.Boolean hasChest, System.Boolean hasNeck, System.Boolean hasShoulders, System.Boolean hasToes, System.Boolean hasLegs, System.Int32 rootIndex, System.Int32 index)
  RVA=0x0AC11EC4  token=0x60004A0  System.Void MovePosition(UnityEngine.Vector3 position)
  RVA=0x0AC11FA8  token=0x60004A1  System.Void MoveRotation(UnityEngine.Quaternion rotation)
  RVA=0x0AC123E8  token=0x60004A2  System.Void Translate(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0AC1228C  token=0x60004A3  System.Void TranslateRoot(UnityEngine.Vector3 newRootPos, UnityEngine.Quaternion newRootRot)
  RVA=0x0AC1216C  token=0x60004A4  System.Void RotateTo(RootMotion.FinalIK.IKSolverVR.VirtualBone bone, UnityEngine.Quaternion rotation, System.Single weight)
  RVA=0x0AC1243C  token=0x60004A5  System.Void Visualize(UnityEngine.Color color)
  RVA=0x0AC12554  token=0x60004A6  System.Void Visualize()
  RVA=0x0AC12584  token=0x60004A7  System.Void .ctor()
END_CLASS

CLASS: Footstep
TYPE:  class
TOKEN: 0x2000084
SIZE:  0xA8
FIELDS:
  public            System.Single                   stepSpeed  // 0x10
  public            UnityEngine.Vector3             characterSpaceOffset  // 0x14
  public            UnityEngine.Vector3             position  // 0x20
  public            UnityEngine.Quaternion          rotation  // 0x2c
  public            UnityEngine.Quaternion          stepToRootRot  // 0x3c
  public            System.Boolean                  isSupportLeg  // 0x4c
  public            System.Boolean                  relaxFlag  // 0x4d
  private           System.Single                   <stepProgress>k__BackingField  // 0x50
  public            UnityEngine.Vector3             stepFrom  // 0x54
  public            UnityEngine.Vector3             stepTo  // 0x60
  public            UnityEngine.Quaternion          stepFromRot  // 0x6c
  public            UnityEngine.Quaternion          stepToRot  // 0x7c
  private           UnityEngine.Quaternion          footRelativeToRoot  // 0x8c
  private           System.Single                   supportLegW  // 0x9c
  private           System.Single                   supportLegWV  // 0xa0
PROPERTIES:
  isStepping  get=0x0AC12B90
  stepProgress  get=0x04D885A0  set=0x04D88FF0
METHODS:
  RVA=0x0AC12A6C  token=0x60004AB  System.Void .ctor(UnityEngine.Quaternion rootRotation, UnityEngine.Vector3 footPosition, UnityEngine.Quaternion footRotation, UnityEngine.Vector3 characterSpaceOffset)
  RVA=0x04DA9770  token=0x60004AC  System.Void Reset(UnityEngine.Quaternion rootRotation, UnityEngine.Vector3 footPosition, UnityEngine.Quaternion footRotation)
  RVA=0x0AC125E4  token=0x60004AD  System.Void StepTo(UnityEngine.Vector3 p, UnityEngine.Quaternion rootRotation, System.Single stepThreshold)
  RVA=0x0AC12804  token=0x60004AE  System.Void UpdateStepping(UnityEngine.Vector3 p, UnityEngine.Quaternion rootRotation, System.Single speed, System.Single deltaTime)
  RVA=0x0AC1270C  token=0x60004AF  System.Void UpdateStanding(UnityEngine.Quaternion rootRotation, System.Single minAngle, System.Single speed, System.Single deltaTime)
  RVA=0x0AC12918  token=0x60004B0  System.Void Update(RootMotion.InterpolationMode interpolation, UnityEngine.Events.UnityEvent onStep, System.Single deltaTime)
END_CLASS

CLASS: Leg
TYPE:  class
TOKEN: 0x2000085
SIZE:  0x150
EXTENDS: BodyPart
FIELDS:
  public            UnityEngine.Transform           target  // 0x48
  public            System.Single                   positionWeight  // 0x50
  public            System.Single                   rotationWeight  // 0x54
  public            UnityEngine.Transform           bendGoal  // 0x58
  public            System.Single                   bendGoalWeight  // 0x60
  public            System.Single                   swivelOffset  // 0x64
  public            System.Single                   bendToTargetWeight  // 0x68
  public            System.Single                   legLengthMlp  // 0x6c
  public            UnityEngine.AnimationCurve      stretchCurve  // 0x70
  public            UnityEngine.Vector3             IKPosition  // 0x78
  public            UnityEngine.Quaternion          IKRotation  // 0x84
  public            UnityEngine.Vector3             footPositionOffset  // 0x94
  public            UnityEngine.Vector3             heelPositionOffset  // 0xa0
  public            UnityEngine.Quaternion          footRotationOffset  // 0xac
  public            System.Single                   currentMag  // 0xbc
  public            System.Boolean                  useAnimatedBendNormal  // 0xc0
  private           UnityEngine.Vector3             <position>k__BackingField  // 0xc4
  private           UnityEngine.Quaternion          <rotation>k__BackingField  // 0xd0
  private           System.Boolean                  <hasToes>k__BackingField  // 0xe0
  private           UnityEngine.Vector3             <thighRelativeToPelvis>k__BackingField  // 0xe4
  private           UnityEngine.Vector3             footPosition  // 0xf0
  private           UnityEngine.Quaternion          footRotation  // 0xfc
  private           UnityEngine.Vector3             bendNormal  // 0x10c
  private           UnityEngine.Quaternion          calfRelToThigh  // 0x118
  private           UnityEngine.Quaternion          thighRelToFoot  // 0x128
  private           UnityEngine.Vector3             <bendNormalRelToPelvis>k__BackingField  // 0x138
  private           UnityEngine.Vector3             <bendNormalRelToTarget>k__BackingField  // 0x144
PROPERTIES:
  position  get=0x04D9E3A0  set=0x04D9E440
  rotation  get=0x04D9E7C0  set=0x04DA12C0
  hasToes  get=0x04D885E0  set=0x04D88640
  thigh  get=0x0AC11E54
  calf  get=0x08355F28
  foot  get=0x0AC170B0
  toes  get=0x0AC17108
  lastBone  get=0x0AC170D8
  thighRelativeToPelvis  get=0x04DA96A0  set=0x04DA96C0
  bendNormalRelToPelvis  get=0x04DA97C0  set=0x04DA9800
  bendNormalRelToTarget  get=0x04DA97E0  set=0x04DA9820
METHODS:
  RVA=0x0AC156C8  token=0x60004C2  System.Void OnRead(UnityEngine.Vector3[] positions, UnityEngine.Quaternion[] rotations, System.Boolean hasChest, System.Boolean hasNeck, System.Boolean hasShoulders, System.Boolean hasToes, System.Boolean hasLegs, System.Int32 rootIndex, System.Int32 index)
  RVA=0x0AC15F04  token=0x60004C3  System.Void PreSolve(System.Single scale)
  RVA=0x0AC14A54  token=0x60004C4  System.Void ApplyOffsets(System.Single scale)
  RVA=0x0AC15098  token=0x60004C5  System.Void ApplyPositionOffset(UnityEngine.Vector3 offset, System.Single weight)
  RVA=0x0AC15180  token=0x60004C6  System.Void ApplyRotationOffset(UnityEngine.Quaternion offset, System.Single weight)
  RVA=0x0AC16540  token=0x60004C7  System.Void Solve(System.Boolean stretch)
  RVA=0x0AC15358  token=0x60004C8  System.Void FixTwistRotations()
  RVA=0x0AC167C8  token=0x60004C9  System.Void Stretching()
  RVA=0x0AC16D80  token=0x60004CA  System.Void Write(UnityEngine.Vector3[]& solvedPositions, UnityEngine.Quaternion[]& solvedRotations)
  RVA=0x0AC164D8  token=0x60004CB  System.Void ResetOffsets()
  RVA=0x0AC16FC4  token=0x60004CC  System.Void .ctor()
END_CLASS

CLASS: Mode
TYPE:  sealed struct
TOKEN: 0x2000087
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.IKSolverVR.Locomotion.ModeProcedural  // const
  public    static  RootMotion.FinalIK.IKSolverVR.Locomotion.ModeAnimated  // const
METHODS:
END_CLASS

CLASS: Locomotion
TYPE:  class
TOKEN: 0x2000086
SIZE:  0x180
FIELDS:
  public            RootMotion.FinalIK.IKSolverVR.Locomotion.Modemode  // 0x10
  public            System.Single                   weight  // 0x14
  public            System.Single                   moveThreshold  // 0x18
  private           System.Byte                     animationHeader  // 0x1c
  public            System.Single                   minAnimationSpeed  // 0x20
  public            System.Single                   maxAnimationSpeed  // 0x24
  public            System.Single                   animationSmoothTime  // 0x28
  private           System.Byte                     rootPositionHeader  // 0x2c
  public            UnityEngine.Vector2             standOffset  // 0x30
  public            System.Single                   rootLerpSpeedWhileMoving  // 0x38
  public            System.Single                   rootLerpSpeedWhileStopping  // 0x3c
  public            System.Single                   rootLerpSpeedWhileTurning  // 0x40
  public            System.Single                   maxRootOffset  // 0x44
  private           System.Byte                     rootRotationHeader  // 0x48
  public            System.Single                   maxRootAngleMoving  // 0x4c
  public            System.Single                   maxRootAngleStanding  // 0x50
  public            System.Single                   stepLengthMlp  // 0x54
  private           UnityEngine.Animator            animator  // 0x58
  private           UnityEngine.Vector3             velocityLocal  // 0x60
  private           UnityEngine.Vector3             velocityLocalV  // 0x6c
  private           UnityEngine.Vector3             lastCorrection  // 0x78
  private           UnityEngine.Vector3             lastHeadTargetPos  // 0x84
  private           UnityEngine.Vector3             lastSpeedRootPos  // 0x90
  private           UnityEngine.Vector3             lastEndRootPos  // 0x9c
  private           System.Single                   rootLerpSpeed  // 0xa8
  private           System.Single                   rootVelocityV  // 0xac
  private           System.Single                   animSpeed  // 0xb0
  private           System.Single                   animSpeedV  // 0xb4
  private           System.Single                   stopMoveTimer  // 0xb8
  private           System.Single                   turn  // 0xbc
  private           System.Single                   maxRootAngleV  // 0xc0
  private           System.Single                   currentAnimationSmoothTime  // 0xc4
  private           System.Boolean                  isMoving  // 0xc8
  private           System.Boolean                  firstFrame  // 0xc9
  private   static  System.Int32                    VRIK_Horizontal  // static @ 0x0
  private   static  System.Int32                    VRIK_Vertical  // static @ 0x4
  private   static  System.Int32                    VRIK_IsMoving  // static @ 0x8
  private   static  System.Int32                    VRIK_Speed  // static @ 0xc
  private   static  System.Int32                    VRIK_Turn  // static @ 0x10
  private   static  System.Boolean                  isHashed  // static @ 0x14
  private           System.Single                   lastVelLocalMag  // 0xcc
  public            System.Single                   footDistance  // 0xd0
  public            System.Single                   stepThreshold  // 0xd4
  public            System.Single                   angleThreshold  // 0xd8
  public            System.Single                   comAngleMlp  // 0xdc
  public            System.Single                   maxVelocity  // 0xe0
  public            System.Single                   velocityFactor  // 0xe4
  public            System.Single                   maxLegStretch  // 0xe8
  public            System.Single                   rootSpeed  // 0xec
  public            System.Single                   stepSpeed  // 0xf0
  public            UnityEngine.AnimationCurve      stepHeight  // 0xf8
  public            System.Single                   maxBodyYOffset  // 0x100
  public            UnityEngine.AnimationCurve      heelHeight  // 0x108
  public            System.Single                   relaxLegTwistMinAngle  // 0x110
  public            System.Single                   relaxLegTwistSpeed  // 0x114
  public            RootMotion.InterpolationMode    stepInterpolation  // 0x118
  public            UnityEngine.Vector3             offset  // 0x11c
  public            System.Boolean                  blockingEnabled  // 0x128
  public            UnityEngine.LayerMask           blockingLayers  // 0x12c
  public            System.Single                   raycastRadius  // 0x130
  public            System.Single                   raycastHeight  // 0x134
  public            UnityEngine.Events.UnityEvent   onLeftFootstep  // 0x138
  public            UnityEngine.Events.UnityEvent   onRightFootstep  // 0x140
  private           UnityEngine.Vector3             <centerOfMass>k__BackingField  // 0x148
  private           RootMotion.FinalIK.IKSolverVR.Footstep[]footsteps  // 0x158
  private           UnityEngine.Vector3             lastComPosition  // 0x160
  private           UnityEngine.Vector3             comVelocity  // 0x16c
  private           System.Int32                    leftFootIndex  // 0x178
  private           System.Int32                    rightFootIndex  // 0x17c
PROPERTIES:
  centerOfMass  get=0x04D90FC0  set=0x04D91040
  leftFootstepPosition  get=0x0AC1B360
  rightFootstepPosition  get=0x0AC1B3E4
  leftFootstepRotation  get=0x0AC1B3A4
  rightFootstepRotation  get=0x0AC1B428
METHODS:
  RVA=0x0AC18048  token=0x60004CD  System.Void Initiate(UnityEngine.Animator animator, UnityEngine.Vector3[] positions, UnityEngine.Quaternion[] rotations, System.Boolean hasToes, System.Single scale)
  RVA=0x0AC186B0  token=0x60004CE  System.Void Reset(UnityEngine.Vector3[] positions, UnityEngine.Quaternion[] rotations)
  RVA=0x0AC180FC  token=0x60004CF  System.Void Relax()
  RVA=0x0AC17A54  token=0x60004D0  System.Void AddDeltaRotation(UnityEngine.Quaternion delta, UnityEngine.Vector3 pivot)
  RVA=0x0AC173BC  token=0x60004D1  System.Void AddDeltaPosition(UnityEngine.Vector3 delta)
  RVA=0x0AC17CC8  token=0x60004D2  System.Void Initiate_Animated(UnityEngine.Animator animator, UnityEngine.Vector3[] positions)
  RVA=0x0AC18104  token=0x60004D3  System.Void ResetParams(UnityEngine.Vector3[] positions)
  RVA=0x0AC181C4  token=0x60004D4  System.Void Reset_Animated(UnityEngine.Vector3[] positions)
  RVA=0x0AC17418  token=0x60004D5  System.Void AddDeltaRotation_Animated(UnityEngine.Quaternion delta, UnityEngine.Vector3 pivot)
  RVA=0x0AC17130  token=0x60004D6  System.Void AddDeltaPosition_Animated(UnityEngine.Vector3 delta)
  RVA=0x0AC186E0  token=0x60004D7  System.Void Solve_Animated(RootMotion.FinalIK.IKSolverVR solver, System.Single scale, System.Single deltaTime)
  RVA=0x0AC17D7C  token=0x60004DA  System.Void Initiate_Procedural(UnityEngine.Vector3[] positions, UnityEngine.Quaternion[] rotations, System.Boolean hasToes, System.Single scale)
  RVA=0x0AC18414  token=0x60004DB  System.Void Reset_Procedural(UnityEngine.Vector3[] positions, UnityEngine.Quaternion[] rotations)
  RVA=0x0AC180A8  token=0x60004DC  System.Void Relax_Procedural()
  RVA=0x0AC17640  token=0x60004DD  System.Void AddDeltaRotation_Procedural(UnityEngine.Quaternion delta, UnityEngine.Vector3 pivot)
  RVA=0x0AC17234  token=0x60004DE  System.Void AddDeltaPosition_Procedural(UnityEngine.Vector3 delta)
  RVA=0x0AC196E4  token=0x60004DF  System.Void Solve_Procedural(RootMotion.FinalIK.IKSolverVR.VirtualBone rootBone, RootMotion.FinalIK.IKSolverVR.Spine spine, RootMotion.FinalIK.IKSolverVR.Leg leftLeg, RootMotion.FinalIK.IKSolverVR.Leg rightLeg, RootMotion.FinalIK.IKSolverVR.Arm leftArm, RootMotion.FinalIK.IKSolverVR.Arm rightArm, System.Int32 supportLegIndex, UnityEngine.Vector3& leftFootPosition, UnityEngine.Vector3& rightFootPosition, UnityEngine.Quaternion& leftFootRotation, UnityEngine.Quaternion& rightFootRotation, System.Single& leftFootOffset, System.Single& rightFootOffset, System.Single& leftHeelOffset, System.Single& rightHeelOffset, System.Single scale, System.Single deltaTime)
  RVA=0x0AC1B028  token=0x60004E4  System.Boolean StepBlocked(UnityEngine.Vector3 fromPosition, UnityEngine.Vector3 toPosition, UnityEngine.Vector3 rootPosition)
  RVA=0x0AC17AD8  token=0x60004E5  System.Boolean CanStep()
  RVA=0x0AC17B38  token=0x60004E6  System.Boolean GetLineSphereCollision(UnityEngine.Vector3 lineStart, UnityEngine.Vector3 lineEnd, UnityEngine.Vector3 sphereCenter, System.Single sphereRadius)
  RVA=0x0AC1B1B4  token=0x60004E7  System.Void .ctor()
END_CLASS

CLASS: Spine
TYPE:  class
TOKEN: 0x2000088
SIZE:  0x230
EXTENDS: BodyPart
FIELDS:
  public            UnityEngine.Transform           headTarget  // 0x48
  public            System.Single                   positionWeight  // 0x50
  public            System.Single                   rotationWeight  // 0x54
  public            System.Single                   headClampWeight  // 0x58
  public            System.Single                   minHeadHeight  // 0x5c
  public            System.Single                   useAnimatedHeadHeightWeight  // 0x60
  public            System.Single                   useAnimatedHeadHeightRange  // 0x64
  public            System.Single                   animatedHeadHeightBlend  // 0x68
  public            UnityEngine.Transform           pelvisTarget  // 0x70
  public            System.Single                   pelvisPositionWeight  // 0x78
  public            System.Single                   pelvisRotationWeight  // 0x7c
  public            System.Single                   maintainPelvisPosition  // 0x80
  public            UnityEngine.Transform           chestGoal  // 0x88
  public            System.Single                   chestGoalWeight  // 0x90
  public            System.Single                   chestClampWeight  // 0x94
  public            System.Single                   rotateChestByHands  // 0x98
  public            System.Single                   bodyPosStiffness  // 0x9c
  public            System.Single                   bodyRotStiffness  // 0xa0
  public            System.Single                   neckStiffness  // 0xa4
  public            System.Single                   moveBodyBackWhenCrouching  // 0xa8
  public            System.Single                   maxRootAngle  // 0xac
  public            System.Single                   rootHeadingOffset  // 0xb0
  public            UnityEngine.Vector3             IKPositionHead  // 0xb4
  public            UnityEngine.Quaternion          IKRotationHead  // 0xc0
  public            UnityEngine.Vector3             IKPositionPelvis  // 0xd0
  public            UnityEngine.Quaternion          IKRotationPelvis  // 0xdc
  public            UnityEngine.Vector3             goalPositionChest  // 0xec
  public            UnityEngine.Vector3             pelvisPositionOffset  // 0xf8
  public            UnityEngine.Vector3             chestPositionOffset  // 0x104
  public            UnityEngine.Vector3             headPositionOffset  // 0x110
  public            UnityEngine.Quaternion          pelvisRotationOffset  // 0x11c
  public            UnityEngine.Quaternion          chestRotationOffset  // 0x12c
  public            UnityEngine.Quaternion          headRotationOffset  // 0x13c
  public            UnityEngine.Vector3             faceDirection  // 0x14c
  private           UnityEngine.Vector3             headPosition  // 0x158
  private           UnityEngine.Quaternion          <anchorRotation>k__BackingField  // 0x164
  private           UnityEngine.Quaternion          <anchorRelativeToHead>k__BackingField  // 0x174
  private           UnityEngine.Quaternion          headRotation  // 0x184
  private           UnityEngine.Quaternion          pelvisRotation  // 0x194
  private           UnityEngine.Quaternion          anchorRelativeToPelvis  // 0x1a4
  private           UnityEngine.Quaternion          pelvisRelativeRotation  // 0x1b4
  private           UnityEngine.Quaternion          chestRelativeRotation  // 0x1c4
  private           UnityEngine.Vector3             headDeltaPosition  // 0x1d4
  private           UnityEngine.Quaternion          pelvisDeltaRotation  // 0x1e0
  private           UnityEngine.Quaternion          chestTargetRotation  // 0x1f0
  private           System.Int32                    pelvisIndex  // 0x200
  private           System.Int32                    spineIndex  // 0x204
  private           System.Int32                    chestIndex  // 0x208
  private           System.Int32                    neckIndex  // 0x20c
  private           System.Int32                    headIndex  // 0x210
  private           System.Single                   length  // 0x214
  private           System.Boolean                  hasChest  // 0x218
  private           System.Boolean                  hasNeck  // 0x219
  private           System.Boolean                  hasLegs  // 0x21a
  private           System.Single                   headHeight  // 0x21c
  private           System.Single                   sizeMlp  // 0x220
  private           UnityEngine.Vector3             chestForward  // 0x224
PROPERTIES:
  pelvis  get=0x0AC1EFBC
  firstSpineBone  get=0x0AC1EF2C
  chest  get=0x0AC1EEE8
  head  get=0x0AC1EF5C
  neck  get=0x0AC1EF8C
  anchorRotation  get=0x04DA9850  set=0x04DA9870
  anchorRelativeToHead  get=0x04DA9840  set=0x04DA9860
METHODS:
  RVA=0x0AC1CB00  token=0x60004F1  System.Void OnRead(UnityEngine.Vector3[] positions, UnityEngine.Quaternion[] rotations, System.Boolean hasChest, System.Boolean hasNeck, System.Boolean hasShoulders, System.Boolean hasToes, System.Boolean hasLegs, System.Int32 rootIndex, System.Int32 index)
  RVA=0x0AC1D668  token=0x60004F2  System.Void PreSolve(System.Single scale)
  RVA=0x0AC1B840  token=0x60004F3  System.Void ApplyOffsets(System.Single scale)
  RVA=0x0AC1C228  token=0x60004F4  System.Void CalculateChestTargetRotation(RootMotion.FinalIK.IKSolverVR.VirtualBone rootBone, RootMotion.FinalIK.IKSolverVR.Arm[] arms)
  RVA=0x0AC1DFB0  token=0x60004F5  System.Void Solve(UnityEngine.Animator animator, RootMotion.FinalIK.IKSolverVR.VirtualBone rootBone, RootMotion.FinalIK.IKSolverVR.Leg[] legs, RootMotion.FinalIK.IKSolverVR.Arm[] arms, System.Single scale)
  RVA=0x0AC1C3F0  token=0x60004F6  System.Void FABRIKPass(UnityEngine.Vector3 animatedPelvisPos, UnityEngine.Vector3 rootUp, System.Single weight)
  RVA=0x0AC1DC98  token=0x60004F7  System.Void SolvePelvis()
  RVA=0x0AC1EB20  token=0x60004F8  System.Void Write(UnityEngine.Vector3[]& solvedPositions, UnityEngine.Quaternion[]& solvedRotations)
  RVA=0x0AC1DBE4  token=0x60004F9  System.Void ResetOffsets()
  RVA=0x0AC1B468  token=0x60004FA  System.Void AdjustChestByHands(UnityEngine.Quaternion& chestTargetRotation, RootMotion.FinalIK.IKSolverVR.Arm[] arms)
  RVA=0x0AC1C614  token=0x60004FB  System.Void InverseTranslateToHead(RootMotion.FinalIK.IKSolverVR.Leg[] legs, System.Boolean limited, System.Boolean useCurrentLegMag, UnityEngine.Vector3 offset, System.Single w)
  RVA=0x0AC1E6C0  token=0x60004FC  System.Void TranslatePelvis(RootMotion.FinalIK.IKSolverVR.Leg[] legs, UnityEngine.Vector3 deltaPosition, UnityEngine.Quaternion deltaRotation, System.Single scale)
  RVA=0x0AC1C7A4  token=0x60004FD  UnityEngine.Vector3 LimitPelvisPosition(RootMotion.FinalIK.IKSolverVR.Leg[] legs, UnityEngine.Vector3 pelvisPosition, System.Boolean useCurrentLegMag, System.Int32 it)
  RVA=0x0AC1BDA4  token=0x60004FE  System.Void Bend(RootMotion.FinalIK.IKSolverVR.VirtualBone[] bones, System.Int32 firstIndex, System.Int32 lastIndex, UnityEngine.Quaternion targetRotation, System.Single clampWeight, System.Boolean uniformWeight, System.Single w)
  RVA=0x0AC1BFB8  token=0x60004FF  System.Void Bend(RootMotion.FinalIK.IKSolverVR.VirtualBone[] bones, System.Int32 firstIndex, System.Int32 lastIndex, UnityEngine.Quaternion targetRotation, UnityEngine.Quaternion rotationOffset, System.Single clampWeight, System.Boolean uniformWeight, System.Single w)
  RVA=0x0AC1ED2C  token=0x6000500  System.Void .ctor()
END_CLASS

CLASS: PositionOffset
TYPE:  sealed struct
TOKEN: 0x2000089
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.IKSolverVR.PositionOffsetPelvis  // const
  public    static  RootMotion.FinalIK.IKSolverVR.PositionOffsetChest  // const
  public    static  RootMotion.FinalIK.IKSolverVR.PositionOffsetHead  // const
  public    static  RootMotion.FinalIK.IKSolverVR.PositionOffsetLeftHand  // const
  public    static  RootMotion.FinalIK.IKSolverVR.PositionOffsetRightHand  // const
  public    static  RootMotion.FinalIK.IKSolverVR.PositionOffsetLeftFoot  // const
  public    static  RootMotion.FinalIK.IKSolverVR.PositionOffsetRightFoot  // const
  public    static  RootMotion.FinalIK.IKSolverVR.PositionOffsetLeftHeel  // const
  public    static  RootMotion.FinalIK.IKSolverVR.PositionOffsetRightHeel  // const
METHODS:
END_CLASS

CLASS: RotationOffset
TYPE:  sealed struct
TOKEN: 0x200008A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.IKSolverVR.RotationOffsetPelvis  // const
  public    static  RootMotion.FinalIK.IKSolverVR.RotationOffsetChest  // const
  public    static  RootMotion.FinalIK.IKSolverVR.RotationOffsetHead  // const
METHODS:
END_CLASS

CLASS: VirtualBone
TYPE:  class
TOKEN: 0x200008B
SIZE:  0x60
FIELDS:
  public            UnityEngine.Vector3             readPosition  // 0x10
  public            UnityEngine.Quaternion          readRotation  // 0x1c
  public            UnityEngine.Vector3             solverPosition  // 0x2c
  public            UnityEngine.Quaternion          solverRotation  // 0x38
  public            System.Single                   length  // 0x48
  public            System.Single                   sqrMag  // 0x4c
  public            UnityEngine.Vector3             axis  // 0x50
METHODS:
  RVA=0x04DA9880  token=0x6000501  System.Void .ctor(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x04DA9880  token=0x6000502  System.Void Read(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0AC21714  token=0x6000503  System.Void SwingRotation(RootMotion.FinalIK.IKSolverVR.VirtualBone[] bones, System.Int32 index, UnityEngine.Vector3 swingTarget, System.Single weight)
  RVA=0x0AC20200  token=0x6000504  System.Single PreSolve(RootMotion.FinalIK.IKSolverVR.VirtualBone[]& bones)
  RVA=0x0AC204B8  token=0x6000505  System.Void RotateAroundPoint(RootMotion.FinalIK.IKSolverVR.VirtualBone[] bones, System.Int32 index, UnityEngine.Vector3 point, UnityEngine.Quaternion rotation)
  RVA=0x0AC2063C  token=0x6000506  System.Void RotateBy(RootMotion.FinalIK.IKSolverVR.VirtualBone[] bones, System.Int32 index, UnityEngine.Quaternion rotation)
  RVA=0x0AC207FC  token=0x6000507  System.Void RotateBy(RootMotion.FinalIK.IKSolverVR.VirtualBone[] bones, UnityEngine.Quaternion rotation)
  RVA=0x0AC209B4  token=0x6000508  System.Void RotateTo(RootMotion.FinalIK.IKSolverVR.VirtualBone[] bones, System.Int32 index, UnityEngine.Quaternion rotation)
  RVA=0x0AC211CC  token=0x6000509  System.Void SolveTrigonometric(RootMotion.FinalIK.IKSolverVR.VirtualBone[] bones, System.Int32 first, System.Int32 second, System.Int32 third, UnityEngine.Vector3 targetPosition, UnityEngine.Vector3 bendNormal, System.Single weight)
  RVA=0x0AC20044  token=0x600050A  UnityEngine.Vector3 GetDirectionToBendPoint(UnityEngine.Vector3 direction, System.Single directionMag, UnityEngine.Vector3 bendDirection, System.Single sqrMag1, System.Single sqrMag2)
  RVA=0x0AC20D3C  token=0x600050B  System.Void SolveFABRIK(RootMotion.FinalIK.IKSolverVR.VirtualBone[] bones, UnityEngine.Vector3 startPosition, UnityEngine.Vector3 targetPosition, System.Single weight, System.Single minNormalizedTargetDistance, System.Int32 iterations, System.Single length, UnityEngine.Vector3 startOffset)
  RVA=0x0AC20C58  token=0x600050C  UnityEngine.Vector3 SolveFABRIKJoint(UnityEngine.Vector3 pos1, UnityEngine.Vector3 pos2, System.Single length)
  RVA=0x0AC20A68  token=0x600050D  System.Void SolveCCD(RootMotion.FinalIK.IKSolverVR.VirtualBone[] bones, UnityEngine.Vector3 targetPosition, System.Single weight, System.Int32 iterations)
END_CLASS

CLASS: InteractionEvent
TYPE:  class
TOKEN: 0x2000091
SIZE:  0x30
FIELDS:
  public            System.Single                   time  // 0x10
  public            System.Boolean                  pause  // 0x14
  public            System.Boolean                  pickUp  // 0x15
  public            RootMotion.FinalIK.InteractionObject.AnimatorEvent[]animations  // 0x18
  public            RootMotion.FinalIK.InteractionObject.Message[]messages  // 0x20
  public            UnityEngine.Events.UnityEvent   unityEvent  // 0x28
METHODS:
  RVA=0x0AC274B0  token=0x600054F  System.Void Activate(UnityEngine.Transform t)
  RVA=0x041E1670  token=0x6000550  System.Void .ctor()
END_CLASS

CLASS: Message
TYPE:  class
TOKEN: 0x2000092
SIZE:  0x20
FIELDS:
  public            System.String                   function  // 0x10
  public            UnityEngine.GameObject          recipient  // 0x18
  private   static  System.String                   empty  // const
METHODS:
  RVA=0x0AC2E2E0  token=0x6000551  System.Void Send(UnityEngine.Transform t)
  RVA=0x041E1670  token=0x6000552  System.Void .ctor()
END_CLASS

CLASS: AnimatorEvent
TYPE:  class
TOKEN: 0x2000093
SIZE:  0x38
FIELDS:
  public            UnityEngine.Animator            animator  // 0x10
  public            UnityEngine.Animation           animation  // 0x18
  public            System.String                   animationState  // 0x20
  public            System.Single                   crossfadeTime  // 0x28
  public            System.Int32                    layer  // 0x2c
  public            System.Boolean                  resetNormalizedTime  // 0x30
  private   static  System.String                   empty  // const
METHODS:
  RVA=0x0AC22CA8  token=0x6000553  System.Void Activate(System.Boolean pickUp)
  RVA=0x0AC22D68  token=0x6000554  System.Void Activate(UnityEngine.Animator animator)
  RVA=0x0AC22BEC  token=0x6000555  System.Void Activate(UnityEngine.Animation animation)
  RVA=0x04DA98B0  token=0x6000556  System.Void .ctor()
END_CLASS

CLASS: Type
TYPE:  sealed struct
TOKEN: 0x2000095
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.InteractionObject.WeightCurve.TypePositionWeight  // const
  public    static  RootMotion.FinalIK.InteractionObject.WeightCurve.TypeRotationWeight  // const
  public    static  RootMotion.FinalIK.InteractionObject.WeightCurve.TypePositionOffsetX  // const
  public    static  RootMotion.FinalIK.InteractionObject.WeightCurve.TypePositionOffsetY  // const
  public    static  RootMotion.FinalIK.InteractionObject.WeightCurve.TypePositionOffsetZ  // const
  public    static  RootMotion.FinalIK.InteractionObject.WeightCurve.TypePull  // const
  public    static  RootMotion.FinalIK.InteractionObject.WeightCurve.TypeReach  // const
  public    static  RootMotion.FinalIK.InteractionObject.WeightCurve.TypeRotateBoneWeight  // const
  public    static  RootMotion.FinalIK.InteractionObject.WeightCurve.TypePush  // const
  public    static  RootMotion.FinalIK.InteractionObject.WeightCurve.TypePushParent  // const
  public    static  RootMotion.FinalIK.InteractionObject.WeightCurve.TypePoserWeight  // const
  public    static  RootMotion.FinalIK.InteractionObject.WeightCurve.TypeBendGoalWeight  // const
METHODS:
END_CLASS

CLASS: WeightCurve
TYPE:  class
TOKEN: 0x2000094
SIZE:  0x20
FIELDS:
  public            RootMotion.FinalIK.InteractionObject.WeightCurve.Typetype  // 0x10
  public            UnityEngine.AnimationCurve      curve  // 0x18
METHODS:
  RVA=0x0AC337EC  token=0x6000557  System.Single GetValue(System.Single timer)
  RVA=0x041E1670  token=0x6000558  System.Void .ctor()
END_CLASS

CLASS: Multiplier
TYPE:  class
TOKEN: 0x2000096
SIZE:  0x20
FIELDS:
  public            RootMotion.FinalIK.InteractionObject.WeightCurve.Typecurve  // 0x10
  public            System.Single                   multiplier  // 0x14
  public            RootMotion.FinalIK.InteractionObject.WeightCurve.Typeresult  // 0x18
METHODS:
  RVA=0x0AC2E3B8  token=0x6000559  System.Single GetValue(RootMotion.FinalIK.InteractionObject.WeightCurve weightCurve, System.Single timer)
  RVA=0x015845C0  token=0x600055A  System.Void .ctor()
END_CLASS

CLASS: InteractionDelegate
TYPE:  sealed class
TOKEN: 0x2000098
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x053DE34C  token=0x6000599  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x600059A  System.Void Invoke(RootMotion.FinalIK.FullBodyBipedEffector effectorType, RootMotion.FinalIK.InteractionObject interactionObject)
  RVA=0x0AC273A8  token=0x600059B  System.IAsyncResult BeginInvoke(RootMotion.FinalIK.FullBodyBipedEffector effectorType, RootMotion.FinalIK.InteractionObject interactionObject, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x600059C  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: InteractionEventDelegate
TYPE:  sealed class
TOKEN: 0x2000099
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x09625A74  token=0x600059D  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x01546280  token=0x600059E  System.Void Invoke(RootMotion.FinalIK.FullBodyBipedEffector effectorType, RootMotion.FinalIK.InteractionObject interactionObject, RootMotion.FinalIK.InteractionObject.InteractionEvent interactionEvent)
  RVA=0x0AC27428  token=0x600059F  System.IAsyncResult BeginInvoke(RootMotion.FinalIK.FullBodyBipedEffector effectorType, RootMotion.FinalIK.InteractionObject interactionObject, RootMotion.FinalIK.InteractionObject.InteractionEvent interactionEvent, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x60005A0  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: RotationMode
TYPE:  sealed struct
TOKEN: 0x200009B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.InteractionTarget.RotationModeTwoDOF  // const
  public    static  RootMotion.FinalIK.InteractionTarget.RotationModeThreeDOF  // const
METHODS:
END_CLASS

CLASS: Multiplier
TYPE:  class
TOKEN: 0x200009C
SIZE:  0x18
FIELDS:
  public            RootMotion.FinalIK.InteractionObject.WeightCurve.Typecurve  // 0x10
  public            System.Single                   multiplier  // 0x14
METHODS:
  RVA=0x041E1670  token=0x60005AD  System.Void .ctor()
END_CLASS

CLASS: CharacterPosition
TYPE:  class
TOKEN: 0x200009E
SIZE:  0x30
FIELDS:
  public            System.Boolean                  use  // 0x10
  public            UnityEngine.Vector2             offset  // 0x14
  public            System.Single                   angleOffset  // 0x1c
  public            System.Single                   maxAngle  // 0x20
  public            System.Single                   radius  // 0x24
  public            System.Boolean                  orbit  // 0x28
  public            System.Boolean                  fixYAxis  // 0x29
PROPERTIES:
  offset3D  get=0x04DA9960
  direction3D  get=0x0AC24A8C
METHODS:
  RVA=0x0AC24444  token=0x60005B7  System.Boolean IsInRange(UnityEngine.Transform character, UnityEngine.Transform trigger, System.Single& error)
  RVA=0x04DA9950  token=0x60005B8  System.Void .ctor()
END_CLASS

CLASS: CameraPosition
TYPE:  class
TOKEN: 0x200009F
SIZE:  0x30
FIELDS:
  public            UnityEngine.Collider            lookAtTarget  // 0x10
  public            UnityEngine.Vector3             direction  // 0x18
  public            System.Single                   maxDistance  // 0x24
  public            System.Single                   maxAngle  // 0x28
  public            System.Boolean                  fixYAxis  // 0x2c
METHODS:
  RVA=0x0AC23FF0  token=0x60005B9  UnityEngine.Quaternion GetRotation()
  RVA=0x0AC24150  token=0x60005BA  System.Boolean IsInRange(UnityEngine.Transform raycastFrom, UnityEngine.RaycastHit hit, UnityEngine.Transform trigger, System.Single& error)
  RVA=0x0AC243EC  token=0x60005BB  System.Void .ctor()
END_CLASS

CLASS: Interaction
TYPE:  class
TOKEN: 0x20000A1
SIZE:  0x20
FIELDS:
  public            RootMotion.FinalIK.InteractionObjectinteractionObject  // 0x10
  public            RootMotion.FinalIK.FullBodyBipedEffector[]effectors  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60005BE  System.Void .ctor()
END_CLASS

CLASS: Range
TYPE:  class
TOKEN: 0x20000A0
SIZE:  0x38
FIELDS:
  public            System.String                   name  // 0x10
  public            System.Boolean                  show  // 0x18
  public            RootMotion.FinalIK.InteractionTrigger.CharacterPositioncharacterPosition  // 0x20
  public            RootMotion.FinalIK.InteractionTrigger.CameraPositioncameraPosition  // 0x28
  public            RootMotion.FinalIK.InteractionTrigger.Range.Interaction[]interactions  // 0x30
METHODS:
  RVA=0x0AC2FB40  token=0x60005BC  System.Boolean IsInRange(UnityEngine.Transform character, UnityEngine.Transform raycastFrom, UnityEngine.RaycastHit raycastHit, UnityEngine.Transform trigger, System.Single& maxError)
  RVA=0x04D8EB60  token=0x60005BD  System.Void .ctor()
END_CLASS

CLASS: Map
TYPE:  class
TOKEN: 0x20000A3
SIZE:  0x40
FIELDS:
  public            UnityEngine.Transform           bone  // 0x10
  public            UnityEngine.Transform           target  // 0x18
  private           UnityEngine.Vector3             defaultLocalPosition  // 0x20
  private           UnityEngine.Quaternion          defaultLocalRotation  // 0x2c
METHODS:
  RVA=0x0AC2E2AC  token=0x60005C6  System.Void .ctor(UnityEngine.Transform bone, UnityEngine.Transform target)
  RVA=0x0ABED324  token=0x60005C7  System.Void StoreDefaultState()
  RVA=0x0AC2E0F0  token=0x60005C8  System.Void FixTransform()
  RVA=0x0AC2E150  token=0x60005C9  System.Void Update(System.Single localRotationWeight, System.Single localPositionWeight)
END_CLASS

CLASS: Rigidbone
TYPE:  class
TOKEN: 0x20000A7
SIZE:  0x78
FIELDS:
  public            UnityEngine.Rigidbody           r  // 0x10
  public            UnityEngine.Transform           t  // 0x18
  public            UnityEngine.Collider            collider  // 0x20
  public            UnityEngine.Joint               joint  // 0x28
  public            UnityEngine.Rigidbody           c  // 0x30
  public            System.Boolean                  updateAnchor  // 0x38
  public            UnityEngine.Vector3             deltaPosition  // 0x3c
  public            UnityEngine.Quaternion          deltaRotation  // 0x48
  public            System.Single                   deltaTime  // 0x58
  public            UnityEngine.Vector3             lastPosition  // 0x5c
  public            UnityEngine.Quaternion          lastRotation  // 0x68
METHODS:
  RVA=0x0AC30384  token=0x60005EB  System.Void .ctor(UnityEngine.Rigidbody r)
  RVA=0x0AC30010  token=0x60005EC  System.Void RecordVelocity()
  RVA=0x0AC3012C  token=0x60005ED  System.Void WakeUp(System.Single velocityWeight, System.Single angularVelocityWeight)
END_CLASS

CLASS: Child
TYPE:  class
TOKEN: 0x20000A8
SIZE:  0x38
FIELDS:
  public            UnityEngine.Transform           t  // 0x10
  public            UnityEngine.Vector3             localPosition  // 0x18
  public            UnityEngine.Quaternion          localRotation  // 0x24
METHODS:
  RVA=0x0AC24CD4  token=0x60005EE  System.Void .ctor(UnityEngine.Transform transform)
  RVA=0x0AC24B1C  token=0x60005EF  System.Void FixTransform(System.Single weight)
  RVA=0x0AC24C78  token=0x60005F0  System.Void StoreLocalState()
END_CLASS

CLASS: <DisableRagdollSmooth>d__21
TYPE:  sealed class
TOKEN: 0x20000A9
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            RootMotion.FinalIK.RagdollUtility<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60005F1  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60005F2  System.Void System.IDisposable.Dispose()
  RVA=0x0AC334D8  token=0x60005F3  System.Boolean MoveNext()
  RVA=0x0AC336A0  token=0x60005F5  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: ReachCone
TYPE:  class
TOKEN: 0x20000AE
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3[]           tetrahedron  // 0x10
  public            System.Single                   volume  // 0x18
  public            UnityEngine.Vector3             S  // 0x1c
  public            UnityEngine.Vector3             B  // 0x28
PROPERTIES:
  o  get=0x0AC2FFE4
  a  get=0x0AC2FF44
  b  get=0x0AC2FF74
  c  get=0x0AC2FFA4
  isValid  get=0x0AC2FFD4
METHODS:
  RVA=0x0AC2FDE8  token=0x600062A  System.Void .ctor(UnityEngine.Vector3 _o, UnityEngine.Vector3 _a, UnityEngine.Vector3 _b, UnityEngine.Vector3 _c)
  RVA=0x0AC2FC30  token=0x600062C  System.Void Calculate()
END_CLASS

CLASS: LimitPoint
TYPE:  class
TOKEN: 0x20000AF
SIZE:  0x20
FIELDS:
  public            UnityEngine.Vector3             point  // 0x10
  public            System.Single                   tangentWeight  // 0x1c
METHODS:
  RVA=0x0AC2D41C  token=0x600062D  System.Void .ctor()
END_CLASS

CLASS: <TurnToTarget>d__37
TYPE:  sealed class
TOKEN: 0x20000B2
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            RootMotion.FinalIK.AimController<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600063D  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600063E  System.Void System.IDisposable.Dispose()
  RVA=0x0AC336EC  token=0x600063F  System.Boolean MoveNext()
  RVA=0x0AC337A0  token=0x6000641  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Pose
TYPE:  class
TOKEN: 0x20000B4
SIZE:  0x38
FIELDS:
  public            System.Boolean                  visualize  // 0x10
  public            System.String                   name  // 0x18
  public            UnityEngine.Vector3             direction  // 0x20
  public            System.Single                   yaw  // 0x2c
  public            System.Single                   pitch  // 0x30
  private           System.Single                   angleBuffer  // 0x34
METHODS:
  RVA=0x0AC2E5BC  token=0x6000646  System.Boolean IsInDirection(UnityEngine.Vector3 d)
  RVA=0x04D8E060  token=0x6000647  System.Void SetAngleBuffer(System.Single value)
  RVA=0x04DA99C0  token=0x6000648  System.Void .ctor()
END_CLASS

CLASS: EffectorLink
TYPE:  class
TOKEN: 0x20000B7
SIZE:  0x18
FIELDS:
  public            RootMotion.FinalIK.FullBodyBipedEffectoreffector  // 0x10
  public            System.Single                   weight  // 0x14
METHODS:
  RVA=0x041E1670  token=0x600064E  System.Void .ctor()
END_CLASS

CLASS: Body
TYPE:  class
TOKEN: 0x20000B6
SIZE:  0x50
FIELDS:
  public            UnityEngine.Transform           transform  // 0x10
  public            UnityEngine.Transform           relativeTo  // 0x18
  public            RootMotion.FinalIK.Amplifier.Body.EffectorLink[]effectorLinks  // 0x20
  public            System.Single                   verticalWeight  // 0x28
  public            System.Single                   horizontalWeight  // 0x2c
  public            System.Single                   speed  // 0x30
  private           UnityEngine.Vector3             lastRelativePos  // 0x34
  private           UnityEngine.Vector3             smoothDelta  // 0x40
  private           System.Boolean                  firstUpdate  // 0x4c
METHODS:
  RVA=0x0AC23578  token=0x600064B  System.Void Update(RootMotion.FinalIK.IKSolverFullBodyBiped solver, System.Single w, System.Single deltaTime)
  RVA=0x04DA98C0  token=0x600064C  UnityEngine.Vector3 Multiply(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2)
  RVA=0x04DA9920  token=0x600064D  System.Void .ctor()
END_CLASS

CLASS: HitPoint
TYPE:  abstract class
TOKEN: 0x20000BD
SIZE:  0x48
FIELDS:
  public            System.String                   name  // 0x10
  public            UnityEngine.Collider            collider  // 0x18
  private           System.Single                   crossFadeTime  // 0x20
  private           System.Single                   <crossFader>k__BackingField  // 0x24
  private           System.Single                   <timer>k__BackingField  // 0x28
  private           UnityEngine.Vector3             <force>k__BackingField  // 0x2c
  private           System.Single                   length  // 0x38
  private           System.Single                   crossFadeSpeed  // 0x3c
  private           System.Single                   lastTime  // 0x40
PROPERTIES:
  inProgress  get=0x0AC26CF8
  crossFader  get=0x04D89850  set=0x04D89860
  timer  get=0x04D86BF0  set=0x04D86C00
  force  get=0x04D90540  set=0x04D90510
METHODS:
  RVA=0x0AC26C1C  token=0x600066E  System.Void Hit(UnityEngine.Vector3 force, UnityEngine.Vector3 point)
  RVA=0x0AC26B54  token=0x600066F  System.Void Apply(RootMotion.FinalIK.IKSolverFullBodyBiped solver, System.Single weight)
  RVA=-1  // abstract  token=0x6000670  System.Single GetLength()
  RVA=-1  // abstract  token=0x6000671  System.Void CrossFadeStart()
  RVA=-1  // abstract  token=0x6000672  System.Void OnApply(RootMotion.FinalIK.IKSolverFullBodyBiped solver, System.Single weight)
  RVA=0x04DA99A0  token=0x6000673  System.Void .ctor()
END_CLASS

CLASS: EffectorLink
TYPE:  class
TOKEN: 0x20000BF
SIZE:  0x30
FIELDS:
  public            RootMotion.FinalIK.FullBodyBipedEffectoreffector  // 0x10
  public            System.Single                   weight  // 0x14
  private           UnityEngine.Vector3             lastValue  // 0x18
  private           UnityEngine.Vector3             current  // 0x24
METHODS:
  RVA=0x0AC258EC  token=0x6000678  System.Void Apply(RootMotion.FinalIK.IKSolverFullBodyBiped solver, UnityEngine.Vector3 offset, System.Single crossFader)
  RVA=0x04DA9980  token=0x6000679  System.Void CrossFadeStart()
  RVA=0x041E1670  token=0x600067A  System.Void .ctor()
END_CLASS

CLASS: HitPointEffector
TYPE:  class
TOKEN: 0x20000BE
SIZE:  0x60
EXTENDS: HitPoint
FIELDS:
  public            UnityEngine.AnimationCurve      offsetInForceDirection  // 0x48
  public            UnityEngine.AnimationCurve      offsetInUpDirection  // 0x50
  public            RootMotion.FinalIK.HitReaction.HitPointEffector.EffectorLink[]effectorLinks  // 0x58
METHODS:
  RVA=0x0AC26874  token=0x6000674  System.Single GetLength()
  RVA=0x0AC26828  token=0x6000675  System.Void CrossFadeStart()
  RVA=0x0AC2697C  token=0x6000676  System.Void OnApply(RootMotion.FinalIK.IKSolverFullBodyBiped solver, System.Single weight)
  RVA=0x04DA99A0  token=0x6000677  System.Void .ctor()
END_CLASS

CLASS: BoneLink
TYPE:  class
TOKEN: 0x20000C1
SIZE:  0x40
FIELDS:
  public            UnityEngine.Transform           bone  // 0x10
  public            System.Single                   weight  // 0x18
  private           UnityEngine.Quaternion          lastValue  // 0x1c
  private           UnityEngine.Quaternion          current  // 0x2c
METHODS:
  RVA=0x0AC239B8  token=0x6000680  System.Void Apply(RootMotion.FinalIK.IKSolverFullBodyBiped solver, UnityEngine.Quaternion offset, System.Single crossFader)
  RVA=0x04DA9940  token=0x6000681  System.Void CrossFadeStart()
  RVA=0x0AC23ABC  token=0x6000682  System.Void .ctor()
END_CLASS

CLASS: HitPointBone
TYPE:  class
TOKEN: 0x20000C0
SIZE:  0x70
EXTENDS: HitPoint
FIELDS:
  public            UnityEngine.AnimationCurve      aroundCenterOfMass  // 0x48
  public            RootMotion.FinalIK.HitReaction.HitPointBone.BoneLink[]boneLinks  // 0x50
  private           UnityEngine.Rigidbody           rigidbody  // 0x58
  private           UnityEngine.Vector3             comAxis  // 0x60
METHODS:
  RVA=0x0AC26594  token=0x600067B  System.Void Hit(UnityEngine.Vector3 force, UnityEngine.Vector3 point)
  RVA=0x0AC26518  token=0x600067C  System.Single GetLength()
  RVA=0x0AC264D4  token=0x600067D  System.Void CrossFadeStart()
  RVA=0x0AC26754  token=0x600067E  System.Void OnApply(RootMotion.FinalIK.IKSolverFullBodyBiped solver, System.Single weight)
  RVA=0x04DA99A0  token=0x600067F  System.Void .ctor()
END_CLASS

CLASS: Offset
TYPE:  abstract class
TOKEN: 0x20000C3
SIZE:  0x48
FIELDS:
  public            System.String                   name  // 0x10
  public            UnityEngine.Collider            collider  // 0x18
  private           System.Single                   crossFadeTime  // 0x20
  private           System.Single                   <crossFader>k__BackingField  // 0x24
  private           System.Single                   <timer>k__BackingField  // 0x28
  private           UnityEngine.Vector3             <force>k__BackingField  // 0x2c
  private           System.Single                   length  // 0x38
  private           System.Single                   crossFadeSpeed  // 0x3c
  private           System.Single                   lastTime  // 0x40
PROPERTIES:
  crossFader  get=0x04D89850  set=0x04D89860
  timer  get=0x04D86BF0  set=0x04D86C00
  force  get=0x04D90540  set=0x04D90510
METHODS:
  RVA=0x0AC2E4C0  token=0x600068C  System.Void Hit(UnityEngine.Vector3 force, UnityEngine.AnimationCurve[] curves, UnityEngine.Vector3 point)
  RVA=0x0AC2E3E8  token=0x600068D  System.Void Apply(RootMotion.FinalIK.VRIK ik, UnityEngine.AnimationCurve[] curves, System.Single weight)
  RVA=-1  // abstract  token=0x600068E  System.Single GetLength(UnityEngine.AnimationCurve[] curves)
  RVA=-1  // abstract  token=0x600068F  System.Void CrossFadeStart()
  RVA=-1  // abstract  token=0x6000690  System.Void OnApply(RootMotion.FinalIK.VRIK ik, UnityEngine.AnimationCurve[] curves, System.Single weight)
  RVA=0x04DA99A0  token=0x6000691  System.Void .ctor()
END_CLASS

CLASS: PositionOffsetLink
TYPE:  class
TOKEN: 0x20000C5
SIZE:  0x30
FIELDS:
  public            RootMotion.FinalIK.IKSolverVR.PositionOffsetpositionOffset  // 0x10
  public            System.Single                   weight  // 0x14
  private           UnityEngine.Vector3             lastValue  // 0x18
  private           UnityEngine.Vector3             current  // 0x24
METHODS:
  RVA=0x0AC2E858  token=0x6000696  System.Void Apply(RootMotion.FinalIK.VRIK ik, UnityEngine.Vector3 offset, System.Single crossFader)
  RVA=0x04DA9980  token=0x6000697  System.Void CrossFadeStart()
  RVA=0x041E1670  token=0x6000698  System.Void .ctor()
END_CLASS

CLASS: PositionOffset
TYPE:  class
TOKEN: 0x20000C4
SIZE:  0x58
EXTENDS: Offset
FIELDS:
  public            System.Int32                    forceDirCurveIndex  // 0x48
  public            System.Int32                    upDirCurveIndex  // 0x4c
  public            RootMotion.FinalIK.HitReactionVRIK.PositionOffset.PositionOffsetLink[]offsetLinks  // 0x50
METHODS:
  RVA=0x0AC2E954  token=0x6000692  System.Single GetLength(UnityEngine.AnimationCurve[] curves)
  RVA=0x0AC2E908  token=0x6000693  System.Void CrossFadeStart()
  RVA=0x0AC2EAC8  token=0x6000694  System.Void OnApply(RootMotion.FinalIK.VRIK ik, UnityEngine.AnimationCurve[] curves, System.Single weight)
  RVA=0x04DA99E0  token=0x6000695  System.Void .ctor()
END_CLASS

CLASS: RotationOffsetLink
TYPE:  class
TOKEN: 0x20000C7
SIZE:  0x38
FIELDS:
  public            RootMotion.FinalIK.IKSolverVR.RotationOffsetrotationOffset  // 0x10
  public            System.Single                   weight  // 0x14
  private           UnityEngine.Quaternion          lastValue  // 0x18
  private           UnityEngine.Quaternion          current  // 0x28
METHODS:
  RVA=0x0AC32F30  token=0x600069E  System.Void Apply(RootMotion.FinalIK.VRIK ik, UnityEngine.Quaternion offset, System.Single crossFader)
  RVA=0x04DA9A20  token=0x600069F  System.Void CrossFadeStart()
  RVA=0x0AC3300C  token=0x60006A0  System.Void .ctor()
END_CLASS

CLASS: RotationOffset
TYPE:  class
TOKEN: 0x20000C6
SIZE:  0x70
EXTENDS: Offset
FIELDS:
  public            System.Int32                    curveIndex  // 0x48
  public            RootMotion.FinalIK.HitReactionVRIK.RotationOffset.RotationOffsetLink[]offsetLinks  // 0x50
  private           UnityEngine.Rigidbody           rigidbody  // 0x58
  private           UnityEngine.Vector3             comAxis  // 0x60
METHODS:
  RVA=0x0AC33138  token=0x6000699  System.Void Hit(UnityEngine.Vector3 force, UnityEngine.AnimationCurve[] curves, UnityEngine.Vector3 point)
  RVA=0x0AC33080  token=0x600069A  System.Single GetLength(UnityEngine.AnimationCurve[] curves)
  RVA=0x0AC3303C  token=0x600069B  System.Void CrossFadeStart()
  RVA=0x0AC33300  token=0x600069C  System.Void OnApply(RootMotion.FinalIK.VRIK ik, UnityEngine.AnimationCurve[] curves, System.Single weight)
  RVA=0x04DA99A0  token=0x600069D  System.Void .ctor()
END_CLASS

CLASS: EffectorLink
TYPE:  class
TOKEN: 0x20000CA
SIZE:  0x18
FIELDS:
  public            RootMotion.FinalIK.FullBodyBipedEffectoreffector  // 0x10
  public            System.Single                   weight  // 0x14
METHODS:
  RVA=0x041E1670  token=0x60006A7  System.Void .ctor()
END_CLASS

CLASS: Body
TYPE:  class
TOKEN: 0x20000C9
SIZE:  0x68
FIELDS:
  public            UnityEngine.Transform           transform  // 0x10
  public            RootMotion.FinalIK.Inertia.Body.EffectorLink[]effectorLinks  // 0x18
  public            System.Single                   speed  // 0x20
  public            System.Single                   acceleration  // 0x24
  public            System.Single                   matchVelocity  // 0x28
  public            System.Single                   gravity  // 0x2c
  private           UnityEngine.Vector3             delta  // 0x30
  private           UnityEngine.Vector3             lazyPoint  // 0x3c
  private           UnityEngine.Vector3             direction  // 0x48
  private           UnityEngine.Vector3             lastPosition  // 0x54
  private           System.Boolean                  firstUpdate  // 0x60
METHODS:
  RVA=0x0AC23044  token=0x60006A4  System.Void Reset()
  RVA=0x0AC23104  token=0x60006A5  System.Void Update(RootMotion.FinalIK.IKSolverFullBodyBiped solver, System.Single weight, System.Single deltaTime)
  RVA=0x04DA9900  token=0x60006A6  System.Void .ctor()
END_CLASS

CLASS: OffsetLimits
TYPE:  class
TOKEN: 0x20000CD
SIZE:  0x38
FIELDS:
  public            RootMotion.FinalIK.FullBodyBipedEffectoreffector  // 0x10
  public            System.Single                   spring  // 0x14
  public            System.Boolean                  x  // 0x18
  public            System.Boolean                  y  // 0x19
  public            System.Boolean                  z  // 0x1a
  public            System.Single                   minX  // 0x1c
  public            System.Single                   maxX  // 0x20
  public            System.Single                   minY  // 0x24
  public            System.Single                   maxY  // 0x28
  public            System.Single                   minZ  // 0x2c
  public            System.Single                   maxZ  // 0x30
METHODS:
  RVA=0x0AC34040  token=0x60006B6  System.Void Apply(RootMotion.FinalIK.IKEffector e, UnityEngine.Quaternion rootRotation)
  RVA=0x0AC34218  token=0x60006B7  System.Single SpringAxis(System.Single value, System.Single min, System.Single max)
  RVA=0x0AC3424C  token=0x60006B8  System.Single Spring(System.Single value, System.Single limit, System.Boolean negative)
  RVA=0x041E1670  token=0x60006B9  System.Void .ctor()
END_CLASS

CLASS: <Initiate>d__8
TYPE:  sealed class
TOKEN: 0x20000CE
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            RootMotion.FinalIK.OffsetModifier<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60006BA  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60006BB  System.Void System.IDisposable.Dispose()
  RVA=0x0AC36A50  token=0x60006BC  System.Boolean MoveNext()
  RVA=0x0AC36BBC  token=0x60006BE  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Initiate>d__7
TYPE:  sealed class
TOKEN: 0x20000D0
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            RootMotion.FinalIK.OffsetModifierVRIK<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60006C7  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60006C8  System.Void System.IDisposable.Dispose()
  RVA=0x0AC36898  token=0x60006C9  System.Boolean MoveNext()
  RVA=0x0AC36A04  token=0x60006CB  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: EffectorLink
TYPE:  class
TOKEN: 0x20000D2
SIZE:  0x38
FIELDS:
  public            RootMotion.FinalIK.FullBodyBipedEffectoreffector  // 0x10
  public            UnityEngine.Vector3             offset  // 0x14
  public            UnityEngine.Vector3             pin  // 0x20
  public            UnityEngine.Vector3             pinWeight  // 0x2c
METHODS:
  RVA=0x0AC33D78  token=0x60006D0  System.Void Apply(RootMotion.FinalIK.IKSolverFullBodyBiped solver, System.Single weight, UnityEngine.Quaternion rotation)
  RVA=0x041E1670  token=0x60006D1  System.Void .ctor()
END_CLASS

CLASS: EffectorLink
TYPE:  class
TOKEN: 0x20000D5
SIZE:  0x18
FIELDS:
  public            RootMotion.FinalIK.FullBodyBipedEffectoreffector  // 0x10
  public            System.Single                   weight  // 0x14
METHODS:
  RVA=0x041E1670  token=0x60006D8  System.Void .ctor()
END_CLASS

CLASS: Avoider
TYPE:  class
TOKEN: 0x20000D4
SIZE:  0x60
FIELDS:
  public            UnityEngine.Transform[]         raycastFrom  // 0x10
  public            UnityEngine.Transform           raycastTo  // 0x18
  public            System.Single                   raycastRadius  // 0x20
  public            RootMotion.FinalIK.PenetrationAvoidance.Avoider.EffectorLink[]effectors  // 0x28
  public            System.Single                   smoothTimeIn  // 0x30
  public            System.Single                   smoothTimeOut  // 0x34
  public            UnityEngine.LayerMask           layers  // 0x38
  private           UnityEngine.Vector3             offset  // 0x3c
  private           UnityEngine.Vector3             offsetTarget  // 0x48
  private           UnityEngine.Vector3             offsetV  // 0x54
METHODS:
  RVA=0x0AC33BB4  token=0x60006D4  System.Void Solve(RootMotion.FinalIK.IKSolverFullBodyBiped solver, System.Single weight)
  RVA=0x0AC3380C  token=0x60006D5  UnityEngine.Vector3 GetOffsetTarget(RootMotion.FinalIK.IKSolverFullBodyBiped solver)
  RVA=0x0AC3398C  token=0x60006D6  UnityEngine.Vector3 Raycast(UnityEngine.Vector3 from, UnityEngine.Vector3 to)
  RVA=0x04DA9A30  token=0x60006D7  System.Void .ctor()
END_CLASS

CLASS: EffectorLink
TYPE:  class
TOKEN: 0x20000D8
SIZE:  0x18
FIELDS:
  public            RootMotion.FinalIK.FullBodyBipedEffectoreffector  // 0x10
  public            System.Single                   weight  // 0x14
METHODS:
  RVA=0x041E1670  token=0x60006E8  System.Void .ctor()
END_CLASS

CLASS: RecoilOffset
TYPE:  class
TOKEN: 0x20000D7
SIZE:  0x48
FIELDS:
  public            UnityEngine.Vector3             offset  // 0x10
  public            System.Single                   additivity  // 0x1c
  public            System.Single                   maxAdditiveOffsetMag  // 0x20
  public            RootMotion.FinalIK.Recoil.RecoilOffset.EffectorLink[]effectorLinks  // 0x28
  private           UnityEngine.Vector3             additiveOffset  // 0x30
  private           UnityEngine.Vector3             lastOffset  // 0x3c
METHODS:
  RVA=0x0AC34C68  token=0x60006E5  System.Void Start()
  RVA=0x0AC34A54  token=0x60006E6  System.Void Apply(RootMotion.FinalIK.IKSolverFullBodyBiped solver, UnityEngine.Quaternion rotation, System.Single masterWeight, System.Single length, System.Single timeLeft)
  RVA=0x04DA9A40  token=0x60006E7  System.Void .ctor()
END_CLASS

CLASS: Handedness
TYPE:  sealed struct
TOKEN: 0x20000D9
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.Recoil.HandednessRight  // const
  public    static  RootMotion.FinalIK.Recoil.HandednessLeft  // const
METHODS:
END_CLASS

CLASS: Settings
TYPE:  class
TOKEN: 0x20000DC
SIZE:  0x88
FIELDS:
  public            System.Single                   scaleMlp  // 0x10
  public            UnityEngine.Vector3             headTrackerForward  // 0x14
  public            UnityEngine.Vector3             headTrackerUp  // 0x20
  public            UnityEngine.Vector3             handTrackerForward  // 0x2c
  public            UnityEngine.Vector3             handTrackerUp  // 0x38
  public            UnityEngine.Vector3             footTrackerForward  // 0x44
  public            UnityEngine.Vector3             footTrackerUp  // 0x50
  public            UnityEngine.Vector3             headOffset  // 0x5c
  public            UnityEngine.Vector3             handOffset  // 0x68
  public            System.Single                   footForwardOffset  // 0x74
  public            System.Single                   footInwardOffset  // 0x78
  public            System.Single                   footHeadingOffset  // 0x7c
  public            System.Single                   pelvisPositionWeight  // 0x80
  public            System.Single                   pelvisRotationWeight  // 0x84
METHODS:
  RVA=0x0AC35EFC  token=0x60006FE  System.Void .ctor()
END_CLASS

CLASS: Target
TYPE:  class
TOKEN: 0x20000DE
SIZE:  0x30
FIELDS:
  public            System.Boolean                  used  // 0x10
  public            UnityEngine.Vector3             localPosition  // 0x14
  public            UnityEngine.Quaternion          localRotation  // 0x20
METHODS:
  RVA=0x0AC367FC  token=0x6000700  System.Void .ctor(UnityEngine.Transform t)
  RVA=0x0AC36794  token=0x6000701  System.Void SetTo(UnityEngine.Transform t)
END_CLASS

CLASS: CalibrationData
TYPE:  class
TOKEN: 0x20000DD
SIZE:  0x70
FIELDS:
  public            System.Single                   scale  // 0x10
  public            RootMotion.FinalIK.VRIKCalibrator.CalibrationData.Targethead  // 0x18
  public            RootMotion.FinalIK.VRIKCalibrator.CalibrationData.TargetleftHand  // 0x20
  public            RootMotion.FinalIK.VRIKCalibrator.CalibrationData.TargetrightHand  // 0x28
  public            RootMotion.FinalIK.VRIKCalibrator.CalibrationData.Targetpelvis  // 0x30
  public            RootMotion.FinalIK.VRIKCalibrator.CalibrationData.TargetleftFoot  // 0x38
  public            RootMotion.FinalIK.VRIKCalibrator.CalibrationData.TargetrightFoot  // 0x40
  public            RootMotion.FinalIK.VRIKCalibrator.CalibrationData.TargetleftLegGoal  // 0x48
  public            RootMotion.FinalIK.VRIKCalibrator.CalibrationData.TargetrightLegGoal  // 0x50
  public            UnityEngine.Vector3             pelvisTargetRight  // 0x58
  public            System.Single                   pelvisPositionWeight  // 0x64
  public            System.Single                   pelvisRotationWeight  // 0x68
METHODS:
  RVA=0x041E1670  token=0x60006FF  System.Void .ctor()
END_CLASS

CLASS: __StaticArrayInitTypeSize=16
TYPE:  sealed struct
TOKEN: 0x20000E2
SIZE:  0x20
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x20000E1
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=16CF97ADEEDB59E05BFD73A2B4C2A8885708C4F4F70C84C64B27120E72AB733B72  // static @ 0x0
METHODS:
END_CLASS

CLASS: RootMotion.Baker
TYPE:  abstract class
TOKEN: 0x2000002
SIZE:  0x90
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Int32                    frameRate  // 0x18
  public            System.Single                   keyReductionError  // 0x1c
  public            RootMotion.Baker.Mode           mode  // 0x20
  public            UnityEngine.AnimationClip[]     animationClips  // 0x28
  public            System.String[]                 animationStates  // 0x30
  public            System.String                   saveToFolder  // 0x38
  public            System.String                   appendName  // 0x40
  public            System.String                   saveName  // 0x48
  private           System.Boolean                  <isBaking>k__BackingField  // 0x50
  private           System.Single                   <bakingProgress>k__BackingField  // 0x54
  public            UnityEngine.Animator            animator  // 0x58
  public            UnityEngine.Playables.PlayableDirectordirector  // 0x60
  public            RootMotion.Baker.BakerDelegate  OnStartClip  // 0x68
  public            RootMotion.Baker.BakerDelegate  OnUpdateClip  // 0x70
  public            System.Boolean                  inheritClipSettings  // 0x78
  public            RootMotion.Baker.ClipSettings   clipSettings  // 0x80
  private           System.Single                   <clipLength>k__BackingField  // 0x88
  protected         System.Boolean                  addLoopFrame  // 0x8c
PROPERTIES:
  isBaking  get=0x04D86800  set=0x04D86840
  bakingProgress  get=0x04D86E00  set=0x04D86E10
  clipLength  get=0x04D8E220  set=0x04D8E250
METHODS:
  RVA=0x0ABD7D74  token=0x6000001  System.Void OpenUserManual()
  RVA=0x0ABD7D40  token=0x6000002  System.Void OpenScriptReference()
  RVA=0x0ABD7DA8  token=0x6000003  System.Void SupportGroup()
  RVA=0x0ABD7D0C  token=0x6000004  System.Void ASThread()
  RVA=-1  // abstract  token=0x6000009  UnityEngine.Transform GetCharacterRoot()
  RVA=-1  // abstract  token=0x600000A  System.Void OnStartBaking()
  RVA=-1  // abstract  token=0x600000B  System.Void OnSetLoopFrame(System.Single time)
  RVA=-1  // abstract  token=0x600000C  System.Void OnSetCurves(UnityEngine.AnimationClip& clip)
  RVA=-1  // abstract  token=0x600000D  System.Void OnSetKeyframes(System.Single time, System.Boolean lastFrame)
  RVA=0x041E1670  token=0x6000010  System.Void BakeClip()
  RVA=0x041E1670  token=0x6000011  System.Void StartBaking()
  RVA=0x041E1670  token=0x6000012  System.Void StopBaking()
  RVA=0x0ABD7DDC  token=0x6000013  System.Void .ctor()
END_CLASS

CLASS: RootMotion.GenericBaker
TYPE:  class
TOKEN: 0x2000009
SIZE:  0xD0
EXTENDS: RootMotion.Baker
FIELDS:
  public            System.Boolean                  markAsLegacy  // 0x90
  public            UnityEngine.Transform           root  // 0x98
  public            UnityEngine.Transform           rootNode  // 0xa0
  public            UnityEngine.Transform[]         ignoreList  // 0xa8
  public            UnityEngine.Transform[]         bakePositionList  // 0xb0
  private           RootMotion.BakerTransform[]     children  // 0xb8
  private           RootMotion.BakerTransform       rootChild  // 0xc0
  private           System.Int32                    rootChildIndex  // 0xc8
METHODS:
  RVA=0x0ABDE448  token=0x6000019  System.Void Awake()
  RVA=0x04D85EE0  token=0x600001A  UnityEngine.Transform GetCharacterRoot()
  RVA=0x0ABDE8E8  token=0x600001B  System.Void OnStartBaking()
  RVA=0x0ABDE880  token=0x600001C  System.Void OnSetLoopFrame(System.Single time)
  RVA=0x0ABDE7B0  token=0x600001D  System.Void OnSetCurves(UnityEngine.AnimationClip& clip)
  RVA=0x0ABDE818  token=0x600001E  System.Void OnSetKeyframes(System.Single time, System.Boolean lastFrame)
  RVA=0x0ABDE710  token=0x600001F  System.Boolean IsIgnored(UnityEngine.Transform t)
  RVA=0x0ABDE670  token=0x6000020  System.Boolean BakePosition(UnityEngine.Transform t)
  RVA=0x0ABDE9DC  token=0x6000021  System.Void .ctor()
END_CLASS

CLASS: RootMotion.TQ
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x30
FIELDS:
  public            UnityEngine.Vector3             t  // 0x10
  public            UnityEngine.Quaternion          q  // 0x1c
METHODS:
  RVA=0x041E1670  token=0x6000022  System.Void .ctor()
  RVA=0x04DA9470  token=0x6000023  System.Void .ctor(UnityEngine.Vector3 translation, UnityEngine.Quaternion rotation)
END_CLASS

CLASS: RootMotion.AvatarUtility
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ABD4100  token=0x6000024  UnityEngine.Quaternion GetPostRotation(UnityEngine.Avatar avatar, UnityEngine.AvatarIKGoal avatarIKGoal)
  RVA=0x0ABD3B40  token=0x6000025  RootMotion.TQ GetIKGoalTQ(UnityEngine.Avatar avatar, System.Single humanScale, UnityEngine.AvatarIKGoal avatarIKGoal, RootMotion.TQ bodyPositionRotation, RootMotion.TQ boneTQ)
  RVA=0x0ABD4480  token=0x6000026  RootMotion.TQ WorldSpaceIKGoalToBone(RootMotion.TQ goalTQ, UnityEngine.Avatar avatar, UnityEngine.AvatarIKGoal avatarIKGoal)
  RVA=0x0ABD42FC  token=0x6000027  RootMotion.TQ GetWorldSpaceIKGoal(RootMotion.BakerHumanoidQT ikQT, RootMotion.BakerHumanoidQT rootQT, System.Single time, System.Single humanScale)
  RVA=0x0ABD444C  token=0x6000028  UnityEngine.HumanBodyBones HumanIDFromAvatarIKGoal(UnityEngine.AvatarIKGoal avatarIKGoal)
  RVA=0x041E1670  token=0x6000029  System.Void .ctor()
END_CLASS

CLASS: RootMotion.BakerUtilities
TYPE:  static class
TOKEN: 0x200000C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ABD7B7C  token=0x600002A  System.Void ReduceKeyframes(UnityEngine.AnimationCurve curve, System.Single maxError)
  RVA=0x0ABD784C  token=0x600002B  UnityEngine.Keyframe[] GetReducedKeyframes(UnityEngine.AnimationCurve curve, System.Single maxError)
  RVA=0x0ABD7BBC  token=0x600002C  System.Void SetLoopFrame(System.Single time, UnityEngine.AnimationCurve curve)
  RVA=0x041E1670  token=0x600002D  System.Void SetTangentMode(UnityEngine.AnimationCurve curve)
  RVA=0x0ABD76B4  token=0x600002E  UnityEngine.Quaternion EnsureQuaternionContinuity(UnityEngine.Quaternion lastQ, UnityEngine.Quaternion q)
END_CLASS

CLASS: RootMotion.BakerHumanoidQT
TYPE:  class
TOKEN: 0x200000D
SIZE:  0xA0
FIELDS:
  private           UnityEngine.Transform           transform  // 0x10
  private           System.String                   Qx  // 0x18
  private           System.String                   Qy  // 0x20
  private           System.String                   Qz  // 0x28
  private           System.String                   Qw  // 0x30
  private           System.String                   Tx  // 0x38
  private           System.String                   Ty  // 0x40
  private           System.String                   Tz  // 0x48
  public            UnityEngine.AnimationCurve      rotX  // 0x50
  public            UnityEngine.AnimationCurve      rotY  // 0x58
  public            UnityEngine.AnimationCurve      rotZ  // 0x60
  public            UnityEngine.AnimationCurve      rotW  // 0x68
  public            UnityEngine.AnimationCurve      posX  // 0x70
  public            UnityEngine.AnimationCurve      posY  // 0x78
  public            UnityEngine.AnimationCurve      posZ  // 0x80
  private           UnityEngine.AvatarIKGoal        goal  // 0x88
  private           UnityEngine.Quaternion          lastQ  // 0x8c
  private           System.Boolean                  lastQSet  // 0x9c
METHODS:
  RVA=0x0ABD5C28  token=0x600002F  System.Void .ctor(System.String name)
  RVA=0x0ABD5D8C  token=0x6000030  System.Void .ctor(UnityEngine.Transform transform, UnityEngine.AvatarIKGoal goal, System.String name)
  RVA=0x0ABD4F50  token=0x6000031  UnityEngine.Quaternion EvaluateRotation(System.Single time)
  RVA=0x0ABD4EB4  token=0x6000032  UnityEngine.Vector3 EvaluatePosition(System.Single time)
  RVA=0x0ABD5018  token=0x6000033  RootMotion.TQ Evaluate(System.Single time)
  RVA=0x041E1670  token=0x6000034  System.Void GetCurvesFromClip(UnityEngine.AnimationClip clip, UnityEngine.Animator animator)
  RVA=0x0ABD5230  token=0x6000035  System.Void Reset()
  RVA=0x0ABD576C  token=0x6000036  System.Void SetIKKeyframes(System.Single time, UnityEngine.Avatar avatar, UnityEngine.Transform root, System.Single humanScale, UnityEngine.Vector3 bodyPosition, UnityEngine.Quaternion bodyRotation)
  RVA=0x0ABD5AAC  token=0x6000037  System.Void SetKeyframes(System.Single time, UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
  RVA=0x0ABD511C  token=0x6000038  System.Void MoveLastKeyframes(System.Single time)
  RVA=0x0ABD5BA0  token=0x6000039  System.Void SetLoopFrame(System.Single time)
  RVA=0x041E1670  token=0x600003A  System.Void SetRootLoopFrame(System.Single time)
  RVA=0x0ABD50B8  token=0x600003B  System.Void MoveLastKeyframe(System.Single time, UnityEngine.AnimationCurve curve)
  RVA=0x0ABD51B4  token=0x600003C  System.Void MultiplyLength(UnityEngine.AnimationCurve curve, System.Single mlp)
  RVA=0x0ABD53D0  token=0x600003D  System.Void SetCurves(UnityEngine.AnimationClip& clip, System.Single maxError, System.Single lengthMlp)
END_CLASS

CLASS: RootMotion.BakerMuscle
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x28
FIELDS:
  public            UnityEngine.AnimationCurve      curve  // 0x10
  private           System.Int32                    muscleIndex  // 0x18
  private           System.String                   propertyName  // 0x20
METHODS:
  RVA=0x0ABD6A68  token=0x600003E  System.Void .ctor(System.Int32 muscleIndex)
  RVA=0x0ABD5F1C  token=0x600003F  System.String MuscleNameToPropertyName(System.String n)
  RVA=0x0ABD51B4  token=0x6000040  System.Void MultiplyLength(UnityEngine.AnimationCurve curve, System.Single mlp)
  RVA=0x0ABD691C  token=0x6000041  System.Void SetCurves(UnityEngine.AnimationClip& clip, System.Single maxError, System.Single lengthMlp)
  RVA=0x0ABD68B8  token=0x6000042  System.Void Reset()
  RVA=0x0ABD6A18  token=0x6000043  System.Void SetKeyframe(System.Single time, System.Single[] muscles)
  RVA=0x0ABD6A58  token=0x6000044  System.Void SetLoopFrame(System.Single time)
END_CLASS

CLASS: RootMotion.BakerTransform
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x80
FIELDS:
  public            UnityEngine.Transform           transform  // 0x10
  public            UnityEngine.AnimationCurve      posX  // 0x18
  public            UnityEngine.AnimationCurve      posY  // 0x20
  public            UnityEngine.AnimationCurve      posZ  // 0x28
  public            UnityEngine.AnimationCurve      rotX  // 0x30
  public            UnityEngine.AnimationCurve      rotY  // 0x38
  public            UnityEngine.AnimationCurve      rotZ  // 0x40
  public            UnityEngine.AnimationCurve      rotW  // 0x48
  private           System.String                   relativePath  // 0x50
  private           System.Boolean                  recordPosition  // 0x58
  private           UnityEngine.Vector3             relativePosition  // 0x5c
  private           System.Boolean                  isRootNode  // 0x68
  private           UnityEngine.Quaternion          relativeRotation  // 0x6c
METHODS:
  RVA=0x0ABD762C  token=0x6000045  System.Void .ctor(UnityEngine.Transform transform, UnityEngine.Transform root, System.Boolean recordPosition, System.Boolean isRootNode)
  RVA=0x04DA8EF0  token=0x6000046  System.Void SetRelativeSpace(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0ABD7150  token=0x6000047  System.Void SetCurves(UnityEngine.AnimationClip& clip)
  RVA=0x0ABD6CA8  token=0x6000048  System.Void AddRootMotionCurves(UnityEngine.AnimationClip& clip)
  RVA=0x0ABD6FD8  token=0x6000049  System.Void Reset()
  RVA=0x0ABD6F54  token=0x600004A  System.Void ReduceKeyframes(System.Single maxError)
  RVA=0x0ABD7420  token=0x600004B  System.Void SetKeyframes(System.Single time)
  RVA=0x0ABD6ACC  token=0x600004C  System.Void AddLoopFrame(System.Single time)
END_CLASS

CLASS: RootMotion.HumanoidBaker
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x138
EXTENDS: RootMotion.Baker
FIELDS:
  public            System.Boolean                  bakeHandIK  // 0x90
  public            System.Single                   IKKeyReductionError  // 0x94
  public            System.Int32                    muscleFrameRateDiv  // 0x98
  private           RootMotion.BakerMuscle[]        bakerMuscles  // 0xa0
  private           RootMotion.BakerHumanoidQT      rootQT  // 0xa8
  private           RootMotion.BakerHumanoidQT      leftFootQT  // 0xb0
  private           RootMotion.BakerHumanoidQT      rightFootQT  // 0xb8
  private           RootMotion.BakerHumanoidQT      leftHandQT  // 0xc0
  private           RootMotion.BakerHumanoidQT      rightHandQT  // 0xc8
  private           System.Single[]                 muscles  // 0xd0
  private           UnityEngine.HumanPose           pose  // 0xd8
  private           UnityEngine.HumanPoseHandler    handler  // 0x100
  private           UnityEngine.Vector3             bodyPosition  // 0x108
  private           UnityEngine.Quaternion          bodyRotation  // 0x114
  private           System.Int32                    mN  // 0x124
  private           UnityEngine.Quaternion          lastBodyRotation  // 0x128
METHODS:
  RVA=0x0ABDF380  token=0x600004D  System.Void Awake()
  RVA=0x0ABDF87C  token=0x600004E  UnityEngine.Transform GetCharacterRoot()
  RVA=0x0ABDFF3C  token=0x600004F  System.Void OnStartBaking()
  RVA=0x0ABDFE5C  token=0x6000050  System.Void OnSetLoopFrame(System.Single time)
  RVA=0x0ABDF89C  token=0x6000051  System.Void OnSetCurves(UnityEngine.AnimationClip& clip)
  RVA=0x0ABDFAA8  token=0x6000052  System.Void OnSetKeyframes(System.Single time, System.Boolean lastFrame)
  RVA=0x0ABE0020  token=0x6000053  System.Void UpdateHumanPose()
  RVA=0x0ABE010C  token=0x6000054  System.Void .ctor()
END_CLASS

CLASS: RootMotion.Axis
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.Axis                 X  // const
  public    static  RootMotion.Axis                 Y  // const
  public    static  RootMotion.Axis                 Z  // const
METHODS:
END_CLASS

CLASS: RootMotion.AxisTools
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ABD4DF4  token=0x6000055  UnityEngine.Vector3 ToVector3(RootMotion.Axis axis)
  RVA=0x0ABD4DB4  token=0x6000056  RootMotion.Axis ToAxis(UnityEngine.Vector3 v)
  RVA=0x0ABD49B8  token=0x6000057  RootMotion.Axis GetAxisToPoint(UnityEngine.Transform t, UnityEngine.Vector3 worldPosition)
  RVA=0x0ABD48CC  token=0x6000058  RootMotion.Axis GetAxisToDirection(UnityEngine.Transform t, UnityEngine.Vector3 direction)
  RVA=0x0ABD4D04  token=0x6000059  UnityEngine.Vector3 GetAxisVectorToPoint(UnityEngine.Transform t, UnityEngine.Vector3 worldPosition)
  RVA=0x0ABD4AA4  token=0x600005A  UnityEngine.Vector3 GetAxisVectorToDirection(UnityEngine.Transform t, UnityEngine.Vector3 direction)
  RVA=0x0ABD4B10  token=0x600005B  UnityEngine.Vector3 GetAxisVectorToDirection(UnityEngine.Quaternion r, UnityEngine.Vector3 direction)
  RVA=0x041E1670  token=0x600005C  System.Void .ctor()
END_CLASS

CLASS: RootMotion.BipedLimbOrientations
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x30
FIELDS:
  public            RootMotion.BipedLimbOrientations.LimbOrientationleftArm  // 0x10
  public            RootMotion.BipedLimbOrientations.LimbOrientationrightArm  // 0x18
  public            RootMotion.BipedLimbOrientations.LimbOrientationleftLeg  // 0x20
  public            RootMotion.BipedLimbOrientations.LimbOrientationrightLeg  // 0x28
PROPERTIES:
  UMA  get=0x0ABD88A8
  MaxBiped  get=0x0ABD8618
METHODS:
  RVA=0x032FABF0  token=0x600005D  System.Void .ctor(RootMotion.BipedLimbOrientations.LimbOrientation leftArm, RootMotion.BipedLimbOrientations.LimbOrientation rightArm, RootMotion.BipedLimbOrientations.LimbOrientation leftLeg, RootMotion.BipedLimbOrientations.LimbOrientation rightLeg)
END_CLASS

CLASS: RootMotion.BipedNaming
TYPE:  static class
TOKEN: 0x2000015
SIZE:  0x10
FIELDS:
  public    static  System.String[]                 typeLeft  // static @ 0x0
  public    static  System.String[]                 typeRight  // static @ 0x8
  public    static  System.String[]                 typeSpine  // static @ 0x10
  public    static  System.String[]                 typeHead  // static @ 0x18
  public    static  System.String[]                 typeArm  // static @ 0x20
  public    static  System.String[]                 typeLeg  // static @ 0x28
  public    static  System.String[]                 typeTail  // static @ 0x30
  public    static  System.String[]                 typeEye  // static @ 0x38
  public    static  System.String[]                 typeExclude  // static @ 0x40
  public    static  System.String[]                 typeExcludeSpine  // static @ 0x48
  public    static  System.String[]                 typeExcludeHead  // static @ 0x50
  public    static  System.String[]                 typeExcludeArm  // static @ 0x58
  public    static  System.String[]                 typeExcludeLeg  // static @ 0x60
  public    static  System.String[]                 typeExcludeTail  // static @ 0x68
  public    static  System.String[]                 typeExcludeEye  // static @ 0x70
  public    static  System.String[]                 pelvis  // static @ 0x78
  public    static  System.String[]                 hand  // static @ 0x80
  public    static  System.String[]                 foot  // static @ 0x88
METHODS:
  RVA=0x0ABD8ECC  token=0x6000061  UnityEngine.Transform[] GetBonesOfType(RootMotion.BipedNaming.BoneType boneType, UnityEngine.Transform[] bones)
  RVA=0x0ABD8D14  token=0x6000062  UnityEngine.Transform[] GetBonesOfSide(RootMotion.BipedNaming.BoneSide boneSide, UnityEngine.Transform[] bones)
  RVA=0x0ABD8E64  token=0x6000063  UnityEngine.Transform[] GetBonesOfTypeAndSide(RootMotion.BipedNaming.BoneType boneType, RootMotion.BipedNaming.BoneSide boneSide, UnityEngine.Transform[] bones)
  RVA=0x0ABD901C  token=0x6000064  UnityEngine.Transform GetFirstBoneOfTypeAndSide(RootMotion.BipedNaming.BoneType boneType, RootMotion.BipedNaming.BoneSide boneSide, UnityEngine.Transform[] bones)
  RVA=0x0ABD90A0  token=0x6000065  UnityEngine.Transform GetNamingMatch(UnityEngine.Transform[] transforms, System.String[][] namings)
  RVA=0x0ABD8BA4  token=0x6000066  RootMotion.BipedNaming.BoneType GetBoneType(System.String boneName)
  RVA=0x0ABD8B38  token=0x6000067  RootMotion.BipedNaming.BoneSide GetBoneSide(System.String boneName)
  RVA=0x0ABD8C9C  token=0x6000068  UnityEngine.Transform GetBone(UnityEngine.Transform[] transforms, RootMotion.BipedNaming.BoneType boneType, RootMotion.BipedNaming.BoneSide boneSide, System.String[][] namings)
  RVA=0x0ABDA68C  token=0x6000069  System.Boolean isLeft(System.String boneName)
  RVA=0x0ABDA7D4  token=0x600006A  System.Boolean isRight(System.String boneName)
  RVA=0x0ABDA894  token=0x600006B  System.Boolean isSpine(System.String boneName)
  RVA=0x0ABDA604  token=0x600006C  System.Boolean isHead(System.String boneName)
  RVA=0x0ABDA4F4  token=0x600006D  System.Boolean isArm(System.String boneName)
  RVA=0x0ABDA74C  token=0x600006E  System.Boolean isLeg(System.String boneName)
  RVA=0x0ABDA91C  token=0x600006F  System.Boolean isTail(System.String boneName)
  RVA=0x0ABDA57C  token=0x6000070  System.Boolean isEye(System.String boneName)
  RVA=0x0ABDA9A4  token=0x6000071  System.Boolean isTypeExclude(System.String boneName)
  RVA=0x0ABDAAEC  token=0x6000072  System.Boolean matchesNaming(System.String boneName, System.String[] namingConvention)
  RVA=0x0ABDA430  token=0x6000073  System.Boolean excludesNaming(System.String boneName, System.String[] namingConvention)
  RVA=0x0ABDAA54  token=0x6000074  System.Boolean matchesLastLetter(System.String boneName, System.String[] namingConvention)
  RVA=0x0ABD9180  token=0x6000075  System.Boolean LastLetterIs(System.String boneName, System.String letter)
  RVA=0x0ABDA498  token=0x6000076  System.String firstLetter(System.String boneName)
  RVA=0x0ABDA9F8  token=0x6000077  System.String lastLetter(System.String boneName)
  RVA=0x0ABD91B8  token=0x6000078  System.Void .cctor()
END_CLASS

CLASS: RootMotion.BipedReferences
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x98
FIELDS:
  public            UnityEngine.Transform           root  // 0x10
  public            UnityEngine.Transform           pelvis  // 0x18
  public            UnityEngine.Transform           leftThigh  // 0x20
  public            UnityEngine.Transform           leftCalf  // 0x28
  public            UnityEngine.Transform           leftFoot  // 0x30
  public            UnityEngine.Transform           rightThigh  // 0x38
  public            UnityEngine.Transform           rightCalf  // 0x40
  public            UnityEngine.Transform           rightFoot  // 0x48
  public            UnityEngine.Transform           leftUpperArm  // 0x50
  public            UnityEngine.Transform           leftForearm  // 0x58
  public            UnityEngine.Transform           leftHand  // 0x60
  public            UnityEngine.Transform           rightUpperArm  // 0x68
  public            UnityEngine.Transform           rightForearm  // 0x70
  public            UnityEngine.Transform           rightHand  // 0x78
  public            UnityEngine.Transform           head  // 0x80
  public            UnityEngine.Transform[]         spine  // 0x88
  public            UnityEngine.Transform[]         eyes  // 0x90
PROPERTIES:
  isFilled  get=0x039DCB00
  isEmpty  get=0x0ABDCCD0
METHODS:
  RVA=0x0ABDC3B0  token=0x600007B  System.Boolean IsEmpty(System.Boolean includeRoot)
  RVA=0x0ABDB50C  token=0x600007C  System.Boolean Contains(UnityEngine.Transform t, System.Boolean ignoreRoot)
  RVA=0x0ABDB314  token=0x600007D  System.Boolean AutoDetectReferences(RootMotion.BipedReferences& references, UnityEngine.Transform root, RootMotion.BipedReferences.AutoDetectParams autoDetectParams)
  RVA=0x0ABDB958  token=0x600007E  System.Void DetectReferencesByNaming(RootMotion.BipedReferences& references, UnityEngine.Transform root, RootMotion.BipedReferences.AutoDetectParams autoDetectParams)
  RVA=0x0ABDAE74  token=0x600007F  System.Void AssignHumanoidReferences(RootMotion.BipedReferences& references, UnityEngine.Animator animator, RootMotion.BipedReferences.AutoDetectParams autoDetectParams)
  RVA=0x039DD2A0  token=0x6000080  System.Boolean SetupError(RootMotion.BipedReferences references, System.String& errorMessage)
  RVA=0x0ABDCC04  token=0x6000081  System.Boolean SetupWarning(RootMotion.BipedReferences references, System.String& warningMessage)
  RVA=0x0ABDC6BC  token=0x6000082  System.Boolean IsNeckBone(UnityEngine.Transform bone, UnityEngine.Transform leftUpperArm)
  RVA=0x0ABDAB9C  token=0x6000083  System.Boolean AddBoneToEyes(UnityEngine.Transform bone, RootMotion.BipedReferences& references, RootMotion.BipedReferences.AutoDetectParams autoDetectParams)
  RVA=0x0ABDAD2C  token=0x6000084  System.Boolean AddBoneToSpine(UnityEngine.Transform bone, RootMotion.BipedReferences& references, RootMotion.BipedReferences.AutoDetectParams autoDetectParams)
  RVA=0x0ABDB838  token=0x6000085  System.Void DetectLimb(RootMotion.BipedNaming.BoneType boneType, RootMotion.BipedNaming.BoneSide boneSide, UnityEngine.Transform& firstBone, UnityEngine.Transform& secondBone, UnityEngine.Transform& lastBone, UnityEngine.Transform[] transforms)
  RVA=0x0ABDAC78  token=0x6000086  System.Void AddBoneToHierarchy(UnityEngine.Transform[]& bones, UnityEngine.Transform transform)
  RVA=0x035B1DE0  token=0x6000087  System.Boolean LimbError(UnityEngine.Transform bone1, UnityEngine.Transform bone2, UnityEngine.Transform bone3, System.String& errorMessage)
  RVA=0x0ABDC778  token=0x6000088  System.Boolean LimbWarning(UnityEngine.Transform bone1, UnityEngine.Transform bone2, UnityEngine.Transform bone3, System.String& warningMessage)
  RVA=0x039E01C0  token=0x6000089  System.Boolean SpineError(RootMotion.BipedReferences references, System.String& errorMessage)
  RVA=0x012081B0  token=0x600008A  System.Boolean SpineWarning(RootMotion.BipedReferences references, System.String& warningMessage)
  RVA=0x039DD3C0  token=0x600008B  System.Boolean EyesError(RootMotion.BipedReferences references, System.String& errorMessage)
  RVA=0x012081B0  token=0x600008C  System.Boolean EyesWarning(RootMotion.BipedReferences references, System.String& warningMessage)
  RVA=0x0ABDCA0C  token=0x600008D  System.Boolean RootHeightWarning(RootMotion.BipedReferences references, System.String& warningMessage)
  RVA=0x0ABDC0EC  token=0x600008E  System.Boolean FacingAxisWarning(RootMotion.BipedReferences references, System.String& warningMessage)
  RVA=0x0ABDC308  token=0x600008F  System.Single GetVerticalOffset(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Quaternion rotation)
  RVA=0x039DD730  token=0x6000090  System.Void .ctor()
END_CLASS

CLASS: RootMotion.Comments
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.String                   text  // 0x18
METHODS:
  RVA=0x05393520  token=0x6000093  System.Void .ctor()
END_CLASS

CLASS: RootMotion.DemoGUIMessage
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.String                   text  // 0x18
  public            UnityEngine.Color               color  // 0x20
METHODS:
  RVA=0x0ABDD268  token=0x6000094  System.Void OnGUI()
  RVA=0x0ABDD30C  token=0x6000095  System.Void .ctor()
END_CLASS

CLASS: RootMotion.Hierarchy
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x039DFDE0  token=0x6000096  System.Boolean HierarchyIsValid(UnityEngine.Transform[] bones)
  RVA=0x035B1D00  token=0x6000097  UnityEngine.Object ContainsDuplicate(UnityEngine.Object[] objects)
  RVA=0x039DFE50  token=0x6000098  System.Boolean IsAncestor(UnityEngine.Transform transform, UnityEngine.Transform ancestor)
  RVA=0x0ABDEC94  token=0x6000099  System.Boolean ContainsChild(UnityEngine.Transform transform, UnityEngine.Transform child)
  RVA=0x0ABDEA38  token=0x600009A  System.Void AddAncestors(UnityEngine.Transform transform, UnityEngine.Transform blocker, UnityEngine.Transform[]& array)
  RVA=0x0ABDED6C  token=0x600009B  UnityEngine.Transform GetAncestor(UnityEngine.Transform transform, System.Int32 minChildCount)
  RVA=0x0ABDEF90  token=0x600009C  UnityEngine.Transform GetFirstCommonAncestor(UnityEngine.Transform t1, UnityEngine.Transform t2)
  RVA=0x0ABDF0BC  token=0x600009D  UnityEngine.Transform GetFirstCommonAncestor(UnityEngine.Transform[] transforms)
  RVA=0x0ABDEE40  token=0x600009E  UnityEngine.Transform GetFirstCommonAncestorRecursive(UnityEngine.Transform transform, UnityEngine.Transform[] transforms)
  RVA=0x0ABDF1DC  token=0x600009F  System.Boolean IsCommonAncestor(UnityEngine.Transform transform, UnityEngine.Transform[] transforms)
  RVA=0x041E1670  token=0x60000A0  System.Void .ctor()
END_CLASS

CLASS: RootMotion.InspectorComment
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x20
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   color  // 0x18
METHODS:
  RVA=0x0ABE0214  token=0x60000A1  System.Void .ctor(System.String name)
  RVA=0x0ABE01A0  token=0x60000A2  System.Void .ctor(System.String name, System.String color)
END_CLASS

CLASS: RootMotion.InterpolationMode
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.InterpolationMode    None  // const
  public    static  RootMotion.InterpolationMode    InOutCubic  // const
  public    static  RootMotion.InterpolationMode    InOutQuintic  // const
  public    static  RootMotion.InterpolationMode    InOutSine  // const
  public    static  RootMotion.InterpolationMode    InQuintic  // const
  public    static  RootMotion.InterpolationMode    InQuartic  // const
  public    static  RootMotion.InterpolationMode    InCubic  // const
  public    static  RootMotion.InterpolationMode    InQuadratic  // const
  public    static  RootMotion.InterpolationMode    InElastic  // const
  public    static  RootMotion.InterpolationMode    InElasticSmall  // const
  public    static  RootMotion.InterpolationMode    InElasticBig  // const
  public    static  RootMotion.InterpolationMode    InSine  // const
  public    static  RootMotion.InterpolationMode    InBack  // const
  public    static  RootMotion.InterpolationMode    OutQuintic  // const
  public    static  RootMotion.InterpolationMode    OutQuartic  // const
  public    static  RootMotion.InterpolationMode    OutCubic  // const
  public    static  RootMotion.InterpolationMode    OutInCubic  // const
  public    static  RootMotion.InterpolationMode    OutInQuartic  // const
  public    static  RootMotion.InterpolationMode    OutElastic  // const
  public    static  RootMotion.InterpolationMode    OutElasticSmall  // const
  public    static  RootMotion.InterpolationMode    OutElasticBig  // const
  public    static  RootMotion.InterpolationMode    OutSine  // const
  public    static  RootMotion.InterpolationMode    OutBack  // const
  public    static  RootMotion.InterpolationMode    OutBackCubic  // const
  public    static  RootMotion.InterpolationMode    OutBackQuartic  // const
  public    static  RootMotion.InterpolationMode    BackInCubic  // const
  public    static  RootMotion.InterpolationMode    BackInQuartic  // const
METHODS:
END_CLASS

CLASS: RootMotion.Interp
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ABE0280  token=0x60000A3  System.Single Float(System.Single t, RootMotion.InterpolationMode mode)
  RVA=0x0ABE0890  token=0x60000A4  UnityEngine.Vector3 V3(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, System.Single t, RootMotion.InterpolationMode mode)
  RVA=0x031CD6D0  token=0x60000A5  System.Single LerpValue(System.Single value, System.Single target, System.Single increaseSpeed, System.Single decreaseSpeed)
  RVA=0x04DA9140  token=0x60000A6  System.Single None(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA9070  token=0x60000A7  System.Single InOutCubic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA90A0  token=0x60000A8  System.Single InOutQuintic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA9120  token=0x60000A9  System.Single InQuintic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA9100  token=0x60000AA  System.Single InQuartic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA8F90  token=0x60000AB  System.Single InCubic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA90F0  token=0x60000AC  System.Single InQuadratic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA9400  token=0x60000AD  System.Single OutQuintic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA93A0  token=0x60000AE  System.Single OutQuartic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA91E0  token=0x60000AF  System.Single OutCubic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA9320  token=0x60000B0  System.Single OutInCubic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA9360  token=0x60000B1  System.Single OutInQuartic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA8F20  token=0x60000B2  System.Single BackInCubic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA8F50  token=0x60000B3  System.Single BackInQuartic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA9150  token=0x60000B4  System.Single OutBackCubic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA9190  token=0x60000B5  System.Single OutBackQuartic(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA92A0  token=0x60000B6  System.Single OutElasticSmall(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA9220  token=0x60000B7  System.Single OutElasticBig(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA9010  token=0x60000B8  System.Single InElasticSmall(System.Single t, System.Single b, System.Single c)
  RVA=0x04DA8FB0  token=0x60000B9  System.Single InElasticBig(System.Single t, System.Single b, System.Single c)
  RVA=0x0ABE0714  token=0x60000BA  System.Single InSine(System.Single t, System.Single b, System.Single c)
  RVA=0x0ABE0854  token=0x60000BB  System.Single OutSine(System.Single t, System.Single b, System.Single c)
  RVA=0x0ABE06C0  token=0x60000BC  System.Single InOutSine(System.Single t, System.Single b, System.Single c)
  RVA=0x0ABE05F8  token=0x60000BD  System.Single InElastic(System.Single t, System.Single b, System.Single c)
  RVA=0x0ABE079C  token=0x60000BE  System.Single OutElastic(System.Single t, System.Single b, System.Single c)
  RVA=0x0ABE05D0  token=0x60000BF  System.Single InBack(System.Single t, System.Single b, System.Single c)
  RVA=0x0ABE0760  token=0x60000C0  System.Single OutBack(System.Single t, System.Single b, System.Single c)
  RVA=0x041E1670  token=0x60000C1  System.Void .ctor()
END_CLASS

CLASS: RootMotion.LayerMaskExtensions
TYPE:  static class
TOKEN: 0x2000020
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ABE0A40  token=0x60000C2  System.Boolean Contains(UnityEngine.LayerMask mask, System.Int32 layer)
  RVA=0x0A994A28  token=0x60000C3  UnityEngine.LayerMask Create(System.String[] layerNames)
  RVA=0x0A994A30  token=0x60000C4  UnityEngine.LayerMask Create(System.Int32[] layerNumbers)
  RVA=0x0A994A38  token=0x60000C5  UnityEngine.LayerMask NamesToMask(System.String[] layerNames)
  RVA=0x0A994A94  token=0x60000C6  UnityEngine.LayerMask LayerNumbersToMask(System.Int32[] layerNumbers)
  RVA=0x04DA68B0  token=0x60000C7  UnityEngine.LayerMask Inverse(UnityEngine.LayerMask original)
  RVA=0x0A994948  token=0x60000C8  UnityEngine.LayerMask AddToMask(UnityEngine.LayerMask original, System.String[] layerNames)
  RVA=0x0A994C78  token=0x60000C9  UnityEngine.LayerMask RemoveFromMask(UnityEngine.LayerMask original, System.String[] layerNames)
  RVA=0x0ABE0A50  token=0x60000CA  System.String[] MaskToNames(UnityEngine.LayerMask original)
  RVA=0x0ABE0B2C  token=0x60000CB  System.Int32[] MaskToNumbers(UnityEngine.LayerMask original)
  RVA=0x0ABE0BF4  token=0x60000CC  System.String MaskToString(UnityEngine.LayerMask original)
  RVA=0x0ABE0C30  token=0x60000CD  System.String MaskToString(UnityEngine.LayerMask original, System.String delimiter)
END_CLASS

CLASS: RootMotion.LazySingleton`1
TYPE:  abstract class
TOKEN: 0x2000021
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  T                               sInstance  // static @ 0x0
PROPERTIES:
  hasInstance  get=-1  // not resolved
  instance  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000D0  System.Void Awake()
  RVA=-1  // not resolved  token=0x60000D1  System.Void .ctor()
END_CLASS

CLASS: RootMotion.ShowIfMode
TYPE:  sealed struct
TOKEN: 0x2000022
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.ShowIfMode           Disabled  // const
  public    static  RootMotion.ShowIfMode           Hidden  // const
METHODS:
END_CLASS

CLASS: RootMotion.ShowIfAttribute
TYPE:  class
TOKEN: 0x2000023
SIZE:  0x30
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  private           System.String                   <propName>k__BackingField  // 0x10
  private           System.Object                   <propValue>k__BackingField  // 0x18
  private           System.Object                   <otherPropValue>k__BackingField  // 0x20
  private           System.Boolean                  <indent>k__BackingField  // 0x28
  private           RootMotion.ShowIfMode           <mode>k__BackingField  // 0x2c
PROPERTIES:
  propName  get=0x02B2ECC0  set=0x053908C0
  propValue  get=0x0385B100  set=0x05392C40
  otherPropValue  get=0x04D862C0  set=0x05390F40
  indent  get=0x011F33C0  set=0x011F33D0
  mode  get=0x04D86710  set=0x04D86720
METHODS:
  RVA=0x0ABE2800  token=0x60000DC  System.Void .ctor(System.String propertyName, System.Object propertyValue, System.Object otherPropertyValue, System.Boolean indent, RootMotion.ShowIfMode mode)
END_CLASS

CLASS: RootMotion.ShowRangeIfAttribute
TYPE:  class
TOKEN: 0x2000024
SIZE:  0x38
EXTENDS: RootMotion.ShowIfAttribute
FIELDS:
  private           System.Single                   <min>k__BackingField  // 0x30
  private           System.Single                   <max>k__BackingField  // 0x34
PROPERTIES:
  min  get=0x04D86E20  set=0x04D86E30
  max  get=0x04D8E050  set=0x04D8E060
METHODS:
  RVA=0x0ABE29AC  token=0x60000E1  System.Void .ctor(System.Single min, System.Single max, System.String propertyName, System.Object propertyValue, System.Object otherPropertyValue, System.Boolean indent, RootMotion.ShowIfMode mode)
END_CLASS

CLASS: RootMotion.ShowLargeHeaderIf
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x40
EXTENDS: RootMotion.ShowIfAttribute
FIELDS:
  public            System.String                   name  // 0x30
  public            System.String                   color  // 0x38
METHODS:
  RVA=0x0ABE28F8  token=0x60000E2  System.Void .ctor(System.String name, System.String propertyName, System.Object propertyValue, System.Object otherPropertyValue, System.Boolean indent, RootMotion.ShowIfMode mode)
  RVA=0x0ABE2848  token=0x60000E3  System.Void .ctor(System.String name, System.String color, System.String propertyName, System.Object propertyValue, System.Object otherPropertyValue, System.Boolean indent, RootMotion.ShowIfMode mode)
END_CLASS

CLASS: RootMotion.LargeHeader
TYPE:  class
TOKEN: 0x2000026
SIZE:  0x20
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   color  // 0x18
METHODS:
  RVA=0x0ABE09D4  token=0x60000E4  System.Void .ctor(System.String name)
  RVA=0x0ABE0960  token=0x60000E5  System.Void .ctor(System.String name, System.String color)
END_CLASS

CLASS: RootMotion.QuaTools
TYPE:  static class
TOKEN: 0x2000027
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ABE20D4  token=0x60000E6  System.Single GetYaw(UnityEngine.Quaternion space, UnityEngine.Vector3 forward)
  RVA=0x0ABE2054  token=0x60000E7  System.Single GetPitch(UnityEngine.Quaternion space, UnityEngine.Vector3 forward)
  RVA=0x0ABE1DF0  token=0x60000E8  System.Single GetBank(UnityEngine.Quaternion space, UnityEngine.Vector3 forward, UnityEngine.Vector3 up)
  RVA=0x0ABE214C  token=0x60000E9  System.Single GetYaw(UnityEngine.Quaternion space, UnityEngine.Quaternion rotation)
  RVA=0x0ABE1F98  token=0x60000EA  System.Single GetPitch(UnityEngine.Quaternion space, UnityEngine.Quaternion rotation)
  RVA=0x0ABE1BF4  token=0x60000EB  System.Single GetBank(UnityEngine.Quaternion space, UnityEngine.Quaternion rotation)
  RVA=0x0ABE2210  token=0x60000EC  UnityEngine.Quaternion Lerp(UnityEngine.Quaternion fromRotation, UnityEngine.Quaternion toRotation, System.Single weight)
  RVA=0x0ABE2698  token=0x60000ED  UnityEngine.Quaternion Slerp(UnityEngine.Quaternion fromRotation, UnityEngine.Quaternion toRotation, System.Single weight)
  RVA=0x0ABE226C  token=0x60000EE  UnityEngine.Quaternion LinearBlend(UnityEngine.Quaternion q, System.Single weight)
  RVA=0x0ABE26F4  token=0x60000EF  UnityEngine.Quaternion SphericalBlend(UnityEngine.Quaternion q, System.Single weight)
  RVA=0x0ABE18A4  token=0x60000F0  UnityEngine.Quaternion FromToAroundAxis(UnityEngine.Vector3 fromDirection, UnityEngine.Vector3 toDirection, UnityEngine.Vector3 axis)
  RVA=0x04128B30  token=0x60000F1  UnityEngine.Quaternion RotationToLocalSpace(UnityEngine.Quaternion space, UnityEngine.Quaternion rotation)
  RVA=0x0ABE19B8  token=0x60000F2  UnityEngine.Quaternion FromToRotation(UnityEngine.Quaternion from, UnityEngine.Quaternion to)
  RVA=0x0ABE1A5C  token=0x60000F3  UnityEngine.Vector3 GetAxis(UnityEngine.Vector3 v)
  RVA=0x0ABE1720  token=0x60000F4  UnityEngine.Quaternion ClampRotation(UnityEngine.Quaternion rotation, System.Single clampWeight, System.Int32 clampSmoothing)
  RVA=0x0ABE1634  token=0x60000F5  System.Single ClampAngle(System.Single angle, System.Single clampWeight, System.Int32 clampSmoothing)
  RVA=0x0ABE22E8  token=0x60000F6  UnityEngine.Quaternion MatchRotation(UnityEngine.Quaternion targetRotation, UnityEngine.Vector3 targetAxis1, UnityEngine.Vector3 targetAxis2, UnityEngine.Vector3 axis1, UnityEngine.Vector3 axis2)
  RVA=0x0ABE27B0  token=0x60000F7  UnityEngine.Vector3 ToBiPolar(UnityEngine.Vector3 euler)
  RVA=0x0ABE2770  token=0x60000F8  System.Single ToBiPolar(System.Single angle)
  RVA=0x0ABE2410  token=0x60000F9  UnityEngine.Quaternion MirrorYZ(UnityEngine.Quaternion r, UnityEngine.Quaternion space)
  RVA=0x0ABE2588  token=0x60000FA  UnityEngine.Quaternion MirrorYZ(UnityEngine.Quaternion r)
END_CLASS

CLASS: RootMotion.Singleton`1
TYPE:  abstract class
TOKEN: 0x2000028
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  T                               sInstance  // static @ 0x0
PROPERTIES:
  instance  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000FC  System.Void Clear()
  RVA=-1  // not resolved  token=0x60000FD  System.Void Awake()
  RVA=-1  // not resolved  token=0x60000FE  System.Void .ctor()
END_CLASS

CLASS: RootMotion.SolverManager
TYPE:  class
TOKEN: 0x2000029
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Boolean                  fixTransforms  // 0x18
  private           UnityEngine.Animator            animator  // 0x20
  private           UnityEngine.Animation           legacy  // 0x28
  private           System.Boolean                  updateFrame  // 0x30
  private           System.Boolean                  componentInitiated  // 0x31
  private           System.Boolean                  skipSolverUpdate  // 0x32
PROPERTIES:
  animatePhysics  get=0x035BD390
  isAnimated  get=0x0408A440
METHODS:
  RVA=0x0ABE2A18  token=0x60000FF  System.Void Disable()
  RVA=0x041E1670  token=0x6000100  System.Void InitiateSolver()
  RVA=0x041E1670  token=0x6000101  System.Void UpdateSolver()
  RVA=0x041E1670  token=0x6000102  System.Void FixTransforms()
  RVA=0x04089B40  token=0x6000103  System.Void OnDisable()
  RVA=0x0408A300  token=0x6000104  System.Void Start()
  RVA=0x0408A310  token=0x6000106  System.Void Initiate()
  RVA=0x035BD130  token=0x6000107  System.Void Update()
  RVA=0x0408A350  token=0x6000108  System.Void FindAnimatorRecursive(UnityEngine.Transform t, System.Boolean findInChildren)
  RVA=0x035BD2D0  token=0x600010A  System.Void FixedUpdate()
  RVA=0x035BD200  token=0x600010B  System.Void LateUpdate()
  RVA=0x0ABE2A90  token=0x600010C  System.Void UpdateSolverExternal()
  RVA=0x05394740  token=0x600010D  System.Void .ctor()
END_CLASS

CLASS: RootMotion.TriggerEventBroadcaster
TYPE:  class
TOKEN: 0x200002A
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.GameObject          target  // 0x18
METHODS:
  RVA=0x0ABE2AC0  token=0x600010E  System.Void OnTriggerEnter(UnityEngine.Collider collider)
  RVA=0x0ABE2BF0  token=0x600010F  System.Void OnTriggerStay(UnityEngine.Collider collider)
  RVA=0x0ABE2B58  token=0x6000110  System.Void OnTriggerExit(UnityEngine.Collider collider)
  RVA=0x05393520  token=0x6000111  System.Void .ctor()
END_CLASS

CLASS: RootMotion.V2Tools
TYPE:  static class
TOKEN: 0x200002B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04D88A40  token=0x6000112  UnityEngine.Vector2 XZ(UnityEngine.Vector3 v)
  RVA=0x0ABE2CD8  token=0x6000113  System.Single DeltaAngle(UnityEngine.Vector2 dir1, UnityEngine.Vector2 dir2)
  RVA=0x0ABE2C88  token=0x6000114  System.Single DeltaAngleXZ(UnityEngine.Vector3 dir1, UnityEngine.Vector3 dir2)
  RVA=0x0ABE2D34  token=0x6000115  System.Boolean LineCircleIntersect(UnityEngine.Vector2 p1, UnityEngine.Vector2 p2, UnityEngine.Vector2 c, System.Single r)
  RVA=0x0ABE2E48  token=0x6000116  System.Boolean RayCircleIntersect(UnityEngine.Vector2 p1, UnityEngine.Vector2 dir, UnityEngine.Vector2 c, System.Single r)
END_CLASS

CLASS: RootMotion.V3Tools
TYPE:  static class
TOKEN: 0x200002C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04DA9490  token=0x6000117  System.Single GetYaw(UnityEngine.Vector3 forward)
  RVA=0x0ABE3A98  token=0x6000118  System.Single GetPitch(UnityEngine.Vector3 forward)
  RVA=0x0ABE39D8  token=0x6000119  System.Single GetBank(UnityEngine.Vector3 forward, UnityEngine.Vector3 up)
  RVA=0x0ABE3B88  token=0x600011A  System.Single GetYaw(UnityEngine.Vector3 spaceForward, UnityEngine.Vector3 spaceUp, UnityEngine.Vector3 forward)
  RVA=0x0ABE3ADC  token=0x600011B  System.Single GetPitch(UnityEngine.Vector3 spaceForward, UnityEngine.Vector3 spaceUp, UnityEngine.Vector3 forward)
  RVA=0x0ABE3840  token=0x600011C  System.Single GetBank(UnityEngine.Vector3 spaceForward, UnityEngine.Vector3 spaceUp, UnityEngine.Vector3 forward, UnityEngine.Vector3 up)
  RVA=0x0ABE3E14  token=0x600011D  UnityEngine.Vector3 Lerp(UnityEngine.Vector3 fromVector, UnityEngine.Vector3 toVector, System.Single weight)
  RVA=0x0ABE4048  token=0x600011E  UnityEngine.Vector3 Slerp(UnityEngine.Vector3 fromVector, UnityEngine.Vector3 toVector, System.Single weight)
  RVA=0x0ABE3620  token=0x600011F  UnityEngine.Vector3 ExtractVertical(UnityEngine.Vector3 v, UnityEngine.Vector3 verticalAxis, System.Single weight)
  RVA=0x0ABE34DC  token=0x6000120  UnityEngine.Vector3 ExtractHorizontal(UnityEngine.Vector3 v, UnityEngine.Vector3 normal, System.Single weight)
  RVA=0x0ABE3744  token=0x6000121  UnityEngine.Vector3 Flatten(UnityEngine.Vector3 v, UnityEngine.Vector3 normal)
  RVA=0x0ABE331C  token=0x6000122  UnityEngine.Vector3 ClampDirection(UnityEngine.Vector3 direction, UnityEngine.Vector3 normalDirection, System.Single clampWeight, System.Int32 clampSmoothing)
  RVA=0x0ABE2F6C  token=0x6000123  UnityEngine.Vector3 ClampDirection(UnityEngine.Vector3 direction, UnityEngine.Vector3 normalDirection, System.Single clampWeight, System.Int32 clampSmoothing, System.Boolean& changed)
  RVA=0x0ABE3138  token=0x6000124  UnityEngine.Vector3 ClampDirection(UnityEngine.Vector3 direction, UnityEngine.Vector3 normalDirection, System.Single clampWeight, System.Int32 clampSmoothing, System.Single& clampValue)
  RVA=0x031CD780  token=0x6000125  UnityEngine.Vector3 LineToPlane(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.Vector3 planeNormal, UnityEngine.Vector3 planePoint)
  RVA=0x0ABE3E8C  token=0x6000126  UnityEngine.Vector3 PointToPlane(UnityEngine.Vector3 point, UnityEngine.Vector3 planePosition, UnityEngine.Vector3 planeNormal)
  RVA=0x0ABE40C8  token=0x6000127  UnityEngine.Vector3 TransformPointUnscaled(UnityEngine.Transform t, UnityEngine.Vector3 point)
  RVA=0x0ABE3C38  token=0x6000128  UnityEngine.Vector3 InverseTransformPointUnscaled(UnityEngine.Transform t, UnityEngine.Vector3 point)
  RVA=0x0ABE3D24  token=0x6000129  UnityEngine.Vector3 InverseTransformPoint(UnityEngine.Vector3 tPos, UnityEngine.Quaternion tRot, UnityEngine.Vector3 tScale, UnityEngine.Vector3 point)
  RVA=0x0ABE41A4  token=0x600012A  UnityEngine.Vector3 TransformPoint(UnityEngine.Vector3 tPos, UnityEngine.Quaternion tRot, UnityEngine.Vector3 tScale, UnityEngine.Vector3 point)
  RVA=0x0AAED088  token=0x600012B  UnityEngine.Vector3 Div(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2)
END_CLASS

CLASS: RootMotion.Warning
TYPE:  static class
TOKEN: 0x200002D
SIZE:  0x10
FIELDS:
  public    static  System.Boolean                  logged  // static @ 0x0
METHODS:
  RVA=0x0ABE4310  token=0x600012C  System.Void Log(System.String message, RootMotion.Warning.Logger logger, System.Boolean logInEditMode)
  RVA=0x0ABE4270  token=0x600012D  System.Void Log(System.String message, UnityEngine.Transform context, System.Boolean logInEditMode)
END_CLASS

CLASS: RootMotion.Demos.Navigator
TYPE:  class
TOKEN: 0x200002F
SIZE:  0x70
FIELDS:
  public            System.Boolean                  activeTargetSeeking  // 0x10
  public            System.Single                   cornerRadius  // 0x14
  public            System.Single                   recalculateOnPathDistance  // 0x18
  public            System.Single                   maxSampleDistance  // 0x1c
  public            System.Single                   nextPathInterval  // 0x20
  private           UnityEngine.Vector3             <normalizedDeltaPosition>k__BackingField  // 0x24
  private           RootMotion.Demos.Navigator.State<state>k__BackingField  // 0x30
  private           UnityEngine.Transform           transform  // 0x38
  private           System.Int32                    cornerIndex  // 0x40
  private           UnityEngine.Vector3[]           corners  // 0x48
  private           UnityEngine.AI.NavMeshPath      path  // 0x50
  private           UnityEngine.Vector3             lastTargetPosition  // 0x58
  private           System.Boolean                  initiated  // 0x64
  private           System.Single                   nextPathTime  // 0x68
PROPERTIES:
  normalizedDeltaPosition  get=0x04D905E0  set=0x04D905D0
  state  get=0x04D864F0  set=0x04D86740
METHODS:
  RVA=0x0ABE0F30  token=0x6000136  System.Void Initiate(UnityEngine.Transform transform)
  RVA=0x0ABE1010  token=0x6000137  System.Void Update(UnityEngine.Vector3 targetPosition)
  RVA=0x0ABE0CBC  token=0x6000138  System.Void CalculatePath(UnityEngine.Vector3 targetPosition)
  RVA=0x0ABE0D40  token=0x6000139  System.Boolean Find(UnityEngine.Vector3 targetPosition)
  RVA=0x0ABE0FF4  token=0x600013A  System.Void Stop()
  RVA=0x0ABE0F04  token=0x600013B  System.Single HorDistance(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2)
  RVA=0x0ABE13C8  token=0x600013C  System.Void Visualize()
  RVA=0x0ABE15D0  token=0x600013D  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.BipedIK
TYPE:  class
TOKEN: 0x2000031
SIZE:  0x48
EXTENDS: RootMotion.SolverManager
FIELDS:
  public            RootMotion.BipedReferences      references  // 0x38
  public            RootMotion.FinalIK.BipedIKSolverssolvers  // 0x40
METHODS:
  RVA=0x0ABD81E8  token=0x600013E  System.Void OpenUserManual()
  RVA=0x0ABD81B4  token=0x600013F  System.Void OpenScriptReference()
  RVA=0x0ABD85D4  token=0x6000140  System.Void SupportGroup()
  RVA=0x0ABD8018  token=0x6000141  System.Void ASThread()
  RVA=0x0ABD80AC  token=0x6000142  System.Single GetIKPositionWeight(UnityEngine.AvatarIKGoal goal)
  RVA=0x0ABD8120  token=0x6000143  System.Single GetIKRotationWeight(UnityEngine.AvatarIKGoal goal)
  RVA=0x0ABD821C  token=0x6000144  System.Void SetIKPositionWeight(UnityEngine.AvatarIKGoal goal, System.Single weight)
  RVA=0x0ABD8288  token=0x6000145  System.Void SetIKRotationWeight(UnityEngine.AvatarIKGoal goal, System.Single weight)
  RVA=0x0ABD8254  token=0x6000146  System.Void SetIKPosition(UnityEngine.AvatarIKGoal goal, UnityEngine.Vector3 IKPosition)
  RVA=0x0ABD82C0  token=0x6000147  System.Void SetIKRotation(UnityEngine.AvatarIKGoal goal, UnityEngine.Quaternion IKRotation)
  RVA=0x0ABD80D0  token=0x6000148  UnityEngine.Vector3 GetIKPosition(UnityEngine.AvatarIKGoal goal)
  RVA=0x0ABD8144  token=0x6000149  UnityEngine.Quaternion GetIKRotation(UnityEngine.AvatarIKGoal goal)
  RVA=0x0ABD831C  token=0x600014A  System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight, System.Single headWeight, System.Single eyesWeight, System.Single clampWeight, System.Single clampWeightHead, System.Single clampWeightEyes)
  RVA=0x0ABD82EC  token=0x600014B  System.Void SetLookAtPosition(UnityEngine.Vector3 lookAtPosition)
  RVA=0x0ABD8388  token=0x600014C  System.Void SetSpinePosition(UnityEngine.Vector3 spinePosition)
  RVA=0x0ABD83B8  token=0x600014D  System.Void SetSpineWeight(System.Single weight)
  RVA=0x0ABD804C  token=0x600014E  RootMotion.FinalIK.IKSolverLimb GetGoalIK(UnityEngine.AvatarIKGoal goal)
  RVA=0x0ABD817C  token=0x600014F  System.Void InitiateBipedIK()
  RVA=0x0ABD8608  token=0x6000150  System.Void UpdateBipedIK()
  RVA=0x0ABD83E0  token=0x6000151  System.Void SetToDefaults()
  RVA=0x0326A140  token=0x6000152  System.Void FixTransforms()
  RVA=0x039DE270  token=0x6000153  System.Void InitiateSolver()
  RVA=0x0326A380  token=0x6000154  System.Void UpdateSolver()
  RVA=0x0ABD818C  token=0x6000155  System.Void LogWarning(System.String message)
  RVA=0x039DD680  token=0x6000156  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.BipedIKSolvers
TYPE:  class
TOKEN: 0x2000032
SIZE:  0x60
FIELDS:
  public            RootMotion.FinalIK.IKSolverLimb leftFoot  // 0x10
  public            RootMotion.FinalIK.IKSolverLimb rightFoot  // 0x18
  public            RootMotion.FinalIK.IKSolverLimb leftHand  // 0x20
  public            RootMotion.FinalIK.IKSolverLimb rightHand  // 0x28
  public            RootMotion.FinalIK.IKSolverFABRIKspine  // 0x30
  public            RootMotion.FinalIK.IKSolverLookAtlookAt  // 0x38
  public            RootMotion.FinalIK.IKSolverAim  aim  // 0x40
  public            RootMotion.FinalIK.Constraints  pelvis  // 0x48
  private           RootMotion.FinalIK.IKSolverLimb[]_limbs  // 0x50
  private           RootMotion.FinalIK.IKSolver[]   _ikSolvers  // 0x58
PROPERTIES:
  limbs  get=0x0326AB60
  ikSolvers  get=0x0ABD7EC0
METHODS:
  RVA=0x039DE050  token=0x6000159  System.Void AssignReferences(RootMotion.BipedReferences references)
  RVA=0x039DD7A0  token=0x600015A  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.Constraint
TYPE:  abstract class
TOKEN: 0x2000033
SIZE:  0x20
FIELDS:
  public            UnityEngine.Transform           transform  // 0x10
  public            System.Single                   weight  // 0x18
PROPERTIES:
  isValid  get=0x0ABDD218
METHODS:
  RVA=-1  // abstract  token=0x600015C  System.Void UpdateConstraint()
  RVA=0x041E1670  token=0x600015D  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.ConstraintPosition
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x30
EXTENDS: RootMotion.FinalIK.Constraint
FIELDS:
  public            UnityEngine.Vector3             position  // 0x20
METHODS:
  RVA=0x0ABDCF14  token=0x600015E  System.Void UpdateConstraint()
  RVA=0x041E1670  token=0x600015F  System.Void .ctor()
  RVA=0x053908C0  token=0x6000160  System.Void .ctor(UnityEngine.Transform transform)
END_CLASS

CLASS: RootMotion.FinalIK.ConstraintPositionOffset
TYPE:  class
TOKEN: 0x2000035
SIZE:  0x48
EXTENDS: RootMotion.FinalIK.Constraint
FIELDS:
  public            UnityEngine.Vector3             offset  // 0x20
  private           UnityEngine.Vector3             defaultLocalPosition  // 0x2c
  private           UnityEngine.Vector3             lastLocalPosition  // 0x38
  private           System.Boolean                  initiated  // 0x44
PROPERTIES:
  positionChanged  get=0x0ABDCE90
METHODS:
  RVA=0x0ABDCCDC  token=0x6000161  System.Void UpdateConstraint()
  RVA=0x041E1670  token=0x6000162  System.Void .ctor()
  RVA=0x053908C0  token=0x6000163  System.Void .ctor(UnityEngine.Transform transform)
END_CLASS

CLASS: RootMotion.FinalIK.ConstraintRotation
TYPE:  class
TOKEN: 0x2000036
SIZE:  0x30
EXTENDS: RootMotion.FinalIK.Constraint
FIELDS:
  public            UnityEngine.Quaternion          rotation  // 0x20
METHODS:
  RVA=0x0ABDD180  token=0x6000165  System.Void UpdateConstraint()
  RVA=0x041E1670  token=0x6000166  System.Void .ctor()
  RVA=0x053908C0  token=0x6000167  System.Void .ctor(UnityEngine.Transform transform)
END_CLASS

CLASS: RootMotion.FinalIK.ConstraintRotationOffset
TYPE:  class
TOKEN: 0x2000037
SIZE:  0x78
EXTENDS: RootMotion.FinalIK.Constraint
FIELDS:
  public            UnityEngine.Quaternion          offset  // 0x20
  private           UnityEngine.Quaternion          defaultRotation  // 0x30
  private           UnityEngine.Quaternion          defaultLocalRotation  // 0x40
  private           UnityEngine.Quaternion          lastLocalRotation  // 0x50
  private           UnityEngine.Quaternion          defaultTargetLocalRotation  // 0x60
  private           System.Boolean                  initiated  // 0x70
PROPERTIES:
  rotationChanged  get=0x0ABDD128
METHODS:
  RVA=0x0ABDCFC0  token=0x6000168  System.Void UpdateConstraint()
  RVA=0x041E1670  token=0x6000169  System.Void .ctor()
  RVA=0x053908C0  token=0x600016A  System.Void .ctor(UnityEngine.Transform transform)
END_CLASS

CLASS: RootMotion.FinalIK.Constraints
TYPE:  class
TOKEN: 0x2000038
SIZE:  0x58
FIELDS:
  public            UnityEngine.Transform           transform  // 0x10
  public            UnityEngine.Transform           target  // 0x18
  public            UnityEngine.Vector3             positionOffset  // 0x20
  public            UnityEngine.Vector3             position  // 0x2c
  public            System.Single                   positionWeight  // 0x38
  public            UnityEngine.Vector3             rotationOffset  // 0x3c
  public            UnityEngine.Vector3             rotation  // 0x48
  public            System.Single                   rotationWeight  // 0x54
METHODS:
  RVA=0x0326B110  token=0x600016C  System.Boolean IsValid()
  RVA=0x039DDFD0  token=0x600016D  System.Void Initiate(UnityEngine.Transform transform)
  RVA=0x0326ADF0  token=0x600016E  System.Void Update()
  RVA=0x041E1670  token=0x600016F  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.Finger
TYPE:  class
TOKEN: 0x2000039
SIZE:  0xB8
FIELDS:
  public            System.Single                   weight  // 0x10
  public            System.Single                   rotationWeight  // 0x14
  public            RootMotion.FinalIK.Finger.DOF   rotationDOF  // 0x18
  public            System.Boolean                  fixBone1Twist  // 0x1c
  public            UnityEngine.Transform           bone1  // 0x20
  public            UnityEngine.Transform           bone2  // 0x28
  public            UnityEngine.Transform           bone3  // 0x30
  public            UnityEngine.Transform           tip  // 0x38
  public            UnityEngine.Transform           target  // 0x40
  private           System.Boolean                  <initiated>k__BackingField  // 0x48
  private           RootMotion.FinalIK.IKSolverLimb solver  // 0x50
  private           UnityEngine.Quaternion          bone3RelativeToTarget  // 0x58
  private           UnityEngine.Vector3             bone3DefaultLocalPosition  // 0x68
  private           UnityEngine.Quaternion          bone3DefaultLocalRotation  // 0x74
  private           UnityEngine.Vector3             bone1Axis  // 0x84
  private           UnityEngine.Vector3             tipAxis  // 0x90
  private           UnityEngine.Vector3             bone1TwistAxis  // 0x9c
  private           UnityEngine.Vector3             defaultBendNormal  // 0xa8
PROPERTIES:
  initiated  get=0x04D867D0  set=0x04D86810
  IKPosition  get=0x0ABDE3AC  set=0x0ABDE3FC
  IKRotation  get=0x0ABDE3D8  set=0x0ABDE428
METHODS:
  RVA=0x0ABDDC10  token=0x6000176  System.Boolean IsValid(System.String& errorMessage)
  RVA=0x0ABDD418  token=0x6000177  System.Void Initiate(UnityEngine.Transform hand, System.Int32 index)
  RVA=0x0ABDD344  token=0x6000178  System.Void FixTransforms()
  RVA=0x0ABDDCD8  token=0x6000179  System.Void StoreDefaultLocalState()
  RVA=0x0ABDDD90  token=0x600017A  System.Void Update(System.Single masterWeight)
  RVA=0x04DA8F10  token=0x600017B  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.FingerRig
TYPE:  class
TOKEN: 0x200003B
SIZE:  0x50
EXTENDS: RootMotion.SolverManager
FIELDS:
  public            System.Single                   weight  // 0x38
  public            RootMotion.FinalIK.Finger[]     fingers  // 0x40
  private           System.Boolean                  <initiated>k__BackingField  // 0x48
PROPERTIES:
  initiated  get=0x04D867D0  set=0x04D86810
METHODS:
  RVA=0x0ABE5028  token=0x600017E  System.Boolean IsValid(System.String& errorMessage)
  RVA=0x0ABE4CCC  token=0x600017F  System.Void AutoDetect()
  RVA=0x0ABE4B48  token=0x6000180  System.Void AddFinger(UnityEngine.Transform bone1, UnityEngine.Transform bone2, UnityEngine.Transform bone3, UnityEngine.Transform tip, UnityEngine.Transform target)
  RVA=0x0ABE5090  token=0x6000181  System.Void RemoveFinger(System.Int32 index)
  RVA=0x0ABE4A8C  token=0x6000182  System.Void AddChildrenRecursive(UnityEngine.Transform parent, UnityEngine.Transform[]& array)
  RVA=0x0ABE4F90  token=0x6000183  System.Void InitiateSolver()
  RVA=0x0ABE5210  token=0x6000184  System.Void UpdateFingerSolvers()
  RVA=0x0ABE4F28  token=0x6000185  System.Void FixFingerTransforms()
  RVA=0x0ABE51C4  token=0x6000186  System.Void StoreDefaultLocalState()
  RVA=0x0ABE5270  token=0x6000187  System.Void UpdateSolver()
  RVA=0x0ABE4F7C  token=0x6000188  System.Void FixTransforms()
  RVA=0x0ABE5278  token=0x6000189  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.Grounder
TYPE:  abstract class
TOKEN: 0x200003C
SIZE:  0x50
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Single                   weight  // 0x18
  public            System.Single                   maintianPelvisFootWeight  // 0x1c
  public            System.Single                   footAdsorbWeight  // 0x20
  public            RootMotion.FinalIK.Grounding    solver  // 0x28
  public            RootMotion.FinalIK.Grounder.GrounderDelegateOnPreGrounder  // 0x30
  public            RootMotion.FinalIK.Grounder.GrounderDelegateOnPostGrounder  // 0x38
  public            RootMotion.FinalIK.Grounder.GrounderDelegateOnPostIK  // 0x40
  private           System.Boolean                  <initiated>k__BackingField  // 0x48
PROPERTIES:
  initiated  get=0x04D867D0  set=0x04D86810
METHODS:
  RVA=-1  // abstract  token=0x600018A  System.Void ResetPosition()
  RVA=0x0ABEAA4C  token=0x600018D  UnityEngine.Vector3 GetSpineOffsetTarget()
  RVA=0x0ABD818C  token=0x600018E  System.Void LogWarning(System.String message)
  RVA=0x0ABEA6A4  token=0x600018F  UnityEngine.Vector3 GetLegSpineBendVector(RootMotion.FinalIK.Grounding.Leg leg)
  RVA=0x0ABEA850  token=0x6000190  UnityEngine.Vector3 GetLegSpineTangent(RootMotion.FinalIK.Grounding.Leg leg)
  RVA=-1  // abstract  token=0x6000191  System.Void OpenUserManual()
  RVA=-1  // abstract  token=0x6000192  System.Void OpenScriptReference()
  RVA=0x04407EE0  token=0x6000193  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.GrounderBipedIK
TYPE:  class
TOKEN: 0x200003E
SIZE:  0xB8
EXTENDS: RootMotion.FinalIK.Grounder
FIELDS:
  public            RootMotion.FinalIK.BipedIK      ik  // 0x50
  public            System.Single                   spineBend  // 0x58
  public            System.Single                   spineSpeed  // 0x5c
  private           UnityEngine.Transform[]         feet  // 0x60
  private           UnityEngine.Quaternion[]        footRotations  // 0x68
  private           UnityEngine.Vector3             animatedPelvisLocalPosition  // 0x70
  private           UnityEngine.Vector3             solvedPelvisLocalPosition  // 0x7c
  private           UnityEngine.Vector3             spineOffset  // 0x88
  private           System.Single                   lastWeight  // 0x94
  private           System.Single                   lastAdsorbWeight  // 0x98
  private           System.Single                   rightFootOffsetY  // 0x9c
  private           System.Single                   leftFootOffsetY  // 0xa0
  private           System.Single                   rightFootOri  // 0xa4
  private           System.Single                   leftFootOri  // 0xa8
  public            UnityEngine.Transform           ikOriReferenceBone  // 0xb0
PROPERTIES:
  RightFootOffsetY  get=0x04D92880
  LeftFootOffsetY  get=0x04D91E60
  RightFootOri  get=0x04DA94B0
  LeftFootOri  get=0x04D8E650
METHODS:
  RVA=0x0ABE5BD4  token=0x6000198  System.Void OpenUserManual()
  RVA=0x0ABE5BA0  token=0x6000199  System.Void OpenScriptReference()
  RVA=0x0ABE5C08  token=0x600019A  System.Void ResetPosition()
  RVA=0x03E5EAC0  token=0x600019F  System.Boolean IsReadyToInitiate()
  RVA=0x03E5EA60  token=0x60001A0  System.Void Update()
  RVA=0x03E5EBC0  token=0x60001A1  System.Void Initiate()
  RVA=0x04D1A0D0  token=0x60001A2  System.Void OnDisable()
  RVA=0x0326EB70  token=0x60001A3  System.Void OnSolverUpdate()
  RVA=0x0326CB90  token=0x60001A4  System.Void SetLegIK(RootMotion.FinalIK.IKSolverLimb limb, System.Int32 index)
  RVA=0x0326C820  token=0x60001A5  System.Void OnPostSolverUpdate()
  RVA=0x04D38F60  token=0x60001A6  System.Void OnDestroy()
  RVA=0x03271CE0  token=0x60001A7  UnityEngine.Quaternion ClampPitchAndRollAroundForward(UnityEngine.Quaternion Q, UnityEngine.Quaternion calfQ, System.Single minPitch, System.Single maxPitch, System.Single minRoll, System.Single maxRoll)
  RVA=0x032721C0  token=0x60001A8  System.Single NormalizeAngle(System.Single angle)
  RVA=0x04407E50  token=0x60001A9  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.GrounderFBBIK
TYPE:  class
TOKEN: 0x200003F
SIZE:  0x80
EXTENDS: RootMotion.FinalIK.Grounder
FIELDS:
  public            RootMotion.FinalIK.FullBodyBipedIKik  // 0x50
  public            System.Single                   spineBend  // 0x58
  public            System.Single                   spineSpeed  // 0x5c
  public            RootMotion.FinalIK.GrounderFBBIK.SpineEffector[]spine  // 0x60
  private           UnityEngine.Transform[]         feet  // 0x68
  private           UnityEngine.Vector3             spineOffset  // 0x70
  private           System.Boolean                  firstSolve  // 0x7c
METHODS:
  RVA=0x0ABE67EC  token=0x60001AA  System.Void OpenTutorial()
  RVA=0x0ABE6820  token=0x60001AB  System.Void OpenUserManual()
  RVA=0x0ABE67B8  token=0x60001AC  System.Void OpenScriptReference()
  RVA=0x0ABE6854  token=0x60001AD  System.Void ResetPosition()
  RVA=0x0ABE5F58  token=0x60001AE  System.Boolean IsReadyToInitiate()
  RVA=0x0ABE6A3C  token=0x60001AF  System.Void Update()
  RVA=0x04D8D080  token=0x60001B0  System.Void FixedUpdate()
  RVA=0x04D8D080  token=0x60001B1  System.Void LateUpdate()
  RVA=0x0ABE5C48  token=0x60001B2  System.Void Initiate()
  RVA=0x0ABE62DC  token=0x60001B3  System.Void OnSolverUpdate()
  RVA=0x0ABE688C  token=0x60001B4  System.Void SetLegIK(RootMotion.FinalIK.IKEffector effector, RootMotion.FinalIK.Grounding.Leg leg)
  RVA=0x0ABE61B4  token=0x60001B5  System.Void OnDrawGizmosSelected()
  RVA=0x0ABE62C4  token=0x60001B6  System.Void OnPostSolverUpdate()
  RVA=0x0ABE5FD0  token=0x60001B7  System.Void OnDestroy()
  RVA=0x0ABE6A98  token=0x60001B8  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.GrounderIK
TYPE:  class
TOKEN: 0x2000041
SIZE:  0xB8
EXTENDS: RootMotion.FinalIK.Grounder
FIELDS:
  public            RootMotion.FinalIK.IK[]         legs  // 0x50
  public            UnityEngine.Transform           pelvis  // 0x58
  public            UnityEngine.Transform           characterRoot  // 0x60
  public            System.Single                   rootRotationWeight  // 0x68
  public            System.Single                   rootRotationSpeed  // 0x6c
  public            System.Single                   maxRootRotationAngle  // 0x70
  private           UnityEngine.Transform[]         feet  // 0x78
  private           UnityEngine.Quaternion[]        footRotations  // 0x80
  private           UnityEngine.Vector3             animatedPelvisLocalPosition  // 0x88
  private           UnityEngine.Vector3             solvedPelvisLocalPosition  // 0x94
  private           System.Int32                    solvedFeet  // 0xa0
  private           System.Boolean                  solved  // 0xa4
  private           System.Single                   lastWeight  // 0xa8
  private           UnityEngine.Rigidbody           characterRootRigidbody  // 0xb0
METHODS:
  RVA=0x0ABE7848  token=0x60001BB  System.Void OpenUserManual()
  RVA=0x0ABE7814  token=0x60001BC  System.Void OpenScriptReference()
  RVA=0x0ABE787C  token=0x60001BD  System.Void ResetPosition()
  RVA=0x0ABE6F7C  token=0x60001BE  System.Boolean IsReadyToInitiate()
  RVA=0x0ABE7320  token=0x60001BF  System.Void OnDisable()
  RVA=0x0ABE7DEC  token=0x60001C0  System.Void Update()
  RVA=0x0ABE6B20  token=0x60001C1  System.Void Initiate()
  RVA=0x0ABE75AC  token=0x60001C2  System.Void OnSolverUpdate()
  RVA=0x0ABE7B04  token=0x60001C3  System.Void SetLegIK(System.Int32 index)
  RVA=0x0ABE73D8  token=0x60001C4  System.Void OnPostSolverUpdate()
  RVA=0x0ABE70F8  token=0x60001C5  System.Void OnDestroy()
  RVA=0x0ABE81E8  token=0x60001C6  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.GrounderQuadruped
TYPE:  class
TOKEN: 0x2000042
SIZE:  0x150
EXTENDS: RootMotion.FinalIK.Grounder
FIELDS:
  public            RootMotion.FinalIK.Grounding    forelegSolver  // 0x50
  public            System.Single                   rootRotationWeight  // 0x58
  public            System.Single                   minRootRotation  // 0x5c
  public            System.Single                   maxRootRotation  // 0x60
  public            System.Single                   rootRotationSpeed  // 0x64
  public            System.Single                   maxLegOffset  // 0x68
  public            System.Single                   maxForeLegOffset  // 0x6c
  public            System.Single                   maintainHeadRotationWeight  // 0x70
  public            UnityEngine.Transform           characterRoot  // 0x78
  public            UnityEngine.Transform           pelvis  // 0x80
  public            UnityEngine.Transform           lastSpineBone  // 0x88
  public            UnityEngine.Transform           head  // 0x90
  public            RootMotion.FinalIK.IK[]         legs  // 0x98
  public            RootMotion.FinalIK.IK[]         forelegs  // 0xa0
  public            UnityEngine.Vector3             gravity  // 0xa8
  private           RootMotion.FinalIK.GrounderQuadruped.Foot[]feet  // 0xb8
  private           UnityEngine.Vector3             animatedPelvisLocalPosition  // 0xc0
  private           UnityEngine.Quaternion          animatedPelvisLocalRotation  // 0xcc
  private           UnityEngine.Quaternion          animatedHeadLocalRotation  // 0xdc
  private           UnityEngine.Vector3             solvedPelvisLocalPosition  // 0xec
  private           UnityEngine.Quaternion          solvedPelvisLocalRotation  // 0xf8
  private           UnityEngine.Quaternion          solvedHeadLocalRotation  // 0x108
  private           System.Int32                    solvedFeet  // 0x118
  private           System.Boolean                  solved  // 0x11c
  private           System.Single                   angle  // 0x120
  private           UnityEngine.Transform           forefeetRoot  // 0x128
  private           UnityEngine.Quaternion          headRotation  // 0x130
  private           System.Single                   lastWeight  // 0x140
  private           UnityEngine.Rigidbody           characterRootRigidbody  // 0x148
METHODS:
  RVA=0x0ABE9AB4  token=0x60001C7  System.Void OpenUserManual()
  RVA=0x0ABE9A80  token=0x60001C8  System.Void OpenScriptReference()
  RVA=0x0ABE9AE8  token=0x60001C9  System.Void ResetPosition()
  RVA=0x0ABE8D18  token=0x60001CA  System.Boolean IsReadyToInitiate()
  RVA=0x0ABE8BD0  token=0x60001CB  System.Boolean IsReadyToInitiateLegs(RootMotion.FinalIK.IK[] ikComponents)
  RVA=0x0ABE8F34  token=0x60001CC  System.Void OnDisable()
  RVA=0x0ABEA55C  token=0x60001CD  System.Void Update()
  RVA=0x0ABE87F4  token=0x60001CE  System.Void Initiate()
  RVA=0x0ABE8490  token=0x60001CF  UnityEngine.Transform[] InitiateFeet(RootMotion.FinalIK.IK[] ikComponents, RootMotion.FinalIK.GrounderQuadruped.Foot[]& f, System.Int32 indexOffset)
  RVA=0x0ABE8E18  token=0x60001D0  System.Void LateUpdate()
  RVA=0x0ABE9CAC  token=0x60001D1  System.Void RootRotation()
  RVA=0x0ABE9278  token=0x60001D2  System.Void OnSolverUpdate()
  RVA=0x0ABEA2C4  token=0x60001D3  System.Void UpdateForefeetRoot()
  RVA=0x0ABEA158  token=0x60001D4  System.Void SetFootIK(RootMotion.FinalIK.GrounderQuadruped.Foot foot, System.Single maxOffset)
  RVA=0x0ABE8FA0  token=0x60001D5  System.Void OnPostSolverUpdate()
  RVA=0x0ABE8EF8  token=0x60001D6  System.Void OnDestroy()
  RVA=0x0ABE8274  token=0x60001D7  System.Void DestroyLegs(RootMotion.FinalIK.IK[] ikComponents)
  RVA=0x0ABEA5BC  token=0x60001D8  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.Grounding
TYPE:  class
TOKEN: 0x2000044
SIZE:  0x190
FIELDS:
  public            UnityEngine.Transform           IKFootBoneL  // 0x10
  public            UnityEngine.Transform           IKFootBoneR  // 0x18
  public            System.Single                   floorLogicTheta  // 0x20
  public            System.Single                   floorAnimTheta  // 0x24
  public            System.Single                   floorPredictTheta  // 0x28
  public            System.Single                   floorFeetThetaByFoot  // 0x2c
  public            System.Single                   floorFeetThetaByRoot  // 0x30
  public            System.Single                   floorFeetThetaByLogic  // 0x34
  public            System.Single                   betweenFeetDistance  // 0x38
  public            System.Boolean                  isMoving  // 0x3c
  public            System.Boolean                  isAccelerating  // 0x3d
  public            System.Boolean                  isPlayingMontage  // 0x3e
  public            System.Boolean                  isOnMovableBase  // 0x3f
  public            System.Single                   SG_timeRef  // 0x40
  public            System.Single                   gait  // 0x44
  public            System.Single                   startAnimWeight  // 0x48
  public            UnityEngine.Vector3             playerSpeed  // 0x4c
  private           System.Boolean                  m_isAcceleratingLastFrame  // 0x58
  public            System.Boolean                  isLastFrameInvalid  // 0x59
  public            System.Single                   maxLegLength  // 0x5c
  public            System.Single                   maxFeetOffset  // 0x60
  public            System.Single                   minPelvisHeight  // 0x64
  public            UnityEngine.LayerMask           layers  // 0x68
  public            System.Single                   maxStep  // 0x6c
  public            System.Single                   heightOffset  // 0x70
  public            System.Single                   footSpeed  // 0x74
  public            System.Single                   footRadius  // 0x78
  public            System.Single                   footCenterOffset  // 0x7c
  public            System.Single                   prediction  // 0x80
  public            System.Single                   footRotationWeight  // 0x84
  public            System.Single                   footRotationSpeed  // 0x88
  public            System.Single                   maxFootRotationAngleFore  // 0x8c
  public            System.Single                   maxFootRotationAngleBack  // 0x90
  public            System.Single                   maxFootRotationAngleIn  // 0x94
  public            System.Single                   maxFootRotationAngleOut  // 0x98
  public            System.Boolean                  rotateSolver  // 0x9c
  public            System.Single                   pelvisSpeed  // 0xa0
  public            System.Single                   pelvisSpeedXZ  // 0xa4
  public            System.Single                   pelvisDamper  // 0xa8
  public            System.Single                   lowerPelvisWeight  // 0xac
  public            System.Single                   liftPelvisWeight  // 0xb0
  public            System.Single                   rootSphereCastRadius  // 0xb4
  public            System.Boolean                  overstepFallsDown  // 0xb8
  public            RootMotion.FinalIK.Grounding.Qualityquality  // 0xbc
  private           RootMotion.BipedReferences      <references>k__BackingField  // 0xc0
  private           RootMotion.FinalIK.Grounding.Leg[]<legs>k__BackingField  // 0xc8
  private           RootMotion.FinalIK.Grounding.Pelvis<pelvis>k__BackingField  // 0xd0
  private           System.Boolean                  <isGrounded>k__BackingField  // 0xd8
  private           UnityEngine.Transform           <root>k__BackingField  // 0xe0
  private           UnityEngine.RaycastHit          <rootHit>k__BackingField  // 0xe8
  public            RootMotion.FinalIK.Grounding.OnRaycastDelegateRaycast  // 0x128
  public            RootMotion.FinalIK.Grounding.OnCapsuleCastDelegateCapsuleCast  // 0x130
  public            RootMotion.FinalIK.Grounding.OnSphereCastDelegateSphereCast  // 0x138
  private           UnityEngine.Transform           <entityTransform>k__BackingField  // 0x140
  private           System.Boolean                  <entityIsOnMovableBase>k__BackingField  // 0x148
  private           UnityEngine.Vector3             <entityRelativePosition>k__BackingField  // 0x14c
  private           UnityEngine.Quaternion          <entityRelativeRotation>k__BackingField  // 0x158
  private           System.Boolean                  lastIsDependingLeftLeg  // 0x168
  private           System.Boolean                  isSameLegWithLastFrame  // 0x169
  private           System.Single                   rootDeltaAngle  // 0x16c
  private           System.Single                   pelvisForwardOffset  // 0x170
  private           UnityEngine.Vector3             m_lastFrameForwardVec  // 0x174
  private           System.Boolean                  lastIsUsingLoswest  // 0x180
  private           System.Single                   m_actualMaxFootOffset  // 0x184
  public            System.Single                   forwardFeetOffset  // 0x188
  public            System.Boolean                  isUsingLoswest  // 0x18c
  public            System.Boolean                  isDependingLeftLeg  // 0x18d
  private           System.Boolean                  initiated  // 0x18e
PROPERTIES:
  references  get=0x04D892F0  set=0x06AC5ACC
  legs  get=0x04D8D1D0  set=0x06AC5B30
  pelvis  get=0x04D876B0  set=0x0678D7BC
  isGrounded  get=0x04DA6C90  set=0x04DA6CE0
  root  get=0x04D8D1C0  set=0x06AC5B08
  rootHit  get=0x04DA0A70  set=0x04DA9530
  rootGrounded  get=0x02FFF600
  entityTransform  get=0x04D8DA60  set=0x06B8B404
  entityIsOnMovableBase  get=0x04D8FE60  set=0x04D8FE90
  entityRelativePosition  get=0x04DA94C0  set=0x04DA9500
  entityRelativeRotation  get=0x04DA94E0  set=0x04DA9520
  isInitiated  get=0x04DA94F0
  up  get=0x03269FA0
  forward  get=0x02F40ED0
  useRootRotation  get=0x0326A000
METHODS:
  RVA=0x03269640  token=0x60001DA  System.Void _UpdateFloorPredictTheta()
  RVA=0x03269970  token=0x60001DB  System.Void _UpdateFloorFeetThetaByFoot()
  RVA=0x03C5D910  token=0x60001DC  System.Void _UpdateFloorFeetThetaByRoot()
  RVA=0x032701C0  token=0x60001F2  UnityEngine.RaycastHit GetRootHit(System.Single maxDistanceMlp)
  RVA=0x03E5F430  token=0x60001F3  System.Boolean IsValid(System.String& errorMessage)
  RVA=0x03E5EF60  token=0x60001F4  System.Void Initiate(UnityEngine.Transform root, UnityEngine.Transform[] feet, RootMotion.BipedReferences references)
  RVA=0x0326D370  token=0x60001F5  System.Void Update()
  RVA=0x0ABEAD1C  token=0x60001F6  UnityEngine.Vector3 GetLegsPlaneNormal()
  RVA=0x0ABEAF28  token=0x60001F7  System.Void Reset()
  RVA=0x0ABEAF10  token=0x60001F9  System.Void LogWarning(System.String message)
  RVA=0x03269F50  token=0x60001FC  System.Single GetVerticalOffset(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2)
  RVA=0x0ABEAB2C  token=0x60001FD  UnityEngine.Vector3 Flatten(UnityEngine.Vector3 v)
  RVA=0x0ABEAC20  token=0x60001FF  UnityEngine.Vector3 GetFootCenterOffset()
  RVA=0x04407F80  token=0x6000200  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.AimIK
TYPE:  class
TOKEN: 0x200004B
SIZE:  0x40
EXTENDS: RootMotion.FinalIK.IK
FIELDS:
  public            RootMotion.FinalIK.IKSolverAim  solver  // 0x38
METHODS:
  RVA=0x0ABE44B4  token=0x6000256  System.Void OpenUserManual()
  RVA=0x0ABE444C  token=0x6000257  System.Void OpenScriptReference()
  RVA=0x0ABE4480  token=0x6000258  System.Void OpenSetupTutorial()
  RVA=0x0ABE44E8  token=0x6000259  System.Void SupportGroup()
  RVA=0x0ABE4418  token=0x600025A  System.Void ASThread()
  RVA=0x04D85A50  token=0x600025B  RootMotion.FinalIK.IKSolver GetIKSolver()
  RVA=0x0ABE451C  token=0x600025C  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.ArmIK
TYPE:  class
TOKEN: 0x200004C
SIZE:  0x40
EXTENDS: RootMotion.FinalIK.IK
FIELDS:
  public            RootMotion.FinalIK.IKSolverArm  solver  // 0x38
METHODS:
  RVA=0x0ABE45F4  token=0x600025D  System.Void OpenUserManual()
  RVA=0x0ABE45C0  token=0x600025E  System.Void OpenScriptReference()
  RVA=0x0ABE4628  token=0x600025F  System.Void SupportGroup()
  RVA=0x0ABE458C  token=0x6000260  System.Void ASThread()
  RVA=0x04D85A50  token=0x6000261  RootMotion.FinalIK.IKSolver GetIKSolver()
  RVA=0x0ABE465C  token=0x6000262  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.CCDIK
TYPE:  class
TOKEN: 0x200004D
SIZE:  0x40
EXTENDS: RootMotion.FinalIK.IK
FIELDS:
  public            RootMotion.FinalIK.IKSolverCCD  solver  // 0x38
METHODS:
  RVA=0x0ABE4734  token=0x6000263  System.Void OpenUserManual()
  RVA=0x0ABE4700  token=0x6000264  System.Void OpenScriptReference()
  RVA=0x0ABE4768  token=0x6000265  System.Void SupportGroup()
  RVA=0x0ABE46CC  token=0x6000266  System.Void ASThread()
  RVA=0x04D85A50  token=0x6000267  RootMotion.FinalIK.IKSolver GetIKSolver()
  RVA=0x0ABE479C  token=0x6000268  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.FABRIK
TYPE:  class
TOKEN: 0x200004E
SIZE:  0x40
EXTENDS: RootMotion.FinalIK.IK
FIELDS:
  public            RootMotion.FinalIK.IKSolverFABRIKsolver  // 0x38
METHODS:
  RVA=0x0ABE49B4  token=0x6000269  System.Void OpenUserManual()
  RVA=0x0ABE4980  token=0x600026A  System.Void OpenScriptReference()
  RVA=0x0ABE49E8  token=0x600026B  System.Void SupportGroup()
  RVA=0x0ABE494C  token=0x600026C  System.Void ASThread()
  RVA=0x04D85A50  token=0x600026D  RootMotion.FinalIK.IKSolver GetIKSolver()
  RVA=0x0ABE4A1C  token=0x600026E  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.FABRIKRoot
TYPE:  class
TOKEN: 0x200004F
SIZE:  0x40
EXTENDS: RootMotion.FinalIK.IK
FIELDS:
  public            RootMotion.FinalIK.IKSolverFABRIKRootsolver  // 0x38
METHODS:
  RVA=0x0ABE4874  token=0x600026F  System.Void OpenUserManual()
  RVA=0x0ABE4840  token=0x6000270  System.Void OpenScriptReference()
  RVA=0x0ABE48A8  token=0x6000271  System.Void SupportGroup()
  RVA=0x0ABE480C  token=0x6000272  System.Void ASThread()
  RVA=0x04D85A50  token=0x6000273  RootMotion.FinalIK.IKSolver GetIKSolver()
  RVA=0x0ABE48DC  token=0x6000274  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.FullBodyBipedIK
TYPE:  class
TOKEN: 0x2000050
SIZE:  0x48
EXTENDS: RootMotion.FinalIK.IK
FIELDS:
  public            RootMotion.BipedReferences      references  // 0x38
  public            RootMotion.FinalIK.IKSolverFullBodyBipedsolver  // 0x40
METHODS:
  RVA=0x0ABE54D4  token=0x6000275  System.Void OpenUserManual()
  RVA=0x0ABE546C  token=0x6000276  System.Void OpenScriptReference()
  RVA=0x0ABE54A0  token=0x6000277  System.Void OpenSetupTutorial()
  RVA=0x0ABE5438  token=0x6000278  System.Void OpenInspectorTutorial()
  RVA=0x0ABE5AC4  token=0x6000279  System.Void SupportGroup()
  RVA=0x0ABE532C  token=0x600027A  System.Void ASThread()
  RVA=0x0ABE5A88  token=0x600027B  System.Void SetReferences(RootMotion.BipedReferences references, UnityEngine.Transform rootNode)
  RVA=0x04D85A60  token=0x600027C  RootMotion.FinalIK.IKSolver GetIKSolver()
  RVA=0x0ABE5508  token=0x600027D  System.Boolean ReferencesError(System.String& errorMessage)
  RVA=0x0ABE56E8  token=0x600027E  System.Boolean ReferencesWarning(System.String& warningMessage)
  RVA=0x0ABE5A5C  token=0x600027F  System.Void Reinitiate()
  RVA=0x0ABE5360  token=0x6000280  System.Void AutoDetectReferences()
  RVA=0x0ABE5AF8  token=0x6000281  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IK
TYPE:  abstract class
TOKEN: 0x2000051
SIZE:  0x38
EXTENDS: RootMotion.SolverManager
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000282  RootMotion.FinalIK.IKSolver GetIKSolver()
  RVA=0x0ABFD638  token=0x6000283  System.Void UpdateSolver()
  RVA=0x0ABFD5DC  token=0x6000284  System.Void InitiateSolver()
  RVA=0x0ABFD598  token=0x6000285  System.Void FixTransforms()
  RVA=-1  // abstract  token=0x6000286  System.Void OpenUserManual()
  RVA=-1  // abstract  token=0x6000287  System.Void OpenScriptReference()
  RVA=0x05394738  token=0x6000288  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKExecutionOrder
TYPE:  class
TOKEN: 0x2000052
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            RootMotion.FinalIK.IK[]         IKComponents  // 0x18
  public            UnityEngine.Animator            animator  // 0x20
  private           System.Boolean                  fixedFrame  // 0x28
PROPERTIES:
  animatePhysics  get=0x0ABF8560
METHODS:
  RVA=0x0ABF84E0  token=0x600028A  System.Void Start()
  RVA=0x0ABF8534  token=0x600028B  System.Void Update()
  RVA=0x0ABF843C  token=0x600028C  System.Void FixedUpdate()
  RVA=0x0ABF846C  token=0x600028D  System.Void LateUpdate()
  RVA=0x0ABF83CC  token=0x600028E  System.Void FixTransforms()
  RVA=0x05393520  token=0x600028F  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.LegIK
TYPE:  class
TOKEN: 0x2000053
SIZE:  0x40
EXTENDS: RootMotion.FinalIK.IK
FIELDS:
  public            RootMotion.FinalIK.IKSolverLeg  solver  // 0x38
METHODS:
  RVA=0x0ABFD778  token=0x6000290  System.Void OpenUserManual()
  RVA=0x0ABFD744  token=0x6000291  System.Void OpenScriptReference()
  RVA=0x0ABFD7AC  token=0x6000292  System.Void SupportGroup()
  RVA=0x0ABFD710  token=0x6000293  System.Void ASThread()
  RVA=0x04D85A50  token=0x6000294  RootMotion.FinalIK.IKSolver GetIKSolver()
  RVA=0x0ABFD7E0  token=0x6000295  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.LimbIK
TYPE:  class
TOKEN: 0x2000054
SIZE:  0x40
EXTENDS: RootMotion.FinalIK.IK
FIELDS:
  public            RootMotion.FinalIK.IKSolverLimb solver  // 0x38
METHODS:
  RVA=0x0ABFD8B8  token=0x6000296  System.Void OpenUserManual()
  RVA=0x0ABFD884  token=0x6000297  System.Void OpenScriptReference()
  RVA=0x0ABFD8EC  token=0x6000298  System.Void SupportGroup()
  RVA=0x0ABFD850  token=0x6000299  System.Void ASThread()
  RVA=0x04D85A50  token=0x600029A  RootMotion.FinalIK.IKSolver GetIKSolver()
  RVA=0x0ABFD920  token=0x600029B  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.LookAtIK
TYPE:  class
TOKEN: 0x2000055
SIZE:  0x40
EXTENDS: RootMotion.FinalIK.IK
FIELDS:
  public            RootMotion.FinalIK.IKSolverLookAtsolver  // 0x38
METHODS:
  RVA=0x0ABFD9F8  token=0x600029C  System.Void OpenUserManual()
  RVA=0x0ABFD9C4  token=0x600029D  System.Void OpenScriptReference()
  RVA=0x0ABFDA2C  token=0x600029E  System.Void SupportGroup()
  RVA=0x0ABFD990  token=0x600029F  System.Void ASThread()
  RVA=0x04D85A50  token=0x60002A0  RootMotion.FinalIK.IKSolver GetIKSolver()
  RVA=0x039DCA90  token=0x60002A1  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.TrigonometricIK
TYPE:  class
TOKEN: 0x2000056
SIZE:  0x40
EXTENDS: RootMotion.FinalIK.IK
FIELDS:
  public            RootMotion.FinalIK.IKSolverTrigonometricsolver  // 0x38
METHODS:
  RVA=0x0ABFEE44  token=0x60002A2  System.Void OpenUserManual()
  RVA=0x0ABFEE10  token=0x60002A3  System.Void OpenScriptReference()
  RVA=0x0ABFEE78  token=0x60002A4  System.Void SupportGroup()
  RVA=0x0ABFEDDC  token=0x60002A5  System.Void ASThread()
  RVA=0x04D85A50  token=0x60002A6  RootMotion.FinalIK.IKSolver GetIKSolver()
  RVA=0x0ABFEEAC  token=0x60002A7  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.VRIK
TYPE:  class
TOKEN: 0x2000057
SIZE:  0x48
EXTENDS: RootMotion.FinalIK.IK
FIELDS:
  public            RootMotion.FinalIK.VRIK.Referencesreferences  // 0x38
  public            RootMotion.FinalIK.IKSolverVR   solver  // 0x40
METHODS:
  RVA=0x0ABFF03C  token=0x60002A8  System.Void OpenUserManual()
  RVA=0x0ABFEFD4  token=0x60002A9  System.Void OpenScriptReference()
  RVA=0x0ABFF008  token=0x60002AA  System.Void OpenSetupTutorial()
  RVA=0x0ABFEF1C  token=0x60002AB  System.Void AutoDetectReferences()
  RVA=0x0ABFEF40  token=0x60002AC  System.Void GuessHandOrientations()
  RVA=0x04D85A60  token=0x60002AD  RootMotion.FinalIK.IKSolver GetIKSolver()
  RVA=0x0ABFEF6C  token=0x60002AE  System.Void InitiateSolver()
  RVA=0x0ABFF070  token=0x60002AF  System.Void UpdateSolver()
  RVA=0x0ABFF1C4  token=0x60002B0  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.FABRIKChain
TYPE:  class
TOKEN: 0x2000059
SIZE:  0x28
FIELDS:
  public            RootMotion.FinalIK.FABRIK       ik  // 0x10
  public            System.Single                   pull  // 0x18
  public            System.Single                   pin  // 0x1c
  public            System.Int32[]                  children  // 0x20
METHODS:
  RVA=0x0ABEEB38  token=0x60002B7  System.Boolean IsValid(System.String& message)
  RVA=0x0ABEEB14  token=0x60002B8  System.Void Initiate()
  RVA=0x0ABEEBE8  token=0x60002B9  System.Void Stage1(RootMotion.FinalIK.FABRIKChain[] chain)
  RVA=0x0ABEECDC  token=0x60002BA  System.Void Stage2(UnityEngine.Vector3 rootPosition, RootMotion.FinalIK.FABRIKChain[] chain)
  RVA=0x0ABEE818  token=0x60002BB  UnityEngine.Vector3 GetCentroid(RootMotion.FinalIK.FABRIKChain[] chain)
  RVA=0x0ABEEE04  token=0x60002BC  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.FBBIKArmBending
TYPE:  class
TOKEN: 0x200005A
SIZE:  0x78
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            RootMotion.FinalIK.FullBodyBipedIKik  // 0x18
  public            UnityEngine.Vector3             bendDirectionOffsetLeft  // 0x20
  public            UnityEngine.Vector3             bendDirectionOffsetRight  // 0x2c
  public            UnityEngine.Vector3             characterSpaceBendOffsetLeft  // 0x38
  public            UnityEngine.Vector3             characterSpaceBendOffsetRight  // 0x44
  private           UnityEngine.Quaternion          leftHandTargetRotation  // 0x50
  private           UnityEngine.Quaternion          rightHandTargetRotation  // 0x60
  private           System.Boolean                  initiated  // 0x70
METHODS:
  RVA=0x0ABEEE54  token=0x60002BD  System.Void LateUpdate()
  RVA=0x0ABEF598  token=0x60002BE  System.Void OnPostFBBIK()
  RVA=0x0ABEF470  token=0x60002BF  System.Void OnDestroy()
  RVA=0x05393520  token=0x60002C0  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.FBBIKHeadEffector
TYPE:  class
TOKEN: 0x200005B
SIZE:  0x170
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            RootMotion.FinalIK.FullBodyBipedIKik  // 0x18
  public            System.Single                   positionWeight  // 0x20
  public            System.Single                   bodyWeight  // 0x24
  public            System.Single                   thighWeight  // 0x28
  public            System.Boolean                  handsPullBody  // 0x2c
  public            System.Single                   rotationWeight  // 0x30
  public            System.Single                   bodyClampWeight  // 0x34
  public            System.Single                   headClampWeight  // 0x38
  public            System.Single                   bendWeight  // 0x3c
  public            RootMotion.FinalIK.FBBIKHeadEffector.BendBone[]bendBones  // 0x40
  public            System.Single                   CCDWeight  // 0x48
  public            System.Single                   roll  // 0x4c
  public            System.Single                   damper  // 0x50
  public            UnityEngine.Transform[]         CCDBones  // 0x58
  public            System.Single                   postStretchWeight  // 0x60
  public            System.Single                   maxStretch  // 0x64
  public            System.Single                   stretchDamper  // 0x68
  public            System.Boolean                  fixHead  // 0x6c
  public            UnityEngine.Transform[]         stretchBones  // 0x70
  public            UnityEngine.Vector3             chestDirection  // 0x78
  public            System.Single                   chestDirectionWeight  // 0x84
  public            UnityEngine.Transform[]         chestBones  // 0x88
  public            RootMotion.FinalIK.IKSolver.UpdateDelegateOnPostHeadEffectorFK  // 0x90
  private           UnityEngine.Vector3             offset  // 0x98
  private           UnityEngine.Vector3             headToBody  // 0xa4
  private           UnityEngine.Vector3             shoulderCenterToHead  // 0xb0
  private           UnityEngine.Vector3             headToLeftThigh  // 0xbc
  private           UnityEngine.Vector3             headToRightThigh  // 0xc8
  private           UnityEngine.Vector3             leftShoulderPos  // 0xd4
  private           UnityEngine.Vector3             rightShoulderPos  // 0xe0
  private           System.Single                   shoulderDist  // 0xec
  private           System.Single                   leftShoulderDist  // 0xf0
  private           System.Single                   rightShoulderDist  // 0xf4
  private           UnityEngine.Quaternion          chestRotation  // 0xf8
  private           UnityEngine.Quaternion          headRotationRelativeToRoot  // 0x108
  private           UnityEngine.Quaternion[]        ccdDefaultLocalRotations  // 0x118
  private           UnityEngine.Vector3             headLocalPosition  // 0x120
  private           UnityEngine.Quaternion          headLocalRotation  // 0x12c
  private           UnityEngine.Vector3[]           stretchLocalPositions  // 0x140
  private           UnityEngine.Quaternion[]        stretchLocalRotations  // 0x148
  private           UnityEngine.Vector3[]           chestLocalPositions  // 0x150
  private           UnityEngine.Quaternion[]        chestLocalRotations  // 0x158
  private           System.Int32                    bendBonesCount  // 0x160
  private           System.Int32                    ccdBonesCount  // 0x164
  private           System.Int32                    stretchBonesCount  // 0x168
  private           System.Int32                    chestBonesCount  // 0x16c
METHODS:
  RVA=0x0ABF2924  token=0x60002C1  System.Void Start()
  RVA=0x0ABF1BEC  token=0x60002C2  System.Void OnStoreDefaultLocalState()
  RVA=0x0ABF0D84  token=0x60002C3  System.Void OnFixTransforms()
  RVA=0x0ABF1370  token=0x60002C4  System.Void OnPreRead()
  RVA=0x0ABF25F4  token=0x60002C5  System.Void SpineBend()
  RVA=0x0ABEF76C  token=0x60002C6  System.Void CCDPass()
  RVA=0x0ABEFDA0  token=0x60002C7  System.Void Iterate(System.Int32 iteration)
  RVA=0x0ABF1170  token=0x60002C8  System.Void OnPostUpdate()
  RVA=0x0ABEFAF4  token=0x60002C9  System.Void ChestDirection()
  RVA=0x0ABF2120  token=0x60002CA  System.Void PostStretching()
  RVA=0x0ABF0850  token=0x60002CB  System.Void LerpSolverPosition(RootMotion.FinalIK.IKEffector effector, UnityEngine.Vector3 position, System.Single weight, UnityEngine.Vector3 offset)
  RVA=0x0ABF2488  token=0x60002CC  System.Void Solve(UnityEngine.Vector3& pos1, UnityEngine.Vector3& pos2, System.Single nominalDistance)
  RVA=0x0ABF0970  token=0x60002CD  System.Void OnDestroy()
  RVA=0x0ABF2DD4  token=0x60002CE  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.FBIKChain
TYPE:  class
TOKEN: 0x200005D
SIZE:  0x88
FIELDS:
  public            System.Single                   pin  // 0x10
  public            System.Single                   pull  // 0x14
  public            System.Single                   push  // 0x18
  public            System.Single                   pushParent  // 0x1c
  public            System.Single                   reach  // 0x20
  public            RootMotion.FinalIK.FBIKChain.SmoothingreachSmoothing  // 0x24
  public            RootMotion.FinalIK.FBIKChain.SmoothingpushSmoothing  // 0x28
  public            RootMotion.FinalIK.IKSolver.Node[]nodes  // 0x30
  public            System.Int32[]                  children  // 0x38
  public            RootMotion.FinalIK.FBIKChain.ChildConstraint[]childConstraints  // 0x40
  public            RootMotion.FinalIK.IKConstraintBendbendConstraint  // 0x48
  private           System.Single                   rootLength  // 0x50
  private           System.Boolean                  initiated  // 0x54
  private           System.Single                   length  // 0x58
  private           System.Single                   distance  // 0x5c
  private           RootMotion.FinalIK.IKSolver.Pointp  // 0x60
  private           System.Single                   reachForce  // 0x68
  private           System.Single                   pullParentSum  // 0x6c
  private           System.Single[]                 crossFades  // 0x70
  private           System.Single                   sqrMag1  // 0x78
  private           System.Single                   sqrMag2  // 0x7c
  private           System.Single                   sqrMagDif  // 0x80
  private   static  System.Single                   maxLimbLength  // const
METHODS:
  RVA=0x0ABF56DC  token=0x60002D3  System.Void .ctor()
  RVA=0x0ABF5580  token=0x60002D4  System.Void .ctor(System.Single pin, System.Single pull, UnityEngine.Transform[] nodeTransforms)
  RVA=0x0ABF4850  token=0x60002D5  System.Void SetNodes(UnityEngine.Transform[] boneTransforms)
  RVA=0x0ABF395C  token=0x60002D6  System.Int32 GetNodeIndex(UnityEngine.Transform boneTransform)
  RVA=0x0ABF3B9C  token=0x60002D7  System.Boolean IsValid(System.String& message)
  RVA=0x0ABF3A04  token=0x60002D8  System.Void Initiate(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF4374  token=0x60002D9  System.Void ReadPose(RootMotion.FinalIK.IKSolverFullBody solver, System.Boolean fullBody)
  RVA=0x0ABF3154  token=0x60002DA  System.Void CalculateBoneLengths(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF4010  token=0x60002DB  System.Void Reach(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF3C80  token=0x60002DC  UnityEngine.Vector3 Push(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF4D48  token=0x60002DD  System.Void SolveTrigonometric(RootMotion.FinalIK.IKSolverFullBody solver, System.Boolean calculateBendDirection)
  RVA=0x0ABF5044  token=0x60002DE  System.Void Stage1(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF5460  token=0x60002DF  System.Void Stage2(RootMotion.FinalIK.IKSolverFullBody solver, UnityEngine.Vector3 position)
  RVA=0x0ABF49AC  token=0x60002E0  System.Void SolveConstraintSystems(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF4AC0  token=0x60002E1  UnityEngine.Vector3 SolveFABRIKJoint(UnityEngine.Vector3 pos1, UnityEngine.Vector3 pos2, System.Single length)
  RVA=0x0ABF379C  token=0x60002E2  UnityEngine.Vector3 GetDirToBendPoint(UnityEngine.Vector3 direction, UnityEngine.Vector3 bendDirection, System.Single directionMagnitude)
  RVA=0x0ABF4948  token=0x60002E3  System.Void SolveChildConstraints(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF4B98  token=0x60002E4  System.Void SolveLinearConstraint(RootMotion.FinalIK.IKSolver.Node node1, RootMotion.FinalIK.IKSolver.Node node2, System.Single crossFade, System.Single distance)
  RVA=0x0ABF3660  token=0x60002E5  System.Void ForwardReach(UnityEngine.Vector3 position)
  RVA=0x0ABF2FA8  token=0x60002E6  System.Void BackwardReach(UnityEngine.Vector3 position)
END_CLASS

CLASS: RootMotion.FinalIK.IKConstraintBend
TYPE:  class
TOKEN: 0x2000060
SIZE:  0x88
FIELDS:
  public            UnityEngine.Transform           bone1  // 0x10
  public            UnityEngine.Transform           bone2  // 0x18
  public            UnityEngine.Transform           bone3  // 0x20
  public            UnityEngine.Transform           bendGoal  // 0x28
  public            UnityEngine.Vector3             direction  // 0x30
  public            UnityEngine.Quaternion          rotationOffset  // 0x3c
  public            System.Single                   weight  // 0x4c
  public            UnityEngine.Vector3             defaultLocalDirection  // 0x50
  public            UnityEngine.Vector3             defaultChildDirection  // 0x5c
  public            System.Single                   clampF  // 0x68
  private           System.Int32                    chainIndex1  // 0x6c
  private           System.Int32                    nodeIndex1  // 0x70
  private           System.Int32                    chainIndex2  // 0x74
  private           System.Int32                    nodeIndex2  // 0x78
  private           System.Int32                    chainIndex3  // 0x7c
  private           System.Int32                    nodeIndex3  // 0x80
  private           System.Boolean                  <initiated>k__BackingField  // 0x84
  private           System.Boolean                  limbOrientationsSet  // 0x85
PROPERTIES:
  initiated  get=0x02A80FA0  set=0x02A80FB0
METHODS:
  RVA=0x0ABF608C  token=0x60002EF  System.Boolean IsValid(RootMotion.FinalIK.IKSolverFullBody solver, RootMotion.Warning.Logger logger)
  RVA=0x0ABF6AA4  token=0x60002F2  System.Void .ctor()
  RVA=0x0ABF6A74  token=0x60002F3  System.Void .ctor(UnityEngine.Transform bone1, UnityEngine.Transform bone2, UnityEngine.Transform bone3)
  RVA=0x031D14F0  token=0x60002F4  System.Void SetBones(UnityEngine.Transform bone1, UnityEngine.Transform bone2, UnityEngine.Transform bone3)
  RVA=0x0ABF5D9C  token=0x60002F5  System.Void Initiate(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF6874  token=0x60002F6  System.Void SetLimbOrientation(UnityEngine.Vector3 upper, UnityEngine.Vector3 lower, UnityEngine.Vector3 last)
  RVA=0x0ABF6218  token=0x60002F7  System.Void LimitBend(System.Single solverWeight, System.Single positionWeight)
  RVA=0x0ABF57E0  token=0x60002F8  UnityEngine.Vector3 GetDir(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF6778  token=0x60002F9  UnityEngine.Vector3 OrthoToLimb(RootMotion.FinalIK.IKSolverFullBody solver, UnityEngine.Vector3 tangent)
  RVA=0x0ABF667C  token=0x60002FA  UnityEngine.Vector3 OrthoToBone1(RootMotion.FinalIK.IKSolverFullBody solver, UnityEngine.Vector3 tangent)
END_CLASS

CLASS: RootMotion.FinalIK.IKEffector
TYPE:  class
TOKEN: 0x2000061
SIZE:  0xF0
FIELDS:
  public            UnityEngine.Transform           bone  // 0x10
  public            UnityEngine.Transform           target  // 0x18
  public            System.Single                   positionWeight  // 0x20
  public            System.Single                   rotationWeight  // 0x24
  public            UnityEngine.Vector3             position  // 0x28
  public            UnityEngine.Quaternion          rotation  // 0x34
  public            UnityEngine.Vector3             positionOffset  // 0x44
  private           System.Boolean                  <isEndEffector>k__BackingField  // 0x50
  public            System.Boolean                  effectChildNodes  // 0x51
  public            System.Single                   maintainRelativePositionWeight  // 0x54
  public            UnityEngine.Transform[]         childBones  // 0x58
  public            UnityEngine.Transform           planeBone1  // 0x60
  public            UnityEngine.Transform           planeBone2  // 0x68
  public            UnityEngine.Transform           planeBone3  // 0x70
  public            UnityEngine.Quaternion          planeRotationOffset  // 0x78
  private           System.Single                   posW  // 0x88
  private           System.Single                   rotW  // 0x8c
  private           UnityEngine.Vector3[]           localPositions  // 0x90
  private           System.Boolean                  usePlaneNodes  // 0x98
  private           UnityEngine.Quaternion          animatedPlaneRotation  // 0x9c
  private           UnityEngine.Vector3             animatedPosition  // 0xac
  private           System.Boolean                  firstUpdate  // 0xb8
  private           System.Int32                    chainIndex  // 0xbc
  private           System.Int32                    nodeIndex  // 0xc0
  private           System.Int32                    plane1ChainIndex  // 0xc4
  private           System.Int32                    plane1NodeIndex  // 0xc8
  private           System.Int32                    plane2ChainIndex  // 0xcc
  private           System.Int32                    plane2NodeIndex  // 0xd0
  private           System.Int32                    plane3ChainIndex  // 0xd4
  private           System.Int32                    plane3NodeIndex  // 0xd8
  private           System.Int32[]                  childChainIndexes  // 0xe0
  private           System.Int32[]                  childNodeIndexes  // 0xe8
PROPERTIES:
  isEndEffector  get=0x04D86800  set=0x04D86840
METHODS:
  RVA=0x0ABF6AC4  token=0x60002FB  RootMotion.FinalIK.IKSolver.Node GetNode(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF7BA0  token=0x60002FE  System.Void PinToBone(System.Single positionWeight, System.Single rotationWeight)
  RVA=0x0ABF8108  token=0x60002FF  System.Void .ctor()
  RVA=0x0ABF8250  token=0x6000300  System.Void .ctor(UnityEngine.Transform bone, UnityEngine.Transform[] childBones)
  RVA=0x0ABF7304  token=0x6000301  System.Boolean IsValid(RootMotion.FinalIK.IKSolver solver, System.String& message)
  RVA=0x0ABF7028  token=0x6000302  System.Void Initiate(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF7C68  token=0x6000303  System.Void ResetOffset(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF7D48  token=0x6000304  System.Void SetToTarget()
  RVA=0x0ABF7568  token=0x6000305  System.Void OnPreSolve(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF7554  token=0x6000306  System.Void OnPostWrite()
  RVA=0x0ABF6B1C  token=0x6000307  UnityEngine.Quaternion GetPlaneRotation(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF7DE8  token=0x6000308  System.Void Update(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF6D30  token=0x6000309  UnityEngine.Vector3 GetPosition(RootMotion.FinalIK.IKSolverFullBody solver, UnityEngine.Quaternion& planeRotationOffset)
END_CLASS

CLASS: RootMotion.FinalIK.IKMapping
TYPE:  class
TOKEN: 0x2000062
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x02FFF600  token=0x600030A  System.Boolean IsValid(RootMotion.FinalIK.IKSolver solver, System.String& message)
  RVA=0x041E1670  token=0x600030B  System.Void Initiate(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABFB2EC  token=0x600030C  System.Boolean BoneIsValid(UnityEngine.Transform bone, RootMotion.FinalIK.IKSolver solver, System.String& message, RootMotion.Warning.Logger logger)
  RVA=0x0ABF4AC0  token=0x600030D  UnityEngine.Vector3 SolveFABRIKJoint(UnityEngine.Vector3 pos1, UnityEngine.Vector3 pos2, System.Single length)
  RVA=0x041E1670  token=0x600030E  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKMappingBone
TYPE:  class
TOKEN: 0x2000064
SIZE:  0x28
EXTENDS: RootMotion.FinalIK.IKMapping
FIELDS:
  public            UnityEngine.Transform           bone  // 0x10
  public            System.Single                   maintainRotationWeight  // 0x18
  private           RootMotion.FinalIK.IKMapping.BoneMapboneMap  // 0x20
METHODS:
  RVA=0x0ABF86A4  token=0x6000329  System.Boolean IsValid(RootMotion.FinalIK.IKSolver solver, System.String& message)
  RVA=0x0ABF8790  token=0x600032A  System.Void .ctor()
  RVA=0x0ABF8814  token=0x600032B  System.Void .ctor(UnityEngine.Transform bone)
  RVA=0x0ABF8740  token=0x600032C  System.Void StoreDefaultLocalState()
  RVA=0x0ABF85D8  token=0x600032D  System.Void FixTransforms()
  RVA=0x0ABF85FC  token=0x600032E  System.Void Initiate(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF8720  token=0x600032F  System.Void ReadPose()
  RVA=0x0ABF8760  token=0x6000330  System.Void WritePose(System.Single solverWeight)
END_CLASS

CLASS: RootMotion.FinalIK.IKMappingLimb
TYPE:  class
TOKEN: 0x2000065
SIZE:  0x60
EXTENDS: RootMotion.FinalIK.IKMapping
FIELDS:
  public            UnityEngine.Transform           parentBone  // 0x10
  public            UnityEngine.Transform           bone1  // 0x18
  public            UnityEngine.Transform           bone2  // 0x20
  public            UnityEngine.Transform           bone3  // 0x28
  public            System.Single                   maintainRotationWeight  // 0x30
  public            System.Single                   weight  // 0x34
  public            System.Boolean                  updatePlaneRotations  // 0x38
  private           RootMotion.FinalIK.IKMapping.BoneMapboneMapParent  // 0x40
  private           RootMotion.FinalIK.IKMapping.BoneMapboneMap1  // 0x48
  private           RootMotion.FinalIK.IKMapping.BoneMapboneMap2  // 0x50
  private           RootMotion.FinalIK.IKMapping.BoneMapboneMap3  // 0x58
METHODS:
  RVA=0x0ABF8CEC  token=0x6000331  System.Boolean IsValid(RootMotion.FinalIK.IKSolver solver, System.String& message)
  RVA=0x0ABF8958  token=0x6000332  RootMotion.FinalIK.IKMapping.BoneMap GetBoneMap(RootMotion.FinalIK.IKMappingLimb.BoneMapType boneMap)
  RVA=0x0ABF8E40  token=0x6000333  System.Void SetLimbOrientation(UnityEngine.Vector3 upper, UnityEngine.Vector3 lower)
  RVA=0x0ABF939C  token=0x6000334  System.Void .ctor()
  RVA=0x0ABF9510  token=0x6000335  System.Void .ctor(UnityEngine.Transform bone1, UnityEngine.Transform bone2, UnityEngine.Transform bone3, UnityEngine.Transform parentBone)
  RVA=0x0ABF8DF8  token=0x6000336  System.Void SetBones(UnityEngine.Transform bone1, UnityEngine.Transform bone2, UnityEngine.Transform bone3, UnityEngine.Transform parentBone)
  RVA=0x0ABF9188  token=0x6000337  System.Void StoreDefaultLocalState()
  RVA=0x0ABF88B0  token=0x6000338  System.Void FixTransforms()
  RVA=0x0ABF89F8  token=0x6000339  System.Void Initiate(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF8D7C  token=0x600033A  System.Void ReadPose()
  RVA=0x0ABF9224  token=0x600033B  System.Void WritePose(RootMotion.FinalIK.IKSolverFullBody solver, System.Boolean fullBody)
END_CLASS

CLASS: RootMotion.FinalIK.IKMappingSpine
TYPE:  class
TOKEN: 0x2000067
SIZE:  0x78
EXTENDS: RootMotion.FinalIK.IKMapping
FIELDS:
  public            UnityEngine.Transform[]         spineBones  // 0x10
  public            UnityEngine.Transform           leftUpperArmBone  // 0x18
  public            UnityEngine.Transform           rightUpperArmBone  // 0x20
  public            UnityEngine.Transform           leftThighBone  // 0x28
  public            UnityEngine.Transform           rightThighBone  // 0x30
  public            System.Int32                    iterations  // 0x38
  public            System.Single                   twistWeight  // 0x3c
  private           System.Int32                    rootNodeIndex  // 0x40
  private           RootMotion.FinalIK.IKMapping.BoneMap[]spine  // 0x48
  private           RootMotion.FinalIK.IKMapping.BoneMapleftUpperArm  // 0x50
  private           RootMotion.FinalIK.IKMapping.BoneMaprightUpperArm  // 0x58
  private           RootMotion.FinalIK.IKMapping.BoneMapleftThigh  // 0x60
  private           RootMotion.FinalIK.IKMapping.BoneMaprightThigh  // 0x68
  private           System.Boolean                  useFABRIK  // 0x70
METHODS:
  RVA=0x0ABFA170  token=0x600033C  System.Boolean IsValid(RootMotion.FinalIK.IKSolver solver, System.String& message)
  RVA=0x0ABFB14C  token=0x600033D  System.Void .ctor()
  RVA=0x0ABFAF34  token=0x600033E  System.Void .ctor(UnityEngine.Transform[] spineBones, UnityEngine.Transform leftUpperArmBone, UnityEngine.Transform rightUpperArmBone, UnityEngine.Transform leftThighBone, UnityEngine.Transform rightThighBone)
  RVA=0x0932E3C0  token=0x600033F  System.Void SetBones(UnityEngine.Transform[] spineBones, UnityEngine.Transform leftUpperArmBone, UnityEngine.Transform rightUpperArmBone, UnityEngine.Transform leftThighBone, UnityEngine.Transform rightThighBone)
  RVA=0x0ABFAAD4  token=0x6000340  System.Void StoreDefaultLocalState()
  RVA=0x0ABF9814  token=0x6000341  System.Void FixTransforms()
  RVA=0x0ABF99D4  token=0x6000342  System.Void Initiate(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABFAB28  token=0x6000343  System.Boolean UseFABRIK()
  RVA=0x0ABFA7B8  token=0x6000344  System.Void ReadPose()
  RVA=0x0ABFAB54  token=0x6000345  System.Void WritePose(RootMotion.FinalIK.IKSolverFullBody solver)
  RVA=0x0ABF9880  token=0x6000346  System.Void ForwardReach(UnityEngine.Vector3 position)
  RVA=0x0ABF96F0  token=0x6000347  System.Void BackwardReach(UnityEngine.Vector3 position)
  RVA=0x0ABFA444  token=0x6000348  System.Void MapToSolverPositions(RootMotion.FinalIK.IKSolverFullBody solver)
END_CLASS

CLASS: RootMotion.FinalIK.IKSolver
TYPE:  abstract class
TOKEN: 0x2000068
SIZE:  0x58
FIELDS:
  public            System.Boolean                  executedInEditor  // 0x10
  public            UnityEngine.Vector3             IKPosition  // 0x14
  public            System.Single                   IKPositionWeight  // 0x20
  private           System.Boolean                  <initiated>k__BackingField  // 0x24
  public            RootMotion.FinalIK.IKSolver.UpdateDelegateOnPreInitiate  // 0x28
  public            RootMotion.FinalIK.IKSolver.UpdateDelegateOnPostInitiate  // 0x30
  public            RootMotion.FinalIK.IKSolver.UpdateDelegateOnPreUpdate  // 0x38
  public            RootMotion.FinalIK.IKSolver.UpdateDelegateOnPostUpdate  // 0x40
  protected         System.Boolean                  firstInitiation  // 0x48
  protected         UnityEngine.Transform           root  // 0x50
PROPERTIES:
  initiated  get=0x04D86870  set=0x04D86880
METHODS:
  RVA=0x0ABFD198  token=0x6000349  System.Boolean IsValid()
  RVA=-1  // abstract  token=0x600034A  System.Boolean IsValid(System.String& message)
  RVA=0x039DE430  token=0x600034B  System.Void Initiate(UnityEngine.Transform root)
  RVA=0x0326AC50  token=0x600034C  System.Void Update()
  RVA=0x016057F0  token=0x600034D  UnityEngine.Vector3 GetIKPosition()
  RVA=0x04D91E50  token=0x600034E  System.Void SetIKPosition(UnityEngine.Vector3 position)
  RVA=0x011EC8A0  token=0x600034F  System.Single GetIKPositionWeight()
  RVA=0x0ABFD574  token=0x6000350  System.Void SetIKPositionWeight(System.Single weight)
  RVA=0x04D86270  token=0x6000351  UnityEngine.Transform GetRoot()
  RVA=-1  // abstract  token=0x6000354  RootMotion.FinalIK.IKSolver.Point[] GetPoints()
  RVA=-1  // abstract  token=0x6000355  RootMotion.FinalIK.IKSolver.Point GetPoint(UnityEngine.Transform transform)
  RVA=-1  // abstract  token=0x6000356  System.Void FixTransforms()
  RVA=-1  // abstract  token=0x6000357  System.Void StoreDefaultLocalState()
  RVA=-1  // abstract  token=0x6000358  System.Void OnInitiate()
  RVA=-1  // abstract  token=0x6000359  System.Void OnUpdate()
  RVA=0x0ABFD1EC  token=0x600035A  System.Void LogWarning(System.String message)
  RVA=0x039DFC20  token=0x600035B  UnityEngine.Transform ContainsDuplicateBone(RootMotion.FinalIK.IKSolver.Bone[] bones)
  RVA=0x039DEEF0  token=0x600035C  System.Boolean HierarchyIsValid(RootMotion.FinalIK.IKSolver.Bone[] bones)
  RVA=0x0ABFD204  token=0x600035D  System.Single PreSolveBones(RootMotion.FinalIK.IKSolver.Bone[]& bones)
  RVA=0x04DA9690  token=0x600035E  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverAim
TYPE:  class
TOKEN: 0x200006E
SIZE:  0xF0
EXTENDS: RootMotion.FinalIK.IKSolverHeuristic
FIELDS:
  public            UnityEngine.Transform           transform  // 0x88
  public            UnityEngine.Vector3             axis  // 0x90
  public            UnityEngine.Vector3             poleAxis  // 0x9c
  public            UnityEngine.Vector3             polePosition  // 0xa8
  public            System.Single                   poleWeight  // 0xb4
  public            UnityEngine.Transform           poleTarget  // 0xb8
  public            System.Single                   clampWeight  // 0xc0
  public            System.Int32                    clampSmoothing  // 0xc4
  public            RootMotion.FinalIK.IKSolver.IterationDelegateOnPreIteration  // 0xc8
  private           System.Single                   step  // 0xd0
  private           UnityEngine.Vector3             clampedIKPosition  // 0xd4
  private           RootMotion.FinalIK.RotationLimittransformLimit  // 0xe0
  private           UnityEngine.Transform           lastTransform  // 0xe8
PROPERTIES:
  transformAxis  get=0x0ABFBF7C
  transformPoleAxis  get=0x0ABFC000
  minBones  get=0x04D85B80
  localDirection  get=0x0ABFBECC
METHODS:
  RVA=0x0ABFB3E8  token=0x600037A  System.Single GetAngle()
  RVA=0x039DE740  token=0x600037D  System.Void OnInitiate()
  RVA=0x03B9F470  token=0x600037E  System.Void OnUpdate()
  RVA=0x0ABFBDB8  token=0x6000380  System.Void Solve()
  RVA=0x0ABFB4B4  token=0x6000381  UnityEngine.Vector3 GetClampedIKPosition()
  RVA=0x0ABFB888  token=0x6000382  System.Void RotateToTarget(UnityEngine.Vector3 targetPosition, RootMotion.FinalIK.IKSolver.Bone bone, System.Single weight)
  RVA=0x039DDB50  token=0x6000384  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverArm
TYPE:  class
TOKEN: 0x200006F
SIZE:  0xB8
EXTENDS: RootMotion.FinalIK.IKSolver
FIELDS:
  public            System.Single                   IKRotationWeight  // 0x58
  public            UnityEngine.Quaternion          IKRotation  // 0x5c
  public            RootMotion.FinalIK.IKSolver.Pointchest  // 0x70
  public            RootMotion.FinalIK.IKSolver.Pointshoulder  // 0x78
  public            RootMotion.FinalIK.IKSolver.PointupperArm  // 0x80
  public            RootMotion.FinalIK.IKSolver.Pointforearm  // 0x88
  public            RootMotion.FinalIK.IKSolver.Pointhand  // 0x90
  public            System.Boolean                  isLeft  // 0x98
  public            RootMotion.FinalIK.IKSolverVR.Armarm  // 0xa0
  private           UnityEngine.Vector3[]           positions  // 0xa8
  private           UnityEngine.Quaternion[]        rotations  // 0xb0
METHODS:
  RVA=0x0ABFC358  token=0x6000385  System.Boolean IsValid(System.String& message)
  RVA=0x0ABFCBC0  token=0x6000386  System.Boolean SetChain(UnityEngine.Transform chest, UnityEngine.Transform shoulder, UnityEngine.Transform upperArm, UnityEngine.Transform forearm, UnityEngine.Transform hand, UnityEngine.Transform root)
  RVA=0x0ABFC254  token=0x6000387  RootMotion.FinalIK.IKSolver.Point[] GetPoints()
  RVA=0x0ABFC0E8  token=0x6000388  RootMotion.FinalIK.IKSolver.Point GetPoint(UnityEngine.Transform transform)
  RVA=0x0ABFCCF0  token=0x6000389  System.Void StoreDefaultLocalState()
  RVA=0x0ABFC084  token=0x600038A  System.Void FixTransforms()
  RVA=0x0ABFC654  token=0x600038B  System.Void OnInitiate()
  RVA=0x0ABFC6D0  token=0x600038C  System.Void OnUpdate()
  RVA=0x0ABFCC70  token=0x600038D  System.Void Solve()
  RVA=0x0ABFC6FC  token=0x600038E  System.Void Read()
  RVA=0x0ABFCD50  token=0x600038F  System.Void Write()
  RVA=0x0ABFCFA0  token=0x6000390  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverCCD
TYPE:  class
TOKEN: 0x2000070
SIZE:  0x90
EXTENDS: RootMotion.FinalIK.IKSolverHeuristic
FIELDS:
  public            RootMotion.FinalIK.IKSolver.IterationDelegateOnPreIteration  // 0x88
METHODS:
  RVA=0x0ABFF25C  token=0x6000391  System.Void FadeOutBoneWeights()
  RVA=0x0ABFF2F4  token=0x6000392  System.Void OnInitiate()
  RVA=0x0ABFF370  token=0x6000393  System.Void OnUpdate()
  RVA=0x0ABFF628  token=0x6000394  System.Void Solve(UnityEngine.Vector3 targetPosition)
  RVA=0x0ABFFD84  token=0x6000395  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverFABRIK
TYPE:  class
TOKEN: 0x2000071
SIZE:  0xA0
EXTENDS: RootMotion.FinalIK.IKSolverHeuristic
FIELDS:
  public            RootMotion.FinalIK.IKSolver.IterationDelegateOnPreIteration  // 0x88
  private           System.Boolean[]                limitedBones  // 0x90
  private           UnityEngine.Vector3[]           solverLocalPositions  // 0x98
PROPERTIES:
  boneLengthCanBeZero  get=0x012081B0
METHODS:
  RVA=0x0AC02600  token=0x6000396  System.Void SolveForward(UnityEngine.Vector3 position)
  RVA=0x0AC02564  token=0x6000397  System.Void SolveBackward(UnityEngine.Vector3 position)
  RVA=0x0AC016A8  token=0x6000398  UnityEngine.Vector3 GetIKPosition()
  RVA=0x039DF320  token=0x6000399  System.Void OnInitiate()
  RVA=0x03F6ECB0  token=0x600039A  System.Void OnUpdate()
  RVA=0x0AC0269C  token=0x600039C  UnityEngine.Vector3 SolveJoint(UnityEngine.Vector3 pos1, UnityEngine.Vector3 pos2, System.Single length)
  RVA=0x0AC02098  token=0x600039D  System.Void OnPreSolve()
  RVA=0x0AC02050  token=0x600039E  System.Void OnPostSolve()
  RVA=0x0AC02784  token=0x600039F  System.Void Solve(UnityEngine.Vector3 targetPosition)
  RVA=0x0AC014CC  token=0x60003A0  System.Void ForwardReach(UnityEngine.Vector3 position)
  RVA=0x0AC02998  token=0x60003A1  System.Void SolverMove(System.Int32 index, UnityEngine.Vector3 offset)
  RVA=0x0AC02AB0  token=0x60003A2  System.Void SolverRotate(System.Int32 index, UnityEngine.Quaternion rotation, System.Boolean recursive)
  RVA=0x0AC02A2C  token=0x60003A3  System.Void SolverRotateChildren(System.Int32 index, UnityEngine.Quaternion rotation)
  RVA=0x0AC0280C  token=0x60003A4  System.Void SolverMoveChildrenAroundPoint(System.Int32 index, UnityEngine.Quaternion rotation)
  RVA=0x039DEA40  token=0x60003A5  UnityEngine.Quaternion GetParentSolverRotation(System.Int32 index)
  RVA=0x039DEBD0  token=0x60003A6  UnityEngine.Vector3 GetParentSolverPosition(System.Int32 index)
  RVA=0x0AC01744  token=0x60003A7  UnityEngine.Quaternion GetLimitedRotation(System.Int32 index, UnityEngine.Quaternion q, System.Boolean& changed)
  RVA=0x0AC01880  token=0x60003A8  System.Void LimitForward(System.Int32 rotateBone, System.Int32 limitBone)
  RVA=0x0AC01498  token=0x60003A9  System.Void BackwardReach(UnityEngine.Vector3 position)
  RVA=0x0AC01378  token=0x60003AA  System.Void BackwardReachUnlimited(UnityEngine.Vector3 position)
  RVA=0x0AC00DBC  token=0x60003AB  System.Void BackwardReachLimited(UnityEngine.Vector3 position)
  RVA=0x0AC01EE4  token=0x60003AC  System.Void MapToSolverPositions()
  RVA=0x0AC01DF4  token=0x60003AD  System.Void MapToSolverPositionsLimited()
  RVA=0x039DDBB0  token=0x60003AE  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverFABRIKRoot
TYPE:  class
TOKEN: 0x2000072
SIZE:  0x88
EXTENDS: RootMotion.FinalIK.IKSolver
FIELDS:
  public            System.Int32                    iterations  // 0x58
  public            System.Single                   rootPin  // 0x5c
  public            RootMotion.FinalIK.FABRIKChain[]chains  // 0x60
  private           System.Boolean                  zeroWeightApplied  // 0x68
  private           System.Boolean[]                isRoot  // 0x70
  private           UnityEngine.Vector3             rootDefaultPosition  // 0x78
METHODS:
  RVA=0x0AC003BC  token=0x60003AF  System.Boolean IsValid(System.String& message)
  RVA=0x0AC00CD4  token=0x60003B0  System.Void StoreDefaultLocalState()
  RVA=0x0ABFFE84  token=0x60003B1  System.Void FixTransforms()
  RVA=0x0AC00A08  token=0x60003B2  System.Void OnInitiate()
  RVA=0x0AC0032C  token=0x60003B3  System.Boolean IsRoot(System.Int32 index)
  RVA=0x0AC00AD8  token=0x60003B4  System.Void OnUpdate()
  RVA=0x0AC002A4  token=0x60003B5  RootMotion.FinalIK.IKSolver.Point[] GetPoints()
  RVA=0x0AC0022C  token=0x60003B6  RootMotion.FinalIK.IKSolver.Point GetPoint(UnityEngine.Transform transform)
  RVA=0x0ABFFD8C  token=0x60003B7  System.Void AddPointsToArray(RootMotion.FinalIK.IKSolver.Point[]& array, RootMotion.FinalIK.FABRIKChain chain)
  RVA=0x0ABFFF24  token=0x60003B8  UnityEngine.Vector3 GetCentroid()
  RVA=0x0AC00D64  token=0x60003B9  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverFullBody
TYPE:  class
TOKEN: 0x2000073
SIZE:  0xD0
EXTENDS: RootMotion.FinalIK.IKSolver
FIELDS:
  public            System.Int32                    iterations  // 0x58
  public            RootMotion.FinalIK.FBIKChain[]  chain  // 0x60
  public            RootMotion.FinalIK.IKEffector[] effectors  // 0x68
  public            RootMotion.FinalIK.IKMappingSpinespineMapping  // 0x70
  public            RootMotion.FinalIK.IKMappingBone[]boneMappings  // 0x78
  public            RootMotion.FinalIK.IKMappingLimb[]limbMappings  // 0x80
  public            System.Boolean                  FABRIKPass  // 0x88
  public            RootMotion.FinalIK.IKSolver.UpdateDelegateOnPreRead  // 0x90
  public            RootMotion.FinalIK.IKSolver.UpdateDelegateOnPreSolve  // 0x98
  public            RootMotion.FinalIK.IKSolver.IterationDelegateOnPreIteration  // 0xa0
  public            RootMotion.FinalIK.IKSolver.IterationDelegateOnPostIteration  // 0xa8
  public            RootMotion.FinalIK.IKSolver.UpdateDelegateOnPreBend  // 0xb0
  public            RootMotion.FinalIK.IKSolver.UpdateDelegateOnPostSolve  // 0xb8
  public            RootMotion.FinalIK.IKSolver.UpdateDelegateOnStoreDefaultLocalState  // 0xc0
  public            RootMotion.FinalIK.IKSolver.UpdateDelegateOnFixTransforms  // 0xc8
METHODS:
  RVA=0x0AC05DDC  token=0x60003BA  RootMotion.FinalIK.IKEffector GetEffector(UnityEngine.Transform t)
  RVA=0x0AC05D98  token=0x60003BB  RootMotion.FinalIK.FBIKChain GetChain(UnityEngine.Transform transform)
  RVA=0x0AC05C94  token=0x60003BC  System.Int32 GetChainIndex(UnityEngine.Transform transform)
  RVA=0x0AC05E98  token=0x60003BD  RootMotion.FinalIK.IKSolver.Node GetNode(System.Int32 chainIndex, System.Int32 nodeIndex)
  RVA=0x0AC05C18  token=0x60003BE  System.Void GetChainAndNodeIndexes(UnityEngine.Transform transform, System.Int32& chainIndex, System.Int32& nodeIndex)
  RVA=0x0AC06020  token=0x60003BF  RootMotion.FinalIK.IKSolver.Point[] GetPoints()
  RVA=0x0AC05EE0  token=0x60003C0  RootMotion.FinalIK.IKSolver.Point GetPoint(UnityEngine.Transform transform)
  RVA=0x0AC0616C  token=0x60003C1  System.Boolean IsValid(System.String& message)
  RVA=0x0AC06AE4  token=0x60003C2  System.Void StoreDefaultLocalState()
  RVA=0x0AC05B50  token=0x60003C3  System.Void FixTransforms()
  RVA=0x0AC0631C  token=0x60003C4  System.Void OnInitiate()
  RVA=0x0AC06428  token=0x60003C5  System.Void OnUpdate()
  RVA=0x0AC06588  token=0x60003C6  System.Void ReadPose()
  RVA=0x0AC067B0  token=0x60003C7  System.Void Solve()
  RVA=0x0AC05B14  token=0x60003C8  System.Void ApplyBendConstraints()
  RVA=0x0AC06B98  token=0x60003C9  System.Void WritePose()
  RVA=0x0AC06C58  token=0x60003CA  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.FullBodyBipedEffector
TYPE:  sealed struct
TOKEN: 0x2000074
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.FullBodyBipedEffectorBody  // const
  public    static  RootMotion.FinalIK.FullBodyBipedEffectorLeftShoulder  // const
  public    static  RootMotion.FinalIK.FullBodyBipedEffectorRightShoulder  // const
  public    static  RootMotion.FinalIK.FullBodyBipedEffectorLeftThigh  // const
  public    static  RootMotion.FinalIK.FullBodyBipedEffectorRightThigh  // const
  public    static  RootMotion.FinalIK.FullBodyBipedEffectorLeftHand  // const
  public    static  RootMotion.FinalIK.FullBodyBipedEffectorRightHand  // const
  public    static  RootMotion.FinalIK.FullBodyBipedEffectorLeftFoot  // const
  public    static  RootMotion.FinalIK.FullBodyBipedEffectorRightFoot  // const
METHODS:
END_CLASS

CLASS: RootMotion.FinalIK.FullBodyBipedChain
TYPE:  sealed struct
TOKEN: 0x2000075
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RootMotion.FinalIK.FullBodyBipedChainLeftArm  // const
  public    static  RootMotion.FinalIK.FullBodyBipedChainRightArm  // const
  public    static  RootMotion.FinalIK.FullBodyBipedChainLeftLeg  // const
  public    static  RootMotion.FinalIK.FullBodyBipedChainRightLeg  // const
METHODS:
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverFullBodyBiped
TYPE:  class
TOKEN: 0x2000076
SIZE:  0x100
EXTENDS: RootMotion.FinalIK.IKSolverFullBody
FIELDS:
  public            UnityEngine.Transform           rootNode  // 0xd0
  public            System.Single                   spineStiffness  // 0xd8
  public            System.Single                   pullBodyVertical  // 0xdc
  public            System.Single                   pullBodyHorizontal  // 0xe0
  private           UnityEngine.Vector3             <pullBodyOffset>k__BackingField  // 0xe4
  private           UnityEngine.Vector3             offset  // 0xf0
PROPERTIES:
  bodyEffector  get=0x0AC05938
  leftShoulderEffector  get=0x0AC05A0C
  rightShoulderEffector  get=0x0AC05AF4
  leftThighEffector  get=0x0AC05A1C
  rightThighEffector  get=0x0AC05B04
  leftHandEffector  get=0x0AC059A8
  rightHandEffector  get=0x0AC05A90
  leftFootEffector  get=0x0AC05998
  rightFootEffector  get=0x0AC05A80
  leftArmChain  get=0x0AC05944
  rightArmChain  get=0x0AC05A2C
  leftLegChain  get=0x0AC059B8
  rightLegChain  get=0x0AC05AA0
  leftArmMapping  get=0x0AC0596C
  rightArmMapping  get=0x0AC05A54
  leftLegMapping  get=0x0AC059E0
  rightLegMapping  get=0x0AC05AC8
  headMapping  get=0x0AC039E4
  pullBodyOffset  get=0x04DA96A0  set=0x04DA96C0
METHODS:
  RVA=0x0AC040E4  token=0x60003DD  System.Void SetChainWeights(RootMotion.FinalIK.FullBodyBipedChain c, System.Single pull, System.Single reach)
  RVA=0x0AC04148  token=0x60003DE  System.Void SetEffectorWeights(RootMotion.FinalIK.FullBodyBipedEffector effector, System.Single positionWeight, System.Single rotationWeight)
  RVA=0x0AC03544  token=0x60003DF  RootMotion.FinalIK.FBIKChain GetChain(RootMotion.FinalIK.FullBodyBipedChain c)
  RVA=0x0AC035C4  token=0x60003E0  RootMotion.FinalIK.FBIKChain GetChain(RootMotion.FinalIK.FullBodyBipedEffector effector)
  RVA=0x0AC03674  token=0x60003E1  RootMotion.FinalIK.IKEffector GetEffector(RootMotion.FinalIK.FullBodyBipedEffector effector)
  RVA=0x0AC037AC  token=0x60003E2  RootMotion.FinalIK.IKEffector GetEndEffector(RootMotion.FinalIK.FullBodyBipedChain c)
  RVA=0x0AC03AAC  token=0x60003E3  RootMotion.FinalIK.IKMappingLimb GetLimbMapping(RootMotion.FinalIK.FullBodyBipedChain chain)
  RVA=0x0AC03B38  token=0x60003E4  RootMotion.FinalIK.IKMappingLimb GetLimbMapping(RootMotion.FinalIK.FullBodyBipedEffector effector)
  RVA=0x04D862B0  token=0x60003E5  RootMotion.FinalIK.IKMappingSpine GetSpineMapping()
  RVA=0x0AC039E4  token=0x60003E6  RootMotion.FinalIK.IKMappingBone GetHeadMapping()
  RVA=0x0AC032A4  token=0x60003E7  RootMotion.FinalIK.IKConstraintBend GetBendConstraint(RootMotion.FinalIK.FullBodyBipedChain limb)
  RVA=0x0AC03C78  token=0x60003E8  System.Boolean IsValid(System.String& message)
  RVA=0x0AC04494  token=0x60003E9  System.Void SetToReferences(RootMotion.BipedReferences references, UnityEngine.Transform rootNode)
  RVA=0x0AC02F28  token=0x60003EA  UnityEngine.Transform DetectRootNodeBone(RootMotion.BipedReferences references)
  RVA=0x0AC04428  token=0x60003EB  System.Void SetLimbOrientations(RootMotion.BipedLimbOrientations o)
  RVA=0x0AC041FC  token=0x60003EE  System.Void SetLimbOrientation(RootMotion.FinalIK.FullBodyBipedChain chain, RootMotion.BipedLimbOrientations.LimbOrientation limbOrientation)
  RVA=0x0AC03A0C  token=0x60003EF  UnityEngine.Transform GetLeftClavicle(RootMotion.BipedReferences references)
  RVA=0x0AC03BD8  token=0x60003F0  UnityEngine.Transform GetRightClavicle(RootMotion.BipedReferences references)
  RVA=0x0AC02E90  token=0x60003F1  System.Boolean Contains(UnityEngine.Transform[] array, UnityEngine.Transform transform)
  RVA=0x0AC03FE0  token=0x60003F2  System.Void ReadPose()
  RVA=0x0AC03E0C  token=0x60003F3  System.Void PullBody()
  RVA=0x0AC0332C  token=0x60003F4  UnityEngine.Vector3 GetBodyOffset()
  RVA=0x0AC0382C  token=0x60003F5  UnityEngine.Vector3 GetHandBodyPull(RootMotion.FinalIK.IKEffector effector, RootMotion.FinalIK.FBIKChain arm, UnityEngine.Vector3 offset)
  RVA=0x0AC02B40  token=0x60003F6  System.Void ApplyBendConstraints()
  RVA=0x0AC05848  token=0x60003F7  System.Void WritePose()
  RVA=0x0AC0591C  token=0x60003F8  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverHeuristic
TYPE:  class
TOKEN: 0x2000077
SIZE:  0x88
EXTENDS: RootMotion.FinalIK.IKSolver
FIELDS:
  public            UnityEngine.Transform           target  // 0x58
  public            System.Single                   tolerance  // 0x60
  public            System.Int32                    maxIterations  // 0x64
  public            System.Boolean                  useRotationLimits  // 0x68
  public            System.Boolean                  XY  // 0x69
  public            RootMotion.FinalIK.IKSolver.Bone[]bones  // 0x70
  protected         UnityEngine.Vector3             lastLocalDirection  // 0x78
  protected         System.Single                   chainLength  // 0x84
PROPERTIES:
  minBones  get=0x02BBE540
  boneLengthCanBeZero  get=0x02FFF600
  allowCommonParent  get=0x012081B0
  localDirection  get=0x0AC07578
  positionOffset  get=0x0AC076C4
METHODS:
  RVA=0x039DED80  token=0x60003F9  System.Boolean SetChain(UnityEngine.Transform[] hierarchy, UnityEngine.Transform root)
  RVA=0x0AC06D78  token=0x60003FA  System.Void AddBone(UnityEngine.Transform bone)
  RVA=0x039DEE90  token=0x60003FB  System.Void StoreDefaultLocalState()
  RVA=0x0AC06E7C  token=0x60003FC  System.Void FixTransforms()
  RVA=0x039DEF70  token=0x60003FD  System.Boolean IsValid(System.String& message)
  RVA=0x04D862B0  token=0x60003FE  RootMotion.FinalIK.IKSolver.Point[] GetPoints()
  RVA=0x0AC06EDC  token=0x60003FF  RootMotion.FinalIK.IKSolver.Point GetPoint(UnityEngine.Transform transform)
  RVA=0x041E1670  token=0x6000403  System.Void OnInitiate()
  RVA=0x041E1670  token=0x6000404  System.Void OnUpdate()
  RVA=0x039DF680  token=0x6000405  System.Void InitiateBones()
  RVA=0x0AC06F98  token=0x6000408  UnityEngine.Vector3 GetSingularityOffset()
  RVA=0x0AC072E8  token=0x6000409  System.Boolean SingularityDetected()
  RVA=0x039DDC40  token=0x600040A  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverLeg
TYPE:  class
TOKEN: 0x2000078
SIZE:  0xC0
EXTENDS: RootMotion.FinalIK.IKSolver
FIELDS:
  public            System.Single                   IKRotationWeight  // 0x58
  public            UnityEngine.Quaternion          IKRotation  // 0x5c
  public            RootMotion.FinalIK.IKSolver.Pointpelvis  // 0x70
  public            RootMotion.FinalIK.IKSolver.Pointthigh  // 0x78
  public            RootMotion.FinalIK.IKSolver.Pointcalf  // 0x80
  public            RootMotion.FinalIK.IKSolver.Pointfoot  // 0x88
  public            RootMotion.FinalIK.IKSolver.Pointtoe  // 0x90
  public            RootMotion.FinalIK.IKSolverVR.Legleg  // 0x98
  public            UnityEngine.Vector3             heelOffset  // 0xa0
  private           UnityEngine.Vector3[]           positions  // 0xb0
  private           UnityEngine.Quaternion[]        rotations  // 0xb8
METHODS:
  RVA=0x0AC079AC  token=0x600040B  System.Boolean IsValid(System.String& message)
  RVA=0x0ABFCBC0  token=0x600040C  System.Boolean SetChain(UnityEngine.Transform pelvis, UnityEngine.Transform thigh, UnityEngine.Transform calf, UnityEngine.Transform foot, UnityEngine.Transform toe, UnityEngine.Transform root)
  RVA=0x0AC078A8  token=0x600040D  RootMotion.FinalIK.IKSolver.Point[] GetPoints()
  RVA=0x0AC0773C  token=0x600040E  RootMotion.FinalIK.IKSolver.Point GetPoint(UnityEngine.Transform transform)
  RVA=0x0ABFCCF0  token=0x600040F  System.Void StoreDefaultLocalState()
  RVA=0x0ABFC084  token=0x6000410  System.Void FixTransforms()
  RVA=0x0AC07CA8  token=0x6000411  System.Void OnInitiate()
  RVA=0x0AC07D24  token=0x6000412  System.Void OnUpdate()
  RVA=0x0AC08210  token=0x6000413  System.Void Solve()
  RVA=0x0AC07D50  token=0x6000414  System.Void Read()
  RVA=0x0AC082FC  token=0x6000415  System.Void Write()
  RVA=0x0AC0854C  token=0x6000416  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverLimb
TYPE:  class
TOKEN: 0x2000079
SIZE:  0x140
EXTENDS: RootMotion.FinalIK.IKSolverTrigonometric
FIELDS:
  public            UnityEngine.AvatarIKGoal        goal  // 0xa8
  public            RootMotion.FinalIK.IKSolverLimb.BendModifierbendModifier  // 0xac
  public            System.Single                   maintainRotationWeight  // 0xb0
  public            System.Single                   bendModifierWeight  // 0xb4
  public            UnityEngine.Transform           bendGoal  // 0xb8
  private           System.Boolean                  maintainBendFor1Frame  // 0xc0
  private           System.Boolean                  maintainRotationFor1Frame  // 0xc1
  private           UnityEngine.Quaternion          defaultRootRotation  // 0xc4
  private           UnityEngine.Quaternion          parentDefaultRotation  // 0xd4
  private           UnityEngine.Quaternion          bone3RotationBeforeSolve  // 0xe4
  private           UnityEngine.Quaternion          maintainRotation  // 0xf4
  private           UnityEngine.Quaternion          bone3DefaultRotation  // 0x104
  private           UnityEngine.Vector3             _bendNormal  // 0x114
  private           UnityEngine.Vector3             animationNormal  // 0x120
  private           RootMotion.FinalIK.IKSolverLimb.AxisDirection[]axisDirectionsLeft  // 0x130
  private           RootMotion.FinalIK.IKSolverLimb.AxisDirection[]axisDirectionsRight  // 0x138
PROPERTIES:
  axisDirections  get=0x0AC087CC
METHODS:
  RVA=0x0326ACB0  token=0x6000417  System.Void MaintainRotation()
  RVA=0x0326AD20  token=0x6000418  System.Void MaintainBend()
  RVA=0x033B26B0  token=0x6000419  System.Void OnInitiateVirtual()
  RVA=0x03A6DCD0  token=0x600041A  System.Void OnUpdateVirtual()
  RVA=0x03C2DF70  token=0x600041B  System.Void OnPostSolveVirtual()
  RVA=0x0AC08744  token=0x600041C  System.Void .ctor()
  RVA=0x039DD990  token=0x600041D  System.Void .ctor(UnityEngine.AvatarIKGoal goal)
  RVA=0x033B3830  token=0x600041F  System.Void StoreAxisDirections(RootMotion.FinalIK.IKSolverLimb.AxisDirection[]& axisDirections)
  RVA=0x03A6DDA0  token=0x6000420  UnityEngine.Vector3 GetModifiedBendNormal()
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverLookAt
TYPE:  class
TOKEN: 0x200007C
SIZE:  0xF8
EXTENDS: RootMotion.FinalIK.IKSolver
FIELDS:
  public            UnityEngine.Transform           target  // 0x58
  public            RootMotion.FinalIK.IKSolverLookAt.LookAtBone[]spine  // 0x60
  private           UnityEngine.Quaternion[]        m_spineVirtualBones  // 0x68
  public            RootMotion.FinalIK.IKSolverLookAt.LookAtBonehead  // 0x70
  private           UnityEngine.Quaternion          m_headVirtualBones  // 0x78
  public            RootMotion.FinalIK.IKSolverLookAt.LookAtBone[]eyes  // 0x88
  private           UnityEngine.Quaternion          m_eyeVirtualBone  // 0x90
  public            System.Single                   bodyWeight  // 0xa0
  public            System.Single                   headWeight  // 0xa4
  public            System.Single                   eyesWeight  // 0xa8
  public            System.Single                   clampWeight  // 0xac
  public            System.Single                   clampWeightHead  // 0xb0
  public            System.Single                   clampWeightEyes  // 0xb4
  public            System.Int32                    clampSmoothing  // 0xb8
  public            UnityEngine.AnimationCurve      spineWeightCurve  // 0xc0
  public            UnityEngine.Vector3             spineTargetOffset  // 0xc8
  protected         UnityEngine.Vector3[]           spineForwards  // 0xd8
  protected         UnityEngine.Vector3[]           headForwards  // 0xe0
  protected         UnityEngine.Vector3[]           eyeForward  // 0xe8
  private           System.Boolean                  isDirty  // 0xf0
PROPERTIES:
  spineVirtualBone  get=0x0AC08D98  set=0x0485F170
  headVirtualBones  get=0x04D8E8F0  set=0x04DA96F0
  eyeVirtualBone  get=0x04D9DC90  set=0x04DA12D0
  spineIsValid  get=0x039E00C0
  spineIsEmpty  get=0x03270BE0
  headIsValid  get=0x039E0040
  headIsEmpty  get=0x03270B60
  eyesIsValid  get=0x039E0050
  eyesIsEmpty  get=0x03270B30
METHODS:
  RVA=0x0ABFD574  token=0x6000428  System.Void SetLookAtWeight(System.Single weight)
  RVA=0x0AC08AF4  token=0x6000429  System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight)
  RVA=0x0AC08BE4  token=0x600042A  System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight, System.Single headWeight)
  RVA=0x0AC08D20  token=0x600042B  System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight, System.Single headWeight, System.Single eyesWeight)
  RVA=0x0AC08B3C  token=0x600042C  System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight, System.Single headWeight, System.Single eyesWeight, System.Single clampWeight)
  RVA=0x0AC08C48  token=0x600042D  System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight, System.Single headWeight, System.Single eyesWeight, System.Single clampWeight, System.Single clampWeightHead, System.Single clampWeightEyes)
  RVA=0x039DE920  token=0x600042E  System.Void StoreDefaultLocalState()
  RVA=0x04DA96E0  token=0x600042F  System.Void SetDirty()
  RVA=0x03E998B0  token=0x6000430  System.Void FixTransforms()
  RVA=0x039DE590  token=0x6000431  System.Boolean IsValid(System.String& message)
  RVA=0x0AC08920  token=0x6000432  RootMotion.FinalIK.IKSolver.Point[] GetPoints()
  RVA=0x0AC087E8  token=0x6000433  RootMotion.FinalIK.IKSolver.Point GetPoint(UnityEngine.Transform transform)
  RVA=0x039DD470  token=0x6000434  System.Boolean SetChain(UnityEngine.Transform[] spine, UnityEngine.Transform head, UnityEngine.Transform[] eyes, UnityEngine.Transform root)
  RVA=0x043C8A50  token=0x6000435  System.Void OnInitiate()
  RVA=0x032709C0  token=0x6000436  System.Void OnUpdate()
  RVA=0x032724E0  token=0x6000439  System.Void SolveSpine()
  RVA=0x032713B0  token=0x600043C  System.Void SolveHead()
  RVA=0x03270A80  token=0x600043F  System.Void SolveEyes()
  RVA=0x03251E90  token=0x6000440  UnityEngine.Vector3[] GetForwards(UnityEngine.Vector3[]& forwards, UnityEngine.Vector3 baseForward, UnityEngine.Vector3 targetForward, System.Int32 bones, System.Single clamp)
  RVA=0x039DD540  token=0x6000441  System.Void SetBones(UnityEngine.Transform[] array, RootMotion.FinalIK.IKSolverLookAt.LookAtBone[]& bones)
  RVA=0x039DDCA0  token=0x6000442  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverTrigonometric
TYPE:  class
TOKEN: 0x200007E
SIZE:  0xA8
EXTENDS: RootMotion.FinalIK.IKSolver
FIELDS:
  public            UnityEngine.Transform           target  // 0x58
  public            System.Single                   IKRotationWeight  // 0x60
  public            UnityEngine.Quaternion          IKRotation  // 0x64
  public            UnityEngine.Vector3             bendNormal  // 0x74
  public            RootMotion.FinalIK.IKSolverTrigonometric.TrigonometricBonebone1  // 0x80
  public            RootMotion.FinalIK.IKSolverTrigonometric.TrigonometricBonebone2  // 0x88
  public            RootMotion.FinalIK.IKSolverTrigonometric.TrigonometricBonebone3  // 0x90
  protected         UnityEngine.Vector3             weightIKPosition  // 0x98
  protected         System.Boolean                  directHierarchy  // 0xa4
METHODS:
  RVA=0x0AC091E4  token=0x6000449  System.Void SetBendGoalPosition(UnityEngine.Vector3 goalPosition, System.Single weight)
  RVA=0x04128BE0  token=0x600044A  System.Void SetBendPlaneToCurrent()
  RVA=0x04DA9710  token=0x600044B  System.Void SetIKRotation(UnityEngine.Quaternion rotation)
  RVA=0x0AC093F0  token=0x600044C  System.Void SetIKRotationWeight(System.Single weight)
  RVA=0x04DA9700  token=0x600044D  UnityEngine.Quaternion GetIKRotation()
  RVA=0x04D88480  token=0x600044E  System.Single GetIKRotationWeight()
  RVA=0x0AC09120  token=0x600044F  RootMotion.FinalIK.IKSolver.Point[] GetPoints()
  RVA=0x0AC09028  token=0x6000450  RootMotion.FinalIK.IKSolver.Point GetPoint(UnityEngine.Transform transform)
  RVA=0x039DFD10  token=0x6000451  System.Void StoreDefaultLocalState()
  RVA=0x02FA5DE0  token=0x6000452  System.Void FixTransforms()
  RVA=0x035B22D0  token=0x6000453  System.Boolean IsValid(System.String& message)
  RVA=0x039DE1F0  token=0x6000454  System.Boolean SetChain(UnityEngine.Transform bone1, UnityEngine.Transform bone2, UnityEngine.Transform bone3, UnityEngine.Transform root)
  RVA=0x0AC09414  token=0x6000455  System.Void Solve(UnityEngine.Transform bone1, UnityEngine.Transform bone2, UnityEngine.Transform bone3, UnityEngine.Vector3 targetPosition, UnityEngine.Vector3 bendNormal, System.Single weight)
  RVA=0x0AC08E6C  token=0x6000456  UnityEngine.Vector3 GetDirectionToBendPoint(UnityEngine.Vector3 direction, System.Single directionMag, UnityEngine.Vector3 bendDirection, System.Single sqrMag1, System.Single sqrMag2)
  RVA=0x04128750  token=0x6000457  System.Void OnInitiate()
  RVA=0x04128C10  token=0x6000458  System.Boolean IsDirectHierarchy()
  RVA=0x04128840  token=0x6000459  System.Void InitiateBones()
  RVA=0x032759E0  token=0x600045A  System.Void OnUpdate()
  RVA=0x041E1670  token=0x600045B  System.Void OnInitiateVirtual()
  RVA=0x041E1670  token=0x600045C  System.Void OnUpdateVirtual()
  RVA=0x041E1670  token=0x600045D  System.Void OnPostSolveVirtual()
  RVA=0x03270E10  token=0x600045E  UnityEngine.Vector3 GetBendDirection(UnityEngine.Vector3 IKPosition, UnityEngine.Vector3 bendNormal)
  RVA=0x039DDA30  token=0x600045F  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.IKSolverVR
TYPE:  class
TOKEN: 0x2000080
SIZE:  0x188
EXTENDS: RootMotion.FinalIK.IKSolver
FIELDS:
  private           UnityEngine.Animator            <animator>k__BackingField  // 0x58
  private           UnityEngine.Transform[]         solverTransforms  // 0x60
  private           System.Boolean                  hasChest  // 0x68
  private           System.Boolean                  hasNeck  // 0x69
  private           System.Boolean                  hasShoulders  // 0x6a
  private           System.Boolean                  hasToes  // 0x6b
  private           System.Boolean                  hasLegs  // 0x6c
  private           System.Boolean                  hasArms  // 0x6d
  private           UnityEngine.Vector3[]           readPositions  // 0x70
  private           UnityEngine.Quaternion[]        readRotations  // 0x78
  private           UnityEngine.Vector3[]           solvedPositions  // 0x80
  private           UnityEngine.Quaternion[]        solvedRotations  // 0x88
  private           UnityEngine.Quaternion[]        defaultLocalRotations  // 0x90
  private           UnityEngine.Vector3[]           defaultLocalPositions  // 0x98
  private           UnityEngine.Vector3             rootV  // 0xa0
  private           UnityEngine.Vector3             rootVelocity  // 0xac
  private           UnityEngine.Vector3             bodyOffset  // 0xb8
  private           System.Int32                    supportLegIndex  // 0xc4
  private           System.Int32                    lastLOD  // 0xc8
  private           System.Single                   lastLocomotionWeight  // 0xcc
  public            System.Int32                    LOD  // 0xd0
  public            System.Single                   scale  // 0xd4
  public            System.Boolean                  plantFeet  // 0xd8
  private           RootMotion.FinalIK.IKSolverVR.VirtualBone<rootBone>k__BackingField  // 0xe0
  public            RootMotion.FinalIK.IKSolverVR.Spinespine  // 0xe8
  public            RootMotion.FinalIK.IKSolverVR.ArmleftArm  // 0xf0
  public            RootMotion.FinalIK.IKSolverVR.ArmrightArm  // 0xf8
  public            RootMotion.FinalIK.IKSolverVR.LegleftLeg  // 0x100
  public            RootMotion.FinalIK.IKSolverVR.LegrightLeg  // 0x108
  public            RootMotion.FinalIK.IKSolverVR.Locomotionlocomotion  // 0x110
  private           RootMotion.FinalIK.IKSolverVR.Leg[]legs  // 0x118
  private           RootMotion.FinalIK.IKSolverVR.Arm[]arms  // 0x120
  private           UnityEngine.Vector3             headPosition  // 0x128
  private           UnityEngine.Vector3             headDeltaPosition  // 0x134
  private           UnityEngine.Vector3             raycastOriginPelvis  // 0x140
  private           UnityEngine.Vector3             lastOffset  // 0x14c
  private           UnityEngine.Vector3             debugPos1  // 0x158
  private           UnityEngine.Vector3             debugPos2  // 0x164
  private           UnityEngine.Vector3             debugPos3  // 0x170
  private           UnityEngine.Vector3             debugPos4  // 0x17c
PROPERTIES:
  animator  get=0x02E56440  set=0x036E5590
  rootBone  get=0x04D8D1C0  set=0x06AC5B08
METHODS:
  RVA=0x0AC0BF8C  token=0x6000466  System.Void SetToReferences(RootMotion.FinalIK.VRIK.References references)
  RVA=0x0AC0AD88  token=0x6000467  System.Void GuessHandOrientations(RootMotion.FinalIK.VRIK.References references, System.Boolean onlyIfZero)
  RVA=0x0AC09E9C  token=0x6000468  System.Void DefaultAnimationCurves()
  RVA=0x0AC09A44  token=0x6000469  System.Void AddPositionOffset(RootMotion.FinalIK.IKSolverVR.PositionOffset positionOffset, UnityEngine.Vector3 value)
  RVA=0x0AC09D50  token=0x600046A  System.Void AddRotationOffset(RootMotion.FinalIK.IKSolverVR.RotationOffset rotationOffset, UnityEngine.Vector3 value)
  RVA=0x0AC09DA8  token=0x600046B  System.Void AddRotationOffset(RootMotion.FinalIK.IKSolverVR.RotationOffset rotationOffset, UnityEngine.Quaternion value)
  RVA=0x0AC098F8  token=0x600046C  System.Void AddPlatformMotion(UnityEngine.Vector3 deltaPosition, UnityEngine.Quaternion deltaRotation, UnityEngine.Vector3 platformPivot)
  RVA=0x0AC0BE54  token=0x600046D  System.Void Reset()
  RVA=0x0AC0DAB8  token=0x600046E  System.Void StoreDefaultLocalState()
  RVA=0x0AC0A054  token=0x600046F  System.Void FixTransforms()
  RVA=0x0AC0AC14  token=0x6000470  RootMotion.FinalIK.IKSolver.Point[] GetPoints()
  RVA=0x0AC0ABC4  token=0x6000471  RootMotion.FinalIK.IKSolver.Point GetPoint(UnityEngine.Transform transform)
  RVA=0x0AC0B0D4  token=0x6000472  System.Boolean IsValid(System.String& message)
  RVA=0x0AC0A228  token=0x6000473  UnityEngine.Vector3 GetNormal(UnityEngine.Transform[] transforms)
  RVA=0x0AC0ACC4  token=0x6000474  UnityEngine.Keyframe[] GetSineKeyframes(System.Single mag)
  RVA=0x0AC0DC10  token=0x6000475  System.Void UpdateSolverTransforms()
  RVA=0x0AC0B358  token=0x6000476  System.Void OnInitiate()
  RVA=0x0AC0B3AC  token=0x6000477  System.Void OnUpdate()
  RVA=0x0AC0DD54  token=0x6000478  System.Void WriteTransforms()
  RVA=0x0AC0B8EC  token=0x6000479  System.Void Read(UnityEngine.Vector3[] positions, UnityEngine.Quaternion[] rotations, System.Boolean hasChest, System.Boolean hasNeck, System.Boolean hasShoulders, System.Boolean hasToes, System.Boolean hasLegs, System.Boolean hasArms)
  RVA=0x0AC0C290  token=0x600047A  System.Void Solve()
  RVA=0x0AC0AC64  token=0x600047B  UnityEngine.Vector3 GetPosition(System.Int32 index)
  RVA=0x0AC0AC94  token=0x600047C  UnityEngine.Quaternion GetRotation(System.Int32 index)
  RVA=0x0AC0E22C  token=0x600047F  System.Void Write()
  RVA=0x0AC0A4DC  token=0x6000480  UnityEngine.Vector3 GetPelvisOffset(System.Single deltaTime)
  RVA=0x0AC0E38C  token=0x6000481  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.TwistRelaxer
TYPE:  class
TOKEN: 0x200008C
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            RootMotion.FinalIK.IK           ik  // 0x18
  public            RootMotion.FinalIK.TwistSolver[]twistSolvers  // 0x20
METHODS:
  RVA=0x0AC1F230  token=0x600050E  System.Void Start()
  RVA=0x0AC1F3E8  token=0x600050F  System.Void Update()
  RVA=0x0AC1F1A4  token=0x6000510  System.Void OnPostUpdate()
  RVA=0x0AC1EFEC  token=0x6000511  System.Void LateUpdate()
  RVA=0x0AC1F078  token=0x6000512  System.Void OnDestroy()
  RVA=0x0AC1F484  token=0x6000513  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.TwistSolver
TYPE:  class
TOKEN: 0x200008D
SIZE:  0x90
FIELDS:
  public            UnityEngine.Transform           transform  // 0x10
  public            UnityEngine.Transform           parent  // 0x18
  public            UnityEngine.Transform[]         children  // 0x20
  public            System.Single                   weight  // 0x28
  public            System.Single                   parentChildCrossfade  // 0x2c
  public            System.Single                   twistAngleOffset  // 0x30
  private           UnityEngine.Vector3             twistAxis  // 0x34
  private           UnityEngine.Vector3             axis  // 0x40
  private           UnityEngine.Vector3             axisRelativeToParentDefault  // 0x4c
  private           UnityEngine.Vector3             axisRelativeToChildDefault  // 0x58
  private           UnityEngine.Quaternion[]        childRotations  // 0x68
  private           System.Boolean                  inititated  // 0x70
  private           UnityEngine.Quaternion          defaultLocalRotation  // 0x74
  private           UnityEngine.Quaternion[]        defaultChildLocalRotations  // 0x88
METHODS:
  RVA=0x0AC1FFA8  token=0x6000514  System.Void .ctor()
  RVA=0x0AC1F588  token=0x6000515  System.Void Initiate()
  RVA=0x0AC1F4D4  token=0x6000516  System.Void FixTransforms()
  RVA=0x0AC1FB44  token=0x6000517  System.Void Relax()
END_CLASS

CLASS: RootMotion.FinalIK.InteractionEffector
TYPE:  class
TOKEN: 0x200008E
SIZE:  0xD0
FIELDS:
  private           RootMotion.FinalIK.FullBodyBipedEffector<effectorType>k__BackingField  // 0x10
  private           System.Boolean                  <isPaused>k__BackingField  // 0x14
  private           RootMotion.FinalIK.InteractionObject<interactionObject>k__BackingField  // 0x18
  private           RootMotion.FinalIK.Poser        poser  // 0x20
  private           RootMotion.FinalIK.IKEffector   effector  // 0x28
  private           System.Single                   timer  // 0x30
  private           System.Single                   length  // 0x34
  private           System.Single                   weight  // 0x38
  private           System.Single                   fadeInSpeed  // 0x3c
  private           System.Single                   defaultPositionWeight  // 0x40
  private           System.Single                   defaultRotationWeight  // 0x44
  private           System.Single                   defaultPull  // 0x48
  private           System.Single                   defaultReach  // 0x4c
  private           System.Single                   defaultPush  // 0x50
  private           System.Single                   defaultPushParent  // 0x54
  private           System.Single                   defaultBendGoalWeight  // 0x58
  private           System.Single                   resetTimer  // 0x5c
  private           System.Boolean                  positionWeightUsed  // 0x60
  private           System.Boolean                  rotationWeightUsed  // 0x61
  private           System.Boolean                  pullUsed  // 0x62
  private           System.Boolean                  reachUsed  // 0x63
  private           System.Boolean                  pushUsed  // 0x64
  private           System.Boolean                  pushParentUsed  // 0x65
  private           System.Boolean                  bendGoalWeightUsed  // 0x66
  private           System.Boolean                  pickedUp  // 0x67
  private           System.Boolean                  defaults  // 0x68
  private           System.Boolean                  pickUpOnPostFBBIK  // 0x69
  private           UnityEngine.Vector3             pickUpPosition  // 0x6c
  private           UnityEngine.Vector3             pausePositionRelative  // 0x78
  private           UnityEngine.Quaternion          pickUpRotation  // 0x84
  private           UnityEngine.Quaternion          pauseRotationRelative  // 0x94
  private           RootMotion.FinalIK.InteractionTargetinteractionTarget  // 0xa8
  private           UnityEngine.Transform           target  // 0xb0
  private           System.Collections.Generic.List<System.Boolean>triggered  // 0xb8
  private           RootMotion.FinalIK.InteractionSysteminteractionSystem  // 0xc0
  private           System.Boolean                  started  // 0xc8
PROPERTIES:
  effectorType  get=0x02B2E2D0  set=0x014F51F0
  isPaused  get=0x04D8EE50  set=0x04D8EE70
  interactionObject  get=0x0385B100  set=0x05392C40
  inInteraction  get=0x0AC149CC
  progress  get=0x0AC14A1C
METHODS:
  RVA=0x0AC14940  token=0x600051F  System.Void .ctor(RootMotion.FinalIK.FullBodyBipedEffector effectorType)
  RVA=0x0AC12BA0  token=0x6000520  System.Void Initiate(RootMotion.FinalIK.InteractionSystem interactionSystem)
  RVA=0x0AC13E0C  token=0x6000521  System.Void StoreDefaults()
  RVA=0x0AC13310  token=0x6000522  System.Boolean ResetToDefaults(System.Single speed, System.Single deltaTime)
  RVA=0x0AC12F44  token=0x6000523  System.Boolean Pause()
  RVA=0x0AC13780  token=0x6000524  System.Boolean Resume()
  RVA=0x0AC13BDC  token=0x6000525  System.Boolean Start(RootMotion.FinalIK.InteractionObject interactionObject, System.String tag, System.Single fadeInTime, System.Boolean interrupt)
  RVA=0x0AC137D4  token=0x6000526  System.Boolean Start(RootMotion.FinalIK.InteractionObject interactionObject, RootMotion.FinalIK.InteractionTarget interactionTarget, System.Single fadeInTime, System.Boolean interrupt)
  RVA=0x0AC142A4  token=0x6000527  System.Void Update(UnityEngine.Transform root, System.Single speed, System.Single deltaTime)
  RVA=0x0AC13FFC  token=0x6000529  System.Void TriggerUntriggeredEvents(System.Boolean checkTime, System.Boolean& pickUp, System.Boolean& pause)
  RVA=0x0AC13064  token=0x600052A  System.Void PickUp(UnityEngine.Transform root)
  RVA=0x0AC13CE4  token=0x600052B  System.Boolean Stop()
  RVA=0x0AC12C58  token=0x600052C  System.Void OnPostFBBIK()
END_CLASS

CLASS: RootMotion.FinalIK.InteractionLookAt
TYPE:  class
TOKEN: 0x200008F
SIZE:  0x40
FIELDS:
  public            RootMotion.FinalIK.LookAtIK     ik  // 0x10
  public            System.Single                   lerpSpeed  // 0x18
  public            System.Single                   weightSpeed  // 0x1c
  public            System.Boolean                  isPaused  // 0x20
  private           UnityEngine.Transform           lookAtTarget  // 0x28
  private           System.Single                   stopLookTime  // 0x30
  private           System.Single                   weight  // 0x34
  private           System.Boolean                  firstFBBIKSolve  // 0x38
METHODS:
  RVA=0x0AC27558  token=0x600052D  System.Void Look(UnityEngine.Transform target, System.Single time)
  RVA=0x0AC276E4  token=0x600052E  System.Void OnFixTransforms()
  RVA=0x0AC2793C  token=0x600052F  System.Void Update()
  RVA=0x0AC27828  token=0x6000530  System.Void SolveSpine()
  RVA=0x0AC27764  token=0x6000531  System.Void SolveHead()
  RVA=0x04DA99B0  token=0x6000532  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.InteractionObject
TYPE:  class
TOKEN: 0x2000090
SIZE:  0x60
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.Transform           otherLookAtTarget  // 0x18
  public            UnityEngine.Transform           otherTargetsRoot  // 0x20
  public            UnityEngine.Transform           positionOffsetSpace  // 0x28
  public            RootMotion.FinalIK.InteractionObject.WeightCurve[]weightCurves  // 0x30
  public            RootMotion.FinalIK.InteractionObject.Multiplier[]multipliers  // 0x38
  public            RootMotion.FinalIK.InteractionObject.InteractionEvent[]events  // 0x40
  private           System.Single                   <length>k__BackingField  // 0x48
  private           RootMotion.FinalIK.InteractionSystem<lastUsedInteractionSystem>k__BackingField  // 0x50
  private           RootMotion.FinalIK.InteractionTarget[]targets  // 0x58
PROPERTIES:
  length  get=0x04D8D350  set=0x04D8D380
  lastUsedInteractionSystem  get=0x04D86270  set=0x02FDAEA0
  lookAtTarget  get=0x0AC28EE4
  targetsRoot  get=0x0AC28F4C
METHODS:
  RVA=0x0AC28E24  token=0x6000533  System.Void OpenUserManual()
  RVA=0x0AC28D20  token=0x6000534  System.Void OpenScriptReference()
  RVA=0x0AC28D54  token=0x6000535  System.Void OpenTutorial1()
  RVA=0x0AC28D88  token=0x6000536  System.Void OpenTutorial2()
  RVA=0x0AC28DBC  token=0x6000537  System.Void OpenTutorial3()
  RVA=0x0AC28DF0  token=0x6000538  System.Void OpenTutorial4()
  RVA=0x0AC28E60  token=0x6000539  System.Void SupportGroup()
  RVA=0x0AC27B60  token=0x600053A  System.Void ASThread()
  RVA=0x0AC28B6C  token=0x600053F  System.Void Initiate()
  RVA=0x0AC28774  token=0x6000541  RootMotion.FinalIK.InteractionTarget GetTarget(RootMotion.FinalIK.FullBodyBipedEffector effectorType, RootMotion.FinalIK.InteractionSystem interactionSystem)
  RVA=0x0AC28590  token=0x6000542  System.Boolean CurveUsed(RootMotion.FinalIK.InteractionObject.WeightCurve.Type type)
  RVA=0x02E56440  token=0x6000543  RootMotion.FinalIK.InteractionTarget[] GetTargets()
  RVA=0x0AC2865C  token=0x6000544  UnityEngine.Transform GetTarget(RootMotion.FinalIK.FullBodyBipedEffector effectorType, System.String tag)
  RVA=0x02FDAEA0  token=0x6000545  System.Void OnStartInteraction(RootMotion.FinalIK.InteractionSystem interactionSystem)
  RVA=0x0AC27B94  token=0x6000546  System.Void Apply(RootMotion.FinalIK.IKSolverFullBodyBiped solver, RootMotion.FinalIK.FullBodyBipedEffector effector, RootMotion.FinalIK.InteractionTarget target, System.Single timer, System.Single weight)
  RVA=0x0AC28908  token=0x6000547  System.Single GetValue(RootMotion.FinalIK.InteractionObject.WeightCurve.Type weightCurveType, RootMotion.FinalIK.InteractionTarget target, System.Single timer)
  RVA=0x0AC28E58  token=0x6000549  System.Void Start()
  RVA=0x0AC27FF4  token=0x600054A  System.Void Apply(RootMotion.FinalIK.IKSolverFullBodyBiped solver, RootMotion.FinalIK.FullBodyBipedEffector effector, RootMotion.FinalIK.InteractionObject.WeightCurve.Type type, System.Single value, System.Single weight)
  RVA=0x0AC288A8  token=0x600054B  UnityEngine.Transform GetTarget(RootMotion.FinalIK.FullBodyBipedEffector effectorType)
  RVA=0x0AC28B18  token=0x600054C  System.Int32 GetWeightCurveIndex(RootMotion.FinalIK.InteractionObject.WeightCurve.Type weightCurveType)
  RVA=0x0AC28608  token=0x600054D  System.Int32 GetMultiplierIndex(RootMotion.FinalIK.InteractionObject.WeightCurve.Type weightCurveType)
  RVA=0x0AC28E94  token=0x600054E  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.InteractionSystem
TYPE:  class
TOKEN: 0x2000097
SIZE:  0x108
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.String                   targetTag  // 0x18
  public            System.Single                   fadeInTime  // 0x20
  public            System.Single                   speed  // 0x24
  public            System.Single                   resetToDefaultsSpeed  // 0x28
  public            UnityEngine.Collider            characterCollider  // 0x30
  public            UnityEngine.Transform           FPSCamera  // 0x38
  public            UnityEngine.LayerMask           camRaycastLayers  // 0x40
  public            System.Single                   camRaycastDistance  // 0x44
  private           System.Collections.Generic.List<RootMotion.FinalIK.InteractionTrigger><triggersInRange>k__BackingField  // 0x48
  private           System.Collections.Generic.List<RootMotion.FinalIK.InteractionTrigger>inContact  // 0x50
  private           System.Collections.Generic.List<System.Int32>bestRangeIndexes  // 0x58
  public            RootMotion.FinalIK.InteractionSystem.InteractionDelegateOnInteractionStart  // 0x60
  public            RootMotion.FinalIK.InteractionSystem.InteractionDelegateOnInteractionPause  // 0x68
  public            RootMotion.FinalIK.InteractionSystem.InteractionDelegateOnInteractionPickUp  // 0x70
  public            RootMotion.FinalIK.InteractionSystem.InteractionDelegateOnInteractionResume  // 0x78
  public            RootMotion.FinalIK.InteractionSystem.InteractionDelegateOnInteractionStop  // 0x80
  public            RootMotion.FinalIK.InteractionSystem.InteractionEventDelegateOnInteractionEvent  // 0x88
  public            UnityEngine.RaycastHit          raycastHit  // 0x90
  private           RootMotion.FinalIK.FullBodyBipedIKfullBody  // 0xd0
  public            RootMotion.FinalIK.InteractionLookAtlookAt  // 0xd8
  private           RootMotion.FinalIK.InteractionEffector[]interactionEffectors  // 0xe0
  private           System.Boolean                  <initiated>k__BackingField  // 0xe8
  private           UnityEngine.Collider            lastCollider  // 0xf0
  private           UnityEngine.Collider            c  // 0xf8
  private           System.Single                   lastTime  // 0x100
PROPERTIES:
  inInteraction  get=0x0AC2C740
  ik  get=0x04D876B0  set=0x0678D7BC
  triggersInRange  get=0x04D86200  set=0x035A41A0
  initiated  get=0x04D8C8C0  set=0x04D8C8F0
METHODS:
  RVA=0x0AC2A858  token=0x600055B  System.Void OpenUserManual()
  RVA=0x0AC2A754  token=0x600055C  System.Void OpenScriptReference()
  RVA=0x0AC2A788  token=0x600055D  System.Void OpenTutorial1()
  RVA=0x0AC2A7BC  token=0x600055E  System.Void OpenTutorial2()
  RVA=0x0AC2A7F0  token=0x600055F  System.Void OpenTutorial3()
  RVA=0x0AC2A824  token=0x6000560  System.Void OpenTutorial4()
  RVA=0x0AC2B428  token=0x6000561  System.Void SupportGroup()
  RVA=0x0AC28FB4  token=0x6000562  System.Void ASThread()
  RVA=0x0AC29A44  token=0x6000564  System.Boolean IsInInteraction(RootMotion.FinalIK.FullBodyBipedEffector effectorType)
  RVA=0x0AC29C64  token=0x6000565  System.Boolean IsPaused(RootMotion.FinalIK.FullBodyBipedEffector effectorType)
  RVA=0x0AC29BD4  token=0x6000566  System.Boolean IsPaused()
  RVA=0x0AC29AF4  token=0x6000567  System.Boolean IsInSync()
  RVA=0x0AC2AC20  token=0x6000568  System.Boolean StartInteraction(RootMotion.FinalIK.FullBodyBipedEffector effectorType, RootMotion.FinalIK.InteractionObject interactionObject, System.Boolean interrupt)
  RVA=0x0AC2AB34  token=0x6000569  System.Boolean StartInteraction(RootMotion.FinalIK.FullBodyBipedEffector effectorType, RootMotion.FinalIK.InteractionObject interactionObject, RootMotion.FinalIK.InteractionTarget target, System.Boolean interrupt)
  RVA=0x0AC2A8F0  token=0x600056A  System.Boolean PauseInteraction(RootMotion.FinalIK.FullBodyBipedEffector effectorType)
  RVA=0x0AC2AAB8  token=0x600056B  System.Boolean ResumeInteraction(RootMotion.FinalIK.FullBodyBipedEffector effectorType)
  RVA=0x0AC2B3AC  token=0x600056C  System.Boolean StopInteraction(RootMotion.FinalIK.FullBodyBipedEffector effectorType)
  RVA=0x0AC2A88C  token=0x600056D  System.Void PauseAll()
  RVA=0x0AC2AA54  token=0x600056E  System.Void ResumeAll()
  RVA=0x0AC2B358  token=0x600056F  System.Void StopAll()
  RVA=0x0AC29748  token=0x6000570  RootMotion.FinalIK.InteractionObject GetInteractionObject(RootMotion.FinalIK.FullBodyBipedEffector effectorType)
  RVA=0x0AC29880  token=0x6000571  System.Single GetProgress(RootMotion.FinalIK.FullBodyBipedEffector effectorType)
  RVA=0x0AC297C4  token=0x6000572  System.Single GetMinActiveProgress()
  RVA=0x0AC2BC44  token=0x6000573  System.Boolean TriggerInteraction(System.Int32 index, System.Boolean interrupt)
  RVA=0x0AC2B7E4  token=0x6000574  System.Boolean TriggerInteraction(System.Int32 index, System.Boolean interrupt, RootMotion.FinalIK.InteractionObject& interactionObject)
  RVA=0x0AC2B9C8  token=0x6000575  System.Boolean TriggerInteraction(System.Int32 index, System.Boolean interrupt, RootMotion.FinalIK.InteractionTarget& interactionTarget)
  RVA=0x0AC292C0  token=0x6000576  RootMotion.FinalIK.InteractionTrigger.Range GetClosestInteractionRange()
  RVA=0x0AC29194  token=0x6000577  RootMotion.FinalIK.InteractionObject GetClosestInteractionObjectInRange()
  RVA=0x0AC29398  token=0x6000578  RootMotion.FinalIK.InteractionTarget GetClosestInteractionTargetInRange()
  RVA=0x0AC291D4  token=0x6000579  RootMotion.FinalIK.InteractionObject[] GetClosestInteractionObjectsInRange()
  RVA=0x0AC29408  token=0x600057A  RootMotion.FinalIK.InteractionTarget[] GetClosestInteractionTargetsInRange()
  RVA=0x0AC2B45C  token=0x600057B  System.Boolean TriggerEffectorsReady(System.Int32 index)
  RVA=0x0AC29900  token=0x600057C  RootMotion.FinalIK.InteractionTrigger.Range GetTriggerRange(System.Int32 index)
  RVA=0x0AC29564  token=0x600057D  System.Int32 GetClosestTriggerIndex()
  RVA=0x0AC2AD0C  token=0x6000584  System.Void Start()
  RVA=0x0AC29A04  token=0x6000585  System.Void InteractionPause(RootMotion.FinalIK.FullBodyBipedEffector effector, RootMotion.FinalIK.InteractionObject interactionObject)
  RVA=0x0AC29A24  token=0x6000586  System.Void InteractionResume(RootMotion.FinalIK.FullBodyBipedEffector effector, RootMotion.FinalIK.InteractionObject interactionObject)
  RVA=0x0AC29A24  token=0x6000587  System.Void InteractionStop(RootMotion.FinalIK.FullBodyBipedEffector effector, RootMotion.FinalIK.InteractionObject interactionObject)
  RVA=0x0AC29DD8  token=0x6000588  System.Void LookAtInteraction(RootMotion.FinalIK.FullBodyBipedEffector effector, RootMotion.FinalIK.InteractionObject interactionObject)
  RVA=0x0AC2A58C  token=0x6000589  System.Void OnTriggerEnter(UnityEngine.Collider c)
  RVA=0x0AC2A684  token=0x600058A  System.Void OnTriggerExit(UnityEngine.Collider c)
  RVA=0x0AC28FE8  token=0x600058B  System.Boolean ContactIsInRange(System.Int32 index, System.Int32& bestRangeIndex)
  RVA=0x0AC2A344  token=0x600058C  System.Void OnDrawGizmosSelected()
  RVA=0x0AC2C150  token=0x600058D  System.Void Update()
  RVA=0x0AC2A96C  token=0x600058E  System.Void Raycasting()
  RVA=0x0AC2BF04  token=0x600058F  System.Void UpdateTriggerEventBroadcasting()
  RVA=0x0AC2A424  token=0x6000590  System.Void OnEnable()
  RVA=0x0AC2BDD8  token=0x6000591  System.Void UpdateEffectors()
  RVA=0x0AC2A510  token=0x6000592  System.Void OnPreFBBIK()
  RVA=0x0AC2A468  token=0x6000593  System.Void OnPostFBBIK()
  RVA=0x0AC2A444  token=0x6000594  System.Void OnFixTransforms()
  RVA=0x0AC29E44  token=0x6000595  System.Void OnDestroy()
  RVA=0x0AC29D10  token=0x6000596  System.Boolean IsValid(System.Boolean log)
  RVA=0x0AC2B6FC  token=0x6000597  System.Boolean TriggerIndexIsValid(System.Int32 index)
  RVA=0x0AC2C360  token=0x6000598  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.InteractionTarget
TYPE:  class
TOKEN: 0x200009A
SIZE:  0x70
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            RootMotion.FinalIK.FullBodyBipedEffectoreffectorType  // 0x18
  public            RootMotion.FinalIK.InteractionTarget.Multiplier[]multipliers  // 0x20
  public            System.Single                   interactionSpeedMlp  // 0x28
  public            UnityEngine.Transform           pivot  // 0x30
  public            RootMotion.FinalIK.InteractionTarget.RotationModerotationMode  // 0x38
  public            UnityEngine.Vector3             twistAxis  // 0x3c
  public            System.Single                   twistWeight  // 0x48
  public            System.Single                   swingWeight  // 0x4c
  public            System.Single                   threeDOFWeight  // 0x50
  public            System.Boolean                  rotateOnce  // 0x54
  private           UnityEngine.Quaternion          defaultLocalRotation  // 0x58
  private           UnityEngine.Transform           lastPivot  // 0x68
METHODS:
  RVA=0x0AC2C968  token=0x60005A1  System.Void OpenUserManual()
  RVA=0x0AC2C864  token=0x60005A2  System.Void OpenScriptReference()
  RVA=0x0AC2C898  token=0x60005A3  System.Void OpenTutorial1()
  RVA=0x0AC2C8CC  token=0x60005A4  System.Void OpenTutorial2()
  RVA=0x0AC2C900  token=0x60005A5  System.Void OpenTutorial3()
  RVA=0x0AC2C934  token=0x60005A6  System.Void OpenTutorial4()
  RVA=0x0AC2D098  token=0x60005A7  System.Void SupportGroup()
  RVA=0x0AC2C7D0  token=0x60005A8  System.Void ASThread()
  RVA=0x0AC2C804  token=0x60005A9  System.Single GetValue(RootMotion.FinalIK.InteractionObject.WeightCurve.Type curveType)
  RVA=0x0AC2C99C  token=0x60005AA  System.Void ResetRotation()
  RVA=0x0AC2CA18  token=0x60005AB  System.Void RotateTo(UnityEngine.Transform bone)
  RVA=0x0AC2D0CC  token=0x60005AC  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.InteractionTrigger
TYPE:  class
TOKEN: 0x200009D
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            RootMotion.FinalIK.InteractionTrigger.Range[]ranges  // 0x18
METHODS:
  RVA=0x0AC2D364  token=0x60005AE  System.Void OpenUserManual()
  RVA=0x0AC2D2FC  token=0x60005AF  System.Void OpenScriptReference()
  RVA=0x0AC2D330  token=0x60005B0  System.Void OpenTutorial4()
  RVA=0x0AC2D398  token=0x60005B1  System.Void SupportGroup()
  RVA=0x0AC2D118  token=0x60005B2  System.Void ASThread()
  RVA=0x0AC2D14C  token=0x60005B3  System.Int32 GetBestRangeIndex(UnityEngine.Transform character, UnityEngine.Transform raycastFrom, UnityEngine.RaycastHit raycastHit)
  RVA=0x0AC2D3CC  token=0x60005B4  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.GenericPoser
TYPE:  class
TOKEN: 0x20000A2
SIZE:  0x58
EXTENDS: RootMotion.FinalIK.Poser
FIELDS:
  public            RootMotion.FinalIK.GenericPoser.Map[]maps  // 0x50
METHODS:
  RVA=0x0AC259D8  token=0x60005BF  System.Void AutoMapping()
  RVA=0x0AC25CD8  token=0x60005C0  System.Void InitiatePoser()
  RVA=0x0AC25D34  token=0x60005C1  System.Void UpdatePoser()
  RVA=0x0AC25C08  token=0x60005C2  System.Void FixPoserTransforms()
  RVA=0x0AC25CE0  token=0x60005C3  System.Void StoreDefaultState()
  RVA=0x0AC25C5C  token=0x60005C4  UnityEngine.Transform GetTargetNamed(System.String tName, UnityEngine.Transform[] array)
  RVA=0x0AC25E0C  token=0x60005C5  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.HandPoser
TYPE:  class
TOKEN: 0x20000A4
SIZE:  0x78
EXTENDS: RootMotion.FinalIK.Poser
FIELDS:
  protected         UnityEngine.Transform[]         children  // 0x50
  private           UnityEngine.Transform           _poseRoot  // 0x58
  private           UnityEngine.Transform[]         poseChildren  // 0x60
  private           UnityEngine.Vector3[]           defaultLocalPositions  // 0x68
  private           UnityEngine.Quaternion[]        defaultLocalRotations  // 0x70
METHODS:
  RVA=0x0AC25E24  token=0x60005CA  System.Void AutoMapping()
  RVA=0x0AC25FD0  token=0x60005CB  System.Void InitiatePoser()
  RVA=0x0AC25EDC  token=0x60005CC  System.Void FixPoserTransforms()
  RVA=0x0AC26184  token=0x60005CD  System.Void UpdatePoser()
  RVA=0x0AC26020  token=0x60005CE  System.Void StoreDefaultState()
  RVA=0x0AC25E0C  token=0x60005CF  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.Poser
TYPE:  abstract class
TOKEN: 0x20000A5
SIZE:  0x50
EXTENDS: RootMotion.SolverManager
FIELDS:
  public            UnityEngine.Transform           poseRoot  // 0x38
  public            System.Single                   weight  // 0x40
  public            System.Single                   localRotationWeight  // 0x44
  public            System.Single                   localPositionWeight  // 0x48
  private           System.Boolean                  initiated  // 0x4c
METHODS:
  RVA=-1  // abstract  token=0x60005D0  System.Void AutoMapping()
  RVA=0x073629EC  token=0x60005D1  System.Void UpdateManual()
  RVA=-1  // abstract  token=0x60005D2  System.Void InitiatePoser()
  RVA=-1  // abstract  token=0x60005D3  System.Void UpdatePoser()
  RVA=-1  // abstract  token=0x60005D4  System.Void FixPoserTransforms()
  RVA=0x0AC2E81C  token=0x60005D5  System.Void UpdateSolver()
  RVA=0x0AC2E7F4  token=0x60005D6  System.Void InitiateSolver()
  RVA=0x0AC2E7E0  token=0x60005D7  System.Void FixTransforms()
  RVA=0x0AC25E0C  token=0x60005D8  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.RagdollUtility
TYPE:  class
TOKEN: 0x20000A6
SIZE:  0x78
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            RootMotion.FinalIK.IK           ik  // 0x18
  public            System.Single                   ragdollToAnimationTime  // 0x20
  public            System.Boolean                  applyIkOnRagdoll  // 0x24
  public            System.Single                   applyVelocity  // 0x28
  public            System.Single                   applyAngularVelocity  // 0x2c
  private           UnityEngine.Animator            animator  // 0x30
  private           RootMotion.FinalIK.RagdollUtility.Rigidbone[]rigidbones  // 0x38
  private           RootMotion.FinalIK.RagdollUtility.Child[]children  // 0x40
  private           System.Boolean                  enableRagdollFlag  // 0x48
  private           UnityEngine.AnimatorUpdateMode  animatorUpdateMode  // 0x4c
  private           RootMotion.FinalIK.IK[]         allIKComponents  // 0x50
  private           System.Boolean[]                fixTransforms  // 0x58
  private           System.Single                   ragdollWeight  // 0x60
  private           System.Single                   ragdollWeightV  // 0x64
  private           System.Boolean                  fixedFrame  // 0x68
  private           System.Boolean[]                disabledIKComponents  // 0x70
PROPERTIES:
  isRagdoll  get=0x0AC2FAE0
  ikUsed  get=0x0AC2F9E4
METHODS:
  RVA=0x0AC2EDD8  token=0x60005D9  System.Void EnableRagdoll()
  RVA=0x0AC2ED90  token=0x60005DA  System.Void DisableRagdoll()
  RVA=0x0AC2F2AC  token=0x60005DB  System.Void Start()
  RVA=0x0AC2ED34  token=0x60005DC  System.Collections.IEnumerator DisableRagdollSmooth()
  RVA=0x0AC2F700  token=0x60005DD  System.Void Update()
  RVA=0x0AC2EE5C  token=0x60005DE  System.Void FixedUpdate()
  RVA=0x0AC2EE94  token=0x60005DF  System.Void LateUpdate()
  RVA=0x0AC2ED08  token=0x60005E0  System.Void AfterLastIK()
  RVA=0x0AC2ECD0  token=0x60005E1  System.Void AfterAnimation()
  RVA=0x0AC2F02C  token=0x60005E2  System.Void OnFinalPose()
  RVA=0x0AC2F068  token=0x60005E3  System.Void RagdollEnabler()
  RVA=0x0AC2F260  token=0x60005E5  System.Void RecordVelocities()
  RVA=0x0AC2F6B4  token=0x60005E7  System.Void StoreLocalState()
  RVA=0x0AC2EE00  token=0x60005E8  System.Void FixTransforms(System.Single weight)
  RVA=0x0AC2EF00  token=0x60005E9  System.Void OnDestroy()
  RVA=0x0AC2F8F0  token=0x60005EA  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.RotationLimit
TYPE:  abstract class
TOKEN: 0x20000AA
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.Vector3             axis  // 0x18
  public            UnityEngine.Quaternion          defaultLocalRotation  // 0x24
  private           System.Boolean                  <defaultLocalRotationOverride>k__BackingField  // 0x34
  private           System.Boolean                  initiated  // 0x35
  private           System.Boolean                  applicationQuit  // 0x36
  private           System.Boolean                  defaultLocalRotationSet  // 0x37
PROPERTIES:
  secondaryAxis  get=0x04DA9A00
  crossAxis  get=0x0AC32EC0
  defaultLocalRotationOverride  get=0x04D86A90  set=0x04D86AA0
METHODS:
  RVA=0x0AC32E50  token=0x60005F7  System.Void SetDefaultLocalRotation()
  RVA=0x04DA99F0  token=0x60005F8  System.Void SetDefaultLocalRotation(UnityEngine.Quaternion localRotation)
  RVA=0x0AC32A34  token=0x60005F9  UnityEngine.Quaternion GetLimitedLocalRotation(UnityEngine.Quaternion localRotation, System.Boolean& changed)
  RVA=0x0AC328A8  token=0x60005FA  System.Boolean Apply()
  RVA=0x0AC32A0C  token=0x60005FB  System.Void Disable()
  RVA=-1  // abstract  token=0x6000600  UnityEngine.Quaternion LimitRotation(UnityEngine.Quaternion rotation)
  RVA=0x0AC32944  token=0x6000601  System.Void Awake()
  RVA=0x0AC32BD0  token=0x6000602  System.Void LateUpdate()
  RVA=0x0ABD818C  token=0x6000603  System.Void LogWarning(System.String message)
  RVA=0x0AC32BD8  token=0x6000604  UnityEngine.Quaternion Limit1DOF(UnityEngine.Quaternion rotation, UnityEngine.Vector3 axis)
  RVA=0x0AC32C88  token=0x6000605  UnityEngine.Quaternion LimitTwist(UnityEngine.Quaternion rotation, UnityEngine.Vector3 axis, UnityEngine.Vector3 orthoAxis, System.Single twistLimit)
  RVA=0x0AC32B58  token=0x6000606  System.Single GetOrthogonalAngle(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, UnityEngine.Vector3 normal)
  RVA=0x0AC32E90  token=0x6000607  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.RotationLimitAngle
TYPE:  class
TOKEN: 0x20000AB
SIZE:  0x40
EXTENDS: RootMotion.FinalIK.RotationLimit
FIELDS:
  public            System.Single                   limit  // 0x38
  public            System.Single                   twistLimit  // 0x3c
METHODS:
  RVA=0x0AC3082C  token=0x6000608  System.Void OpenUserManual()
  RVA=0x0AC307F8  token=0x6000609  System.Void OpenScriptReference()
  RVA=0x0AC30860  token=0x600060A  System.Void SupportGroup()
  RVA=0x0AC304F8  token=0x600060B  System.Void ASThread()
  RVA=0x0AC3052C  token=0x600060C  UnityEngine.Quaternion LimitRotation(UnityEngine.Quaternion rotation)
  RVA=0x0AC305F0  token=0x600060D  UnityEngine.Quaternion LimitSwing(UnityEngine.Quaternion rotation)
  RVA=0x0AC30894  token=0x600060E  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.RotationLimitHinge
TYPE:  class
TOKEN: 0x20000AC
SIZE:  0x50
EXTENDS: RootMotion.FinalIK.RotationLimit
FIELDS:
  public            System.Boolean                  useLimits  // 0x38
  public            System.Single                   min  // 0x3c
  public            System.Single                   max  // 0x40
  public            System.Single                   zeroAxisDisplayOffset  // 0x44
  private           System.Single                   lastAngle  // 0x48
METHODS:
  RVA=0x0AC30B74  token=0x600060F  System.Void OpenUserManual()
  RVA=0x0AC30B40  token=0x6000610  System.Void OpenScriptReference()
  RVA=0x0AC30BA8  token=0x6000611  System.Void SupportGroup()
  RVA=0x0AC308AC  token=0x6000612  System.Void ASThread()
  RVA=0x0AC30B0C  token=0x6000613  UnityEngine.Quaternion LimitRotation(UnityEngine.Quaternion rotation)
  RVA=0x0AC308E0  token=0x6000614  UnityEngine.Quaternion LimitHinge(UnityEngine.Quaternion rotation)
  RVA=0x0AC30BDC  token=0x6000615  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.RotationLimitPolygonal
TYPE:  class
TOKEN: 0x20000AD
SIZE:  0x58
EXTENDS: RootMotion.FinalIK.RotationLimit
FIELDS:
  public            System.Single                   twistLimit  // 0x38
  public            System.Int32                    smoothIterations  // 0x3c
  public            RootMotion.FinalIK.RotationLimitPolygonal.LimitPoint[]points  // 0x40
  public            UnityEngine.Vector3[]           P  // 0x48
  public            RootMotion.FinalIK.RotationLimitPolygonal.ReachCone[]reachCones  // 0x50
METHODS:
  RVA=0x0AC31560  token=0x6000616  System.Void OpenUserManual()
  RVA=0x0AC3152C  token=0x6000617  System.Void OpenScriptReference()
  RVA=0x0AC32230  token=0x6000618  System.Void SupportGroup()
  RVA=0x0AC30BF8  token=0x6000619  System.Void ASThread()
  RVA=0x0AC31A7C  token=0x600061A  System.Void SetLimitPoints(RootMotion.FinalIK.RotationLimitPolygonal.LimitPoint[] points)
  RVA=0x0AC31190  token=0x600061B  UnityEngine.Quaternion LimitRotation(UnityEngine.Quaternion rotation)
  RVA=0x0AC32058  token=0x600061C  System.Void Start()
  RVA=0x0AC31698  token=0x600061D  System.Void ResetToDefault()
  RVA=0x0AC30C2C  token=0x600061E  System.Void BuildReachCones()
  RVA=0x0AC31AF0  token=0x600061F  UnityEngine.Vector3[] SmoothPoints()
  RVA=0x0AC31140  token=0x6000620  System.Single GetScalar(System.Int32 k)
  RVA=0x0AC31594  token=0x6000621  UnityEngine.Vector3 PointToTangentPlane(UnityEngine.Vector3 p, System.Single r)
  RVA=0x0AC32264  token=0x6000622  UnityEngine.Vector3 TangentPointToSphere(UnityEngine.Vector3 q, System.Single r)
  RVA=0x0AC3127C  token=0x6000623  UnityEngine.Quaternion LimitSwing(UnityEngine.Quaternion rotation)
  RVA=0x0AC30FF8  token=0x6000624  System.Int32 GetReachCone(UnityEngine.Vector3 L)
  RVA=0x0AC323D0  token=0x6000625  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.RotationLimitSpline
TYPE:  class
TOKEN: 0x20000B0
SIZE:  0x48
EXTENDS: RootMotion.FinalIK.RotationLimit
FIELDS:
  public            System.Single                   twistLimit  // 0x38
  public            UnityEngine.AnimationCurve      spline  // 0x40
METHODS:
  RVA=0x0AC32810  token=0x600062E  System.Void OpenUserManual()
  RVA=0x0AC327DC  token=0x600062F  System.Void OpenScriptReference()
  RVA=0x0AC32864  token=0x6000630  System.Void SupportGroup()
  RVA=0x0AC32428  token=0x6000631  System.Void ASThread()
  RVA=0x0AC32844  token=0x6000632  System.Void SetSpline(UnityEngine.Keyframe[] keyframes)
  RVA=0x0AC3245C  token=0x6000633  UnityEngine.Quaternion LimitRotation(UnityEngine.Quaternion rotation)
  RVA=0x0AC32520  token=0x6000634  UnityEngine.Quaternion LimitSwing(UnityEngine.Quaternion rotation)
  RVA=0x0AC32898  token=0x6000635  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.AimController
TYPE:  class
TOKEN: 0x20000B1
SIZE:  0xD0
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            RootMotion.FinalIK.AimIK        ik  // 0x18
  public            System.Single                   weight  // 0x20
  public            UnityEngine.Transform           target  // 0x28
  public            System.Single                   targetSwitchSmoothTime  // 0x30
  public            System.Single                   weightSmoothTime  // 0x34
  public            System.Boolean                  smoothTurnTowardsTarget  // 0x38
  public            System.Single                   maxRadiansDelta  // 0x3c
  public            System.Single                   maxMagnitudeDelta  // 0x40
  public            System.Single                   slerpSpeed  // 0x44
  public            System.Single                   smoothDampTime  // 0x48
  public            UnityEngine.Vector3             pivotOffsetFromRoot  // 0x4c
  public            System.Single                   minDistance  // 0x58
  public            UnityEngine.Vector3             offset  // 0x5c
  public            System.Single                   maxRootAngle  // 0x68
  public            System.Boolean                  turnToTarget  // 0x6c
  public            System.Single                   turnToTargetTime  // 0x70
  public            System.Boolean                  useAnimatedAimDirection  // 0x74
  public            UnityEngine.Vector3             animatedAimDirection  // 0x78
  private           UnityEngine.Transform           lastTarget  // 0x88
  private           System.Single                   switchWeight  // 0x90
  private           System.Single                   switchWeightV  // 0x94
  private           System.Single                   weightV  // 0x98
  private           UnityEngine.Vector3             lastPosition  // 0x9c
  private           UnityEngine.Vector3             dir  // 0xa8
  private           System.Boolean                  lastSmoothTowardsTarget  // 0xb4
  private           System.Boolean                  turningToTarget  // 0xb5
  private           System.Single                   turnToTargetMlp  // 0xb8
  private           System.Single                   turnToTargetMlpV  // 0xbc
  private           System.Single                   yawV  // 0xc0
  private           System.Single                   pitchV  // 0xc4
  private           System.Single                   dirMagV  // 0xc8
PROPERTIES:
  pivot  get=0x0AC22840
METHODS:
  RVA=0x0AC22664  token=0x6000636  System.Void Start()
  RVA=0x0AC21A10  token=0x6000637  System.Void LateUpdate()
  RVA=0x0AC218D8  token=0x6000639  System.Void ApplyMinDistance()
  RVA=0x0AC223A0  token=0x600063A  System.Void RootRotation()
  RVA=0x0AC22754  token=0x600063B  System.Collections.IEnumerator TurnToTarget()
  RVA=0x0AC227B0  token=0x600063C  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.AimPoser
TYPE:  class
TOKEN: 0x20000B3
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Single                   angleBuffer  // 0x18
  public            RootMotion.FinalIK.AimPoser.Pose[]poses  // 0x20
METHODS:
  RVA=0x0AC22948  token=0x6000643  RootMotion.FinalIK.AimPoser.Pose GetPose(UnityEngine.Vector3 localDirection)
  RVA=0x0AC22A10  token=0x6000644  System.Void SetPoseActive(RootMotion.FinalIK.AimPoser.Pose pose)
  RVA=0x0AC22A70  token=0x6000645  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.Amplifier
TYPE:  class
TOKEN: 0x20000B5
SIZE:  0x38
EXTENDS: RootMotion.FinalIK.OffsetModifier
FIELDS:
  public            RootMotion.FinalIK.Amplifier.Body[]bodies  // 0x30
METHODS:
  RVA=0x0AC22AC8  token=0x6000649  System.Void OnModifyOffset()
  RVA=0x0AC22BE4  token=0x600064A  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.BodyTilt
TYPE:  class
TOKEN: 0x20000B8
SIZE:  0x58
EXTENDS: RootMotion.FinalIK.OffsetModifier
FIELDS:
  public            System.Single                   tiltSpeed  // 0x30
  public            System.Single                   tiltSensitivity  // 0x34
  public            RootMotion.FinalIK.OffsetPose   poseLeft  // 0x38
  public            RootMotion.FinalIK.OffsetPose   poseRight  // 0x40
  private           System.Single                   tiltAngle  // 0x48
  private           UnityEngine.Vector3             lastForward  // 0x4c
METHODS:
  RVA=0x0AC22FE0  token=0x600064F  System.Void Start()
  RVA=0x0AC22E04  token=0x6000650  System.Void OnModifyOffset()
  RVA=0x0AC2302C  token=0x6000651  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.CCDBendGoal
TYPE:  class
TOKEN: 0x20000B9
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            RootMotion.FinalIK.CCDIK        ik  // 0x18
  public            System.Single                   weight  // 0x20
METHODS:
  RVA=0x0AC23EFC  token=0x6000652  System.Void Start()
  RVA=0x0AC23AEC  token=0x6000653  System.Void BeforeIK()
  RVA=0x0AC23DD4  token=0x6000654  System.Void OnDestroy()
  RVA=0x0539808C  token=0x6000655  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.EditorIK
TYPE:  class
TOKEN: 0x20000BA
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.Animator            animator  // 0x18
  public            RootMotion.FinalIK.EditorIKPose defaultPose  // 0x20
  public            UnityEngine.Transform[]         bones  // 0x28
  private           RootMotion.FinalIK.IK           <ik>k__BackingField  // 0x30
PROPERTIES:
  ik  get=0x011F36E0  set=0x05396200
METHODS:
  RVA=0x0AC2544C  token=0x6000658  System.Void OnEnable()
  RVA=0x0AC25380  token=0x6000659  System.Void OnDisable()
  RVA=0x0AC25248  token=0x600065A  System.Void OnDestroy()
  RVA=0x0AC25594  token=0x600065B  System.Void StoreDefaultPose()
  RVA=0x0AC25078  token=0x600065C  System.Boolean Initiate()
  RVA=0x0AC2560C  token=0x600065D  System.Void Update()
  RVA=0x0AC2589C  token=0x600065E  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.EditorIKPose
TYPE:  class
TOKEN: 0x20000BB
SIZE:  0x28
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public            UnityEngine.Vector3[]           localPositions  // 0x18
  public            UnityEngine.Quaternion[]        localRotations  // 0x20
PROPERTIES:
  poseStored  get=0x0AC25058
METHODS:
  RVA=0x0AC24E94  token=0x6000660  System.Void Store(UnityEngine.Transform[] T)
  RVA=0x0AC24D3C  token=0x6000661  System.Boolean Restore(UnityEngine.Transform[] T)
  RVA=0x0AC24FE0  token=0x6000662  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.HitReaction
TYPE:  class
TOKEN: 0x20000BC
SIZE:  0x40
EXTENDS: RootMotion.FinalIK.OffsetModifier
FIELDS:
  public            RootMotion.FinalIK.HitReaction.HitPointEffector[]effectorHitPoints  // 0x30
  public            RootMotion.FinalIK.HitReaction.HitPointBone[]boneHitPoints  // 0x38
PROPERTIES:
  inProgress  get=0x0AC2721C
METHODS:
  RVA=0x0AC27174  token=0x6000664  System.Void OnModifyOffset()
  RVA=0x0AC26F94  token=0x6000665  System.Void Hit(UnityEngine.Collider collider, UnityEngine.Vector3 force, UnityEngine.Vector3 point)
  RVA=0x0AC22BE4  token=0x6000666  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.HitReactionVRIK
TYPE:  class
TOKEN: 0x20000C2
SIZE:  0x48
EXTENDS: RootMotion.FinalIK.OffsetModifierVRIK
FIELDS:
  public            UnityEngine.AnimationCurve[]    offsetCurves  // 0x30
  public            RootMotion.FinalIK.HitReactionVRIK.PositionOffset[]positionOffsets  // 0x38
  public            RootMotion.FinalIK.HitReactionVRIK.RotationOffset[]rotationOffsets  // 0x40
METHODS:
  RVA=0x0AC26EF0  token=0x6000683  System.Void OnModifyOffset()
  RVA=0x0AC26D08  token=0x6000684  System.Void Hit(UnityEngine.Collider collider, UnityEngine.Vector3 force, UnityEngine.Vector3 point)
  RVA=0x0AC22BE4  token=0x6000685  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.Inertia
TYPE:  class
TOKEN: 0x20000C8
SIZE:  0x40
EXTENDS: RootMotion.FinalIK.OffsetModifier
FIELDS:
  public            RootMotion.FinalIK.Inertia.Body[]bodies  // 0x30
  public            RootMotion.FinalIK.OffsetModifier.OffsetLimits[]limits  // 0x38
METHODS:
  RVA=0x0AC2734C  token=0x60006A1  System.Void ResetBodies()
  RVA=0x0AC2729C  token=0x60006A2  System.Void OnModifyOffset()
  RVA=0x0AC22BE4  token=0x60006A3  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.LookAtController
TYPE:  class
TOKEN: 0x20000CB
SIZE:  0x98
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            RootMotion.FinalIK.LookAtIK     ik  // 0x18
  public            UnityEngine.Transform           target  // 0x20
  public            System.Single                   weight  // 0x28
  public            UnityEngine.Vector3             offset  // 0x2c
  public            System.Single                   targetSwitchSmoothTime  // 0x38
  public            System.Single                   weightSmoothTime  // 0x3c
  public            System.Boolean                  smoothTurnTowardsTarget  // 0x40
  public            System.Single                   maxRadiansDelta  // 0x44
  public            System.Single                   maxMagnitudeDelta  // 0x48
  public            System.Single                   slerpSpeed  // 0x4c
  public            UnityEngine.Vector3             pivotOffsetFromRoot  // 0x50
  public            System.Single                   minDistance  // 0x5c
  public            System.Single                   maxRootAngle  // 0x60
  private           UnityEngine.Transform           lastTarget  // 0x68
  private           System.Single                   switchWeight  // 0x70
  private           System.Single                   switchWeightV  // 0x74
  private           System.Single                   weightV  // 0x78
  private           UnityEngine.Vector3             lastPosition  // 0x7c
  private           UnityEngine.Vector3             dir  // 0x88
  private           System.Boolean                  lastSmoothTowardsTarget  // 0x94
PROPERTIES:
  pivot  get=0x0AC2DFE8
METHODS:
  RVA=0x0AC2DEBC  token=0x60006A8  System.Void Start()
  RVA=0x0AC2D578  token=0x60006A9  System.Void LateUpdate()
  RVA=0x0AC2D440  token=0x60006AB  System.Void ApplyMinDistance()
  RVA=0x0AC2DC58  token=0x60006AC  System.Void RootRotation()
  RVA=0x0AC2DF84  token=0x60006AD  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.OffsetModifier
TYPE:  abstract class
TOKEN: 0x20000CC
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Single                   weight  // 0x18
  public            RootMotion.FinalIK.FullBodyBipedIKik  // 0x20
  protected         System.Single                   lastTime  // 0x28
PROPERTIES:
  deltaTime  get=0x0AC34514
METHODS:
  RVA=-1  // abstract  token=0x60006AF  System.Void OnModifyOffset()
  RVA=0x0AC34838  token=0x60006B0  System.Void Start()
  RVA=0x0AC345F0  token=0x60006B1  System.Collections.IEnumerator Initiate()
  RVA=0x0AC3464C  token=0x60006B2  System.Void ModifyOffset()
  RVA=0x0AC34530  token=0x60006B3  System.Void ApplyLimits(RootMotion.FinalIK.OffsetModifier.OffsetLimits[] limits)
  RVA=0x0AC34710  token=0x60006B4  System.Void OnDestroy()
  RVA=0x090D4B8C  token=0x60006B5  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.OffsetModifierVRIK
TYPE:  abstract class
TOKEN: 0x20000CF
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Single                   weight  // 0x18
  public            RootMotion.FinalIK.VRIK         ik  // 0x20
  private           System.Single                   lastTime  // 0x28
PROPERTIES:
  deltaTime  get=0x0AC34514
METHODS:
  RVA=-1  // abstract  token=0x60006C1  System.Void OnModifyOffset()
  RVA=0x0AC344F0  token=0x60006C2  System.Void Start()
  RVA=0x0AC342A8  token=0x60006C3  System.Collections.IEnumerator Initiate()
  RVA=0x0AC34304  token=0x60006C4  System.Void ModifyOffset()
  RVA=0x0AC343C8  token=0x60006C5  System.Void OnDestroy()
  RVA=0x090D4B8C  token=0x60006C6  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.OffsetPose
TYPE:  class
TOKEN: 0x20000D1
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            RootMotion.FinalIK.OffsetPose.EffectorLink[]effectorLinks  // 0x18
METHODS:
  RVA=0x0AC3485C  token=0x60006CD  System.Void Apply(RootMotion.FinalIK.IKSolverFullBodyBiped solver, System.Single weight)
  RVA=0x0AC34908  token=0x60006CE  System.Void Apply(RootMotion.FinalIK.IKSolverFullBodyBiped solver, System.Single weight, UnityEngine.Quaternion rotation)
  RVA=0x0AC34998  token=0x60006CF  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.PenetrationAvoidance
TYPE:  class
TOKEN: 0x20000D3
SIZE:  0x38
EXTENDS: RootMotion.FinalIK.OffsetModifier
FIELDS:
  public            RootMotion.FinalIK.PenetrationAvoidance.Avoider[]avoiders  // 0x30
METHODS:
  RVA=0x0AC349E8  token=0x60006D2  System.Void OnModifyOffset()
  RVA=0x0AC22BE4  token=0x60006D3  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.Recoil
TYPE:  class
TOKEN: 0x20000D6
SIZE:  0xF8
EXTENDS: RootMotion.FinalIK.OffsetModifier
FIELDS:
  public            RootMotion.FinalIK.AimIK        aimIK  // 0x30
  public            RootMotion.FinalIK.AimIK        headIK  // 0x38
  public            System.Boolean                  aimIKSolvedLast  // 0x40
  public            RootMotion.FinalIK.Recoil.Handednesshandedness  // 0x44
  public            System.Boolean                  twoHanded  // 0x48
  public            UnityEngine.AnimationCurve      recoilWeight  // 0x50
  public            System.Single                   magnitudeRandom  // 0x58
  public            UnityEngine.Vector3             rotationRandom  // 0x5c
  public            UnityEngine.Vector3             handRotationOffset  // 0x68
  public            System.Single                   blendTime  // 0x74
  public            RootMotion.FinalIK.Recoil.RecoilOffset[]offsets  // 0x78
  public            UnityEngine.Quaternion          rotationOffset  // 0x80
  private           System.Single                   magnitudeMlp  // 0x90
  private           System.Single                   endTime  // 0x94
  private           UnityEngine.Quaternion          handRotation  // 0x98
  private           UnityEngine.Quaternion          secondaryHandRelativeRotation  // 0xa8
  private           UnityEngine.Quaternion          randomRotation  // 0xb8
  private           System.Single                   length  // 0xc8
  private           System.Boolean                  initiated  // 0xcc
  private           System.Single                   blendWeight  // 0xd0
  private           System.Single                   w  // 0xd4
  private           UnityEngine.Quaternion          primaryHandRotation  // 0xd8
  private           System.Boolean                  handRotationsSet  // 0xe8
  private           UnityEngine.Vector3             aimIKAxis  // 0xec
PROPERTIES:
  isFinished  get=0x0AC35E0C
  primaryHandEffector  get=0x0AC35E2C
  secondaryHandEffector  get=0x0AC35E94
  primaryHand  get=0x0AC35E74
  secondaryHand  get=0x0AC35EDC
METHODS:
  RVA=0x0AC35D80  token=0x60006DA  System.Void SetHandRotations(UnityEngine.Quaternion leftHandRotation, UnityEngine.Quaternion rightHandRotation)
  RVA=0x0AC34ED4  token=0x60006DB  System.Void Fire(System.Single magnitude)
  RVA=0x0AC35234  token=0x60006DC  System.Void OnModifyOffset()
  RVA=0x0AC34D8C  token=0x60006DD  System.Void AfterFBBIK()
  RVA=0x0AC34CDC  token=0x60006DE  System.Void AfterAimIK()
  RVA=0x0AC35020  token=0x60006E3  System.Void OnDestroy()
  RVA=0x0AC35DA0  token=0x60006E4  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.ShoulderRotator
TYPE:  class
TOKEN: 0x20000DA
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Single                   weight  // 0x18
  public            System.Single                   offset  // 0x1c
  private           RootMotion.FinalIK.FullBodyBipedIKik  // 0x20
  private           System.Boolean                  skip  // 0x28
METHODS:
  RVA=0x0AC36654  token=0x60006E9  System.Void Start()
  RVA=0x0AC3657C  token=0x60006EA  System.Void RotateShoulders()
  RVA=0x0AC36128  token=0x60006EB  System.Void RotateShoulder(RootMotion.FinalIK.FullBodyBipedChain chain, System.Single weight, System.Single offset)
  RVA=0x0AC35FC4  token=0x60006EC  RootMotion.FinalIK.IKMapping.BoneMap GetParentBoneMap(RootMotion.FinalIK.FullBodyBipedChain chain)
  RVA=0x0AC36000  token=0x60006ED  System.Void OnDestroy()
  RVA=0x0AC36770  token=0x60006EE  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.VRIKCalibrator
TYPE:  static class
TOKEN: 0x20000DB
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AC3B260  token=0x60006EF  System.Void RecalibrateScale(RootMotion.FinalIK.VRIK ik, RootMotion.FinalIK.VRIKCalibrator.CalibrationData data, RootMotion.FinalIK.VRIKCalibrator.Settings settings)
  RVA=0x0AC3B200  token=0x60006F0  System.Void RecalibrateScale(RootMotion.FinalIK.VRIK ik, RootMotion.FinalIK.VRIKCalibrator.CalibrationData data, System.Single scaleMlp)
  RVA=0x0AC38530  token=0x60006F1  System.Void CalibrateScale(RootMotion.FinalIK.VRIK ik, RootMotion.FinalIK.VRIKCalibrator.Settings settings)
  RVA=0x0AC38550  token=0x60006F2  System.Void CalibrateScale(RootMotion.FinalIK.VRIK ik, System.Single scaleMlp)
  RVA=0x0AC39254  token=0x60006F3  RootMotion.FinalIK.VRIKCalibrator.CalibrationData Calibrate(RootMotion.FinalIK.VRIK ik, RootMotion.FinalIK.VRIKCalibrator.Settings settings, UnityEngine.Transform headTracker, UnityEngine.Transform bodyTracker, UnityEngine.Transform leftHandTracker, UnityEngine.Transform rightHandTracker, UnityEngine.Transform leftFootTracker, UnityEngine.Transform rightFootTracker)
  RVA=0x0AC37DF0  token=0x60006F4  System.Void CalibrateLeg(RootMotion.FinalIK.VRIKCalibrator.Settings settings, UnityEngine.Transform tracker, RootMotion.FinalIK.IKSolverVR.Leg leg, UnityEngine.Transform lastBone, UnityEngine.Vector3 rootForward, System.Boolean isLeft)
  RVA=0x0AC386DC  token=0x60006F5  System.Void Calibrate(RootMotion.FinalIK.VRIK ik, RootMotion.FinalIK.VRIKCalibrator.CalibrationData data, UnityEngine.Transform headTracker, UnityEngine.Transform bodyTracker, UnityEngine.Transform leftHandTracker, UnityEngine.Transform rightHandTracker, UnityEngine.Transform leftFootTracker, UnityEngine.Transform rightFootTracker)
  RVA=0x0AC37BA0  token=0x60006F6  System.Void CalibrateLeg(RootMotion.FinalIK.VRIKCalibrator.CalibrationData data, UnityEngine.Transform tracker, RootMotion.FinalIK.IKSolverVR.Leg leg, UnityEngine.Transform lastBone, UnityEngine.Vector3 rootForward, System.Boolean isLeft)
  RVA=0x0AC3AA44  token=0x60006F7  RootMotion.FinalIK.VRIKCalibrator.CalibrationData Calibrate(RootMotion.FinalIK.VRIK ik, UnityEngine.Transform centerEyeAnchor, UnityEngine.Transform leftHandAnchor, UnityEngine.Transform rightHandAnchor, UnityEngine.Vector3 centerEyePositionOffset, UnityEngine.Vector3 centerEyeRotationOffset, UnityEngine.Vector3 handPositionOffset, UnityEngine.Vector3 handRotationOffset, System.Single scaleMlp)
  RVA=0x0AC37610  token=0x60006F8  System.Void CalibrateHead(RootMotion.FinalIK.VRIK ik, UnityEngine.Transform centerEyeAnchor, UnityEngine.Vector3 anchorPositionOffset, UnityEngine.Vector3 anchorRotationOffset)
  RVA=0x0AC36C08  token=0x60006F9  System.Void CalibrateBody(RootMotion.FinalIK.VRIK ik, UnityEngine.Transform pelvisTracker, UnityEngine.Vector3 trackerPositionOffset, UnityEngine.Vector3 trackerRotationOffset)
  RVA=0x0AC373C0  token=0x60006FA  System.Void CalibrateHands(RootMotion.FinalIK.VRIK ik, UnityEngine.Transform leftHandAnchor, UnityEngine.Transform rightHandAnchor, UnityEngine.Vector3 anchorPositionOffset, UnityEngine.Vector3 anchorRotationOffset)
  RVA=0x0AC36F04  token=0x60006FB  System.Void CalibrateHand(RootMotion.FinalIK.VRIK ik, UnityEngine.Transform anchor, UnityEngine.Vector3 positionOffset, UnityEngine.Vector3 rotationOffset, System.Boolean isLeft)
  RVA=0x0AC3B084  token=0x60006FC  UnityEngine.Vector3 GuessWristToPalmAxis(UnityEngine.Transform hand, UnityEngine.Transform forearm)
  RVA=0x0AC3AC90  token=0x60006FD  UnityEngine.Vector3 GuessPalmToThumbAxis(UnityEngine.Transform hand, UnityEngine.Transform forearm)
END_CLASS

CLASS: RootMotion.FinalIK.VRIKLODController
TYPE:  class
TOKEN: 0x20000DF
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.Renderer            LODRenderer  // 0x18
  public            System.Single                   LODDistance  // 0x20
  public            System.Boolean                  allowCulled  // 0x24
  private           RootMotion.FinalIK.VRIK         ik  // 0x28
METHODS:
  RVA=0x0AC3B3E0  token=0x6000702  System.Void Start()
  RVA=0x0AC3B428  token=0x6000703  System.Void Update()
  RVA=0x0AC3B284  token=0x6000704  System.Int32 GetLODLevel()
  RVA=0x0AC3B45C  token=0x6000705  System.Void .ctor()
END_CLASS

CLASS: RootMotion.FinalIK.VRIKRootController
TYPE:  class
TOKEN: 0x20000E0
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Vector3             <pelvisTargetRight>k__BackingField  // 0x18
  private           UnityEngine.Transform           pelvisTarget  // 0x28
  private           UnityEngine.Transform           leftFootTarget  // 0x30
  private           UnityEngine.Transform           rightFootTarget  // 0x38
  private           RootMotion.FinalIK.VRIK         ik  // 0x40
PROPERTIES:
  pelvisTargetRight  get=0x04D89870  set=0x04D89890
METHODS:
  RVA=0x0AC3B47C  token=0x6000708  System.Void Awake()
  RVA=0x0AC3B5A8  token=0x6000709  System.Void Calibrate()
  RVA=0x0AC3B7CC  token=0x600070A  System.Void Calibrate(RootMotion.FinalIK.VRIKCalibrator.CalibrationData data)
  RVA=0x0AC3BA7C  token=0x600070B  System.Void OnPreUpdate()
  RVA=0x0AC3B954  token=0x600070C  System.Void OnDestroy()
  RVA=0x05393520  token=0x600070D  System.Void .ctor()
END_CLASS

