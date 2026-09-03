// ========================================================
// Dumped by @desirepro
// Assembly: Entry.Beyond.dll
// Classes:  286
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000002  // size: 0x30
    public class RenderSilhouetteRT : UnityEngine.MonoBehaviour
    {
        // Fields
        public UnityEngine.Camera cameraRT;  // 0x18
        public UnityEngine.Camera mainCamera;  // 0x20
        public UnityEngine.Material materialSilhouette;  // 0x28
        private static UnityEngine.Texture s_mSilhouetteRT;  // static @ 0x0
        private static UnityEngine.RenderTexture s_mSilhouetteRth;  // static @ 0x8

        // Methods
        // RVA: 0x03D76B40  token: 0x6000001
        private System.Void Update() { }
        // RVA: 0x0484FD10  token: 0x6000002
        private System.Void OnDestroy() { }
        // RVA: 0x05393520  token: 0x6000003
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x50
    public sealed struct WIN32_FIND_DATA
    {
        // Fields
        public System.UInt32 dwFileAttributes;  // 0x10
        public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;  // 0x14
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;  // 0x1c
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;  // 0x24
        public System.UInt32 nFileSizeHigh;  // 0x2c
        public System.UInt32 nFileSizeLow;  // 0x30
        public System.UInt32 dwReserved0;  // 0x34
        public System.UInt32 dwReserved1;  // 0x38
        public System.String cFileName;  // 0x40
        public System.String cAlternateFileName;  // 0x48

    }

    // TypeToken: 0x2000006  // size: 0x38
    public class FileEntry
    {
        // Fields
        public System.String name;  // 0x10
        public System.String fullPath;  // 0x18
        public System.Int64 size;  // 0x20
        public System.String md5;  // 0x28
        public System.Int32 pathLength;  // 0x30
        public System.Boolean readable;  // 0x34
        public System.Boolean writable;  // 0x35
        public System.Boolean readOnly;  // 0x36

        // Methods
        // RVA: 0x041E1670  token: 0x600003B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x28
    public sealed struct PendingFile
    {
        // Fields
        public System.String fullPath;  // 0x10
        public System.Int64 size;  // 0x18
        public System.Boolean readOnlyAttr;  // 0x20

    }

    // TypeToken: 0x2000008  // size: 0x30
    public class ScanContext
    {
        // Fields
        public readonly System.Collections.Concurrent.ConcurrentQueue<System.String> dirQueue;  // 0x10
        public readonly System.Collections.Concurrent.ConcurrentQueue<Beyond.GameApp.PendingFile> fileQueue;  // 0x18
        public readonly System.Collections.Concurrent.ConcurrentBag<Beyond.GameApp.FileEntry> results;  // 0x20
        public System.Int32 pendingDirs;  // 0x28
        public System.Int32 pendingFiles;  // 0x2c

        // Methods
        // RVA: 0x06993CD0  token: 0x600003C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x28
    public class TreeNode
    {
        // Fields
        public System.String name;  // 0x10
        public Beyond.GameApp.FileEntry fileEntry;  // 0x18
        public System.Collections.Generic.Dictionary<System.String,Beyond.GameApp.TreeNode> children;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x600003D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x28
    public sealed class <_DelayInit>d__14 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.GameApp <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600003E
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600003F
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x06994B48  token: 0x6000040
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994BF8  token: 0x6000042
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200000B  // size: 0x28
    public sealed class <_InitSecuritySDK>d__35 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.GameApp <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000044
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000045
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0340BDA0  token: 0x6000046
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994E0C  token: 0x6000048
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200000C  // size: 0x30
    public sealed class <_FetchRemoteConfigUtilSuc>d__37 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        private Beyond.Cfg.RemoteNetworkCfg <config>5__2;  // 0x20
        private Beyond.Misc.ObjectRef <resultRef>5__3;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600004A
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600004B
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03C09CD0  token: 0x600004C
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994D28  token: 0x600004E
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200000D  // size: 0x30
    public sealed class <>c__DisplayClass71_0
    {
        // Fields
        public System.String capturedRoot;  // 0x10
        public System.Int32 capturedThreads;  // 0x18
        public System.String capturedOutput;  // 0x20
        public System.String capturedSysInfo;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000050
        public System.Void .ctor() { }
        // RVA: 0x069943B4  token: 0x6000051
        private System.Void <LaunchScanCoordinator>b__0() { }

    }

    // TypeToken: 0x2000011  // size: 0x28
    public sealed class <_DoInit>d__5 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.GameInitState <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000061
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000062
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03C11C20  token: 0x6000063
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994C90  token: 0x6000065
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000015  // size: 0x28
    public sealed class <_DoPreload>d__6 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.GamePreloadState <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600007E
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600007F
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0340DD90  token: 0x6000080
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994CDC  token: 0x6000082
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000018  // size: 0x30
    public sealed class <_SoftRestart>d__5 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.GameSoftRestartState <>4__this;  // 0x20
        private UnityEngine.AsyncOperation <clearSceneOp>5__2;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000090
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000091
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x06994EF0  token: 0x6000092
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069957DC  token: 0x6000094
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200001C  // size: 0x28
    public sealed class <ReturnToLoginState>d__8 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.GameStateMachine <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000A9
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000AA
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03C12470  token: 0x60000AB
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699428C  token: 0x60000AD
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000023  // size: 0x18
    public sealed struct FOptions
    {
        // Fields
        public Beyond.LoginContext context;  // 0x10

    }

    // TypeToken: 0x2000025  // size: 0x30
    public sealed class <_CloseCoroutine>d__4 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.LoginController.Dialog <>4__this;  // 0x20
        public System.Action callback;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000DA
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000DB
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x06994A30  token: 0x60000DC
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994AFC  token: 0x60000DE
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000026
    public sealed class <Create>d__8`1 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private System.Object <>2__current;  // 0x0
        public Beyond.Misc.ObjectRef outRef;  // 0x0
        public Beyond.LoginController controller;  // 0x0
        public System.String resPath;  // 0x0

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000E0
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x60000E1
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x60000E2
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x60000E4
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000024  // size: 0x38
    public abstract class Dialog : UnityEngine.MonoBehaviour
    {
        // Fields
        private Beyond.UI.UISwitchTween m_switchTween;  // 0x18
        private Beyond.LoginController m_controller;  // 0x20
        public System.String showAudioEvent;  // 0x28
        public System.String closeAudioEvent;  // 0x30

        // Methods
        // RVA: 0x0698D340  token: 0x60000D4
        private System.Collections.IEnumerator _CloseCoroutine(System.Action callback) { }
        // RVA: 0x0698D1E4  token: 0x60000D5
        public System.Boolean TryCloseWithCallback(System.Action callback) { }
        // RVA: 0x0698D0F4  token: 0x60000D6
        public System.Void Show() { }
        // RVA: 0x0698D0B0  token: 0x60000D7
        protected virtual System.Void OnInit() { }
        // RVA: -1  // generic def  token: 0x60000D8
        public static System.Collections.IEnumerator Create(System.String resPath, Beyond.LoginController controller, Beyond.Misc.ObjectRef outRef) { }
        // RVA: 0x05393520  token: 0x60000D9
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000027  // size: 0x10
    public static class MaskConsts
    {
        // Fields
        public static System.Single DELAY_WAIT_NODE;  // const
        public static System.String INIT;  // const
        public static System.String U8_LOGIN;  // const
        public static System.String FROM_GS_LOGIN;  // const
        public static System.String FROM_CREATE_ROLE;  // const
        public static System.String NET_AGE_TIPS;  // const

    }

    // TypeToken: 0x2000028  // size: 0x14
    public sealed struct EMaskType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.LoginController.EMaskType None;  // const
        public static Beyond.LoginController.EMaskType Black;  // const
        public static Beyond.LoginController.EMaskType Blur;  // const
        public static Beyond.LoginController.EMaskType White;  // const

    }

    // TypeToken: 0x2000029  // size: 0x20
    public class MaskCfg
    {
        // Fields
        public UnityEngine.CanvasGroup target;  // 0x10
        public System.Single showDuration;  // 0x18
        public System.Single hideDuration;  // 0x1c

        // Methods
        // RVA: 0x03F0F380  token: 0x60000E6
        public Beyond.UI.UISwitchTween CreateTweenAndHide() { }
        // RVA: 0x04D8CC90  token: 0x60000E7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002B  // size: 0x20
    public class NetMaskRef
    {
        // Fields
        private System.String <key>k__BackingField;  // 0x10
        public System.Int32 maskId;  // 0x18
        public System.Boolean isEnabled;  // 0x1c

        // Properties
        System.String key { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x60000F4
        public System.Void .ctor(System.String key) { }

    }

    // TypeToken: 0x200002C  // size: 0x40
    public sealed class <WaitForCrossSceneMasks>d__10 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.LoginController.MaskCtrl <>4__this;  // 0x20
        private System.Int32[] <>7__wrap1;  // 0x28
        private System.Int32 <>7__wrap2;  // 0x30
        private Beyond.UI.UISwitchTween <mask>5__4;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000F5
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000F6
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069948BC  token: 0x60000F7
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069949E4  token: 0x60000F9
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200002A  // size: 0x28
    public class MaskCtrl : System.IDisposable
    {
        // Fields
        private static readonly System.Int32[] CROSS_SCENE_MASKS;  // static @ 0x0
        private Beyond.ListDict<System.Int32,System.Collections.Generic.HashSet<System.String>> m_maskRefs;  // 0x10
        private Beyond.ListDict<System.Int32,Beyond.UI.UISwitchTween> m_maskTweens;  // 0x18
        private Beyond.ListDict<System.String,Beyond.LoginController.MaskCtrl.NetMaskRef> m_netMasks;  // 0x20

        // Methods
        // RVA: 0x03F0F180  token: 0x60000E8
        public System.Void .ctor(Beyond.LoginController controller) { }
        // RVA: 0x03F0F600  token: 0x60000E9
        public virtual System.Void Dispose() { }
        // RVA: 0x041E77B0  token: 0x60000EA
        public System.Void EnableMask(System.String key, Beyond.LoginController.EMaskType type, System.Boolean enable, System.Boolean reset) { }
        // RVA: 0x041E7690  token: 0x60000EB
        public System.Boolean IsMaskTweening(Beyond.LoginController.EMaskType type) { }
        // RVA: 0x041E76E0  token: 0x60000EC
        public System.Boolean IsCrossSceneMaskTweening() { }
        // RVA: 0x06993BB8  token: 0x60000ED
        public System.Collections.IEnumerator WaitForCrossSceneMasks() { }
        // RVA: 0x069939E0  token: 0x60000EE
        public System.Void EnableNetMask(System.String key, System.Single timeoutSecs) { }
        // RVA: 0x069938F4  token: 0x60000EF
        public System.Void DisableNetMask(System.String key) { }
        // RVA: 0x041E7990  token: 0x60000F0
        private Beyond.UI.UISwitchTween _GetMaskByType(System.Int32 type) { }
        // RVA: 0x04CE79C0  token: 0x60000F1
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002D  // size: 0x28
    public sealed class <BeforeDestroy>d__35 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.LoginController <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000FB
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000FC
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x041E5D70  token: 0x60000FD
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069941A8  token: 0x60000FF
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200002E
    public sealed class <_InstantiateUIFromRes>d__36`1 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private System.Object <>2__current;  // 0x0
        public Beyond.Misc.ObjectRef objRef;  // 0x0
        public Beyond.LoginController <>4__this;  // 0x0
        public System.String resPath;  // 0x0
        public UnityEngine.Transform parent;  // 0x0
        private System.Nullable<Beyond.Resource.FAssetProxyHandle> <handle>5__2;  // 0x0

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000101
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000102
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000103
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000105
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200002F  // size: 0x38
    public sealed class <_InitCoroutine>d__38 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.LoginController <>4__this;  // 0x20
        private Beyond.LoginContext <context>5__2;  // 0x28
        private Beyond.Misc.ObjectRef <panelRef>5__3;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000107
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000108
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x041E5520  token: 0x6000109
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994DC0  token: 0x600010B
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000030  // size: 0x18
    public sealed class <>c__DisplayClass45_0
    {
        // Fields
        public Beyond.Misc.ObjectRef objRef;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600010D
        public System.Void .ctor() { }
        // RVA: 0x03C8D270  token: 0x600010E
        private System.Void <_MockLoginCoroutine>b__0(System.String pUID) { }

    }

    // TypeToken: 0x2000031  // size: 0x40
    public sealed class <_MockLoginCoroutine>d__45 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.LoginController <>4__this;  // 0x20
        private Beyond.LoginController.<>c__DisplayClass45_0 <>8__1;  // 0x28
        public System.String fixedUID;  // 0x30
        private Beyond.Login.LoginMockUIDPanel <nullablePanel>5__2;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600010F
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000110
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03F0E1B0  token: 0x6000111
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994EA4  token: 0x6000113
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000033  // size: 0x30
    public sealed class <_DisposeCoroutine>d__48 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.LoginController legacyController;  // 0x20
        public System.Action disposeAction;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000133
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000134
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x04A22250  token: 0x6000135
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994C44  token: 0x6000137
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000037  // size: 0x18
    public class AssetLoader : System.IDisposable
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.String,Beyond.Resource.FAssetProxyHandle> m_assetHandles;  // 0x10

        // Methods
        // RVA: -1  // generic def  token: 0x600015C
        public Beyond.Resource.FAssetProxyHandle CachedLoad(System.String path) { }
        // RVA: -1  // generic def  token: 0x600015D
        public Beyond.Resource.FAssetProxyHandle CachedLoadAsync(System.String path) { }
        // RVA: 0x04701C90  token: 0x600015E
        private System.Boolean _CheckDisposedBeforeLoad(System.String path) { }
        // RVA: 0x048E3130  token: 0x600015F
        public virtual System.Void Dispose() { }
        // RVA: 0x0380AA10  token: 0x6000160
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000039  // size: 0x60
    public sealed class <_FlowCoroutine>d__4 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.LoginManager.FlowWithMaskMgr <>4__this;  // 0x20
        public Beyond.LoginManager.FFlowWithMask flow;  // 0x28
        private Beyond.LoginController.MaskCtrl <maskCtrl>5__2;  // 0x50
        private System.Boolean <isFlowSuc>5__3;  // 0x58

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000165
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000166
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x041E61D0  token: 0x6000167
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994D74  token: 0x6000169
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000038  // size: 0x20
    public class FlowWithMaskMgr
    {
        // Fields
        private Beyond.LoginContext m_context;  // 0x10
        private System.Collections.Generic.HashSet<System.String> m_activeFlows;  // 0x18

        // Methods
        // RVA: 0x0380A5D0  token: 0x6000161
        public System.Void .ctor(Beyond.LoginContext context) { }
        // RVA: 0x03A80DD0  token: 0x6000162
        public System.Void StartFlow(Beyond.LoginManager.FFlowWithMask flow) { }
        // RVA: 0x03A80F10  token: 0x6000163
        private System.Collections.IEnumerator _FlowCoroutine(Beyond.LoginManager.FFlowWithMask flow) { }
        // RVA: 0x0698D3E8  token: 0x6000164
        private System.Void _OnFlowFinished(Beyond.LoginManager.FFlowWithMask flow) { }

    }

    // TypeToken: 0x200003A  // size: 0x38
    public sealed struct FFlowWithMask
    {
        // Fields
        public System.String key;  // 0x10
        public Beyond.LoginController.EMaskType maskType;  // 0x18
        public System.Func<System.Boolean> flowAction;  // 0x20
        public Beyond.LoginManager.FFlowCondition condition;  // 0x28
        public System.Single disableDelay;  // 0x30

    }

    // TypeToken: 0x200003B  // size: 0x18
    public sealed struct FFlowCondition
    {
        // Fields
        private Beyond.Login.LoginWorkFlow.ENode <priTarget>k__BackingField;  // 0x10
        private Beyond.Login.LoginWorkFlow.ENode <secTarget>k__BackingField;  // 0x14

        // Properties
        Beyond.Login.LoginWorkFlow.ENode priTarget { get; /* RVA: 0x02B77620 */ set; /* RVA: 0x04D86140 */ }
        Beyond.Login.LoginWorkFlow.ENode secTarget { get; /* RVA: 0x04D88B20 */ set; /* RVA: 0x04D88B50 */ }

        // Methods
        // RVA: 0x04D8CC80  token: 0x600016F
        public System.Void .ctor(Beyond.Login.LoginWorkFlow.ENode target) { }
        // RVA: 0x04D85C40  token: 0x6000170
        public System.Void .ctor(Beyond.Login.LoginWorkFlow.ENode first, Beyond.Login.LoginWorkFlow.ENode second) { }
        // RVA: 0x041E7630  token: 0x6000171
        public System.Boolean IsTargetMatch(Beyond.Login.LoginWorkFlow.ENode node) { }

    }

    // TypeToken: 0x200003C  // size: 0x30
    public sealed class <InitCoroutine>d__17 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.LoginManager <>4__this;  // 0x20
        public System.Boolean quickStart;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000172
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000173
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x04D30A10  token: 0x6000174
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994240  token: 0x6000176
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200003D  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.LoginManager.<>c <>9;  // static @ 0x0
        public static System.Action<Beyond.Login.LoginAlertDialog,Beyond.Login.LoginAlertDialog.FParams> <>9__29_0;  // static @ 0x8
        public static System.Action<Beyond.Login.LoginVocResSelDialog,Beyond.Login.LoginVocResSelDialog.FParams> <>9__30_0;  // static @ 0x10
        public static System.Action<Beyond.Login.LoginJudgeDialog,Beyond.Login.LoginJudgeDialog.FParams> <>9__36_0;  // static @ 0x18

        // Methods
        // RVA: 0x069946D0  token: 0x6000178
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000179
        public System.Void .ctor() { }
        // RVA: 0x069942D8  token: 0x600017A
        private System.Void <AlertDialog>b__29_0(Beyond.Login.LoginAlertDialog param1, Beyond.Login.LoginAlertDialog.FParams param2) { }
        // RVA: 0x06994308  token: 0x600017B
        private System.Void <VocResSelDialog>b__30_0(Beyond.Login.LoginVocResSelDialog param1, Beyond.Login.LoginVocResSelDialog.FParams param2) { }
        // RVA: 0x06994330  token: 0x600017C
        private System.Void <_StartShowJudgeDialogCoroutine>b__36_0(Beyond.Login.LoginJudgeDialog param1, Beyond.Login.LoginJudgeDialog.FParams param2) { }

    }

    // TypeToken: 0x200003E  // size: 0x18
    public sealed class <>c__DisplayClass31_0
    {
        // Fields
        public System.Object wait;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600017D
        public System.Void .ctor() { }
        // RVA: 0x040E20A0  token: 0x600017E
        private System.Void <VocResSelDialogWithConfirm>b__0() { }

    }

    // TypeToken: 0x200003F  // size: 0x30
    public sealed class <VocResSelDialogWithConfirm>d__31 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.LoginManager <>4__this;  // 0x20
        private Beyond.LoginManager.<>c__DisplayClass31_0 <>8__1;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600017F
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000180
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x06994734  token: 0x6000181
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994870  token: 0x6000183
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000040  // size: 0x20
    public sealed class <>c__DisplayClass32_0
    {
        // Fields
        public Beyond.Misc.ObjectRef outRef;  // 0x10
        public System.Action<Beyond.Misc.ObjectRef> callback;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000185
        public System.Void .ctor() { }
        // RVA: 0x06994374  token: 0x6000186
        private System.Void <AlertDialogWithResult>b__0() { }

    }

    // TypeToken: 0x2000041  // size: 0x40
    public sealed class <AlertDialogWithResult>d__32 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Misc.ObjectRef outRef;  // 0x20
        public System.Action<Beyond.Misc.ObjectRef> callback;  // 0x28
        public Beyond.LoginManager <>4__this;  // 0x30
        public System.String desc;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000187
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000188
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x06994004  token: 0x6000189
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699415C  token: 0x600018B
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000042  // size: 0x18
    public sealed class <>c__DisplayClass33_0
    {
        // Fields
        public System.Object wait;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600018D
        public System.Void .ctor() { }
        // RVA: 0x040E20A0  token: 0x600018E
        private System.Void <AlertDialogWithConfirm>b__0() { }

    }

    // TypeToken: 0x2000043  // size: 0x38
    public sealed class <AlertDialogWithConfirm>d__33 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.LoginManager <>4__this;  // 0x20
        public System.String desc;  // 0x28
        private Beyond.LoginManager.<>c__DisplayClass33_0 <>8__1;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600018F
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000190
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x06993E68  token: 0x6000191
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06993FB8  token: 0x6000193
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000044
    public sealed class <_ShowDialogCoroutine>d__37`2 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private System.Object <>2__current;  // 0x0
        public Beyond.LoginManager <>4__this;  // 0x0
        public System.String resPath;  // 0x0
        public System.Action<TDialog,TParam> setParamFunc;  // 0x0
        public TParam param;  // 0x0
        private Beyond.Misc.ObjectRef <outRef>5__2;  // 0x0

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000195
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000196
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000197
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000199
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000045  // size: 0x18
    public sealed class <>c__DisplayClass39_0
    {
        // Fields
        public System.Action callback;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600019B
        public System.Void .ctor() { }
        // RVA: 0x02A729E0  token: 0x600019C
        private System.Void <MockManualSelectServer>b__0(System.String _) { }

    }

    // TypeToken: 0x2000046  // size: 0x20
    public sealed class <>c__DisplayClass40_0
    {
        // Fields
        public Beyond.LoginController controller;  // 0x10
        public Beyond.LoginManager <>4__this;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600019D
        public System.Void .ctor() { }
        // RVA: 0x04CA7390  token: 0x600019E
        private System.Void <_Init>b__0() { }

    }

    // TypeToken: 0x2000047  // size: 0x30
    public sealed class <_LoginWorkFlowCoroutine>d__43 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.LoginManager <>4__this;  // 0x20
        public Beyond.LoginController nullableCtrl;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600019F
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60001A0
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x041E4CB0  token: 0x60001A1
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06994E58  token: 0x60001A3
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000048  // size: 0x90
    public sealed class <FetchRemoteConfigUtilSuc>d__45 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Cfg.IRemoteConfig config;  // 0x20
        public System.String configEnv;  // 0x28
        public System.String keyErrorWithCode;  // 0x30
        public System.String keyTimeout;  // 0x38
        private Beyond.Cfg.FRemoteConfigRequest <request>5__2;  // 0x40
        private System.Boolean <isServiceSuc>5__3;  // 0x80
        private Beyond.Misc.ObjectRef <resultRef>5__4;  // 0x88

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60001A5
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60001A6
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03C0A020  token: 0x60001A7
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069941F4  token: 0x60001A9
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000053  // size: 0x20
    public sealed struct SplashConfig
    {
        // Fields
        public UnityEngine.Sprite sprite;  // 0x10
        public System.Boolean blackCoverFadeOut;  // 0x18

    }

    // TypeToken: 0x2000054  // size: 0x20
    public sealed struct SplashConfigOverride
    {
        // Fields
        public Beyond.GEnums.EnvLang envLang;  // 0x10
        public Beyond.UI.SplashController.SplashConfig[] splashConfigs;  // 0x18

    }

    // TypeToken: 0x2000055  // size: 0x28
    public sealed struct SplashConfigs
    {
        // Fields
        public Beyond.UI.SplashController.SplashConfig[] splashConfigs;  // 0x10
        public Beyond.UI.SplashController.SplashConfig[] splashConfigsVNMobile;  // 0x18
        public Beyond.UI.SplashController.SplashConfigOverride[] i18nSplashConfigOverrides;  // 0x20

    }

    // TypeToken: 0x2000056  // size: 0x28
    public sealed class <>c__DisplayClass31_0
    {
        // Fields
        public Beyond.UI.SplashController.SplashConfig config;  // 0x10
        public Beyond.UI.SplashController <>4__this;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x60001D2
        public System.Void .ctor() { }
        // RVA: 0x04D25EF0  token: 0x60001D3
        private System.Void <_DoStartSplash>b__0() { }
        // RVA: 0x04CF4D60  token: 0x60001D4
        private System.Void <_DoStartSplash>b__2() { }

    }

    // TypeToken: 0x2000057  // size: 0x28
    public sealed class <_OnSplashCompleteCoroutine>d__33 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.UI.SplashController <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60001D5
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60001D6
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03811690  token: 0x60001D7
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699DCF0  token: 0x60001D9
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000058  // size: 0x28
    public sealed class <_StartLicenseCoroutine>d__34 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        private System.Boolean <displayCursor>5__2;  // 0x20
        private System.Boolean <usingController>5__3;  // 0x21
        private System.Boolean <isGranted>5__4;  // 0x22

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60001DB
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60001DC
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03A72270  token: 0x60001DD
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699DD88  token: 0x60001DF
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200005B  // size: 0x38
    public sealed struct HttpPostRequest
    {
        // Fields
        public System.String url;  // 0x10
        public System.String param;  // 0x18
        public System.String contentType;  // 0x20
        public System.Collections.Generic.Dictionary<System.String,System.String> header;  // 0x28
        public System.Action<Beyond.SDK.SDKNetUtils.WebHttpResponse> callback;  // 0x30

    }

    // TypeToken: 0x200005C  // size: 0x20
    public sealed struct HttpGetRequest
    {
        // Fields
        public System.String url;  // 0x10
        public System.Action<Beyond.SDK.SDKNetUtils.WebHttpResponse> callback;  // 0x18

    }

    // TypeToken: 0x200005D  // size: 0x40
    public sealed struct WebHttpResponse
    {
        // Fields
        public System.Boolean isTimeout;  // 0x10
        public System.Boolean isError;  // 0x11
        public System.Int64 responseCode;  // 0x18
        public System.Collections.Generic.Dictionary<System.String,System.String> header;  // 0x20
        public System.String text;  // 0x28
        public System.Byte[] data;  // 0x30
        public System.String error;  // 0x38

    }

    // TypeToken: 0x200005E  // size: 0x14
    public sealed struct HTTPOptType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.SDK.SDKNetUtils.HTTPOptType GET;  // const
        public static Beyond.SDK.SDKNetUtils.HTTPOptType POST;  // const

    }

    // TypeToken: 0x200005F  // size: 0x40
    public sealed struct HTTPConfig
    {
        // Fields
        public System.String url;  // 0x10
        public System.String body;  // 0x18
        public System.String contentType;  // 0x20
        public System.Collections.Generic.Dictionary<System.String,System.String> header;  // 0x28
        public Beyond.SDK.SDKNetUtils.HTTPOptType optType;  // 0x30
        public System.Action<Beyond.SDK.SDKNetUtils.WebHttpResponse> callback;  // 0x38

    }

    // TypeToken: 0x2000060  // size: 0x10
    public abstract class HTTPImpl
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001E6
        public virtual System.Collections.IEnumerator DoHTTP(Beyond.SDK.SDKNetUtils.HTTPConfig config) { }
        // RVA: 0x041E1670  token: 0x60001E7
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000062  // size: 0x58
    public sealed class <DoHTTP>d__0 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.SDK.SDKNetUtils.HTTPConfig config;  // 0x20
        private Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result> <sendTask>5__2;  // 0x50

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60001EA
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60001EB
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699BDC0  token: 0x60001EC
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699C05C  token: 0x60001EE
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000061  // size: 0x10
    public class BeyondHttp : HTTPImpl
    {
        // Methods
        // RVA: 0x069964CC  token: 0x60001E8
        public virtual System.Collections.IEnumerator DoHTTP(Beyond.SDK.SDKNetUtils.HTTPConfig config) { }
        // RVA: 0x041E1670  token: 0x60001E9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000065  // size: 0x12
    public sealed struct FHGSDKInit
    {
        // Fields
        public System.Boolean isIniting;  // 0x10
        public System.Boolean isInited;  // 0x11

    }

    // TypeToken: 0x2000067  // size: 0x38
    public sealed class <FetchCaptchaCoroutine>d__3 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.String captchaParams;  // 0x20
        public Beyond.SDK.Impl.U8ExternalTools.CaptchaHandler <>4__this;  // 0x28
        public U8.SDK.SDKCaptchaHandler.Result outResult;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000215
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000216
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699C400  token: 0x6000217
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699C4F4  token: 0x6000219
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000066  // size: 0x20
    public class CaptchaHandler : U8.SDK.SDKCaptchaHandler
    {
        // Fields
        private static System.Int32 FAILED;  // const
        private System.Int64 m_flag;  // 0x10
        private System.String m_desc;  // 0x18

        // Methods
        // RVA: 0x06996594  token: 0x6000212
        public virtual System.Collections.IEnumerator FetchCaptchaCoroutine(System.String captchaParams, U8.SDK.SDKCaptchaHandler.Result outResult) { }
        // RVA: 0x0699665C  token: 0x6000213
        public System.Void OnGT3Message(System.Collections.Generic.Dictionary<System.String,System.Object> msg) { }
        // RVA: 0x04D8CCA0  token: 0x6000214
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000068  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.SDK.Impl.U8ExternalTools.<>c <>9;  // static @ 0x0
        public static System.Action<System.Object> <>9__9_0;  // static @ 0x8
        public static System.Action<System.String> <>9__9_1;  // static @ 0x10
        public static System.Action <>9__27_0;  // static @ 0x18

        // Methods
        // RVA: 0x04D4AF50  token: 0x600021B
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600021C
        public System.Void .ctor() { }
        // RVA: 0x0699C540  token: 0x600021D
        private System.Void <InitSDKSystem>b__9_0(System.Object error) { }
        // RVA: 0x035A38E0  token: 0x600021E
        private System.Void <InitSDKSystem>b__9_1(System.String rawExtConfig) { }
        // RVA: 0x0699C70C  token: 0x600021F
        private System.Void <_ProcessExtraInfo>b__27_0() { }

    }

    // TypeToken: 0x2000069  // size: 0x20
    public sealed class <WaitForInited>d__10 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000220
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000221
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x04B4A840  token: 0x6000222
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699C94C  token: 0x6000224
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200006A  // size: 0x20
    public sealed class <WaitForInitHGSDK>d__15 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000226
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000227
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x04CD0580  token: 0x6000228
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699C900  token: 0x600022A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200006B  // size: 0x18
    public sealed class <>c__DisplayClass25_0
    {
        // Fields
        public System.Action<U8.SDK.SDKExternalTools.POSTResult> callback;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600022C
        public System.Void .ctor() { }
        // RVA: 0x0699C7C0  token: 0x600022D
        private System.Void <POSTImplementation>b__0(Beyond.SDK.SDKNetUtils.WebHttpResponse webRequest) { }

    }

    // TypeToken: 0x200006D  // size: 0x30
    public sealed class <>c__DisplayClass7_0
    {
        // Fields
        public U8.SDK.ExternalPluginPayParams args;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600023A
        public System.Void .ctor() { }
        // RVA: 0x0699C844  token: 0x600023B
        private System.Void <Pay>b__0(U8.SDK.U8PayResult payResult) { }
        // RVA: 0x0699C87C  token: 0x600023C
        private System.Void <Pay>b__1(System.Object failMsg) { }

    }

    // TypeToken: 0x200006E  // size: 0x38
    public sealed class <>c__DisplayClass10_0
    {
        // Fields
        public U8.SDK.ExternalPluginLoginParams args;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600023D
        public System.Void .ctor() { }
        // RVA: 0x03074E90  token: 0x600023E
        private System.Void <_MockLogin>b__0(System.String uid) { }

    }

    // TypeToken: 0x2000070  // size: 0x18
    public class AppVersionRespone
    {
        // Fields
        public System.Int32 action;  // 0x10
        public System.Int32 state;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x600024A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000071  // size: 0x28
    public sealed class <_CheckForUpdateCoroutine>d__4 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginCheckForUpdateNode <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600024B
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600024C
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0340A370  token: 0x600024D
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699C998  token: 0x600024F
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000072  // size: 0x20
    public sealed class <_AlertAndQuitForExtraConfigFatalError>d__5 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000251
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000252
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x014F7CD0  token: 0x6000253
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x014F90C0  token: 0x6000255
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000073  // size: 0x20
    public sealed class <AlertNoMatchDeviceBeforeLogin>d__6 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000257
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000258
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x014F60E0  token: 0x6000259
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x014F7C80  token: 0x600025B
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000074  // size: 0x30
    public sealed class <_CheckForUpdateCoroutineMobile>d__7 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginCheckForUpdateNode <>4__this;  // 0x20
        private Beyond.Login.LoginGameUpdater.Result <result>5__2;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600025D
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600025E
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x014F9110  token: 0x600025F
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x014FA7A0  token: 0x6000261
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000075  // size: 0x28
    public sealed class <_CheckForUpdateCoroutinePC>d__9 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginCheckForUpdateNode <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000263
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000264
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x014FA7F0  token: 0x6000265
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x014FAF80  token: 0x6000267
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000076  // size: 0x38
    public sealed class <_CheckForUpdatePC>d__10 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginCheckForUpdateNode <>4__this;  // 0x20
        private Beyond.Misc.ObjectRef <actionRef>5__2;  // 0x28
        private System.String <errorInfo>5__3;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000269
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600026A
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x014FAFD0  token: 0x600026B
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x014FD090  token: 0x600026D
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000077  // size: 0x40
    public sealed class <_GetOnlineAppVersion>d__12 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginCheckForUpdateNode <>4__this;  // 0x20
        public Beyond.Misc.ObjectRef outRef;  // 0x28
        private System.String <url>5__2;  // 0x30
        private Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result<Beyond.Login.LoginCheckForUpdateNode.AppVersionRespone>> <httpResIndex>5__3;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600026F
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000270
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x014FD0E0  token: 0x6000271
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x014FF2C0  token: 0x6000273
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000079  // size: 0x20
    public class WorkProcess : UnityEngine.CustomYieldInstruction, System.IDisposable
    {
        // Fields
        private Beyond.Login.LoginCreateRoleNode m_closure;  // 0x10
        private System.Boolean m_isFinished;  // 0x18

        // Properties
        System.Boolean keepWaiting { get; /* RVA: 0x069A00CC */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600027C
        public System.Void .ctor(Beyond.Login.LoginCreateRoleNode closure) { }
        // RVA: 0x069A0058  token: 0x600027E
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x200007B  // size: 0x20
    public sealed class <>c__DisplayClass24_0
    {
        // Fields
        public Beyond.Login.LoginDownloadTask <>4__this;  // 0x10
        public Beyond.SDK.HGDownloader.PauseReason reason;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600028E
        public System.Void .ctor() { }
        // RVA: 0x0699C798  token: 0x600028F
        private System.Void <_OnDownloadPaused>b__1() { }

    }

    // TypeToken: 0x200007C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.Login.LoginDownloadTask.<>c <>9;  // static @ 0x0
        public static System.Action <>9__24_0;  // static @ 0x8

        // Methods
        // RVA: 0x0699C89C  token: 0x6000290
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000291
        public System.Void .ctor() { }
        // RVA: 0x0699C6E8  token: 0x6000292
        private System.Void <_OnDownloadPaused>b__24_0() { }

    }

    // TypeToken: 0x200007F  // size: 0x20
    public sealed class <_GameUpdateCoroutine>d__5 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600029E
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600029F
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x04D83CA0  token: 0x60002A0
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699D2E4  token: 0x60002A2
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000080  // size: 0x30
    public sealed class <_GameUpdateCoroutineMobileImpl>d__6 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginGameUpdateNode <>4__this;  // 0x20
        private Beyond.Login.LoginGameUpdater.Result <result>5__2;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60002A4
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60002A5
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699D0BC  token: 0x60002A6
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699D298  token: 0x60002A8
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000082  // size: 0x14
    public sealed struct CodeType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Login.LoginGameUpdater.CodeType CODE_2103;  // const
        public static Beyond.Login.LoginGameUpdater.CodeType CODE_2105;  // const
        public static Beyond.Login.LoginGameUpdater.CodeType CODE_2200;  // const
        public static Beyond.Login.LoginGameUpdater.CodeType CODE_2201;  // const
        public static Beyond.Login.LoginGameUpdater.CodeType CODE_2202;  // const
        public static Beyond.Login.LoginGameUpdater.CodeType CODE_2203;  // const
        public static Beyond.Login.LoginGameUpdater.CodeType CODE_2204;  // const
        public static Beyond.Login.LoginGameUpdater.CodeType CODE_2205;  // const
        public static Beyond.Login.LoginGameUpdater.CodeType CODE_2206;  // const
        public static Beyond.Login.LoginGameUpdater.CodeType CODE_2207;  // const
        public static Beyond.Login.LoginGameUpdater.CodeType CODE_2208;  // const
        public static Beyond.Login.LoginGameUpdater.CodeType CODE_2209;  // const

    }

    // TypeToken: 0x2000083  // size: 0x14
    public sealed struct UpdateType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Login.LoginGameUpdater.UpdateType FullUpdate;  // const
        public static Beyond.Login.LoginGameUpdater.UpdateType IncrementalUpdate;  // const
        public static Beyond.Login.LoginGameUpdater.UpdateType GoToBilibile;  // const
        public static Beyond.Login.LoginGameUpdater.UpdateType GoToTaptap;  // const
        public static Beyond.Login.LoginGameUpdater.UpdateType GoToAppStore;  // const
        public static Beyond.Login.LoginGameUpdater.UpdateType GoToGooglePlay;  // const
        public static Beyond.Login.LoginGameUpdater.UpdateType GoToSamsungStore;  // const
        public static Beyond.Login.LoginGameUpdater.UpdateType GoToRustore;  // const

    }

    // TypeToken: 0x2000084  // size: 0x14
    public sealed struct UpdateCode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Login.LoginGameUpdater.UpdateCode Success;  // const

    }

    // TypeToken: 0x2000085  // size: 0x14
    public sealed struct UpdateAction
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Login.LoginGameUpdater.UpdateAction Invalid;  // const
        public static Beyond.Login.LoginGameUpdater.UpdateAction Latest;  // const
        public static Beyond.Login.LoginGameUpdater.UpdateAction ForcedUpdate;  // const
        public static Beyond.Login.LoginGameUpdater.UpdateAction OptionalUpdate;  // const
        public static Beyond.Login.LoginGameUpdater.UpdateAction Error;  // const
        public static Beyond.Login.LoginGameUpdater.UpdateAction Congestion;  // const

    }

    // TypeToken: 0x2000086  // size: 0x38
    public class HGLatestGameInfo
    {
        // Fields
        public System.Int32 code;  // 0x10
        public System.String version;  // 0x18
        public System.Int32 action;  // 0x20
        public System.Int32 updateType;  // 0x24
        public System.String updateInfo;  // 0x28
        public System.Int32 state;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x60002BB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000087  // size: 0x20
    public class NotificationTitleConfig
    {
        // Fields
        public System.String downloading;  // 0x10
        public System.String pause;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60002BC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000088  // size: 0x18
    public class HGUpdateConfig
    {
        // Fields
        public System.String downloadPath;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60002BD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000089  // size: 0x28
    public sealed struct Options
    {
        // Fields
        public Beyond.Login.NetUsagePolicy netUsagePolicy;  // 0x10
        public System.Action<System.String,System.Action> alertDialog;  // 0x18
        public Beyond.LoginContext loginContext;  // 0x20

    }

    // TypeToken: 0x200008A  // size: 0x28
    public class Result
    {
        // Fields
        public System.Boolean needRetry;  // 0x10
        public System.String errorInfo;  // 0x18
        public System.Int32 code;  // 0x20
        public System.Boolean auditing;  // 0x24

        // Methods
        // RVA: 0x041E1670  token: 0x60002BE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008B  // size: 0x40
    public class Context
    {
        // Fields
        public System.Int64 taskId;  // 0x10
        public System.String errorInfo;  // 0x18
        public System.String logError;  // 0x20
        public System.Boolean needClear;  // 0x28
        public Beyond.Login.LoginGameUpdater.HGLatestGameInfo gameInfo;  // 0x30
        private System.Boolean <complete>k__BackingField;  // 0x38
        private System.Int32 <code>k__BackingField;  // 0x3c

        // Properties
        System.Boolean complete { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        System.Int32 code { get; /* RVA: 0x04D86640 */ set; /* RVA: 0x04D86670 */ }

        // Methods
        // RVA: 0x06996750  token: 0x60002C3
        public System.Void CompleteWithCode(System.Int32 pCode) { }
        // RVA: 0x069967AC  token: 0x60002C4
        public System.Void UncompleteWithCode(System.Int32 pCode) { }
        // RVA: 0x06996808  token: 0x60002C5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008C  // size: 0x30
    public sealed class <CheckLastGameInfo>d__23 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginGameUpdater <>4__this;  // 0x20
        public Beyond.Login.LoginGameUpdater.Result result;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60002C6
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60002C7
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699BB30  token: 0x60002C8
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699BD74  token: 0x60002CA
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200008D  // size: 0x30
    public sealed class <DoUpdate>d__24 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginGameUpdater <>4__this;  // 0x20
        public Beyond.Login.LoginGameUpdater.Result result;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60002CC
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60002CD
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699C0A8  token: 0x60002CE
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699C3B4  token: 0x60002D0
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200008E  // size: 0x30
    public sealed class <_GetLastGameInfo>d__25 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginGameUpdater <>4__this;  // 0x20
        public Beyond.Login.LoginGameUpdater.Context context;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60002D2
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60002D3
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699D330  token: 0x60002D4
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699D9A8  token: 0x60002D6
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200008F  // size: 0x48
    public sealed class <_UpdateGame>d__26 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginGameUpdater <>4__this;  // 0x20
        public Beyond.Login.LoginGameUpdater.Context context;  // 0x28
        private Beyond.Login.LoginGameUpdater.HGLatestGameInfo <gameInfo>5__2;  // 0x30
        private System.Int64 <size>5__3;  // 0x38
        private System.Boolean <enableMobileData>5__4;  // 0x40

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60002D8
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60002D9
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699DDD4  token: 0x60002DA
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699E4A4  token: 0x60002DC
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000090  // size: 0x58
    public sealed class <_DoTaskUpdating>d__27 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginGameUpdater <>4__this;  // 0x20
        public Beyond.Login.LoginGameUpdater.Context context;  // 0x28
        private System.Int64 <taskId>5__2;  // 0x30
        private System.Int32 <status>5__3;  // 0x38
        private System.Int64 <curSize>5__4;  // 0x40
        private System.Int64 <totalSize>5__5;  // 0x48
        private System.Int64 <speed>5__6;  // 0x50

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60002DE
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60002DF
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699CA30  token: 0x60002E0
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699D024  token: 0x60002E2
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000091  // size: 0x18
    public sealed class <>c__DisplayClass32_0
    {
        // Fields
        public System.Boolean wait;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60002E4
        public System.Void .ctor() { }
        // RVA: 0x04D8CCB0  token: 0x60002E5
        private System.Void <_YieldErrorAlert>b__0() { }

    }

    // TypeToken: 0x2000092  // size: 0x38
    public sealed class <_YieldErrorAlert>d__32 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginGameUpdater <>4__this;  // 0x20
        public System.String desc;  // 0x28
        private Beyond.Login.LoginGameUpdater.<>c__DisplayClass32_0 <>8__1;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60002E6
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60002E7
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699E99C  token: 0x60002E8
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699EAD8  token: 0x60002EA
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000093  // size: 0x18
    public sealed class <>c__DisplayClass33_0
    {
        // Fields
        public System.Boolean rejected;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60002EC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000094  // size: 0x20
    public sealed class <>c__DisplayClass33_1
    {
        // Fields
        public System.Boolean complete;  // 0x10
        public Beyond.Login.LoginGameUpdater.<>c__DisplayClass33_0 CS$<>8__locals1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60002ED
        public System.Void .ctor() { }
        // RVA: 0x015EFCD0  token: 0x60002EE
        private System.Void <_YieldCheckNetUsagePolicy>b__0() { }
        // RVA: 0x0699C824  token: 0x60002EF
        private System.Void <_YieldCheckNetUsagePolicy>b__1() { }
        // RVA: 0x015EFCD0  token: 0x60002F0
        private System.Void <_YieldCheckNetUsagePolicy>b__2() { }

    }

    // TypeToken: 0x2000095  // size: 0x40
    public sealed class <_YieldCheckNetUsagePolicy>d__33 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        private Beyond.Login.LoginGameUpdater.<>c__DisplayClass33_0 <>8__1;  // 0x20
        public Beyond.Login.LoginGameUpdater <>4__this;  // 0x28
        public System.Int64 downloadSize;  // 0x30
        private Beyond.Login.LoginGameUpdater.<>c__DisplayClass33_1 <>8__2;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60002F1
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60002F2
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699E6E4  token: 0x60002F3
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699E950  token: 0x60002F5
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000096  // size: 0x38
    public sealed class <_WaitForTaskCancel>d__34 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginGameUpdater <>4__this;  // 0x20
        public System.Int64 taskId;  // 0x28
        private System.Boolean <isCanceled>5__2;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60002F7
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60002F8
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699E4F0  token: 0x60002F9
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699E570  token: 0x60002FB
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000099  // size: 0x48
    public sealed class <_EnterGameCoroutine>d__11 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginGSNode.EnterGameServiceFlow <>4__this;  // 0x20
        private U8.SDK.SDKPromiseEnumerator<U8.SDK.U8AuthV2Result> <authPromise>5__2;  // 0x28
        private System.String <token>5__3;  // 0x30
        private System.String <uid>5__4;  // 0x38
        private Beyond.Misc.ObjectRef <loginRespRef>5__5;  // 0x40

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600031B
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600031C
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03A853B0  token: 0x600031D
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699D070  token: 0x600031F
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200009A  // size: 0x58
    public sealed class <_NetConnectAndGSLogin>d__12 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Misc.ObjectRef loginRespRef;  // 0x20
        public Beyond.Login.LoginGSNode.EnterGameServiceFlow <>4__this;  // 0x28
        public System.String uid;  // 0x30
        public System.String token;  // 0x38
        private Beyond.Network.HGNetSessionConnectYield <connectTask>5__2;  // 0x40
        private System.Int32 <maxRetryCount>5__3;  // 0x48
        private System.Int32 <currentRetry>5__4;  // 0x4c
        private Beyond.Network.HGNetSessionLoginYield <loginHandler>5__5;  // 0x50

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000321
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000322
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03A4B510  token: 0x6000323
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699DB98  token: 0x6000325
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200009B  // size: 0x28
    public sealed class <_YieldAlertError>d__14 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.String errorInfo;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000327
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000328
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699E608  token: 0x6000329
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699E698  token: 0x600032B
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200009C  // size: 0x30
    public sealed class <_SaveCurrentUserToServerConfig>d__15 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginGSNode.EnterGameServiceFlow <>4__this;  // 0x20
        public System.String uid;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600032D
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600032E
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03DA3230  token: 0x600032F
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699DD3C  token: 0x6000331
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200009D  // size: 0x18
    public sealed class <>c__DisplayClass16_0
    {
        // Fields
        public Beyond.Misc.ObjectRef waitForCallback;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000333
        public System.Void .ctor() { }
        // RVA: 0x0699C774  token: 0x6000334
        private System.Void <_MockManualSelectServer>b__0() { }

    }

    // TypeToken: 0x200009E  // size: 0x28
    public sealed class <_MockManualSelectServer>d__16 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.String uid;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000335
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000336
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699D9F4  token: 0x6000337
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699DB4C  token: 0x6000339
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200009F  // size: 0x30
    public sealed class <_OnAuthError>d__17 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Object rejectInfo;  // 0x20
        public Beyond.Login.LoginGSNode.EnterGameServiceFlow <>4__this;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600033B
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600033C
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0699DBE4  token: 0x600033D
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699DCA4  token: 0x600033F
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000098  // size: 0x30
    public class EnterGameServiceFlow : UnityEngine.CustomYieldInstruction
    {
        // Fields
        private Beyond.LoginContext m_context;  // 0x10
        private Beyond.Login.LoginGSNode m_curNode;  // 0x18
        private Beyond.Misc.ObjectRef m_dialogBlocker;  // 0x20
        private System.Int32 m_state;  // 0x28

        // Properties
        System.Boolean keepWaiting { get; /* RVA: 0x0404E110 */ }

        // Methods
        // RVA: 0x049D6330  token: 0x600030C
        public System.Void .ctor(Beyond.Login.LoginGSNode node) { }
        // RVA: 0x069968D0  token: 0x600030D
        public System.Void MarkAborting() { }
        // RVA: 0x03A84270  token: 0x600030E
        public System.Void EnterGame() { }
        // RVA: 0x06996870  token: 0x600030F
        public System.Boolean IsInterruptable() { }
        // RVA: 0x06996928  token: 0x6000310
        public System.Boolean TryInterrupt() { }
        // RVA: 0x03A84390  token: 0x6000311
        private System.Collections.IEnumerator _EnterGameCoroutine() { }
        // RVA: 0x03A84400  token: 0x6000312
        private System.Collections.IEnumerator _NetConnectAndGSLogin(System.String uid, System.String token, Beyond.Misc.ObjectRef loginRespRef) { }
        // RVA: 0x06996990  token: 0x6000313
        private static System.String _ExtractErrorInfo(Beyond.Network.HttpSender.Result result, System.String keyErrorWithCode, System.String keyTimeout) { }
        // RVA: 0x06996C34  token: 0x6000314
        private System.Collections.IEnumerator _YieldAlertError(System.String errorInfo) { }
        // RVA: 0x03A4B930  token: 0x6000315
        private System.Collections.IEnumerator _SaveCurrentUserToServerConfig(System.String uid) { }
        // RVA: 0x06996A8C  token: 0x6000316
        private System.Collections.IEnumerator _MockManualSelectServer(System.String uid) { }
        // RVA: 0x06996B28  token: 0x6000317
        private System.Collections.IEnumerator _OnAuthError(System.Object rejectInfo) { }
        // RVA: 0x06996BD0  token: 0x6000318
        private System.Void _ReLoginWhenErrorEncountered() { }
        // RVA: 0x040EB370  token: 0x6000319
        private static System.Void _GlobalLoginSucBusiness(Proto.MSG_B1 msg) { }
        // RVA: 0x04D84FC0  token: 0x600031A
        private System.Boolean <_EnterGameCoroutine>b__11_0() { }

    }

    // TypeToken: 0x20000A0  // size: 0x28
    public sealed class <_ConfirmServer>d__10 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        private U8.SDK.SDKPromiseEnumerator<U8.SDK.U8ConfirmServerResult> <confirmPromise>5__2;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000341
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000342
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x040E9A00  token: 0x6000343
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699C9E4  token: 0x6000345
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000A3
    public interface IProgress
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000349
        public virtual System.Double GetTotal() { }
        // RVA: -1  // abstract  token: 0x600034A
        public virtual System.Double GetCurrent() { }
        // RVA: -1  // abstract  token: 0x600034B
        public virtual System.String GetInfo() { }

    }

    // TypeToken: 0x20000A4  // size: 0x18
    public class SimpleProgress : IProgress
    {
        // Fields
        public System.Double current;  // 0x10

        // Methods
        // RVA: 0x039367C0  token: 0x600034C
        public virtual System.Double GetCurrent() { }
        // RVA: 0x039369B0  token: 0x600034D
        public virtual System.Double GetTotal() { }
        // RVA: 0x039369E0  token: 0x600034E
        public virtual System.String GetInfo() { }
        // RVA: 0x041E1670  token: 0x600034F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A6  // size: 0x58
    public class HotUpdateInfo
    {
        // Fields
        public Beyond.Resource.HotUpdate.FResourceDiff resDiff;  // 0x10
        public Beyond.Resource.HotUpdate.LocalResPref localResPref;  // 0x50

        // Methods
        // RVA: 0x06996CD0  token: 0x600037C
        public System.Boolean CheckIfNeedReload() { }
        // RVA: 0x03DA4D90  token: 0x600037D
        public System.Boolean HasChangedByType(Beyond.VFS.EVFSBlockType type) { }
        // RVA: 0x06996E7C  token: 0x600037E
        public System.Boolean HasAssetsChanged() { }
        // RVA: 0x041E1670  token: 0x600037F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A7  // size: 0x48
    public class WorkContext
    {
        // Fields
        public Beyond.Login.LoginHotUpdater.HotUpdateInfo updateInitial;  // 0x10
        public Beyond.Login.LoginHotUpdater.HotUpdateInfo updateMain;  // 0x18
        public System.String vitalError;  // 0x20
        public System.Boolean reboot;  // 0x28
        public Beyond.Login.NetUsagePolicy netUsagePolicy;  // 0x30
        public Beyond.Resource.HotUpdate.ResourceIndex resIndex;  // 0x38
        private System.Boolean <illegalItemsInLocalRes>k__BackingField;  // 0x40

        // Properties
        System.Boolean illegalItemsInLocalRes { get; /* RVA: 0x02D480A0 */ set; /* RVA: 0x02D480B0 */ }

        // Methods
        // RVA: 0x069A0008  token: 0x6000382
        public System.Void MarkIllegalItemsInLocalRes() { }
        // RVA: 0x0699FFA4  token: 0x6000383
        public Beyond.Login.LoginHotUpdater.HotUpdateInfo GetHotUpdateInfo(System.Boolean isInitial) { }
        // RVA: 0x03DA4D20  token: 0x6000384
        public System.Boolean HasChangedByType(Beyond.VFS.EVFSBlockType type) { }
        // RVA: 0x03C075E0  token: 0x6000385
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A8  // size: 0x30
    public sealed class <_Work>d__28 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x20
        private Beyond.Login.LoginHotUpdater.WorkContext <workContext>5__2;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000386
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000387
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03C07330  token: 0x6000388
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0699E5BC  token: 0x600038A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000A9  // size: 0x30
    public sealed class <_CreateMainCoroutine>d__29 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x20
        public Beyond.Login.LoginHotUpdater.WorkContext workContext;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600038C
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600038D
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0455F6A0  token: 0x600038E
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A6914  token: 0x6000390
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000AA  // size: 0x30
    public sealed class <_MainCoroutineModeSkip>d__30 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x20
        public Beyond.Login.LoginHotUpdater.WorkContext context;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000392
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000393
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x04B21A80  token: 0x6000394
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069AA11C  token: 0x6000396
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000AB  // size: 0x30
    public sealed class <_MainCoroutineModeLocal>d__31 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x20
        public Beyond.Login.LoginHotUpdater.WorkContext context;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000398
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000399
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A9F90  token: 0x600039A
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069AA0D0  token: 0x600039C
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000AC  // size: 0x30
    public sealed class <_UpdateResVersion>d__32 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x20
        public Beyond.Login.LoginHotUpdater.WorkContext workContext;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600039E
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600039F
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x04B45BC0  token: 0x60003A0
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069AA388  token: 0x60003A2
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000AD  // size: 0x28
    public sealed class <_UpdateResVersionDev>d__36 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        private Beyond.Cfg.ResVersionDevCfg <config>5__2;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003A4
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003A5
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069AA200  token: 0x60003A6
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069AA2F0  token: 0x60003A8
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000AE  // size: 0x40
    public sealed class <_UpdateResVersionProd>d__38 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater.WorkContext workContext;  // 0x20
        private Beyond.Cfg.ResVersionCfg <config>5__2;  // 0x28
        private Beyond.Misc.ObjectRef <resultRef>5__3;  // 0x30
        private System.String <url>5__4;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003AA
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003AB
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03C07B40  token: 0x60003AC
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069AA33C  token: 0x60003AE
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000AF  // size: 0x18
    public sealed class <>c__DisplayClass43_0
    {
        // Fields
        public System.Boolean isInitial;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003B0
        public System.Void .ctor() { }
        // RVA: 0x069A2ACC  token: 0x60003B1
        private Beyond.Resource.HotUpdate.LocalResPref <_MainCoroutineModeDownload>b__0() { }

    }

    // TypeToken: 0x20000B0  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.Login.LoginHotUpdater.<>c <>9;  // static @ 0x0
        public static System.Action <>9__43_1;  // static @ 0x8

        // Methods
        // RVA: 0x069A2C00  token: 0x60003B2
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60003B3
        public System.Void .ctor() { }
        // RVA: 0x069A2640  token: 0x60003B4
        private System.Void <_MainCoroutineModeDownload>b__43_1() { }

    }

    // TypeToken: 0x20000B1  // size: 0x58
    public sealed class <_MainCoroutineModeDownload>d__43 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x20
        public Beyond.Login.LoginHotUpdater.WorkContext workContext;  // 0x28
        private Beyond.Login.LoginHotUpdater.<>c__DisplayClass43_0 <>8__1;  // 0x30
        private System.Boolean <initialDownloaded>5__2;  // 0x38
        private System.Boolean <hasHotUpdateSucceeded>5__3;  // 0x39
        private System.Boolean <needDeleteInitial>5__4;  // 0x3a
        private System.Boolean <neewDeleteMain>5__5;  // 0x3b
        private System.Int32 <initialOrMain>5__6;  // 0x3c
        private System.String <onlineResVersion>5__7;  // 0x40
        private Beyond.WaitForAsyncTask<Beyond.Resource.HotUpdate.LocalResPref> <waitForResPref>5__8;  // 0x48
        private Beyond.Login.LoginHotUpdater.HotUpdateInfo <info>5__9;  // 0x50

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003B5
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003B6
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A89CC  token: 0x60003B7
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A9F44  token: 0x60003B9
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000B2  // size: 0x28
    public sealed class <_ReloadResourceIndexes>d__46 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater.WorkContext workContext;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003BB
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003BC
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03DA6720  token: 0x60003BD
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069AA168  token: 0x60003BF
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000B3  // size: 0x28
    public sealed class <>c__DisplayClass47_0
    {
        // Fields
        public System.String filePath;  // 0x10
        public Beyond.Resource.HotUpdate.ResourceIndex resIndex;  // 0x18
        public System.Func<Beyond.Resource.HotUpdate.ResourceIndex> <>9__1;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x60003C1
        public System.Void .ctor() { }
        // RVA: 0x069A2B7C  token: 0x60003C2
        private Beyond.Resource.HotUpdate.ResourceIndex <_LoadOrDownloadResIndex>b__1() { }
        // RVA: 0x069A2B40  token: 0x60003C3
        private System.Object <_LoadOrDownloadResIndex>b__0() { }

    }

    // TypeToken: 0x20000B4  // size: 0x60
    public sealed class <_LoadOrDownloadResIndex>d__47 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Boolean isInitial;  // 0x20
        private Beyond.Login.LoginHotUpdater.<>c__DisplayClass47_0 <>8__1;  // 0x28
        public System.String onlineVer;  // 0x30
        public Beyond.Misc.ObjectRef resIndexRef;  // 0x38
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x40
        private System.String <indexUrl>5__2;  // 0x48
        private Beyond.Misc.ObjectRef <resultRef>5__3;  // 0x50
        private Beyond.WaitForAsyncTask<Beyond.Resource.HotUpdate.ResourceIndex> <loadTask>5__4;  // 0x58

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003C4
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003C5
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A7E48  token: 0x60003C6
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A848C  token: 0x60003C8
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000B5  // size: 0x48
    public sealed class <_GenResIndex>d__48 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x20
        public System.String onlineVer;  // 0x28
        public System.Boolean isInitial;  // 0x30
        public Beyond.Login.LoginHotUpdater.WorkContext workContext;  // 0x38
        private Beyond.Misc.ObjectRef <resIndexRef>5__2;  // 0x40

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003CA
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003CB
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A715C  token: 0x60003CC
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A7298  token: 0x60003CE
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000B6  // size: 0x78
    public sealed class <_GenRiff>d__49 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater.WorkContext workContext;  // 0x20
        public System.Boolean isInitial;  // 0x28
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x30
        public System.String onlineVer;  // 0x38
        private Beyond.Login.LoginHotUpdater.HotUpdateInfo <info>5__2;  // 0x40
        private Beyond.Resource.HotUpdate.LocalResPref <localResPref>5__3;  // 0x48
        private System.String <rootUrl>5__4;  // 0x50
        private System.String <patchUrl>5__5;  // 0x58
        private System.String <fileUrl>5__6;  // 0x60
        private Beyond.Resource.HotUpdate.ResourceIndex <resIndex>5__7;  // 0x68
        private Beyond.Misc.ObjectRef <streamingIndexRef>5__8;  // 0x70

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003D0
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003D1
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A72E4  token: 0x60003D2
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A7698  token: 0x60003D4
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000B7  // size: 0x38
    public sealed class <_GenLocalRef>d__51 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater.WorkContext workContext;  // 0x20
        public System.Boolean isInitial;  // 0x28
        public System.String onlineVer;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003D6
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003D7
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A6FB0  token: 0x60003D8
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A7110  token: 0x60003DA
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000B8  // size: 0x78
    public sealed class <_CheckAndDownloadResource>d__52 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater.WorkContext workContext;  // 0x20
        public System.Boolean isInitial;  // 0x28
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x30
        public System.String onlineVer;  // 0x38
        private Beyond.Login.LoginHotUpdater.HotUpdateInfo <info>5__2;  // 0x40
        private Beyond.Login.LoginHotUpdateModel <hotUpdateModel>5__3;  // 0x48
        private System.String <patchInfo>5__4;  // 0x50
        private Beyond.Login.LoginDownloadTask <downloadTask>5__5;  // 0x58
        private Beyond.SDK.HGDownloader.WorkState <lastWorkState>5__6;  // 0x60
        private System.Boolean <progress5>5__7;  // 0x64
        private System.Boolean <progress25>5__8;  // 0x65
        private System.Boolean <progress50>5__9;  // 0x66
        private System.Boolean <progress75>5__10;  // 0x67
        private System.Boolean <progress90>5__11;  // 0x68
        private System.Boolean <downloadingShaderWarmupStarted>5__12;  // 0x69
        private Beyond.Misc.ObjectRef <patchInfoRef>5__13;  // 0x70

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003DC
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003DD
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A3C84  token: 0x60003DE
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A50E4  token: 0x60003E0
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000B9  // size: 0x50
    public sealed class <_DownloadPatchInfo>d__54 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x20
        public System.String onlineVer;  // 0x28
        public System.Boolean isInitial;  // 0x30
        public Beyond.Misc.ObjectRef patchInfoRef;  // 0x38
        private System.String <patchInfoUrl>5__2;  // 0x40
        private Beyond.WaitForAsyncTask<Beyond.Network.HttpSender.Result<Beyond.Resource.HotUpdate.PatchInfo>> <httpResIndex>5__3;  // 0x48

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003E2
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003E3
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A6960  token: 0x60003E4
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A6C38  token: 0x60003E6
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000BA  // size: 0x58
    public sealed class <_ClearDownloadFolder>d__55 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x20
        public System.Boolean needDeleteInitial;  // 0x28
        public System.Boolean needDeleteMain;  // 0x29
        public Beyond.Login.LoginHotUpdater.WorkContext workContext;  // 0x30
        private System.Collections.Generic.List<Beyond.SDK.HGFolder> <folderList>5__2;  // 0x38
        private System.Int32 <initialOrMain>5__3;  // 0x40
        private System.Boolean <isInitial>5__4;  // 0x44
        private System.String <onlineVer>5__5;  // 0x48
        private Beyond.Login.LoginHotUpdater.HotUpdateInfo <info>5__6;  // 0x50

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003E8
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003E9
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A5C38  token: 0x60003EA
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A68C8  token: 0x60003EC
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000BB  // size: 0x68
    public sealed class <_CheckLocalResConsistency>d__56 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater.WorkContext context;  // 0x20
        public System.Boolean isInitial;  // 0x28
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x30
        public System.String onlineVer;  // 0x38
        private Beyond.Resource.HotUpdate.LocalResPref <localResInfo>5__2;  // 0x40
        private Beyond.Misc.ObjectRef <resIndexRef>5__3;  // 0x48
        private System.Boolean <showProgress>5__4;  // 0x50
        private Beyond.WaitForAsyncTask<System.Object> <checkTask>5__5;  // 0x58
        private Beyond.Resource.HotUpdate.LocalResChecker.CheckOutput <checkOutput>5__6;  // 0x60

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003EE
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003EF
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A5130  token: 0x60003F0
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A5BEC  token: 0x60003F2
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000BC  // size: 0x30
    public sealed class <_VitalError>d__57 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginHotUpdater <>4__this;  // 0x20
        public System.String error;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003F4
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003F5
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069AA3D4  token: 0x60003F6
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069AA528  token: 0x60003F8
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000BD  // size: 0x18
    public sealed class <>c__DisplayClass58_0
    {
        // Fields
        public Beyond.RuntimeFileUtils.StreamingResult indexRet;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003FA
        public System.Void .ctor() { }
        // RVA: 0x069A2BDC  token: 0x60003FB
        private Beyond.Resource.HotUpdate.ResourceIndex <_LoadStreamingIndex>b__0() { }

    }

    // TypeToken: 0x20000BE  // size: 0x50
    public sealed class <_LoadStreamingIndex>d__58 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Boolean isInitial;  // 0x20
        private Beyond.Login.LoginHotUpdater.<>c__DisplayClass58_0 <>8__1;  // 0x28
        public Beyond.Misc.ObjectRef resultRef;  // 0x30
        public Beyond.Login.LoginHotUpdater.WorkContext workContext;  // 0x38
        private System.String <indexPath>5__2;  // 0x40
        private Beyond.WaitForAsyncTask<Beyond.Resource.HotUpdate.ResourceIndex> <waitForIndex>5__3;  // 0x48

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003FC
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003FD
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A84D8  token: 0x60003FE
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A8934  token: 0x6000400
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000C1  // size: 0x14
    public sealed struct EUnit
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Login.LoginProgress.FDownloadSize.EUnit NONE;  // const
        public static Beyond.Login.LoginProgress.FDownloadSize.EUnit B;  // const
        public static Beyond.Login.LoginProgress.FDownloadSize.EUnit KB;  // const
        public static Beyond.Login.LoginProgress.FDownloadSize.EUnit MB;  // const

    }

    // TypeToken: 0x20000C0  // size: 0x30
    public sealed struct FDownloadSize
    {
        // Fields
        private static System.Double MB;  // const
        private static System.Double KB;  // const
        private Beyond.Login.LoginProgress.FDownloadSize.EUnit m_unit;  // 0x10
        private System.Single m_displayCurrent;  // 0x14
        private System.Single m_displayTotal;  // 0x18
        private System.Single m_displaySpeed;  // 0x1c
        public System.String displayInfo;  // 0x20
        private System.Single m_current;  // 0x28
        private System.Single m_total;  // 0x2c

        // Methods
        // RVA: 0x069A08D4  token: 0x6000406
        public System.Void SetProgress(System.Double current, System.Double total) { }
        // RVA: 0x069A0790  token: 0x6000407
        public System.Void SetIdle() { }
        // RVA: 0x069A0A60  token: 0x6000408
        public System.Void SetSize(System.Double current, System.Double total, System.Double speed) { }
        // RVA: 0x069A1094  token: 0x6000409
        private static Beyond.Login.LoginProgress.FDownloadSize.EUnit _PickUnit(System.Double number) { }
        // RVA: 0x069A0CF0  token: 0x600040A
        private static System.Single _DisplayWithUnit(System.Double number, Beyond.Login.LoginProgress.FDownloadSize.EUnit unit) { }
        // RVA: 0x069A0DC0  token: 0x600040B
        private static System.String _FormatDisplayInfo(System.Single current, System.Single total, Beyond.Login.LoginProgress.FDownloadSize.EUnit unit) { }
        // RVA: 0x069A0FA4  token: 0x600040C
        private static System.String _FormatSpeedInfo(System.Single speed, Beyond.Login.LoginProgress.FDownloadSize.EUnit unit) { }
        // RVA: 0x069A0EF4  token: 0x600040D
        private static System.String _FormatProgressInfo(System.Single current, System.Single total) { }

    }

    // TypeToken: 0x20000C3  // size: 0x28
    public sealed class <_InitCoroutine>d__4 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginInitNode <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000414
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000415
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0484D630  token: 0x6000416
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A7D64  token: 0x6000418
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000C4  // size: 0x30
    public sealed class <_InitRemoteConfigs>d__5 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginInitNode <>4__this;  // 0x20
        private Beyond.Network.NetConfigInterface.InitYiedInstruction <initNetConfig>5__2;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600041A
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600041B
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03DA38E0  token: 0x600041C
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A7DB0  token: 0x600041E
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000C5  // size: 0x28
    public sealed class <_YieldAlertError>d__6 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.String errorInfo;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000420
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000421
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069AA60C  token: 0x6000422
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069AA69C  token: 0x6000424
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000C7  // size: 0x20
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.String indexPath;  // 0x10
        public System.String prefPath;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000429
        public System.Void .ctor() { }
        // RVA: 0x069A2A4C  token: 0x600042A
        private Beyond.Resource.HotUpdate.ResourceIndex <Work>b__0() { }
        // RVA: 0x069A2A9C  token: 0x600042B
        private Beyond.Resource.HotUpdate.LocalResPref <Work>b__1() { }

    }

    // TypeToken: 0x20000C8  // size: 0x80
    public sealed class <Work>d__3 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginLocalResTask <>4__this;  // 0x20
        private Beyond.Login.LoginLocalResTask.<>c__DisplayClass3_0 <>8__1;  // 0x28
        private Beyond.Login.LoginHotUpdater.WorkContext <workContext>5__2;  // 0x30
        private Beyond.Login.LoginHotUpdateProp <viewProp>5__3;  // 0x38
        private Beyond.Login.LoginHotUpdateModel <viewModel>5__4;  // 0x40
        private Beyond.Resource.HotUpdate.ResourceIndex[] <resIndexList>5__5;  // 0x48
        private System.Int32 <initialOrMain>5__6;  // 0x50
        private System.Boolean <isInitial>5__7;  // 0x54
        private Beyond.WaitForAsyncTask<Beyond.Resource.HotUpdate.ResourceIndex> <waitForResIndex>5__8;  // 0x58
        private Beyond.Resource.RootCategory <rootCategory>5__9;  // 0x60
        private Beyond.Login.LoginHotUpdateModel.SimpleProgress <progress>5__10;  // 0x68
        private Beyond.WaitForAsyncTask<System.Object> <checkTask>5__11;  // 0x70
        private Beyond.Resource.HotUpdate.LocalResChecker.CheckOutput <checkOutput>5__12;  // 0x78

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600042C
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600042D
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A2CB0  token: 0x600042E
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A3BEC  token: 0x6000430
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000CA  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.Login.PreloadNode.<>c <>9;  // static @ 0x0
        public static System.Func<System.Boolean> <>9__5_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D4D2F0  token: 0x600043F
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000440
        public System.Void .ctor() { }
        // RVA: 0x036A61A0  token: 0x6000441
        private System.Boolean <Work>b__5_0() { }

    }

    // TypeToken: 0x20000CB  // size: 0x28
    public sealed class <_LoginPreLoad>d__7 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.PreloadNode <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000442
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000443
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0455F3A0  token: 0x6000444
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A8980  token: 0x6000446
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000CC  // size: 0x28
    public sealed class <_WarmUp>d__9 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.PreloadNode <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000448
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000449
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03C8F5F0  token: 0x600044A
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069AA5C0  token: 0x600044C
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000CD  // size: 0x28
    public sealed class <_GetServerList>d__10 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.PreloadNode <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600044E
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600044F
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x040EACC0  token: 0x6000450
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A7D18  token: 0x6000452
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000CE  // size: 0x28
    public sealed class <_GetServerListInfoMock>d__12 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.PreloadNode <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000454
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000455
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x040E9740  token: 0x6000456
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A76E4  token: 0x6000458
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000CF  // size: 0x30
    public sealed class <_GetServerListInfo>d__13 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.PreloadNode <>4__this;  // 0x20
        private U8.SDK.SDKPromiseEnumerator<System.Collections.Generic.List<U8.SDK.U8ServerInfo>> <getServerListPromise>5__2;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600045A
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600045B
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A7730  token: 0x600045C
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A7CCC  token: 0x600045E
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000D0  // size: 0x28
    public sealed class <>c__DisplayClass14_0
    {
        // Fields
        public System.Collections.Generic.List<Beyond.Gameplay.ServerZoneManager.ServerZoneData> serverZoneDataList;  // 0x10
        public System.Collections.Generic.List<Hypergryph.SpeedTest.SpeedTestServerInfo> serverList;  // 0x18
        public System.Action<Hypergryph.SpeedTest.SpeedTestResult> callback;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000460
        public System.Void .ctor() { }
        // RVA: 0x069A2698  token: 0x6000461
        private System.Void <_OnGetServerListSuc>b__0(Hypergryph.SpeedTest.SpeedTestResult result) { }
        // RVA: 0x069A29E4  token: 0x6000462
        private System.Threading.Tasks.Task<Hypergryph.SpeedTest.TaskResult> <_OnGetServerListSuc>b__1() { }

    }

    // TypeToken: 0x20000D2  // size: 0x30
    public sealed class <_SDKInitCoroutine>d__4 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginSDKInitNode <>4__this;  // 0x20
        private System.String <env>5__2;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000469
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600046A
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03C8FD10  token: 0x600046B
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069AA1B4  token: 0x600046D
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000D3  // size: 0x20
    public sealed class <_WaitForSDKInited>d__5 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600046F
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000470
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x04A426B0  token: 0x6000471
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069AA574  token: 0x6000473
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000D4  // size: 0x28
    public sealed class <_InitSubSDKs>d__6 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.String env;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000475
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000476
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0332A190  token: 0x6000477
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A7DFC  token: 0x6000479
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000D6  // size: 0x28
    public sealed class <_LoginCoroutine>d__7 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.U8LoginNode <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000492
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000493
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x015043A0  token: 0x6000494
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x01505930  token: 0x6000496
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000D7  // size: 0x30
    public sealed class <_FinishLogin>d__8 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        private System.String <env>5__2;  // 0x20
        private U8.SDK.SDKPromiseEnumerator<U8.SDK.U8AuthV2Result> <authPromise>5__3;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000498
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000499
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x01502380  token: 0x600049A
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x01504350  token: 0x600049C
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000DC  // size: 0x13
    public sealed struct FAgeTipsPolicy
    {
        // Fields
        public System.Boolean canOpenDetail;  // 0x10
        public System.Boolean displayCN;  // 0x11
        public System.Boolean displayKR;  // 0x12

        // Properties
        System.Boolean display { get; /* RVA: 0x03408B80 */ }

    }

    // TypeToken: 0x20000E1  // size: 0x20
    public abstract class Node
    {
        // Fields
        private Beyond.LoginContext <context>k__BackingField;  // 0x10
        private Beyond.Login.LoginWorkFlow <workflow>k__BackingField;  // 0x18

        // Properties
        Beyond.LoginContext context { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        Beyond.Login.LoginWorkFlow workflow { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: 0x03405FE0  token: 0x60004E3
        public System.Void Init(Beyond.LoginContext context, Beyond.Login.LoginWorkFlow workflow) { }
        // RVA: 0x034060C0  token: 0x60004E4
        public System.Void Dispose() { }
        // RVA: -1  // abstract  token: 0x60004E6
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x069A2380  token: 0x60004E7
        public virtual System.Boolean CanCancel() { }
        // RVA: 0x069A23CC  token: 0x60004E8
        public virtual UnityEngine.CustomYieldInstruction Cancel() { }
        // RVA: 0x03406040  token: 0x60004E9
        protected virtual System.Void OnInit() { }
        // RVA: 0x046DD140  token: 0x60004EA
        public virtual System.Void OnDispose() { }
        // RVA: 0x0470B370  token: 0x60004EB
        public virtual System.Boolean OnEvent(Beyond.ELoginEvent evt, System.Object param) { }
        // RVA: 0x041E1670  token: 0x60004EC
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000E2  // size: 0x14
    public sealed struct ENode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Login.LoginWorkFlow.ENode None;  // const
        public static Beyond.Login.LoginWorkFlow.ENode CheckForUpdate;  // const
        public static Beyond.Login.LoginWorkFlow.ENode SDKInit;  // const
        public static Beyond.Login.LoginWorkFlow.ENode GameUpdate;  // const
        public static Beyond.Login.LoginWorkFlow.ENode Init;  // const
        public static Beyond.Login.LoginWorkFlow.ENode U8Login;  // const
        public static Beyond.Login.LoginWorkFlow.ENode HotUpdate;  // const
        public static Beyond.Login.LoginWorkFlow.ENode ResCheck;  // const
        public static Beyond.Login.LoginWorkFlow.ENode Preload;  // const
        public static Beyond.Login.LoginWorkFlow.ENode PreloadPostDelay;  // const
        public static Beyond.Login.LoginWorkFlow.ENode Trans2GsLogin;  // const
        public static Beyond.Login.LoginWorkFlow.ENode GsLogin;  // const
        public static Beyond.Login.LoginWorkFlow.ENode CreateRole;  // const
        public static Beyond.Login.LoginWorkFlow.ENode Finish;  // const

    }

    // TypeToken: 0x20000E3
    public interface IWorkerUpdateOnce
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60004ED
        public virtual System.Void Worker_UpdateOnce() { }

    }

    // TypeToken: 0x20000E4
    public interface IEventReceiver
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60004EE
        public virtual System.Void OnEvent(Beyond.ELoginEvent evt, System.Object param) { }

    }

    // TypeToken: 0x20000E6  // size: 0x38
    public sealed class <Work>d__20 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginWorkFlow.Worker <>4__this;  // 0x20
        public UnityEngine.CustomYieldInstruction work;  // 0x28
        private UnityEngine.CustomYieldInstruction <cancelTask>5__2;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60004FD
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60004FE
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03D4ECE0  token: 0x60004FF
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A2C64  token: 0x6000501
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000E5  // size: 0x38
    public class Worker : System.IDisposable
    {
        // Fields
        private System.Collections.Generic.HashSet<Beyond.Login.LoginWorkFlow.IWorkerUpdateOnce> m_updateOnce;  // 0x10
        private System.Collections.Generic.List<Beyond.Login.LoginWorkFlow.IWorkerUpdateOnce> m_buffer;  // 0x18
        private UnityEngine.CustomYieldInstruction m_curWork;  // 0x20
        private System.Boolean <isCancelled>k__BackingField;  // 0x28
        private System.Boolean <isDisposed>k__BackingField;  // 0x29
        private Beyond.Login.LoginWorkFlow.Node <curNode>k__BackingField;  // 0x30

        // Properties
        System.Boolean isCancelled { get; /* RVA: 0x011F33C0 */ set; /* RVA: 0x011F33D0 */ }
        System.Boolean isDisposed { get; /* RVA: 0x04D867B0 */ set; /* RVA: 0x04D867C0 */ }
        Beyond.Login.LoginWorkFlow.Node curNode { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }

        // Methods
        // RVA: 0x069AAA0C  token: 0x60004F5
        public System.Boolean Cancel() { }
        // RVA: 0x04D26A80  token: 0x60004F6
        public virtual System.Void Dispose() { }
        // RVA: 0x03405DE0  token: 0x60004F7
        public System.Void SetCurNode(Beyond.Login.LoginWorkFlow.Node node) { }
        // RVA: 0x0450CB50  token: 0x60004F8
        public System.Boolean UpdateOnce(Beyond.Login.LoginWorkFlow.IWorkerUpdateOnce inst) { }
        // RVA: 0x03D4ECA0  token: 0x60004F9
        public System.Void TickInEmptyFrame() { }
        // RVA: 0x03406100  token: 0x60004FA
        public System.Collections.IEnumerator Work(UnityEngine.CustomYieldInstruction work) { }
        // RVA: 0x03D4EE00  token: 0x60004FB
        private System.Void _InternalTick() { }
        // RVA: 0x04CC9AF0  token: 0x60004FC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E7  // size: 0x1C
    public sealed struct FWork
    {
        // Fields
        public static readonly Beyond.Login.LoginWorkFlow.FWork IDLE;  // static @ 0x0
        private System.Boolean <didFallback>k__BackingField;  // 0x10
        private System.Int32 <nodeIndex>k__BackingField;  // 0x14
        private System.Boolean <isWorking>k__BackingField;  // 0x18

        // Properties
        System.Boolean didFallback { get; /* RVA: 0x04D883E0 */ set; /* RVA: 0x04D87180 */ }
        System.Int32 nodeIndex { get; /* RVA: 0x04D88B20 */ set; /* RVA: 0x04D88B50 */ }
        System.Boolean isWorking { get; /* RVA: 0x04D8CCC0 */ set; /* RVA: 0x04D8CCD0 */ }
        Beyond.Login.LoginWorkFlow.ENode curNode { get; /* RVA: 0x03406B00 */ }

        // Methods
        // RVA: 0x069A1118  token: 0x6000509
        public System.Void MarkFallback(System.Int32 newIndex) { }
        // RVA: 0x03406C30  token: 0x600050A
        public System.Boolean ConsumeFallback() { }
        // RVA: 0x03406BD0  token: 0x600050B
        public System.Void MoveNextIndex() { }
        // RVA: 0x04D18180  token: 0x600050C
        public static Beyond.Login.LoginWorkFlow.FWork Start() { }
        // RVA: 0x03406C90  token: 0x600050E
        public System.Boolean WorkEnd() { }
        // RVA: 0x041E1670  token: 0x600050F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E8  // size: 0x50
    public sealed class <DoWorkFlow>d__25 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginWorkFlow <>4__this;  // 0x20
        private Beyond.Login.LoginWorkFlow.Worker <worker>5__2;  // 0x28
        private Beyond.Login.LoginViewProperty <viewProp>5__3;  // 0x30
        private Beyond.Login.LoginWorkFlow.ENode <cachedCurrent>5__4;  // 0x38
        private System.Boolean <isEmptyFrame>5__5;  // 0x3c
        private System.Boolean <isAbort>5__6;  // 0x3d
        private Beyond.Login.LoginWorkFlow.Node[] <>7__wrap6;  // 0x40
        private System.Int32 <>7__wrap7;  // 0x48

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000510
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000511
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x034064C0  token: 0x6000512
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A25F4  token: 0x6000514
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000E9  // size: 0x48
    public sealed class <_FetchAgeTipsCoroutine>d__45 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginAgeTipsProp prop;  // 0x20
        public Beyond.Login.LoginWorkFlow <>4__this;  // 0x28
        private Beyond.Login.LoginAgeTipsModel <model>5__2;  // 0x30
        private Beyond.LoginController.MaskCtrl <maskCtrl>5__3;  // 0x38
        private Beyond.Misc.ObjectRef <httpRetRef>5__4;  // 0x40

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000516
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000517
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069A6C84  token: 0x6000518
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A6F64  token: 0x600051A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000EA  // size: 0x28
    public sealed class <_AdjustCurrentGameState>d__47 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginWorkFlow.ENode curNode;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600051C
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600051D
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x036A6080  token: 0x600051E
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069A3C38  token: 0x6000520
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000F2  // size: 0x18
    public sealed struct Options
    {
        // Fields
        public System.Int64 minBytesToNotifyCarrier;  // 0x10

    }

    // TypeToken: 0x20000F3  // size: 0x30
    public sealed class <>c__DisplayClass6_0
    {
        // Fields
        public System.Int64 downloadSize;  // 0x10
        public Beyond.Login.NetUsagePolicy <>4__this;  // 0x18
        public System.Action onAllowed;  // 0x20
        public System.Action onRejected;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000542
        public System.Void .ctor() { }
        // RVA: 0x069B4C50  token: 0x6000543
        private System.Void <CheckIfAllowDownload>b__0() { }
        // RVA: 0x069B4CD0  token: 0x6000544
        private System.Void <CheckIfAllowDownload>b__1() { }

    }

    // TypeToken: 0x20000F5  // size: 0x14
    public sealed struct EState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Login.LoginSceneAnimCtrl.EState NONE;  // const
        public static Beyond.Login.LoginSceneAnimCtrl.EState A1;  // const
        public static Beyond.Login.LoginSceneAnimCtrl.EState A2;  // const

    }

    // TypeToken: 0x20000FB  // size: 0x20
    public sealed struct FParams
    {
        // Fields
        public System.String text;  // 0x10
        public System.Action onConfirm;  // 0x18

    }

    // TypeToken: 0x2000100  // size: 0x14
    public sealed struct Style
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Login.LoginJudgeDialog.Style Center;  // const
        public static Beyond.Login.LoginJudgeDialog.Style Left;  // const

    }

    // TypeToken: 0x2000101  // size: 0x40
    public sealed struct FParams
    {
        // Fields
        public System.String text;  // 0x10
        public System.Action onPositive;  // 0x18
        public System.Action onNegative;  // 0x20
        public System.String btnPositive;  // 0x28
        public System.String btnNegative;  // 0x30
        public Beyond.Login.LoginJudgeDialog.Style style;  // 0x38

    }

    // TypeToken: 0x2000103  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.Login.LoginMenuPanel.<>c <>9;  // static @ 0x0
        public static System.Action <>9__43_0;  // static @ 0x8

        // Methods
        // RVA: 0x069B4D40  token: 0x60005BE
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60005BF
        public System.Void .ctor() { }
        // RVA: 0x069B4830  token: 0x60005C0
        private System.Void <_OnQuitGameClicked>b__43_0() { }

    }

    // TypeToken: 0x2000105  // size: 0x20
    public sealed struct FOptions
    {
        // Fields
        public System.String fixedUID;  // 0x10
        public Beyond.LoginContext context;  // 0x18

        // Methods
        // RVA: 0x03F10080  token: 0x60005D6
        public System.Boolean IsUIDFixed() { }

    }

    // TypeToken: 0x2000106  // size: 0x28
    public sealed class <HideAndDestroy>d__22 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginMockUIDPanel <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60005D7
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60005D8
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x049D1390  token: 0x60005D9
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069B47E4  token: 0x60005DB
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000109  // size: 0x28
    public abstract class ChildPanel : UnityEngine.MonoBehaviour, Beyond.Login.ILoginContextReceiver
    {
        // Fields
        private Beyond.UI.UISwitchTween <switchTween>k__BackingField;  // 0x18
        private Beyond.LoginContext <context>k__BackingField;  // 0x20

        // Properties
        Beyond.UI.UISwitchTween switchTween { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        Beyond.LoginContext context { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x041E5A50  token: 0x60005EE
        public virtual System.Void OnLoginContext(Beyond.LoginContext context) { }
        // RVA: 0x042B60D0  token: 0x60005EF
        protected virtual System.Void OnInit() { }
        // RVA: 0x048AB6E0  token: 0x60005F0
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x069AAF5C  token: 0x60005F1
        protected System.Void SetPanelVisibleByAnimSwitchTween(Beyond.UI.UIAnimationWrapper anim, System.Boolean isVisible, System.Boolean forceReset) { }
        // RVA: 0x03406EF0  token: 0x60005F2
        protected System.Void SetPanelVisibleByFadeSwitchTween(UnityEngine.CanvasGroup canvasGroup, System.Boolean isVisible, System.Boolean forceReset) { }
        // RVA: 0x05393520  token: 0x60005F3
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200010B  // size: 0x30
    public sealed struct DelayInfo
    {
        // Fields
        public UnityEngine.Color color;  // 0x10
        public System.Int32 colorCount;  // 0x20
        public System.Int32 delayMax;  // 0x24
        public System.String text;  // 0x28

    }

    // TypeToken: 0x2000110  // size: 0x30
    public sealed class <>c__DisplayClass10_0
    {
        // Fields
        public Beyond.Login.LoginServerSelectPanel <>4__this;  // 0x10
        public System.Collections.Generic.List<Beyond.Gameplay.ServerZoneManager.ServerZoneData> serverZoneDataList;  // 0x18
        public System.Collections.Generic.List<Hypergryph.SpeedTest.SpeedTestServerInfo> serverList;  // 0x20
        public System.Action<Hypergryph.SpeedTest.SpeedTestResult> callback;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600060F
        public System.Void .ctor() { }
        // RVA: 0x069B4874  token: 0x6000610
        private System.Void <OnEnable>b__0(Hypergryph.SpeedTest.SpeedTestResult result) { }
        // RVA: 0x069B4BE8  token: 0x6000611
        private System.Threading.Tasks.Task<Hypergryph.SpeedTest.TaskResult> <OnEnable>b__1() { }

    }

    // TypeToken: 0x2000111  // size: 0x28
    public sealed class <_WaitingForShowListAwake>d__11 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Beyond.Login.LoginServerSelectPanel <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000612
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000613
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x069B4DA4  token: 0x6000614
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x069B4E94  token: 0x6000616
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000114  // size: 0x18
    public sealed struct FParams
    {
        // Fields
        public System.Action onConfirm;  // 0x10

    }

    // TypeToken: 0x2000115  // size: 0x20
    public class VocResInfo
    {
        // Fields
        public System.String nameTextId;  // 0x10
        public Beyond.VFS.EVFSBlockType type;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000630
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011A  // size: 0x44
    public sealed struct __StaticArrayInitTypeSize=52
    {
    }

    // TypeToken: 0x2000119  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=52 DF6EA5B59F0E6C03F390AD5FEA0BBC199EC3524C0A682913148FD071AF1898EC;  // static @ 0x0

    }

namespace Beyond
{

    // TypeToken: 0x2000004  // size: 0x28
    public class GameApp : UnityEngine.MonoBehaviour, Beyond.ILuaCallCSharp
    {
        // Fields
        private Beyond.GameStateMachine m_stateMachine;  // 0x18
        private static Beyond.GameApp <instance>k__BackingField;  // static @ 0x0
        private System.Boolean m_hasFocus;  // 0x20
        private System.Boolean m_needCallLuaFocus;  // 0x21
        private System.Boolean m_pauseStatus;  // 0x22
        private System.Boolean m_needCallLuaPause;  // 0x23
        private static System.UInt32 FILE_ATTRIBUTE_DIRECTORY;  // const
        private static System.UInt32 FILE_ATTRIBUTE_READONLY;  // const
        private static System.UInt32 FILE_ATTRIBUTE_NORMAL;  // const
        private static System.UInt32 FILE_ATTRIBUTE_REPARSE_POINT;  // const
        private static readonly System.IntPtr INVALID_HANDLE_VALUE;  // static @ 0x8
        private static System.UInt32 GENERIC_READ;  // const
        private static System.UInt32 GENERIC_WRITE;  // const
        private static System.UInt32 FILE_SHARE_READ;  // const
        private static System.UInt32 FILE_SHARE_WRITE;  // const
        private static System.UInt32 FILE_SHARE_DELETE;  // const
        private static System.UInt32 OPEN_EXISTING;  // const
        private static System.UInt32 FILE_FLAG_SEQUENTIAL_SCAN;  // const
        private static System.Int32 ERROR_FILE_NOT_FOUND;  // const
        private static System.Int32 ERROR_PATH_NOT_FOUND;  // const
        private static System.Int32 ERROR_NO_MORE_FILES;  // const
        private static System.Byte[] s_md5Buffer;  // static @ 0xffffffff
        private static System.Int32 s_scanRunning;  // static @ 0x10
        private static System.String s_pendingScanOutput;  // static @ 0x18

        // Properties
        Beyond.GameApp instance { get; /* RVA: 0x06991E18 */ set; /* RVA: 0x06991E68 */ }
        Beyond.GameState currentState { get; /* RVA: 0x036A6260 */ }
        System.Boolean IsScanRunning { get; /* RVA: 0x06991D98 */ }

        // Methods
        // RVA: 0x03C11BB0  token: 0x6000008
        public System.Void SwitchState(Beyond.GameState state) { }
        // RVA: 0x0380F1F0  token: 0x6000009
        private System.Void Awake() { }
        // RVA: 0x041A5BF0  token: 0x600000A
        private System.Boolean _NeedDeepProfilerMode() { }
        // RVA: 0x0698FB68  token: 0x600000B
        private System.Collections.IEnumerator _DelayInit() { }
        // RVA: 0x0488B500  token: 0x600000C
        private System.Void _RegisterLuaAfterGCEvent() { }
        // RVA: 0x0464E170  token: 0x600000D
        private System.Void _InitNotificationCenter() { }
        // RVA: 0x032E1800  token: 0x600000E
        private System.Void Update() { }
        // RVA: 0x02F5C730  token: 0x600000F
        private System.Void LateUpdate() { }
        // RVA: 0x0698D850  token: 0x6000010
        private System.Void OnApplicationQuit() { }
        // RVA: 0x03A73FA0  token: 0x6000011
        private System.Void OnApplicationFocus(System.Boolean hasFocus) { }
        // RVA: 0x03820560  token: 0x6000012
        private System.Void _OnApplicationFocusForLua() { }
        // RVA: 0x03F26470  token: 0x6000013
        private System.Void OnApplicationPause(System.Boolean pauseStatus) { }
        // RVA: 0x03F26780  token: 0x6000014
        private System.Void _OnApplicationPauseForLua() { }
        // RVA: 0x0698DA78  token: 0x6000015
        private System.Void OnDestroy() { }
        // RVA: 0x035C2290  token: 0x6000016
        private System.Void _PreInitGameSetting() { }
        // RVA: 0x0464DC20  token: 0x6000017
        private System.Void _Init() { }
        // RVA: 0x06990FE4  token: 0x6000018
        private System.Void _UnInit() { }
        // RVA: 0x0464E1A0  token: 0x6000019
        private System.Void _InitGameStateMachine() { }
        // RVA: 0x06990F60  token: 0x600001A
        private System.Void _UnInitGameStateMachine() { }
        // RVA: 0x0698DD34  token: 0x600001B
        public static System.Void QuitGame(Beyond.GameAppExitCode exitCode) { }
        // RVA: 0x0698DDA8  token: 0x600001C
        public static System.Void QuitGame(System.Int32 exitCode) { }
        // RVA: 0x0698DE64  token: 0x600001D
        public static System.Void RefreshScreenResolution() { }
        // RVA: 0x06990210  token: 0x600001E
        private static System.Void _OnSystemDisplaySizeChanged() { }
        // RVA: 0x0699029C  token: 0x600001F
        private static System.Void _OnSystemScreenSizeChanged(Beyond.EventData<System.Boolean,System.Boolean>& data) { }
        // RVA: 0x0464DE50  token: 0x6000020
        private System.Collections.IEnumerator _InitSecuritySDK() { }
        // RVA: 0x0340DAF0  token: 0x6000021
        private System.Void _OnApplicationPauseForSecurity(System.Boolean pauseStatus) { }
        // RVA: 0x04D32D40  token: 0x6000022
        private System.Collections.IEnumerator _FetchRemoteConfigUtilSuc() { }
        // RVA: 0x0698FC74  token: 0x6000023
        private static System.IntPtr _FindFirstFileW(System.String lpFileName, Beyond.GameApp.WIN32_FIND_DATA& lpFindFileData) { }
        // RVA: 0x0698FD8C  token: 0x6000024
        private static System.Boolean _FindNextFileW(System.IntPtr hFindFile, Beyond.GameApp.WIN32_FIND_DATA& lpFindFileData) { }
        // RVA: 0x0698FBF4  token: 0x6000025
        private static System.Boolean _FindClose(System.IntPtr hFindFile) { }
        // RVA: 0x0698FA90  token: 0x6000026
        private static System.IntPtr _CreateFileW(System.String lpFileName, System.UInt32 dwDesiredAccess, System.UInt32 dwShareMode, System.IntPtr lpSecurityAttributes, System.UInt32 dwCreationDisposition, System.UInt32 dwFlagsAndAttributes, System.IntPtr hTemplateFile) { }
        // RVA: 0x06990760  token: 0x6000027
        private static System.Boolean _ReadFile(System.IntPtr hFile, System.Byte[] lpBuffer, System.UInt32 nNumberOfBytesToRead, System.UInt32& lpNumberOfBytesRead, System.IntPtr lpOverlapped) { }
        // RVA: 0x0698E99C  token: 0x6000028
        private static System.Boolean _CloseHandle(System.IntPtr hObject) { }
        // RVA: 0x0698DC9C  token: 0x600002A
        public static System.Boolean PollScanResult(System.String& gzOutputPath) { }
        // RVA: 0x0698DF64  token: 0x600002B
        public static System.Void ScanLocalFiles(System.String rootDir, System.String outputPath, System.Int32 threadCount) { }
        // RVA: 0x0698D4A0  token: 0x600002C
        private static System.Void LaunchScanCoordinator(System.String rootDir, System.String outputPath, System.Int32 threadCount, System.String systemInfo) { }
        // RVA: 0x06990C04  token: 0x600002D
        private static System.Collections.Generic.List<Beyond.GameApp.FileEntry> _ScanParallel(System.String root, System.Int32 threadCount) { }
        // RVA: 0x06991074  token: 0x600002E
        private static System.Void _WorkerLoop(System.Object state) { }
        // RVA: 0x06990824  token: 0x600002F
        private static System.Void _ScanDirectory(Beyond.GameApp.ScanContext ctx, System.String dir) { }
        // RVA: 0x06990060  token: 0x6000030
        private static System.String _MakeLongPath(System.String path) { }
        // RVA: 0x0698E160  token: 0x6000031
        private static Beyond.GameApp.FileEntry _BuildEntry(Beyond.GameApp.PendingFile pf) { }
        // RVA: 0x0698E8C0  token: 0x6000032
        private static System.Boolean _CanAccess(System.String path, System.UInt32 desiredAccess) { }
        // RVA: 0x0698E3F0  token: 0x6000033
        private static System.String _CalculateMd5(System.String path) { }
        // RVA: 0x0698E9EC  token: 0x6000034
        private static System.String _CollectSystemInfo() { }
        // RVA: 0x06991268  token: 0x6000035
        private static System.Void _WriteTreeReport(System.Collections.Generic.List<Beyond.GameApp.FileEntry> entries, System.String root, System.DateTime scanStart, System.String outputPath, System.String systemInfo) { }
        // RVA: 0x06990358  token: 0x6000036
        private static System.Void _PrintTreeNode(System.Text.StringBuilder sb, Beyond.GameApp.TreeNode node, System.String prefix) { }
        // RVA: 0x06990EB4  token: 0x6000037
        private static System.Int32 _TreeNodeCompare(Beyond.GameApp.TreeNode a, Beyond.GameApp.TreeNode b) { }
        // RVA: 0x0698FE9C  token: 0x6000038
        private static System.String _FormatSize(System.Int64 bytes) { }
        // RVA: 0x04D84C40  token: 0x6000039
        public System.Void .ctor() { }
        // RVA: 0x04D7A710  token: 0x600003A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000E
    public interface IMemoryPackProxy
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000052
        public virtual System.Void Init() { }

    }

    // TypeToken: 0x200000F  // size: 0x10
    public class MemoryPackProxyModule
    {
        // Fields
        private static Beyond.IMemoryPackProxy s_memoryPackProxy;  // static @ 0x0

        // Methods
        // RVA: 0x06993C80  token: 0x6000053
        public static System.Void RegisterProxy(Beyond.IMemoryPackProxy memoryPackProxy) { }
        // RVA: 0x06993C48  token: 0x6000054
        public static Beyond.IMemoryPackProxy GetMemoryPackProxy() { }
        // RVA: 0x041E1670  token: 0x6000055
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000010  // size: 0x18
    public class GameInitState : Beyond.GameStateBase
    {
        // Properties
        Beyond.GameState stateId { get; /* RVA: 0x04D261C0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000057
        public System.Void .ctor(Beyond.GameStateMachine stateMachine) { }
        // RVA: 0x0380EA50  token: 0x6000058
        public virtual System.Void OnEnter(Beyond.GameState fromStateId) { }
        // RVA: 0x041E4230  token: 0x6000059
        public virtual System.Void OnLeave(Beyond.GameState toStateId) { }
        // RVA: 0x0380EB40  token: 0x600005A
        private System.Collections.IEnumerator _DoInit() { }
        // RVA: 0x03DA3770  token: 0x600005B
        private System.Void _InitInitialResourceManager(Beyond.VFS.VirtualFileSystem.BlockValidator& blockValidator) { }
        // RVA: 0x0332A050  token: 0x600005C
        private System.Boolean _CheckIfLocalInitialAssetsValid(Beyond.Resource.HotUpdate.LocalResPref& outputLocalResPref, Beyond.VFS.VirtualFileSystem.BlockValidator& blockValidator) { }
        // RVA: 0x03BA9CF0  token: 0x600005D
        private System.Void _InitAPPDefaultCulture() { }
        // RVA: 0x03C91F20  token: 0x600005E
        private System.Void _InitGameSetting() { }
        // RVA: 0x06991EC4  token: 0x600005F
        public System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0) { }
        // RVA: 0x06991ECC  token: 0x6000060
        public System.Void <>iFixBaseProxy_OnLeave(Beyond.GameState P0) { }

    }

    // TypeToken: 0x2000012  // size: 0x18
    public class GameLoginState : Beyond.GameStateBase
    {
        // Properties
        Beyond.GameState stateId { get; /* RVA: 0x04D51FE0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000068
        public System.Void .ctor(Beyond.GameStateMachine stateMachine) { }
        // RVA: 0x0380EBB0  token: 0x6000069
        public virtual System.Void OnEnter(Beyond.GameState fromStateId) { }
        // RVA: 0x041E4770  token: 0x600006A
        public virtual System.Void OnLeave(Beyond.GameState toStateId) { }
        // RVA: 0x04A20820  token: 0x600006B
        private static System.Void _TryStartLoginWorkflow() { }
        // RVA: 0x06991EC4  token: 0x600006C
        public System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0) { }
        // RVA: 0x06991ECC  token: 0x600006D
        public System.Void <>iFixBaseProxy_OnLeave(Beyond.GameState P0) { }

    }

    // TypeToken: 0x2000013  // size: 0x20
    public class GameMainState : Beyond.GameStateBase
    {
        // Fields
        private System.Boolean m_isFirstEnter;  // 0x18

        // Properties
        Beyond.GameState stateId { get; /* RVA: 0x04D51FB0 */ }

        // Methods
        // RVA: 0x05397B28  token: 0x600006F
        public System.Void .ctor(Beyond.GameStateMachine stateMachine) { }
        // RVA: 0x0380E4E0  token: 0x6000070
        public virtual System.Void OnEnter(Beyond.GameState fromStateId) { }
        // RVA: 0x041E4830  token: 0x6000071
        public virtual System.Void OnLeave(Beyond.GameState toStateId) { }
        // RVA: 0x04816600  token: 0x6000072
        public static System.Void ReleaseMainGame(Beyond.GameState toStateId) { }
        // RVA: 0x06991ED4  token: 0x6000073
        private static System.Void _ClearUtils() { }
        // RVA: 0x06991EC4  token: 0x6000074
        public System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0) { }
        // RVA: 0x06991ECC  token: 0x6000075
        public System.Void <>iFixBaseProxy_OnLeave(Beyond.GameState P0) { }

    }

    // TypeToken: 0x2000014  // size: 0x20
    public class GamePreloadState : Beyond.GameStateBase
    {
        // Fields
        private Beyond.Cfg.Tables.AsyncLoadTask m_asyncTask;  // 0x18

        // Properties
        Beyond.GameState stateId { get; /* RVA: 0x04D51F50 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000077
        public System.Void .ctor(Beyond.GameStateMachine stateMachine) { }
        // RVA: 0x0380E620  token: 0x6000078
        public virtual System.Void OnEnter(Beyond.GameState fromStateId) { }
        // RVA: 0x041E4880  token: 0x6000079
        public virtual System.Void OnLeave(Beyond.GameState toStateId) { }
        // RVA: 0x0380E6C0  token: 0x600007A
        private System.Collections.IEnumerator _DoPreload() { }
        // RVA: 0x04151FC0  token: 0x600007B
        private System.Void _InitGameSettingStateInPreload() { }
        // RVA: 0x06991EC4  token: 0x600007C
        public System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0) { }
        // RVA: 0x06991ECC  token: 0x600007D
        public System.Void <>iFixBaseProxy_OnLeave(Beyond.GameState P0) { }

    }

    // TypeToken: 0x2000016  // size: 0x18
    public class GameQuitState : Beyond.GameStateBase
    {
        // Properties
        Beyond.GameState stateId { get; /* RVA: 0x04D51F20 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000085
        public System.Void .ctor(Beyond.GameStateMachine stateMachine) { }
        // RVA: 0x06992410  token: 0x6000086
        public virtual System.Void OnEnter(Beyond.GameState fromStateId) { }
        // RVA: 0x06992478  token: 0x6000087
        public virtual System.Void OnLeave(Beyond.GameState toStateId) { }
        // RVA: 0x06991FBC  token: 0x6000088
        public static System.Void DoQuit() { }
        // RVA: 0x06991EC4  token: 0x6000089
        public System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0) { }
        // RVA: 0x06991ECC  token: 0x600008A
        public System.Void <>iFixBaseProxy_OnLeave(Beyond.GameState P0) { }

    }

    // TypeToken: 0x2000017  // size: 0x18
    public class GameSoftRestartState : Beyond.GameStateBase
    {
        // Fields
        private static System.String CLEAR_SCENE_PATH;  // const

        // Properties
        Beyond.GameState stateId { get; /* RVA: 0x04D51EF0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600008C
        public System.Void .ctor(Beyond.GameStateMachine stateMachine) { }
        // RVA: 0x069924DC  token: 0x600008D
        public virtual System.Void OnEnter(Beyond.GameState fromStateId) { }
        // RVA: 0x06992594  token: 0x600008E
        private System.Collections.IEnumerator _SoftRestart() { }
        // RVA: 0x06991EC4  token: 0x600008F
        public System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0) { }

    }

    // TypeToken: 0x2000019  // size: 0x18
    public class GameStartState : Beyond.GameStateBase
    {
        // Properties
        Beyond.GameState stateId { get; /* RVA: 0x04D51EC0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000097
        public System.Void .ctor(Beyond.GameStateMachine stateMachine) { }
        // RVA: 0x0380E730  token: 0x6000098
        public virtual System.Void OnEnter(Beyond.GameState fromStateId) { }
        // RVA: 0x041E48F0  token: 0x6000099
        public virtual System.Void OnLeave(Beyond.GameState toStateId) { }
        // RVA: 0x04B52310  token: 0x600009A
        private System.Void _OnEnterMainGame() { }
        // RVA: 0x03C123C0  token: 0x600009B
        private System.Void <_OnEnterMainGame>b__5_0() { }
        // RVA: 0x06991EC4  token: 0x600009C
        public System.Void <>iFixBaseProxy_OnEnter(Beyond.GameState P0) { }
        // RVA: 0x06991ECC  token: 0x600009D
        public System.Void <>iFixBaseProxy_OnLeave(Beyond.GameState P0) { }

    }

    // TypeToken: 0x200001A  // size: 0x18
    public abstract class GameStateBase : Beyond.Gameplay.ISimpleStateNode`1, Beyond.ILuaCallCSharp
    {
        // Fields
        protected readonly Beyond.GameStateMachine m_stateMachine;  // 0x10

        // Properties
        Beyond.GameState stateId { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600009E
        protected System.Void .ctor(Beyond.GameStateMachine stateMachine) { }
        // RVA: 0x0380E880  token: 0x60000A0
        public virtual System.Void OnEnter(Beyond.GameState fromStateId) { }
        // RVA: 0x041E49F0  token: 0x60000A1
        public virtual System.Void OnLeave(Beyond.GameState toStateId) { }
        // RVA: 0x06992624  token: 0x60000A2
        public virtual System.Void OnTick(System.Single deltaTime) { }

    }

    // TypeToken: 0x200001B  // size: 0x38
    public class GameStateMachine : Beyond.Gameplay.SimpleEnumStateMachine`1, Beyond.IGameStateMachine, Beyond.ILuaCallCSharp
    {
        // Fields
        private readonly Beyond.GameApp <app>k__BackingField;  // 0x30

        // Properties
        Beyond.GameApp app { get; /* RVA: 0x011F36E0 */ }
        Beyond.GameState curState { get; /* RVA: 0x0332C370 */ }

        // Methods
        // RVA: 0x0464E240  token: 0x60000A5
        public System.Void .ctor(Beyond.GameApp gameApp) { }
        // RVA: 0x0464E2A0  token: 0x60000A6
        private System.Void _RegisterStates() { }
        // RVA: 0x03C119D0  token: 0x60000A7
        public virtual System.Boolean CheckCanReturnToLoginState() { }
        // RVA: 0x03C11960  token: 0x60000A8
        public virtual System.Collections.IEnumerator ReturnToLoginState() { }

    }

    // TypeToken: 0x200001D  // size: 0x10
    public static class GlobalInitializer
    {
        // Methods
        // RVA: 0x04D83BE0  token: 0x60000AF
        public static System.Void InitGlobalConfig() { }

    }

    // TypeToken: 0x200001E
    public interface IHotFixCommon
    {
    }

    // TypeToken: 0x200001F
    public interface IHotFixPatchManagerProxy : Beyond.IHotFixCommon
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60000B0
        public virtual System.Void InitManager() { }
        // RVA: -1  // abstract  token: 0x60000B1
        public virtual System.Void ReleaseManager() { }
        // RVA: -1  // abstract  token: 0x60000B2
        public virtual System.Void OnReturnToLogin() { }
        // RVA: -1  // abstract  token: 0x60000B3
        public virtual System.Void RegisterHotfixSDKEvent() { }
        // RVA: -1  // abstract  token: 0x60000B4
        public virtual System.Void UnregisterHotfixSDKEvent() { }
        // RVA: -1  // abstract  token: 0x60000B5
        public virtual System.Boolean TryProcessHotfixSDKExtraInfo(System.Int32 code, System.Collections.Generic.Dictionary<System.String,System.Object> msg) { }
        // RVA: -1  // abstract  token: 0x60000B6
        public virtual System.Void PatchInitAssemble() { }
        // RVA: -1  // abstract  token: 0x60000B7
        public virtual System.Void PatchRemoteInitAssemble() { }
        // RVA: -1  // abstract  token: 0x60000B8
        public virtual System.Void PatchRemoteInjectedLua() { }
        // RVA: -1  // abstract  token: 0x60000B9
        public virtual System.Void PatchMainAssemble() { }
        // RVA: -1  // abstract  token: 0x60000BA
        public virtual System.Void PatchRemoteMainAssemble() { }

    }

    // TypeToken: 0x2000020  // size: 0x10
    public class HotFixPatchModules
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.Type,Beyond.IHotFixCommon> s_hotFixModules;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x60000BB
        public static System.Void Register(T module) { }
        // RVA: -1  // generic def  token: 0x60000BC
        public static System.Void UnRegister() { }
        // RVA: -1  // generic def  token: 0x60000BD
        public static T GetModule() { }
        // RVA: -1  // generic def  token: 0x60000BE
        public static System.Boolean TryGetModule(T& module) { }
        // RVA: 0x041E1670  token: 0x60000BF
        public System.Void .ctor() { }
        // RVA: 0x04D17E20  token: 0x60000C0
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000021  // size: 0x10
    public static class InitialPathDef
    {
        // Fields
        public static System.String LOGIN_SCENE_PATH;  // const
        public static System.String LOGIN_SK_ACTOR_M;  // const
        public static System.String LOGIN_SK_ACTOR_F;  // const
        public static System.String LOGIN_SK_ACTOR_NO_GENDER;  // const
        public static System.String LOGIN_MOCK_UID_PANEL;  // const
        public static System.String LOGIN_CONTROLLER_PATH;  // const
        public static System.String LOGIN_ROOT_PANEL_PATH;  // const
        public static System.String LOGIN_ALERT_DIALOG_PATH;  // const
        public static System.String LOGIN_JUDGE_DIALOG_PATH;  // const
        public static System.String LOGIN_VOC_RES_SELECT_DIALOG_PATH;  // const

    }

    // TypeToken: 0x2000022  // size: 0xC0
    public class LoginController : UnityEngine.MonoBehaviour, Beyond.Login.ILoginBinder`1
    {
        // Fields
        private UnityEngine.Transform _uiFunc;  // 0x18
        private UnityEngine.Transform _uiDialog;  // 0x20
        private Beyond.UI.UIButton _versionBtn;  // 0x28
        private Beyond.Login.LoginDecorateUI _uiDecorate;  // 0x30
        private Beyond.LoginController.MaskCfg _maskBlur;  // 0x38
        private Beyond.LoginController.MaskCfg _maskBlack;  // 0x40
        private Beyond.LoginController.MaskCfg _maskWhite;  // 0x48
        private System.Single _enterGamePanelPredelay;  // 0x50
        private UnityEngine.Canvas _uiRootCanvas;  // 0x58
        private Beyond.UI.UICanvasScaleHelper _uiRootCanvasScaleHelper;  // 0x60
        private UnityEngine.Canvas _uiDecoCanvas;  // 0x68
        private Beyond.UI.UICanvasScaleHelper _uiDecoCanvasScaleHelper;  // 0x70
        private System.Boolean m_isInited;  // 0x78
        private Beyond.LoginController.FOptions m_options;  // 0x80
        private Beyond.LatchUtils.InvokeWhenUnlock m_startLatch;  // 0x88
        private Beyond.LatchUtils.InvokeWhenUnlock m_initLatch;  // 0x90
        private Beyond.Login.LoginWorkFlow.ENode m_lastNode;  // 0x98
        private Beyond.LoginController.MaskCtrl m_maskCtrl;  // 0xa0
        private Beyond.Login.LoginRootPanel m_rootPanel;  // 0xa8
        private System.Collections.IEnumerator m_mockLoginRoutine;  // 0xb0
        private System.Action<System.String> m_onMockLogin;  // 0xb8

        // Properties
        Beyond.LoginController.MaskCtrl maskCtrl { get; /* RVA: 0x041E6130 */ }
        Beyond.Login.LoginRootPanel rootPanel { get; /* RVA: 0x041E7A20 */ }

        // Events
        event System.Action<System.String> m_onMockLogin;

        // Methods
        // RVA: 0x04D2A8F0  token: 0x60000C3
        protected System.Void Awake() { }
        // RVA: 0x04D59930  token: 0x60000C4
        protected System.Void Start() { }
        // RVA: 0x041E7A50  token: 0x60000C5
        public System.Single GetEnterGamePanelPredelay() { }
        // RVA: 0x034089F0  token: 0x60000C6
        public virtual System.Void OnValueChanged(Beyond.Login.LoginViewModel viewModel) { }
        // RVA: 0x041E51F0  token: 0x60000C7
        public System.Void InitUI(Beyond.LoginController.FOptions options) { }
        // RVA: 0x0380AB70  token: 0x60000C8
        public System.Void InvokeWhenStart(System.Action callback) { }
        // RVA: 0x04A22370  token: 0x60000C9
        public System.Collections.IEnumerator BeforeDestroy() { }
        // RVA: -1  // generic def  token: 0x60000CA
        private System.Collections.IEnumerator _InstantiateUIFromRes(System.String resPath, UnityEngine.Transform parent, Beyond.Misc.ObjectRef objRef) { }
        // RVA: 0x04CB24C0  token: 0x60000CB
        private System.Void _InitImpl() { }
        // RVA: 0x04CB2510  token: 0x60000CC
        private System.Collections.IEnumerator _InitCoroutine() { }
        // RVA: 0x041E4A80  token: 0x60000CD
        private System.Void _InitVersionBtn() { }
        // RVA: 0x03AF7270  token: 0x60000D0
        public System.Void MockLoginWithCallback(System.String fixedUID, System.Action<System.String> callback) { }
        // RVA: 0x03AF7370  token: 0x60000D1
        private System.Collections.IEnumerator _MockLoginCoroutine(System.String fixedUID) { }
        // RVA: 0x049C1650  token: 0x60000D2
        public System.Void .ctor() { }
        // RVA: 0x04D84E00  token: 0x60000D3
        private System.Void <MockLoginWithCallback>b__44_0() { }

    }

    // TypeToken: 0x2000032  // size: 0x58
    public class LoginContext : System.IDisposable
    {
        // Fields
        private Beyond.LoginController m_controller;  // 0x10
        private System.Boolean m_isDisposed;  // 0x18
        private System.Boolean <quickStart>k__BackingField;  // 0x19
        private System.Boolean <firstLogin>k__BackingField;  // 0x1a
        private Beyond.Login.LoginWorkFlow <workflow>k__BackingField;  // 0x20
        private Beyond.Login.LoginViewProperty <viewProp>k__BackingField;  // 0x28
        private Beyond.LoginManager.AssetLoader <loader>k__BackingField;  // 0x30
        private Beyond.LoginManager.FlowWithMaskMgr <flowWithMask>k__BackingField;  // 0x38
        private Beyond.Login.NetUsagePolicy <netUsagePolicy>k__BackingField;  // 0x40
        private Beyond.Login.LoginGameUpdater <gameUpdater>k__BackingField;  // 0x48
        private System.Boolean <startConnectServer>k__BackingField;  // 0x50

        // Properties
        System.Boolean quickStart { get; /* RVA: 0x04D866A0 */ set; /* RVA: 0x04D866C0 */ }
        System.Boolean firstLogin { get; /* RVA: 0x015AF940 */ set; /* RVA: 0x04D866E0 */ }
        Beyond.Login.LoginWorkFlow workflow { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        Beyond.Login.LoginViewProperty viewProp { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        Beyond.LoginManager.AssetLoader loader { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        Beyond.LoginManager.FlowWithMaskMgr flowWithMask { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        Beyond.Login.NetUsagePolicy netUsagePolicy { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        Beyond.Login.LoginGameUpdater gameUpdater { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        System.Boolean startConnectServer { get; /* RVA: 0x04D86800 */ set; /* RVA: 0x04D86840 */ }

        // Methods
        // RVA: 0x041E6090  token: 0x6000127
        public Beyond.LoginController ViewOnlyController() { }
        // RVA: 0x0380A3F0  token: 0x6000128
        public System.Void Init(System.Boolean quickStart, Beyond.LoginController controller, Beyond.Login.LoginWorkFlow workflow, Beyond.Login.LoginViewProperty viewProp, Beyond.LoginManager.AssetLoader loader, Beyond.LoginManager.FlowWithMaskMgr flowWithMask, Beyond.Login.NetUsagePolicy netUsagePolicy, Beyond.Login.LoginGameUpdater gameUpdater) { }
        // RVA: 0x03A82B00  token: 0x6000129
        public System.Void StartCoroutine(System.Collections.IEnumerator routine) { }
        // RVA: 0x0699286C  token: 0x600012A
        public System.Void StopCoroutine(System.Collections.IEnumerator routine) { }
        // RVA: 0x06992718  token: 0x600012B
        public System.Void SendEvent(Beyond.ELoginEvent evt, System.Object param) { }
        // RVA: 0x041E3F20  token: 0x600012C
        public virtual System.Void Dispose() { }
        // RVA: 0x06992798  token: 0x600012D
        public System.Void ShutDown() { }
        // RVA: 0x069926C4  token: 0x600012E
        public System.Boolean IsWorkflowDisposed() { }
        // RVA: 0x06992678  token: 0x600012F
        public System.Boolean IsDisposed() { }
        // RVA: 0x0380A4D0  token: 0x6000130
        private System.Void _DisposeImpl() { }
        // RVA: 0x041E4070  token: 0x6000131
        private static System.Collections.IEnumerator _DisposeCoroutine(Beyond.LoginController legacyController, System.Action disposeAction) { }
        // RVA: 0x041E1670  token: 0x6000132
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000034  // size: 0x14
    public sealed struct ELoginEvent
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.ELoginEvent None;  // const
        public static Beyond.ELoginEvent V2C_U8Login;  // const
        public static Beyond.ELoginEvent V2C_EnterGame;  // const
        public static Beyond.ELoginEvent V2C_Logout;  // const
        public static Beyond.ELoginEvent V2C_ResourceRepair;  // const
        public static Beyond.ELoginEvent V2C_OpenAgeTips;  // const
        public static Beyond.ELoginEvent V2C_CloseAgeTips;  // const
        public static Beyond.ELoginEvent V2C_OpenServerSelect;  // const
        public static Beyond.ELoginEvent V2C_CloseServerSelect;  // const
        public static Beyond.ELoginEvent V2C_SelectServer;  // const
        public static Beyond.ELoginEvent V2C_ConfirmServer;  // const
        public static Beyond.ELoginEvent V2C_Relogin;  // const
        public static Beyond.ELoginEvent V2C_ReplayGsLogin;  // const
        public static Beyond.ELoginEvent C2V_PrepareGsLogin;  // const

    }

    // TypeToken: 0x2000035  // size: 0x10
    public static class LoginTextDefines
    {
        // Fields
        public static System.String CS_LOGIN_CONN_FAIL;  // const
        public static System.String HOTUPDATE_INDEX_FAILED;  // const
        public static System.String HOTUPDATE_IO_FAILED;  // const
        public static System.String HOTUPDATE_CHECK_VERSION;  // const
        public static System.String HOTUPDATE_DOWNLOADING;  // const
        public static System.String HOTUPDATE_DECOMPRESSING;  // const
        public static System.String HOTUPDATE_DOWNLOAD_ERROR;  // const
        public static System.String HOTUPDATE_DOWNLOAD_DISKFULL;  // const
        public static System.String HOTUPDATE_LOCAL_RES_CHECK;  // const
        public static System.String HOTUPDATE_PRELOAD_RES;  // const
        public static System.String HOTUPDATE_INVALID_LOCAL_FILES;  // const
        public static System.String HOTUPDATE_INIT_RES_CHANGED;  // const
        public static System.String HOTUPDATE_DOWNLOAD_IO_ERROR;  // const
        public static System.String HOTUPDATE_DOWNLOAD_DATA_CHECK_ERROR;  // const
        public static System.String HOTUPDATE_DOWNLOAD_START_DOWNLOAD_ERROR;  // const
        public static System.String HOTUPDATE_DECOMPRESS_ERROR;  // const
        public static System.String REMOTE_CFG_GAME_CFG_ERROR;  // const
        public static System.String REMOTE_CFG_GAME_CFG_TIMEOUT;  // const
        public static System.String REMOTE_CFG_NETWORK_CFG_ERROR;  // const
        public static System.String REMOTE_CFG_NETWORK_CFG_TIMEOUT;  // const
        public static System.String REMOTE_CFG_RES_VERSION_ERROR;  // const
        public static System.String REMOTE_CFG_RES_VERSION_TIMEOUT;  // const
        public static System.String REMOTE_CFG_GAME_VERSION_ERROR;  // const
        public static System.String REMOTE_CFG_GAME_VERSION_TIMEOUT;  // const
        public static System.String LOGIN_JUDGE_EXIT_GAME;  // const
        public static System.String LOGIN_JUDGE_LOGOUT;  // const
        public static System.String LOGIN_JUDGE_RES_REPAIR;  // const
        public static System.String LOGIN_AGE_TIPS_FAILED;  // const
        public static System.String LOGIN_TXT_DIALOG_CONFIRM;  // const
        public static System.String LOGIN_TXT_DIALOG_CANCEL;  // const
        public static System.String LOGIN_AUDIO_LANG_CN;  // const
        public static System.String LOGIN_AUDIO_LANG_JP;  // const
        public static System.String LOGIN_AUDIO_LANG_EN;  // const
        public static System.String LOGIN_AUDIO_LANG_KR;  // const
        public static System.String LOGIN_TXT_NETWORK_ERROR;  // const
        public static System.String LOGIN_GAME_UPGRADED;  // const
        public static System.String LOGIN_TXT_SERVER_CLOSED;  // const
        public static System.String LOGIN_TXT_SERVER_CLOSED_DATETIME;  // const
        public static System.String LOGIN_TXT_USR_BAN_DATETIME;  // const
        public static System.String LOGIN_TXT_HOTUPDATE_CLEAR_DOWNLOAD_FOLDER;  // const
        public static System.String LOGIN_GET_SERVER_LIST_FAILED;  // const
        public static System.String LOGIN_TEXT_SERVER_ZONE_FAILED;  // const
        public static System.String LOGIN_TEXT_GET_APP_VERSION_FAILED;  // const
        public static System.String LOGIN_TEXT_ALERT_CARRIER_NETWORK;  // const
        public static System.String LOGIN_TEXT_ERROR_DOWNLOAD_HOTUPDATE_RES_FAILED;  // const
        public static System.String LOGIN_SHADER_WARM_UP_PRELOAD_RES;  // const
        public static System.String LOGIN_GAME_UPDATE_TIPS_DOWNLOADING;  // const
        public static System.String LOGIN_GAME_UPDATE_TIPS_PAUSE;  // const
        public static System.String LOGIN_GAME_UPDATE_ALERT_NEWER;  // const
        public static System.String LOGIN_GAME_UPDATE_ERROR_DISK_FULL;  // const
        public static System.String LOGIN_GAME_UPDATE_ALERT_SUC;  // const
        public static System.String LOGIN_GAME_UPDATE_ERROR_CHECK_FOR_UPDATE_COMMON;  // const
        public static System.String LOGIN_GAME_UPDATE_ERROR_COMMON;  // const
        public static System.String LOGIN_GAME_UPDATE_TITLE;  // const
        public static System.String LOGIN_GOTO_SHOP_FOR_GAME_UPDATE;  // const
        public static System.String LOGIN_SDK_INIT_FAILED;  // const
        public static System.String LOGIN_DOWNLOADING_SPEED_HINT;  // const
        public static System.String LOGIN_SDK_INIT_FAILED_NEED_QUIT_GAME;  // const
        public static System.String LOGIN_NETWORK_CONGESTION_TITLE;  // const
        public static System.String LOGIN_LOW_SCORE_DEVICE_TIP;  // const
        public static System.String LOGIN_REGISTRY_WRITE_FAILED;  // const

    }

    // TypeToken: 0x2000036  // size: 0x50
    public class LoginManager : Beyond.ExplicitSingleton`1
    {
        // Fields
        public static System.Int64 MIN_BYTES_SIZE_TO_NOTIFY_IF_NOT_WIFI;  // const
        private Beyond.Resource.ISceneHandle m_sceneHandle;  // 0x10
        private Beyond.LoginContext m_context;  // 0x18
        private Beyond.LatchUtils.InvokeWhenUnlock m_workflowFinishLatch;  // 0x20
        private Beyond.LatchUtils.InvokeWhenUnlock m_workflowStartLatch;  // 0x28
        private System.Collections.Generic.Queue<System.WeakReference<Beyond.Login.LoginSceneComponent>> m_pendingSceneComps;  // 0x30
        private System.Collections.Generic.HashSet<Beyond.Login.LoginSceneComponent> m_sceneComps;  // 0x38
        private System.Boolean <quickStart>k__BackingField;  // 0x40
        private System.Object m_dialogBlocker;  // 0x48

        // Properties
        System.Boolean quickStart { get; /* RVA: 0x02D480A0 */ set; /* RVA: 0x02D480B0 */ }
        System.Boolean firstLogin { get; /* RVA: 0x06993898 */ }

        // Methods
        // RVA: 0x04A20900  token: 0x600013C
        public System.Collections.IEnumerator InitCoroutine(System.Boolean quickStart) { }
        // RVA: 0x06992F48  token: 0x600013D
        public System.Boolean IsReLoginInProgress() { }
        // RVA: 0x0699334C  token: 0x600013E
        public static System.Void OnReleaseMainGame() { }
        // RVA: 0x04A20980  token: 0x600013F
        public System.Boolean IsWorkflowInProgress() { }
        // RVA: 0x06992C60  token: 0x6000140
        public System.Void ClearSync() { }
        // RVA: 0x0380E5E0  token: 0x6000141
        public System.Void Clear() { }
        // RVA: 0x0380E1E0  token: 0x6000142
        private System.Void _ReleaseLoginScene() { }
        // RVA: 0x04B523D0  token: 0x6000143
        public System.Void InvokeWhenWorkFlowFinished(System.Action callback) { }
        // RVA: 0x0340BA80  token: 0x6000144
        public System.Void SceneComponentOnly_Bind(Beyond.Login.LoginSceneComponent comp) { }
        // RVA: 0x041E5EA0  token: 0x6000145
        private System.Void _BindPendingComponents() { }
        // RVA: 0x069933D8  token: 0x6000146
        public System.Void ToggleLoginBindingGroup(System.Boolean enable) { }
        // RVA: 0x06992A78  token: 0x6000147
        public System.Void AlertDialog(System.String desc, System.Action callback) { }
        // RVA: 0x06993530  token: 0x6000148
        public System.Void VocResSelDialog(System.Action callback) { }
        // RVA: 0x069934A0  token: 0x6000149
        public System.Collections.IEnumerator VocResSelDialogWithConfirm() { }
        // RVA: 0x06992990  token: 0x600014A
        public System.Collections.IEnumerator AlertDialogWithResult(System.String desc, Beyond.Misc.ObjectRef outRef, System.Action<Beyond.Misc.ObjectRef> callback) { }
        // RVA: 0x069928E8  token: 0x600014B
        public System.Collections.IEnumerator AlertDialogWithConfirm(System.String desc) { }
        // RVA: 0x069930FC  token: 0x600014C
        public System.Void JudgeDialog(System.String desc, System.Action onPositive, System.Action onNegative) { }
        // RVA: 0x06993060  token: 0x600014D
        public System.Void JudgeDialog(Beyond.Login.LoginJudgeDialog.FParams param) { }
        // RVA: 0x069936D0  token: 0x600014E
        private System.Void _StartShowJudgeDialogCoroutine(Beyond.Login.LoginJudgeDialog.FParams param) { }
        // RVA: -1  // generic def  token: 0x600014F
        private System.Collections.IEnumerator _ShowDialogCoroutine(System.String resPath, TParam param, System.Action<TDialog,TParam> setParamFunc) { }
        // RVA: 0x041E5AD0  token: 0x6000150
        public System.Void MockLoginWithCallback(System.Action<System.String> callback) { }
        // RVA: 0x069931EC  token: 0x6000151
        public System.Void MockManualSelectServer(System.String uid, System.Action callback) { }
        // RVA: 0x0380AC70  token: 0x6000152
        private System.Void _Init() { }
        // RVA: 0x0340B9B0  token: 0x6000153
        public System.Boolean IsLoginSceneLoading() { }
        // RVA: 0x0380A660  token: 0x6000154
        private System.Void _PrepareBeforeLoginProcess() { }
        // RVA: 0x04CA73D0  token: 0x6000155
        private System.Collections.IEnumerator _LoginWorkFlowCoroutine(Beyond.LoginController nullableCtrl) { }
        // RVA: 0x041E5120  token: 0x6000156
        private System.Void _ClearWorkflowAndContext(System.Boolean isShutDownMode) { }
        // RVA: 0x04CAF5B0  token: 0x6000157
        public static System.Collections.IEnumerator FetchRemoteConfigUtilSuc(Beyond.Cfg.IRemoteConfig config, System.String configEnv, System.String keyErrorWithCode, System.String keyTimeout) { }
        // RVA: 0x03C0A3F0  token: 0x6000158
        private static System.String _ExtractErrorInfo(Beyond.Network.HttpSender.Result result, System.String keyErrorWithCode, System.String keyTimeout) { }
        // RVA: 0x06992CC0  token: 0x6000159
        public static System.Void DevOnlyCheckEnableLocalResMode(System.Boolean& enable) { }
        // RVA: 0x06992D40  token: 0x600015A
        public static System.Void DevOnlyUseLocalResVersion(Beyond.Cfg.ResVersionDevCfg config, System.Boolean& useLocalResVersion) { }
        // RVA: 0x04A7D450  token: 0x600015B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000049  // size: 0x18
    public class VietnamLoginAgePanel : UnityEngine.MonoBehaviour
    {
        // Fields
        private static System.String PREFAB_PATH;  // const
        private static Beyond.Resource.FAssetProxyHandle s_prefabAssetHandle;  // static @ 0x0
        public static UnityEngine.GameObject instance;  // static @ 0x18

        // Methods
        // RVA: 0x041E4E90  token: 0x60001AB
        public static System.Void Init() { }
        // RVA: 0x06995914  token: 0x60001AC
        public static System.Void UnInit() { }
        // RVA: 0x0699598C  token: 0x60001AD
        private static System.Void _Init() { }
        // RVA: 0x06995C14  token: 0x60001AE
        private static System.Void _UnInit() { }
        // RVA: 0x06995828  token: 0x60001AF
        private System.Void OnApplicationQuit() { }
        // RVA: 0x05393520  token: 0x60001B0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004A  // size: 0x38
    public class VietnamLoginAgeTipsPanelCtrl : UnityEngine.MonoBehaviour
    {
        // Fields
        private Beyond.UI.UIDrag _UIDrag;  // 0x18
        private Beyond.UI.UIButton _tipsBtn;  // 0x20
        private Beyond.UI.UIButton _circleBtn;  // 0x28
        private Beyond.UI.UIState.UIStateController _stateController;  // 0x30

        // Methods
        // RVA: 0x06995DB4  token: 0x60001B1
        public System.Void Awake() { }
        // RVA: 0x0699608C  token: 0x60001B2
        private System.Void _SetStateTips(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x06995FF0  token: 0x60001B3
        private System.Void _SetStateCircle(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x05393520  token: 0x60001B4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004B  // size: 0x10
    public static class RenderPipelineDataLoader
    {
        // Fields
        public static Beyond.Resource.FAssetProxyHandle s_globalSettings;  // static @ 0x0
        public static Beyond.Resource.FAssetProxyHandle s_pipelineAsset;  // static @ 0x14

        // Methods
        // RVA: 0x041E42D0  token: 0x60001B5
        public static System.Void LoadSimpleRpData() { }
        // RVA: 0x0380ECC0  token: 0x60001B6
        public static System.Void PreloadFullRpResources() { }
        // RVA: 0x03C12270  token: 0x60001B7
        public static System.Boolean IsPreLoadFinished() { }
        // RVA: 0x041E44D0  token: 0x60001B8
        public static System.Void LoadFullRpResources() { }

    }

    // TypeToken: 0x200004C  // size: 0x18
    public class TailGameLoop : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x02F5C450  token: 0x60001B9
        private System.Void Update() { }
        // RVA: 0x02F5C8C0  token: 0x60001BA
        private System.Void LateUpdate() { }
        // RVA: 0x05393520  token: 0x60001BB
        public System.Void .ctor() { }

    }

}

namespace Beyond.Login
{

    // TypeToken: 0x200006F  // size: 0x30
    public class LoginCheckForUpdateNode : Node
    {
        // Fields
        private Beyond.Login.LoginGameUpdater m_sdk;  // 0x20
        private System.Boolean m_checkSuccess;  // 0x28
        private static System.String REQUEST_API;  // const
        private static System.String REQUEST_PARAM_FORMAT;  // const
        private static System.String REQUEST_URL_FORMAT;  // const
        private static System.String PRAM_SOURCE;  // const

        // Properties
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: 0x049B1850 */ }

        // Methods
        // RVA: 0x045E8660  token: 0x6000240
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x045E8760  token: 0x6000241
        private System.Collections.IEnumerator _CheckForUpdateCoroutine() { }
        // RVA: 0x014F5A60  token: 0x6000242
        private System.Collections.IEnumerator _AlertAndQuitForExtraConfigFatalError() { }
        // RVA: 0x04D351D0  token: 0x6000243
        public static System.Collections.IEnumerator AlertNoMatchDeviceBeforeLogin() { }
        // RVA: 0x06996F20  token: 0x6000244
        private System.Collections.IEnumerator _CheckForUpdateCoroutineMobile() { }
        // RVA: 0x04D1F080  token: 0x6000245
        private System.Collections.IEnumerator _CheckForUpdateCoroutinePC() { }
        // RVA: 0x04D1F010  token: 0x6000246
        private System.Collections.IEnumerator _CheckForUpdatePC() { }
        // RVA: 0x04CE7A50  token: 0x6000247
        private System.Collections.IEnumerator _GetOnlineAppVersion(Beyond.Misc.ObjectRef outRef) { }
        // RVA: 0x038128E0  token: 0x6000248
        private System.String _GetRequestPram() { }
        // RVA: 0x041E1670  token: 0x6000249
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000078  // size: 0x28
    public class LoginCreateRoleNode : Node
    {
        // Fields
        public static System.String LUA_SYS_NAME;  // const
        private Beyond.Login.LoginCreateRoleNode.WorkProcess m_activeProcess;  // 0x20

        // Properties
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: 0x049B1820 */ }

        // Methods
        // RVA: 0x046DD050  token: 0x6000276
        public virtual System.Void OnDispose() { }
        // RVA: 0x06996FB0  token: 0x6000277
        public virtual System.Boolean CanCancel() { }
        // RVA: 0x04CCA6C0  token: 0x6000278
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x041E1670  token: 0x6000279
        public System.Void .ctor() { }
        // RVA: 0x06997004  token: 0x600027A
        public System.Void <>iFixBaseProxy_OnDispose() { }
        // RVA: 0x06996FFC  token: 0x600027B
        public System.Boolean <>iFixBaseProxy_CanCancel() { }

    }

    // TypeToken: 0x200007A  // size: 0x90
    public class LoginDownloadTask : System.IDisposable
    {
        // Fields
        private System.Boolean m_isDownloadFinished;  // 0x10
        private Beyond.SDK.HGDownloader.TaskHandler m_downloadTask;  // 0x18
        private Beyond.Resource.HotUpdate.FResourceDiff m_resDiff;  // 0x20
        private Beyond.Resource.HotUpdate.LocalResPref m_localResPref;  // 0x60
        private System.String m_patchInfo;  // 0x68
        private System.Collections.IEnumerator m_mockCoroutine;  // 0x70
        private System.Boolean m_isInitial;  // 0x78
        private System.String <vitalError>k__BackingField;  // 0x80
        private Beyond.Login.NetUsagePolicy m_netUsagePolicy;  // 0x88

        // Properties
        System.String vitalError { get; /* RVA: 0x04D86260 */ set; /* RVA: 0x04CD9970 */ }

        // Methods
        // RVA: 0x069984DC  token: 0x600027F
        public System.Void .ctor(Beyond.Resource.HotUpdate.FResourceDiff resDiff, Beyond.Resource.HotUpdate.LocalResPref localResPref, System.String patchInfo, System.Boolean isInitial, Beyond.Login.NetUsagePolicy netUsagePolicy) { }
        // RVA: 0x069970FC  token: 0x6000282
        public Beyond.SDK.HGDownloader.DownloadProgress GetDownloadProgress() { }
        // RVA: 0x06997098  token: 0x6000283
        public System.Single GetDecompressProgress() { }
        // RVA: 0x06997194  token: 0x6000284
        public Beyond.SDK.HGDownloader.WorkState GetWorkState() { }
        // RVA: 0x069971F4  token: 0x6000285
        public System.Boolean Tick() { }
        // RVA: 0x0699700C  token: 0x6000286
        public virtual System.Void Dispose() { }
        // RVA: 0x069981DC  token: 0x6000287
        private System.Void _StartDownload() { }
        // RVA: 0x06997E4C  token: 0x6000288
        private System.Void _StartDownloadImpl() { }
        // RVA: 0x06997260  token: 0x6000289
        private System.Collections.Generic.List<Beyond.SDK.HGFolder> _GetAllFileList(Beyond.Resource.HotUpdate.LocalResPref localResPref) { }
        // RVA: 0x0699773C  token: 0x600028A
        private System.Collections.Generic.List<Beyond.SDK.HGFileInfo> _GetFileList(System.String rootUrl) { }
        // RVA: 0x06997B98  token: 0x600028B
        private System.Void _OnDownloadFinish() { }
        // RVA: 0x0699797C  token: 0x600028C
        private System.Boolean _OnDownloadError(Beyond.SDK.HGRetCodeType codeType, System.Int32 errorCode) { }
        // RVA: 0x06997BE8  token: 0x600028D
        private System.Void _OnDownloadPaused(Beyond.SDK.HGDownloader.PauseReason reason) { }

    }

    // TypeToken: 0x200007D  // size: 0x28
    public class LoginGameSettingInitializer : UnityEngine.MonoBehaviour
    {
        // Fields
        private static System.Boolean s_isInitialized;  // static @ 0x0
        private UnityEngine.RectTransform m_rectTransform;  // 0x18
        private Beyond.UI.UICanvasScaleHelper m_canvasScaleHelper;  // 0x20

        // Methods
        // RVA: 0x03BFCCE0  token: 0x6000293
        private System.Void Awake() { }
        // RVA: 0x03BFCD40  token: 0x6000294
        private System.Void OnEnable() { }
        // RVA: 0x03BFCE00  token: 0x6000295
        private System.Void _TryInitializeComponents() { }
        // RVA: 0x03BFCDC0  token: 0x6000296
        private System.Void _InitGameSettingStateInLogin() { }
        // RVA: 0x03BFD130  token: 0x6000297
        private System.Void _InitGameSettingNotchPadding() { }
        // RVA: 0x05393520  token: 0x6000298
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007E  // size: 0x30
    public class LoginGameUpdateNode : Node
    {
        // Fields
        private Beyond.Login.LoginGameUpdater m_sdk;  // 0x20
        private Beyond.Login.NetUsagePolicy m_netUsagePolicy;  // 0x28

        // Properties
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: 0x049B17C0 */ }

        // Methods
        // RVA: 0x045E87D0  token: 0x600029A
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x045E88E0  token: 0x600029B
        private System.Collections.IEnumerator _GameUpdateCoroutine() { }
        // RVA: 0x06998CC0  token: 0x600029C
        private System.Collections.IEnumerator _GameUpdateCoroutineMobileImpl() { }
        // RVA: 0x041E1670  token: 0x600029D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000081  // size: 0x50
    public class LoginGameUpdater
    {
        // Fields
        private static System.Single COOLDOWN_INSTALL;  // const
        private static System.Int32 ERROR_CODE_NETWORK;  // const
        private static System.Int32 ERROR_INIT_FAILED;  // const
        private static System.Int32 ERROR_DESERIALIZE_FAILED;  // const
        public static System.Int32 ERROR_ACTION;  // const
        private static System.Int32 WAIT_TIME_SECONDS_MIN;  // const
        private static System.Int32 WAIT_TIME_SECONDS_MAX;  // const
        private Beyond.Login.LoginGameUpdater.Options m_options;  // 0x10
        private Beyond.SDK.IGameUpdateInterface m_sdkImpl;  // 0x28
        private System.Boolean m_hasUpdateAlerted;  // 0x30
        private Beyond.Login.LoginGameUpdater.Context m_context;  // 0x38
        private Beyond.Login.LoginProgress m_progress;  // 0x40
        private Beyond.Login.LoginHotUpdateProp m_hotUpdateProp;  // 0x48

        // Methods
        // RVA: 0x045E8E10  token: 0x60002AA
        public System.Void Clear() { }
        // RVA: 0x0380A530  token: 0x60002AB
        public System.Void .ctor(Beyond.Login.LoginGameUpdater.Options options) { }
        // RVA: 0x06998D50  token: 0x60002AC
        public System.Collections.IEnumerator CheckLastGameInfo(Beyond.Login.LoginGameUpdater.Result result) { }
        // RVA: 0x06998DF8  token: 0x60002AD
        public System.Collections.IEnumerator DoUpdate(Beyond.Login.LoginGameUpdater.Result result) { }
        // RVA: 0x0699929C  token: 0x60002AE
        private System.Collections.IEnumerator _GetLastGameInfo(Beyond.Login.LoginGameUpdater.Context context) { }
        // RVA: 0x0699958C  token: 0x60002AF
        private System.Collections.IEnumerator _UpdateGame(Beyond.Login.LoginGameUpdater.Context context) { }
        // RVA: 0x069991F4  token: 0x60002B0
        private System.Collections.IEnumerator _DoTaskUpdating(Beyond.Login.LoginGameUpdater.Context context) { }
        // RVA: 0x06999444  token: 0x60002B1
        private System.Void _OnDownloadStart() { }
        // RVA: 0x06999344  token: 0x60002B2
        private System.Void _OnDownloadProgress(System.Int64 curSize, System.Int64 totalSize, System.Int64 speed) { }
        // RVA: 0x06999774  token: 0x60002B3
        private System.Collections.IEnumerator _YieldErrorAlert(System.String desc) { }
        // RVA: 0x069996D4  token: 0x60002B4
        private System.Collections.IEnumerator _YieldCheckNetUsagePolicy(System.Int64 downloadSize) { }
        // RVA: 0x06999634  token: 0x60002B5
        private System.Collections.IEnumerator _WaitForTaskCancel(System.Int64 taskId) { }
        // RVA: 0x06999188  token: 0x60002B6
        private static System.Void _CreateDirectoryIfNotExists(System.String folderPath) { }
        // RVA: 0x03A80A00  token: 0x60002B7
        public System.Void ClearWaitProgress() { }
        // RVA: 0x06999044  token: 0x60002B8
        public System.Void ShowWaitProgress() { }
        // RVA: 0x06998EA0  token: 0x60002B9
        public System.Int32 GetNoAlertCode() { }
        // RVA: 0x06998EF0  token: 0x60002BA
        public System.Single GetWaitTime() { }

    }

    // TypeToken: 0x2000097  // size: 0x28
    public class LoginGSNode : Node
    {
        // Fields
        private static System.Int32 FLOW_NONE;  // const
        private static System.Int32 FLOW_WORKING;  // const
        private static System.Int32 FLOW_FINISH;  // const
        private static System.Int32 FLOW_ABORTING;  // const
        private Beyond.Login.LoginGSNode.EnterGameServiceFlow m_flow;  // 0x20

        // Properties
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: 0x049B17F0 */ }

        // Methods
        // RVA: 0x049D61A0  token: 0x60002FE
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x0470B2D0  token: 0x60002FF
        public virtual System.Boolean OnEvent(Beyond.ELoginEvent evt, System.Object param) { }
        // RVA: 0x03A847F0  token: 0x6000300
        private static System.Collections.IEnumerator _ConfirmServer() { }
        // RVA: 0x06998718  token: 0x6000301
        private System.Void _FallbackToInitWithMask(System.Boolean bLogoutAccount) { }
        // RVA: 0x06998A38  token: 0x6000302
        private System.Void _OnEventLogout() { }
        // RVA: 0x06998B7C  token: 0x6000303
        private System.Void _OnEventResourceRepair() { }
        // RVA: 0x0699897C  token: 0x6000304
        private System.Boolean _LogoutToInitNode() { }
        // RVA: 0x06998698  token: 0x6000305
        private System.Boolean _FallbackToInitNodeImpl() { }
        // RVA: 0x0699889C  token: 0x6000306
        private System.Boolean _FallbackToResRepairImpl() { }
        // RVA: 0x041E1670  token: 0x6000307
        public System.Void .ctor() { }
        // RVA: 0x06998560  token: 0x6000308
        private System.Void <_OnEventLogout>b__12_0() { }
        // RVA: 0x0699856C  token: 0x6000309
        private System.Void <_OnEventResourceRepair>b__13_0() { }
        // RVA: 0x06998558  token: 0x600030A
        public System.Boolean <>iFixBaseProxy_OnEvent(Beyond.ELoginEvent P0, System.Object P1) { }

    }

    // TypeToken: 0x20000A1  // size: 0x30
    public class LoginHotUpdateProp : Beyond.Login.LoginProperty`1
    {
        // Methods
        // RVA: 0x03A80AD0  token: 0x6000347
        public System.Void .ctor(Beyond.Login.LoginWorkFlow workflow) { }

    }

    // TypeToken: 0x20000A2  // size: 0x28
    public class LoginHotUpdateModel
    {
        // Fields
        public System.String title;  // 0x10
        public System.String desc;  // 0x18
        public Beyond.Login.LoginHotUpdateModel.IProgress progress;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000348
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A5  // size: 0x50
    public class LoginHotUpdater : UnityEngine.CustomYieldInstruction, System.IDisposable
    {
        // Fields
        private static System.Int32 MODE_SKIP;  // const
        private static System.Int32 MODE_DOWNLOAD;  // const
        private static System.Int32 MODE_LOCAL;  // const
        private static System.Boolean s_hasInitialAssetChanged;  // static @ 0x0
        private static System.Boolean s_isNeedSoftRestart;  // static @ 0x1
        private Beyond.LoginContext m_loginCxt;  // 0x10
        private System.Collections.IEnumerator m_mainRoutine;  // 0x18
        private Beyond.Login.LoginProgress m_progress;  // 0x20
        private Beyond.Login.LoginHotUpdateProp m_hotupdateProp;  // 0x28
        private Beyond.Login.LoginDownloadTask m_downloadTask;  // 0x30
        private Beyond.Login.LoginLocalResTask m_localResTask;  // 0x38
        private System.Collections.IEnumerator m_downloadingShaderWarmupRoutine;  // 0x40
        private System.Boolean m_resRepairMode;  // 0x48
        public static System.String URL_GET_SPLIT;  // const

        // Properties
        System.Boolean keepWaiting { get; /* RVA: 0x04CEB9B0 */ }

        // Methods
        // RVA: 0x0699997C  token: 0x6000350
        protected static System.Void MarkInitialAssetChanged() { }
        // RVA: 0x0699981C  token: 0x6000351
        protected static System.Boolean CheckInitialAssetChanged() { }
        // RVA: 0x069999E8  token: 0x6000352
        protected static System.Void MarkNeedClearFolder(System.Boolean isInitial) { }
        // RVA: 0x06999ADC  token: 0x6000353
        protected static System.Boolean NeedClearFolder(System.Boolean isInitial) { }
        // RVA: 0x069998F4  token: 0x6000354
        protected static System.Void MarkClearSuccessed(System.Boolean isInitial) { }
        // RVA: 0x06999A70  token: 0x6000355
        public static System.Void MarkSoftRestart() { }
        // RVA: 0x06999888  token: 0x6000356
        public static System.Void ClearSoftRestartFlag() { }
        // RVA: 0x03406070  token: 0x6000357
        public static System.Boolean IsNeedSoftRestart() { }
        // RVA: 0x03A80D30  token: 0x6000358
        private System.Void .ctor(Beyond.LoginContext context, System.Boolean resRepairMode) { }
        // RVA: 0x046DD1E0  token: 0x600035A
        public virtual System.Void Dispose() { }
        // RVA: 0x03A81090  token: 0x600035B
        public static Beyond.Login.LoginHotUpdater StartHotUpdate(Beyond.LoginContext context, System.Boolean resRepairMode) { }
        // RVA: 0x03A81140  token: 0x600035C
        private System.Collections.IEnumerator _Work() { }
        // RVA: 0x03C07550  token: 0x600035D
        private System.Collections.IEnumerator _CreateMainCoroutine(Beyond.Login.LoginHotUpdater.WorkContext workContext) { }
        // RVA: 0x0455F7A0  token: 0x600035E
        private System.Collections.IEnumerator _MainCoroutineModeSkip(Beyond.Login.LoginHotUpdater.WorkContext context) { }
        // RVA: 0x0699AEC0  token: 0x600035F
        private System.Collections.IEnumerator _MainCoroutineModeLocal(Beyond.Login.LoginHotUpdater.WorkContext context) { }
        // RVA: 0x03C074C0  token: 0x6000360
        private System.Collections.IEnumerator _UpdateResVersion(Beyond.Login.LoginHotUpdater.WorkContext workContext) { }
        // RVA: 0x03C079C0  token: 0x6000361
        private static System.Void _SetResVersionForLog() { }
        // RVA: 0x0699ABA8  token: 0x6000362
        private System.String _GetOnlineResVersion(System.Boolean isInitial) { }
        // RVA: 0x06999B60  token: 0x6000363
        public System.String ResourceRootUrl(System.String version, System.Boolean isInitial) { }
        // RVA: 0x0699B5B4  token: 0x6000364
        private System.Collections.IEnumerator _UpdateResVersionDev(Beyond.Login.LoginHotUpdater.WorkContext workContext) { }
        // RVA: 0x04B45C40  token: 0x6000365
        private System.Collections.IEnumerator _UpdateResVersionProd(Beyond.Login.LoginHotUpdater.WorkContext workContext) { }
        // RVA: 0x0699B1AC  token: 0x6000366
        private System.String _ResourcePatchUrl(System.String version, System.Boolean isInitial) { }
        // RVA: 0x0699AF68  token: 0x6000367
        private System.String _ResourceFileUrl(System.String version, System.Boolean isInitial) { }
        // RVA: 0x0699B0E8  token: 0x6000368
        private System.String _ResourcePatchRootUrl(System.String version, System.Boolean isInitial) { }
        // RVA: 0x0699B020  token: 0x6000369
        private System.String _ResourceIndexUrl(System.String version, System.Boolean isInitial) { }
        // RVA: 0x0699AE18  token: 0x600036A
        private System.Collections.IEnumerator _MainCoroutineModeDownload(Beyond.Login.LoginHotUpdater.WorkContext workContext) { }
        // RVA: 0x0699B264  token: 0x600036B
        private System.Void _StartDownloadingShaderWarmupIfNeeded() { }
        // RVA: 0x0699B524  token: 0x600036C
        private System.Void _StopDownloadingShaderWarmupIfNeeded() { }
        // RVA: 0x04B21B20  token: 0x600036D
        private System.Collections.IEnumerator _ReloadResourceIndexes(Beyond.Login.LoginHotUpdater.WorkContext workContext) { }
        // RVA: 0x0699AC7C  token: 0x600036E
        private System.Collections.IEnumerator _LoadOrDownloadResIndex(System.String onlineVer, System.Boolean isInitial, Beyond.Misc.ObjectRef resIndexRef) { }
        // RVA: 0x0699A9F0  token: 0x600036F
        private System.Collections.IEnumerator _GenResIndex(System.String onlineVer, System.Boolean isInitial, Beyond.Login.LoginHotUpdater.WorkContext workContext) { }
        // RVA: 0x0699AACC  token: 0x6000370
        private System.Collections.IEnumerator _GenRiff(System.String onlineVer, System.Boolean isInitial, Beyond.Login.LoginHotUpdater.WorkContext workContext) { }
        // RVA: 0x0699A004  token: 0x6000371
        private static Beyond.Resource.HotUpdate.LocalResPref _CreateUpdatedLocalResPref(System.String onlineVer, System.Boolean isInitial, Beyond.Resource.HotUpdate.LocalResPref oldLocalResPref, Beyond.Resource.HotUpdate.FResourceDiff resDiff, System.Int32& removedFileCount) { }
        // RVA: 0x0699A920  token: 0x6000372
        private System.Collections.IEnumerator _GenLocalRef(System.String onlineVer, System.Boolean isInitial, Beyond.Login.LoginHotUpdater.WorkContext workContext) { }
        // RVA: 0x06999D78  token: 0x6000373
        private System.Collections.IEnumerator _CheckAndDownloadResource(System.String onlineVer, System.Boolean isInitial, Beyond.Login.LoginHotUpdater.WorkContext workContext) { }
        // RVA: 0x0699A808  token: 0x6000374
        private System.Void _EventLogDownloadProgress(System.Boolean& first, System.Double current, System.Double total, System.Int32 target, System.Boolean isInitial) { }
        // RVA: 0x0699A72C  token: 0x6000375
        private System.Collections.IEnumerator _DownloadPatchInfo(System.String onlineVer, System.Boolean isInitial, Beyond.Misc.ObjectRef patchInfoRef) { }
        // RVA: 0x06999F30  token: 0x6000376
        private System.Collections.IEnumerator _ClearDownloadFolder(Beyond.Login.LoginHotUpdater.WorkContext workContext, System.Boolean needDeleteInitial, System.Boolean needDeleteMain) { }
        // RVA: 0x06999E54  token: 0x6000377
        private System.Collections.IEnumerator _CheckLocalResConsistency(System.String onlineVer, System.Boolean isInitial, Beyond.Login.LoginHotUpdater.WorkContext context) { }
        // RVA: 0x0699B63C  token: 0x6000378
        private System.Collections.IEnumerator _VitalError(System.String error) { }
        // RVA: 0x0699AD58  token: 0x6000379
        private static System.Collections.IEnumerator _LoadStreamingIndex(System.Boolean isInitial, Beyond.Login.LoginHotUpdater.WorkContext workContext, Beyond.Misc.ObjectRef resultRef) { }
        // RVA: 0x06999C1C  token: 0x600037A
        public static System.Void WriteLocalResPrefToFile(Beyond.Resource.HotUpdate.LocalResPref localResPref, Beyond.Login.LoginHotUpdater.WorkContext workContext) { }
        // RVA: 0x0455F830  token: 0x600037B
        private static System.Int32 _CurrentWorkMode(Beyond.LoginContext context) { }

    }

    // TypeToken: 0x20000BF  // size: 0x50
    public class LoginProgress : IProgress
    {
        // Fields
        public static System.Int32 MODE_PROG;  // const
        public static System.Int32 MODE_SIZE;  // const
        public static System.Int32 MODE_IDEL;  // const
        public System.Int32 mode;  // 0x10
        public System.Double current;  // 0x18
        public System.Double total;  // 0x20
        public System.Double speed;  // 0x28
        private Beyond.Login.LoginProgress.FDownloadSize m_downloadSize;  // 0x30

        // Methods
        // RVA: 0x069A1480  token: 0x6000402
        public virtual System.Double GetCurrent() { }
        // RVA: 0x069A157C  token: 0x6000403
        public virtual System.Double GetTotal() { }
        // RVA: 0x069A14D0  token: 0x6000404
        public virtual System.String GetInfo() { }
        // RVA: 0x041E1670  token: 0x6000405
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C2  // size: 0x28
    public class LoginInitNode : Node
    {
        // Fields
        private System.Boolean m_isSDKInited;  // 0x20

        // Properties
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: 0x049B1790 */ }

        // Methods
        // RVA: 0x045E8940  token: 0x600040F
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x045E8A20  token: 0x6000410
        private System.Collections.IEnumerator _InitCoroutine() { }
        // RVA: 0x0484D6F0  token: 0x6000411
        private System.Collections.IEnumerator _InitRemoteConfigs() { }
        // RVA: 0x069A1310  token: 0x6000412
        private System.Collections.IEnumerator _YieldAlertError(System.String errorInfo) { }
        // RVA: 0x041E1670  token: 0x6000413
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C6  // size: 0x20
    public class LoginLocalResTask : System.IDisposable
    {
        // Fields
        private Beyond.LoginContext m_loginCxt;  // 0x10
        private Beyond.Login.LoginHotUpdater.WorkContext m_workCxt;  // 0x18

        // Methods
        // RVA: 0x032AF420  token: 0x6000426
        public System.Void .ctor(Beyond.LoginContext loginContext, Beyond.Login.LoginHotUpdater.WorkContext workContext) { }
        // RVA: 0x069A13F0  token: 0x6000427
        public System.Collections.IEnumerator Work() { }
        // RVA: 0x069A13AC  token: 0x6000428
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x20000C9  // size: 0x38
    public class PreloadNode : Node
    {
        // Fields
        private Beyond.Login.LoginHotUpdateModel.SimpleProgress m_progress;  // 0x20
        private System.Boolean m_getServerListSuc;  // 0x28
        private System.String m_errorMsg;  // 0x30

        // Properties
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: 0x049B16A0 */ }

        // Methods
        // RVA: 0x03A811B0  token: 0x6000433
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x03A80B70  token: 0x6000434
        private System.Void _ShowPreloadResourcePanel() { }
        // RVA: 0x03A81370  token: 0x6000435
        private System.Collections.IEnumerator _LoginPreLoad() { }
        // RVA: 0x069A2564  token: 0x6000436
        private System.Void _QuitGame(Beyond.GameAppExitCode exitCode) { }
        // RVA: 0x0455F560  token: 0x6000437
        private System.Collections.IEnumerator _WarmUp() { }
        // RVA: 0x0455F4F0  token: 0x6000438
        private System.Collections.IEnumerator _GetServerList() { }
        // RVA: 0x03C11630  token: 0x6000439
        private System.Void _EnterPreLoad() { }
        // RVA: 0x040EADB0  token: 0x600043A
        private System.Collections.IEnumerator _GetServerListInfoMock() { }
        // RVA: 0x069A2418  token: 0x600043B
        private System.Collections.IEnumerator _GetServerListInfo() { }
        // RVA: 0x040EA1F0  token: 0x600043C
        private System.Void _OnGetServerListSuc() { }
        // RVA: 0x069A24A8  token: 0x600043D
        private System.Void _OnGetServerListFail(System.String errorInfo) { }
        // RVA: 0x047318D0  token: 0x600043E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D1  // size: 0x28
    public class LoginSDKInitNode : Node
    {
        // Fields
        private System.Boolean m_isSDKInited;  // 0x20

        // Properties
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: 0x049B1760 */ }

        // Methods
        // RVA: 0x045E8A90  token: 0x6000464
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x045E8B70  token: 0x6000465
        private System.Collections.IEnumerator _SDKInitCoroutine() { }
        // RVA: 0x03C90130  token: 0x6000466
        private System.Collections.IEnumerator _WaitForSDKInited() { }
        // RVA: 0x03C90200  token: 0x6000467
        private System.Collections.IEnumerator _InitSubSDKs(System.String env) { }
        // RVA: 0x041E1670  token: 0x6000468
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D5  // size: 0x28
    public class U8LoginNode : Node
    {
        // Fields
        private System.Boolean m_loginSuc;  // 0x20
        private System.Boolean m_isSDKLoginFinished;  // 0x21
        private System.Boolean m_isWaitingGameProtocolUpdateCheck;  // 0x22
        private System.Int32 m_rqstSkipLoginCache;  // 0x24

        // Properties
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: 0x049B1610 */ }

        // Methods
        // RVA: 0x045E8BE0  token: 0x600047C
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x045E8CC0  token: 0x600047D
        private System.Collections.IEnumerator _LoginCoroutine() { }
        // RVA: 0x04D32A40  token: 0x600047E
        private System.Collections.IEnumerator _FinishLogin() { }
        // RVA: 0x069AA6E8  token: 0x600047F
        public virtual System.Boolean OnEvent(Beyond.ELoginEvent evt, System.Object param) { }
        // RVA: 0x040EAE20  token: 0x6000480
        private System.Void _StartSDKLogin() { }
        // RVA: 0x040EB1E0  token: 0x6000481
        private System.Void _SetCursorOnLoginStart() { }
        // RVA: 0x040EB260  token: 0x6000482
        private System.Void _SetCursorOnLoginFinish() { }
        // RVA: 0x069AA940  token: 0x6000483
        private System.Void _SkipSDKLogin() { }
        // RVA: 0x03A7F950  token: 0x6000484
        private System.Void _ClearLoginCaches() { }
        // RVA: 0x01509B20  token: 0x6000485
        private System.Void _StartGameProtocolUpdateCheck() { }
        // RVA: 0x01509160  token: 0x6000486
        private System.Void _OnGameProtocolUpdateCheckSucceeded() { }
        // RVA: 0x01508230  token: 0x6000487
        private System.Void _OnGameProtocolUpdateCheckFailed() { }
        // RVA: 0x01507380  token: 0x6000488
        private System.Void _ClearGameProtocolUpdateCheckEvents() { }
        // RVA: 0x041E5BC0  token: 0x6000489
        private System.Void _OnLoginSuc() { }
        // RVA: 0x069AA818  token: 0x600048A
        private System.Void _OnLoginFail(System.Object rawInfo) { }
        // RVA: 0x041E7AC0  token: 0x600048B
        private System.Void _OnSDKViewDisabled() { }
        // RVA: 0x069AA76C  token: 0x600048C
        private System.Void _OnLoginCanceled() { }
        // RVA: 0x041E5E00  token: 0x600048D
        private System.Void _EnableBlurMask(System.Boolean enable) { }
        // RVA: 0x041E1670  token: 0x600048E
        public System.Void .ctor() { }
        // RVA: 0x01505980  token: 0x600048F
        private System.Void <_StartSDKLogin>b__10_0(System.String sessionToken) { }
        // RVA: 0x01506540  token: 0x6000490
        private System.Void <_StartSDKLogin>b__10_1(System.Object pram) { }
        // RVA: 0x06998558  token: 0x6000491
        public System.Boolean <>iFixBaseProxy_OnEvent(Beyond.ELoginEvent P0, System.Object P1) { }

    }

    // TypeToken: 0x20000D8
    public class LoginProperty`1 : IWorkerUpdateOnce
    {
        // Fields
        private T m_value;  // 0x0
        private System.Collections.Generic.HashSet<Beyond.Login.ILoginBinder<T>> m_binders;  // 0x0
        private Beyond.Login.LoginWorkFlow m_workflow;  // 0x0
        private System.Boolean <isDataChanged>k__BackingField;  // 0x0

        // Properties
        System.Boolean isDataChanged { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        T value { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600049E
        public System.Void .ctor(Beyond.Login.LoginWorkFlow workflow) { }
        // RVA: -1  // not resolved  token: 0x60004A2
        public System.Void NotifyDataChanged() { }
        // RVA: -1  // not resolved  token: 0x60004A3
        public System.Void Bind(Beyond.Login.ILoginBinder<T> binder) { }
        // RVA: -1  // not resolved  token: 0x60004A4
        public virtual System.Void Worker_UpdateOnce() { }

    }

    // TypeToken: 0x20000D9
    public interface ILoginBinder`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60004A5
        public virtual System.Void OnValueChanged(T value) { }

    }

    // TypeToken: 0x20000DA
    public interface ILoginContextReceiver
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60004A6
        public virtual System.Void OnLoginContext(Beyond.LoginContext context) { }

    }

    // TypeToken: 0x20000DB  // size: 0x30
    public class LoginViewModel
    {
        // Fields
        public Beyond.Login.LoginWorkFlow.ENode workCachedCurNode;  // 0x10
        public Beyond.Login.LoginWorkFlow.ENode workCurNode;  // 0x14
        public System.Boolean isFirstLogin;  // 0x18
        private System.Int32 <rqstSkipU8Login>k__BackingField;  // 0x1c
        public System.Boolean isU8LoginCancelled;  // 0x20
        private System.Int32 <rqstResRepairCode>k__BackingField;  // 0x24
        public Beyond.Login.LoginViewModel.FAgeTipsPolicy ageTipsPolicy;  // 0x28

        // Properties
        System.Int32 rqstSkipU8Login { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x04D86320 */ }
        System.Int32 rqstResRepairCode { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }

        // Methods
        // RVA: 0x04CD95A0  token: 0x60004A7
        public System.Void .ctor() { }
        // RVA: 0x03408B00  token: 0x60004AC
        public System.Boolean IsAgeTipsVisible() { }
        // RVA: 0x034089C0  token: 0x60004AD
        public System.Boolean IsGovTipsVisible() { }
        // RVA: 0x03407160  token: 0x60004AE
        public System.Boolean IsMenuVisible() { }
        // RVA: 0x069A15CC  token: 0x60004AF
        public System.Void RequestResourceRepair() { }
        // RVA: 0x069A1618  token: 0x60004B0
        public System.Void RequestSkipU8Login() { }
        // RVA: 0x04CD95D0  token: 0x60004B1
        private static Beyond.Login.LoginViewModel.FAgeTipsPolicy _LoadAgeTipsPolicy() { }

    }

    // TypeToken: 0x20000DD  // size: 0x48
    public class LoginViewProperty : Beyond.Login.LoginProperty`1
    {
        // Fields
        private Beyond.Login.LoginHotUpdateProp <hotUpdateProp>k__BackingField;  // 0x30
        private Beyond.Login.LoginAgeTipsProp <ageTipsProp>k__BackingField;  // 0x38
        private Beyond.Login.LoginServerSelectProp <serverSelectProp>k__BackingField;  // 0x40

        // Properties
        Beyond.Login.LoginHotUpdateProp hotUpdateProp { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        Beyond.Login.LoginAgeTipsProp ageTipsProp { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        Beyond.Login.LoginServerSelectProp serverSelectProp { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }

        // Methods
        // RVA: 0x03A7F7F0  token: 0x60004B9
        public System.Void .ctor(Beyond.Login.LoginWorkFlow workflow) { }

    }

    // TypeToken: 0x20000DE  // size: 0x20
    public class LoginAgeTipsModel
    {
        // Fields
        public System.String contentCache;  // 0x10
        public System.Boolean isShow;  // 0x18
        public System.Boolean isFetchingContent;  // 0x19

        // Methods
        // RVA: 0x03A809C0  token: 0x60004BA
        public System.Boolean IsContentEmpty() { }
        // RVA: 0x04B480F0  token: 0x60004BB
        public System.Boolean CanDisplay() { }
        // RVA: 0x041E1670  token: 0x60004BC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DF  // size: 0x30
    public class LoginAgeTipsProp : Beyond.Login.LoginProperty`1
    {
        // Methods
        // RVA: 0x03A80950  token: 0x60004BD
        public System.Void .ctor(Beyond.Login.LoginWorkFlow workflow) { }
        // RVA: 0x069A119C  token: 0x60004BE
        public System.Void ClearContentCache() { }
        // RVA: 0x069A1260  token: 0x60004BF
        public System.Void UpdateContent(System.String content) { }

    }

    // TypeToken: 0x20000E0  // size: 0x50
    public class LoginWorkFlow : System.IDisposable
    {
        // Fields
        public static System.String CURSOR_KEY_LOGIN;  // const
        private static System.Boolean s_hasEnteredPreloadThisSession;  // static @ 0x0
        private static readonly Beyond.Login.LoginWorkFlow.ENode[] NODE_ORDER;  // static @ 0x8
        private static Beyond.Login.LoginWorkFlow.ENode NEED_GAME_LOGIN_STATE_BEFORE_NODE;  // const
        private readonly Beyond.Login.LoginWorkFlow.Node[] m_nodes;  // 0x10
        private readonly Beyond.LoginContext m_context;  // 0x18
        private Beyond.Login.LoginWorkFlow.FWork m_work;  // 0x20
        private Beyond.Login.LoginWorkFlow.Worker m_worker;  // 0x30
        private System.Collections.Generic.HashSet<Beyond.Login.LoginWorkFlow.IEventReceiver> m_evtRecvrs;  // 0x38
        private System.Collections.Generic.List<Beyond.Login.LoginWorkFlow.IEventReceiver> m_recvrBuffer;  // 0x40
        private System.Boolean <isDisposed>k__BackingField;  // 0x48

        // Properties
        System.Boolean isDisposed { get; /* RVA: 0x04D867D0 */ set; /* RVA: 0x04D86810 */ }

        // Methods
        // RVA: 0x03A82A90  token: 0x60004C0
        public static System.Void MarkEnteredPreloadThisSession() { }
        // RVA: 0x069A1788  token: 0x60004C1
        public static System.Void ClearEnteredPreloadThisSession() { }
        // RVA: 0x069A1A5C  token: 0x60004C2
        public static System.Boolean HasEnteredPreloadThisSession() { }
        // RVA: 0x04731570  token: 0x60004C3
        private Beyond.Login.LoginWorkFlow.Node[] _RegisterNodes() { }
        // RVA: 0x03406190  token: 0x60004C6
        public System.Void .ctor(Beyond.LoginContext context) { }
        // RVA: 0x041E4DE0  token: 0x60004C7
        public System.Collections.IEnumerator DoWorkFlow() { }
        // RVA: 0x03406310  token: 0x60004C8
        public virtual System.Void Dispose() { }
        // RVA: 0x046DD390  token: 0x60004C9
        public System.Boolean PeekFallbackStatus(Beyond.Login.LoginWorkFlow.ENode& fallbackNode) { }
        // RVA: 0x069A1800  token: 0x60004CA
        public System.Boolean Fallback(Beyond.Login.LoginWorkFlow.ENode target, System.Boolean validCheck) { }
        // RVA: 0x069A16F4  token: 0x60004CB
        public System.Boolean CancelAndFallback(Beyond.Login.LoginWorkFlow.ENode target, System.Boolean validCheck) { }
        // RVA: 0x0470AF20  token: 0x60004CC
        public System.Void SendEvent(Beyond.ELoginEvent evt, System.Object param) { }
        // RVA: 0x069A1664  token: 0x60004CD
        public System.Void AddEventReceiver(Beyond.Login.LoginWorkFlow.IEventReceiver receiver) { }
        // RVA: 0x0450CAF0  token: 0x60004CE
        public System.Boolean UpdateOnce(Beyond.Login.LoginWorkFlow.IWorkerUpdateOnce inst) { }
        // RVA: 0x034077A0  token: 0x60004CF
        public static System.Boolean IsBeforeWork(Beyond.Login.LoginWorkFlow.ENode curNode, Beyond.Login.LoginWorkFlow.ENode check) { }
        // RVA: 0x034088B0  token: 0x60004D0
        public static System.Boolean IsOnOrAfterWork(Beyond.Login.LoginWorkFlow.ENode curNode, Beyond.Login.LoginWorkFlow.ENode check) { }
        // RVA: 0x069A1AD4  token: 0x60004D1
        public static System.Boolean IsAfterWork(Beyond.Login.LoginWorkFlow.ENode target, Beyond.Login.LoginWorkFlow.ENode check) { }
        // RVA: 0x0470B260  token: 0x60004D2
        private System.Boolean _HandleGlobalEvent(Beyond.ELoginEvent evt, System.Object param) { }
        // RVA: 0x069A1E60  token: 0x60004D3
        private System.Void _EventOnOpenAgeTips() { }
        // RVA: 0x069A1B98  token: 0x60004D4
        private System.Void _EventOnCloseAgeTips() { }
        // RVA: 0x069A1FE0  token: 0x60004D5
        private System.Void _EventOnOpenServerSelect(System.Boolean isOpen) { }
        // RVA: 0x069A20B0  token: 0x60004D6
        private System.Void _EventOnSelectServer(Beyond.Gameplay.ServerZoneManager.ServerZoneData serverZoneData) { }
        // RVA: 0x069A1CC4  token: 0x60004D7
        private System.Void _EventOnConfirmServer() { }
        // RVA: 0x04B2C680  token: 0x60004D8
        private System.Void _EnableCursor(System.Boolean enabled) { }
        // RVA: 0x069A2238  token: 0x60004D9
        private System.Void _UpdateCursor(Beyond.EventData<Beyond.DeviceInfo.InputType>& input) { }
        // RVA: 0x049D6BB0  token: 0x60004DA
        private System.Void _EnbaleToggleGroup() { }
        // RVA: 0x069A2190  token: 0x60004DB
        private System.Collections.IEnumerator _FetchAgeTipsCoroutine(Beyond.Login.LoginAgeTipsProp prop) { }
        // RVA: 0x03406D40  token: 0x60004DC
        private static System.Boolean _CheckIfGameStateNotMatch(Beyond.Login.LoginWorkFlow.ENode curNode) { }
        // RVA: 0x049B82F0  token: 0x60004DD
        private static System.Collections.IEnumerator _AdjustCurrentGameState(Beyond.Login.LoginWorkFlow.ENode curNode) { }
        // RVA: 0x04CE9E50  token: 0x60004DE
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000EB  // size: 0x30
    public class HotUpdateNode : Node
    {
        // Fields
        private Beyond.Login.LoginHotUpdater m_updater;  // 0x20
        private System.Int32 m_cachedResRepairCode;  // 0x28

        // Properties
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: 0x049B18E0 */ }

        // Methods
        // RVA: 0x03A80FC0  token: 0x6000523
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x046DD0A0  token: 0x6000524
        public virtual System.Void OnDispose() { }
        // RVA: 0x041E1670  token: 0x6000525
        public System.Void .ctor() { }
        // RVA: 0x06997004  token: 0x6000526
        public System.Void <>iFixBaseProxy_OnDispose() { }

    }

    // TypeToken: 0x20000EC  // size: 0x28
    public abstract class WaitForSecondsNode : Node
    {
        // Fields
        private System.Nullable<System.Int32> m_timerId;  // 0x20

        // Methods
        // RVA: 0x03A81490  token: 0x6000527
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x036A6140  token: 0x6000528
        private System.Boolean _IsTimeout() { }
        // RVA: 0x04D39810  token: 0x6000529
        private System.Void _NotifyTimeout() { }
        // RVA: 0x046DD170  token: 0x600052A
        private System.Void _DisposeTimer() { }
        // RVA: -1  // abstract  token: 0x600052B
        protected virtual System.Single WaitForSeconds(System.Boolean& unscaled) { }
        // RVA: 0x046DD100  token: 0x600052C
        public virtual System.Void OnDispose() { }
        // RVA: 0x041E1670  token: 0x600052D
        protected System.Void .ctor() { }
        // RVA: 0x06997004  token: 0x600052E
        public System.Void <>iFixBaseProxy_OnDispose() { }

    }

    // TypeToken: 0x20000ED  // size: 0x28
    public class PreloadPostDelayNode : Beyond.Login.WaitForSecondsNode
    {
        // Properties
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: 0x049B1670 */ }

        // Methods
        // RVA: 0x04D79D50  token: 0x6000530
        protected virtual System.Single WaitForSeconds(System.Boolean& unscaled) { }
        // RVA: 0x03A813E0  token: 0x6000531
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x03A80C70  token: 0x6000532
        private System.Void _DisableLoadingPanel() { }
        // RVA: 0x041E1670  token: 0x6000533
        public System.Void .ctor() { }
        // RVA: 0x069A25EC  token: 0x6000534
        public UnityEngine.CustomYieldInstruction <>iFixBaseProxy_Work() { }

    }

    // TypeToken: 0x20000EE  // size: 0x28
    public class Trans2GsLoginNode : Beyond.Login.WaitForSecondsNode
    {
        // Properties
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: 0x049B1640 */ }

        // Methods
        // RVA: 0x041E5CA0  token: 0x6000536
        protected virtual System.Single WaitForSeconds(System.Boolean& unscaled) { }
        // RVA: 0x03A81420  token: 0x6000537
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x041E1670  token: 0x6000538
        public System.Void .ctor() { }
        // RVA: 0x069A25EC  token: 0x6000539
        public UnityEngine.CustomYieldInstruction <>iFixBaseProxy_Work() { }

    }

    // TypeToken: 0x20000EF  // size: 0x20
    public class FinishNode : Node
    {
        // Properties
        Beyond.Login.LoginWorkFlow.ENode type { get; /* RVA: 0x049B1940 */ }

        // Methods
        // RVA: 0x04A31020  token: 0x600053B
        public virtual UnityEngine.CustomYieldInstruction Work() { }
        // RVA: 0x041E1670  token: 0x600053C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F0  // size: 0x20
    public sealed struct CarrierDownloadCache
    {
        // Fields
        public static readonly Beyond.Login.CarrierDownloadCache EMPTY;  // static @ 0x0
        public System.Int64 size;  // 0x10
        public System.Boolean isAllowed;  // 0x18

        // Methods
        // RVA: 0x04D56A10  token: 0x600053D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000F1  // size: 0x30
    public class NetUsagePolicy
    {
        // Fields
        private static System.Int32 NOT_REACHABLE_ERROR_CODE;  // const
        private Beyond.Login.CarrierDownloadCache m_mobileDataCache;  // 0x10
        private UnityEngine.NetworkReachability m_lastNetworkState;  // 0x20
        private Beyond.Login.NetUsagePolicy.Options m_options;  // 0x28

        // Methods
        // RVA: 0x0380AB00  token: 0x600053E
        public System.Void .ctor(Beyond.Login.NetUsagePolicy.Options options) { }
        // RVA: 0x069B43E4  token: 0x600053F
        public System.Void CheckIfAllowDownload(System.Int64 downloadSize, System.Action onAllowed, System.Action onRejected, System.Action onReachable) { }
        // RVA: 0x069B438C  token: 0x6000540
        public System.Boolean AllowMobileDataInLastCheck() { }
        // RVA: 0x069B4734  token: 0x6000541
        private static System.String _GetPrettySizeStringUpToMB(System.Double number) { }

    }

    // TypeToken: 0x20000F4  // size: 0xD8
    public class LoginSceneAnimCtrl : Beyond.Login.LoginSceneComponent, Beyond.Login.ILoginBinder`1, UnityEngine.IAnimationClipSource
    {
        // Fields
        private static System.Single COUNT_DOWN_EMPTY;  // const
        private UnityEngine.Animator _target;  // 0x78
        private UnityEngine.AnimationClip _clipA1;  // 0x80
        private UnityEngine.AnimationClip _clipA1s;  // 0x88
        private System.Single _transA1;  // 0x90
        private System.Single _transA1s;  // 0x94
        private UnityEngine.Vector2 _randomRangeA1s;  // 0x98
        private UnityEngine.AnimationClip _clipA1A2;  // 0xa0
        private System.Single _predelayA1A2;  // 0xa8
        private System.Single _transA1A2;  // 0xac
        private UnityEngine.AnimationClip _clipA2;  // 0xb0
        private System.Single _transA2;  // 0xb8
        private Beyond.UI.UIAnimationMixPlayer m_player;  // 0xc0
        private Beyond.Login.LoginSceneAnimCtrl.EState m_state;  // 0xc8
        private System.Single m_countDownA1s;  // 0xcc
        private System.Single m_countDownA1A2;  // 0xd0

        // Properties
        Beyond.TickType tickOption { get; /* RVA: 0x047333E0 */ }

        // Methods
        // RVA: 0x041E5760  token: 0x6000546
        protected virtual System.Void OnBindToManager() { }
        // RVA: 0x03D4FF30  token: 0x6000547
        private Beyond.UI.UIAnimationMixPlayer _EnsurePlayer() { }
        // RVA: 0x03408830  token: 0x6000548
        private System.Void _ChangeToState(Beyond.Login.LoginSceneAnimCtrl.EState target) { }
        // RVA: 0x03D4F930  token: 0x6000549
        private System.Void _ResetToA1() { }
        // RVA: 0x049B8B60  token: 0x600054A
        private System.Void _TransitionToA2() { }
        // RVA: 0x03D4FD90  token: 0x600054B
        private System.Void _PlayA1sAndTriggerNext() { }
        // RVA: 0x03D50000  token: 0x600054C
        private System.Single _RandomIntervalA1s() { }
        // RVA: 0x03D4FB60  token: 0x600054D
        private System.Void _PlayA1A2Impl() { }
        // RVA: 0x03408790  token: 0x600054E
        public virtual System.Void OnValueChanged(Beyond.Login.LoginViewModel value) { }
        // RVA: 0x0340A1D0  token: 0x600054F
        protected virtual System.Void Start() { }
        // RVA: 0x02F446B0  token: 0x6000550
        public virtual System.Void Tick(System.Single deltaTime) { }
        // RVA: 0x04994870  token: 0x6000551
        protected virtual System.Void OnRelease() { }
        // RVA: 0x069B1654  token: 0x6000552
        public virtual System.Void GetAnimationClips(System.Collections.Generic.List<UnityEngine.AnimationClip> results) { }
        // RVA: 0x04B6CD40  token: 0x6000553
        public System.Void .ctor() { }
        // RVA: 0x069B16B8  token: 0x6000554
        public Beyond.TickType <>iFixBaseProxy_get_tickOption() { }
        // RVA: 0x069B16A8  token: 0x6000555
        public System.Void <>iFixBaseProxy_OnBindToManager() { }
        // RVA: 0x069B16B0  token: 0x6000556
        public System.Void <>iFixBaseProxy_Start() { }
        // RVA: 0x01538B50  token: 0x6000557
        public System.Void <>iFixBaseProxy_Tick(System.Single P0) { }
        // RVA: 0x069AFD04  token: 0x6000558
        public System.Void <>iFixBaseProxy_OnRelease() { }

    }

    // TypeToken: 0x20000F6  // size: 0x78
    public abstract class LoginSceneComponent : Beyond.TickableMono
    {
        // Fields
        private Beyond.LoginContext <context>k__BackingField;  // 0x68
        private Beyond.LoginController <controller>k__BackingField;  // 0x70

        // Properties
        Beyond.TickType tickOption { get; /* RVA: 0x069B16C0 */ }
        Beyond.LoginContext context { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }
        Beyond.LoginController controller { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }

        // Methods
        // RVA: 0x0340A300  token: 0x600055E
        protected virtual System.Void Start() { }
        // RVA: 0x041E60C0  token: 0x600055F
        public System.Void Bind(Beyond.LoginContext context, Beyond.LoginController controller) { }
        // RVA: 0x041E5AA0  token: 0x6000560
        protected virtual System.Void OnBindToManager() { }
        // RVA: 0x039C2B00  token: 0x6000561
        protected System.Void .ctor() { }
        // RVA: 0x069AFD0C  token: 0x6000562
        public Beyond.TickType <>iFixBaseProxy_get_tickOption() { }

    }

    // TypeToken: 0x20000F7  // size: 0x78
    public class LoginSceneRoot : Beyond.Login.LoginSceneComponent
    {
        // Properties
        Beyond.TickType tickOption { get; /* RVA: 0x04D51C40 */ }

        // Methods
        // RVA: 0x0380F010  token: 0x6000564
        protected virtual System.Void OnBindToManager() { }
        // RVA: 0x039C2B00  token: 0x6000565
        public System.Void .ctor() { }
        // RVA: 0x069B16B8  token: 0x6000566
        public Beyond.TickType <>iFixBaseProxy_get_tickOption() { }
        // RVA: 0x069B16A8  token: 0x6000567
        public System.Void <>iFixBaseProxy_OnBindToManager() { }

    }

    // TypeToken: 0x20000F8  // size: 0x68
    public class LoginAgeTipsDetailPanel : UnityEngine.MonoBehaviour, Beyond.Login.ILoginBinder`1
    {
        // Fields
        private Beyond.UI.UIAnimationWrapper _anim;  // 0x18
        private UnityEngine.RectTransform _content;  // 0x20
        private Beyond.UI.UIText _txtTitle;  // 0x28
        private Beyond.UI.UIText _lineTmpl;  // 0x30
        private Beyond.UI.UIButton _btnClose;  // 0x38
        private Beyond.UI.UIScrollRect _scroll;  // 0x40
        private Beyond.Input.InputBindingGroupMonoTarget _ageTipsBindingGroup;  // 0x48
        private Beyond.LoginContext m_context;  // 0x50
        private Beyond.UI.UISwitchTween m_switchTween;  // 0x58
        private DG.Tweening.Tween m_displayDelayTween;  // 0x60
        private static System.Int32 MAX_TEXT_STRLEN;  // const
        private static System.Int32 MIN_TEXT_STRLEN;  // const

        // Methods
        // RVA: 0x042B6AA0  token: 0x6000568
        public System.Void Init(Beyond.LoginContext context) { }
        // RVA: 0x04B48030  token: 0x6000569
        public virtual System.Void OnValueChanged(Beyond.Login.LoginAgeTipsModel value) { }
        // RVA: 0x069AF530  token: 0x600056A
        private System.Void _RenderContent(System.String contentStr) { }
        // RVA: 0x069AF6A0  token: 0x600056B
        private System.Void _ShowContent() { }
        // RVA: 0x069AF4B8  token: 0x600056C
        private System.Void _EventOnCloseClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069AF6FC  token: 0x600056D
        private static System.Collections.Generic.List<System.String> _SplitLicenseToSegments(System.String content) { }
        // RVA: 0x069AF9E0  token: 0x600056E
        private static System.Void _SplitLongString(System.String longStr, System.Collections.Generic.List<System.String> outputList) { }
        // RVA: 0x05393520  token: 0x600056F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F9  // size: 0x48
    public class LoginAgeTipsPanel : ChildPanel, Beyond.Login.ILoginBinder`1
    {
        // Fields
        private Beyond.Login.LoginAgeTipsDetailPanel _detailPanel;  // 0x28
        private Beyond.UI.UIButton _ageTipsCN;  // 0x30
        private UnityEngine.GameObject _ageTipsKR;  // 0x38
        private UnityEngine.CanvasGroup _canvasVersionTop;  // 0x40

        // Methods
        // RVA: 0x042B61D0  token: 0x6000570
        protected virtual System.Void OnInit() { }
        // RVA: 0x03408A70  token: 0x6000571
        public virtual System.Void OnValueChanged(Beyond.Login.LoginViewModel value) { }
        // RVA: 0x03409910  token: 0x6000572
        private System.Void _UpdateAgeTipsPolicy(Beyond.Login.LoginViewModel.FAgeTipsPolicy policy) { }
        // RVA: 0x069AFAB4  token: 0x6000573
        private System.Void _EventOnOpenAgeTipsClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x05393520  token: 0x6000574
        public System.Void .ctor() { }
        // RVA: 0x069AFAAC  token: 0x6000575
        public System.Void <>iFixBaseProxy_OnInit() { }

    }

    // TypeToken: 0x20000FA  // size: 0x58
    public class LoginAlertDialog : Dialog
    {
        // Fields
        private Beyond.UI.UIButton _btnConfirm;  // 0x38
        private Beyond.UI.UIText _text;  // 0x40
        private Beyond.Login.LoginAlertDialog.FParams m_params;  // 0x48

        // Methods
        // RVA: 0x069AFB2C  token: 0x6000576
        protected virtual System.Void OnInit() { }
        // RVA: 0x069AFC10  token: 0x6000577
        public System.Void SetParams(Beyond.Login.LoginAlertDialog.FParams param) { }
        // RVA: 0x069AFCA0  token: 0x6000578
        private System.Void _OnConfirmClicked(UnityEngine.EventSystems.PointerEventData evt) { }
        // RVA: 0x05393520  token: 0x6000579
        public System.Void .ctor() { }
        // RVA: 0x069AFC98  token: 0x600057A
        public System.Void <>iFixBaseProxy_OnInit() { }

    }

    // TypeToken: 0x20000FC  // size: 0xE0
    public class LoginDecorateUI : Beyond.TickableUIMono, Beyond.Login.ILoginBinder`1, Beyond.Login.ILoginContextReceiver
    {
        // Fields
        private UnityEngine.CanvasGroup _imgLogo;  // 0x68
        private System.Single _durationLogo;  // 0x70
        private UnityEngine.AnimationClip _clipLoopGlow;  // 0x78
        private UnityEngine.AnimationClip _clipInGlow;  // 0x80
        private UnityEngine.Animator _targetGlow;  // 0x88
        private System.Single _delay;  // 0x90
        private Beyond.LoginContext m_context;  // 0x98
        private System.Boolean m_isOnTransNode;  // 0xa0
        private Beyond.Interpolator.FLerpByTime m_hideLogoLerp;  // 0xa8
        private Beyond.UI.UIAnimationLayerMixPlayer m_glowPlayer;  // 0xc8
        private System.Boolean m_isGlowEnabled;  // 0xd0
        private System.Single m_countDownDelay;  // 0xd4
        private System.Single m_countDownDisableGlow;  // 0xd8

        // Properties
        Beyond.TickType tickOption { get; /* RVA: 0x04A357C0 */ }

        // Methods
        // RVA: 0x041E4C00  token: 0x600057C
        public virtual System.Void OnLoginContext(Beyond.LoginContext context) { }
        // RVA: 0x03408BB0  token: 0x600057D
        public virtual System.Void OnValueChanged(Beyond.Login.LoginViewModel value) { }
        // RVA: 0x03D4F5A0  token: 0x600057E
        private System.Void _TriggerTrans2GSLoginImpl() { }
        // RVA: 0x03409D20  token: 0x600057F
        private System.Void _ToggleGlow(System.Boolean enable) { }
        // RVA: 0x03D4F720  token: 0x6000580
        private Beyond.UI.UIAnimationLayerMixPlayer _EnsureGlowPlayer() { }
        // RVA: 0x03D4F360  token: 0x6000581
        public virtual System.Void Tick(System.Single deltaTime) { }
        // RVA: 0x049947F0  token: 0x6000582
        protected virtual System.Void OnRelease() { }
        // RVA: 0x03D4F3D0  token: 0x6000583
        private System.Void _TimeManagerTick(System.Single timeDelta) { }
        // RVA: 0x04CB1AE0  token: 0x6000584
        public System.Void .ctor() { }
        // RVA: 0x069AFD0C  token: 0x6000585
        public Beyond.TickType <>iFixBaseProxy_get_tickOption() { }
        // RVA: 0x01538B50  token: 0x6000586
        public System.Void <>iFixBaseProxy_Tick(System.Single P0) { }
        // RVA: 0x069AFD04  token: 0x6000587
        public System.Void <>iFixBaseProxy_OnRelease() { }

    }

    // TypeToken: 0x20000FD  // size: 0x58
    public class LoginEnterGamePanel : ChildPanel, Beyond.Login.ILoginBinder`1
    {
        // Fields
        private UnityEngine.CanvasGroup _canvasGroup;  // 0x28
        private Beyond.UI.UIButton _btnEnterGame;  // 0x30
        private Beyond.UI.UIText _textEnterGame;  // 0x38
        private System.Single _delayEnterGame;  // 0x40
        private System.Boolean m_isPanelActive;  // 0x44
        private System.Double m_panelActiveTime;  // 0x48
        private System.Boolean m_isInited;  // 0x50

        // Methods
        // RVA: 0x04B6B370  token: 0x6000588
        private System.Void _InitIfNot() { }
        // RVA: 0x03406E00  token: 0x6000589
        public virtual System.Void OnValueChanged(Beyond.Login.LoginViewModel value) { }
        // RVA: 0x0470AE70  token: 0x600058A
        private System.Void _OnEnterGameClicked(UnityEngine.EventSystems.PointerEventData evt) { }
        // RVA: 0x04D11A70  token: 0x600058B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FE  // size: 0x30
    public class LoginGovTipsPanel : ChildPanel, Beyond.Login.ILoginBinder`1
    {
        // Fields
        private Beyond.UI.UIButton _btnOpen;  // 0x28

        // Methods
        // RVA: 0x042B6100  token: 0x600058C
        protected virtual System.Void OnInit() { }
        // RVA: 0x03408960  token: 0x600058D
        public virtual System.Void OnValueChanged(Beyond.Login.LoginViewModel value) { }
        // RVA: 0x069AFD14  token: 0x600058E
        private System.Void _EventOnOpenAgeTipsClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x05393520  token: 0x600058F
        public System.Void .ctor() { }
        // RVA: 0x069AFAAC  token: 0x6000590
        public System.Void <>iFixBaseProxy_OnInit() { }

    }

    // TypeToken: 0x20000FF  // size: 0x98
    public class LoginJudgeDialog : Dialog
    {
        // Fields
        private Beyond.UI.UIButton _btnPositive;  // 0x38
        private Beyond.UI.UIButton _btnNegative;  // 0x40
        private Beyond.UI.UIText _text;  // 0x48
        private Beyond.UI.UIText _text2;  // 0x50
        private Beyond.UI.UIText _textPositive;  // 0x58
        private Beyond.UI.UIText _textNegative;  // 0x60
        private Beyond.Login.LoginJudgeDialog.FParams m_params;  // 0x68

        // Methods
        // RVA: 0x069AFD7C  token: 0x6000591
        protected virtual System.Void OnInit() { }
        // RVA: 0x069AFEC8  token: 0x6000592
        public System.Void SetParams(Beyond.Login.LoginJudgeDialog.FParams param) { }
        // RVA: 0x069B0170  token: 0x6000593
        private System.Void _OnPositiveClicked(UnityEngine.EventSystems.PointerEventData evt) { }
        // RVA: 0x069B010C  token: 0x6000594
        private System.Void _OnNegativeClicked(UnityEngine.EventSystems.PointerEventData evt) { }
        // RVA: 0x05393520  token: 0x6000595
        public System.Void .ctor() { }
        // RVA: 0x069AFC98  token: 0x6000596
        public System.Void <>iFixBaseProxy_OnInit() { }

    }

    // TypeToken: 0x2000102  // size: 0xF0
    public class LoginMenuPanel : ChildPanel, Beyond.Login.ILoginBinder`1
    {
        // Fields
        public static System.String CONTROLLER_STATE_NORMAL;  // const
        public static System.String CONTROLLER_STATE_SELECT;  // const
        private Beyond.UI.UIButton _btnAnnounce;  // 0x28
        private Beyond.UI.UIButton _btnSetting;  // 0x30
        private Beyond.UI.UIButton _btnResCheck;  // 0x38
        private Beyond.UI.UIButton _btnLogout;  // 0x40
        private Beyond.UI.UIButton _btnQuitGame;  // 0x48
        private Beyond.UI.UIButton _btnLogin;  // 0x50
        private Beyond.UI.UIButton _btnQR;  // 0x58
        private Beyond.UI.UIState.UIStateController _ctrlAnnounce;  // 0x60
        private Beyond.UI.UIState.UIStateController _ctrlSetting;  // 0x68
        private Beyond.UI.UIState.UIStateController _ctrlResCheck;  // 0x70
        private Beyond.UI.UIState.UIStateController _ctrlLogout;  // 0x78
        private Beyond.UI.UIState.UIStateController _ctrlQR;  // 0x80
        private Beyond.UI.UISimpleStateController _ctrlRight;  // 0x88
        private Beyond.UI.UISelectableNaviGroup _naviGroupRight;  // 0x90
        private Beyond.Input.InputBindingGroupMonoTarget _bindingGroupForKeyboard;  // 0x98
        private Beyond.Input.InputBindingGroupMonoTarget _rootPanelBindingGroup;  // 0xa0
        private UnityEngine.CanvasGroup _alphaHandler;  // 0xa8
        private UnityEngine.GameObject _redDotAnnounce;  // 0xb0
        private Beyond.UI.UIAnimationWrapper _transAnim;  // 0xb8
        private Beyond.UI.UIButton _serverZoneBtn;  // 0xc0
        private Beyond.UI.UIText _serverZoneName;  // 0xc8
        private UnityEngine.GameObject _serverZonePS5;  // 0xd0
        private Beyond.UI.UIText _serverZoneNamePS5;  // 0xd8
        private Beyond.UI.UIState.UIStateController m_curRightBtnCtrl;  // 0xe0
        private System.Boolean m_hasRedDot;  // 0xe8
        private System.Int32 m_rootParentGroupId;  // 0xec

        // Methods
        // RVA: 0x039D2890  token: 0x6000597
        protected virtual System.Void OnInit() { }
        // RVA: 0x03407440  token: 0x6000598
        public virtual System.Void OnValueChanged(Beyond.Login.LoginViewModel value) { }
        // RVA: 0x041E7B00  token: 0x6000599
        public System.Void InitBulletinAndRedAfterLoginSuc() { }
        // RVA: 0x069B08EC  token: 0x600059A
        private static System.Void _OnBulletinOpen() { }
        // RVA: 0x069B0854  token: 0x600059B
        private static System.Void _OnBulletinClose() { }
        // RVA: 0x04CAA620  token: 0x600059C
        private System.Void Awake() { }
        // RVA: 0x040101A0  token: 0x600059D
        private System.Void OnEnable() { }
        // RVA: 0x040103A0  token: 0x600059E
        private System.Void OnDisable() { }
        // RVA: 0x048AB5F0  token: 0x600059F
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x069B0604  token: 0x60005A0
        private System.Void _OnAnnounceClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B1168  token: 0x60005A1
        private System.Void _OnSettingClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B1034  token: 0x60005A2
        private System.Void _OnResCheckClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B096C  token: 0x60005A3
        private System.Void _OnCustomServiceClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B053C  token: 0x60005A4
        private System.Void _OnAccountCenterClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B0CB0  token: 0x60005A5
        private System.Void _OnLogoutClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B0E28  token: 0x60005A6
        private System.Void _OnQuitGameClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B0C38  token: 0x60005A7
        private System.Void _OnLoginClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B0D78  token: 0x60005A8
        private System.Void _OnQRClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B0FC8  token: 0x60005A9
        private System.Void _OnRequestRelogin() { }
        // RVA: 0x04D576D0  token: 0x60005AA
        private System.Boolean _CanQRLogin() { }
        // RVA: 0x041E9090  token: 0x60005AB
        private System.Void _UpdateAnnounceRedDot() { }
        // RVA: 0x03A85290  token: 0x60005AC
        private System.Void _UpdateServerZone() { }
        // RVA: 0x069B10FC  token: 0x60005AD
        private System.Void _OnServerZoneBtnClicked() { }
        // RVA: 0x069B0A1C  token: 0x60005AE
        private System.Void _OnFocusMenuGroup(System.Boolean isFocused) { }
        // RVA: 0x069B07B4  token: 0x60005AF
        private System.Void _OnAnnounceClosed() { }
        // RVA: 0x041E99E0  token: 0x60005B0
        private System.Void _OnReceiveRedDotAndPopup(System.Boolean showRedDot) { }
        // RVA: 0x069B1254  token: 0x60005B1
        private System.Void _OnUpdateRedDotForSDKListener(Beyond.EventData<System.Boolean>& show) { }
        // RVA: 0x069B0B0C  token: 0x60005B2
        private System.Void _OnInputChanged(Beyond.EventData<Beyond.DeviceInfo.InputType>& input) { }
        // RVA: 0x04D11A90  token: 0x60005B3
        public System.Void .ctor() { }
        // RVA: 0x069B01D4  token: 0x60005B4
        private System.Void <OnInit>b__28_0(System.Boolean isTarget, System.Boolean isGroupChanged, System.Boolean isOnNaviTargetEnabledAgain) { }
        // RVA: 0x069B0260  token: 0x60005B5
        private System.Void <OnInit>b__28_1(System.Boolean isTarget, System.Boolean isGroupChanged, System.Boolean isOnNaviTargetEnabledAgain) { }
        // RVA: 0x069B02EC  token: 0x60005B6
        private System.Void <OnInit>b__28_2(System.Boolean isTarget, System.Boolean isGroupChanged, System.Boolean isOnNaviTargetEnabledAgain) { }
        // RVA: 0x069B0378  token: 0x60005B7
        private System.Void <OnInit>b__28_3(System.Boolean isTarget, System.Boolean isGroupChanged, System.Boolean isOnNaviTargetEnabledAgain) { }
        // RVA: 0x069B0404  token: 0x60005B8
        private System.Void <OnInit>b__28_4(System.Boolean isTarget, System.Boolean isGroupChanged, System.Boolean isOnNaviTargetEnabledAgain) { }
        // RVA: 0x069B0498  token: 0x60005B9
        private System.Void <OnInit>b__28_5(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B04A0  token: 0x60005BA
        private System.Void <OnInit>b__28_6(System.Boolean isFocused) { }
        // RVA: 0x069B04B0  token: 0x60005BB
        private System.Void <_OnAnnounceClicked>b__37_0(System.String s) { }
        // RVA: 0x069AFAAC  token: 0x60005BC
        public System.Void <>iFixBaseProxy_OnInit() { }
        // RVA: 0x069B04A8  token: 0x60005BD
        public System.Void <>iFixBaseProxy_OnDestroy() { }

    }

    // TypeToken: 0x2000104  // size: 0x98
    public class LoginMockUIDPanel : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.CanvasGroup _canvas;  // 0x18
        private Beyond.UI.UIInputField _userIdInputField;  // 0x20
        private Beyond.UI.UIButton _btnEnterGame;  // 0x28
        private Beyond.UI.UIButton _btnDelLocalFile;  // 0x30
        private Beyond.UI.UIDropdown _serverDropdown;  // 0x38
        private UnityEngine.GameObject _hintSelectServer;  // 0x40
        private Beyond.UI.UIButton _btnClose;  // 0x48
        private Beyond.UI.UIText _selectedServerLabel;  // 0x50
        private System.Boolean m_isInited;  // 0x58
        private Beyond.UI.UISwitchTween m_switchTween;  // 0x60
        private System.Collections.Generic.List<Beyond.Network.ServerDesc> m_serverDescList;  // 0x68
        private Beyond.Network.ServerList m_serverListObj;  // 0x70
        private Beyond.Network.ServerConfig m_serverConfig;  // 0x78
        private System.Action<System.String> m_callback;  // 0x80
        private Beyond.Login.LoginMockUIDPanel.FOptions m_options;  // 0x88
        private static System.String LAST_SELECTED_SERVER_PREF_KEY;  // const
        private static Beyond.Login.LoginMockUIDPanel s_instance;  // static @ 0x0

        // Properties
        Beyond.Login.LoginMockUIDPanel instance { get; /* RVA: 0x069B14B0 */ }

        // Methods
        // RVA: 0x03F0EF20  token: 0x60005C2
        public System.Void LoginWithCallback(Beyond.Login.LoginMockUIDPanel.FOptions options, System.Action<System.String> callback) { }
        // RVA: 0x069B12B8  token: 0x60005C3
        public System.Void Refresh() { }
        // RVA: 0x03F0E5A0  token: 0x60005C4
        public System.Collections.IEnumerator HideAndDestroy() { }
        // RVA: 0x03F0FEA0  token: 0x60005C5
        private System.Void _UpdateStatus() { }
        // RVA: 0x03DA3B90  token: 0x60005C6
        private System.Void _InitLoginInfo() { }
        // RVA: 0x03F4A940  token: 0x60005C7
        private System.Void _MovePreferredServerToFront(System.Int32& curServerIndex) { }
        // RVA: 0x03DA4A40  token: 0x60005C8
        private System.Int32 _IndexOfServer(System.String serverName) { }
        // RVA: 0x03F4B630  token: 0x60005C9
        private System.Void _SaveLastSelectedServer(System.String serverName) { }
        // RVA: 0x03DA4240  token: 0x60005CA
        private System.Void _SaveConfig() { }
        // RVA: 0x04CE84A0  token: 0x60005CB
        private System.Void _OnBtnEnterGame() { }
        // RVA: 0x069B135C  token: 0x60005CC
        private System.Void _SmokeEnterGame(System.String serverName, System.String uid) { }
        // RVA: 0x069B1318  token: 0x60005CD
        private System.Void _OnBtnDelLocalFile() { }
        // RVA: 0x069B1414  token: 0x60005CE
        private System.Void _TryClosePanel() { }
        // RVA: 0x03F4AB20  token: 0x60005CF
        private System.Void _OnSelectServer(System.Int32 index) { }
        // RVA: 0x04D51270  token: 0x60005D0
        private System.Void OnDestroy() { }
        // RVA: 0x05393520  token: 0x60005D1
        public System.Void .ctor() { }
        // RVA: 0x04CE8490  token: 0x60005D2
        private System.Void <_UpdateStatus>b__23_0(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B1308  token: 0x60005D3
        private System.Void <_UpdateStatus>b__23_1(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B1310  token: 0x60005D4
        private System.Void <_UpdateStatus>b__23_2(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x03F4ABC0  token: 0x60005D5
        private System.Void <_InitLoginInfo>b__24_0(System.Int32 index, Beyond.UI.UIDropdownOption options, System.Boolean isSelected) { }

    }

    // TypeToken: 0x2000107  // size: 0x88
    public class LoginResourcePanel : ChildPanel, Beyond.Login.ILoginBinder`1
    {
        // Fields
        private static System.Single PROGRESS_LERP_DURATION;  // const
        private UnityEngine.CanvasGroup _alphaHandler;  // 0x28
        private Beyond.UI.UISlider _progressBar;  // 0x30
        private Beyond.UI.UIText _textTitle;  // 0x38
        private Beyond.UI.UIText _textInfo;  // 0x40
        private Beyond.Login.LoginHotUpdateModel.IProgress m_progress;  // 0x48
        private Beyond.UI.TweenUtils.FLerpByTime m_progressLerper;  // 0x50
        private System.String m_resInfoCache;  // 0x80

        // Methods
        // RVA: 0x049C89F0  token: 0x60005DD
        public virtual System.Void OnValueChanged(Beyond.Login.LoginHotUpdateModel value) { }
        // RVA: 0x039367F0  token: 0x60005DE
        private System.Void _UpdateResourceInfo() { }
        // RVA: 0x03935650  token: 0x60005DF
        private System.Void Update() { }
        // RVA: 0x05393520  token: 0x60005E0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000108  // size: 0x60
    public class LoginRootPanel : UnityEngine.MonoBehaviour
    {
        // Fields
        private Beyond.UI.NotchAdapter _notchAdapter;  // 0x18
        private Beyond.Login.LoginMenuPanel _menuPanel;  // 0x20
        private Beyond.Login.LoginAgeTipsPanel _ageTipsPanel;  // 0x28
        private Beyond.Login.LoginGovTipsPanel _govTipsPanel;  // 0x30
        private Beyond.Login.LoginResourcePanel _resPanel;  // 0x38
        private Beyond.Login.LoginEnterGamePanel _enterGamePanel;  // 0x40
        private Beyond.Login.LoginServerSelectPanel _serverSelectPanel;  // 0x48
        private Beyond.Input.InputBindingGroupMonoTarget _bindingGroup;  // 0x50
        private System.Int32 m_loginRootBindingGroupId;  // 0x58

        // Methods
        // RVA: 0x03CD3860  token: 0x60005E1
        private System.Void Awake() { }
        // RVA: 0x03CD3960  token: 0x60005E2
        private System.Void OnDestroy() { }
        // RVA: 0x041E57E0  token: 0x60005E3
        public System.Void Init(Beyond.LoginContext context) { }
        // RVA: 0x069B15B0  token: 0x60005E4
        private System.Void _OnNotchPaddingChanged(Beyond.EventData<System.Single>& data) { }
        // RVA: 0x041E4B40  token: 0x60005E5
        private System.Void _InitController() { }
        // RVA: 0x069B151C  token: 0x60005E6
        public System.Void ToggleLoginBindingGroup(System.Boolean enable) { }
        // RVA: 0x041E7A80  token: 0x60005E7
        public System.Void InitBulletinAndRedAfterLoginSuc() { }
        // RVA: 0x041E58D0  token: 0x60005E8
        private static System.Void _InitChildPanel(Beyond.Login.LoginRootPanel.ChildPanel childPanel, Beyond.LoginContext context) { }
        // RVA: 0x05393520  token: 0x60005E9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010A  // size: 0x68
    public class LoginServerSelectItemView : UnityEngine.MonoBehaviour
    {
        // Fields
        private Beyond.UI.UIText _serverName;  // 0x18
        private Beyond.UI.UIImage _playerIcon;  // 0x20
        private Beyond.UI.UIText _playerLevel;  // 0x28
        private Beyond.UI.UIImage[] _delayImages;  // 0x30
        private Beyond.UI.UIText _delayText;  // 0x38
        private Beyond.UI.UIButton _btnSelect;  // 0x40
        private Beyond.UI.UIState.UIStateController _stateController;  // 0x48
        private static readonly UnityEngine.Color DEFAULT_COLOR;  // static @ 0x0
        private static System.String DEFAULT_TEXT;  // const
        private static readonly Beyond.Login.LoginServerSelectItemView.DelayInfo DEFAULT_SPEED_INFO;  // static @ 0x10
        private Beyond.Gameplay.ServerZoneManager.ServerZoneData m_serverItem;  // 0x50
        private Beyond.LoginContext m_context;  // 0x58
        private System.Collections.Generic.List<Beyond.Login.LoginServerSelectItemView.DelayInfo> m_speedInfos;  // 0x60

        // Methods
        // RVA: 0x069B179C  token: 0x60005F4
        public System.Void RefreshView(Beyond.LoginContext context, Beyond.Gameplay.ServerZoneManager.ServerZoneData serverItem, System.Boolean isSelected) { }
        // RVA: 0x069B170C  token: 0x60005F5
        public System.Void NaviToBtnTarget() { }
        // RVA: 0x069B1CC4  token: 0x60005F6
        public System.Void SetSelected(System.Boolean isSelected) { }
        // RVA: 0x069B1D68  token: 0x60005F7
        private System.Void _OnClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x046093D0  token: 0x60005F8
        public System.Void .ctor() { }
        // RVA: 0x04CB5D40  token: 0x60005F9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200010C  // size: 0x30
    public class LoginServerSelectProp : Beyond.Login.LoginProperty`1
    {
        // Methods
        // RVA: 0x03A7F900  token: 0x60005FA
        public System.Void .ctor(Beyond.Login.LoginWorkFlow workflow) { }

    }

    // TypeToken: 0x200010D  // size: 0x18
    public class LoginServerItem
    {
        // Fields
        public Beyond.Gameplay.ServerZoneManager.ServerZoneData serverZoneData;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60005FB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010E  // size: 0x18
    public class LoginServerSelectModel
    {
        // Fields
        public System.Boolean showPanel;  // 0x10
        public System.Int32 selectedIndex;  // 0x14

        // Properties
        System.Collections.Generic.List<Beyond.Gameplay.ServerZoneManager.ServerZoneData> serverList { get; /* RVA: 0x069B2050 */ }
        Beyond.Gameplay.ServerZoneManager.ServerZoneData curServerZoneData { get; /* RVA: 0x069B1FBC */ }

        // Methods
        // RVA: 0x069B1E64  token: 0x60005FE
        public System.Void InitData() { }
        // RVA: 0x069B1DE0  token: 0x60005FF
        public Beyond.Gameplay.ServerZoneManager.ServerZoneData GetSelectedServer() { }
        // RVA: 0x069B1ECC  token: 0x6000600
        public System.Boolean UpdateSelectedServer(Beyond.Gameplay.ServerZoneManager.ServerZoneData selectedServer) { }
        // RVA: 0x04D8CCE0  token: 0x6000601
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010F  // size: 0x68
    public class LoginServerSelectPanel : ChildPanel, Beyond.Login.ILoginBinder`1
    {
        // Fields
        private Beyond.UI.UIButton _btnBack;  // 0x28
        private Beyond.UI.UIButton _btnConfirm;  // 0x30
        private Beyond.UI.UIAnimationWrapper _showAnim;  // 0x38
        private Beyond.UI.UIScrollList _showList;  // 0x40
        private Beyond.UI.UISelectableNaviGroup _naviGroup;  // 0x48
        private Beyond.Input.InputBindingGroupMonoTarget _bindingGroup;  // 0x50
        private Beyond.Input.InputBindingGroupMonoTarget _rootPanelBindingGroup;  // 0x58
        private Beyond.Login.LoginServerSelectModel m_viewModel;  // 0x60

        // Methods
        // RVA: 0x042B4AF0  token: 0x6000602
        protected virtual System.Void OnInit() { }
        // RVA: 0x069B20E4  token: 0x6000603
        private System.Void OnDisable() { }
        // RVA: 0x069B21E8  token: 0x6000604
        private System.Void OnEnable() { }
        // RVA: 0x069B2EB4  token: 0x6000605
        private System.Collections.IEnumerator _WaitingForShowListAwake() { }
        // RVA: 0x069B2CE8  token: 0x6000606
        private System.Void _UpdateServerCell(UnityEngine.GameObject cell, System.Int32 index) { }
        // RVA: 0x069B2DF4  token: 0x6000607
        private System.Void _UpdateServerSelected(System.Int32 index, UnityEngine.GameObject cell) { }
        // RVA: 0x069B28F4  token: 0x6000608
        public virtual System.Void OnValueChanged(Beyond.Login.LoginServerSelectModel value) { }
        // RVA: 0x069B2A4C  token: 0x6000609
        private System.Void _NaviToSelectedItem() { }
        // RVA: 0x069B2C84  token: 0x600060A
        private System.Void _OnInputChanged(Beyond.EventData<Beyond.DeviceInfo.InputType>& input) { }
        // RVA: 0x069B2B94  token: 0x600060B
        private System.Void _OnCloseClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x069B2C0C  token: 0x600060C
        private System.Void _OnConfirmClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x05393520  token: 0x600060D
        public System.Void .ctor() { }
        // RVA: 0x069AFAAC  token: 0x600060E
        public System.Void <>iFixBaseProxy_OnInit() { }

    }

    // TypeToken: 0x2000112  // size: 0x20
    public class LoginVersionPanel : UnityEngine.MonoBehaviour
    {
        // Fields
        private Beyond.UI.UIText _textVersion;  // 0x18

        // Properties
        System.String displayGameVersion { get; /* RVA: 0x0368CED0 */ }
        System.String versionZoneType { get; /* RVA: 0x0368CD50 */ }
        System.String versionPlatform { get; /* RVA: 0x0368CE30 */ }
        System.String versionAppType { get; /* RVA: 0x0368CE80 */ }
        System.String versionVersionNum { get; /* RVA: 0x038118B0 */ }
        System.String versionProjectCL { get; /* RVA: 0x0368CDC0 */ }

        // Methods
        // RVA: 0x0368CC80  token: 0x6000618
        private System.Void Start() { }
        // RVA: 0x05393520  token: 0x600061F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000113  // size: 0x70
    public class LoginVocResSelDialog : Dialog
    {
        // Fields
        private Beyond.Login.LoginVocResSelDialog.FParams m_params;  // 0x38
        private Beyond.UI.UIButton _btnConfirm;  // 0x40
        private Beyond.UI.UIScrollList _showList;  // 0x48
        private Beyond.UI.UISelectableNaviGroup _naviGroup;  // 0x50
        private Beyond.Input.InputBindingGroupMonoTarget _bindingGroup;  // 0x58
        private System.Collections.Generic.List<Beyond.Login.LoginVocResSelDialog.VocResInfo> m_vocResInfos;  // 0x60
        private System.Int32 m_selectedIndex;  // 0x68

        // Methods
        // RVA: 0x069B3150  token: 0x6000620
        protected virtual System.Void OnInit() { }
        // RVA: 0x069B3AC8  token: 0x6000621
        private System.Int32 _ResolveInitialSelectedIndex() { }
        // RVA: 0x069B3A64  token: 0x6000622
        private System.Void _RefreshConfirmInteractable() { }
        // RVA: 0x069B3048  token: 0x6000623
        private System.Void OnEnable() { }
        // RVA: 0x069B2F44  token: 0x6000624
        private System.Void OnDisable() { }
        // RVA: 0x069B34E4  token: 0x6000625
        public System.Void SetParams(Beyond.Login.LoginVocResSelDialog.FParams param) { }
        // RVA: 0x069B3548  token: 0x6000626
        public System.Void UpdateSelected(Beyond.VFS.EVFSBlockType type) { }
        // RVA: 0x069B3CEC  token: 0x6000627
        private System.Void _UpdateList() { }
        // RVA: 0x069B3DAC  token: 0x6000628
        private System.Void _UpdateSelected(System.Int32 index, UnityEngine.GameObject cell) { }
        // RVA: 0x069B3C0C  token: 0x6000629
        private System.Void _UpdateCell(UnityEngine.GameObject cell, System.Int32 index) { }
        // RVA: 0x069B394C  token: 0x600062A
        private System.Void _OnConfirmClicked(UnityEngine.EventSystems.PointerEventData evt) { }
        // RVA: 0x069B3788  token: 0x600062B
        private System.Void _OnConfirmClicked() { }
        // RVA: 0x069B3A00  token: 0x600062C
        private System.Void _OnInputTypeChanged(Beyond.EventData<Beyond.DeviceInfo.InputType>& data) { }
        // RVA: 0x069B3644  token: 0x600062D
        private System.Void _NaviToSelectedItem() { }
        // RVA: 0x05393520  token: 0x600062E
        public System.Void .ctor() { }
        // RVA: 0x069AFC98  token: 0x600062F
        public System.Void <>iFixBaseProxy_OnInit() { }

    }

    // TypeToken: 0x2000116  // size: 0x48
    public class LoginVocResSelItemView : UnityEngine.MonoBehaviour
    {
        // Fields
        private Beyond.UI.UIText _name;  // 0x18
        private Beyond.UI.UIText _size;  // 0x20
        private Beyond.UI.UIButton _btnSelect;  // 0x28
        private Beyond.UI.UIState.UIStateController _stateController;  // 0x30
        private Beyond.Login.LoginVocResSelDialog.VocResInfo m_info;  // 0x38
        private Beyond.Login.LoginVocResSelDialog m_context;  // 0x40
        private static System.Double MB;  // const

        // Methods
        // RVA: 0x069B3EEC  token: 0x6000631
        public System.Void RefreshView(Beyond.Login.LoginVocResSelDialog context, Beyond.Login.LoginVocResSelDialog.VocResInfo info, System.Boolean isSelected) { }
        // RVA: 0x069B3E5C  token: 0x6000632
        public System.Void NaviToBtnTarget() { }
        // RVA: 0x069B4274  token: 0x6000633
        public System.Void SetSelected(System.Boolean isSelected) { }
        // RVA: 0x069B4318  token: 0x6000634
        private System.Void _OnClicked(UnityEngine.EventSystems.PointerEventData _) { }
        // RVA: 0x05393520  token: 0x6000635
        public System.Void .ctor() { }

    }

}

namespace Beyond.SDK
{

    // TypeToken: 0x2000059  // size: 0x18
    public class PlatformFont : Hypergryph.PlatformFacade.IPlatformFont
    {
        // Fields
        private static System.Int32 DEFAULT_FONT_ID;  // const
        private readonly System.Boolean m_forceChineseFont;  // 0x10

        // Methods
        // RVA: 0x04D86130  token: 0x60001E1
        public System.Void .ctor(System.Boolean forceChineseFont) { }
        // RVA: 0x0699B6E4  token: 0x60001E2
        public virtual TMPro.TMP_FontAsset GetFont() { }

    }

    // TypeToken: 0x200005A  // size: 0x10
    public static class SDKNetUtils
    {
        // Fields
        public static System.String CONTENT_TYPE_JSON;  // const
        private static System.Int32 GENERAL_TIMEOUT;  // const
        private static Beyond.SDK.SDKNetUtils.HTTPImpl s_HTTPImpl;  // static @ 0x0

        // Methods
        // RVA: 0x0699B8D4  token: 0x60001E3
        public static System.Void HTTPPost(Beyond.SDK.SDKNetUtils.HttpPostRequest request) { }
        // RVA: 0x0699B77C  token: 0x60001E4
        public static System.Void HTTPGet(Beyond.SDK.SDKNetUtils.HttpGetRequest request) { }
        // RVA: 0x0699BAC0  token: 0x60001E5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000063  // size: 0x10
    public static class SDKTextDefines
    {
        // Fields
        public static System.String CS_SDK_STO_DESC;  // const
        public static System.String CS_SDK_COMMON_ERROR;  // const
        public static System.String CS_SDK_CREATE_ORDER_ERROR;  // const
        public static System.String CS_SDK_CREATE_ORDER_ERROR_LIMIT_COUNT;  // const
        public static System.String CS_SDK_USR_BAN;  // const
        public static System.String CS_SDK_USR_BAN_TIME;  // const
        public static System.String CS_SDK_LOGIN_FORBID;  // const
        public static System.String CS_SDK_CREATE_ORDER_ERROR_NETWORK_TIMEOUT;  // const
        public static System.String CS_SDK_CREATE_ORDER_ERROR_NETWORK_ERROR;  // const
        public static System.String CS_SDK_CREATE_ORDER_ERROR_TOKEN_INVALID;  // const

    }

}

namespace Beyond.SDK.Impl
{

    // TypeToken: 0x2000064  // size: 0x28
    public class U8ExternalTools : U8.SDK.SDKExternalToolsShortcut
    {
        // Fields
        private static System.Boolean s_inited;  // static @ 0x0
        private static System.Boolean s_initing;  // static @ 0x1
        private static System.Boolean s_needRetry;  // static @ 0x2
        private static System.Boolean s_needQuitGame;  // static @ 0x3
        private static Beyond.SDK.Impl.U8ExternalTools.FHGSDKInit s_initHGSDK;  // static @ 0x4

        // Methods
        // RVA: 0x0699EFE4  token: 0x60001F0
        public static System.Boolean IsInited() { }
        // RVA: 0x03C90D50  token: 0x60001F1
        public static System.Boolean NeedRetryInit() { }
        // RVA: 0x04A427C0  token: 0x60001F2
        public static System.Boolean NeedQuitGame() { }
        // RVA: 0x0699F3C4  token: 0x60001F3
        public static System.Void RetryInitSDKSystem(System.String env) { }
        // RVA: 0x04D225E0  token: 0x60001F4
        public static System.Void PublicSetGameVersion(System.String version) { }
        // RVA: 0x03C8F0A0  token: 0x60001F5
        public static System.Void InitSDKSystem(System.String env) { }
        // RVA: 0x04A42760  token: 0x60001F6
        public static System.Collections.IEnumerator WaitForInited() { }
        // RVA: 0x03AF7760  token: 0x60001F7
        public static System.Void InitHGSDK(System.String env) { }
        // RVA: 0x0699EF78  token: 0x60001F8
        public static System.Boolean IsHGSDKInited() { }
        // RVA: 0x0332A350  token: 0x60001F9
        public static System.Collections.IEnumerator WaitForInitHGSDK() { }
        // RVA: 0x0699F130  token: 0x60001FA
        protected virtual System.Void Log(System.String content) { }
        // RVA: 0x0699F050  token: 0x60001FB
        protected virtual System.Void LogError(System.String content) { }
        // RVA: 0x0699F0DC  token: 0x60001FC
        protected virtual System.Void LogWarning(System.String content) { }
        // RVA: 0x0699EF0C  token: 0x60001FD
        protected virtual System.String GetSignKey() { }
        // RVA: 0x0699EBA0  token: 0x60001FE
        public virtual System.Collections.Generic.Dictionary<System.String,System.String> GetDeviceIDs() { }
        // RVA: 0x0699F464  token: 0x60001FF
        public System.Void SetSubChannel(System.String subChannel) { }
        // RVA: 0x0699EE94  token: 0x6000200
        protected virtual System.Int32 GetPlatformKey() { }
        // RVA: 0x044419B0  token: 0x6000201
        public virtual System.Void OnSDKExtraInfo(System.String jsonData) { }
        // RVA: 0x0699F4D0  token: 0x6000202
        protected virtual System.String U8RootUrl() { }
        // RVA: 0x0699F184  token: 0x6000203
        protected virtual System.Void POSTImplementation(U8.SDK.SDKExternalTools.POSTRequest request, System.Action<U8.SDK.SDKExternalTools.POSTResult> callback) { }
        // RVA: 0x0699F560  token: 0x6000204
        private static U8.SDK.SDKExternalTools.POSTResult _ConvertToPOSTResult(Beyond.SDK.SDKNetUtils.WebHttpResponse response) { }
        // RVA: 0x04441BC0  token: 0x6000205
        private System.Void _ProcessExtraInfo(U8.SDK.SDKExtraData extraData) { }
        // RVA: 0x014FF310  token: 0x6000206
        private static System.Void _ProcessGameProtocolUpdateCheck(System.Collections.Generic.Dictionary<System.String,System.Object> msg) { }
        // RVA: 0x01500F80  token: 0x6000207
        private static System.Int64 _TryGetGameProtocolUpdateCheckErrorCode(System.Collections.Generic.Dictionary<System.String,System.Object> msg) { }
        // RVA: 0x0699FA4C  token: 0x6000208
        private static System.Void _OnRequestExitGame() { }
        // RVA: 0x0699FA0C  token: 0x6000209
        private static System.Void _OnFakeAccountCenter() { }
        // RVA: 0x0699EB24  token: 0x600020A
        protected virtual U8.SDK.SDKCaptchaHandler CreateCaptchaHandler() { }
        // RVA: 0x0699ECD4  token: 0x600020B
        protected virtual System.String GetErrorMessage(U8.SDK.SDKExternalTools.ErrMsgMeta meta) { }
        // RVA: 0x0699F804  token: 0x600020C
        private System.Boolean _ErrorMsg_GET_TOKEN(U8.SDK.SDKExternalTools.ErrMsgMeta meta, System.String& errorMsg) { }
        // RVA: 0x0699F664  token: 0x600020D
        private System.Boolean _ErrorMsg_CREATE_ORDER(U8.SDK.SDKExternalTools.ErrMsgMeta meta, System.String& errorMsg) { }
        // RVA: 0x04D778D0  token: 0x600020E
        public System.Void .ctor() { }
        // RVA: 0x011EC580  token: 0x600020F
        public System.String <>iFixBaseProxy_GetSignKey() { }
        // RVA: 0x0699F4C8  token: 0x6000210
        public System.Collections.Generic.Dictionary<System.String,System.String> <>iFixBaseProxy_GetDeviceIDs() { }
        // RVA: 0x041E1670  token: 0x6000211
        public System.Void <>iFixBaseProxy_OnSDKExtraInfo(System.String P0) { }

    }

    // TypeToken: 0x200006C  // size: 0x10
    public class U8Plugin : U8.SDK.IExternalPlugin
    {
        // Methods
        // RVA: 0x0699FAE8  token: 0x600022E
        public virtual System.String GetPayAddition() { }
        // RVA: 0x0699FB34  token: 0x600022F
        public virtual System.String GetSDKToken() { }
        // RVA: 0x0699FB80  token: 0x6000230
        public virtual System.String GetSDKUid() { }
        // RVA: 0x035A20D0  token: 0x6000231
        public virtual System.Void Init() { }
        // RVA: 0x03AF75B0  token: 0x6000232
        public virtual System.Void Login(U8.SDK.ExternalPluginLoginParams args) { }
        // RVA: 0x0699FBCC  token: 0x6000233
        public virtual System.Void Logout(U8.SDK.ExternalPluginLogoutParams args) { }
        // RVA: 0x0699FC54  token: 0x6000234
        public virtual System.Void OnProductListChanged(System.Collections.Generic.List<U8.SDK.U8ProductInfo> productList) { }
        // RVA: 0x0699FCA8  token: 0x6000235
        public virtual System.Void Pay(U8.SDK.ExternalPluginPayParams args) { }
        // RVA: 0x0699FE8C  token: 0x6000236
        public virtual System.Boolean TryLoadSDKMeta(System.Func<U8.SDK.SDKMeta> loadMetaNative, U8.SDK.SDKMeta& meta) { }
        // RVA: 0x03AF7680  token: 0x6000237
        private static System.Boolean _UseMockVersion() { }
        // RVA: 0x03AF90A0  token: 0x6000238
        private System.Void _MockLogin(U8.SDK.ExternalPluginLoginParams args) { }
        // RVA: 0x041E1670  token: 0x6000239
        public System.Void .ctor() { }

    }

}

namespace Beyond.Scripts.Entry
{

    // TypeToken: 0x2000117  // size: 0x10
    public static class GameSettingSetter
    {
        // Methods
        // RVA: 0x0366F370  token: 0x6000636
        public static System.Void RegisterSettingCallbacks() { }
        // RVA: 0x043F3130  token: 0x6000637
        private static System.Void _OnAudioGlobalVolumeChange(System.Single volume) { }
        // RVA: 0x04D38130  token: 0x6000638
        private static System.Void _OnAudioMusicVolumeChange(System.Single volume) { }
        // RVA: 0x04D380C0  token: 0x6000639
        private static System.Void _OnAudioVoiceVolumeChange(System.Single volume) { }
        // RVA: 0x043F30C0  token: 0x600063A
        private static System.Void _OnAudioSfxVolumeChange(System.Single volume) { }
        // RVA: 0x04CE1800  token: 0x600063B
        private static System.Void _OnAudioSuspendUnfocusedChange(System.Boolean suspendUnfocused) { }
        // RVA: 0x069AB054  token: 0x600063C
        private static System.Void _OnAudioBackgroundMusicChange(System.Boolean enable) { }
        // RVA: 0x03F92AF0  token: 0x600063D
        private static System.Void _OnAudioSuiteModeChange(Beyond.GameSetting.GameSettingAudioSuiteMode mode) { }
        // RVA: 0x04D27280  token: 0x600063E
        private static System.Void _OnAudioControllerChange(System.Boolean enable) { }
        // RVA: 0x04007320  token: 0x600063F
        private static System.Void _OnAudioSpatialAudioChange(System.Boolean enable) { }
        // RVA: 0x043F3290  token: 0x6000640
        private static System.Void _OnCameraDistanceLevelChange(System.Single distance) { }
        // RVA: 0x04D27200  token: 0x6000641
        private static System.Void _OnControllerMotionChange(System.Boolean valid) { }
        // RVA: 0x03F92A00  token: 0x6000642
        private static System.Void _OnKeyboardTypeChange(Beyond.GameSetting.KeyboardLayoutType value) { }
        // RVA: 0x0332ADD0  token: 0x6000643
        private static System.Void _OnLanguageTextChange(Beyond.GEnums.EnvLang language) { }
        // RVA: 0x0366EE20  token: 0x6000644
        private static System.Void _OnLanguageAudioChange(Beyond.GameSetting.GameSettingLanguageAudio language) { }
        // RVA: 0x04B89C60  token: 0x6000645
        private static System.Void _OnVideoQualityChange(Beyond.GameSetting.GameSettingVideoQuality quality) { }
        // RVA: 0x036A4280  token: 0x6000646
        private static System.Void _OnVideoFullScreenChange(System.Boolean fullScreen) { }
        // RVA: 0x0366D880  token: 0x6000647
        private static System.Void _OnVideoResolutionChange(Beyond.GameSetting.ScreenResolution newResolution) { }
        // RVA: 0x036A4300  token: 0x6000648
        private static System.Void _SetResolution(System.Int32 width, System.Int32 height, System.Boolean fullScreen) { }
        // RVA: 0x069AB0A4  token: 0x6000649
        private static System.Void _OnVideoNotchPaddingChange(System.Single notchPadding) { }
        // RVA: 0x03F92820  token: 0x600064A
        private static System.Void _OnTeammateSkillEffectStrengthChange(Beyond.GameSetting.TeammateSkillEffectStrength strength) { }
        // RVA: 0x043F31A0  token: 0x600064B
        private static System.Void _OnTeammateSkillEffectOpacityChange(System.Single opacity) { }

    }

    // TypeToken: 0x2000118  // size: 0x10
    public class WarmUpProgress : IProgress
    {
        // Methods
        // RVA: 0x069B4FDC  token: 0x600064C
        public virtual System.Double GetTotal() { }
        // RVA: 0x069B4EE0  token: 0x600064D
        public virtual System.Double GetCurrent() { }
        // RVA: 0x069B4F70  token: 0x600064E
        public virtual System.String GetInfo() { }
        // RVA: 0x041E1670  token: 0x600064F
        public System.Void .ctor() { }

    }

}

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x200004D  // size: 0x20
    public class DataNAttribute : System.Attribute
    {
        // Fields
        public System.Int32 capacity;  // 0x10
        public System.Int32 tSize;  // 0x14
        public System.Boolean ring;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60001BC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004E  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x60001BD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x60001BE
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000050  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60001BF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000051  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60001C0
        public System.Void .ctor() { }

    }

}

namespace Beyond.UI
{

    // TypeToken: 0x2000052  // size: 0x80
    public class SplashController : UnityEngine.MonoBehaviour
    {
        // Fields
        private static System.String CURSOR_KEY_SPLASH;  // const
        private static System.String SPLASH_RESOURCES_PATH;  // const
        private System.Single _initialFadeTime;  // 0x18
        private System.Single _regularFadeTime;  // 0x1c
        private System.Single _showTime;  // 0x20
        private UnityEngine.UI.Image _blackCoverImage;  // 0x28
        private UnityEngine.UI.Image _splashImage;  // 0x30
        private Beyond.UI.SplashController.SplashConfigs _splashConfigsCN;  // 0x38
        private Beyond.UI.SplashController.SplashConfigs _splashConfigsOS;  // 0x50
        private DG.Tweening.Sequence m_tween;  // 0x68
        private System.Action m_onComplete;  // 0x70
        private System.Boolean m_skipLicense;  // 0x78
        private static System.Boolean s_isPlaying;  // static @ 0x0
        private System.Boolean m_canBeHide;  // 0x79
        private static Beyond.UI.SplashController <instance>k__BackingField;  // static @ 0x8

        // Properties
        Beyond.UI.SplashController instance { get; /* RVA: 0x06993DDC */ set; /* RVA: 0x06993E14 */ }
        System.Boolean isPlaying { get; /* RVA: 0x0340B960 */ }

        // Methods
        // RVA: 0x0380F750  token: 0x60001C4
        public static Beyond.UI.SplashController Show(System.Action onComplete, System.Boolean skipLicense) { }
        // RVA: 0x0380F650  token: 0x60001C5
        public static System.Void Hide() { }
        // RVA: 0x0380F5A0  token: 0x60001C6
        public static System.Void CanFinished() { }
        // RVA: 0x0380F460  token: 0x60001C7
        private System.Void Awake() { }
        // RVA: 0x0380F510  token: 0x60001C8
        private System.Void OnDestroy() { }
        // RVA: 0x041E4EF0  token: 0x60001C9
        private System.Void _Play(System.Action onComplete, System.Boolean skipLicense) { }
        // RVA: 0x0380E980  token: 0x60001CA
        private System.Void _Stop() { }
        // RVA: 0x041E5000  token: 0x60001CB
        private Beyond.UI.SplashController.SplashConfig[] _GetSplashConfigs() { }
        // RVA: 0x045E2E10  token: 0x60001CC
        private System.Void _DoStartSplash(Beyond.UI.SplashController.SplashConfig[] splashConfigs) { }
        // RVA: 0x04CA7270  token: 0x60001CD
        private System.Void _OnTweenFinished() { }
        // RVA: 0x04CA72D0  token: 0x60001CE
        private System.Collections.IEnumerator _OnSplashCompleteCoroutine() { }
        // RVA: 0x04D32AA0  token: 0x60001CF
        private System.Collections.IEnumerator _StartLicenseCoroutine() { }
        // RVA: 0x04D5EC50  token: 0x60001D0
        public System.Void .ctor() { }
        // RVA: 0x04D4E7C0  token: 0x60001D1
        private System.Void <_DoStartSplash>b__31_1() { }

    }

}

namespace HG.Rendering.Runtime
{

    // TypeToken: 0x2000003  // size: 0x10
    public class HGConstantBufferLayoutAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000004
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x200011B  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x6000650
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x06360E84  token: 0x6000651
        public Beyond.GameState __Gen_Wrap_0(System.Object P0) { }
        // RVA: 0x066F3838  token: 0x6000652
        public System.Void __Gen_Wrap_1(System.Object P0, Beyond.GameState P1) { }
        // RVA: 0x069ABD7C  token: 0x6000653
        public System.Void __Gen_Wrap_2(System.Single P0) { }
        // RVA: 0x06375088  token: 0x6000654
        public System.Void __Gen_Wrap_3(System.Boolean P0) { }
        // RVA: 0x0635FF24  token: 0x6000655
        public System.Void __Gen_Wrap_4(Beyond.GameSetting.GameSettingAudioSuiteMode P0) { }
        // RVA: 0x0635FF24  token: 0x6000656
        public System.Void __Gen_Wrap_5(Beyond.GameSetting.KeyboardLayoutType P0) { }
        // RVA: 0x0635FF24  token: 0x6000657
        public System.Void __Gen_Wrap_6(Beyond.GEnums.EnvLang P0) { }
        // RVA: 0x0635FF24  token: 0x6000658
        public System.Void __Gen_Wrap_7(Beyond.GameSetting.GameSettingLanguageAudio P0) { }
        // RVA: 0x0635FF24  token: 0x6000659
        public System.Void __Gen_Wrap_8(Beyond.GameSetting.GameSettingVideoQuality P0) { }
        // RVA: 0x069AF3C0  token: 0x600065A
        public System.Void __Gen_Wrap_9(System.Int32 P0, System.Int32 P1, System.Boolean P2) { }
        // RVA: 0x069AB15C  token: 0x600065B
        public System.Void __Gen_Wrap_10(Beyond.GameSetting.ScreenResolution P0) { }
        // RVA: 0x0635FF24  token: 0x600065C
        public System.Void __Gen_Wrap_11(Beyond.GameSetting.TeammateSkillEffectStrength P0) { }
        // RVA: 0x063625A4  token: 0x600065D
        public System.Void __Gen_Wrap_12() { }
        // RVA: 0x063633B4  token: 0x600065E
        public System.Void __Gen_Wrap_13(System.Object P0) { }
        // RVA: 0x063632E4  token: 0x600065F
        public System.Boolean __Gen_Wrap_14(System.Object P0) { }
        // RVA: 0x069AB250  token: 0x6000660
        public Beyond.UI.SplashController.SplashConfig[] __Gen_Wrap_15(System.Object P0) { }
        // RVA: 0x069AB32C  token: 0x6000661
        public System.Collections.IEnumerator __Gen_Wrap_16(System.Object P0) { }
        // RVA: 0x06377B18  token: 0x6000662
        public System.Void __Gen_Wrap_17(System.Object P0, System.Object P1) { }
        // RVA: 0x069AB41C  token: 0x6000663
        public System.Void __Gen_Wrap_18(System.Object P0, System.Object P1, System.Boolean P2) { }
        // RVA: 0x069AB500  token: 0x6000664
        public Beyond.UI.SplashController __Gen_Wrap_19(System.Object P0, System.Boolean P1) { }
        // RVA: 0x069AB5F8  token: 0x6000665
        public System.Void __Gen_Wrap_20(Beyond.EventData<System.Boolean,System.Boolean>& P0) { }
        // RVA: 0x069AB6F0  token: 0x6000666
        public System.String __Gen_Wrap_21() { }
        // RVA: 0x069AB7A8  token: 0x6000667
        public System.String __Gen_Wrap_22(System.Object P0) { }
        // RVA: 0x069AB898  token: 0x6000668
        public System.Boolean __Gen_Wrap_23(System.Object P0, System.UInt32 P1) { }
        // RVA: 0x069AB970  token: 0x6000669
        public Beyond.GameApp.FileEntry __Gen_Wrap_24(Beyond.GameApp.PendingFile P0) { }
        // RVA: 0x069ABA7C  token: 0x600066A
        public System.Collections.Generic.List<Beyond.GameApp.FileEntry> __Gen_Wrap_25(System.Object P0, System.Int32 P1) { }
        // RVA: 0x06360610  token: 0x600066B
        public System.Int32 __Gen_Wrap_26(System.Object P0, System.Object P1) { }
        // RVA: 0x069ABB74  token: 0x600066C
        public System.String __Gen_Wrap_27(System.Int64 P0) { }
        // RVA: 0x06364290  token: 0x600066D
        public System.Void __Gen_Wrap_28(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x069ABC50  token: 0x600066E
        public System.Void __Gen_Wrap_29(System.Object P0, System.Object P1, System.DateTime P2, System.Object P3, System.Object P4) { }
        // RVA: 0x069ABE3C  token: 0x600066F
        public System.Void __Gen_Wrap_30(System.Object P0, System.Object P1, System.Int32 P2, System.Object P3) { }
        // RVA: 0x063778A4  token: 0x6000670
        public System.Void __Gen_Wrap_31(System.Object P0, System.Object P1, System.Int32 P2) { }
        // RVA: 0x063623A8  token: 0x6000671
        public System.Boolean __Gen_Wrap_32() { }
        // RVA: 0x066C2B40  token: 0x6000672
        public System.Void __Gen_Wrap_33(System.Object P0, System.Boolean P1) { }
        // RVA: 0x0635FF24  token: 0x6000673
        public System.Void __Gen_Wrap_34(System.Int32 P0) { }
        // RVA: 0x0635FF24  token: 0x6000674
        public System.Void __Gen_Wrap_35(Beyond.GameAppExitCode P0) { }
        // RVA: 0x069ABF34  token: 0x6000675
        public System.Boolean __Gen_Wrap_36(System.String& P0) { }
        // RVA: 0x069AC03C  token: 0x6000676
        public System.Boolean __Gen_Wrap_37(System.Object P0, Beyond.Resource.HotUpdate.LocalResPref& P1, Beyond.VFS.VirtualFileSystem.BlockValidator& P2) { }
        // RVA: 0x069AC1C4  token: 0x6000677
        public System.Void __Gen_Wrap_38(System.Object P0, Beyond.VFS.VirtualFileSystem.BlockValidator& P1) { }
        // RVA: 0x069AC2EC  token: 0x6000678
        public System.Void __Gen_Wrap_39(System.Object P0, Beyond.EventData<Beyond.DeviceInfo.InputType>& P1) { }
        // RVA: 0x069AC400  token: 0x6000679
        public Beyond.LoginController.MaskCtrl __Gen_Wrap_40(System.Object P0) { }
        // RVA: 0x069AC4F0  token: 0x600067A
        public Beyond.UI.UISwitchTween __Gen_Wrap_41(System.Object P0, System.Int32 P1) { }
        // RVA: 0x069AC5FC  token: 0x600067B
        public System.Collections.IEnumerator __Gen_Wrap_42(System.Object P0, System.Object P1) { }
        // RVA: 0x069AC708  token: 0x600067C
        public System.Void __Gen_Wrap_43(System.Object P0, System.Object P1, Beyond.LoginController.EMaskType P2, System.Boolean P3, System.Boolean P4) { }
        // RVA: 0x069AC81C  token: 0x600067D
        public Beyond.UI.UIAnimationLayerMixPlayer __Gen_Wrap_44(System.Object P0) { }
        // RVA: 0x069AC90C  token: 0x600067E
        public System.Void __Gen_Wrap_45(System.Object P0, Beyond.LoginController.FOptions P1) { }
        // RVA: 0x069ACA1C  token: 0x600067F
        public Beyond.Login.LoginWorkFlow.FWork __Gen_Wrap_46() { }
        // RVA: 0x069ACB10  token: 0x6000680
        public Beyond.Login.LoginWorkFlow.ENode __Gen_Wrap_47(Beyond.Login.LoginWorkFlow.FWork& P0) { }
        // RVA: 0x069ACC64  token: 0x6000681
        public System.Boolean __Gen_Wrap_48(Beyond.Login.LoginWorkFlow.ENode P0, Beyond.Login.LoginWorkFlow.ENode P1) { }
        // RVA: 0x06361FA4  token: 0x6000682
        public System.Boolean __Gen_Wrap_49(Beyond.Login.LoginWorkFlow.ENode P0) { }
        // RVA: 0x069ACD4C  token: 0x6000683
        public System.Collections.IEnumerator __Gen_Wrap_50(Beyond.Login.LoginWorkFlow.ENode P0) { }
        // RVA: 0x069ACE38  token: 0x6000684
        public UnityEngine.CustomYieldInstruction __Gen_Wrap_51(System.Object P0) { }
        // RVA: 0x069ACF28  token: 0x6000685
        public System.Boolean __Gen_Wrap_52(Beyond.Login.LoginWorkFlow.FWork& P0) { }
        // RVA: 0x069AD07C  token: 0x6000686
        public System.Void __Gen_Wrap_53(Beyond.Login.LoginWorkFlow.FWork& P0) { }
        // RVA: 0x069AD1C0  token: 0x6000687
        public System.Void __Gen_Wrap_54(System.Object P0, Beyond.Login.LoginAlertDialog.FParams P1) { }
        // RVA: 0x069AD2C0  token: 0x6000688
        public System.Void __Gen_Wrap_55(System.Object P0, Beyond.Login.LoginVocResSelDialog.FParams P1) { }
        // RVA: 0x069AD3C0  token: 0x6000689
        public System.Void __Gen_Wrap_56(System.Object P0, Beyond.Login.LoginJudgeDialog.FParams P1) { }
        // RVA: 0x069AD4E4  token: 0x600068A
        public System.Void __Gen_Wrap_57(Beyond.Login.LoginAlertDialog P0, Beyond.Login.LoginAlertDialog.FParams P1) { }
        // RVA: 0x069AD5E4  token: 0x600068B
        public System.Void __Gen_Wrap_58(System.Object P0, System.Boolean P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6, System.Object P7, System.Object P8) { }
        // RVA: 0x069AD73C  token: 0x600068C
        public System.Collections.IEnumerator __Gen_Wrap_59(System.Object P0, System.Boolean P1) { }
        // RVA: 0x069AD848  token: 0x600068D
        public System.Boolean __Gen_Wrap_60(System.Object P0, Beyond.Login.LoginWorkFlow.ENode& P1) { }
        // RVA: 0x0635FF24  token: 0x600068E
        public System.Void __Gen_Wrap_61(Beyond.GameState P0) { }
        // RVA: 0x066C288C  token: 0x600068F
        public System.Void __Gen_Wrap_62(System.Object P0, System.Single P1) { }
        // RVA: 0x069AD954  token: 0x6000690
        public Beyond.Login.LoginRootPanel __Gen_Wrap_63(System.Object P0) { }
        // RVA: 0x066C0730  token: 0x6000691
        public System.Single __Gen_Wrap_64(System.Object P0) { }
        // RVA: 0x069ADA44  token: 0x6000692
        public Beyond.LoginController __Gen_Wrap_65(System.Object P0) { }
        // RVA: 0x069ADB34  token: 0x6000693
        public System.Void __Gen_Wrap_66(System.Object P0, System.Object P1, System.Single P2) { }
        // RVA: 0x069ADC18  token: 0x6000694
        public System.Collections.Generic.List<Beyond.Gameplay.ServerZoneManager.ServerZoneData> __Gen_Wrap_67(System.Object P0) { }
        // RVA: 0x06366BC4  token: 0x6000695
        public System.Boolean __Gen_Wrap_68(System.Object P0, System.Object P1) { }
        // RVA: 0x069ADD08  token: 0x6000696
        public Beyond.Gameplay.ServerZoneManager.ServerZoneData __Gen_Wrap_69(System.Object P0) { }
        // RVA: 0x069ADDF8  token: 0x6000697
        public System.Boolean __Gen_Wrap_70(System.Object P0, Beyond.ELoginEvent P1, System.Object P2) { }
        // RVA: 0x066F4C88  token: 0x6000698
        public System.Void __Gen_Wrap_71(System.Object P0, Beyond.ELoginEvent P1, System.Object P2) { }
        // RVA: 0x066C296C  token: 0x6000699
        public System.Void __Gen_Wrap_72(System.String P0) { }
        // RVA: 0x066C296C  token: 0x600069A
        public System.Void __Gen_Wrap_73(UnityEngine.EventSystems.PointerEventData P0) { }
        // RVA: 0x069ADEFC  token: 0x600069B
        public System.Boolean __Gen_Wrap_74(Beyond.Login.LoginMockUIDPanel.FOptions& P0) { }
        // RVA: 0x069AE048  token: 0x600069C
        public System.Void __Gen_Wrap_75(System.Object P0, System.Int32& P1) { }
        // RVA: 0x069AE148  token: 0x600069D
        public System.Void __Gen_Wrap_76(System.Int32 P0, Beyond.UI.UIDropdownOption P1, System.Boolean P2) { }
        // RVA: 0x066F3838  token: 0x600069E
        public System.Void __Gen_Wrap_77(System.Object P0, System.Int32 P1) { }
        // RVA: 0x069AE22C  token: 0x600069F
        public System.Void __Gen_Wrap_78(System.Object P0, Beyond.Login.LoginMockUIDPanel.FOptions P1, System.Object P2) { }
        // RVA: 0x069AE358  token: 0x60006A0
        public Beyond.UI.UISwitchTween __Gen_Wrap_79(System.Object P0) { }
        // RVA: 0x066E0A9C  token: 0x60006A1
        public System.Boolean __Gen_Wrap_80(System.Object P0, Beyond.LoginController.EMaskType P1) { }
        // RVA: 0x069AE448  token: 0x60006A2
        public System.Void __Gen_Wrap_81(Beyond.Login.LoginVocResSelDialog P0, Beyond.Login.LoginVocResSelDialog.FParams P1) { }
        // RVA: 0x069AE548  token: 0x60006A3
        public System.Collections.IEnumerator __Gen_Wrap_82(System.Object P0, System.Object P1, System.Object P2, System.Object P3) { }
        // RVA: 0x069AE680  token: 0x60006A4
        public System.Void __Gen_Wrap_83(Beyond.Login.LoginJudgeDialog P0, Beyond.Login.LoginJudgeDialog.FParams P1) { }
        // RVA: 0x066E8E30  token: 0x60006A5
        public System.Void __Gen_Wrap_84(System.Object P0, System.Object P1, System.Object P2, System.Object P3) { }
        // RVA: 0x069AE790  token: 0x60006A6
        public System.String __Gen_Wrap_85(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x069AE8B8  token: 0x60006A7
        public System.Void __Gen_Wrap_86(System.Boolean& P0) { }
        // RVA: 0x069AE98C  token: 0x60006A8
        public System.Void __Gen_Wrap_87(System.Object P0, System.Boolean& P1) { }
        // RVA: 0x069AEA7C  token: 0x60006A9
        public System.Void __Gen_Wrap_88(System.Object P0, Beyond.LoginManager.FFlowWithMask P1) { }
        // RVA: 0x069AEBA0  token: 0x60006AA
        public System.Boolean __Gen_Wrap_89(Beyond.LoginManager.FFlowCondition& P0, Beyond.Login.LoginWorkFlow.ENode P1) { }
        // RVA: 0x069AECF4  token: 0x60006AB
        public System.Collections.IEnumerator __Gen_Wrap_90(System.Object P0, Beyond.LoginManager.FFlowWithMask P1) { }
        // RVA: 0x069AEE34  token: 0x60006AC
        public TMPro.TMP_FontAsset __Gen_Wrap_91(System.Object P0) { }
        // RVA: 0x069AEF10  token: 0x60006AD
        public System.Void __Gen_Wrap_92(Beyond.SDK.SDKNetUtils.HttpPostRequest P0) { }
        // RVA: 0x069AF004  token: 0x60006AE
        public System.Void __Gen_Wrap_93(Beyond.SDK.SDKNetUtils.HttpGetRequest P0) { }
        // RVA: 0x069AF0E8  token: 0x60006AF
        public System.Collections.IEnumerator __Gen_Wrap_94(System.Object P0, Beyond.SDK.SDKNetUtils.HTTPConfig P1) { }
        // RVA: 0x069AF218  token: 0x60006B0
        public System.Collections.IEnumerator __Gen_Wrap_95() { }
        // RVA: 0x069AF2E4  token: 0x60006B1
        public System.Collections.Generic.Dictionary<System.String,System.String> __Gen_Wrap_96(System.Object P0) { }
        // RVA: 0x06360E84  token: 0x60006B2
        public System.Int32 __Gen_Wrap_97(System.Object P0) { }
        // RVA: 0x011D1C90  token: 0x60006B3
        public System.Int64 __Gen_Wrap_98(System.Object P0) { }
        // RVA: 0x01527E90  token: 0x60006B4
        public System.Void __Gen_Wrap_99(System.Object P0, U8.SDK.SDKExtraData P1) { }
        // RVA: 0x0150B3A0  token: 0x60006B5
        public U8.SDK.SDKExternalTools.POSTResult __Gen_Wrap_100(Beyond.SDK.SDKNetUtils.WebHttpResponse P0) { }
        // RVA: 0x0150BF40  token: 0x60006B6
        public System.Void __Gen_Wrap_101(Beyond.SDK.SDKNetUtils.WebHttpResponse P0) { }
        // RVA: 0x0150C4F0  token: 0x60006B7
        public System.Void __Gen_Wrap_102(System.Object P0, U8.SDK.SDKExternalTools.POSTRequest P1, System.Object P2) { }
        // RVA: 0x0150CC40  token: 0x60006B8
        public U8.SDK.SDKCaptchaHandler __Gen_Wrap_103(System.Object P0) { }
        // RVA: 0x0150D570  token: 0x60006B9
        public System.Boolean __Gen_Wrap_104(System.Object P0, U8.SDK.SDKExternalTools.ErrMsgMeta P1, System.String& P2) { }
        // RVA: 0x0150E2F0  token: 0x60006BA
        public System.String __Gen_Wrap_105(System.Object P0, U8.SDK.SDKExternalTools.ErrMsgMeta P1) { }
        // RVA: 0x0150EFD0  token: 0x60006BB
        public System.Collections.IEnumerator __Gen_Wrap_106(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x0150FAD0  token: 0x60006BC
        public System.Void __Gen_Wrap_107(System.Object P0, U8.SDK.ExternalPluginLoginParams P1) { }
        // RVA: 0x01510160  token: 0x60006BD
        public System.Void __Gen_Wrap_108(System.Object P0, U8.SDK.ExternalPluginLogoutParams P1) { }
        // RVA: 0x015107D0  token: 0x60006BE
        public System.Void __Gen_Wrap_109(U8.SDK.U8PayResult P0) { }
        // RVA: 0x01510D70  token: 0x60006BF
        public System.Void __Gen_Wrap_110(System.Object P0, U8.SDK.ExternalPluginPayParams P1) { }
        // RVA: 0x015113E0  token: 0x60006C0
        public System.Boolean __Gen_Wrap_111(System.Object P0, System.Object P1, U8.SDK.SDKMeta& P2) { }
        // RVA: 0x01298AE0  token: 0x60006C1
        public Beyond.Login.LoginWorkFlow.ENode __Gen_Wrap_112(System.Object P0) { }
        // RVA: 0x01511F20  token: 0x60006C2
        public System.Void __Gen_Wrap_113(Beyond.Login.LoginWorkFlow.FWork& P0, System.Int32 P1) { }
        // RVA: 0x01512B10  token: 0x60006C3
        public System.Boolean __Gen_Wrap_114(System.Object P0, Beyond.Login.LoginWorkFlow.ENode P1, System.Boolean P2) { }
        // RVA: 0x01512E20  token: 0x60006C4
        public Beyond.SDK.HGDownloader.DownloadProgress __Gen_Wrap_115(System.Object P0) { }
        // RVA: 0x01298AE0  token: 0x60006C5
        public Beyond.SDK.HGDownloader.WorkState __Gen_Wrap_116(System.Object P0) { }
        // RVA: 0x01513720  token: 0x60006C6
        public System.Collections.Generic.List<Beyond.SDK.HGFileInfo> __Gen_Wrap_117(System.Object P0, System.Object P1) { }
        // RVA: 0x01514140  token: 0x60006C7
        public System.Collections.Generic.List<Beyond.SDK.HGFolder> __Gen_Wrap_118(System.Object P0, System.Object P1) { }
        // RVA: 0x0129F0D0  token: 0x60006C8
        public System.Boolean __Gen_Wrap_119(System.Object P0, Beyond.SDK.HGRetCodeType P1, System.Int32 P2) { }
        // RVA: 0x01514B60  token: 0x60006C9
        public System.String __Gen_Wrap_120(System.Double P0) { }
        // RVA: 0x015153E0  token: 0x60006CA
        public System.Void __Gen_Wrap_121(System.Object P0, System.Int64 P1, System.Object P2, System.Object P3, System.Object P4) { }
        // RVA: 0x012A0D50  token: 0x60006CB
        public System.Void __Gen_Wrap_122(System.Object P0, Beyond.SDK.HGDownloader.PauseReason P1) { }
        // RVA: 0x01515910  token: 0x60006CC
        public System.Collections.IEnumerator __Gen_Wrap_123(System.Object P0, System.Int64 P1) { }
        // RVA: 0x015162B0  token: 0x60006CD
        public System.Void __Gen_Wrap_124(System.Object P0, System.Int64 P1, System.Int64 P2, System.Int64 P3) { }
        // RVA: 0x0146E800  token: 0x60006CE
        public System.Double __Gen_Wrap_125(System.Object P0) { }
        // RVA: 0x01408E40  token: 0x60006CF
        public System.Boolean __Gen_Wrap_126(System.Boolean P0) { }
        // RVA: 0x015165C0  token: 0x60006D0
        public System.String __Gen_Wrap_127(System.Object P0, System.Boolean P1) { }
        // RVA: 0x01516F60  token: 0x60006D1
        public Beyond.Resource.HotUpdate.LocalResPref __Gen_Wrap_128() { }
        // RVA: 0x015177A0  token: 0x60006D2
        public Beyond.Login.LoginHotUpdater.HotUpdateInfo __Gen_Wrap_129(System.Object P0, System.Boolean P1) { }
        // RVA: 0x01518140  token: 0x60006D3
        public Beyond.Resource.HotUpdate.ResourceIndex __Gen_Wrap_130() { }
        // RVA: 0x01518980  token: 0x60006D4
        public System.String __Gen_Wrap_131(System.Object P0, System.Object P1, System.Boolean P2) { }
        // RVA: 0x015193D0  token: 0x60006D5
        public System.Object __Gen_Wrap_132() { }
        // RVA: 0x01519C10  token: 0x60006D6
        public System.Collections.IEnumerator __Gen_Wrap_133(System.Object P0, System.Object P1, System.Boolean P2, System.Object P3) { }
        // RVA: 0x0151A730  token: 0x60006D7
        public System.Collections.IEnumerator __Gen_Wrap_134(System.Boolean P0, System.Object P1, System.Object P2) { }
        // RVA: 0x0151B170  token: 0x60006D8
        public Beyond.Resource.HotUpdate.LocalResPref __Gen_Wrap_135(System.Object P0, System.Boolean P1, System.Object P2, Beyond.Resource.HotUpdate.FResourceDiff P3, System.Int32& P4) { }
        // RVA: 0x0151BED0  token: 0x60006D9
        public System.Void __Gen_Wrap_136(System.Object P0, System.Boolean& P1, System.Double P2, System.Double P3, System.Int32 P4, System.Boolean P5) { }
        // RVA: 0x0151C240  token: 0x60006DA
        public System.Boolean __Gen_Wrap_137(System.Object P0, Beyond.VFS.EVFSBlockType P1) { }
        // RVA: 0x0151C530  token: 0x60006DB
        public System.Collections.IEnumerator __Gen_Wrap_138(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3) { }
        // RVA: 0x0151CFA0  token: 0x60006DC
        public Beyond.Login.LoginHotUpdater __Gen_Wrap_139(System.Object P0, System.Boolean P1) { }
        // RVA: 0x0151D900  token: 0x60006DD
        public Beyond.Login.LoginProgress.FDownloadSize.EUnit __Gen_Wrap_140(System.Double P0) { }
        // RVA: 0x0151DB00  token: 0x60006DE
        public System.Single __Gen_Wrap_141(System.Double P0, Beyond.Login.LoginProgress.FDownloadSize.EUnit P1) { }
        // RVA: 0x0151DD30  token: 0x60006DF
        public System.String __Gen_Wrap_142(System.Single P0, System.Single P1, Beyond.Login.LoginProgress.FDownloadSize.EUnit P2) { }
        // RVA: 0x0151E620  token: 0x60006E0
        public System.String __Gen_Wrap_143(System.Single P0, Beyond.Login.LoginProgress.FDownloadSize.EUnit P1) { }
        // RVA: 0x0151EED0  token: 0x60006E1
        public System.String __Gen_Wrap_144(System.Single P0, System.Single P1) { }
        // RVA: 0x0151F7A0  token: 0x60006E2
        public System.Void __Gen_Wrap_145(Beyond.Login.LoginProgress.FDownloadSize& P0, System.Double P1, System.Double P2, System.Double P3) { }
        // RVA: 0x01520470  token: 0x60006E3
        public System.Void __Gen_Wrap_146(Beyond.Login.LoginProgress.FDownloadSize& P0, System.Double P1, System.Double P2) { }
        // RVA: 0x01521120  token: 0x60006E4
        public System.Void __Gen_Wrap_147(Beyond.Login.LoginProgress.FDownloadSize& P0) { }
        // RVA: 0x0133CDE0  token: 0x60006E5
        public System.Void __Gen_Wrap_148(System.Object P0, Beyond.GameAppExitCode P1) { }
        // RVA: 0x012A5970  token: 0x60006E6
        public System.Void __Gen_Wrap_149(Hypergryph.SpeedTest.SpeedTestResult P0) { }
        // RVA: 0x01521D30  token: 0x60006E7
        public System.Threading.Tasks.Task<Hypergryph.SpeedTest.TaskResult> __Gen_Wrap_150() { }
        // RVA: 0x01522570  token: 0x60006E8
        public System.Boolean __Gen_Wrap_151(Beyond.Login.LoginViewModel.FAgeTipsPolicy& P0) { }
        // RVA: 0x01523140  token: 0x60006E9
        public Beyond.Login.LoginViewModel.FAgeTipsPolicy __Gen_Wrap_152() { }
        // RVA: 0x01523950  token: 0x60006EA
        public Beyond.Login.LoginWorkFlow.Node[] __Gen_Wrap_153(System.Object P0) { }
        // RVA: 0x01524290  token: 0x60006EB
        public System.Single __Gen_Wrap_154(System.Object P0, System.Boolean& P1) { }
        // RVA: 0x01298AE0  token: 0x60006EC
        public Beyond.TickType __Gen_Wrap_155(System.Object P0) { }
        // RVA: 0x015245B0  token: 0x60006ED
        public Beyond.UI.UIAnimationMixPlayer __Gen_Wrap_156(System.Object P0) { }
        // RVA: 0x0133CDE0  token: 0x60006EE
        public System.Void __Gen_Wrap_157(System.Object P0, Beyond.Login.LoginSceneAnimCtrl.EState P1) { }
        // RVA: 0x01524EF0  token: 0x60006EF
        public System.Collections.Generic.List<System.String> __Gen_Wrap_158(System.Object P0) { }
        // RVA: 0x01525830  token: 0x60006F0
        public System.Void __Gen_Wrap_159(System.Object P0, Beyond.Login.LoginViewModel.FAgeTipsPolicy P1) { }
        // RVA: 0x01525EC0  token: 0x60006F1
        public System.Void __Gen_Wrap_160(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3) { }
        // RVA: 0x015262A0  token: 0x60006F2
        public System.Void __Gen_Wrap_161(System.Boolean P0, System.Boolean P1, System.Boolean P2) { }
        // RVA: 0x015264D0  token: 0x60006F3
        public System.Void __Gen_Wrap_162(System.Object P0, Beyond.EventData<System.Boolean>& P1) { }
        // RVA: 0x01526B50  token: 0x60006F4
        public Beyond.Login.LoginMockUIDPanel __Gen_Wrap_163() { }
        // RVA: 0x01527390  token: 0x60006F5
        public System.Void __Gen_Wrap_164(System.Object P0, Beyond.EventData<System.Single>& P1) { }
        // RVA: 0x01527A20  token: 0x60006F6
        public System.Void __Gen_Wrap_165(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3) { }
        // RVA: 0x012ABC50  token: 0x60006F7
        public System.Void __Gen_Wrap_166(System.Object P0, System.Int32 P1, System.Object P2) { }
        // RVA: 0x0122AC70  token: 0x60006F8
        public System.Void __Gen_Wrap_167(System.Object P0, Beyond.VFS.EVFSBlockType P1) { }
        // RVA: 0x04D37790  token: 0x60006F9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200011C  // size: 0x58
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey, System.IDisposable, System.Collections.IEnumerator, System.Collections.Generic.IEnumerator`1
    {
        // Fields
        private System.Int32 methodId_0;  // 0x40
        private System.Int32 methodId_1;  // 0x44
        private System.Int32 methodId_2;  // 0x48
        private System.Int32 methodId_3;  // 0x4c
        private System.Int32 methodId_4;  // 0x50

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x069B5064 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x069B5110 */ }

        // Methods
        // RVA: 0x06365FC0  token: 0x60006FA
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x06365D60  token: 0x60006FB
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06365E98  token: 0x60006FD
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x069B51BC  token: 0x60006FF
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x6000700
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x200011D  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000701
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x069B5480  token: 0x6000702
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x02F44DE0  token: 0x6000703
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x069B533C  token: 0x6000704
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x069B5408  token: 0x6000705
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x069B54E4  token: 0x6000706
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x069B5298  token: 0x6000707
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x200011E  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 Beyond-GameApp-get_currentState0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-SwitchState0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnAudioGlobalVolumeChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnAudioMusicVolumeChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnAudioVoiceVolumeChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnAudioSfxVolumeChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnAudioSuspendUnfocusedChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnAudioBackgroundMusicChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnAudioSuiteModeChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnAudioControllerChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnAudioSpatialAudioChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnCameraDistanceLevelChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnControllerMotionChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnKeyboardTypeChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnLanguageTextChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnLanguageAudioChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnVideoQualityChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_SetResolution0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnVideoFullScreenChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnVideoResolutionChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnVideoNotchPaddingChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnTeammateSkillEffectStrengthChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-_OnTeammateSkillEffectOpacityChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-GameSettingSetter-RegisterSettingCallbacks0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_PreInitGameSetting0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_NeedDeepProfilerMode0;  // const
        public static IFix.IDMAP0 Beyond-RenderPipelineDataLoader-LoadSimpleRpData0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-_GetSplashConfigs0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-PublicSetGameVersion0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-_StartLicenseCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-_Stop0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-Hide0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-_OnSplashCompleteCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-_OnTweenFinished0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-_DoStartSplash0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-_Play0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-Show0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_InitNotificationCenter0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_FetchRemoteConfigUtilSuc0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_InitSecuritySDK0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_InitGameStateMachine0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_OnApplicationFocusForLua0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_OnApplicationPauseForLua0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_RegisterLuaAfterGCEvent0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-RefreshScreenResolution0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_OnSystemDisplaySizeChanged0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_OnSystemScreenSizeChanged0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_Init0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_CollectSystemInfo0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_MakeLongPath0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_ScanDirectory0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_CanAccess0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_CalculateMd50;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_BuildEntry0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_WorkerLoop0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_ScanParallel0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_TreeNodeCompare0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_FormatSize0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_PrintTreeNode0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_WriteTreeReport0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-LaunchScanCoordinator0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-ScanLocalFiles0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-get_IsScanRunning0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_DelayInit0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-Awake0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-Update0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-LateUpdate0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-OnApplicationQuit0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-OnApplicationFocus0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_OnApplicationPauseForSecurity0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-OnApplicationPause0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_UnInitGameStateMachine0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-_UnInit0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-OnDestroy0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-QuitGame0;  // const
        public static IFix.IDMAP0 Beyond-GameApp-QuitGame1;  // const
        public static IFix.IDMAP0 Beyond-GameApp-PollScanResult0;  // const
        public static IFix.IDMAP0 Beyond-GameInitState-get_stateId0;  // const
        public static IFix.IDMAP0 Beyond-GameStateBase-OnEnter0;  // const
        public static IFix.IDMAP0 Beyond-GameInitState-_CheckIfLocalInitialAssetsValid0;  // const
        public static IFix.IDMAP0 Beyond-GameInitState-_InitAPPDefaultCulture0;  // const
        public static IFix.IDMAP0 Beyond-GameInitState-_InitInitialResourceManager0;  // const
        public static IFix.IDMAP0 Beyond-RenderPipelineDataLoader-PreloadFullRpResources0;  // const
        public static IFix.IDMAP0 Beyond-VietnamLoginAgePanel-_UnInit0;  // const
        public static IFix.IDMAP0 Beyond-VietnamLoginAgePanel-_Init0;  // const
        public static IFix.IDMAP0 Beyond-VietnamLoginAgePanel-Init0;  // const
        public static IFix.IDMAP0 Beyond-GameInitState-_InitGameSetting0;  // const
        public static IFix.IDMAP0 Beyond-RenderPipelineDataLoader-IsPreLoadFinished0;  // const
        public static IFix.IDMAP0 Beyond-GameInitState-_DoInit0;  // const
        public static IFix.IDMAP0 Beyond-GameInitState-OnEnter0;  // const
        public static IFix.IDMAP0 Beyond-GameStateBase-OnLeave0;  // const
        public static IFix.IDMAP0 Beyond-GameInitState-OnLeave0;  // const
        public static IFix.IDMAP0 Beyond-GameLoginState-get_stateId0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-CanFinished0;  // const
        public static IFix.IDMAP0 Beyond-RenderPipelineDataLoader-LoadFullRpResources0;  // const
        public static IFix.IDMAP0 Beyond-LoginContext-IsDisposed0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-IsWorkflowInProgress0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Node-OnDispose0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Node-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_EnableCursor0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_UpdateCursor0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_EnbaleToggleGroup0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Worker-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-AssetLoader-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-LoginContext-_DisposeImpl0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-get_maskCtrl0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-MaskCtrl-_GetMaskByType0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-MaskCtrl-IsCrossSceneMaskTweening0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-MaskCtrl-WaitForCrossSceneMasks0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-MaskCtrl-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-BeforeDestroy0;  // const
        public static IFix.IDMAP0 Beyond-LoginContext-_DisposeCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-LoginContext-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-LoginContext-IsWorkflowDisposed0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-MaskCtrl-EnableMask0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-_InitController0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-ChildPanel-OnInit0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-ChildPanel-OnLoginContext0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-_InitChildPanel0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-Init0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDecorateUI-_EnsureGlowPlayer0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDecorateUI-_ToggleGlow0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDecorateUI-OnLoginContext0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-_InitVersionBtn0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-_InitCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-_InitImpl0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-InitUI0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-FWork-Start0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-FWork-get_curNode0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-IsOnOrAfterWork0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-IsBeforeWork0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_CheckIfGameStateNotMatch0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_AdjustCurrentGameState0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Worker-SetCurNode0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Worker-_InternalTick0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Node-Cancel0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Worker-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-IsNeedSoftRestart0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Worker-TickInEmptyFrame0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-FWork-ConsumeFallback0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-FWork-MoveNextIndex0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-FWork-WorkEnd0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-DoWorkFlow0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-_LoginWorkFlowCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-_PrepareBeforeLoginProcess0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAlertDialog-SetParams0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-SetParams0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginJudgeDialog-SetParams0;  // const
        public static IFix.IDMAP0 Beyond-LoginContext-StartCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-AlertDialog0;  // const
        public static IFix.IDMAP0 Beyond-LoginContext-Init0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-InvokeWhenStart0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-_Init0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-InitCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-GameLoginState-_TryStartLoginWorkflow0;  // const
        public static IFix.IDMAP0 Beyond-GameLoginState-OnEnter0;  // const
        public static IFix.IDMAP0 Beyond-GameLoginState-OnLeave0;  // const
        public static IFix.IDMAP0 Beyond-GameMainState-get_stateId0;  // const
        public static IFix.IDMAP0 Beyond-LoginContext-ShutDown0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-_ClearWorkflowAndContext0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-_ReleaseLoginScene0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-Clear0;  // const
        public static IFix.IDMAP0 Beyond-GameMainState-OnEnter0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-IsAfterWork0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-PeekFallbackStatus0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-IsReLoginInProgress0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-OnReleaseMainGame0;  // const
        public static IFix.IDMAP0 Beyond-GameMainState-_ClearUtils0;  // const
        public static IFix.IDMAP0 Beyond-GameMainState-ReleaseMainGame0;  // const
        public static IFix.IDMAP0 Beyond-GameMainState-OnLeave0;  // const
        public static IFix.IDMAP0 Beyond-GamePreloadState-get_stateId0;  // const
        public static IFix.IDMAP0 Beyond-GamePreloadState-_InitGameSettingStateInPreload0;  // const
        public static IFix.IDMAP0 Beyond-GamePreloadState-_DoPreload0;  // const
        public static IFix.IDMAP0 Beyond-GamePreloadState-OnEnter0;  // const
        public static IFix.IDMAP0 Beyond-GamePreloadState-OnLeave0;  // const
        public static IFix.IDMAP0 Beyond-GameQuitState-get_stateId0;  // const
        public static IFix.IDMAP0 Beyond-VietnamLoginAgePanel-UnInit0;  // const
        public static IFix.IDMAP0 Beyond-GameQuitState-DoQuit0;  // const
        public static IFix.IDMAP0 Beyond-GameQuitState-OnEnter0;  // const
        public static IFix.IDMAP0 Beyond-GameQuitState-OnLeave0;  // const
        public static IFix.IDMAP0 Beyond-GameSoftRestartState-get_stateId0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-ClearSync0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-ClearEnteredPreloadThisSession0;  // const
        public static IFix.IDMAP0 Beyond-GameSoftRestartState-_SoftRestart0;  // const
        public static IFix.IDMAP0 Beyond-GameSoftRestartState-OnEnter0;  // const
        public static IFix.IDMAP0 Beyond-GameStartState-get_stateId0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-InvokeWhenWorkFlowFinished0;  // const
        public static IFix.IDMAP0 Beyond-GameStartState-_OnEnterMainGame0;  // const
        public static IFix.IDMAP0 Beyond-GameStartState-OnEnter0;  // const
        public static IFix.IDMAP0 Beyond-GameStartState-OnLeave0;  // const
        public static IFix.IDMAP0 Beyond-GameStateBase-OnTick0;  // const
        public static IFix.IDMAP0 Beyond-GameStateMachine-get_curState0;  // const
        public static IFix.IDMAP0 Beyond-GameStateMachine-_RegisterStates0;  // const
        public static IFix.IDMAP0 Beyond-GameStateMachine-CheckCanReturnToLoginState0;  // const
        public static IFix.IDMAP0 Beyond-GameStateMachine-ReturnToLoginState0;  // const
        public static IFix.IDMAP0 Beyond-GlobalInitializer-InitGlobalConfig0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-get_rootPanel0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-Awake0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-Start0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-GetEnterGamePanelPredelay0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsModel-IsContentEmpty0;  // const
        public static IFix.IDMAP0 Beyond-LoginContext-ViewOnlyController0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-MaskCtrl-EnableNetMask0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-MaskCtrl-DisableNetMask0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_FetchAgeTipsCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-ToggleLoginBindingGroup0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_EventOnOpenAgeTips0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_EventOnCloseAgeTips0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_EventOnOpenServerSelect0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectModel-get_serverList0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectModel-UpdateSelectedServer0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_EventOnSelectServer0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectModel-GetSelectedServer0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_EventOnConfirmServer0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_HandleGlobalEvent0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Node-OnEvent0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-SendEvent0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-OnValueChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-_SaveConfig0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-_OnBtnEnterGame0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-_OnBtnDelLocalFile0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-FOptions-IsUIDFixed0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-_TryClosePanel0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-_IndexOfServer0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-_MovePreferredServerToFront0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-_SaveLastSelectedServer0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-_OnSelectServer0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-_InitLoginInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-_UpdateStatus0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-LoginWithCallback0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-HideAndDestroy0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-_MockLoginCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-MockLoginWithCallback0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-Dialog-_CloseCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-Dialog-TryCloseWithCallback0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-Dialog-Show0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-Dialog-OnInit0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-MaskCfg-CreateTweenAndHide0;  // const
        public static IFix.IDMAP0 Beyond-LoginController-MaskCtrl-IsMaskTweening0;  // const
        public static IFix.IDMAP0 Beyond-LoginContext-StopCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-LoginContext-SendEvent0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-get_firstLogin0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneComponent-OnBindToManager0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneComponent-Bind0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-_BindPendingComponents0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-SceneComponentOnly_Bind0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-ToggleLoginBindingGroup0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-VocResSelDialog0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-VocResSelDialogWithConfirm0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-AlertDialogWithResult0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-AlertDialogWithConfirm0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-_StartShowJudgeDialogCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-JudgeDialog0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-JudgeDialog1;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-MockLoginWithCallback0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-MockManualSelectServer0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-IsLoginSceneLoading0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-_ExtractErrorInfo0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-FetchRemoteConfigUtilSuc0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-DevOnlyCheckEnableLocalResMode0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-DevOnlyUseLocalResVersion0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-AssetLoader-_CheckDisposedBeforeLoad0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-FlowWithMaskMgr-_OnFlowFinished0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-FFlowCondition-IsTargetMatch0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-FlowWithMaskMgr-_FlowCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-LoginManager-FlowWithMaskMgr-StartFlow0;  // const
        public static IFix.IDMAP0 Beyond-VietnamLoginAgePanel-OnApplicationQuit0;  // const
        public static IFix.IDMAP0 Beyond-VietnamLoginAgeTipsPanelCtrl-_SetStateTips0;  // const
        public static IFix.IDMAP0 Beyond-VietnamLoginAgeTipsPanelCtrl-_SetStateCircle0;  // const
        public static IFix.IDMAP0 Beyond-VietnamLoginAgeTipsPanelCtrl-Awake0;  // const
        public static IFix.IDMAP0 Beyond-TailGameLoop-Update0;  // const
        public static IFix.IDMAP0 Beyond-TailGameLoop-LateUpdate0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-get_isPlaying0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-Awake0;  // const
        public static IFix.IDMAP0 Beyond-UI-SplashController-OnDestroy0;  // const
        public static IFix.IDMAP0 Beyond-SDK-PlatformFont-GetFont0;  // const
        public static IFix.IDMAP0 Beyond-SDK-SDKNetUtils-HTTPPost0;  // const
        public static IFix.IDMAP0 Beyond-SDK-SDKNetUtils-HTTPGet0;  // const
        public static IFix.IDMAP0 Beyond-SDK-SDKNetUtils-BeyondHttp-DoHTTP0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-IsInited0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-NeedRetryInit0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-NeedQuitGame0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-_OnRequestExitGame0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-InitSDKSystem0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-RetryInitSDKSystem0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-WaitForInited0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-InitHGSDK0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-IsHGSDKInited0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-WaitForInitHGSDK0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-Log0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-LogError0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-LogWarning0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-GetSignKey0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-GetDeviceIDs0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-SetSubChannel0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-GetPlatformKey0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-CaptchaHandler-OnGT3Message0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-_TryGetGameProtocolUpdateCheckErrorCode0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-_ProcessGameProtocolUpdateCheck0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-_ProcessExtraInfo0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-OnSDKExtraInfo0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-U8RootUrl0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-_ConvertToPOSTResult0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-POSTImplementation0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-_OnFakeAccountCenter0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-CreateCaptchaHandler0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-_ErrorMsg_GET_TOKEN0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-_ErrorMsg_CREATE_ORDER0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-GetErrorMessage0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8ExternalTools-CaptchaHandler-FetchCaptchaCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8Plugin-GetPayAddition0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8Plugin-GetSDKToken0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8Plugin-GetSDKUid0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8Plugin-Init0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8Plugin-_UseMockVersion0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8Plugin-_MockLogin0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8Plugin-Login0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8Plugin-Logout0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8Plugin-OnProductListChanged0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8Plugin-Pay0;  // const
        public static IFix.IDMAP0 Beyond-SDK-Impl-U8Plugin-TryLoadSDKMeta0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCheckForUpdateNode-get_type0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-Clear0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCheckForUpdateNode-_AlertAndQuitForExtraConfigFatalError0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCheckForUpdateNode-AlertNoMatchDeviceBeforeLogin0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCheckForUpdateNode-_GetRequestPram0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCheckForUpdateNode-_GetOnlineAppVersion0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-ShowWaitProgress0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-GetWaitTime0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCheckForUpdateNode-_CheckForUpdatePC0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCheckForUpdateNode-_CheckForUpdateCoroutinePC0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-ClearWaitProgress0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCheckForUpdateNode-_CheckForUpdateCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCheckForUpdateNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-_CreateDirectoryIfNotExists0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-Context-CompleteWithCode0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-Context-UncompleteWithCode0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-GetNoAlertCode0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-_GetLastGameInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-CheckLastGameInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-FWork-MarkFallback0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Fallback0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCheckForUpdateNode-_CheckForUpdateCoroutineMobile0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCreateRoleNode-get_type0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCreateRoleNode-WorkProcess-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCreateRoleNode-OnDispose0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCreateRoleNode-CanCancel0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCreateRoleNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginCreateRoleNode-WorkProcess-get_keepWaiting0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDownloadTask-GetDownloadProgress0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDownloadTask-GetDecompressProgress0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDownloadTask-GetWorkState0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDownloadTask-Tick0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDownloadTask-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDownloadTask-_GetFileList0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDownloadTask-_GetAllFileList0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-HasEnteredPreloadThisSession0;  // const
        public static IFix.IDMAP0 Beyond-Login-NetUsagePolicy-AllowMobileDataInLastCheck0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDownloadTask-_OnDownloadFinish0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDownloadTask-_OnDownloadError0;  // const
        public static IFix.IDMAP0 Beyond-Login-NetUsagePolicy-_GetPrettySizeStringUpToMB0;  // const
        public static IFix.IDMAP0 Beyond-Login-NetUsagePolicy-CheckIfAllowDownload0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDownloadTask-_OnDownloadPaused0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDownloadTask-_StartDownloadImpl0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDownloadTask-_StartDownload0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameSettingInitializer-_TryInitializeComponents0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameSettingInitializer-Awake0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameSettingInitializer-_InitGameSettingNotchPadding0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameSettingInitializer-_InitGameSettingStateInLogin0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameSettingInitializer-OnEnable0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdateNode-get_type0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdateNode-_GameUpdateCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdateNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-_YieldCheckNetUsagePolicy0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-_YieldErrorAlert0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-_OnDownloadStart0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-_OnDownloadProgress0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-_DoTaskUpdating0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-_UpdateGame0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-_WaitForTaskCancel0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdater-DoUpdate0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGameUpdateNode-_GameUpdateCoroutineMobileImpl0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-get_type0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-IsInterruptable0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-TryInterrupt0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-_FallbackToInitNodeImpl0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-_LogoutToInitNode0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-_FallbackToInitWithMask0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-_ReLoginWhenErrorEncountered0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-_OnAuthError0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-_ConfirmServer0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-_MockManualSelectServer0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-_SaveCurrentUserToServerConfig0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-_NetConnectAndGSLogin0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-_GlobalLoginSucBusiness0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-_EnterGameCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-EnterGame0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-_OnEventLogout0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginViewModel-RequestResourceRepair0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-_FallbackToResRepairImpl0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-_OnEventResourceRepair0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-OnEvent0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-get_keepWaiting0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-MarkAborting0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-_ExtractErrorInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGSNode-EnterGameServiceFlow-_YieldAlertError0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdateModel-SimpleProgress-GetCurrent0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdateModel-SimpleProgress-GetTotal0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdateModel-SimpleProgress-GetInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-MarkInitialAssetChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-CheckInitialAssetChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-MarkNeedClearFolder0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-NeedClearFolder0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-MarkClearSuccessed0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-MarkSoftRestart0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-ClearSoftRestartFlag0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-get_keepWaiting0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginLocalResTask-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_UpdateResVersionDev0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_UpdateResVersionProd0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_UpdateResVersion0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_SetResVersionForLog0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_CurrentWorkMode0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_GetOnlineResVersion0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-WorkContext-GetHotUpdateInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-ResourceRootUrl0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_ResourceIndexUrl0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_LoadOrDownloadResIndex0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_GenResIndex0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_ResourcePatchUrl0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_ResourceFileUrl0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_LoadStreamingIndex0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_GenRiff0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_ResourcePatchRootUrl0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_DownloadPatchInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_CreateUpdatedLocalResPref0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_StartDownloadingShaderWarmupIfNeeded0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_EventLogDownloadProgress0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-WriteLocalResPrefToFile0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_GenLocalRef0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_CheckAndDownloadResource0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_StopDownloadingShaderWarmupIfNeeded0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-HotUpdateInfo-CheckIfNeedReload0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-HotUpdateInfo-HasChangedByType0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-WorkContext-MarkIllegalItemsInLocalRes0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_CheckLocalResConsistency0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_ClearDownloadFolder0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-WorkContext-HasChangedByType0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_ReloadResourceIndexes0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_MainCoroutineModeDownload0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginLocalResTask-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_MainCoroutineModeLocal0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_MainCoroutineModeSkip0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_CreateMainCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginViewModel-RequestSkipU8Login0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_VitalError0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-_Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-StartHotUpdate0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginHotUpdater-HotUpdateInfo-HasAssetsChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginProgress-GetCurrent0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginProgress-GetTotal0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginProgress-FDownloadSize-_PickUnit0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginProgress-FDownloadSize-_DisplayWithUnit0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginProgress-FDownloadSize-_FormatDisplayInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginProgress-FDownloadSize-_FormatSpeedInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginProgress-FDownloadSize-_FormatProgressInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginProgress-FDownloadSize-SetSize0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginProgress-FDownloadSize-SetProgress0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginProgress-FDownloadSize-SetIdle0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginProgress-GetInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginInitNode-get_type0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginInitNode-_YieldAlertError0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginInitNode-_InitRemoteConfigs0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginInitNode-_InitCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginInitNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadNode-get_type0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-MarkEnteredPreloadThisSession0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadNode-_ShowPreloadResourcePanel0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadNode-_QuitGame0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadNode-_WarmUp0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadNode-_OnGetServerListSuc0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadNode-_GetServerListInfoMock0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadNode-_OnGetServerListFail0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadNode-_GetServerListInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadNode-_GetServerList0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadNode-_EnterPreLoad0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadNode-_LoginPreLoad0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSDKInitNode-get_type0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSDKInitNode-_WaitForSDKInited0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSDKInitNode-_InitSubSDKs0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSDKInitNode-_SDKInitCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSDKInitNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-get_type0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_EnableBlurMask0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_OnSDKViewDisabled0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnReceiveRedDotAndPopup0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_UpdateAnnounceRedDot0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnBulletinOpen0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnBulletinClose0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-InitBulletinAndRedAfterLoginSuc0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-InitBulletinAndRedAfterLoginSuc0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_OnLoginSuc0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_OnGameProtocolUpdateCheckSucceeded0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_OnLoginCanceled0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_SetCursorOnLoginStart0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_SetCursorOnLoginFinish0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_StartGameProtocolUpdateCheck0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_StartSDKLogin0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_OnLoginFail0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_OnGameProtocolUpdateCheckFailed0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_ClearGameProtocolUpdateCheckEvents0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_ClearLoginCaches0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_SkipSDKLogin0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_FinishLogin0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-_LoginCoroutine0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-U8LoginNode-OnEvent0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginViewModel-FAgeTipsPolicy-get_display0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginViewModel-IsAgeTipsVisible0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginViewModel-IsGovTipsVisible0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginViewModel-IsMenuVisible0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginViewModel-_LoadAgeTipsPolicy0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsModel-CanDisplay0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsProp-ClearContentCache0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsProp-UpdateContent0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-_RegisterNodes0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Node-CanCancel0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Worker-Cancel0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-CancelAndFallback0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-AddEventReceiver0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Worker-UpdateOnce0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-UpdateOnce0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Node-OnInit0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginWorkFlow-Node-Init0;  // const
        public static IFix.IDMAP0 Beyond-Login-HotUpdateNode-get_type0;  // const
        public static IFix.IDMAP0 Beyond-Login-HotUpdateNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-HotUpdateNode-OnDispose0;  // const
        public static IFix.IDMAP0 Beyond-Login-WaitForSecondsNode-_NotifyTimeout0;  // const
        public static IFix.IDMAP0 Beyond-Login-WaitForSecondsNode-_IsTimeout0;  // const
        public static IFix.IDMAP0 Beyond-Login-WaitForSecondsNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-WaitForSecondsNode-_DisposeTimer0;  // const
        public static IFix.IDMAP0 Beyond-Login-WaitForSecondsNode-OnDispose0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadPostDelayNode-get_type0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadPostDelayNode-WaitForSeconds0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadPostDelayNode-_DisableLoadingPanel0;  // const
        public static IFix.IDMAP0 Beyond-Login-PreloadPostDelayNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-Trans2GsLoginNode-get_type0;  // const
        public static IFix.IDMAP0 Beyond-Login-Trans2GsLoginNode-WaitForSeconds0;  // const
        public static IFix.IDMAP0 Beyond-Login-Trans2GsLoginNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-FinishNode-get_type0;  // const
        public static IFix.IDMAP0 Beyond-Login-FinishNode-Work0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-get_tickOption0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-OnBindToManager0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-_EnsurePlayer0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-_RandomIntervalA1s0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-_ResetToA10;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-_PlayA1A2Impl0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-_TransitionToA20;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-_ChangeToState0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-_PlayA1sAndTriggerNext0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-OnValueChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneComponent-Start0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-Start0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-Tick0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-OnRelease0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneAnimCtrl-GetAnimationClips0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneComponent-get_tickOption0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneRoot-get_tickOption0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginSceneRoot-OnBindToManager0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsDetailPanel-_EventOnCloseClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsDetailPanel-Init0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsDetailPanel-_SplitLongString0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsDetailPanel-_SplitLicenseToSegments0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsDetailPanel-_RenderContent0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsDetailPanel-_ShowContent0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsDetailPanel-OnValueChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsPanel-_EventOnOpenAgeTipsClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsPanel-OnInit0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsPanel-_UpdateAgeTipsPolicy0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAgeTipsPanel-OnValueChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAlertDialog-_OnConfirmClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginAlertDialog-OnInit0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDecorateUI-get_tickOption0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDecorateUI-_TriggerTrans2GSLoginImpl0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDecorateUI-OnValueChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDecorateUI-_TimeManagerTick0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDecorateUI-Tick0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginDecorateUI-OnRelease0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginEnterGamePanel-_OnEnterGameClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginEnterGamePanel-_InitIfNot0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-ChildPanel-SetPanelVisibleByFadeSwitchTween0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginEnterGamePanel-OnValueChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGovTipsPanel-_EventOnOpenAgeTipsClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGovTipsPanel-OnInit0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginGovTipsPanel-OnValueChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginJudgeDialog-_OnPositiveClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginJudgeDialog-_OnNegativeClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginJudgeDialog-OnInit0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnAnnounceClosed0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnAnnounceClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnSettingClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnResCheckClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnLogoutClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnQuitGameClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnLoginClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnQRClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnServerZoneBtnClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnFocusMenuGroup0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-OnInit0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_CanQRLogin0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-OnValueChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_UpdateServerZone0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-Awake0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnRequestRelogin0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnInputChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnUpdateRedDotForSDKListener0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-OnEnable0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-OnDisable0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-ChildPanel-OnDestroy0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-OnDestroy0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnCustomServiceClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMenuPanel-_OnAccountCenterClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-get_instance0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-Refresh0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-_SmokeEnterGame0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginMockUIDPanel-OnDestroy0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginResourcePanel-_UpdateResourceInfo0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginResourcePanel-OnValueChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginResourcePanel-Update0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-_OnNotchPaddingChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-Awake0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-OnDestroy0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginRootPanel-ChildPanel-SetPanelVisibleByAnimSwitchTween0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectItemView-_OnClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectItemView-RefreshView0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectItemView-NaviToBtnTarget0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectItemView-SetSelected0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectModel-get_curServerZoneData0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectModel-InitData0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectPanel-_OnConfirmClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectPanel-_OnCloseClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectPanel-_UpdateServerCell0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectPanel-OnInit0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectPanel-_NaviToSelectedItem0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectPanel-_OnInputChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectPanel-OnDisable0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectPanel-_WaitingForShowListAwake0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectPanel-OnEnable0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectPanel-_UpdateServerSelected0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginServerSelectPanel-OnValueChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVersionPanel-get_versionZoneType0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVersionPanel-get_versionPlatform0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVersionPanel-get_versionAppType0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVersionPanel-get_versionVersionNum0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVersionPanel-get_versionProjectCL0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVersionPanel-get_displayGameVersion0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVersionPanel-Start0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-_OnConfirmClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-_OnConfirmClicked1;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelItemView-SetSelected0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-_UpdateSelected0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-_UpdateList0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-_RefreshConfirmInteractable0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-UpdateSelected0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelItemView-_OnClicked0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelItemView-RefreshView0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-_UpdateCell0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-_ResolveInitialSelectedIndex0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelItemView-NaviToBtnTarget0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-_NaviToSelectedItem0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-OnInit0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-_OnInputTypeChanged0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-OnEnable0;  // const
        public static IFix.IDMAP0 Beyond-Login-LoginVocResSelDialog-OnDisable0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-WarmUpProgress-GetTotal0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-WarmUpProgress-GetCurrent0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Entry-WarmUpProgress-GetInfo0;  // const

    }

}

