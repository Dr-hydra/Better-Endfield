// ========================================================
// Dumped by @desirepro
// Assembly: Unity.Formats.USD.Common.dll
// Classes:  10
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
    public sealed struct UsageAnalyticsData
    {
        // Fields
        public System.Boolean InitSucceeded;  // 0x10
        public System.Double TimeTakenMs;  // 0x18

    }

    // TypeToken: 0x2000006  // size: 0x28
    public sealed struct ImportAnalyticsData
    {
        // Fields
        public System.String FileExtension;  // 0x10
        public System.Double TimeTakenMs;  // 0x18
        public System.Boolean Succeeded;  // 0x20
        public System.Boolean IncludesMeshes;  // 0x21
        public System.Boolean IncludesPointInstancer;  // 0x22
        public System.Boolean IncludesMaterials;  // 0x23
        public System.Boolean IncludesSkel;  // 0x24

    }

    // TypeToken: 0x2000007  // size: 0x1C
    public sealed struct ImportResult
    {
        // Fields
        public System.Boolean Success;  // 0x10
        public Unity.Formats.USD.ImportType ImportType;  // 0x14
        public System.Boolean ContainsMeshes;  // 0x18
        public System.Boolean ContainsPointInstancer;  // 0x19
        public System.Boolean ContainsSkel;  // 0x1a
        public System.Boolean ContainsMaterials;  // 0x1b

        // Properties
        Unity.Formats.USD.UsdEditorAnalytics.ImportResult Default { get; /* RVA: 0x04D85DC0 */ }

    }

    // TypeToken: 0x2000008  // size: 0x28
    public sealed struct ReimportAnalyticsData
    {
        // Fields
        public System.String FileExtension;  // 0x10
        public System.Double TimeTakenMs;  // 0x18
        public System.Boolean Succeeded;  // 0x20
        public System.Boolean ForceRebuild;  // 0x21
        public System.Boolean IncludesMeshes;  // 0x22
        public System.Boolean IncludesPointInstancer;  // 0x23
        public System.Boolean IncludesMaterials;  // 0x24
        public System.Boolean IncludesSkel;  // 0x25

    }

    // TypeToken: 0x2000009  // size: 0x28
    public sealed struct ExportAnalyticsData
    {
        // Fields
        public System.String FileExtension;  // 0x10
        public System.Double TimeTakenMs;  // 0x18
        public System.Boolean Succeeded;  // 0x20
        public System.Boolean OnlyOverrides;  // 0x21

    }

    // TypeToken: 0x200000A  // size: 0x28
    public sealed struct RecorderExportAnalyticsData
    {
        // Fields
        public System.String FileExtension;  // 0x10
        public System.Double TimeTakenMs;  // 0x18
        public System.Boolean Succeeded;  // 0x20
        public System.Boolean OnlyOverrides;  // 0x21
        public System.Int32 FrameCount;  // 0x24

    }

namespace Unity.Formats.USD
{

    // TypeToken: 0x2000002  // size: 0x10
    public static class InitUsd
    {
        // Fields
        private static System.Boolean m_usdInitialized;  // static @ 0x0
        private static System.Boolean m_usdInitializeFailed;  // static @ 0x1
        private static USD.NET.Unity.DiagnosticHandler m_handler;  // static @ 0x8

        // Methods
        // RVA: 0x0B165660  token: 0x6000001
        public static System.Boolean Initialize() { }
        // RVA: 0x0B165900  token: 0x6000002
        private static System.Void SetupUsdPath(System.String sourceFilePath) { }

    }

    // TypeToken: 0x2000003  // size: 0x14
    public sealed struct ImportType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.Formats.USD.ImportType Initial;  // const
        public static Unity.Formats.USD.ImportType ForceRebuild;  // const
        public static Unity.Formats.USD.ImportType Refresh;  // const
        public static Unity.Formats.USD.ImportType Streaming;  // const

    }

    // TypeToken: 0x2000004  // size: 0x10
    public static class UsdEditorAnalytics
    {
        // Fields
        private static System.Int32 k_MaxEventsPerHour;  // const
        private static System.Int32 k_MaxNumberOfElements;  // const
        private static System.String k_VendorKey;  // const
        private static System.String k_UsageEventName;  // const
        private static System.String k_ImportEventName;  // const
        private static System.String k_ReimportEventName;  // const
        private static System.String k_ExportEventName;  // const
        private static System.String k_RecorderExportEventName;  // const
        public static System.Collections.Generic.Dictionary<System.String,System.Boolean> sUsdEditorAnalyticsEvents;  // static @ 0x0

        // Methods
        // RVA: 0x012081B0  token: 0x6000003
        private static System.Boolean RegisterAnalytics(System.String eventName) { }
        // RVA: 0x041E1670  token: 0x6000004
        public static System.Void SendUsageEvent(System.Boolean success, System.Double timeTakenMs) { }
        // RVA: 0x041E1670  token: 0x6000005
        public static System.Void SendImportEvent(System.String fileExtension, System.Double timeTakenMs, Unity.Formats.USD.UsdEditorAnalytics.ImportResult importResult) { }
        // RVA: 0x041E1670  token: 0x6000006
        public static System.Void SendReimportEvent(System.String fileExtension, System.Double timeTakenMs, Unity.Formats.USD.UsdEditorAnalytics.ImportResult importResult) { }
        // RVA: 0x041E1670  token: 0x6000007
        public static System.Void SendExportEvent(System.String fileExtension, System.Double timeTakenMs, System.Boolean exportSucceeded, System.Boolean onlyOverrides) { }
        // RVA: 0x041E1670  token: 0x6000008
        public static System.Void SendRecorderExportEvent(System.String fileExtension, System.Double timeTakenMs, System.Boolean exportSucceeded, System.Boolean onlyOverrides, System.Int32 frameCount) { }
        // RVA: 0x0B165A38  token: 0x6000009
        private static System.Void .cctor() { }

    }

}

