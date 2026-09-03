// ========================================================
// Dumped by @desirepro
// Assembly: Hypergryph.Browser.dll
// Classes:  43
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

CLASS: BulletinEventData
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x20
FIELDS:
  public            System.Int32                    eventCode  // 0x10
  public            Newtonsoft.Json.Linq.JObject    data  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000015  System.Void .ctor()
END_CLASS

CLASS: RedDotEventData
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x20
FIELDS:
  public            System.String                   target  // 0x10
  public            System.Boolean                  showRedDot  // 0x18
  public            System.Boolean                  showPopup  // 0x19
METHODS:
  RVA=0x041E1670  token=0x6000016  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x28
FIELDS:
  public            System.Action<System.String>    eventListener  // 0x10
  public            System.Action<System.String>    onResult  // 0x18
  public            System.Action<Hypergryph.CloudClientBridge.CloudCallResult>eventCallback  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000017  System.Void .ctor()
  RVA=0x09DD1234  token=0x6000018  System.Void <Open>b__0(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DD1438  token=0x6000019  System.Void <Open>b__1(Hypergryph.CloudClientBridge.CloudCallResult result)
END_CLASS

CLASS: OpenCallback
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679B0C8  token=0x6000027  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000028  System.Void Invoke(System.String jsonData)
END_CLASS

CLASS: EventCallback
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x049CB4E0  token=0x6000029  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600002A  System.Void Invoke(System.String jsonData)
END_CLASS

CLASS: EventListener
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679B0C8  token=0x600002B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600002C  System.Void Invoke(System.String jsonData)
END_CLASS

CLASS: StatusCode
TYPE:  static class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000012
SIZE:  0x18
FIELDS:
  public            Hypergryph.SDK.IHGBrowserSDKCallbackcallback  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000036  System.Void .ctor()
  RVA=0x09DD0AC0  token=0x6000037  System.Void <Open>b__0(System.String data)
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x2000014
SIZE:  0x18
FIELDS:
  public            System.Action<System.String>    onResult  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600003C  System.Void .ctor()
  RVA=0x09DD0884  token=0x600003D  System.Void <Open>b__0(Hypergryph.CloudClientBridge.CloudCallResult result)
END_CLASS

CLASS: ExtraInfoCallback
TYPE:  sealed class
TOKEN: 0x2000016
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679B0C8  token=0x6000047  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000048  System.Void Invoke(System.String jsonData)
END_CLASS

CLASS: <>c__DisplayClass8_0
TYPE:  sealed class
TOKEN: 0x2000020
SIZE:  0x18
FIELDS:
  public            System.Action<Hypergryph.SDK.SurveyStateResult>onResult  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600005F  System.Void .ctor()
  RVA=0x09DD1924  token=0x6000060  System.Void <GetSurveyStatus>b__0(System.String jsonData)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000022
SIZE:  0x28
FIELDS:
  public            System.Action<System.String>    eventListener  // 0x10
  public            System.Action<System.String>    onResult  // 0x18
  public            System.Action<Hypergryph.CloudClientBridge.CloudCallResult>eventCallback  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000068  System.Void .ctor()
  RVA=0x09DD0B0C  token=0x6000069  System.Void <Open>b__0(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DD0D10  token=0x600006A  System.Void <Open>b__1(Hypergryph.CloudClientBridge.CloudCallResult result)
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x2000023
SIZE:  0x18
FIELDS:
  public            System.Action<Hypergryph.SDK.HGWebPortalState>onResult  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600006B  System.Void .ctor()
  RVA=0x09DD0FB0  token=0x600006C  System.Void <GetWebPortalState>b__0(Hypergryph.CloudClientBridge.CloudCallResult result)
END_CLASS

CLASS: <>c__DisplayClass8_0
TYPE:  sealed class
TOKEN: 0x2000024
SIZE:  0x18
FIELDS:
  public            System.Action<System.String>    onResult  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600006D  System.Void .ctor()
  RVA=0x09DD16D8  token=0x600006E  System.Void <GetSurveyStatus>b__0(Hypergryph.CloudClientBridge.CloudCallResult result)
END_CLASS

CLASS: <>c__DisplayClass9_0
TYPE:  sealed class
TOKEN: 0x2000025
SIZE:  0x18
FIELDS:
  public            System.Action<System.String>    onResult  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600006F  System.Void .ctor()
  RVA=0x09DD19C0  token=0x6000070  System.Void <OpenBrowser>b__0(Hypergryph.CloudClientBridge.CloudCallResult result)
END_CLASS

CLASS: OpenCallback
TYPE:  sealed class
TOKEN: 0x2000027
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679B0C8  token=0x6000080  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000081  System.Void Invoke(System.String jsonData)
END_CLASS

CLASS: EventListener
TYPE:  sealed class
TOKEN: 0x2000028
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679B0C8  token=0x6000082  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000083  System.Void Invoke(System.String jsonData)
END_CLASS

CLASS: OpenBrowserCallback
TYPE:  sealed class
TOKEN: 0x2000029
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679B0C8  token=0x6000084  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000085  System.Void Invoke(System.String jsonData)
END_CLASS

CLASS: WebPortalStateCallback
TYPE:  sealed class
TOKEN: 0x200002A
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x049CB4E0  token=0x6000086  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000087  System.Void Invoke(System.String jsonData)
END_CLASS

CLASS: WebProtalSurveyStatusCallback
TYPE:  sealed class
TOKEN: 0x200002B
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679B0C8  token=0x6000088  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000089  System.Void Invoke(System.String jsonData)
END_CLASS

CLASS: Hypergryph.SDK.IHGBulletinSDK
TYPE:  interface
TOKEN: 0x2000002
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000001  System.Void Open(System.String type, System.Action<System.String> onResult, System.Action<System.String> eventListener)
  RVA=-1  // abstract  token=0x6000002  System.Void Close()
  RVA=-1  // abstract  token=0x6000003  System.String GetRedDotState(System.String type)
  RVA=-1  // abstract  token=0x6000004  System.Void RegisterBulletinEventListener(System.Action<System.String> eventListener)
END_CLASS

CLASS: Hypergryph.SDK.HGBulletinSDK
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x10
FIELDS:
  private   static  Hypergryph.SDK.IHGBulletinSDK   s_bulletin  // static @ 0x0
PROPERTIES:
  bulletinSdk  get=0x040BD300
METHODS:
  RVA=0x09DCE324  token=0x6000006  System.Void Open(System.String type, System.Action<System.String> onResult, System.Action<System.String> eventListener)
  RVA=0x09DCE2C0  token=0x6000007  System.Void Close()
  RVA=0x040BD600  token=0x6000008  System.String GetRedDotState(System.String type)
  RVA=0x040BD2A0  token=0x6000009  System.Void RegisterBulletinEventListener(System.Action<System.String> eventListener)
END_CLASS

CLASS: Hypergryph.SDK.HGBulletinSDKPluginCloud
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x10
IMPLEMENTS: Hypergryph.SDK.IHGBulletinSDK
FIELDS:
  private   static  System.Action<System.String>    outerEventCallback  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<System.String,Hypergryph.SDK.HGBulletinSDKPluginCloud.RedDotEventData>_redDotStateDict  // static @ 0x8
METHODS:
  RVA=0x09DCDD78  token=0x600000A  System.Void .ctor()
  RVA=0x09DCCF34  token=0x600000B  System.Void Finalize()
  RVA=0x09DCD18C  token=0x600000C  System.Void InnerEventCallBack(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DCD534  token=0x600000D  System.Void Open(System.String type, System.Action<System.String> onResult, System.Action<System.String> eventListener)
  RVA=0x09DCCA88  token=0x600000E  System.Void Close()
  RVA=0x09DCD070  token=0x600000F  System.String GetRedDotState(System.String type)
  RVA=0x09DCDA94  token=0x6000010  System.Void RegisterBulletinEventListener(System.Action<System.String> eventListener)
  RVA=0x09DCD424  token=0x6000011  System.Void NotifySDKInstantiated()
  RVA=0x09DCDB20  token=0x6000012  System.Void UpdateRedDotState(System.String target, Hypergryph.SDK.HGBulletinSDKPluginCloud.RedDotEventData data)
  RVA=0x09DCCCC8  token=0x6000013  System.String CreateRedDotStateJson(System.String type, System.Boolean showRedDot, System.Boolean showPopup)
  RVA=0x09DCDCE4  token=0x6000014  System.Void .cctor()
END_CLASS

CLASS: Hypergryph.SDK.HGBulletinSDKPluginWindows
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x10
IMPLEMENTS: Hypergryph.SDK.IHGBulletinSDK
FIELDS:
  private   static  System.Action<System.String>    outerOpenCallback  // static @ 0x0
  private   static  System.Action<System.String>    outerEventCallback  // static @ 0x8
  private   static  System.Action<System.String>    outerEventListener  // static @ 0x10
METHODS:
  RVA=0x09DCDECC  token=0x600001A  System.Void BulletinSDK_OpenBulletin(System.String type, Hypergryph.SDK.HGBulletinSDKPluginWindows.OpenCallback callback, Hypergryph.SDK.HGBulletinSDKPluginWindows.EventListener eventListener)
  RVA=0x09DCDE64  token=0x600001B  System.Void BulletinSDK_CloseBulletin()
  RVA=0x040BD5A0  token=0x600001C  System.IntPtr BulletinSDK_GetRedDotState(System.String type)
  RVA=0x03827010  token=0x600001D  System.Void BulletinSDK_RegisterBulletinEventListener(Hypergryph.SDK.HGBulletinSDKPluginWindows.EventCallback callback)
  RVA=0x09DCE094  token=0x600001E  System.Void InnerOpenCallBack(System.String jsonData)
  RVA=0x09DCDFF4  token=0x600001F  System.Void InnerEventCallBack(System.String jsonData)
  RVA=0x09DCE044  token=0x6000020  System.Void InnerEventListener(System.String jsonData)
  RVA=0x040BD1E0  token=0x6000021  System.Void .ctor()
  RVA=0x09DCC5E8  token=0x6000022  System.Void Finalize()
  RVA=0x09DCE130  token=0x6000023  System.Void Open(System.String type, System.Action<System.String> onResult, System.Action<System.String> eventListener)
  RVA=0x09DCDF80  token=0x6000024  System.Void Close()
  RVA=0x040BD460  token=0x6000025  System.String GetRedDotState(System.String type)
  RVA=0x04A78730  token=0x6000026  System.Void RegisterBulletinEventListener(System.Action<System.String> eventListener)
END_CLASS

CLASS: Hypergryph.SDK.CloudGameUtil
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x040BCB50  token=0x600002D  System.Boolean IsCloudGame()
END_CLASS

CLASS: Hypergryph.SDK.HGBroserSDKConstants
TYPE:  static class
TOKEN: 0x200000D
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Hypergryph.SDK.IHGBrowserSDKCallback
TYPE:  interface
TOKEN: 0x200000F
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600002E  System.Void onBrowserEvent(System.String data)
END_CLASS

CLASS: Hypergryph.SDK.IHGBrowserSDK
TYPE:  interface
TOKEN: 0x2000010
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600002F  System.Void Open(System.String url, System.String styleConfig, System.Action<System.String> onResult)
  RVA=-1  // abstract  token=0x6000030  System.Void Close()
  RVA=-1  // abstract  token=0x6000031  System.Void SetAudioConfig(System.String config)
END_CLASS

CLASS: Hypergryph.SDK.HGBrowserSDK
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x10
FIELDS:
  private   static  Hypergryph.SDK.IHGBrowserSDK    s_browser  // static @ 0x0
PROPERTIES:
  browserSDK  get=0x040BCEE0
METHODS:
  RVA=0x09DCC954  token=0x6000033  System.Void Open(System.String url, System.String styleConfig, Hypergryph.SDK.IHGBrowserSDKCallback callback)
  RVA=0x09DCC8F0  token=0x6000034  System.Void Close()
  RVA=0x040BCE80  token=0x6000035  System.Void SetAudioConfig(System.String config)
END_CLASS

CLASS: Hypergryph.SDK.HGBrowserSDKPluginCloud
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x10
IMPLEMENTS: Hypergryph.SDK.IHGBrowserSDK
FIELDS:
METHODS:
  RVA=0x09DCBE7C  token=0x6000038  System.Void Open(System.String url, System.String styleConfig, System.Action<System.String> onResult)
  RVA=0x09DCBC3C  token=0x6000039  System.Void Close()
  RVA=0x09DCC2D0  token=0x600003A  System.Void SetAudioConfig(System.String config)
  RVA=0x041E1670  token=0x600003B  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.SDK.HGBrowserSDKPluginWindows
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x10
IMPLEMENTS: Hypergryph.SDK.IHGBrowserSDK
FIELDS:
  private   static  System.Action<System.String>    outerCallback  // static @ 0x0
METHODS:
  RVA=0x09DCC70C  token=0x600003E  System.Void MiniWebViewSDKLoad(System.String url, System.String styleConfig, Hypergryph.SDK.HGBrowserSDKPluginWindows.ExtraInfoCallback callback)
  RVA=0x09DCC6E0  token=0x600003F  System.Void MiniWebViewSDKClose()
  RVA=0x03827900  token=0x6000040  System.Void WebViewSDKSetAudioConfig(System.String config)
  RVA=0x09DCC66C  token=0x6000041  System.Void InnerCallBack(System.String jsonData)
  RVA=0x040BD1E0  token=0x6000042  System.Void .ctor()
  RVA=0x09DCC5E8  token=0x6000043  System.Void Finalize()
  RVA=0x09DCC7CC  token=0x6000044  System.Void Open(System.String url, System.String styleConfig, System.Action<System.String> onResult)
  RVA=0x09DCC574  token=0x6000045  System.Void Close()
  RVA=0x040BCE60  token=0x6000046  System.Void SetAudioConfig(System.String config)
END_CLASS

CLASS: Hypergryph.SDK.HGBrowerSDKTickEvent
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x03827D80  token=0x6000049  System.Void Update()
  RVA=0x03827D80  token=0x600004A  System.Void WebViewSDKTickEvent()
  RVA=0x05393520  token=0x600004B  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.SDK.HGBrowerSDKTickEventManager
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x20
FIELDS:
  private           UnityEngine.GameObject          tickGameObject  // 0x10
  private           Hypergryph.SDK.HGBrowerSDKTickEventtickEvent  // 0x18
  private   static  Hypergryph.SDK.HGBrowerSDKTickEventManagerinstanceMangaer  // static @ 0x0
PROPERTIES:
  InstanceMangaer  get=0x040BD210
METHODS:
  RVA=0x04A3C610  token=0x600004D  System.Void InitTickEvent()
  RVA=0x09DCBBB0  token=0x600004E  System.Void UnInitTickEvent()
  RVA=0x041E1670  token=0x600004F  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.SDK.HGBrowserSDKVersion
TYPE:  static class
TOKEN: 0x2000019
SIZE:  0x10
FIELDS:
  public    static readonly System.String                   VERSION  // static @ 0x0
METHODS:
  RVA=0x04D506E0  token=0x6000050  System.Void .cctor()
END_CLASS

CLASS: Hypergryph.SDK.HGWebPortalState
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x18
FIELDS:
  public            System.Int32                    status  // 0x10
  public            System.Boolean                  redDot  // 0x14
METHODS:
  RVA=0x041E1670  token=0x6000051  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.SDK.HGWebPortalMetadata
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000052  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.SDK.SurveyState
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x20
FIELDS:
  public            System.String                   surveyId  // 0x10
  public            System.Boolean                  isAnswered  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000053  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.SDK.SurveyStateResult
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x20
FIELDS:
  public            System.Int32                    status  // 0x10
  public            Hypergryph.SDK.SurveyState[]    stateList  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000054  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.SDK.IHGWebPortalSDK
TYPE:  interface
TOKEN: 0x200001E
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000055  System.Void Open(System.String target, System.String urlParams, System.Action<System.String> onResult, System.Action<System.String> eventListener)
  RVA=-1  // abstract  token=0x6000056  System.Void OpenBrowser(System.String url, System.String styleConfig, System.Action<System.String> onResult)
  RVA=-1  // abstract  token=0x6000057  System.Void GetWebPortalState(System.String target, System.Action<Hypergryph.SDK.HGWebPortalState> onResult)
  RVA=-1  // abstract  token=0x6000058  System.Void GetSurveyStatus(System.String surveyIds, System.Action<System.String> onResult)
END_CLASS

CLASS: Hypergryph.SDK.HGWebPortalSDK
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x10
FIELDS:
  private   static  Hypergryph.SDK.IHGWebPortalSDK  s_webportal  // static @ 0x0
PROPERTIES:
  webportalSdk  get=0x040BD090
METHODS:
  RVA=0x09DD003C  token=0x600005A  System.Void Open(System.String target, System.String urlParams, System.Action<System.String> onResult, System.Action<System.String> eventListener)
  RVA=0x09DCFF90  token=0x600005B  System.Void OpenBrowser(System.String url, System.String styleConfig, System.Action<System.String> onResult)
  RVA=0x040BD030  token=0x600005C  System.Void GetWebPortalState(System.String target, System.Action<Hypergryph.SDK.HGWebPortalState> onResult)
  RVA=0x09DCFE78  token=0x600005D  System.Void GetSurveyStatus(System.String surveyIds, System.Action<Hypergryph.SDK.SurveyStateResult> onResult)
  RVA=0x09DD00F0  token=0x600005E  Hypergryph.SDK.SurveyStateResult ParseSurveyStateResult(System.String jsonData)
END_CLASS

CLASS: Hypergryph.SDK.HGWebPortalSDKPluginCloud
TYPE:  class
TOKEN: 0x2000021
SIZE:  0x10
IMPLEMENTS: Hypergryph.SDK.IHGWebPortalSDK
FIELDS:
  private   static  System.Action<System.String>    outerSchemeJumpCallback  // static @ 0x0
METHODS:
  RVA=0x09DCF548  token=0x6000061  System.Void .ctor()
  RVA=0x09DCE3C4  token=0x6000062  System.Void Finalize()
  RVA=0x09DCEB50  token=0x6000063  System.Void InnerSchemeJumpCallBack(Hypergryph.CloudClientBridge.CloudCallResult result)
  RVA=0x09DCEFBC  token=0x6000064  System.Void Open(System.String target, System.String urlParams, System.Action<System.String> onResult, System.Action<System.String> eventListener)
  RVA=0x09DCE828  token=0x6000065  System.Void GetWebPortalState(System.String target, System.Action<Hypergryph.SDK.HGWebPortalState> onResult)
  RVA=0x09DCE500  token=0x6000066  System.Void GetSurveyStatus(System.String surveyIds, System.Action<System.String> onResult)
  RVA=0x09DCEC74  token=0x6000067  System.Void OpenBrowser(System.String url, System.String styleConfig, System.Action<System.String> onResult)
END_CLASS

CLASS: Hypergryph.SDK.HGWebPortalSDKPluginWindows
TYPE:  class
TOKEN: 0x2000026
SIZE:  0x10
IMPLEMENTS: Hypergryph.SDK.IHGWebPortalSDK
FIELDS:
  private   static  System.Action<System.String>    outerOpenCallback  // static @ 0x0
  private   static  System.Action<System.String>    outerEventListener  // static @ 0x8
  private   static  System.Action<System.String>    outerOpenBrowserCallback  // static @ 0x10
  private   static  System.Action<Hypergryph.SDK.HGWebPortalState>outerWebPortalStateCallback  // static @ 0x18
  private   static  System.Action<System.String>    outerWebPortalSurveyStatusCallback  // static @ 0x20
METHODS:
  RVA=0x09DCFDA0  token=0x6000071  System.Void WebPortalSDK_Open(System.String target, System.String urlParams, Hypergryph.SDK.HGWebPortalSDKPluginWindows.OpenCallback callback, Hypergryph.SDK.HGWebPortalSDKPluginWindows.EventListener eventListener)
  RVA=0x09DCFCE4  token=0x6000072  System.Void WebPortalSDK_OpenBrowser(System.String url, System.String styleConfig, Hypergryph.SDK.HGWebPortalSDKPluginWindows.OpenBrowserCallback callback)
  RVA=0x03826F60  token=0x6000073  System.Void WebPortalSDK_GetWebPortalState(System.String target, Hypergryph.SDK.HGWebPortalSDKPluginWindows.WebPortalStateCallback callback)
  RVA=0x09DCFC3C  token=0x6000074  System.IntPtr WebPortalSDK_GetSurveyStatus(System.String surveyIds, Hypergryph.SDK.HGWebPortalSDKPluginWindows.WebProtalSurveyStatusCallback callback)
  RVA=0x09DCF854  token=0x6000075  System.Void InnerOpenCallBack(System.String jsonData)
  RVA=0x09DCF788  token=0x6000076  System.Void InnerEventListener(System.String jsonData)
  RVA=0x09DCF7D8  token=0x6000077  System.Void InnerOpenBrowserCallback(System.String jsonData)
  RVA=0x04B6B510  token=0x6000078  System.Void InnerWebPortalStateCallback(System.String jsonData)
  RVA=0x09DCF8F0  token=0x6000079  System.Void InnerWebPortaSurveyStatusCallback(System.String jsonData)
  RVA=0x040BD1E0  token=0x600007A  System.Void .ctor()
  RVA=0x09DCC5E8  token=0x600007B  System.Void Finalize()
  RVA=0x09DCFA94  token=0x600007C  System.Void Open(System.String target, System.String urlParams, System.Action<System.String> onResult, System.Action<System.String> eventListener)
  RVA=0x09DCF96C  token=0x600007D  System.Void OpenBrowser(System.String url, System.String styleConfig, System.Action<System.String> onResult)
  RVA=0x04A425F0  token=0x600007E  System.Void GetWebPortalState(System.String target, System.Action<Hypergryph.SDK.HGWebPortalState> onResult)
  RVA=0x09DCF620  token=0x600007F  System.Void GetSurveyStatus(System.String surveyIds, System.Action<System.String> onResult)
END_CLASS

