// ========================================================
// Dumped by @desirepro
// Assembly: VFSEditor.dll
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

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x2000003  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000003
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000004
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000005
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x20
    public class DataNAttribute : System.Attribute
    {
        // Fields
        public System.Int32 capacity;  // 0x10
        public System.Int32 tSize;  // 0x14
        public System.Boolean ring;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000006
        public System.Void .ctor() { }

    }

}

namespace Beyond.VFS.Editor
{

    // TypeToken: 0x2000008  // size: 0x10
    public class VFSEditorUnsafeUtils
    {
        // Methods
        // RVA: 0x0B8961FC  token: 0x6000007
        public static System.Void TryGetBundleDeps(Beyond.ManifestBinary.ManifestDataBinary manifestDataBinary, Beyond.ManifestBinary.ManifestDataBinary.Bundle bundle, System.Collections.Generic.List<Beyond.ManifestBinary.ManifestDataBinary.Bundle>& bundles) { }
        // RVA: 0x0B89622C  token: 0x6000008
        public static System.Void TryGetBundleDirectDeps(Beyond.ManifestBinary.ManifestDataBinary manifestDataBinary, Beyond.ManifestBinary.ManifestDataBinary.Bundle bundle, System.Collections.Generic.List<Beyond.ManifestBinary.ManifestDataBinary.Bundle>& bundles) { }
        // RVA: 0x0B8963A0  token: 0x6000009
        public static System.Void TryGetBundleDirectReverseDeps(Beyond.ManifestBinary.ManifestDataBinary manifestDataBinary, Beyond.ManifestBinary.ManifestDataBinary.Bundle bundle, System.Collections.Generic.List<Beyond.ManifestBinary.ManifestDataBinary.Bundle>& bundles) { }
        // RVA: 0x0B896514  token: 0x600000A
        private static System.Void _TryGetBundleDeps(Beyond.ManifestBinary.ManifestDataBinary manifestDataBinary, Beyond.ManifestBinary.ManifestDataBinary.Bundle bundle, System.Collections.Generic.List<Beyond.ManifestBinary.ManifestDataBinary.Bundle>& bundles) { }
        // RVA: 0x0B896178  token: 0x600000B
        public static System.String GetLogBundleName(Beyond.ManifestBinary.RuntimeManifestBinary manifestBinary, Beyond.ManifestBinary.ManifestDataBinary.Bundle bundle) { }
        // RVA: 0x0B8960F4  token: 0x600000C
        public static System.String GetLogAssetPath(Beyond.ManifestBinary.RuntimeManifestBinary manifestBinary, Beyond.ManifestBinary.ManifestDataBinary.AssetInfo asset) { }
        // RVA: 0x041E1670  token: 0x600000D
        public System.Void .ctor() { }

    }

}

namespace HG.Rendering.Runtime
{

    // TypeToken: 0x2000002  // size: 0x10
    public class HGConstantBufferLayoutAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x2000009  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x600000E
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x0B895FFC  token: 0x600000F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x0B896060  token: 0x6000010
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x6000011
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x200000B  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000012
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0B896870  token: 0x6000013
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x0B896960  token: 0x6000014
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x0B89672C  token: 0x6000015
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x0B8967F8  token: 0x6000016
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x0B8968D4  token: 0x6000017
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x0B896688  token: 0x6000018
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

}

