// ========================================================
// Dumped by @desirepro
// Assembly: Sirenix.Serialization.Config.dll
// Classes:  8
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace Sirenix.Serialization
{

    // TypeToken: 0x2000002  // size: 0x28
    public class CustomLogger : Sirenix.Serialization.ILogger
    {
        // Fields
        private System.Action<System.String> logWarningDelegate;  // 0x10
        private System.Action<System.String> logErrorDelegate;  // 0x18
        private System.Action<System.Exception> logExceptionDelegate;  // 0x20

        // Methods
        // RVA: 0x0AC46D10  token: 0x6000001
        public System.Void .ctor(System.Action<System.String> logWarningDelegate, System.Action<System.String> logErrorDelegate, System.Action<System.Exception> logExceptionDelegate) { }
        // RVA: 0x0810F944  token: 0x6000002
        public virtual System.Void LogWarning(System.String warning) { }
        // RVA: 0x09B21654  token: 0x6000003
        public virtual System.Void LogError(System.String error) { }
        // RVA: 0x0AC46CF0  token: 0x6000004
        public virtual System.Void LogException(System.Exception exception) { }

    }

    // TypeToken: 0x2000003  // size: 0x14
    public sealed struct DataFormat
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Sirenix.Serialization.DataFormat Binary;  // const
        public static Sirenix.Serialization.DataFormat JSON;  // const
        public static Sirenix.Serialization.DataFormat Nodes;  // const

    }

    // TypeToken: 0x2000004  // size: 0x10
    public static class DefaultLoggers
    {
        // Fields
        private static readonly System.Object LOCK;  // static @ 0x0
        private static Sirenix.Serialization.ILogger unityLogger;  // static @ 0x8

        // Properties
        Sirenix.Serialization.ILogger DefaultLogger { get; /* RVA: 0x0AC46EC4 */ }
        Sirenix.Serialization.ILogger UnityLogger { get; /* RVA: 0x0AC46F04 */ }

        // Methods
        // RVA: 0x0AC46E54  token: 0x6000007
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x14
    public sealed struct ErrorHandlingPolicy
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Sirenix.Serialization.ErrorHandlingPolicy Resilient;  // const
        public static Sirenix.Serialization.ErrorHandlingPolicy ThrowOnErrors;  // const
        public static Sirenix.Serialization.ErrorHandlingPolicy ThrowOnWarningsAndErrors;  // const

    }

    // TypeToken: 0x2000006  // size: 0x30
    public class GlobalSerializationConfig : Sirenix.Utilities.GlobalConfig`1
    {
        // Fields
        public static System.String ODIN_SERIALIZATION_CAUTIONARY_WARNING_TEXT;  // const
        public static System.String ODIN_PREFAB_CAUTIONARY_WARNING_TEXT;  // const
        public static System.String ODIN_SERIALIZATION_CAUTIONARY_WARNING_BUTTON_TEXT;  // const
        public static System.String ODIN_PREFAB_CAUTIONARY_WARNING_BUTTON_TEXT;  // const
        private static readonly Sirenix.Serialization.DataFormat[] BuildFormats;  // static @ 0x0
        public System.Boolean HideSerializationCautionaryMessage;  // 0x18
        public System.Boolean HidePrefabCautionaryMessage;  // 0x19
        public System.Boolean HideOdinSerializeAttributeWarningMessages;  // 0x1a
        public System.Boolean HideNonSerializedShowInInspectorWarningMessages;  // 0x1b
        private Sirenix.Serialization.DataFormat buildSerializationFormat;  // 0x1c
        private Sirenix.Serialization.DataFormat editorSerializationFormat;  // 0x20
        private Sirenix.Serialization.LoggingPolicy loggingPolicy;  // 0x24
        private Sirenix.Serialization.ErrorHandlingPolicy errorHandlingPolicy;  // 0x28

        // Properties
        Sirenix.Serialization.ILogger Logger { get; /* RVA: 0x0AC47598 */ }
        Sirenix.Serialization.DataFormat EditorSerializationFormat { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        Sirenix.Serialization.DataFormat BuildSerializationFormat { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x04D86320 */ }
        Sirenix.Serialization.LoggingPolicy LoggingPolicy { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        Sirenix.Serialization.ErrorHandlingPolicy ErrorHandlingPolicy { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }

        // Methods
        // RVA: 0x0AC47204  token: 0x6000011
        private System.Void OnInspectorGUI() { }
        // RVA: 0x0AC4755C  token: 0x6000012
        public System.Void .ctor() { }
        // RVA: 0x0AC474D4  token: 0x6000013
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000007
    public interface ILogger
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000014
        public virtual System.Void LogWarning(System.String warning) { }
        // RVA: -1  // abstract  token: 0x6000015
        public virtual System.Void LogError(System.String error) { }
        // RVA: -1  // abstract  token: 0x6000016
        public virtual System.Void LogException(System.Exception exception) { }

    }

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct LoggingPolicy
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Sirenix.Serialization.LoggingPolicy LogErrors;  // const
        public static Sirenix.Serialization.LoggingPolicy LogWarningsAndErrors;  // const
        public static Sirenix.Serialization.LoggingPolicy Silent;  // const

    }

}

