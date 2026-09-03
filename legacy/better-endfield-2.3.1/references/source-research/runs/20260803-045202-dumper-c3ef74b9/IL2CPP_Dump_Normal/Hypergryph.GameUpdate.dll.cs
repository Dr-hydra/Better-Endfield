// ========================================================
// Dumped by @desirepro
// Assembly: Hypergryph.GameUpdate.dll
// Classes:  9
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000003  // size: 0x18
    public sealed class <>c__DisplayClass6_0
    {
        // Fields
        public System.String data;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000005
        public System.Void .ctor() { }
        // RVA: 0x09DE40F0  token: 0x6000006
        private System.Void <onLatestGame>b__0() { }

    }

    // TypeToken: 0x2000004  // size: 0x18
    public sealed class <>c__DisplayClass7_0
    {
        // Fields
        public System.Action action;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000007
        public System.Void .ctor() { }
        // RVA: 0x06DFA030  token: 0x6000008
        private System.Void <runInMainTread>b__0(System.Object p) { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Hypergryph.SDK.HGGameUpdateSDK.<>c <>9;  // static @ 0x0
        public static System.Action<System.String> <>9__6_0;  // static @ 0x8

        // Methods
        // RVA: 0x09DE4140  token: 0x6000027
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000028
        public System.Void .ctor() { }
        // RVA: 0x09DE4054  token: 0x6000029
        private System.Void <GetLatestGame>b__6_0(System.String result) { }

    }

namespace Hypergryph.SDK
{

    // TypeToken: 0x2000002  // size: 0x18
    public class HGGameUpdateCallback : UnityEngine.MonoBehaviour
    {
        // Fields
        private static Hypergryph.SDK.IHGGameUpdateSDKCallback m_callback;  // static @ 0x0
        private static System.String gameObjectName;  // const
        private static System.Threading.SynchronizationContext mainThreadContext;  // static @ 0x8
        public static Hypergryph.SDK.HGGameUpdateCallback s_instance;  // static @ 0x10

        // Methods
        // RVA: 0x09DE3274  token: 0x6000001
        public System.Void .ctor() { }
        // RVA: 0x09DE30B8  token: 0x6000002
        public static System.Void Init(Hypergryph.SDK.IHGGameUpdateSDKCallback callback) { }
        // RVA: 0x09DE32D8  token: 0x6000003
        public System.Void onLatestGame(System.String data) { }
        // RVA: 0x09DE3390  token: 0x6000004
        private System.Void runInMainTread(System.Action action) { }

    }

    // TypeToken: 0x2000005
    public interface IHGGameUpdateSDKCallback
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000009
        public virtual System.Void onLatestGame(System.String data) { }

    }

    // TypeToken: 0x2000006
    public interface IHGGameUpdateSDK
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600000A
        public virtual System.Int32 Init(System.String config) { }
        // RVA: -1  // abstract  token: 0x600000B
        public virtual System.Void GetLatestGame(System.Action<System.String> onResult) { }
        // RVA: -1  // abstract  token: 0x600000C
        public virtual System.Int64 Update(System.Int32 updateType, System.Boolean useMobileData) { }
        // RVA: -1  // abstract  token: 0x600000D
        public virtual System.Int32 EnableMobileData(System.Int64 taskId) { }
        // RVA: -1  // abstract  token: 0x600000E
        public virtual System.Int32 GetTaskState(System.Int64 taskId) { }
        // RVA: -1  // abstract  token: 0x600000F
        public virtual System.Int32 Resume(System.Int64 taskId) { }
        // RVA: -1  // abstract  token: 0x6000010
        public virtual System.Int32 CancelAndClear(System.Int64 taskId) { }
        // RVA: -1  // abstract  token: 0x6000011
        public virtual System.Int32 Cancel(System.Int64 taskId) { }
        // RVA: -1  // abstract  token: 0x6000012
        public virtual System.Int32 Install(System.Int64 taskId) { }
        // RVA: -1  // abstract  token: 0x6000013
        public virtual System.Int64 GetDownloadSpeed(System.Int64 taskId) { }
        // RVA: -1  // abstract  token: 0x6000014
        public virtual System.Int64 GetDownloadedSize(System.Int64 taskId) { }
        // RVA: -1  // abstract  token: 0x6000015
        public virtual System.Int64 GetTotalDownloadSize(System.Int64 taskId) { }
        // RVA: -1  // abstract  token: 0x6000016
        public virtual System.Int64 GetEstimatedDownloadSize(System.Int32 updateType) { }
        // RVA: -1  // abstract  token: 0x6000017
        public virtual System.Int32 SetNotificationTitle(System.String titleConfig) { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public static class HGGameUpdateSDK
    {
        // Fields
        private static Hypergryph.SDK.IHGGameUpdateSDK s_gu;  // static @ 0x0
        private static Hypergryph.SDK.IHGGameUpdateSDKCallback m_callback;  // static @ 0x8
        private static System.Threading.SynchronizationContext mainThreadContext;  // static @ 0x10

        // Properties
        Hypergryph.SDK.IHGGameUpdateSDK gameUpdateSDK { get; /* RVA: 0x09DE3FC0 */ }

        // Methods
        // RVA: 0x09DE3D50  token: 0x6000019
        public static System.Int32 Init(System.String config, Hypergryph.SDK.IHGGameUpdateSDKCallback callback) { }
        // RVA: 0x09DE3B70  token: 0x600001A
        public static System.Void GetLatestGame() { }
        // RVA: 0x09DE3F54  token: 0x600001B
        public static System.Int64 Update(System.Int32 updateType, System.Boolean useMobileData) { }
        // RVA: 0x09DE39F0  token: 0x600001C
        public static System.Int32 EnableMobileData(System.Int64 taskId) { }
        // RVA: 0x09DE3C90  token: 0x600001D
        public static System.Int32 GetTaskState(System.Int64 taskId) { }
        // RVA: 0x09DE3E94  token: 0x600001E
        public static System.Int32 Resume(System.Int64 taskId) { }
        // RVA: 0x09DE3930  token: 0x600001F
        public static System.Int32 CancelAndClear(System.Int64 taskId) { }
        // RVA: 0x09DE3990  token: 0x6000020
        public static System.Int32 Cancel(System.Int64 taskId) { }
        // RVA: 0x09DE3E34  token: 0x6000021
        public static System.Int32 Install(System.Int64 taskId) { }
        // RVA: 0x09DE3A50  token: 0x6000022
        public static System.Int64 GetDownloadSpeed(System.Int64 taskId) { }
        // RVA: 0x09DE3AB0  token: 0x6000023
        public static System.Int64 GetDownloadedSize(System.Int64 taskId) { }
        // RVA: 0x09DE3CF0  token: 0x6000024
        public static System.Int64 GetTotalDownloadSize(System.Int64 taskId) { }
        // RVA: 0x09DE3B10  token: 0x6000025
        public static System.Int64 GetEstimatedDownloadSize(System.Int32 updateType) { }
        // RVA: 0x09DE3EF4  token: 0x6000026
        public static System.Int32 SetNotificationTitle(System.String titleConfig) { }

    }

    // TypeToken: 0x2000009  // size: 0x10
    public class HGGameUpdateSDKPluginDefault : Hypergryph.SDK.IHGGameUpdateSDK
    {
        // Methods
        // RVA: 0x09DE3798  token: 0x600002A
        public virtual System.Int32 Init(System.String config) { }
        // RVA: 0x09DE3668  token: 0x600002B
        public virtual System.Void GetLatestGame(System.Action<System.String> onResult) { }
        // RVA: 0x09DE38D8  token: 0x600002C
        public virtual System.Int64 Update(System.Int32 updateType, System.Boolean useMobileData) { }
        // RVA: 0x09DE3528  token: 0x600002D
        public virtual System.Int32 EnableMobileData(System.Int64 taskId) { }
        // RVA: 0x09DE36F8  token: 0x600002E
        public virtual System.Int32 GetTaskState(System.Int64 taskId) { }
        // RVA: 0x09DE3838  token: 0x600002F
        public virtual System.Int32 Resume(System.Int64 taskId) { }
        // RVA: 0x09DE3488  token: 0x6000030
        public virtual System.Int32 CancelAndClear(System.Int64 taskId) { }
        // RVA: 0x09DE34D8  token: 0x6000031
        public virtual System.Int32 Cancel(System.Int64 taskId) { }
        // RVA: 0x09DE37E8  token: 0x6000032
        public virtual System.Int32 Install(System.Int64 taskId) { }
        // RVA: 0x09DE3578  token: 0x6000033
        public virtual System.Int64 GetDownloadSpeed(System.Int64 taskId) { }
        // RVA: 0x09DE35C8  token: 0x6000034
        public virtual System.Int64 GetDownloadedSize(System.Int64 taskId) { }
        // RVA: 0x09DE3748  token: 0x6000035
        public virtual System.Int64 GetTotalDownloadSize(System.Int64 taskId) { }
        // RVA: 0x09DE3618  token: 0x6000036
        public virtual System.Int64 GetEstimatedDownloadSize(System.Int32 updateType) { }
        // RVA: 0x09DE3888  token: 0x6000037
        public virtual System.Int32 SetNotificationTitle(System.String titleConfig) { }
        // RVA: 0x041E1670  token: 0x6000038
        public System.Void .ctor() { }

    }

}

