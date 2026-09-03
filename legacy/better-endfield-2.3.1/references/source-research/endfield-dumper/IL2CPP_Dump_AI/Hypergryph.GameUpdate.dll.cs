// ========================================================
// Dumped by @desirepro
// Assembly: Hypergryph.GameUpdate.dll
// Classes:  9
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

CLASS: <>c__DisplayClass6_0
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x18
FIELDS:
  public            System.String                   data  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000005  System.Void .ctor()
  RVA=0x09DE40F0  token=0x6000006  System.Void <onLatestGame>b__0()
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x18
FIELDS:
  public            System.Action                   action  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000007  System.Void .ctor()
  RVA=0x06DFA030  token=0x6000008  System.Void <runInMainTread>b__0(System.Object p)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
  public    static readonly Hypergryph.SDK.HGGameUpdateSDK.<>c<>9  // static @ 0x0
  public    static  System.Action<System.String>    <>9__6_0  // static @ 0x8
METHODS:
  RVA=0x09DE4140  token=0x6000027  System.Void .cctor()
  RVA=0x041E1670  token=0x6000028  System.Void .ctor()
  RVA=0x09DE4054  token=0x6000029  System.Void <GetLatestGame>b__6_0(System.String result)
END_CLASS

CLASS: Hypergryph.SDK.HGGameUpdateCallback
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  Hypergryph.SDK.IHGGameUpdateSDKCallbackm_callback  // static @ 0x0
  private   static  System.String                   gameObjectName  // const
  private   static  System.Threading.SynchronizationContextmainThreadContext  // static @ 0x8
  public    static  Hypergryph.SDK.HGGameUpdateCallbacks_instance  // static @ 0x10
METHODS:
  RVA=0x09DE3274  token=0x6000001  System.Void .ctor()
  RVA=0x09DE30B8  token=0x6000002  System.Void Init(Hypergryph.SDK.IHGGameUpdateSDKCallback callback)
  RVA=0x09DE32D8  token=0x6000003  System.Void onLatestGame(System.String data)
  RVA=0x09DE3390  token=0x6000004  System.Void runInMainTread(System.Action action)
END_CLASS

CLASS: Hypergryph.SDK.IHGGameUpdateSDKCallback
TYPE:  interface
TOKEN: 0x2000005
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000009  System.Void onLatestGame(System.String data)
END_CLASS

CLASS: Hypergryph.SDK.IHGGameUpdateSDK
TYPE:  interface
TOKEN: 0x2000006
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600000A  System.Int32 Init(System.String config)
  RVA=-1  // abstract  token=0x600000B  System.Void GetLatestGame(System.Action<System.String> onResult)
  RVA=-1  // abstract  token=0x600000C  System.Int64 Update(System.Int32 updateType, System.Boolean useMobileData)
  RVA=-1  // abstract  token=0x600000D  System.Int32 EnableMobileData(System.Int64 taskId)
  RVA=-1  // abstract  token=0x600000E  System.Int32 GetTaskState(System.Int64 taskId)
  RVA=-1  // abstract  token=0x600000F  System.Int32 Resume(System.Int64 taskId)
  RVA=-1  // abstract  token=0x6000010  System.Int32 CancelAndClear(System.Int64 taskId)
  RVA=-1  // abstract  token=0x6000011  System.Int32 Cancel(System.Int64 taskId)
  RVA=-1  // abstract  token=0x6000012  System.Int32 Install(System.Int64 taskId)
  RVA=-1  // abstract  token=0x6000013  System.Int64 GetDownloadSpeed(System.Int64 taskId)
  RVA=-1  // abstract  token=0x6000014  System.Int64 GetDownloadedSize(System.Int64 taskId)
  RVA=-1  // abstract  token=0x6000015  System.Int64 GetTotalDownloadSize(System.Int64 taskId)
  RVA=-1  // abstract  token=0x6000016  System.Int64 GetEstimatedDownloadSize(System.Int32 updateType)
  RVA=-1  // abstract  token=0x6000017  System.Int32 SetNotificationTitle(System.String titleConfig)
END_CLASS

CLASS: Hypergryph.SDK.HGGameUpdateSDK
TYPE:  static class
TOKEN: 0x2000007
SIZE:  0x10
FIELDS:
  private   static  Hypergryph.SDK.IHGGameUpdateSDK s_gu  // static @ 0x0
  private   static  Hypergryph.SDK.IHGGameUpdateSDKCallbackm_callback  // static @ 0x8
  private   static  System.Threading.SynchronizationContextmainThreadContext  // static @ 0x10
PROPERTIES:
  gameUpdateSDK  get=0x09DE3FC0
METHODS:
  RVA=0x09DE3D50  token=0x6000019  System.Int32 Init(System.String config, Hypergryph.SDK.IHGGameUpdateSDKCallback callback)
  RVA=0x09DE3B70  token=0x600001A  System.Void GetLatestGame()
  RVA=0x09DE3F54  token=0x600001B  System.Int64 Update(System.Int32 updateType, System.Boolean useMobileData)
  RVA=0x09DE39F0  token=0x600001C  System.Int32 EnableMobileData(System.Int64 taskId)
  RVA=0x09DE3C90  token=0x600001D  System.Int32 GetTaskState(System.Int64 taskId)
  RVA=0x09DE3E94  token=0x600001E  System.Int32 Resume(System.Int64 taskId)
  RVA=0x09DE3930  token=0x600001F  System.Int32 CancelAndClear(System.Int64 taskId)
  RVA=0x09DE3990  token=0x6000020  System.Int32 Cancel(System.Int64 taskId)
  RVA=0x09DE3E34  token=0x6000021  System.Int32 Install(System.Int64 taskId)
  RVA=0x09DE3A50  token=0x6000022  System.Int64 GetDownloadSpeed(System.Int64 taskId)
  RVA=0x09DE3AB0  token=0x6000023  System.Int64 GetDownloadedSize(System.Int64 taskId)
  RVA=0x09DE3CF0  token=0x6000024  System.Int64 GetTotalDownloadSize(System.Int64 taskId)
  RVA=0x09DE3B10  token=0x6000025  System.Int64 GetEstimatedDownloadSize(System.Int32 updateType)
  RVA=0x09DE3EF4  token=0x6000026  System.Int32 SetNotificationTitle(System.String titleConfig)
END_CLASS

CLASS: Hypergryph.SDK.HGGameUpdateSDKPluginDefault
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x10
IMPLEMENTS: Hypergryph.SDK.IHGGameUpdateSDK
FIELDS:
METHODS:
  RVA=0x09DE3798  token=0x600002A  System.Int32 Init(System.String config)
  RVA=0x09DE3668  token=0x600002B  System.Void GetLatestGame(System.Action<System.String> onResult)
  RVA=0x09DE38D8  token=0x600002C  System.Int64 Update(System.Int32 updateType, System.Boolean useMobileData)
  RVA=0x09DE3528  token=0x600002D  System.Int32 EnableMobileData(System.Int64 taskId)
  RVA=0x09DE36F8  token=0x600002E  System.Int32 GetTaskState(System.Int64 taskId)
  RVA=0x09DE3838  token=0x600002F  System.Int32 Resume(System.Int64 taskId)
  RVA=0x09DE3488  token=0x6000030  System.Int32 CancelAndClear(System.Int64 taskId)
  RVA=0x09DE34D8  token=0x6000031  System.Int32 Cancel(System.Int64 taskId)
  RVA=0x09DE37E8  token=0x6000032  System.Int32 Install(System.Int64 taskId)
  RVA=0x09DE3578  token=0x6000033  System.Int64 GetDownloadSpeed(System.Int64 taskId)
  RVA=0x09DE35C8  token=0x6000034  System.Int64 GetDownloadedSize(System.Int64 taskId)
  RVA=0x09DE3748  token=0x6000035  System.Int64 GetTotalDownloadSize(System.Int64 taskId)
  RVA=0x09DE3618  token=0x6000036  System.Int64 GetEstimatedDownloadSize(System.Int32 updateType)
  RVA=0x09DE3888  token=0x6000037  System.Int32 SetNotificationTitle(System.String titleConfig)
  RVA=0x041E1670  token=0x6000038  System.Void .ctor()
END_CLASS

