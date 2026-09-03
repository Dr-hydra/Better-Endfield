// ========================================================
// Dumped by @desirepro
// Assembly: CriMw.CriWare.Vp9.Runtime.dll
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

    // TypeToken: 0x2000003  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000003
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000004
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
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

namespace CriWare
{

    // TypeToken: 0x2000008  // size: 0x10
    public static class CriManaVp9
    {
        // Fields
        private static System.String scriptVersionString;  // const
        public static System.String cri_mana_vp9_name;  // const

        // Methods
        // RVA: 0x02FFF600  token: 0x6000007
        public static System.Boolean SupportCurrentPlatform() { }
        // RVA: 0x03827DB0  token: 0x6000008
        public static System.Void SetupVp9Decoder() { }
        // RVA: 0x03827B50  token: 0x6000009
        private static System.IntPtr criWareUnity_GetAllocateFunc() { }
        // RVA: 0x03827E80  token: 0x600000A
        private static System.IntPtr criWareUnity_GetDeallocateFunc() { }
        // RVA: 0x03827C60  token: 0x600000B
        private static System.IntPtr criManaUnity_GetAllocatorManager() { }
        // RVA: 0x03827BC0  token: 0x600000C
        private static System.Void criMvPly_AttachCodecInterface(System.Int32 codec_type, System.IntPtr codec_if, System.IntPtr codecalpha_if) { }
        // RVA: 0x038279A0  token: 0x600000D
        private static System.Void criVvp9_SetUserAllocator(System.IntPtr alloc_func, System.IntPtr free_func, System.IntPtr usr_obj) { }
        // RVA: 0x038276D0  token: 0x600000E
        private static System.IntPtr criVvp9_GetInterface() { }
        // RVA: 0x03827A40  token: 0x600000F
        private static System.IntPtr criVvp9_GetAlphaInterface() { }

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
        // RVA: 0x032B2C30  token: 0x6000010
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x0690A4C4  token: 0x6000011
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x0690A528  token: 0x6000012
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x6000013
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x200000B  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000014
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0690A7A4  token: 0x6000015
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x0690A894  token: 0x6000016
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x0690A660  token: 0x6000017
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x0690A72C  token: 0x6000018
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x0690A808  token: 0x6000019
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x0690A5BC  token: 0x600001A
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

}

