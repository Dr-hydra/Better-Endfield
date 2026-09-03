// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.UnityAnalyticsModule.dll
// Classes:  11
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000003  // size: 0x80
    public sealed class UpdatedEventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x053DEDC8  token: 0x6000004
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x053DEDB8  token: 0x6000005
        public virtual System.Void Invoke() { }

    }

    // TypeToken: 0x2000006  // size: 0x14
    public sealed struct Tag
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.RemoteConfigSettingsHelper.Tag kUnknown;  // const
        public static UnityEngine.RemoteConfigSettingsHelper.Tag kIntVal;  // const
        public static UnityEngine.RemoteConfigSettingsHelper.Tag kInt64Val;  // const
        public static UnityEngine.RemoteConfigSettingsHelper.Tag kUInt64Val;  // const
        public static UnityEngine.RemoteConfigSettingsHelper.Tag kDoubleVal;  // const
        public static UnityEngine.RemoteConfigSettingsHelper.Tag kBoolVal;  // const
        public static UnityEngine.RemoteConfigSettingsHelper.Tag kStringVal;  // const
        public static UnityEngine.RemoteConfigSettingsHelper.Tag kArrayVal;  // const
        public static UnityEngine.RemoteConfigSettingsHelper.Tag kMixedArrayVal;  // const
        public static UnityEngine.RemoteConfigSettingsHelper.Tag kMapVal;  // const
        public static UnityEngine.RemoteConfigSettingsHelper.Tag kMaxTags;  // const

    }

    // TypeToken: 0x200000A  // size: 0x80
    public sealed class SessionStateChanged : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B588ACC  token: 0x6000009
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02C448D0  token: 0x600000A
        public virtual System.Void Invoke(UnityEngine.Analytics.AnalyticsSessionState sessionState, System.Int64 sessionId, System.Int64 sessionElapsedTime, System.Boolean sessionChanged) { }

    }

    // TypeToken: 0x200000B  // size: 0x80
    public sealed class IdentityTokenChanged : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x600000B
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x600000C
        public virtual System.Void Invoke(System.String token) { }

    }

namespace UnityEngine
{

    // TypeToken: 0x2000002  // size: 0x10
    public static class RemoteSettings
    {
        // Fields
        private static UnityEngine.RemoteSettings.UpdatedEventHandler Updated;  // static @ 0x0
        private static System.Action BeforeFetchFromServer;  // static @ 0x8
        private static System.Action<System.Boolean,System.Boolean,System.Int32> Completed;  // static @ 0x10

        // Methods
        // RVA: 0x0B588A84  token: 0x6000001
        private static System.Void RemoteSettingsUpdated(System.Boolean wasLastUpdatedFromServer) { }
        // RVA: 0x0B5889C8  token: 0x6000002
        private static System.Void RemoteSettingsBeforeFetchFromServer() { }
        // RVA: 0x0B588A0C  token: 0x6000003
        private static System.Void RemoteSettingsUpdateCompleted(System.Boolean wasLastUpdatedFromServer, System.Boolean settingsChanged, System.Int32 response) { }

    }

    // TypeToken: 0x2000004  // size: 0x20
    public class RemoteConfigSettings
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10
        private System.Action<System.Boolean> Updated;  // 0x18

        // Methods
        // RVA: 0x0B5889A0  token: 0x6000006
        private static System.Void RemoteConfigSettingsUpdated(UnityEngine.RemoteConfigSettings rcs, System.Boolean wasLastUpdatedFromServer) { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public static class RemoteConfigSettingsHelper
    {
    }

}

namespace UnityEngine.Analytics
{

    // TypeToken: 0x2000007  // size: 0x10
    public class ContinuousEvent
    {
    }

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct AnalyticsSessionState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Analytics.AnalyticsSessionState kSessionStopped;  // const
        public static UnityEngine.Analytics.AnalyticsSessionState kSessionStarted;  // const
        public static UnityEngine.Analytics.AnalyticsSessionState kSessionPaused;  // const
        public static UnityEngine.Analytics.AnalyticsSessionState kSessionResumed;  // const

    }

    // TypeToken: 0x2000009  // size: 0x10
    public static class AnalyticsSessionInfo
    {
        // Fields
        private static UnityEngine.Analytics.AnalyticsSessionInfo.SessionStateChanged sessionStateChanged;  // static @ 0x0
        private static UnityEngine.Analytics.AnalyticsSessionInfo.IdentityTokenChanged identityTokenChanged;  // static @ 0x8

        // Methods
        // RVA: 0x04D3E5C0  token: 0x6000007
        private static System.Void CallSessionStateChanged(UnityEngine.Analytics.AnalyticsSessionState sessionState, System.Int64 sessionId, System.Int64 sessionElapsedTime, System.Boolean sessionChanged) { }
        // RVA: 0x0B588950  token: 0x6000008
        private static System.Void CallIdentityTokenChanged(System.String token) { }

    }

}

