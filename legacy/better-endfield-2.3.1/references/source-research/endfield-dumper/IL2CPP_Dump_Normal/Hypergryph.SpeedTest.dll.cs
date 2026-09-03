// ========================================================
// Dumped by @desirepro
// Assembly: Hypergryph.SpeedTest.dll
// Classes:  33
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000003  // size: 0x20
    public sealed class <>c__DisplayClass11_0
    {
        // Fields
        public Hypergryph.SpeedTest.EventLogWrapper <>4__this;  // 0x10
        public System.String region;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000007
        public System.Void .ctor() { }
        // RVA: 0x040BA980  token: 0x6000008
        private System.Boolean <init>b__0() { }

    }

    // TypeToken: 0x2000004  // size: 0x58
    public sealed struct <init>d__11 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncVoidMethodBuilder <>t__builder;  // 0x18
        public Hypergryph.SpeedTest.EventLogWrapper <>4__this;  // 0x38
        private Hypergryph.SpeedTest.EventLogWrapper.<>c__DisplayClass11_0 <>8__1;  // 0x40
        private System.Runtime.CompilerServices.TaskAwaiter <>u__1;  // 0x48
        private System.Runtime.CompilerServices.TaskAwaiter<System.Boolean> <>u__2;  // 0x50

        // Methods
        // RVA: 0x03811CC0  token: 0x6000009
        private virtual System.Void MoveNext() { }
        // RVA: 0x053982D4  token: 0x600000A
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000006  // size: 0x40
    public sealed struct <<EventTrack>b__0>d : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public Hypergryph.SpeedTest.EventLogWrapper.<>c__DisplayClass12_0 <>4__this;  // 0x30
        private System.Runtime.CompilerServices.TaskAwaiter <>u__1;  // 0x38

        // Methods
        // RVA: 0x09DF9A98  token: 0x600000D
        private virtual System.Void MoveNext() { }
        // RVA: 0x09DF9D28  token: 0x600000E
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000005  // size: 0x28
    public sealed class <>c__DisplayClass12_0
    {
        // Fields
        public Hypergryph.SpeedTest.EventLogWrapper <>4__this;  // 0x10
        public System.String name;  // 0x18
        public System.Collections.Generic.Dictionary<System.String,System.Object> data;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x600000B
        public System.Void .ctor() { }
        // RVA: 0x09DF9D84  token: 0x600000C
        private System.Threading.Tasks.Task <EventTrack>b__0() { }

    }

    // TypeToken: 0x2000007  // size: 0x28
    public sealed class <>c__DisplayClass13_0
    {
        // Fields
        public Hypergryph.SpeedTest.EventLogWrapper <>4__this;  // 0x10
        public System.String name;  // 0x18
        public Newtonsoft.Json.Linq.JObject properties;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x600000F
        public System.Void .ctor() { }
        // RVA: 0x09DF9E34  token: 0x6000010
        private System.Boolean <ExecuteEventTrackSafely>b__0() { }

    }

    // TypeToken: 0x2000008  // size: 0x58
    public sealed struct <ExecuteEventTrackSafely>d__13 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public Hypergryph.SpeedTest.EventLogWrapper <>4__this;  // 0x30
        public System.String name;  // 0x38
        public System.Collections.Generic.Dictionary<System.String,System.Object> data;  // 0x40
        private Hypergryph.SpeedTest.EventLogWrapper.<>c__DisplayClass13_0 <>8__1;  // 0x48
        private System.Runtime.CompilerServices.TaskAwaiter<System.Boolean> <>u__1;  // 0x50

        // Methods
        // RVA: 0x09DF8968  token: 0x6000011
        private virtual System.Void MoveNext() { }
        // RVA: 0x09DF8E54  token: 0x6000012
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x200000F  // size: 0x48
    public sealed struct <OnApplicationPause>d__16 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public System.Boolean pauseStatus;  // 0x30
        public Hypergryph.SpeedTest.HGSpeedTestSDK <>4__this;  // 0x38
        private System.Runtime.CompilerServices.TaskAwaiter<System.Boolean> <>u__1;  // 0x40

        // Methods
        // RVA: 0x038121F0  token: 0x6000026
        private virtual System.Void MoveNext() { }
        // RVA: 0x0459AD30  token: 0x6000027
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000010  // size: 0x28
    public sealed class <>c__DisplayClass17_0
    {
        // Fields
        public Hypergryph.SpeedTest.HGSpeedTestSDK <>4__this;  // 0x10
        public System.Action<Hypergryph.SpeedTest.SpeedTestResult> callback;  // 0x18
        public System.Int32 round;  // 0x20
        public Hypergryph.SpeedTest.SpeedTestProtocol protocol;  // 0x24

        // Methods
        // RVA: 0x041E1670  token: 0x6000028
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x20
    public sealed class <>c__DisplayClass17_1
    {
        // Fields
        public Hypergryph.SpeedTest.SpeedTestServerInfo server;  // 0x10
        public Hypergryph.SpeedTest.HGSpeedTestSDK.<>c__DisplayClass17_0 CS$<>8__locals1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000029
        public System.Void .ctor() { }
        // RVA: 0x09DF9F44  token: 0x600002A
        private System.Void <StartSpeedTest>b__0() { }

    }

    // TypeToken: 0x2000012  // size: 0x58
    public sealed struct <StartSpeedTest>d__17 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder<Hypergryph.SpeedTest.TaskResult> <>t__builder;  // 0x18
        public Hypergryph.SpeedTest.HGSpeedTestSDK <>4__this;  // 0x30
        public System.Action<Hypergryph.SpeedTest.SpeedTestResult> callback;  // 0x38
        public System.Int32 round;  // 0x40
        public Hypergryph.SpeedTest.SpeedTestProtocol protocol;  // 0x44
        public System.Collections.Generic.List<Hypergryph.SpeedTest.SpeedTestServerInfo> serverList;  // 0x48
        private System.Runtime.CompilerServices.TaskAwaiter <>u__1;  // 0x50

        // Methods
        // RVA: 0x09DF8EB0  token: 0x600002B
        private virtual System.Void MoveNext() { }
        // RVA: 0x09DF9A38  token: 0x600002C
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000013  // size: 0x20
    public sealed class <>c__DisplayClass20_0
    {
        // Fields
        public System.Action<Hypergryph.SpeedTest.SpeedTestResult> callback;  // 0x10
        public Hypergryph.SpeedTest.SpeedTestResult result;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600002D
        public System.Void .ctor() { }
        // RVA: 0x09DF9FD0  token: 0x600002E
        private System.Void <NotifyRoundResult>b__0() { }

    }

    // TypeToken: 0x2000014  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Hypergryph.SpeedTest.HGSpeedTestSDK.<>c <>9;  // static @ 0x0
        public static System.Func<System.Int64,System.Int64> <>9__27_0;  // static @ 0x8

        // Methods
        // RVA: 0x09DFA0A4  token: 0x600002F
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000030
        public System.Void .ctor() { }
        // RVA: 0x04D88C30  token: 0x6000031
        private System.Int64 <GetP50>b__27_0(System.Int64 x) { }

    }

    // TypeToken: 0x2000016  // size: 0x40
    public sealed struct <init>d__7 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public Hypergryph.SpeedTest.NativeConfigLoader <>4__this;  // 0x30
        private System.Runtime.CompilerServices.TaskAwaiter<System.String> <>u__1;  // 0x38

        // Methods
        // RVA: 0x04863740  token: 0x600003B
        private virtual System.Void MoveNext() { }
        // RVA: 0x0459AD90  token: 0x600003C
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000018  // size: 0x18
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.String logInfo;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000040
        public System.Void .ctor() { }
        // RVA: 0x03AC6D90  token: 0x6000041
        private System.Void <PrintLogToView>b__0() { }

    }

    // TypeToken: 0x200001A  // size: 0x18
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.Action action;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000046
        public System.Void .ctor() { }
        // RVA: 0x04CC60B0  token: 0x6000047
        private System.Void <RunOnMainThread>b__0(System.Object _) { }

    }

    // TypeToken: 0x200001B
    public sealed class <>c__DisplayClass4_0`1
    {
        // Fields
        public System.Threading.Tasks.TaskCompletionSource<T> tcs;  // 0x0
        public System.Func<T> func;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000048
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000049
        private System.Void <RunOnMainThread>b__0() { }

    }

    // TypeToken: 0x200001C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Hypergryph.SpeedTest.UnityMainThread.<>c <>9;  // static @ 0x0
        public static System.Func<System.Boolean> <>9__5_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D41EF0  token: 0x600004A
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600004B
        public System.Void .ctor() { }
        // RVA: 0x05396550  token: 0x600004C
        private System.Boolean <isAppFocused>b__5_0() { }

    }

    // TypeToken: 0x200001D  // size: 0x38
    public sealed struct <isAppFocused>d__5 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Boolean> <>t__builder;  // 0x18
        private System.Runtime.CompilerServices.TaskAwaiter<System.Boolean> <>u__1;  // 0x30

        // Methods
        // RVA: 0x03811A60  token: 0x600004D
        private virtual System.Void MoveNext() { }
        // RVA: 0x048BB2C0  token: 0x600004E
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000020  // size: 0x20
    public sealed struct __StaticArrayInitTypeSize=16
    {
    }

    // TypeToken: 0x2000021  // size: 0x30
    public sealed struct __StaticArrayInitTypeSize=32
    {
    }

    // TypeToken: 0x200001F  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=32 4DAABD10C615552E680FD1F3F9C9E1B1C74413B5F519EDE1335CD44C46B11EC8;  // static @ 0x0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=16 701AA427DC569405D7608BAD336BBED96E382E97EE07C02CFED96D856E313AE5;  // static @ 0x20

    }

namespace Hypergryph.SpeedTest
{

    // TypeToken: 0x2000002  // size: 0x20
    public class EventLogWrapper
    {
        // Fields
        private static readonly Hypergryph.SpeedTest.EventLogWrapper _instance;  // static @ 0x0
        private System.String _appId;  // 0x10
        private System.String _gameVersion;  // 0x18

        // Properties
        Hypergryph.SpeedTest.EventLogWrapper Instance { get; /* RVA: 0x09DF6434 */ }

        // Methods
        // RVA: 0x04CDB9D0  token: 0x6000001
        private System.Void .ctor() { }
        // RVA: 0x030749A0  token: 0x6000003
        public System.Void init() { }
        // RVA: 0x09DF625C  token: 0x6000004
        public System.Void EventTrack(System.String name, System.Collections.Generic.Dictionary<System.String,System.Object> data) { }
        // RVA: 0x09DF6348  token: 0x6000005
        private System.Threading.Tasks.Task ExecuteEventTrackSafely(System.String name, System.Collections.Generic.Dictionary<System.String,System.Object> data) { }
        // RVA: 0x04CDB960  token: 0x6000006
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x10
    public static class HGSpeedTestSDKVersion
    {
        // Fields
        public static readonly System.String VERSION;  // static @ 0x0

        // Methods
        // RVA: 0x09DF6484  token: 0x6000013
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x20
    public class SpeedTestServerInfo
    {
        // Fields
        public System.String host;  // 0x10
        public System.String name;  // 0x18

        // Methods
        // RVA: 0x032AF420  token: 0x6000014
        public System.Void .ctor(System.String host, System.String name) { }

    }

    // TypeToken: 0x200000B  // size: 0x20
    public class TaskResult
    {
        // Fields
        public System.Int32 taskCode;  // 0x10
        public System.String message;  // 0x18

        // Methods
        // RVA: 0x05391D70  token: 0x6000015
        public System.Void .ctor(System.Int32 code, System.String message) { }

    }

    // TypeToken: 0x200000C  // size: 0x28
    public class SpeedTestResult
    {
        // Fields
        public System.String host;  // 0x10
        public System.String name;  // 0x18
        public System.Int32 latency;  // 0x20
        public System.Int32 resultCode;  // 0x24

        // Methods
        // RVA: 0x09DF8928  token: 0x6000016
        public System.Void .ctor(System.Int32 resultCode, System.Int32 latency, Hypergryph.SpeedTest.SpeedTestServerInfo server) { }

    }

    // TypeToken: 0x200000D  // size: 0x14
    public sealed struct SpeedTestProtocol
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Hypergryph.SpeedTest.SpeedTestProtocol TCP;  // const
        public static Hypergryph.SpeedTest.SpeedTestProtocol UDP;  // const

    }

    // TypeToken: 0x200000E  // size: 0x40
    public class HGSpeedTestSDK
    {
        // Fields
        private static readonly Hypergryph.SpeedTest.HGSpeedTestSDK _instance;  // static @ 0x0
        private readonly System.Collections.Generic.Dictionary<System.String,System.Net.Sockets.TcpClient> _connections;  // 0x10
        private readonly System.Collections.Generic.Dictionary<System.String,System.Net.Sockets.NetworkStream> _streams;  // 0x18
        private readonly System.Object _lockObject;  // 0x20
        private System.Threading.CancellationTokenSource cts;  // 0x28
        private System.Int64 _packetIndex;  // 0x30
        private System.Boolean _isInSpeedTest;  // 0x38

        // Methods
        // RVA: 0x04A14000  token: 0x6000017
        private System.Void .ctor() { }
        // RVA: 0x09DF7074  token: 0x6000018
        public static Hypergryph.SpeedTest.HGSpeedTestSDK GetInstance() { }
        // RVA: 0x03F266A0  token: 0x6000019
        public System.Threading.Tasks.Task OnApplicationPause(System.Boolean pauseStatus) { }
        // RVA: 0x09DF7B78  token: 0x600001A
        public System.Threading.Tasks.Task<Hypergryph.SpeedTest.TaskResult> StartSpeedTest(System.Collections.Generic.List<Hypergryph.SpeedTest.SpeedTestServerInfo> serverList, System.Action<Hypergryph.SpeedTest.SpeedTestResult> callback, System.Int32 round, Hypergryph.SpeedTest.SpeedTestProtocol protocol) { }
        // RVA: 0x09DF77D0  token: 0x600001B
        private System.Void OnSpeedTestStart() { }
        // RVA: 0x09DF75B0  token: 0x600001C
        private Hypergryph.SpeedTest.TaskResult OnSpeedTaskResult(System.Int32 code, System.String message) { }
        // RVA: 0x09DF7278  token: 0x600001D
        private System.Void NotifyRoundResult(System.Action<Hypergryph.SpeedTest.SpeedTestResult> callback, System.Int32 code, System.Int32 latency, Hypergryph.SpeedTest.SpeedTestServerInfo server) { }
        // RVA: 0x09DF64E0  token: 0x600001E
        private System.Void CleanupConnections() { }
        // RVA: 0x09DF7C94  token: 0x600001F
        private System.Void TestServerLatency(Hypergryph.SpeedTest.SpeedTestServerInfo server, System.Action<Hypergryph.SpeedTest.SpeedTestResult> callback, System.Int32 round, Hypergryph.SpeedTest.SpeedTestProtocol protocol, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x09DF6A78  token: 0x6000020
        private System.Int32 EstablishConnection(Hypergryph.SpeedTest.SpeedTestServerInfo server) { }
        // RVA: 0x09DF82B8  token: 0x6000021
        private Hypergryph.SpeedTest.SpeedTestResult TestTcpLatency(Hypergryph.SpeedTest.SpeedTestServerInfo server, System.Collections.Generic.List<System.Int64> serverLatencyList, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x09DF7938  token: 0x6000022
        private System.Int64 SendTcpPackage(System.Net.Sockets.NetworkStream stream) { }
        // RVA: 0x09DF6978  token: 0x6000023
        private System.Byte[] CreateTcpPacket() { }
        // RVA: 0x09DF70C4  token: 0x6000024
        public System.Int64 GetP50(System.Collections.Generic.List<System.Int64> latencies) { }
        // RVA: 0x04A13F90  token: 0x6000025
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000015  // size: 0x28
    public class NativeConfigLoader
    {
        // Fields
        private static Hypergryph.SpeedTest.NativeConfigLoader instance;  // static @ 0x0
        private System.String region;  // 0x10
        private System.String appDataPath;  // 0x18
        private System.String _gameVersion;  // 0x20

        // Methods
        // RVA: 0x04CE7FF0  token: 0x6000032
        private System.Void .ctor() { }
        // RVA: 0x04CDA8D0  token: 0x6000033
        public System.Threading.Tasks.Task init() { }
        // RVA: 0x09DF886C  token: 0x6000034
        public static Hypergryph.SpeedTest.NativeConfigLoader GetInstance() { }
        // RVA: 0x04D862C0  token: 0x6000035
        public System.String GetGameVersion() { }
        // RVA: 0x038115F0  token: 0x6000036
        public System.String GetRegion() { }
        // RVA: 0x03F4DB30  token: 0x6000037
        private System.String GetRegionFromConfig() { }
        // RVA: 0x03F4D9D0  token: 0x6000038
        public System.String ReadWinConfig() { }
        // RVA: 0x04CE7F80  token: 0x6000039
        private static System.Void .cctor() { }
        // RVA: 0x041A7470  token: 0x600003A
        private System.String <init>b__7_0() { }

    }

    // TypeToken: 0x2000017  // size: 0x10
    public class SpeedTestLogger
    {
        // Methods
        // RVA: 0x03073F00  token: 0x600003D
        public static System.Void Debug(System.String message) { }
        // RVA: 0x09DF88BC  token: 0x600003E
        public static System.Void Error(System.String message) { }
        // RVA: 0x03073F80  token: 0x600003F
        public static System.Void PrintLogToView(System.String logInfo) { }

    }

    // TypeToken: 0x2000019  // size: 0x10
    public class UnityMainThread
    {
        // Fields
        private static System.Threading.SynchronizationContext MainContext;  // static @ 0x0
        public static System.Boolean isDebug;  // static @ 0x8

        // Methods
        // RVA: 0x03FCE3F0  token: 0x6000042
        public static System.Void Init() { }
        // RVA: 0x03074030  token: 0x6000043
        public static System.Void RunOnMainThread(System.Action action) { }
        // RVA: -1  // generic def  token: 0x6000044
        public static System.Threading.Tasks.Task<T> RunOnMainThread(System.Func<T> func) { }
        // RVA: 0x03812500  token: 0x6000045
        public static System.Threading.Tasks.Task<System.Boolean> isAppFocused() { }

    }

    // TypeToken: 0x200001E  // size: 0x10
    public class WinConfigParser
    {
        // Fields
        private static readonly System.Byte[] iv;  // static @ 0x0
        private static readonly System.Byte[] aes_key;  // static @ 0x8
        private static readonly System.String public_key_str;  // static @ 0x10

        // Methods
        // RVA: 0x030757F0  token: 0x600004F
        public static System.String ParseEncryptionFile(System.String encryptionfile, System.String verifyfile) { }
        // RVA: 0x03C0D7B0  token: 0x6000050
        private static System.String DecryptConfig(System.Byte[] encryptedData, System.Byte[] key, System.Byte[] iv) { }
        // RVA: 0x03C0A840  token: 0x6000051
        private static System.Boolean VerifySignature(System.String data, System.String sign) { }
        // RVA: 0x03FE4EF0  token: 0x6000052
        private static System.Void ImportRsaPublicKeyManually(System.Security.Cryptography.RSACryptoServiceProvider rsa, System.Byte[] publicKeyBytes) { }
        // RVA: 0x03C0B260  token: 0x6000053
        private static System.Byte[] ExtractPublicKeyFromPem(System.String pemString) { }
        // RVA: 0x04B4BB30  token: 0x6000054
        private static System.Void .cctor() { }

    }

}

