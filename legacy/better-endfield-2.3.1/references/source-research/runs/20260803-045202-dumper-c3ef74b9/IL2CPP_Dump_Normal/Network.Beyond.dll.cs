// ========================================================
// Dumped by @desirepro
// Assembly: Network.Beyond.dll
// Classes:  107
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000015  // size: 0x18
    public class HandlerEntry
    {
        // Fields
        public Beyond.Network.NetResponseHandler handler;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600007D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000016  // size: 0x40
    public class ResultHandler : System.IDisposable
    {
        // Fields
        private System.Boolean m_ignoreSeq;  // 0x10
        private Beyond.ListDict<Proto.SCMessageID,System.Object> m_responseTokens;  // 0x18
        private Beyond.ListDict<Proto.SCMessageID,Beyond.Network.NetResponseHandler> m_responseHandlers;  // 0x20
        private System.Boolean <isDisposed>k__BackingField;  // 0x28
        private System.UInt64 <cachedSeqNum>k__BackingField;  // 0x30
        private System.Int32 <sessionID>k__BackingField;  // 0x38

        // Properties
        System.Boolean isDisposed { get; /* RVA: 0x011F33C0 */ set; /* RVA: 0x011F33D0 */ }
        System.UInt64 cachedSeqNum { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x011F36F0 */ }
        System.Int32 sessionID { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }

        // Methods
        // RVA: 0x0A898E20  token: 0x6000084
        public System.Void SetIgnoreSeq(System.Boolean ignoreSeq) { }
        // RVA: 0x03A4F4A0  token: 0x6000085
        public Beyond.Network.NetBus.ResultHandler OnResponse(Proto.SCMessageID msgId, Beyond.Network.NetResponseHandler handler) { }
        // RVA: 0x03A4EB80  token: 0x6000086
        public virtual System.Void Dispose() { }
        // RVA: 0x03A4E950  token: 0x6000087
        private System.Boolean _CheckSessionIDValid() { }
        // RVA: 0x03A4E7E0  token: 0x6000088
        private System.Void _HandleMessage(Beyond.Network.NetResponse& msg) { }
        // RVA: 0x041E1670  token: 0x6000089
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x10
    public abstract class Sender
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600008A
        public virtual Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg) { }
        // RVA: -1  // abstract  token: 0x600008B
        public virtual System.Void OnNetShutDown() { }
        // RVA: 0x03AC46C0  token: 0x600008C
        protected Beyond.Network.NetBus.ResultHandler DoSend(Google.Protobuf.IMessage message) { }
        // RVA: 0x0A898E74  token: 0x600008D
        protected System.Void DoListen(Proto.SCMessageID msgId, Beyond.Network.NetResponseHandler handler) { }
        // RVA: 0x0A898F14  token: 0x600008E
        protected System.Void DoUnListen(Proto.SCMessageID msgId, Beyond.Network.NetResponseHandler handler) { }
        // RVA: 0x041E1670  token: 0x600008F
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200001D  // size: 0x20
    public sealed class <>c__DisplayClass7_0
    {
        // Fields
        public System.Int32 key;  // 0x10
        public Beyond.Network.UIBusSender <>4__this;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60000B3
        public System.Void .ctor() { }
        // RVA: 0x0A899A04  token: 0x60000B4
        private System.Void <Send>b__0(Beyond.Network.NetResponse& resp) { }
        // RVA: 0x0A899AF0  token: 0x60000B5
        private System.Void <Send>b__1(Beyond.Network.NetResponse& resp) { }
        // RVA: 0x0A899C4C  token: 0x60000B6
        private System.Void <Send>b__2(Beyond.Network.NetResponse& resp) { }

    }

    // TypeToken: 0x200001F  // size: 0x20
    public sealed class <>c__DisplayClass2_0
    {
        // Fields
        public System.Int32 key;  // 0x10
        public Beyond.Network.TeleportMaskSender <>4__this;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60000BD
        public System.Void .ctor() { }
        // RVA: 0x0A899870  token: 0x60000BE
        private System.Void <Send>b__0(Beyond.Network.NetResponse& resp) { }
        // RVA: 0x03A4CE00  token: 0x60000BF
        private System.Void <Send>b__1(Beyond.Network.NetResponse& resp) { }
        // RVA: 0x0A8998EC  token: 0x60000C0
        private System.Void <Send>b__2(Beyond.Network.NetResponse& resp) { }
        // RVA: 0x0A899968  token: 0x60000C1
        private System.Void <Send>b__3(Beyond.Network.NetResponse& resp) { }

    }

    // TypeToken: 0x2000021  // size: 0x18
    public sealed class <>c__DisplayClass7_0
    {
        // Fields
        public System.Action<System.Int32> onError;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000C9
        public System.Void .ctor() { }
        // RVA: 0x0A899A80  token: 0x60000CA
        private System.Void <Send>b__0(Beyond.Network.NetResponse& errorMsg) { }
        // RVA: 0x0A899B6C  token: 0x60000CB
        private System.Void <Send>b__1(Beyond.Network.NetResponse& errorMsg) { }
        // RVA: 0x0A899BDC  token: 0x60000CC
        private System.Void <Send>b__2(Beyond.Network.NetResponse& errorMsg) { }

    }

    // TypeToken: 0x200002A  // size: 0x18
    public class Config
    {
        // Fields
        public System.Collections.Generic.Dictionary<System.Int32,Proto.SCMessageID> messages;  // 0x10

        // Methods
        // RVA: 0x0A89341C  token: 0x60000D7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002F  // size: 0x18
    public sealed class <>c__DisplayClass18_0
    {
        // Fields
        public System.Object target;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000E6
        public System.Void .ctor() { }
        // RVA: 0x03895110  token: 0x60000E7
        private System.Void <AutoRegisterMessageHandlers>b__0(System.Type msgType, System.Reflection.MethodInfo methodInfo) { }

    }

    // TypeToken: 0x2000030  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.Network.NetUtil.<>c <>9;  // static @ 0x0
        public static System.Action<System.Type,System.Reflection.MethodInfo> <>9__19_0;  // static @ 0x8

        // Methods
        // RVA: 0x0A899CC8  token: 0x60000E8
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000E9
        public System.Void .ctor() { }
        // RVA: 0x0A899824  token: 0x60000EA
        private System.Void <AutoUnRegisterMessageHandlers>b__19_0(System.Type msgType, System.Reflection.MethodInfo methodInfo) { }

    }

    // TypeToken: 0x2000032  // size: 0x20
    public class NetworkFrequency
    {
        // Fields
        public System.Type type;  // 0x10
        public System.Single second;  // 0x18
        public System.Int32 maxCount;  // 0x1c

        // Methods
        // RVA: 0x041E1670  token: 0x60000EC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000034  // size: 0x30
    public class MaskInfo
    {
        // Fields
        public System.Int32 key;  // 0x10
        public System.String name;  // 0x18
        public System.Single startTime;  // 0x20
        public System.Single delayTime;  // 0x24
        public System.Single timeoutTime;  // 0x28

        // Properties
        System.Single endTime { get; /* RVA: 0x03A4CDD0 */ }
        System.Single showDecoTime { get; /* RVA: 0x03A4CDA0 */ }

        // Methods
        // RVA: 0x04DA5BB0  token: 0x6000103
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000036  // size: 0x20
    public class Config
    {
        // Fields
        public System.String key;  // 0x10
        public Beyond.SerializeFieldDictionaryPaired<Proto.CSMessageID,Proto.SCMessageID> messages;  // 0x18

        // Methods
        // RVA: 0x04B30D40  token: 0x6000105
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000038  // size: 0x14
    public sealed struct EUrlType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Network.NetworkUrls.EUrlType ServerList;  // const
        public static Beyond.Network.NetworkUrls.EUrlType GameLicense;  // const
        public static Beyond.Network.NetworkUrls.EUrlType AgeTips;  // const
        public static Beyond.Network.NetworkUrls.EUrlType GateBulletin;  // const
        public static Beyond.Network.NetworkUrls.EUrlType GameBulletin;  // const
        public static Beyond.Network.NetworkUrls.EUrlType Launcher;  // const
        public static Beyond.Network.NetworkUrls.EUrlType Gov;  // const

    }

    // TypeToken: 0x200003A  // size: 0x20
    public sealed struct WeakNetDelayConfig
    {
        // Fields
        public System.Boolean enabled;  // 0x10
        public System.Int32 minDelayMs;  // 0x14
        public System.Int32 maxDelayMs;  // 0x18
        public System.Int32 maxTriggerCountPerSecond;  // 0x1c

    }

    // TypeToken: 0x200003C  // size: 0x14
    public sealed struct ErrorType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Network.HttpSender.ErrorType NONE;  // const
        public static Beyond.Network.HttpSender.ErrorType TIMEOUT;  // const
        public static Beyond.Network.HttpSender.ErrorType DNS_FAILED;  // const
        public static Beyond.Network.HttpSender.ErrorType CANCELED;  // const
        public static Beyond.Network.HttpSender.ErrorType INTERNAL;  // const

    }

    // TypeToken: 0x200003D  // size: 0x20
    public class Cancellation
    {
        // Fields
        private System.Boolean m_isCancelled;  // 0x10
        private System.Action OnCancelInvoked;  // 0x18

        // Events
        event System.Action OnCancelInvoked;

        // Methods
        // RVA: 0x0A893240  token: 0x6000148
        public System.Void Cancel() { }
        // RVA: 0x0A8932A0  token: 0x6000149
        public System.Boolean IsCanceled() { }
        // RVA: 0x041E1670  token: 0x600014A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003E  // size: 0x30
    public sealed struct FGetOptions
    {
        // Fields
        public static readonly Beyond.Network.HttpSender.FGetOptions DEFAULT;  // static @ 0x0
        public System.Collections.Generic.Dictionary<System.String,System.String> headers;  // 0x10
        public System.String contentType;  // 0x18
        public System.Boolean headOnly;  // 0x20
        public Beyond.Network.HttpSender.Cancellation cancellation;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600014B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003F  // size: 0x30
    public sealed struct FPostOptions
    {
        // Fields
        public static readonly Beyond.Network.HttpSender.FPostOptions DEFAULT;  // static @ 0x0
        public System.Collections.Generic.Dictionary<System.String,System.String> headers;  // 0x10
        public System.String textBody;  // 0x18
        public System.String contentType;  // 0x20
        public Beyond.Network.HttpSender.Cancellation cancellation;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600014C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000040  // size: 0x68
    public sealed struct FSendFunc
    {
        // Fields
        public System.String url;  // 0x10
        public System.Int32 opt;  // 0x18
        public Beyond.Network.HttpSender.FGetOptions optionsGET;  // 0x20
        public Beyond.Network.HttpSender.FPostOptions optionsPOST;  // 0x40
        public System.Boolean editorOnly;  // 0x60

        // Methods
        // RVA: 0x03E37380  token: 0x600014D
        public UnityEngine.Networking.UnityWebRequest Invoke() { }
        // RVA: 0x03E37330  token: 0x600014E
        public Beyond.Network.HttpSender.Cancellation GetCancellation() { }
        // RVA: 0x0A8934E8  token: 0x600014F
        public System.String GetOptMethod() { }

    }

    // TypeToken: 0x2000041  // size: 0x48
    public class Result
    {
        // Fields
        public System.Int32 respCode;  // 0x10
        public Beyond.Network.HttpSender.ErrorType errorType;  // 0x14
        public System.Boolean isError;  // 0x18
        public System.Exception exception;  // 0x20
        public System.String respText;  // 0x28
        public System.Byte[] respData;  // 0x30
        public System.String respError;  // 0x38
        public System.Boolean isServiceDone;  // 0x40

        // Methods
        // RVA: 0x041E1670  token: 0x6000150
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000042  // size: 0x60
    public class TypedResult : Result
    {
        // Fields
        private System.Type <respType>k__BackingField;  // 0x48
        public System.Object respObject;  // 0x50
        public System.Boolean isDeserializeFailed;  // 0x58

        // Properties
        System.Type respType { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }

        // Methods
        // RVA: 0x035A41A0  token: 0x6000153
        public System.Void .ctor(System.Type respType) { }

    }

    // TypeToken: 0x2000043
    public class Result`1 : TypedResult
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000154
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000155
        public TResp GetRespObject() { }

    }

    // TypeToken: 0x2000044  // size: 0x50
    public sealed class <YieldSendGet>d__16 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.String url;  // 0x20
        public Beyond.Network.HttpSender.FGetOptions options;  // 0x28
        public Beyond.Misc.ObjectRef resultRef;  // 0x48

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000156
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000157
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03C07E90  token: 0x6000158
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0A899D2C  token: 0x600015A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000045
    public sealed class <YieldSendGet>d__17`1 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private System.Object <>2__current;  // 0x0
        public System.String url;  // 0x0
        public Beyond.Network.HttpSender.FGetOptions options;  // 0x0
        public Beyond.Misc.ObjectRef resultRef;  // 0x0

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600015C
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x600015D
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x600015E
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000160
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000046  // size: 0x58
    public sealed class <YieldSendGet>d__18 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Network.HttpSender.FGetOptions options;  // 0x20
        public System.String url;  // 0x40
        public Beyond.Misc.ObjectRef resultRef;  // 0x48
        public System.Type respType;  // 0x50

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000162
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000163
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x046BACA0  token: 0x6000164
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0A899D78  token: 0x6000166
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000047  // size: 0x50
    public sealed class <YieldSendPost>d__19 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.String url;  // 0x20
        public Beyond.Network.HttpSender.FPostOptions options;  // 0x28
        public Beyond.Misc.ObjectRef resultRef;  // 0x48

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000168
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000169
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0A899DC4  token: 0x600016A
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0A899E2C  token: 0x600016C
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000048
    public sealed class <YieldSendPost>d__20`1 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private System.Object <>2__current;  // 0x0
        public System.String url;  // 0x0
        public Beyond.Network.HttpSender.FPostOptions options;  // 0x0
        public Beyond.Misc.ObjectRef resultRef;  // 0x0

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600016E
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x600016F
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000170
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000172
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000049  // size: 0x58
    public sealed class <YieldSendPost>d__21 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Network.HttpSender.FPostOptions options;  // 0x20
        public System.String url;  // 0x40
        public Beyond.Misc.ObjectRef resultRef;  // 0x48
        public System.Type respType;  // 0x50

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000174
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000175
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x046BABB0  token: 0x6000176
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0A899E78  token: 0x6000178
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200004A  // size: 0xA0
    public sealed class <_YieldSendHttpRequest>d__28 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Network.HttpSender.FSendFunc sendFunc;  // 0x20
        public Beyond.Network.HttpSender.Result result;  // 0x78
        private Beyond.Network.HttpSender.Cancellation <cancelation>5__2;  // 0x80
        private System.Boolean <isCanceled>5__3;  // 0x88
        private UnityEngine.Networking.UnityWebRequest <request>5__4;  // 0x90
        private UnityEngine.Networking.UnityWebRequestAsyncOperation <sendOpt>5__5;  // 0x98

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600017A
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x0A899F10  token: 0x600017B
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03E363C0  token: 0x600017C
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x03E37400  token: 0x600017D
        private System.Void <>m__Finally1() { }
        // RVA: 0x0A899EC4  token: 0x600017F
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200004B  // size: 0x90
    public sealed class <_YieldSendHttpRequest>d__29 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Misc.ObjectRef resultRef;  // 0x20
        public System.Type respType;  // 0x28
        public Beyond.Network.HttpSender.FSendFunc sendFunc;  // 0x30
        private Beyond.Network.HttpSender.Result <result>5__2;  // 0x88

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000181
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000182
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x035A3EF0  token: 0x6000183
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0A899F88  token: 0x6000185
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200004C  // size: 0x18
    public sealed class <>c__DisplayClass30_0
    {
        // Fields
        public Beyond.Network.HttpSender.Result result;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000187
        public System.Void .ctor() { }
        // RVA: 0x0A8999E4  token: 0x6000188
        private Beyond.Network.HttpSender.Result <_WaitForSendHttpRequest>b__0() { }

    }

    // TypeToken: 0x200004D
    public sealed class <>c__DisplayClass31_0`1
    {
        // Fields
        public Beyond.Network.HttpSender.Result<TResp> result;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000189
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600018A
        private Beyond.Network.HttpSender.Result<TResp> <_WaitForSendHttpRequest>b__0() { }

    }

    // TypeToken: 0x200004F
    public sealed class <GetEnumerator>d__18 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private T <>2__current;  // 0x0
        public Beyond.Network.CircularBuffer<T> <>4__this;  // 0x0
        private System.ArraySegment<T>[] <>7__wrap1;  // 0x0
        private System.Int32 <>7__wrap2;  // 0x0
        private System.ArraySegment<T> <segment>5__4;  // 0x0
        private System.Int32 <i>5__5;  // 0x0

        // Properties
        T System.Collections.Generic.IEnumerator<T>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60001A7
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x60001A8
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x60001A9
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x60001AB
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000051  // size: 0x20
    public sealed class <>c__DisplayClass64_0
    {
        // Fields
        public Beyond.Network.HGNetBaseSession <>4__this;  // 0x10
        public Beyond.Network.INetIO tcpIO;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60001E8
        public System.Void .ctor() { }
        // RVA: 0x0480FC50  token: 0x60001E9
        private System.Void <ConnectAsync>b__0(System.Threading.Tasks.Task<System.Threading.Tasks.Task> task) { }

    }

    // TypeToken: 0x2000052  // size: 0x28
    public sealed class <>c__DisplayClass77_0
    {
        // Fields
        public Beyond.Network.HGNetBaseSession <>4__this;  // 0x10
        public Beyond.Network.INetIO tcp;  // 0x18
        public System.Int32 nextid;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x60001EA
        public System.Void .ctor() { }
        // RVA: 0x048AC9A0  token: 0x60001EB
        private System.Threading.Tasks.Task <StartTCPMsgLoopThreadTask>b__0() { }

    }

    // TypeToken: 0x2000053  // size: 0x50
    public sealed struct <_SessionThread>d__79 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public Beyond.Network.HGNetBaseSession <>4__this;  // 0x30
        public Beyond.Network.INetIO tcp;  // 0x38
        public System.Int32 threadID;  // 0x40
        private System.Runtime.CompilerServices.TaskAwaiter <>u__1;  // 0x48

        // Methods
        // RVA: 0x0314B1D0  token: 0x60001EC
        private virtual System.Void MoveNext() { }
        // RVA: 0x0459ADF0  token: 0x60001ED
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000054  // size: 0x48
    public sealed struct <_DoReconnectAsync>d__89 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public Beyond.Network.HGNetBaseSession <>4__this;  // 0x30
        public Beyond.Network.INetIO tcpIO;  // 0x38
        private System.Runtime.CompilerServices.TaskAwaiter<System.Boolean> <>u__1;  // 0x40

        // Methods
        // RVA: 0x03813060  token: 0x60001EE
        private virtual System.Void MoveNext() { }
        // RVA: 0x0459AE50  token: 0x60001EF
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000055  // size: 0x30
    public sealed class <>c__DisplayClass90_0
    {
        // Fields
        public Beyond.Network.INetIO tcpIO;  // 0x10
        public System.String ip;  // 0x18
        public System.Int32 port;  // 0x20
        public System.Int32 sendTimeout;  // 0x24
        public System.Int32 receiveBufferLen;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x60001F0
        public System.Void .ctor() { }
        // RVA: 0x040EC030  token: 0x60001F1
        private System.Void <TryConnectWithTimeout>b__0() { }

    }

    // TypeToken: 0x2000056  // size: 0x68
    public sealed struct <TryConnectWithTimeout>d__90 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Boolean> <>t__builder;  // 0x18
        public Beyond.Network.INetIO tcpIO;  // 0x30
        public System.String ip;  // 0x38
        public System.Int32 port;  // 0x40
        public System.Int32 sendTimeout;  // 0x44
        public System.Int32 receiveBufferLen;  // 0x48
        public System.Int32 timeoutMilliseconds;  // 0x4c
        public Beyond.Network.HGNetBaseSession <>4__this;  // 0x50
        private System.Threading.Tasks.Task <connectTask>5__2;  // 0x58
        private System.Runtime.CompilerServices.TaskAwaiter<System.Threading.Tasks.Task> <>u__1;  // 0x60

        // Methods
        // RVA: 0x038EBD80  token: 0x60001F2
        private virtual System.Void MoveNext() { }
        // RVA: 0x048BB330  token: 0x60001F3
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000057  // size: 0x18
    public sealed class <>c__DisplayClass91_0
    {
        // Fields
        public Beyond.Network.INetIO tcpIO;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60001F4
        public System.Void .ctor() { }
        // RVA: 0x03994490  token: 0x60001F5
        private System.Void <TryCloseTcpIOWithTimeout>b__0() { }

    }

    // TypeToken: 0x2000058  // size: 0x58
    public sealed struct <TryCloseTcpIOWithTimeout>d__91 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Boolean> <>t__builder;  // 0x18
        public Beyond.Network.INetIO tcpIO;  // 0x30
        public System.Int32 timeoutMilliseconds;  // 0x38
        public Beyond.Network.HGNetBaseSession <>4__this;  // 0x40
        private System.Threading.Tasks.Task <closeTask>5__2;  // 0x48
        private System.Runtime.CompilerServices.TaskAwaiter<System.Threading.Tasks.Task> <>u__1;  // 0x50

        // Methods
        // RVA: 0x038EC0B0  token: 0x60001F6
        private virtual System.Void MoveNext() { }
        // RVA: 0x048BB3A0  token: 0x60001F7
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000061  // size: 0x20
    public class LoginErrorInfo
    {
        // Fields
        private System.Int32 <code>k__BackingField;  // 0x10
        private System.String <details>k__BackingField;  // 0x18

        // Properties
        System.Int32 code { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        System.String details { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x03A4B4A0  token: 0x600023E
        public System.Void SetErrorInvaild() { }
        // RVA: 0x0A8A5BBC  token: 0x600023F
        public System.Void SetErrorConnectError() { }
        // RVA: 0x0A8A5C48  token: 0x6000240
        public System.Void SetErrorConnectTimeOut() { }
        // RVA: 0x03A4AE80  token: 0x6000241
        public System.Void Clear() { }
        // RVA: 0x0A8A5CD4  token: 0x6000242
        public System.Void Set(System.Int32 code, System.String details) { }
        // RVA: 0x03A4B450  token: 0x6000243
        public System.Void .ctor() { }

    }

namespace Beyond.Network
{

    // TypeToken: 0x2000008
    public class DoubleQueue`1
    {
        // Fields
        private System.Collections.Generic.Queue<T> m_readQueue;  // 0x0
        private System.Collections.Generic.Queue<T> m_writeQueue;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000007
        public System.Void LockingSwap() { }
        // RVA: -1  // not resolved  token: 0x6000008
        public System.Void TryLockingSwap() { }
        // RVA: -1  // not resolved  token: 0x6000009
        public System.Void Enqueue(T item) { }
        // RVA: -1  // not resolved  token: 0x600000A
        public System.Boolean TryDequeue(T& item) { }
        // RVA: -1  // not resolved  token: 0x600000B
        public System.Void EnqueueRange(System.Collections.Generic.List<T> list) { }
        // RVA: -1  // not resolved  token: 0x600000C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x14
    public sealed struct ENetWeakNetPreset
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Network.ENetWeakNetPreset Disabled;  // const
        public static Beyond.Network.ENetWeakNetPreset LightJitter;  // const
        public static Beyond.Network.ENetWeakNetPreset NormalWifiLag;  // const
        public static Beyond.Network.ENetWeakNetPreset PoorMobileNetwork;  // const
        public static Beyond.Network.ENetWeakNetPreset SevereLag;  // const

    }

    // TypeToken: 0x200000A  // size: 0x30
    public class HGTcpClient : System.IDisposable
    {
        // Fields
        private System.Net.Sockets.Socket m_ClientSocket;  // 0x10
        private System.Boolean m_Active;  // 0x18
        private System.Net.Sockets.NetworkStream m_DataStream;  // 0x20
        private System.Net.Sockets.AddressFamily m_Family;  // 0x28
        private System.Boolean m_CleanedUp;  // 0x2c

        // Properties
        System.Net.Sockets.Socket Client { get; /* RVA: 0x031442C0 */ set; /* RVA: 0x040EC940 */ }
        System.Boolean Active { get; /* RVA: 0x0A894B1C */ set; /* RVA: 0x0A894D88 */ }
        System.Int32 Available { get; /* RVA: 0x0314B080 */ }
        System.Boolean Connected { get; /* RVA: 0x0314AFE0 */ }
        System.Boolean ExclusiveAddressUse { get; /* RVA: 0x0A894B64 */ set; /* RVA: 0x0A894DDC */ }
        System.Int32 ReceiveBufferSize { get; /* RVA: 0x0A894C18 */ set; /* RVA: 0x040EC980 */ }
        System.Int32 SendBufferSize { get; /* RVA: 0x0A894CD0 */ set; /* RVA: 0x040EC9F0 */ }
        System.Int32 ReceiveTimeout { get; /* RVA: 0x0A894C74 */ set; /* RVA: 0x0A894EC0 */ }
        System.Int32 SendTimeout { get; /* RVA: 0x0A894D2C */ set; /* RVA: 0x040ECA60 */ }
        System.Net.Sockets.LingerOption LingerState { get; /* RVA: 0x040EC260 */ set; /* RVA: 0x0A894E44 */ }
        System.Boolean NoDelay { get; /* RVA: 0x0A894BB8 */ set; /* RVA: 0x040ECAD0 */ }

        // Methods
        // RVA: 0x040EC880  token: 0x600000D
        public System.Void .ctor() { }
        // RVA: 0x0A89498C  token: 0x600000E
        public System.Void .ctor(System.String hostname, System.Int32 port) { }
        // RVA: 0x0A894AF8  token: 0x600000F
        private System.Void .ctor(System.Net.Sockets.Socket acceptedSocket) { }
        // RVA: 0x040ED980  token: 0x6000018
        public System.Void Connect(System.String hostname, System.Int32 port) { }
        // RVA: 0x0A8946DC  token: 0x6000019
        public System.Void Connect(System.Net.IPAddress address, System.Int32 port) { }
        // RVA: 0x040EE780  token: 0x600001A
        public System.Void Connect(System.Net.IPEndPoint remoteEP) { }
        // RVA: 0x0A894654  token: 0x600001B
        public System.Void Connect(System.Net.IPAddress[] ipAddresses, System.Int32 port) { }
        // RVA: 0x040F20A0  token: 0x600001C
        public System.IAsyncResult BeginConnect(System.String host, System.Int32 port, System.AsyncCallback requestCallback, System.Object state) { }
        // RVA: 0x0A8942D4  token: 0x600001D
        public System.IAsyncResult BeginConnect(System.Net.IPAddress address, System.Int32 port, System.AsyncCallback requestCallback, System.Object state) { }
        // RVA: 0x0A894224  token: 0x600001E
        public System.IAsyncResult BeginConnect(System.Net.IPAddress[] addresses, System.Int32 port, System.AsyncCallback requestCallback, System.Object state) { }
        // RVA: 0x0481CC90  token: 0x600001F
        public System.Void EndConnect(System.IAsyncResult asyncResult) { }
        // RVA: 0x0A894384  token: 0x6000020
        public System.Threading.Tasks.Task ConnectAsync(System.Net.IPAddress address, System.Int32 port) { }
        // RVA: 0x040EBC70  token: 0x6000021
        public System.Threading.Tasks.Task ConnectAsync(System.String host, System.Int32 port) { }
        // RVA: 0x0A8944EC  token: 0x6000022
        public System.Threading.Tasks.Task ConnectAsync(System.Net.IPAddress[] addresses, System.Int32 port) { }
        // RVA: 0x03144110  token: 0x6000023
        public System.Net.Sockets.NetworkStream GetStream() { }
        // RVA: 0x03994EA0  token: 0x6000024
        public System.Void Close() { }
        // RVA: 0x039949E0  token: 0x6000025
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x039949A0  token: 0x6000026
        public virtual System.Void Dispose() { }
        // RVA: 0x0A894868  token: 0x6000027
        protected virtual System.Void Finalize() { }
        // RVA: 0x040EC890  token: 0x6000034
        private System.Void _Initialize() { }
        // RVA: 0x0A8948DC  token: 0x6000035
        private System.Int32 _NumericOption(System.Net.Sockets.SocketOptionLevel optionLevel, System.Net.Sockets.SocketOptionName optionName) { }

    }

    // TypeToken: 0x200000B
    public interface INetIO : System.IDisposable
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000036
        public virtual System.Void Connect(System.String serverIp, System.Int32 serverPort, System.Int32 sendTimeout, System.Int32 receiveBufferSize, System.Int32 sendBufferSize) { }
        // RVA: -1  // abstract  token: 0x6000037
        public virtual System.Threading.Tasks.Task ConnectAsync(System.String serverIp, System.Int32 serverPort, System.Int32 sendTimeout, System.Int32 receiveBufferSize, System.Int32 sendBufferSize) { }
        // RVA: -1  // abstract  token: 0x6000038
        public virtual System.Void Close() { }
        // RVA: -1  // abstract  token: 0x6000039
        public virtual System.Void TestCloseNetIO(System.Boolean bIsTestReconnectFailed) { }
        // RVA: -1  // abstract  token: 0x600003A
        public virtual System.Void TestDropNetIO(System.Boolean bIsTestDropSC, System.Boolean bIsTestDropCS) { }
        // RVA: -1  // abstract  token: 0x600003B
        public virtual System.Void SetWeakNetPreset(Beyond.Network.ENetWeakNetPreset preset) { }
        // RVA: -1  // abstract  token: 0x600003C
        public virtual System.Void SetWeakNetCustom(System.Int32 readMinDelayMs, System.Int32 readMaxDelayMs, System.Int32 readMaxTriggerCountPerSecond, System.Int32 writeMinDelayMs, System.Int32 writeMaxDelayMs, System.Int32 writeMaxTriggerCountPerSecond) { }
        // RVA: -1  // abstract  token: 0x600003D
        public virtual Beyond.Network.TcpIOProfilerSnapshot GetProfilerSnapshot() { }
        // RVA: -1  // abstract  token: 0x600003E
        public virtual System.Boolean IsConnected() { }
        // RVA: -1  // abstract  token: 0x600003F
        public virtual System.Int32 ReadData(System.Byte[] buffer, System.Int32 offset, System.Int32 len) { }
        // RVA: -1  // abstract  token: 0x6000040
        public virtual System.Void WriteData(System.Byte[] data, System.Int32 offset, System.Int32 len) { }
        // RVA: -1  // abstract  token: 0x6000041
        public virtual System.Boolean IsAuthed() { }
        // RVA: -1  // abstract  token: 0x6000042
        public virtual System.Void MarkAuthed() { }
        // RVA: -1  // abstract  token: 0x6000043
        public virtual System.Void EnableCryptStream(System.Byte[] key, System.Byte[] iv) { }
        // RVA: -1  // abstract  token: 0x6000044
        public virtual System.Int32 ReadCryptoData(System.Byte[] buffer, System.Int32 offset, System.Int32 len) { }
        // RVA: -1  // abstract  token: 0x6000045
        public virtual System.Void WriteCryptoData(System.Byte[] data, System.Int32 offset, System.Int32 len) { }
        // RVA: -1  // abstract  token: 0x6000046
        public virtual System.Byte[] GetSendHeadBuffer() { }
        // RVA: -1  // abstract  token: 0x6000047
        public virtual System.Byte[] GetSendBuffer() { }
        // RVA: -1  // abstract  token: 0x6000048
        public virtual System.Byte[] GetReceiveBuffer() { }
        // RVA: -1  // abstract  token: 0x6000049
        public virtual System.Int32 GetReceiveBufferOffset() { }
        // RVA: -1  // abstract  token: 0x600004A
        public virtual System.Void ClearReceiveBufferOffset() { }
        // RVA: -1  // abstract  token: 0x600004B
        public virtual System.Void SkipReceiveBufferOffset(System.Int32 len) { }
        // RVA: -1  // abstract  token: 0x600004C
        public virtual System.Net.EndPoint GetEndPoint() { }
        // RVA: -1  // abstract  token: 0x600004D
        public virtual System.Int32 Available() { }

    }

    // TypeToken: 0x200000C  // size: 0x18
    public class NetMsgHandlerAttribute : System.Attribute
    {
        // Fields
        public readonly System.Type msgType;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x600004E
        public System.Void .ctor(System.Type t) { }
        // RVA: 0x040E20A0  token: 0x600004F
        public System.Void .ctor() { }
        // RVA: 0x040B71D0  token: 0x6000050
        public System.Type GetMsgType(System.Reflection.MethodInfo annotatedMethod) { }

    }

    // TypeToken: 0x200000D  // size: 0x80
    public sealed class NetResponseHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x02F288D0  token: 0x6000051
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000052
        public virtual System.Void Invoke(Beyond.Network.NetResponse& msg) { }
        // RVA: 0x0A895E24  token: 0x6000053
        public virtual System.IAsyncResult BeginInvoke(Beyond.Network.NetResponse& msg, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x06E6C454  token: 0x6000054
        public virtual System.Void EndInvoke(Beyond.Network.NetResponse& msg, System.IAsyncResult result) { }

    }

    // TypeToken: 0x200000E
    public sealed class NetMessageHandle`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000055
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000056
        public virtual System.Void Invoke(T message) { }
        // RVA: -1  // runtime  token: 0x6000057
        public virtual System.IAsyncResult BeginInvoke(T message, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000058
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200000F
    public interface INetBusResponseListener
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000059
        public virtual System.Void UpdateMsgTime(Beyond.Network.NetResponse resp) { }

    }

    // TypeToken: 0x2000010  // size: 0x28
    public sealed struct NetRequest
    {
        // Fields
        public System.UInt64 csSeqID;  // 0x10
        public System.Int32 mergeNum;  // 0x18
        public Google.Protobuf.IMessage msg;  // 0x20

    }

    // TypeToken: 0x2000011  // size: 0x30
    public sealed struct NetResponse
    {
        // Fields
        public static Beyond.Network.NetResponse EMPTY;  // static @ 0x0
        public Google.Protobuf.IMessage headMsg;  // 0x10
        public Google.Protobuf.IMessage msgBody;  // 0x18
        public System.Boolean isPacked;  // 0x20
        public System.Int64 recvTimestamp;  // 0x28

        // Properties
        Beyond.Network.HGNetLogger.Header logHead { get; /* RVA: 0x036AF1E0 */ set; /* RVA: 0x04239180 */ }
        Proto.CSHead csHead { get; /* RVA: 0x036AE550 */ set; /* RVA: 0x03A1B170 */ }
        System.Int32 msgId { get; /* RVA: 0x036AE3F0 */ }
        System.UInt64 csSeqId { get; /* RVA: 0x036ADB80 */ }
        System.UInt64 scSeqId { get; /* RVA: 0x036ADA10 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000061
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x10
    public abstract class MessageHandleWrapper
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000062
        public virtual System.Void HandleMessage(Google.Protobuf.IMessage msg) { }
        // RVA: 0x041E1670  token: 0x6000063
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000013
    public class MessageHandleWrapper`1 : Beyond.Network.MessageHandleWrapper
    {
        // Fields
        private readonly Beyond.Network.NetMessageHandle<T> m_handle;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000064
        public System.Void .ctor(Beyond.Network.NetMessageHandle<T> handle) { }
        // RVA: -1  // not resolved  token: 0x6000065
        public virtual System.Void HandleMessage(Google.Protobuf.IMessage msg) { }

    }

    // TypeToken: 0x2000014  // size: 0x60
    public class NetBus : Beyond.ExplicitSingleton`1
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.Network.NetResponseHandler> m_msgHandlers;  // 0x10
        private readonly System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<Beyond.Network.NetBus.HandlerEntry>> m_msgWithSeqNumHandlers;  // 0x18
        private readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.Network.MessageHandleWrapper> m_msgHdlWrappers;  // 0x20
        private Beyond.Network.HGNetSession m_netSession;  // 0x28
        private static System.Collections.Generic.List<Beyond.Network.NetResponseHandler> s_handlersToCall;  // static @ 0x0
        private readonly Beyond.Network.UIBusSender <uiSender>k__BackingField;  // 0x30
        private readonly Beyond.Network.TeleportMaskSender <teleportMaskSender>k__BackingField;  // 0x38
        private readonly Beyond.Network.DisposableBusSender <defaultSender>k__BackingField;  // 0x40
        private readonly Beyond.Network.Special2BusSender <s2Sender>k__BackingField;  // 0x48
        private readonly Beyond.Network.InteractiveBusSender <interactiveSender>k__BackingField;  // 0x50
        private readonly Beyond.Network.FrequencyControlBusSender <frequencyControlSender>k__BackingField;  // 0x58

        // Properties
        Beyond.Network.UIBusSender uiSender { get; /* RVA: 0x011F36E0 */ }
        Beyond.Network.TeleportMaskSender teleportMaskSender { get; /* RVA: 0x04D85A50 */ }
        Beyond.Network.DisposableBusSender defaultSender { get; /* RVA: 0x04D85A60 */ }
        Beyond.Network.Special2BusSender s2Sender { get; /* RVA: 0x04D86200 */ }
        Beyond.Network.InteractiveBusSender interactiveSender { get; /* RVA: 0x04D86270 */ }
        Beyond.Network.FrequencyControlBusSender frequencyControlSender { get; /* RVA: 0x02E56440 */ }
        System.Int32 currentSessionID { get; /* RVA: 0x03A4E9C0 */ }

        // Methods
        // RVA: 0x03BA9780  token: 0x600006D
        public System.Void .ctor() { }
        // RVA: 0x0A895968  token: 0x600006E
        public System.Boolean IsBusBusy() { }
        // RVA: 0x03A4BFC0  token: 0x600006F
        public System.Void Setup(Beyond.Network.HGNetSession session) { }
        // RVA: 0x036AF5E0  token: 0x6000070
        public System.Void Send(Google.Protobuf.IMessage msg) { }
        // RVA: 0x03895000  token: 0x6000071
        public System.Void RegisterMessageHandler(System.Int32 msgId, Beyond.Network.MessageHandleWrapper handler) { }
        // RVA: 0x0A8959B0  token: 0x6000072
        public System.Void UnRegisterMessageHandler(System.Int32 msgId) { }
        // RVA: 0x0A8955F8  token: 0x6000073
        public System.Void HandleMessageTime(Beyond.Network.NetResponse response) { }
        // RVA: 0x0A895678  token: 0x6000074
        public System.Void HandleMessage(Beyond.Network.NetResponse response) { }
        // RVA: 0x036AF640  token: 0x6000075
        private System.Boolean _ValidateSession() { }
        // RVA: 0x03A4BC60  token: 0x6000076
        public System.Void OnNetClientShutDown() { }
        // RVA: 0x03AC4730  token: 0x6000077
        private Beyond.Network.NetBus.ResultHandler _SendWithResult(Google.Protobuf.IMessage msg) { }
        // RVA: 0x0A895B4C  token: 0x6000078
        private System.Void _Listen(System.Int32 msgId, Beyond.Network.NetResponseHandler handler) { }
        // RVA: 0x0A895C80  token: 0x6000079
        private System.Void _UnListen(System.Int32 msgId, Beyond.Network.NetResponseHandler handler) { }
        // RVA: 0x0A895A18  token: 0x600007A
        private System.Object _ListenWithSeqNum(System.Int32 msgId, Beyond.Network.NetResponseHandler callback) { }
        // RVA: 0x03A4F820  token: 0x600007B
        private System.Void _UnListenWithSeqNum(System.Int32 msgId, System.Object token) { }
        // RVA: 0x04D175B0  token: 0x600007C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000018  // size: 0x38
    public class Special2BusSender : Beyond.Network.DisposableBusSender, Beyond.Network.INetBusResponseListener
    {
        // Fields
        public System.Collections.Generic.Dictionary<System.UInt64,System.Double> sendedMsgWithTime;  // 0x20
        public System.Double minSendTime;  // 0x28
        private System.Single m_maxWaitTime;  // 0x30

        // Properties
        System.Boolean isBusBusy { get; /* RVA: 0x03231E70 */ }

        // Methods
        // RVA: 0x03AC4590  token: 0x6000091
        public virtual Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg) { }
        // RVA: 0x0A899144  token: 0x6000092
        public System.Void UpdateStartBlockTime() { }
        // RVA: 0x03A4CA20  token: 0x6000093
        public virtual System.Void OnNetShutDown() { }
        // RVA: 0x0A899038  token: 0x6000094
        public virtual System.Void Dispose() { }
        // RVA: 0x03A4C9B0  token: 0x6000095
        private System.Void _ClearSendTime() { }
        // RVA: 0x0A899098  token: 0x6000096
        public virtual System.Void UpdateMsgTime(Beyond.Network.NetResponse resp) { }
        // RVA: 0x03BA9BD0  token: 0x6000097
        public System.Void .ctor() { }
        // RVA: 0x0A893C28  token: 0x6000098
        public Beyond.Network.NetBus.ResultHandler <>iFixBaseProxy_Send(Google.Protobuf.IMessage P0) { }
        // RVA: 0x0A899090  token: 0x6000099
        public System.Void <>iFixBaseProxy_OnNetShutDown() { }
        // RVA: 0x0A893C20  token: 0x600009A
        public System.Void <>iFixBaseProxy_Dispose() { }

    }

    // TypeToken: 0x2000019  // size: 0x40
    public class InteractiveBusSender : Beyond.Network.Special2BusSender
    {
        // Fields
        private System.Single m_maxWaitTime;  // 0x38

        // Properties
        System.Boolean isBusBusy { get; /* RVA: 0x03231DC0 */ }

        // Methods
        // RVA: 0x05395568  token: 0x600009C
        public System.Void .ctor() { }
        // RVA: 0x0A8955F0  token: 0x600009D
        public System.Boolean <>iFixBaseProxy_get_isBusBusy() { }

    }

    // TypeToken: 0x200001A  // size: 0x20
    public class DisposableBusSender : Sender, System.IDisposable
    {
        // Fields
        private readonly System.Collections.Generic.HashSet<Beyond.Network.NetBus.ResultHandler> m_resultHandlers;  // 0x10
        private System.Boolean <isDisposed>k__BackingField;  // 0x18

        // Properties
        System.Boolean isDisposed { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }

        // Methods
        // RVA: 0x03AC4650  token: 0x60000A0
        public virtual Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg) { }
        // RVA: 0x03A4D540  token: 0x60000A1
        public virtual System.Void OnNetShutDown() { }
        // RVA: 0x0A893494  token: 0x60000A2
        public virtual System.Void Dispose() { }
        // RVA: 0x03A4EA30  token: 0x60000A3
        private System.Void _Clear() { }
        // RVA: 0x03BA9C70  token: 0x60000A4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001B  // size: 0x18
    public class NetworkMaskConfigHolder
    {
        // Fields
        public System.Collections.Generic.Dictionary<Proto.CSMessageID,Proto.SCMessageID> msgs;  // 0x10

        // Methods
        // RVA: 0x03BA9110  token: 0x60000A5
        public System.Void Init() { }
        // RVA: 0x0A89802C  token: 0x60000A6
        private System.Void _LoadJsonConfig(Beyond.Resource.StringPathHash jsonHash) { }
        // RVA: 0x03BAA9E0  token: 0x60000A7
        private System.Void _LoadScriptableObjectConfig() { }
        // RVA: 0x0A897FB4  token: 0x60000A8
        public System.Void Dispose() { }
        // RVA: 0x03BA9AD0  token: 0x60000A9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x30
    public class UIBusSender : Beyond.Network.DisposableBusSender
    {
        // Fields
        private Beyond.Network.NetworkMaskConfigHolder <config>k__BackingField;  // 0x20
        private System.Collections.Generic.HashSet<System.Int32> m_maskKeys;  // 0x28

        // Properties
        Beyond.Network.NetworkMaskConfigHolder config { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x03BA9A00  token: 0x60000AC
        public System.Void .ctor() { }
        // RVA: 0x0A899FD4  token: 0x60000AD
        public virtual System.Void Dispose() { }
        // RVA: 0x0A89A1C8  token: 0x60000AE
        public virtual Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg) { }
        // RVA: 0x03A4C140  token: 0x60000AF
        public virtual System.Void OnNetShutDown() { }
        // RVA: 0x0A893C20  token: 0x60000B0
        public System.Void <>iFixBaseProxy_Dispose() { }
        // RVA: 0x0A893C28  token: 0x60000B1
        public Beyond.Network.NetBus.ResultHandler <>iFixBaseProxy_Send(Google.Protobuf.IMessage P0) { }
        // RVA: 0x0A899090  token: 0x60000B2
        public System.Void <>iFixBaseProxy_OnNetShutDown() { }

    }

    // TypeToken: 0x200001E  // size: 0x28
    public class TeleportMaskSender : Beyond.Network.DisposableBusSender
    {
        // Fields
        private System.Collections.Generic.HashSet<System.Int32> m_maskKeys;  // 0x20

        // Methods
        // RVA: 0x0A89964C  token: 0x60000B7
        public virtual System.Void Dispose() { }
        // RVA: 0x03A4E360  token: 0x60000B8
        public Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg, System.Single delay) { }
        // RVA: 0x03A4C400  token: 0x60000B9
        public virtual System.Void OnNetShutDown() { }
        // RVA: 0x03BA9B50  token: 0x60000BA
        public System.Void .ctor() { }
        // RVA: 0x0A893C20  token: 0x60000BB
        public System.Void <>iFixBaseProxy_Dispose() { }
        // RVA: 0x0A899090  token: 0x60000BC
        public System.Void <>iFixBaseProxy_OnNetShutDown() { }

    }

    // TypeToken: 0x2000020  // size: 0x40
    public class FrequencyControlBusSender : Beyond.Network.DisposableBusSender
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<Proto.CSMessageID,System.Collections.Generic.Queue<System.Double>> m_lastSendTime;  // 0x20
        private System.Collections.Generic.List<Google.Protobuf.IMessage> m_waitingSendMsgs;  // 0x28
        private System.Collections.Generic.List<System.Action<System.Int32>> m_onError;  // 0x30
        private Beyond.TickFunction m_loopId;  // 0x38

        // Methods
        // RVA: 0x0382A7D0  token: 0x60000C2
        public System.Void .ctor() { }
        // RVA: 0x04CCF2E0  token: 0x60000C3
        private System.Void _TrySendWaitingMsg(System.Single deltaTime) { }
        // RVA: 0x0A893640  token: 0x60000C4
        public virtual Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg) { }
        // RVA: 0x0A8936B4  token: 0x60000C5
        public Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg, System.Boolean needAddToWaitingList, System.Action<System.Int32> onError, System.Action onWait) { }
        // RVA: 0x0A89359C  token: 0x60000C6
        public virtual System.Void Dispose() { }
        // RVA: 0x0A893C28  token: 0x60000C7
        public Beyond.Network.NetBus.ResultHandler <>iFixBaseProxy_Send(Google.Protobuf.IMessage P0) { }
        // RVA: 0x0A893C20  token: 0x60000C8
        public System.Void <>iFixBaseProxy_Dispose() { }

    }

    // TypeToken: 0x2000022  // size: 0x28
    public class ServerDesc
    {
        // Fields
        public System.String name;  // 0x10
        public System.String addr;  // 0x18
        public System.Int32 port;  // 0x20

        // Methods
        // RVA: 0x041AB130  token: 0x60000CD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000023  // size: 0x18
    public class ServerList
    {
        // Fields
        public System.Collections.Generic.List<Beyond.Network.ServerDesc> servers;  // 0x10

        // Methods
        // RVA: 0x04D2DF20  token: 0x60000CE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000024  // size: 0x28
    public class ServerConfig
    {
        // Fields
        public System.String serverName;  // 0x10
        public System.String uid;  // 0x18
        public System.Boolean checkDev;  // 0x20

        // Methods
        // RVA: 0x0A898FB4  token: 0x60000CF
        public System.Boolean IsSame(Beyond.Network.ServerConfig other) { }
        // RVA: 0x04D53840  token: 0x60000D0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000025  // size: 0x28
    public class HostData
    {
        // Fields
        public System.String host;  // 0x10
        public System.Int32 port;  // 0x18
        public System.Double delay;  // 0x20

        // Methods
        // RVA: 0x03DA4C40  token: 0x60000D1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000026  // size: 0x18
    public class NetConfig
    {
        // Fields
        public System.Collections.Generic.List<Beyond.Network.HostData> hosts;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60000D2
        public System.Void .ctor(System.Collections.Generic.List<Beyond.Network.HostData> hosts) { }

    }

    // TypeToken: 0x2000027  // size: 0x10
    public static class NetConsts
    {
        // Fields
        public static readonly System.String NETMASK_CONFIG_PATH;  // static @ 0x0
        public static readonly System.String NETMASK_CONFIG_JSON_PATH;  // static @ 0x8
        public static readonly System.String NETMASK_PREFAB_PATH;  // static @ 0x10

        // Methods
        // RVA: 0x04CC95D0  token: 0x60000D3
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000028
    public interface INetworkSystem : Beyond.ILuaCallCSharp
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60000D4
        public virtual System.Void OnReConnect() { }
        // RVA: -1  // abstract  token: 0x60000D5
        public virtual System.Void SendMsg(Google.Protobuf.IMessage msg, System.Action<System.Int32> onError) { }

    }

    // TypeToken: 0x2000029  // size: 0x18
    public class NetMaskConfig
    {
        // Fields
        public System.Collections.Generic.Dictionary<System.String,Beyond.Network.NetMaskConfig.Config> configs;  // 0x10

        // Methods
        // RVA: 0x0A895DAC  token: 0x60000D6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002B  // size: 0x50
    public sealed struct TcpIOProfilerSnapshot
    {
        // Fields
        public System.Int64 readApiCallCount;  // 0x10
        public System.Int64 readApiCallCountPerSecond;  // 0x18
        public System.Int64 writeApiCallCount;  // 0x20
        public System.Int64 writeApiCallCountPerSecond;  // 0x28
        public System.Int64 totalReadBytes;  // 0x30
        public System.Int64 totalWriteBytes;  // 0x38
        public System.Int64 readBytesPerSecond;  // 0x40
        public System.Int64 writeBytesPerSecond;  // 0x48

    }

    // TypeToken: 0x200002C  // size: 0x40
    public sealed struct NetSessionProfilerSnapshot
    {
        // Fields
        public System.Int64 logicalSendPacketCount;  // 0x10
        public System.Int64 logicalSendPacketCountPerSecond;  // 0x18
        public System.Int64 actualSendPacketCount;  // 0x20
        public System.Int64 actualSendPacketCountPerSecond;  // 0x28
        public System.Int64 receivePacketCount;  // 0x30
        public System.Int64 receivePacketCountPerSecond;  // 0x38

    }

    // TypeToken: 0x200002D  // size: 0x80
    public sealed struct NetDevProfilerSnapshot
    {
        // Fields
        public Beyond.Network.TcpIOProfilerSnapshot tcpIO;  // 0x10
        public Beyond.Network.NetSessionProfilerSnapshot session;  // 0x50

    }

    // TypeToken: 0x200002E  // size: 0x10
    public static class NetUtil
    {
        // Fields
        private static System.Boolean s_isRegistedAllMessages;  // static @ 0x0
        public static readonly System.Int32 MAX_RECEIVE_PACK_BODY_SIZE;  // static @ 0x4
        public static readonly System.Int32 MAX_MERGE_PACK_BODY_SIZE;  // static @ 0x8
        public static readonly System.Int32 MAX_SEND_PACK_BODY_SIZE;  // static @ 0xc
        private static System.Collections.Generic.Dictionary<System.Int32,System.Type> s_sc_id2MessageType;  // static @ 0x10
        private static System.Collections.Generic.Dictionary<System.Type,System.Int32> s_sc_messageType2Id;  // static @ 0x18
        private static System.Collections.Generic.Dictionary<System.Type,System.Int32> s_cs_messageType2Id;  // static @ 0x20
        private static System.Byte[] s_decompressedBuffer;  // static @ 0x28

        // Methods
        // RVA: 0x03693A30  token: 0x60000D8
        public static System.Void RegisterSCMessage(System.Int32 msgId, System.Type msgType) { }
        // RVA: 0x036928F0  token: 0x60000D9
        public static System.Void RegisterCSMessage(System.Int32 msgId, System.Type msgType) { }
        // RVA: 0x03895080  token: 0x60000DA
        public static System.Int32 GetSCMsgIdByType(System.Type msgType) { }
        // RVA: 0x0A895FE8  token: 0x60000DB
        public static System.Type GetSCMsgTypeById(System.Int32 msgId) { }
        // RVA: 0x03142260  token: 0x60000DC
        public static System.Int32 GetCSMsgIdByType(System.Type msgType) { }
        // RVA: -1  // generic def  token: 0x60000DD
        private static System.Void _GenMessageName2ValuePair(System.Collections.Generic.Dictionary<System.String,System.Int32> dic) { }
        // RVA: 0x036935C0  token: 0x60000DE
        public static System.Void FastRegisterMessage() { }
        // RVA: 0x03A19EE0  token: 0x60000DF
        public static System.Boolean GetNetMessageFromDataBytes(System.Span<System.Byte> headBytes, System.Byte[] bodyBuffer, System.Int32 offset, System.Int32 bodyLen, System.Byte[] packedbuffer, Beyond.Network.NetResponse& response) { }
        // RVA: 0x03142960  token: 0x60000E0
        public static System.Int32 SerializeHeadMessage(Google.Protobuf.IMessage msg, System.UInt64 upSeqid, System.Int32 bodyLen, System.Int32 crc32, System.Byte[] buffer, System.Int32 offset) { }
        // RVA: 0x0A896098  token: 0x60000E1
        public static System.Int32 SerializeBodyMessage(Google.Protobuf.IMessage msg, System.Byte[] buffer, System.Int32& crc32, System.Int32 offset) { }
        // RVA: 0x04331FF0  token: 0x60000E2
        public static System.Void AutoRegisterMessageHandlers(System.Object target) { }
        // RVA: 0x0A895EA4  token: 0x60000E3
        public static System.Void AutoUnRegisterMessageHandlers(System.Object target) { }
        // RVA: 0x02F2F9F0  token: 0x60000E4
        private static System.Void _ForeachMessageHandlers(System.Object target, System.Action<System.Type,System.Reflection.MethodInfo> doFunc) { }
        // RVA: 0x0404E310  token: 0x60000E5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000031  // size: 0x10
    public static class NetworkFrequencyConfig
    {
        // Fields
        public static Beyond.SerializeFieldDictionary<System.Int32,Beyond.Network.NetworkFrequencyConfig.NetworkFrequency> msgs;  // static @ 0x0

        // Methods
        // RVA: 0x0A896288  token: 0x60000EB
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000033  // size: 0x60
    public class NetworkMask : Beyond.Singleton`1
    {
        // Fields
        private Beyond.TickFunction m_preTickFunction;  // 0x10
        private System.Int32 m_nextKey;  // 0x18
        public UnityEngine.GameObject maskObj;  // 0x20
        public UnityEngine.GameObject decoObj;  // 0x28
        private Beyond.Resource.FAssetProxyHandle m_handle;  // 0x30
        private System.Action<System.Boolean> m_onNetMaskChanged;  // 0x48
        private System.Boolean <showingMask>k__BackingField;  // 0x50
        private System.Boolean <showingDeco>k__BackingField;  // 0x51
        private System.Boolean m_isNetReconnecting;  // 0x52
        private static System.String DECO_NAME;  // const
        private System.Collections.Generic.List<Beyond.Network.NetworkMask.MaskInfo> m_masks;  // 0x58

        // Properties
        System.Boolean showingMask { get; /* RVA: 0x04D86800 */ set; /* RVA: 0x04D86840 */ }
        System.Boolean showingDeco { get; /* RVA: 0x04D867F0 */ set; /* RVA: 0x04D86830 */ }

        // Methods
        // RVA: 0x047A6DA0  token: 0x60000F1
        private System.Void .ctor() { }
        // RVA: 0x047A6E40  token: 0x60000F2
        protected System.Void OnInit() { }
        // RVA: 0x0A89869C  token: 0x60000F3
        protected System.Void OnUnInit() { }
        // RVA: 0x0A898738  token: 0x60000F4
        public System.Void Release() { }
        // RVA: 0x0A898968  token: 0x60000F5
        private System.Void _OnSwitchLanguage() { }
        // RVA: 0x047A71B0  token: 0x60000F6
        private System.Void _CreateTickFunction() { }
        // RVA: 0x0A898900  token: 0x60000F7
        private System.Void _ClearTickFunction() { }
        // RVA: 0x03A4CB30  token: 0x60000F8
        public System.Void PreTick(System.Single deltaTime) { }
        // RVA: 0x03A4CF90  token: 0x60000F9
        private System.Int32 _GetKey() { }
        // RVA: 0x0A8985DC  token: 0x60000FA
        public System.Int32 AddMask(Proto.CSMessageID msgId, System.Single timeoutTime) { }
        // RVA: 0x03A4CE80  token: 0x60000FB
        public System.Int32 AddMask(System.String name, System.Single timeoutTime, System.Single delayTime) { }
        // RVA: 0x03A4CA60  token: 0x60000FC
        public System.Void RemoveMask(System.Int32 key) { }
        // RVA: 0x04DA5BC0  token: 0x60000FD
        public System.Void SetIsNetReconnecting(System.Boolean isReconnecting) { }
        // RVA: 0x03A4CBE0  token: 0x60000FE
        private System.Void _UpdateMaskState() { }
        // RVA: 0x04A290A0  token: 0x60000FF
        public System.Void ListenOnNetMaskChanged(System.Action<System.Boolean> action) { }
        // RVA: 0x0A898848  token: 0x6000100
        public System.Void UnListenOnNetMaskChanged(System.Action<System.Boolean> action) { }

    }

    // TypeToken: 0x2000035  // size: 0x28
    public class NetworkMaskConfig : UnityEngine.ScriptableObject
    {
        // Fields
        public Beyond.SerializeFieldDictionary<Proto.CSMessageID,Proto.SCMessageID> msgs;  // 0x18
        public System.Collections.Generic.List<Beyond.Network.NetworkMaskConfig.Config> configs;  // 0x20

        // Methods
        // RVA: 0x04CB5BA0  token: 0x6000104
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000037  // size: 0x10
    public static class NetworkUrls
    {
        // Fields
        public static System.String URL_GET_SPLIT;  // const

        // Methods
        // RVA: 0x03C0DDD0  token: 0x6000106
        public static System.String RemoteConfigUrl() { }
        // RVA: 0x0A898D70  token: 0x6000107
        public static System.String ResourceRootUrlDev(System.String version) { }
        // RVA: 0x03C082B0  token: 0x6000108
        public static System.String ServerListUrl() { }
        // RVA: 0x0A898C40  token: 0x6000109
        public static System.String AgeTipsUrl() { }
        // RVA: 0x0A898CD8  token: 0x600010A
        public static System.String GovUrl() { }
        // RVA: 0x0340BD20  token: 0x600010B
        public static System.String LauncherUrl() { }
        // RVA: 0x0340DC70  token: 0x600010C
        private static System.String _SelectUrl(System.String urlInConfig, Beyond.Network.NetworkUrls.EUrlType urlType) { }
        // RVA: 0x03C082E0  token: 0x600010D
        private static System.String _GetUrlFromLocal(Beyond.Network.NetworkUrls.EUrlType type) { }

    }

    // TypeToken: 0x2000039  // size: 0xA0
    public class TcpIO : Beyond.Network.INetIO, System.IDisposable
    {
        // Fields
        private Beyond.Network.HGTcpClient m_tcpClient;  // 0x10
        protected System.Byte[] m_recvBuffer;  // 0x18
        protected System.Byte[] m_sendHeadBuffer;  // 0x20
        protected System.Byte[] m_sendBuffer;  // 0x28
        protected System.Int32 m_recvBufferOffset;  // 0x30
        private Beyond.XXEnc.XXE1 m_encryptor;  // 0x38
        private Beyond.XXEnc.XXE1 m_decryptor;  // 0x40
        protected System.Boolean m_isAuthed;  // 0x48
        private System.Boolean m_bIsTestDropSC;  // 0x49
        private System.Boolean m_bIsTestDropCS;  // 0x4a
        private readonly System.Object m_readWeakNetLock;  // 0x50
        private readonly System.Object m_writeWeakNetLock;  // 0x58
        private System.Boolean m_bIsTestReconnectFailed;  // 0x60
        private System.Int64 m_readWeakNetWindowStartMs;  // 0x68
        private System.Int64 m_writeWeakNetWindowStartMs;  // 0x70
        private System.Int32 m_readWeakNetTriggerCount;  // 0x78
        private System.Int32 m_writeWeakNetTriggerCount;  // 0x7c
        private Beyond.Network.TcpIO.WeakNetDelayConfig m_readWeakNetConfig;  // 0x80
        private Beyond.Network.TcpIO.WeakNetDelayConfig m_writeWeakNetConfig;  // 0x90
        public static System.Int32 SEND_HEAD_BUFFER_LENGTH;  // const
        public static System.Int32 s_MAX_SEND_SIZE;  // static @ 0x0
        private static readonly System.Random s_random;  // static @ 0x8

        // Methods
        // RVA: 0x040EC0B0  token: 0x600010E
        public virtual System.Void Connect(System.String serverIp, System.Int32 serverPort, System.Int32 sendTimeout, System.Int32 receiveBufferSize, System.Int32 sendBufferSize) { }
        // RVA: 0x040EBE40  token: 0x600010F
        public virtual System.Threading.Tasks.Task ConnectAsync(System.String serverIp, System.Int32 serverPort, System.Int32 sendTimeout, System.Int32 receiveBufferSize, System.Int32 sendBufferSize) { }
        // RVA: 0x031491C0  token: 0x6000110
        public virtual System.Boolean IsAuthed() { }
        // RVA: 0x03219110  token: 0x6000111
        public virtual System.Void MarkAuthed() { }
        // RVA: 0x049E1B50  token: 0x6000112
        public virtual System.Void EnableCryptStream(System.Byte[] key, System.Byte[] iv) { }
        // RVA: 0x0A89927C  token: 0x6000113
        public virtual System.Net.EndPoint GetEndPoint() { }
        // RVA: 0x039944E0  token: 0x6000114
        public virtual System.Void Close() { }
        // RVA: 0x0A8993A8  token: 0x6000115
        public virtual System.Void TestCloseNetIO(System.Boolean bIsTestReconnectFailed) { }
        // RVA: 0x0A899414  token: 0x6000116
        public virtual System.Void TestDropNetIO(System.Boolean bIsTestDropSC, System.Boolean bIsTestDropCS) { }
        // RVA: 0x038EA280  token: 0x6000117
        public virtual System.Void SetWeakNetPreset(Beyond.Network.ENetWeakNetPreset preset) { }
        // RVA: 0x038EA2E0  token: 0x6000118
        public virtual System.Void SetWeakNetCustom(System.Int32 readMinDelayMs, System.Int32 readMaxDelayMs, System.Int32 readMaxTriggerCountPerSecond, System.Int32 writeMinDelayMs, System.Int32 writeMaxDelayMs, System.Int32 writeMaxTriggerCountPerSecond) { }
        // RVA: 0x0314ACA0  token: 0x6000119
        public virtual System.Boolean IsConnected() { }
        // RVA: 0x0A899304  token: 0x600011A
        public virtual Beyond.Network.TcpIOProfilerSnapshot GetProfilerSnapshot() { }
        // RVA: 0x03145500  token: 0x600011B
        public virtual System.Void WriteData(System.Byte[] data, System.Int32 offset, System.Int32 len) { }
        // RVA: 0x03142580  token: 0x600011C
        public virtual System.Void WriteCryptoData(System.Byte[] data, System.Int32 offset, System.Int32 len) { }
        // RVA: 0x03142620  token: 0x600011D
        public virtual System.Byte[] GetSendHeadBuffer() { }
        // RVA: 0x03143910  token: 0x600011E
        public virtual System.Byte[] GetSendBuffer() { }
        // RVA: 0x03149B30  token: 0x600011F
        public virtual System.Byte[] GetReceiveBuffer() { }
        // RVA: 0x03F636B0  token: 0x6000120
        public virtual System.Void ClearReceiveBufferOffset() { }
        // RVA: 0x0314AEB0  token: 0x6000121
        public virtual System.Int32 GetReceiveBufferOffset() { }
        // RVA: 0x03F2E980  token: 0x6000122
        public virtual System.Void SkipReceiveBufferOffset(System.Int32 len) { }
        // RVA: 0x03143E50  token: 0x6000123
        public virtual System.Int32 ReadData(System.Byte[] buffer, System.Int32 offset, System.Int32 len) { }
        // RVA: 0x03143CE0  token: 0x6000124
        public virtual System.Int32 ReadCryptoData(System.Byte[] buffer, System.Int32 offset, System.Int32 len) { }
        // RVA: 0x0314A480  token: 0x6000125
        public virtual System.Int32 Available() { }
        // RVA: 0x03994450  token: 0x6000126
        public virtual System.Void Dispose() { }
        // RVA: 0x038EA4C0  token: 0x6000127
        private static System.Void _ResetWeakNetWindow(System.Int64& windowStartMs, System.Int32& triggerCount) { }
        // RVA: 0x038EA3F0  token: 0x6000128
        private static Beyond.Network.TcpIO.WeakNetDelayConfig _CreateWeakNetDelayConfig(System.Int32 minDelayMs, System.Int32 maxDelayMs, System.Int32 maxTriggerCountPerSecond) { }
        // RVA: 0x0A899484  token: 0x6000129
        private static System.Int32 _NextDelayMs(Beyond.Network.TcpIO.WeakNetDelayConfig config) { }
        // RVA: 0x03144350  token: 0x600012A
        private static System.Void _ApplyWeakNetDelay(System.Object delayLock, Beyond.Network.TcpIO.WeakNetDelayConfig config, System.Int64& windowStartMs, System.Int32& triggerCount) { }
        // RVA: 0x039943D0  token: 0x600012B
        protected virtual System.Void Finalize() { }
        // RVA: 0x03A4B2F0  token: 0x600012C
        public System.Void .ctor() { }
        // RVA: 0x04022150  token: 0x600012D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003B  // size: 0x10
    public static class HttpSender
    {
        // Fields
        public static System.String CONTENT_TYPE_JSON;  // const
        public static System.String CONTENT_ENCODING_GZIP;  // const
        public static System.Int32 ERROR_CODE_TIMEOUT;  // const
        public static System.Int32 ERROR_CODE_DNS_FAILED;  // const
        public static System.Int32 ERROR_CODE_DESERIALIZE;  // const
        public static System.Single TIMEOUT_DEFAULT;  // const
        private static System.Int32 OPT_GET;  // const
        private static System.Int32 OPT_POST;  // const

        // Methods
        // RVA: 0x03C0A480  token: 0x600012E
        public static System.Collections.IEnumerator YieldSendGet(System.String url, Beyond.Network.HttpSender.FGetOptions options, Beyond.Misc.ObjectRef resultRef) { }
        // RVA: -1  // generic def  token: 0x600012F
        public static System.Collections.IEnumerator YieldSendGet(System.String url, Beyond.Network.HttpSender.FGetOptions options, Beyond.Misc.ObjectRef resultRef) { }
        // RVA: 0x03C07F10  token: 0x6000130
        public static System.Collections.IEnumerator YieldSendGet(System.String url, Beyond.Network.HttpSender.FGetOptions options, System.Type respType, Beyond.Misc.ObjectRef resultRef) { }
        // RVA: 0x0A8951D4  token: 0x6000131
        public static System.Collections.IEnumerator YieldSendPost(System.String url, Beyond.Network.HttpSender.FPostOptions options, Beyond.Misc.ObjectRef resultRef) { }
        // RVA: -1  // generic def  token: 0x6000132
        public static System.Collections.IEnumerator YieldSendPost(System.String url, Beyond.Network.HttpSender.FPostOptions options, Beyond.Misc.ObjectRef resultRef) { }
        // RVA: 0x049E2670  token: 0x6000133
        public static System.Collections.IEnumerator YieldSendPost(System.String url, Beyond.Network.HttpSender.FPostOptions options, System.Type respType, Beyond.Misc.ObjectRef resultRef) { }
        // RVA: 0x0A8950D8  token: 0x6000134
        public static Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> WaitForSendPost(System.String url, Beyond.Network.HttpSender.FPostOptions options) { }
        // RVA: 0x0A894F3C  token: 0x6000135
        public static Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> WaitForSendGet(System.String url) { }
        // RVA: 0x0A894FDC  token: 0x6000136
        public static Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> WaitForSendGet(System.String url, Beyond.Network.HttpSender.FGetOptions options) { }
        // RVA: -1  // generic def  token: 0x6000137
        public static Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result<TResp>> WaitForSendPost(System.String url, Beyond.Network.HttpSender.FPostOptions options) { }
        // RVA: -1  // generic def  token: 0x6000138
        public static Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result<TResp>> WaitForSendGet(System.String url, Beyond.Network.HttpSender.FGetOptions options) { }
        // RVA: 0x03C08390  token: 0x6000139
        public static System.Int32 GetGeneralErrorCode(Beyond.Network.HttpSender.Result result, System.Boolean& hasError) { }
        // RVA: 0x035A40C0  token: 0x600013A
        private static System.Collections.IEnumerator _YieldSendHttpRequest(Beyond.Network.HttpSender.Result result, Beyond.Network.HttpSender.FSendFunc sendFunc) { }
        // RVA: 0x046BAD90  token: 0x600013B
        private static System.Collections.IEnumerator _YieldSendHttpRequest(Beyond.Misc.ObjectRef resultRef, Beyond.Network.HttpSender.FSendFunc sendFunc, System.Type respType) { }
        // RVA: 0x0A895428  token: 0x600013C
        private static Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> _WaitForSendHttpRequest(Beyond.Network.HttpSender.FSendFunc sendFunc) { }
        // RVA: -1  // generic def  token: 0x600013D
        private static Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result<TResp>> _WaitForSendHttpRequest(Beyond.Network.HttpSender.FSendFunc sendFunc) { }
        // RVA: 0x035A3740  token: 0x600013E
        private static System.Void _StartWebRequestTask(Beyond.Network.HttpSender.Result result, Beyond.Network.HttpSender.FSendFunc sendFunc) { }
        // RVA: 0x04B88010  token: 0x600013F
        private static System.Void _WaitForServiceDone(Beyond.Network.HttpSender.Result result) { }
        // RVA: 0x03E37670  token: 0x6000140
        private static UnityEngine.Networking.UnityWebRequest _CreateWebRequestGET(System.String url, Beyond.Network.HttpSender.FGetOptions options) { }
        // RVA: 0x03E34BA0  token: 0x6000141
        private static UnityEngine.Networking.UnityWebRequest _CreateWebRequestPOST(System.String url, Beyond.Network.HttpSender.FPostOptions options) { }
        // RVA: 0x03E37480  token: 0x6000142
        private static System.Void _SetHttpResult(Beyond.Network.HttpSender.Result result, UnityEngine.Networking.UnityWebRequest request) { }
        // RVA: 0x035A4580  token: 0x6000143
        private static System.Void _DeserializeTypedResult(Beyond.Network.HttpSender.TypedResult result) { }
        // RVA: 0x0A8952BC  token: 0x6000144
        private static System.Void _DebugOnlyProfileResult(Beyond.Network.HttpSender.Result result, Beyond.Network.HttpSender.FSendFunc sendFunc) { }
        // RVA: 0x0A895354  token: 0x6000145
        private static System.Void _MarkResultAsException(Beyond.Network.HttpSender.Result result, System.Exception ex) { }

    }

    // TypeToken: 0x200004E
    public class CircularBuffer`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IReadOnlyList`1, System.Collections.Generic.IReadOnlyCollection`1
    {
        // Fields
        private readonly T[] m_buffer;  // 0x0
        private System.Int32 m_end;  // 0x0
        private System.Int32 m_count;  // 0x0
        private System.Int32 m_start;  // 0x0

        // Properties
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsFull { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsEmpty { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600018B
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x600018C
        public System.Void .ctor(System.Int32 capacity, T[] items) { }
        // RVA: -1  // not resolved  token: 0x6000193
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000194
        public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000195
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000196
        public T Front() { }
        // RVA: -1  // not resolved  token: 0x6000197
        public System.Boolean TryFront(T& result) { }
        // RVA: -1  // not resolved  token: 0x6000198
        public T Back() { }
        // RVA: -1  // not resolved  token: 0x6000199
        public System.Boolean TryBack(T& result) { }
        // RVA: -1  // not resolved  token: 0x600019A
        public System.Void PushBack(T item) { }
        // RVA: -1  // not resolved  token: 0x600019B
        public System.Void PushFront(T item) { }
        // RVA: -1  // not resolved  token: 0x600019C
        public System.Void PopBack() { }
        // RVA: -1  // not resolved  token: 0x600019D
        public System.Void PopFront() { }
        // RVA: -1  // not resolved  token: 0x600019E
        public System.Boolean TryPopFront(T& result) { }
        // RVA: -1  // not resolved  token: 0x600019F
        public System.Boolean TryPopBack(T& result) { }
        // RVA: -1  // not resolved  token: 0x60001A0
        public T[] ToArray() { }
        // RVA: -1  // not resolved  token: 0x60001A1
        private System.Void _ThrowIfEmpty(System.String message) { }
        // RVA: -1  // not resolved  token: 0x60001A2
        private System.Void _Increment(System.Int32& index) { }
        // RVA: -1  // not resolved  token: 0x60001A3
        private System.Void _Decrement(System.Int32& index) { }
        // RVA: -1  // not resolved  token: 0x60001A4
        private System.Int32 _InternalIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60001A5
        private System.ArraySegment<T> _ArrayOne() { }
        // RVA: -1  // not resolved  token: 0x60001A6
        private System.ArraySegment<T> _ArrayTwo() { }

    }

    // TypeToken: 0x2000050  // size: 0xE0
    public abstract class HGNetBaseSession
    {
        // Fields
        private Beyond.Network.INetIO m_tcpIO;  // 0x10
        private Beyond.Network.ENetWeakNetPreset m_weakNetPreset;  // 0x18
        private System.Boolean m_useCustomWeakNetConfig;  // 0x1c
        private System.Int32 m_readWeakNetMinDelayMs;  // 0x20
        private System.Int32 m_readWeakNetMaxDelayMs;  // 0x24
        private System.Int32 m_readWeakNetMaxTriggerCountPerSecond;  // 0x28
        private System.Int32 m_writeWeakNetMinDelayMs;  // 0x2c
        private System.Int32 m_writeWeakNetMaxDelayMs;  // 0x30
        private System.Int32 m_writeWeakNetMaxTriggerCountPerSecond;  // 0x34
        public static System.Int32 s_recvBuffLen;  // static @ 0x0
        public static System.Int32 s_tcpSendTimeout;  // static @ 0x4
        public static System.Int32 s_sessionThreadSleepTimeMS;  // static @ 0x8
        public static System.Int32 s_sessionThreadPauseSleepTimeMS;  // static @ 0xc
        public System.Int32 m_maxReconnectTimeout;  // 0x38
        public System.Int32 m_maxReconnectTimes;  // 0x3c
        public System.Int32 sendNumLimit;  // 0x40
        protected readonly System.Collections.Concurrent.ConcurrentQueue<Beyond.Network.NetResponse> m_recvQueue;  // 0x48
        protected readonly System.Collections.Concurrent.ConcurrentQueue<Beyond.Network.NetRequest> m_sendQueue;  // 0x50
        private static System.Int32 MAX_RECYCLE_QUEUE_COUNT;  // const
        protected Beyond.Network.CircularBuffer<Beyond.Network.NetRequest> m_circularBuffer;  // 0x58
        private System.Int64 m_csSeqId;  // 0x60
        protected System.Int64 m_scSeqId;  // 0x68
        protected System.Threading.Tasks.Task m_threadTask;  // 0x70
        protected System.Int32 m_threadTaskSignalID;  // 0x78
        protected Beyond.ELogChannel m_logChannel;  // 0x7c
        protected System.Boolean m_isSyncFullDataEnd;  // 0x7d
        protected Google.Protobuf.IMessage m_lastLoginMsg;  // 0x80
        private System.String m_lastConnectServerIp;  // 0x88
        private System.Int32 m_lastConnectServerPort;  // 0x90
        protected System.Int32 m_tryReconnectTimes;  // 0x94
        protected System.Boolean m_isAutoReconnect;  // 0x98
        protected System.Int64 m_startReconnectTimestampMS;  // 0xa0
        protected System.Int32 m_closeNetMsgID;  // 0xa8
        public static System.Int32 INVALID_SESSION_ID;  // static @ 0x10
        protected static System.Int32 s_sessionIDGenerator;  // static @ 0x14
        private System.Int32 <sessionID>k__BackingField;  // 0xac
        protected Beyond.Network.HGNetSessionConnectYield m_lastAsyncConnectHandle;  // 0xb0
        protected Beyond.Network.EHGNetSessionState m_state;  // 0xb8
        private Beyond.Network.EHGNetSessionReconnectState m_reconnectState;  // 0xb9
        protected System.Double m_startReconnectTimeMS;  // 0xc0
        protected System.Int32 m_closeErrorCode;  // 0xc8
        public static System.Boolean s_applicationIsPaused;  // static @ 0x18
        private System.Int64 m_clientLastReceiveMsgMilliTs;  // 0xd0
        private System.Int64 m_clientLastSendMsgMilliTs;  // 0xd8

        // Properties
        System.Int32 sessionID { get; /* RVA: 0x04D88CA0 */ set; /* RVA: 0x04D88D00 */ }
        Beyond.Network.EHGNetSessionReconnectState reconnectState { get; /* RVA: 0x0314AA90 */ set; /* RVA: 0x03813C30 */ }
        System.Boolean isRunning { get; /* RVA: 0x036AB820 */ }
        System.Boolean isConnecting { get; /* RVA: 0x0A8941D0 */ }
        System.Boolean isRunningAndConnected { get; /* RVA: 0x036AC050 */ }
        System.Boolean isReconnecting { get; /* RVA: 0x036AB0B0 */ }
        System.Int32 closeErrorCode { get; /* RVA: 0x0A894130 */ }
        System.Boolean isAutoReconnect { get; /* RVA: 0x0A894180 */ }
        System.Int32 maxRecycleQueueCount { get; /* RVA: 0x03142230 */ }

        // Methods
        // RVA: 0x03812B00  token: 0x60001B8
        public virtual System.Void Start() { }
        // RVA: 0x0A893C30  token: 0x60001B9
        public virtual System.Boolean Connect(System.String serverIp, System.Int32 serverPort, System.Boolean forceReconnect) { }
        // RVA: 0x038EB980  token: 0x60001BA
        public virtual System.Boolean ConnectAsync(System.String serverIp, System.Int32 serverPort, System.Boolean forceReconnect, Beyond.Network.HGNetSessionConnectYield& yieldHandle) { }
        // RVA: 0x0480FC80  token: 0x60001BB
        public System.Void OnConnectAsyncEnd(System.Threading.Tasks.Task completedTask, Beyond.Network.INetIO tcpIO) { }
        // RVA: -1  // abstract  token: 0x60001BC
        protected virtual System.Void OnConnectedSucceed(Beyond.Network.INetIO tcpIO) { }
        // RVA: 0x040EE260  token: 0x60001BD
        private System.Void _DNSCheck(System.String serverIp) { }
        // RVA: 0x039941E0  token: 0x60001BE
        public virtual System.Void Close(Beyond.Network.ENetCloseReason netCloseReason) { }
        // RVA: 0x0A893FE0  token: 0x60001BF
        protected System.Void InternalClose(Beyond.Network.INetIO tcpIO, Beyond.Network.ENetCloseReason netCloseReason) { }
        // RVA: 0x03994220  token: 0x60001C0
        protected System.Void InternalClose(Beyond.Network.INetIO tcpIO, System.Int32 netCloseReason) { }
        // RVA: 0x036AEF10  token: 0x60001C1
        protected System.Void EnqueueSendMsg(Beyond.Network.NetRequest msg) { }
        // RVA: 0x036AD6A0  token: 0x60001C2
        public System.Int32 GetSendQueueMsgCount() { }
        // RVA: 0x036AEED0  token: 0x60001C3
        protected System.Int64 IncrementCSSeqID() { }
        // RVA: 0x03219140  token: 0x60001C4
        protected System.Void SetCSSeqID(System.Int64 id) { }
        // RVA: 0x0A893F90  token: 0x60001C5
        protected System.Int64 GetCSSeqID() { }
        // RVA: 0x03219080  token: 0x60001C6
        protected Beyond.Network.INetIO GetIO() { }
        // RVA: 0x040EB970  token: 0x60001C7
        protected System.Void StartTCPMsgLoopThreadTask() { }
        // RVA: 0x03994F30  token: 0x60001C8
        protected System.Void CloseTCPMsgThreadTask() { }
        // RVA: 0x048AC9D0  token: 0x60001C9
        private System.Threading.Tasks.Task _SessionThread(Beyond.Network.INetIO tcp, System.Int32 threadID) { }
        // RVA: 0x031480E0  token: 0x60001CA
        private System.Boolean _SendAndCheckMessage(Beyond.Network.INetIO tcp, System.Int32 threadID) { }
        // RVA: 0x03148890  token: 0x60001CB
        private System.Boolean _ReadAndCheckMessageInSessionThread(Beyond.Network.INetIO tcp, System.Int32 threadID) { }
        // RVA: -1  // abstract  token: 0x60001CC
        protected virtual System.Boolean ReadMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& resp) { }
        // RVA: -1  // abstract  token: 0x60001CD
        protected virtual System.Void CheckAndDealRespMessages(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& m) { }
        // RVA: -1  // abstract  token: 0x60001CE
        protected virtual System.Void CheckAndWriteReqMsg(Beyond.Network.INetIO netIO, Beyond.Network.NetRequest& m) { }
        // RVA: 0x0314A640  token: 0x60001CF
        protected System.Void SessionCheckSignalAndSleep(System.Int32 threadID) { }
        // RVA: 0x040EB530  token: 0x60001D0
        protected System.Void OpenAutoReconnect() { }
        // RVA: 0x03994F00  token: 0x60001D1
        protected System.Void RemoveAutoReconnect() { }
        // RVA: 0x0314CF30  token: 0x60001D2
        private System.Threading.Tasks.Task _ReconnectCheckerTick(Beyond.Network.INetIO tcpIO) { }
        // RVA: 0x047CEC70  token: 0x60001D3
        private System.Threading.Tasks.Task _DoReconnectAsync(Beyond.Network.INetIO tcpIO) { }
        // RVA: 0x03813800  token: 0x60001D4
        protected System.Threading.Tasks.Task<System.Boolean> TryConnectWithTimeout(Beyond.Network.INetIO tcpIO, System.String ip, System.Int32 port, System.Int32 sendTimeout, System.Int32 receiveBufferLen, System.Int32 timeoutMilliseconds) { }
        // RVA: 0x03813970  token: 0x60001D5
        protected System.Threading.Tasks.Task<System.Boolean> TryCloseTcpIOWithTimeout(Beyond.Network.INetIO tcpIO, System.Int32 timeoutMilliseconds) { }
        // RVA: 0x038EA1D0  token: 0x60001D6
        public System.Void SetWeakNetPreset(Beyond.Network.ENetWeakNetPreset preset) { }
        // RVA: 0x0A89405C  token: 0x60001D7
        public System.Void SetWeakNetCustom(System.Int32 readMinDelayMs, System.Int32 readMaxDelayMs, System.Int32 readMaxTriggerCountPerSecond, System.Int32 writeMinDelayMs, System.Int32 writeMaxDelayMs, System.Int32 writeMaxTriggerCountPerSecond) { }
        // RVA: 0x038EA220  token: 0x60001D8
        private System.Void _ApplyWeakNetConfigToTcpIO(Beyond.Network.INetIO tcpIO) { }
        // RVA: -1  // abstract  token: 0x60001D9
        protected virtual System.Void OnReconnectTimesOver(Beyond.Network.INetIO tcpIO) { }
        // RVA: -1  // abstract  token: 0x60001DA
        protected virtual System.Boolean NeedKeepConnect() { }
        // RVA: -1  // abstract  token: 0x60001DB
        protected virtual System.Void DoReconnectLogin(Beyond.Network.INetIO netIO) { }
        // RVA: -1  // abstract  token: 0x60001DC
        protected virtual System.Void InitClientRSAKey() { }
        // RVA: -1  // abstract  token: 0x60001DD
        protected virtual System.Void ClearRSAKey() { }
        // RVA: -1  // abstract  token: 0x60001DE
        public virtual System.Void ClearRecvQueue() { }
        // RVA: -1  // abstract  token: 0x60001DF
        public virtual System.Void ClearSendQueue() { }
        // RVA: 0x03148FE0  token: 0x60001E0
        protected System.Void NetStatusChecker_Receive(Beyond.Network.INetIO netIO, System.Int64 scSeqId, System.Int32 receiveCount) { }
        // RVA: 0x03148680  token: 0x60001E1
        protected System.Void NetStatusChecker_Sender(Beyond.Network.INetIO netIO, System.Int64 csSeqId, System.Int32 sendCount) { }
        // RVA: -1  // abstract  token: 0x60001E2
        protected virtual System.Void SessionConnectTimeoutCheckerTick(Beyond.Network.INetIO netIO, System.Int64 clientLastReceiveMsgMilliTs, System.Int64 clientLastSendMsgMilliTs) { }
        // RVA: -1  // abstract  token: 0x60001E3
        protected virtual System.Void ClearPingWDG() { }
        // RVA: -1  // abstract  token: 0x60001E4
        protected virtual System.Boolean IsPingOut() { }
        // RVA: -1  // abstract  token: 0x60001E5
        protected virtual System.Void PingSenderTick(Beyond.Network.INetIO netIO, System.Int64 upSeqId) { }
        // RVA: 0x03A4B180  token: 0x60001E6
        protected System.Void .ctor() { }
        // RVA: 0x04D11BD0  token: 0x60001E7
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000059  // size: 0x188
    public class HGNetSession : Beyond.Network.HGNetBaseSession
    {
        // Fields
        private System.String m_uid;  // 0xe0
        private System.String m_token;  // 0xe8
        private System.Int32 m_channel;  // 0xf0
        public static System.Int32 LOGIN_TIMEOUT;  // static @ 0x0
        public static System.Int32 MAX_CLIENT_RESEND_NUM;  // static @ 0x4
        public static System.Single PER_FRAME_RECEIVED_MESSAGES_TIME_30FPS;  // static @ 0x8
        public static System.Single PER_FRAME_RECEIVED_MESSAGES_TIME;  // static @ 0xc
        public static System.Single MESSAGE_MAX_DELAY_TIME;  // static @ 0x10
        protected System.Int32 m_messageDelayCount;  // 0xf4
        protected System.Int64 m_logCDTimestamp;  // 0xf8
        private static System.Boolean s_HGNetSessionBurst;  // static @ 0x14
        private readonly System.Collections.Concurrent.ConcurrentQueue<Beyond.Network.NetRequest> m_frameSendQueue;  // 0x100
        private System.Collections.Generic.Queue<Beyond.Network.NetRequest> m_mergeQueueCache;  // 0x108
        private System.Object m_frameSendQueueLocker;  // 0x110
        private System.Byte[] m_frameSendPacketBuffer;  // 0x118
        private System.Byte[] m_frameSendPacketHeadBuffer;  // 0x120
        private System.Byte[] m_frameSendPacketTempBuffer;  // 0x128
        private System.Byte[] m_frameSendCryptoBuffer;  // 0x130
        private System.Byte[] m_frameReceivePacketBuffer;  // 0x138
        public static System.Int64 s_maxframeSendPacketBuffer;  // static @ 0x18
        public static System.Int64 s_maxPackedMessageNum;  // static @ 0x20
        protected readonly System.Collections.Generic.Queue<Beyond.Network.NetResponse> m_callbackQueue;  // 0x140
        private Beyond.Network.HGNetSessionLoginYield m_currLoginHandle;  // 0x148
        private static System.Int32 SEND_PING_DELTA_TICK_TIME_MS;  // const
        private static System.Int32 IN_PING_PROGRESS_MAX_TIME;  // const
        private static System.Int32 s_pingWdgOutTimes;  // static @ 0x28
        private static System.Int32 s_pingSyncFullEndBeforeTimes;  // static @ 0x2c
        private System.Int64 m_needSendPingTimer;  // 0x150
        private System.Int32 m_pingWDG;  // 0x158
        private System.Int64 m_clientTsDelayms;  // 0x160
        private System.Int32 m_pingValue;  // 0x168
        private System.Boolean m_isPingInProgress;  // 0x16c
        private System.Int64 m_isPingInProgressStartTime;  // 0x170
        private static readonly System.String CLIENT_NET_PEM_PUBLIC_KEY_HEAD_FORMAT;  // static @ 0x30
        private static readonly System.String CLIENT_NET_RSA_PUBLIC_KEY;  // static @ 0x38
        private static readonly System.String CLIENT_NET_RSA_PRIVATE_KEY;  // static @ 0x40
        private static System.Byte[] s_clientPublicKeyBytes;  // static @ 0x48
        private System.Security.Cryptography.RSA m_rsaClientPrivateProvider;  // 0x178
        private static readonly System.Byte[] EMPTY_BYTES;  // static @ 0x50
        private static System.Int32 s_CacheLogLength;  // static @ 0x58
        private System.Text.StringBuilder m_sbCache;  // 0x180

        // Methods
        // RVA: 0x03A4BBE0  token: 0x60001F8
        public System.Void SetAutoReconnectToken(System.String uid, System.String token, System.Int32 channel) { }
        // RVA: 0x03CCC6E0  token: 0x60001F9
        protected virtual System.Void SessionConnectTimeoutCheckerTick(Beyond.Network.INetIO netIO, System.Int64 clientLastReceiveMsgMilliTs, System.Int64 clientLastSendMsgMilliTs) { }
        // RVA: 0x0A89D01C  token: 0x60001FA
        protected virtual System.Void DoReconnectLogin(Beyond.Network.INetIO netIO) { }
        // RVA: 0x04A42F40  token: 0x60001FB
        public virtual System.Void Start() { }
        // RVA: 0x0A89D938  token: 0x60001FC
        public System.Void OnApplicationPause(System.Boolean pause) { }
        // RVA: 0x036AF240  token: 0x60001FD
        public System.UInt64 SendMessage(Google.Protobuf.IMessage msg) { }
        // RVA: 0x0A89DA94  token: 0x60001FE
        protected System.Void PackMessageToSendQueue() { }
        // RVA: 0x0A89E23C  token: 0x60001FF
        private System.Void _FlushMergeQueueToSendQueue() { }
        // RVA: 0x0A89E1D0  token: 0x6000200
        public System.Void UpdateInGameThread() { }
        // RVA: 0x0A89E198  token: 0x6000201
        public System.Void TailLateUpdateInGameThread() { }
        // RVA: 0x0A89E314  token: 0x6000202
        private System.Single _GetMessageTimeLimit() { }
        // RVA: 0x0A89ED38  token: 0x6000203
        private System.Void _ProcessReceivedMessagesInGameThread(System.Boolean flushAll) { }
        // RVA: 0x0379D830  token: 0x6000204
        public Beyond.Network.HGNetSessionLoginYield LoginAsync(System.String uid, System.String token, System.Int32 channel) { }
        // RVA: 0x03C07910  token: 0x6000205
        private static System.Void _CSLoginHelper_UpdateAppInfo(Proto.MSG_A1 msg) { }
        // RVA: 0x03C07A90  token: 0x6000206
        private static System.String _GetOnlineResVersion() { }
        // RVA: 0x03C07850  token: 0x6000207
        private static Proto.ENV_TYPE _GetSDKEnv() { }
        // RVA: 0x03C07FF0  token: 0x6000208
        private static Proto.CLIENT_PLATFORM_TYPE _GetPlatformID() { }
        // RVA: 0x03C08020  token: 0x6000209
        private static System.Int32 _GetPayPlatform() { }
        // RVA: 0x03218D00  token: 0x600020A
        private System.Void _SessionLoginThreadTask(System.Object handle) { }
        // RVA: 0x040EB480  token: 0x600020B
        public System.Void StartMsgLoop() { }
        // RVA: 0x04D802C0  token: 0x600020C
        protected virtual System.Void OnConnectedSucceed(Beyond.Network.INetIO tcpIO) { }
        // RVA: 0x0A89DA28  token: 0x600020D
        protected virtual System.Void OnReconnectTimesOver(Beyond.Network.INetIO tcpIO) { }
        // RVA: 0x03CCC7A0  token: 0x600020E
        protected virtual System.Boolean NeedKeepConnect() { }
        // RVA: 0x03142110  token: 0x600020F
        protected virtual System.Void CheckAndWriteReqMsg(Beyond.Network.INetIO netIO, Beyond.Network.NetRequest& req) { }
        // RVA: 0x03142650  token: 0x6000210
        private System.Void _WriteMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetRequest& request, System.Boolean useSRSA) { }
        // RVA: 0x03148010  token: 0x6000211
        protected virtual System.Boolean ReadMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& resp) { }
        // RVA: 0x03149BD0  token: 0x6000212
        private System.Boolean _ReadMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& resp, System.Boolean useSRSA) { }
        // RVA: 0x036AB3C0  token: 0x6000213
        public virtual System.Void ClearRecvQueue() { }
        // RVA: 0x04D32C80  token: 0x6000214
        public virtual System.Void ClearSendQueue() { }
        // RVA: 0x0A89E144  token: 0x6000215
        public static System.Void SetBurstMode(System.Boolean val) { }
        // RVA: 0x0A89D804  token: 0x6000216
        public Beyond.Network.NetDevProfilerSnapshot GetNetDevProfilerSnapshot() { }
        // RVA: 0x03993B40  token: 0x6000217
        protected virtual System.Void Finalize() { }
        // RVA: 0x036ABAA0  token: 0x6000218
        public System.Int32 GetPing() { }
        // RVA: 0x0A89D8E8  token: 0x6000219
        public System.Boolean IsPingInProgress() { }
        // RVA: 0x039F3270  token: 0x600021A
        public System.Int64 GetPingPassedTime() { }
        // RVA: 0x0425AD80  token: 0x600021B
        protected virtual System.Void ClearPingWDG() { }
        // RVA: 0x03219180  token: 0x600021C
        private System.Void _ApproximateLoginServerTime(System.Int64 serverTimeMS, System.Int32 serverZone) { }
        // RVA: 0x03F7A520  token: 0x600021D
        private System.Boolean _HandleSpecialMessagePing(Beyond.Network.NetResponse m) { }
        // RVA: 0x03B27E90  token: 0x600021E
        protected virtual System.Boolean IsPingOut() { }
        // RVA: 0x03147B20  token: 0x600021F
        protected virtual System.Void PingSenderTick(Beyond.Network.INetIO netIO, System.Int64 upSeqId) { }
        // RVA: 0x04D1E2F0  token: 0x6000220
        public static System.Byte[] GetClientPublicKey() { }
        // RVA: 0x03FE3D40  token: 0x6000221
        protected virtual System.Void InitClientRSAKey() { }
        // RVA: 0x032198D0  token: 0x6000222
        private System.Byte[] _RSADecrypt(System.Byte[] data) { }
        // RVA: 0x0A89CF54  token: 0x6000223
        protected virtual System.Void ClearRSAKey() { }
        // RVA: 0x036AE080  token: 0x6000224
        protected virtual System.Void CheckAndDealRespMessages(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& msg) { }
        // RVA: 0x0A89E624  token: 0x6000225
        private System.Boolean _HandleSpecialMessageReconnectIncr(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse m) { }
        // RVA: 0x0A89E474  token: 0x6000226
        private System.Boolean _HandleSpecialMessageReconnectFull(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse m) { }
        // RVA: 0x0A89E394  token: 0x6000227
        private System.Boolean _HandleSpecialMessageErrorCode(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse m) { }
        // RVA: 0x0A89EB80  token: 0x6000228
        private System.Boolean _HandleSpecialMessageReconnectLogin(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse m) { }
        // RVA: 0x032195B0  token: 0x6000229
        private System.Boolean _HandleLoginEncryp(Beyond.Network.INetIO netIO, Proto.MSG_B1 msg) { }
        // RVA: 0x03A4AEF0  token: 0x600022A
        public System.Void .ctor() { }
        // RVA: 0x04A24C30  token: 0x600022B
        private static System.Void .cctor() { }
        // RVA: 0x0A89CA18  token: 0x600022C
        public System.Void <>iFixBaseProxy_Start() { }

    }

    // TypeToken: 0x200005A  // size: 0x11
    public sealed struct EHGNetSessionState
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.Network.EHGNetSessionState Closed;  // const
        public static Beyond.Network.EHGNetSessionState Opened;  // const
        public static Beyond.Network.EHGNetSessionState Connecting;  // const
        public static Beyond.Network.EHGNetSessionState SessionRunning;  // const

    }

    // TypeToken: 0x200005B  // size: 0x11
    public sealed struct EHGNetSessionReconnectState
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.Network.EHGNetSessionReconnectState None;  // const
        public static Beyond.Network.EHGNetSessionReconnectState ReconnectingTcp;  // const
        public static Beyond.Network.EHGNetSessionReconnectState SendLogin;  // const
        public static Beyond.Network.EHGNetSessionReconnectState ResendPack;  // const

    }

    // TypeToken: 0x200005C  // size: 0x14
    public sealed struct ENetCloseReason
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Network.ENetCloseReason Invalid;  // const
        public static Beyond.Network.ENetCloseReason None;  // const
        public static Beyond.Network.ENetCloseReason ForServerErrorMax;  // const
        public static Beyond.Network.ENetCloseReason ConnectFailed;  // const
        public static Beyond.Network.ENetCloseReason ServerReconnectFailed;  // const
        public static Beyond.Network.ENetCloseReason TooMuchClientReconnectPack;  // const
        public static Beyond.Network.ENetCloseReason ServerReconnectFullPack;  // const
        public static Beyond.Network.ENetCloseReason ClientShutDown;  // const
        public static Beyond.Network.ENetCloseReason ClientLaunch;  // const
        public static Beyond.Network.ENetCloseReason ClientReconnectTooMuchTimes;  // const

    }

    // TypeToken: 0x200005D  // size: 0x10
    public static class HGNetSessionUtils
    {
        // Methods
        // RVA: 0x0A89CEEC  token: 0x600022D
        public static System.Boolean CheckErrorNeedsDisconnect(Proto.CODE errorCode) { }

    }

    // TypeToken: 0x200005E  // size: 0x10
    public static class HGNetSessionConstant
    {
        // Fields
        public static System.Int32 CODE_SUC;  // const
        public static System.Int32 CODE_CONN_FAILED;  // const
        public static System.Int32 CODE_CONN_WAIT;  // const

    }

    // TypeToken: 0x200005F  // size: 0x14
    public sealed struct ENetConnectErrorCode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Network.ENetConnectErrorCode Invalid;  // const
        public static Beyond.Network.ENetConnectErrorCode None;  // const
        public static Beyond.Network.ENetConnectErrorCode ForServerErrorMax;  // const
        public static Beyond.Network.ENetConnectErrorCode ConnectError;  // const
        public static Beyond.Network.ENetConnectErrorCode TimeOut;  // const

    }

    // TypeToken: 0x2000060  // size: 0x38
    public class HGNetSessionLoginYield : UnityEngine.CustomYieldInstruction
    {
        // Fields
        private Beyond.Network.HGNetSessionLoginYield.LoginErrorInfo m_loginErrorInfo;  // 0x10
        private System.Boolean m_bIsFinish;  // 0x18
        private Proto.MSG_A1 m_request;  // 0x20
        private Proto.MSG_B1 m_resp;  // 0x28
        private System.Object m_locker;  // 0x30

        // Properties
        System.Boolean isSucceed { get; /* RVA: 0x03A4BB20 */ }
        System.Boolean keepWaiting { get; /* RVA: 0x04CC5CD0 */ }

        // Methods
        // RVA: 0x03A4B3A0  token: 0x600022E
        public System.Void .ctor() { }
        // RVA: 0x03A4ADC0  token: 0x600022F
        public System.Void .ctor(Proto.MSG_A1 req) { }
        // RVA: 0x032190B0  token: 0x6000230
        public Proto.MSG_A1 GetRequest() { }
        // RVA: 0x03A4B870  token: 0x6000231
        public Proto.MSG_B1 GetResponse() { }
        // RVA: 0x0A89CB24  token: 0x6000232
        public System.Int32 GetErrorCode() { }
        // RVA: 0x0A89CB7C  token: 0x6000233
        public System.String GetErrorDetails() { }
        // RVA: 0x03A4ABD0  token: 0x6000234
        public System.Void SetSucceed(Proto.MSG_B1 sc) { }
        // RVA: 0x0A89CCCC  token: 0x6000235
        public System.Void SetNtfError(System.Int32 ntfErrorCode, System.String ntfErrorDetails) { }
        // RVA: 0x0A89CBD4  token: 0x6000236
        public System.Void SetConnectErrorError() { }
        // RVA: 0x0A89CDF4  token: 0x6000237
        public System.Void SetTimeOutError() { }

    }

    // TypeToken: 0x2000062  // size: 0x20
    public class HGNetSessionConnectYield : UnityEngine.CustomYieldInstruction
    {
        // Fields
        private System.Threading.Tasks.Task m_connectTask;  // 0x10
        private Beyond.Network.ENetConnectErrorCode connectErrorCode;  // 0x18

        // Properties
        System.Boolean isSucceed { get; /* RVA: 0x03A4B840 */ }
        System.Boolean keepWaiting { get; /* RVA: 0x04CE44F0 */ }

        // Methods
        // RVA: 0x04D87910  token: 0x6000244
        public System.Void .ctor() { }
        // RVA: 0x038EA930  token: 0x6000245
        public System.Void .ctor(System.Threading.Tasks.Task connectTask) { }
        // RVA: 0x0A89CAD8  token: 0x6000246
        public System.Int32 GetCode() { }
        // RVA: 0x0480FDD0  token: 0x6000247
        public System.Void SetFinish(Beyond.Network.ENetConnectErrorCode code) { }

    }

}

namespace Beyond.Network.HGNetLogger
{

    // TypeToken: 0x2000063  // size: 0x98
    public abstract class HGNetLogBaseSession
    {
        // Fields
        private readonly Beyond.Network.INetIO m_tcpIO;  // 0x10
        public static System.Int32 s_recvBuffLen;  // static @ 0x0
        public static System.Int32 s_tcpSendTimeout;  // static @ 0x4
        public static System.Int32 s_sessionThreadSleepTimeMS;  // static @ 0x8
        public static System.Int32 s_sessionThreadPauseSleepTimeMS;  // static @ 0xc
        public static System.Int32 s_maxReconnectTimeout;  // static @ 0x10
        public static System.Int32 s_maxReconnectTimes;  // static @ 0x14
        protected System.Int32 m_sendNumLimit;  // 0x18
        protected readonly System.Collections.Concurrent.ConcurrentQueue<Beyond.Network.NetResponse> m_recvQueue;  // 0x20
        private readonly System.Collections.Concurrent.ConcurrentQueue<Beyond.Network.NetRequest> m_sendQueue;  // 0x28
        private static System.Int32 MAX_RECYCLE_QUEUE_COUNT;  // const
        protected Beyond.Network.CircularBuffer<Beyond.Network.NetRequest> m_circularBuffer;  // 0x30
        private System.Int64 m_csSeqId;  // 0x38
        protected System.Int64 m_scSeqId;  // 0x40
        protected System.Threading.Tasks.Task m_threadTask;  // 0x48
        protected System.Int32 m_threadTaskSignal;  // 0x50
        protected Beyond.ELogChannel m_logChannel;  // 0x54
        private System.String m_lastConnectServerIp;  // 0x58
        private System.Int32 m_lastConnectServerPort;  // 0x60
        protected System.Int32 m_tryReconnectTimes;  // 0x64
        protected System.Int64 m_startReconnectTimestampMS;  // 0x68
        protected System.Boolean m_isAutoReconnect;  // 0x70
        protected Beyond.Network.HGNetSessionConnectYield m_lastAsyncConnectHandle;  // 0x78
        protected Beyond.Network.EHGNetSessionState m_state;  // 0x80
        protected Beyond.Network.EHGNetSessionReconnectState m_reconnectState;  // 0x81
        protected System.Int32 m_closeErrorCode;  // 0x84
        public static System.Boolean s_applicationIsPaused;  // static @ 0x18
        private System.Int64 m_clientLastReceiveMsgMilliTs;  // 0x88
        private System.Int64 m_clientLastSendMsgMilliTs;  // 0x90

        // Properties
        System.Boolean isRunning { get; /* RVA: 0x0A89C518 */ }
        System.Boolean isConnecting { get; /* RVA: 0x0A89C470 */ }
        System.Boolean isRunningAndConnected { get; /* RVA: 0x0A89C4C4 */ }
        System.Int32 closeErrorCode { get; /* RVA: 0x0A89C3D4 */ }
        System.Boolean isAutoReconnect { get; /* RVA: 0x0A89C424 */ }

        // Methods
        // RVA: 0x0A89B840  token: 0x600024F
        public virtual System.Void Start() { }
        // RVA: 0x0A89ACF0  token: 0x6000250
        public virtual System.Boolean Connect(System.String serverIp, System.Int32 serverPort, System.Boolean forceReconnect) { }
        // RVA: 0x0A89A954  token: 0x6000251
        public virtual System.Boolean ConnectAsync(System.String serverIp, System.Int32 serverPort, System.Boolean forceReconnect, Beyond.Network.HGNetSessionConnectYield& yieldHandle) { }
        // RVA: 0x0A89B384  token: 0x6000252
        public System.Void OnConnectAsyncEnd(System.Threading.Tasks.Task continuationAction) { }
        // RVA: -1  // abstract  token: 0x6000253
        protected virtual System.Void OnConnectedSucceed(Beyond.Network.INetIO tcpIO) { }
        // RVA: 0x0A89B988  token: 0x6000254
        private System.Void _DNSCheck(System.String serverIp) { }
        // RVA: 0x0A89A6F0  token: 0x6000255
        public virtual System.Void Close(Beyond.Network.ENetCloseReason netCloseReason) { }
        // RVA: 0x0A89A754  token: 0x6000256
        public virtual System.Void Close(System.Int32 netCloseReason) { }
        // RVA: 0x0A89AFDC  token: 0x6000257
        protected System.Void EnqueueSendMsg(Beyond.Network.NetRequest msg) { }
        // RVA: 0x0A89B130  token: 0x6000258
        public System.Int32 GetSendQueueMsgCount() { }
        // RVA: 0x0A89B1AC  token: 0x6000259
        protected System.Int64 IncrementCSSeqID() { }
        // RVA: 0x0A89B620  token: 0x600025A
        protected System.Void SetCSSeqID(System.Int64 id) { }
        // RVA: 0x0A89B090  token: 0x600025B
        protected System.Int64 GetCSSeqID() { }
        // RVA: 0x0A89B0E0  token: 0x600025C
        protected Beyond.Network.INetIO GetIO() { }
        // RVA: 0x0A89B67C  token: 0x600025D
        protected System.Void StartTCPMsgLoopThreadTask() { }
        // RVA: 0x0A89A610  token: 0x600025E
        protected System.Void CloseTCPMsgThreadTask() { }
        // RVA: 0x0A89BE08  token: 0x600025F
        private System.Void _SessionThread() { }
        // RVA: -1  // abstract  token: 0x6000260
        protected virtual System.Boolean ReadMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& resp) { }
        // RVA: -1  // abstract  token: 0x6000261
        protected virtual System.Void CheckAndDealRespMessages(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& m) { }
        // RVA: -1  // abstract  token: 0x6000262
        protected virtual System.Void CheckAndWriteReqMsg(Beyond.Network.INetIO netIO, Beyond.Network.NetRequest& m) { }
        // RVA: 0x0A89B558  token: 0x6000263
        protected System.Void SessionCheckSignalAndSleep() { }
        // RVA: 0x0A89B4BC  token: 0x6000264
        protected System.Void OpenAutoReconnect() { }
        // RVA: 0x0A89B50C  token: 0x6000265
        protected System.Void RemoveAutoReconnect() { }
        // RVA: 0x0A89BB44  token: 0x6000266
        private System.Void _ReconnectCheckerTick() { }
        // RVA: -1  // abstract  token: 0x6000267
        protected virtual System.Void OnReconnectTimesOver() { }
        // RVA: -1  // abstract  token: 0x6000268
        protected virtual System.Boolean NeedKeepConnect() { }
        // RVA: -1  // abstract  token: 0x6000269
        protected virtual System.Void DoReconnectLogin(Beyond.Network.INetIO netIO) { }
        // RVA: -1  // abstract  token: 0x600026A
        protected virtual System.Void InitClientRSAKey() { }
        // RVA: -1  // abstract  token: 0x600026B
        protected virtual System.Void ClearRSAKey() { }
        // RVA: 0x0A89B204  token: 0x600026C
        protected System.Void NetStatusChecker_Receive(Beyond.Network.INetIO netIO, System.Int64 scSeqId, System.Int32 receiveCount) { }
        // RVA: 0x0A89B2B8  token: 0x600026D
        protected System.Void NetStatusChecker_Sender(Beyond.Network.INetIO netIO, System.Int64 csSeqId, System.Int32 sendCount) { }
        // RVA: -1  // abstract  token: 0x600026E
        protected virtual System.Void SessionConnectTimeoutCheckerTick(Beyond.Network.INetIO netIO, System.Int64 clientLastReceiveMsgMilliTs, System.Int64 clientLastSendMsgMilliTs) { }
        // RVA: -1  // abstract  token: 0x600026F
        protected virtual System.Void ClearPingWDG() { }
        // RVA: -1  // abstract  token: 0x6000270
        protected virtual System.Boolean IsPingOut() { }
        // RVA: -1  // abstract  token: 0x6000271
        protected virtual System.Void PingSenderTick(Beyond.Network.INetIO netIO, System.Int64 upSeqId) { }
        // RVA: 0x0A89C27C  token: 0x6000272
        protected System.Void .ctor() { }
        // RVA: 0x0A89C1C8  token: 0x6000273
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000064  // size: 0x38
    public class HGNetLogger : Beyond.Log.ILogger
    {
        // Fields
        private Beyond.Log.BitArray256 m_enabledLogChannel;  // 0x10
        private System.Collections.Concurrent.ConcurrentQueue<Beyond.Log.FLogMessage> m_logMessagesQueue;  // 0x30
        public static System.Int32 s_maxLogQueueSize;  // static @ 0x0
        public static System.Int32 s_maxRepeatedMsgMs;  // static @ 0x4
        public static System.Int32 s_maxRepeatedMsgCount;  // static @ 0x8

        // Methods
        // RVA: 0x03136140  token: 0x6000274
        public virtual System.Void Log(Beyond.Log.FLogMessage& msg) { }
        // RVA: 0x03F7B160  token: 0x6000275
        public static Beyond.Network.HGNetLogger.LogLevel ConvertFromDLogger(Beyond.ELogLevel level) { }
        // RVA: 0x03F7B050  token: 0x6000276
        public static Beyond.Network.HGNetLogger.NativeLogLevel ConvertFromDLoggerV2(Beyond.ELogLevel level) { }
        // RVA: 0x03F5AE50  token: 0x6000277
        public System.Boolean CheckLog(Beyond.Log.FLogMessage& msg) { }
        // RVA: 0x048FE070  token: 0x6000278
        public System.Void Clear() { }
        // RVA: 0x0A89CA20  token: 0x6000279
        public System.Void SetLogChannel(System.UInt64 channel1, System.UInt64 channel2, System.UInt64 channel3, System.UInt64 channel4) { }
        // RVA: 0x040329F0  token: 0x600027A
        public static System.Int32 ConvertFromDLogger(Beyond.ELogChannel channel) { }
        // RVA: 0x03E3E0C0  token: 0x600027B
        public System.Void .ctor() { }
        // RVA: 0x04D53380  token: 0x600027C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000065  // size: 0x30
    public class HGNetLoggerClient
    {
        // Fields
        private static Beyond.Network.HGNetLogger.HGNetLoggerClient s_instance;  // static @ 0x0
        private static System.Int64 <s_roleID>k__BackingField;  // static @ 0x8
        public Beyond.Network.HGNetLogger.HGNetLoggerSession m_logsession;  // 0x10
        private Beyond.Network.HGNetLogger.HGNetLogger m_netLogger;  // 0x18
        public static readonly System.Int32 MAX_LOG_SEND_QUEUE_SIZE;  // static @ 0x10
        private static System.Int32 MAX_LOCAL_LOG_QUEUE_SIZE;  // const
        private readonly System.Collections.Generic.Queue<Beyond.Log.FLogMessage> m_localLogQueue;  // 0x20
        private readonly System.Object m_queueLock;  // 0x28
        public static Beyond.Network.HGNetLogger.Version s_usedVersion;  // static @ 0x14

        // Properties
        Beyond.Network.HGNetLogger.HGNetLoggerClient instance { get; /* RVA: 0x03A84130 */ }
        System.Int64 s_roleID { get; /* RVA: 0x0A89C634 */ set; /* RVA: 0x0A89C684 */ }

        // Methods
        // RVA: 0x04CDEC20  token: 0x600027D
        protected System.Void .ctor() { }
        // RVA: 0x03E3DA80  token: 0x6000281
        public System.Void Init(System.String uid, System.String token, System.String env, System.String roleId) { }
        // RVA: 0x0313A4C0  token: 0x6000282
        public System.Void AddLog(System.String message, System.String stacktrace, UnityEngine.LogType type) { }
        // RVA: 0x03139630  token: 0x6000283
        private System.Void _FlushLocalLogQueue() { }
        // RVA: 0x0A89C56C  token: 0x6000284
        public static System.Void OnApplicationPause(System.Boolean pauseStatus) { }
        // RVA: 0x03C11B10  token: 0x6000285
        public System.Void ShutDown() { }
        // RVA: 0x03993C20  token: 0x6000286
        private System.Void _CloseAndClear(Beyond.Network.ENetCloseReason reason) { }
        // RVA: 0x036AE800  token: 0x6000287
        public System.Void WriteNetLog(Beyond.Network.HGNetLogger.LogMessage& log) { }
        // RVA: 0x04CED270  token: 0x6000288
        public static System.Void SetRoleID(System.UInt64 roleID) { }
        // RVA: 0x04D802F0  token: 0x6000289
        public System.Void SetNetLogMaskFromServer(Google.Protobuf.Collections.RepeatedField<System.UInt64> masks) { }
        // RVA: 0x04D5DD70  token: 0x600028A
        private static System.Void .cctor() { }
        // RVA: 0x048FE050  token: 0x600028B
        private System.Void <Init>b__15_0() { }

    }

    // TypeToken: 0x2000066  // size: 0x30
    public class HGNetLoggerCloudSettings
    {
        // Fields
        public static readonly Beyond.Network.HGNetLogger.HGNetLoggerCloudSettings instance;  // static @ 0x0
        private Beyond.Network.HGNetLogger.LogControl m_cloudLogControl;  // 0x10
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> m_cloudChannelLevelMaskTables;  // 0x18
        private Beyond.Network.HGNetLogger.LogControl m_localLogControl;  // 0x20
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> m_localChannelLevelMaskTables;  // 0x28
        private static System.Collections.Generic.List<Beyond.Network.HGNetLogger.NativeLogLevel> s_allLevels;  // static @ 0x8

        // Methods
        // RVA: 0x03A82D30  token: 0x600028C
        public static System.Void SetCloudLogControl(Beyond.Network.HGNetLogger.AuthResponse control, Beyond.Network.HGNetBaseSession session) { }
        // RVA: 0x0313A3F0  token: 0x600028D
        public static System.Boolean IsNetLogLevelEnabled(Beyond.ELogLevel elevel, Beyond.ELogChannel channel) { }
        // RVA: 0x0333BE50  token: 0x600028E
        public static System.Boolean IsNetLogLevelEnabled(Beyond.Network.HGNetLogger.NativeLogLevel level, System.Int32 channel) { }
        // RVA: 0x0A89C6DC  token: 0x600028F
        public static System.Boolean IsLocalLogLevelEnabled(Beyond.ELogLevel elevel, Beyond.ELogChannel channel) { }
        // RVA: 0x0313A470  token: 0x6000290
        public static Beyond.Network.HGNetLogger.NativeLogLevel ConvertFromDLogger(Beyond.ELogLevel level) { }
        // RVA: 0x049C5F00  token: 0x6000291
        public System.Void .ctor() { }
        // RVA: 0x049C5DA0  token: 0x6000292
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000067  // size: 0x118
    public class HGNetLoggerSession : Beyond.Network.HGNetBaseSession
    {
        // Fields
        private System.Byte[] m_keyBytes;  // 0xe0
        private System.Collections.Generic.Dictionary<System.String,System.Int32> m_templeCRCDict;  // 0xe8
        private System.Action m_onReconnectSuccess;  // 0xf0
        private System.String m_uid;  // 0xf8
        private System.String m_roleId;  // 0x100
        private System.String m_token;  // 0x108
        private System.String m_env;  // 0x110
        private static System.Collections.Generic.Dictionary<System.Type,Beyond.Network.HGNetLogger.MessageType> s_m_messageType2Id;  // static @ 0x0
        private static System.Collections.Generic.Dictionary<Beyond.Network.HGNetLogger.MessageType,System.Type> s_m_messageId2Type;  // static @ 0x8

        // Properties
        System.String uid { get; /* RVA: 0x04D7D910 */ }
        System.String roleId { get; /* RVA: 0x04D7D940 */ }
        System.Collections.Generic.Dictionary<System.Type,Beyond.Network.HGNetLogger.MessageType> s_messageType2Id { get; /* RVA: 0x03142E60 */ }
        System.Collections.Generic.Dictionary<Beyond.Network.HGNetLogger.MessageType,System.Type> s_messageId2Type { get; /* RVA: 0x041CE8B0 */ }

        // Methods
        // RVA: 0x03142E00  token: 0x6000297
        public static System.Boolean TryGetMsgTypeByID(System.Type msgType, Beyond.Network.HGNetLogger.MessageType& type) { }
        // RVA: 0x038125C0  token: 0x6000298
        public virtual System.Void Start() { }
        // RVA: 0x03E3E070  token: 0x6000299
        public System.Void SetReconnectSuccessCallback(System.Action onReconnectSuccess) { }
        // RVA: 0x0A89C85C  token: 0x600029A
        public System.Void OnApplicationPause(System.Boolean pause) { }
        // RVA: 0x040EB4D0  token: 0x600029B
        protected virtual System.Void OnConnectedSucceed(Beyond.Network.INetIO tcpIO) { }
        // RVA: 0x03149260  token: 0x600029C
        protected virtual System.Boolean ReadMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& resp) { }
        // RVA: 0x036AEF80  token: 0x600029D
        protected virtual System.Void CheckAndDealRespMessages(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& resp) { }
        // RVA: 0x03142D90  token: 0x600029E
        protected virtual System.Void CheckAndWriteReqMsg(Beyond.Network.INetIO netIO, Beyond.Network.NetRequest& m) { }
        // RVA: 0x031432B0  token: 0x600029F
        private System.Void _WriteMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetRequest request) { }
        // RVA: 0x036AE900  token: 0x60002A0
        public System.UInt64 SendMessage(Google.Protobuf.IMessage msg) { }
        // RVA: 0x03E3E280  token: 0x60002A1
        public System.Void SetAutoReconnectToken(System.String uid, System.String token, System.String env, System.String roleId) { }
        // RVA: 0x036AD550  token: 0x60002A2
        protected virtual System.Boolean NeedKeepConnect() { }
        // RVA: 0x03813B40  token: 0x60002A3
        protected virtual System.Void DoReconnectLogin(Beyond.Network.INetIO netIO) { }
        // RVA: 0x03147870  token: 0x60002A4
        private System.Int32 _GetConnectTimeoutTime() { }
        // RVA: 0x03147910  token: 0x60002A5
        protected virtual System.Void SessionConnectTimeoutCheckerTick(Beyond.Network.INetIO netIO, System.Int64 clientLastReceiveMsgMilliTs, System.Int64 clientLastSendMsgMilliTs) { }
        // RVA: 0x04CD9730  token: 0x60002A6
        protected virtual System.Void ClearPingWDG() { }
        // RVA: 0x03813DF0  token: 0x60002A7
        public System.Void SendLogin(Beyond.Network.INetIO netIO) { }
        // RVA: 0x0423B0F0  token: 0x60002A8
        protected virtual System.Boolean IsPingOut() { }
        // RVA: 0x03DAE9A0  token: 0x60002A9
        protected virtual System.Void PingSenderTick(Beyond.Network.INetIO netIO, System.Int64 upSeqId) { }
        // RVA: 0x04D5ED40  token: 0x60002AA
        protected virtual System.Void InitClientRSAKey() { }
        // RVA: 0x0A89C818  token: 0x60002AB
        protected virtual System.Void ClearRSAKey() { }
        // RVA: 0x04D5EDA0  token: 0x60002AC
        public virtual System.Void ClearRecvQueue() { }
        // RVA: 0x04D5ED70  token: 0x60002AD
        public virtual System.Void ClearSendQueue() { }
        // RVA: 0x0A89C95C  token: 0x60002AE
        protected virtual System.Void OnReconnectTimesOver(Beyond.Network.INetIO tcpIO) { }
        // RVA: 0x03E3E320  token: 0x60002AF
        public System.Void .ctor() { }
        // RVA: 0x0A89CA18  token: 0x60002B0
        public System.Void <>iFixBaseProxy_Start() { }

    }

}

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x2000003  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000003
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000004
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x20
    public class DataNAttribute : System.Attribute
    {
        // Fields
        public System.Int32 capacity;  // 0x10
        public System.Int32 tSize;  // 0x14
        public System.Boolean ring;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000005
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000006
        public System.Void .ctor() { }

    }

}

namespace HG.Rendering.Runtime
{

    // TypeToken: 0x2000002  // size: 0x10
    public class HGConstantBufferLayoutAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x2000068  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x60002B1
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x0A89F0C8  token: 0x60002B2
        public System.Net.Sockets.Socket __Gen_Wrap_0(System.Object P0) { }
        // RVA: 0x06377B18  token: 0x60002B3
        public System.Void __Gen_Wrap_1(System.Object P0, System.Object P1) { }
        // RVA: 0x063632E4  token: 0x60002B4
        public System.Boolean __Gen_Wrap_2(System.Object P0) { }
        // RVA: 0x066C2B40  token: 0x60002B5
        public System.Void __Gen_Wrap_3(System.Object P0, System.Boolean P1) { }
        // RVA: 0x06360E84  token: 0x60002B6
        public System.Int32 __Gen_Wrap_4(System.Object P0) { }
        // RVA: 0x066F4838  token: 0x60002B7
        public System.Void __Gen_Wrap_5(System.Object P0, System.Object P1, System.Int32 P2) { }
        // RVA: 0x0A8A43F4  token: 0x60002B8
        public System.IAsyncResult __Gen_Wrap_6(System.Object P0, System.Object P1, System.Int32 P2, System.Object P3, System.Object P4) { }
        // RVA: 0x0A8A4CF8  token: 0x60002B9
        public System.Threading.Tasks.Task __Gen_Wrap_7(System.Object P0, System.Object P1, System.Int32 P2) { }
        // RVA: 0x0A8A5528  token: 0x60002BA
        public System.Net.Sockets.NetworkStream __Gen_Wrap_8(System.Object P0) { }
        // RVA: 0x063633B4  token: 0x60002BB
        public System.Void __Gen_Wrap_9(System.Object P0) { }
        // RVA: 0x071AFE44  token: 0x60002BC
        public System.Int32 __Gen_Wrap_10(System.Object P0, System.Net.Sockets.SocketOptionLevel P1, System.Net.Sockets.SocketOptionName P2) { }
        // RVA: 0x066F3838  token: 0x60002BD
        public System.Void __Gen_Wrap_11(System.Object P0, System.Int32 P1) { }
        // RVA: 0x0A8A08BC  token: 0x60002BE
        public System.Net.Sockets.LingerOption __Gen_Wrap_12(System.Object P0) { }
        // RVA: 0x0A8A0C78  token: 0x60002BF
        public Beyond.Network.HGNetLogger.Header __Gen_Wrap_13(Beyond.Network.NetResponse& P0) { }
        // RVA: 0x0A8A0DE0  token: 0x60002C0
        public System.Void __Gen_Wrap_14(Beyond.Network.NetResponse& P0, System.Object P1) { }
        // RVA: 0x0A8A0F44  token: 0x60002C1
        public Proto.CSHead __Gen_Wrap_15(Beyond.Network.NetResponse& P0) { }
        // RVA: 0x0A8A10AC  token: 0x60002C2
        public System.Int32 __Gen_Wrap_16(Beyond.Network.NetResponse& P0) { }
        // RVA: 0x0A8A1204  token: 0x60002C3
        public System.UInt64 __Gen_Wrap_17(Beyond.Network.NetResponse& P0) { }
        // RVA: 0x067CDAC0  token: 0x60002C4
        public System.Int64 __Gen_Wrap_18(System.Object P0) { }
        // RVA: 0x0A8A135C  token: 0x60002C5
        public System.UInt64 __Gen_Wrap_19(System.Object P0, System.Object P1) { }
        // RVA: 0x0A8A1448  token: 0x60002C6
        public System.Type __Gen_Wrap_20(System.Int32 P0) { }
        // RVA: 0x066F4C88  token: 0x60002C7
        public System.Void __Gen_Wrap_21(System.Object P0, System.Int32 P1, System.Object P2) { }
        // RVA: 0x0A8A1534  token: 0x60002C8
        public Beyond.Network.NetBus.ResultHandler __Gen_Wrap_22(System.Object P0, System.Object P1) { }
        // RVA: 0x0A8A1640  token: 0x60002C9
        public System.Void __Gen_Wrap_23(System.Object P0, Beyond.Network.NetResponse& P1) { }
        // RVA: 0x0A8A176C  token: 0x60002CA
        public Beyond.Network.NetBus.ResultHandler __Gen_Wrap_24(System.Object P0, Proto.SCMessageID P1, System.Object P2) { }
        // RVA: 0x09C81AB0  token: 0x60002CB
        public System.Void __Gen_Wrap_25(System.Object P0, Proto.SCMessageID P1, System.Object P2) { }
        // RVA: 0x0A8A1890  token: 0x60002CC
        public System.Void __Gen_Wrap_26(System.Object P0, Beyond.Resource.StringPathHash P1) { }
        // RVA: 0x066C0730  token: 0x60002CD
        public System.Single __Gen_Wrap_27(System.Object P0) { }
        // RVA: 0x0A8A1990  token: 0x60002CE
        public System.Int32 __Gen_Wrap_28(System.Object P0, System.Object P1, System.Single P2, System.Single P3) { }
        // RVA: 0x0A8A1AA8  token: 0x60002CF
        public System.Int32 __Gen_Wrap_29(System.Object P0, Proto.CSMessageID P1, System.Single P2) { }
        // RVA: 0x0A8A1B9C  token: 0x60002D0
        public System.Void __Gen_Wrap_30(Beyond.Network.NetResponse& P0) { }
        // RVA: 0x0A8A1C98  token: 0x60002D1
        public Beyond.Network.NetBus.ResultHandler __Gen_Wrap_31(System.Object P0, System.Object P1, System.Single P2) { }
        // RVA: 0x0A8A1DAC  token: 0x60002D2
        public Beyond.Network.NetBus.ResultHandler __Gen_Wrap_32(System.Object P0, System.Object P1, System.Boolean P2, System.Object P3, System.Object P4) { }
        // RVA: 0x066C288C  token: 0x60002D3
        public System.Void __Gen_Wrap_33(System.Object P0, System.Single P1) { }
        // RVA: 0x0A8A1EF4  token: 0x60002D4
        public System.Boolean __Gen_Wrap_34(System.Object P0, System.Object P1) { }
        // RVA: 0x06374CB0  token: 0x60002D5
        public System.Void __Gen_Wrap_35(System.Int32 P0, System.Object P1) { }
        // RVA: 0x063625A4  token: 0x60002D6
        public System.Void __Gen_Wrap_36() { }
        // RVA: 0x0A8A1FCC  token: 0x60002D7
        public System.Boolean __Gen_Wrap_37(System.Span<System.Byte> P0, System.Object P1, System.Int32 P2, System.Int32 P3, System.Object P4, Beyond.Network.NetResponse& P5) { }
        // RVA: 0x0A8A2180  token: 0x60002D8
        public System.Int32 __Gen_Wrap_38(System.Object P0, System.UInt64 P1, System.Int32 P2, System.Int32 P3, System.Object P4, System.Int32 P5) { }
        // RVA: 0x09C8B45C  token: 0x60002D9
        public System.Void __Gen_Wrap_39(System.Type P0, System.Reflection.MethodInfo P1) { }
        // RVA: 0x0A8A22B4  token: 0x60002DA
        public System.String __Gen_Wrap_40() { }
        // RVA: 0x0A8A2380  token: 0x60002DB
        public System.String __Gen_Wrap_41(System.Object P0) { }
        // RVA: 0x0A8A2470  token: 0x60002DC
        public System.String __Gen_Wrap_42(Beyond.Network.NetworkUrls.EUrlType P0) { }
        // RVA: 0x0A8A255C  token: 0x60002DD
        public System.String __Gen_Wrap_43(System.Object P0, Beyond.Network.NetworkUrls.EUrlType P1) { }
        // RVA: 0x0A8A2668  token: 0x60002DE
        public System.Void __Gen_Wrap_44(System.Object P0, System.Object P1, System.Int32 P2, System.Int32 P3, System.Int32 P4, System.Int32 P5) { }
        // RVA: 0x0A8A278C  token: 0x60002DF
        public System.Threading.Tasks.Task __Gen_Wrap_45(System.Object P0, System.Object P1, System.Int32 P2, System.Int32 P3, System.Int32 P4, System.Int32 P5) { }
        // RVA: 0x06364290  token: 0x60002E0
        public System.Void __Gen_Wrap_46(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x0A8A28E0  token: 0x60002E1
        public System.Net.EndPoint __Gen_Wrap_47(System.Object P0) { }
        // RVA: 0x0A8A29BC  token: 0x60002E2
        public System.Void __Gen_Wrap_48(System.Object P0, System.Boolean P1, System.Boolean P2) { }
        // RVA: 0x0A8A2AA0  token: 0x60002E3
        public Beyond.Network.TcpIO.WeakNetDelayConfig __Gen_Wrap_49(System.Int32 P0, System.Int32 P1, System.Int32 P2) { }
        // RVA: 0x0A8A2BD8  token: 0x60002E4
        public System.Void __Gen_Wrap_50(System.Int64& P0, System.Int32& P1) { }
        // RVA: 0x0A8A2CF8  token: 0x60002E5
        public System.Void __Gen_Wrap_51(System.Object P0, System.Int32 P1, System.Int32 P2, System.Int32 P3, System.Int32 P4, System.Int32 P5, System.Int32 P6) { }
        // RVA: 0x066F3838  token: 0x60002E6
        public System.Void __Gen_Wrap_52(System.Object P0, Beyond.Network.ENetWeakNetPreset P1) { }
        // RVA: 0x0A8A2E2C  token: 0x60002E7
        public Beyond.Network.TcpIOProfilerSnapshot __Gen_Wrap_53(System.Object P0) { }
        // RVA: 0x0A8A2F40  token: 0x60002E8
        public System.Int32 __Gen_Wrap_54(Beyond.Network.TcpIO.WeakNetDelayConfig P0) { }
        // RVA: 0x0A8A3048  token: 0x60002E9
        public System.Void __Gen_Wrap_55(System.Object P0, Beyond.Network.TcpIO.WeakNetDelayConfig P1, System.Int64& P2, System.Int32& P3) { }
        // RVA: 0x0A8A31CC  token: 0x60002EA
        public System.Void __Gen_Wrap_56(System.Object P0, System.Object P1, System.Int32 P2, System.Int32 P3) { }
        // RVA: 0x0A8A32D4  token: 0x60002EB
        public System.Byte[] __Gen_Wrap_57(System.Object P0) { }
        // RVA: 0x0A8A33C4  token: 0x60002EC
        public System.Int32 __Gen_Wrap_58(System.Object P0, System.Object P1, System.Int32 P2, System.Int32 P3) { }
        // RVA: 0x0A8A34D8  token: 0x60002ED
        public Beyond.Network.HttpSender.Cancellation __Gen_Wrap_59(Beyond.Network.HttpSender.FSendFunc& P0) { }
        // RVA: 0x0A8A3688  token: 0x60002EE
        public UnityEngine.Networking.UnityWebRequest __Gen_Wrap_60(System.Object P0, Beyond.Network.HttpSender.FGetOptions P1) { }
        // RVA: 0x0A8A37C0  token: 0x60002EF
        public UnityEngine.Networking.UnityWebRequest __Gen_Wrap_61(System.Object P0, Beyond.Network.HttpSender.FPostOptions P1) { }
        // RVA: 0x0A8A38F8  token: 0x60002F0
        public UnityEngine.Networking.UnityWebRequest __Gen_Wrap_62(Beyond.Network.HttpSender.FSendFunc& P0) { }
        // RVA: 0x0A8A3AA8  token: 0x60002F1
        public System.Collections.IEnumerator __Gen_Wrap_63(System.Object P0, Beyond.Network.HttpSender.FSendFunc P1) { }
        // RVA: 0x0A8A3C04  token: 0x60002F2
        public System.Collections.IEnumerator __Gen_Wrap_64(System.Object P0, Beyond.Network.HttpSender.FSendFunc P1, System.Object P2) { }
        // RVA: 0x0A8A3D78  token: 0x60002F3
        public System.Collections.IEnumerator __Gen_Wrap_65(System.Object P0, Beyond.Network.HttpSender.FGetOptions P1, System.Object P2, System.Object P3) { }
        // RVA: 0x0A8A3ED8  token: 0x60002F4
        public System.Collections.IEnumerator __Gen_Wrap_66(System.Object P0, Beyond.Network.HttpSender.FGetOptions P1, System.Object P2) { }
        // RVA: 0x0A8A4028  token: 0x60002F5
        public System.Collections.IEnumerator __Gen_Wrap_67(System.Object P0, Beyond.Network.HttpSender.FPostOptions P1, System.Object P2, System.Object P3) { }
        // RVA: 0x0A8A4188  token: 0x60002F6
        public System.Collections.IEnumerator __Gen_Wrap_68(System.Object P0, Beyond.Network.HttpSender.FPostOptions P1, System.Object P2) { }
        // RVA: 0x0A8A42C8  token: 0x60002F7
        public System.Void __Gen_Wrap_69(System.Object P0, Beyond.Network.HttpSender.FSendFunc P1) { }
        // RVA: 0x0A8A4538  token: 0x60002F8
        public Beyond.Network.HttpSender.Result __Gen_Wrap_70() { }
        // RVA: 0x0A8A45F0  token: 0x60002F9
        public Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> __Gen_Wrap_71(Beyond.Network.HttpSender.FSendFunc P0) { }
        // RVA: 0x0A8A4720  token: 0x60002FA
        public Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> __Gen_Wrap_72(System.Object P0, Beyond.Network.HttpSender.FPostOptions P1) { }
        // RVA: 0x0A8A4848  token: 0x60002FB
        public Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> __Gen_Wrap_73(System.Object P0, Beyond.Network.HttpSender.FGetOptions P1) { }
        // RVA: 0x0A8A4970  token: 0x60002FC
        public Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> __Gen_Wrap_74(System.Object P0) { }
        // RVA: 0x0A8A4A4C  token: 0x60002FD
        public System.Int32 __Gen_Wrap_75(System.Object P0, System.Boolean& P1) { }
        // RVA: 0x0A8A4B58  token: 0x60002FE
        public System.String __Gen_Wrap_76(Beyond.Network.HttpSender.FSendFunc& P0) { }
        // RVA: 0x06360E84  token: 0x60002FF
        public Beyond.Network.EHGNetSessionReconnectState __Gen_Wrap_77(System.Object P0) { }
        // RVA: 0x09E3EDF8  token: 0x6000300
        public System.Void __Gen_Wrap_78(System.Object P0, Beyond.Network.EHGNetSessionReconnectState P1) { }
        // RVA: 0x066F4838  token: 0x6000301
        public System.Void __Gen_Wrap_79(System.Object P0, System.Object P1, Beyond.Network.ENetCloseReason P2) { }
        // RVA: 0x0A8A4E1C  token: 0x6000302
        public System.Boolean __Gen_Wrap_80(System.Object P0, System.Object P1, System.Int32 P2, System.Boolean P3) { }
        // RVA: 0x066F3838  token: 0x6000303
        public System.Void __Gen_Wrap_81(System.Object P0, Beyond.Network.ENetConnectErrorCode P1) { }
        // RVA: 0x066F3838  token: 0x6000304
        public System.Void __Gen_Wrap_82(System.Object P0, Beyond.Network.ENetCloseReason P1) { }
        // RVA: 0x066C296C  token: 0x6000305
        public System.Void __Gen_Wrap_83(System.Threading.Tasks.Task<System.Threading.Tasks.Task> P0) { }
        // RVA: 0x0A8A4F24  token: 0x6000306
        public System.Boolean __Gen_Wrap_84(System.Object P0, System.Object P1, System.Int32 P2, System.Boolean P3, Beyond.Network.HGNetSessionConnectYield& P4) { }
        // RVA: 0x0A8A5094  token: 0x6000307
        public System.Void __Gen_Wrap_85(System.Object P0, Beyond.Network.NetRequest P1) { }
        // RVA: 0x09CD5A30  token: 0x6000308
        public System.Void __Gen_Wrap_86(System.Object P0, System.Int64 P1) { }
        // RVA: 0x0A8A51B0  token: 0x6000309
        public Beyond.Network.INetIO __Gen_Wrap_87(System.Object P0) { }
        // RVA: 0x0A8A52A0  token: 0x600030A
        public System.Threading.Tasks.Task<System.Boolean> __Gen_Wrap_88(System.Object P0, System.Object P1, System.Int32 P2) { }
        // RVA: 0x0A8A53C4  token: 0x600030B
        public System.Threading.Tasks.Task<System.Boolean> __Gen_Wrap_89(System.Object P0, System.Object P1, System.Object P2, System.Int32 P3, System.Int32 P4, System.Int32 P5, System.Int32 P6) { }
        // RVA: 0x0A8A5618  token: 0x600030C
        public System.Threading.Tasks.Task __Gen_Wrap_90(System.Object P0, System.Object P1) { }
        // RVA: 0x0A8A5724  token: 0x600030D
        public System.Void __Gen_Wrap_91(System.Object P0, System.Object P1, System.Int64 P2, System.Int32 P3) { }
        // RVA: 0x0A8A582C  token: 0x600030E
        public System.Boolean __Gen_Wrap_92(System.Object P0, System.Object P1, System.Int32 P2) { }
        // RVA: 0x0A8A5930  token: 0x600030F
        public System.Threading.Tasks.Task __Gen_Wrap_93() { }
        // RVA: 0x0719F418  token: 0x6000310
        public System.Void __Gen_Wrap_94(System.Object P0, System.Object P1, System.Object P2, System.Int32 P3) { }
        // RVA: 0x0A8A59E8  token: 0x6000311
        public System.Void __Gen_Wrap_95(System.Object P0, System.Object P1, System.Int64 P2, System.Int64 P3) { }
        // RVA: 0x0A8A5AF0  token: 0x6000312
        public System.Byte[] __Gen_Wrap_96() { }
        // RVA: 0x063621C4  token: 0x6000313
        public Proto.CLIENT_PLATFORM_TYPE __Gen_Wrap_97() { }
        // RVA: 0x063621C4  token: 0x6000314
        public Proto.ENV_TYPE __Gen_Wrap_98() { }
        // RVA: 0x063621C4  token: 0x6000315
        public System.Int32 __Gen_Wrap_99() { }
        // RVA: 0x0A89F1B8  token: 0x6000316
        public System.Void __Gen_Wrap_100(System.Object P0, System.Object P1, Beyond.Network.NetRequest& P2, System.Boolean P3) { }
        // RVA: 0x0A89F34C  token: 0x6000317
        public System.Boolean __Gen_Wrap_101(System.Object P0, System.Object P1, Beyond.Network.NetResponse& P2, System.Boolean P3) { }
        // RVA: 0x0A89F4BC  token: 0x6000318
        public System.Void __Gen_Wrap_102(System.Object P0, System.Int64 P1, System.Int32 P2) { }
        // RVA: 0x0A89F5B4  token: 0x6000319
        public System.Byte[] __Gen_Wrap_103(System.Object P0, System.Object P1) { }
        // RVA: 0x07195DFC  token: 0x600031A
        public System.Boolean __Gen_Wrap_104(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x0A89F6C0  token: 0x600031B
        public Proto.MSG_A1 __Gen_Wrap_105(System.Object P0) { }
        // RVA: 0x0A89F7B0  token: 0x600031C
        public Beyond.Network.HGNetSessionLoginYield __Gen_Wrap_106(System.Object P0, System.Object P1, System.Object P2, System.Int32 P3) { }
        // RVA: 0x0A89F8E4  token: 0x600031D
        public System.Void __Gen_Wrap_107(System.Object P0, System.Object P1, Beyond.Network.NetRequest& P2) { }
        // RVA: 0x0A89FA68  token: 0x600031E
        public System.Boolean __Gen_Wrap_108(System.Object P0, System.Object P1, Beyond.Network.NetResponse& P2) { }
        // RVA: 0x0A89FBCC  token: 0x600031F
        public Beyond.Network.NetDevProfilerSnapshot __Gen_Wrap_109(System.Object P0) { }
        // RVA: 0x0A89FCF8  token: 0x6000320
        public System.Boolean __Gen_Wrap_110(System.Object P0, Beyond.Network.NetResponse P1) { }
        // RVA: 0x071AEEA0  token: 0x6000321
        public System.Void __Gen_Wrap_111(System.Object P0, System.Object P1, System.Int64 P2) { }
        // RVA: 0x06361FA4  token: 0x6000322
        public System.Boolean __Gen_Wrap_112(Proto.CODE P0) { }
        // RVA: 0x0A89FE20  token: 0x6000323
        public System.Boolean __Gen_Wrap_113(System.Object P0, System.Object P1, Beyond.Network.NetResponse P2) { }
        // RVA: 0x0A89FF60  token: 0x6000324
        public System.Void __Gen_Wrap_114(System.Object P0, System.Object P1, Beyond.Network.NetResponse& P2) { }
        // RVA: 0x0A8A00E0  token: 0x6000325
        public Proto.MSG_B1 __Gen_Wrap_115(System.Object P0) { }
        // RVA: 0x0A8A01D0  token: 0x6000326
        public System.Boolean __Gen_Wrap_116(System.Object P0, Beyond.Log.FLogMessage& P1) { }
        // RVA: 0x0A8A0364  token: 0x6000327
        public Beyond.Network.HGNetLogger.LogLevel __Gen_Wrap_117(Beyond.ELogLevel P0) { }
        // RVA: 0x0A8A0364  token: 0x6000328
        public Beyond.Network.HGNetLogger.NativeLogLevel __Gen_Wrap_118(Beyond.ELogLevel P0) { }
        // RVA: 0x0A8A0364  token: 0x6000329
        public System.Int32 __Gen_Wrap_119(Beyond.ELogChannel P0) { }
        // RVA: 0x069ACC64  token: 0x600032A
        public System.Boolean __Gen_Wrap_120(Beyond.Network.HGNetLogger.NativeLogLevel P0, System.Int32 P1) { }
        // RVA: 0x06373694  token: 0x600032B
        public System.Boolean __Gen_Wrap_121(Beyond.ELogLevel P0, Beyond.ELogChannel P1) { }
        // RVA: 0x0719F418  token: 0x600032C
        public System.Void __Gen_Wrap_122(System.Object P0, System.Object P1, System.Object P2, UnityEngine.LogType P3) { }
        // RVA: 0x0A8A0434  token: 0x600032D
        public Beyond.Network.HGNetLogger.HGNetLoggerClient __Gen_Wrap_123() { }
        // RVA: 0x0A8A0500  token: 0x600032E
        public System.Void __Gen_Wrap_124(System.Object P0, Beyond.Network.HGNetLogger.LogMessage& P1) { }
        // RVA: 0x0A8A0628  token: 0x600032F
        public System.Void __Gen_Wrap_125(System.Object P0, Beyond.Log.FLogMessage& P1) { }
        // RVA: 0x0A8A07AC  token: 0x6000330
        public System.Void __Gen_Wrap_126(System.Object P0, System.UInt64 P1, System.UInt64 P2, System.UInt64 P3, System.UInt64 P4) { }
        // RVA: 0x066C1D88  token: 0x6000331
        public System.Void __Gen_Wrap_127(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4) { }
        // RVA: 0x066DCC44  token: 0x6000332
        public System.Void __Gen_Wrap_128(System.Boolean P0) { }
        // RVA: 0x06362A48  token: 0x6000333
        public System.Void __Gen_Wrap_129(System.UInt64 P0) { }
        // RVA: 0x0A8A09AC  token: 0x6000334
        public System.Collections.Generic.Dictionary<System.Type,Beyond.Network.HGNetLogger.MessageType> __Gen_Wrap_130() { }
        // RVA: 0x0A8A0A78  token: 0x6000335
        public System.Collections.Generic.Dictionary<Beyond.Network.HGNetLogger.MessageType,System.Type> __Gen_Wrap_131() { }
        // RVA: 0x069AD848  token: 0x6000336
        public System.Boolean __Gen_Wrap_132(System.Object P0, Beyond.Network.HGNetLogger.MessageType& P1) { }
        // RVA: 0x0A8A0B44  token: 0x6000337
        public System.Void __Gen_Wrap_133(System.Object P0, System.Object P1, Beyond.Network.NetRequest P2) { }
        // RVA: 0x04D375D0  token: 0x6000338
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000069  // size: 0x60
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey, System.IDisposable, System.Collections.IEnumerator, System.Collections.Generic.IEnumerator`1, System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        private System.Int32 methodId_0;  // 0x40
        private System.Int32 methodId_1;  // 0x44
        private System.Int32 methodId_2;  // 0x48
        private System.Int32 methodId_3;  // 0x4c
        private System.Int32 methodId_4;  // 0x50
        private System.Int32 methodId_5;  // 0x54
        private System.Int32 methodId_6;  // 0x58

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0A8A605C */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0A8A6108 */ }

        // Methods
        // RVA: 0x06365FC0  token: 0x6000339
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x06365D60  token: 0x600033A
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06365E98  token: 0x600033C
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0A8A5D4C  token: 0x600033E
        private virtual System.Void MoveNext() { }
        // RVA: 0x0A8A5FAC  token: 0x600033F
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine P0) { }
        // RVA: 0x0A8A61B4  token: 0x6000340
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0A8A5E70  token: 0x6000341
        public System.Void RefAwaitUnsafeOnCompleteMethod() { }
        // RVA: 0x0A8A5DDC  token: 0x6000342
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x200006A  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000343
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0A8A6490  token: 0x6000344
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x0314AF50  token: 0x6000345
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x0A8A634C  token: 0x6000346
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x0A8A6418  token: 0x6000347
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x0A8A64F4  token: 0x6000348
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x0A8A62A8  token: 0x6000349
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x200006B  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-get_Client0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-set_Client0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-get_Active0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-set_Active0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-get_Available0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-get_Connected0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-get_ExclusiveAddressUse0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-set_ExclusiveAddressUse0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-Connect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-Connect1;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-Connect2;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-Connect3;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-BeginConnect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-BeginConnect1;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-BeginConnect2;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-EndConnect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-ConnectAsync0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-ConnectAsync1;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-ConnectAsync2;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-GetStream0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-Close0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-Dispose1;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-_NumericOption0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-get_ReceiveBufferSize0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-set_ReceiveBufferSize0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-get_SendBufferSize0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-set_SendBufferSize0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-get_ReceiveTimeout0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-set_ReceiveTimeout0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-get_SendTimeout0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-set_SendTimeout0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-get_LingerState0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-set_LingerState0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-get_NoDelay0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-set_NoDelay0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGTcpClient-_Initialize0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetResponse-get_logHead0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetResponse-set_logHead0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetResponse-get_csHead0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetResponse-set_csHead0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetResponse-get_msgId0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetResponse-get_csSeqId0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetResponse-get_scSeqId0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-get_currentSessionID0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-Setup0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-get_isRunning0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-_ValidateSession0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-IncrementCSSeqID0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-SendMessage0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-Send0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetUtil-GetSCMsgTypeById0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-RegisterMessageHandler0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-UnRegisterMessageHandler0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-OnNetClientShutDown0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-_SendWithResult0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-_Listen0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-_UnListen0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-_UnListenWithSeqNum0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-ResultHandler-SetIgnoreSeq0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-ResultHandler-_CheckSessionIDValid0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-ResultHandler-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-ResultHandler-_HandleMessage0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-ResultHandler-OnResponse0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-Sender-DoSend0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-Sender-DoListen0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetBus-Sender-DoUnListen0;  // const
        public static IFix.IDMAP0 Beyond-Network-Special2BusSender-get_isBusBusy0;  // const
        public static IFix.IDMAP0 Beyond-Network-DisposableBusSender-Send0;  // const
        public static IFix.IDMAP0 Beyond-Network-Special2BusSender-Send0;  // const
        public static IFix.IDMAP0 Beyond-Network-DisposableBusSender-_Clear0;  // const
        public static IFix.IDMAP0 Beyond-Network-DisposableBusSender-OnNetShutDown0;  // const
        public static IFix.IDMAP0 Beyond-Network-Special2BusSender-_ClearSendTime0;  // const
        public static IFix.IDMAP0 Beyond-Network-Special2BusSender-OnNetShutDown0;  // const
        public static IFix.IDMAP0 Beyond-Network-DisposableBusSender-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Network-Special2BusSender-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Network-InteractiveBusSender-get_isBusBusy0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMaskConfigHolder-_LoadJsonConfig0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMaskConfigHolder-_LoadScriptableObjectConfig0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMaskConfigHolder-Init0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMaskConfigHolder-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-MaskInfo-get_endTime0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-MaskInfo-get_showDecoTime0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-_UpdateMaskState0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-RemoveMask0;  // const
        public static IFix.IDMAP0 Beyond-Network-UIBusSender-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetUtil-GetCSMsgIdByType0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-_GetKey0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-AddMask0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-AddMask1;  // const
        public static IFix.IDMAP0 Beyond-Network-UIBusSender-Send0;  // const
        public static IFix.IDMAP0 Beyond-Network-UIBusSender-OnNetShutDown0;  // const
        public static IFix.IDMAP0 Beyond-Network-TeleportMaskSender-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Network-TeleportMaskSender-Send0;  // const
        public static IFix.IDMAP0 Beyond-Network-TeleportMaskSender-OnNetShutDown0;  // const
        public static IFix.IDMAP0 Beyond-Network-FrequencyControlBusSender-Send0;  // const
        public static IFix.IDMAP0 Beyond-Network-FrequencyControlBusSender-_TrySendWaitingMsg0;  // const
        public static IFix.IDMAP0 Beyond-Network-FrequencyControlBusSender-Send1;  // const
        public static IFix.IDMAP0 Beyond-Network-FrequencyControlBusSender-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Network-ServerConfig-IsSame0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetUtil-RegisterSCMessage0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetUtil-RegisterCSMessage0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetUtil-GetSCMsgIdByType0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetUtil-FastRegisterMessage0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetUtil-GetNetMessageFromDataBytes0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetUtil-SerializeHeadMessage0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetUtil-AutoRegisterMessageHandlers0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetUtil-_ForeachMessageHandlers0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetUtil-AutoUnRegisterMessageHandlers0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-PreTick0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-_CreateTickFunction0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-_OnSwitchLanguage0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-OnInit0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-_ClearTickFunction0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-OnUnInit0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-Release0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-ListenOnNetMaskChanged0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkMask-UnListenOnNetMaskChanged0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkUrls-RemoteConfigUrl0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkUrls-ResourceRootUrlDev0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkUrls-_GetUrlFromLocal0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkUrls-ServerListUrl0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkUrls-_SelectUrl0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkUrls-AgeTipsUrl0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkUrls-GovUrl0;  // const
        public static IFix.IDMAP0 Beyond-Network-NetworkUrls-LauncherUrl0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-IsConnected0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-Close0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-Connect0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-ConnectAsync0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-IsAuthed0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-MarkAuthed0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-EnableCryptStream0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-GetEndPoint0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-TestCloseNetIO0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-TestDropNetIO0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-_CreateWeakNetDelayConfig0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-_ResetWeakNetWindow0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-SetWeakNetCustom0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-SetWeakNetPreset0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-GetProfilerSnapshot0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-_NextDelayMs0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-_ApplyWeakNetDelay0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-WriteData0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-WriteCryptoData0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-GetSendHeadBuffer0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-GetSendBuffer0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-GetReceiveBuffer0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-ClearReceiveBufferOffset0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-GetReceiveBufferOffset0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-SkipReceiveBufferOffset0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-ReadData0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-ReadCryptoData0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-Available0;  // const
        public static IFix.IDMAP0 Beyond-Network-TcpIO-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-FSendFunc-GetCancellation0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-_CreateWebRequestGET0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-_CreateWebRequestPOST0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-FSendFunc-Invoke0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-Cancellation-IsCanceled0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-_SetHttpResult0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-_MarkResultAsException0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-_YieldSendHttpRequest0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-_DeserializeTypedResult0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-_YieldSendHttpRequest1;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-YieldSendGet0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-YieldSendGet1;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-YieldSendPost0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-YieldSendPost1;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-_WaitForServiceDone0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-_StartWebRequestTask0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-_WaitForSendHttpRequest0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-WaitForSendPost0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-WaitForSendGet0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-WaitForSendGet1;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-GetGeneralErrorCode0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-_DebugOnlyProfileResult0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-Cancellation-Cancel0;  // const
        public static IFix.IDMAP0 Beyond-Network-HttpSender-FSendFunc-GetOptMethod0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-get_reconnectState0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-set_reconnectState0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-get_isConnecting0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-get_isRunningAndConnected0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-get_isReconnecting0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-get_closeErrorCode0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-get_isAutoReconnect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-get_maxRecycleQueueCount0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-Start0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-_ApplyWeakNetConfigToTcpIO0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-_DNSCheck0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-RemoveAutoReconnect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-CloseTCPMsgThreadTask0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-InternalClose0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-InternalClose1;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-Connect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionConnectYield-SetFinish0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-Close0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-OnConnectAsyncEnd0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-ConnectAsync0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-EnqueueSendMsg0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-GetSendQueueMsgCount0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-SetCSSeqID0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-GetCSSeqID0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-GetIO0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-TryCloseTcpIOWithTimeout0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-TryConnectWithTimeout0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-_DoReconnectAsync0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-_ReconnectCheckerTick0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-SessionCheckSignalAndSleep0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-NetStatusChecker_Receive0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-_ReadAndCheckMessageInSessionThread0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-NetStatusChecker_Sender0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-_SendAndCheckMessage0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-_SessionThread0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-StartTCPMsgLoopThreadTask0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-OpenAutoReconnect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-SetWeakNetPreset0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetBaseSession-SetWeakNetCustom0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-SetAutoReconnectToken0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-SessionConnectTimeoutCheckerTick0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-GetClientPublicKey0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_GetOnlineResVersion0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_GetPlatformID0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_GetSDKEnv0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_GetPayPlatform0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_CSLoginHelper_UpdateAppInfo0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_WriteMessageInSessionThread0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_ReadMessageInSessionThread0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_ApproximateLoginServerTime0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_RSADecrypt0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_HandleLoginEncryp0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-DoReconnectLogin0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-Start0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-OnApplicationPause0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_FlushMergeQueueToSendQueue0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-GetRequest0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-LoginErrorInfo-SetErrorConnectError0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-SetConnectErrorError0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-LoginErrorInfo-Clear0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-SetSucceed0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-LoginErrorInfo-Set0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-SetNtfError0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-LoginErrorInfo-SetErrorConnectTimeOut0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-SetTimeOutError0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_SessionLoginThreadTask0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-LoginAsync0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-StartMsgLoop0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-OnConnectedSucceed0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-OnReconnectTimesOver0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-NeedKeepConnect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-CheckAndWriteReqMsg0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-ReadMessageInSessionThread0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-ClearRecvQueue0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-ClearSendQueue0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-GetNetDevProfilerSnapshot0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-GetPingPassedTime0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-GetPing0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-IsPingInProgress0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-ClearPingWDG0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_HandleSpecialMessagePing0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-IsPingOut0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-PingSenderTick0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-InitClientRSAKey0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-ClearRSAKey0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionUtils-CheckErrorNeedsDisconnect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_HandleSpecialMessageErrorCode0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_HandleSpecialMessageReconnectIncr0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_HandleSpecialMessageReconnectFull0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-CheckAndDealRespMessages0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSession-_HandleSpecialMessageReconnectLogin0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-GetResponse0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-GetErrorCode0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-GetErrorDetails0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-get_isSucceed0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-get_keepWaiting0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionLoginYield-LoginErrorInfo-SetErrorInvaild0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionConnectYield-GetCode0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionConnectYield-get_isSucceed0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetSessionConnectYield-get_keepWaiting0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-get_isRunning0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-get_isConnecting0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-get_isRunningAndConnected0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-get_closeErrorCode0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-get_isAutoReconnect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-Start0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-_DNSCheck0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-RemoveAutoReconnect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-CloseTCPMsgThreadTask0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-Close0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-Close1;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-Connect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-OnConnectAsyncEnd0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-ConnectAsync0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-EnqueueSendMsg0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-GetSendQueueMsgCount0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-IncrementCSSeqID0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-SetCSSeqID0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-GetCSSeqID0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-GetIO0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-_ReconnectCheckerTick0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-SessionCheckSignalAndSleep0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-NetStatusChecker_Receive0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-NetStatusChecker_Sender0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-_SessionThread0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-StartTCPMsgLoopThreadTask0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogBaseSession-OpenAutoReconnect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogger-CheckLog0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogger-ConvertFromDLogger0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogger-ConvertFromDLoggerV20;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogger-ConvertFromDLogger1;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerCloudSettings-ConvertFromDLogger0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerCloudSettings-IsNetLogLevelEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerCloudSettings-IsNetLogLevelEnabled1;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerClient-_FlushLocalLogQueue0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerClient-AddLog0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerClient-_CloseAndClear0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerClient-ShutDown0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerClient-get_instance0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-SendMessage0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerClient-WriteNetLog0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogger-Log0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogger-Clear0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLogger-SetLogChannel0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-get_uid0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-get_roleId0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-SetAutoReconnectToken0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-SetReconnectSuccessCallback0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerClient-Init0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-OnApplicationPause0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerClient-OnApplicationPause0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerClient-SetRoleID0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerClient-SetNetLogMaskFromServer0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerCloudSettings-SetCloudLogControl0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerCloudSettings-IsLocalLogLevelEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-get_s_messageType2Id0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-get_s_messageId2Type0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-TryGetMsgTypeByID0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-Start0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-_WriteMessageInSessionThread0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-SendLogin0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-OnConnectedSucceed0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-ReadMessageInSessionThread0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-CheckAndDealRespMessages0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-CheckAndWriteReqMsg0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-NeedKeepConnect0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-DoReconnectLogin0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-_GetConnectTimeoutTime0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-SessionConnectTimeoutCheckerTick0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-ClearPingWDG0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-IsPingOut0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-PingSenderTick0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-InitClientRSAKey0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-ClearRSAKey0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-ClearRecvQueue0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-ClearSendQueue0;  // const
        public static IFix.IDMAP0 Beyond-Network-HGNetLogger-HGNetLoggerSession-OnReconnectTimesOver0;  // const

    }

}

