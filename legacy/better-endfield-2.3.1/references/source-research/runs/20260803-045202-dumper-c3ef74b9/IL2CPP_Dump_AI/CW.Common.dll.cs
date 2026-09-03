// ========================================================
// Dumped by @desirepro
// Assembly: CW.Common.dll
// Classes:  34
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

CLASS: LinkType
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  CW.Common.CwDemoButton.LinkType PreviousScene  // const
  public    static  CW.Common.CwDemoButton.LinkType NextScene  // const
  public    static  CW.Common.CwDemoButton.LinkType Publisher  // const
  public    static  CW.Common.CwDemoButton.LinkType URL  // const
  public    static  CW.Common.CwDemoButton.LinkType Isolate  // const
METHODS:
END_CLASS

CLASS: ToggleType
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  CW.Common.CwDemoButton.ToggleTypeKeepSelected  // const
  public    static  CW.Common.CwDemoButton.ToggleTypeToggleSelection  // const
  public    static  CW.Common.CwDemoButton.ToggleTypeSelectPrevious  // const
METHODS:
END_CLASS

CLASS: IHasChildren
TYPE:  interface
TOKEN: 0x200000D
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600007D  System.Boolean HasChild(CW.Common.CwChild child)
END_CLASS

CLASS: FollowType
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  CW.Common.CwFollow.FollowType   TargetTransform  // const
  public    static  CW.Common.CwFollow.FollowType   MainCamera  // const
METHODS:
END_CLASS

CLASS: UpdateType
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  CW.Common.CwFollow.UpdateType   Update  // const
  public    static  CW.Common.CwFollow.UpdateType   LateUpdate  // const
METHODS:
END_CLASS

CLASS: AxisGesture
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  CW.Common.CwInputManager.AxisGestureHorizontalDrag  // const
  public    static  CW.Common.CwInputManager.AxisGestureVerticalDrag  // const
  public    static  CW.Common.CwInputManager.AxisGestureTwist  // const
  public    static  CW.Common.CwInputManager.AxisGestureHorizontalPull  // const
  public    static  CW.Common.CwInputManager.AxisGestureVerticalPull  // const
METHODS:
END_CLASS

CLASS: Axis
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x34
FIELDS:
  public            System.Int32                    FingerCount  // 0x10
  public            System.Boolean                  FingerInvert  // 0x14
  public            CW.Common.CwInputManager.AxisGestureFingerGesture  // 0x18
  public            System.Single                   FingerSensitivity  // 0x1c
  public            UnityEngine.KeyCode             KeyNegative  // 0x20
  public            UnityEngine.KeyCode             KeyPositive  // 0x24
  public            UnityEngine.KeyCode             KeyNegativeAlt  // 0x28
  public            UnityEngine.KeyCode             KeyPositiveAlt  // 0x2c
  public            System.Single                   KeySensitivity  // 0x30
METHODS:
  RVA=0x04D89450  token=0x60000B3  System.Void .ctor(System.Int32 fCount, System.Boolean fInvert, CW.Common.CwInputManager.AxisGesture fGesture, System.Single fSensitivty, UnityEngine.KeyCode kNegative, UnityEngine.KeyCode kPositive, UnityEngine.KeyCode kNegativeAlt, UnityEngine.KeyCode kPositiveAlt, System.Single kSensitivity)
  RVA=0x0690A92C  token=0x60000B4  System.Single GetValue(System.Single delta)
END_CLASS

CLASS: Trigger
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x18
FIELDS:
  public            System.Boolean                  UseFinger  // 0x10
  public            System.Boolean                  UseMouse  // 0x11
  public            UnityEngine.KeyCode             UseKey  // 0x14
METHODS:
  RVA=0x04D898A0  token=0x60000B5  System.Void .ctor(System.Boolean uFinger, System.Boolean uMouse, UnityEngine.KeyCode uKey)
  RVA=0x0691231C  token=0x60000B6  System.Boolean WentDown(CW.Common.CwInputManager.Finger finger)
  RVA=0x0691227C  token=0x60000B7  System.Boolean IsDown(CW.Common.CwInputManager.Finger finger)
  RVA=0x069123BC  token=0x60000B8  System.Boolean WentUp(CW.Common.CwInputManager.Finger finger, System.Boolean useAnyFinger)
END_CLASS

CLASS: Link
TYPE:  abstract class
TOKEN: 0x2000016
SIZE:  0x18
FIELDS:
  public            CW.Common.CwInputManager.Finger Finger  // 0x10
METHODS:
  RVA=-1  // generic def  token=0x60000B9  T Find(System.Collections.Generic.List<T> links, CW.Common.CwInputManager.Finger finger)
  RVA=-1  // generic def  token=0x60000BA  T Create(System.Collections.Generic.List<T>& links, CW.Common.CwInputManager.Finger finger)
  RVA=-1  // generic def  token=0x60000BB  System.Void ClearAll(System.Collections.Generic.List<T> links)
  RVA=-1  // generic def  token=0x60000BC  System.Void ClearAndRemove(System.Collections.Generic.List<T> links, T link)
  RVA=0x041E1670  token=0x60000BD  System.Void Clear()
  RVA=0x041E1670  token=0x60000BE  System.Void .ctor()
END_CLASS

CLASS: Finger
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x50
FIELDS:
  public            System.Int32                    Index  // 0x10
  public            System.Single                   Pressure  // 0x14
  public            System.Boolean                  Down  // 0x18
  public            System.Boolean                  Up  // 0x19
  public            System.Single                   Age  // 0x1c
  public            System.Boolean                  StartedOverGui  // 0x20
  public            UnityEngine.Vector2             StartScreenPosition  // 0x24
  public            UnityEngine.Vector2             ScreenPosition  // 0x2c
  public            UnityEngine.Vector2             ScreenPositionOld  // 0x34
  public            UnityEngine.Vector2             ScreenPositionOldOld  // 0x3c
  public            UnityEngine.Vector2             ScreenPositionOldOldOld  // 0x44
PROPERTIES:
  SmoothScreenPositionDelta  get=0x069121E0
METHODS:
  RVA=0x069120A4  token=0x60000C0  UnityEngine.Vector2 GetSmoothScreenPosition(System.Single t)
  RVA=0x041E1670  token=0x60000C1  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200001E
SIZE:  0x10
FIELDS:
  public    static readonly CW.Common.CwHelper.<>c          <>9  // static @ 0x0
METHODS:
  RVA=0x0691266C  token=0x6000108  System.Void .cctor()
  RVA=0x041E1670  token=0x6000109  System.Void .ctor()
  RVA=0x0691247C  token=0x600010A  System.Void <.cctor>b__11_0(UnityEngine.Camera camera)
  RVA=0x069124F8  token=0x600010B  System.Void <.cctor>b__11_1(UnityEngine.Camera camera)
  RVA=0x06912574  token=0x600010C  System.Void <.cctor>b__11_2(UnityEngine.Rendering.ScriptableRenderContext context, UnityEngine.Camera camera)
  RVA=0x069125F0  token=0x600010D  System.Void <.cctor>b__11_3(UnityEngine.Rendering.ScriptableRenderContext context, UnityEngine.Camera camera)
END_CLASS

CLASS: Pipeline
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  CW.Common.CwShaderBundle.PipelineInvalid  // const
  public    static  CW.Common.CwShaderBundle.PipelineStandard  // const
  public    static  CW.Common.CwShaderBundle.PipelineURP2019  // const
  public    static  CW.Common.CwShaderBundle.PipelineURP2020  // const
  public    static  CW.Common.CwShaderBundle.PipelineURP2021  // const
  public    static  CW.Common.CwShaderBundle.PipelineHDRP2019  // const
  public    static  CW.Common.CwShaderBundle.PipelineHDRP2020  // const
  public    static  CW.Common.CwShaderBundle.PipelineHDRP2021  // const
  public    static  CW.Common.CwShaderBundle.PipelineCOUNT  // const
METHODS:
END_CLASS

CLASS: ShaderVariant
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x28
FIELDS:
  public            CW.Common.CwShaderBundle.PipelinePipe  // 0x10
  public            System.String                   Code  // 0x18
  public            System.Int32                    Hash  // 0x20
  public            System.Boolean                  Dirty  // 0x24
PROPERTIES:
  HashString  get=0x06912220
METHODS:
  RVA=0x041E1670  token=0x6000130  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwCameraLook
TYPE:  class
TOKEN: 0x2000002
SIZE:  0xA0
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Boolean                  listen  // 0x18
  private           System.Single                   damping  // 0x1c
  private           System.Single                   sensitivity  // 0x20
  private           CW.Common.CwInputManager.Axis   pitchControls  // 0x24
  private           CW.Common.CwInputManager.Axis   yawControls  // 0x48
  private           CW.Common.CwInputManager.Axis   rollControls  // 0x6c
  private           UnityEngine.Quaternion          remainingDelta  // 0x90
PROPERTIES:
  Listen  get=0x04D86340  set=0x04D86350
  Damping  get=0x04D868F0  set=0x04D86900
  Sensitivity  get=0x011EC8A0  set=0x011EC9E0
  PitchControls  get=0x04D89490  set=0x04D894F0
  YawControls  get=0x04D894D0  set=0x04D89530
  RollControls  get=0x04D894B0  set=0x04D89510
METHODS:
  RVA=0x0690AF80  token=0x600000D  System.Void Start()
  RVA=0x041E1670  token=0x600000E  System.Void OnDisable()
  RVA=0x0690AFC0  token=0x600000F  System.Void Update()
  RVA=0x041E1670  token=0x6000010  System.Void OnApplicationFocus(System.Boolean focus)
  RVA=0x0690ABFC  token=0x6000011  System.Void AddToDelta()
  RVA=0x0690AE08  token=0x6000012  System.Void DampenDelta()
  RVA=0x0690AFE8  token=0x6000013  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwCameraMove
TYPE:  class
TOKEN: 0x2000003
SIZE:  0xA0
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Boolean                  listen  // 0x18
  private           System.Single                   damping  // 0x1c
  private           System.Single                   sensitivity  // 0x20
  private           CW.Common.CwInputManager.Axis   horizontalControls  // 0x24
  private           CW.Common.CwInputManager.Axis   depthControls  // 0x48
  private           CW.Common.CwInputManager.Axis   verticalControls  // 0x6c
  private           UnityEngine.Vector3             remainingDelta  // 0x90
PROPERTIES:
  Listen  get=0x04D86340  set=0x04D86350
  Damping  get=0x04D868F0  set=0x04D86900
  Sensitivity  get=0x011EC8A0  set=0x011EC9E0
  HorizontalControls  get=0x04D89490  set=0x04D894F0
  DepthControls  get=0x04D894D0  set=0x04D89530
  VerticalControls  get=0x04D894B0  set=0x04D89510
METHODS:
  RVA=0x0690B480  token=0x6000020  System.Void Start()
  RVA=0x0690B4C0  token=0x6000021  System.Void Update()
  RVA=0x0690B0CC  token=0x6000022  System.Void AddToDelta()
  RVA=0x0690B2F0  token=0x6000023  System.Void DampenDelta()
  RVA=0x0690B4E8  token=0x6000024  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwCameraPivot
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x78
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Boolean                  listen  // 0x18
  private           System.Single                   damping  // 0x1c
  private           CW.Common.CwInputManager.Axis   pitchControls  // 0x20
  private           CW.Common.CwInputManager.Axis   yawControls  // 0x44
  private           UnityEngine.Vector3             remainingDelta  // 0x68
PROPERTIES:
  Listen  get=0x04D86340  set=0x04D86350
  Damping  get=0x04D868F0  set=0x04D86900
  PitchControls  get=0x04D89550  set=0x04D89590
  YawControls  get=0x04D89570  set=0x04D895B0
METHODS:
  RVA=0x0690B804  token=0x600002D  System.Void OnEnable()
  RVA=0x0690B844  token=0x600002E  System.Void Update()
  RVA=0x0690B5C4  token=0x600002F  System.Void AddToDelta()
  RVA=0x0690B628  token=0x6000030  System.Void DampenDelta()
  RVA=0x0690B86C  token=0x6000031  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwDemo
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Boolean                  upgradeInputModule  // 0x18
  private           System.Boolean                  changeExposureInHDRP  // 0x19
  private           System.Boolean                  changeVisualEnvironmentInHDRP  // 0x1a
  private           System.Boolean                  changeFogInHDRP  // 0x1b
  private           System.Boolean                  changeCloudsInHDRP  // 0x1c
  private           System.Boolean                  changeMotionBlurInHDRP  // 0x1d
  private           System.Boolean                  upgradeLightsInHDRP  // 0x1e
  private           System.Boolean                  upgradeCamerasInHDRP  // 0x1f
PROPERTIES:
  UpgradeInputModule  get=0x04D86340  set=0x04D86350
  ChangeExposureInHDRP  get=0x04D866A0  set=0x04D866C0
  ChangeVisualEnvironmentInHDRP  get=0x015AF940  set=0x04D866E0
  ChangeFogInHDRP  get=0x04D895D0  set=0x04D89600
  ChangeCloudsInHDRP  get=0x04D86150  set=0x04D86160
  ChangeMotionBlurInHDRP  get=0x04D88310  set=0x04D89610
  UpgradeLightsInHDRP  get=0x04D895F0  set=0x04D89630
  UpgradeCamerasInHDRP  get=0x04D895E0  set=0x04D89620
METHODS:
  RVA=0x0690C498  token=0x6000042  System.Void OnEnable()
  RVA=0x041E1670  token=0x6000043  System.Void TryApplyURP()
  RVA=0x04D83230  token=0x6000044  System.Void TryApplyHDRP()
  RVA=0x041E1670  token=0x6000045  System.Void TryCreateVolume()
  RVA=0x041E1670  token=0x6000046  System.Void TryUpgradeLights()
  RVA=0x041E1670  token=0x6000047  System.Void TryUpgradeCameras()
  RVA=0x041E1670  token=0x6000048  System.Void TryUpgradeEventSystem()
  RVA=0x0690C4A8  token=0x6000049  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwDemoButton
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
  private           CW.Common.CwDemoButton.LinkType link  // 0x18
  private           System.String                   urlTarget  // 0x20
  private           UnityEngine.Transform           isolateTarget  // 0x28
  private           CW.Common.CwDemoButton.ToggleTypeisolateToggle  // 0x30
  private           UnityEngine.CanvasGroup         cachedCanvasGroup  // 0x38
  private           UnityEngine.Transform           previousChild  // 0x40
PROPERTIES:
  Link  get=0x011EF5B0  set=0x011EF9B0
  UrlTarget  get=0x04D862C0  set=0x05390F40
  IsolateTarget  get=0x04D86240  set=0x02FDB880
  IsolateToggle  get=0x04D864F0  set=0x04D86740
METHODS:
  RVA=0x0690BDC8  token=0x6000052  System.Void OnEnable()
  RVA=0x0690C350  token=0x6000053  System.Void Update()
  RVA=0x0690BE10  token=0x6000054  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0690BCCC  token=0x6000055  System.Int32 GetCurrentLevel()
  RVA=0x0690BD40  token=0x6000056  System.Int32 GetLevelCount()
  RVA=0x0690BD80  token=0x6000057  System.Void LoadLevel(System.Int32 index)
  RVA=0x05393520  token=0x6000058  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwDemoButtonBuilder
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x50
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.GameObject          buttonPrefab  // 0x18
  private           UnityEngine.RectTransform       buttonRoot  // 0x20
  private           UnityEngine.Sprite              icon  // 0x28
  private           UnityEngine.Color               color  // 0x30
  private           System.String                   overrideName  // 0x40
  private           UnityEngine.GameObject          clone  // 0x48
PROPERTIES:
  ButtonPrefab  get=0x0385B100  set=0x05392C40
  ButtonRoot  get=0x04D862C0  set=0x05390F40
  Icon  get=0x04D86240  set=0x02FDB880
  Color  get=0x04D87480  set=0x04D87490
  OverrideName  get=0x04D85A60  set=0x042B4AE0
METHODS:
  RVA=0x0690BA08  token=0x6000063  System.Void Build()
  RVA=0x0690B970  token=0x6000064  System.Void BuildAll()
  RVA=0x0690BC2C  token=0x6000065  UnityEngine.GameObject DoInstantiate()
  RVA=0x0690BC94  token=0x6000066  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwLightIntensity
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Single                   multiplier  // 0x18
  private           System.Single                   intensityInStandard  // 0x1c
  private           System.Single                   intensityInURP  // 0x20
  private           System.Single                   intensityInHDRP  // 0x24
  private           UnityEngine.Light               cachedLight  // 0x28
  private           System.Boolean                  cachedLightSet  // 0x30
PROPERTIES:
  Multiplier  get=0x04D86380  set=0x04D86390
  IntensityInStandard  get=0x04D868F0  set=0x04D86900
  IntensityInURP  get=0x011EC8A0  set=0x011EC9E0
  IntensityInHDRP  get=0x04D89850  set=0x04D89860
  CachedLight  get=0x06911748
METHODS:
  RVA=0x069116B4  token=0x6000070  System.Void Update()
  RVA=0x0691162C  token=0x6000071  System.Void ApplyIntensity(System.Single intensity)
  RVA=0x06911714  token=0x6000072  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwRotate
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Vector3             angularVelocity  // 0x18
  private           UnityEngine.Space               relativeTo  // 0x24
PROPERTIES:
  AngularVelocity  get=0x04D89870  set=0x04D89890
  RelativeTo  get=0x011F0020  set=0x011F0030
METHODS:
  RVA=0x06911D04  token=0x6000077  System.Void Update()
  RVA=0x06911D98  token=0x6000078  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwChild
TYPE:  abstract class
TOKEN: 0x200000C
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x0690B8F4  token=0x6000079  System.Void DestroyGameObjectIfInvalid()
  RVA=-1  // abstract  token=0x600007A  CW.Common.CwChild.IHasChildren GetParent()
  RVA=0x041E1670  token=0x600007B  System.Void Start()
  RVA=0x05393520  token=0x600007C  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwDepthTextureMode
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.DepthTextureMode    depthMode  // 0x18
  private           UnityEngine.Camera              cachedCamera  // 0x20
PROPERTIES:
  DepthMode  get=0x011EF5B0  set=0x0690C56C
METHODS:
  RVA=0x0690C4CC  token=0x6000080  System.Void UpdateDepthMode()
  RVA=0x0690C564  token=0x6000081  System.Void Update()
  RVA=0x05393520  token=0x6000082  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwFollow
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x50
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           CW.Common.CwFollow.FollowType   follow  // 0x18
  private           UnityEngine.Transform           target  // 0x20
  private           System.Single                   damping  // 0x28
  private           System.Boolean                  rotate  // 0x2c
  private           System.Boolean                  ignoreZ  // 0x2d
  private           CW.Common.CwFollow.UpdateType   followIn  // 0x30
  private           UnityEngine.Vector3             localPosition  // 0x34
  private           UnityEngine.Vector3             localRotation  // 0x40
PROPERTIES:
  Follow  get=0x011EF5B0  set=0x011EF9B0
  Target  get=0x04D862C0  set=0x05390F40
  Damping  get=0x04D86BF0  set=0x04D86C00
  Rotate  get=0x04D869F0  set=0x04D86A00
  IgnoreZ  get=0x04D86CB0  set=0x04D86CE0
  FollowIn  get=0x04D864F0  set=0x04D86740
  LocalPosition  get=0x04D89640  set=0x04D89680
  LocalRotation  get=0x04D89660  set=0x04D89690
METHODS:
  RVA=0x0690C588  token=0x6000093  System.Void UpdatePosition()
  RVA=0x0690C848  token=0x6000094  System.Void Update()
  RVA=0x0690C578  token=0x6000095  System.Void LateUpdate()
  RVA=0x0690C858  token=0x6000096  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwInputManager
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.LayerMask           guiLayers  // 0x18
  private   static  System.Action<CW.Common.CwInputManager.Finger>OnFingerDown  // static @ 0x0
  private   static  System.Action<CW.Common.CwInputManager.Finger>OnFingerUpdate  // static @ 0x8
  private   static  System.Action<CW.Common.CwInputManager.Finger>OnFingerUp  // static @ 0x10
  public    static  System.Int32                    MOUSE_FINGER_INDEX  // const
  public    static  System.Int32                    HOVER_FINGER_INDEX  // const
  private   static  System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>tempRaycastResults  // static @ 0x18
  private   static  UnityEngine.EventSystems.PointerEventDatatempPointerEventData  // static @ 0x20
  private   static  UnityEngine.EventSystems.EventSystemtempEventSystem  // static @ 0x28
  private   static  System.Collections.Generic.List<CW.Common.CwInputManager.Finger>fingers  // static @ 0x30
  private   static  System.Collections.Generic.List<CW.Common.CwInputManager.Finger>filteredFingers  // static @ 0x38
  private   static  System.Collections.Generic.Stack<CW.Common.CwInputManager.Finger>pool  // static @ 0x40
PROPERTIES:
  GuiLayers  get=0x011EF5B0  set=0x011EF9B0
  Fingers  get=0x06910CDC
  ScaleFactor  get=0x06910D2C
EVENTS:
  OnFingerDown  add=add_OnFingerDown  remove=remove_OnFingerDown
  OnFingerUpdate  add=add_OnFingerUpdate  remove=remove_OnFingerUpdate
  OnFingerUp  add=add_OnFingerUp  remove=remove_OnFingerUp
METHODS:
  RVA=0x0690FC48  token=0x60000A1  System.Collections.Generic.List<CW.Common.CwInputManager.Finger> GetFingers(System.Boolean ignoreStartedOverGui, System.Boolean ignoreHover)
  RVA=0x0690FF6C  token=0x60000A2  System.Boolean PointOverGui(UnityEngine.Vector2 screenPosition, System.Int32 guiLayers)
  RVA=0x0690FFE0  token=0x60000A3  System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> RaycastGui(UnityEngine.Vector2 screenPosition, System.Int32 guiLayers)
  RVA=0x0690F5C8  token=0x60000A4  UnityEngine.Vector2 GetAveragePosition(System.Collections.Generic.List<CW.Common.CwInputManager.Finger> fingers)
  RVA=0x0690F40C  token=0x60000A5  UnityEngine.Vector2 GetAverageOldPosition(System.Collections.Generic.List<CW.Common.CwInputManager.Finger> fingers)
  RVA=0x0690F784  token=0x60000A6  UnityEngine.Vector2 GetAveragePullScaled(System.Collections.Generic.List<CW.Common.CwInputManager.Finger> fingers)
  RVA=0x0690F214  token=0x60000A7  UnityEngine.Vector2 GetAverageDeltaScaled(System.Collections.Generic.List<CW.Common.CwInputManager.Finger> fingers)
  RVA=0x0690F97C  token=0x60000A8  System.Single GetAverageTwistRadians(System.Collections.Generic.List<CW.Common.CwInputManager.Finger> fingers)
  RVA=0x0690EFC4  token=0x60000A9  System.Void EnsureThisComponentExists()
  RVA=0x06910378  token=0x60000AA  System.Void Update()
  RVA=0x0690F0D4  token=0x60000AB  CW.Common.CwInputManager.Finger FindFinger(System.Int32 index)
  RVA=0x0690EDA0  token=0x60000AC  System.Void AddFinger(System.Int32 index, UnityEngine.Vector2 screenPosition, System.Single pressure, System.Boolean set)
  RVA=0x0690FE5C  token=0x60000AD  UnityEngine.Vector2 Hermite(UnityEngine.Vector2 a, UnityEngine.Vector2 b, UnityEngine.Vector2 c, UnityEngine.Vector2 d, System.Single t)
  RVA=0x04D89730  token=0x60000AE  System.Single HermiteInterpolate(System.Single y0, System.Single y1, System.Single y2, System.Single y3, System.Single mu, System.Single mu2, System.Single mu3)
  RVA=0x04D89700  token=0x60000AF  System.Single GetRadians(UnityEngine.Vector2 screenPosition, UnityEngine.Vector2 referencePoint)
  RVA=0x0690FB60  token=0x60000B0  System.Single GetDeltaRadians(CW.Common.CwInputManager.Finger finger, UnityEngine.Vector2 referencePoint, UnityEngine.Vector2 lastReferencePoint)
  RVA=0x06910A68  token=0x60000B1  System.Void .ctor()
  RVA=0x069108B0  token=0x60000B2  System.Void .cctor()
END_CLASS

CLASS: CW.Common.CwRenderTextureManager
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Int32                    lifetime  // 0x18
PROPERTIES:
  Lifetime  get=0x011EF5B0  set=0x011EF9B0
METHODS:
  RVA=0x069117A0  token=0x60000C4  UnityEngine.RenderTexture GetTemporary(UnityEngine.RenderTextureDescriptor desc, System.String title)
  RVA=0x06911850  token=0x60000C5  UnityEngine.RenderTexture ReleaseTemporary(UnityEngine.RenderTexture renderTexture)
  RVA=0x069118BC  token=0x60000C6  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwRoot
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.Collections.Generic.List<CW.Common.CwRoot>instances  // static @ 0x0
PROPERTIES:
  Exists  get=0x06911BF8
  Root  get=0x06911C58
METHODS:
  RVA=0x069118D8  token=0x60000C9  UnityEngine.Transform GetRoot()
  RVA=0x06911A50  token=0x60000CA  System.Void OnEnable()
  RVA=0x069119E0  token=0x60000CB  System.Void OnDisable()
  RVA=0x05393520  token=0x60000CC  System.Void .ctor()
  RVA=0x06911B6C  token=0x60000CD  System.Void .cctor()
END_CLASS

CLASS: CW.Common.CwSeedAttribute
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60000CE  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwShared
TYPE:  static class
TOKEN: 0x200001B
SIZE:  0x10
FIELDS:
  public    static  System.String                   HelpUrlPrefix  // const
  public    static  System.String                   ComponentMenuPrefix  // const
  public    static  System.String                   GameObjectMenuPrefix  // const
METHODS:
END_CLASS

CLASS: CW.Common.CwGuide
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x40
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public            System.String                   LongName  // 0x18
  public            System.String                   ShortName  // 0x20
  public            UnityEngine.TextAsset           Documentation  // 0x28
  private           UnityEngine.Texture2D           icon  // 0x30
  private           System.String                   version  // 0x38
PROPERTIES:
  Icon  get=0x0690C880
  Version  get=0x0690C964
METHODS:
  RVA=0x03573370  token=0x60000D1  System.Void .ctor()
END_CLASS

CLASS: CW.Common.CwHelper
TYPE:  static class
TOKEN: 0x200001D
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Stack<UnityEngine.Random.State>seedStates  // static @ 0x0
  public    static  System.Collections.Generic.List<UnityEngine.Material>tempMaterials  // static @ 0x8
  public    static  System.Collections.Generic.List<UnityEngine.MaterialPropertyBlock>tempProperties  // static @ 0x10
  private   static  System.Action<UnityEngine.Camera>OnCameraPreRender  // static @ 0x18
  private   static  System.Action<UnityEngine.Camera>OnCameraPostRender  // static @ 0x20
  private   static  System.Collections.Generic.Stack<UnityEngine.RenderTexture>actives  // static @ 0x28
  private   static  System.Int32                    uniqueSeed  // static @ 0x30
  private   static  System.Collections.Generic.List<UnityEngine.Material>materials  // static @ 0x38
EVENTS:
  OnCameraPreRender  add=add_OnCameraPreRender  remove=remove_OnCameraPreRender
  OnCameraPostRender  add=add_OnCameraPostRender  remove=remove_OnCameraPostRender
METHODS:
  RVA=0x0690E5AC  token=0x60000D6  System.Void .cctor()
  RVA=-1  // generic def  token=0x60000D7  T GetOrAddComponent(UnityEngine.GameObject gameObject, System.Boolean recordUndo)
  RVA=-1  // generic def  token=0x60000D8  T AddComponent(UnityEngine.GameObject gameObject, System.Boolean recordUndo)
  RVA=0x0690DAE4  token=0x60000D9  System.Boolean IndexInMask(System.Int32 index, System.Int32 mask)
  RVA=0x0690D728  token=0x60000DA  UnityEngine.Camera GetCamera(UnityEngine.Camera currentCamera, UnityEngine.GameObject gameObject)
  RVA=0x0690D7EC  token=0x60000DB  UnityEngine.Vector3 GetObserverPosition(UnityEngine.Transform observer)
  RVA=0x0690D5D0  token=0x60000DC  System.Boolean Enabled(UnityEngine.Behaviour b)
  RVA=0x0690CEAC  token=0x60000DD  System.Void BeginSeed()
  RVA=0x0690CE14  token=0x60000DE  System.Void BeginSeed(System.Int32 newSeed)
  RVA=0x0690D6A4  token=0x60000DF  System.Void EndSeed()
  RVA=0x0690CF30  token=0x60000E0  UnityEngine.Color Brighten(UnityEngine.Color color, System.Single brightness, System.Boolean convertToGamma)
  RVA=0x04D896C0  token=0x60000E1  UnityEngine.Color Premultiply(UnityEngine.Color color)
  RVA=0x0690DFE4  token=0x60000E2  System.Single Saturate(System.Single c)
  RVA=0x0690E010  token=0x60000E3  UnityEngine.Color Saturate(UnityEngine.Color c)
  RVA=-1  // generic def  token=0x60000E4  System.Void Resize(System.Collections.Generic.List<T> list, System.Int32 size)
  RVA=0x0690E41C  token=0x60000E5  System.Single Sharpness(System.Single a, System.Single p)
  RVA=0x0690E4E0  token=0x60000E6  UnityEngine.Color ToLinear(UnityEngine.Color gamma)
  RVA=0x0690E524  token=0x60000E7  System.Single ToLinear(System.Single gamma)
  RVA=0x0690E49C  token=0x60000E8  UnityEngine.Color ToGamma(UnityEngine.Color linear)
  RVA=0x0690E468  token=0x60000E9  System.Single ToGamma(System.Single linear)
  RVA=0x0690E558  token=0x60000EA  System.Single UniformScale(UnityEngine.Vector3 scale)
  RVA=0x0690CD88  token=0x60000EB  System.Void BeginActive(UnityEngine.RenderTexture renderTexture)
  RVA=0x0690D634  token=0x60000EC  System.Void EndActive()
  RVA=0x0690E0A0  token=0x60000ED  System.Void SetTempMaterial(UnityEngine.Material material)
  RVA=0x0690E16C  token=0x60000EE  System.Void SetTempMaterial(UnityEngine.Material material1, UnityEngine.Material material2)
  RVA=0x0690E270  token=0x60000EF  System.Void SetTempMaterial(System.Collections.Generic.List<UnityEngine.Material> materials)
  RVA=0x0690E350  token=0x60000F0  System.Void SetTempMaterial(UnityEngine.MaterialPropertyBlock properties)
  RVA=0x0690CB38  token=0x60000F1  System.Void AddMaterial(UnityEngine.Renderer r, UnityEngine.Material m)
  RVA=0x0690DD60  token=0x60000F2  System.Void ReplaceMaterial(UnityEngine.Renderer r, UnityEngine.Material m)
  RVA=0x0690DB68  token=0x60000F3  System.Void RemoveMaterial(UnityEngine.Renderer r, UnityEngine.Material m)
  RVA=0x0690D42C  token=0x60000F4  UnityEngine.Texture2D CreateTempTexture2D(System.String name, System.Int32 width, System.Int32 height, UnityEngine.TextureFormat format, System.Boolean mips, System.Boolean linear)
  RVA=0x0690D2CC  token=0x60000F5  UnityEngine.Material CreateTempMaterial(System.String materialName, System.String shaderName)
  RVA=0x0690D3A0  token=0x60000F6  UnityEngine.Material CreateTempMaterial(System.String materialName, UnityEngine.Shader shader)
  RVA=0x0690D240  token=0x60000F7  UnityEngine.Material CreateTempMaterial(System.String materialName, UnityEngine.Material source)
  RVA=-1  // generic def  token=0x60000F8  T Destroy(T o)
  RVA=0x0690D138  token=0x60000F9  UnityEngine.GameObject CreateGameObject(System.String name, System.Int32 layer, UnityEngine.Transform parent, System.String recordUndo)
  RVA=0x0690CFE0  token=0x60000FA  UnityEngine.GameObject CreateGameObject(System.String name, System.Int32 layer, UnityEngine.Transform parent, UnityEngine.Vector3 localPosition, UnityEngine.Quaternion localRotation, UnityEngine.Vector3 localScale, System.String recordUndo)
  RVA=-1  // generic def  token=0x60000FB  T CreateElement(UnityEngine.Transform parent)
  RVA=0x0690DB30  token=0x60000FC  System.Single Reciprocal(System.Single v)
  RVA=0x0690DB4C  token=0x60000FD  System.Double Reciprocal(System.Double v)
  RVA=0x0690D5A8  token=0x60000FE  System.Single Divide(System.Single a, System.Single b)
  RVA=0x0690D5BC  token=0x60000FF  System.Double Divide(System.Double a, System.Double b)
  RVA=0x0690CACC  token=0x6000100  System.Single Acos(System.Single v)
  RVA=0x0690CA60  token=0x6000101  System.Double Acos(System.Double v)
  RVA=0x0690D560  token=0x6000102  System.Single DampenFactor(System.Single speed, System.Single elapsed)
  RVA=0x0690D4E8  token=0x6000103  System.Single DampenFactor(System.Single damping, System.Single deltaTime, System.Single linear)
  RVA=0x04D896A0  token=0x6000104  System.Single Atan2(UnityEngine.Vector2 xy)
  RVA=0x0690DAF4  token=0x6000105  System.Int32 Mod(System.Int32 a, System.Int32 b)
  RVA=0x0690DB08  token=0x6000106  System.Single Mod(System.Single a, System.Single b)
  RVA=0x0690D8B8  token=0x6000107  UnityEngine.Texture2D GetReadableCopy(UnityEngine.Texture texture, UnityEngine.TextureFormat format, System.Boolean mipMaps, System.Int32 width, System.Int32 height)
END_CLASS

CLASS: CW.Common.CwInput
TYPE:  static class
TOKEN: 0x200001F
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<UnityEngine.KeyCode,UnityEngine.InputSystem.Key>keyMapping  // static @ 0x0
METHODS:
  RVA=0x03DFA4A0  token=0x600010E  System.Void Enable()
  RVA=0x06911208  token=0x600010F  UnityEngine.InputSystem.Controls.ButtonControl GetMouseButtonControl(System.Int32 index)
  RVA=0x06910FAC  token=0x6000110  UnityEngine.InputSystem.Controls.ButtonControl GetButtonControl(UnityEngine.KeyCode oldKey)
  RVA=0x06911484  token=0x6000111  System.Int32 GetTouchCount()
  RVA=0x069114F0  token=0x6000112  System.Void GetTouch(System.Int32 index, System.Int32& id, UnityEngine.Vector2& position, System.Single& pressure, System.Boolean& set)
  RVA=0x06911308  token=0x6000113  UnityEngine.Vector2 GetMousePosition()
  RVA=0x0691114C  token=0x6000114  System.Boolean GetKeyWentDown(UnityEngine.KeyCode oldKey)
  RVA=0x069110F8  token=0x6000115  System.Boolean GetKeyIsHeld(UnityEngine.KeyCode oldKey)
  RVA=0x069111A0  token=0x6000116  System.Boolean GetKeyWentUp(UnityEngine.KeyCode oldKey)
  RVA=0x06911368  token=0x6000117  System.Boolean GetMouseWentDown(System.Int32 index)
  RVA=0x069112B4  token=0x6000118  System.Boolean GetMouseIsHeld(System.Int32 index)
  RVA=0x069113BC  token=0x6000119  System.Boolean GetMouseWentUp(System.Int32 index)
  RVA=0x06911410  token=0x600011A  System.Single GetMouseWheelDelta()
  RVA=0x069112A0  token=0x600011B  System.Boolean GetMouseExists()
  RVA=0x069111F4  token=0x600011C  System.Boolean GetKeyboardExists()
  RVA=0x04548E40  token=0x600011D  System.Void .cctor()
END_CLASS

CLASS: CW.Common.CwShaderBundle
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x38
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private           System.String                   title  // 0x18
  private           UnityEngine.Shader              target  // 0x20
  private           System.Int32                    variantHash  // 0x28
  private           System.Int32                    projectHash  // 0x2c
  private           System.Collections.Generic.List<CW.Common.CwShaderBundle.ShaderVariant>variants  // 0x30
PROPERTIES:
  Title  get=0x0385B100  set=0x05392C40
  Target  get=0x04D862C0  set=0x05390F40
  VariantHash  get=0x011F2A90  set=0x011F2AA0
  ProjectHash  get=0x04D86710  set=0x04D86720
  Variants  get=0x06912020
  Dirty  get=0x06911F14
METHODS:
  RVA=0x06911E70  token=0x6000128  System.Int32 GetProjectHash()
  RVA=0x06911DC8  token=0x6000129  CW.Common.CwShaderBundle.Pipeline DetectProjectPipeline()
  RVA=0x06911EF8  token=0x600012A  System.Boolean IsStandard(CW.Common.CwShaderBundle.Pipeline pipe)
  RVA=0x06911ECC  token=0x600012B  System.Boolean IsScriptable(CW.Common.CwShaderBundle.Pipeline pipe)
  RVA=0x06911F00  token=0x600012C  System.Boolean IsURP(CW.Common.CwShaderBundle.Pipeline pipe)
  RVA=0x06911EB8  token=0x600012D  System.Boolean IsHDRP(CW.Common.CwShaderBundle.Pipeline pipe)
  RVA=0x03573370  token=0x600012E  System.Void .ctor()
END_CLASS

