// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.SubsystemsModule.dll
// Classes:  14
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace UnityEngine
{

    // TypeToken: 0x2000002  // size: 0x20
    public class IntegratedSubsystem
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10
        private UnityEngine.ISubsystemDescriptor m_SubsystemDescriptor;  // 0x18

        // Methods
        // RVA: 0x0B42B6FC  token: 0x6000001
        private System.Void SetHandle(UnityEngine.IntegratedSubsystem subsystem) { }
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000003
    public class IntegratedSubsystem`1 : UnityEngine.IntegratedSubsystem
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000003
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x18
    public abstract class IntegratedSubsystemDescriptor : UnityEngine.ISubsystemDescriptor
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10

        // Properties
        System.String id { get; /* RVA: 0x0B42B6F0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000005
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000005
    public class IntegratedSubsystemDescriptor`1 : UnityEngine.IntegratedSubsystemDescriptor
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000006
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public static class SubsystemDescriptorBindings
    {
        // Methods
        // RVA: 0x0B42B78C  token: 0x6000007
        public static System.String GetId(System.IntPtr descriptorPtr) { }

    }

    // TypeToken: 0x2000007
    public interface ISubsystemDescriptor
    {
        // Properties
        System.String id { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public abstract class Subsystem
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000009
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x18
    public abstract class SubsystemDescriptor : UnityEngine.ISubsystemDescriptor
    {
        // Fields
        private System.String <id>k__BackingField;  // 0x10

        // Properties
        System.String id { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600000B
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x10
    public static class Internal_SubsystemDescriptors
    {
        // Methods
        // RVA: 0x0B42B740  token: 0x600000C
        private static System.Void Internal_AddDescriptor(UnityEngine.SubsystemDescriptor descriptor) { }

    }

    // TypeToken: 0x200000B  // size: 0x10
    public static class SubsystemManager
    {
        // Fields
        private static System.Action beforeReloadSubsystems;  // static @ 0x0
        private static System.Action afterReloadSubsystems;  // static @ 0x8
        private static System.Collections.Generic.List<UnityEngine.IntegratedSubsystem> s_IntegratedSubsystems;  // static @ 0x10
        private static System.Collections.Generic.List<UnityEngine.SubsystemsImplementation.SubsystemWithProvider> s_StandaloneSubsystems;  // static @ 0x18
        private static System.Collections.Generic.List<UnityEngine.Subsystem> s_DeprecatedSubsystems;  // static @ 0x20
        private static System.Action reloadSubsytemsStarted;  // static @ 0x28
        private static System.Action reloadSubsytemsCompleted;  // static @ 0x30

        // Methods
        // RVA: 0x0B42C00C  token: 0x600000D
        private static System.Void ReloadSubsystemsStarted() { }
        // RVA: 0x0B42BF5C  token: 0x600000E
        private static System.Void ReloadSubsystemsCompleted() { }
        // RVA: 0x0B42BEC8  token: 0x600000F
        private static System.Void InitializeIntegratedSubsystem(System.IntPtr ptr, UnityEngine.IntegratedSubsystem subsystem) { }
        // RVA: 0x0B42BBAC  token: 0x6000010
        private static System.Void ClearSubsystems() { }
        // RVA: 0x0B42C0BC  token: 0x6000011
        private static System.Void StaticConstructScriptingClassMap() { }
        // RVA: 0x0B42C0E8  token: 0x6000012
        private static System.Void .cctor() { }
        // RVA: 0x0B42BD88  token: 0x6000013
        private static UnityEngine.IntegratedSubsystem GetIntegratedSubsystemByPtr(System.IntPtr ptr) { }

    }

}

namespace UnityEngine.SubsystemsImplementation
{

    // TypeToken: 0x200000C  // size: 0x10
    public static class SubsystemDescriptorStore
    {
        // Fields
        private static System.Collections.Generic.List<UnityEngine.IntegratedSubsystemDescriptor> s_IntegratedDescriptors;  // static @ 0x0
        private static System.Collections.Generic.List<UnityEngine.SubsystemsImplementation.SubsystemDescriptorWithProvider> s_StandaloneDescriptors;  // static @ 0x8
        private static System.Collections.Generic.List<UnityEngine.SubsystemDescriptor> s_DeprecatedDescriptors;  // static @ 0x10

        // Methods
        // RVA: 0x0B42B934  token: 0x6000014
        private static System.Void InitializeManagedDescriptor(System.IntPtr ptr, UnityEngine.IntegratedSubsystemDescriptor desc) { }
        // RVA: 0x0B42B7C0  token: 0x6000015
        private static System.Void ClearManagedDescriptors() { }
        // RVA: 0x0B42BA1C  token: 0x6000016
        private static System.Void ReportSingleSubsystemAnalytics(System.String id) { }
        // RVA: -1  // generic def  token: 0x6000017
        private static System.Void RegisterDescriptor(TDescriptor descriptor, System.Collections.Generic.List<TBaseTypeInList> storeInList) { }
        // RVA: 0x0B42B9B8  token: 0x6000018
        private static System.Void RegisterDeprecatedDescriptor(UnityEngine.SubsystemDescriptor descriptor) { }
        // RVA: 0x0B42BA50  token: 0x6000019
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x18
    public abstract class SubsystemDescriptorWithProvider : UnityEngine.ISubsystemDescriptor
    {
        // Fields
        private System.String <id>k__BackingField;  // 0x10

        // Properties
        System.String id { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600001B
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200000E  // size: 0x10
    public abstract class SubsystemWithProvider
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600001C
        protected System.Void .ctor() { }

    }

}

