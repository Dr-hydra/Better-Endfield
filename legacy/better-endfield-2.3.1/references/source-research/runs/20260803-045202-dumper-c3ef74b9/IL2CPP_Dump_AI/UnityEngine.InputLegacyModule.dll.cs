// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.InputLegacyModule.dll
// Classes:  14
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

CLASS: HitInfo
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x20
FIELDS:
  public            UnityEngine.GameObject          target  // 0x10
  public            UnityEngine.Camera              camera  // 0x18
METHODS:
  RVA=0x04525D60  token=0x6000066  System.Void SendMessage(System.String name)
  RVA=0x03251050  token=0x6000067  System.Boolean op_Implicit(UnityEngine.SendMouseEvents.HitInfo exists)
  RVA=0x03251120  token=0x6000068  System.Boolean Compare(UnityEngine.SendMouseEvents.HitInfo lhs, UnityEngine.SendMouseEvents.HitInfo rhs)
END_CLASS

CLASS: UnityEngine.TouchPhase
TYPE:  sealed struct
TOKEN: 0x2000002
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TouchPhase          Began  // const
  public    static  UnityEngine.TouchPhase          Moved  // const
  public    static  UnityEngine.TouchPhase          Stationary  // const
  public    static  UnityEngine.TouchPhase          Ended  // const
  public    static  UnityEngine.TouchPhase          Canceled  // const
METHODS:
END_CLASS

CLASS: UnityEngine.IMECompositionMode
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.IMECompositionMode  Auto  // const
  public    static  UnityEngine.IMECompositionMode  On  // const
  public    static  UnityEngine.IMECompositionMode  Off  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TouchType
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TouchType           Direct  // const
  public    static  UnityEngine.TouchType           Indirect  // const
  public    static  UnityEngine.TouchType           Stylus  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Touch
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x54
FIELDS:
  private           System.Int32                    m_FingerId  // 0x10
  private           UnityEngine.Vector2             m_Position  // 0x14
  private           UnityEngine.Vector2             m_RawPosition  // 0x1c
  private           UnityEngine.Vector2             m_PositionDelta  // 0x24
  private           System.Single                   m_TimeDelta  // 0x2c
  private           System.Int32                    m_TapCount  // 0x30
  private           UnityEngine.TouchPhase          m_Phase  // 0x34
  private           UnityEngine.TouchType           m_Type  // 0x38
  private           System.Single                   m_Pressure  // 0x3c
  private           System.Single                   m_maximumPossiblePressure  // 0x40
  private           System.Single                   m_Radius  // 0x44
  private           System.Single                   m_RadiusVariance  // 0x48
  private           System.Single                   m_AltitudeAngle  // 0x4c
  private           System.Single                   m_AzimuthAngle  // 0x50
PROPERTIES:
  fingerId  get=0x04DBA750
  position  get=0x04DBD190  set=0x04DBD1F0
  rawPosition  get=0x04DBD1C0  set=0x04DBD200
  deltaPosition  get=0x04D963F0  set=0x04DBD1E0
  deltaTime  get=0x04DBD180
  tapCount  get=0x04DBBE60
  phase  get=0x04DBA990
  pressure  get=0x04DBD1B0
  maximumPossiblePressure  get=0x04DB9A40
  type  get=0x04DBBEC0
  altitudeAngle  get=0x05391670
  azimuthAngle  get=0x05391640
  radius  get=0x053957B0
  radiusVariance  get=0x05391630
METHODS:
END_CLASS

CLASS: UnityEngine.DeviceOrientation
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.DeviceOrientation   Unknown  // const
  public    static  UnityEngine.DeviceOrientation   Portrait  // const
  public    static  UnityEngine.DeviceOrientation   PortraitUpsideDown  // const
  public    static  UnityEngine.DeviceOrientation   LandscapeLeft  // const
  public    static  UnityEngine.DeviceOrientation   LandscapeRight  // const
  public    static  UnityEngine.DeviceOrientation   FaceUp  // const
  public    static  UnityEngine.DeviceOrientation   FaceDown  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AccelerationEvent
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x20
FIELDS:
  private           System.Single                   x  // 0x10
  private           System.Single                   y  // 0x14
  private           System.Single                   z  // 0x18
  private           System.Single                   m_TimeDelta  // 0x1c
METHODS:
END_CLASS

CLASS: UnityEngine.Gyroscope
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x18
FIELDS:
  private           System.Int32                    m_GyroIndex  // 0x10
METHODS:
  RVA=0x014F51F0  token=0x6000012  System.Void .ctor(System.Int32 index)
END_CLASS

CLASS: UnityEngine.LocationService
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000013  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Compass
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000014  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.CameraRaycastHelper
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x05391B2C  token=0x6000015  UnityEngine.GameObject RaycastTry(UnityEngine.Camera cam, UnityEngine.Ray ray, System.Single distance, System.Int32 layerMask)
  RVA=0x05391B14  token=0x6000016  UnityEngine.GameObject RaycastTry2D(UnityEngine.Camera cam, UnityEngine.Ray ray, System.Single distance, System.Int32 layerMask)
  RVA=0x03E00C70  token=0x6000017  UnityEngine.GameObject RaycastTry_Injected(UnityEngine.Camera cam, UnityEngine.Ray& ray, System.Single distance, System.Int32 layerMask)
  RVA=0x03E00CE0  token=0x6000018  UnityEngine.GameObject RaycastTry2D_Injected(UnityEngine.Camera cam, UnityEngine.Ray& ray, System.Single distance, System.Int32 layerMask)
END_CLASS

CLASS: UnityEngine.Input
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.LocationService     locationServiceInstance  // static @ 0x0
  private   static  UnityEngine.Compass             compassInstance  // static @ 0x8
  private   static  UnityEngine.Gyroscope           s_MainGyro  // static @ 0x10
PROPERTIES:
  simulateMouseWithTouches  get=0x0B417F6C  set=0x04B87920
  anyKey  get=0x0B417B80
  anyKeyDown  get=0x040C1FF0
  inputString  get=0x0B417E54
  mousePosition  get=0x03400200
  mouseScrollDelta  get=0x037CF7C0
  imeCompositionMode  get=0x0B417DFC  set=0x04D512B0
  compositionString  get=0x04118270
  imeIsSelected  get=0x0B417E28
  compositionCursorPos  get=0x0B417CD4  set=0x0B418124
  eatKeyPressOnTextFieldFocus  get=0x0B417D24  set=0x0B418140
  mousePresent  get=0x03F9BAB0
  touchCount  get=0x037D6BE0
  touchPressureSupported  get=0x0B417FC4
  stylusTouchSupported  get=0x0B417F98
  touchSupported  get=0x0B417FF0
  multiTouchEnabled  get=0x0B417F40  set=0x04B879D0
  isGyroAvailable  get=0x0B417E80
  deviceOrientation  get=0x0B417CF8
  acceleration  get=0x0B417B5C
  compensateSensors  get=0x0B417C74  set=0x0B4180F0
  accelerationEventCount  get=0x0B417A60
  backButtonLeavesApp  get=0x0B417BAC  set=0x0B4180BC
  location  get=0x0B417EAC
  compass  get=0x0B417BD8
  gyro  get=0x0B417D50
  touches  get=0x0B41801C
  accelerationEvents  get=0x0B417A8C
METHODS:
  RVA=0x03D9D9F0  token=0x6000019  System.Boolean GetKeyInt(UnityEngine.KeyCode key)
  RVA=0x0B4178E4  token=0x600001A  System.Boolean GetKeyString(System.String name)
  RVA=0x03D536F0  token=0x600001B  System.Boolean GetKeyUpInt(UnityEngine.KeyCode key)
  RVA=0x0B417918  token=0x600001C  System.Boolean GetKeyUpString(System.String name)
  RVA=0x03D25200  token=0x600001D  System.Boolean GetKeyDownInt(UnityEngine.KeyCode key)
  RVA=0x0B4178A4  token=0x600001E  System.Boolean GetKeyDownString(System.String name)
  RVA=0x0B4177B0  token=0x600001F  System.Single GetAxis(System.String axisName)
  RVA=0x030BA490  token=0x6000020  System.Single GetAxisRaw(System.String axisName)
  RVA=0x030BADB0  token=0x6000021  System.Boolean GetButton(System.String buttonName)
  RVA=0x0B4177E4  token=0x6000022  System.Boolean GetButtonDown(System.String buttonName)
  RVA=0x0B417818  token=0x6000023  System.Boolean GetButtonUp(System.String buttonName)
  RVA=0x033FE9D0  token=0x6000024  System.Boolean GetMouseButton(System.Int32 button)
  RVA=0x034002D0  token=0x6000025  System.Boolean GetMouseButtonDown(System.Int32 button)
  RVA=0x03E2FA40  token=0x6000026  System.Boolean GetMouseButtonUp(System.Int32 button)
  RVA=0x0B4179DC  token=0x6000027  System.Void ResetInputAxes()
  RVA=0x0B417878  token=0x6000028  System.String[] GetJoystickNames()
  RVA=0x0B4179A4  token=0x6000029  UnityEngine.Touch GetTouch(System.Int32 index)
  RVA=0x0B417788  token=0x600002A  UnityEngine.AccelerationEvent GetAccelerationEvent(System.Int32 index)
  RVA=0x0539205C  token=0x600002B  System.Boolean GetKey(UnityEngine.KeyCode key)
  RVA=0x0B417958  token=0x600002C  System.Boolean GetKey(System.String name)
  RVA=0x05392068  token=0x600002D  System.Boolean GetKeyUp(UnityEngine.KeyCode key)
  RVA=0x0B41794C  token=0x600002E  System.Boolean GetKeyUp(System.String name)
  RVA=0x05392074  token=0x600002F  System.Boolean GetKeyDown(UnityEngine.KeyCode key)
  RVA=0x0B4178D8  token=0x6000030  System.Boolean GetKeyDown(System.String name)
  RVA=0x041E1670  token=0x6000031  System.Void SimulateTouch(UnityEngine.Touch touch)
  RVA=0x0B417A4C  token=0x6000032  System.Void SimulateTouchInternal(UnityEngine.Touch touch, System.Int64 timestamp)
  RVA=0x0B41784C  token=0x6000053  System.Int32 GetGyroInternal()
  RVA=0x03400290  token=0x6000057  System.Boolean CheckDisabled()
  RVA=0x041E1670  token=0x6000058  System.Void .ctor()
  RVA=0x0B417964  token=0x6000059  System.Void GetTouch_Injected(System.Int32 index, UnityEngine.Touch& ret)
  RVA=0x0B417748  token=0x600005A  System.Void GetAccelerationEvent_Injected(System.Int32 index, UnityEngine.AccelerationEvent& ret)
  RVA=0x0B417A08  token=0x600005B  System.Void SimulateTouchInternal_Injected(UnityEngine.Touch& touch, System.Int64 timestamp)
  RVA=0x03400250  token=0x600005C  System.Void get_mousePosition_Injected(UnityEngine.Vector3& ret)
  RVA=0x037CF810  token=0x600005D  System.Void get_mouseScrollDelta_Injected(UnityEngine.Vector2& ret)
  RVA=0x0B417CA0  token=0x600005E  System.Void get_compositionCursorPos_Injected(UnityEngine.Vector2& ret)
  RVA=0x04A31690  token=0x600005F  System.Void set_compositionCursorPos_Injected(UnityEngine.Vector2& value)
  RVA=0x0B417B28  token=0x6000060  System.Void get_acceleration_Injected(UnityEngine.Vector3& ret)
END_CLASS

CLASS: UnityEngine.SendMouseEvents
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_MouseUsed  // static @ 0x0
  private   static readonly UnityEngine.SendMouseEvents.HitInfo[]m_LastHit  // static @ 0x8
  private   static readonly UnityEngine.SendMouseEvents.HitInfo[]m_MouseDownHit  // static @ 0x10
  private   static readonly UnityEngine.SendMouseEvents.HitInfo[]m_CurrentHit  // static @ 0x18
  private   static  UnityEngine.Camera[]            m_Cameras  // static @ 0x20
  public    static  System.Func<System.Collections.Generic.KeyValuePair<System.Int32,UnityEngine.Vector2>>s_GetMouseState  // static @ 0x28
  private   static  UnityEngine.Vector2             s_MousePosition  // static @ 0x30
  private   static  System.Boolean                  s_MouseButtonPressedThisFrame  // static @ 0x38
  private   static  System.Boolean                  s_MouseButtonIsPressed  // static @ 0x39
METHODS:
  RVA=0x034000D0  token=0x6000061  System.Void UpdateMouse()
  RVA=0x0B418174  token=0x6000062  System.Void SetMouseMoved()
  RVA=0x0324FB30  token=0x6000063  System.Void DoSendMouseEvents(System.Int32 skipRTCameras)
  RVA=0x03250820  token=0x6000064  System.Void SendEvents(System.Int32 i, UnityEngine.SendMouseEvents.HitInfo hit)
  RVA=0x04CB0CF0  token=0x6000065  System.Void .cctor()
END_CLASS

