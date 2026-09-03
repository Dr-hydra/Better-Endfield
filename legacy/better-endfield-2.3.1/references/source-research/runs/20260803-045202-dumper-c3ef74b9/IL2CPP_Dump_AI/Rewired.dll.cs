// ========================================================
// Dumped by @desirepro
// Assembly: Rewired.dll
// Classes:  55
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

CLASS: DataStore
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x28
IMPLEMENTS: IDataStore
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,System.Object>_data  // 0x10
  private   readonly System.String                   _absFilePath  // 0x18
  private           Rewired.Data.UserDataStore_File.IDataHandler_dataHandler  // 0x20
METHODS:
  RVA=0x0A9C7FAC  token=0x600023E  System.Void .ctor(System.String fileName, System.String absDirectory, Rewired.Data.UserDataStore_File.IDataHandler dataHandler)
  RVA=0x0A9C7F2C  token=0x600023F  System.Boolean TryGetValue(System.String key, System.Object& value)
  RVA=0x0A9C7EB4  token=0x6000240  System.Boolean SetValue(System.String key, System.Object value)
  RVA=0x0A9C7DB8  token=0x6000241  System.Boolean Save()
  RVA=0x0A9C7C0C  token=0x6000242  System.Boolean Load()
  RVA=0x0A9C7AD8  token=0x6000243  System.Boolean Clear()
END_CLASS

CLASS: LocalFileDataHandler
TYPE:  sealed class
TOKEN: 0x2000012
SIZE:  0x20
IMPLEMENTS: IDataHandler
FIELDS:
  private   readonly System.Func<Rewired.Data.UserDataStore_File.DataFormat>_dataFormatDelegate  // 0x10
  private   readonly Rewired.Data.UserDataStore_File.Codec_codec  // 0x18
METHODS:
  RVA=0x0A9CB794  token=0x6000244  System.Void .ctor(System.Func<Rewired.Data.UserDataStore_File.DataFormat> dataFormatDelegate, Rewired.Data.UserDataStore_File.Codec codec)
  RVA=0x0A9CB3CC  token=0x6000245  System.Boolean Load(System.String absoluteFilePath, System.String& data)
  RVA=0x0A9CB5B0  token=0x6000246  System.Boolean Save(System.String absoluteFilePath, System.String data)
  RVA=0x0A9CB30C  token=0x6000247  System.Boolean Clear(System.String absoluteFilePath)
END_CLASS

CLASS: Codec
TYPE:  abstract class
TOKEN: 0x2000013
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000248  System.Byte[] Encode(System.String string)
  RVA=-1  // abstract  token=0x6000249  System.String Decode(System.Byte[] data)
  RVA=0x041E1670  token=0x600024A  System.Void .ctor()
END_CLASS

CLASS: UTF8Text
TYPE:  sealed class
TOKEN: 0x2000014
SIZE:  0x10
EXTENDS: Codec
FIELDS:
METHODS:
  RVA=0x0A9D3EBC  token=0x600024B  System.Byte[] Encode(System.String string)
  RVA=0x0A9D3E8C  token=0x600024C  System.String Decode(System.Byte[] data)
  RVA=0x041E1670  token=0x600024D  System.Void .ctor()
END_CLASS

CLASS: CLZF2
TYPE:  sealed class
TOKEN: 0x2000015
SIZE:  0x18
EXTENDS: Codec
FIELDS:
  private   readonly Rewired.Utils.Libraries.CLZF2.CLZF2_cLZF2  // 0x10
METHODS:
  RVA=0x0A9C7574  token=0x600024E  System.Void .ctor()
  RVA=0x0A9C7524  token=0x600024F  System.Byte[] Encode(System.String string)
  RVA=0x0A9C74C8  token=0x6000250  System.String Decode(System.Byte[] data)
END_CLASS

CLASS: IDataHandler
TYPE:  interface
TOKEN: 0x2000016
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000251  System.Boolean Load(System.String absoluteFilePath, System.String& data)
  RVA=-1  // abstract  token=0x6000252  System.Boolean Save(System.String absoluteFilePath, System.String data)
  RVA=-1  // abstract  token=0x6000253  System.Boolean Clear(System.String absoluteFilePath)
END_CLASS

CLASS: DataFormat
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Rewired.Data.UserDataStore_File.DataFormatText  // const
  public    static  Rewired.Data.UserDataStore_File.DataFormatBinary  // const
METHODS:
END_CLASS

CLASS: PlayerInfo
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x20
FIELDS:
  public            System.Int32                    id  // 0x10
  public            System.Boolean                  hasKeyboard  // 0x14
  public            System.Boolean                  hasMouse  // 0x15
  public            Rewired.Data.UserDataStore_KeyValue.ControllerAssignmentSaveInfo.JoystickInfo[]joysticks  // 0x18
PROPERTIES:
  joystickCount  get=0x03BA3DE0
METHODS:
  RVA=0x0A9CBB8C  token=0x60002AE  System.Int32 IndexOfJoystick(System.Int32 joystickId)
  RVA=0x0A9CBB74  token=0x60002AF  System.Boolean ContainsJoystick(System.Int32 joystickId)
  RVA=0x041E1670  token=0x60002B0  System.Void .ctor()
END_CLASS

CLASS: JoystickInfo
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x30
FIELDS:
  public            System.Guid                     instanceGuid  // 0x10
  public            System.String                   hardwareIdentifier  // 0x20
  public            System.Int32                    id  // 0x28
METHODS:
  RVA=0x041E1670  token=0x60002B1  System.Void .ctor()
END_CLASS

CLASS: ControllerAssignmentSaveInfo
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x18
FIELDS:
  public            Rewired.Data.UserDataStore_KeyValue.ControllerAssignmentSaveInfo.PlayerInfo[]players  // 0x10
PROPERTIES:
  playerCount  get=0x03141B90
METHODS:
  RVA=0x041E1670  token=0x60002A9  System.Void .ctor()
  RVA=0x0A9C766C  token=0x60002AA  System.Void .ctor(System.Int32 playerCount)
  RVA=0x0A9C75F0  token=0x60002AB  System.Int32 IndexOfPlayer(System.Int32 playerId)
  RVA=0x0A9C75D8  token=0x60002AC  System.Boolean ContainsPlayer(System.Int32 playerId)
END_CLASS

CLASS: JoystickAssignmentHistoryInfo
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x20
FIELDS:
  public    readonly Rewired.Joystick                joystick  // 0x10
  public    readonly System.Int32                    oldJoystickId  // 0x18
METHODS:
  RVA=0x0A9CB290  token=0x60002B2  System.Void .ctor(Rewired.Joystick joystick, System.Int32 oldJoystickId)
END_CLASS

CLASS: IDataStore
TYPE:  interface
TOKEN: 0x200001D
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60002B3  System.Boolean Save()
  RVA=-1  // abstract  token=0x60002B4  System.Boolean Load()
  RVA=-1  // abstract  token=0x60002B5  System.Boolean Clear()
  RVA=-1  // abstract  token=0x60002B6  System.Boolean TryGetValue(System.String key, System.Object& result)
  RVA=-1  // abstract  token=0x60002B7  System.Boolean SetValue(System.String key, System.Object value)
END_CLASS

CLASS: <>c__DisplayClass71_0
TYPE:  sealed class
TOKEN: 0x200001E
SIZE:  0x18
FIELDS:
  public            Rewired.Joystick                joystick  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60002B8  System.Void .ctor()
  RVA=0x0A98CBF4  token=0x60002B9  System.Boolean <LoadJoystickAssignmentsNow>b__0(Rewired.Data.UserDataStore_KeyValue.JoystickAssignmentHistoryInfo x)
END_CLASS

CLASS: <>c__DisplayClass71_1
TYPE:  sealed class
TOKEN: 0x200001F
SIZE:  0x18
FIELDS:
  public            Rewired.Data.UserDataStore_KeyValue.ControllerAssignmentSaveInfo.JoystickInfojoystickInfo  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60002BA  System.Void .ctor()
  RVA=0x0A9D3E64  token=0x60002BB  System.Boolean <LoadJoystickAssignmentsNow>b__1(Rewired.Data.UserDataStore_KeyValue.JoystickAssignmentHistoryInfo x)
END_CLASS

CLASS: <>c__DisplayClass71_2
TYPE:  sealed class
TOKEN: 0x2000020
SIZE:  0x18
FIELDS:
  public            Rewired.Joystick                match  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60002BC  System.Void .ctor()
  RVA=0x0A98CBF4  token=0x60002BD  System.Boolean <LoadJoystickAssignmentsNow>b__2(Rewired.Data.UserDataStore_KeyValue.JoystickAssignmentHistoryInfo x)
END_CLASS

CLASS: <LoadJoystickAssignmentsDeferred>d__73
TYPE:  sealed class
TOKEN: 0x2000021
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Rewired.Data.UserDataStore_KeyValue<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002BE  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002BF  System.Void System.IDisposable.Dispose()
  RVA=0x0A9D3C3C  token=0x60002C0  System.Boolean MoveNext()
  RVA=0x0A9D3D04  token=0x60002C2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: PlayerInfo
TYPE:  class
TOKEN: 0x2000024
SIZE:  0x20
FIELDS:
  public            System.Int32                    id  // 0x10
  public            System.Boolean                  hasKeyboard  // 0x14
  public            System.Boolean                  hasMouse  // 0x15
  public            Rewired.Data.UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo[]joysticks  // 0x18
PROPERTIES:
  joystickCount  get=0x03BA3DE0
METHODS:
  RVA=0x0A9CBB8C  token=0x600031F  System.Int32 IndexOfJoystick(System.Int32 joystickId)
  RVA=0x0A9CBB74  token=0x6000320  System.Boolean ContainsJoystick(System.Int32 joystickId)
  RVA=0x041E1670  token=0x6000321  System.Void .ctor()
END_CLASS

CLASS: JoystickInfo
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x30
FIELDS:
  public            System.Guid                     instanceGuid  // 0x10
  public            System.String                   hardwareIdentifier  // 0x20
  public            System.Int32                    id  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000322  System.Void .ctor()
END_CLASS

CLASS: ControllerAssignmentSaveInfo
TYPE:  class
TOKEN: 0x2000023
SIZE:  0x18
FIELDS:
  public            Rewired.Data.UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo[]players  // 0x10
PROPERTIES:
  playerCount  get=0x03141B90
METHODS:
  RVA=0x041E1670  token=0x600031A  System.Void .ctor()
  RVA=0x0A9C7734  token=0x600031B  System.Void .ctor(System.Int32 playerCount)
  RVA=0x0A9C75F0  token=0x600031C  System.Int32 IndexOfPlayer(System.Int32 playerId)
  RVA=0x0A9C75D8  token=0x600031D  System.Boolean ContainsPlayer(System.Int32 playerId)
END_CLASS

CLASS: JoystickAssignmentHistoryInfo
TYPE:  class
TOKEN: 0x2000026
SIZE:  0x20
FIELDS:
  public    readonly Rewired.Joystick                joystick  // 0x10
  public    readonly System.Int32                    oldJoystickId  // 0x18
METHODS:
  RVA=0x0A9CB214  token=0x6000323  System.Void .ctor(Rewired.Joystick joystick, System.Int32 oldJoystickId)
END_CLASS

CLASS: <>c__DisplayClass78_0
TYPE:  sealed class
TOKEN: 0x2000027
SIZE:  0x18
FIELDS:
  public            Rewired.Joystick                joystick  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000324  System.Void .ctor()
  RVA=0x0A98CBF4  token=0x6000325  System.Boolean <LoadJoystickAssignmentsNow>b__0(Rewired.Data.UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo x)
END_CLASS

CLASS: <>c__DisplayClass78_1
TYPE:  sealed class
TOKEN: 0x2000028
SIZE:  0x18
FIELDS:
  public            Rewired.Data.UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfojoystickInfo  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000326  System.Void .ctor()
  RVA=0x0A9D3E64  token=0x6000327  System.Boolean <LoadJoystickAssignmentsNow>b__1(Rewired.Data.UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo x)
END_CLASS

CLASS: <>c__DisplayClass78_2
TYPE:  sealed class
TOKEN: 0x2000029
SIZE:  0x18
FIELDS:
  public            Rewired.Joystick                match  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000328  System.Void .ctor()
  RVA=0x0A98CBF4  token=0x6000329  System.Boolean <LoadJoystickAssignmentsNow>b__2(Rewired.Data.UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo x)
END_CLASS

CLASS: <LoadJoystickAssignmentsDeferred>d__80
TYPE:  sealed class
TOKEN: 0x200002A
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Rewired.Data.UserDataStore_PlayerPrefs<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600032A  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600032B  System.Void System.IDisposable.Dispose()
  RVA=0x0A9D3D50  token=0x600032C  System.Boolean MoveNext()
  RVA=0x0A9D3E18  token=0x600032E  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: MouseState
TYPE:  class
TOKEN: 0x200002F
SIZE:  0x18
FIELDS:
  private           System.Collections.Generic.List<Rewired.Integration.UnityUI.RewiredPointerInputModule.ButtonState>m_TrackedButtons  // 0x10
METHODS:
  RVA=0x0A9CB878  token=0x6000365  System.Boolean AnyPressesThisFrame()
  RVA=0x0A9CB904  token=0x6000366  System.Boolean AnyReleasesThisFrame()
  RVA=0x0A9CB990  token=0x6000367  Rewired.Integration.UnityUI.RewiredPointerInputModule.ButtonState GetButtonState(System.Int32 button)
  RVA=0x0A9CBAAC  token=0x6000368  System.Void SetButtonState(System.Int32 button, UnityEngine.EventSystems.PointerEventData.FramePressState stateForMouseButton, Rewired.Integration.UnityUI.PlayerPointerEventData data)
  RVA=0x0A9CBAFC  token=0x6000369  System.Void .ctor()
END_CLASS

CLASS: MouseButtonEventData
TYPE:  class
TOKEN: 0x2000030
SIZE:  0x20
FIELDS:
  public            UnityEngine.EventSystems.PointerEventData.FramePressStatebuttonState  // 0x10
  public            Rewired.Integration.UnityUI.PlayerPointerEventDatabuttonData  // 0x18
METHODS:
  RVA=0x0A9CB864  token=0x600036A  System.Boolean PressedThisFrame()
  RVA=0x0863E6DC  token=0x600036B  System.Boolean ReleasedThisFrame()
  RVA=0x041E1670  token=0x600036C  System.Void .ctor()
END_CLASS

CLASS: ButtonState
TYPE:  class
TOKEN: 0x2000031
SIZE:  0x20
FIELDS:
  private           System.Int32                    m_Button  // 0x10
  private           Rewired.Integration.UnityUI.RewiredPointerInputModule.MouseButtonEventDatam_EventData  // 0x18
PROPERTIES:
  eventData  get=0x0385B100  set=0x05392C40
  button  get=0x02B2E2D0  set=0x014F51F0
METHODS:
  RVA=0x041E1670  token=0x6000371  System.Void .ctor()
END_CLASS

CLASS: UnityInputSource
TYPE:  sealed class
TOKEN: 0x2000032
SIZE:  0x28
IMPLEMENTS: Rewired.UI.IMouseInputSource Rewired.UI.ITouchInputSource
FIELDS:
  private           UnityEngine.Vector2             m_MousePosition  // 0x10
  private           UnityEngine.Vector2             m_MousePositionPrev  // 0x18
  private           System.Int32                    m_LastUpdatedFrame  // 0x20
PROPERTIES:
  Rewired.UI.IMouseInputSource.playerId  get=0x0A9D3F9C
  Rewired.UI.ITouchInputSource.playerId  get=0x0A9D3F9C
  Rewired.UI.IMouseInputSource.enabled  get=0x0A9D3F64
  Rewired.UI.IMouseInputSource.locked  get=0x0A9D3F7C
  Rewired.UI.IMouseInputSource.buttonCount  get=0x0A9D3F4C
  Rewired.UI.IMouseInputSource.screenPosition  get=0x0A9D3FF0
  Rewired.UI.IMouseInputSource.screenPositionDelta  get=0x0A9D3FB0
  Rewired.UI.IMouseInputSource.wheelDelta  get=0x0A9D4020
  Rewired.UI.ITouchInputSource.touchSupported  get=0x0A9D40B4
  Rewired.UI.ITouchInputSource.touchCount  get=0x0A9D409C
METHODS:
  RVA=0x0A9D3EEC  token=0x6000377  System.Boolean Rewired.UI.IMouseInputSource.GetButtonDown(System.Int32 button)
  RVA=0x0A9D3F0C  token=0x6000378  System.Boolean Rewired.UI.IMouseInputSource.GetButtonUp(System.Int32 button)
  RVA=0x0A9D3F2C  token=0x6000379  System.Boolean Rewired.UI.IMouseInputSource.GetButton(System.Int32 button)
  RVA=0x0A9D4038  token=0x600037F  UnityEngine.Touch Rewired.UI.ITouchInputSource.GetTouch(System.Int32 index)
  RVA=0x0A9D40CC  token=0x6000380  System.Void TryUpdate()
  RVA=0x04D91890  token=0x6000381  System.Void .ctor()
END_CLASS

CLASS: PlayerSetting
TYPE:  class
TOKEN: 0x2000035
SIZE:  0x20
FIELDS:
  public            System.Int32                    playerId  // 0x10
  public            System.Collections.Generic.List<Rewired.Components.PlayerMouse>playerMice  // 0x18
METHODS:
  RVA=0x0ABD38A0  token=0x60003DD  System.Void .ctor()
  RVA=0x0ABD3918  token=0x60003DE  System.Void .ctor(Rewired.Integration.UnityUI.RewiredStandaloneInputModule.PlayerSetting other)
  RVA=0x0ABD3840  token=0x60003DF  Rewired.Integration.UnityUI.RewiredStandaloneInputModule.PlayerSetting Clone()
END_CLASS

CLASS: Rewired.IGamepadTemplate
TYPE:  interface
TOKEN: 0x2000002
IMPLEMENTS: Rewired.IControllerTemplate
FIELDS:
PROPERTIES:
  actionBottomRow1  get=-1  // abstract
  a  get=-1  // abstract
  actionBottomRow2  get=-1  // abstract
  b  get=-1  // abstract
  actionBottomRow3  get=-1  // abstract
  c  get=-1  // abstract
  actionTopRow1  get=-1  // abstract
  x  get=-1  // abstract
  actionTopRow2  get=-1  // abstract
  y  get=-1  // abstract
  actionTopRow3  get=-1  // abstract
  z  get=-1  // abstract
  leftShoulder1  get=-1  // abstract
  leftBumper  get=-1  // abstract
  leftShoulder2  get=-1  // abstract
  leftTrigger  get=-1  // abstract
  rightShoulder1  get=-1  // abstract
  rightBumper  get=-1  // abstract
  rightShoulder2  get=-1  // abstract
  rightTrigger  get=-1  // abstract
  center1  get=-1  // abstract
  back  get=-1  // abstract
  center2  get=-1  // abstract
  start  get=-1  // abstract
  center3  get=-1  // abstract
  guide  get=-1  // abstract
  leftStick  get=-1  // abstract
  rightStick  get=-1  // abstract
  dPad  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Rewired.IRacingWheelTemplate
TYPE:  interface
TOKEN: 0x2000003
IMPLEMENTS: Rewired.IControllerTemplate
FIELDS:
PROPERTIES:
  wheel  get=-1  // abstract
  accelerator  get=-1  // abstract
  brake  get=-1  // abstract
  clutch  get=-1  // abstract
  shiftDown  get=-1  // abstract
  shiftUp  get=-1  // abstract
  wheelButton1  get=-1  // abstract
  wheelButton2  get=-1  // abstract
  wheelButton3  get=-1  // abstract
  wheelButton4  get=-1  // abstract
  wheelButton5  get=-1  // abstract
  wheelButton6  get=-1  // abstract
  wheelButton7  get=-1  // abstract
  wheelButton8  get=-1  // abstract
  wheelButton9  get=-1  // abstract
  wheelButton10  get=-1  // abstract
  consoleButton1  get=-1  // abstract
  consoleButton2  get=-1  // abstract
  consoleButton3  get=-1  // abstract
  consoleButton4  get=-1  // abstract
  consoleButton5  get=-1  // abstract
  consoleButton6  get=-1  // abstract
  consoleButton7  get=-1  // abstract
  consoleButton8  get=-1  // abstract
  consoleButton9  get=-1  // abstract
  consoleButton10  get=-1  // abstract
  shifter1  get=-1  // abstract
  shifter2  get=-1  // abstract
  shifter3  get=-1  // abstract
  shifter4  get=-1  // abstract
  shifter5  get=-1  // abstract
  shifter6  get=-1  // abstract
  shifter7  get=-1  // abstract
  shifter8  get=-1  // abstract
  shifter9  get=-1  // abstract
  shifter10  get=-1  // abstract
  reverseGear  get=-1  // abstract
  select  get=-1  // abstract
  start  get=-1  // abstract
  systemButton  get=-1  // abstract
  horn  get=-1  // abstract
  dPad  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Rewired.IHOTASTemplate
TYPE:  interface
TOKEN: 0x2000004
IMPLEMENTS: Rewired.IControllerTemplate
FIELDS:
PROPERTIES:
  stickTrigger  get=-1  // abstract
  stickTriggerStage2  get=-1  // abstract
  stickPinkyButton  get=-1  // abstract
  stickPinkyTrigger  get=-1  // abstract
  stickButton1  get=-1  // abstract
  stickButton2  get=-1  // abstract
  stickButton3  get=-1  // abstract
  stickButton4  get=-1  // abstract
  stickButton5  get=-1  // abstract
  stickButton6  get=-1  // abstract
  stickButton7  get=-1  // abstract
  stickButton8  get=-1  // abstract
  stickButton9  get=-1  // abstract
  stickButton10  get=-1  // abstract
  stickBaseButton1  get=-1  // abstract
  stickBaseButton2  get=-1  // abstract
  stickBaseButton3  get=-1  // abstract
  stickBaseButton4  get=-1  // abstract
  stickBaseButton5  get=-1  // abstract
  stickBaseButton6  get=-1  // abstract
  stickBaseButton7  get=-1  // abstract
  stickBaseButton8  get=-1  // abstract
  stickBaseButton9  get=-1  // abstract
  stickBaseButton10  get=-1  // abstract
  stickBaseButton11  get=-1  // abstract
  stickBaseButton12  get=-1  // abstract
  mode1  get=-1  // abstract
  mode2  get=-1  // abstract
  mode3  get=-1  // abstract
  throttleButton1  get=-1  // abstract
  throttleButton2  get=-1  // abstract
  throttleButton3  get=-1  // abstract
  throttleButton4  get=-1  // abstract
  throttleButton5  get=-1  // abstract
  throttleButton6  get=-1  // abstract
  throttleButton7  get=-1  // abstract
  throttleButton8  get=-1  // abstract
  throttleButton9  get=-1  // abstract
  throttleButton10  get=-1  // abstract
  throttleBaseButton1  get=-1  // abstract
  throttleBaseButton2  get=-1  // abstract
  throttleBaseButton3  get=-1  // abstract
  throttleBaseButton4  get=-1  // abstract
  throttleBaseButton5  get=-1  // abstract
  throttleBaseButton6  get=-1  // abstract
  throttleBaseButton7  get=-1  // abstract
  throttleBaseButton8  get=-1  // abstract
  throttleBaseButton9  get=-1  // abstract
  throttleBaseButton10  get=-1  // abstract
  throttleBaseButton11  get=-1  // abstract
  throttleBaseButton12  get=-1  // abstract
  throttleBaseButton13  get=-1  // abstract
  throttleBaseButton14  get=-1  // abstract
  throttleBaseButton15  get=-1  // abstract
  throttleSlider1  get=-1  // abstract
  throttleSlider2  get=-1  // abstract
  throttleSlider3  get=-1  // abstract
  throttleSlider4  get=-1  // abstract
  throttleDial1  get=-1  // abstract
  throttleDial2  get=-1  // abstract
  throttleDial3  get=-1  // abstract
  throttleDial4  get=-1  // abstract
  throttleWheel1Forward  get=-1  // abstract
  throttleWheel1Back  get=-1  // abstract
  throttleWheel1Press  get=-1  // abstract
  throttleWheel2Forward  get=-1  // abstract
  throttleWheel2Back  get=-1  // abstract
  throttleWheel2Press  get=-1  // abstract
  throttleWheel3Forward  get=-1  // abstract
  throttleWheel3Back  get=-1  // abstract
  throttleWheel3Press  get=-1  // abstract
  leftPedal  get=-1  // abstract
  rightPedal  get=-1  // abstract
  slidePedals  get=-1  // abstract
  stick  get=-1  // abstract
  stickMiniStick1  get=-1  // abstract
  stickMiniStick2  get=-1  // abstract
  stickHat1  get=-1  // abstract
  stickHat2  get=-1  // abstract
  stickHat3  get=-1  // abstract
  stickHat4  get=-1  // abstract
  throttle1  get=-1  // abstract
  throttle2  get=-1  // abstract
  throttleMiniStick  get=-1  // abstract
  throttleHat1  get=-1  // abstract
  throttleHat2  get=-1  // abstract
  throttleHat3  get=-1  // abstract
  throttleHat4  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Rewired.IFlightYokeTemplate
TYPE:  interface
TOKEN: 0x2000005
IMPLEMENTS: Rewired.IControllerTemplate
FIELDS:
PROPERTIES:
  leftPaddle  get=-1  // abstract
  rightPaddle  get=-1  // abstract
  leftGripButton1  get=-1  // abstract
  leftGripButton2  get=-1  // abstract
  leftGripButton3  get=-1  // abstract
  leftGripButton4  get=-1  // abstract
  leftGripButton5  get=-1  // abstract
  leftGripButton6  get=-1  // abstract
  rightGripButton1  get=-1  // abstract
  rightGripButton2  get=-1  // abstract
  rightGripButton3  get=-1  // abstract
  rightGripButton4  get=-1  // abstract
  rightGripButton5  get=-1  // abstract
  rightGripButton6  get=-1  // abstract
  centerButton1  get=-1  // abstract
  centerButton2  get=-1  // abstract
  centerButton3  get=-1  // abstract
  centerButton4  get=-1  // abstract
  centerButton5  get=-1  // abstract
  centerButton6  get=-1  // abstract
  centerButton7  get=-1  // abstract
  centerButton8  get=-1  // abstract
  wheel1Up  get=-1  // abstract
  wheel1Down  get=-1  // abstract
  wheel1Press  get=-1  // abstract
  wheel2Up  get=-1  // abstract
  wheel2Down  get=-1  // abstract
  wheel2Press  get=-1  // abstract
  consoleButton1  get=-1  // abstract
  consoleButton2  get=-1  // abstract
  consoleButton3  get=-1  // abstract
  consoleButton4  get=-1  // abstract
  consoleButton5  get=-1  // abstract
  consoleButton6  get=-1  // abstract
  consoleButton7  get=-1  // abstract
  consoleButton8  get=-1  // abstract
  consoleButton9  get=-1  // abstract
  consoleButton10  get=-1  // abstract
  mode1  get=-1  // abstract
  mode2  get=-1  // abstract
  mode3  get=-1  // abstract
  yoke  get=-1  // abstract
  lever1  get=-1  // abstract
  lever2  get=-1  // abstract
  lever3  get=-1  // abstract
  lever4  get=-1  // abstract
  lever5  get=-1  // abstract
  leftGripHat  get=-1  // abstract
  rightGripHat  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Rewired.IFlightPedalsTemplate
TYPE:  interface
TOKEN: 0x2000006
IMPLEMENTS: Rewired.IControllerTemplate
FIELDS:
PROPERTIES:
  leftPedal  get=-1  // abstract
  rightPedal  get=-1  // abstract
  slide  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Rewired.ISixDofControllerTemplate
TYPE:  interface
TOKEN: 0x2000007
IMPLEMENTS: Rewired.IControllerTemplate
FIELDS:
PROPERTIES:
  extraAxis1  get=-1  // abstract
  extraAxis2  get=-1  // abstract
  extraAxis3  get=-1  // abstract
  extraAxis4  get=-1  // abstract
  button1  get=-1  // abstract
  button2  get=-1  // abstract
  button3  get=-1  // abstract
  button4  get=-1  // abstract
  button5  get=-1  // abstract
  button6  get=-1  // abstract
  button7  get=-1  // abstract
  button8  get=-1  // abstract
  button9  get=-1  // abstract
  button10  get=-1  // abstract
  button11  get=-1  // abstract
  button12  get=-1  // abstract
  button13  get=-1  // abstract
  button14  get=-1  // abstract
  button15  get=-1  // abstract
  button16  get=-1  // abstract
  button17  get=-1  // abstract
  button18  get=-1  // abstract
  button19  get=-1  // abstract
  button20  get=-1  // abstract
  button21  get=-1  // abstract
  button22  get=-1  // abstract
  button23  get=-1  // abstract
  button24  get=-1  // abstract
  button25  get=-1  // abstract
  button26  get=-1  // abstract
  button27  get=-1  // abstract
  button28  get=-1  // abstract
  button29  get=-1  // abstract
  button30  get=-1  // abstract
  button31  get=-1  // abstract
  button32  get=-1  // abstract
  hat1  get=-1  // abstract
  hat2  get=-1  // abstract
  throttle1  get=-1  // abstract
  throttle2  get=-1  // abstract
  stick  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Rewired.GamepadTemplate
TYPE:  sealed class
TOKEN: 0x2000008
SIZE:  0x78
EXTENDS: Rewired.ControllerTemplate
IMPLEMENTS: Rewired.IGamepadTemplate Rewired.IControllerTemplate
FIELDS:
  public    static readonly System.Guid                     typeGuid  // static @ 0x0
  public    static  System.Int32                    elementId_leftStickX  // const
  public    static  System.Int32                    elementId_leftStickY  // const
  public    static  System.Int32                    elementId_rightStickX  // const
  public    static  System.Int32                    elementId_rightStickY  // const
  public    static  System.Int32                    elementId_actionBottomRow1  // const
  public    static  System.Int32                    elementId_a  // const
  public    static  System.Int32                    elementId_actionBottomRow2  // const
  public    static  System.Int32                    elementId_b  // const
  public    static  System.Int32                    elementId_actionBottomRow3  // const
  public    static  System.Int32                    elementId_c  // const
  public    static  System.Int32                    elementId_actionTopRow1  // const
  public    static  System.Int32                    elementId_x  // const
  public    static  System.Int32                    elementId_actionTopRow2  // const
  public    static  System.Int32                    elementId_y  // const
  public    static  System.Int32                    elementId_actionTopRow3  // const
  public    static  System.Int32                    elementId_z  // const
  public    static  System.Int32                    elementId_leftShoulder1  // const
  public    static  System.Int32                    elementId_leftBumper  // const
  public    static  System.Int32                    elementId_leftShoulder2  // const
  public    static  System.Int32                    elementId_leftTrigger  // const
  public    static  System.Int32                    elementId_rightShoulder1  // const
  public    static  System.Int32                    elementId_rightBumper  // const
  public    static  System.Int32                    elementId_rightShoulder2  // const
  public    static  System.Int32                    elementId_rightTrigger  // const
  public    static  System.Int32                    elementId_center1  // const
  public    static  System.Int32                    elementId_back  // const
  public    static  System.Int32                    elementId_center2  // const
  public    static  System.Int32                    elementId_start  // const
  public    static  System.Int32                    elementId_center3  // const
  public    static  System.Int32                    elementId_guide  // const
  public    static  System.Int32                    elementId_leftStickButton  // const
  public    static  System.Int32                    elementId_rightStickButton  // const
  public    static  System.Int32                    elementId_dPadUp  // const
  public    static  System.Int32                    elementId_dPadRight  // const
  public    static  System.Int32                    elementId_dPadDown  // const
  public    static  System.Int32                    elementId_dPadLeft  // const
  public    static  System.Int32                    elementId_leftStick  // const
  public    static  System.Int32                    elementId_rightStick  // const
  public    static  System.Int32                    elementId_dPad  // const
PROPERTIES:
  Rewired.IGamepadTemplate.actionBottomRow1  get=0x0A9C93BC
  Rewired.IGamepadTemplate.a  get=0x0A9C937C
  Rewired.IGamepadTemplate.actionBottomRow2  get=0x0A9C93FC
  Rewired.IGamepadTemplate.b  get=0x0A9C953C
  Rewired.IGamepadTemplate.actionBottomRow3  get=0x0A9C943C
  Rewired.IGamepadTemplate.c  get=0x0A9C95BC
  Rewired.IGamepadTemplate.actionTopRow1  get=0x0A9C947C
  Rewired.IGamepadTemplate.x  get=0x0A9C99FC
  Rewired.IGamepadTemplate.actionTopRow2  get=0x0A9C94BC
  Rewired.IGamepadTemplate.y  get=0x0A9C9A3C
  Rewired.IGamepadTemplate.actionTopRow3  get=0x0A9C94FC
  Rewired.IGamepadTemplate.z  get=0x0A9C9A7C
  Rewired.IGamepadTemplate.leftShoulder1  get=0x0A9C977C
  Rewired.IGamepadTemplate.leftBumper  get=0x0A9C973C
  Rewired.IGamepadTemplate.leftShoulder2  get=0x0A9C97BC
  Rewired.IGamepadTemplate.leftTrigger  get=0x0A9C983C
  Rewired.IGamepadTemplate.rightShoulder1  get=0x0A9C98BC
  Rewired.IGamepadTemplate.rightBumper  get=0x0A9C987C
  Rewired.IGamepadTemplate.rightShoulder2  get=0x0A9C98FC
  Rewired.IGamepadTemplate.rightTrigger  get=0x0A9C997C
  Rewired.IGamepadTemplate.center1  get=0x0A9C95FC
  Rewired.IGamepadTemplate.back  get=0x0A9C957C
  Rewired.IGamepadTemplate.center2  get=0x0A9C963C
  Rewired.IGamepadTemplate.start  get=0x0A9C99BC
  Rewired.IGamepadTemplate.center3  get=0x0A9C967C
  Rewired.IGamepadTemplate.guide  get=0x0A9C96FC
  Rewired.IGamepadTemplate.leftStick  get=0x0A9C97FC
  Rewired.IGamepadTemplate.rightStick  get=0x0A9C993C
  Rewired.IGamepadTemplate.dPad  get=0x0A9C96BC
METHODS:
  RVA=0x0A9C86D0  token=0x600011A  System.Void .ctor(System.Object payload)
  RVA=0x0A9C9ABC  token=0x600011B  System.Void .cctor()
END_CLASS

CLASS: Rewired.RacingWheelTemplate
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x78
EXTENDS: Rewired.ControllerTemplate
IMPLEMENTS: Rewired.IRacingWheelTemplate Rewired.IControllerTemplate
FIELDS:
  public    static readonly System.Guid                     typeGuid  // static @ 0x0
  public    static  System.Int32                    elementId_wheel  // const
  public    static  System.Int32                    elementId_accelerator  // const
  public    static  System.Int32                    elementId_brake  // const
  public    static  System.Int32                    elementId_clutch  // const
  public    static  System.Int32                    elementId_shiftDown  // const
  public    static  System.Int32                    elementId_shiftUp  // const
  public    static  System.Int32                    elementId_wheelButton1  // const
  public    static  System.Int32                    elementId_wheelButton2  // const
  public    static  System.Int32                    elementId_wheelButton3  // const
  public    static  System.Int32                    elementId_wheelButton4  // const
  public    static  System.Int32                    elementId_wheelButton5  // const
  public    static  System.Int32                    elementId_wheelButton6  // const
  public    static  System.Int32                    elementId_wheelButton7  // const
  public    static  System.Int32                    elementId_wheelButton8  // const
  public    static  System.Int32                    elementId_wheelButton9  // const
  public    static  System.Int32                    elementId_wheelButton10  // const
  public    static  System.Int32                    elementId_consoleButton1  // const
  public    static  System.Int32                    elementId_consoleButton2  // const
  public    static  System.Int32                    elementId_consoleButton3  // const
  public    static  System.Int32                    elementId_consoleButton4  // const
  public    static  System.Int32                    elementId_consoleButton5  // const
  public    static  System.Int32                    elementId_consoleButton6  // const
  public    static  System.Int32                    elementId_consoleButton7  // const
  public    static  System.Int32                    elementId_consoleButton8  // const
  public    static  System.Int32                    elementId_consoleButton9  // const
  public    static  System.Int32                    elementId_consoleButton10  // const
  public    static  System.Int32                    elementId_shifter1  // const
  public    static  System.Int32                    elementId_shifter2  // const
  public    static  System.Int32                    elementId_shifter3  // const
  public    static  System.Int32                    elementId_shifter4  // const
  public    static  System.Int32                    elementId_shifter5  // const
  public    static  System.Int32                    elementId_shifter6  // const
  public    static  System.Int32                    elementId_shifter7  // const
  public    static  System.Int32                    elementId_shifter8  // const
  public    static  System.Int32                    elementId_shifter9  // const
  public    static  System.Int32                    elementId_shifter10  // const
  public    static  System.Int32                    elementId_reverseGear  // const
  public    static  System.Int32                    elementId_select  // const
  public    static  System.Int32                    elementId_start  // const
  public    static  System.Int32                    elementId_systemButton  // const
  public    static  System.Int32                    elementId_horn  // const
  public    static  System.Int32                    elementId_dPadUp  // const
  public    static  System.Int32                    elementId_dPadRight  // const
  public    static  System.Int32                    elementId_dPadDown  // const
  public    static  System.Int32                    elementId_dPadLeft  // const
  public    static  System.Int32                    elementId_dPad  // const
PROPERTIES:
  Rewired.IRacingWheelTemplate.wheel  get=0x0A9CC8C0
  Rewired.IRacingWheelTemplate.accelerator  get=0x0A9CBE80
  Rewired.IRacingWheelTemplate.brake  get=0x0A9CBEC0
  Rewired.IRacingWheelTemplate.clutch  get=0x0A9CBF00
  Rewired.IRacingWheelTemplate.shiftDown  get=0x0A9CC2C0
  Rewired.IRacingWheelTemplate.shiftUp  get=0x0A9CC300
  Rewired.IRacingWheelTemplate.wheelButton1  get=0x0A9CC680
  Rewired.IRacingWheelTemplate.wheelButton2  get=0x0A9CC6C0
  Rewired.IRacingWheelTemplate.wheelButton3  get=0x0A9CC700
  Rewired.IRacingWheelTemplate.wheelButton4  get=0x0A9CC740
  Rewired.IRacingWheelTemplate.wheelButton5  get=0x0A9CC780
  Rewired.IRacingWheelTemplate.wheelButton6  get=0x0A9CC7C0
  Rewired.IRacingWheelTemplate.wheelButton7  get=0x0A9CC800
  Rewired.IRacingWheelTemplate.wheelButton8  get=0x0A9CC840
  Rewired.IRacingWheelTemplate.wheelButton9  get=0x0A9CC880
  Rewired.IRacingWheelTemplate.wheelButton10  get=0x0A9CC640
  Rewired.IRacingWheelTemplate.consoleButton1  get=0x0A9CBF80
  Rewired.IRacingWheelTemplate.consoleButton2  get=0x0A9CBFC0
  Rewired.IRacingWheelTemplate.consoleButton3  get=0x0A9CC000
  Rewired.IRacingWheelTemplate.consoleButton4  get=0x0A9CC040
  Rewired.IRacingWheelTemplate.consoleButton5  get=0x0A9CC080
  Rewired.IRacingWheelTemplate.consoleButton6  get=0x0A9CC0C0
  Rewired.IRacingWheelTemplate.consoleButton7  get=0x0A9CC100
  Rewired.IRacingWheelTemplate.consoleButton8  get=0x0A9CC140
  Rewired.IRacingWheelTemplate.consoleButton9  get=0x0A9CC180
  Rewired.IRacingWheelTemplate.consoleButton10  get=0x0A9CBF40
  Rewired.IRacingWheelTemplate.shifter1  get=0x0A9CC380
  Rewired.IRacingWheelTemplate.shifter2  get=0x0A9CC3C0
  Rewired.IRacingWheelTemplate.shifter3  get=0x0A9CC400
  Rewired.IRacingWheelTemplate.shifter4  get=0x0A9CC440
  Rewired.IRacingWheelTemplate.shifter5  get=0x0A9CC480
  Rewired.IRacingWheelTemplate.shifter6  get=0x0A9CC4C0
  Rewired.IRacingWheelTemplate.shifter7  get=0x0A9CC500
  Rewired.IRacingWheelTemplate.shifter8  get=0x0A9CC540
  Rewired.IRacingWheelTemplate.shifter9  get=0x0A9CC580
  Rewired.IRacingWheelTemplate.shifter10  get=0x0A9CC340
  Rewired.IRacingWheelTemplate.reverseGear  get=0x0A9CC240
  Rewired.IRacingWheelTemplate.select  get=0x0A9CC280
  Rewired.IRacingWheelTemplate.start  get=0x0A9CC5C0
  Rewired.IRacingWheelTemplate.systemButton  get=0x0A9CC600
  Rewired.IRacingWheelTemplate.horn  get=0x0A9CC200
  Rewired.IRacingWheelTemplate.dPad  get=0x0A9CC1C0
METHODS:
  RVA=0x0A9C86D0  token=0x6000146  System.Void .ctor(System.Object payload)
  RVA=0x0A9CC8FC  token=0x6000147  System.Void .cctor()
END_CLASS

CLASS: Rewired.HOTASTemplate
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0x78
EXTENDS: Rewired.ControllerTemplate
IMPLEMENTS: Rewired.IHOTASTemplate Rewired.IControllerTemplate
FIELDS:
  public    static readonly System.Guid                     typeGuid  // static @ 0x0
  public    static  System.Int32                    elementId_stickX  // const
  public    static  System.Int32                    elementId_stickY  // const
  public    static  System.Int32                    elementId_stickRotate  // const
  public    static  System.Int32                    elementId_stickMiniStick1X  // const
  public    static  System.Int32                    elementId_stickMiniStick1Y  // const
  public    static  System.Int32                    elementId_stickMiniStick1Press  // const
  public    static  System.Int32                    elementId_stickMiniStick2X  // const
  public    static  System.Int32                    elementId_stickMiniStick2Y  // const
  public    static  System.Int32                    elementId_stickMiniStick2Press  // const
  public    static  System.Int32                    elementId_stickTrigger  // const
  public    static  System.Int32                    elementId_stickTriggerStage2  // const
  public    static  System.Int32                    elementId_stickPinkyButton  // const
  public    static  System.Int32                    elementId_stickPinkyTrigger  // const
  public    static  System.Int32                    elementId_stickButton1  // const
  public    static  System.Int32                    elementId_stickButton2  // const
  public    static  System.Int32                    elementId_stickButton3  // const
  public    static  System.Int32                    elementId_stickButton4  // const
  public    static  System.Int32                    elementId_stickButton5  // const
  public    static  System.Int32                    elementId_stickButton6  // const
  public    static  System.Int32                    elementId_stickButton7  // const
  public    static  System.Int32                    elementId_stickButton8  // const
  public    static  System.Int32                    elementId_stickButton9  // const
  public    static  System.Int32                    elementId_stickButton10  // const
  public    static  System.Int32                    elementId_stickBaseButton1  // const
  public    static  System.Int32                    elementId_stickBaseButton2  // const
  public    static  System.Int32                    elementId_stickBaseButton3  // const
  public    static  System.Int32                    elementId_stickBaseButton4  // const
  public    static  System.Int32                    elementId_stickBaseButton5  // const
  public    static  System.Int32                    elementId_stickBaseButton6  // const
  public    static  System.Int32                    elementId_stickBaseButton7  // const
  public    static  System.Int32                    elementId_stickBaseButton8  // const
  public    static  System.Int32                    elementId_stickBaseButton9  // const
  public    static  System.Int32                    elementId_stickBaseButton10  // const
  public    static  System.Int32                    elementId_stickBaseButton11  // const
  public    static  System.Int32                    elementId_stickBaseButton12  // const
  public    static  System.Int32                    elementId_stickHat1Up  // const
  public    static  System.Int32                    elementId_stickHat1UpRight  // const
  public    static  System.Int32                    elementId_stickHat1Right  // const
  public    static  System.Int32                    elementId_stickHat1DownRight  // const
  public    static  System.Int32                    elementId_stickHat1Down  // const
  public    static  System.Int32                    elementId_stickHat1DownLeft  // const
  public    static  System.Int32                    elementId_stickHat1Left  // const
  public    static  System.Int32                    elementId_stickHat1Up_Left  // const
  public    static  System.Int32                    elementId_stickHat2Up  // const
  public    static  System.Int32                    elementId_stickHat2Up_right  // const
  public    static  System.Int32                    elementId_stickHat2Right  // const
  public    static  System.Int32                    elementId_stickHat2Down_Right  // const
  public    static  System.Int32                    elementId_stickHat2Down  // const
  public    static  System.Int32                    elementId_stickHat2Down_Left  // const
  public    static  System.Int32                    elementId_stickHat2Left  // const
  public    static  System.Int32                    elementId_stickHat2Up_Left  // const
  public    static  System.Int32                    elementId_stickHat3Up  // const
  public    static  System.Int32                    elementId_stickHat3Up_Right  // const
  public    static  System.Int32                    elementId_stickHat3Right  // const
  public    static  System.Int32                    elementId_stickHat3Down_Right  // const
  public    static  System.Int32                    elementId_stickHat3Down  // const
  public    static  System.Int32                    elementId_stickHat3Down_Left  // const
  public    static  System.Int32                    elementId_stickHat3Left  // const
  public    static  System.Int32                    elementId_stickHat3Up_Left  // const
  public    static  System.Int32                    elementId_stickHat4Up  // const
  public    static  System.Int32                    elementId_stickHat4Up_Right  // const
  public    static  System.Int32                    elementId_stickHat4Right  // const
  public    static  System.Int32                    elementId_stickHat4Down_Right  // const
  public    static  System.Int32                    elementId_stickHat4Down  // const
  public    static  System.Int32                    elementId_stickHat4Down_Left  // const
  public    static  System.Int32                    elementId_stickHat4Left  // const
  public    static  System.Int32                    elementId_stickHat4Up_Left  // const
  public    static  System.Int32                    elementId_mode1  // const
  public    static  System.Int32                    elementId_mode2  // const
  public    static  System.Int32                    elementId_mode3  // const
  public    static  System.Int32                    elementId_throttle1Axis  // const
  public    static  System.Int32                    elementId_throttle2Axis  // const
  public    static  System.Int32                    elementId_throttle1MinDetent  // const
  public    static  System.Int32                    elementId_throttle2MinDetent  // const
  public    static  System.Int32                    elementId_throttleButton1  // const
  public    static  System.Int32                    elementId_throttleButton2  // const
  public    static  System.Int32                    elementId_throttleButton3  // const
  public    static  System.Int32                    elementId_throttleButton4  // const
  public    static  System.Int32                    elementId_throttleButton5  // const
  public    static  System.Int32                    elementId_throttleButton6  // const
  public    static  System.Int32                    elementId_throttleButton7  // const
  public    static  System.Int32                    elementId_throttleButton8  // const
  public    static  System.Int32                    elementId_throttleButton9  // const
  public    static  System.Int32                    elementId_throttleButton10  // const
  public    static  System.Int32                    elementId_throttleBaseButton1  // const
  public    static  System.Int32                    elementId_throttleBaseButton2  // const
  public    static  System.Int32                    elementId_throttleBaseButton3  // const
  public    static  System.Int32                    elementId_throttleBaseButton4  // const
  public    static  System.Int32                    elementId_throttleBaseButton5  // const
  public    static  System.Int32                    elementId_throttleBaseButton6  // const
  public    static  System.Int32                    elementId_throttleBaseButton7  // const
  public    static  System.Int32                    elementId_throttleBaseButton8  // const
  public    static  System.Int32                    elementId_throttleBaseButton9  // const
  public    static  System.Int32                    elementId_throttleBaseButton10  // const
  public    static  System.Int32                    elementId_throttleBaseButton11  // const
  public    static  System.Int32                    elementId_throttleBaseButton12  // const
  public    static  System.Int32                    elementId_throttleBaseButton13  // const
  public    static  System.Int32                    elementId_throttleBaseButton14  // const
  public    static  System.Int32                    elementId_throttleBaseButton15  // const
  public    static  System.Int32                    elementId_throttleSlider1  // const
  public    static  System.Int32                    elementId_throttleSlider2  // const
  public    static  System.Int32                    elementId_throttleSlider3  // const
  public    static  System.Int32                    elementId_throttleSlider4  // const
  public    static  System.Int32                    elementId_throttleDial1  // const
  public    static  System.Int32                    elementId_throttleDial2  // const
  public    static  System.Int32                    elementId_throttleDial3  // const
  public    static  System.Int32                    elementId_throttleDial4  // const
  public    static  System.Int32                    elementId_throttleMiniStickX  // const
  public    static  System.Int32                    elementId_throttleMiniStickY  // const
  public    static  System.Int32                    elementId_throttleMiniStickPress  // const
  public    static  System.Int32                    elementId_throttleWheel1Forward  // const
  public    static  System.Int32                    elementId_throttleWheel1Back  // const
  public    static  System.Int32                    elementId_throttleWheel1Press  // const
  public    static  System.Int32                    elementId_throttleWheel2Forward  // const
  public    static  System.Int32                    elementId_throttleWheel2Back  // const
  public    static  System.Int32                    elementId_throttleWheel2Press  // const
  public    static  System.Int32                    elementId_throttleWheel3Forward  // const
  public    static  System.Int32                    elementId_throttleWheel3Back  // const
  public    static  System.Int32                    elementId_throttleWheel3Press  // const
  public    static  System.Int32                    elementId_throttleHat1Up  // const
  public    static  System.Int32                    elementId_throttleHat1Up_Right  // const
  public    static  System.Int32                    elementId_throttleHat1Right  // const
  public    static  System.Int32                    elementId_throttleHat1Down_Right  // const
  public    static  System.Int32                    elementId_throttleHat1Down  // const
  public    static  System.Int32                    elementId_throttleHat1Down_Left  // const
  public    static  System.Int32                    elementId_throttleHat1Left  // const
  public    static  System.Int32                    elementId_throttleHat1Up_Left  // const
  public    static  System.Int32                    elementId_throttleHat2Up  // const
  public    static  System.Int32                    elementId_throttleHat2Up_Right  // const
  public    static  System.Int32                    elementId_throttleHat2Right  // const
  public    static  System.Int32                    elementId_throttleHat2Down_Right  // const
  public    static  System.Int32                    elementId_throttleHat2Down  // const
  public    static  System.Int32                    elementId_throttleHat2Down_Left  // const
  public    static  System.Int32                    elementId_throttleHat2Left  // const
  public    static  System.Int32                    elementId_throttleHat2Up_Left  // const
  public    static  System.Int32                    elementId_throttleHat3Up  // const
  public    static  System.Int32                    elementId_throttleHat3Up_Right  // const
  public    static  System.Int32                    elementId_throttleHat3Right  // const
  public    static  System.Int32                    elementId_throttleHat3Down_Right  // const
  public    static  System.Int32                    elementId_throttleHat3Down  // const
  public    static  System.Int32                    elementId_throttleHat3Down_Left  // const
  public    static  System.Int32                    elementId_throttleHat3Left  // const
  public    static  System.Int32                    elementId_throttleHat3Up_Left  // const
  public    static  System.Int32                    elementId_throttleHat4Up  // const
  public    static  System.Int32                    elementId_throttleHat4Up_Right  // const
  public    static  System.Int32                    elementId_throttleHat4Right  // const
  public    static  System.Int32                    elementId_throttleHat4Down_Right  // const
  public    static  System.Int32                    elementId_throttleHat4Down  // const
  public    static  System.Int32                    elementId_throttleHat4Down_Left  // const
  public    static  System.Int32                    elementId_throttleHat4Left  // const
  public    static  System.Int32                    elementId_throttleHat4Up_Left  // const
  public    static  System.Int32                    elementId_leftPedal  // const
  public    static  System.Int32                    elementId_rightPedal  // const
  public    static  System.Int32                    elementId_slidePedals  // const
  public    static  System.Int32                    elementId_stick  // const
  public    static  System.Int32                    elementId_stickMiniStick1  // const
  public    static  System.Int32                    elementId_stickMiniStick2  // const
  public    static  System.Int32                    elementId_stickHat1  // const
  public    static  System.Int32                    elementId_stickHat2  // const
  public    static  System.Int32                    elementId_stickHat3  // const
  public    static  System.Int32                    elementId_stickHat4  // const
  public    static  System.Int32                    elementId_throttle1  // const
  public    static  System.Int32                    elementId_throttle2  // const
  public    static  System.Int32                    elementId_throttleMiniStick  // const
  public    static  System.Int32                    elementId_throttleHat1  // const
  public    static  System.Int32                    elementId_throttleHat2  // const
  public    static  System.Int32                    elementId_throttleHat3  // const
  public    static  System.Int32                    elementId_throttleHat4  // const
PROPERTIES:
  Rewired.IHOTASTemplate.stickTrigger  get=0x0A9CA460
  Rewired.IHOTASTemplate.stickTriggerStage2  get=0x0A9CA420
  Rewired.IHOTASTemplate.stickPinkyButton  get=0x0A9CA3A0
  Rewired.IHOTASTemplate.stickPinkyTrigger  get=0x0A9CA3E0
  Rewired.IHOTASTemplate.stickButton1  get=0x0A9C9FE0
  Rewired.IHOTASTemplate.stickButton2  get=0x0A9CA020
  Rewired.IHOTASTemplate.stickButton3  get=0x0A9CA060
  Rewired.IHOTASTemplate.stickButton4  get=0x0A9CA0A0
  Rewired.IHOTASTemplate.stickButton5  get=0x0A9CA0E0
  Rewired.IHOTASTemplate.stickButton6  get=0x0A9CA120
  Rewired.IHOTASTemplate.stickButton7  get=0x0A9CA160
  Rewired.IHOTASTemplate.stickButton8  get=0x0A9CA1A0
  Rewired.IHOTASTemplate.stickButton9  get=0x0A9CA1E0
  Rewired.IHOTASTemplate.stickButton10  get=0x0A9C9FA0
  Rewired.IHOTASTemplate.stickBaseButton1  get=0x0A9C9D60
  Rewired.IHOTASTemplate.stickBaseButton2  get=0x0A9C9DA0
  Rewired.IHOTASTemplate.stickBaseButton3  get=0x0A9C9DE0
  Rewired.IHOTASTemplate.stickBaseButton4  get=0x0A9C9E20
  Rewired.IHOTASTemplate.stickBaseButton5  get=0x0A9C9E60
  Rewired.IHOTASTemplate.stickBaseButton6  get=0x0A9C9EA0
  Rewired.IHOTASTemplate.stickBaseButton7  get=0x0A9C9EE0
  Rewired.IHOTASTemplate.stickBaseButton8  get=0x0A9C9F20
  Rewired.IHOTASTemplate.stickBaseButton9  get=0x0A9C9F60
  Rewired.IHOTASTemplate.stickBaseButton10  get=0x0A9C9CA0
  Rewired.IHOTASTemplate.stickBaseButton11  get=0x0A9C9CE0
  Rewired.IHOTASTemplate.stickBaseButton12  get=0x0A9C9D20
  Rewired.IHOTASTemplate.mode1  get=0x0A9C9B60
  Rewired.IHOTASTemplate.mode2  get=0x0A9C9BA0
  Rewired.IHOTASTemplate.mode3  get=0x0A9C9BE0
  Rewired.IHOTASTemplate.throttleButton1  get=0x0A9CA960
  Rewired.IHOTASTemplate.throttleButton2  get=0x0A9CA9A0
  Rewired.IHOTASTemplate.throttleButton3  get=0x0A9CA9E0
  Rewired.IHOTASTemplate.throttleButton4  get=0x0A9CAA20
  Rewired.IHOTASTemplate.throttleButton5  get=0x0A9CAA60
  Rewired.IHOTASTemplate.throttleButton6  get=0x0A9CAAA0
  Rewired.IHOTASTemplate.throttleButton7  get=0x0A9CAAE0
  Rewired.IHOTASTemplate.throttleButton8  get=0x0A9CAB20
  Rewired.IHOTASTemplate.throttleButton9  get=0x0A9CAB60
  Rewired.IHOTASTemplate.throttleButton10  get=0x0A9CA920
  Rewired.IHOTASTemplate.throttleBaseButton1  get=0x0A9CA6E0
  Rewired.IHOTASTemplate.throttleBaseButton2  get=0x0A9CA720
  Rewired.IHOTASTemplate.throttleBaseButton3  get=0x0A9CA760
  Rewired.IHOTASTemplate.throttleBaseButton4  get=0x0A9CA7A0
  Rewired.IHOTASTemplate.throttleBaseButton5  get=0x0A9CA7E0
  Rewired.IHOTASTemplate.throttleBaseButton6  get=0x0A9CA820
  Rewired.IHOTASTemplate.throttleBaseButton7  get=0x0A9CA860
  Rewired.IHOTASTemplate.throttleBaseButton8  get=0x0A9CA8A0
  Rewired.IHOTASTemplate.throttleBaseButton9  get=0x0A9CA8E0
  Rewired.IHOTASTemplate.throttleBaseButton10  get=0x0A9CA560
  Rewired.IHOTASTemplate.throttleBaseButton11  get=0x0A9CA5A0
  Rewired.IHOTASTemplate.throttleBaseButton12  get=0x0A9CA5E0
  Rewired.IHOTASTemplate.throttleBaseButton13  get=0x0A9CA620
  Rewired.IHOTASTemplate.throttleBaseButton14  get=0x0A9CA660
  Rewired.IHOTASTemplate.throttleBaseButton15  get=0x0A9CA6A0
  Rewired.IHOTASTemplate.throttleSlider1  get=0x0A9CADE0
  Rewired.IHOTASTemplate.throttleSlider2  get=0x0A9CAE20
  Rewired.IHOTASTemplate.throttleSlider3  get=0x0A9CAE60
  Rewired.IHOTASTemplate.throttleSlider4  get=0x0A9CAEA0
  Rewired.IHOTASTemplate.throttleDial1  get=0x0A9CABA0
  Rewired.IHOTASTemplate.throttleDial2  get=0x0A9CABE0
  Rewired.IHOTASTemplate.throttleDial3  get=0x0A9CAC20
  Rewired.IHOTASTemplate.throttleDial4  get=0x0A9CAC60
  Rewired.IHOTASTemplate.throttleWheel1Forward  get=0x0A9CAF20
  Rewired.IHOTASTemplate.throttleWheel1Back  get=0x0A9CAEE0
  Rewired.IHOTASTemplate.throttleWheel1Press  get=0x0A9CAF60
  Rewired.IHOTASTemplate.throttleWheel2Forward  get=0x0A9CAFE0
  Rewired.IHOTASTemplate.throttleWheel2Back  get=0x0A9CAFA0
  Rewired.IHOTASTemplate.throttleWheel2Press  get=0x0A9CB020
  Rewired.IHOTASTemplate.throttleWheel3Forward  get=0x0A9CB0A0
  Rewired.IHOTASTemplate.throttleWheel3Back  get=0x0A9CB060
  Rewired.IHOTASTemplate.throttleWheel3Press  get=0x0A9CB0E0
  Rewired.IHOTASTemplate.leftPedal  get=0x0A9C9B20
  Rewired.IHOTASTemplate.rightPedal  get=0x0A9C9C20
  Rewired.IHOTASTemplate.slidePedals  get=0x0A9C9C60
  Rewired.IHOTASTemplate.stick  get=0x0A9CA4A0
  Rewired.IHOTASTemplate.stickMiniStick1  get=0x0A9CA320
  Rewired.IHOTASTemplate.stickMiniStick2  get=0x0A9CA360
  Rewired.IHOTASTemplate.stickHat1  get=0x0A9CA220
  Rewired.IHOTASTemplate.stickHat2  get=0x0A9CA260
  Rewired.IHOTASTemplate.stickHat3  get=0x0A9CA2A0
  Rewired.IHOTASTemplate.stickHat4  get=0x0A9CA2E0
  Rewired.IHOTASTemplate.throttle1  get=0x0A9CA4E0
  Rewired.IHOTASTemplate.throttle2  get=0x0A9CA520
  Rewired.IHOTASTemplate.throttleMiniStick  get=0x0A9CADA0
  Rewired.IHOTASTemplate.throttleHat1  get=0x0A9CACA0
  Rewired.IHOTASTemplate.throttleHat2  get=0x0A9CACE0
  Rewired.IHOTASTemplate.throttleHat3  get=0x0A9CAD20
  Rewired.IHOTASTemplate.throttleHat4  get=0x0A9CAD60
METHODS:
  RVA=0x0A9C86D0  token=0x60001A0  System.Void .ctor(System.Object payload)
  RVA=0x0A9CB120  token=0x60001A1  System.Void .cctor()
END_CLASS

CLASS: Rewired.FlightYokeTemplate
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x78
EXTENDS: Rewired.ControllerTemplate
IMPLEMENTS: Rewired.IFlightYokeTemplate Rewired.IControllerTemplate
FIELDS:
  public    static readonly System.Guid                     typeGuid  // static @ 0x0
  public    static  System.Int32                    elementId_rotateYoke  // const
  public    static  System.Int32                    elementId_yokeZ  // const
  public    static  System.Int32                    elementId_leftPaddle  // const
  public    static  System.Int32                    elementId_rightPaddle  // const
  public    static  System.Int32                    elementId_lever1Axis  // const
  public    static  System.Int32                    elementId_lever1MinDetent  // const
  public    static  System.Int32                    elementId_lever2Axis  // const
  public    static  System.Int32                    elementId_lever2MinDetent  // const
  public    static  System.Int32                    elementId_lever3Axis  // const
  public    static  System.Int32                    elementId_lever3MinDetent  // const
  public    static  System.Int32                    elementId_lever4Axis  // const
  public    static  System.Int32                    elementId_lever4MinDetent  // const
  public    static  System.Int32                    elementId_lever5Axis  // const
  public    static  System.Int32                    elementId_lever5MinDetent  // const
  public    static  System.Int32                    elementId_leftGripButton1  // const
  public    static  System.Int32                    elementId_leftGripButton2  // const
  public    static  System.Int32                    elementId_leftGripButton3  // const
  public    static  System.Int32                    elementId_leftGripButton4  // const
  public    static  System.Int32                    elementId_leftGripButton5  // const
  public    static  System.Int32                    elementId_leftGripButton6  // const
  public    static  System.Int32                    elementId_rightGripButton1  // const
  public    static  System.Int32                    elementId_rightGripButton2  // const
  public    static  System.Int32                    elementId_rightGripButton3  // const
  public    static  System.Int32                    elementId_rightGripButton4  // const
  public    static  System.Int32                    elementId_rightGripButton5  // const
  public    static  System.Int32                    elementId_rightGripButton6  // const
  public    static  System.Int32                    elementId_centerButton1  // const
  public    static  System.Int32                    elementId_centerButton2  // const
  public    static  System.Int32                    elementId_centerButton3  // const
  public    static  System.Int32                    elementId_centerButton4  // const
  public    static  System.Int32                    elementId_centerButton5  // const
  public    static  System.Int32                    elementId_centerButton6  // const
  public    static  System.Int32                    elementId_centerButton7  // const
  public    static  System.Int32                    elementId_centerButton8  // const
  public    static  System.Int32                    elementId_wheel1Up  // const
  public    static  System.Int32                    elementId_wheel1Down  // const
  public    static  System.Int32                    elementId_wheel1Press  // const
  public    static  System.Int32                    elementId_wheel2Up  // const
  public    static  System.Int32                    elementId_wheel2Down  // const
  public    static  System.Int32                    elementId_wheel2Press  // const
  public    static  System.Int32                    elementId_leftGripHatUp  // const
  public    static  System.Int32                    elementId_leftGripHatUpRight  // const
  public    static  System.Int32                    elementId_leftGripHatRight  // const
  public    static  System.Int32                    elementId_leftGripHatDownRight  // const
  public    static  System.Int32                    elementId_leftGripHatDown  // const
  public    static  System.Int32                    elementId_leftGripHatDownLeft  // const
  public    static  System.Int32                    elementId_leftGripHatLeft  // const
  public    static  System.Int32                    elementId_leftGripHatUpLeft  // const
  public    static  System.Int32                    elementId_rightGripHatUp  // const
  public    static  System.Int32                    elementId_rightGripHatUpRight  // const
  public    static  System.Int32                    elementId_rightGripHatRight  // const
  public    static  System.Int32                    elementId_rightGripHatDownRight  // const
  public    static  System.Int32                    elementId_rightGripHatDown  // const
  public    static  System.Int32                    elementId_rightGripHatDownLeft  // const
  public    static  System.Int32                    elementId_rightGripHatLeft  // const
  public    static  System.Int32                    elementId_rightGripHatUpLeft  // const
  public    static  System.Int32                    elementId_consoleButton1  // const
  public    static  System.Int32                    elementId_consoleButton2  // const
  public    static  System.Int32                    elementId_consoleButton3  // const
  public    static  System.Int32                    elementId_consoleButton4  // const
  public    static  System.Int32                    elementId_consoleButton5  // const
  public    static  System.Int32                    elementId_consoleButton6  // const
  public    static  System.Int32                    elementId_consoleButton7  // const
  public    static  System.Int32                    elementId_consoleButton8  // const
  public    static  System.Int32                    elementId_consoleButton9  // const
  public    static  System.Int32                    elementId_consoleButton10  // const
  public    static  System.Int32                    elementId_mode1  // const
  public    static  System.Int32                    elementId_mode2  // const
  public    static  System.Int32                    elementId_mode3  // const
  public    static  System.Int32                    elementId_yoke  // const
  public    static  System.Int32                    elementId_lever1  // const
  public    static  System.Int32                    elementId_lever2  // const
  public    static  System.Int32                    elementId_lever3  // const
  public    static  System.Int32                    elementId_lever4  // const
  public    static  System.Int32                    elementId_lever5  // const
  public    static  System.Int32                    elementId_leftGripHat  // const
  public    static  System.Int32                    elementId_rightGripHat  // const
PROPERTIES:
  Rewired.IFlightYokeTemplate.leftPaddle  get=0x0A9C8D18
  Rewired.IFlightYokeTemplate.rightPaddle  get=0x0A9C9118
  Rewired.IFlightYokeTemplate.leftGripButton1  get=0x0A9C8B58
  Rewired.IFlightYokeTemplate.leftGripButton2  get=0x0A9C8B98
  Rewired.IFlightYokeTemplate.leftGripButton3  get=0x0A9C8BD8
  Rewired.IFlightYokeTemplate.leftGripButton4  get=0x0A9C8C18
  Rewired.IFlightYokeTemplate.leftGripButton5  get=0x0A9C8C58
  Rewired.IFlightYokeTemplate.leftGripButton6  get=0x0A9C8C98
  Rewired.IFlightYokeTemplate.rightGripButton1  get=0x0A9C8F58
  Rewired.IFlightYokeTemplate.rightGripButton2  get=0x0A9C8F98
  Rewired.IFlightYokeTemplate.rightGripButton3  get=0x0A9C8FD8
  Rewired.IFlightYokeTemplate.rightGripButton4  get=0x0A9C9018
  Rewired.IFlightYokeTemplate.rightGripButton5  get=0x0A9C9058
  Rewired.IFlightYokeTemplate.rightGripButton6  get=0x0A9C9098
  Rewired.IFlightYokeTemplate.centerButton1  get=0x0A9C86D8
  Rewired.IFlightYokeTemplate.centerButton2  get=0x0A9C8718
  Rewired.IFlightYokeTemplate.centerButton3  get=0x0A9C8758
  Rewired.IFlightYokeTemplate.centerButton4  get=0x0A9C8798
  Rewired.IFlightYokeTemplate.centerButton5  get=0x0A9C87D8
  Rewired.IFlightYokeTemplate.centerButton6  get=0x0A9C8818
  Rewired.IFlightYokeTemplate.centerButton7  get=0x0A9C8858
  Rewired.IFlightYokeTemplate.centerButton8  get=0x0A9C8898
  Rewired.IFlightYokeTemplate.wheel1Up  get=0x0A9C91D8
  Rewired.IFlightYokeTemplate.wheel1Down  get=0x0A9C9158
  Rewired.IFlightYokeTemplate.wheel1Press  get=0x0A9C9198
  Rewired.IFlightYokeTemplate.wheel2Up  get=0x0A9C9298
  Rewired.IFlightYokeTemplate.wheel2Down  get=0x0A9C9218
  Rewired.IFlightYokeTemplate.wheel2Press  get=0x0A9C9258
  Rewired.IFlightYokeTemplate.consoleButton1  get=0x0A9C8918
  Rewired.IFlightYokeTemplate.consoleButton2  get=0x0A9C8958
  Rewired.IFlightYokeTemplate.consoleButton3  get=0x0A9C8998
  Rewired.IFlightYokeTemplate.consoleButton4  get=0x0A9C89D8
  Rewired.IFlightYokeTemplate.consoleButton5  get=0x0A9C8A18
  Rewired.IFlightYokeTemplate.consoleButton6  get=0x0A9C8A58
  Rewired.IFlightYokeTemplate.consoleButton7  get=0x0A9C8A98
  Rewired.IFlightYokeTemplate.consoleButton8  get=0x0A9C8AD8
  Rewired.IFlightYokeTemplate.consoleButton9  get=0x0A9C8B18
  Rewired.IFlightYokeTemplate.consoleButton10  get=0x0A9C88D8
  Rewired.IFlightYokeTemplate.mode1  get=0x0A9C8E98
  Rewired.IFlightYokeTemplate.mode2  get=0x0A9C8ED8
  Rewired.IFlightYokeTemplate.mode3  get=0x0A9C8F18
  Rewired.IFlightYokeTemplate.yoke  get=0x0A9C92D8
  Rewired.IFlightYokeTemplate.lever1  get=0x0A9C8D58
  Rewired.IFlightYokeTemplate.lever2  get=0x0A9C8D98
  Rewired.IFlightYokeTemplate.lever3  get=0x0A9C8DD8
  Rewired.IFlightYokeTemplate.lever4  get=0x0A9C8E18
  Rewired.IFlightYokeTemplate.lever5  get=0x0A9C8E58
  Rewired.IFlightYokeTemplate.leftGripHat  get=0x0A9C8CD8
  Rewired.IFlightYokeTemplate.rightGripHat  get=0x0A9C90D8
METHODS:
  RVA=0x0A9C86D0  token=0x60001D3  System.Void .ctor(System.Object payload)
  RVA=0x0A9C9318  token=0x60001D4  System.Void .cctor()
END_CLASS

CLASS: Rewired.FlightPedalsTemplate
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x78
EXTENDS: Rewired.ControllerTemplate
IMPLEMENTS: Rewired.IFlightPedalsTemplate Rewired.IControllerTemplate
FIELDS:
  public    static readonly System.Guid                     typeGuid  // static @ 0x0
  public    static  System.Int32                    elementId_leftPedal  // const
  public    static  System.Int32                    elementId_rightPedal  // const
  public    static  System.Int32                    elementId_slide  // const
PROPERTIES:
  Rewired.IFlightPedalsTemplate.leftPedal  get=0x0A9C85B0
  Rewired.IFlightPedalsTemplate.rightPedal  get=0x0A9C85EC
  Rewired.IFlightPedalsTemplate.slide  get=0x0A9C862C
METHODS:
  RVA=0x0A9C86D0  token=0x60001D8  System.Void .ctor(System.Object payload)
  RVA=0x0A9C866C  token=0x60001D9  System.Void .cctor()
END_CLASS

CLASS: Rewired.SixDofControllerTemplate
TYPE:  sealed class
TOKEN: 0x200000D
SIZE:  0x78
EXTENDS: Rewired.ControllerTemplate
IMPLEMENTS: Rewired.ISixDofControllerTemplate Rewired.IControllerTemplate
FIELDS:
  public    static readonly System.Guid                     typeGuid  // static @ 0x0
  public    static  System.Int32                    elementId_positionX  // const
  public    static  System.Int32                    elementId_positionY  // const
  public    static  System.Int32                    elementId_positionZ  // const
  public    static  System.Int32                    elementId_rotationX  // const
  public    static  System.Int32                    elementId_rotationY  // const
  public    static  System.Int32                    elementId_rotationZ  // const
  public    static  System.Int32                    elementId_throttle1Axis  // const
  public    static  System.Int32                    elementId_throttle1MinDetent  // const
  public    static  System.Int32                    elementId_throttle2Axis  // const
  public    static  System.Int32                    elementId_throttle2MinDetent  // const
  public    static  System.Int32                    elementId_extraAxis1  // const
  public    static  System.Int32                    elementId_extraAxis2  // const
  public    static  System.Int32                    elementId_extraAxis3  // const
  public    static  System.Int32                    elementId_extraAxis4  // const
  public    static  System.Int32                    elementId_button1  // const
  public    static  System.Int32                    elementId_button2  // const
  public    static  System.Int32                    elementId_button3  // const
  public    static  System.Int32                    elementId_button4  // const
  public    static  System.Int32                    elementId_button5  // const
  public    static  System.Int32                    elementId_button6  // const
  public    static  System.Int32                    elementId_button7  // const
  public    static  System.Int32                    elementId_button8  // const
  public    static  System.Int32                    elementId_button9  // const
  public    static  System.Int32                    elementId_button10  // const
  public    static  System.Int32                    elementId_button11  // const
  public    static  System.Int32                    elementId_button12  // const
  public    static  System.Int32                    elementId_button13  // const
  public    static  System.Int32                    elementId_button14  // const
  public    static  System.Int32                    elementId_button15  // const
  public    static  System.Int32                    elementId_button16  // const
  public    static  System.Int32                    elementId_button17  // const
  public    static  System.Int32                    elementId_button18  // const
  public    static  System.Int32                    elementId_button19  // const
  public    static  System.Int32                    elementId_button20  // const
  public    static  System.Int32                    elementId_button21  // const
  public    static  System.Int32                    elementId_button22  // const
  public    static  System.Int32                    elementId_button23  // const
  public    static  System.Int32                    elementId_button24  // const
  public    static  System.Int32                    elementId_button25  // const
  public    static  System.Int32                    elementId_button26  // const
  public    static  System.Int32                    elementId_button27  // const
  public    static  System.Int32                    elementId_button28  // const
  public    static  System.Int32                    elementId_button29  // const
  public    static  System.Int32                    elementId_button30  // const
  public    static  System.Int32                    elementId_button31  // const
  public    static  System.Int32                    elementId_button32  // const
  public    static  System.Int32                    elementId_hat1Up  // const
  public    static  System.Int32                    elementId_hat1UpRight  // const
  public    static  System.Int32                    elementId_hat1Right  // const
  public    static  System.Int32                    elementId_hat1DownRight  // const
  public    static  System.Int32                    elementId_hat1Down  // const
  public    static  System.Int32                    elementId_hat1DownLeft  // const
  public    static  System.Int32                    elementId_hat1Left  // const
  public    static  System.Int32                    elementId_hat1UpLeft  // const
  public    static  System.Int32                    elementId_hat2Up  // const
  public    static  System.Int32                    elementId_hat2UpRight  // const
  public    static  System.Int32                    elementId_hat2Right  // const
  public    static  System.Int32                    elementId_hat2DownRight  // const
  public    static  System.Int32                    elementId_hat2Down  // const
  public    static  System.Int32                    elementId_hat2DownLeft  // const
  public    static  System.Int32                    elementId_hat2Left  // const
  public    static  System.Int32                    elementId_hat2UpLeft  // const
  public    static  System.Int32                    elementId_hat1  // const
  public    static  System.Int32                    elementId_hat2  // const
  public    static  System.Int32                    elementId_throttle1  // const
  public    static  System.Int32                    elementId_throttle2  // const
  public    static  System.Int32                    elementId_stick  // const
PROPERTIES:
  Rewired.ISixDofControllerTemplate.extraAxis1  get=0x0A9D3998
  Rewired.ISixDofControllerTemplate.extraAxis2  get=0x0A9D39D8
  Rewired.ISixDofControllerTemplate.extraAxis3  get=0x0A9D3A18
  Rewired.ISixDofControllerTemplate.extraAxis4  get=0x0A9D3A58
  Rewired.ISixDofControllerTemplate.button1  get=0x0A9D3418
  Rewired.ISixDofControllerTemplate.button2  get=0x0A9D36D8
  Rewired.ISixDofControllerTemplate.button3  get=0x0A9D37D8
  Rewired.ISixDofControllerTemplate.button4  get=0x0A9D3818
  Rewired.ISixDofControllerTemplate.button5  get=0x0A9D3858
  Rewired.ISixDofControllerTemplate.button6  get=0x0A9D3898
  Rewired.ISixDofControllerTemplate.button7  get=0x0A9D38D8
  Rewired.ISixDofControllerTemplate.button8  get=0x0A9D3918
  Rewired.ISixDofControllerTemplate.button9  get=0x0A9D3958
  Rewired.ISixDofControllerTemplate.button10  get=0x0A9D3198
  Rewired.ISixDofControllerTemplate.button11  get=0x0A9D31D8
  Rewired.ISixDofControllerTemplate.button12  get=0x0A9D3218
  Rewired.ISixDofControllerTemplate.button13  get=0x0A9D3258
  Rewired.ISixDofControllerTemplate.button14  get=0x0A9D3298
  Rewired.ISixDofControllerTemplate.button15  get=0x0A9D32D8
  Rewired.ISixDofControllerTemplate.button16  get=0x0A9D3318
  Rewired.ISixDofControllerTemplate.button17  get=0x0A9D3358
  Rewired.ISixDofControllerTemplate.button18  get=0x0A9D3398
  Rewired.ISixDofControllerTemplate.button19  get=0x0A9D33D8
  Rewired.ISixDofControllerTemplate.button20  get=0x0A9D3458
  Rewired.ISixDofControllerTemplate.button21  get=0x0A9D3498
  Rewired.ISixDofControllerTemplate.button22  get=0x0A9D34D8
  Rewired.ISixDofControllerTemplate.button23  get=0x0A9D3518
  Rewired.ISixDofControllerTemplate.button24  get=0x0A9D3558
  Rewired.ISixDofControllerTemplate.button25  get=0x0A9D3598
  Rewired.ISixDofControllerTemplate.button26  get=0x0A9D35D8
  Rewired.ISixDofControllerTemplate.button27  get=0x0A9D3618
  Rewired.ISixDofControllerTemplate.button28  get=0x0A9D3658
  Rewired.ISixDofControllerTemplate.button29  get=0x0A9D3698
  Rewired.ISixDofControllerTemplate.button30  get=0x0A9D3718
  Rewired.ISixDofControllerTemplate.button31  get=0x0A9D3758
  Rewired.ISixDofControllerTemplate.button32  get=0x0A9D3798
  Rewired.ISixDofControllerTemplate.hat1  get=0x0A9D3A98
  Rewired.ISixDofControllerTemplate.hat2  get=0x0A9D3AD8
  Rewired.ISixDofControllerTemplate.throttle1  get=0x0A9D3B58
  Rewired.ISixDofControllerTemplate.throttle2  get=0x0A9D3B98
  Rewired.ISixDofControllerTemplate.stick  get=0x0A9D3B18
METHODS:
  RVA=0x0A9C86D0  token=0x6000203  System.Void .ctor(System.Object payload)
  RVA=0x0A9D3BD8  token=0x6000204  System.Void .cctor()
END_CLASS

CLASS: Rewired.InputManager
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x58
EXTENDS: Rewired.InputManager_Base
FIELDS:
  private           System.Boolean                  ignoreRecompile  // 0x50
METHODS:
  RVA=0x03FCE990  token=0x6000205  System.Void OnInitialized()
  RVA=0x0A9CB20C  token=0x6000206  System.Void OnDeinitialized()
  RVA=0x04D30AD0  token=0x6000207  System.Void DetectPlatform()
  RVA=0x041E1670  token=0x6000208  System.Void CheckRecompile()
  RVA=0x04D5E7E0  token=0x6000209  Rewired.Utils.Interfaces.IExternalTools GetExternalTools()
  RVA=0x0A9CB184  token=0x600020A  System.Boolean CheckDeviceName(System.String searchPattern, System.String deviceName, System.String deviceModel)
  RVA=0x03FCEEE0  token=0x600020B  System.Void SubscribeEvents()
  RVA=0x03FCEF90  token=0x600020C  System.Void UnsubscribeEvents()
  RVA=0x03CB71B0  token=0x600020D  System.Void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
  RVA=0x04D1C6F0  token=0x600020E  System.Void .ctor()
END_CLASS

CLASS: Rewired.Data.UserDataStore_File
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x68
EXTENDS: Rewired.Data.UserDataStore_KeyValue
FIELDS:
  private   static readonly System.String                   thisScriptName  // static @ 0x0
  private   static  System.String                   logPrefix  // const
  private   static  System.String                   defaultExtensionText  // const
  private   static  System.String                   defaultExtensionBinary  // const
  private   static  System.String                   defaultFileName  // const
  private           System.String                   _fileName  // 0x38
  private           Rewired.Data.UserDataStore_File.DataFormat_dataFormat  // 0x40
  private           System.String                   __directory  // 0x48
  private           Rewired.Data.UserDataStore_File.DataStore_dataStore  // 0x50
  private           Rewired.Data.UserDataStore_File.IDataHandler__dataHandler  // 0x58
  private           System.Boolean                  _initialized  // 0x60
PROPERTIES:
  directory  get=0x0A9D4434  set=0x0A9D44B0
  fileName  get=0x04D85A50  set=0x0A9D44E0
  dataFormat  get=0x04D865E0  set=0x0A9D446C
  dataHandler  get=0x0A9D4334  set=0x0A9D4480
  dataStore  get=0x04D86270
METHODS:
  RVA=0x041E1670  token=0x6000238  System.Void SetInitialValues()
  RVA=0x0A9D4218  token=0x6000239  System.Void OnInitialize()
  RVA=0x0A9D4148  token=0x600023A  System.Void OnDataSourceChanged()
  RVA=0x0A9D42D4  token=0x600023B  System.Void .ctor()
  RVA=0x0A9D423C  token=0x600023C  System.Void .cctor()
  RVA=0x04D865E0  token=0x600023D  Rewired.Data.UserDataStore_File.DataFormat <get_dataHandler>b__17_0()
END_CLASS

CLASS: Rewired.Data.UserDataStore_KeyValue
TYPE:  abstract class
TOKEN: 0x2000018
SIZE:  0x38
EXTENDS: Rewired.Data.UserDataStore
FIELDS:
  private   static readonly System.String                   thisScriptName  // static @ 0x0
  private   static  System.String                   logPrefix  // const
  private   static  System.String                   key_controllerAssignments  // const
  private   static  System.Int32                    controllerMapKeyVersion  // const
  private           System.Boolean                  _isEnabled  // 0x18
  private           System.Boolean                  _loadDataOnStart  // 0x19
  private           System.Boolean                  _loadJoystickAssignments  // 0x1a
  private           System.Boolean                  _loadKeyboardAssignments  // 0x1b
  private           System.Boolean                  _loadMouseAssignments  // 0x1c
  private           System.Boolean                  _allowImpreciseJoystickAssignmentMatching  // 0x1d
  private           System.Boolean                  _deferredJoystickAssignmentLoadPending  // 0x1e
  private           System.Boolean                  _wasJoystickEverDetected  // 0x1f
  private           System.Collections.Generic.List<System.Int32>__allActionIds  // 0x20
  private           System.String                   __allActionIdsString  // 0x28
  private   readonly System.Text.StringBuilder       _sb  // 0x30
PROPERTIES:
  isEnabled  get=0x04D86340  set=0x04D86350
  loadDataOnStart  get=0x04D866A0  set=0x04D866C0
  loadJoystickAssignments  get=0x015AF940  set=0x04D866E0
  loadKeyboardAssignments  get=0x04D895D0  set=0x04D89600
  loadMouseAssignments  get=0x04D86150  set=0x04D86160
  dataStore  get=-1  // abstract
  loadControllerAssignments  get=0x0A9D9A2C
  allActionIds  get=0x0A9D98EC
  allActionIdsString  get=0x0A9D97D4
METHODS:
  RVA=0x0A9D934C  token=0x6000262  System.Void Save()
  RVA=0x0A9D86D0  token=0x6000263  System.Void SaveControllerData(System.Int32 playerId, Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9D85FC  token=0x6000264  System.Void SaveControllerData(Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9D928C  token=0x6000265  System.Void SavePlayerData(System.Int32 playerId)
  RVA=0x0A9D8DE0  token=0x6000266  System.Void SaveInputBehavior(System.Int32 playerId, System.Int32 behaviorId)
  RVA=0x0A9D7D84  token=0x6000267  System.Void Load()
  RVA=0x0A9D5EE4  token=0x6000268  System.Void LoadControllerData(System.Int32 playerId, Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9D5FC8  token=0x6000269  System.Void LoadControllerData(Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9D7CC4  token=0x600026A  System.Void LoadPlayerData(System.Int32 playerId)
  RVA=0x0A9D664C  token=0x600026B  System.Void LoadInputBehavior(System.Int32 playerId, System.Int32 behaviorId)
  RVA=0x0A9D7F18  token=0x600026C  System.Void OnInitialize()
  RVA=0x0A9D7E40  token=0x600026D  System.Void OnControllerConnected(Rewired.ControllerStatusChangedEventArgs args)
  RVA=0x0A9D7EE8  token=0x600026E  System.Void OnControllerPreDisconnect(Rewired.ControllerStatusChangedEventArgs args)
  RVA=0x0A9D7EB8  token=0x600026F  System.Void OnControllerDisconnected(Rewired.ControllerStatusChangedEventArgs args)
  RVA=0x0A9D898C  token=0x6000270  System.Void SaveControllerMap(System.Int32 playerId, Rewired.ControllerMap controllerMap)
  RVA=0x0A9D61AC  token=0x6000271  Rewired.ControllerMap LoadControllerMap(System.Int32 playerId, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId)
  RVA=0x0A9D4CC0  token=0x6000272  System.Void ClearSaveData()
  RVA=0x0A9D5B50  token=0x6000273  System.Int32 LoadAll()
  RVA=0x0A9D7A2C  token=0x6000274  System.Int32 LoadPlayerDataNow(System.Int32 playerId)
  RVA=0x0A9D7A9C  token=0x6000275  System.Int32 LoadPlayerDataNow(Rewired.Player player)
  RVA=0x0A9D5A84  token=0x6000276  System.Int32 LoadAllJoystickCalibrationData()
  RVA=0x0A9D7574  token=0x6000277  System.Int32 LoadJoystickCalibrationData(Rewired.Joystick joystick)
  RVA=0x0A9D75AC  token=0x6000278  System.Int32 LoadJoystickCalibrationData(System.Int32 joystickId)
  RVA=0x0A9D761C  token=0x6000279  System.Int32 LoadJoystickData(System.Int32 joystickId)
  RVA=0x0A9D5E58  token=0x600027A  System.Int32 LoadControllerDataNow(System.Int32 playerId, Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9D5EC4  token=0x600027B  System.Int32 LoadControllerDataNow(Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9D626C  token=0x600027C  System.Int32 LoadControllerMaps(System.Int32 playerId, Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9D6098  token=0x600027D  Rewired.ControllerMap LoadControllerMap(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId)
  RVA=0x0A9D671C  token=0x600027E  System.Int32 LoadInputBehaviors(System.Int32 playerId)
  RVA=0x0A9D64E4  token=0x600027F  System.Int32 LoadInputBehaviorNow(System.Int32 playerId, System.Int32 behaviorId)
  RVA=0x0A9D65A8  token=0x6000280  System.Int32 LoadInputBehaviorNow(Rewired.Player player, Rewired.InputBehavior inputBehavior)
  RVA=0x0A9D5D98  token=0x6000281  System.Boolean LoadControllerAssignmentsNow()
  RVA=0x0A9D7764  token=0x6000282  System.Boolean LoadKeyboardAndMouseAssignmentsNow(Rewired.Data.UserDataStore_KeyValue.ControllerAssignmentSaveInfo data)
  RVA=0x0A9D68A0  token=0x6000283  System.Boolean LoadJoystickAssignmentsNow(Rewired.Data.UserDataStore_KeyValue.ControllerAssignmentSaveInfo data)
  RVA=0x0A9D5C54  token=0x6000284  Rewired.Data.UserDataStore_KeyValue.ControllerAssignmentSaveInfo LoadControllerAssignmentData()
  RVA=0x0A9D6844  token=0x6000285  System.Collections.IEnumerator LoadJoystickAssignmentsDeferred()
  RVA=0x0A9D80D8  token=0x6000286  System.Void SaveAll()
  RVA=0x0A9D9204  token=0x6000287  System.Void SavePlayerDataNow(System.Int32 playerId)
  RVA=0x0A9D9140  token=0x6000288  System.Void SavePlayerDataNow(Rewired.Player player)
  RVA=0x0A9D801C  token=0x6000289  System.Void SaveAllJoystickCalibrationData()
  RVA=0x0A9D8F1C  token=0x600028A  System.Void SaveJoystickCalibrationData(System.Int32 joystickId)
  RVA=0x0A9D8F8C  token=0x600028B  System.Void SaveJoystickCalibrationData(Rewired.Joystick joystick)
  RVA=0x0A9D9020  token=0x600028C  System.Void SaveJoystickData(System.Int32 joystickId)
  RVA=0x0A9D85A0  token=0x600028D  System.Void SaveControllerDataNow(System.Int32 playerId, Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9D85E8  token=0x600028E  System.Void SaveControllerDataNow(Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9D8B3C  token=0x600028F  System.Void SaveControllerMaps(Rewired.Player player, Rewired.PlayerSaveData playerSaveData)
  RVA=0x0A9D8A30  token=0x6000290  System.Void SaveControllerMaps(System.Int32 playerId, Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9D87B4  token=0x6000291  System.Void SaveControllerMap(Rewired.Player player, Rewired.ControllerMap controllerMap)
  RVA=0x0A9D8EB0  token=0x6000292  System.Void SaveInputBehaviors(Rewired.Player player, Rewired.PlayerSaveData playerSaveData)
  RVA=0x0A9D8D04  token=0x6000293  System.Void SaveInputBehaviorNow(System.Int32 playerId, System.Int32 behaviorId)
  RVA=0x0A9D8C90  token=0x6000294  System.Void SaveInputBehaviorNow(Rewired.Player player, Rewired.InputBehavior inputBehavior)
  RVA=0x0A9D81D4  token=0x6000295  System.Boolean SaveControllerAssignments()
  RVA=0x0A9D4A00  token=0x6000296  System.Void AppendBaseKey(System.Text.StringBuilder sb, Rewired.Player player)
  RVA=0x0A9D4F6C  token=0x6000297  System.String GetControllerMapKey(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId, System.Int32 ppKeyVersion)
  RVA=0x0A9D5088  token=0x6000298  System.String GetControllerMapKnownActionIdsKey(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId, System.Int32 ppKeyVersion)
  RVA=0x0A9D4A70  token=0x6000299  System.Void AppendControllerMapKeyCommonSuffix(System.Text.StringBuilder sb, Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId, System.Int32 keyVersion)
  RVA=0x0A9D58EC  token=0x600029A  System.String GetJoystickCalibrationMapKey(Rewired.Joystick joystick)
  RVA=0x0A9D5770  token=0x600029B  System.String GetInputBehaviorKey(Rewired.Player player, System.Int32 inputBehaviorId)
  RVA=0x0A9D4E64  token=0x600029C  System.String GetControllerMapJson(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId)
  RVA=0x0A9D51A4  token=0x600029D  System.Collections.Generic.List<System.Int32> GetControllerMapKnownActionIds(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId)
  RVA=0x0A9D5868  token=0x600029E  System.String GetJoystickCalibrationMapJson(Rewired.Joystick joystick)
  RVA=0x0A9D56DC  token=0x600029F  System.String GetInputBehaviorJson(Rewired.Player player, System.Int32 id)
  RVA=0x0A9D4510  token=0x60002A0  System.Void AddDefaultMappingsForNewActions(Rewired.ControllerIdentifier controllerIdentifier, Rewired.ControllerMap controllerMap, System.Collections.Generic.List<System.Int32> knownActionIds)
  RVA=0x0A9D4D14  token=0x60002A1  Rewired.Joystick FindJoystickPrecise(Rewired.Data.UserDataStore_KeyValue.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo)
  RVA=0x0A9D9408  token=0x60002A2  System.Boolean TryFindJoysticksImprecise(Rewired.Data.UserDataStore_KeyValue.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo, System.Collections.Generic.List<Rewired.Joystick>& matches)
  RVA=0x0A9D7FA0  token=0x60002A3  System.Void RefreshLayoutManager(System.Int32 playerId)
  RVA=0x0A9D537C  token=0x60002A4  System.Int32 GetDuplicateIndex(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier)
  RVA=0x0A9D95BC  token=0x60002A5  System.Boolean TryGetString(Rewired.Data.UserDataStore_KeyValue.IDataStore store, System.String key, System.String& result)
  RVA=0x0A9D9758  token=0x60002A6  System.Void .ctor()
  RVA=0x0A9D96C0  token=0x60002A7  System.Void .cctor()
END_CLASS

CLASS: Rewired.Data.UserDataStore_PlayerPrefs
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x40
EXTENDS: Rewired.Data.UserDataStore
FIELDS:
  private   static  System.String                   thisScriptName  // const
  private   static  System.String                   logPrefix  // const
  private   static  System.String                   editorLoadedMessage  // const
  private   static  System.String                   playerPrefsKeySuffix_controllerAssignments  // const
  private   static  System.Int32                    controllerMapPPKeyVersion_original  // const
  private   static  System.Int32                    controllerMapPPKeyVersion_includeDuplicateJoystickIndex  // const
  private   static  System.Int32                    controllerMapPPKeyVersion_supportDisconnectedControllers  // const
  private   static  System.Int32                    controllerMapPPKeyVersion_includeFormatVersion  // const
  private   static  System.Int32                    controllerMapPPKeyVersion  // const
  private           System.Boolean                  isEnabled  // 0x18
  private           System.Boolean                  loadDataOnStart  // 0x19
  private           System.Boolean                  loadJoystickAssignments  // 0x1a
  private           System.Boolean                  loadKeyboardAssignments  // 0x1b
  private           System.Boolean                  loadMouseAssignments  // 0x1c
  private           System.String                   playerPrefsKeyPrefix  // 0x20
  private           System.Boolean                  allowImpreciseJoystickAssignmentMatching  // 0x28
  private           System.Boolean                  deferredJoystickAssignmentLoadPending  // 0x29
  private           System.Boolean                  wasJoystickEverDetected  // 0x2a
  private           System.Collections.Generic.List<System.Int32>__allActionIds  // 0x30
  private           System.String                   __allActionIdsString  // 0x38
PROPERTIES:
  IsEnabled  get=0x04D86340  set=0x04D86350
  LoadDataOnStart  get=0x04D866A0  set=0x04D866C0
  LoadJoystickAssignments  get=0x015AF940  set=0x04D866E0
  LoadKeyboardAssignments  get=0x04D895D0  set=0x04D89600
  LoadMouseAssignments  get=0x04D86150  set=0x04D86160
  PlayerPrefsKeyPrefix  get=0x04D862C0  set=0x05390F40
  playerPrefsKey_controllerAssignments  get=0x0A9DE92C
  loadControllerAssignments  get=0x0A9D9A2C
  allActionIds  get=0x0A9DE7EC
  allActionIdsString  get=0x0A9DE6D4
METHODS:
  RVA=0x0A9DE45C  token=0x60002D4  System.Void Save()
  RVA=0x0A9DD8EC  token=0x60002D5  System.Void SaveControllerData(System.Int32 playerId, Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9DD984  token=0x60002D6  System.Void SaveControllerData(Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9DE3E4  token=0x60002D7  System.Void SavePlayerData(System.Int32 playerId)
  RVA=0x0A9DDFB0  token=0x60002D8  System.Void SaveInputBehavior(System.Int32 playerId, System.Int32 behaviorId)
  RVA=0x0A9DD0FC  token=0x60002D9  System.Void Load()
  RVA=0x0A9DB408  token=0x60002DA  System.Void LoadControllerData(System.Int32 playerId, Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9DB380  token=0x60002DB  System.Void LoadControllerData(Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9DD084  token=0x60002DC  System.Void LoadPlayerData(System.Int32 playerId)
  RVA=0x0A9DBA54  token=0x60002DD  System.Void LoadInputBehavior(System.Int32 playerId, System.Int32 behaviorId)
  RVA=0x0A9DD240  token=0x60002DE  System.Void OnInitialize()
  RVA=0x0A9DD168  token=0x60002DF  System.Void OnControllerConnected(Rewired.ControllerStatusChangedEventArgs args)
  RVA=0x0A9DD210  token=0x60002E0  System.Void OnControllerPreDisconnect(Rewired.ControllerStatusChangedEventArgs args)
  RVA=0x0A9DD1E0  token=0x60002E1  System.Void OnControllerDisconnected(Rewired.ControllerStatusChangedEventArgs args)
  RVA=0x0A9DDA0C  token=0x60002E2  System.Void SaveControllerMap(System.Int32 playerId, Rewired.ControllerMap controllerMap)
  RVA=0x0A9DB4A0  token=0x60002E3  Rewired.ControllerMap LoadControllerMap(System.Int32 playerId, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId)
  RVA=0x0A9DB018  token=0x60002E4  System.Int32 LoadAll()
  RVA=0x0A9DCDEC  token=0x60002E5  System.Int32 LoadPlayerDataNow(System.Int32 playerId)
  RVA=0x0A9DCE5C  token=0x60002E6  System.Int32 LoadPlayerDataNow(Rewired.Player player)
  RVA=0x0A9DAF4C  token=0x60002E7  System.Int32 LoadAllJoystickCalibrationData()
  RVA=0x0A9DC934  token=0x60002E8  System.Int32 LoadJoystickCalibrationData(Rewired.Joystick joystick)
  RVA=0x0A9DC96C  token=0x60002E9  System.Int32 LoadJoystickCalibrationData(System.Int32 joystickId)
  RVA=0x0A9DC9DC  token=0x60002EA  System.Int32 LoadJoystickData(System.Int32 joystickId)
  RVA=0x0A9DB2F4  token=0x60002EB  System.Int32 LoadControllerDataNow(System.Int32 playerId, Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9DB360  token=0x60002EC  System.Int32 LoadControllerDataNow(Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9DB674  token=0x60002ED  System.Int32 LoadControllerMaps(System.Int32 playerId, Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9DB560  token=0x60002EE  Rewired.ControllerMap LoadControllerMap(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId)
  RVA=0x0A9DBADC  token=0x60002EF  System.Int32 LoadInputBehaviors(System.Int32 playerId)
  RVA=0x0A9DB990  token=0x60002F0  System.Int32 LoadInputBehaviorNow(System.Int32 playerId, System.Int32 behaviorId)
  RVA=0x0A9DB8EC  token=0x60002F1  System.Int32 LoadInputBehaviorNow(Rewired.Player player, Rewired.InputBehavior inputBehavior)
  RVA=0x0A9DB234  token=0x60002F2  System.Boolean LoadControllerAssignmentsNow()
  RVA=0x0A9DCB24  token=0x60002F3  System.Boolean LoadKeyboardAndMouseAssignmentsNow(Rewired.Data.UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo data)
  RVA=0x0A9DBC60  token=0x60002F4  System.Boolean LoadJoystickAssignmentsNow(Rewired.Data.UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo data)
  RVA=0x0A9DB11C  token=0x60002F5  Rewired.Data.UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo LoadControllerAssignmentData()
  RVA=0x0A9DBC04  token=0x60002F6  System.Collections.IEnumerator LoadJoystickAssignmentsDeferred()
  RVA=0x0A9DD400  token=0x60002F7  System.Void SaveAll()
  RVA=0x0A9DE2A8  token=0x60002F8  System.Void SavePlayerDataNow(System.Int32 playerId)
  RVA=0x0A9DE320  token=0x60002F9  System.Void SavePlayerDataNow(Rewired.Player player)
  RVA=0x0A9DD344  token=0x60002FA  System.Void SaveAllJoystickCalibrationData()
  RVA=0x0A9DE0A4  token=0x60002FB  System.Void SaveJoystickCalibrationData(System.Int32 joystickId)
  RVA=0x0A9DE114  token=0x60002FC  System.Void SaveJoystickCalibrationData(Rewired.Joystick joystick)
  RVA=0x0A9DE188  token=0x60002FD  System.Void SaveJoystickData(System.Int32 joystickId)
  RVA=0x0A9DD8A0  token=0x60002FE  System.Void SaveControllerDataNow(System.Int32 playerId, Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9DD87C  token=0x60002FF  System.Void SaveControllerDataNow(Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9DDC3C  token=0x6000300  System.Void SaveControllerMaps(Rewired.Player player, Rewired.PlayerSaveData playerSaveData)
  RVA=0x0A9DDD90  token=0x6000301  System.Void SaveControllerMaps(System.Int32 playerId, Rewired.ControllerType controllerType, System.Int32 controllerId)
  RVA=0x0A9DDA98  token=0x6000302  System.Void SaveControllerMap(Rewired.Player player, Rewired.ControllerMap controllerMap)
  RVA=0x0A9DE038  token=0x6000303  System.Void SaveInputBehaviors(Rewired.Player player, Rewired.PlayerSaveData playerSaveData)
  RVA=0x0A9DDEE8  token=0x6000304  System.Void SaveInputBehaviorNow(System.Int32 playerId, System.Int32 behaviorId)
  RVA=0x0A9DDE9C  token=0x6000305  System.Void SaveInputBehaviorNow(Rewired.Player player, Rewired.InputBehavior inputBehavior)
  RVA=0x0A9DD4E4  token=0x6000306  System.Boolean SaveControllerAssignments()
  RVA=0x0A9D9F30  token=0x6000307  System.Boolean ControllerAssignmentSaveDataExists()
  RVA=0x0A9DA0CC  token=0x6000308  System.String GetBasePlayerPrefsKey(Rewired.Player player)
  RVA=0x0A9DA698  token=0x6000309  System.String GetControllerMapPlayerPrefsKey(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId, System.Int32 ppKeyVersion)
  RVA=0x0A9DA130  token=0x600030A  System.String GetControllerMapKnownActionIdsPlayerPrefsKey(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId, System.Int32 ppKeyVersion)
  RVA=0x0A9DA394  token=0x600030B  System.String GetControllerMapPlayerPrefsKeyCommonSuffix(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId, System.Int32 ppKeyVersion)
  RVA=0x0A9DADA8  token=0x600030C  System.String GetJoystickCalibrationMapPlayerPrefsKey(Rewired.Joystick joystick)
  RVA=0x0A9DAC98  token=0x600030D  System.String GetInputBehaviorPlayerPrefsKey(Rewired.Player player, System.Int32 inputBehaviorId)
  RVA=0x0A9DA880  token=0x600030E  System.String GetControllerMapXml(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId)
  RVA=0x0A9DA1F8  token=0x600030F  System.Collections.Generic.List<System.Int32> GetControllerMapKnownActionIds(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier, System.Int32 categoryId, System.Int32 layoutId)
  RVA=0x0A9DAED8  token=0x6000310  System.String GetJoystickCalibrationMapXml(Rewired.Joystick joystick)
  RVA=0x0A9DAD24  token=0x6000311  System.String GetInputBehaviorXml(Rewired.Player player, System.Int32 id)
  RVA=0x0A9D9A40  token=0x6000312  System.Void AddDefaultMappingsForNewActions(Rewired.ControllerIdentifier controllerIdentifier, Rewired.ControllerMap controllerMap, System.Collections.Generic.List<System.Int32> knownActionIds)
  RVA=0x0A9D9F7C  token=0x6000313  Rewired.Joystick FindJoystickPrecise(Rewired.Data.UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo)
  RVA=0x0A9DE4C8  token=0x6000314  System.Boolean TryFindJoysticksImprecise(Rewired.Data.UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo, System.Collections.Generic.List<Rewired.Joystick>& matches)
  RVA=0x0A9DA938  token=0x6000315  System.Int32 GetDuplicateIndex(Rewired.Player player, Rewired.ControllerIdentifier controllerIdentifier)
  RVA=0x0A9DD2C8  token=0x6000316  System.Void RefreshLayoutManager(System.Int32 playerId)
  RVA=0x0A9DA760  token=0x6000317  System.Type GetControllerMapType(Rewired.ControllerType controllerType)
  RVA=0x0A9DE67C  token=0x6000318  System.Void .ctor()
END_CLASS

CLASS: Rewired.Integration.UnityUI.PlayerPointerEventData
TYPE:  class
TOKEN: 0x200002C
SIZE:  0x180
EXTENDS: UnityEngine.EventSystems.PointerEventData
FIELDS:
  private           System.Int32                    <playerId>k__BackingField  // 0x160
  private           System.Int32                    <inputSourceIndex>k__BackingField  // 0x164
  private           Rewired.UI.IMouseInputSource    <mouseSource>k__BackingField  // 0x168
  private           Rewired.UI.ITouchInputSource    <touchSource>k__BackingField  // 0x170
  private           Rewired.Integration.UnityUI.PointerEventType<sourceType>k__BackingField  // 0x178
  private           System.Int32                    <buttonIndex>k__BackingField  // 0x17c
PROPERTIES:
  playerId  get=0x04DA6C30  set=0x04DA6C40
  inputSourceIndex  get=0x04D903F0  set=0x04D90470
  mouseSource  get=0x04D8DF00  set=0x06B8B954
  touchSource  get=0x04D8DEF0  set=0x06B8B940
  sourceType  get=0x04DA2300  set=0x04DA6C50
  buttonIndex  get=0x04D92A50  set=0x04D92A60
METHODS:
  RVA=0x0A9CBE54  token=0x6000340  System.Void .ctor(UnityEngine.EventSystems.EventSystem eventSystem)
  RVA=0x0A9CBC08  token=0x6000341  System.String ToString()
END_CLASS

CLASS: Rewired.Integration.UnityUI.RewiredEventSystem
TYPE:  class
TOKEN: 0x200002D
SIZE:  0x58
EXTENDS: UnityEngine.EventSystems.EventSystem
FIELDS:
  private           System.Boolean                  _alwaysUpdate  // 0x50
PROPERTIES:
  alwaysUpdate  get=0x04D86800  set=0x04D86840
METHODS:
  RVA=0x0A9CC960  token=0x6000344  System.Void Update()
  RVA=0x0A9CCA9C  token=0x6000345  System.Void .ctor()
END_CLASS

CLASS: Rewired.Integration.UnityUI.RewiredPointerInputModule
TYPE:  abstract class
TOKEN: 0x200002E
SIZE:  0x70
EXTENDS: UnityEngine.EventSystems.BaseInputModule
FIELDS:
  public    static  System.Int32                    kMouseLeftId  // const
  public    static  System.Int32                    kMouseRightId  // const
  public    static  System.Int32                    kMouseMiddleId  // const
  public    static  System.Int32                    kFakeTouchesId  // const
  private   static  System.Int32                    customButtonsStartingId  // const
  private   static  System.Int32                    customButtonsMaxCount  // const
  private   static  System.Int32                    customButtonsLastId  // const
  private   readonly System.Collections.Generic.List<Rewired.UI.IMouseInputSource>m_MouseInputSourcesList  // 0x48
  private           System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.Dictionary<System.Int32,Rewired.Integration.UnityUI.PlayerPointerEventData>[]>m_PlayerPointerData  // 0x50
  private           Rewired.UI.ITouchInputSource    m_UserDefaultTouchInputSource  // 0x58
  private           Rewired.Integration.UnityUI.RewiredPointerInputModule.UnityInputSource__m_DefaultInputSource  // 0x60
  private   readonly Rewired.Integration.UnityUI.RewiredPointerInputModule.MouseStatem_MouseState  // 0x68
PROPERTIES:
  defaultInputSource  get=0x0A9CF360
  defaultMouseInputSource  get=0x0A9CF3C8
  defaultTouchInputSource  get=0x0A9CF3C8
  isMouseSupported  get=0x0A9CF3D0
METHODS:
  RVA=0x0A9CE5FC  token=0x6000349  System.Boolean IsDefaultMouse(Rewired.UI.IMouseInputSource mouse)
  RVA=0x0A9CD648  token=0x600034A  Rewired.UI.IMouseInputSource GetMouseInputSource(System.Int32 playerId, System.Int32 mouseIndex)
  RVA=0x0A9CEB4C  token=0x600034B  System.Void RemoveMouseInputSource(Rewired.UI.IMouseInputSource source)
  RVA=0x0A9CCAE8  token=0x600034C  System.Void AddMouseInputSource(Rewired.UI.IMouseInputSource source)
  RVA=0x0A9CD530  token=0x600034D  System.Int32 GetMouseInputSourceCount(System.Int32 playerId)
  RVA=0x0A9CE33C  token=0x600034E  Rewired.UI.ITouchInputSource GetTouchInputSource(System.Int32 playerId, System.Int32 sourceIndex)
  RVA=0x0A9CECA0  token=0x600034F  System.Void RemoveTouchInputSource(Rewired.UI.ITouchInputSource source)
  RVA=0x0A9CCBC4  token=0x6000350  System.Void AddTouchInputSource(Rewired.UI.ITouchInputSource source)
  RVA=0x0A9CE314  token=0x6000351  System.Int32 GetTouchInputSourceCount(System.Int32 playerId)
  RVA=0x0A9CCC88  token=0x6000352  System.Void ClearMouseInputSources()
  RVA=-1  // abstract  token=0x6000354  System.Boolean IsDefaultPlayer(System.Int32 playerId)
  RVA=0x0A9CDFAC  token=0x6000355  System.Boolean GetPointerData(System.Int32 playerId, System.Int32 pointerIndex, System.Int32 pointerTypeId, Rewired.Integration.UnityUI.PlayerPointerEventData& data, System.Boolean create, Rewired.Integration.UnityUI.PointerEventType pointerEventType)
  RVA=0x0A9CD104  token=0x6000356  Rewired.Integration.UnityUI.PlayerPointerEventData CreatePointerEventData(System.Int32 playerId, System.Int32 pointerIndex, System.Int32 pointerTypeId, Rewired.Integration.UnityUI.PointerEventType pointerEventType)
  RVA=0x0A9CEBF4  token=0x6000357  System.Void RemovePointerData(Rewired.Integration.UnityUI.PlayerPointerEventData data)
  RVA=0x0A9CE3B0  token=0x6000358  Rewired.Integration.UnityUI.PlayerPointerEventData GetTouchPointerEventData(System.Int32 playerId, System.Int32 touchDeviceIndex, UnityEngine.Touch input, System.Boolean& pressed, System.Boolean& released)
  RVA=0x0A9CD7E0  token=0x6000359  Rewired.Integration.UnityUI.RewiredPointerInputModule.MouseState GetMousePointerEventData(System.Int32 playerId, System.Int32 mouseIndex)
  RVA=0x0A9CD30C  token=0x600035A  Rewired.Integration.UnityUI.PlayerPointerEventData GetLastPointerEventData(System.Int32 playerId, System.Int32 pointerIndex, System.Int32 pointerTypeId, System.Boolean ignorePointerTypeId, Rewired.Integration.UnityUI.PointerEventType pointerEventType)
  RVA=0x0A9CED1C  token=0x600035B  System.Boolean ShouldStartDrag(UnityEngine.Vector2 pressPos, UnityEngine.Vector2 currentPos, System.Single threshold, System.Boolean useDragThreshold)
  RVA=0x0A9CEA68  token=0x600035C  System.Void ProcessMove(Rewired.Integration.UnityUI.PlayerPointerEventData pointerEvent)
  RVA=0x0A9CE808  token=0x600035D  System.Void ProcessDrag(Rewired.Integration.UnityUI.PlayerPointerEventData pointerEvent)
  RVA=0x0A9CE618  token=0x600035E  System.Boolean IsPointerOverGameObject(System.Int32 pointerTypeId)
  RVA=0x0A9CCCD0  token=0x600035F  System.Void ClearSelection()
  RVA=0x0A9CEDC8  token=0x6000360  System.String ToString()
  RVA=0x0A9CD240  token=0x6000361  System.Void DeselectIfSelectionChanged(UnityEngine.GameObject currentOverGo, UnityEngine.EventSystems.BaseEventData pointerEvent)
  RVA=0x0A9CD028  token=0x6000362  System.Void CopyFromTo(UnityEngine.EventSystems.PointerEventData from, UnityEngine.EventSystems.PointerEventData to)
  RVA=0x0A9CED5C  token=0x6000363  UnityEngine.EventSystems.PointerEventData.FramePressState StateForMouseButton(System.Int32 playerId, System.Int32 mouseIndex, System.Int32 buttonId)
  RVA=0x0A9CF25C  token=0x6000364  System.Void .ctor()
END_CLASS

CLASS: Rewired.Integration.UnityUI.PointerEventType
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Rewired.Integration.UnityUI.PointerEventTypeMouse  // const
  public    static  Rewired.Integration.UnityUI.PointerEventTypeTouch  // const
METHODS:
END_CLASS

CLASS: Rewired.Integration.UnityUI.RewiredStandaloneInputModule
TYPE:  sealed class
TOKEN: 0x2000034
SIZE:  0x108
EXTENDS: Rewired.Integration.UnityUI.RewiredPointerInputModule
FIELDS:
  private   static  System.String                   DEFAULT_ACTION_MOVE_HORIZONTAL  // const
  private   static  System.String                   DEFAULT_ACTION_MOVE_VERTICAL  // const
  private   static  System.String                   DEFAULT_ACTION_SUBMIT  // const
  private   static  System.String                   DEFAULT_ACTION_CANCEL  // const
  private           Rewired.InputManager_Base       rewiredInputManager  // 0x70
  private           System.Boolean                  useAllRewiredGamePlayers  // 0x78
  private           System.Boolean                  useRewiredSystemPlayer  // 0x79
  private           System.Int32[]                  rewiredPlayerIds  // 0x80
  private           System.Boolean                  usePlayingPlayersOnly  // 0x88
  private           System.Collections.Generic.List<Rewired.Components.PlayerMouse>playerMice  // 0x90
  private           System.Boolean                  moveOneElementPerAxisPress  // 0x98
  private           System.Boolean                  setActionsById  // 0x99
  private           System.Int32                    horizontalActionId  // 0x9c
  private           System.Int32                    verticalActionId  // 0xa0
  private           System.Int32                    submitActionId  // 0xa4
  private           System.Int32                    cancelActionId  // 0xa8
  private           System.String                   m_HorizontalAxis  // 0xb0
  private           System.String                   m_VerticalAxis  // 0xb8
  private           System.String                   m_SubmitButton  // 0xc0
  private           System.String                   m_CancelButton  // 0xc8
  private           System.Single                   m_InputActionsPerSecond  // 0xd0
  private           System.Single                   m_RepeatDelay  // 0xd4
  private           System.Boolean                  m_allowMouseInput  // 0xd8
  private           System.Boolean                  m_allowMouseInputIfTouchSupported  // 0xd9
  private           System.Boolean                  m_allowTouchInput  // 0xda
  private           System.Boolean                  m_deselectIfBackgroundClicked  // 0xdb
  private           System.Boolean                  m_deselectBeforeSelecting  // 0xdc
  private           System.Boolean                  m_ForceModuleActive  // 0xdd
  private           System.Int32[]                  playerIds  // 0xe0
  private           System.Boolean                  recompiling  // 0xe8
  private           System.Boolean                  isTouchSupported  // 0xe9
  private           System.Double                   m_PrevActionTime  // 0xf0
  private           UnityEngine.Vector2             m_LastMoveVector  // 0xf8
  private           System.Int32                    m_ConsecutiveMoveCount  // 0x100
  private           System.Boolean                  m_HasFocus  // 0x104
PROPERTIES:
  RewiredInputManager  get=0x04D862B0  set=0x063AD760
  UseAllRewiredGamePlayers  get=0x04D86F90  set=0x0A9D2DD0
  UseRewiredSystemPlayer  get=0x04D88E40  set=0x0A9D2DEC
  RewiredPlayerIds  get=0x0A9D2914  set=0x0A9D2C3C
  UsePlayingPlayersOnly  get=0x011F2750  set=0x011F2760
  PlayerMice  get=0x0A9D289C  set=0x0A9D2B80
  MoveOneElementPerAxisPress  get=0x04D86C50  set=0x04D86C70
  allowMouseInput  get=0x04DA6C90  set=0x04DA6CE0
  allowMouseInputIfTouchSupported  get=0x04DA6C80  set=0x04DA6CD0
  allowTouchInput  get=0x04DA6CA0  set=0x04DA6CF0
  deselectIfBackgroundClicked  get=0x04DA6CB0  set=0x04DA6D00
  deselectBeforeSelecting  get=0x04D90A50  set=0x04D90A60
  SetActionsById  get=0x04DA34C0  set=0x0A9D2CC8
  HorizontalActionId  get=0x04D86C60  set=0x0A9D2A90
  VerticalActionId  get=0x04D88CD0  set=0x0A9D2E08
  SubmitActionId  get=0x04D88C90  set=0x0A9D2CE0
  CancelActionId  get=0x04D88C80  set=0x0A9D29A0
  isMouseSupported  get=0x0A9D2964
  isTouchAllowed  get=0x04DA6CA0
  allowActivationOnMobileDevice  get=0x04DA6C70  set=0x04DA6CC0
  forceModuleActive  get=0x04DA6C70  set=0x04DA6CC0
  inputActionsPerSecond  get=0x04D8E2E0  set=0x04D8E320
  repeatDelay  get=0x04D8E2C0  set=0x04D8E300
  horizontalAxis  get=0x04D86250  set=0x0A9D2FA0
  verticalAxis  get=0x04D85EF0  set=0x0A9D30F0
  submitButton  get=0x04D892F0  set=0x0A9D3048
  cancelButton  get=0x04D8D1D0  set=0x0A9D2EF8
METHODS:
  RVA=0x0A9D270C  token=0x60003B6  System.Void .ctor()
  RVA=0x0A9CF53C  token=0x60003B7  System.Void Awake()
  RVA=0x0A9D26A4  token=0x60003B8  System.Void UpdateModule()
  RVA=0x02FFF600  token=0x60003B9  System.Boolean IsModuleSupported()
  RVA=0x0A9D22C4  token=0x60003BA  System.Boolean ShouldActivateModule()
  RVA=0x0A9CF484  token=0x60003BB  System.Void ActivateModule()
  RVA=0x0A9CF828  token=0x60003BC  System.Void DeactivateModule()
  RVA=0x0A9D14F0  token=0x60003BD  System.Void Process()
  RVA=0x0A9D0DE4  token=0x60003BE  System.Boolean ProcessTouchEvents()
  RVA=0x0A9D0F48  token=0x60003BF  System.Void ProcessTouchPress(UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released)
  RVA=0x0A9D1904  token=0x60003C0  System.Boolean SendSubmitEventToSelectedObject()
  RVA=0x0A9CFB84  token=0x60003C1  UnityEngine.Vector2 GetRawMoveVector()
  RVA=0x0A9D15E0  token=0x60003C2  System.Boolean SendMoveEventToSelectedObject()
  RVA=0x0A9CF620  token=0x60003C3  System.Void CheckButtonOrKeyMovement(System.Boolean& downHorizontal, System.Boolean& downVertical)
  RVA=0x0A9D0754  token=0x60003C4  System.Void ProcessMouseEvents()
  RVA=0x0A9D0460  token=0x60003C5  System.Void ProcessMouseEvent(System.Int32 playerId, System.Int32 pointerIndex)
  RVA=0x0A9D1AF4  token=0x60003C6  System.Boolean SendUpdateEventToSelectedObject()
  RVA=0x0A9D0894  token=0x60003C7  System.Void ProcessMousePress(Rewired.Integration.UnityUI.RewiredPointerInputModule.MouseButtonEventData data)
  RVA=0x0A9CFECC  token=0x60003C8  System.Void HandleMouseTouchDeselectionOnSelectionChanged(UnityEngine.GameObject currentOverGo, UnityEngine.EventSystems.BaseEventData pointerEvent)
  RVA=0x04DA6C60  token=0x60003C9  System.Void OnApplicationFocus(System.Boolean hasFocus)
  RVA=0x0A9D263C  token=0x60003CA  System.Boolean ShouldIgnoreEventsOnNoFocus()
  RVA=0x0A9D032C  token=0x60003CB  System.Void OnDestroy()
  RVA=0x0A9D01D0  token=0x60003CC  System.Boolean IsDefaultPlayer(System.Int32 playerId)
  RVA=0x0A9D0038  token=0x60003CD  System.Void InitializeRewired()
  RVA=0x0A9D1F64  token=0x60003CE  System.Void SetupRewiredVars()
  RVA=0x0A9D1E78  token=0x60003CF  System.Void SetUpRewiredPlayerMice()
  RVA=0x0A9D1BC8  token=0x60003D0  System.Void SetUpRewiredActions()
  RVA=0x0A9CF9C8  token=0x60003D1  System.Boolean GetButton(Rewired.Player player, System.Int32 actionId)
  RVA=0x0A9CF994  token=0x60003D2  System.Boolean GetButtonDown(Rewired.Player player, System.Int32 actionId)
  RVA=0x0A9CFB50  token=0x60003D3  System.Boolean GetNegativeButton(Rewired.Player player, System.Int32 actionId)
  RVA=0x0A9CFB1C  token=0x60003D4  System.Boolean GetNegativeButtonDown(Rewired.Player player, System.Int32 actionId)
  RVA=0x0A9CF960  token=0x60003D5  System.Single GetAxis(Rewired.Player player, System.Int32 actionId)
  RVA=0x0A9CF78C  token=0x60003D6  System.Void CheckEditorRecompile()
  RVA=0x0A9D0440  token=0x60003D7  System.Void OnEditorRecompile()
  RVA=0x0A9CF7E8  token=0x60003D8  System.Void ClearRewiredVars()
  RVA=0x0A9CF830  token=0x60003D9  System.Boolean DidAnyMouseMove()
  RVA=0x0A9CF9FC  token=0x60003DA  System.Boolean GetMouseButtonDownOnAnyMouse(System.Int32 buttonIndex)
  RVA=0x0A9D0450  token=0x60003DB  System.Void OnRewiredInitialized()
  RVA=0x0A9D0458  token=0x60003DC  System.Void OnRewiredShutDown()
END_CLASS

CLASS: Rewired.Internal.ControllerTemplateFactory
TYPE:  static class
TOKEN: 0x200002B
SIZE:  0x10
FIELDS:
  private   static readonly System.Type[]                   _defaultTemplateTypes  // static @ 0x0
  private   static readonly System.Type[]                   _defaultTemplateInterfaceTypes  // static @ 0x8
PROPERTIES:
  templateTypes  get=0x0A9C7A88
  templateInterfaceTypes  get=0x0A9C7A38
METHODS:
  RVA=0x0A9C77FC  token=0x6000332  Rewired.IControllerTemplate Create(System.Guid typeGuid, System.Object payload)
  RVA=0x047F2CF0  token=0x6000333  System.Void .cctor()
END_CLASS

CLASS: Rewired.Localization.LocalizedStringProvider
TYPE:  class
TOKEN: 0x2000036
SIZE:  0x38
EXTENDS: Rewired.Localization.LocalizedStringProviderBase
FIELDS:
  private           UnityEngine.TextAsset           _localizedStringsFile  // 0x20
  private           System.Collections.Generic.Dictionary<System.String,System.String>_dictionary  // 0x28
  private           System.Boolean                  _initialized  // 0x30
PROPERTIES:
  dictionary  get=0x04D86240  set=0x02FDB880
  localizedStringsFile  get=0x04D862C0  set=0x0ABD381C
  initialized  get=0x04D865F0
METHODS:
  RVA=0x0ABD3578  token=0x60003E5  System.Boolean Initialize()
  RVA=0x0ABD3610  token=0x60003E6  System.Boolean TryLoadLocalizedStringData()
  RVA=0x0ABD3594  token=0x60003E7  System.Boolean TryGetLocalizedString(System.String key, System.String& result)
  RVA=0x0ABD379C  token=0x60003E8  System.Void .ctor()
END_CLASS

CLASS: Rewired.Localization.LocalizedStringProviderBase
TYPE:  abstract class
TOKEN: 0x2000037
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Rewired.Interfaces.ILocalizedStringProvider
FIELDS:
  private           System.Boolean                  _prefetch  // 0x18
PROPERTIES:
  prefetch  get=0x04D86340  set=0x0ABD34A4
  initialized  get=-1  // abstract
METHODS:
  RVA=0x0ABD31A0  token=0x60003EC  System.Void OnEnable()
  RVA=0x0ABD30B0  token=0x60003ED  System.Void OnDisable()
  RVA=0x041E1670  token=0x60003EE  System.Void Update()
  RVA=0x0ABD32F0  token=0x60003EF  System.Void TrySetLocalizedStringProvider()
  RVA=-1  // abstract  token=0x60003F0  System.Boolean Initialize()
  RVA=0x0ABD31D8  token=0x60003F1  System.Void Reload()
  RVA=-1  // abstract  token=0x60003F2  System.Boolean TryGetLocalizedString(System.String key, System.String& result)
  RVA=0x0ABD32A4  token=0x60003F3  System.Boolean Rewired.Interfaces.ILocalizedStringProvider.TryGetLocalizedString(System.String key, System.String& result)
  RVA=0x05393520  token=0x60003F4  System.Void .ctor()
END_CLASS

CLASS: Rewired.Utils.ExternalTools
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x28
IMPLEMENTS: Rewired.Utils.Interfaces.IExternalTools
FIELDS:
  private   static  System.Func<System.Object>      _getPlatformInitializerDelegate  // static @ 0x0
  private           System.Boolean                  _isEditorPaused  // 0x10
  private           System.Action<System.Boolean>   _EditorPausedStateChangedEvent  // 0x18
  private           System.Action<System.UInt32,System.Boolean>XboxOneInput_OnGamepadStateChange  // 0x20
PROPERTIES:
  getPlatformInitializerDelegate  get=0x0A9C8428  set=0x0A9C8560
  isEditorPaused  get=0x015EFCE0
  UnityInput_IsTouchPressureSupported  get=0x0A9C8420
EVENTS:
  EditorPausedStateChangedEvent  add=add_EditorPausedStateChangedEvent  remove=remove_EditorPausedStateChangedEvent
  XboxOneInput_OnGamepadStateChange  add=add_XboxOneInput_OnGamepadStateChange  remove=remove_XboxOneInput_OnGamepadStateChange
METHODS:
  RVA=0x041E1670  token=0x6000211  System.Void .ctor()
  RVA=0x041E1670  token=0x6000212  System.Void Destroy()
  RVA=0x0539494C  token=0x6000216  System.Object GetPlatformInitializer()
  RVA=0x0A9C81E8  token=0x6000217  System.String GetFocusedEditorWindowTitle()
  RVA=0x012081B0  token=0x6000218  System.Boolean IsEditorSceneViewFocused()
  RVA=0x012081B0  token=0x6000219  System.Boolean LinuxInput_IsJoystickPreconfigured(System.String name)
  RVA=0x011EC580  token=0x600021C  System.Int32 XboxOneInput_GetUserIdForGamepad(System.UInt32 id)
  RVA=0x011EC580  token=0x600021D  System.UInt64 XboxOneInput_GetControllerId(System.UInt32 unityJoystickId)
  RVA=0x012081B0  token=0x600021E  System.Boolean XboxOneInput_IsGamepadActive(System.UInt32 unityJoystickId)
  RVA=0x0A9C835C  token=0x600021F  System.String XboxOneInput_GetControllerType(System.UInt64 xboxControllerId)
  RVA=0x011EC580  token=0x6000220  System.UInt32 XboxOneInput_GetJoystickId(System.UInt64 xboxControllerId)
  RVA=0x041E1670  token=0x6000221  System.Void XboxOne_Gamepad_UpdatePlugin()
  RVA=0x012081B0  token=0x6000222  System.Boolean XboxOne_Gamepad_SetGamepadVibration(System.UInt64 xboxOneJoystickId, System.Single leftMotor, System.Single rightMotor, System.Single leftTriggerLevel, System.Single rightTriggerLevel)
  RVA=0x041E1670  token=0x6000223  System.Void XboxOne_Gamepad_PulseVibrateMotor(System.UInt64 xboxOneJoystickId, System.Int32 motorInt, System.Single startLevel, System.Single endLevel, System.UInt64 durationMS)
  RVA=0x0A9C8138  token=0x6000224  System.Void GetDeviceVIDPIDs(System.Collections.Generic.List<System.Int32>& vids, System.Collections.Generic.List<System.Int32>& pids)
  RVA=0x04DA2BB0  token=0x6000225  System.Int32 GetAndroidAPILevel()
  RVA=0x04031BA0  token=0x6000226  System.Void WindowsStandalone_ForwardRawInput(System.IntPtr rawInputHeaderIndices, System.IntPtr rawInputDataIndices, System.UInt32 indicesCount, System.IntPtr rawInputData, System.UInt32 rawInputDataSize)
  RVA=0x0A9C8220  token=0x6000227  System.Boolean UnityUI_Graphic_GetRaycastTarget(System.Object graphic)
  RVA=0x0A9C82B4  token=0x6000228  System.Void UnityUI_Graphic_SetRaycastTarget(System.Object graphic, System.Boolean value)
  RVA=0x04DA6C20  token=0x600022A  System.Single UnityInput_GetTouchPressure(UnityEngine.Touch& touch)
  RVA=0x04DA6C10  token=0x600022B  System.Single UnityInput_GetTouchMaximumPossiblePressure(UnityEngine.Touch& touch)
  RVA=0x0A9C80DC  token=0x600022C  Rewired.IControllerTemplate CreateControllerTemplate(System.Guid typeGuid, System.Object payload)
  RVA=0x0381A8D0  token=0x600022D  System.Type[] GetControllerTemplateTypes()
  RVA=0x0381A9A0  token=0x600022E  System.Type[] GetControllerTemplateInterfaceTypes()
END_CLASS

