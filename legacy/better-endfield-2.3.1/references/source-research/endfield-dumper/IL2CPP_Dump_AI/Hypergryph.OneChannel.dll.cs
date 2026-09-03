// ========================================================
// Dumped by @desirepro
// Assembly: Hypergryph.OneChannel.dll
// Classes:  143
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

CLASS: Status
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.SDKCaptchaHandler.Status SUC  // const
  public    static  U8.SDK.SDKCaptchaHandler.Status SYS_BUSY  // const
  public    static  U8.SDK.SDKCaptchaHandler.Status USER_CANCEL  // const
  public    static  U8.SDK.SDKCaptchaHandler.Status NOT_SUPPORT  // const
  public    static  U8.SDK.SDKCaptchaHandler.Status INVALID_PARAM  // const
  public    static  U8.SDK.SDKCaptchaHandler.Status UNKNOWN  // const
METHODS:
END_CLASS

CLASS: Result
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x20
FIELDS:
  private           U8.SDK.SDKCaptchaHandler.Status <status>k__BackingField  // 0x10
  private           System.Int32                    <errorCode>k__BackingField  // 0x14
  private           System.String                   <captcha>k__BackingField  // 0x18
PROPERTIES:
  status  get=0x02B2E2D0  set=0x014F51F0
  errorCode  set=0x04D8F540
  captcha  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x041E1670  token=0x6000010  System.Void .ctor()
  RVA=0x09DE6E20  token=0x6000011  U8.SDK.SDKCaptchaHandler.Result CreateForOutput()
  RVA=0x09DE6EB8  token=0x6000012  System.Void MarkSucceed(System.String captcha)
  RVA=0x09DE6E8C  token=0x6000013  System.Void MarkFailed(U8.SDK.SDKCaptchaHandler.Status status, System.Int32 errorCode)
  RVA=0x09DE6E64  token=0x6000014  System.Void MarkFailed(U8.SDK.SDKCaptchaHandler.Status status)
END_CLASS

CLASS: IFromJSON
TYPE:  interface
TOKEN: 0x200000A
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600005A  System.Boolean LoadFromJSON(System.String json)
END_CLASS

CLASS: POSTStatus
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.SDKExternalTools.POSTStatusNONE  // const
  public    static  U8.SDK.SDKExternalTools.POSTStatusSUC  // const
  public    static  U8.SDK.SDKExternalTools.POSTStatusTIMEOUT  // const
  public    static  U8.SDK.SDKExternalTools.POSTStatusERROR  // const
METHODS:
END_CLASS

CLASS: BusType
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.SDKExternalTools.BusType NONE  // const
  public    static  U8.SDK.SDKExternalTools.BusType GET_TOKEN  // const
  public    static  U8.SDK.SDKExternalTools.BusType GET_CAPTCHA  // const
  public    static  U8.SDK.SDKExternalTools.BusType CREATE_ORDER  // const
  public    static  U8.SDK.SDKExternalTools.BusType CONFIRM_ORDER  // const
  public    static  U8.SDK.SDKExternalTools.BusType VERIFY_ACCOUNT  // const
  public    static  U8.SDK.SDKExternalTools.BusType GET_PRODUCT_LIST  // const
  public    static  U8.SDK.SDKExternalTools.BusType AUTHV2_SESSION_TOKEN  // const
  public    static  U8.SDK.SDKExternalTools.BusType AUTHV2_OAUTH2CODE  // const
  public    static  U8.SDK.SDKExternalTools.BusType AUTHV2_OAUTH2TOKEN  // const
  public    static  U8.SDK.SDKExternalTools.BusType SERVER_LIST  // const
  public    static  U8.SDK.SDKExternalTools.BusType CONFIRM_SERVER  // const
METHODS:
END_CLASS

CLASS: ErrorType
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.SDKExternalTools.ErrorTypeNONE  // const
  public    static  U8.SDK.SDKExternalTools.ErrorTypeTIMEOUT  // const
  public    static  U8.SDK.SDKExternalTools.ErrorTypeNETWORK_ERROR  // const
  public    static  U8.SDK.SDKExternalTools.ErrorTypeDESERIALIZE_FAILED  // const
  public    static  U8.SDK.SDKExternalTools.ErrorTypeBUSINESS_ERROR  // const
METHODS:
END_CLASS

CLASS: POSTRequest
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x30
FIELDS:
  public            System.String                   url  // 0x10
  public            System.String                   body  // 0x18
  public            System.String                   contentType  // 0x20
  public            System.Collections.Generic.Dictionary<System.String,System.String>nullableHeaders  // 0x28
METHODS:
END_CLASS

CLASS: POSTResult
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x28
FIELDS:
  public            U8.SDK.SDKExternalTools.POSTStatusstatus  // 0x10
  public            System.Int32                    httpCode  // 0x14
  public            System.String                   response  // 0x18
  public            System.String                   error  // 0x20
METHODS:
  RVA=0x09DE6CF4  token=0x600005B  System.Boolean ServiceFailed()
END_CLASS

CLASS: ErrMsgMeta
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x30
FIELDS:
  public    static readonly U8.SDK.SDKExternalTools.ErrMsgMetaEMPTY  // static @ 0x0
  public            U8.SDK.SDKExternalTools.BusType busType  // 0x10
  public            U8.SDK.SDKExternalTools.ErrorTypeerrorType  // 0x14
  public            System.Int32                    errorCode  // 0x18
  public            System.String                   alertFromServer  // 0x20
  public            System.Object                   meta  // 0x28
METHODS:
  RVA=0x0426F6A0  token=0x600005C  System.Boolean IsEmpty()
  RVA=0x09DE4E74  token=0x600005D  U8.SDK.SDKExternalTools.ErrMsgMeta FromPOSTResult(U8.SDK.SDKExternalTools.BusType busType, U8.SDK.SDKExternalTools.POSTResult postRet)
END_CLASS

CLASS: <FetchCaptchaCoroutine>d__6
TYPE:  sealed class
TOKEN: 0x2000012
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.SDKExternalTools.CaptchaMgr<>4__this  // 0x20
  public            U8.SDK.SDKCaptchaHandler.Result outResult  // 0x28
  public            System.Collections.Generic.Dictionary<System.String,System.Object>captchaParams  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000062  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x09DEEEE4  token=0x6000063  System.Void System.IDisposable.Dispose()
  RVA=0x09DEEAFC  token=0x6000064  System.Boolean MoveNext()
  RVA=0x09DEEF5C  token=0x6000065  System.Void <>m__Finally1()
  RVA=0x09DEEE98  token=0x6000067  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: CaptchaMgr
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x20
FIELDS:
  private           U8.SDK.SDKExternalTools         m_host  // 0x10
  private           U8.SDK.SDKCaptchaHandler        <activeHandler>k__BackingField  // 0x18
PROPERTIES:
  activeHandler  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x053908C0  token=0x6000060  System.Void .ctor(U8.SDK.SDKExternalTools host)
  RVA=0x09DE4778  token=0x6000061  System.Collections.IEnumerator FetchCaptchaCoroutine(System.Collections.Generic.Dictionary<System.String,System.Object> captchaParams, U8.SDK.SDKCaptchaHandler.Result outResult)
END_CLASS

CLASS: Builder
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x38
FIELDS:
  public            U8.SDK.SDKExternalTools.POSTRequestrequest  // 0x10
  public            U8.SDK.SDKExternalTools.BusType busType  // 0x30
METHODS:
  RVA=0x09DE4704  token=0x6000075  U8.SDK.SDKExternalTools.POSTProcedure Build()
  RVA=-1  // generic def  token=0x6000076  U8.SDK.SDKExternalTools.POSTProcedure<TResp> TypedBuild()
END_CLASS

CLASS: <POST>d__18
TYPE:  sealed class
TOKEN: 0x2000015
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.SDKExternalTools         host  // 0x20
  public            U8.SDK.SDKExternalTools.POSTProcedure<>4__this  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000077  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000078  System.Void System.IDisposable.Dispose()
  RVA=0x09DEEF8C  token=0x6000079  System.Boolean MoveNext()
  RVA=0x09DEF0B0  token=0x600007B  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: POSTProcedure
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x68
FIELDS:
  private           U8.SDK.SDKExternalTools.POSTProcedure.Builder<builder>k__BackingField  // 0x10
  private           System.Boolean                  <isServiceFinished>k__BackingField  // 0x38
  private           U8.SDK.SDKExternalTools.ErrMsgMeta<serviceError>k__BackingField  // 0x40
  private           System.String                   <rawResponse>k__BackingField  // 0x60
PROPERTIES:
  builder  get=0x02B2D2D0  set=0x09DC5750
  isServiceFinished  get=0x011F2A70  set=0x011F2A80
  serviceError  get=0x04DA2E80  set=0x09DE6CDC
  rawResponse  set=0x063CC164
METHODS:
  RVA=0x04DA2E40  token=0x6000070  U8.SDK.SDKExternalTools.ErrMsgMeta MakeBusinessError(System.Int32 errorCode)
  RVA=0x09DE6B58  token=0x6000071  System.Collections.IEnumerator POST(U8.SDK.SDKExternalTools host)
  RVA=0x09DE6BCC  token=0x6000072  System.Void _OnServiceFinished(U8.SDK.SDKExternalTools.POSTResult postRet)
  RVA=0x041E1670  token=0x6000073  System.Void HandleServiceResponse(U8.SDK.SDKExternalTools.POSTResult postRet)
  RVA=0x041E1670  token=0x6000074  System.Void .ctor()
END_CLASS

CLASS: POSTProcedure`1
TYPE:  class
TOKEN: 0x2000016
EXTENDS: POSTProcedure
FIELDS:
  private           TResp                           <response>k__BackingField  // 0x0
PROPERTIES:
  response  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600007F  System.Void HandleServiceResponse(U8.SDK.SDKExternalTools.POSTResult postRet)
  RVA=-1  // not resolved  token=0x6000080  System.Void .ctor()
END_CLASS

CLASS: AuthV2SessionTokenResp
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x40
IMPLEMENTS: IFromJSON
FIELDS:
  public            System.Int32                    status  // 0x10
  public            System.String                   msg  // 0x18
  public            System.String                   type  // 0x20
  public            System.String                   token  // 0x28
  public            System.String                   uid  // 0x30
  public            System.Boolean                  isNew  // 0x38
METHODS:
  RVA=0x09DE4510  token=0x6000081  System.Boolean LoadFromJSON(System.String json)
  RVA=0x041E1670  token=0x6000082  System.Void .ctor()
END_CLASS

CLASS: GetProductLsitResp
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x20
IMPLEMENTS: IFromJSON
FIELDS:
  public            System.Int32                    flag  // 0x10
  public            System.Collections.Generic.Dictionary<System.String,System.Object>desc  // 0x18
METHODS:
  RVA=0x09DE4EB4  token=0x6000083  System.Boolean LoadFromDictionary(System.Collections.Generic.Dictionary<System.String,System.Object> dict)
  RVA=0x09DE5118  token=0x6000084  System.Boolean LoadFromJSON(System.String json)
  RVA=0x041E1670  token=0x6000085  System.Void .ctor()
END_CLASS

CLASS: AuthV2GrantResp
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x68
IMPLEMENTS: IFromJSON
FIELDS:
  public            System.Int32                    status  // 0x10
  public            System.String                   msg  // 0x18
  public            System.String                   type  // 0x20
  public            System.String                   uid  // 0x28
  public            System.String                   code  // 0x30
  public            System.String                   token  // 0x38
  public            System.Int64                    banStartTs  // 0x40
  public            System.Int64                    banEndTs  // 0x48
  public            System.Int64                    maintainStartTs  // 0x50
  public            System.Int64                    maintainEndTs  // 0x58
  public            System.Collections.Generic.Dictionary<System.String,System.Object>captcha  // 0x60
METHODS:
  RVA=0x09DE4244  token=0x6000086  System.Boolean LoadFromJSON(System.String json)
  RVA=0x041E1670  token=0x6000087  System.Void .ctor()
END_CLASS

CLASS: ServerData
TYPE:  sealed struct
TOKEN: 0x200001B
SIZE:  0x50
FIELDS:
  public            System.String                   serverId  // 0x10
  public            System.String                   serverName  // 0x18
  public            System.String                   serverDomain  // 0x20
  public            System.Boolean                  defaultChoose  // 0x28
  public            System.String                   roleId  // 0x30
  public            System.String                   nickName  // 0x38
  public            System.Int64                    level  // 0x40
  public            System.String                   extension  // 0x48
METHODS:
END_CLASS

CLASS: ServerListResp
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x28
IMPLEMENTS: IFromJSON
FIELDS:
  public            System.Int32                    status  // 0x10
  public            System.String                   msg  // 0x18
  public            System.Collections.Generic.List<U8.SDK.SDKExternalTools.ServerListResp.ServerData>data  // 0x20
METHODS:
  RVA=0x09DEE38C  token=0x6000088  System.Boolean LoadFromJSON(System.String json)
  RVA=0x09DEE884  token=0x6000089  System.Collections.Generic.List<U8.SDK.U8ServerInfo> ToServerInfo()
  RVA=0x041E1670  token=0x600008A  System.Void .ctor()
END_CLASS

CLASS: ConfirmServerResp
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x20
IMPLEMENTS: IFromJSON
FIELDS:
  public            System.Int32                    status  // 0x10
  public            System.String                   msg  // 0x18
METHODS:
  RVA=0x09DE49F4  token=0x600008B  System.Boolean LoadFromJSON(System.String json)
  RVA=0x041E1670  token=0x600008C  System.Void .ctor()
END_CLASS

CLASS: ConfirmOrderU1Resp
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x18
IMPLEMENTS: IFromJSON
FIELDS:
  public            System.Int32                    status  // 0x10
METHODS:
  RVA=0x09DE4958  token=0x600008D  System.Boolean LoadFromJSON(System.String json)
  RVA=0x041E1670  token=0x600008E  System.Void .ctor()
END_CLASS

CLASS: Data
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x40
FIELDS:
  public            System.String                   orderId  // 0x10
  public            System.String                   channelProductCode  // 0x18
  public            System.String                   productName  // 0x20
  public            System.String                   productDesc  // 0x28
  public            System.Int64                    amount  // 0x30
  public            System.Collections.Generic.Dictionary<System.String,System.Object>extension  // 0x38
METHODS:
END_CLASS

CLASS: CreateOrderU1Resp
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x58
IMPLEMENTS: IFromJSON
FIELDS:
  public            System.Int32                    status  // 0x10
  public            System.String                   msg  // 0x18
  public            System.String                   type  // 0x20
  public            U8.SDK.SDKExternalTools.CreateOrderU1Resp.Datadata  // 0x28
METHODS:
  RVA=0x09DE4AD0  token=0x600008F  System.Boolean LoadFromJSON(System.String json)
  RVA=0x09DE4D5C  token=0x6000090  U8.SDK.U8OrderInfo ToOrderInfo()
  RVA=0x041E1670  token=0x6000091  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000020
SIZE:  0x10
FIELDS:
  public    static readonly U8.SDK.SDKExternalTools.<>c     <>9  // static @ 0x0
  public    static  System.Action<System.String>    <>9__43_0  // static @ 0x8
  public    static  System.Action<System.String>    <>9__43_1  // static @ 0x10
  public    static  System.Action                   <>9__44_0  // static @ 0x18
  public    static  System.Action                   <>9__44_1  // static @ 0x20
METHODS:
  RVA=0x04D40390  token=0x6000092  System.Void .cctor()
  RVA=0x041E1670  token=0x6000093  System.Void .ctor()
  RVA=0x04D26E00  token=0x6000094  System.Void <LoginWithPlugin>b__43_0(System.String extension)
  RVA=0x09DF02D4  token=0x6000095  System.Void <LoginWithPlugin>b__43_1(System.String message)
  RVA=0x09DF0340  token=0x6000096  System.Void <LogoutWithPlugin>b__44_0()
  RVA=0x09DF03C0  token=0x6000097  System.Void <LogoutWithPlugin>b__44_1()
END_CLASS

CLASS: <_NextFrameCoroutine>d__61
TYPE:  sealed class
TOKEN: 0x2000021
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Action                   action  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000098  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000099  System.Void System.IDisposable.Dispose()
  RVA=0x09DF2EAC  token=0x600009A  System.Boolean MoveNext()
  RVA=0x09DF2EFC  token=0x600009C  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <SDKInterfaceAuthV2SessionToken>d__114
TYPE:  sealed class
TOKEN: 0x2000022
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.SDKPromiseWithResult<U8.SDK.U8LoginV2Result>promise  // 0x20
  public            U8.SDK.SDKMeta                  meta  // 0x28
  public            System.String                   channelToken  // 0x30
  public            U8.SDK.SDKExternalTools         <>4__this  // 0x38
  private           U8.SDK.SDKExternalTools.POSTProcedure<U8.SDK.SDKExternalTools.AuthV2SessionTokenResp><postImpl>5__2  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600009E  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600009F  System.Void System.IDisposable.Dispose()
  RVA=0x09DEF1AC  token=0x60000A0  System.Boolean MoveNext()
  RVA=0x09DEF650  token=0x60000A2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <SDKInterfaceAuthV2OAuth2Code>d__115
TYPE:  sealed class
TOKEN: 0x2000023
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.SDKExternalTools         <>4__this  // 0x20
  public            System.String                   sessionToken  // 0x28
  public            U8.SDK.SDKPromiseWithResult<U8.SDK.U8GrantResult>promise  // 0x30
  public            System.Action                   onSessionInvalid  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000A4  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000A5  System.Void System.IDisposable.Dispose()
  RVA=0x09DEF0FC  token=0x60000A6  System.Boolean MoveNext()
  RVA=0x09DEF160  token=0x60000A8  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_AuthV2GrantImpl>d__121
TYPE:  sealed class
TOKEN: 0x2000024
SIZE:  0x60
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.SDKPromiseWithResult<U8.SDK.U8GrantResult>promise  // 0x20
  public            U8.SDK.SDKExternalTools         <>4__this  // 0x28
  public            System.String                   sessionToken  // 0x30
  public            System.Action                   onSessionInvalid  // 0x38
  private           System.String                   <captcha>5__2  // 0x40
  private           System.Boolean                  <needCaptcha>5__3  // 0x48
  private           U8.SDK.SDKExternalTools.POSTProcedure<U8.SDK.SDKExternalTools.AuthV2GrantResp><postImpl>5__4  // 0x50
  private           U8.SDK.SDKCaptchaHandler.Result <captchaRet>5__5  // 0x58
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000AA  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000AB  System.Void System.IDisposable.Dispose()
  RVA=0x09DF1D28  token=0x60000AC  System.Boolean MoveNext()
  RVA=0x09DF2484  token=0x60000AE  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_ConfirmOrderU1Coroutine>d__124
TYPE:  sealed class
TOKEN: 0x2000025
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.SDKExternalTools         <>4__this  // 0x20
  public            System.String                   orderId  // 0x28
  public            System.String                   extension  // 0x30
  public            U8.SDK.SDKPromise<U8.SDK.U8ConfirmOrderResult>promise  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000B0  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000B1  System.Void System.IDisposable.Dispose()
  RVA=0x09DF24D0  token=0x60000B2  System.Boolean MoveNext()
  RVA=0x09DF26D4  token=0x60000B4  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_PollOrderStatusCoroutine>d__125
TYPE:  sealed class
TOKEN: 0x2000026
SIZE:  0x70
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.SDKExternalTools         <>4__this  // 0x20
  public            System.String                   orderId  // 0x28
  public            U8.SDK.SDKPromise<U8.SDK.U8ConfirmOrderResult>promise  // 0x30
  private           System.Int32                    <tryCnt>5__2  // 0x38
  private           System.Int32                    <maxTryCnt>5__3  // 0x3c
  private           U8.SDK.SDKExternalTools.POSTProcedure.Builder<postBuilder>5__4  // 0x40
  private           U8.SDK.SDKExternalTools.POSTProcedure<U8.SDK.SDKExternalTools.ConfirmOrderU1Resp><postImpl>5__5  // 0x68
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000B6  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000B7  System.Void System.IDisposable.Dispose()
  RVA=0x09DF2F48  token=0x60000B8  System.Boolean MoveNext()
  RVA=0x09DF340C  token=0x60000BA  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_GetServerListCoroutine>d__126
TYPE:  sealed class
TOKEN: 0x2000027
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.SDKExternalTools         <>4__this  // 0x20
  public            System.String                   paramStr  // 0x28
  public            U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ServerInfo>>promise  // 0x30
  private           U8.SDK.SDKExternalTools.POSTProcedure<U8.SDK.SDKExternalTools.ServerListResp><postImpl>5__2  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000BC  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000BD  System.Void System.IDisposable.Dispose()
  RVA=0x09DF2B20  token=0x60000BE  System.Boolean MoveNext()
  RVA=0x09DF2DC8  token=0x60000C0  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_ConfirmServerListCoroutine>d__128
TYPE:  sealed class
TOKEN: 0x2000028
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.SDKExternalTools         <>4__this  // 0x20
  public            System.String                   paramStr  // 0x28
  public            U8.SDK.SDKPromise<U8.SDK.U8ConfirmServerResult>promise  // 0x30
  public            System.String                   serverId  // 0x38
  private           U8.SDK.SDKExternalTools.POSTProcedure<U8.SDK.SDKExternalTools.ConfirmServerResp><postImpl>5__2  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000C2  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000C3  System.Void System.IDisposable.Dispose()
  RVA=0x09DF2720  token=0x60000C4  System.Boolean MoveNext()
  RVA=0x09DF2AD4  token=0x60000C6  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_SendCreateOrderU1Coroutine>d__129
TYPE:  sealed class
TOKEN: 0x2000029
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.SDKExternalTools         <>4__this  // 0x20
  public            System.String                   paramStr  // 0x28
  public            U8.SDK.SDKPromise<U8.SDK.U8OrderInfo>promise  // 0x30
  private           U8.SDK.SDKExternalTools.POSTProcedure<U8.SDK.SDKExternalTools.CreateOrderU1Resp><postImpl>5__2  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000C8  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000C9  System.Void System.IDisposable.Dispose()
  RVA=0x09DF3514  token=0x60000CA  System.Boolean MoveNext()
  RVA=0x09DF37AC  token=0x60000CC  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_SendCreateOrderFailCoroutine>d__130
TYPE:  sealed class
TOKEN: 0x200002A
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.SDKPromise<U8.SDK.U8OrderInfo>promise  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000CE  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000CF  System.Void System.IDisposable.Dispose()
  RVA=0x09DF3458  token=0x60000D0  System.Boolean MoveNext()
  RVA=0x09DF34C8  token=0x60000D2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: U8ProductListDataV2
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x18
IMPLEMENTS: IFromJSON
FIELDS:
  public            System.Collections.Generic.List<U8.SDK.U8ProductInfo>productList  // 0x10
METHODS:
  RVA=0x09DF4498  token=0x60000DC  System.Boolean LoadFromJSON(System.String json)
  RVA=0x09DF4740  token=0x60000DD  System.Boolean _BuildProductInfo(U8.SDK.U8ProductInfo outInfo, System.Collections.Generic.Dictionary<System.String,System.Object> dict)
END_CLASS

CLASS: RequestConnPolicy
TYPE:  sealed struct
TOKEN: 0x200002E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.SDKExternalToolsShortcut.RequestConnPolicyDEFAULT  // const
  public    static  U8.SDK.SDKExternalToolsShortcut.RequestConnPolicyCLOSE_EACH_CON  // const
METHODS:
END_CLASS

CLASS: RequestHeaderInjectOptions
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x14
FIELDS:
  public            U8.SDK.SDKExternalToolsShortcut.RequestConnPolicypolicy  // 0x10
METHODS:
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed class
TOKEN: 0x2000030
SIZE:  0x48
FIELDS:
  public            U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ProductInfo>>promise  // 0x10
  public            U8.SDK.SDKExternalToolsShortcut <>4__this  // 0x18
  public            U8.SDK.SDKExternalTools.POSTRequestrequest  // 0x20
  public            System.Action<U8.SDK.SDKExternalTools.POSTResult>callback  // 0x40
METHODS:
  RVA=0x041E1670  token=0x60000DE  System.Void .ctor()
  RVA=0x09DF0518  token=0x60000DF  System.Void <SendGetProductListRequestV2>b__0(U8.SDK.SDKExternalTools.POSTResult postRet)
  RVA=0x09DF065C  token=0x60000E0  System.Void <SendGetProductListRequestV2>b__1()
END_CLASS

CLASS: AuthStatus
TYPE:  sealed struct
TOKEN: 0x2000032
SIZE:  0x30
FIELDS:
  public    static readonly U8.SDK.U8SDKInterface.AuthStatusEMPTY  // static @ 0x0
  public            System.String                   uid  // 0x10
  public            System.String                   oauth2code  // 0x18
  public            System.String                   oauth2token  // 0x20
  public            System.String                   sessionToken  // 0x28
METHODS:
END_CLASS

CLASS: V2LoginStatus
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x18
FIELDS:
  public            System.String                   sessionToken  // 0x10
METHODS:
END_CLASS

CLASS: ReceiverProductListResult
TYPE:  sealed class
TOKEN: 0x2000034
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x068FE0AC  token=0x600012B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600012C  System.Void Invoke(System.Collections.Generic.Dictionary<System.String,System.Object> msg)
END_CLASS

CLASS: GameServerTimeCache
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x28
FIELDS:
  public            System.Int64                    cacheTime  // 0x10
  public            System.Int64                    maintainEndTs  // 0x18
  public            System.Int64                    maintainStartTs  // 0x20
METHODS:
END_CLASS

CLASS: <>c__DisplayClass51_0`1
TYPE:  sealed class
TOKEN: 0x2000036
FIELDS:
  public            U8.SDK.SDKPromise<System.String>delegatePromise  // 0x0
  public            U8.SDK.U8SDKInterface           <>4__this  // 0x0
  public            System.String                   env  // 0x0
  public            System.Action                   <>9__3  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600012D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600012E  System.Void <V2PublicInit>b__0(System.String obj)
  RVA=-1  // not resolved  token=0x600012F  System.Void <V2PublicInit>b__3()
  RVA=-1  // not resolved  token=0x6000130  System.Void <V2PublicInit>b__1(System.Object info)
  RVA=-1  // not resolved  token=0x6000131  System.Void <V2PublicInit>b__2()
END_CLASS

CLASS: <>c__DisplayClass55_0
TYPE:  sealed class
TOKEN: 0x2000037
SIZE:  0x28
FIELDS:
  public            U8.SDK.U8SDKInterface           <>4__this  // 0x10
  public            System.String                   uid  // 0x18
  public            System.Boolean                  isNew  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000132  System.Void .ctor()
  RVA=0x04CE3A40  token=0x6000133  System.Void <_onLoginSuc>b__0()
END_CLASS

CLASS: <>c__DisplayClass57_0
TYPE:  sealed class
TOKEN: 0x2000038
SIZE:  0x38
FIELDS:
  public            U8.SDK.SDKExternalTools         externalTools  // 0x10
  public            U8.SDK.U8SDKInterface           <>4__this  // 0x18
  public            U8.SDK.SDKPromise<U8.SDK.U8ProductsResult>delegatePromise  // 0x20
  public            System.Action<System.Collections.Generic.List<U8.SDK.U8ProductInfo>><>9__1  // 0x28
  public            System.Action<System.Object>    <>9__2  // 0x30
METHODS:
  RVA=0x041E1670  token=0x6000134  System.Void .ctor()
  RVA=0x09DF08C8  token=0x6000135  System.Void <PublicGetProductListV2>b__0()
  RVA=0x09DF09E8  token=0x6000136  System.Void <PublicGetProductListV2>b__1(System.Collections.Generic.List<U8.SDK.U8ProductInfo> productList)
  RVA=0x09DF0CBC  token=0x6000137  System.Void <PublicGetProductListV2>b__2(System.Object rejectObj)
END_CLASS

CLASS: <>c__DisplayClass57_1
TYPE:  sealed class
TOKEN: 0x2000039
SIZE:  0x28
FIELDS:
  public            System.Collections.Generic.List<U8.SDK.U8ProductInfo>productList  // 0x10
  public            U8.SDK.U8SDKInterface.ReceiverProductListResultreceiver  // 0x18
  public            U8.SDK.U8SDKInterface.<>c__DisplayClass57_0CS$<>8__locals1  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000138  System.Void .ctor()
  RVA=0x09DF0CE0  token=0x6000139  System.Void <PublicGetProductListV2>b__3(System.Collections.Generic.Dictionary<System.String,System.Object> msg)
END_CLASS

CLASS: <>c__DisplayClass69_0
TYPE:  sealed class
TOKEN: 0x200003A
SIZE:  0x40
FIELDS:
  public            U8.SDK.U8PayParams              payParams  // 0x10
  public            U8.SDK.U8SDKInterface           <>4__this  // 0x18
  public            U8.SDK.SDKPromise<U8.SDK.U8PayResult>wrappedPromise  // 0x20
  public            U8.SDK.U8ProductInfo            productInfo  // 0x28
  public            System.Action<U8.SDK.U8PayResult><>9__1  // 0x30
  public            System.Action<System.Object>    <>9__2  // 0x38
METHODS:
  RVA=0x041E1670  token=0x600013A  System.Void .ctor()
  RVA=0x09DF1620  token=0x600013B  System.Void <PublicPay>b__0()
  RVA=0x09DF17AC  token=0x600013C  System.Void <PublicPay>b__1(U8.SDK.U8PayResult payResult)
  RVA=0x09DF0CBC  token=0x600013D  System.Void <PublicPay>b__2(System.Object payFailMsg)
  RVA=0x09DF1850  token=0x600013E  System.Void <PublicPay>b__3(U8.SDK.U8PayResult payResult)
  RVA=0x09DF0CBC  token=0x600013F  System.Void <PublicPay>b__4(System.Object payFailMsg)
END_CLASS

CLASS: <>c__DisplayClass70_0
TYPE:  sealed class
TOKEN: 0x200003B
SIZE:  0x20
FIELDS:
  public            U8.SDK.U8SDKInterface           <>4__this  // 0x10
  public            System.Int64                    revenue  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000140  System.Void .ctor()
  RVA=0x09DF18F4  token=0x6000141  System.Void <_OnNativePayFulfilled>b__0()
END_CLASS

CLASS: <>c__DisplayClass78_0
TYPE:  sealed class
TOKEN: 0x200003C
SIZE:  0x20
FIELDS:
  public            U8.SDK.U8SDKInterface           <>4__this  // 0x10
  public            U8.SDK.U8PayParams              payParams  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000142  System.Void .ctor()
  RVA=0x09DF1964  token=0x6000143  System.Void <_NativePay>b__0()
END_CLASS

CLASS: <_NextFrameCoroutine>d__105
TYPE:  sealed class
TOKEN: 0x200003D
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Action                   call  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000144  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000145  System.Void System.IDisposable.Dispose()
  RVA=0x04991450  token=0x6000146  System.Boolean MoveNext()
  RVA=0x09DF2E60  token=0x6000148  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass131_0
TYPE:  sealed class
TOKEN: 0x200003E
SIZE:  0x20
FIELDS:
  public            U8.SDK.U8SDKInterface           <>4__this  // 0x10
  public            U8.SDK.SDKPromise<U8.SDK.U8AuthV2Result>promise  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600014A  System.Void .ctor()
  RVA=0x03F4B8E0  token=0x600014B  System.Void <_DoMockAuthV2>b__0()
END_CLASS

CLASS: <_LoginV2Coroutine>d__132
TYPE:  sealed class
TOKEN: 0x200003F
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.U8SDKInterface           <>4__this  // 0x20
  public            U8.SDK.SDKPromise<System.String>promise  // 0x28
  private           U8.SDK.SDKPromiseEnumerator<System.String><loginEnumerator>5__2  // 0x30
  private           U8.SDK.SDKPromiseWithResult<U8.SDK.U8LoginV2Result><sessionTokenPromise>5__3  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600014C  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600014D  System.Void System.IDisposable.Dispose()
  RVA=0x03AF94D0  token=0x600014E  System.Boolean MoveNext()
  RVA=0x09DF2E14  token=0x6000150  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_AuthV2Coroutine>d__134
TYPE:  sealed class
TOKEN: 0x2000040
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            U8.SDK.U8SDKInterface           <>4__this  // 0x20
  public            U8.SDK.SDKPromise<U8.SDK.U8AuthV2Result>promise  // 0x28
  private           U8.SDK.SDKPromiseWithResult<U8.SDK.U8GrantResult><oauth2codePromise>5__2  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000152  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000153  System.Void System.IDisposable.Dispose()
  RVA=0x09DF1A10  token=0x6000154  System.Boolean MoveNext()
  RVA=0x09DF1CDC  token=0x6000156  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: DataCallback
TYPE:  sealed class
TOKEN: 0x2000054
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x07D884AC  token=0x600019F  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x60001A0  System.Void Invoke(System.Int32 index, System.String paramValue)
END_CLASS

CLASS: U8SDKSendMessageCallback
TYPE:  sealed class
TOKEN: 0x2000055
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x08469FD0  token=0x60001A1  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x60001A2  System.Void Invoke(System.String methodName, System.String paramValue)
END_CLASS

CLASS: LoginResult
TYPE:  class
TOKEN: 0x2000056
SIZE:  0x20
FIELDS:
  public            U8.SDK.SDKInterfaceCloud.LoginResultCodecode  // 0x10
  public            Newtonsoft.Json.Linq.JToken     msg  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001A3  System.Void .ctor()
END_CLASS

CLASS: LoginResultCode
TYPE:  sealed struct
TOKEN: 0x2000057
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.SDKInterfaceCloud.LoginResultCodeSuccess  // const
  public    static  U8.SDK.SDKInterfaceCloud.LoginResultCodeCanceled  // const
  public    static  U8.SDK.SDKInterfaceCloud.LoginResultCodeFailed  // const
METHODS:
END_CLASS

CLASS: PaymentResult
TYPE:  class
TOKEN: 0x2000058
SIZE:  0x20
FIELDS:
  public            U8.SDK.SDKInterfaceCloud.PaymentResultCodecode  // 0x10
  public            Newtonsoft.Json.Linq.JToken     msg  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001A4  System.Void .ctor()
END_CLASS

CLASS: PaymentResultCode
TYPE:  sealed struct
TOKEN: 0x2000059
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.SDKInterfaceCloud.PaymentResultCodeSuccess  // const
  public    static  U8.SDK.SDKInterfaceCloud.PaymentResultCodeFailed  // const
METHODS:
END_CLASS

CLASS: SetDataCode
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.SDKInterfaceCloud.SetDataCodeSplashProtocol  // const
  public    static  U8.SDK.SDKInterfaceCloud.SetDataCodeGameAgreement  // const
  public    static  U8.SDK.SDKInterfaceCloud.SetDataCodeSetLanguage  // const
  public    static  U8.SDK.SDKInterfaceCloud.SetDataCodeSetU8Token  // const
  public    static  U8.SDK.SDKInterfaceCloud.SetDataCodeShowGameProtocol  // const
  public    static  U8.SDK.SDKInterfaceCloud.SetDataCodeQueryProductList  // const
  public    static  U8.SDK.SDKInterfaceCloud.SetDataCodeCustomerServiceCenter  // const
  public    static  U8.SDK.SDKInterfaceCloud.SetDataCodeCustomerUnreadMsgCount  // const
  public    static  U8.SDK.SDKInterfaceCloud.SetDataCodeCloudShareEvent  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass35_0
TYPE:  sealed class
TOKEN: 0x200005B
SIZE:  0x18
FIELDS:
  public            System.String                   defaultValue  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001A5  System.Void .ctor()
  RVA=0x02D40530  token=0x60001A6  System.Void <LoadExtraConfig>b__0(System.String data)
END_CLASS

CLASS: <>c__DisplayClass45_0
TYPE:  sealed class
TOKEN: 0x200005C
SIZE:  0x20
FIELDS:
  public            System.String                   defaultValue  // 0x10
  public            System.Int32                    type  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001A7  System.Void .ctor()
  RVA=0x02D40590  token=0x60001A8  System.Void <GetData>b__0(System.String data)
END_CLASS

CLASS: <>c__DisplayClass55_0
TYPE:  sealed class
TOKEN: 0x200005D
SIZE:  0x18
FIELDS:
  public            System.String                   json  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001A9  System.Void .ctor()
  RVA=0x02D40530  token=0x60001AA  System.Void <LoadSDKMeta>b__0(System.String data)
END_CLASS

CLASS: <<HandleCloudShareEvent>b__0>d
TYPE:  sealed struct
TOKEN: 0x200005F
SIZE:  0x40
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>t__builder  // 0x18
  public            U8.SDK.SDKInterfaceCloud.<>c__DisplayClass74_0<>4__this  // 0x30
  private           System.Runtime.CompilerServices.TaskAwaiter<>u__1  // 0x38
METHODS:
  RVA=0x02D3EC60  token=0x60001AD  System.Void MoveNext()
  RVA=0x02D40110  token=0x60001AE  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <>c__DisplayClass74_0
TYPE:  sealed class
TOKEN: 0x200005E
SIZE:  0x20
FIELDS:
  public            U8.SDK.SDKInterfaceCloud        <>4__this  // 0x10
  public            System.String                   jsonData  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001AB  System.Void .ctor()
  RVA=0x02D40940  token=0x60001AC  System.Threading.Tasks.Task <HandleCloudShareEvent>b__0()
END_CLASS

CLASS: <SaveImageToTargetFolder>d__75
TYPE:  sealed struct
TOKEN: 0x2000060
SIZE:  0x48
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>t__builder  // 0x18
  public            System.String                   jsonData  // 0x30
  public            U8.SDK.SDKInterfaceCloud        <>4__this  // 0x38
  private           System.Runtime.CompilerServices.TaskAwaiter<System.Boolean><>u__1  // 0x40
METHODS:
  RVA=0x02D3C490  token=0x60001AF  System.Void MoveNext()
  RVA=0x02D3E840  token=0x60001B0  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <>c__DisplayClass77_0
TYPE:  sealed class
TOKEN: 0x2000061
SIZE:  0x20
FIELDS:
  public            System.String                   methodName  // 0x10
  public            System.String                   paramValue  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001B1  System.Void .ctor()
  RVA=0x02D41100  token=0x60001B2  System.Void <SendMessageInMain>b__0()
END_CLASS

CLASS: DataCallback
TYPE:  sealed class
TOKEN: 0x2000063
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x047562C0  token=0x60001D9  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x60001DA  System.Void Invoke(System.Int32 index, System.String paramValue)
END_CLASS

CLASS: U8SDKSendMessageCallback
TYPE:  sealed class
TOKEN: 0x2000064
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x04756450  token=0x60001DB  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x60001DC  System.Void Invoke(System.String methodName, System.String paramValue)
END_CLASS

CLASS: <>c__DisplayClass34_0
TYPE:  sealed class
TOKEN: 0x2000065
SIZE:  0x18
FIELDS:
  public            System.String                   defaultValue  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001DD  System.Void .ctor()
  RVA=0x053908C0  token=0x60001DE  System.Void <LoadExtraConfig>b__0(System.String data)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000066
SIZE:  0x10
FIELDS:
  public    static readonly U8.SDK.SDKInterfaceWIN.<>c      <>9  // static @ 0x0
  public    static  System.Action<System.Threading.Tasks.Task><>9__42_0  // static @ 0x8
METHODS:
  RVA=0x09DF19AC  token=0x60001DF  System.Void .cctor()
  RVA=0x041E1670  token=0x60001E0  System.Void .ctor()
  RVA=0x09DF0440  token=0x60001E1  System.Void <SetData>b__42_0(System.Threading.Tasks.Task task)
END_CLASS

CLASS: <>c__DisplayClass43_0
TYPE:  sealed class
TOKEN: 0x2000067
SIZE:  0x18
FIELDS:
  public            System.String                   defaultValue  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001E2  System.Void .ctor()
  RVA=0x053908C0  token=0x60001E3  System.Void <GetData>b__0(System.String data)
END_CLASS

CLASS: <>c__DisplayClass51_0
TYPE:  sealed class
TOKEN: 0x2000068
SIZE:  0x18
FIELDS:
  public            System.String                   json  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001E4  System.Void .ctor()
  RVA=0x053908C0  token=0x60001E5  System.Void <LoadSDKMeta>b__0(System.String data)
END_CLASS

CLASS: State
TYPE:  sealed struct
TOKEN: 0x200006E
FIELDS:
  public            System.Int32                    value__  // 0x0
  public    static  U8.SDK.SDKPromiseEnumerator.State<T>NONE  // const
  public    static  U8.SDK.SDKPromiseEnumerator.State<T>FULFILL  // const
  public    static  U8.SDK.SDKPromiseEnumerator.State<T>REJECT  // const
METHODS:
END_CLASS

CLASS: <Yield>d__12
TYPE:  sealed class
TOKEN: 0x200006F
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           System.Object                   <>2__current  // 0x0
  public            U8.SDK.SDKPromiseEnumerator<T>  <>4__this  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001FE  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x60001FF  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000200  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000202  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_onLogoutNextFrameCoroutine>d__13
TYPE:  sealed class
TOKEN: 0x2000078
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000225  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000226  System.Void System.IDisposable.Dispose()
  RVA=0x09DF37F8  token=0x6000227  System.Boolean MoveNext()
  RVA=0x09DF38D0  token=0x6000229  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: FolderType
TYPE:  sealed struct
TOKEN: 0x200007D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.ImageSaver.FolderType    UserPicturesFolder  // const
  public    static  U8.SDK.ImageSaver.FolderType    GameInstalledFolder  // const
METHODS:
END_CLASS

CLASS: ErrorCode
TYPE:  sealed struct
TOKEN: 0x200007E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.ImageSaver.ErrorCode     NotInited  // const
  public    static  U8.SDK.ImageSaver.ErrorCode     ParamsError  // const
  public    static  U8.SDK.ImageSaver.ErrorCode     ImageNotFound  // const
  public    static  U8.SDK.ImageSaver.ErrorCode     ShareChannelError  // const
  public    static  U8.SDK.ImageSaver.ErrorCode     SaveFailed  // const
  public    static  U8.SDK.ImageSaver.ErrorCode     UnknowError  // const
METHODS:
END_CLASS

CLASS: CallbackResult
TYPE:  sealed struct
TOKEN: 0x200007F
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  U8.SDK.ImageSaver.CallbackResultSuccess  // const
  public    static  U8.SDK.ImageSaver.CallbackResultCancelled  // const
  public    static  U8.SDK.ImageSaver.CallbackResultFailed  // const
METHODS:
END_CLASS

CLASS: ImageShareInfo
TYPE:  class
TOKEN: 0x2000080
SIZE:  0x48
FIELDS:
  public            System.Int32                    shareChannel  // 0x10
  public            System.String                   extraData  // 0x18
  public            System.String                   imgPath  // 0x20
  public            System.String                   relativePath  // 0x28
  public            System.Int32                    folderType  // 0x30
  public            System.String                   title  // 0x38
  public            System.String                   desc  // 0x40
METHODS:
  RVA=0x041E1670  token=0x6000247  System.Void .ctor()
END_CLASS

CLASS: <SaveImage>d__5
TYPE:  sealed struct
TOKEN: 0x2000081
SIZE:  0x50
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>t__builder  // 0x18
  public            U8.SDK.ImageSaver               <>4__this  // 0x30
  public            System.String                   paramJson  // 0x38
  private           System.String                   <targetPath>5__2  // 0x40
  private           System.Runtime.CompilerServices.TaskAwaiter<System.Boolean><>u__1  // 0x48
METHODS:
  RVA=0x09DEFE58  token=0x6000248  System.Void MoveNext()
  RVA=0x09DF0278  token=0x6000249  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <SaveCloudGameImage>d__6
TYPE:  sealed struct
TOKEN: 0x2000082
SIZE:  0x48
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Boolean><>t__builder  // 0x18
  public            U8.SDK.ImageSaver               <>4__this  // 0x30
  public            System.String                   paramJson  // 0x38
  private           System.Runtime.CompilerServices.TaskAwaiter<System.Boolean><>u__1  // 0x40
METHODS:
  RVA=0x09DEF69C  token=0x600024A  System.Void MoveNext()
  RVA=0x09DEFAD0  token=0x600024B  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <>c__DisplayClass13_0
TYPE:  sealed class
TOKEN: 0x2000083
SIZE:  0x30
FIELDS:
  public            U8.SDK.ImageSaver               <>4__this  // 0x10
  public            System.String                   sourcePath  // 0x18
  public            System.String                   targetPath  // 0x20
  public            System.String                   imgFolder  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600024C  System.Void .ctor()
  RVA=0x09DF0694  token=0x600024D  System.Boolean <SaveImageToPathAsync>b__0()
END_CLASS

CLASS: <SaveImageToPathAsync>d__13
TYPE:  sealed struct
TOKEN: 0x2000084
SIZE:  0x58
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Boolean><>t__builder  // 0x18
  public            U8.SDK.ImageSaver               <>4__this  // 0x30
  public            System.String                   sourcePath  // 0x38
  public            System.String                   targetPath  // 0x40
  public            System.String                   imgFolder  // 0x48
  private           System.Runtime.CompilerServices.TaskAwaiter<System.Boolean><>u__1  // 0x50
METHODS:
  RVA=0x09DEFB30  token=0x600024E  System.Void MoveNext()
  RVA=0x09DEFDF8  token=0x600024F  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <>c__DisplayClass24_0
TYPE:  sealed class
TOKEN: 0x2000085
SIZE:  0x20
FIELDS:
  public            System.String                   methodName  // 0x10
  public            System.String                   paramValue  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000250  System.Void .ctor()
  RVA=0x09DF086C  token=0x6000251  System.Void <SendMessageInMain>b__0()
END_CLASS

CLASS: TOKEN
TYPE:  sealed struct
TOKEN: 0x2000089
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.MiniJSON.Json.Parser.TOKENNONE  // const
  public    static  U8.SDK.MiniJSON.Json.Parser.TOKENCURLY_OPEN  // const
  public    static  U8.SDK.MiniJSON.Json.Parser.TOKENCURLY_CLOSE  // const
  public    static  U8.SDK.MiniJSON.Json.Parser.TOKENSQUARED_OPEN  // const
  public    static  U8.SDK.MiniJSON.Json.Parser.TOKENSQUARED_CLOSE  // const
  public    static  U8.SDK.MiniJSON.Json.Parser.TOKENCOLON  // const
  public    static  U8.SDK.MiniJSON.Json.Parser.TOKENCOMMA  // const
  public    static  U8.SDK.MiniJSON.Json.Parser.TOKENSTRING  // const
  public    static  U8.SDK.MiniJSON.Json.Parser.TOKENNUMBER  // const
  public    static  U8.SDK.MiniJSON.Json.Parser.TOKENTRUE  // const
  public    static  U8.SDK.MiniJSON.Json.Parser.TOKENFALSE  // const
  public    static  U8.SDK.MiniJSON.Json.Parser.TOKENNULL  // const
METHODS:
END_CLASS

CLASS: Parser
TYPE:  sealed class
TOKEN: 0x2000088
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IO.StringReader          json  // 0x10
PROPERTIES:
  PeekChar  get=0x03140ED0
  NextChar  get=0x03141060
  NextWord  get=0x03140AF0
  NextToken  get=0x03140D50
METHODS:
  RVA=0x031410E0  token=0x6000259  System.Boolean IsWordBreak(System.Char c)
  RVA=0x035A6570  token=0x600025A  System.Void .ctor(System.String jsonString)
  RVA=0x035A6470  token=0x600025B  System.Object Parse(System.String jsonString)
  RVA=0x04702E20  token=0x600025C  System.Void Dispose()
  RVA=0x03140BB0  token=0x600025D  System.Collections.Generic.Dictionary<System.String,System.Object> ParseObject()
  RVA=0x09DE6CFC  token=0x600025E  System.Collections.Generic.List<System.Object> ParseArray()
  RVA=0x03141150  token=0x600025F  System.Object ParseValue()
  RVA=0x03141180  token=0x6000260  System.Object ParseByToken(U8.SDK.MiniJSON.Json.Parser.TOKEN token)
  RVA=0x03140F30  token=0x6000261  System.String ParseString()
  RVA=0x0321A290  token=0x6000262  System.Object ParseNumber()
  RVA=0x03140CE0  token=0x6000263  System.Void EatWhitespace()
END_CLASS

CLASS: Serializer
TYPE:  sealed class
TOKEN: 0x200008A
SIZE:  0x18
FIELDS:
  private           System.Text.StringBuilder       builder  // 0x10
METHODS:
  RVA=0x0403C9D0  token=0x6000268  System.Void .ctor()
  RVA=0x0403C950  token=0x6000269  System.String Serialize(System.Object obj)
  RVA=0x0403CBE0  token=0x600026A  System.Void SerializeValue(System.Object value)
  RVA=0x0403CFB0  token=0x600026B  System.Void SerializeObject(System.Collections.IDictionary obj)
  RVA=0x09DEDF14  token=0x600026C  System.Void SerializeArray(System.Collections.IList anArray)
  RVA=0x0403CDA0  token=0x600026D  System.Void SerializeString(System.String str)
  RVA=0x0403CA40  token=0x600026E  System.Void SerializeOther(System.Object value)
END_CLASS

CLASS: __StaticArrayInitTypeSize=24
TYPE:  sealed struct
TOKEN: 0x200008E
SIZE:  0x28
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=72
TYPE:  sealed struct
TOKEN: 0x200008F
SIZE:  0x58
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x200008D
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=72C8939CFEA086350CA0F084D5A8FFA18BE802D605B6111130EB5143B5882250C4  // static @ 0x0
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=24C9A1311D05AB372579F6B92A220603EAB8191E15071784A11774A076DC45320B  // static @ 0x48
METHODS:
END_CLASS

CLASS: Hypergryph.PlatformFacade.PlatformFacade
TYPE:  class
TOKEN: 0x200008B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600026F  System.Void SetupPlatformFont(Hypergryph.PlatformFacade.IPlatformFont iFont)
END_CLASS

CLASS: Hypergryph.PlatformFacade.IPlatformFont
TYPE:  interface
TOKEN: 0x200008C
FIELDS:
METHODS:
END_CLASS

CLASS: U8.SDK.ExternalPluginLoginParams
TYPE:  sealed struct
TOKEN: 0x2000002
SIZE:  0x38
FIELDS:
  public            System.Action                   nativeLogin  // 0x10
  public            System.Action<System.String>    nativeLoginCustom  // 0x18
  public            System.Action<System.String>    nativeOnLoginSuc  // 0x20
  public            System.Action<System.String>    nativeOnLoginFail  // 0x28
  public            System.Action<U8.SDK.U8MockLogin>markMockLogin  // 0x30
METHODS:
END_CLASS

CLASS: U8.SDK.ExternalPluginLogoutParams
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x28
FIELDS:
  public            System.Action                   nativeLogout  // 0x10
  public            System.Action                   nativeOnLogoutSuc  // 0x18
  public            System.Action                   nativeOnLogoutFail  // 0x20
METHODS:
END_CLASS

CLASS: U8.SDK.ExternalPluginPayParams
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x30
FIELDS:
  public            U8.SDK.U8PayParams              payParams  // 0x10
  public            System.Func<U8.SDK.U8PayParams,U8.SDK.SDKPromise<U8.SDK.U8PayResult>>nativePay  // 0x18
  public            System.Action<U8.SDK.U8PayResult>onPaySuc  // 0x20
  public            System.Action<System.Object>    onPayFail  // 0x28
METHODS:
END_CLASS

CLASS: U8.SDK.IExternalPlugin
TYPE:  interface
TOKEN: 0x2000005
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000001  System.Void Init()
  RVA=-1  // abstract  token=0x6000002  System.Void Login(U8.SDK.ExternalPluginLoginParams args)
  RVA=-1  // abstract  token=0x6000003  System.Void Pay(U8.SDK.ExternalPluginPayParams args)
  RVA=-1  // abstract  token=0x6000004  System.Void Logout(U8.SDK.ExternalPluginLogoutParams args)
  RVA=-1  // abstract  token=0x6000005  System.String GetSDKToken()
  RVA=-1  // abstract  token=0x6000006  System.Boolean TryLoadSDKMeta(System.Func<U8.SDK.SDKMeta> loadMetaNative, U8.SDK.SDKMeta& meta)
  RVA=-1  // abstract  token=0x6000007  System.Void OnProductListChanged(System.Collections.Generic.List<U8.SDK.U8ProductInfo> productList)
  RVA=-1  // abstract  token=0x6000008  System.String GetPayAddition()
END_CLASS

CLASS: U8.SDK.SDKCaptchaHandler
TYPE:  abstract class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000009  System.Collections.IEnumerator FetchCaptchaCoroutine(System.String captchaParams, U8.SDK.SDKCaptchaHandler.Result outResult)
  RVA=0x041E1670  token=0x600000A  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.SDKExternalTools
TYPE:  abstract class
TOKEN: 0x2000009
SIZE:  0x28
FIELDS:
  private   static readonly System.Char[]                   RANDOM_CHAR_MAP  // static @ 0x0
  protected static readonly System.Int32[]                  PAY_CONFIRM_RETRY_INTERVALS  // static @ 0x8
  private   static  U8.SDK.SDKExternalTools         s_instance  // static @ 0x10
  private   static  U8.SDK.IExternalPlugin          s_plugin  // static @ 0x18
  private           System.Boolean                  <inited>k__BackingField  // 0x10
  private           U8.SDK.SDKExternalTools.CaptchaMgr<captchaMgr>k__BackingField  // 0x18
  private           System.String                   <subChannel>k__BackingField  // 0x20
  private   static  System.Int32                    U8_SDK_SET_DATA_SET_SERVER  // static @ 0x20
PROPERTIES:
  inited  get=0x015EFCE0  set=0x04D86130
  plugin  get=0x09DE9390
  captchaMgr  get=0x0385B100  set=0x05392C40
  subChannel  set=0x05390F40
METHODS:
  RVA=-1  // generic def  token=0x600001B  System.Boolean SDKInterfaceInitExternalTools()
  RVA=0x035A5050  token=0x600001C  U8.SDK.SDKExternalTools GetInstance()
  RVA=0x09DE76D4  token=0x600001D  System.Boolean HasInstance()
  RVA=0x09DE7BB4  token=0x600001E  System.String PublicServiceSignKey()
  RVA=0x09DE8580  token=0x600001F  U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ProductInfo>> SDKInterfaceProductListV2()
  RVA=0x09DE7B5C  token=0x6000020  System.Void NotifyProductListUpdated(System.Collections.Generic.List<U8.SDK.U8ProductInfo> productList)
  RVA=0x09DE8628  token=0x6000021  System.String SDKInterfaceSDKToken()
  RVA=0x05395194  token=0x6000022  System.String MakeHttpPostParam(System.Collections.Generic.Dictionary<System.String,System.Object> param)
  RVA=0x03AFA240  token=0x6000023  System.Void LoginWithPlugin(System.Action nativeLogin, System.Action<System.String> nativeLoginCustom, System.Action<U8.SDK.U8MockLogin> markMockLogin)
  RVA=0x09DE7908  token=0x6000024  System.Void LogoutWithPlugin(System.Action nativeLogout)
  RVA=0x09DE8458  token=0x6000025  System.Void SDKInterfacePayWithPlugin(U8.SDK.U8PayParams payParams, System.Func<U8.SDK.U8PayParams,U8.SDK.SDKPromise<U8.SDK.U8PayResult>> nativePay, System.Action<U8.SDK.U8PayResult> onSuc, System.Action<System.Object> onFail)
  RVA=0x09DE83A4  token=0x6000026  System.String SDKInterfaceGetPayAddition()
  RVA=0x09DE87E0  token=0x6000027  System.Void StaticLog(System.String content)
  RVA=0x09DE8748  token=0x6000028  System.Void StaticLogWarning(System.String content)
  RVA=0x09DE86B0  token=0x6000029  System.Void StaticLogError(System.String content)
  RVA=0x035A1FB0  token=0x600002A  System.Void RegisterPlugin(U8.SDK.IExternalPlugin plugin)
  RVA=-1  // generic def  token=0x600002B  T GetValueSafe(System.Collections.Generic.Dictionary<System.String,System.Object> dict, System.String key, T defVal)
  RVA=0x09DE8878  token=0x600002C  System.String U8Url(System.String routeUrl)
  RVA=0x09DE891C  token=0x600002D  System.String U8urlWithoutU8(System.String routeUrl)
  RVA=-1  // generic def  token=0x600002E  U8.SDK.SDKExternalTools.ErrMsgMeta HandleResponseFromU8(U8.SDK.SDKExternalTools.BusType busType, U8.SDK.SDKExternalTools.POSTResult postRet, TResp& resp)
  RVA=0x09DE7610  token=0x600002F  System.Collections.Generic.Dictionary<System.String,System.Object> FromMiniJSON(System.String json)
  RVA=0x09DE771C  token=0x6000030  System.Void InternalInvokeCoroutine(System.Collections.IEnumerator coroutine)
  RVA=0x09DE7778  token=0x6000031  System.Void InternalInvokeNextFrame(System.Action action)
  RVA=0x09DE91C0  token=0x6000032  System.Collections.IEnumerator _NextFrameCoroutine(System.Action action)
  RVA=0x035A2100  token=0x6000033  System.Void InitIfNot()
  RVA=0x041E1670  token=0x6000034  System.Void Init()
  RVA=0x041E1670  token=0x6000035  System.Void SwitchAccount()
  RVA=0x041E1670  token=0x6000036  System.Void OnSDKError(U8.SDK.SDKError error)
  RVA=0x041E1670  token=0x6000037  System.Void OnInvalidProduct(System.Int32 storeId)
  RVA=0x041E1670  token=0x6000038  System.Void OnSDKExtraInfo(System.String jsonData)
  RVA=0x09DE7804  token=0x6000039  U8.SDK.SDKMeta LoadSDKMetaFromPlugin(System.Func<U8.SDK.SDKMeta> nativeLoadMeta)
  RVA=-1  // abstract  token=0x600003A  U8.SDK.SDKCaptchaHandler CreateCaptchaHandler()
  RVA=0x011EC580  token=0x600003B  System.String GetSignKey()
  RVA=-1  // abstract  token=0x600003C  System.Collections.Generic.Dictionary<System.String,System.String> GetDeviceIDs()
  RVA=-1  // abstract  token=0x600003D  U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ProductInfo>> SendGetProductListRequestV2(System.String paramStr)
  RVA=-1  // abstract  token=0x600003E  System.Int32 GetPlatformKey()
  RVA=-1  // abstract  token=0x600003F  System.Void POSTImplementation(U8.SDK.SDKExternalTools.POSTRequest request, System.Action<U8.SDK.SDKExternalTools.POSTResult> callback)
  RVA=-1  // abstract  token=0x6000040  System.String U8RootUrl()
  RVA=-1  // abstract  token=0x6000041  System.String GetErrorMessage(U8.SDK.SDKExternalTools.ErrMsgMeta meta)
  RVA=-1  // abstract  token=0x6000042  System.Void Log(System.String content)
  RVA=-1  // abstract  token=0x6000043  System.Void LogWarning(System.String content)
  RVA=-1  // abstract  token=0x6000044  System.Void LogError(System.String content)
  RVA=0x04D36530  token=0x6000045  System.Boolean _ValidatePlugin()
  RVA=0x09DE8A14  token=0x6000046  System.Void _AddAppIdOrAppCode(System.Collections.Generic.Dictionary<System.String,System.Object> paramDict, U8.SDK.SDKMeta meta)
  RVA=0x09DE8EE4  token=0x6000047  System.String _MakeGetProductListV2Param(U8.SDK.SDKMeta meta)
  RVA=0x09DE7F18  token=0x6000048  System.Collections.IEnumerator SDKInterfaceAuthV2SessionToken(U8.SDK.SDKMeta meta, System.String channelToken, U8.SDK.SDKPromiseWithResult<U8.SDK.U8LoginV2Result> promise)
  RVA=0x09DE7E70  token=0x6000049  System.Collections.IEnumerator SDKInterfaceAuthV2OAuth2Code(System.String sessionToken, U8.SDK.SDKPromiseWithResult<U8.SDK.U8GrantResult> promise, System.Action onSessionInvalid)
  RVA=0x09DE7FC0  token=0x600004A  U8.SDK.SDKPromise<U8.SDK.U8ConfirmOrderResult> SDKInterfaceConfirmOrderU1(System.String orderId, System.String extension)
  RVA=0x09DE7D54  token=0x600004B  U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ServerInfo>> SDKInterFaceGetServerList(System.String sessionToken)
  RVA=0x09DE7C00  token=0x600004C  U8.SDK.SDKPromise<U8.SDK.U8ConfirmServerResult> SDKInterFaceConfirmServer(System.String sessionToken, System.String serverId)
  RVA=0x09DE8074  token=0x600004D  U8.SDK.SDKPromise<U8.SDK.U8OrderInfo> SDKInterfaceCreateOrderU1(System.String sessionToken, System.String productId, System.String signParams)
  RVA=0x09DE900C  token=0x600004E  System.String _MakeGrantParam(System.String sessionToken, System.Int32 type, System.String captcha)
  RVA=0x09DE8AAC  token=0x600004F  System.Collections.IEnumerator _AuthV2GrantImpl(System.String sessionToken, U8.SDK.SDKPromiseWithResult<U8.SDK.U8GrantResult> promise, System.Action onSessionInvalid)
  RVA=0x09DE8D30  token=0x6000050  System.String _MakeCheckOrderV1Param(System.String orderId, System.String extension)
  RVA=0x09DE8E28  token=0x6000051  System.String _MakeConfirmOrderV1Param(System.String orderId)
  RVA=0x09DE8B54  token=0x6000052  System.Collections.IEnumerator _ConfirmOrderU1Coroutine(System.String orderId, System.String extension, U8.SDK.SDKPromise<U8.SDK.U8ConfirmOrderResult> promise)
  RVA=0x09DE921C  token=0x6000053  System.Collections.IEnumerator _PollOrderStatusCoroutine(System.String orderId, System.String extension, U8.SDK.SDKPromise<U8.SDK.U8ConfirmOrderResult> promise)
  RVA=0x09DE8CA4  token=0x6000054  System.Collections.IEnumerator _GetServerListCoroutine(System.String paramStr, U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ServerInfo>> promise)
  RVA=0x09DE8BFC  token=0x6000055  System.Collections.IEnumerator _ConfirmServerListCoroutine(System.String paramStr, System.String serverId, U8.SDK.SDKPromise<U8.SDK.U8ConfirmServerResult> promise)
  RVA=0x09DE9304  token=0x6000056  System.Collections.IEnumerator _SendCreateOrderU1Coroutine(System.String paramStr, U8.SDK.SDKPromise<U8.SDK.U8OrderInfo> promise)
  RVA=0x09DE92A8  token=0x6000057  System.Collections.IEnumerator _SendCreateOrderFailCoroutine(U8.SDK.SDKPromise<U8.SDK.U8OrderInfo> promise)
  RVA=0x041E1670  token=0x6000058  System.Void .ctor()
  RVA=0x04B47280  token=0x6000059  System.Void .cctor()
END_CLASS

CLASS: U8.SDK.SDKExtraData
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x20
FIELDS:
  public    static readonly U8.SDK.SDKExtraData             EMPTY  // static @ 0x0
  public            System.Int32                    code  // 0x10
  public            System.Collections.Generic.Dictionary<System.String,System.Object>msg  // 0x18
METHODS:
  RVA=0x04441A40  token=0x60000D4  U8.SDK.SDKExtraData FromJson(System.String jsonStr)
  RVA=0x04D4E810  token=0x60000D5  System.Void .cctor()
END_CLASS

CLASS: U8.SDK.SDKExternalToolsShortcut
TYPE:  abstract class
TOKEN: 0x200002C
SIZE:  0x28
EXTENDS: U8.SDK.SDKExternalTools
FIELDS:
METHODS:
  RVA=0x09DE7404  token=0x60000D6  U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ProductInfo>> SendGetProductListRequestV2(System.String paramStr)
  RVA=0x09DE7100  token=0x60000D7  System.Collections.Generic.Dictionary<System.String,System.String> GetDeviceIDs()
  RVA=0x09DE720C  token=0x60000D8  System.String GetU8DeviceID()
  RVA=0x09DE732C  token=0x60000D9  System.Void InjectRequestHeaders(System.Collections.Generic.Dictionary<System.String,System.String>& headers, U8.SDK.SDKExternalToolsShortcut.RequestHeaderInjectOptions policy)
  RVA=0x09DE75D4  token=0x60000DA  System.String _GetProductListV1Url()
  RVA=0x04D778E0  token=0x60000DB  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8SDKInterface
TYPE:  abstract class
TOKEN: 0x2000031
SIZE:  0xD0
FIELDS:
  private   static  U8.SDK.U8SDKInterface           m_instance  // static @ 0x0
  protected         U8.SDK.SDKPromiseWrapper        m_initPromise  // 0x10
  protected         U8.SDK.SDKPromiseWrapper        m_loginPromise  // 0x18
  protected         U8.SDK.SDKPromiseWrapper        m_logoutPromise  // 0x20
  protected         U8.SDK.SDKPromiseWrapper        m_payPromise  // 0x28
  private           U8.SDK.U8LoginResult            m_loginResult  // 0x30
  private           U8.SDK.U8SDKInterface.AuthStatusm_authStatus  // 0x68
  private           System.String                   m_loginExtV1  // 0x88
  private           U8.SDK.U8MockLogin              m_mockLogin  // 0x90
  private           System.Collections.Generic.List<U8.SDK.U8ProductInfo>m_productList  // 0xa0
  private           System.Nullable<System.Boolean> m_isNativePlugin  // 0xa8
  private           U8.SDK.U8SDKInterface.V2LoginStatusm_loginStatusV2  // 0xb0
  private   static  System.Int32                    U8_SDK_SET_DATA_SET_SESSION_TOKEN  // static @ 0x8
  private   static  System.Int32                    U8_SDK_SET_DATA_GET_PRODUCT_LIST  // static @ 0xc
  private   static  U8.SDK.U8SDKInterface.ReceiverProductListResultonReceiverProductlist  // static @ 0x10
  private           U8.SDK.U8SDKInterface.GameServerTimeCachem_gameServerTimeCache  // 0xb8
PROPERTIES:
  Instance  get=0x03A72EC0
  isNativePlugin  get=0x03AF78D0
  uid  get=0x04D86230
  cachedUid  get=0x03811920  set=0x03F4BAD0
  oauth2token  get=0x04D862A0
  oauth2code  get=0x04D862B0
  isInterfaceDisabled  get=0x012081B0
  sessionToken  get=0x04D86250
  serverCacheTime  get=0x04DA2EA0  set=0x04DA2EC0
EVENTS:
  onReceiverProductlist  add=add_onReceiverProductlist  remove=remove_onReceiverProductlist
METHODS:
  RVA=0x049DAA10  token=0x60000E3  System.Void CallbackInitSuc(System.String extConfigs)
  RVA=0x09DF4BB4  token=0x60000E4  System.Void CallbackInitFail(System.String info)
  RVA=0x049DAA90  token=0x60000E5  System.Void CallbackLoginSuc(System.String extension)
  RVA=0x09DF4BD4  token=0x60000E6  System.Void CallbackLoginFail(System.String info)
  RVA=0x09DF4BF4  token=0x60000E7  System.Void CallbackLogout()
  RVA=0x09DF4D40  token=0x60000E8  System.Void CallbackSwitchAccount()
  RVA=0x09DF4CAC  token=0x60000E9  System.Void CallbackPaySuc(System.String jsonData)
  RVA=0x09DF4C18  token=0x60000EA  System.Void CallbackPayFail(System.String failMsg)
  RVA=0x05395664  token=0x60000F1  System.String PublicLoadExtraConfig()
  RVA=0x053981E0  token=0x60000F2  System.Void PublicSetGameVersion(System.String version)
  RVA=0x09DF5F70  token=0x60000F3  U8.SDK.U8SDKInterface.ReceiverProductListResult getOnReceiverProductlist()
  RVA=-1  // generic def  token=0x60000F4  U8.SDK.SDKPromise<System.String> V2PublicInit(System.String env)
  RVA=0x04D84E50  token=0x60000F5  System.Void _MarkMockLogin(U8.SDK.U8MockLogin mockLogin)
  RVA=0x04CA4650  token=0x60000F6  System.Void _onLoginSuc(System.String uid, System.Boolean isNew)
  RVA=0x09DF51D0  token=0x60000F7  U8.SDK.SDKPromise<U8.SDK.U8ProductsResult> PublicGetProductListV2()
  RVA=0x09DF4D8C  token=0x60000F8  System.Void ClearAuthStatus()
  RVA=0x09DF4DE8  token=0x60000F9  System.Void ClearLoginStatus()
  RVA=0x03F4BA90  token=0x60000FA  System.Void _ConfirmAuthResult(U8.SDK.U8SDKInterface.AuthStatus result)
  RVA=0x09DF53D4  token=0x60000FB  U8.SDK.SDKPromise<System.Object> PublicLogout()
  RVA=0x09DF52F0  token=0x60000FC  U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ServerInfo>> PublicGetServerList()
  RVA=0x09DF56BC  token=0x60000FD  U8.SDK.SDKPromise<U8.SDK.U8ConfirmServerResult> PubliceConfirmServer(System.String serverId)
  RVA=0x09DF5474  token=0x60000FE  U8.SDK.SDKPromise<U8.SDK.U8PayResult> PublicPay(System.Int32 storeId, U8.SDK.U8OrderInfo orderInfo)
  RVA=0x09DF5CF0  token=0x60000FF  System.Void _OnNativePayFulfilled(System.Int64 revenue)
  RVA=0x09DF5DCC  token=0x6000100  System.Boolean _TryGetProduct(System.Int32 storeId, U8.SDK.U8ProductInfo& productInfo)
  RVA=0x09DF5A70  token=0x6000101  U8.SDK.U8PayParams _GeneratePayParams(U8.SDK.U8ProductInfo productInfo, U8.SDK.U8OrderInfo orderInfo)
  RVA=0x09DF5C0C  token=0x6000102  U8.SDK.SDKPromise<U8.SDK.U8PayResult> _NativePay(U8.SDK.U8PayParams payParams)
  RVA=-1  // generic def  token=0x6000103  T GetValueSafe(System.Collections.Generic.Dictionary<System.String,System.Object> dict, System.String key, T defVal)
  RVA=0x09DF52F8  token=0x6000104  U8.SDK.SDKMeta PublicLoadSDKMeta()
  RVA=-1  // abstract  token=0x6000105  System.String LoadExtraConfig()
  RVA=-1  // abstract  token=0x6000106  System.Void V2Init(System.String env)
  RVA=-1  // abstract  token=0x6000107  System.Void Login()
  RVA=-1  // abstract  token=0x6000108  System.Void LoginCustom(System.String customData)
  RVA=-1  // abstract  token=0x6000109  System.Boolean Logout()
  RVA=-1  // abstract  token=0x600010A  System.Boolean ShowAccountCenter()
  RVA=-1  // abstract  token=0x600010B  System.Void SubmitGameData(U8.SDK.U8ExtraGameData data)
  RVA=-1  // abstract  token=0x600010C  System.Void Pay(U8.SDK.U8PayParams data)
  RVA=-1  // abstract  token=0x600010D  System.Void SetData(System.Int32 type, System.String paramJson)
  RVA=-1  // abstract  token=0x600010E  System.String GetData(System.Int32 type, System.String paramJson)
  RVA=-1  // abstract  token=0x600010F  U8.SDK.SDKMeta LoadSDKMeta()
  RVA=-1  // abstract  token=0x6000110  System.Boolean IsNativePlugin()
  RVA=-1  // abstract  token=0x6000111  System.Void SetGameVersion(System.String version)
  RVA=0x03A84D40  token=0x6000112  System.Collections.IEnumerator _NextFrameCoroutine(System.Action call)
  RVA=0x03A84CB0  token=0x6000113  System.Void InvokeNextFrame(System.Action action)
  RVA=0x040EB180  token=0x6000114  System.Void InvokeCoroutine(System.Collections.IEnumerator coroutine)
  RVA=0x03AF99C0  token=0x6000115  System.String EncodeGameData(U8.SDK.U8ExtraGameData data)
  RVA=0x09DF4E14  token=0x6000116  System.String EncodePayParams(U8.SDK.U8PayParams data)
  RVA=0x040EB080  token=0x600011C  U8.SDK.SDKPromise<System.String> V2PublicLogin()
  RVA=0x03A84850  token=0x600011D  U8.SDK.SDKPromise<U8.SDK.U8AuthV2Result> V2PublicAuth()
  RVA=0x09DF5798  token=0x600011E  U8.SDK.SDKPromise<U8.SDK.U8OrderInfo> U1PublicCreateOrder(System.String productId, System.String signParams)
  RVA=0x09DF5818  token=0x600011F  U8.SDK.SDKPromise<System.Collections.Generic.List<U8.SDK.U8ServerInfo>> U1PublicGetServerList()
  RVA=0x09DF572C  token=0x6000120  U8.SDK.SDKPromise<U8.SDK.U8ConfirmServerResult> U1PublicConfirmServer(System.String serverId)
  RVA=0x09DF56C4  token=0x6000121  U8.SDK.SDKPromise<U8.SDK.U8ConfirmOrderResult> U1PublicConfirmOrder(System.String orderId, System.String extension)
  RVA=0x03A848B0  token=0x6000122  U8.SDK.SDKPromise<U8.SDK.U8AuthV2Result> _DoMockAuthV2()
  RVA=0x040EB100  token=0x6000123  System.Collections.IEnumerator _LoginV2Coroutine(U8.SDK.SDKPromise<System.String> promise)
  RVA=0x09DF5DAC  token=0x6000124  System.Void _OnSessionTokenInvalid()
  RVA=0x09DF59FC  token=0x6000125  System.Collections.IEnumerator _AuthV2Coroutine(U8.SDK.SDKPromise<U8.SDK.U8AuthV2Result> promise)
  RVA=0x047564D0  token=0x6000126  System.Void .ctor()
  RVA=0x04D5D230  token=0x6000127  System.Void .cctor()
  RVA=0x09DF5874  token=0x6000128  System.Void <PublicLogout>b__65_0()
  RVA=0x09DF5998  token=0x6000129  System.Void <PublicLogout>b__65_1()
  RVA=0x09DF59A8  token=0x600012A  System.Void <PublicLogout>b__65_2()
END_CLASS

CLASS: U8.SDK.SDKError
TYPE:  sealed struct
TOKEN: 0x2000041
SIZE:  0x20
FIELDS:
  private           System.String                   <code>k__BackingField  // 0x10
  private           System.String                   <msg>k__BackingField  // 0x18
PROPERTIES:
  code  set=0x05392320
  msg  set=0x067D5E9C
METHODS:
  RVA=0x09DE6ED8  token=0x600015A  System.Void .ctor(System.String content)
END_CLASS

CLASS: U8.SDK.U8MockLogin
TYPE:  sealed struct
TOKEN: 0x2000042
SIZE:  0x20
FIELDS:
  public    static readonly U8.SDK.U8MockLogin              EMPTY  // static @ 0x0
  public            System.String                   uid  // 0x10
  public            System.String                   token  // 0x18
METHODS:
  RVA=0x05394A74  token=0x600015B  System.Boolean IsEmpty()
END_CLASS

CLASS: U8.SDK.U8ProductInfo
TYPE:  class
TOKEN: 0x2000043
SIZE:  0x48
FIELDS:
  public            System.Int32                    store_id  // 0x10
  public            System.String                   product_id  // 0x18
  public            System.String                   desc  // 0x20
  public            System.String                   name  // 0x28
  public            System.Int64                    price  // 0x30
  public            System.String                   channel_product_id  // 0x38
  public            System.String                   display_price  // 0x40
METHODS:
  RVA=0x041E1670  token=0x600015C  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8ProductsResult
TYPE:  class
TOKEN: 0x2000044
SIZE:  0x20
FIELDS:
  public            System.Int32                    status  // 0x10
  public            System.Collections.Generic.List<U8.SDK.U8ProductInfo>u8Products  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600015D  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8ServerInfo
TYPE:  class
TOKEN: 0x2000045
SIZE:  0x50
FIELDS:
  public            System.String                   serverId  // 0x10
  public            System.String                   serverName  // 0x18
  public            System.String                   serverDomain  // 0x20
  public            System.Boolean                  defaultChoose  // 0x28
  public            System.String                   roleId  // 0x30
  public            System.String                   nickName  // 0x38
  public            System.Int64                    level  // 0x40
  public            System.String                   extension  // 0x48
METHODS:
  RVA=0x041E1670  token=0x600015E  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8ServerErrorInfo
TYPE:  class
TOKEN: 0x2000046
SIZE:  0x18
FIELDS:
  public            System.Int32                    status  // 0x10
  public            System.Int32                    errorCode  // 0x14
METHODS:
  RVA=0x041E1670  token=0x600015F  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8OrderInfo
TYPE:  class
TOKEN: 0x2000047
SIZE:  0x40
FIELDS:
  public            System.String                   orderId  // 0x10
  public            System.String                   channelProductCode  // 0x18
  public            System.String                   productName  // 0x20
  public            System.String                   productDesc  // 0x28
  public            System.Int64                    amount  // 0x30
  public            System.String                   extension  // 0x38
METHODS:
  RVA=0x041E1670  token=0x6000160  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8PayParams
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x98
FIELDS:
  public            System.String                   productId  // 0x10
  public            System.String                   productName  // 0x18
  public            System.String                   productDesc  // 0x20
  public            System.Int64                    price  // 0x28
  public            System.Int32                    buyNum  // 0x30
  public            System.Int32                    coinNum  // 0x34
  public            System.String                   serverId  // 0x38
  public            System.String                   serverName  // 0x40
  public            System.String                   roleId  // 0x48
  public            System.String                   roleName  // 0x50
  public            System.Int32                    roleLevel  // 0x58
  public            System.String                   vip  // 0x60
  public            System.String                   orderID  // 0x68
  public            System.String                   worldID  // 0x70
  public            System.String                   token  // 0x78
  public            System.String                   appKey  // 0x80
  public            System.String                   extension  // 0x88
  public            System.String                   addition  // 0x90
METHODS:
  RVA=0x041E1670  token=0x6000161  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8ExtraGameData
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x78
FIELDS:
  public            System.Int32                    dataType  // 0x10
  public            System.String                   uid  // 0x18
  public            System.String                   roleID  // 0x20
  public            System.String                   roleName  // 0x28
  public            System.String                   roleLevel  // 0x30
  public            System.Int32                    serverID  // 0x38
  public            System.String                   serverName  // 0x40
  public            System.String                   channel  // 0x48
  public            System.String                   subChannel  // 0x50
  public            System.Boolean                  isNewUser  // 0x58
  public            System.Int64                    revenue  // 0x60
  public            System.String                   customEventName  // 0x68
  public            System.String                   customEventParams  // 0x70
METHODS:
  RVA=0x041E1670  token=0x6000162  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8LoginResult
TYPE:  sealed struct
TOKEN: 0x200004A
SIZE:  0x48
FIELDS:
  public    static readonly U8.SDK.U8LoginResult            EMPTY  // static @ 0x0
  public            System.Int32                    result  // 0x10
  public            System.String                   uid  // 0x18
  public            System.String                   channelUid  // 0x20
  public            System.String                   token  // 0x28
  public            System.String                   extension  // 0x30
  public            System.Boolean                  isGuest  // 0x38
  public            System.Boolean                  isNew  // 0x39
  public            System.String                   error  // 0x40
METHODS:
  RVA=0x09DF391C  token=0x6000163  System.String ToString()
END_CLASS

CLASS: U8.SDK.U8ProductError
TYPE:  sealed struct
TOKEN: 0x200004B
SIZE:  0x18
FIELDS:
  public            System.Int32                    status  // 0x10
  public            System.Int32                    errorCode  // 0x14
METHODS:
END_CLASS

CLASS: U8.SDK.PayResultStatus
TYPE:  sealed struct
TOKEN: 0x200004C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.PayResultStatus          SUCCEED  // const
  public    static  U8.SDK.PayResultStatus          PENDING  // const
METHODS:
END_CLASS

CLASS: U8.SDK.PayFailStatus
TYPE:  sealed struct
TOKEN: 0x200004D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.PayFailStatus            UNKNOWN  // const
  public    static  U8.SDK.PayFailStatus            CANCELED  // const
  public    static  U8.SDK.PayFailStatus            ERROR  // const
  public    static  U8.SDK.PayFailStatus            HALT  // const
METHODS:
END_CLASS

CLASS: U8.SDK.U8ConfirmServerResult
TYPE:  class
TOKEN: 0x200004E
SIZE:  0x20
FIELDS:
  public            System.Int32                    status  // 0x10
  public            System.String                   message  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000164  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8ConfirmOrderStatus
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.U8ConfirmOrderStatus     NONE  // const
  public    static  U8.SDK.U8ConfirmOrderStatus     INTERNAL_ERROR  // const
  public    static  U8.SDK.U8ConfirmOrderStatus     TIMEOUT  // const
  public    static  U8.SDK.U8ConfirmOrderStatus     INVALID_ORDER  // const
  public    static  U8.SDK.U8ConfirmOrderStatus     SUCCEED  // const
METHODS:
END_CLASS

CLASS: U8.SDK.U8ConfirmOrderResult
TYPE:  class
TOKEN: 0x2000050
SIZE:  0x20
FIELDS:
  public            U8.SDK.U8ConfirmOrderStatus     status  // 0x10
  public            System.String                   message  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000165  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8PayFailMsg
TYPE:  sealed struct
TOKEN: 0x2000051
SIZE:  0x28
FIELDS:
  public    static  U8.SDK.U8PayFailMsg             EMPTY  // static @ 0x0
  public            U8.SDK.PayFailStatus            status  // 0x10
  public            System.String                   message  // 0x18
  public            System.String                   extension  // 0x20
METHODS:
  RVA=0x09DF3B64  token=0x6000166  U8.SDK.U8PayFailMsg FromJson(System.String jsonStr)
  RVA=0x09DF3F54  token=0x6000167  System.Void .cctor()
END_CLASS

CLASS: U8.SDK.U8PayResult
TYPE:  sealed struct
TOKEN: 0x2000052
SIZE:  0x28
FIELDS:
  public    static readonly U8.SDK.U8PayResult              EMPTY  // static @ 0x0
  public            U8.SDK.PayResultStatus          status  // 0x10
  public            System.String                   outTradeNo  // 0x18
  public            System.String                   extension  // 0x20
METHODS:
  RVA=0x09DF3FB8  token=0x6000168  U8.SDK.U8PayResult FromJson(System.String jsonData)
  RVA=0x09DF4338  token=0x6000169  System.String ToString()
  RVA=0x09DF4424  token=0x600016A  System.Void .cctor()
END_CLASS

CLASS: U8.SDK.SDKInterfaceCloud
TYPE:  class
TOKEN: 0x2000053
SIZE:  0xE0
EXTENDS: U8.SDK.U8SDKInterface
FIELDS:
  private           System.Int32                    actionIndex  // 0xd0
  private   static  System.Collections.Generic.Dictionary<System.Int32,System.Action<System.String>>dictAction  // static @ 0x0
  private           System.String                   languageParam  // 0xd8
METHODS:
  RVA=0x09DED060  token=0x600016B  System.Void .ctor()
  RVA=0x09DEA138  token=0x600016C  System.Void Finalize()
  RVA=0x09DE95CC  token=0x600016D  System.Int32 AddAction(System.Action<System.String> action)
  RVA=0x09DE9F4C  token=0x600016E  System.Void DataActionCallback(System.Int32 index, System.String paramValue)
  RVA=0x09DE93E0  token=0x600016F  System.Void ASyncDataActionCallback(System.Int32 index, System.String paramValue)
  RVA=0x09DECE5C  token=0x6000170  System.Void U8SDKSendMessage(System.String methodName, System.String paramValue)
  RVA=0x09DEA790  token=0x6000171  System.Void HGU8SDKInitLibrary(System.String windowText, System.Int64 windowHandle)
  RVA=0x09DEA8EC  token=0x6000172  System.Int32 HGU8SDKSetUserDataPath(System.String path)
  RVA=0x09DEA940  token=0x6000173  System.Void HGU8SDKUnInitLibrary()
  RVA=0x09DEA740  token=0x6000174  System.Void HGU8SDKInitDatacallback(U8.SDK.SDKInterfaceCloud.DataCallback Callback, U8.SDK.SDKInterfaceCloud.DataCallback asyncCallback)
  RVA=0x09DEA7E0  token=0x6000175  System.Void HGU8SDKInitSendMessage(U8.SDK.SDKInterfaceCloud.U8SDKSendMessageCallback callback)
  RVA=0x09DEA964  token=0x6000176  System.Void HGU8SDKV2Init(System.String env)
  RVA=0x09DEA714  token=0x6000177  System.Void HGU8SDKGetSDKMeta(System.Int32 index)
  RVA=0x09DEA854  token=0x6000178  System.Void HGU8SDKSetData(System.Int32 code, System.String paramJson)
  RVA=0x09DEA6B4  token=0x6000179  System.Void HGU8SDKGetData(System.Int32 code, System.String paramJson, System.Int32 index)
  RVA=0x09DEA828  token=0x600017A  System.Void HGU8SDKLoadExtraConfig(System.Int32 index)
  RVA=0x09DEA8A4  token=0x600017B  System.Void HGU8SDKSetGameVersion(System.String version)
  RVA=0x09DEB5A4  token=0x600017C  System.String LoadExtraConfig()
  RVA=0x09DECEC4  token=0x600017D  System.Void V2Init(System.String env)
  RVA=0x09DEBAFC  token=0x600017E  System.Void Login()
  RVA=0x09DEB7D0  token=0x600017F  System.Void LoginCallback(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DEBC1C  token=0x6000180  System.Boolean Logout()
  RVA=0x09DEBAEC  token=0x6000181  System.Void LoginCustom(System.String customData)
  RVA=0x09DECD8C  token=0x6000182  System.Void SubmitGameData(U8.SDK.U8ExtraGameData data)
  RVA=0x09DEC530  token=0x6000183  System.Void SetData(System.Int32 type, System.String paramJson)
  RVA=0x09DEA298  token=0x6000184  System.String GetData(System.Int32 type, System.String paramJson)
  RVA=0x09DECA34  token=0x6000185  System.Boolean ShowAccountCenter()
  RVA=0x09DEC7CC  token=0x6000186  System.Void ShowAccountCenterCallback(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DEC15C  token=0x6000187  System.Void Pay(U8.SDK.U8PayParams data)
  RVA=0x09DEBE40  token=0x6000188  System.Void PayCallback(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DEB6A0  token=0x6000189  U8.SDK.SDKMeta LoadSDKMeta()
  RVA=0x02FFF600  token=0x600018A  System.Boolean IsNativePlugin()
  RVA=0x09DEC6FC  token=0x600018B  System.Void SetGameVersion(System.String version)
  RVA=0x09DEB550  token=0x600018C  System.Void HandleSplashProtocol(System.String paramJson)
  RVA=0x02D39980  token=0x600018D  System.Void HandleGameAgreement(System.String paramJson)
  RVA=0x02D3B430  token=0x600018E  System.Void HandleSetLanguage(System.String paramJson)
  RVA=0x09DEB304  token=0x600018F  System.Void HandleSetU8Token(System.String paramJson)
  RVA=0x09DEB410  token=0x6000190  System.Void HandleShowGameProtocol(System.String paramJson)
  RVA=0x09DECB30  token=0x6000191  System.Void ShowGameProtocolCallback(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DEB0A8  token=0x6000192  System.Void HandleQueryProductList(System.String paramJson)
  RVA=0x09DEAE28  token=0x6000193  System.Void HandleCustomerServiceCenter(System.String paramJson)
  RVA=0x09DE9A0C  token=0x6000194  System.Void CustomerServiceCenterCallback(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DEAF68  token=0x6000195  System.Void HandleCustomerUnreadMsgCount(System.String paramJson)
  RVA=0x09DE9C68  token=0x6000196  System.Void CustomerUnreadMsgCountCallback(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DEA9A4  token=0x6000197  System.Void HandleCloudShareEvent(System.String paramJson, System.String fileNamePrefix)
  RVA=0x09DEC388  token=0x6000198  System.Threading.Tasks.Task SaveImageToTargetFolder(System.String jsonData)
  RVA=0x09DE9724  token=0x6000199  System.Void CloudShareEventCallback(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DEC458  token=0x600019A  System.Void SendMessageInMain(System.String methodName, System.String paramValue)
  RVA=0x09DEBC6C  token=0x600019B  System.Void OnPipelineConnected(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DEC274  token=0x600019C  System.Void RequestLaunchInfoCallback(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DEA3BC  token=0x600019D  System.Void GetThinClientDeviceInfo(System.String jsonData)
  RVA=0x09DECFD4  token=0x600019E  System.Void .cctor()
END_CLASS

CLASS: U8.SDK.SDKInterfaceWIN
TYPE:  class
TOKEN: 0x2000062
SIZE:  0xD8
EXTENDS: U8.SDK.U8SDKInterface
FIELDS:
  private           System.Int32                    actionIndex  // 0xd0
  private   static  System.Collections.Generic.Dictionary<System.Int32,System.Action<System.String>>dictAction  // static @ 0x0
METHODS:
  RVA=0x04756120  token=0x60001B3  System.Void .ctor()
  RVA=0x09DED42C  token=0x60001B4  System.Void Finalize()
  RVA=0x03AFA8E0  token=0x60001B5  System.Int32 AddAction(System.Action<System.String> action)
  RVA=0x03A849A0  token=0x60001B6  System.Void DataActionCallback(System.Int32 index, System.String paramValue)
  RVA=0x09DED240  token=0x60001B7  System.Void ASyncDataActionCallback(System.Int32 index, System.String paramValue)
  RVA=0x03A84DA0  token=0x60001B8  System.Void U8SDKSendMessage(System.String methodName, System.String paramValue)
  RVA=0x047563F0  token=0x60001B9  System.Void HGU8SDKInitLibrary(System.String windowText, System.Int64 windowHandle)
  RVA=0x04756390  token=0x60001BA  System.Int32 HGU8SDKSetUserDataPath(System.String path)
  RVA=0x09DED76C  token=0x60001BB  System.Void HGU8SDKUnInitLibrary()
  RVA=0x03826E60  token=0x60001BC  System.Void HGU8SDKInitDatacallback(U8.SDK.SDKInterfaceWIN.DataCallback Callback, U8.SDK.SDKInterfaceWIN.DataCallback asyncCallback)
  RVA=0x03826BA0  token=0x60001BD  System.Void HGU8SDKInitSendMessage(U8.SDK.SDKInterfaceWIN.U8SDKSendMessageCallback callback)
  RVA=0x0488EC70  token=0x60001BE  System.Void HGU8SDKV2Init(System.String env)
  RVA=0x09DED5AC  token=0x60001BF  System.Void HGU8SDKLogin()
  RVA=0x09DED524  token=0x60001C0  System.Void HGU8SDKLoginCustom(System.String json)
  RVA=0x09DED614  token=0x60001C1  System.Int32 HGU8SDKLogout()
  RVA=0x09DED704  token=0x60001C2  System.Int32 HGU8SDKShowAccountCenter()
  RVA=0x09DED67C  token=0x60001C3  System.Void HGU8SDKPay(System.String json)
  RVA=0x03AF9CD0  token=0x60001C4  System.Void HGU8SDKSubmitGameData(System.String json)
  RVA=0x09DED4F8  token=0x60001C5  System.Void HGU8SDKGetSDKMeta(System.Int32 index)
  RVA=0x03A86A60  token=0x60001C6  System.Void HGU8SDKSetData(System.Int32 code, System.String paramJson)
  RVA=0x03AFAE80  token=0x60001C7  System.Void HGU8SDKGetData(System.Int32 code, System.String paramJson, System.Int32 index)
  RVA=0x03826C30  token=0x60001C8  System.Int32 HGU8SDKIsNativePlugin()
  RVA=0x03AFAEF0  token=0x60001C9  System.Void HGU8SDKLoadExtraConfig(System.Int32 index)
  RVA=0x04CC88A0  token=0x60001CA  System.Void HGU8SDKSetGameVersion(System.String version)
  RVA=0x03AFA690  token=0x60001CB  System.String LoadExtraConfig()
  RVA=0x0488EB00  token=0x60001CC  System.Void V2Init(System.String env)
  RVA=0x09DED90C  token=0x60001CD  System.Void Login()
  RVA=0x09DED94C  token=0x60001CE  System.Boolean Logout()
  RVA=0x09DED8C0  token=0x60001CF  System.Void LoginCustom(System.String customData)
  RVA=0x03AF9950  token=0x60001D0  System.Void SubmitGameData(U8.SDK.U8ExtraGameData data)
  RVA=0x03A869B0  token=0x60001D1  System.Void SetData(System.Int32 type, System.String paramJson)
  RVA=0x03AFA7A0  token=0x60001D2  System.String GetData(System.Int32 type, System.String paramJson)
  RVA=0x09DED9F4  token=0x60001D3  System.Boolean ShowAccountCenter()
  RVA=0x09DED994  token=0x60001D4  System.Void Pay(U8.SDK.U8PayParams data)
  RVA=0x09DED790  token=0x60001D5  U8.SDK.SDKMeta LoadSDKMeta()
  RVA=0x04D59840  token=0x60001D6  System.Boolean IsNativePlugin()
  RVA=0x04CC8850  token=0x60001D7  System.Void SetGameVersion(System.String version)
  RVA=0x04D16E60  token=0x60001D8  System.Void .cctor()
END_CLASS

CLASS: U8.SDK.SDKMeta
TYPE:  class
TOKEN: 0x2000069
SIZE:  0x40
FIELDS:
  public            System.String                   appID  // 0x10
  public            System.String                   appKey  // 0x18
  public            System.String                   channel  // 0x20
  public            System.String                   worldId  // 0x28
  public            System.String                   extension  // 0x30
  public            System.String                   appCode  // 0x38
METHODS:
  RVA=0x041E1670  token=0x60001E6  System.Void .ctor()
  RVA=0x09DEDBA0  token=0x60001E7  System.Void .ctor(System.String jsonData)
  RVA=0x09DEDA3C  token=0x60001E8  System.String ToString()
END_CLASS

CLASS: U8.SDK.ISDKPromise
TYPE:  interface
TOKEN: 0x200006A
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001E9  System.Void Fulfill(System.Object param)
  RVA=-1  // abstract  token=0x60001EA  System.Void Reject(System.Object reason)
END_CLASS

CLASS: U8.SDK.SDKPromise`1
TYPE:  class
TOKEN: 0x200006B
IMPLEMENTS: U8.SDK.ISDKPromise
FIELDS:
  public            System.Action<Param>            onFulfilled  // 0x0
  public            System.Action<System.Object>    onRejected  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001EB  System.Void Fulfill(System.Object param)
  RVA=-1  // not resolved  token=0x60001EC  System.Void Reject(System.Object reason)
  RVA=-1  // not resolved  token=0x60001ED  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.SDKPromiseWithResult`1
TYPE:  class
TOKEN: 0x200006C
EXTENDS: U8.SDK.SDKPromise`1
FIELDS:
  private           Param                           <result>k__BackingField  // 0x0
  private           System.Object                   <rejectInfo>k__BackingField  // 0x0
PROPERTIES:
  result  get=-1  // not resolved  set=-1  // not resolved
  rejectInfo  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001F2  System.Void Fulfill(System.Object param)
  RVA=-1  // not resolved  token=0x60001F3  System.Void Reject(System.Object reason)
  RVA=-1  // not resolved  token=0x60001F4  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.SDKPromiseEnumerator`1
TYPE:  class
TOKEN: 0x200006D
FIELDS:
  private           U8.SDK.SDKPromiseEnumerator.State<T>m_state  // 0x0
  private           T                               <result>k__BackingField  // 0x0
  private           System.Object                   <reject>k__BackingField  // 0x0
PROPERTIES:
  isFulfilled  get=-1  // not resolved
  result  get=-1  // not resolved  set=-1  // not resolved
  reject  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001FA  System.Collections.IEnumerator Yield()
  RVA=-1  // not resolved  token=0x60001FB  System.Void .ctor(U8.SDK.SDKPromise<T> promise)
  RVA=-1  // not resolved  token=0x60001FC  System.Void _OnFulfilled(T result)
  RVA=-1  // not resolved  token=0x60001FD  System.Void _OnRejected(System.Object rejectInfo)
END_CLASS

CLASS: U8.SDK.SDKPromiseWrapper
TYPE:  class
TOKEN: 0x2000070
SIZE:  0x18
FIELDS:
  private           U8.SDK.ISDKPromise              m_promise  // 0x10
METHODS:
  RVA=-1  // generic def  token=0x6000204  T EnsurePromise()
  RVA=0x049DAAB0  token=0x6000205  System.Void Fulfill(System.Object param)
  RVA=0x09DEDEE4  token=0x6000206  System.Void Reject(System.Object reason)
  RVA=0x040E20A0  token=0x6000207  System.Void _Clear()
  RVA=0x041E1670  token=0x6000208  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.SDKPromiseExtensions
TYPE:  static class
TOKEN: 0x2000071
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DEDECC  token=0x6000209  System.Void RejectWithString(U8.SDK.ISDKPromise promise, System.String errorInfo)
  RVA=-1  // generic def  token=0x600020A  System.Void FulfillWithType(U8.SDK.SDKPromise<T> promise, T param)
END_CLASS

CLASS: U8.SDK.U8SDKTickEvent
TYPE:  class
TOKEN: 0x2000072
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x03F31120  token=0x600020B  System.Void Update()
  RVA=0x03F31120  token=0x600020C  System.Void HGU8SDKTickEvent()
  RVA=0x05393520  token=0x600020D  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8TickEventManager
TYPE:  class
TOKEN: 0x2000073
SIZE:  0x20
FIELDS:
  private           UnityEngine.GameObject          tickGameObject  // 0x10
  private           U8.SDK.U8SDKTickEvent           tickEvent  // 0x18
  private   static  U8.SDK.U8TickEventManager       instanceManager  // static @ 0x0
  private   static readonly System.Object                   lockObject  // static @ 0x8
PROPERTIES:
  InstanceManager  get=0x0488EB90
METHODS:
  RVA=0x0488ECC0  token=0x600020F  System.Void InitTickEvent()
  RVA=0x09DF6094  token=0x6000210  System.Void UnInitTickEvent()
  RVA=0x041E1670  token=0x6000211  System.Void .ctor()
  RVA=0x04D30310  token=0x6000212  System.Void .cctor()
END_CLASS

CLASS: U8.SDK.U8LoginV2Result
TYPE:  class
TOKEN: 0x2000074
SIZE:  0x28
FIELDS:
  public            System.String                   token  // 0x10
  public            System.String                   uid  // 0x18
  public            System.Boolean                  isNew  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000213  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8AuthV2Result
TYPE:  class
TOKEN: 0x2000075
SIZE:  0x28
FIELDS:
  public            System.String                   uid  // 0x10
  public            System.String                   oauth2Code  // 0x18
  public            System.String                   oauth2Token  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000214  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8GrantResult
TYPE:  class
TOKEN: 0x2000076
SIZE:  0x28
FIELDS:
  public            System.String                   uid  // 0x10
  public            System.String                   code  // 0x18
  public            System.String                   token  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000215  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8SDKCallback
TYPE:  class
TOKEN: 0x2000077
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  U8.SDK.U8SDKCallback            m_instance  // static @ 0x0
  private   static  System.Object                   m_lock  // static @ 0x8
METHODS:
  RVA=0x03A84E20  token=0x6000216  U8.SDK.U8SDKCallback InitCallback()
  RVA=0x049DA9B0  token=0x6000217  System.Void OnInitSuc(System.String extConfigs)
  RVA=0x09DF4908  token=0x6000218  System.Void OnInitFail(System.String info)
  RVA=0x049DAA30  token=0x6000219  System.Void OnLoginSuc(System.String extension)
  RVA=0x09DF4960  token=0x600021A  System.Void OnLoginFail(System.String info)
  RVA=0x09DF49B8  token=0x600021B  System.Void OnLogout(System.String info)
  RVA=0x09DF4B24  token=0x600021C  System.Void OnSwitchAccount()
  RVA=0x09DF4A5C  token=0x600021D  System.Void OnPaySuc(System.String jsonData)
  RVA=0x09DF4A04  token=0x600021E  System.Void OnPayFail(System.String failMsg)
  RVA=0x09DF4AB4  token=0x600021F  System.Void OnSDKError(System.String jsonData)
  RVA=0x035A5E80  token=0x6000220  System.Void OnExtraInfo(System.String jsonData)
  RVA=0x09DF4B70  token=0x6000221  System.Collections.IEnumerator _onLogoutNextFrameCoroutine()
  RVA=-1  // generic def  token=0x6000222  T GetValueSafe(System.Collections.Generic.Dictionary<System.String,System.Object> dict, System.String key, T defVal)
  RVA=0x05393520  token=0x6000223  System.Void .ctor()
  RVA=0x04D30380  token=0x6000224  System.Void .cctor()
END_CLASS

CLASS: U8.SDK.OneChannelSDKVersion
TYPE:  static class
TOKEN: 0x2000079
SIZE:  0x10
FIELDS:
  public    static readonly System.String                   VERSION  // static @ 0x0
METHODS:
  RVA=0x04D4FF60  token=0x600022B  System.Void .cctor()
END_CLASS

CLASS: U8.SDK.GameType
TYPE:  sealed struct
TOKEN: 0x200007A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  U8.SDK.GameType                 Local  // const
  public    static  U8.SDK.GameType                 Cloud  // const
METHODS:
END_CLASS

CLASS: U8.SDK.CloudGameUtil
TYPE:  class
TOKEN: 0x200007B
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    m_thinClientPlatform  // static @ 0x0
  private   static  System.String                   m_thinClientDeviceProperties  // static @ 0x8
PROPERTIES:
  ThinClientPlatform  get=0x09DE4854  set=0x09DE4904
  ThinClientDeviceProperties  get=0x09DE4804  set=0x09DE48A4
METHODS:
  RVA=0x040BC9E0  token=0x6000230  System.Boolean IsCloudGame()
  RVA=0x04D4FB30  token=0x6000231  System.Void .cctor()
END_CLASS

CLASS: U8.SDK.ImageSaver
TYPE:  class
TOKEN: 0x200007C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09DE6094  token=0x6000232  System.Threading.Tasks.Task SaveImage(System.String paramJson)
  RVA=0x09DE5E88  token=0x6000233  System.Threading.Tasks.Task<System.Boolean> SaveCloudGameImage(System.String paramJson)
  RVA=0x09DE687C  token=0x6000234  System.Boolean ValidateInput(System.String paramJson, U8.SDK.ImageSaver.ImageShareInfo& shareInfo)
  RVA=0x09DE5C58  token=0x6000235  System.String NormalizeSourcePath(System.String imgPath)
  RVA=0x09DE5DCC  token=0x6000236  U8.SDK.ImageSaver.FolderType ParseFolderType(System.Int32 folderType)
  RVA=0x09DE5324  token=0x6000237  System.String BuildTargetPath(System.String sourcePath, System.String relativePath, System.String baseFolder)
  RVA=0x09DE5878  token=0x6000238  System.Boolean IsDirectoryPath(System.String path)
  RVA=0x09DE51F8  token=0x6000239  System.String BuildPathForDirectory(System.String sourcePath, System.String relativeDirPath, System.String baseFolder)
  RVA=0x09DE5F74  token=0x600023A  System.Threading.Tasks.Task<System.Boolean> SaveImageToPathAsync(System.String sourcePath, System.String targetPath, System.String imgFolder)
  RVA=0x09DE592C  token=0x600023B  System.Boolean IsSamePath(System.String sourcePath, System.String targetPath)
  RVA=0x09DE6980  token=0x600023C  System.Boolean ValidateMove(System.String sourcePath, System.String targetPath, System.String rootDir, System.String& error)
  RVA=0x09DE5450  token=0x600023D  System.Void EnsureDirectoryExists(System.String filePath)
  RVA=0x09DE5A00  token=0x600023E  System.Boolean MoveFile(System.String sourcePath, System.String targetPath)
  RVA=0x09DE5664  token=0x600023F  System.String GetImageSaveFolder(U8.SDK.ImageSaver.FolderType folderType)
  RVA=0x09DE5718  token=0x6000240  System.String GetWindowsPicturePath()
  RVA=0x09DE54B8  token=0x6000241  System.String GetGameInstalledFolder()
  RVA=0x09DE6788  token=0x6000242  System.Void SendSuccessResponse(System.String targetPath)
  RVA=0x09DE6164  token=0x6000243  System.Void SendErrorResponse(U8.SDK.ImageSaver.ErrorCode errorCode)
  RVA=0x09DE6314  token=0x6000244  System.Void SendResponse(U8.SDK.ImageSaver.CallbackResult code, System.Collections.Generic.Dictionary<System.String,System.Object> extraMsg)
  RVA=0x09DE623C  token=0x6000245  System.Void SendMessageInMain(System.String methodName, System.String paramValue)
  RVA=0x041E1670  token=0x6000246  System.Void .ctor()
END_CLASS

CLASS: U8.SDK.U8MainThreadDispatcher
TYPE:  class
TOKEN: 0x2000086
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static readonly System.Collections.Concurrent.ConcurrentQueue<System.Action>_queue  // static @ 0x0
  private   static  U8.SDK.U8MainThreadDispatcher   _instance  // static @ 0x8
  private   static  System.Int32                    _initialized  // static @ 0x10
METHODS:
  RVA=0x04B46C10  token=0x6000252  System.Void Initialize()
  RVA=0x09DF3AF0  token=0x6000253  System.Void RunOnMainThread(System.Action action)
  RVA=0x03B643C0  token=0x6000254  System.Void Update()
  RVA=0x05393520  token=0x6000255  System.Void .ctor()
  RVA=0x04A1D370  token=0x6000256  System.Void .cctor()
END_CLASS

CLASS: U8.SDK.MiniJSON.Json
TYPE:  static class
TOKEN: 0x2000087
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x035A6450  token=0x6000257  System.Object Deserialize(System.String json)
  RVA=0x05395194  token=0x6000258  System.String Serialize(System.Object obj)
END_CLASS

