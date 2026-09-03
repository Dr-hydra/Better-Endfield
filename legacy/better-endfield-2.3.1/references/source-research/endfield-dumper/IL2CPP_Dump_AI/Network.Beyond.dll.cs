// ========================================================
// Dumped by @desirepro
// Assembly: Network.Beyond.dll
// Classes:  107
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

CLASS: HandlerEntry
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x18
FIELDS:
  public            Beyond.Network.NetResponseHandlerhandler  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600007D  System.Void .ctor()
END_CLASS

CLASS: ResultHandler
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x40
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Boolean                  m_ignoreSeq  // 0x10
  private           Beyond.ListDict<Proto.SCMessageID,System.Object>m_responseTokens  // 0x18
  private           Beyond.ListDict<Proto.SCMessageID,Beyond.Network.NetResponseHandler>m_responseHandlers  // 0x20
  private           System.Boolean                  <isDisposed>k__BackingField  // 0x28
  private           System.UInt64                   <cachedSeqNum>k__BackingField  // 0x30
  private           System.Int32                    <sessionID>k__BackingField  // 0x38
PROPERTIES:
  isDisposed  get=0x011F33C0  set=0x011F33D0
  cachedSeqNum  get=0x011F36E0  set=0x011F36F0
  sessionID  get=0x04D863B0  set=0x04D86430
METHODS:
  RVA=0x0A898E20  token=0x6000084  System.Void SetIgnoreSeq(System.Boolean ignoreSeq)
  RVA=0x03A4F4A0  token=0x6000085  Beyond.Network.NetBus.ResultHandler OnResponse(Proto.SCMessageID msgId, Beyond.Network.NetResponseHandler handler)
  RVA=0x03A4EB80  token=0x6000086  System.Void Dispose()
  RVA=0x03A4E950  token=0x6000087  System.Boolean _CheckSessionIDValid()
  RVA=0x03A4E7E0  token=0x6000088  System.Void _HandleMessage(Beyond.Network.NetResponse& msg)
  RVA=0x041E1670  token=0x6000089  System.Void .ctor()
END_CLASS

CLASS: Sender
TYPE:  abstract class
TOKEN: 0x2000017
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600008A  Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg)
  RVA=-1  // abstract  token=0x600008B  System.Void OnNetShutDown()
  RVA=0x03AC46C0  token=0x600008C  Beyond.Network.NetBus.ResultHandler DoSend(Google.Protobuf.IMessage message)
  RVA=0x0A898E74  token=0x600008D  System.Void DoListen(Proto.SCMessageID msgId, Beyond.Network.NetResponseHandler handler)
  RVA=0x0A898F14  token=0x600008E  System.Void DoUnListen(Proto.SCMessageID msgId, Beyond.Network.NetResponseHandler handler)
  RVA=0x041E1670  token=0x600008F  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x200001D
SIZE:  0x20
FIELDS:
  public            System.Int32                    key  // 0x10
  public            Beyond.Network.UIBusSender      <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60000B3  System.Void .ctor()
  RVA=0x0A899A04  token=0x60000B4  System.Void <Send>b__0(Beyond.Network.NetResponse& resp)
  RVA=0x0A899AF0  token=0x60000B5  System.Void <Send>b__1(Beyond.Network.NetResponse& resp)
  RVA=0x0A899C4C  token=0x60000B6  System.Void <Send>b__2(Beyond.Network.NetResponse& resp)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x200001F
SIZE:  0x20
FIELDS:
  public            System.Int32                    key  // 0x10
  public            Beyond.Network.TeleportMaskSender<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60000BD  System.Void .ctor()
  RVA=0x0A899870  token=0x60000BE  System.Void <Send>b__0(Beyond.Network.NetResponse& resp)
  RVA=0x03A4CE00  token=0x60000BF  System.Void <Send>b__1(Beyond.Network.NetResponse& resp)
  RVA=0x0A8998EC  token=0x60000C0  System.Void <Send>b__2(Beyond.Network.NetResponse& resp)
  RVA=0x0A899968  token=0x60000C1  System.Void <Send>b__3(Beyond.Network.NetResponse& resp)
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x2000021
SIZE:  0x18
FIELDS:
  public            System.Action<System.Int32>     onError  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000C9  System.Void .ctor()
  RVA=0x0A899A80  token=0x60000CA  System.Void <Send>b__0(Beyond.Network.NetResponse& errorMsg)
  RVA=0x0A899B6C  token=0x60000CB  System.Void <Send>b__1(Beyond.Network.NetResponse& errorMsg)
  RVA=0x0A899BDC  token=0x60000CC  System.Void <Send>b__2(Beyond.Network.NetResponse& errorMsg)
END_CLASS

CLASS: Config
TYPE:  class
TOKEN: 0x200002A
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.Dictionary<System.Int32,Proto.SCMessageID>messages  // 0x10
METHODS:
  RVA=0x0A89341C  token=0x60000D7  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass18_0
TYPE:  sealed class
TOKEN: 0x200002F
SIZE:  0x18
FIELDS:
  public            System.Object                   target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000E6  System.Void .ctor()
  RVA=0x03895110  token=0x60000E7  System.Void <AutoRegisterMessageHandlers>b__0(System.Type msgType, System.Reflection.MethodInfo methodInfo)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000030
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.Network.NetUtil.<>c      <>9  // static @ 0x0
  public    static  System.Action<System.Type,System.Reflection.MethodInfo><>9__19_0  // static @ 0x8
METHODS:
  RVA=0x0A899CC8  token=0x60000E8  System.Void .cctor()
  RVA=0x041E1670  token=0x60000E9  System.Void .ctor()
  RVA=0x0A899824  token=0x60000EA  System.Void <AutoUnRegisterMessageHandlers>b__19_0(System.Type msgType, System.Reflection.MethodInfo methodInfo)
END_CLASS

CLASS: NetworkFrequency
TYPE:  class
TOKEN: 0x2000032
SIZE:  0x20
FIELDS:
  public            System.Type                     type  // 0x10
  public            System.Single                   second  // 0x18
  public            System.Int32                    maxCount  // 0x1c
METHODS:
  RVA=0x041E1670  token=0x60000EC  System.Void .ctor()
END_CLASS

CLASS: MaskInfo
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x30
FIELDS:
  public            System.Int32                    key  // 0x10
  public            System.String                   name  // 0x18
  public            System.Single                   startTime  // 0x20
  public            System.Single                   delayTime  // 0x24
  public            System.Single                   timeoutTime  // 0x28
PROPERTIES:
  endTime  get=0x03A4CDD0
  showDecoTime  get=0x03A4CDA0
METHODS:
  RVA=0x04DA5BB0  token=0x6000103  System.Void .ctor()
END_CLASS

CLASS: Config
TYPE:  class
TOKEN: 0x2000036
SIZE:  0x20
FIELDS:
  public            System.String                   key  // 0x10
  public            Beyond.SerializeFieldDictionaryPaired<Proto.CSMessageID,Proto.SCMessageID>messages  // 0x18
METHODS:
  RVA=0x04B30D40  token=0x6000105  System.Void .ctor()
END_CLASS

CLASS: EUrlType
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Network.NetworkUrls.EUrlTypeServerList  // const
  public    static  Beyond.Network.NetworkUrls.EUrlTypeGameLicense  // const
  public    static  Beyond.Network.NetworkUrls.EUrlTypeAgeTips  // const
  public    static  Beyond.Network.NetworkUrls.EUrlTypeGateBulletin  // const
  public    static  Beyond.Network.NetworkUrls.EUrlTypeGameBulletin  // const
  public    static  Beyond.Network.NetworkUrls.EUrlTypeLauncher  // const
  public    static  Beyond.Network.NetworkUrls.EUrlTypeGov  // const
METHODS:
END_CLASS

CLASS: WeakNetDelayConfig
TYPE:  sealed struct
TOKEN: 0x200003A
SIZE:  0x20
FIELDS:
  public            System.Boolean                  enabled  // 0x10
  public            System.Int32                    minDelayMs  // 0x14
  public            System.Int32                    maxDelayMs  // 0x18
  public            System.Int32                    maxTriggerCountPerSecond  // 0x1c
METHODS:
END_CLASS

CLASS: ErrorType
TYPE:  sealed struct
TOKEN: 0x200003C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Network.HttpSender.ErrorTypeNONE  // const
  public    static  Beyond.Network.HttpSender.ErrorTypeTIMEOUT  // const
  public    static  Beyond.Network.HttpSender.ErrorTypeDNS_FAILED  // const
  public    static  Beyond.Network.HttpSender.ErrorTypeCANCELED  // const
  public    static  Beyond.Network.HttpSender.ErrorTypeINTERNAL  // const
METHODS:
END_CLASS

CLASS: Cancellation
TYPE:  class
TOKEN: 0x200003D
SIZE:  0x20
FIELDS:
  private           System.Boolean                  m_isCancelled  // 0x10
  private           System.Action                   OnCancelInvoked  // 0x18
EVENTS:
  OnCancelInvoked  add=add_OnCancelInvoked  remove=remove_OnCancelInvoked
METHODS:
  RVA=0x0A893240  token=0x6000148  System.Void Cancel()
  RVA=0x0A8932A0  token=0x6000149  System.Boolean IsCanceled()
  RVA=0x041E1670  token=0x600014A  System.Void .ctor()
END_CLASS

CLASS: FGetOptions
TYPE:  sealed struct
TOKEN: 0x200003E
SIZE:  0x30
FIELDS:
  public    static readonly Beyond.Network.HttpSender.FGetOptionsDEFAULT  // static @ 0x0
  public            System.Collections.Generic.Dictionary<System.String,System.String>headers  // 0x10
  public            System.String                   contentType  // 0x18
  public            System.Boolean                  headOnly  // 0x20
  public            Beyond.Network.HttpSender.Cancellationcancellation  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600014B  System.Void .cctor()
END_CLASS

CLASS: FPostOptions
TYPE:  sealed struct
TOKEN: 0x200003F
SIZE:  0x30
FIELDS:
  public    static readonly Beyond.Network.HttpSender.FPostOptionsDEFAULT  // static @ 0x0
  public            System.Collections.Generic.Dictionary<System.String,System.String>headers  // 0x10
  public            System.String                   textBody  // 0x18
  public            System.String                   contentType  // 0x20
  public            Beyond.Network.HttpSender.Cancellationcancellation  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600014C  System.Void .cctor()
END_CLASS

CLASS: FSendFunc
TYPE:  sealed struct
TOKEN: 0x2000040
SIZE:  0x68
FIELDS:
  public            System.String                   url  // 0x10
  public            System.Int32                    opt  // 0x18
  public            Beyond.Network.HttpSender.FGetOptionsoptionsGET  // 0x20
  public            Beyond.Network.HttpSender.FPostOptionsoptionsPOST  // 0x40
  public            System.Boolean                  editorOnly  // 0x60
METHODS:
  RVA=0x03E37380  token=0x600014D  UnityEngine.Networking.UnityWebRequest Invoke()
  RVA=0x03E37330  token=0x600014E  Beyond.Network.HttpSender.Cancellation GetCancellation()
  RVA=0x0A8934E8  token=0x600014F  System.String GetOptMethod()
END_CLASS

CLASS: Result
TYPE:  class
TOKEN: 0x2000041
SIZE:  0x48
FIELDS:
  public            System.Int32                    respCode  // 0x10
  public            Beyond.Network.HttpSender.ErrorTypeerrorType  // 0x14
  public            System.Boolean                  isError  // 0x18
  public            System.Exception                exception  // 0x20
  public            System.String                   respText  // 0x28
  public            System.Byte[]                   respData  // 0x30
  public            System.String                   respError  // 0x38
  public            System.Boolean                  isServiceDone  // 0x40
METHODS:
  RVA=0x041E1670  token=0x6000150  System.Void .ctor()
END_CLASS

CLASS: TypedResult
TYPE:  class
TOKEN: 0x2000042
SIZE:  0x60
EXTENDS: Result
FIELDS:
  private           System.Type                     <respType>k__BackingField  // 0x48
  public            System.Object                   respObject  // 0x50
  public            System.Boolean                  isDeserializeFailed  // 0x58
PROPERTIES:
  respType  get=0x04D86200  set=0x035A41A0
METHODS:
  RVA=0x035A41A0  token=0x6000153  System.Void .ctor(System.Type respType)
END_CLASS

CLASS: Result`1
TYPE:  class
TOKEN: 0x2000043
EXTENDS: TypedResult
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000154  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000155  TResp GetRespObject()
END_CLASS

CLASS: <YieldSendGet>d__16
TYPE:  sealed class
TOKEN: 0x2000044
SIZE:  0x50
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.String                   url  // 0x20
  public            Beyond.Network.HttpSender.FGetOptionsoptions  // 0x28
  public            Beyond.Misc.ObjectRef           resultRef  // 0x48
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000156  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000157  System.Void System.IDisposable.Dispose()
  RVA=0x03C07E90  token=0x6000158  System.Boolean MoveNext()
  RVA=0x0A899D2C  token=0x600015A  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <YieldSendGet>d__17`1
TYPE:  sealed class
TOKEN: 0x2000045
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           System.Object                   <>2__current  // 0x0
  public            System.String                   url  // 0x0
  public            Beyond.Network.HttpSender.FGetOptionsoptions  // 0x0
  public            Beyond.Misc.ObjectRef           resultRef  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600015C  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x600015D  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x600015E  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000160  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <YieldSendGet>d__18
TYPE:  sealed class
TOKEN: 0x2000046
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Network.HttpSender.FGetOptionsoptions  // 0x20
  public            System.String                   url  // 0x40
  public            Beyond.Misc.ObjectRef           resultRef  // 0x48
  public            System.Type                     respType  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000162  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000163  System.Void System.IDisposable.Dispose()
  RVA=0x046BACA0  token=0x6000164  System.Boolean MoveNext()
  RVA=0x0A899D78  token=0x6000166  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <YieldSendPost>d__19
TYPE:  sealed class
TOKEN: 0x2000047
SIZE:  0x50
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.String                   url  // 0x20
  public            Beyond.Network.HttpSender.FPostOptionsoptions  // 0x28
  public            Beyond.Misc.ObjectRef           resultRef  // 0x48
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000168  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000169  System.Void System.IDisposable.Dispose()
  RVA=0x0A899DC4  token=0x600016A  System.Boolean MoveNext()
  RVA=0x0A899E2C  token=0x600016C  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <YieldSendPost>d__20`1
TYPE:  sealed class
TOKEN: 0x2000048
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           System.Object                   <>2__current  // 0x0
  public            System.String                   url  // 0x0
  public            Beyond.Network.HttpSender.FPostOptionsoptions  // 0x0
  public            Beyond.Misc.ObjectRef           resultRef  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600016E  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x600016F  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000170  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000172  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <YieldSendPost>d__21
TYPE:  sealed class
TOKEN: 0x2000049
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Network.HttpSender.FPostOptionsoptions  // 0x20
  public            System.String                   url  // 0x40
  public            Beyond.Misc.ObjectRef           resultRef  // 0x48
  public            System.Type                     respType  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000174  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000175  System.Void System.IDisposable.Dispose()
  RVA=0x046BABB0  token=0x6000176  System.Boolean MoveNext()
  RVA=0x0A899E78  token=0x6000178  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_YieldSendHttpRequest>d__28
TYPE:  sealed class
TOKEN: 0x200004A
SIZE:  0xA0
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Network.HttpSender.FSendFuncsendFunc  // 0x20
  public            Beyond.Network.HttpSender.Resultresult  // 0x78
  private           Beyond.Network.HttpSender.Cancellation<cancelation>5__2  // 0x80
  private           System.Boolean                  <isCanceled>5__3  // 0x88
  private           UnityEngine.Networking.UnityWebRequest<request>5__4  // 0x90
  private           UnityEngine.Networking.UnityWebRequestAsyncOperation<sendOpt>5__5  // 0x98
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600017A  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0A899F10  token=0x600017B  System.Void System.IDisposable.Dispose()
  RVA=0x03E363C0  token=0x600017C  System.Boolean MoveNext()
  RVA=0x03E37400  token=0x600017D  System.Void <>m__Finally1()
  RVA=0x0A899EC4  token=0x600017F  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_YieldSendHttpRequest>d__29
TYPE:  sealed class
TOKEN: 0x200004B
SIZE:  0x90
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Misc.ObjectRef           resultRef  // 0x20
  public            System.Type                     respType  // 0x28
  public            Beyond.Network.HttpSender.FSendFuncsendFunc  // 0x30
  private           Beyond.Network.HttpSender.Result<result>5__2  // 0x88
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000181  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000182  System.Void System.IDisposable.Dispose()
  RVA=0x035A3EF0  token=0x6000183  System.Boolean MoveNext()
  RVA=0x0A899F88  token=0x6000185  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass30_0
TYPE:  sealed class
TOKEN: 0x200004C
SIZE:  0x18
FIELDS:
  public            Beyond.Network.HttpSender.Resultresult  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000187  System.Void .ctor()
  RVA=0x0A8999E4  token=0x6000188  Beyond.Network.HttpSender.Result <_WaitForSendHttpRequest>b__0()
END_CLASS

CLASS: <>c__DisplayClass31_0`1
TYPE:  sealed class
TOKEN: 0x200004D
FIELDS:
  public            Beyond.Network.HttpSender.Result<TResp>result  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000189  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600018A  Beyond.Network.HttpSender.Result<TResp> <_WaitForSendHttpRequest>b__0()
END_CLASS

CLASS: <GetEnumerator>d__18
TYPE:  sealed class
TOKEN: 0x200004F
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           T                               <>2__current  // 0x0
  public            Beyond.Network.CircularBuffer<T><>4__this  // 0x0
  private           System.ArraySegment<T>[]        <>7__wrap1  // 0x0
  private           System.Int32                    <>7__wrap2  // 0x0
  private           System.ArraySegment<T>          <segment>5__4  // 0x0
  private           System.Int32                    <i>5__5  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<T>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001A7  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x60001A8  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x60001A9  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60001AB  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass64_0
TYPE:  sealed class
TOKEN: 0x2000051
SIZE:  0x20
FIELDS:
  public            Beyond.Network.HGNetBaseSession <>4__this  // 0x10
  public            Beyond.Network.INetIO           tcpIO  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001E8  System.Void .ctor()
  RVA=0x0480FC50  token=0x60001E9  System.Void <ConnectAsync>b__0(System.Threading.Tasks.Task<System.Threading.Tasks.Task> task)
END_CLASS

CLASS: <>c__DisplayClass77_0
TYPE:  sealed class
TOKEN: 0x2000052
SIZE:  0x28
FIELDS:
  public            Beyond.Network.HGNetBaseSession <>4__this  // 0x10
  public            Beyond.Network.INetIO           tcp  // 0x18
  public            System.Int32                    nextid  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60001EA  System.Void .ctor()
  RVA=0x048AC9A0  token=0x60001EB  System.Threading.Tasks.Task <StartTCPMsgLoopThreadTask>b__0()
END_CLASS

CLASS: <_SessionThread>d__79
TYPE:  sealed struct
TOKEN: 0x2000053
SIZE:  0x50
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>t__builder  // 0x18
  public            Beyond.Network.HGNetBaseSession <>4__this  // 0x30
  public            Beyond.Network.INetIO           tcp  // 0x38
  public            System.Int32                    threadID  // 0x40
  private           System.Runtime.CompilerServices.TaskAwaiter<>u__1  // 0x48
METHODS:
  RVA=0x0314B1D0  token=0x60001EC  System.Void MoveNext()
  RVA=0x0459ADF0  token=0x60001ED  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <_DoReconnectAsync>d__89
TYPE:  sealed struct
TOKEN: 0x2000054
SIZE:  0x48
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>t__builder  // 0x18
  public            Beyond.Network.HGNetBaseSession <>4__this  // 0x30
  public            Beyond.Network.INetIO           tcpIO  // 0x38
  private           System.Runtime.CompilerServices.TaskAwaiter<System.Boolean><>u__1  // 0x40
METHODS:
  RVA=0x03813060  token=0x60001EE  System.Void MoveNext()
  RVA=0x0459AE50  token=0x60001EF  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <>c__DisplayClass90_0
TYPE:  sealed class
TOKEN: 0x2000055
SIZE:  0x30
FIELDS:
  public            Beyond.Network.INetIO           tcpIO  // 0x10
  public            System.String                   ip  // 0x18
  public            System.Int32                    port  // 0x20
  public            System.Int32                    sendTimeout  // 0x24
  public            System.Int32                    receiveBufferLen  // 0x28
METHODS:
  RVA=0x041E1670  token=0x60001F0  System.Void .ctor()
  RVA=0x040EC030  token=0x60001F1  System.Void <TryConnectWithTimeout>b__0()
END_CLASS

CLASS: <TryConnectWithTimeout>d__90
TYPE:  sealed struct
TOKEN: 0x2000056
SIZE:  0x68
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Boolean><>t__builder  // 0x18
  public            Beyond.Network.INetIO           tcpIO  // 0x30
  public            System.String                   ip  // 0x38
  public            System.Int32                    port  // 0x40
  public            System.Int32                    sendTimeout  // 0x44
  public            System.Int32                    receiveBufferLen  // 0x48
  public            System.Int32                    timeoutMilliseconds  // 0x4c
  public            Beyond.Network.HGNetBaseSession <>4__this  // 0x50
  private           System.Threading.Tasks.Task     <connectTask>5__2  // 0x58
  private           System.Runtime.CompilerServices.TaskAwaiter<System.Threading.Tasks.Task><>u__1  // 0x60
METHODS:
  RVA=0x038EBD80  token=0x60001F2  System.Void MoveNext()
  RVA=0x048BB330  token=0x60001F3  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <>c__DisplayClass91_0
TYPE:  sealed class
TOKEN: 0x2000057
SIZE:  0x18
FIELDS:
  public            Beyond.Network.INetIO           tcpIO  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001F4  System.Void .ctor()
  RVA=0x03994490  token=0x60001F5  System.Void <TryCloseTcpIOWithTimeout>b__0()
END_CLASS

CLASS: <TryCloseTcpIOWithTimeout>d__91
TYPE:  sealed struct
TOKEN: 0x2000058
SIZE:  0x58
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Boolean><>t__builder  // 0x18
  public            Beyond.Network.INetIO           tcpIO  // 0x30
  public            System.Int32                    timeoutMilliseconds  // 0x38
  public            Beyond.Network.HGNetBaseSession <>4__this  // 0x40
  private           System.Threading.Tasks.Task     <closeTask>5__2  // 0x48
  private           System.Runtime.CompilerServices.TaskAwaiter<System.Threading.Tasks.Task><>u__1  // 0x50
METHODS:
  RVA=0x038EC0B0  token=0x60001F6  System.Void MoveNext()
  RVA=0x048BB3A0  token=0x60001F7  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: LoginErrorInfo
TYPE:  class
TOKEN: 0x2000061
SIZE:  0x20
FIELDS:
  private           System.Int32                    <code>k__BackingField  // 0x10
  private           System.String                   <details>k__BackingField  // 0x18
PROPERTIES:
  code  get=0x02B2E2D0  set=0x014F51F0
  details  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x03A4B4A0  token=0x600023E  System.Void SetErrorInvaild()
  RVA=0x0A8A5BBC  token=0x600023F  System.Void SetErrorConnectError()
  RVA=0x0A8A5C48  token=0x6000240  System.Void SetErrorConnectTimeOut()
  RVA=0x03A4AE80  token=0x6000241  System.Void Clear()
  RVA=0x0A8A5CD4  token=0x6000242  System.Void Set(System.Int32 code, System.String details)
  RVA=0x03A4B450  token=0x6000243  System.Void .ctor()
END_CLASS

CLASS: Beyond.Network.DoubleQueue`1
TYPE:  class
TOKEN: 0x2000008
FIELDS:
  private           System.Collections.Generic.Queue<T>m_readQueue  // 0x0
  private           System.Collections.Generic.Queue<T>m_writeQueue  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000007  System.Void LockingSwap()
  RVA=-1  // not resolved  token=0x6000008  System.Void TryLockingSwap()
  RVA=-1  // not resolved  token=0x6000009  System.Void Enqueue(T item)
  RVA=-1  // not resolved  token=0x600000A  System.Boolean TryDequeue(T& item)
  RVA=-1  // not resolved  token=0x600000B  System.Void EnqueueRange(System.Collections.Generic.List<T> list)
  RVA=-1  // not resolved  token=0x600000C  System.Void .ctor()
END_CLASS

CLASS: Beyond.Network.ENetWeakNetPreset
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Network.ENetWeakNetPresetDisabled  // const
  public    static  Beyond.Network.ENetWeakNetPresetLightJitter  // const
  public    static  Beyond.Network.ENetWeakNetPresetNormalWifiLag  // const
  public    static  Beyond.Network.ENetWeakNetPresetPoorMobileNetwork  // const
  public    static  Beyond.Network.ENetWeakNetPresetSevereLag  // const
METHODS:
END_CLASS

CLASS: Beyond.Network.HGTcpClient
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Net.Sockets.Socket       m_ClientSocket  // 0x10
  private           System.Boolean                  m_Active  // 0x18
  private           System.Net.Sockets.NetworkStreamm_DataStream  // 0x20
  private           System.Net.Sockets.AddressFamilym_Family  // 0x28
  private           System.Boolean                  m_CleanedUp  // 0x2c
PROPERTIES:
  Client  get=0x031442C0  set=0x040EC940
  Active  get=0x0A894B1C  set=0x0A894D88
  Available  get=0x0314B080
  Connected  get=0x0314AFE0
  ExclusiveAddressUse  get=0x0A894B64  set=0x0A894DDC
  ReceiveBufferSize  get=0x0A894C18  set=0x040EC980
  SendBufferSize  get=0x0A894CD0  set=0x040EC9F0
  ReceiveTimeout  get=0x0A894C74  set=0x0A894EC0
  SendTimeout  get=0x0A894D2C  set=0x040ECA60
  LingerState  get=0x040EC260  set=0x0A894E44
  NoDelay  get=0x0A894BB8  set=0x040ECAD0
METHODS:
  RVA=0x040EC880  token=0x600000D  System.Void .ctor()
  RVA=0x0A89498C  token=0x600000E  System.Void .ctor(System.String hostname, System.Int32 port)
  RVA=0x0A894AF8  token=0x600000F  System.Void .ctor(System.Net.Sockets.Socket acceptedSocket)
  RVA=0x040ED980  token=0x6000018  System.Void Connect(System.String hostname, System.Int32 port)
  RVA=0x0A8946DC  token=0x6000019  System.Void Connect(System.Net.IPAddress address, System.Int32 port)
  RVA=0x040EE780  token=0x600001A  System.Void Connect(System.Net.IPEndPoint remoteEP)
  RVA=0x0A894654  token=0x600001B  System.Void Connect(System.Net.IPAddress[] ipAddresses, System.Int32 port)
  RVA=0x040F20A0  token=0x600001C  System.IAsyncResult BeginConnect(System.String host, System.Int32 port, System.AsyncCallback requestCallback, System.Object state)
  RVA=0x0A8942D4  token=0x600001D  System.IAsyncResult BeginConnect(System.Net.IPAddress address, System.Int32 port, System.AsyncCallback requestCallback, System.Object state)
  RVA=0x0A894224  token=0x600001E  System.IAsyncResult BeginConnect(System.Net.IPAddress[] addresses, System.Int32 port, System.AsyncCallback requestCallback, System.Object state)
  RVA=0x0481CC90  token=0x600001F  System.Void EndConnect(System.IAsyncResult asyncResult)
  RVA=0x0A894384  token=0x6000020  System.Threading.Tasks.Task ConnectAsync(System.Net.IPAddress address, System.Int32 port)
  RVA=0x040EBC70  token=0x6000021  System.Threading.Tasks.Task ConnectAsync(System.String host, System.Int32 port)
  RVA=0x0A8944EC  token=0x6000022  System.Threading.Tasks.Task ConnectAsync(System.Net.IPAddress[] addresses, System.Int32 port)
  RVA=0x03144110  token=0x6000023  System.Net.Sockets.NetworkStream GetStream()
  RVA=0x03994EA0  token=0x6000024  System.Void Close()
  RVA=0x039949E0  token=0x6000025  System.Void Dispose(System.Boolean disposing)
  RVA=0x039949A0  token=0x6000026  System.Void Dispose()
  RVA=0x0A894868  token=0x6000027  System.Void Finalize()
  RVA=0x040EC890  token=0x6000034  System.Void _Initialize()
  RVA=0x0A8948DC  token=0x6000035  System.Int32 _NumericOption(System.Net.Sockets.SocketOptionLevel optionLevel, System.Net.Sockets.SocketOptionName optionName)
END_CLASS

CLASS: Beyond.Network.INetIO
TYPE:  interface
TOKEN: 0x200000B
IMPLEMENTS: System.IDisposable
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000036  System.Void Connect(System.String serverIp, System.Int32 serverPort, System.Int32 sendTimeout, System.Int32 receiveBufferSize, System.Int32 sendBufferSize)
  RVA=-1  // abstract  token=0x6000037  System.Threading.Tasks.Task ConnectAsync(System.String serverIp, System.Int32 serverPort, System.Int32 sendTimeout, System.Int32 receiveBufferSize, System.Int32 sendBufferSize)
  RVA=-1  // abstract  token=0x6000038  System.Void Close()
  RVA=-1  // abstract  token=0x6000039  System.Void TestCloseNetIO(System.Boolean bIsTestReconnectFailed)
  RVA=-1  // abstract  token=0x600003A  System.Void TestDropNetIO(System.Boolean bIsTestDropSC, System.Boolean bIsTestDropCS)
  RVA=-1  // abstract  token=0x600003B  System.Void SetWeakNetPreset(Beyond.Network.ENetWeakNetPreset preset)
  RVA=-1  // abstract  token=0x600003C  System.Void SetWeakNetCustom(System.Int32 readMinDelayMs, System.Int32 readMaxDelayMs, System.Int32 readMaxTriggerCountPerSecond, System.Int32 writeMinDelayMs, System.Int32 writeMaxDelayMs, System.Int32 writeMaxTriggerCountPerSecond)
  RVA=-1  // abstract  token=0x600003D  Beyond.Network.TcpIOProfilerSnapshot GetProfilerSnapshot()
  RVA=-1  // abstract  token=0x600003E  System.Boolean IsConnected()
  RVA=-1  // abstract  token=0x600003F  System.Int32 ReadData(System.Byte[] buffer, System.Int32 offset, System.Int32 len)
  RVA=-1  // abstract  token=0x6000040  System.Void WriteData(System.Byte[] data, System.Int32 offset, System.Int32 len)
  RVA=-1  // abstract  token=0x6000041  System.Boolean IsAuthed()
  RVA=-1  // abstract  token=0x6000042  System.Void MarkAuthed()
  RVA=-1  // abstract  token=0x6000043  System.Void EnableCryptStream(System.Byte[] key, System.Byte[] iv)
  RVA=-1  // abstract  token=0x6000044  System.Int32 ReadCryptoData(System.Byte[] buffer, System.Int32 offset, System.Int32 len)
  RVA=-1  // abstract  token=0x6000045  System.Void WriteCryptoData(System.Byte[] data, System.Int32 offset, System.Int32 len)
  RVA=-1  // abstract  token=0x6000046  System.Byte[] GetSendHeadBuffer()
  RVA=-1  // abstract  token=0x6000047  System.Byte[] GetSendBuffer()
  RVA=-1  // abstract  token=0x6000048  System.Byte[] GetReceiveBuffer()
  RVA=-1  // abstract  token=0x6000049  System.Int32 GetReceiveBufferOffset()
  RVA=-1  // abstract  token=0x600004A  System.Void ClearReceiveBufferOffset()
  RVA=-1  // abstract  token=0x600004B  System.Void SkipReceiveBufferOffset(System.Int32 len)
  RVA=-1  // abstract  token=0x600004C  System.Net.EndPoint GetEndPoint()
  RVA=-1  // abstract  token=0x600004D  System.Int32 Available()
END_CLASS

CLASS: Beyond.Network.NetMsgHandlerAttribute
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Type                     msgType  // 0x10
METHODS:
  RVA=0x053908C0  token=0x600004E  System.Void .ctor(System.Type t)
  RVA=0x040E20A0  token=0x600004F  System.Void .ctor()
  RVA=0x040B71D0  token=0x6000050  System.Type GetMsgType(System.Reflection.MethodInfo annotatedMethod)
END_CLASS

CLASS: Beyond.Network.NetResponseHandler
TYPE:  sealed class
TOKEN: 0x200000D
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x02F288D0  token=0x6000051  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000052  System.Void Invoke(Beyond.Network.NetResponse& msg)
  RVA=0x0A895E24  token=0x6000053  System.IAsyncResult BeginInvoke(Beyond.Network.NetResponse& msg, System.AsyncCallback callback, System.Object object)
  RVA=0x06E6C454  token=0x6000054  System.Void EndInvoke(Beyond.Network.NetResponse& msg, System.IAsyncResult result)
END_CLASS

CLASS: Beyond.Network.NetMessageHandle`1
TYPE:  sealed class
TOKEN: 0x200000E
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000055  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000056  System.Void Invoke(T message)
  RVA=-1  // runtime  token=0x6000057  System.IAsyncResult BeginInvoke(T message, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000058  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: Beyond.Network.INetBusResponseListener
TYPE:  interface
TOKEN: 0x200000F
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000059  System.Void UpdateMsgTime(Beyond.Network.NetResponse resp)
END_CLASS

CLASS: Beyond.Network.NetRequest
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x28
FIELDS:
  public            System.UInt64                   csSeqID  // 0x10
  public            System.Int32                    mergeNum  // 0x18
  public            Google.Protobuf.IMessage        msg  // 0x20
METHODS:
END_CLASS

CLASS: Beyond.Network.NetResponse
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x30
FIELDS:
  public    static  Beyond.Network.NetResponse      EMPTY  // static @ 0x0
  public            Google.Protobuf.IMessage        headMsg  // 0x10
  public            Google.Protobuf.IMessage        msgBody  // 0x18
  public            System.Boolean                  isPacked  // 0x20
  public            System.Int64                    recvTimestamp  // 0x28
PROPERTIES:
  logHead  get=0x036AF1E0  set=0x04239180
  csHead  get=0x036AE550  set=0x03A1B170
  msgId  get=0x036AE3F0
  csSeqId  get=0x036ADB80
  scSeqId  get=0x036ADA10
METHODS:
  RVA=0x041E1670  token=0x6000061  System.Void .cctor()
END_CLASS

CLASS: Beyond.Network.MessageHandleWrapper
TYPE:  abstract class
TOKEN: 0x2000012
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000062  System.Void HandleMessage(Google.Protobuf.IMessage msg)
  RVA=0x041E1670  token=0x6000063  System.Void .ctor()
END_CLASS

CLASS: Beyond.Network.MessageHandleWrapper`1
TYPE:  class
TOKEN: 0x2000013
EXTENDS: Beyond.Network.MessageHandleWrapper
FIELDS:
  private   readonly Beyond.Network.NetMessageHandle<T>m_handle  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000064  System.Void .ctor(Beyond.Network.NetMessageHandle<T> handle)
  RVA=-1  // not resolved  token=0x6000065  System.Void HandleMessage(Google.Protobuf.IMessage msg)
END_CLASS

CLASS: Beyond.Network.NetBus
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x60
EXTENDS: Beyond.ExplicitSingleton`1
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.Network.NetResponseHandler>m_msgHandlers  // 0x10
  private   readonly System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<Beyond.Network.NetBus.HandlerEntry>>m_msgWithSeqNumHandlers  // 0x18
  private   readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.Network.MessageHandleWrapper>m_msgHdlWrappers  // 0x20
  private           Beyond.Network.HGNetSession     m_netSession  // 0x28
  private   static  System.Collections.Generic.List<Beyond.Network.NetResponseHandler>s_handlersToCall  // static @ 0x0
  private   readonly Beyond.Network.UIBusSender      <uiSender>k__BackingField  // 0x30
  private   readonly Beyond.Network.TeleportMaskSender<teleportMaskSender>k__BackingField  // 0x38
  private   readonly Beyond.Network.DisposableBusSender<defaultSender>k__BackingField  // 0x40
  private   readonly Beyond.Network.Special2BusSender<s2Sender>k__BackingField  // 0x48
  private   readonly Beyond.Network.InteractiveBusSender<interactiveSender>k__BackingField  // 0x50
  private   readonly Beyond.Network.FrequencyControlBusSender<frequencyControlSender>k__BackingField  // 0x58
PROPERTIES:
  uiSender  get=0x011F36E0
  teleportMaskSender  get=0x04D85A50
  defaultSender  get=0x04D85A60
  s2Sender  get=0x04D86200
  interactiveSender  get=0x04D86270
  frequencyControlSender  get=0x02E56440
  currentSessionID  get=0x03A4E9C0
METHODS:
  RVA=0x03BA9780  token=0x600006D  System.Void .ctor()
  RVA=0x0A895968  token=0x600006E  System.Boolean IsBusBusy()
  RVA=0x03A4BFC0  token=0x600006F  System.Void Setup(Beyond.Network.HGNetSession session)
  RVA=0x036AF5E0  token=0x6000070  System.Void Send(Google.Protobuf.IMessage msg)
  RVA=0x03895000  token=0x6000071  System.Void RegisterMessageHandler(System.Int32 msgId, Beyond.Network.MessageHandleWrapper handler)
  RVA=0x0A8959B0  token=0x6000072  System.Void UnRegisterMessageHandler(System.Int32 msgId)
  RVA=0x0A8955F8  token=0x6000073  System.Void HandleMessageTime(Beyond.Network.NetResponse response)
  RVA=0x0A895678  token=0x6000074  System.Void HandleMessage(Beyond.Network.NetResponse response)
  RVA=0x036AF640  token=0x6000075  System.Boolean _ValidateSession()
  RVA=0x03A4BC60  token=0x6000076  System.Void OnNetClientShutDown()
  RVA=0x03AC4730  token=0x6000077  Beyond.Network.NetBus.ResultHandler _SendWithResult(Google.Protobuf.IMessage msg)
  RVA=0x0A895B4C  token=0x6000078  System.Void _Listen(System.Int32 msgId, Beyond.Network.NetResponseHandler handler)
  RVA=0x0A895C80  token=0x6000079  System.Void _UnListen(System.Int32 msgId, Beyond.Network.NetResponseHandler handler)
  RVA=0x0A895A18  token=0x600007A  System.Object _ListenWithSeqNum(System.Int32 msgId, Beyond.Network.NetResponseHandler callback)
  RVA=0x03A4F820  token=0x600007B  System.Void _UnListenWithSeqNum(System.Int32 msgId, System.Object token)
  RVA=0x04D175B0  token=0x600007C  System.Void .cctor()
END_CLASS

CLASS: Beyond.Network.Special2BusSender
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x38
EXTENDS: Beyond.Network.DisposableBusSender
IMPLEMENTS: Beyond.Network.INetBusResponseListener
FIELDS:
  public            System.Collections.Generic.Dictionary<System.UInt64,System.Double>sendedMsgWithTime  // 0x20
  public            System.Double                   minSendTime  // 0x28
  private           System.Single                   m_maxWaitTime  // 0x30
PROPERTIES:
  isBusBusy  get=0x03231E70
METHODS:
  RVA=0x03AC4590  token=0x6000091  Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg)
  RVA=0x0A899144  token=0x6000092  System.Void UpdateStartBlockTime()
  RVA=0x03A4CA20  token=0x6000093  System.Void OnNetShutDown()
  RVA=0x0A899038  token=0x6000094  System.Void Dispose()
  RVA=0x03A4C9B0  token=0x6000095  System.Void _ClearSendTime()
  RVA=0x0A899098  token=0x6000096  System.Void UpdateMsgTime(Beyond.Network.NetResponse resp)
  RVA=0x03BA9BD0  token=0x6000097  System.Void .ctor()
  RVA=0x0A893C28  token=0x6000098  Beyond.Network.NetBus.ResultHandler <>iFixBaseProxy_Send(Google.Protobuf.IMessage P0)
  RVA=0x0A899090  token=0x6000099  System.Void <>iFixBaseProxy_OnNetShutDown()
  RVA=0x0A893C20  token=0x600009A  System.Void <>iFixBaseProxy_Dispose()
END_CLASS

CLASS: Beyond.Network.InteractiveBusSender
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x40
EXTENDS: Beyond.Network.Special2BusSender
FIELDS:
  private           System.Single                   m_maxWaitTime  // 0x38
PROPERTIES:
  isBusBusy  get=0x03231DC0
METHODS:
  RVA=0x05395568  token=0x600009C  System.Void .ctor()
  RVA=0x0A8955F0  token=0x600009D  System.Boolean <>iFixBaseProxy_get_isBusBusy()
END_CLASS

CLASS: Beyond.Network.DisposableBusSender
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x20
EXTENDS: Sender
IMPLEMENTS: System.IDisposable
FIELDS:
  private   readonly System.Collections.Generic.HashSet<Beyond.Network.NetBus.ResultHandler>m_resultHandlers  // 0x10
  private           System.Boolean                  <isDisposed>k__BackingField  // 0x18
PROPERTIES:
  isDisposed  get=0x04D86340  set=0x04D86350
METHODS:
  RVA=0x03AC4650  token=0x60000A0  Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg)
  RVA=0x03A4D540  token=0x60000A1  System.Void OnNetShutDown()
  RVA=0x0A893494  token=0x60000A2  System.Void Dispose()
  RVA=0x03A4EA30  token=0x60000A3  System.Void _Clear()
  RVA=0x03BA9C70  token=0x60000A4  System.Void .ctor()
END_CLASS

CLASS: Beyond.Network.NetworkMaskConfigHolder
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.Dictionary<Proto.CSMessageID,Proto.SCMessageID>msgs  // 0x10
METHODS:
  RVA=0x03BA9110  token=0x60000A5  System.Void Init()
  RVA=0x0A89802C  token=0x60000A6  System.Void _LoadJsonConfig(Beyond.Resource.StringPathHash jsonHash)
  RVA=0x03BAA9E0  token=0x60000A7  System.Void _LoadScriptableObjectConfig()
  RVA=0x0A897FB4  token=0x60000A8  System.Void Dispose()
  RVA=0x03BA9AD0  token=0x60000A9  System.Void .ctor()
END_CLASS

CLASS: Beyond.Network.UIBusSender
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x30
EXTENDS: Beyond.Network.DisposableBusSender
FIELDS:
  private           Beyond.Network.NetworkMaskConfigHolder<config>k__BackingField  // 0x20
  private           System.Collections.Generic.HashSet<System.Int32>m_maskKeys  // 0x28
PROPERTIES:
  config  get=0x04D862C0  set=0x05390F40
METHODS:
  RVA=0x03BA9A00  token=0x60000AC  System.Void .ctor()
  RVA=0x0A899FD4  token=0x60000AD  System.Void Dispose()
  RVA=0x0A89A1C8  token=0x60000AE  Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg)
  RVA=0x03A4C140  token=0x60000AF  System.Void OnNetShutDown()
  RVA=0x0A893C20  token=0x60000B0  System.Void <>iFixBaseProxy_Dispose()
  RVA=0x0A893C28  token=0x60000B1  Beyond.Network.NetBus.ResultHandler <>iFixBaseProxy_Send(Google.Protobuf.IMessage P0)
  RVA=0x0A899090  token=0x60000B2  System.Void <>iFixBaseProxy_OnNetShutDown()
END_CLASS

CLASS: Beyond.Network.TeleportMaskSender
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x28
EXTENDS: Beyond.Network.DisposableBusSender
FIELDS:
  private           System.Collections.Generic.HashSet<System.Int32>m_maskKeys  // 0x20
METHODS:
  RVA=0x0A89964C  token=0x60000B7  System.Void Dispose()
  RVA=0x03A4E360  token=0x60000B8  Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg, System.Single delay)
  RVA=0x03A4C400  token=0x60000B9  System.Void OnNetShutDown()
  RVA=0x03BA9B50  token=0x60000BA  System.Void .ctor()
  RVA=0x0A893C20  token=0x60000BB  System.Void <>iFixBaseProxy_Dispose()
  RVA=0x0A899090  token=0x60000BC  System.Void <>iFixBaseProxy_OnNetShutDown()
END_CLASS

CLASS: Beyond.Network.FrequencyControlBusSender
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x40
EXTENDS: Beyond.Network.DisposableBusSender
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<Proto.CSMessageID,System.Collections.Generic.Queue<System.Double>>m_lastSendTime  // 0x20
  private           System.Collections.Generic.List<Google.Protobuf.IMessage>m_waitingSendMsgs  // 0x28
  private           System.Collections.Generic.List<System.Action<System.Int32>>m_onError  // 0x30
  private           Beyond.TickFunction             m_loopId  // 0x38
METHODS:
  RVA=0x0382A7D0  token=0x60000C2  System.Void .ctor()
  RVA=0x04CCF2E0  token=0x60000C3  System.Void _TrySendWaitingMsg(System.Single deltaTime)
  RVA=0x0A893640  token=0x60000C4  Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg)
  RVA=0x0A8936B4  token=0x60000C5  Beyond.Network.NetBus.ResultHandler Send(Google.Protobuf.IMessage msg, System.Boolean needAddToWaitingList, System.Action<System.Int32> onError, System.Action onWait)
  RVA=0x0A89359C  token=0x60000C6  System.Void Dispose()
  RVA=0x0A893C28  token=0x60000C7  Beyond.Network.NetBus.ResultHandler <>iFixBaseProxy_Send(Google.Protobuf.IMessage P0)
  RVA=0x0A893C20  token=0x60000C8  System.Void <>iFixBaseProxy_Dispose()
END_CLASS

CLASS: Beyond.Network.ServerDesc
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x28
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   addr  // 0x18
  public            System.Int32                    port  // 0x20
METHODS:
  RVA=0x041AB130  token=0x60000CD  System.Void .ctor()
END_CLASS

CLASS: Beyond.Network.ServerList
TYPE:  class
TOKEN: 0x2000023
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<Beyond.Network.ServerDesc>servers  // 0x10
METHODS:
  RVA=0x04D2DF20  token=0x60000CE  System.Void .ctor()
END_CLASS

CLASS: Beyond.Network.ServerConfig
TYPE:  class
TOKEN: 0x2000024
SIZE:  0x28
FIELDS:
  public            System.String                   serverName  // 0x10
  public            System.String                   uid  // 0x18
  public            System.Boolean                  checkDev  // 0x20
METHODS:
  RVA=0x0A898FB4  token=0x60000CF  System.Boolean IsSame(Beyond.Network.ServerConfig other)
  RVA=0x04D53840  token=0x60000D0  System.Void .ctor()
END_CLASS

CLASS: Beyond.Network.HostData
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x28
FIELDS:
  public            System.String                   host  // 0x10
  public            System.Int32                    port  // 0x18
  public            System.Double                   delay  // 0x20
METHODS:
  RVA=0x03DA4C40  token=0x60000D1  System.Void .ctor()
END_CLASS

CLASS: Beyond.Network.NetConfig
TYPE:  class
TOKEN: 0x2000026
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<Beyond.Network.HostData>hosts  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60000D2  System.Void .ctor(System.Collections.Generic.List<Beyond.Network.HostData> hosts)
END_CLASS

CLASS: Beyond.Network.NetConsts
TYPE:  static class
TOKEN: 0x2000027
SIZE:  0x10
FIELDS:
  public    static readonly System.String                   NETMASK_CONFIG_PATH  // static @ 0x0
  public    static readonly System.String                   NETMASK_CONFIG_JSON_PATH  // static @ 0x8
  public    static readonly System.String                   NETMASK_PREFAB_PATH  // static @ 0x10
METHODS:
  RVA=0x04CC95D0  token=0x60000D3  System.Void .cctor()
END_CLASS

CLASS: Beyond.Network.INetworkSystem
TYPE:  interface
TOKEN: 0x2000028
IMPLEMENTS: Beyond.ILuaCallCSharp
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60000D4  System.Void OnReConnect()
  RVA=-1  // abstract  token=0x60000D5  System.Void SendMsg(Google.Protobuf.IMessage msg, System.Action<System.Int32> onError)
END_CLASS

CLASS: Beyond.Network.NetMaskConfig
TYPE:  class
TOKEN: 0x2000029
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.Dictionary<System.String,Beyond.Network.NetMaskConfig.Config>configs  // 0x10
METHODS:
  RVA=0x0A895DAC  token=0x60000D6  System.Void .ctor()
END_CLASS

CLASS: Beyond.Network.TcpIOProfilerSnapshot
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x50
FIELDS:
  public            System.Int64                    readApiCallCount  // 0x10
  public            System.Int64                    readApiCallCountPerSecond  // 0x18
  public            System.Int64                    writeApiCallCount  // 0x20
  public            System.Int64                    writeApiCallCountPerSecond  // 0x28
  public            System.Int64                    totalReadBytes  // 0x30
  public            System.Int64                    totalWriteBytes  // 0x38
  public            System.Int64                    readBytesPerSecond  // 0x40
  public            System.Int64                    writeBytesPerSecond  // 0x48
METHODS:
END_CLASS

CLASS: Beyond.Network.NetSessionProfilerSnapshot
TYPE:  sealed struct
TOKEN: 0x200002C
SIZE:  0x40
FIELDS:
  public            System.Int64                    logicalSendPacketCount  // 0x10
  public            System.Int64                    logicalSendPacketCountPerSecond  // 0x18
  public            System.Int64                    actualSendPacketCount  // 0x20
  public            System.Int64                    actualSendPacketCountPerSecond  // 0x28
  public            System.Int64                    receivePacketCount  // 0x30
  public            System.Int64                    receivePacketCountPerSecond  // 0x38
METHODS:
END_CLASS

CLASS: Beyond.Network.NetDevProfilerSnapshot
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x80
FIELDS:
  public            Beyond.Network.TcpIOProfilerSnapshottcpIO  // 0x10
  public            Beyond.Network.NetSessionProfilerSnapshotsession  // 0x50
METHODS:
END_CLASS

CLASS: Beyond.Network.NetUtil
TYPE:  static class
TOKEN: 0x200002E
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_isRegistedAllMessages  // static @ 0x0
  public    static readonly System.Int32                    MAX_RECEIVE_PACK_BODY_SIZE  // static @ 0x4
  public    static readonly System.Int32                    MAX_MERGE_PACK_BODY_SIZE  // static @ 0x8
  public    static readonly System.Int32                    MAX_SEND_PACK_BODY_SIZE  // static @ 0xc
  private   static  System.Collections.Generic.Dictionary<System.Int32,System.Type>s_sc_id2MessageType  // static @ 0x10
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Int32>s_sc_messageType2Id  // static @ 0x18
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Int32>s_cs_messageType2Id  // static @ 0x20
  private   static  System.Byte[]                   s_decompressedBuffer  // static @ 0x28
METHODS:
  RVA=0x03693A30  token=0x60000D8  System.Void RegisterSCMessage(System.Int32 msgId, System.Type msgType)
  RVA=0x036928F0  token=0x60000D9  System.Void RegisterCSMessage(System.Int32 msgId, System.Type msgType)
  RVA=0x03895080  token=0x60000DA  System.Int32 GetSCMsgIdByType(System.Type msgType)
  RVA=0x0A895FE8  token=0x60000DB  System.Type GetSCMsgTypeById(System.Int32 msgId)
  RVA=0x03142260  token=0x60000DC  System.Int32 GetCSMsgIdByType(System.Type msgType)
  RVA=-1  // generic def  token=0x60000DD  System.Void _GenMessageName2ValuePair(System.Collections.Generic.Dictionary<System.String,System.Int32> dic)
  RVA=0x036935C0  token=0x60000DE  System.Void FastRegisterMessage()
  RVA=0x03A19EE0  token=0x60000DF  System.Boolean GetNetMessageFromDataBytes(System.Span<System.Byte> headBytes, System.Byte[] bodyBuffer, System.Int32 offset, System.Int32 bodyLen, System.Byte[] packedbuffer, Beyond.Network.NetResponse& response)
  RVA=0x03142960  token=0x60000E0  System.Int32 SerializeHeadMessage(Google.Protobuf.IMessage msg, System.UInt64 upSeqid, System.Int32 bodyLen, System.Int32 crc32, System.Byte[] buffer, System.Int32 offset)
  RVA=0x0A896098  token=0x60000E1  System.Int32 SerializeBodyMessage(Google.Protobuf.IMessage msg, System.Byte[] buffer, System.Int32& crc32, System.Int32 offset)
  RVA=0x04331FF0  token=0x60000E2  System.Void AutoRegisterMessageHandlers(System.Object target)
  RVA=0x0A895EA4  token=0x60000E3  System.Void AutoUnRegisterMessageHandlers(System.Object target)
  RVA=0x02F2F9F0  token=0x60000E4  System.Void _ForeachMessageHandlers(System.Object target, System.Action<System.Type,System.Reflection.MethodInfo> doFunc)
  RVA=0x0404E310  token=0x60000E5  System.Void .cctor()
END_CLASS

CLASS: Beyond.Network.NetworkFrequencyConfig
TYPE:  static class
TOKEN: 0x2000031
SIZE:  0x10
FIELDS:
  public    static  Beyond.SerializeFieldDictionary<System.Int32,Beyond.Network.NetworkFrequencyConfig.NetworkFrequency>msgs  // static @ 0x0
METHODS:
  RVA=0x0A896288  token=0x60000EB  System.Void .cctor()
END_CLASS

CLASS: Beyond.Network.NetworkMask
TYPE:  class
TOKEN: 0x2000033
SIZE:  0x60
EXTENDS: Beyond.Singleton`1
FIELDS:
  private           Beyond.TickFunction             m_preTickFunction  // 0x10
  private           System.Int32                    m_nextKey  // 0x18
  public            UnityEngine.GameObject          maskObj  // 0x20
  public            UnityEngine.GameObject          decoObj  // 0x28
  private           Beyond.Resource.FAssetProxyHandlem_handle  // 0x30
  private           System.Action<System.Boolean>   m_onNetMaskChanged  // 0x48
  private           System.Boolean                  <showingMask>k__BackingField  // 0x50
  private           System.Boolean                  <showingDeco>k__BackingField  // 0x51
  private           System.Boolean                  m_isNetReconnecting  // 0x52
  private   static  System.String                   DECO_NAME  // const
  private           System.Collections.Generic.List<Beyond.Network.NetworkMask.MaskInfo>m_masks  // 0x58
PROPERTIES:
  showingMask  get=0x04D86800  set=0x04D86840
  showingDeco  get=0x04D867F0  set=0x04D86830
METHODS:
  RVA=0x047A6DA0  token=0x60000F1  System.Void .ctor()
  RVA=0x047A6E40  token=0x60000F2  System.Void OnInit()
  RVA=0x0A89869C  token=0x60000F3  System.Void OnUnInit()
  RVA=0x0A898738  token=0x60000F4  System.Void Release()
  RVA=0x0A898968  token=0x60000F5  System.Void _OnSwitchLanguage()
  RVA=0x047A71B0  token=0x60000F6  System.Void _CreateTickFunction()
  RVA=0x0A898900  token=0x60000F7  System.Void _ClearTickFunction()
  RVA=0x03A4CB30  token=0x60000F8  System.Void PreTick(System.Single deltaTime)
  RVA=0x03A4CF90  token=0x60000F9  System.Int32 _GetKey()
  RVA=0x0A8985DC  token=0x60000FA  System.Int32 AddMask(Proto.CSMessageID msgId, System.Single timeoutTime)
  RVA=0x03A4CE80  token=0x60000FB  System.Int32 AddMask(System.String name, System.Single timeoutTime, System.Single delayTime)
  RVA=0x03A4CA60  token=0x60000FC  System.Void RemoveMask(System.Int32 key)
  RVA=0x04DA5BC0  token=0x60000FD  System.Void SetIsNetReconnecting(System.Boolean isReconnecting)
  RVA=0x03A4CBE0  token=0x60000FE  System.Void _UpdateMaskState()
  RVA=0x04A290A0  token=0x60000FF  System.Void ListenOnNetMaskChanged(System.Action<System.Boolean> action)
  RVA=0x0A898848  token=0x6000100  System.Void UnListenOnNetMaskChanged(System.Action<System.Boolean> action)
END_CLASS

CLASS: Beyond.Network.NetworkMaskConfig
TYPE:  class
TOKEN: 0x2000035
SIZE:  0x28
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public            Beyond.SerializeFieldDictionary<Proto.CSMessageID,Proto.SCMessageID>msgs  // 0x18
  public            System.Collections.Generic.List<Beyond.Network.NetworkMaskConfig.Config>configs  // 0x20
METHODS:
  RVA=0x04CB5BA0  token=0x6000104  System.Void .ctor()
END_CLASS

CLASS: Beyond.Network.NetworkUrls
TYPE:  static class
TOKEN: 0x2000037
SIZE:  0x10
FIELDS:
  public    static  System.String                   URL_GET_SPLIT  // const
METHODS:
  RVA=0x03C0DDD0  token=0x6000106  System.String RemoteConfigUrl()
  RVA=0x0A898D70  token=0x6000107  System.String ResourceRootUrlDev(System.String version)
  RVA=0x03C082B0  token=0x6000108  System.String ServerListUrl()
  RVA=0x0A898C40  token=0x6000109  System.String AgeTipsUrl()
  RVA=0x0A898CD8  token=0x600010A  System.String GovUrl()
  RVA=0x0340BD20  token=0x600010B  System.String LauncherUrl()
  RVA=0x0340DC70  token=0x600010C  System.String _SelectUrl(System.String urlInConfig, Beyond.Network.NetworkUrls.EUrlType urlType)
  RVA=0x03C082E0  token=0x600010D  System.String _GetUrlFromLocal(Beyond.Network.NetworkUrls.EUrlType type)
END_CLASS

CLASS: Beyond.Network.TcpIO
TYPE:  class
TOKEN: 0x2000039
SIZE:  0xA0
IMPLEMENTS: Beyond.Network.INetIO System.IDisposable
FIELDS:
  private           Beyond.Network.HGTcpClient      m_tcpClient  // 0x10
  protected         System.Byte[]                   m_recvBuffer  // 0x18
  protected         System.Byte[]                   m_sendHeadBuffer  // 0x20
  protected         System.Byte[]                   m_sendBuffer  // 0x28
  protected         System.Int32                    m_recvBufferOffset  // 0x30
  private           Beyond.XXEnc.XXE1               m_encryptor  // 0x38
  private           Beyond.XXEnc.XXE1               m_decryptor  // 0x40
  protected         System.Boolean                  m_isAuthed  // 0x48
  private           System.Boolean                  m_bIsTestDropSC  // 0x49
  private           System.Boolean                  m_bIsTestDropCS  // 0x4a
  private   readonly System.Object                   m_readWeakNetLock  // 0x50
  private   readonly System.Object                   m_writeWeakNetLock  // 0x58
  private           System.Boolean                  m_bIsTestReconnectFailed  // 0x60
  private           System.Int64                    m_readWeakNetWindowStartMs  // 0x68
  private           System.Int64                    m_writeWeakNetWindowStartMs  // 0x70
  private           System.Int32                    m_readWeakNetTriggerCount  // 0x78
  private           System.Int32                    m_writeWeakNetTriggerCount  // 0x7c
  private           Beyond.Network.TcpIO.WeakNetDelayConfigm_readWeakNetConfig  // 0x80
  private           Beyond.Network.TcpIO.WeakNetDelayConfigm_writeWeakNetConfig  // 0x90
  public    static  System.Int32                    SEND_HEAD_BUFFER_LENGTH  // const
  public    static  System.Int32                    s_MAX_SEND_SIZE  // static @ 0x0
  private   static readonly System.Random                   s_random  // static @ 0x8
METHODS:
  RVA=0x040EC0B0  token=0x600010E  System.Void Connect(System.String serverIp, System.Int32 serverPort, System.Int32 sendTimeout, System.Int32 receiveBufferSize, System.Int32 sendBufferSize)
  RVA=0x040EBE40  token=0x600010F  System.Threading.Tasks.Task ConnectAsync(System.String serverIp, System.Int32 serverPort, System.Int32 sendTimeout, System.Int32 receiveBufferSize, System.Int32 sendBufferSize)
  RVA=0x031491C0  token=0x6000110  System.Boolean IsAuthed()
  RVA=0x03219110  token=0x6000111  System.Void MarkAuthed()
  RVA=0x049E1B50  token=0x6000112  System.Void EnableCryptStream(System.Byte[] key, System.Byte[] iv)
  RVA=0x0A89927C  token=0x6000113  System.Net.EndPoint GetEndPoint()
  RVA=0x039944E0  token=0x6000114  System.Void Close()
  RVA=0x0A8993A8  token=0x6000115  System.Void TestCloseNetIO(System.Boolean bIsTestReconnectFailed)
  RVA=0x0A899414  token=0x6000116  System.Void TestDropNetIO(System.Boolean bIsTestDropSC, System.Boolean bIsTestDropCS)
  RVA=0x038EA280  token=0x6000117  System.Void SetWeakNetPreset(Beyond.Network.ENetWeakNetPreset preset)
  RVA=0x038EA2E0  token=0x6000118  System.Void SetWeakNetCustom(System.Int32 readMinDelayMs, System.Int32 readMaxDelayMs, System.Int32 readMaxTriggerCountPerSecond, System.Int32 writeMinDelayMs, System.Int32 writeMaxDelayMs, System.Int32 writeMaxTriggerCountPerSecond)
  RVA=0x0314ACA0  token=0x6000119  System.Boolean IsConnected()
  RVA=0x0A899304  token=0x600011A  Beyond.Network.TcpIOProfilerSnapshot GetProfilerSnapshot()
  RVA=0x03145500  token=0x600011B  System.Void WriteData(System.Byte[] data, System.Int32 offset, System.Int32 len)
  RVA=0x03142580  token=0x600011C  System.Void WriteCryptoData(System.Byte[] data, System.Int32 offset, System.Int32 len)
  RVA=0x03142620  token=0x600011D  System.Byte[] GetSendHeadBuffer()
  RVA=0x03143910  token=0x600011E  System.Byte[] GetSendBuffer()
  RVA=0x03149B30  token=0x600011F  System.Byte[] GetReceiveBuffer()
  RVA=0x03F636B0  token=0x6000120  System.Void ClearReceiveBufferOffset()
  RVA=0x0314AEB0  token=0x6000121  System.Int32 GetReceiveBufferOffset()
  RVA=0x03F2E980  token=0x6000122  System.Void SkipReceiveBufferOffset(System.Int32 len)
  RVA=0x03143E50  token=0x6000123  System.Int32 ReadData(System.Byte[] buffer, System.Int32 offset, System.Int32 len)
  RVA=0x03143CE0  token=0x6000124  System.Int32 ReadCryptoData(System.Byte[] buffer, System.Int32 offset, System.Int32 len)
  RVA=0x0314A480  token=0x6000125  System.Int32 Available()
  RVA=0x03994450  token=0x6000126  System.Void Dispose()
  RVA=0x038EA4C0  token=0x6000127  System.Void _ResetWeakNetWindow(System.Int64& windowStartMs, System.Int32& triggerCount)
  RVA=0x038EA3F0  token=0x6000128  Beyond.Network.TcpIO.WeakNetDelayConfig _CreateWeakNetDelayConfig(System.Int32 minDelayMs, System.Int32 maxDelayMs, System.Int32 maxTriggerCountPerSecond)
  RVA=0x0A899484  token=0x6000129  System.Int32 _NextDelayMs(Beyond.Network.TcpIO.WeakNetDelayConfig config)
  RVA=0x03144350  token=0x600012A  System.Void _ApplyWeakNetDelay(System.Object delayLock, Beyond.Network.TcpIO.WeakNetDelayConfig config, System.Int64& windowStartMs, System.Int32& triggerCount)
  RVA=0x039943D0  token=0x600012B  System.Void Finalize()
  RVA=0x03A4B2F0  token=0x600012C  System.Void .ctor()
  RVA=0x04022150  token=0x600012D  System.Void .cctor()
END_CLASS

CLASS: Beyond.Network.HttpSender
TYPE:  static class
TOKEN: 0x200003B
SIZE:  0x10
FIELDS:
  public    static  System.String                   CONTENT_TYPE_JSON  // const
  public    static  System.String                   CONTENT_ENCODING_GZIP  // const
  public    static  System.Int32                    ERROR_CODE_TIMEOUT  // const
  public    static  System.Int32                    ERROR_CODE_DNS_FAILED  // const
  public    static  System.Int32                    ERROR_CODE_DESERIALIZE  // const
  public    static  System.Single                   TIMEOUT_DEFAULT  // const
  private   static  System.Int32                    OPT_GET  // const
  private   static  System.Int32                    OPT_POST  // const
METHODS:
  RVA=0x03C0A480  token=0x600012E  System.Collections.IEnumerator YieldSendGet(System.String url, Beyond.Network.HttpSender.FGetOptions options, Beyond.Misc.ObjectRef resultRef)
  RVA=-1  // generic def  token=0x600012F  System.Collections.IEnumerator YieldSendGet(System.String url, Beyond.Network.HttpSender.FGetOptions options, Beyond.Misc.ObjectRef resultRef)
  RVA=0x03C07F10  token=0x6000130  System.Collections.IEnumerator YieldSendGet(System.String url, Beyond.Network.HttpSender.FGetOptions options, System.Type respType, Beyond.Misc.ObjectRef resultRef)
  RVA=0x0A8951D4  token=0x6000131  System.Collections.IEnumerator YieldSendPost(System.String url, Beyond.Network.HttpSender.FPostOptions options, Beyond.Misc.ObjectRef resultRef)
  RVA=-1  // generic def  token=0x6000132  System.Collections.IEnumerator YieldSendPost(System.String url, Beyond.Network.HttpSender.FPostOptions options, Beyond.Misc.ObjectRef resultRef)
  RVA=0x049E2670  token=0x6000133  System.Collections.IEnumerator YieldSendPost(System.String url, Beyond.Network.HttpSender.FPostOptions options, System.Type respType, Beyond.Misc.ObjectRef resultRef)
  RVA=0x0A8950D8  token=0x6000134  Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> WaitForSendPost(System.String url, Beyond.Network.HttpSender.FPostOptions options)
  RVA=0x0A894F3C  token=0x6000135  Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> WaitForSendGet(System.String url)
  RVA=0x0A894FDC  token=0x6000136  Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> WaitForSendGet(System.String url, Beyond.Network.HttpSender.FGetOptions options)
  RVA=-1  // generic def  token=0x6000137  Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result<TResp>> WaitForSendPost(System.String url, Beyond.Network.HttpSender.FPostOptions options)
  RVA=-1  // generic def  token=0x6000138  Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result<TResp>> WaitForSendGet(System.String url, Beyond.Network.HttpSender.FGetOptions options)
  RVA=0x03C08390  token=0x6000139  System.Int32 GetGeneralErrorCode(Beyond.Network.HttpSender.Result result, System.Boolean& hasError)
  RVA=0x035A40C0  token=0x600013A  System.Collections.IEnumerator _YieldSendHttpRequest(Beyond.Network.HttpSender.Result result, Beyond.Network.HttpSender.FSendFunc sendFunc)
  RVA=0x046BAD90  token=0x600013B  System.Collections.IEnumerator _YieldSendHttpRequest(Beyond.Misc.ObjectRef resultRef, Beyond.Network.HttpSender.FSendFunc sendFunc, System.Type respType)
  RVA=0x0A895428  token=0x600013C  Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> _WaitForSendHttpRequest(Beyond.Network.HttpSender.FSendFunc sendFunc)
  RVA=-1  // generic def  token=0x600013D  Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result<TResp>> _WaitForSendHttpRequest(Beyond.Network.HttpSender.FSendFunc sendFunc)
  RVA=0x035A3740  token=0x600013E  System.Void _StartWebRequestTask(Beyond.Network.HttpSender.Result result, Beyond.Network.HttpSender.FSendFunc sendFunc)
  RVA=0x04B88010  token=0x600013F  System.Void _WaitForServiceDone(Beyond.Network.HttpSender.Result result)
  RVA=0x03E37670  token=0x6000140  UnityEngine.Networking.UnityWebRequest _CreateWebRequestGET(System.String url, Beyond.Network.HttpSender.FGetOptions options)
  RVA=0x03E34BA0  token=0x6000141  UnityEngine.Networking.UnityWebRequest _CreateWebRequestPOST(System.String url, Beyond.Network.HttpSender.FPostOptions options)
  RVA=0x03E37480  token=0x6000142  System.Void _SetHttpResult(Beyond.Network.HttpSender.Result result, UnityEngine.Networking.UnityWebRequest request)
  RVA=0x035A4580  token=0x6000143  System.Void _DeserializeTypedResult(Beyond.Network.HttpSender.TypedResult result)
  RVA=0x0A8952BC  token=0x6000144  System.Void _DebugOnlyProfileResult(Beyond.Network.HttpSender.Result result, Beyond.Network.HttpSender.FSendFunc sendFunc)
  RVA=0x0A895354  token=0x6000145  System.Void _MarkResultAsException(Beyond.Network.HttpSender.Result result, System.Exception ex)
END_CLASS

CLASS: Beyond.Network.CircularBuffer`1
TYPE:  class
TOKEN: 0x200004E
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IReadOnlyList`1 System.Collections.Generic.IReadOnlyCollection`1
FIELDS:
  private   readonly T[]                             m_buffer  // 0x0
  private           System.Int32                    m_end  // 0x0
  private           System.Int32                    m_count  // 0x0
  private           System.Int32                    m_start  // 0x0
PROPERTIES:
  Capacity  get=-1  // not resolved
  IsFull  get=-1  // not resolved
  IsEmpty  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600018B  System.Void .ctor(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x600018C  System.Void .ctor(System.Int32 capacity, T[] items)
  RVA=-1  // not resolved  token=0x6000193  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000194  System.Collections.Generic.IEnumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000195  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000196  T Front()
  RVA=-1  // not resolved  token=0x6000197  System.Boolean TryFront(T& result)
  RVA=-1  // not resolved  token=0x6000198  T Back()
  RVA=-1  // not resolved  token=0x6000199  System.Boolean TryBack(T& result)
  RVA=-1  // not resolved  token=0x600019A  System.Void PushBack(T item)
  RVA=-1  // not resolved  token=0x600019B  System.Void PushFront(T item)
  RVA=-1  // not resolved  token=0x600019C  System.Void PopBack()
  RVA=-1  // not resolved  token=0x600019D  System.Void PopFront()
  RVA=-1  // not resolved  token=0x600019E  System.Boolean TryPopFront(T& result)
  RVA=-1  // not resolved  token=0x600019F  System.Boolean TryPopBack(T& result)
  RVA=-1  // not resolved  token=0x60001A0  T[] ToArray()
  RVA=-1  // not resolved  token=0x60001A1  System.Void _ThrowIfEmpty(System.String message)
  RVA=-1  // not resolved  token=0x60001A2  System.Void _Increment(System.Int32& index)
  RVA=-1  // not resolved  token=0x60001A3  System.Void _Decrement(System.Int32& index)
  RVA=-1  // not resolved  token=0x60001A4  System.Int32 _InternalIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x60001A5  System.ArraySegment<T> _ArrayOne()
  RVA=-1  // not resolved  token=0x60001A6  System.ArraySegment<T> _ArrayTwo()
END_CLASS

CLASS: Beyond.Network.HGNetBaseSession
TYPE:  abstract class
TOKEN: 0x2000050
SIZE:  0xE0
FIELDS:
  private           Beyond.Network.INetIO           m_tcpIO  // 0x10
  private           Beyond.Network.ENetWeakNetPresetm_weakNetPreset  // 0x18
  private           System.Boolean                  m_useCustomWeakNetConfig  // 0x1c
  private           System.Int32                    m_readWeakNetMinDelayMs  // 0x20
  private           System.Int32                    m_readWeakNetMaxDelayMs  // 0x24
  private           System.Int32                    m_readWeakNetMaxTriggerCountPerSecond  // 0x28
  private           System.Int32                    m_writeWeakNetMinDelayMs  // 0x2c
  private           System.Int32                    m_writeWeakNetMaxDelayMs  // 0x30
  private           System.Int32                    m_writeWeakNetMaxTriggerCountPerSecond  // 0x34
  public    static  System.Int32                    s_recvBuffLen  // static @ 0x0
  public    static  System.Int32                    s_tcpSendTimeout  // static @ 0x4
  public    static  System.Int32                    s_sessionThreadSleepTimeMS  // static @ 0x8
  public    static  System.Int32                    s_sessionThreadPauseSleepTimeMS  // static @ 0xc
  public            System.Int32                    m_maxReconnectTimeout  // 0x38
  public            System.Int32                    m_maxReconnectTimes  // 0x3c
  public            System.Int32                    sendNumLimit  // 0x40
  protected readonly System.Collections.Concurrent.ConcurrentQueue<Beyond.Network.NetResponse>m_recvQueue  // 0x48
  protected readonly System.Collections.Concurrent.ConcurrentQueue<Beyond.Network.NetRequest>m_sendQueue  // 0x50
  private   static  System.Int32                    MAX_RECYCLE_QUEUE_COUNT  // const
  protected         Beyond.Network.CircularBuffer<Beyond.Network.NetRequest>m_circularBuffer  // 0x58
  private           System.Int64                    m_csSeqId  // 0x60
  protected         System.Int64                    m_scSeqId  // 0x68
  protected         System.Threading.Tasks.Task     m_threadTask  // 0x70
  protected         System.Int32                    m_threadTaskSignalID  // 0x78
  protected         Beyond.ELogChannel              m_logChannel  // 0x7c
  protected         System.Boolean                  m_isSyncFullDataEnd  // 0x7d
  protected         Google.Protobuf.IMessage        m_lastLoginMsg  // 0x80
  private           System.String                   m_lastConnectServerIp  // 0x88
  private           System.Int32                    m_lastConnectServerPort  // 0x90
  protected         System.Int32                    m_tryReconnectTimes  // 0x94
  protected         System.Boolean                  m_isAutoReconnect  // 0x98
  protected         System.Int64                    m_startReconnectTimestampMS  // 0xa0
  protected         System.Int32                    m_closeNetMsgID  // 0xa8
  public    static  System.Int32                    INVALID_SESSION_ID  // static @ 0x10
  protected static  System.Int32                    s_sessionIDGenerator  // static @ 0x14
  private           System.Int32                    <sessionID>k__BackingField  // 0xac
  protected         Beyond.Network.HGNetSessionConnectYieldm_lastAsyncConnectHandle  // 0xb0
  protected         Beyond.Network.EHGNetSessionStatem_state  // 0xb8
  private           Beyond.Network.EHGNetSessionReconnectStatem_reconnectState  // 0xb9
  protected         System.Double                   m_startReconnectTimeMS  // 0xc0
  protected         System.Int32                    m_closeErrorCode  // 0xc8
  public    static  System.Boolean                  s_applicationIsPaused  // static @ 0x18
  private           System.Int64                    m_clientLastReceiveMsgMilliTs  // 0xd0
  private           System.Int64                    m_clientLastSendMsgMilliTs  // 0xd8
PROPERTIES:
  sessionID  get=0x04D88CA0  set=0x04D88D00
  reconnectState  get=0x0314AA90  set=0x03813C30
  isRunning  get=0x036AB820
  isConnecting  get=0x0A8941D0
  isRunningAndConnected  get=0x036AC050
  isReconnecting  get=0x036AB0B0
  closeErrorCode  get=0x0A894130
  isAutoReconnect  get=0x0A894180
  maxRecycleQueueCount  get=0x03142230
METHODS:
  RVA=0x03812B00  token=0x60001B8  System.Void Start()
  RVA=0x0A893C30  token=0x60001B9  System.Boolean Connect(System.String serverIp, System.Int32 serverPort, System.Boolean forceReconnect)
  RVA=0x038EB980  token=0x60001BA  System.Boolean ConnectAsync(System.String serverIp, System.Int32 serverPort, System.Boolean forceReconnect, Beyond.Network.HGNetSessionConnectYield& yieldHandle)
  RVA=0x0480FC80  token=0x60001BB  System.Void OnConnectAsyncEnd(System.Threading.Tasks.Task completedTask, Beyond.Network.INetIO tcpIO)
  RVA=-1  // abstract  token=0x60001BC  System.Void OnConnectedSucceed(Beyond.Network.INetIO tcpIO)
  RVA=0x040EE260  token=0x60001BD  System.Void _DNSCheck(System.String serverIp)
  RVA=0x039941E0  token=0x60001BE  System.Void Close(Beyond.Network.ENetCloseReason netCloseReason)
  RVA=0x0A893FE0  token=0x60001BF  System.Void InternalClose(Beyond.Network.INetIO tcpIO, Beyond.Network.ENetCloseReason netCloseReason)
  RVA=0x03994220  token=0x60001C0  System.Void InternalClose(Beyond.Network.INetIO tcpIO, System.Int32 netCloseReason)
  RVA=0x036AEF10  token=0x60001C1  System.Void EnqueueSendMsg(Beyond.Network.NetRequest msg)
  RVA=0x036AD6A0  token=0x60001C2  System.Int32 GetSendQueueMsgCount()
  RVA=0x036AEED0  token=0x60001C3  System.Int64 IncrementCSSeqID()
  RVA=0x03219140  token=0x60001C4  System.Void SetCSSeqID(System.Int64 id)
  RVA=0x0A893F90  token=0x60001C5  System.Int64 GetCSSeqID()
  RVA=0x03219080  token=0x60001C6  Beyond.Network.INetIO GetIO()
  RVA=0x040EB970  token=0x60001C7  System.Void StartTCPMsgLoopThreadTask()
  RVA=0x03994F30  token=0x60001C8  System.Void CloseTCPMsgThreadTask()
  RVA=0x048AC9D0  token=0x60001C9  System.Threading.Tasks.Task _SessionThread(Beyond.Network.INetIO tcp, System.Int32 threadID)
  RVA=0x031480E0  token=0x60001CA  System.Boolean _SendAndCheckMessage(Beyond.Network.INetIO tcp, System.Int32 threadID)
  RVA=0x03148890  token=0x60001CB  System.Boolean _ReadAndCheckMessageInSessionThread(Beyond.Network.INetIO tcp, System.Int32 threadID)
  RVA=-1  // abstract  token=0x60001CC  System.Boolean ReadMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& resp)
  RVA=-1  // abstract  token=0x60001CD  System.Void CheckAndDealRespMessages(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& m)
  RVA=-1  // abstract  token=0x60001CE  System.Void CheckAndWriteReqMsg(Beyond.Network.INetIO netIO, Beyond.Network.NetRequest& m)
  RVA=0x0314A640  token=0x60001CF  System.Void SessionCheckSignalAndSleep(System.Int32 threadID)
  RVA=0x040EB530  token=0x60001D0  System.Void OpenAutoReconnect()
  RVA=0x03994F00  token=0x60001D1  System.Void RemoveAutoReconnect()
  RVA=0x0314CF30  token=0x60001D2  System.Threading.Tasks.Task _ReconnectCheckerTick(Beyond.Network.INetIO tcpIO)
  RVA=0x047CEC70  token=0x60001D3  System.Threading.Tasks.Task _DoReconnectAsync(Beyond.Network.INetIO tcpIO)
  RVA=0x03813800  token=0x60001D4  System.Threading.Tasks.Task<System.Boolean> TryConnectWithTimeout(Beyond.Network.INetIO tcpIO, System.String ip, System.Int32 port, System.Int32 sendTimeout, System.Int32 receiveBufferLen, System.Int32 timeoutMilliseconds)
  RVA=0x03813970  token=0x60001D5  System.Threading.Tasks.Task<System.Boolean> TryCloseTcpIOWithTimeout(Beyond.Network.INetIO tcpIO, System.Int32 timeoutMilliseconds)
  RVA=0x038EA1D0  token=0x60001D6  System.Void SetWeakNetPreset(Beyond.Network.ENetWeakNetPreset preset)
  RVA=0x0A89405C  token=0x60001D7  System.Void SetWeakNetCustom(System.Int32 readMinDelayMs, System.Int32 readMaxDelayMs, System.Int32 readMaxTriggerCountPerSecond, System.Int32 writeMinDelayMs, System.Int32 writeMaxDelayMs, System.Int32 writeMaxTriggerCountPerSecond)
  RVA=0x038EA220  token=0x60001D8  System.Void _ApplyWeakNetConfigToTcpIO(Beyond.Network.INetIO tcpIO)
  RVA=-1  // abstract  token=0x60001D9  System.Void OnReconnectTimesOver(Beyond.Network.INetIO tcpIO)
  RVA=-1  // abstract  token=0x60001DA  System.Boolean NeedKeepConnect()
  RVA=-1  // abstract  token=0x60001DB  System.Void DoReconnectLogin(Beyond.Network.INetIO netIO)
  RVA=-1  // abstract  token=0x60001DC  System.Void InitClientRSAKey()
  RVA=-1  // abstract  token=0x60001DD  System.Void ClearRSAKey()
  RVA=-1  // abstract  token=0x60001DE  System.Void ClearRecvQueue()
  RVA=-1  // abstract  token=0x60001DF  System.Void ClearSendQueue()
  RVA=0x03148FE0  token=0x60001E0  System.Void NetStatusChecker_Receive(Beyond.Network.INetIO netIO, System.Int64 scSeqId, System.Int32 receiveCount)
  RVA=0x03148680  token=0x60001E1  System.Void NetStatusChecker_Sender(Beyond.Network.INetIO netIO, System.Int64 csSeqId, System.Int32 sendCount)
  RVA=-1  // abstract  token=0x60001E2  System.Void SessionConnectTimeoutCheckerTick(Beyond.Network.INetIO netIO, System.Int64 clientLastReceiveMsgMilliTs, System.Int64 clientLastSendMsgMilliTs)
  RVA=-1  // abstract  token=0x60001E3  System.Void ClearPingWDG()
  RVA=-1  // abstract  token=0x60001E4  System.Boolean IsPingOut()
  RVA=-1  // abstract  token=0x60001E5  System.Void PingSenderTick(Beyond.Network.INetIO netIO, System.Int64 upSeqId)
  RVA=0x03A4B180  token=0x60001E6  System.Void .ctor()
  RVA=0x04D11BD0  token=0x60001E7  System.Void .cctor()
END_CLASS

CLASS: Beyond.Network.HGNetSession
TYPE:  class
TOKEN: 0x2000059
SIZE:  0x188
EXTENDS: Beyond.Network.HGNetBaseSession
FIELDS:
  private           System.String                   m_uid  // 0xe0
  private           System.String                   m_token  // 0xe8
  private           System.Int32                    m_channel  // 0xf0
  public    static  System.Int32                    LOGIN_TIMEOUT  // static @ 0x0
  public    static  System.Int32                    MAX_CLIENT_RESEND_NUM  // static @ 0x4
  public    static  System.Single                   PER_FRAME_RECEIVED_MESSAGES_TIME_30FPS  // static @ 0x8
  public    static  System.Single                   PER_FRAME_RECEIVED_MESSAGES_TIME  // static @ 0xc
  public    static  System.Single                   MESSAGE_MAX_DELAY_TIME  // static @ 0x10
  protected         System.Int32                    m_messageDelayCount  // 0xf4
  protected         System.Int64                    m_logCDTimestamp  // 0xf8
  private   static  System.Boolean                  s_HGNetSessionBurst  // static @ 0x14
  private   readonly System.Collections.Concurrent.ConcurrentQueue<Beyond.Network.NetRequest>m_frameSendQueue  // 0x100
  private           System.Collections.Generic.Queue<Beyond.Network.NetRequest>m_mergeQueueCache  // 0x108
  private           System.Object                   m_frameSendQueueLocker  // 0x110
  private           System.Byte[]                   m_frameSendPacketBuffer  // 0x118
  private           System.Byte[]                   m_frameSendPacketHeadBuffer  // 0x120
  private           System.Byte[]                   m_frameSendPacketTempBuffer  // 0x128
  private           System.Byte[]                   m_frameSendCryptoBuffer  // 0x130
  private           System.Byte[]                   m_frameReceivePacketBuffer  // 0x138
  public    static  System.Int64                    s_maxframeSendPacketBuffer  // static @ 0x18
  public    static  System.Int64                    s_maxPackedMessageNum  // static @ 0x20
  protected readonly System.Collections.Generic.Queue<Beyond.Network.NetResponse>m_callbackQueue  // 0x140
  private           Beyond.Network.HGNetSessionLoginYieldm_currLoginHandle  // 0x148
  private   static  System.Int32                    SEND_PING_DELTA_TICK_TIME_MS  // const
  private   static  System.Int32                    IN_PING_PROGRESS_MAX_TIME  // const
  private   static  System.Int32                    s_pingWdgOutTimes  // static @ 0x28
  private   static  System.Int32                    s_pingSyncFullEndBeforeTimes  // static @ 0x2c
  private           System.Int64                    m_needSendPingTimer  // 0x150
  private           System.Int32                    m_pingWDG  // 0x158
  private           System.Int64                    m_clientTsDelayms  // 0x160
  private           System.Int32                    m_pingValue  // 0x168
  private           System.Boolean                  m_isPingInProgress  // 0x16c
  private           System.Int64                    m_isPingInProgressStartTime  // 0x170
  private   static readonly System.String                   CLIENT_NET_PEM_PUBLIC_KEY_HEAD_FORMAT  // static @ 0x30
  private   static readonly System.String                   CLIENT_NET_RSA_PUBLIC_KEY  // static @ 0x38
  private   static readonly System.String                   CLIENT_NET_RSA_PRIVATE_KEY  // static @ 0x40
  private   static  System.Byte[]                   s_clientPublicKeyBytes  // static @ 0x48
  private           System.Security.Cryptography.RSAm_rsaClientPrivateProvider  // 0x178
  private   static readonly System.Byte[]                   EMPTY_BYTES  // static @ 0x50
  private   static  System.Int32                    s_CacheLogLength  // static @ 0x58
  private           System.Text.StringBuilder       m_sbCache  // 0x180
METHODS:
  RVA=0x03A4BBE0  token=0x60001F8  System.Void SetAutoReconnectToken(System.String uid, System.String token, System.Int32 channel)
  RVA=0x03CCC6E0  token=0x60001F9  System.Void SessionConnectTimeoutCheckerTick(Beyond.Network.INetIO netIO, System.Int64 clientLastReceiveMsgMilliTs, System.Int64 clientLastSendMsgMilliTs)
  RVA=0x0A89D01C  token=0x60001FA  System.Void DoReconnectLogin(Beyond.Network.INetIO netIO)
  RVA=0x04A42F40  token=0x60001FB  System.Void Start()
  RVA=0x0A89D938  token=0x60001FC  System.Void OnApplicationPause(System.Boolean pause)
  RVA=0x036AF240  token=0x60001FD  System.UInt64 SendMessage(Google.Protobuf.IMessage msg)
  RVA=0x0A89DA94  token=0x60001FE  System.Void PackMessageToSendQueue()
  RVA=0x0A89E23C  token=0x60001FF  System.Void _FlushMergeQueueToSendQueue()
  RVA=0x0A89E1D0  token=0x6000200  System.Void UpdateInGameThread()
  RVA=0x0A89E198  token=0x6000201  System.Void TailLateUpdateInGameThread()
  RVA=0x0A89E314  token=0x6000202  System.Single _GetMessageTimeLimit()
  RVA=0x0A89ED38  token=0x6000203  System.Void _ProcessReceivedMessagesInGameThread(System.Boolean flushAll)
  RVA=0x0379D830  token=0x6000204  Beyond.Network.HGNetSessionLoginYield LoginAsync(System.String uid, System.String token, System.Int32 channel)
  RVA=0x03C07910  token=0x6000205  System.Void _CSLoginHelper_UpdateAppInfo(Proto.MSG_A1 msg)
  RVA=0x03C07A90  token=0x6000206  System.String _GetOnlineResVersion()
  RVA=0x03C07850  token=0x6000207  Proto.ENV_TYPE _GetSDKEnv()
  RVA=0x03C07FF0  token=0x6000208  Proto.CLIENT_PLATFORM_TYPE _GetPlatformID()
  RVA=0x03C08020  token=0x6000209  System.Int32 _GetPayPlatform()
  RVA=0x03218D00  token=0x600020A  System.Void _SessionLoginThreadTask(System.Object handle)
  RVA=0x040EB480  token=0x600020B  System.Void StartMsgLoop()
  RVA=0x04D802C0  token=0x600020C  System.Void OnConnectedSucceed(Beyond.Network.INetIO tcpIO)
  RVA=0x0A89DA28  token=0x600020D  System.Void OnReconnectTimesOver(Beyond.Network.INetIO tcpIO)
  RVA=0x03CCC7A0  token=0x600020E  System.Boolean NeedKeepConnect()
  RVA=0x03142110  token=0x600020F  System.Void CheckAndWriteReqMsg(Beyond.Network.INetIO netIO, Beyond.Network.NetRequest& req)
  RVA=0x03142650  token=0x6000210  System.Void _WriteMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetRequest& request, System.Boolean useSRSA)
  RVA=0x03148010  token=0x6000211  System.Boolean ReadMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& resp)
  RVA=0x03149BD0  token=0x6000212  System.Boolean _ReadMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& resp, System.Boolean useSRSA)
  RVA=0x036AB3C0  token=0x6000213  System.Void ClearRecvQueue()
  RVA=0x04D32C80  token=0x6000214  System.Void ClearSendQueue()
  RVA=0x0A89E144  token=0x6000215  System.Void SetBurstMode(System.Boolean val)
  RVA=0x0A89D804  token=0x6000216  Beyond.Network.NetDevProfilerSnapshot GetNetDevProfilerSnapshot()
  RVA=0x03993B40  token=0x6000217  System.Void Finalize()
  RVA=0x036ABAA0  token=0x6000218  System.Int32 GetPing()
  RVA=0x0A89D8E8  token=0x6000219  System.Boolean IsPingInProgress()
  RVA=0x039F3270  token=0x600021A  System.Int64 GetPingPassedTime()
  RVA=0x0425AD80  token=0x600021B  System.Void ClearPingWDG()
  RVA=0x03219180  token=0x600021C  System.Void _ApproximateLoginServerTime(System.Int64 serverTimeMS, System.Int32 serverZone)
  RVA=0x03F7A520  token=0x600021D  System.Boolean _HandleSpecialMessagePing(Beyond.Network.NetResponse m)
  RVA=0x03B27E90  token=0x600021E  System.Boolean IsPingOut()
  RVA=0x03147B20  token=0x600021F  System.Void PingSenderTick(Beyond.Network.INetIO netIO, System.Int64 upSeqId)
  RVA=0x04D1E2F0  token=0x6000220  System.Byte[] GetClientPublicKey()
  RVA=0x03FE3D40  token=0x6000221  System.Void InitClientRSAKey()
  RVA=0x032198D0  token=0x6000222  System.Byte[] _RSADecrypt(System.Byte[] data)
  RVA=0x0A89CF54  token=0x6000223  System.Void ClearRSAKey()
  RVA=0x036AE080  token=0x6000224  System.Void CheckAndDealRespMessages(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& msg)
  RVA=0x0A89E624  token=0x6000225  System.Boolean _HandleSpecialMessageReconnectIncr(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse m)
  RVA=0x0A89E474  token=0x6000226  System.Boolean _HandleSpecialMessageReconnectFull(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse m)
  RVA=0x0A89E394  token=0x6000227  System.Boolean _HandleSpecialMessageErrorCode(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse m)
  RVA=0x0A89EB80  token=0x6000228  System.Boolean _HandleSpecialMessageReconnectLogin(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse m)
  RVA=0x032195B0  token=0x6000229  System.Boolean _HandleLoginEncryp(Beyond.Network.INetIO netIO, Proto.MSG_B1 msg)
  RVA=0x03A4AEF0  token=0x600022A  System.Void .ctor()
  RVA=0x04A24C30  token=0x600022B  System.Void .cctor()
  RVA=0x0A89CA18  token=0x600022C  System.Void <>iFixBaseProxy_Start()
END_CLASS

CLASS: Beyond.Network.EHGNetSessionState
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  Beyond.Network.EHGNetSessionStateClosed  // const
  public    static  Beyond.Network.EHGNetSessionStateOpened  // const
  public    static  Beyond.Network.EHGNetSessionStateConnecting  // const
  public    static  Beyond.Network.EHGNetSessionStateSessionRunning  // const
METHODS:
END_CLASS

CLASS: Beyond.Network.EHGNetSessionReconnectState
TYPE:  sealed struct
TOKEN: 0x200005B
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  Beyond.Network.EHGNetSessionReconnectStateNone  // const
  public    static  Beyond.Network.EHGNetSessionReconnectStateReconnectingTcp  // const
  public    static  Beyond.Network.EHGNetSessionReconnectStateSendLogin  // const
  public    static  Beyond.Network.EHGNetSessionReconnectStateResendPack  // const
METHODS:
END_CLASS

CLASS: Beyond.Network.ENetCloseReason
TYPE:  sealed struct
TOKEN: 0x200005C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Network.ENetCloseReason  Invalid  // const
  public    static  Beyond.Network.ENetCloseReason  None  // const
  public    static  Beyond.Network.ENetCloseReason  ForServerErrorMax  // const
  public    static  Beyond.Network.ENetCloseReason  ConnectFailed  // const
  public    static  Beyond.Network.ENetCloseReason  ServerReconnectFailed  // const
  public    static  Beyond.Network.ENetCloseReason  TooMuchClientReconnectPack  // const
  public    static  Beyond.Network.ENetCloseReason  ServerReconnectFullPack  // const
  public    static  Beyond.Network.ENetCloseReason  ClientShutDown  // const
  public    static  Beyond.Network.ENetCloseReason  ClientLaunch  // const
  public    static  Beyond.Network.ENetCloseReason  ClientReconnectTooMuchTimes  // const
METHODS:
END_CLASS

CLASS: Beyond.Network.HGNetSessionUtils
TYPE:  static class
TOKEN: 0x200005D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A89CEEC  token=0x600022D  System.Boolean CheckErrorNeedsDisconnect(Proto.CODE errorCode)
END_CLASS

CLASS: Beyond.Network.HGNetSessionConstant
TYPE:  static class
TOKEN: 0x200005E
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    CODE_SUC  // const
  public    static  System.Int32                    CODE_CONN_FAILED  // const
  public    static  System.Int32                    CODE_CONN_WAIT  // const
METHODS:
END_CLASS

CLASS: Beyond.Network.ENetConnectErrorCode
TYPE:  sealed struct
TOKEN: 0x200005F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Network.ENetConnectErrorCodeInvalid  // const
  public    static  Beyond.Network.ENetConnectErrorCodeNone  // const
  public    static  Beyond.Network.ENetConnectErrorCodeForServerErrorMax  // const
  public    static  Beyond.Network.ENetConnectErrorCodeConnectError  // const
  public    static  Beyond.Network.ENetConnectErrorCodeTimeOut  // const
METHODS:
END_CLASS

CLASS: Beyond.Network.HGNetSessionLoginYield
TYPE:  class
TOKEN: 0x2000060
SIZE:  0x38
EXTENDS: UnityEngine.CustomYieldInstruction
FIELDS:
  private           Beyond.Network.HGNetSessionLoginYield.LoginErrorInfom_loginErrorInfo  // 0x10
  private           System.Boolean                  m_bIsFinish  // 0x18
  private           Proto.MSG_A1                    m_request  // 0x20
  private           Proto.MSG_B1                    m_resp  // 0x28
  private           System.Object                   m_locker  // 0x30
PROPERTIES:
  isSucceed  get=0x03A4BB20
  keepWaiting  get=0x04CC5CD0
METHODS:
  RVA=0x03A4B3A0  token=0x600022E  System.Void .ctor()
  RVA=0x03A4ADC0  token=0x600022F  System.Void .ctor(Proto.MSG_A1 req)
  RVA=0x032190B0  token=0x6000230  Proto.MSG_A1 GetRequest()
  RVA=0x03A4B870  token=0x6000231  Proto.MSG_B1 GetResponse()
  RVA=0x0A89CB24  token=0x6000232  System.Int32 GetErrorCode()
  RVA=0x0A89CB7C  token=0x6000233  System.String GetErrorDetails()
  RVA=0x03A4ABD0  token=0x6000234  System.Void SetSucceed(Proto.MSG_B1 sc)
  RVA=0x0A89CCCC  token=0x6000235  System.Void SetNtfError(System.Int32 ntfErrorCode, System.String ntfErrorDetails)
  RVA=0x0A89CBD4  token=0x6000236  System.Void SetConnectErrorError()
  RVA=0x0A89CDF4  token=0x6000237  System.Void SetTimeOutError()
END_CLASS

CLASS: Beyond.Network.HGNetSessionConnectYield
TYPE:  class
TOKEN: 0x2000062
SIZE:  0x20
EXTENDS: UnityEngine.CustomYieldInstruction
FIELDS:
  private           System.Threading.Tasks.Task     m_connectTask  // 0x10
  private           Beyond.Network.ENetConnectErrorCodeconnectErrorCode  // 0x18
PROPERTIES:
  isSucceed  get=0x03A4B840
  keepWaiting  get=0x04CE44F0
METHODS:
  RVA=0x04D87910  token=0x6000244  System.Void .ctor()
  RVA=0x038EA930  token=0x6000245  System.Void .ctor(System.Threading.Tasks.Task connectTask)
  RVA=0x0A89CAD8  token=0x6000246  System.Int32 GetCode()
  RVA=0x0480FDD0  token=0x6000247  System.Void SetFinish(Beyond.Network.ENetConnectErrorCode code)
END_CLASS

CLASS: Beyond.Network.HGNetLogger.HGNetLogBaseSession
TYPE:  abstract class
TOKEN: 0x2000063
SIZE:  0x98
FIELDS:
  private   readonly Beyond.Network.INetIO           m_tcpIO  // 0x10
  public    static  System.Int32                    s_recvBuffLen  // static @ 0x0
  public    static  System.Int32                    s_tcpSendTimeout  // static @ 0x4
  public    static  System.Int32                    s_sessionThreadSleepTimeMS  // static @ 0x8
  public    static  System.Int32                    s_sessionThreadPauseSleepTimeMS  // static @ 0xc
  public    static  System.Int32                    s_maxReconnectTimeout  // static @ 0x10
  public    static  System.Int32                    s_maxReconnectTimes  // static @ 0x14
  protected         System.Int32                    m_sendNumLimit  // 0x18
  protected readonly System.Collections.Concurrent.ConcurrentQueue<Beyond.Network.NetResponse>m_recvQueue  // 0x20
  private   readonly System.Collections.Concurrent.ConcurrentQueue<Beyond.Network.NetRequest>m_sendQueue  // 0x28
  private   static  System.Int32                    MAX_RECYCLE_QUEUE_COUNT  // const
  protected         Beyond.Network.CircularBuffer<Beyond.Network.NetRequest>m_circularBuffer  // 0x30
  private           System.Int64                    m_csSeqId  // 0x38
  protected         System.Int64                    m_scSeqId  // 0x40
  protected         System.Threading.Tasks.Task     m_threadTask  // 0x48
  protected         System.Int32                    m_threadTaskSignal  // 0x50
  protected         Beyond.ELogChannel              m_logChannel  // 0x54
  private           System.String                   m_lastConnectServerIp  // 0x58
  private           System.Int32                    m_lastConnectServerPort  // 0x60
  protected         System.Int32                    m_tryReconnectTimes  // 0x64
  protected         System.Int64                    m_startReconnectTimestampMS  // 0x68
  protected         System.Boolean                  m_isAutoReconnect  // 0x70
  protected         Beyond.Network.HGNetSessionConnectYieldm_lastAsyncConnectHandle  // 0x78
  protected         Beyond.Network.EHGNetSessionStatem_state  // 0x80
  protected         Beyond.Network.EHGNetSessionReconnectStatem_reconnectState  // 0x81
  protected         System.Int32                    m_closeErrorCode  // 0x84
  public    static  System.Boolean                  s_applicationIsPaused  // static @ 0x18
  private           System.Int64                    m_clientLastReceiveMsgMilliTs  // 0x88
  private           System.Int64                    m_clientLastSendMsgMilliTs  // 0x90
PROPERTIES:
  isRunning  get=0x0A89C518
  isConnecting  get=0x0A89C470
  isRunningAndConnected  get=0x0A89C4C4
  closeErrorCode  get=0x0A89C3D4
  isAutoReconnect  get=0x0A89C424
METHODS:
  RVA=0x0A89B840  token=0x600024F  System.Void Start()
  RVA=0x0A89ACF0  token=0x6000250  System.Boolean Connect(System.String serverIp, System.Int32 serverPort, System.Boolean forceReconnect)
  RVA=0x0A89A954  token=0x6000251  System.Boolean ConnectAsync(System.String serverIp, System.Int32 serverPort, System.Boolean forceReconnect, Beyond.Network.HGNetSessionConnectYield& yieldHandle)
  RVA=0x0A89B384  token=0x6000252  System.Void OnConnectAsyncEnd(System.Threading.Tasks.Task continuationAction)
  RVA=-1  // abstract  token=0x6000253  System.Void OnConnectedSucceed(Beyond.Network.INetIO tcpIO)
  RVA=0x0A89B988  token=0x6000254  System.Void _DNSCheck(System.String serverIp)
  RVA=0x0A89A6F0  token=0x6000255  System.Void Close(Beyond.Network.ENetCloseReason netCloseReason)
  RVA=0x0A89A754  token=0x6000256  System.Void Close(System.Int32 netCloseReason)
  RVA=0x0A89AFDC  token=0x6000257  System.Void EnqueueSendMsg(Beyond.Network.NetRequest msg)
  RVA=0x0A89B130  token=0x6000258  System.Int32 GetSendQueueMsgCount()
  RVA=0x0A89B1AC  token=0x6000259  System.Int64 IncrementCSSeqID()
  RVA=0x0A89B620  token=0x600025A  System.Void SetCSSeqID(System.Int64 id)
  RVA=0x0A89B090  token=0x600025B  System.Int64 GetCSSeqID()
  RVA=0x0A89B0E0  token=0x600025C  Beyond.Network.INetIO GetIO()
  RVA=0x0A89B67C  token=0x600025D  System.Void StartTCPMsgLoopThreadTask()
  RVA=0x0A89A610  token=0x600025E  System.Void CloseTCPMsgThreadTask()
  RVA=0x0A89BE08  token=0x600025F  System.Void _SessionThread()
  RVA=-1  // abstract  token=0x6000260  System.Boolean ReadMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& resp)
  RVA=-1  // abstract  token=0x6000261  System.Void CheckAndDealRespMessages(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& m)
  RVA=-1  // abstract  token=0x6000262  System.Void CheckAndWriteReqMsg(Beyond.Network.INetIO netIO, Beyond.Network.NetRequest& m)
  RVA=0x0A89B558  token=0x6000263  System.Void SessionCheckSignalAndSleep()
  RVA=0x0A89B4BC  token=0x6000264  System.Void OpenAutoReconnect()
  RVA=0x0A89B50C  token=0x6000265  System.Void RemoveAutoReconnect()
  RVA=0x0A89BB44  token=0x6000266  System.Void _ReconnectCheckerTick()
  RVA=-1  // abstract  token=0x6000267  System.Void OnReconnectTimesOver()
  RVA=-1  // abstract  token=0x6000268  System.Boolean NeedKeepConnect()
  RVA=-1  // abstract  token=0x6000269  System.Void DoReconnectLogin(Beyond.Network.INetIO netIO)
  RVA=-1  // abstract  token=0x600026A  System.Void InitClientRSAKey()
  RVA=-1  // abstract  token=0x600026B  System.Void ClearRSAKey()
  RVA=0x0A89B204  token=0x600026C  System.Void NetStatusChecker_Receive(Beyond.Network.INetIO netIO, System.Int64 scSeqId, System.Int32 receiveCount)
  RVA=0x0A89B2B8  token=0x600026D  System.Void NetStatusChecker_Sender(Beyond.Network.INetIO netIO, System.Int64 csSeqId, System.Int32 sendCount)
  RVA=-1  // abstract  token=0x600026E  System.Void SessionConnectTimeoutCheckerTick(Beyond.Network.INetIO netIO, System.Int64 clientLastReceiveMsgMilliTs, System.Int64 clientLastSendMsgMilliTs)
  RVA=-1  // abstract  token=0x600026F  System.Void ClearPingWDG()
  RVA=-1  // abstract  token=0x6000270  System.Boolean IsPingOut()
  RVA=-1  // abstract  token=0x6000271  System.Void PingSenderTick(Beyond.Network.INetIO netIO, System.Int64 upSeqId)
  RVA=0x0A89C27C  token=0x6000272  System.Void .ctor()
  RVA=0x0A89C1C8  token=0x6000273  System.Void .cctor()
END_CLASS

CLASS: Beyond.Network.HGNetLogger.HGNetLogger
TYPE:  class
TOKEN: 0x2000064
SIZE:  0x38
IMPLEMENTS: Beyond.Log.ILogger
FIELDS:
  private           Beyond.Log.BitArray256          m_enabledLogChannel  // 0x10
  private           System.Collections.Concurrent.ConcurrentQueue<Beyond.Log.FLogMessage>m_logMessagesQueue  // 0x30
  public    static  System.Int32                    s_maxLogQueueSize  // static @ 0x0
  public    static  System.Int32                    s_maxRepeatedMsgMs  // static @ 0x4
  public    static  System.Int32                    s_maxRepeatedMsgCount  // static @ 0x8
METHODS:
  RVA=0x03136140  token=0x6000274  System.Void Log(Beyond.Log.FLogMessage& msg)
  RVA=0x03F7B160  token=0x6000275  Beyond.Network.HGNetLogger.LogLevel ConvertFromDLogger(Beyond.ELogLevel level)
  RVA=0x03F7B050  token=0x6000276  Beyond.Network.HGNetLogger.NativeLogLevel ConvertFromDLoggerV2(Beyond.ELogLevel level)
  RVA=0x03F5AE50  token=0x6000277  System.Boolean CheckLog(Beyond.Log.FLogMessage& msg)
  RVA=0x048FE070  token=0x6000278  System.Void Clear()
  RVA=0x0A89CA20  token=0x6000279  System.Void SetLogChannel(System.UInt64 channel1, System.UInt64 channel2, System.UInt64 channel3, System.UInt64 channel4)
  RVA=0x040329F0  token=0x600027A  System.Int32 ConvertFromDLogger(Beyond.ELogChannel channel)
  RVA=0x03E3E0C0  token=0x600027B  System.Void .ctor()
  RVA=0x04D53380  token=0x600027C  System.Void .cctor()
END_CLASS

CLASS: Beyond.Network.HGNetLogger.HGNetLoggerClient
TYPE:  class
TOKEN: 0x2000065
SIZE:  0x30
FIELDS:
  private   static  Beyond.Network.HGNetLogger.HGNetLoggerClients_instance  // static @ 0x0
  private   static  System.Int64                    <s_roleID>k__BackingField  // static @ 0x8
  public            Beyond.Network.HGNetLogger.HGNetLoggerSessionm_logsession  // 0x10
  private           Beyond.Network.HGNetLogger.HGNetLoggerm_netLogger  // 0x18
  public    static readonly System.Int32                    MAX_LOG_SEND_QUEUE_SIZE  // static @ 0x10
  private   static  System.Int32                    MAX_LOCAL_LOG_QUEUE_SIZE  // const
  private   readonly System.Collections.Generic.Queue<Beyond.Log.FLogMessage>m_localLogQueue  // 0x20
  private   readonly System.Object                   m_queueLock  // 0x28
  public    static  Beyond.Network.HGNetLogger.Versions_usedVersion  // static @ 0x14
PROPERTIES:
  instance  get=0x03A84130
  s_roleID  get=0x0A89C634  set=0x0A89C684
METHODS:
  RVA=0x04CDEC20  token=0x600027D  System.Void .ctor()
  RVA=0x03E3DA80  token=0x6000281  System.Void Init(System.String uid, System.String token, System.String env, System.String roleId)
  RVA=0x0313A4C0  token=0x6000282  System.Void AddLog(System.String message, System.String stacktrace, UnityEngine.LogType type)
  RVA=0x03139630  token=0x6000283  System.Void _FlushLocalLogQueue()
  RVA=0x0A89C56C  token=0x6000284  System.Void OnApplicationPause(System.Boolean pauseStatus)
  RVA=0x03C11B10  token=0x6000285  System.Void ShutDown()
  RVA=0x03993C20  token=0x6000286  System.Void _CloseAndClear(Beyond.Network.ENetCloseReason reason)
  RVA=0x036AE800  token=0x6000287  System.Void WriteNetLog(Beyond.Network.HGNetLogger.LogMessage& log)
  RVA=0x04CED270  token=0x6000288  System.Void SetRoleID(System.UInt64 roleID)
  RVA=0x04D802F0  token=0x6000289  System.Void SetNetLogMaskFromServer(Google.Protobuf.Collections.RepeatedField<System.UInt64> masks)
  RVA=0x04D5DD70  token=0x600028A  System.Void .cctor()
  RVA=0x048FE050  token=0x600028B  System.Void <Init>b__15_0()
END_CLASS

CLASS: Beyond.Network.HGNetLogger.HGNetLoggerCloudSettings
TYPE:  class
TOKEN: 0x2000066
SIZE:  0x30
FIELDS:
  public    static readonly Beyond.Network.HGNetLogger.HGNetLoggerCloudSettingsinstance  // static @ 0x0
  private           Beyond.Network.HGNetLogger.LogControlm_cloudLogControl  // 0x10
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>m_cloudChannelLevelMaskTables  // 0x18
  private           Beyond.Network.HGNetLogger.LogControlm_localLogControl  // 0x20
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>m_localChannelLevelMaskTables  // 0x28
  private   static  System.Collections.Generic.List<Beyond.Network.HGNetLogger.NativeLogLevel>s_allLevels  // static @ 0x8
METHODS:
  RVA=0x03A82D30  token=0x600028C  System.Void SetCloudLogControl(Beyond.Network.HGNetLogger.AuthResponse control, Beyond.Network.HGNetBaseSession session)
  RVA=0x0313A3F0  token=0x600028D  System.Boolean IsNetLogLevelEnabled(Beyond.ELogLevel elevel, Beyond.ELogChannel channel)
  RVA=0x0333BE50  token=0x600028E  System.Boolean IsNetLogLevelEnabled(Beyond.Network.HGNetLogger.NativeLogLevel level, System.Int32 channel)
  RVA=0x0A89C6DC  token=0x600028F  System.Boolean IsLocalLogLevelEnabled(Beyond.ELogLevel elevel, Beyond.ELogChannel channel)
  RVA=0x0313A470  token=0x6000290  Beyond.Network.HGNetLogger.NativeLogLevel ConvertFromDLogger(Beyond.ELogLevel level)
  RVA=0x049C5F00  token=0x6000291  System.Void .ctor()
  RVA=0x049C5DA0  token=0x6000292  System.Void .cctor()
END_CLASS

CLASS: Beyond.Network.HGNetLogger.HGNetLoggerSession
TYPE:  class
TOKEN: 0x2000067
SIZE:  0x118
EXTENDS: Beyond.Network.HGNetBaseSession
FIELDS:
  private           System.Byte[]                   m_keyBytes  // 0xe0
  private           System.Collections.Generic.Dictionary<System.String,System.Int32>m_templeCRCDict  // 0xe8
  private           System.Action                   m_onReconnectSuccess  // 0xf0
  private           System.String                   m_uid  // 0xf8
  private           System.String                   m_roleId  // 0x100
  private           System.String                   m_token  // 0x108
  private           System.String                   m_env  // 0x110
  private   static  System.Collections.Generic.Dictionary<System.Type,Beyond.Network.HGNetLogger.MessageType>s_m_messageType2Id  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<Beyond.Network.HGNetLogger.MessageType,System.Type>s_m_messageId2Type  // static @ 0x8
PROPERTIES:
  uid  get=0x04D7D910
  roleId  get=0x04D7D940
  s_messageType2Id  get=0x03142E60
  s_messageId2Type  get=0x041CE8B0
METHODS:
  RVA=0x03142E00  token=0x6000297  System.Boolean TryGetMsgTypeByID(System.Type msgType, Beyond.Network.HGNetLogger.MessageType& type)
  RVA=0x038125C0  token=0x6000298  System.Void Start()
  RVA=0x03E3E070  token=0x6000299  System.Void SetReconnectSuccessCallback(System.Action onReconnectSuccess)
  RVA=0x0A89C85C  token=0x600029A  System.Void OnApplicationPause(System.Boolean pause)
  RVA=0x040EB4D0  token=0x600029B  System.Void OnConnectedSucceed(Beyond.Network.INetIO tcpIO)
  RVA=0x03149260  token=0x600029C  System.Boolean ReadMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& resp)
  RVA=0x036AEF80  token=0x600029D  System.Void CheckAndDealRespMessages(Beyond.Network.INetIO netIO, Beyond.Network.NetResponse& resp)
  RVA=0x03142D90  token=0x600029E  System.Void CheckAndWriteReqMsg(Beyond.Network.INetIO netIO, Beyond.Network.NetRequest& m)
  RVA=0x031432B0  token=0x600029F  System.Void _WriteMessageInSessionThread(Beyond.Network.INetIO netIO, Beyond.Network.NetRequest request)
  RVA=0x036AE900  token=0x60002A0  System.UInt64 SendMessage(Google.Protobuf.IMessage msg)
  RVA=0x03E3E280  token=0x60002A1  System.Void SetAutoReconnectToken(System.String uid, System.String token, System.String env, System.String roleId)
  RVA=0x036AD550  token=0x60002A2  System.Boolean NeedKeepConnect()
  RVA=0x03813B40  token=0x60002A3  System.Void DoReconnectLogin(Beyond.Network.INetIO netIO)
  RVA=0x03147870  token=0x60002A4  System.Int32 _GetConnectTimeoutTime()
  RVA=0x03147910  token=0x60002A5  System.Void SessionConnectTimeoutCheckerTick(Beyond.Network.INetIO netIO, System.Int64 clientLastReceiveMsgMilliTs, System.Int64 clientLastSendMsgMilliTs)
  RVA=0x04CD9730  token=0x60002A6  System.Void ClearPingWDG()
  RVA=0x03813DF0  token=0x60002A7  System.Void SendLogin(Beyond.Network.INetIO netIO)
  RVA=0x0423B0F0  token=0x60002A8  System.Boolean IsPingOut()
  RVA=0x03DAE9A0  token=0x60002A9  System.Void PingSenderTick(Beyond.Network.INetIO netIO, System.Int64 upSeqId)
  RVA=0x04D5ED40  token=0x60002AA  System.Void InitClientRSAKey()
  RVA=0x0A89C818  token=0x60002AB  System.Void ClearRSAKey()
  RVA=0x04D5EDA0  token=0x60002AC  System.Void ClearRecvQueue()
  RVA=0x04D5ED70  token=0x60002AD  System.Void ClearSendQueue()
  RVA=0x0A89C95C  token=0x60002AE  System.Void OnReconnectTimesOver(Beyond.Network.INetIO tcpIO)
  RVA=0x03E3E320  token=0x60002AF  System.Void .ctor()
  RVA=0x0A89CA18  token=0x60002B0  System.Void <>iFixBaseProxy_Start()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.Runtime.InteropServices.LayoutKindlayoutKind  // 0x10
  public            System.Boolean                  isTag  // 0x14
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000003  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000004  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000068
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x60002B1  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x0A89F0C8  token=0x60002B2  System.Net.Sockets.Socket __Gen_Wrap_0(System.Object P0)
  RVA=0x06377B18  token=0x60002B3  System.Void __Gen_Wrap_1(System.Object P0, System.Object P1)
  RVA=0x063632E4  token=0x60002B4  System.Boolean __Gen_Wrap_2(System.Object P0)
  RVA=0x066C2B40  token=0x60002B5  System.Void __Gen_Wrap_3(System.Object P0, System.Boolean P1)
  RVA=0x06360E84  token=0x60002B6  System.Int32 __Gen_Wrap_4(System.Object P0)
  RVA=0x066F4838  token=0x60002B7  System.Void __Gen_Wrap_5(System.Object P0, System.Object P1, System.Int32 P2)
  RVA=0x0A8A43F4  token=0x60002B8  System.IAsyncResult __Gen_Wrap_6(System.Object P0, System.Object P1, System.Int32 P2, System.Object P3, System.Object P4)
  RVA=0x0A8A4CF8  token=0x60002B9  System.Threading.Tasks.Task __Gen_Wrap_7(System.Object P0, System.Object P1, System.Int32 P2)
  RVA=0x0A8A5528  token=0x60002BA  System.Net.Sockets.NetworkStream __Gen_Wrap_8(System.Object P0)
  RVA=0x063633B4  token=0x60002BB  System.Void __Gen_Wrap_9(System.Object P0)
  RVA=0x071AFE44  token=0x60002BC  System.Int32 __Gen_Wrap_10(System.Object P0, System.Net.Sockets.SocketOptionLevel P1, System.Net.Sockets.SocketOptionName P2)
  RVA=0x066F3838  token=0x60002BD  System.Void __Gen_Wrap_11(System.Object P0, System.Int32 P1)
  RVA=0x0A8A08BC  token=0x60002BE  System.Net.Sockets.LingerOption __Gen_Wrap_12(System.Object P0)
  RVA=0x0A8A0C78  token=0x60002BF  Beyond.Network.HGNetLogger.Header __Gen_Wrap_13(Beyond.Network.NetResponse& P0)
  RVA=0x0A8A0DE0  token=0x60002C0  System.Void __Gen_Wrap_14(Beyond.Network.NetResponse& P0, System.Object P1)
  RVA=0x0A8A0F44  token=0x60002C1  Proto.CSHead __Gen_Wrap_15(Beyond.Network.NetResponse& P0)
  RVA=0x0A8A10AC  token=0x60002C2  System.Int32 __Gen_Wrap_16(Beyond.Network.NetResponse& P0)
  RVA=0x0A8A1204  token=0x60002C3  System.UInt64 __Gen_Wrap_17(Beyond.Network.NetResponse& P0)
  RVA=0x067CDAC0  token=0x60002C4  System.Int64 __Gen_Wrap_18(System.Object P0)
  RVA=0x0A8A135C  token=0x60002C5  System.UInt64 __Gen_Wrap_19(System.Object P0, System.Object P1)
  RVA=0x0A8A1448  token=0x60002C6  System.Type __Gen_Wrap_20(System.Int32 P0)
  RVA=0x066F4C88  token=0x60002C7  System.Void __Gen_Wrap_21(System.Object P0, System.Int32 P1, System.Object P2)
  RVA=0x0A8A1534  token=0x60002C8  Beyond.Network.NetBus.ResultHandler __Gen_Wrap_22(System.Object P0, System.Object P1)
  RVA=0x0A8A1640  token=0x60002C9  System.Void __Gen_Wrap_23(System.Object P0, Beyond.Network.NetResponse& P1)
  RVA=0x0A8A176C  token=0x60002CA  Beyond.Network.NetBus.ResultHandler __Gen_Wrap_24(System.Object P0, Proto.SCMessageID P1, System.Object P2)
  RVA=0x09C81AB0  token=0x60002CB  System.Void __Gen_Wrap_25(System.Object P0, Proto.SCMessageID P1, System.Object P2)
  RVA=0x0A8A1890  token=0x60002CC  System.Void __Gen_Wrap_26(System.Object P0, Beyond.Resource.StringPathHash P1)
  RVA=0x066C0730  token=0x60002CD  System.Single __Gen_Wrap_27(System.Object P0)
  RVA=0x0A8A1990  token=0x60002CE  System.Int32 __Gen_Wrap_28(System.Object P0, System.Object P1, System.Single P2, System.Single P3)
  RVA=0x0A8A1AA8  token=0x60002CF  System.Int32 __Gen_Wrap_29(System.Object P0, Proto.CSMessageID P1, System.Single P2)
  RVA=0x0A8A1B9C  token=0x60002D0  System.Void __Gen_Wrap_30(Beyond.Network.NetResponse& P0)
  RVA=0x0A8A1C98  token=0x60002D1  Beyond.Network.NetBus.ResultHandler __Gen_Wrap_31(System.Object P0, System.Object P1, System.Single P2)
  RVA=0x0A8A1DAC  token=0x60002D2  Beyond.Network.NetBus.ResultHandler __Gen_Wrap_32(System.Object P0, System.Object P1, System.Boolean P2, System.Object P3, System.Object P4)
  RVA=0x066C288C  token=0x60002D3  System.Void __Gen_Wrap_33(System.Object P0, System.Single P1)
  RVA=0x0A8A1EF4  token=0x60002D4  System.Boolean __Gen_Wrap_34(System.Object P0, System.Object P1)
  RVA=0x06374CB0  token=0x60002D5  System.Void __Gen_Wrap_35(System.Int32 P0, System.Object P1)
  RVA=0x063625A4  token=0x60002D6  System.Void __Gen_Wrap_36()
  RVA=0x0A8A1FCC  token=0x60002D7  System.Boolean __Gen_Wrap_37(System.Span<System.Byte> P0, System.Object P1, System.Int32 P2, System.Int32 P3, System.Object P4, Beyond.Network.NetResponse& P5)
  RVA=0x0A8A2180  token=0x60002D8  System.Int32 __Gen_Wrap_38(System.Object P0, System.UInt64 P1, System.Int32 P2, System.Int32 P3, System.Object P4, System.Int32 P5)
  RVA=0x09C8B45C  token=0x60002D9  System.Void __Gen_Wrap_39(System.Type P0, System.Reflection.MethodInfo P1)
  RVA=0x0A8A22B4  token=0x60002DA  System.String __Gen_Wrap_40()
  RVA=0x0A8A2380  token=0x60002DB  System.String __Gen_Wrap_41(System.Object P0)
  RVA=0x0A8A2470  token=0x60002DC  System.String __Gen_Wrap_42(Beyond.Network.NetworkUrls.EUrlType P0)
  RVA=0x0A8A255C  token=0x60002DD  System.String __Gen_Wrap_43(System.Object P0, Beyond.Network.NetworkUrls.EUrlType P1)
  RVA=0x0A8A2668  token=0x60002DE  System.Void __Gen_Wrap_44(System.Object P0, System.Object P1, System.Int32 P2, System.Int32 P3, System.Int32 P4, System.Int32 P5)
  RVA=0x0A8A278C  token=0x60002DF  System.Threading.Tasks.Task __Gen_Wrap_45(System.Object P0, System.Object P1, System.Int32 P2, System.Int32 P3, System.Int32 P4, System.Int32 P5)
  RVA=0x06364290  token=0x60002E0  System.Void __Gen_Wrap_46(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x0A8A28E0  token=0x60002E1  System.Net.EndPoint __Gen_Wrap_47(System.Object P0)
  RVA=0x0A8A29BC  token=0x60002E2  System.Void __Gen_Wrap_48(System.Object P0, System.Boolean P1, System.Boolean P2)
  RVA=0x0A8A2AA0  token=0x60002E3  Beyond.Network.TcpIO.WeakNetDelayConfig __Gen_Wrap_49(System.Int32 P0, System.Int32 P1, System.Int32 P2)
  RVA=0x0A8A2BD8  token=0x60002E4  System.Void __Gen_Wrap_50(System.Int64& P0, System.Int32& P1)
  RVA=0x0A8A2CF8  token=0x60002E5  System.Void __Gen_Wrap_51(System.Object P0, System.Int32 P1, System.Int32 P2, System.Int32 P3, System.Int32 P4, System.Int32 P5, System.Int32 P6)
  RVA=0x066F3838  token=0x60002E6  System.Void __Gen_Wrap_52(System.Object P0, Beyond.Network.ENetWeakNetPreset P1)
  RVA=0x0A8A2E2C  token=0x60002E7  Beyond.Network.TcpIOProfilerSnapshot __Gen_Wrap_53(System.Object P0)
  RVA=0x0A8A2F40  token=0x60002E8  System.Int32 __Gen_Wrap_54(Beyond.Network.TcpIO.WeakNetDelayConfig P0)
  RVA=0x0A8A3048  token=0x60002E9  System.Void __Gen_Wrap_55(System.Object P0, Beyond.Network.TcpIO.WeakNetDelayConfig P1, System.Int64& P2, System.Int32& P3)
  RVA=0x0A8A31CC  token=0x60002EA  System.Void __Gen_Wrap_56(System.Object P0, System.Object P1, System.Int32 P2, System.Int32 P3)
  RVA=0x0A8A32D4  token=0x60002EB  System.Byte[] __Gen_Wrap_57(System.Object P0)
  RVA=0x0A8A33C4  token=0x60002EC  System.Int32 __Gen_Wrap_58(System.Object P0, System.Object P1, System.Int32 P2, System.Int32 P3)
  RVA=0x0A8A34D8  token=0x60002ED  Beyond.Network.HttpSender.Cancellation __Gen_Wrap_59(Beyond.Network.HttpSender.FSendFunc& P0)
  RVA=0x0A8A3688  token=0x60002EE  UnityEngine.Networking.UnityWebRequest __Gen_Wrap_60(System.Object P0, Beyond.Network.HttpSender.FGetOptions P1)
  RVA=0x0A8A37C0  token=0x60002EF  UnityEngine.Networking.UnityWebRequest __Gen_Wrap_61(System.Object P0, Beyond.Network.HttpSender.FPostOptions P1)
  RVA=0x0A8A38F8  token=0x60002F0  UnityEngine.Networking.UnityWebRequest __Gen_Wrap_62(Beyond.Network.HttpSender.FSendFunc& P0)
  RVA=0x0A8A3AA8  token=0x60002F1  System.Collections.IEnumerator __Gen_Wrap_63(System.Object P0, Beyond.Network.HttpSender.FSendFunc P1)
  RVA=0x0A8A3C04  token=0x60002F2  System.Collections.IEnumerator __Gen_Wrap_64(System.Object P0, Beyond.Network.HttpSender.FSendFunc P1, System.Object P2)
  RVA=0x0A8A3D78  token=0x60002F3  System.Collections.IEnumerator __Gen_Wrap_65(System.Object P0, Beyond.Network.HttpSender.FGetOptions P1, System.Object P2, System.Object P3)
  RVA=0x0A8A3ED8  token=0x60002F4  System.Collections.IEnumerator __Gen_Wrap_66(System.Object P0, Beyond.Network.HttpSender.FGetOptions P1, System.Object P2)
  RVA=0x0A8A4028  token=0x60002F5  System.Collections.IEnumerator __Gen_Wrap_67(System.Object P0, Beyond.Network.HttpSender.FPostOptions P1, System.Object P2, System.Object P3)
  RVA=0x0A8A4188  token=0x60002F6  System.Collections.IEnumerator __Gen_Wrap_68(System.Object P0, Beyond.Network.HttpSender.FPostOptions P1, System.Object P2)
  RVA=0x0A8A42C8  token=0x60002F7  System.Void __Gen_Wrap_69(System.Object P0, Beyond.Network.HttpSender.FSendFunc P1)
  RVA=0x0A8A4538  token=0x60002F8  Beyond.Network.HttpSender.Result __Gen_Wrap_70()
  RVA=0x0A8A45F0  token=0x60002F9  Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> __Gen_Wrap_71(Beyond.Network.HttpSender.FSendFunc P0)
  RVA=0x0A8A4720  token=0x60002FA  Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> __Gen_Wrap_72(System.Object P0, Beyond.Network.HttpSender.FPostOptions P1)
  RVA=0x0A8A4848  token=0x60002FB  Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> __Gen_Wrap_73(System.Object P0, Beyond.Network.HttpSender.FGetOptions P1)
  RVA=0x0A8A4970  token=0x60002FC  Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> __Gen_Wrap_74(System.Object P0)
  RVA=0x0A8A4A4C  token=0x60002FD  System.Int32 __Gen_Wrap_75(System.Object P0, System.Boolean& P1)
  RVA=0x0A8A4B58  token=0x60002FE  System.String __Gen_Wrap_76(Beyond.Network.HttpSender.FSendFunc& P0)
  RVA=0x06360E84  token=0x60002FF  Beyond.Network.EHGNetSessionReconnectState __Gen_Wrap_77(System.Object P0)
  RVA=0x09E3EDF8  token=0x6000300  System.Void __Gen_Wrap_78(System.Object P0, Beyond.Network.EHGNetSessionReconnectState P1)
  RVA=0x066F4838  token=0x6000301  System.Void __Gen_Wrap_79(System.Object P0, System.Object P1, Beyond.Network.ENetCloseReason P2)
  RVA=0x0A8A4E1C  token=0x6000302  System.Boolean __Gen_Wrap_80(System.Object P0, System.Object P1, System.Int32 P2, System.Boolean P3)
  RVA=0x066F3838  token=0x6000303  System.Void __Gen_Wrap_81(System.Object P0, Beyond.Network.ENetConnectErrorCode P1)
  RVA=0x066F3838  token=0x6000304  System.Void __Gen_Wrap_82(System.Object P0, Beyond.Network.ENetCloseReason P1)
  RVA=0x066C296C  token=0x6000305  System.Void __Gen_Wrap_83(System.Threading.Tasks.Task<System.Threading.Tasks.Task> P0)
  RVA=0x0A8A4F24  token=0x6000306  System.Boolean __Gen_Wrap_84(System.Object P0, System.Object P1, System.Int32 P2, System.Boolean P3, Beyond.Network.HGNetSessionConnectYield& P4)
  RVA=0x0A8A5094  token=0x6000307  System.Void __Gen_Wrap_85(System.Object P0, Beyond.Network.NetRequest P1)
  RVA=0x09CD5A30  token=0x6000308  System.Void __Gen_Wrap_86(System.Object P0, System.Int64 P1)
  RVA=0x0A8A51B0  token=0x6000309  Beyond.Network.INetIO __Gen_Wrap_87(System.Object P0)
  RVA=0x0A8A52A0  token=0x600030A  System.Threading.Tasks.Task<System.Boolean> __Gen_Wrap_88(System.Object P0, System.Object P1, System.Int32 P2)
  RVA=0x0A8A53C4  token=0x600030B  System.Threading.Tasks.Task<System.Boolean> __Gen_Wrap_89(System.Object P0, System.Object P1, System.Object P2, System.Int32 P3, System.Int32 P4, System.Int32 P5, System.Int32 P6)
  RVA=0x0A8A5618  token=0x600030C  System.Threading.Tasks.Task __Gen_Wrap_90(System.Object P0, System.Object P1)
  RVA=0x0A8A5724  token=0x600030D  System.Void __Gen_Wrap_91(System.Object P0, System.Object P1, System.Int64 P2, System.Int32 P3)
  RVA=0x0A8A582C  token=0x600030E  System.Boolean __Gen_Wrap_92(System.Object P0, System.Object P1, System.Int32 P2)
  RVA=0x0A8A5930  token=0x600030F  System.Threading.Tasks.Task __Gen_Wrap_93()
  RVA=0x0719F418  token=0x6000310  System.Void __Gen_Wrap_94(System.Object P0, System.Object P1, System.Object P2, System.Int32 P3)
  RVA=0x0A8A59E8  token=0x6000311  System.Void __Gen_Wrap_95(System.Object P0, System.Object P1, System.Int64 P2, System.Int64 P3)
  RVA=0x0A8A5AF0  token=0x6000312  System.Byte[] __Gen_Wrap_96()
  RVA=0x063621C4  token=0x6000313  Proto.CLIENT_PLATFORM_TYPE __Gen_Wrap_97()
  RVA=0x063621C4  token=0x6000314  Proto.ENV_TYPE __Gen_Wrap_98()
  RVA=0x063621C4  token=0x6000315  System.Int32 __Gen_Wrap_99()
  RVA=0x0A89F1B8  token=0x6000316  System.Void __Gen_Wrap_100(System.Object P0, System.Object P1, Beyond.Network.NetRequest& P2, System.Boolean P3)
  RVA=0x0A89F34C  token=0x6000317  System.Boolean __Gen_Wrap_101(System.Object P0, System.Object P1, Beyond.Network.NetResponse& P2, System.Boolean P3)
  RVA=0x0A89F4BC  token=0x6000318  System.Void __Gen_Wrap_102(System.Object P0, System.Int64 P1, System.Int32 P2)
  RVA=0x0A89F5B4  token=0x6000319  System.Byte[] __Gen_Wrap_103(System.Object P0, System.Object P1)
  RVA=0x07195DFC  token=0x600031A  System.Boolean __Gen_Wrap_104(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x0A89F6C0  token=0x600031B  Proto.MSG_A1 __Gen_Wrap_105(System.Object P0)
  RVA=0x0A89F7B0  token=0x600031C  Beyond.Network.HGNetSessionLoginYield __Gen_Wrap_106(System.Object P0, System.Object P1, System.Object P2, System.Int32 P3)
  RVA=0x0A89F8E4  token=0x600031D  System.Void __Gen_Wrap_107(System.Object P0, System.Object P1, Beyond.Network.NetRequest& P2)
  RVA=0x0A89FA68  token=0x600031E  System.Boolean __Gen_Wrap_108(System.Object P0, System.Object P1, Beyond.Network.NetResponse& P2)
  RVA=0x0A89FBCC  token=0x600031F  Beyond.Network.NetDevProfilerSnapshot __Gen_Wrap_109(System.Object P0)
  RVA=0x0A89FCF8  token=0x6000320  System.Boolean __Gen_Wrap_110(System.Object P0, Beyond.Network.NetResponse P1)
  RVA=0x071AEEA0  token=0x6000321  System.Void __Gen_Wrap_111(System.Object P0, System.Object P1, System.Int64 P2)
  RVA=0x06361FA4  token=0x6000322  System.Boolean __Gen_Wrap_112(Proto.CODE P0)
  RVA=0x0A89FE20  token=0x6000323  System.Boolean __Gen_Wrap_113(System.Object P0, System.Object P1, Beyond.Network.NetResponse P2)
  RVA=0x0A89FF60  token=0x6000324  System.Void __Gen_Wrap_114(System.Object P0, System.Object P1, Beyond.Network.NetResponse& P2)
  RVA=0x0A8A00E0  token=0x6000325  Proto.MSG_B1 __Gen_Wrap_115(System.Object P0)
  RVA=0x0A8A01D0  token=0x6000326  System.Boolean __Gen_Wrap_116(System.Object P0, Beyond.Log.FLogMessage& P1)
  RVA=0x0A8A0364  token=0x6000327  Beyond.Network.HGNetLogger.LogLevel __Gen_Wrap_117(Beyond.ELogLevel P0)
  RVA=0x0A8A0364  token=0x6000328  Beyond.Network.HGNetLogger.NativeLogLevel __Gen_Wrap_118(Beyond.ELogLevel P0)
  RVA=0x0A8A0364  token=0x6000329  System.Int32 __Gen_Wrap_119(Beyond.ELogChannel P0)
  RVA=0x069ACC64  token=0x600032A  System.Boolean __Gen_Wrap_120(Beyond.Network.HGNetLogger.NativeLogLevel P0, System.Int32 P1)
  RVA=0x06373694  token=0x600032B  System.Boolean __Gen_Wrap_121(Beyond.ELogLevel P0, Beyond.ELogChannel P1)
  RVA=0x0719F418  token=0x600032C  System.Void __Gen_Wrap_122(System.Object P0, System.Object P1, System.Object P2, UnityEngine.LogType P3)
  RVA=0x0A8A0434  token=0x600032D  Beyond.Network.HGNetLogger.HGNetLoggerClient __Gen_Wrap_123()
  RVA=0x0A8A0500  token=0x600032E  System.Void __Gen_Wrap_124(System.Object P0, Beyond.Network.HGNetLogger.LogMessage& P1)
  RVA=0x0A8A0628  token=0x600032F  System.Void __Gen_Wrap_125(System.Object P0, Beyond.Log.FLogMessage& P1)
  RVA=0x0A8A07AC  token=0x6000330  System.Void __Gen_Wrap_126(System.Object P0, System.UInt64 P1, System.UInt64 P2, System.UInt64 P3, System.UInt64 P4)
  RVA=0x066C1D88  token=0x6000331  System.Void __Gen_Wrap_127(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4)
  RVA=0x066DCC44  token=0x6000332  System.Void __Gen_Wrap_128(System.Boolean P0)
  RVA=0x06362A48  token=0x6000333  System.Void __Gen_Wrap_129(System.UInt64 P0)
  RVA=0x0A8A09AC  token=0x6000334  System.Collections.Generic.Dictionary<System.Type,Beyond.Network.HGNetLogger.MessageType> __Gen_Wrap_130()
  RVA=0x0A8A0A78  token=0x6000335  System.Collections.Generic.Dictionary<Beyond.Network.HGNetLogger.MessageType,System.Type> __Gen_Wrap_131()
  RVA=0x069AD848  token=0x6000336  System.Boolean __Gen_Wrap_132(System.Object P0, Beyond.Network.HGNetLogger.MessageType& P1)
  RVA=0x0A8A0B44  token=0x6000337  System.Void __Gen_Wrap_133(System.Object P0, System.Object P1, Beyond.Network.NetRequest P2)
  RVA=0x04D375D0  token=0x6000338  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000069
SIZE:  0x60
EXTENDS: IFix.Core.AnonymousStorey
IMPLEMENTS: System.IDisposable System.Collections.IEnumerator System.Collections.Generic.IEnumerator`1 System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  private           System.Int32                    methodId_0  // 0x40
  private           System.Int32                    methodId_1  // 0x44
  private           System.Int32                    methodId_2  // 0x48
  private           System.Int32                    methodId_3  // 0x4c
  private           System.Int32                    methodId_4  // 0x50
  private           System.Int32                    methodId_5  // 0x54
  private           System.Int32                    methodId_6  // 0x58
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0A8A605C
  System.Collections.IEnumerator.Current  get=0x0A8A6108
METHODS:
  RVA=0x06365FC0  token=0x6000339  System.Void System.IDisposable.Dispose()
  RVA=0x06365D60  token=0x600033A  System.Boolean MoveNext()
  RVA=0x06365E98  token=0x600033C  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0A8A5D4C  token=0x600033E  System.Void MoveNext()
  RVA=0x0A8A5FAC  token=0x600033F  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine P0)
  RVA=0x0A8A61B4  token=0x6000340  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0A8A5E70  token=0x6000341  System.Void RefAwaitUnsafeOnCompleteMethod()
  RVA=0x0A8A5DDC  token=0x6000342  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x200006A
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000343  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0A8A6490  token=0x6000344  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x0314AF50  token=0x6000345  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0A8A634C  token=0x6000346  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0A8A6418  token=0x6000347  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0A8A64F4  token=0x6000348  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0A8A62A8  token=0x6000349  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x200006B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-get_Client0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-set_Client0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-get_Active0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-set_Active0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-get_Available0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-get_Connected0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-get_ExclusiveAddressUse0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-set_ExclusiveAddressUse0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-Connect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-Connect1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-Connect2  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-Connect3  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-BeginConnect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-BeginConnect1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-BeginConnect2  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-EndConnect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-ConnectAsync0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-ConnectAsync1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-ConnectAsync2  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-GetStream0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-Close0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-Dispose1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-_NumericOption0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-get_ReceiveBufferSize0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-set_ReceiveBufferSize0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-get_SendBufferSize0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-set_SendBufferSize0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-get_ReceiveTimeout0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-set_ReceiveTimeout0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-get_SendTimeout0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-set_SendTimeout0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-get_LingerState0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-set_LingerState0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-get_NoDelay0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-set_NoDelay0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGTcpClient-_Initialize0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetResponse-get_logHead0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetResponse-set_logHead0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetResponse-get_csHead0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetResponse-set_csHead0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetResponse-get_msgId0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetResponse-get_csSeqId0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetResponse-get_scSeqId0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-get_currentSessionID0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-Setup0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-get_isRunning0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-_ValidateSession0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-IncrementCSSeqID0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-SendMessage0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-Send0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetUtil-GetSCMsgTypeById0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-RegisterMessageHandler0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-UnRegisterMessageHandler0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-OnNetClientShutDown0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-_SendWithResult0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-_Listen0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-_UnListen0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-_UnListenWithSeqNum0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-ResultHandler-SetIgnoreSeq0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-ResultHandler-_CheckSessionIDValid0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-ResultHandler-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-ResultHandler-_HandleMessage0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-ResultHandler-OnResponse0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-Sender-DoSend0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-Sender-DoListen0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetBus-Sender-DoUnListen0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-Special2BusSender-get_isBusBusy0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-DisposableBusSender-Send0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-Special2BusSender-Send0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-DisposableBusSender-_Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-DisposableBusSender-OnNetShutDown0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-Special2BusSender-_ClearSendTime0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-Special2BusSender-OnNetShutDown0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-DisposableBusSender-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-Special2BusSender-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-InteractiveBusSender-get_isBusBusy0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMaskConfigHolder-_LoadJsonConfig0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMaskConfigHolder-_LoadScriptableObjectConfig0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMaskConfigHolder-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMaskConfigHolder-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-MaskInfo-get_endTime0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-MaskInfo-get_showDecoTime0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-_UpdateMaskState0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-RemoveMask0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-UIBusSender-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetUtil-GetCSMsgIdByType0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-_GetKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-AddMask0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-AddMask1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-UIBusSender-Send0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-UIBusSender-OnNetShutDown0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TeleportMaskSender-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TeleportMaskSender-Send0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TeleportMaskSender-OnNetShutDown0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-FrequencyControlBusSender-Send0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-FrequencyControlBusSender-_TrySendWaitingMsg0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-FrequencyControlBusSender-Send1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-FrequencyControlBusSender-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-ServerConfig-IsSame0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetUtil-RegisterSCMessage0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetUtil-RegisterCSMessage0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetUtil-GetSCMsgIdByType0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetUtil-FastRegisterMessage0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetUtil-GetNetMessageFromDataBytes0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetUtil-SerializeHeadMessage0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetUtil-AutoRegisterMessageHandlers0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetUtil-_ForeachMessageHandlers0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetUtil-AutoUnRegisterMessageHandlers0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-PreTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-_CreateTickFunction0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-_OnSwitchLanguage0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-_ClearTickFunction0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-OnUnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-Release0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-ListenOnNetMaskChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkMask-UnListenOnNetMaskChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkUrls-RemoteConfigUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkUrls-ResourceRootUrlDev0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkUrls-_GetUrlFromLocal0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkUrls-ServerListUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkUrls-_SelectUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkUrls-AgeTipsUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkUrls-GovUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-NetworkUrls-LauncherUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-IsConnected0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-Close0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-Connect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-ConnectAsync0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-IsAuthed0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-MarkAuthed0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-EnableCryptStream0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-GetEndPoint0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-TestCloseNetIO0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-TestDropNetIO0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-_CreateWeakNetDelayConfig0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-_ResetWeakNetWindow0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-SetWeakNetCustom0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-SetWeakNetPreset0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-GetProfilerSnapshot0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-_NextDelayMs0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-_ApplyWeakNetDelay0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-WriteData0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-WriteCryptoData0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-GetSendHeadBuffer0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-GetSendBuffer0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-GetReceiveBuffer0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-ClearReceiveBufferOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-GetReceiveBufferOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-SkipReceiveBufferOffset0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-ReadData0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-ReadCryptoData0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-Available0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-TcpIO-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-FSendFunc-GetCancellation0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-_CreateWebRequestGET0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-_CreateWebRequestPOST0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-FSendFunc-Invoke0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-Cancellation-IsCanceled0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-_SetHttpResult0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-_MarkResultAsException0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-_YieldSendHttpRequest0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-_DeserializeTypedResult0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-_YieldSendHttpRequest1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-YieldSendGet0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-YieldSendGet1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-YieldSendPost0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-YieldSendPost1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-_WaitForServiceDone0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-_StartWebRequestTask0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-_WaitForSendHttpRequest0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-WaitForSendPost0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-WaitForSendGet0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-WaitForSendGet1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-GetGeneralErrorCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-_DebugOnlyProfileResult0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-Cancellation-Cancel0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HttpSender-FSendFunc-GetOptMethod0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-get_reconnectState0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-set_reconnectState0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-get_isConnecting0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-get_isRunningAndConnected0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-get_isReconnecting0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-get_closeErrorCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-get_isAutoReconnect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-get_maxRecycleQueueCount0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-_ApplyWeakNetConfigToTcpIO0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-_DNSCheck0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-RemoveAutoReconnect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-CloseTCPMsgThreadTask0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-InternalClose0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-InternalClose1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-Connect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionConnectYield-SetFinish0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-Close0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-OnConnectAsyncEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-ConnectAsync0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-EnqueueSendMsg0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-GetSendQueueMsgCount0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-SetCSSeqID0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-GetCSSeqID0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-GetIO0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-TryCloseTcpIOWithTimeout0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-TryConnectWithTimeout0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-_DoReconnectAsync0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-_ReconnectCheckerTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-SessionCheckSignalAndSleep0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-NetStatusChecker_Receive0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-_ReadAndCheckMessageInSessionThread0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-NetStatusChecker_Sender0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-_SendAndCheckMessage0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-_SessionThread0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-StartTCPMsgLoopThreadTask0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-OpenAutoReconnect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-SetWeakNetPreset0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetBaseSession-SetWeakNetCustom0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-SetAutoReconnectToken0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-SessionConnectTimeoutCheckerTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-GetClientPublicKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_GetOnlineResVersion0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_GetPlatformID0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_GetSDKEnv0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_GetPayPlatform0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_CSLoginHelper_UpdateAppInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_WriteMessageInSessionThread0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_ReadMessageInSessionThread0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_ApproximateLoginServerTime0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_RSADecrypt0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_HandleLoginEncryp0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-DoReconnectLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-OnApplicationPause0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_FlushMergeQueueToSendQueue0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-GetRequest0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-LoginErrorInfo-SetErrorConnectError0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-SetConnectErrorError0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-LoginErrorInfo-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-SetSucceed0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-LoginErrorInfo-Set0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-SetNtfError0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-LoginErrorInfo-SetErrorConnectTimeOut0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-SetTimeOutError0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_SessionLoginThreadTask0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-LoginAsync0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-StartMsgLoop0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-OnConnectedSucceed0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-OnReconnectTimesOver0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-NeedKeepConnect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-CheckAndWriteReqMsg0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-ReadMessageInSessionThread0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-ClearRecvQueue0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-ClearSendQueue0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-GetNetDevProfilerSnapshot0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-GetPingPassedTime0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-GetPing0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-IsPingInProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-ClearPingWDG0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_HandleSpecialMessagePing0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-IsPingOut0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-PingSenderTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-InitClientRSAKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-ClearRSAKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionUtils-CheckErrorNeedsDisconnect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_HandleSpecialMessageErrorCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_HandleSpecialMessageReconnectIncr0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_HandleSpecialMessageReconnectFull0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-CheckAndDealRespMessages0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSession-_HandleSpecialMessageReconnectLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-GetResponse0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-GetErrorCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-GetErrorDetails0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-get_isSucceed0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-get_keepWaiting0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionLoginYield-LoginErrorInfo-SetErrorInvaild0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionConnectYield-GetCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionConnectYield-get_isSucceed0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetSessionConnectYield-get_keepWaiting0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-get_isRunning0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-get_isConnecting0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-get_isRunningAndConnected0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-get_closeErrorCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-get_isAutoReconnect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-_DNSCheck0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-RemoveAutoReconnect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-CloseTCPMsgThreadTask0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-Close0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-Close1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-Connect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-OnConnectAsyncEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-ConnectAsync0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-EnqueueSendMsg0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-GetSendQueueMsgCount0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-IncrementCSSeqID0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-SetCSSeqID0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-GetCSSeqID0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-GetIO0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-_ReconnectCheckerTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-SessionCheckSignalAndSleep0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-NetStatusChecker_Receive0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-NetStatusChecker_Sender0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-_SessionThread0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-StartTCPMsgLoopThreadTask0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogBaseSession-OpenAutoReconnect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogger-CheckLog0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogger-ConvertFromDLogger0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogger-ConvertFromDLoggerV20  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogger-ConvertFromDLogger1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerCloudSettings-ConvertFromDLogger0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerCloudSettings-IsNetLogLevelEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerCloudSettings-IsNetLogLevelEnabled1  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerClient-_FlushLocalLogQueue0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerClient-AddLog0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerClient-_CloseAndClear0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerClient-ShutDown0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerClient-get_instance0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-SendMessage0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerClient-WriteNetLog0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogger-Log0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogger-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLogger-SetLogChannel0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-get_uid0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-get_roleId0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-SetAutoReconnectToken0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-SetReconnectSuccessCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerClient-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-OnApplicationPause0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerClient-OnApplicationPause0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerClient-SetRoleID0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerClient-SetNetLogMaskFromServer0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerCloudSettings-SetCloudLogControl0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerCloudSettings-IsLocalLogLevelEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-get_s_messageType2Id0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-get_s_messageId2Type0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-TryGetMsgTypeByID0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-_WriteMessageInSessionThread0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-SendLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-OnConnectedSucceed0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-ReadMessageInSessionThread0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-CheckAndDealRespMessages0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-CheckAndWriteReqMsg0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-NeedKeepConnect0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-DoReconnectLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-_GetConnectTimeoutTime0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-SessionConnectTimeoutCheckerTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-ClearPingWDG0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-IsPingOut0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-PingSenderTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-InitClientRSAKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-ClearRSAKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-ClearRecvQueue0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-ClearSendQueue0  // const
  public    static  IFix.IDMAP0                     Beyond-Network-HGNetLogger-HGNetLoggerSession-OnReconnectTimesOver0  // const
METHODS:
END_CLASS

