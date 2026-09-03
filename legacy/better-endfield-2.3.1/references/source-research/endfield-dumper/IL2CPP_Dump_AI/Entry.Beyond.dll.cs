// ========================================================
// Dumped by @desirepro
// Assembly: Entry.Beyond.dll
// Classes:  286
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

CLASS: RenderSilhouetteRT
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.Camera              cameraRT  // 0x18
  public            UnityEngine.Camera              mainCamera  // 0x20
  public            UnityEngine.Material            materialSilhouette  // 0x28
  private   static  UnityEngine.Texture             s_mSilhouetteRT  // static @ 0x0
  private   static  UnityEngine.RenderTexture       s_mSilhouetteRth  // static @ 0x8
METHODS:
  RVA=0x03D76B40  token=0x6000001  System.Void Update()
  RVA=0x0484FD10  token=0x6000002  System.Void OnDestroy()
  RVA=0x05393520  token=0x6000003  System.Void .ctor()
END_CLASS

CLASS: WIN32_FIND_DATA
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x50
FIELDS:
  public            System.UInt32                   dwFileAttributes  // 0x10
  public            System.Runtime.InteropServices.ComTypes.FILETIMEftCreationTime  // 0x14
  public            System.Runtime.InteropServices.ComTypes.FILETIMEftLastAccessTime  // 0x1c
  public            System.Runtime.InteropServices.ComTypes.FILETIMEftLastWriteTime  // 0x24
  public            System.UInt32                   nFileSizeHigh  // 0x2c
  public            System.UInt32                   nFileSizeLow  // 0x30
  public            System.UInt32                   dwReserved0  // 0x34
  public            System.UInt32                   dwReserved1  // 0x38
  public            System.String                   cFileName  // 0x40
  public            System.String                   cAlternateFileName  // 0x48
METHODS:
END_CLASS

CLASS: FileEntry
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x38
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   fullPath  // 0x18
  public            System.Int64                    size  // 0x20
  public            System.String                   md5  // 0x28
  public            System.Int32                    pathLength  // 0x30
  public            System.Boolean                  readable  // 0x34
  public            System.Boolean                  writable  // 0x35
  public            System.Boolean                  readOnly  // 0x36
METHODS:
  RVA=0x041E1670  token=0x600003B  System.Void .ctor()
END_CLASS

CLASS: PendingFile
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x28
FIELDS:
  public            System.String                   fullPath  // 0x10
  public            System.Int64                    size  // 0x18
  public            System.Boolean                  readOnlyAttr  // 0x20
METHODS:
END_CLASS

CLASS: ScanContext
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x30
FIELDS:
  public    readonly System.Collections.Concurrent.ConcurrentQueue<System.String>dirQueue  // 0x10
  public    readonly System.Collections.Concurrent.ConcurrentQueue<Beyond.GameApp.PendingFile>fileQueue  // 0x18
  public    readonly System.Collections.Concurrent.ConcurrentBag<Beyond.GameApp.FileEntry>results  // 0x20
  public            System.Int32                    pendingDirs  // 0x28
  public            System.Int32                    pendingFiles  // 0x2c
METHODS:
  RVA=0x06993CD0  token=0x600003C  System.Void .ctor()
END_CLASS

CLASS: TreeNode
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x28
FIELDS:
  public            System.String                   name  // 0x10
  public            Beyond.GameApp.FileEntry        fileEntry  // 0x18
  public            System.Collections.Generic.Dictionary<System.String,Beyond.GameApp.TreeNode>children  // 0x20
METHODS:
  RVA=0x041E1670  token=0x600003D  System.Void .ctor()
END_CLASS

CLASS: <_DelayInit>d__14
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.GameApp                  <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600003E  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600003F  System.Void System.IDisposable.Dispose()
  RVA=0x06994B48  token=0x6000040  System.Boolean MoveNext()
  RVA=0x06994BF8  token=0x6000042  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_InitSecuritySDK>d__35
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.GameApp                  <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000044  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000045  System.Void System.IDisposable.Dispose()
  RVA=0x0340BDA0  token=0x6000046  System.Boolean MoveNext()
  RVA=0x06994E0C  token=0x6000048  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_FetchRemoteConfigUtilSuc>d__37
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  private           Beyond.Cfg.RemoteNetworkCfg     <config>5__2  // 0x20
  private           Beyond.Misc.ObjectRef           <resultRef>5__3  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600004A  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600004B  System.Void System.IDisposable.Dispose()
  RVA=0x03C09CD0  token=0x600004C  System.Boolean MoveNext()
  RVA=0x06994D28  token=0x600004E  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass71_0
TYPE:  sealed class
TOKEN: 0x200000D
SIZE:  0x30
FIELDS:
  public            System.String                   capturedRoot  // 0x10
  public            System.Int32                    capturedThreads  // 0x18
  public            System.String                   capturedOutput  // 0x20
  public            System.String                   capturedSysInfo  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000050  System.Void .ctor()
  RVA=0x069943B4  token=0x6000051  System.Void <LaunchScanCoordinator>b__0()
END_CLASS

CLASS: <_DoInit>d__5
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.GameInitState            <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000061  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000062  System.Void System.IDisposable.Dispose()
  RVA=0x03C11C20  token=0x6000063  System.Boolean MoveNext()
  RVA=0x06994C90  token=0x6000065  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_DoPreload>d__6
TYPE:  sealed class
TOKEN: 0x2000015
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.GamePreloadState         <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600007E  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600007F  System.Void System.IDisposable.Dispose()
  RVA=0x0340DD90  token=0x6000080  System.Boolean MoveNext()
  RVA=0x06994CDC  token=0x6000082  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_SoftRestart>d__5
TYPE:  sealed class
TOKEN: 0x2000018
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.GameSoftRestartState     <>4__this  // 0x20
  private           UnityEngine.AsyncOperation      <clearSceneOp>5__2  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000090  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000091  System.Void System.IDisposable.Dispose()
  RVA=0x06994EF0  token=0x6000092  System.Boolean MoveNext()
  RVA=0x069957DC  token=0x6000094  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <ReturnToLoginState>d__8
TYPE:  sealed class
TOKEN: 0x200001C
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.GameStateMachine         <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000A9  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000AA  System.Void System.IDisposable.Dispose()
  RVA=0x03C12470  token=0x60000AB  System.Boolean MoveNext()
  RVA=0x0699428C  token=0x60000AD  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: FOptions
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x18
FIELDS:
  public            Beyond.LoginContext             context  // 0x10
METHODS:
END_CLASS

CLASS: <_CloseCoroutine>d__4
TYPE:  sealed class
TOKEN: 0x2000025
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.LoginController.Dialog   <>4__this  // 0x20
  public            System.Action                   callback  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000DA  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000DB  System.Void System.IDisposable.Dispose()
  RVA=0x06994A30  token=0x60000DC  System.Boolean MoveNext()
  RVA=0x06994AFC  token=0x60000DE  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Create>d__8`1
TYPE:  sealed class
TOKEN: 0x2000026
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           System.Object                   <>2__current  // 0x0
  public            Beyond.Misc.ObjectRef           outRef  // 0x0
  public            Beyond.LoginController          controller  // 0x0
  public            System.String                   resPath  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000E0  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x60000E1  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x60000E2  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60000E4  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Dialog
TYPE:  abstract class
TOKEN: 0x2000024
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UISwitchTween         m_switchTween  // 0x18
  private           Beyond.LoginController          m_controller  // 0x20
  public            System.String                   showAudioEvent  // 0x28
  public            System.String                   closeAudioEvent  // 0x30
METHODS:
  RVA=0x0698D340  token=0x60000D4  System.Collections.IEnumerator _CloseCoroutine(System.Action callback)
  RVA=0x0698D1E4  token=0x60000D5  System.Boolean TryCloseWithCallback(System.Action callback)
  RVA=0x0698D0F4  token=0x60000D6  System.Void Show()
  RVA=0x0698D0B0  token=0x60000D7  System.Void OnInit()
  RVA=-1  // generic def  token=0x60000D8  System.Collections.IEnumerator Create(System.String resPath, Beyond.LoginController controller, Beyond.Misc.ObjectRef outRef)
  RVA=0x05393520  token=0x60000D9  System.Void .ctor()
END_CLASS

CLASS: MaskConsts
TYPE:  static class
TOKEN: 0x2000027
SIZE:  0x10
FIELDS:
  public    static  System.Single                   DELAY_WAIT_NODE  // const
  public    static  System.String                   INIT  // const
  public    static  System.String                   U8_LOGIN  // const
  public    static  System.String                   FROM_GS_LOGIN  // const
  public    static  System.String                   FROM_CREATE_ROLE  // const
  public    static  System.String                   NET_AGE_TIPS  // const
METHODS:
END_CLASS

CLASS: EMaskType
TYPE:  sealed struct
TOKEN: 0x2000028
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.LoginController.EMaskTypeNone  // const
  public    static  Beyond.LoginController.EMaskTypeBlack  // const
  public    static  Beyond.LoginController.EMaskTypeBlur  // const
  public    static  Beyond.LoginController.EMaskTypeWhite  // const
METHODS:
END_CLASS

CLASS: MaskCfg
TYPE:  class
TOKEN: 0x2000029
SIZE:  0x20
FIELDS:
  public            UnityEngine.CanvasGroup         target  // 0x10
  public            System.Single                   showDuration  // 0x18
  public            System.Single                   hideDuration  // 0x1c
METHODS:
  RVA=0x03F0F380  token=0x60000E6  Beyond.UI.UISwitchTween CreateTweenAndHide()
  RVA=0x04D8CC90  token=0x60000E7  System.Void .ctor()
END_CLASS

CLASS: NetMaskRef
TYPE:  class
TOKEN: 0x200002B
SIZE:  0x20
FIELDS:
  private           System.String                   <key>k__BackingField  // 0x10
  public            System.Int32                    maskId  // 0x18
  public            System.Boolean                  isEnabled  // 0x1c
PROPERTIES:
  key  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x053908C0  token=0x60000F4  System.Void .ctor(System.String key)
END_CLASS

CLASS: <WaitForCrossSceneMasks>d__10
TYPE:  sealed class
TOKEN: 0x200002C
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.LoginController.MaskCtrl <>4__this  // 0x20
  private           System.Int32[]                  <>7__wrap1  // 0x28
  private           System.Int32                    <>7__wrap2  // 0x30
  private           Beyond.UI.UISwitchTween         <mask>5__4  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000F5  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000F6  System.Void System.IDisposable.Dispose()
  RVA=0x069948BC  token=0x60000F7  System.Boolean MoveNext()
  RVA=0x069949E4  token=0x60000F9  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: MaskCtrl
TYPE:  class
TOKEN: 0x200002A
SIZE:  0x28
IMPLEMENTS: System.IDisposable
FIELDS:
  private   static readonly System.Int32[]                  CROSS_SCENE_MASKS  // static @ 0x0
  private           Beyond.ListDict<System.Int32,System.Collections.Generic.HashSet<System.String>>m_maskRefs  // 0x10
  private           Beyond.ListDict<System.Int32,Beyond.UI.UISwitchTween>m_maskTweens  // 0x18
  private           Beyond.ListDict<System.String,Beyond.LoginController.MaskCtrl.NetMaskRef>m_netMasks  // 0x20
METHODS:
  RVA=0x03F0F180  token=0x60000E8  System.Void .ctor(Beyond.LoginController controller)
  RVA=0x03F0F600  token=0x60000E9  System.Void Dispose()
  RVA=0x041E77B0  token=0x60000EA  System.Void EnableMask(System.String key, Beyond.LoginController.EMaskType type, System.Boolean enable, System.Boolean reset)
  RVA=0x041E7690  token=0x60000EB  System.Boolean IsMaskTweening(Beyond.LoginController.EMaskType type)
  RVA=0x041E76E0  token=0x60000EC  System.Boolean IsCrossSceneMaskTweening()
  RVA=0x06993BB8  token=0x60000ED  System.Collections.IEnumerator WaitForCrossSceneMasks()
  RVA=0x069939E0  token=0x60000EE  System.Void EnableNetMask(System.String key, System.Single timeoutSecs)
  RVA=0x069938F4  token=0x60000EF  System.Void DisableNetMask(System.String key)
  RVA=0x041E7990  token=0x60000F0  Beyond.UI.UISwitchTween _GetMaskByType(System.Int32 type)
  RVA=0x04CE79C0  token=0x60000F1  System.Void .cctor()
END_CLASS

CLASS: <BeforeDestroy>d__35
TYPE:  sealed class
TOKEN: 0x200002D
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.LoginController          <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000FB  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000FC  System.Void System.IDisposable.Dispose()
  RVA=0x041E5D70  token=0x60000FD  System.Boolean MoveNext()
  RVA=0x069941A8  token=0x60000FF  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_InstantiateUIFromRes>d__36`1
TYPE:  sealed class
TOKEN: 0x200002E
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           System.Object                   <>2__current  // 0x0
  public            Beyond.Misc.ObjectRef           objRef  // 0x0
  public            Beyond.LoginController          <>4__this  // 0x0
  public            System.String                   resPath  // 0x0
  public            UnityEngine.Transform           parent  // 0x0
  private           System.Nullable<Beyond.Resource.FAssetProxyHandle><handle>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000101  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000102  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000103  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000105  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_InitCoroutine>d__38
TYPE:  sealed class
TOKEN: 0x200002F
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.LoginController          <>4__this  // 0x20
  private           Beyond.LoginContext             <context>5__2  // 0x28
  private           Beyond.Misc.ObjectRef           <panelRef>5__3  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000107  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000108  System.Void System.IDisposable.Dispose()
  RVA=0x041E5520  token=0x6000109  System.Boolean MoveNext()
  RVA=0x06994DC0  token=0x600010B  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass45_0
TYPE:  sealed class
TOKEN: 0x2000030
SIZE:  0x18
FIELDS:
  public            Beyond.Misc.ObjectRef           objRef  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600010D  System.Void .ctor()
  RVA=0x03C8D270  token=0x600010E  System.Void <_MockLoginCoroutine>b__0(System.String pUID)
END_CLASS

CLASS: <_MockLoginCoroutine>d__45
TYPE:  sealed class
TOKEN: 0x2000031
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.LoginController          <>4__this  // 0x20
  private           Beyond.LoginController.<>c__DisplayClass45_0<>8__1  // 0x28
  public            System.String                   fixedUID  // 0x30
  private           Beyond.Login.LoginMockUIDPanel  <nullablePanel>5__2  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600010F  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000110  System.Void System.IDisposable.Dispose()
  RVA=0x03F0E1B0  token=0x6000111  System.Boolean MoveNext()
  RVA=0x06994EA4  token=0x6000113  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_DisposeCoroutine>d__48
TYPE:  sealed class
TOKEN: 0x2000033
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.LoginController          legacyController  // 0x20
  public            System.Action                   disposeAction  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000133  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000134  System.Void System.IDisposable.Dispose()
  RVA=0x04A22250  token=0x6000135  System.Boolean MoveNext()
  RVA=0x06994C44  token=0x6000137  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: AssetLoader
TYPE:  class
TOKEN: 0x2000037
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,Beyond.Resource.FAssetProxyHandle>m_assetHandles  // 0x10
METHODS:
  RVA=-1  // generic def  token=0x600015C  Beyond.Resource.FAssetProxyHandle CachedLoad(System.String path)
  RVA=-1  // generic def  token=0x600015D  Beyond.Resource.FAssetProxyHandle CachedLoadAsync(System.String path)
  RVA=0x04701C90  token=0x600015E  System.Boolean _CheckDisposedBeforeLoad(System.String path)
  RVA=0x048E3130  token=0x600015F  System.Void Dispose()
  RVA=0x0380AA10  token=0x6000160  System.Void .ctor()
END_CLASS

CLASS: <_FlowCoroutine>d__4
TYPE:  sealed class
TOKEN: 0x2000039
SIZE:  0x60
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.LoginManager.FlowWithMaskMgr<>4__this  // 0x20
  public            Beyond.LoginManager.FFlowWithMaskflow  // 0x28
  private           Beyond.LoginController.MaskCtrl <maskCtrl>5__2  // 0x50
  private           System.Boolean                  <isFlowSuc>5__3  // 0x58
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000165  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000166  System.Void System.IDisposable.Dispose()
  RVA=0x041E61D0  token=0x6000167  System.Boolean MoveNext()
  RVA=0x06994D74  token=0x6000169  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: FlowWithMaskMgr
TYPE:  class
TOKEN: 0x2000038
SIZE:  0x20
FIELDS:
  private           Beyond.LoginContext             m_context  // 0x10
  private           System.Collections.Generic.HashSet<System.String>m_activeFlows  // 0x18
METHODS:
  RVA=0x0380A5D0  token=0x6000161  System.Void .ctor(Beyond.LoginContext context)
  RVA=0x03A80DD0  token=0x6000162  System.Void StartFlow(Beyond.LoginManager.FFlowWithMask flow)
  RVA=0x03A80F10  token=0x6000163  System.Collections.IEnumerator _FlowCoroutine(Beyond.LoginManager.FFlowWithMask flow)
  RVA=0x0698D3E8  token=0x6000164  System.Void _OnFlowFinished(Beyond.LoginManager.FFlowWithMask flow)
END_CLASS

CLASS: FFlowWithMask
TYPE:  sealed struct
TOKEN: 0x200003A
SIZE:  0x38
FIELDS:
  public            System.String                   key  // 0x10
  public            Beyond.LoginController.EMaskTypemaskType  // 0x18
  public            System.Func<System.Boolean>     flowAction  // 0x20
  public            Beyond.LoginManager.FFlowConditioncondition  // 0x28
  public            System.Single                   disableDelay  // 0x30
METHODS:
END_CLASS

CLASS: FFlowCondition
TYPE:  sealed struct
TOKEN: 0x200003B
SIZE:  0x18
FIELDS:
  private           Beyond.Login.LoginWorkFlow.ENode<priTarget>k__BackingField  // 0x10
  private           Beyond.Login.LoginWorkFlow.ENode<secTarget>k__BackingField  // 0x14
PROPERTIES:
  priTarget  get=0x02B77620  set=0x04D86140
  secTarget  get=0x04D88B20  set=0x04D88B50
METHODS:
  RVA=0x04D8CC80  token=0x600016F  System.Void .ctor(Beyond.Login.LoginWorkFlow.ENode target)
  RVA=0x04D85C40  token=0x6000170  System.Void .ctor(Beyond.Login.LoginWorkFlow.ENode first, Beyond.Login.LoginWorkFlow.ENode second)
  RVA=0x041E7630  token=0x6000171  System.Boolean IsTargetMatch(Beyond.Login.LoginWorkFlow.ENode node)
END_CLASS

CLASS: <InitCoroutine>d__17
TYPE:  sealed class
TOKEN: 0x200003C
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.LoginManager             <>4__this  // 0x20
  public            System.Boolean                  quickStart  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000172  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000173  System.Void System.IDisposable.Dispose()
  RVA=0x04D30A10  token=0x6000174  System.Boolean MoveNext()
  RVA=0x06994240  token=0x6000176  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200003D
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.LoginManager.<>c         <>9  // static @ 0x0
  public    static  System.Action<Beyond.Login.LoginAlertDialog,Beyond.Login.LoginAlertDialog.FParams><>9__29_0  // static @ 0x8
  public    static  System.Action<Beyond.Login.LoginVocResSelDialog,Beyond.Login.LoginVocResSelDialog.FParams><>9__30_0  // static @ 0x10
  public    static  System.Action<Beyond.Login.LoginJudgeDialog,Beyond.Login.LoginJudgeDialog.FParams><>9__36_0  // static @ 0x18
METHODS:
  RVA=0x069946D0  token=0x6000178  System.Void .cctor()
  RVA=0x041E1670  token=0x6000179  System.Void .ctor()
  RVA=0x069942D8  token=0x600017A  System.Void <AlertDialog>b__29_0(Beyond.Login.LoginAlertDialog param1, Beyond.Login.LoginAlertDialog.FParams param2)
  RVA=0x06994308  token=0x600017B  System.Void <VocResSelDialog>b__30_0(Beyond.Login.LoginVocResSelDialog param1, Beyond.Login.LoginVocResSelDialog.FParams param2)
  RVA=0x06994330  token=0x600017C  System.Void <_StartShowJudgeDialogCoroutine>b__36_0(Beyond.Login.LoginJudgeDialog param1, Beyond.Login.LoginJudgeDialog.FParams param2)
END_CLASS

CLASS: <>c__DisplayClass31_0
TYPE:  sealed class
TOKEN: 0x200003E
SIZE:  0x18
FIELDS:
  public            System.Object                   wait  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600017D  System.Void .ctor()
  RVA=0x040E20A0  token=0x600017E  System.Void <VocResSelDialogWithConfirm>b__0()
END_CLASS

CLASS: <VocResSelDialogWithConfirm>d__31
TYPE:  sealed class
TOKEN: 0x200003F
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.LoginManager             <>4__this  // 0x20
  private           Beyond.LoginManager.<>c__DisplayClass31_0<>8__1  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600017F  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000180  System.Void System.IDisposable.Dispose()
  RVA=0x06994734  token=0x6000181  System.Boolean MoveNext()
  RVA=0x06994870  token=0x6000183  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass32_0
TYPE:  sealed class
TOKEN: 0x2000040
SIZE:  0x20
FIELDS:
  public            Beyond.Misc.ObjectRef           outRef  // 0x10
  public            System.Action<Beyond.Misc.ObjectRef>callback  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000185  System.Void .ctor()
  RVA=0x06994374  token=0x6000186  System.Void <AlertDialogWithResult>b__0()
END_CLASS

CLASS: <AlertDialogWithResult>d__32
TYPE:  sealed class
TOKEN: 0x2000041
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Misc.ObjectRef           outRef  // 0x20
  public            System.Action<Beyond.Misc.ObjectRef>callback  // 0x28
  public            Beyond.LoginManager             <>4__this  // 0x30
  public            System.String                   desc  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000187  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000188  System.Void System.IDisposable.Dispose()
  RVA=0x06994004  token=0x6000189  System.Boolean MoveNext()
  RVA=0x0699415C  token=0x600018B  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass33_0
TYPE:  sealed class
TOKEN: 0x2000042
SIZE:  0x18
FIELDS:
  public            System.Object                   wait  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600018D  System.Void .ctor()
  RVA=0x040E20A0  token=0x600018E  System.Void <AlertDialogWithConfirm>b__0()
END_CLASS

CLASS: <AlertDialogWithConfirm>d__33
TYPE:  sealed class
TOKEN: 0x2000043
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.LoginManager             <>4__this  // 0x20
  public            System.String                   desc  // 0x28
  private           Beyond.LoginManager.<>c__DisplayClass33_0<>8__1  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600018F  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000190  System.Void System.IDisposable.Dispose()
  RVA=0x06993E68  token=0x6000191  System.Boolean MoveNext()
  RVA=0x06993FB8  token=0x6000193  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_ShowDialogCoroutine>d__37`2
TYPE:  sealed class
TOKEN: 0x2000044
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           System.Object                   <>2__current  // 0x0
  public            Beyond.LoginManager             <>4__this  // 0x0
  public            System.String                   resPath  // 0x0
  public            System.Action<TDialog,TParam>   setParamFunc  // 0x0
  public            TParam                          param  // 0x0
  private           Beyond.Misc.ObjectRef           <outRef>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000195  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000196  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000197  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000199  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass39_0
TYPE:  sealed class
TOKEN: 0x2000045
SIZE:  0x18
FIELDS:
  public            System.Action                   callback  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600019B  System.Void .ctor()
  RVA=0x02A729E0  token=0x600019C  System.Void <MockManualSelectServer>b__0(System.String _)
END_CLASS

CLASS: <>c__DisplayClass40_0
TYPE:  sealed class
TOKEN: 0x2000046
SIZE:  0x20
FIELDS:
  public            Beyond.LoginController          controller  // 0x10
  public            Beyond.LoginManager             <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600019D  System.Void .ctor()
  RVA=0x04CA7390  token=0x600019E  System.Void <_Init>b__0()
END_CLASS

CLASS: <_LoginWorkFlowCoroutine>d__43
TYPE:  sealed class
TOKEN: 0x2000047
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.LoginManager             <>4__this  // 0x20
  public            Beyond.LoginController          nullableCtrl  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600019F  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60001A0  System.Void System.IDisposable.Dispose()
  RVA=0x041E4CB0  token=0x60001A1  System.Boolean MoveNext()
  RVA=0x06994E58  token=0x60001A3  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <FetchRemoteConfigUtilSuc>d__45
TYPE:  sealed class
TOKEN: 0x2000048
SIZE:  0x90
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Cfg.IRemoteConfig        config  // 0x20
  public            System.String                   configEnv  // 0x28
  public            System.String                   keyErrorWithCode  // 0x30
  public            System.String                   keyTimeout  // 0x38
  private           Beyond.Cfg.FRemoteConfigRequest <request>5__2  // 0x40
  private           System.Boolean                  <isServiceSuc>5__3  // 0x80
  private           Beyond.Misc.ObjectRef           <resultRef>5__4  // 0x88
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60001A5  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60001A6  System.Void System.IDisposable.Dispose()
  RVA=0x03C0A020  token=0x60001A7  System.Boolean MoveNext()
  RVA=0x069941F4  token=0x60001A9  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: SplashConfig
TYPE:  sealed struct
TOKEN: 0x2000053
SIZE:  0x20
FIELDS:
  public            UnityEngine.Sprite              sprite  // 0x10
  public            System.Boolean                  blackCoverFadeOut  // 0x18
METHODS:
END_CLASS

CLASS: SplashConfigOverride
TYPE:  sealed struct
TOKEN: 0x2000054
SIZE:  0x20
FIELDS:
  public            Beyond.GEnums.EnvLang           envLang  // 0x10
  public            Beyond.UI.SplashController.SplashConfig[]splashConfigs  // 0x18
METHODS:
END_CLASS

CLASS: SplashConfigs
TYPE:  sealed struct
TOKEN: 0x2000055
SIZE:  0x28
FIELDS:
  public            Beyond.UI.SplashController.SplashConfig[]splashConfigs  // 0x10
  public            Beyond.UI.SplashController.SplashConfig[]splashConfigsVNMobile  // 0x18
  public            Beyond.UI.SplashController.SplashConfigOverride[]i18nSplashConfigOverrides  // 0x20
METHODS:
END_CLASS

CLASS: <>c__DisplayClass31_0
TYPE:  sealed class
TOKEN: 0x2000056
SIZE:  0x28
FIELDS:
  public            Beyond.UI.SplashController.SplashConfigconfig  // 0x10
  public            Beyond.UI.SplashController      <>4__this  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60001D2  System.Void .ctor()
  RVA=0x04D25EF0  token=0x60001D3  System.Void <_DoStartSplash>b__0()
  RVA=0x04CF4D60  token=0x60001D4  System.Void <_DoStartSplash>b__2()
END_CLASS

CLASS: <_OnSplashCompleteCoroutine>d__33
TYPE:  sealed class
TOKEN: 0x2000057
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.UI.SplashController      <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60001D5  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60001D6  System.Void System.IDisposable.Dispose()
  RVA=0x03811690  token=0x60001D7  System.Boolean MoveNext()
  RVA=0x0699DCF0  token=0x60001D9  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_StartLicenseCoroutine>d__34
TYPE:  sealed class
TOKEN: 0x2000058
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  private           System.Boolean                  <displayCursor>5__2  // 0x20
  private           System.Boolean                  <usingController>5__3  // 0x21
  private           System.Boolean                  <isGranted>5__4  // 0x22
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60001DB  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60001DC  System.Void System.IDisposable.Dispose()
  RVA=0x03A72270  token=0x60001DD  System.Boolean MoveNext()
  RVA=0x0699DD88  token=0x60001DF  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: HttpPostRequest
TYPE:  sealed struct
TOKEN: 0x200005B
SIZE:  0x38
FIELDS:
  public            System.String                   url  // 0x10
  public            System.String                   param  // 0x18
  public            System.String                   contentType  // 0x20
  public            System.Collections.Generic.Dictionary<System.String,System.String>header  // 0x28
  public            System.Action<Beyond.SDK.SDKNetUtils.WebHttpResponse>callback  // 0x30
METHODS:
END_CLASS

CLASS: HttpGetRequest
TYPE:  sealed struct
TOKEN: 0x200005C
SIZE:  0x20
FIELDS:
  public            System.String                   url  // 0x10
  public            System.Action<Beyond.SDK.SDKNetUtils.WebHttpResponse>callback  // 0x18
METHODS:
END_CLASS

CLASS: WebHttpResponse
TYPE:  sealed struct
TOKEN: 0x200005D
SIZE:  0x40
FIELDS:
  public            System.Boolean                  isTimeout  // 0x10
  public            System.Boolean                  isError  // 0x11
  public            System.Int64                    responseCode  // 0x18
  public            System.Collections.Generic.Dictionary<System.String,System.String>header  // 0x20
  public            System.String                   text  // 0x28
  public            System.Byte[]                   data  // 0x30
  public            System.String                   error  // 0x38
METHODS:
END_CLASS

CLASS: HTTPOptType
TYPE:  sealed struct
TOKEN: 0x200005E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.SDK.SDKNetUtils.HTTPOptTypeGET  // const
  public    static  Beyond.SDK.SDKNetUtils.HTTPOptTypePOST  // const
METHODS:
END_CLASS

CLASS: HTTPConfig
TYPE:  sealed struct
TOKEN: 0x200005F
SIZE:  0x40
FIELDS:
  public            System.String                   url  // 0x10
  public            System.String                   body  // 0x18
  public            System.String                   contentType  // 0x20
  public            System.Collections.Generic.Dictionary<System.String,System.String>header  // 0x28
  public            Beyond.SDK.SDKNetUtils.HTTPOptTypeoptType  // 0x30
  public            System.Action<Beyond.SDK.SDKNetUtils.WebHttpResponse>callback  // 0x38
METHODS:
END_CLASS

CLASS: HTTPImpl
TYPE:  abstract class
TOKEN: 0x2000060
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001E6  System.Collections.IEnumerator DoHTTP(Beyond.SDK.SDKNetUtils.HTTPConfig config)
  RVA=0x041E1670  token=0x60001E7  System.Void .ctor()
END_CLASS

CLASS: <DoHTTP>d__0
TYPE:  sealed class
TOKEN: 0x2000062
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.SDK.SDKNetUtils.HTTPConfigconfig  // 0x20
  private           Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result><sendTask>5__2  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60001EA  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60001EB  System.Void System.IDisposable.Dispose()
  RVA=0x0699BDC0  token=0x60001EC  System.Boolean MoveNext()
  RVA=0x0699C05C  token=0x60001EE  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: BeyondHttp
TYPE:  class
TOKEN: 0x2000061
SIZE:  0x10
EXTENDS: HTTPImpl
FIELDS:
METHODS:
  RVA=0x069964CC  token=0x60001E8  System.Collections.IEnumerator DoHTTP(Beyond.SDK.SDKNetUtils.HTTPConfig config)
  RVA=0x041E1670  token=0x60001E9  System.Void .ctor()
END_CLASS

CLASS: FHGSDKInit
TYPE:  sealed struct
TOKEN: 0x2000065
SIZE:  0x12
FIELDS:
  public            System.Boolean                  isIniting  // 0x10
  public            System.Boolean                  isInited  // 0x11
METHODS:
END_CLASS

CLASS: <FetchCaptchaCoroutine>d__3
TYPE:  sealed class
TOKEN: 0x2000067
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.String                   captchaParams  // 0x20
  public            Beyond.SDK.Impl.U8ExternalTools.CaptchaHandler<>4__this  // 0x28
  public            U8.SDK.SDKCaptchaHandler.Result outResult  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000215  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000216  System.Void System.IDisposable.Dispose()
  RVA=0x0699C400  token=0x6000217  System.Boolean MoveNext()
  RVA=0x0699C4F4  token=0x6000219  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: CaptchaHandler
TYPE:  class
TOKEN: 0x2000066
SIZE:  0x20
EXTENDS: U8.SDK.SDKCaptchaHandler
FIELDS:
  private   static  System.Int32                    FAILED  // const
  private           System.Int64                    m_flag  // 0x10
  private           System.String                   m_desc  // 0x18
METHODS:
  RVA=0x06996594  token=0x6000212  System.Collections.IEnumerator FetchCaptchaCoroutine(System.String captchaParams, U8.SDK.SDKCaptchaHandler.Result outResult)
  RVA=0x0699665C  token=0x6000213  System.Void OnGT3Message(System.Collections.Generic.Dictionary<System.String,System.Object> msg)
  RVA=0x04D8CCA0  token=0x6000214  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000068
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.SDK.Impl.U8ExternalTools.<>c<>9  // static @ 0x0
  public    static  System.Action<System.Object>    <>9__9_0  // static @ 0x8
  public    static  System.Action<System.String>    <>9__9_1  // static @ 0x10
  public    static  System.Action                   <>9__27_0  // static @ 0x18
METHODS:
  RVA=0x04D4AF50  token=0x600021B  System.Void .cctor()
  RVA=0x041E1670  token=0x600021C  System.Void .ctor()
  RVA=0x0699C540  token=0x600021D  System.Void <InitSDKSystem>b__9_0(System.Object error)
  RVA=0x035A38E0  token=0x600021E  System.Void <InitSDKSystem>b__9_1(System.String rawExtConfig)
  RVA=0x0699C70C  token=0x600021F  System.Void <_ProcessExtraInfo>b__27_0()
END_CLASS

CLASS: <WaitForInited>d__10
TYPE:  sealed class
TOKEN: 0x2000069
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000220  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000221  System.Void System.IDisposable.Dispose()
  RVA=0x04B4A840  token=0x6000222  System.Boolean MoveNext()
  RVA=0x0699C94C  token=0x6000224  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <WaitForInitHGSDK>d__15
TYPE:  sealed class
TOKEN: 0x200006A
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000226  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000227  System.Void System.IDisposable.Dispose()
  RVA=0x04CD0580  token=0x6000228  System.Boolean MoveNext()
  RVA=0x0699C900  token=0x600022A  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass25_0
TYPE:  sealed class
TOKEN: 0x200006B
SIZE:  0x18
FIELDS:
  public            System.Action<U8.SDK.SDKExternalTools.POSTResult>callback  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600022C  System.Void .ctor()
  RVA=0x0699C7C0  token=0x600022D  System.Void <POSTImplementation>b__0(Beyond.SDK.SDKNetUtils.WebHttpResponse webRequest)
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x200006D
SIZE:  0x30
FIELDS:
  public            U8.SDK.ExternalPluginPayParams  args  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600023A  System.Void .ctor()
  RVA=0x0699C844  token=0x600023B  System.Void <Pay>b__0(U8.SDK.U8PayResult payResult)
  RVA=0x0699C87C  token=0x600023C  System.Void <Pay>b__1(System.Object failMsg)
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed class
TOKEN: 0x200006E
SIZE:  0x38
FIELDS:
  public            U8.SDK.ExternalPluginLoginParamsargs  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600023D  System.Void .ctor()
  RVA=0x03074E90  token=0x600023E  System.Void <_MockLogin>b__0(System.String uid)
END_CLASS

CLASS: AppVersionRespone
TYPE:  class
TOKEN: 0x2000070
SIZE:  0x18
FIELDS:
  public            System.Int32                    action  // 0x10
  public            System.Int32                    state  // 0x14
METHODS:
  RVA=0x041E1670  token=0x600024A  System.Void .ctor()
END_CLASS

CLASS: <_CheckForUpdateCoroutine>d__4
TYPE:  sealed class
TOKEN: 0x2000071
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginCheckForUpdateNode<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600024B  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600024C  System.Void System.IDisposable.Dispose()
  RVA=0x0340A370  token=0x600024D  System.Boolean MoveNext()
  RVA=0x0699C998  token=0x600024F  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_AlertAndQuitForExtraConfigFatalError>d__5
TYPE:  sealed class
TOKEN: 0x2000072
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000251  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000252  System.Void System.IDisposable.Dispose()
  RVA=0x014F7CD0  token=0x6000253  System.Boolean MoveNext()
  RVA=0x014F90C0  token=0x6000255  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <AlertNoMatchDeviceBeforeLogin>d__6
TYPE:  sealed class
TOKEN: 0x2000073
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000257  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000258  System.Void System.IDisposable.Dispose()
  RVA=0x014F60E0  token=0x6000259  System.Boolean MoveNext()
  RVA=0x014F7C80  token=0x600025B  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_CheckForUpdateCoroutineMobile>d__7
TYPE:  sealed class
TOKEN: 0x2000074
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginCheckForUpdateNode<>4__this  // 0x20
  private           Beyond.Login.LoginGameUpdater.Result<result>5__2  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600025D  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600025E  System.Void System.IDisposable.Dispose()
  RVA=0x014F9110  token=0x600025F  System.Boolean MoveNext()
  RVA=0x014FA7A0  token=0x6000261  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_CheckForUpdateCoroutinePC>d__9
TYPE:  sealed class
TOKEN: 0x2000075
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginCheckForUpdateNode<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000263  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000264  System.Void System.IDisposable.Dispose()
  RVA=0x014FA7F0  token=0x6000265  System.Boolean MoveNext()
  RVA=0x014FAF80  token=0x6000267  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_CheckForUpdatePC>d__10
TYPE:  sealed class
TOKEN: 0x2000076
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginCheckForUpdateNode<>4__this  // 0x20
  private           Beyond.Misc.ObjectRef           <actionRef>5__2  // 0x28
  private           System.String                   <errorInfo>5__3  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000269  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600026A  System.Void System.IDisposable.Dispose()
  RVA=0x014FAFD0  token=0x600026B  System.Boolean MoveNext()
  RVA=0x014FD090  token=0x600026D  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_GetOnlineAppVersion>d__12
TYPE:  sealed class
TOKEN: 0x2000077
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginCheckForUpdateNode<>4__this  // 0x20
  public            Beyond.Misc.ObjectRef           outRef  // 0x28
  private           System.String                   <url>5__2  // 0x30
  private           Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result<Beyond.Login.LoginCheckForUpdateNode.AppVersionRespone>><httpResIndex>5__3  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600026F  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000270  System.Void System.IDisposable.Dispose()
  RVA=0x014FD0E0  token=0x6000271  System.Boolean MoveNext()
  RVA=0x014FF2C0  token=0x6000273  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: WorkProcess
TYPE:  class
TOKEN: 0x2000079
SIZE:  0x20
EXTENDS: UnityEngine.CustomYieldInstruction
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Beyond.Login.LoginCreateRoleNodem_closure  // 0x10
  private           System.Boolean                  m_isFinished  // 0x18
PROPERTIES:
  keepWaiting  get=0x069A00CC
METHODS:
  RVA=0x053908C0  token=0x600027C  System.Void .ctor(Beyond.Login.LoginCreateRoleNode closure)
  RVA=0x069A0058  token=0x600027E  System.Void Dispose()
END_CLASS

CLASS: <>c__DisplayClass24_0
TYPE:  sealed class
TOKEN: 0x200007B
SIZE:  0x20
FIELDS:
  public            Beyond.Login.LoginDownloadTask  <>4__this  // 0x10
  public            Beyond.SDK.HGDownloader.PauseReasonreason  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600028E  System.Void .ctor()
  RVA=0x0699C798  token=0x600028F  System.Void <_OnDownloadPaused>b__1()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200007C
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.Login.LoginDownloadTask.<>c<>9  // static @ 0x0
  public    static  System.Action                   <>9__24_0  // static @ 0x8
METHODS:
  RVA=0x0699C89C  token=0x6000290  System.Void .cctor()
  RVA=0x041E1670  token=0x6000291  System.Void .ctor()
  RVA=0x0699C6E8  token=0x6000292  System.Void <_OnDownloadPaused>b__24_0()
END_CLASS

CLASS: <_GameUpdateCoroutine>d__5
TYPE:  sealed class
TOKEN: 0x200007F
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600029E  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600029F  System.Void System.IDisposable.Dispose()
  RVA=0x04D83CA0  token=0x60002A0  System.Boolean MoveNext()
  RVA=0x0699D2E4  token=0x60002A2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_GameUpdateCoroutineMobileImpl>d__6
TYPE:  sealed class
TOKEN: 0x2000080
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginGameUpdateNode<>4__this  // 0x20
  private           Beyond.Login.LoginGameUpdater.Result<result>5__2  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002A4  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002A5  System.Void System.IDisposable.Dispose()
  RVA=0x0699D0BC  token=0x60002A6  System.Boolean MoveNext()
  RVA=0x0699D298  token=0x60002A8  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: CodeType
TYPE:  sealed struct
TOKEN: 0x2000082
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Login.LoginGameUpdater.CodeTypeCODE_2103  // const
  public    static  Beyond.Login.LoginGameUpdater.CodeTypeCODE_2105  // const
  public    static  Beyond.Login.LoginGameUpdater.CodeTypeCODE_2200  // const
  public    static  Beyond.Login.LoginGameUpdater.CodeTypeCODE_2201  // const
  public    static  Beyond.Login.LoginGameUpdater.CodeTypeCODE_2202  // const
  public    static  Beyond.Login.LoginGameUpdater.CodeTypeCODE_2203  // const
  public    static  Beyond.Login.LoginGameUpdater.CodeTypeCODE_2204  // const
  public    static  Beyond.Login.LoginGameUpdater.CodeTypeCODE_2205  // const
  public    static  Beyond.Login.LoginGameUpdater.CodeTypeCODE_2206  // const
  public    static  Beyond.Login.LoginGameUpdater.CodeTypeCODE_2207  // const
  public    static  Beyond.Login.LoginGameUpdater.CodeTypeCODE_2208  // const
  public    static  Beyond.Login.LoginGameUpdater.CodeTypeCODE_2209  // const
METHODS:
END_CLASS

CLASS: UpdateType
TYPE:  sealed struct
TOKEN: 0x2000083
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Login.LoginGameUpdater.UpdateTypeFullUpdate  // const
  public    static  Beyond.Login.LoginGameUpdater.UpdateTypeIncrementalUpdate  // const
  public    static  Beyond.Login.LoginGameUpdater.UpdateTypeGoToBilibile  // const
  public    static  Beyond.Login.LoginGameUpdater.UpdateTypeGoToTaptap  // const
  public    static  Beyond.Login.LoginGameUpdater.UpdateTypeGoToAppStore  // const
  public    static  Beyond.Login.LoginGameUpdater.UpdateTypeGoToGooglePlay  // const
  public    static  Beyond.Login.LoginGameUpdater.UpdateTypeGoToSamsungStore  // const
  public    static  Beyond.Login.LoginGameUpdater.UpdateTypeGoToRustore  // const
METHODS:
END_CLASS

CLASS: UpdateCode
TYPE:  sealed struct
TOKEN: 0x2000084
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Login.LoginGameUpdater.UpdateCodeSuccess  // const
METHODS:
END_CLASS

CLASS: UpdateAction
TYPE:  sealed struct
TOKEN: 0x2000085
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Login.LoginGameUpdater.UpdateActionInvalid  // const
  public    static  Beyond.Login.LoginGameUpdater.UpdateActionLatest  // const
  public    static  Beyond.Login.LoginGameUpdater.UpdateActionForcedUpdate  // const
  public    static  Beyond.Login.LoginGameUpdater.UpdateActionOptionalUpdate  // const
  public    static  Beyond.Login.LoginGameUpdater.UpdateActionError  // const
  public    static  Beyond.Login.LoginGameUpdater.UpdateActionCongestion  // const
METHODS:
END_CLASS

CLASS: HGLatestGameInfo
TYPE:  class
TOKEN: 0x2000086
SIZE:  0x38
FIELDS:
  public            System.Int32                    code  // 0x10
  public            System.String                   version  // 0x18
  public            System.Int32                    action  // 0x20
  public            System.Int32                    updateType  // 0x24
  public            System.String                   updateInfo  // 0x28
  public            System.Int32                    state  // 0x30
METHODS:
  RVA=0x041E1670  token=0x60002BB  System.Void .ctor()
END_CLASS

CLASS: NotificationTitleConfig
TYPE:  class
TOKEN: 0x2000087
SIZE:  0x20
FIELDS:
  public            System.String                   downloading  // 0x10
  public            System.String                   pause  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60002BC  System.Void .ctor()
END_CLASS

CLASS: HGUpdateConfig
TYPE:  class
TOKEN: 0x2000088
SIZE:  0x18
FIELDS:
  public            System.String                   downloadPath  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60002BD  System.Void .ctor()
END_CLASS

CLASS: Options
TYPE:  sealed struct
TOKEN: 0x2000089
SIZE:  0x28
FIELDS:
  public            Beyond.Login.NetUsagePolicy     netUsagePolicy  // 0x10
  public            System.Action<System.String,System.Action>alertDialog  // 0x18
  public            Beyond.LoginContext             loginContext  // 0x20
METHODS:
END_CLASS

CLASS: Result
TYPE:  class
TOKEN: 0x200008A
SIZE:  0x28
FIELDS:
  public            System.Boolean                  needRetry  // 0x10
  public            System.String                   errorInfo  // 0x18
  public            System.Int32                    code  // 0x20
  public            System.Boolean                  auditing  // 0x24
METHODS:
  RVA=0x041E1670  token=0x60002BE  System.Void .ctor()
END_CLASS

CLASS: Context
TYPE:  class
TOKEN: 0x200008B
SIZE:  0x40
FIELDS:
  public            System.Int64                    taskId  // 0x10
  public            System.String                   errorInfo  // 0x18
  public            System.String                   logError  // 0x20
  public            System.Boolean                  needClear  // 0x28
  public            Beyond.Login.LoginGameUpdater.HGLatestGameInfogameInfo  // 0x30
  private           System.Boolean                  <complete>k__BackingField  // 0x38
  private           System.Int32                    <code>k__BackingField  // 0x3c
PROPERTIES:
  complete  get=0x011F2A70  set=0x011F2A80
  code  get=0x04D86640  set=0x04D86670
METHODS:
  RVA=0x06996750  token=0x60002C3  System.Void CompleteWithCode(System.Int32 pCode)
  RVA=0x069967AC  token=0x60002C4  System.Void UncompleteWithCode(System.Int32 pCode)
  RVA=0x06996808  token=0x60002C5  System.Void .ctor()
END_CLASS

CLASS: <CheckLastGameInfo>d__23
TYPE:  sealed class
TOKEN: 0x200008C
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginGameUpdater   <>4__this  // 0x20
  public            Beyond.Login.LoginGameUpdater.Resultresult  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002C6  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002C7  System.Void System.IDisposable.Dispose()
  RVA=0x0699BB30  token=0x60002C8  System.Boolean MoveNext()
  RVA=0x0699BD74  token=0x60002CA  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <DoUpdate>d__24
TYPE:  sealed class
TOKEN: 0x200008D
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginGameUpdater   <>4__this  // 0x20
  public            Beyond.Login.LoginGameUpdater.Resultresult  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002CC  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002CD  System.Void System.IDisposable.Dispose()
  RVA=0x0699C0A8  token=0x60002CE  System.Boolean MoveNext()
  RVA=0x0699C3B4  token=0x60002D0  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_GetLastGameInfo>d__25
TYPE:  sealed class
TOKEN: 0x200008E
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginGameUpdater   <>4__this  // 0x20
  public            Beyond.Login.LoginGameUpdater.Contextcontext  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002D2  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002D3  System.Void System.IDisposable.Dispose()
  RVA=0x0699D330  token=0x60002D4  System.Boolean MoveNext()
  RVA=0x0699D9A8  token=0x60002D6  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_UpdateGame>d__26
TYPE:  sealed class
TOKEN: 0x200008F
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginGameUpdater   <>4__this  // 0x20
  public            Beyond.Login.LoginGameUpdater.Contextcontext  // 0x28
  private           Beyond.Login.LoginGameUpdater.HGLatestGameInfo<gameInfo>5__2  // 0x30
  private           System.Int64                    <size>5__3  // 0x38
  private           System.Boolean                  <enableMobileData>5__4  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002D8  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002D9  System.Void System.IDisposable.Dispose()
  RVA=0x0699DDD4  token=0x60002DA  System.Boolean MoveNext()
  RVA=0x0699E4A4  token=0x60002DC  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_DoTaskUpdating>d__27
TYPE:  sealed class
TOKEN: 0x2000090
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginGameUpdater   <>4__this  // 0x20
  public            Beyond.Login.LoginGameUpdater.Contextcontext  // 0x28
  private           System.Int64                    <taskId>5__2  // 0x30
  private           System.Int32                    <status>5__3  // 0x38
  private           System.Int64                    <curSize>5__4  // 0x40
  private           System.Int64                    <totalSize>5__5  // 0x48
  private           System.Int64                    <speed>5__6  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002DE  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002DF  System.Void System.IDisposable.Dispose()
  RVA=0x0699CA30  token=0x60002E0  System.Boolean MoveNext()
  RVA=0x0699D024  token=0x60002E2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass32_0
TYPE:  sealed class
TOKEN: 0x2000091
SIZE:  0x18
FIELDS:
  public            System.Boolean                  wait  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60002E4  System.Void .ctor()
  RVA=0x04D8CCB0  token=0x60002E5  System.Void <_YieldErrorAlert>b__0()
END_CLASS

CLASS: <_YieldErrorAlert>d__32
TYPE:  sealed class
TOKEN: 0x2000092
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginGameUpdater   <>4__this  // 0x20
  public            System.String                   desc  // 0x28
  private           Beyond.Login.LoginGameUpdater.<>c__DisplayClass32_0<>8__1  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002E6  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002E7  System.Void System.IDisposable.Dispose()
  RVA=0x0699E99C  token=0x60002E8  System.Boolean MoveNext()
  RVA=0x0699EAD8  token=0x60002EA  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass33_0
TYPE:  sealed class
TOKEN: 0x2000093
SIZE:  0x18
FIELDS:
  public            System.Boolean                  rejected  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60002EC  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass33_1
TYPE:  sealed class
TOKEN: 0x2000094
SIZE:  0x20
FIELDS:
  public            System.Boolean                  complete  // 0x10
  public            Beyond.Login.LoginGameUpdater.<>c__DisplayClass33_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60002ED  System.Void .ctor()
  RVA=0x015EFCD0  token=0x60002EE  System.Void <_YieldCheckNetUsagePolicy>b__0()
  RVA=0x0699C824  token=0x60002EF  System.Void <_YieldCheckNetUsagePolicy>b__1()
  RVA=0x015EFCD0  token=0x60002F0  System.Void <_YieldCheckNetUsagePolicy>b__2()
END_CLASS

CLASS: <_YieldCheckNetUsagePolicy>d__33
TYPE:  sealed class
TOKEN: 0x2000095
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  private           Beyond.Login.LoginGameUpdater.<>c__DisplayClass33_0<>8__1  // 0x20
  public            Beyond.Login.LoginGameUpdater   <>4__this  // 0x28
  public            System.Int64                    downloadSize  // 0x30
  private           Beyond.Login.LoginGameUpdater.<>c__DisplayClass33_1<>8__2  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002F1  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002F2  System.Void System.IDisposable.Dispose()
  RVA=0x0699E6E4  token=0x60002F3  System.Boolean MoveNext()
  RVA=0x0699E950  token=0x60002F5  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_WaitForTaskCancel>d__34
TYPE:  sealed class
TOKEN: 0x2000096
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginGameUpdater   <>4__this  // 0x20
  public            System.Int64                    taskId  // 0x28
  private           System.Boolean                  <isCanceled>5__2  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002F7  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002F8  System.Void System.IDisposable.Dispose()
  RVA=0x0699E4F0  token=0x60002F9  System.Boolean MoveNext()
  RVA=0x0699E570  token=0x60002FB  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_EnterGameCoroutine>d__11
TYPE:  sealed class
TOKEN: 0x2000099
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginGSNode.EnterGameServiceFlow<>4__this  // 0x20
  private           U8.SDK.SDKPromiseEnumerator<U8.SDK.U8AuthV2Result><authPromise>5__2  // 0x28
  private           System.String                   <token>5__3  // 0x30
  private           System.String                   <uid>5__4  // 0x38
  private           Beyond.Misc.ObjectRef           <loginRespRef>5__5  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600031B  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600031C  System.Void System.IDisposable.Dispose()
  RVA=0x03A853B0  token=0x600031D  System.Boolean MoveNext()
  RVA=0x0699D070  token=0x600031F  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_NetConnectAndGSLogin>d__12
TYPE:  sealed class
TOKEN: 0x200009A
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Misc.ObjectRef           loginRespRef  // 0x20
  public            Beyond.Login.LoginGSNode.EnterGameServiceFlow<>4__this  // 0x28
  public            System.String                   uid  // 0x30
  public            System.String                   token  // 0x38
  private           Beyond.Network.HGNetSessionConnectYield<connectTask>5__2  // 0x40
  private           System.Int32                    <maxRetryCount>5__3  // 0x48
  private           System.Int32                    <currentRetry>5__4  // 0x4c
  private           Beyond.Network.HGNetSessionLoginYield<loginHandler>5__5  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000321  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000322  System.Void System.IDisposable.Dispose()
  RVA=0x03A4B510  token=0x6000323  System.Boolean MoveNext()
  RVA=0x0699DB98  token=0x6000325  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_YieldAlertError>d__14
TYPE:  sealed class
TOKEN: 0x200009B
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.String                   errorInfo  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000327  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000328  System.Void System.IDisposable.Dispose()
  RVA=0x0699E608  token=0x6000329  System.Boolean MoveNext()
  RVA=0x0699E698  token=0x600032B  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_SaveCurrentUserToServerConfig>d__15
TYPE:  sealed class
TOKEN: 0x200009C
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginGSNode.EnterGameServiceFlow<>4__this  // 0x20
  public            System.String                   uid  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600032D  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600032E  System.Void System.IDisposable.Dispose()
  RVA=0x03DA3230  token=0x600032F  System.Boolean MoveNext()
  RVA=0x0699DD3C  token=0x6000331  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass16_0
TYPE:  sealed class
TOKEN: 0x200009D
SIZE:  0x18
FIELDS:
  public            Beyond.Misc.ObjectRef           waitForCallback  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000333  System.Void .ctor()
  RVA=0x0699C774  token=0x6000334  System.Void <_MockManualSelectServer>b__0()
END_CLASS

CLASS: <_MockManualSelectServer>d__16
TYPE:  sealed class
TOKEN: 0x200009E
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.String                   uid  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000335  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000336  System.Void System.IDisposable.Dispose()
  RVA=0x0699D9F4  token=0x6000337  System.Boolean MoveNext()
  RVA=0x0699DB4C  token=0x6000339  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_OnAuthError>d__17
TYPE:  sealed class
TOKEN: 0x200009F
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Object                   rejectInfo  // 0x20
  public            Beyond.Login.LoginGSNode.EnterGameServiceFlow<>4__this  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600033B  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600033C  System.Void System.IDisposable.Dispose()
  RVA=0x0699DBE4  token=0x600033D  System.Boolean MoveNext()
  RVA=0x0699DCA4  token=0x600033F  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: EnterGameServiceFlow
TYPE:  class
TOKEN: 0x2000098
SIZE:  0x30
EXTENDS: UnityEngine.CustomYieldInstruction
FIELDS:
  private           Beyond.LoginContext             m_context  // 0x10
  private           Beyond.Login.LoginGSNode        m_curNode  // 0x18
  private           Beyond.Misc.ObjectRef           m_dialogBlocker  // 0x20
  private           System.Int32                    m_state  // 0x28
PROPERTIES:
  keepWaiting  get=0x0404E110
METHODS:
  RVA=0x049D6330  token=0x600030C  System.Void .ctor(Beyond.Login.LoginGSNode node)
  RVA=0x069968D0  token=0x600030D  System.Void MarkAborting()
  RVA=0x03A84270  token=0x600030E  System.Void EnterGame()
  RVA=0x06996870  token=0x600030F  System.Boolean IsInterruptable()
  RVA=0x06996928  token=0x6000310  System.Boolean TryInterrupt()
  RVA=0x03A84390  token=0x6000311  System.Collections.IEnumerator _EnterGameCoroutine()
  RVA=0x03A84400  token=0x6000312  System.Collections.IEnumerator _NetConnectAndGSLogin(System.String uid, System.String token, Beyond.Misc.ObjectRef loginRespRef)
  RVA=0x06996990  token=0x6000313  System.String _ExtractErrorInfo(Beyond.Network.HttpSender.Result result, System.String keyErrorWithCode, System.String keyTimeout)
  RVA=0x06996C34  token=0x6000314  System.Collections.IEnumerator _YieldAlertError(System.String errorInfo)
  RVA=0x03A4B930  token=0x6000315  System.Collections.IEnumerator _SaveCurrentUserToServerConfig(System.String uid)
  RVA=0x06996A8C  token=0x6000316  System.Collections.IEnumerator _MockManualSelectServer(System.String uid)
  RVA=0x06996B28  token=0x6000317  System.Collections.IEnumerator _OnAuthError(System.Object rejectInfo)
  RVA=0x06996BD0  token=0x6000318  System.Void _ReLoginWhenErrorEncountered()
  RVA=0x040EB370  token=0x6000319  System.Void _GlobalLoginSucBusiness(Proto.MSG_B1 msg)
  RVA=0x04D84FC0  token=0x600031A  System.Boolean <_EnterGameCoroutine>b__11_0()
END_CLASS

CLASS: <_ConfirmServer>d__10
TYPE:  sealed class
TOKEN: 0x20000A0
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  private           U8.SDK.SDKPromiseEnumerator<U8.SDK.U8ConfirmServerResult><confirmPromise>5__2  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000341  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000342  System.Void System.IDisposable.Dispose()
  RVA=0x040E9A00  token=0x6000343  System.Boolean MoveNext()
  RVA=0x0699C9E4  token=0x6000345  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: IProgress
TYPE:  interface
TOKEN: 0x20000A3
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000349  System.Double GetTotal()
  RVA=-1  // abstract  token=0x600034A  System.Double GetCurrent()
  RVA=-1  // abstract  token=0x600034B  System.String GetInfo()
END_CLASS

CLASS: SimpleProgress
TYPE:  class
TOKEN: 0x20000A4
SIZE:  0x18
IMPLEMENTS: IProgress
FIELDS:
  public            System.Double                   current  // 0x10
METHODS:
  RVA=0x039367C0  token=0x600034C  System.Double GetCurrent()
  RVA=0x039369B0  token=0x600034D  System.Double GetTotal()
  RVA=0x039369E0  token=0x600034E  System.String GetInfo()
  RVA=0x041E1670  token=0x600034F  System.Void .ctor()
END_CLASS

CLASS: HotUpdateInfo
TYPE:  class
TOKEN: 0x20000A6
SIZE:  0x58
FIELDS:
  public            Beyond.Resource.HotUpdate.FResourceDiffresDiff  // 0x10
  public            Beyond.Resource.HotUpdate.LocalResPreflocalResPref  // 0x50
METHODS:
  RVA=0x06996CD0  token=0x600037C  System.Boolean CheckIfNeedReload()
  RVA=0x03DA4D90  token=0x600037D  System.Boolean HasChangedByType(Beyond.VFS.EVFSBlockType type)
  RVA=0x06996E7C  token=0x600037E  System.Boolean HasAssetsChanged()
  RVA=0x041E1670  token=0x600037F  System.Void .ctor()
END_CLASS

CLASS: WorkContext
TYPE:  class
TOKEN: 0x20000A7
SIZE:  0x48
FIELDS:
  public            Beyond.Login.LoginHotUpdater.HotUpdateInfoupdateInitial  // 0x10
  public            Beyond.Login.LoginHotUpdater.HotUpdateInfoupdateMain  // 0x18
  public            System.String                   vitalError  // 0x20
  public            System.Boolean                  reboot  // 0x28
  public            Beyond.Login.NetUsagePolicy     netUsagePolicy  // 0x30
  public            Beyond.Resource.HotUpdate.ResourceIndexresIndex  // 0x38
  private           System.Boolean                  <illegalItemsInLocalRes>k__BackingField  // 0x40
PROPERTIES:
  illegalItemsInLocalRes  get=0x02D480A0  set=0x02D480B0
METHODS:
  RVA=0x069A0008  token=0x6000382  System.Void MarkIllegalItemsInLocalRes()
  RVA=0x0699FFA4  token=0x6000383  Beyond.Login.LoginHotUpdater.HotUpdateInfo GetHotUpdateInfo(System.Boolean isInitial)
  RVA=0x03DA4D20  token=0x6000384  System.Boolean HasChangedByType(Beyond.VFS.EVFSBlockType type)
  RVA=0x03C075E0  token=0x6000385  System.Void .ctor()
END_CLASS

CLASS: <_Work>d__28
TYPE:  sealed class
TOKEN: 0x20000A8
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x20
  private           Beyond.Login.LoginHotUpdater.WorkContext<workContext>5__2  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000386  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000387  System.Void System.IDisposable.Dispose()
  RVA=0x03C07330  token=0x6000388  System.Boolean MoveNext()
  RVA=0x0699E5BC  token=0x600038A  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_CreateMainCoroutine>d__29
TYPE:  sealed class
TOKEN: 0x20000A9
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x20
  public            Beyond.Login.LoginHotUpdater.WorkContextworkContext  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600038C  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600038D  System.Void System.IDisposable.Dispose()
  RVA=0x0455F6A0  token=0x600038E  System.Boolean MoveNext()
  RVA=0x069A6914  token=0x6000390  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_MainCoroutineModeSkip>d__30
TYPE:  sealed class
TOKEN: 0x20000AA
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x20
  public            Beyond.Login.LoginHotUpdater.WorkContextcontext  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000392  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000393  System.Void System.IDisposable.Dispose()
  RVA=0x04B21A80  token=0x6000394  System.Boolean MoveNext()
  RVA=0x069AA11C  token=0x6000396  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_MainCoroutineModeLocal>d__31
TYPE:  sealed class
TOKEN: 0x20000AB
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x20
  public            Beyond.Login.LoginHotUpdater.WorkContextcontext  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000398  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000399  System.Void System.IDisposable.Dispose()
  RVA=0x069A9F90  token=0x600039A  System.Boolean MoveNext()
  RVA=0x069AA0D0  token=0x600039C  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_UpdateResVersion>d__32
TYPE:  sealed class
TOKEN: 0x20000AC
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x20
  public            Beyond.Login.LoginHotUpdater.WorkContextworkContext  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600039E  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600039F  System.Void System.IDisposable.Dispose()
  RVA=0x04B45BC0  token=0x60003A0  System.Boolean MoveNext()
  RVA=0x069AA388  token=0x60003A2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_UpdateResVersionDev>d__36
TYPE:  sealed class
TOKEN: 0x20000AD
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  private           Beyond.Cfg.ResVersionDevCfg     <config>5__2  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003A4  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003A5  System.Void System.IDisposable.Dispose()
  RVA=0x069AA200  token=0x60003A6  System.Boolean MoveNext()
  RVA=0x069AA2F0  token=0x60003A8  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_UpdateResVersionProd>d__38
TYPE:  sealed class
TOKEN: 0x20000AE
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater.WorkContextworkContext  // 0x20
  private           Beyond.Cfg.ResVersionCfg        <config>5__2  // 0x28
  private           Beyond.Misc.ObjectRef           <resultRef>5__3  // 0x30
  private           System.String                   <url>5__4  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003AA  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003AB  System.Void System.IDisposable.Dispose()
  RVA=0x03C07B40  token=0x60003AC  System.Boolean MoveNext()
  RVA=0x069AA33C  token=0x60003AE  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass43_0
TYPE:  sealed class
TOKEN: 0x20000AF
SIZE:  0x18
FIELDS:
  public            System.Boolean                  isInitial  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003B0  System.Void .ctor()
  RVA=0x069A2ACC  token=0x60003B1  Beyond.Resource.HotUpdate.LocalResPref <_MainCoroutineModeDownload>b__0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000B0
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.Login.LoginHotUpdater.<>c<>9  // static @ 0x0
  public    static  System.Action                   <>9__43_1  // static @ 0x8
METHODS:
  RVA=0x069A2C00  token=0x60003B2  System.Void .cctor()
  RVA=0x041E1670  token=0x60003B3  System.Void .ctor()
  RVA=0x069A2640  token=0x60003B4  System.Void <_MainCoroutineModeDownload>b__43_1()
END_CLASS

CLASS: <_MainCoroutineModeDownload>d__43
TYPE:  sealed class
TOKEN: 0x20000B1
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x20
  public            Beyond.Login.LoginHotUpdater.WorkContextworkContext  // 0x28
  private           Beyond.Login.LoginHotUpdater.<>c__DisplayClass43_0<>8__1  // 0x30
  private           System.Boolean                  <initialDownloaded>5__2  // 0x38
  private           System.Boolean                  <hasHotUpdateSucceeded>5__3  // 0x39
  private           System.Boolean                  <needDeleteInitial>5__4  // 0x3a
  private           System.Boolean                  <neewDeleteMain>5__5  // 0x3b
  private           System.Int32                    <initialOrMain>5__6  // 0x3c
  private           System.String                   <onlineResVersion>5__7  // 0x40
  private           Beyond.WaitForAsyncTask<Beyond.Resource.HotUpdate.LocalResPref><waitForResPref>5__8  // 0x48
  private           Beyond.Login.LoginHotUpdater.HotUpdateInfo<info>5__9  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003B5  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003B6  System.Void System.IDisposable.Dispose()
  RVA=0x069A89CC  token=0x60003B7  System.Boolean MoveNext()
  RVA=0x069A9F44  token=0x60003B9  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_ReloadResourceIndexes>d__46
TYPE:  sealed class
TOKEN: 0x20000B2
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater.WorkContextworkContext  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003BB  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003BC  System.Void System.IDisposable.Dispose()
  RVA=0x03DA6720  token=0x60003BD  System.Boolean MoveNext()
  RVA=0x069AA168  token=0x60003BF  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass47_0
TYPE:  sealed class
TOKEN: 0x20000B3
SIZE:  0x28
FIELDS:
  public            System.String                   filePath  // 0x10
  public            Beyond.Resource.HotUpdate.ResourceIndexresIndex  // 0x18
  public            System.Func<Beyond.Resource.HotUpdate.ResourceIndex><>9__1  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60003C1  System.Void .ctor()
  RVA=0x069A2B7C  token=0x60003C2  Beyond.Resource.HotUpdate.ResourceIndex <_LoadOrDownloadResIndex>b__1()
  RVA=0x069A2B40  token=0x60003C3  System.Object <_LoadOrDownloadResIndex>b__0()
END_CLASS

CLASS: <_LoadOrDownloadResIndex>d__47
TYPE:  sealed class
TOKEN: 0x20000B4
SIZE:  0x60
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Boolean                  isInitial  // 0x20
  private           Beyond.Login.LoginHotUpdater.<>c__DisplayClass47_0<>8__1  // 0x28
  public            System.String                   onlineVer  // 0x30
  public            Beyond.Misc.ObjectRef           resIndexRef  // 0x38
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x40
  private           System.String                   <indexUrl>5__2  // 0x48
  private           Beyond.Misc.ObjectRef           <resultRef>5__3  // 0x50
  private           Beyond.WaitForAsyncTask<Beyond.Resource.HotUpdate.ResourceIndex><loadTask>5__4  // 0x58
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003C4  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003C5  System.Void System.IDisposable.Dispose()
  RVA=0x069A7E48  token=0x60003C6  System.Boolean MoveNext()
  RVA=0x069A848C  token=0x60003C8  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_GenResIndex>d__48
TYPE:  sealed class
TOKEN: 0x20000B5
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x20
  public            System.String                   onlineVer  // 0x28
  public            System.Boolean                  isInitial  // 0x30
  public            Beyond.Login.LoginHotUpdater.WorkContextworkContext  // 0x38
  private           Beyond.Misc.ObjectRef           <resIndexRef>5__2  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003CA  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003CB  System.Void System.IDisposable.Dispose()
  RVA=0x069A715C  token=0x60003CC  System.Boolean MoveNext()
  RVA=0x069A7298  token=0x60003CE  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_GenRiff>d__49
TYPE:  sealed class
TOKEN: 0x20000B6
SIZE:  0x78
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater.WorkContextworkContext  // 0x20
  public            System.Boolean                  isInitial  // 0x28
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x30
  public            System.String                   onlineVer  // 0x38
  private           Beyond.Login.LoginHotUpdater.HotUpdateInfo<info>5__2  // 0x40
  private           Beyond.Resource.HotUpdate.LocalResPref<localResPref>5__3  // 0x48
  private           System.String                   <rootUrl>5__4  // 0x50
  private           System.String                   <patchUrl>5__5  // 0x58
  private           System.String                   <fileUrl>5__6  // 0x60
  private           Beyond.Resource.HotUpdate.ResourceIndex<resIndex>5__7  // 0x68
  private           Beyond.Misc.ObjectRef           <streamingIndexRef>5__8  // 0x70
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003D0  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003D1  System.Void System.IDisposable.Dispose()
  RVA=0x069A72E4  token=0x60003D2  System.Boolean MoveNext()
  RVA=0x069A7698  token=0x60003D4  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_GenLocalRef>d__51
TYPE:  sealed class
TOKEN: 0x20000B7
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater.WorkContextworkContext  // 0x20
  public            System.Boolean                  isInitial  // 0x28
  public            System.String                   onlineVer  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003D6  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003D7  System.Void System.IDisposable.Dispose()
  RVA=0x069A6FB0  token=0x60003D8  System.Boolean MoveNext()
  RVA=0x069A7110  token=0x60003DA  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_CheckAndDownloadResource>d__52
TYPE:  sealed class
TOKEN: 0x20000B8
SIZE:  0x78
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater.WorkContextworkContext  // 0x20
  public            System.Boolean                  isInitial  // 0x28
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x30
  public            System.String                   onlineVer  // 0x38
  private           Beyond.Login.LoginHotUpdater.HotUpdateInfo<info>5__2  // 0x40
  private           Beyond.Login.LoginHotUpdateModel<hotUpdateModel>5__3  // 0x48
  private           System.String                   <patchInfo>5__4  // 0x50
  private           Beyond.Login.LoginDownloadTask  <downloadTask>5__5  // 0x58
  private           Beyond.SDK.HGDownloader.WorkState<lastWorkState>5__6  // 0x60
  private           System.Boolean                  <progress5>5__7  // 0x64
  private           System.Boolean                  <progress25>5__8  // 0x65
  private           System.Boolean                  <progress50>5__9  // 0x66
  private           System.Boolean                  <progress75>5__10  // 0x67
  private           System.Boolean                  <progress90>5__11  // 0x68
  private           System.Boolean                  <downloadingShaderWarmupStarted>5__12  // 0x69
  private           Beyond.Misc.ObjectRef           <patchInfoRef>5__13  // 0x70
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003DC  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003DD  System.Void System.IDisposable.Dispose()
  RVA=0x069A3C84  token=0x60003DE  System.Boolean MoveNext()
  RVA=0x069A50E4  token=0x60003E0  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_DownloadPatchInfo>d__54
TYPE:  sealed class
TOKEN: 0x20000B9
SIZE:  0x50
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x20
  public            System.String                   onlineVer  // 0x28
  public            System.Boolean                  isInitial  // 0x30
  public            Beyond.Misc.ObjectRef           patchInfoRef  // 0x38
  private           System.String                   <patchInfoUrl>5__2  // 0x40
  private           Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result<Beyond.Resource.HotUpdate.PatchInfo>><httpResIndex>5__3  // 0x48
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003E2  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003E3  System.Void System.IDisposable.Dispose()
  RVA=0x069A6960  token=0x60003E4  System.Boolean MoveNext()
  RVA=0x069A6C38  token=0x60003E6  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_ClearDownloadFolder>d__55
TYPE:  sealed class
TOKEN: 0x20000BA
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x20
  public            System.Boolean                  needDeleteInitial  // 0x28
  public            System.Boolean                  needDeleteMain  // 0x29
  public            Beyond.Login.LoginHotUpdater.WorkContextworkContext  // 0x30
  private           System.Collections.Generic.List<Beyond.SDK.HGFolder><folderList>5__2  // 0x38
  private           System.Int32                    <initialOrMain>5__3  // 0x40
  private           System.Boolean                  <isInitial>5__4  // 0x44
  private           System.String                   <onlineVer>5__5  // 0x48
  private           Beyond.Login.LoginHotUpdater.HotUpdateInfo<info>5__6  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003E8  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003E9  System.Void System.IDisposable.Dispose()
  RVA=0x069A5C38  token=0x60003EA  System.Boolean MoveNext()
  RVA=0x069A68C8  token=0x60003EC  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_CheckLocalResConsistency>d__56
TYPE:  sealed class
TOKEN: 0x20000BB
SIZE:  0x68
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater.WorkContextcontext  // 0x20
  public            System.Boolean                  isInitial  // 0x28
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x30
  public            System.String                   onlineVer  // 0x38
  private           Beyond.Resource.HotUpdate.LocalResPref<localResInfo>5__2  // 0x40
  private           Beyond.Misc.ObjectRef           <resIndexRef>5__3  // 0x48
  private           System.Boolean                  <showProgress>5__4  // 0x50
  private           Beyond.WaitForAsyncTask<System.Object><checkTask>5__5  // 0x58
  private           Beyond.Resource.HotUpdate.LocalResChecker.CheckOutput<checkOutput>5__6  // 0x60
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003EE  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003EF  System.Void System.IDisposable.Dispose()
  RVA=0x069A5130  token=0x60003F0  System.Boolean MoveNext()
  RVA=0x069A5BEC  token=0x60003F2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_VitalError>d__57
TYPE:  sealed class
TOKEN: 0x20000BC
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginHotUpdater    <>4__this  // 0x20
  public            System.String                   error  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003F4  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003F5  System.Void System.IDisposable.Dispose()
  RVA=0x069AA3D4  token=0x60003F6  System.Boolean MoveNext()
  RVA=0x069AA528  token=0x60003F8  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass58_0
TYPE:  sealed class
TOKEN: 0x20000BD
SIZE:  0x18
FIELDS:
  public            Beyond.RuntimeFileUtils.StreamingResultindexRet  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003FA  System.Void .ctor()
  RVA=0x069A2BDC  token=0x60003FB  Beyond.Resource.HotUpdate.ResourceIndex <_LoadStreamingIndex>b__0()
END_CLASS

CLASS: <_LoadStreamingIndex>d__58
TYPE:  sealed class
TOKEN: 0x20000BE
SIZE:  0x50
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Boolean                  isInitial  // 0x20
  private           Beyond.Login.LoginHotUpdater.<>c__DisplayClass58_0<>8__1  // 0x28
  public            Beyond.Misc.ObjectRef           resultRef  // 0x30
  public            Beyond.Login.LoginHotUpdater.WorkContextworkContext  // 0x38
  private           System.String                   <indexPath>5__2  // 0x40
  private           Beyond.WaitForAsyncTask<Beyond.Resource.HotUpdate.ResourceIndex><waitForIndex>5__3  // 0x48
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003FC  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003FD  System.Void System.IDisposable.Dispose()
  RVA=0x069A84D8  token=0x60003FE  System.Boolean MoveNext()
  RVA=0x069A8934  token=0x6000400  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: EUnit
TYPE:  sealed struct
TOKEN: 0x20000C1
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Login.LoginProgress.FDownloadSize.EUnitNONE  // const
  public    static  Beyond.Login.LoginProgress.FDownloadSize.EUnitB  // const
  public    static  Beyond.Login.LoginProgress.FDownloadSize.EUnitKB  // const
  public    static  Beyond.Login.LoginProgress.FDownloadSize.EUnitMB  // const
METHODS:
END_CLASS

CLASS: FDownloadSize
TYPE:  sealed struct
TOKEN: 0x20000C0
SIZE:  0x30
FIELDS:
  private   static  System.Double                   MB  // const
  private   static  System.Double                   KB  // const
  private           Beyond.Login.LoginProgress.FDownloadSize.EUnitm_unit  // 0x10
  private           System.Single                   m_displayCurrent  // 0x14
  private           System.Single                   m_displayTotal  // 0x18
  private           System.Single                   m_displaySpeed  // 0x1c
  public            System.String                   displayInfo  // 0x20
  private           System.Single                   m_current  // 0x28
  private           System.Single                   m_total  // 0x2c
METHODS:
  RVA=0x069A08D4  token=0x6000406  System.Void SetProgress(System.Double current, System.Double total)
  RVA=0x069A0790  token=0x6000407  System.Void SetIdle()
  RVA=0x069A0A60  token=0x6000408  System.Void SetSize(System.Double current, System.Double total, System.Double speed)
  RVA=0x069A1094  token=0x6000409  Beyond.Login.LoginProgress.FDownloadSize.EUnit _PickUnit(System.Double number)
  RVA=0x069A0CF0  token=0x600040A  System.Single _DisplayWithUnit(System.Double number, Beyond.Login.LoginProgress.FDownloadSize.EUnit unit)
  RVA=0x069A0DC0  token=0x600040B  System.String _FormatDisplayInfo(System.Single current, System.Single total, Beyond.Login.LoginProgress.FDownloadSize.EUnit unit)
  RVA=0x069A0FA4  token=0x600040C  System.String _FormatSpeedInfo(System.Single speed, Beyond.Login.LoginProgress.FDownloadSize.EUnit unit)
  RVA=0x069A0EF4  token=0x600040D  System.String _FormatProgressInfo(System.Single current, System.Single total)
END_CLASS

CLASS: <_InitCoroutine>d__4
TYPE:  sealed class
TOKEN: 0x20000C3
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginInitNode      <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000414  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000415  System.Void System.IDisposable.Dispose()
  RVA=0x0484D630  token=0x6000416  System.Boolean MoveNext()
  RVA=0x069A7D64  token=0x6000418  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_InitRemoteConfigs>d__5
TYPE:  sealed class
TOKEN: 0x20000C4
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginInitNode      <>4__this  // 0x20
  private           Beyond.Network.NetConfigInterface.InitYiedInstruction<initNetConfig>5__2  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600041A  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600041B  System.Void System.IDisposable.Dispose()
  RVA=0x03DA38E0  token=0x600041C  System.Boolean MoveNext()
  RVA=0x069A7DB0  token=0x600041E  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_YieldAlertError>d__6
TYPE:  sealed class
TOKEN: 0x20000C5
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.String                   errorInfo  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000420  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000421  System.Void System.IDisposable.Dispose()
  RVA=0x069AA60C  token=0x6000422  System.Boolean MoveNext()
  RVA=0x069AA69C  token=0x6000424  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x20000C7
SIZE:  0x20
FIELDS:
  public            System.String                   indexPath  // 0x10
  public            System.String                   prefPath  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000429  System.Void .ctor()
  RVA=0x069A2A4C  token=0x600042A  Beyond.Resource.HotUpdate.ResourceIndex <Work>b__0()
  RVA=0x069A2A9C  token=0x600042B  Beyond.Resource.HotUpdate.LocalResPref <Work>b__1()
END_CLASS

CLASS: <Work>d__3
TYPE:  sealed class
TOKEN: 0x20000C8
SIZE:  0x80
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginLocalResTask  <>4__this  // 0x20
  private           Beyond.Login.LoginLocalResTask.<>c__DisplayClass3_0<>8__1  // 0x28
  private           Beyond.Login.LoginHotUpdater.WorkContext<workContext>5__2  // 0x30
  private           Beyond.Login.LoginHotUpdateProp <viewProp>5__3  // 0x38
  private           Beyond.Login.LoginHotUpdateModel<viewModel>5__4  // 0x40
  private           Beyond.Resource.HotUpdate.ResourceIndex[]<resIndexList>5__5  // 0x48
  private           System.Int32                    <initialOrMain>5__6  // 0x50
  private           System.Boolean                  <isInitial>5__7  // 0x54
  private           Beyond.WaitForAsyncTask<Beyond.Resource.HotUpdate.ResourceIndex><waitForResIndex>5__8  // 0x58
  private           Beyond.Resource.RootCategory    <rootCategory>5__9  // 0x60
  private           Beyond.Login.LoginHotUpdateModel.SimpleProgress<progress>5__10  // 0x68
  private           Beyond.WaitForAsyncTask<System.Object><checkTask>5__11  // 0x70
  private           Beyond.Resource.HotUpdate.LocalResChecker.CheckOutput<checkOutput>5__12  // 0x78
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600042C  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600042D  System.Void System.IDisposable.Dispose()
  RVA=0x069A2CB0  token=0x600042E  System.Boolean MoveNext()
  RVA=0x069A3BEC  token=0x6000430  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000CA
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.Login.PreloadNode.<>c    <>9  // static @ 0x0
  public    static  System.Func<System.Boolean>     <>9__5_0  // static @ 0x8
METHODS:
  RVA=0x04D4D2F0  token=0x600043F  System.Void .cctor()
  RVA=0x041E1670  token=0x6000440  System.Void .ctor()
  RVA=0x036A61A0  token=0x6000441  System.Boolean <Work>b__5_0()
END_CLASS

CLASS: <_LoginPreLoad>d__7
TYPE:  sealed class
TOKEN: 0x20000CB
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.PreloadNode        <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000442  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000443  System.Void System.IDisposable.Dispose()
  RVA=0x0455F3A0  token=0x6000444  System.Boolean MoveNext()
  RVA=0x069A8980  token=0x6000446  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_WarmUp>d__9
TYPE:  sealed class
TOKEN: 0x20000CC
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.PreloadNode        <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000448  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000449  System.Void System.IDisposable.Dispose()
  RVA=0x03C8F5F0  token=0x600044A  System.Boolean MoveNext()
  RVA=0x069AA5C0  token=0x600044C  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_GetServerList>d__10
TYPE:  sealed class
TOKEN: 0x20000CD
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.PreloadNode        <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600044E  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600044F  System.Void System.IDisposable.Dispose()
  RVA=0x040EACC0  token=0x6000450  System.Boolean MoveNext()
  RVA=0x069A7D18  token=0x6000452  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_GetServerListInfoMock>d__12
TYPE:  sealed class
TOKEN: 0x20000CE
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.PreloadNode        <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000454  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000455  System.Void System.IDisposable.Dispose()
  RVA=0x040E9740  token=0x6000456  System.Boolean MoveNext()
  RVA=0x069A76E4  token=0x6000458  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_GetServerListInfo>d__13
TYPE:  sealed class
TOKEN: 0x20000CF
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.PreloadNode        <>4__this  // 0x20
  private           U8.SDK.SDKPromiseEnumerator<System.Collections.Generic.List<U8.SDK.U8ServerInfo>><getServerListPromise>5__2  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600045A  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600045B  System.Void System.IDisposable.Dispose()
  RVA=0x069A7730  token=0x600045C  System.Boolean MoveNext()
  RVA=0x069A7CCC  token=0x600045E  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass14_0
TYPE:  sealed class
TOKEN: 0x20000D0
SIZE:  0x28
FIELDS:
  public            System.Collections.Generic.List<Beyond.Gameplay.ServerZoneManager.ServerZoneData>serverZoneDataList  // 0x10
  public            System.Collections.Generic.List<Hypergryph.SpeedTest.SpeedTestServerInfo>serverList  // 0x18
  public            System.Action<Hypergryph.SpeedTest.SpeedTestResult>callback  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000460  System.Void .ctor()
  RVA=0x069A2698  token=0x6000461  System.Void <_OnGetServerListSuc>b__0(Hypergryph.SpeedTest.SpeedTestResult result)
  RVA=0x069A29E4  token=0x6000462  System.Threading.Tasks.Task<Hypergryph.SpeedTest.TaskResult> <_OnGetServerListSuc>b__1()
END_CLASS

CLASS: <_SDKInitCoroutine>d__4
TYPE:  sealed class
TOKEN: 0x20000D2
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginSDKInitNode   <>4__this  // 0x20
  private           System.String                   <env>5__2  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000469  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600046A  System.Void System.IDisposable.Dispose()
  RVA=0x03C8FD10  token=0x600046B  System.Boolean MoveNext()
  RVA=0x069AA1B4  token=0x600046D  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_WaitForSDKInited>d__5
TYPE:  sealed class
TOKEN: 0x20000D3
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600046F  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000470  System.Void System.IDisposable.Dispose()
  RVA=0x04A426B0  token=0x6000471  System.Boolean MoveNext()
  RVA=0x069AA574  token=0x6000473  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_InitSubSDKs>d__6
TYPE:  sealed class
TOKEN: 0x20000D4
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.String                   env  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000475  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000476  System.Void System.IDisposable.Dispose()
  RVA=0x0332A190  token=0x6000477  System.Boolean MoveNext()
  RVA=0x069A7DFC  token=0x6000479  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_LoginCoroutine>d__7
TYPE:  sealed class
TOKEN: 0x20000D6
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.U8LoginNode        <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000492  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000493  System.Void System.IDisposable.Dispose()
  RVA=0x015043A0  token=0x6000494  System.Boolean MoveNext()
  RVA=0x01505930  token=0x6000496  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_FinishLogin>d__8
TYPE:  sealed class
TOKEN: 0x20000D7
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  private           System.String                   <env>5__2  // 0x20
  private           U8.SDK.SDKPromiseEnumerator<U8.SDK.U8AuthV2Result><authPromise>5__3  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000498  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000499  System.Void System.IDisposable.Dispose()
  RVA=0x01502380  token=0x600049A  System.Boolean MoveNext()
  RVA=0x01504350  token=0x600049C  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: FAgeTipsPolicy
TYPE:  sealed struct
TOKEN: 0x20000DC
SIZE:  0x13
FIELDS:
  public            System.Boolean                  canOpenDetail  // 0x10
  public            System.Boolean                  displayCN  // 0x11
  public            System.Boolean                  displayKR  // 0x12
PROPERTIES:
  display  get=0x03408B80
METHODS:
END_CLASS

CLASS: Node
TYPE:  abstract class
TOKEN: 0x20000E1
SIZE:  0x20
FIELDS:
  private           Beyond.LoginContext             <context>k__BackingField  // 0x10
  private           Beyond.Login.LoginWorkFlow      <workflow>k__BackingField  // 0x18
PROPERTIES:
  context  get=0x02B2ECC0  set=0x053908C0
  workflow  get=0x0385B100  set=0x05392C40
  type  get=-1  // abstract
METHODS:
  RVA=0x03405FE0  token=0x60004E3  System.Void Init(Beyond.LoginContext context, Beyond.Login.LoginWorkFlow workflow)
  RVA=0x034060C0  token=0x60004E4  System.Void Dispose()
  RVA=-1  // abstract  token=0x60004E6  UnityEngine.CustomYieldInstruction Work()
  RVA=0x069A2380  token=0x60004E7  System.Boolean CanCancel()
  RVA=0x069A23CC  token=0x60004E8  UnityEngine.CustomYieldInstruction Cancel()
  RVA=0x03406040  token=0x60004E9  System.Void OnInit()
  RVA=0x046DD140  token=0x60004EA  System.Void OnDispose()
  RVA=0x0470B370  token=0x60004EB  System.Boolean OnEvent(Beyond.ELoginEvent evt, System.Object param)
  RVA=0x041E1670  token=0x60004EC  System.Void .ctor()
END_CLASS

CLASS: ENode
TYPE:  sealed struct
TOKEN: 0x20000E2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Login.LoginWorkFlow.ENodeNone  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodeCheckForUpdate  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodeSDKInit  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodeGameUpdate  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodeInit  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodeU8Login  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodeHotUpdate  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodeResCheck  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodePreload  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodePreloadPostDelay  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodeTrans2GsLogin  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodeGsLogin  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodeCreateRole  // const
  public    static  Beyond.Login.LoginWorkFlow.ENodeFinish  // const
METHODS:
END_CLASS

CLASS: IWorkerUpdateOnce
TYPE:  interface
TOKEN: 0x20000E3
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60004ED  System.Void Worker_UpdateOnce()
END_CLASS

CLASS: IEventReceiver
TYPE:  interface
TOKEN: 0x20000E4
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60004EE  System.Void OnEvent(Beyond.ELoginEvent evt, System.Object param)
END_CLASS

CLASS: <Work>d__20
TYPE:  sealed class
TOKEN: 0x20000E6
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginWorkFlow.Worker<>4__this  // 0x20
  public            UnityEngine.CustomYieldInstructionwork  // 0x28
  private           UnityEngine.CustomYieldInstruction<cancelTask>5__2  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60004FD  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60004FE  System.Void System.IDisposable.Dispose()
  RVA=0x03D4ECE0  token=0x60004FF  System.Boolean MoveNext()
  RVA=0x069A2C64  token=0x6000501  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Worker
TYPE:  class
TOKEN: 0x20000E5
SIZE:  0x38
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Collections.Generic.HashSet<Beyond.Login.LoginWorkFlow.IWorkerUpdateOnce>m_updateOnce  // 0x10
  private           System.Collections.Generic.List<Beyond.Login.LoginWorkFlow.IWorkerUpdateOnce>m_buffer  // 0x18
  private           UnityEngine.CustomYieldInstructionm_curWork  // 0x20
  private           System.Boolean                  <isCancelled>k__BackingField  // 0x28
  private           System.Boolean                  <isDisposed>k__BackingField  // 0x29
  private           Beyond.Login.LoginWorkFlow.Node <curNode>k__BackingField  // 0x30
PROPERTIES:
  isCancelled  get=0x011F33C0  set=0x011F33D0
  isDisposed  get=0x04D867B0  set=0x04D867C0
  curNode  get=0x011F36E0  set=0x05396200
METHODS:
  RVA=0x069AAA0C  token=0x60004F5  System.Boolean Cancel()
  RVA=0x04D26A80  token=0x60004F6  System.Void Dispose()
  RVA=0x03405DE0  token=0x60004F7  System.Void SetCurNode(Beyond.Login.LoginWorkFlow.Node node)
  RVA=0x0450CB50  token=0x60004F8  System.Boolean UpdateOnce(Beyond.Login.LoginWorkFlow.IWorkerUpdateOnce inst)
  RVA=0x03D4ECA0  token=0x60004F9  System.Void TickInEmptyFrame()
  RVA=0x03406100  token=0x60004FA  System.Collections.IEnumerator Work(UnityEngine.CustomYieldInstruction work)
  RVA=0x03D4EE00  token=0x60004FB  System.Void _InternalTick()
  RVA=0x04CC9AF0  token=0x60004FC  System.Void .ctor()
END_CLASS

CLASS: FWork
TYPE:  sealed struct
TOKEN: 0x20000E7
SIZE:  0x1C
FIELDS:
  public    static readonly Beyond.Login.LoginWorkFlow.FWorkIDLE  // static @ 0x0
  private           System.Boolean                  <didFallback>k__BackingField  // 0x10
  private           System.Int32                    <nodeIndex>k__BackingField  // 0x14
  private           System.Boolean                  <isWorking>k__BackingField  // 0x18
PROPERTIES:
  didFallback  get=0x04D883E0  set=0x04D87180
  nodeIndex  get=0x04D88B20  set=0x04D88B50
  isWorking  get=0x04D8CCC0  set=0x04D8CCD0
  curNode  get=0x03406B00
METHODS:
  RVA=0x069A1118  token=0x6000509  System.Void MarkFallback(System.Int32 newIndex)
  RVA=0x03406C30  token=0x600050A  System.Boolean ConsumeFallback()
  RVA=0x03406BD0  token=0x600050B  System.Void MoveNextIndex()
  RVA=0x04D18180  token=0x600050C  Beyond.Login.LoginWorkFlow.FWork Start()
  RVA=0x03406C90  token=0x600050E  System.Boolean WorkEnd()
  RVA=0x041E1670  token=0x600050F  System.Void .cctor()
END_CLASS

CLASS: <DoWorkFlow>d__25
TYPE:  sealed class
TOKEN: 0x20000E8
SIZE:  0x50
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginWorkFlow      <>4__this  // 0x20
  private           Beyond.Login.LoginWorkFlow.Worker<worker>5__2  // 0x28
  private           Beyond.Login.LoginViewProperty  <viewProp>5__3  // 0x30
  private           Beyond.Login.LoginWorkFlow.ENode<cachedCurrent>5__4  // 0x38
  private           System.Boolean                  <isEmptyFrame>5__5  // 0x3c
  private           System.Boolean                  <isAbort>5__6  // 0x3d
  private           Beyond.Login.LoginWorkFlow.Node[]<>7__wrap6  // 0x40
  private           System.Int32                    <>7__wrap7  // 0x48
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000510  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000511  System.Void System.IDisposable.Dispose()
  RVA=0x034064C0  token=0x6000512  System.Boolean MoveNext()
  RVA=0x069A25F4  token=0x6000514  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_FetchAgeTipsCoroutine>d__45
TYPE:  sealed class
TOKEN: 0x20000E9
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginAgeTipsProp   prop  // 0x20
  public            Beyond.Login.LoginWorkFlow      <>4__this  // 0x28
  private           Beyond.Login.LoginAgeTipsModel  <model>5__2  // 0x30
  private           Beyond.LoginController.MaskCtrl <maskCtrl>5__3  // 0x38
  private           Beyond.Misc.ObjectRef           <httpRetRef>5__4  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000516  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000517  System.Void System.IDisposable.Dispose()
  RVA=0x069A6C84  token=0x6000518  System.Boolean MoveNext()
  RVA=0x069A6F64  token=0x600051A  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <_AdjustCurrentGameState>d__47
TYPE:  sealed class
TOKEN: 0x20000EA
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginWorkFlow.ENodecurNode  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600051C  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600051D  System.Void System.IDisposable.Dispose()
  RVA=0x036A6080  token=0x600051E  System.Boolean MoveNext()
  RVA=0x069A3C38  token=0x6000520  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Options
TYPE:  sealed struct
TOKEN: 0x20000F2
SIZE:  0x18
FIELDS:
  public            System.Int64                    minBytesToNotifyCarrier  // 0x10
METHODS:
END_CLASS

CLASS: <>c__DisplayClass6_0
TYPE:  sealed class
TOKEN: 0x20000F3
SIZE:  0x30
FIELDS:
  public            System.Int64                    downloadSize  // 0x10
  public            Beyond.Login.NetUsagePolicy     <>4__this  // 0x18
  public            System.Action                   onAllowed  // 0x20
  public            System.Action                   onRejected  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000542  System.Void .ctor()
  RVA=0x069B4C50  token=0x6000543  System.Void <CheckIfAllowDownload>b__0()
  RVA=0x069B4CD0  token=0x6000544  System.Void <CheckIfAllowDownload>b__1()
END_CLASS

CLASS: EState
TYPE:  sealed struct
TOKEN: 0x20000F5
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Login.LoginSceneAnimCtrl.EStateNONE  // const
  public    static  Beyond.Login.LoginSceneAnimCtrl.EStateA1  // const
  public    static  Beyond.Login.LoginSceneAnimCtrl.EStateA2  // const
METHODS:
END_CLASS

CLASS: FParams
TYPE:  sealed struct
TOKEN: 0x20000FB
SIZE:  0x20
FIELDS:
  public            System.String                   text  // 0x10
  public            System.Action                   onConfirm  // 0x18
METHODS:
END_CLASS

CLASS: Style
TYPE:  sealed struct
TOKEN: 0x2000100
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Login.LoginJudgeDialog.StyleCenter  // const
  public    static  Beyond.Login.LoginJudgeDialog.StyleLeft  // const
METHODS:
END_CLASS

CLASS: FParams
TYPE:  sealed struct
TOKEN: 0x2000101
SIZE:  0x40
FIELDS:
  public            System.String                   text  // 0x10
  public            System.Action                   onPositive  // 0x18
  public            System.Action                   onNegative  // 0x20
  public            System.String                   btnPositive  // 0x28
  public            System.String                   btnNegative  // 0x30
  public            Beyond.Login.LoginJudgeDialog.Stylestyle  // 0x38
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000103
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.Login.LoginMenuPanel.<>c <>9  // static @ 0x0
  public    static  System.Action                   <>9__43_0  // static @ 0x8
METHODS:
  RVA=0x069B4D40  token=0x60005BE  System.Void .cctor()
  RVA=0x041E1670  token=0x60005BF  System.Void .ctor()
  RVA=0x069B4830  token=0x60005C0  System.Void <_OnQuitGameClicked>b__43_0()
END_CLASS

CLASS: FOptions
TYPE:  sealed struct
TOKEN: 0x2000105
SIZE:  0x20
FIELDS:
  public            System.String                   fixedUID  // 0x10
  public            Beyond.LoginContext             context  // 0x18
METHODS:
  RVA=0x03F10080  token=0x60005D6  System.Boolean IsUIDFixed()
END_CLASS

CLASS: <HideAndDestroy>d__22
TYPE:  sealed class
TOKEN: 0x2000106
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginMockUIDPanel  <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60005D7  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60005D8  System.Void System.IDisposable.Dispose()
  RVA=0x049D1390  token=0x60005D9  System.Boolean MoveNext()
  RVA=0x069B47E4  token=0x60005DB  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: ChildPanel
TYPE:  abstract class
TOKEN: 0x2000109
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.Login.ILoginContextReceiver
FIELDS:
  private           Beyond.UI.UISwitchTween         <switchTween>k__BackingField  // 0x18
  private           Beyond.LoginContext             <context>k__BackingField  // 0x20
PROPERTIES:
  switchTween  get=0x0385B100  set=0x05392C40
  context  get=0x04D862C0  set=0x05390F40
METHODS:
  RVA=0x041E5A50  token=0x60005EE  System.Void OnLoginContext(Beyond.LoginContext context)
  RVA=0x042B60D0  token=0x60005EF  System.Void OnInit()
  RVA=0x048AB6E0  token=0x60005F0  System.Void OnDestroy()
  RVA=0x069AAF5C  token=0x60005F1  System.Void SetPanelVisibleByAnimSwitchTween(Beyond.UI.UIAnimationWrapper anim, System.Boolean isVisible, System.Boolean forceReset)
  RVA=0x03406EF0  token=0x60005F2  System.Void SetPanelVisibleByFadeSwitchTween(UnityEngine.CanvasGroup canvasGroup, System.Boolean isVisible, System.Boolean forceReset)
  RVA=0x05393520  token=0x60005F3  System.Void .ctor()
END_CLASS

CLASS: DelayInfo
TYPE:  sealed struct
TOKEN: 0x200010B
SIZE:  0x30
FIELDS:
  public            UnityEngine.Color               color  // 0x10
  public            System.Int32                    colorCount  // 0x20
  public            System.Int32                    delayMax  // 0x24
  public            System.String                   text  // 0x28
METHODS:
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed class
TOKEN: 0x2000110
SIZE:  0x30
FIELDS:
  public            Beyond.Login.LoginServerSelectPanel<>4__this  // 0x10
  public            System.Collections.Generic.List<Beyond.Gameplay.ServerZoneManager.ServerZoneData>serverZoneDataList  // 0x18
  public            System.Collections.Generic.List<Hypergryph.SpeedTest.SpeedTestServerInfo>serverList  // 0x20
  public            System.Action<Hypergryph.SpeedTest.SpeedTestResult>callback  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600060F  System.Void .ctor()
  RVA=0x069B4874  token=0x6000610  System.Void <OnEnable>b__0(Hypergryph.SpeedTest.SpeedTestResult result)
  RVA=0x069B4BE8  token=0x6000611  System.Threading.Tasks.Task<Hypergryph.SpeedTest.TaskResult> <OnEnable>b__1()
END_CLASS

CLASS: <_WaitingForShowListAwake>d__11
TYPE:  sealed class
TOKEN: 0x2000111
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            Beyond.Login.LoginServerSelectPanel<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000612  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000613  System.Void System.IDisposable.Dispose()
  RVA=0x069B4DA4  token=0x6000614  System.Boolean MoveNext()
  RVA=0x069B4E94  token=0x6000616  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: FParams
TYPE:  sealed struct
TOKEN: 0x2000114
SIZE:  0x18
FIELDS:
  public            System.Action                   onConfirm  // 0x10
METHODS:
END_CLASS

CLASS: VocResInfo
TYPE:  class
TOKEN: 0x2000115
SIZE:  0x20
FIELDS:
  public            System.String                   nameTextId  // 0x10
  public            Beyond.VFS.EVFSBlockType        type  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000630  System.Void .ctor()
END_CLASS

CLASS: __StaticArrayInitTypeSize=52
TYPE:  sealed struct
TOKEN: 0x200011A
SIZE:  0x44
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x2000119
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=52DF6EA5B59F0E6C03F390AD5FEA0BBC199EC3524C0A682913148FD071AF1898EC  // static @ 0x0
METHODS:
END_CLASS

CLASS: Beyond.GameApp
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.ILuaCallCSharp
FIELDS:
  private           Beyond.GameStateMachine         m_stateMachine  // 0x18
  private   static  Beyond.GameApp                  <instance>k__BackingField  // static @ 0x0
  private           System.Boolean                  m_hasFocus  // 0x20
  private           System.Boolean                  m_needCallLuaFocus  // 0x21
  private           System.Boolean                  m_pauseStatus  // 0x22
  private           System.Boolean                  m_needCallLuaPause  // 0x23
  private   static  System.UInt32                   FILE_ATTRIBUTE_DIRECTORY  // const
  private   static  System.UInt32                   FILE_ATTRIBUTE_READONLY  // const
  private   static  System.UInt32                   FILE_ATTRIBUTE_NORMAL  // const
  private   static  System.UInt32                   FILE_ATTRIBUTE_REPARSE_POINT  // const
  private   static readonly System.IntPtr                   INVALID_HANDLE_VALUE  // static @ 0x8
  private   static  System.UInt32                   GENERIC_READ  // const
  private   static  System.UInt32                   GENERIC_WRITE  // const
  private   static  System.UInt32                   FILE_SHARE_READ  // const
  private   static  System.UInt32                   FILE_SHARE_WRITE  // const
  private   static  System.UInt32                   FILE_SHARE_DELETE  // const
  private   static  System.UInt32                   OPEN_EXISTING  // const
  private   static  System.UInt32                   FILE_FLAG_SEQUENTIAL_SCAN  // const
  private   static  System.Int32                    ERROR_FILE_NOT_FOUND  // const
  private   static  System.Int32                    ERROR_PATH_NOT_FOUND  // const
  private   static  System.Int32                    ERROR_NO_MORE_FILES  // const
  private   static  System.Byte[]                   s_md5Buffer  // static @ 0xffffffff
  private   static  System.Int32                    s_scanRunning  // static @ 0x10
  private   static  System.String                   s_pendingScanOutput  // static @ 0x18
PROPERTIES:
  instance  get=0x06991E18  set=0x06991E68
  currentState  get=0x036A6260
  IsScanRunning  get=0x06991D98
METHODS:
  RVA=0x03C11BB0  token=0x6000008  System.Void SwitchState(Beyond.GameState state)
  RVA=0x0380F1F0  token=0x6000009  System.Void Awake()
  RVA=0x041A5BF0  token=0x600000A  System.Boolean _NeedDeepProfilerMode()
  RVA=0x0698FB68  token=0x600000B  System.Collections.IEnumerator _DelayInit()
  RVA=0x0488B500  token=0x600000C  System.Void _RegisterLuaAfterGCEvent()
  RVA=0x0464E170  token=0x600000D  System.Void _InitNotificationCenter()
  RVA=0x032E1800  token=0x600000E  System.Void Update()
  RVA=0x02F5C730  token=0x600000F  System.Void LateUpdate()
  RVA=0x0698D850  token=0x6000010  System.Void OnApplicationQuit()
  RVA=0x03A73FA0  token=0x6000011  System.Void OnApplicationFocus(System.Boolean hasFocus)
  RVA=0x03820560  token=0x6000012  System.Void _OnApplicationFocusForLua()
  RVA=0x03F26470  token=0x6000013  System.Void OnApplicationPause(System.Boolean pauseStatus)
  RVA=0x03F26780  token=0x6000014  System.Void _OnApplicationPauseForLua()
  RVA=0x0698DA78  token=0x6000015  System.Void OnDestroy()
  RVA=0x035C2290  token=0x6000016  System.Void _PreInitGameSetting()
  RVA=0x0464DC20  token=0x6000017  System.Void _Init()
  RVA=0x06990FE4  token=0x6000018  System.Void _UnInit()
  RVA=0x0464E1A0  token=0x6000019  System.Void _InitGameStateMachine()
  RVA=0x06990F60  token=0x600001A  System.Void _UnInitGameStateMachine()
  RVA=0x0698DD34  token=0x600001B  System.Void QuitGame(Beyond.GameAppExitCode exitCode)
  RVA=0x0698DDA8  token=0x600001C  System.Void QuitGame(System.Int32 exitCode)
  RVA=0x0698DE64  token=0x600001D  System.Void RefreshScreenResolution()
  RVA=0x06990210  token=0x600001E  System.Void _OnSystemDisplaySizeChanged()
  RVA=0x0699029C  token=0x600001F  System.Void _OnSystemScreenSizeChanged(Beyond.EventData<System.Boolean,System.Boolean>& data)
  RVA=0x0464DE50  token=0x6000020  System.Collections.IEnumerator _InitSecuritySDK()
  RVA=0x0340DAF0  token=0x6000021  System.Void _OnApplicationPauseForSecurity(System.Boolean pauseStatus)
  RVA=0x04D32D40  token=0x6000022  System.Collections.IEnumerator _FetchRemoteConfigUtilSuc()
  RVA=0x0698FC74  token=0x6000023  System.IntPtr _FindFirstFileW(System.String lpFileName, Beyond.GameApp.WIN32_FIND_DATA& lpFindFileData)
  RVA=0x0698FD8C  token=0x6000024  System.Boolean _FindNextFileW(System.IntPtr hFindFile, Beyond.GameApp.WIN32_FIND_DATA& lpFindFileData)
  RVA=0x0698FBF4  token=0x6000025  System.Boolean _FindClose(System.IntPtr hFindFile)
  RVA=0x0698FA90  token=0x6000026  System.IntPtr _CreateFileW(System.String lpFileName, System.UInt32 dwDesiredAccess, System.UInt32 dwShareMode, System.IntPtr lpSecurityAttributes, System.UInt32 dwCreationDisposition, System.UInt32 dwFlagsAndAttributes, System.IntPtr hTemplateFile)
  RVA=0x06990760  token=0x6000027  System.Boolean _ReadFile(System.IntPtr hFile, System.Byte[] lpBuffer, System.UInt32 nNumberOfBytesToRead, System.UInt32& lpNumberOfBytesRead, System.IntPtr lpOverlapped)
  RVA=0x0698E99C  token=0x6000028  System.Boolean _CloseHandle(System.IntPtr hObject)
  RVA=0x0698DC9C  token=0x600002A  System.Boolean PollScanResult(System.String& gzOutputPath)
  RVA=0x0698DF64  token=0x600002B  System.Void ScanLocalFiles(System.String rootDir, System.String outputPath, System.Int32 threadCount)
  RVA=0x0698D4A0  token=0x600002C  System.Void LaunchScanCoordinator(System.String rootDir, System.String outputPath, System.Int32 threadCount, System.String systemInfo)
  RVA=0x06990C04  token=0x600002D  System.Collections.Generic.List<Beyond.GameApp.FileEntry> _ScanParallel(System.String root, System.Int32 threadCount)
  RVA=0x06991074  token=0x600002E  System.Void _WorkerLoop(System.Object state)
  RVA=0x06990824  token=0x600002F  System.Void _ScanDirectory(Beyond.GameApp.ScanContext ctx, System.String dir)
  RVA=0x06990060  token=0x6000030  System.String _MakeLongPath(System.String path)
  RVA=0x0698E160  token=0x6000031  Beyond.GameApp.FileEntry _BuildEntry(Beyond.GameApp.PendingFile pf)
  RVA=0x0698E8C0  token=0x6000032  System.Boolean _CanAccess(System.String path, System.UInt32 desiredAccess)
  RVA=0x0698E3F0  token=0x6000033  System.String _CalculateMd5(System.String path)
  RVA=0x0698E9EC  token=0x6000034  System.String _CollectSystemInfo()
  RVA=0x06991268  token=0x6000035  System.Void _WriteTreeReport(System.Collections.Generic.List<Beyond.GameApp.FileEntry> entries, System.String root, System.DateTime scanStart, System.String outputPath, System.String systemInfo)
  RVA=0x06990358  token=0x6000036  System.Void _PrintTreeNode(System.Text.StringBuilder sb, Beyond.GameApp.TreeNode node, System.String prefix)
  RVA=0x06990EB4  token=0x6000037  System.Int32 _TreeNodeCompare(Beyond.GameApp.TreeNode a, Beyond.GameApp.TreeNode b)
  RVA=0x0698FE9C  token=0x6000038  System.String _FormatSize(System.Int64 bytes)
  RVA=0x04D84C40  token=0x6000039  System.Void .ctor()
  RVA=0x04D7A710  token=0x600003A  System.Void .cctor()
END_CLASS

CLASS: Beyond.IMemoryPackProxy
TYPE:  interface
TOKEN: 0x200000E
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000052  System.Void Init()
END_CLASS

CLASS: Beyond.MemoryPackProxyModule
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x10
FIELDS:
  private   static  Beyond.IMemoryPackProxy         s_memoryPackProxy  // static @ 0x0
METHODS:
  RVA=0x06993C80  token=0x6000053  System.Void RegisterProxy(Beyond.IMemoryPackProxy memoryPackProxy)
  RVA=0x06993C48  token=0x6000054  Beyond.IMemoryPackProxy GetMemoryPackProxy()
  RVA=0x041E1670  token=0x6000055  System.Void .ctor()
END_CLASS

CLASS: Beyond.GameInitState
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x18
EXTENDS: Beyond.GameStateBase
FIELDS:
PROPERTIES:
  stateId  get=0x04D261C0
METHODS:
  RVA=0x053908C0  token=0x6000057  System.Void .ctor(Beyond.GameStateMachine stateMachine)
  RVA=0x0380EA50  token=0x6000058  System.Void OnEnter(Beyond.GameState fromStateId)
  RVA=0x041E4230  token=0x6000059  System.Void OnLeave(Beyond.GameState toStateId)
  RVA=0x0380EB40  token=0x600005A  System.Collections.IEnumerator _DoInit()
  RVA=0x03DA3770  token=0x600005B  System.Void _InitInitialResourceManager(Beyond.VFS.VirtualFileSystem.BlockValidator& blockValidator)
  RVA=0x0332A050  token=0x600005C  System.Boolean _CheckIfLocalInitialAssetsValid(Beyond.Resource.HotUpdate.LocalResPref& outputLocalResPref, Beyond.VFS.VirtualFileSystem.BlockValidator& blockValidator)
  RVA=0x03BA9CF0  token=0x600005D  System.Void _InitAPPDefaultCulture()
  RVA=0x03C91F20  token=0x600005E  System.Void _InitGameSetting()
  RVA=0x06991EC4  token=0x600005F  System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0)
  RVA=0x06991ECC  token=0x6000060  System.Void <>iFixBaseProxy_OnLeave(Beyond.GameState P0)
END_CLASS

CLASS: Beyond.GameLoginState
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x18
EXTENDS: Beyond.GameStateBase
FIELDS:
PROPERTIES:
  stateId  get=0x04D51FE0
METHODS:
  RVA=0x053908C0  token=0x6000068  System.Void .ctor(Beyond.GameStateMachine stateMachine)
  RVA=0x0380EBB0  token=0x6000069  System.Void OnEnter(Beyond.GameState fromStateId)
  RVA=0x041E4770  token=0x600006A  System.Void OnLeave(Beyond.GameState toStateId)
  RVA=0x04A20820  token=0x600006B  System.Void _TryStartLoginWorkflow()
  RVA=0x06991EC4  token=0x600006C  System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0)
  RVA=0x06991ECC  token=0x600006D  System.Void <>iFixBaseProxy_OnLeave(Beyond.GameState P0)
END_CLASS

CLASS: Beyond.GameMainState
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x20
EXTENDS: Beyond.GameStateBase
FIELDS:
  private           System.Boolean                  m_isFirstEnter  // 0x18
PROPERTIES:
  stateId  get=0x04D51FB0
METHODS:
  RVA=0x05397B28  token=0x600006F  System.Void .ctor(Beyond.GameStateMachine stateMachine)
  RVA=0x0380E4E0  token=0x6000070  System.Void OnEnter(Beyond.GameState fromStateId)
  RVA=0x041E4830  token=0x6000071  System.Void OnLeave(Beyond.GameState toStateId)
  RVA=0x04816600  token=0x6000072  System.Void ReleaseMainGame(Beyond.GameState toStateId)
  RVA=0x06991ED4  token=0x6000073  System.Void _ClearUtils()
  RVA=0x06991EC4  token=0x6000074  System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0)
  RVA=0x06991ECC  token=0x6000075  System.Void <>iFixBaseProxy_OnLeave(Beyond.GameState P0)
END_CLASS

CLASS: Beyond.GamePreloadState
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x20
EXTENDS: Beyond.GameStateBase
FIELDS:
  private           Beyond.Cfg.Tables.AsyncLoadTask m_asyncTask  // 0x18
PROPERTIES:
  stateId  get=0x04D51F50
METHODS:
  RVA=0x053908C0  token=0x6000077  System.Void .ctor(Beyond.GameStateMachine stateMachine)
  RVA=0x0380E620  token=0x6000078  System.Void OnEnter(Beyond.GameState fromStateId)
  RVA=0x041E4880  token=0x6000079  System.Void OnLeave(Beyond.GameState toStateId)
  RVA=0x0380E6C0  token=0x600007A  System.Collections.IEnumerator _DoPreload()
  RVA=0x04151FC0  token=0x600007B  System.Void _InitGameSettingStateInPreload()
  RVA=0x06991EC4  token=0x600007C  System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0)
  RVA=0x06991ECC  token=0x600007D  System.Void <>iFixBaseProxy_OnLeave(Beyond.GameState P0)
END_CLASS

CLASS: Beyond.GameQuitState
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x18
EXTENDS: Beyond.GameStateBase
FIELDS:
PROPERTIES:
  stateId  get=0x04D51F20
METHODS:
  RVA=0x053908C0  token=0x6000085  System.Void .ctor(Beyond.GameStateMachine stateMachine)
  RVA=0x06992410  token=0x6000086  System.Void OnEnter(Beyond.GameState fromStateId)
  RVA=0x06992478  token=0x6000087  System.Void OnLeave(Beyond.GameState toStateId)
  RVA=0x06991FBC  token=0x6000088  System.Void DoQuit()
  RVA=0x06991EC4  token=0x6000089  System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0)
  RVA=0x06991ECC  token=0x600008A  System.Void <>iFixBaseProxy_OnLeave(Beyond.GameState P0)
END_CLASS

CLASS: Beyond.GameSoftRestartState
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x18
EXTENDS: Beyond.GameStateBase
FIELDS:
  private   static  System.String                   CLEAR_SCENE_PATH  // const
PROPERTIES:
  stateId  get=0x04D51EF0
METHODS:
  RVA=0x053908C0  token=0x600008C  System.Void .ctor(Beyond.GameStateMachine stateMachine)
  RVA=0x069924DC  token=0x600008D  System.Void OnEnter(Beyond.GameState fromStateId)
  RVA=0x06992594  token=0x600008E  System.Collections.IEnumerator _SoftRestart()
  RVA=0x06991EC4  token=0x600008F  System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0)
END_CLASS

CLASS: Beyond.GameStartState
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x18
EXTENDS: Beyond.GameStateBase
FIELDS:
PROPERTIES:
  stateId  get=0x04D51EC0
METHODS:
  RVA=0x053908C0  token=0x6000097  System.Void .ctor(Beyond.GameStateMachine stateMachine)
  RVA=0x0380E730  token=0x6000098  System.Void OnEnter(Beyond.GameState fromStateId)
  RVA=0x041E48F0  token=0x6000099  System.Void OnLeave(Beyond.GameState toStateId)
  RVA=0x04B52310  token=0x600009A  System.Void _OnEnterMainGame()
  RVA=0x03C123C0  token=0x600009B  System.Void <_OnEnterMainGame>b__5_0()
  RVA=0x06991EC4  token=0x600009C  System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0)
  RVA=0x06991ECC  token=0x600009D  System.Void <>iFixBaseProxy_OnLeave(Beyond.GameState P0)
END_CLASS

CLASS: Beyond.GameStateBase
TYPE:  abstract class
TOKEN: 0x200001A
SIZE:  0x18
IMPLEMENTS: Beyond.Gameplay.ISimpleStateNode`1 Beyond.ILuaCallCSharp
FIELDS:
  protected readonly Beyond.GameStateMachine         m_stateMachine  // 0x10
PROPERTIES:
  stateId  get=-1  // abstract
METHODS:
  RVA=0x053908C0  token=0x600009E  System.Void .ctor(Beyond.GameStateMachine stateMachine)
  RVA=0x0380E880  token=0x60000A0  System.Void OnEnter(Beyond.GameState fromStateId)
  RVA=0x041E49F0  token=0x60000A1  System.Void OnLeave(Beyond.GameState toStateId)
  RVA=0x06992624  token=0x60000A2  System.Void OnTick(System.Single deltaTime)
END_CLASS

CLASS: Beyond.GameStateMachine
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x38
EXTENDS: Beyond.Gameplay.SimpleEnumStateMachine`1
IMPLEMENTS: Beyond.IGameStateMachine Beyond.ILuaCallCSharp
FIELDS:
  private   readonly Beyond.GameApp                  <app>k__BackingField  // 0x30
PROPERTIES:
  app  get=0x011F36E0
  curState  get=0x0332C370
METHODS:
  RVA=0x0464E240  token=0x60000A5  System.Void .ctor(Beyond.GameApp gameApp)
  RVA=0x0464E2A0  token=0x60000A6  System.Void _RegisterStates()
  RVA=0x03C119D0  token=0x60000A7  System.Boolean CheckCanReturnToLoginState()
  RVA=0x03C11960  token=0x60000A8  System.Collections.IEnumerator ReturnToLoginState()
END_CLASS

CLASS: Beyond.GlobalInitializer
TYPE:  static class
TOKEN: 0x200001D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04D83BE0  token=0x60000AF  System.Void InitGlobalConfig()
END_CLASS

CLASS: Beyond.IHotFixCommon
TYPE:  interface
TOKEN: 0x200001E
FIELDS:
METHODS:
END_CLASS

CLASS: Beyond.IHotFixPatchManagerProxy
TYPE:  interface
TOKEN: 0x200001F
IMPLEMENTS: Beyond.IHotFixCommon
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60000B0  System.Void InitManager()
  RVA=-1  // abstract  token=0x60000B1  System.Void ReleaseManager()
  RVA=-1  // abstract  token=0x60000B2  System.Void OnReturnToLogin()
  RVA=-1  // abstract  token=0x60000B3  System.Void RegisterHotfixSDKEvent()
  RVA=-1  // abstract  token=0x60000B4  System.Void UnregisterHotfixSDKEvent()
  RVA=-1  // abstract  token=0x60000B5  System.Boolean TryProcessHotfixSDKExtraInfo(System.Int32 code, System.Collections.Generic.Dictionary<System.String,System.Object> msg)
  RVA=-1  // abstract  token=0x60000B6  System.Void PatchInitAssemble()
  RVA=-1  // abstract  token=0x60000B7  System.Void PatchRemoteInitAssemble()
  RVA=-1  // abstract  token=0x60000B8  System.Void PatchRemoteInjectedLua()
  RVA=-1  // abstract  token=0x60000B9  System.Void PatchMainAssemble()
  RVA=-1  // abstract  token=0x60000BA  System.Void PatchRemoteMainAssemble()
END_CLASS

CLASS: Beyond.HotFixPatchModules
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.Type,Beyond.IHotFixCommon>s_hotFixModules  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x60000BB  System.Void Register(T module)
  RVA=-1  // generic def  token=0x60000BC  System.Void UnRegister()
  RVA=-1  // generic def  token=0x60000BD  T GetModule()
  RVA=-1  // generic def  token=0x60000BE  System.Boolean TryGetModule(T& module)
  RVA=0x041E1670  token=0x60000BF  System.Void .ctor()
  RVA=0x04D17E20  token=0x60000C0  System.Void .cctor()
END_CLASS

CLASS: Beyond.InitialPathDef
TYPE:  static class
TOKEN: 0x2000021
SIZE:  0x10
FIELDS:
  public    static  System.String                   LOGIN_SCENE_PATH  // const
  public    static  System.String                   LOGIN_SK_ACTOR_M  // const
  public    static  System.String                   LOGIN_SK_ACTOR_F  // const
  public    static  System.String                   LOGIN_SK_ACTOR_NO_GENDER  // const
  public    static  System.String                   LOGIN_MOCK_UID_PANEL  // const
  public    static  System.String                   LOGIN_CONTROLLER_PATH  // const
  public    static  System.String                   LOGIN_ROOT_PANEL_PATH  // const
  public    static  System.String                   LOGIN_ALERT_DIALOG_PATH  // const
  public    static  System.String                   LOGIN_JUDGE_DIALOG_PATH  // const
  public    static  System.String                   LOGIN_VOC_RES_SELECT_DIALOG_PATH  // const
METHODS:
END_CLASS

CLASS: Beyond.LoginController
TYPE:  class
TOKEN: 0x2000022
SIZE:  0xC0
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.Login.ILoginBinder`1
FIELDS:
  private           UnityEngine.Transform           _uiFunc  // 0x18
  private           UnityEngine.Transform           _uiDialog  // 0x20
  private           Beyond.UI.UIButton              _versionBtn  // 0x28
  private           Beyond.Login.LoginDecorateUI    _uiDecorate  // 0x30
  private           Beyond.LoginController.MaskCfg  _maskBlur  // 0x38
  private           Beyond.LoginController.MaskCfg  _maskBlack  // 0x40
  private           Beyond.LoginController.MaskCfg  _maskWhite  // 0x48
  private           System.Single                   _enterGamePanelPredelay  // 0x50
  private           UnityEngine.Canvas              _uiRootCanvas  // 0x58
  private           Beyond.UI.UICanvasScaleHelper   _uiRootCanvasScaleHelper  // 0x60
  private           UnityEngine.Canvas              _uiDecoCanvas  // 0x68
  private           Beyond.UI.UICanvasScaleHelper   _uiDecoCanvasScaleHelper  // 0x70
  private           System.Boolean                  m_isInited  // 0x78
  private           Beyond.LoginController.FOptions m_options  // 0x80
  private           Beyond.LatchUtils.InvokeWhenUnlockm_startLatch  // 0x88
  private           Beyond.LatchUtils.InvokeWhenUnlockm_initLatch  // 0x90
  private           Beyond.Login.LoginWorkFlow.ENodem_lastNode  // 0x98
  private           Beyond.LoginController.MaskCtrl m_maskCtrl  // 0xa0
  private           Beyond.Login.LoginRootPanel     m_rootPanel  // 0xa8
  private           System.Collections.IEnumerator  m_mockLoginRoutine  // 0xb0
  private           System.Action<System.String>    m_onMockLogin  // 0xb8
PROPERTIES:
  maskCtrl  get=0x041E6130
  rootPanel  get=0x041E7A20
EVENTS:
  m_onMockLogin  add=add_m_onMockLogin  remove=remove_m_onMockLogin
METHODS:
  RVA=0x04D2A8F0  token=0x60000C3  System.Void Awake()
  RVA=0x04D59930  token=0x60000C4  System.Void Start()
  RVA=0x041E7A50  token=0x60000C5  System.Single GetEnterGamePanelPredelay()
  RVA=0x034089F0  token=0x60000C6  System.Void OnValueChanged(Beyond.Login.LoginViewModel viewModel)
  RVA=0x041E51F0  token=0x60000C7  System.Void InitUI(Beyond.LoginController.FOptions options)
  RVA=0x0380AB70  token=0x60000C8  System.Void InvokeWhenStart(System.Action callback)
  RVA=0x04A22370  token=0x60000C9  System.Collections.IEnumerator BeforeDestroy()
  RVA=-1  // generic def  token=0x60000CA  System.Collections.IEnumerator _InstantiateUIFromRes(System.String resPath, UnityEngine.Transform parent, Beyond.Misc.ObjectRef objRef)
  RVA=0x04CB24C0  token=0x60000CB  System.Void _InitImpl()
  RVA=0x04CB2510  token=0x60000CC  System.Collections.IEnumerator _InitCoroutine()
  RVA=0x041E4A80  token=0x60000CD  System.Void _InitVersionBtn()
  RVA=0x03AF7270  token=0x60000D0  System.Void MockLoginWithCallback(System.String fixedUID, System.Action<System.String> callback)
  RVA=0x03AF7370  token=0x60000D1  System.Collections.IEnumerator _MockLoginCoroutine(System.String fixedUID)
  RVA=0x049C1650  token=0x60000D2  System.Void .ctor()
  RVA=0x04D84E00  token=0x60000D3  System.Void <MockLoginWithCallback>b__44_0()
END_CLASS

CLASS: Beyond.LoginContext
TYPE:  class
TOKEN: 0x2000032
SIZE:  0x58
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Beyond.LoginController          m_controller  // 0x10
  private           System.Boolean                  m_isDisposed  // 0x18
  private           System.Boolean                  <quickStart>k__BackingField  // 0x19
  private           System.Boolean                  <firstLogin>k__BackingField  // 0x1a
  private           Beyond.Login.LoginWorkFlow      <workflow>k__BackingField  // 0x20
  private           Beyond.Login.LoginViewProperty  <viewProp>k__BackingField  // 0x28
  private           Beyond.LoginManager.AssetLoader <loader>k__BackingField  // 0x30
  private           Beyond.LoginManager.FlowWithMaskMgr<flowWithMask>k__BackingField  // 0x38
  private           Beyond.Login.NetUsagePolicy     <netUsagePolicy>k__BackingField  // 0x40
  private           Beyond.Login.LoginGameUpdater   <gameUpdater>k__BackingField  // 0x48
  private           System.Boolean                  <startConnectServer>k__BackingField  // 0x50
PROPERTIES:
  quickStart  get=0x04D866A0  set=0x04D866C0
  firstLogin  get=0x015AF940  set=0x04D866E0
  workflow  get=0x04D862C0  set=0x05390F40
  viewProp  get=0x04D86240  set=0x02FDB880
  loader  get=0x011F36E0  set=0x05396200
  flowWithMask  get=0x04D85A50  set=0x046A91B0
  netUsagePolicy  get=0x04D85A60  set=0x042B4AE0
  gameUpdater  get=0x04D86200  set=0x035A41A0
  startConnectServer  get=0x04D86800  set=0x04D86840
METHODS:
  RVA=0x041E6090  token=0x6000127  Beyond.LoginController ViewOnlyController()
  RVA=0x0380A3F0  token=0x6000128  System.Void Init(System.Boolean quickStart, Beyond.LoginController controller, Beyond.Login.LoginWorkFlow workflow, Beyond.Login.LoginViewProperty viewProp, Beyond.LoginManager.AssetLoader loader, Beyond.LoginManager.FlowWithMaskMgr flowWithMask, Beyond.Login.NetUsagePolicy netUsagePolicy, Beyond.Login.LoginGameUpdater gameUpdater)
  RVA=0x03A82B00  token=0x6000129  System.Void StartCoroutine(System.Collections.IEnumerator routine)
  RVA=0x0699286C  token=0x600012A  System.Void StopCoroutine(System.Collections.IEnumerator routine)
  RVA=0x06992718  token=0x600012B  System.Void SendEvent(Beyond.ELoginEvent evt, System.Object param)
  RVA=0x041E3F20  token=0x600012C  System.Void Dispose()
  RVA=0x06992798  token=0x600012D  System.Void ShutDown()
  RVA=0x069926C4  token=0x600012E  System.Boolean IsWorkflowDisposed()
  RVA=0x06992678  token=0x600012F  System.Boolean IsDisposed()
  RVA=0x0380A4D0  token=0x6000130  System.Void _DisposeImpl()
  RVA=0x041E4070  token=0x6000131  System.Collections.IEnumerator _DisposeCoroutine(Beyond.LoginController legacyController, System.Action disposeAction)
  RVA=0x041E1670  token=0x6000132  System.Void .ctor()
END_CLASS

CLASS: Beyond.ELoginEvent
TYPE:  sealed struct
TOKEN: 0x2000034
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.ELoginEvent              None  // const
  public    static  Beyond.ELoginEvent              V2C_U8Login  // const
  public    static  Beyond.ELoginEvent              V2C_EnterGame  // const
  public    static  Beyond.ELoginEvent              V2C_Logout  // const
  public    static  Beyond.ELoginEvent              V2C_ResourceRepair  // const
  public    static  Beyond.ELoginEvent              V2C_OpenAgeTips  // const
  public    static  Beyond.ELoginEvent              V2C_CloseAgeTips  // const
  public    static  Beyond.ELoginEvent              V2C_OpenServerSelect  // const
  public    static  Beyond.ELoginEvent              V2C_CloseServerSelect  // const
  public    static  Beyond.ELoginEvent              V2C_SelectServer  // const
  public    static  Beyond.ELoginEvent              V2C_ConfirmServer  // const
  public    static  Beyond.ELoginEvent              V2C_Relogin  // const
  public    static  Beyond.ELoginEvent              V2C_ReplayGsLogin  // const
  public    static  Beyond.ELoginEvent              C2V_PrepareGsLogin  // const
METHODS:
END_CLASS

CLASS: Beyond.LoginTextDefines
TYPE:  static class
TOKEN: 0x2000035
SIZE:  0x10
FIELDS:
  public    static  System.String                   CS_LOGIN_CONN_FAIL  // const
  public    static  System.String                   HOTUPDATE_INDEX_FAILED  // const
  public    static  System.String                   HOTUPDATE_IO_FAILED  // const
  public    static  System.String                   HOTUPDATE_CHECK_VERSION  // const
  public    static  System.String                   HOTUPDATE_DOWNLOADING  // const
  public    static  System.String                   HOTUPDATE_DECOMPRESSING  // const
  public    static  System.String                   HOTUPDATE_DOWNLOAD_ERROR  // const
  public    static  System.String                   HOTUPDATE_DOWNLOAD_DISKFULL  // const
  public    static  System.String                   HOTUPDATE_LOCAL_RES_CHECK  // const
  public    static  System.String                   HOTUPDATE_PRELOAD_RES  // const
  public    static  System.String                   HOTUPDATE_INVALID_LOCAL_FILES  // const
  public    static  System.String                   HOTUPDATE_INIT_RES_CHANGED  // const
  public    static  System.String                   HOTUPDATE_DOWNLOAD_IO_ERROR  // const
  public    static  System.String                   HOTUPDATE_DOWNLOAD_DATA_CHECK_ERROR  // const
  public    static  System.String                   HOTUPDATE_DOWNLOAD_START_DOWNLOAD_ERROR  // const
  public    static  System.String                   HOTUPDATE_DECOMPRESS_ERROR  // const
  public    static  System.String                   REMOTE_CFG_GAME_CFG_ERROR  // const
  public    static  System.String                   REMOTE_CFG_GAME_CFG_TIMEOUT  // const
  public    static  System.String                   REMOTE_CFG_NETWORK_CFG_ERROR  // const
  public    static  System.String                   REMOTE_CFG_NETWORK_CFG_TIMEOUT  // const
  public    static  System.String                   REMOTE_CFG_RES_VERSION_ERROR  // const
  public    static  System.String                   REMOTE_CFG_RES_VERSION_TIMEOUT  // const
  public    static  System.String                   REMOTE_CFG_GAME_VERSION_ERROR  // const
  public    static  System.String                   REMOTE_CFG_GAME_VERSION_TIMEOUT  // const
  public    static  System.String                   LOGIN_JUDGE_EXIT_GAME  // const
  public    static  System.String                   LOGIN_JUDGE_LOGOUT  // const
  public    static  System.String                   LOGIN_JUDGE_RES_REPAIR  // const
  public    static  System.String                   LOGIN_AGE_TIPS_FAILED  // const
  public    static  System.String                   LOGIN_TXT_DIALOG_CONFIRM  // const
  public    static  System.String                   LOGIN_TXT_DIALOG_CANCEL  // const
  public    static  System.String                   LOGIN_AUDIO_LANG_CN  // const
  public    static  System.String                   LOGIN_AUDIO_LANG_JP  // const
  public    static  System.String                   LOGIN_AUDIO_LANG_EN  // const
  public    static  System.String                   LOGIN_AUDIO_LANG_KR  // const
  public    static  System.String                   LOGIN_TXT_NETWORK_ERROR  // const
  public    static  System.String                   LOGIN_GAME_UPGRADED  // const
  public    static  System.String                   LOGIN_TXT_SERVER_CLOSED  // const
  public    static  System.String                   LOGIN_TXT_SERVER_CLOSED_DATETIME  // const
  public    static  System.String                   LOGIN_TXT_USR_BAN_DATETIME  // const
  public    static  System.String                   LOGIN_TXT_HOTUPDATE_CLEAR_DOWNLOAD_FOLDER  // const
  public    static  System.String                   LOGIN_GET_SERVER_LIST_FAILED  // const
  public    static  System.String                   LOGIN_TEXT_SERVER_ZONE_FAILED  // const
  public    static  System.String                   LOGIN_TEXT_GET_APP_VERSION_FAILED  // const
  public    static  System.String                   LOGIN_TEXT_ALERT_CARRIER_NETWORK  // const
  public    static  System.String                   LOGIN_TEXT_ERROR_DOWNLOAD_HOTUPDATE_RES_FAILED  // const
  public    static  System.String                   LOGIN_SHADER_WARM_UP_PRELOAD_RES  // const
  public    static  System.String                   LOGIN_GAME_UPDATE_TIPS_DOWNLOADING  // const
  public    static  System.String                   LOGIN_GAME_UPDATE_TIPS_PAUSE  // const
  public    static  System.String                   LOGIN_GAME_UPDATE_ALERT_NEWER  // const
  public    static  System.String                   LOGIN_GAME_UPDATE_ERROR_DISK_FULL  // const
  public    static  System.String                   LOGIN_GAME_UPDATE_ALERT_SUC  // const
  public    static  System.String                   LOGIN_GAME_UPDATE_ERROR_CHECK_FOR_UPDATE_COMMON  // const
  public    static  System.String                   LOGIN_GAME_UPDATE_ERROR_COMMON  // const
  public    static  System.String                   LOGIN_GAME_UPDATE_TITLE  // const
  public    static  System.String                   LOGIN_GOTO_SHOP_FOR_GAME_UPDATE  // const
  public    static  System.String                   LOGIN_SDK_INIT_FAILED  // const
  public    static  System.String                   LOGIN_DOWNLOADING_SPEED_HINT  // const
  public    static  System.String                   LOGIN_SDK_INIT_FAILED_NEED_QUIT_GAME  // const
  public    static  System.String                   LOGIN_NETWORK_CONGESTION_TITLE  // const
  public    static  System.String                   LOGIN_LOW_SCORE_DEVICE_TIP  // const
  public    static  System.String                   LOGIN_REGISTRY_WRITE_FAILED  // const
METHODS:
END_CLASS

CLASS: Beyond.LoginManager
TYPE:  class
TOKEN: 0x2000036
SIZE:  0x50
EXTENDS: Beyond.ExplicitSingleton`1
FIELDS:
  public    static  System.Int64                    MIN_BYTES_SIZE_TO_NOTIFY_IF_NOT_WIFI  // const
  private           Beyond.Resource.ISceneHandle    m_sceneHandle  // 0x10
  private           Beyond.LoginContext             m_context  // 0x18
  private           Beyond.LatchUtils.InvokeWhenUnlockm_workflowFinishLatch  // 0x20
  private           Beyond.LatchUtils.InvokeWhenUnlockm_workflowStartLatch  // 0x28
  private           System.Collections.Generic.Queue<System.WeakReference<Beyond.Login.LoginSceneComponent>>m_pendingSceneComps  // 0x30
  private           System.Collections.Generic.HashSet<Beyond.Login.LoginSceneComponent>m_sceneComps  // 0x38
  private           System.Boolean                  <quickStart>k__BackingField  // 0x40
  private           System.Object                   m_dialogBlocker  // 0x48
PROPERTIES:
  quickStart  get=0x02D480A0  set=0x02D480B0
  firstLogin  get=0x06993898
METHODS:
  RVA=0x04A20900  token=0x600013C  System.Collections.IEnumerator InitCoroutine(System.Boolean quickStart)
  RVA=0x06992F48  token=0x600013D  System.Boolean IsReLoginInProgress()
  RVA=0x0699334C  token=0x600013E  System.Void OnReleaseMainGame()
  RVA=0x04A20980  token=0x600013F  System.Boolean IsWorkflowInProgress()
  RVA=0x06992C60  token=0x6000140  System.Void ClearSync()
  RVA=0x0380E5E0  token=0x6000141  System.Void Clear()
  RVA=0x0380E1E0  token=0x6000142  System.Void _ReleaseLoginScene()
  RVA=0x04B523D0  token=0x6000143  System.Void InvokeWhenWorkFlowFinished(System.Action callback)
  RVA=0x0340BA80  token=0x6000144  System.Void SceneComponentOnly_Bind(Beyond.Login.LoginSceneComponent comp)
  RVA=0x041E5EA0  token=0x6000145  System.Void _BindPendingComponents()
  RVA=0x069933D8  token=0x6000146  System.Void ToggleLoginBindingGroup(System.Boolean enable)
  RVA=0x06992A78  token=0x6000147  System.Void AlertDialog(System.String desc, System.Action callback)
  RVA=0x06993530  token=0x6000148  System.Void VocResSelDialog(System.Action callback)
  RVA=0x069934A0  token=0x6000149  System.Collections.IEnumerator VocResSelDialogWithConfirm()
  RVA=0x06992990  token=0x600014A  System.Collections.IEnumerator AlertDialogWithResult(System.String desc, Beyond.Misc.ObjectRef outRef, System.Action<Beyond.Misc.ObjectRef> callback)
  RVA=0x069928E8  token=0x600014B  System.Collections.IEnumerator AlertDialogWithConfirm(System.String desc)
  RVA=0x069930FC  token=0x600014C  System.Void JudgeDialog(System.String desc, System.Action onPositive, System.Action onNegative)
  RVA=0x06993060  token=0x600014D  System.Void JudgeDialog(Beyond.Login.LoginJudgeDialog.FParams param)
  RVA=0x069936D0  token=0x600014E  System.Void _StartShowJudgeDialogCoroutine(Beyond.Login.LoginJudgeDialog.FParams param)
  RVA=-1  // generic def  token=0x600014F  System.Collections.IEnumerator _ShowDialogCoroutine(System.String resPath, TParam param, System.Action<TDialog,TParam> setParamFunc)
  RVA=0x041E5AD0  token=0x6000150  System.Void MockLoginWithCallback(System.Action<System.String> callback)
  RVA=0x069931EC  token=0x6000151  System.Void MockManualSelectServer(System.String uid, System.Action callback)
  RVA=0x0380AC70  token=0x6000152  System.Void _Init()
  RVA=0x0340B9B0  token=0x6000153  System.Boolean IsLoginSceneLoading()
  RVA=0x0380A660  token=0x6000154  System.Void _PrepareBeforeLoginProcess()
  RVA=0x04CA73D0  token=0x6000155  System.Collections.IEnumerator _LoginWorkFlowCoroutine(Beyond.LoginController nullableCtrl)
  RVA=0x041E5120  token=0x6000156  System.Void _ClearWorkflowAndContext(System.Boolean isShutDownMode)
  RVA=0x04CAF5B0  token=0x6000157  System.Collections.IEnumerator FetchRemoteConfigUtilSuc(Beyond.Cfg.IRemoteConfig config, System.String configEnv, System.String keyErrorWithCode, System.String keyTimeout)
  RVA=0x03C0A3F0  token=0x6000158  System.String _ExtractErrorInfo(Beyond.Network.HttpSender.Result result, System.String keyErrorWithCode, System.String keyTimeout)
  RVA=0x06992CC0  token=0x6000159  System.Void DevOnlyCheckEnableLocalResMode(System.Boolean& enable)
  RVA=0x06992D40  token=0x600015A  System.Void DevOnlyUseLocalResVersion(Beyond.Cfg.ResVersionDevCfg config, System.Boolean& useLocalResVersion)
  RVA=0x04A7D450  token=0x600015B  System.Void .ctor()
END_CLASS

CLASS: Beyond.VietnamLoginAgePanel
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.String                   PREFAB_PATH  // const
  private   static  Beyond.Resource.FAssetProxyHandles_prefabAssetHandle  // static @ 0x0
  public    static  UnityEngine.GameObject          instance  // static @ 0x18
METHODS:
  RVA=0x041E4E90  token=0x60001AB  System.Void Init()
  RVA=0x06995914  token=0x60001AC  System.Void UnInit()
  RVA=0x0699598C  token=0x60001AD  System.Void _Init()
  RVA=0x06995C14  token=0x60001AE  System.Void _UnInit()
  RVA=0x06995828  token=0x60001AF  System.Void OnApplicationQuit()
  RVA=0x05393520  token=0x60001B0  System.Void .ctor()
END_CLASS

CLASS: Beyond.VietnamLoginAgeTipsPanelCtrl
TYPE:  class
TOKEN: 0x200004A
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UIDrag                _UIDrag  // 0x18
  private           Beyond.UI.UIButton              _tipsBtn  // 0x20
  private           Beyond.UI.UIButton              _circleBtn  // 0x28
  private           Beyond.UI.UIState.UIStateController_stateController  // 0x30
METHODS:
  RVA=0x06995DB4  token=0x60001B1  System.Void Awake()
  RVA=0x0699608C  token=0x60001B2  System.Void _SetStateTips(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x06995FF0  token=0x60001B3  System.Void _SetStateCircle(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x05393520  token=0x60001B4  System.Void .ctor()
END_CLASS

CLASS: Beyond.RenderPipelineDataLoader
TYPE:  static class
TOKEN: 0x200004B
SIZE:  0x10
FIELDS:
  public    static  Beyond.Resource.FAssetProxyHandles_globalSettings  // static @ 0x0
  public    static  Beyond.Resource.FAssetProxyHandles_pipelineAsset  // static @ 0x14
METHODS:
  RVA=0x041E42D0  token=0x60001B5  System.Void LoadSimpleRpData()
  RVA=0x0380ECC0  token=0x60001B6  System.Void PreloadFullRpResources()
  RVA=0x03C12270  token=0x60001B7  System.Boolean IsPreLoadFinished()
  RVA=0x041E44D0  token=0x60001B8  System.Void LoadFullRpResources()
END_CLASS

CLASS: Beyond.TailGameLoop
TYPE:  class
TOKEN: 0x200004C
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x02F5C450  token=0x60001B9  System.Void Update()
  RVA=0x02F5C8C0  token=0x60001BA  System.Void LateUpdate()
  RVA=0x05393520  token=0x60001BB  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginCheckForUpdateNode
TYPE:  class
TOKEN: 0x200006F
SIZE:  0x30
EXTENDS: Node
FIELDS:
  private           Beyond.Login.LoginGameUpdater   m_sdk  // 0x20
  private           System.Boolean                  m_checkSuccess  // 0x28
  private   static  System.String                   REQUEST_API  // const
  private   static  System.String                   REQUEST_PARAM_FORMAT  // const
  private   static  System.String                   REQUEST_URL_FORMAT  // const
  private   static  System.String                   PRAM_SOURCE  // const
PROPERTIES:
  type  get=0x049B1850
METHODS:
  RVA=0x045E8660  token=0x6000240  UnityEngine.CustomYieldInstruction Work()
  RVA=0x045E8760  token=0x6000241  System.Collections.IEnumerator _CheckForUpdateCoroutine()
  RVA=0x014F5A60  token=0x6000242  System.Collections.IEnumerator _AlertAndQuitForExtraConfigFatalError()
  RVA=0x04D351D0  token=0x6000243  System.Collections.IEnumerator AlertNoMatchDeviceBeforeLogin()
  RVA=0x06996F20  token=0x6000244  System.Collections.IEnumerator _CheckForUpdateCoroutineMobile()
  RVA=0x04D1F080  token=0x6000245  System.Collections.IEnumerator _CheckForUpdateCoroutinePC()
  RVA=0x04D1F010  token=0x6000246  System.Collections.IEnumerator _CheckForUpdatePC()
  RVA=0x04CE7A50  token=0x6000247  System.Collections.IEnumerator _GetOnlineAppVersion(Beyond.Misc.ObjectRef outRef)
  RVA=0x038128E0  token=0x6000248  System.String _GetRequestPram()
  RVA=0x041E1670  token=0x6000249  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginCreateRoleNode
TYPE:  class
TOKEN: 0x2000078
SIZE:  0x28
EXTENDS: Node
FIELDS:
  public    static  System.String                   LUA_SYS_NAME  // const
  private           Beyond.Login.LoginCreateRoleNode.WorkProcessm_activeProcess  // 0x20
PROPERTIES:
  type  get=0x049B1820
METHODS:
  RVA=0x046DD050  token=0x6000276  System.Void OnDispose()
  RVA=0x06996FB0  token=0x6000277  System.Boolean CanCancel()
  RVA=0x04CCA6C0  token=0x6000278  UnityEngine.CustomYieldInstruction Work()
  RVA=0x041E1670  token=0x6000279  System.Void .ctor()
  RVA=0x06997004  token=0x600027A  System.Void <>iFixBaseProxy_OnDispose()
  RVA=0x06996FFC  token=0x600027B  System.Boolean <>iFixBaseProxy_CanCancel()
END_CLASS

CLASS: Beyond.Login.LoginDownloadTask
TYPE:  class
TOKEN: 0x200007A
SIZE:  0x90
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Boolean                  m_isDownloadFinished  // 0x10
  private           Beyond.SDK.HGDownloader.TaskHandlerm_downloadTask  // 0x18
  private           Beyond.Resource.HotUpdate.FResourceDiffm_resDiff  // 0x20
  private           Beyond.Resource.HotUpdate.LocalResPrefm_localResPref  // 0x60
  private           System.String                   m_patchInfo  // 0x68
  private           System.Collections.IEnumerator  m_mockCoroutine  // 0x70
  private           System.Boolean                  m_isInitial  // 0x78
  private           System.String                   <vitalError>k__BackingField  // 0x80
  private           Beyond.Login.NetUsagePolicy     m_netUsagePolicy  // 0x88
PROPERTIES:
  vitalError  get=0x04D86260  set=0x04CD9970
METHODS:
  RVA=0x069984DC  token=0x600027F  System.Void .ctor(Beyond.Resource.HotUpdate.FResourceDiff resDiff, Beyond.Resource.HotUpdate.LocalResPref localResPref, System.String patchInfo, System.Boolean isInitial, Beyond.Login.NetUsagePolicy netUsagePolicy)
  RVA=0x069970FC  token=0x6000282  Beyond.SDK.HGDownloader.DownloadProgress GetDownloadProgress()
  RVA=0x06997098  token=0x6000283  System.Single GetDecompressProgress()
  RVA=0x06997194  token=0x6000284  Beyond.SDK.HGDownloader.WorkState GetWorkState()
  RVA=0x069971F4  token=0x6000285  System.Boolean Tick()
  RVA=0x0699700C  token=0x6000286  System.Void Dispose()
  RVA=0x069981DC  token=0x6000287  System.Void _StartDownload()
  RVA=0x06997E4C  token=0x6000288  System.Void _StartDownloadImpl()
  RVA=0x06997260  token=0x6000289  System.Collections.Generic.List<Beyond.SDK.HGFolder> _GetAllFileList(Beyond.Resource.HotUpdate.LocalResPref localResPref)
  RVA=0x0699773C  token=0x600028A  System.Collections.Generic.List<Beyond.SDK.HGFileInfo> _GetFileList(System.String rootUrl)
  RVA=0x06997B98  token=0x600028B  System.Void _OnDownloadFinish()
  RVA=0x0699797C  token=0x600028C  System.Boolean _OnDownloadError(Beyond.SDK.HGRetCodeType codeType, System.Int32 errorCode)
  RVA=0x06997BE8  token=0x600028D  System.Void _OnDownloadPaused(Beyond.SDK.HGDownloader.PauseReason reason)
END_CLASS

CLASS: Beyond.Login.LoginGameSettingInitializer
TYPE:  class
TOKEN: 0x200007D
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.Boolean                  s_isInitialized  // static @ 0x0
  private           UnityEngine.RectTransform       m_rectTransform  // 0x18
  private           Beyond.UI.UICanvasScaleHelper   m_canvasScaleHelper  // 0x20
METHODS:
  RVA=0x03BFCCE0  token=0x6000293  System.Void Awake()
  RVA=0x03BFCD40  token=0x6000294  System.Void OnEnable()
  RVA=0x03BFCE00  token=0x6000295  System.Void _TryInitializeComponents()
  RVA=0x03BFCDC0  token=0x6000296  System.Void _InitGameSettingStateInLogin()
  RVA=0x03BFD130  token=0x6000297  System.Void _InitGameSettingNotchPadding()
  RVA=0x05393520  token=0x6000298  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginGameUpdateNode
TYPE:  class
TOKEN: 0x200007E
SIZE:  0x30
EXTENDS: Node
FIELDS:
  private           Beyond.Login.LoginGameUpdater   m_sdk  // 0x20
  private           Beyond.Login.NetUsagePolicy     m_netUsagePolicy  // 0x28
PROPERTIES:
  type  get=0x049B17C0
METHODS:
  RVA=0x045E87D0  token=0x600029A  UnityEngine.CustomYieldInstruction Work()
  RVA=0x045E88E0  token=0x600029B  System.Collections.IEnumerator _GameUpdateCoroutine()
  RVA=0x06998CC0  token=0x600029C  System.Collections.IEnumerator _GameUpdateCoroutineMobileImpl()
  RVA=0x041E1670  token=0x600029D  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginGameUpdater
TYPE:  class
TOKEN: 0x2000081
SIZE:  0x50
FIELDS:
  private   static  System.Single                   COOLDOWN_INSTALL  // const
  private   static  System.Int32                    ERROR_CODE_NETWORK  // const
  private   static  System.Int32                    ERROR_INIT_FAILED  // const
  private   static  System.Int32                    ERROR_DESERIALIZE_FAILED  // const
  public    static  System.Int32                    ERROR_ACTION  // const
  private   static  System.Int32                    WAIT_TIME_SECONDS_MIN  // const
  private   static  System.Int32                    WAIT_TIME_SECONDS_MAX  // const
  private           Beyond.Login.LoginGameUpdater.Optionsm_options  // 0x10
  private           Beyond.SDK.IGameUpdateInterface m_sdkImpl  // 0x28
  private           System.Boolean                  m_hasUpdateAlerted  // 0x30
  private           Beyond.Login.LoginGameUpdater.Contextm_context  // 0x38
  private           Beyond.Login.LoginProgress      m_progress  // 0x40
  private           Beyond.Login.LoginHotUpdateProp m_hotUpdateProp  // 0x48
METHODS:
  RVA=0x045E8E10  token=0x60002AA  System.Void Clear()
  RVA=0x0380A530  token=0x60002AB  System.Void .ctor(Beyond.Login.LoginGameUpdater.Options options)
  RVA=0x06998D50  token=0x60002AC  System.Collections.IEnumerator CheckLastGameInfo(Beyond.Login.LoginGameUpdater.Result result)
  RVA=0x06998DF8  token=0x60002AD  System.Collections.IEnumerator DoUpdate(Beyond.Login.LoginGameUpdater.Result result)
  RVA=0x0699929C  token=0x60002AE  System.Collections.IEnumerator _GetLastGameInfo(Beyond.Login.LoginGameUpdater.Context context)
  RVA=0x0699958C  token=0x60002AF  System.Collections.IEnumerator _UpdateGame(Beyond.Login.LoginGameUpdater.Context context)
  RVA=0x069991F4  token=0x60002B0  System.Collections.IEnumerator _DoTaskUpdating(Beyond.Login.LoginGameUpdater.Context context)
  RVA=0x06999444  token=0x60002B1  System.Void _OnDownloadStart()
  RVA=0x06999344  token=0x60002B2  System.Void _OnDownloadProgress(System.Int64 curSize, System.Int64 totalSize, System.Int64 speed)
  RVA=0x06999774  token=0x60002B3  System.Collections.IEnumerator _YieldErrorAlert(System.String desc)
  RVA=0x069996D4  token=0x60002B4  System.Collections.IEnumerator _YieldCheckNetUsagePolicy(System.Int64 downloadSize)
  RVA=0x06999634  token=0x60002B5  System.Collections.IEnumerator _WaitForTaskCancel(System.Int64 taskId)
  RVA=0x06999188  token=0x60002B6  System.Void _CreateDirectoryIfNotExists(System.String folderPath)
  RVA=0x03A80A00  token=0x60002B7  System.Void ClearWaitProgress()
  RVA=0x06999044  token=0x60002B8  System.Void ShowWaitProgress()
  RVA=0x06998EA0  token=0x60002B9  System.Int32 GetNoAlertCode()
  RVA=0x06998EF0  token=0x60002BA  System.Single GetWaitTime()
END_CLASS

CLASS: Beyond.Login.LoginGSNode
TYPE:  class
TOKEN: 0x2000097
SIZE:  0x28
EXTENDS: Node
FIELDS:
  private   static  System.Int32                    FLOW_NONE  // const
  private   static  System.Int32                    FLOW_WORKING  // const
  private   static  System.Int32                    FLOW_FINISH  // const
  private   static  System.Int32                    FLOW_ABORTING  // const
  private           Beyond.Login.LoginGSNode.EnterGameServiceFlowm_flow  // 0x20
PROPERTIES:
  type  get=0x049B17F0
METHODS:
  RVA=0x049D61A0  token=0x60002FE  UnityEngine.CustomYieldInstruction Work()
  RVA=0x0470B2D0  token=0x60002FF  System.Boolean OnEvent(Beyond.ELoginEvent evt, System.Object param)
  RVA=0x03A847F0  token=0x6000300  System.Collections.IEnumerator _ConfirmServer()
  RVA=0x06998718  token=0x6000301  System.Void _FallbackToInitWithMask(System.Boolean bLogoutAccount)
  RVA=0x06998A38  token=0x6000302  System.Void _OnEventLogout()
  RVA=0x06998B7C  token=0x6000303  System.Void _OnEventResourceRepair()
  RVA=0x0699897C  token=0x6000304  System.Boolean _LogoutToInitNode()
  RVA=0x06998698  token=0x6000305  System.Boolean _FallbackToInitNodeImpl()
  RVA=0x0699889C  token=0x6000306  System.Boolean _FallbackToResRepairImpl()
  RVA=0x041E1670  token=0x6000307  System.Void .ctor()
  RVA=0x06998560  token=0x6000308  System.Void <_OnEventLogout>b__12_0()
  RVA=0x0699856C  token=0x6000309  System.Void <_OnEventResourceRepair>b__13_0()
  RVA=0x06998558  token=0x600030A  System.Boolean <>iFixBaseProxy_OnEvent(Beyond.ELoginEvent P0, System.Object P1)
END_CLASS

CLASS: Beyond.Login.LoginHotUpdateProp
TYPE:  class
TOKEN: 0x20000A1
SIZE:  0x30
EXTENDS: Beyond.Login.LoginProperty`1
FIELDS:
METHODS:
  RVA=0x03A80AD0  token=0x6000347  System.Void .ctor(Beyond.Login.LoginWorkFlow workflow)
END_CLASS

CLASS: Beyond.Login.LoginHotUpdateModel
TYPE:  class
TOKEN: 0x20000A2
SIZE:  0x28
FIELDS:
  public            System.String                   title  // 0x10
  public            System.String                   desc  // 0x18
  public            Beyond.Login.LoginHotUpdateModel.IProgressprogress  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000348  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginHotUpdater
TYPE:  class
TOKEN: 0x20000A5
SIZE:  0x50
EXTENDS: UnityEngine.CustomYieldInstruction
IMPLEMENTS: System.IDisposable
FIELDS:
  private   static  System.Int32                    MODE_SKIP  // const
  private   static  System.Int32                    MODE_DOWNLOAD  // const
  private   static  System.Int32                    MODE_LOCAL  // const
  private   static  System.Boolean                  s_hasInitialAssetChanged  // static @ 0x0
  private   static  System.Boolean                  s_isNeedSoftRestart  // static @ 0x1
  private           Beyond.LoginContext             m_loginCxt  // 0x10
  private           System.Collections.IEnumerator  m_mainRoutine  // 0x18
  private           Beyond.Login.LoginProgress      m_progress  // 0x20
  private           Beyond.Login.LoginHotUpdateProp m_hotupdateProp  // 0x28
  private           Beyond.Login.LoginDownloadTask  m_downloadTask  // 0x30
  private           Beyond.Login.LoginLocalResTask  m_localResTask  // 0x38
  private           System.Collections.IEnumerator  m_downloadingShaderWarmupRoutine  // 0x40
  private           System.Boolean                  m_resRepairMode  // 0x48
  public    static  System.String                   URL_GET_SPLIT  // const
PROPERTIES:
  keepWaiting  get=0x04CEB9B0
METHODS:
  RVA=0x0699997C  token=0x6000350  System.Void MarkInitialAssetChanged()
  RVA=0x0699981C  token=0x6000351  System.Boolean CheckInitialAssetChanged()
  RVA=0x069999E8  token=0x6000352  System.Void MarkNeedClearFolder(System.Boolean isInitial)
  RVA=0x06999ADC  token=0x6000353  System.Boolean NeedClearFolder(System.Boolean isInitial)
  RVA=0x069998F4  token=0x6000354  System.Void MarkClearSuccessed(System.Boolean isInitial)
  RVA=0x06999A70  token=0x6000355  System.Void MarkSoftRestart()
  RVA=0x06999888  token=0x6000356  System.Void ClearSoftRestartFlag()
  RVA=0x03406070  token=0x6000357  System.Boolean IsNeedSoftRestart()
  RVA=0x03A80D30  token=0x6000358  System.Void .ctor(Beyond.LoginContext context, System.Boolean resRepairMode)
  RVA=0x046DD1E0  token=0x600035A  System.Void Dispose()
  RVA=0x03A81090  token=0x600035B  Beyond.Login.LoginHotUpdater StartHotUpdate(Beyond.LoginContext context, System.Boolean resRepairMode)
  RVA=0x03A81140  token=0x600035C  System.Collections.IEnumerator _Work()
  RVA=0x03C07550  token=0x600035D  System.Collections.IEnumerator _CreateMainCoroutine(Beyond.Login.LoginHotUpdater.WorkContext workContext)
  RVA=0x0455F7A0  token=0x600035E  System.Collections.IEnumerator _MainCoroutineModeSkip(Beyond.Login.LoginHotUpdater.WorkContext context)
  RVA=0x0699AEC0  token=0x600035F  System.Collections.IEnumerator _MainCoroutineModeLocal(Beyond.Login.LoginHotUpdater.WorkContext context)
  RVA=0x03C074C0  token=0x6000360  System.Collections.IEnumerator _UpdateResVersion(Beyond.Login.LoginHotUpdater.WorkContext workContext)
  RVA=0x03C079C0  token=0x6000361  System.Void _SetResVersionForLog()
  RVA=0x0699ABA8  token=0x6000362  System.String _GetOnlineResVersion(System.Boolean isInitial)
  RVA=0x06999B60  token=0x6000363  System.String ResourceRootUrl(System.String version, System.Boolean isInitial)
  RVA=0x0699B5B4  token=0x6000364  System.Collections.IEnumerator _UpdateResVersionDev(Beyond.Login.LoginHotUpdater.WorkContext workContext)
  RVA=0x04B45C40  token=0x6000365  System.Collections.IEnumerator _UpdateResVersionProd(Beyond.Login.LoginHotUpdater.WorkContext workContext)
  RVA=0x0699B1AC  token=0x6000366  System.String _ResourcePatchUrl(System.String version, System.Boolean isInitial)
  RVA=0x0699AF68  token=0x6000367  System.String _ResourceFileUrl(System.String version, System.Boolean isInitial)
  RVA=0x0699B0E8  token=0x6000368  System.String _ResourcePatchRootUrl(System.String version, System.Boolean isInitial)
  RVA=0x0699B020  token=0x6000369  System.String _ResourceIndexUrl(System.String version, System.Boolean isInitial)
  RVA=0x0699AE18  token=0x600036A  System.Collections.IEnumerator _MainCoroutineModeDownload(Beyond.Login.LoginHotUpdater.WorkContext workContext)
  RVA=0x0699B264  token=0x600036B  System.Void _StartDownloadingShaderWarmupIfNeeded()
  RVA=0x0699B524  token=0x600036C  System.Void _StopDownloadingShaderWarmupIfNeeded()
  RVA=0x04B21B20  token=0x600036D  System.Collections.IEnumerator _ReloadResourceIndexes(Beyond.Login.LoginHotUpdater.WorkContext workContext)
  RVA=0x0699AC7C  token=0x600036E  System.Collections.IEnumerator _LoadOrDownloadResIndex(System.String onlineVer, System.Boolean isInitial, Beyond.Misc.ObjectRef resIndexRef)
  RVA=0x0699A9F0  token=0x600036F  System.Collections.IEnumerator _GenResIndex(System.String onlineVer, System.Boolean isInitial, Beyond.Login.LoginHotUpdater.WorkContext workContext)
  RVA=0x0699AACC  token=0x6000370  System.Collections.IEnumerator _GenRiff(System.String onlineVer, System.Boolean isInitial, Beyond.Login.LoginHotUpdater.WorkContext workContext)
  RVA=0x0699A004  token=0x6000371  Beyond.Resource.HotUpdate.LocalResPref _CreateUpdatedLocalResPref(System.String onlineVer, System.Boolean isInitial, Beyond.Resource.HotUpdate.LocalResPref oldLocalResPref, Beyond.Resource.HotUpdate.FResourceDiff resDiff, System.Int32& removedFileCount)
  RVA=0x0699A920  token=0x6000372  System.Collections.IEnumerator _GenLocalRef(System.String onlineVer, System.Boolean isInitial, Beyond.Login.LoginHotUpdater.WorkContext workContext)
  RVA=0x06999D78  token=0x6000373  System.Collections.IEnumerator _CheckAndDownloadResource(System.String onlineVer, System.Boolean isInitial, Beyond.Login.LoginHotUpdater.WorkContext workContext)
  RVA=0x0699A808  token=0x6000374  System.Void _EventLogDownloadProgress(System.Boolean& first, System.Double current, System.Double total, System.Int32 target, System.Boolean isInitial)
  RVA=0x0699A72C  token=0x6000375  System.Collections.IEnumerator _DownloadPatchInfo(System.String onlineVer, System.Boolean isInitial, Beyond.Misc.ObjectRef patchInfoRef)
  RVA=0x06999F30  token=0x6000376  System.Collections.IEnumerator _ClearDownloadFolder(Beyond.Login.LoginHotUpdater.WorkContext workContext, System.Boolean needDeleteInitial, System.Boolean needDeleteMain)
  RVA=0x06999E54  token=0x6000377  System.Collections.IEnumerator _CheckLocalResConsistency(System.String onlineVer, System.Boolean isInitial, Beyond.Login.LoginHotUpdater.WorkContext context)
  RVA=0x0699B63C  token=0x6000378  System.Collections.IEnumerator _VitalError(System.String error)
  RVA=0x0699AD58  token=0x6000379  System.Collections.IEnumerator _LoadStreamingIndex(System.Boolean isInitial, Beyond.Login.LoginHotUpdater.WorkContext workContext, Beyond.Misc.ObjectRef resultRef)
  RVA=0x06999C1C  token=0x600037A  System.Void WriteLocalResPrefToFile(Beyond.Resource.HotUpdate.LocalResPref localResPref, Beyond.Login.LoginHotUpdater.WorkContext workContext)
  RVA=0x0455F830  token=0x600037B  System.Int32 _CurrentWorkMode(Beyond.LoginContext context)
END_CLASS

CLASS: Beyond.Login.LoginProgress
TYPE:  class
TOKEN: 0x20000BF
SIZE:  0x50
IMPLEMENTS: IProgress
FIELDS:
  public    static  System.Int32                    MODE_PROG  // const
  public    static  System.Int32                    MODE_SIZE  // const
  public    static  System.Int32                    MODE_IDEL  // const
  public            System.Int32                    mode  // 0x10
  public            System.Double                   current  // 0x18
  public            System.Double                   total  // 0x20
  public            System.Double                   speed  // 0x28
  private           Beyond.Login.LoginProgress.FDownloadSizem_downloadSize  // 0x30
METHODS:
  RVA=0x069A1480  token=0x6000402  System.Double GetCurrent()
  RVA=0x069A157C  token=0x6000403  System.Double GetTotal()
  RVA=0x069A14D0  token=0x6000404  System.String GetInfo()
  RVA=0x041E1670  token=0x6000405  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginInitNode
TYPE:  class
TOKEN: 0x20000C2
SIZE:  0x28
EXTENDS: Node
FIELDS:
  private           System.Boolean                  m_isSDKInited  // 0x20
PROPERTIES:
  type  get=0x049B1790
METHODS:
  RVA=0x045E8940  token=0x600040F  UnityEngine.CustomYieldInstruction Work()
  RVA=0x045E8A20  token=0x6000410  System.Collections.IEnumerator _InitCoroutine()
  RVA=0x0484D6F0  token=0x6000411  System.Collections.IEnumerator _InitRemoteConfigs()
  RVA=0x069A1310  token=0x6000412  System.Collections.IEnumerator _YieldAlertError(System.String errorInfo)
  RVA=0x041E1670  token=0x6000413  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginLocalResTask
TYPE:  class
TOKEN: 0x20000C6
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Beyond.LoginContext             m_loginCxt  // 0x10
  private           Beyond.Login.LoginHotUpdater.WorkContextm_workCxt  // 0x18
METHODS:
  RVA=0x032AF420  token=0x6000426  System.Void .ctor(Beyond.LoginContext loginContext, Beyond.Login.LoginHotUpdater.WorkContext workContext)
  RVA=0x069A13F0  token=0x6000427  System.Collections.IEnumerator Work()
  RVA=0x069A13AC  token=0x6000428  System.Void Dispose()
END_CLASS

CLASS: Beyond.Login.PreloadNode
TYPE:  class
TOKEN: 0x20000C9
SIZE:  0x38
EXTENDS: Node
FIELDS:
  private           Beyond.Login.LoginHotUpdateModel.SimpleProgressm_progress  // 0x20
  private           System.Boolean                  m_getServerListSuc  // 0x28
  private           System.String                   m_errorMsg  // 0x30
PROPERTIES:
  type  get=0x049B16A0
METHODS:
  RVA=0x03A811B0  token=0x6000433  UnityEngine.CustomYieldInstruction Work()
  RVA=0x03A80B70  token=0x6000434  System.Void _ShowPreloadResourcePanel()
  RVA=0x03A81370  token=0x6000435  System.Collections.IEnumerator _LoginPreLoad()
  RVA=0x069A2564  token=0x6000436  System.Void _QuitGame(Beyond.GameAppExitCode exitCode)
  RVA=0x0455F560  token=0x6000437  System.Collections.IEnumerator _WarmUp()
  RVA=0x0455F4F0  token=0x6000438  System.Collections.IEnumerator _GetServerList()
  RVA=0x03C11630  token=0x6000439  System.Void _EnterPreLoad()
  RVA=0x040EADB0  token=0x600043A  System.Collections.IEnumerator _GetServerListInfoMock()
  RVA=0x069A2418  token=0x600043B  System.Collections.IEnumerator _GetServerListInfo()
  RVA=0x040EA1F0  token=0x600043C  System.Void _OnGetServerListSuc()
  RVA=0x069A24A8  token=0x600043D  System.Void _OnGetServerListFail(System.String errorInfo)
  RVA=0x047318D0  token=0x600043E  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginSDKInitNode
TYPE:  class
TOKEN: 0x20000D1
SIZE:  0x28
EXTENDS: Node
FIELDS:
  private           System.Boolean                  m_isSDKInited  // 0x20
PROPERTIES:
  type  get=0x049B1760
METHODS:
  RVA=0x045E8A90  token=0x6000464  UnityEngine.CustomYieldInstruction Work()
  RVA=0x045E8B70  token=0x6000465  System.Collections.IEnumerator _SDKInitCoroutine()
  RVA=0x03C90130  token=0x6000466  System.Collections.IEnumerator _WaitForSDKInited()
  RVA=0x03C90200  token=0x6000467  System.Collections.IEnumerator _InitSubSDKs(System.String env)
  RVA=0x041E1670  token=0x6000468  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.U8LoginNode
TYPE:  class
TOKEN: 0x20000D5
SIZE:  0x28
EXTENDS: Node
FIELDS:
  private           System.Boolean                  m_loginSuc  // 0x20
  private           System.Boolean                  m_isSDKLoginFinished  // 0x21
  private           System.Boolean                  m_isWaitingGameProtocolUpdateCheck  // 0x22
  private           System.Int32                    m_rqstSkipLoginCache  // 0x24
PROPERTIES:
  type  get=0x049B1610
METHODS:
  RVA=0x045E8BE0  token=0x600047C  UnityEngine.CustomYieldInstruction Work()
  RVA=0x045E8CC0  token=0x600047D  System.Collections.IEnumerator _LoginCoroutine()
  RVA=0x04D32A40  token=0x600047E  System.Collections.IEnumerator _FinishLogin()
  RVA=0x069AA6E8  token=0x600047F  System.Boolean OnEvent(Beyond.ELoginEvent evt, System.Object param)
  RVA=0x040EAE20  token=0x6000480  System.Void _StartSDKLogin()
  RVA=0x040EB1E0  token=0x6000481  System.Void _SetCursorOnLoginStart()
  RVA=0x040EB260  token=0x6000482  System.Void _SetCursorOnLoginFinish()
  RVA=0x069AA940  token=0x6000483  System.Void _SkipSDKLogin()
  RVA=0x03A7F950  token=0x6000484  System.Void _ClearLoginCaches()
  RVA=0x01509B20  token=0x6000485  System.Void _StartGameProtocolUpdateCheck()
  RVA=0x01509160  token=0x6000486  System.Void _OnGameProtocolUpdateCheckSucceeded()
  RVA=0x01508230  token=0x6000487  System.Void _OnGameProtocolUpdateCheckFailed()
  RVA=0x01507380  token=0x6000488  System.Void _ClearGameProtocolUpdateCheckEvents()
  RVA=0x041E5BC0  token=0x6000489  System.Void _OnLoginSuc()
  RVA=0x069AA818  token=0x600048A  System.Void _OnLoginFail(System.Object rawInfo)
  RVA=0x041E7AC0  token=0x600048B  System.Void _OnSDKViewDisabled()
  RVA=0x069AA76C  token=0x600048C  System.Void _OnLoginCanceled()
  RVA=0x041E5E00  token=0x600048D  System.Void _EnableBlurMask(System.Boolean enable)
  RVA=0x041E1670  token=0x600048E  System.Void .ctor()
  RVA=0x01505980  token=0x600048F  System.Void <_StartSDKLogin>b__10_0(System.String sessionToken)
  RVA=0x01506540  token=0x6000490  System.Void <_StartSDKLogin>b__10_1(System.Object pram)
  RVA=0x06998558  token=0x6000491  System.Boolean <>iFixBaseProxy_OnEvent(Beyond.ELoginEvent P0, System.Object P1)
END_CLASS

CLASS: Beyond.Login.LoginProperty`1
TYPE:  class
TOKEN: 0x20000D8
IMPLEMENTS: IWorkerUpdateOnce
FIELDS:
  private           T                               m_value  // 0x0
  private           System.Collections.Generic.HashSet<Beyond.Login.ILoginBinder<T>>m_binders  // 0x0
  private           Beyond.Login.LoginWorkFlow      m_workflow  // 0x0
  private           System.Boolean                  <isDataChanged>k__BackingField  // 0x0
PROPERTIES:
  isDataChanged  get=-1  // not resolved  set=-1  // not resolved
  value  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600049E  System.Void .ctor(Beyond.Login.LoginWorkFlow workflow)
  RVA=-1  // not resolved  token=0x60004A2  System.Void NotifyDataChanged()
  RVA=-1  // not resolved  token=0x60004A3  System.Void Bind(Beyond.Login.ILoginBinder<T> binder)
  RVA=-1  // not resolved  token=0x60004A4  System.Void Worker_UpdateOnce()
END_CLASS

CLASS: Beyond.Login.ILoginBinder`1
TYPE:  interface
TOKEN: 0x20000D9
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60004A5  System.Void OnValueChanged(T value)
END_CLASS

CLASS: Beyond.Login.ILoginContextReceiver
TYPE:  interface
TOKEN: 0x20000DA
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60004A6  System.Void OnLoginContext(Beyond.LoginContext context)
END_CLASS

CLASS: Beyond.Login.LoginViewModel
TYPE:  class
TOKEN: 0x20000DB
SIZE:  0x30
FIELDS:
  public            Beyond.Login.LoginWorkFlow.ENodeworkCachedCurNode  // 0x10
  public            Beyond.Login.LoginWorkFlow.ENodeworkCurNode  // 0x14
  public            System.Boolean                  isFirstLogin  // 0x18
  private           System.Int32                    <rqstSkipU8Login>k__BackingField  // 0x1c
  public            System.Boolean                  isU8LoginCancelled  // 0x20
  private           System.Int32                    <rqstResRepairCode>k__BackingField  // 0x24
  public            Beyond.Login.LoginViewModel.FAgeTipsPolicyageTipsPolicy  // 0x28
PROPERTIES:
  rqstSkipU8Login  get=0x04D86310  set=0x04D86320
  rqstResRepairCode  get=0x011F0020  set=0x011F0030
METHODS:
  RVA=0x04CD95A0  token=0x60004A7  System.Void .ctor()
  RVA=0x03408B00  token=0x60004AC  System.Boolean IsAgeTipsVisible()
  RVA=0x034089C0  token=0x60004AD  System.Boolean IsGovTipsVisible()
  RVA=0x03407160  token=0x60004AE  System.Boolean IsMenuVisible()
  RVA=0x069A15CC  token=0x60004AF  System.Void RequestResourceRepair()
  RVA=0x069A1618  token=0x60004B0  System.Void RequestSkipU8Login()
  RVA=0x04CD95D0  token=0x60004B1  Beyond.Login.LoginViewModel.FAgeTipsPolicy _LoadAgeTipsPolicy()
END_CLASS

CLASS: Beyond.Login.LoginViewProperty
TYPE:  class
TOKEN: 0x20000DD
SIZE:  0x48
EXTENDS: Beyond.Login.LoginProperty`1
FIELDS:
  private           Beyond.Login.LoginHotUpdateProp <hotUpdateProp>k__BackingField  // 0x30
  private           Beyond.Login.LoginAgeTipsProp   <ageTipsProp>k__BackingField  // 0x38
  private           Beyond.Login.LoginServerSelectProp<serverSelectProp>k__BackingField  // 0x40
PROPERTIES:
  hotUpdateProp  get=0x011F36E0  set=0x05396200
  ageTipsProp  get=0x04D85A50  set=0x046A91B0
  serverSelectProp  get=0x04D85A60  set=0x042B4AE0
METHODS:
  RVA=0x03A7F7F0  token=0x60004B9  System.Void .ctor(Beyond.Login.LoginWorkFlow workflow)
END_CLASS

CLASS: Beyond.Login.LoginAgeTipsModel
TYPE:  class
TOKEN: 0x20000DE
SIZE:  0x20
FIELDS:
  public            System.String                   contentCache  // 0x10
  public            System.Boolean                  isShow  // 0x18
  public            System.Boolean                  isFetchingContent  // 0x19
METHODS:
  RVA=0x03A809C0  token=0x60004BA  System.Boolean IsContentEmpty()
  RVA=0x04B480F0  token=0x60004BB  System.Boolean CanDisplay()
  RVA=0x041E1670  token=0x60004BC  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginAgeTipsProp
TYPE:  class
TOKEN: 0x20000DF
SIZE:  0x30
EXTENDS: Beyond.Login.LoginProperty`1
FIELDS:
METHODS:
  RVA=0x03A80950  token=0x60004BD  System.Void .ctor(Beyond.Login.LoginWorkFlow workflow)
  RVA=0x069A119C  token=0x60004BE  System.Void ClearContentCache()
  RVA=0x069A1260  token=0x60004BF  System.Void UpdateContent(System.String content)
END_CLASS

CLASS: Beyond.Login.LoginWorkFlow
TYPE:  class
TOKEN: 0x20000E0
SIZE:  0x50
IMPLEMENTS: System.IDisposable
FIELDS:
  public    static  System.String                   CURSOR_KEY_LOGIN  // const
  private   static  System.Boolean                  s_hasEnteredPreloadThisSession  // static @ 0x0
  private   static readonly Beyond.Login.LoginWorkFlow.ENode[]NODE_ORDER  // static @ 0x8
  private   static  Beyond.Login.LoginWorkFlow.ENodeNEED_GAME_LOGIN_STATE_BEFORE_NODE  // const
  private   readonly Beyond.Login.LoginWorkFlow.Node[]m_nodes  // 0x10
  private   readonly Beyond.LoginContext             m_context  // 0x18
  private           Beyond.Login.LoginWorkFlow.FWorkm_work  // 0x20
  private           Beyond.Login.LoginWorkFlow.Workerm_worker  // 0x30
  private           System.Collections.Generic.HashSet<Beyond.Login.LoginWorkFlow.IEventReceiver>m_evtRecvrs  // 0x38
  private           System.Collections.Generic.List<Beyond.Login.LoginWorkFlow.IEventReceiver>m_recvrBuffer  // 0x40
  private           System.Boolean                  <isDisposed>k__BackingField  // 0x48
PROPERTIES:
  isDisposed  get=0x04D867D0  set=0x04D86810
METHODS:
  RVA=0x03A82A90  token=0x60004C0  System.Void MarkEnteredPreloadThisSession()
  RVA=0x069A1788  token=0x60004C1  System.Void ClearEnteredPreloadThisSession()
  RVA=0x069A1A5C  token=0x60004C2  System.Boolean HasEnteredPreloadThisSession()
  RVA=0x04731570  token=0x60004C3  Beyond.Login.LoginWorkFlow.Node[] _RegisterNodes()
  RVA=0x03406190  token=0x60004C6  System.Void .ctor(Beyond.LoginContext context)
  RVA=0x041E4DE0  token=0x60004C7  System.Collections.IEnumerator DoWorkFlow()
  RVA=0x03406310  token=0x60004C8  System.Void Dispose()
  RVA=0x046DD390  token=0x60004C9  System.Boolean PeekFallbackStatus(Beyond.Login.LoginWorkFlow.ENode& fallbackNode)
  RVA=0x069A1800  token=0x60004CA  System.Boolean Fallback(Beyond.Login.LoginWorkFlow.ENode target, System.Boolean validCheck)
  RVA=0x069A16F4  token=0x60004CB  System.Boolean CancelAndFallback(Beyond.Login.LoginWorkFlow.ENode target, System.Boolean validCheck)
  RVA=0x0470AF20  token=0x60004CC  System.Void SendEvent(Beyond.ELoginEvent evt, System.Object param)
  RVA=0x069A1664  token=0x60004CD  System.Void AddEventReceiver(Beyond.Login.LoginWorkFlow.IEventReceiver receiver)
  RVA=0x0450CAF0  token=0x60004CE  System.Boolean UpdateOnce(Beyond.Login.LoginWorkFlow.IWorkerUpdateOnce inst)
  RVA=0x034077A0  token=0x60004CF  System.Boolean IsBeforeWork(Beyond.Login.LoginWorkFlow.ENode curNode, Beyond.Login.LoginWorkFlow.ENode check)
  RVA=0x034088B0  token=0x60004D0  System.Boolean IsOnOrAfterWork(Beyond.Login.LoginWorkFlow.ENode curNode, Beyond.Login.LoginWorkFlow.ENode check)
  RVA=0x069A1AD4  token=0x60004D1  System.Boolean IsAfterWork(Beyond.Login.LoginWorkFlow.ENode target, Beyond.Login.LoginWorkFlow.ENode check)
  RVA=0x0470B260  token=0x60004D2  System.Boolean _HandleGlobalEvent(Beyond.ELoginEvent evt, System.Object param)
  RVA=0x069A1E60  token=0x60004D3  System.Void _EventOnOpenAgeTips()
  RVA=0x069A1B98  token=0x60004D4  System.Void _EventOnCloseAgeTips()
  RVA=0x069A1FE0  token=0x60004D5  System.Void _EventOnOpenServerSelect(System.Boolean isOpen)
  RVA=0x069A20B0  token=0x60004D6  System.Void _EventOnSelectServer(Beyond.Gameplay.ServerZoneManager.ServerZoneData serverZoneData)
  RVA=0x069A1CC4  token=0x60004D7  System.Void _EventOnConfirmServer()
  RVA=0x04B2C680  token=0x60004D8  System.Void _EnableCursor(System.Boolean enabled)
  RVA=0x069A2238  token=0x60004D9  System.Void _UpdateCursor(Beyond.EventData<Beyond.DeviceInfo.InputType>& input)
  RVA=0x049D6BB0  token=0x60004DA  System.Void _EnbaleToggleGroup()
  RVA=0x069A2190  token=0x60004DB  System.Collections.IEnumerator _FetchAgeTipsCoroutine(Beyond.Login.LoginAgeTipsProp prop)
  RVA=0x03406D40  token=0x60004DC  System.Boolean _CheckIfGameStateNotMatch(Beyond.Login.LoginWorkFlow.ENode curNode)
  RVA=0x049B82F0  token=0x60004DD  System.Collections.IEnumerator _AdjustCurrentGameState(Beyond.Login.LoginWorkFlow.ENode curNode)
  RVA=0x04CE9E50  token=0x60004DE  System.Void .cctor()
END_CLASS

CLASS: Beyond.Login.HotUpdateNode
TYPE:  class
TOKEN: 0x20000EB
SIZE:  0x30
EXTENDS: Node
FIELDS:
  private           Beyond.Login.LoginHotUpdater    m_updater  // 0x20
  private           System.Int32                    m_cachedResRepairCode  // 0x28
PROPERTIES:
  type  get=0x049B18E0
METHODS:
  RVA=0x03A80FC0  token=0x6000523  UnityEngine.CustomYieldInstruction Work()
  RVA=0x046DD0A0  token=0x6000524  System.Void OnDispose()
  RVA=0x041E1670  token=0x6000525  System.Void .ctor()
  RVA=0x06997004  token=0x6000526  System.Void <>iFixBaseProxy_OnDispose()
END_CLASS

CLASS: Beyond.Login.WaitForSecondsNode
TYPE:  abstract class
TOKEN: 0x20000EC
SIZE:  0x28
EXTENDS: Node
FIELDS:
  private           System.Nullable<System.Int32>   m_timerId  // 0x20
METHODS:
  RVA=0x03A81490  token=0x6000527  UnityEngine.CustomYieldInstruction Work()
  RVA=0x036A6140  token=0x6000528  System.Boolean _IsTimeout()
  RVA=0x04D39810  token=0x6000529  System.Void _NotifyTimeout()
  RVA=0x046DD170  token=0x600052A  System.Void _DisposeTimer()
  RVA=-1  // abstract  token=0x600052B  System.Single WaitForSeconds(System.Boolean& unscaled)
  RVA=0x046DD100  token=0x600052C  System.Void OnDispose()
  RVA=0x041E1670  token=0x600052D  System.Void .ctor()
  RVA=0x06997004  token=0x600052E  System.Void <>iFixBaseProxy_OnDispose()
END_CLASS

CLASS: Beyond.Login.PreloadPostDelayNode
TYPE:  class
TOKEN: 0x20000ED
SIZE:  0x28
EXTENDS: Beyond.Login.WaitForSecondsNode
FIELDS:
PROPERTIES:
  type  get=0x049B1670
METHODS:
  RVA=0x04D79D50  token=0x6000530  System.Single WaitForSeconds(System.Boolean& unscaled)
  RVA=0x03A813E0  token=0x6000531  UnityEngine.CustomYieldInstruction Work()
  RVA=0x03A80C70  token=0x6000532  System.Void _DisableLoadingPanel()
  RVA=0x041E1670  token=0x6000533  System.Void .ctor()
  RVA=0x069A25EC  token=0x6000534  UnityEngine.CustomYieldInstruction <>iFixBaseProxy_Work()
END_CLASS

CLASS: Beyond.Login.Trans2GsLoginNode
TYPE:  class
TOKEN: 0x20000EE
SIZE:  0x28
EXTENDS: Beyond.Login.WaitForSecondsNode
FIELDS:
PROPERTIES:
  type  get=0x049B1640
METHODS:
  RVA=0x041E5CA0  token=0x6000536  System.Single WaitForSeconds(System.Boolean& unscaled)
  RVA=0x03A81420  token=0x6000537  UnityEngine.CustomYieldInstruction Work()
  RVA=0x041E1670  token=0x6000538  System.Void .ctor()
  RVA=0x069A25EC  token=0x6000539  UnityEngine.CustomYieldInstruction <>iFixBaseProxy_Work()
END_CLASS

CLASS: Beyond.Login.FinishNode
TYPE:  class
TOKEN: 0x20000EF
SIZE:  0x20
EXTENDS: Node
FIELDS:
PROPERTIES:
  type  get=0x049B1940
METHODS:
  RVA=0x04A31020  token=0x600053B  UnityEngine.CustomYieldInstruction Work()
  RVA=0x041E1670  token=0x600053C  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.CarrierDownloadCache
TYPE:  sealed struct
TOKEN: 0x20000F0
SIZE:  0x20
FIELDS:
  public    static readonly Beyond.Login.CarrierDownloadCacheEMPTY  // static @ 0x0
  public            System.Int64                    size  // 0x10
  public            System.Boolean                  isAllowed  // 0x18
METHODS:
  RVA=0x04D56A10  token=0x600053D  System.Void .cctor()
END_CLASS

CLASS: Beyond.Login.NetUsagePolicy
TYPE:  class
TOKEN: 0x20000F1
SIZE:  0x30
FIELDS:
  private   static  System.Int32                    NOT_REACHABLE_ERROR_CODE  // const
  private           Beyond.Login.CarrierDownloadCachem_mobileDataCache  // 0x10
  private           UnityEngine.NetworkReachability m_lastNetworkState  // 0x20
  private           Beyond.Login.NetUsagePolicy.Optionsm_options  // 0x28
METHODS:
  RVA=0x0380AB00  token=0x600053E  System.Void .ctor(Beyond.Login.NetUsagePolicy.Options options)
  RVA=0x069B43E4  token=0x600053F  System.Void CheckIfAllowDownload(System.Int64 downloadSize, System.Action onAllowed, System.Action onRejected, System.Action onReachable)
  RVA=0x069B438C  token=0x6000540  System.Boolean AllowMobileDataInLastCheck()
  RVA=0x069B4734  token=0x6000541  System.String _GetPrettySizeStringUpToMB(System.Double number)
END_CLASS

CLASS: Beyond.Login.LoginSceneAnimCtrl
TYPE:  class
TOKEN: 0x20000F4
SIZE:  0xD8
EXTENDS: Beyond.Login.LoginSceneComponent
IMPLEMENTS: Beyond.Login.ILoginBinder`1 UnityEngine.IAnimationClipSource
FIELDS:
  private   static  System.Single                   COUNT_DOWN_EMPTY  // const
  private           UnityEngine.Animator            _target  // 0x78
  private           UnityEngine.AnimationClip       _clipA1  // 0x80
  private           UnityEngine.AnimationClip       _clipA1s  // 0x88
  private           System.Single                   _transA1  // 0x90
  private           System.Single                   _transA1s  // 0x94
  private           UnityEngine.Vector2             _randomRangeA1s  // 0x98
  private           UnityEngine.AnimationClip       _clipA1A2  // 0xa0
  private           System.Single                   _predelayA1A2  // 0xa8
  private           System.Single                   _transA1A2  // 0xac
  private           UnityEngine.AnimationClip       _clipA2  // 0xb0
  private           System.Single                   _transA2  // 0xb8
  private           Beyond.UI.UIAnimationMixPlayer  m_player  // 0xc0
  private           Beyond.Login.LoginSceneAnimCtrl.EStatem_state  // 0xc8
  private           System.Single                   m_countDownA1s  // 0xcc
  private           System.Single                   m_countDownA1A2  // 0xd0
PROPERTIES:
  tickOption  get=0x047333E0
METHODS:
  RVA=0x041E5760  token=0x6000546  System.Void OnBindToManager()
  RVA=0x03D4FF30  token=0x6000547  Beyond.UI.UIAnimationMixPlayer _EnsurePlayer()
  RVA=0x03408830  token=0x6000548  System.Void _ChangeToState(Beyond.Login.LoginSceneAnimCtrl.EState target)
  RVA=0x03D4F930  token=0x6000549  System.Void _ResetToA1()
  RVA=0x049B8B60  token=0x600054A  System.Void _TransitionToA2()
  RVA=0x03D4FD90  token=0x600054B  System.Void _PlayA1sAndTriggerNext()
  RVA=0x03D50000  token=0x600054C  System.Single _RandomIntervalA1s()
  RVA=0x03D4FB60  token=0x600054D  System.Void _PlayA1A2Impl()
  RVA=0x03408790  token=0x600054E  System.Void OnValueChanged(Beyond.Login.LoginViewModel value)
  RVA=0x0340A1D0  token=0x600054F  System.Void Start()
  RVA=0x02F446B0  token=0x6000550  System.Void Tick(System.Single deltaTime)
  RVA=0x04994870  token=0x6000551  System.Void OnRelease()
  RVA=0x069B1654  token=0x6000552  System.Void GetAnimationClips(System.Collections.Generic.List<UnityEngine.AnimationClip> results)
  RVA=0x04B6CD40  token=0x6000553  System.Void .ctor()
  RVA=0x069B16B8  token=0x6000554  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x069B16A8  token=0x6000555  System.Void <>iFixBaseProxy_OnBindToManager()
  RVA=0x069B16B0  token=0x6000556  System.Void <>iFixBaseProxy_Start()
  RVA=0x01538B50  token=0x6000557  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x069AFD04  token=0x6000558  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.Login.LoginSceneComponent
TYPE:  abstract class
TOKEN: 0x20000F6
SIZE:  0x78
EXTENDS: Beyond.TickableMono
FIELDS:
  private           Beyond.LoginContext             <context>k__BackingField  // 0x68
  private           Beyond.LoginController          <controller>k__BackingField  // 0x70
PROPERTIES:
  tickOption  get=0x069B16C0
  context  get=0x04D86230  set=0x0485F170
  controller  get=0x04D862B0  set=0x063AD760
METHODS:
  RVA=0x0340A300  token=0x600055E  System.Void Start()
  RVA=0x041E60C0  token=0x600055F  System.Void Bind(Beyond.LoginContext context, Beyond.LoginController controller)
  RVA=0x041E5AA0  token=0x6000560  System.Void OnBindToManager()
  RVA=0x039C2B00  token=0x6000561  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000562  Beyond.TickType <>iFixBaseProxy_get_tickOption()
END_CLASS

CLASS: Beyond.Login.LoginSceneRoot
TYPE:  class
TOKEN: 0x20000F7
SIZE:  0x78
EXTENDS: Beyond.Login.LoginSceneComponent
FIELDS:
PROPERTIES:
  tickOption  get=0x04D51C40
METHODS:
  RVA=0x0380F010  token=0x6000564  System.Void OnBindToManager()
  RVA=0x039C2B00  token=0x6000565  System.Void .ctor()
  RVA=0x069B16B8  token=0x6000566  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x069B16A8  token=0x6000567  System.Void <>iFixBaseProxy_OnBindToManager()
END_CLASS

CLASS: Beyond.Login.LoginAgeTipsDetailPanel
TYPE:  class
TOKEN: 0x20000F8
SIZE:  0x68
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.Login.ILoginBinder`1
FIELDS:
  private           Beyond.UI.UIAnimationWrapper    _anim  // 0x18
  private           UnityEngine.RectTransform       _content  // 0x20
  private           Beyond.UI.UIText                _txtTitle  // 0x28
  private           Beyond.UI.UIText                _lineTmpl  // 0x30
  private           Beyond.UI.UIButton              _btnClose  // 0x38
  private           Beyond.UI.UIScrollRect          _scroll  // 0x40
  private           Beyond.Input.InputBindingGroupMonoTarget_ageTipsBindingGroup  // 0x48
  private           Beyond.LoginContext             m_context  // 0x50
  private           Beyond.UI.UISwitchTween         m_switchTween  // 0x58
  private           DG.Tweening.Tween               m_displayDelayTween  // 0x60
  private   static  System.Int32                    MAX_TEXT_STRLEN  // const
  private   static  System.Int32                    MIN_TEXT_STRLEN  // const
METHODS:
  RVA=0x042B6AA0  token=0x6000568  System.Void Init(Beyond.LoginContext context)
  RVA=0x04B48030  token=0x6000569  System.Void OnValueChanged(Beyond.Login.LoginAgeTipsModel value)
  RVA=0x069AF530  token=0x600056A  System.Void _RenderContent(System.String contentStr)
  RVA=0x069AF6A0  token=0x600056B  System.Void _ShowContent()
  RVA=0x069AF4B8  token=0x600056C  System.Void _EventOnCloseClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069AF6FC  token=0x600056D  System.Collections.Generic.List<System.String> _SplitLicenseToSegments(System.String content)
  RVA=0x069AF9E0  token=0x600056E  System.Void _SplitLongString(System.String longStr, System.Collections.Generic.List<System.String> outputList)
  RVA=0x05393520  token=0x600056F  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginAgeTipsPanel
TYPE:  class
TOKEN: 0x20000F9
SIZE:  0x48
EXTENDS: ChildPanel
IMPLEMENTS: Beyond.Login.ILoginBinder`1
FIELDS:
  private           Beyond.Login.LoginAgeTipsDetailPanel_detailPanel  // 0x28
  private           Beyond.UI.UIButton              _ageTipsCN  // 0x30
  private           UnityEngine.GameObject          _ageTipsKR  // 0x38
  private           UnityEngine.CanvasGroup         _canvasVersionTop  // 0x40
METHODS:
  RVA=0x042B61D0  token=0x6000570  System.Void OnInit()
  RVA=0x03408A70  token=0x6000571  System.Void OnValueChanged(Beyond.Login.LoginViewModel value)
  RVA=0x03409910  token=0x6000572  System.Void _UpdateAgeTipsPolicy(Beyond.Login.LoginViewModel.FAgeTipsPolicy policy)
  RVA=0x069AFAB4  token=0x6000573  System.Void _EventOnOpenAgeTipsClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x05393520  token=0x6000574  System.Void .ctor()
  RVA=0x069AFAAC  token=0x6000575  System.Void <>iFixBaseProxy_OnInit()
END_CLASS

CLASS: Beyond.Login.LoginAlertDialog
TYPE:  class
TOKEN: 0x20000FA
SIZE:  0x58
EXTENDS: Dialog
FIELDS:
  private           Beyond.UI.UIButton              _btnConfirm  // 0x38
  private           Beyond.UI.UIText                _text  // 0x40
  private           Beyond.Login.LoginAlertDialog.FParamsm_params  // 0x48
METHODS:
  RVA=0x069AFB2C  token=0x6000576  System.Void OnInit()
  RVA=0x069AFC10  token=0x6000577  System.Void SetParams(Beyond.Login.LoginAlertDialog.FParams param)
  RVA=0x069AFCA0  token=0x6000578  System.Void _OnConfirmClicked(UnityEngine.EventSystems.PointerEventData evt)
  RVA=0x05393520  token=0x6000579  System.Void .ctor()
  RVA=0x069AFC98  token=0x600057A  System.Void <>iFixBaseProxy_OnInit()
END_CLASS

CLASS: Beyond.Login.LoginDecorateUI
TYPE:  class
TOKEN: 0x20000FC
SIZE:  0xE0
EXTENDS: Beyond.TickableUIMono
IMPLEMENTS: Beyond.Login.ILoginBinder`1 Beyond.Login.ILoginContextReceiver
FIELDS:
  private           UnityEngine.CanvasGroup         _imgLogo  // 0x68
  private           System.Single                   _durationLogo  // 0x70
  private           UnityEngine.AnimationClip       _clipLoopGlow  // 0x78
  private           UnityEngine.AnimationClip       _clipInGlow  // 0x80
  private           UnityEngine.Animator            _targetGlow  // 0x88
  private           System.Single                   _delay  // 0x90
  private           Beyond.LoginContext             m_context  // 0x98
  private           System.Boolean                  m_isOnTransNode  // 0xa0
  private           Beyond.Interpolator.FLerpByTime m_hideLogoLerp  // 0xa8
  private           Beyond.UI.UIAnimationLayerMixPlayerm_glowPlayer  // 0xc8
  private           System.Boolean                  m_isGlowEnabled  // 0xd0
  private           System.Single                   m_countDownDelay  // 0xd4
  private           System.Single                   m_countDownDisableGlow  // 0xd8
PROPERTIES:
  tickOption  get=0x04A357C0
METHODS:
  RVA=0x041E4C00  token=0x600057C  System.Void OnLoginContext(Beyond.LoginContext context)
  RVA=0x03408BB0  token=0x600057D  System.Void OnValueChanged(Beyond.Login.LoginViewModel value)
  RVA=0x03D4F5A0  token=0x600057E  System.Void _TriggerTrans2GSLoginImpl()
  RVA=0x03409D20  token=0x600057F  System.Void _ToggleGlow(System.Boolean enable)
  RVA=0x03D4F720  token=0x6000580  Beyond.UI.UIAnimationLayerMixPlayer _EnsureGlowPlayer()
  RVA=0x03D4F360  token=0x6000581  System.Void Tick(System.Single deltaTime)
  RVA=0x049947F0  token=0x6000582  System.Void OnRelease()
  RVA=0x03D4F3D0  token=0x6000583  System.Void _TimeManagerTick(System.Single timeDelta)
  RVA=0x04CB1AE0  token=0x6000584  System.Void .ctor()
  RVA=0x069AFD0C  token=0x6000585  Beyond.TickType <>iFixBaseProxy_get_tickOption()
  RVA=0x01538B50  token=0x6000586  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x069AFD04  token=0x6000587  System.Void <>iFixBaseProxy_OnRelease()
END_CLASS

CLASS: Beyond.Login.LoginEnterGamePanel
TYPE:  class
TOKEN: 0x20000FD
SIZE:  0x58
EXTENDS: ChildPanel
IMPLEMENTS: Beyond.Login.ILoginBinder`1
FIELDS:
  private           UnityEngine.CanvasGroup         _canvasGroup  // 0x28
  private           Beyond.UI.UIButton              _btnEnterGame  // 0x30
  private           Beyond.UI.UIText                _textEnterGame  // 0x38
  private           System.Single                   _delayEnterGame  // 0x40
  private           System.Boolean                  m_isPanelActive  // 0x44
  private           System.Double                   m_panelActiveTime  // 0x48
  private           System.Boolean                  m_isInited  // 0x50
METHODS:
  RVA=0x04B6B370  token=0x6000588  System.Void _InitIfNot()
  RVA=0x03406E00  token=0x6000589  System.Void OnValueChanged(Beyond.Login.LoginViewModel value)
  RVA=0x0470AE70  token=0x600058A  System.Void _OnEnterGameClicked(UnityEngine.EventSystems.PointerEventData evt)
  RVA=0x04D11A70  token=0x600058B  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginGovTipsPanel
TYPE:  class
TOKEN: 0x20000FE
SIZE:  0x30
EXTENDS: ChildPanel
IMPLEMENTS: Beyond.Login.ILoginBinder`1
FIELDS:
  private           Beyond.UI.UIButton              _btnOpen  // 0x28
METHODS:
  RVA=0x042B6100  token=0x600058C  System.Void OnInit()
  RVA=0x03408960  token=0x600058D  System.Void OnValueChanged(Beyond.Login.LoginViewModel value)
  RVA=0x069AFD14  token=0x600058E  System.Void _EventOnOpenAgeTipsClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x05393520  token=0x600058F  System.Void .ctor()
  RVA=0x069AFAAC  token=0x6000590  System.Void <>iFixBaseProxy_OnInit()
END_CLASS

CLASS: Beyond.Login.LoginJudgeDialog
TYPE:  class
TOKEN: 0x20000FF
SIZE:  0x98
EXTENDS: Dialog
FIELDS:
  private           Beyond.UI.UIButton              _btnPositive  // 0x38
  private           Beyond.UI.UIButton              _btnNegative  // 0x40
  private           Beyond.UI.UIText                _text  // 0x48
  private           Beyond.UI.UIText                _text2  // 0x50
  private           Beyond.UI.UIText                _textPositive  // 0x58
  private           Beyond.UI.UIText                _textNegative  // 0x60
  private           Beyond.Login.LoginJudgeDialog.FParamsm_params  // 0x68
METHODS:
  RVA=0x069AFD7C  token=0x6000591  System.Void OnInit()
  RVA=0x069AFEC8  token=0x6000592  System.Void SetParams(Beyond.Login.LoginJudgeDialog.FParams param)
  RVA=0x069B0170  token=0x6000593  System.Void _OnPositiveClicked(UnityEngine.EventSystems.PointerEventData evt)
  RVA=0x069B010C  token=0x6000594  System.Void _OnNegativeClicked(UnityEngine.EventSystems.PointerEventData evt)
  RVA=0x05393520  token=0x6000595  System.Void .ctor()
  RVA=0x069AFC98  token=0x6000596  System.Void <>iFixBaseProxy_OnInit()
END_CLASS

CLASS: Beyond.Login.LoginMenuPanel
TYPE:  class
TOKEN: 0x2000102
SIZE:  0xF0
EXTENDS: ChildPanel
IMPLEMENTS: Beyond.Login.ILoginBinder`1
FIELDS:
  public    static  System.String                   CONTROLLER_STATE_NORMAL  // const
  public    static  System.String                   CONTROLLER_STATE_SELECT  // const
  private           Beyond.UI.UIButton              _btnAnnounce  // 0x28
  private           Beyond.UI.UIButton              _btnSetting  // 0x30
  private           Beyond.UI.UIButton              _btnResCheck  // 0x38
  private           Beyond.UI.UIButton              _btnLogout  // 0x40
  private           Beyond.UI.UIButton              _btnQuitGame  // 0x48
  private           Beyond.UI.UIButton              _btnLogin  // 0x50
  private           Beyond.UI.UIButton              _btnQR  // 0x58
  private           Beyond.UI.UIState.UIStateController_ctrlAnnounce  // 0x60
  private           Beyond.UI.UIState.UIStateController_ctrlSetting  // 0x68
  private           Beyond.UI.UIState.UIStateController_ctrlResCheck  // 0x70
  private           Beyond.UI.UIState.UIStateController_ctrlLogout  // 0x78
  private           Beyond.UI.UIState.UIStateController_ctrlQR  // 0x80
  private           Beyond.UI.UISimpleStateController_ctrlRight  // 0x88
  private           Beyond.UI.UISelectableNaviGroup _naviGroupRight  // 0x90
  private           Beyond.Input.InputBindingGroupMonoTarget_bindingGroupForKeyboard  // 0x98
  private           Beyond.Input.InputBindingGroupMonoTarget_rootPanelBindingGroup  // 0xa0
  private           UnityEngine.CanvasGroup         _alphaHandler  // 0xa8
  private           UnityEngine.GameObject          _redDotAnnounce  // 0xb0
  private           Beyond.UI.UIAnimationWrapper    _transAnim  // 0xb8
  private           Beyond.UI.UIButton              _serverZoneBtn  // 0xc0
  private           Beyond.UI.UIText                _serverZoneName  // 0xc8
  private           UnityEngine.GameObject          _serverZonePS5  // 0xd0
  private           Beyond.UI.UIText                _serverZoneNamePS5  // 0xd8
  private           Beyond.UI.UIState.UIStateControllerm_curRightBtnCtrl  // 0xe0
  private           System.Boolean                  m_hasRedDot  // 0xe8
  private           System.Int32                    m_rootParentGroupId  // 0xec
METHODS:
  RVA=0x039D2890  token=0x6000597  System.Void OnInit()
  RVA=0x03407440  token=0x6000598  System.Void OnValueChanged(Beyond.Login.LoginViewModel value)
  RVA=0x041E7B00  token=0x6000599  System.Void InitBulletinAndRedAfterLoginSuc()
  RVA=0x069B08EC  token=0x600059A  System.Void _OnBulletinOpen()
  RVA=0x069B0854  token=0x600059B  System.Void _OnBulletinClose()
  RVA=0x04CAA620  token=0x600059C  System.Void Awake()
  RVA=0x040101A0  token=0x600059D  System.Void OnEnable()
  RVA=0x040103A0  token=0x600059E  System.Void OnDisable()
  RVA=0x048AB5F0  token=0x600059F  System.Void OnDestroy()
  RVA=0x069B0604  token=0x60005A0  System.Void _OnAnnounceClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B1168  token=0x60005A1  System.Void _OnSettingClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B1034  token=0x60005A2  System.Void _OnResCheckClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B096C  token=0x60005A3  System.Void _OnCustomServiceClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B053C  token=0x60005A4  System.Void _OnAccountCenterClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B0CB0  token=0x60005A5  System.Void _OnLogoutClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B0E28  token=0x60005A6  System.Void _OnQuitGameClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B0C38  token=0x60005A7  System.Void _OnLoginClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B0D78  token=0x60005A8  System.Void _OnQRClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B0FC8  token=0x60005A9  System.Void _OnRequestRelogin()
  RVA=0x04D576D0  token=0x60005AA  System.Boolean _CanQRLogin()
  RVA=0x041E9090  token=0x60005AB  System.Void _UpdateAnnounceRedDot()
  RVA=0x03A85290  token=0x60005AC  System.Void _UpdateServerZone()
  RVA=0x069B10FC  token=0x60005AD  System.Void _OnServerZoneBtnClicked()
  RVA=0x069B0A1C  token=0x60005AE  System.Void _OnFocusMenuGroup(System.Boolean isFocused)
  RVA=0x069B07B4  token=0x60005AF  System.Void _OnAnnounceClosed()
  RVA=0x041E99E0  token=0x60005B0  System.Void _OnReceiveRedDotAndPopup(System.Boolean showRedDot)
  RVA=0x069B1254  token=0x60005B1  System.Void _OnUpdateRedDotForSDKListener(Beyond.EventData<System.Boolean>& show)
  RVA=0x069B0B0C  token=0x60005B2  System.Void _OnInputChanged(Beyond.EventData<Beyond.DeviceInfo.InputType>& input)
  RVA=0x04D11A90  token=0x60005B3  System.Void .ctor()
  RVA=0x069B01D4  token=0x60005B4  System.Void <OnInit>b__28_0(System.Boolean isTarget, System.Boolean isGroupChanged, System.Boolean isOnNaviTargetEnabledAgain)
  RVA=0x069B0260  token=0x60005B5  System.Void <OnInit>b__28_1(System.Boolean isTarget, System.Boolean isGroupChanged, System.Boolean isOnNaviTargetEnabledAgain)
  RVA=0x069B02EC  token=0x60005B6  System.Void <OnInit>b__28_2(System.Boolean isTarget, System.Boolean isGroupChanged, System.Boolean isOnNaviTargetEnabledAgain)
  RVA=0x069B0378  token=0x60005B7  System.Void <OnInit>b__28_3(System.Boolean isTarget, System.Boolean isGroupChanged, System.Boolean isOnNaviTargetEnabledAgain)
  RVA=0x069B0404  token=0x60005B8  System.Void <OnInit>b__28_4(System.Boolean isTarget, System.Boolean isGroupChanged, System.Boolean isOnNaviTargetEnabledAgain)
  RVA=0x069B0498  token=0x60005B9  System.Void <OnInit>b__28_5(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B04A0  token=0x60005BA  System.Void <OnInit>b__28_6(System.Boolean isFocused)
  RVA=0x069B04B0  token=0x60005BB  System.Void <_OnAnnounceClicked>b__37_0(System.String s)
  RVA=0x069AFAAC  token=0x60005BC  System.Void <>iFixBaseProxy_OnInit()
  RVA=0x069B04A8  token=0x60005BD  System.Void <>iFixBaseProxy_OnDestroy()
END_CLASS

CLASS: Beyond.Login.LoginMockUIDPanel
TYPE:  class
TOKEN: 0x2000104
SIZE:  0x98
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.CanvasGroup         _canvas  // 0x18
  private           Beyond.UI.UIInputField          _userIdInputField  // 0x20
  private           Beyond.UI.UIButton              _btnEnterGame  // 0x28
  private           Beyond.UI.UIButton              _btnDelLocalFile  // 0x30
  private           Beyond.UI.UIDropdown            _serverDropdown  // 0x38
  private           UnityEngine.GameObject          _hintSelectServer  // 0x40
  private           Beyond.UI.UIButton              _btnClose  // 0x48
  private           Beyond.UI.UIText                _selectedServerLabel  // 0x50
  private           System.Boolean                  m_isInited  // 0x58
  private           Beyond.UI.UISwitchTween         m_switchTween  // 0x60
  private           System.Collections.Generic.List<Beyond.Network.ServerDesc>m_serverDescList  // 0x68
  private           Beyond.Network.ServerList       m_serverListObj  // 0x70
  private           Beyond.Network.ServerConfig     m_serverConfig  // 0x78
  private           System.Action<System.String>    m_callback  // 0x80
  private           Beyond.Login.LoginMockUIDPanel.FOptionsm_options  // 0x88
  private   static  System.String                   LAST_SELECTED_SERVER_PREF_KEY  // const
  private   static  Beyond.Login.LoginMockUIDPanel  s_instance  // static @ 0x0
PROPERTIES:
  instance  get=0x069B14B0
METHODS:
  RVA=0x03F0EF20  token=0x60005C2  System.Void LoginWithCallback(Beyond.Login.LoginMockUIDPanel.FOptions options, System.Action<System.String> callback)
  RVA=0x069B12B8  token=0x60005C3  System.Void Refresh()
  RVA=0x03F0E5A0  token=0x60005C4  System.Collections.IEnumerator HideAndDestroy()
  RVA=0x03F0FEA0  token=0x60005C5  System.Void _UpdateStatus()
  RVA=0x03DA3B90  token=0x60005C6  System.Void _InitLoginInfo()
  RVA=0x03F4A940  token=0x60005C7  System.Void _MovePreferredServerToFront(System.Int32& curServerIndex)
  RVA=0x03DA4A40  token=0x60005C8  System.Int32 _IndexOfServer(System.String serverName)
  RVA=0x03F4B630  token=0x60005C9  System.Void _SaveLastSelectedServer(System.String serverName)
  RVA=0x03DA4240  token=0x60005CA  System.Void _SaveConfig()
  RVA=0x04CE84A0  token=0x60005CB  System.Void _OnBtnEnterGame()
  RVA=0x069B135C  token=0x60005CC  System.Void _SmokeEnterGame(System.String serverName, System.String uid)
  RVA=0x069B1318  token=0x60005CD  System.Void _OnBtnDelLocalFile()
  RVA=0x069B1414  token=0x60005CE  System.Void _TryClosePanel()
  RVA=0x03F4AB20  token=0x60005CF  System.Void _OnSelectServer(System.Int32 index)
  RVA=0x04D51270  token=0x60005D0  System.Void OnDestroy()
  RVA=0x05393520  token=0x60005D1  System.Void .ctor()
  RVA=0x04CE8490  token=0x60005D2  System.Void <_UpdateStatus>b__23_0(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B1308  token=0x60005D3  System.Void <_UpdateStatus>b__23_1(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B1310  token=0x60005D4  System.Void <_UpdateStatus>b__23_2(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x03F4ABC0  token=0x60005D5  System.Void <_InitLoginInfo>b__24_0(System.Int32 index, Beyond.UI.UIDropdownOption options, System.Boolean isSelected)
END_CLASS

CLASS: Beyond.Login.LoginResourcePanel
TYPE:  class
TOKEN: 0x2000107
SIZE:  0x88
EXTENDS: ChildPanel
IMPLEMENTS: Beyond.Login.ILoginBinder`1
FIELDS:
  private   static  System.Single                   PROGRESS_LERP_DURATION  // const
  private           UnityEngine.CanvasGroup         _alphaHandler  // 0x28
  private           Beyond.UI.UISlider              _progressBar  // 0x30
  private           Beyond.UI.UIText                _textTitle  // 0x38
  private           Beyond.UI.UIText                _textInfo  // 0x40
  private           Beyond.Login.LoginHotUpdateModel.IProgressm_progress  // 0x48
  private           Beyond.UI.TweenUtils.FLerpByTimem_progressLerper  // 0x50
  private           System.String                   m_resInfoCache  // 0x80
METHODS:
  RVA=0x049C89F0  token=0x60005DD  System.Void OnValueChanged(Beyond.Login.LoginHotUpdateModel value)
  RVA=0x039367F0  token=0x60005DE  System.Void _UpdateResourceInfo()
  RVA=0x03935650  token=0x60005DF  System.Void Update()
  RVA=0x05393520  token=0x60005E0  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginRootPanel
TYPE:  class
TOKEN: 0x2000108
SIZE:  0x60
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.NotchAdapter          _notchAdapter  // 0x18
  private           Beyond.Login.LoginMenuPanel     _menuPanel  // 0x20
  private           Beyond.Login.LoginAgeTipsPanel  _ageTipsPanel  // 0x28
  private           Beyond.Login.LoginGovTipsPanel  _govTipsPanel  // 0x30
  private           Beyond.Login.LoginResourcePanel _resPanel  // 0x38
  private           Beyond.Login.LoginEnterGamePanel_enterGamePanel  // 0x40
  private           Beyond.Login.LoginServerSelectPanel_serverSelectPanel  // 0x48
  private           Beyond.Input.InputBindingGroupMonoTarget_bindingGroup  // 0x50
  private           System.Int32                    m_loginRootBindingGroupId  // 0x58
METHODS:
  RVA=0x03CD3860  token=0x60005E1  System.Void Awake()
  RVA=0x03CD3960  token=0x60005E2  System.Void OnDestroy()
  RVA=0x041E57E0  token=0x60005E3  System.Void Init(Beyond.LoginContext context)
  RVA=0x069B15B0  token=0x60005E4  System.Void _OnNotchPaddingChanged(Beyond.EventData<System.Single>& data)
  RVA=0x041E4B40  token=0x60005E5  System.Void _InitController()
  RVA=0x069B151C  token=0x60005E6  System.Void ToggleLoginBindingGroup(System.Boolean enable)
  RVA=0x041E7A80  token=0x60005E7  System.Void InitBulletinAndRedAfterLoginSuc()
  RVA=0x041E58D0  token=0x60005E8  System.Void _InitChildPanel(Beyond.Login.LoginRootPanel.ChildPanel childPanel, Beyond.LoginContext context)
  RVA=0x05393520  token=0x60005E9  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginServerSelectItemView
TYPE:  class
TOKEN: 0x200010A
SIZE:  0x68
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UIText                _serverName  // 0x18
  private           Beyond.UI.UIImage               _playerIcon  // 0x20
  private           Beyond.UI.UIText                _playerLevel  // 0x28
  private           Beyond.UI.UIImage[]             _delayImages  // 0x30
  private           Beyond.UI.UIText                _delayText  // 0x38
  private           Beyond.UI.UIButton              _btnSelect  // 0x40
  private           Beyond.UI.UIState.UIStateController_stateController  // 0x48
  private   static readonly UnityEngine.Color               DEFAULT_COLOR  // static @ 0x0
  private   static  System.String                   DEFAULT_TEXT  // const
  private   static readonly Beyond.Login.LoginServerSelectItemView.DelayInfoDEFAULT_SPEED_INFO  // static @ 0x10
  private           Beyond.Gameplay.ServerZoneManager.ServerZoneDatam_serverItem  // 0x50
  private           Beyond.LoginContext             m_context  // 0x58
  private           System.Collections.Generic.List<Beyond.Login.LoginServerSelectItemView.DelayInfo>m_speedInfos  // 0x60
METHODS:
  RVA=0x069B179C  token=0x60005F4  System.Void RefreshView(Beyond.LoginContext context, Beyond.Gameplay.ServerZoneManager.ServerZoneData serverItem, System.Boolean isSelected)
  RVA=0x069B170C  token=0x60005F5  System.Void NaviToBtnTarget()
  RVA=0x069B1CC4  token=0x60005F6  System.Void SetSelected(System.Boolean isSelected)
  RVA=0x069B1D68  token=0x60005F7  System.Void _OnClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x046093D0  token=0x60005F8  System.Void .ctor()
  RVA=0x04CB5D40  token=0x60005F9  System.Void .cctor()
END_CLASS

CLASS: Beyond.Login.LoginServerSelectProp
TYPE:  class
TOKEN: 0x200010C
SIZE:  0x30
EXTENDS: Beyond.Login.LoginProperty`1
FIELDS:
METHODS:
  RVA=0x03A7F900  token=0x60005FA  System.Void .ctor(Beyond.Login.LoginWorkFlow workflow)
END_CLASS

CLASS: Beyond.Login.LoginServerItem
TYPE:  class
TOKEN: 0x200010D
SIZE:  0x18
FIELDS:
  public            Beyond.Gameplay.ServerZoneManager.ServerZoneDataserverZoneData  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60005FB  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginServerSelectModel
TYPE:  class
TOKEN: 0x200010E
SIZE:  0x18
FIELDS:
  public            System.Boolean                  showPanel  // 0x10
  public            System.Int32                    selectedIndex  // 0x14
PROPERTIES:
  serverList  get=0x069B2050
  curServerZoneData  get=0x069B1FBC
METHODS:
  RVA=0x069B1E64  token=0x60005FE  System.Void InitData()
  RVA=0x069B1DE0  token=0x60005FF  Beyond.Gameplay.ServerZoneManager.ServerZoneData GetSelectedServer()
  RVA=0x069B1ECC  token=0x6000600  System.Boolean UpdateSelectedServer(Beyond.Gameplay.ServerZoneManager.ServerZoneData selectedServer)
  RVA=0x04D8CCE0  token=0x6000601  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginServerSelectPanel
TYPE:  class
TOKEN: 0x200010F
SIZE:  0x68
EXTENDS: ChildPanel
IMPLEMENTS: Beyond.Login.ILoginBinder`1
FIELDS:
  private           Beyond.UI.UIButton              _btnBack  // 0x28
  private           Beyond.UI.UIButton              _btnConfirm  // 0x30
  private           Beyond.UI.UIAnimationWrapper    _showAnim  // 0x38
  private           Beyond.UI.UIScrollList          _showList  // 0x40
  private           Beyond.UI.UISelectableNaviGroup _naviGroup  // 0x48
  private           Beyond.Input.InputBindingGroupMonoTarget_bindingGroup  // 0x50
  private           Beyond.Input.InputBindingGroupMonoTarget_rootPanelBindingGroup  // 0x58
  private           Beyond.Login.LoginServerSelectModelm_viewModel  // 0x60
METHODS:
  RVA=0x042B4AF0  token=0x6000602  System.Void OnInit()
  RVA=0x069B20E4  token=0x6000603  System.Void OnDisable()
  RVA=0x069B21E8  token=0x6000604  System.Void OnEnable()
  RVA=0x069B2EB4  token=0x6000605  System.Collections.IEnumerator _WaitingForShowListAwake()
  RVA=0x069B2CE8  token=0x6000606  System.Void _UpdateServerCell(UnityEngine.GameObject cell, System.Int32 index)
  RVA=0x069B2DF4  token=0x6000607  System.Void _UpdateServerSelected(System.Int32 index, UnityEngine.GameObject cell)
  RVA=0x069B28F4  token=0x6000608  System.Void OnValueChanged(Beyond.Login.LoginServerSelectModel value)
  RVA=0x069B2A4C  token=0x6000609  System.Void _NaviToSelectedItem()
  RVA=0x069B2C84  token=0x600060A  System.Void _OnInputChanged(Beyond.EventData<Beyond.DeviceInfo.InputType>& input)
  RVA=0x069B2B94  token=0x600060B  System.Void _OnCloseClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x069B2C0C  token=0x600060C  System.Void _OnConfirmClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x05393520  token=0x600060D  System.Void .ctor()
  RVA=0x069AFAAC  token=0x600060E  System.Void <>iFixBaseProxy_OnInit()
END_CLASS

CLASS: Beyond.Login.LoginVersionPanel
TYPE:  class
TOKEN: 0x2000112
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UIText                _textVersion  // 0x18
PROPERTIES:
  displayGameVersion  get=0x0368CED0
  versionZoneType  get=0x0368CD50
  versionPlatform  get=0x0368CE30
  versionAppType  get=0x0368CE80
  versionVersionNum  get=0x038118B0
  versionProjectCL  get=0x0368CDC0
METHODS:
  RVA=0x0368CC80  token=0x6000618  System.Void Start()
  RVA=0x05393520  token=0x600061F  System.Void .ctor()
END_CLASS

CLASS: Beyond.Login.LoginVocResSelDialog
TYPE:  class
TOKEN: 0x2000113
SIZE:  0x70
EXTENDS: Dialog
FIELDS:
  private           Beyond.Login.LoginVocResSelDialog.FParamsm_params  // 0x38
  private           Beyond.UI.UIButton              _btnConfirm  // 0x40
  private           Beyond.UI.UIScrollList          _showList  // 0x48
  private           Beyond.UI.UISelectableNaviGroup _naviGroup  // 0x50
  private           Beyond.Input.InputBindingGroupMonoTarget_bindingGroup  // 0x58
  private           System.Collections.Generic.List<Beyond.Login.LoginVocResSelDialog.VocResInfo>m_vocResInfos  // 0x60
  private           System.Int32                    m_selectedIndex  // 0x68
METHODS:
  RVA=0x069B3150  token=0x6000620  System.Void OnInit()
  RVA=0x069B3AC8  token=0x6000621  System.Int32 _ResolveInitialSelectedIndex()
  RVA=0x069B3A64  token=0x6000622  System.Void _RefreshConfirmInteractable()
  RVA=0x069B3048  token=0x6000623  System.Void OnEnable()
  RVA=0x069B2F44  token=0x6000624  System.Void OnDisable()
  RVA=0x069B34E4  token=0x6000625  System.Void SetParams(Beyond.Login.LoginVocResSelDialog.FParams param)
  RVA=0x069B3548  token=0x6000626  System.Void UpdateSelected(Beyond.VFS.EVFSBlockType type)
  RVA=0x069B3CEC  token=0x6000627  System.Void _UpdateList()
  RVA=0x069B3DAC  token=0x6000628  System.Void _UpdateSelected(System.Int32 index, UnityEngine.GameObject cell)
  RVA=0x069B3C0C  token=0x6000629  System.Void _UpdateCell(UnityEngine.GameObject cell, System.Int32 index)
  RVA=0x069B394C  token=0x600062A  System.Void _OnConfirmClicked(UnityEngine.EventSystems.PointerEventData evt)
  RVA=0x069B3788  token=0x600062B  System.Void _OnConfirmClicked()
  RVA=0x069B3A00  token=0x600062C  System.Void _OnInputTypeChanged(Beyond.EventData<Beyond.DeviceInfo.InputType>& data)
  RVA=0x069B3644  token=0x600062D  System.Void _NaviToSelectedItem()
  RVA=0x05393520  token=0x600062E  System.Void .ctor()
  RVA=0x069AFC98  token=0x600062F  System.Void <>iFixBaseProxy_OnInit()
END_CLASS

CLASS: Beyond.Login.LoginVocResSelItemView
TYPE:  class
TOKEN: 0x2000116
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           Beyond.UI.UIText                _name  // 0x18
  private           Beyond.UI.UIText                _size  // 0x20
  private           Beyond.UI.UIButton              _btnSelect  // 0x28
  private           Beyond.UI.UIState.UIStateController_stateController  // 0x30
  private           Beyond.Login.LoginVocResSelDialog.VocResInfom_info  // 0x38
  private           Beyond.Login.LoginVocResSelDialogm_context  // 0x40
  private   static  System.Double                   MB  // const
METHODS:
  RVA=0x069B3EEC  token=0x6000631  System.Void RefreshView(Beyond.Login.LoginVocResSelDialog context, Beyond.Login.LoginVocResSelDialog.VocResInfo info, System.Boolean isSelected)
  RVA=0x069B3E5C  token=0x6000632  System.Void NaviToBtnTarget()
  RVA=0x069B4274  token=0x6000633  System.Void SetSelected(System.Boolean isSelected)
  RVA=0x069B4318  token=0x6000634  System.Void _OnClicked(UnityEngine.EventSystems.PointerEventData _)
  RVA=0x05393520  token=0x6000635  System.Void .ctor()
END_CLASS

CLASS: Beyond.SDK.PlatformFont
TYPE:  class
TOKEN: 0x2000059
SIZE:  0x18
IMPLEMENTS: Hypergryph.PlatformFacade.IPlatformFont
FIELDS:
  private   static  System.Int32                    DEFAULT_FONT_ID  // const
  private   readonly System.Boolean                  m_forceChineseFont  // 0x10
METHODS:
  RVA=0x04D86130  token=0x60001E1  System.Void .ctor(System.Boolean forceChineseFont)
  RVA=0x0699B6E4  token=0x60001E2  TMPro.TMP_FontAsset GetFont()
END_CLASS

CLASS: Beyond.SDK.SDKNetUtils
TYPE:  static class
TOKEN: 0x200005A
SIZE:  0x10
FIELDS:
  public    static  System.String                   CONTENT_TYPE_JSON  // const
  private   static  System.Int32                    GENERAL_TIMEOUT  // const
  private   static  Beyond.SDK.SDKNetUtils.HTTPImpl s_HTTPImpl  // static @ 0x0
METHODS:
  RVA=0x0699B8D4  token=0x60001E3  System.Void HTTPPost(Beyond.SDK.SDKNetUtils.HttpPostRequest request)
  RVA=0x0699B77C  token=0x60001E4  System.Void HTTPGet(Beyond.SDK.SDKNetUtils.HttpGetRequest request)
  RVA=0x0699BAC0  token=0x60001E5  System.Void .cctor()
END_CLASS

CLASS: Beyond.SDK.SDKTextDefines
TYPE:  static class
TOKEN: 0x2000063
SIZE:  0x10
FIELDS:
  public    static  System.String                   CS_SDK_STO_DESC  // const
  public    static  System.String                   CS_SDK_COMMON_ERROR  // const
  public    static  System.String                   CS_SDK_CREATE_ORDER_ERROR  // const
  public    static  System.String                   CS_SDK_CREATE_ORDER_ERROR_LIMIT_COUNT  // const
  public    static  System.String                   CS_SDK_USR_BAN  // const
  public    static  System.String                   CS_SDK_USR_BAN_TIME  // const
  public    static  System.String                   CS_SDK_LOGIN_FORBID  // const
  public    static  System.String                   CS_SDK_CREATE_ORDER_ERROR_NETWORK_TIMEOUT  // const
  public    static  System.String                   CS_SDK_CREATE_ORDER_ERROR_NETWORK_ERROR  // const
  public    static  System.String                   CS_SDK_CREATE_ORDER_ERROR_TOKEN_INVALID  // const
METHODS:
END_CLASS

CLASS: Beyond.SDK.Impl.U8ExternalTools
TYPE:  class
TOKEN: 0x2000064
SIZE:  0x28
EXTENDS: U8.SDK.SDKExternalToolsShortcut
FIELDS:
  private   static  System.Boolean                  s_inited  // static @ 0x0
  private   static  System.Boolean                  s_initing  // static @ 0x1
  private   static  System.Boolean                  s_needRetry  // static @ 0x2
  private   static  System.Boolean                  s_needQuitGame  // static @ 0x3
  private   static  Beyond.SDK.Impl.U8ExternalTools.FHGSDKInits_initHGSDK  // static @ 0x4
METHODS:
  RVA=0x0699EFE4  token=0x60001F0  System.Boolean IsInited()
  RVA=0x03C90D50  token=0x60001F1  System.Boolean NeedRetryInit()
  RVA=0x04A427C0  token=0x60001F2  System.Boolean NeedQuitGame()
  RVA=0x0699F3C4  token=0x60001F3  System.Void RetryInitSDKSystem(System.String env)
  RVA=0x04D225E0  token=0x60001F4  System.Void PublicSetGameVersion(System.String version)
  RVA=0x03C8F0A0  token=0x60001F5  System.Void InitSDKSystem(System.String env)
  RVA=0x04A42760  token=0x60001F6  System.Collections.IEnumerator WaitForInited()
  RVA=0x03AF7760  token=0x60001F7  System.Void InitHGSDK(System.String env)
  RVA=0x0699EF78  token=0x60001F8  System.Boolean IsHGSDKInited()
  RVA=0x0332A350  token=0x60001F9  System.Collections.IEnumerator WaitForInitHGSDK()
  RVA=0x0699F130  token=0x60001FA  System.Void Log(System.String content)
  RVA=0x0699F050  token=0x60001FB  System.Void LogError(System.String content)
  RVA=0x0699F0DC  token=0x60001FC  System.Void LogWarning(System.String content)
  RVA=0x0699EF0C  token=0x60001FD  System.String GetSignKey()
  RVA=0x0699EBA0  token=0x60001FE  System.Collections.Generic.Dictionary<System.String,System.String> GetDeviceIDs()
  RVA=0x0699F464  token=0x60001FF  System.Void SetSubChannel(System.String subChannel)
  RVA=0x0699EE94  token=0x6000200  System.Int32 GetPlatformKey()
  RVA=0x044419B0  token=0x6000201  System.Void OnSDKExtraInfo(System.String jsonData)
  RVA=0x0699F4D0  token=0x6000202  System.String U8RootUrl()
  RVA=0x0699F184  token=0x6000203  System.Void POSTImplementation(U8.SDK.SDKExternalTools.POSTRequest request, System.Action<U8.SDK.SDKExternalTools.POSTResult> callback)
  RVA=0x0699F560  token=0x6000204  U8.SDK.SDKExternalTools.POSTResult _ConvertToPOSTResult(Beyond.SDK.SDKNetUtils.WebHttpResponse response)
  RVA=0x04441BC0  token=0x6000205  System.Void _ProcessExtraInfo(U8.SDK.SDKExtraData extraData)
  RVA=0x014FF310  token=0x6000206  System.Void _ProcessGameProtocolUpdateCheck(System.Collections.Generic.Dictionary<System.String,System.Object> msg)
  RVA=0x01500F80  token=0x6000207  System.Int64 _TryGetGameProtocolUpdateCheckErrorCode(System.Collections.Generic.Dictionary<System.String,System.Object> msg)
  RVA=0x0699FA4C  token=0x6000208  System.Void _OnRequestExitGame()
  RVA=0x0699FA0C  token=0x6000209  System.Void _OnFakeAccountCenter()
  RVA=0x0699EB24  token=0x600020A  U8.SDK.SDKCaptchaHandler CreateCaptchaHandler()
  RVA=0x0699ECD4  token=0x600020B  System.String GetErrorMessage(U8.SDK.SDKExternalTools.ErrMsgMeta meta)
  RVA=0x0699F804  token=0x600020C  System.Boolean _ErrorMsg_GET_TOKEN(U8.SDK.SDKExternalTools.ErrMsgMeta meta, System.String& errorMsg)
  RVA=0x0699F664  token=0x600020D  System.Boolean _ErrorMsg_CREATE_ORDER(U8.SDK.SDKExternalTools.ErrMsgMeta meta, System.String& errorMsg)
  RVA=0x04D778D0  token=0x600020E  System.Void .ctor()
  RVA=0x011EC580  token=0x600020F  System.String <>iFixBaseProxy_GetSignKey()
  RVA=0x0699F4C8  token=0x6000210  System.Collections.Generic.Dictionary<System.String,System.String> <>iFixBaseProxy_GetDeviceIDs()
  RVA=0x041E1670  token=0x6000211  System.Void <>iFixBaseProxy_OnSDKExtraInfo(System.String P0)
END_CLASS

CLASS: Beyond.SDK.Impl.U8Plugin
TYPE:  class
TOKEN: 0x200006C
SIZE:  0x10
IMPLEMENTS: U8.SDK.IExternalPlugin
FIELDS:
METHODS:
  RVA=0x0699FAE8  token=0x600022E  System.String GetPayAddition()
  RVA=0x0699FB34  token=0x600022F  System.String GetSDKToken()
  RVA=0x0699FB80  token=0x6000230  System.String GetSDKUid()
  RVA=0x035A20D0  token=0x6000231  System.Void Init()
  RVA=0x03AF75B0  token=0x6000232  System.Void Login(U8.SDK.ExternalPluginLoginParams args)
  RVA=0x0699FBCC  token=0x6000233  System.Void Logout(U8.SDK.ExternalPluginLogoutParams args)
  RVA=0x0699FC54  token=0x6000234  System.Void OnProductListChanged(System.Collections.Generic.List<U8.SDK.U8ProductInfo> productList)
  RVA=0x0699FCA8  token=0x6000235  System.Void Pay(U8.SDK.ExternalPluginPayParams args)
  RVA=0x0699FE8C  token=0x6000236  System.Boolean TryLoadSDKMeta(System.Func<U8.SDK.SDKMeta> loadMetaNative, U8.SDK.SDKMeta& meta)
  RVA=0x03AF7680  token=0x6000237  System.Boolean _UseMockVersion()
  RVA=0x03AF90A0  token=0x6000238  System.Void _MockLogin(U8.SDK.ExternalPluginLoginParams args)
  RVA=0x041E1670  token=0x6000239  System.Void .ctor()
END_CLASS

CLASS: Beyond.Scripts.Entry.GameSettingSetter
TYPE:  static class
TOKEN: 0x2000117
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0366F370  token=0x6000636  System.Void RegisterSettingCallbacks()
  RVA=0x043F3130  token=0x6000637  System.Void _OnAudioGlobalVolumeChange(System.Single volume)
  RVA=0x04D38130  token=0x6000638  System.Void _OnAudioMusicVolumeChange(System.Single volume)
  RVA=0x04D380C0  token=0x6000639  System.Void _OnAudioVoiceVolumeChange(System.Single volume)
  RVA=0x043F30C0  token=0x600063A  System.Void _OnAudioSfxVolumeChange(System.Single volume)
  RVA=0x04CE1800  token=0x600063B  System.Void _OnAudioSuspendUnfocusedChange(System.Boolean suspendUnfocused)
  RVA=0x069AB054  token=0x600063C  System.Void _OnAudioBackgroundMusicChange(System.Boolean enable)
  RVA=0x03F92AF0  token=0x600063D  System.Void _OnAudioSuiteModeChange(Beyond.GameSetting.GameSettingAudioSuiteMode mode)
  RVA=0x04D27280  token=0x600063E  System.Void _OnAudioControllerChange(System.Boolean enable)
  RVA=0x04007320  token=0x600063F  System.Void _OnAudioSpatialAudioChange(System.Boolean enable)
  RVA=0x043F3290  token=0x6000640  System.Void _OnCameraDistanceLevelChange(System.Single distance)
  RVA=0x04D27200  token=0x6000641  System.Void _OnControllerMotionChange(System.Boolean valid)
  RVA=0x03F92A00  token=0x6000642  System.Void _OnKeyboardTypeChange(Beyond.GameSetting.KeyboardLayoutType value)
  RVA=0x0332ADD0  token=0x6000643  System.Void _OnLanguageTextChange(Beyond.GEnums.EnvLang language)
  RVA=0x0366EE20  token=0x6000644  System.Void _OnLanguageAudioChange(Beyond.GameSetting.GameSettingLanguageAudio language)
  RVA=0x04B89C60  token=0x6000645  System.Void _OnVideoQualityChange(Beyond.GameSetting.GameSettingVideoQuality quality)
  RVA=0x036A4280  token=0x6000646  System.Void _OnVideoFullScreenChange(System.Boolean fullScreen)
  RVA=0x0366D880  token=0x6000647  System.Void _OnVideoResolutionChange(Beyond.GameSetting.ScreenResolution newResolution)
  RVA=0x036A4300  token=0x6000648  System.Void _SetResolution(System.Int32 width, System.Int32 height, System.Boolean fullScreen)
  RVA=0x069AB0A4  token=0x6000649  System.Void _OnVideoNotchPaddingChange(System.Single notchPadding)
  RVA=0x03F92820  token=0x600064A  System.Void _OnTeammateSkillEffectStrengthChange(Beyond.GameSetting.TeammateSkillEffectStrength strength)
  RVA=0x043F31A0  token=0x600064B  System.Void _OnTeammateSkillEffectOpacityChange(System.Single opacity)
END_CLASS

CLASS: Beyond.Scripts.Entry.WarmUpProgress
TYPE:  class
TOKEN: 0x2000118
SIZE:  0x10
IMPLEMENTS: IProgress
FIELDS:
METHODS:
  RVA=0x069B4FDC  token=0x600064C  System.Double GetTotal()
  RVA=0x069B4EE0  token=0x600064D  System.Double GetCurrent()
  RVA=0x069B4F70  token=0x600064E  System.String GetInfo()
  RVA=0x041E1670  token=0x600064F  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x200004D
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001BC  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x200004E
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.Runtime.InteropServices.LayoutKindlayoutKind  // 0x10
  public            System.Boolean                  isTag  // 0x14
METHODS:
  RVA=0x041E1670  token=0x60001BD  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x200004F
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x60001BE  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x2000050
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60001BF  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000051
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60001C0  System.Void .ctor()
END_CLASS

CLASS: Beyond.UI.SplashController
TYPE:  class
TOKEN: 0x2000052
SIZE:  0x80
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.String                   CURSOR_KEY_SPLASH  // const
  private   static  System.String                   SPLASH_RESOURCES_PATH  // const
  private           System.Single                   _initialFadeTime  // 0x18
  private           System.Single                   _regularFadeTime  // 0x1c
  private           System.Single                   _showTime  // 0x20
  private           UnityEngine.UI.Image            _blackCoverImage  // 0x28
  private           UnityEngine.UI.Image            _splashImage  // 0x30
  private           Beyond.UI.SplashController.SplashConfigs_splashConfigsCN  // 0x38
  private           Beyond.UI.SplashController.SplashConfigs_splashConfigsOS  // 0x50
  private           DG.Tweening.Sequence            m_tween  // 0x68
  private           System.Action                   m_onComplete  // 0x70
  private           System.Boolean                  m_skipLicense  // 0x78
  private   static  System.Boolean                  s_isPlaying  // static @ 0x0
  private           System.Boolean                  m_canBeHide  // 0x79
  private   static  Beyond.UI.SplashController      <instance>k__BackingField  // static @ 0x8
PROPERTIES:
  instance  get=0x06993DDC  set=0x06993E14
  isPlaying  get=0x0340B960
METHODS:
  RVA=0x0380F750  token=0x60001C4  Beyond.UI.SplashController Show(System.Action onComplete, System.Boolean skipLicense)
  RVA=0x0380F650  token=0x60001C5  System.Void Hide()
  RVA=0x0380F5A0  token=0x60001C6  System.Void CanFinished()
  RVA=0x0380F460  token=0x60001C7  System.Void Awake()
  RVA=0x0380F510  token=0x60001C8  System.Void OnDestroy()
  RVA=0x041E4EF0  token=0x60001C9  System.Void _Play(System.Action onComplete, System.Boolean skipLicense)
  RVA=0x0380E980  token=0x60001CA  System.Void _Stop()
  RVA=0x041E5000  token=0x60001CB  Beyond.UI.SplashController.SplashConfig[] _GetSplashConfigs()
  RVA=0x045E2E10  token=0x60001CC  System.Void _DoStartSplash(Beyond.UI.SplashController.SplashConfig[] splashConfigs)
  RVA=0x04CA7270  token=0x60001CD  System.Void _OnTweenFinished()
  RVA=0x04CA72D0  token=0x60001CE  System.Collections.IEnumerator _OnSplashCompleteCoroutine()
  RVA=0x04D32AA0  token=0x60001CF  System.Collections.IEnumerator _StartLicenseCoroutine()
  RVA=0x04D5EC50  token=0x60001D0  System.Void .ctor()
  RVA=0x04D4E7C0  token=0x60001D1  System.Void <_DoStartSplash>b__31_1()
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000004  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x200011B
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x6000650  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x06360E84  token=0x6000651  Beyond.GameState __Gen_Wrap_0(System.Object P0)
  RVA=0x066F3838  token=0x6000652  System.Void __Gen_Wrap_1(System.Object P0, Beyond.GameState P1)
  RVA=0x069ABD7C  token=0x6000653  System.Void __Gen_Wrap_2(System.Single P0)
  RVA=0x06375088  token=0x6000654  System.Void __Gen_Wrap_3(System.Boolean P0)
  RVA=0x0635FF24  token=0x6000655  System.Void __Gen_Wrap_4(Beyond.GameSetting.GameSettingAudioSuiteMode P0)
  RVA=0x0635FF24  token=0x6000656  System.Void __Gen_Wrap_5(Beyond.GameSetting.KeyboardLayoutType P0)
  RVA=0x0635FF24  token=0x6000657  System.Void __Gen_Wrap_6(Beyond.GEnums.EnvLang P0)
  RVA=0x0635FF24  token=0x6000658  System.Void __Gen_Wrap_7(Beyond.GameSetting.GameSettingLanguageAudio P0)
  RVA=0x0635FF24  token=0x6000659  System.Void __Gen_Wrap_8(Beyond.GameSetting.GameSettingVideoQuality P0)
  RVA=0x069AF3C0  token=0x600065A  System.Void __Gen_Wrap_9(System.Int32 P0, System.Int32 P1, System.Boolean P2)
  RVA=0x069AB15C  token=0x600065B  System.Void __Gen_Wrap_10(Beyond.GameSetting.ScreenResolution P0)
  RVA=0x0635FF24  token=0x600065C  System.Void __Gen_Wrap_11(Beyond.GameSetting.TeammateSkillEffectStrength P0)
  RVA=0x063625A4  token=0x600065D  System.Void __Gen_Wrap_12()
  RVA=0x063633B4  token=0x600065E  System.Void __Gen_Wrap_13(System.Object P0)
  RVA=0x063632E4  token=0x600065F  System.Boolean __Gen_Wrap_14(System.Object P0)
  RVA=0x069AB250  token=0x6000660  Beyond.UI.SplashController.SplashConfig[] __Gen_Wrap_15(System.Object P0)
  RVA=0x069AB32C  token=0x6000661  System.Collections.IEnumerator __Gen_Wrap_16(System.Object P0)
  RVA=0x06377B18  token=0x6000662  System.Void __Gen_Wrap_17(System.Object P0, System.Object P1)
  RVA=0x069AB41C  token=0x6000663  System.Void __Gen_Wrap_18(System.Object P0, System.Object P1, System.Boolean P2)
  RVA=0x069AB500  token=0x6000664  Beyond.UI.SplashController __Gen_Wrap_19(System.Object P0, System.Boolean P1)
  RVA=0x069AB5F8  token=0x6000665  System.Void __Gen_Wrap_20(Beyond.EventData<System.Boolean,System.Boolean>& P0)
  RVA=0x069AB6F0  token=0x6000666  System.String __Gen_Wrap_21()
  RVA=0x069AB7A8  token=0x6000667  System.String __Gen_Wrap_22(System.Object P0)
  RVA=0x069AB898  token=0x6000668  System.Boolean __Gen_Wrap_23(System.Object P0, System.UInt32 P1)
  RVA=0x069AB970  token=0x6000669  Beyond.GameApp.FileEntry __Gen_Wrap_24(Beyond.GameApp.PendingFile P0)
  RVA=0x069ABA7C  token=0x600066A  System.Collections.Generic.List<Beyond.GameApp.FileEntry> __Gen_Wrap_25(System.Object P0, System.Int32 P1)
  RVA=0x06360610  token=0x600066B  System.Int32 __Gen_Wrap_26(System.Object P0, System.Object P1)
  RVA=0x069ABB74  token=0x600066C  System.String __Gen_Wrap_27(System.Int64 P0)
  RVA=0x06364290  token=0x600066D  System.Void __Gen_Wrap_28(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x069ABC50  token=0x600066E  System.Void __Gen_Wrap_29(System.Object P0, System.Object P1, System.DateTime P2, System.Object P3, System.Object P4)
  RVA=0x069ABE3C  token=0x600066F  System.Void __Gen_Wrap_30(System.Object P0, System.Object P1, System.Int32 P2, System.Object P3)
  RVA=0x063778A4  token=0x6000670  System.Void __Gen_Wrap_31(System.Object P0, System.Object P1, System.Int32 P2)
  RVA=0x063623A8  token=0x6000671  System.Boolean __Gen_Wrap_32()
  RVA=0x066C2B40  token=0x6000672  System.Void __Gen_Wrap_33(System.Object P0, System.Boolean P1)
  RVA=0x0635FF24  token=0x6000673  System.Void __Gen_Wrap_34(System.Int32 P0)
  RVA=0x0635FF24  token=0x6000674  System.Void __Gen_Wrap_35(Beyond.GameAppExitCode P0)
  RVA=0x069ABF34  token=0x6000675  System.Boolean __Gen_Wrap_36(System.String& P0)
  RVA=0x069AC03C  token=0x6000676  System.Boolean __Gen_Wrap_37(System.Object P0, Beyond.Resource.HotUpdate.LocalResPref& P1, Beyond.VFS.VirtualFileSystem.BlockValidator& P2)
  RVA=0x069AC1C4  token=0x6000677  System.Void __Gen_Wrap_38(System.Object P0, Beyond.VFS.VirtualFileSystem.BlockValidator& P1)
  RVA=0x069AC2EC  token=0x6000678  System.Void __Gen_Wrap_39(System.Object P0, Beyond.EventData<Beyond.DeviceInfo.InputType>& P1)
  RVA=0x069AC400  token=0x6000679  Beyond.LoginController.MaskCtrl __Gen_Wrap_40(System.Object P0)
  RVA=0x069AC4F0  token=0x600067A  Beyond.UI.UISwitchTween __Gen_Wrap_41(System.Object P0, System.Int32 P1)
  RVA=0x069AC5FC  token=0x600067B  System.Collections.IEnumerator __Gen_Wrap_42(System.Object P0, System.Object P1)
  RVA=0x069AC708  token=0x600067C  System.Void __Gen_Wrap_43(System.Object P0, System.Object P1, Beyond.LoginController.EMaskType P2, System.Boolean P3, System.Boolean P4)
  RVA=0x069AC81C  token=0x600067D  Beyond.UI.UIAnimationLayerMixPlayer __Gen_Wrap_44(System.Object P0)
  RVA=0x069AC90C  token=0x600067E  System.Void __Gen_Wrap_45(System.Object P0, Beyond.LoginController.FOptions P1)
  RVA=0x069ACA1C  token=0x600067F  Beyond.Login.LoginWorkFlow.FWork __Gen_Wrap_46()
  RVA=0x069ACB10  token=0x6000680  Beyond.Login.LoginWorkFlow.ENode __Gen_Wrap_47(Beyond.Login.LoginWorkFlow.FWork& P0)
  RVA=0x069ACC64  token=0x6000681  System.Boolean __Gen_Wrap_48(Beyond.Login.LoginWorkFlow.ENode P0, Beyond.Login.LoginWorkFlow.ENode P1)
  RVA=0x06361FA4  token=0x6000682  System.Boolean __Gen_Wrap_49(Beyond.Login.LoginWorkFlow.ENode P0)
  RVA=0x069ACD4C  token=0x6000683  System.Collections.IEnumerator __Gen_Wrap_50(Beyond.Login.LoginWorkFlow.ENode P0)
  RVA=0x069ACE38  token=0x6000684  UnityEngine.CustomYieldInstruction __Gen_Wrap_51(System.Object P0)
  RVA=0x069ACF28  token=0x6000685  System.Boolean __Gen_Wrap_52(Beyond.Login.LoginWorkFlow.FWork& P0)
  RVA=0x069AD07C  token=0x6000686  System.Void __Gen_Wrap_53(Beyond.Login.LoginWorkFlow.FWork& P0)
  RVA=0x069AD1C0  token=0x6000687  System.Void __Gen_Wrap_54(System.Object P0, Beyond.Login.LoginAlertDialog.FParams P1)
  RVA=0x069AD2C0  token=0x6000688  System.Void __Gen_Wrap_55(System.Object P0, Beyond.Login.LoginVocResSelDialog.FParams P1)
  RVA=0x069AD3C0  token=0x6000689  System.Void __Gen_Wrap_56(System.Object P0, Beyond.Login.LoginJudgeDialog.FParams P1)
  RVA=0x069AD4E4  token=0x600068A  System.Void __Gen_Wrap_57(Beyond.Login.LoginAlertDialog P0, Beyond.Login.LoginAlertDialog.FParams P1)
  RVA=0x069AD5E4  token=0x600068B  System.Void __Gen_Wrap_58(System.Object P0, System.Boolean P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6, System.Object P7, System.Object P8)
  RVA=0x069AD73C  token=0x600068C  System.Collections.IEnumerator __Gen_Wrap_59(System.Object P0, System.Boolean P1)
  RVA=0x069AD848  token=0x600068D  System.Boolean __Gen_Wrap_60(System.Object P0, Beyond.Login.LoginWorkFlow.ENode& P1)
  RVA=0x0635FF24  token=0x600068E  System.Void __Gen_Wrap_61(Beyond.GameState P0)
  RVA=0x066C288C  token=0x600068F  System.Void __Gen_Wrap_62(System.Object P0, System.Single P1)
  RVA=0x069AD954  token=0x6000690  Beyond.Login.LoginRootPanel __Gen_Wrap_63(System.Object P0)
  RVA=0x066C0730  token=0x6000691  System.Single __Gen_Wrap_64(System.Object P0)
  RVA=0x069ADA44  token=0x6000692  Beyond.LoginController __Gen_Wrap_65(System.Object P0)
  RVA=0x069ADB34  token=0x6000693  System.Void __Gen_Wrap_66(System.Object P0, System.Object P1, System.Single P2)
  RVA=0x069ADC18  token=0x6000694  System.Collections.Generic.List<Beyond.Gameplay.ServerZoneManager.ServerZoneData> __Gen_Wrap_67(System.Object P0)
  RVA=0x06366BC4  token=0x6000695  System.Boolean __Gen_Wrap_68(System.Object P0, System.Object P1)
  RVA=0x069ADD08  token=0x6000696  Beyond.Gameplay.ServerZoneManager.ServerZoneData __Gen_Wrap_69(System.Object P0)
  RVA=0x069ADDF8  token=0x6000697  System.Boolean __Gen_Wrap_70(System.Object P0, Beyond.ELoginEvent P1, System.Object P2)
  RVA=0x066F4C88  token=0x6000698  System.Void __Gen_Wrap_71(System.Object P0, Beyond.ELoginEvent P1, System.Object P2)
  RVA=0x066C296C  token=0x6000699  System.Void __Gen_Wrap_72(System.String P0)
  RVA=0x066C296C  token=0x600069A  System.Void __Gen_Wrap_73(UnityEngine.EventSystems.PointerEventData P0)
  RVA=0x069ADEFC  token=0x600069B  System.Boolean __Gen_Wrap_74(Beyond.Login.LoginMockUIDPanel.FOptions& P0)
  RVA=0x069AE048  token=0x600069C  System.Void __Gen_Wrap_75(System.Object P0, System.Int32& P1)
  RVA=0x069AE148  token=0x600069D  System.Void __Gen_Wrap_76(System.Int32 P0, Beyond.UI.UIDropdownOption P1, System.Boolean P2)
  RVA=0x066F3838  token=0x600069E  System.Void __Gen_Wrap_77(System.Object P0, System.Int32 P1)
  RVA=0x069AE22C  token=0x600069F  System.Void __Gen_Wrap_78(System.Object P0, Beyond.Login.LoginMockUIDPanel.FOptions P1, System.Object P2)
  RVA=0x069AE358  token=0x60006A0  Beyond.UI.UISwitchTween __Gen_Wrap_79(System.Object P0)
  RVA=0x066E0A9C  token=0x60006A1  System.Boolean __Gen_Wrap_80(System.Object P0, Beyond.LoginController.EMaskType P1)
  RVA=0x069AE448  token=0x60006A2  System.Void __Gen_Wrap_81(Beyond.Login.LoginVocResSelDialog P0, Beyond.Login.LoginVocResSelDialog.FParams P1)
  RVA=0x069AE548  token=0x60006A3  System.Collections.IEnumerator __Gen_Wrap_82(System.Object P0, System.Object P1, System.Object P2, System.Object P3)
  RVA=0x069AE680  token=0x60006A4  System.Void __Gen_Wrap_83(Beyond.Login.LoginJudgeDialog P0, Beyond.Login.LoginJudgeDialog.FParams P1)
  RVA=0x066E8E30  token=0x60006A5  System.Void __Gen_Wrap_84(System.Object P0, System.Object P1, System.Object P2, System.Object P3)
  RVA=0x069AE790  token=0x60006A6  System.String __Gen_Wrap_85(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x069AE8B8  token=0x60006A7  System.Void __Gen_Wrap_86(System.Boolean& P0)
  RVA=0x069AE98C  token=0x60006A8  System.Void __Gen_Wrap_87(System.Object P0, System.Boolean& P1)
  RVA=0x069AEA7C  token=0x60006A9  System.Void __Gen_Wrap_88(System.Object P0, Beyond.LoginManager.FFlowWithMask P1)
  RVA=0x069AEBA0  token=0x60006AA  System.Boolean __Gen_Wrap_89(Beyond.LoginManager.FFlowCondition& P0, Beyond.Login.LoginWorkFlow.ENode P1)
  RVA=0x069AECF4  token=0x60006AB  System.Collections.IEnumerator __Gen_Wrap_90(System.Object P0, Beyond.LoginManager.FFlowWithMask P1)
  RVA=0x069AEE34  token=0x60006AC  TMPro.TMP_FontAsset __Gen_Wrap_91(System.Object P0)
  RVA=0x069AEF10  token=0x60006AD  System.Void __Gen_Wrap_92(Beyond.SDK.SDKNetUtils.HttpPostRequest P0)
  RVA=0x069AF004  token=0x60006AE  System.Void __Gen_Wrap_93(Beyond.SDK.SDKNetUtils.HttpGetRequest P0)
  RVA=0x069AF0E8  token=0x60006AF  System.Collections.IEnumerator __Gen_Wrap_94(System.Object P0, Beyond.SDK.SDKNetUtils.HTTPConfig P1)
  RVA=0x069AF218  token=0x60006B0  System.Collections.IEnumerator __Gen_Wrap_95()
  RVA=0x069AF2E4  token=0x60006B1  System.Collections.Generic.Dictionary<System.String,System.String> __Gen_Wrap_96(System.Object P0)
  RVA=0x06360E84  token=0x60006B2  System.Int32 __Gen_Wrap_97(System.Object P0)
  RVA=0x011D1C90  token=0x60006B3  System.Int64 __Gen_Wrap_98(System.Object P0)
  RVA=0x01527E90  token=0x60006B4  System.Void __Gen_Wrap_99(System.Object P0, U8.SDK.SDKExtraData P1)
  RVA=0x0150B3A0  token=0x60006B5  U8.SDK.SDKExternalTools.POSTResult __Gen_Wrap_100(Beyond.SDK.SDKNetUtils.WebHttpResponse P0)
  RVA=0x0150BF40  token=0x60006B6  System.Void __Gen_Wrap_101(Beyond.SDK.SDKNetUtils.WebHttpResponse P0)
  RVA=0x0150C4F0  token=0x60006B7  System.Void __Gen_Wrap_102(System.Object P0, U8.SDK.SDKExternalTools.POSTRequest P1, System.Object P2)
  RVA=0x0150CC40  token=0x60006B8  U8.SDK.SDKCaptchaHandler __Gen_Wrap_103(System.Object P0)
  RVA=0x0150D570  token=0x60006B9  System.Boolean __Gen_Wrap_104(System.Object P0, U8.SDK.SDKExternalTools.ErrMsgMeta P1, System.String& P2)
  RVA=0x0150E2F0  token=0x60006BA  System.String __Gen_Wrap_105(System.Object P0, U8.SDK.SDKExternalTools.ErrMsgMeta P1)
  RVA=0x0150EFD0  token=0x60006BB  System.Collections.IEnumerator __Gen_Wrap_106(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x0150FAD0  token=0x60006BC  System.Void __Gen_Wrap_107(System.Object P0, U8.SDK.ExternalPluginLoginParams P1)
  RVA=0x01510160  token=0x60006BD  System.Void __Gen_Wrap_108(System.Object P0, U8.SDK.ExternalPluginLogoutParams P1)
  RVA=0x015107D0  token=0x60006BE  System.Void __Gen_Wrap_109(U8.SDK.U8PayResult P0)
  RVA=0x01510D70  token=0x60006BF  System.Void __Gen_Wrap_110(System.Object P0, U8.SDK.ExternalPluginPayParams P1)
  RVA=0x015113E0  token=0x60006C0  System.Boolean __Gen_Wrap_111(System.Object P0, System.Object P1, U8.SDK.SDKMeta& P2)
  RVA=0x01298AE0  token=0x60006C1  Beyond.Login.LoginWorkFlow.ENode __Gen_Wrap_112(System.Object P0)
  RVA=0x01511F20  token=0x60006C2  System.Void __Gen_Wrap_113(Beyond.Login.LoginWorkFlow.FWork& P0, System.Int32 P1)
  RVA=0x01512B10  token=0x60006C3  System.Boolean __Gen_Wrap_114(System.Object P0, Beyond.Login.LoginWorkFlow.ENode P1, System.Boolean P2)
  RVA=0x01512E20  token=0x60006C4  Beyond.SDK.HGDownloader.DownloadProgress __Gen_Wrap_115(System.Object P0)
  RVA=0x01298AE0  token=0x60006C5  Beyond.SDK.HGDownloader.WorkState __Gen_Wrap_116(System.Object P0)
  RVA=0x01513720  token=0x60006C6  System.Collections.Generic.List<Beyond.SDK.HGFileInfo> __Gen_Wrap_117(System.Object P0, System.Object P1)
  RVA=0x01514140  token=0x60006C7  System.Collections.Generic.List<Beyond.SDK.HGFolder> __Gen_Wrap_118(System.Object P0, System.Object P1)
  RVA=0x0129F0D0  token=0x60006C8  System.Boolean __Gen_Wrap_119(System.Object P0, Beyond.SDK.HGRetCodeType P1, System.Int32 P2)
  RVA=0x01514B60  token=0x60006C9  System.String __Gen_Wrap_120(System.Double P0)
  RVA=0x015153E0  token=0x60006CA  System.Void __Gen_Wrap_121(System.Object P0, System.Int64 P1, System.Object P2, System.Object P3, System.Object P4)
  RVA=0x012A0D50  token=0x60006CB  System.Void __Gen_Wrap_122(System.Object P0, Beyond.SDK.HGDownloader.PauseReason P1)
  RVA=0x01515910  token=0x60006CC  System.Collections.IEnumerator __Gen_Wrap_123(System.Object P0, System.Int64 P1)
  RVA=0x015162B0  token=0x60006CD  System.Void __Gen_Wrap_124(System.Object P0, System.Int64 P1, System.Int64 P2, System.Int64 P3)
  RVA=0x0146E800  token=0x60006CE  System.Double __Gen_Wrap_125(System.Object P0)
  RVA=0x01408E40  token=0x60006CF  System.Boolean __Gen_Wrap_126(System.Boolean P0)
  RVA=0x015165C0  token=0x60006D0  System.String __Gen_Wrap_127(System.Object P0, System.Boolean P1)
  RVA=0x01516F60  token=0x60006D1  Beyond.Resource.HotUpdate.LocalResPref __Gen_Wrap_128()
  RVA=0x015177A0  token=0x60006D2  Beyond.Login.LoginHotUpdater.HotUpdateInfo __Gen_Wrap_129(System.Object P0, System.Boolean P1)
  RVA=0x01518140  token=0x60006D3  Beyond.Resource.HotUpdate.ResourceIndex __Gen_Wrap_130()
  RVA=0x01518980  token=0x60006D4  System.String __Gen_Wrap_131(System.Object P0, System.Object P1, System.Boolean P2)
  RVA=0x015193D0  token=0x60006D5  System.Object __Gen_Wrap_132()
  RVA=0x01519C10  token=0x60006D6  System.Collections.IEnumerator __Gen_Wrap_133(System.Object P0, System.Object P1, System.Boolean P2, System.Object P3)
  RVA=0x0151A730  token=0x60006D7  System.Collections.IEnumerator __Gen_Wrap_134(System.Boolean P0, System.Object P1, System.Object P2)
  RVA=0x0151B170  token=0x60006D8  Beyond.Resource.HotUpdate.LocalResPref __Gen_Wrap_135(System.Object P0, System.Boolean P1, System.Object P2, Beyond.Resource.HotUpdate.FResourceDiff P3, System.Int32& P4)
  RVA=0x0151BED0  token=0x60006D9  System.Void __Gen_Wrap_136(System.Object P0, System.Boolean& P1, System.Double P2, System.Double P3, System.Int32 P4, System.Boolean P5)
  RVA=0x0151C240  token=0x60006DA  System.Boolean __Gen_Wrap_137(System.Object P0, Beyond.VFS.EVFSBlockType P1)
  RVA=0x0151C530  token=0x60006DB  System.Collections.IEnumerator __Gen_Wrap_138(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3)
  RVA=0x0151CFA0  token=0x60006DC  Beyond.Login.LoginHotUpdater __Gen_Wrap_139(System.Object P0, System.Boolean P1)
  RVA=0x0151D900  token=0x60006DD  Beyond.Login.LoginProgress.FDownloadSize.EUnit __Gen_Wrap_140(System.Double P0)
  RVA=0x0151DB00  token=0x60006DE  System.Single __Gen_Wrap_141(System.Double P0, Beyond.Login.LoginProgress.FDownloadSize.EUnit P1)
  RVA=0x0151DD30  token=0x60006DF  System.String __Gen_Wrap_142(System.Single P0, System.Single P1, Beyond.Login.LoginProgress.FDownloadSize.EUnit P2)
  RVA=0x0151E620  token=0x60006E0  System.String __Gen_Wrap_143(System.Single P0, Beyond.Login.LoginProgress.FDownloadSize.EUnit P1)
  RVA=0x0151EED0  token=0x60006E1  System.String __Gen_Wrap_144(System.Single P0, System.Single P1)
  RVA=0x0151F7A0  token=0x60006E2  System.Void __Gen_Wrap_145(Beyond.Login.LoginProgress.FDownloadSize& P0, System.Double P1, System.Double P2, System.Double P3)
  RVA=0x01520470  token=0x60006E3  System.Void __Gen_Wrap_146(Beyond.Login.LoginProgress.FDownloadSize& P0, System.Double P1, System.Double P2)
  RVA=0x01521120  token=0x60006E4  System.Void __Gen_Wrap_147(Beyond.Login.LoginProgress.FDownloadSize& P0)
  RVA=0x0133CDE0  token=0x60006E5  System.Void __Gen_Wrap_148(System.Object P0, Beyond.GameAppExitCode P1)
  RVA=0x012A5970  token=0x60006E6  System.Void __Gen_Wrap_149(Hypergryph.SpeedTest.SpeedTestResult P0)
  RVA=0x01521D30  token=0x60006E7  System.Threading.Tasks.Task<Hypergryph.SpeedTest.TaskResult> __Gen_Wrap_150()
  RVA=0x01522570  token=0x60006E8  System.Boolean __Gen_Wrap_151(Beyond.Login.LoginViewModel.FAgeTipsPolicy& P0)
  RVA=0x01523140  token=0x60006E9  Beyond.Login.LoginViewModel.FAgeTipsPolicy __Gen_Wrap_152()
  RVA=0x01523950  token=0x60006EA  Beyond.Login.LoginWorkFlow.Node[] __Gen_Wrap_153(System.Object P0)
  RVA=0x01524290  token=0x60006EB  System.Single __Gen_Wrap_154(System.Object P0, System.Boolean& P1)
  RVA=0x01298AE0  token=0x60006EC  Beyond.TickType __Gen_Wrap_155(System.Object P0)
  RVA=0x015245B0  token=0x60006ED  Beyond.UI.UIAnimationMixPlayer __Gen_Wrap_156(System.Object P0)
  RVA=0x0133CDE0  token=0x60006EE  System.Void __Gen_Wrap_157(System.Object P0, Beyond.Login.LoginSceneAnimCtrl.EState P1)
  RVA=0x01524EF0  token=0x60006EF  System.Collections.Generic.List<System.String> __Gen_Wrap_158(System.Object P0)
  RVA=0x01525830  token=0x60006F0  System.Void __Gen_Wrap_159(System.Object P0, Beyond.Login.LoginViewModel.FAgeTipsPolicy P1)
  RVA=0x01525EC0  token=0x60006F1  System.Void __Gen_Wrap_160(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3)
  RVA=0x015262A0  token=0x60006F2  System.Void __Gen_Wrap_161(System.Boolean P0, System.Boolean P1, System.Boolean P2)
  RVA=0x015264D0  token=0x60006F3  System.Void __Gen_Wrap_162(System.Object P0, Beyond.EventData<System.Boolean>& P1)
  RVA=0x01526B50  token=0x60006F4  Beyond.Login.LoginMockUIDPanel __Gen_Wrap_163()
  RVA=0x01527390  token=0x60006F5  System.Void __Gen_Wrap_164(System.Object P0, Beyond.EventData<System.Single>& P1)
  RVA=0x01527A20  token=0x60006F6  System.Void __Gen_Wrap_165(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3)
  RVA=0x012ABC50  token=0x60006F7  System.Void __Gen_Wrap_166(System.Object P0, System.Int32 P1, System.Object P2)
  RVA=0x0122AC70  token=0x60006F8  System.Void __Gen_Wrap_167(System.Object P0, Beyond.VFS.EVFSBlockType P1)
  RVA=0x04D37790  token=0x60006F9  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x200011C
SIZE:  0x58
EXTENDS: IFix.Core.AnonymousStorey
IMPLEMENTS: System.IDisposable System.Collections.IEnumerator System.Collections.Generic.IEnumerator`1
FIELDS:
  private           System.Int32                    methodId_0  // 0x40
  private           System.Int32                    methodId_1  // 0x44
  private           System.Int32                    methodId_2  // 0x48
  private           System.Int32                    methodId_3  // 0x4c
  private           System.Int32                    methodId_4  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x069B5064
  System.Collections.IEnumerator.Current  get=0x069B5110
METHODS:
  RVA=0x06365FC0  token=0x60006FA  System.Void System.IDisposable.Dispose()
  RVA=0x06365D60  token=0x60006FB  System.Boolean MoveNext()
  RVA=0x06365E98  token=0x60006FD  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x069B51BC  token=0x60006FF  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x6000700  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x200011D
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000701  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x069B5480  token=0x6000702  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x02F44DE0  token=0x6000703  System.Boolean IsPatched(System.Int32 id)
  RVA=0x069B533C  token=0x6000704  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x069B5408  token=0x6000705  System.Object CreateWrapper(System.Int32 id)
  RVA=0x069B54E4  token=0x6000706  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x069B5298  token=0x6000707  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x200011E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-GameApp-get_currentState0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-SwitchState0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnAudioGlobalVolumeChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnAudioMusicVolumeChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnAudioVoiceVolumeChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnAudioSfxVolumeChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnAudioSuspendUnfocusedChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnAudioBackgroundMusicChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnAudioSuiteModeChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnAudioControllerChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnAudioSpatialAudioChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnCameraDistanceLevelChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnControllerMotionChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnKeyboardTypeChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnLanguageTextChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnLanguageAudioChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnVideoQualityChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_SetResolution0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnVideoFullScreenChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnVideoResolutionChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnVideoNotchPaddingChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnTeammateSkillEffectStrengthChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-_OnTeammateSkillEffectOpacityChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-GameSettingSetter-RegisterSettingCallbacks0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_PreInitGameSetting0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_NeedDeepProfilerMode0  // const
  public    static  IFix.IDMAP0                     Beyond-RenderPipelineDataLoader-LoadSimpleRpData0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-_GetSplashConfigs0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-PublicSetGameVersion0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-_StartLicenseCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-_Stop0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-Hide0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-_OnSplashCompleteCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-_OnTweenFinished0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-_DoStartSplash0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-_Play0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-Show0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_InitNotificationCenter0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_FetchRemoteConfigUtilSuc0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_InitSecuritySDK0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_InitGameStateMachine0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_OnApplicationFocusForLua0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_OnApplicationPauseForLua0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_RegisterLuaAfterGCEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-RefreshScreenResolution0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_OnSystemDisplaySizeChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_OnSystemScreenSizeChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_Init0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_CollectSystemInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_MakeLongPath0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_ScanDirectory0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_CanAccess0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_CalculateMd50  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_BuildEntry0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_WorkerLoop0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_ScanParallel0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_TreeNodeCompare0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_FormatSize0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_PrintTreeNode0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_WriteTreeReport0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-LaunchScanCoordinator0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-ScanLocalFiles0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-get_IsScanRunning0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_DelayInit0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-Update0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-LateUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-OnApplicationQuit0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-OnApplicationFocus0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_OnApplicationPauseForSecurity0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-OnApplicationPause0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_UnInitGameStateMachine0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-_UnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-QuitGame0  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-QuitGame1  // const
  public    static  IFix.IDMAP0                     Beyond-GameApp-PollScanResult0  // const
  public    static  IFix.IDMAP0                     Beyond-GameInitState-get_stateId0  // const
  public    static  IFix.IDMAP0                     Beyond-GameStateBase-OnEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-GameInitState-_CheckIfLocalInitialAssetsValid0  // const
  public    static  IFix.IDMAP0                     Beyond-GameInitState-_InitAPPDefaultCulture0  // const
  public    static  IFix.IDMAP0                     Beyond-GameInitState-_InitInitialResourceManager0  // const
  public    static  IFix.IDMAP0                     Beyond-RenderPipelineDataLoader-PreloadFullRpResources0  // const
  public    static  IFix.IDMAP0                     Beyond-VietnamLoginAgePanel-_UnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-VietnamLoginAgePanel-_Init0  // const
  public    static  IFix.IDMAP0                     Beyond-VietnamLoginAgePanel-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-GameInitState-_InitGameSetting0  // const
  public    static  IFix.IDMAP0                     Beyond-RenderPipelineDataLoader-IsPreLoadFinished0  // const
  public    static  IFix.IDMAP0                     Beyond-GameInitState-_DoInit0  // const
  public    static  IFix.IDMAP0                     Beyond-GameInitState-OnEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-GameStateBase-OnLeave0  // const
  public    static  IFix.IDMAP0                     Beyond-GameInitState-OnLeave0  // const
  public    static  IFix.IDMAP0                     Beyond-GameLoginState-get_stateId0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-CanFinished0  // const
  public    static  IFix.IDMAP0                     Beyond-RenderPipelineDataLoader-LoadFullRpResources0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginContext-IsDisposed0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-IsWorkflowInProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Node-OnDispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Node-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_EnableCursor0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_UpdateCursor0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_EnbaleToggleGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Worker-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-AssetLoader-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginContext-_DisposeImpl0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-get_maskCtrl0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-MaskCtrl-_GetMaskByType0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-MaskCtrl-IsCrossSceneMaskTweening0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-MaskCtrl-WaitForCrossSceneMasks0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-MaskCtrl-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-BeforeDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginContext-_DisposeCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginContext-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginContext-IsWorkflowDisposed0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-MaskCtrl-EnableMask0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-_InitController0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-ChildPanel-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-ChildPanel-OnLoginContext0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-_InitChildPanel0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDecorateUI-_EnsureGlowPlayer0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDecorateUI-_ToggleGlow0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDecorateUI-OnLoginContext0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-_InitVersionBtn0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-_InitCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-_InitImpl0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-InitUI0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-FWork-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-FWork-get_curNode0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-IsOnOrAfterWork0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-IsBeforeWork0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_CheckIfGameStateNotMatch0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_AdjustCurrentGameState0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Worker-SetCurNode0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Worker-_InternalTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Node-Cancel0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Worker-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-IsNeedSoftRestart0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Worker-TickInEmptyFrame0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-FWork-ConsumeFallback0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-FWork-MoveNextIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-FWork-WorkEnd0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-DoWorkFlow0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-_LoginWorkFlowCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-_PrepareBeforeLoginProcess0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAlertDialog-SetParams0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-SetParams0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginJudgeDialog-SetParams0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginContext-StartCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-AlertDialog0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginContext-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-InvokeWhenStart0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-_Init0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-InitCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-GameLoginState-_TryStartLoginWorkflow0  // const
  public    static  IFix.IDMAP0                     Beyond-GameLoginState-OnEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-GameLoginState-OnLeave0  // const
  public    static  IFix.IDMAP0                     Beyond-GameMainState-get_stateId0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginContext-ShutDown0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-_ClearWorkflowAndContext0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-_ReleaseLoginScene0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-GameMainState-OnEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-IsAfterWork0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-PeekFallbackStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-IsReLoginInProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-OnReleaseMainGame0  // const
  public    static  IFix.IDMAP0                     Beyond-GameMainState-_ClearUtils0  // const
  public    static  IFix.IDMAP0                     Beyond-GameMainState-ReleaseMainGame0  // const
  public    static  IFix.IDMAP0                     Beyond-GameMainState-OnLeave0  // const
  public    static  IFix.IDMAP0                     Beyond-GamePreloadState-get_stateId0  // const
  public    static  IFix.IDMAP0                     Beyond-GamePreloadState-_InitGameSettingStateInPreload0  // const
  public    static  IFix.IDMAP0                     Beyond-GamePreloadState-_DoPreload0  // const
  public    static  IFix.IDMAP0                     Beyond-GamePreloadState-OnEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-GamePreloadState-OnLeave0  // const
  public    static  IFix.IDMAP0                     Beyond-GameQuitState-get_stateId0  // const
  public    static  IFix.IDMAP0                     Beyond-VietnamLoginAgePanel-UnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-GameQuitState-DoQuit0  // const
  public    static  IFix.IDMAP0                     Beyond-GameQuitState-OnEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-GameQuitState-OnLeave0  // const
  public    static  IFix.IDMAP0                     Beyond-GameSoftRestartState-get_stateId0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-ClearSync0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-ClearEnteredPreloadThisSession0  // const
  public    static  IFix.IDMAP0                     Beyond-GameSoftRestartState-_SoftRestart0  // const
  public    static  IFix.IDMAP0                     Beyond-GameSoftRestartState-OnEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-GameStartState-get_stateId0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-InvokeWhenWorkFlowFinished0  // const
  public    static  IFix.IDMAP0                     Beyond-GameStartState-_OnEnterMainGame0  // const
  public    static  IFix.IDMAP0                     Beyond-GameStartState-OnEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-GameStartState-OnLeave0  // const
  public    static  IFix.IDMAP0                     Beyond-GameStateBase-OnTick0  // const
  public    static  IFix.IDMAP0                     Beyond-GameStateMachine-get_curState0  // const
  public    static  IFix.IDMAP0                     Beyond-GameStateMachine-_RegisterStates0  // const
  public    static  IFix.IDMAP0                     Beyond-GameStateMachine-CheckCanReturnToLoginState0  // const
  public    static  IFix.IDMAP0                     Beyond-GameStateMachine-ReturnToLoginState0  // const
  public    static  IFix.IDMAP0                     Beyond-GlobalInitializer-InitGlobalConfig0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-get_rootPanel0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-GetEnterGamePanelPredelay0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsModel-IsContentEmpty0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginContext-ViewOnlyController0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-MaskCtrl-EnableNetMask0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-MaskCtrl-DisableNetMask0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_FetchAgeTipsCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-ToggleLoginBindingGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_EventOnOpenAgeTips0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_EventOnCloseAgeTips0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_EventOnOpenServerSelect0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectModel-get_serverList0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectModel-UpdateSelectedServer0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_EventOnSelectServer0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectModel-GetSelectedServer0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_EventOnConfirmServer0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_HandleGlobalEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Node-OnEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-SendEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-OnValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-_SaveConfig0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-_OnBtnEnterGame0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-_OnBtnDelLocalFile0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-FOptions-IsUIDFixed0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-_TryClosePanel0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-_IndexOfServer0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-_MovePreferredServerToFront0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-_SaveLastSelectedServer0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-_OnSelectServer0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-_InitLoginInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-_UpdateStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-LoginWithCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-HideAndDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-_MockLoginCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-MockLoginWithCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-Dialog-_CloseCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-Dialog-TryCloseWithCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-Dialog-Show0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-Dialog-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-MaskCfg-CreateTweenAndHide0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginController-MaskCtrl-IsMaskTweening0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginContext-StopCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginContext-SendEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-get_firstLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneComponent-OnBindToManager0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneComponent-Bind0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-_BindPendingComponents0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-SceneComponentOnly_Bind0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-ToggleLoginBindingGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-VocResSelDialog0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-VocResSelDialogWithConfirm0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-AlertDialogWithResult0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-AlertDialogWithConfirm0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-_StartShowJudgeDialogCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-JudgeDialog0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-JudgeDialog1  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-MockLoginWithCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-MockManualSelectServer0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-IsLoginSceneLoading0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-_ExtractErrorInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-FetchRemoteConfigUtilSuc0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-DevOnlyCheckEnableLocalResMode0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-DevOnlyUseLocalResVersion0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-AssetLoader-_CheckDisposedBeforeLoad0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-FlowWithMaskMgr-_OnFlowFinished0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-FFlowCondition-IsTargetMatch0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-FlowWithMaskMgr-_FlowCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-LoginManager-FlowWithMaskMgr-StartFlow0  // const
  public    static  IFix.IDMAP0                     Beyond-VietnamLoginAgePanel-OnApplicationQuit0  // const
  public    static  IFix.IDMAP0                     Beyond-VietnamLoginAgeTipsPanelCtrl-_SetStateTips0  // const
  public    static  IFix.IDMAP0                     Beyond-VietnamLoginAgeTipsPanelCtrl-_SetStateCircle0  // const
  public    static  IFix.IDMAP0                     Beyond-VietnamLoginAgeTipsPanelCtrl-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-TailGameLoop-Update0  // const
  public    static  IFix.IDMAP0                     Beyond-TailGameLoop-LateUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-get_isPlaying0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-UI-SplashController-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-PlatformFont-GetFont0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-SDKNetUtils-HTTPPost0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-SDKNetUtils-HTTPGet0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-SDKNetUtils-BeyondHttp-DoHTTP0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-IsInited0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-NeedRetryInit0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-NeedQuitGame0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-_OnRequestExitGame0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-InitSDKSystem0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-RetryInitSDKSystem0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-WaitForInited0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-InitHGSDK0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-IsHGSDKInited0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-WaitForInitHGSDK0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-Log0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-LogError0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-LogWarning0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-GetSignKey0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-GetDeviceIDs0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-SetSubChannel0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-GetPlatformKey0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-CaptchaHandler-OnGT3Message0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-_TryGetGameProtocolUpdateCheckErrorCode0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-_ProcessGameProtocolUpdateCheck0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-_ProcessExtraInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-OnSDKExtraInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-U8RootUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-_ConvertToPOSTResult0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-POSTImplementation0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-_OnFakeAccountCenter0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-CreateCaptchaHandler0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-_ErrorMsg_GET_TOKEN0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-_ErrorMsg_CREATE_ORDER0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-GetErrorMessage0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8ExternalTools-CaptchaHandler-FetchCaptchaCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8Plugin-GetPayAddition0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8Plugin-GetSDKToken0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8Plugin-GetSDKUid0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8Plugin-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8Plugin-_UseMockVersion0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8Plugin-_MockLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8Plugin-Login0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8Plugin-Logout0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8Plugin-OnProductListChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8Plugin-Pay0  // const
  public    static  IFix.IDMAP0                     Beyond-SDK-Impl-U8Plugin-TryLoadSDKMeta0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCheckForUpdateNode-get_type0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCheckForUpdateNode-_AlertAndQuitForExtraConfigFatalError0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCheckForUpdateNode-AlertNoMatchDeviceBeforeLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCheckForUpdateNode-_GetRequestPram0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCheckForUpdateNode-_GetOnlineAppVersion0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-ShowWaitProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-GetWaitTime0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCheckForUpdateNode-_CheckForUpdatePC0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCheckForUpdateNode-_CheckForUpdateCoroutinePC0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-ClearWaitProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCheckForUpdateNode-_CheckForUpdateCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCheckForUpdateNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-_CreateDirectoryIfNotExists0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-Context-CompleteWithCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-Context-UncompleteWithCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-GetNoAlertCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-_GetLastGameInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-CheckLastGameInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-FWork-MarkFallback0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Fallback0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCheckForUpdateNode-_CheckForUpdateCoroutineMobile0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCreateRoleNode-get_type0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCreateRoleNode-WorkProcess-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCreateRoleNode-OnDispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCreateRoleNode-CanCancel0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCreateRoleNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginCreateRoleNode-WorkProcess-get_keepWaiting0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDownloadTask-GetDownloadProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDownloadTask-GetDecompressProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDownloadTask-GetWorkState0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDownloadTask-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDownloadTask-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDownloadTask-_GetFileList0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDownloadTask-_GetAllFileList0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-HasEnteredPreloadThisSession0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-NetUsagePolicy-AllowMobileDataInLastCheck0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDownloadTask-_OnDownloadFinish0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDownloadTask-_OnDownloadError0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-NetUsagePolicy-_GetPrettySizeStringUpToMB0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-NetUsagePolicy-CheckIfAllowDownload0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDownloadTask-_OnDownloadPaused0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDownloadTask-_StartDownloadImpl0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDownloadTask-_StartDownload0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameSettingInitializer-_TryInitializeComponents0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameSettingInitializer-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameSettingInitializer-_InitGameSettingNotchPadding0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameSettingInitializer-_InitGameSettingStateInLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameSettingInitializer-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdateNode-get_type0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdateNode-_GameUpdateCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdateNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-_YieldCheckNetUsagePolicy0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-_YieldErrorAlert0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-_OnDownloadStart0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-_OnDownloadProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-_DoTaskUpdating0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-_UpdateGame0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-_WaitForTaskCancel0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdater-DoUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGameUpdateNode-_GameUpdateCoroutineMobileImpl0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-get_type0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-IsInterruptable0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-TryInterrupt0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-_FallbackToInitNodeImpl0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-_LogoutToInitNode0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-_FallbackToInitWithMask0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-_ReLoginWhenErrorEncountered0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-_OnAuthError0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-_ConfirmServer0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-_MockManualSelectServer0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-_SaveCurrentUserToServerConfig0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-_NetConnectAndGSLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-_GlobalLoginSucBusiness0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-_EnterGameCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-EnterGame0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-_OnEventLogout0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginViewModel-RequestResourceRepair0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-_FallbackToResRepairImpl0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-_OnEventResourceRepair0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-OnEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-get_keepWaiting0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-MarkAborting0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-_ExtractErrorInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGSNode-EnterGameServiceFlow-_YieldAlertError0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdateModel-SimpleProgress-GetCurrent0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdateModel-SimpleProgress-GetTotal0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdateModel-SimpleProgress-GetInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-MarkInitialAssetChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-CheckInitialAssetChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-MarkNeedClearFolder0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-NeedClearFolder0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-MarkClearSuccessed0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-MarkSoftRestart0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-ClearSoftRestartFlag0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-get_keepWaiting0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginLocalResTask-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_UpdateResVersionDev0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_UpdateResVersionProd0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_UpdateResVersion0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_SetResVersionForLog0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_CurrentWorkMode0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_GetOnlineResVersion0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-WorkContext-GetHotUpdateInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-ResourceRootUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_ResourceIndexUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_LoadOrDownloadResIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_GenResIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_ResourcePatchUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_ResourceFileUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_LoadStreamingIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_GenRiff0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_ResourcePatchRootUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_DownloadPatchInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_CreateUpdatedLocalResPref0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_StartDownloadingShaderWarmupIfNeeded0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_EventLogDownloadProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-WriteLocalResPrefToFile0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_GenLocalRef0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_CheckAndDownloadResource0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_StopDownloadingShaderWarmupIfNeeded0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-HotUpdateInfo-CheckIfNeedReload0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-HotUpdateInfo-HasChangedByType0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-WorkContext-MarkIllegalItemsInLocalRes0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_CheckLocalResConsistency0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_ClearDownloadFolder0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-WorkContext-HasChangedByType0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_ReloadResourceIndexes0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_MainCoroutineModeDownload0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginLocalResTask-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_MainCoroutineModeLocal0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_MainCoroutineModeSkip0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_CreateMainCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginViewModel-RequestSkipU8Login0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_VitalError0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-_Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-StartHotUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginHotUpdater-HotUpdateInfo-HasAssetsChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginProgress-GetCurrent0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginProgress-GetTotal0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginProgress-FDownloadSize-_PickUnit0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginProgress-FDownloadSize-_DisplayWithUnit0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginProgress-FDownloadSize-_FormatDisplayInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginProgress-FDownloadSize-_FormatSpeedInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginProgress-FDownloadSize-_FormatProgressInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginProgress-FDownloadSize-SetSize0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginProgress-FDownloadSize-SetProgress0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginProgress-FDownloadSize-SetIdle0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginProgress-GetInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginInitNode-get_type0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginInitNode-_YieldAlertError0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginInitNode-_InitRemoteConfigs0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginInitNode-_InitCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginInitNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadNode-get_type0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-MarkEnteredPreloadThisSession0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadNode-_ShowPreloadResourcePanel0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadNode-_QuitGame0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadNode-_WarmUp0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadNode-_OnGetServerListSuc0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadNode-_GetServerListInfoMock0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadNode-_OnGetServerListFail0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadNode-_GetServerListInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadNode-_GetServerList0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadNode-_EnterPreLoad0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadNode-_LoginPreLoad0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSDKInitNode-get_type0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSDKInitNode-_WaitForSDKInited0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSDKInitNode-_InitSubSDKs0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSDKInitNode-_SDKInitCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSDKInitNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-get_type0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_EnableBlurMask0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_OnSDKViewDisabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnReceiveRedDotAndPopup0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_UpdateAnnounceRedDot0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnBulletinOpen0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnBulletinClose0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-InitBulletinAndRedAfterLoginSuc0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-InitBulletinAndRedAfterLoginSuc0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_OnLoginSuc0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_OnGameProtocolUpdateCheckSucceeded0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_OnLoginCanceled0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_SetCursorOnLoginStart0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_SetCursorOnLoginFinish0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_StartGameProtocolUpdateCheck0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_StartSDKLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_OnLoginFail0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_OnGameProtocolUpdateCheckFailed0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_ClearGameProtocolUpdateCheckEvents0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_ClearLoginCaches0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_SkipSDKLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_FinishLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-_LoginCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-U8LoginNode-OnEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginViewModel-FAgeTipsPolicy-get_display0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginViewModel-IsAgeTipsVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginViewModel-IsGovTipsVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginViewModel-IsMenuVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginViewModel-_LoadAgeTipsPolicy0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsModel-CanDisplay0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsProp-ClearContentCache0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsProp-UpdateContent0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-_RegisterNodes0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Node-CanCancel0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Worker-Cancel0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-CancelAndFallback0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-AddEventReceiver0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Worker-UpdateOnce0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-UpdateOnce0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Node-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginWorkFlow-Node-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-HotUpdateNode-get_type0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-HotUpdateNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-HotUpdateNode-OnDispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-WaitForSecondsNode-_NotifyTimeout0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-WaitForSecondsNode-_IsTimeout0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-WaitForSecondsNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-WaitForSecondsNode-_DisposeTimer0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-WaitForSecondsNode-OnDispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadPostDelayNode-get_type0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadPostDelayNode-WaitForSeconds0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadPostDelayNode-_DisableLoadingPanel0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-PreloadPostDelayNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-Trans2GsLoginNode-get_type0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-Trans2GsLoginNode-WaitForSeconds0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-Trans2GsLoginNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-FinishNode-get_type0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-FinishNode-Work0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-OnBindToManager0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-_EnsurePlayer0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-_RandomIntervalA1s0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-_ResetToA10  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-_PlayA1A2Impl0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-_TransitionToA20  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-_ChangeToState0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-_PlayA1sAndTriggerNext0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-OnValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneComponent-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneAnimCtrl-GetAnimationClips0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneComponent-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneRoot-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginSceneRoot-OnBindToManager0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsDetailPanel-_EventOnCloseClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsDetailPanel-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsDetailPanel-_SplitLongString0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsDetailPanel-_SplitLicenseToSegments0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsDetailPanel-_RenderContent0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsDetailPanel-_ShowContent0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsDetailPanel-OnValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsPanel-_EventOnOpenAgeTipsClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsPanel-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsPanel-_UpdateAgeTipsPolicy0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAgeTipsPanel-OnValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAlertDialog-_OnConfirmClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginAlertDialog-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDecorateUI-get_tickOption0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDecorateUI-_TriggerTrans2GSLoginImpl0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDecorateUI-OnValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDecorateUI-_TimeManagerTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDecorateUI-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginDecorateUI-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginEnterGamePanel-_OnEnterGameClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginEnterGamePanel-_InitIfNot0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-ChildPanel-SetPanelVisibleByFadeSwitchTween0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginEnterGamePanel-OnValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGovTipsPanel-_EventOnOpenAgeTipsClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGovTipsPanel-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginGovTipsPanel-OnValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginJudgeDialog-_OnPositiveClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginJudgeDialog-_OnNegativeClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginJudgeDialog-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnAnnounceClosed0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnAnnounceClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnSettingClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnResCheckClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnLogoutClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnQuitGameClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnLoginClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnQRClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnServerZoneBtnClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnFocusMenuGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_CanQRLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-OnValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_UpdateServerZone0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnRequestRelogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnInputChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnUpdateRedDotForSDKListener0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-ChildPanel-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnCustomServiceClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMenuPanel-_OnAccountCenterClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-get_instance0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-Refresh0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-_SmokeEnterGame0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginMockUIDPanel-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginResourcePanel-_UpdateResourceInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginResourcePanel-OnValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginResourcePanel-Update0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-_OnNotchPaddingChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginRootPanel-ChildPanel-SetPanelVisibleByAnimSwitchTween0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectItemView-_OnClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectItemView-RefreshView0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectItemView-NaviToBtnTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectItemView-SetSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectModel-get_curServerZoneData0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectModel-InitData0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectPanel-_OnConfirmClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectPanel-_OnCloseClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectPanel-_UpdateServerCell0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectPanel-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectPanel-_NaviToSelectedItem0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectPanel-_OnInputChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectPanel-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectPanel-_WaitingForShowListAwake0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectPanel-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectPanel-_UpdateServerSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginServerSelectPanel-OnValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVersionPanel-get_versionZoneType0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVersionPanel-get_versionPlatform0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVersionPanel-get_versionAppType0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVersionPanel-get_versionVersionNum0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVersionPanel-get_versionProjectCL0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVersionPanel-get_displayGameVersion0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVersionPanel-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-_OnConfirmClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-_OnConfirmClicked1  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelItemView-SetSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-_UpdateSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-_UpdateList0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-_RefreshConfirmInteractable0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-UpdateSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelItemView-_OnClicked0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelItemView-RefreshView0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-_UpdateCell0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-_ResolveInitialSelectedIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelItemView-NaviToBtnTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-_NaviToSelectedItem0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-_OnInputTypeChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-Login-LoginVocResSelDialog-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-WarmUpProgress-GetTotal0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-WarmUpProgress-GetCurrent0  // const
  public    static  IFix.IDMAP0                     Beyond-Scripts-Entry-WarmUpProgress-GetInfo0  // const
METHODS:
END_CLASS

