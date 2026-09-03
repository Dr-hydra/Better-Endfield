// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.UnityAnalyticsModule.dll
// Classes:  11
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

CLASS: UpdatedEventHandler
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x053DEDC8  token=0x6000004  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x6000005  System.Void Invoke()
END_CLASS

CLASS: Tag
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.RemoteConfigSettingsHelper.TagkUnknown  // const
  public    static  UnityEngine.RemoteConfigSettingsHelper.TagkIntVal  // const
  public    static  UnityEngine.RemoteConfigSettingsHelper.TagkInt64Val  // const
  public    static  UnityEngine.RemoteConfigSettingsHelper.TagkUInt64Val  // const
  public    static  UnityEngine.RemoteConfigSettingsHelper.TagkDoubleVal  // const
  public    static  UnityEngine.RemoteConfigSettingsHelper.TagkBoolVal  // const
  public    static  UnityEngine.RemoteConfigSettingsHelper.TagkStringVal  // const
  public    static  UnityEngine.RemoteConfigSettingsHelper.TagkArrayVal  // const
  public    static  UnityEngine.RemoteConfigSettingsHelper.TagkMixedArrayVal  // const
  public    static  UnityEngine.RemoteConfigSettingsHelper.TagkMapVal  // const
  public    static  UnityEngine.RemoteConfigSettingsHelper.TagkMaxTags  // const
METHODS:
END_CLASS

CLASS: SessionStateChanged
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B588ACC  token=0x6000009  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02C448D0  token=0x600000A  System.Void Invoke(UnityEngine.Analytics.AnalyticsSessionState sessionState, System.Int64 sessionId, System.Int64 sessionElapsedTime, System.Boolean sessionChanged)
END_CLASS

CLASS: IdentityTokenChanged
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679B0C8  token=0x600000B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600000C  System.Void Invoke(System.String token)
END_CLASS

CLASS: UnityEngine.RemoteSettings
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.RemoteSettings.UpdatedEventHandlerUpdated  // static @ 0x0
  private   static  System.Action                   BeforeFetchFromServer  // static @ 0x8
  private   static  System.Action<System.Boolean,System.Boolean,System.Int32>Completed  // static @ 0x10
METHODS:
  RVA=0x0B588A84  token=0x6000001  System.Void RemoteSettingsUpdated(System.Boolean wasLastUpdatedFromServer)
  RVA=0x0B5889C8  token=0x6000002  System.Void RemoteSettingsBeforeFetchFromServer()
  RVA=0x0B588A0C  token=0x6000003  System.Void RemoteSettingsUpdateCompleted(System.Boolean wasLastUpdatedFromServer, System.Boolean settingsChanged, System.Int32 response)
END_CLASS

CLASS: UnityEngine.RemoteConfigSettings
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x20
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
  private           System.Action<System.Boolean>   Updated  // 0x18
METHODS:
  RVA=0x0B5889A0  token=0x6000006  System.Void RemoteConfigSettingsUpdated(UnityEngine.RemoteConfigSettings rcs, System.Boolean wasLastUpdatedFromServer)
END_CLASS

CLASS: UnityEngine.RemoteConfigSettingsHelper
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Analytics.ContinuousEvent
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Analytics.AnalyticsSessionState
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Analytics.AnalyticsSessionStatekSessionStopped  // const
  public    static  UnityEngine.Analytics.AnalyticsSessionStatekSessionStarted  // const
  public    static  UnityEngine.Analytics.AnalyticsSessionStatekSessionPaused  // const
  public    static  UnityEngine.Analytics.AnalyticsSessionStatekSessionResumed  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Analytics.AnalyticsSessionInfo
TYPE:  static class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.Analytics.AnalyticsSessionInfo.SessionStateChangedsessionStateChanged  // static @ 0x0
  private   static  UnityEngine.Analytics.AnalyticsSessionInfo.IdentityTokenChangedidentityTokenChanged  // static @ 0x8
METHODS:
  RVA=0x04D3E5C0  token=0x6000007  System.Void CallSessionStateChanged(UnityEngine.Analytics.AnalyticsSessionState sessionState, System.Int64 sessionId, System.Int64 sessionElapsedTime, System.Boolean sessionChanged)
  RVA=0x0B588950  token=0x6000008  System.Void CallIdentityTokenChanged(System.String token)
END_CLASS

