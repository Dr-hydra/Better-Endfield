// ========================================================
// Dumped by @desirepro
// Assembly: Hypergryph.Browser.dll
// Classes:  43
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000005  // size: 0x20
    public class BulletinEventData
    {
        // Fields
        public System.Int32 eventCode;  // 0x10
        public Newtonsoft.Json.Linq.JObject data;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000015
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x20
    public class RedDotEventData
    {
        // Fields
        public System.String target;  // 0x10
        public System.Boolean showRedDot;  // 0x18
        public System.Boolean showPopup;  // 0x19

        // Methods
        // RVA: 0x041E1670  token: 0x6000016
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x28
    public sealed class <>c__DisplayClass7_0
    {
        // Fields
        public System.Action<System.String> eventListener;  // 0x10
        public System.Action<System.String> onResult;  // 0x18
        public System.Action<Hypergryph.CloudClientBridge.CloudCallResult> eventCallback;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000017
        public System.Void .ctor() { }
        // RVA: 0x09DD1234  token: 0x6000018
        private System.Void <Open>b__0(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DD1438  token: 0x6000019
        private System.Void <Open>b__1(Hypergryph.CloudClientBridge.CloudCallResult result) { }

    }

    // TypeToken: 0x2000009  // size: 0x80
    public sealed class OpenCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x6000027
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000028
        public virtual System.Void Invoke(System.String jsonData) { }

    }

    // TypeToken: 0x200000A  // size: 0x80
    public sealed class EventCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x049CB4E0  token: 0x6000029
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x600002A
        public virtual System.Void Invoke(System.String jsonData) { }

    }

    // TypeToken: 0x200000B  // size: 0x80
    public sealed class EventListener : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x600002B
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x600002C
        public virtual System.Void Invoke(System.String jsonData) { }

    }

    // TypeToken: 0x200000E  // size: 0x10
    public static class StatusCode
    {
    }

    // TypeToken: 0x2000012  // size: 0x18
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public Hypergryph.SDK.IHGBrowserSDKCallback callback;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000036
        public System.Void .ctor() { }
        // RVA: 0x09DD0AC0  token: 0x6000037
        private System.Void <Open>b__0(System.String data) { }

    }

    // TypeToken: 0x2000014  // size: 0x18
    public sealed class <>c__DisplayClass0_0
    {
        // Fields
        public System.Action<System.String> onResult;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600003C
        public System.Void .ctor() { }
        // RVA: 0x09DD0884  token: 0x600003D
        private System.Void <Open>b__0(Hypergryph.CloudClientBridge.CloudCallResult result) { }

    }

    // TypeToken: 0x2000016  // size: 0x80
    public sealed class ExtraInfoCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x6000047
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000048
        public virtual System.Void Invoke(System.String jsonData) { }

    }

    // TypeToken: 0x2000020  // size: 0x18
    public sealed class <>c__DisplayClass8_0
    {
        // Fields
        public System.Action<Hypergryph.SDK.SurveyStateResult> onResult;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600005F
        public System.Void .ctor() { }
        // RVA: 0x09DD1924  token: 0x6000060
        private System.Void <GetSurveyStatus>b__0(System.String jsonData) { }

    }

    // TypeToken: 0x2000022  // size: 0x28
    public sealed class <>c__DisplayClass4_0
    {
        // Fields
        public System.Action<System.String> eventListener;  // 0x10
        public System.Action<System.String> onResult;  // 0x18
        public System.Action<Hypergryph.CloudClientBridge.CloudCallResult> eventCallback;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000068
        public System.Void .ctor() { }
        // RVA: 0x09DD0B0C  token: 0x6000069
        private System.Void <Open>b__0(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DD0D10  token: 0x600006A
        private System.Void <Open>b__1(Hypergryph.CloudClientBridge.CloudCallResult result) { }

    }

    // TypeToken: 0x2000023  // size: 0x18
    public sealed class <>c__DisplayClass7_0
    {
        // Fields
        public System.Action<Hypergryph.SDK.HGWebPortalState> onResult;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600006B
        public System.Void .ctor() { }
        // RVA: 0x09DD0FB0  token: 0x600006C
        private System.Void <GetWebPortalState>b__0(Hypergryph.CloudClientBridge.CloudCallResult result) { }

    }

    // TypeToken: 0x2000024  // size: 0x18
    public sealed class <>c__DisplayClass8_0
    {
        // Fields
        public System.Action<System.String> onResult;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600006D
        public System.Void .ctor() { }
        // RVA: 0x09DD16D8  token: 0x600006E
        private System.Void <GetSurveyStatus>b__0(Hypergryph.CloudClientBridge.CloudCallResult result) { }

    }

    // TypeToken: 0x2000025  // size: 0x18
    public sealed class <>c__DisplayClass9_0
    {
        // Fields
        public System.Action<System.String> onResult;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600006F
        public System.Void .ctor() { }
        // RVA: 0x09DD19C0  token: 0x6000070
        private System.Void <OpenBrowser>b__0(Hypergryph.CloudClientBridge.CloudCallResult result) { }

    }

    // TypeToken: 0x2000027  // size: 0x80
    public sealed class OpenCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x6000080
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000081
        public virtual System.Void Invoke(System.String jsonData) { }

    }

    // TypeToken: 0x2000028  // size: 0x80
    public sealed class EventListener : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x6000082
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000083
        public virtual System.Void Invoke(System.String jsonData) { }

    }

    // TypeToken: 0x2000029  // size: 0x80
    public sealed class OpenBrowserCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x6000084
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000085
        public virtual System.Void Invoke(System.String jsonData) { }

    }

    // TypeToken: 0x200002A  // size: 0x80
    public sealed class WebPortalStateCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x049CB4E0  token: 0x6000086
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000087
        public virtual System.Void Invoke(System.String jsonData) { }

    }

    // TypeToken: 0x200002B  // size: 0x80
    public sealed class WebProtalSurveyStatusCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x6000088
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000089
        public virtual System.Void Invoke(System.String jsonData) { }

    }

namespace Hypergryph.SDK
{

    // TypeToken: 0x2000002
    public interface IHGBulletinSDK
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000001
        public virtual System.Void Open(System.String type, System.Action<System.String> onResult, System.Action<System.String> eventListener) { }
        // RVA: -1  // abstract  token: 0x6000002
        public virtual System.Void Close() { }
        // RVA: -1  // abstract  token: 0x6000003
        public virtual System.String GetRedDotState(System.String type) { }
        // RVA: -1  // abstract  token: 0x6000004
        public virtual System.Void RegisterBulletinEventListener(System.Action<System.String> eventListener) { }

    }

    // TypeToken: 0x2000003  // size: 0x10
    public class HGBulletinSDK
    {
        // Fields
        private static Hypergryph.SDK.IHGBulletinSDK s_bulletin;  // static @ 0x0

        // Properties
        Hypergryph.SDK.IHGBulletinSDK bulletinSdk { get; /* RVA: 0x040BD300 */ }

        // Methods
        // RVA: 0x09DCE324  token: 0x6000006
        public static System.Void Open(System.String type, System.Action<System.String> onResult, System.Action<System.String> eventListener) { }
        // RVA: 0x09DCE2C0  token: 0x6000007
        public static System.Void Close() { }
        // RVA: 0x040BD600  token: 0x6000008
        public static System.String GetRedDotState(System.String type) { }
        // RVA: 0x040BD2A0  token: 0x6000009
        public static System.Void RegisterBulletinEventListener(System.Action<System.String> eventListener) { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public class HGBulletinSDKPluginCloud : Hypergryph.SDK.IHGBulletinSDK
    {
        // Fields
        private static System.Action<System.String> outerEventCallback;  // static @ 0x0
        private static System.Collections.Generic.Dictionary<System.String,Hypergryph.SDK.HGBulletinSDKPluginCloud.RedDotEventData> _redDotStateDict;  // static @ 0x8

        // Methods
        // RVA: 0x09DCDD78  token: 0x600000A
        public System.Void .ctor() { }
        // RVA: 0x09DCCF34  token: 0x600000B
        protected virtual System.Void Finalize() { }
        // RVA: 0x09DCD18C  token: 0x600000C
        public static System.Void InnerEventCallBack(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DCD534  token: 0x600000D
        public virtual System.Void Open(System.String type, System.Action<System.String> onResult, System.Action<System.String> eventListener) { }
        // RVA: 0x09DCCA88  token: 0x600000E
        public virtual System.Void Close() { }
        // RVA: 0x09DCD070  token: 0x600000F
        public virtual System.String GetRedDotState(System.String type) { }
        // RVA: 0x09DCDA94  token: 0x6000010
        public virtual System.Void RegisterBulletinEventListener(System.Action<System.String> eventListener) { }
        // RVA: 0x09DCD424  token: 0x6000011
        private System.Void NotifySDKInstantiated() { }
        // RVA: 0x09DCDB20  token: 0x6000012
        private static System.Void UpdateRedDotState(System.String target, Hypergryph.SDK.HGBulletinSDKPluginCloud.RedDotEventData data) { }
        // RVA: 0x09DCCCC8  token: 0x6000013
        private System.String CreateRedDotStateJson(System.String type, System.Boolean showRedDot, System.Boolean showPopup) { }
        // RVA: 0x09DCDCE4  token: 0x6000014
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public class HGBulletinSDKPluginWindows : Hypergryph.SDK.IHGBulletinSDK
    {
        // Fields
        private static System.Action<System.String> outerOpenCallback;  // static @ 0x0
        private static System.Action<System.String> outerEventCallback;  // static @ 0x8
        private static System.Action<System.String> outerEventListener;  // static @ 0x10

        // Methods
        // RVA: 0x09DCDECC  token: 0x600001A
        private static System.Void BulletinSDK_OpenBulletin(System.String type, Hypergryph.SDK.HGBulletinSDKPluginWindows.OpenCallback callback, Hypergryph.SDK.HGBulletinSDKPluginWindows.EventListener eventListener) { }
        // RVA: 0x09DCDE64  token: 0x600001B
        private static System.Void BulletinSDK_CloseBulletin() { }
        // RVA: 0x040BD5A0  token: 0x600001C
        private static System.IntPtr BulletinSDK_GetRedDotState(System.String type) { }
        // RVA: 0x03827010  token: 0x600001D
        private static System.Void BulletinSDK_RegisterBulletinEventListener(Hypergryph.SDK.HGBulletinSDKPluginWindows.EventCallback callback) { }
        // RVA: 0x09DCE094  token: 0x600001E
        public static System.Void InnerOpenCallBack(System.String jsonData) { }
        // RVA: 0x09DCDFF4  token: 0x600001F
        public static System.Void InnerEventCallBack(System.String jsonData) { }
        // RVA: 0x09DCE044  token: 0x6000020
        public static System.Void InnerEventListener(System.String jsonData) { }
        // RVA: 0x040BD1E0  token: 0x6000021
        public System.Void .ctor() { }
        // RVA: 0x09DCC5E8  token: 0x6000022
        protected virtual System.Void Finalize() { }
        // RVA: 0x09DCE130  token: 0x6000023
        public virtual System.Void Open(System.String type, System.Action<System.String> onResult, System.Action<System.String> eventListener) { }
        // RVA: 0x09DCDF80  token: 0x6000024
        public virtual System.Void Close() { }
        // RVA: 0x040BD460  token: 0x6000025
        public virtual System.String GetRedDotState(System.String type) { }
        // RVA: 0x04A78730  token: 0x6000026
        public virtual System.Void RegisterBulletinEventListener(System.Action<System.String> eventListener) { }

    }

    // TypeToken: 0x200000C  // size: 0x10
    public class CloudGameUtil
    {
        // Methods
        // RVA: 0x040BCB50  token: 0x600002D
        public static System.Boolean IsCloudGame() { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public static class HGBroserSDKConstants
    {
    }

    // TypeToken: 0x200000F
    public interface IHGBrowserSDKCallback
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600002E
        public virtual System.Void onBrowserEvent(System.String data) { }

    }

    // TypeToken: 0x2000010
    public interface IHGBrowserSDK
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600002F
        public virtual System.Void Open(System.String url, System.String styleConfig, System.Action<System.String> onResult) { }
        // RVA: -1  // abstract  token: 0x6000030
        public virtual System.Void Close() { }
        // RVA: -1  // abstract  token: 0x6000031
        public virtual System.Void SetAudioConfig(System.String config) { }

    }

    // TypeToken: 0x2000011  // size: 0x10
    public class HGBrowserSDK
    {
        // Fields
        private static Hypergryph.SDK.IHGBrowserSDK s_browser;  // static @ 0x0

        // Properties
        Hypergryph.SDK.IHGBrowserSDK browserSDK { get; /* RVA: 0x040BCEE0 */ }

        // Methods
        // RVA: 0x09DCC954  token: 0x6000033
        public static System.Void Open(System.String url, System.String styleConfig, Hypergryph.SDK.IHGBrowserSDKCallback callback) { }
        // RVA: 0x09DCC8F0  token: 0x6000034
        public static System.Void Close() { }
        // RVA: 0x040BCE80  token: 0x6000035
        public static System.Void SetAudioConfig(System.String config) { }

    }

    // TypeToken: 0x2000013  // size: 0x10
    public class HGBrowserSDKPluginCloud : Hypergryph.SDK.IHGBrowserSDK
    {
        // Methods
        // RVA: 0x09DCBE7C  token: 0x6000038
        public virtual System.Void Open(System.String url, System.String styleConfig, System.Action<System.String> onResult) { }
        // RVA: 0x09DCBC3C  token: 0x6000039
        public virtual System.Void Close() { }
        // RVA: 0x09DCC2D0  token: 0x600003A
        public virtual System.Void SetAudioConfig(System.String config) { }
        // RVA: 0x041E1670  token: 0x600003B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000015  // size: 0x10
    public class HGBrowserSDKPluginWindows : Hypergryph.SDK.IHGBrowserSDK
    {
        // Fields
        private static System.Action<System.String> outerCallback;  // static @ 0x0

        // Methods
        // RVA: 0x09DCC70C  token: 0x600003E
        private static System.Void MiniWebViewSDKLoad(System.String url, System.String styleConfig, Hypergryph.SDK.HGBrowserSDKPluginWindows.ExtraInfoCallback callback) { }
        // RVA: 0x09DCC6E0  token: 0x600003F
        private static System.Void MiniWebViewSDKClose() { }
        // RVA: 0x03827900  token: 0x6000040
        private static System.Void WebViewSDKSetAudioConfig(System.String config) { }
        // RVA: 0x09DCC66C  token: 0x6000041
        public static System.Void InnerCallBack(System.String jsonData) { }
        // RVA: 0x040BD1E0  token: 0x6000042
        public System.Void .ctor() { }
        // RVA: 0x09DCC5E8  token: 0x6000043
        protected virtual System.Void Finalize() { }
        // RVA: 0x09DCC7CC  token: 0x6000044
        public virtual System.Void Open(System.String url, System.String styleConfig, System.Action<System.String> onResult) { }
        // RVA: 0x09DCC574  token: 0x6000045
        public virtual System.Void Close() { }
        // RVA: 0x040BCE60  token: 0x6000046
        public virtual System.Void SetAudioConfig(System.String config) { }

    }

    // TypeToken: 0x2000017  // size: 0x18
    public class HGBrowerSDKTickEvent : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x03827D80  token: 0x6000049
        private System.Void Update() { }
        // RVA: 0x03827D80  token: 0x600004A
        private static System.Void WebViewSDKTickEvent() { }
        // RVA: 0x05393520  token: 0x600004B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000018  // size: 0x20
    public class HGBrowerSDKTickEventManager
    {
        // Fields
        private UnityEngine.GameObject tickGameObject;  // 0x10
        private Hypergryph.SDK.HGBrowerSDKTickEvent tickEvent;  // 0x18
        private static Hypergryph.SDK.HGBrowerSDKTickEventManager instanceMangaer;  // static @ 0x0

        // Properties
        Hypergryph.SDK.HGBrowerSDKTickEventManager InstanceMangaer { get; /* RVA: 0x040BD210 */ }

        // Methods
        // RVA: 0x04A3C610  token: 0x600004D
        public System.Void InitTickEvent() { }
        // RVA: 0x09DCBBB0  token: 0x600004E
        public System.Void UnInitTickEvent() { }
        // RVA: 0x041E1670  token: 0x600004F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x10
    public static class HGBrowserSDKVersion
    {
        // Fields
        public static readonly System.String VERSION;  // static @ 0x0

        // Methods
        // RVA: 0x04D506E0  token: 0x6000050
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x18
    public class HGWebPortalState
    {
        // Fields
        public System.Int32 status;  // 0x10
        public System.Boolean redDot;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000051
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001B  // size: 0x10
    public class HGWebPortalMetadata
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000052
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x20
    public class SurveyState
    {
        // Fields
        public System.String surveyId;  // 0x10
        public System.Boolean isAnswered;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000053
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001D  // size: 0x20
    public class SurveyStateResult
    {
        // Fields
        public System.Int32 status;  // 0x10
        public Hypergryph.SDK.SurveyState[] stateList;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000054
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001E
    public interface IHGWebPortalSDK
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000055
        public virtual System.Void Open(System.String target, System.String urlParams, System.Action<System.String> onResult, System.Action<System.String> eventListener) { }
        // RVA: -1  // abstract  token: 0x6000056
        public virtual System.Void OpenBrowser(System.String url, System.String styleConfig, System.Action<System.String> onResult) { }
        // RVA: -1  // abstract  token: 0x6000057
        public virtual System.Void GetWebPortalState(System.String target, System.Action<Hypergryph.SDK.HGWebPortalState> onResult) { }
        // RVA: -1  // abstract  token: 0x6000058
        public virtual System.Void GetSurveyStatus(System.String surveyIds, System.Action<System.String> onResult) { }

    }

    // TypeToken: 0x200001F  // size: 0x10
    public class HGWebPortalSDK
    {
        // Fields
        private static Hypergryph.SDK.IHGWebPortalSDK s_webportal;  // static @ 0x0

        // Properties
        Hypergryph.SDK.IHGWebPortalSDK webportalSdk { get; /* RVA: 0x040BD090 */ }

        // Methods
        // RVA: 0x09DD003C  token: 0x600005A
        public static System.Void Open(System.String target, System.String urlParams, System.Action<System.String> onResult, System.Action<System.String> eventListener) { }
        // RVA: 0x09DCFF90  token: 0x600005B
        public static System.Void OpenBrowser(System.String url, System.String styleConfig, System.Action<System.String> onResult) { }
        // RVA: 0x040BD030  token: 0x600005C
        public static System.Void GetWebPortalState(System.String target, System.Action<Hypergryph.SDK.HGWebPortalState> onResult) { }
        // RVA: 0x09DCFE78  token: 0x600005D
        public static System.Void GetSurveyStatus(System.String surveyIds, System.Action<Hypergryph.SDK.SurveyStateResult> onResult) { }
        // RVA: 0x09DD00F0  token: 0x600005E
        private static Hypergryph.SDK.SurveyStateResult ParseSurveyStateResult(System.String jsonData) { }

    }

    // TypeToken: 0x2000021  // size: 0x10
    public class HGWebPortalSDKPluginCloud : Hypergryph.SDK.IHGWebPortalSDK
    {
        // Fields
        private static System.Action<System.String> outerSchemeJumpCallback;  // static @ 0x0

        // Methods
        // RVA: 0x09DCF548  token: 0x6000061
        public System.Void .ctor() { }
        // RVA: 0x09DCE3C4  token: 0x6000062
        protected virtual System.Void Finalize() { }
        // RVA: 0x09DCEB50  token: 0x6000063
        public static System.Void InnerSchemeJumpCallBack(Hypergryph.CloudClientBridge.CloudCallResult result) { }
        // RVA: 0x09DCEFBC  token: 0x6000064
        public virtual System.Void Open(System.String target, System.String urlParams, System.Action<System.String> onResult, System.Action<System.String> eventListener) { }
        // RVA: 0x09DCE828  token: 0x6000065
        public virtual System.Void GetWebPortalState(System.String target, System.Action<Hypergryph.SDK.HGWebPortalState> onResult) { }
        // RVA: 0x09DCE500  token: 0x6000066
        public virtual System.Void GetSurveyStatus(System.String surveyIds, System.Action<System.String> onResult) { }
        // RVA: 0x09DCEC74  token: 0x6000067
        public virtual System.Void OpenBrowser(System.String url, System.String styleConfig, System.Action<System.String> onResult) { }

    }

    // TypeToken: 0x2000026  // size: 0x10
    public class HGWebPortalSDKPluginWindows : Hypergryph.SDK.IHGWebPortalSDK
    {
        // Fields
        private static System.Action<System.String> outerOpenCallback;  // static @ 0x0
        private static System.Action<System.String> outerEventListener;  // static @ 0x8
        private static System.Action<System.String> outerOpenBrowserCallback;  // static @ 0x10
        private static System.Action<Hypergryph.SDK.HGWebPortalState> outerWebPortalStateCallback;  // static @ 0x18
        private static System.Action<System.String> outerWebPortalSurveyStatusCallback;  // static @ 0x20

        // Methods
        // RVA: 0x09DCFDA0  token: 0x6000071
        private static System.Void WebPortalSDK_Open(System.String target, System.String urlParams, Hypergryph.SDK.HGWebPortalSDKPluginWindows.OpenCallback callback, Hypergryph.SDK.HGWebPortalSDKPluginWindows.EventListener eventListener) { }
        // RVA: 0x09DCFCE4  token: 0x6000072
        private static System.Void WebPortalSDK_OpenBrowser(System.String url, System.String styleConfig, Hypergryph.SDK.HGWebPortalSDKPluginWindows.OpenBrowserCallback callback) { }
        // RVA: 0x03826F60  token: 0x6000073
        private static System.Void WebPortalSDK_GetWebPortalState(System.String target, Hypergryph.SDK.HGWebPortalSDKPluginWindows.WebPortalStateCallback callback) { }
        // RVA: 0x09DCFC3C  token: 0x6000074
        private static System.IntPtr WebPortalSDK_GetSurveyStatus(System.String surveyIds, Hypergryph.SDK.HGWebPortalSDKPluginWindows.WebProtalSurveyStatusCallback callback) { }
        // RVA: 0x09DCF854  token: 0x6000075
        public static System.Void InnerOpenCallBack(System.String jsonData) { }
        // RVA: 0x09DCF788  token: 0x6000076
        public static System.Void InnerEventListener(System.String jsonData) { }
        // RVA: 0x09DCF7D8  token: 0x6000077
        public static System.Void InnerOpenBrowserCallback(System.String jsonData) { }
        // RVA: 0x04B6B510  token: 0x6000078
        public static System.Void InnerWebPortalStateCallback(System.String jsonData) { }
        // RVA: 0x09DCF8F0  token: 0x6000079
        public static System.Void InnerWebPortaSurveyStatusCallback(System.String jsonData) { }
        // RVA: 0x040BD1E0  token: 0x600007A
        public System.Void .ctor() { }
        // RVA: 0x09DCC5E8  token: 0x600007B
        protected virtual System.Void Finalize() { }
        // RVA: 0x09DCFA94  token: 0x600007C
        public virtual System.Void Open(System.String target, System.String urlParams, System.Action<System.String> onResult, System.Action<System.String> eventListener) { }
        // RVA: 0x09DCF96C  token: 0x600007D
        public virtual System.Void OpenBrowser(System.String url, System.String styleConfig, System.Action<System.String> onResult) { }
        // RVA: 0x04A425F0  token: 0x600007E
        public virtual System.Void GetWebPortalState(System.String target, System.Action<Hypergryph.SDK.HGWebPortalState> onResult) { }
        // RVA: 0x09DCF620  token: 0x600007F
        public virtual System.Void GetSurveyStatus(System.String surveyIds, System.Action<System.String> onResult) { }

    }

}

