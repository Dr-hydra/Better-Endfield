// ========================================================
// Dumped by @desirepro
// Assembly: Hypergryph.CloudClientBridge.dll
// Classes:  39
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000004  // size: 0x80
    public sealed class _OnReceiveData : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x09DDBB98  token: 0x6000013
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x6000014
        public virtual System.Void Invoke(System.IntPtr data, System.Int32& size) { }

    }

    // TypeToken: 0x2000005  // size: 0x80
    public sealed class _OnReceiveDataWithKey : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x09DDBAC4  token: 0x6000015
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x01546280  token: 0x6000016
        public virtual System.Void Invoke(System.String key, System.IntPtr data, System.Int32& size) { }

    }

    // TypeToken: 0x2000006  // size: 0x80
    public sealed class _OnReceiveDataWithKeyEx : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x09DDB9F0  token: 0x6000017
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015547C0  token: 0x6000018
        public virtual System.Void Invoke(System.String key, System.IntPtr data, System.Int32& size, System.String userID) { }

    }

    // TypeToken: 0x2000007  // size: 0x80
    public sealed class _OnChangeResolution : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x09836370  token: 0x6000019
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x600001A
        public virtual System.Void Invoke(System.Int32 width, System.Int32 height) { }

    }

    // TypeToken: 0x2000010  // size: 0x48
    public sealed struct <init>d__37 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncVoidMethodBuilder <>t__builder;  // 0x18
        public Hypergryph.CloudClientBridge.EventLogWrapper <>4__this;  // 0x38
        private System.Runtime.CompilerServices.TaskAwaiter<System.Boolean> <>u__1;  // 0x40

        // Methods
        // RVA: 0x09DDB660  token: 0x6000048
        private virtual System.Void MoveNext() { }
        // RVA: 0x053982D4  token: 0x6000049
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000012  // size: 0x40
    public sealed struct <<EventTrack>b__0>d : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public Hypergryph.CloudClientBridge.EventLogWrapper.<>c__DisplayClass38_0 <>4__this;  // 0x30
        private System.Runtime.CompilerServices.TaskAwaiter <>u__1;  // 0x38

        // Methods
        // RVA: 0x09DDAC30  token: 0x600004C
        private virtual System.Void MoveNext() { }
        // RVA: 0x09DDAEC0  token: 0x600004D
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000011  // size: 0x28
    public sealed class <>c__DisplayClass38_0
    {
        // Fields
        public Hypergryph.CloudClientBridge.EventLogWrapper <>4__this;  // 0x10
        public System.String name;  // 0x18
        public System.Collections.Generic.Dictionary<System.String,System.Object> data;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x600004A
        public System.Void .ctor() { }
        // RVA: 0x09DDB328  token: 0x600004B
        private System.Threading.Tasks.Task <EventTrack>b__0() { }

    }

    // TypeToken: 0x2000013  // size: 0x28
    public sealed class <>c__DisplayClass43_0
    {
        // Fields
        public Hypergryph.CloudClientBridge.EventLogWrapper <>4__this;  // 0x10
        public System.String name;  // 0x18
        public Newtonsoft.Json.Linq.JObject properties;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x600004E
        public System.Void .ctor() { }
        // RVA: 0x09DDB3D8  token: 0x600004F
        private System.Boolean <ExecuteEventTrackSafely>b__0() { }

    }

    // TypeToken: 0x2000014  // size: 0x58
    public sealed struct <ExecuteEventTrackSafely>d__43 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public Hypergryph.CloudClientBridge.EventLogWrapper <>4__this;  // 0x30
        public System.String name;  // 0x38
        public System.Collections.Generic.Dictionary<System.String,System.Object> data;  // 0x40
        private Hypergryph.CloudClientBridge.EventLogWrapper.<>c__DisplayClass43_0 <>8__1;  // 0x48
        private System.Runtime.CompilerServices.TaskAwaiter<System.Boolean> <>u__1;  // 0x50

        // Methods
        // RVA: 0x09DDA5A8  token: 0x6000050
        private virtual System.Void MoveNext() { }
        // RVA: 0x09DDABD4  token: 0x6000051
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000018  // size: 0x18
    public sealed class <>c__DisplayClass13_0
    {
        // Fields
        public Hypergryph.CloudClientBridge.IEventReceiver receiver;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000062
        public System.Void .ctor() { }
        // RVA: 0x09DDAF1C  token: 0x6000063
        private System.Void <SetCallbacks>b__0(System.Byte[] data, System.Int32 size) { }
        // RVA: 0x09DDB088  token: 0x6000064
        private System.Void <SetCallbacks>b__1(System.String key, System.Byte[] data, System.Int32 size) { }
        // RVA: 0x09DDB270  token: 0x6000065
        private System.Void <SetCallbacks>b__2(System.Int32 width, System.Int32 height) { }

    }

    // TypeToken: 0x200001B  // size: 0x30
    public sealed class <>c__DisplayClass22_0
    {
        // Fields
        public System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>> keyCallbacks;  // 0x10
        public Hypergryph.CloudClientBridge.HGCloudClientBridge <>4__this;  // 0x18
        public Hypergryph.CloudClientBridge.CloudCallResult result;  // 0x20
        public System.String key;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600008A
        public System.Void .ctor() { }
        // RVA: 0x02D33780  token: 0x600008B
        private System.Void <HandleCommandNotSupported>b__0() { }

    }

    // TypeToken: 0x200001C  // size: 0x30
    public sealed class <>c__DisplayClass23_0
    {
        // Fields
        public Hypergryph.CloudClientBridge.HGCloudClientBridge <>4__this;  // 0x10
        public Hypergryph.CloudClientBridge.CloudCallResult result;  // 0x18
        public System.String key;  // 0x20
        public System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>> callbacks;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600008C
        public System.Void .ctor() { }
        // RVA: 0x02D346B0  token: 0x600008D
        private System.Void <HandleGlobalEvent>b__0() { }

    }

    // TypeToken: 0x200001D  // size: 0x30
    public sealed class <>c__DisplayClass26_0
    {
        // Fields
        public System.Int32 width;  // 0x10
        public System.Int32 height;  // 0x14
        public System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>> callbacks;  // 0x18
        public Hypergryph.CloudClientBridge.HGCloudClientBridge <>4__this;  // 0x20
        public System.String key;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600008E
        public System.Void .ctor() { }
        // RVA: 0x02D355E0  token: 0x600008F
        private System.Void <Hypergryph.CloudClientBridge.IEventReceiver.OnChangeResolutionEvent>b__0(Hypergryph.CloudClientBridge.CloudCallResult result) { }

    }

    // TypeToken: 0x200001E  // size: 0x20
    public sealed class <>c__DisplayClass26_1
    {
        // Fields
        public Hypergryph.CloudClientBridge.CloudCallResult callResult;  // 0x10
        public Hypergryph.CloudClientBridge.HGCloudClientBridge.<>c__DisplayClass26_0 CS$<>8__locals1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000090
        public System.Void .ctor() { }
        // RVA: 0x02D37A90  token: 0x6000091
        private System.Void <Hypergryph.CloudClientBridge.IEventReceiver.OnChangeResolutionEvent>b__1() { }

    }

    // TypeToken: 0x200001F  // size: 0x20
    public sealed class <>c__DisplayClass26_2
    {
        // Fields
        public Hypergryph.CloudClientBridge.CloudCallResult callResult;  // 0x10
        public Hypergryph.CloudClientBridge.HGCloudClientBridge.<>c__DisplayClass26_0 CS$<>8__locals2;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000092
        public System.Void .ctor() { }
        // RVA: 0x02D389E0  token: 0x6000093
        private System.Void <Hypergryph.CloudClientBridge.IEventReceiver.OnChangeResolutionEvent>b__2() { }

    }

    // TypeToken: 0x2000020  // size: 0x30
    public sealed class <>c__DisplayClass27_0
    {
        // Fields
        public Hypergryph.CloudClientBridge.HGCloudClientBridge <>4__this;  // 0x10
        public System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback;  // 0x18
        public Hypergryph.CloudClientBridge.CloudCallResult result;  // 0x20
        public System.String key;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000094
        public System.Void .ctor() { }
        // RVA: 0x02D39930  token: 0x6000095
        private System.Void <InvokeCallback>b__0() { }

    }

    // TypeToken: 0x2000021  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Hypergryph.CloudClientBridge.HGCloudClientBridge.<>c <>9;  // static @ 0x0
        public static System.Func<System.String,System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>>> <>9__34_0;  // static @ 0x8

        // Methods
        // RVA: 0x09DDB5FC  token: 0x6000096
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000097
        public System.Void .ctor() { }
        // RVA: 0x02D331B0  token: 0x6000098
        private System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>> <RegisterGlobalCallback>b__34_0(System.String _) { }

    }

    // TypeToken: 0x2000024  // size: 0x18
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.String logInfo;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600009D
        public System.Void .ctor() { }
        // RVA: 0x04CC3670  token: 0x600009E
        private System.Void <PrintLogToView>b__0() { }

    }

    // TypeToken: 0x2000026  // size: 0x18
    public sealed class <>c__DisplayClass4_0
    {
        // Fields
        public System.Action action;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000A3
        public System.Void .ctor() { }
        // RVA: 0x06DFA030  token: 0x60000A4
        private System.Void <RunOnMainThread>b__0(System.Object _) { }

    }

    // TypeToken: 0x2000027
    public sealed class <>c__DisplayClass5_0`1
    {
        // Fields
        public System.Threading.Tasks.TaskCompletionSource<T> tcs;  // 0x0
        public System.Func<T> func;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000A5
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60000A6
        private System.Void <RunOnMainThread>b__0() { }

    }

namespace Hypergryph.CloudClientBridge
{

    // TypeToken: 0x2000008  // size: 0x20
    public class CloudClientManager : UnityEngine.MonoBehaviour
    {
        // Fields
        private static Hypergryph.CloudClientBridge.CloudClientManager _instance;  // static @ 0x0
        private static readonly System.Object _instanceLock;  // static @ 0x8
        private System.Boolean isPipeConnected;  // 0x18
        private System.Boolean isInitDone;  // 0x19

        // Methods
        // RVA: 0x09DD21A4  token: 0x600001B
        public static Hypergryph.CloudClientBridge.HGCloudClientBridge GetBridge() { }
        // RVA: 0x03FCD5D0  token: 0x600001C
        private static System.Void AutoInitialize() { }
        // RVA: 0x09DD1E88  token: 0x600001D
        private static System.Void CreateInstance() { }
        // RVA: 0x09DD1E20  token: 0x600001E
        private System.Void Awake() { }
        // RVA: 0x09DD2244  token: 0x600001F
        private System.Void Start() { }
        // RVA: 0x09DD21E4  token: 0x6000020
        private System.Void OnDestroy() { }
        // RVA: 0x05393520  token: 0x6000021
        public System.Void .ctor() { }
        // RVA: 0x04D30850  token: 0x6000022
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x10
    public class CloudGameUtils
    {
        // Fields
        private static readonly System.Object _lock;  // static @ 0x0
        private static System.String _gameVersion;  // static @ 0x8
        private static System.String _launchId;  // static @ 0x10

        // Methods
        // RVA: 0x040BC700  token: 0x6000023
        public static System.Boolean IsCloudGame() { }
        // RVA: 0x09DD23F8  token: 0x6000024
        public static System.Void InitGameVersionOnUI() { }
        // RVA: 0x09DD2358  token: 0x6000025
        public static System.String GetGameVersion() { }
        // RVA: 0x09DD2460  token: 0x6000026
        public static System.Void InitLaunchIdOnUI() { }
        // RVA: 0x09DD23A8  token: 0x6000027
        public static System.String GetLaunchId() { }
        // RVA: 0x04CC76D0  token: 0x6000028
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x28
    public class CloudCallResult
    {
        // Fields
        public System.String data;  // 0x10
        public System.Int32 code;  // 0x18
        public System.String message;  // 0x20

        // Methods
        // RVA: 0x09DD1D64  token: 0x6000029
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x38
    public class ClientPipeMessage
    {
        // Fields
        public System.Boolean isGlobalEvent;  // 0x10
        public System.Int32 eventId;  // 0x14
        public System.String key;  // 0x18
        public Newtonsoft.Json.Linq.JToken data;  // 0x20
        public System.Int32 code;  // 0x28
        public System.String message;  // 0x30

        // Methods
        // RVA: 0x09DD1D04  token: 0x600002A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x14
    public sealed struct CloudGameGlobalEvent
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Hypergryph.CloudClientBridge.CloudGameGlobalEvent RESOLUTON_CHANGE;  // const
        public static Hypergryph.CloudClientBridge.CloudGameGlobalEvent BULLETIN_EVENT;  // const
        public static Hypergryph.CloudClientBridge.CloudGameGlobalEvent BULLETIN_OPEN_EVENT;  // const
        public static Hypergryph.CloudClientBridge.CloudGameGlobalEvent WEBPORTAL_SCHEME_JUMP;  // const
        public static Hypergryph.CloudClientBridge.CloudGameGlobalEvent WEBPORTAL_OPEN_EVENT;  // const
        public static Hypergryph.CloudClientBridge.CloudGameGlobalEvent PIPELINE_CONNECTED;  // const
        public static Hypergryph.CloudClientBridge.CloudGameGlobalEvent GAMEPAD_CHANGE;  // const

    }

    // TypeToken: 0x200000D  // size: 0x18
    public class StringValueAttribute : System.Attribute
    {
        // Fields
        private readonly System.String <Value>k__BackingField;  // 0x10

        // Properties
        System.String Value { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600002C
        public System.Void .ctor(System.String value) { }

    }

    // TypeToken: 0x200000E  // size: 0x10
    public static class EnumExtensions
    {
        // Methods
        // RVA: 0x09DD24EC  token: 0x600002D
        public static System.String GetStringValue(System.Enum value) { }

    }

    // TypeToken: 0x200000F  // size: 0x48
    public class EventLogWrapper
    {
        // Fields
        private static readonly Hypergryph.CloudClientBridge.EventLogWrapper _instance;  // static @ 0x0
        private System.String <AppId>k__BackingField;  // 0x10
        private System.String <Region>k__BackingField;  // 0x18
        private System.String <SdkVersion>k__BackingField;  // 0x20
        private System.String <GameVersion>k__BackingField;  // 0x28
        private System.String <LaunchId>k__BackingField;  // 0x30
        private System.Int64 <Platform>k__BackingField;  // 0x38
        private System.Boolean <IsWeb>k__BackingField;  // 0x40

        // Properties
        System.String AppId { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String Region { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.String SdkVersion { get; /* RVA: 0x04D862C0 */ }
        System.String GameVersion { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        System.String LaunchId { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        System.Int64 TsMs { get; /* RVA: 0x09DD3078 */ }
        System.Int64 Platform { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x04D865B0 */ }
        System.Boolean IsWeb { get; /* RVA: 0x02D480A0 */ set; /* RVA: 0x02D480B0 */ }
        Hypergryph.CloudClientBridge.EventLogWrapper Instance { get; /* RVA: 0x09DD3028 */ }

        // Methods
        // RVA: 0x09DD2778  token: 0x6000038
        public static System.Int64 GetCurrentTimeMs() { }
        // RVA: 0x09DD2F60  token: 0x600003D
        private System.Void .ctor() { }
        // RVA: 0x09DD30B8  token: 0x600003F
        public System.Void init() { }
        // RVA: 0x09DD2588  token: 0x6000040
        public System.Void EventTrack(System.String name, System.Collections.Generic.Dictionary<System.String,System.Object> data) { }
        // RVA: 0x09DD2B1C  token: 0x6000041
        public System.Void TrackPipelineInit(System.Boolean isSuccess, System.Int64 times, System.Int64 duration) { }
        // RVA: 0x09DD2CA4  token: 0x6000042
        public System.Void TrackSendDataCall(System.String eventName, System.Int64 eventId, System.String data) { }
        // RVA: 0x09DD27CC  token: 0x6000043
        public System.Void TrackClientEvent(System.String eventName, System.Int64 eventId, System.Int64 code, System.String message, System.Boolean isGlobal, System.String data) { }
        // RVA: 0x09DD29FC  token: 0x6000044
        public System.Void TrackException(System.String methodName, System.String extra) { }
        // RVA: 0x09DD268C  token: 0x6000045
        private System.Threading.Tasks.Task ExecuteEventTrackSafely(System.String name, System.Collections.Generic.Dictionary<System.String,System.Object> data) { }
        // RVA: 0x09DD2EF0  token: 0x6000046
        private static System.Void .cctor() { }
        // RVA: 0x09DD2E18  token: 0x6000047
        private System.Boolean <init>b__37_0() { }

    }

    // TypeToken: 0x2000015  // size: 0x30
    public class CloudGameEvent
    {
        // Fields
        public System.Int32 eventId;  // 0x10
        public System.String key;  // 0x18
        public System.String data;  // 0x20
        public System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback;  // 0x28

        // Methods
        // RVA: 0x09DD2304  token: 0x6000052
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000016
    public interface IEventReceiver
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000053
        public virtual System.Void OnReceiveData(System.String dataString) { }
        // RVA: -1  // abstract  token: 0x6000054
        public virtual System.Void OnReceiveDataWithKey(System.String key, System.String dataString) { }
        // RVA: -1  // abstract  token: 0x6000055
        public virtual System.Void OnChangeResolutionEvent(System.Int32 width, System.Int32 height) { }

    }

    // TypeToken: 0x2000017  // size: 0x50
    public class EventQueueProcessor
    {
        // Fields
        private readonly System.Collections.Generic.Queue<Hypergryph.CloudClientBridge.CloudGameEvent> eventQueue;  // 0x10
        private readonly System.Object queueLock;  // 0x18
        private readonly System.Collections.Generic.Queue<Hypergryph.CloudClientBridge.CloudGameEvent> pendingEventQueue;  // 0x20
        private readonly System.Object pendingQueueLock;  // 0x28
        private readonly System.Threading.AutoResetEvent autoQueueEvent;  // 0x30
        private System.Threading.Thread processThread;  // 0x38
        private System.Boolean isThreadRunning;  // 0x40
        private System.Boolean isPipeConnected;  // 0x41
        private System.Boolean isInitDone;  // 0x42
        private System.Int32 initTryTimes;  // 0x44
        private readonly Hypergryph.CloudClientBridge.IEventReceiver eventReceiver;  // 0x48

        // Methods
        // RVA: 0x09DD5400  token: 0x6000056
        public System.Void .ctor(Hypergryph.CloudClientBridge.IEventReceiver eventReceiver) { }
        // RVA: 0x09DD4C2C  token: 0x6000057
        private System.Void SetCallbacks() { }
        // RVA: 0x09DD4DEC  token: 0x6000058
        public System.Void Start() { }
        // RVA: 0x09DD4F20  token: 0x6000059
        public System.Void Stop() { }
        // RVA: 0x09DD3734  token: 0x600005A
        public System.Void EnqueueEvent(Hypergryph.CloudClientBridge.CloudGameEvent cloudEvent) { }
        // RVA: 0x09DD3EDC  token: 0x600005B
        private System.Void ProcessEventQueue() { }
        // RVA: 0x09DD3D58  token: 0x600005C
        private System.Void InitPipeLine() { }
        // RVA: 0x09DD3460  token: 0x600005D
        private System.Void DoInitPipeLine() { }
        // RVA: 0x09DD46C4  token: 0x600005E
        private static System.Void ReportException(System.String methodName, System.Exception ex, Newtonsoft.Json.Linq.JObject extraInfo) { }
        // RVA: 0x09DD4998  token: 0x600005F
        private System.Void SendPendingEvents() { }
        // RVA: 0x09DD3158  token: 0x6000060
        private System.Void CleanUp() { }
        // RVA: 0x09DD3908  token: 0x6000061
        private System.String GetErrorResultJson(System.String key, System.Int32 eventId, System.Int32 code, System.String message, Newtonsoft.Json.Linq.JObject data) { }

    }

    // TypeToken: 0x2000019  // size: 0x28
    public class FocusMessageSender : UnityEngine.MonoBehaviour
    {
        // Fields
        private TMPro.TMP_InputField tmpInput;  // 0x18
        private static System.UInt32 WM_USER_FOCUS_GAINED;  // const
        private static System.UInt32 WM_USER_FOCUS_LOST;  // const
        private System.IntPtr mainWindowHandle;  // 0x20

        // Methods
        // RVA: 0x09DD5AAC  token: 0x6000066
        private static System.IntPtr SendMessage(System.IntPtr hWnd, System.UInt32 Msg, System.IntPtr wParam, System.IntPtr lParam) { }
        // RVA: 0x09DD5B4C  token: 0x6000067
        private System.Void SendWindowsMessage(System.IntPtr targetWindow, System.UInt32 message, System.String text) { }
        // RVA: 0x09DD555C  token: 0x6000068
        private static System.IntPtr FindWindow(System.String className, System.String windowName) { }
        // RVA: 0x09DD55F0  token: 0x6000069
        private static System.IntPtr GetActiveWindow() { }
        // RVA: 0x09DD5618  token: 0x600006A
        private static System.IntPtr GetForegroundWindow() { }
        // RVA: 0x09DD5750  token: 0x600006B
        private static System.Int32 GetWindowText(System.IntPtr hWnd, System.Text.StringBuilder lpString, System.Int32 nMaxCount) { }
        // RVA: 0x09DD5810  token: 0x600006C
        private static System.Boolean IsWindow(System.IntPtr hWnd) { }
        // RVA: 0x09DD5640  token: 0x600006D
        private System.IntPtr GetMainWindowHandle() { }
        // RVA: 0x09DD5E40  token: 0x600006E
        private System.Void Start() { }
        // RVA: 0x09DD5844  token: 0x600006F
        private System.Void OnFocusGained(System.String text) { }
        // RVA: 0x09DD5970  token: 0x6000070
        private System.Void OnFocusLost(System.String text) { }
        // RVA: 0x05393520  token: 0x6000071
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x50
    public class HGCloudClientBridge : Hypergryph.CloudClientBridge.IEventReceiver
    {
        // Fields
        private static Hypergryph.CloudClientBridge.HGCloudClientBridge _instance;  // static @ 0x0
        private static readonly System.Object _lock;  // static @ 0x8
        private System.Boolean isInitialized;  // 0x10
        private Hypergryph.CloudClientBridge.EventQueueProcessor eventQueueProcessor;  // 0x18
        private System.Boolean isPipeConnected;  // 0x20
        private System.Boolean isInitDone;  // 0x21
        private System.Int32 nextEventIndex;  // 0x24
        private readonly System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>> _eventCallbacks;  // 0x28
        private readonly System.Collections.Concurrent.ConcurrentDictionary<System.String,System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>>> _globalCallbacks;  // 0x30
        private readonly System.Collections.Concurrent.ConcurrentDictionary<System.Int32,System.Tuple<System.String,System.Action<Hypergryph.CloudClientBridge.CloudCallResult>>> _globalEventIdMap;  // 0x38
        private Hypergryph.CloudClientBridge.CloudCallResult _cacheLaunchInfo;  // 0x40
        private System.Int32 _cachedScreenWidth;  // 0x48
        private System.Int32 _cachedScreenHeight;  // 0x4c

        // Properties
        Hypergryph.CloudClientBridge.HGCloudClientBridge Instance { get; /* RVA: 0x09DD98B4 */ }
        Hypergryph.CloudClientBridge.HGCloudClientBridge ExistingInstance { get; /* RVA: 0x09DD97B4 */ }

        // Methods
        // RVA: 0x09DD9690  token: 0x6000074
        private System.Void .ctor() { }
        // RVA: 0x09DD7A1C  token: 0x6000075
        public System.Void InitOnUI() { }
        // RVA: 0x09DD79C0  token: 0x6000076
        private virtual System.Void Hypergryph.CloudClientBridge.IEventReceiver.OnReceiveData(System.String dataString) { }
        // RVA: 0x09DD7704  token: 0x6000077
        private virtual System.Void Hypergryph.CloudClientBridge.IEventReceiver.OnReceiveDataWithKey(System.String key, System.String dataString) { }
        // RVA: 0x09DD66AC  token: 0x6000078
        private System.Void HandleCommandNotSupported(Hypergryph.CloudClientBridge.ClientPipeMessage pipeMessage) { }
        // RVA: 0x09DD6A9C  token: 0x6000079
        private System.Void HandleGlobalEvent(Hypergryph.CloudClientBridge.ClientPipeMessage pipeMessage) { }
        // RVA: 0x09DD6E98  token: 0x600007A
        private System.Void HandleNormalEvent(Hypergryph.CloudClientBridge.ClientPipeMessage pipeMessage) { }
        // RVA: 0x09DD9208  token: 0x600007B
        private System.Void UpdateEventLogPlatformInfo(Newtonsoft.Json.Linq.JToken data) { }
        // RVA: 0x09DD71D4  token: 0x600007C
        private virtual System.Void Hypergryph.CloudClientBridge.IEventReceiver.OnChangeResolutionEvent(System.Int32 width, System.Int32 height) { }
        // RVA: 0x09DD7DE4  token: 0x600007D
        private System.Void InvokeCallback(System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback, System.String key, System.Int32 code, System.String message, System.String data, System.Boolean isGlobalEvent) { }
        // RVA: 0x09DD7C20  token: 0x600007E
        private System.Void InvokeCallbackDirect(System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback, Hypergryph.CloudClientBridge.CloudCallResult result, System.String key) { }
        // RVA: 0x09DD7F8C  token: 0x600007F
        private Hypergryph.CloudClientBridge.ClientPipeMessage ParseClientPipeMessage(System.String jsonString) { }
        // RVA: 0x09DD8C08  token: 0x6000080
        private static System.Void TrackClientEventSafe(Hypergryph.CloudClientBridge.ClientPipeMessage pipeMessage, System.String originalData) { }
        // RVA: 0x09DD85F8  token: 0x6000081
        private static System.Void ReportException(System.String methodName, System.Exception ex, Newtonsoft.Json.Linq.JObject extraInfo) { }
        // RVA: 0x09DD88CC  token: 0x6000082
        public System.Void SendDataWithKey(System.String key, System.String data, System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback) { }
        // RVA: 0x09DD5FD4  token: 0x6000083
        private System.String BuildCloudEventData(System.Int32 eventId, System.String key, System.String userData) { }
        // RVA: 0x09DD81E8  token: 0x6000084
        public System.Int32 RegisterGlobalCallback(Hypergryph.CloudClientBridge.CloudGameGlobalEvent globalEvent, System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback) { }
        // RVA: 0x02D315F0  token: 0x6000085
        private System.Void DispatchCachedGlobalEventOnRegister(Hypergryph.CloudClientBridge.CloudGameGlobalEvent globalEvent, System.String key, System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback) { }
        // RVA: 0x09DD8DB8  token: 0x6000086
        public System.Void UnRegisterGlobalCallback(Hypergryph.CloudClientBridge.CloudGameGlobalEvent globalEvent, System.Action<Hypergryph.CloudClientBridge.CloudCallResult> callback) { }
        // RVA: 0x09DD6368  token: 0x6000087
        public System.Void Cleanup() { }
        // RVA: 0x09DD961C  token: 0x6000088
        private static System.Void .cctor() { }
        // RVA: 0x02D31980  token: 0x6000089
        private System.Void <Cleanup>b__37_0() { }

    }

    // TypeToken: 0x2000022  // size: 0x10
    public static class CloudClientBridgeVersion
    {
        // Fields
        public static readonly System.String VERSION;  // static @ 0x0

        // Methods
        // RVA: 0x09DD1DC4  token: 0x6000099
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000023  // size: 0x10
    public class Logger
    {
        // Methods
        // RVA: 0x03FCD680  token: 0x600009A
        public static System.Void Debug(System.String message) { }
        // RVA: 0x09DD9A7C  token: 0x600009B
        public static System.Void Error(System.String message) { }
        // RVA: 0x03FCD710  token: 0x600009C
        public static System.Void PrintLogToView(System.String logInfo) { }

    }

    // TypeToken: 0x2000025  // size: 0x10
    public class UnityMainThread
    {
        // Fields
        private static System.Threading.SynchronizationContext MainContext;  // static @ 0x0
        public static System.Boolean isDebug;  // static @ 0x8
        private static readonly System.Object initLock;  // static @ 0x10

        // Methods
        // RVA: 0x03FCE180  token: 0x600009F
        public static System.Void InitOnUI() { }
        // RVA: 0x03FCD7E0  token: 0x60000A0
        public static System.Void RunOnMainThread(System.Action action) { }
        // RVA: -1  // generic def  token: 0x60000A1
        public static System.Threading.Tasks.Task<T> RunOnMainThread(System.Func<T> func) { }
        // RVA: 0x04D21660  token: 0x60000A2
        private static System.Void .cctor() { }

    }

}

namespace WeLing.SDK
{

    // TypeToken: 0x2000002  // size: 0x10
    public class MonoPInvokeCallbackAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000003  // size: 0x10
    public class PipeWrapper
    {
        // Fields
        public static System.String version;  // static @ 0x0
        public static System.Action<System.Byte[],System.Int32> OnReceiveDataEvent;  // static @ 0x8
        public static System.Action<System.String,System.Byte[],System.Int32> OnReceiveDataWithKeyEvent;  // static @ 0x10
        public static System.Action<System.String,System.Byte[],System.Int32,System.String> OnReceiveDataWithKeyExEvent;  // static @ 0x18
        public static System.Action<System.Int32,System.Int32> OnChangeResolutionEvent;  // static @ 0x20

        // Methods
        // RVA: 0x09DDA18C  token: 0x6000002
        private static System.Boolean _InitEx() { }
        // RVA: 0x09DDA1FC  token: 0x6000003
        private static System.Boolean _Release() { }
        // RVA: 0x09DDA328  token: 0x6000004
        private static System.Boolean _SendData(System.Byte[] data, System.Int32 size) { }
        // RVA: 0x09DDA26C  token: 0x6000005
        private static System.Boolean _SendDataWithKey(System.String key, System.Byte[] data, System.Int32 size) { }
        // RVA: 0x09DDA508  token: 0x6000006
        private static System.Boolean _SetOnRecieveDataCallback(WeLing.SDK.PipeWrapper._OnReceiveData cb) { }
        // RVA: 0x09DDA440  token: 0x6000007
        private static System.Boolean _SetOnDataWithKeyCallback(WeLing.SDK.PipeWrapper._OnReceiveDataWithKey cb) { }
        // RVA: 0x09DDA484  token: 0x6000008
        private static System.Boolean _SetOnDataWithKeyExCallback(WeLing.SDK.PipeWrapper._OnReceiveDataWithKeyEx cb) { }
        // RVA: 0x09DDA3BC  token: 0x6000009
        private static System.Boolean _SetOnChangeResolutionCallback(WeLing.SDK.PipeWrapper._OnChangeResolution cb) { }
        // RVA: 0x09DD9D98  token: 0x600000A
        private static System.Void SimpleCallBackImp(System.IntPtr data, System.Int32& size) { }
        // RVA: 0x09DD9F6C  token: 0x600000B
        private static System.Void WithKeyCallBackImp(System.String key, System.IntPtr data, System.Int32& size) { }
        // RVA: 0x09DDA074  token: 0x600000C
        private static System.Void WithKeyExCallBackImp(System.String key, System.IntPtr data, System.Int32& size, System.String userID) { }
        // RVA: 0x09DD9AE8  token: 0x600000D
        private static System.Void ChangeResolutionCallBackImp(System.Int32 width, System.Int32 height) { }
        // RVA: 0x09DD9B70  token: 0x600000E
        public static System.Boolean Init() { }
        // RVA: 0x09DD9E8C  token: 0x600000F
        public static System.Void Uninit() { }
        // RVA: 0x09DD9D40  token: 0x6000010
        public static System.Boolean SendData(System.Byte[] data, System.Int32 size) { }
        // RVA: 0x09DD9CE0  token: 0x6000011
        public static System.Boolean SendDataWithKey(System.String key, System.Byte[] data, System.Int32 size) { }
        // RVA: 0x09DDA54C  token: 0x6000012
        private static System.Void .cctor() { }

    }

}

