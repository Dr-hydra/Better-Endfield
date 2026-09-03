// ========================================================
// Dumped by @desirepro
// Assembly: Hypergryph.CloudClientBridge.dll
// Classes:  39
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

CLASS: _OnReceiveData
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x09DDBB98  token=0x6000013  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x6000014  System.Void Invoke(System.IntPtr data, System.Int32& size)
END_CLASS

CLASS: _OnReceiveDataWithKey
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x09DDBAC4  token=0x6000015  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x01546280  token=0x6000016  System.Void Invoke(System.String key, System.IntPtr data, System.Int32& size)
END_CLASS

CLASS: _OnReceiveDataWithKeyEx
TYPE:  sealed class
TOKEN: 0x2000006
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x09DDB9F0  token=0x6000017  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x015547C0  token=0x6000018  System.Void Invoke(System.String key, System.IntPtr data, System.Int32& size, System.String userID)
END_CLASS

CLASS: _OnChangeResolution
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x09836370  token=0x6000019  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x600001A  System.Void Invoke(System.Int32 width, System.Int32 height)
END_CLASS

CLASS: <init>d__37
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x48
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncVoidMethodBuilder<>t__builder  // 0x18
  public            Hypergryph.CloudClientBridge.EventLogWrapper<>4__this  // 0x38
  private           System.Runtime.CompilerServices.TaskAwaiter<System.Boolean><>u__1  // 0x40
METHODS:
  RVA=0x09DDB660  token=0x6000048  System.Void MoveNext()
  RVA=0x053982D4  token=0x6000049  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <<EventTrack>b__0>d
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x40
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>t__builder  // 0x18
  public            Hypergryph.CloudClientBridge.EventLogWrapper.<>c__DisplayClass38_0<>4__this  // 0x30
  private           System.Runtime.CompilerServices.TaskAwaiter<>u__1  // 0x38
METHODS:
  RVA=0x09DDAC30  token=0x600004C  System.Void MoveNext()
  RVA=0x09DDAEC0  token=0x600004D  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <>c__DisplayClass38_0
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x28
FIELDS:
  public            Hypergryph.CloudClientBridge.EventLogWrapper<>4__this  // 0x10
  public            System.String                   name  // 0x18
  public            System.Collections.Generic.Dictionary<System.String,System.Object>data  // 0x20
METHODS:
  RVA=0x041E1670  token=0x600004A  System.Void .ctor()
  RVA=0x09DDB328  token=0x600004B  System.Threading.Tasks.Task <EventTrack>b__0()
END_CLASS

CLASS: <>c__DisplayClass43_0
TYPE:  sealed class
TOKEN: 0x2000013
SIZE:  0x28
FIELDS:
  public            Hypergryph.CloudClientBridge.EventLogWrapper<>4__this  // 0x10
  public            System.String                   name  // 0x18
  public            Newtonsoft.Json.Linq.JObject    properties  // 0x20
METHODS:
  RVA=0x041E1670  token=0x600004E  System.Void .ctor()
  RVA=0x09DDB3D8  token=0x600004F  System.Boolean <ExecuteEventTrackSafely>b__0()
END_CLASS

CLASS: <ExecuteEventTrackSafely>d__43
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x58
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>t__builder  // 0x18
  public            Hypergryph.CloudClientBridge.EventLogWrapper<>4__this  // 0x30
  public            System.String                   name  // 0x38
  public            System.Collections.Generic.Dictionary<System.String,System.Object>data  // 0x40
  private           Hypergryph.CloudClientBridge.EventLogWrapper.<>c__DisplayClass43_0<>8__1  // 0x48
  private           System.Runtime.CompilerServices.TaskAwaiter<System.Boolean><>u__1  // 0x50
METHODS:
  RVA=0x09DDA5A8  token=0x6000050  System.Void MoveNext()
  RVA=0x09DDABD4  token=0x6000051  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <>c__DisplayClass13_0
TYPE:  sealed class
TOKEN: 0x2000018
SIZE:  0x18
FIELDS:
  public            Hypergryph.CloudClientBridge.IEventReceiverreceiver  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000062  System.Void .ctor()
  RVA=0x09DDAF1C  token=0x6000063  System.Void <SetCallbacks>b__0(System.Byte[] data, System.Int32 size)
  RVA=0x09DDB088  token=0x6000064  System.Void <SetCallbacks>b__1(System.String key, System.Byte[] data, System.Int32 size)
  RVA=0x09DDB270  token=0x6000065  System.Void <SetCallbacks>b__2(System.Int32 width, System.Int32 height)
END_CLASS

CLASS: <>c__DisplayClass22_0
TYPE:  sealed class
TOKEN: 0x200001B
SIZE:  0x30
FIELDS:
  public            System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>>keyCallbacks  // 0x10
  public            Hypergryph.CloudClientBridge.HGCloudClientBridge<>4__this  // 0x18
  public            Hypergryph.CloudClientBridge.CloudCallResultresult  // 0x20
  public            System.String                   key  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600008A  System.Void .ctor()
  RVA=0x02D33780  token=0x600008B  System.Void <HandleCommandNotSupported>b__0()
END_CLASS

CLASS: <>c__DisplayClass23_0
TYPE:  sealed class
TOKEN: 0x200001C
SIZE:  0x30
FIELDS:
  public            Hypergryph.CloudClientBridge.HGCloudClientBridge<>4__this  // 0x10
  public            Hypergryph.CloudClientBridge.CloudCallResultresult  // 0x18
  public            System.String                   key  // 0x20
  public            System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>>callbacks  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600008C  System.Void .ctor()
  RVA=0x02D346B0  token=0x600008D  System.Void <HandleGlobalEvent>b__0()
END_CLASS

CLASS: <>c__DisplayClass26_0
TYPE:  sealed class
TOKEN: 0x200001D
SIZE:  0x30
FIELDS:
  public            System.Int32                    width  // 0x10
  public            System.Int32                    height  // 0x14
  public            System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>>callbacks  // 0x18
  public            Hypergryph.CloudClientBridge.HGCloudClientBridge<>4__this  // 0x20
  public            System.String                   key  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600008E  System.Void .ctor()
  RVA=0x02D355E0  token=0x600008F  System.Void <Hypergryph.CloudClientBridge.IEventReceiver.OnChangeResolutionEvent>b__0(Hypergryph.CloudClientBridge.CloudCallResult result)
END_CLASS

CLASS: <>c__DisplayClass26_1
TYPE:  sealed class
TOKEN: 0x200001E
SIZE:  0x20
FIELDS:
  public            Hypergryph.CloudClientBridge.CloudCallResultcallResult  // 0x10
  public            Hypergryph.CloudClientBridge.HGCloudClientBridge.<>c__DisplayClass26_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000090  System.Void .ctor()
  RVA=0x02D37A90  token=0x6000091  System.Void <Hypergryph.CloudClientBridge.IEventReceiver.OnChangeResolutionEvent>b__1()
END_CLASS

CLASS: <>c__DisplayClass26_2
TYPE:  sealed class
TOKEN: 0x200001F
SIZE:  0x20
FIELDS:
  public            Hypergryph.CloudClientBridge.CloudCallResultcallResult  // 0x10
  public            Hypergryph.CloudClientBridge.HGCloudClientBridge.<>c__DisplayClass26_0CS$<>8__locals2  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000092  System.Void .ctor()
  RVA=0x02D389E0  token=0x6000093  System.Void <Hypergryph.CloudClientBridge.IEventReceiver.OnChangeResolutionEvent>b__2()
END_CLASS

CLASS: <>c__DisplayClass27_0
TYPE:  sealed class
TOKEN: 0x2000020
SIZE:  0x30
FIELDS:
  public            Hypergryph.CloudClientBridge.HGCloudClientBridge<>4__this  // 0x10
  public            System.Action<Hypergryph.CloudClientBridge.CloudCallResult>callback  // 0x18
  public            Hypergryph.CloudClientBridge.CloudCallResultresult  // 0x20
  public            System.String                   key  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000094  System.Void .ctor()
  RVA=0x02D39930  token=0x6000095  System.Void <InvokeCallback>b__0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000021
SIZE:  0x10
FIELDS:
  public    static readonly Hypergryph.CloudClientBridge.HGCloudClientBridge.<>c<>9  // static @ 0x0
  public    static  System.Func<System.String,System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>>><>9__34_0  // static @ 0x8
METHODS:
  RVA=0x09DDB5FC  token=0x6000096  System.Void .cctor()
  RVA=0x041E1670  token=0x6000097  System.Void .ctor()
  RVA=0x02D331B0  token=0x6000098  System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>> <RegisterGlobalCallback>b__34_0(System.String _)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000024
SIZE:  0x18
FIELDS:
  public            System.String                   logInfo  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600009D  System.Void .ctor()
  RVA=0x04CC3670  token=0x600009E  System.Void <PrintLogToView>b__0()
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000026
SIZE:  0x18
FIELDS:
  public            System.Action                   action  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000A3  System.Void .ctor()
  RVA=0x06DFA030  token=0x60000A4  System.Void <RunOnMainThread>b__0(System.Object _)
END_CLASS

CLASS: <>c__DisplayClass5_0`1
TYPE:  sealed class
TOKEN: 0x2000027
FIELDS:
  public            System.Threading.Tasks.TaskCompletionSource<T>tcs  // 0x0
  public            System.Func<T>                  func  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000A5  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60000A6  System.Void <RunOnMainThread>b__0()
END_CLASS

CLASS: Hypergryph.CloudClientBridge.CloudClientManager
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  Hypergryph.CloudClientBridge.CloudClientManager_instance  // static @ 0x0
  private   static readonly System.Object                   _instanceLock  // static @ 0x8
  private           System.Boolean                  isPipeConnected  // 0x18
  private           System.Boolean                  isInitDone  // 0x19
METHODS:
  RVA=0x09DD21A4  token=0x600001B  Hypergryph.CloudClientBridge.HGCloudClientBridge GetBridge()
  RVA=0x03FCD5D0  token=0x600001C  System.Void AutoInitialize()
  RVA=0x09DD1E88  token=0x600001D  System.Void CreateInstance()
  RVA=0x09DD1E20  token=0x600001E  System.Void Awake()
  RVA=0x09DD2244  token=0x600001F  System.Void Start()
  RVA=0x09DD21E4  token=0x6000020  System.Void OnDestroy()
  RVA=0x05393520  token=0x6000021  System.Void .ctor()
  RVA=0x04D30850  token=0x6000022  System.Void .cctor()
END_CLASS

CLASS: Hypergryph.CloudClientBridge.CloudGameUtils
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
  private   static readonly System.Object                   _lock  // static @ 0x0
  private   static  System.String                   _gameVersion  // static @ 0x8
  private   static  System.String                   _launchId  // static @ 0x10
METHODS:
  RVA=0x040BC700  token=0x6000023  System.Boolean IsCloudGame()
  RVA=0x09DD23F8  token=0x6000024  System.Void InitGameVersionOnUI()
  RVA=0x09DD2358  token=0x6000025  System.String GetGameVersion()
  RVA=0x09DD2460  token=0x6000026  System.Void InitLaunchIdOnUI()
  RVA=0x09DD23A8  token=0x6000027  System.String GetLaunchId()
  RVA=0x04CC76D0  token=0x6000028  System.Void .cctor()
END_CLASS

CLASS: Hypergryph.CloudClientBridge.CloudCallResult
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x28
FIELDS:
  public            System.String                   data  // 0x10
  public            System.Int32                    code  // 0x18
  public            System.String                   message  // 0x20
METHODS:
  RVA=0x09DD1D64  token=0x6000029  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.CloudClientBridge.ClientPipeMessage
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x38
FIELDS:
  public            System.Boolean                  isGlobalEvent  // 0x10
  public            System.Int32                    eventId  // 0x14
  public            System.String                   key  // 0x18
  public            Newtonsoft.Json.Linq.JToken     data  // 0x20
  public            System.Int32                    code  // 0x28
  public            System.String                   message  // 0x30
METHODS:
  RVA=0x09DD1D04  token=0x600002A  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.CloudClientBridge.CloudGameGlobalEvent
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Hypergryph.CloudClientBridge.CloudGameGlobalEventRESOLUTON_CHANGE  // const
  public    static  Hypergryph.CloudClientBridge.CloudGameGlobalEventBULLETIN_EVENT  // const
  public    static  Hypergryph.CloudClientBridge.CloudGameGlobalEventBULLETIN_OPEN_EVENT  // const
  public    static  Hypergryph.CloudClientBridge.CloudGameGlobalEventWEBPORTAL_SCHEME_JUMP  // const
  public    static  Hypergryph.CloudClientBridge.CloudGameGlobalEventWEBPORTAL_OPEN_EVENT  // const
  public    static  Hypergryph.CloudClientBridge.CloudGameGlobalEventPIPELINE_CONNECTED  // const
  public    static  Hypergryph.CloudClientBridge.CloudGameGlobalEventGAMEPAD_CHANGE  // const
METHODS:
END_CLASS

CLASS: Hypergryph.CloudClientBridge.StringValueAttribute
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.String                   <Value>k__BackingField  // 0x10
PROPERTIES:
  Value  get=0x02B2ECC0
METHODS:
  RVA=0x053908C0  token=0x600002C  System.Void .ctor(System.String value)
END_CLASS

CLASS: Hypergryph.CloudClientBridge.EnumExtensions
TYPE:  static class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DD24EC  token=0x600002D  System.String GetStringValue(System.Enum value)
END_CLASS

CLASS: Hypergryph.CloudClientBridge.EventLogWrapper
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x48
FIELDS:
  private   static readonly Hypergryph.CloudClientBridge.EventLogWrapper_instance  // static @ 0x0
  private           System.String                   <AppId>k__BackingField  // 0x10
  private           System.String                   <Region>k__BackingField  // 0x18
  private           System.String                   <SdkVersion>k__BackingField  // 0x20
  private           System.String                   <GameVersion>k__BackingField  // 0x28
  private           System.String                   <LaunchId>k__BackingField  // 0x30
  private           System.Int64                    <Platform>k__BackingField  // 0x38
  private           System.Boolean                  <IsWeb>k__BackingField  // 0x40
PROPERTIES:
  AppId  get=0x02B2ECC0  set=0x053908C0
  Region  get=0x0385B100  set=0x05392C40
  SdkVersion  get=0x04D862C0
  GameVersion  get=0x04D86240  set=0x02FDB880
  LaunchId  get=0x011F36E0  set=0x05396200
  TsMs  get=0x09DD3078
  Platform  get=0x04D85A50  set=0x04D865B0
  IsWeb  get=0x02D480A0  set=0x02D480B0
  Instance  get=0x09DD3028
METHODS:
  RVA=0x09DD2778  token=0x6000038  System.Int64 GetCurrentTimeMs()
  RVA=0x09DD2F60  token=0x600003D  System.Void .ctor()
  RVA=0x09DD30B8  token=0x600003F  System.Void init()
  RVA=0x09DD2588  token=0x6000040  System.Void EventTrack(System.String name, System.Collections.Generic.Dictionary<System.String,System.Object> data)
  RVA=0x09DD2B1C  token=0x6000041  System.Void TrackPipelineInit(System.Boolean isSuccess, System.Int64 times, System.Int64 duration)
  RVA=0x09DD2CA4  token=0x6000042  System.Void TrackSendDataCall(System.String eventName, System.Int64 eventId, System.String data)
  RVA=0x09DD27CC  token=0x6000043  System.Void TrackClientEvent(System.String eventName, System.Int64 eventId, System.Int64 code, System.String message, System.Boolean isGlobal, System.String data)
  RVA=0x09DD29FC  token=0x6000044  System.Void TrackException(System.String methodName, System.String extra)
  RVA=0x09DD268C  token=0x6000045  System.Threading.Tasks.Task ExecuteEventTrackSafely(System.String name, System.Collections.Generic.Dictionary<System.String,System.Object> data)
  RVA=0x09DD2EF0  token=0x6000046  System.Void .cctor()
  RVA=0x09DD2E18  token=0x6000047  System.Boolean <init>b__37_0()
END_CLASS

CLASS: Hypergryph.CloudClientBridge.CloudGameEvent
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x30
FIELDS:
  public            System.Int32                    eventId  // 0x10
  public            System.String                   key  // 0x18
  public            System.String                   data  // 0x20
  public            System.Action<Hypergryph.CloudClientBridge.CloudCallResult>callback  // 0x28
METHODS:
  RVA=0x09DD2304  token=0x6000052  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.CloudClientBridge.IEventReceiver
TYPE:  interface
TOKEN: 0x2000016
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000053  System.Void OnReceiveData(System.String dataString)
  RVA=-1  // abstract  token=0x6000054  System.Void OnReceiveDataWithKey(System.String key, System.String dataString)
  RVA=-1  // abstract  token=0x6000055  System.Void OnChangeResolutionEvent(System.Int32 width, System.Int32 height)
END_CLASS

CLASS: Hypergryph.CloudClientBridge.EventQueueProcessor
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x50
FIELDS:
  private   readonly System.Collections.Generic.Queue<Hypergryph.CloudClientBridge.CloudGameEvent>eventQueue  // 0x10
  private   readonly System.Object                   queueLock  // 0x18
  private   readonly System.Collections.Generic.Queue<Hypergryph.CloudClientBridge.CloudGameEvent>pendingEventQueue  // 0x20
  private   readonly System.Object                   pendingQueueLock  // 0x28
  private   readonly System.Threading.AutoResetEvent autoQueueEvent  // 0x30
  private           System.Threading.Thread         processThread  // 0x38
  private           System.Boolean                  isThreadRunning  // 0x40
  private           System.Boolean                  isPipeConnected  // 0x41
  private           System.Boolean                  isInitDone  // 0x42
  private           System.Int32                    initTryTimes  // 0x44
  private   readonly Hypergryph.CloudClientBridge.IEventReceivereventReceiver  // 0x48
METHODS:
  RVA=0x09DD5400  token=0x6000056  System.Void .ctor(Hypergryph.CloudClientBridge.IEventReceiver eventReceiver)
  RVA=0x09DD4C2C  token=0x6000057  System.Void SetCallbacks()
  RVA=0x09DD4DEC  token=0x6000058  System.Void Start()
  RVA=0x09DD4F20  token=0x6000059  System.Void Stop()
  RVA=0x09DD3734  token=0x600005A  System.Void EnqueueEvent(Hypergryph.CloudClientBridge.CloudGameEvent cloudEvent)
  RVA=0x09DD3EDC  token=0x600005B  System.Void ProcessEventQueue()
  RVA=0x09DD3D58  token=0x600005C  System.Void InitPipeLine()
  RVA=0x09DD3460  token=0x600005D  System.Void DoInitPipeLine()
  RVA=0x09DD46C4  token=0x600005E  System.Void ReportException(System.String methodName, System.Exception ex, Newtonsoft.Json.Linq.JObject extraInfo)
  RVA=0x09DD4998  token=0x600005F  System.Void SendPendingEvents()
  RVA=0x09DD3158  token=0x6000060  System.Void CleanUp()
  RVA=0x09DD3908  token=0x6000061  System.String GetErrorResultJson(System.String key, System.Int32 eventId, System.Int32 code, System.String message, Newtonsoft.Json.Linq.JObject data)
END_CLASS

CLASS: Hypergryph.CloudClientBridge.FocusMessageSender
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           TMPro.TMP_InputField            tmpInput  // 0x18
  private   static  System.UInt32                   WM_USER_FOCUS_GAINED  // const
  private   static  System.UInt32                   WM_USER_FOCUS_LOST  // const
  private           System.IntPtr                   mainWindowHandle  // 0x20
METHODS:
  RVA=0x09DD5AAC  token=0x6000066  System.IntPtr SendMessage(System.IntPtr hWnd, System.UInt32 Msg, System.IntPtr wParam, System.IntPtr lParam)
  RVA=0x09DD5B4C  token=0x6000067  System.Void SendWindowsMessage(System.IntPtr targetWindow, System.UInt32 message, System.String text)
  RVA=0x09DD555C  token=0x6000068  System.IntPtr FindWindow(System.String className, System.String windowName)
  RVA=0x09DD55F0  token=0x6000069  System.IntPtr GetActiveWindow()
  RVA=0x09DD5618  token=0x600006A  System.IntPtr GetForegroundWindow()
  RVA=0x09DD5750  token=0x600006B  System.Int32 GetWindowText(System.IntPtr hWnd, System.Text.StringBuilder lpString, System.Int32 nMaxCount)
  RVA=0x09DD5810  token=0x600006C  System.Boolean IsWindow(System.IntPtr hWnd)
  RVA=0x09DD5640  token=0x600006D  System.IntPtr GetMainWindowHandle()
  RVA=0x09DD5E40  token=0x600006E  System.Void Start()
  RVA=0x09DD5844  token=0x600006F  System.Void OnFocusGained(System.String text)
  RVA=0x09DD5970  token=0x6000070  System.Void OnFocusLost(System.String text)
  RVA=0x05393520  token=0x6000071  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.CloudClientBridge.HGCloudClientBridge
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x50
IMPLEMENTS: Hypergryph.CloudClientBridge.IEventReceiver
FIELDS:
  private   static  Hypergryph.CloudClientBridge.HGCloudClientBridge_instance  // static @ 0x0
  private   static readonly System.Object                   _lock  // static @ 0x8
  private           System.Boolean                  isInitialized  // 0x10
  private           Hypergryph.CloudClientBridge.EventQueueProcessoreventQueueProcessor  // 0x18
  private           System.Boolean                  isPipeConnected  // 0x20
  private           System.Boolean                  isInitDone  // 0x21
  private           System.Int32                    nextEventIndex  // 0x24
  private   readonly System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>>_eventCallbacks  // 0x28
  private   readonly System.Collections.Concurrent.ConcurrentDictionary<System.String,System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>>>_globalCallbacks  // 0x30
  private   readonly System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Tuple<System.String,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>>>_globalEventIdMap  // 0x38
  private           Hypergryph.CloudClientBridge.CloudCallResult_cacheLaunchInfo  // 0x40
  private           System.Int32                    _cachedScreenWidth  // 0x48
  private           System.Int32                    _cachedScreenHeight  // 0x4c
PROPERTIES:
  Instance  get=0x09DD98B4
  ExistingInstance  get=0x09DD97B4
METHODS:
  RVA=0x09DD9690  token=0x6000074  System.Void .ctor()
  RVA=0x09DD7A1C  token=0x6000075  System.Void InitOnUI()
  RVA=0x09DD79C0  token=0x6000076  System.Void Hypergryph.CloudClientBridge.IEventReceiver.OnReceiveData(System.String dataString)
  RVA=0x09DD7704  token=0x6000077  System.Void Hypergryph.CloudClientBridge.IEventReceiver.OnReceiveDataWithKey(System.String key, System.String dataString)
  RVA=0x09DD66AC  token=0x6000078  System.Void HandleCommandNotSupported(Hypergryph.CloudClientBridge.ClientPipeMessage pipeMessage)
  RVA=0x09DD6A9C  token=0x6000079  System.Void HandleGlobalEvent(Hypergryph.CloudClientBridge.ClientPipeMessage pipeMessage)
  RVA=0x09DD6E98  token=0x600007A  System.Void HandleNormalEvent(Hypergryph.CloudClientBridge.ClientPipeMessage pipeMessage)
  RVA=0x09DD9208  token=0x600007B  System.Void UpdateEventLogPlatformInfo(Newtonsoft.Json.Linq.JToken data)
  RVA=0x09DD71D4  token=0x600007C  System.Void Hypergryph.CloudClientBridge.IEventReceiver.OnChangeResolutionEvent(System.Int32 width, System.Int32 height)
  RVA=0x09DD7DE4  token=0x600007D  System.Void InvokeCallback(System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback, System.String key, System.Int32 code, System.String message, System.String data, System.Boolean isGlobalEvent)
  RVA=0x09DD7C20  token=0x600007E  System.Void InvokeCallbackDirect(System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback, Hypergryph.CloudClientBridge.CloudCallResult result, System.String key)
  RVA=0x09DD7F8C  token=0x600007F  Hypergryph.CloudClientBridge.ClientPipeMessage ParseClientPipeMessage(System.String jsonString)
  RVA=0x09DD8C08  token=0x6000080  System.Void TrackClientEventSafe(Hypergryph.CloudClientBridge.ClientPipeMessage pipeMessage, System.String originalData)
  RVA=0x09DD85F8  token=0x6000081  System.Void ReportException(System.String methodName, System.Exception ex, Newtonsoft.Json.Linq.JObject extraInfo)
  RVA=0x09DD88CC  token=0x6000082  System.Void SendDataWithKey(System.String key, System.String data, System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback)
  RVA=0x09DD5FD4  token=0x6000083  System.String BuildCloudEventData(System.Int32 eventId, System.String key, System.String userData)
  RVA=0x09DD81E8  token=0x6000084  System.Int32 RegisterGlobalCallback(Hypergryph.CloudClientBridge.CloudGameGlobalEvent globalEvent, System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback)
  RVA=0x02D315F0  token=0x6000085  System.Void DispatchCachedGlobalEventOnRegister(Hypergryph.CloudClientBridge.CloudGameGlobalEvent globalEvent, System.String key, System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback)
  RVA=0x09DD8DB8  token=0x6000086  System.Void UnRegisterGlobalCallback(Hypergryph.CloudClientBridge.CloudGameGlobalEvent globalEvent, System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback)
  RVA=0x09DD6368  token=0x6000087  System.Void Cleanup()
  RVA=0x09DD961C  token=0x6000088  System.Void .cctor()
  RVA=0x02D31980  token=0x6000089  System.Void <Cleanup>b__37_0()
END_CLASS

CLASS: Hypergryph.CloudClientBridge.CloudClientBridgeVersion
TYPE:  static class
TOKEN: 0x2000022
SIZE:  0x10
FIELDS:
  public    static readonly System.String                   VERSION  // static @ 0x0
METHODS:
  RVA=0x09DD1DC4  token=0x6000099  System.Void .cctor()
END_CLASS

CLASS: Hypergryph.CloudClientBridge.Logger
TYPE:  class
TOKEN: 0x2000023
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03FCD680  token=0x600009A  System.Void Debug(System.String message)
  RVA=0x09DD9A7C  token=0x600009B  System.Void Error(System.String message)
  RVA=0x03FCD710  token=0x600009C  System.Void PrintLogToView(System.String logInfo)
END_CLASS

CLASS: Hypergryph.CloudClientBridge.UnityMainThread
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x10
FIELDS:
  private   static  System.Threading.SynchronizationContextMainContext  // static @ 0x0
  public    static  System.Boolean                  isDebug  // static @ 0x8
  private   static readonly System.Object                   initLock  // static @ 0x10
METHODS:
  RVA=0x03FCE180  token=0x600009F  System.Void InitOnUI()
  RVA=0x03FCD7E0  token=0x60000A0  System.Void RunOnMainThread(System.Action action)
  RVA=-1  // generic def  token=0x60000A1  System.Threading.Tasks.Task<T> RunOnMainThread(System.Func<T> func)
  RVA=0x04D21660  token=0x60000A2  System.Void .cctor()
END_CLASS

CLASS: WeLing.SDK.MonoPInvokeCallbackAttribute
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: WeLing.SDK.PipeWrapper
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x10
FIELDS:
  public    static  System.String                   version  // static @ 0x0
  public    static  System.Action<System.Byte[],System.Int32>OnReceiveDataEvent  // static @ 0x8
  public    static  System.Action<System.String,System.Byte[],System.Int32>OnReceiveDataWithKeyEvent  // static @ 0x10
  public    static  System.Action<System.String,System.Byte[],System.Int32,System.String>OnReceiveDataWithKeyExEvent  // static @ 0x18
  public    static  System.Action<System.Int32,System.Int32>OnChangeResolutionEvent  // static @ 0x20
METHODS:
  RVA=0x09DDA18C  token=0x6000002  System.Boolean _InitEx()
  RVA=0x09DDA1FC  token=0x6000003  System.Boolean _Release()
  RVA=0x09DDA328  token=0x6000004  System.Boolean _SendData(System.Byte[] data, System.Int32 size)
  RVA=0x09DDA26C  token=0x6000005  System.Boolean _SendDataWithKey(System.String key, System.Byte[] data, System.Int32 size)
  RVA=0x09DDA508  token=0x6000006  System.Boolean _SetOnRecieveDataCallback(WeLing.SDK.PipeWrapper._OnReceiveData cb)
  RVA=0x09DDA440  token=0x6000007  System.Boolean _SetOnDataWithKeyCallback(WeLing.SDK.PipeWrapper._OnReceiveDataWithKey cb)
  RVA=0x09DDA484  token=0x6000008  System.Boolean _SetOnDataWithKeyExCallback(WeLing.SDK.PipeWrapper._OnReceiveDataWithKeyEx cb)
  RVA=0x09DDA3BC  token=0x6000009  System.Boolean _SetOnChangeResolutionCallback(WeLing.SDK.PipeWrapper._OnChangeResolution cb)
  RVA=0x09DD9D98  token=0x600000A  System.Void SimpleCallBackImp(System.IntPtr data, System.Int32& size)
  RVA=0x09DD9F6C  token=0x600000B  System.Void WithKeyCallBackImp(System.String key, System.IntPtr data, System.Int32& size)
  RVA=0x09DDA074  token=0x600000C  System.Void WithKeyExCallBackImp(System.String key, System.IntPtr data, System.Int32& size, System.String userID)
  RVA=0x09DD9AE8  token=0x600000D  System.Void ChangeResolutionCallBackImp(System.Int32 width, System.Int32 height)
  RVA=0x09DD9B70  token=0x600000E  System.Boolean Init()
  RVA=0x09DD9E8C  token=0x600000F  System.Void Uninit()
  RVA=0x09DD9D40  token=0x6000010  System.Boolean SendData(System.Byte[] data, System.Int32 size)
  RVA=0x09DD9CE0  token=0x6000011  System.Boolean SendDataWithKey(System.String key, System.Byte[] data, System.Int32 size)
  RVA=0x09DDA54C  token=0x6000012  System.Void .cctor()
END_CLASS

