// ========================================================
// Dumped by @desirepro
// Assembly: Hypergryph.OneChannel.dll
// Classes:  143
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000007  // size: 0x14
    public sealed struct Status
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.SDKCaptchaHandler.Status SUC;  // const
        public static U8.SDK.SDKCaptchaHandler.Status SYS_BUSY;  // const
        public static U8.SDK.SDKCaptchaHandler.Status USER_CANCEL;  // const
        public static U8.SDK.SDKCaptchaHandler.Status NOT_SUPPORT;  // const
        public static U8.SDK.SDKCaptchaHandler.Status INVALID_PARAM;  // const
        public static U8.SDK.SDKCaptchaHandler.Status UNKNOWN;  // const

    }

    // TypeToken: 0x2000008  // size: 0x20
    public class Result
    {
        // Fields
        private U8.SDK.SDKCaptchaHandler.Status <status>k__BackingField;  // 0x10
        private System.Int32 <errorCode>k__BackingField;  // 0x14
        private System.String <captcha>k__BackingField;  // 0x18

        // Properties
        U8.SDK.SDKCaptchaHandler.Status status { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        System.Int32 errorCode { set; /* RVA: 0x04D8F540 */ }
        System.String captcha { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000010
        private System.Void .ctor() { }
        // RVA: 0x09DE6E20  token: 0x6000011
        public static U8.SDK.SDKCaptchaHandler.Result CreateForOutput() { }
        // RVA: 0x09DE6EB8  token: 0x6000012
        public System.Void MarkSucceed(System.String captcha) { }
        // RVA: 0x09DE6E8C  token: 0x6000013
        public System.Void MarkFailed(U8.SDK.SDKCaptchaHandler.Status status, System.Int32 errorCode) { }
        // RVA: 0x09DE6E64  token: 0x6000014
        public System.Void MarkFailed(U8.SDK.SDKCaptchaHandler.Status status) { }

    }

    // TypeToken: 0x200000A
    public interface IFromJSON
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600005A
        public virtual System.Boolean LoadFromJSON(System.String json) { }

    }

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct POSTStatus
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.SDKExternalTools.POSTStatus NONE;  // const
        public static U8.SDK.SDKExternalTools.POSTStatus SUC;  // const
        public static U8.SDK.SDKExternalTools.POSTStatus TIMEOUT;  // const
        public static U8.SDK.SDKExternalTools.POSTStatus ERROR;  // const

    }

    // TypeToken: 0x200000C  // size: 0x14
    public sealed struct BusType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.SDKExternalTools.BusType NONE;  // const
        public static U8.SDK.SDKExternalTools.BusType GET_TOKEN;  // const
        public static U8.SDK.SDKExternalTools.BusType GET_CAPTCHA;  // const
        public static U8.SDK.SDKExternalTools.BusType CREATE_ORDER;  // const
        public static U8.SDK.SDKExternalTools.BusType CONFIRM_ORDER;  // const
        public static U8.SDK.SDKExternalTools.BusType VERIFY_ACCOUNT;  // const
        public static U8.SDK.SDKExternalTools.BusType GET_PRODUCT_LIST;  // const
        public static U8.SDK.SDKExternalTools.BusType AUTHV2_SESSION_TOKEN;  // const
        public static U8.SDK.SDKExternalTools.BusType AUTHV2_OAUTH2CODE;  // const
        public static U8.SDK.SDKExternalTools.BusType AUTHV2_OAUTH2TOKEN;  // const
        public static U8.SDK.SDKExternalTools.BusType SERVER_LIST;  // const
        public static U8.SDK.SDKExternalTools.BusType CONFIRM_SERVER;  // const

    }

    // TypeToken: 0x200000D  // size: 0x14
    public sealed struct ErrorType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.SDKExternalTools.ErrorType NONE;  // const
        public static U8.SDK.SDKExternalTools.ErrorType TIMEOUT;  // const
        public static U8.SDK.SDKExternalTools.ErrorType NETWORK_ERROR;  // const
        public static U8.SDK.SDKExternalTools.ErrorType DESERIALIZE_FAILED;  // const
        public static U8.SDK.SDKExternalTools.ErrorType BUSINESS_ERROR;  // const

    }

    // TypeToken: 0x200000E  // size: 0x30
    public sealed struct POSTRequest
    {
        // Fields
        public System.String url;  // 0x10
        public System.String body;  // 0x18
        public System.String contentType;  // 0x20
        public System.Collections.Generic.Dictionary<System.String,System.String> nullableHeaders;  // 0x28

    }

    // TypeToken: 0x200000F  // size: 0x28
    public sealed struct POSTResult
    {
        // Fields
        public U8.SDK.SDKExternalTools.POSTStatus status;  // 0x10
        public System.Int32 httpCode;  // 0x14
        public System.String response;  // 0x18
        public System.String error;  // 0x20

        // Methods
        // RVA: 0x09DE6CF4  token: 0x600005B
        public System.Boolean ServiceFailed() { }

    }

    // TypeToken: 0x2000010  // size: 0x30
    public sealed struct ErrMsgMeta
    {
        // Fields
        public static readonly U8.SDK.SDKExternalTools.ErrMsgMeta EMPTY;  // static @ 0x0
        public U8.SDK.SDKExternalTools.BusType busType;  // 0x10
        public U8.SDK.SDKExternalTools.ErrorType errorType;  // 0x14
        public System.Int32 errorCode;  // 0x18
        public System.String alertFromServer;  // 0x20
        public System.Object meta;  // 0x28

        // Methods
        // RVA: 0x0426F6A0  token: 0x600005C
        public System.Boolean IsEmpty() { }
        // RVA: 0x09DE4E74  token: 0x600005D
        public static U8.SDK.SDKExternalTools.ErrMsgMeta FromPOSTResult(U8.SDK.SDKExternalTools.BusType busType, U8.SDK.SDKExternalTools.POSTResult postRet) { }

    }

    // TypeToken: 0x2000012  // size: 0x38
    public sealed class <FetchCaptchaCoroutine>d__6 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.SDKExternalTools.CaptchaMgr <>4__this;  // 0x20
        public U8.SDK.SDKCaptchaHandler.Result outResult;  // 0x28
        public System.Collections.Generic.Dictionary<System.String,System.Object> captchaParams;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000062
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x09DEEEE4  token: 0x6000063
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DEEAFC  token: 0x6000064
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DEEF5C  token: 0x6000065
        private System.Void <>m__Finally1() { }
        // RVA: 0x09DEEE98  token: 0x6000067
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000011  // size: 0x20
    public class CaptchaMgr
    {
        // Fields
        private U8.SDK.SDKExternalTools m_host;  // 0x10
        private U8.SDK.SDKCaptchaHandler <activeHandler>k__BackingField;  // 0x18

        // Properties
        U8.SDK.SDKCaptchaHandler activeHandler { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000060
        public System.Void .ctor(U8.SDK.SDKExternalTools host) { }
        // RVA: 0x09DE4778  token: 0x6000061
        public System.Collections.IEnumerator FetchCaptchaCoroutine(System.Collections.Generic.Dictionary<System.String,System.Object> captchaParams, U8.SDK.SDKCaptchaHandler.Result outResult) { }

    }

    // TypeToken: 0x2000014  // size: 0x38
    public sealed struct Builder
    {
        // Fields
        public U8.SDK.SDKExternalTools.POSTRequest request;  // 0x10
        public U8.SDK.SDKExternalTools.BusType busType;  // 0x30

        // Methods
        // RVA: 0x09DE4704  token: 0x6000075
        public U8.SDK.SDKExternalTools.POSTProcedure Build() { }
        // RVA: -1  // generic def  token: 0x6000076
        public U8.SDK.SDKExternalTools.POSTProcedure<TResp> TypedBuild() { }

    }

    // TypeToken: 0x2000015  // size: 0x30
    public sealed class <POST>d__18 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.SDKExternalTools host;  // 0x20
        public U8.SDK.SDKExternalTools.POSTProcedure <>4__this;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000077
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000078
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DEEF8C  token: 0x6000079
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DEF0B0  token: 0x600007B
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000013  // size: 0x68
    public class POSTProcedure
    {
        // Fields
        private U8.SDK.SDKExternalTools.POSTProcedure.Builder <builder>k__BackingField;  // 0x10
        private System.Boolean <isServiceFinished>k__BackingField;  // 0x38
        private U8.SDK.SDKExternalTools.ErrMsgMeta <serviceError>k__BackingField;  // 0x40
        private System.String <rawResponse>k__BackingField;  // 0x60

        // Properties
        U8.SDK.SDKExternalTools.POSTProcedure.Builder builder { get; /* RVA: 0x02B2D2D0 */ set; /* RVA: 0x09DC5750 */ }
        System.Boolean isServiceFinished { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        U8.SDK.SDKExternalTools.ErrMsgMeta serviceError { get; /* RVA: 0x04DA2E80 */ set; /* RVA: 0x09DE6CDC */ }
        System.String rawResponse { set; /* RVA: 0x063CC164 */ }

        // Methods
        // RVA: 0x04DA2E40  token: 0x6000070
        public U8.SDK.SDKExternalTools.ErrMsgMeta MakeBusinessError(System.Int32 errorCode) { }
        // RVA: 0x09DE6B58  token: 0x6000071
        public System.Collections.IEnumerator POST(U8.SDK.SDKExternalTools host) { }
        // RVA: 0x09DE6BCC  token: 0x6000072
        private System.Void _OnServiceFinished(U8.SDK.SDKExternalTools.POSTResult postRet) { }
        // RVA: 0x041E1670  token: 0x6000073
        protected virtual System.Void HandleServiceResponse(U8.SDK.SDKExternalTools.POSTResult postRet) { }
        // RVA: 0x041E1670  token: 0x6000074
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000016
    public class POSTProcedure`1 : POSTProcedure
    {
        // Fields
        private TResp <response>k__BackingField;  // 0x0

        // Properties
        TResp response { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600007F
        protected virtual System.Void HandleServiceResponse(U8.SDK.SDKExternalTools.POSTResult postRet) { }
        // RVA: -1  // not resolved  token: 0x6000080
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x40
    public class AuthV2SessionTokenResp : IFromJSON
    {
        // Fields
        public System.Int32 status;  // 0x10
        public System.String msg;  // 0x18
        public System.String type;  // 0x20
        public System.String token;  // 0x28
        public System.String uid;  // 0x30
        public System.Boolean isNew;  // 0x38

        // Methods
        // RVA: 0x09DE4510  token: 0x6000081
        public virtual System.Boolean LoadFromJSON(System.String json) { }
        // RVA: 0x041E1670  token: 0x6000082
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000018  // size: 0x20
    public class GetProductLsitResp : IFromJSON
    {
        // Fields
        public System.Int32 flag;  // 0x10
        public System.Collections.Generic.Dictionary<System.String,System.Object> desc;  // 0x18

        // Methods
        // RVA: 0x09DE4EB4  token: 0x6000083
        public System.Boolean LoadFromDictionary(System.Collections.Generic.Dictionary<System.String,System.Object> dict) { }
        // RVA: 0x09DE5118  token: 0x6000084
        public virtual System.Boolean LoadFromJSON(System.String json) { }
        // RVA: 0x041E1670  token: 0x6000085
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x68
    public class AuthV2GrantResp : IFromJSON
    {
        // Fields
        public System.Int32 status;  // 0x10
        public System.String msg;  // 0x18
        public System.String type;  // 0x20
        public System.String uid;  // 0x28
        public System.String code;  // 0x30
        public System.String token;  // 0x38
        public System.Int64 banStartTs;  // 0x40
        public System.Int64 banEndTs;  // 0x48
        public System.Int64 maintainStartTs;  // 0x50
        public System.Int64 maintainEndTs;  // 0x58
        public System.Collections.Generic.Dictionary<System.String,System.Object> captcha;  // 0x60

        // Methods
        // RVA: 0x09DE4244  token: 0x6000086
        public virtual System.Boolean LoadFromJSON(System.String json) { }
        // RVA: 0x041E1670  token: 0x6000087
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001B  // size: 0x50
    public sealed struct ServerData
    {
        // Fields
        public System.String serverId;  // 0x10
        public System.String serverName;  // 0x18
        public System.String serverDomain;  // 0x20
        public System.Boolean defaultChoose;  // 0x28
        public System.String roleId;  // 0x30
        public System.String nickName;  // 0x38
        public System.Int64 level;  // 0x40
        public System.String extension;  // 0x48

    }

    // TypeToken: 0x200001A  // size: 0x28
    public class ServerListResp : IFromJSON
    {
        // Fields
        public System.Int32 status;  // 0x10
        public System.String msg;  // 0x18
        public System.Collections.Generic.List<U8.SDK.SDKExternalTools.ServerListResp.ServerData> data;  // 0x20

        // Methods
        // RVA: 0x09DEE38C  token: 0x6000088
        public virtual System.Boolean LoadFromJSON(System.String json) { }
        // RVA: 0x09DEE884  token: 0x6000089
        public System.Collections.Generic.List<U8.SDK.U8ServerInfo> ToServerInfo() { }
        // RVA: 0x041E1670  token: 0x600008A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x20
    public class ConfirmServerResp : IFromJSON
    {
        // Fields
        public System.Int32 status;  // 0x10
        public System.String msg;  // 0x18

        // Methods
        // RVA: 0x09DE49F4  token: 0x600008B
        public virtual System.Boolean LoadFromJSON(System.String json) { }
        // RVA: 0x041E1670  token: 0x600008C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001D  // size: 0x18
    public class ConfirmOrderU1Resp : IFromJSON
    {
        // Fields
        public System.Int32 status;  // 0x10

        // Methods
        // RVA: 0x09DE4958  token: 0x600008D
        public virtual System.Boolean LoadFromJSON(System.String json) { }
        // RVA: 0x041E1670  token: 0x600008E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x40
    public sealed struct Data
    {
        // Fields
        public System.String orderId;  // 0x10
        public System.String channelProductCode;  // 0x18
        public System.String productName;  // 0x20
        public System.String productDesc;  // 0x28
        public System.Int64 amount;  // 0x30
        public System.Collections.Generic.Dictionary<System.String,System.Object> extension;  // 0x38

    }

    // TypeToken: 0x200001E  // size: 0x58
    public class CreateOrderU1Resp : IFromJSON
    {
        // Fields
        public System.Int32 status;  // 0x10
        public System.String msg;  // 0x18
        public System.String type;  // 0x20
        public U8.SDK.SDKExternalTools.CreateOrderU1Resp.Data data;  // 0x28

        // Methods
        // RVA: 0x09DE4AD0  token: 0x600008F
        public virtual System.Boolean LoadFromJSON(System.String json) { }
        // RVA: 0x09DE4D5C  token: 0x6000090
        public U8.SDK.U8OrderInfo ToOrderInfo() { }
        // RVA: 0x041E1670  token: 0x6000091
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000020  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly U8.SDK.SDKExternalTools.<>c <>9;  // static @ 0x0
        public static System.Action<System.String> <>9__43_0;  // static @ 0x8
        public static System.Action<System.String> <>9__43_1;  // static @ 0x10
        public static System.Action <>9__44_0;  // static @ 0x18
        public static System.Action <>9__44_1;  // static @ 0x20

        // Methods
        // RVA: 0x04D40390  token: 0x6000092
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000093
        public System.Void .ctor() { }
        // RVA: 0x04D26E00  token: 0x6000094
        private System.Void <LoginWithPlugin>b__43_0(System.String extension) { }
        // RVA: 0x09DF02D4  token: 0x6000095
        private System.Void <LoginWithPlugin>b__43_1(System.String message) { }
        // RVA: 0x09DF0340  token: 0x6000096
        private System.Void <LogoutWithPlugin>b__44_0() { }
        // RVA: 0x09DF03C0  token: 0x6000097
        private System.Void <LogoutWithPlugin>b__44_1() { }

    }

    // TypeToken: 0x2000021  // size: 0x28
    public sealed class <_NextFrameCoroutine>d__61 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Action action;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000098
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000099
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DF2EAC  token: 0x600009A
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DF2EFC  token: 0x600009C
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000022  // size: 0x48
    public sealed class <SDKInterfaceAuthV2SessionToken>d__114 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.SDKPromiseWithResult<U8.SDK.U8LoginV2Result> promise;  // 0x20
        public U8.SDK.SDKMeta meta;  // 0x28
        public System.String channelToken;  // 0x30
        public U8.SDK.SDKExternalTools <>4__this;  // 0x38
        private U8.SDK.SDKExternalTools.POSTProcedure<U8.SDK.SDKExternalTools.AuthV2SessionTokenResp> <postImpl>5__2;  // 0x40

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600009E
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600009F
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DEF1AC  token: 0x60000A0
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DEF650  token: 0x60000A2
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000023  // size: 0x40
    public sealed class <SDKInterfaceAuthV2OAuth2Code>d__115 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.SDKExternalTools <>4__this;  // 0x20
        public System.String sessionToken;  // 0x28
        public U8.SDK.SDKPromiseWithResult<U8.SDK.U8GrantResult> promise;  // 0x30
        public System.Action onSessionInvalid;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000A4
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000A5
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DEF0FC  token: 0x60000A6
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DEF160  token: 0x60000A8
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000024  // size: 0x60
    public sealed class <_AuthV2GrantImpl>d__121 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.SDKPromiseWithResult<U8.SDK.U8GrantResult> promise;  // 0x20
        public U8.SDK.SDKExternalTools <>4__this;  // 0x28
        public System.String sessionToken;  // 0x30
        public System.Action onSessionInvalid;  // 0x38
        private System.String <captcha>5__2;  // 0x40
        private System.Boolean <needCaptcha>5__3;  // 0x48
        private U8.SDK.SDKExternalTools.POSTProcedure<U8.SDK.SDKExternalTools.AuthV2GrantResp> <postImpl>5__4;  // 0x50
        private U8.SDK.SDKCaptchaHandler.Result <captchaRet>5__5;  // 0x58

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000AA
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000AB
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DF1D28  token: 0x60000AC
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DF2484  token: 0x60000AE
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000025  // size: 0x40
    public sealed class <_ConfirmOrderU1Coroutine>d__124 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.SDKExternalTools <>4__this;  // 0x20
        public System.String orderId;  // 0x28
        public System.String extension;  // 0x30
        public U8.SDK.SDKPromise<U8.SDK.U8ConfirmOrderResult> promise;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000B0
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000B1
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DF24D0  token: 0x60000B2
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DF26D4  token: 0x60000B4
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000026  // size: 0x70
    public sealed class <_PollOrderStatusCoroutine>d__125 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.SDKExternalTools <>4__this;  // 0x20
        public System.String orderId;  // 0x28
        public U8.SDK.SDKPromise<U8.SDK.U8ConfirmOrderResult> promise;  // 0x30
        private System.Int32 <tryCnt>5__2;  // 0x38
        private System.Int32 <maxTryCnt>5__3;  // 0x3c
        private U8.SDK.SDKExternalTools.POSTProcedure.Builder <postBuilder>5__4;  // 0x40
        private U8.SDK.SDKExternalTools.POSTProcedure<U8.SDK.SDKExternalTools.ConfirmOrderU1Resp> <postImpl>5__5;  // 0x68

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000B6
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000B7
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DF2F48  token: 0x60000B8
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DF340C  token: 0x60000BA
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000027  // size: 0x40
    public sealed class <_GetServerListCoroutine>d__126 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.SDKExternalTools <>4__this;  // 0x20
        public System.String paramStr;  // 0x28
        public U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ServerInfo>> promise;  // 0x30
        private U8.SDK.SDKExternalTools.POSTProcedure<U8.SDK.SDKExternalTools.ServerListResp> <postImpl>5__2;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000BC
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000BD
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DF2B20  token: 0x60000BE
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DF2DC8  token: 0x60000C0
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000028  // size: 0x48
    public sealed class <_ConfirmServerListCoroutine>d__128 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.SDKExternalTools <>4__this;  // 0x20
        public System.String paramStr;  // 0x28
        public U8.SDK.SDKPromise<U8.SDK.U8ConfirmServerResult> promise;  // 0x30
        public System.String serverId;  // 0x38
        private U8.SDK.SDKExternalTools.POSTProcedure<U8.SDK.SDKExternalTools.ConfirmServerResp> <postImpl>5__2;  // 0x40

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000C2
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000C3
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DF2720  token: 0x60000C4
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DF2AD4  token: 0x60000C6
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000029  // size: 0x40
    public sealed class <_SendCreateOrderU1Coroutine>d__129 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.SDKExternalTools <>4__this;  // 0x20
        public System.String paramStr;  // 0x28
        public U8.SDK.SDKPromise<U8.SDK.U8OrderInfo> promise;  // 0x30
        private U8.SDK.SDKExternalTools.POSTProcedure<U8.SDK.SDKExternalTools.CreateOrderU1Resp> <postImpl>5__2;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000C8
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000C9
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DF3514  token: 0x60000CA
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DF37AC  token: 0x60000CC
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200002A  // size: 0x28
    public sealed class <_SendCreateOrderFailCoroutine>d__130 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.SDKPromise<U8.SDK.U8OrderInfo> promise;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000CE
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000CF
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DF3458  token: 0x60000D0
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DF34C8  token: 0x60000D2
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200002D  // size: 0x18
    public sealed struct U8ProductListDataV2 : IFromJSON
    {
        // Fields
        public System.Collections.Generic.List<U8.SDK.U8ProductInfo> productList;  // 0x10

        // Methods
        // RVA: 0x09DF4498  token: 0x60000DC
        public virtual System.Boolean LoadFromJSON(System.String json) { }
        // RVA: 0x09DF4740  token: 0x60000DD
        private static System.Boolean _BuildProductInfo(U8.SDK.U8ProductInfo outInfo, System.Collections.Generic.Dictionary<System.String,System.Object> dict) { }

    }

    // TypeToken: 0x200002E  // size: 0x14
    public sealed struct RequestConnPolicy
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.SDKExternalToolsShortcut.RequestConnPolicy DEFAULT;  // const
        public static U8.SDK.SDKExternalToolsShortcut.RequestConnPolicy CLOSE_EACH_CON;  // const

    }

    // TypeToken: 0x200002F  // size: 0x14
    public sealed struct RequestHeaderInjectOptions
    {
        // Fields
        public U8.SDK.SDKExternalToolsShortcut.RequestConnPolicy policy;  // 0x10

    }

    // TypeToken: 0x2000030  // size: 0x48
    public sealed class <>c__DisplayClass10_0
    {
        // Fields
        public U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ProductInfo>> promise;  // 0x10
        public U8.SDK.SDKExternalToolsShortcut <>4__this;  // 0x18
        public U8.SDK.SDKExternalTools.POSTRequest request;  // 0x20
        public System.Action<U8.SDK.SDKExternalTools.POSTResult> callback;  // 0x40

        // Methods
        // RVA: 0x041E1670  token: 0x60000DE
        public System.Void .ctor() { }
        // RVA: 0x09DF0518  token: 0x60000DF
        private System.Void <SendGetProductListRequestV2>b__0(U8.SDK.SDKExternalTools.POSTResult postRet) { }
        // RVA: 0x09DF065C  token: 0x60000E0
        private System.Void <SendGetProductListRequestV2>b__1() { }

    }

    // TypeToken: 0x2000032  // size: 0x30
    public sealed struct AuthStatus
    {
        // Fields
        public static readonly U8.SDK.U8SDKInterface.AuthStatus EMPTY;  // static @ 0x0
        public System.String uid;  // 0x10
        public System.String oauth2code;  // 0x18
        public System.String oauth2token;  // 0x20
        public System.String sessionToken;  // 0x28

    }

    // TypeToken: 0x2000033  // size: 0x18
    public sealed struct V2LoginStatus
    {
        // Fields
        public System.String sessionToken;  // 0x10

    }

    // TypeToken: 0x2000034  // size: 0x80
    public sealed class ReceiverProductListResult : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FE0AC  token: 0x600012B
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x600012C
        public virtual System.Void Invoke(System.Collections.Generic.Dictionary<System.String,System.Object> msg) { }

    }

    // TypeToken: 0x2000035  // size: 0x28
    public sealed struct GameServerTimeCache
    {
        // Fields
        public System.Int64 cacheTime;  // 0x10
        public System.Int64 maintainEndTs;  // 0x18
        public System.Int64 maintainStartTs;  // 0x20

    }

    // TypeToken: 0x2000036
    public sealed class <>c__DisplayClass51_0`1
    {
        // Fields
        public U8.SDK.SDKPromise<System.String> delegatePromise;  // 0x0
        public U8.SDK.U8SDKInterface <>4__this;  // 0x0
        public System.String env;  // 0x0
        public System.Action <>9__3;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600012D
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600012E
        private System.Void <V2PublicInit>b__0(System.String obj) { }
        // RVA: -1  // not resolved  token: 0x600012F
        private System.Void <V2PublicInit>b__3() { }
        // RVA: -1  // not resolved  token: 0x6000130
        private System.Void <V2PublicInit>b__1(System.Object info) { }
        // RVA: -1  // not resolved  token: 0x6000131
        private System.Void <V2PublicInit>b__2() { }

    }

    // TypeToken: 0x2000037  // size: 0x28
    public sealed class <>c__DisplayClass55_0
    {
        // Fields
        public U8.SDK.U8SDKInterface <>4__this;  // 0x10
        public System.String uid;  // 0x18
        public System.Boolean isNew;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000132
        public System.Void .ctor() { }
        // RVA: 0x04CE3A40  token: 0x6000133
        private System.Void <_onLoginSuc>b__0() { }

    }

    // TypeToken: 0x2000038  // size: 0x38
    public sealed class <>c__DisplayClass57_0
    {
        // Fields
        public U8.SDK.SDKExternalTools externalTools;  // 0x10
        public U8.SDK.U8SDKInterface <>4__this;  // 0x18
        public U8.SDK.SDKPromise<U8.SDK.U8ProductsResult> delegatePromise;  // 0x20
        public System.Action<System.Collections.Generic.List<U8.SDK.U8ProductInfo>> <>9__1;  // 0x28
        public System.Action<System.Object> <>9__2;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x6000134
        public System.Void .ctor() { }
        // RVA: 0x09DF08C8  token: 0x6000135
        private System.Void <PublicGetProductListV2>b__0() { }
        // RVA: 0x09DF09E8  token: 0x6000136
        private System.Void <PublicGetProductListV2>b__1(System.Collections.Generic.List<U8.SDK.U8ProductInfo> productList) { }
        // RVA: 0x09DF0CBC  token: 0x6000137
        private System.Void <PublicGetProductListV2>b__2(System.Object rejectObj) { }

    }

    // TypeToken: 0x2000039  // size: 0x28
    public sealed class <>c__DisplayClass57_1
    {
        // Fields
        public System.Collections.Generic.List<U8.SDK.U8ProductInfo> productList;  // 0x10
        public U8.SDK.U8SDKInterface.ReceiverProductListResult receiver;  // 0x18
        public U8.SDK.U8SDKInterface.<>c__DisplayClass57_0 CS$<>8__locals1;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000138
        public System.Void .ctor() { }
        // RVA: 0x09DF0CE0  token: 0x6000139
        private System.Void <PublicGetProductListV2>b__3(System.Collections.Generic.Dictionary<System.String,System.Object> msg) { }

    }

    // TypeToken: 0x200003A  // size: 0x40
    public sealed class <>c__DisplayClass69_0
    {
        // Fields
        public U8.SDK.U8PayParams payParams;  // 0x10
        public U8.SDK.U8SDKInterface <>4__this;  // 0x18
        public U8.SDK.SDKPromise<U8.SDK.U8PayResult> wrappedPromise;  // 0x20
        public U8.SDK.U8ProductInfo productInfo;  // 0x28
        public System.Action<U8.SDK.U8PayResult> <>9__1;  // 0x30
        public System.Action<System.Object> <>9__2;  // 0x38

        // Methods
        // RVA: 0x041E1670  token: 0x600013A
        public System.Void .ctor() { }
        // RVA: 0x09DF1620  token: 0x600013B
        private System.Void <PublicPay>b__0() { }
        // RVA: 0x09DF17AC  token: 0x600013C
        private System.Void <PublicPay>b__1(U8.SDK.U8PayResult payResult) { }
        // RVA: 0x09DF0CBC  token: 0x600013D
        private System.Void <PublicPay>b__2(System.Object payFailMsg) { }
        // RVA: 0x09DF1850  token: 0x600013E
        private System.Void <PublicPay>b__3(U8.SDK.U8PayResult payResult) { }
        // RVA: 0x09DF0CBC  token: 0x600013F
        private System.Void <PublicPay>b__4(System.Object payFailMsg) { }

    }

    // TypeToken: 0x200003B  // size: 0x20
    public sealed class <>c__DisplayClass70_0
    {
        // Fields
        public U8.SDK.U8SDKInterface <>4__this;  // 0x10
        public System.Int64 revenue;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000140
        public System.Void .ctor() { }
        // RVA: 0x09DF18F4  token: 0x6000141
        private System.Void <_OnNativePayFulfilled>b__0() { }

    }

    // TypeToken: 0x200003C  // size: 0x20
    public sealed class <>c__DisplayClass78_0
    {
        // Fields
        public U8.SDK.U8SDKInterface <>4__this;  // 0x10
        public U8.SDK.U8PayParams payParams;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000142
        public System.Void .ctor() { }
        // RVA: 0x09DF1964  token: 0x6000143
        private System.Void <_NativePay>b__0() { }

    }

    // TypeToken: 0x200003D  // size: 0x28
    public sealed class <_NextFrameCoroutine>d__105 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Action call;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000144
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000145
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x04991450  token: 0x6000146
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DF2E60  token: 0x6000148
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200003E  // size: 0x20
    public sealed class <>c__DisplayClass131_0
    {
        // Fields
        public U8.SDK.U8SDKInterface <>4__this;  // 0x10
        public U8.SDK.SDKPromise<U8.SDK.U8AuthV2Result> promise;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600014A
        public System.Void .ctor() { }
        // RVA: 0x03F4B8E0  token: 0x600014B
        private System.Void <_DoMockAuthV2>b__0() { }

    }

    // TypeToken: 0x200003F  // size: 0x40
    public sealed class <_LoginV2Coroutine>d__132 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.U8SDKInterface <>4__this;  // 0x20
        public U8.SDK.SDKPromise<System.String> promise;  // 0x28
        private U8.SDK.SDKPromiseEnumerator<System.String> <loginEnumerator>5__2;  // 0x30
        private U8.SDK.SDKPromiseWithResult<U8.SDK.U8LoginV2Result> <sessionTokenPromise>5__3;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600014C
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600014D
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03AF94D0  token: 0x600014E
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DF2E14  token: 0x6000150
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000040  // size: 0x38
    public sealed class <_AuthV2Coroutine>d__134 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public U8.SDK.U8SDKInterface <>4__this;  // 0x20
        public U8.SDK.SDKPromise<U8.SDK.U8AuthV2Result> promise;  // 0x28
        private U8.SDK.SDKPromiseWithResult<U8.SDK.U8GrantResult> <oauth2codePromise>5__2;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000152
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000153
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DF1A10  token: 0x6000154
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DF1CDC  token: 0x6000156
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000054  // size: 0x80
    public sealed class DataCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x07D884AC  token: 0x600019F
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x60001A0
        public virtual System.Void Invoke(System.Int32 index, System.String paramValue) { }

    }

    // TypeToken: 0x2000055  // size: 0x80
    public sealed class U8SDKSendMessageCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x08469FD0  token: 0x60001A1
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x60001A2
        public virtual System.Void Invoke(System.String methodName, System.String paramValue) { }

    }

    // TypeToken: 0x2000056  // size: 0x20
    public class LoginResult
    {
        // Fields
        public U8.SDK.SDKInterfaceCloud.LoginResultCode code;  // 0x10
        public Newtonsoft.Json.Linq.JToken msg;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60001A3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000057  // size: 0x14
    public sealed struct LoginResultCode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.SDKInterfaceCloud.LoginResultCode Success;  // const
        public static U8.SDK.SDKInterfaceCloud.LoginResultCode Canceled;  // const
        public static U8.SDK.SDKInterfaceCloud.LoginResultCode Failed;  // const

    }

    // TypeToken: 0x2000058  // size: 0x20
    public class PaymentResult
    {
        // Fields
        public U8.SDK.SDKInterfaceCloud.PaymentResultCode code;  // 0x10
        public Newtonsoft.Json.Linq.JToken msg;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60001A4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000059  // size: 0x14
    public sealed struct PaymentResultCode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.SDKInterfaceCloud.PaymentResultCode Success;  // const
        public static U8.SDK.SDKInterfaceCloud.PaymentResultCode Failed;  // const

    }

    // TypeToken: 0x200005A  // size: 0x14
    public sealed struct SetDataCode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.SDKInterfaceCloud.SetDataCode SplashProtocol;  // const
        public static U8.SDK.SDKInterfaceCloud.SetDataCode GameAgreement;  // const
        public static U8.SDK.SDKInterfaceCloud.SetDataCode SetLanguage;  // const
        public static U8.SDK.SDKInterfaceCloud.SetDataCode SetU8Token;  // const
        public static U8.SDK.SDKInterfaceCloud.SetDataCode ShowGameProtocol;  // const
        public static U8.SDK.SDKInterfaceCloud.SetDataCode QueryProductList;  // const
        public static U8.SDK.SDKInterfaceCloud.SetDataCode CustomerServiceCenter;  // const
        public static U8.SDK.SDKInterfaceCloud.SetDataCode CustomerUnreadMsgCount;  // const
        public static U8.SDK.SDKInterfaceCloud.SetDataCode CloudShareEvent;  // const

    }

    // TypeToken: 0x200005B  // size: 0x18
    public sealed class <>c__DisplayClass35_0
    {
        // Fields
        public System.String defaultValue;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60001A5
        public System.Void .ctor() { }
        // RVA: 0x02D40530  token: 0x60001A6
        private System.Void <LoadExtraConfig>b__0(System.String data) { }

    }

    // TypeToken: 0x200005C  // size: 0x20
    public sealed class <>c__DisplayClass45_0
    {
        // Fields
        public System.String defaultValue;  // 0x10
        public System.Int32 type;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60001A7
        public System.Void .ctor() { }
        // RVA: 0x02D40590  token: 0x60001A8
        private System.Void <GetData>b__0(System.String data) { }

    }

    // TypeToken: 0x200005D  // size: 0x18
    public sealed class <>c__DisplayClass55_0
    {
        // Fields
        public System.String json;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60001A9
        public System.Void .ctor() { }
        // RVA: 0x02D40530  token: 0x60001AA
        private System.Void <LoadSDKMeta>b__0(System.String data) { }

    }

    // TypeToken: 0x200005F  // size: 0x40
    public sealed struct <<HandleCloudShareEvent>b__0>d : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public U8.SDK.SDKInterfaceCloud.<>c__DisplayClass74_0 <>4__this;  // 0x30
        private System.Runtime.CompilerServices.TaskAwaiter <>u__1;  // 0x38

        // Methods
        // RVA: 0x02D3EC60  token: 0x60001AD
        private virtual System.Void MoveNext() { }
        // RVA: 0x02D40110  token: 0x60001AE
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x200005E  // size: 0x20
    public sealed class <>c__DisplayClass74_0
    {
        // Fields
        public U8.SDK.SDKInterfaceCloud <>4__this;  // 0x10
        public System.String jsonData;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60001AB
        public System.Void .ctor() { }
        // RVA: 0x02D40940  token: 0x60001AC
        private System.Threading.Tasks.Task <HandleCloudShareEvent>b__0() { }

    }

    // TypeToken: 0x2000060  // size: 0x48
    public sealed struct <SaveImageToTargetFolder>d__75 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public System.String jsonData;  // 0x30
        public U8.SDK.SDKInterfaceCloud <>4__this;  // 0x38
        private System.Runtime.CompilerServices.TaskAwaiter<System.Boolean> <>u__1;  // 0x40

        // Methods
        // RVA: 0x02D3C490  token: 0x60001AF
        private virtual System.Void MoveNext() { }
        // RVA: 0x02D3E840  token: 0x60001B0
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000061  // size: 0x20
    public sealed class <>c__DisplayClass77_0
    {
        // Fields
        public System.String methodName;  // 0x10
        public System.String paramValue;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60001B1
        public System.Void .ctor() { }
        // RVA: 0x02D41100  token: 0x60001B2
        private System.Void <SendMessageInMain>b__0() { }

    }

    // TypeToken: 0x2000063  // size: 0x80
    public sealed class DataCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x047562C0  token: 0x60001D9
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x60001DA
        public virtual System.Void Invoke(System.Int32 index, System.String paramValue) { }

    }

    // TypeToken: 0x2000064  // size: 0x80
    public sealed class U8SDKSendMessageCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x04756450  token: 0x60001DB
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x60001DC
        public virtual System.Void Invoke(System.String methodName, System.String paramValue) { }

    }

    // TypeToken: 0x2000065  // size: 0x18
    public sealed class <>c__DisplayClass34_0
    {
        // Fields
        public System.String defaultValue;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60001DD
        public System.Void .ctor() { }
        // RVA: 0x053908C0  token: 0x60001DE
        private System.Void <LoadExtraConfig>b__0(System.String data) { }

    }

    // TypeToken: 0x2000066  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly U8.SDK.SDKInterfaceWIN.<>c <>9;  // static @ 0x0
        public static System.Action<System.Threading.Tasks.Task> <>9__42_0;  // static @ 0x8

        // Methods
        // RVA: 0x09DF19AC  token: 0x60001DF
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60001E0
        public System.Void .ctor() { }
        // RVA: 0x09DF0440  token: 0x60001E1
        private System.Void <SetData>b__42_0(System.Threading.Tasks.Task task) { }

    }

    // TypeToken: 0x2000067  // size: 0x18
    public sealed class <>c__DisplayClass43_0
    {
        // Fields
        public System.String defaultValue;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60001E2
        public System.Void .ctor() { }
        // RVA: 0x053908C0  token: 0x60001E3
        private System.Void <GetData>b__0(System.String data) { }

    }

    // TypeToken: 0x2000068  // size: 0x18
    public sealed class <>c__DisplayClass51_0
    {
        // Fields
        public System.String json;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60001E4
        public System.Void .ctor() { }
        // RVA: 0x053908C0  token: 0x60001E5
        private System.Void <LoadSDKMeta>b__0(System.String data) { }

    }

    // TypeToken: 0x200006E
    public sealed struct State
    {
        // Fields
        public System.Int32 value__;  // 0x0
        public static U8.SDK.SDKPromiseEnumerator.State<T> NONE;  // const
        public static U8.SDK.SDKPromiseEnumerator.State<T> FULFILL;  // const
        public static U8.SDK.SDKPromiseEnumerator.State<T> REJECT;  // const

    }

    // TypeToken: 0x200006F
    public sealed class <Yield>d__12 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private System.Object <>2__current;  // 0x0
        public U8.SDK.SDKPromiseEnumerator<T> <>4__this;  // 0x0

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60001FE
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x60001FF
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000200
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000202
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000078  // size: 0x20
    public sealed class <_onLogoutNextFrameCoroutine>d__13 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000225
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000226
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09DF37F8  token: 0x6000227
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09DF38D0  token: 0x6000229
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200007D  // size: 0x14
    public sealed struct FolderType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.ImageSaver.FolderType UserPicturesFolder;  // const
        public static U8.SDK.ImageSaver.FolderType GameInstalledFolder;  // const

    }

    // TypeToken: 0x200007E  // size: 0x14
    public sealed struct ErrorCode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.ImageSaver.ErrorCode NotInited;  // const
        public static U8.SDK.ImageSaver.ErrorCode ParamsError;  // const
        public static U8.SDK.ImageSaver.ErrorCode ImageNotFound;  // const
        public static U8.SDK.ImageSaver.ErrorCode ShareChannelError;  // const
        public static U8.SDK.ImageSaver.ErrorCode SaveFailed;  // const
        public static U8.SDK.ImageSaver.ErrorCode UnknowError;  // const

    }

    // TypeToken: 0x200007F  // size: 0x11
    public sealed struct CallbackResult
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static U8.SDK.ImageSaver.CallbackResult Success;  // const
        public static U8.SDK.ImageSaver.CallbackResult Cancelled;  // const
        public static U8.SDK.ImageSaver.CallbackResult Failed;  // const

    }

    // TypeToken: 0x2000080  // size: 0x48
    public class ImageShareInfo
    {
        // Fields
        public System.Int32 shareChannel;  // 0x10
        public System.String extraData;  // 0x18
        public System.String imgPath;  // 0x20
        public System.String relativePath;  // 0x28
        public System.Int32 folderType;  // 0x30
        public System.String title;  // 0x38
        public System.String desc;  // 0x40

        // Methods
        // RVA: 0x041E1670  token: 0x6000247
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000081  // size: 0x50
    public sealed struct <SaveImage>d__5 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public U8.SDK.ImageSaver <>4__this;  // 0x30
        public System.String paramJson;  // 0x38
        private System.String <targetPath>5__2;  // 0x40
        private System.Runtime.CompilerServices.TaskAwaiter<System.Boolean> <>u__1;  // 0x48

        // Methods
        // RVA: 0x09DEFE58  token: 0x6000248
        private virtual System.Void MoveNext() { }
        // RVA: 0x09DF0278  token: 0x6000249
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000082  // size: 0x48
    public sealed struct <SaveCloudGameImage>d__6 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Boolean> <>t__builder;  // 0x18
        public U8.SDK.ImageSaver <>4__this;  // 0x30
        public System.String paramJson;  // 0x38
        private System.Runtime.CompilerServices.TaskAwaiter<System.Boolean> <>u__1;  // 0x40

        // Methods
        // RVA: 0x09DEF69C  token: 0x600024A
        private virtual System.Void MoveNext() { }
        // RVA: 0x09DEFAD0  token: 0x600024B
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000083  // size: 0x30
    public sealed class <>c__DisplayClass13_0
    {
        // Fields
        public U8.SDK.ImageSaver <>4__this;  // 0x10
        public System.String sourcePath;  // 0x18
        public System.String targetPath;  // 0x20
        public System.String imgFolder;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600024C
        public System.Void .ctor() { }
        // RVA: 0x09DF0694  token: 0x600024D
        private System.Boolean <SaveImageToPathAsync>b__0() { }

    }

    // TypeToken: 0x2000084  // size: 0x58
    public sealed struct <SaveImageToPathAsync>d__13 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Boolean> <>t__builder;  // 0x18
        public U8.SDK.ImageSaver <>4__this;  // 0x30
        public System.String sourcePath;  // 0x38
        public System.String targetPath;  // 0x40
        public System.String imgFolder;  // 0x48
        private System.Runtime.CompilerServices.TaskAwaiter<System.Boolean> <>u__1;  // 0x50

        // Methods
        // RVA: 0x09DEFB30  token: 0x600024E
        private virtual System.Void MoveNext() { }
        // RVA: 0x09DEFDF8  token: 0x600024F
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000085  // size: 0x20
    public sealed class <>c__DisplayClass24_0
    {
        // Fields
        public System.String methodName;  // 0x10
        public System.String paramValue;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000250
        public System.Void .ctor() { }
        // RVA: 0x09DF086C  token: 0x6000251
        private System.Void <SendMessageInMain>b__0() { }

    }

    // TypeToken: 0x2000089  // size: 0x14
    public sealed struct TOKEN
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.MiniJSON.Json.Parser.TOKEN NONE;  // const
        public static U8.SDK.MiniJSON.Json.Parser.TOKEN CURLY_OPEN;  // const
        public static U8.SDK.MiniJSON.Json.Parser.TOKEN CURLY_CLOSE;  // const
        public static U8.SDK.MiniJSON.Json.Parser.TOKEN SQUARED_OPEN;  // const
        public static U8.SDK.MiniJSON.Json.Parser.TOKEN SQUARED_CLOSE;  // const
        public static U8.SDK.MiniJSON.Json.Parser.TOKEN COLON;  // const
        public static U8.SDK.MiniJSON.Json.Parser.TOKEN COMMA;  // const
        public static U8.SDK.MiniJSON.Json.Parser.TOKEN STRING;  // const
        public static U8.SDK.MiniJSON.Json.Parser.TOKEN NUMBER;  // const
        public static U8.SDK.MiniJSON.Json.Parser.TOKEN TRUE;  // const
        public static U8.SDK.MiniJSON.Json.Parser.TOKEN FALSE;  // const
        public static U8.SDK.MiniJSON.Json.Parser.TOKEN NULL;  // const

    }

    // TypeToken: 0x2000088  // size: 0x18
    public sealed class Parser : System.IDisposable
    {
        // Fields
        private System.IO.StringReader json;  // 0x10

        // Properties
        System.Char PeekChar { get; /* RVA: 0x03140ED0 */ }
        System.Char NextChar { get; /* RVA: 0x03141060 */ }
        System.String NextWord { get; /* RVA: 0x03140AF0 */ }
        U8.SDK.MiniJSON.Json.Parser.TOKEN NextToken { get; /* RVA: 0x03140D50 */ }

        // Methods
        // RVA: 0x031410E0  token: 0x6000259
        public static System.Boolean IsWordBreak(System.Char c) { }
        // RVA: 0x035A6570  token: 0x600025A
        private System.Void .ctor(System.String jsonString) { }
        // RVA: 0x035A6470  token: 0x600025B
        public static System.Object Parse(System.String jsonString) { }
        // RVA: 0x04702E20  token: 0x600025C
        public virtual System.Void Dispose() { }
        // RVA: 0x03140BB0  token: 0x600025D
        private System.Collections.Generic.Dictionary<System.String,System.Object> ParseObject() { }
        // RVA: 0x09DE6CFC  token: 0x600025E
        private System.Collections.Generic.List<System.Object> ParseArray() { }
        // RVA: 0x03141150  token: 0x600025F
        private System.Object ParseValue() { }
        // RVA: 0x03141180  token: 0x6000260
        private System.Object ParseByToken(U8.SDK.MiniJSON.Json.Parser.TOKEN token) { }
        // RVA: 0x03140F30  token: 0x6000261
        private System.String ParseString() { }
        // RVA: 0x0321A290  token: 0x6000262
        private System.Object ParseNumber() { }
        // RVA: 0x03140CE0  token: 0x6000263
        private System.Void EatWhitespace() { }

    }

    // TypeToken: 0x200008A  // size: 0x18
    public sealed class Serializer
    {
        // Fields
        private System.Text.StringBuilder builder;  // 0x10

        // Methods
        // RVA: 0x0403C9D0  token: 0x6000268
        private System.Void .ctor() { }
        // RVA: 0x0403C950  token: 0x6000269
        public static System.String Serialize(System.Object obj) { }
        // RVA: 0x0403CBE0  token: 0x600026A
        private System.Void SerializeValue(System.Object value) { }
        // RVA: 0x0403CFB0  token: 0x600026B
        private System.Void SerializeObject(System.Collections.IDictionary obj) { }
        // RVA: 0x09DEDF14  token: 0x600026C
        private System.Void SerializeArray(System.Collections.IList anArray) { }
        // RVA: 0x0403CDA0  token: 0x600026D
        private System.Void SerializeString(System.String str) { }
        // RVA: 0x0403CA40  token: 0x600026E
        private System.Void SerializeOther(System.Object value) { }

    }

    // TypeToken: 0x200008E  // size: 0x28
    public sealed struct __StaticArrayInitTypeSize=24
    {
    }

    // TypeToken: 0x200008F  // size: 0x58
    public sealed struct __StaticArrayInitTypeSize=72
    {
    }

    // TypeToken: 0x200008D  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=72 C8939CFEA086350CA0F084D5A8FFA18BE802D605B6111130EB5143B5882250C4;  // static @ 0x0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=24 C9A1311D05AB372579F6B92A220603EAB8191E15071784A11774A076DC45320B;  // static @ 0x48

    }

namespace Hypergryph.PlatformFacade
{

    // TypeToken: 0x200008B  // size: 0x10
    public class PlatformFacade
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600026F
        public static System.Void SetupPlatformFont(Hypergryph.PlatformFacade.IPlatformFont iFont) { }

    }

    // TypeToken: 0x200008C
    public interface IPlatformFont
    {
    }

}

namespace U8.SDK
{

    // TypeToken: 0x2000002  // size: 0x38
    public sealed struct ExternalPluginLoginParams
    {
        // Fields
        public System.Action nativeLogin;  // 0x10
        public System.Action<System.String> nativeLoginCustom;  // 0x18
        public System.Action<System.String> nativeOnLoginSuc;  // 0x20
        public System.Action<System.String> nativeOnLoginFail;  // 0x28
        public System.Action<U8.SDK.U8MockLogin> markMockLogin;  // 0x30

    }

    // TypeToken: 0x2000003  // size: 0x28
    public sealed struct ExternalPluginLogoutParams
    {
        // Fields
        public System.Action nativeLogout;  // 0x10
        public System.Action nativeOnLogoutSuc;  // 0x18
        public System.Action nativeOnLogoutFail;  // 0x20

    }

    // TypeToken: 0x2000004  // size: 0x30
    public sealed struct ExternalPluginPayParams
    {
        // Fields
        public U8.SDK.U8PayParams payParams;  // 0x10
        public System.Func<U8.SDK.U8PayParams,U8.SDK.SDKPromise<U8.SDK.U8PayResult>> nativePay;  // 0x18
        public System.Action<U8.SDK.U8PayResult> onPaySuc;  // 0x20
        public System.Action<System.Object> onPayFail;  // 0x28

    }

    // TypeToken: 0x2000005
    public interface IExternalPlugin
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000001
        public virtual System.Void Init() { }
        // RVA: -1  // abstract  token: 0x6000002
        public virtual System.Void Login(U8.SDK.ExternalPluginLoginParams args) { }
        // RVA: -1  // abstract  token: 0x6000003
        public virtual System.Void Pay(U8.SDK.ExternalPluginPayParams args) { }
        // RVA: -1  // abstract  token: 0x6000004
        public virtual System.Void Logout(U8.SDK.ExternalPluginLogoutParams args) { }
        // RVA: -1  // abstract  token: 0x6000005
        public virtual System.String GetSDKToken() { }
        // RVA: -1  // abstract  token: 0x6000006
        public virtual System.Boolean TryLoadSDKMeta(System.Func<U8.SDK.SDKMeta> loadMetaNative, U8.SDK.SDKMeta& meta) { }
        // RVA: -1  // abstract  token: 0x6000007
        public virtual System.Void OnProductListChanged(System.Collections.Generic.List<U8.SDK.U8ProductInfo> productList) { }
        // RVA: -1  // abstract  token: 0x6000008
        public virtual System.String GetPayAddition() { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public abstract class SDKCaptchaHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000009
        public virtual System.Collections.IEnumerator FetchCaptchaCoroutine(System.String captchaParams, U8.SDK.SDKCaptchaHandler.Result outResult) { }
        // RVA: 0x041E1670  token: 0x600000A
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x28
    public abstract class SDKExternalTools
    {
        // Fields
        private static readonly System.Char[] RANDOM_CHAR_MAP;  // static @ 0x0
        protected static readonly System.Int32[] PAY_CONFIRM_RETRY_INTERVALS;  // static @ 0x8
        private static U8.SDK.SDKExternalTools s_instance;  // static @ 0x10
        private static U8.SDK.IExternalPlugin s_plugin;  // static @ 0x18
        private System.Boolean <inited>k__BackingField;  // 0x10
        private U8.SDK.SDKExternalTools.CaptchaMgr <captchaMgr>k__BackingField;  // 0x18
        private System.String <subChannel>k__BackingField;  // 0x20
        private static System.Int32 U8_SDK_SET_DATA_SET_SERVER;  // static @ 0x20

        // Properties
        System.Boolean inited { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }
        U8.SDK.IExternalPlugin plugin { get; /* RVA: 0x09DE9390 */ }
        U8.SDK.SDKExternalTools.CaptchaMgr captchaMgr { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.String subChannel { set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: -1  // generic def  token: 0x600001B
        public static System.Boolean SDKInterfaceInitExternalTools() { }
        // RVA: 0x035A5050  token: 0x600001C
        public static U8.SDK.SDKExternalTools GetInstance() { }
        // RVA: 0x09DE76D4  token: 0x600001D
        public static System.Boolean HasInstance() { }
        // RVA: 0x09DE7BB4  token: 0x600001E
        public System.String PublicServiceSignKey() { }
        // RVA: 0x09DE8580  token: 0x600001F
        public U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ProductInfo>> SDKInterfaceProductListV2() { }
        // RVA: 0x09DE7B5C  token: 0x6000020
        public System.Void NotifyProductListUpdated(System.Collections.Generic.List<U8.SDK.U8ProductInfo> productList) { }
        // RVA: 0x09DE8628  token: 0x6000021
        public System.String SDKInterfaceSDKToken() { }
        // RVA: 0x05395194  token: 0x6000022
        public static System.String MakeHttpPostParam(System.Collections.Generic.Dictionary<System.String,System.Object> param) { }
        // RVA: 0x03AFA240  token: 0x6000023
        public System.Void LoginWithPlugin(System.Action nativeLogin, System.Action<System.String> nativeLoginCustom, System.Action<U8.SDK.U8MockLogin> markMockLogin) { }
        // RVA: 0x09DE7908  token: 0x6000024
        public System.Void LogoutWithPlugin(System.Action nativeLogout) { }
        // RVA: 0x09DE8458  token: 0x6000025
        public System.Void SDKInterfacePayWithPlugin(U8.SDK.U8PayParams payParams, System.Func<U8.SDK.U8PayParams,U8.SDK.SDKPromise<U8.SDK.U8PayResult>> nativePay, System.Action<U8.SDK.U8PayResult> onSuc, System.Action<System.Object> onFail) { }
        // RVA: 0x09DE83A4  token: 0x6000026
        public System.String SDKInterfaceGetPayAddition() { }
        // RVA: 0x09DE87E0  token: 0x6000027
        public static System.Void StaticLog(System.String content) { }
        // RVA: 0x09DE8748  token: 0x6000028
        public static System.Void StaticLogWarning(System.String content) { }
        // RVA: 0x09DE86B0  token: 0x6000029
        public static System.Void StaticLogError(System.String content) { }
        // RVA: 0x035A1FB0  token: 0x600002A
        public static System.Void RegisterPlugin(U8.SDK.IExternalPlugin plugin) { }
        // RVA: -1  // generic def  token: 0x600002B
        protected static T GetValueSafe(System.Collections.Generic.Dictionary<System.String,System.Object> dict, System.String key, T defVal) { }
        // RVA: 0x09DE8878  token: 0x600002C
        protected System.String U8Url(System.String routeUrl) { }
        // RVA: 0x09DE891C  token: 0x600002D
        protected System.String U8urlWithoutU8(System.String routeUrl) { }
        // RVA: -1  // generic def  token: 0x600002E
        protected static U8.SDK.SDKExternalTools.ErrMsgMeta HandleResponseFromU8(U8.SDK.SDKExternalTools.BusType busType, U8.SDK.SDKExternalTools.POSTResult postRet, TResp& resp) { }
        // RVA: 0x09DE7610  token: 0x600002F
        protected static System.Collections.Generic.Dictionary<System.String,System.Object> FromMiniJSON(System.String json) { }
        // RVA: 0x09DE771C  token: 0x6000030
        protected static System.Void InternalInvokeCoroutine(System.Collections.IEnumerator coroutine) { }
        // RVA: 0x09DE7778  token: 0x6000031
        protected static System.Void InternalInvokeNextFrame(System.Action action) { }
        // RVA: 0x09DE91C0  token: 0x6000032
        private static System.Collections.IEnumerator _NextFrameCoroutine(System.Action action) { }
        // RVA: 0x035A2100  token: 0x6000033
        public System.Void InitIfNot() { }
        // RVA: 0x041E1670  token: 0x6000034
        protected virtual System.Void Init() { }
        // RVA: 0x041E1670  token: 0x6000035
        public virtual System.Void SwitchAccount() { }
        // RVA: 0x041E1670  token: 0x6000036
        public virtual System.Void OnSDKError(U8.SDK.SDKError error) { }
        // RVA: 0x041E1670  token: 0x6000037
        public virtual System.Void OnInvalidProduct(System.Int32 storeId) { }
        // RVA: 0x041E1670  token: 0x6000038
        public virtual System.Void OnSDKExtraInfo(System.String jsonData) { }
        // RVA: 0x09DE7804  token: 0x6000039
        public virtual U8.SDK.SDKMeta LoadSDKMetaFromPlugin(System.Func<U8.SDK.SDKMeta> nativeLoadMeta) { }
        // RVA: -1  // abstract  token: 0x600003A
        protected virtual U8.SDK.SDKCaptchaHandler CreateCaptchaHandler() { }
        // RVA: 0x011EC580  token: 0x600003B
        protected virtual System.String GetSignKey() { }
        // RVA: -1  // abstract  token: 0x600003C
        public virtual System.Collections.Generic.Dictionary<System.String,System.String> GetDeviceIDs() { }
        // RVA: -1  // abstract  token: 0x600003D
        protected virtual U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ProductInfo>> SendGetProductListRequestV2(System.String paramStr) { }
        // RVA: -1  // abstract  token: 0x600003E
        protected virtual System.Int32 GetPlatformKey() { }
        // RVA: -1  // abstract  token: 0x600003F
        protected virtual System.Void POSTImplementation(U8.SDK.SDKExternalTools.POSTRequest request, System.Action<U8.SDK.SDKExternalTools.POSTResult> callback) { }
        // RVA: -1  // abstract  token: 0x6000040
        protected virtual System.String U8RootUrl() { }
        // RVA: -1  // abstract  token: 0x6000041
        protected virtual System.String GetErrorMessage(U8.SDK.SDKExternalTools.ErrMsgMeta meta) { }
        // RVA: -1  // abstract  token: 0x6000042
        protected virtual System.Void Log(System.String content) { }
        // RVA: -1  // abstract  token: 0x6000043
        protected virtual System.Void LogWarning(System.String content) { }
        // RVA: -1  // abstract  token: 0x6000044
        protected virtual System.Void LogError(System.String content) { }
        // RVA: 0x04D36530  token: 0x6000045
        private System.Boolean _ValidatePlugin() { }
        // RVA: 0x09DE8A14  token: 0x6000046
        private static System.Void _AddAppIdOrAppCode(System.Collections.Generic.Dictionary<System.String,System.Object> paramDict, U8.SDK.SDKMeta meta) { }
        // RVA: 0x09DE8EE4  token: 0x6000047
        private static System.String _MakeGetProductListV2Param(U8.SDK.SDKMeta meta) { }
        // RVA: 0x09DE7F18  token: 0x6000048
        public System.Collections.IEnumerator SDKInterfaceAuthV2SessionToken(U8.SDK.SDKMeta meta, System.String channelToken, U8.SDK.SDKPromiseWithResult<U8.SDK.U8LoginV2Result> promise) { }
        // RVA: 0x09DE7E70  token: 0x6000049
        public System.Collections.IEnumerator SDKInterfaceAuthV2OAuth2Code(System.String sessionToken, U8.SDK.SDKPromiseWithResult<U8.SDK.U8GrantResult> promise, System.Action onSessionInvalid) { }
        // RVA: 0x09DE7FC0  token: 0x600004A
        public U8.SDK.SDKPromise<U8.SDK.U8ConfirmOrderResult> SDKInterfaceConfirmOrderU1(System.String orderId, System.String extension) { }
        // RVA: 0x09DE7D54  token: 0x600004B
        public U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ServerInfo>> SDKInterFaceGetServerList(System.String sessionToken) { }
        // RVA: 0x09DE7C00  token: 0x600004C
        public U8.SDK.SDKPromise<U8.SDK.U8ConfirmServerResult> SDKInterFaceConfirmServer(System.String sessionToken, System.String serverId) { }
        // RVA: 0x09DE8074  token: 0x600004D
        public U8.SDK.SDKPromise<U8.SDK.U8OrderInfo> SDKInterfaceCreateOrderU1(System.String sessionToken, System.String productId, System.String signParams) { }
        // RVA: 0x09DE900C  token: 0x600004E
        private static System.String _MakeGrantParam(System.String sessionToken, System.Int32 type, System.String captcha) { }
        // RVA: 0x09DE8AAC  token: 0x600004F
        private System.Collections.IEnumerator _AuthV2GrantImpl(System.String sessionToken, U8.SDK.SDKPromiseWithResult<U8.SDK.U8GrantResult> promise, System.Action onSessionInvalid) { }
        // RVA: 0x09DE8D30  token: 0x6000050
        private static System.String _MakeCheckOrderV1Param(System.String orderId, System.String extension) { }
        // RVA: 0x09DE8E28  token: 0x6000051
        private static System.String _MakeConfirmOrderV1Param(System.String orderId) { }
        // RVA: 0x09DE8B54  token: 0x6000052
        private System.Collections.IEnumerator _ConfirmOrderU1Coroutine(System.String orderId, System.String extension, U8.SDK.SDKPromise<U8.SDK.U8ConfirmOrderResult> promise) { }
        // RVA: 0x09DE921C  token: 0x6000053
        private System.Collections.IEnumerator _PollOrderStatusCoroutine(System.String orderId, System.String extension, U8.SDK.SDKPromise<U8.SDK.U8ConfirmOrderResult> promise) { }
        // RVA: 0x09DE8CA4  token: 0x6000054
        private System.Collections.IEnumerator _GetServerListCoroutine(System.String paramStr, U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ServerInfo>> promise) { }
        // RVA: 0x09DE8BFC  token: 0x6000055
        private System.Collections.IEnumerator _ConfirmServerListCoroutine(System.String paramStr, System.String serverId, U8.SDK.SDKPromise<U8.SDK.U8ConfirmServerResult> promise) { }
        // RVA: 0x09DE9304  token: 0x6000056
        private System.Collections.IEnumerator _SendCreateOrderU1Coroutine(System.String paramStr, U8.SDK.SDKPromise<U8.SDK.U8OrderInfo> promise) { }
        // RVA: 0x09DE92A8  token: 0x6000057
        private System.Collections.IEnumerator _SendCreateOrderFailCoroutine(U8.SDK.SDKPromise<U8.SDK.U8OrderInfo> promise) { }
        // RVA: 0x041E1670  token: 0x6000058
        protected System.Void .ctor() { }
        // RVA: 0x04B47280  token: 0x6000059
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002B  // size: 0x20
    public sealed struct SDKExtraData
    {
        // Fields
        public static readonly U8.SDK.SDKExtraData EMPTY;  // static @ 0x0
        public System.Int32 code;  // 0x10
        public System.Collections.Generic.Dictionary<System.String,System.Object> msg;  // 0x18

        // Methods
        // RVA: 0x04441A40  token: 0x60000D4
        public static U8.SDK.SDKExtraData FromJson(System.String jsonStr) { }
        // RVA: 0x04D4E810  token: 0x60000D5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002C  // size: 0x28
    public abstract class SDKExternalToolsShortcut : U8.SDK.SDKExternalTools
    {
        // Methods
        // RVA: 0x09DE7404  token: 0x60000D6
        protected virtual U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ProductInfo>> SendGetProductListRequestV2(System.String paramStr) { }
        // RVA: 0x09DE7100  token: 0x60000D7
        public virtual System.Collections.Generic.Dictionary<System.String,System.String> GetDeviceIDs() { }
        // RVA: 0x09DE720C  token: 0x60000D8
        public static System.String GetU8DeviceID() { }
        // RVA: 0x09DE732C  token: 0x60000D9
        public static System.Void InjectRequestHeaders(System.Collections.Generic.Dictionary<System.String,System.String>& headers, U8.SDK.SDKExternalToolsShortcut.RequestHeaderInjectOptions policy) { }
        // RVA: 0x09DE75D4  token: 0x60000DA
        private System.String _GetProductListV1Url() { }
        // RVA: 0x04D778E0  token: 0x60000DB
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000031  // size: 0xD0
    public abstract class U8SDKInterface
    {
        // Fields
        private static U8.SDK.U8SDKInterface m_instance;  // static @ 0x0
        protected U8.SDK.SDKPromiseWrapper m_initPromise;  // 0x10
        protected U8.SDK.SDKPromiseWrapper m_loginPromise;  // 0x18
        protected U8.SDK.SDKPromiseWrapper m_logoutPromise;  // 0x20
        protected U8.SDK.SDKPromiseWrapper m_payPromise;  // 0x28
        private U8.SDK.U8LoginResult m_loginResult;  // 0x30
        private U8.SDK.U8SDKInterface.AuthStatus m_authStatus;  // 0x68
        private System.String m_loginExtV1;  // 0x88
        private U8.SDK.U8MockLogin m_mockLogin;  // 0x90
        private System.Collections.Generic.List<U8.SDK.U8ProductInfo> m_productList;  // 0xa0
        private System.Nullable<System.Boolean> m_isNativePlugin;  // 0xa8
        private U8.SDK.U8SDKInterface.V2LoginStatus m_loginStatusV2;  // 0xb0
        private static System.Int32 U8_SDK_SET_DATA_SET_SESSION_TOKEN;  // static @ 0x8
        private static System.Int32 U8_SDK_SET_DATA_GET_PRODUCT_LIST;  // static @ 0xc
        private static U8.SDK.U8SDKInterface.ReceiverProductListResult onReceiverProductlist;  // static @ 0x10
        private U8.SDK.U8SDKInterface.GameServerTimeCache m_gameServerTimeCache;  // 0xb8

        // Properties
        U8.SDK.U8SDKInterface Instance { get; /* RVA: 0x03A72EC0 */ }
        System.Boolean isNativePlugin { get; /* RVA: 0x03AF78D0 */ }
        System.String uid { get; /* RVA: 0x04D86230 */ }
        System.String cachedUid { get; /* RVA: 0x03811920 */ set; /* RVA: 0x03F4BAD0 */ }
        System.String oauth2token { get; /* RVA: 0x04D862A0 */ }
        System.String oauth2code { get; /* RVA: 0x04D862B0 */ }
        System.Boolean isInterfaceDisabled { get; /* RVA: 0x012081B0 */ }
        System.String sessionToken { get; /* RVA: 0x04D86250 */ }
        U8.SDK.U8SDKInterface.GameServerTimeCache serverCacheTime { get; /* RVA: 0x04DA2EA0 */ set; /* RVA: 0x04DA2EC0 */ }

        // Events
        event U8.SDK.U8SDKInterface.ReceiverProductListResult onReceiverProductlist;

        // Methods
        // RVA: 0x049DAA10  token: 0x60000E3
        public System.Void CallbackInitSuc(System.String extConfigs) { }
        // RVA: 0x09DF4BB4  token: 0x60000E4
        public System.Void CallbackInitFail(System.String info) { }
        // RVA: 0x049DAA90  token: 0x60000E5
        public System.Void CallbackLoginSuc(System.String extension) { }
        // RVA: 0x09DF4BD4  token: 0x60000E6
        public System.Void CallbackLoginFail(System.String info) { }
        // RVA: 0x09DF4BF4  token: 0x60000E7
        public System.Void CallbackLogout() { }
        // RVA: 0x09DF4D40  token: 0x60000E8
        public System.Void CallbackSwitchAccount() { }
        // RVA: 0x09DF4CAC  token: 0x60000E9
        public System.Void CallbackPaySuc(System.String jsonData) { }
        // RVA: 0x09DF4C18  token: 0x60000EA
        public System.Void CallbackPayFail(System.String failMsg) { }
        // RVA: 0x05395664  token: 0x60000F1
        public System.String PublicLoadExtraConfig() { }
        // RVA: 0x053981E0  token: 0x60000F2
        public System.Void PublicSetGameVersion(System.String version) { }
        // RVA: 0x09DF5F70  token: 0x60000F3
        public U8.SDK.U8SDKInterface.ReceiverProductListResult getOnReceiverProductlist() { }
        // RVA: -1  // generic def  token: 0x60000F4
        public U8.SDK.SDKPromise<System.String> V2PublicInit(System.String env) { }
        // RVA: 0x04D84E50  token: 0x60000F5
        private System.Void _MarkMockLogin(U8.SDK.U8MockLogin mockLogin) { }
        // RVA: 0x04CA4650  token: 0x60000F6
        private System.Void _onLoginSuc(System.String uid, System.Boolean isNew) { }
        // RVA: 0x09DF51D0  token: 0x60000F7
        public U8.SDK.SDKPromise<U8.SDK.U8ProductsResult> PublicGetProductListV2() { }
        // RVA: 0x09DF4D8C  token: 0x60000F8
        public System.Void ClearAuthStatus() { }
        // RVA: 0x09DF4DE8  token: 0x60000F9
        public System.Void ClearLoginStatus() { }
        // RVA: 0x03F4BA90  token: 0x60000FA
        private System.Void _ConfirmAuthResult(U8.SDK.U8SDKInterface.AuthStatus result) { }
        // RVA: 0x09DF53D4  token: 0x60000FB
        public U8.SDK.SDKPromise<System.Object> PublicLogout() { }
        // RVA: 0x09DF52F0  token: 0x60000FC
        public U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ServerInfo>> PublicGetServerList() { }
        // RVA: 0x09DF56BC  token: 0x60000FD
        public U8.SDK.SDKPromise<U8.SDK.U8ConfirmServerResult> PubliceConfirmServer(System.String serverId) { }
        // RVA: 0x09DF5474  token: 0x60000FE
        public U8.SDK.SDKPromise<U8.SDK.U8PayResult> PublicPay(System.Int32 storeId, U8.SDK.U8OrderInfo orderInfo) { }
        // RVA: 0x09DF5CF0  token: 0x60000FF
        private System.Void _OnNativePayFulfilled(System.Int64 revenue) { }
        // RVA: 0x09DF5DCC  token: 0x6000100
        private System.Boolean _TryGetProduct(System.Int32 storeId, U8.SDK.U8ProductInfo& productInfo) { }
        // RVA: 0x09DF5A70  token: 0x6000101
        private U8.SDK.U8PayParams _GeneratePayParams(U8.SDK.U8ProductInfo productInfo, U8.SDK.U8OrderInfo orderInfo) { }
        // RVA: 0x09DF5C0C  token: 0x6000102
        private U8.SDK.SDKPromise<U8.SDK.U8PayResult> _NativePay(U8.SDK.U8PayParams payParams) { }
        // RVA: -1  // generic def  token: 0x6000103
        private static T GetValueSafe(System.Collections.Generic.Dictionary<System.String,System.Object> dict, System.String key, T defVal) { }
        // RVA: 0x09DF52F8  token: 0x6000104
        public U8.SDK.SDKMeta PublicLoadSDKMeta() { }
        // RVA: -1  // abstract  token: 0x6000105
        protected virtual System.String LoadExtraConfig() { }
        // RVA: -1  // abstract  token: 0x6000106
        protected virtual System.Void V2Init(System.String env) { }
        // RVA: -1  // abstract  token: 0x6000107
        protected virtual System.Void Login() { }
        // RVA: -1  // abstract  token: 0x6000108
        protected virtual System.Void LoginCustom(System.String customData) { }
        // RVA: -1  // abstract  token: 0x6000109
        protected virtual System.Boolean Logout() { }
        // RVA: -1  // abstract  token: 0x600010A
        public virtual System.Boolean ShowAccountCenter() { }
        // RVA: -1  // abstract  token: 0x600010B
        public virtual System.Void SubmitGameData(U8.SDK.U8ExtraGameData data) { }
        // RVA: -1  // abstract  token: 0x600010C
        protected virtual System.Void Pay(U8.SDK.U8PayParams data) { }
        // RVA: -1  // abstract  token: 0x600010D
        public virtual System.Void SetData(System.Int32 type, System.String paramJson) { }
        // RVA: -1  // abstract  token: 0x600010E
        public virtual System.String GetData(System.Int32 type, System.String paramJson) { }
        // RVA: -1  // abstract  token: 0x600010F
        protected virtual U8.SDK.SDKMeta LoadSDKMeta() { }
        // RVA: -1  // abstract  token: 0x6000110
        protected virtual System.Boolean IsNativePlugin() { }
        // RVA: -1  // abstract  token: 0x6000111
        public virtual System.Void SetGameVersion(System.String version) { }
        // RVA: 0x03A84D40  token: 0x6000112
        private System.Collections.IEnumerator _NextFrameCoroutine(System.Action call) { }
        // RVA: 0x03A84CB0  token: 0x6000113
        protected System.Void InvokeNextFrame(System.Action action) { }
        // RVA: 0x040EB180  token: 0x6000114
        protected System.Void InvokeCoroutine(System.Collections.IEnumerator coroutine) { }
        // RVA: 0x03AF99C0  token: 0x6000115
        protected System.String EncodeGameData(U8.SDK.U8ExtraGameData data) { }
        // RVA: 0x09DF4E14  token: 0x6000116
        protected System.String EncodePayParams(U8.SDK.U8PayParams data) { }
        // RVA: 0x040EB080  token: 0x600011C
        public U8.SDK.SDKPromise<System.String> V2PublicLogin() { }
        // RVA: 0x03A84850  token: 0x600011D
        public U8.SDK.SDKPromise<U8.SDK.U8AuthV2Result> V2PublicAuth() { }
        // RVA: 0x09DF5798  token: 0x600011E
        public U8.SDK.SDKPromise<U8.SDK.U8OrderInfo> U1PublicCreateOrder(System.String productId, System.String signParams) { }
        // RVA: 0x09DF5818  token: 0x600011F
        public U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ServerInfo>> U1PublicGetServerList() { }
        // RVA: 0x09DF572C  token: 0x6000120
        public U8.SDK.SDKPromise<U8.SDK.U8ConfirmServerResult> U1PublicConfirmServer(System.String serverId) { }
        // RVA: 0x09DF56C4  token: 0x6000121
        public U8.SDK.SDKPromise<U8.SDK.U8ConfirmOrderResult> U1PublicConfirmOrder(System.String orderId, System.String extension) { }
        // RVA: 0x03A848B0  token: 0x6000122
        private U8.SDK.SDKPromise<U8.SDK.U8AuthV2Result> _DoMockAuthV2() { }
        // RVA: 0x040EB100  token: 0x6000123
        private System.Collections.IEnumerator _LoginV2Coroutine(U8.SDK.SDKPromise<System.String> promise) { }
        // RVA: 0x09DF5DAC  token: 0x6000124
        private System.Void _OnSessionTokenInvalid() { }
        // RVA: 0x09DF59FC  token: 0x6000125
        private System.Collections.IEnumerator _AuthV2Coroutine(U8.SDK.SDKPromise<U8.SDK.U8AuthV2Result> promise) { }
        // RVA: 0x047564D0  token: 0x6000126
        protected System.Void .ctor() { }
        // RVA: 0x04D5D230  token: 0x6000127
        private static System.Void .cctor() { }
        // RVA: 0x09DF5874  token: 0x6000128
        private System.Void <PublicLogout>b__65_0() { }
        // RVA: 0x09DF5998  token: 0x6000129
        private System.Void <PublicLogout>b__65_1() { }
        // RVA: 0x09DF59A8  token: 0x600012A
        private System.Void <PublicLogout>b__65_2() { }

    }

    // TypeToken: 0x2000041  // size: 0x20
    public sealed struct SDKError
    {
        // Fields
        private System.String <code>k__BackingField;  // 0x10
        private System.String <msg>k__BackingField;  // 0x18

        // Properties
        System.String code { set; /* RVA: 0x05392320 */ }
        System.String msg { set; /* RVA: 0x067D5E9C */ }

        // Methods
        // RVA: 0x09DE6ED8  token: 0x600015A
        public System.Void .ctor(System.String content) { }

    }

    // TypeToken: 0x2000042  // size: 0x20
    public sealed struct U8MockLogin
    {
        // Fields
        public static readonly U8.SDK.U8MockLogin EMPTY;  // static @ 0x0
        public System.String uid;  // 0x10
        public System.String token;  // 0x18

        // Methods
        // RVA: 0x05394A74  token: 0x600015B
        public System.Boolean IsEmpty() { }

    }

    // TypeToken: 0x2000043  // size: 0x48
    public class U8ProductInfo
    {
        // Fields
        public System.Int32 store_id;  // 0x10
        public System.String product_id;  // 0x18
        public System.String desc;  // 0x20
        public System.String name;  // 0x28
        public System.Int64 price;  // 0x30
        public System.String channel_product_id;  // 0x38
        public System.String display_price;  // 0x40

        // Methods
        // RVA: 0x041E1670  token: 0x600015C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000044  // size: 0x20
    public class U8ProductsResult
    {
        // Fields
        public System.Int32 status;  // 0x10
        public System.Collections.Generic.List<U8.SDK.U8ProductInfo> u8Products;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600015D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000045  // size: 0x50
    public class U8ServerInfo
    {
        // Fields
        public System.String serverId;  // 0x10
        public System.String serverName;  // 0x18
        public System.String serverDomain;  // 0x20
        public System.Boolean defaultChoose;  // 0x28
        public System.String roleId;  // 0x30
        public System.String nickName;  // 0x38
        public System.Int64 level;  // 0x40
        public System.String extension;  // 0x48

        // Methods
        // RVA: 0x041E1670  token: 0x600015E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000046  // size: 0x18
    public class U8ServerErrorInfo
    {
        // Fields
        public System.Int32 status;  // 0x10
        public System.Int32 errorCode;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x600015F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000047  // size: 0x40
    public class U8OrderInfo
    {
        // Fields
        public System.String orderId;  // 0x10
        public System.String channelProductCode;  // 0x18
        public System.String productName;  // 0x20
        public System.String productDesc;  // 0x28
        public System.Int64 amount;  // 0x30
        public System.String extension;  // 0x38

        // Methods
        // RVA: 0x041E1670  token: 0x6000160
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000048  // size: 0x98
    public class U8PayParams
    {
        // Fields
        public System.String productId;  // 0x10
        public System.String productName;  // 0x18
        public System.String productDesc;  // 0x20
        public System.Int64 price;  // 0x28
        public System.Int32 buyNum;  // 0x30
        public System.Int32 coinNum;  // 0x34
        public System.String serverId;  // 0x38
        public System.String serverName;  // 0x40
        public System.String roleId;  // 0x48
        public System.String roleName;  // 0x50
        public System.Int32 roleLevel;  // 0x58
        public System.String vip;  // 0x60
        public System.String orderID;  // 0x68
        public System.String worldID;  // 0x70
        public System.String token;  // 0x78
        public System.String appKey;  // 0x80
        public System.String extension;  // 0x88
        public System.String addition;  // 0x90

        // Methods
        // RVA: 0x041E1670  token: 0x6000161
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000049  // size: 0x78
    public class U8ExtraGameData
    {
        // Fields
        public System.Int32 dataType;  // 0x10
        public System.String uid;  // 0x18
        public System.String roleID;  // 0x20
        public System.String roleName;  // 0x28
        public System.String roleLevel;  // 0x30
        public System.Int32 serverID;  // 0x38
        public System.String serverName;  // 0x40
        public System.String channel;  // 0x48
        public System.String subChannel;  // 0x50
        public System.Boolean isNewUser;  // 0x58
        public System.Int64 revenue;  // 0x60
        public System.String customEventName;  // 0x68
        public System.String customEventParams;  // 0x70

        // Methods
        // RVA: 0x041E1670  token: 0x6000162
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004A  // size: 0x48
    public sealed struct U8LoginResult
    {
        // Fields
        public static readonly U8.SDK.U8LoginResult EMPTY;  // static @ 0x0
        public System.Int32 result;  // 0x10
        public System.String uid;  // 0x18
        public System.String channelUid;  // 0x20
        public System.String token;  // 0x28
        public System.String extension;  // 0x30
        public System.Boolean isGuest;  // 0x38
        public System.Boolean isNew;  // 0x39
        public System.String error;  // 0x40

        // Methods
        // RVA: 0x09DF391C  token: 0x6000163
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200004B  // size: 0x18
    public sealed struct U8ProductError
    {
        // Fields
        public System.Int32 status;  // 0x10
        public System.Int32 errorCode;  // 0x14

    }

    // TypeToken: 0x200004C  // size: 0x14
    public sealed struct PayResultStatus
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.PayResultStatus SUCCEED;  // const
        public static U8.SDK.PayResultStatus PENDING;  // const

    }

    // TypeToken: 0x200004D  // size: 0x14
    public sealed struct PayFailStatus
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.PayFailStatus UNKNOWN;  // const
        public static U8.SDK.PayFailStatus CANCELED;  // const
        public static U8.SDK.PayFailStatus ERROR;  // const
        public static U8.SDK.PayFailStatus HALT;  // const

    }

    // TypeToken: 0x200004E  // size: 0x20
    public class U8ConfirmServerResult
    {
        // Fields
        public System.Int32 status;  // 0x10
        public System.String message;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000164
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x14
    public sealed struct U8ConfirmOrderStatus
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.U8ConfirmOrderStatus NONE;  // const
        public static U8.SDK.U8ConfirmOrderStatus INTERNAL_ERROR;  // const
        public static U8.SDK.U8ConfirmOrderStatus TIMEOUT;  // const
        public static U8.SDK.U8ConfirmOrderStatus INVALID_ORDER;  // const
        public static U8.SDK.U8ConfirmOrderStatus SUCCEED;  // const

    }

    // TypeToken: 0x2000050  // size: 0x20
    public class U8ConfirmOrderResult
    {
        // Fields
        public U8.SDK.U8ConfirmOrderStatus status;  // 0x10
        public System.String message;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000165
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000051  // size: 0x28
    public sealed struct U8PayFailMsg
    {
        // Fields
        public static U8.SDK.U8PayFailMsg EMPTY;  // static @ 0x0
        public U8.SDK.PayFailStatus status;  // 0x10
        public System.String message;  // 0x18
        public System.String extension;  // 0x20

        // Methods
        // RVA: 0x09DF3B64  token: 0x6000166
        public static U8.SDK.U8PayFailMsg FromJson(System.String jsonStr) { }
        // RVA: 0x09DF3F54  token: 0x6000167
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000052  // size: 0x28
    public sealed struct U8PayResult
    {
        // Fields
        public static readonly U8.SDK.U8PayResult EMPTY;  // static @ 0x0
        public U8.SDK.PayResultStatus status;  // 0x10
        public System.String outTradeNo;  // 0x18
        public System.String extension;  // 0x20

        // Methods
        // RVA: 0x09DF3FB8  token: 0x6000168
        public static U8.SDK.U8PayResult FromJson(System.String jsonData) { }
        // RVA: 0x09DF4338  token: 0x6000169
        public virtual System.String ToString() { }
        // RVA: 0x09DF4424  token: 0x600016A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000053  // size: 0xE0
    public class SDKInterfaceCloud : U8.SDK.U8SDKInterface
    {
        // Fields
        private System.Int32 actionIndex;  // 0xd0
        private static System.Collections.Generic.Dictionary<System.Int32,System.Action<System.String>> dictAction;  // static @ 0x0
        private System.String languageParam;  // 0xd8

        // Methods
        // RVA: 0x09DED060  token: 0x600016B
        public System.Void .ctor() { }
        // RVA: 0x09DEA138  token: 0x600016C
        protected virtual System.Void Finalize() { }
        // RVA: 0x09DE95CC  token: 0x600016D
        public System.Int32 AddAction(System.Action<System.String> action) { }
        // RVA: 0x09DE9F4C  token: 0x600016E
        public static System.Void DataActionCallback(System.Int32 index, System.String paramValue) { }
        // RVA: 0x09DE93E0  token: 0x600016F
        public static System.Void ASyncDataActionCallback(System.Int32 index, System.String paramValue) { }
        // RVA: 0x09DECE5C  token: 0x6000170
        public static System.Void U8SDKSendMessage(System.String methodName, System.String paramValue) { }
        // RVA: 0x09DEA790  token: 0x6000171
        public static System.Void HGU8SDKInitLibrary(System.String windowText, System.Int64 windowHandle) { }
        // RVA: 0x09DEA8EC  token: 0x6000172
        public static System.Int32 HGU8SDKSetUserDataPath(System.String path) { }
        // RVA: 0x09DEA940  token: 0x6000173
        public static System.Void HGU8SDKUnInitLibrary() { }
        // RVA: 0x09DEA740  token: 0x6000174
        public static System.Void HGU8SDKInitDatacallback(U8.SDK.SDKInterfaceCloud.DataCallback Callback, U8.SDK.SDKInterfaceCloud.DataCallback asyncCallback) { }
        // RVA: 0x09DEA7E0  token: 0x6000175
        public static System.Void HGU8SDKInitSendMessage(U8.SDK.SDKInterfaceCloud.U8SDKSendMessageCallback callback) { }
        // RVA: 0x09DEA964  token: 0x6000176
        public static System.Void HGU8SDKV2Init(System.String env) { }
        // RVA: 0x09DEA714  token: 0x6000177
        public static System.Void HGU8SDKGetSDKMeta(System.Int32 index) { }
        // RVA: 0x09DEA854  token: 0x6000178
        public static System.Void HGU8SDKSetData(System.Int32 code, System.String paramJson) { }
        // RVA: 0x09DEA6B4  token: 0x6000179
        public static System.Void HGU8SDKGetData(System.Int32 code, System.String paramJson, System.Int32 index) { }
        // RVA: 0x09DEA828  token: 0x600017A
        public static System.Void HGU8SDKLoadExtraConfig(System.Int32 index) { }
        // RVA: 0x09DEA8A4  token: 0x600017B
        public static System.Void HGU8SDKSetGameVersion(System.String version) { }
        // RVA: 0x09DEB5A4  token: 0x600017C
        protected virtual System.String LoadExtraConfig() { }
        // RVA: 0x09DECEC4  token: 0x600017D
        protected virtual System.Void V2Init(System.String env) { }
        // RVA: 0x09DEBAFC  token: 0x600017E
        protected virtual System.Void Login() { }
        // RVA: 0x09DEB7D0  token: 0x600017F
        private System.Void LoginCallback(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DEBC1C  token: 0x6000180
        protected virtual System.Boolean Logout() { }
        // RVA: 0x09DEBAEC  token: 0x6000181
        protected virtual System.Void LoginCustom(System.String customData) { }
        // RVA: 0x09DECD8C  token: 0x6000182
        public virtual System.Void SubmitGameData(U8.SDK.U8ExtraGameData data) { }
        // RVA: 0x09DEC530  token: 0x6000183
        public virtual System.Void SetData(System.Int32 type, System.String paramJson) { }
        // RVA: 0x09DEA298  token: 0x6000184
        public virtual System.String GetData(System.Int32 type, System.String paramJson) { }
        // RVA: 0x09DECA34  token: 0x6000185
        public virtual System.Boolean ShowAccountCenter() { }
        // RVA: 0x09DEC7CC  token: 0x6000186
        private System.Void ShowAccountCenterCallback(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DEC15C  token: 0x6000187
        protected virtual System.Void Pay(U8.SDK.U8PayParams data) { }
        // RVA: 0x09DEBE40  token: 0x6000188
        private System.Void PayCallback(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DEB6A0  token: 0x6000189
        protected virtual U8.SDK.SDKMeta LoadSDKMeta() { }
        // RVA: 0x02FFF600  token: 0x600018A
        protected virtual System.Boolean IsNativePlugin() { }
        // RVA: 0x09DEC6FC  token: 0x600018B
        public virtual System.Void SetGameVersion(System.String version) { }
        // RVA: 0x09DEB550  token: 0x600018C
        private System.Void HandleSplashProtocol(System.String paramJson) { }
        // RVA: 0x02D39980  token: 0x600018D
        private System.Void HandleGameAgreement(System.String paramJson) { }
        // RVA: 0x02D3B430  token: 0x600018E
        private System.Void HandleSetLanguage(System.String paramJson) { }
        // RVA: 0x09DEB304  token: 0x600018F
        private System.Void HandleSetU8Token(System.String paramJson) { }
        // RVA: 0x09DEB410  token: 0x6000190
        private System.Void HandleShowGameProtocol(System.String paramJson) { }
        // RVA: 0x09DECB30  token: 0x6000191
        private System.Void ShowGameProtocolCallback(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DEB0A8  token: 0x6000192
        private System.Void HandleQueryProductList(System.String paramJson) { }
        // RVA: 0x09DEAE28  token: 0x6000193
        private System.Void HandleCustomerServiceCenter(System.String paramJson) { }
        // RVA: 0x09DE9A0C  token: 0x6000194
        private System.Void CustomerServiceCenterCallback(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DEAF68  token: 0x6000195
        private System.Void HandleCustomerUnreadMsgCount(System.String paramJson) { }
        // RVA: 0x09DE9C68  token: 0x6000196
        private System.Void CustomerUnreadMsgCountCallback(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DEA9A4  token: 0x6000197
        private System.Void HandleCloudShareEvent(System.String paramJson, System.String fileNamePrefix) { }
        // RVA: 0x09DEC388  token: 0x6000198
        private System.Threading.Tasks.Task SaveImageToTargetFolder(System.String jsonData) { }
        // RVA: 0x09DE9724  token: 0x6000199
        private System.Void CloudShareEventCallback(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DEC458  token: 0x600019A
        private System.Void SendMessageInMain(System.String methodName, System.String paramValue) { }
        // RVA: 0x09DEBC6C  token: 0x600019B
        private static System.Void OnPipelineConnected(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DEC274  token: 0x600019C
        private static System.Void RequestLaunchInfoCallback(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DEA3BC  token: 0x600019D
        private static System.Void GetThinClientDeviceInfo(System.String jsonData) { }
        // RVA: 0x09DECFD4  token: 0x600019E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000062  // size: 0xD8
    public class SDKInterfaceWIN : U8.SDK.U8SDKInterface
    {
        // Fields
        private System.Int32 actionIndex;  // 0xd0
        private static System.Collections.Generic.Dictionary<System.Int32,System.Action<System.String>> dictAction;  // static @ 0x0

        // Methods
        // RVA: 0x04756120  token: 0x60001B3
        public System.Void .ctor() { }
        // RVA: 0x09DED42C  token: 0x60001B4
        protected virtual System.Void Finalize() { }
        // RVA: 0x03AFA8E0  token: 0x60001B5
        public System.Int32 AddAction(System.Action<System.String> action) { }
        // RVA: 0x03A849A0  token: 0x60001B6
        public static System.Void DataActionCallback(System.Int32 index, System.String paramValue) { }
        // RVA: 0x09DED240  token: 0x60001B7
        public static System.Void ASyncDataActionCallback(System.Int32 index, System.String paramValue) { }
        // RVA: 0x03A84DA0  token: 0x60001B8
        public static System.Void U8SDKSendMessage(System.String methodName, System.String paramValue) { }
        // RVA: 0x047563F0  token: 0x60001B9
        public static System.Void HGU8SDKInitLibrary(System.String windowText, System.Int64 windowHandle) { }
        // RVA: 0x04756390  token: 0x60001BA
        public static System.Int32 HGU8SDKSetUserDataPath(System.String path) { }
        // RVA: 0x09DED76C  token: 0x60001BB
        public static System.Void HGU8SDKUnInitLibrary() { }
        // RVA: 0x03826E60  token: 0x60001BC
        public static System.Void HGU8SDKInitDatacallback(U8.SDK.SDKInterfaceWIN.DataCallback Callback, U8.SDK.SDKInterfaceWIN.DataCallback asyncCallback) { }
        // RVA: 0x03826BA0  token: 0x60001BD
        public static System.Void HGU8SDKInitSendMessage(U8.SDK.SDKInterfaceWIN.U8SDKSendMessageCallback callback) { }
        // RVA: 0x0488EC70  token: 0x60001BE
        public static System.Void HGU8SDKV2Init(System.String env) { }
        // RVA: 0x09DED5AC  token: 0x60001BF
        public static System.Void HGU8SDKLogin() { }
        // RVA: 0x09DED524  token: 0x60001C0
        public static System.Void HGU8SDKLoginCustom(System.String json) { }
        // RVA: 0x09DED614  token: 0x60001C1
        public static System.Int32 HGU8SDKLogout() { }
        // RVA: 0x09DED704  token: 0x60001C2
        public static System.Int32 HGU8SDKShowAccountCenter() { }
        // RVA: 0x09DED67C  token: 0x60001C3
        public static System.Void HGU8SDKPay(System.String json) { }
        // RVA: 0x03AF9CD0  token: 0x60001C4
        public static System.Void HGU8SDKSubmitGameData(System.String json) { }
        // RVA: 0x09DED4F8  token: 0x60001C5
        public static System.Void HGU8SDKGetSDKMeta(System.Int32 index) { }
        // RVA: 0x03A86A60  token: 0x60001C6
        public static System.Void HGU8SDKSetData(System.Int32 code, System.String paramJson) { }
        // RVA: 0x03AFAE80  token: 0x60001C7
        public static System.Void HGU8SDKGetData(System.Int32 code, System.String paramJson, System.Int32 index) { }
        // RVA: 0x03826C30  token: 0x60001C8
        public static System.Int32 HGU8SDKIsNativePlugin() { }
        // RVA: 0x03AFAEF0  token: 0x60001C9
        public static System.Void HGU8SDKLoadExtraConfig(System.Int32 index) { }
        // RVA: 0x04CC88A0  token: 0x60001CA
        public static System.Void HGU8SDKSetGameVersion(System.String version) { }
        // RVA: 0x03AFA690  token: 0x60001CB
        protected virtual System.String LoadExtraConfig() { }
        // RVA: 0x0488EB00  token: 0x60001CC
        protected virtual System.Void V2Init(System.String env) { }
        // RVA: 0x09DED90C  token: 0x60001CD
        protected virtual System.Void Login() { }
        // RVA: 0x09DED94C  token: 0x60001CE
        protected virtual System.Boolean Logout() { }
        // RVA: 0x09DED8C0  token: 0x60001CF
        protected virtual System.Void LoginCustom(System.String customData) { }
        // RVA: 0x03AF9950  token: 0x60001D0
        public virtual System.Void SubmitGameData(U8.SDK.U8ExtraGameData data) { }
        // RVA: 0x03A869B0  token: 0x60001D1
        public virtual System.Void SetData(System.Int32 type, System.String paramJson) { }
        // RVA: 0x03AFA7A0  token: 0x60001D2
        public virtual System.String GetData(System.Int32 type, System.String paramJson) { }
        // RVA: 0x09DED9F4  token: 0x60001D3
        public virtual System.Boolean ShowAccountCenter() { }
        // RVA: 0x09DED994  token: 0x60001D4
        protected virtual System.Void Pay(U8.SDK.U8PayParams data) { }
        // RVA: 0x09DED790  token: 0x60001D5
        protected virtual U8.SDK.SDKMeta LoadSDKMeta() { }
        // RVA: 0x04D59840  token: 0x60001D6
        protected virtual System.Boolean IsNativePlugin() { }
        // RVA: 0x04CC8850  token: 0x60001D7
        public virtual System.Void SetGameVersion(System.String version) { }
        // RVA: 0x04D16E60  token: 0x60001D8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000069  // size: 0x40
    public class SDKMeta
    {
        // Fields
        public System.String appID;  // 0x10
        public System.String appKey;  // 0x18
        public System.String channel;  // 0x20
        public System.String worldId;  // 0x28
        public System.String extension;  // 0x30
        public System.String appCode;  // 0x38

        // Methods
        // RVA: 0x041E1670  token: 0x60001E6
        public System.Void .ctor() { }
        // RVA: 0x09DEDBA0  token: 0x60001E7
        public System.Void .ctor(System.String jsonData) { }
        // RVA: 0x09DEDA3C  token: 0x60001E8
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200006A
    public interface ISDKPromise
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001E9
        public virtual System.Void Fulfill(System.Object param) { }
        // RVA: -1  // abstract  token: 0x60001EA
        public virtual System.Void Reject(System.Object reason) { }

    }

    // TypeToken: 0x200006B
    public class SDKPromise`1 : U8.SDK.ISDKPromise
    {
        // Fields
        public System.Action<Param> onFulfilled;  // 0x0
        public System.Action<System.Object> onRejected;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001EB
        public virtual System.Void Fulfill(System.Object param) { }
        // RVA: -1  // not resolved  token: 0x60001EC
        public virtual System.Void Reject(System.Object reason) { }
        // RVA: -1  // not resolved  token: 0x60001ED
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006C
    public class SDKPromiseWithResult`1 : U8.SDK.SDKPromise`1
    {
        // Fields
        private Param <result>k__BackingField;  // 0x0
        private System.Object <rejectInfo>k__BackingField;  // 0x0

        // Properties
        Param result { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Object rejectInfo { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60001F2
        public virtual System.Void Fulfill(System.Object param) { }
        // RVA: -1  // not resolved  token: 0x60001F3
        public virtual System.Void Reject(System.Object reason) { }
        // RVA: -1  // not resolved  token: 0x60001F4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006D
    public class SDKPromiseEnumerator`1
    {
        // Fields
        private U8.SDK.SDKPromiseEnumerator.State<T> m_state;  // 0x0
        private T <result>k__BackingField;  // 0x0
        private System.Object <reject>k__BackingField;  // 0x0

        // Properties
        System.Boolean isFulfilled { get; /* RVA: -1  // not resolved */ }
        T result { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Object reject { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60001FA
        public System.Collections.IEnumerator Yield() { }
        // RVA: -1  // not resolved  token: 0x60001FB
        public System.Void .ctor(U8.SDK.SDKPromise<T> promise) { }
        // RVA: -1  // not resolved  token: 0x60001FC
        private System.Void _OnFulfilled(T result) { }
        // RVA: -1  // not resolved  token: 0x60001FD
        private System.Void _OnRejected(System.Object rejectInfo) { }

    }

    // TypeToken: 0x2000070  // size: 0x18
    public class SDKPromiseWrapper
    {
        // Fields
        private U8.SDK.ISDKPromise m_promise;  // 0x10

        // Methods
        // RVA: -1  // generic def  token: 0x6000204
        public T EnsurePromise() { }
        // RVA: 0x049DAAB0  token: 0x6000205
        public System.Void Fulfill(System.Object param) { }
        // RVA: 0x09DEDEE4  token: 0x6000206
        public System.Void Reject(System.Object reason) { }
        // RVA: 0x040E20A0  token: 0x6000207
        private System.Void _Clear() { }
        // RVA: 0x041E1670  token: 0x6000208
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000071  // size: 0x10
    public static class SDKPromiseExtensions
    {
        // Methods
        // RVA: 0x09DEDECC  token: 0x6000209
        public static System.Void RejectWithString(U8.SDK.ISDKPromise promise, System.String errorInfo) { }
        // RVA: -1  // generic def  token: 0x600020A
        public static System.Void FulfillWithType(U8.SDK.SDKPromise<T> promise, T param) { }

    }

    // TypeToken: 0x2000072  // size: 0x18
    public class U8SDKTickEvent : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x03F31120  token: 0x600020B
        private System.Void Update() { }
        // RVA: 0x03F31120  token: 0x600020C
        public static System.Void HGU8SDKTickEvent() { }
        // RVA: 0x05393520  token: 0x600020D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000073  // size: 0x20
    public class U8TickEventManager
    {
        // Fields
        private UnityEngine.GameObject tickGameObject;  // 0x10
        private U8.SDK.U8SDKTickEvent tickEvent;  // 0x18
        private static U8.SDK.U8TickEventManager instanceManager;  // static @ 0x0
        private static readonly System.Object lockObject;  // static @ 0x8

        // Properties
        U8.SDK.U8TickEventManager InstanceManager { get; /* RVA: 0x0488EB90 */ }

        // Methods
        // RVA: 0x0488ECC0  token: 0x600020F
        public System.Void InitTickEvent() { }
        // RVA: 0x09DF6094  token: 0x6000210
        public System.Void UnInitTickEvent() { }
        // RVA: 0x041E1670  token: 0x6000211
        public System.Void .ctor() { }
        // RVA: 0x04D30310  token: 0x6000212
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000074  // size: 0x28
    public class U8LoginV2Result
    {
        // Fields
        public System.String token;  // 0x10
        public System.String uid;  // 0x18
        public System.Boolean isNew;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000213
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000075  // size: 0x28
    public class U8AuthV2Result
    {
        // Fields
        public System.String uid;  // 0x10
        public System.String oauth2Code;  // 0x18
        public System.String oauth2Token;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000214
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000076  // size: 0x28
    public class U8GrantResult
    {
        // Fields
        public System.String uid;  // 0x10
        public System.String code;  // 0x18
        public System.String token;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000215
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000077  // size: 0x18
    public class U8SDKCallback : UnityEngine.MonoBehaviour
    {
        // Fields
        private static U8.SDK.U8SDKCallback m_instance;  // static @ 0x0
        private static System.Object m_lock;  // static @ 0x8

        // Methods
        // RVA: 0x03A84E20  token: 0x6000216
        public static U8.SDK.U8SDKCallback InitCallback() { }
        // RVA: 0x049DA9B0  token: 0x6000217
        public System.Void OnInitSuc(System.String extConfigs) { }
        // RVA: 0x09DF4908  token: 0x6000218
        public System.Void OnInitFail(System.String info) { }
        // RVA: 0x049DAA30  token: 0x6000219
        public System.Void OnLoginSuc(System.String extension) { }
        // RVA: 0x09DF4960  token: 0x600021A
        public System.Void OnLoginFail(System.String info) { }
        // RVA: 0x09DF49B8  token: 0x600021B
        public System.Void OnLogout(System.String info) { }
        // RVA: 0x09DF4B24  token: 0x600021C
        public System.Void OnSwitchAccount() { }
        // RVA: 0x09DF4A5C  token: 0x600021D
        public System.Void OnPaySuc(System.String jsonData) { }
        // RVA: 0x09DF4A04  token: 0x600021E
        public System.Void OnPayFail(System.String failMsg) { }
        // RVA: 0x09DF4AB4  token: 0x600021F
        public System.Void OnSDKError(System.String jsonData) { }
        // RVA: 0x035A5E80  token: 0x6000220
        public System.Void OnExtraInfo(System.String jsonData) { }
        // RVA: 0x09DF4B70  token: 0x6000221
        private System.Collections.IEnumerator _onLogoutNextFrameCoroutine() { }
        // RVA: -1  // generic def  token: 0x6000222
        protected static T GetValueSafe(System.Collections.Generic.Dictionary<System.String,System.Object> dict, System.String key, T defVal) { }
        // RVA: 0x05393520  token: 0x6000223
        public System.Void .ctor() { }
        // RVA: 0x04D30380  token: 0x6000224
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000079  // size: 0x10
    public static class OneChannelSDKVersion
    {
        // Fields
        public static readonly System.String VERSION;  // static @ 0x0

        // Methods
        // RVA: 0x04D4FF60  token: 0x600022B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200007A  // size: 0x14
    public sealed struct GameType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static U8.SDK.GameType Local;  // const
        public static U8.SDK.GameType Cloud;  // const

    }

    // TypeToken: 0x200007B  // size: 0x10
    public class CloudGameUtil
    {
        // Fields
        private static System.Int32 m_thinClientPlatform;  // static @ 0x0
        private static System.String m_thinClientDeviceProperties;  // static @ 0x8

        // Properties
        System.Int32 ThinClientPlatform { get; /* RVA: 0x09DE4854 */ set; /* RVA: 0x09DE4904 */ }
        System.String ThinClientDeviceProperties { get; /* RVA: 0x09DE4804 */ set; /* RVA: 0x09DE48A4 */ }

        // Methods
        // RVA: 0x040BC9E0  token: 0x6000230
        public static System.Boolean IsCloudGame() { }
        // RVA: 0x04D4FB30  token: 0x6000231
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200007C  // size: 0x10
    public class ImageSaver
    {
        // Methods
        // RVA: 0x09DE6094  token: 0x6000232
        public System.Threading.Tasks.Task SaveImage(System.String paramJson) { }
        // RVA: 0x09DE5E88  token: 0x6000233
        public System.Threading.Tasks.Task<System.Boolean> SaveCloudGameImage(System.String paramJson) { }
        // RVA: 0x09DE687C  token: 0x6000234
        private System.Boolean ValidateInput(System.String paramJson, U8.SDK.ImageSaver.ImageShareInfo& shareInfo) { }
        // RVA: 0x09DE5C58  token: 0x6000235
        private System.String NormalizeSourcePath(System.String imgPath) { }
        // RVA: 0x09DE5DCC  token: 0x6000236
        private U8.SDK.ImageSaver.FolderType ParseFolderType(System.Int32 folderType) { }
        // RVA: 0x09DE5324  token: 0x6000237
        private System.String BuildTargetPath(System.String sourcePath, System.String relativePath, System.String baseFolder) { }
        // RVA: 0x09DE5878  token: 0x6000238
        private System.Boolean IsDirectoryPath(System.String path) { }
        // RVA: 0x09DE51F8  token: 0x6000239
        private System.String BuildPathForDirectory(System.String sourcePath, System.String relativeDirPath, System.String baseFolder) { }
        // RVA: 0x09DE5F74  token: 0x600023A
        private System.Threading.Tasks.Task<System.Boolean> SaveImageToPathAsync(System.String sourcePath, System.String targetPath, System.String imgFolder) { }
        // RVA: 0x09DE592C  token: 0x600023B
        private System.Boolean IsSamePath(System.String sourcePath, System.String targetPath) { }
        // RVA: 0x09DE6980  token: 0x600023C
        private System.Boolean ValidateMove(System.String sourcePath, System.String targetPath, System.String rootDir, System.String& error) { }
        // RVA: 0x09DE5450  token: 0x600023D
        private System.Void EnsureDirectoryExists(System.String filePath) { }
        // RVA: 0x09DE5A00  token: 0x600023E
        private System.Boolean MoveFile(System.String sourcePath, System.String targetPath) { }
        // RVA: 0x09DE5664  token: 0x600023F
        private System.String GetImageSaveFolder(U8.SDK.ImageSaver.FolderType folderType) { }
        // RVA: 0x09DE5718  token: 0x6000240
        private System.String GetWindowsPicturePath() { }
        // RVA: 0x09DE54B8  token: 0x6000241
        private System.String GetGameInstalledFolder() { }
        // RVA: 0x09DE6788  token: 0x6000242
        private System.Void SendSuccessResponse(System.String targetPath) { }
        // RVA: 0x09DE6164  token: 0x6000243
        private System.Void SendErrorResponse(U8.SDK.ImageSaver.ErrorCode errorCode) { }
        // RVA: 0x09DE6314  token: 0x6000244
        private System.Void SendResponse(U8.SDK.ImageSaver.CallbackResult code, System.Collections.Generic.Dictionary<System.String,System.Object> extraMsg) { }
        // RVA: 0x09DE623C  token: 0x6000245
        private System.Void SendMessageInMain(System.String methodName, System.String paramValue) { }
        // RVA: 0x041E1670  token: 0x6000246
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000086  // size: 0x18
    public class U8MainThreadDispatcher : UnityEngine.MonoBehaviour
    {
        // Fields
        private static readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _queue;  // static @ 0x0
        private static U8.SDK.U8MainThreadDispatcher _instance;  // static @ 0x8
        private static System.Int32 _initialized;  // static @ 0x10

        // Methods
        // RVA: 0x04B46C10  token: 0x6000252
        private static System.Void Initialize() { }
        // RVA: 0x09DF3AF0  token: 0x6000253
        public static System.Void RunOnMainThread(System.Action action) { }
        // RVA: 0x03B643C0  token: 0x6000254
        private System.Void Update() { }
        // RVA: 0x05393520  token: 0x6000255
        public System.Void .ctor() { }
        // RVA: 0x04A1D370  token: 0x6000256
        private static System.Void .cctor() { }

    }

}

namespace U8.SDK.MiniJSON
{

    // TypeToken: 0x2000087  // size: 0x10
    public static class Json
    {
        // Methods
        // RVA: 0x035A6450  token: 0x6000257
        public static System.Object Deserialize(System.String json) { }
        // RVA: 0x05395194  token: 0x6000258
        public static System.String Serialize(System.Object obj) { }

    }

}

